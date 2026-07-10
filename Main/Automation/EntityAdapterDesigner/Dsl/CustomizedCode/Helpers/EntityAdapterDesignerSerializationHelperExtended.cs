using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling;
using Linx.Tools;

namespace Linx.EntityAdapterDesigner
{
	public partial class EntityAdapterDesignerSerializationHelper
	{   
        public override EntityAdapterDesignerRoot LoadModel(SerializationResult serializationResult, Partition partition, string fileName, ISchemaResolver schemaResolver, Microsoft.VisualStudio.Modeling.Validation.ValidationController validationController, ISerializerLocator serializerLocator)
        {
            EntityAdapterDesignerRoot modelRoot = base.LoadModel(serializationResult, partition, fileName, schemaResolver, validationController, serializerLocator);
            modelRoot.AdjustDocumentInfo(fileName);
            return modelRoot;
        }
               
        
        //public override void SaveModelAndDiagram(SerializationResult serializationResult, EntityAdapterDesignerRoot modelRoot, string modelFileName, EntityAdapterDesignerDiagram diagram, string diagramFileName, Encoding encoding, bool writeOptionalPropertiesWithDefaultValue)
        //{
        //    AdjustStructuralInfo(modelRoot, modelFileName, false);
        //    base.SaveModelAndDiagram(serializationResult, modelRoot, modelFileName, diagram, diagramFileName, encoding, writeOptionalPropertiesWithDefaultValue);
        //}
        
	}
}
