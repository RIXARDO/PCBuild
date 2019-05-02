using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCbuild_ASP.MVC_.Domain.Abstract;
using PCbuild_ASP.MVC_.Domain.Entities;

namespace PCbuild_ASP.MVC_.Domain.Concrete
{
    class EFProductRepository : IProductRepository
    {
        EFDbContext context = new EFDbContext();

        public IQueryable<Product> Products { get { return context.Products; } }

        public Product DeleteProduct(Guid ProductId)
        {
            Product dbEntry = context.Products.Find(ProductId);
            if (dbEntry != null)
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductID == null)
            {
                context.Products.Add(product);
            }
            else
            {
                Product dbEntry = context.Products.Find(product.ProductID);
                if (dbEntry != null)
                {
                    dbEntry.Status = product.Status;
                    dbEntry.Price = product.Price;
                }
            }
            context.SaveChanges();
        }
    }
}
