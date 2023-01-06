using System;
using System.Collections.Generic;

namespace QuizManager.Types.Quiz.Models
{
    public class QuizDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<QuestionDto> Questions { get; set; }

        public QuizDto()
        {

        }
    }
}
