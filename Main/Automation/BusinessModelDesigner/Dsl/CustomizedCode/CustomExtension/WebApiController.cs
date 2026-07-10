using EnvDTE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linx.Tools;

namespace Linx.BusinessModelDesigner
{
    public partial class WebApiController
    {
        public string GetRoutePrefix()
        {
            return (this.RoutePrefix.IsNullOrEmpty() ? "api/" + this.Name : this.RoutePrefix.Replace("{Name}", this.Name));
        }

        public bool GetIsAspNetCoreValue()
        {
            return this.BusinessModelDesignerRoot.IsAspNetCore || this.BusinessModelDesignerRoot.IsAspNetCoreEnabled();
        }

        #region Files Rules

        public ProjectItem GetControllerFolder()
        {
            Project eadProject = this.BusinessModelDesignerRoot.GetBmdProject();
            if (eadProject != null)
            {
                Project webApiProject = this.BusinessModelDesignerRoot.GetWebApiProject(this.ProjectSuffix, eadProject);
                if (webApiProject != null)
                {
                    return this.BusinessModelDesignerRoot.GetWebApiControllersItem(this, webApiProject);
                }
            }
            return null;
        }

        public void RenameSourceFiles(string oldName)
        {
            if (!oldName.IsNullOrEmpty() && oldName != this.Name)
            {
                ProjectItem item = this.GetControllerFolder(), fileItem;
                if (!item.IsNull())
                {
                    fileItem = BusinessModelDesignerRoot.GetProjectItemByName(item.ProjectItems, oldName + ".cs");
                    if (fileItem != null)
                    {
                        if (this.WebApiActions.Count > 0)
                            fileItem.Name = this.Name + ".cs";
                        else
                            fileItem.Delete();
                    }

                    fileItem = BusinessModelDesignerRoot.GetProjectItemByName(item.ProjectItems, oldName + "AutoGen.cs");
                    if (fileItem != null)
                        fileItem.Name = this.Name + "AutoGen.cs";
                }

                item = item = this.BusinessModelDesignerRoot.GetWebApiAppStartItem(this);
                if (!item.IsNull())
                {
                    fileItem = BusinessModelDesignerRoot.GetProjectItemByName(item.ProjectItems, oldName + "AttributeRoutingHttp.cs");
                    if (fileItem != null)
                        fileItem.Name = this.Name + "AttributeRoutingHttp.cs";
                }
            }
        }

        public void DeleteSourceFiles()
        {
            ProjectItem item = this.GetControllerFolder(), fileItem;
            if (!item.IsNull())
            {
                fileItem = BusinessModelDesignerRoot.GetProjectItemByName(item.ProjectItems, this.Name + ".cs");
                if (fileItem != null)
                    fileItem.Delete();

                fileItem = BusinessModelDesignerRoot.GetProjectItemByName(item.ProjectItems, this.Name + "AutoGen.cs");
                if (fileItem != null)
                    fileItem.Delete();
            }

            item = item = this.BusinessModelDesignerRoot.GetWebApiAppStartItem(this);
            if (!item.IsNull())
            {
                fileItem = BusinessModelDesignerRoot.GetProjectItemByName(item.ProjectItems, this.Name + "AttributeRoutingHttp.cs");
                if (fileItem != null)
                    fileItem.Delete();
            }
        }

        public void DeleteInconsistentFiles(Project webApiProject = null)
        {
            ProjectItem item = this.GetControllerFolder(), fileItem;

            item = item = this.BusinessModelDesignerRoot.GetWebApiAppStartItem(this, webApiProject);
            if (!item.IsNull())
            {
                fileItem = BusinessModelDesignerRoot.GetProjectItemByName(item.ProjectItems, "AttributeRoutingHttp.cs");
                if (fileItem != null)
                    fileItem.Delete();
                if (!this.ExposeAllContext)
                {
                    fileItem = BusinessModelDesignerRoot.GetProjectItemByName(item.ProjectItems, this.Name + "AttributeRoutingHttp.cs");
                    if (fileItem != null)
                        fileItem.Delete();
                }
            }
        }

        #endregion
    }
}
