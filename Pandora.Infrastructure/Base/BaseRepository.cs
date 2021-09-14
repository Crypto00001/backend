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
        void Add(T entity);
        void Update(T entity);
        void Remove(Guid id);
        void Remove(T entity);

        T Get(int id);
        List<T> Get();
    }
}
