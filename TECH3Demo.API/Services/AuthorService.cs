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

        public AuthorService(IAuthorRepository authorRepository)
        {
            __authorRepository = authorRepository;
        }

        public async Task<List<Author>> GetAllAuthors()
        {
            var authors = await __authorRepository.GetAll();
            return authors;
        }
        public async Task<Author> GetAuthorById(int id)
        {
            var author = await __authorRepository.GetById(id);
            return author;
        }
        public async Task<Author> CreateAuthor(Author author)
        {
            var createAuthor = await __authorRepository.Create(author);
            return createAuthor;
        }
        public async Task<Author> UpdateAuthor(int id, Author author)
        {
           var updateAuthor = await __authorRepository.Update(id, author);
            return updateAuthor;
        }
        public Task<Author> DeleteAuthor(int id)
        {
            var author = __authorRepository.Delete(id);
            return author;
        }


    }
}
