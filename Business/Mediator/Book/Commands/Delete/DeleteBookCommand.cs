namespace business.Mediatr.Book.Commands.Delete
{
    using AutoMapper;
    using domain;
    using domain.Models;
    using Exceptions;
    using MediatR;
    using Services;

    public class DeleteBookCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
    
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, int>
    {

        private readonly IBookDbContext _db;

        public DeleteBookCommandHandler(IBookDbContext db)
        {
            _db = db;
        }

        public async Task<int> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _db.Books.FindAsync(new object?[] { request.Id, cancellationToken }, cancellationToken);
            if (book == null)
            {
                throw new NotFoundException(typeof(Book), request.Id);
            }

            _db.Books.Remove(book);
            return await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
