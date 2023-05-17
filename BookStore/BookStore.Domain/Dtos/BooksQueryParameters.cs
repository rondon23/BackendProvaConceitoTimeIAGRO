using System;
using BookStore.Domain.Entities;

namespace BookStore.Domain.Dtos
{

    public class BooksQueryParameters
    {
            public string name { get; set; } = "";
            public decimal price { get; set; } = 0;
            public string originally_published { get; set; } = "";
            public string author { get; set; } = "";
            public int page_count { get; set; } = 0;
            public string illustrator { get; set; } = "";
            public string genres { get; set; } = "";
            public bool orderAsc { get; set; } = true;
    }

        public static class BooksQueryParametersStatic
        {
            public static IEnumerable<Book> Filtered(this BooksQueryParameters query, IEnumerable<Book> books)
            {
                var newBooks = new List<Book>();

                Func<Book, bool> predicate = (x => true);

            if (!String.IsNullOrEmpty(query.name))
                newBooks.AddRange(books.Where(x => x.Name.Contains(query.name)).ToList()) ;

                if (query.price > 0)
                newBooks.AddRange(books.Where(x => x.Price == query.price));

                if (!String.IsNullOrEmpty(query.originally_published))
                newBooks.AddRange(books.Where(x => x.Specifications.OriginallyPublished.Contains(query.originally_published)));

                if (!String.IsNullOrEmpty(query.author))
                newBooks.AddRange(books.Where(x => x.Specifications.Author.Contains(query.author)));

                if (query.page_count > 0)
                newBooks.AddRange(books.Where(x => x.Specifications.PageCount == query.page_count));

                if (!String.IsNullOrEmpty(query.illustrator))
                newBooks.AddRange(books.Where(x => x.Specifications.Illustrator.Contains(query.illustrator)));

                if (!String.IsNullOrEmpty(query.genres))
                newBooks.AddRange(books.Where(x => x.Specifications.Genres.Contains(query.genres)));

                
                if (query.orderAsc)
                    newBooks = newBooks.OrderBy(x => x.Price).ToList();
                else
                    newBooks = newBooks.OrderByDescending(x => x.Price).ToList();

                return newBooks;
            }
        }
    }


