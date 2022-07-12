using AutoMapper;
using Notex.Core.Domain.Notes.ReadModels;
using Notex.Messages.Notes;

namespace Notex.Core.Profiles;

public class NoteProfile : Profile
{
    public NoteProfile()
    {
        CreateMap<NoteDetail, NoteDto>();
        CreateMap<NoteHistory, NoteHistoryDto>();
    }
}