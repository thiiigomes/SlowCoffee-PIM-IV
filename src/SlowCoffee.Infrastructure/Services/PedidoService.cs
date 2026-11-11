
using System.Data;
using Microsoft.EntityFrameworkCore;
using SlowCoffee.Application.DTOs;
using SlowCoffee.Application.Interfaces;
using SlowCoffee.Domain.Entities;
using SlowCoffee.Infrastructure.Data;

namespace SlowCoffee.Infrastructure.Services;

public class PedidoService(AppDbContext db) : IPedidoService
{
    public async Task<IReadOnlyList<PedidoDto>> ListarAsync()
    {
        var pedidos = await db.Pedidos
            .AsNoTracking()
            .Include(p => p.Cliente)
            .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
            .OrderByDescending(p => p.CriadoEm)
            .ToListAsync();

        return pedidos.Select(Mapear).ToList();
    }

    public async Task<PedidoDto?> ObterAsync(int id)
    {
        var pedido = await db.Pedidos
            .AsNoTracking()
            .Include(p => p.Cliente)
            .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
            .FirstOrDefaultAsync(p => p.Id == id);

        return pedido is null ? null : Mapear(pedido);
    }

    private static PedidoDto Mapear(Pedido pedido) =>
        new(
            pedido.Id,
            pedido.ClienteId,
            pedido.Cliente!.Nome,
            pedido.Status.ToString(),
            pedido.ValorTotal,
            pedido.CriadoEm,
            pedido.Itens.Select(i => new PedidoItemDto(
                i.ProdutoId,
                i.Produto!.Nome,
                i.Quantidade,
                i.PrecoUnitario,
                i.Quantidade * i.PrecoUnitario
            )).ToList()
        );

    public async Task<PedidoDto> CriarAsync(PedidoCreateDto dto)
    {
        if (dto.Itens is null || dto.Itens.Count == 0)
            throw new ArgumentException("O pedido precisa ter ao menos um item.");

        if (dto.Itens.Any(i => i.Quantidade < 1))
            throw new ArgumentException("A quantidade dos itens deve ser maior que zero.");

        await using var transaction = await db.Database.BeginTransactionAsync(
            IsolationLevel.Serializable
        );

        var cliente = await db.Clientes.FirstOrDefaultAsync(
            c => c.Id == dto.ClienteId && c.Ativo
        ) ?? throw new KeyNotFoundException("Cliente não encontrado ou inativo.");

        var solicitados = dto.Itens
            .GroupBy(i => i.ProdutoId)
            .Select(g => new
            {
                ProdutoId = g.Key,
                Quantidade = g.Sum(i => i.Quantidade)
            })
            .ToList();

        var ids = solicitados.Select(i => i.ProdutoId).ToList();

        var produtos = await db.Produtos
            .Where(p => ids.Contains(p.Id) && p.Ativo)
            .ToDictionaryAsync(p => p.Id);

        var pedido = new Pedido { ClienteId = cliente.Id };

        foreach (var item in solicitados)
        {
            if (!produtos.TryGetValue(item.ProdutoId, out var produto))
                throw new KeyNotFoundException(
                    $"Produto {item.ProdutoId} não encontrado ou inativo."
                );

            if (produto.Estoque < item.Quantidade)
                throw new InvalidOperationException(
                    $"Estoque insuficiente para {produto.Nome}. Disponível: {produto.Estoque}."
                );

            produto.Estoque -= item.Quantidade;

            pedido.Itens.Add(new ItemPedido
            {
                ProdutoId = produto.Id,
                Quantidade = item.Quantidade,
                PrecoUnitario = produto.Preco
            });

            pedido.ValorTotal += produto.Preco * item.Quantidade;
        }

        db.Pedidos.Add(pedido);

        await db.SaveChangesAsync();
        await transaction.CommitAsync();

        return (await ObterAsync(pedido.Id))!;
    }

    public async Task<bool> AtualizarStatusAsync(int id, string status)
    {
        if (!Enum.TryParse<StatusPedido>(status, true, out var novoStatus))
            return false;

        await using var transaction = await db.Database.BeginTransactionAsync(
            IsolationLevel.Serializable
        );

        var pedido = await db.Pedidos
            .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pedido is null ||
            pedido.Status is StatusPedido.Cancelado or StatusPedido.Entregue)
            return false;

        if (pedido.Status == novoStatus)
            return true;

        if (novoStatus == StatusPedido.Cancelado)
        {
            foreach (var item in pedido.Itens)
            {
                if (item.Produto is not null)
                    item.Produto.Estoque += item.Quantidade;
            }
        }

        pedido.Status = novoStatus;

        await db.SaveChangesAsync();
        await transaction.CommitAsync();

        return true;
    }
}
