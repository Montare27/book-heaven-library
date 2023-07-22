namespace business.Mediator.Author.Validators
{
    using FluentValidation;
    using Mediatr.Author.Commands.Update;

    public class UpdateAuthorValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(20);

            RuleFor(x => x.Image)
                .NotEmpty();
        }
    }
}
