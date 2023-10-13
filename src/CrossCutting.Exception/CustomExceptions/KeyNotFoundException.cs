namespace CrossCutting.Exception.CustomExceptions
{
    public class KeyNotFoundException: BaseException
    {
        public KeyNotFoundException(): base(404, "KeyNotFoundExceptionBaseException")
        {
        }
    }
}