namespace business.Mediator.Book.Query.GetAll
{
    using AutoMapper;
    using business.Services;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetBookListQueryDto
    {
        public int Id { get; set; }
        public BookDetailsDto BookDetails { get; set; } = null!;
        public AuthorDto Author { get; set; } = null!;
        public Guid PublisherId { get; set; }
        public ICollection<GenreDto> Genres { get; set; } = new List<GenreDto>();
        
        public class BookDetailsDto
        {
            public int Id { get; set; }
            public string Title { get; set; } = string.Empty;
            public int Rating { get; set; }
            public string Image { get; set; } = string.Empty;
        }
        public class AuthorDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }
        public class GenreDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }
    }   
    
    public class GetBookListQuery : IRequest<List<GetBookListQueryDto>>
    {
        public int Id { get; set; }
    }
    
    public class GetBookListQueryHandler : IRequestHandler<GetBookListQuery, List<GetBookListQueryDto>>
    {
        private readonly IBookDbContext _db;
        private readonly IMapper _mapper;
        
        public GetBookListQueryHandler(IBookDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public Task<List<GetBookListQueryDto>> Handle(GetBookListQuery request, CancellationToken cancellationToken)
        {
            var books = _db.Books
                .Include(x => x.BookDetails)
                .Include(x => x.Genres)
                .Include(x => x.Author);

            var result = _mapper.Map<List<GetBookListQueryDto>>(books);
            return Task.FromResult(result);
        }
    }
}
