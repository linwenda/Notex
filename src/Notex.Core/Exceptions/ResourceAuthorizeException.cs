namespace Notex.Core.Exceptions;

public class ResourceAuthorizationException : Exception
{
    public ResourceAuthorizationException(string resourceName) : base(
        $"Authorization failed! Given requirements has not granted for given resource:{resourceName}")
    {
    }
}