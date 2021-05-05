using AppMvcBasic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevDe.Business.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByProvider(Guid providerId);
        Task<IEnumerable<Product>> GetProductsProviders();
        Task<Product> GetProductProvider(Guid id);

    }
}
