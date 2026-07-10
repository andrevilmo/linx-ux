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
    public partial class Generalization
    {
        public void UpdatePropertyRelations()
        {
            if (this.Superclass == null || this.Superclass.BusinessModelDesignerRoot == null || this.Superclass.BusinessModelDesignerRoot.IsLocked)
                return;

            if (this.Superclass != null && this.Subclass != null && !(this.Subclass is ReferenceModelClass))
            {
                //Check if should remove old primary keys
                var keys = this.Superclass.Attributes.Where(p => p.IsPrimaryKey).Select(p => p.Name).ToList();
                foreach (var attrPK in this.Subclass.Attributes.Where(p => p.IsPrimaryKey && !keys.Contains(("." + p.ForeignKey).Right("."))).ToList())
                {
                    this.Subclass.Attributes.Remove(attrPK);
                }

                //foreach (var source in this.Superclass.Attributes.Where(p => p.IsPrimaryKey))
                //{
                //    var attr = this.Subclass.Attributes.Where(p => p.ForeignKey == this.Id.ToString() + "." + source.Name).FirstOrDefault();
                //    if (attr == null)
                //    {
                //        attr = (ModelAttribute)this.Subclass.Attributes.AddNew();
                //        attr.CopyInstanceFrom(source);
                //        attr.DataType = source.DataType;
                //        attr.ForeignKey = this.Id.ToString() + "." + attr.Name;
                //        attr.Name = attr.Name;
                //        this.Subclass.Attributes.Move(attr, 0);
                //    }
                //    attr.IsPrimaryKey = true;
                //    attr.IsNullable = false;
                //}
            }
        }

        public void DeletePropertyRelations()
        {
            if (this.Superclass == null || this.Superclass.BusinessModelDesignerRoot == null || this.Superclass.BusinessModelDesignerRoot.IsLocked)
                return;

            if (this.Superclass != null && this.Subclass != null && !(this.Subclass is ReferenceModelClass))
            {
                foreach (var source in this.Superclass.Attributes.Where(p => p.IsPrimaryKey).ToList())
                {
                    var attr = this.Subclass.Attributes.Where(p => p.ForeignKey == this.Id.ToString() + "." + source.Name).FirstOrDefault();
                    if (attr != null)
                        this.Subclass.Attributes.Remove(attr);
                }
                this.Subclass.AddPrimaryKey();
            }
        }

    }
}
