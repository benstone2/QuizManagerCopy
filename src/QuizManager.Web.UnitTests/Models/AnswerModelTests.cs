using AutoFixture;
using FluentAssertions;
using NUnit.Framework;
using QuizManager.Web.Models.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizManager.Web.UnitTests.Models
{
    public class AnswerModelTests
    {
        private Fixture _autoFixture;

        private Answer _answer;

        [SetUp]
        public void Arrange()
        {
            _autoFixture = new Fixture();

            _answer = _autoFixture.Create<Answer>();
        }

        [Test]
        public void When_SettingAnAnswerNumber_Then_NumberIsSet()
        {
            _answer.SetAnswerNumber(2);

            _answer.AnswerNumber.Number.Should().Be(2);
            _answer.AnswerNumber.Letter.Should().Be("B");
        }
    }
}
