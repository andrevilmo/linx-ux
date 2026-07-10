using System.Windows.Controls;
using Linx.Internet.Application.WinHost;
using Linx.Internet.Application.WinHost.ViewModels;

namespace Linx.Internet.Application.WinHost.Views
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            
            //cwb.ShowDevTools();

            cwb.Address = System.Configuration.ConfigurationManager.AppSettings.GetValue("ApplicationUrl", "http://localhost:56650/");

            //cwb.ShowDevTools();
        }
    }
}
