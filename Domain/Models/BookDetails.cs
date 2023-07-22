namespace domain.Models
{
    public class BookDetails
    {
        public int Id { get; set; }
        public int Length { get; set; }
        public string Title { get; set; } = string.Empty;
        public double Rating { get; set; }
        public string Description { get; set; } = string.Empty;
        public int YearOfBook { get; set; }
        public string Image { get; set; } = string.Empty;
        public string File { get; set; } = string.Empty;
        public DateTime AddDate { get; set; } = DateTime.UtcNow;
    }
}
