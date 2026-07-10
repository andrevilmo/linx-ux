using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Linx.Tools;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.ComponentModel.Composition;
using System.Web.Http;
using Linx.Framework.BV.Configuracao;
using System.Transactions;
using System.ServiceModel.DomainServices.Server;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{

    ////////////////////////////////////////////////////////////////////////////
    /////////////////////////// Business Api Controller ////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class LinxFrameworkConfiguracaoController
    {

        [Route("CriaConfiguracaoInicial")]
        [HttpPost()]
        [LinxFrameworkConfiguracaoControllerAuthorize()]
        public void CriaConfiguracaoInicial(List<ConfiguracaoAcesso> acessos)
        {
            try
            {
                if (acessos.IsNullOrEmpty() || acessos.Count() == 0)
                {
                    return;
                }

                Empresa.EmpresaDomainService dsEmpresa = new Empresa.EmpresaDomainService();
                int idLinx = acessos[0].IdLinx;
                List<Empresa.TcsEmpresaModulo> empresaModulo = dsEmpresa.GetTcsEmpresaModuloNoAssociations().Where(i => i.IdLinx == idLinx).ToList();
                List<Int64> empresaModulos = empresaModulo.Select(i => i.IdModulo).ToList();

                if (empresaModulo.IsNull() || empresaModulo.Count() == 0)
                {
                    throw new Exception("Não foram encontrados Módulos cadastrados para a Empresa.");
                }

                TransactionOptions transactionOptions = new TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

                using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
                {
                    foreach (ConfiguracaoAcesso acesso in acessos)
                    {
                        Dictionary<string, string> headers = new Dictionary<string, string>
                        {
                            {"Environment", acesso.IdTcsAmbiente.ToString() },
                            {"EconomicGroup", acesso.UidEmpresa.ToString() },
                            {"CurrentCompany", acesso.UidEmpresa.ToString() }
                        };

                        List<ChangeSetEntry> changeSetEntries = new List<ChangeSetEntry>();
                        int entityCount = 0;

                        //Verifica existência do Usuário no banco de Aplicação.
                        Usuario.UsuarioDomainService dsUsuario = new Usuario.UsuarioDomainService(headers);
                        UsuarioAutorizacao.UsuarioAutorizacaoDomainService dsUsuarioAutorizacao = new UsuarioAutorizacao.UsuarioAutorizacaoDomainService();
                        Usuario.TcsUsuario tcsUsuario = dsUsuario.GetTcsUsuarioNoAssociations().Where(i => i.IdUsuario == acesso.IdUsuario).FirstOrDefault();

                        if (tcsUsuario.IsNullOrEmpty())
                        {
                            UsuarioAutorizacao.TcsUsuarioAutenticacao tcsUsuarioAutenticacao = dsUsuarioAutorizacao.GetTcsUsuarioAutenticacaoNoAssociations().Where(i => i.IdUsuario == acesso.IdUsuario).FirstOrDefault();
                            tcsUsuario = new Usuario.TcsUsuario()
                            {
                                IdUsuario = tcsUsuarioAutenticacao.IdUsuario,
                                UidUsuario = tcsUsuarioAutenticacao.UidUsuario,
                                NomeUsuario = tcsUsuarioAutenticacao.NomeUsuario,
                                LxPfjFisicaJuridica = tcsUsuarioAutenticacao.LxPfjFisicaJuridica,
                                CnpjCpf = tcsUsuarioAutenticacao.CnpjCpf,
                                InscrEstadualRg = tcsUsuarioAutenticacao.InscrEstadualRg,
                                LxTipoLogradouro = tcsUsuarioAutenticacao.LxTipoLogradouro,
                                Logradouro = tcsUsuarioAutenticacao.Logradouro,
                                Numero = tcsUsuarioAutenticacao.Numero,
                                Complemento = tcsUsuarioAutenticacao.Complemento,
                                Bairro = tcsUsuarioAutenticacao.Bairro,
                                Municipio = tcsUsuarioAutenticacao.Municipio,
                                Uf = tcsUsuarioAutenticacao.Uf,
                                Cep = tcsUsuarioAutenticacao.Cep,
                                ObsEndereco = tcsUsuarioAutenticacao.ObsEndereco,
                                Email = tcsUsuarioAutenticacao.Email,
                                FoneCelular = tcsUsuarioAutenticacao.FoneCelular,
                                FoneFixo = tcsUsuarioAutenticacao.FoneFixo,
                                Ramal = tcsUsuarioAutenticacao.Ramal,
                                DataCadastro = tcsUsuarioAutenticacao.DataCadastro,
                                DataAlteracao = tcsUsuarioAutenticacao.DataAlteracao,
                                IdLinx = tcsUsuarioAutenticacao.IdLinx
                            };
                            changeSetEntries.Add(new ChangeSetEntry(entityCount, tcsUsuario, null, DomainOperation.Insert));
                            dsUsuario.SaveEntities(changeSetEntries);

                            changeSetEntries = new List<ChangeSetEntry>();
                            entityCount = 0;
                        }

                        headers.Add("CurrentUser", tcsUsuario.UidUsuario.ToString());

                        //Módulo Grupo
                        string descGrupoModulo = String.Format("Grupo Módulo - Aplicativo {0}", acesso.IdTcsAplicativo);
                        Modulo.ModuloDomainService dsModulo = new Modulo.ModuloDomainService(headers);
                        Modulo.TcsModuloGrupo moduloGrupo = dsModulo.GetTcsModuloGrupoNoAssociations().Where(i => i.IdTcsAplicativo == acesso.IdTcsAplicativo && i.DescGrupoModulo == descGrupoModulo).FirstOrDefault();

                        if (moduloGrupo.IsNullOrEmpty())
                        {
                            moduloGrupo = new Modulo.TcsModuloGrupo() { DescGrupoModulo = descGrupoModulo, IdTcsAplicativo = acesso.IdTcsAplicativo };
                            changeSetEntries.Add(new ChangeSetEntry(entityCount, moduloGrupo, null, DomainOperation.Insert));
                        }
                        else
                        {
                            changeSetEntries.Add(new ChangeSetEntry(entityCount, moduloGrupo, null, DomainOperation.Update) { HasMemberChanges = false });
                        }
                        entityCount++;

                        //Módulo do Grupo
                        List<Int64> modulosDoGrupo = dsModulo.GetTcsModuloDoGrupoNoAssociations().Where(i => i.IdGrupoModulo == moduloGrupo.IdGrupoModulo).Select(i => i.IdModulo).ToList();
                        List<long> modulos = empresaModulo.Where(i => i.IdTcsAplicativo == acesso.IdTcsAplicativo && !modulosDoGrupo.Contains(i.IdModulo)).Select(i => i.IdModulo).Distinct().ToList();
                        foreach (long idModulo in modulos)
                        {
                            changeSetEntries.Add(new ChangeSetEntry(entityCount, new Modulo.TcsModuloDoGrupoDetalhe() { IdGrupoModulo = moduloGrupo.IdGrupoModulo, IdModulo = idModulo }, null, DomainOperation.Insert));
                            entityCount++;
                        }

                        dsModulo.SaveEntities(changeSetEntries);
                        changeSetEntries = new List<ChangeSetEntry>();
                        entityCount = 0;


                        //Grupo Módulo
                        ParametroAutorizacao.ParametroAutorizacaoDomainService dsParametroAutorizacao = new ParametroAutorizacao.ParametroAutorizacaoDomainService();
                        long idParametroGrupoModulo = dsParametroAutorizacao.GetTcsParametroAutorizacaoNoAssociations().Where(i => i.TituloParametro == "GRUPO_MODULO" && i.IdTcsAplicativo == acesso.IdTcsAplicativo).Select(i => i.IdParametro).FirstOrDefault();

                        Parametro.ParametroDomainService dsParametro = new Parametro.ParametroDomainService(headers);
                        Parametro.TcsParametroValorP valorGrupoModulo = dsParametro.GetTcsParametroValorPNoAssociations().Where(i => i.IdParametro == idParametroGrupoModulo).FirstOrDefault();

                        if (valorGrupoModulo.IsNullOrEmpty())
                        {
                            valorGrupoModulo = new Parametro.TcsParametroValorP() { IdParametro = idParametroGrupoModulo, ValorParametro = moduloGrupo.IdGrupoModulo.ToString() };
                            changeSetEntries.Add(new ChangeSetEntry(entityCount, valorGrupoModulo, null, DomainOperation.Insert));
                        }
                        else
                        {
                            Parametro.TcsParametroValorP valorOld = new Parametro.TcsParametroValorP();
                            valorOld.CopyFrom(valorGrupoModulo);
                            valorGrupoModulo.ValorParametro = moduloGrupo.IdGrupoModulo.ToString();
                            changeSetEntries.Add(new ChangeSetEntry(entityCount, valorGrupoModulo, valorOld, DomainOperation.Update));
                        }
                        entityCount++;

                        //Nível Acesso Parâmetro
                        long idParametroNivelAcesso = dsParametroAutorizacao.GetTcsParametroAutorizacaoNoAssociations().Where(i => i.TituloParametro == "NIVEL_ACESSO_PARAMETRO" && i.IdTcsAplicativo == 1).Select(i => i.IdParametro).FirstOrDefault();
                        string value = dsParametro.GetTcsParametroValorNoAssociations().Where(i => i.IdParametro == idParametroNivelAcesso).Select(i => i.ValorParametro).FirstOrDefault();

                        if (value.IsNullOrEmpty())
                        {
                            changeSetEntries.Add(new ChangeSetEntry(entityCount, new Parametro.TcsParametroValorP() { IdParametro = idParametroNivelAcesso, ValorParametro = "0" }, null, DomainOperation.Insert));
                            entityCount++;
                        }

                        if (entityCount > 0)
                        {
                            dsParametro.SaveEntities(changeSetEntries);
                            changeSetEntries = new List<ChangeSetEntry>();
                            entityCount = 0;
                        }

                        string descPerfil = "Perfil Automático - Acesso Total";

                        //Perfil
                        Perfil.PerfilDomainService dsPerfil = new Perfil.PerfilDomainService(headers);
                        Perfil.TcsPerfil tcsPerfil = dsPerfil.GetTcsPerfilNoAssociations().Where(i => i.DescPerfil == descPerfil).FirstOrDefault();

                        if (tcsPerfil.IsNullOrEmpty())
                        {
                            tcsPerfil = new Perfil.TcsPerfil() { DescPerfil = descPerfil };

                            changeSetEntries.Add(new ChangeSetEntry(entityCount, tcsPerfil, null, DomainOperation.Insert));
                        }
                        else
                        {
                            changeSetEntries.Add(new ChangeSetEntry(entityCount, tcsPerfil, null, DomainOperation.Update) { HasMemberChanges = false });
                        }
                        entityCount++;

                        List<Int64> regraModulos = dsPerfil.GetTcsPerfilRegraModuloNoAssociations().Where(i => i.IdPerfil == tcsPerfil.IdPerfil).Select(i => i.IdModulo).ToList();
                        List<Int64> regraModulosAdd = empresaModulos.Where(i => !regraModulos.Contains(i)).ToList();

                        foreach (Int64 modulo in regraModulosAdd)
                        {
                            changeSetEntries.Add(new ChangeSetEntry(entityCount, new Perfil.TcsPerfilRegraModulo() { IdPerfil = tcsPerfil.IdPerfil, IdModulo = modulo, LxRegraAcessoModulo = 2 }, null, DomainOperation.Insert));
                            entityCount++;
                        }

                        dsPerfil.SaveEntities(changeSetEntries);
                        changeSetEntries = new List<ChangeSetEntry>();
                        entityCount = 0;


                        //Usuário Perfil
                        Perfil.TcsUsuarioPerfil usuarioPerfil = dsPerfil.GetTcsUsuarioPerfilNoAssociations().Where(i => i.IdPerfil == tcsPerfil.IdPerfil && i.IdUsuario == tcsUsuario.IdUsuario).FirstOrDefault();
                        if (usuarioPerfil.IsNullOrEmpty())
                        {
                            changeSetEntries.Add(new ChangeSetEntry(entityCount, tcsPerfil, null, DomainOperation.Update) { HasMemberChanges = false });
                            entityCount++;
                            changeSetEntries.Add(new ChangeSetEntry(entityCount, new Perfil.TcsUsuarioPerfil { IdPerfil = tcsPerfil.IdPerfil, IdUsuario = tcsUsuario.IdUsuario }, null, DomainOperation.Insert));
                            dsPerfil.SaveEntities(changeSetEntries);
                            changeSetEntries = new List<ChangeSetEntry>();
                            entityCount = 0;
                        }
                    }
                    transaction.Complete();
                }
            }
            catch (Exception oException)
            {
                throw new Exception(oException.Message);
            }
        }
    }
}
