using System;

namespace QuizManager.Types.Quiz.Models
{
    public class AnswerDto
    {
        public Guid Id { get; }
        public Guid QuestionId { get; }
        public int AnswerNumber { get; set; }
        public string AnswerText { get; }
        public bool IsCorrect { get; }

        public AnswerDto(Guid id, Guid questionId, int answerNumber, string answerText, bool isCorrect)
        {
            Id = id;
            QuestionId = questionId;
            AnswerNumber = answerNumber;
            AnswerText = answerText;
            IsCorrect = isCorrect;
        }
    }
}
