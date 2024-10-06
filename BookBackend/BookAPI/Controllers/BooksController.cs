using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(BookRepository bookRepository) : ControllerBase
{
    // Dependency Injection for BookRepository
    private readonly BookRepository _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));

    // GET: api/books
    // Retrieves all books from the repository
    [EnableCors("AllowAll")]
    [HttpGet]
    public IActionResult GetBooks()
    {
        try
        {
            var books = _bookRepository.GetAll();
            return Ok(books); // Returns a 200 OK response with the list of books
        }
        catch (Exception)
        {
            // Log exception if needed
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving books.");
        }
    }

    // GET: api/books/{id}
    // Retrieves a specific book by ID
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
                return NotFound($"No such book with id: {id}"); // Returns 404 if the book is not found
            }
            return Ok(book); // Returns 200 OK with the found book
        }
        catch (Exception)
        {
            // Log exception if needed
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the book.");
        }
    }

    // POST: api/books
    // Adds a new book to the repository
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
                return UnprocessableEntity(ModelState); // Returns 422 if model validation fails
            }

            var existingBook = _bookRepository.GetById(book.Id);
            if (existingBook != null)
            {
                return Conflict($"A book with id: {book.Id} already exists."); // Returns 409 Conflict if the book already exists
            }

            var newBook = _bookRepository.Add(book);
            if (newBook == null)
            {
                return BadRequest("Invalid book data."); // Returns 400 Bad Request if the book cannot be added
            }

            return CreatedAtAction(nameof(GetBook), new { id = newBook.Id }, newBook); // Returns 201 Created with the newly added book
        }
        catch (Exception)
        {
            // Log exception if needed
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the book.");
        }
    }

    // PUT: api/books/{id}
    // Updates an existing book in the repository
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
                return UnprocessableEntity(ModelState); // Returns 422 if model validation fails
            }

            if (id != book.Id)
            {
                return BadRequest("Book ID mismatch."); // Returns 400 Bad Request if the IDs do not match
            }

            var updatedBook = _bookRepository.Update(book);
            if (updatedBook == null)
            {
                return NotFound($"No such book with id: {id}"); // Returns 404 if the book is not found
            }

            return Ok(updatedBook); // Returns 200 OK with the updated book
        }
        catch (Exception)
        {
            // Log exception if needed
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the book.");
        }
    }

    // DELETE: api/books/{id}
    // Deletes a specific book by ID
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
                return NotFound($"No such book with id: {id}"); // Returns 404 if the book is not found
            }

            return NoContent(); // Returns 204 No Content after successful deletion
        }
        catch (Exception)
        {
            // Log exception if needed
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the book.");
        }
    }
}
