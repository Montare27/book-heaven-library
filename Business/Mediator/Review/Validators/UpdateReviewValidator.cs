namespace business.Mediator.Review.Validators
{
    using FluentValidation;
    using Mediatr.Review.Commands.Update;
    
    public class UpdateReviewValidator : AbstractValidator<UpdateReviewCommand>
    {
        public UpdateReviewValidator()
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
