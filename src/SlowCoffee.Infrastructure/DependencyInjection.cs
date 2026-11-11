using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SlowCoffee.Application.Interfaces;
using SlowCoffee.Infrastructure.Data;
using SlowCoffee.Infrastructure.Services;

namespace SlowCoffee.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("A connection string 'DefaultConnection' não foi configurada.");

        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        services.AddScoped<IProdutoService, ProdutoService>();
        services.AddScoped<IClienteService, ClienteService>();
        services.AddScoped<IPedidoService, PedidoService>();
        return services;
    }
}
