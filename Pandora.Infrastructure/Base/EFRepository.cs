using Microsoft.EntityFrameworkCore;
using Pandora.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandora.Infrastructure.Base
{
    public class EFRepository<T> where T : BaseEntity, new()
    {
        protected DbContext _context;
        private DbSet<T> entities;

        public EFRepository(DbContext context)
        {
            _context = context;
        }

        private DbSet<T> Entities
        {
            get
            {
                if (entities == null)
                {
                    entities = _context.Set<T>();
                }

                return entities;
            }
        }

        public async Task Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            entity.Id = entity.Id == Guid.Empty ? Guid.NewGuid() : entity.Id;
            entity.CreateDate = DateTime.Now;

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            Entities.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(Guid id)
        {
            T entity = new T()
            {
                Id = id
            };
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}