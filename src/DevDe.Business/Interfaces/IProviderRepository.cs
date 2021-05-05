using AppMvcBasic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevDe.Business.Interfaces
{
    public interface IProviderRepository : IRepository<Provider>
    {
        Task<Provider> GetProviderAddress(Guid id);
        Task<Provider> GetProviderProductsAddress(Guid id);
    }
}
