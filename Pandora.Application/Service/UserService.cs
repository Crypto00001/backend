using Pandora.Application.Command.Users;
using Pandora.Domain.Domain;
using Pandora.Domain.Repository;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Collections.Generic;
using Pandora.Application.Contract;
using System;
using Pandora.Application.ViewModel;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using Pandora.Application.Enums;

namespace Pandora.Application.Service
{
    public class UserService : IUserService
    {
        const int MaxNumberOfFailedAttemptsToLogin = 3;
        const int BlockMinutesAfterLimitFailedAttemptsToLogin = 1;
        private readonly UserRepository _userRepository;
        private readonly WalletRepository _walletRepository;
        private readonly ReferralRepository _referralRepository;

        public UserService(UserRepository userRepository, WalletRepository walletRepository,
            ReferralRepository referralRepository)
        {
            _userRepository = userRepository;
            _walletRepository = walletRepository;
            _referralRepository = referralRepository;
        }

        public async Task CreateAsync(CreateUserCommand command)
        {
            if (await _userRepository.HasUserByEmail(command.Email))
                throw new AppException("Email '" + command.Email + "' is already taken");

            User user = command.ToUser();
            user.PasswordHash = BCryptNet.HashPassword(command.Password);
            user.UserReferralCode = await GetUserRefferalCodeAsync();
            user.Id = Guid.NewGuid();

            await ReferralUpdating(command.ReferralCode, user.Id);
            
            await WalletCreating(user.Id);

            await _userRepository.Add(user);
        }

        private async Task WalletCreating(Guid userId)
        {
            await _walletRepository.Add(new Wallet
            {
                Balance = 2,
                UserId = userId,
                InvestedBalance = 0,
                Type = (int)WalletType.Bitcoin,
                AvailableBalance = 2,
                Address = "asdfasdfa"
            });
            await _walletRepository.Add(new Wallet
            {
                Balance = 1,
                UserId = userId,
                InvestedBalance = 0,
                Type = (int)WalletType.Etherium,
                AvailableBalance = 1,
                Address = "asdfasdfa"
            });
            await _walletRepository.Add(new Wallet
            {
                Balance = 0,
                UserId = userId,
                InvestedBalance = 0,
                Type = (int)WalletType.Litecoin,
                AvailableBalance = 0,
                Address = "asdfasdfa"
            });
            await _walletRepository.Add(new Wallet
            {
                Balance = 0,
                UserId = userId,
                InvestedBalance = 0,
                Type = (int)WalletType.Zcash,
                AvailableBalance = 0,
                Address = "asdfasdfa"
            });
        }

        private async Task ReferralUpdating(string referralCode, Guid userId)
        {
            if (!string.IsNullOrEmpty(referralCode))
            {
                var referral = await _userRepository.GetByReferralCode(referralCode);
                if (referral != null)
                {
                    if (!await _referralRepository.IsReferralLimitationFull(referral.Id))
                    {
                        await _referralRepository.Add(new Referral
                        {
                            UserId = referral.Id,
                            InvitedUserId = userId
                        });
                    }
                    else
                    {
                        throw new AppException("The referral user has the maximum invited users");
                    }
                }
                else
                {
                    throw new AppException("The referral user has not been found");
                }
            }
        }

        private async Task<string> GetUserRefferalCodeAsync()
        {
            Random generator = new Random();
            string result;
            do
            {
                result = generator.Next(0, 1000000).ToString("D6");
            } while (await _userRepository.GetByUserReferralCode(result) != null);

            return result;
        }

        public async Task<User> Authenticate(LoginCommand model)
        {
            var user = await _userRepository.GetUserByEmail(model.Email);
            if (user == null)
                throw new AppException("Invalid username or password");

            if (user == null || !BCryptNet.Verify(model.Password, user.PasswordHash))
            {
                user.LastLoginAttemptAt = DateTime.Now;
                user.LoginFailedAttemptsCount++;
                await _userRepository.Update(user);

                if (user.LoginFailedAttemptsCount >= MaxNumberOfFailedAttemptsToLogin
                    && user.LastLoginAttemptAt.HasValue
                    && DateTime.Now <
                    user.LastLoginAttemptAt.Value.AddMinutes(BlockMinutesAfterLimitFailedAttemptsToLogin))
                {
                    AppException exception = new AppException("Invalid username or password");
                    exception.Data.Add("Captcha", true);
                    throw exception;
                }

                throw new AppException("Invalid username or password");
            }
            else
            {
                user.LoginFailedAttemptsCount = 0;
                user.LastLoginAttemptAt = DateTime.Now;
                await _userRepository.Update(user);
            }

            return user;
        }

        public async Task<User> GetById(Guid UserId)
        {
            return await _userRepository.GetById(UserId);
        }

        public async Task<UserViewModel> GetCurrenctUserById(Guid userId)
        {
            User user = await _userRepository.GetById(userId);
            return new UserViewModel()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Country = user.Country
            };
        }

        public async Task<List<User>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        public async Task Remove(RemoveUserCommand command)
        {
            await _userRepository.Remove(command.Id);
        }

        public async Task<UserViewModel> UpdateAsync(UpdateUserCommand command, Guid userId)
        {
            User user = await _userRepository.GetById(userId);

            user.FirstName = command.FirstName;
            user.LastName = command.LastName;
            user.Country = command.Country;

            await _userRepository.Update(user);
            return new UserViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Country = user.Country
            };
        }

        public async Task UpdatePassword(UpdatePasswordUserCommand command, Guid userId)
        {
            User user = await _userRepository.GetById(userId);

            if (!BCryptNet.Verify(command.OldPassword, user.PasswordHash))
                throw new AppException("OldPassword is incorrect");

            user.PasswordHash = BCryptNet.HashPassword(command.NewPassword);

            await _userRepository.Update(user);
        }

        public async Task ResetPasswordRequest(ResetPasswordRequestCommand command)
        {
            User user = await _userRepository.GetUserByEmail(command.Email);

            if (user == null)
                throw new AppException("Email address has not registered");

            Random generator = new Random();
            var resetPasswordCode = generator.Next(0, 1000000).ToString("D6");
            await SendResetPasswordCode(command.Email, resetPasswordCode);

            user.ResetPasswordCode = resetPasswordCode;
            await _userRepository.Update(user);
        }

        private async Task SendResetPasswordCode(string email, string code)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            message.From = new MailAddress("sasantrader001@gmail.com");
            message.To.Add(new MailAddress(email));
            message.Subject = "Test";
            message.IsBodyHtml = false; //to make message body as html  
            message.Body = code;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("sasantrader001@gmail.com", "trading@1");
            await smtp.SendMailAsync(message);
        }

        public async Task DoResetPassword(DoResetPasswordCommand command)
        {
            User user = await _userRepository.GetUserByEmail(command.Email);

            if (user == null)
                throw new AppException("Email address has not registered");

            if (user.ResetPasswordCode != command.ResetCode)
                throw new AppException("Reset code is not correct");

            user.PasswordHash = BCryptNet.HashPassword(command.NewPassword);

            await _userRepository.Update(user);
        }
    }
}