namespace business.Mediator.Genre.Validators
{
    using FluentValidation;
    using Mediatr.Genre.Commands.Create;

    public class CreateGenreValidator : AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(15);
        }
    }
}
