using MediatR;
using QuizManager.Types.Quiz.Responses;
using System;

namespace QuizManager.Types.Quiz.Commands
{
    public class DeleteQuizCommand : IRequest<DeleteQuizResponse>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
