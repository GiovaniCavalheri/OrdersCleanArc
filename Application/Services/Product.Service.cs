using Application.DTOs;
using Domain.Entities;
using Application.Interfaces;

namespace Application.Services
{

    public class ProductService
    {
        private readonly IProductRepository _productRepository; 

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductResponseDTO> CreateProductAsync(CreateProductDTO createprodutoDTO)
        {
            var product = new Product(createprodutoDTO.ProductName, createprodutoDTO.ProductPrice, createprodutoDTO.QuantityStock);
            await _productRepository.AddAsync(product);

            return new ProductResponseDTO
            {
                Id = product.Id,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                QuantityStock = product.QuantityStock
            };
        }

        public async Task<IEnumerable<ProductResponseDTO>> ListProductsAsync()
        {
            var products = await _productRepository.ListAllAsync();
            var listProducts = new List<ProductResponseDTO>();
            foreach (var product in products)
            {
                listProducts.Add(new ProductResponseDTO
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    ProductPrice = product.ProductPrice,
                    QuantityStock = product.QuantityStock
                });
            }
            return listProducts;
        }

        public async Task<ProductResponseDTO>GetProductByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                throw new KeyNotFoundException("Product is not found.");

            return new ProductResponseDTO
            {
                Id = product.Id,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                QuantityStock = product.QuantityStock
            };
        }

        public async Task<ProductResponseDTO> UpdateProductPriceAsync(Guid id, decimal newPrice)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                throw new KeyNotFoundException("Product is not found.");

            product.ChangePrice(newPrice); 
            await _productRepository.UpdateAsync(product);
            
            return new ProductResponseDTO
            {
                Id = product.Id,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                QuantityStock = product.QuantityStock
            };
        } 

        public async Task<ProductResponseDTO>UpdateProductStockAsync(Guid id, int newQuantity)
        {
            var product = await _productRepository.GetByIdAsync(id); 
            if(product == null)     
                throw new KeyNotFoundException("Product is not found.");
            
            product.UpdateStock(newQuantity);
            await _productRepository.UpdateAsync(product);

            return new ProductResponseDTO
            {
                Id = product.Id,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                QuantityStock = product.QuantityStock
            };  
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                throw new KeyNotFoundException("Product is not found.");

            await _productRepository.DeleteAsync(id);
        }
    }
}
