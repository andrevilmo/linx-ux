using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Linx;
using Linx.Tools;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Linx.Data;
using System.Text;
using System.Data.Entity.Core.Objects;
using System.Data.Common;
using System.Runtime.Serialization;
using System.Reflection;
using Linx.Demo.BM;

namespace Linx.Demo.BV.Graf_dash_KPI
{
    
    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class Produto
    {
        public static void OnSearching(ref IQueryable<Produto> searchDefinition, bool noAssociations, List<EntitySearch> searchList)
        {
            /*
            searchDefinition = (from pt in searchDefinition
                                where pt.IdProduto >= 1
                                select pt);

    */

        }
    }
}
