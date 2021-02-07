# Store API

Exemplo de API com ASP.NET 5, Dapper, Cache, padrão REST, CQRS, Testes unitários, Logs com ELMAH, Swagger, etc.

## Estrutura do projeto

- **Store.Api:** aplicação (application service) com output web do projeto
- **Store.Domain:** regras de negócio da aplicação
- **Store.Infra:** tem conhecimento sobre detalhes das implementações concretas da infraestrutura tais como: acesso a bancos de dados, acesso a rede, controle de operações de IO, acesso a hardware etc.
- **Store.Shared:** compartilhado com restante do projeto.
- **Store.Tests:** projeto com testes unitários e testes de integração utilizando Xunit.

## Exemplos importantes

- No [Customer.cs](/Store.Domain/Context/Entities/Customer.cs "Customer.cs") podemos observar na prática o uso do conceito de **domínio rico**. Nessa classe algumas propriedades (Name, Document e Email) que são comumente usadas apenas com tipos primitivos, nesse caso possuem Value Objects específicos para definir as regras que envolvem essas propriedades.

## Conceitos e ferramentas utilizadas

- Princípios do SOLID
- Domain-Driven Design (DDD)
    - ![Representação básica do DDD](/.imgs/representacao_basica_ddd.PNG)
- Domínio rico
- Micro ORM Dapper
- FluentValidator

