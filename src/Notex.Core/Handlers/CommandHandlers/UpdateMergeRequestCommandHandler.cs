using MediatR;
using Notex.Core.Domain.MergeRequests;
using Notex.Core.Domain.SeedWork;
using Notex.Core.Identity;
using Notex.Messages.MergeRequests.Commands;

namespace Notex.Core.Handlers.CommandHandlers;

public class UpdateMergeRequestCommandHandler : ICommandHandler<UpdateMergeRequestCommand>
{
    private readonly ICurrentUser _currentUser;
    private readonly IEventSourcedRepository<MergeRequest> _mergeRequestRepository;

    public UpdateMergeRequestCommandHandler(
        ICurrentUser currentUser,
        IEventSourcedRepository<MergeRequest> mergeRequestRepository)
    {
        _currentUser = currentUser;
        _mergeRequestRepository = mergeRequestRepository;
    }

    public async Task<Unit> Handle(UpdateMergeRequestCommand request, CancellationToken cancellationToken)
    {
        var mergeRequest = await _mergeRequestRepository.GetAsync(request.MergeRequestId, cancellationToken);

        mergeRequest.Update(_currentUser.Id, request.Title, request.Description);

        await _mergeRequestRepository.SaveAsync(mergeRequest, cancellationToken);

        return Unit.Value;
    }
}