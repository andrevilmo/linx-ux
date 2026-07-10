

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

namespace Linx.Framework.BV.Empresa
{
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[];DisplayName[];Height[0];Width[0];Entities[TCS_EMPRESA_AUTENTICACAO:IdLinxGpecon];EdmEntityName[TCS_EMPRESA_AUTENTICACAO]")]	

	public partial class LookUpTcsEmpresaAutenticacaoGpecon 
	{
		
	    #region Data Properties	
	 


	    private Int32 _IdLinxGpecon;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_EMPRESA_AUTENTICACAO.ID_LINX]")]
	    public Int32 IdLinxGpecon
	    {
	    	    get
	    	    {
	    	          return _IdLinxGpecon;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLinxGpecon != value)
	    	          {
	    	              this._IdLinxGpecon = value;
	    	          }
	    	    }
	    }

	    private System.String _GrupoEconomico;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Empresa / Grupo Econômico", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA]")]
	    public System.String GrupoEconomico
	    {
	    	    get
	    	    {
	    	          return _GrupoEconomico;
	    	    }
	    	    set
	    	    {
	    	          if (this._GrupoEconomico != value)
	    	          {
	    	              this._GrupoEconomico = value;
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
	[FunctionalPoint("ClassDescription[Look Up TCS_APLICACAO];DisplayName[Look Up TCS_APLICACAO];Height[0];Width[0];EdmEntityName[TCS_APLICACAO]")]	

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

	    private Boolean _EmDesenvolvimento;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Em Desenvolvimento", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
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

	    #endregion Data Properties	

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[];DisplayName[];Height[0];Width[0];Entities[TCS_AMBIENTE:IdTcsAmbiente];EdmEntityName[TCS_AMBIENTE]")]	

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

	    private Int32 _IdTcsAmbiente;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
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
	 


	    private System.String _DescModulo;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Módulo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(60)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_MODULO_AUTORIZACAO.DESC_MODULO]")]
	    public System.String DescModulo
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

	    private Int64 _IdModulo;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_MODULO_AUTORIZACAO.ID_MODULO]")]
	    public Int64 IdModulo
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

	    private Int32 _IdTcsAplicativo;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Aplicativo", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO]")]
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
	[FunctionalPoint("ClassDescription[];DisplayName[];Height[0];Width[0];EdmEntityName[TCS_USUARIO_AUTENTICACAO]")]	

	public partial class LookUpTcsUsuarioAutenticacao 
	{
		
	    #region Data Properties	
	 


	    private Int32 _IdLinx;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
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
	    [Display(Name = "Id Usuario", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
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

	    private System.String _NomeUsuario;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Usuário", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_USUARIO_AUTENTICACAO.NOME_USUARIO]")]
	    public System.String NomeUsuario
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

	    private System.String _NomeAutenticacao;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário Autenticação", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	[FunctionalPoint("ClassDescription[];DisplayName[];Height[0];Width[0];Entities[TCS_EMPRESA_AUTENTICACAO:IdLinx];EdmEntityName[TCS_EMPRESA_AUTENTICACAO]")]	

	public partial class LookUpTcsEmpresaAutenticacaoP 
	{
		
	    #region Data Properties	
	 


	    private Int32 _IdLinx;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
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
	    [Display(Name = "Empresa", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	

}