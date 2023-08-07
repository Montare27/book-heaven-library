namespace business.Mediator.Book.Validators
{
    using Commands.Create;
    using FluentValidation;

    public class CreateBookValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookValidator()
        {
            RuleFor(x => x.BookDetails.Description)
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(1000);
            
            RuleFor(x => x.BookDetails.Title)
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(50);
            
            RuleFor(x => x.BookDetails.File)
                .NotEmpty();

            RuleFor(x => x.BookDetails.Image)
                .NotEmpty();
            
            RuleFor(x => x.BookDetails.Length)
                .Must(x => x > 0);
        }
    }
}
