# Store API

Exemplo de API com ASP.NET 5, Dapper, Cache, padrão REST, CQRS, Testes unitários, Logs com ELMAH, Swagger, etc.

## Estrutura do projeto

- **Store.Api:** aplicação (application service) com output web do projeto
- **Store.Domain:** regras de negócio da aplicação
- **Store.Infra:** tem conhecimento sobre detalhes das implementações concretas da infraestrutura tais como: acesso a bancos de dados, acesso a rede, controle de operações de IO, acesso a hardware etc.
- **Store.Shared:** compartilhado com restante do projeto.
- **Store.Tests:** projeto com testes unitários e testes de integração utilizando Xunit.

## Exemplos importantes

