using System;
using Funzone.Application.Configuration.Commands;

namespace Funzone.Application.PostDrafts.Commands
{
    public class AddPostDraftCommand : ICommand<bool>
    {
        public Guid ZoneId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
    }
}