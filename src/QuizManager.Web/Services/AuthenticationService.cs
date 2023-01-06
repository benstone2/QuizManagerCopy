using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using QuizManager.Types.Account;
using QuizManager.Web.Interfaces;
using QuizManager.Web.Provider;
using System.Threading.Tasks;

namespace QuizManager.Web.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpService _httpService;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthenticationService(HttpService httpService, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorage)
        {
            _httpService = httpService;
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
        }

        public async Task<AuthenticationResponse> Login(SignInUserDto userToSignIn)
        {
            var authResponse = await _httpService.HttpPostAsync<AuthenticationResponse>("account/signin", userToSignIn);

            if (authResponse.IsAuthenticationSuccessful)
            {
                await _localStorage.SetItemAsync("authToken", authResponse.Token);
                
                ((AppAuthenticationStateProvider)_authStateProvider).NotifyUserAuthentication(userToSignIn.EmailAddress);
            }

            return authResponse;
        }
        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((AppAuthenticationStateProvider)_authStateProvider).NotifyLogout();
        }

        public async Task<RegistrationResponse> Register(RegisterUserDto userToRegister)
        {
            return await _httpService.HttpPostAsync<RegistrationResponse>("account/register", userToRegister);

        }
    }
}
