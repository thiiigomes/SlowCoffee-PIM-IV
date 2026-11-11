using SlowCoffee.Application.DTOs;
namespace SlowCoffee.Application.Interfaces;
public interface IPedidoService { Task<IReadOnlyList<PedidoDto>> ListarAsync(); Task<PedidoDto?> ObterAsync(int id); Task<PedidoDto> CriarAsync(PedidoCreateDto dto); Task<bool> AtualizarStatusAsync(int id,string status); }
