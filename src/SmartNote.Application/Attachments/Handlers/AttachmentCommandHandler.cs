using FluentValidation.Results;
using SmartNote.Application.Attachments.Commands;
using SmartNote.Application.Configuration.Commands;
using SmartNote.Application.Configuration.Exceptions;
using SmartNote.Application.Configuration.Files;
using SmartNote.Application.Configuration.Security.Users;
using SmartNote.Domain;
using SmartNote.Domain.Attachments;

namespace SmartNote.Application.Attachments.Handlers
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

        public async Task<Guid> Handle(AddAttachmentCommand request, CancellationToken cancellationToken)
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