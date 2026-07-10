					
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
using LinxTraining002.BM;

namespace LinxTraining001.BV.NotNull
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TiposCampos.ID_TiposCampos", IsUpdatable=false, EdmName="LinxTraining002.BM.ModeloVendaCliente")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TiposCamposView,TiposCamposView.TiposCamposFilhaView];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IDTiposCampos];ReadOnly[false];Entities[TiposCampos:IDTiposCampos];SubQueryInfo[];EdmEntityName[TiposCampos];EntityRelations[];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TiposCamposView")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "LinxTraining001.BV.NotNull.TiposCamposView")]
	public partial class TiposCamposView : Linx.Data.Entity
	{

	
		
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.TiposCamposFilhaViewList != null && this.TiposCamposFilhaViewList.Count() > 0)
	      {
	         foreach (var entity in this.TiposCamposFilhaViewList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.TiposCamposFilhaViewList != null)
	      {
	         foreach (var detail in this.TiposCamposFilhaViewList)
	         {
	            detail.ResetDetails();
	         }
	         this.TiposCamposFilhaViewList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(NotNullDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("TiposCamposFilhaView"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TiposCamposFilhaView");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IDTiposCampos"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IDTiposCampos));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TiposCamposFilhaView and all sub-details
	         if (this.TiposCamposFilhaViewList == null || this.TiposCamposFilhaViewList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TiposCamposFilhaViewList = context.GetPagedTiposCamposFilhaView(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TiposCamposFilhaViewList = (from r in context.GetTiposCamposFilhaViewByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _TiposCamposFilhaViewElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TiposCamposFilhaView && ((TiposCamposFilhaView)e.Entity).TiposCamposView == null && e.Associations == null && e.OriginalAssociations == null && ((TiposCamposFilhaView)e.Entity).IDTiposCampos == this.IDTiposCampos).ToList();
 	      if (_TiposCamposFilhaViewElements.Count > 0 && this.TiposCamposFilhaViewList.Count() == 0)
 	      {
 	          this.TiposCamposFilhaViewList = _TiposCamposFilhaViewElements.Select(e => (TiposCamposFilhaView)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TiposCamposFilhaViewElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TiposCamposFilhaView)detail.Entity).TiposCamposView = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TiposCamposView", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TiposCamposFilhaViewList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For Boolean
	    partial void OnBooleanChanging(System.Nullable<System.Boolean> value);
	    partial void OnBooleanChanged();

	    private System.Nullable<System.Boolean> _Boolean;

	    [DataMember(Name = "Boolean", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Boolean", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TiposCampos.Boolean];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TiposCampos.Boolean")]
	    public System.Nullable<System.Boolean> Boolean
	    {
	    	    get
	    	    {
	    	          return _Boolean;
	    	    }
	    	    set
	    	    {
	    	          if (this._Boolean != value)
	    	          {
	    	              this.ValidateProperty("Boolean", value);
	    	              this.OnBooleanChanging(value);
	    	              this.RaiseDataMemberChanging("Boolean");
	    	              this._Boolean = value;
	    	              this.RaiseDataMemberChanged("Boolean");
	    	              this.OnBooleanChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Byte
	    partial void OnByteChanging(System.Nullable<System.Byte> value);
	    partial void OnByteChanged();

	    private System.Nullable<System.Byte> _Byte;

	    [DataMember(Name = "Byte", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Byte", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TiposCampos.Byte];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TiposCampos.Byte")]
	    public System.Nullable<System.Byte> Byte
	    {
	    	    get
	    	    {
	    	          return _Byte;
	    	    }
	    	    set
	    	    {
	    	          if (this._Byte != value)
	    	          {
	    	              this.ValidateProperty("Byte", value);
	    	              this.OnByteChanging(value);
	    	              this.RaiseDataMemberChanging("Byte");
	    	              this._Byte = value;
	    	              this.RaiseDataMemberChanged("Byte");
	    	              this.OnByteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DateTime
	    partial void OnDateTimeChanging(System.Nullable<System.DateTime> value);
	    partial void OnDateTimeChanged();

	    private System.Nullable<System.DateTime> _DateTime;

	    [DataMember(Name = "DateTime", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "DateTime", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TiposCampos.DateTime];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TiposCampos.DateTime")]
	    public System.Nullable<System.DateTime> DateTime
	    {
	    	    get
	    	    {
	    	          return _DateTime;
	    	    }
	    	    set
	    	    {
	    	          if (this._DateTime != value)
	    	          {
	    	              this.ValidateProperty("DateTime", value);
	    	              this.OnDateTimeChanging(value);
	    	              this.RaiseDataMemberChanging("DateTime");
	    	              this._DateTime = value;
	    	              this.RaiseDataMemberChanged("DateTime");
	    	              this.OnDateTimeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Decimal
	    partial void OnDecimalChanging(System.Nullable<System.Decimal> value);
	    partial void OnDecimalChanged();

	    private System.Nullable<System.Decimal> _Decimal;

	    [DataMember(Name = "Decimal", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TiposCampos.Decimal];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TiposCampos.Decimal")]
	    public System.Nullable<System.Decimal> Decimal
	    {
	    	    get
	    	    {
	    	          return _Decimal;
	    	    }
	    	    set
	    	    {
	    	          if (this._Decimal != value)
	    	          {
	    	              this.ValidateProperty("Decimal", value);
	    	              this.OnDecimalChanging(value);
	    	              this.RaiseDataMemberChanging("Decimal");
	    	              this._Decimal = value;
	    	              this.RaiseDataMemberChanged("Decimal");
	    	              this.OnDecimalChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IDTiposCampos
	    partial void OnIDTiposCamposChanging(Int32 value);
	    partial void OnIDTiposCamposChanged();

	    private Int32 _IDTiposCampos;

	    [DataMember(IsRequired = true, Name = "IDTiposCampos", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID TiposCampos", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TiposCampos.ID_TiposCampos];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TiposCampos.ID_TiposCampos")]
	    public Int32 IDTiposCampos
	    {
	    	    get
	    	    {
	    	          return _IDTiposCampos;
	    	    }
	    	    set
	    	    {
	    	          if (this._IDTiposCampos != value)
	    	          {
	    	              this.ValidateProperty("IDTiposCampos", value);
	    	              this.OnIDTiposCamposChanging(value);
	    	              this.RaiseDataMemberChanging("IDTiposCampos");
	    	              this._IDTiposCampos = value;
	    	              this.RaiseDataMemberChanged("IDTiposCampos");
	    	              this.OnIDTiposCamposChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Int
	    partial void OnIntChanging(System.Nullable<System.Int32> value);
	    partial void OnIntChanged();

	    private System.Nullable<System.Int32> _Int;

	    [DataMember(Name = "Int", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TiposCampos.Int];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TiposCampos.Int")]
	    public System.Nullable<System.Int32> Int
	    {
	    	    get
	    	    {
	    	          return _Int;
	    	    }
	    	    set
	    	    {
	    	          if (this._Int != value)
	    	          {
	    	              this.ValidateProperty("Int", value);
	    	              this.OnIntChanging(value);
	    	              this.RaiseDataMemberChanging("Int");
	    	              this._Int = value;
	    	              this.RaiseDataMemberChanged("Int");
	    	              this.OnIntChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Long
	    partial void OnLongChanging(System.Nullable<System.Int64> value);
	    partial void OnLongChanged();

	    private System.Nullable<System.Int64> _Long;

	    [DataMember(Name = "Long", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Long", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TiposCampos.Long];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TiposCampos.Long")]
	    public System.Nullable<System.Int64> Long
	    {
	    	    get
	    	    {
	    	          return _Long;
	    	    }
	    	    set
	    	    {
	    	          if (this._Long != value)
	    	          {
	    	              this.ValidateProperty("Long", value);
	    	              this.OnLongChanging(value);
	    	              this.RaiseDataMemberChanging("Long");
	    	              this._Long = value;
	    	              this.RaiseDataMemberChanged("Long");
	    	              this.OnLongChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Short
	    partial void OnShortChanging(System.Nullable<System.Int16> value);
	    partial void OnShortChanged();

	    private System.Nullable<System.Int16> _Short;

	    [DataMember(Name = "Short", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Short", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TiposCampos.Short];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TiposCampos.Short")]
	    public System.Nullable<System.Int16> Short
	    {
	    	    get
	    	    {
	    	          return _Short;
	    	    }
	    	    set
	    	    {
	    	          if (this._Short != value)
	    	          {
	    	              this.ValidateProperty("Short", value);
	    	              this.OnShortChanging(value);
	    	              this.RaiseDataMemberChanging("Short");
	    	              this._Short = value;
	    	              this.RaiseDataMemberChanged("Short");
	    	              this.OnShortChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For String
	    partial void OnStringChanging(System.String value);
	    partial void OnStringChanged();

	    private System.String _String;

	    [DataMember(Name = "String", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TiposCampos.String];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TiposCampos.String")]
	    public System.String String
	    {
	    	    get
	    	    {
	    	          return _String;
	    	    }
	    	    set
	    	    {
	    	          if (this._String != value)
	    	          {
	    	              this.ValidateProperty("String", value);
	    	              this.OnStringChanging(value);
	    	              this.RaiseDataMemberChanging("String");
	    	              this._String = value;
	    	              this.RaiseDataMemberChanged("String");
	    	              this.OnStringChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringChar
	    partial void OnStringCharChanging(System.String value);
	    partial void OnStringCharChanged();

	    private System.String _StringChar;

	    [DataMember(Name = "StringChar", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "StringChar", Description="", Order = 14, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TiposCampos.StringChar];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TiposCampos.StringChar")]
	    public System.String StringChar
	    {
	    	    get
	    	    {
	    	          return _StringChar;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringChar != value)
	    	          {
	    	              this.ValidateProperty("StringChar", value);
	    	              this.OnStringCharChanging(value);
	    	              this.RaiseDataMemberChanging("StringChar");
	    	              this._StringChar = value;
	    	              this.RaiseDataMemberChanged("StringChar");
	    	              this.OnStringCharChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringText
	    partial void OnStringTextChanging(System.String value);
	    partial void OnStringTextChanged();

	    private System.String _StringText;

	    [DataMember(Name = "StringText", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "StringText", Description="", Order = 15, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TiposCampos.StringText];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TiposCampos.StringText")]
	    public System.String StringText
	    {
	    	    get
	    	    {
	    	          return _StringText;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringText != value)
	    	          {
	    	              this.ValidateProperty("StringText", value);
	    	              this.OnStringTextChanging(value);
	    	              this.RaiseDataMemberChanging("StringText");
	    	              this._StringText = value;
	    	              this.RaiseDataMemberChanged("StringText");
	    	              this.OnStringTextChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIDTiposCampos;
	    [DataMember(Name = "TemporaryIDTiposCampos", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID TiposCampos (Tmp)", Description="Temporary Key", Order = 9, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIDTiposCampos
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIDTiposCampos.IsNullOrEmpty())
	    	                this._TemporaryIDTiposCampos = this._IDTiposCampos;
	    	          return this._TemporaryIDTiposCampos;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIDTiposCampos != value)
	    	              this._TemporaryIDTiposCampos = value;
	    	    }
	    }	

	    #endregion Data Properties

	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<TiposCamposFilhaView> _TiposCamposFilhaViewList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TiposCamposView_TiposCamposFilhaView", "IDTiposCampos", "IDTiposCampos", IsForeignKey=false)]
	    [DataMember(Name = "TiposCamposFilhaViewList", EmitDefaultValue = true)]
	    public IEnumerable<TiposCamposFilhaView> TiposCamposFilhaViewList
	    {
	        get
	        {
	
	            if (this._TiposCamposFilhaViewList == null)
	            	this._TiposCamposFilhaViewList = new List<TiposCamposFilhaView>();
	
	            return this._TiposCamposFilhaViewList;
	        }
	        set
	        {
	            if (this._TiposCamposFilhaViewList != value)
	            {
	                this._TiposCamposFilhaViewList = value;
	                this.RaisePropertyChanged("TiposCamposFilhaViewList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ModeloVendaCliente.TiposCampos").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(LinxTraining002.BM.TiposCampos), QualifiedEntitySetName = "ModeloVendaCliente.TiposCampos" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TiposCampos.Int", Source = "Int", Target = "Int", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TiposCampos", RelationPropertyName = "TiposCampos" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TiposCampos.Byte", Source = "Byte", Target = "Byte", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TiposCampos", RelationPropertyName = "TiposCampos" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TiposCampos.Long", Source = "Long", Target = "Long", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TiposCampos", RelationPropertyName = "TiposCampos" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TiposCampos.Short", Source = "Short", Target = "Short", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TiposCampos", RelationPropertyName = "TiposCampos" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TiposCampos.String", Source = "String", Target = "String", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TiposCampos", RelationPropertyName = "TiposCampos" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TiposCampos.Boolean", Source = "Boolean", Target = "Boolean", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TiposCampos", RelationPropertyName = "TiposCampos" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TiposCampos.Decimal", Source = "Decimal", Target = "Decimal", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TiposCampos", RelationPropertyName = "TiposCampos" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TiposCampos.DateTime", Source = "DateTime", Target = "DateTime", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TiposCampos", RelationPropertyName = "TiposCampos" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TiposCampos.StringChar", Source = "StringChar", Target = "StringChar", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TiposCampos", RelationPropertyName = "TiposCampos" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TiposCampos.StringText", Source = "StringText", Target = "StringText", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TiposCampos", RelationPropertyName = "TiposCampos" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TiposCampos.ID_TiposCampos", Source = "IDTiposCampos", Target = "ID_TiposCampos", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TiposCampos", RelationPropertyName = "TiposCampos" });

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
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TiposCamposFilha.ID_TiposCamposFilha", IsUpdatable=false, EdmName="LinxTraining002.BM.ModeloVendaCliente")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[TiposCamposFilhaView];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IDTiposCamposFilha];ReadOnly[false];Entities[TiposCamposFilha:IDTiposCamposFilha];SubQueryInfo[Select 1 From #ParentAlias#.TiposCamposFilha_LISTA as #Alias#];EdmEntityName[TiposCamposFilha];EntityRelations[TiposCampos(TiposCampos)];EdmParentEntityName[TiposCampos];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TiposCamposFilhaView")]
	[Serializable()]
	public partial class TiposCamposFilhaView : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(NotNullDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TiposCamposView");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IDTiposCampos"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IDTiposCampos));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TiposCamposView
	         this.TiposCamposView = (from r in context.GetTiposCamposViewByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Load Data Parent

	
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
	 

	    //Extensibility Partial Method Definitions For Boolean
	    partial void OnBooleanChanging(System.Nullable<System.Boolean> value);
	    partial void OnBooleanChanged();

	    private System.Nullable<System.Boolean> _Boolean;

	    [DataMember(Name = "Boolean", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Boolean", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TiposCamposFilha.Boolean];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TiposCamposFilha.Boolean")]
	    public System.Nullable<System.Boolean> Boolean
	    {
	    	    get
	    	    {
	    	          return _Boolean;
	    	    }
	    	    set
	    	    {
	    	          if (this._Boolean != value)
	    	          {
	    	              this.ValidateProperty("Boolean", value);
	    	              this.OnBooleanChanging(value);
	    	              this.RaiseDataMemberChanging("Boolean");
	    	              this._Boolean = value;
	    	              this.RaiseDataMemberChanged("Boolean");
	    	              this.OnBooleanChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Byte
	    partial void OnByteChanging(System.Nullable<System.Byte> value);
	    partial void OnByteChanged();

	    private System.Nullable<System.Byte> _Byte;

	    [DataMember(Name = "Byte", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Byte", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TiposCamposFilha.Byte];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TiposCamposFilha.Byte")]
	    public System.Nullable<System.Byte> Byte
	    {
	    	    get
	    	    {
	    	          return _Byte;
	    	    }
	    	    set
	    	    {
	    	          if (this._Byte != value)
	    	          {
	    	              this.ValidateProperty("Byte", value);
	    	              this.OnByteChanging(value);
	    	              this.RaiseDataMemberChanging("Byte");
	    	              this._Byte = value;
	    	              this.RaiseDataMemberChanged("Byte");
	    	              this.OnByteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DateTime
	    partial void OnDateTimeChanging(System.Nullable<System.DateTime> value);
	    partial void OnDateTimeChanged();

	    private System.Nullable<System.DateTime> _DateTime;

	    [DataMember(Name = "DateTime", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "DateTime", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TiposCamposFilha.DateTime];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TiposCamposFilha.DateTime")]
	    public System.Nullable<System.DateTime> DateTime
	    {
	    	    get
	    	    {
	    	          return _DateTime;
	    	    }
	    	    set
	    	    {
	    	          if (this._DateTime != value)
	    	          {
	    	              this.ValidateProperty("DateTime", value);
	    	              this.OnDateTimeChanging(value);
	    	              this.RaiseDataMemberChanging("DateTime");
	    	              this._DateTime = value;
	    	              this.RaiseDataMemberChanged("DateTime");
	    	              this.OnDateTimeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Decimal
	    partial void OnDecimalChanging(System.Nullable<System.Decimal> value);
	    partial void OnDecimalChanged();

	    private System.Nullable<System.Decimal> _Decimal;

	    [DataMember(Name = "Decimal", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TiposCamposFilha.Decimal];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TiposCamposFilha.Decimal")]
	    public System.Nullable<System.Decimal> Decimal
	    {
	    	    get
	    	    {
	    	          return _Decimal;
	    	    }
	    	    set
	    	    {
	    	          if (this._Decimal != value)
	    	          {
	    	              this.ValidateProperty("Decimal", value);
	    	              this.OnDecimalChanging(value);
	    	              this.RaiseDataMemberChanging("Decimal");
	    	              this._Decimal = value;
	    	              this.RaiseDataMemberChanged("Decimal");
	    	              this.OnDecimalChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IDTiposCampos
	    partial void OnIDTiposCamposChanging(Int32 value);
	    partial void OnIDTiposCamposChanged();

	    private Int32 _IDTiposCampos;

	    [DataMember(IsRequired = true, Name = "IDTiposCampos", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID TiposCampos", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TiposCamposFilha.TiposCampos.ID_TiposCampos];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TiposCamposFilha.TiposCampos.ID_TiposCampos")]
	    public Int32 IDTiposCampos
	    {
	    	    get
	    	    {
	    	          return _IDTiposCampos;
	    	    }
	    	    set
	    	    {
	    	          if (this._IDTiposCampos != value)
	    	          {
	    	              this.ValidateProperty("IDTiposCampos", value);
	    	              this.OnIDTiposCamposChanging(value);
	    	              this.RaiseDataMemberChanging("IDTiposCampos");
	    	              this._IDTiposCampos = value;
	    	              this.RaiseDataMemberChanged("IDTiposCampos");
	    	              this.OnIDTiposCamposChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IDTiposCamposFilha
	    partial void OnIDTiposCamposFilhaChanging(Int32 value);
	    partial void OnIDTiposCamposFilhaChanged();

	    private Int32 _IDTiposCamposFilha;

	    [DataMember(IsRequired = true, Name = "IDTiposCamposFilha", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID TiposCamposFilha", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TiposCamposFilha.ID_TiposCamposFilha];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TiposCamposFilha.ID_TiposCamposFilha")]
	    public Int32 IDTiposCamposFilha
	    {
	    	    get
	    	    {
	    	          return _IDTiposCamposFilha;
	    	    }
	    	    set
	    	    {
	    	          if (this._IDTiposCamposFilha != value)
	    	          {
	    	              this.ValidateProperty("IDTiposCamposFilha", value);
	    	              this.OnIDTiposCamposFilhaChanging(value);
	    	              this.RaiseDataMemberChanging("IDTiposCamposFilha");
	    	              this._IDTiposCamposFilha = value;
	    	              this.RaiseDataMemberChanged("IDTiposCamposFilha");
	    	              this.OnIDTiposCamposFilhaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Int
	    partial void OnIntChanging(System.Nullable<System.Int32> value);
	    partial void OnIntChanged();

	    private System.Nullable<System.Int32> _Int;

	    [DataMember(Name = "Int", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TiposCamposFilha.Int];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TiposCamposFilha.Int")]
	    public System.Nullable<System.Int32> Int
	    {
	    	    get
	    	    {
	    	          return _Int;
	    	    }
	    	    set
	    	    {
	    	          if (this._Int != value)
	    	          {
	    	              this.ValidateProperty("Int", value);
	    	              this.OnIntChanging(value);
	    	              this.RaiseDataMemberChanging("Int");
	    	              this._Int = value;
	    	              this.RaiseDataMemberChanged("Int");
	    	              this.OnIntChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Long
	    partial void OnLongChanging(System.Nullable<System.Int64> value);
	    partial void OnLongChanged();

	    private System.Nullable<System.Int64> _Long;

	    [DataMember(Name = "Long", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Long", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TiposCamposFilha.Long];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TiposCamposFilha.Long")]
	    public System.Nullable<System.Int64> Long
	    {
	    	    get
	    	    {
	    	          return _Long;
	    	    }
	    	    set
	    	    {
	    	          if (this._Long != value)
	    	          {
	    	              this.ValidateProperty("Long", value);
	    	              this.OnLongChanging(value);
	    	              this.RaiseDataMemberChanging("Long");
	    	              this._Long = value;
	    	              this.RaiseDataMemberChanged("Long");
	    	              this.OnLongChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Short
	    partial void OnShortChanging(System.Nullable<System.Int16> value);
	    partial void OnShortChanged();

	    private System.Nullable<System.Int16> _Short;

	    [DataMember(Name = "Short", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Short", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TiposCamposFilha.Short];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TiposCamposFilha.Short")]
	    public System.Nullable<System.Int16> Short
	    {
	    	    get
	    	    {
	    	          return _Short;
	    	    }
	    	    set
	    	    {
	    	          if (this._Short != value)
	    	          {
	    	              this.ValidateProperty("Short", value);
	    	              this.OnShortChanging(value);
	    	              this.RaiseDataMemberChanging("Short");
	    	              this._Short = value;
	    	              this.RaiseDataMemberChanged("Short");
	    	              this.OnShortChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For String
	    partial void OnStringChanging(System.String value);
	    partial void OnStringChanged();

	    private System.String _String;

	    [DataMember(Name = "String", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[TstDomainString];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TiposCamposFilha.String];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TiposCamposFilha.String")]
	    public System.String String
	    {
	    	    get
	    	    {
	    	          return _String;
	    	    }
	    	    set
	    	    {
	    	          if (this._String != value)
	    	          {
	    	              this.ValidateProperty("String", value);
	    	              this.OnStringChanging(value);
	    	              this.RaiseDataMemberChanging("String");
	    	              this._String = value;
	    	              this.RaiseDataMemberChanged("String");
	    	              this.OnStringChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringChar
	    partial void OnStringCharChanging(System.String value);
	    partial void OnStringCharChanged();

	    private System.String _StringChar;

	    [DataMember(Name = "StringChar", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "StringChar", Description="", Order = 14, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TiposCamposFilha.StringChar];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TiposCamposFilha.StringChar")]
	    public System.String StringChar
	    {
	    	    get
	    	    {
	    	          return _StringChar;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringChar != value)
	    	          {
	    	              this.ValidateProperty("StringChar", value);
	    	              this.OnStringCharChanging(value);
	    	              this.RaiseDataMemberChanging("StringChar");
	    	              this._StringChar = value;
	    	              this.RaiseDataMemberChanged("StringChar");
	    	              this.OnStringCharChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringText
	    partial void OnStringTextChanging(System.String value);
	    partial void OnStringTextChanged();

	    private System.String _StringText;

	    [DataMember(Name = "StringText", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "StringText", Description="", Order = 15, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TiposCamposFilha.StringText];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TiposCamposFilha.StringText")]
	    public System.String StringText
	    {
	    	    get
	    	    {
	    	          return _StringText;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringText != value)
	    	          {
	    	              this.ValidateProperty("StringText", value);
	    	              this.OnStringTextChanging(value);
	    	              this.RaiseDataMemberChanging("StringText");
	    	              this._StringText = value;
	    	              this.RaiseDataMemberChanged("StringText");
	    	              this.OnStringTextChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIDTiposCamposFilha;
	    [DataMember(Name = "TemporaryIDTiposCamposFilha", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID TiposCamposFilha (Tmp)", Description="Temporary Key", Order = 9, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIDTiposCamposFilha
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIDTiposCamposFilha.IsNullOrEmpty())
	    	                this._TemporaryIDTiposCamposFilha = this._IDTiposCamposFilha;
	    	          return this._TemporaryIDTiposCamposFilha;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIDTiposCamposFilha != value)
	    	              this._TemporaryIDTiposCamposFilha = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TiposCamposView _TiposCamposView;
	    [DataMember(Name = "TiposCamposView", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TiposCamposView_TiposCamposFilhaView", "IDTiposCampos", "IDTiposCampos", IsForeignKey=true)]
	    public TiposCamposView TiposCamposView
	    {
	        get
	        {
	            return this._TiposCamposView;
	        }
	        set
	        {
	            if (this._TiposCamposView != value)
	            {
	                this._TiposCamposView = value;
	                this.RaisePropertyChanged("TiposCamposViewList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ModeloVendaCliente.TiposCamposFilha").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(LinxTraining002.BM.TiposCamposFilha), QualifiedEntitySetName = "ModeloVendaCliente.TiposCamposFilha" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TiposCamposFilha.Int", Source = "Int", Target = "Int", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TiposCamposFilha", RelationPropertyName = "TiposCamposFilha" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TiposCamposFilha.Byte", Source = "Byte", Target = "Byte", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TiposCamposFilha", RelationPropertyName = "TiposCamposFilha" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TiposCamposFilha.Long", Source = "Long", Target = "Long", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TiposCamposFilha", RelationPropertyName = "TiposCamposFilha" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TiposCamposFilha.Short", Source = "Short", Target = "Short", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TiposCamposFilha", RelationPropertyName = "TiposCamposFilha" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TiposCamposFilha.String", Source = "String", Target = "String", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TiposCamposFilha", RelationPropertyName = "TiposCamposFilha" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TiposCamposFilha.Boolean", Source = "Boolean", Target = "Boolean", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TiposCamposFilha", RelationPropertyName = "TiposCamposFilha" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TiposCamposFilha.Decimal", Source = "Decimal", Target = "Decimal", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TiposCamposFilha", RelationPropertyName = "TiposCamposFilha" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TiposCamposFilha.DateTime", Source = "DateTime", Target = "DateTime", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TiposCamposFilha", RelationPropertyName = "TiposCamposFilha" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TiposCamposFilha.StringChar", Source = "StringChar", Target = "StringChar", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TiposCamposFilha", RelationPropertyName = "TiposCamposFilha" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TiposCamposFilha.StringText", Source = "StringText", Target = "StringText", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TiposCamposFilha", RelationPropertyName = "TiposCamposFilha" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TiposCamposFilha.ID_TiposCamposFilha", Source = "IDTiposCamposFilha", Target = "ID_TiposCamposFilha", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TiposCamposFilha", RelationPropertyName = "TiposCamposFilha" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TiposCamposFilha.TiposCampos.ID_TiposCampos", Source = "IDTiposCampos", Target = "ID_TiposCampos", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ModeloVendaCliente.TiposCampos", RelationPropertyName = "TiposCampos" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetStringValues()
	    {
	    	    return LinxTraining001.BV.Domains.TstDomainString.GetValues();
	    }
	    private string _stringName;
	    [DataMember(IsRequired = false, Name = "StringName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "String", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string StringName
	    {
	    	    get { if (this.String.IsNullOrEmpty()) { _stringName = String.Empty; } else { string key = this.String.ToString(); var dmValues = this.GetStringValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _stringName) _stringName = domainName; } return _stringName; } set { _stringName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="PaiNotNull.ID_PaiNotNull", IsUpdatable=false, EdmName="LinxTraining002.BM.ModeloVendaCliente")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[PaiNotNullView,PaiNotNullView.FilhaNotNullView];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IDPaiNotNull];ReadOnly[false];Entities[PaiNotNull:IDPaiNotNull];SubQueryInfo[];EdmEntityName[PaiNotNull];EntityRelations[];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "PaiNotNullView")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "LinxTraining001.BV.NotNull.PaiNotNullView")]
	public partial class PaiNotNullView : Linx.Data.Entity
	{

	
		
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.FilhaNotNullViewList != null && this.FilhaNotNullViewList.Count() > 0)
	      {
	         foreach (var entity in this.FilhaNotNullViewList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.FilhaNotNullViewList != null)
	      {
	         foreach (var detail in this.FilhaNotNullViewList)
	         {
	            detail.ResetDetails();
	         }
	         this.FilhaNotNullViewList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(NotNullDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("FilhaNotNullView"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("FilhaNotNullView");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IDPaiNotNull"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IDPaiNotNull));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load FilhaNotNullView and all sub-details
	         if (this.FilhaNotNullViewList == null || this.FilhaNotNullViewList.Count() == 0)
	         {
	             if (take > 0)
	                 this.FilhaNotNullViewList = context.GetPagedFilhaNotNullView(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.FilhaNotNullViewList = (from r in context.GetFilhaNotNullViewByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _FilhaNotNullViewElements = changeSet.ChangeSetEntries.Where(e => e.Entity is FilhaNotNullView && ((FilhaNotNullView)e.Entity).PaiNotNullView == null && e.Associations == null && e.OriginalAssociations == null && ((FilhaNotNullView)e.Entity).IDPaiNotNull == this.IDPaiNotNull).ToList();
 	      if (_FilhaNotNullViewElements.Count > 0 && this.FilhaNotNullViewList.Count() == 0)
 	      {
 	          this.FilhaNotNullViewList = _FilhaNotNullViewElements.Select(e => (FilhaNotNullView)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _FilhaNotNullViewElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((FilhaNotNullView)detail.Entity).PaiNotNullView = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("PaiNotNullView", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("FilhaNotNullViewList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For Boolean
	    partial void OnBooleanChanging(Boolean value);
	    partial void OnBooleanChanged();

	    private Boolean _Boolean;

	    [DataMember(IsRequired = true, Name = "Boolean", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Boolean", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[PaiNotNull.Boolean];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="PaiNotNull.Boolean")]
	    public Boolean Boolean
	    {
	    	    get
	    	    {
	    	          return _Boolean;
	    	    }
	    	    set
	    	    {
	    	          if (this._Boolean != value)
	    	          {
	    	              this.ValidateProperty("Boolean", value);
	    	              this.OnBooleanChanging(value);
	    	              this.RaiseDataMemberChanging("Boolean");
	    	              this._Boolean = value;
	    	              this.RaiseDataMemberChanged("Boolean");
	    	              this.OnBooleanChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Byte
	    partial void OnByteChanging(Byte value);
	    partial void OnByteChanged();

	    private Byte _Byte;

	    [DataMember(IsRequired = true, Name = "Byte", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Byte", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[PaiNotNull.Byte];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="PaiNotNull.Byte")]
	    public Byte Byte
	    {
	    	    get
	    	    {
	    	          return _Byte;
	    	    }
	    	    set
	    	    {
	    	          if (this._Byte != value)
	    	          {
	    	              this.ValidateProperty("Byte", value);
	    	              this.OnByteChanging(value);
	    	              this.RaiseDataMemberChanging("Byte");
	    	              this._Byte = value;
	    	              this.RaiseDataMemberChanged("Byte");
	    	              this.OnByteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DateTime
	    partial void OnDateTimeChanging(System.DateTime value);
	    partial void OnDateTimeChanged();

	    private System.DateTime _DateTime;

	    [DataMember(IsRequired = true, Name = "DateTime", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "DateTime", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[PaiNotNull.DateTime];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="PaiNotNull.DateTime")]
	    public System.DateTime DateTime
	    {
	    	    get
	    	    {
	    	          return _DateTime;
	    	    }
	    	    set
	    	    {
	    	          if (this._DateTime != value)
	    	          {
	    	              this.ValidateProperty("DateTime", value);
	    	              this.OnDateTimeChanging(value);
	    	              this.RaiseDataMemberChanging("DateTime");
	    	              this._DateTime = value;
	    	              this.RaiseDataMemberChanged("DateTime");
	    	              this.OnDateTimeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Decimal
	    partial void OnDecimalChanging(System.Decimal value);
	    partial void OnDecimalChanged();

	    private System.Decimal _Decimal;

	    [DataMember(IsRequired = true, Name = "Decimal", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[PaiNotNull.Decimal];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="PaiNotNull.Decimal")]
	    public System.Decimal Decimal
	    {
	    	    get
	    	    {
	    	          return _Decimal;
	    	    }
	    	    set
	    	    {
	    	          if (this._Decimal != value)
	    	          {
	    	              this.ValidateProperty("Decimal", value);
	    	              this.OnDecimalChanging(value);
	    	              this.RaiseDataMemberChanging("Decimal");
	    	              this._Decimal = value;
	    	              this.RaiseDataMemberChanged("Decimal");
	    	              this.OnDecimalChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IDPaiNotNull
	    partial void OnIDPaiNotNullChanging(Int32 value);
	    partial void OnIDPaiNotNullChanged();

	    private Int32 _IDPaiNotNull;

	    [DataMember(IsRequired = true, Name = "IDPaiNotNull", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID PaiNotNull", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[PaiNotNull.ID_PaiNotNull];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="PaiNotNull.ID_PaiNotNull")]
	    public Int32 IDPaiNotNull
	    {
	    	    get
	    	    {
	    	          return _IDPaiNotNull;
	    	    }
	    	    set
	    	    {
	    	          if (this._IDPaiNotNull != value)
	    	          {
	    	              this.ValidateProperty("IDPaiNotNull", value);
	    	              this.OnIDPaiNotNullChanging(value);
	    	              this.RaiseDataMemberChanging("IDPaiNotNull");
	    	              this._IDPaiNotNull = value;
	    	              this.RaiseDataMemberChanged("IDPaiNotNull");
	    	              this.OnIDPaiNotNullChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Int
	    partial void OnIntChanging(Int32 value);
	    partial void OnIntChanged();

	    private Int32 _Int;

	    [DataMember(IsRequired = true, Name = "Int", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[PaiNotNull.Int];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="PaiNotNull.Int")]
	    public Int32 Int
	    {
	    	    get
	    	    {
	    	          return _Int;
	    	    }
	    	    set
	    	    {
	    	          if (this._Int != value)
	    	          {
	    	              this.ValidateProperty("Int", value);
	    	              this.OnIntChanging(value);
	    	              this.RaiseDataMemberChanging("Int");
	    	              this._Int = value;
	    	              this.RaiseDataMemberChanged("Int");
	    	              this.OnIntChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Long
	    partial void OnLongChanging(Int64 value);
	    partial void OnLongChanged();

	    private Int64 _Long;

	    [DataMember(IsRequired = true, Name = "Long", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Long", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[PaiNotNull.Long];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="PaiNotNull.Long")]
	    public Int64 Long
	    {
	    	    get
	    	    {
	    	          return _Long;
	    	    }
	    	    set
	    	    {
	    	          if (this._Long != value)
	    	          {
	    	              this.ValidateProperty("Long", value);
	    	              this.OnLongChanging(value);
	    	              this.RaiseDataMemberChanging("Long");
	    	              this._Long = value;
	    	              this.RaiseDataMemberChanged("Long");
	    	              this.OnLongChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Short
	    partial void OnShortChanging(Int16 value);
	    partial void OnShortChanged();

	    private Int16 _Short;

	    [DataMember(IsRequired = true, Name = "Short", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Short", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[PaiNotNull.Short];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="PaiNotNull.Short")]
	    public Int16 Short
	    {
	    	    get
	    	    {
	    	          return _Short;
	    	    }
	    	    set
	    	    {
	    	          if (this._Short != value)
	    	          {
	    	              this.ValidateProperty("Short", value);
	    	              this.OnShortChanging(value);
	    	              this.RaiseDataMemberChanging("Short");
	    	              this._Short = value;
	    	              this.RaiseDataMemberChanged("Short");
	    	              this.OnShortChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For String
	    partial void OnStringChanging(System.String value);
	    partial void OnStringChanged();

	    private System.String _String;

	    [DataMember(IsRequired = true, Name = "String", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[PaiNotNull.String];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="PaiNotNull.String")]
	    public System.String String
	    {
	    	    get
	    	    {
	    	          return _String;
	    	    }
	    	    set
	    	    {
	    	          if (this._String != value)
	    	          {
	    	              this.ValidateProperty("String", value);
	    	              this.OnStringChanging(value);
	    	              this.RaiseDataMemberChanging("String");
	    	              this._String = value;
	    	              this.RaiseDataMemberChanged("String");
	    	              this.OnStringChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringChar
	    partial void OnStringCharChanging(System.String value);
	    partial void OnStringCharChanged();

	    private System.String _StringChar;

	    [DataMember(IsRequired = true, Name = "StringChar", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "StringChar", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[PaiNotNull.StringChar];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="PaiNotNull.StringChar")]
	    public System.String StringChar
	    {
	    	    get
	    	    {
	    	          return _StringChar;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringChar != value)
	    	          {
	    	              this.ValidateProperty("StringChar", value);
	    	              this.OnStringCharChanging(value);
	    	              this.RaiseDataMemberChanging("StringChar");
	    	              this._StringChar = value;
	    	              this.RaiseDataMemberChanged("StringChar");
	    	              this.OnStringCharChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringText
	    partial void OnStringTextChanging(System.String value);
	    partial void OnStringTextChanged();

	    private System.String _StringText;

	    [DataMember(IsRequired = true, Name = "StringText", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "StringText", Description="", Order = 14, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[PaiNotNull.StringText];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="PaiNotNull.StringText")]
	    public System.String StringText
	    {
	    	    get
	    	    {
	    	          return _StringText;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringText != value)
	    	          {
	    	              this.ValidateProperty("StringText", value);
	    	              this.OnStringTextChanging(value);
	    	              this.RaiseDataMemberChanging("StringText");
	    	              this._StringText = value;
	    	              this.RaiseDataMemberChanged("StringText");
	    	              this.OnStringTextChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIDPaiNotNull;
	    [DataMember(Name = "TemporaryIDPaiNotNull", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID PaiNotNull (Tmp)", Description="Temporary Key", Order = 8, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIDPaiNotNull
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIDPaiNotNull.IsNullOrEmpty())
	    	                this._TemporaryIDPaiNotNull = this._IDPaiNotNull;
	    	          return this._TemporaryIDPaiNotNull;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIDPaiNotNull != value)
	    	              this._TemporaryIDPaiNotNull = value;
	    	    }
	    }	

	    #endregion Data Properties

	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<FilhaNotNullView> _FilhaNotNullViewList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_PaiNotNullView_FilhaNotNullView", "IDPaiNotNull", "IDPaiNotNull", IsForeignKey=false)]
	    [DataMember(Name = "FilhaNotNullViewList", EmitDefaultValue = true)]
	    public IEnumerable<FilhaNotNullView> FilhaNotNullViewList
	    {
	        get
	        {
	
	            if (this._FilhaNotNullViewList == null)
	            	this._FilhaNotNullViewList = new List<FilhaNotNullView>();
	
	            return this._FilhaNotNullViewList;
	        }
	        set
	        {
	            if (this._FilhaNotNullViewList != value)
	            {
	                this._FilhaNotNullViewList = value;
	                this.RaisePropertyChanged("FilhaNotNullViewList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ModeloVendaCliente.PaiNotNull").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(LinxTraining002.BM.PaiNotNull), QualifiedEntitySetName = "ModeloVendaCliente.PaiNotNull" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="PaiNotNull.Int", Source = "Int", Target = "Int", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.PaiNotNull", RelationPropertyName = "PaiNotNull" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="PaiNotNull.Byte", Source = "Byte", Target = "Byte", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.PaiNotNull", RelationPropertyName = "PaiNotNull" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="PaiNotNull.Long", Source = "Long", Target = "Long", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.PaiNotNull", RelationPropertyName = "PaiNotNull" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="PaiNotNull.Short", Source = "Short", Target = "Short", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.PaiNotNull", RelationPropertyName = "PaiNotNull" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="PaiNotNull.String", Source = "String", Target = "String", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.PaiNotNull", RelationPropertyName = "PaiNotNull" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="PaiNotNull.Boolean", Source = "Boolean", Target = "Boolean", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.PaiNotNull", RelationPropertyName = "PaiNotNull" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="PaiNotNull.Decimal", Source = "Decimal", Target = "Decimal", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.PaiNotNull", RelationPropertyName = "PaiNotNull" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="PaiNotNull.DateTime", Source = "DateTime", Target = "DateTime", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.PaiNotNull", RelationPropertyName = "PaiNotNull" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="PaiNotNull.StringChar", Source = "StringChar", Target = "StringChar", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.PaiNotNull", RelationPropertyName = "PaiNotNull" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="PaiNotNull.StringText", Source = "StringText", Target = "StringText", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.PaiNotNull", RelationPropertyName = "PaiNotNull" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="PaiNotNull.ID_PaiNotNull", Source = "IDPaiNotNull", Target = "ID_PaiNotNull", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.PaiNotNull", RelationPropertyName = "PaiNotNull" });

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
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="FilhaNotNull.ID_FilhaNotNull", IsUpdatable=false, EdmName="LinxTraining002.BM.ModeloVendaCliente")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[FilhaNotNullView];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IDFilhaNotNull];ReadOnly[false];Entities[FilhaNotNull:IDFilhaNotNull];SubQueryInfo[Select 1 From #ParentAlias#.FilhaNotNull_LISTA as #Alias#];EdmEntityName[FilhaNotNull];EntityRelations[PaiNotNull(PaiNotNull)];EdmParentEntityName[PaiNotNull];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "FilhaNotNullView")]
	[Serializable()]
	public partial class FilhaNotNullView : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(NotNullDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("PaiNotNullView");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IDPaiNotNull"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IDPaiNotNull));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load PaiNotNullView
	         this.PaiNotNullView = (from r in context.GetPaiNotNullViewByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Load Data Parent

	
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
	 

	    //Extensibility Partial Method Definitions For Boolean
	    partial void OnBooleanChanging(Boolean value);
	    partial void OnBooleanChanged();

	    private Boolean _Boolean;

	    [DataMember(IsRequired = true, Name = "Boolean", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Boolean", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[FilhaNotNull.Boolean];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="FilhaNotNull.Boolean")]
	    public Boolean Boolean
	    {
	    	    get
	    	    {
	    	          return _Boolean;
	    	    }
	    	    set
	    	    {
	    	          if (this._Boolean != value)
	    	          {
	    	              this.ValidateProperty("Boolean", value);
	    	              this.OnBooleanChanging(value);
	    	              this.RaiseDataMemberChanging("Boolean");
	    	              this._Boolean = value;
	    	              this.RaiseDataMemberChanged("Boolean");
	    	              this.OnBooleanChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Byte
	    partial void OnByteChanging(Byte value);
	    partial void OnByteChanged();

	    private Byte _Byte;

	    [DataMember(IsRequired = true, Name = "Byte", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Byte", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[FilhaNotNull.Byte];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="FilhaNotNull.Byte")]
	    public Byte Byte
	    {
	    	    get
	    	    {
	    	          return _Byte;
	    	    }
	    	    set
	    	    {
	    	          if (this._Byte != value)
	    	          {
	    	              this.ValidateProperty("Byte", value);
	    	              this.OnByteChanging(value);
	    	              this.RaiseDataMemberChanging("Byte");
	    	              this._Byte = value;
	    	              this.RaiseDataMemberChanged("Byte");
	    	              this.OnByteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DateTime
	    partial void OnDateTimeChanging(System.DateTime value);
	    partial void OnDateTimeChanged();

	    private System.DateTime _DateTime;

	    [DataMember(IsRequired = true, Name = "DateTime", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "DateTime", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[FilhaNotNull.DateTime];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="FilhaNotNull.DateTime")]
	    public System.DateTime DateTime
	    {
	    	    get
	    	    {
	    	          return _DateTime;
	    	    }
	    	    set
	    	    {
	    	          if (this._DateTime != value)
	    	          {
	    	              this.ValidateProperty("DateTime", value);
	    	              this.OnDateTimeChanging(value);
	    	              this.RaiseDataMemberChanging("DateTime");
	    	              this._DateTime = value;
	    	              this.RaiseDataMemberChanged("DateTime");
	    	              this.OnDateTimeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Decimal
	    partial void OnDecimalChanging(System.Decimal value);
	    partial void OnDecimalChanged();

	    private System.Decimal _Decimal;

	    [DataMember(IsRequired = true, Name = "Decimal", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[FilhaNotNull.Decimal];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="FilhaNotNull.Decimal")]
	    public System.Decimal Decimal
	    {
	    	    get
	    	    {
	    	          return _Decimal;
	    	    }
	    	    set
	    	    {
	    	          if (this._Decimal != value)
	    	          {
	    	              this.ValidateProperty("Decimal", value);
	    	              this.OnDecimalChanging(value);
	    	              this.RaiseDataMemberChanging("Decimal");
	    	              this._Decimal = value;
	    	              this.RaiseDataMemberChanged("Decimal");
	    	              this.OnDecimalChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IDFilhaNotNull
	    partial void OnIDFilhaNotNullChanging(Int32 value);
	    partial void OnIDFilhaNotNullChanged();

	    private Int32 _IDFilhaNotNull;

	    [DataMember(IsRequired = true, Name = "IDFilhaNotNull", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID FilhaNotNull", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[FilhaNotNull.ID_FilhaNotNull];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="FilhaNotNull.ID_FilhaNotNull")]
	    public Int32 IDFilhaNotNull
	    {
	    	    get
	    	    {
	    	          return _IDFilhaNotNull;
	    	    }
	    	    set
	    	    {
	    	          if (this._IDFilhaNotNull != value)
	    	          {
	    	              this.ValidateProperty("IDFilhaNotNull", value);
	    	              this.OnIDFilhaNotNullChanging(value);
	    	              this.RaiseDataMemberChanging("IDFilhaNotNull");
	    	              this._IDFilhaNotNull = value;
	    	              this.RaiseDataMemberChanged("IDFilhaNotNull");
	    	              this.OnIDFilhaNotNullChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IDPaiNotNull
	    partial void OnIDPaiNotNullChanging(Int32 value);
	    partial void OnIDPaiNotNullChanged();

	    private Int32 _IDPaiNotNull;

	    [DataMember(IsRequired = true, Name = "IDPaiNotNull", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID PaiNotNull", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[FilhaNotNull.PaiNotNull.ID_PaiNotNull];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="FilhaNotNull.PaiNotNull.ID_PaiNotNull")]
	    public Int32 IDPaiNotNull
	    {
	    	    get
	    	    {
	    	          return _IDPaiNotNull;
	    	    }
	    	    set
	    	    {
	    	          if (this._IDPaiNotNull != value)
	    	          {
	    	              this.ValidateProperty("IDPaiNotNull", value);
	    	              this.OnIDPaiNotNullChanging(value);
	    	              this.RaiseDataMemberChanging("IDPaiNotNull");
	    	              this._IDPaiNotNull = value;
	    	              this.RaiseDataMemberChanged("IDPaiNotNull");
	    	              this.OnIDPaiNotNullChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Int
	    partial void OnIntChanging(Int32 value);
	    partial void OnIntChanged();

	    private Int32 _Int;

	    [DataMember(IsRequired = true, Name = "Int", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[FilhaNotNull.Int];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="FilhaNotNull.Int")]
	    public Int32 Int
	    {
	    	    get
	    	    {
	    	          return _Int;
	    	    }
	    	    set
	    	    {
	    	          if (this._Int != value)
	    	          {
	    	              this.ValidateProperty("Int", value);
	    	              this.OnIntChanging(value);
	    	              this.RaiseDataMemberChanging("Int");
	    	              this._Int = value;
	    	              this.RaiseDataMemberChanged("Int");
	    	              this.OnIntChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Long
	    partial void OnLongChanging(Int64 value);
	    partial void OnLongChanged();

	    private Int64 _Long;

	    [DataMember(IsRequired = true, Name = "Long", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Long", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[FilhaNotNull.Long];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="FilhaNotNull.Long")]
	    public Int64 Long
	    {
	    	    get
	    	    {
	    	          return _Long;
	    	    }
	    	    set
	    	    {
	    	          if (this._Long != value)
	    	          {
	    	              this.ValidateProperty("Long", value);
	    	              this.OnLongChanging(value);
	    	              this.RaiseDataMemberChanging("Long");
	    	              this._Long = value;
	    	              this.RaiseDataMemberChanged("Long");
	    	              this.OnLongChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Short
	    partial void OnShortChanging(Int16 value);
	    partial void OnShortChanged();

	    private Int16 _Short;

	    [DataMember(IsRequired = true, Name = "Short", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Short", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[FilhaNotNull.Short];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="FilhaNotNull.Short")]
	    public Int16 Short
	    {
	    	    get
	    	    {
	    	          return _Short;
	    	    }
	    	    set
	    	    {
	    	          if (this._Short != value)
	    	          {
	    	              this.ValidateProperty("Short", value);
	    	              this.OnShortChanging(value);
	    	              this.RaiseDataMemberChanging("Short");
	    	              this._Short = value;
	    	              this.RaiseDataMemberChanged("Short");
	    	              this.OnShortChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For String
	    partial void OnStringChanging(System.String value);
	    partial void OnStringChanged();

	    private System.String _String;

	    [DataMember(IsRequired = true, Name = "String", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[FilhaNotNull.String];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="FilhaNotNull.String")]
	    public System.String String
	    {
	    	    get
	    	    {
	    	          return _String;
	    	    }
	    	    set
	    	    {
	    	          if (this._String != value)
	    	          {
	    	              this.ValidateProperty("String", value);
	    	              this.OnStringChanging(value);
	    	              this.RaiseDataMemberChanging("String");
	    	              this._String = value;
	    	              this.RaiseDataMemberChanged("String");
	    	              this.OnStringChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringChar
	    partial void OnStringCharChanging(System.String value);
	    partial void OnStringCharChanged();

	    private System.String _StringChar;

	    [DataMember(IsRequired = true, Name = "StringChar", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "StringChar", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[FilhaNotNull.StringChar];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="FilhaNotNull.StringChar")]
	    public System.String StringChar
	    {
	    	    get
	    	    {
	    	          return _StringChar;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringChar != value)
	    	          {
	    	              this.ValidateProperty("StringChar", value);
	    	              this.OnStringCharChanging(value);
	    	              this.RaiseDataMemberChanging("StringChar");
	    	              this._StringChar = value;
	    	              this.RaiseDataMemberChanged("StringChar");
	    	              this.OnStringCharChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringText
	    partial void OnStringTextChanging(System.String value);
	    partial void OnStringTextChanged();

	    private System.String _StringText;

	    [DataMember(IsRequired = true, Name = "StringText", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "StringText", Description="", Order = 14, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[FilhaNotNull.StringText];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="FilhaNotNull.StringText")]
	    public System.String StringText
	    {
	    	    get
	    	    {
	    	          return _StringText;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringText != value)
	    	          {
	    	              this.ValidateProperty("StringText", value);
	    	              this.OnStringTextChanging(value);
	    	              this.RaiseDataMemberChanging("StringText");
	    	              this._StringText = value;
	    	              this.RaiseDataMemberChanged("StringText");
	    	              this.OnStringTextChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIDFilhaNotNull;
	    [DataMember(Name = "TemporaryIDFilhaNotNull", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID FilhaNotNull (Tmp)", Description="Temporary Key", Order = 8, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIDFilhaNotNull
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIDFilhaNotNull.IsNullOrEmpty())
	    	                this._TemporaryIDFilhaNotNull = this._IDFilhaNotNull;
	    	          return this._TemporaryIDFilhaNotNull;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIDFilhaNotNull != value)
	    	              this._TemporaryIDFilhaNotNull = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private PaiNotNullView _PaiNotNullView;
	    [DataMember(Name = "PaiNotNullView", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_PaiNotNullView_FilhaNotNullView", "IDPaiNotNull", "IDPaiNotNull", IsForeignKey=true)]
	    public PaiNotNullView PaiNotNullView
	    {
	        get
	        {
	            return this._PaiNotNullView;
	        }
	        set
	        {
	            if (this._PaiNotNullView != value)
	            {
	                this._PaiNotNullView = value;
	                this.RaisePropertyChanged("PaiNotNullViewList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ModeloVendaCliente.FilhaNotNull").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(LinxTraining002.BM.FilhaNotNull), QualifiedEntitySetName = "ModeloVendaCliente.FilhaNotNull" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="FilhaNotNull.Int", Source = "Int", Target = "Int", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.FilhaNotNull", RelationPropertyName = "FilhaNotNull" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="FilhaNotNull.Byte", Source = "Byte", Target = "Byte", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.FilhaNotNull", RelationPropertyName = "FilhaNotNull" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="FilhaNotNull.Long", Source = "Long", Target = "Long", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.FilhaNotNull", RelationPropertyName = "FilhaNotNull" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="FilhaNotNull.Short", Source = "Short", Target = "Short", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.FilhaNotNull", RelationPropertyName = "FilhaNotNull" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="FilhaNotNull.String", Source = "String", Target = "String", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.FilhaNotNull", RelationPropertyName = "FilhaNotNull" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="FilhaNotNull.Boolean", Source = "Boolean", Target = "Boolean", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.FilhaNotNull", RelationPropertyName = "FilhaNotNull" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="FilhaNotNull.Decimal", Source = "Decimal", Target = "Decimal", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.FilhaNotNull", RelationPropertyName = "FilhaNotNull" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="FilhaNotNull.DateTime", Source = "DateTime", Target = "DateTime", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.FilhaNotNull", RelationPropertyName = "FilhaNotNull" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="FilhaNotNull.StringChar", Source = "StringChar", Target = "StringChar", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.FilhaNotNull", RelationPropertyName = "FilhaNotNull" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="FilhaNotNull.StringText", Source = "StringText", Target = "StringText", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.FilhaNotNull", RelationPropertyName = "FilhaNotNull" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="FilhaNotNull.ID_FilhaNotNull", Source = "IDFilhaNotNull", Target = "ID_FilhaNotNull", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.FilhaNotNull", RelationPropertyName = "FilhaNotNull" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="FilhaNotNull.PaiNotNull.ID_PaiNotNull", Source = "IDPaiNotNull", Target = "ID_PaiNotNull", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ModeloVendaCliente.PaiNotNull", RelationPropertyName = "PaiNotNull" });

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
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[TiposCamposFilhaView];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IDTiposCamposFilha];ReadOnly[false];Entities[TiposCamposFilha:IDTiposCamposFilha];SubQueryInfo[Select 1 From #ParentAlias#.TiposCamposFilha_LISTA as #Alias#];EdmEntityName[TiposCamposFilha];EntityRelations[TiposCampos(TiposCampos)];EdmParentEntityName[TiposCampos];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TiposCamposFilhaView")]
	[Serializable()]
	public partial class TiposCamposFilhaViewParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For Boolean
	    partial void OnBooleanChanging(System.Nullable<System.Boolean> value);
	    partial void OnBooleanChanged();

	    private System.Nullable<System.Boolean> _Boolean;

	    [DataMember(Name = "Boolean", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Boolean", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TiposCamposFilha.Boolean];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TiposCamposFilha.Boolean")]
	    public System.Nullable<System.Boolean> Boolean
	    {
	    	    get
	    	    {
	    	          return _Boolean;
	    	    }
	    	    set
	    	    {
	    	          if (this._Boolean != value)
	    	          {
	    	              this.ValidateProperty("Boolean", value);
	    	              this.OnBooleanChanging(value);
	    	              this.RaiseDataMemberChanging("Boolean");
	    	              this._Boolean = value;
	    	              this.RaiseDataMemberChanged("Boolean");
	    	              this.OnBooleanChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Byte
	    partial void OnByteChanging(System.Nullable<System.Byte> value);
	    partial void OnByteChanged();

	    private System.Nullable<System.Byte> _Byte;

	    [DataMember(Name = "Byte", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Byte", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TiposCamposFilha.Byte];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TiposCamposFilha.Byte")]
	    public System.Nullable<System.Byte> Byte
	    {
	    	    get
	    	    {
	    	          return _Byte;
	    	    }
	    	    set
	    	    {
	    	          if (this._Byte != value)
	    	          {
	    	              this.ValidateProperty("Byte", value);
	    	              this.OnByteChanging(value);
	    	              this.RaiseDataMemberChanging("Byte");
	    	              this._Byte = value;
	    	              this.RaiseDataMemberChanged("Byte");
	    	              this.OnByteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DateTime
	    partial void OnDateTimeChanging(System.Nullable<System.DateTime> value);
	    partial void OnDateTimeChanged();

	    private System.Nullable<System.DateTime> _DateTime;

	    [DataMember(Name = "DateTime", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "DateTime", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TiposCamposFilha.DateTime];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TiposCamposFilha.DateTime")]
	    public System.Nullable<System.DateTime> DateTime
	    {
	    	    get
	    	    {
	    	          return _DateTime;
	    	    }
	    	    set
	    	    {
	    	          if (this._DateTime != value)
	    	          {
	    	              this.ValidateProperty("DateTime", value);
	    	              this.OnDateTimeChanging(value);
	    	              this.RaiseDataMemberChanging("DateTime");
	    	              this._DateTime = value;
	    	              this.RaiseDataMemberChanged("DateTime");
	    	              this.OnDateTimeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Decimal
	    partial void OnDecimalChanging(System.Nullable<System.Decimal> value);
	    partial void OnDecimalChanged();

	    private System.Nullable<System.Decimal> _Decimal;

	    [DataMember(Name = "Decimal", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TiposCamposFilha.Decimal];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TiposCamposFilha.Decimal")]
	    public System.Nullable<System.Decimal> Decimal
	    {
	    	    get
	    	    {
	    	          return _Decimal;
	    	    }
	    	    set
	    	    {
	    	          if (this._Decimal != value)
	    	          {
	    	              this.ValidateProperty("Decimal", value);
	    	              this.OnDecimalChanging(value);
	    	              this.RaiseDataMemberChanging("Decimal");
	    	              this._Decimal = value;
	    	              this.RaiseDataMemberChanged("Decimal");
	    	              this.OnDecimalChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IDTiposCampos
	    partial void OnIDTiposCamposChanging(Int32 value);
	    partial void OnIDTiposCamposChanged();

	    private Int32 _IDTiposCampos;

	    [DataMember(IsRequired = true, Name = "IDTiposCampos", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID TiposCampos", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TiposCamposFilha.TiposCampos.ID_TiposCampos];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TiposCamposFilha.TiposCampos.ID_TiposCampos")]
	    public Int32 IDTiposCampos
	    {
	    	    get
	    	    {
	    	          return _IDTiposCampos;
	    	    }
	    	    set
	    	    {
	    	          if (this._IDTiposCampos != value)
	    	          {
	    	              this.ValidateProperty("IDTiposCampos", value);
	    	              this.OnIDTiposCamposChanging(value);
	    	              this.RaiseDataMemberChanging("IDTiposCampos");
	    	              this._IDTiposCampos = value;
	    	              this.RaiseDataMemberChanged("IDTiposCampos");
	    	              this.OnIDTiposCamposChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IDTiposCamposFilha
	    partial void OnIDTiposCamposFilhaChanging(Int32 value);
	    partial void OnIDTiposCamposFilhaChanged();

	    private Int32 _IDTiposCamposFilha;

	    [DataMember(IsRequired = true, Name = "IDTiposCamposFilha", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID TiposCamposFilha", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TiposCamposFilha.ID_TiposCamposFilha];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TiposCamposFilha.ID_TiposCamposFilha")]
	    public Int32 IDTiposCamposFilha
	    {
	    	    get
	    	    {
	    	          return _IDTiposCamposFilha;
	    	    }
	    	    set
	    	    {
	    	          if (this._IDTiposCamposFilha != value)
	    	          {
	    	              this.ValidateProperty("IDTiposCamposFilha", value);
	    	              this.OnIDTiposCamposFilhaChanging(value);
	    	              this.RaiseDataMemberChanging("IDTiposCamposFilha");
	    	              this._IDTiposCamposFilha = value;
	    	              this.RaiseDataMemberChanged("IDTiposCamposFilha");
	    	              this.OnIDTiposCamposFilhaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Int
	    partial void OnIntChanging(System.Nullable<System.Int32> value);
	    partial void OnIntChanged();

	    private System.Nullable<System.Int32> _Int;

	    [DataMember(Name = "Int", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TiposCamposFilha.Int];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TiposCamposFilha.Int")]
	    public System.Nullable<System.Int32> Int
	    {
	    	    get
	    	    {
	    	          return _Int;
	    	    }
	    	    set
	    	    {
	    	          if (this._Int != value)
	    	          {
	    	              this.ValidateProperty("Int", value);
	    	              this.OnIntChanging(value);
	    	              this.RaiseDataMemberChanging("Int");
	    	              this._Int = value;
	    	              this.RaiseDataMemberChanged("Int");
	    	              this.OnIntChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Long
	    partial void OnLongChanging(System.Nullable<System.Int64> value);
	    partial void OnLongChanged();

	    private System.Nullable<System.Int64> _Long;

	    [DataMember(Name = "Long", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Long", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TiposCamposFilha.Long];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TiposCamposFilha.Long")]
	    public System.Nullable<System.Int64> Long
	    {
	    	    get
	    	    {
	    	          return _Long;
	    	    }
	    	    set
	    	    {
	    	          if (this._Long != value)
	    	          {
	    	              this.ValidateProperty("Long", value);
	    	              this.OnLongChanging(value);
	    	              this.RaiseDataMemberChanging("Long");
	    	              this._Long = value;
	    	              this.RaiseDataMemberChanged("Long");
	    	              this.OnLongChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Short
	    partial void OnShortChanging(System.Nullable<System.Int16> value);
	    partial void OnShortChanged();

	    private System.Nullable<System.Int16> _Short;

	    [DataMember(Name = "Short", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Short", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TiposCamposFilha.Short];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TiposCamposFilha.Short")]
	    public System.Nullable<System.Int16> Short
	    {
	    	    get
	    	    {
	    	          return _Short;
	    	    }
	    	    set
	    	    {
	    	          if (this._Short != value)
	    	          {
	    	              this.ValidateProperty("Short", value);
	    	              this.OnShortChanging(value);
	    	              this.RaiseDataMemberChanging("Short");
	    	              this._Short = value;
	    	              this.RaiseDataMemberChanged("Short");
	    	              this.OnShortChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For String
	    partial void OnStringChanging(System.String value);
	    partial void OnStringChanged();

	    private System.String _String;

	    [DataMember(Name = "String", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[TstDomainString];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TiposCamposFilha.String];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TiposCamposFilha.String")]
	    public System.String String
	    {
	    	    get
	    	    {
	    	          return _String;
	    	    }
	    	    set
	    	    {
	    	          if (this._String != value)
	    	          {
	    	              this.ValidateProperty("String", value);
	    	              this.OnStringChanging(value);
	    	              this.RaiseDataMemberChanging("String");
	    	              this._String = value;
	    	              this.RaiseDataMemberChanged("String");
	    	              this.OnStringChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringChar
	    partial void OnStringCharChanging(System.String value);
	    partial void OnStringCharChanged();

	    private System.String _StringChar;

	    [DataMember(Name = "StringChar", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "StringChar", Description="", Order = 14, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TiposCamposFilha.StringChar];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TiposCamposFilha.StringChar")]
	    public System.String StringChar
	    {
	    	    get
	    	    {
	    	          return _StringChar;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringChar != value)
	    	          {
	    	              this.ValidateProperty("StringChar", value);
	    	              this.OnStringCharChanging(value);
	    	              this.RaiseDataMemberChanging("StringChar");
	    	              this._StringChar = value;
	    	              this.RaiseDataMemberChanged("StringChar");
	    	              this.OnStringCharChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringText
	    partial void OnStringTextChanging(System.String value);
	    partial void OnStringTextChanged();

	    private System.String _StringText;

	    [DataMember(Name = "StringText", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "StringText", Description="", Order = 15, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TiposCamposFilha.StringText];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TiposCamposFilha.StringText")]
	    public System.String StringText
	    {
	    	    get
	    	    {
	    	          return _StringText;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringText != value)
	    	          {
	    	              this.ValidateProperty("StringText", value);
	    	              this.OnStringTextChanging(value);
	    	              this.RaiseDataMemberChanging("StringText");
	    	              this._StringText = value;
	    	              this.RaiseDataMemberChanged("StringText");
	    	              this.OnStringTextChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ModeloVendaCliente.TiposCamposFilha").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(LinxTraining002.BM.TiposCamposFilha), QualifiedEntitySetName = "ModeloVendaCliente.TiposCamposFilha" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TiposCamposFilha.Int", Source = "Int", Target = "Int", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TiposCamposFilha", RelationPropertyName = "TiposCamposFilha" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TiposCamposFilha.Byte", Source = "Byte", Target = "Byte", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TiposCamposFilha", RelationPropertyName = "TiposCamposFilha" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TiposCamposFilha.Long", Source = "Long", Target = "Long", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TiposCamposFilha", RelationPropertyName = "TiposCamposFilha" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TiposCamposFilha.Short", Source = "Short", Target = "Short", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TiposCamposFilha", RelationPropertyName = "TiposCamposFilha" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TiposCamposFilha.String", Source = "String", Target = "String", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TiposCamposFilha", RelationPropertyName = "TiposCamposFilha" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TiposCamposFilha.Boolean", Source = "Boolean", Target = "Boolean", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TiposCamposFilha", RelationPropertyName = "TiposCamposFilha" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TiposCamposFilha.Decimal", Source = "Decimal", Target = "Decimal", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TiposCamposFilha", RelationPropertyName = "TiposCamposFilha" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TiposCamposFilha.DateTime", Source = "DateTime", Target = "DateTime", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TiposCamposFilha", RelationPropertyName = "TiposCamposFilha" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TiposCamposFilha.StringChar", Source = "StringChar", Target = "StringChar", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TiposCamposFilha", RelationPropertyName = "TiposCamposFilha" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TiposCamposFilha.StringText", Source = "StringText", Target = "StringText", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TiposCamposFilha", RelationPropertyName = "TiposCamposFilha" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TiposCamposFilha.ID_TiposCamposFilha", Source = "IDTiposCamposFilha", Target = "ID_TiposCamposFilha", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TiposCamposFilha", RelationPropertyName = "TiposCamposFilha" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TiposCamposFilha.TiposCampos.ID_TiposCampos", Source = "IDTiposCampos", Target = "ID_TiposCampos", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ModeloVendaCliente.TiposCampos", RelationPropertyName = "TiposCampos" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetStringValues()
	    {
	    	    return LinxTraining001.BV.Domains.TstDomainString.GetValues();
	    }
	    private string _stringName;
	    [DataMember(IsRequired = false, Name = "StringName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "String", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string StringName
	    {
	    	    get { if (this.String.IsNullOrEmpty()) { _stringName = String.Empty; } else { string key = this.String.ToString(); var dmValues = this.GetStringValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _stringName) _stringName = domainName; } return _stringName; } set { _stringName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[FilhaNotNullView];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IDFilhaNotNull];ReadOnly[false];Entities[FilhaNotNull:IDFilhaNotNull];SubQueryInfo[Select 1 From #ParentAlias#.FilhaNotNull_LISTA as #Alias#];EdmEntityName[FilhaNotNull];EntityRelations[PaiNotNull(PaiNotNull)];EdmParentEntityName[PaiNotNull];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "FilhaNotNullView")]
	[Serializable()]
	public partial class FilhaNotNullViewParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For Boolean
	    partial void OnBooleanChanging(Boolean value);
	    partial void OnBooleanChanged();

	    private Boolean _Boolean;

	    [DataMember(IsRequired = true, Name = "Boolean", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Boolean", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[FilhaNotNull.Boolean];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="FilhaNotNull.Boolean")]
	    public Boolean Boolean
	    {
	    	    get
	    	    {
	    	          return _Boolean;
	    	    }
	    	    set
	    	    {
	    	          if (this._Boolean != value)
	    	          {
	    	              this.ValidateProperty("Boolean", value);
	    	              this.OnBooleanChanging(value);
	    	              this.RaiseDataMemberChanging("Boolean");
	    	              this._Boolean = value;
	    	              this.RaiseDataMemberChanged("Boolean");
	    	              this.OnBooleanChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Byte
	    partial void OnByteChanging(Byte value);
	    partial void OnByteChanged();

	    private Byte _Byte;

	    [DataMember(IsRequired = true, Name = "Byte", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Byte", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[FilhaNotNull.Byte];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="FilhaNotNull.Byte")]
	    public Byte Byte
	    {
	    	    get
	    	    {
	    	          return _Byte;
	    	    }
	    	    set
	    	    {
	    	          if (this._Byte != value)
	    	          {
	    	              this.ValidateProperty("Byte", value);
	    	              this.OnByteChanging(value);
	    	              this.RaiseDataMemberChanging("Byte");
	    	              this._Byte = value;
	    	              this.RaiseDataMemberChanged("Byte");
	    	              this.OnByteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DateTime
	    partial void OnDateTimeChanging(System.DateTime value);
	    partial void OnDateTimeChanged();

	    private System.DateTime _DateTime;

	    [DataMember(IsRequired = true, Name = "DateTime", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "DateTime", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[FilhaNotNull.DateTime];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="FilhaNotNull.DateTime")]
	    public System.DateTime DateTime
	    {
	    	    get
	    	    {
	    	          return _DateTime;
	    	    }
	    	    set
	    	    {
	    	          if (this._DateTime != value)
	    	          {
	    	              this.ValidateProperty("DateTime", value);
	    	              this.OnDateTimeChanging(value);
	    	              this.RaiseDataMemberChanging("DateTime");
	    	              this._DateTime = value;
	    	              this.RaiseDataMemberChanged("DateTime");
	    	              this.OnDateTimeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Decimal
	    partial void OnDecimalChanging(System.Decimal value);
	    partial void OnDecimalChanged();

	    private System.Decimal _Decimal;

	    [DataMember(IsRequired = true, Name = "Decimal", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[FilhaNotNull.Decimal];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="FilhaNotNull.Decimal")]
	    public System.Decimal Decimal
	    {
	    	    get
	    	    {
	    	          return _Decimal;
	    	    }
	    	    set
	    	    {
	    	          if (this._Decimal != value)
	    	          {
	    	              this.ValidateProperty("Decimal", value);
	    	              this.OnDecimalChanging(value);
	    	              this.RaiseDataMemberChanging("Decimal");
	    	              this._Decimal = value;
	    	              this.RaiseDataMemberChanged("Decimal");
	    	              this.OnDecimalChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IDFilhaNotNull
	    partial void OnIDFilhaNotNullChanging(Int32 value);
	    partial void OnIDFilhaNotNullChanged();

	    private Int32 _IDFilhaNotNull;

	    [DataMember(IsRequired = true, Name = "IDFilhaNotNull", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID FilhaNotNull", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[FilhaNotNull.ID_FilhaNotNull];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="FilhaNotNull.ID_FilhaNotNull")]
	    public Int32 IDFilhaNotNull
	    {
	    	    get
	    	    {
	    	          return _IDFilhaNotNull;
	    	    }
	    	    set
	    	    {
	    	          if (this._IDFilhaNotNull != value)
	    	          {
	    	              this.ValidateProperty("IDFilhaNotNull", value);
	    	              this.OnIDFilhaNotNullChanging(value);
	    	              this.RaiseDataMemberChanging("IDFilhaNotNull");
	    	              this._IDFilhaNotNull = value;
	    	              this.RaiseDataMemberChanged("IDFilhaNotNull");
	    	              this.OnIDFilhaNotNullChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IDPaiNotNull
	    partial void OnIDPaiNotNullChanging(Int32 value);
	    partial void OnIDPaiNotNullChanged();

	    private Int32 _IDPaiNotNull;

	    [DataMember(IsRequired = true, Name = "IDPaiNotNull", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID PaiNotNull", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[FilhaNotNull.PaiNotNull.ID_PaiNotNull];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="FilhaNotNull.PaiNotNull.ID_PaiNotNull")]
	    public Int32 IDPaiNotNull
	    {
	    	    get
	    	    {
	    	          return _IDPaiNotNull;
	    	    }
	    	    set
	    	    {
	    	          if (this._IDPaiNotNull != value)
	    	          {
	    	              this.ValidateProperty("IDPaiNotNull", value);
	    	              this.OnIDPaiNotNullChanging(value);
	    	              this.RaiseDataMemberChanging("IDPaiNotNull");
	    	              this._IDPaiNotNull = value;
	    	              this.RaiseDataMemberChanged("IDPaiNotNull");
	    	              this.OnIDPaiNotNullChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Int
	    partial void OnIntChanging(Int32 value);
	    partial void OnIntChanged();

	    private Int32 _Int;

	    [DataMember(IsRequired = true, Name = "Int", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[FilhaNotNull.Int];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="FilhaNotNull.Int")]
	    public Int32 Int
	    {
	    	    get
	    	    {
	    	          return _Int;
	    	    }
	    	    set
	    	    {
	    	          if (this._Int != value)
	    	          {
	    	              this.ValidateProperty("Int", value);
	    	              this.OnIntChanging(value);
	    	              this.RaiseDataMemberChanging("Int");
	    	              this._Int = value;
	    	              this.RaiseDataMemberChanged("Int");
	    	              this.OnIntChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Long
	    partial void OnLongChanging(Int64 value);
	    partial void OnLongChanged();

	    private Int64 _Long;

	    [DataMember(IsRequired = true, Name = "Long", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Long", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[FilhaNotNull.Long];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="FilhaNotNull.Long")]
	    public Int64 Long
	    {
	    	    get
	    	    {
	    	          return _Long;
	    	    }
	    	    set
	    	    {
	    	          if (this._Long != value)
	    	          {
	    	              this.ValidateProperty("Long", value);
	    	              this.OnLongChanging(value);
	    	              this.RaiseDataMemberChanging("Long");
	    	              this._Long = value;
	    	              this.RaiseDataMemberChanged("Long");
	    	              this.OnLongChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Short
	    partial void OnShortChanging(Int16 value);
	    partial void OnShortChanged();

	    private Int16 _Short;

	    [DataMember(IsRequired = true, Name = "Short", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Short", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[FilhaNotNull.Short];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="FilhaNotNull.Short")]
	    public Int16 Short
	    {
	    	    get
	    	    {
	    	          return _Short;
	    	    }
	    	    set
	    	    {
	    	          if (this._Short != value)
	    	          {
	    	              this.ValidateProperty("Short", value);
	    	              this.OnShortChanging(value);
	    	              this.RaiseDataMemberChanging("Short");
	    	              this._Short = value;
	    	              this.RaiseDataMemberChanged("Short");
	    	              this.OnShortChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For String
	    partial void OnStringChanging(System.String value);
	    partial void OnStringChanged();

	    private System.String _String;

	    [DataMember(IsRequired = true, Name = "String", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[FilhaNotNull.String];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="FilhaNotNull.String")]
	    public System.String String
	    {
	    	    get
	    	    {
	    	          return _String;
	    	    }
	    	    set
	    	    {
	    	          if (this._String != value)
	    	          {
	    	              this.ValidateProperty("String", value);
	    	              this.OnStringChanging(value);
	    	              this.RaiseDataMemberChanging("String");
	    	              this._String = value;
	    	              this.RaiseDataMemberChanged("String");
	    	              this.OnStringChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringChar
	    partial void OnStringCharChanging(System.String value);
	    partial void OnStringCharChanged();

	    private System.String _StringChar;

	    [DataMember(IsRequired = true, Name = "StringChar", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "StringChar", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[FilhaNotNull.StringChar];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="FilhaNotNull.StringChar")]
	    public System.String StringChar
	    {
	    	    get
	    	    {
	    	          return _StringChar;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringChar != value)
	    	          {
	    	              this.ValidateProperty("StringChar", value);
	    	              this.OnStringCharChanging(value);
	    	              this.RaiseDataMemberChanging("StringChar");
	    	              this._StringChar = value;
	    	              this.RaiseDataMemberChanged("StringChar");
	    	              this.OnStringCharChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringText
	    partial void OnStringTextChanging(System.String value);
	    partial void OnStringTextChanged();

	    private System.String _StringText;

	    [DataMember(IsRequired = true, Name = "StringText", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "StringText", Description="", Order = 14, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[FilhaNotNull.StringText];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="FilhaNotNull.StringText")]
	    public System.String StringText
	    {
	    	    get
	    	    {
	    	          return _StringText;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringText != value)
	    	          {
	    	              this.ValidateProperty("StringText", value);
	    	              this.OnStringTextChanging(value);
	    	              this.RaiseDataMemberChanging("StringText");
	    	              this._StringText = value;
	    	              this.RaiseDataMemberChanged("StringText");
	    	              this.OnStringTextChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ModeloVendaCliente.FilhaNotNull").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(LinxTraining002.BM.FilhaNotNull), QualifiedEntitySetName = "ModeloVendaCliente.FilhaNotNull" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="FilhaNotNull.Int", Source = "Int", Target = "Int", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.FilhaNotNull", RelationPropertyName = "FilhaNotNull" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="FilhaNotNull.Byte", Source = "Byte", Target = "Byte", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.FilhaNotNull", RelationPropertyName = "FilhaNotNull" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="FilhaNotNull.Long", Source = "Long", Target = "Long", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.FilhaNotNull", RelationPropertyName = "FilhaNotNull" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="FilhaNotNull.Short", Source = "Short", Target = "Short", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.FilhaNotNull", RelationPropertyName = "FilhaNotNull" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="FilhaNotNull.String", Source = "String", Target = "String", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.FilhaNotNull", RelationPropertyName = "FilhaNotNull" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="FilhaNotNull.Boolean", Source = "Boolean", Target = "Boolean", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.FilhaNotNull", RelationPropertyName = "FilhaNotNull" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="FilhaNotNull.Decimal", Source = "Decimal", Target = "Decimal", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.FilhaNotNull", RelationPropertyName = "FilhaNotNull" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="FilhaNotNull.DateTime", Source = "DateTime", Target = "DateTime", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.FilhaNotNull", RelationPropertyName = "FilhaNotNull" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="FilhaNotNull.StringChar", Source = "StringChar", Target = "StringChar", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.FilhaNotNull", RelationPropertyName = "FilhaNotNull" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="FilhaNotNull.StringText", Source = "StringText", Target = "StringText", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.FilhaNotNull", RelationPropertyName = "FilhaNotNull" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="FilhaNotNull.ID_FilhaNotNull", Source = "IDFilhaNotNull", Target = "ID_FilhaNotNull", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.FilhaNotNull", RelationPropertyName = "FilhaNotNull" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="FilhaNotNull.PaiNotNull.ID_PaiNotNull", Source = "IDPaiNotNull", Target = "ID_PaiNotNull", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ModeloVendaCliente.PaiNotNull", RelationPropertyName = "PaiNotNull" });

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
	[DomainIdentifier("ProcessorOverviewNotNullDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class NotNullDomainService : DomainService, IDataServiceContext 
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

	
	    private LinxTraining002.BM.ModeloVendaCliente _dbContext;
	    protected LinxTraining002.BM.ModeloVendaCliente DbContext 
	    { 
	    	get 
	    	{
	        	if (this._dbContext == null)
	        	{
	        		this._dbContext = new LinxTraining002.BM.ModeloVendaCliente(connectionString, this.Headers);
	        		((System.Data.Entity.Infrastructure.IObjectContextAdapter)this._dbContext).ObjectContext.CommandTimeout = 180;
	        	}
	        	return this._dbContext;
	    	}
	    }

	    public string GetModelAssemblyName()
	    {
	        return typeof(LinxTraining002.BM.ModeloVendaCliente).Assembly.FullName;
	    }

	    public System.Data.Entity.Database Database
	    {
	        get { return this.DbContext.Database; }
	    }

		
	    public NotNullDomainService() : this("", null, null){ }
	    public NotNullDomainService(string connectionString) : this(connectionString, null, null) { }
	    public NotNullDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public NotNullDomainService(LinxTraining002.BM.ModeloVendaCliente dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public NotNullDomainService(string connectionString, LinxTraining002.BM.ModeloVendaCliente dataContext, Dictionary<string, string> headers) : base() 
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
	    public LinxTraining002.BM.ModeloVendaCliente GetEDM()
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

		
 
 	        bool createNewChangeSet = false;
 
 	        //Adjust data hierarchy
 	        var _TiposCamposViewElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TiposCamposView && e.Entity.GetType().Name == "TiposCamposView" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _TiposCamposViewElements)
 	           if (((TiposCamposView)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 	        var _PaiNotNullViewElements = changeSet.ChangeSetEntries.Where(e => e.Entity is PaiNotNullView && e.Entity.GetType().Name == "PaiNotNullView" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _PaiNotNullViewElements)
 	           if (((PaiNotNullView)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 
 	        //Remove inconsistent details
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TiposCamposFilhaView && e.Entity.GetType().Name == "TiposCamposFilhaView" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is FilhaNotNullView && e.Entity.GetType().Name == "FilhaNotNullView" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 
 	        if (createNewChangeSet) changeSet = new ChangeSet(changeSet.ChangeSetEntries.Where(e => e.Operation != DomainOperation.None));
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
	
		

	        if (entityName.InList("LinxTraining001.BV.NotNull.TiposCamposView"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TiposCamposView",
	        			NameSpace = "LinxTraining001.BV.NotNull",
	        			ParentClassName = null,	
	        			DisplayName = "TiposCamposView",
	        			ClearMethodName = "ClearTiposCamposView",
	        			QueryMethodName  = "GetPagedTiposCamposView",	
	        			CountingMethodName  = "GetTiposCamposView" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("LinxTraining001.BV.NotNull.TiposCamposView"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("LinxTraining001.BV.NotNull.TiposCamposView"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("LinxTraining001.BV.NotNull.TiposCamposView", "LinxTraining001.BV.NotNull.TiposCamposFilhaView"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TiposCamposFilhaView" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "LinxTraining001.BV.NotNull",
	        			ParentClassName = "TiposCamposView",	
	        			DisplayName = "TiposCamposFilhaView",
	        			ClearMethodName = "ClearTiposCamposFilhaView" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTiposCamposFilhaView" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTiposCamposFilhaView" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("LinxTraining001.BV.NotNull.TiposCamposFilhaView"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("LinxTraining001.BV.NotNull.TiposCamposFilhaView" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("LinxTraining001.BV.NotNull.PaiNotNullView"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "PaiNotNullView",
	        			NameSpace = "LinxTraining001.BV.NotNull",
	        			ParentClassName = null,	
	        			DisplayName = "PaiNotNullView",
	        			ClearMethodName = "ClearPaiNotNullView",
	        			QueryMethodName  = "GetPagedPaiNotNullView",	
	        			CountingMethodName  = "GetPaiNotNullView" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("LinxTraining001.BV.NotNull.PaiNotNullView"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("LinxTraining001.BV.NotNull.PaiNotNullView"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("LinxTraining001.BV.NotNull.PaiNotNullView", "LinxTraining001.BV.NotNull.FilhaNotNullView"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "FilhaNotNullView" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "LinxTraining001.BV.NotNull",
	        			ParentClassName = "PaiNotNullView",	
	        			DisplayName = "FilhaNotNullView",
	        			ClearMethodName = "ClearFilhaNotNullView" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedFilhaNotNullView" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetFilhaNotNullView" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("LinxTraining001.BV.NotNull.FilhaNotNullView"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("LinxTraining001.BV.NotNull.FilhaNotNullView" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
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


             return new string[] { "LinxTraining001_notNullService", Linx.Tools.AssemblyHelper.ReadResourceContent("LinxTraining001.BV.ClientResources.notNullService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	

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
	    //Clear TiposCamposView.
	    public IEnumerable<TiposCamposView> ClearTiposCamposView()
	    {
	        List<TiposCamposView> result = new List<TiposCamposView>();
	        result.Add(new TiposCamposView());	
			
	        result[0].TiposCamposFilhaViewList = new List<TiposCamposFilhaView>();
	        ((List<TiposCamposFilhaView>)result[0].TiposCamposFilhaViewList).Add(new TiposCamposFilhaView());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TiposCamposFilhaView.
	    public IEnumerable<TiposCamposFilhaView> ClearTiposCamposFilhaView()
	    {
	        List<TiposCamposFilhaView> result = new List<TiposCamposFilhaView>();
	        result.Add(new TiposCamposFilhaView());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear PaiNotNullView.
	    public IEnumerable<PaiNotNullView> ClearPaiNotNullView()
	    {
	        List<PaiNotNullView> result = new List<PaiNotNullView>();
	        result.Add(new PaiNotNullView());	
			
	        result[0].FilhaNotNullViewList = new List<FilhaNotNullView>();
	        ((List<FilhaNotNullView>)result[0].FilhaNotNullViewList).Add(new FilhaNotNullView());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear FilhaNotNullView.
	    public IEnumerable<FilhaNotNullView> ClearFilhaNotNullView()
	    {
	        List<FilhaNotNullView> result = new List<FilhaNotNullView>();
	        result.Add(new FilhaNotNullView());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    [TiposCamposViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TiposCamposView.
	    public IQueryable<TiposCamposView> GetTiposCamposView()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTiposCamposView")))
 	        {
 	             AuthorizationResult authorizationResult = (new TiposCamposViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<TiposCamposView> result = 
	            (from entity0 in this.DbContext.TiposCampos
	            
	            	
	            select new TiposCamposView()		
	            {
	            
                Boolean = entity0.Boolean
                , Byte = entity0.Byte
                , DateTime = entity0.DateTime
                , Decimal = entity0.Decimal
                , IDTiposCampos = entity0.ID_TiposCampos
                , Int = entity0.Int
                , Long = entity0.Long
                , Short = entity0.Short
                , String = entity0.String
                , StringChar = entity0.StringChar
                , StringText = entity0.StringText
			
                ,TiposCamposFilhaViewList = 
	                        (from entity1 in entity0.TiposCamposFilha_LISTA
                                  let entity1Al1 = entity1.TiposCampos
	                        
	                        	
	                        select new TiposCamposFilhaView()
	                        {
	                        
                                Boolean = entity1.Boolean
                                , Byte = entity1.Byte
                                , DateTime = entity1.DateTime
                                , Decimal = entity1.Decimal
                                , IDTiposCampos = entity1Al1.ID_TiposCampos
                                , IDTiposCamposFilha = entity1.ID_TiposCamposFilha
                                , Int = entity1.Int
                                , Long = entity1.Long
                                , Short = entity1.Short
                                , String = entity1.String
                                , StringName = ((entity1.String) == "01" ? "String 01" : ((entity1.String) == "01A" ? "String 01A" : ((entity1.String) == "02" ? "String 02" : ((entity1.String) == "A" ? "String A" : ((entity1.String) == "ststdd" ? "NewString" : ((entity1.String) == "sttst" ? "String Teste" : ((entity1.String) == "ValString" ? "ValString" : "")))))))
                                , StringChar = entity1.StringChar
                                , StringText = entity1.StringText
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TiposCamposFilhaViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TiposCamposFilhaView.
	    public IQueryable<TiposCamposFilhaView> GetTiposCamposFilhaView()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTiposCamposFilhaView")))
 	        {
 	             AuthorizationResult authorizationResult = (new TiposCamposFilhaViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<TiposCamposFilhaView> result = 
	            (from entity0 in this.DbContext.TiposCamposFilha
                  let entity0Al1 = entity0.TiposCampos
	            
	            	
	            select new TiposCamposFilhaView()		
	            {
	            
                Boolean = entity0.Boolean
                , Byte = entity0.Byte
                , DateTime = entity0.DateTime
                , Decimal = entity0.Decimal
                , IDTiposCampos = entity0Al1.ID_TiposCampos
                , IDTiposCamposFilha = entity0.ID_TiposCamposFilha
                , Int = entity0.Int
                , Long = entity0.Long
                , Short = entity0.Short
                , String = entity0.String
                , StringName = ((entity0.String) == "01" ? "String 01" : ((entity0.String) == "01A" ? "String 01A" : ((entity0.String) == "02" ? "String 02" : ((entity0.String) == "A" ? "String A" : ((entity0.String) == "ststdd" ? "NewString" : ((entity0.String) == "sttst" ? "String Teste" : ((entity0.String) == "ValString" ? "ValString" : "")))))))
                , StringChar = entity0.StringChar
                , StringText = entity0.StringText
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TiposCamposViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TiposCamposViewNoAssociations.
	    public IQueryable<TiposCamposView> GetTiposCamposViewNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTiposCamposViewNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new TiposCamposViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<TiposCamposView> result = 
	            (from entity0 in this.DbContext.TiposCampos
	            
	            	
	            select new TiposCamposView()		
	            {
	            
                Boolean = entity0.Boolean
                , Byte = entity0.Byte
                , DateTime = entity0.DateTime
                , Decimal = entity0.Decimal
                , IDTiposCampos = entity0.ID_TiposCampos
                , Int = entity0.Int
                , Long = entity0.Long
                , Short = entity0.Short
                , String = entity0.String
                , StringChar = entity0.StringChar
                , StringText = entity0.StringText
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TiposCamposFilhaViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TiposCamposFilhaViewNoAssociations.
	    public IQueryable<TiposCamposFilhaView> GetTiposCamposFilhaViewNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTiposCamposFilhaViewNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new TiposCamposFilhaViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<TiposCamposFilhaView> result = 
	            (from entity0 in this.DbContext.TiposCamposFilha
                  let entity0Al1 = entity0.TiposCampos
	            
	            	
	            select new TiposCamposFilhaView()		
	            {
	            
                Boolean = entity0.Boolean
                , Byte = entity0.Byte
                , DateTime = entity0.DateTime
                , Decimal = entity0.Decimal
                , IDTiposCampos = entity0Al1.ID_TiposCampos
                , IDTiposCamposFilha = entity0.ID_TiposCamposFilha
                , Int = entity0.Int
                , Long = entity0.Long
                , Short = entity0.Short
                , String = entity0.String
                , StringName = ((entity0.String) == "01" ? "String 01" : ((entity0.String) == "01A" ? "String 01A" : ((entity0.String) == "02" ? "String 02" : ((entity0.String) == "A" ? "String A" : ((entity0.String) == "ststdd" ? "NewString" : ((entity0.String) == "sttst" ? "String Teste" : ((entity0.String) == "ValString" ? "ValString" : "")))))))
                , StringChar = entity0.StringChar
                , StringText = entity0.StringText
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [PaiNotNullViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PaiNotNullView.
	    public IQueryable<PaiNotNullView> GetPaiNotNullView()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPaiNotNullView")))
 	        {
 	             AuthorizationResult authorizationResult = (new PaiNotNullViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<PaiNotNullView> result = 
	            (from entity0 in this.DbContext.PaiNotNull
	            
	            	
	            select new PaiNotNullView()		
	            {
	            
                Boolean = entity0.Boolean
                , Byte = entity0.Byte
                , DateTime = entity0.DateTime
                , Decimal = entity0.Decimal
                , IDPaiNotNull = entity0.ID_PaiNotNull
                , Int = entity0.Int
                , Long = entity0.Long
                , Short = entity0.Short
                , String = entity0.String
                , StringChar = entity0.StringChar
                , StringText = entity0.StringText
			
                ,FilhaNotNullViewList = 
	                        (from entity1 in entity0.FilhaNotNull_LISTA
                                  let entity1Al1 = entity1.PaiNotNull
	                        
	                        	
	                        select new FilhaNotNullView()
	                        {
	                        
                                Boolean = entity1.Boolean
                                , Byte = entity1.Byte
                                , DateTime = entity1.DateTime
                                , Decimal = entity1.Decimal
                                , IDFilhaNotNull = entity1.ID_FilhaNotNull
                                , IDPaiNotNull = entity1Al1.ID_PaiNotNull
                                , Int = entity1.Int
                                , Long = entity1.Long
                                , Short = entity1.Short
                                , String = entity1.String
                                , StringChar = entity1.StringChar
                                , StringText = entity1.StringText
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [FilhaNotNullViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get FilhaNotNullView.
	    public IQueryable<FilhaNotNullView> GetFilhaNotNullView()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetFilhaNotNullView")))
 	        {
 	             AuthorizationResult authorizationResult = (new FilhaNotNullViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<FilhaNotNullView> result = 
	            (from entity0 in this.DbContext.FilhaNotNull
                  let entity0Al1 = entity0.PaiNotNull
	            
	            	
	            select new FilhaNotNullView()		
	            {
	            
                Boolean = entity0.Boolean
                , Byte = entity0.Byte
                , DateTime = entity0.DateTime
                , Decimal = entity0.Decimal
                , IDFilhaNotNull = entity0.ID_FilhaNotNull
                , IDPaiNotNull = entity0Al1.ID_PaiNotNull
                , Int = entity0.Int
                , Long = entity0.Long
                , Short = entity0.Short
                , String = entity0.String
                , StringChar = entity0.StringChar
                , StringText = entity0.StringText
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [PaiNotNullViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PaiNotNullViewNoAssociations.
	    public IQueryable<PaiNotNullView> GetPaiNotNullViewNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPaiNotNullViewNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new PaiNotNullViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<PaiNotNullView> result = 
	            (from entity0 in this.DbContext.PaiNotNull
	            
	            	
	            select new PaiNotNullView()		
	            {
	            
                Boolean = entity0.Boolean
                , Byte = entity0.Byte
                , DateTime = entity0.DateTime
                , Decimal = entity0.Decimal
                , IDPaiNotNull = entity0.ID_PaiNotNull
                , Int = entity0.Int
                , Long = entity0.Long
                , Short = entity0.Short
                , String = entity0.String
                , StringChar = entity0.StringChar
                , StringText = entity0.StringText
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [FilhaNotNullViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get FilhaNotNullViewNoAssociations.
	    public IQueryable<FilhaNotNullView> GetFilhaNotNullViewNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetFilhaNotNullViewNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new FilhaNotNullViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<FilhaNotNullView> result = 
	            (from entity0 in this.DbContext.FilhaNotNull
                  let entity0Al1 = entity0.PaiNotNull
	            
	            	
	            select new FilhaNotNullView()		
	            {
	            
                Boolean = entity0.Boolean
                , Byte = entity0.Byte
                , DateTime = entity0.DateTime
                , Decimal = entity0.Decimal
                , IDFilhaNotNull = entity0.ID_FilhaNotNull
                , IDPaiNotNull = entity0Al1.ID_PaiNotNull
                , Int = entity0.Int
                , Long = entity0.Long
                , Short = entity0.Short
                , String = entity0.String
                , StringChar = entity0.StringChar
                , StringText = entity0.StringText
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	//Add filtering disabled property for TiposCampos
	    	string[] bmDisabledTiposCamposViewList = this.GetEDM().GetFilteringDisabledList("TiposCampos");
	    	if (bmDisabledTiposCamposViewList.Length > 0)
	    	{
	
	    		if (bmDisabledTiposCamposViewList.Contains("TiposCampos.Boolean"))
	    		{
	    			result.Add("TiposCamposView|Boolean");
	    			result.Add("TiposCamposView|TiposCampos.Boolean");
	    		}
	
	    		if (bmDisabledTiposCamposViewList.Contains("TiposCampos.Byte"))
	    		{
	    			result.Add("TiposCamposView|Byte");
	    			result.Add("TiposCamposView|TiposCampos.Byte");
	    		}
	
	    		if (bmDisabledTiposCamposViewList.Contains("TiposCampos.DateTime"))
	    		{
	    			result.Add("TiposCamposView|DateTime");
	    			result.Add("TiposCamposView|TiposCampos.DateTime");
	    		}
	
	    		if (bmDisabledTiposCamposViewList.Contains("TiposCampos.Decimal"))
	    		{
	    			result.Add("TiposCamposView|Decimal");
	    			result.Add("TiposCamposView|TiposCampos.Decimal");
	    		}
	
	    		if (bmDisabledTiposCamposViewList.Contains("TiposCampos.ID_TiposCampos"))
	    		{
	    			result.Add("TiposCamposView|IDTiposCampos");
	    			result.Add("TiposCamposView|TiposCampos.ID_TiposCampos");
	    		}
	
	    		if (bmDisabledTiposCamposViewList.Contains("TiposCampos.Int"))
	    		{
	    			result.Add("TiposCamposView|Int");
	    			result.Add("TiposCamposView|TiposCampos.Int");
	    		}
	
	    		if (bmDisabledTiposCamposViewList.Contains("TiposCampos.Long"))
	    		{
	    			result.Add("TiposCamposView|Long");
	    			result.Add("TiposCamposView|TiposCampos.Long");
	    		}
	
	    		if (bmDisabledTiposCamposViewList.Contains("TiposCampos.Short"))
	    		{
	    			result.Add("TiposCamposView|Short");
	    			result.Add("TiposCamposView|TiposCampos.Short");
	    		}
	
	    		if (bmDisabledTiposCamposViewList.Contains("TiposCampos.String"))
	    		{
	    			result.Add("TiposCamposView|String");
	    			result.Add("TiposCamposView|TiposCampos.String");
	    		}
	
	    		if (bmDisabledTiposCamposViewList.Contains("TiposCampos.StringChar"))
	    		{
	    			result.Add("TiposCamposView|StringChar");
	    			result.Add("TiposCamposView|TiposCampos.StringChar");
	    		}
	
	    		if (bmDisabledTiposCamposViewList.Contains("TiposCampos.StringText"))
	    		{
	    			result.Add("TiposCamposView|StringText");
	    			result.Add("TiposCamposView|TiposCampos.StringText");
	    		}
	    	}
	    	//Add filtering disabled property for TiposCamposFilha
	    	string[] bmDisabledTiposCamposFilhaViewList = this.GetEDM().GetFilteringDisabledList("TiposCamposFilha");
	    	if (bmDisabledTiposCamposFilhaViewList.Length > 0)
	    	{
	
	    		if (bmDisabledTiposCamposFilhaViewList.Contains("TiposCamposFilha.Boolean"))
	    		{
	    			result.Add("TiposCamposFilhaView|Boolean");
	    			result.Add("TiposCamposFilhaView|TiposCamposFilha.Boolean");
	    		}
	
	    		if (bmDisabledTiposCamposFilhaViewList.Contains("TiposCamposFilha.Byte"))
	    		{
	    			result.Add("TiposCamposFilhaView|Byte");
	    			result.Add("TiposCamposFilhaView|TiposCamposFilha.Byte");
	    		}
	
	    		if (bmDisabledTiposCamposFilhaViewList.Contains("TiposCamposFilha.DateTime"))
	    		{
	    			result.Add("TiposCamposFilhaView|DateTime");
	    			result.Add("TiposCamposFilhaView|TiposCamposFilha.DateTime");
	    		}
	
	    		if (bmDisabledTiposCamposFilhaViewList.Contains("TiposCamposFilha.Decimal"))
	    		{
	    			result.Add("TiposCamposFilhaView|Decimal");
	    			result.Add("TiposCamposFilhaView|TiposCamposFilha.Decimal");
	    		}
	
	    		if (bmDisabledTiposCamposFilhaViewList.Contains("TiposCamposFilha.ID_TiposCamposFilha"))
	    		{
	    			result.Add("TiposCamposFilhaView|IDTiposCamposFilha");
	    			result.Add("TiposCamposFilhaView|TiposCamposFilha.ID_TiposCamposFilha");
	    		}
	
	    		if (bmDisabledTiposCamposFilhaViewList.Contains("TiposCamposFilha.Int"))
	    		{
	    			result.Add("TiposCamposFilhaView|Int");
	    			result.Add("TiposCamposFilhaView|TiposCamposFilha.Int");
	    		}
	
	    		if (bmDisabledTiposCamposFilhaViewList.Contains("TiposCamposFilha.Long"))
	    		{
	    			result.Add("TiposCamposFilhaView|Long");
	    			result.Add("TiposCamposFilhaView|TiposCamposFilha.Long");
	    		}
	
	    		if (bmDisabledTiposCamposFilhaViewList.Contains("TiposCamposFilha.Short"))
	    		{
	    			result.Add("TiposCamposFilhaView|Short");
	    			result.Add("TiposCamposFilhaView|TiposCamposFilha.Short");
	    		}
	
	    		if (bmDisabledTiposCamposFilhaViewList.Contains("TiposCamposFilha.String"))
	    		{
	    			result.Add("TiposCamposFilhaView|String");
	    			result.Add("TiposCamposFilhaView|TiposCamposFilha.String");
	    		}
	
	    		if (bmDisabledTiposCamposFilhaViewList.Contains("TiposCamposFilha.StringChar"))
	    		{
	    			result.Add("TiposCamposFilhaView|StringChar");
	    			result.Add("TiposCamposFilhaView|TiposCamposFilha.StringChar");
	    		}
	
	    		if (bmDisabledTiposCamposFilhaViewList.Contains("TiposCamposFilha.StringText"))
	    		{
	    			result.Add("TiposCamposFilhaView|StringText");
	    			result.Add("TiposCamposFilhaView|TiposCamposFilha.StringText");
	    		}
	    	}
	    	//Add filtering disabled property for PaiNotNull
	    	string[] bmDisabledPaiNotNullViewList = this.GetEDM().GetFilteringDisabledList("PaiNotNull");
	    	if (bmDisabledPaiNotNullViewList.Length > 0)
	    	{
	
	    		if (bmDisabledPaiNotNullViewList.Contains("PaiNotNull.Boolean"))
	    		{
	    			result.Add("PaiNotNullView|Boolean");
	    			result.Add("PaiNotNullView|PaiNotNull.Boolean");
	    		}
	
	    		if (bmDisabledPaiNotNullViewList.Contains("PaiNotNull.Byte"))
	    		{
	    			result.Add("PaiNotNullView|Byte");
	    			result.Add("PaiNotNullView|PaiNotNull.Byte");
	    		}
	
	    		if (bmDisabledPaiNotNullViewList.Contains("PaiNotNull.DateTime"))
	    		{
	    			result.Add("PaiNotNullView|DateTime");
	    			result.Add("PaiNotNullView|PaiNotNull.DateTime");
	    		}
	
	    		if (bmDisabledPaiNotNullViewList.Contains("PaiNotNull.Decimal"))
	    		{
	    			result.Add("PaiNotNullView|Decimal");
	    			result.Add("PaiNotNullView|PaiNotNull.Decimal");
	    		}
	
	    		if (bmDisabledPaiNotNullViewList.Contains("PaiNotNull.ID_PaiNotNull"))
	    		{
	    			result.Add("PaiNotNullView|IDPaiNotNull");
	    			result.Add("PaiNotNullView|PaiNotNull.ID_PaiNotNull");
	    		}
	
	    		if (bmDisabledPaiNotNullViewList.Contains("PaiNotNull.Int"))
	    		{
	    			result.Add("PaiNotNullView|Int");
	    			result.Add("PaiNotNullView|PaiNotNull.Int");
	    		}
	
	    		if (bmDisabledPaiNotNullViewList.Contains("PaiNotNull.Long"))
	    		{
	    			result.Add("PaiNotNullView|Long");
	    			result.Add("PaiNotNullView|PaiNotNull.Long");
	    		}
	
	    		if (bmDisabledPaiNotNullViewList.Contains("PaiNotNull.Short"))
	    		{
	    			result.Add("PaiNotNullView|Short");
	    			result.Add("PaiNotNullView|PaiNotNull.Short");
	    		}
	
	    		if (bmDisabledPaiNotNullViewList.Contains("PaiNotNull.String"))
	    		{
	    			result.Add("PaiNotNullView|String");
	    			result.Add("PaiNotNullView|PaiNotNull.String");
	    		}
	
	    		if (bmDisabledPaiNotNullViewList.Contains("PaiNotNull.StringChar"))
	    		{
	    			result.Add("PaiNotNullView|StringChar");
	    			result.Add("PaiNotNullView|PaiNotNull.StringChar");
	    		}
	
	    		if (bmDisabledPaiNotNullViewList.Contains("PaiNotNull.StringText"))
	    		{
	    			result.Add("PaiNotNullView|StringText");
	    			result.Add("PaiNotNullView|PaiNotNull.StringText");
	    		}
	    	}
	    	//Add filtering disabled property for FilhaNotNull
	    	string[] bmDisabledFilhaNotNullViewList = this.GetEDM().GetFilteringDisabledList("FilhaNotNull");
	    	if (bmDisabledFilhaNotNullViewList.Length > 0)
	    	{
	
	    		if (bmDisabledFilhaNotNullViewList.Contains("FilhaNotNull.Boolean"))
	    		{
	    			result.Add("FilhaNotNullView|Boolean");
	    			result.Add("FilhaNotNullView|FilhaNotNull.Boolean");
	    		}
	
	    		if (bmDisabledFilhaNotNullViewList.Contains("FilhaNotNull.Byte"))
	    		{
	    			result.Add("FilhaNotNullView|Byte");
	    			result.Add("FilhaNotNullView|FilhaNotNull.Byte");
	    		}
	
	    		if (bmDisabledFilhaNotNullViewList.Contains("FilhaNotNull.DateTime"))
	    		{
	    			result.Add("FilhaNotNullView|DateTime");
	    			result.Add("FilhaNotNullView|FilhaNotNull.DateTime");
	    		}
	
	    		if (bmDisabledFilhaNotNullViewList.Contains("FilhaNotNull.Decimal"))
	    		{
	    			result.Add("FilhaNotNullView|Decimal");
	    			result.Add("FilhaNotNullView|FilhaNotNull.Decimal");
	    		}
	
	    		if (bmDisabledFilhaNotNullViewList.Contains("FilhaNotNull.ID_FilhaNotNull"))
	    		{
	    			result.Add("FilhaNotNullView|IDFilhaNotNull");
	    			result.Add("FilhaNotNullView|FilhaNotNull.ID_FilhaNotNull");
	    		}
	
	    		if (bmDisabledFilhaNotNullViewList.Contains("FilhaNotNull.Int"))
	    		{
	    			result.Add("FilhaNotNullView|Int");
	    			result.Add("FilhaNotNullView|FilhaNotNull.Int");
	    		}
	
	    		if (bmDisabledFilhaNotNullViewList.Contains("FilhaNotNull.Long"))
	    		{
	    			result.Add("FilhaNotNullView|Long");
	    			result.Add("FilhaNotNullView|FilhaNotNull.Long");
	    		}
	
	    		if (bmDisabledFilhaNotNullViewList.Contains("FilhaNotNull.Short"))
	    		{
	    			result.Add("FilhaNotNullView|Short");
	    			result.Add("FilhaNotNullView|FilhaNotNull.Short");
	    		}
	
	    		if (bmDisabledFilhaNotNullViewList.Contains("FilhaNotNull.String"))
	    		{
	    			result.Add("FilhaNotNullView|String");
	    			result.Add("FilhaNotNullView|FilhaNotNull.String");
	    		}
	
	    		if (bmDisabledFilhaNotNullViewList.Contains("FilhaNotNull.StringChar"))
	    		{
	    			result.Add("FilhaNotNullView|StringChar");
	    			result.Add("FilhaNotNullView|FilhaNotNull.StringChar");
	    		}
	
	    		if (bmDisabledFilhaNotNullViewList.Contains("FilhaNotNull.StringText"))
	    		{
	    			result.Add("FilhaNotNullView|StringText");
	    			result.Add("FilhaNotNullView|FilhaNotNull.StringText");
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
	    //Get TiposCamposView By EntitySearchId.
	    public IQueryable<TiposCamposView> GetTiposCamposViewByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTiposCamposViewByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get TiposCamposFilhaView By EntitySearchId.
	    public IQueryable<TiposCamposFilhaView> GetTiposCamposFilhaViewByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTiposCamposFilhaViewByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get TiposCamposView By EntitySearchId.
	    public IQueryable<TiposCamposView> GetTiposCamposViewByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTiposCamposViewByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get TiposCamposFilhaView By EntitySearchId.
	    public IQueryable<TiposCamposFilhaView> GetTiposCamposFilhaViewByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTiposCamposFilhaViewByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get PaiNotNullView By EntitySearchId.
	    public IQueryable<PaiNotNullView> GetPaiNotNullViewByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetPaiNotNullViewByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get FilhaNotNullView By EntitySearchId.
	    public IQueryable<FilhaNotNullView> GetFilhaNotNullViewByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetFilhaNotNullViewByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get PaiNotNullView By EntitySearchId.
	    public IQueryable<PaiNotNullView> GetPaiNotNullViewByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetPaiNotNullViewByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get FilhaNotNullView By EntitySearchId.
	    public IQueryable<FilhaNotNullView> GetFilhaNotNullViewByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetFilhaNotNullViewByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get TiposCamposView By Example.
	    [Ignore]
	    public IQueryable<TiposCamposView> GetTiposCamposViewByExample(TiposCamposView entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTiposCamposViewByEntitySearch(queryAnalysis);
	    }
			
	    //Get TiposCamposFilhaView By Example.
	    [Ignore]
	    public IQueryable<TiposCamposFilhaView> GetTiposCamposFilhaViewByExample(TiposCamposFilhaView entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTiposCamposFilhaViewByEntitySearch(queryAnalysis);
	    }
			
	    //Get TiposCamposView By Example.
	    [Ignore]
	    public IQueryable<TiposCamposView> GetTiposCamposViewByExampleNoAssociations(TiposCamposView entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTiposCamposViewByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TiposCamposFilhaView By Example.
	    [Ignore]
	    public IQueryable<TiposCamposFilhaView> GetTiposCamposFilhaViewByExampleNoAssociations(TiposCamposFilhaView entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTiposCamposFilhaViewByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get PaiNotNullView By Example.
	    [Ignore]
	    public IQueryable<PaiNotNullView> GetPaiNotNullViewByExample(PaiNotNullView entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetPaiNotNullViewByEntitySearch(queryAnalysis);
	    }
			
	    //Get FilhaNotNullView By Example.
	    [Ignore]
	    public IQueryable<FilhaNotNullView> GetFilhaNotNullViewByExample(FilhaNotNullView entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetFilhaNotNullViewByEntitySearch(queryAnalysis);
	    }
			
	    //Get PaiNotNullView By Example.
	    [Ignore]
	    public IQueryable<PaiNotNullView> GetPaiNotNullViewByExampleNoAssociations(PaiNotNullView entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetPaiNotNullViewByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get FilhaNotNullView By Example.
	    [Ignore]
	    public IQueryable<FilhaNotNullView> GetFilhaNotNullViewByExampleNoAssociations(FilhaNotNullView entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetFilhaNotNullViewByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public TiposCamposView GetTiposCamposViewByKey(Int32 iDTiposCampos)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TiposCamposView");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IDTiposCampos"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, iDTiposCampos));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTiposCamposViewByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TiposCamposFilhaView GetTiposCamposFilhaViewByKey(Int32 iDTiposCamposFilha)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TiposCamposFilhaView");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IDTiposCamposFilha"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, iDTiposCamposFilha));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTiposCamposFilhaViewByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public PaiNotNullView GetPaiNotNullViewByKey(Int32 iDPaiNotNull)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("PaiNotNullView");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IDPaiNotNull"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, iDPaiNotNull));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetPaiNotNullViewByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public FilhaNotNullView GetFilhaNotNullViewByKey(Int32 iDFilhaNotNull)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("FilhaNotNullView");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IDFilhaNotNull"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, iDFilhaNotNull));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetFilhaNotNullViewByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    [TiposCamposViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TiposCamposViewByEntitySearch.
	    public IQueryable<TiposCamposView> GetTiposCamposViewByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTiposCamposViewByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new TiposCamposViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TiposCamposView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TiposCamposView> result = 
	            (from entity0 in this.DbContext.TiposCampos.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TiposCamposView()		
	            {
	            
                Boolean = entity0.Boolean
                , Byte = entity0.Byte
                , DateTime = entity0.DateTime
                , Decimal = entity0.Decimal
                , IDTiposCampos = entity0.ID_TiposCampos
                , Int = entity0.Int
                , Long = entity0.Long
                , Short = entity0.Short
                , String = entity0.String
                , StringChar = entity0.StringChar
                , StringText = entity0.StringText
			
                ,TiposCamposFilhaViewList = 
	                        (from entity1 in entity0.TiposCamposFilha_LISTA
                                  let entity1Al1 = entity1.TiposCampos
	                        
	                        	
	                        select new TiposCamposFilhaView()
	                        {
	                        
                                Boolean = entity1.Boolean
                                , Byte = entity1.Byte
                                , DateTime = entity1.DateTime
                                , Decimal = entity1.Decimal
                                , IDTiposCampos = entity1Al1.ID_TiposCampos
                                , IDTiposCamposFilha = entity1.ID_TiposCamposFilha
                                , Int = entity1.Int
                                , Long = entity1.Long
                                , Short = entity1.Short
                                , String = entity1.String
                                , StringName = ((entity1.String) == "01" ? "String 01" : ((entity1.String) == "01A" ? "String 01A" : ((entity1.String) == "02" ? "String 02" : ((entity1.String) == "A" ? "String A" : ((entity1.String) == "ststdd" ? "NewString" : ((entity1.String) == "sttst" ? "String Teste" : ((entity1.String) == "ValString" ? "ValString" : "")))))))
                                , StringChar = entity1.StringChar
                                , StringText = entity1.StringText
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TiposCamposFilhaViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TiposCamposFilhaViewByEntitySearch.
	    public IQueryable<TiposCamposFilhaView> GetTiposCamposFilhaViewByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTiposCamposFilhaViewByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new TiposCamposFilhaViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TiposCamposFilhaView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TiposCamposFilhaView> result = 
	            (from entity0 in this.DbContext.TiposCamposFilha.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TiposCampos
	            
	            	
	            select new TiposCamposFilhaView()		
	            {
	            
                Boolean = entity0.Boolean
                , Byte = entity0.Byte
                , DateTime = entity0.DateTime
                , Decimal = entity0.Decimal
                , IDTiposCampos = entity0Al1.ID_TiposCampos
                , IDTiposCamposFilha = entity0.ID_TiposCamposFilha
                , Int = entity0.Int
                , Long = entity0.Long
                , Short = entity0.Short
                , String = entity0.String
                , StringName = ((entity0.String) == "01" ? "String 01" : ((entity0.String) == "01A" ? "String 01A" : ((entity0.String) == "02" ? "String 02" : ((entity0.String) == "A" ? "String A" : ((entity0.String) == "ststdd" ? "NewString" : ((entity0.String) == "sttst" ? "String Teste" : ((entity0.String) == "ValString" ? "ValString" : "")))))))
                , StringChar = entity0.StringChar
                , StringText = entity0.StringText
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TiposCamposViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TiposCamposViewByEntitySearchNoAssociations.
	    public IQueryable<TiposCamposView> GetTiposCamposViewByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTiposCamposViewByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new TiposCamposViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TiposCamposView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TiposCamposView> result = 
	            (from entity0 in this.DbContext.TiposCampos.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TiposCamposView()		
	            {
	            
                Boolean = entity0.Boolean
                , Byte = entity0.Byte
                , DateTime = entity0.DateTime
                , Decimal = entity0.Decimal
                , IDTiposCampos = entity0.ID_TiposCampos
                , Int = entity0.Int
                , Long = entity0.Long
                , Short = entity0.Short
                , String = entity0.String
                , StringChar = entity0.StringChar
                , StringText = entity0.StringText
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TiposCamposFilhaViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TiposCamposFilhaViewByEntitySearchNoAssociations.
	    public IQueryable<TiposCamposFilhaView> GetTiposCamposFilhaViewByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTiposCamposFilhaViewByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new TiposCamposFilhaViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TiposCamposFilhaView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TiposCamposFilhaView> result = 
	            (from entity0 in this.DbContext.TiposCamposFilha.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TiposCampos
	            
	            	
	            select new TiposCamposFilhaView()		
	            {
	            
                Boolean = entity0.Boolean
                , Byte = entity0.Byte
                , DateTime = entity0.DateTime
                , Decimal = entity0.Decimal
                , IDTiposCampos = entity0Al1.ID_TiposCampos
                , IDTiposCamposFilha = entity0.ID_TiposCamposFilha
                , Int = entity0.Int
                , Long = entity0.Long
                , Short = entity0.Short
                , String = entity0.String
                , StringName = ((entity0.String) == "01" ? "String 01" : ((entity0.String) == "01A" ? "String 01A" : ((entity0.String) == "02" ? "String 02" : ((entity0.String) == "A" ? "String A" : ((entity0.String) == "ststdd" ? "NewString" : ((entity0.String) == "sttst" ? "String Teste" : ((entity0.String) == "ValString" ? "ValString" : "")))))))
                , StringChar = entity0.StringChar
                , StringText = entity0.StringText
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TiposCamposFilhaViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TiposCamposFilhaViewParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TiposCamposFilhaViewParentComposition> GetTiposCamposFilhaViewParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTiposCamposFilhaViewParentCompositionByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new TiposCamposFilhaViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TiposCamposFilhaViewParentComposition));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TiposCamposFilhaViewParentComposition> result = 
	            (from entity0 in this.DbContext.TiposCamposFilha.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TiposCampos
	            
	            	
	            select new TiposCamposFilhaViewParentComposition()		
	            {
	            
                Boolean = entity0.Boolean
                , Byte = entity0.Byte
                , DateTime = entity0.DateTime
                , Decimal = entity0.Decimal
                , IDTiposCampos = entity0Al1.ID_TiposCampos
                , IDTiposCamposFilha = entity0.ID_TiposCamposFilha
                , Int = entity0.Int
                , Long = entity0.Long
                , Short = entity0.Short
                , String = entity0.String
                , StringName = ((entity0.String) == "01" ? "String 01" : ((entity0.String) == "01A" ? "String 01A" : ((entity0.String) == "02" ? "String 02" : ((entity0.String) == "A" ? "String A" : ((entity0.String) == "ststdd" ? "NewString" : ((entity0.String) == "sttst" ? "String Teste" : ((entity0.String) == "ValString" ? "ValString" : "")))))))
                , StringChar = entity0.StringChar
                , StringText = entity0.StringText
                //TiposCamposView Properties.
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [PaiNotNullViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PaiNotNullViewByEntitySearch.
	    public IQueryable<PaiNotNullView> GetPaiNotNullViewByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPaiNotNullViewByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new PaiNotNullViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(PaiNotNullView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<PaiNotNullView> result = 
	            (from entity0 in this.DbContext.PaiNotNull.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new PaiNotNullView()		
	            {
	            
                Boolean = entity0.Boolean
                , Byte = entity0.Byte
                , DateTime = entity0.DateTime
                , Decimal = entity0.Decimal
                , IDPaiNotNull = entity0.ID_PaiNotNull
                , Int = entity0.Int
                , Long = entity0.Long
                , Short = entity0.Short
                , String = entity0.String
                , StringChar = entity0.StringChar
                , StringText = entity0.StringText
			
                ,FilhaNotNullViewList = 
	                        (from entity1 in entity0.FilhaNotNull_LISTA
                                  let entity1Al1 = entity1.PaiNotNull
	                        
	                        	
	                        select new FilhaNotNullView()
	                        {
	                        
                                Boolean = entity1.Boolean
                                , Byte = entity1.Byte
                                , DateTime = entity1.DateTime
                                , Decimal = entity1.Decimal
                                , IDFilhaNotNull = entity1.ID_FilhaNotNull
                                , IDPaiNotNull = entity1Al1.ID_PaiNotNull
                                , Int = entity1.Int
                                , Long = entity1.Long
                                , Short = entity1.Short
                                , String = entity1.String
                                , StringChar = entity1.StringChar
                                , StringText = entity1.StringText
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [FilhaNotNullViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get FilhaNotNullViewByEntitySearch.
	    public IQueryable<FilhaNotNullView> GetFilhaNotNullViewByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetFilhaNotNullViewByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new FilhaNotNullViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(FilhaNotNullView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<FilhaNotNullView> result = 
	            (from entity0 in this.DbContext.FilhaNotNull.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.PaiNotNull
	            
	            	
	            select new FilhaNotNullView()		
	            {
	            
                Boolean = entity0.Boolean
                , Byte = entity0.Byte
                , DateTime = entity0.DateTime
                , Decimal = entity0.Decimal
                , IDFilhaNotNull = entity0.ID_FilhaNotNull
                , IDPaiNotNull = entity0Al1.ID_PaiNotNull
                , Int = entity0.Int
                , Long = entity0.Long
                , Short = entity0.Short
                , String = entity0.String
                , StringChar = entity0.StringChar
                , StringText = entity0.StringText
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [PaiNotNullViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PaiNotNullViewByEntitySearchNoAssociations.
	    public IQueryable<PaiNotNullView> GetPaiNotNullViewByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPaiNotNullViewByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new PaiNotNullViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(PaiNotNullView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<PaiNotNullView> result = 
	            (from entity0 in this.DbContext.PaiNotNull.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new PaiNotNullView()		
	            {
	            
                Boolean = entity0.Boolean
                , Byte = entity0.Byte
                , DateTime = entity0.DateTime
                , Decimal = entity0.Decimal
                , IDPaiNotNull = entity0.ID_PaiNotNull
                , Int = entity0.Int
                , Long = entity0.Long
                , Short = entity0.Short
                , String = entity0.String
                , StringChar = entity0.StringChar
                , StringText = entity0.StringText
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [FilhaNotNullViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get FilhaNotNullViewByEntitySearchNoAssociations.
	    public IQueryable<FilhaNotNullView> GetFilhaNotNullViewByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetFilhaNotNullViewByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new FilhaNotNullViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(FilhaNotNullView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<FilhaNotNullView> result = 
	            (from entity0 in this.DbContext.FilhaNotNull.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.PaiNotNull
	            
	            	
	            select new FilhaNotNullView()		
	            {
	            
                Boolean = entity0.Boolean
                , Byte = entity0.Byte
                , DateTime = entity0.DateTime
                , Decimal = entity0.Decimal
                , IDFilhaNotNull = entity0.ID_FilhaNotNull
                , IDPaiNotNull = entity0Al1.ID_PaiNotNull
                , Int = entity0.Int
                , Long = entity0.Long
                , Short = entity0.Short
                , String = entity0.String
                , StringChar = entity0.StringChar
                , StringText = entity0.StringText
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [FilhaNotNullViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get FilhaNotNullViewParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<FilhaNotNullViewParentComposition> GetFilhaNotNullViewParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetFilhaNotNullViewParentCompositionByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new FilhaNotNullViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(FilhaNotNullViewParentComposition));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<FilhaNotNullViewParentComposition> result = 
	            (from entity0 in this.DbContext.FilhaNotNull.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.PaiNotNull
	            
	            	
	            select new FilhaNotNullViewParentComposition()		
	            {
	            
                Boolean = entity0.Boolean
                , Byte = entity0.Byte
                , DateTime = entity0.DateTime
                , Decimal = entity0.Decimal
                , IDFilhaNotNull = entity0.ID_FilhaNotNull
                , IDPaiNotNull = entity0Al1.ID_PaiNotNull
                , Int = entity0.Int
                , Long = entity0.Long
                , Short = entity0.Short
                , String = entity0.String
                , StringChar = entity0.StringChar
                , StringText = entity0.StringText
                //PaiNotNullView Properties.
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    [TiposCamposViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedTiposCamposView.
	    public IQueryable<TiposCamposView> GetPagedTiposCamposView(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedTiposCamposView")))
 	        {
 	             AuthorizationResult authorizationResult = (new TiposCamposViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TiposCamposView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TiposCamposView> result = 
	            (from entity0 in this.DbContext.TiposCampos.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_TiposCampos ascending
	            
	            	
	            select new TiposCamposView()		
	            {
	            
                Boolean = entity0.Boolean
                , Byte = entity0.Byte
                , DateTime = entity0.DateTime
                , Decimal = entity0.Decimal
                , IDTiposCampos = entity0.ID_TiposCampos
                , Int = entity0.Int
                , Long = entity0.Long
                , Short = entity0.Short
                , String = entity0.String
                , StringChar = entity0.StringChar
                , StringText = entity0.StringText
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    [TiposCamposFilhaViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedTiposCamposFilhaView.
	    public IQueryable<TiposCamposFilhaView> GetPagedTiposCamposFilhaView(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedTiposCamposFilhaView")))
 	        {
 	             AuthorizationResult authorizationResult = (new TiposCamposFilhaViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TiposCamposFilhaView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TiposCamposFilhaView> result = 
	            (from entity0 in this.DbContext.TiposCamposFilha.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TiposCampos
                orderby entity0.ID_TiposCamposFilha ascending
	            
	            	
	            select new TiposCamposFilhaView()		
	            {
	            
                Boolean = entity0.Boolean
                , Byte = entity0.Byte
                , DateTime = entity0.DateTime
                , Decimal = entity0.Decimal
                , IDTiposCampos = entity0Al1.ID_TiposCampos
                , IDTiposCamposFilha = entity0.ID_TiposCamposFilha
                , Int = entity0.Int
                , Long = entity0.Long
                , Short = entity0.Short
                , String = entity0.String
                , StringName = ((entity0.String) == "01" ? "String 01" : ((entity0.String) == "01A" ? "String 01A" : ((entity0.String) == "02" ? "String 02" : ((entity0.String) == "A" ? "String A" : ((entity0.String) == "ststdd" ? "NewString" : ((entity0.String) == "sttst" ? "String Teste" : ((entity0.String) == "ValString" ? "ValString" : "")))))))
                , StringChar = entity0.StringChar
                , StringText = entity0.StringText
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTiposCamposViewCounting(string serializedEntitySearch)
	    {	
		 	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TiposCamposView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TiposCampos.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTiposCamposFilhaViewCounting(string serializedEntitySearch)
	    {	
		 	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TiposCamposFilhaView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TiposCamposFilha.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TiposCampos
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    [PaiNotNullViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedPaiNotNullView.
	    public IQueryable<PaiNotNullView> GetPagedPaiNotNullView(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedPaiNotNullView")))
 	        {
 	             AuthorizationResult authorizationResult = (new PaiNotNullViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(PaiNotNullView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<PaiNotNullView> result = 
	            (from entity0 in this.DbContext.PaiNotNull.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_PaiNotNull ascending
	            
	            	
	            select new PaiNotNullView()		
	            {
	            
                Boolean = entity0.Boolean
                , Byte = entity0.Byte
                , DateTime = entity0.DateTime
                , Decimal = entity0.Decimal
                , IDPaiNotNull = entity0.ID_PaiNotNull
                , Int = entity0.Int
                , Long = entity0.Long
                , Short = entity0.Short
                , String = entity0.String
                , StringChar = entity0.StringChar
                , StringText = entity0.StringText
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    [FilhaNotNullViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedFilhaNotNullView.
	    public IQueryable<FilhaNotNullView> GetPagedFilhaNotNullView(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedFilhaNotNullView")))
 	        {
 	             AuthorizationResult authorizationResult = (new FilhaNotNullViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(FilhaNotNullView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<FilhaNotNullView> result = 
	            (from entity0 in this.DbContext.FilhaNotNull.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.PaiNotNull
                orderby entity0.ID_FilhaNotNull ascending
	            
	            	
	            select new FilhaNotNullView()		
	            {
	            
                Boolean = entity0.Boolean
                , Byte = entity0.Byte
                , DateTime = entity0.DateTime
                , Decimal = entity0.Decimal
                , IDFilhaNotNull = entity0.ID_FilhaNotNull
                , IDPaiNotNull = entity0Al1.ID_PaiNotNull
                , Int = entity0.Int
                , Long = entity0.Long
                , Short = entity0.Short
                , String = entity0.String
                , StringChar = entity0.StringChar
                , StringText = entity0.StringText
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetPaiNotNullViewCounting(string serializedEntitySearch)
	    {	
		 	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(PaiNotNullView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.PaiNotNull.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetFilhaNotNullViewCounting(string serializedEntitySearch)
	    {	
		 	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(FilhaNotNullView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.FilhaNotNull.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.PaiNotNull
	            
	            select 1
	            ).Count();	
		
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    [TiposCamposViewUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update TiposCamposView.
	    public void UpdateTiposCamposView(TiposCamposView entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateTiposCamposView")))
 	        {
 	             AuthorizationResult authorizationResult = (new TiposCamposViewUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    [TiposCamposViewInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert TiposCamposView.
	    public void InsertTiposCamposView(TiposCamposView entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertTiposCamposView")))
 	        {
 	             AuthorizationResult authorizationResult = (new TiposCamposViewInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    [TiposCamposViewDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete TiposCamposView.
	    public void DeleteTiposCamposView(TiposCamposView entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteTiposCamposView")))
 	        {
 	             AuthorizationResult authorizationResult = (new TiposCamposViewDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    [TiposCamposFilhaViewUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update TiposCamposFilhaView.
	    public void UpdateTiposCamposFilhaView(TiposCamposFilhaView entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateTiposCamposFilhaView")))
 	        {
 	             AuthorizationResult authorizationResult = (new TiposCamposFilhaViewUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.TiposCamposView.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TiposCamposView) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TiposCamposView); 	
	            

	
	        }
	
	    }

	    [TiposCamposFilhaViewInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert TiposCamposFilhaView.
	    public void InsertTiposCamposFilhaView(TiposCamposFilhaView entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertTiposCamposFilhaView")))
 	        {
 	             AuthorizationResult authorizationResult = (new TiposCamposFilhaViewInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.TiposCamposView.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TiposCamposView) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TiposCamposView);
	            

	
	        }
	
	    }

	    [TiposCamposFilhaViewDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete TiposCamposFilhaView.
	    public void DeleteTiposCamposFilhaView(TiposCamposFilhaView entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteTiposCamposFilhaView")))
 	        {
 	             AuthorizationResult authorizationResult = (new TiposCamposFilhaViewDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.TiposCamposView.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TiposCamposView) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TiposCamposView);
	            

	
	        }

	
	    }
		
			
	    [PaiNotNullViewUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update PaiNotNullView.
	    public void UpdatePaiNotNullView(PaiNotNullView entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdatePaiNotNullView")))
 	        {
 	             AuthorizationResult authorizationResult = (new PaiNotNullViewUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    [PaiNotNullViewInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert PaiNotNullView.
	    public void InsertPaiNotNullView(PaiNotNullView entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertPaiNotNullView")))
 	        {
 	             AuthorizationResult authorizationResult = (new PaiNotNullViewInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    [PaiNotNullViewDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete PaiNotNullView.
	    public void DeletePaiNotNullView(PaiNotNullView entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeletePaiNotNullView")))
 	        {
 	             AuthorizationResult authorizationResult = (new PaiNotNullViewDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    [FilhaNotNullViewUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update FilhaNotNullView.
	    public void UpdateFilhaNotNullView(FilhaNotNullView entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateFilhaNotNullView")))
 	        {
 	             AuthorizationResult authorizationResult = (new FilhaNotNullViewUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.PaiNotNullView.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.PaiNotNullView) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.PaiNotNullView); 	
	            

	
	        }
	
	    }

	    [FilhaNotNullViewInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert FilhaNotNullView.
	    public void InsertFilhaNotNullView(FilhaNotNullView entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertFilhaNotNullView")))
 	        {
 	             AuthorizationResult authorizationResult = (new FilhaNotNullViewInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.PaiNotNullView.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.PaiNotNullView) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.PaiNotNullView);
	            

	
	        }
	
	    }

	    [FilhaNotNullViewDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete FilhaNotNullView.
	    public void DeleteFilhaNotNullView(FilhaNotNullView entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteFilhaNotNullView")))
 	        {
 	             AuthorizationResult authorizationResult = (new FilhaNotNullViewDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.PaiNotNullView.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.PaiNotNullView) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.PaiNotNullView);
	            

	
	        }

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}