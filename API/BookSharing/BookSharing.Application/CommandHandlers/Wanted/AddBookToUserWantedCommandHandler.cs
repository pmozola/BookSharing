using System.Threading;
using System.Threading.Tasks;
using BookSharing.Application.Interface;
using BookSharing.Domain.UserWantedAggregate;
using MediatR;

namespace BookSharing.Application.CommandHandlers.Wanted
{
    public class AddBookToUserWantedCommandHandler : AsyncRequestHandler<AddBookToUserWantedCommand>
    {
        private readonly IUserContext _userContext;
        private readonly IUserWantedRepository _repository;

        public AddBookToUserWantedCommandHandler(IUserContext userContext, IUserWantedRepository repository)
        {
            _userContext = userContext;
            _repository = repository;
        }

        protected override async Task Handle(AddBookToUserWantedCommand request, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(new UserWanted(request.Isbn, _userContext.GetUserId()), cancellationToken);
        }
    }

    public record AddBookToUserWantedCommand(long Isbn) : IRequest;
}
