using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnvDTE;
using Linx.Tools;

namespace Linx.EntityAdapterDesigner
{
    partial class ClientLocalService
    {
        public void DeleteClientAppSourceFiles()
        {
            var serviceName = this.EntityAdapterDesignerRoot.ClientErpCodeGen.GetClientErpDataFactoryName(this);
            var fileItem = this.EntityAdapterDesignerRoot.ClientErpCodeGen.GetResourceFile(serviceName);
            if (fileItem != null)
                fileItem.Delete();

            serviceName = this.EntityAdapterDesignerRoot.ClientErpCodeGen.GetClientErpDataFactoryName(this, true);
            fileItem = this.EntityAdapterDesignerRoot.ClientErpCodeGen.GetResourceFile(serviceName);
            if (fileItem != null)
                fileItem.Delete();

        }

        public void RenameClientErpSourceFiles(string oldName)
        {
            if (!oldName.IsNullOrEmpty() && oldName != this.Name)
            {
                var serviceName = this.EntityAdapterDesignerRoot.ClientErpCodeGen.GetClientErpDataFactoryName(this);                
                var fileItem = this.EntityAdapterDesignerRoot.ClientErpCodeGen.GetResourceFile(oldName + "ClientErpFactory");
                if (fileItem != null)
                    fileItem.Name = serviceName + ".res";

                serviceName = this.EntityAdapterDesignerRoot.ClientErpCodeGen.GetClientErpDataFactoryName(this, true);                
                fileItem = this.EntityAdapterDesignerRoot.ClientErpCodeGen.GetResourceFile(oldName + "ExtendedClientErpFactory");
                if (fileItem != null)
                    fileItem.Name = serviceName + ".res";
            }
        }

        public void DeleteMobileSourceFiles()
        {
            ProjectItem item = this.EntityAdapterDesignerRoot.MobileCodeGen.GetMobileAppFactoryFolder();
            if (!item.IsNull())
            {
                var serviceName = this.EntityAdapterDesignerRoot.MobileCodeGen.GetMobileDataFactoryName(this);
                var fileItem = EntityAdapterDesignerRoot.GetProjectItemByName(item.ProjectItems, serviceName + ".js");
                if (fileItem != null)
                    fileItem.Delete();

                fileItem = this.EntityAdapterDesignerRoot.MobileCodeGen.GetResourceFile(serviceName);
                if (fileItem != null)
                    fileItem.Delete();

                serviceName = this.EntityAdapterDesignerRoot.MobileCodeGen.GetMobileDataFactoryName(this, true);
                fileItem = EntityAdapterDesignerRoot.GetProjectItemByName(item.ProjectItems, serviceName + ".js");
                if (fileItem != null)
                    fileItem.Delete();

                fileItem = this.EntityAdapterDesignerRoot.MobileCodeGen.GetResourceFile(serviceName);
                if (fileItem != null)
                    fileItem.Delete();
            }
        }

        public void RenameMobileSourceFiles(string oldName)
        {
            if (!oldName.IsNullOrEmpty() && oldName != this.Name)
            {
                ProjectItem item = this.EntityAdapterDesignerRoot.MobileCodeGen.GetMobileAppFactoryFolder();
                if (!item.IsNull())
                {
                    var serviceName = this.EntityAdapterDesignerRoot.MobileCodeGen.GetMobileDataFactoryName(this);
                    var fileItem = EntityAdapterDesignerRoot.GetProjectItemByName(item.ProjectItems, oldName.ToCamelCase() + "Factory.js");
                    if (fileItem != null)
                        fileItem.Name = serviceName + ".js";

                    fileItem = this.EntityAdapterDesignerRoot.MobileCodeGen.GetResourceFile(oldName.ToCamelCase() + "Factory");
                    if (fileItem != null)
                        fileItem.Name = serviceName + ".res";

                    serviceName = this.EntityAdapterDesignerRoot.MobileCodeGen.GetMobileDataFactoryName(this, true);
                    fileItem = EntityAdapterDesignerRoot.GetProjectItemByName(item.ProjectItems, oldName.ToCamelCase() + "ExtendedFactory.js");
                    if (fileItem != null)
                        fileItem.Name = serviceName + ".js";

                    fileItem = this.EntityAdapterDesignerRoot.MobileCodeGen.GetResourceFile(oldName.ToCamelCase() + "ExtendedFactory");
                    if (fileItem != null)
                        fileItem.Name = serviceName + ".res";
                }
            }
        }

        public bool ExistsClientEvent(string eventName)
        {
            return this.ServiceClientEvents.Any(e => e.Name == eventName);
        }

        public List<ServiceClientEvent> GetClientEvents()
        {
            return this.ServiceClientEvents.ToList();
        }

        public List<string> GetClientEventNames()
        {
            List<string> result = new List<string>();

            result.Add("OnInit");
            result.Add("OnClearing");
            result.Add("OnCleared");
            result.Add("OnSearching");
            result.Add("OnSearched");
            result.Add("OnEditing");
            result.Add("OnEdited");
            result.Add("OnPrinting");
            result.Add("OnPrinted");
            result.Add("OnCancelling");
            result.Add("OnCancelled");
            result.Add("OnSaving");
            result.Add("OnSaved");
            result.Add("OnToolbarAction");
            result.Add("OnReporting");

            return result;
        }
        
        public string GetClientEventDefinition(string eventName)
        {
            string returnType = "void", parameters = "";

            switch (eventName)
            {
                case "OnSearching":
                    returnType = "string";
                    break;
                case "OnReporting":
                    returnType = "string";
                    parameters = "string reportName";
                    break;
                case "OnToolbarAction":
                    returnType = "bool";
                    parameters = "string action";
                    break;
                case "OnClearing":
                case "OnEditing":
                case "OnPrinting":
                case "OnCancelling":
                case "OnClosing":
                    returnType = "bool";
                    break;
                case "OnSaving":
                    returnType = "bool";
                    parameters = "object[] changes";
                    break;
                case "OnSaved":
                    parameters = "object[] changes";
                    break;
                default:
                    break;
            }

            return returnType + " | " + parameters;
        }

        public void AddClientEvent(string eventName, Dictionary<string, string> messages)
        {
            ServiceClientEvent customEvent = new ServiceClientEvent(this.Partition);
            customEvent.Name = eventName;
            customEvent.OverloadName = eventName;
            customEvent.IsUniqueOverload = true;
            customEvent.Access = OperationAccess.Public;
            customEvent.Exposed = false;
            customEvent.IsOutputMessage = false;

            if (messages.ContainsKey(eventName))
            {
                customEvent.IsInputMessage = true;
                customEvent.ReturnType = messages[eventName].Left(" | ");
                customEvent.Parameters = messages[eventName].Right(" | ");
            }
            else
            {
                string eventConfig = GetClientEventDefinition(eventName);
                customEvent.IsInputMessage = false;
                customEvent.ReturnType = eventConfig.Left(" | ");
                customEvent.Parameters = eventConfig.Right(" | ");
            }

            //Add event
            this.ServiceClientEvents.Add(customEvent);
        }
    }
}
