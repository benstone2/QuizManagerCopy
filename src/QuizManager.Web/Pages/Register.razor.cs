using Microsoft.AspNetCore.Components;
using QuizManager.Shared;
using QuizManager.Shared.DevelopmentData;
using QuizManager.Types.Account;
using QuizManager.Web.Interfaces;
using QuizManager.Web.Shared;
using System.Threading.Tasks;

namespace QuizManager.Web.Pages
{
    public partial class Register
    {
        public bool RestrictedRegistered { get; set; }
        public bool ViewOnlyRegistered { get; set; }
        public bool EditorRegistered { get; set; }

        [CascadingParameter]
        public MainLayout Layout { get; set; }

        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }

        protected override void OnInitialized()
        {
            Layout.ShowNavbar(false);
        }

        public async Task RegisterEditor()
        {
            var command = new RegisterUserDto
            {
                EmailAddress = "Editor@test.com",
                Password = "Password1!",
                Role = UserRoles.Editor,
                OrganisationId = DevData.OrganisationId
            };

            var result = await AuthenticationService.Register(command);

            if (result.Success)
            {
                EditorRegistered = true;
            }
        }

        public async Task RegisterRestricted()
        {
            var command = new RegisterUserDto
            {
                EmailAddress = "restricted@test.com",
                Password = "Password1!",
                Role = UserRoles.Restricted,
                OrganisationId = DevData.OrganisationId
            };

            var result = await AuthenticationService.Register(command);

            if (result.Success)
            {
                RestrictedRegistered = true;
            }
        }

        public async Task RegisterViewOnly()
        {
            var command = new RegisterUserDto
            {
                EmailAddress = "viewer@test.com",
                Password = "Password1!",
                Role = UserRoles.Viewer,
                OrganisationId = DevData.OrganisationId
            };

            var result = await AuthenticationService.Register(command);

            if (result.Success)
            {
                ViewOnlyRegistered = true;
            }
        }
    }
}
