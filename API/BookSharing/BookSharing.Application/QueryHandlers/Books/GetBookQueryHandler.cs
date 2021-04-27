using System.Threading;
using System.Threading.Tasks;
using BookSharing.Infrastructure.Interface;
using MediatR;

namespace BookSharing.Application.QueryHandlers.Books
{
    public class GetBookQueryHandler : IRequestHandler<GetBookQuery, BookInformationResource>
    {
        private readonly IExternalBookApiClient _bookservice;

        public GetBookQueryHandler(IExternalBookApiClient bookservice)
        {
            _bookservice = bookservice;
        }
        public async Task<BookInformationResource> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            var book = await this._bookservice.GetBook(request.ISBN);

            return new BookInformationResource(
                request.ISBN,
                book.bookString);
                //book.Title,
                //book.Year,
                //book.ImageUrl);
        }
    }

    public record GetBookQuery(long ISBN) : IRequest<BookInformationResource>;

    public class BookInformationResource
    {
        public long ISBN { get; }
        public string Title { get; }
        public int Year { get; }
        public string ImageUrl { get; }
        public string BookString { get; set; }

        public BookInformationResource(long isbn, string bookString/*string title, int year, string imageUrl*/)
        {
            ISBN = isbn;
            BookString = bookString;
            //Title = title;
            //Year = year;
            //ImageUrl = imageUrl;
        }
    }
}
