using QuizManager.SharedKernel;
using System;
using System.Collections.Generic;

namespace QuizManager.Core.Enitites
{
    public class Quiz : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid OrganisationId { get; set; }
        public Guid AuthorId { get; set; }

        public Quiz(string title, string description, Guid organisationId, Guid authorId)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            OrganisationId = organisationId;
            AuthorId = authorId;
            Questions = new List<Question>();
        }

        public virtual IEnumerable<Question> Questions { get; set; }

    }
}
