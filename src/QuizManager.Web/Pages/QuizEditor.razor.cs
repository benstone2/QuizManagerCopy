using FluentValidation.Results;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using QuizManager.Types.Quiz.Commands;
using QuizManager.Types.Quiz.Models;
using QuizManager.Web.Components;
using QuizManager.Web.Interfaces;
using QuizManager.Web.Models.Editor;
using QuizManager.Web.Services;
using QuizManager.Web.Shared;
using QuizManager.Web.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizManager.Web.Pages
{
    public partial class QuizEditor
    {
        [Parameter]
        public Guid QuizId { get; set; }

        [CascadingParameter]
        public MainLayout Layout { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public QuizService QuizService { get; set; }

        [Inject]
        public QuestionValidator QuestionValidator { get; set; }

        [Inject]
        public AnswerValidator AnswerValidator { get; set; }

        public Quiz Quiz { get; set; }

        public List<string> Errors = new List<string>();

        public ModalComponent Modal { get; set; } = new ModalComponent();

        protected override async Task OnInitializedAsync()
        {
            Layout.ShowNavbar(true);

            var auth = await AuthenticationStateProvider.GetAuthenticationStateAsync();

            Layout.SetUserData(auth);

            if (auth.User.Identity.IsAuthenticated == true)
            {
                var response = await QuizService.GetQuiz(Layout.OrganisationId, QuizId, Layout.UserRole);

                Quiz = new Quiz(response);
            }
            else
            {
                NavigationManager.NavigateTo("/login");
            }       
        }

        private async Task HandleSaveQuiz()
        {
            SaveCurrentQuestion();

            if (Errors.Count == 0)
            {
                var quizDto = MapModelToDto(Quiz);

                var command = new UpdateQuizCommand
                {
                    UserId = Layout.UserId,
                    QuizDto = quizDto
                };

                var response = await QuizService.UpdateQuiz(command);

                if (response.Success)
                {
                    // Show success notification
                }
            }
        }

        private void AddQuestion()
        {
            SaveCurrentQuestion();

            if (Errors.Count == 0)
            {
                Quiz.AddQuestion();
            }
        }

        private void SaveCurrentQuestion()
        {
            foreach (var question in Quiz.Questions)
            {
                if (question.Editing == true)
                {
                    SaveQuestionChanges(question);

                    if (Errors.Count == 0)
                    {
                        question.Editing = false;
                    }
                    else
                    {
                        question.ContainsErrors = true;
                        Modal.OpenModal();
                    }
                }
            }
        }

        private void SaveQuestionChanges(Question question)
        {
            Errors = new List<string>();
            question.ResetError();

            var answerErrors = ValidateAnswers(question);
            var questionErrors = ValidateQuestion(question);

            if (questionErrors.Count == 0 && answerErrors.Count == 0)
            {
                question.Editing = false;
            }
            else
            {
                question.SetError();
                Errors.AddRange(answerErrors);
                Errors.AddRange(questionErrors);
            } 
        }

        private void StartEditQuestion(Question question)
        {
            foreach (var q in Quiz.Questions)
            {
                if (q.Editing == true)
                {
                    SaveQuestionChanges(q);
                }
            }

            if (Errors.Count == 0)
            {
                question.Editing = true;
            }
            else
            {
                Modal.OpenModal();
            }
        }

        private void DeleteQuestion(Question question)
        {
            SaveCurrentQuestion();

            if (Errors.Count == 0)
            {
                Quiz.DeleteQuestion(question);
            }
        }
       
        private void EditAnswer(Answer answer)
        {
            answer.Editing = true;
        }

        private void SaveAnswer(Answer answer)
        {
            var result = AnswerValidator.Validate(answer);

            if (result.IsValid == true)
            {
                answer.Editing = false;
            }
        }

        private List<string> ValidateQuestion(Question question)
        {
            var questionErrors = QuestionValidator.Validate(question);

            return questionErrors.Errors.Select(e => e.ErrorMessage).ToList();
        }

        private List<string> ValidateAnswers(Question question)
        {
            var answerErrors = new List<ValidationFailure>();

            foreach (var answer in question.Answers)
            {
                if (answer.Editing)
                {
                    var result = AnswerValidator.Validate(answer);

                    if (result.IsValid == true)
                    {
                        answer.Editing = false;
                    }
                    else
                    {
                        answerErrors.AddRange(result.Errors);
                    }
                }
            }

            if (answerErrors.Count == 1)
            {
                return new List<string> { answerErrors.First().ErrorMessage };
            }
            else if (answerErrors.Count > 1)
            {
                return new List<string> { "Multiple errors in answers" };
            }

            return new List<string>();
        }

        private QuizDto MapModelToDto(Quiz quizModel)
        {
            return new QuizDto
            {
                Id = quizModel.Id,
                Title = quizModel.Title,
                Description = quizModel.Description,
                Questions = quizModel.Questions.Select(q => new QuestionDto(q.Id,
                    quizModel.Id,
                    q.QuestionNumber,
                    q.QuestionText,
                    q.Answers.Select(a => new AnswerDto(a.Id, q.Id, a.AnswerNumber.Number, a.AnswerText, a.IsCorrect)).ToList()
                )).ToList()
            };
        }
    }
}
