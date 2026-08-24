using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.ListProductsAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO createProductDTO)
        {
            var newProduct = await _productService.CreateProductAsync(createProductDTO);
            return StatusCode(201, newProduct);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById([FromRoute]Guid id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                return Ok(product); 
            }
            catch (KeyNotFoundException) // = Alterar a excessão
            {

                return NotFound(new { message = "Produto com o ID específicado, não existe." }); 
            }
        }
    }
}