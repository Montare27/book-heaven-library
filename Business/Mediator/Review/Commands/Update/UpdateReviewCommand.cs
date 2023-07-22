namespace business.Mediatr.Review.Commands.Update
{
    using AutoMapper;
    using domain.Models;
    using Exceptions;
    using Interfaces;
    using MediatR;
    using Services;

    public class UpdateReviewCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int Evaluation { get; set; }
        public int BookId { get; set; } 
    }
    
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, int>
    {
        private readonly IBookDbContext _db;
        private readonly ICurrentUserService _userService;
        private readonly IMapper _mapper;

        public UpdateReviewCommandHandler(IBookDbContext db, ICurrentUserService userService, IMapper mapper)
        {
            _db = db;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var existedReview = await _db.Reviews.FindAsync(request, cancellationToken);
            if (existedReview == null)
            {
                throw new NotFoundException(typeof(Review), request.Id);
            }

            if (existedReview.UserId != _userService.Id && !_userService.IsAdmin)
            {
                throw new NotAccessedActionException(typeof(UpdateReviewCommandHandler), _userService.Id);
            }

            var review = _mapper.Map<Review>(request);
            review.UserId = _userService.Id;
            _db.Reviews.Entry(existedReview).CurrentValues.SetValues(review);
            return await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
