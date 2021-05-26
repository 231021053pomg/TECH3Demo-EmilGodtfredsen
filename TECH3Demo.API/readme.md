# Onsdag 26/05-21
	
	* Implemented create author functionality
	* Implemented delete author functionality
	* Implemented update author functionality
	* Created CORS policy inside startup.cs file
	* Implenented GetById (Consol.Log(response.data))
	* Finished testing BookRepository

# Tirsdag 25/05-21

	* Created book repository
	* Implement first couple of tests on book repository
	* Created React app
	* Created GetAll authors displayd in browser

# Fredag 21/05-21

	* Implemented Author controller
	* Implemented full CRUD on author controller, with POST, GET/GET("{id}"), PUT, DELETE
	* Implemented test with Moq on author controller
	* Tested is Http responds with the right status codes ex. (200 Ok, 204 No Content, 404 Not Found, 500 Internal Server Error)

# Torsdag 20/05-21

	* Summed up testing of author repo
	* Refactored test for Update and Delete on author service test
	* Implemented IAuthorService
	* Implemented AuthorService
	* Implemented test on AuthorService GetByAuthorId
	* Implemented test on AuthorService GetAllAuthors
	* Implemented test on AuthorService CreateAuthor
	* Implemented test on AuthorService UpdateAuthor
	* Implemented test on AuthorService DeleteAuthor

# Onsdag 19/05-21

	* Created test project 
	* Referenced test project to API project
	* Tested simple 1+1 equals 2
	* Tested simple 1+2 equals 3
	* Tested author table with dummy data
	* Tested GetAll() authors
	* Tested GetAuthor(id)
	* Created Test for Create, Update and Delete Author. Validation on the methods: CreatedAt, UpdatedAt and DeletedAt, with Assert.NotEqual(default Datetime, new author.value).

# Tirsdag 18/05-21

	* Created Domain/Model folder
	* Created Author and Book Model
	* Inherrited IAuthorRepository to class AuthorRepository
	* Created DbContext (TechH3DemoDbContext)
	* First DB Migration, Initial

# Mandag 17/05-21

	* Created project and corresponding git repo
