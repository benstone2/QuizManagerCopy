using QuizManager.Types.Quiz.Commands;
using QuizManager.Types.Quiz.Models;
using QuizManager.Types.Quiz.Responses;
using QuizManager.Web.Interfaces;
using System;
using System.Threading.Tasks;

namespace QuizManager.Web.Services
{
    public class QuizService 
    {
        private readonly HttpService _httpService;

        public QuizService(HttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<GetQuizzesResponse> GetQuizzes(Guid organisationId)
        {
            return await _httpService.HttpGetAsync<GetQuizzesResponse>($"quiz/{organisationId}");
        }

        public async Task<QuizDto> GetQuiz(Guid organisationId, Guid quizId, string role)
        {
            return await _httpService.HttpGetAsync<QuizDto>($"quiz/{organisationId}/quiz/{quizId}/{role}");
        }

        public async Task<CreateQuizResponse> CreateQuiz(CreateQuizCommand command)
        {
            return await _httpService.HttpPostAsync<CreateQuizResponse>("quiz/create", command);
        }

        public async Task<DeleteQuizResponse> DeleteQuiz(DeleteQuizCommand command)
        {
            return await _httpService.HttpPostAsync<DeleteQuizResponse>("quiz/delete", command);
        }

        public async Task<UpdateQuizResponse> UpdateQuiz(UpdateQuizCommand command)
        {
            return await _httpService.HttpPostAsync<UpdateQuizResponse>("quiz/update", command);
        }
    }
}
