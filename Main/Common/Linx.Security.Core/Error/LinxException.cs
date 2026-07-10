using System;
using System.Collections.Generic;
using System.Text;

namespace Linx.Security.Core
{
    public class LinxAuthorizationException : Exception
    {
        public LinxAuthorizationException(string message) : base (message)
        {

        }
    }
}
