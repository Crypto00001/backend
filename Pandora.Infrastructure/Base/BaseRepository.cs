using Pandora.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Infrastructure.Base
{
    public interface BaseRepository<T> where T : BaseEntity
    {
        Task Add(T entity);
        Task Update(T entity);
        Task Remove(Guid id);
        Task Remove(T entity);

        Task<T> Get(Guid id);
        Task<List<T>> GetAll();
    }
}
