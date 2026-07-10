

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
using System.Linq.Expressions;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

using LINXDEMO.BM;

namespace Linx.Demo.BV.ModalExterna
{
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[Look Up ESTADO];DisplayName[Look Up ESTADO];Height[0];Width[0];EdmEntityName[ESTADO]")]	

	public partial class LookUpEstado 
	{
		
	    #region Data Properties	
	 


	    private System.Nullable<int> _IdEstado;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Estado", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[ESTADO.ID_ESTADO]")]
	    public System.Nullable<int> IdEstado
	    {
	    	    get
	    	    {
	    	          return _IdEstado;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdEstado != value)
	    	          {
	    	              this._IdEstado = value;
	    	          }
	    	    }
	    }

	    private System.Nullable<int> _IdPais;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Pais", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[ESTADO.PAIS.ID_PAIS]")]
	    public System.Nullable<int> IdPais
	    {
	    	    get
	    	    {
	    	          return _IdPais;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdPais != value)
	    	          {
	    	              this._IdPais = value;
	    	          }
	    	    }
	    }

	    private System.Nullable<string> _StringPais;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Pais", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(50)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[ESTADO.PAIS.STRING_PAIS]")]
	    public System.Nullable<string> StringPais
	    {
	    	    get
	    	    {
	    	          return _StringPais;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringPais != value)
	    	          {
	    	              this._StringPais = value;
	    	          }
	    	    }
	    }

	    private System.Nullable<string> _StringEstado;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Estado", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(50)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[ESTADO.STRING_ESTADO]")]
	    public System.Nullable<string> StringEstado
	    {
	    	    get
	    	    {
	    	          return _StringEstado;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringEstado != value)
	    	          {
	    	              this._StringEstado = value;
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
	[FunctionalPoint("ClassDescription[Look Up CLIENTE];DisplayName[Look Up CLIENTE];Height[0];Width[0];EdmEntityName[CLIENTE]")]	

	public partial class LookUpCliente 
	{
		
	    #region Data Properties	
	 


	    private System.Nullable<int> _IdCliente;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Cliente", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[CLIENTE.ID_CLIENTE]")]
	    public System.Nullable<int> IdCliente
	    {
	    	    get
	    	    {
	    	          return _IdCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdCliente != value)
	    	          {
	    	              this._IdCliente = value;
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
	[FunctionalPoint("ClassDescription[Look Up LOJA];DisplayName[Look Up LOJA];Height[0];Width[0];EdmEntityName[LOJA]")]	

	public partial class LookUpLoja 
	{
		
	    #region Data Properties	
	 


	    private System.Nullable<int> _IdLoja;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Loja", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[LOJA.ID_LOJA]")]
	    public System.Nullable<int> IdLoja
	    {
	    	    get
	    	    {
	    	          return _IdLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLoja != value)
	    	          {
	    	              this._IdLoja = value;
	    	          }
	    	    }
	    }

	    private System.Nullable<string> _StringLoja;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Loja", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(50)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[LOJA.STRING_LOJA]")]
	    public System.Nullable<string> StringLoja
	    {
	    	    get
	    	    {
	    	          return _StringLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringLoja != value)
	    	          {
	    	              this._StringLoja = value;
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
	[FunctionalPoint("ClassDescription[Look Up VENDA];DisplayName[Look Up VENDA];Height[0];Width[0];EdmEntityName[VENDA]")]	

	public partial class LookUpVenda 
	{
		
	    #region Data Properties	
	 


	    private System.Nullable<int> _IdVenda;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Venda", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[VENDA.ID_VENDA]")]
	    public System.Nullable<int> IdVenda
	    {
	    	    get
	    	    {
	    	          return _IdVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdVenda != value)
	    	          {
	    	              this._IdVenda = value;
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
	[FunctionalPoint("ClassDescription[Look Up PAIS];DisplayName[Look Up PAIS];Height[0];Width[0];EdmEntityName[PAIS]")]	

	public partial class LookUpPais 
	{
		
	    #region Data Properties	
	 


	    private System.Nullable<int> _IdPais;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Pais", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[PAIS.ID_PAIS]")]
	    public System.Nullable<int> IdPais
	    {
	    	    get
	    	    {
	    	          return _IdPais;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdPais != value)
	    	          {
	    	              this._IdPais = value;
	    	          }
	    	    }
	    }

	    private System.Nullable<string> _StringPais;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Pais", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(50)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[PAIS.STRING_PAIS]")]
	    public System.Nullable<string> StringPais
	    {
	    	    get
	    	    {
	    	          return _StringPais;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringPais != value)
	    	          {
	    	              this._StringPais = value;
	    	          }
	    	    }
	    }	

	    #endregion Data Properties	

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	

}