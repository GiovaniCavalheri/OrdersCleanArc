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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductPrice([FromRoute]Guid id, [FromBody] UpdateProductPriceDTO productDTO)
        {
            try
            {
                var updatedProduct = await _productService.UpdateProductPriceAsync(id, productDTO.NewPrice);
                return Ok(updatedProduct); 
            }
            catch (KeyNotFoundException)
            {

                return NotFound(new { message = "Produto com o ID específicado, não encontrado." });
            }
        }

        [HttpPut("{id}/stock")]
        public async Task<IActionResult> UpdateProductStock([FromRoute]Guid id, [FromBody] UpdateProductStockDTO productDTO)
        {
            try
            {
                var updatedStockProduct = await _productService.UpdateProductStockAsync(id, productDTO.Stock);
                return Ok(updatedStockProduct); 
            }
            catch (KeyNotFoundException)
            {

                return NotFound(new { message = "Produto com o ID específicado, não encontrado." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute]Guid id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);
                return StatusCode(204); 
            }
            catch (KeyNotFoundException)
            {

                return NotFound(new { message = "Produto com o ID específicado, não encontrado." });
            }
        }
    }
}