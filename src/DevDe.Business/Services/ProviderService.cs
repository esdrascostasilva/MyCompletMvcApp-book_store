using AppMvcBasic.Models;
using DevDe.Business.Interfaces;
using DevDe.Business.Models.Validation;
using System;
using System.Threading.Tasks;

namespace DevDe.Business.Services
{
    public class ProviderService : BaseService, IProviderService
    {
        public async Task Add(Provider provider)
        {
            // Validate the state of entity
            //var validator = new ProviderValidation();
            //var result = validator.Validate(provider);

            if (!ExecuteValidation(new ProviderValidation(), provider) && !ExecuteValidation(new AddressValidation(), provider.Address))
            {
                //result.Errors;
                return;
            }
            // Validate if exist with same document

            //return;
        }

        public async Task Update(Provider provider)
        {
            if (!ExecuteValidation(new ProviderValidation(), provider))
            {
                return;
            }
        }

        public async Task UpdateAddress(Address address)
        {
            if (!ExecuteValidation(new AddressValidation(), address))
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
