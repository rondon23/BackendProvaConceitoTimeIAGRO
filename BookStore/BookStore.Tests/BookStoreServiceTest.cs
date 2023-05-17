using BookStore.Application.Services;
using BookStore.Domain.Entities;
using BookStore.Interface.Repositories;
using Moq;
using Newtonsoft.Json.Linq;
using System.Text;

namespace BookStore.Tests;

public class BookStoreServiceTest
{

    private readonly Mock<IBookStoreRepository> _mockBookRepository;
    private readonly BookStoreService _bookService;

    public BookStoreServiceTest()
    {
        _mockBookRepository = new Mock<IBookStoreRepository>();
        _bookService = new BookStoreService(_mockBookRepository.Object);
    }

    [Fact]
    public void GetBooksByAuthor_ReturnsBooks_WhenAuthorExists()
    {
        var correctAuthor = "Author Name";
        var wrongAuthor1 = "Wrong Author1";
        var wrongAuthor2 = "Wrong Author2";
        var testBooks = new List<Book>
            {
                new Book(1, "Book 1", 10, new Specifications { Author = correctAuthor }),
                new Book(2, "Book 2", 15, new Specifications { Author = correctAuthor }),
                new Book(3, "Book 3", 20, new Specifications { Author = wrongAuthor1 }),
                new Book(4, "Book 4", 25, new Specifications { Author = wrongAuthor2 })
            };

        _mockBookRepository.Setup(repo => repo.GetByParameters(It.IsAny<string>())).Returns<string>(author => testBooks.Where(book => book.Specifications.Author == author));

        var result = _bookService.GetBooksByAuthor(correctAuthor);

        Assert.Collection(result,
            book1 => Assert.Equal(testBooks[0], book1, new BookEqualityComparer()),
            book2 => Assert.Equal(testBooks[1], book2, new BookEqualityComparer())
        );

        _mockBookRepository.Verify(repo => repo.GetBooksByAuthor(correctAuthor), Times.Once());
    }

    [Theory]
    [InlineData("asc")]
    [InlineData("desc")]
    public void GetBooksByAuthor_ReturnsBooks_WithCorrectSortOrder(string sortOrder)
    {

        var correctAuthor = "Author Name";
        var wrongAuthor1 = "Wrong Author1";
        var wrongAuthor2 = "Wrong Author2";
        var testBooks = new List<Book>
        {
            new Book(1, "Book 1", 10, new Specifications { Author = correctAuthor }),
            new Book(2, "Book 2", 15, new Specifications { Author = correctAuthor }),
            new Book(3, "Book 3", 20, new Specifications { Author = wrongAuthor1 }),
            new Book(4, "Book 4", 25, new Specifications { Author = wrongAuthor2 })
        };

        _mockBookRepository.Setup(repo => repo.GetBooksByAuthor(It.IsAny<string>())).Returns<string>(author => testBooks.Where(book => book.Specifications.Author == author));


        var result = _bookService.GetBooksByAuthor(correctAuthor, sortOrder);

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());

        if (sortOrder == "asc")
        {
            Assert.Equal(testBooks[0], result.ElementAt(0), new BookEqualityComparer());
            Assert.Equal(testBooks[1], result.ElementAt(1), new BookEqualityComparer());
        }
        else
        {
            Assert.Equal(testBooks[1], result.ElementAt(0), new BookEqualityComparer());
            Assert.Equal(testBooks[0], result.ElementAt(1), new BookEqualityComparer());
        }

        _mockBookRepository.Verify(repo => repo.GetBooksByAuthor(correctAuthor), Times.Once());
    }

    [Fact]
    public void GetBooksByName_ReturnsBooks_WhenNameExists()
    {
        var name = "Book Name";
        var testBooks = new List<Book>
            {
                new Book(1, "Book Name", 10, new Specifications { Author = "Author 1" }),
                new Book(2, "Book Name", 15, new Specifications { Author = "Author 2" }),
                new Book(3, "Book 3", 20, new Specifications { Author = "Author 3" }),
                new Book(4, "Book 4", 25, new Specifications { Author = "Author 4" }),
                new Book(5, "Book Name", 152, new Specifications { Author = "Author 5" }),
            };

        _mockBookRepository.Setup(repo => repo.GetBooksByName(It.IsAny<string>())).Returns<string>(nameFilter => testBooks.Where(book => book.Name == nameFilter));

        var result = _bookService.GetBooksByName(name, "desc");
        Assert.Collection(result,
            book3 => Assert.Equal(testBooks[4], book3, new BookEqualityComparer()),
            book1 => Assert.Equal(testBooks[1], book1, new BookEqualityComparer()),
            book2 => Assert.Equal(testBooks[0], book2, new BookEqualityComparer())
        );

        _mockBookRepository.Verify(repo => repo.GetBooksByName(name), Times.Once());
    }

    [Fact]
    public void GetBookById_ReturnsBook_WhenIdExists()
    {
        var testBooks = new List<Book>
            {
                new Book(1, "Book 1", 10, new Specifications { Author = "Author 1" }),
                new Book(2, "Book 2", 15, new Specifications { Author = "Author 2" }),
                new Book(3, "Book 3", 20, new Specifications { Author = "Author 3" }),
                new Book(4, "Book 4", 25, new Specifications { Author = "Author 4" })
            };

        var bookId = 2;

        _mockBookRepository.Setup(repo => repo.GetBookById(It.IsAny<int>())).Returns<int>(id => testBooks.FirstOrDefault(book => book.Id == id));

        var result = _bookService.GetBookById(bookId);

        Assert.NotNull(result);
        Assert.Equal(testBooks[1], result, new BookEqualityComparer());
        _mockBookRepository.Verify(repo => repo.GetBookById(bookId), Times.Once());
    }
    public class BookEqualityComparer : IEqualityComparer<Book>
    {
        public bool Equals(Book x, Book y)
        {
            if (x == null || y == null)
                return false;

            return x.Id == y.Id &&
                   x.Name == y.Name &&
                   x.Price == y.Price &&
                   Equals(x.Specifications, y.Specifications);
        }

        public int GetHashCode(Book obj)
        {
            return HashCode.Combine(obj.Id, obj.Name, obj.Price, obj.Specifications);
        }
    }
}

}
