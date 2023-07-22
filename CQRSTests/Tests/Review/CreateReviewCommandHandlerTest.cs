namespace BusinessTests.Tests.Review
{
    using Book;
    using business.Mediator.Review.Commands.Create;
    using business.Services;
    using Common;
    using Microsoft.EntityFrameworkCore;
    using Shouldly;

    [Collection("QueryCollection")]
    public class CreateReviewCommandHandlerTest : TestFactory
    {
        [Fact]
        public async void ReviewsMustBeCreated_AndRatingMustBeChanged()
        {
            //arrange
            var ratingService = new RatingService(Context);
            var commands = new List<CreateReviewCommand>(){
                new CreateReviewCommand()
                {
                    Id = 1,
                    BookId = 1,
                    Text = "R1",
                    Evaluation = 2,
                },
                new CreateReviewCommand()
                {
                    Id = 2,
                    BookId = 1,
                    Text = "R2",
                    Evaluation = 4,
                },
                new CreateReviewCommand()
                {
                    Id = 3,
                    BookId = 1,
                    Text = "R3",
                    Evaluation = 5,
                },
                new CreateReviewCommand()
                {
                    Id = 4,
                    BookId = 1,
                    Text = "R4",
                    Evaluation = 5,
                },
            };
            var handler = new CreateReviewCommandHandler(Context, Mapper, UserService.Object, ratingService);
            // var book = BookTestEntities.GetBook("BookForReviews");
            // if (!Context.Books.Contains(book))
            // {
            //     await Context.Books.AddAsync(book);
            // }
            
            
            //act
            for(int i = 0; i < commands.Count; i++)
            {
                 await handler.Handle(commands[i], default);
            }
            var resultReviews = await Context.Reviews.Where(x => x.Id < 5).ToListAsync();
            var resultBook = await Context.Books.FirstOrDefaultAsync(x => x.BookDetails.Title.Equals("BookForReviews"));

            //assert
            resultReviews.ShouldBeEquivalentTo(ReviewTestEntities.Reviews);
            resultBook.ShouldNotBeNull();
            resultBook.BookDetails.Rating.ShouldBe(4);
        }
    }
}
