using System.Threading;
using System.Threading.Tasks;
using Dapper;
using FluentValidation;
using MediatR;
using UserService.Features.Query;
using UserService.Helper;
using UserService.Models.Entities;

namespace UserService.Features.Command
{
    public class UpdateUserPasswordByMail
    {
        public class Query : IRequest<User>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class UpdateUserPasswordByMailValidator : AbstractValidator<Query>
        {
            public UpdateUserPasswordByMailValidator()
            {
                RuleFor(request => request.Email).NotEmpty().EmailAddress().NotNull().WithMessage("Email Eksik!");
            }
        }

        public class UpdateUserPasswordByMailHandler : IRequestHandler<Query, User>
        {
            private readonly IConnectionHelper _connectionHelper;
            private readonly IMediator _mediator;

            public UpdateUserPasswordByMailHandler(IConnectionHelper connectionHelper, IMediator mediator)
            {
                _connectionHelper = connectionHelper;
                _mediator = mediator;
            }

            public async Task<User> Handle(Query request, CancellationToken cancellationToken)
            {
                const string sql = @"UPDATE ""User""
                                     SET ""Password"" = @Password
                                     WHERE ""Email"" = @Email RETURNING ""Email"";";

                using (var dbConnection = _connectionHelper.GetOpenAppointmentConnection())
                {
                    dbConnection.Open();
                    var email = await dbConnection.ExecuteScalarAsync<string>(sql, new
                    {
                        Email = request.Email ?? "",
                        Password = request.Password ?? ""
                    });

                    var response = await _mediator.Send(new GetUserByMail.Query
                    {
                        Email = email
                    }, cancellationToken);

                    return response;
                }
            }
        }
    }
}