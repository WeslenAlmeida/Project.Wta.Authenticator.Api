namespace CrossCutting.Exception.CustomExceptions
{
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException(): base(401, "UnauthorizedException")
        {
        }
    }
}