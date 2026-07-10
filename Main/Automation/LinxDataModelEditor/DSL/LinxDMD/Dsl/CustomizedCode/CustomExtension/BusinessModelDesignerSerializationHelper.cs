using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling;

namespace Linx.BusinessDataModelDesigner
{
    public partial class BusinessDataModelDesignerSerializationHelper
    {
        public override BusinessDataModelDesignerRoot LoadModel(SerializationResult serializationResult, Partition partition, string fileName, ISchemaResolver schemaResolver, Microsoft.VisualStudio.Modeling.Validation.ValidationController validationController, ISerializerLocator serializerLocator)
        {
            BusinessDataModelDesignerRoot modelRoot = base.LoadModel(serializationResult, partition, fileName, schemaResolver, validationController, serializerLocator);
            if (modelRoot != null)
            {
                modelRoot.AdjustDocumentInfo(fileName);                
            }
            return modelRoot;
        }        
    }
}
