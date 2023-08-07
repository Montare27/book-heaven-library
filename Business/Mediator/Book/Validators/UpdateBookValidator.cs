namespace business.Mediator.Book.Validators
{
    using business.Mediatr.Book.Commands.Update;
    using FluentValidation;

    public class UpdateBookValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookValidator()
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
