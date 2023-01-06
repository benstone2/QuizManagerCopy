using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuizManager.Core.User;
using QuizManager.Shared;
using QuizManager.Types.Quiz.Commands;
using QuizManager.Types.Quiz.Queries;
using QuizManager.Types.Quiz.Responses;
using System;
using System.Threading.Tasks;

namespace QuizManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<QuizManagerUser> _userManager;

        public QuizController(IMediator mediator, UserManager<QuizManagerUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [HttpGet("{organisationId}")]
        public async Task<IActionResult> GetQuizzes(Guid organisationId)
        {
            var query = new GetQuizzesQuery { OrganisationId = organisationId };

            var quizzes = await _mediator.Send(query);

            return Ok(quizzes);
        }

        [HttpGet("{organisationId}/quiz/{quizId}/{role}")]
        public async Task<IActionResult> GetQuiz(Guid organisationId, Guid quizId, string role)
        {
            var query = new GetQuizQuery(organisationId, quizId, role);

            var quiz = await _mediator.Send(query);

            return Ok(quiz);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateQuiz(CreateQuizCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteQuiz(DeleteQuizCommand command)
        {
            var isEditor = await IsEditor(command.UserId);

            if (isEditor == false)
            {
                return Unauthorized(new DeleteQuizResponse { Success = false });
            }

            var result = await _mediator.Send(command);

            if (result.Success == false)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateQuiz(UpdateQuizCommand command)
        {
            var isEditor = await IsEditor(command.UserId);

            if (isEditor == false)
            {
                return Unauthorized(new DeleteQuizResponse { Success = false });
            }

            var result = await _mediator.Send(command);

            if (result.Success == false)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<bool> IsEditor(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            return await _userManager.IsInRoleAsync(user, UserRoles.Editor);
        }
    }
}
