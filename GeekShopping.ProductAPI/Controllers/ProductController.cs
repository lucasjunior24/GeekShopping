using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductVO>>> FindAll()
        {
            var products = await _productRepository.FindAll();
            return products == null ? NotFound() : Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductVO>> FindById(long id)
        {
            var productVO = await _productRepository.FindById(id);
            return productVO == null ? NotFound() : Ok(productVO);
        }
        [HttpPost]
        public async Task<ActionResult<ProductVO>> Create([FromBody] ProductVO VO)
        {
            if (VO == null) return NotFound();

            var productCreado = await _productRepository.Create(VO);
            return productCreado == null ? NotFound() : Ok(productCreado);
        }

        [HttpPut]
        public async Task<ActionResult<ProductVO>> Update([FromBody] ProductVO VO)
        {
            if (VO == null) return NotFound();

            var productVO = await _productRepository.Update(VO);
            return productVO == null ? NotFound() : Ok(productVO);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var status = await _productRepository.Delete(id);
            if (!status) return NotFound();

            return Ok(status);
        }
    }
}
