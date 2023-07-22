namespace BusinessTests.Tests.Review
{
    using Common;
    using domain.Models;

    public abstract class ReviewTestEntities
    {
        public static Review GetReview(string text) => new Review(){
            BookId = 1,
            Text = text,
            Evaluation = 2,
            UserId = TestFactory.Id,
        };
        
        public static List<Review> Reviews = new List<Review>()
        {
            new Review()
            {
                Id = 1,
                BookId = 1,
                Text = "R1",
                Evaluation = 2,
                UserId = TestFactory.Id,
            },
            new Review()
            {
                Id = 2,
                BookId = 1,
                Text = "R2",
                Evaluation = 4,
                UserId = TestFactory.Id,
            },
            new Review()
            {
                Id = 3,
                BookId = 1,
                Text = "R3",
                Evaluation = 5,
                UserId = TestFactory.Id,
            },
            new Review()
            {
                Id = 4,
                BookId = 1,
                Text = "R4",
                Evaluation = 5,
                UserId = TestFactory.Id,
            },
            
        };
    }
}
