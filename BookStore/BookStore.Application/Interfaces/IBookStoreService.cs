using System;
using BookStore.Domain.Dtos;
using BookStore.Domain.Entities;

namespace BookStore.Application.Interfaces
{
	public interface IBookStoreService
	{
        IEnumerable<Book> GetByParameters(BooksQueryParameters parameters);
        string CalculateShippingCost(int id);
        Book GetBookById(int id);
    }
}

