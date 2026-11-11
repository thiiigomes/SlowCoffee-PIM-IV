# Requisitos do sistema

## Objetivo

Desenvolver uma plataforma corporativa integrada para apoiar a operação de uma cafeteria, reunindo administração, atendimento, catálogo, pedidos, controle de estoque e indicadores.

## Atores

- **Administrador/gerente:** administra catálogo, clientes, acompanha pedidos e indicadores.
- **Atendente:** registra pedidos e acompanha o fluxo de atendimento.
- **Cliente mobile (demonstração):** consulta catálogo, seleciona cadastro de cliente de demonstração e envia pedidos.
- **Serviço externo:** consome a API REST mediante configuração e controles de segurança a implementar antes de produção.

## Requisitos funcionais

| ID | Requisito | Critério de aceite |
|---|---|---|
| RF01 | Cadastrar produto | Produto válido é persistido e aparece no catálogo |
| RF02 | Consultar e editar produto | Dados atualizados são exibidos na listagem |
| RF03 | Inativar produto | Produto deixa de aparecer no catálogo ativo |
| RF04 | Cadastrar e consultar cliente | Dados são validados e persistidos |
| RF05 | Inativar cliente | Cliente permanece no histórico, mas fica inativo |
| RF06 | Registrar pedido | Pedido exige cliente ativo e ao menos um item válido |
| RF07 | Calcular total | Total é calculado pelo preço vigente multiplicado pela quantidade |
| RF08 | Controlar estoque | Pedido não é criado se o estoque for insuficiente |
| RF09 | Atualizar status | Pedido permite fluxo Recebido, EmPreparo, Pronto, Entregue ou Cancelado |
| RF10 | Exibir dashboard | Indicadores apresentam contagem e faturamento armazenados |
| RF11 | Exibir relatórios | Consolida produtos, clientes, pedidos, pedidos prontos e faturamento |
| RF12 | Expor API REST | Recursos principais podem ser consultados e alterados por HTTP/JSON |
| RF13 | Integrar mobile | App consulta API e registra pedido via endpoint REST |
| RF14 | Verificar saúde | Endpoint `/health` informa estado do serviço |

## Requisitos não funcionais

- RNF01 — Interface responsiva para desktop e dispositivos móveis.
- RNF02 — Separação de responsabilidades e arquitetura modular.
- RNF03 — Persistência relacional em MySQL via EF Core (Pomelo).
- RNF04 — Validação de dados no servidor e feedback ao usuário.
- RNF05 — API documentada por Swagger/OpenAPI em Development.
- RNF06 — Pipeline de integração contínua com build e testes.
- RNF07 — Contêiner para execução da API e banco em ambiente de demonstração.
- RNF08 — Preparar controles de segurança, privacidade, backup e disponibilidade antes de produção.
- RNF09 — Usabilidade, contraste, navegação semântica e rótulos acessíveis.

## Regras de negócio

- RN01 — Todo pedido pertence a um cliente existente.
- RN02 — Um pedido deve conter ao menos um item.
- RN03 — A quantidade de cada item deve ser maior que zero.
- RN04 — O estoque não pode ficar negativo.
- RN05 — O preço aplicado ao item é copiado do cadastro do produto no momento do pedido.
- RN06 — O total é a soma dos subtotais dos itens.
- RN07 — Produtos e clientes inativados não devem ser selecionados em novos pedidos.
- RN08 — Pedidos cancelados são excluídos do faturamento apresentado no dashboard.
