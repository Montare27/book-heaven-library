namespace business.Services
{
    using domain;
    using domain.Models;
    using Exceptions;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class SaveBookService
    {

        private readonly IBookDbContext _db;
        private readonly ICurrentUserService _userService;

        public SaveBookService(IBookDbContext db, ICurrentUserService userService)
        {
            _db = db;
            _userService = userService;
        }

        public async Task<List<Book>> GetALlBooksFromSavedList()
        {
            var books = await _db.SavedBooksLists
                .FirstOrDefaultAsync(x => x.UserId == _userService.Id);
            return books == null  ? new List<Book>() : books.SavedBooks.ToList();
        }
        
        public async Task SaveBook(int id)
        {
            var book = await _db.Books.FindAsync(id);
            if (book == null)
            {
                throw new NotFoundException(typeof(Book), id);
            }
            
            var existedList = _db.SavedBooksLists
                .FirstOrDefault(x => x.UserId == _userService.Id);

            if (existedList == null)
            {
                var newList = new SavedBooksList
                {
                    UserId = _userService.Id, 
                    SavedBooks = new List<Book>{ book },
                };
                await _db.SavedBooksLists.AddAsync(newList);
                await _db.SaveChangesAsync(CancellationToken.None);
            }
            
            else if (!existedList.SavedBooks.Contains(book))
            {
                existedList.SavedBooks.Add(book);
                await _db.SaveChangesAsync(CancellationToken.None);
            }
            
        }

        public async Task RemoveBookFromSavedList(int id)
        {
            var book = await _db.Books.FindAsync(id);
            if (book == null)
            {
                throw new NotFoundException(typeof(Book), id);
            }
            
            var existedList = _db.SavedBooksLists
                .FirstOrDefault(x => x.UserId == _userService.Id);

            if (existedList == null)
            {
                throw new NotFoundException(typeof(SavedBooksList), id);
            }
            
            if (!existedList.SavedBooks.Contains(book))
            {
                throw new NotFoundException(typeof(Book), book.Id);
            }

            existedList.SavedBooks.Remove(book);
            await _db.SaveChangesAsync(CancellationToken.None);
        }
    }
}
