namespace business.Services
{
    using domain;
    using domain.Models;
    using Domain.Models;
    using Microsoft.EntityFrameworkCore;

    public interface IBookDbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<BookDetails> Details { get; set; }
        public DbSet<Author> Authors { get; set; } 
        public DbSet<Genre> Genres { get; set; } 
        public DbSet<Review> Reviews { get; set; }
        public DbSet<SavedBooksList> SavedBooksLists { get; set; }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
