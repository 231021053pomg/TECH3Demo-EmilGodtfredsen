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
    public class AuthorControllerTests
    {
        private readonly AuthorController _sut;
        private readonly Mock<IAuthorService> _authorServiceMock = new();

        public AuthorControllerTests()
        {
            _sut = new AuthorController(_authorServiceMock.Object);
        }

        [Fact]
        public async Task GetAllAuthors_ShouldReturnStatusCode200Ok_IfDataIsPresent()
        {
            // Arrange
            List<Author> authors = new List<Author>();
            authors.Add(new Author());
            authors.Add(new Author());

            _authorServiceMock
                .Setup(x => x.GetAllAuthors())
                .ReturnsAsync(authors);

            // Act
            var res = await _sut.GetAll();

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)res;

            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task GetAllAuthors_ShouldReturnStatusCode204NoContent_WhenNoDataIsPresent()
        {
            // Arrange
            List<Author> authors = new List<Author>();
            _authorServiceMock
               .Setup(x => x.GetAllAuthors())
               .ReturnsAsync(authors);

            // Act
            var res = await _sut.GetAll();

            // Assert

            var statusCodeResult = (IStatusCodeActionResult)res;

            Assert.Equal(204, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task GetAllAuthors_ShouldReturnStatusCode500InternalServerError_WhenDataIsNull()
        {
            // Arrange
            List<Author> authors = null;
            _authorServiceMock
               .Setup(x => x.GetAllAuthors())
               .ReturnsAsync(authors);

            // Act
            var res = await _sut.GetAll();

            // Assert

            var statusCodeResult = (IStatusCodeActionResult)res;

            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task GetAuthorById_ShouldReturnStatusCode200Ok_WhenTheAuthorExists()
        {
            // Arrange
            Author mockAuthor = new Author
            {
                Id = 1,
                FirstName = "Emil",
                LastName = "Godtfredsen"
            };
            _authorServiceMock
               .Setup(x => x.GetAuthorById(mockAuthor.Id))
               .ReturnsAsync(mockAuthor);

            // Act
            var res = await _sut.Get(mockAuthor.Id);

            // Assert

            var statusCodeResult = (IStatusCodeActionResult)res;

            Assert.Equal(200, statusCodeResult.StatusCode);
        }
        [Fact]
        public async Task GetAuthorById_ShouldReturnStatusCode404NotFound_WhenAuthorIsNull()
        {
            // Arrange
            Author mockAuthor = new Author
            {
                Id = 1,
                FirstName = "Emil",
                LastName = "Godtfredsen"
            };
            _authorServiceMock
               .Setup(x => x.GetAuthorById(mockAuthor.Id))
               .ReturnsAsync(mockAuthor);

            // Act

            var res = await _sut.Get(2); // Calls Get() with non existing id

            // Assert

            var statusCodeResult = (IStatusCodeActionResult)res;

            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task CreateAuthor_ShouldReturnStatusCode200Ok_WhenAuthorSuccessfullyCreated()
        {
            // Arrange
            Author mockAuthor = new Author
            {
                Id = 1,
                FirstName = "Emil",
                LastName = "Godtfredsen"
            };
            _authorServiceMock
               .Setup(x => x.CreateAuthor(mockAuthor))
               .ReturnsAsync(mockAuthor);

            // Act

            var res = await _sut.Create(mockAuthor);

            // Assert

            var statusCodeResult = (IStatusCodeActionResult)res;

            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task CreateAuthor_ShouldReturnStatusCode400BadRequest_WhenAuthorIsNull()
        {
            // Arrange
            Author mockAuthor = null;

            _authorServiceMock
                .Setup(x => x.CreateAuthor(mockAuthor))
                .ReturnsAsync(mockAuthor);

            // Act

            var res = await _sut.Create(mockAuthor);

            // Assert

            var statusCodeResult = (IStatusCodeActionResult)res;

            Assert.Equal(400, statusCodeResult.StatusCode);
        }
        [Fact]
        public async Task UpdateAuthor_ShouldReturnStatusCode200Ok_WhenAuthorSuccesfullyUpdated()
        {
            int id = 1;
            string firstName = "Jim";
            string lastName = "Daggerthuggert";

            int updateId = 2;
            // Arrange
            Author mockAuthor = new Author
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                UpdatedAt = DateTime.Now
            };

            _authorServiceMock
                .Setup(x => x.UpdateAuthor(updateId, mockAuthor))
                .ReturnsAsync(mockAuthor);

            // Act

            var res = await _sut.Update(updateId, mockAuthor);

            // Assert

            var statusCodeResult = (IStatusCodeActionResult)res;

            Assert.Equal(200, statusCodeResult.StatusCode);
        }
        [Fact]
        public async Task UpdateAuthor_ShouldReturnStatusCode500InternalServerError_WhenAuthorIsNull()
        {
            int updateId = 2;
            // Arrange
            Author mockAuthor = null;

            _authorServiceMock
                .Setup(x => x.UpdateAuthor(updateId, mockAuthor))
                .ReturnsAsync(mockAuthor);

            // Act

            var res = await _sut.Update(updateId, mockAuthor);

            // Assert

            var statusCodeResult = (IStatusCodeActionResult)res;

            Assert.Equal(500, statusCodeResult.StatusCode);
        }
        [Fact]
        public async Task DeleteAuthor_ShouldReturnStatusCode200Ok_WhenAuthorSuccesfullyDeleted()
        {
            int id = 1;
            string firstName = "Jim";
            string lastName = "Daggerthuggert";

            int deleteId = 2;

            // Arrange
            Author mockAuthor = new Author
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                DeletedAt = DateTime.Now
            };

            _authorServiceMock
                .Setup(x => x.DeleteAuthor(deleteId))
                .ReturnsAsync(mockAuthor);

            // Act

            var res = await _sut.Delete(deleteId);

            // Assert

            var statusCodeResult = (IStatusCodeActionResult)res;

            Assert.Equal(200, statusCodeResult.StatusCode);
        }
        [Fact]
        public async Task DeleteAuthor_ShouldReturnStatusCode404NotFound_WhenAuthorIsNull()
        {
            int deleteId = 2;

            // Arrange
            Author mockAuthor = null;
           

            _authorServiceMock
                .Setup(x => x.DeleteAuthor(deleteId))
                .ReturnsAsync(mockAuthor);

            // Act

            var res = await _sut.Delete(deleteId);

            // Assert

            var statusCodeResult = (IStatusCodeActionResult)res;

            Assert.Equal(404, statusCodeResult.StatusCode);
        }
    }
}
