using System.ComponentModel.DataAnnotations;

namespace QuizManager.Web.Models.Account
{
    public class UserSignInModel
    {
        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
