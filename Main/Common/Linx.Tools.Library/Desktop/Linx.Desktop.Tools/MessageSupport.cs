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

namespace Linx
{
    public delegate void SingleMsgMethodHandler(Linx.MessageBoxResult messageBoxResult);

    public enum MessageBoxButton
    {
        AbortRetryIgnore = 1,
        Cancel = 2,
        OK = 3,
        OKCancel = 4,
        RetryCancel = 5,
        YesNo = 6,
        YesNoCancel = 7
    }

    public enum MessageBoxResult
    {
        Abort = 1,
        Cancel = 2,
        Ignore = 3,
        No = 4,
        None = 5,
        OK = 6,
        Yes = 7,
        Retry = 8
    }

    public enum MessageBoxImage
    {
        Asterisk = 1,
        Error = 2,
        Exclamation = 3,
        Hand = 4,
        Information = 5,
        None = 6,
        Question = 7,
        Stop = 8,
        Warning = 9
    }
}
