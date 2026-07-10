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
using Linx.Framework.ControleSistema.BM;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices;

namespace Linx.Framework.BV.Rede
{

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TbcBandeiraRede
    {
        /// Replace the automatic search method.
        public static IQueryable<TbcBandeiraRede> OnSearchingReplacement(ControleSistemaContext context, string dynQuery, List<ObjectParameter> parameters, List<EntitySearch> entitySearchList)
        {
            var idUsuario = BusinessUserServiceHelper.GetCurrentUserId();

            List<TbcBandeiraRede> bandeiraRede;

            if (!idUsuario.IsNull())
            {
                List<Int64> tcsPerfil =
                         (from result1 in context.TCS_USUARIO_PERFIL
                          let result = result1.TCS_PERFIL
                          let result2 = result1.TCS_USUARIO
                          where !result.INATIVO && result2.ID_USUARIO == idUsuario
                          select result.ID_PERFIL).ToList();

                bandeiraRede = 
                    ((from entity0 in context.TCS_USUARIO_BANDEIRA_REDE
                     let entity0Al1 = entity0.TBC_BANDEIRA_REDE
                     let entity0al2 = entity0.TCS_USUARIO
                     where entity0al2.ID_USUARIO == idUsuario
                     select new TbcBandeiraRede()
                     {
                         CodBandeiraRede = entity0Al1.COD_BANDEIRA_REDE,
                         DataAtualizacao = entity0Al1.DATA_ATUALIZACAO,
                         DataCadastro = entity0Al1.DATA_CADASTRO,
                         DescBandeiraRede = entity0Al1.DESC_BANDEIRA_REDE,
                         IdBandeiraRede = entity0.ID_BANDEIRA_REDE,
                     }
                ).Union
                (from entity0 in context.TCS_PERFIL_BANDEIRA_REDE
                 let entity0Al1 = entity0.TBC_BANDEIRA_REDE
                 where tcsPerfil.Contains(entity0.ID_PERFIL)
                 select new TbcBandeiraRede()
                 {
                     CodBandeiraRede = entity0Al1.COD_BANDEIRA_REDE,
                     DataAtualizacao = entity0Al1.DATA_ATUALIZACAO,
                     DataCadastro = entity0Al1.DATA_CADASTRO,
                     DescBandeiraRede = entity0Al1.DESC_BANDEIRA_REDE,
                     IdBandeiraRede = entity0.ID_BANDEIRA_REDE,
                 }
                )).ToList();
            }
            else
            {
                bandeiraRede =
                    ((from entity0Al1 in context.TBC_BANDEIRA_REDE
                     select new TbcBandeiraRede()
                     {
                         CodBandeiraRede = entity0Al1.COD_BANDEIRA_REDE,
                         DataAtualizacao = entity0Al1.DATA_ATUALIZACAO,
                         DataCadastro = entity0Al1.DATA_CADASTRO,
                         DescBandeiraRede = entity0Al1.DESC_BANDEIRA_REDE,
                         IdBandeiraRede = entity0Al1.ID_BANDEIRA_REDE,
                     }
                )).ToList();
            }


            Linx.Framework.BV.Multimidia.MultimidiaDomainService ds = new Multimidia.MultimidiaDomainService();

            foreach (TbcBandeiraRede item in bandeiraRede)
            {
                item.Midia = ds.GetMultimedia("TBC_BANDEIRA_REDE", item.IdBandeiraRede, null, null, null, null).FirstOrDefault();
            }

            return bandeiraRede.AsQueryable();

        }
    }
}
