namespace Notex.Core.Aggregates.Spaces.DomainServices;

public interface ISpaceChecker
{
    bool IsUniqueNameInUserSpace(Guid userId, string name);
}