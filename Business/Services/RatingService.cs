namespace business.Services
{
    using domain.Models;
    using Exceptions;

    public class RatingService
    {
        private readonly IBookDbContext _db;

        public RatingService(IBookDbContext db)
        {
            _db = db;
        }

        public async Task<int> UpdateRatingForBook(int id)
        {
            var book = await _db.Books.FindAsync(id);
            if (book == null)
            {
                throw new NotFoundException(typeof(Book), id);
            }

            var reviews = book.Reviews;
            double rate = 0;
            
            if (reviews.Count != 0)
            {
                rate = reviews.Sum(x => x.Evaluation) / (double)reviews.Count;
            }
            
            book.BookDetails.Rating = Math.Round(rate, 1);
            return await _db.SaveChangesAsync(CancellationToken.None);
        }
    }
}
