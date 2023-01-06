using System;

namespace QuizManager.Types.Quiz.Models
{
    public class QuizOverviewDto
    {
        public Guid Id { get; }
        public Guid OrganisationId { get; }
        public Guid AuthorId { get; }
        public string Title { get; }
        public string Description { get; }
        public QuizOverviewDto(Guid id, Guid organisationId, Guid authorId, string title, string description)
        {
            Id = id;
            OrganisationId = organisationId;
            AuthorId = authorId;
            Title = title;
            Description = description;
        }
    }
}
