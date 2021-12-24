using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SmartNote.Application.Configuration.Queries;
using SmartNote.Application.Configuration.Security.Users;
using SmartNote.Application.NoteMergeRequests.Queries;
using SmartNote.Domain;
using SmartNote.Domain.NoteMergeRequests;

namespace SmartNote.Application.NoteMergeRequests.Handlers
{
    public class NoteMergeRequestQueryHandler :
        IQueryHandler<GetNoteMergeRequestQuery, IEnumerable<NoteMergeRequestDto>>
    {
        private readonly IRepository<NoteMergeRequest> _noteMergeRequestRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;

        public NoteMergeRequestQueryHandler(
            IRepository<NoteMergeRequest> noteMergeRequestRepository,
            ICurrentUser currentUser, 
            IMapper mapper)
        {
            _noteMergeRequestRepository = noteMergeRequestRepository;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NoteMergeRequestDto>> Handle(GetNoteMergeRequestQuery request,
            CancellationToken cancellationToken)
        {
            return await _noteMergeRequestRepository.Queryable
                .Where(n => n.CreatorId == _currentUser.Id && n.Status == request.Status)
                .ProjectTo<NoteMergeRequestDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }
    }
}