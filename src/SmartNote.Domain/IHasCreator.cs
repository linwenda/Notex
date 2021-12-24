namespace SmartNote.Domain
{
    public interface IHasCreator
    {
        Guid CreatorId { get; set; }
    }
}