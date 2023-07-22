namespace business.Mediatr.Genre.Commands.Update
{
    using Author.Commands.Update;
    using AutoMapper;
    using domain;
    using Domain.Models;
    using Exceptions;
    using MediatR;
    using Services;

    public class UpdateGenreCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
    
    public class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand, int>
    {
        private readonly IBookDbContext _db;
        private readonly IMapper _mapper;

        public UpdateGenreCommandHandler(IBookDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
        {
            var existedGenre = await _db.Genres.FindAsync(request.Id, cancellationToken);
            if (existedGenre == null)
            {
                throw new NotFoundException(typeof(Genre), request.Id);
            }

            var genre = _mapper.Map<Genre>(request);
            _db.Genres.Entry(existedGenre).CurrentValues.SetValues(genre);
            return await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
