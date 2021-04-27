using System;

namespace Funzone.Application.Commands.PostDrafts
{
    public class PostToDraftCommand : ICommand<bool>
    {
        public Guid PostDraftId { get; set; }
    }
}