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
        private readonly IBookRepository _repository;

        public GetBookQueryHandler(IExternalBookApiProvider bookInformationProvider, IBookRepository repository)
        {
            _bookInformationProvider = bookInformationProvider;
            _repository = repository;
        }

        async Task<Result<BookInformationResource>> IRequestHandler<GetBookQuery, Result<BookInformationResource>>.Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            var book = await _repository.GetAsync(request.Isbn, cancellationToken);
            if (book != null)
            {
                return Result<BookInformationResource>.Success(
                        new BookInformationResource(book.ISBN, book.Title, book.Description, book.Author, book.Year, book.ImageUrl));

            }

            var externalBookInformation = await _bookInformationProvider.GetBook(request.Isbn);
            if (externalBookInformation != null)
            {
                return Result<BookInformationResource>.Success(
                    new BookInformationResource(
                        externalBookInformation.Isbn,
                        externalBookInformation.Title,
                        externalBookInformation.Description,
                        string.Join(", ", externalBookInformation.Autor),
                        externalBookInformation.Year,
                        externalBookInformation.ImageUrl));
            }

            return Result<BookInformationResource>.Error(new NotFoundException());
        }
    }

    public record GetBookQuery(long Isbn) : IRequest<Result<BookInformationResource>>;

    public record BookInformationResource(long Isbn, string Title, string Authors, string Description, int Year, string ImageUrl);
}
