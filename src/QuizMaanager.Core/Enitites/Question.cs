using QuizManager.SharedKernel;
using System;
using System.Collections.Generic;

namespace QuizManager.Core.Enitites
{
    public class Question : BaseEntity
    {
        public Guid QuizId { get; set; }
        public int QuestionNumber { get; set; }
        public string QuestionText { get; set; }


        public virtual IEnumerable<Answer> Answers { get; set; }
    }
}
