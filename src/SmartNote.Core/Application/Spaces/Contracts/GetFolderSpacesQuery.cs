namespace SmartNote.Core.Application.Spaces.Contracts
{
    public class GetFolderSpacesQuery : IQuery<IEnumerable<SpaceDto>>
    {
        public Guid SpaceId { get; }

        public GetFolderSpacesQuery(Guid spaceId)
        {
            SpaceId = spaceId;
        }
    }
}