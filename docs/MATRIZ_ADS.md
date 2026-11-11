# Matriz de integração com as áreas do curso de ADS

A matriz relaciona conhecimentos acadêmicos às evidências presentes no sistema. A associação não significa que toda disciplina foi implementada integralmente; cada item indica a contribuição prática demonstrável no escopo atual.

| Área de conhecimento | Aplicação no projeto | Evidência |
|---|---|---|
| Lógica de programação e algoritmos | Cálculo de total, validação de quantidade e fluxo de pedidos | `PedidoService` |
| Programação Orientada a Objetos | Entidades, encapsulamento por serviços e contratos | `Domain`, interfaces e serviços |
| Estrutura de dados | Coleções de itens e consultas ordenadas | `List<ItemPedido>`, LINQ |
| Engenharia de Software | Requisitos, critérios de aceite, arquitetura e testes | `REQUISITOS.md`, testes |
| Análise e Projeto de Sistemas | Atores, regras, diagramas e sequência de operações | `ARQUITETURA.md`, `MODELO_DE_DADOS.md` |
| Banco de Dados | Modelagem relacional, integridade, índices e persistência | `AppDbContext`, `schema.sql` |
| Desenvolvimento Web | MVC, Razor, HTML, CSS e Bootstrap | `SlowCoffee.Web` |
| Programação C# / .NET | Controllers, serviços, DTOs, LINQ e async/await | `src/` |
| Redes e Sistemas Distribuídos | Cliente-servidor e comunicação HTTP/JSON | API REST consumida pelo mobile |
| Desenvolvimento Mobile | Interface React Native e consumo da API | `mobile/SlowCoffee.Mobile` |
| Segurança da Informação | Validação, antiforgery no MVC e recomendações de hardening | Controllers, requisitos de segurança |
| Testes de Software | Testes unitários de regras de domínio | `tests/SlowCoffee.Tests` |
| UX/UI e acessibilidade | Hierarquia visual, responsividade, rótulos e navegação | Views, CSS e componentes mobile |
| Inteligência de Dados | Indicadores de produtos, clientes, pedidos e faturamento | Dashboard e relatórios |
| Computação em Nuvem | Aplicação conteinerizada e configuração por ambiente | Dockerfile e Compose |
| DevOps | Automação de restore, build e testes | `.github/workflows/ci.yml` |
| Gestão Ágil | Backlog, planejamento de sprints, review e retrospectiva | `PLANO_AGIL.md` |
| Responsabilidade social e inclusão | Diretrizes de interface clara e recomendações de acessibilidade/LGPD | Requisitos não funcionais e checklist |

## Melhorias futuras para ampliar a cobertura

- Autenticação completa com ASP.NET Core Identity/JWT e perfis de acesso.
- Auditoria de alterações e trilha de logs.
- Testes de integração com banco isolado e testes end-to-end.
- Relatórios com séries temporais e exportação CSV/PDF.
- Indicadores preditivos de demanda com conjunto de dados validado; não apresentar previsões sem dados históricos suficientes.
- Deploy real em provedor cloud com domínio, TLS, secrets e banco gerenciado.
- Acessibilidade validada com teclado, leitor de tela e WCAG 2.2 AA.
