

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

using Linx.Framework.ControleSistema.BM;

namespace Linx.Framework.BV.Perfil
{
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[Look Up TBC_GRUPO_ECONOMICO];DisplayName[Look Up TBC_GRUPO_ECONOMICO];Height[0];Width[0];EdmEntityName[TBC_GRUPO_ECONOMICO]")]	

	public partial class LookUpTbcGrupoEconomico 
	{
		
	    #region Data Properties	
	 


	    private System.Nullable<int> _IdGpeconP;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Gpecon", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TBC_GRUPO_ECONOMICO.ID_GPECON]")]
	    public System.Nullable<int> IdGpeconP
	    {
	    	    get
	    	    {
	    	          return _IdGpeconP;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdGpeconP != value)
	    	          {
	    	              this._IdGpeconP = value;
	    	          }
	    	    }
	    }

	    private string _DescGrupoEconomico;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Grupo Econômico", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(60)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TBC_GRUPO_ECONOMICO.DESC_GRUPO_ECONOMICO]")]
	    public string DescGrupoEconomico
	    {
	    	    get
	    	    {
	    	          return _DescGrupoEconomico;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescGrupoEconomico != value)
	    	          {
	    	              this._DescGrupoEconomico = value;
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
	[FunctionalPoint("ClassDescription[Look Up TCS_USUARIO];DisplayName[Look Up TCS_USUARIO];Height[0];Width[0];EdmEntityName[TCS_USUARIO]")]	

	public partial class LookUpTcsUsuario 
	{
		
	    #region Data Properties	
	 


	    private string _NomeUsuario;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_USUARIO.NOME_USUARIO]")]
	    public string NomeUsuario
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

	    private long _IdUsuario;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_USUARIO.ID_USUARIO]")]
	    public long IdUsuario
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

	    private Guid _UidUsuario;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Usuario", Description="", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_USUARIO.UID_USUARIO]")]
	    public Guid UidUsuario
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

	    private int _IdLinx;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_USUARIO.ID_LINX]")]
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

	public partial class LookUpTcsPerfilRegraModulo 
	{
		
	    #region Data Properties	
	 


	    private Int64 _IdModulo;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[0]")]
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

	    private string _DescModulo;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Módulo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(60)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey['']")]
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

	    private string _DescAplicativo;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicativo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public string DescAplicativo
	    {
	    	    get
	    	    {
	    	          return _DescAplicativo;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescAplicativo != value)
	    	          {
	    	              this._DescAplicativo = value;
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

	public partial class LookUpTcsPerfilRegraColuna 
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
	    [StringLength(60)]
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
	    [Display(Name = "Código Transação", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(40)]
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
	
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[];DisplayName[];Height[0];Width[0];EdmEntityName[]")]	

	public partial class LookUpTcsPerfilRegraTransacao 
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
	    [StringLength(60)]
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
	    [Display(Name = "Código Transação", Description="", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [StringLength(40)]
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
	
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[Look Up TBC_BANDEIRA_REDE];DisplayName[Look Up TBC_BANDEIRA_REDE];Height[0];Width[0];Entities[TBC_BANDEIRA_REDE:IdBandeiraR];EdmEntityName[TBC_BANDEIRA_REDE]")]	

	public partial class LookUpTbcBandeiraRede 
	{
		
	    #region Data Properties	
	 


	    private System.String _DescBandeiraRede;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bandeira / Rede", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(60)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TBC_BANDEIRA_REDE.DESC_BANDEIRA_REDE]")]
	    public System.String DescBandeiraRede
	    {
	    	    get
	    	    {
	    	          return _DescBandeiraRede;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescBandeiraRede != value)
	    	          {
	    	              this._DescBandeiraRede = value;
	    	          }
	    	    }
	    }

	    private Int32 _IdBandeiraR;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Bandeira Rede", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TBC_BANDEIRA_REDE.ID_BANDEIRA_REDE]")]
	    public Int32 IdBandeiraR
	    {
	    	    get
	    	    {
	    	          return _IdBandeiraR;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdBandeiraR != value)
	    	          {
	    	              this._IdBandeiraR = value;
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
	[FunctionalPoint("ClassDescription[Look Up TCS_LAYOUT];DisplayName[Look Up TCS_LAYOUT];Height[0];Width[0];EdmEntityName[TCS_LAYOUT]")]	

	public partial class LookUpTcsLayout 
	{
		
	    #region Data Properties	
	 


	    private System.String _DescLayout;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Desc Layout", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(60)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_LAYOUT.DESC_LAYOUT]")]
	    public System.String DescLayout
	    {
	    	    get
	    	    {
	    	          return _DescLayout;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescLayout != value)
	    	          {
	    	              this._DescLayout = value;
	    	          }
	    	    }
	    }

	    private System.String _Detalhes;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Detalhes", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(500)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_LAYOUT.DETALHES]")]
	    public System.String Detalhes
	    {
	    	    get
	    	    {
	    	          return _Detalhes;
	    	    }
	    	    set
	    	    {
	    	          if (this._Detalhes != value)
	    	          {
	    	              this._Detalhes = value;
	    	          }
	    	    }
	    }

	    private Boolean _Inativo;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inativo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[CheckBox];FilterDataKey[TCS_LAYOUT.INATIVO]")]
	    public Boolean Inativo
	    {
	    	    get
	    	    {
	    	          return _Inativo;
	    	    }
	    	    set
	    	    {
	    	          if (this._Inativo != value)
	    	          {
	    	              this._Inativo = value;
	    	          }
	    	    }
	    }

	    private Int64 _IdObjetoConteudo;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Objeto Conteudo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_LAYOUT.ID_OBJETO_CONTEUDO]")]
	    public Int64 IdObjetoConteudo
	    {
	    	    get
	    	    {
	    	          return _IdObjetoConteudo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdObjetoConteudo != value)
	    	          {
	    	              this._IdObjetoConteudo = value;
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
	[FunctionalPoint("ClassDescription[Look Up TBC_FILIAL];DisplayName[Look Up TBC_FILIAL];Height[0];Width[0];Entities[TBC_FILIAL:IdFilialPfj];EdmEntityName[TBC_FILIAL]")]	

	public partial class LookUpTbcFilial 
	{
		
	    #region Data Properties	
	 


	    private System.String _CodigoFilial;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Código Filial", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(18)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TBC_FILIAL.CODIGO_FILIAL]")]
	    public System.String CodigoFilial
	    {
	    	    get
	    	    {
	    	          return _CodigoFilial;
	    	    }
	    	    set
	    	    {
	    	          if (this._CodigoFilial != value)
	    	          {
	    	              this._CodigoFilial = value;
	    	          }
	    	    }
	    }

	    private Int32 _IdFilialPfj;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Filial Pfj", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TBC_FILIAL.ID_FILIAL_PFJ]")]
	    public Int32 IdFilialPfj
	    {
	    	    get
	    	    {
	    	          return _IdFilialPfj;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdFilialPfj != value)
	    	          {
	    	              this._IdFilialPfj = value;
	    	          }
	    	    }
	    }

	    private System.String _NomeFilial;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Fantasia", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(60)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TBC_FILIAL.NOME_FILIAL]")]
	    public System.String NomeFilial
	    {
	    	    get
	    	    {
	    	          return _NomeFilial;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeFilial != value)
	    	          {
	    	              this._NomeFilial = value;
	    	          }
	    	    }
	    }	

	    #endregion Data Properties	

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	

}