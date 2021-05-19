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

            _context.Authors.Add(new Author
            {

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

        [Fact]
        public async Task GetAuthorById_ShouldReturnAuthor_WhenAuthorExists()
        {

            // Arrange
            AuthorRepository authorRepository = new AuthorRepository(_context);

            // Act
            var author = await authorRepository.GetById(1);

            // Assert
            Assert.NotNull(author);

            Assert.Equal(1, author.Id);

            Assert.Equal("Emil", author.FirstName);

            Assert.Equal("Godtfredsen", author.LastName);
        }

        [Fact]
        public async Task CreateAuthor_ShouldReturnNewAuthor_WhenCreatedAtIsNotEqualToDefaultDateTimeValue()
        {

            // Arrange
            AuthorRepository authorRepository = new AuthorRepository(_context);

            // Act
            var author = await authorRepository.Create(new Author
            {
                Id = 6,
                FirstName = "Jim",
                LastName = "DaggertHuggert"
            });

            // Assert
            Assert.NotNull(author);

            Assert.NotEqual(DateTime.MinValue, author.CreatedAt);

        }

        [Fact]
        public async Task UpdateAuthor_ShouldReturnAuthor_WhenUpdatedAtIsNotEqualToDefaultDateTimeValue()
        {
            // Arrange
            AuthorRepository authorRepository = new AuthorRepository(_context);

            // Act
            var author = await authorRepository.Update(2, new Author
            {
                FirstName = "Egon",
                LastName = "Olsen"
            });

            // Assert

            Assert.NotNull(author);

            Assert.NotEqual(DateTime.MinValue, author.UpdatedAt);
        }

        [Fact]
        public async Task DeleteAuthor_ShouldReturnAuthor_WhenDeletedAtIsNotEqualToDefaultDateTimeValue()
        {
            // Arrange 
            AuthorRepository authorRepository = new AuthorRepository(_context);

            // Act 
            var author = await authorRepository.Delete(2);

            //Assert
            Assert.NotNull(author);

            Assert.NotEqual(DateTime.MinValue, author.DeletedAt);
        }

    }
}
