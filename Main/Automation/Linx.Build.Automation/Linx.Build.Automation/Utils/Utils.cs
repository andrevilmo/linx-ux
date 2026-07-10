using EnvDTE;
using Linx.Tools;
using Microsoft.VisualStudio.ComponentModelHost;
using NuGet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VSLangProj80;

namespace Linx.Build.Automation
{
    public static class Utils
    {
        public static EnvDTE.DTE DTEReference { get; set; }

        private static string GetDistributorProductName()
        {
            return "Linx Framework 6.0.0";
        }

        private static string GetInstalledFrameworkPath(string innerPath)
        {
            var installPath = System.IO.Path.Combine(GetLinxProgramFiles(), GetDistributorProductName());

            if (Directory.Exists(installPath) && File.Exists(Path.Combine(installPath, "Information\\EntityAdapterDirectoryInfo.xml")))
            {
                if (!innerPath.IsNullOrEmpty())
                    installPath = Path.Combine(installPath, innerPath);
                return installPath;
            }
            else
                return "";
        }
        private static string GetLinxProgramFiles()
        {
            return "C:\\Linx Program Files";
        }

        private static string DocumentPath { get; set; }

        private static string GetLocalFrameworkPath()
        {
            if (!DocumentPath.IsNullOrEmpty())
            {
                string localMapPath = Path.Combine(Path.GetPathRoot(DocumentPath), "VSTS - GrupoLinx\\Framework");
                if (Directory.Exists(localMapPath))
                    return localMapPath;
            }

            return "";
        }

