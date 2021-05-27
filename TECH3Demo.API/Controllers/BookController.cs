using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TecH3Demo.API.Domain;
using TecH3Demo.API.Services;

namespace TecH3Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;

        }

        // GET ALL /api/book

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetAll()
        {
            try
            {
                var books = await _bookService.GetAllBooks();
                if (books == null)
                {
                    return Problem("Returned null, unexpected behavior.");
                }
                else if (books.Count == 0)
                {
                    return NoContent();
                }
                return Ok(books);

            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        // GET by id /api/book

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Get([FromRoute] int id)
        {
            try
            {
                var book = await _bookService.GetBookById(id);
                if (book == null)
                {
                    return NotFound();
                }
                return Ok(book);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        // POST /api/book

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Create(Book book)
        {
            try
            {
                if (book == null)
                {
                    return BadRequest("Creation of Author failed.");
                }
                var newBook = await _bookService.CreateBook(book);
                return Ok(newBook);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        // PUT /api/book

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Update([FromRoute] int id, Book book)
        {
            try
            {
                var updateBook = await _bookService.UpdateBook(id, book);

                if (updateBook == null)
                {
                    return NotFound("Editing of book not possible, Book = null.");
                }
                return Ok(updateBook);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }


        // DELETE /api/book

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var deleteBook = await _bookService.DeleteBook(id);

                if (deleteBook == null)
                {
                    return NotFound("Book with id: " + id + " does not exist");
                }
                return Ok(deleteBook);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}
