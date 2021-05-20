using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Moq;
using System.Threading.Tasks;
using TecH3Demo.API.Services;
using TecH3Demo.API.Repositories;
using TecH3Demo.API.Domain;

namespace TecH3Demo.Tests

{
    public class AuthorServiceTests
    {
        private readonly AuthorService _sut;
        private readonly Mock<IAuthorRepository> _authorRepositoryMock = new();

        public AuthorServiceTests()
        {
            _sut = new AuthorService(_authorRepositoryMock.Object);
        }
        // Example test for force return of NULL
        [Fact]
        public async Task GetById_ShouldReturnNullIfAuthorDoesNotExist()
        {
            // Arrange
            _authorRepositoryMock
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);
            // Act
            var author = await _sut.GetAuthorById(123);
            // Assert
            Assert.Null(author);
        }
        // Example test for returning data
        [Fact]
        public async Task GetById_ShouldReturnAuthor_IfAuthorExists()
        {
            // Arrange
            Author mockAuthor = new Author
            {
                Id = 1,
                FirstName = "Marshall",
                LastName = "Mathers"
            };

            _authorRepositoryMock
                .Setup(x => x.GetById(mockAuthor.Id))
                .ReturnsAsync(mockAuthor);

            // Act

            var author = await _sut.GetAuthorById(mockAuthor.Id);

            // Assert

            Assert.NotNull(author);
            Assert.Equal(mockAuthor, author);

        }

        [Fact]
        public async Task GetAllAuthorsList_ShouldReturnListOfAuthors_IfListOfAuthorsExists()
        {
            // Arrange
            List<Author> mockAuthorList = new List<Author>
            {
                  new Author{Id = 1, FirstName = "Eric", LastName = "Sherman"},
                  new Author{Id = 2, FirstName = "David", LastName = "Schwimmer"},
                  new Author{Id = 3, FirstName = "Jim", LastName = "Daggerthuggert"}
            };

            _authorRepositoryMock
               .Setup(x => x.GetAll())
               .ReturnsAsync(mockAuthorList);

            // Act

            var authorList = await _sut.GetAllAuthors();
            var localId = 1;

            // Assert

            Assert.NotNull(authorList);

            Assert.Equal(mockAuthorList.Count, authorList.Count);
           Assert.Equal(localId, authorList[0].Id);
        }
    }
}
