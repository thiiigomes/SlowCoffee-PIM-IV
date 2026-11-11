# ☕ Slow Coffee — Plataforma de Gestão de Cafeteria

<p align="center">
  **Sistema de gerenciamento administrativo e operacional para cafeterias**
</p>

<p align="center">
  Projeto acadêmico desenvolvido no curso de Análise e Desenvolvimento de Sistemas — PIM IV.
</p>

---

## 📌 Sobre o projeto

O **Slow Coffee** é uma solução de software desenvolvida para apoiar a gestão de cafeterias, centralizando informações e processos administrativos e operacionais em uma única plataforma.

A aplicação foi projetada para facilitar o gerenciamento de produtos, clientes, pedidos e estoque, além de disponibilizar indicadores que auxiliam no acompanhamento das atividades do negócio.

O projeto foi desenvolvido com foco em organização, modularidade, separação de responsabilidades e integração entre diferentes interfaces, aplicando conceitos e práticas da Engenharia de Software.

## 🎯 Objetivos

- Centralizar informações administrativas e operacionais.
- Facilitar o gerenciamento de produtos, clientes e pedidos.
- Apoiar o controle de estoque.
- Disponibilizar indicadores para acompanhamento do negócio.
- Aplicar conceitos de arquitetura em camadas e desenvolvimento orientado a objetos.
- Desenvolver uma estrutura preparada para integração entre Web, API e Mobile.

## 🚀 Funcionalidades

| Módulo | Descrição |
|---|---|
| Dashboard | Visão geral e indicadores administrativos. |
| Produtos | Gerenciamento do catálogo de produtos. |
| Clientes | Organização e gerenciamento de clientes. |
| Pedidos | Estrutura para gerenciamento de pedidos. |
| Estoque | Controle de informações relacionadas ao estoque. |
| API REST | Interface para comunicação e integração com outros sistemas. |
| Mobile | Estrutura Expo/React Native preparada para integração. |

## 🛠️ Tecnologias utilizadas

**Backend**
- C#
- .NET 8
- ASP.NET Core MVC
- ASP.NET Core Web API
- Entity Framework Core 8

**Banco de dados**
- MySQL 8.0
- Pomelo.EntityFrameworkCore.MySql

**Frontend**
- HTML
- CSS
- JavaScript
- Razor Views

**Mobile**
- React Native
- Expo

**Qualidade e ferramentas**
- xUnit
- Git e GitHub
- Docker e Docker Compose
- Visual Studio / Visual Studio Code

## 🏗️ Arquitetura do projeto

O sistema utiliza uma organização em camadas, separando as responsabilidades para facilitar a manutenção, os testes e a evolução da aplicação.

```text
SlowCoffee/
│
├── src/
│   ├── SlowCoffee.Domain/
│   │
│   ├── SlowCoffee.Application/
│   │
│   ├── SlowCoffee.Infrastructure/
│   │
│   ├── SlowCoffee.Web/
│   │
│   ├── SlowCoffee.Api/
│   │
│   └── SlowCoffee.Mobile/
│
├── tests/
│
├── docs/
│
├── docker-compose.yml
├── .env.example
├── .gitignore
└── README.md
```

**Responsabilidades das camadas:**

- **Domain:** concentra as entidades e regras centrais do negócio.
- **Application:** organiza os casos de uso e a lógica de aplicação.
- **Infrastructure:** concentra implementações de persistência e integração.
- **Web:** disponibiliza a interface web utilizando ASP.NET Core MVC.
- **Api:** disponibiliza os serviços de comunicação por meio de uma API REST.
- **Mobile:** contém a estrutura da aplicação móvel com Expo e React Native.
- **Tests:** reúne os testes automatizados do projeto.

## ⚙️ Como executar o projeto

### Pré-requisitos

- .NET 8 SDK
- MySQL 8.0
- Visual Studio ou Visual Studio Code
- Git

### 1. Clonar o repositório

```bash
git clone https://github.com/thiiigomes/SlowCoffee-PIM-IV.git
```

### 2. Acessar a pasta

```bash
cd SlowCoffee-PIM-IV
```

### 3. Configurar o banco de dados

Configure as credenciais de conexão com o MySQL conforme as orientações da documentação.

Para utilizar o Docker Compose, crie seu arquivo `.env` com base no modelo disponibilizado:

```bash
cp .env.example .env
```

Defina uma senha forte para o banco de dados no arquivo `.env`.

### 4. Executar a aplicação

Abra a solução no Visual Studio ou utilize o terminal para executar o projeto Web:

```bash
dotnet run --project src/SlowCoffee.Web
```

A configuração do banco de dados e os demais detalhes de inicialização estão descritos na documentação técnica.

## 🧪 Testes

O projeto possui uma estrutura de testes automatizados utilizando **xUnit**, permitindo validar componentes e comportamentos da aplicação.

Para executar os testes:

```bash
dotnet test
```

## 📚 Documentação

A documentação complementar apresenta informações sobre requisitos, configuração do ambiente, execução dos projetos, banco de dados, API, estrutura mobile e testes.

➡️ **[Acessar documentação técnica](docs/README.md)**

## 🔐 Segurança

- Arquivos de configuração locais não devem ser publicados no repositório.
- Credenciais e senhas devem ser definidas por variáveis de ambiente.
- O arquivo `.env.example` apresenta o modelo de configuração sem expor credenciais reais.

## 👨‍💻 Autor

**Thiago Gomes Magalhães**
**Gabriel Carnovalle de Araújo**
**Gabriel Souza Farias**
**Jorge Conrado Kerssner Neto**
**Lucas Henrique Ranolphi Pires**

Estudante de Análise e Desenvolvimento de Sistemas, com interesse em desenvolvimento de software, arquitetura de aplicações, APIs e automação de processos.




---

<p align="center">
  Desenvolvido como parte do Projeto Integrado Multidisciplinar — PIM IV.
</p>

<p align="center">
  ☕ **Slow Coffee — Tecnologia a serviço de uma gestão mais organizada.**
</p>