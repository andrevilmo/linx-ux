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
using Linx.Framework.ControleSistema.BM;
using System.ServiceModel.DomainServices.Server;
using Linx.Business.Tools;
using Linx.Framework.BV.Domains;

namespace Linx.Framework.Custom.BV.PerfilFranquia
{

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsPerfilRegraTransacao
    {
        public static void OnSavedContextChanges(PerfilFranquiaDomainService context, ChangeSetEntry[] entities)
        {
            if (entities.Where(i => i.Entity is TcsPerfilRegraTransacao && i.Operation != DomainOperation.None).Count() > 0)
            {
                Framework.BV.Utils.RemoveModulesFromCache();
            }
        }

        public static void OnLookingUpLookUpTcsPerfilRegraTransacao(ref IQueryable<LookUpTcsPerfilRegraTransacao> searchDefinition, string propertyName, EntitySearch entitySearch)
        {
            searchDefinition = from result in Framework.BV.Utils.GetLookupTransacaoPerfil(entitySearch)
                               select new LookUpTcsPerfilRegraTransacao()
                               {
                                   IdTransacao = result.IdTransacao,
                                   ClasseNome = result.ClasseNome,
                                   DescTransacao = result.DescTransacao,
                                   Origem = result.Origem
                               };
        }

        public static void OnLookingUpLookupLxRegraAcessoTransacao(ref IQueryable<LookupLxRegraAcessoTransacao> searchDefinition, string propertyName, EntitySearch entitySearch)
        {
            Dictionary<byte, string> regras = new Dictionary<byte, string>();
            RegraAcesso.GetValues().Where(i => i.Key != "99" && i.Key != "13").Foreach(item =>
            {
                regras.Add(Convert.ToByte(item.Key), item.Value);
            });

            KeyValuePair<byte, string> bloqueado = regras.Where(i => i.Key == 1).FirstOrDefault();

            //LxRegraAcessoTransacaoName
            string regraAcesso = string.Empty;

            EntitySearchExpression expression = entitySearch.Expressions.Where(i => i.Name == "Field" && i.Value.ToString() == "LxRegraAcessoTransacaoName").FirstOrDefault();
            if (!expression.IsNull())
            {
                int fieldPos = entitySearch.Expressions.IndexOf(expression);
                regraAcesso = (entitySearch.Expressions[fieldPos + 2].Value).ToString().Replace("%", "").ToUpper();
                Linx.Framework.BV.Utils.RemoveExpressionFromEntitySearh(entitySearch, expression, fieldPos);
            }

            entitySearch.EntityName = string.Empty;
            string serializedString = SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch>() { entitySearch });
            Int64 idUsuario = UserServiceHelper.GetCurrentUserId().GetValueOrDefault();
            PerfilFranquiaDomainService ds = new PerfilFranquiaDomainService();
            List<Int64> perfis = ds.GetPerfilList(idUsuario);

            Linx.Framework.BV.Usuario.UsuarioDomainService dsUsuario = new Framework.BV.Usuario.UsuarioDomainService();

            //Regra Transação Usuário
            var regraUsuario = dsUsuario.GetTcsUsuarioRegraTransacaoByEntitySearchNoAssociations(serializedString).Where(i => i.IdUsuario == idUsuario).Select(i => new { i.LxRegraAcessoTransacao, i.LxRegraAcessoTransacaoName }).ToList();

            if (regraUsuario.Count() > 0)
            {
                //Se Acesso Total adiciona todas as opções
                if (regraUsuario.Where(i => i.LxRegraAcessoTransacao == 2).Count() > 0)
                {
                    searchDefinition = regras.Select(i => new LookupLxRegraAcessoTransacao() { LxRegraAcessoTransacao = i.Key, LxRegraAcessoTransacaoName = i.Value }).AsQueryable();
                }
                else
                {
                    //Adiciona acesso bloqueado
                    if (regraUsuario.Where(i => i.LxRegraAcessoTransacao == 1).Count() == 0)
                    {
                        regraUsuario.Add(new { LxRegraAcessoTransacao = bloqueado.Key, LxRegraAcessoTransacaoName = bloqueado.Value });
                    }
                    if (regraAcesso.IsNullOrEmpty())
                        searchDefinition = regraUsuario.Select(i => new LookupLxRegraAcessoTransacao() { LxRegraAcessoTransacao = i.LxRegraAcessoTransacao, LxRegraAcessoTransacaoName = i.LxRegraAcessoTransacaoName }).AsQueryable();
                    else
                        searchDefinition = regraUsuario.Where(i=> i.LxRegraAcessoTransacaoName.ToUpper().Contains(regraAcesso)).Select(i => new LookupLxRegraAcessoTransacao() { LxRegraAcessoTransacao = i.LxRegraAcessoTransacao, LxRegraAcessoTransacaoName = i.LxRegraAcessoTransacaoName }).AsQueryable();
                }
            }
            else
            {
                //Regra Transacao Perfil
                var regraPerfil = ds.GetTcsPerfilRegraTransacaoByEntitySearchNoAssociations(serializedString).Where(i => perfis.Contains(i.IdPerfil)).Select(i => new { i.LxRegraAcessoTransacao, i.LxRegraAcessoTransacaoName }).ToList();

                if (regraPerfil.Count() > 0)
                {
                    //Se Acesso Total adiciona todas as opções
                    if (regraPerfil.Where(i => i.LxRegraAcessoTransacao == 2).Count() > 0)
                    {
                        if (regraAcesso.IsNullOrEmpty())
                            searchDefinition = regras.Select(i => new LookupLxRegraAcessoTransacao() { LxRegraAcessoTransacao = i.Key, LxRegraAcessoTransacaoName = i.Value }).AsQueryable();
                        else
                            searchDefinition = regras.Where(i => i.Value.ToUpper().Contains(regraAcesso)).Select(i => new LookupLxRegraAcessoTransacao() { LxRegraAcessoTransacao = i.Key, LxRegraAcessoTransacaoName = i.Value }).AsQueryable();
                    }
                    else
                    {
                        //Adiciona acesso bloqueado
                        if (regraPerfil.Where(i => i.LxRegraAcessoTransacao == 1).Count() == 0)
                        {
                            regraPerfil.Add(new { LxRegraAcessoTransacao = bloqueado.Key, LxRegraAcessoTransacaoName = bloqueado.Value });
                        }
                        if (regraAcesso.IsNullOrEmpty())
                            searchDefinition = regraPerfil.Select(i => new LookupLxRegraAcessoTransacao() { LxRegraAcessoTransacao = i.LxRegraAcessoTransacao, LxRegraAcessoTransacaoName = i.LxRegraAcessoTransacaoName }).AsQueryable();
                        else
                            searchDefinition = regraPerfil.Where(i=> i.LxRegraAcessoTransacaoName.ToUpper().Contains(regraAcesso)) .Select(i => new LookupLxRegraAcessoTransacao() { LxRegraAcessoTransacao = i.LxRegraAcessoTransacao, LxRegraAcessoTransacaoName = i.LxRegraAcessoTransacaoName }).AsQueryable();
                    }
                }
                else
                {
                    //Modulo Autorizacao
                    Framework.BV.TransacaoAutorizacao.TransacaoAutorizacaoDomainService dstransacaoAut = new Framework.BV.TransacaoAutorizacao.TransacaoAutorizacaoDomainService();
                    long IdModulo = dstransacaoAut.GetTcsTransacaoMenuAutorizacaoByEntitySearchNoAssociations(serializedString).Select(i => i.IdModulo).FirstOrDefault();

                    //Módulo Local
                    if (IdModulo.IsNull())
                    {
                        Framework.BV.Transacao.TransacaoDomainService dsTransacaoLoc = new Framework.BV.Transacao.TransacaoDomainService();
                        IdModulo = dsTransacaoLoc.GetTcsTransacaoMenuChildByEntitySearchNoAssociations(serializedString).Select(i => i.IdModulo).FirstOrDefault();
                    }

                    EntitySearch search = new EntitySearch();
                    search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdModulo"));
                    search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
                    search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, IdModulo));
                    //Regra de Módulo -> retira a opção Acesso por Transação
                    searchDefinition = Utils.GetRegraAcessoModulo(search).Where(i => i.RegraAcesso != 13).Select(i => new LookupLxRegraAcessoTransacao() { LxRegraAcessoTransacao = i.RegraAcesso, LxRegraAcessoTransacaoName = i.RegraAcessoName }).AsQueryable();
                }
            }
        }
    }
}
