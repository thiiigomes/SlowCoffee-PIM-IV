using System.ComponentModel.DataAnnotations;
namespace SlowCoffee.Application.DTOs;
public record ClienteDto(int Id,string Nome,string Telefone,string Email,bool Ativo);
public record ClienteCreateDto([Required, StringLength(150, MinimumLength=2)] string Nome,[Required, StringLength(20)] string Telefone,[Required, EmailAddress, StringLength(180)] string Email);
