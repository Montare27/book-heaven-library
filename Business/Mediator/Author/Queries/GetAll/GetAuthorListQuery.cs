namespace business.Mediatr.Author.Queries.GetAll
{
    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Services;

    public class GetAuthorListQueryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
    }
    
    public class GetAuthorListQuery : IRequest<List<GetAuthorListQueryDto>>
    {
        public int Id { get; set; }
    }
    
    public class GetAuthorListQueryHandler : IRequestHandler<GetAuthorListQuery, List<GetAuthorListQueryDto>>
    {
        private readonly IBookDbContext _db;
        private readonly IMapper _mapper;

        public GetAuthorListQueryHandler(IBookDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<GetAuthorListQueryDto>> Handle(GetAuthorListQuery request, CancellationToken cancellationToken)
        {
            var authors = await _db.Authors.ToListAsync(cancellationToken);
            if (authors.Count == 0)
            {
                throw new Exception();
            }

            var result = _mapper.Map<List<GetAuthorListQueryDto>>(authors);
            return result;
        }
    }
}
