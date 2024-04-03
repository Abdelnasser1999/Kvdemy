namespace Kvdemy.Core.Exceptions
{
    public class BusinessException : Exception
    {
        public string Message { get; set; }

        public BusinessException(string message)
        {
            Message = message;
        }
    }
}
