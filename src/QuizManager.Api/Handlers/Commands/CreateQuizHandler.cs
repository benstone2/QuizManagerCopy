using MediatR;
using QuizManager.Core.Enitites;
using QuizManager.Infrastructure.Data;
using QuizManager.Types.Quiz.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace QuizManager.Api.Handlers.Commands
{
    public class CreateQuizHandler : IRequestHandler<CreateQuizCommand, CreateQuizResponse>
    {
        private readonly AppDbContext _dbContext;

        public CreateQuizHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CreateQuizResponse> Handle(CreateQuizCommand command, CancellationToken cancellationToken)
        {
            var quiz = new Quiz(command.Title, command.Description, command.OrganisationId, command.AuthorId);

            _dbContext.Add(quiz);

            await _dbContext.SaveChangesAsync();

            return new CreateQuizResponse
            {
                Id = quiz.Id
            };
        }
    }
}
