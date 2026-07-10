using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linx.Tools;
using EnvDTE;
using Linx.EntityAdapterDesigner.CustomizedCode.Apps.SPA;

namespace Linx.EntityAdapterDesigner
{
    public partial class WebApiController
    {
        public string GetRoutePrefix()
        {
            return (this.RoutePrefix.IsNullOrEmpty() ? this.Name : this.RoutePrefix.Replace("{Name}", this.Name));
        }

        public bool GetIsAspNetCoreValue()
        {
            return this.EntityAdapterDesignerRoot.IsAspNetCore;
        }

        #region Files Rules

        public ProjectItem GetControllerFolder()
        {
            Project eadProject = this.EntityAdapterDesignerRoot.GetEadProject();
            if (eadProject != null)
            {
                Project webApiProject = this.EntityAdapterDesignerRoot.GetWebApiProject(this.ProjectSuffix, eadProject);
                if (webApiProject != null)
                {
                    return this.EntityAdapterDesignerRoot.GetWebApiControllersItem(this, webApiProject);
                }
            }
            return null;
        }

        public void RenameSourceFiles(string oldName)
        {
            if (!oldName.IsNullOrEmpty() && oldName != this.Name)
            {
                ProjectItem item = this.GetControllerFolder();
                if (!item.IsNull())
                {
                    var fileItem = EntityAdapterDesignerRoot.GetProjectItemByName(item.ProjectItems, oldName + ".cs");
                    if (fileItem != null)
                    {
                        if (this.WebApiActions.Count > 0)
                            fileItem.Name = this.Name + ".cs";
                        else
                            fileItem.Delete();
                    }

                    fileItem = EntityAdapterDesignerRoot.GetProjectItemByName(item.ProjectItems, oldName + "AutoGen.cs");
                    if (fileItem != null)
                        fileItem.Name = this.Name + "AutoGen.cs";
                }
            }
        }
        
        public void DeleteInconsistentFiles(Project webApiProject = null)
        {
            if (!(this.SynchronizedWithDomainService && this.IsDataService))
            {
                ProjectItem item = this.GetControllerFolder(), fileItem;
                item = item = this.EntityAdapterDesignerRoot.GetWebApiAppStartItem(this, webApiProject);
                if (!item.IsNull())
                {
                    fileItem = EntityAdapterDesignerRoot.GetProjectItemByName(item.ProjectItems, this.Name + "ODataStart.cs");
                    if (fileItem != null)
                        fileItem.Delete();
                }
            }
        }

        public void DeleteWebApiAppStartCode(Project webApiProject = null)
        {
            ProjectItem item = this.GetControllerFolder(), fileItem;

            item = item = this.EntityAdapterDesignerRoot.GetWebApiAppStartItem(this, webApiProject);
            if (!item.IsNull())
            {
                fileItem = EntityAdapterDesignerRoot.GetProjectItemByName(item.ProjectItems, "AttributeRoutingHttp.cs");
                if (fileItem != null)
                    fileItem.Delete();               
            }
        }

        public void DeleteSourceFiles()
        {
            ProjectItem item = this.GetControllerFolder();
            if (!item.IsNull())
            {
                var fileItem = EntityAdapterDesignerRoot.GetProjectItemByName(item.ProjectItems, this.Name + ".cs");
                if (fileItem != null)
                    fileItem.Delete();

                fileItem = EntityAdapterDesignerRoot.GetProjectItemByName(item.ProjectItems, this.Name + "AutoGen.cs");
                if (fileItem != null)
                    fileItem.Delete();
            }
            DeleteSpaServiceCode();
            DeleteMobileDataServiceApiCode();
        }

        public void DeleteSpaServiceCode()
        {
            if (this.SynchronizedWithDomainService)
            {
                var item = this.EntityAdapterDesignerRoot.SpaCodeGen.GetSpaAppFolder("services");
                if (!item.IsNull())
                {
                    var fileItem = this.EntityAdapterDesignerRoot.GetProjectItemByName(item.ProjectItems, this.EntityAdapterDesignerRoot.SpaCodeGen.GetSpaContextName() + ".js");
                    if (fileItem != null)
                        fileItem.Delete();
                }
            }
        }

        public void DeleteMobileDataServiceApiCode()
        {
            if (this.SynchronizedWithDomainService)
            {
                var item = this.EntityAdapterDesignerRoot.MobileCodeGen.GetMobileAppServiceFolder();
                if (!item.IsNull())
                {
                    var fileItem = this.EntityAdapterDesignerRoot.GetProjectItemByName(item.ProjectItems, this.EntityAdapterDesignerRoot.MobileCodeGen.GetMobileDataServiceApiName() + ".js");
                    if (fileItem != null)
                        fileItem.Delete();
                }
            }
        }
        

        #endregion
    }
}
