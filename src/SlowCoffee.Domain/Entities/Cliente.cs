using System.ComponentModel.DataAnnotations;
namespace SlowCoffee.Domain.Entities;
public class Cliente { public int Id {get;set;} [Required,MaxLength(150)] public string Nome {get;set;}=string.Empty; [Required,MaxLength(20)] public string Telefone {get;set;}=string.Empty; [Required,EmailAddress,MaxLength(180)] public string Email {get;set;}=string.Empty; public bool Ativo {get;set;}=true; public DateTime CriadoEm {get;set;}=DateTime.UtcNow; }
