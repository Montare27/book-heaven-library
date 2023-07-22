namespace domain.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public int Evaluation { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;
    }
}
