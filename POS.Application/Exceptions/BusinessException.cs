namespace POS.Application.Exceptions
{
    public class BusinessException : Exception
    {
        public string? Code { get; set; }
        public BusinessException(string message, string? code = null) 
            : base(message)
        {
            Code = code;
        }

        public BusinessException(string message, string? code, Exception innerException) 
            : base(message, innerException)
        {
            Code = code;
        }
    }
}
