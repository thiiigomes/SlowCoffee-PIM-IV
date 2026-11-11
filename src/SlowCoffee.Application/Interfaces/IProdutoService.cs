using SlowCoffee.Application.DTOs;
namespace SlowCoffee.Application.Interfaces;
public interface IProdutoService { Task<IReadOnlyList<ProdutoDto>> ListarAsync(); Task<ProdutoDto?> ObterAsync(int id); Task<ProdutoDto> CriarAsync(ProdutoCreateDto dto); Task<bool> AtualizarAsync(int id,ProdutoCreateDto dto); Task<bool> ExcluirAsync(int id); }
