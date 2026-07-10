using Linx.Internet.Application.WinHost.Mvvm;
using CefSharp.Wpf;
using System.ComponentModel;
using System.Windows;

namespace Linx.Internet.Application.WinHost.ViewModels
{
    public class MainViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IWpfWebBrowser webBrowser;
        public IWpfWebBrowser WebBrowser
        {
            get { return webBrowser; }
            set { PropertyChanged.ChangeAndNotify(ref webBrowser, value, () => WebBrowser); }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { PropertyChanged.ChangeAndNotify(ref title, value, () => Title); }
        }

        public MainViewModel()
        {
            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Title")
            {
                System.Windows.Application.Current.MainWindow.Title = Title;
            }
        }
    }
}
