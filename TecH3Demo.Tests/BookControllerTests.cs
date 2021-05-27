using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecH3Demo.API.Controllers;
using TecH3Demo.API.Domain;
using TecH3Demo.API.Services;
using Xunit;

namespace TecH3Demo.Tests
{
    public class BookControllerTests
    {
        private readonly BookController _sut;
        private readonly Mock<IBookService> _bookServiceMock = new();

        public BookControllerTests()
        {
            _sut = new BookController(_bookServiceMock.Object);
        }

        [Fact]
        public async Task GetAllBooks_ShouldReturnStatusCode200Ok_IfDataIsPresent()
        {
            // Arrange
            List<Book> books = new List<Book>();
            books.Add(new Book());
            books.Add(new Book());

            _bookServiceMock
                .Setup(x => x.GetAllBooks())
                .ReturnsAsync(books);

            // Act
            var res = await _sut.GetAll();

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)res;

            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task GetAllBooks_ShouldReturnStatusCode204NoContent_WhenNoDataIsPresent()
        {
            // Arrange
            List<Book> books = new List<Book>();
            _bookServiceMock
               .Setup(x => x.GetAllBooks())
               .ReturnsAsync(books);

            // Act
            var res = await _sut.GetAll();

            // Assert

            var statusCodeResult = (IStatusCodeActionResult)res;

            Assert.Equal(204, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task GetAllBooks_ShouldReturnStatusCode500InternalServerError_WhenDataIsNull()
        {
            // Arrange
            List<Book> books = null;
            _bookServiceMock
               .Setup(x => x.GetAllBooks())
               .ReturnsAsync(books);

            // Act
            var res = await _sut.GetAll();

            // Assert

            var statusCodeResult = (IStatusCodeActionResult)res;

            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task GetBookById_ShouldReturnStatusCode200Ok_WhenTheAuthorExists()
        {
            // Arrange
            Book mockBook = new Book
            {
                Id = 1,
                Title = "Harry Potter",
                Published = DateTime.Now,
                AuthorId = 1,
            };
            _bookServiceMock
               .Setup(x => x.GetBookById(mockBook.Id))
               .ReturnsAsync(mockBook);

            // Act
            var res = await _sut.Get(mockBook.Id);

            // Assert

            var statusCodeResult = (IStatusCodeActionResult)res;

            Assert.Equal(200, statusCodeResult.StatusCode);
        }
        [Fact]
        public async Task GetBookById_ShouldReturnStatusCode404NotFound_WhenAuthorIsNull()
        {
            // Arrange
            Book mockBook = new Book
            {
                Id = 1,
                Title = "Harry Potter",
                Published = DateTime.Now,
                AuthorId = 1,
            };
            _bookServiceMock
               .Setup(x => x.GetBookById(mockBook.Id))
               .ReturnsAsync(mockBook);

            // Act

            var res = await _sut.Get(2); // Calls Get() with non existing id

            // Assert

            var statusCodeResult = (IStatusCodeActionResult)res;

            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task CreateBook_ShouldReturnStatusCode200Ok_WhenAuthorSuccessfullyCreated()
        {
            // Arrange
            Book mockBook = new Book
            {
                Id = 1,
                Title = "Harry Potter",
                Published = DateTime.Now,
                AuthorId = 1,
            };
            _bookServiceMock
               .Setup(x => x.CreateBook(mockBook))
               .ReturnsAsync(mockBook);

            // Act

            var res = await _sut.Create(mockBook);

            // Assert

            var statusCodeResult = (IStatusCodeActionResult)res;

            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task CreateBook_ShouldReturnStatusCode400BadRequest_WhenAuthorIsNull()
        {
            // Arrange
            Book mockBook = null;

            _bookServiceMock
                .Setup(x => x.CreateBook(mockBook))
                .ReturnsAsync(mockBook);

            // Act

            var res = await _sut.Create(mockBook);

            // Assert

            var statusCodeResult = (IStatusCodeActionResult)res;

            Assert.Equal(400, statusCodeResult.StatusCode);
        }
        [Fact]
        public async Task UpdateBook_ShouldReturnStatusCode200Ok_WhenAuthorSuccesfullyUpdated()
        {
            int id = 1;
            string title = "Harry Potter";
            DateTime published = DateTime.Now;
            int authorId = 1;

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

            _bookServiceMock
                .Setup(x => x.UpdateBook(updateId, mockBook))
                .ReturnsAsync(mockBook);

            // Act

            var res = await _sut.Update(updateId, mockBook);

            // Assert

            var statusCodeResult = (IStatusCodeActionResult)res;

            Assert.Equal(200, statusCodeResult.StatusCode);
        }
        [Fact]
        public async Task UpdateBook_ShouldReturnStatusCode404BadRequest_WhenAuthorIsNull()
        {
            int updateId = 2;
            // Arrange
            Book mockBook = null;

            _bookServiceMock
                .Setup(x => x.UpdateBook(updateId, mockBook))
                .ReturnsAsync(mockBook);

            // Act

            var res = await _sut.Update(updateId, mockBook);

            // Assert

            var statusCodeResult = (IStatusCodeActionResult)res;

            Assert.Equal(404, statusCodeResult.StatusCode);
        }
        [Fact]
        public async Task DeleteBook_ShouldReturnStatusCode200Ok_WhenAuthorSuccesfullyDeleted()
        {
            int id = 1;
            string title = "Harry Potter";
            DateTime published = DateTime.Now;
            int authorId = 1;

            int deleteId = 2;

            // Arrange
            Book mockBook = new Book
            {
                Id = id,
                Title = title,
                Published = published,
                AuthorId = authorId,
                UpdatedAt = DateTime.Now
            };

            _bookServiceMock
                .Setup(x => x.DeleteBook(deleteId))
                .ReturnsAsync(mockBook);

            // Act

            var res = await _sut.Delete(deleteId);

            // Assert

            var statusCodeResult = (IStatusCodeActionResult)res;

            Assert.Equal(200, statusCodeResult.StatusCode);
        }
        [Fact]
        public async Task DeleteBook_ShouldReturnStatusCode404NotFound_WhenAuthorIsNull()
        {
            int deleteId = 2;

            // Arrange
            Book mockBook = null;


            _bookServiceMock
                .Setup(x => x.DeleteBook(deleteId))
                .ReturnsAsync(mockBook);

            // Act

            var res = await _sut.Delete(deleteId);

            // Assert

            var statusCodeResult = (IStatusCodeActionResult)res;

            Assert.Equal(404, statusCodeResult.StatusCode);
        }
    }
}
