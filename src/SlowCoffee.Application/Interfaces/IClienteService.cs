using SlowCoffee.Application.DTOs;
namespace SlowCoffee.Application.Interfaces;
public interface IClienteService { Task<IReadOnlyList<ClienteDto>> ListarAsync(); Task<ClienteDto?> ObterAsync(int id); Task<ClienteDto> CriarAsync(ClienteCreateDto dto); Task<bool> AtualizarAsync(int id,ClienteCreateDto dto); Task<bool> ExcluirAsync(int id); }
