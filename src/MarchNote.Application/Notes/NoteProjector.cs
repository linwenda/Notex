using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MarchNote.Domain.NoteAggregate;
using MarchNote.Domain.NoteAggregate.Events;
using MarchNote.Domain.NoteAggregate.ReadModels;
using MarchNote.Domain.SeedWork;

namespace MarchNote.Application.Notes
{
    public class NoteProjector :
        INotificationHandler<NoteCreatedEvent>,
        INotificationHandler<NoteEditedEvent>,
        INotificationHandler<NotePublishedEvent>,
        INotificationHandler<NoteDeletedEvent>,
        INotificationHandler<NoteDraftedOutEvent>,
        INotificationHandler<NoteMergedEvent>,
        INotificationHandler<NoteMemberInvitedEvent>,
        INotificationHandler<NoteMemberRemovedEvent>,
        INotificationHandler<NoteUpdatedEvent>
    {
        private readonly IRepository<NoteReadModel> _noteRepository;
        private readonly IRepository<NoteHistoryReadModel> _noteHistoryRepository;
        private readonly IRepository<NoteMemberReadModel> _noteMemberRepository;

        public NoteProjector(
            IRepository<NoteReadModel> noteRepository,
            IRepository<NoteHistoryReadModel> noteHistoryRepository,
            IRepository<NoteMemberReadModel> noteMemberRepository)
        {
            _noteRepository = noteRepository;
            _noteHistoryRepository = noteHistoryRepository;
            _noteMemberRepository = noteMemberRepository;
        }

        public async Task Handle(NoteCreatedEvent notification, CancellationToken cancellationToken)
        {
            var node = new NoteReadModel
            {
                Id = notification.NoteId,
                AuthorId = notification.AuthorId,
                CreatedAt = notification.CreatedAt,
                Title = notification.Title,
                Content = notification.Content,
                Version = 1,
                IsDeleted = false,
                Status = NoteStatus.Draft
            };

            await _noteRepository.InsertAsync(node);

            await _noteMemberRepository.InsertAsync(new NoteMemberReadModel
            {
                NoteId = notification.NoteId,
                Role = NoteMemberRole.Owner.Value,
                IsActive = true,
                JoinedAt = notification.CreatedAt,
                MemberId = notification.AuthorId
            });
        }

        public async Task Handle(NoteEditedEvent notification, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.FirstAsync(n => n.Id == notification.NoteId);

            note.Title = notification.Title;
            note.Content = notification.Content;

            if (notification.Status == NoteStatus.Published)
            {
                note.Version += 1;
            }

            await _noteRepository.UpdateAsync(note);
            await InsertNoteHistoryAsync(note);
        }

        public async Task Handle(NotePublishedEvent notification, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.FirstAsync(n => n.Id == notification.NoteId);

            note.Status = NoteStatus.Published;

            await _noteRepository.UpdateAsync(note);
            await InsertNoteHistoryAsync(note);
        }

        public async Task Handle(NoteDraftedOutEvent notification, CancellationToken cancellationToken)
        {
            var node = new NoteReadModel
            {
                Id = notification.NoteId,
                FromId = notification.FromNoteId,
                AuthorId = notification.AuthorId,
                CreatedAt = notification.CreatedAt,
                Title = notification.Title,
                Content = notification.Content,
                Version = 1,
                IsDeleted = false,
                Status = NoteStatus.Draft
            };

            await _noteMemberRepository.InsertAsync(new NoteMemberReadModel
            {
                NoteId = notification.NoteId,
                Role = NoteMemberRole.Owner.Value,
                IsActive = true,
                JoinedAt = notification.CreatedAt,
                MemberId = notification.AuthorId
            });

            await _noteRepository.InsertAsync(node);
        }


        public async Task Handle(NoteDeletedEvent notification, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.FirstAsync(n => n.Id == notification.NoteId);

            note.IsDeleted = true;

            await _noteRepository.UpdateAsync(note);
        }

        public async Task Handle(NoteMergedEvent notification, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.FirstAsync(n => n.Id == notification.NoteId);

            note.IsDeleted = true;

            await _noteRepository.UpdateAsync(note);
        }
        
        public async Task Handle(NoteUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.FirstAsync(n => n.Id == notification.NoteId);
            note.Title = notification.Title;
            note.Content = notification.Content;
            note.Version += 1;

            await _noteRepository.UpdateAsync(note);
            await InsertNoteHistoryAsync(note);
        }

        public async Task Handle(NoteMemberInvitedEvent notification, CancellationToken cancellationToken)
        {
            await _noteMemberRepository.InsertAsync(new NoteMemberReadModel
            {
                NoteId = notification.NoteId,
                Role = notification.Role,
                IsActive = true,
                JoinedAt = notification.JoinedAt,
                MemberId = notification.MemberId
            });
        }

        public async Task Handle(NoteMemberRemovedEvent notification, CancellationToken cancellationToken)
        {
            var noteMember = await _noteMemberRepository.FirstOrDefaultAsync(n =>
                n.NoteId == notification.NoteId && n.MemberId == notification.MemberId);

            if (noteMember != null)
            {
                noteMember.LeaveAt = notification.LeaveAt;
                noteMember.IsActive = false;

                await _noteMemberRepository.UpdateAsync(noteMember);
            }
        }

        private async Task InsertNoteHistoryAsync(NoteReadModel note)
        {
            if (note.Status == NoteStatus.Published)
            {
                await _noteHistoryRepository.InsertAsync(new NoteHistoryReadModel
                {
                    Id = Guid.NewGuid(),
                    NoteId = note.Id,
                    AuthorId = note.AuthorId,
                    AuditedAt = DateTime.UtcNow,
                    Title = note.Title,
                    Content = note.Content,
                    Version = note.Version
                });
            }
        }
    }
}