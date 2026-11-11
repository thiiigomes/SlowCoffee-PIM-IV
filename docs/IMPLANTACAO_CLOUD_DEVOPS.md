# Cloud e DevOps

## Ambiente conteinerizado local

O `docker-compose.yml` descreve MySQL 8.0 e API ASP.NET Core em serviços separados. O volume nomeado mantém os dados entre reinicializações do contêiner. A senha é fornecida por variável de ambiente e não deve ser versionada.

## Caminho de implantação em nuvem

Arquitetura de referência para Azure:

1. Azure App Service para a aplicação Web e/ou API;
2. Azure SQL Database para persistência;
3. Azure Key Vault para connection strings e segredos;
4. Application Insights para telemetria e diagnóstico;
5. HTTPS/TLS, regras de rede e CORS restrito;
6. backups e política de recuperação;
7. GitHub Actions para build, testes e publicação.

A arquitetura é uma referência de implantação. A publicação real exige assinatura Azure, criação dos recursos, configuração de variáveis/segredos e execução do workflow manual `Deploy Slow Coffee Web to Azure`. Nenhuma conta ou recurso cloud é criado automaticamente por este repositório.

## CI

O workflow `ci.yml` executa restore, build Release e testes unitários em push e pull request.

## CD (modelo)

O workflow `deploy-azure.yml` é manual e publica a aplicação Web em Azure App Service. Configure no repositório GitHub:

- `AZURE_WEBAPP_NAME`: nome do App Service;
- `AZURE_WEBAPP_PUBLISH_PROFILE`: conteúdo do perfil de publicação obtido no portal Azure.

A connection string deve ser cadastrada na configuração do App Service, não no repositório. Para publicar a API separadamente, crie outro App Service e ajuste o workflow para o projeto `SlowCoffee.Api`.

## Segurança de implantação

- Não publicar `.env`, senha SA ou connection strings.
- Trocar as credenciais padrão antes de iniciar containers.
- Usar secrets/Key Vault e identidade gerenciada sempre que possível.
- Desabilitar Swagger em produção ou proteger seu acesso.
- Restringir CORS ao domínio do aplicativo.
- Implantar migrations controladas antes de liberar a versão nova.
- Configurar monitoramento, logs, alertas, backup e recuperação.
