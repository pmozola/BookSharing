using System.Threading;
using System.Threading.Tasks;
using BookSharing.Application.Interface;
using BookSharing.Domain.UserBookAggregate;
using MediatR;

namespace BookSharing.Application.CommandHandlers.UserLibrary
{
    public class AddBookToUserLibraryCommandHandler : AsyncRequestHandler<AddBookToUserLibraryCommand>
    {
        private readonly IUserContext _userContext;
        private readonly IUserBookRepository _repository;

        public AddBookToUserLibraryCommandHandler(IUserContext userContext, IUserBookRepository repository)
        {
            _userContext = userContext;
            _repository = repository;
        }
        protected override async Task Handle(AddBookToUserLibraryCommand request, CancellationToken cancellationToken)
        {
            var book = new UserBook(_userContext.GetUserId(), request.Isbn, request.Rank);

            await _repository.AddAsync(book);
        }
    }

    public record AddBookToUserLibraryCommand(long Isbn, int Rank, string Description) : IRequest;
}
