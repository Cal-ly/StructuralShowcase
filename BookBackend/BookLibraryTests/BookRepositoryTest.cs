using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibraryTests;
[TestClass]
public class BookRepositoryTest
{
    private BookRepository repository = null!;
    private Book validBook = null!;

    [TestInitialize]
    public void Setup()
    {
        repository = new BookRepository();
        validBook = new Book
        {
            Id = 1,
            Name = "Test Book",
            Author = "Test Author",
            Price = 9.99m,
            ISBN = "1234567890123"
        };
        repository.Add(validBook);
    }

    [TestMethod]
    public void GetAll_ReturnsAllBooks()
    {
        var books = repository.GetAll();
        Assert.AreEqual(1, books.Count());
    }

    [TestMethod]
    public void GetById_ExistingId_ReturnsBook()
    {
        var book = repository.GetById(1);
        Assert.IsNotNull(book);
        Assert.AreEqual(validBook, book);
    }

    [TestMethod]
    public void GetById_NonExistingId_ReturnsNull()
    {
        var book = repository.GetById(999);
        Assert.IsNull(book);
    }

    [TestMethod]
    public void Add_ValidBook_ReturnsBook()
    {
        var newBook = new Book
        {
            Id = 2,
            Name = "New Book",
            Author = "New Author",
            Price = 19.99m,
            ISBN = "9876543210987"
        };
        var addedBook = repository.Add(newBook);
        Assert.IsNotNull(addedBook);
        Assert.AreEqual(newBook, addedBook);
    }

    [TestMethod]
    public void Add_InvalidBook_ReturnsNull()
    {
        var invalidBook = new Book
        {
            Id = 0,
            Name = "",
            Author = "",
            Price = 0,
            ISBN = ""
        };
        var addedBook = repository.Add(invalidBook);
        Assert.IsNull(addedBook);
    }

    [TestMethod]
    public void Update_ValidBook_ReturnsUpdatedBook()
    {
        validBook.Name = "Updated Book";
        var updatedBook = repository.Update(validBook);
        Assert.IsNotNull(updatedBook);
        Assert.AreEqual("Updated Book", updatedBook.Name);
    }

    [TestMethod]
    public void Update_InvalidBook_ReturnsNull()
    {
        var invalidBook = new Book
        {
            Id = 1,
            Name = "",
            Author = "",
            Price = 0,
            ISBN = ""
        };
        var updatedBook = repository.Update(invalidBook);
        Assert.IsNull(updatedBook);
    }

    [TestMethod]
    public void Update_NonExistingBook_ReturnsNull()
    {
        var nonExistingBook = new Book
        {
            Id = 999,
            Name = "Non-Existing Book",
            Author = "Non-Existing Author",
            Price = 19.99m,
            ISBN = "9876543210987"
        };
        var updatedBook = repository.Update(nonExistingBook);
        Assert.IsNull(updatedBook);
    }

    [TestMethod]
    public void Remove_ExistingId_ReturnsRemovedBook()
    {
        var removedBook = repository.Remove(1);
        Assert.IsNotNull(removedBook);
        Assert.AreEqual(validBook, removedBook);
    }

    [TestMethod]
    public void Remove_NonExistingId_ReturnsNull()
    {
        var removedBook = repository.Remove(999);
        Assert.IsNull(removedBook);
    }
}
