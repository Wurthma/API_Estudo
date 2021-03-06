using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Sotre.Infra.StoreContext.Services;
using Store.Domain.StoreContext.Handlers;
using Store.Domain.StoreContext.Repositories;
using Store.Domain.StoreContext.Services;
using Store.Infra.DataContexts;
using Store.Infra.StoreContext.Repositories;
using Elmah.Io.AspNetCore;
using System;
using System.IO;
using Store.Shared;

namespace Store.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddResponseCompression();
            
            // Injeção de dependência
            // Add scoped: verifica se já existe na memória e usa o mesmo (por transação)
            services.AddScoped<StoreDataContext, StoreDataContext>(); 
            // Add Transient: instância sempre que a implementação foi chamada
            services.AddTransient<ICustomerRepository, CustomerRepository>(); 
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<CustomerHandler, CustomerHandler>();
            // Add Singleton: uma instância única do objeto para toda aplicação (não utilizado no projeto)

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Store.Api", Version = "v1" });
            });

            var elmahApiKey = Configuration["Elmah:ElmahApiKey"];
            var elmahLogID = Configuration["Elmah:ElmahLogID"];

            services.AddElmahIo(o => {
                o.ApiKey = elmahApiKey;
                o.LogId = new Guid(elmahLogID);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            Configuration = builder.Build();

            Settings.ConnectionString = Configuration.GetValue<string>("ConnectionStrings:StoreConnectionString");
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Store.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseResponseCompression();

            app.UseElmahIo();
        }
    }
}
