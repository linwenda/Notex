using System;
using Funzone.Application.Configuration.Commands;

namespace Funzone.Application.PostDrafts.Commands
{
    public class DeletePostDraftCommand : ICommand<bool>
    {
        public Guid PostDraftId { get; set; }
    }
}