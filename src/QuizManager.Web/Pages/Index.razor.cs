using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using QuizManager.Types.Quiz.Commands;
using QuizManager.Types.Quiz.Models;
using QuizManager.Web.Interfaces;
using QuizManager.Web.Services;
using QuizManager.Web.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizManager.Web.Pages
{
    public partial class Index
    {
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
       
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public QuizService QuizService { get; set; }

        [CascadingParameter]
        public MainLayout Layout { get; set; }

        public List<QuizOverviewDto> Quizzes { get; set; }

        private bool quizzesLoaded = false;

        protected override async Task OnInitializedAsync()
        {
            Layout.ShowNavbar(true);

            var auth = await AuthenticationStateProvider.GetAuthenticationStateAsync();

            if (auth.User.Identity.IsAuthenticated == true)
            {
                Layout.SetUserData(auth);

                // Imaginary API call to get organisation data 

                // Pass organisation data back to the layout to show theme on each page
                Layout.SetOrganisationData("BCS", "/logos/bcs-logo.png", "British Computer Society");

                var response = await QuizService.GetQuizzes(Layout.OrganisationId);

                Quizzes = response.Quizzes.ToList();
                quizzesLoaded = true;
            }
            else
            {
                NavigationManager.NavigateTo("/login");
            }

        }

        public async Task DeleteQuiz(Guid quizId)
        {
            var command = new DeleteQuizCommand
            {
                Id = quizId,
                UserId = Layout.UserId
            };

            var result = await QuizService.DeleteQuiz(command);

            if (result.Success == true)
            {
                RemoveQuizFromList(quizId);
            }
        }

        private void RemoveQuizFromList(Guid quizId)
        {
            var quizToRemove = Quizzes.FirstOrDefault(q => q.Id == quizId);

            Quizzes.Remove(quizToRemove);
        }
    }
}
