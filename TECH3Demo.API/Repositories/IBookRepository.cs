using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TecH3Demo.API.Domain;

namespace TecH3Demo.API.Repositories
{
    interface IBookRepository
    {
        Task<List<Book>> GetAll();
        Task<Book> GetById(int id);
        Task<Book> Create(Book book);
        Task<Book> Update(Book book, int id);
        Task<Book> Delete(int id);
    }
}
