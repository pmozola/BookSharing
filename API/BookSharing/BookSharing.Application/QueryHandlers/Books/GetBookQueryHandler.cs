using System.Threading;
using System.Threading.Tasks;

using MediatR;

namespace BookSharing.Application.QueryHandlers.Books
{
    public class GetBookQueryHandler : IRequestHandler<GetBookQuery, BookInformationResource>
    {
        public async Task<BookInformationResource> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            return new BookInformationResource(
                request.ISBN,
                "Testowa Ksiazka",
                2020,
                "http://covers.openlibrary.org/b/id/5548424-L.jpg");
        }
    }

    public record GetBookQuery(long ISBN) : IRequest<BookInformationResource>;


    public class BookInformationResource
    {
        public long ISBN { get; }
        public string Title { get; }
        public int Year { get; }
        public string ImageUrl { get; }

        public BookInformationResource(long isbn, string title, int year, string imageUrl)
        {
            ISBN = isbn;
            Title = title;
            Year = year;
            ImageUrl = imageUrl;
        }
    }
}
