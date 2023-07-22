namespace business.Mediator.Author.Commands.Delete
{
    using business.Services;
    using MediatR;

    public class DeleteAuthorCommand : IRequest<int>
    {
        public int Id { get; set; }    
    }
    
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, int>
    {
        private readonly IBookDbContext _db;

        public DeleteAuthorCommandHandler(IBookDbContext db)
        {
            _db = db;
        }

        public async Task<int> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _db.Authors.FindAsync(request.Id, cancellationToken);
            if (author == null)
            {
                throw new Exception();
            }

            _db.Authors.Remove(author);
            return await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
