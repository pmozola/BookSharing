using System;
using BookSharing.Domain.Base;

namespace BookSharing.Domain.BookAggregate
{
    public class Book : IAggregateRoot
    {
        public Book(long isbn, string title, string author, string year, string description, string imageUrl)
        {
            ISBN = isbn;
            Title = title;
            Author = author;
            Year = year;
            Description = description;
            ImageUrl = imageUrl;
        }
        private Book() { }

        public int Id { get;  private set; }
        public long ISBN { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public string ImageUrl { get; private set; }
        public string Year { get; private set; }
        public string Description { get; private set; }
    }
}
