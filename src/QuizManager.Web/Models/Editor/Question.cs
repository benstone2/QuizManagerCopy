using QuizManager.Types.Quiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizManager.Web.Models.Editor
{
    public class Question
    {
        private int MIN_ANSWERS = 3;
        private int MAX_ANSWERS = 5;

        public bool AddAnswersDisabled => Answers.Count >= MAX_ANSWERS;

        public Guid Id { get; set; }
        public int QuestionNumber { get; set; }
        public string QuestionText { get; set; }

        public List<Answer> Answers { get; set; }

        public bool Editing = false;
        public bool ContainsErrors = false;

        public Question(int questionNumber)
        {
            Id = Guid.NewGuid();
            Editing = true;
            QuestionNumber = questionNumber;
            QuestionText = "New question";
            Answers = new List<Answer>();
        }

        public Question(QuestionDto dto)
        {
            Id = dto.Id;
            QuestionNumber = dto.QuestionNumber;
            QuestionText = dto.QuestionText;
            Answers = dto.Answers.Select(a => new Answer(a)).OrderBy(a => a.AnswerNumber.Number).ToList();
        }

        public void AddStartingAnswers()
        {
            for (var i = 1; i <= MIN_ANSWERS; i++)
            {
                Answers.Add(new Answer(Id, i));
            }
        }

        public void AddAnswer()
        {
            var currentCount = Answers.Count;

            if (currentCount < MAX_ANSWERS)
            {
                Answers.Add(new Answer(Id, currentCount + 1));
            }
        }

        public void DeleteAnswer(Answer answerToDelete)
        {
            var index = answerToDelete.AnswerNumber.Number - 1;

            Answers.RemoveAt(index);

            for(var i = index; i < Answers.Count; i++)
            {
                Answers[i].SetAnswerNumber(i + 1);
            }
        }

        public void SetQuestionNumber(int number)
        {
            QuestionNumber = number;
        }

        public void ResetError()
        {
            ContainsErrors = false;
        }

        public void SetError()
        {
            ContainsErrors = true;
        }
    }
}
