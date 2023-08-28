namespace Biblioteca.Helper
{
    public class ErrorDefault
    {
        public int StatusCode { get; private set; }
        public string? Message { get; private set; }
        public ErrorDefault(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
