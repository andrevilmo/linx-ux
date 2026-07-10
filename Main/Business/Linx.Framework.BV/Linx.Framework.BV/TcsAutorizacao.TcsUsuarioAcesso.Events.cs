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
#if !SILVERLIGHT
using System.ServiceModel.DomainServices.Server;
using Linx.Data;
#endif
using System.Text;
using System.Data.Objects;
using System.Data.Common;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Data.Linq.SqlClient;
using System.Reflection;
using System.Data.Objects.DataClasses;
using Linx.Framework.Autorizacao.BM;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices;

namespace Linx.TCS0101.BO.TcsAutorizacao
{

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsUsuarioAcesso
    {
        /// Execute before save context changes.
        public static void OnSavingContextChanges(TcsAutorizacaoDomainService context, ChangeSetEntry[] entities)
        {
            //Verifica se existe aplicativo diferente de 1 (Linx Ux) e 15 (Linx Services) cadastrado mais de uma vez
            var apps = from result in entities.Where(i => i.Entity is TcsUsuarioAcesso && (((TcsUsuarioAcesso)i.Entity).IdAplicativo != 1 && ((TcsUsuarioAcesso)i.Entity).IdAplicativo != 15) && i.Operation != DomainOperation.Delete).Select(i => i.Entity as TcsUsuarioAcesso)
                       group result by new { result.UidUsuario, result.IdAplicativo } into groupId
                       where groupId.Count() > 1
                       select groupId;

            if (apps.Count() > 0)
            {
                throw new DomainException(String.Format("Somente os aplicativos {0} e {1} permitem múltiplos acessos.".Translate(), Linx.TCS0101.BO.Domains.IdAplicativo.GetValues()["1"], Linx.TCS0101.BO.Domains.IdAplicativo.GetValues()["15"]));
            }
        }
    }
}
