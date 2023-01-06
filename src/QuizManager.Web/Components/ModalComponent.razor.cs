using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace QuizManager.Web.Components
{
    public partial class ModalComponent
    {
        [Parameter]
        public RenderFragment Title { get; set; }

        [Parameter]
        public RenderFragment Body { get; set; }

        [Parameter]
        public RenderFragment Footer { get; set; }

        public string Display = "none";
        public string Class = "";

        public List<string> Errors { get; set; }

        public void SetErrors(List<string> errors)
        {
            Errors = errors;
        }

        public void OpenModal()
        {
            Display = "block;";
            Class = "show";
            StateHasChanged();
        }

        public void CloseModal()
        {
            Class = "";
            Display = "none;";
            StateHasChanged();
        }
    }
}
