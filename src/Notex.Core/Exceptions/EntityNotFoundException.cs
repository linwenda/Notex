namespace Notex.Core.Exceptions;

public class EntityNotFoundException : Exception
{
    public Type EntityType { get; }
    public object Id { get; }

    public EntityNotFoundException()
    {
    }

    public EntityNotFoundException(Type entityType, object id) : base(id == null
        ? $"Entity not found. Entity type: {entityType.FullName}"
        : $"Entity not found. Entity type: {entityType.FullName}, id: {id}")
    {
        EntityType = entityType;
        Id = id;
    }
}