using BookSharing.Domain.Base;

namespace BookSharing.Domain.BookAggregate
{
    public class Book : IAggregateRoot
    {
        public Book(string ISBN, string Title, int Year)
        {
            this.ISBN = ISBN;
            this.Title = Title;
            this.Year = Year;
        }
        public int Id { get;  private set; }
        public string ISBN { get; private set; }
        public string Title { get; private set; }
        public int Year { get; private set; }
    }
}
