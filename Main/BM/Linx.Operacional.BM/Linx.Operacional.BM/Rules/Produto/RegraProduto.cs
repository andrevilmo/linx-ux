using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Operacional.BM.Rules.Produto
{
    public class RegraProduto
    {
        private RepositorioProduto repositorioProduto = null;
        private LinxOperacional contexto = null;

        public RegraProduto()
        {
            contexto = new LinxOperacional();
            this.repositorioProduto = new RepositorioProduto(contexto); 
        }


        /// <summary>
        /// Atualiza Descrição e NCM do produto
        /// </summary>
        /// <param name="codSku">Código do produto</param>
        /// <param name="descricao">Descrição</param>
        /// <param name="idClassifFiscal">Id da classificação fiscal</param>
        /// <returns></returns>
        public bool AtualizaCadastroProduto(int idSku, string descricao, short idClassifFiscal)
        {
            if (String.IsNullOrEmpty(descricao)) return false; 

            var produto = this.repositorioProduto.GetProduto(idSku);
           
            if (produto != null)
            {
                produto.DESC_SKU = descricao;
                produto.ID_CLASSIF_FISCAL = idClassifFiscal;
                this.repositorioProduto.Alter(produto);
                this.repositorioProduto.SaveChanges();
                return true;
            }
            else
                return false;
        }



        /// <summary>
        /// Verifica se o GTIN recebido (codigo + dígito) é válido para os tipos (8,12,13,14)
        /// </summary>
        /// <param name="codigoDeBarras">codigo + dígito para validação/param>      
        /// <returns></returns>
        public bool ValidaGTIN(string codigoDeBarras)
        {
            bool GtinValido = true;
            int tamanho = codigoDeBarras.Length;

            try
            {

                if (tamanho != 8 && tamanho != 12 && tamanho != 13 && tamanho != 14)
                {
                    GtinValido = false;
                    //throw new Exception("GTIN -Inválido");
                }

                string digVerRec = codigoDeBarras.Substring(tamanho - 1, 1);
                codigoDeBarras = codigoDeBarras.Substring(0, tamanho - 1);

                int somaPos = 0;


                int[] Pos = new int[tamanho - 1];
                short val = 0;

                for (int i = 0; i < codigoDeBarras.Length; i++)
                {
                    if (Int16.TryParse(codigoDeBarras.Substring(i, 1), out val))
                    {
                        Pos[i] = Convert.ToInt16(codigoDeBarras.Substring(i, 1));

                    }
                    else
                    {
                        GtinValido = false;
                        //throw new Exception("GTIN -Inválido! Utilize somente números.");
                    }
                }


                switch (tamanho)
                {
                    case 8:
                        //GTIM 8
                        somaPos = Pos[0] + Pos[2] + Pos[4] + Pos[6];
                        somaPos = somaPos * 3;

                        somaPos = somaPos + Pos[1] + Pos[3] + Pos[5];

                        break;

                    case 12:
                        //GTIM 12
                        somaPos = Pos[0] + Pos[2] + Pos[4] + Pos[6] + Pos[8] + Pos[10];
                        somaPos = somaPos * 3;

                        somaPos = somaPos + Pos[1] + Pos[3] + Pos[5] + Pos[7] + Pos[9];

                        break;
                    case 13:
                        //GTIM 13

                        somaPos = Pos[1] + Pos[3] + Pos[5] + Pos[7] + Pos[9] + Pos[11];
                        somaPos = somaPos * 3;

                        somaPos = somaPos + Pos[0] + Pos[2] + Pos[4] + Pos[6] + Pos[8] + Pos[10];

                        break;

                    case 14:
                        //GTIM 14
                        somaPos = Pos[0] + Pos[2] + Pos[4] + Pos[6] + Pos[8] + Pos[10] + Pos[12];
                        somaPos = somaPos * 3;

                        somaPos = somaPos + Pos[1] + Pos[3] + Pos[5] + Pos[7] + Pos[9] + Pos[11];

                        break;
                }


                string _digito = "";

                if ((somaPos + 0) % 10 == 0) _digito = "0";
                if ((somaPos + 1) % 10 == 0) _digito = "1";
                if ((somaPos + 2) % 10 == 0) _digito = "2";
                if ((somaPos + 3) % 10 == 0) _digito = "3";
                if ((somaPos + 4) % 10 == 0) _digito = "4";
                if ((somaPos + 5) % 10 == 0) _digito = "5";
                if ((somaPos + 6) % 10 == 0) _digito = "6";
                if ((somaPos + 7) % 10 == 0) _digito = "7";
                if ((somaPos + 8) % 10 == 0) _digito = "8";
                if ((somaPos + 9) % 10 == 0) _digito = "9";

                if (_digito != digVerRec)
                {
                    GtinValido = false;
                }

            }
            catch (Exception ex)
            {
                GtinValido = false;             
                //throw new Exception(ex.Message);                
            }
      

            return GtinValido;
        }
    }
}
