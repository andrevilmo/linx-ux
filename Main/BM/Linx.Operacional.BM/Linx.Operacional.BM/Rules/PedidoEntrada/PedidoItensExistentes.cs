using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Operacional.BM.Rules.PedidoEntrada
{
    public class PedidoItensExistentes
    {
        public int IdPedido { get; set; }

        public List<int> IdsItens { get; set; }
    }
}
