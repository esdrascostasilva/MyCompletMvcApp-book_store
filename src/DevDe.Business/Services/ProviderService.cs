using AppMvcBasic.Models;
using DevDe.Business.Interfaces;
using DevDe.Business.Models.Validation;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DevDe.Business.Services
{
    public class ProviderService : BaseService, IProviderService
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IAddressRepository _addressRepository;

        public ProviderService(IProviderRepository providerRepository, IAddressRepository addressRepository, INotifier notifier) : base(notifier)
        {
            _providerRepository = providerRepository;
            _addressRepository = addressRepository;
        }


        public async Task Add(Provider provider)
        {
            if (!ExecuteValidation(new ProviderValidation(), provider) || !ExecuteValidation(new AddressValidation(), provider.Address))
            {
                return;
            }

            if (_providerRepository.Find(p=>p.Document == provider.Document).Result.Any())
            {
                Notify("Já existe um fornecedor com o documento informado");
                return;
            }

            await _providerRepository.Add(provider);
        }

        public async Task Update(Provider provider)
        {
            if (!ExecuteValidation(new ProviderValidation(), provider))
                return;
            
            if (_providerRepository.Find(p=>p.Document == provider.Document && p.Id != provider.Id).Result.Any())
            // In this case, you should not let the provider change the document
            {
                Notify("Já existe um fornecedor com este documento informado");
                return;
            }

            await _providerRepository.Add(provider);
        }

        public async Task UpdateAddress(Address address)
        {
            if (!ExecuteValidation(new AddressValidation(), address))
                return;

            await _addressRepository.Update(address);
        }

        public async Task Remove(Guid id)
        {
            if (_providerRepository.GetProviderProductsAddress(id).Result.Products.Any())
            {
                Notify("O fornecedor possui produtos cadastrados!");
                return;
            }

            await _providerRepository.Remove(id);
        }

        public void Dispose()
        {
            _providerRepository?.Dispose();
            _addressRepository?.Dispose();
        }
    }

}
