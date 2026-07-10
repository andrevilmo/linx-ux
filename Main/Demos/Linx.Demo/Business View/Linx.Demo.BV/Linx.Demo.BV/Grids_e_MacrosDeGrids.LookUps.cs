

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

namespace Linx.Demo.BV.Grids_e_MacrosDeGrids
{
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[Look Up LOJA];DisplayName[Look Up LOJA];Height[0];Width[0];EdmEntityName[LOJA]")]	

	public partial class LookUpLoja 
	{
		
	    #region Data Properties	
	 


	    private int _IdLoja;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Loja", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[LOJA.ID_LOJA]")]
	    public int IdLoja
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

	    private string _StringLoja;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Loja", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(50)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[LOJA.STRING_LOJA]")]
	    public string StringLoja
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

	    private int _IdCidade;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Cidade", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[LOJA.CIDADE.ID_CIDADE]")]
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

	    private string _NomeCidade;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Cidade", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(50)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[LOJA.CIDADE.NOME_CIDADE]")]
	    public string NomeCidade
	    {
	    	    get
	    	    {
	    	          return _NomeCidade;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeCidade != value)
	    	          {
	    	              this._NomeCidade = value;
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

	    private string _StringVendedor;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Vendedor", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(50)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[VENDEDOR.STRING_VENDEDOR]")]
	    public string StringVendedor
	    {
	    	    get
	    	    {
	    	          return _StringVendedor;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringVendedor != value)
	    	          {
	    	              this._StringVendedor = value;
	    	          }
	    	    }
	    }	

	    #endregion Data Properties	

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	

}