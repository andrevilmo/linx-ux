

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

namespace Linx.Framework.BV.UsuarioFranquia
{
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[Look Up TCS_PERFIL];DisplayName[Look Up TCS_PERFIL];Height[0];Width[0];EdmEntityName[TCS_PERFIL]")]	

	public partial class LookUpTcsPerfil 
	{
		
	    #region Data Properties	
	 


	    private System.String _DescPerfil;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Perfil", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(60)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_PERFIL.DESC_PERFIL]")]
	    public System.String DescPerfil
	    {
	    	    get
	    	    {
	    	          return _DescPerfil;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescPerfil != value)
	    	          {
	    	              this._DescPerfil = value;
	    	          }
	    	    }
	    }

	    private Int64 _IdPerfil;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Perfil", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_PERFIL.ID_PERFIL]")]
	    public Int64 IdPerfil
	    {
	    	    get
	    	    {
	    	          return _IdPerfil;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdPerfil != value)
	    	          {
	    	              this._IdPerfil = value;
	    	          }
	    	    }
	    }

	    private Boolean _Inativo;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inativo", Description="", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[CheckBox];FilterDataKey[TCS_PERFIL.INATIVO]")]
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

	    private int _IdLinxPerfil;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_PERFIL.ID_LINX]")]
	    public int IdLinxPerfil
	    {
	    	    get
	    	    {
	    	          return _IdLinxPerfil;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLinxPerfil != value)
	    	          {
	    	              this._IdLinxPerfil = value;
	    	          }
	    	    }
	    }	

	    #endregion Data Properties	

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	

}