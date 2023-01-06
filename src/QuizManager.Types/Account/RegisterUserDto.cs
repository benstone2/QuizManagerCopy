using System;

namespace QuizManager.Types.Account
{
    public class RegisterUserDto
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public Guid OrganisationId { get; set; }
    }
}
