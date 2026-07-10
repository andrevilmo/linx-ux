using System;
using System.Collections.Generic;
using System.Text;

namespace Linx.DS.Core.Data
{
    public class DomainException : Exception
    {
        public DomainException() : base()
        {

        }

        public DomainException(string message) : base(message)
        {

        }

        public DomainException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
