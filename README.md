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

- [Dependency Injection](/Store.Api/Startup.cs "Dependency Injection") (AddScoped, AddTransient e AddSingleton)
	```csharp
    // Add scoped: verifica se já existe na memória e usa o mesmo (por transação)
    services.AddScoped<StoreDataContext, StoreDataContext>(); 
    // Add Transient: instância sempre que a implementação foi chamada
    services.AddTransient<ICustomerRepository, CustomerRepository>(); 
    services.AddTransient<IEmailService, EmailService>();
    // Add Singleton: uma instância única do objeto para toda aplicação (não utilizado no projeto)
    ```

- Exemplos de uso de [cache](/Store.Api/Controllers/CustomerController.cs "cache"):
    ```csharp
    [HttpGet]
    [Route("v1/customers")]
    [ResponseCache(Duration = 15)] // Exemplo de cache de 15 minutos
    // [ResponseCache(Location = ResponseCacheLocation.Client ,Duration = 15)] // Exemplo de cache de 15 minutos armazenado ná máquina do cliente
    public IEnumerable<ListCustomerQueryResult> Get() => _repository.Get();
    ```

## Conceitos e ferramentas utilizadas

- Princípios do SOLID
- Domain-Driven Design (DDD)
    - ![Representação básica do DDD](/.imgs/representacao_basica_ddd.PNG)
- Domínio rico
- Micro ORM Dapper
- FluentValidator
- Cache
- Compression (Microsoft.AspNetCore.ResponseCompression)

