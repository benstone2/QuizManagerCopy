using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using QuizManager.Shared;
using QuizManager.Types.Quiz.Commands;
using QuizManager.Types.Quiz.Models;
using QuizManager.Web.Interfaces;
using QuizManager.Web.Services;
using QuizManager.Web.Shared;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QuizManager.Web.Pages
{
    public partial class NewQuiz
    {
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public QuizService QuizService { get; set; }

        [CascadingParameter]
        public MainLayout Layout { get; set; }

        public NewQuizDto NewQuizDto { get; set; } = new NewQuizDto();

        protected override async Task OnInitializedAsync()
        {
            Layout.ShowNavbar(true);

            var auth = await AuthenticationStateProvider.GetAuthenticationStateAsync();

            if (auth.User.Identity.IsAuthenticated == true)
            {
                var role = GetUserRole(auth);

                if (role != UserRoles.Editor)
                {
                    NavigationManager.NavigateTo("/");
                }
                else
                {
                    var orgId = auth.User.Claims.Where(c => c.Type == ClaimTypes.Country).Select(c => c.Value).SingleOrDefault();
                    var userId = auth.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();

                    NewQuizDto.AuthorId = Guid.Parse(userId);
                    NewQuizDto.OrganisationId = Guid.Parse(orgId);
                }
            }
            else
            {
                NavigationManager.NavigateTo("/login");
            }
        }

        public async Task SendCreateNewQuiz()
        {
            var command = new CreateQuizCommand
            {
                AuthorId = NewQuizDto.AuthorId,
                OrganisationId = NewQuizDto.OrganisationId,
                Title = NewQuizDto.Title,
                Description = NewQuizDto.Description
            };

            var response = await QuizService.CreateQuiz(command);

            if (response != null)
            {
                NavigationManager.NavigateTo($"/edit/{response.Id}");
            }
        }

        private string GetUserRole(AuthenticationState auth)
        {
            if (auth.User.IsInRole("Editor"))
            {
                return "Editor";
            }
            else if (auth.User.IsInRole("Viewer"))
            {
                return "Viewer";
            }

            return "Restricted";
        }
    }
}
