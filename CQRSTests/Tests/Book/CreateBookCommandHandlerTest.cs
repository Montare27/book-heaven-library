namespace BusinessTests.Tests.Book
{
    using business.Mediator.Book.Commands.Create;
    using Common;
    using Shouldly;
    
    [Collection("QueryCollection")]
    public class CreateBookCommandHandlerTest : TestFactory
    {
        [Fact]
        public async void CreateHandlerMustMustCreateBook()
        {
            //arrange
            int id = 1;
            var handler = new CreateBookCommandHandler(Context, Mapper, UserService.Object);
            var command = new CreateBookCommand
            {
                Id = id,
                BookDetails = new CreateBookCommand.CreateBookDetailsDto()
                {
                    Description = "Description",
                    File = "File",
                    // Image = "Image",
                    Length = 100,
                    Title = "CreateBook",
                    YearOfBook = 2001,
                },
                AuthorId = 1,
                GenresId = new List<int> { 1 },
            };

            //act
            await handler.Handle(command, default);
            var expected = await Context.Books.FindAsync(id);
            expected!.BookDetails.AddDate = DateTime;

            //assert
            expected.ShouldNotBeNull();
            expected.ShouldBeEquivalentTo(BookTestEntities.GetBook( id));
        }
    }
}
