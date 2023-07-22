namespace domain.Models
{

    public class SavedBooksList
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public ICollection<Book> SavedBooks { get; set; } = new List<Book>();
    }
}
