namespace SmartNote.Application.Configuration.Security
{
    public interface IExecutionContextAccessor
    {
        Guid UserId { get; }
        bool IsAvailable { get; }
    }
}