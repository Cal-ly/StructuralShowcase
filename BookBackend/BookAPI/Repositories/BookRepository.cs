namespace BookAPI.Repositories;

public class BookRepository(BookDbContext context)
{
    public IEnumerable<Book> GetAll()
    {
        return [.. context.Books];
    }

    public Book? GetById(int id)
    {
        return context.Books.Find(id);
    }

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
