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
        private readonly IMapper _mapper;

        public ProvidersController(IProviderRepository providerRepository, IMapper mapper)
        {
            _providerRepository = providerRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProviderViewModel>>(await _providerRepository.GetAll()));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var providerViewModel = await GetProviderAddress(id);
            if (providerViewModel == null)
            {
                return NotFound();
            }

            return View(providerViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

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

        public async Task<IActionResult> Edit(Guid id)
        {
            var providerViewModel = await GetProviderProductsAddress(id);
            
            if (providerViewModel == null)
            {
                return NotFound();
            }

            return View(providerViewModel);
        }

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

        public async Task<IActionResult> Delete(Guid id)
        {
            var providerViewModel = await GetProviderAddress(id);
            if (providerViewModel == null)
            {
                return NotFound();
            }

            return View(providerViewModel);
        }

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
