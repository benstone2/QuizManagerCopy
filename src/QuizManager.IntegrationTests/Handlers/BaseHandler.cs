using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using QuizManager.Core.Enitites;
using QuizManager.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizManager.IntegrationTests.Handlers
{
    public class BaseHandler
    {
        protected Guid _organisationId;
        protected Guid _authorId;
        protected Guid _questionId;
        protected Guid _quizId;

        protected DbContextOptionsBuilder<AppDbContext> _dbContext;

        [SetUp]
        public void BaseArrange()
        {
            _organisationId = Guid.NewGuid();
            _authorId = Guid.NewGuid();
            _questionId = Guid.NewGuid();

            _dbContext = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "QuizManagerTestDatabase");
        }

        protected void AddQuizzes(List<Quiz> quizzes)
        {
            using (var context = new AppDbContext(_dbContext.Options))
            {
                foreach (var quiz in quizzes)
                {
                    context.Quizzes.Add(quiz);
                }

                context.SaveChanges();
            }
        }

        protected void AddQuestions(List<Question> questions)
        {
            using (var context = new AppDbContext(_dbContext.Options))
            {
                foreach (var question in questions)
                {
                    context.Questions.Add(question);
                }

                context.SaveChanges();
            }
        }

        protected void AddAnswers(List<Answer> answers)
        {
            using (var context = new AppDbContext(_dbContext.Options))
            {
                foreach (var answer in answers)
                {
                    context.Answers.Add(answer);
                }

                context.SaveChanges();
            }
        }
    }
}
