using System.ComponentModel.DataAnnotations;
namespace SlowCoffee.Domain.Entities;
public class Produto { public int Id {get;set;} [Required,MaxLength(120)] public string Nome {get;set;}=string.Empty; [Required,MaxLength(80)] public string Categoria {get;set;}=string.Empty; [Range(0.01,100000)] public decimal Preco {get;set;} [Range(0,100000)] public int Estoque {get;set;} public bool Ativo {get;set;}=true; public DateTime CriadoEm {get;set;}=DateTime.UtcNow; }
