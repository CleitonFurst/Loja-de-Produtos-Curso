# 🛒 Loja de Produtos - Curso ASP.NET MVC

![.NET Version](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=.net)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp)
![ASP.NET](https://img.shields.io/badge/ASP.NET-5C2D91?style=for-the-badge&logo=.net)

Aplicação de e-commerce desenvolvido com ASP.NET Core MVC utilizando .NET 8, focado em demonstrar as melhores práticas do framework para construção de aplicações web escaláveis e modernas.

## 📋 Sobre o Projeto

Este projeto é uma aplicação completa de loja virtual desenvolvida durante um curso de ASP.NET MVC, implementando funcionalidades essenciais para um sistema de e-commerce, incluindo gerenciamento de produtos, carrinho de compras e controle de pedidos.

## 🎯 Objetivos de Aprendizado

- Compreender a arquitetura ASP.NET Core MVC (.NET 8)
- Implementar operações CRUD completas
- Trabalhar com Entity Framework Core e Code-First Migrations
- Aplicar padrões de design como Repository Pattern
- Integrar autenticação e autorização com ASP.NET Identity
- Desenvolver interfaces responsivas com Bootstrap

## 🚀 Tecnologias Utilizadas

### Backend
- **.NET 8** - Framework principal
- **ASP.NET Core MVC** - Arquitetura Model-View-Controller
- **Entity Framework Core** - ORM para acesso a dados
- **C#** - Linguagem de programação

### Frontend
- **HTML5** - Estrutura das páginas
- **CSS3** - Estilização (83.6% do projeto)
- **JavaScript** - Interatividade do cliente
- **Bootstrap** - Framework CSS para design responsivo

## 📂 Estrutura do Projeto

```
LojaProdutosCurso/
├── Controllers/        # Controladores MVC
├── Models/            # Modelos de domínio e ViewModels
├── Views/             # Views Razor
├── DTOs/              # Data Transfer Objects
├── wwwroot/           # Arquivos estáticos (CSS, JS, imagens)
├── appsettings.json   # Configurações da aplicação
└── Program.cs         # Ponto de entrada da aplicação
```

## 🔧 Pré-requisitos

Antes de começar, certifique-se de ter instalado em sua máquina:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [Visual Studio Code](https://code.visualstudio.com/)
- [SQL Server](https://www.microsoft.com/sql-server) (LocalDB ou Express)

## 💻 Instalação e Execução

### 1. Clone o repositório

```bash
git clone https://github.com/CleitonFurst/Loja-de-Produtos-Curso.git
cd Loja-de-Produtos-Curso
```

### 2. Restaure as dependências

```bash
dotnet restore
```

### 3. Configure a string de conexão

Edite o arquivo `appsettings.json` e ajuste a string de conexão conforme seu ambiente:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=LojaProdutos;Trusted_Connection=True;"
  }
}
```

### 4. Execute as migrations

```bash
dotnet ef database update
```

### 5. Execute a aplicação

```bash
dotnet run
```

A aplicação estará disponível em `https://localhost:5001` ou `http://localhost:5000`.

## 📦 Funcionalidades

- ✅ Cadastro e gerenciamento de produtos
- ✅ Carrinho de compras com sessões
- ✅ Sistema de autenticação e autorização
- ✅ Painel administrativo
- ✅ Processamento de pedidos
- ✅ Interface responsiva

## 🏗️ Padrões e Práticas Implementadas

- **Repository Pattern** - Abstração da camada de dados
- **N-Tier Architecture** - Separação de responsabilidades
- **DTOs** - Data Transfer Objects para comunicação entre camadas
- **Code-First Migrations** - Gerenciamento de banco de dados
- **Dependency Injection** - Injeção de dependências nativa do .NET

## 📚 Conceitos Aprendidos

- Arquitetura MVC e separação de responsabilidades
- Entity Framework Core e LINQ
- Razor Pages e View Components
- Tag Helpers personalizados
- Autenticação baseada em cookies
- Bootstrap v5 para UI responsiva

## 🤝 Contribuindo

Contribuições são bem-vindas! Para contribuir:

1. Faça um Fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/MinhaFeature`)
3. Commit suas mudanças (`git commit -m 'Adiciona MinhaFeature'`)
4. Push para a branch (`git push origin feature/MinhaFeature`)
5. Abra um Pull Request

## 📝 Licença

Este projeto foi desenvolvido para fins educacionais durante um curso de ASP.NET MVC.

## 👤 Autor

**Cleiton Furst**

- GitHub: [@CleitonFurst](https://github.com/CleitonFurst)
- LinkedIn: [Cleiton Furst](https://www.linkedin.com/in/cleiton-furst/)

## 🌟 Agradecimentos

- À comunidade .NET pelo excelente suporte e documentação
- Aos instrutores do curso que tornaram este aprendizado possível

---

⭐ Se este projeto foi útil para você, considere dar uma estrela no repositório!
