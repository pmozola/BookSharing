using System.Threading;
using System.Threading.Tasks;
using BookSharing.Application.Interface;
using BookSharing.Application.Results;
using BookSharing.Domain.Exceptions;
using BookSharing.Domain.UserBookAggregate;
using MediatR;

namespace BookSharing.Application.CommandHandlers.UserLibrary
{
    public class MarkUserBookAsGivenCommandHandler : IRequestHandler<MarkUserBookAsGivenCommand, Result>
    {
        private readonly IUserContext _userContext;
        private readonly IUserBookRepository _repository;

        public MarkUserBookAsGivenCommandHandler(IUserContext userContext, IUserBookRepository repository)
        {
            _userContext = userContext;
            _repository = repository;
        }

        public async Task<Result> Handle(MarkUserBookAsGivenCommand request, CancellationToken cancellationToken)
        {
            var book = await _repository.GetAsync(request.Id, _userContext.GetUserId());

            if (book == null)
            {
                return Result.Error(new NotFoundException());
            }

            book.MarkAsGiven();

            await _repository.UpdateAsync(book);

            return Result.Success();
        }
    }

    public record MarkUserBookAsGivenCommand(int Id) : IRequest<Result>;
}
