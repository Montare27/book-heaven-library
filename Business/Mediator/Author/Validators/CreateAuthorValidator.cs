namespace business.Mediator.Author.Validators
{
    using Commands.Create;
    using FluentValidation;

    public class CreateAuthorValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorValidator()
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
