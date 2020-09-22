using System.Threading.Tasks;
using Dapper;
using UserService.Helper;
using UserService.Models.Entities;
using UserService.Services.ServiceInterface;

namespace UserService.Services.ServiceBusiness
{
    public class ErrorService : IErrorService
    {
        private readonly IConnectionHelper _connectionHelper;

        public ErrorService(IConnectionHelper connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }

        public async Task<bool> InsertErrorLog(ErrorLog errorLog)
        {
            const string sql = @"INSERT INTO public.error_log
                                (error_message,error_stack_trace,project_name,create_date,update_date,device ,device_version ,ip, operating_system, path, status_code)
                                VALUES (@ErrorMessage ,@ErrorStackTrace, @ProjectName,@CreateDate,@UpdateDate,@Device ,@DeviceVersion ,@Ip, @OperatingSystem ,@Path,@StatusCode);";

            using (var dbConnection = _connectionHelper.GetOpenAppointmentConnection())
            {
                var result = await dbConnection
                    .ExecuteAsync(sql, new
                    {
                        ErrorMessage = errorLog.ErrorMessage,
                        ErrorStackTrace = errorLog.ErrorStacktrace,
                        ProjectName = errorLog.ProjectName,
                        CreateDate = errorLog.CreateDate,
                        UpdateDate = errorLog.UpdateDate,
                        StatusCode = errorLog.StatusCode,
                        Device = errorLog.Device,
                        DeviceVersion = errorLog.DeviceVersion,
                        Ip = errorLog.Ip,
                        OperatingSystem = errorLog.OperatingSystem,
                        Path = errorLog.Path
                    });

                return result > 0;
            }
        }
    }
}