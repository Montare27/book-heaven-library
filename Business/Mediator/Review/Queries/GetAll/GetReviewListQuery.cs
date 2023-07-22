namespace business.Mediatr.Review.Queries.GetAll
{
    using AutoMapper;
    using Interfaces;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Services;

    public class GetReviewListQueryDto
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; } = string.Empty;
        public int Evaluation { get; set; }
        public int BookId { get; set; } 
    }
    
    public class GetReviewListQuery : IRequest<List<GetReviewListQueryDto>>
    {
        public int Id { get; set; }
    }
    
    public class GetReviewListQueryHandler : IRequestHandler<GetReviewListQuery, List<GetReviewListQueryDto>>
    {
        private readonly IBookDbContext _db;
        private readonly ICurrentUserService _userService;
        private readonly IMapper _mapper;

        public GetReviewListQueryHandler(IBookDbContext db, ICurrentUserService userService, IMapper mapper)
        {
            _db = db;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<List<GetReviewListQueryDto>> Handle(GetReviewListQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _db.Reviews.ToListAsync(cancellationToken);
            var reviewsDto = _mapper.Map<List<GetReviewListQueryDto>>(reviews);
            return reviewsDto;
        }
    }
}
