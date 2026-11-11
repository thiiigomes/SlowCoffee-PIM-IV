
using Microsoft.EntityFrameworkCore;
using SlowCoffee.Application.DTOs;
using SlowCoffee.Application.Interfaces;
using SlowCoffee.Domain.Entities;
using SlowCoffee.Infrastructure.Data;

namespace SlowCoffee.Infrastructure.Services;

public class ClienteService(AppDbContext db) : IClienteService
{
    public async Task<IReadOnlyList<ClienteDto>> ListarAsync()
    {
        return await db.Clientes
            .AsNoTracking()
            .Where(x => x.Ativo)
            .OrderBy(x => x.Nome)
            .Select(x => new ClienteDto(
                x.Id,
                x.Nome,
                x.Telefone,
                x.Email,
                x.Ativo
            ))
            .ToListAsync();
    }

    public async Task<ClienteDto?> ObterAsync(int id)
    {
        return await db.Clientes
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new ClienteDto(
                x.Id,
                x.Nome,
                x.Telefone,
                x.Email,
                x.Ativo
            ))
            .FirstOrDefaultAsync();
    }

    public async Task<ClienteDto> CriarAsync(ClienteCreateDto d)
    {
        var x = new Cliente
        {
            Nome = d.Nome.Trim(),
            Telefone = d.Telefone.Trim(),
            Email = d.Email.Trim().ToLowerInvariant()
        };

        db.Add(x);
        await db.SaveChangesAsync();

        return new ClienteDto(
            x.Id,
            x.Nome,
            x.Telefone,
            x.Email,
            x.Ativo
        );
    }

    public async Task<bool> AtualizarAsync(int id, ClienteCreateDto d)
    {
        var x = await db.Clientes.FindAsync(id);

        if (x is null || !x.Ativo)
            return false;

        x.Nome = d.Nome.Trim();
        x.Telefone = d.Telefone.Trim();
        x.Email = d.Email.Trim().ToLowerInvariant();

        await db.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ExcluirAsync(int id)
    {
        var x = await db.Clientes.FindAsync(id);

        if (x is null || !x.Ativo)
            return false;

        x.Ativo = false;

        await db.SaveChangesAsync();

        return true;
    }
}
