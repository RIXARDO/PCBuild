using PCbuild_ASP.MVC_.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCbuild_ASP.MVC_.Domain.Concrete
{
    class EFPriceRepository
    {
        public EFDbContext context = new EFDbContext();

        public IQueryable<Price> Products { get { return context.Prices; } }

        public Price DeletePrice(Guid priceGuid)
        {
            Price dbEntry = context.Prices.Find(priceGuid);
            if (dbEntry != null)
            {
                context.Prices.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public void SavePrice(Price price)
        {
            if (price.PriceGuid == null)
            {
                //Add Guid generation!!!
                context.Prices.Add(price);
            }
            else
            {
                Price dbEntry = context.Prices.Find(price.PriceGuid);
                if (dbEntry != null)
                {
                    dbEntry.Amount = price.Amount;
                    dbEntry.PriceGuid = price.PriceGuid;
                    dbEntry.Vendor = price.Vendor;
                    dbEntry.Сurrency = price.Сurrency;
                }
            }
            context.SaveChanges();
        }
    }
}
