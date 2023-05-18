using BookStore.Application.Services;
using BookStore.Domain.Dtos;
using BookStore.Domain.Entities;
using BookStore.Interface.Repositories;
using BookStore.Repository.Repositories;
using Moq;

namespace BookStore.Tests;

public class BookStoreServiceTest
{

    private readonly Mock<IBookStoreRepository> _mockBookRepository;
    private readonly BookStoreService _bookService;
    private readonly BookStoreRepository _bookStoreRepository;

    public BookStoreServiceTest()
    {
        _mockBookRepository = new Mock<IBookStoreRepository>();
        _bookService = new BookStoreService(_mockBookRepository.Object);
        _bookStoreRepository = new BookStoreRepository();
    }

    [Fact]
    public void GetBooksByAuthor_ReturnsBooks_WhenAuthorNotExists()
    {
        var correctAuthor = "Jules Verner";
        var testBooks = new BooksQueryParameters("Journey", 0, "", correctAuthor, 0, "", "", false);
            

        _mockBookRepository.Setup(repo => repo.GetByParameters(testBooks));

        var result = _bookService.GetByParameters(testBooks);

        Assert.NotNull(result);
        Assert.Equal(0, result.Count());

        _mockBookRepository.Verify(repo => repo.GetByParameters(testBooks), Times.Once());
    }

}


