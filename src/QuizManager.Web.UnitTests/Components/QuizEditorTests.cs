//using Bunit;
//using Microsoft.Extensions.DependencyInjection;
//using Moq;
//using NUnit.Framework;
//using QuizManager.Types.Quiz.Models;
//using QuizManager.Web.Interfaces;
//using QuizManager.Web.Pages;
//using QuizManager.Web.Shared;
//using QuizManager.Web.Validation;
//using System;
//using System.Collections.Generic;

//namespace QuizManager.Web.UnitTests.Components
//{
//    public class QuizEditorTests
//    {

//        public void Arrange()
//        {
            
//        }

//        [Test]
//        public void When_AddingQuestionToQuiz_Then_IncrementQuestions()
//        {
//            using var ctx = new Bunit.TestContext();

//            var mockQuizService = new Mock<IQuizService>();

//            mockQuizService.Setup(q => q.GetQuiz(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<string>()))
//                .ReturnsAsync(SetupQuizWithQuestions);

//            ctx.Services.AddSingleton(new QuestionValidator());
//            ctx.Services.AddSingleton(new AnswerValidator());
//            ctx.Services.AddSingleton(mockQuizService.Object);

//            var cut = ctx.RenderComponent<QuizEditor>(parameters => parameters
//                .Add(p => p.QuizId, Guid.NewGuid())
//                .Add(p => p.Layout, SetupMainLayout()));

//            var cardCount = cut.FindAll(".question-card").Count;

//            Assert.AreEqual(1, cardCount);
//        }

//        private QuizDto SetupQuizWithQuestions()
//        {
//            var quizId = Guid.NewGuid();

//            return new QuizDto
//            {
//                Id = quizId,
//                Description = "Description",
//                Questions = new List<QuestionDto>()
//                {
//                    new QuestionDto
//                    {
//                        Id = Guid.NewGuid(),
//                        QuizId = quizId,
//                        QuestionNumber = 1,
//                        QuestionText = "Question 1?"
//                    }
//                }
//            };
//        }

//        private MainLayout SetupMainLayout()
//        {
//            return new MainLayout
//            {
//                OrganisationLongName = "Test long name",
//                UserRole = "Editor"
//            };
//        }
//    }
//}
