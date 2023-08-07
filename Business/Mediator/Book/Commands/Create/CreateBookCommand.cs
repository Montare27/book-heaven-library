namespace business.Mediator.Book.Commands.Create
{
    using AutoMapper;
    using Exceptions;
    using Services;
    using domain.Models;
    using Domain.Models;
    using Interfaces;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class CreateBookCommand : IRequest<int>
    {
        public int Id { get; set; }
        public CreateBookDetailsDto BookDetails { get; set; } = null!;
        public int AuthorId { get; set; }
        public List<int> GenresId { get; set; } = new List<int>();
        public class CreateBookDetailsDto
        {
            public int Id { get; set; }
            public int Length { get; set; }
            public string Title { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public int YearOfBook { get; set; }
            public string Image { get; set; } = string.Empty;
            public string File { get; set; } = string.Empty;
        }
    }
    
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, int>
    {
        private readonly IBookDbContext _db;
        private readonly ILogger<CreateBookCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _userService;

        public CreateBookCommandHandler(IBookDbContext db, IMapper mapper, ICurrentUserService userService, ILogger<CreateBookCommandHandler> logger)
        {
            _db = db;
            _mapper = mapper;
            _userService = userService;
            _logger = logger;
        }

        public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started Create Command");
            if (_userService.Id == Guid.Empty)
            {
                throw new NotAccessedActionException(typeof(CreateBookCommandHandler), _userService.Id);
            }
            
            var book = _mapper.Map<Book>(request);
            var author = await _db.Authors
                .FindAsync(new object?[] { request.AuthorId }, cancellationToken);

            if (author == null)
            {
                throw new NotFoundException(typeof(Author), request.AuthorId);
            }
            
            var genres = await _db.Genres.Where(x => 
                    request.GenresId.Contains(x.Id))
                .ToListAsync(cancellationToken);
            
            if (genres == null)
            {
                throw new NotFoundException(typeof(Genre), request.GenresId.First());
            }
            
            book.PublisherId = _userService.Id;
            book.Genres = genres;

            await _db.Books.AddAsync(book, cancellationToken);
            return await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
