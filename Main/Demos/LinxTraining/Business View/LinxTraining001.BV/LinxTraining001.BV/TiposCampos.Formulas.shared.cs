

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


namespace LinxTraining001.BV.TiposCampos
{	
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////
	public partial class TiposCamposView
	{
	
	

	
	    public string GetKpiForDecimal()
        {
	        Linx.Business.Tools.KpiManager.UpdateKpiInfo(GetDecimalKPI());
	        KpiRangeItem range = GetDecimalKPI().GetRangeByValue((double)this.Decimal);
	        if (!range.IsNull())
                return range.Description;
	        else return String.Empty;
        }
		  
        public void CalculateDecimalKpiInfo()
        {
            DecimalKpiInfo = GetKpiForDecimal();
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
    
            if ((",Decimal,").IndexOf("," + e.PropertyName + ",") >= 0)
            	CalculateDecimalKpiInfo(); 
	    }		
		

	}
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////
	public partial class TiposCamposFilhaView
	{
	
	

	
	}
}


