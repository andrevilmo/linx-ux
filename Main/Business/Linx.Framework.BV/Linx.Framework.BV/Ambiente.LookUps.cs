

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

namespace Linx.Framework.BV.Ambiente
{
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[Look Up TCS_APLICACAO];DisplayName[Look Up TCS_APLICACAO];Height[0];Width[0];Entities[TCS_APLICACAO:IdAplicacao];EdmEntityName[TCS_APLICACAO]")]	

	public partial class LookUpTcsAplicacao 
	{
		
	    #region Data Properties	
	 


	    private System.String _DescricaoAplicacao;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicação", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(60)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_APLICACAO.DESCRICAO_APLICACAO]")]
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

	    private System.String _DescricaoAplicativo;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicativo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO]")]
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

	    private Boolean _EmDesenvolvimento;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Em Desenvolvimento", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[CheckBox];FilterDataKey[TCS_APLICACAO.EM_DESENVOLVIMENTO]")]
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

	    private Int32 _IdTcsAplicativo;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Aplicativo", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO]")]
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

	    private Int32 _IdAplicacao;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Aplicacao", Description="", Order = 4, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_APLICACAO.ID_APLICACAO]")]
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

	    private System.Guid _UidAplicacao;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Aplicacao", Description="", Order = 5, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_APLICACAO.UID_APLICACAO]")]
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

	    private System.String _Url;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Url", Description="", Order = 6, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_APLICACAO.URL]")]
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

	    private System.String _UrlWorkArea;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Url Work Area", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_APLICACAO.URL_WORK_AREA]")]
	    public System.String UrlWorkArea
	    {
	    	    get
	    	    {
	    	          return _UrlWorkArea;
	    	    }
	    	    set
	    	    {
	    	          if (this._UrlWorkArea != value)
	    	          {
	    	              this._UrlWorkArea = value;
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
	[FunctionalPoint("ClassDescription[Look Up TCS_EMPRESA_AUTENTICACAO];DisplayName[Look Up TCS_EMPRESA_AUTENTICACAO];Height[0];Width[0];Entities[TCS_EMPRESA_AUTENTICACAO:IdLinx];EdmEntityName[TCS_EMPRESA_AUTENTICACAO]")]	

	public partial class LookUpTcsEmpresaAutenticacao 
	{
		
	    #region Data Properties	
	 


	    private Int32 _IdLinx;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Linx", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    [Display(Name = "Empresa (Id Linx)", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    [Display(Name = "Uid Empresa", Description="", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
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
	[FunctionalPoint("ClassDescription[Look Up TCS_USUARIO_AUTENTICACAO];DisplayName[Look Up TCS_USUARIO_AUTENTICACAO];Height[0];Width[0];EdmEntityName[TCS_USUARIO_AUTENTICACAO]")]	

	public partial class LookUpTcsUsuarioAutenticacao 
	{
		
	    #region Data Properties	
	 


	    private String _NomeUsuario;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_USUARIO_AUTENTICACAO.NOME_USUARIO]")]
	    public String NomeUsuario
	    {
	    	    get
	    	    {
	    	          return _NomeUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeUsuario != value)
	    	          {
	    	              this._NomeUsuario = value;
	    	          }
	    	    }
	    }

	    private System.String _NomeEmpresa;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Grupo Econômico", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA]")]
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

	    private Int32 _IdLinx;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Grupo Econômico", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.ID_LINX]")]
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

	    private Int64 _IdUsuario;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_USUARIO_AUTENTICACAO.ID_USUARIO]")]
	    public Int64 IdUsuario
	    {
	    	    get
	    	    {
	    	          return _IdUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdUsuario != value)
	    	          {
	    	              this._IdUsuario = value;
	    	          }
	    	    }
	    }

	    private System.Guid _UidUsuario;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Usuario", Description="", Order = 4, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_USUARIO_AUTENTICACAO.UID_USUARIO]")]
	    public System.Guid UidUsuario
	    {
	    	    get
	    	    {
	    	          return _UidUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidUsuario != value)
	    	          {
	    	              this._UidUsuario = value;
	    	          }
	    	    }
	    }

	    private System.String _NomeAutenticacao;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Autenticacao", Description="", Order = 5, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO]")]
	    public System.String NomeAutenticacao
	    {
	    	    get
	    	    {
	    	          return _NomeAutenticacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeAutenticacao != value)
	    	          {
	    	              this._NomeAutenticacao = value;
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

	public partial class LookUpTcsAmbienteAdministrativo 
	{
		
	    #region Data Properties	
	 


	    private int _IdTcsAmbienteRelacionado;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public int IdTcsAmbienteRelacionado
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

	    private int _IdLinxAmbienteRelacionado;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public int IdLinxAmbienteRelacionado
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

	    private string _NomeEmpresaAmbienteRelacionado;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Empresa", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public string NomeEmpresaAmbienteRelacionado
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

	    private string _DescricaoAmbienteRelacionado;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ambiente", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public string DescricaoAmbienteRelacionado
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

	    private string _DescricaoAplicacaoAmbienteRelacionado;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicação", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(60)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public string DescricaoAplicacaoAmbienteRelacionado
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

	    #endregion Data Properties	

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[Look Up TCS_BANCO_SERVIDOR];DisplayName[Look Up TCS_BANCO_SERVIDOR];Height[0];Width[0];Entities[TCS_BANCO_SERVIDOR:IdTcsBancoServidor];EdmEntityName[TCS_BANCO_SERVIDOR]")]	

	public partial class LookUpTcsBancoServidor 
	{
		
	    #region Data Properties	
	 


	    private Int32 _IdTcsBancoServidor;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Banco Servidor", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_BANCO_SERVIDOR.ID_TCS_BANCO_SERVIDOR]")]
	    public Int32 IdTcsBancoServidor
	    {
	    	    get
	    	    {
	    	          return _IdTcsBancoServidor;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsBancoServidor != value)
	    	          {
	    	              this._IdTcsBancoServidor = value;
	    	          }
	    	    }
	    }

	    private System.String _DescricaoBancoServidor;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(80)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_BANCO_SERVIDOR.DESCRICAO_BANCO_SERVIDOR]")]
	    public System.String DescricaoBancoServidor
	    {
	    	    get
	    	    {
	    	          return _DescricaoBancoServidor;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescricaoBancoServidor != value)
	    	          {
	    	              this._DescricaoBancoServidor = value;
	    	          }
	    	    }
	    }

	    private System.String _NomeServidor;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Servidor", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_BANCO_SERVIDOR.NOME_SERVIDOR]")]
	    public System.String NomeServidor
	    {
	    	    get
	    	    {
	    	          return _NomeServidor;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeServidor != value)
	    	          {
	    	              this._NomeServidor = value;
	    	          }
	    	    }
	    }

	    private System.String _NomeBanco;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Banco de Dados", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_BANCO_SERVIDOR.NOME_BANCO]")]
	    public System.String NomeBanco
	    {
	    	    get
	    	    {
	    	          return _NomeBanco;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeBanco != value)
	    	          {
	    	              this._NomeBanco = value;
	    	          }
	    	    }
	    }

	    private Byte _LxTipoServidor;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Lx Tipo Servidor", Description="", Order = 4, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_BANCO_SERVIDOR.LX_TIPO_SERVIDOR]")]
	    public Byte LxTipoServidor
	    {
	    	    get
	    	    {
	    	          return _LxTipoServidor;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoServidor != value)
	    	          {
	    	              this._LxTipoServidor = value;
	    	          }
	    	    }
	    }

	    private System.String _StringConexao;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Conexao", Description="", Order = 5, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [StringLength(1000)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_BANCO_SERVIDOR.STRING_CONEXAO]")]
	    public System.String StringConexao
	    {
	    	    get
	    	    {
	    	          return _StringConexao;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringConexao != value)
	    	          {
	    	              this._StringConexao = value;
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
	[FunctionalPoint("ClassDescription[Look Up TCS_APLICATIVO_CONEXAO];DisplayName[Look Up TCS_APLICATIVO_CONEXAO];Height[0];Width[0];Entities[TCS_APLICATIVO_CONEXAO:IdTcsAplicativoConexao];EdmEntityName[TCS_APLICATIVO_CONEXAO]")]	

	public partial class LookUpTcsAplicativoConexao 
	{
		
	    #region Data Properties	
	 


	    private Int32 _IdTcsAplicativoConexao;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Aplicativo Conexao", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_APLICATIVO_CONEXAO.ID_TCS_APLICATIVO_CONEXAO]")]
	    public Int32 IdTcsAplicativoConexao
	    {
	    	    get
	    	    {
	    	          return _IdTcsAplicativoConexao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAplicativoConexao != value)
	    	          {
	    	              this._IdTcsAplicativoConexao = value;
	    	          }
	    	    }
	    }

	    private Int32 _IdConexaoDb;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Conexao Db", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB.ID_CONEXAO_DB]")]
	    public Int32 IdConexaoDb
	    {
	    	    get
	    	    {
	    	          return _IdConexaoDb;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdConexaoDb != value)
	    	          {
	    	              this._IdConexaoDb = value;
	    	          }
	    	    }
	    }

	    private System.String _NomeConexao;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Provider BM", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB.NOME_CONEXAO]")]
	    public System.String NomeConexao
	    {
	    	    get
	    	    {
	    	          return _NomeConexao;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeConexao != value)
	    	          {
	    	              this._NomeConexao = value;
	    	          }
	    	    }
	    }

	    private Int32 _IdTcsAplicativo;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Aplicativo", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_APLICATIVO_CONEXAO.TCS_APLICATIVO.ID_TCS_APLICATIVO]")]
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
	[FunctionalPoint("ClassDescription[Look Up TCS_SERVICO];DisplayName[Look Up TCS_SERVICO];Height[0];Width[0];Entities[TCS_SERVICO:IdTcsServico];EdmEntityName[TCS_SERVICO]")]	

	public partial class LookUpTcsServico 
	{
		
	    #region Data Properties	
	 


	    private Int32 _IdTcsServico;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Servico", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_SERVICO.ID_TCS_SERVICO]")]
	    public Int32 IdTcsServico
	    {
	    	    get
	    	    {
	    	          return _IdTcsServico;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsServico != value)
	    	          {
	    	              this._IdTcsServico = value;
	    	          }
	    	    }
	    }

	    private System.String _NomeServico;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Serviço", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_SERVICO.NOME_SERVICO]")]
	    public System.String NomeServico
	    {
	    	    get
	    	    {
	    	          return _NomeServico;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeServico != value)
	    	          {
	    	              this._NomeServico = value;
	    	          }
	    	    }
	    }	

	    #endregion Data Properties	

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	

}