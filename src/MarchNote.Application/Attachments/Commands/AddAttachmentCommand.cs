using System;
using System.IO;
using System.Linq;
using FluentValidation;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Responses;
using Microsoft.AspNetCore.Http;

namespace MarchNote.Application.Attachments.Commands
{
    public class AddAttachmentCommand : ICommand<MarchNoteResponse<Guid>>
    {
        public AddAttachmentCommand(IFormFile file)
        {
            File = file;
        }

        public IFormFile File { get; }
    }

    public class AddAttachmentCommandValidator : AbstractValidator<AddAttachmentCommand>
    {
        private readonly string[] _permittedExtensions = {".png", ".jpg"};

        public AddAttachmentCommandValidator()
        {
            RuleFor(v => v.File)
                .NotNull()
                .WithMessage("Invalid file. Check a a file extension or the size less than 2 MB");

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