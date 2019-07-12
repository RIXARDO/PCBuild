using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCbuild_ASP.MVC_.Domain.Abstract;

namespace PCbuild_ASP.MVC_.Domain.Concrete
{
    public class EFRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        EFDbContext context;

        DbSet<TEntity> dbset;

        public EFRepository(IUnitOfWork unitOfWork, EFDbContext dbContext)
        {
            unitOfWork.Register(this);
            //Bad Decision
            context = (EFDbContext)unitOfWork.GetSource();
            dbset = context.Set<TEntity>();
        }

        public EFRepository(EFDbContext dbContext)
        {
            context = dbContext;
            dbset = context.Set<TEntity>();
        }

        public void Create(TEntity item)
        {
            dbset.Add(item);
        }

        public void Delete(TEntity item)
        {
            dbset.Remove(item);
        }

        public TEntity FindById(Guid id)
        {
            return dbset.Find(id);
        }

        public IEnumerable<TEntity> Get()
        {
            return dbset.AsNoTracking().ToList();
        }

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return dbset.AsNoTracking().Where(predicate).ToList();
        }

        public void Update(TEntity item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
    }
}
