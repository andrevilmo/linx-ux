using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Linx.TelerikReportDesigner.Setup
{
    public class Installer
    {
        public void InstallTelerikReport()
        {
            using (var proc = Process.Start(Environment.CurrentDirectory + "/Telerik_Reporting_Q1_2015_9.0.15.422_DEV.msi"))
                proc.WaitForExit();
        }

        public void ConfigTelerikReporting(string directoryTelerikReportDesigner, string directoryTelerikReportTemplates)
        {
            var utils = new Library.Utils();
            var reportController = new Library.FileReportController();

            if (!Directory.Exists(directoryTelerikReportTemplates))
                Directory.CreateDirectory(directoryTelerikReportTemplates);

            reportController.KillReportDesignProcess();

            utils.SetNotReadOnlyFolder(directoryTelerikReportDesigner);
            utils.SetNotReadOnlyFolder(directoryTelerikReportTemplates);
            utils.BaseTelerikConfig(directoryTelerikReportDesigner);
            utils.AddDefaultDirectoryTelerikReporting(directoryTelerikReportDesigner);

            utils.CopyToTelerikPath(directoryTelerikReportDesigner, Environment.CurrentDirectory + "\\Linx.ico");
            utils.CopyToTelerikPath(directoryTelerikReportDesigner, Environment.CurrentDirectory + "\\Linx.Data.dll");
            utils.CopyToTelerikPath(directoryTelerikReportDesigner, Environment.CurrentDirectory + "\\Newtonsoft.Json.dll");
            utils.CopyToTelerikPath(directoryTelerikReportDesigner, Environment.CurrentDirectory + "\\EntityFramework.dll");
            utils.CopyToTelerikPath(directoryTelerikReportTemplates, Environment.CurrentDirectory + "\\Linx Template.trtx");
            utils.CopyToTelerikPath(directoryTelerikReportDesigner, Environment.CurrentDirectory + "\\Linx.Business.Tools.dll");
            utils.CopyToTelerikPath(directoryTelerikReportDesigner, Environment.CurrentDirectory + "\\EntityFramework.SqlServer.dll");
            utils.CopyToTelerikPath(directoryTelerikReportDesigner, Environment.CurrentDirectory + "\\Linx.TelerikReportDesigner.Report.exe");
            utils.CopyToTelerikPath(directoryTelerikReportDesigner, Environment.CurrentDirectory + "\\Linx.TelerikReportDesigner.Library.dll");
            utils.CopyToTelerikPath(directoryTelerikReportDesigner, Environment.CurrentDirectory + "\\System.ServiceModel.DomainServices.Server.dll");
            utils.CopyToTelerikPath(directoryTelerikReportDesigner, Environment.CurrentDirectory + "\\Linx.Tools.dll");

            utils.CreateExtensionLdsx();
            utils.CreateExtensionLtrx();
        }

        public void InstallPublish(string directoryTelerikReportDesigner)
        {
            var utils = new Library.Utils();
            utils.CopyToTelerikPath(directoryTelerikReportDesigner, Environment.CurrentDirectory + "\\Linx.TelerikReportDesigner.Publisher.exe");
        }        
    }
}
