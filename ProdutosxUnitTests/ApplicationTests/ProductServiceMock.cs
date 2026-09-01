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
}