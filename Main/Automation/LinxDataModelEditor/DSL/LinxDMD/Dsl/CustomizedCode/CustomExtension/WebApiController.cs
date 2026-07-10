using EnvDTE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linx.Tools;

namespace Linx.BusinessDataModelDesigner
{
    public partial class WebApiController
    {
        public string GetRoutePrefix()
        {
            return (this.RoutePrefix.IsNullOrEmpty() ? "api/" + this.Name : this.RoutePrefix.Replace("{Name}", this.Name));
        }

        #region Files Rules

        public ProjectItem GetControllerFolder(string folderName = "")
        {
            if (folderName.IsNullOrEmpty())
                folderName = this.ProjectSuffix;
            var srcFolder = this.BusinessDataModelDesignerRoot.GetSrcFolder();
            return BusinessDataModelDesignerRoot.GetProjectItemByName(srcFolder.ProjectItems, folderName);
        }

        public void RenameSourceFiles(string oldName)
        {
            if (!oldName.IsNullOrEmpty() && oldName != this.ProjectSuffix)
            {
                ProjectItem item = this.GetControllerFolder(oldName);
                if (!item.IsNull())
                {
                    item.Name = this.ProjectSuffix;
                }
            }
        }

        public void DeleteSourceFiles()
        {
            ProjectItem item = this.GetControllerFolder(), fileItem;
            if (!item.IsNull())
            {
                fileItem = BusinessDataModelDesignerRoot.GetProjectItemByName(item.ProjectItems, this.Name + ".cs");
                if (fileItem != null)
                    fileItem.Delete();

                fileItem = BusinessDataModelDesignerRoot.GetProjectItemByName(item.ProjectItems, this.Name + "AutoGen.cs");
                if (fileItem != null)
                    fileItem.Delete();
            }

            item = item = this.BusinessDataModelDesignerRoot.GetWebApiAppStartItem(this);
            if (!item.IsNull())
            {
                fileItem = BusinessDataModelDesignerRoot.GetProjectItemByName(item.ProjectItems, this.Name + "AttributeRoutingHttp.cs");
                if (fileItem != null)
                    fileItem.Delete();
            }
        }

        public void DeleteInconsistentFiles(Project webApiProject = null)
        {
            ProjectItem item = this.GetControllerFolder(), fileItem;

            item = item = this.BusinessDataModelDesignerRoot.GetWebApiAppStartItem(this, webApiProject);
            if (!item.IsNull())
            {
                fileItem = BusinessDataModelDesignerRoot.GetProjectItemByName(item.ProjectItems, "AttributeRoutingHttp.cs");
                if (fileItem != null)
                    fileItem.Delete();
                if (!this.ExposeAllContext)
                {
                    fileItem = BusinessDataModelDesignerRoot.GetProjectItemByName(item.ProjectItems, this.Name + "AttributeRoutingHttp.cs");
                    if (fileItem != null)
                        fileItem.Delete();
                }
            }
        }

        #endregion
    }
}
