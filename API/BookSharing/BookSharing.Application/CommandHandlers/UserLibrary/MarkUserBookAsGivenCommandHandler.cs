using System.Threading;
using System.Threading.Tasks;
using BookSharing.Application.Interface;
using BookSharing.Domain.UserBookAggregate;
using MediatR;

namespace BookSharing.Application.CommandHandlers.UserLibrary
{
    public class MarkUserBookAsGivenCommandHandler : AsyncRequestHandler<MarkUserBookAsGivenCommand>
    {
        private readonly IUserContext _userContext;
        private readonly IUserBookRepository _repository;

        public MarkUserBookAsGivenCommandHandler(IUserContext userContext, IUserBookRepository repository)
        {
            _userContext = userContext;
            _repository = repository;
        }
        protected override async Task Handle(MarkUserBookAsGivenCommand request, CancellationToken cancellationToken)
        {
            var book = await _repository.GetAsync(request.Id, _userContext.GetUserId());
            book.MarkAsGiven();

            await _repository.UpdateAsync(book);
        }
    }

    public record MarkUserBookAsGivenCommand(int Id) : IRequest;
}
