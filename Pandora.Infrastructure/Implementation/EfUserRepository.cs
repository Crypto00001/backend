﻿using Microsoft.EntityFrameworkCore;
using Pandora.Domain.Domain;
using Pandora.Domain.Repository;
using Pandora.Infrastructure.Base;
using Pandora.Infrastructure.Context;
using System;
using System.Threading.Tasks;

namespace Pandora.Infrastructure.Implementation
{
    public class EfUserRepository : EFRepository<User>, UserRepository
    {
        public EfUserRepository(EFDbContext context) : base(context)
        {
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Set<User>().SingleOrDefaultAsync(q => q.Email == email);
        }

        public async Task<bool> HasUserByEmail(string email)
        {
            return await _context.Set<User>().AnyAsync(q => q.Email == email);
        }
    }
}
