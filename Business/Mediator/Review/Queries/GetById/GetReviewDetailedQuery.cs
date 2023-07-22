namespace business.Mediatr.Review.Queries.GetById
{
    using AutoMapper;
    using Interfaces;
    using MediatR;
    using Services;

    public class GetReviewDetailedQueryDto
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; } = string.Empty;
        public int Evaluation { get; set; }
        public int BookId { get; set; } 
    }
    
    public class GetReviewDetailedQuery : IRequest<GetReviewDetailedQueryDto>
    {
        public int Id { get; set; }
    }
    
    public class GetReviewDetailedQueryHandler : IRequestHandler<GetReviewDetailedQuery, GetReviewDetailedQueryDto>
    {
        private readonly IBookDbContext _db;
        private readonly ICurrentUserService _userService;
        private readonly IMapper _mapper;
        
        public GetReviewDetailedQueryHandler(IBookDbContext db, ICurrentUserService userService, IMapper mapper)
        {
            _db = db;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<GetReviewDetailedQueryDto> Handle(GetReviewDetailedQuery request, CancellationToken cancellationToken)
        {
            var review = await _db.Reviews.FindAsync(request.Id);
            if (review == null)
            {
                throw new Exception();
            }

            var result = _mapper.Map<GetReviewDetailedQueryDto>(review);
            return result;
        }
    }
}
