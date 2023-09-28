namespace CrossCutting.Exception.CustomExceptions
{
    public class InternalErrorException : BaseException
    {
        public InternalErrorException() : base(500, "InternalErrorException")
        {
        }
    }
}