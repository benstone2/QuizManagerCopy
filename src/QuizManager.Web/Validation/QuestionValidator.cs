using FluentValidation;
using QuizManager.Web.Models.Editor;

namespace QuizManager.Web.Validation
{
    public class QuestionValidator : AbstractValidator<Question>
    {
        public QuestionValidator()
        {
            RuleFor(c => c.QuestionText).NotEmpty().WithMessage("Enter a question");

            RuleFor(q => q.Answers).Must(q => q.Count > 2).WithMessage("Enter atleast 3 possible answers");
        }
    }
}
