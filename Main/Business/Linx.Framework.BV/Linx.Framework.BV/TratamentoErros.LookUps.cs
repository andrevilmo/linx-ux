

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

namespace Linx.Framework.BV.TratamentoErros
{
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[Look Up TCS_EMPRESA_AUTENTICACAO];DisplayName[Look Up TCS_EMPRESA_AUTENTICACAO];Height[0];Width[0];EdmEntityName[TCS_EMPRESA_AUTENTICACAO]")]	

	public partial class LookUpGpecon 
	{
		
	    #region Data Properties	
	 


	    private System.Nullable<Int32> _IdLinxGpecon;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx Gpecon", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_EMPRESA_AUTENTICACAO.ID_LINX]")]
	    public System.Nullable<Int32> IdLinxGpecon
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

	    private System.String _Gpecon;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Grupo Economico", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA]")]
	    public System.String Gpecon
	    {
	    	    get
	    	    {
	    	          return _Gpecon;
	    	    }
	    	    set
	    	    {
	    	          if (this._Gpecon != value)
	    	          {
	    	              this._Gpecon = value;
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
	    [Display(Name = "Descricao Ambiente", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
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

	    private System.Nullable<Int32> _IdTcsAmbiente;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_AMBIENTE.ID_TCS_AMBIENTE]")]
	    public System.Nullable<Int32> IdTcsAmbiente
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
	[FunctionalPoint("ClassDescription[Look Up TCS_APLICACAO];DisplayName[Look Up TCS_APLICACAO];Height[0];Width[0];EdmEntityName[TCS_APLICACAO]")]	

	public partial class LookUpTcsAplicacao 
	{
		
	    #region Data Properties	
	 


	    private System.String _DescricaoAplicacao;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descricao Aplicacao", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
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

	    private System.Nullable<Int32> _IdAplicacao;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Aplicacao", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_APLICACAO.ID_APLICACAO]")]
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
	 


	    private System.Nullable<Int32> _IdLinxEmpresa;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx Empresa", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_EMPRESA_AUTENTICACAO.ID_LINX]")]
	    public System.Nullable<Int32> IdLinxEmpresa
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

	    private System.String _NomeEmpresa;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Empresa", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	 


	    private System.Nullable<Int64> _IdUsuario;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_USUARIO_AUTENTICACAO.ID_USUARIO]")]
	    public System.Nullable<Int64> IdUsuario
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

	    private String _NomeAutenticacao;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário Autenticação", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO]")]
	    public String NomeAutenticacao
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

	    private String _NomeUsuario;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
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

	    #endregion Data Properties	

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	

}