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
using Linx.Framework.Setup.LinxAutoSetup;
using System.Transactions;
using System.ServiceModel.DomainServices.Server;

namespace Linx.Framework.Setup.WebAPI.DS.Controllers
{

    ////////////////////////////////////////////////////////////////////////////
    /////////////////////////// Business Api Controller ////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class LinxFrameworkLinxAutoSetupController
    {
        [Route("CriaAmbiente")]
        [HttpPost()]
        public bool CriaAmbiente(AmbienteInfo info)
        {
            try
            {
                //Valor dos Parâmetros
                string strIdTcsAmbienteOperacional = Linx.Business.Tools.LinxParameters.GetParameter<string>("ID_AMBIENTE_OPERACIONAL_LINXAUTO", null);
                string strIdTcsAmbienteAdministrativo = Linx.Business.Tools.LinxParameters.GetParameter<string>("ID_AMBIENTE_ADMINISTRATIVO_LINXAUTO", null);
                string strIdPerfilOperacional = Linx.Business.Tools.LinxParameters.GetParameter<string>("ID_PERFIL_OPERACIONAL_LINXAUTO", null);
                string strIdPerfilAdministrativo = Linx.Business.Tools.LinxParameters.GetParameter<string>("ID_PERFIL_ADMINISTRATIVO_LINXAUTO", null);

                //Verifica se encontrou valor para os Parâmetros
                if (strIdTcsAmbienteOperacional.IsNullOrEmpty() || strIdTcsAmbienteAdministrativo.IsNullOrEmpty() || strIdPerfilOperacional.IsNullOrEmpty() || strIdPerfilAdministrativo.IsNullOrEmpty())
                {
                    throw new Exception("Não foi encontrado valor para um dos seguintes Parâmetros 'ID_AMBIENTE_OPERACIONAL_LINXAUTO / ID_AMBIENTE_ADMINISTRATIVO_LINXAUTO / ID_PERFIL_OPERACIONAL_LINXAUTO / ID_PERFIL_ADMINISTRATIVO_LINXAUTO'.");
                }

                int idTcsAmbienteOperacional = Convert.ToInt16(strIdTcsAmbienteOperacional);
                int idTcsAmbienteAdministrativo = Convert.ToInt16(strIdTcsAmbienteAdministrativo);
                Int64 idPerfilOperacional = Convert.ToInt64(strIdPerfilOperacional);
                Int64 idPerfilAdministrativo = Convert.ToInt64(strIdPerfilAdministrativo);

                LinxAutoSetupDomainService ds = new LinxAutoSetupDomainService();

                TcsAmbiente ambienteOperacionalInfo = ds.GetTcsAmbienteNoAssociations().Where(i => i.IdTcsAmbiente == idTcsAmbienteOperacional).FirstOrDefault();
                TcsAmbiente ambienteAdministrativoInfo = ds.GetTcsAmbienteNoAssociations().Where(i => i.IdTcsAmbiente == idTcsAmbienteAdministrativo).FirstOrDefault();
                TcsPerfil perfilOperacionalInfo = ds.GetTcsPerfilNoAssociations().Where(i => i.IdPerfil == idPerfilOperacional).FirstOrDefault();
                TcsPerfil perfilAdministrativoInfo = ds.GetTcsPerfilNoAssociations().Where(i => i.IdPerfil == idPerfilAdministrativo).FirstOrDefault();

                //Verifica se valor dos Parâmetros está correto
                if (ambienteOperacionalInfo.IsNull() || ambienteAdministrativoInfo.IsNull() || perfilOperacionalInfo.IsNull() || perfilAdministrativoInfo.IsNull())
                {
                    throw new Exception("Parâmetro(s) com valor inconsistente. Verifique o valor dos Parâmetros 'ID_AMBIENTE_OPERACIONAL_LINXAUTO / ID_AMBIENTE_ADMINISTRATIVO_LINXAUTO / ID_PERFIL_OPERACIONAL_LINXAUTO / ID_PERFIL_ADMINISTRATIVO_LINXAUTO'.");
                }

                TransactionOptions transactionOptions = new TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

                using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
                {
                    List<ChangeSetEntry> changeSetEntries = new List<ChangeSetEntry>();
                    int entityCount = 0;

                    //Grupo Econômico
                    TcsEmpresaGpecon gpecon = ds.GetTcsEmpresaGpeconNoAssociations().Where(i => i.IdLinx == ambienteOperacionalInfo.IdLinx && i.IdLinxGpecon == info.IdLinx).FirstOrDefault();

                    //Empresa
                    TcsEmpresaAutenticacao empresa = ds.GetTcsEmpresaAutenticacaoNoAssociations().Where(i => i.IdLinx == info.IdLinx).FirstOrDefault();

                    if (gpecon.IsNull())
                    {
                        gpecon = new TcsEmpresaGpecon() { IdLinx = ambienteOperacionalInfo.IdLinx, IdLinxGpecon = info.IdLinx };
                        changeSetEntries.Add(new ChangeSetEntry(entityCount, gpecon, null, DomainOperation.Insert));
                        entityCount++;

                        if (empresa.IsNull())
                        {
                            empresa = new TcsEmpresaAutenticacao() { IdLinx = info.IdLinx, NomeEmpresa = info.RazaoSocial, UidEmpresa = Guid.NewGuid(), CnpjCpf = info.Cnpj };
                            changeSetEntries.Add(new ChangeSetEntry(entityCount, empresa, null, DomainOperation.Insert));
                        }
                        else
                        {
                            changeSetEntries.Add(new ChangeSetEntry(entityCount, empresa, null, DomainOperation.Update) { HasMemberChanges = false });
                        }

                        entityCount++;

                        //Modulo Empresa
                        List<TcsPerfilRegraModulo> modulosOrigem = ds.GetTcsPerfilRegraModuloNoAssociations().Where(i => i.IdPerfil == perfilOperacionalInfo.IdPerfil || i.IdPerfil == perfilAdministrativoInfo.IdPerfil).ToList();
                        List<Int64> modulosEmp = ds.GetTcsEmpresaAutenticacaoModuloNoAssociations().Where(i => i.IdLinx == info.IdLinx).Select(i => i.IdModulo).ToList();
                        List<Int64> empresaModulo = modulosOrigem.Where(i => !modulosEmp.Contains(i.IdModulo)).Select(i => i.IdModulo).Distinct().ToList();

                        foreach (Int64 modulo in empresaModulo)
                        {
                            changeSetEntries.Add(new ChangeSetEntry(entityCount, new TcsEmpresaAutenticacaoModulo() { IdModulo = modulo, IdLinx = info.IdLinx }, null, DomainOperation.Insert));
                            entityCount++;
                        }
                    }

                    //Usuário
                    TcsUsuarioAutenticacao usuario = ds.GetTcsUsuarioAutenticacaoNoAssociations().Where(i => i.NomeAutenticacao == info.NomeAutenticacao).FirstOrDefault();

                    if (!usuario.IsNull())
                    {
                        throw new Exception(string.Format("Já existe um usuário cadastrado com esse Login '{0}'.", info.NomeAutenticacao));
                    }
                    else
                    {
                        string nomeCurtoUsuario = (info.NomeUsuario.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))[0];
                        usuario = new TcsUsuarioAutenticacao()
                        {
                            IdLinx = info.IdLinx,
                            UidUsuario = Guid.NewGuid(),
                            NomeUsuario = info.NomeUsuario,
                            NomeCurtoUsuario = nomeCurtoUsuario,
                            NomeAutenticacao = info.NomeAutenticacao,
                            GeraSenhaUsuario = false,
                            //CriaUsuario = true,
                            ConfirmacaoUsuario = info.Senha,
                            ConfirmacaoUsuario1 = info.Senha,
                            Email = info.Email,
                            VigenciaFinal = DateTime.Now.AddYears(10),
                            DataExpiracaoSenha = DateTime.Now.AddDays(30),
                            AutenticacaoWindows = false
                        };
                        changeSetEntries.Add(new ChangeSetEntry(entityCount, usuario, null, DomainOperation.Insert));
                        entityCount++;

                        //Acesso usuário ambiente Operacional
                        TcsUsuarioAutenticacaoAcesso acessoOperacional = new TcsUsuarioAutenticacaoAcesso() { IdTcsAmbiente = ambienteOperacionalInfo.IdTcsAmbiente, IndicaAcessoPadrao = true, IndicaAdministrador = false, IndicaMultiGpecon = false };
                        changeSetEntries.Add(new ChangeSetEntry(entityCount, acessoOperacional, null, DomainOperation.Insert));
                        entityCount++;

                        ds.SaveEntities(changeSetEntries);

                        changeSetEntries = new List<ChangeSetEntry>();
                        entityCount = 0;

                        //Headers para IdLinx LinxAuto
                        TcsAmbienteInfo ambienteInfo = ds.GetTcsAmbienteInfoNoAssociations().Where(i => i.IdLinx == ambienteOperacionalInfo.IdLinx).FirstOrDefault();
                        Dictionary<string, string> headers = new Dictionary<string, string>
                        {
                            {"Environment", ambienteOperacionalInfo.IdTcsAmbiente.ToString() },
                            {"EconomicGroup", ambienteOperacionalInfo.UidEmpresa.ToString() },
                            {"CurrentCompany", ambienteOperacionalInfo.UidEmpresa.ToString() },
                        };

                        LinxAutoSetupDomainService ds1 = new LinxAutoSetupDomainService(headers);

                        //Perfil Operacional
                        TcsUsuarioPerfil perfilOperacional = new TcsUsuarioPerfil() { IdPerfil = perfilOperacionalInfo.IdPerfil, IdUsuario = usuario.IdUsuario };
                        changeSetEntries.Add(new ChangeSetEntry(entityCount, perfilOperacional, null, DomainOperation.Insert));
                        entityCount++;

                        ds1.SaveEntities(changeSetEntries);

                        //Criação Ambiente Administrativo
                        TcsAmbiente ambienteAdministrativo = CriaAmbienteAdministrativo(info, empresa, ambienteOperacionalInfo, ambienteAdministrativoInfo, perfilAdministrativoInfo, usuario);

                        //Headers para IdLinx Nova Empresa
                        headers = new Dictionary<string, string>
                            {
                                {"Environment", ambienteAdministrativo.IdTcsAmbiente.ToString() },
                                {"EconomicGroup", empresa.UidEmpresa.ToString() },
                                {"CurrentCompany", empresa.UidEmpresa.ToString() },
                            };

                        LinxAutoSetupDomainService ds2 = new LinxAutoSetupDomainService(headers);
                        changeSetEntries = new List<ChangeSetEntry>();
                        entityCount = 0;

                        //Atualiza ambiente Relacionado
                        TcsUsuarioAutenticacao usuarioU = new TcsUsuarioAutenticacao();
                        usuarioU.CopyInstanceFrom(usuario);
                        TcsUsuarioAutenticacaoAcesso acessoOperacionalU = new TcsUsuarioAutenticacaoAcesso();
                        acessoOperacionalU.CopyInstanceFrom(acessoOperacional);
                        acessoOperacionalU.IdTcsAmbienteRelacionado = ambienteAdministrativo.IdTcsAmbiente;
                        changeSetEntries.Add(new ChangeSetEntry(entityCount, usuarioU, usuarioU, DomainOperation.Update) { HasMemberChanges = false });
                        entityCount++;
                        changeSetEntries.Add(new ChangeSetEntry(entityCount, acessoOperacionalU, acessoOperacional, DomainOperation.Update) { HasMemberChanges = true });
                        entityCount++;

                        ds2.SaveEntities(changeSetEntries);
                    }

                    transaction.Complete();
                }
            }
            catch (Exception oException)
            {
                throw new Exception(oException.Message);
            }
            return true;
        }

