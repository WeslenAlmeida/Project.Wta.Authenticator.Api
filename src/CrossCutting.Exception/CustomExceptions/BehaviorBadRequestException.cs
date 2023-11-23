namespace CrossCutting.Exception.CustomExceptions
{
    public class BehaviorBadRequestException : BaseException
    {
        public BehaviorBadRequestException(object message): base(400, message)
        {
        }
    }
}