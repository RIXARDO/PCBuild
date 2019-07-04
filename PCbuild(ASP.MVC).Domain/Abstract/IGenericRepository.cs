using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCbuild_ASP.MVC_.Domain.Abstract
{
    public interface IGenericRepository<TEntity>: IRepository where TEntity: class
    {
        void Create(TEntity item);
        TEntity FindById(Guid id);
        IEnumerable<TEntity> Get();
        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
        void Delete(TEntity item);
        void Update(TEntity item);
    }
}
