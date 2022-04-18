using Autofac;
using Autofac.Extensions.DependencyInjection;
using FormsCreator.App.Middlewares;
using FormsCreator.Domain;
using FormsCreator.Domain.Db;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace FormsCreator.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        /// <summary>
        /// Kontener IOC
        /// </summary>
        public ILifetimeScope AutofacContainer { get; set; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpContextAccessor();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "FormsCreator.App", Version = "v1"});
            });
            
            services.AddDbContext<Context>();
            services.AddAuth();
            
            services.AddScoped<CookieAuthenticationBaseEventHandler>();

            ConfigureKestrel(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FormsCreator.App v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();
            var origins = Configuration.GetSection("corsOrigins").Get<string[]>();
            app.UseCors(builder => builder.WithOrigins(origins)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
        
        /// <summary>
        /// Metoda konfigurjaca kontener IOC
        /// </summary>
        /// <param name="builder">Builder kontenera IOC</param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<DomainModule>();
        }

        private static void ConfigureKestrel(IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(options => { options.Limits.MaxRequestBodySize = long.MaxValue; });

            services.Configure<FormOptions>(options =>
            {
                options.ValueLengthLimit = int.MaxValue;
                options.MultipartBodyLengthLimit = long.MaxValue;
                options.MultipartHeadersLengthLimit = int.MaxValue;
            });
        }
    }
}