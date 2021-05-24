using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TecH3Demo.API.Database;
using TecH3Demo.API.Domain;

namespace TecH3Demo.API.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly TechH3DemoDbContext _context;

        public BookRepository(TechH3DemoDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAll()
        {
            return await _context.Books
              .Where(b => b.DeletedAt == null)
              .OrderBy(b => b.Title)
              .ToListAsync();
        }

        public async Task<Book> GetById(int id)
        {
            return await _context.Books
                .Where(b => b.DeletedAt == null)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book> Create(Book book)
        {
            book.CreatedAt = DateTime.Now;
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book> Update(Book book, int id)
        {
            var updateBook = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if(updateBook != null)
            {
                updateBook.UpdatedAt = DateTime.Now;
                updateBook.Title = book.Title;
                updateBook.Published = book.Published;
                _context.Books.Update(updateBook);
                await _context.SaveChangesAsync();
            }
            return updateBook;
        }
        public async Task<Book> Delete(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if(book != null)
            {
                book.DeletedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            return book;
        }
    }
}
