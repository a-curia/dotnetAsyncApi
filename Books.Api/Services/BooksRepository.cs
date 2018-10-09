using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Api.Contexts;
using Books.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Books.Api.Services
{
    public class BooksRepository : IBooksRepository, IDisposable // implement IDisposable so that you can avoid leaks - Provides a mechanism for releasing unmanaged resources.

    {
        private BooksContext _context;

        // we want these methods to access DB using EF, so we need the book context injected
        public BooksRepository(BooksContext booksContext)
        {
            _context = booksContext ?? throw new ArgumentNullException(nameof(booksContext)); // in case context is null throw exception
        }

        public async Task<Book> GetBookAsync(Guid bookId)
        {
            //await _context.Database.ExecuteSqlCommandAsync("WAITFOR DELAI '00:00:02';");

            return await _context.Books.Include(a => a.Author).FirstOrDefaultAsync(b => b.Id == bookId);
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            //return _context.Books.ToList();
            return await _context.Books.Include(a => a.Author).ToListAsync()/* it has .GetAwaiter()*/;
        }

        /*
         * virtual：This method can be override by its sub classes。
         * public：This method can be accessed by instance of the class
         * protected：This method can be only accessed by the class itself，or can be accessed by the inherited class，it cannot be accessed directly through the sub class instance
         */

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // ensures that CLR does not call finalize for our repository - telling GC that this repo has already been cleaned
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }

        // synchronous
        public IEnumerable<Book> GetBooks()
        {
            // simulate long running operation
            //_context.Database.ExecuteSqlCommand("WAITFOR DELAI '00:00:02';");

            return _context.Books.Include(a => a.Author).ToList();
        }

        public Book GetBook(Guid bookId)
        {
            return _context.Books.Include(a => a.Author).FirstOrDefault(b => b.Id == bookId);
        }

        public async Task AddBookAsync(Book bookToAdd)
        {
            if (bookToAdd == null)
            {
                throw new ArgumentNullException(nameof(bookToAdd));
            }
            await _context.AddAsync(bookToAdd); // this means it's added to the DbSet not to the DB, so the Async is only usefull when autogen id to the DB - this is no IO bound opperation
        }
    }
}
