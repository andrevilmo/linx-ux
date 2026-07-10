using Linx.TelerikReportDesigner.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.TelerikReportDesigner.Report
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var reportController = new FileReportController();

                reportController.KillReportDesignProcess();
                reportController.ExportReportFiles(args[0]);

                reportController.OpenReport(args[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
