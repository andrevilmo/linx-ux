using BrendanGrant.Helpers.FileAssociation;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Linx.TelerikReportDesigner.Library
{
    public class Utils
    {
        #region Insert and Configure Data Source in Telerik

        public void UpdateTelerikConfig(string safeFileName, string directoryTelerikReport)
        {
            var doc = XDocument.Load(directoryTelerikReport + "Telerik.ReportDesigner.exe.config");

            if (doc.Root.Element("Telerik.Reporting") == null)
            {
                AddAssemblyElement(safeFileName, doc);
                AddBaseAssembliesElements(doc);
            }
            else
            {
                RemoveAssemblyElement(doc, safeFileName.Replace(".dll", ""));
                RemoveAssemblyElement(doc, safeFileName.Replace(".Reports.dll", ""));
                AddAssemblyElement(safeFileName, doc);
            }

            doc.Save(directoryTelerikReport + "Telerik.ReportDesigner.exe.config");
        }

        public void BaseTelerikConfig(string directoryTelerikReport)
        {
            XDocument doc = XDocument.Load(directoryTelerikReport + "Telerik.ReportDesigner.exe.config");

            AddBaseAssembliesElements(doc);

            doc.Save(directoryTelerikReport + "Telerik.ReportDesigner.exe.config");
        }

        public void RemoveAssemblyElement(XDocument doc, string value)
        {
            var element = doc.Root.Element("Telerik.Reporting")
                .Element("AssemblyReferences")
                .Elements()
                .FirstOrDefault(s => s.Attribute("name").Value == value);

            if (element != null)
                element.Remove();

            //Exclude element AssemblyReferences if him don't have sub elements
            if (doc.Root.Element("Telerik.Reporting").Element("AssemblyReferences").IsEmpty)
                doc.Root.Elements("Telerik.Reporting").Remove();
        }

        public void AddAssemblyElement(string fileName, XDocument doc)
        {
            if (doc.Root.Element("Telerik.Reporting") == null)
            {
                doc.Root.Add(
                new XElement("Telerik.Reporting",
                    new XElement("AssemblyReferences",
                        new XElement("add", new XAttribute("name", fileName.Replace(".dll", "")),
                            new XAttribute("version", "1.0.0.0")),
                        new XElement("add", new XAttribute("name", fileName.Replace(".Reports.dll", "")),
                            new XAttribute("version", "1.0.0.0"))
                        )
                    )
                );
            }
            else
            {
                doc.Root.Element("Telerik.Reporting").Element("AssemblyReferences").Add(
                        new XElement("add", new XAttribute("name", fileName.Replace(".dll", "")),
                            new XAttribute("version", "1.0.0.0")),
                        new XElement("add", new XAttribute("name", fileName.Replace(".Reports.dll", "")),
                            new XAttribute("version", "1.0.0.0"))
                        );
            }
        }

        public void AddBaseAssembliesElements(XDocument doc)
        {
            if (doc.Root.Element("Telerik.Reporting") == null)
            {
                doc.Root.Add(
                new XElement("Telerik.Reporting",
                    new XElement("AssemblyReferences",
                        new XElement("add", new XAttribute("name", "Linx.Data"), new XAttribute("version", "1.0.0.0")),
                        new XElement("add", new XAttribute("name", "Newtonsoft.Json"), new XAttribute("version", "6.0.0.0")),
                        new XElement("add", new XAttribute("name", "EntityFramework"), new XAttribute("version", "6.0.0.0")),
                        new XElement("add", new XAttribute("name", "EntityFramework.SqlServer"), new XAttribute("version", "6.0.0.0")),
                        new XElement("add", new XAttribute("name", "Linx.Business.Tools"), new XAttribute("version", "1.0.0.0"))
                        )
                    )
                );
            }
            else
            {
                var baseAssembliesReferences =
                doc.Root.Element("Telerik.Reporting").Element("AssemblyReferences").Elements("add");
                
                baseAssembliesReferences.Where(x => x.Attribute("name").Value == "Linx.Data").Remove();
                baseAssembliesReferences.Where(x => x.Attribute("name").Value == "Newtonsoft.Json").Remove();
                baseAssembliesReferences.Where(x => x.Attribute("name").Value == "EntityFramework").Remove();
                baseAssembliesReferences.Where(x => x.Attribute("name").Value == "EntityFramework.SqlServer").Remove();
                baseAssembliesReferences.Where(x => x.Attribute("name").Value == "Linx.Business.Tools").Remove();

                doc.Root.Element("Telerik.Reporting").Element("AssemblyReferences").Add(
                        new XElement("add", new XAttribute("name", "Linx.Data"), new XAttribute("version", "1.0.0.0")),
                        new XElement("add", new XAttribute("name", "Newtonsoft.Json"), new XAttribute("version", "6.0.0.0")),
                        new XElement("add", new XAttribute("name", "EntityFramework"), new XAttribute("version", "6.0.0.0")),
                        new XElement("add", new XAttribute("name", "EntityFramework.SqlServer"), new XAttribute("version", "6.0.0.0")),
                        new XElement("add", new XAttribute("name", "Linx.Business.Tools"), new XAttribute("version", "1.0.0.0"))
                        );
            }
        }

        public void AddDefaultDirectoryTelerikReporting(string directoryTelerikReport)
        {
            XDocument doc = XDocument.Load(directoryTelerikReport + "Telerik.ReportDesigner.exe.config");

            doc.Root.Element("Telerik.ReportDesigner").Remove();
            doc.Root.Add(
                new XElement("Telerik.ReportDesigner", new XAttribute("DefaultWorkingDir", "Linx Reports"))
            );

            doc.Save(directoryTelerikReport + "Telerik.ReportDesigner.exe.config");
        }


        public void InsertDataSourceToTelerikPath(string directoryBase, string directoryTelerikReport)
        {
            //Copy Reports.dll Data Source to Telerik Path
            File.Copy(directoryBase, Path.Combine(directoryTelerikReport, Path.GetFileName(directoryBase)), true);

            //Copy BV.dll Data Source to Telerik Path
            var bv = directoryBase.Replace(".Reports.dll", ".dll");
            File.Copy(bv, Path.Combine(directoryTelerikReport, Path.GetFileName(bv)), true);
        }

        public void InsertTrdxToTelerikPath(string directoryBase, string directoryTelerikReport)
        {
            //Copy trdx file to Telerik Path
            File.Copy(directoryBase, Path.Combine(directoryTelerikReport, Path.GetFileName(directoryBase)), true);
        }

        public void InsertBaseDllsToTelerikPath(string directoryTelerikReport, string basePath)
        {
            //Copy Linx.Data.dll Data Source to Telerik Path
            var linxData = basePath + "Linx.Data.dll";
            File.Copy(linxData, Path.Combine(directoryTelerikReport, Path.GetFileName(linxData)), true);


            //Copy EntityFramework.dll  to Telerik Path
            var entityFramework = basePath + "EntityFramework.dll";
            File.Copy(entityFramework, Path.Combine(directoryTelerikReport, Path.GetFileName(entityFramework)), true);

            //Copy Link.Business.Tools.dll  to Telerik Path
            var linxBusinessTools = basePath + "Linx.Business.Tools.dll";
            File.Copy(linxBusinessTools, Path.Combine(directoryTelerikReport, Path.GetFileName(linxBusinessTools)), true);

            //Copy EntityFramework.SqlServer.dll  to Telerik Path
            var entityFrameworkSqlServer = basePath + "EntityFramework.SqlServer.dll";
            File.Copy(entityFrameworkSqlServer, Path.Combine(directoryTelerikReport, Path.GetFileName(entityFrameworkSqlServer)), true);
        }

        public void SetNotReadOnlyFolder(string directoryTelerikReport)
        {
            var directoryInfo = new DirectoryInfo(directoryTelerikReport);
            directoryInfo.Attributes &= ~FileAttributes.ReadOnly;

            foreach (string file in Directory.GetFiles(directoryTelerikReport))
            {
                var fileInfo = new FileInfo(file);
                fileInfo.Attributes &= ~FileAttributes.ReadOnly;
            }
        }

        public string GetReportDesignerFullPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + @"\Telerik\Reporting Q1 2015\Report Designer\Telerik.ReportDesigner.exe";
        }

        public string GetTrdxFileName(string zipName)
        {
            var trdxPath = string.Empty;

            using (var zip = ZipFile.OpenRead(zipName))
            {
                var directoryTelerikReportDesigner = string.Format("{0}\\Telerik\\Reporting Q1 2015\\Report Designer\\",
                    Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86));

                if (zip.Entries != null && zip.Entries.Any(x => x.Name.Contains(".trdx")))
                {
                    var trdxFile = zip.Entries.FirstOrDefault(x => x.Name.Contains(".trdx"));
                    trdxPath = Path.Combine(directoryTelerikReportDesigner + "Linx Reports\\", trdxFile.FullName);
                }
            }

            return trdxPath;
        }

        #endregion

        #region Incomplete -Import os templates

        public OpenFileDialog OpenTelerikFilesToTrtx()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Path.GetPathRoot(Environment.SystemDirectory);
            openFileDialog.Filter = "trtx files (*.trtx)|*.trtx";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            return openFileDialog;
        }

        public void InsertTemplateToTelerikPath(string directoryBase, string file)
        {
            if (!Directory.Exists(directoryBase))
                Directory.CreateDirectory(directoryBase);

            //Copy template(.trtx) to Telerik Path
            File.Copy(file, Path.Combine(directoryBase, Path.GetFileName(file)), true);
        }

        #endregion

        #region Zips Methods

        public string Unzip(string zipName)
        {
            string relativePathReport = string.Empty;
            //This stores the path where the file should be unzipped to,
            //including any subfolders that the file was originally in.
            string fileUnzipFullPath = string.Empty;

            //This is the full name of the destination file including
            //the path
            string fileUnzipFullName = string.Empty;

            //Opens the zip file up to be read
            using (ZipArchive archive = ZipFile.OpenRead(zipName))
            {
                var directoryTelerikReportDesigner = string.Format("{0}\\Telerik\\Reporting Q1 2015\\Report Designer\\",
                    Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86));

                SetNotReadOnlyFolder(directoryTelerikReportDesigner);

                //Loops through each file in the zip file
                foreach (ZipArchiveEntry file in archive.Entries)
                {
                    //Identifies the destination file name and path
                    if (file.Name.Contains(".trdx"))
                    {
                        if (!Directory.Exists(directoryTelerikReportDesigner + "Linx Reports/"))
                            Directory.CreateDirectory(directoryTelerikReportDesigner + "Linx Reports/");

                        fileUnzipFullName = Path.Combine(directoryTelerikReportDesigner + "Linx Reports/", file.FullName);
                        relativePathReport = Path.Combine("Linx Reports\\", file.FullName);
                    }
                    else
                        fileUnzipFullName = Path.Combine(directoryTelerikReportDesigner, file.FullName).Replace('\\', '/');

                    //Calculates what the new full path for the unzipped file should be
                    fileUnzipFullPath = Path.GetDirectoryName(fileUnzipFullName);

                    //Creates the directory (if it doesn't exist) for the new path
                    Directory.CreateDirectory(fileUnzipFullPath);

                    //Verify if the variable file is a path or archive
                    if (file.FullName.ToCharArray().Last() != '/')
                        //Extracts the file to (potentially new) path
                        file.ExtractToFile(fileUnzipFullName, true);
                }

                if (!File.Exists(directoryTelerikReportDesigner + "/Telerik.ReportDesigner.exe.config"))
                    throw new Exception("O Telerik Report Designer não está instalado. Favor realize a instalação através do link: ");

                //Loop used for configuration of Telerik
                foreach (ZipArchiveEntry file in archive.Entries)
                    if (file.Name.Contains("Reports.dll"))
                        UpdateTelerikConfig(file.Name, directoryTelerikReportDesigner);
            }

            return relativePathReport;
        }

        public void ZipPublishedFiles(string[] files)
        {
            string programFilesDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            var directoryTelerikReportDesigner = programFilesDirectory.Replace('\\', '/') + "/Telerik/Reporting Q1 2015/Report Designer/";

            if (!Directory.Exists(directoryTelerikReportDesigner + "Publisher"))
                Directory.CreateDirectory(directoryTelerikReportDesigner + "Publisher");

            var zip = ZipFile.Open(String.Format(directoryTelerikReportDesigner + "Publisher/Publisher {0}_{1}.zip",
                    DateTime.Now.ToShortDateString().Replace("/", "-"),
                    DateTime.Now.ToLongTimeString().Replace(":", "-")),
                ZipArchiveMode.Create);

            foreach (string file in files)
            {
                zip.CreateEntryFromFile(file, Path.GetFileName(file), CompressionLevel.Optimal);
            }

            zip.Dispose();
        }

        #endregion

        #region File Association Utils

        private string GetFileCommand()
        {
            return string.Format("\"{0}{1}\"  \"%1\"", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), @"\Telerik\Reporting Q1 2015\Report Designer\Linx.TelerikReportDesigner.Report.exe");
        }
        private string GetFileIcon()
        {
            return "\"" + Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + @"\Telerik\Reporting Q1 2015\Report Designer\Linx.ico" + "\"";
        }
        //A extensão Linx Telerik Report extension(.ltrx) é usada para inserção de Data Source junto com o template customizado no momento do download.
        public void CreateExtensionLtrx()
        {
            var fai = new FileAssociationInfo(".lrtx");

            if (fai.Exists) fai.Delete();

            fai.Create("LinxReportTemplate");
            fai.ContentType = "application/linxreporttemplate";

            var pai = new ProgramAssociationInfo(fai.ProgID);

            if (pai.Exists)
            {
                pai.RemoveVerb(new ProgramVerb("Open", GetFileCommand()));
                pai.Delete();
            }

            pai.Create("Linx Report Template", new ProgramVerb("Open", GetFileCommand()));
            pai.DefaultIcon = new ProgramIcon(GetFileIcon());
        }

        //A extensão Linx Data Source extension(.ldsx) é usada para inserção de Data Source.
        public void CreateExtensionLdsx()
        {
            var fai = new FileAssociationInfo(".ldsx");

            if (fai.Exists) fai.Delete();

            fai.Create("LinxDataSource");
            fai.ContentType = "application/linxdatasource";

            var pai = new ProgramAssociationInfo(fai.ProgID);

            if (pai.Exists)
            {
                pai.RemoveVerb(new ProgramVerb("Open", GetFileCommand()));
                pai.Delete();
            }

            pai.Create("Linx Data Source", new ProgramVerb("Open", GetFileCommand()));
            pai.DefaultIcon = new ProgramIcon(GetFileIcon());
        }

        public void CopyToTelerikPath(string directoryTelerikReport, string file)
        {
            if (!Directory.Exists(directoryTelerikReport))
                Directory.CreateDirectory(directoryTelerikReport);

            File.Copy(file, Path.Combine(directoryTelerikReport, Path.GetFileName(file)), true);
        }

        #endregion

        public static XmlDocument GetReport(string path)
        {
            var document = new XmlDocument();

            document.Load(path);
            Utils.RemoveParameters(document);

            return document;
        }

        private static void RemoveParameters(XmlDocument report)
        {
            var toRemove = new List<XmlNode>();
            var parameters = report.GetElementsByTagName("ReportParameter");

            foreach (XmlNode item in parameters)
            {
                var attributeName = item.Attributes["Name"].InnerText.ToLower();

                if (attributeName == "username" || attributeName == "password")
                    toRemove.Add(item);
            }

            foreach (var item in toRemove)
                report["Report"]["ReportParameters"].RemoveChild(item);
        }
    }
}
