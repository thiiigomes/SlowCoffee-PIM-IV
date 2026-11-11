using System.ComponentModel.DataAnnotations;
namespace SlowCoffee.Application.DTOs;
public record ItemPedidoCreateDto([Range(1,int.MaxValue)] int ProdutoId,[Range(1,1000)] int Quantidade);
public record PedidoCreateDto([Range(1,int.MaxValue)] int ClienteId,[Required, MinLength(1)] List<ItemPedidoCreateDto> Itens);
public record PedidoDto(int Id,int ClienteId,string Cliente, string Status,decimal ValorTotal,DateTime CriadoEm,List<PedidoItemDto> Itens);
public record PedidoItemDto(int ProdutoId,string Produto,int Quantidade,decimal PrecoUnitario,decimal Subtotal);
