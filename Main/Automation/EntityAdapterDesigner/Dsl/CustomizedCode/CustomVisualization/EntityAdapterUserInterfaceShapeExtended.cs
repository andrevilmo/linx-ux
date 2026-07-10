using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnvDTE;
using System.IO;
using Linx.Tools;
using Linx.Builder.Resources;
using System.CodeDom;
using System.Windows.Forms;
using Microsoft.VisualStudio.Modeling;
using Linx.EntityAdapterDesigner.CustomizedCode;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System.Drawing;

namespace Linx.EntityAdapterDesigner
{
    public partial class EntityAdapterUserInterfaceShape
    {
        public override void OnDoubleClick(DiagramPointEventArgs e)
        {
            foreach (var element in e.DiagramHitTestInfo.HitDiagramItem.RepresentedElements)
            {
                if (element is UserInterfaceClientEvent)
                    ((UserInterfaceClientEvent)element).EntityAdapterUserInterface.EntityAdapterDesignerRoot.OpenClientUiEvent(((UserInterfaceClientEvent)element));
                else if (element is EntityAdapterUserInterfaceShape)
                {
                    var ui = ((EntityAdapterUserInterface)((EntityAdapterUserInterfaceShape)element).ModelElement);
                    if (ui != null)
                    {
                        FormUserInterface preview = new FormUserInterface() { UserInterface = ui };
                        preview.ShowDialog();
                    }
                }
                break;
            }

            base.OnDoubleClick(e);
        }

        public void SetOutlineColor(System.Drawing.Color color)
        {
            using (Transaction tran = this.Store.TransactionManager.BeginTransaction("Change color"))
            {
                this.OutlineColor = color;
                tran.Commit();
            }
        }

        public void SetTextColor(System.Drawing.Color color)
        {
            using (Transaction tran = this.Store.TransactionManager.BeginTransaction("Change color"))
            {
                this.TextColor = color;
                tran.Commit();
            }
        }

        private static bool initializeFromMappings;
        protected override void InitializeFromMappings()
        {
            if (!EntityAdapterUserInterfaceShape.initializeFromMappings && EntityAdapterUserInterfaceShape.compartmentMappings != null && EntityAdapterUserInterfaceShape.compartmentMappings.Count > 0)
            {
                ElementListCompartmentMapping operationMapping = EntityAdapterUserInterfaceShape.compartmentMappings.First().Value.FirstOrDefault(e => e.CompartmentId == "ClientEventsCompartiment") as ElementListCompartmentMapping;
                if (operationMapping != null)
                {
                    operationMapping.ImageGetter = EntityAdapterUserInterfaceShape.GetElementImage;
                    EntityAdapterUserInterfaceShape.initializeFromMappings = true;
                }
            }
            base.InitializeFromMappings();
        }

        /// <summary>
        /// Decides what the icon of the Attribute will be in the class shape
        /// </summary>
        private static Image GetElementImage(ModelElement mel)
        {
            UserInterfaceClientEvent member = mel as UserInterfaceClientEvent;
            if ((member != null))
            {
                if (member.ExposedByViewModel)
                    return Resources.VSObject_Exposed;
            }

            return null;
        }

    }

}
