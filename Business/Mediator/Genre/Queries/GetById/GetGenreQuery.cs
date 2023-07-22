namespace business.Mediatr.Genre.Queries.GetById
{
    using AutoMapper;
    using Domain.Models;
    using Exceptions;
    using MediatR;
    using Services;

    public class GetGenreDetailedQueryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<BookDto> Books { get; set; } = new List<BookDto>();
        
        public class BookDto
        {
            public int Id { get; set; }
            public BookDetailsDto BookDetails { get; set; } = null!;
        }
        public class BookDetailsDto
        {
            public int Id { get; set; }
            public string Title { get; set; } = string.Empty;
            public int YearOfBook { get; set; }
            public double Rating { get; set; }
            public string Image { get; set; } = string.Empty;
        }
    }
    
    public class GetGenreQuery : IRequest<GetGenreDetailedQueryDto>
    {
        public int Id { get; set; }
    }
    
    public class GetGenreDetailedQueryHandler : IRequestHandler<GetGenreQuery, GetGenreDetailedQueryDto>
    {
        private readonly IBookDbContext _db;
        private readonly IMapper _mapper;

        public GetGenreDetailedQueryHandler(IBookDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<GetGenreDetailedQueryDto> Handle(GetGenreQuery request, CancellationToken cancellationToken)
        {
            var genre = await _db.Genres.FindAsync(request.Id, cancellationToken);
            if (genre == null)
            {
                throw new NotFoundException(typeof(Genre), request.Id);
            }

            var result = _mapper.Map<GetGenreDetailedQueryDto>(genre);
            return result;
        }
    }
}
