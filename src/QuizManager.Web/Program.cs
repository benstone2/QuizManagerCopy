using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using QuizManager.Web.Interfaces;
using QuizManager.Web.Provider;
using QuizManager.Web.Services;
using QuizManager.Web.Validation;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace QuizManager.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5001/") });

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, AppAuthenticationStateProvider>();
            builder.Services.AddScoped<HttpService>();
            builder.Services.AddScoped<QuizService>();

            builder.Services.AddScoped<QuestionValidator>();
            builder.Services.AddScoped<AnswerValidator>();

            await builder.Build().RunAsync();
        }
    }
}
