using MediatR;
using Microsoft.EntityFrameworkCore;
using QuizManager.Core.Enitites;
using QuizManager.Infrastructure.Data;
using QuizManager.Shared;
using QuizManager.Types.Quiz.Models;
using QuizManager.Types.Quiz.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QuizManager.Api.Handlers.Queries
{
    public class GetQuizHandler : IRequestHandler<GetQuizQuery, QuizDto>
    {
        private readonly AppDbContext _dbContext;

        public GetQuizHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<QuizDto> Handle(GetQuizQuery request, CancellationToken cancellationToken)
        {
            var quiz = await _dbContext.Quizzes.Where(q => q.OrganisationId == request.OrganisationId && q.Id == request.QuizId)
                                .Include(q => q.Questions)
                                .FirstOrDefaultAsync();

            var questions = quiz.Questions.Select(q => new QuestionDto(q.Id, q.QuizId, q.QuestionNumber, q.QuestionText)).ToList();

            if (request.Role != UserRoles.Restricted)
            {
                foreach (var question in questions.OrderBy(q => q.QuestionNumber))
                {
                    var answers = await _dbContext.Answers.Where(a => a.QuestionId == question.Id).OrderBy(a => a.AnswerNumber).ToListAsync();

                    question.Answers.AddRange(answers.Select(a => new AnswerDto(a.Id, a.QuestionId, a.AnswerNumber, a.AnswerText, a.IsCorrect)).ToList());
                }
            }
            
            return new QuizDto
            {
                Id = quiz.Id,
                Title = quiz.Title,
                Description = quiz.Description,
                Questions = questions
            };
        }
    }
}
