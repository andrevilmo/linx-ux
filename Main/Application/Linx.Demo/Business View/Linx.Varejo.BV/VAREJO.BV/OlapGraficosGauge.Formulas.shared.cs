

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using Linx.LinqExtensions.Query;
using Linx.LinqExtensions.Functional;
using Linx.LinqExtensions.Expressions;
using System.Xml.Serialization;
using Linx;
using Linx.Tools;


namespace VAREJO.BV.OlapGraficosGauge
{	
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////
	public partial class Venda
	{
	
	

	
	    public string GetKpiForBigIntVenda()
        {
	        Linx.Business.Tools.KpiManager.UpdateKpiInfo(GetBigIntVendaKPI());
	        KpiRangeItem range = GetBigIntVendaKPI().GetRangeByValue((double)this.BigIntVenda);
	        if (!range.IsNull())
                return range.Description;
	        else return String.Empty;
        }
		  
        public void CalculateBigIntVendaKpiInfo()
        {
            BigIntVendaKpiInfo = GetKpiForBigIntVenda();
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
    
            if ((",BigIntVenda,").IndexOf("," + e.PropertyName + ",") >= 0)
            	CalculateBigIntVendaKpiInfo(); 
	    }		
		

	}
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////
	public partial class VendaItem
	{
	
	

	
	    public string GetKpiForBigIntVendaItem()
        {
	        Linx.Business.Tools.KpiManager.UpdateKpiInfo(GetBigIntVendaItemKPI());
	        KpiRangeItem range = GetBigIntVendaItemKPI().GetRangeByValue((double)this.BigIntVendaItem);
	        if (!range.IsNull())
                return range.Description;
	        else return String.Empty;
        }
		  
        public void CalculateBigIntVendaItemKpiInfo()
        {
            BigIntVendaItemKpiInfo = GetKpiForBigIntVendaItem();
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
    
            if ((",BigIntVendaItem,").IndexOf("," + e.PropertyName + ",") >= 0)
            	CalculateBigIntVendaItemKpiInfo(); 
	    }		
		

	}
}


