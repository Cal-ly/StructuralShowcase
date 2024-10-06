using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(BookRepository bookRepository) : ControllerBase
{
    private readonly BookRepository _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));

    [EndpointDescription("Get all books")]
    [EnableCors("AllowAll")]
    [HttpGet]
    public IActionResult GetBooks()
    {
        try
        {
            var books = _bookRepository.GetAll();
            return Ok(books);
        }
        catch (Exception)
        {
            // Log the exception (ex) here if needed
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving books.");
        }
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public ActionResult<Book> GetBook(int id)
    {
        try
        {
            var book = _bookRepository.GetById(id);
            if (book == null)
            {
                return NotFound($"No such book with id: {id}");
            }
            return Ok(book);
        }
        catch (Exception)
        {
            // Log the exception (ex) here if needed
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the book.");
        }
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [HttpPost]
    public ActionResult<Book> AddBook([FromBody] Book book)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var existingBook = _bookRepository.GetById(book.Id);
            if (existingBook != null)
            {
                return Conflict($"A book with id: {book.Id} already exists.");
            }

            var newBook = _bookRepository.Add(book);
            if (newBook == null)
            {
                return BadRequest("Invalid book data.");
            }
            return CreatedAtAction(nameof(GetBook), new { id = newBook.Id }, newBook);
        }
        catch (Exception)
        {
            // Log the exception (ex) here if needed
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the book.");
        }
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [HttpPut("{id}")]
    public ActionResult<Book> UpdateBook(int id, [FromBody] Book book)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            if (id != book.Id)
            {
                return BadRequest("Book ID mismatch.");
            }

            var updatedBook = _bookRepository.Update(book);
            if (updatedBook == null)
            {
                return NotFound($"No such book with id: {id}");
            }

            return Ok(updatedBook);
        }
        catch (Exception)
        {
            // Log the exception (ex) here if needed
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the book.");
        }
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        try
        {
            var deletedBook = _bookRepository.Remove(id);
            if (deletedBook == null)
            {
                return NotFound($"No such book with id: {id}");
            }

            return NoContent();
        }
        catch (Exception)
        {
            // Log the exception (ex) here if needed
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the book.");
        }
    }
}
