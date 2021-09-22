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

namespace Pandora.Application.Service
{
    public class ReferralService : IReferralService
    {
        private readonly ReferralRepository _referralRepository;
        public ReferralService(ReferralRepository ReferralRepository)
        {
            _referralRepository = ReferralRepository;
        }

        public async Task CreateAsync(CreateReferralCommand command, Guid userId)
        {
            if (await _referralRepository.HasReferralByEmail(command.Email))
                throw new AppException("This Email Address has recently been invited");

            Referral referral = new Referral()
            {
                Email = command.Email,
                ReferralCode = await GetRefferalCodeAsync(),
                UserId = userId
            };

            await _referralRepository.Add(referral);
            try
            {
                await SendInvitation("sasantrader002@gmail.com");
            }
            catch (Exception e)
            {
                await _referralRepository.Remove((await _referralRepository.GetByReferralCode(referral.ReferralCode)).Id);
                throw new AppException(e.Message);
            }
        }

        private async Task SendInvitation(string email)
        {

            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            message.From = new MailAddress("sasantrader001@gmail.com");
            message.To.Add(new MailAddress(email));
            message.Subject = "Test";
            message.IsBodyHtml = false; //to make message body as html  
            message.Body = "This is test mail";
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("sasantrader001@gmail.com", "trading@1");
            await smtp.SendMailAsync(message);

        }

        private async Task<string> GetRefferalCodeAsync()
        {
            Random generator = new Random();
            string result;
            do
            {
                result = generator.Next(0, 1000000).ToString("D6");
            } while (await _referralRepository.GetByReferralCode(result) != null);
            return result;
        }
    }
}
