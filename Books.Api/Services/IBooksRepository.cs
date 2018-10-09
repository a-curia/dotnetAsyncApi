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
        IEnumerable<Entities.Book> GetBooks();
        Book GetBook(Guid bookId);

        // asynchronous
        Task<IEnumerable<Entities.Book>> GetBooksAsync();
        Task<Book> GetBookAsync(Guid bookId);

        Task AddBookAsync(Entities.Book bookToAdd);

    }
}
