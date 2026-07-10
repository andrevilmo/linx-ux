using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling;

namespace Linx.BusinessModelDesigner
{
    public partial class BusinessModelDesignerSerializationHelper
    {
        public override BusinessModelDesignerRoot LoadModel(SerializationResult serializationResult, Partition partition, string fileName, ISchemaResolver schemaResolver, Microsoft.VisualStudio.Modeling.Validation.ValidationController validationController, ISerializerLocator serializerLocator)
        {
            BusinessModelDesignerRoot modelRoot = base.LoadModel(serializationResult, partition, fileName, schemaResolver, validationController, serializerLocator);
            if (modelRoot != null)
            {
                modelRoot.AdjustDocumentInfo(fileName);                
            }
            return modelRoot;
        }        
    }
}
