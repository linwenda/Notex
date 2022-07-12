using System;
using System.Threading.Tasks;
using Notex.Core.Domain.Notes;
using Notex.Messages.Notes;
using Notex.UnitTests.Spaces;

namespace Notex.UnitTests.Notes;

public static class NoteTestHelper
{
    public static async Task<Note> CreateNote(NoteOptions options)
    {
        var user = new FakeCurrentUser();

        var space = await SpaceTestHelper.CreateSpace(new SpaceOptions
        {
            UserId = user.Id
        });

        var note = space.CreateNote(
            options.Title ?? "microsoft",
            options.Content ?? ".Net 6 new feature",
            options.Status);

        return options.IsClone ? note.Clone(user.Id, space.Id) : note;
    }
}

public class NoteOptions
{
    public Guid? UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public NoteStatus Status { get; set; } = NoteStatus.Published;
    public bool IsClone { get; set; }
}