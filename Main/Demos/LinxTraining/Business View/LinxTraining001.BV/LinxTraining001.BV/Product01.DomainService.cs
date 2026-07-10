					
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.Entity.Core.Objects;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Linq.Expressions;
using Linx.LinqExtensions.Functional;
using Linx.LinqExtensions.Expressions;
using System.Data.Linq.SqlClient;
using System.Reflection;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Transactions;
using System.Xml.Serialization;
using System.ServiceModel.DomainServices.Server;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices;
using System.ComponentModel.Composition;
using Linx;
using Linx.Data;
using Linx.Tools;
using Linx.LinqExtensions.Dynamic;
using Linx.LinqExtensions.Query;
using LinxTraining001.BM;

namespace LinxTraining001.BV.Product01
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="Product.ProductID", IsUpdatable=false, EdmName="LinxTraining001.BM.BMLFWTraining")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[ProductView];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[ProductID];ReadOnly[false];Entities[Product:ProductID|ProductModel:ProductModelID|ProductSubcategory:ProductSubcategoryID];SubQueryInfo[];EdmEntityName[Product];EntityRelations[ProductModel(ProductModel)#ProductSubcategory(ProductSubcategory)#ProductCategory(ProductCategory)#UnitMeasure(UnitMeasure)#UnitMeasure1(UnitMeasure)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "ProductView")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "LinxTraining001.BV.Product01.ProductView")]
	public partial class ProductView : Linx.Data.Entity
	{

	
		
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	    }

	    #endregion Flat Entities

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For Class
	    partial void OnClassChanging(System.String value);
	    partial void OnClassChanged();

	    private System.String _Class;

	    [DataMember(Name = "Class", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Class", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(2)]
	    [FunctionalPoint("Precision[2:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[Color];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[Product.Class];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Product.Class")]
	    public System.String Class
	    {
	    	    get
	    	    {
	    	          return _Class;
	    	    }
	    	    set
	    	    {
	    	          if (this._Class != value)
	    	          {
	    	              this.ValidateProperty("Class", value);
	    	              this.OnClassChanging(value);
	    	              this.RaiseDataMemberChanging("Class");
	    	              this._Class = value;
	    	              this.RaiseDataMemberChanged("Class");
	    	              this.OnClassChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Color
	    partial void OnColorChanging(System.String value);
	    partial void OnColorChanged();

	    private System.String _Color;

	    [DataMember(Name = "Color", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Color", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(15)]
	    [FunctionalPoint("Precision[15:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[Product.Color];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Product.Color")]
	    public System.String Color
	    {
	    	    get
	    	    {
	    	          return _Color;
	    	    }
	    	    set
	    	    {
	    	          if (this._Color != value)
	    	          {
	    	              this.ValidateProperty("Color", value);
	    	              this.OnColorChanging(value);
	    	              this.RaiseDataMemberChanging("Color");
	    	              this._Color = value;
	    	              this.RaiseDataMemberChanged("Color");
	    	              this.OnColorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DaysToManufacture
	    partial void OnDaysToManufactureChanging(Int32 value);
	    partial void OnDaysToManufactureChanged();

	    private Int32 _DaysToManufacture;

	    [DataMember(IsRequired = true, Name = "DaysToManufacture", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "DaysToManufacture", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[Product.DaysToManufacture];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Product.DaysToManufacture")]
	    public Int32 DaysToManufacture
	    {
	    	    get
	    	    {
	    	          return _DaysToManufacture;
	    	    }
	    	    set
	    	    {
	    	          if (this._DaysToManufacture != value)
	    	          {
	    	              this.ValidateProperty("DaysToManufacture", value);
	    	              this.OnDaysToManufactureChanging(value);
	    	              this.RaiseDataMemberChanging("DaysToManufacture");
	    	              this._DaysToManufacture = value;
	    	              this.RaiseDataMemberChanged("DaysToManufacture");
	    	              this.OnDaysToManufactureChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DiscontinuedDate
	    partial void OnDiscontinuedDateChanging(System.Nullable<System.DateTime> value);
	    partial void OnDiscontinuedDateChanged();

	    private System.Nullable<System.DateTime> _DiscontinuedDate;

	    [DataMember(Name = "DiscontinuedDate", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "DiscontinuedDate", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[Product.DiscontinuedDate];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Product.DiscontinuedDate")]
	    public System.Nullable<System.DateTime> DiscontinuedDate
	    {
	    	    get
	    	    {
	    	          return _DiscontinuedDate;
	    	    }
	    	    set
	    	    {
	    	          if (this._DiscontinuedDate != value)
	    	          {
	    	              this.ValidateProperty("DiscontinuedDate", value);
	    	              this.OnDiscontinuedDateChanging(value);
	    	              this.RaiseDataMemberChanging("DiscontinuedDate");
	    	              this._DiscontinuedDate = value;
	    	              this.RaiseDataMemberChanged("DiscontinuedDate");
	    	              this.OnDiscontinuedDateChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For FinishedGoodsFlag
	    partial void OnFinishedGoodsFlagChanging(Boolean value);
	    partial void OnFinishedGoodsFlagChanged();

	    private Boolean _FinishedGoodsFlag;

	    [DataMember(IsRequired = true, Name = "FinishedGoodsFlag", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "FinishedGoodsFlag", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[Product.FinishedGoodsFlag];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Product.FinishedGoodsFlag")]
	    public Boolean FinishedGoodsFlag
	    {
	    	    get
	    	    {
	    	          return _FinishedGoodsFlag;
	    	    }
	    	    set
	    	    {
	    	          if (this._FinishedGoodsFlag != value)
	    	          {
	    	              this.ValidateProperty("FinishedGoodsFlag", value);
	    	              this.OnFinishedGoodsFlagChanging(value);
	    	              this.RaiseDataMemberChanging("FinishedGoodsFlag");
	    	              this._FinishedGoodsFlag = value;
	    	              this.RaiseDataMemberChanged("FinishedGoodsFlag");
	    	              this.OnFinishedGoodsFlagChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ListPrice
	    partial void OnListPriceChanging(System.Decimal value);
	    partial void OnListPriceChanged();

	    private System.Decimal _ListPrice;

	    [DataMember(IsRequired = true, Name = "ListPrice", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ListPrice", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[19:4];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N4];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[Product.ListPrice];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Product.ListPrice")]
	    public System.Decimal ListPrice
	    {
	    	    get
	    	    {
	    	          return _ListPrice;
	    	    }
	    	    set
	    	    {
	    	          if (this._ListPrice != value)
	    	          {
	    	              this.ValidateProperty("ListPrice", value);
	    	              this.OnListPriceChanging(value);
	    	              this.RaiseDataMemberChanging("ListPrice");
	    	              this._ListPrice = value;
	    	              this.RaiseDataMemberChanged("ListPrice");
	    	              this.OnListPriceChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For MakeFlag
	    partial void OnMakeFlagChanging(Boolean value);
	    partial void OnMakeFlagChanged();

	    private Boolean _MakeFlag;

	    [DataMember(IsRequired = true, Name = "MakeFlag", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "MakeFlag", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[Product.MakeFlag];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Product.MakeFlag")]
	    public Boolean MakeFlag
	    {
	    	    get
	    	    {
	    	          return _MakeFlag;
	    	    }
	    	    set
	    	    {
	    	          if (this._MakeFlag != value)
	    	          {
	    	              this.ValidateProperty("MakeFlag", value);
	    	              this.OnMakeFlagChanging(value);
	    	              this.RaiseDataMemberChanging("MakeFlag");
	    	              this._MakeFlag = value;
	    	              this.RaiseDataMemberChanged("MakeFlag");
	    	              this.OnMakeFlagChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ModifiedDate
	    partial void OnModifiedDateChanging(System.DateTime value);
	    partial void OnModifiedDateChanged();

	    private System.DateTime _ModifiedDate;

	    [DataMember(IsRequired = true, Name = "ModifiedDate", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ModifiedDate", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[Product.ModifiedDate];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Product.ModifiedDate")]
	    public System.DateTime ModifiedDate
	    {
	    	    get
	    	    {
	    	          return _ModifiedDate;
	    	    }
	    	    set
	    	    {
	    	          if (this._ModifiedDate != value)
	    	          {
	    	              this.ValidateProperty("ModifiedDate", value);
	    	              this.OnModifiedDateChanging(value);
	    	              this.RaiseDataMemberChanging("ModifiedDate");
	    	              this._ModifiedDate = value;
	    	              this.RaiseDataMemberChanged("ModifiedDate");
	    	              this.OnModifiedDateChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Name
	    partial void OnNameChanging(System.String value);
	    partial void OnNameChanged();

	    private System.String _Name;

	    [DataMember(IsRequired = true, Name = "Name", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Name", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[Product.Name];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Product.Name")]
	    public System.String Name
	    {
	    	    get
	    	    {
	    	          return _Name;
	    	    }
	    	    set
	    	    {
	    	          if (this._Name != value)
	    	          {
	    	              this.ValidateProperty("Name", value);
	    	              this.OnNameChanging(value);
	    	              this.RaiseDataMemberChanging("Name");
	    	              this._Name = value;
	    	              this.RaiseDataMemberChanged("Name");
	    	              this.OnNameChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ProductID
	    partial void OnProductIDChanging(Int32 value);
	    partial void OnProductIDChanged();

	    private Int32 _ProductID;

	    [DataMember(IsRequired = true, Name = "ProductID", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ProductID", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[Product.ProductID];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Product.ProductID")]
	    public Int32 ProductID
	    {
	    	    get
	    	    {
	    	          return _ProductID;
	    	    }
	    	    set
	    	    {
	    	          if (this._ProductID != value)
	    	          {
	    	              this.ValidateProperty("ProductID", value);
	    	              this.OnProductIDChanging(value);
	    	              this.RaiseDataMemberChanging("ProductID");
	    	              this._ProductID = value;
	    	              this.RaiseDataMemberChanged("ProductID");
	    	              this.OnProductIDChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ProductLine
	    partial void OnProductLineChanging(System.String value);
	    partial void OnProductLineChanged();

	    private System.String _ProductLine;

	    [DataMember(Name = "ProductLine", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ProductLine", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(2)]
	    [FunctionalPoint("Precision[2:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[Product.ProductLine];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Product.ProductLine")]
	    public System.String ProductLine
	    {
	    	    get
	    	    {
	    	          return _ProductLine;
	    	    }
	    	    set
	    	    {
	    	          if (this._ProductLine != value)
	    	          {
	    	              this.ValidateProperty("ProductLine", value);
	    	              this.OnProductLineChanging(value);
	    	              this.RaiseDataMemberChanging("ProductLine");
	    	              this._ProductLine = value;
	    	              this.RaiseDataMemberChanged("ProductLine");
	    	              this.OnProductLineChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ProductModelID
	    partial void OnProductModelIDChanging(System.Nullable<Int32> value);
	    partial void OnProductModelIDChanged();

	    private System.Nullable<Int32> _ProductModelID;

	    [DataMember(Name = "ProductModelID", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ProductModelID", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpProductModel];LookUpTitle[Seleção de (ProductModelID)];LookUpQuery[executeLookUpProductModel];LookUpFinalize[finalizeLookUpProductModel];LookUpDisplayColumns[{\"ProductModelID\" : \"ProductModelID\"}];LookUpColumns[{\"ProductModelID\" : true}];FilterDataKey[Product.ProductModel.ProductModelID];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<Int32>#ProductModelID#true##12:0##ProductModelID#0#true##::LookUpProductModel##false#false#ProductModel#ProductModel#LinxTraining001.BV.Product01#IQueryable###true#false", EdmKey="Product.ProductModel.ProductModelID")]
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
	    	              this.ValidateProperty("ProductModelID", value);
	    	              this.OnProductModelIDChanging(value);
	    	              this.RaiseDataMemberChanging("ProductModelID");
	    	              this._ProductModelID = value;
	    	              this.RaiseDataMemberChanged("ProductModelID");
	    	              this.OnProductModelIDChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ProductNumber
	    partial void OnProductNumberChanging(System.String value);
	    partial void OnProductNumberChanged();

	    private System.String _ProductNumber;

	    [DataMember(IsRequired = true, Name = "ProductNumber", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ProductNumber", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(25)]
	    [FunctionalPoint("Precision[25:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[Product.ProductNumber];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Product.ProductNumber")]
	    public System.String ProductNumber
	    {
	    	    get
	    	    {
	    	          return _ProductNumber;
	    	    }
	    	    set
	    	    {
	    	          if (this._ProductNumber != value)
	    	          {
	    	              this.ValidateProperty("ProductNumber", value);
	    	              this.OnProductNumberChanging(value);
	    	              this.RaiseDataMemberChanging("ProductNumber");
	    	              this._ProductNumber = value;
	    	              this.RaiseDataMemberChanged("ProductNumber");
	    	              this.OnProductNumberChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ProductSubcategoryID
	    partial void OnProductSubcategoryIDChanging(System.Nullable<Int32> value);
	    partial void OnProductSubcategoryIDChanged();

	    private System.Nullable<Int32> _ProductSubcategoryID;

	    [DataMember(Name = "ProductSubcategoryID", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ProductSubcategoryIDOWERTJERTBEWRGKBEWRKTGBEWKRBTGKEWRTBGKELWRG", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpProductSubcategory];LookUpTitle[Seleção de (ProductSubcategoryIDOWERTJERTBEWRGKBEWRKTGBEWKRBTGKEWRTBGKELWRG)];LookUpQuery[executeLookUpProductSubcategory];LookUpFinalize[finalizeLookUpProductSubcategory];LookUpDisplayColumns[{\"ProductSubcategoryID\" : \"ProductSubcategoryID\"}];LookUpColumns[{\"ProductSubcategoryID\" : true}];FilterDataKey[Product.ProductSubcategory.ProductSubcategoryID];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<Int32>#ProductSubcategoryID#true##12:0##ProductSubcategoryID#0#true##::LookUpProductSubcategory##false#false#ProductSubcategory#ProductSubcategory#LinxTraining001.BV.Product01#IQueryable###true#false", EdmKey="Product.ProductSubcategory.ProductSubcategoryID")]
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
	    	              this.ValidateProperty("ProductSubcategoryID", value);
	    	              this.OnProductSubcategoryIDChanging(value);
	    	              this.RaiseDataMemberChanging("ProductSubcategoryID");
	    	              this._ProductSubcategoryID = value;
	    	              this.RaiseDataMemberChanged("ProductSubcategoryID");
	    	              this.OnProductSubcategoryIDChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ReorderPoint
	    partial void OnReorderPointChanging(Int16 value);
	    partial void OnReorderPointChanged();

	    private Int16 _ReorderPoint;

	    [DataMember(IsRequired = true, Name = "ReorderPoint", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ReorderPoint", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[Product.ReorderPoint];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Product.ReorderPoint")]
	    public Int16 ReorderPoint
	    {
	    	    get
	    	    {
	    	          return _ReorderPoint;
	    	    }
	    	    set
	    	    {
	    	          if (this._ReorderPoint != value)
	    	          {
	    	              this.ValidateProperty("ReorderPoint", value);
	    	              this.OnReorderPointChanging(value);
	    	              this.RaiseDataMemberChanging("ReorderPoint");
	    	              this._ReorderPoint = value;
	    	              this.RaiseDataMemberChanged("ReorderPoint");
	    	              this.OnReorderPointChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Rowguid
	    partial void OnRowguidChanging(System.Guid value);
	    partial void OnRowguidChanged();

	    private System.Guid _Rowguid;

	    [DataMember(IsRequired = true, Name = "Rowguid", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Rowguid", Description="", Order = 14, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[Product.rowguid];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Product.rowguid")]
	    public System.Guid Rowguid
	    {
	    	    get
	    	    {
	    	          return _Rowguid;
	    	    }
	    	    set
	    	    {
	    	          if (this._Rowguid != value)
	    	          {
	    	              this.ValidateProperty("Rowguid", value);
	    	              this.OnRowguidChanging(value);
	    	              this.RaiseDataMemberChanging("Rowguid");
	    	              this._Rowguid = value;
	    	              this.RaiseDataMemberChanged("Rowguid");
	    	              this.OnRowguidChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For SafetyStockLevel
	    partial void OnSafetyStockLevelChanging(Int16 value);
	    partial void OnSafetyStockLevelChanged();

	    private Int16 _SafetyStockLevel;

	    [DataMember(IsRequired = true, Name = "SafetyStockLevel", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "SafetyStockLevel", Description="", Order = 15, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[Product.SafetyStockLevel];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Product.SafetyStockLevel")]
	    public Int16 SafetyStockLevel
	    {
	    	    get
	    	    {
	    	          return _SafetyStockLevel;
	    	    }
	    	    set
	    	    {
	    	          if (this._SafetyStockLevel != value)
	    	          {
	    	              this.ValidateProperty("SafetyStockLevel", value);
	    	              this.OnSafetyStockLevelChanging(value);
	    	              this.RaiseDataMemberChanging("SafetyStockLevel");
	    	              this._SafetyStockLevel = value;
	    	              this.RaiseDataMemberChanged("SafetyStockLevel");
	    	              this.OnSafetyStockLevelChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For SellEndDate
	    partial void OnSellEndDateChanging(System.Nullable<System.DateTime> value);
	    partial void OnSellEndDateChanged();

	    private System.Nullable<System.DateTime> _SellEndDate;

	    [DataMember(Name = "SellEndDate", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "SellEndDate", Description="", Order = 16, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[Product.SellEndDate];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Product.SellEndDate")]
	    public System.Nullable<System.DateTime> SellEndDate
	    {
	    	    get
	    	    {
	    	          return _SellEndDate;
	    	    }
	    	    set
	    	    {
	    	          if (this._SellEndDate != value)
	    	          {
	    	              this.ValidateProperty("SellEndDate", value);
	    	              this.OnSellEndDateChanging(value);
	    	              this.RaiseDataMemberChanging("SellEndDate");
	    	              this._SellEndDate = value;
	    	              this.RaiseDataMemberChanged("SellEndDate");
	    	              this.OnSellEndDateChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For SellStartDate
	    partial void OnSellStartDateChanging(System.DateTime value);
	    partial void OnSellStartDateChanged();

	    private System.DateTime _SellStartDate;

	    [DataMember(IsRequired = true, Name = "SellStartDate", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "SellStartDate", Description="", Order = 17, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[Product.SellStartDate];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Product.SellStartDate")]
	    public System.DateTime SellStartDate
	    {
	    	    get
	    	    {
	    	          return _SellStartDate;
	    	    }
	    	    set
	    	    {
	    	          if (this._SellStartDate != value)
	    	          {
	    	              this.ValidateProperty("SellStartDate", value);
	    	              this.OnSellStartDateChanging(value);
	    	              this.RaiseDataMemberChanging("SellStartDate");
	    	              this._SellStartDate = value;
	    	              this.RaiseDataMemberChanged("SellStartDate");
	    	              this.OnSellStartDateChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Size
	    partial void OnSizeChanging(System.String value);
	    partial void OnSizeChanged();

	    private System.String _Size;

	    [DataMember(Name = "Size", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Size", Description="", Order = 18, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(5)]
	    [FunctionalPoint("Precision[5:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[Product.Size];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Product.Size")]
	    public System.String Size
	    {
	    	    get
	    	    {
	    	          return _Size;
	    	    }
	    	    set
	    	    {
	    	          if (this._Size != value)
	    	          {
	    	              this.ValidateProperty("Size", value);
	    	              this.OnSizeChanging(value);
	    	              this.RaiseDataMemberChanging("Size");
	    	              this._Size = value;
	    	              this.RaiseDataMemberChanged("Size");
	    	              this.OnSizeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StandardCost
	    partial void OnStandardCostChanging(System.Decimal value);
	    partial void OnStandardCostChanged();

	    private System.Decimal _StandardCost;

	    [DataMember(IsRequired = true, Name = "StandardCost", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "StandardCost", Description="", Order = 19, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[19:4];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N4];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[Product.StandardCost];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Product.StandardCost")]
	    public System.Decimal StandardCost
	    {
	    	    get
	    	    {
	    	          return _StandardCost;
	    	    }
	    	    set
	    	    {
	    	          if (this._StandardCost != value)
	    	          {
	    	              this.ValidateProperty("StandardCost", value);
	    	              this.OnStandardCostChanging(value);
	    	              this.RaiseDataMemberChanging("StandardCost");
	    	              this._StandardCost = value;
	    	              this.RaiseDataMemberChanged("StandardCost");
	    	              this.OnStandardCostChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Style
	    partial void OnStyleChanging(System.String value);
	    partial void OnStyleChanged();

	    private System.String _Style;

	    [DataMember(Name = "Style", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Style", Description="", Order = 20, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(2)]
	    [FunctionalPoint("Precision[2:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[Product.Style];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Product.Style")]
	    public System.String Style
	    {
	    	    get
	    	    {
	    	          return _Style;
	    	    }
	    	    set
	    	    {
	    	          if (this._Style != value)
	    	          {
	    	              this.ValidateProperty("Style", value);
	    	              this.OnStyleChanging(value);
	    	              this.RaiseDataMemberChanging("Style");
	    	              this._Style = value;
	    	              this.RaiseDataMemberChanged("Style");
	    	              this.OnStyleChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UnitMeasureCode
	    partial void OnUnitMeasureCodeChanging(System.String value);
	    partial void OnUnitMeasureCodeChanged();

	    private System.String _UnitMeasureCode;

	    [DataMember(Name = "UnitMeasureCode", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "UnitMeasureCodeLIJASKDFKHASDF", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(3)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[ProductSubcategoryID];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpUnitMeasure];LookUpTitle[Seleção de (UnitMeasureCodeLIJASKDFKHASDF)];LookUpQuery[executeLookUpUnitMeasure];LookUpFinalize[finalizeLookUpUnitMeasure];LookUpDisplayColumns[{\"UnitMeasureCode\" : \"UnitMeasureCode\"}];LookUpColumns[{\"UnitMeasureCode\" : true}];FilterDataKey[Product.UnitMeasure.UnitMeasureCode];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#UnitMeasureCode#true##3:0##UnitMeasureCode#0#true##::LookUpUnitMeasure##false#false#UnitMeasure#UnitMeasure#LinxTraining001.BV.Product01#IQueryable###true#false", EdmKey="Product.UnitMeasure.UnitMeasureCode")]
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
	    	              this.ValidateProperty("UnitMeasureCode", value);
	    	              this.OnUnitMeasureCodeChanging(value);
	    	              this.RaiseDataMemberChanging("UnitMeasureCode");
	    	              this._UnitMeasureCode = value;
	    	              this.RaiseDataMemberChanged("UnitMeasureCode");
	    	              this.OnUnitMeasureCodeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UnitMeasureCode1
	    partial void OnUnitMeasureCode1Changing(System.String value);
	    partial void OnUnitMeasureCode1Changed();

	    private System.String _UnitMeasureCode1;

	    [DataMember(Name = "UnitMeasureCode1", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "UnitMeasureCode1", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(3)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpUnitMeasure1];LookUpTitle[Seleção de (UnitMeasureCode1)];LookUpQuery[executeLookUpUnitMeasure1];LookUpFinalize[finalizeLookUpUnitMeasure1];LookUpDisplayColumns[{\"UnitMeasureCode1\" : \"UnitMeasureCode1\"}];LookUpColumns[{\"UnitMeasureCode1\" : true}];FilterDataKey[Product.UnitMeasure1.UnitMeasureCode];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#UnitMeasureCode1#true##3:0##UnitMeasureCode1#0#true##::LookUpUnitMeasure1##false#false#UnitMeasure1#UnitMeasure#LinxTraining001.BV.Product01#IQueryable###true#false", EdmKey="Product.UnitMeasure1.UnitMeasureCode")]
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
	    	              this.ValidateProperty("UnitMeasureCode1", value);
	    	              this.OnUnitMeasureCode1Changing(value);
	    	              this.RaiseDataMemberChanging("UnitMeasureCode1");
	    	              this._UnitMeasureCode1 = value;
	    	              this.RaiseDataMemberChanged("UnitMeasureCode1");
	    	              this.OnUnitMeasureCode1Changed();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Weight
	    partial void OnWeightChanging(System.Nullable<System.Decimal> value);
	    partial void OnWeightChanged();

	    private System.Nullable<System.Decimal> _Weight;

	    [DataMember(Name = "Weight", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Weight", Description="", Order = 21, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[8:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[Product.Weight];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Product.Weight")]
	    public System.Nullable<System.Decimal> Weight
	    {
	    	    get
	    	    {
	    	          return _Weight;
	    	    }
	    	    set
	    	    {
	    	          if (this._Weight != value)
	    	          {
	    	              this.ValidateProperty("Weight", value);
	    	              this.OnWeightChanging(value);
	    	              this.RaiseDataMemberChanging("Weight");
	    	              this._Weight = value;
	    	              this.RaiseDataMemberChanged("Weight");
	    	              this.OnWeightChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryProductID;
	    [DataMember(Name = "TemporaryProductID", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ProductID (Tmp)", Description="Temporary Key", Order = 10, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryProductID
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryProductID.IsNullOrEmpty())
	    	                this._TemporaryProductID = this._ProductID;
	    	          return this._TemporaryProductID;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryProductID != value)
	    	              this._TemporaryProductID = value;
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "BMLFWTraining.Product").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(LinxTraining001.BM.Product), QualifiedEntitySetName = "BMLFWTraining.Product" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Product.Name", Source = "Name", Target = "Name", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMLFWTraining.Product", RelationPropertyName = "Product" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Product.Size", Source = "Size", Target = "Size", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMLFWTraining.Product", RelationPropertyName = "Product" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Product.Class", Source = "Class", Target = "Class", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMLFWTraining.Product", RelationPropertyName = "Product" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Product.Color", Source = "Color", Target = "Color", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMLFWTraining.Product", RelationPropertyName = "Product" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Product.Style", Source = "Style", Target = "Style", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMLFWTraining.Product", RelationPropertyName = "Product" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Product.Weight", Source = "Weight", Target = "Weight", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMLFWTraining.Product", RelationPropertyName = "Product" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Product.rowguid", Source = "Rowguid", Target = "rowguid", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMLFWTraining.Product", RelationPropertyName = "Product" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Product.MakeFlag", Source = "MakeFlag", Target = "MakeFlag", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMLFWTraining.Product", RelationPropertyName = "Product" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Product.ListPrice", Source = "ListPrice", Target = "ListPrice", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMLFWTraining.Product", RelationPropertyName = "Product" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Product.ProductID", Source = "ProductID", Target = "ProductID", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "BMLFWTraining.Product", RelationPropertyName = "Product" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Product.ProductLine", Source = "ProductLine", Target = "ProductLine", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMLFWTraining.Product", RelationPropertyName = "Product" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Product.SellEndDate", Source = "SellEndDate", Target = "SellEndDate", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMLFWTraining.Product", RelationPropertyName = "Product" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Product.ModifiedDate", Source = "ModifiedDate", Target = "ModifiedDate", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMLFWTraining.Product", RelationPropertyName = "Product" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Product.ReorderPoint", Source = "ReorderPoint", Target = "ReorderPoint", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMLFWTraining.Product", RelationPropertyName = "Product" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Product.StandardCost", Source = "StandardCost", Target = "StandardCost", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMLFWTraining.Product", RelationPropertyName = "Product" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Product.ProductNumber", Source = "ProductNumber", Target = "ProductNumber", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMLFWTraining.Product", RelationPropertyName = "Product" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Product.SellStartDate", Source = "SellStartDate", Target = "SellStartDate", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMLFWTraining.Product", RelationPropertyName = "Product" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Product.DiscontinuedDate", Source = "DiscontinuedDate", Target = "DiscontinuedDate", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMLFWTraining.Product", RelationPropertyName = "Product" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Product.SafetyStockLevel", Source = "SafetyStockLevel", Target = "SafetyStockLevel", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMLFWTraining.Product", RelationPropertyName = "Product" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Product.DaysToManufacture", Source = "DaysToManufacture", Target = "DaysToManufacture", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMLFWTraining.Product", RelationPropertyName = "Product" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Product.FinishedGoodsFlag", Source = "FinishedGoodsFlag", Target = "FinishedGoodsFlag", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMLFWTraining.Product", RelationPropertyName = "Product" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Product.ProductModel.ProductModelID", Source = "ProductModelID", Target = "ProductModelID", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BMLFWTraining.ProductModel", RelationPropertyName = "ProductModel" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Product.UnitMeasure.UnitMeasureCode", Source = "UnitMeasureCode", Target = "UnitMeasureCode", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BMLFWTraining.UnitMeasure", RelationPropertyName = "UnitMeasure" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Product.UnitMeasure1.UnitMeasureCode", Source = "UnitMeasureCode1", Target = "UnitMeasureCode", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BMLFWTraining.UnitMeasure", RelationPropertyName = "UnitMeasure1" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Product.ProductSubcategory.ProductSubcategoryID", Source = "ProductSubcategoryID", Target = "ProductSubcategoryID", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BMLFWTraining.ProductSubcategory", RelationPropertyName = "ProductSubcategory" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	//////////////////////// DomainService Class V1 ///////////////////////
	///////////////////////////////////////////////////////////////////////
	[EnableClientAccess()]	
	[DomainIdentifier("ProcessorOverviewProduct01DomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class Product01DomainService : DomainService, IDataServiceContext 
	{
	
	
	    private bool[] _trueMetaCondition = new bool[] { true };
	    private bool[] _falseMetaCondition = new bool[] { };
	    partial void OnCreate();
	    private bool _isInitialized;
	    private bool _controlKeyMapping = false;
	    private List<DataKeyMapping> _keyMappings = new List<DataKeyMapping>();
	    private string connectionString;
	    public bool IsSecure { get; set; }
	    public Dictionary<string, string> Headers { get; set; }
	
	    #region SecurityHelper
	    private static ISecurityHelper _securityHelper;
	    [Ignore]
        private static ISecurityHelper SecurityHelper
        {
            get
            {
                if (_securityHelper == null)
                {
                    try { _securityHelper = ImplementationHelper<ISecurityHelper>.GetInstance("SecurityHelper", "Linx.Business.Tools"); }
                    catch { }
                }
                return _securityHelper;
            }
        }
	    #endregion

	
	    private LinxTraining001.BM.BMLFWTraining _dbContext;
	    protected LinxTraining001.BM.BMLFWTraining DbContext 
	    { 
	    	get 
	    	{
	        	if (this._dbContext == null)
	        	{
	        		this._dbContext = new LinxTraining001.BM.BMLFWTraining(connectionString, this.Headers);
	        		((System.Data.Entity.Infrastructure.IObjectContextAdapter)this._dbContext).ObjectContext.CommandTimeout = 180;
	        	}
	        	return this._dbContext;
	    	}
	    }

	    public string GetModelAssemblyName()
	    {
	        return typeof(LinxTraining001.BM.BMLFWTraining).Assembly.FullName;
	    }

	    public System.Data.Entity.Database Database
	    {
	        get { return this.DbContext.Database; }
	    }

		
	    public Product01DomainService() : this("", null, null){ }
	    public Product01DomainService(string connectionString) : this(connectionString, null, null) { }
	    public Product01DomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public Product01DomainService(LinxTraining001.BM.BMLFWTraining dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public Product01DomainService(string connectionString, LinxTraining001.BM.BMLFWTraining dataContext, Dictionary<string, string> headers) : base() 
	    { 
	    	this.connectionString = connectionString;
	    	this.Headers = headers;
	    	this._dbContext = dataContext; 


	    	this.OnCreate(); 
	    }

	    [Ignore]
	    public List<DataKeyMapping> SaveEntities(List<ChangeSetEntry> changeSetEntries)
	    {
	      if (changeSetEntries.Count == 0) return null;
	      
	      this.Initialize();
	      _keyMappings.Clear();
	      _controlKeyMapping = true;
	      this.Submit(new ChangeSet(changeSetEntries));
	      _controlKeyMapping = false;
	      return _keyMappings;
	    }

	    protected override int Count<T>(IQueryable<T> query)
	    {
	       return query.Count<T>();
	    }

	    public override void Initialize(DomainServiceContext context)
	    {
	       if (!_isInitialized)
	       {
	    		base.Initialize(context);
	    		this.AuthorizationContext = this.CreateAuthorizationContext();
	    		((System.Data.Entity.Infrastructure.IObjectContextAdapter)(object)this.DbContext).ObjectContext.ContextOptions.ProxyCreationEnabled = false;
	    		_isInitialized = true;
	       }
	    }
	
	    ChangeSet currentChangeSet = null;
	    [Ignore]
	    public ChangeSet GetChangeSet()
        {
          return this.currentChangeSet;
        }

	
	    [Ignore]
	    protected bool InvokeSaveChanges()
	    {
          try
          {
          	if (this._dbContext != null)
          		this._dbContext.SaveChanges();                
          }
          catch (Exception exp)
          {
          	throw new DomainException(exp.GetCompleteMessage("Fail by saving data:"));
          }
          return true;
	    }	

	    protected override void Dispose(bool disposing)
	    {
	      if (disposing)
	      {
	    		if (this._dbContext != null)
	    		{
	    			this._dbContext.Dispose();
	    		}
	      }
	      base.Dispose(disposing);
	    }

	    [Ignore]
	    public LinxTraining001.BM.BMLFWTraining GetEDM()
        {
          return this.DbContext;
        }	

			
	    [Ignore]	
	    public void AddCustomChanges(Entity changedEntity, Entity originalEntity, ChangeOperation operation)
	    {
	
 	        changedEntity.ApplyChanges(this.DbContext, originalEntity, operation, null);
	    }	
	
	    private int CurrentIdLinx(string connection)
        {
	        if(SecurityHelper.IsNull()) return 0;
            var idLinx = SecurityHelper.GetCurrentIdLinx(connection, this.Headers);
            return idLinx ?? 0;
        }
        private int CurrentIdGpEcon()
        {
	        if(SecurityHelper.IsNull()) return 0;
            var idGpEcon = SecurityHelper.GetCurrentIdGpecon(this.Headers);
            return idGpEcon ?? 0;
        }

	    [Ignore]	
	    public void SubmitData(DomainServiceContext context, Entity changedEntity, Entity originalEntity, ChangeOperation operation)
	    {
          var changeSetEntries = new ChangeSetEntry[] { new ChangeSetEntry(0, changedEntity, originalEntity, (DomainOperation)Enum.Parse(typeof(DomainOperation), operation.ToString())) { HasMemberChanges = true } };
          if (context == null) this.Initialize(); else this.Initialize(context);
          this.Submit(new ChangeSet(changeSetEntries));
	    }	

	    [Ignore]
	    public void SubmitData(DomainServiceContext context, List<EntityChange> entityChanges)
	    {
          if (entityChanges.Count == 0) return;
          List<ChangeSetEntry> changeSetEntries = new List<ChangeSetEntry>();
          for (int changeIndex = 0; changeIndex < entityChanges.Count; changeIndex++)
          {
              changeSetEntries.Add( new ChangeSetEntry(changeIndex, entityChanges[changeIndex].Entity, entityChanges[changeIndex].Original, (DomainOperation)Enum.Parse(typeof(DomainOperation), entityChanges[changeIndex].Operation.ToString())) { HasMemberChanges = true } );
          }
          if (context == null) this.Initialize(); else this.Initialize(context);
          this.Submit(new ChangeSet(changeSetEntries));
	    }
	
	    [Ignore]
	    public void SaveCustomChanges()
	    {
	        this.InvokeSaveChanges();
	    }		

	    #region Workflow Invoke Definitions
		


	    #endregion Workflow Invoke Definitions
	
	    #region KPI Informations
		


	    #endregion KPI Informations

	    #region Entity Event Call Definitions
	
	    private bool OnValidatingChanges(ChangeSet changeSet)
	    {
	
	
	        return true;
	    }

	    private void OnSavingChanges(ChangeSet changeSet)
	    {
	
		
	    }
	
	    private void SaveMedia(ChangeSet changeSet)
	    {
	    		foreach (ChangeSetEntry entry in changeSet.ChangeSetEntries)
	    		{
	    		}
	    }

	    private void OnSavedChanges(ChangeSet changeSet)
	    {
	
		
	    }
		
	    private void OnTransactingChanges(ChangeSet changeSet)
	    {
	
		
	    }
	
	    private void OnTransactedChanges(ChangeSet changeSet)
	    {
	
		
	    }
		
	    #endregion Entity Event Call Definitions
	
	    #region Transaction Control.
	
	    TransactionScope transactionScope = null;	
	
	    //Adjust Hierarchy Composition
	    private ChangeSet AdjustHierarchyForSaving(ChangeSet changeSet)
	    {

		
 	        return changeSet;
 	

	    }


	
	    //Transactions control
	    public override bool Submit(ChangeSet changeSet)
	    {
	        bool result = false;
	        try
	        {
	            currentChangeSet = changeSet = AdjustHierarchyForSaving(changeSet);
	            if (!OnValidatingChanges(changeSet)) return false;
	            Dictionary<object, object> oldKeys = new Dictionary<object, object>();
	            //Get temporary keys.
	            if (_controlKeyMapping)
	            {
	                foreach (ChangeSetEntry entry in changeSet.ChangeSetEntries)
	                {	
	                    var keys = ObjectExtension.GetKeyProperties(entry.Entity.GetType());
	                    if (keys.Count == 0) keys.Add("EntityUniqueKey");
	                    string tempKey = String.Join(":::", keys.Select(p => entry.Entity.GetPropertyValue(p)));
	                    if (!tempKey.IsNullOrEmpty())
	                        oldKeys.Add(entry.Entity, tempKey);
	                }
	            }
	            OnSavingChanges(changeSet);
	            result = base.Submit(changeSet);
	            if (!changeSet.HasError)
	            {	
	                

	                //Refresh real keys.
	                foreach (ChangeSetEntry entry in changeSet.ChangeSetEntries)
	                {	
	                    if (entry.Entity is Entity && changeSet.GetChangeOperation(entry.Entity) == ChangeOperation.Insert)
	                    	 ((Entity)entry.Entity).RefreshKeys();
	                
	                    if (_controlKeyMapping && oldKeys.ContainsKey(entry.Entity))
	                    {
	                		   var entityType = entry.Entity.GetType();
	                        var keys = ObjectExtension.GetKeyProperties(entityType);
	                        if (keys.Count == 0) keys.Add("EntityUniqueKey");
	                        string newKey = String.Join(":::", keys.Select(p => entry.Entity.GetPropertyValue(p)));
	                        if (!newKey.IsNullOrEmpty())
	                        {
	                            _keyMappings.Add(new DataKeyMapping
	                           {
	                               EntityTypeName = entityType.FullName,
	                               RealValue = (changeSet.GetChangeOperation(entry.Entity) == ChangeOperation.Delete ? null : newKey),
	                               TempValue = (changeSet.GetChangeOperation(entry.Entity) == ChangeOperation.Insert ? oldKeys[entry.Entity] : newKey)
	                           });
	                        }
	                    }
	                }	
	                OnTransactedChanges(changeSet);
	                if (!transactionScope.IsNull()) transactionScope.Complete();	
	            }
	        }
	        catch (Exception exp)
	        {
	            throw new DomainException(exp.Message, exp.InnerException);
	        }
	        finally
	        {
	            if (!transactionScope.IsNull())
	            {
	                transactionScope.Dispose();
	                transactionScope = null;
	            }
	        }
	    
	        OnSavedChanges(changeSet);
	        SaveMedia(changeSet);
	        return result;
	    }

	
	    protected override bool PersistChangeSet()
	    {
	        transactionScope = (this.GetEDM().ProviderName == "SQLite" ? null : new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }));
	        OnTransactingChanges(this.ChangeSet);
	        bool result = this.InvokeSaveChanges();
	        
	        return result;
	    }
	
	    #endregion Transaction Control.
		


	    #region Get OLAP Definitions.
	
			
	
	    #endregion Get OLAP Definitions.


	    #region Get LookUp Definitions.
	
		
			
        [Ignore]
	    //Get All LookUpProductModel.
	    public IQueryable<LookUpProductModel> GetAllLookUpProductModel()
	    {
	        return this.GetLookUpProductModel(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpProductModel By EntitySearch.
	    public IQueryable<LookUpProductModel> GetLookUpProductModelByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpProductModel(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpProductModel.
	    public IQueryable<LookUpProductModel> GetLookUpProductModel(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "ProductModel" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpProductModel";
	        object propvalue = (propertyName.IsNullOrEmpty() || serializedPropertyValue.IsNullOrEmpty() ? null : SerializationManager<object>.StringToObject(serializedPropertyValue));
	        if (!propvalue.IsNullOrEmpty())
	        {
	        	if (entitySearch.Expressions.Count > 0)
	        		entitySearch.Expressions.Add(new EntitySearchExpression("Condition", "&&"));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Field", propertyName));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Operator", (propvalue != null && propvalue is string && ((string)propvalue).Contains("%") ? "Like" : "==")));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Value", propvalue));
	        }
	
		

	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        if (entitySearch.Expressions.Count > 0)
	        {
	        	List<EntitySearch> entitySearchList = new List<EntitySearch>();
	        	entitySearchList.Add(entitySearch);
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpProductModel));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpProductModel> query =  
	
	            (from entity in this.DbContext.ProductModel.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpProductModel()		
	            {
	            
                ProductModelID = entity.ProductModelID
	            });

	            
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpProductSubcategory.
	    public IQueryable<LookUpProductSubcategory> GetAllLookUpProductSubcategory()
	    {
	        return this.GetLookUpProductSubcategory(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpProductSubcategory By EntitySearch.
	    public IQueryable<LookUpProductSubcategory> GetLookUpProductSubcategoryByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpProductSubcategory(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpProductSubcategory.
	    public IQueryable<LookUpProductSubcategory> GetLookUpProductSubcategory(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "ProductSubcategory" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpProductSubcategory";
	        object propvalue = (propertyName.IsNullOrEmpty() || serializedPropertyValue.IsNullOrEmpty() ? null : SerializationManager<object>.StringToObject(serializedPropertyValue));
	        if (!propvalue.IsNullOrEmpty())
	        {
	        	if (entitySearch.Expressions.Count > 0)
	        		entitySearch.Expressions.Add(new EntitySearchExpression("Condition", "&&"));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Field", propertyName));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Operator", (propvalue != null && propvalue is string && ((string)propvalue).Contains("%") ? "Like" : "==")));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Value", propvalue));
	        }
	
		

	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        if (entitySearch.Expressions.Count > 0)
	        {
	        	List<EntitySearch> entitySearchList = new List<EntitySearch>();
	        	entitySearchList.Add(entitySearch);
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpProductSubcategory));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpProductSubcategory> query =  
	
	            (from entity in this.DbContext.ProductSubcategory.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpProductSubcategory()		
	            {
	            
                ProductSubcategoryID = entity.ProductSubcategoryID
	            });

	            
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpUnitMeasure.
	    public IQueryable<LookUpUnitMeasure> GetAllLookUpUnitMeasure()
	    {
	        return this.GetLookUpUnitMeasure(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpUnitMeasure By EntitySearch.
	    public IQueryable<LookUpUnitMeasure> GetLookUpUnitMeasureByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpUnitMeasure(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpUnitMeasure.
	    public IQueryable<LookUpUnitMeasure> GetLookUpUnitMeasure(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "UnitMeasure" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpUnitMeasure";
	        object propvalue = (propertyName.IsNullOrEmpty() || serializedPropertyValue.IsNullOrEmpty() ? null : SerializationManager<object>.StringToObject(serializedPropertyValue));
	        if (!propvalue.IsNullOrEmpty())
	        {
	        	if (entitySearch.Expressions.Count > 0)
	        		entitySearch.Expressions.Add(new EntitySearchExpression("Condition", "&&"));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Field", propertyName));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Operator", (propvalue != null && propvalue is string && ((string)propvalue).Contains("%") ? "Like" : "==")));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Value", propvalue));
	        }
	
		

	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        if (entitySearch.Expressions.Count > 0)
	        {
	        	List<EntitySearch> entitySearchList = new List<EntitySearch>();
	        	entitySearchList.Add(entitySearch);
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpUnitMeasure));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpUnitMeasure> query =  
	
	            (from entity in this.DbContext.UnitMeasure.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpUnitMeasure()		
	            {
	            
                UnitMeasureCode = entity.UnitMeasureCode
	            });

	            
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpUnitMeasure1.
	    public IQueryable<LookUpUnitMeasure1> GetAllLookUpUnitMeasure1()
	    {
	        return this.GetLookUpUnitMeasure1(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpUnitMeasure1 By EntitySearch.
	    public IQueryable<LookUpUnitMeasure1> GetLookUpUnitMeasure1ByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpUnitMeasure1(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpUnitMeasure1.
	    public IQueryable<LookUpUnitMeasure1> GetLookUpUnitMeasure1(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "UnitMeasure" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpUnitMeasure1";
	        object propvalue = (propertyName.IsNullOrEmpty() || serializedPropertyValue.IsNullOrEmpty() ? null : SerializationManager<object>.StringToObject(serializedPropertyValue));
	        if (!propvalue.IsNullOrEmpty())
	        {
	        	if (entitySearch.Expressions.Count > 0)
	        		entitySearch.Expressions.Add(new EntitySearchExpression("Condition", "&&"));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Field", propertyName));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Operator", (propvalue != null && propvalue is string && ((string)propvalue).Contains("%") ? "Like" : "==")));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Value", propvalue));
	        }
	
		

	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        if (entitySearch.Expressions.Count > 0)
	        {
	        	List<EntitySearch> entitySearchList = new List<EntitySearch>();
	        	entitySearchList.Add(entitySearch);
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpUnitMeasure1));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpUnitMeasure1> query =  
	
	            (from entity in this.DbContext.UnitMeasure.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpUnitMeasure1()		
	            {
	            
                UnitMeasureCode1 = entity.UnitMeasureCode
	            });

	            
	
		
	
	
	        return query;

	    }
			
	    #endregion Get LookUp Definitions.
			

	    #region Get Meta Data.

	    [Ignore]
	    public List<BmMetaDataProperty> GetBmEntityProperties(string entityName, string parentDataPath)
	    {
		        return this.GetEDM().GetBmEntityProperties(entityName, parentDataPath);
		    }
	
	    [Ignore]
	    //Get Meta Data.
	    public string GetMetaData(string entityName, bool forceAll = false)
        {
	        return SerializationManager<List<LinxEntityReferenceInfo>>.ObjectToString(GetMetaDataObject(entityName, forceAll));
	    }

	    [Ignore]
	    public List<LinxEntityReferenceInfo> GetMetaDataObject(string entityName, bool forceAll = false, bool removeParentComposition = false)
        {
            List<LinxEntityReferenceInfo> result = new List<LinxEntityReferenceInfo>();
	
		

	        if (entityName.InList("LinxTraining001.BV.Product01.ProductView"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "ProductView",
	        			NameSpace = "LinxTraining001.BV.Product01",
	        			ParentClassName = null,	
	        			DisplayName = "ProductView",
	        			ClearMethodName = "ClearProductView",
	        			QueryMethodName  = "GetPagedProductView",	
	        			CountingMethodName  = "GetProductView" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("LinxTraining001.BV.Product01.ProductView"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("LinxTraining001.BV.Product01.ProductView"), forceAll: forceAll)
	        		});
	        }
	
            return result;
        }
	
	    [Ignore]
	    public string[] GetClientDomains()
        {	


             return new string[] { "LinxTraining001_MobileDataDomains", Linx.Tools.AssemblyHelper.ReadResourceContent("LinxTraining001.BV.ClientResources.MobileDataDomains.res", System.Reflection.Assembly.GetExecutingAssembly()) };	

        }

	    [Ignore]
	    public string[] GetClientService()
        {	


             return new string[] { "LinxTraining001_product01Service", Linx.Tools.AssemblyHelper.ReadResourceContent("LinxTraining001.BV.ClientResources.product01Service.res", System.Reflection.Assembly.GetExecutingAssembly()) };	

        }

	    [Ignore]
	    public string[] GetClientFactory(string entityName)
        {	


             return new string[] { };	

        }

	    [Ignore]
	    public string[] GetClientFactoryCustomEvents(string entityName)
        {	


             return new string[] { };	

        }
	
	    #endregion Get Meta Data.
	
	    #region Clear Methods Definitions.
	
		
	
	    [Ignore]
	    //Clear ProductView.
	    public IEnumerable<ProductView> ClearProductView()
	    {
	        List<ProductView> result = new List<ProductView>();
	        result.Add(new ProductView());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    [ProductViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get ProductView.
	    public IQueryable<ProductView> GetProductView()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetProductView")))
 	        {
 	             AuthorizationResult authorizationResult = (new ProductViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<ProductView> result = 
	            (from entity0 in this.DbContext.Product
                  let entity0Al3 = entity0.UnitMeasure
                  let entity0Al1 = entity0.ProductModel
                  let entity0Al4 = entity0.UnitMeasure1
                  let entity0Al2 = entity0.ProductSubcategory
	            
	            	
	            select new ProductView()		
	            {
	            
                Class = entity0.Class
                , Color = entity0.Color
                , DaysToManufacture = entity0.DaysToManufacture
                , DiscontinuedDate = entity0.DiscontinuedDate
                , FinishedGoodsFlag = entity0.FinishedGoodsFlag
                , ListPrice = entity0.ListPrice
                , MakeFlag = entity0.MakeFlag
                , ModifiedDate = entity0.ModifiedDate
                , Name = entity0.Name
                , ProductID = entity0.ProductID
                , ProductLine = entity0.ProductLine
                , ProductModelID = entity0Al1.ProductModelID
                , ProductNumber = entity0.ProductNumber
                , ProductSubcategoryID = entity0Al2.ProductSubcategoryID
                , ReorderPoint = entity0.ReorderPoint
                , Rowguid = entity0.rowguid
                , SafetyStockLevel = entity0.SafetyStockLevel
                , SellEndDate = entity0.SellEndDate
                , SellStartDate = entity0.SellStartDate
                , Size = entity0.Size
                , StandardCost = entity0.StandardCost
                , Style = entity0.Style
                , UnitMeasureCode = entity0Al3.UnitMeasureCode
                , UnitMeasureCode1 = entity0Al4.UnitMeasureCode
                , Weight = entity0.Weight
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [ProductViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get ProductViewNoAssociations.
	    public IQueryable<ProductView> GetProductViewNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetProductViewNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new ProductViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<ProductView> result = 
	            (from entity0 in this.DbContext.Product
                  let entity0Al3 = entity0.UnitMeasure
                  let entity0Al1 = entity0.ProductModel
                  let entity0Al4 = entity0.UnitMeasure1
                  let entity0Al2 = entity0.ProductSubcategory
	            
	            	
	            select new ProductView()		
	            {
	            
                Class = entity0.Class
                , Color = entity0.Color
                , DaysToManufacture = entity0.DaysToManufacture
                , DiscontinuedDate = entity0.DiscontinuedDate
                , FinishedGoodsFlag = entity0.FinishedGoodsFlag
                , ListPrice = entity0.ListPrice
                , MakeFlag = entity0.MakeFlag
                , ModifiedDate = entity0.ModifiedDate
                , Name = entity0.Name
                , ProductID = entity0.ProductID
                , ProductLine = entity0.ProductLine
                , ProductModelID = entity0Al1.ProductModelID
                , ProductNumber = entity0.ProductNumber
                , ProductSubcategoryID = entity0Al2.ProductSubcategoryID
                , ReorderPoint = entity0.ReorderPoint
                , Rowguid = entity0.rowguid
                , SafetyStockLevel = entity0.SafetyStockLevel
                , SellEndDate = entity0.SellEndDate
                , SellStartDate = entity0.SellStartDate
                , Size = entity0.Size
                , StandardCost = entity0.StandardCost
                , Style = entity0.Style
                , UnitMeasureCode = entity0Al3.UnitMeasureCode
                , UnitMeasureCode1 = entity0Al4.UnitMeasureCode
                , Weight = entity0.Weight
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	//Add filtering disabled property for Product
	    	string[] bmDisabledProductViewList = this.GetEDM().GetFilteringDisabledList("Product");
	    	if (bmDisabledProductViewList.Length > 0)
	    	{
	
	    		if (bmDisabledProductViewList.Contains("Product.Class"))
	    		{
	    			result.Add("ProductView|Class");
	    			result.Add("ProductView|Product.Class");
	    		}
	
	    		if (bmDisabledProductViewList.Contains("Product.Color"))
	    		{
	    			result.Add("ProductView|Color");
	    			result.Add("ProductView|Product.Color");
	    		}
	
	    		if (bmDisabledProductViewList.Contains("Product.DaysToManufacture"))
	    		{
	    			result.Add("ProductView|DaysToManufacture");
	    			result.Add("ProductView|Product.DaysToManufacture");
	    		}
	
	    		if (bmDisabledProductViewList.Contains("Product.DiscontinuedDate"))
	    		{
	    			result.Add("ProductView|DiscontinuedDate");
	    			result.Add("ProductView|Product.DiscontinuedDate");
	    		}
	
	    		if (bmDisabledProductViewList.Contains("Product.FinishedGoodsFlag"))
	    		{
	    			result.Add("ProductView|FinishedGoodsFlag");
	    			result.Add("ProductView|Product.FinishedGoodsFlag");
	    		}
	
	    		if (bmDisabledProductViewList.Contains("Product.ListPrice"))
	    		{
	    			result.Add("ProductView|ListPrice");
	    			result.Add("ProductView|Product.ListPrice");
	    		}
	
	    		if (bmDisabledProductViewList.Contains("Product.MakeFlag"))
	    		{
	    			result.Add("ProductView|MakeFlag");
	    			result.Add("ProductView|Product.MakeFlag");
	    		}
	
	    		if (bmDisabledProductViewList.Contains("Product.ModifiedDate"))
	    		{
	    			result.Add("ProductView|ModifiedDate");
	    			result.Add("ProductView|Product.ModifiedDate");
	    		}
	
	    		if (bmDisabledProductViewList.Contains("Product.Name"))
	    		{
	    			result.Add("ProductView|Name");
	    			result.Add("ProductView|Product.Name");
	    		}
	
	    		if (bmDisabledProductViewList.Contains("Product.ProductID"))
	    		{
	    			result.Add("ProductView|ProductID");
	    			result.Add("ProductView|Product.ProductID");
	    		}
	
	    		if (bmDisabledProductViewList.Contains("Product.ProductLine"))
	    		{
	    			result.Add("ProductView|ProductLine");
	    			result.Add("ProductView|Product.ProductLine");
	    		}
	
	    		if (bmDisabledProductViewList.Contains("Product.ProductNumber"))
	    		{
	    			result.Add("ProductView|ProductNumber");
	    			result.Add("ProductView|Product.ProductNumber");
	    		}
	
	    		if (bmDisabledProductViewList.Contains("Product.ReorderPoint"))
	    		{
	    			result.Add("ProductView|ReorderPoint");
	    			result.Add("ProductView|Product.ReorderPoint");
	    		}
	
	    		if (bmDisabledProductViewList.Contains("Product.rowguid"))
	    		{
	    			result.Add("ProductView|Rowguid");
	    			result.Add("ProductView|Product.rowguid");
	    		}
	
	    		if (bmDisabledProductViewList.Contains("Product.SafetyStockLevel"))
	    		{
	    			result.Add("ProductView|SafetyStockLevel");
	    			result.Add("ProductView|Product.SafetyStockLevel");
	    		}
	
	    		if (bmDisabledProductViewList.Contains("Product.SellEndDate"))
	    		{
	    			result.Add("ProductView|SellEndDate");
	    			result.Add("ProductView|Product.SellEndDate");
	    		}
	
	    		if (bmDisabledProductViewList.Contains("Product.SellStartDate"))
	    		{
	    			result.Add("ProductView|SellStartDate");
	    			result.Add("ProductView|Product.SellStartDate");
	    		}
	
	    		if (bmDisabledProductViewList.Contains("Product.Size"))
	    		{
	    			result.Add("ProductView|Size");
	    			result.Add("ProductView|Product.Size");
	    		}
	
	    		if (bmDisabledProductViewList.Contains("Product.StandardCost"))
	    		{
	    			result.Add("ProductView|StandardCost");
	    			result.Add("ProductView|Product.StandardCost");
	    		}
	
	    		if (bmDisabledProductViewList.Contains("Product.Style"))
	    		{
	    			result.Add("ProductView|Style");
	    			result.Add("ProductView|Product.Style");
	    		}
	
	    		if (bmDisabledProductViewList.Contains("Product.Weight"))
	    		{
	    			result.Add("ProductView|Weight");
	    			result.Add("ProductView|Product.Weight");
	    		}
	    	}
	    	return result;
	    }

	    #endregion Excluded Filters

	    #region Get By EntitySearchId Methods Definitions.
	
			
	    [Ignore]
	    //Add EntitySearch Id.
	    public void AddEntitySearchId(Guid entitySearchId, string searchDefinition)
	    {	
	            Linx.Tools.WebCacheHelper.AddWebCache(entitySearchId.ToString(), searchDefinition);
	    }
	    
	    [Ignore]
	    //Remove EntitySearch Id.
	    public void RemoveEntitySearchId(Guid entitySearchId)
	    {	
	            Linx.Tools.WebCacheHelper.RemoveWebCache(entitySearchId.ToString());
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get ProductView By EntitySearchId.
	    public IQueryable<ProductView> GetProductViewByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetProductViewByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get ProductView By EntitySearchId.
	    public IQueryable<ProductView> GetProductViewByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetProductViewByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get ProductView By Example.
	    [Ignore]
	    public IQueryable<ProductView> GetProductViewByExample(ProductView entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetProductViewByEntitySearch(queryAnalysis);
	    }
			
	    //Get ProductView By Example.
	    [Ignore]
	    public IQueryable<ProductView> GetProductViewByExampleNoAssociations(ProductView entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetProductViewByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public ProductView GetProductViewByKey(Int32 productID)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("ProductView");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "ProductID"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, productID));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetProductViewByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    [ProductViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get ProductViewByEntitySearch.
	    public IQueryable<ProductView> GetProductViewByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetProductViewByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new ProductViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(ProductView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<ProductView> result = 
	            (from entity0 in this.DbContext.Product.Where(dynQuery, parameters.ToArray())
                  let entity0Al3 = entity0.UnitMeasure
                  let entity0Al1 = entity0.ProductModel
                  let entity0Al4 = entity0.UnitMeasure1
                  let entity0Al2 = entity0.ProductSubcategory
	            
	            	
	            select new ProductView()		
	            {
	            
                Class = entity0.Class
                , Color = entity0.Color
                , DaysToManufacture = entity0.DaysToManufacture
                , DiscontinuedDate = entity0.DiscontinuedDate
                , FinishedGoodsFlag = entity0.FinishedGoodsFlag
                , ListPrice = entity0.ListPrice
                , MakeFlag = entity0.MakeFlag
                , ModifiedDate = entity0.ModifiedDate
                , Name = entity0.Name
                , ProductID = entity0.ProductID
                , ProductLine = entity0.ProductLine
                , ProductModelID = entity0Al1.ProductModelID
                , ProductNumber = entity0.ProductNumber
                , ProductSubcategoryID = entity0Al2.ProductSubcategoryID
                , ReorderPoint = entity0.ReorderPoint
                , Rowguid = entity0.rowguid
                , SafetyStockLevel = entity0.SafetyStockLevel
                , SellEndDate = entity0.SellEndDate
                , SellStartDate = entity0.SellStartDate
                , Size = entity0.Size
                , StandardCost = entity0.StandardCost
                , Style = entity0.Style
                , UnitMeasureCode = entity0Al3.UnitMeasureCode
                , UnitMeasureCode1 = entity0Al4.UnitMeasureCode
                , Weight = entity0.Weight
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [ProductViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get ProductViewByEntitySearchNoAssociations.
	    public IQueryable<ProductView> GetProductViewByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetProductViewByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new ProductViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(ProductView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<ProductView> result = 
	            (from entity0 in this.DbContext.Product.Where(dynQuery, parameters.ToArray())
                  let entity0Al3 = entity0.UnitMeasure
                  let entity0Al1 = entity0.ProductModel
                  let entity0Al4 = entity0.UnitMeasure1
                  let entity0Al2 = entity0.ProductSubcategory
	            
	            	
	            select new ProductView()		
	            {
	            
                Class = entity0.Class
                , Color = entity0.Color
                , DaysToManufacture = entity0.DaysToManufacture
                , DiscontinuedDate = entity0.DiscontinuedDate
                , FinishedGoodsFlag = entity0.FinishedGoodsFlag
                , ListPrice = entity0.ListPrice
                , MakeFlag = entity0.MakeFlag
                , ModifiedDate = entity0.ModifiedDate
                , Name = entity0.Name
                , ProductID = entity0.ProductID
                , ProductLine = entity0.ProductLine
                , ProductModelID = entity0Al1.ProductModelID
                , ProductNumber = entity0.ProductNumber
                , ProductSubcategoryID = entity0Al2.ProductSubcategoryID
                , ReorderPoint = entity0.ReorderPoint
                , Rowguid = entity0.rowguid
                , SafetyStockLevel = entity0.SafetyStockLevel
                , SellEndDate = entity0.SellEndDate
                , SellStartDate = entity0.SellStartDate
                , Size = entity0.Size
                , StandardCost = entity0.StandardCost
                , Style = entity0.Style
                , UnitMeasureCode = entity0Al3.UnitMeasureCode
                , UnitMeasureCode1 = entity0Al4.UnitMeasureCode
                , Weight = entity0.Weight
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    [ProductViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedProductView.
	    public IQueryable<ProductView> GetPagedProductView(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedProductView")))
 	        {
 	             AuthorizationResult authorizationResult = (new ProductViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(ProductView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<ProductView> result = 
	            (from entity0 in this.DbContext.Product.Where(dynQuery, parameters.ToArray())
                  let entity0Al3 = entity0.UnitMeasure
                  let entity0Al1 = entity0.ProductModel
                  let entity0Al4 = entity0.UnitMeasure1
                  let entity0Al2 = entity0.ProductSubcategory
                orderby entity0.ProductID ascending
	            
	            	
	            select new ProductView()		
	            {
	            
                Class = entity0.Class
                , Color = entity0.Color
                , DaysToManufacture = entity0.DaysToManufacture
                , DiscontinuedDate = entity0.DiscontinuedDate
                , FinishedGoodsFlag = entity0.FinishedGoodsFlag
                , ListPrice = entity0.ListPrice
                , MakeFlag = entity0.MakeFlag
                , ModifiedDate = entity0.ModifiedDate
                , Name = entity0.Name
                , ProductID = entity0.ProductID
                , ProductLine = entity0.ProductLine
                , ProductModelID = entity0Al1.ProductModelID
                , ProductNumber = entity0.ProductNumber
                , ProductSubcategoryID = entity0Al2.ProductSubcategoryID
                , ReorderPoint = entity0.ReorderPoint
                , Rowguid = entity0.rowguid
                , SafetyStockLevel = entity0.SafetyStockLevel
                , SellEndDate = entity0.SellEndDate
                , SellStartDate = entity0.SellStartDate
                , Size = entity0.Size
                , StandardCost = entity0.StandardCost
                , Style = entity0.Style
                , UnitMeasureCode = entity0Al3.UnitMeasureCode
                , UnitMeasureCode1 = entity0Al4.UnitMeasureCode
                , Weight = entity0.Weight
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetProductViewCounting(string serializedEntitySearch)
	    {	
		 	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(ProductView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.Product.Where(dynQuery, parameters.ToArray())
                  let entityAl3 = entity.UnitMeasure
                  let entityAl1 = entity.ProductModel
                  let entityAl4 = entity.UnitMeasure1
                  let entityAl2 = entity.ProductSubcategory
	            
	            select 1
	            ).Count();	
		
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    [ProductViewUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update ProductView.
	    public void UpdateProductView(ProductView entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateProductView")))
 	        {
 	             AuthorizationResult authorizationResult = (new ProductViewUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    [ProductViewInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert ProductView.
	    public void InsertProductView(ProductView entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertProductView")))
 	        {
 	             AuthorizationResult authorizationResult = (new ProductViewInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    [ProductViewDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete ProductView.
	    public void DeleteProductView(ProductView entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteProductView")))
 	        {
 	             AuthorizationResult authorizationResult = (new ProductViewDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}