namespace business.Mediatr.Author.Queries.GetById
{
    using AutoMapper;
    using MediatR;
    using Services;

    public class GetAuthorDetailedQueryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public ICollection<BookDto> Books { get; set; } = new List<BookDto>();
        public class BookDto
        {
            public int Id { get; set; }
            public BookDetailsDto? BookDetails { get; set; } 
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
    
    public class GetAuthorDetailedQuery : IRequest<GetAuthorDetailedQueryDto>
    {
        public int Id { get; set; }
    }
    
    public class GetAuthorDetailedQueryHandler : IRequestHandler<GetAuthorDetailedQuery, GetAuthorDetailedQueryDto>
    {
        private readonly IBookDbContext _db;
        private readonly IMapper _mapper;

        public GetAuthorDetailedQueryHandler(IBookDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<GetAuthorDetailedQueryDto> Handle(GetAuthorDetailedQuery request, CancellationToken cancellationToken)
        {
            var author = await _db.Authors.FindAsync(request.Id, cancellationToken);
            if (author == null)
            {
                throw new Exception();
            }

            var result = _mapper.Map<GetAuthorDetailedQueryDto>(author);
            return result;
        }
    }
}
