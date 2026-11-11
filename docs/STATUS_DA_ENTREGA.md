# Status técnico da entrega

## Incluído no repositório

- Aplicação Web MVC e identidade visual responsiva.
- API REST com Swagger em ambiente de desenvolvimento.
- App mobile Expo/React Native consumindo endpoints da API.
- Camadas Domain, Application e Infrastructure.
- Persistência MySQL 8.0 com EF Core e dados iniciais demonstrativos.
- Cadastro/consulta/edição/inativação de produtos e clientes.
- Registro de pedidos, itens, cálculo de total, baixa de estoque e fluxo de status.
- Reposição de estoque quando um pedido é cancelado.
- Dashboard e relatórios agregados.
- Testes unitários de domínio.
- CI GitHub Actions, workflow manual de CD para Azure e Docker Compose.
- Documentação de requisitos, arquitetura, UML, dados, ADS, Scrum, cloud e roteiro acadêmico.

## Pendências que dependem do ambiente e da equipe

1. Restaurar pacotes, compilar e executar os projetos no Visual Studio/.NET 8.
2. Ajustar a credenciais do MySQL local conforme o computador.
3. Instalar dependências npm e validar o aplicativo no emulador/dispositivo.
4. Executar e registrar testes funcionais e de integração com banco real.
5. Criar recursos cloud e cadastrar segredos; o workflow de publicação está preparado, mas não há recursos cloud provisionados.
6. Implementar autenticação/autorização, auditoria e hardening de segurança antes de produção.
7. Gerar capturas reais do sistema e completar o relatório ABNT com dados, datas, participantes, sprint e evidências verdadeiras.

**Nota de verificação:** o ambiente de preparação utilizado para montar este pacote não possui o SDK .NET instalado. Portanto, não foi possível executar `dotnet build` ou `dotnet test` neste ambiente. O projeto não deve ser considerado compilado/validado até que esses comandos sejam executados com sucesso no computador do grupo ou em CI.
