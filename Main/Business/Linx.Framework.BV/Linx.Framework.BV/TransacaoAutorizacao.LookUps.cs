

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

using Linx.Framework.Autorizacao.BM;

namespace Linx.Framework.BV.TransacaoAutorizacao
{
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[Look Up TCS_OBJETO_AUTORIZACAO];DisplayName[Look Up TCS_OBJETO_AUTORIZACAO];Height[0];Width[0];EdmEntityName[TCS_OBJETO_AUTORIZACAO]")]	

	public partial class LookUpTcsObjetoAutorizacao 
	{
		
	    #region Data Properties	
	 


	    private string _DescObjeto;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Classe BO", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(60)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_OBJETO_AUTORIZACAO.DESC_OBJETO]")]
	    public string DescObjeto
	    {
	    	    get
	    	    {
	    	          return _DescObjeto;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescObjeto != value)
	    	          {
	    	              this._DescObjeto = value;
	    	          }
	    	    }
	    }

	    private string _ObjetoClasseNome;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Classe Nome", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_OBJETO_AUTORIZACAO.CLASSE_NOME]")]
	    public string ObjetoClasseNome
	    {
	    	    get
	    	    {
	    	          return _ObjetoClasseNome;
	    	    }
	    	    set
	    	    {
	    	          if (this._ObjetoClasseNome != value)
	    	          {
	    	              this._ObjetoClasseNome = value;
	    	          }
	    	    }
	    }

	    private long _IdObjeto;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Objeto", Description="", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_OBJETO_AUTORIZACAO.ID_OBJETO]")]
	    public long IdObjeto
	    {
	    	    get
	    	    {
	    	          return _IdObjeto;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdObjeto != value)
	    	          {
	    	              this._IdObjeto = value;
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
	[FunctionalPoint("ClassDescription[Look Up TCS_MODULO_AUTORIZACAO];DisplayName[Look Up TCS_MODULO_AUTORIZACAO];Height[0];Width[0];EdmEntityName[TCS_MODULO_AUTORIZACAO]")]	

	public partial class LookUpTcsModuloAutorizacao 
	{
		
	    #region Data Properties	
	 


	    private string _DescModulo;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Módulo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(100)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_MODULO_AUTORIZACAO.DESC_MODULO]")]
	    public string DescModulo
	    {
	    	    get
	    	    {
	    	          return _DescModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescModulo != value)
	    	          {
	    	              this._DescModulo = value;
	    	          }
	    	    }
	    }

	    private System.Nullable<long> _IdModulo;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_MODULO_AUTORIZACAO.ID_MODULO]")]
	    public System.Nullable<long> IdModulo
	    {
	    	    get
	    	    {
	    	          return _IdModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdModulo != value)
	    	          {
	    	              this._IdModulo = value;
	    	          }
	    	    }
	    }

	    private System.String _DescricaoAplicativo;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicativo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO]")]
	    public System.String DescricaoAplicativo
	    {
	    	    get
	    	    {
	    	          return _DescricaoAplicativo;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescricaoAplicativo != value)
	    	          {
	    	              this._DescricaoAplicativo = value;
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
	[FunctionalPoint("ClassDescription[Look Up TCS_MODULO_MENU_AUTORIZACAO];DisplayName[Look Up TCS_MODULO_MENU_AUTORIZACAO];Height[0];Width[0];EdmEntityName[TCS_MODULO_MENU_AUTORIZACAO]")]	

	public partial class LookUpTcsModuloMenuAutorizacao 
	{
		
	    #region Data Properties	
	 


	    private string _DescricaoAplicativo;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicativo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO]")]
	    public string DescricaoAplicativo
	    {
	    	    get
	    	    {
	    	          return _DescricaoAplicativo;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescricaoAplicativo != value)
	    	          {
	    	              this._DescricaoAplicativo = value;
	    	          }
	    	    }
	    }

	    private string _DescModulo;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Módulo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(100)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.DESC_MODULO]")]
	    public string DescModulo
	    {
	    	    get
	    	    {
	    	          return _DescModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescModulo != value)
	    	          {
	    	              this._DescModulo = value;
	    	          }
	    	    }
	    }

	    private System.String _DescModuloMenu;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Menu", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(100)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_MODULO_MENU_AUTORIZACAO.DESC_MODULO_MENU]")]
	    public System.String DescModuloMenu
	    {
	    	    get
	    	    {
	    	          return _DescModuloMenu;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescModuloMenu != value)
	    	          {
	    	              this._DescModuloMenu = value;
	    	          }
	    	    }
	    }

	    private System.String _DescModuloMenuSuperior;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Menu Superior", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(100)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_MODULO_MENU_AUTORIZACAO.MODULO_MENU_SUPERIOR.DESC_MODULO_MENU]")]
	    public System.String DescModuloMenuSuperior
	    {
	    	    get
	    	    {
	    	          return _DescModuloMenuSuperior;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescModuloMenuSuperior != value)
	    	          {
	    	              this._DescModuloMenuSuperior = value;
	    	          }
	    	    }
	    }

	    private long _IdModulo;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo", Description="", Order = 4, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.ID_MODULO]")]
	    public long IdModulo
	    {
	    	    get
	    	    {
	    	          return _IdModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdModulo != value)
	    	          {
	    	              this._IdModulo = value;
	    	          }
	    	    }
	    }

	    private bool _InativoModulo;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inativo", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[CheckBox];FilterDataKey[TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.INATIVO]")]
	    public bool InativoModulo
	    {
	    	    get
	    	    {
	    	          return _InativoModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._InativoModulo != value)
	    	          {
	    	              this._InativoModulo = value;
	    	          }
	    	    }
	    }

	    private long _IdModuloMenu;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo Menu", Description="", Order = 6, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_MODULO_MENU_AUTORIZACAO.ID_MODULO_MENU]")]
	    public long IdModuloMenu
	    {
	    	    get
	    	    {
	    	          return _IdModuloMenu;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdModuloMenu != value)
	    	          {
	    	              this._IdModuloMenu = value;
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
	[FunctionalPoint("ClassDescription[];DisplayName[];Height[0];Width[0];EdmEntityName[]")]	

	public partial class LookUpTcsTransacaoDependente 
	{
		
	    #region Data Properties	
	 


	    private Int64 _IdTransacao;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[0]")]
	    public Int64 IdTransacao
	    {
	    	    get
	    	    {
	    	          return _IdTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTransacao != value)
	    	          {
	    	              this._IdTransacao = value;
	    	          }
	    	    }
	    }

	    private string _DescTransacao;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Transação", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(6)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey['']")]
	    public string DescTransacao
	    {
	    	    get
	    	    {
	    	          return _DescTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescTransacao != value)
	    	          {
	    	              this._DescTransacao = value;
	    	          }
	    	    }
	    }

	    private string _ClasseNome;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Formulário", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(4)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey['']")]
	    public string ClasseNome
	    {
	    	    get
	    	    {
	    	          return _ClasseNome;
	    	    }
	    	    set
	    	    {
	    	          if (this._ClasseNome != value)
	    	          {
	    	              this._ClasseNome = value;
	    	          }
	    	    }
	    }	

	    #endregion Data Properties	

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	

}