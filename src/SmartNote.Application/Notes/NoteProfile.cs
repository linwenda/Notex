using AutoMapper;
using SmartNote.Application.Notes.Queries;
using SmartNote.Core.Extensions;
using SmartNote.Domain.Notes.Blocks;
using SmartNote.Domain.Notes.ReadModels;

namespace SmartNote.Application.Notes;

public class NoteProfile : Profile
{
    public NoteProfile()
    {
        CreateMap<NoteReadModel, NoteDto>().ConvertUsing(new NoteDtoConverter());
        CreateMap<NoteReadModel, NoteSimpleDto>();
    }

    private class NoteDtoConverter : ITypeConverter<NoteReadModel, NoteDto>
    {
        public NoteDto Convert(NoteReadModel source, NoteDto destination, ResolutionContext context)
        {
            var blocks = source.Content.ConvertAll(b => new BlockDto
            {
                Id = b.Id,
                Type = b.Type,
                Data = DeserializeBlockData(b.Type, b.Data) ?? throw new NullReferenceException()
            });

            return new NoteDto
            {
                Blocks = blocks,
                Id = source.Id,
                Status = source.Status,
                Title = source.Title,
                Version = source.Version,
                AuthorId = source.AuthorId,
                CreationTime = source.CreationTime,
                ForkId = source.ForkId,
                IsDeleted = source.IsDeleted,
                SpaceId = source.SpaceId
            };
        }
    }

    private static IAmBlockData DeserializeBlockData(string type, string data)
    {
        return type switch
        {
            "delimiter" => data.FromJson<Delimiter>(),
            "header" => data.FromJson<Header>(),
            "image" => data.FromJson<Image>(),
            "list" => data.FromJson<List>(),
            "paragraph" => data.FromJson<Paragraph>(),
            _ => null
        };
    }
}