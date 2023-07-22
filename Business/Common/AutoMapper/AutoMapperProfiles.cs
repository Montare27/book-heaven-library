namespace business.Common.AutoMapper
{
    using domain.Models;
    using Domain.Models;
    using global::AutoMapper;
    using Mediator.Author.Commands.Create;
    using Mediator.Book.Commands.Create;
    using Mediator.Book.Query.GetAll;
    using Mediator.Review.Commands.Create;
    using Mediatr.Author.Commands.Update;
    using Mediatr.Author.Queries.GetAll;
    using Mediatr.Author.Queries.GetById;
    using Mediatr.Book.Commands.Update;
    using Mediatr.Book.Query.GetById;
    using Mediatr.Genre.Commands.Create;
    using Mediatr.Genre.Commands.Update;
    using Mediatr.Genre.Queries.GetAll;
    using Mediatr.Genre.Queries.GetById;
    using Mediatr.Review.Commands.Update;
    using Mediatr.Review.Queries.GetAll;
    using Mediatr.Review.Queries.GetById;

    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //Book
            //Create | Update
            CreateMap<CreateBookCommand, Book>();
            CreateMap<CreateBookCommand.CreateBookDetailsDto, BookDetails>();
            CreateMap<UpdateBookCommand, Book>();
            CreateMap<UpdateBookCommand.UpdateBookDetailsDto, BookDetails>();
            
            //Get Detailed
            CreateMap<Book, GetBookDetailedQueryDto>();
            CreateMap<Genre, GetBookDetailedQueryDto.GenreDto>();
            CreateMap<Review, GetBookDetailedQueryDto.ReviewDto>();
            CreateMap<Author, GetBookDetailedQueryDto.AuthorDto>();
            CreateMap<BookDetails, GetBookDetailedQueryDto.BookDetailsDto>();
            
            //Get List
            CreateMap<Book, GetBookListQueryDto>();
            CreateMap<BookDetails, GetBookListQueryDto.BookDetailsDto>();
            CreateMap<Author, GetBookListQueryDto.AuthorDto>();
            CreateMap<Genre, GetBookListQueryDto.GenreDto>();

            //Author
            //Create | Update
            CreateMap<CreateAuthorCommand, Author>();
            CreateMap<UpdateAuthorCommand, Author>();
            
            //Get Detailed
            CreateMap<Author, GetAuthorDetailedQueryDto>();
            CreateMap<Book, GetAuthorDetailedQueryDto.BookDto>();
            CreateMap<BookDetails, GetAuthorDetailedQueryDto.BookDetailsDto>();
            
            //Get List
            CreateMap<Author, GetAuthorListQueryDto>();
            
            //Genre
            //Create | Update
            CreateMap<CreateGenreCommand, Genre>();
            CreateMap<UpdateGenreCommand, Genre>();
            
            //Get Detailed
            CreateMap<Genre, GetGenreDetailedQueryDto>();
            CreateMap<Book, GetGenreDetailedQueryDto.BookDto>();
            CreateMap<BookDetails, GetGenreDetailedQueryDto.BookDetailsDto>();
            
            //Get List
            CreateMap<Genre, GetGenreListQueryDto>();
            
            //Review
            //Create | Update
            CreateMap<CreateReviewCommand, Review>();
            CreateMap<UpdateReviewCommand, Review>();
            
            //Get Detailed
            CreateMap<Review, GetReviewDetailedQueryDto>();
            
            //Get List
            CreateMap<Review, GetReviewListQueryDto>();
        }
    }
}
