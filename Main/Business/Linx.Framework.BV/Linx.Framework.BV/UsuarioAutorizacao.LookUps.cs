

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

namespace Linx.Framework.BV.UsuarioAutorizacao
{
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[Look Up TCS_EMPRESA_AUTENTICACAO];DisplayName[Look Up TCS_EMPRESA_AUTENTICACAO];Height[0];Width[0];Entities[TCS_EMPRESA_AUTENTICACAO:IdLinx];EdmEntityName[TCS_EMPRESA_AUTENTICACAO]")]	

	public partial class LookUpTcsUsuarioEmpresaAutenticacao 
	{
		
	    #region Data Properties	
	 


	    private Int32 _IdLinx;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Grupo Econômico", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_EMPRESA_AUTENTICACAO.ID_LINX]")]
	    public Int32 IdLinx
	    {
	    	    get
	    	    {
	    	          return _IdLinx;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLinx != value)
	    	          {
	    	              this._IdLinx = value;
	    	          }
	    	    }
	    }

	    private System.String _NomeEmpresa;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Empresa / Grupo Econômico", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA]")]
	    public System.String NomeEmpresa
	    {
	    	    get
	    	    {
	    	          return _NomeEmpresa;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeEmpresa != value)
	    	          {
	    	              this._NomeEmpresa = value;
	    	          }
	    	    }
	    }

	    private System.Guid _UidEmpresa;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Empresa", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA]")]
	    public System.Guid UidEmpresa
	    {
	    	    get
	    	    {
	    	          return _UidEmpresa;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidEmpresa != value)
	    	          {
	    	              this._UidEmpresa = value;
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
	[FunctionalPoint("ClassDescription[Look Up TCS_AMBIENTE];DisplayName[Look Up TCS_AMBIENTE];Height[0];Width[0];EdmEntityName[TCS_AMBIENTE]")]	

	public partial class LookUpTcsAmbiente 
	{
		
	    #region Data Properties	
	 


	    private System.String _DescricaoAmbiente;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ambiente", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_AMBIENTE.DESCRICAO_AMBIENTE]")]
	    public System.String DescricaoAmbiente
	    {
	    	    get
	    	    {
	    	          return _DescricaoAmbiente;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescricaoAmbiente != value)
	    	          {
	    	              this._DescricaoAmbiente = value;
	    	          }
	    	    }
	    }

	    private System.String _DescricaoAplicacao;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicação", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(60)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_AMBIENTE.TCS_APLICACAO.DESCRICAO_APLICACAO]")]
	    public System.String DescricaoAplicacao
	    {
	    	    get
	    	    {
	    	          return _DescricaoAplicacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescricaoAplicacao != value)
	    	          {
	    	              this._DescricaoAplicacao = value;
	    	          }
	    	    }
	    }

	    private Boolean _EmDesenvolvimento;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Em Desenvolvimento", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[CheckBox];FilterDataKey[TCS_AMBIENTE.TCS_APLICACAO.EM_DESENVOLVIMENTO]")]
	    public Boolean EmDesenvolvimento
	    {
	    	    get
	    	    {
	    	          return _EmDesenvolvimento;
	    	    }
	    	    set
	    	    {
	    	          if (this._EmDesenvolvimento != value)
	    	          {
	    	              this._EmDesenvolvimento = value;
	    	          }
	    	    }
	    }

	    private Int32 _IdLinxEmpresa;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX]")]
	    public Int32 IdLinxEmpresa
	    {
	    	    get
	    	    {
	    	          return _IdLinxEmpresa;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLinxEmpresa != value)
	    	          {
	    	              this._IdLinxEmpresa = value;
	    	          }
	    	    }
	    }

	    private System.String _DescricaoAplicativo;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicativo", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO]")]
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

	    private Int32 _IdTcsAmbiente;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente", Description="", Order = 5, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_AMBIENTE.ID_TCS_AMBIENTE]")]
	    public Int32 IdTcsAmbiente
	    {
	    	    get
	    	    {
	    	          return _IdTcsAmbiente;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAmbiente != value)
	    	          {
	    	              this._IdTcsAmbiente = value;
	    	          }
	    	    }
	    }

	    private System.String _NomeEmpresa;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Grupo Econômico", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA]")]
	    public System.String NomeEmpresa
	    {
	    	    get
	    	    {
	    	          return _NomeEmpresa;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeEmpresa != value)
	    	          {
	    	              this._NomeEmpresa = value;
	    	          }
	    	    }
	    }

	    private System.Guid _UidAplicacao;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Aplicacao", Description="", Order = 7, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_AMBIENTE.TCS_APLICACAO.UID_APLICACAO]")]
	    public System.Guid UidAplicacao
	    {
	    	    get
	    	    {
	    	          return _UidAplicacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidAplicacao != value)
	    	          {
	    	              this._UidAplicacao = value;
	    	          }
	    	    }
	    }

	    private System.Guid _UidEmpresa;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Empresa", Description="", Order = 8, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA]")]
	    public System.Guid UidEmpresa
	    {
	    	    get
	    	    {
	    	          return _UidEmpresa;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidEmpresa != value)
	    	          {
	    	              this._UidEmpresa = value;
	    	          }
	    	    }
	    }

	    private System.String _Url;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Url", Description="", Order = 9, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_AMBIENTE.TCS_APLICACAO.URL]")]
	    public System.String Url
	    {
	    	    get
	    	    {
	    	          return _Url;
	    	    }
	    	    set
	    	    {
	    	          if (this._Url != value)
	    	          {
	    	              this._Url = value;
	    	          }
	    	    }
	    }

	    private Int32 _IdTcsAplicativo;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Aplicativo", Description="", Order = 10, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO]")]
	    public Int32 IdTcsAplicativo
	    {
	    	    get
	    	    {
	    	          return _IdTcsAplicativo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAplicativo != value)
	    	          {
	    	              this._IdTcsAplicativo = value;
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
	[FunctionalPoint("ClassDescription[Look Up TCS_AMBIENTE];DisplayName[Look Up TCS_AMBIENTE];Height[0];Width[0];EdmEntityName[TCS_AMBIENTE]")]	

	public partial class LookUpTcsAmbiente1 
	{
		
	    #region Data Properties	
	 


	    private System.String _DescricaoAmbienteRelacionado;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ambiente", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_AMBIENTE.DESCRICAO_AMBIENTE]")]
	    public System.String DescricaoAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _DescricaoAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescricaoAmbienteRelacionado != value)
	    	          {
	    	              this._DescricaoAmbienteRelacionado = value;
	    	          }
	    	    }
	    }

	    private System.String _NomeEmpresaAmbienteRelacionado;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Empresa", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA]")]
	    public System.String NomeEmpresaAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _NomeEmpresaAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeEmpresaAmbienteRelacionado != value)
	    	          {
	    	              this._NomeEmpresaAmbienteRelacionado = value;
	    	          }
	    	    }
	    }

	    private System.String _DescricaoAplicacaoAmbienteRelacionado;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicação", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(60)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_AMBIENTE.TCS_APLICACAO.DESCRICAO_APLICACAO]")]
	    public System.String DescricaoAplicacaoAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _DescricaoAplicacaoAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescricaoAplicacaoAmbienteRelacionado != value)
	    	          {
	    	              this._DescricaoAplicacaoAmbienteRelacionado = value;
	    	          }
	    	    }
	    }

	    private System.Nullable<Int32> _IdLinxAmbienteRelacionado;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Linx", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX]")]
	    public System.Nullable<Int32> IdLinxAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _IdLinxAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLinxAmbienteRelacionado != value)
	    	          {
	    	              this._IdLinxAmbienteRelacionado = value;
	    	          }
	    	    }
	    }

	    private System.Nullable<Int32> _IdTcsAmbienteRelacionado;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente1", Description="", Order = 4, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_AMBIENTE.ID_TCS_AMBIENTE]")]
	    public System.Nullable<Int32> IdTcsAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _IdTcsAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAmbienteRelacionado != value)
	    	          {
	    	              this._IdTcsAmbienteRelacionado = value;
	    	          }
	    	    }
	    }

	    private System.Nullable<Int32> _IdAplicacao;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Aplicacao", Description="", Order = 5, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_AMBIENTE.TCS_APLICACAO.ID_APLICACAO]")]
	    public System.Nullable<Int32> IdAplicacao
	    {
	    	    get
	    	    {
	    	          return _IdAplicacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdAplicacao != value)
	    	          {
	    	              this._IdAplicacao = value;
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
	[FunctionalPoint("ClassDescription[Look Up TCS_EMPRESA_AUTENTICACAO];DisplayName[Look Up TCS_EMPRESA_AUTENTICACAO];Height[0];Width[0];EdmEntityName[TCS_EMPRESA_AUTENTICACAO]")]	

	public partial class LookUpTcsEmpresaAutenticacao 
	{
		
	    #region Data Properties	
	 


	    private int _IdLinx;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx Empresa / Grupo Econômico", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_EMPRESA_AUTENTICACAO.ID_LINX]")]
	    public int IdLinx
	    {
	    	    get
	    	    {
	    	          return _IdLinx;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLinx != value)
	    	          {
	    	              this._IdLinx = value;
	    	          }
	    	    }
	    }

	    private string _NomeEmpresa;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Empresa / Grupo Econômico", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA]")]
	    public string NomeEmpresa
	    {
	    	    get
	    	    {
	    	          return _NomeEmpresa;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeEmpresa != value)
	    	          {
	    	              this._NomeEmpresa = value;
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
	[FunctionalPoint("ClassDescription[];DisplayName[];Height[0];Width[0];Entities[:IdTcsAmbienteRelacionado];EdmEntityName[]")]	

	public partial class LookUpTcsAmbiente2Relacionado 
	{
		
	    #region Data Properties	
	 


	    private System.String _DescricaoAmbienteRelacionado;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ambiente", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public System.String DescricaoAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _DescricaoAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescricaoAmbienteRelacionado != value)
	    	          {
	    	              this._DescricaoAmbienteRelacionado = value;
	    	          }
	    	    }
	    }

	    private Int32 _IdTcsAmbienteRelacionado;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public Int32 IdTcsAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _IdTcsAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAmbienteRelacionado != value)
	    	          {
	    	              this._IdTcsAmbienteRelacionado = value;
	    	          }
	    	    }
	    }

	    private System.String _DescricaoAplicativo;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicativo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
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

	    private System.String _DescricaoAplicacao;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicação", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [StringLength(60)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public System.String DescricaoAplicacao
	    {
	    	    get
	    	    {
	    	          return _DescricaoAplicacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescricaoAplicacao != value)
	    	          {
	    	              this._DescricaoAplicacao = value;
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
	[FunctionalPoint("ClassDescription[];DisplayName[];Height[0];Width[0];Entities[:IdTcsAmbiente];EdmEntityName[]")]	

	public partial class LookUpTcsAmbiente2 
	{
		
	    #region Data Properties	
	 


	    private System.String _DescricaoAmbiente;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ambiente", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public System.String DescricaoAmbiente
	    {
	    	    get
	    	    {
	    	          return _DescricaoAmbiente;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescricaoAmbiente != value)
	    	          {
	    	              this._DescricaoAmbiente = value;
	    	          }
	    	    }
	    }

	    private System.String _DescricaoAplicativo;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicativo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
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

	    private System.String _NomeEmpresa;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Empresa", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public System.String NomeEmpresa
	    {
	    	    get
	    	    {
	    	          return _NomeEmpresa;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeEmpresa != value)
	    	          {
	    	              this._NomeEmpresa = value;
	    	          }
	    	    }
	    }

	    private System.String _DescricaoAplicacao;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicação", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [StringLength(60)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public System.String DescricaoAplicacao
	    {
	    	    get
	    	    {
	    	          return _DescricaoAplicacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescricaoAplicacao != value)
	    	          {
	    	              this._DescricaoAplicacao = value;
	    	          }
	    	    }
	    }

	    private Int32 _IdTcsAmbiente;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente", Description="", Order = 4, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public Int32 IdTcsAmbiente
	    {
	    	    get
	    	    {
	    	          return _IdTcsAmbiente;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAmbiente != value)
	    	          {
	    	              this._IdTcsAmbiente = value;
	    	          }
	    	    }
	    }

	    private Int32 _IdAplicacao;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Aplicacao", Description="", Order = 5, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public Int32 IdAplicacao
	    {
	    	    get
	    	    {
	    	          return _IdAplicacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdAplicacao != value)
	    	          {
	    	              this._IdAplicacao = value;
	    	          }
	    	    }
	    }

	    private Int32 _IdTcsAplicativo;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Aplicativo", Description="", Order = 6, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public Int32 IdTcsAplicativo
	    {
	    	    get
	    	    {
	    	          return _IdTcsAplicativo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAplicativo != value)
	    	          {
	    	              this._IdTcsAplicativo = value;
	    	          }
	    	    }
	    }

	    private Int32 _IdLinx;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public Int32 IdLinx
	    {
	    	    get
	    	    {
	    	          return _IdLinx;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLinx != value)
	    	          {
	    	              this._IdLinx = value;
	    	          }
	    	    }
	    }	

	    #endregion Data Properties	

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	

}