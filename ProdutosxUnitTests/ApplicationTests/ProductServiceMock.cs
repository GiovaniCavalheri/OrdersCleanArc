using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using FluentAssertions;
using Moq;

namespace ProdutosxUnitTests.ApplicationTests;

public class ProductServiceTests
{
    [Fact]
    public async Task CreateProductAsync_DadosValidos_DeveCriarProdutoComSucesso()
    {
        var mockRepo = new Mock<IProductRepository>();

        mockRepo.Setup(repo => repo.AddAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);

        var dto = new CreateProductDTO
        {
            ProductName = "Pão Fresco",
            ProductPrice = 12m,
            QuantityStock = 5
        };

        var productService = new ProductService(mockRepo.Object);
        var result = await productService.CreateProductAsync(dto);

        result.ProductName.Should().Be(dto.ProductName);
        result.ProductPrice.Should().Be(dto.ProductPrice);
        result.QuantityStock.Should().Be(dto.QuantityStock);
        result.Id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task GetProductByIdAsync_ProdutoNaoExiste_DeveLancarExcecao()
    {
        var idInexistente = Guid.NewGuid();
        var mockRepo = new Mock<IProductRepository>();

        mockRepo.Setup(repo => repo.GetByIdAsync(idInexistente)).ReturnsAsync((Product?)null);

        var productService = new ProductService(mockRepo.Object);
        Func<Task> act = async () => await productService.GetProductByIdAsync(idInexistente);

        await act.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task ListProductAsync_DadosValidos_DeveRetornarListaDeProdutos()
    {
        var mockRepo = new Mock<IProductRepository>();
        var products = new List<Product>
        {
            new Product("Mouse", 100m, 10),
            new Product("Teclado", 150m, 5),
            new Product("Monitor", 800m, 3)
        };
        mockRepo.Setup(repo => repo.ListAllAsync()).ReturnsAsync(products);
        var productService = new ProductService(mockRepo.Object);
        var result = await productService.ListProductsAsync();
        result.Should().BeEquivalentTo(products.Select(p => new ProductResponseDTO
        {
            Id = p.Id,
            ProductName = p.ProductName,
            ProductPrice = p.ProductPrice,
            QuantityStock = p.QuantityStock
        }));
    }

    [Fact]
    public async Task UpdateProductAsync_ProdutoExiste_DeveAtualizarProdutoComSucesso()
    {
        var existingProduct = new Product("Mouse Gamer", 100m, 10);
        var mockRepo = new Mock<IProductRepository>();
        mockRepo.Setup(repo => repo.GetByIdAsync(existingProduct.Id)).ReturnsAsync(existingProduct);
        mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);
        var dto = new ProductResponseDTO
        {
            ProductName = "Mouse Gamer",
            ProductPrice = 1200m,
            QuantityStock = 10
        };
        var productService = new ProductService(mockRepo.Object);
        var result = await productService.UpdateProductPriceAsync (existingProduct.Id, dto.ProductPrice);
        result.ProductName.Should().Be(dto.ProductName);
        result.ProductPrice.Should().Be(dto.ProductPrice);
        result.QuantityStock.Should().Be(dto.QuantityStock);
    }

    [Fact]
    public async Task UpdateProductStockAsync_ProdutoExiste_DeveAtualizarStock()
    {
        var product = new Product("Mouse Gamer", 100m, 5);
        var mockRepo = new Mock<IProductRepository>();
        mockRepo.Setup(repo => repo.GetByIdAsync(product.Id)).ReturnsAsync(product);
        mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);
        var dto1 = new ProductResponseDTO
        {
            ProductName = "Mouse Gamer",
            ProductPrice = 100m,
            QuantityStock = 10
        };
        var productServ = new ProductService(mockRepo.Object);
        var result = await productServ.UpdateProductStockAsync(product.Id, dto1.QuantityStock);
        result.ProductName.Should().Be(dto1.ProductName);
        result.ProductPrice.Should().Be(dto1.ProductPrice);
        result.QuantityStock.Should().Be(dto1.QuantityStock);
    }

    [Fact]
    public async Task DeleteProductAsync_ProdutoExiste_DeveDeletarComSucesso()
    {
        var existingProduct = new Product("Mouse Gamer", 100m, 10);
        var mockRepo = new Mock<IProductRepository>();
        mockRepo.Setup(repo => repo.GetByIdAsync(existingProduct.Id)).ReturnsAsync(existingProduct);
        mockRepo.Setup(repo => repo.DeleteAsync(existingProduct.Id)).Returns(Task.CompletedTask);
        var productService = new ProductService(mockRepo.Object);
        Func<Task> act = async () => await productService.DeleteProductAsync(existingProduct.Id);
        await act.Should().NotThrowAsync();
    }

}