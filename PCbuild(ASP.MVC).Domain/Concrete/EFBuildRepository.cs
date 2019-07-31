using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PCbuilder_ASP.MVC_.Domain.Abstract;
using PCbuilder_ASP.MVC_.Domain.Entities;

namespace PCbuilder_ASP.MVC_.Domain.Concrete
{
    public class EFBuildRepository : IBuildEntityRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<BuildEntity> Builds
        {
            get
            {
                return context.BuildEntities;
            }
        }

        public void SaveBuild(BuildEntity build)
        {
            if (build.BuildEntityGuid == null)
            {
                context.BuildEntities.Add(build);
            }
            else
            {
                BuildEntity dbEntry = context.BuildEntities.Find(build.BuildEntityGuid);
                if (dbEntry != null)
                {
                    dbEntry.CPU = build.CPU;
                    dbEntry.GPU = build.GPU;
                    dbEntry.CPUID = build.CPUID;
                    dbEntry.GPUID = build.GPUID;
                    //doubtful
                    dbEntry.UserID = build.UserID;
                    //
                }
            }
            context.SaveChanges();
        }

        public BuildEntity Delete(int BuildId)
        {
            BuildEntity dbEntry = context.BuildEntities.Find(BuildId);
            if (dbEntry != null)
            {
                context.BuildEntities.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        
    }
}