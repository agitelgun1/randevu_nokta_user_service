using Microsoft.AspNetCore.Builder;
using UserService.Middleware;

namespace UserService.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseErrorMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ErrorMiddleware>();
        }

        public static void UseSwaggerUIBuilder(this IApplicationBuilder builder)
        {
            builder.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Appointment System API"); });
        }

        public static void UseCorsBuilder(this IApplicationBuilder builder)
        {
            builder.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        }
    }
}