namespace Interfaces
{
    public interface IFilterModel
    {
        public string? Title { get; set; }
        public string? AuthorName { get; set; }
        public string? GenreName { get; set; } 
        public int? YearOfBook { get; set; }
        public bool? OrderByNewer { get; set; }
        public bool? OrderByOlder { get; set; }
    }
}
