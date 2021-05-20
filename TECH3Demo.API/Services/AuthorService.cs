using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TecH3Demo.API.Domain;
using TecH3Demo.API.Repositories;

namespace TecH3Demo.API.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository __authorRepository;
        public Task<List<Author>> GetAllAuthors()
        {
            throw new NotImplementedException();
        }

        public Task<Author> GetAuthorById(int id)
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
