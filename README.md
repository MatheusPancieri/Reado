# Reado

**Reado** é um sistema de recomendação de livros e filmes que permite aos usuários descobrir e explorar novos conteúdos de forma personalizada. O aplicativo já possui uma base de dados pré-carregada com livros e filmes, facilitando a navegação e a recomendação com base nas preferências do usuário.

## Índice

- [Características](#características)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Instalação](#instalação)
- [Uso](#uso)
- [Contribuição](#contribuição)
- [Licença](#licença)

## Características

- **Recomendações Personalizadas**: Receba sugestões de livros e filmes com base nas suas preferências pessoais.
- **Sistema de Avaliação**: Avalie os livros e filmes que você leu ou assistiu, ajudando a aprimorar as recomendações.
- **Gêneros Múltiplos**: Cada livro e filme pode pertencer a vários gêneros, proporcionando uma experiência de descoberta mais rica e diversificada.
- **Interface Intuitiva**: Navegação fácil e amigável que torna a busca pelo que você gosta uma experiência agradável.

## Tecnologias Utilizadas

- **ASP.NET Core**: Framework utilizado para o desenvolvimento do backend.
- **Entity Framework**: Ferramenta para gerenciamento de dados e interações com o banco de dados.
- **AutoMapper**: Biblioteca para mapeamento entre entidades e Data Transfer Objects (DTOs).
- **C#**: Linguagem de programação principal do projeto.
- **JavaScript/React**: Tecnologias planejadas para o frontend (caso você decida utilizar React no futuro).

## Estrutura do Projeto

```plaintext
Reado/
│
├── src/
│   ├── Reado.Domain/                # Lógica de negócios e entidades
│   │   ├── Entities/
│   │   │   ├── Movie.cs              # Classe que representa um filme
│   │   │   ├── Book.cs               # Classe que representa um livro
│   │   │   └── Genre.cs              # Classe que representa um gênero
│   │   ├── Interfaces/
│   │   │   ├── IBookHandler.cs       # Interface para manipulação de livros
│   │   └── Enums/
│   │       └── Genre.cs              # Enumeração de gêneros
│   │
│   ├── Reado.Application/            # Regras de negócios da aplicação
│   │   ├── DTOs/
│   │   │   ├── MovieDto.cs           # Data Transfer Object para filmes
│   │   │   ├── BookDto.cs            # Data Transfer Object para livros
│   │   ├── Services/
│   │   │   └── BookService.cs        # Serviço para manipulação de livros
│   │
│   ├── Reado.Infrastructure/         # Preocupações externas
│   │   ├── Data/
│   │   │   ├── ApplicationDbContext.cs # Contexto do banco de dados
│   │   │   └── Repositories/         # Repositórios para acesso aos dados
│   │
│   └── Reado.Web/                    # API Web e UI
│       ├── Controllers/              # Controladores para a API
│       └── Program.cs                # Ponto de entrada da aplicação
│
├── tests/
│   ├── Reado.UnitTests/              # Testes unitários
│   └── Reado.IntegrationTests/       # Testes de integração
│
└── Reado.sln                         # Solução do Visual Studio
