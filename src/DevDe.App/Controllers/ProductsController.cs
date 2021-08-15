using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DevDe.App.ViewModels;
using DevDe.Business.Interfaces;
using AutoMapper;
using AppMvcBasic.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace DevDe.App.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductRepository _productRepository;
        private readonly IProviderRepository _providerRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository, IProviderRepository providerRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _providerRepository = providerRepository;
            _mapper = mapper;
        }

        [Route("products-list")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetProductsProviders()));
        }

        [Route("products-details/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var productViewModel = await GetProduct(id);

            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        [Route("new-product")]
        public async Task<IActionResult> Create()
        {
            var productViewModel = await FillProviders(new ProductViewModel());
            return View(productViewModel);
        }

        [Route("new-product")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            productViewModel = await FillProviders(productViewModel);

            if (!ModelState.IsValid)
                return View(productViewModel);

            var imgPrefix = Guid.NewGuid() + "_";

            if (!await UploadFile(productViewModel.ImageUpload, imgPrefix))
            {
                return View(productViewModel);
            }

            productViewModel.Image = imgPrefix + productViewModel.ImageUpload.FileName;

            await _productRepository.Add(_mapper.Map<Product>(productViewModel));

            return RedirectToAction("Index");
            
        }

        [Route("edit-product/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var productViewModel = await GetProduct(id);

            if (productViewModel == null)
            {
                return NotFound();
            }
            
            return View(productViewModel);
        }

        [Route("edit-product/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id)
            {
                return NotFound();
            }   

            var updateProduct = await GetProduct(id);
            productViewModel.Provider = updateProduct.Provider;
            productViewModel.Image = updateProduct.Image;

            if (!ModelState.IsValid)
            {
                return View(productViewModel);
            }

            if (productViewModel.ImageUpload != null)
            {
                var imgPrefix = Guid.NewGuid() + "_";
                if (!await UploadFile(productViewModel.ImageUpload, imgPrefix))
                {
                    return View(productViewModel);
                }

                updateProduct.Image = imgPrefix + productViewModel.ImageUpload.FileName;
            }

            updateProduct.Name = productViewModel.Name;
            updateProduct.Description = productViewModel.Description;
            updateProduct.Value = productViewModel.Value;
            updateProduct.Active = productViewModel.Active;

            await _productRepository.Update(_mapper.Map<Product>(updateProduct));

            return RedirectToAction("Index");
        }

        [Route("product-delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await GetProduct(id);
            
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [Route("product-delete/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = await GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            await _productRepository.Remove(id);

            return RedirectToAction("Index");
        }

        private async Task<ProductViewModel> GetProduct(Guid id)
        {
            var product = _mapper.Map<ProductViewModel>(await _productRepository.GetProductProvider(id));
            product.Providers = _mapper.Map<IEnumerable<ProviderViewModel>>(await _providerRepository.GetAll());

            return product;
        }

        private async Task<ProductViewModel> FillProviders(ProductViewModel product)
        {
            product.Providers = _mapper.Map<IEnumerable<ProviderViewModel>>(await _providerRepository.GetAll());
            return product;
        }

        private async Task<bool> UploadFile(IFormFile file, string imgPrefix)
        {
            if (file.Length <= 0)
                return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imgPrefix + file.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Already exist a file with same name!");
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return true;
        }

    }
}
