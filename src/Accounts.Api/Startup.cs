using System;
using System.IO;
using Accounts.Api.DataAccess.Accounts;
using Accounts.Api.DataAccess.Transactions;
using Accounts.Api.Features.Transactions.Report;
using Accounts.Api.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Accounts.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IAccountsRepo, MockAccountsRepo>();
            services.AddTransient<ITransactionsRepo, MockTransactionsRepo>();
            services.AddSingleton<IDateTimeProxy, DateTimeProxy>();
            services.AddSingleton<GetTransactionsReport>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Accounts API", Version = "v1" });

                var xmlDocsFile = Path.Combine(AppContext.BaseDirectory, "Accounts.Api.xml");
                options.IncludeXmlComments(xmlDocsFile, true);
            });

            services.AddHealthChecks();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "AccountsAPI v1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
