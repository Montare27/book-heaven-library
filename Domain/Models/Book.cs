namespace domain.Models
{
    using Domain.Models;

    public class Book
    {
        public int Id { get; set; }
        public BookDetails BookDetails { get; set; } = null!;
        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;
        public Guid PublisherId { get; set; }
        public ICollection<Genre> Genres { get; set; } = new List<Genre>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
