namespace POS.Application.Exceptions
{
    // Inherits from BusinessException because it represents a specific type of business logic error (resource not found)
    public class NotFoundException : BusinessException 
    {
        public NotFoundException(string message) 
            : base(message)
        {
        }
    }
}
