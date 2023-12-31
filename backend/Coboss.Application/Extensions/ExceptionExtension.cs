namespace Coboss.Application.Extensions
{
    public static class ExceptionExtension
    {
        public static string ToMessage(this Exception exception)
        {
            string message = $"{exception.Message}\n{exception.StackTrace}";
            if(exception.InnerException != null)
            {
                message += $"\n{exception.InnerException.Message}\n{exception.InnerException.StackTrace}";
            }
            return message;
        }
    }
}
