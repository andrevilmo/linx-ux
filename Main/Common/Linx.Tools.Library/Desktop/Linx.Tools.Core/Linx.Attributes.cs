using System;
using System.ComponentModel.DataAnnotations;

namespace Linx.Tools
{
    public class LinxRequiredAttribute : RequiredAttribute
    {       
                
    }

    public class LinxStringLength : StringLengthAttribute
    {
        public LinxStringLength(int maximumLength)
            : base(maximumLength)
        {

        }        
        
    }

}
