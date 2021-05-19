using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using TecH3Demo.API.Database;
using TecH3Demo.API.Repositories;
using Microsoft.EntityFrameworkCore;
using TecH3Demo.API.Domain;

namespace TecH3Demo.Tests
{
    public class AuthorRepositoryTests
    {
        private readonly DbContextOptions<TechH3DemoDbContext> _options;
        private readonly TechH3DemoDbContext _context;

        public AuthorRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<TechH3DemoDbContext>()
                .UseInMemoryDatabase(databaseName: "AuthorsDatabase")
                .Options;

            _context = new TechH3DemoDbContext(_options);

            _context.Database.EnsureDeleted();

            _context.Authors.Add(new Author { 

                Id = 1,
                FirstName = "Emil",
                LastName = "Godtfredsen"
            
            });

            _context.Authors.Add(new Author
            {

                Id = 2,
                FirstName = "Jeff",
                LastName = "Bezos"

            });

            _context.Authors.Add(new Author
            {

                Id = 3,
                FirstName = "Elon",
                LastName = "Musk"

            });

            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAllAuthors_ShouldReturnAllAuthors_WhenAuthorsExist()
        {
            // Arrange
            AuthorRepository authorRepository = new AuthorRepository(_context);

            // Act
            var authors = await authorRepository.GetAll();

            //Assert
            Assert.NotNull(authors);

            Assert.Equal(3, authors.Count);
        }
       
    }
}
