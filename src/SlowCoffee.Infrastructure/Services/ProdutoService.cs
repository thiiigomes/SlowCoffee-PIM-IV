using Microsoft.EntityFrameworkCore; using SlowCoffee.Application.DTOs; using SlowCoffee.Application.Interfaces; using SlowCoffee.Domain.Entities; using SlowCoffee.Infrastructure.Data;
namespace SlowCoffee.Infrastructure.Services;
public class ProdutoService(AppDbContext db):IProdutoService {
 public async Task<IReadOnlyList<ProdutoDto>> ListarAsync()=>await db.Produtos.AsNoTracking().OrderBy(x=>x.Nome).Select(x=>new ProdutoDto(x.Id,x.Nome,x.Categoria,x.Preco,x.Estoque,x.Ativo)).ToListAsync();
 public async Task<ProdutoDto?> ObterAsync(int id)=>await db.Produtos.AsNoTracking().Where(x=>x.Id==id).Select(x=>new ProdutoDto(x.Id,x.Nome,x.Categoria,x.Preco,x.Estoque,x.Ativo)).FirstOrDefaultAsync();
 public async Task<ProdutoDto> CriarAsync(ProdutoCreateDto d){var x=new Produto{Nome=d.Nome.Trim(),Categoria=d.Categoria.Trim(),Preco=d.Preco,Estoque=d.Estoque};db.Add(x);await db.SaveChangesAsync();return new(x.Id,x.Nome,x.Categoria,x.Preco,x.Estoque,x.Ativo);}
 public async Task<bool> AtualizarAsync(int id,ProdutoCreateDto d){var x=await db.Produtos.FindAsync(id);if(x is null)return false;x.Nome=d.Nome.Trim();x.Categoria=d.Categoria.Trim();x.Preco=d.Preco;x.Estoque=d.Estoque;await db.SaveChangesAsync();return true;}
 public async Task<bool> ExcluirAsync(int id){var x=await db.Produtos.FindAsync(id);if(x is null)return false;x.Ativo=false;await db.SaveChangesAsync();return true;}
}
