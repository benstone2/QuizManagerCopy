using FluentValidation;
using QuizManager.Web.Models.Editor;

namespace QuizManager.Web.Validation
{
    public class AnswerValidator : AbstractValidator<Answer>
    {
        public AnswerValidator()
        {
            RuleFor(a => a.AnswerText).NotEmpty().WithMessage("Enter an answer");
        }
    }
}
