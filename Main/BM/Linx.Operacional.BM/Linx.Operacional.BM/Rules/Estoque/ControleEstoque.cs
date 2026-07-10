using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Operacional.BM.Rules.Estoque
{
    public partial class ControleEstoque
    {
        public decimal Saldo { get; set; }
        public int IdDeposito { get; set; }
        public int IdSku { get; set; }
        public decimal EntradaPendente { get; set; }
        public decimal SaidaPendente { get; set; }
        public DateTime? DataSaida { get; set; }
        public DateTime? DataEntrada { get; set; }
        public int? IdStkLote { get; set; }
        public int? IdStkLocalizacao { get; set; }
        public bool Exclusao { get; set; }
    }
}
