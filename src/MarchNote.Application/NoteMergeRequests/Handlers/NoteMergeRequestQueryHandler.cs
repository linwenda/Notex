using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MarchNote.Application.Configuration.Queries;
using MarchNote.Application.NoteMergeRequests.Queries;
using MarchNote.Domain.NoteMergeRequests;
using MarchNote.Domain.Shared;
using MarchNote.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace MarchNote.Application.NoteMergeRequests.Handlers
{
    public class NoteMergeRequestQueryHandler :
        IQueryHandler<GetNoteMergeRequestQuery, IEnumerable<NoteMergeRequestDto>>
    {
        private readonly IRepository<NoteMergeRequest> _noteMergeRequestRepository;
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;

        public NoteMergeRequestQueryHandler(IRepository<NoteMergeRequest> noteMergeRequestRepository,
            IUserContext userContext, IMapper mapper)
        {
            _noteMergeRequestRepository = noteMergeRequestRepository;
            _userContext = userContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NoteMergeRequestDto>> Handle(GetNoteMergeRequestQuery request,
            CancellationToken cancellationToken)
        {
            return await _noteMergeRequestRepository.Queryable
                .Where(n => n.CreatorId == _userContext.UserId && n.Status == request.Status)
                .ProjectTo<NoteMergeRequestDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }
    }
}