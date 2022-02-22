using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WoundHealingDb;
using WoundHealingWebApi.Configs;
using WoundHealingWebApi.Handlers;

namespace WoundHealingWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppConfig>(Configuration.GetSection("AppConfig"));

            services.AddDbContext<WoundHealingContext>(options => GetDbCtxOptions(options));

            services.AddMediatR(typeof(UserRequestHandler).Assembly);

            services.AddHealthChecks();
            services.AddControllers();
            services.AddHttpClient();

            services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder =>
                {
                    builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowAnyOrigin();
                }));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WoundHealingWebApi", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if(env.IsDevelopment() || env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WoundHealingWebApi v1"));
            }

            app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseCors("CorsPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapControllers();
            });
        }

        private DbContextOptionsBuilder GetDbCtxOptions(DbContextOptionsBuilder options)
        {
            return options.UseSqlServer(GetContextConnectionString());
        }

        private DbContextOptionsBuilder<WoundHealingContext> GetDbCtxOptions(DbContextOptionsBuilder<WoundHealingContext> options)
        {
            return options.UseSqlServer(GetContextConnectionString());
        }

        private string GetContextConnectionString()
        {
            return Configuration.GetConnectionString("WoundHealingDb");
        }
    }
}