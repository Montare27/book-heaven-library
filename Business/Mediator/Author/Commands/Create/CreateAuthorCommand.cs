namespace business.Mediator.Author.Commands.Create
{
    using AutoMapper;
    using business.Services;
    using domain.Models;
    using MediatR;

    public class CreateAuthorCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
    }
    
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, int>
    {
        private readonly IBookDbContext _db;
        private readonly IMapper _mapper;
        
        public CreateAuthorCommandHandler(IBookDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = _mapper.Map<Author>(request);
            await _db.Authors.AddAsync(author, cancellationToken);
            return await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
