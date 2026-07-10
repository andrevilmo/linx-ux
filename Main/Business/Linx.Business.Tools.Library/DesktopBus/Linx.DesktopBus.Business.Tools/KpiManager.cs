using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linx.Tools;
using Linx.Framework.BV.IndicadorMedida;


namespace Linx.Business.Tools
{
    public partial class KpiManager
    {
        public static void UpdateKpiInfo(KpiInfo kpi)
        {
            if (!kpi.IsNull() && !kpi.Started)
            {
                using (IndicadorMedidaDomainService context = new IndicadorMedidaDomainService())
                {
                    var entities = context.GetTcsIndicadorMedida().Where(e => e.CodIndicadorMedida == kpi.Name);

                    if (entities.Count() > 0 && entities.First().TcsIndicadorIndiceList.Count() > 0)
                    {
                        kpi.Description = entities.First().DescIndicadorMedida;
                        kpi.Ranges.Clear();
                        foreach (TcsIndicadorIndice item in entities.First().TcsIndicadorIndiceList)
                        {
                            kpi.Ranges.Add(item.CodIndiceMedida, new KpiRangeItem() { Description = item.DescIndiceMedida, StartValue = (double)item.LimiteInferior, EndValue = (double)item.LimiteSuperior, Alpha = (byte)(item.Rgb >> 24), Red = (byte)(item.Rgb >> 16), Green = (byte)(item.Rgb >> 8), Blue = (byte)(item.Rgb) });
                        }                        
                    }
                    kpi.Started = true;
                }
            }
        }
    }
}
