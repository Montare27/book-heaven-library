namespace business.Mediator.Review.Commands.Create
{
    using AutoMapper;
    using business.Services;
    using domain.Models;
    using Interfaces;
    using MediatR;

    public class CreateReviewCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int Evaluation { get; set; }
        public int BookId { get; set; } 
    }
    
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, int>
    {
        private readonly IBookDbContext _db;
        private readonly ICurrentUserService _userService;
        private readonly IMapper _mapper;
        private readonly RatingService _ratingService;

        public CreateReviewCommandHandler(IBookDbContext db, IMapper mapper, ICurrentUserService userService, RatingService ratingService)
        {
            _db = db;
            _mapper = mapper;
            _userService = userService;
            _ratingService = ratingService;
        }

        public async Task<int> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var review = _mapper.Map<Review>(request);
            review.UserId = _userService.Id;
            await _db.Reviews.AddAsync(review, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);

            return await _ratingService.UpdateRatingForBook(review.BookId);
        }
    }
}
