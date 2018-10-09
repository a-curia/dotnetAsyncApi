using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Api.Contexts;
using Books.Api.Entities;

namespace Books.Api.Services
{
    public class BooksRepository : IBooksRepository
    {
        private BooksContext _context;

        // we want these methods to access DB using EF, so we need the book context injected
        public BooksRepository(BooksContext booksContext)
        {
            _context = booksContext ?? throw new ArgumentNullException(nameof(booksContext)); // in case context is null throw exception
        }
        public Task<Book> GetBookAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Book>> GetBooksAsync()
        {
            throw new NotImplementedException();
        }
    }
}
