using System.Threading;
using System.Threading.Tasks;
using BookSharing.Domain.BookAggregate;
using MediatR;

namespace BookSharing.Application.QueryHandlers.Books
{
    public class GetBookQueryHandler : IRequestHandler<GetBookQuery, BookInformationResource>
    {
        private readonly IExternalBookApiProvider _bookInformationProvider;

        public GetBookQueryHandler(IExternalBookApiProvider bookInformationProvider)
        {
            _bookInformationProvider = bookInformationProvider;
        }
        public async Task<BookInformationResource> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            var book = await _bookInformationProvider.GetBook(request.ISBN);

            return new BookInformationResource(book.Isbn, string.Join(", ", book.Autor), book.Title, book.Year, book.ImageUrl);  
        }
    }

    public record GetBookQuery(long ISBN) : IRequest<BookInformationResource>;

    public record BookInformationResource (long ISBN, string Authors, string Title, int Year, string ImageUrl);
}
