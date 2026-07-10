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


namespace FrameworkBusinessCleaner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public CleanerPaths CPaths { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            CPaths = DataFilesHelper.GetDirectories();            
            this.DataContext = CPaths;
        }

        private void btClean_Click(object sender, RoutedEventArgs e)
        {

            var response = MessageBox.Show("All non Framework files will be deleted. Are you sure?", "Alert", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (response == MessageBoxResult.Yes) {

                try
                {
                    var idxProgress = 1;
                    progress.Value = idxProgress;

                    //Cleaning BM
                    if (Directory.Exists(CPaths.BusinessModelPath))
                    {
                        var fwFiles = DataFilesHelper.BusinessModelFiles;
                        foreach (var file in Directory.GetFiles(CPaths.BusinessModelPath))
                        {
                            if (!fwFiles.Contains(Path.GetFileName(file).ToLower()))
                            {
                                File.Delete(file);
                            }
                        }
                    }

                    idxProgress++;
                    progress.Value = idxProgress;

                    //Cleaning BV
                    if (Directory.Exists(CPaths.BusinessViewPath))
                    {
                        var fwFiles = DataFilesHelper.BusinessViewFiles;
                        foreach (var file in Directory.GetFiles(CPaths.BusinessViewPath))
                        {
                            if (!fwFiles.Contains(Path.GetFileName(file).ToLower()))
                            {
                                File.Delete(file);
                            }
                        }
                    }

                    idxProgress++;
                    progress.Value = idxProgress;

                    //Cleaning Service
                    if (Directory.Exists(CPaths.ServiceBusPath))
                    {
                        var fwFiles = DataFilesHelper.ServiceBusFiles;
                        foreach (var file in Directory.GetFiles(CPaths.ServiceBusPath))
                        {
                            if (!fwFiles.Contains(Path.GetFileName(file).ToLower()))
                            {
                                File.Delete(file);
                            }
                        }
                    }


                    idxProgress++;
                    progress.Value = idxProgress;

                    //Cleaning UserInterfacePath
                    if (Directory.Exists(CPaths.UserInterfacePath))
                    {
                        var fwFiles = DataFilesHelper.UserInterfaceFiles;
                        foreach (var file in Directory.GetFiles(CPaths.UserInterfacePath))
                        {
                            if (!fwFiles.Contains(Path.GetFileName(file).ToLower()))
                            {
                                File.Delete(file);
                            }
                        }
                    }

                    idxProgress++;
                    progress.Value = idxProgress;

                    //Cleaning WebApiPath
                    if (Directory.Exists(CPaths.WebApiPath))
                    {
                        var fwFiles = DataFilesHelper.WebApiFiles;
                        foreach (var file in Directory.GetFiles(CPaths.WebApiPath))
                        {
                            if (!fwFiles.Contains(Path.GetFileName(file).ToLower()))
                            {
                                File.Delete(file);
                            }
                        }
                    }

                    idxProgress++;
                    progress.Value = idxProgress;

                    //Cleaning WebApiClientPath
                    if (Directory.Exists(CPaths.WebApiClientPath))
                    {
                        var fwFiles = DataFilesHelper.WebApiClientFiles;
                        foreach (var file in Directory.GetFiles(CPaths.WebApiClientPath))
                        {
                            if (!fwFiles.Contains(Path.GetFileName(file).ToLower()))
                            {
                                File.Delete(file);
                            }
                        }
                    }

                    idxProgress++;
                    progress.Value = idxProgress;

                    //Cleaning NugetBMPath
                    if (Directory.Exists(CPaths.NugetBMPath))
                    {
                        var fwFiles = DataFilesHelper.NugetBMFiles;
                        foreach (var file in Directory.GetFiles(CPaths.NugetBMPath))
                        {
                            if (!fwFiles.Contains(Path.GetFileName(file).ToLower()))
                            {
                                File.Delete(file);
                            }
                        }
                    }


                    idxProgress++;
                    progress.Value = idxProgress;

                    //Cleaning NugetBvPath
                    if (Directory.Exists(CPaths.NugetBvPath))
                    {
                        var fwFiles = DataFilesHelper.NugetBvFiles;
                        foreach (var file in Directory.GetFiles(CPaths.NugetBvPath))
                        {
                            if (!fwFiles.Contains(Path.GetFileName(file).ToLower()))
                            {
                                File.Delete(file);
                            }
                        }
                    }

                    idxProgress++;
                    progress.Value = idxProgress;

                    MessageBox.Show("Cleaning successful!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception excp)
                {
                    MessageBox.Show("Verify if some directory is readonly and try again!\r\nDetails:\r\n" + excp.Message, "Alert", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }
    }

    


}
