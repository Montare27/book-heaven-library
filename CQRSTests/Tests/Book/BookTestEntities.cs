namespace BusinessTests.Tests.Book
{
    using Common;
    using domain.Models;
    using Domain.Models;

    public abstract class BookTestEntities
    {
        public readonly static Author AuthorForCreating = new Author()
        {
            Id = 1,
            Image = "Image",
            Name = "Name",
            Books = new List<Book>(),
        };

        public readonly static Genre GenreForCreating = new Genre()
        {
            Id = 1,
            Name = "Horror",
            Books = new List<Book>(),
        };

        public static int Id
        {
            get => Id++;
            set => throw new Exception();
        }

        public static Book GetBook(int id) => new Book()
        {
            BookDetails = new BookDetails()
            {
                Id = id,
                Description = "Description",
                File = "File",
                Image = "Image",
                Length = 100,
                Title = "Title",
                YearOfBook = 2001,
                AddDate = TestFactory.DateTime,
                Rating = 0,
            },
            Author = AuthorForCreating,
            Genres = new List<Genre>(){GenreForCreating},
            PublisherId = TestFactory.Id,
            Reviews = new List<Review>()
        };

    }
}
