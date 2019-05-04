using PCbuild_ASP.MVC_.Domain.Abstract;
using PCbuild_ASP.MVC_.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCbuild_ASP.MVC_.Domain.Concrete
{
    class EFProductRepository : IProductRepository
    {
        public EFDbContext context = new EFDbContext();

        public IQueryable<Product> Products { get { return context.Products; } }

        public Product DeleteProduct(Guid productGuid)
        {
            Product dbEntry = context.Products.Find(productGuid);
            if (dbEntry != null)
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductGuid == null)
            {
                product.ProductGuid = Guid.NewGuid();
                context.Products.Add(product);
            }
            else
            {
                Product dbEntry = context.Products.Find(product.ProductGuid);
                if (dbEntry != null)
                {
                    dbEntry.ProductGuid = product.ProductGuid;
                    dbEntry.Price = product.Price;
                    dbEntry.Status = product.Status;
                }
            }
            context.SaveChanges();
        }
    }
}