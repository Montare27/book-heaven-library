namespace business.Mediatr.Book.Commands.Update
{
    using AutoMapper;
    using domain.Models;
    using Exceptions;
    using Interfaces;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Services;

    public class UpdateBookCommand : IRequest<int>
    {
        public int Id { get; set; }
        public UpdateBookDetailsDto BookDetails { get; set; } = null!;
        public int AuthorId { get; set; }
        public List<int> GenresId { get; set; } = new List<int>();
        public class UpdateBookDetailsDto
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
    
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, int>
    {
        private readonly IBookDbContext _db;
        private readonly ICurrentUserService _userService;
        private readonly IMapper _mapper;
        
        public UpdateBookCommandHandler(IBookDbContext db, IMapper mapper, ICurrentUserService userService)
        {
            _db = db;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<int> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var existedBook = await _db.Books.FindAsync(request.Id, cancellationToken);

            if (existedBook == null)
            {
                throw new NotFoundException(typeof(Book), request.Id);
            }

            if (existedBook.PublisherId != _userService.Id)
            {
                throw new NotAccessedActionException(typeof(UpdateBookCommandHandler), _userService.Id);
            }
            
            var bookDetails = _mapper.Map<BookDetails>(request.BookDetails ?? throw new Exception(""));

            var book = _mapper.Map<Book>(request);
            var author = await _db.Authors.FindAsync(request.AuthorId, cancellationToken);
            var genres = await _db.Genres.Where(x => request.GenresId.Contains(x.Id))
                .ToListAsync(cancellationToken);
        
            book.BookDetails = bookDetails;
            book.Genres = genres;
            
            _db.Books.Entry(existedBook).CurrentValues.SetValues(book);
            return await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
