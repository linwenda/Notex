using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace SmartNote.Core.Application.Attachments.Commands
{
    public class AddAttachmentCommand : ICommand<Guid>
    {
        public AddAttachmentCommand(IFormFile file)
        {
            File = file;
        }

        public IFormFile File { get; }
    }

    public class AddAttachmentCommandValidator : AbstractValidator<AddAttachmentCommand>
    {
        private readonly string[] _permittedExtensions = { ".png", ".jpg" };

        public AddAttachmentCommandValidator()
        {
            RuleFor(v => v.File)
                .NotNull();

            RuleFor(v => v.File)
                .Must(f =>
                {
                    var ext = Path.GetExtension(f.FileName).ToLowerInvariant();
                    return !string.IsNullOrEmpty(ext) && _permittedExtensions.Contains(ext);
                })
                .When(f => f.File != null)
                .WithMessage("The file extension is invalid, only support PNG and JPG");
        }
    }
}