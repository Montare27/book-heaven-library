namespace business.Mediatr.Genre.Queries.GetAll
{
    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Services;

    public class GetGenreListQueryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
    
    public class GetGenreListQuery : IRequest<List<GetGenreListQueryDto>>
    {
        public int Id { get; set; }
    }
    
    public class GetGenreListQueryHandler : IRequestHandler<GetGenreListQuery, List<GetGenreListQueryDto>>
    {
        private readonly IBookDbContext _db;
        private readonly IMapper _mapper;

        public GetGenreListQueryHandler(IBookDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<GetGenreListQueryDto>> Handle(GetGenreListQuery request, CancellationToken cancellationToken)
        {
            var genres = await _db.Genres.ToListAsync(cancellationToken);
            var result = _mapper.Map<List<GetGenreListQueryDto>>(genres);
            return result;
        }
    }
}
