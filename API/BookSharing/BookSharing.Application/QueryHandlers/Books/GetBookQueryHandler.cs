using System.Threading;
using System.Threading.Tasks;
using BookSharing.Infrastructure.Interface;
using MediatR;

namespace BookSharing.Application.QueryHandlers.Books
{
    public class GetBookQueryHandler : IRequestHandler<GetBookQuery, BookInformationResource>
    {
        private readonly IBookService _bookservice;

        public GetBookQueryHandler(IBookService bookservice)
        {
            this._bookservice = bookservice;
        }
        public async Task<BookInformationResource> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            var book = await this._bookservice.GetBook(request.ISBN);


            return new BookInformationResource(
                request.ISBN,
            // "Testowa Ksiazka",
            //"2020",
            //"http://covers.openlibrary.org/b/id/5548424-L.jpg");
            book.Title,
            book.Year,
            book.ImageUrl);
        }
    }

    public record GetBookQuery(long ISBN) : IRequest<BookInformationResource>;

    public class BookInformationResource
    {
        public long ISBN { get; }
        public string Title { get; }
        public string Year { get; }
        public string ImageUrl { get; }

        public BookInformationResource(long isbn, string title, string year, string imageUrl)
        {
            ISBN = isbn;
            Title = title;
            Year = year;
            ImageUrl = imageUrl;
        }
    }
}
