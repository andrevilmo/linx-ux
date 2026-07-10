using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Linx.LinqExtensions.Query;
using Linx.LinqExtensions.Functional;
using Linx.LinqExtensions.Expressions;
using Linx;
using Linx.Tools;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ServiceModel.DomainServices.Server;
using Linx.Data;
using System.Text;
using System.Data.Entity.Core.Objects;
using System.Data.Common;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Data.Linq.SqlClient;
using System.Reflection;
using System.Data.Entity.Core.Objects.DataClasses;
using LinxTraining002.BM;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices;



namespace LinxTraining001.BV.CadastroVendas
{
    
    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class VendasView
    {
        public void tstDD()
        {

        }

        /// Execute after save changes.
        public void OnSavedChanges(CadastroVendasDomainService context, ChangeOperation changeOperation)
        {
            
        }
    }
}
