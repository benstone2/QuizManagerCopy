using AutoFixture;
using FluentAssertions;
using NUnit.Framework;
using QuizManager.Types.Quiz.Models;
using QuizManager.Web.Models.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizManager.Web.UnitTests.Models
{
    public class QuestionModelTests
    {
        private Fixture _autoFixture;

        private Question _question;

        [SetUp]
        public void Arrange()
        {
            _autoFixture = new Fixture();

            _question = _autoFixture.Create<Question>();
        }

        [Test]
        public void When_DeletingAnAnswer_Then_AnswerNumbersAreReIndexed()
        {
            _question.Answers[0].AnswerNumber = new AnswerNumber(1);
            _question.Answers[1].AnswerNumber = new AnswerNumber(2);
            _question.Answers[2].AnswerNumber = new AnswerNumber(3);

            var answerToDelete = _question.Answers[1];
            var expectedAnswer = _question.Answers[2];

            _question.DeleteAnswer(answerToDelete);

            _question.Answers.Count.Should().Be(2);
            _question.Answers[1].Should().BeEquivalentTo(expectedAnswer, options => options.Excluding(o => o.AnswerNumber));
            _question.Answers[1].AnswerNumber.Number.Should().Be(2);
            _question.Answers[1].AnswerNumber.Letter.Should().Be("B");
        }
    }
}
