using System;
using System.Net;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel.DataAnnotations;
using Linx.Tools;

namespace Linx.Tools
{
    public class LinxRequiredAttribute : RequiredAttribute
    {
        public bool ForceAlways { get; set; }

        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((ForceAlways || (!validationContext.IsNull() && !validationContext.MemberName.IsNullOrEmpty() && validationContext.ObjectType.GetProperty(validationContext.MemberName).PropertyType.Name == "String")) && value.IsNullOrEmpty())
            {
                if (!validationContext.ObjectInstance.IsNull())
                {
                    IBusinessContextControl ctxControl = validationContext.ObjectInstance.GetPropertyValue("BusinessControl") as IBusinessContextControl;
                    if (!ctxControl.IsNull())
                    {
                        if (ctxControl.ControlStatus == ToolbarStatus.Validating)
                        {
                            return new ValidationResult(System.Windows.Application.Current.GetResource((this.ErrorMessage.IsNullOrEmpty() ? "RequiredMessage" : this.ErrorMessage)), new string[] { validationContext.MemberName });
                        }
                    }
                }
            }

            return ValidationResult.Success;
        }
                
    }

    public class LinxStringLength : StringLengthAttribute
    {
        public LinxStringLength(int maximumLength)
            : base(maximumLength)
        {

        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (base.IsValid(value, validationContext) != ValidationResult.Success)
            {
                if (!validationContext.IsNull() && !validationContext.ObjectInstance.IsNull())
                {
                    IBusinessContextControl ctxControl = validationContext.ObjectInstance.GetPropertyValue("BusinessControl") as IBusinessContextControl;
                    if (!ctxControl.IsNull())
                    {
                        if (ctxControl.ControlStatus == ToolbarStatus.Validating)
                        {
                            return new ValidationResult(String.Format(System.Windows.Application.Current.GetResource((this.ErrorMessage.IsNullOrEmpty() ? "StringLengthMessage" : this.ErrorMessage)), this.MaximumLength), new string[] { validationContext.MemberName });
                        }
                    }
                }
            }

            return ValidationResult.Success;
        }        
        
    }


}
