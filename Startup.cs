using FluentNHibernate.Cfg;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using NHibernate;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Swagger;


namespace CoreNHibernateOracleTest
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

            services.AddSingleton<NHibernate.ISessionFactory>(factory =>
              {
                  return Fluently.Configure()
                  .Database(
                      () =>
                      {
                          return FluentNHibernate.Cfg.Db.OracleClientConfiguration.Oracle10
                          .ShowSql()
                          .ConnectionString(Configuration.GetConnectionString("OracleConStr"));
                      }
                  )
                  .Mappings(m => m.FluentMappings.AddFromAssemblyOf<USER_TEST_TABLE>())
                  .BuildSessionFactory();
              }
);

            services.AddSingleton<NHibernate.ISession>(factory => factory.GetServices<ISessionFactory>().First().OpenSession());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });



            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Configure Metodu
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NHibernateTest"));
        }


    }
}
