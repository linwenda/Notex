namespace SmartNote.Domain
{
    public interface IHasModifier
    {
        Guid? LastModifierId { get; set; }
    }
}