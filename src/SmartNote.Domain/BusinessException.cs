namespace SmartNote.Domain
{
    public class BusinessException : Exception, IBusinessException, IHasErrorCode
    {
        public string Code { get; }

        public BusinessException(
            string code,
            string message) :
            base(message)
        {
            Code = code;
        }
    }
}