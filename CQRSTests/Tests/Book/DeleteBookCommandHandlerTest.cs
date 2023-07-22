namespace BusinessTests.Tests.Book
{
    using business.Mediatr.Book.Commands.Delete;
    using BusinessTests.Common;
    using Microsoft.EntityFrameworkCore;
    using Shouldly;

    [Collection("QueryCollection")]
    public class DeleteBookCommandHandlerTest : TestFactory
    {

        [Fact]
        public async void HandlerMustDeleteBook()
        {
            //arrange
            var id = 2;
            await Context.Books.AddAsync(BookTestEntities.GetBook(id));
            await Context.SaveChangesAsync();
            var command = new DeleteBookCommand(){Id = id};
            var handler = new DeleteBookCommandHandler(Context);


            //act
            var result = await handler.Handle(command, default);
            var book = await Context.Books.FindAsync(id);

            //assert
            book.ShouldBeNull();
        }
    }
}
