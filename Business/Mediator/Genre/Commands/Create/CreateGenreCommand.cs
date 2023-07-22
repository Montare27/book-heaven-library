namespace business.Mediatr.Genre.Commands.Create
{
    using AutoMapper;
    using domain;
    using Domain.Models;
    using MediatR;
    using Services;

    public class CreateGenreCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
    
    public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, int>
    {
        private readonly IBookDbContext _db;
        private readonly IMapper _mapper;

        public CreateGenreCommandHandler(IBookDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = _mapper.Map<Genre>(request);
            await _db.Genres.AddAsync(genre, cancellationToken);
            return await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
