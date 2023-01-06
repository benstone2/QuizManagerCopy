using Microsoft.AspNetCore.Components;
using QuizManager.Types.Account;
using QuizManager.Web.Interfaces;
using QuizManager.Web.Models.Account;
using QuizManager.Web.Shared;
using System.Threading.Tasks;

namespace QuizManager.Web.Pages
{
    public partial class Login
    {
        private UserSignInModel UserSignInModel = new UserSignInModel();

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }
        
        [CascadingParameter]
        public MainLayout Layout { get; set; } 

        protected override void OnInitialized()
        {
            Layout.ShowNavbar(false);            
        }

        public async Task ExecuteLogin()
        {
            var signInDto = new SignInUserDto
            {
                EmailAddress = UserSignInModel.EmailAddress,
                Password = UserSignInModel.Password
            };

            var response = await AuthenticationService.Login(signInDto);

            if (response != null)
            {
                NavigationManager.NavigateTo("/");
            }
        }
    }
}
