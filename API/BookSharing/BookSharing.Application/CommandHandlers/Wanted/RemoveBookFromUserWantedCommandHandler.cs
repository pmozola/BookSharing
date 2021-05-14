using System.Threading;
using System.Threading.Tasks;
using BookSharing.Application.Interface;
using BookSharing.Domain.UserWantedAggregate;
using MediatR;

namespace BookSharing.Application.CommandHandlers.Wanted
{
    public class RemoveBookFromUserWantedCommandHandler : AsyncRequestHandler<RemoveBookFromUserWantedCommand>
    {
        private readonly IUserWantedRepository _repository;
        private readonly IUserContext _userContext;

        public RemoveBookFromUserWantedCommandHandler(IUserWantedRepository repository, IUserContext userContext)
        {
            _repository = repository;
            _userContext = userContext;
        }
        protected override async Task Handle(RemoveBookFromUserWantedCommand request, CancellationToken cancellationToken)
        {
            var book = await _repository.GetForUser(request.Isbn, _userContext.GetUserId(), cancellationToken);

            if (book != null)
            {
                await _repository.Delete(book, cancellationToken);
            }
        }
    }

    public record RemoveBookFromUserWantedCommand(long Isbn) : IRequest;
}
