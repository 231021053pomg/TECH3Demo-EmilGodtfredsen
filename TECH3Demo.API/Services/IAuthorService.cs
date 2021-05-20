using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TecH3Demo.API.Domain;

namespace TecH3Demo.API.Services
{
    public interface IAuthorService
    {
        Task<List<Author>> GetAllAuthors();
        Task<Author> GetAuthorById(int id);
        Task<Author> Create(Author author);
        Task<Author> Update(int id, Author author);

        Task<Author> Delete(int id);

    }
}
