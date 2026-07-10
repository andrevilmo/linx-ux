using Linx.TelerikReportDesigner.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
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

namespace Linx.TelerikReportDesigner.Publisher
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

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
         
        }

        private void PopulateReportSources()
        {
            var fileCollection = new List<ReportFile>();

            if (Directory.Exists(txtSelectedFolder.Text))
            {
                var files = Directory.GetFiles(txtSelectedFolder.Text, "*.trdx");

                if (files != null)
                    foreach (var file in files)
                        fileCollection.Add(new ReportFile()
                        {
                            FilePath = file,
                            Name = System.IO.Path.GetFileName(file),
                        });
            }

            lbxResourcesOprions.ItemsSource = fileCollection;
        }

        private void PublishReportsZipFile()
        {
            var path = this.OpenFolderBrowserDialog();

            if (!string.IsNullOrEmpty(path))
            {
                var zipFileName = System.IO.Path.Combine(path, string.Format("Linx_Reports_{0}.zip", DateTime.Now.ToString("dd_MM_yyyy_hh_ss")));

                using (var zipFile = ZipFile.Open(zipFileName, ZipArchiveMode.Create))
                {
                    foreach (var reportFile in lbxResourcesSelected.ItemsSource as List<ReportFile>)
                    {
                        try
                        {
                            var pathFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), reportFile.Name);

                            var report = Utils.GetReport(reportFile.FilePath);
                            report.Save(pathFile);

                            zipFile.CreateEntryFromFile(pathFile, reportFile.Name, CompressionLevel.Optimal);
                        }
                        catch { }
                    }
                }

                MessageBox.Show("Relatório(s) importado(s) com sucesso.");
            }
        }

        private void PublishReportsDevelopmentEnvironment()
        {
            var directory = this.GetDirectoryInfo();

            if (!string.IsNullOrEmpty(directory))
            {
                foreach (var reportFile in lbxResourcesSelected.ItemsSource as List<ReportFile>)
                {
                    try
                    {
                        var pathFile = System.IO.Path.Combine(directory, reportFile.Name);
                        var report = Utils.GetReport(reportFile.FilePath);

                        report.Save(pathFile);
                    }
                    catch { }
                }

                MessageBox.Show("Relatório(s) importado(s) com sucesso.");
            }
        }

        private string OpenFolderBrowserDialog()
        {
            var path = string.Empty;

            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                    path = dialog.SelectedPath;
            }

            return path;
        }

        private void RefreshDataView(object source)
        {
            var view = CollectionViewSource.GetDefaultView(source);
            view.Refresh();
        }

        private List<ReportFile> GetItemsSource(ListView listView)
        {
            var itemsSource = new List<ReportFile>();

            if (listView.ItemsSource != null)
                itemsSource = listView.ItemsSource as List<ReportFile>;

            return itemsSource;
        }

        private List<ReportFile> GetSelectedItems(ListView listView)
        {
            var selectedItems = new List<ReportFile>();

            if (listView.SelectedItems != null)
            {
                var a = listView.SelectedItems as List<ReportFile>;
                selectedItems = (List<ReportFile>)listView.SelectedItems;
            }

            return selectedItems;
        }

        public string GetDirectoryInfo()
        {
            return @"C:\Linx Workspace\Linx Framework\Dev\Binary\Service\bin\";
        }

        #region Event Methods

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Environment.Is64BitOperatingSystem)
                this.txtSelectedFolder.Text = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            else
                this.txtSelectedFolder.Text = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            this.txtSelectedFolder.Text += @"\Telerik\Reporting Q1 2015\Report Designer\Linx Reports";



            if (Directory.Exists(GetDirectoryInfo()))
                ckbDevPublish.Visibility = System.Windows.Visibility.Visible;

            this.PopulateReportSources();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (lbxResourcesOprions.SelectedItems != null &&
                lbxResourcesOprions.SelectedItems.Cast<ReportFile>().Any())
            {
                var resourcesSelected = this.GetItemsSource(lbxResourcesSelected);

                foreach (ReportFile file in lbxResourcesOprions.SelectedItems)
                {
                    resourcesSelected.Add(file);
                    this.GetItemsSource(lbxResourcesOprions).RemoveAll(x => x.Name == file.Name);
                }

                lbxResourcesSelected.ItemsSource = resourcesSelected;

                this.RefreshDataView(lbxResourcesOprions.ItemsSource);
                this.RefreshDataView(lbxResourcesSelected.ItemsSource);
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (lbxResourcesSelected.SelectedItems != null &&
                lbxResourcesSelected.SelectedItems.Cast<ReportFile>().Any())
            {
                var resourcesOptions = this.GetItemsSource(lbxResourcesOprions);

                foreach (ReportFile file in lbxResourcesSelected.SelectedItems)
                {
                    resourcesOptions.Add(file);
                    this.GetItemsSource(lbxResourcesSelected).RemoveAll(x => x.Name == file.Name);
                }

                this.RefreshDataView(lbxResourcesOprions.ItemsSource);
                this.RefreshDataView(lbxResourcesSelected.ItemsSource);
            }
        }

        private void btnSelectFolder_Click(object sender, RoutedEventArgs e)
        {
            var path = this.OpenFolderBrowserDialog();

            if (!string.IsNullOrEmpty(path))
            {
                txtSelectedFolder.Text = path;
                this.PopulateReportSources();
            }
        }
   

        private void btnPublish_Click(object sender, RoutedEventArgs e)
        {
            if (lbxResourcesSelected.ItemsSource != null)
            {
                if ((!ckbDevPublish.IsChecked.HasValue || ckbDevPublish.IsChecked.HasValue) && !ckbDevPublish.IsChecked.Value)
                    this.PublishReportsZipFile();
                else
                    this.PublishReportsDevelopmentEnvironment();
            }
            else
                MessageBox.Show("Selecione ao menos um relatório para exportar!");
        }

        private void btnAll_Click(object sender, RoutedEventArgs e)
        {
            if (lbxResourcesOprions.ItemsSource != null &&
                lbxResourcesOprions.ItemsSource.Cast<ReportFile>().Any())
            {
                var resourcesSelected = this.GetItemsSource(lbxResourcesSelected);

                foreach (ReportFile file in lbxResourcesOprions.ItemsSource)
                    resourcesSelected.Add(file);

                foreach (var file in resourcesSelected)
                    this.GetItemsSource(lbxResourcesOprions).RemoveAll(x => x.Name == file.Name);

                lbxResourcesSelected.ItemsSource = resourcesSelected;

                this.RefreshDataView(lbxResourcesOprions.ItemsSource);
                this.RefreshDataView(lbxResourcesSelected.ItemsSource);
            }
            else if (lbxResourcesSelected.ItemsSource != null &&
                     lbxResourcesSelected.ItemsSource.Cast<ReportFile>().Any())
            {
                var resourcesOptions = this.GetItemsSource(lbxResourcesOprions);

                foreach (ReportFile file in lbxResourcesSelected.ItemsSource)
                    resourcesOptions.Add(file);

                foreach (var file in resourcesOptions)
                    this.GetItemsSource(lbxResourcesSelected).RemoveAll(x => x.Name == file.Name);

                lbxResourcesOprions.ItemsSource = resourcesOptions;

                this.RefreshDataView(lbxResourcesOprions.ItemsSource);
                this.RefreshDataView(lbxResourcesSelected.ItemsSource);
            }
        }
        #endregion
    }
}
