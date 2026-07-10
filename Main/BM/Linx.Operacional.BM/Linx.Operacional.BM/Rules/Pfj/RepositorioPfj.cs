using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linx.Tools;
using System.Data.Entity;

namespace Linx.Operacional.BM.Rules.Pfj
{
    public class RepositorioPfj
    {
        private LinxOperacional contexto = null;

        public RepositorioPfj(LinxOperacional contexto)
        {
            this.contexto = contexto;
        }

        public TBC_PFJ GetPfj(string cpfCnpj)
        {
            return contexto.TBC_PFJ
                .Where(w => w.CNPJ_CPF == cpfCnpj)
                .ToList()
                .FirstOrDefault();
        }

        public TBC_PFJ GetPfj(int idPfj)
        {
            return contexto.TBC_PFJ
                .Where(w => w.ID_PFJ == idPfj)
                .ToList()
                .FirstOrDefault();
        }

        private string GetHashEndereco(TBC_PFJ_ENDERECO pfjendereco)
        {
            StringBuilder sbHash = new StringBuilder();
            if(pfjendereco != null)
            {
                sbHash.Append(pfjendereco.LX_TIPO_ENDERECO.ToString()
                    + (!String.IsNullOrEmpty(pfjendereco.NOME_ENDERECO) ? pfjendereco.NOME_ENDERECO : String.Empty)
                    + (!String.IsNullOrEmpty(pfjendereco.CNPJ_CPF_COLETA_ENTREGA) ? pfjendereco.CNPJ_CPF_COLETA_ENTREGA : String.Empty)
                    + (!String.IsNullOrEmpty(pfjendereco.INSCR_ESTADUAL) ? pfjendereco.INSCR_ESTADUAL : String.Empty)
                    + (!String.IsNullOrEmpty(pfjendereco.INSCR_MUNICIPAL) ? pfjendereco.INSCR_MUNICIPAL : String.Empty)
                    + (pfjendereco.LX_TIPO_LOGRADOURO != null ? pfjendereco.LX_TIPO_LOGRADOURO.ToString() : String.Empty)
                    + (!String.IsNullOrEmpty(pfjendereco.LOGRADOURO) ? pfjendereco.LOGRADOURO : String.Empty)
                    + (!String.IsNullOrEmpty(pfjendereco.NUMERO) ? pfjendereco.NUMERO : String.Empty)
                    + (!String.IsNullOrEmpty(pfjendereco.COMPLEMENTO) ? pfjendereco.COMPLEMENTO : String.Empty)
                    + (!String.IsNullOrEmpty(pfjendereco.BAIRRO) ? pfjendereco.BAIRRO : String.Empty)
                    + (pfjendereco.ID_MUNICIPIO != null ? pfjendereco.ID_MUNICIPIO.ToString() : String.Empty)
                    + (!String.IsNullOrEmpty(pfjendereco.MUNICIPIO) ? pfjendereco.MUNICIPIO : String.Empty)
                    + (pfjendereco.ID_UF != null ? pfjendereco.ID_UF.ToString() : String.Empty)
                    + (!String.IsNullOrEmpty(pfjendereco.UF) ? pfjendereco.UF : String.Empty)
                    + (pfjendereco.ID_CEP != null ? pfjendereco.ID_CEP.ToString() : String.Empty)
                    + (!String.IsNullOrEmpty(pfjendereco.CEP) ? pfjendereco.CEP : String.Empty)
                    + (pfjendereco.ID_PAIS != null ? pfjendereco.ID_PAIS.ToString() : String.Empty)
                    + (!String.IsNullOrEmpty(pfjendereco.PAIS) ? pfjendereco.PAIS : String.Empty)
                    + (!String.IsNullOrEmpty(pfjendereco.OBS_ENDERECO) ? pfjendereco.OBS_ENDERECO : String.Empty)                                        
                    );
            }

            return sbHash.ToString().Trim(); 
        }