        private TcsAmbiente CriaAmbienteAdministrativo(AmbienteInfo info, TcsEmpresaAutenticacao empresaAutenticacao, TcsAmbiente ambienteOperacionalInfo, TcsAmbiente ambienteAdministrativoInfo, TcsPerfil perfilAdministrativoInfo, TcsUsuarioAutenticacao usuarioAutenticacao)
        {
            List<ChangeSetEntry> changeSetEntries = new List<ChangeSetEntry>();
            int entityCount = 0;
            bool hasEnvironment = false;

            //Headers para IdLinx LinxAuto
            LinxAutoSetupDomainService ds = new LinxAutoSetupDomainService();
            TcsAmbienteInfo ambienteInfo = ds.GetTcsAmbienteInfoNoAssociations().Where(i => i.IdLinx == ambienteOperacionalInfo.IdLinx).FirstOrDefault();
            Dictionary<string, string> headers = new Dictionary<string, string>();
            //        {
            //            {"Environment", ambienteOperacionalInfo.IdTcsAmbiente.ToString() },
            //            {"EconomicGroup", ambienteOperacionalInfo.UidEmpresa.ToString() },
            //            {"CurrentCompany", ambienteOperacionalInfo.UidEmpresa.ToString() },
            //        };

            //LinxAutoSetupDomainService ds1 = new LinxAutoSetupDomainService(headers);

            //Ambiente Administrativo

            TcsAmbiente ambienteAdministrativo = ds.GetTcsAmbienteNoAssociations().Where(i => i.IdAplicacao == ambienteAdministrativoInfo.IdAplicacao && i.IdLinx == info.IdLinx).FirstOrDefault();

            if (ambienteAdministrativo.IsNull())
            {
                ambienteAdministrativo = new TcsAmbiente() { DescricaoAmbiente = "Administrativo Linx Auto - " + info.RazaoSocial, IdAplicacao = ambienteAdministrativoInfo.IdAplicacao, IdLinx = info.IdLinx, UidEmpresa = empresaAutenticacao.UidEmpresa };
                changeSetEntries.Add(new ChangeSetEntry(entityCount, ambienteAdministrativo, null, DomainOperation.Insert));
                entityCount++;

                //Conexões Administrativo
                List<TcsAmbienteConexao> ambienteAdmConexao = ds.GetTcsAmbienteConexaoNoAssociations().Where(i => i.IdTcsAmbiente == ambienteAdministrativoInfo.IdTcsAmbiente).ToList();
                foreach (TcsAmbienteConexao conexao in ambienteAdmConexao)
                {
                    changeSetEntries.Add(new ChangeSetEntry(entityCount, new TcsAmbienteConexao() { IdTcsAplicativoConexao = conexao.IdTcsAplicativoConexao, IdTcsBancoServidor = conexao.IdTcsBancoServidor }, null, DomainOperation.Insert));
                    entityCount++;

                }
            }
            else
            {
                changeSetEntries.Add(new ChangeSetEntry(entityCount, ambienteAdministrativo, null, DomainOperation.Update) { HasMemberChanges = false });
                entityCount++;
                hasEnvironment = true;
            }

            //UsarioAcesso
            TcsAmbienteUsuarioAcesso acessoAdministrativo = ds.GetTcsAmbienteUsuarioAcessoNoAssociations().Where(i => i.IdTcsAmbiente == ambienteAdministrativo.IdTcsAmbiente && i.IdUsuario == usuarioAutenticacao.IdUsuario).FirstOrDefault();

            if (acessoAdministrativo.IsNull())
            {

                //Acesso Administrativo
                acessoAdministrativo = new TcsAmbienteUsuarioAcesso()
                {
                    IdUsuario = usuarioAutenticacao.IdUsuario,
                    IndicaAdministrador = true,
                    IndicaMultiGpecon = true,
                    NomeUsuario = usuarioAutenticacao.NomeUsuario,
                    NomeAutenticacao = usuarioAutenticacao.NomeAutenticacao,
                    UidUsuario = usuarioAutenticacao.UidUsuario,
                    IdTcsAmbiente = ambienteAdministrativo.IdTcsAmbiente
                };

                changeSetEntries.Add(new ChangeSetEntry(entityCount, acessoAdministrativo, null, DomainOperation.Insert));
                entityCount++;
            }

            ds.SaveEntities(changeSetEntries);

            //Headers para IdLinx Nova Empresa
            headers = new Dictionary<string, string>
                    {
                        {"Environment", ambienteAdministrativo.IdTcsAmbiente.ToString() },
                        {"EconomicGroup", empresaAutenticacao.UidEmpresa.ToString() },
                        {"CurrentCompany", empresaAutenticacao.UidEmpresa.ToString() },
                    };

            LinxAutoSetupDomainService ds2 = new LinxAutoSetupDomainService(headers);
            changeSetEntries = new List<ChangeSetEntry>();
            entityCount = 0;

            string descPerfil = "Perfil Automático Administrativo - Id Linx : " + info.IdLinx;
            TcsPerfil perfilAdministrativo;

            if (!hasEnvironment)
            {

                //Grupo Módulo Administrativo
                TcsModuloGrupo moduloGrupo = new TcsModuloGrupo() { DescGrupoModulo = "Administrativo - Id Linx : " + info.IdLinx, IdTcsAplicativo = 2 };
                changeSetEntries.Add(new ChangeSetEntry(entityCount, moduloGrupo, null, DomainOperation.Insert));
                entityCount++;

                List<TcsPerfilRegraModulo> perfilRegraAdministrativo = ds.GetTcsPerfilRegraModuloNoAssociations().Where(i => i.IdPerfil == perfilAdministrativoInfo.IdPerfil).ToList();
                List<Int64> modulosDoGrupo = perfilRegraAdministrativo.Select(i => i.IdModulo).Distinct().ToList();

                //Módulos do Grupo
                foreach (Int64 modulo in modulosDoGrupo)
                {
                    changeSetEntries.Add(new ChangeSetEntry(entityCount, new TcsModuloGrupoDetalhe() { IdModulo = modulo }, null, DomainOperation.Insert));
                    entityCount++;
                }

                //Perfil Ambiente Administrativo
                perfilAdministrativo = new TcsPerfil() { DescPerfil = descPerfil };
                changeSetEntries.Add(new ChangeSetEntry(entityCount, perfilAdministrativo, null, DomainOperation.Insert));
                entityCount++;

                //Perfil Regra Módulo
                foreach (TcsPerfilRegraModulo regraModulo in perfilRegraAdministrativo)
                {
                    changeSetEntries.Add(new ChangeSetEntry(entityCount, new TcsPerfilRegraModulo() { IdModulo = regraModulo.IdModulo, LxRegraAcessoModulo = regraModulo.LxRegraAcessoModulo }, null, DomainOperation.Insert));
                    entityCount++;
                }

                ds2.SaveEntities(changeSetEntries);

                changeSetEntries = new List<ChangeSetEntry>();
                entityCount = 0;

                //Valor Parâmetro Grupo Módulo
                Int64 idParametro = ds.GetTcsParametroAutorizacaoNoAssociations().Where(i => i.TituloParametro == "GRUPO_MODULO" && i.IdTcsAplicativo == 2).Select(i => i.IdParametro).FirstOrDefault();
                changeSetEntries.Add(new ChangeSetEntry(entityCount, new TcsParametroValor() { IdParametro = idParametro, ValorParametro = moduloGrupo.IdGrupoModulo.ToString() }, null, DomainOperation.Insert));
                entityCount++;

                //Valor Parâmetro NIVEL_ACESSO_PARAMETRO
                Int64 idParametroNivel = ds.GetTcsParametroAutorizacaoNoAssociations().Where(i => i.TituloParametro == "NIVEL_ACESSO_PARAMETRO" && i.IdTcsAplicativo == 1).Select(i => i.IdParametro).FirstOrDefault();
                changeSetEntries.Add(new ChangeSetEntry(entityCount, new TcsParametroValor() { IdParametro = idParametroNivel, ValorParametro = "0" }, null, DomainOperation.Insert));
                entityCount++;

                //Valor Parâmetro MOEDA_PADRAO_FINANCEIRO
                Int64 idParametroMoeda = ds.GetTcsParametroAutorizacaoNoAssociations().Where(i => i.TituloParametro == "MOEDA_PADRAO_FINANCEIRO" && i.IdTcsAplicativo == 2).Select(i => i.IdParametro).FirstOrDefault();
                changeSetEntries.Add(new ChangeSetEntry(entityCount, new TcsParametroValor() { IdParametro = idParametroMoeda, ValorParametro = "1" }, null, DomainOperation.Insert));
                entityCount++;
            }
            else
            {
                perfilAdministrativo = ds2.GetTcsPerfilNoAssociations().Where(i => i.DescPerfil == descPerfil).FirstOrDefault();
                changeSetEntries.Add(new ChangeSetEntry(entityCount, perfilAdministrativo, null, DomainOperation.Update) { HasMemberChanges = false });
                entityCount++;
            }

            //Usuário Perfil
            changeSetEntries.Add(new ChangeSetEntry(entityCount, new TcsUsuarioPerfil() { IdUsuario = usuarioAutenticacao.IdUsuario, IdPerfil = perfilAdministrativo.IdPerfil }, null, DomainOperation.Insert));
            entityCount++;

            ds2.SaveEntities(changeSetEntries);

            return ambienteAdministrativo;
        }


