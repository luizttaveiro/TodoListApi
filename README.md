# Todo API
Esta é uma API simples para gerenciar tarefas (to-do).

## Arquitetura
A aplicação segue a arquitetura DDD (Domain-Driven Design), que consiste em dividir a aplicação em camadas, separando a lógica de negócio das outras responsabilidades da aplicação. A seguir, temos uma descrição das camadas da aplicação:

- Domain: nessa camada ficam as entidades, enums e outras classes que representam o domínio da aplicação, ou seja, as regras de negócio. É uma camada independente das outras camadas.
- Application: nessa camada ficam as classes responsáveis por orquestrar a lógica de negócio, que pode ser distribuída em várias classes da camada de domínio. Aqui também é onde os DTOs (Data Transfer Objects) são definidos, para representar as informações que serão retornadas pela API.
- Presentation: nessa camada ficam as classes responsáveis por receber as requisições e enviar as respostas para o cliente. Aqui é onde os controllers da API estão localizados.

## Design Patterns
Foram utilizados os seguintes Design Patterns na implementação da API:

- Repository Pattern: utilizamos esse padrão para separar a camada de acesso a dados (repository) da camada de aplicação. Com isso, podemos testar a camada de aplicação sem precisar acessar o banco de dados.
- Builder Pattern: utilizamos esse padrão para criar objetos de uma forma mais elegante e intuitiva, sem precisar passar vários parâmetros para o construtor. Isso ajuda a manter o código mais legível e organizado.
- Mapper Pattern: utilizamos o AutoMapper para mapear objetos DTO para objetos de domínio e vice-versa. Isso ajuda a reduzir a quantidade de código necessário para fazer a conversão entre objetos.

## Libs utilizadas
As seguintes bibliotecas foram utilizadas na implementação da API:

- ASP.NET Core: utilizamos o ASP.NET Core como framework para implementar a API.
- Entity Framework Core: utilizamos o Entity Framework Core como ORM para acessar o banco de dados.
- xUnit: utilizamos o xUnit como framework de testes.
- Moq: utilizamos o Moq para criar mocks nos testes.
- AutoMapper: utilizamos o AutoMapper para mapear objetos DTO para objetos de domínio e vice-versa.

## Como rodar o projeto
Para rodar o projeto, é necessário ter o .NET 7.0 instalado. Com o .NET instalado, basta seguir os seguintes passos:

- Clonar o repositório
- Abrir o terminal na pasta do projeto
- Executar o comando dotnet restore para restaurar as dependências do projeto
- Executar o comando dotnet build para compilar o projeto
- Executar o comando dotnet ef database update para criar o banco de dados
- Executar o comando dotnet run para rodar o projeto
- O projeto estará rodando na porta 5182.

## Como rodar os testes
Para rodar os testes, basta executar o comando dotnet test no terminal, na pasta do projeto. Isso irá executar todos os testes da aplicação