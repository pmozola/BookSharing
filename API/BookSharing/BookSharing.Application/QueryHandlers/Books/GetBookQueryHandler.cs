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
                "Autor ksiazki",
                @"Zmień sposób myślenia o projektowaniu systemów informatycznych! Tworzenie skomplikowanych systemów informatycznych 
                  wymaga nowego podejścia. Dotychczas stosowane metody przestają się sprawdzać i generują mnóstwo problemów.",
                2020,
                "http://covers.openlibrary.org/b/id/5548424-L.jpg");
        }
    }

    public record GetBookQuery(long ISBN) : IRequest<BookInformationResource>;

    public record BookInformationResource(long Isbn, string Title, string Authors, string Description, int Year, string ImageUrl);
}
