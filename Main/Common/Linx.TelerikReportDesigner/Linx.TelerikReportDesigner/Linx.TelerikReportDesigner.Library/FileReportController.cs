using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Linx.TelerikReportDesigner.Library
{
    public class FileReportController
    {
        public void ExportReportFiles(string zipPath)
        {
            var library = new Library.Utils();
            library.Unzip(zipPath);
        }

        public void OpenReport(string file)
        {
            var library = new Library.Utils();
            var trdxFileName = library.GetTrdxFileName(file);

            if (string.IsNullOrEmpty(trdxFileName))
                Process.Start(library.GetReportDesignerFullPath());
            else
                Process.Start(library.GetReportDesignerFullPath(), "\"" + trdxFileName + "\"");
        }

        public void KillReportDesignProcess()
        {
            var currentProcess = this.GetReportDesignProcess();

            if (currentProcess != null)
            {
                MessageBox.Show(string.Format( " O Telerik Report Designer será reiniciado. {0} Por favor certifique-se que todos os seus relatórios estejam salvos.",
                    Environment.NewLine));

                currentProcess.Kill();
                currentProcess.WaitForExit();
            }
        }

        private Process GetReportDesignProcess()
        {
            var process = default(Process);
            var processes = Process.GetProcessesByName("Telerik.ReportDesigner");

            if (processes != null && processes.Any())
                process = processes.First();

            return process;
        }
    }
}
