using System;
namespace BookStore.Domain.Entities
{
	public class Book
    {
		
        public Book(int id,string name, decimal price, Specifications specifications)
        {
            Id = id;
            Name = name;
            Price = price;
            Specifications = specifications;
        }

        public int Id { get; set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public Specifications Specifications { get; set; }
    }
}


