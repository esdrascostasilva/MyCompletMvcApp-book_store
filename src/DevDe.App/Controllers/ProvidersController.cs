using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DevDe.App.ViewModels;
using DevDe.Business.Interfaces;
using AutoMapper;
using AppMvcBasic.Models;

namespace DevDe.App.Controllers
{
    public class ProvidersController : BaseController
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public ProvidersController(IProviderRepository providerRepository, IMapper mapper, IAddressRepository addressRepository)
        {
            _providerRepository = providerRepository;
            _mapper = mapper;
            _addressRepository = addressRepository;
        }

        [Route("providers-list")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProviderViewModel>>(await _providerRepository.GetAll()));
        }

        [Route("providers-data/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var providerViewModel = await GetProviderAddress(id);
            if (providerViewModel == null)
            {
                return NotFound();
            }

            return View(providerViewModel);
        }

        [Route("new-provider")]
        public IActionResult Create()
        {
            return View();
        }

        [Route("new-provider")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProviderViewModel providerViewModel)
        {
            if (!ModelState.IsValid)
                return View(providerViewModel);

            var provider = _mapper.Map<Provider>(providerViewModel);
            await _providerRepository.Add(provider);

            return RedirectToAction("Index");
        }

        [Route("providers-edit/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var providerViewModel = await GetProviderProductsAddress(id);
            
            if (providerViewModel == null)
            {
                return NotFound();
            }

            return View(providerViewModel);
        }

        [Route("providers-edit/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProviderViewModel providerViewModel)
        {
            if (id != providerViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return View(providerViewModel);

            var provider = _mapper.Map<Provider>(providerViewModel);
            await _providerRepository.Update(provider);

            return RedirectToAction("Index");
            
        }

        [Route("delete-providers/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var providerViewModel = await GetProviderAddress(id);
            if (providerViewModel == null)
            {
                return NotFound();
            }

            return View(providerViewModel);
        }

        [Route("delete-providers/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var providerViewModel = await GetProviderAddress(id);

            if (providerViewModel == null)
            {
                return NotFound();
            }

            await _providerRepository.Remove(id);

            return RedirectToAction("Index");
        }

        [Route("get-address-providers/{id:guid}")]
        public async Task<IActionResult> GetAddress(Guid id)
        {
            var provider = await GetProviderAddress(id);

            if (provider == null)
            {
                return NotFound();
            }

            return PartialView("_AddressDetails", provider);
        }

        [Route("update-address-providers/{id:guid}")]
        public async Task<IActionResult> AddressUpdate(Guid id)
        {
            var provider = await GetProviderAddress(id);

            if (provider == null)
            {
                return NotFound();
            }

            return PartialView("_AddressUpdate", new ProviderViewModel { Address = provider.Address });
        }

        [Route("update-address-providers/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddressUpdate(ProviderViewModel providerViewModel)
        {
            ModelState.Remove("Name");
            ModelState.Remove("Document");

            if (!ModelState.IsValid)
                return PartialView("_AddressUpdate", providerViewModel);

            await _addressRepository.Update(_mapper.Map<Address>(providerViewModel.Address));

            var url = Url.Action("GetAddress", "Providers", new { id = providerViewModel.Address.ProviderId });

            return Json(new { success = true, url });
        }

        private async Task<ProviderViewModel> GetProviderAddress(Guid id)
        {
            return _mapper.Map<ProviderViewModel>(await _providerRepository.GetProviderAddress(id));
        }

        private async Task<ProviderViewModel> GetProviderProductsAddress(Guid id)
        {
            return _mapper.Map<ProviderViewModel>(await _providerRepository.GetProviderProductsAddress(id));
        }
    }
}
