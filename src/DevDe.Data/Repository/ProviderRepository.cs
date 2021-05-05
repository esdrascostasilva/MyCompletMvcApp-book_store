using AppMvcBasic.Models;
using DevDe.Business.Interfaces;
using DevDe.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevDe.Data.Repository
{
    public class ProviderRepository : Repository<Provider>, IProviderRepository
    {
        public ProviderRepository(MyDbContext context) : base(context) { }

        public async Task<Provider> GetProviderAddress(Guid id)
        {
            return await Db.Providers.AsNoTracking().Include(p => p.Address).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Provider> GetProviderProductsAddress(Guid id)
        {
            return await Db.Providers.AsNoTracking()
                .Include(p => p.Products)
                .Include(p => p.Address)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
