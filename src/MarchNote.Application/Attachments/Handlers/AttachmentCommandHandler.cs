using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using MarchNote.Application.Attachments.Commands;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Exceptions;
using MarchNote.Domain.Attachments;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.Users;

namespace MarchNote.Application.Attachments.Handlers
{
    public class AttachmentCommandHandler : ICommandHandler<AddAttachmentCommand, Guid>
    {
        private readonly IUserContext _userContext;
        private readonly IAttachmentServer _attachmentService;
        private readonly IRepository<Attachment> _attachmentRepository;

        public AttachmentCommandHandler(
            IUserContext userContext,
            IAttachmentServer attachmentService,
            IRepository<Attachment> attachmentRepository)
        {
            _userContext = userContext;
            _attachmentService = attachmentService;
            _attachmentRepository = attachmentRepository;
        }

        public async Task<Guid> Handle(AddAttachmentCommand request,
            CancellationToken cancellationToken)
        {
            var uploadResult = await _attachmentService.UploadAsync(request.File);

            if (!uploadResult.Succeeded)
            {
                throw new ValidationException(new ValidationFailure[]
                    {new(nameof(request.File), uploadResult.Message)});
            }

            var attachment = new Attachment(
                _userContext.UserId,
                request.File.FileName,
                uploadResult.FileName,
                uploadResult.SavePath,
                request.File.ContentType);

            await _attachmentRepository.InsertAsync(attachment);

            return attachment.Id;
        }
    }
}