using Linx.Business.Tools;
using Linx.Framework.BV.Domains;
using Linx.Framework.Custom.BV.PerfilFranquia;
using Linx.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Framework.Custom.BV
{
    public class Utils
    {
        public static List<AcessoModulo> GetRegraAcessoModulo(EntitySearch entitySearch)
        {
            List<AcessoModulo> acessoModulos = new List<AcessoModulo>();
            Dictionary<byte, string> regras = new Dictionary<byte, string>();
            RegraAcesso.GetValues().Where(i => i.Key != "99").Foreach(item =>
            {
                regras.Add(Convert.ToByte(item.Key), item.Value);
            });

            KeyValuePair<byte, string> bloqueado = regras.Where(i => i.Key == 1).FirstOrDefault();

            //LxRegraAcessoModuloName
            string regraAcesso = string.Empty;

            EntitySearchExpression expression = entitySearch.Expressions.Where(i => i.Name == "Field" && i.Value.ToString() == "LxRegraAcessoModuloName").FirstOrDefault();
            if (!expression.IsNull())
            {
                int fieldPos = entitySearch.Expressions.IndexOf(expression);
                regraAcesso = (entitySearch.Expressions[fieldPos + 2].Value).ToString().Replace("%", "");
                Linx.Framework.BV.Utils.RemoveExpressionFromEntitySearh(entitySearch, expression, fieldPos);
            }

            entitySearch.EntityName = string.Empty;
            string serializedString = SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch>() { entitySearch });
            Int64 idUsuario = UserServiceHelper.GetCurrentUserId().GetValueOrDefault();
            PerfilFranquiaDomainService ds = new PerfilFranquiaDomainService();
            List<Int64> perfis = ds.GetPerfilList(idUsuario);

            Linx.Framework.BV.Usuario.UsuarioDomainService dsUsuario = new Framework.BV.Usuario.UsuarioDomainService();

            //Regra Módulo Usuário
            var regraUsuario = dsUsuario.GetTcsUsuarioRegraModuloByEntitySearchNoAssociations(serializedString).Where(i => i.IdUsuario == idUsuario).Select(i => new { i.LxRegraAcessoModulo, i.LxRegraAcessoModuloName }).Distinct().ToList();

            if (regraUsuario.Count() > 0)
            {
                //Se Acesso Total adiciona todas as opções
                if (regraUsuario.Where(i => i.LxRegraAcessoModulo == 2).Count() > 0)
                {
                    acessoModulos = regras.Select(i => new AcessoModulo() { RegraAcesso = i.Key, RegraAcessoName = i.Value }).ToList();
                }
                else
                {
                    //Adiciona acesso bloqueado
                    if (regraUsuario.Where(i => i.LxRegraAcessoModulo == 1).Count() == 0)
                    {
                        regraUsuario.Add(new { LxRegraAcessoModulo = bloqueado.Key, LxRegraAcessoModuloName = bloqueado.Value });
                    }
                    acessoModulos = regraUsuario.Select(i => new AcessoModulo() { RegraAcesso = i.LxRegraAcessoModulo, RegraAcessoName = i.LxRegraAcessoModuloName }).ToList();
                }
            }
            else
            {
                //Regra Módulo Perfil
                var regraPerfil = ds.GetTcsPerfilRegraModuloByEntitySearchNoAssociations(serializedString).Where(i => perfis.Contains(i.IdPerfil)).Select(i => new { i.LxRegraAcessoModulo, i.LxRegraAcessoModuloName }).Distinct().ToList();

                if (regraPerfil.Where(i => i.LxRegraAcessoModulo == 2).Count() > 0)
                {
                    acessoModulos = regras.Select(i => new AcessoModulo() { RegraAcesso = i.Key, RegraAcessoName = i.Value }).ToList();
                }
                else
                {
                    if (regraPerfil.Where(i => i.LxRegraAcessoModulo == 1).Count() == 0)
                    {
                        regraPerfil.Add(new { LxRegraAcessoModulo = bloqueado.Key, LxRegraAcessoModuloName = bloqueado.Value });
                    }
                    acessoModulos = regraPerfil.Select(i => new AcessoModulo() { RegraAcesso = i.LxRegraAcessoModulo, RegraAcessoName = i.LxRegraAcessoModuloName }).ToList();
                }
            }

            if (regraAcesso.IsNullOrEmpty())
                return acessoModulos;
            else
                return acessoModulos.Where(i => i.RegraAcessoName.ToUpper().Contains(regraAcesso.ToUpper())).ToList();
        }
    }

    public class AcessoModulo
    {
        public byte RegraAcesso { get; set; }
        public string RegraAcessoName { get; set; }
    }

}
