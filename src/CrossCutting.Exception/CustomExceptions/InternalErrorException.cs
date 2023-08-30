using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrossCutting.Exception.CustomExceptions
{
    public class InternalErrorException : BaseException
    {
        public InternalErrorException() : base(500, "InternalErrorException")
        {
        }
    }
}