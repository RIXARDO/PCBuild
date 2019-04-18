using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PCbuild_ASP.MVC_.Domain.Entities;

namespace PCbuild_ASP.MVC_.Domain.Abstract
{
    public interface IBuildEntityRepository
    {
        IQueryable<BuildEntity> Builds { get; }
        void SaveBuild(BuildEntity build);
        BuildEntity Delete(int BuildId);
    }
}