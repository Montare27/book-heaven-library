namespace business.Mediator.Genre.Validators
{
    using FluentValidation;
    using Mediatr.Genre.Commands.Update;

    public class UpdateGenreValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(15);
        }
    }
}
