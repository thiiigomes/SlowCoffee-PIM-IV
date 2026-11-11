# Modelo de dados

## Entidades e atributos

- **Cliente:** Id (PK), Nome, Telefone, Email (único), Ativo, CriadoEm.
- **Produto:** Id (PK), Nome, Categoria, Preco, Estoque, Ativo, CriadoEm.
- **Pedido:** Id (PK), ClienteId (FK), CriadoEm, Status, ValorTotal.
- **ItemPedido:** Id (PK), PedidoId (FK), ProdutoId (FK), Quantidade, PrecoUnitario.

## Relacionamentos

- Cliente 1:N Pedido.
- Pedido 1:N ItemPedido.
- Produto 1:N ItemPedido.

## DER (Mermaid)

```mermaid
erDiagram
  CLIENTE ||--o{ PEDIDO : realiza
  PEDIDO ||--|{ ITEM_PEDIDO : contem
  PRODUTO ||--o{ ITEM_PEDIDO : compoe
  CLIENTE { int Id PK string Nome string Email UK string Telefone bool Ativo }
  PEDIDO { int Id PK int ClienteId FK datetime CriadoEm int Status decimal ValorTotal }
  ITEM_PEDIDO { int Id PK int PedidoId FK int ProdutoId FK int Quantidade decimal PrecoUnitario }
  PRODUTO { int Id PK string Nome string Categoria decimal Preco int Estoque bool Ativo }
```

## Normalização e integridade

As entidades principais possuem tabelas próprias; os itens de pedido ficam em tabela associativa, evitando repetição de múltiplos produtos em colunas fixas do pedido. Chaves primárias identificam registros e chaves estrangeiras mantêm integridade referencial. Valores monetários são armazenados em `DECIMAL(18,2)`. Índices apoiam pesquisas por status, data de pedido, categoria e relacionamento dos itens.