        public TBC_PFJ_ENDERECO ResolvePfjEnderecoOperacional(TBC_PFJ_ENDERECO pfjendereco)
        {
#if DEBUG
            contexto.Configuration.ProxyCreationEnabled = true;
            contexto.Configuration.LazyLoadingEnabled = true;
#endif

            if (pfjendereco == null)
                throw new Exception("[TBC_PFJ_ENDERECO] não encontrada no contexto de atualização. \n ***Crítica gerada por [ResolvePfjEndereco]***");

            TBC_PFJ_ENDERECO endereco = null;

            if (pfjendereco.ID_PFJ_ENDERECO > 0)
            {
                endereco = contexto.TBC_PFJ_ENDERECO.Include("TBC_PFJ_ENDERECO.TBC_PFJ")
                                       .Where(w => (endereco.ID_PFJ_ENDERECO == pfjendereco.ID_PFJ_ENDERECO)).FirstOrDefault();
            }            
            else  //buscar endereço pelo hash             
            {
                string hash = this.GetHashEndereco(pfjendereco);

                endereco = contexto.TBC_PFJ_ENDERECO.Include("TBC_PFJ")
                                        .Where(w => (w.LX_TIPO_ENDERECO.ToString()
                                                                + (!String.IsNullOrEmpty(w.NOME_ENDERECO) ? w.NOME_ENDERECO : String.Empty)
                                                                + (!String.IsNullOrEmpty(w.CNPJ_CPF_COLETA_ENTREGA) ? w.CNPJ_CPF_COLETA_ENTREGA : String.Empty)
                                                                + (!String.IsNullOrEmpty(w.INSCR_ESTADUAL) ? w.INSCR_ESTADUAL : String.Empty)
                                                                + (!String.IsNullOrEmpty(w.INSCR_MUNICIPAL) ? w.INSCR_MUNICIPAL : String.Empty)
                                                                + (w.LX_TIPO_LOGRADOURO != null ? w.LX_TIPO_LOGRADOURO.ToString() : String.Empty)
                                                                + (!String.IsNullOrEmpty(w.LOGRADOURO) ? w.LOGRADOURO : String.Empty)
                                                                + (!String.IsNullOrEmpty(w.NUMERO) ? w.NUMERO : String.Empty)
                                                                + (!String.IsNullOrEmpty(w.COMPLEMENTO) ? w.COMPLEMENTO : String.Empty)
                                                                + (!String.IsNullOrEmpty(w.BAIRRO) ? w.BAIRRO : String.Empty)
                                                                + (w.ID_MUNICIPIO != null ? w.ID_MUNICIPIO.ToString() : String.Empty)
                                                                + (!String.IsNullOrEmpty(w.MUNICIPIO) ? w.MUNICIPIO : String.Empty)
                                                                + (w.ID_UF != null ? w.ID_UF.ToString() : String.Empty)
                                                                + (!String.IsNullOrEmpty(w.UF) ? w.UF : String.Empty)
                                                                + (w.ID_CEP != null ? w.ID_CEP.ToString() : String.Empty)
                                                                + (!String.IsNullOrEmpty(w.CEP) ? w.CEP : String.Empty)
                                                                + (w.ID_PAIS != null ? w.ID_PAIS.ToString() : String.Empty)
                                                                + (!String.IsNullOrEmpty(w.PAIS) ? w.PAIS : String.Empty)
                                                                + (!String.IsNullOrEmpty(w.OBS_ENDERECO) ? w.OBS_ENDERECO : String.Empty)                                                                                                                               
                                                            ) == hash
                                                ).FirstOrDefault();                                                           
            }
            
            if (endereco == null && pfjendereco != null)
            {
                //novo endereco           
                if (String.IsNullOrEmpty(pfjendereco.NOME_ENDERECO) && pfjendereco.INDICA_PRINCIPAL)
                    pfjendereco.NOME_ENDERECO = "Endereço Principal";
                
                contexto.TBC_PFJ_ENDERECO.Add(pfjendereco);
                contexto.SaveChanges();
                endereco = pfjendereco;
            }
            else
            {
                // altera endereço 
                if((!String.IsNullOrEmpty(pfjendereco.NOME_ENDERECO) &&  endereco.NOME_ENDERECO  != pfjendereco.NOME_ENDERECO) ||
                    (!String.IsNullOrEmpty(pfjendereco.CNPJ_CPF_COLETA_ENTREGA) && endereco.CNPJ_CPF_COLETA_ENTREGA != pfjendereco.CNPJ_CPF_COLETA_ENTREGA) ||
                    (!String.IsNullOrEmpty(pfjendereco.INSCR_ESTADUAL) && endereco.INSCR_ESTADUAL != pfjendereco.INSCR_ESTADUAL) ||
                    (!String.IsNullOrEmpty(pfjendereco.INSCR_MUNICIPAL) && endereco.INSCR_MUNICIPAL != pfjendereco.INSCR_MUNICIPAL) ||
                    (pfjendereco.LX_TIPO_LOGRADOURO != null && endereco.LX_TIPO_LOGRADOURO != pfjendereco.LX_TIPO_LOGRADOURO) ||
                    (!String.IsNullOrEmpty(pfjendereco.LOGRADOURO) && endereco.LOGRADOURO != pfjendereco.LOGRADOURO) ||
                    (!String.IsNullOrEmpty(pfjendereco.NUMERO) && endereco.NUMERO != pfjendereco.NUMERO) ||
                    (!String.IsNullOrEmpty(pfjendereco.COMPLEMENTO) && endereco.COMPLEMENTO != pfjendereco.COMPLEMENTO) ||
                    (!String.IsNullOrEmpty(pfjendereco.BAIRRO) && endereco.BAIRRO != pfjendereco.BAIRRO) ||
                    (pfjendereco.ID_MUNICIPIO != null && endereco.ID_MUNICIPIO != pfjendereco.ID_MUNICIPIO) ||
                    (!String.IsNullOrEmpty(pfjendereco.MUNICIPIO) && endereco.MUNICIPIO != pfjendereco.MUNICIPIO) ||
                    (pfjendereco.ID_UF != null && endereco.ID_UF != pfjendereco.ID_UF) ||
                    (!String.IsNullOrEmpty(pfjendereco.UF) && endereco.UF != pfjendereco.UF) ||
                    (pfjendereco.ID_CEP != null && endereco.ID_CEP != pfjendereco.ID_CEP) ||
                    (!String.IsNullOrEmpty(pfjendereco.CEP) && endereco.CEP != pfjendereco.CEP) ||
                    (pfjendereco.ID_PAIS != null && endereco.ID_PAIS != pfjendereco.ID_PAIS) ||
                    (!String.IsNullOrEmpty(pfjendereco.PAIS) && endereco.PAIS != pfjendereco.PAIS) ||
                    (!String.IsNullOrEmpty(pfjendereco.OBS_ENDERECO) && endereco.OBS_ENDERECO != pfjendereco.OBS_ENDERECO) ||
                    (endereco.INDICA_PRINCIPAL != pfjendereco.INDICA_PRINCIPAL) ||
                    (endereco.INATIVO != pfjendereco.INATIVO) ||                      
                    (pfjendereco.EX_ID_PFJ_ENDERECO != null && endereco.EX_ID_PFJ_ENDERECO != pfjendereco.EX_ID_PFJ_ENDERECO)
                    )
                {
                    if (!pfjendereco.NOME_ENDERECO.IsNullOrEmpty()) endereco.NOME_ENDERECO = pfjendereco.NOME_ENDERECO;
                    if (!pfjendereco.CNPJ_CPF_COLETA_ENTREGA.IsNullOrEmpty()) endereco.CNPJ_CPF_COLETA_ENTREGA = pfjendereco.CNPJ_CPF_COLETA_ENTREGA;
                    if (!pfjendereco.INSCR_ESTADUAL.IsNullOrEmpty()) endereco.INSCR_ESTADUAL = pfjendereco.INSCR_ESTADUAL;
                    if (!pfjendereco.INSCR_MUNICIPAL.IsNullOrEmpty()) endereco.INSCR_MUNICIPAL = pfjendereco.INSCR_MUNICIPAL;
                    if (pfjendereco.LX_TIPO_LOGRADOURO != null) endereco.LX_TIPO_LOGRADOURO = pfjendereco.LX_TIPO_LOGRADOURO;
                    if (!pfjendereco.LOGRADOURO.IsNullOrEmpty()) endereco.LOGRADOURO = pfjendereco.LOGRADOURO;
                    if (!pfjendereco.NUMERO.IsNullOrEmpty()) endereco.NUMERO = pfjendereco.NUMERO;
                    if (!pfjendereco.COMPLEMENTO.IsNullOrEmpty()) endereco.COMPLEMENTO = pfjendereco.COMPLEMENTO;
                    if (!pfjendereco.BAIRRO.IsNullOrEmpty()) endereco.BAIRRO = pfjendereco.BAIRRO;
                    if (pfjendereco.ID_MUNICIPIO != null) endereco.ID_MUNICIPIO = pfjendereco.ID_MUNICIPIO;
                    if (!pfjendereco.MUNICIPIO.IsNullOrEmpty()) endereco.MUNICIPIO = pfjendereco.MUNICIPIO;
                    if (pfjendereco.ID_UF != null) endereco.ID_UF = pfjendereco.ID_UF;
                    if (!pfjendereco.UF.IsNullOrEmpty()) endereco.UF = pfjendereco.UF;
                    if (pfjendereco.ID_CEP != null) endereco.ID_CEP = pfjendereco.ID_CEP;
                    if (!pfjendereco.CEP.IsNullOrEmpty()) endereco.CEP = pfjendereco.CEP;
                    if (pfjendereco.ID_PAIS != null) endereco.ID_PAIS = pfjendereco.ID_PAIS;
                    if (!pfjendereco.PAIS.IsNullOrEmpty()) endereco.PAIS = pfjendereco.PAIS;
                    if (!pfjendereco.OBS_ENDERECO.IsNullOrEmpty()) endereco.OBS_ENDERECO = pfjendereco.OBS_ENDERECO;
                    endereco.INDICA_PRINCIPAL = pfjendereco.INDICA_PRINCIPAL;
                    endereco.INATIVO = pfjendereco.INATIVO;
                    if (pfjendereco.EX_ID_PFJ_ENDERECO != null) endereco.EX_ID_PFJ_ENDERECO = pfjendereco.EX_ID_PFJ_ENDERECO;
                }

                contexto.SaveChanges();
                pfjendereco = endereco;
            }

            if (pfjendereco.INDICA_PRINCIPAL)
                this.SetEnderecoPrincipal(pfjendereco.ID_PFJ, pfjendereco.ID_PFJ_ENDERECO);

            return pfjendereco;                                 
        }

