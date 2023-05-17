using System;
using System.Text.Json.Serialization;
using BookStore.Repository.Converter;

namespace BookStore.Domain.Entities
{
	public class Specifications
	{
        public Specifications()
        {

        }
        public Specifications(
            string originallyPublished,
            string author,
            int pageCount,
            string[] illustrator,
            string[] genres)
        {
            OriginallyPublished = originallyPublished;
            Author = author;
            PageCount = pageCount;
            Illustrator = illustrator;
            Genres = genres;
        }

        [JsonPropertyName("Originally published")]
        public string OriginallyPublished { get; set; }

        [JsonPropertyName("Author")]
        public string Author { get; set; }

        [JsonPropertyName("Page count")]
        public int PageCount { get; set; }

        [JsonConverter(typeof(SingleOrArrayConverter<string>))]
        [JsonPropertyName("Illustrator")]
        public string[] Illustrator { get; set; }

        [JsonConverter(typeof(SingleOrArrayConverter<string>))]
        [JsonPropertyName("Genres")]
        public string[] Genres { get; set; }
    }
}

