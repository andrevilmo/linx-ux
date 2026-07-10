

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

namespace Linx.Demo.BV.ExAutocomplete
{
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[Look Up TBNMMEIO];DisplayName[Look Up TBNMMEIO];Height[0];Width[0];EdmEntityName[TBNMMEIO]")]	

	public partial class LookUpTbnmmeio 
	{
		
	    #region Data Properties	
	 


	    private int _idnomeMeio;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "id nomeMeio", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TBNMMEIO.id_nomeMeio]")]
	    public int idnomeMeio
	    {
	    	    get
	    	    {
	    	          return _idnomeMeio;
	    	    }
	    	    set
	    	    {
	    	          if (this._idnomeMeio != value)
	    	          {
	    	              this._idnomeMeio = value;
	    	          }
	    	    }
	    }

	    private string _Nomedomeio;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nomedomeio", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(100)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TBNMMEIO.Nomedomeio]")]
	    public string Nomedomeio
	    {
	    	    get
	    	    {
	    	          return _Nomedomeio;
	    	    }
	    	    set
	    	    {
	    	          if (this._Nomedomeio != value)
	    	          {
	    	              this._Nomedomeio = value;
	    	          }
	    	    }
	    }

	    private System.Nullable<int> _IdNome;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Nome", Description="", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TBNMMEIO.id_nome]")]
	    public System.Nullable<int> IdNome
	    {
	    	    get
	    	    {
	    	          return _IdNome;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdNome != value)
	    	          {
	    	              this._IdNome = value;
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
	[FunctionalPoint("ClassDescription[Look Up TBNOME];DisplayName[Look Up TBNOME];Height[0];Width[0];EdmEntityName[TBNOME]")]	

	public partial class LookUpTbnome 
	{
		
	    #region Data Properties	
	 


	    private System.Nullable<int> _IdNome;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Nome", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TBNOME.id_nome]")]
	    public System.Nullable<int> IdNome
	    {
	    	    get
	    	    {
	    	          return _IdNome;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdNome != value)
	    	          {
	    	              this._IdNome = value;
	    	          }
	    	    }
	    }

	    private string _Nome;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(100)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TBNOME.Nome]")]
	    public string Nome
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
	
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[Look Up TBSOBRENM];DisplayName[Look Up TBSOBRENM];Height[0];Width[0];EdmEntityName[TBSOBRENM]")]	

	public partial class LookUpTbsobrenm 
	{
		
	    #region Data Properties	
	 


	    private int _IdSobrenome;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Sobrenome", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TBSOBRENM.id_sobrenome]")]
	    public int IdSobrenome
	    {
	    	    get
	    	    {
	    	          return _IdSobrenome;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdSobrenome != value)
	    	          {
	    	              this._IdSobrenome = value;
	    	          }
	    	    }
	    }

	    private string _SobreNome;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "SobreNome", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(100)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TBSOBRENM.SobreNome]")]
	    public string SobreNome
	    {
	    	    get
	    	    {
	    	          return _SobreNome;
	    	    }
	    	    set
	    	    {
	    	          if (this._SobreNome != value)
	    	          {
	    	              this._SobreNome = value;
	    	          }
	    	    }
	    }

	    private System.Nullable<int> _IdNome;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Nome", Description="", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TBSOBRENM.id_nome]")]
	    public System.Nullable<int> IdNome
	    {
	    	    get
	    	    {
	    	          return _IdNome;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdNome != value)
	    	          {
	    	              this._IdNome = value;
	    	          }
	    	    }
	    }

	    private System.Nullable<int> _idnomeMeio;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "id nomeMeio", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TBSOBRENM.id_nomeMeio]")]
	    public System.Nullable<int> idnomeMeio
	    {
	    	    get
	    	    {
	    	          return _idnomeMeio;
	    	    }
	    	    set
	    	    {
	    	          if (this._idnomeMeio != value)
	    	          {
	    	              this._idnomeMeio = value;
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
	 


	    private int _IdCliente;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Cliente", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[CLIENTE.ID_CLIENTE]")]
	    public int IdCliente
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

	    private int _IdCliente2;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Cliente", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[CLIENTE.ID_CLIENTE]")]
	    public int IdCliente2
	    {
	    	    get
	    	    {
	    	          return _IdCliente2;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdCliente2 != value)
	    	          {
	    	              this._IdCliente2 = value;
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

	    private string _StringEstado;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Estado", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(50)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[ESTADO.STRING_ESTADO]")]
	    public string StringEstado
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
	[FunctionalPoint("ClassDescription[];DisplayName[Montar nome completo];Height[0];Width[0];EdmEntityName[]")]	

	public partial class LkpTbnmcompleto 
	{
		
	    #region Data Properties	
	 


	    private int _idNomeCompleto;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public int idNomeCompleto
	    {
	    	    get
	    	    {
	    	          return _idNomeCompleto;
	    	    }
	    	    set
	    	    {
	    	          if (this._idNomeCompleto != value)
	    	          {
	    	              this._idNomeCompleto = value;
	    	          }
	    	    }
	    }

	    private string _NomeCompleto;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public string NomeCompleto
	    {
	    	    get
	    	    {
	    	          return _NomeCompleto;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeCompleto != value)
	    	          {
	    	              this._NomeCompleto = value;
	    	          }
	    	    }
	    }	

	    #endregion Data Properties	

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	

}