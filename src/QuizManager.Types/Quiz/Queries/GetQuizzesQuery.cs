using MediatR;
using QuizManager.Types.Quiz.Responses;
using System;

namespace QuizManager.Types.Quiz.Queries
{
    public class GetQuizzesQuery : IRequest<GetQuizzesResponse>
    {
        public Guid OrganisationId { get; set; }
    }
}
