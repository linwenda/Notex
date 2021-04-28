using System;

namespace Funzone.Application.Commands.PostDrafts
{
    public class DeletePostDraftCommand : ICommand<bool>
    {
        public Guid PostDraftId { get; set; }
    }
}