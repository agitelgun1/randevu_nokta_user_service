using Microsoft.AspNetCore.Http;
namespace UserService.Models.Response
{
    public class BaseResponse<T>
    {
        public int StatusCode { get; set; } = StatusCodes.Status200OK;
        public string ErrorDescription { get; set; } = "";
        public string UserMesssage { get; set; } = "";
        public string UserMessageTitle { get; set; } = "";
        public int TotalCount { get; set; }
        public T Result { get; set; }
        public bool IsSuccess { get; set; } = false;
    }
}