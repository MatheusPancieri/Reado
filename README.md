# Reado

Reado é um sistema de recomendação de livros e filmes que permite aos usuários descobrir e explorar novos conteúdos de forma personalizada. O aplicativo já possui uma base de dados pré-carregada com livros e filmes, facilitando a navegação e a recomendação com base nas preferências do usuário.

## Índice

- [Características](#características)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Instalação](#instalação)
- [Uso](#uso)
- [Contribuição](#contribuição)
- [Licença](#licença)

## Características

- **Recomendações Personalizadas**: Receba sugestões de livros e filmes com base nas suas preferências.
- **Sistema de Avaliação**: Avalie os livros e filmes que você leu ou assistiu.
- **Gêneros Múltiplos**: Cada livro e filme pode pertencer a vários gêneros, proporcionando uma experiência de descoberta mais rica.
- **Interface Intuitiva**: Navegação fácil e amigável para encontrar o que você gosta.

## Tecnologias Utilizadas

- **ASP.NET Core**: Para o desenvolvimento do backend.
- **Entity Framework**: Para gerenciamento de dados e interações com o banco de dados.
- **AutoMapper**: Para mapeamento entre entidades e DTOs.
- **C#**: Linguagem de programação principal.
- **JavaScript/React**: Para o frontend (caso você decida usar React no futuro).

## Estrutura do Projeto

```plaintext
Reado/
│
├── src/
│   ├── Reado.Domain/                # Lógica de negócios e entidades
│   │   ├── Entities/
│   │   │   ├── Movie.cs
│   │   │   ├── Book.cs
│   │   │   └── Genre.cs
│   │   ├── Interfaces/
│   │   │   ├── IBookHandler.cs
│   │   └── Enums/
│   │       └── Genre.cs
│   │
│   ├── Reado.Application/           # Regras de negócios da aplicação
│   │   ├── DTOs/
│   │   │   ├── MovieDto.cs
│   │   │   ├── BookDto.cs
│   │   ├── Services/
│   │   │   └── BookService.cs
│   │
│   ├── Reado.Infrastructure/        # Preocupações externas
│   │   ├── Data/
│   │   │   ├── ApplicationDbContext.cs
│   │   │   └── Repositories/
│   │
│   └── Reado.Web/                   # API Web e UI
│       ├── Controllers/
│       └── Program.cs
│
├── tests/
│   ├── Reado.UnitTests/
│   └── Reado.IntegrationTests/
│
└── Reado.sln
