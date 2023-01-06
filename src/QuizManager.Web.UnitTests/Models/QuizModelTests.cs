using AutoFixture;
using FluentAssertions;
using NUnit.Framework;
using QuizManager.Types.Quiz.Models;
using QuizManager.Web.Models.Editor;

namespace QuizManager.Web.UnitTests.Models
{
    public class QuizModelTests
    {
        private Fixture _autoFixture;

        private QuizDto _quizDto;

        [SetUp]
        public void Arrange()
        {
            _autoFixture = new Fixture();

            _quizDto = _autoFixture.Create<QuizDto>();
        }

        [Test]
        public void When_CreatingQuiz_Then_DataIsMapped()
        {
            var quiz = new Quiz(_quizDto);

            quiz.Id.Should().Be(_quizDto.Id); 
            quiz.Title.Should().Be(_quizDto.Title);
            quiz.Description.Should().Be(_quizDto.Description);
        }

        [Test]
        public void When_AddingAQuestion_Then_NewQuestionIsAddedAtTheEndOfTheQuestionList()
        {
            var quiz = new Quiz(_quizDto);

            var startingQuestionCount = quiz.Questions.Count;

            quiz.AddQuestion();

            quiz.Questions.Count.Should().Be(startingQuestionCount + 1);
        }
    }
}
