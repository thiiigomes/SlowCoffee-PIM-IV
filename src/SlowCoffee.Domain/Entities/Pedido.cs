namespace SlowCoffee.Domain.Entities;
public enum StatusPedido { Recebido=1, EmPreparo=2, Pronto=3, Entregue=4, Cancelado=5 }
public class Pedido { public int Id {get;set;} public int ClienteId {get;set;} public Cliente? Cliente {get;set;} public DateTime CriadoEm {get;set;}=DateTime.UtcNow; public StatusPedido Status {get;set;}=StatusPedido.Recebido; public decimal ValorTotal {get;set;} public List<ItemPedido> Itens {get;set;}=new(); }
