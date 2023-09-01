namespace CrossCutting.Exception.CustomExceptions
{
    public class UserNotFoundException : BaseException
    {
        public UserNotFoundException() : base(404, "UserNotFoundException")
        {
        }
    }
}