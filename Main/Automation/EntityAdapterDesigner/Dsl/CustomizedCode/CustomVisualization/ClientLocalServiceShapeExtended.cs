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
    public partial class ClientLocalServiceShape
	{
        public override void OnDoubleClick(DiagramPointEventArgs e)
        {
            foreach (var element in e.DiagramHitTestInfo.HitDiagramItem.RepresentedElements)
            {
                if (element is ServiceClientEvent)
                    ((ServiceClientEvent)element).ClientLocalService.EntityAdapterDesignerRoot.OpenClientServiceEvent(((ServiceClientEvent)element));
                else if (element is ClientLocalServiceShape)
                {
                    var service = ((ClientLocalService)((ClientLocalServiceShape)element).ModelElement);
                    if (service != null)
                    {
                        service.EntityAdapterDesignerRoot.OpenClientLocalServiceFile(service);                       
                    }
                }
                break;
            }

            base.OnDoubleClick(e);
        }

        private static bool initializeFromMappings;
        protected override void InitializeFromMappings()
        {
            if (!ClientLocalServiceShape.initializeFromMappings && ClientLocalServiceShape.compartmentMappings != null && ClientLocalServiceShape.compartmentMappings.Count > 0)
            {
                ElementListCompartmentMapping operationMapping = ClientLocalServiceShape.compartmentMappings.First().Value.FirstOrDefault(e => e.CompartmentId == "ServiceClientEventsCompartiment") as ElementListCompartmentMapping;
                if (operationMapping != null)
                {
                    operationMapping.ImageGetter = ClientLocalServiceShape.GetElementImage;
                    ClientLocalServiceShape.initializeFromMappings = true;
                }

                ElementListCompartmentMapping propertyMapping = ClientLocalServiceShape.compartmentMappings.First().Value.FirstOrDefault(e => e.CompartmentId == "ServiceClientPropertiesCompartiment") as ElementListCompartmentMapping;
                if (propertyMapping != null)
                {
                    propertyMapping.ImageGetter = ClientLocalServiceShape.GetElementImage;
                    ClientLocalServiceShape.initializeFromMappings = true;
                }

            }
            base.InitializeFromMappings();
        }

        /// <summary>
        /// Decides what the icon of the Attribute will be in the class shape
        /// </summary>
        private static Image GetElementImage(ModelElement mel)
        {
            ServiceClientEvent memberEvent = mel as ServiceClientEvent;
            if ((memberEvent != null))
            {
                if (memberEvent.IsOutputMessage)
                    return Resources.VSObject_OutputMessage;
                else if (memberEvent.IsInputMessage)
                    return Resources.VSObject_InputMessage;
                else if (memberEvent.Exposed)
                    return Resources.VSObject_Exposed;
            }
            else
            {
                var modelProperty = mel as ServiceClientProperty;
                if ((modelProperty != null))
                {
                    if (modelProperty.Exposed)
                        return Resources.VSObject_Properties;
                }
            }

            return null;
        }
		
	}

}
