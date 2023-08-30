using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrossCutting.Exception.CustomExceptions
{
    public class UserNotFoundException : BaseException
    {
        public UserNotFoundException() : base(404, "UserNotFoundException")
        {
        }
    }
}