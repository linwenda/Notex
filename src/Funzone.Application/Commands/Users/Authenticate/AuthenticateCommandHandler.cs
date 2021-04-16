using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Funzone.Application.Configuration;
using Funzone.Application.Configuration.Data;
using Funzone.Application.Contract;
using MediatR;

namespace Funzone.Application.Commands.Users.Authenticate
{
    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, AuthenticationResult>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public AuthenticateCommandHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<AuthenticationResult> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT " +
                               "[User].[Id], " +
                               "[User].[UserName], " +
                               "[User].[EmailAddress], " +
                               "[User].[PasswordSalt], " +
                               "[User].[PasswordHash] " +
                               "FROM [Users] AS [User] " +
                               "WHERE [User].[EmailAddress] = @Email";

            var user = await connection.QuerySingleOrDefaultAsync<UserDto>(sql, new {request.Email});

            if (user == null)
            {
                return new AuthenticationResult("Incorrect email or password");
            }

            if (!PasswordManager.VerifyHashedPassword(
                request.Password,
                user.PasswordHash,
                user.PasswordSalt))
            {
                return new AuthenticationResult("Incorrect email or password");
            }

            user.Claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.EmailAddress),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            return new AuthenticationResult(user);
        }
    }
}