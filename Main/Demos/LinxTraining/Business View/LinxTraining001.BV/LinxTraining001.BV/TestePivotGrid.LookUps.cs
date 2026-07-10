

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linx.Data;
using Linx.Tools;
using System.Data.Entity.Core.Objects;
using System.ComponentModel;
using System.Data.Common;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Linq.Expressions;
using Linx.LinqExtensions.Query;
using Linx.LinqExtensions.Functional;
using Linx.LinqExtensions.Expressions;
using System.Data.Linq.SqlClient;
using System.Reflection;
using System.Data.Entity.Core.Objects.DataClasses;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;


namespace LinxTraining001.BV.TestePivotGrid
{
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[Look Up Ano];DisplayName[Look Up Ano];Height[0];Width[0];EdmEntityName[PivoGridOlap_Ano]")]	

	public partial class LookUpPivoGridOlapAno 
	{
		
	    #region Data Properties	
	 


	    private Int16 _Ano;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ano", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[[DATAS].[ANO].[ANO]]")]
	    public Int16 Ano
	    {
	    	    get
	    	    {
	    	          return _Ano;
	    	    }
	    	    set
	    	    {
	    	          if (this._Ano != value)
	    	          {
	    	              this._Ano = value;
	    	          }
	    	    }
	    }	

	    #endregion Data Properties	

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[Look Up Ano];DisplayName[Look Up Ano];Height[0];Width[0];EdmEntityName[EntityAdapter1_Ano]")]	

	public partial class LookUpEntityAdapter1Ano 
	{
		
	    #region Data Properties	
	 


	    private Int16 _Ano;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ano", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[[DATAS].[ANO].[ANO]]")]
	    public Int16 Ano
	    {
	    	    get
	    	    {
	    	          return _Ano;
	    	    }
	    	    set
	    	    {
	    	          if (this._Ano != value)
	    	          {
	    	              this._Ano = value;
	    	          }
	    	    }
	    }	

	    #endregion Data Properties	

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	

}