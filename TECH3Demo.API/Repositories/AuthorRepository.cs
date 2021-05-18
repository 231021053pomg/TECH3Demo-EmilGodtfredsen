using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TECH3Demo.API.Domain;
using TECH3Demo.API.Database;
using Microsoft.EntityFrameworkCore;

namespace TECH3Demo.API.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {

        private readonly TechH3DemoDbContext _context;
        public AuthorRepository(TechH3DemoDbContext context)
        {
            _context = context;
        }
        public async Task<List<Author>> GetAll()
        {
            return await _context.Authors
                .Where(a => a.DeletedAt == null)
                .ToListAsync();
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
