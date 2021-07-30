namespace MarchNote.Application.Configuration.Responses
{
    public class MarchNoteResponse : IMarchNoteResponse
    {
        public int Code { get; set; } = DefaultResponseCode.Succeeded;
        public string Message { get; set; }
    }

    public class MarchNoteResponse<T> : IMarchNoteResponse<T>
    {
        public MarchNoteResponse()
        {
        }
        
        public MarchNoteResponse(T data)
        {
            Data = data;
        }

        public int Code { get; set; } = DefaultResponseCode.Succeeded;
        public string Message { get; set; }
        public T Data { get; set; }
    }
}