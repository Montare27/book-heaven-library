namespace persistence
{
    using business.Services;
    using domain.Models;
    using Domain.Models;
    using Microsoft.EntityFrameworkCore;

    public class BookDbContext : DbContext, IBookDbContext
    {
        public BookDbContext(){}
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options){}

        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<BookDetails> Details { get; set; } = null!;
        public DbSet<Author> Authors { get; set; } = null!;
        public DbSet<Genre> Genres { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<SavedBooksList> SavedBooksLists { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasMany(x=>x.Genres)
                .WithMany(x => x.Books);

            modelBuilder.Entity<Book>()
                .HasOne(x => x.Author)
                .WithMany(x => x.Books);

            modelBuilder.Entity<Book>()
                .HasOne(x => x.BookDetails);

            modelBuilder.Entity<Book>()
                .HasMany(x => x.Reviews)
                .WithOne(x => x.Book);

            modelBuilder.Entity<SavedBooksList>()
                .HasMany<Book>();
        }
    }
}
