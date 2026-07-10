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
using Linx.Demo.BM;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices;

namespace Linx.Demo.BV.PaiFilha
{
    
    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class Loja
    {

        /// Execute after save changes.
        public void OnSavedChanges(PaiFilhaDomainService context, ChangeOperation changeOperation)
        {
            //this.ComboboxLoja = Convert.ToByte(Domains.LX_COMBOBOX_LOJA.LOJA3.Value);
            //this.ComboboxLojaName = Domains.LX_COMBOBOX_LOJA.LOJA3.DisplayName;
            //context.UpdateLoja(this);
            //context.SaveCustomChanges();
        }

        /// Execute before search data.
        public static void OnSearching(ref IQueryable<Loja> searchDefinition, bool noAssociations, List<EntitySearch> searchList)
        {
            //searchDefinition =
             //(from entity0 in searchDefinition
             // select new Loja()
             // {

             //     BigIntLoja = entity0.BigIntLoja,
             //     BitLoja = entity0.BitLoja
             // ,
             //     CampoGuid = entity0.CampoGuid
             // ,
             //     ComboboxLoja = entity0.ComboboxLoja
             // ,
             //     ComboboxLojaName = entity0.ComboboxLojaName
             // ,
             //     DatetimeLoja = entity0.DatetimeLoja
             // ,
             //     DecimalLoja = entity0.DecimalLoja
             // ,
             //     IdCidade = entity0.IdCidade
             // ,
             //     IdEstado = entity0.IdEstado
             // ,
             //     IdLoja = entity0.IdLoja
             // ,
             //     IdPais = entity0.IdPais
             // ,
             //     IntLoja = entity0.IntLoja
             // ,
             //     NomeCidade = entity0.NomeCidade
             // ,
             //     NomeEstado = entity0.NomeEstado
             // ,
             //     NomeLoja = entity0.NomeLoja
             // ,
             //     NomePais = entity0.NomePais
             // ,
             //     SmallIntLoja = entity0.SmallIntLoja

             // }
             //);
        }
    }
}
