using FluentAssertions;
using NUnit.Framework;
using QuizManager.Api.Handlers.Queries;
using QuizManager.Core.Enitites;
using QuizManager.Infrastructure.Data;
using QuizManager.Types.Quiz.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QuizManager.IntegrationTests.Handlers
{
    public class GetQuizHandlerTests : BaseHandler
    {
       
        [SetUp]
        public void Arrange()
        {
            base.BaseArrange();

            var quiz = new Quiz("Test quiz", "Test quiz description", _organisationId, _authorId);

            _quizId = quiz.Id;

            var questions = new List<Question>
            {
                new Question
                {
                    Id = _questionId,
                    QuizId = quiz.Id,
                    QuestionNumber = 1,
                    QuestionText = "Question 1?"
                }
            };

            var answers = new List<Answer>
            {
                new Answer
                {
                    Id = Guid.NewGuid(),
                    QuestionId = _questionId,
                    AnswerText = "Answer A"
                }
            };

            base.AddQuizzes(new List<Quiz> { quiz });
            base.AddQuestions(questions);
            base.AddAnswers(answers);
        }

        [TestCase("Editor")]
        [TestCase("Viewer")]
        public async Task When_RoleIsEditorOrViewer_Then_QuizIncludesAnswers(string role)
        {
            var query = new GetQuizQuery(_organisationId, _quizId, role);

            using (var context = new AppDbContext(_dbContext.Options))
            {
                var handler = new GetQuizHandler(context);

                var result = await handler.Handle(query, CancellationToken.None);

                result.Title.Should().Be("Test quiz");
                result.Description.Should().Be("Test quiz description");

                result.Questions.First().Answers.First().AnswerText.Should().Be("Answer A");
            }
        }

        [Test]
        public async Task When_RoleIsRestricted_Then_QuizDoesNotIncludeAnswers()
        {
            var query = new GetQuizQuery(_organisationId, _quizId, "Restricted");

            using (var context = new AppDbContext(_dbContext.Options))
            {
                var handler = new GetQuizHandler(context);

                var result = await handler.Handle(query, CancellationToken.None);

                result.Title.Should().Be("Test quiz");
                result.Description.Should().Be("Test quiz description");

                result.Questions.First().Answers.Should().BeEmpty();
            }
        }
    }
}
