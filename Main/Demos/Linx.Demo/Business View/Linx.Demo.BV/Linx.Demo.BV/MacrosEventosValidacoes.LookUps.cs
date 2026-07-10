

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

using Linx.Demo.BM;

namespace Linx.Demo.BV.MacrosEventosValidacoes
{
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[Look Up Cliente];DisplayName[Look Up Cliente];Height[0];Width[0];EdmEntityName[EntityAdapter1_Cliente]")]	

	public partial class LookUpEntityAdapter1Cliente 
	{
		
	    #region Data Properties	
	 


	    private String _Cliente;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cliente", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[[CRM_PFJ].[CLIENTE].[CLIENTE]]")]
	    public String Cliente
	    {
	    	    get
	    	    {
	    	          return _Cliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._Cliente != value)
	    	          {
	    	              this._Cliente = value;
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
	[FunctionalPoint("ClassDescription[Look Up CodLoja];DisplayName[Look Up CodLoja];Height[0];Width[0];EdmEntityName[EntityAdapter1_CodLoja]")]	

	public partial class LookUpEntityAdapter1CodLoja 
	{
		
	    #region Data Properties	
	 


	    private String _CodLoja;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cod Loja", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[[LJV_LOJA].[COD_LOJA].[COD_LOJA]]")]
	    public String CodLoja
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

	    #endregion Data Properties	

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[Look Up Data];DisplayName[Look Up Data];Height[0];Width[0];EdmEntityName[EntityAdapter1_Data]")]	

	public partial class LookUpEntityAdapter1Data 
	{
		
	    #region Data Properties	
	 


	    private DateTime _Data;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Data", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[[DATAS].[DATA].[DATA]]")]
	    public DateTime Data
	    {
	    	    get
	    	    {
	    	          return _Data;
	    	    }
	    	    set
	    	    {
	    	          if (this._Data != value)
	    	          {
	    	              this._Data = value;
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
	[FunctionalPoint("ClassDescription[Look Up IdBandeiraRede];DisplayName[Look Up IdBandeiraRede];Height[0];Width[0];EdmEntityName[EntityAdapter1_IdBandeiraRede]")]	

	public partial class LookUpEntityAdapter1IdBandeiraRede 
	{
		
	    #region Data Properties	
	 


	    private Int64 _IdBandeiraRede;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Bandeira Rede", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[[TBC_BANDEIRA_REDE].[ID_BANDEIRA_REDE].[ID_BANDEIRA_REDE]]")]
	    public Int64 IdBandeiraRede
	    {
	    	    get
	    	    {
	    	          return _IdBandeiraRede;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdBandeiraRede != value)
	    	          {
	    	              this._IdBandeiraRede = value;
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
	[FunctionalPoint("ClassDescription[Look Up Loja];DisplayName[Look Up Loja];Height[0];Width[0];EdmEntityName[EntityAdapter1_Loja]")]	

	public partial class LookUpEntityAdapter1Loja 
	{
		
	    #region Data Properties	
	 


	    private String _Loja;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Loja", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[[LJV_LOJA].[LOJA].[LOJA]]")]
	    public String Loja
	    {
	    	    get
	    	    {
	    	          return _Loja;
	    	    }
	    	    set
	    	    {
	    	          if (this._Loja != value)
	    	          {
	    	              this._Loja = value;
	    	          }
	    	    }
	    }	

	    #endregion Data Properties	

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	

}