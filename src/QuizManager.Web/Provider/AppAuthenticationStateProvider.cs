using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QuizManager.Web.Provider
{
    public class AppAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public AppAuthenticationStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var savedToken = await _localStorage.GetItemAsync<string>("authToken");

                if (string.IsNullOrEmpty(savedToken))
                {
                    return NotAuthenticated();
                }

                var token = _tokenHandler.ReadJwtToken(savedToken);
                var expiry = token.ValidTo;

                if (expiry < DateTime.UtcNow)
                {
                    await _localStorage.RemoveItemAsync("authToken");

                    return NotAuthenticated();
                }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearerToken", token.ToString());

                var user = new ClaimsPrincipal(new ClaimsIdentity(token.Claims, "jwt"));

                return new AuthenticationState(user);
            }
            catch
            {
                return NotAuthenticated();
            }
        }

        public void NotifyUserAuthentication(string email)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, email) }, "jwtAuthType"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyLogout() => NotifyAuthenticationStateChanged(Task.FromResult(NotAuthenticated()));

        private static AuthenticationState NotAuthenticated()
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
    }
}
