using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TecH3Demo.API.Domain;

namespace TecH3Demo.API.Repositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAll();
        Task<Book> GetById(int id);
        Task<Book> Create(Book book);
        Task<Book> Update(int id, Book book);
        Task<Book> Delete(int id);
    }
}
