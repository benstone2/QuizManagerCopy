using MediatR;
using QuizManager.Types.Quiz.Models;
using System;

namespace QuizManager.Types.Quiz.Queries
{
    public class GetQuizQuery : IRequest<QuizDto>
    {
        public Guid OrganisationId { get; }
        public Guid QuizId { get; }
        public string Role { get; }

        public GetQuizQuery(Guid organisationId, Guid quizId, string role)
        {
            OrganisationId = organisationId;
            QuizId = quizId;
            Role = role;
        }
    }
}
