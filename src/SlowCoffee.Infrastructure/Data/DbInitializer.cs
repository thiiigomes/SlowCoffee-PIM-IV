using Microsoft.EntityFrameworkCore;
using SlowCoffee.Domain.Entities;

namespace SlowCoffee.Infrastructure.Data;

/// <summary>Inicializa um banco de demonstração somente quando ele ainda não contém dados.</summary>
public static class DbInitializer
{
    public static async Task InitializeAsync(AppDbContext db)
    {
        await db.Database.EnsureCreatedAsync();
        if (await db.Produtos.AnyAsync() || await db.Clientes.AnyAsync()) return;

        db.Clientes.AddRange(
            new Cliente { Nome = "Mariana Costa", Telefone = "(11) 99999-1001", Email = "mariana@email.com" },
            new Cliente { Nome = "Rafael Mendes", Telefone = "(11) 99999-1002", Email = "rafael@email.com" },
            new Cliente { Nome = "Camila Oliveira", Telefone = "(11) 99999-1003", Email = "camila@email.com" }
        );
        db.Produtos.AddRange(
            new Produto { Nome = "Espresso clássico", Categoria = "Cafés", Preco = 7.50m, Estoque = 80 },
            new Produto { Nome = "Cappuccino Slow", Categoria = "Cafés", Preco = 14.90m, Estoque = 45 },
            new Produto { Nome = "Latte baunilha", Categoria = "Cafés", Preco = 16.50m, Estoque = 32 },
            new Produto { Nome = "Pão de queijo", Categoria = "Salgados", Preco = 8.00m, Estoque = 60 },
            new Produto { Nome = "Brownie artesanal", Categoria = "Doces", Preco = 12.00m, Estoque = 25 },
            new Produto { Nome = "Cold brew", Categoria = "Bebidas geladas", Preco = 13.50m, Estoque = 20 }
        );
        await db.SaveChangesAsync();
    }
}
