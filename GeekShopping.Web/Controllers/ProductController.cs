using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> ProductIndex()
        {
            var product = await _productService.FindAllProducts();
            return View(product);
        }

        public IActionResult ProductCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductModel productModel)
        {
            if(ModelState.IsValid)
            {
                var product = await _productService.CreateProduct(productModel);
                if(product != null) return RedirectToAction("ProductIndex");
            }
            return View(productModel);
        }
        public async Task<IActionResult> ProductUpdate(long id)
        {
            var product = await _productService.FindProductById(id);
            if(product != null) return View(product);
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> ProductUpdate(ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                var product = await _productService.UpdateProduct(productModel);
                if (product != null) return RedirectToAction(nameof(ProductIndex));
            }
            return View(productModel);
        }

        public async Task<IActionResult> ProductDelete(long id)
        {
            var product = await _productService.FindProductById(id);
            if (product != null) return View(product);
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> ProductDelete(ProductModel productModel)
        {
            var response = await _productService.DeleteProductById(productModel.Id);
            if (response != false) return RedirectToAction(nameof(ProductIndex));

            return View(productModel);
        }
    }
}
