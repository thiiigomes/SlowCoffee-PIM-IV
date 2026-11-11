using SlowCoffee.Domain.Entities;
using Xunit;

namespace SlowCoffee.Tests;

public class DomainTests
{
    [Fact]
    public void SubtotalDoItemDePedidoDeveMultiplicarQuantidadePorPrecoUnitario()
    {
        var item = new ItemPedido { Quantidade = 3, PrecoUnitario = 8.50m };
        Assert.Equal(25.50m, item.Subtotal);
    }

    [Fact]
    public void PedidoNovoDeveIniciarComoRecebido()
    {
        var pedido = new Pedido();
        Assert.Equal(StatusPedido.Recebido, pedido.Status);
        Assert.Empty(pedido.Itens);
    }

    [Fact]
    public void ProdutoNovoDeveIniciarAtivo()
    {
        var produto = new Produto { Nome = "Espresso", Categoria = "Cafés", Preco = 7.5m, Estoque = 10 };
        Assert.True(produto.Ativo);
    }
}
