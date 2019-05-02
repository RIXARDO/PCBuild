using PCbuild_ASP.MVC_.Domain.Abstract;
using PCbuild_ASP.MVC_.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCbuild_ASP.MVC_.Domain.Concrete
{
    public class EFPriceRepository: IPriceRepository
    {
        EFDbContext context = new EFDbContext();

        public IQueryable<Price> Prices { get { return context.Prices; } }

        public Price DeletePrice(int PriceId)
        {
            Price dbEntry = context.Prices.Find(PriceId);
            if (dbEntry != null)
            {
                context.Prices.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public void SavePrice(Price price)
        {
            if (price.PriceID == 0)
            {
                context.Prices.Add(price);
            }
            else
            {
                Price dbEntry = context.Prices.Find(price.PriceID);
                if (dbEntry != null)
                {
                    dbEntry.Amount = price.Amount;
                    dbEntry.Product = price.Product;
                    dbEntry.Vendor = price.Vendor;
                    dbEntry.Сurrency = price.Сurrency;
                }
            }
            context.SaveChanges();
        }
    }
}
