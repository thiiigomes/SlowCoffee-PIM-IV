using System.ComponentModel.DataAnnotations;
namespace SlowCoffee.Application.DTOs;
public record ProdutoDto(int Id,string Nome,string Categoria,decimal Preco,int Estoque,bool Ativo);
public record ProdutoCreateDto([Required, StringLength(120, MinimumLength=2)] string Nome,[Required, StringLength(80)] string Categoria,[Range(0.01,100000)] decimal Preco,[Range(0,100000)] int Estoque);
