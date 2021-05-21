using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TecH3Demo.API.Domain;
using TecH3Demo.API.Services;

namespace TecH3Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // GET All /api/author

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var authors = await _authorService.GetAllAuthors();
                if (authors == null)
                {
                    return Problem("Returned null, unexspected behavior.");
                }
                else if (authors.Count == 0)
                {
                    return NoContent();
                }
                return Ok(authors);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }

        }


        // GET By Id /api/author

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Get([FromRoute] int id)
        {
            try
            {
                var author = await _authorService.GetAuthorById(id);
                if (author == null)
                {
                    return NotFound();
                }
                return Ok(author);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }


        // POST /api/author

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Create(Author author)
        {
            try
            {
                if(author == null)
                {
                    return BadRequest("Creation of Author failed.");
                }
                var newAuthor = await _authorService.CreateAuthor(author);
                return Ok(newAuthor);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }


        // PUT /api/author

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Update([FromRoute] int id, Author author)
        {
            try
            {
                var updateAuthor = await _authorService.UpdateAuthor(id, author);

                if(updateAuthor == null)
                {
                    return Problem("Editing of author failed.");
                }
                return Ok(updateAuthor);         
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            } 
        }


        // DELETE /api/author

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var deleteAuthor = await _authorService.DeleteAuthor(id);
                if(deleteAuthor == null)
                {
                    return NotFound("User with id: " + id + " does not exist");
                }
                return Ok(deleteAuthor);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}
