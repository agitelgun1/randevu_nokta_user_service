using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Extensions;

namespace UserService
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureSingletonCollections();
            services.AddMediatR(typeof(Startup).Assembly);
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddFluentValidation();
            services.ConfigureJWTCollection(Configuration);
            services.ConfigureDistributedRedisCacheCollection();
            services.ConfigureSwaggerGenCollection();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCorsBuilder();
            app.UseErrorMiddleware();
            app.UseAuthentication();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUIBuilder();
        }
    }
}