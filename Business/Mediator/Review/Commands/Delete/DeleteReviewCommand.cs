namespace business.Mediator.Review.Commands.Delete
{
    using AutoMapper;
    using business.Exceptions;
    using business.Services;
    using domain.Models;
    using Interfaces;
    using MediatR;

    public class DeleteReviewCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
    
    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, int>
    {
        private readonly IBookDbContext _db;
        private readonly ICurrentUserService _userService;
        private readonly IMapper _mapper;

        public DeleteReviewCommandHandler(IMapper mapper, ICurrentUserService userService, IBookDbContext db)
        {
            _mapper = mapper;
            _userService = userService;
            _db = db;
        }

        public async Task<int> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _db.Reviews.FindAsync(request.Id);
            if (review == null)
            {
                throw new NotFoundException(typeof(Review), request.Id);
            }
            
            if (review.UserId != _userService.Id && _userService.IsAdmin)
            {
                throw new Exception();
            }

            _db.Reviews.Remove(review);
            return await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
