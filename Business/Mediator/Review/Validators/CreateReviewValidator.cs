namespace business.Mediator.Review.Validators
{
    using Commands.Create;
    using FluentValidation;
    using System.Data;

    public class CreateReviewValidator : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(500);

            RuleFor(x => x.Evaluation)
                .Must(x => x is >= 0 and <= 5);
        }
    }
}
