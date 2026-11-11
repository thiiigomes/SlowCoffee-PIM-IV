# ☕ Slow Coffee — Plataforma de Gestão de Cafeteria

**Projeto acadêmico — PIM IV | Análise e Desenvolvimento de Sistemas**

O Slow Coffee é uma solução de software desenvolvida para apoiar as atividades administrativas e operacionais de uma cafeteria. O projeto evolui a aplicação iniciada no PIM III, incorporando uma arquitetura organizada em camadas, API REST, estrutura mobile, persistência relacional, testes automatizados e infraestrutura conteinerizada.

## 1. Sobre o projeto

A plataforma centraliza operações essenciais de uma cafeteria, oferecendo recursos para gerenciamento de produtos, clientes, pedidos, estoque e indicadores administrativos.

O projeto foi desenvolvido utilizando tecnologias do ecossistema .NET e ferramentas de desenvolvimento de software, aplicando conceitos de orientação a objetos, arquitetura em camadas, modelagem de dados e integração entre sistemas.

## 2. Funcionalidades

- **Dashboard:** apresentação de indicadores e informações administrativas.
- **Produtos:** cadastro, consulta, atualização e controle de estoque.
- **Clientes:** gerenciamento de clientes cadastrados.
- **Pedidos:** registro, acompanhamento e atualização de status.
- **Estoque:** movimentação automática conforme as operações de pedidos.
- **Relatórios:** visualização de informações para acompanhamento da cafeteria.
- **API REST:** disponibilização de recursos para integração com outras interfaces.
- **Mobile:** estrutura de aplicativo desenvolvida com Expo e React Native.

## 3. Tecnologias utilizadas

| Tecnologia | Aplicação |
|---|---|
| C# | Linguagem principal |
| .NET 8 | Plataforma de desenvolvimento |
| ASP.NET Core MVC | Aplicação web |
| ASP.NET Core Web API | Serviços REST |
| Entity Framework Core | Persistência e acesso a dados |
| MySQL 8.0 | Banco de dados relacional |
| Pomelo | Integração EF Core com MySQL |
| HTML e CSS | Interface web |
| Razor | Construção das páginas |
| Expo / React Native | Estrutura mobile |
| xUnit | Testes automatizados |
| Docker | Conteinerização |
| GitHub Actions | Integração contínua |

## 4. Arquitetura do projeto

A solução utiliza separação de responsabilidades em camadas, facilitando a organização, manutenção e evolução do software.

- **Domain:** entidades e regras centrais do negócio.
- **Application:** DTOs, contratos e serviços da aplicação.
- **Infrastructure:** acesso ao banco de dados, Entity Framework Core e implementações.
- **Web:** interface administrativa ASP.NET Core MVC.
- **API:** serviços REST e integração com outras aplicações.

## 5. Estrutura de diretórios

```text
SlowCoffee_PIM_IV/
├── .github/
│   └── workflows/
├── database/
│   └── schema.sql
├── docs/
├── mobile/
│   └── SlowCoffee.Mobile/
├── src/
│   ├── SlowCoffee.Domain/
│   ├── SlowCoffee.Application/
│   ├── SlowCoffee.Infrastructure/
│   ├── SlowCoffee.Web/
│   └── SlowCoffee.Api/
├── tests/
│   └── SlowCoffee.Tests/
├── .env.example
├── .gitignore
├── docker-compose.yml
├── README.md
└── SlowCoffee.PIMIV.sln
```

## 6. Requisitos para execução

- Windows 10/11, Linux ou macOS.
- .NET 8 SDK.
- MySQL Server 8.0, para execução local.
- Visual Studio Code ou Visual Studio 2022.
- Node.js e npm, para a estrutura mobile.
- Docker Desktop, opcional para execução conteinerizada.

## 7. Executando a aplicação Web

1. Clone ou baixe o repositório.
2. Configure a conexão com o MySQL no arquivo `src/SlowCoffee.Web/appsettings.Development.json`.
3. Utilize a porta 3306 e o banco `slowcoffee_db` na configuração local.
4. Abra o terminal na raiz do projeto e execute:

```bash
dotnet restore SlowCoffee.PIMIV.sln
dotnet run --project src/SlowCoffee.Web/SlowCoffee.Web.csproj
```

