using System.Threading;
using System.Threading.Tasks;
using BookSharing.Application.Results;
using BookSharing.Domain.BookAggregate;
using BookSharing.Domain.Exceptions;
using MediatR;

namespace BookSharing.Application.QueryHandlers.Books
{
    public class GetBookQueryHandler : IRequestHandler<GetBookQuery, Result<BookInformationResource>>
    {
        private readonly IExternalBookApiProvider _bookInformationProvider;

        public GetBookQueryHandler(IExternalBookApiProvider bookInformationProvider)
        {
            _bookInformationProvider = bookInformationProvider;
        }

        async Task<Result<BookInformationResource>> IRequestHandler<GetBookQuery, Result<BookInformationResource>>.Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            var book = await _bookInformationProvider.GetBook(request.ISBN);
            
            if (book == null)
            {
                return Result<BookInformationResource>.Error(new NotFoundException());
            }

            return Result<BookInformationResource>.Success(
                new BookInformationResource(book.Isbn, book.Title, book.Description, string.Join(", ", book.Autor), book.Year, book.ImageUrl));
        }
    }

    public record GetBookQuery(long ISBN) : IRequest<Result<BookInformationResource>>;

    public record BookInformationResource(long Isbn, string Title, string Authors, string Description, int Year, string ImageUrl);
}
