using Pandora.Application.Command.Referrals;
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
using System.Linq;

namespace Pandora.Application.Service
{
    public class ReferralService : IReferralService
    {
        private readonly ReferralRepository _referralRepository;
        private readonly UserRepository _userRepository;
        public ReferralService(ReferralRepository referralRepository, UserRepository userRepository)
        {
            _referralRepository = referralRepository;
            _userRepository = userRepository;
        }

        public async Task CreateAsync(CreateReferralCommand command, Guid userId)
        {
            var user = await _userRepository.GetById(userId);

            // if (await _referralRepository.HasReferralByEmail(command.Email))
            //     throw new AppException("This Email Address has recently been invited");

            if (user.Email==command.Email || await _userRepository.HasUserByEmail(command.Email))
                throw new AppException("This Email Address is reserved");

            if (await _referralRepository.IsReferralLimitationFull(userId))
                throw new AppException("You could not invite more than 30 people");

            Referral referral = new Referral()
            {
                UserId = userId
            };

            
            try
            {
                await SendInvitation(referral);
                await _referralRepository.Add(referral);
            }
            catch (Exception e)
            {
                //await _referralRepository.Remove((await _referralRepository.GetByReferralCode(referral.ReferralCode)).Id);
                throw new AppException(e.Message);
            }
        }

        private async Task SendInvitation(Referral referral)
        {

            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            message.From = new MailAddress("sasantrader001@gmail.com");
            //message.To.Add(new MailAddress(referral.Email));
            message.Subject = "Test";
            message.IsBodyHtml = false; //to make message body as html  
            //message.Body = "http://localhost:4200/register/?referralCode=" + referral.ReferralCode;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("sasantrader001@gmail.com", "trading@1");
            await smtp.SendMailAsync(message);

        }
        
        public async Task<int> GetActiveInviteesCount(Guid userId)
        {
            return await _referralRepository.GetActiveInviteesCount(userId);
        }        
        public async Task<List<ReferralViewModel>> GetActiveInvitees(Guid userId)
        {
            return (await _referralRepository.GetActiveInvitees(userId)).Select(q => new ReferralViewModel
            {
                Email = q.Email,
                FirstName = q.FirstName,
                LastName = q.LastName,
                IsActive = q.HasInvested
            }).ToList();
        }
    }
}
