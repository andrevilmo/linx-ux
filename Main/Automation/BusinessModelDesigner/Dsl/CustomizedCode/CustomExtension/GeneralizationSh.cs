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
    public partial class GeneralizationSh
    {
        public void UpdatePropertyRelations()
        {
            if (this.SuperclassSh == null || this.SuperclassSh.BusinessModelDesignerRoot == null || this.SuperclassSh.BusinessModelDesignerRoot.IsLocked)
                return;

            if (this.SuperclassSh != null && this.SubclassSh != null && !(this.SubclassSh is ReferenceModelClass))
            {
                //Check if should remove old primary keys
                var keys = this.SuperclassSh.Attributes.Where(p => p.IsPrimaryKey).Select(p => p.Name).ToList();
                foreach (var attrPK in this.SubclassSh.Attributes.Where(p => p.IsPrimaryKey && !keys.Contains(("." + p.ForeignKey).Right("."))).ToList())
                {
                    this.SubclassSh.Attributes.Remove(attrPK);
                }

                //foreach (var source in this.SuperclassSh.Attributes.Where(p => p.IsPrimaryKey))
                //{
                //    var attr = this.SubclassSh.Attributes.Where(p => p.ForeignKey == this.Id.ToString() + "." + source.Name).FirstOrDefault();
                //    if (attr == null)
                //    {
                //        attr = (ModelAttribute)this.SubclassSh.Attributes.AddNew();
                //        attr.CopyInstanceFrom(source);
                //        attr.DataType = source.DataType;
                //        attr.ForeignKey = this.Id.ToString() + "." + attr.Name;
                //        attr.Name = attr.Name;
                //        this.SubclassSh.Attributes.Move(attr, 0);
                //    }
                //    attr.IsPrimaryKey = true;
                //    attr.IsNullable = false;
                //}
            }
        }

        public void DeletePropertyRelations()
        {
            if (this.SuperclassSh == null || this.SuperclassSh.BusinessModelDesignerRoot == null || this.SuperclassSh.BusinessModelDesignerRoot.IsLocked)
                return;

            if (this.SuperclassSh != null && this.SubclassSh != null && !(this.SubclassSh is ReferenceModelClass))
            {
                foreach (var source in this.SuperclassSh.Attributes.Where(p => p.IsPrimaryKey).ToList())
                {
                    var attr = this.SubclassSh.Attributes.Where(p => p.ForeignKey == this.Id.ToString() + "." + source.Name).FirstOrDefault();
                    if (attr != null)
                        this.SubclassSh.Attributes.Remove(attr);
                }
                this.SubclassSh.AddPrimaryKey();
            }
        }

    }
}
