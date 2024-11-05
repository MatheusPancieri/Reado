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
├── Reado.Api/                            # API principal com extensões, mapeamentos, endpoints e handlers
│   ├── Common/
│   │   ├── Api/
│   │   │   ├── AppExtension.cs
│   │   │   ├── BuilderExtension.cs
│   │   │   └── IEndpoint.cs
│   │
│   ├── Data/
│   │   ├── Mappings/
│   │   │   ├── Identity/
│   │   │   │   └── (Todos os arquivos de mapeamento do Identity)
│   │   │   ├── BookMapping.cs
│   │   │   ├── RecommendationMapping.cs
│   │   │   └── UserPreferences.cs
│   │   └── AppDbContext.cs
│   │
│   ├── Endpoints/
│   │   ├── Books/
│   │   ├── Identity/
│   │   ├── Movies/
│   │   ├── Recommendation/
│   │   ├── UserPreference/
│   │   └── Endpoints.cs
│   │
│   ├── Handlers/
│   │   ├── BookHandler.cs
│   │   ├── MovieHandler.cs
│   │   ├── RecommendationHandler.cs
│   │   └── UserPreferenceHandler.cs
│   │
│   ├── Migrations/
│   │   └── (Arquivos de migração)
│   │
│   ├── Models/
│   │   └── User.cs
│   │
│   ├── ApiConfiguration.cs
│   └── Program.cs
│
├── Reado.Domain/                         # Lógica de domínio, entidades, enumeradores e handlers
│   ├── Entities/
│   │   ├── Account/
│   │   │   ├── RoleClaim.cs
│   │   │   └── User.cs
│   │   ├── Books.cs
│   │   ├── Movie.cs
│   │   ├── Recommendation.cs
│   │   └── UserPreference.cs
│   │
│   ├── Enums/
│   │   ├── ContentTypes.cs
│   │   └── Genre.cs
│   │
│   ├── Handlers/
│   │   ├── IAccountHandler.cs
│   │   ├── IBookHandler.cs
│   │   ├── IMovieHandler.cs
│   │   ├── IRecommendationHandler.cs
│   │   └── IUserPreferenceHandler.cs
│   │
│   ├── Response/
│   │   ├── PageResponse.cs
│   │   └── Response.cs
│   │
│   └── Request/
│       └── (Padronização do Request)
│
├── Reado.Web/ *Ainda Em Produção*          # Aplicação Web, controladores e configuração da interface
│   ├── Controllers/
│   │   └── (Controladores da interface)
│   │
│   └── (Demais arquivos específicos da interface web)
│
└── Reado.sln                              # Arquivo de solução do projeto

