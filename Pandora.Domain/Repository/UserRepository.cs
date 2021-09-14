using System;
using System.Collections.Generic;
using Pandora.Domain.Domain;

namespace Pandora.Domain.Repository
{
    public interface UserRepository
    {
        void Add(User User);
        void Remove(Guid UserId);

        User Get(int UserId);
        List<User> Get();

    }
}
