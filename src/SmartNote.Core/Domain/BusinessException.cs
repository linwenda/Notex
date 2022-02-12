namespace SmartNote.Core.Domain;

public class BusinessException : Exception
{
    public string Code { get; }

    protected BusinessException(string message) : base(message)
    {
    }

    protected BusinessException(
        string code,
        string message) :
        base(message)
    {
        Code = code;
    }
}