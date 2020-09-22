using System.Collections;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Features.Command;
using UserService.Features.Query;
using UserService.Models.Entities;
using UserService.Models.Request;
using UserService.Models.Response;

namespace UserService.Controllers
{
    [Route("api/user")]
    [ApiController]
    //[Authorize(Roles = Role.Admin)]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [AllowAnonymous]
        [HttpGet("getuserbymail")]
        public async Task<BaseResponse<User>> GetUserByMail(string email)
        {
            var response = new BaseResponse<User>
            {
                IsSuccess = false
            };

            var user = await _mediator.Send(new GetUserByMail.Query {Email = email});

            if (user != null)
            {
                response.IsSuccess = true;
                response.Result = user;
            }

            return response;
        }
        [AllowAnonymous]
        [HttpPost("updateuserpasswordbymail")]
        public async Task<BaseResponse<User>> UpdateUserPasswordByMail(UpdateUserPasswordRequest request)
        {
            var response = new BaseResponse<User>()
            {
                IsSuccess = false
            };

            var user = await _mediator.Send(new UpdateUserPasswordByMail.Query
            {
                Email = request.Email,
                Password = request.Password
            });

            if (user == null) return response;
            response.IsSuccess = true;
            response.Result = user;

            return response;
        }

    }
}