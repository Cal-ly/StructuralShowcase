namespace BookLibrary;
/// <summary>  
/// Repository for managing a collection of books.  
/// </summary>  
public class BookRepository
{
    private readonly List<Book> _books = [];

    /// <summary>  
    /// Gets all books in the repository.  
    /// </summary>  
    /// <returns>An enumerable collection of books.</returns>  
    public IEnumerable<Book> GetAll()
    {
        return _books;
    }

    /// <summary>  
    /// Gets a book by its unique identifier.  
    /// </summary>  
    /// <param name="id">The unique identifier of the book.</param>  
    /// <returns>The book with the specified identifier, or null if not found.</returns>  
    public Book? GetById(int id)
    {
        return _books.FirstOrDefault(book => book.Id == id);
    }

    /// <summary>  
    /// Adds a new book to the repository.  
    /// </summary>  
    /// <param name="book">The book to add.</param>  
    /// <returns>The added book if successful, or null if the book is invalid.</returns>  
    public Book? Add(Book book)
    {
        if (book.Validate())
        {
            _books.Add(book);
            return book;
        }
        return null;
    }

    /// <summary>  
    /// Updates an existing book in the repository.  
    /// </summary>  
    /// <param name="book">The book with updated information.</param>  
    /// <returns>The updated book if successful, or null if the book is invalid or not found.</returns>  
    public Book? Update(Book book)
    {
        if (book.Validate())
        {
            var existingBook = GetById(book.Id);
            if (existingBook != null)
            {
                existingBook.Name = book.Name;
                existingBook.Author = book.Author;
                existingBook.Price = book.Price;
                existingBook.ISBN = book.ISBN;
                return existingBook;
            }
        }
        return null;
    }

    /// <summary>  
    /// Removes a book from the repository by its unique identifier.  
    /// </summary>  
    /// <param name="id">The unique identifier of the book to remove.</param>  
    /// <returns>The removed book if successful, or null if the book is not found.</returns>  
    public Book? Remove(int id)
    {
        var book = GetById(id);
        if (book != null)
        {
            _books.Remove(book);
            return book;
        }
        return null;
    }
}
