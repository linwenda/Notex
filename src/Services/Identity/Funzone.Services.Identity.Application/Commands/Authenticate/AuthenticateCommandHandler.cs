using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Funzone.BuildingBlocks.Application;
using Funzone.BuildingBlocks.Application.Commands;

namespace Funzone.Services.Identity.Application.Commands.Authenticate
{
    public class AuthenticateCommandHandler : ICommandHandler<AuthenticateCommand, AuthenticationResult>
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
                               "FROM [IdentityAccess].[Users] AS [User] " +
                               "WHERE [User].[EmailAddress] = @Email";

            var user = await connection.QuerySingleOrDefaultAsync<UserDto>(sql, new {request.Email});

            if (user == null)
            {
                return new AuthenticationResult("E-mail does not exist");
            }

            if (!PasswordManager.VerifyHashedPassword(
                request.Password,
                user.PasswordHash,
                user.PasswordSalt))
            {
                return new AuthenticationResult("Incorrect password");
            }

            user.Claims = new List<Claim>
            {
                new Claim(CustomClaimTypes.Email, user.EmailAddress),
                new Claim(CustomClaimTypes.Name, user.UserName),
                new Claim(CustomClaimTypes.Roles, "Administrator")
            };

            return new AuthenticationResult(user);
        }
    }
}