using Microsoft.AspNetCore.Components;
using QuizManager.Types.Quiz.Commands;
using QuizManager.Types.Quiz.Models;
using QuizManager.Web.Services;
using System;
using System.Threading.Tasks;

namespace QuizManager.Web.Components
{
    public partial class QuizOverview
    {

        [Parameter]
        public QuizOverviewDto Quiz { get; set; }

        [Parameter]
        public string UserRole { get; set; }

        [Parameter]
        public EventCallback DeleteQuiz { get; set; }

        public ModalComponent Modal { get; set; } = new ModalComponent();

        public void TriggerDeleteWarning()
        {
            Modal.OpenModal();
        }

        public void TriggerDeleteQuiz()
        {
            DeleteQuiz.InvokeAsync(Quiz.Id);
            Modal.CloseModal();
        }

        public void CloseModal()
        {
            Modal.CloseModal();
        }
    }
}
