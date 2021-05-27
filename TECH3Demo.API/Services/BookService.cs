using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TecH3Demo.API.Domain;
using TecH3Demo.API.Repositories;

namespace TecH3Demo.API.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task<List<Book>> GetAllBooks()
        {
            var books = await _bookRepository.GetAll();
            return books;
        }

        public async Task<Book> GetBookById(int id)
        {
            var book = await _bookRepository.GetById(id);
            return book;

        }
        public async Task<Book> CreateBook(Book book)
        {
            var createBook = await _bookRepository.Create(book);
            return createBook;
        }
        public async Task<Book> UpdateBook(int id, Book book)
        {
            var updateBook = await _bookRepository.Update(id, book);
            return updateBook;
        }

        public async Task<Book> DeleteBook(int id)
        {
            var book = await _bookRepository.Delete(id);
            return book;
        }

    }
}
