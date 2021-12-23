using AutoMapper;
using Newtonsoft.Json;
using SmartNote.Core.Application.Notes.Queries;
using SmartNote.Core.Domain.Notes.Blocks;
using SmartNote.Core.Domain.Notes.ReadModels;

namespace SmartNote.Core.Application.Notes;

public class NoteProfile : Profile
{
    public NoteProfile()
    {
        CreateMap<BlockDto, Block>().ConvertUsing(new BlockConverter());

        CreateMap<Block, BlockDto>().ForMember(d => d.Type, opt => opt.MapFrom(s => s.Type.Value));

        CreateMap<NoteReadModel, NoteDto>()
            .ConvertUsing(new NoteDtoConverter());
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

    private class BlockConverter : ITypeConverter<BlockDto, Block>
    {
        public Block Convert(BlockDto source, Block destination, ResolutionContext context)
        {
            IAmBlockData blockData = null;

            var blockType = BlockType.Of(source.Type);

            if (blockType == BlockType.Delimiter)
            {
                blockData = source.Data as IAmDelimiter;
            }
            else if (blockType == BlockType.Header)
            {
                blockData = source.Data as IAmHeader;
            }
            else if (blockType == BlockType.Image)
            {
                blockData = source.Data as IAmImage;
            }
            else if (blockType == BlockType.List)
            {
                blockData = source.Data as IAmList;
            }
            else if (blockType == BlockType.Paragraph)
            {
                blockData = source.Data as IAmParagraph;
            }

            if (blockData == null)
            {
                throw new ArgumentNullException(nameof(blockData));
            }

            return new Block(source.Id, blockType, blockData);
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