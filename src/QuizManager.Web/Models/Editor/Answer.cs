using QuizManager.Types.Quiz.Models;
using System;

namespace QuizManager.Web.Models.Editor
{
    public class Answer
    {
        public Guid Id { get; }
        public Guid QuestionId { get; }
        public AnswerNumber AnswerNumber { get; set; }
        public string AnswerText { get; set; }
        public bool IsCorrect { get; }
        public bool Editing = false;

        public Answer(Guid questionId, int answerNumber)
        {
            Id = Guid.NewGuid();
            QuestionId = questionId;
            AnswerNumber = new AnswerNumber(answerNumber);
            AnswerText = "";
            IsCorrect = false;
            Editing = true;
        }

        public Answer(AnswerDto dto)
        {
            Id = dto.Id;
            QuestionId = dto.QuestionId;
            AnswerNumber = new AnswerNumber(dto.AnswerNumber);
            AnswerText = dto.AnswerText;
            IsCorrect = dto.IsCorrect;
        }

        public void SetAnswerNumber(int number)
        {
            AnswerNumber = new AnswerNumber(number);
        }
    }
}
