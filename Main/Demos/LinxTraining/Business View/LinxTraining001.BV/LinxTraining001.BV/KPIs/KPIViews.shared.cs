											

using System;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using Linx.Tools; 
    
namespace LinxTraining001.BV.KPIs
{

			
    public partial class KPITeste : KpiInfo
    {

		
			public KPITeste() : base()
			{
					this.Description = "";
					this.Name = "KPITeste";
					this.ShowType = KpiShowType.Value;
			
					this.Ranges.Add("Baixo", new KpiRangeItem() { Description = "Baixo", StartValue = 0, EndValue = 10, Alpha = 255, Red = 255, Green = 0, Blue = 0  });
				    
					this.Ranges.Add("Medio", new KpiRangeItem() { Description = "Médio", StartValue = 10.01, EndValue = 100, Alpha = 255, Red = 255, Green = 255, Blue = 0  });
				    
					this.Ranges.Add("Alto", new KpiRangeItem() { Description = "Alto", StartValue = 100.01, EndValue = 1000, Alpha = 255, Red = 0, Green = 128, Blue = 0  });
				    
			}
		
		
	}    
			
    public partial class KpiTeste : KpiInfo
    {

		
			public KpiTeste() : base()
			{
					this.Description = "";
					this.Name = "KpiTeste";
					this.ShowType = KpiShowType.Description;
			
					this.Ranges.Add("Baixo", new KpiRangeItem() { Description = "Baixo", StartValue = -1, EndValue = -0.2, Alpha = 255, Red = 255, Green = 0, Blue = 0  });
				    
					this.Ranges.Add("Medio", new KpiRangeItem() { Description = "Médio", StartValue = -0.19, EndValue = 0.19, Alpha = 255, Red = 255, Green = 255, Blue = 0  });
				    
					this.Ranges.Add("Alto", new KpiRangeItem() { Description = "Alto", StartValue = 0.2, EndValue = 1, Alpha = 255, Red = 0, Green = 192, Blue = 0  });
				    
			}
		
		
	}    

}