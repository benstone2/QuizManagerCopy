using MediatR;
using QuizManager.Infrastructure.Data;
using QuizManager.Types.Quiz.Commands;
using QuizManager.Types.Quiz.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QuizManager.Api.Handlers.Commands
{
    public class DeleteQuizHandler : IRequestHandler<DeleteQuizCommand, DeleteQuizResponse>
    {
        private readonly AppDbContext _dbContext;

        public DeleteQuizHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DeleteQuizResponse> Handle(DeleteQuizCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var quizToDelete = _dbContext.Quizzes.Single(q => q.Id == request.Id);
                _dbContext.Quizzes.Remove(quizToDelete);
                await _dbContext.SaveChangesAsync();

                return new DeleteQuizResponse { Success = true };
            }
            catch
            {
                return new DeleteQuizResponse { Success = false };
            }
        }
    }
}
