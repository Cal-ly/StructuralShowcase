namespace BookLibraryTests;

[TestClass]
public class BookTest
{
    private Book validBook = null!;

    [TestInitialize]
    public void Setup()
    {
        validBook = new Book
        {
            Id = 1,
            Title = "Test Book",
            Author = "Test Author",
            Price = 9.99m,
            ISBN = "1234567890123"
        };
    }

    [TestMethod]
    public void Validate_ValidBook_ReturnsTrue()
    {
        Assert.IsTrue(validBook.Validate());
    }

    [TestMethod]
    public void Validate_InvalidId_ReturnsFalse()
    {
        validBook.Id = 0;
        Assert.IsFalse(validBook.Validate());
    }

    [TestMethod]
    public void Validate_InvalidName_ReturnsFalse()
    {
        validBook.Title = "";
        Assert.IsFalse(validBook.Validate());
    }

    [TestMethod]
    public void Validate_InvalidAuthor_ReturnsFalse()
    {
        validBook.Author = "";
        Assert.IsFalse(validBook.Validate());
    }

    [TestMethod]
    public void Validate_InvalidPrice_ReturnsFalse()
    {
        validBook.Price = 0;
        Assert.IsFalse(validBook.Validate());
    }

    [TestMethod]
    public void Validate_InvalidISBN_ReturnsFalse()
    {
        validBook.ISBN = "123";
        Assert.IsFalse(validBook.Validate());
    }

    [TestMethod]
    public void ValidateId_ValidId_ReturnsTrue()
    {
        Assert.IsTrue(validBook.ValidateId());
    }

    [TestMethod]
    public void ValidateId_InvalidId_ReturnsFalse()
    {
        validBook.Id = 0;
        Assert.IsFalse(validBook.ValidateId());
    }

    [TestMethod]
    public void ValidateName_ValidName_ReturnsTrue()
    {
        Assert.IsTrue(validBook.ValidateName());
    }

    [TestMethod]
    public void ValidateName_InvalidName_ReturnsFalse()
    {
        validBook.Title = "";
        Assert.IsFalse(validBook.ValidateName());
    }

    [TestMethod]
    public void ValidateAuthor_ValidAuthor_ReturnsTrue()
    {
        Assert.IsTrue(validBook.ValidateAuthor());
    }

    [TestMethod]
    public void ValidateAuthor_InvalidAuthor_ReturnsFalse()
    {
        validBook.Author = "";
        Assert.IsFalse(validBook.ValidateAuthor());
    }

    [TestMethod]
    public void ValidatePrice_ValidPrice_ReturnsTrue()
    {
        Assert.IsTrue(validBook.ValidatePrice());
    }

    [TestMethod]
    public void ValidatePrice_InvalidPrice_ReturnsFalse()
    {
        validBook.Price = 0;
        Assert.IsFalse(validBook.ValidatePrice());
    }

    [TestMethod]
    public void ValidateISBN_ValidISBN_ReturnsTrue()
    {
        Assert.IsTrue(validBook.ValidateISBN());
    }

    [TestMethod]
    public void ValidateISBN_InvalidISBN_ReturnsFalse()
    {
        validBook.ISBN = "123";
        Assert.IsFalse(validBook.ValidateISBN());
    }

    [TestMethod]
    public void ToString_ReturnsCorrectFormat()
    {
        string expected = "Id: 1, Name: Test Book, Author: Test Author, Price: 9,99 kr., ISBN: 1234567890123";
        Assert.AreEqual(expected.Trim(), validBook.ToString().Trim());
    }

    [TestMethod]
    public void Equals_SameProperties_ReturnsTrue()
    {
        var anotherBook = new Book
        {
            Id = 1,
            Title = "Test Book",
            Author = "Test Author",
            Price = 9.99m,
            ISBN = "1234567890123"
        };
        Assert.IsTrue(validBook.Equals(anotherBook));
    }

    [TestMethod]
    public void Equals_DifferentProperties_ReturnsFalse()
    {
        var anotherBook = new Book
        {
            Id = 2,
            Title = "Another Book",
            Author = "Another Author",
            Price = 19.99m,
            ISBN = "9876543210987"
        };
        Assert.IsFalse(validBook.Equals(anotherBook));
    }

    [TestMethod]
    public void GetHashCode_SameProperties_ReturnsSameHashCode()
    {
        var anotherBook = new Book
        {
            Id = 1,
            Title = "Test Book",
            Author = "Test Author",
            Price = 9.99m,
            ISBN = "1234567890123"
        };
        Assert.AreEqual(validBook.GetHashCode(), anotherBook.GetHashCode());
    }
}
