using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linx.Tools;
using System.Data.Entity;

namespace Linx.Operacional.BM.Rules.Filial
{
    public class RepositorioFilial
    {
        private LinxOperacional contexto = null;

        public RepositorioFilial(LinxOperacional contexto)
        {
            this.contexto = contexto;
            this.contexto.Configuration.AutoDetectChangesEnabled = true;
        }

        public void Delete(TBC_PFJ pfj)
        {
            this.contexto.Entry(pfj).State = EntityState.Deleted;
        }

        public void Delete(TBC_FILIAL filial)
        {
            this.contexto.Entry(filial).State = EntityState.Deleted;
        }

        public void Delete(TBC_PFJ_ENDERECO endereco)
        {
            this.contexto.Entry(endereco).State = EntityState.Deleted;
        }
        
        public void Dispose()
        {
            if (contexto != null)
                contexto.Dispose();
        }

        public TBC_FILIAL GetFilialById(int idFilial)
        {
           return contexto.TBC_FILIAL.FirstOrDefault(p => p.ID_FILIAL_PFJ == idFilial);
        }

        public TBC_PFJ GetPFJFilial(int idPfjFilial)
        {
            try
            {
                int? idLinx = ContextEvents.GetIdLinxOperacional();

                return contexto.TBC_PFJ
                    .Include("TBC_FILIAL_LISTA")
                    .Include("TBC_FILIAL_LISTA.TBC_GRUPO_ECONOMICO")
                    .Where(e => e.ID_LINX == idLinx)
                    .FirstOrDefault(p => p.ID_PFJ == idPfjFilial);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TBC_PFJ GetPFJFilial(string cnpj)
        {
            try
            {
                // int? idLinx = ContextEvents.GetIdLinxOperacional();

                return contexto.TBC_PFJ
                    .Include("TBC_FILIAL_LISTA")
                    .Include("TBC_FILIAL_LISTA.TBC_GRUPO_ECONOMICO")
                    .Where(e => 1 == 1 && e.CNPJ_CPF == cnpj)
                    // && e.ID_LINX == idLinx)
                    .FirstOrDefault(); // p => p.CNPJ_CPF == cnpj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public TBC_PFJ GetPFJ(string cnpj = null, string codigoPfj = null)
        {
            if (cnpj != null && !String.IsNullOrEmpty(cnpj))
                return contexto.TBC_PFJ.FirstOrDefault(p => p.CNPJ_CPF == (string)cnpj);
            else if (codigoPfj != null && !String.IsNullOrEmpty(codigoPfj))
                return contexto.TBC_PFJ.FirstOrDefault(p => p.CODIGO_PFJ == (string)codigoPfj);
            else
                return null;
        }

        /// <summary>
        /// Cria, Atualiza ou Busca TbcPfj e Filial
        /// </summary>
        /// <param name="tbcPfj"></param>
        /// <returns></returns>
        public KeyValuePair<TBC_PFJ, TBC_FILIAL> ResolvePfjFilial(TBC_PFJ tbcPfj, TBC_FILIAL tbcFilial, string codigoFilialMatriz, bool validaDadosEndereco, int idlinxadmistrativo, bool? excluir)
        {

#if DEBUG
            contexto.Configuration.ProxyCreationEnabled = true;
            contexto.Configuration.LazyLoadingEnabled = true;
#endif

            TBC_PFJ pfj = null; TBC_FILIAL filial = null;

            if (excluir == null) excluir = false;
            bool bUpdate = false;

            if (!tbcPfj.CNPJ_CPF.IsNullOrEmpty())
            {
                tbcPfj.CNPJ_CPF = tbcPfj.CNPJ_CPF.Trim();
                pfj = contexto.TBC_PFJ.Where(w => w.CNPJ_CPF == tbcPfj.CNPJ_CPF).FirstOrDefault();
            }

            if ((bool)excluir)
            {
                if (pfj != null)
                {
                    try
                    {
                        if (pfj.TBC_FILIAL_LISTA != null)
                        {
                            //pfj.TBC_FILIAL_LISTA.ID_MATRIZ_CONTABIL_PFJ = null;
                            //pfj.TBC_FILIAL_LISTA.INDICA_MATRIZ_CONTABIL = false;
                            //contexto.SaveChanges();
                            this.Delete(pfj.TBC_FILIAL_LISTA);
                            contexto.SaveChanges();
                        }

                        if (pfj.TBC_PFJ_ENDERECO_LISTA != null && pfj.TBC_PFJ_ENDERECO_LISTA.Count() > 0)
                        {
                            while (pfj.TBC_PFJ_ENDERECO_LISTA.Count() > 0)
                                this.Delete(pfj.TBC_PFJ_ENDERECO_LISTA.FirstOrDefault());

                            contexto.SaveChanges();
                        }

                        this.Delete(pfj);
                        contexto.SaveChanges();
                    }
                    catch { throw new Exception("Não foi possível excluir a filial, verifique se existem movimentos! - Operacional"); }
                }
                else
                {                    
                    if(idlinxadmistrativo != ContextEvents.GetIdLinxOperacional()) // Caso contrário o registro já foi excluído no Administrativo
                        throw new Exception("Filial não encontrada para exclusão! - Operacional");
                }
            }
            else
            {
                #region pfj
                if (tbcPfj == null)
                    throw new Exception("[TBC_PFJ] não encontrado no contexto de atualização. \n ***Crítica gerada por [ResolvePfjFilial] - Operacional ***");

                if (tbcFilial == null)
                    throw new Exception("[TBC_FILIAL] não encontrada no contexto de atualização. \n ***Crítica gerada por [ResolvePfjFilial]  - Operacional ***");

                if (pfj.IsNull() && tbcPfj != null)
                {
                    //novo pfj
                    string mensagem = "";                   

                    if (tbcPfj.CNPJ_CPF.IsNullOrEmpty())
                        mensagem += "Não foi informado o CNPJ/CPF da " + TipoEndereco.Filial + "\n";

                    if (tbcPfj.NOME_FANTASIA_APELIDO.IsNullOrEmpty())
                        mensagem += "Não foi informado o Nome Fantasia da " + TipoEndereco.Filial + "\n";

                    if (tbcPfj.ID_REGIME_TRIBUTARIO == null)
                        mensagem += "Não foi informado o Regime Tributário da " + TipoEndereco.Filial + "\n";

                    if (tbcPfj.ID_INDICADOR_FISCAL_PFJ == null)
                        mensagem += "Não foi informado o Indicador Fiscal da " + TipoEndereco.Filial + "\n";

                    if (!mensagem.IsNullOrEmpty())
                        throw new Exception("Inserção da " + TipoEndereco.Filial + ": " + mensagem + " - Operacional");

                    tbcPfj.CODIGO_PFJ = tbcPfj.CODIGO_PFJ.IsNullOrEmpty() ? tbcPfj.CNPJ_CPF : tbcPfj.CODIGO_PFJ;
                    tbcPfj.RAZAO_SOCIAL_NOME_COMPLETO = tbcPfj.RAZAO_SOCIAL_NOME_COMPLETO.IsNullOrEmpty() ? tbcPfj.NOME_FANTASIA_APELIDO : tbcPfj.RAZAO_SOCIAL_NOME_COMPLETO;
                    tbcPfj.DATA_ALTERACAO = System.DateTime.Now;
                    tbcPfj.DATA_CADASTRO = System.DateTime.Now;
                    tbcPfj.LX_PFJ_FISICA_JURIDICA = tbcPfj.CNPJ_CPF.Length == 11 ?
                        Convert.ToByte(Domains.LX_PFJ_FISICA_JURIDICA.FISICA.Value) : Convert.ToByte(Domains.LX_PFJ_FISICA_JURIDICA.JURIDICA.Value);

                    if (tbcPfj.LX_PFJ_FISICA_JURIDICA == Convert.ToByte(Domains.LX_PFJ_FISICA_JURIDICA.FISICA.Value)) tbcPfj.INSCR_ESTADUAL = "ISENTO";

                    if (!String.IsNullOrEmpty(tbcPfj.CEP)) tbcPfj.CEP = tbcPfj.CEP.Replace("-", "").Trim();

                    if (!String.IsNullOrEmpty(tbcPfj.DDD_FIXO)) tbcPfj.DDD_FIXO = tbcPfj.DDD_FIXO;
                    if (!String.IsNullOrEmpty(tbcPfj.FONE_FIXO))
                        tbcPfj.FONE_FIXO = Business.Common.Util.RetiraMascaraTelefone(tbcPfj.FONE_FIXO);
                    if (!String.IsNullOrEmpty(tbcPfj.FONE_CELULAR))
                        tbcPfj.FONE_CELULAR = Business.Common.Util.RetiraMascaraTelefone(tbcPfj.FONE_CELULAR);

                    tbcPfj.INDICA_FILIAL = true;

                    contexto.TBC_PFJ.Add(tbcPfj);

                    ValidaPreenchimentoEnderecoCompletoTBC(tbcPfj, TipoEndereco.Filial, validaDadosEndereco);
                    contexto.SaveChanges();

                    pfj = tbcPfj;
                }
                else
                {
                    if ((!tbcPfj.NOME_FANTASIA_APELIDO.IsNullOrEmpty() && pfj.NOME_FANTASIA_APELIDO != tbcPfj.NOME_FANTASIA_APELIDO) ||
                       (!tbcPfj.RAZAO_SOCIAL_NOME_COMPLETO.IsNullOrEmpty() && pfj.RAZAO_SOCIAL_NOME_COMPLETO != tbcPfj.RAZAO_SOCIAL_NOME_COMPLETO) ||
                       (!tbcPfj.CODIGO_PFJ.IsNullOrEmpty() && pfj.CODIGO_PFJ != tbcPfj.CODIGO_PFJ) ||
                       (tbcPfj.LX_PFJ_FISICA_JURIDICA != null && pfj.LX_PFJ_FISICA_JURIDICA != tbcPfj.LX_PFJ_FISICA_JURIDICA) ||
                       (!String.IsNullOrEmpty(tbcPfj.CNPJ_CPF) && pfj.CNPJ_CPF != tbcPfj.CNPJ_CPF) ||
                       (!String.IsNullOrEmpty(tbcPfj.INSCR_ESTADUAL) && pfj.INSCR_ESTADUAL != tbcPfj.INSCR_ESTADUAL) ||
                       (!String.IsNullOrEmpty(tbcPfj.INSCRICAO_SUFRAMA) && pfj.INSCRICAO_SUFRAMA != tbcPfj.INSCRICAO_SUFRAMA) ||
                       (tbcPfj.ID_INDICADOR_FISCAL_PFJ != null && pfj.ID_INDICADOR_FISCAL_PFJ != tbcPfj.ID_INDICADOR_FISCAL_PFJ) ||
                       (tbcPfj.ID_REGIME_TRIBUTARIO != null && pfj.ID_REGIME_TRIBUTARIO != tbcPfj.ID_REGIME_TRIBUTARIO) ||
                       (tbcPfj.LX_TIPO_LOGRADOURO != null && pfj.LX_TIPO_LOGRADOURO != tbcPfj.LX_TIPO_LOGRADOURO) ||
                       (!tbcPfj.EMAIL.IsNullOrEmpty() && pfj.EMAIL != tbcPfj.EMAIL) ||
                       (!tbcPfj.DDI_FIXO.IsNullOrEmpty() && pfj.DDI_FIXO != tbcPfj.DDI_FIXO) ||
                       (!tbcPfj.DDD_FIXO.IsNullOrEmpty() && pfj.DDD_FIXO != tbcPfj.DDD_FIXO) ||
                       (!tbcPfj.FONE_FIXO.IsNullOrEmpty() && pfj.FONE_FIXO != Business.Common.Util.RetiraMascaraTelefone(tbcPfj.FONE_FIXO)) ||
                       (!tbcPfj.DDI_CELULAR.IsNullOrEmpty() && pfj.DDI_CELULAR != tbcPfj.DDI_CELULAR) ||
                       (!tbcPfj.DDD_CELULAR.IsNullOrEmpty() && pfj.DDD_CELULAR != tbcPfj.DDD_CELULAR) ||
                       (!tbcPfj.FONE_CELULAR.IsNullOrEmpty() && pfj.FONE_CELULAR != Business.Common.Util.RetiraMascaraTelefone(tbcPfj.FONE_CELULAR)) ||
                       (!tbcPfj.RAMAL.IsNullOrEmpty() && pfj.RAMAL != tbcPfj.RAMAL)
                       )
                    {
                        pfj.DATA_ALTERACAO = DateTime.Now;
                        bUpdate = true;

                        if (!tbcPfj.NOME_FANTASIA_APELIDO.IsNullOrEmpty()) pfj.NOME_FANTASIA_APELIDO = tbcPfj.NOME_FANTASIA_APELIDO;
                        if (!tbcPfj.RAZAO_SOCIAL_NOME_COMPLETO.IsNullOrEmpty()) pfj.RAZAO_SOCIAL_NOME_COMPLETO = tbcPfj.RAZAO_SOCIAL_NOME_COMPLETO;
                        if (!tbcPfj.CODIGO_PFJ.IsNullOrEmpty()) pfj.CODIGO_PFJ = tbcPfj.CODIGO_PFJ;
                        if (tbcPfj.LX_PFJ_FISICA_JURIDICA != null) pfj.LX_PFJ_FISICA_JURIDICA = tbcPfj.LX_PFJ_FISICA_JURIDICA;
                        if (!String.IsNullOrEmpty(tbcPfj.CNPJ_CPF)) pfj.CNPJ_CPF = tbcPfj.CNPJ_CPF;
                        if (!String.IsNullOrEmpty(tbcPfj.INSCR_ESTADUAL)) pfj.INSCR_ESTADUAL = tbcPfj.INSCR_ESTADUAL;
                        if (!String.IsNullOrEmpty(tbcPfj.INSCRICAO_SUFRAMA)) pfj.INSCRICAO_SUFRAMA = tbcPfj.INSCRICAO_SUFRAMA;
                        if (tbcPfj.ID_INDICADOR_FISCAL_PFJ != null) pfj.ID_INDICADOR_FISCAL_PFJ = tbcPfj.ID_INDICADOR_FISCAL_PFJ;
                        if (tbcPfj.ID_REGIME_TRIBUTARIO != null) pfj.ID_REGIME_TRIBUTARIO = tbcPfj.ID_REGIME_TRIBUTARIO;
                        if (tbcPfj.LX_TIPO_LOGRADOURO != null) pfj.LX_TIPO_LOGRADOURO = tbcPfj.LX_TIPO_LOGRADOURO;
                        if (!tbcPfj.EMAIL.IsNullOrEmpty()) pfj.EMAIL = tbcPfj.EMAIL;
                        if (!tbcPfj.DDI_FIXO.IsNullOrEmpty()) pfj.DDI_FIXO = tbcPfj.DDI_FIXO;
                        if (!tbcPfj.DDD_FIXO.IsNullOrEmpty()) pfj.DDD_FIXO = tbcPfj.DDD_FIXO;
                        if (!tbcPfj.FONE_FIXO.IsNullOrEmpty()) pfj.FONE_FIXO = Business.Common.Util.RetiraMascaraTelefone(tbcPfj.FONE_FIXO);
                        if (!tbcPfj.DDI_CELULAR.IsNullOrEmpty()) pfj.DDI_CELULAR = tbcPfj.DDI_CELULAR;
                        if (!tbcPfj.DDD_CELULAR.IsNullOrEmpty()) pfj.DDD_CELULAR = tbcPfj.DDD_CELULAR;
                        if (!tbcPfj.FONE_CELULAR.IsNullOrEmpty()) pfj.FONE_CELULAR = Business.Common.Util.RetiraMascaraTelefone(tbcPfj.FONE_CELULAR);
                        if (!tbcPfj.RAMAL.IsNullOrEmpty()) pfj.RAMAL = tbcPfj.RAMAL;
                    }

                    if ((!tbcPfj.LOGRADOURO.IsNullOrEmpty() && pfj.LOGRADOURO != tbcPfj.LOGRADOURO)
                        || (!tbcPfj.NUMERO.IsNullOrEmpty() && pfj.NUMERO != tbcPfj.NUMERO)
                        || (!tbcPfj.BAIRRO.IsNullOrEmpty() && pfj.BAIRRO != tbcPfj.BAIRRO)
                        || (!tbcPfj.MUNICIPIO.IsNullOrEmpty() && pfj.MUNICIPIO != tbcPfj.MUNICIPIO)
                        || (!tbcPfj.UF.IsNullOrEmpty() && pfj.UF != tbcPfj.UF)
                        || (!tbcPfj.CEP.IsNullOrEmpty() && pfj.CEP != tbcPfj.CEP.Replace("-", "").Trim())

                        )
                    {
                        pfj.DATA_ALTERACAO = DateTime.Now;
                        bUpdate = true;

                        bool enderecoMudou = (pfj.LOGRADOURO != tbcPfj.LOGRADOURO);
                        if (!tbcPfj.LOGRADOURO.IsNullOrEmpty()) pfj.LOGRADOURO = tbcPfj.LOGRADOURO;
                        if (!tbcPfj.NUMERO.IsNullOrEmpty() || enderecoMudou) pfj.NUMERO = tbcPfj.NUMERO;
                        if (!tbcPfj.COMPLEMENTO.IsNullOrEmpty() || enderecoMudou) pfj.COMPLEMENTO = tbcPfj.COMPLEMENTO;
                        if (!tbcPfj.BAIRRO.IsNullOrEmpty() || enderecoMudou) pfj.BAIRRO = tbcPfj.BAIRRO;
                        if (!tbcPfj.ID_MUNICIPIO.IsNullOrEmpty() || enderecoMudou) pfj.ID_MUNICIPIO = tbcPfj.ID_MUNICIPIO;
                        if (!tbcPfj.MUNICIPIO.IsNullOrEmpty() || enderecoMudou) pfj.MUNICIPIO = tbcPfj.MUNICIPIO;
                        if (!tbcPfj.ID_UF.IsNullOrEmpty() || enderecoMudou) pfj.ID_UF = tbcPfj.ID_UF;
                        if (!tbcPfj.UF.IsNullOrEmpty() || enderecoMudou) pfj.UF = tbcPfj.UF;
                        if (!tbcPfj.ID_CEP.IsNullOrEmpty() || enderecoMudou) pfj.ID_CEP = tbcPfj.ID_CEP;
                        if (!tbcPfj.CEP.IsNullOrEmpty() || enderecoMudou) pfj.CEP = !String.IsNullOrEmpty(tbcPfj.CEP) ? tbcPfj.CEP.Replace("-", "") : null;
                        if (!tbcPfj.ID_PAIS.IsNullOrEmpty() || enderecoMudou) pfj.ID_PAIS = tbcPfj.ID_PAIS;
                        if (!tbcPfj.OBS_ENDERECO.IsNullOrEmpty() || enderecoMudou) pfj.OBS_ENDERECO = tbcPfj.OBS_ENDERECO;
                    }

                    ValidaPreenchimentoEnderecoCompletoTBC(pfj, TipoEndereco.Terceiro, validaDadosEndereco);
                    if (bUpdate) contexto.SaveChanges();
                }
                #endregion

                #region filial
                filial = contexto.TBC_FILIAL.Where(w => w.ID_FILIAL_PFJ == pfj.ID_PFJ).FirstOrDefault();

                if (filial.IsNull() && tbcFilial != null && tbcFilial.ID_FILIAL_PFJ == 0)
                {
                    // nova filial 
                    string mensagem = "";

                    if ((bool)excluir)
                        mensagem += "Filial não foi encontrada para exclusão!";

                    if (tbcFilial.CODIGO_FILIAL.IsNullOrEmpty())
                        mensagem += "Não foi informado o Código da " + TipoEndereco.Filial + "\n";

                    if (tbcFilial.NOME_FILIAL.IsNullOrEmpty())
                        mensagem += "Não foi informado o Nome da " + TipoEndereco.Filial + "\n";

                    if (!mensagem.IsNullOrEmpty())
                        throw new Exception("Inserção da " + TipoEndereco.Filial + ": " + mensagem + " - Operacional");

                    tbcFilial.CODIGO_FILIAL = tbcFilial.CODIGO_FILIAL.IsNullOrEmpty() ? tbcPfj.CNPJ_CPF : tbcFilial.CODIGO_FILIAL;
                    tbcFilial.ID_FILIAL_PFJ = pfj.ID_PFJ;

                    if (codigoFilialMatriz == tbcFilial.CODIGO_FILIAL)
                    {
                        tbcFilial.ID_MATRIZ_CONTABIL_PFJ = pfj.ID_PFJ;
                        tbcFilial.INDICA_MATRIZ_CONTABIL = true;
                    }
                    else
                    {
                        #region Matriz Contabil
                        var matriz = this.GetPFJ(null, codigoFilialMatriz);
                        if (matriz != null)
                        {
                            tbcFilial.ID_MATRIZ_CONTABIL_PFJ = matriz.ID_PFJ;
                            tbcFilial.INDICA_MATRIZ_CONTABIL = true;
                        }
                        else
                        {
                            tbcFilial.ID_MATRIZ_CONTABIL_PFJ = null;
                            tbcFilial.INDICA_MATRIZ_CONTABIL = false;
                        }
                        #endregion
                    }

                    tbcFilial.INATIVO = false;
                    //tbcFilial.ID_GPECON = pfj.ID_LINX;

                    contexto.TBC_FILIAL.Add(tbcFilial);
                    contexto.SaveChanges();
                    filial = tbcFilial;
                }
                else
                {
                    bUpdate = false;
                    if (
                        (!tbcFilial.NOME_FILIAL.IsNullOrEmpty() && filial.NOME_FILIAL != tbcFilial.NOME_FILIAL) ||
                        (!tbcFilial.CODIGO_FILIAL.IsNullOrEmpty() && filial.CODIGO_FILIAL != tbcFilial.CODIGO_FILIAL) ||
                        (filial.ID_MATRIZ_CONTABIL_PFJ != tbcFilial.ID_MATRIZ_CONTABIL_PFJ)
                        )
                    {
                        bUpdate = true;
                        if (!tbcFilial.NOME_FILIAL.IsNullOrEmpty()) filial.NOME_FILIAL = tbcFilial.NOME_FILIAL;
                        if (!tbcFilial.CODIGO_FILIAL.IsNullOrEmpty()) filial.CODIGO_FILIAL = tbcFilial.CODIGO_FILIAL;

                        if (tbcFilial.ID_MATRIZ_CONTABIL_PFJ != null)
                        {
                            filial.ID_MATRIZ_CONTABIL_PFJ = tbcFilial.ID_MATRIZ_CONTABIL_PFJ;
                            if (filial.ID_MATRIZ_CONTABIL_PFJ == null) filial.INDICA_MATRIZ_CONTABIL = false; else filial.INDICA_MATRIZ_CONTABIL = true;
                        }
                        else if (!String.IsNullOrEmpty(codigoFilialMatriz))
                        {
                            #region Matriz Contabil
                            var matriz = this.GetPFJ(null, codigoFilialMatriz);
                            if (matriz != null)
                            {
                                filial.ID_MATRIZ_CONTABIL_PFJ = matriz.ID_PFJ;
                                filial.INDICA_MATRIZ_CONTABIL = true;
                            }
                            else
                            {
                                filial.ID_MATRIZ_CONTABIL_PFJ = null;
                                filial.INDICA_MATRIZ_CONTABIL = false;
                            }
                            #endregion
                        }

                    }

                    if (bUpdate) contexto.SaveChanges();
                }
                #endregion
            }

            return new KeyValuePair<TBC_PFJ, TBC_FILIAL>(pfj, filial);
        }

        private void ValidaPreenchimentoEnderecoCompletoTBC(TBC_PFJ pfj, TipoEndereco tipoEndereco, bool validaEndereco)
        {
            try
            {
                string mensagem = "";

                if (tipoEndereco == TipoEndereco.Filial) mensagem = "endereço da Filial";
                if (tipoEndereco == TipoEndereco.Terceiro) mensagem = "endereço do Terceiro";
                if (tipoEndereco == TipoEndereco.Fornecedor) mensagem = "endereço do Fornecedor";
                if (tipoEndereco == TipoEndereco.Transportadora) mensagem = "endereço da Transportadora";

                if (pfj.UF == "EX")
                {
                    var municipio = contexto.GEO_MUNICIPIO.Where(f => f.DESC_MUNICIPIO == "Exterior").FirstOrDefault();
                    if (municipio != null)
                    {
                        pfj.MUNICIPIO = municipio.DESC_MUNICIPIO;
                        pfj.ID_MUNICIPIO = municipio.ID_MUNICIPIO;
                    }

                    var uf = contexto.GEO_UNIDADE_FEDERACAO.Where(f => f.SIGLA_UF == pfj.UF).FirstOrDefault();
                    if (uf != null)
                        pfj.ID_UF = uf.ID_UF;

                    var pais = contexto.GEO_PAIS.Where(f => f.DESC_PAIS == pfj.PAIS).FirstOrDefault();
                    if (pais != null)
                        pfj.ID_PAIS = pais.ID_PAIS;

                    if (pfj.LOGRADOURO.IsNullOrEmpty() || pfj.NUMERO.IsNullOrEmpty() || pfj.BAIRRO.IsNullOrEmpty() || pfj.UF.IsNullOrEmpty())
                        throw new Exception("Não foi possível cadastrar/atualizar os dados do " + mensagem + ", pois os mesmos não foram preenchidos. Para UF igual a EX, é obrigatório informação de Logradouro, Número, Bairro e UF (EX).");
                }
                else
                {
                    if (pfj.LOGRADOURO.IsNullOrEmpty() || pfj.NUMERO.IsNullOrEmpty() || pfj.BAIRRO.IsNullOrEmpty() || pfj.MUNICIPIO.IsNullOrEmpty() || pfj.CEP.IsNullOrEmpty())
                        throw new Exception("Não foi possível cadastrar/atualizar os dados do " + mensagem + ", pois os mesmos não foram preenchidos.");

                    if (pfj.ID_MUNICIPIO.IsNullOrEmpty() || pfj.ID_UF.IsNullOrEmpty() || pfj.ID_PAIS.IsNullOrEmpty() || pfj.ID_CEP.IsNullOrEmpty())
                    {
                        if (!pfj.CEP.IsNullOrEmpty() && pfj.ID_CEP.IsNullOrEmpty())
                        {
                            var cep = contexto.GEO_CEP.Where(f => f.CEP == pfj.CEP).FirstOrDefault();
                            if (cep != null)
                                pfj.ID_CEP = cep.ID_CEP;
                        }
                        if (!pfj.UF.IsNullOrEmpty() && (pfj.ID_UF.IsNullOrEmpty() || pfj.ID_PAIS.IsNullOrEmpty()))
                        {
                            var uf = contexto.GEO_UNIDADE_FEDERACAO.Where(f => f.SIGLA_UF == pfj.UF).FirstOrDefault();
                            if (uf != null)
                            {
                                pfj.ID_UF = uf.ID_UF;
                                pfj.ID_PAIS = uf.ID_PAIS;
                            }
                        }
                        if (!pfj.MUNICIPIO.IsNullOrEmpty() && !pfj.ID_UF.IsNullOrEmpty() && pfj.ID_MUNICIPIO.IsNullOrEmpty())
                        {
                            var municipio = contexto.GEO_MUNICIPIO.Where(f => f.DESC_MUNICIPIO == pfj.MUNICIPIO && f.ID_UF == pfj.ID_UF).FirstOrDefault();
                            if (municipio != null)
                                pfj.ID_MUNICIPIO = municipio.ID_MUNICIPIO;
                        }

                        if (pfj.ID_MUNICIPIO.IsNullOrEmpty() || pfj.ID_UF.IsNullOrEmpty() || pfj.ID_PAIS.IsNullOrEmpty())
                            throw new Exception("O sistema não conseguiu encontrar o Município, UF e País para o " + mensagem + " informado.");
                    }
                }
            }
            catch (Exception ex)
            {
                if (validaEndereco)
                    throw new Exception(ex.Message.ToString());
            }
        }

        enum TipoEndereco
        {
            Terceiro = 1,
            Fornecedor = 2,
            Transportadora = 3,
            Filial = 4,
            Emissor = 5,
        }
    }
}
