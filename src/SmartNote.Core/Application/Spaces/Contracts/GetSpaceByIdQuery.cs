namespace SmartNote.Core.Application.Spaces.Contracts
{
    public class GetSpaceByIdQuery : IQuery<SpaceDto>
    {
        public Guid SpaceId { get; }

        public GetSpaceByIdQuery(Guid spaceId)
        {
            SpaceId = spaceId;
        }
    }
}