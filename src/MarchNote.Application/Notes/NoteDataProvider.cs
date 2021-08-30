using System;
using System.Linq;
using System.Threading.Tasks;
using MarchNote.Domain.Notes;
using MarchNote.Domain.Notes.ReadModels;
using MarchNote.Domain.SeedWork;
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

        public async Task<NoteMemberGroup> GetMemberList(Guid noteId)
        {
            var noteMembers = await _noteMemberRepository.ListAsync(n => n.NoteId == noteId);

            return new NoteMemberGroup(noteMembers.Select(n => new NoteMember(
                new UserId(n.MemberId),
                NoteMemberRole.Of(n.Role),
                n.JoinedAt,
                n.IsActive,
                n.LeaveAt)).ToList());
        }
    }
}