namespace CrossCutting.Exception
{
    public abstract class BaseException : System.Exception
    {
        public int StatusCode { get; set; }
        public object? CustomMessage { get; set; }

        public BaseException(int statusCode, string customException)
        {
            StatusCode = statusCode;
            CustomMessage = ConfigurationResources.GetExceptionMessage(customException);
        }
    }
}