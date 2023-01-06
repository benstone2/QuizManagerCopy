using QuizManager.SharedKernel;
using System;

namespace QuizManager.Core.Enitites
{
    public class Answer : BaseEntity
    {
        public Guid QuestionId { get; set; }
        public int AnswerNumber { get; set; }
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }
    }
}
