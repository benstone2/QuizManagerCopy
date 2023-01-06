using MediatR;
using System;

namespace QuizManager.Types.Quiz.Commands
{
    public class CreateQuizCommand : IRequest<CreateQuizResponse>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid OrganisationId { get; set; }
        public Guid AuthorId { get; set; }
    }
}