        private static string[] GetEnvironments()
        {
            string[] result = new string[] { };
            string worksapaceMapedpath = GetLocalFrameworkPath();

            if (worksapaceMapedpath.IsNullOrEmpty())
                worksapaceMapedpath = GetLocalFrameworkPath();

            if (!worksapaceMapedpath.IsNullOrEmpty())
            {
                string endInfoFile = Path.Combine(worksapaceMapedpath, "Linx Framework\\Environments.xml");
                if (File.Exists(endInfoFile))
                {
                    try
                    {
                        System.Xml.Linq.XElement xElementFound = System.Xml.Linq.XElement.Load(endInfoFile);
                        result = (xElementFound.IsNull() ? String.Empty : xElementFound.Value.Replace("\n", String.Empty).Replace("\t", String.Empty)).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(String.Format("Fail reading the file {0}.", endInfoFile) + exp.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }

            return result;
        }

        private static string GetDirectorySourcePart()
        {
            string dirPart = null;
            if (GetInstalledFrameworkPath("").IsNullOrEmpty())
            {
                if (DTEReference != null)
                {
                    var envs = GetEnvironments();
                    dirPart = envs.FirstOrDefault(e => DTEReference.Solution.FullName.ToLower().Contains("\\" + e.Trim().ToLower() + "\\"));
                }
            }
            return (dirPart.IsNullOrEmpty() ? "Main" : dirPart);
        }

        private static string GetDirectoryInfo(string directoryName)
        {
            string dirPart = String.Empty;
            string result = String.Empty;
            string tfsMiddleDir = String.Empty;
            string worksapaceMapedpath = GetInstalledFrameworkPath("");

            if (worksapaceMapedpath.IsNullOrEmpty())
            {
                dirPart = GetDirectorySourcePart();
                tfsMiddleDir = "Linx Framework\\" + dirPart + "\\Binary";
                worksapaceMapedpath = GetLocalFrameworkPath();
            }

            if (!worksapaceMapedpath.IsNullOrEmpty())
            {
                string dirInfoFile = (tfsMiddleDir.IsNullOrEmpty() ? Path.Combine(Path.Combine(worksapaceMapedpath, "information"), "EntityAdapterDirectoryInfo.xml") : Path.Combine(worksapaceMapedpath, tfsMiddleDir + "\\Library\\Common\\Linx\\Information\\EntityAdapterDirectoryInfo.xml"));
                if (File.Exists(dirInfoFile))
                {
                    try
                    {
                        System.Xml.Linq.XElement xElement = System.Xml.Linq.XElement.Load(dirInfoFile);
                        if (!xElement.IsNull())
                        {
                            System.Xml.Linq.XElement xElementFound = xElement.Elements().Where(e => e.Name == directoryName).FirstOrDefault();
                            result = (xElementFound.IsNull() ? String.Empty : xElementFound.Value.Replace("\n", String.Empty).Replace("\t", String.Empty));
                        }
                        if (tfsMiddleDir.IsNullOrEmpty())
                            result = result.Replace(@"C:\VSTS - GrupoLinx\Framework\Linx Framework\Main\Binary", worksapaceMapedpath);

                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(String.Format("Fail reading the file {0}.", dirInfoFile) + exp.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }

            if (result.IsNullOrEmpty())
                MessageBox.Show(String.Format("The DirectoryInfo [{0}] is not found in the environment {1}!", directoryName, dirPart), "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                //Create Directory If Does Not Exist
                if (!Path.HasExtension(result) && !Directory.Exists(result))
                    Directory.CreateDirectory(result);
            }

            return result;
        }
        private static string GetFullPath(string directoryName)
        {
            string dirtLib = GetDirectoryInfo(directoryName);
            if (!dirtLib.IsNullOrEmpty())
                return dirtLib.Trim();
            else
                return "";
        }
        private static void RemoveReference(Project project, string strAssemblyName)
        {
            try
            {
                VSLangProj.VSProject vsProject = (VSLangProj.VSProject)project.Object;
                VSLangProj.Reference reference = vsProject.References.Find(strAssemblyName);
                if (!reference.IsNullOrEmpty())
                    reference.Remove();
            }
            catch (Exception excep)
            {
                MessageBox.Show(@"Cannot remove the assembly """ + strAssemblyName + @""" to the project!\r\nDetails:\r\n" + excep.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private static VSLangProj.Reference GetReference(Project project, string strAssemblyName)
        {
            if (!strAssemblyName.IsNullOrEmpty())
            {
                if (strAssemblyName.Right(4).ToLower() != ".dll")
                    strAssemblyName += ".dll";

                VSLangProj.VSProject vsProject = (VSLangProj.VSProject)project.Object;
                foreach (VSLangProj.Reference reference in vsProject.References)
                {
                    if (reference.Name == Path.GetFileNameWithoutExtension(strAssemblyName))
                        return reference;
                }
            }

            return null;
        }
        private static VSLangProj.Reference AddNewReference(Project project, string strAssemblyName, bool copyLocal = false, bool specificVersion = false)
        {
            VSLangProj.Reference reference = null;
            try
            {
                if (!project.IsNull())
                {
                    reference = GetReference(project, strAssemblyName);
                    if (reference == null)
                    {
                        VSLangProj.VSProject vsProject = (VSLangProj.VSProject)project.Object;
                        reference = vsProject.References.Add(strAssemblyName);
                    }

                    if (reference != null)
                    {
                        if (reference.CopyLocal != copyLocal)
                            reference.CopyLocal = copyLocal;
                        if (reference is Reference3 && ((Reference3)reference).SpecificVersion != specificVersion)
                            ((Reference3)reference).SpecificVersion = specificVersion;
                    }
                }
            }
            catch (Exception exeption)
            {
                MessageBox.Show("Cannot add the assembly \"" + strAssemblyName + "\" to the project!\r\nDetails:\r\n" + exeption.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return reference;
        }
        private static void UpdateReference(EnvDTE.Project project, string reference, bool remove = false, bool copyLocal = false, bool specificVersion = false)
        {
            VSLangProj.Reference refItem = null;

            if (remove)
                RemoveReference(project, reference);
            else
                refItem = GetReference(project, reference);

            if (refItem != null && refItem.Path.IsNullOrEmpty())
            {
                refItem.Remove();
                refItem = null;
            }

            //Check path
            if (refItem != null)
            {
                string assemblyPath = System.IO.Path.GetDirectoryName(reference).ToLower();
                if (!assemblyPath.IsNullOrEmpty() && System.IO.Path.GetDirectoryName(refItem.Path).ToLower() != assemblyPath)
                {
                    refItem.Remove();
                    refItem = null;
                }
            }

            if (refItem == null)
                refItem = AddNewReference(project, reference, copyLocal, specificVersion);
            else
            {
                refItem.CopyLocal = copyLocal;
                if (refItem is Reference3)
                    ((Reference3)refItem).SpecificVersion = specificVersion;
            }
        }
        public static void AdjustMissingReferences(Project project)
        {
            if (!project.IsNull())
            {
                DocumentPath = System.IO.Path.GetDirectoryName(project.FullName);

                List<string> references = new List<string>();
                VSLangProj.VSProject vsProject = (VSLangProj.VSProject)project.Object;
                foreach (VSLangProj.Reference reference in vsProject.References)
                {
                    if (reference.Path.IsNullOrEmpty() || !File.Exists(reference.Path))
                    {
                        references.Add(reference.Name);
                    }
                }

                string bmPath = GetFullPath("Linx.Business.Models");

                //Adjust BM inconsistent references
                foreach (string reference in references)
                {
                    if (reference.Right(2) == "BM" || reference.Right(2) == "BL" || reference.Contains(".BM."))
                    {
                        string filePath = Path.Combine(bmPath, reference + ".dll");
                        if (File.Exists(filePath))
                        {
                            UpdateReference(project, filePath);
                        }
                    }
                    else
                    {
                        string path = string.Empty;

                        switch (reference)
                        {
                            case "Linx.Tools":
                            case "EntityFramework.Utilities":
                            case "Linx.LinqExtensions":
                                path = GetFullPath("Linx.GAC");
                                break;

                            case "Linx.Data":
                                path = GetFullPath("Linx.Data.Library");
                                break;

                            case "InteractivePreGeneratedViews":
                                path = GetFullPath("Linx.CodeFirst.PreGenViews");
                                break;

                            case "Linx.Business.Common":
                            case "Linx.Business.Tools":
                                path = GetFullPath("Linx.Business.Objects");
                                break;

                            case "AutoMapper":
                            case "AutoMapper.Net4":
                            case "Microsoft.Data.Edm":
                            case "Microsoft.Data.OData":
                            case "Microsoft.Web.Infrastructure":
                            case "Newtonsoft.Json":
                            case "System.Net.Http.Formatting":
                            case "System.Spatial":
                            case "System.Web.Http":
                            case "System.Web.Http.OData":
                            case "System.Web.Http.WebHost":
                            case "WebActivatorEx":
                                path = GetFullPath("Linx.WebApi.Library");
                                break;

                            case "Breeze.ContextProvider":
                            case "Breeze.WebApi2":
                                path = GetFullPath("Linx.DataService.Library");
                                break;

                            case "Linx.DataService":
                                path = GetFullPath("Linx.LinxDataService.Library");
                                break;

                            case "EntityFramework":
                            case "EntityFramework.SqlServer":
                                path = GetFullPath("Linx.CodeFirst.EF");
                                break;

                            case "System.ServiceModel.DomainServices.Hosting":
                            case "System.ServiceModel.DomainServices.Server":
                                path = GetFullPath("Linx.DomainServices");
                                break;

                            default:
                                path = string.Empty;
                                break;
                        }

                        if (!path.IsNullOrEmpty())
                        {
                            UpdateReference(project, Path.Combine(path, reference + ".dll"));
                        }

                    }
                }
            }
        }
        public static bool UpgradeVersion(Project project)
        {
            //Upgrade project to new Framework version if necessary        
            if ((((uint)project.Properties.Item("TargetFramework").Value) != 262406))
            {
                project.Properties.Item("TargetFrameworkMoniker").Value = (new System.Runtime.Versioning.FrameworkName(".NETFramework", new Version(4, 6, 1))).FullName;
                return true;
            }

            return false;
        }

        private static bool UpdatePackages(Project project)
        {
            Dictionary<string, string> packages = new Dictionary<string, string>();
            packages.Add("EntityFramework", "6.1.3");
            foreach (var package in packages)
            {
                string nuget = package.Key, version = package.Value;
                string packagesFile = Path.Combine(project.Properties.Item("FullPath").Value.ToString(), "packages.config");

                if (File.Exists(packagesFile))
                {
                    InstallNuGetPackage(nuget, version, project);
                    return true;
                }
            }
            return false;
        }


        public static void InstallNuGetPackage(string packageID, string version, Project project)
        {
            IPackageRepository repo = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");
            List<IPackage> packages = repo.FindPackagesById(packageID).ToList();

            //Initialize the package manager
            string path = System.IO.Path.GetDirectoryName(project.FullName);
            PackageManager packageManager = new PackageManager(repo, path);

            packageManager.UninstallPackage(packageID);

            //Download and unzip the package
            packageManager.InstallPackage(packageID, SemanticVersion.Parse(version));
        }
    }
}
