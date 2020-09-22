using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using UAParser;
using UserService.Models.Entities;
using UserService.Services.ServiceInterface;

namespace UserService.Middleware
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IErrorService _errorLog;

        public ErrorMiddleware(RequestDelegate next, IErrorService errorLog)
        {
            _next = next;
            _errorLog = errorLog;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                //Device,os
                var userAgent = context.Request.Headers["User-Agent"];
                var uaString = Convert.ToString(userAgent[0]);
                var uaParser = Parser.GetDefault();
                var client = uaParser.Parse(uaString);

                //Ip
                var remoteIpAddress = context.Connection.RemoteIpAddress.MapToIPv4().ToString();
                if (context.Request.Headers.ContainsKey("X-Forwarded-For"))
                    remoteIpAddress = context.Request.Headers["X-Forwarded-For"];
                
                

                var error = new ErrorLog
                {
                    ErrorMessage = ex.Message,
                    ErrorStacktrace = ex.StackTrace,
                    ProjectName = "UserService",
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Device = client.UA.Family,
                    DeviceVersion = client.UA.Major + "." + client.UA.Minor + "." + client.UA.Patch,
                    OperatingSystem = client.OS.ToString(),
                    Ip = remoteIpAddress,
                    Path = context.Request.Path
                };

                await _errorLog.InsertErrorLog(error);
            }
        }
    }
}