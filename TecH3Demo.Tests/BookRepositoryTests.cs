using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecH3Demo.API.Database;
using TecH3Demo.API.Domain;
using TecH3Demo.API.Repositories;
using Xunit;

namespace TecH3Demo.Tests
{
    public class BookRepositoryTests
    {
        private readonly DbContextOptions<TechH3DemoDbContext> _options;
        private readonly TechH3DemoDbContext _context;

        public BookRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<TechH3DemoDbContext>()
                .UseInMemoryDatabase(databaseName: "BooksDatabase")
                .Options;

            _context = new TechH3DemoDbContext(_options);

            _context.Database.EnsureDeleted();

            _context.Books.Add(new Book
            {
                Id = 1,
                Title = "Brigde over river Kwai",
                Published = DateTime.Now
            });

            _context.Books.Add(new Book
            {
                Id = 2,
                Title = "Harry Pothead",
                Published = DateTime.Now
            });

            _context.Books.Add(new Book
            {
                Id = 3,
                Title = "A history of violence",
                Published = DateTime.Now
            });

            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAllBooks_ShouldReturnAllThreeBooks_IfThreeBooksExist()
        {
            // Arrange
            BookRepository bookRepository = new BookRepository(_context);

            // Act 
            var books = await bookRepository.GetAll();

            // Assert
            Assert.NotNull(books);

            Assert.Equal(3, books.Count);
        }
        [Fact]
        public async Task GetBookById_ShouldReturnBook_WhenBookExist()
        {

            // Arrange
            BookRepository bookRepository = new BookRepository(_context);

            // Act
            var book = await bookRepository.GetById(1);

            // Assert
            Assert.NotNull(book);

            Assert.Equal(1, book.Id);

            Assert.Equal("Brigde over river Kwai", book.Title);

        }
        [Fact]
        public async Task CreateBook_ShouldReturnNewBook_WhenCreatedAtIsNotEqualToDatetimeMinValue()
        {
            // Arrange

            BookRepository bookRepository = new BookRepository(_context);

            // Act

            var book = await bookRepository.Create(new Book
            {
                Title = "Karate Kid",
                Published = DateTime.Now,
                CreatedAt = DateTime.Now
            });

            // Assert 

            Assert.NotNull(book);

            Assert.NotEqual(DateTime.MinValue, book.CreatedAt);
        }

        [Fact]
        public async Task UpdateBook_ShouldReturnBook_WhenUpdatedAtIsNotEqualToDatetimeMinValue()
        {
            // Arrange

            BookRepository bookRepository = new BookRepository(_context);

            // Act

            var book = await bookRepository.Update(2, new Book
            {
                Title = "",
                Published = DateTime.Now
            });

            // Assert

            Assert.NotNull(book);

            Assert.NotEqual(DateTime.MinValue, book.UpdatedAt);
        }

        [Fact]
        public async Task DeleteBook_ShouldReturnBook_WhenDeletedAtIsNotEqualToDatetimeMinValue()
        {
            // Arrange

            BookRepository bookRepository = new BookRepository(_context);

            // Act

            var book = await bookRepository.Delete(2);

            // Assert

            Assert.NotNull(book.DeletedAt);

        }
    }

}
