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
        public Task<Book> Create(Book book)
        {
            throw new NotImplementedException();
        }

        public Task<Book> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Book>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Book> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Book> Update(Book book, int id)
        {
            throw new NotImplementedException();
        }
    }
}
