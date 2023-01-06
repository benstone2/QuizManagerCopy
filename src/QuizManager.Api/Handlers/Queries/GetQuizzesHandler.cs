using MediatR;
using Microsoft.EntityFrameworkCore;
using QuizManager.Infrastructure.Data;
using QuizManager.Types.Quiz.Models;
using QuizManager.Types.Quiz.Queries;
using QuizManager.Types.Quiz.Responses;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QuizManager.Api.Handlers.Queries
{
    public class GetQuizzesHandler : IRequestHandler<GetQuizzesQuery, GetQuizzesResponse>
    {
        private readonly AppDbContext _dbContext;

        public GetQuizzesHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetQuizzesResponse> Handle(GetQuizzesQuery request, CancellationToken cancellationToken)
        {
            var quizzes = await _dbContext.Quizzes.Where(quiz => quiz.OrganisationId == request.OrganisationId).ToListAsync();

            var quizDtos = quizzes.Select(q => new QuizOverviewDto(q.Id, q.OrganisationId, q.AuthorId, q.Title, q.Description));
            
            return new GetQuizzesResponse(quizDtos);
        }
    }
}
