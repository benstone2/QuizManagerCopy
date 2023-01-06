using MediatR;
using QuizManager.Types.Quiz.Models;
using QuizManager.Types.Quiz.Responses;
using System;

namespace QuizManager.Types.Quiz.Commands
{
    public class UpdateQuizCommand : IRequest<UpdateQuizResponse>
    {
        public Guid UserId { get; set; }
        public QuizDto QuizDto { get; set; }
    }
}
