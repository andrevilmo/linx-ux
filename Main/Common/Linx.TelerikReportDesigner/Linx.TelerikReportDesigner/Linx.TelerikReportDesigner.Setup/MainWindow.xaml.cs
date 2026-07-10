using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Linx.TelerikReportDesigner.Setup
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdateProgressbar(double value)
        {
            if (pbInstall.Visibility == System.Windows.Visibility.Hidden)
                pbInstall.Visibility = System.Windows.Visibility.Visible;

            for (int i = 0; i < value; i++)
                pbInstall.Value += 1D;
        }

        private void UpdateLog(string text)
        {
            txbLog.Text += string.Format("{0}. {1}{2}",
                text, Environment.NewLine, Environment.NewLine);
        }

        private void EnableVisibilityCloseButton()
        {
            btnCancel.Visibility = System.Windows.Visibility.Hidden;
            btnInstall.Visibility = System.Windows.Visibility.Hidden;

            btnClose.Visibility = System.Windows.Visibility.Visible;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnInstall_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnInstall.IsEnabled = false;

                var installer = new Installer();

                var directoryTelerikReportDesigner = string.Format("{0}\\Telerik\\Reporting Q1 2015\\Report Designer\\",
                    Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86));

                var directoryTelerikReportTemplates = string.Format("{0}\\Telerik Report Designer\\Templates\\",
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

                this.UpdateProgressbar(10D);

                this.UpdateLog("Iniciando instalação Telerik Report");

                installer.InstallTelerikReport();

                if (File.Exists(string.Format("{0}Telerik.ReportDesigner.exe.config", directoryTelerikReportDesigner)))
                {
                    this.UpdateProgressbar(30D);

                    this.UpdateLog("Iniciando instalação Linx Report");

                    installer.ConfigTelerikReporting(directoryTelerikReportDesigner, directoryTelerikReportTemplates);

                    this.UpdateProgressbar(30D);

                    this.UpdateLog("Iniciando instalação Publish Report");

                    installer.InstallPublish(directoryTelerikReportDesigner);

                    this.UpdateProgressbar(30D);
                }

                this.EnableVisibilityCloseButton();
            }
            catch (Exception ex)
            {
                txbLog.Text = ex.Message;
            }
            finally
            {
                btnInstall.IsEnabled = true;
            }
        }
    }
}
