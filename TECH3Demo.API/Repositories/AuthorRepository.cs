using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TECH3Demo.API.Domain;

namespace TECH3Demo.API.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        public Task<List<Author>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Author> GetById(int id)
        {
            throw new NotImplementedException();
        }
        public Task<Author> Create(Author author)
        {
            throw new NotImplementedException();
        }

        public Task<Author> Update(int id, Author author)
        {
            throw new NotImplementedException();
        }

        public Task<Author> Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}
