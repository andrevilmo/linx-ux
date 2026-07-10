

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

namespace Linx.Framework.Custom.BV.PerfilFranquia
{
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[Look Up TCS_USUARIO];DisplayName[Look Up TCS_USUARIO];Height[0];Width[0];EdmEntityName[TCS_USUARIO]")]	

	public partial class LookUpTcsUsuario 
	{
		
	    #region Data Properties	
	 


	    private long _IdUsuario;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
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

	    private string _NomeUsuario;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Usuario", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
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

	    private int _IdLinx;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx", Description="", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
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

	    private Guid _UidUsuario;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Usuario", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
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

	    private string _Origem;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public string Origem
	    {
	    	    get
	    	    {
	    	          return _Origem;
	    	    }
	    	    set
	    	    {
	    	          if (this._Origem != value)
	    	          {
	    	              this._Origem = value;
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

	public partial class LookUpLxRegraAcessoModulo 
	{
		
	    #region Data Properties	
	 


	    private byte _LxRegraAcessoModulo;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Regra Módulo", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public byte LxRegraAcessoModulo
	    {
	    	    get
	    	    {
	    	          return _LxRegraAcessoModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxRegraAcessoModulo != value)
	    	          {
	    	              this._LxRegraAcessoModulo = value;
	    	          }
	    	    }
	    }

	    private string _LxRegraAcessoModuloName;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Regra Acesso", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public string LxRegraAcessoModuloName
	    {
	    	    get
	    	    {
	    	          return _LxRegraAcessoModuloName;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxRegraAcessoModuloName != value)
	    	          {
	    	              this._LxRegraAcessoModuloName = value;
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

	    private string _Origem;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(60)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public string Origem
	    {
	    	    get
	    	    {
	    	          return _Origem;
	    	    }
	    	    set
	    	    {
	    	          if (this._Origem != value)
	    	          {
	    	              this._Origem = value;
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

	public partial class LookupLxRegraAcessoTransacao 
	{
		
	    #region Data Properties	
	 


	    private byte _LxRegraAcessoTransacao;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[0]")]
	    public byte LxRegraAcessoTransacao
	    {
	    	    get
	    	    {
	    	          return _LxRegraAcessoTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxRegraAcessoTransacao != value)
	    	          {
	    	              this._LxRegraAcessoTransacao = value;
	    	          }
	    	    }
	    }

	    private string _LxRegraAcessoTransacaoName;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Regra Acesso", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(60)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey['']")]
	    public string LxRegraAcessoTransacaoName
	    {
	    	    get
	    	    {
	    	          return _LxRegraAcessoTransacaoName;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxRegraAcessoTransacaoName != value)
	    	          {
	    	              this._LxRegraAcessoTransacaoName = value;
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
	[FunctionalPoint("ClassDescription[Look Up TBC_BANDEIRA_REDE];DisplayName[Look Up TBC_BANDEIRA_REDE];Height[0];Width[0];EdmEntityName[TBC_BANDEIRA_REDE]")]	

	public partial class LookUpTbcBandeiraRede 
	{
		
	    #region Data Properties	
	 


	    private int _IdBandeiraR;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Bandeira / Rede", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TBC_BANDEIRA_REDE.ID_BANDEIRA_REDE]")]
	    public int IdBandeiraR
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

	    private string _DescBandeiraRede;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bandeira / Rede", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(60)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TBC_BANDEIRA_REDE.DESC_BANDEIRA_REDE]")]
	    public string DescBandeiraRede
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

	    #endregion Data Properties	

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[Look Up TBC_FILIAL];DisplayName[Look Up TBC_FILIAL];Height[0];Width[0];EdmEntityName[TBC_FILIAL]")]	

	public partial class LookUpTbcFilial 
	{
		
	    #region Data Properties	
	 


	    private string _CodigoFilial;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Código Filial", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(18)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TBC_FILIAL.CODIGO_FILIAL]")]
	    public string CodigoFilial
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

	    private int _IdFilialPfj;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Filial Pfj", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TBC_FILIAL.ID_FILIAL_PFJ]")]
	    public int IdFilialPfj
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

	    private string _NomeFilial;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Fantasia", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(60)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TBC_FILIAL.NOME_FILIAL]")]
	    public string NomeFilial
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