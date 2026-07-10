using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Operacional.BM.Exceptions
{
    public class BusinessModelException : Exception
    {
        public BusinessModelException() : 
            base() 
        {
        }

        public BusinessModelException(string mensagem) : 
            base(mensagem)
        {
        }

        public BusinessModelException(string mensagem, Exception ex) : 
            base (mensagem,ex)
        {
        }
    }
}
