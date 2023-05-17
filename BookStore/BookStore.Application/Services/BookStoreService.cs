using BookStore.Application.Interfaces;
using BookStore.Domain.Dtos;
using BookStore.Domain.Entities;
using BookStore.Interface.Repositories;

namespace BookStore.Application.Services
{
	public class BookStoreService : IBookStoreService
    {
        private readonly IBookStoreRepository _bookStoreRepository;

        public BookStoreService(IBookStoreRepository bookStoreRepository)
        {
            _bookStoreRepository = bookStoreRepository;
        }

        public string CalculateShippingCost(int id)
        {
            var book = _bookStoreRepository.GetBookById(id);

            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }
            string msg = "Shipping cost is: " + Math.Round(book.Price * 0.2m, 2);
            return msg;
        }

        public Book GetBookById(int id)
        {
            return _bookStoreRepository.GetBookById(id);
        }

        public IEnumerable<Book> GetByParameters(BooksQueryParameters parameters)
        {
            var books = _bookStoreRepository.GetByParameters(parameters);
            return books;
        }
    }
}

