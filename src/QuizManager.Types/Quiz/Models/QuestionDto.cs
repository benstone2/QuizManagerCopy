using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizManager.Types.Quiz.Models
{
    public class QuestionDto
    {
        public Guid Id { get; set; }
        public Guid QuizId { get; set; }
        public int QuestionNumber { get; set; }
        public string QuestionText { get; set; }
        public List<AnswerDto> Answers { get; set; }

        public QuestionDto()
        {

        }

        public QuestionDto(Guid id, Guid quizId, int questionNumber, string questionText)
        {
            Id = id;
            QuizId = quizId;
            QuestionNumber = questionNumber;
            QuestionText = questionText;
            Answers = new List<AnswerDto>();
        }

        public QuestionDto(Guid id, Guid quizId, int questionNumber, string questionText, List<AnswerDto> answers)
        {
            Id = id;
            QuizId = quizId;
            QuestionNumber = questionNumber;
            QuestionText = questionText;
            Answers = answers;
        }
    }
}
