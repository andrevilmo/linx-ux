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
using Microsoft.VisualStudio.Modeling.Validation;
using System.Linq;
using Linx.Tools;
using Microsoft.VisualStudio.Modeling.Immutability;
using EnvDTE;
using System.Windows.Forms;

namespace Linx.EntityAdapterDesigner
{
    public partial class DomainView
    {
        public string GetInconsistenceInfo()
        {
            if (this.DomainValues.Any(e => e.Value.IsNullOrEmpty()))
                return "Property Value is empty";
            if (this.DomainValues.Any(e => e.DisplayName.IsNullOrEmpty()))
                return "Property DisplayName is empty";
            if (this.DomainValues.Any(e => e.Name.IsNullOrEmpty()))
                return "Property Name is empty";
            if (this.DomainValues.Any(e => this.DomainValues.Any(c => c != e && c.Value == e.Value)))
                return "Property Value is duplicated";
            if (this.DomainValues.Any(e => this.DomainValues.Any(c => c != e && c.DisplayName == e.DisplayName)))
                return "Property DisplayName is duplicated";
            if (this.DomainValues.Any(e => this.DomainValues.Any(c => c != e && c.Name == e.Name)))
                return "Property Name is duplicated";

            return String.Empty;
        }

        public void OpenCustomMethod()
        {
            if (this.HasCustomValues)
            {
                var item = this.EntityAdapterDesignerRoot.GetProjectItemByName(this.EntityAdapterDesignerRoot.GetEadProject(), "Domains");
                if (item != null)
                {
                    var fileName = this.EntityAdapterDesignerRoot.GetDomainViewCustomFile(this, false, item);

                    using (Transaction transaction =
                               this.Store.TransactionManager.BeginTransaction("Domain custom operation."))
                    {   
                        EntityAdapterOperation targetOperation = new EntityAdapterOperation(this.Partition);
                        targetOperation.Name = "GetCustomValues";
                        targetOperation.OverloadName = targetOperation.Name;
                        targetOperation.ReturnType = "Dictionary<string, string>";
                        targetOperation.Parameters = "";
                        targetOperation.Access = OperationAccess.Public;
                        targetOperation.IsStatic = true;
                        string domainService = this.EntityAdapterDesignerRoot.GetContextName();
                        targetOperation.Comment = "For accessing the business data, create an instance of [" + domainService + "." + domainService + "DomainService].";

                        this.EntityAdapterDesignerRoot.GenerateDomainViewExtension(this, item);
                        this.EntityAdapterDesignerRoot.OpenGenericOperation(fileName, targetOperation, this.Name, item);

                        transaction.Rollback();
                    }
                }
            }
            else
            {
                MessageBox.Show("This Domain is not checked as [HasCustomValues].", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } 
        }

    }
}
