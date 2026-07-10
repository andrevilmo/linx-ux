

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

using LinxTraining001.BM;

namespace LinxTraining001.BV.Product01
{
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[Look Up ProductModel];DisplayName[Look Up ProductModel];Height[0];Width[0];EdmEntityName[ProductModel]")]	

	public partial class LookUpProductModel 
	{
		
	    #region Data Properties	
	 


	    private System.Nullable<Int32> _ProductModelID;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ProductModelID", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[ProductModel.ProductModelID]")]
	    public System.Nullable<Int32> ProductModelID
	    {
	    	    get
	    	    {
	    	          return _ProductModelID;
	    	    }
	    	    set
	    	    {
	    	          if (this._ProductModelID != value)
	    	          {
	    	              this._ProductModelID = value;
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
	[FunctionalPoint("ClassDescription[Look Up ProductSubcategory];DisplayName[Look Up ProductSubcategory];Height[0];Width[0];EdmEntityName[ProductSubcategory]")]	

	public partial class LookUpProductSubcategory 
	{
		
	    #region Data Properties	
	 


	    private System.Nullable<Int32> _ProductSubcategoryID;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ProductSubcategoryID", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[ProductSubcategory.ProductSubcategoryID]")]
	    public System.Nullable<Int32> ProductSubcategoryID
	    {
	    	    get
	    	    {
	    	          return _ProductSubcategoryID;
	    	    }
	    	    set
	    	    {
	    	          if (this._ProductSubcategoryID != value)
	    	          {
	    	              this._ProductSubcategoryID = value;
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
	[FunctionalPoint("ClassDescription[Look Up UnitMeasure];DisplayName[Look Up UnitMeasure];Height[0];Width[0];EdmEntityName[UnitMeasure]")]	

	public partial class LookUpUnitMeasure 
	{
		
	    #region Data Properties	
	 


	    private System.String _UnitMeasureCode;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "UnitMeasureCode", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(3)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[UnitMeasure.UnitMeasureCode]")]
	    public System.String UnitMeasureCode
	    {
	    	    get
	    	    {
	    	          return _UnitMeasureCode;
	    	    }
	    	    set
	    	    {
	    	          if (this._UnitMeasureCode != value)
	    	          {
	    	              this._UnitMeasureCode = value;
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
	[FunctionalPoint("ClassDescription[Look Up UnitMeasure];DisplayName[Look Up UnitMeasure];Height[0];Width[0];EdmEntityName[UnitMeasure]")]	

	public partial class LookUpUnitMeasure1 
	{
		
	    #region Data Properties	
	 


	    private System.String _UnitMeasureCode1;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "UnitMeasureCode1", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(3)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[UnitMeasure.UnitMeasureCode]")]
	    public System.String UnitMeasureCode1
	    {
	    	    get
	    	    {
	    	          return _UnitMeasureCode1;
	    	    }
	    	    set
	    	    {
	    	          if (this._UnitMeasureCode1 != value)
	    	          {
	    	              this._UnitMeasureCode1 = value;
	    	          }
	    	    }
	    }	

	    #endregion Data Properties	

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	

}