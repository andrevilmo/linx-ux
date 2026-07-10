using EnvDTE;
using System;
using System.Collections.Generic;
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
using Linx.Tools;
using System.Windows.Threading;
using System.Windows.Forms;
using Microsoft.TeamFoundation.VersionControl.Client;
using Microsoft.TeamFoundation.Client;
using System.Runtime.InteropServices;
using System.ComponentModel;
using MvcJqGrid.Tests.JavascriptCompiler;
using System.IO;

namespace Linx.Build.Automation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        private List<SolutionInfo> SolutionInfoList = new List<SolutionInfo>();
        private string projectName = "", businessAlerts = "";
        private int lastDesignerIndex = 0;
        const int _MAX_DESIGNER_INDEX = 5;
        private bool isCancelled = false;

        public MainWindow()
        {
            InitializeComponent();
            lbSolutionList.ItemsSource = SolutionInfoList;
        }

        private void PopulateSolutios()
        {
            ClearProcess();

            if (System.IO.Directory.Exists(this.TxSolutionFolder.Text))
            {
                Mouse.SetCursor(System.Windows.Input.Cursors.Wait);
                try
                {
                    var dsolFiles = System.IO.Directory.GetFiles(this.TxSolutionFolder.Text, "*.sln", System.IO.SearchOption.AllDirectories);
                    foreach (var file in dsolFiles.OrderBy(e => System.IO.Path.GetFileNameWithoutExtension(e)))
                    {
                        SolutionInfoList.Add(new SolutionInfo() { IsSelected = true, Name = System.IO.Path.GetFileNameWithoutExtension(file), Path = file });
                    }
                }
                catch (UnauthorizedAccessException ua)
                {
                    System.Windows.MessageBox.Show(ua.Message, "Unauthorized Access", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error, System.Windows.MessageBoxResult.OK);
                }
                finally
                {
                    lbSolutionList.ItemsSource = SolutionInfoList;
                    Mouse.SetCursor(System.Windows.Input.Cursors.Arrow);
                }
            }

            AdjustSelectionInfo();
        }

        private void AdjustSelectionInfo()
        {
            this.LblSolutionInfo.Content = "Solutions (" + SolutionInfoList.Count(s => s.IsSelected) + "):";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Open a folder whith Business Solutions";
                dialog.ShowNewFolderButton = false;
                dialog.RootFolder = Environment.SpecialFolder.MyComputer;
                dialog.SelectedPath = this.TxSolutionFolder.Text;

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.TxSolutionFolder.Text = dialog.SelectedPath;
                }
            }

        }

        private bool SaveAllDocuments(EnvDTE.DTE vs, string stopDesigner)
        {
            foreach (Project folder in vs.Solution.Projects)
            {
                foreach (var eadProject in GetEadProjects(folder))
                {
                    if (eadProject != null)
                    {
                        if (isCancelled) return true;

                        //try to remove folder readonly attribute
                        try
                        {
                            var di = new System.IO.DirectoryInfo(System.IO.Path.GetDirectoryName(eadProject.FullName));
                            di.Attributes &= ~FileAttributes.ReadOnly;
                        }
                        catch { }

                        if (!SaveDesigners(eadProject, stopDesigner))
                            return false;
                    }
                }
            }

            return true;
        }

        private bool SaveDesigners(Project eadProject, string stopDesigner)
        {
            if (!projectName.IsNullOrEmpty() && projectName != eadProject.Name)
                return true;

            int docCnt = 0;
            List<ProjectItem> items = new List<ProjectItem>();
            foreach (ProjectItem item in eadProject.ProjectItems)
            {
                string extension = System.IO.Path.GetExtension(item.Name).ToLower();
                if ((eadProject.Name.Right(2) != "BL" && !eadProject.Name.Contains(".BL.")) && (stopDesigner.IsNullOrEmpty() && extension.InList(".ead", ".bmd")) || (!stopDesigner.IsNullOrEmpty() && extension == stopDesigner))
                {
                    docCnt++;
                    items.Add(item);
                    //if ((!stopDesigner.IsNullOrEmpty() && extension == stopDesigner))
                    if ((!stopDesigner.IsNullOrEmpty() && extension == stopDesigner) || (extension == ".bmd" && docCnt > 1))
                        break;
                }
            }

            Utils.UpgradeVersion(eadProject);
            Utils.AdjustMissingReferences(eadProject);


            CtrlProgressDesigners.Minimum = 0;
            CtrlProgressDesigners.Maximum = items.Count;
            CtrlProgressDesigners.Value = lastDesignerIndex;
            this.StatusDesigners.Text = "";
            System.Windows.Forms.Application.DoEvents();
            Mouse.SetCursor(System.Windows.Input.Cursors.Wait);
            System.Threading.Thread.Sleep(500);

            for (int idx = lastDesignerIndex; idx < items.Count; idx++)
            {
                if (idx > lastDesignerIndex && (idx % _MAX_DESIGNER_INDEX) == 0)
                {
                    lastDesignerIndex = idx;
                    projectName = eadProject.Name;
                    return false;
                }

                ProjectItem item = items[idx];
                if (isCancelled) return true;
                string extension = System.IO.Path.GetExtension(item.Name).ToLower();

                this.StatusDesigners.Text = "Processing (" + (idx + 1) + "/" + items.Count + "): " + eadProject.Name + " (" + item.Name + ")";
                CtrlProgressDesigners.Value = idx + 1;
                System.Windows.Forms.Application.DoEvents();
                Mouse.SetCursor(System.Windows.Input.Cursors.Wait);
                System.Threading.Thread.Sleep(500);
                EnvDTE.Window window = item.Open("{7651A702-06E5-11D1-8EBD-00A0C90F26EA}");
                window.SetFocus();
                window.Document.Save();
                window.Close();
                Collect(eadProject.DTE);
            }

            //Reset processing
            projectName = "";
            lastDesignerIndex = 0;

            ////

            //if (eadProject.Name.Right(2) == "BL" || (items.Count == 0 && (eadProject.Name.Contains(".BM.") || eadProject.Name.Contains(".BL."))))
            //{

            //}
            ////

            return true;
        }

        private void Collect(DTE dte)
        {
            Marshal.ReleaseComObject(dte);
            GC.Collect();
            GC.WaitForFullGCComplete();
        }

        private List<Project> GetEadProjects(Project folder)
        {
            List<Project> result = new List<Project>();

            if (folder.ProjectItems != null && folder.ProjectItems.Count > 0)
            {
                if (!folder.ProjectItems.IsNullOrEmpty())
                {
                    foreach (ProjectItem projItem in folder.ProjectItems)
                    {
                        if (projItem.SubProject != null)
                        {
                            result.Add(projItem.SubProject);
                        }
                    }
                }
            }

            if (result.Count == 0)
                result.Add(folder);

            return result;
        }

        private void BtClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void ClearProcess()
        {
            this.SolutionInfoList.Clear();
            this.TxOutput.Text = "";
            this.CtrlProgress.Minimum = 0;
            this.CtrlProgress.Maximum = 1;
            this.CtrlProgress.Value = 0;
            this.StatusLabel.Text = "";
            this.CtrlProgressDesigners.Minimum = 0;
            this.CtrlProgressDesigners.Maximum = 1;
            this.CtrlProgressDesigners.Value = 0;
            this.StatusDesigners.Text = "";
            this.TxSolutionFolder.IsEnabled = true;
            this.BtProcess.IsEnabled = true;
            this.BtCancel.IsEnabled = false;
            this.BtSelectDirectory.IsEnabled = true;
            this.lbSolutionList.IsEnabled = true;
            this.lbSolutionList.ItemsSource = null;
            this.BtDeselectAll.IsEnabled = true;
            this.BtSelectAll.IsEnabled = true;
            this.CkSaveDesigners.IsEnabled = true;
            this.CkCompileJS.IsEnabled = true;
            this.CkCompileSolution.IsEnabled = true;
            this.cmbCompileConfig.IsEnabled = true;
            this.CkCustomClient.IsEnabled = true;
        }

        private void ConfigureStartProcess(int maxElements)
        {
            //Block elements edition
            this.TxOutput.Foreground = Brushes.Black;
            this.CtrlProgress.Minimum = 0;
            this.CtrlProgress.Maximum = maxElements;
            this.CtrlProgress.Value = 0;
            this.StatusLabel.Text = "";
            this.StatusDesigners.Text = "";
            this.TxSolutionFolder.IsEnabled = false;
            this.BtProcess.IsEnabled = false;
            this.BtCancel.IsEnabled = true;
            this.BtSelectDirectory.IsEnabled = false;
            this.lbSolutionList.IsEnabled = false;
            this.BtDeselectAll.IsEnabled = false;
            this.BtSelectAll.IsEnabled = false;
            this.CkSaveDesigners.IsEnabled = false;
            this.CkCompileJS.IsEnabled = false;
            this.CkCompileSolution.IsEnabled = false;
            this.cmbCompileConfig.IsEnabled = false;
            this.CkCustomClient.IsEnabled = false;
            System.Windows.Forms.Application.DoEvents();
            Mouse.SetCursor(System.Windows.Input.Cursors.Wait);
            System.Threading.Thread.Sleep(500);
        }

        private void ConfigureEndProcess()
        {
            //Restore elements edition            
            this.TxSolutionFolder.IsEnabled = true;
            this.BtProcess.IsEnabled = true;
            this.BtCancel.IsEnabled = false;
            this.BtSelectDirectory.IsEnabled = true;
            this.lbSolutionList.IsEnabled = true;
            this.BtDeselectAll.IsEnabled = true;
            this.BtSelectAll.IsEnabled = true;
            this.CkSaveDesigners.IsEnabled = true;
            this.CkCompileJS.IsEnabled = true;
            this.CkCompileSolution.IsEnabled = true;
            this.cmbCompileConfig.IsEnabled = true;
            if (isCancelled)
            {
                this.TxOutput.Foreground = Brushes.Black;
                this.TxOutput.Text = "Cancelled!\r\n" + this.TxOutput.Text;
            }
            else
            {
                if (this.TxOutput.Text.IsNullOrEmpty())
                {
                    if (!businessAlerts.IsNullOrEmpty())
                    {
                        this.TxOutput.Foreground = Brushes.Blue;
                        this.TxOutput.Text = businessAlerts;
                    }
                    else
                    {
                        this.TxOutput.Foreground = Brushes.Green;
                        this.TxOutput.Text = "Successful!";
                    }
                }
                else
                {
                    this.TxOutput.Foreground = Brushes.Red;
                    this.TxOutput.Text = "Finished With Alerts!\r\n\r\n" + this.TxOutput.Text;
                }
            }
            this.StatusDesigners.Text = "";
            System.Windows.Forms.Application.DoEvents();
            Mouse.SetCursor(System.Windows.Input.Cursors.Arrow);
            System.Threading.Thread.Sleep(500);
            isCancelled = false;
        }


        private string GetOutput(DTE dte)
        {
            EnvDTE.Window win;
            OutputWindow w;
            OutputWindowPane wp;
            TextDocument td;
            win = dte.Windows.Item("{34E76E81-EE4A-11D0-AE2E-00A0C90FFFC3}");
            w = win.Object;
            for (int i = 1; i < w.OutputWindowPanes.Count; i++)
            {
                wp = w.OutputWindowPanes.Item(i);
                if (wp.Name == "Build")
                {
                    td = wp.TextDocument;
                    td.Selection.SelectAll();
                    var ts = td.Selection;
                    return ts.Text;
                }
            }

            return String.Empty;
        }

        private string GeteJavascriptErrors(string directory)
        {
            string result = String.Empty;
            string[] javascriptFiles = System.IO.Directory.GetFiles(directory, "*.js",
              System.IO.SearchOption.AllDirectories);

            using (JavaScriptCompiler compiler = new JavaScriptCompiler())
            {
                var compilerResult = compiler.Compile(javascriptFiles);
                foreach (string file in javascriptFiles)
                {
                    if (compilerResult.ContainsKey(file) && compilerResult[file].Errors.Count > 0)
                    {
                        result = "\r\n\r\nFail Compiling " + file + ":";
                        foreach (var erro in compilerResult[file].Errors)
                        {
                            result += "\r\n" + erro.ErrorText;
                        }
                    }
                }
            }

            return result;
        }

        private void BtProcess_Click(object sender, RoutedEventArgs e)
        {
            if (this.TxSolutionFolder.Text.IsNullOrEmpty() || !System.IO.Directory.Exists(this.TxSolutionFolder.Text))
            {
                System.Windows.MessageBox.Show("The [Business Solutions Directory] is not valid!", "Alert", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning, System.Windows.MessageBoxResult.OK);
                return;
            }

            businessAlerts = "";
            this.TxOutput.Text = "";
            var dsolFiles = SolutionInfoList.Where(s => s.IsSelected).Select(s => s.Path).ToArray();

            if (dsolFiles.Length > 0)
            {
                ConfigureStartProcess(dsolFiles.Length * 2);

                //Get VS11
                Type visualStudioType = Type.GetTypeFromProgID("VisualStudio.DTE.15.0");

                for (int idx = 0; idx < dsolFiles.Length; idx++)
                {
                    if (isCancelled)
                    {
                        ConfigureEndProcess();
                        return;
                    }

                    //Notify progress
                    this.StatusLabel.Text = "Processing Solution (" + (idx + 1) + "/" + dsolFiles.Length + "): " + System.IO.Path.GetFileName(dsolFiles[idx]);
                    this.CtrlProgress.Value = 2 * idx + 1;
                    CtrlProgressDesigners.Minimum = 0;
                    CtrlProgressDesigners.Maximum = 1;
                    CtrlProgressDesigners.Value = 0;

                    // Register the IOleMessageFilter to handle any threading 
                    // errors.
                    MessageFilter.Register();
                    DTE dte = null;
                    string solutionDir = System.IO.Path.GetDirectoryName(dsolFiles[idx]);
                    string solutionAlerts = System.IO.Path.Combine(solutionDir, "Alerts.info");

                    try
                    {
                        if (this.CkCustomClient.IsChecked.Value)
                            System.IO.File.WriteAllText(solutionAlerts, "");

                        dte = OpenSolution(visualStudioType, dsolFiles[idx]);

                        //if (this.CkSaveDesigners.IsChecked.Value)
                        //{
                        this.StatusDesigners.Text = "Executing Checkout...";
                        System.Windows.Forms.Application.DoEvents();
                        Mouse.SetCursor(System.Windows.Input.Cursors.Wait);
                        System.Threading.Thread.Sleep(500);
                        //Checkout element
                        Checkout(solutionDir);

                        Utils.DTEReference = dte;

                        ////Save all documents
                        while (!SaveAllDocuments(dte, (this.CkSaveDesigners.IsChecked.Value ? "" : ".ead")))
                        {
                            //Quit from IDE
                            ReleaseDTE(dte);

                            System.Threading.Thread.Sleep(500);

                            //Reopen solution
                            if (!isCancelled)
                                dte = OpenSolution(visualStudioType, dsolFiles[idx]);

                            System.Threading.Thread.Sleep(500);
                        }
                        //}

                        //Building process
                        if (!isCancelled)
                        {
                            if (this.CkCompileSolution.IsChecked.Value)
                            {
                                this.StatusDesigners.Text = "Compiling solution...";
                                System.Windows.Forms.Application.DoEvents();
                                Mouse.SetCursor(System.Windows.Input.Cursors.Wait);
                                System.Threading.Thread.Sleep(500);
                                var selectedConfiguration = ((ComboBoxItem)cmbCompileConfig.SelectedItem).Content.ToString();
                                var configName = dte.Solution.SolutionBuild.ActiveConfiguration.Name;
                                if (selectedConfiguration != configName)
                                {
                                    var solnCfg = dte.Solution.SolutionBuild.SolutionConfigurations.Item(selectedConfiguration);
                                    solnCfg.Activate();
                                }
                                dte.Solution.SolutionBuild.Build(true);
                                if (selectedConfiguration != configName)
                                {
                                    var solnCfg = dte.Solution.SolutionBuild.SolutionConfigurations.Item(configName);
                                    solnCfg.Activate();
                                }

                                var compilingResult = GetOutput(dte);
                                if (compilingResult.ToLower().Contains(": error "))
                                    this.TxOutput.Text = "C# Error Compiling " + dsolFiles[idx] + ":\r\n" + compilingResult + this.TxOutput.Text + "\r\n\r\n";
                            }

                            if (this.CkCompileJS.IsChecked.Value)
                            {
                                this.StatusDesigners.Text = "Compiling JavaScripts...";
                                System.Windows.Forms.Application.DoEvents();
                                Mouse.SetCursor(System.Windows.Input.Cursors.Wait);
                                System.Threading.Thread.Sleep(500);
                                var compilingResult = GeteJavascriptErrors(solutionDir);
                                if (!compilingResult.IsNullOrEmpty())
                                    this.TxOutput.Text = "JavaScript Error Compiling " + dsolFiles[idx] + ":\r\n" + compilingResult + this.TxOutput.Text + "\r\n\r\n";
                            }

                            if (this.CkCustomClient.IsChecked.Value)
                            {
                                //Check alerts
                                if (System.IO.File.Exists(solutionAlerts))
                                {
                                    string alerts = System.IO.File.ReadAllText(solutionAlerts);
                                    if (!alerts.IsNullOrEmpty())
                                    {
                                        businessAlerts += "Business alerts from solution " + dsolFiles[idx] + ":\r\n" + alerts + "\r\n\r\n";
                                    }
                                    System.IO.File.Delete(solutionAlerts);
                                }
                            }
                        }

                        //Checkin(System.IO.Path.GetDirectoryName(dsolFiles[idx]));

                    }
                    catch (Exception excp)
                    {
                        this.TxOutput.Text = "Fail in " + dsolFiles[idx] + ":\r\n" + excp.GetCompleteMessage() + this.TxOutput.Text + "\r\n\r\n";
                        System.Windows.Forms.Application.DoEvents();
                        System.Threading.Thread.Sleep(500);
                    }

                    //Quit from IDE
                    ReleaseDTE(dte);

                    // and turn off the IOleMessageFilter.
                    MessageFilter.Revoke();

                    //Notify progress
                    this.CtrlProgress.Value = 2 * idx + 2;
                    this.CtrlProgress.InvalidateVisual();
                    System.Windows.Forms.Application.DoEvents();
                    Mouse.SetCursor(System.Windows.Input.Cursors.Wait);
                    System.Threading.Thread.Sleep(500);
                }

                ConfigureEndProcess();

            }
            else
                System.Windows.MessageBox.Show("No Business Solutions Found!", "Information", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information, System.Windows.MessageBoxResult.OK);


        }

        private void ReleaseDTE(DTE dte)
        {
            //Quit from IDE
            if (dte != null)
            {
                //Close solution
                dte.Solution.Close();
                dte.Quit();
                Collect(dte);
            }
        }

        private DTE OpenSolution(Type visualStudioType, string solutionName)
        {
            var dte = Activator.CreateInstance(visualStudioType) as DTE;
            dte.MainWindow.Visible = false;

            //Open solution                        
            dte.Solution.Open(solutionName);
            dte.Documents.CloseAll();

            return dte;
        }

        private void Checkout(string path)
        {
            var workspaceInfo = Workstation.Current.GetLocalWorkspaceInfo(path);
            if (workspaceInfo != null)
            {
                var server = new TfsTeamProjectCollection(workspaceInfo.ServerUri);
                var workspace = workspaceInfo.GetWorkspace(server);

                var files = System.IO.Directory.GetFiles(path, "*.*", System.IO.SearchOption.AllDirectories);
                foreach (string file in files)
                {
                    workspace.PendEdit(file);
                }
            }
        }

        private void Checkin(string path)
        {
            var workspaceInfo = Workstation.Current.GetLocalWorkspaceInfo(path);
            if (workspaceInfo != null)
            {
                var server = new TfsTeamProjectCollection(workspaceInfo.ServerUri);
                var workspace = workspaceInfo.GetWorkspace(server);

                var files = System.IO.Directory.GetFiles(path, "*.*", System.IO.SearchOption.AllDirectories);
                foreach (string file in files)
                {
                    PendingChange[] pendingChange = workspace.GetPendingChanges(file);
                    if (pendingChange.Length > 0)
                        workspace.CheckIn(pendingChange, "Automatic Process");
                }
            }
        }

        private void BtCancel_Click(object sender, RoutedEventArgs e)
        {
            isCancelled = true;
        }

        private void TxSolutionFolder_TextChanged(object sender, TextChangedEventArgs e)
        {
            PopulateSolutios();
        }

        private void lbSolutionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AdjustSelectionInfo();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (!this.BtProcess.IsEnabled)
                e.Cancel = true;
        }

        private void BtSelectAll_Click(object sender, RoutedEventArgs e)
        {
            Mouse.SetCursor(System.Windows.Input.Cursors.Wait);
            this.BtDeselectAll.IsEnabled = false;
            this.BtSelectAll.IsEnabled = false;
            foreach (var item in SolutionInfoList.ToList())
            {
                item.IsSelected = true;
            }
            lbSolutionList.ItemsSource = null;
            lbSolutionList.ItemsSource = SolutionInfoList;
            this.BtDeselectAll.IsEnabled = true;
            this.BtSelectAll.IsEnabled = true;
            Mouse.SetCursor(System.Windows.Input.Cursors.Arrow);
        }

        private void BtDeselectAll_Click(object sender, RoutedEventArgs e)
        {
            Mouse.SetCursor(System.Windows.Input.Cursors.Wait);
            this.BtDeselectAll.IsEnabled = false;
            this.BtSelectAll.IsEnabled = false;
            foreach (var item in SolutionInfoList.ToList())
            {
                item.IsSelected = false;
            }
            lbSolutionList.ItemsSource = null;
            lbSolutionList.ItemsSource = SolutionInfoList;
            this.BtDeselectAll.IsEnabled = true;
            this.BtSelectAll.IsEnabled = true;
            Mouse.SetCursor(System.Windows.Input.Cursors.Arrow);
        }

        private void CkSaveDesigners_Checked(object sender, RoutedEventArgs e)
        {
            if (!this.CkSaveDesigners.IsChecked.Value && this.CkCustomClient.IsChecked.Value)
                this.CkCustomClient.IsChecked = false;
        }

        private void CkCustomClient_Checked(object sender, RoutedEventArgs e)
        {
            if (!this.CkSaveDesigners.IsChecked.Value && this.CkCustomClient.IsChecked.Value)
            {
                this.CkSaveDesigners.IsChecked = true;
            }
        }


    }

    public class MessageFilter : IOleMessageFilter
    {
        //
        // Class containing the IOleMessageFilter
        // thread error-handling functions.

        // Start the filter.
        public static void Register()
        {
            IOleMessageFilter newFilter = new MessageFilter();
            IOleMessageFilter oldFilter = null;
            CoRegisterMessageFilter(newFilter, out oldFilter);
        }

        // Done with the filter, close it.
        public static void Revoke()
        {
            IOleMessageFilter oldFilter = null;
            CoRegisterMessageFilter(null, out oldFilter);
        }

        //
        // IOleMessageFilter functions.
        // Handle incoming thread requests.
        int IOleMessageFilter.HandleInComingCall(int dwCallType,
          System.IntPtr hTaskCaller, int dwTickCount, System.IntPtr
          lpInterfaceInfo)
        {
            //Return the flag SERVERCALL_ISHANDLED.
            return 0;
        }

        // Thread call was rejected, so try again.
        int IOleMessageFilter.RetryRejectedCall(System.IntPtr
          hTaskCallee, int dwTickCount, int dwRejectType)
        {
            if (dwRejectType == 2)
            // flag = SERVERCALL_RETRYLATER.
            {
                // Retry the thread call immediately if return >=0 & 
                // <100.
                return 99;
            }
            // Too busy; cancel call.
            return -1;
        }

        int IOleMessageFilter.MessagePending(System.IntPtr hTaskCallee,
          int dwTickCount, int dwPendingType)
        {
            //Return the flag PENDINGMSG_WAITDEFPROCESS.
            return 2;
        }

        // Implement the IOleMessageFilter interface.
        [DllImport("Ole32.dll")]
        private static extern int
          CoRegisterMessageFilter(IOleMessageFilter newFilter, out
          IOleMessageFilter oldFilter);
    }

    [ComImport(), Guid("00000016-0000-0000-C000-000000000046"),
    InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    interface IOleMessageFilter
    {
        [PreserveSig]
        int HandleInComingCall(
            int dwCallType,
            IntPtr hTaskCaller,
            int dwTickCount,
            IntPtr lpInterfaceInfo);

        [PreserveSig]
        int RetryRejectedCall(
            IntPtr hTaskCallee,
            int dwTickCount,
            int dwRejectType);

        [PreserveSig]
        int MessagePending(
            IntPtr hTaskCallee,
            int dwTickCount,
            int dwPendingType);
    }

    public class SolutionInfo
    {
        public bool IsSelected { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }

}
