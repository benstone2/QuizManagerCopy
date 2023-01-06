using System;

namespace QuizManager.Types.Quiz.Models
{
    public class NewQuizDto
    {
        public Guid OrganisationId { get; set; }
        public Guid AuthorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
