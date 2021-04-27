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
            var book = await _bookservice.GetBook(request.ISBN);

            return new BookInformationResource(book.Isbn, book.Title, book.Year, book.ImageUrl, string.Join(", ", book.Autor));  
        }
    }

    public record GetBookQuery(long ISBN) : IRequest<BookInformationResource>;

    public record  BookInformationResource (long ISBN, string Title, int Year, string ImageUrl, string Authors);
}
