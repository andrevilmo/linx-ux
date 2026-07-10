

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

using Linx.Demo.BM;

namespace Linx.Demo.BV.ModalExterna
{
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[Look Up CIDADE];DisplayName[Look Up CIDADE];Height[0];Width[0];EdmEntityName[CIDADE]")]	

	public partial class LookUpCidade 
	{
		
	    #region Data Properties	
	 


	    private int _IdCidade;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Cidade", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[CIDADE.ID_CIDADE]")]
	    public int IdCidade
	    {
	    	    get
	    	    {
	    	          return _IdCidade;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdCidade != value)
	    	          {
	    	              this._IdCidade = value;
	    	          }
	    	    }
	    }

	    private System.Nullable<int> _IdEstado;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Estado", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[CIDADE.ESTADO.ID_ESTADO]")]
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
	    [Display(Name = "Id Pais", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[CIDADE.ESTADO.PAIS.ID_PAIS]")]
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

	    #endregion Data Properties	

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[Look Up VENDEDOR];DisplayName[Look Up VENDEDOR];Height[0];Width[0];EdmEntityName[VENDEDOR]")]	

	public partial class LookUpVendedor 
	{
		
	    #region Data Properties	
	 


	    private System.Nullable<int> _IdVendedor;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Vendedor", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[VENDEDOR.ID_VENDEDOR]")]
	    public System.Nullable<int> IdVendedor
	    {
	    	    get
	    	    {
	    	          return _IdVendedor;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdVendedor != value)
	    	          {
	    	              this._IdVendedor = value;
	    	          }
	    	    }
	    }	

	    #endregion Data Properties	

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	

}