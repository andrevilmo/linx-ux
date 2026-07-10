using System;
using System.IO;
using System.Windows.Forms;

namespace Linx.EntityAdapterDesigner.CustomizedCode.Helpers
{
    public static class TreatException
    {
        static string fileName = "DslErrors.log";
        static string path = @"c:\temp\" + fileName;

        public static void LogError(Exception ex)
        {
            Log(ex, true);
        }
        public static void LogWarn(Exception ex)
        {
            Log(ex, false);
        }
        private static void Log(Exception ex, bool isError)
        {
            string formatted = string.Format("[{5}]{0:dd/MM/yyyy hh:mm:ss}: {1}{4}{2}{4}{3}{4}", DateTime.Now, ex.Message, ex.StackTrace, new string('-', 80), Environment.NewLine, (isError ? "Error" : "Warning"));
            try
            {
                if (Directory.Exists(Path.GetDirectoryName(path)))
                    File.AppendAllText(path, formatted);
                else
                    File.AppendAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName), formatted);
            }
            catch
            {
                if (isError)
                    MessageBox.Show(formatted, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
