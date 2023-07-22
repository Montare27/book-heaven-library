namespace business.Mediatr.Genre.Commands.Delete
{
    using AutoMapper;
    using domain;
    using Domain.Models;
    using Exceptions;
    using Mediator.Author.Commands.Delete;
    using MediatR;
    using Services;
    using System.Linq.Expressions;

    public class DeleteGenreCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
    
    public class DeleteGenreCommandHandler : IRequestHandler<DeleteAuthorCommand, int>
    {
        private readonly IBookDbContext _db;

        public DeleteGenreCommandHandler(IBookDbContext db)
        {
            _db = db;
        }

        public async Task<int> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var existedGenre = await _db.Genres.FindAsync(request.Id, cancellationToken);
            if (existedGenre == null)
            {
                throw new NotFoundException(typeof(Genre), request.Id);
            }

            _db.Genres.Remove(existedGenre);
            return await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
