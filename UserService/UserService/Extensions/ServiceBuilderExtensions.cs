using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using UserService.Helper;
using UserService.Services.ServiceBusiness;
using UserService.Services.ServiceInterface;

namespace UserService.Extensions
{
    public static class ServiceBuilderExtensions
    {
        public static void ConfigureSingletonCollections(this IServiceCollection services)
        {
            services.AddSingleton<IConnectionHelper, ConnectionHelper>();
            services.AddSingleton<IErrorService, ErrorService>();
        }
        
        public static void ConfigureJWTCollection(this IServiceCollection services,IConfiguration configuration)
        {
            var issuerSigningKey = configuration["JWT:IssuerSigningKey"];

            var key = Encoding.ASCII.GetBytes(issuerSigningKey);

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }
        
        public static void ConfigureDistributedRedisCacheCollection(this IServiceCollection services)
        {
            services.AddDistributedRedisCache(option =>
            {

                option.Configuration = "127.0.0.1:6379 ";
                option.InstanceName = "master";
            });
        }
        
        public static void ConfigureSwaggerGenCollection(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info {Title = "User Service Api", Version = "v1"});
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "Example: \"Bearer {token}\"",
                    Name = "authorization",
                    In = "header",
                    Type = "apiKey"
                });

                c.AddSecurityRequirement(security);
            });
        }
    }
}