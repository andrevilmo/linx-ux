

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

using LinxTraining002.BM;

namespace LinxTraining001.BV.TiposCamposFilhaSingle
{
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[Look Up TiposCampos];DisplayName[Look Up TiposCampos];Height[0];Width[0];Entities[TiposCampos:IDTiposCampos];EdmEntityName[TiposCampos]")]	

	public partial class LookUpTiposCampos 
	{
		
	    #region Data Properties	
	 


	    private Int32 _IDTiposCampos;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID TiposCampos", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TiposCampos.ID_TiposCampos]")]
	    public Int32 IDTiposCampos
	    {
	    	    get
	    	    {
	    	          return _IDTiposCampos;
	    	    }
	    	    set
	    	    {
	    	          if (this._IDTiposCampos != value)
	    	          {
	    	              this._IDTiposCampos = value;
	    	          }
	    	    }
	    }	

	    #endregion Data Properties	

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	

}