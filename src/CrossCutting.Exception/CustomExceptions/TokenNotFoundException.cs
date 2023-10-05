using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrossCutting.Exception.CustomExceptions
{
    public class TokenNotFoundException: BaseException
    {
        public TokenNotFoundException(): base(404, "TokenNotFoundException")
        {
        }
    }
}