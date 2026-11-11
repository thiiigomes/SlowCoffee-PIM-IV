using Microsoft.EntityFrameworkCore;
using SlowCoffee.Domain.Entities;
namespace SlowCoffee.Infrastructure.Data;
public class AppDbContext(DbContextOptions<AppDbContext> options):DbContext(options) {
 public DbSet<Produto> Produtos=>Set<Produto>(); public DbSet<Cliente> Clientes=>Set<Cliente>(); public DbSet<Pedido> Pedidos=>Set<Pedido>(); public DbSet<ItemPedido> ItensPedido=>Set<ItemPedido>();
 protected override void OnModelCreating(ModelBuilder b){
  b.Entity<Cliente>().HasIndex(x=>x.Email).IsUnique();
  b.Entity<Produto>().Property(x=>x.Preco).HasPrecision(18,2); b.Entity<Pedido>().Property(x=>x.ValorTotal).HasPrecision(18,2); b.Entity<ItemPedido>().Property(x=>x.PrecoUnitario).HasPrecision(18,2);
  b.Entity<Pedido>().HasOne(x=>x.Cliente).WithMany().HasForeignKey(x=>x.ClienteId).OnDelete(DeleteBehavior.Restrict);
  b.Entity<ItemPedido>().HasOne(x=>x.Pedido).WithMany(x=>x.Itens).HasForeignKey(x=>x.PedidoId).OnDelete(DeleteBehavior.Cascade);
  b.Entity<ItemPedido>().HasOne(x=>x.Produto).WithMany().HasForeignKey(x=>x.ProdutoId).OnDelete(DeleteBehavior.Restrict);
 }
}
