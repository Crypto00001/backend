using Pandora.Domain.Domain;
using Pandora.Domain.Repository;
using Pandora.Infrastructure.Base;
using Pandora.Infrastructure.Context;
using System;
using System.Linq;

namespace Pandora.Infrastructure.Implementation
{
    public class EfUserRepository : EFRepository<User>, UserRepository
    {
        public EfUserRepository(EFDbContext context) : base(context)
        {
        }

        public User GetUserByEmail(string email)
        {
            return _context.Set<User>().SingleOrDefault(q => q.Email == email);
        }

        public bool HasUserByEmail(string email)
        {
            return _context.Set<User>().Any(q => q.Email == email);
        }
    }
}
