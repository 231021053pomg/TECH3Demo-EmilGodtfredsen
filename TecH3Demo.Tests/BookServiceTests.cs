using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecH3Demo.API.Database;
using TecH3Demo.API.Domain;
using TecH3Demo.API.Repositories;
using TecH3Demo.API.Services;
using Xunit;

namespace TecH3Demo.Tests
{
    public class BookServiceTests
    {
        private readonly BookService _sut;
        private readonly Mock<IBookRepository> _bookRepositoryMock = new();

        public BookServiceTests()
        {
            _sut = new BookService(_bookRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllBooks_ShouldReturnListOfAuthors_IfListExists()
        {
            // Arrange

            List<Book> mockBookList = new List<Book>
            {
                new Book{Id = 1, Title = "Metallica", Published = DateTime.Today, AuthorId = 1},
                new Book{Id = 2, Title = "Def Leppard", Published = DateTime.Today, AuthorId = 2},
                new Book{Id = 3, Title = "Steel Panther", Published = DateTime.Today, AuthorId = 3}
            };
            _bookRepositoryMock
                .Setup(x => x.GetAll())
                .ReturnsAsync(mockBookList);

            // Act

            var bookList = await _sut.GetAllBooks();
            var localId = 1;

            // Assert

            Assert.NotNull(bookList);

            Assert.Equal(mockBookList.Count, bookList.Count);
            Assert.Equal(localId, bookList[0].Id);
        }

        [Fact]
        public async Task GetOneBookById_ShouldReturnBook_IfBookExists()
        {
            // Arrange

            Book mockBook = new Book
            {
                Id = 1,
                Title = "Metallica",
                Published = DateTime.Today,
                AuthorId = 1
            };

            _bookRepositoryMock
                .Setup(x => x.GetById(mockBook.Id))
                .ReturnsAsync(mockBook);

            // Act

            var book = await _sut.GetBookById(mockBook.Id);

            // Assert

            Assert.NotNull(book);

            Assert.Equal(mockBook, book);

        }
        [Fact]
        public async Task GetBookById_ShouldReturnNull_IfBookDoesNotExist()
        {
            // Arrange
            _bookRepositoryMock
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var book = await _sut.GetBookById(123);

            // Assert

            Assert.Null(book);
        }

        [Fact]
        public async Task CreateBook_ShouldReturnCreatedBook_IfBookIsSuccessfullyCreated()
        {
            int id = 1;
            string title = "Harry Pothead";
            DateTime published = new DateTime(2020, 12, 12);
            int authorId = 1;

            // Arrange

            Book mockBook = new Book
            {
                Id = id,
                Title = title,
                Published = published,
                AuthorId = authorId,
                CreatedAt = DateTime.Now
            };
            _bookRepositoryMock
                .Setup(x => x.Create(mockBook))
                .ReturnsAsync(mockBook);

            // Act
            var book = await _sut.CreateBook(mockBook);

            // Assert

            Assert.NotNull(book);
            Assert.NotEqual(DateTime.MinValue, book.CreatedAt);
            Assert.Equal(title, book.Title);
        }

        [Fact]
        public async Task UpdateBook_ShouldReturnDifferentUpdatedAt_IfSuccessfullyUpdated()
        {
            int id = 1;
            string title = "Harry Pothead";
            DateTime published = new DateTime(2020, 12, 12);
            int authorId = 2;

            int updateId = 2;

            // Arrange

            Book mockBook = new Book
            {
                Id = id,
                Title = title,
                Published = published,
                AuthorId = authorId,
                UpdatedAt = DateTime.Now
            };
            _bookRepositoryMock
                .Setup(x => x.Update(updateId, mockBook))
                .ReturnsAsync(mockBook);

            // Act
            var book = await _sut.UpdateBook(updateId, mockBook);

            // Assert

            Assert.NotNull(book);
            Assert.NotEqual(DateTime.MinValue, book.UpdatedAt);

        }

        [Fact]
        public async Task DeleteBook_ShouldReturnDifferentDeletedAt_IfSuccesssfullyDeleted()
        {
            int id = 1;
            string title = "Harry Pothead";
            DateTime published = new DateTime(2020, 12, 12);
            int authorId = 1;

            int deleteId = 1;
            // Arrange

            Book mockBook = new Book
            {
                Id = id,
                Title = title,
                Published = published,
                AuthorId = authorId,
                DeletedAt = DateTime.Now
            };
            _bookRepositoryMock
                .Setup(x => x.Delete(deleteId))
                .ReturnsAsync(mockBook);

            // Act
            var book = await _sut.DeleteBook(deleteId);

            // Assert

            Assert.NotNull(book);

            Assert.NotEqual(DateTime.MinValue, book.DeletedAt);

        }







    }
}
