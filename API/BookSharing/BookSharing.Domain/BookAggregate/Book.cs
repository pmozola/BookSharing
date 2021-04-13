using System;
using BookSharing.Domain.Base;

namespace BookSharing.Domain.BookAggregate
{
    public class Book : IAggregateRoot
    {
        public Book(string ISBN, string Title, string Author, string Year, string Description)
        {
            this.ISBN = ISBN;
            this.Title = Title;
            this.Author = Author;
            this.Year = Year;
            this.Description = Description;
        }

        public int Id { get;  private set; }
        public string ISBN { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public string ImageUrl { get; private set; }
        public string Year { get; private set; }
        public string Description { get; private set; }
    }
}
