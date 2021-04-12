using BookSharing.Domain.Base;

namespace BookSharing.Domain.BookAggregate
{
    public class Book : IAggregateRoot
    {
        public Book(string ISBN, string Title)
        {
            this.ISBN = ISBN;
            this.Title = Title;
        }

        public int Id { get;  private set; }
        public string ISBN { get; private set; }
        public string Title { get; private set; }
    }
}
