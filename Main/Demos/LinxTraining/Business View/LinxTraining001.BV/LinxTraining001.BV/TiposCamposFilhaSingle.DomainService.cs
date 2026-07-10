					
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

namespace LinxTraining001.BV.TiposCamposFilhaSingle
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TiposCamposFilha.ID_TiposCamposFilha", IsUpdatable=false, EdmName="LinxTraining002.BM.ModeloVendaCliente")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TiposCamposFilhaView];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IDTiposCamposFilha];ReadOnly[false];Entities[TiposCamposFilha:IDTiposCamposFilha|TiposCampos:IDTiposCampos];SubQueryInfo[];EdmEntityName[TiposCamposFilha];EntityRelations[TiposCampos(TiposCampos)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TiposCamposFilhaView")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "LinxTraining001.BV.TiposCamposFilhaSingle.TiposCamposFilhaView")]
	public partial class TiposCamposFilhaView : Linx.Data.Entity
	{

	

	    public TiposCamposFilhaView() : this(true) { }

	    public TiposCamposFilhaView(bool setDefaults) 
	    {

	        if (setDefaults)
	        {
	        	        Guid = Guid.NewGuid();
	        }	

	    }

			
	

	
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
	    //Extensibility Partial Method Definitions For Guid
	    partial void OnGuidChanging(System.Guid value);
	    partial void OnGuidChanged();

	    private System.Guid _Guid;

	    [DataMember(IsRequired = true, Name = "Guid", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Guid", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[Guid.NewGuid()];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TiposCamposFilha.Guid];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TiposCamposFilha.Guid")]
	    public System.Guid Guid
	    {
	    	    get
	    	    {
	    	          return _Guid;
	    	    }
	    	    set
	    	    {
	    	          if (this._Guid != value)
	    	          {
	    	              this.ValidateProperty("Guid", value);
	    	              this.OnGuidChanging(value);
	    	              this.RaiseDataMemberChanging("Guid");
	    	              this._Guid = value;
	    	              this.RaiseDataMemberChanged("Guid");
	    	              this.OnGuidChanged();
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
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTiposCampos];LookUpTitle[Seleção de (ID TiposCampos)];LookUpQuery[executeLookUpTiposCampos];LookUpFinalize[finalizeLookUpTiposCampos];LookUpDisplayColumns[{\"IDTiposCampos\" : \"ID TiposCampos\"}];LookUpColumns[{\"IDTiposCampos\" : true}];FilterDataKey[TiposCamposFilha.TiposCampos.ID_TiposCampos];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IDTiposCampos#true##12:0##ID TiposCampos#0#true##::LookUpTiposCampos##false#false#TiposCampos#TiposCampos#LinxTraining001.BV.TiposCamposFilhaSingle#IQueryable###true#false", EdmKey="TiposCamposFilha.TiposCampos.ID_TiposCampos")]
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
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[TstDomainString];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TiposCamposFilha.String];IsMeasure[false]")]
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
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TiposCamposFilha.Guid", Source = "Guid", Target = "Guid", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TiposCamposFilha", RelationPropertyName = "TiposCamposFilha" });
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
	//////////////////////// DomainService Class V1 ///////////////////////
	///////////////////////////////////////////////////////////////////////
	[EnableClientAccess()]	
	[DomainIdentifier("ProcessorOverviewTiposCamposFilhaSingleDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class TiposCamposFilhaSingleDomainService : DomainService, IDataServiceContext 
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

		
	    public TiposCamposFilhaSingleDomainService() : this("", null, null){ }
	    public TiposCamposFilhaSingleDomainService(string connectionString) : this(connectionString, null, null) { }
	    public TiposCamposFilhaSingleDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public TiposCamposFilhaSingleDomainService(LinxTraining002.BM.ModeloVendaCliente dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public TiposCamposFilhaSingleDomainService(string connectionString, LinxTraining002.BM.ModeloVendaCliente dataContext, Dictionary<string, string> headers) : base() 
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
	    //Get All LookUpTiposCampos.
	    public IQueryable<LookUpTiposCampos> GetAllLookUpTiposCampos()
	    {
	        return this.GetLookUpTiposCampos(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTiposCampos By EntitySearch.
	    public IQueryable<LookUpTiposCampos> GetLookUpTiposCamposByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTiposCampos(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTiposCampos.
	    public IQueryable<LookUpTiposCampos> GetLookUpTiposCampos(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TiposCampos" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTiposCampos";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTiposCampos));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTiposCampos> query =  
	
	            (from entity in this.DbContext.TiposCampos.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpTiposCampos()		
	            {
	            
                IDTiposCampos = entity.ID_TiposCampos
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
	
		

	        if (entityName.InList("LinxTraining001.BV.TiposCamposFilhaSingle.TiposCamposFilhaView"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TiposCamposFilhaView",
	        			NameSpace = "LinxTraining001.BV.TiposCamposFilhaSingle",
	        			ParentClassName = null,	
	        			DisplayName = "TiposCamposFilhaView",
	        			ClearMethodName = "ClearTiposCamposFilhaView",
	        			QueryMethodName  = "GetPagedTiposCamposFilhaView",	
	        			CountingMethodName  = "GetTiposCamposFilhaView" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("LinxTraining001.BV.TiposCamposFilhaSingle.TiposCamposFilhaView"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("LinxTraining001.BV.TiposCamposFilhaSingle.TiposCamposFilhaView"), forceAll: forceAll)
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


             return new string[] { "LinxTraining001_tiposCamposFilhaSingleService", Linx.Tools.AssemblyHelper.ReadResourceContent("LinxTraining001.BV.ClientResources.tiposCamposFilhaSingleService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	

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
	    //Clear TiposCamposFilhaView.
	    public IEnumerable<TiposCamposFilhaView> ClearTiposCamposFilhaView()
	    {
	        List<TiposCamposFilhaView> result = new List<TiposCamposFilhaView>();
	        result.Add(new TiposCamposFilhaView(false));	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
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
                , Guid = entity0.Guid
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
                , Guid = entity0.Guid
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
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
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
	
	    		if (bmDisabledTiposCamposFilhaViewList.Contains("TiposCamposFilha.Guid"))
	    		{
	    			result.Add("TiposCamposFilhaView|Guid");
	    			result.Add("TiposCamposFilhaView|TiposCamposFilha.Guid");
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
	    //Get TiposCamposFilhaView By EntitySearchId.
	    public IQueryable<TiposCamposFilhaView> GetTiposCamposFilhaViewByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTiposCamposFilhaViewByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get TiposCamposFilhaView By EntitySearchId.
	    public IQueryable<TiposCamposFilhaView> GetTiposCamposFilhaViewByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTiposCamposFilhaViewByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get TiposCamposFilhaView By Example.
	    [Ignore]
	    public IQueryable<TiposCamposFilhaView> GetTiposCamposFilhaViewByExample(TiposCamposFilhaView entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTiposCamposFilhaViewByEntitySearch(queryAnalysis);
	    }
			
	    //Get TiposCamposFilhaView By Example.
	    [Ignore]
	    public IQueryable<TiposCamposFilhaView> GetTiposCamposFilhaViewByExampleNoAssociations(TiposCamposFilhaView entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTiposCamposFilhaViewByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



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

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
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
                , Guid = entity0.Guid
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
                , Guid = entity0.Guid
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
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
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
                , Guid = entity0.Guid
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
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
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

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
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

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
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

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}