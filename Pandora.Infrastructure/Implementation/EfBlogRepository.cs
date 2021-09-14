using Pandora.Domain.Domain;
using Pandora.Domain.Repository;
using Pandora.Infrastructure.Base;
using Pandora.Infrastructure.Context;

namespace Pandora.Infrastructure.Implementation
{
    public class EfUserRepository : EFRepository<User>, UserRepository
    {
        public EfUserRepository(EFDbContext context) : base(context)
        {
        }
    }
}