        [Route("CriaMultimarca")]
        [HttpPost()]
        public bool CriaMultimarca(MultimarcaInfo info)
        {
            try
            {
                //Valor dos Parâmetros
                string strBandeiraRede = Linx.Business.Tools.LinxParameters.GetParameter<string>("ID_BANDEIRA_REDE_PADRAO_LINXAUTO", null);
                string strCanalVenda = Linx.Business.Tools.LinxParameters.GetParameter<string>("ID_CANAL_VENDA_PADRAO_LINXAUTO", null);

                //Verifica se encontrou valor para os Parâmetros
                if (strBandeiraRede.IsNullOrEmpty() || strCanalVenda.IsNullOrEmpty())
                {
                    throw new Exception("Não foi encontrado valor para um dos seguintes Parâmetros 'ID_BANDEIRA_REDE_PADRAO_LINXAUTO / ID_CANAL_VENDA_PADRAO_LINXAUTO'.");
                }

                int idBandeiraRede = Convert.ToInt16(strBandeiraRede);
                int idCanalVenda = Convert.ToInt16(strCanalVenda);

                LinxAutoSetupDomainService ds = new LinxAutoSetupDomainService() { IsSecure = true };

                TbcBandeiraRede bandeiraRede = ds.GetTbcBandeiraRedeNoAssociations().Where(i => i.IdBandeiraRedeCadastro == idBandeiraRede).FirstOrDefault();
                LjvCanalVenda canalVenda = ds.GetLjvCanalVendaNoAssociations().Where(i => i.IdLjvCanalVenda == idCanalVenda).FirstOrDefault();


                //Verifica se valor dos Parâmetros está correto
                if (bandeiraRede.IsNull() || canalVenda.IsNull())
                {
                    throw new Exception("Parâmetro(s) com valor inconsistente. Verifique o valor dos Parâmetros 'ID_BANDEIRA_REDE_PADRAO_LINXAUTO / ID_CANAL_VENDA_PADRAO_LINXAUTO'.");
                }

                TransactionOptions transactionOptions = new TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

                using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
                {
                    List<ChangeSetEntry> changeSetEntries = new List<ChangeSetEntry>();
                    int entityCount = 0;
                    string descGrupo = info.RazaoSocial.Length > 60 ? info.RazaoSocial.Left(60) : info.RazaoSocial;

                    //Grupo Econômico
                    TbcGrupoEconomico gpecon = ds.GetTbcGrupoEconomicoNoAssociations().Where(i => i.IdGpeconCadastro == info.IdLinx).FirstOrDefault();
                    if (gpecon.IsNull())
                    {
                        changeSetEntries.Add(new ChangeSetEntry(entityCount, new TbcGrupoEconomico() { IdGpeconCadastro = info.IdLinx, DescGrupoEconomico = descGrupo }, null, DomainOperation.Insert));
                        ds.SaveEntities(changeSetEntries);
                    }

                    entityCount = 0;
                    changeSetEntries = new List<ChangeSetEntry>();

                    var query = ds.GetTbcFilialNoAssociations().Where(i => i.CnpjCpf == info.Cnpj).Select(i => i.IdFilialPfj).ToList();

                    if (query.Count() > 0)
                    {
                        throw new Exception("Filial já cadastrada.");
                    }

                    //Filial
                    TbcFilial filial = new TbcFilial()
                    {
                        Bairro = info.Bairro,
                        BandeiraRede = idBandeiraRede,
                        Cep = info.Cep,
                        CnpjCpf = info.Cnpj,
                        CodDeposito = "000001",
                        CodigoFilial = info.Cnpj,
                        CodigoPfj = info.Cnpj,
                        Complemento = info.Complemento,
                        DddCelular = info.DddCelular,
                        DddFixo = info.DddFixo,
                        Email = info.Email,
                        FoneCelular = info.FoneCelular,
                        FoneFixo = info.FoneFixo,
                        IdGpecon = info.IdLinx,
                        IdLjvCanalVenda = idCanalVenda,
                        IncluiDeposito = true,
                        IncluiLoja = true,
                        IndicaEstrangeiro = false,
                        IndicaFilial = true,
                        InscrEstadual = info.InscrEstadual,
                        Logradouro = info.Logradouro,
                        LxPfjFisicaJuridica = 2,
                        LxTipoLogradouro = info.LxTipoLogradouro,
                        Municipio = info.Municipio,
                        NomeFantasiaApelido = descGrupo,
                        NomeFilial = info.RazaoSocial,
                        Numero = info.Numero,
                        ObsEndereco = info.ObsEndereco,
                        Pais = info.Pais.IsNullOrEmpty() ? "Brasil" : info.Pais,
                        RazaoSocialNomeCompleto = info.RazaoSocial,
                        Uf = info.Uf,
                        IndicaMatrizContabil = true
                    };

                    changeSetEntries.Add(new ChangeSetEntry(entityCount, filial, null, DomainOperation.Insert));
                    ds.SaveEntities(changeSetEntries);
                    transaction.Complete();
                }
            }
            catch (Exception oException)
            {
                throw new Exception(oException.Message);
            }
            return true;
        }

        [Route("IncluiMultimarcaLinxAuto")]
        [HttpPost()]
        public bool IncluiMultimarcaLinxAuto(MultimarcaInfo info)
        {
            TransactionOptions transactionOptions = new TransactionOptions();
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
            {
                CriaAmbiente(new AmbienteInfo() { IdLinx = info.IdLinx, RazaoSocial = info.RazaoSocial, NomeUsuario = info.NomeUsuario, NomeAutenticacao = info.NomeAutenticacao, Email = info.Email, Senha = info.Senha, Cnpj = info.Cnpj });

                CriaMultimarca(info);

                transaction.Complete();
            }

            return true;
        }

        [Route("VerificaExistenciaFilial")]
        [HttpGet()]
        public bool VerificaExistenciaFilial(string cnpj)
        {
            LinxAutoSetupDomainService ds = new LinxAutoSetupDomainService() { IsSecure = true };
            var info = ds.GetTbcFilialNoAssociations().Where(i => i.CnpjCpf == cnpj).Select(i => i.IdFilialPfj).ToList();
            return info.Count() > 0;
        }
    }
}
