using QuizManager.Types.Account;
using System.Threading.Tasks;

namespace QuizManager.Web.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<AuthenticationResponse> Login(SignInUserDto userToSignIn);
        public Task<RegistrationResponse> Register(RegisterUserDto userToRegister);
        Task Logout();
    }
}
