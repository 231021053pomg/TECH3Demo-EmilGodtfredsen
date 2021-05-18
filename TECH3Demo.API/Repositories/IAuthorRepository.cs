using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TECH3Demo.API.Domain;

namespace TECH3Demo.API.Repositories
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAll();
        Task<Author> GetById(int id);
        Task<Author> Create(Author author);
        Task<Author> Update(int id, Author author);
        Task<Author> Delete(int id);
    }
}
