using Linx.Financeiro.BM.Contracts;
using Linx.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Operacional.BM.Rules.Loja
{
    public class RegraLoja
    {
        private RepositorioLoja repositorioLoja = null;

        public RegraLoja()
        {
            this.repositorioLoja = new RepositorioLoja(new LinxOperacional());
        }

        public List<LojaRetorno> IntegraLoja(List<BM.Loja> lLojas)
        {
            LojaRetorno retorno = null;
            List<LojaRetorno> lretorno = new List<LojaRetorno>();
            StringBuilder sbMensagemRetorno = null;

            foreach (var loja in lLojas)
            {
                sbMensagemRetorno = new StringBuilder();

                LJV_LOJA ljv = new LJV_LOJA()
                {
                    COD_LOJA = loja.CodLoja,
                    DESC_LOJA = loja.DescLoja,
                    INDICA_FRANQUIA = (loja.IndicaFranquia != null ? (bool)loja.IndicaFranquia : false),
                    INDICA_ECOMMERCE = (loja.IndicaECommerce != null ? (bool)loja.IndicaECommerce : false),
                    INDICA_SOMENTE_CRM = (loja.IndicaSomenteCRM != null ? (bool)loja.IndicaSomenteCRM : false),
                };

                if (loja.IdLoja > 0) ljv.ID_LOJA = loja.IdLoja;

                try
                {
                    var ljvFil = this.repositorioLoja.ResolveLoja(ljv, loja);

                    if (ljvFil != null && ljvFil.ID_LOJA > 0)
                    {
                        retorno = new LojaRetorno()
                        {
                            IdentificadorExterno = loja.IdentificadorExterno,
                            Erro = false,
                            IdLojaRetorno = ljvFil.ID_LOJA,
                        };
                    }
                    else
                    {
                        retorno = new LojaRetorno()
                        {
                            IdentificadorExterno = loja.IdentificadorExterno,
                            Erro = true,
                            Mensagem = "Não foi possível integrar a loja."
                        };
                    }
                }
                catch (Exception err)
                {
                    retorno = new LojaRetorno()
                    {
                        IdentificadorExterno = loja.IdentificadorExterno,
                        Erro = true,
                        Mensagem = err.Message
                    };
                }

                lretorno.Add(retorno);
            }

            return lretorno;
        }

        public void AtualizaStatusConferenciaControle(int idControleConferencia, byte statusConferencia)
        {
            var conf = repositorioLoja.GetConferencia(idControleConferencia);
            if (conf != null)
            {
                conf.LX_STATUS_CONFERENCIA = statusConferencia;
                repositorioLoja.Alter(conf);
                repositorioLoja.SaveChanges();
            }
        }

        public void AtualizaStatusConferenciaAtendimentoPgto(Int64 idAtendimentoPgto, byte statusConferencia)
        {
            var pgto = repositorioLoja.GetAtendimentoPagamento(idAtendimentoPgto);
            if (pgto != null)
            {
                pgto.LX_STATUS_CONFERENCIA = statusConferencia;
                repositorioLoja.Alter(pgto);
                repositorioLoja.SaveChanges();
            }
            else
                throw new Exceptions.BusinessModelException("Pagamento não encontrado");

        }

        public void AtualizaStatusConferenciaAtendimentoPgto(List<Int64> lstIdAtendimentoPgto, byte statusConferencia)
        {

            foreach (var idAtendimentoPgto in lstIdAtendimentoPgto)
            {
                var pgto = repositorioLoja.GetAtendimentoPagamento(idAtendimentoPgto);
                if (pgto != null)
                {
                    pgto.LX_STATUS_CONFERENCIA = statusConferencia;
                    repositorioLoja.Alter(pgto);
                }
                else
                    throw new Exceptions.BusinessModelException("Pagamento não encontrado");
            }

            repositorioLoja.SaveChanges();

        }

        public void AtualizaStatusConferenciaAtendimentoItem(Int64 idAtendimentoItem, byte statusConferencia)
        {
            var atendimentoItem = repositorioLoja.GetAtendimentoItem(idAtendimentoItem);
            if (atendimentoItem != null)
            {
                atendimentoItem.LX_STATUS_CONFERENCIA = statusConferencia;
                repositorioLoja.Alter(atendimentoItem);
                repositorioLoja.SaveChanges();
            }
            else
                throw new Exceptions.BusinessModelException("Item do atendimento não encontrado");
        }

        public void AtualizaStatusConferenciaAtendimentoItem(List<Int64> lstIdAtendimentoItem, byte statusConferencia)
        {
            foreach (var idAtendimentoItem in lstIdAtendimentoItem)
            {

                var atendimentoItem = repositorioLoja.GetAtendimentoItem(idAtendimentoItem);
                if (atendimentoItem != null)
                {
                    atendimentoItem.LX_STATUS_CONFERENCIA = statusConferencia;
                    repositorioLoja.Alter(atendimentoItem);

                }
                else
                    throw new Exceptions.BusinessModelException("Item do atendimento não encontrado");

            }

            repositorioLoja.SaveChanges();
        }

        public void AtualizaStatusConferenciaLancamento(Int64 idLancamento, byte statusConferencia)
        {
            var lancamento = repositorioLoja.GetLancamento(idLancamento);
            if (lancamento != null)
            {
                lancamento.LX_STATUS_CONFERENCIA = statusConferencia;
                repositorioLoja.Alter(lancamento);
                repositorioLoja.SaveChanges();
            }
            else
                throw new Exceptions.BusinessModelException("Lançamento não encontrado");
        }

        public void AtualizaStatusConferenciaLancamento(List<Int64> lstIdLancamento, byte statusConferencia)
        {
            foreach (var idLancamento in lstIdLancamento)
            {
                var lancamento = repositorioLoja.GetLancamento(idLancamento);
                if (lancamento != null)
                {
                    lancamento.LX_STATUS_CONFERENCIA = statusConferencia;
                    repositorioLoja.Alter(lancamento);
                }
                else
                    throw new Exceptions.BusinessModelException("Lançamento não encontrado");
            }

            repositorioLoja.SaveChanges();
        }

        public void AtualizaStatusConferencia(int idConferencia, byte statusConferencia, bool fechamentoMovimento, string obsConferencia, string obsIntegracao)
        {
            //Busco a conferencia

            LJV_CTRL_CONFERENCIA conferencia = repositorioLoja.GetConferencia(idConferencia);

            if (conferencia != null)
            {
                if (statusConferencia == Convert.ToByte(Domains.LX_STATUS_CONFERENCIA_CTRL.Conferido.Value))
                {
                    //Verifico se é venda ou caixa
                    if (conferencia.LX_TIPO_CTRL_TIPO_PGTO == Convert.ToByte(Domains.LX_TIPO_CTRL_TIPO_PGTO.Venda.Value))
                    {
                        if (conferencia.LX_ID_TIPO_PGTO == 68)
                        {
                            var itens = repositorioLoja.GetAtendimentoItens(conferencia.ID_CONTROLE, conferencia.ID_CAIXA_CTRL, conferencia.ID_TERMINAL);

                            if (itens.Where(w => w.LX_STATUS_CONFERENCIA != 4).Any())
                                conferencia.LX_STATUS_CONFERENCIA = Convert.ToByte(Domains.LX_STATUS_CONFERENCIA_CTRL.Divergencia.Value);
                            else
                                conferencia.LX_STATUS_CONFERENCIA = Convert.ToByte(Domains.LX_STATUS_CONFERENCIA_CTRL.Integrado.Value);
                        }
                        else
                        {
                            var pagamentos = repositorioLoja.GetAtendimentoPgtos(conferencia.ID_CONTROLE, (byte)conferencia.LX_ID_TIPO_PGTO, conferencia.ID_CAIXA_CTRL, conferencia.ID_TERMINAL, conferencia.ID_ADM_CARTAO);

                            if (pagamentos.Where(w => w.LX_STATUS_CONFERENCIA != 4).Any())
                                conferencia.LX_STATUS_CONFERENCIA = Convert.ToByte(Domains.LX_STATUS_CONFERENCIA_CTRL.Divergencia.Value);
                            else
                                conferencia.LX_STATUS_CONFERENCIA = Convert.ToByte(Domains.LX_STATUS_CONFERENCIA_CTRL.Integrado.Value);
                        }
                    }
                    else
                    {
                        var lancamentos = repositorioLoja.GetCaixaLancamentos(conferencia.ID_CONTROLE, conferencia.ID_CAIXA_CTRL, conferencia.ID_TERMINAL);

                        if (lancamentos.Where(w => w.LX_STATUS_CONFERENCIA != 4).Any())
                            conferencia.LX_STATUS_CONFERENCIA = Convert.ToByte(Domains.LX_STATUS_CONFERENCIA_CTRL.Divergencia.Value);
                        else
                            conferencia.LX_STATUS_CONFERENCIA = Convert.ToByte(Domains.LX_STATUS_CONFERENCIA_CTRL.Integrado.Value);
                    }
                }
                else
                {
                    if (fechamentoMovimento == false && statusConferencia == Convert.ToByte(Domains.LX_STATUS_CONFERENCIA_CTRL.NaoAplicaNaoConferido.Value))
                    {
                        if (conferencia.LX_TIPO_CTRL_TIPO_PGTO == Convert.ToByte(Domains.LX_TIPO_CTRL_TIPO_PGTO.Venda.Value))
                        {
                            if (conferencia.LX_ID_TIPO_PGTO == 68)
                            {
                                var itens = repositorioLoja.GetAtendimentoItens(conferencia.ID_CONTROLE, conferencia.ID_CAIXA_CTRL, conferencia.ID_TERMINAL);

                                if (itens.Where(w => w.LX_STATUS_CONFERENCIA == 4).Any())
                                    conferencia.LX_STATUS_CONFERENCIA = Convert.ToByte(Domains.LX_STATUS_CONFERENCIA_CTRL.Divergencia.Value);
                                else
                                    conferencia.LX_STATUS_CONFERENCIA = Convert.ToByte(Domains.LX_STATUS_CONFERENCIA_CTRL.NaoAplicaNaoConferido.Value);
                            }
                            else
                            {
                                var pagamentos = repositorioLoja.GetAtendimentoPgtos(conferencia.ID_CONTROLE, (byte)conferencia.LX_ID_TIPO_PGTO, conferencia.ID_CAIXA_CTRL, conferencia.ID_TERMINAL, conferencia.ID_ADM_CARTAO);

                                if (pagamentos.Where(w => w.LX_STATUS_CONFERENCIA == 4).Any())
                                    conferencia.LX_STATUS_CONFERENCIA = Convert.ToByte(Domains.LX_STATUS_CONFERENCIA_CTRL.Divergencia.Value);
                                else
                                    conferencia.LX_STATUS_CONFERENCIA = Convert.ToByte(Domains.LX_STATUS_CONFERENCIA_CTRL.NaoAplicaNaoConferido.Value);
                            }
                        }
                        else
                        {
                            var lancamentos = repositorioLoja.GetCaixaLancamentos(conferencia.ID_CONTROLE, conferencia.ID_CAIXA_CTRL, conferencia.ID_TERMINAL);

                            if (lancamentos.Where(w => w.LX_STATUS_CONFERENCIA == 4).Any())
                                conferencia.LX_STATUS_CONFERENCIA = Convert.ToByte(Domains.LX_STATUS_CONFERENCIA_CTRL.Divergencia.Value);
                            else
                                conferencia.LX_STATUS_CONFERENCIA = Convert.ToByte(Domains.LX_STATUS_CONFERENCIA_CTRL.NaoAplicaNaoConferido.Value);
                        }
                    }
                    else
                        conferencia.LX_STATUS_CONFERENCIA = statusConferencia;
                }

                conferencia.OBS_CONFERENCIA = obsConferencia;

                conferencia.OBS_INTEGRACAO = obsIntegracao;

                repositorioLoja.Alter(conferencia);
                repositorioLoja.SaveChanges();
            }
        }

        public void AtualizaStatusControle(List<Int64> lstIdControle, byte statusOperacao)
        {
            byte vendaEncerrada = Convert.ToByte(Domains.LX_STATUS_OPERACAO.VendaEncerrada.Value);
            byte movimentoIntegrado = Convert.ToByte(Domains.LX_STATUS_OPERACAO.MovimentoIntegrado.Value);

            List<LJV_CTRL> lstCtrl = new List<LJV_CTRL>();
            lstCtrl = repositorioLoja.GetControles(lstIdControle);

            if (statusOperacao == vendaEncerrada)
            {
                if (lstCtrl.Any())
                {
                    foreach (var ctrl in lstCtrl)
                    {
                        ctrl.LX_STATUS_OPERACAO = vendaEncerrada;

                        repositorioLoja.Alter(ctrl);
                    }
                }

                repositorioLoja.SaveChanges();
            }
            else
            {
                if (lstCtrl.Any())
                {
                    foreach (var ctrl in lstCtrl)
                    {
                        if (repositorioLoja.ExisteConferenciaPendente(ctrl.ID_CONTROLE))
                            ctrl.LX_STATUS_OPERACAO = vendaEncerrada;
                        else
                            ctrl.LX_STATUS_OPERACAO = movimentoIntegrado;

                        repositorioLoja.Alter(ctrl);

                    }

                    repositorioLoja.SaveChanges();
                }
            }
        }

        public void AtualizarAdministradoraControle(List<ConferenciaAdministradora> lstConferenciaAdministradora)
        {
            List<LJV_CTRL_CONFERENCIA> lstConfererencias = new List<LJV_CTRL_CONFERENCIA>();
            List<byte> lstTipoPgtoTEF = new List<byte>() { 7, 9, 22, 23 };

            //Busco as conferências
            lstConfererencias = repositorioLoja.GetConferencias(lstConferenciaAdministradora.Select(s => (int)s.IdCtrlConferencia).Distinct().ToList());

            //Percorro as conferências para atualizar os dados
            foreach (var conferencia in lstConfererencias)
            {
                if (lstConferenciaAdministradora.Where(w => w.IdCtrlConferencia == conferencia.ID_CTRL_CONFERENCIA).Any() && conferencia.ID_ADM_CARTAO == null && lstTipoPgtoTEF.Contains((byte)conferencia.LX_ID_TIPO_PGTO))
                {
                    var conferenciaAdm = lstConferenciaAdministradora.Where(w => w.IdCtrlConferencia == conferencia.ID_CTRL_CONFERENCIA).FirstOrDefault();

                    conferencia.ID_ADM_CARTAO = (short?)conferenciaAdm.IdAdmCartao;

                    //Busco os pagamentos para atualização da administradora

                    List<LJV_ATENDIMENTO_PGTO> lstAtendimentoPgto = repositorioLoja.GetAtendimentoPgtos(conferencia.ID_CONTROLE, (byte)conferencia.LX_ID_TIPO_PGTO, conferencia.ID_CAIXA_CTRL, conferencia.ID_TERMINAL, null);

                    foreach (var atendimentoPgto in lstAtendimentoPgto.Where(w => w.ID_BANDEIRA_TEF.Trim() == conferencia.ID_COD_TEF_BANDEIRA.Trim() && w.LX_COD_TEF_REDE.Trim() == conferencia.LX_COD_TEF_REDE.Trim()))
                    {
                        atendimentoPgto.ID_ADM_CARTAO = conferencia.ID_ADM_CARTAO;
                        repositorioLoja.Alter(atendimentoPgto);
                    }

                    repositorioLoja.Alter(conferencia);
                }
            }

            repositorioLoja.SaveChanges();
        }

        #region Conferência automatica de loja

        public class ConferenciaAutomatica
        {
            public IEnumerable<AtendimentoItemConferencia> AtendimentoItem_LISTA { get; set; }
            public IEnumerable<AtendimentoPgtoConferencia> Atendimento_LISTA { get; set; }
            public DateTime? DATA_MOV { get; set; }
            public short? ID_ADM_CARTAO { get; set; }
            public long ID_CONTROLE { get; set; }
            public int ID_CTRL_CONFERENCIA { get; set; }
            public int ID_LINX_CONF { get; set; }
            public int? ID_LOJA { get; set; }
            public IEnumerable<LancamentoCaixaConferencia> LancamentoCaixa_LISTA { get; set; }
            public byte? LX_ID_TIPO_PGTO { get; set; }
            public byte LxStatusConferencia { get; set; }
            public string ObsIntegracao { get; set; }
            public string ObsConferencia { get; set; }
        }

        public class AtendimentoPgtoConferencia
        {
            public long IdAtendimentoPgto { get; set; }
            public int IdCtrlConferencia { get; set; }
            public byte LxStatusConferencia { get; set; }
            public bool Cancelado { get; set; }
        }

        public class AtendimentoItemConferencia
        {
            public long IdAtendimentoItem { get; set; }
            public int IdCtrlConferencia { get; set; }
            public byte LxStatusConferencia { get; set; }
            public bool Cancelado { get; set; }
        }

        public class LancamentoCaixaConferencia
        {
            public long IdLancamentoCaixa { get; set; }
            public int IdCtrlConferencia { get; set; }
            public byte LxStatusConferencia { get; set; }
        }

        /// <summary>
        /// Método que irá efetuar as conferência de loja com o financeiro sem utilização da tela de conferência
        /// </summary>
        /// <param name="IdLoja">Id da Loja</param>
        public void IntegracaoAutomaticaFinanceiraLoja(int IdLoja)
        {
            List<Conferencia> lstConferencia = new List<Conferencia>();
            List<ConferenciaAutomatica> lstConferenciaAutomatica = new List<ConferenciaAutomatica>();

            //Busco os pagamentos para conferência
            lstConferenciaAutomatica = repositorioLoja.GetAtendimentosPgtoConferir(IdLoja);

            //Incluo as movimentação de caixa
            lstConferenciaAutomatica.AddRange(repositorioLoja.GetMovimentosCaixaConferir(IdLoja));

            //Converto a entidade para envio das conferências ao financeiro

            if (lstConferenciaAutomatica.Any())
            {
                foreach (var conferenciaAutomatica in lstConferenciaAutomatica)
                {
                    Conferencia conferencia = new Conferencia()
                    {
                        ID_LINX_CONF = conferenciaAutomatica.ID_LINX_CONF,
                        ID_CTRL_CONFERENCIA = conferenciaAutomatica.ID_CTRL_CONFERENCIA,
                        ID_CONTROLE = conferenciaAutomatica.ID_CONTROLE,
                        LX_ID_TIPO_PGTO = conferenciaAutomatica.LX_ID_TIPO_PGTO,
                        DATA_MOV = conferenciaAutomatica.DATA_MOV,
                        ID_LOJA = conferenciaAutomatica.ID_LOJA,
                        ID_ADM_CARTAO = null,
                        AtendimentoItem_LISTA = conferenciaAutomatica.AtendimentoItem_LISTA.Where(w => w.Cancelado == false).Select(s => new AtendimentoItem { ID_AtendimentoItem = s.IdAtendimentoItem, ID_CTRL_CONFERENCIA = s.IdCtrlConferencia }).ToList(),
                        Atendimento_LISTA = conferenciaAutomatica.Atendimento_LISTA.Where(w => w.Cancelado == false).Select(s => new Linx.Financeiro.BM.Contracts.Atendimento { ID_Atendimento = s.IdAtendimentoPgto, ID_CTRL_CONFERENCIA = s.IdCtrlConferencia }).ToList(),
                        LancamentoCaixa_LISTA = conferenciaAutomatica.LancamentoCaixa_LISTA.Select(s => new LancamentoCaixa { ID_LancamentoCaixa = s.IdLancamentoCaixa, ID_CTRL_CONFERENCIA = s.IdCtrlConferencia }).ToList(),
                    };

                    lstConferencia.Add(conferencia);
                }

                //Atualizo as conferências

                var implementacoes = ImplementationHelper<IFinanceiro>.GetInstance("ImplementacoesFinanceiro");

                foreach (var conferencia in lstConferencia)
                {

                    //Preencho as listas com os pagamento, itens e lançamentos de caixa com divergência

                    List<Int64> lstIdAtendimentoPgtoDivergencia = new List<Int64>();
                    List<Int64> lstIdAtendimentoItemDivergencia = new List<Int64>();

                    List<Int64> lstIdCaixaLancamentoDivergencia = new List<Int64>();
                    List<Int64> lstIdAtendimentoPgtoCancelado = new List<Int64>();
                    List<Int64> lstIdAtendimentoItemCancelado = new List<Int64>();

                    try
                    {
                        implementacoes.IntegracaoFinanceiraLoja(conferencia);
                    }
                    catch (Exception ex)
                    {
                        byte divergencia = Convert.ToByte(Domains.LX_STATUS_CONFERENCIA_CTRL.Divergencia.Value);

                        string mensagemErro = string.Empty;

                        if (ex.InnerException != null)
                            mensagemErro += ex.InnerException.Message;
                        else
                            mensagemErro += ex.Message;

                        foreach (var conferenciaAutomatica in lstConferenciaAutomatica.Where(w => w.ID_CTRL_CONFERENCIA == conferencia.ID_CTRL_CONFERENCIA))
                        {
                            conferenciaAutomatica.LxStatusConferencia = divergencia;
                            conferenciaAutomatica.ObsIntegracao = mensagemErro;

                            if (conferenciaAutomatica.AtendimentoItem_LISTA.Any())
                            {
                                foreach (var item in conferenciaAutomatica.AtendimentoItem_LISTA)
                                {
                                    item.LxStatusConferencia = divergencia;
                                }
                            }

                            if (conferenciaAutomatica.Atendimento_LISTA.Any())
                            {
                                foreach (var pgto in conferenciaAutomatica.Atendimento_LISTA)
                                {
                                    pgto.LxStatusConferencia = divergencia;
                                }
                            }

                            if (conferenciaAutomatica.LancamentoCaixa_LISTA.Any())
                            {
                                foreach (var caixaLancamento in conferenciaAutomatica.LancamentoCaixa_LISTA)
                                {
                                    caixaLancamento.LxStatusConferencia = divergencia;
                                }
                            }
                        }
                    }

                    if (lstConferenciaAutomatica.Where(w => w.ID_CTRL_CONFERENCIA == conferencia.ID_CTRL_CONFERENCIA).FirstOrDefault().ObsIntegracao == null)
                    {
                        byte conferido = Convert.ToByte(Domains.LX_STATUS_CONFERENCIA_CTRL.Conferido.Value);

                        foreach (var conferenciaAutomatica in lstConferenciaAutomatica.Where(w => w.ID_CTRL_CONFERENCIA == conferencia.ID_CTRL_CONFERENCIA))
                        {
                            conferenciaAutomatica.LxStatusConferencia = conferido;
                            conferenciaAutomatica.ObsConferencia = null;
                            conferenciaAutomatica.ObsIntegracao = null;

                            if (conferenciaAutomatica.AtendimentoItem_LISTA.Any())
                            {
                                foreach (var item in conferenciaAutomatica.AtendimentoItem_LISTA)
                                {
                                    item.LxStatusConferencia = conferido;
                                }
                            }

                            if (conferenciaAutomatica.Atendimento_LISTA.Any())
                            {
                                foreach (var pgto in conferenciaAutomatica.Atendimento_LISTA)
                                {
                                    pgto.LxStatusConferencia = conferido;
                                }
                            }

                            if (conferenciaAutomatica.LancamentoCaixa_LISTA.Any())
                            {
                                foreach (var caixaLancamento in conferenciaAutomatica.LancamentoCaixa_LISTA)
                                {
                                    caixaLancamento.LxStatusConferencia = conferido;
                                }
                            }
                        }
                    }

                    //Atualizo as administradoras das conferências

                    try
                    {
                        List<byte> lstTipoPgtoTEF = new List<byte>() { 7, 9, 22, 23 };

                        List<ConferenciaAdministradora> lstConferenciaAdministradora = lstConferencia.Where(w => lstTipoPgtoTEF.Contains((byte)w.LX_ID_TIPO_PGTO) && w.ID_ADM_CARTAO != null)
                            .Select(s => new ConferenciaAdministradora
                            {
                                IdCtrlConferencia = s.ID_CTRL_CONFERENCIA,
                                IdAdmCartao = s.ID_ADM_CARTAO
                            }).ToList();

                        this.AtualizarAdministradoraControle(lstConferenciaAdministradora);
                    }
                    catch { };

                    //Tratamento para divergências e cancelamentos

                    foreach (var conferenciaAutomatica in lstConferenciaAutomatica)
                    {
                        //Divergências
                        lstIdAtendimentoItemDivergencia.AddRange(conferenciaAutomatica.AtendimentoItem_LISTA.Where(w => w.LxStatusConferencia == 3).Select(s => s.IdAtendimentoItem).ToList());
                        lstIdAtendimentoPgtoDivergencia.AddRange(conferenciaAutomatica.Atendimento_LISTA.Where(w => w.LxStatusConferencia == 3).Select(s => s.IdAtendimentoPgto).ToList());
                        lstIdCaixaLancamentoDivergencia.AddRange(conferenciaAutomatica.LancamentoCaixa_LISTA.Where(w => w.LxStatusConferencia == 3).Select(s => s.IdLancamentoCaixa).ToList());

                        //Cancelamentos

                        lstIdAtendimentoItemCancelado.AddRange(conferenciaAutomatica.AtendimentoItem_LISTA.Where(w => w.Cancelado == true).Select(s => s.IdAtendimentoItem).ToList());
                        lstIdAtendimentoPgtoCancelado.AddRange(conferenciaAutomatica.Atendimento_LISTA.Where(w => w.Cancelado == true).Select(s => s.IdAtendimentoPgto).ToList());
                    }

                    //Atualizo os pagamentos, itens e lançamento caixa com divergência

                    try
                    {
                        if (lstIdAtendimentoItemDivergencia.Any())
                        {
                            this.AtualizaStatusConferenciaAtendimentoItem(lstIdAtendimentoItemDivergencia.Distinct().ToList(), Convert.ToByte(Domains.LX_STATUS_CONFERENCIA_CTRL.Divergencia.Value));
                        }

                        if (lstIdAtendimentoPgtoDivergencia.Any())
                        {
                            this.AtualizaStatusConferenciaAtendimentoPgto(lstIdAtendimentoPgtoDivergencia.Distinct().ToList(), Convert.ToByte(Domains.LX_STATUS_CONFERENCIA_CTRL.Divergencia.Value));
                        }
                    }
                    catch
                    { }

                    try
                    {
                        if (lstIdCaixaLancamentoDivergencia.Any())
                        {
                            this.AtualizaStatusConferenciaLancamento(lstIdCaixaLancamentoDivergencia.Distinct().ToList(), Convert.ToByte(Domains.LX_STATUS_CONFERENCIA_CTRL.Divergencia.Value));
                        }
                    }
                    catch
                    { }

                    //Atualizo os pagamentos e itens cancelados para status de integrado

                    try
                    {
                        if (lstIdAtendimentoItemCancelado.Any())
                        {
                            this.AtualizaStatusConferenciaAtendimentoItem(lstIdAtendimentoItemCancelado.Distinct().ToList(), Convert.ToByte(Domains.LX_STATUS_CONFERENCIA_CTRL.Integrado.Value));
                        }

                        if (lstIdAtendimentoPgtoCancelado.Any())
                        {
                            this.AtualizaStatusConferenciaAtendimentoPgto(lstIdAtendimentoPgtoCancelado.Distinct().ToList(), Convert.ToByte(Domains.LX_STATUS_CONFERENCIA_CTRL.Integrado.Value));
                        }
                    }
                    catch
                    { }

                    try
                    {
                        foreach (var conferenciaAutomatica in lstConferenciaAutomatica)
                        {
                            this.AtualizaStatusConferencia(conferenciaAutomatica.ID_CTRL_CONFERENCIA, conferenciaAutomatica.LxStatusConferencia, true, conferenciaAutomatica.ObsConferencia, conferenciaAutomatica.ObsIntegracao);
                        }
                    }
                    catch
                    { }

                    //Atualizo status do controle

                    try
                    {
                        byte movimentoIntegrado = Convert.ToByte(Domains.LX_STATUS_OPERACAO.MovimentoIntegrado.Value);

                        this.AtualizaStatusControle(lstConferencia.Select(s => s.ID_CONTROLE).Distinct().ToList(), movimentoIntegrado);
                    }
                    catch
                    { }

                }
            }
        }

        #endregion
    }
}
