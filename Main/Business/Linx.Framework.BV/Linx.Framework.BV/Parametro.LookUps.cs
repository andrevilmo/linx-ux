

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

namespace Linx.Framework.BV.Parametro
{
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[Look Up TCS_APLICATIVO];DisplayName[Look Up TCS_APLICATIVO];Height[0];Width[0];Entities[:IdTcsAplicativo];EdmEntityName[]")]	

	public partial class LookUpTcsAplicativo 
	{
		
	    #region Data Properties	
	 


	    private Int32 _IdTcsAplicativo;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicativo", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[0]")]
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

	    private System.String _DescricaoAplicativo;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey['']")]
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
	[FunctionalPoint("ClassDescription[];DisplayName[];Height[0];Width[0];EdmEntityName[]")]	

	public partial class LookTcsParametroUsuario 
	{
		
	    #region Data Properties	
	 


	    private Int64 _IdUsuario;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
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

	    private string _NomeUsuario;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(25)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
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

	    private string _ChaveSelecao;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [StringLength(25)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public string ChaveSelecao
	    {
	    	    get
	    	    {
	    	          return _ChaveSelecao;
	    	    }
	    	    set
	    	    {
	    	          if (this._ChaveSelecao != value)
	    	          {
	    	              this._ChaveSelecao = value;
	    	          }
	    	    }
	    }

	    private string _UidUsuarioString;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [StringLength(25)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public string UidUsuarioString
	    {
	    	    get
	    	    {
	    	          return _UidUsuarioString;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidUsuarioString != value)
	    	          {
	    	              this._UidUsuarioString = value;
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

	public partial class LookUpParametroRede 
	{
		
	    #region Data Properties	
	 


	    private int _IdBandeiraRedeParam;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public int IdBandeiraRedeParam
	    {
	    	    get
	    	    {
	    	          return _IdBandeiraRedeParam;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdBandeiraRedeParam != value)
	    	          {
	    	              this._IdBandeiraRedeParam = value;
	    	          }
	    	    }
	    }

	    private string _CodBandeiraRede;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Código Bandeira / Rede", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(2)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public string CodBandeiraRede
	    {
	    	    get
	    	    {
	    	          return _CodBandeiraRede;
	    	    }
	    	    set
	    	    {
	    	          if (this._CodBandeiraRede != value)
	    	          {
	    	              this._CodBandeiraRede = value;
	    	          }
	    	    }
	    }

	    private string _DescBandeiraRede;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bandeira / Rede", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(6)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
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

	    private string _ChaveSelecao;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [StringLength(25)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public string ChaveSelecao
	    {
	    	    get
	    	    {
	    	          return _ChaveSelecao;
	    	    }
	    	    set
	    	    {
	    	          if (this._ChaveSelecao != value)
	    	          {
	    	              this._ChaveSelecao = value;
	    	          }
	    	    }
	    }

	    private string _IdBandeiraRedeString;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 4, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [StringLength(25)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public string IdBandeiraRedeString
	    {
	    	    get
	    	    {
	    	          return _IdBandeiraRedeString;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdBandeiraRedeString != value)
	    	          {
	    	              this._IdBandeiraRedeString = value;
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
	[FunctionalPoint("ClassDescription[];DisplayName[];Height[0];Width[0];Entities[:IdGpecon];EdmEntityName[]")]	

	public partial class LookUpParametroGpecon 
	{
		
	    #region Data Properties	
	 


	    private Int32 _IdGpecon;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = " Código Grupo Econômico", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public Int32 IdGpecon
	    {
	    	    get
	    	    {
	    	          return _IdGpecon;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdGpecon != value)
	    	          {
	    	              this._IdGpecon = value;
	    	          }
	    	    }
	    }

	    private string _DescGrupoEconomico;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Grupo Econômico", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(6)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
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

	    private string _IdGpeconString;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [StringLength(1)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public string IdGpeconString
	    {
	    	    get
	    	    {
	    	          return _IdGpeconString;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdGpeconString != value)
	    	          {
	    	              this._IdGpeconString = value;
	    	          }
	    	    }
	    }

	    private string _ChaveSelecao;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [StringLength(25)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public string ChaveSelecao
	    {
	    	    get
	    	    {
	    	          return _ChaveSelecao;
	    	    }
	    	    set
	    	    {
	    	          if (this._ChaveSelecao != value)
	    	          {
	    	              this._ChaveSelecao = value;
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
	[FunctionalPoint("ClassDescription[];DisplayName[];Height[0];Width[0];Entities[:IdFilialPfj];EdmEntityName[]")]	

	public partial class LookUpParametroFilial 
	{
		
	    #region Data Properties	
	 


	    private string _CodigoFilial;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Código Filial", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(1)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
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

	    private Int32 _IdFilialPfj;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
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

	    private String _IdFilialString;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [StringLength(2)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public String IdFilialString
	    {
	    	    get
	    	    {
	    	          return _IdFilialString;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdFilialString != value)
	    	          {
	    	              this._IdFilialString = value;
	    	          }
	    	    }
	    }

	    private string _NomeFilial;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Filial", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(6)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
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

	    private string _ChaveSelecao;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 4, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [StringLength(25)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public string ChaveSelecao
	    {
	    	    get
	    	    {
	    	          return _ChaveSelecao;
	    	    }
	    	    set
	    	    {
	    	          if (this._ChaveSelecao != value)
	    	          {
	    	              this._ChaveSelecao = value;
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
	[FunctionalPoint("ClassDescription[];DisplayName[];Height[0];Width[0];Entities[:IdLoja];EdmEntityName[]")]	

	public partial class LookUpParametroLoja 
	{
		
	    #region Data Properties	
	 


	    private string _CodLoja;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Código Loja", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(2)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public string CodLoja
	    {
	    	    get
	    	    {
	    	          return _CodLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._CodLoja != value)
	    	          {
	    	              this._CodLoja = value;
	    	          }
	    	    }
	    }

	    private string _DescLoja;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Loja", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(6)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public string DescLoja
	    {
	    	    get
	    	    {
	    	          return _DescLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescLoja != value)
	    	          {
	    	              this._DescLoja = value;
	    	          }
	    	    }
	    }

	    private Int32 _IdLoja;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public Int32 IdLoja
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

	    private string _IdLojaString;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [StringLength(2)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public string IdLojaString
	    {
	    	    get
	    	    {
	    	          return _IdLojaString;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLojaString != value)
	    	          {
	    	              this._IdLojaString = value;
	    	          }
	    	    }
	    }

	    private string _ChaveSelecao;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 4, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [StringLength(25)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public string ChaveSelecao
	    {
	    	    get
	    	    {
	    	          return _ChaveSelecao;
	    	    }
	    	    set
	    	    {
	    	          if (this._ChaveSelecao != value)
	    	          {
	    	              this._ChaveSelecao = value;
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
	[FunctionalPoint("ClassDescription[];DisplayName[];Height[0];Width[0];Entities[:UidTabela];EdmEntityName[]")]	

	public partial class LookUpTcsTabelaAutorizacaoC 
	{
		
	    #region Data Properties	
	 


	    private Guid _UidTabela;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[Guid.Empty]")]
	    public Guid UidTabela
	    {
	    	    get
	    	    {
	    	          return _UidTabela;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidTabela != value)
	    	          {
	    	              this._UidTabela = value;
	    	          }
	    	    }
	    }

	    private string _DescTabela;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(8)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey['']")]
	    public string DescTabela
	    {
	    	    get
	    	    {
	    	          return _DescTabela;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescTabela != value)
	    	          {
	    	              this._DescTabela = value;
	    	          }
	    	    }
	    }

	    private string _NomeTabela;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Tabela", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(25)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey['']")]
	    public string NomeTabela
	    {
	    	    get
	    	    {
	    	          return _NomeTabela;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeTabela != value)
	    	          {
	    	              this._NomeTabela = value;
	    	          }
	    	    }
	    }

	    private string _ClasseNome;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Código Transação", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
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