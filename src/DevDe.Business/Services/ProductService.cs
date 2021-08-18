using AppMvcBasic.Models;
using DevDe.Business.Interfaces;
using DevDe.Business.Models.Validation;
using System;
using System.Threading.Tasks;

namespace DevDe.Business.Services
{
    public class ProductService : BaseService, IProductService
    {
        public async Task Add(Product product)
        {
            if (!ExecuteValidation(new ProductValidation(), product))
            {
                return;
            }
        }
        
        public async Task Update(Product product)
        {
            if (!ExecuteValidation(new ProductValidation(), product))
            {
                return;
            }
        }
        public async Task Remove(Guid id)
        {
            throw new NotImplementedException();
        }

    }

}
