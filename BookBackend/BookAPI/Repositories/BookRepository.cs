namespace BookAPI.Repositories;

/// <summary>
/// Repository class for managing book entities in the database.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="BookRepository"/> class.
/// </remarks>
/// <param name="context">The database context to be used by the repository.</param>
public class BookRepository(BookDbContext context)
{

    /// <summary>
    /// Retrieves all books from the database.
    /// </summary>
    /// <returns>An enumerable collection of all books.</returns>
    public IEnumerable<Book> GetAll() => [.. context.Books];

    /// <summary>
    /// Retrieves a book by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the book.</param>
    /// <returns>The book with the specified identifier, or null if not found.</returns>
    public Book? GetById(int id)
    {
        return context.Books.Find(id);
    }

    /// <summary>
    /// Adds a new book to the database.
    /// </summary>
    /// <param name="book">The book entity to be added.</param>
    /// <returns>The added book entity, or null if the book is invalid.</returns>
    public Book? Add(Book book)
    {
        if (book.Validate())
        {
            context.Books.Add(book);
            context.SaveChanges();
            return book;
        }
        return null;
    }

    /// <summary>
    /// Updates an existing book in the database.
    /// </summary>
    /// <param name="book">The book entity to be updated.</param>
    /// <returns>The updated book entity, or null if the book is invalid.</returns>
    public Book? Update(Book book)
    {
        if (book.Validate())
        {
            context.Books.Update(book);
            context.SaveChanges();
            return book;
        }
        return null;
    }

    /// <summary>
    /// Removes a book from the database by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the book to be removed.</param>
    /// <returns>The removed book entity, or null if the book was not found.</returns>
    public Book? Remove(int id)
    {
        var book = context.Books.Find(id);
        if (book != null)
        {
            context.Books.Remove(book);
            context.SaveChanges();
            return book;
        }
        return null;
    }
}