        private void SetEnderecoPrincipal(int idPfj, int idPfjEndereco)
        {
            var enderecos = this.contexto.TBC_PFJ_ENDERECO.Include("TBC_PFJ").Where(w => w.ID_PFJ == idPfj).ToList();
            if (enderecos != null && enderecos.Count() > 0)
            {
                enderecos.ForEach(f => f.INDICA_PRINCIPAL = false);
                var principal = enderecos.Where(w => w.ID_PFJ_ENDERECO == idPfjEndereco).FirstOrDefault(); 
                
                principal.INDICA_PRINCIPAL = true;
                principal.INATIVO = false;
                principal.TBC_PFJ.LX_TIPO_LOGRADOURO = principal.LX_TIPO_LOGRADOURO;                
                principal.TBC_PFJ.LOGRADOURO = principal.LOGRADOURO;
                principal.TBC_PFJ.NUMERO = principal.NUMERO;
                principal.TBC_PFJ.BAIRRO = principal.BAIRRO;
                principal.TBC_PFJ.COMPLEMENTO = principal.COMPLEMENTO;
                principal.TBC_PFJ.MUNICIPIO = principal.MUNICIPIO;
                principal.TBC_PFJ.ID_MUNICIPIO = principal.ID_MUNICIPIO;
                principal.TBC_PFJ.UF = principal.UF;
                principal.TBC_PFJ.ID_UF = principal.ID_UF;
                principal.TBC_PFJ.ID_PAIS = principal.ID_PAIS;
                principal.TBC_PFJ.PAIS = principal.PAIS;
                principal.TBC_PFJ.OBS_ENDERECO = principal.OBS_ENDERECO;
                principal.TBC_PFJ.INSCR_ESTADUAL = principal.INSCR_ESTADUAL;
                principal.TBC_PFJ.INSCR_MUNICIPAL = principal.INSCR_MUNICIPAL;
                principal.TBC_PFJ.DATA_ALTERACAO = DateTime.Now;
                this.Alter(principal.TBC_PFJ);
                enderecos.ForEach(f => this.Alter(f));                
                this.SaveChanges();
            }
        }

        public void Alter(TBC_PFJ_ENDERECO endereco)
        {
            this.contexto.Entry(endereco).State = EntityState.Modified;
        }

        public void Alter(TBC_PFJ pfj)
        {
            this.contexto.Entry(pfj).State = EntityState.Modified;
        }
       
        public void SaveChanges()
        {
            this.contexto.SaveChanges();
        }

        public void Dispose()
        {
            if (contexto != null)
                contexto.Dispose();
        }
    }
}