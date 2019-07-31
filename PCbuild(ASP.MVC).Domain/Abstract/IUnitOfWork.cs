using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCbuilder_ASP.MVC_.Domain.Abstract
{
    public interface IUnitOfWork: IDisposable
    {
        //Bed decision
        object GetSource();
        void Register(IRepository repository);
        void Save();
    }
}
