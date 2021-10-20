using System.Threading.Tasks;
using NashIRL.Auth.Models;

namespace NashIRL.Auth
{
    public interface IFirebaseAuthService
    {
        Task<FirebaseUser> Login(Credentials credentials);
        Task<FirebaseUser> Register(Registration registration);
    }
}