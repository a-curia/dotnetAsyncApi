using Books.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Api.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase // will enforce the attribute base routing, present on netcore 2.1>; convention based routing is ment for mvc not for apis
    {
        private IBooksRepository _booksRepository;

        // inject the repository
        public BooksController(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository ?? throw new ArgumentNullException(nameof(booksRepository));
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var bookEntities = await _booksRepository.GetBooksAsync();

            return Ok(bookEntities);
        }

        [HttpGet]
        [Route("{bookId}")]
        public async Task<IActionResult> GetBook(Guid bookId)
        {
            var bookEntity = await _booksRepository.GetBookAsync(bookId);

            if (bookEntity == null)
            {
                return NotFound();
            }

            return Ok(bookEntity);
        }


    }
}
