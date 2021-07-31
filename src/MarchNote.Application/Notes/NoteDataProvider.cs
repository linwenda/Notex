using System;
using System.Linq;
using System.Threading.Tasks;
using MarchNote.Domain.NoteAggregate;
using MarchNote.Domain.NoteAggregate.ReadModels;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.SeedWork.Aggregates;
using MarchNote.Domain.Users;

namespace MarchNote.Application.Notes
{
    public class NoteDataProvider : INoteDataProvider
    {
        private readonly IRepository<NoteMemberReadModel> _noteMemberRepository;

        public NoteDataProvider(IRepository<NoteMemberReadModel> noteMemberRepository)
        {
            _noteMemberRepository = noteMemberRepository;
        }

        public async Task<NoteMemberList> GetMemberList(Guid noteId)
        {
            var noteMembers = await _noteMemberRepository.ListAsync(n => n.NoteId == noteId);

            return new NoteMemberList(noteMembers.Select(n => new NoteMember(
                new UserId(n.MemberId),
                NoteMemberRole.Of(n.Role),
                n.JoinedAt,
                n.IsActive,
                n.LeaveAt)).ToList());
        }
    }
}