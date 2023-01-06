using QuizManager.Types.Quiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizManager.Web.Models.Editor
{
    public class Quiz
    {
        public Guid Id { get; }
        public string Title { get; }
        public string Description { get; }

        public List<Question> Questions { get; }

        public Quiz(QuizDto dto)
        {
            Id = dto.Id;
            Title = dto.Title;
            Description = dto.Description;
            Questions = dto.Questions.Select(q => new Question(q)).OrderBy(q => q.QuestionNumber).ToList();
        }

        public void AddQuestion()
        {
            var currentQuestionCount = Questions.Count;

            var question = new Question(currentQuestionCount + 1);

            question.AddStartingAnswers();

            Questions.Add(question);
        }

        public void DeleteQuestion(Question questionToDelete)
        {
            var index = questionToDelete.QuestionNumber - 1;

            Questions.RemoveAt(index);

            for (var i = index; i < Questions.Count; i++)
            {
                Questions[i].SetQuestionNumber(i + 1);
            }
        }
    }
}
