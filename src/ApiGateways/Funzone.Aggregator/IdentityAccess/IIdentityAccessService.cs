using System.Threading.Tasks;

namespace Funzone.Aggregator.IdentityAccess
{
    public interface IIdentityAccessService
    {
        Task RegisterUserWithEmail(RegisterUserWithEmailRequest registerUserRequest);
    }
}