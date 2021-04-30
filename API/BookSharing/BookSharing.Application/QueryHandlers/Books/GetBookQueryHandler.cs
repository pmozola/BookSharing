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
            if (book == null)
            {
                return null;
            }

            return new BookInformationResource(book.Isbn, book.Title, book.Description, string.Join(", ", book.Autor), book.Year, book.ImageUrl);
        }
    }

    public record GetBookQuery(long ISBN) : IRequest<BookInformationResource>;

    public record BookInformationResource(long Isbn, string Title, string Authors, string Description, int Year, string ImageUrl);
}
