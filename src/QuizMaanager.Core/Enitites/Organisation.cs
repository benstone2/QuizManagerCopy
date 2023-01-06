using QuizManager.SharedKernel;
using System;
using System.Collections.Generic;

namespace QuizManager.Core.Enitites
{
    public class Organisation : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<Quiz> Quizzes { get; set; }
    }
}
