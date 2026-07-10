using System;
using System.Windows;
using System.Windows.Threading;

namespace Linx.Tools
{
    public partial class DelegateCommand<T>
    {
        /// <summary>
        /// Raises <see cref="CanExecuteChanged"/> on the UI thread so every command invoker can requery to check if the command can execute.
        /// </summary>
        protected virtual void OnCanExecuteChanged()
        {
            Dispatcher dispatcher = null;
            if (Application.Current != null)
            {
                dispatcher = Application.Current.Dispatcher;
            }

            EventHandler canExecuteChangedHandler = CanExecuteChanged;
            if (canExecuteChangedHandler != null)
            {
                if (dispatcher != null && !dispatcher.CheckAccess())
                {
                    dispatcher.BeginInvoke(DispatcherPriority.Normal,
                                           (Action)OnCanExecuteChanged);
                }
                else
                {
                    canExecuteChangedHandler(this, EventArgs.Empty);
                }
            }
        }
    }
}
