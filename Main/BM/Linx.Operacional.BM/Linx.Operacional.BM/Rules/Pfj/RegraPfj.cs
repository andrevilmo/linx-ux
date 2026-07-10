using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linx.Tools;
using Linx.Administrativo.BM.Contracts;

namespace Linx.Operacional.BM.Rules.Pfj
{
    public class RegraPfj
    {

        private RepositorioPfj repositorioPfj = null;

        public RegraPfj()
        {
            this.repositorioPfj = new RepositorioPfj(new LinxOperacional());
        }

        /// <summary>
        /// Resolve entidades de endereço partindo do Operacional e replicando para o Administrativo via contrato se a origem for operacional
        /// </summary>
        /// <param name="lEnderecos"></param>
        public void ResolvePfjEnderecoOperacional(List<Endereco> lEnderecos, bool indicaOrigemAdministrativo)
        {
            List<Administrativo.BM.Contracts.Endereco> lendAdm = new List<Administrativo.BM.Contracts.Endereco>(); //caso seja necessario submeter ao administrativo
            Administrativo.BM.Contracts.Endereco endAdm = null;
             
            if (lEnderecos != null && lEnderecos.Count > 0)
            {
                TBC_PFJ_ENDERECO endereco = null;

                foreach (var end in lEnderecos)
                {
                    if (end.IdPfj == 0)
                    {
                        if (indicaOrigemAdministrativo && !String.IsNullOrEmpty(end.CnpjCpfPfj))
                        {                            
                            end.IdPfjEndereco = 0;
                            // encontrar idpfj pelo cnpj 
                            var pfj = this.repositorioPfj.GetPfj(end.CnpjCpfPfj);
                            if (pfj != null) end.IdPfj = pfj.ID_PFJ;
                            else throw new Exception("Pfj não encontrado!");
                        }
                        else
                            throw new Exception("Pfj do endereço não informado!");
                    }

                    endereco = new TBC_PFJ_ENDERECO()
                    {
                        BAIRRO = end.Bairro,
                        CEP = end.Cep,
                        CNPJ_CPF_COLETA_ENTREGA = end.CnpjCpfColetaEntrega,
                        COMPLEMENTO = end.Complemento,
                        EX_ID_PFJ_ENDERECO = end.ExIdPfjEndereco,
                        ID_CEP = end.IdCep,
                        ID_MUNICIPIO = end.IdMunicipio,
                        ID_PAIS = end.IdPais,
                        INATIVO = end.Inativo,
                        INDICA_PRINCIPAL = end.IndicaPrincipal,
                        LOGRADOURO = end.Logradouro,
                        INSCR_ESTADUAL = end.InscricaoEstadual,
                        NOME_ENDERECO = end.NomeEndereco,
                        MUNICIPIO = end.Municipio,
                        NUMERO = end.Numero,
                        OBS_ENDERECO = end.ObsEndereco,
                        PAIS = end.Pais,
                        UF = end.UF,
                        LX_TIPO_ENDERECO = end.LxTipoEndereco,
                        LX_TIPO_LOGRADOURO = end.LxTipoLogradouro,
                        INSCR_MUNICIPAL = end.InscricaoMunicipal,
                        ID_PFJ = end.IdPfj,
                        ID_UF = end.IdUF,
                        ID_PFJ_ENDERECO = end.IdPfjEndereco,
                    };
                    endereco = this.repositorioPfj.ResolvePfjEnderecoOperacional(endereco);

                    if (!indicaOrigemAdministrativo)
                    {
                        end.IdPfjEndereco = endereco.ID_PFJ_ENDERECO;
                        endAdm = new Administrativo.BM.Contracts.Endereco();
                        endAdm.CopyFrom(end);
                        if (endereco.TBC_PFJ != null && !String.IsNullOrEmpty(endereco.TBC_PFJ.CNPJ_CPF))
                        {
                            endAdm.CnpjCpfPfj = endereco.TBC_PFJ.CNPJ_CPF;
                            endAdm.IdPfj = 0;
                        }
                        lendAdm.Add(endAdm);
                    }
                }

                if (!indicaOrigemAdministrativo)
                {
                    var implementacoesAministrativo = ImplementationHelper<IAdministrativo>.GetInstance("ImplementacoesAdministrativo");
                    implementacoesAministrativo.ResolveEnderecoAdministrativo(lendAdm, true);
                }
            }
        }
    }
}
