using Pandora.Application.Command.Users;
using Pandora.Domain.Domain;
using Pandora.Domain.Repository;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Collections.Generic;
using Pandora.Application.Contract;
using System;
using Pandora.Application.ViewModel;
using System.Threading.Tasks;

namespace Pandora.Application.Service
{
    public class UserService : IUserService
    {
        const int MaxNumberOfFailedAttemptsToLogin = 3;
        const int BlockMinutesAfterLimitFailedAttemptsToLogin = 1;
        private readonly UserRepository _userRepository;
        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task CreateAsync(CreateUserCommand command)
        {
            if (await _userRepository.HasUserByEmail(command.Email))
                throw new AppException("Email '" + command.Email + "' is already taken");

            User user = command.ToUser();
            user.PasswordHash = BCryptNet.HashPassword(command.Password);

            await _userRepository.Add(user);
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
                    && DateTime.Now < user.LastLoginAttemptAt.Value.AddMinutes(BlockMinutesAfterLimitFailedAttemptsToLogin))
                {
                    AppException exception = new AppException();
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
            user.Email = command.Email;

            await _userRepository.Update(user);
            return new UserViewModel()
            {
                Email = user.Email,
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
    }
}
