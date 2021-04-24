namespace Funzone.Domain.SharedKernel
{
    public interface ICanSoftDeleted
    {
        bool IsDeleted { get; }
    }
}