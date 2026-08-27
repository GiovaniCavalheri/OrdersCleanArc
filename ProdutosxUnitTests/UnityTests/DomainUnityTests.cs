using Domain.Entities;
using FluentAssertions;

namespace ProdutosxUnitTests.UnityTests;

public class DomainUnityTests
{
    [Fact]
    public void Construtor_DadosValidos_CriarProdutoCorretamente()
    {
        var nome = "Pão Fresco";
        var preco = 12;
        var quant = 1;

        var product = new Product(nome, preco, quant);

        // ==> objeto.Propriedade.Should().Be(vlrEsperado);
        product.ProductName.Should().Be(nome);
        product.ProductPrice.Should().Be(preco);
        product.QuantityStock.Should().Be(quant);
        product.Id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public void Construtor_NomeVazio_DeveLancarExcecao()
    {
        var nome = "";
        var preco = 12m;
        var quant = 2;

        Action act = () => new Product(nome, preco, quant);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Construtor_ComNomeNulo_DeveLancarExcecao()
    {
        string? nome = null;
        var preco = 12m;
        var quant = 2;

        Action act = () => new Product(nome, preco, quant);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Construtor_ComEstoqueZero_DeveCriarProdutoCorretamente()
    {
        var nome = "Pão Fresco";
        var preco = 12m;
        var quant = 0;

        var product = new Product(nome, preco, quant);

        product.QuantityStock.Should().Be(quant);
    }

    [Fact]
    public void Construtor_ComPrecoNegativo_DeveLancarExcecao()
    {
        var nome = "Pao Fresco";
        var preco = -3m;
        var quant = 1;

        Action act = () => new Product(nome, preco, quant);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Construtor_ComEstoqueNegativo_DeveLancarExcecao()
    {
        var nome = "Pao Fresco";
        var preco = 3m;
        var quant = -100;

        Action act = () => new Product(nome, preco, quant);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Metodo_AlterarPreco_DeveAlterarCorretamente() 
    {
        var nome = "Pao Fresco";
        var preco = 3m;
        var quant = 10;
        decimal newPrice = 100; 

        var product = new Product(nome, preco, quant);

        product.ChangePrice(newPrice);
        product.ProductPrice.Should().Be(newPrice);
    }

    [Fact]
    public void Metodo_AlterarPrecoNegativo_DeveLancarExcecao()
    {
        var nome = "Pao Fresco";
        var preco = 3m;
        var quant = 10;
        decimal newPrice = -100;
        var product = new Product(nome, preco, quant);

        Action act = () => product.ChangePrice(newPrice);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Metodo_AtualizarEstoque_DeveAtualizarCorretamente()
    {
        var nome = "Pao Fresco";
        var preco = 3m;
        var quant = 10;
        int newQuantity = 100;

        var product = new Product(nome, preco, quant);
        product.UpdateStock(newQuantity);
        product.QuantityStock.Should().Be(newQuantity);
    }

    [Fact]
    public void Metodo_AtualizarEstoqueNegativo_DeveLancarExcessao()
    {
        {
            var nome = "Pao Fresco";
            var preco = 3m;
            var quant = 10;
            int newQuantity = -100;
            var product = new Product(nome, preco, quant);

            Action act = () => product.UpdateStock(newQuantity);
            act.Should().Throw<ArgumentException>();
        }
    }
}