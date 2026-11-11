# Arquitetura da solução

## Visão lógica

```mermaid
flowchart TB
  M[Aplicativo Mobile<br/>Expo / React Native] -->|HTTPS / JSON| A[API REST<br/>ASP.NET Core]
  W[Portal Administrativo<br/>ASP.NET Core MVC] --> APP[Camada Application]
  A --> APP
  APP --> D[Camada Domain<br/>Entidades e regras]
  APP --> I[Camada Infrastructure<br/>Serviços e persistência]
  I --> DB[(MySQL 8.0<br/>Entity Framework Core)]
  CI[GitHub Actions] -->|Restore / Build / Test| W
  CI -->|Restore / Build / Test| A
  DO[Docker / Cloud target] -. implantação .-> A
```

## Responsabilidades

- **Domain:** entidades `Produto`, `Cliente`, `Pedido`, `ItemPedido` e enumeração `StatusPedido`.
- **Application:** DTOs e interfaces `IProdutoService`, `IClienteService`, `IPedidoService` que definem os contratos da aplicação.
- **Infrastructure:** `AppDbContext`, mapeamento relacional, serviços concretos e integração MySQL via EF Core (Pomelo).
- **Web:** interface administrativa MVC; os controllers recebem serviços por injeção de dependência.
- **API:** recursos REST consumidos pelo mobile e por possíveis sistemas externos.
- **Mobile:** cliente de API; não acessa o banco diretamente.

## Fluxo de criação de pedido

```mermaid
sequenceDiagram
  actor U as Usuário mobile
  participant M as Aplicativo
  participant A as API REST
  participant S as PedidoService
  participant DB as MySQL 8.0
  U->>M: Seleciona cliente, produto e quantidade
  M->>A: POST /api/pedidos
  A->>S: CriarAsync(dto)
  S->>DB: Consulta cliente e produtos ativos
  S->>S: Valida quantidade e estoque
  S->>DB: Registra pedido, itens e baixa estoque
  DB-->>S: Confirma persistência
  S-->>A: PedidoDto
  A-->>M: 201 Created
  M-->>U: Confirmação do pedido
```

## Decisões técnicas

1. API e Web compartilham contratos de aplicação e infraestrutura, evitando duplicação de regras de negócio.
2. O mobile consome exclusivamente a API REST.
3. Exclusão lógica (`Ativo = false`) preserva histórico de produtos e clientes.
4. O pedido armazena preço unitário praticado no momento da venda, mantendo histórico mesmo após reajustes no catálogo.
5. Relacionamentos usam chaves estrangeiras; preços usam `DECIMAL(18,2)`.
6. A baixa de estoque ocorre no serviço de pedidos, após validação de disponibilidade. Para produção, deve ser protegida por transação/concorrência otimista para evitar vendas simultâneas acima do estoque.
