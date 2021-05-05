using AppMvcBasic.Models;
using DevDe.Business.Interfaces;
using DevDe.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevDe.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(MyDbContext context) : base(context) { }

        public async Task<Product> GetProductProvider(Guid id)
        {
            return await Db.Products.AsNoTracking().Include(p => p.Provider)
                .FirstOrDefaultAsync(pr => pr.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsProviders()
        {
            return await Db.Products.AsNoTracking()
                .Include(p => p.Provider)
                .OrderBy(p => p.Name).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByProvider(Guid providerId)
        {
            return await Find(p => p.ProviderId == providerId);
        }
    }
}
