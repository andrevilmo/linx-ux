

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

namespace Linx.Framework.BV.GrupoEconomico
{
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[Look Up TCS_MOEDA_INDICADOR];DisplayName[Look Up TCS_MOEDA_INDICADOR];Height[0];Width[0];EdmEntityName[TCS_MOEDA_INDICADOR]")]	

	public partial class LookUpTcsMoedaIndicador 
	{
		
	    #region Data Properties	
	 


	    private Int16 _IdMoedaIndicador;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Moeda Indicador", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_MOEDA_INDICADOR.ID_MOEDA_INDICADOR]")]
	    public Int16 IdMoedaIndicador
	    {
	    	    get
	    	    {
	    	          return _IdMoedaIndicador;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdMoedaIndicador != value)
	    	          {
	    	              this._IdMoedaIndicador = value;
	    	          }
	    	    }
	    }

	    private System.String _NomeMoeda;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Moeda", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(25)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_MOEDA_INDICADOR.NOME_MOEDA]")]
	    public System.String NomeMoeda
	    {
	    	    get
	    	    {
	    	          return _NomeMoeda;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeMoeda != value)
	    	          {
	    	              this._NomeMoeda = value;
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
	 


	    private System.String _NomeUsuario;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_USUARIO.NOME_USUARIO]")]
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

	    private System.Guid _UidUsuario;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Usuario", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_USUARIO.UID_USUARIO]")]
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

	    private Int64 _IdUsuario;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_USUARIO.ID_USUARIO]")]
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

	    #endregion Data Properties	

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	

}