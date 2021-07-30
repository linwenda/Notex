namespace MarchNote.Application.Configuration.Responses
{
    public interface IMarchNoteResponse
    {
        int Code { get; set; }
        string Message { get; set; }
    }

    public interface IMarchNoteResponse<T> : IMarchNoteResponse
    {
        T Data { get; set; }
    }
}