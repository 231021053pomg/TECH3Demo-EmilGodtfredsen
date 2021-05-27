using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TecH3Demo.API.Domain;
using TecH3Demo.API.Database;
using Microsoft.EntityFrameworkCore;

namespace TecH3Demo.API.Repositories
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
                .OrderBy(a => a.FirstName)
                .OrderBy(a => a.LastName)
                .Include(a => a.Books.Where(b => b.DeletedAt == null))
                .ToListAsync();
        }

        public async Task<Author> GetById(int id)
        {
            return await _context.Authors
                .Where(a => a.DeletedAt == null)
                .Include(a => a.Books.Where(b => b.DeletedAt == null))
                .FirstOrDefaultAsync(a => a.Id == id);
                
        }
        public async Task<Author> Create(Author author)
        {
            author.CreatedAt = DateTime.Now;
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<Author> Update(int id, Author author)
        {
            var updateAuthor = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
            if(updateAuthor != null)
            {
                updateAuthor.UpdatedAt = DateTime.Now; // Sets flag as current datetime for updated at
                updateAuthor.FirstName = author.FirstName;
                updateAuthor.LastName = author.LastName;
                _context.Authors.Update(updateAuthor);
                await _context.SaveChangesAsync();
            }
            return updateAuthor;
        }

        public async Task<Author> Delete(int id)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
            if (author != null)
            {
                author.DeletedAt = DateTime.Now; // Sets flag as current datetime for "soft delete"
                await _context.SaveChangesAsync();
            }
            return author;
        }

    }
}
