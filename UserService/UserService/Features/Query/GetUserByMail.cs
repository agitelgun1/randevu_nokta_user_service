using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using FluentValidation;
using MediatR;
using UserService.Helper;

namespace UserService.Features.Query
{
    public class GetUserByMail
    {
        public class Query : IRequest<Models.Entities.User>
        {
            public string Email { get; set; }
        }

        public class GetUserByMailValidator : AbstractValidator<Query>
        {
            public GetUserByMailValidator()
            {
                RuleFor(request => request.Email).NotNull().NotEmpty().WithMessage("Email Eksik");
            }
        }

        public class GetClinicListHandler : IRequestHandler<Query, Models.Entities.User>
        {
            private readonly IConnectionHelper _connectionHelper;

            public GetClinicListHandler(IConnectionHelper connectionHelper)
            {
                _connectionHelper = connectionHelper;
            }

            public async Task<Models.Entities.User> Handle(Query request,
                CancellationToken cancellationToken)
            {
                const string query =
                    @"SELECT * FROM public.""User"" 
                        WHERE ""IsDeleted""=FALSE 
                         AND ""IsActive""= TRUE
                         AND ""Email"" = @Email
                         ORDER BY ""Id"";";

                using (var dbConnection = _connectionHelper.GetOpenAppointmentConnection())
                {
                    dbConnection.Open();
                    var user =
                        await dbConnection.QueryAsync<Models.Entities.User>(query, new {Email = request.Email});
                    return user.FirstOrDefault();
                }
            }
        }
    }
}