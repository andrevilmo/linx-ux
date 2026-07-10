					

using System;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using Linx.Tools; 
    
namespace VAREJO.BV.KPIs
{

			
    public partial class KPIGrid : KpiInfo
    {

		
			public KPIGrid() : base()
			{
					this.Description = "";
					this.Name = "KPIGrid";
					this.ShowType = KpiShowType.Description;
			
					this.Ranges.Add("RUIM", new KpiRangeItem() { Description = "RUIM", StartValue = 0, EndValue = 10, Alpha = 255, Red = 255, Green = 0, Blue = 0  });
				    
					this.Ranges.Add("MEDIO", new KpiRangeItem() { Description = "MEDIO", StartValue = 11, EndValue = 20, Alpha = 255, Red = 255, Green = 255, Blue = 0  });
				    
					this.Ranges.Add("OTIMO", new KpiRangeItem() { Description = "OTIMO", StartValue = 21, EndValue = 50, Alpha = 255, Red = 0, Green = 255, Blue = 0  });
				    
			}
		
		
	}    
			
    public partial class GaugesFormulario : KpiInfo
    {

		
			public GaugesFormulario() : base()
			{
					this.Description = "";
					this.Name = "GaugesFormulario";
					this.ShowType = KpiShowType.Description;
			
					this.Ranges.Add("RUIM", new KpiRangeItem() { Description = "RUIM", StartValue = 0, EndValue = 10, Alpha = 255, Red = 255, Green = 0, Blue = 0  });
				    
					this.Ranges.Add("MEDIO", new KpiRangeItem() { Description = "MEDIO", StartValue = 11, EndValue = 20, Alpha = 255, Red = 255, Green = 255, Blue = 0  });
				    
					this.Ranges.Add("OTIMO", new KpiRangeItem() { Description = "OTIMO", StartValue = 21, EndValue = 50, Alpha = 255, Red = 0, Green = 255, Blue = 0  });
				    
			}
		
		
	}    

}