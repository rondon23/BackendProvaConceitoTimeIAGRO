using System;
using BookStore.Domain.Dtos;
using BookStore.Domain.Entities;

namespace BookStore.Interface.Repositories
{
	public interface IBookStoreRepository
	{
        IEnumerable<Book> GetByParameters(BooksQueryParameters parameters);
        Book GetBookById(int id);
    }
}

