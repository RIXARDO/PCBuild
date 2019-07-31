using PCbuilder_ASP.MVC_.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCbuilder_ASP.MVC_.Domain.Abstract;

namespace PCbuilder_ASP.MVC_.Domain.Concrete
{
    public class EFUnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly EFDbContext context;

        private Dictionary<string, IRepository> repositores;


        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        public EFUnitOfWork()
        {
            context = new EFDbContext();
            repositores = new Dictionary<string, IRepository>();
        }

        public EFUnitOfWork(EFDbContext dbContext)
        {
            repositores = new Dictionary<string, IRepository>();
            context = dbContext;
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Register(IRepository repository)
        {
            repositores.Add(string.Concat(repository.GetType().Name,repositores.Count), repository);
        }

        public object GetSource()
        {
            return context;
        }
    }
}
