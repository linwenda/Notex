using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SmartNote.Core.Application.NoteMergeRequests.Queries;
using SmartNote.Core.Domain;
using SmartNote.Core.Domain.NoteMergeRequests;
using SmartNote.Core.Security.Users;

namespace SmartNote.Core.Application.NoteMergeRequests.Handlers
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