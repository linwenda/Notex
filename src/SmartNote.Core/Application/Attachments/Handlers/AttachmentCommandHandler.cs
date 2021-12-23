using FluentValidation.Results;
using SmartNote.Core.Application.Attachments.Commands;
using SmartNote.Core.Domain;
using SmartNote.Core.Domain.Attachments;
using SmartNote.Core.Files;
using SmartNote.Core.Security.Users;

namespace SmartNote.Core.Application.Attachments.Handlers
{
    public class AttachmentCommandHandler : ICommandHandler<AddAttachmentCommand, Guid>
    {
        private readonly ICurrentUser _currentUser;
        private readonly IFileService _fileService;
        private readonly IRepository<Attachment> _attachmentRepository;

        public AttachmentCommandHandler(
            ICurrentUser currentUser,
            IFileService fileService,
            IRepository<Attachment> attachmentRepository)
        {
            _currentUser = currentUser;
            _fileService = fileService;
            _attachmentRepository = attachmentRepository;
        }

        public async Task<Guid> Handle(AddAttachmentCommand request,
            CancellationToken cancellationToken)
        {
            var uploadResult = await _fileService.UploadAsync(request.File);

            if (!uploadResult.Succeeded)
            {
                throw new ValidationException(new ValidationFailure[]
                    { new(nameof(request.File), uploadResult.Message) });
            }

            var attachment = new Attachment(
                _currentUser.Id,
                request.File.FileName,
                uploadResult.FileName,
                uploadResult.SavePath,
                request.File.ContentType);

            await _attachmentRepository.InsertAsync(attachment);

            return attachment.Id;
        }
    }
}