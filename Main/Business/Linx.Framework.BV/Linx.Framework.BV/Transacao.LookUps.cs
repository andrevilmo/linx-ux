

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

namespace Linx.Framework.BV.Transacao
{
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[];DisplayName[];Height[0];Width[0];EdmEntityName[]")]	

	public partial class LookUpTcsObjetoTransacao 
	{
		
	    #region Data Properties	
	 


	    private Int64 _IdObjeto;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[0]")]
	    public Int64 IdObjeto
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

	    private string _DescObjeto;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Classe BO", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(60)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey['']")]
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

	    private string _ClasseNome;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
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

	public partial class LookUpTcsTransacaoMenuChildTcsModuloMenu 
	{
		
	    #region Data Properties	
	 


	    private string _DescModulo;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Módulo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    [Display(Name = "Aplicativo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(60)]
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

	    private string _DescModuloMenu;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Menu", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(60)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey['']")]
	    public string DescModuloMenu
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

	    private Int64 _IdModulo;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
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

	    private Int64 _IdModuloMenu;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 4, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[0]")]
	    public Int64 IdModuloMenu
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
	

}