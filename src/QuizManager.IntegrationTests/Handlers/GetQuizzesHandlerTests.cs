using FluentAssertions;
using Microsoft.EntityFrameworkCore;
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
    public class GetQuizzesHandlerTests : BaseHandler
    {
        [Test]
        public async Task When_RequestingAnOrganisationsQuizzes_Then_OnlyTheirQuizzesAreReturned()
        {
            base.BaseArrange();

            var organisationId = Guid.NewGuid();
            var authorId = Guid.NewGuid();

            var quiz = new Quiz("Test quiz", "Test quiz description", organisationId, authorId);
            var quizByAnotherOrganisation = new Quiz("Someone else's quiz", "Description", Guid.NewGuid(), Guid.NewGuid());

            base.AddQuizzes(new List<Quiz> { quiz, quizByAnotherOrganisation });

            var query = new GetQuizzesQuery
            {
                OrganisationId = organisationId
            };

            using (var context = new AppDbContext(_dbContext.Options))
            {
                var handler = new GetQuizzesHandler(context);

                var result = await handler.Handle(query, CancellationToken.None);

                result.Quizzes.Count().Should().Be(1);
                result.Quizzes.First().Title.Should().Be("Test quiz");
            }
        }
    }
}