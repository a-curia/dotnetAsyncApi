using Books.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Api.Services
{
    public interface IBooksRepository
    {

        // synchronous
        IEnumerable<Book> GetBooks();
        Book GetBook(Guid bookId);

        // asynchronous
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<Book> GetBookAsync(Guid bookId);

    }
}
