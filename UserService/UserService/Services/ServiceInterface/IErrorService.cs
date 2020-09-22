using System.Threading.Tasks;
using UserService.Models.Entities;

namespace UserService.Services.ServiceInterface
{
    public interface IErrorService
    {
        Task<bool> InsertErrorLog(ErrorLog errorLog);
    }
}