5. Acesse o endereço local apresentado no terminal.

No ambiente de desenvolvimento, o inicializador do sistema prepara a estrutura do banco e os dados demonstrativos quando necessário.

**Importante:** o arquivo `appsettings.Development.json` é local e não deve ser enviado ao GitHub. Utilize as configurações de desenvolvimento com suas próprias credenciais.

## 8. Executando a API

Para iniciar a API REST:

```bash
dotnet run --project src/SlowCoffee.Api/SlowCoffee.Api.csproj
```

No ambiente de desenvolvimento, a documentação Swagger pode ser acessada pelo caminho `/swagger`.

O endpoint `/health` disponibiliza uma verificação básica de funcionamento do serviço.

### Principais endpoints

| Método | Endpoint | Finalidade |
|---|---|---|
| GET | `/api/produtos` | Listar produtos |
| GET | `/api/produtos/{id}` | Consultar produto |
| POST | `/api/produtos` | Cadastrar produto |
| PUT | `/api/produtos/{id}` | Atualizar produto |
| DELETE | `/api/produtos/{id}` | Inativar produto |
| GET | `/api/clientes` | Listar clientes |
| POST | `/api/clientes` | Cadastrar cliente |
| GET | `/api/pedidos` | Listar pedidos |
| POST | `/api/pedidos` | Criar pedido |
| PATCH | `/api/pedidos/{id}/status` | Atualizar status |
| GET | `/health` | Verificar funcionamento da API |

## 9. Executando com Docker

O projeto possui configuração Docker Compose para execução da API e do MySQL 8.0 em contêineres.

1. Copie `.env.example` para `.env`.
2. Defina uma senha forte para o MySQL no arquivo `.env`.
3. Na raiz do projeto, execute:

```bash
docker compose up --build
```

A API ficará disponível em:

`http://localhost:8080`

A documentação Swagger estará disponível em `/swagger`.

Para encerrar os contêineres:

```bash
docker compose down
```

Para remover também o volume persistente do banco:

```bash
docker compose down -v
```

**Atenção:** o comando anterior remove os dados armazenados no volume do banco de dados.

## 10. Aplicativo mobile

A estrutura mobile está localizada em `mobile/SlowCoffee.Mobile` e utiliza Expo e React Native.

Para instalar as dependências:

```bash
cd mobile/SlowCoffee.Mobile
npm install
npm start
```

A configuração do endereço da API deve considerar o ambiente de execução, como emulador Android ou dispositivo físico conectado à mesma rede do computador.

## 11. Testes automatizados

O projeto possui uma estrutura de testes automatizados utilizando xUnit.

Para executar os testes:

```bash
dotnet test SlowCoffee.PIMIV.sln
```

Para compilar a solução em modo Release:

```bash
dotnet build SlowCoffee.PIMIV.sln --configuration Release
```

Na validação acadêmica realizada, os três testes automatizados disponíveis foram aprovados, sem falhas ou testes ignorados.

## 12. Segurança e boas práticas

As credenciais de acesso ao banco de dados devem permanecer em arquivos locais ou variáveis de ambiente, nunca em arquivos públicos do repositório.

O arquivo `.env.example` disponibiliza apenas um modelo de configuração. O arquivo `.env` contém configurações locais e deve permanecer fora do controle de versão.

Esta entrega representa uma base acadêmica funcional. Para utilização em ambiente de produção, recomenda-se implementar autenticação e autorização, proteção de segredos, políticas CORS restritas, monitoramento, backups testados, migrations e validações adicionais de segurança.

## 13. Documentação complementar

A pasta `docs/` reúne materiais técnicos relacionados ao projeto, incluindo requisitos, arquitetura, modelo de dados, planejamento e documentação de desenvolvimento.

## 14. Evolução do PIM III para o PIM IV

O Slow Coffee preserva o contexto de gerenciamento de cafeteria apresentado no PIM III e amplia sua estrutura com novas tecnologias e componentes.

A evolução contempla organização em camadas, API REST, estrutura mobile, testes automatizados e infraestrutura conteinerizada, ampliando as possibilidades de integração e manutenção da solução.

---

**Slow Coffee | PIM IV**

Projeto acadêmico desenvolvido no curso de Análise e Desenvolvimento de Sistemas.