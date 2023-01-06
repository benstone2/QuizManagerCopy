using Microsoft.AspNetCore.Identity;
using System;

namespace QuizManager.Core.User
{
    public class QuizManagerUser : IdentityUser
    {
        public Guid OrganisationId { get; set; }
    }
}
