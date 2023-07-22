namespace BusinessTests.Tests.Book
{
    using business.Mediatr.Book.Query.GetById;
    using Common;
    using Shouldly;

    [Collection("QueryCollection")]
    public class GetBookDetailedCommandHandler : TestFactory
    {
        [Fact]
        public async void HandleMustBookMustGetBookById()
        {
            //arrange
            int id = 3;
            var expected = new GetBookDetailedQueryDto()
            {
                Id = id,
                PublisherId = Id,
                Reviews = new List<GetBookDetailedQueryDto.ReviewDto>(),
                Genres = new List<GetBookDetailedQueryDto.GenreDto>() 
                {
                    new GetBookDetailedQueryDto.GenreDto()
                    {
                        Name = "Horror",
                    },
                },
                Author = new GetBookDetailedQueryDto.AuthorDto()
                {
                    Image = "Image",
                    Name = "Name",
                },
                BookDetails = new GetBookDetailedQueryDto.BookDetailsDto()
                {
                    Description  = "Description",
                    File = "File",
                    Image = "Image",
                    Length = 100,
                    Title = "GetDetailedBook",
                    YearOfBook = 2001,
                },
            };
            
            var command = new GetBookDetailedQuery(){Id = id};

            //act
            var book = BookTestEntities.GetBook(id);
            await Context.Books.AddAsync(book);
            await Context.SaveChangesAsync();
            var handler = new GetBookDetailedQueryHandler(Context, Mapper);
            var result = await handler.Handle(command, default);
    
    
            //assert
            result.ShouldBeEquivalentTo(expected);
        }
    }
}
