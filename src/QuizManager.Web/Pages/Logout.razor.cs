using Microsoft.AspNetCore.Components;
using QuizManager.Web.Interfaces;
using System.Threading.Tasks;

namespace QuizManager.Web.Pages
{
    public partial class Logout
    {
        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await AuthenticationService.Logout();
            NavigationManager.NavigateTo("/login");
        }
    }
}
