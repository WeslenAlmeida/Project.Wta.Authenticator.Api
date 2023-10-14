namespace CrossCutting.Exception.CustomExceptions
{
    public class TokenNotFoundException: BaseException
    {
        public TokenNotFoundException(): base(404, "TokenNotFoundException")
        {
        }
    }
}