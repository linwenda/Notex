namespace Notex.Messages.Spaces.Queries;

public class GetSpaceQuery : IQuery<SpaceDto>
{
    public Guid SpaceId { get; }

    public GetSpaceQuery(Guid spaceId)
    {
        SpaceId = spaceId;
    }
}