

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
using System.ServiceModel;
using System.Linq.Expressions;
using Linx.LinqExtensions.Query;
using Linx.LinqExtensions.Functional;
using Linx.LinqExtensions.Expressions;
using System.Data.Linq.SqlClient;
using System.Reflection;
using System.Data.Entity.Core.Objects.DataClasses;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

using Linx.Demo.BM;

namespace VAREJO.BV.MestreDetalheSubDetalhes
{
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[Look Up ESTADO];DisplayName[Look Up ESTADO];Height[0];Width[0];EdmEntityName[ESTADO]")]	

	public partial class LookUpEstado 
	{
		
	    #region Data Properties	
	 


	    private System.Nullable<Int32> _IdEstado;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Estado", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[ESTADO.ID_ESTADO]")]
	    public System.Nullable<Int32> IdEstado
	    {
	    	    get
	    	    {
	    	          return _IdEstado;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdEstado != value)
	    	          {
	    	              this._IdEstado = value;
	    	          }
	    	    }
	    }

	    private System.Nullable<Int32> _IdPais;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Pais", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[ESTADO.PAIS.ID_PAIS]")]
	    public System.Nullable<Int32> IdPais
	    {
	    	    get
	    	    {
	    	          return _IdPais;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdPais != value)
	    	          {
	    	              this._IdPais = value;
	    	          }
	    	    }
	    }

	    private System.String _StringPais;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Pais", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(50)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[ESTADO.PAIS.STRING_PAIS]")]
	    public System.String StringPais
	    {
	    	    get
	    	    {
	    	          return _StringPais;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringPais != value)
	    	          {
	    	              this._StringPais = value;
	    	          }
	    	    }
	    }

	    private System.String _StringEstado;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Estado", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(50)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[ESTADO.STRING_ESTADO]")]
	    public System.String StringEstado
	    {
	    	    get
	    	    {
	    	          return _StringEstado;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringEstado != value)
	    	          {
	    	              this._StringEstado = value;
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
	[FunctionalPoint("ClassDescription[Look Up LOJA];DisplayName[Look Up LOJA];Height[0];Width[0];EdmEntityName[LOJA]")]	

	public partial class LookUpLoja 
	{
		
	    #region Data Properties	
	 


	    private System.Nullable<Int32> _IdLoja;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Loja", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[LOJA.ID_LOJA]")]
	    public System.Nullable<Int32> IdLoja
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

	    private System.String _StringLoja;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Loja", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(50)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[LOJA.STRING_LOJA]")]
	    public System.String StringLoja
	    {
	    	    get
	    	    {
	    	          return _StringLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringLoja != value)
	    	          {
	    	              this._StringLoja = value;
	    	          }
	    	    }
	    }	

	    #endregion Data Properties	

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	

}