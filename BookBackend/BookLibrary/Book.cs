namespace BookLibrary;

/// <summary>
/// Represents a book with properties such as Id, Name, Author, Price, and ISBN.
/// </summary>
public class Book
{
    /// <summary>
    /// Gets or sets the unique identifier for the book.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the book.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the author of the book.
    /// </summary>
    public string? Author { get; set; }

    /// <summary>
    /// Gets or sets the price of the book.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the ISBN of the book.
    /// </summary>
    public string? ISBN { get; set; }

    /// <summary>
    /// Validates all properties of the book.
    /// </summary>
    /// <returns>True if all properties are valid; otherwise, false.</returns>
    public bool Validate()
    {
        return ValidateId() && ValidateName() && ValidateAuthor() && ValidatePrice() && ValidateISBN();
    }

    /// <summary>
    /// Validates the Id property.
    /// </summary>
    /// <returns>True if Id is greater than 0; otherwise, false.</returns>
    public bool ValidateId()
    {
        return Id > 0;
    }

    /// <summary>
    /// Validates the Name property.
    /// </summary>
    /// <returns>True if Name is not null or whitespace; otherwise, false.</returns>
    public bool ValidateName()
    {
        return !string.IsNullOrWhiteSpace(Name);
    }

    /// <summary>
    /// Validates the Author property.
    /// </summary>
    /// <returns>True if Author is not null or whitespace; otherwise, false.</returns>
    public bool ValidateAuthor()
    {
        return !string.IsNullOrWhiteSpace(Author);
    }

    /// <summary>
    /// Validates the Price property.
    /// </summary>
    /// <returns>True if Price is greater than 0; otherwise, false.</returns>
    public bool ValidatePrice()
    {
        return Price > 0;
    }

    /// <summary>
    /// Validates the ISBN property.
    /// </summary>
    /// <returns>True if ISBN is not null or whitespace and has a length of 13; otherwise, false.</returns>
    public bool ValidateISBN()
    {
        return !string.IsNullOrWhiteSpace(ISBN) && ISBN.Length == 13;
    }

    /// <summary>
    /// Returns a string that represents the current book.
    /// </summary>
    /// <returns>A string that represents the current book.</returns>
    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}, Author: {Author}, Price: {Price:C2}, ISBN: {ISBN}";
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current book.
    /// </summary>
    /// <param name="obj">The object to compare with the current book.</param>
    /// <returns>True if the specified object is equal to the current book; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is Book book)
        {
            return Id == book.Id &&
                   Name == book.Name &&
                   Author == book.Author &&
                   Price == book.Price &&
                   ISBN == book.ISBN;
        }
        return false;
    }

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current book.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Author, Price, ISBN);
    }
}
