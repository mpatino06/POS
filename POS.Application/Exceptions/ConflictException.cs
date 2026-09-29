namespace POS.Application.Exceptions
{
    // Inherits from BusinessException because it represents a specific type of business logic error (conflict)
    public class ConflictException : BusinessException
    {
        public ConflictException(string message)
            : base(message)
        {
        }
    
    }
}
