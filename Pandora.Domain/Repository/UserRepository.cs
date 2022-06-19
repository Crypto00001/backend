using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pandora.Domain.Domain;

namespace Pandora.Domain.Repository
{
    public interface UserRepository
    {
        Task Add(User User);
        Task Update(User User);
        Task Remove(Guid UserId);
        Task<User> GetById(Guid UserId);
        Task<User> GetByUserReferralCode(string userReferralCode);
        Task<User> GetByReferralCode(string referralCode);
        Task<bool> HasUserByEmail(string email);
        Task<User> GetUserByEmail(string email);
        Task<List<User>> GetAll();

    }
}
