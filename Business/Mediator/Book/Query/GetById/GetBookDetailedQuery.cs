namespace business.Mediatr.Book.Query.GetById
{
    using AutoMapper;
    using domain.Models;
    using Exceptions;
    using MediatR;
    using Services;

    public class GetBookDetailedQueryDto 
    {
        public int Id { get; set; }
        public BookDetailsDto BookDetails { get; set; } = null!;
        public AuthorDto Author { get; set; } = null!;
        public Guid PublisherId { get; set; }
        public ICollection<GenreDto> Genres { get; set; } = new List<GenreDto>();
        public ICollection<ReviewDto> Reviews { get; set; } = new List<ReviewDto>();
        
        public class BookDetailsDto
        {
            public int Id { get; set; }
            public int Length { get; set; }
            public string Title { get; set; } = string.Empty;
            public double Rating { get; set; }
            public string Description { get; set; } = string.Empty;
            public int YearOfBook { get; set; }
            public string Image { get; set; } = string.Empty;
            public string File { get; set; } = string.Empty;
        }
        public class AuthorDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Image { get; set; } = string.Empty;
        }
        public class GenreDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }
        public class ReviewDto
        {
            public int Id { get; set; }
            public Guid UserId { get; set; }
            public string Text { get; set; } = string.Empty;
            public int Evaluation { get; set; }
        }
    }

    public class GetBookDetailedQuery : IRequest<GetBookDetailedQueryDto>
    {
        public int Id { get; set; }
    }
    
    public class GetBookDetailedQueryHandler : IRequestHandler<GetBookDetailedQuery, GetBookDetailedQueryDto>
    {
        private readonly IBookDbContext _db;
        private readonly IMapper _mapper;

        public GetBookDetailedQueryHandler(IBookDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        
        public async Task<GetBookDetailedQueryDto> Handle(GetBookDetailedQuery request, CancellationToken cancellationToken)
        { 
            var book = await _db.Books.FindAsync(request.Id, cancellationToken);
            var result = _mapper.Map<GetBookDetailedQueryDto>(book);
            // result.GetAuthor = _mapper.Map<GetAuthorDtoForBookService>(book.Author ?? throw new Exception());
            // result.Genres = _mapper.Map<List<GenreDtoForBookService>>(book.Genres ?? throw new Exception());
            // result.Reviews = _mapper.Map<List<GetReviewDtoForBookService>>(book.Reviews ?? throw new Exception());

            return result;
        }
    }
}
