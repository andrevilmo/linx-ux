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

namespace TelerikReport.Publish
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

            var zipFileName = System.IO.Path.Combine(path, string.Format("Linx_Reports_{0}.rar", DateTime.Now.ToString("dd_MM_yyyy_hh_ss")));

            using (var zipFile = System.IO.Compression.ZipFile.Open(zipFileName, ZipArchiveMode.Create))
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
        }

        private void PublishReportsDevelopmentEnvironment()
        {
            var directory = this.GetDirectoryInfo();

            if (!string.IsNullOrEmpty(directory))
            {
                var path = string.Format("{0}\\Bin\\", directory);

                foreach (var reportFile in lbxResourcesSelected.ItemsSource as List<ReportFile>)
                {
                    try
                    {
                        var pathFile = System.IO.Path.Combine(path, reportFile.Name);
                        var report = Utils.GetReport(reportFile.FilePath);

                        report.Save(pathFile);
                    }
                    catch
                    {

                    }
                }
            }
        }

        private string OpenFolderBrowserDialog()
        {
            var path = string.Empty;

            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                dialog.Description = "Descrição";
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
            this.PopulateReportSources();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (lbxResourcesOprions.SelectedItems != null)
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
            if (lbxResourcesSelected.SelectedItems != null)
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

        private void txtSelectedFolder_GotFocus(object sender, RoutedEventArgs e)
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
                if ((!cbxDevPublish.IsChecked.HasValue || cbxDevPublish.IsChecked.HasValue) && !cbxDevPublish.IsChecked.Value)
                    this.PublishReportsZipFile();
                else
                    this.PublishReportsDevelopmentEnvironment();

                MessageBox.Show("Relatório(s) importado(s) com sucesso.");
            }
            else
                MessageBox.Show("Selecione ao menos um relatório para exportar!");
        }

        #endregion
    }
}
