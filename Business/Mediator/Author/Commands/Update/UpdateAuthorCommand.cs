namespace business.Mediatr.Author.Commands.Update
{
    using AutoMapper;
    using domain;
    using domain.Models;
    using MediatR;
    using Services;

    public class UpdateAuthorCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
    }
    
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, int>
    {
        private readonly IBookDbContext _db;
        private readonly IMapper _mapper;

        public UpdateAuthorCommandHandler(IBookDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var existedAuthor = await _db.Authors.FindAsync(request.Id, cancellationToken);
            if (existedAuthor == null)
            {
                throw new Exception();
            }

            var author = _mapper.Map<Author>(request);
            _db.Authors.Entry(existedAuthor).CurrentValues.SetValues(author);
            return await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
