namespace Todo_API2.Domain.Exceptions
{
    public class TodoItemServiceException : Exception
    {
        public TodoItemServiceException()
        {
        }

        public TodoItemServiceException(string message)
            : base(message)
        {
        }

        public TodoItemServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
