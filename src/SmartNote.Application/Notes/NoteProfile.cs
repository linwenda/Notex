using AutoMapper;
using Newtonsoft.Json;
using SmartNote.Application.Notes.Queries;
using SmartNote.Domain.Notes.Blocks;
using SmartNote.Domain.Notes.ReadModels;

namespace SmartNote.Application.Notes;

public class NoteProfile : Profile
{
    public NoteProfile()
    {
        CreateMap<Block, BlockDto>().ForMember(d => d.Type, opt => opt.MapFrom(s => s.Type.Value));

        CreateMap<NoteReadModel, NoteDto>()
            .ConvertUsing(new NoteDtoConverter());

        CreateMap<NoteReadModel, NoteSimpleDto>();
    }

    private class NoteDtoConverter : ITypeConverter<NoteReadModel, NoteDto>
    {
        public NoteDto Convert(NoteReadModel source, NoteDto destination, ResolutionContext context)
        {
            var blocks = source.Blocks.ConvertAll(b => new BlockDto
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
        var blockType = BlockType.Of(type);

        if (blockType == BlockType.Delimiter)
        {
            return JsonConvert.DeserializeObject<Delimiter>(data);
        }

        if (blockType == BlockType.Header)
        {
            return JsonConvert.DeserializeObject<Header>(data);
        }

        if (blockType == BlockType.Image)
        {
            return JsonConvert.DeserializeObject<Image>(data);
        }

        if (blockType == BlockType.List)
        {
            return JsonConvert.DeserializeObject<List>(data);
        }

        if (blockType == BlockType.Paragraph)
        {
            return JsonConvert.DeserializeObject<IAmParagraph>(data);
        }

        return null;
    }
}