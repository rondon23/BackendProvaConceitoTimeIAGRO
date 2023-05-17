using System;
using BookStore.Domain.Entities;
using System.Text.Json;
using BookStore.Interface.Repositories;
using System.Xml.Linq;
using BookStore.Domain.Dtos;

namespace BookStore.Repository.Repositories
{
	public class BookStoreRepository : IBookStoreRepository
    {
        private readonly List<Book> _books;
        private readonly string _jsonFilePath = "books.json";

        public BookStoreRepository()
        {
            try
            {
                string jsonString = File.ReadAllText(_jsonFilePath);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                _books = JsonSerializer.Deserialize<List<Book>>(jsonString, options);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public IEnumerable<Book> GetByParameters(BooksQueryParameters parameters)
        {
            var booksFiltered = parameters.Filtered(_books);

            if (booksFiltered.Count() == 0)
                throw new ArgumentNullException("Book not found.");

            return booksFiltered;
        }

        public Book GetBookById(int id)
        {
            var booksFiltered = _books.FirstOrDefault(b => b.Id == id);

            if (booksFiltered is null)
                throw new ArgumentNullException("Book not found.");

            return booksFiltered;
        }
    }
}

