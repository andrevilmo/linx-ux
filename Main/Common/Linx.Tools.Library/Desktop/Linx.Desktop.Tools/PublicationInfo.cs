using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Linx.Tools
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class LinxPublicationViewAttribute : System.Attribute
    {
        public string PrimaryKeys { get; set; }
        public string EdmName { get; set; }
        public bool IsUpdatable { get; set; }
    }        

    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class LinxPublicationLookUpAttribute : System.Attribute
    {
        public string NameSpace { get; set; }
        public string ClassName { get; set; }
        public string EntityName { get; set; }
        public bool AllowsMaintenance { get; set; }
    }

    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class LinxPublicationFilterAttribute : System.Attribute
    {
        public string NameSpace { get; set; }
        public string ClassName { get; set; }
        public string EntityName { get; set; }
    }

    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class LinxPublicationFieldAttribute : System.Attribute
    {
        public bool IsSuggestion { get; set; }
        public string EdmKey { get; set; }
        public string LookUpInfo { get; set; }
    }
}
