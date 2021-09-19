using System;
using System.Collections.Generic;
using Pandora.Domain.Domain;

namespace Pandora.Domain.Repository
{
    public interface UserRepository
    {
        void Add(User User);
        void Update(User User);
        void Remove(Guid UserId);
        User Get(Guid UserId);
        bool HasUserByEmail(string email);
        User GetUserByEmail(string email);
        List<User> GetAll();

    }
}
