//***************************************************************************
//
//    Copyright (c) Microsoft Corporation. All rights reserved.
//    This code is licensed under the MICROSOFT VISUAL STUDIO 2010
//    VISUALIZATION AND MODELING SOFTWARE DEVELOPMENT KIT license terms.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//***************************************************************************
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Integration;
using Microsoft.VisualStudio.Modeling.Integration.Picker;
using Microsoft.VisualStudio.Modeling.Validation;
using System.Linq;
using Linx.Tools;

namespace Linx.BusinessModelDesigner
{
    public partial class MultipleAssociationTarget
    {
        public void UpdatePropertyRelations()
        {
            if (this.TargetType == null || this.TargetType.BusinessModelDesignerRoot == null || this.TargetType.BusinessModelDesignerRoot.IsLocked)
                return;

            if (this.TargetType != null && !(this.TargetType is ReferenceModelClass))
            {
                if (this.MultipleAssociation.OriginTypes.Count > 0)
                {
                    foreach (var link in MultipleAssociationOrigin.GetLinksToOriginTypes(this.MultipleAssociation))
                    {
                        link.UpdatePropertyRelations(String.Empty);
                    }
                }                                
            }
        }

        public void DeletePropertyRelations()
        {
            if (this.TargetType == null || this.TargetType.BusinessModelDesignerRoot == null || this.TargetType.BusinessModelDesignerRoot.IsLocked)
                return;

            if (this.TargetType != null && !(this.TargetType is ReferenceModelClass))
            {
                foreach (var attr in this.TargetType.Attributes.Where(p => p.IsPrimaryKey).ToList())
                {
                    this.TargetType.Attributes.Remove(attr);
                }
                this.TargetType.AddPrimaryKey();
            }
        }
    }
}
