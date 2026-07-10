

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

namespace LinxTraining001.BV.DetalhamentoVenda
{
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[Look Up Clientes];DisplayName[Look Up Clientes];Height[0];Width[0];Entities[Clientes:IDClientes];EdmEntityName[Clientes]")]	

	public partial class LookUpClientes 
	{
		
	    #region Data Properties	
	 


	    private System.Guid _IDClientes;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Clientes", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[Clientes.ID_Clientes]")]
	    public System.Guid IDClientes
	    {
	    	    get
	    	    {
	    	          return _IDClientes;
	    	    }
	    	    set
	    	    {
	    	          if (this._IDClientes != value)
	    	          {
	    	              this._IDClientes = value;
	    	          }
	    	    }
	    }

	    private System.String _Nome;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(40)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[Clientes.Nome]")]
	    public System.String Nome
	    {
	    	    get
	    	    {
	    	          return _Nome;
	    	    }
	    	    set
	    	    {
	    	          if (this._Nome != value)
	    	          {
	    	              this._Nome = value;
	    	          }
	    	    }
	    }	

	    #endregion Data Properties	

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	

}