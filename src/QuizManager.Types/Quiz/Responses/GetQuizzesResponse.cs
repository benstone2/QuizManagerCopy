using QuizManager.Types.Quiz.Models;
using System.Collections.Generic;

namespace QuizManager.Types.Quiz.Responses
{
    public class GetQuizzesResponse
    {
        public IEnumerable<QuizOverviewDto> Quizzes { get; }

        public GetQuizzesResponse(IEnumerable<QuizOverviewDto> quizzes)
        {
            Quizzes = quizzes;
        }
    }
}
