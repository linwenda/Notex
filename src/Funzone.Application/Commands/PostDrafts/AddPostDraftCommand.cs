using System;

namespace Funzone.Application.Commands.PostDrafts
{
    public class AddPostDraftCommand : ICommand<bool>
    {
        public Guid ZoneId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
    }
}