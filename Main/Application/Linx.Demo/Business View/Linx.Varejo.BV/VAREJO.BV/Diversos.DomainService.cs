					
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
using Linx.Demo.BM;

namespace VAREJO.BV.Diversos
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="VENDA.ID_VENDA", IsUpdatable=false, EdmName="Linx.Demo.BM.DCLinxDemoBM")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[Venda,Venda.VendaItem];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdVenda];ReadOnly[false];Entities[VENDA:IdVenda|CLIENTE:IdCliente|LOJA:IdLoja];SubQueryInfo[];EdmEntityName[VENDA];EntityRelations[CLIENTE(CLIENTE)#ESTADO(ESTADO)#PAIS(PAIS)#LOJA(LOJA)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "Venda")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "VAREJO.BV.Diversos.Venda")]
	public partial class Venda : Linx.Data.Entity
	{

	
		
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.VendaItemList != null && this.VendaItemList.Count() > 0)
	      {
	         foreach (var entity in this.VendaItemList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.VendaItemList != null)
	      {
	         foreach (var detail in this.VendaItemList)
	         {
	            detail.ResetDetails();
	         }
	         this.VendaItemList = null;
	      }
	    }

	    public virtual void ResetChangeState()
	    {
	      this.ChangeState = "N";
	      if (this.VendaItemList != null)
	      {
	         foreach (var detail in this.VendaItemList.ToArray())
	         {
	            detail.ResetChangeState();
	         }
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(DiversosDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("VendaItem"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("VendaItem");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdVenda"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdVenda));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load VendaItem and all sub-details
	         if (this.VendaItemList == null || this.VendaItemList.Count() == 0)
	         {
	             if (take > 0)
	                 this.VendaItemList = context.GetPagedVendaItem(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.VendaItemList = (from r in context.GetVendaItemByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _VendaItemElements = changeSet.ChangeSetEntries.Where(e => e.Entity is VendaItem && ((VendaItem)e.Entity).Venda == null && e.Associations == null && e.OriginalAssociations == null && ((VendaItem)e.Entity).IdVenda == this.IdVenda).ToList();
 	      if (_VendaItemElements.Count > 0 && this.VendaItemList.Count() == 0)
 	      {
 	          this.VendaItemList = _VendaItemElements.Select(e => (VendaItem)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _VendaItemElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((VendaItem)detail.Entity).Venda = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("Venda", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("VendaItemList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For BigIntVenda
	    partial void OnBigIntVendaChanging(System.Nullable<System.Int64> value);
	    partial void OnBigIntVendaChanged();

	    private System.Nullable<System.Int64> _BigIntVenda;

	    [DataMember(Name = "BigIntVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Big Int Venda", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.BIG_INT_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.BIG_INT_VENDA")]
	    public System.Nullable<System.Int64> BigIntVenda
	    {
	    	    get
	    	    {
	    	          return _BigIntVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._BigIntVenda != value)
	    	          {
	    	              this.ValidateProperty("BigIntVenda", value);
	    	              this.OnBigIntVendaChanging(value);
	    	              this.RaiseDataMemberChanging("BigIntVenda");
	    	              this._BigIntVenda = value;
	    	              this.RaiseDataMemberChanged("BigIntVenda");
	    	              this.OnBigIntVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For BitVenda
	    partial void OnBitVendaChanging(System.Nullable<System.Boolean> value);
	    partial void OnBitVendaChanged();

	    private System.Nullable<System.Boolean> _BitVenda;

	    [DataMember(Name = "BitVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bit Venda", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.BIT_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.BIT_VENDA")]
	    public System.Nullable<System.Boolean> BitVenda
	    {
	    	    get
	    	    {
	    	          return _BitVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._BitVenda != value)
	    	          {
	    	              this.ValidateProperty("BitVenda", value);
	    	              this.OnBitVendaChanging(value);
	    	              this.RaiseDataMemberChanging("BitVenda");
	    	              this._BitVenda = value;
	    	              this.RaiseDataMemberChanged("BitVenda");
	    	              this.OnBitVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ComboboxVenda
	    partial void OnComboboxVendaChanging(Byte value);
	    partial void OnComboboxVendaChanged();

	    private Byte _ComboboxVenda;

	    [DataMember(IsRequired = true, Name = "ComboboxVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Venda", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_VENDA];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.COMBOBOX_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.COMBOBOX_VENDA")]
	    public Byte ComboboxVenda
	    {
	    	    get
	    	    {
	    	          return _ComboboxVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._ComboboxVenda != value)
	    	          {
	    	              this.ValidateProperty("ComboboxVenda", value);
	    	              this.OnComboboxVendaChanging(value);
	    	              this.RaiseDataMemberChanging("ComboboxVenda");
	    	              this._ComboboxVenda = value;
	    	              this.RaiseDataMemberChanged("ComboboxVenda");
	    	              this.OnComboboxVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DatetimeVenda
	    partial void OnDatetimeVendaChanging(System.Nullable<System.DateTime> value);
	    partial void OnDatetimeVendaChanged();

	    private System.Nullable<System.DateTime> _DatetimeVenda;

	    [DataMember(Name = "DatetimeVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Datetime Venda", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.DATETIME_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.DATETIME_VENDA")]
	    public System.Nullable<System.DateTime> DatetimeVenda
	    {
	    	    get
	    	    {
	    	          return _DatetimeVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._DatetimeVenda != value)
	    	          {
	    	              this.ValidateProperty("DatetimeVenda", value);
	    	              this.OnDatetimeVendaChanging(value);
	    	              this.RaiseDataMemberChanging("DatetimeVenda");
	    	              this._DatetimeVenda = value;
	    	              this.RaiseDataMemberChanged("DatetimeVenda");
	    	              this.OnDatetimeVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DecimalVenda
	    partial void OnDecimalVendaChanging(System.Nullable<System.Decimal> value);
	    partial void OnDecimalVendaChanged();

	    private System.Nullable<System.Decimal> _DecimalVenda;

	    [DataMember(Name = "DecimalVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal Venda", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.DECIMAL_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.DECIMAL_VENDA")]
	    public System.Nullable<System.Decimal> DecimalVenda
	    {
	    	    get
	    	    {
	    	          return _DecimalVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._DecimalVenda != value)
	    	          {
	    	              this.ValidateProperty("DecimalVenda", value);
	    	              this.OnDecimalVendaChanging(value);
	    	              this.RaiseDataMemberChanging("DecimalVenda");
	    	              this._DecimalVenda = value;
	    	              this.RaiseDataMemberChanged("DecimalVenda");
	    	              this.OnDecimalVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For GuidVenda
	    partial void OnGuidVendaChanging(System.Nullable<System.Guid> value);
	    partial void OnGuidVendaChanged();

	    private System.Nullable<System.Guid> _GuidVenda;

	    [DataMember(Name = "GuidVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Guid Venda", Description="", Order = 6, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.GUID_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.GUID_VENDA")]
	    public System.Nullable<System.Guid> GuidVenda
	    {
	    	    get
	    	    {
	    	          return _GuidVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._GuidVenda != value)
	    	          {
	    	              this.ValidateProperty("GuidVenda", value);
	    	              this.OnGuidVendaChanging(value);
	    	              this.RaiseDataMemberChanging("GuidVenda");
	    	              this._GuidVenda = value;
	    	              this.RaiseDataMemberChanged("GuidVenda");
	    	              this.OnGuidVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdCliente
	    partial void OnIdClienteChanging(System.Nullable<Int32> value);
	    partial void OnIdClienteChanged();

	    private System.Nullable<Int32> _IdCliente;

	    [DataMember(Name = "IdCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Cliente", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpCliente];LookUpTitle[Seleção de (Id Cliente)];LookUpQuery[executeLookUpCliente];LookUpFinalize[finalizeLookUpCliente];LookUpDisplayColumns[{\"IdCliente\" : \"Id Cliente\"}];LookUpColumns[{\"IdCliente\" : true}];FilterDataKey[VENDA.CLIENTE.ID_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<Int32>#IdCliente#true##12:0##Id Cliente#0#true##::LookUpCliente##false#false#CLIENTE#CLIENTE#VAREJO.BV.Diversos#IQueryable###true#false", EdmKey="VENDA.CLIENTE.ID_CLIENTE")]
	    public System.Nullable<Int32> IdCliente
	    {
	    	    get
	    	    {
	    	          return _IdCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdCliente != value)
	    	          {
	    	              this.ValidateProperty("IdCliente", value);
	    	              this.OnIdClienteChanging(value);
	    	              this.RaiseDataMemberChanging("IdCliente");
	    	              this._IdCliente = value;
	    	              this.RaiseDataMemberChanged("IdCliente");
	    	              this.OnIdClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLoja
	    partial void OnIdLojaChanging(System.Nullable<Int32> value);
	    partial void OnIdLojaChanged();

	    private System.Nullable<Int32> _IdLoja;

	    [DataMember(Name = "IdLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Loja", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpLoja];LookUpTitle[Seleção de (Id Loja)];LookUpQuery[executeLookUpLoja];LookUpFinalize[finalizeLookUpLoja];LookUpDisplayColumns[{\"IdLoja\" : \"Id Loja\"}];LookUpColumns[{\"IdLoja\" : true}];FilterDataKey[VENDA.LOJA.ID_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<Int32>#IdLoja#true##12:0##Id Loja#0#true##::LookUpLoja##false#false#LOJA#LOJA#VAREJO.BV.Diversos#IQueryable###true#false", EdmKey="VENDA.LOJA.ID_LOJA")]
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
	    	              this.ValidateProperty("IdLoja", value);
	    	              this.OnIdLojaChanging(value);
	    	              this.RaiseDataMemberChanging("IdLoja");
	    	              this._IdLoja = value;
	    	              this.RaiseDataMemberChanged("IdLoja");
	    	              this.OnIdLojaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdVenda
	    partial void OnIdVendaChanging(Int32 value);
	    partial void OnIdVendaChanged();

	    private Int32 _IdVenda;

	    [DataMember(IsRequired = true, Name = "IdVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Venda", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.ID_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.ID_VENDA")]
	    public Int32 IdVenda
	    {
	    	    get
	    	    {
	    	          return _IdVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdVenda != value)
	    	          {
	    	              this.ValidateProperty("IdVenda", value);
	    	              this.OnIdVendaChanging(value);
	    	              this.RaiseDataMemberChanging("IdVenda");
	    	              this._IdVenda = value;
	    	              this.RaiseDataMemberChanged("IdVenda");
	    	              this.OnIdVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IntVenda
	    partial void OnIntVendaChanging(System.Nullable<System.Int32> value);
	    partial void OnIntVendaChanged();

	    private System.Nullable<System.Int32> _IntVenda;

	    [DataMember(Name = "IntVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int Venda", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.INT_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.INT_VENDA")]
	    public System.Nullable<System.Int32> IntVenda
	    {
	    	    get
	    	    {
	    	          return _IntVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._IntVenda != value)
	    	          {
	    	              this.ValidateProperty("IntVenda", value);
	    	              this.OnIntVendaChanging(value);
	    	              this.RaiseDataMemberChanging("IntVenda");
	    	              this._IntVenda = value;
	    	              this.RaiseDataMemberChanged("IntVenda");
	    	              this.OnIntVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For SmallIntVenda
	    partial void OnSmallIntVendaChanging(System.Nullable<System.Int16> value);
	    partial void OnSmallIntVendaChanged();

	    private System.Nullable<System.Int16> _SmallIntVenda;

	    [DataMember(Name = "SmallIntVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Small Int Venda", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.SMALL_INT_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.SMALL_INT_VENDA")]
	    public System.Nullable<System.Int16> SmallIntVenda
	    {
	    	    get
	    	    {
	    	          return _SmallIntVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._SmallIntVenda != value)
	    	          {
	    	              this.ValidateProperty("SmallIntVenda", value);
	    	              this.OnSmallIntVendaChanging(value);
	    	              this.RaiseDataMemberChanging("SmallIntVenda");
	    	              this._SmallIntVenda = value;
	    	              this.RaiseDataMemberChanged("SmallIntVenda");
	    	              this.OnSmallIntVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringVenda
	    partial void OnStringVendaChanging(System.String value);
	    partial void OnStringVendaChanged();

	    private System.String _StringVenda;

	    [DataMember(Name = "StringVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Venda", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.STRING_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.STRING_VENDA")]
	    public System.String StringVenda
	    {
	    	    get
	    	    {
	    	          return _StringVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringVenda != value)
	    	          {
	    	              this.ValidateProperty("StringVenda", value);
	    	              this.OnStringVendaChanging(value);
	    	              this.RaiseDataMemberChanging("StringVenda");
	    	              this._StringVenda = value;
	    	              this.RaiseDataMemberChanged("StringVenda");
	    	              this.OnStringVendaChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIdVenda;
	    [DataMember(Name = "TemporaryIdVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Venda (Tmp)", Description="Temporary Key", Order = 7, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdVenda
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdVenda.IsNullOrEmpty())
	    	                this._TemporaryIdVenda = this._IdVenda;
	    	          return this._TemporaryIdVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdVenda != value)
	    	              this._TemporaryIdVenda = value;
	    	    }
	    }	

	    #endregion Data Properties

	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<VendaItem> _VendaItemList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_Venda_VendaItem", "IdVenda", "IdVenda", IsForeignKey=false)]
	    [DataMember(Name = "VendaItemList", EmitDefaultValue = true)]
	    public IEnumerable<VendaItem> VendaItemList
	    {
	        get
	        {
	
	            if (this._VendaItemList == null)
	            	this._VendaItemList = new List<VendaItem>();
	
	            return this._VendaItemList;
	        }
	        set
	        {
	            if (this._VendaItemList != value)
	            {
	                this._VendaItemList = value;
	                this.RaisePropertyChanged("VendaItemList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "DCLinxDemoBM.VENDA").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Demo.BM.VENDA), QualifiedEntitySetName = "DCLinxDemoBM.VENDA" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.ID_VENDA", Source = "IdVenda", Target = "ID_VENDA", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.BIT_VENDA", Source = "BitVenda", Target = "BIT_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.INT_VENDA", Source = "IntVenda", Target = "INT_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.GUID_VENDA", Source = "GuidVenda", Target = "GUID_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.LOJA.ID_LOJA", Source = "IdLoja", Target = "ID_LOJA", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "DCLinxDemoBM.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.STRING_VENDA", Source = "StringVenda", Target = "STRING_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.BIG_INT_VENDA", Source = "BigIntVenda", Target = "BIG_INT_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.DECIMAL_VENDA", Source = "DecimalVenda", Target = "DECIMAL_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.COMBOBOX_VENDA", Source = "ComboboxVenda", Target = "COMBOBOX_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.DATETIME_VENDA", Source = "DatetimeVenda", Target = "DATETIME_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.SMALL_INT_VENDA", Source = "SmallIntVenda", Target = "SMALL_INT_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.CLIENTE.ID_CLIENTE", Source = "IdCliente", Target = "ID_CLIENTE", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "DCLinxDemoBM.CLIENTE", RelationPropertyName = "CLIENTE" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 

	    private string _changeState = "N";
	    [DataMember()]
	    public string ChangeState { get { return _changeState; } set { _changeState = value; } }	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetComboboxVendaValues()
	    {
	    	    return VAREJO.BV.Domains.LX_VENDA.GetValues();
	    }
	    private string _comboboxVendaName;
	    [DataMember(IsRequired = false, Name = "ComboboxVendaName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Combobox Venda", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ComboboxVendaName
	    {
	    	    get { if (this.ComboboxVenda.IsNull()) { _comboboxVendaName = String.Empty; } else { string key = this.ComboboxVenda.ToString(); var dmValues = this.GetComboboxVendaValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _comboboxVendaName) _comboboxVendaName = domainName; } return _comboboxVendaName; } set { _comboboxVendaName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="VENDA_ITEM.ID_VENDA_ITEM", IsUpdatable=false, EdmName="Linx.Demo.BM.DCLinxDemoBM")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[VendaItem];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdVendaItem];ReadOnly[false];Entities[VENDA_ITEM:IdVendaItem];SubQueryInfo[Select 1 From #ParentAlias#.VENDA_ITEM_LISTA as #Alias#];EdmEntityName[VENDA_ITEM];EntityRelations[VENDA(VENDA)#CLIENTE(CLIENTE)#ESTADO(ESTADO)#PAIS(PAIS)#LOJA(LOJA)];EdmParentEntityName[VENDA];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "VendaItem")]
	[Serializable()]
	public partial class VendaItem : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(DiversosDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("Venda");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdVenda"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdVenda));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load Venda
	         this.Venda = (from r in context.GetVendaByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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

	    public virtual void ResetChangeState()
	    {
	      this.ChangeState = "N";
	    }

	    #endregion Flat Entities

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For BigIntVendaItem
	    partial void OnBigIntVendaItemChanging(System.Nullable<System.Int64> value);
	    partial void OnBigIntVendaItemChanged();

	    private System.Nullable<System.Int64> _BigIntVendaItem;

	    [DataMember(Name = "BigIntVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Big Int Venda Item", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.BIG_INT_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.BIG_INT_VENDA_ITEM")]
	    public System.Nullable<System.Int64> BigIntVendaItem
	    {
	    	    get
	    	    {
	    	          return _BigIntVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._BigIntVendaItem != value)
	    	          {
	    	              this.ValidateProperty("BigIntVendaItem", value);
	    	              this.OnBigIntVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("BigIntVendaItem");
	    	              this._BigIntVendaItem = value;
	    	              this.RaiseDataMemberChanged("BigIntVendaItem");
	    	              this.OnBigIntVendaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For BitVendaItem
	    partial void OnBitVendaItemChanging(System.Nullable<System.Boolean> value);
	    partial void OnBitVendaItemChanged();

	    private System.Nullable<System.Boolean> _BitVendaItem;

	    [DataMember(Name = "BitVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bit Venda Item", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.BIT_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.BIT_VENDA_ITEM")]
	    public System.Nullable<System.Boolean> BitVendaItem
	    {
	    	    get
	    	    {
	    	          return _BitVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._BitVendaItem != value)
	    	          {
	    	              this.ValidateProperty("BitVendaItem", value);
	    	              this.OnBitVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("BitVendaItem");
	    	              this._BitVendaItem = value;
	    	              this.RaiseDataMemberChanged("BitVendaItem");
	    	              this.OnBitVendaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ComboboxVendaItem
	    partial void OnComboboxVendaItemChanging(Byte value);
	    partial void OnComboboxVendaItemChanged();

	    private Byte _ComboboxVendaItem;

	    [DataMember(IsRequired = true, Name = "ComboboxVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Venda Item", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_VENDA_ITEM];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.COMBOBOX_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.COMBOBOX_VENDA_ITEM")]
	    public Byte ComboboxVendaItem
	    {
	    	    get
	    	    {
	    	          return _ComboboxVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._ComboboxVendaItem != value)
	    	          {
	    	              this.ValidateProperty("ComboboxVendaItem", value);
	    	              this.OnComboboxVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("ComboboxVendaItem");
	    	              this._ComboboxVendaItem = value;
	    	              this.RaiseDataMemberChanged("ComboboxVendaItem");
	    	              this.OnComboboxVendaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DatetimeVendaItem
	    partial void OnDatetimeVendaItemChanging(System.Nullable<System.DateTime> value);
	    partial void OnDatetimeVendaItemChanged();

	    private System.Nullable<System.DateTime> _DatetimeVendaItem;

	    [DataMember(Name = "DatetimeVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Datetime Venda Item", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.DATETIME_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.DATETIME_VENDA_ITEM")]
	    public System.Nullable<System.DateTime> DatetimeVendaItem
	    {
	    	    get
	    	    {
	    	          return _DatetimeVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._DatetimeVendaItem != value)
	    	          {
	    	              this.ValidateProperty("DatetimeVendaItem", value);
	    	              this.OnDatetimeVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("DatetimeVendaItem");
	    	              this._DatetimeVendaItem = value;
	    	              this.RaiseDataMemberChanged("DatetimeVendaItem");
	    	              this.OnDatetimeVendaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DecimalVendaItem
	    partial void OnDecimalVendaItemChanging(System.Nullable<System.Decimal> value);
	    partial void OnDecimalVendaItemChanged();

	    private System.Nullable<System.Decimal> _DecimalVendaItem;

	    [DataMember(Name = "DecimalVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal Venda Item", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.DECIMAL_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.DECIMAL_VENDA_ITEM")]
	    public System.Nullable<System.Decimal> DecimalVendaItem
	    {
	    	    get
	    	    {
	    	          return _DecimalVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._DecimalVendaItem != value)
	    	          {
	    	              this.ValidateProperty("DecimalVendaItem", value);
	    	              this.OnDecimalVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("DecimalVendaItem");
	    	              this._DecimalVendaItem = value;
	    	              this.RaiseDataMemberChanged("DecimalVendaItem");
	    	              this.OnDecimalVendaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For GuidVendaItem
	    partial void OnGuidVendaItemChanging(System.Nullable<System.Guid> value);
	    partial void OnGuidVendaItemChanged();

	    private System.Nullable<System.Guid> _GuidVendaItem;

	    [DataMember(Name = "GuidVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Guid Venda Item", Description="", Order = 6, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.GUID_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.GUID_VENDA_ITEM")]
	    public System.Nullable<System.Guid> GuidVendaItem
	    {
	    	    get
	    	    {
	    	          return _GuidVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._GuidVendaItem != value)
	    	          {
	    	              this.ValidateProperty("GuidVendaItem", value);
	    	              this.OnGuidVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("GuidVendaItem");
	    	              this._GuidVendaItem = value;
	    	              this.RaiseDataMemberChanged("GuidVendaItem");
	    	              this.OnGuidVendaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdVenda
	    partial void OnIdVendaChanging(System.Nullable<Int32> value);
	    partial void OnIdVendaChanged();

	    private System.Nullable<Int32> _IdVenda;

	    [DataMember(Name = "IdVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Venda", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.VENDA.ID_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.VENDA.ID_VENDA")]
	    public System.Nullable<Int32> IdVenda
	    {
	    	    get
	    	    {
	    	          return _IdVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdVenda != value)
	    	          {
	    	              this.ValidateProperty("IdVenda", value);
	    	              this.OnIdVendaChanging(value);
	    	              this.RaiseDataMemberChanging("IdVenda");
	    	              this._IdVenda = value;
	    	              this.RaiseDataMemberChanged("IdVenda");
	    	              this.OnIdVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdVendaItem
	    partial void OnIdVendaItemChanging(Int32 value);
	    partial void OnIdVendaItemChanged();

	    private Int32 _IdVendaItem;

	    [DataMember(IsRequired = true, Name = "IdVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Venda Item", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.ID_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.ID_VENDA_ITEM")]
	    public Int32 IdVendaItem
	    {
	    	    get
	    	    {
	    	          return _IdVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdVendaItem != value)
	    	          {
	    	              this.ValidateProperty("IdVendaItem", value);
	    	              this.OnIdVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("IdVendaItem");
	    	              this._IdVendaItem = value;
	    	              this.RaiseDataMemberChanged("IdVendaItem");
	    	              this.OnIdVendaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IntVendaItem
	    partial void OnIntVendaItemChanging(System.Nullable<System.Int32> value);
	    partial void OnIntVendaItemChanged();

	    private System.Nullable<System.Int32> _IntVendaItem;

	    [DataMember(Name = "IntVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int Venda Item", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.INT_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.INT_VENDA_ITEM")]
	    public System.Nullable<System.Int32> IntVendaItem
	    {
	    	    get
	    	    {
	    	          return _IntVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._IntVendaItem != value)
	    	          {
	    	              this.ValidateProperty("IntVendaItem", value);
	    	              this.OnIntVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("IntVendaItem");
	    	              this._IntVendaItem = value;
	    	              this.RaiseDataMemberChanged("IntVendaItem");
	    	              this.OnIntVendaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For SmallIntVendaItem
	    partial void OnSmallIntVendaItemChanging(System.Nullable<System.Int16> value);
	    partial void OnSmallIntVendaItemChanged();

	    private System.Nullable<System.Int16> _SmallIntVendaItem;

	    [DataMember(Name = "SmallIntVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Small Int Venda Item", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.SMALL_INT_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.SMALL_INT_VENDA_ITEM")]
	    public System.Nullable<System.Int16> SmallIntVendaItem
	    {
	    	    get
	    	    {
	    	          return _SmallIntVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._SmallIntVendaItem != value)
	    	          {
	    	              this.ValidateProperty("SmallIntVendaItem", value);
	    	              this.OnSmallIntVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("SmallIntVendaItem");
	    	              this._SmallIntVendaItem = value;
	    	              this.RaiseDataMemberChanged("SmallIntVendaItem");
	    	              this.OnSmallIntVendaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringVendaItem
	    partial void OnStringVendaItemChanging(System.String value);
	    partial void OnStringVendaItemChanged();

	    private System.String _StringVendaItem;

	    [DataMember(Name = "StringVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Venda Item", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.STRING_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.STRING_VENDA_ITEM")]
	    public System.String StringVendaItem
	    {
	    	    get
	    	    {
	    	          return _StringVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringVendaItem != value)
	    	          {
	    	              this.ValidateProperty("StringVendaItem", value);
	    	              this.OnStringVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("StringVendaItem");
	    	              this._StringVendaItem = value;
	    	              this.RaiseDataMemberChanged("StringVendaItem");
	    	              this.OnStringVendaItemChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIdVendaItem;
	    [DataMember(Name = "TemporaryIdVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Venda Item (Tmp)", Description="Temporary Key", Order = 7, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdVendaItem
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdVendaItem.IsNullOrEmpty())
	    	                this._TemporaryIdVendaItem = this._IdVendaItem;
	    	          return this._TemporaryIdVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdVendaItem != value)
	    	              this._TemporaryIdVendaItem = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private Venda _Venda;
	    [DataMember(Name = "Venda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_Venda_VendaItem", "IdVenda", "IdVenda", IsForeignKey=true)]
	    public Venda Venda
	    {
	        get
	        {
	            return this._Venda;
	        }
	        set
	        {
	            if (this._Venda != value)
	            {
	                this._Venda = value;
	                this.RaisePropertyChanged("VendaList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "DCLinxDemoBM.VENDA_ITEM").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Demo.BM.VENDA_ITEM), QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ITEM" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.ID_VENDA_ITEM", Source = "IdVendaItem", Target = "ID_VENDA_ITEM", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.BIT_VENDA_ITEM", Source = "BitVendaItem", Target = "BIT_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.VENDA.ID_VENDA", Source = "IdVenda", Target = "ID_VENDA", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "DCLinxDemoBM.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.INT_VENDA_ITEM", Source = "IntVendaItem", Target = "INT_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.GUID_VENDA_ITEM", Source = "GuidVendaItem", Target = "GUID_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.STRING_VENDA_ITEM", Source = "StringVendaItem", Target = "STRING_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.BIG_INT_VENDA_ITEM", Source = "BigIntVendaItem", Target = "BIG_INT_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.DECIMAL_VENDA_ITEM", Source = "DecimalVendaItem", Target = "DECIMAL_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.COMBOBOX_VENDA_ITEM", Source = "ComboboxVendaItem", Target = "COMBOBOX_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.DATETIME_VENDA_ITEM", Source = "DatetimeVendaItem", Target = "DATETIME_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.SMALL_INT_VENDA_ITEM", Source = "SmallIntVendaItem", Target = "SMALL_INT_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 

	    private string _changeState = "N";
	    [DataMember()]
	    public string ChangeState { get { return _changeState; } set { _changeState = value; } }	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetComboboxVendaItemValues()
	    {
	    	    return VAREJO.BV.Domains.LX_VENDA_ITEM.GetValues();
	    }
	    private string _comboboxVendaItemName;
	    [DataMember(IsRequired = false, Name = "ComboboxVendaItemName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Combobox Venda Item", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ComboboxVendaItemName
	    {
	    	    get { if (this.ComboboxVendaItem.IsNull()) { _comboboxVendaItemName = String.Empty; } else { string key = this.ComboboxVendaItem.ToString(); var dmValues = this.GetComboboxVendaItemValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _comboboxVendaItemName) _comboboxVendaItemName = domainName; } return _comboboxVendaItemName; } set { _comboboxVendaItemName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[VendaItem];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdVendaItem];ReadOnly[false];Entities[VENDA_ITEM:IdVendaItem];SubQueryInfo[Select 1 From #ParentAlias#.VENDA_ITEM_LISTA as #Alias#];EdmEntityName[VENDA_ITEM];EntityRelations[VENDA(VENDA)#CLIENTE(CLIENTE)#ESTADO(ESTADO)#PAIS(PAIS)#LOJA(LOJA)];EdmParentEntityName[VENDA];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "VendaItem")]
	[Serializable()]
	public partial class VendaItemParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For BigIntVendaItem
	    partial void OnBigIntVendaItemChanging(System.Nullable<System.Int64> value);
	    partial void OnBigIntVendaItemChanged();

	    private System.Nullable<System.Int64> _BigIntVendaItem;

	    [DataMember(Name = "BigIntVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Big Int Venda Item", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.BIG_INT_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.BIG_INT_VENDA_ITEM")]
	    public System.Nullable<System.Int64> BigIntVendaItem
	    {
	    	    get
	    	    {
	    	          return _BigIntVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._BigIntVendaItem != value)
	    	          {
	    	              this.ValidateProperty("BigIntVendaItem", value);
	    	              this.OnBigIntVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("BigIntVendaItem");
	    	              this._BigIntVendaItem = value;
	    	              this.RaiseDataMemberChanged("BigIntVendaItem");
	    	              this.OnBigIntVendaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For BitVendaItem
	    partial void OnBitVendaItemChanging(System.Nullable<System.Boolean> value);
	    partial void OnBitVendaItemChanged();

	    private System.Nullable<System.Boolean> _BitVendaItem;

	    [DataMember(Name = "BitVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bit Venda Item", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.BIT_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.BIT_VENDA_ITEM")]
	    public System.Nullable<System.Boolean> BitVendaItem
	    {
	    	    get
	    	    {
	    	          return _BitVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._BitVendaItem != value)
	    	          {
	    	              this.ValidateProperty("BitVendaItem", value);
	    	              this.OnBitVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("BitVendaItem");
	    	              this._BitVendaItem = value;
	    	              this.RaiseDataMemberChanged("BitVendaItem");
	    	              this.OnBitVendaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ComboboxVendaItem
	    partial void OnComboboxVendaItemChanging(Byte value);
	    partial void OnComboboxVendaItemChanged();

	    private Byte _ComboboxVendaItem;

	    [DataMember(IsRequired = true, Name = "ComboboxVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Venda Item", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_VENDA_ITEM];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.COMBOBOX_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.COMBOBOX_VENDA_ITEM")]
	    public Byte ComboboxVendaItem
	    {
	    	    get
	    	    {
	    	          return _ComboboxVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._ComboboxVendaItem != value)
	    	          {
	    	              this.ValidateProperty("ComboboxVendaItem", value);
	    	              this.OnComboboxVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("ComboboxVendaItem");
	    	              this._ComboboxVendaItem = value;
	    	              this.RaiseDataMemberChanged("ComboboxVendaItem");
	    	              this.OnComboboxVendaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DatetimeVendaItem
	    partial void OnDatetimeVendaItemChanging(System.Nullable<System.DateTime> value);
	    partial void OnDatetimeVendaItemChanged();

	    private System.Nullable<System.DateTime> _DatetimeVendaItem;

	    [DataMember(Name = "DatetimeVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Datetime Venda Item", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.DATETIME_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.DATETIME_VENDA_ITEM")]
	    public System.Nullable<System.DateTime> DatetimeVendaItem
	    {
	    	    get
	    	    {
	    	          return _DatetimeVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._DatetimeVendaItem != value)
	    	          {
	    	              this.ValidateProperty("DatetimeVendaItem", value);
	    	              this.OnDatetimeVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("DatetimeVendaItem");
	    	              this._DatetimeVendaItem = value;
	    	              this.RaiseDataMemberChanged("DatetimeVendaItem");
	    	              this.OnDatetimeVendaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DecimalVendaItem
	    partial void OnDecimalVendaItemChanging(System.Nullable<System.Decimal> value);
	    partial void OnDecimalVendaItemChanged();

	    private System.Nullable<System.Decimal> _DecimalVendaItem;

	    [DataMember(Name = "DecimalVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal Venda Item", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.DECIMAL_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.DECIMAL_VENDA_ITEM")]
	    public System.Nullable<System.Decimal> DecimalVendaItem
	    {
	    	    get
	    	    {
	    	          return _DecimalVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._DecimalVendaItem != value)
	    	          {
	    	              this.ValidateProperty("DecimalVendaItem", value);
	    	              this.OnDecimalVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("DecimalVendaItem");
	    	              this._DecimalVendaItem = value;
	    	              this.RaiseDataMemberChanged("DecimalVendaItem");
	    	              this.OnDecimalVendaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For GuidVendaItem
	    partial void OnGuidVendaItemChanging(System.Nullable<System.Guid> value);
	    partial void OnGuidVendaItemChanged();

	    private System.Nullable<System.Guid> _GuidVendaItem;

	    [DataMember(Name = "GuidVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Guid Venda Item", Description="", Order = 6, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.GUID_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.GUID_VENDA_ITEM")]
	    public System.Nullable<System.Guid> GuidVendaItem
	    {
	    	    get
	    	    {
	    	          return _GuidVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._GuidVendaItem != value)
	    	          {
	    	              this.ValidateProperty("GuidVendaItem", value);
	    	              this.OnGuidVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("GuidVendaItem");
	    	              this._GuidVendaItem = value;
	    	              this.RaiseDataMemberChanged("GuidVendaItem");
	    	              this.OnGuidVendaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdVenda
	    partial void OnIdVendaChanging(System.Nullable<Int32> value);
	    partial void OnIdVendaChanged();

	    private System.Nullable<Int32> _IdVenda;

	    [DataMember(Name = "IdVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Venda", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.VENDA.ID_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.VENDA.ID_VENDA")]
	    public System.Nullable<Int32> IdVenda
	    {
	    	    get
	    	    {
	    	          return _IdVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdVenda != value)
	    	          {
	    	              this.ValidateProperty("IdVenda", value);
	    	              this.OnIdVendaChanging(value);
	    	              this.RaiseDataMemberChanging("IdVenda");
	    	              this._IdVenda = value;
	    	              this.RaiseDataMemberChanged("IdVenda");
	    	              this.OnIdVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdVendaItem
	    partial void OnIdVendaItemChanging(Int32 value);
	    partial void OnIdVendaItemChanged();

	    private Int32 _IdVendaItem;

	    [DataMember(IsRequired = true, Name = "IdVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Venda Item", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.ID_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.ID_VENDA_ITEM")]
	    public Int32 IdVendaItem
	    {
	    	    get
	    	    {
	    	          return _IdVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdVendaItem != value)
	    	          {
	    	              this.ValidateProperty("IdVendaItem", value);
	    	              this.OnIdVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("IdVendaItem");
	    	              this._IdVendaItem = value;
	    	              this.RaiseDataMemberChanged("IdVendaItem");
	    	              this.OnIdVendaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IntVendaItem
	    partial void OnIntVendaItemChanging(System.Nullable<System.Int32> value);
	    partial void OnIntVendaItemChanged();

	    private System.Nullable<System.Int32> _IntVendaItem;

	    [DataMember(Name = "IntVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int Venda Item", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.INT_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.INT_VENDA_ITEM")]
	    public System.Nullable<System.Int32> IntVendaItem
	    {
	    	    get
	    	    {
	    	          return _IntVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._IntVendaItem != value)
	    	          {
	    	              this.ValidateProperty("IntVendaItem", value);
	    	              this.OnIntVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("IntVendaItem");
	    	              this._IntVendaItem = value;
	    	              this.RaiseDataMemberChanged("IntVendaItem");
	    	              this.OnIntVendaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For SmallIntVendaItem
	    partial void OnSmallIntVendaItemChanging(System.Nullable<System.Int16> value);
	    partial void OnSmallIntVendaItemChanged();

	    private System.Nullable<System.Int16> _SmallIntVendaItem;

	    [DataMember(Name = "SmallIntVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Small Int Venda Item", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.SMALL_INT_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.SMALL_INT_VENDA_ITEM")]
	    public System.Nullable<System.Int16> SmallIntVendaItem
	    {
	    	    get
	    	    {
	    	          return _SmallIntVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._SmallIntVendaItem != value)
	    	          {
	    	              this.ValidateProperty("SmallIntVendaItem", value);
	    	              this.OnSmallIntVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("SmallIntVendaItem");
	    	              this._SmallIntVendaItem = value;
	    	              this.RaiseDataMemberChanged("SmallIntVendaItem");
	    	              this.OnSmallIntVendaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringVendaItem
	    partial void OnStringVendaItemChanging(System.String value);
	    partial void OnStringVendaItemChanged();

	    private System.String _StringVendaItem;

	    [DataMember(Name = "StringVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Venda Item", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.STRING_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.STRING_VENDA_ITEM")]
	    public System.String StringVendaItem
	    {
	    	    get
	    	    {
	    	          return _StringVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringVendaItem != value)
	    	          {
	    	              this.ValidateProperty("StringVendaItem", value);
	    	              this.OnStringVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("StringVendaItem");
	    	              this._StringVendaItem = value;
	    	              this.RaiseDataMemberChanged("StringVendaItem");
	    	              this.OnStringVendaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For BigIntVenda
	    partial void OnBigIntVendaChanging(System.Nullable<System.Int64> value);
	    partial void OnBigIntVendaChanged();

	    private System.Nullable<System.Int64> _BigIntVenda;

	    [DataMember(Name = "BigIntVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Big Int Venda", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.BIG_INT_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.BIG_INT_VENDA")]
	    public System.Nullable<System.Int64> BigIntVenda
	    {
	    	    get
	    	    {
	    	          return _BigIntVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._BigIntVenda != value)
	    	          {
	    	              this.ValidateProperty("BigIntVenda", value);
	    	              this.OnBigIntVendaChanging(value);
	    	              this.RaiseDataMemberChanging("BigIntVenda");
	    	              this._BigIntVenda = value;
	    	              this.RaiseDataMemberChanged("BigIntVenda");
	    	              this.OnBigIntVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For BitVenda
	    partial void OnBitVendaChanging(System.Nullable<System.Boolean> value);
	    partial void OnBitVendaChanged();

	    private System.Nullable<System.Boolean> _BitVenda;

	    [DataMember(Name = "BitVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bit Venda", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.BIT_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.BIT_VENDA")]
	    public System.Nullable<System.Boolean> BitVenda
	    {
	    	    get
	    	    {
	    	          return _BitVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._BitVenda != value)
	    	          {
	    	              this.ValidateProperty("BitVenda", value);
	    	              this.OnBitVendaChanging(value);
	    	              this.RaiseDataMemberChanging("BitVenda");
	    	              this._BitVenda = value;
	    	              this.RaiseDataMemberChanged("BitVenda");
	    	              this.OnBitVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ComboboxVenda
	    partial void OnComboboxVendaChanging(Byte value);
	    partial void OnComboboxVendaChanged();

	    private Byte _ComboboxVenda;

	    [DataMember(IsRequired = true, Name = "ComboboxVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Venda", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_VENDA];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.COMBOBOX_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.COMBOBOX_VENDA")]
	    public Byte ComboboxVenda
	    {
	    	    get
	    	    {
	    	          return _ComboboxVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._ComboboxVenda != value)
	    	          {
	    	              this.ValidateProperty("ComboboxVenda", value);
	    	              this.OnComboboxVendaChanging(value);
	    	              this.RaiseDataMemberChanging("ComboboxVenda");
	    	              this._ComboboxVenda = value;
	    	              this.RaiseDataMemberChanged("ComboboxVenda");
	    	              this.OnComboboxVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DatetimeVenda
	    partial void OnDatetimeVendaChanging(System.Nullable<System.DateTime> value);
	    partial void OnDatetimeVendaChanged();

	    private System.Nullable<System.DateTime> _DatetimeVenda;

	    [DataMember(Name = "DatetimeVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Datetime Venda", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.DATETIME_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.DATETIME_VENDA")]
	    public System.Nullable<System.DateTime> DatetimeVenda
	    {
	    	    get
	    	    {
	    	          return _DatetimeVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._DatetimeVenda != value)
	    	          {
	    	              this.ValidateProperty("DatetimeVenda", value);
	    	              this.OnDatetimeVendaChanging(value);
	    	              this.RaiseDataMemberChanging("DatetimeVenda");
	    	              this._DatetimeVenda = value;
	    	              this.RaiseDataMemberChanged("DatetimeVenda");
	    	              this.OnDatetimeVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DecimalVenda
	    partial void OnDecimalVendaChanging(System.Nullable<System.Decimal> value);
	    partial void OnDecimalVendaChanged();

	    private System.Nullable<System.Decimal> _DecimalVenda;

	    [DataMember(Name = "DecimalVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal Venda", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.DECIMAL_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.DECIMAL_VENDA")]
	    public System.Nullable<System.Decimal> DecimalVenda
	    {
	    	    get
	    	    {
	    	          return _DecimalVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._DecimalVenda != value)
	    	          {
	    	              this.ValidateProperty("DecimalVenda", value);
	    	              this.OnDecimalVendaChanging(value);
	    	              this.RaiseDataMemberChanging("DecimalVenda");
	    	              this._DecimalVenda = value;
	    	              this.RaiseDataMemberChanged("DecimalVenda");
	    	              this.OnDecimalVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For GuidVenda
	    partial void OnGuidVendaChanging(System.Nullable<System.Guid> value);
	    partial void OnGuidVendaChanged();

	    private System.Nullable<System.Guid> _GuidVenda;

	    [DataMember(Name = "GuidVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Guid Venda", Description="", Order = 6, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.GUID_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.GUID_VENDA")]
	    public System.Nullable<System.Guid> GuidVenda
	    {
	    	    get
	    	    {
	    	          return _GuidVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._GuidVenda != value)
	    	          {
	    	              this.ValidateProperty("GuidVenda", value);
	    	              this.OnGuidVendaChanging(value);
	    	              this.RaiseDataMemberChanging("GuidVenda");
	    	              this._GuidVenda = value;
	    	              this.RaiseDataMemberChanged("GuidVenda");
	    	              this.OnGuidVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdCliente
	    partial void OnIdClienteChanging(System.Nullable<Int32> value);
	    partial void OnIdClienteChanged();

	    private System.Nullable<Int32> _IdCliente;

	    [DataMember(Name = "IdCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Cliente", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.CLIENTE.ID_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.CLIENTE.ID_CLIENTE")]
	    public System.Nullable<Int32> IdCliente
	    {
	    	    get
	    	    {
	    	          return _IdCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdCliente != value)
	    	          {
	    	              this.ValidateProperty("IdCliente", value);
	    	              this.OnIdClienteChanging(value);
	    	              this.RaiseDataMemberChanging("IdCliente");
	    	              this._IdCliente = value;
	    	              this.RaiseDataMemberChanged("IdCliente");
	    	              this.OnIdClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLoja
	    partial void OnIdLojaChanging(System.Nullable<Int32> value);
	    partial void OnIdLojaChanged();

	    private System.Nullable<Int32> _IdLoja;

	    [DataMember(Name = "IdLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Loja", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.LOJA.ID_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.LOJA.ID_LOJA")]
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
	    	              this.ValidateProperty("IdLoja", value);
	    	              this.OnIdLojaChanging(value);
	    	              this.RaiseDataMemberChanging("IdLoja");
	    	              this._IdLoja = value;
	    	              this.RaiseDataMemberChanged("IdLoja");
	    	              this.OnIdLojaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IntVenda
	    partial void OnIntVendaChanging(System.Nullable<System.Int32> value);
	    partial void OnIntVendaChanged();

	    private System.Nullable<System.Int32> _IntVenda;

	    [DataMember(Name = "IntVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int Venda", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.INT_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.INT_VENDA")]
	    public System.Nullable<System.Int32> IntVenda
	    {
	    	    get
	    	    {
	    	          return _IntVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._IntVenda != value)
	    	          {
	    	              this.ValidateProperty("IntVenda", value);
	    	              this.OnIntVendaChanging(value);
	    	              this.RaiseDataMemberChanging("IntVenda");
	    	              this._IntVenda = value;
	    	              this.RaiseDataMemberChanged("IntVenda");
	    	              this.OnIntVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For SmallIntVenda
	    partial void OnSmallIntVendaChanging(System.Nullable<System.Int16> value);
	    partial void OnSmallIntVendaChanged();

	    private System.Nullable<System.Int16> _SmallIntVenda;

	    [DataMember(Name = "SmallIntVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Small Int Venda", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.SMALL_INT_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.SMALL_INT_VENDA")]
	    public System.Nullable<System.Int16> SmallIntVenda
	    {
	    	    get
	    	    {
	    	          return _SmallIntVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._SmallIntVenda != value)
	    	          {
	    	              this.ValidateProperty("SmallIntVenda", value);
	    	              this.OnSmallIntVendaChanging(value);
	    	              this.RaiseDataMemberChanging("SmallIntVenda");
	    	              this._SmallIntVenda = value;
	    	              this.RaiseDataMemberChanged("SmallIntVenda");
	    	              this.OnSmallIntVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringVenda
	    partial void OnStringVendaChanging(System.String value);
	    partial void OnStringVendaChanged();

	    private System.String _StringVenda;

	    [DataMember(Name = "StringVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Venda", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.STRING_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.STRING_VENDA")]
	    public System.String StringVenda
	    {
	    	    get
	    	    {
	    	          return _StringVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringVenda != value)
	    	          {
	    	              this.ValidateProperty("StringVenda", value);
	    	              this.OnStringVendaChanging(value);
	    	              this.RaiseDataMemberChanging("StringVenda");
	    	              this._StringVenda = value;
	    	              this.RaiseDataMemberChanged("StringVenda");
	    	              this.OnStringVendaChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "DCLinxDemoBM.VENDA_ITEM").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Demo.BM.VENDA_ITEM), QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ITEM" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.ID_VENDA_ITEM", Source = "IdVendaItem", Target = "ID_VENDA_ITEM", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.BIT_VENDA_ITEM", Source = "BitVendaItem", Target = "BIT_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.VENDA.ID_VENDA", Source = "IdVenda", Target = "ID_VENDA", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "DCLinxDemoBM.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.INT_VENDA_ITEM", Source = "IntVendaItem", Target = "INT_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.GUID_VENDA_ITEM", Source = "GuidVendaItem", Target = "GUID_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.STRING_VENDA_ITEM", Source = "StringVendaItem", Target = "STRING_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.BIG_INT_VENDA_ITEM", Source = "BigIntVendaItem", Target = "BIG_INT_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.DECIMAL_VENDA_ITEM", Source = "DecimalVendaItem", Target = "DECIMAL_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.COMBOBOX_VENDA_ITEM", Source = "ComboboxVendaItem", Target = "COMBOBOX_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.DATETIME_VENDA_ITEM", Source = "DatetimeVendaItem", Target = "DATETIME_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.SMALL_INT_VENDA_ITEM", Source = "SmallIntVendaItem", Target = "SMALL_INT_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 

	    private string _changeState = "N";
	    [DataMember()]
	    public string ChangeState { get { return _changeState; } set { _changeState = value; } }	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetComboboxVendaItemValues()
	    {
	    	    return VAREJO.BV.Domains.LX_VENDA_ITEM.GetValues();
	    }
	    private string _comboboxVendaItemName;
	    [DataMember(IsRequired = false, Name = "ComboboxVendaItemName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Combobox Venda Item", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ComboboxVendaItemName
	    {
	    	    get { if (this.ComboboxVendaItem.IsNull()) { _comboboxVendaItemName = String.Empty; } else { string key = this.ComboboxVendaItem.ToString(); var dmValues = this.GetComboboxVendaItemValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _comboboxVendaItemName) _comboboxVendaItemName = domainName; } return _comboboxVendaItemName; } set { _comboboxVendaItemName = value;  }
	    }
	    public Dictionary<string, string> GetComboboxVendaValues()
	    {
	    	    return VAREJO.BV.Domains.LX_VENDA.GetValues();
	    }
	    private string _comboboxVendaName;
	    [DataMember(IsRequired = false, Name = "ComboboxVendaName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Combobox Venda", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ComboboxVendaName
	    {
	    	    get { if (this.ComboboxVenda.IsNull()) { _comboboxVendaName = String.Empty; } else { string key = this.ComboboxVenda.ToString(); var dmValues = this.GetComboboxVendaValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _comboboxVendaName) _comboboxVendaName = domainName; } return _comboboxVendaName; } set { _comboboxVendaName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	//////////////////////// DomainService Class V1 ///////////////////////
	///////////////////////////////////////////////////////////////////////
	[EnableClientAccess()]	
	[DomainIdentifier("ProcessorOverviewDiversosDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class DiversosDomainService : DomainService, IDataServiceContext 
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

	
	    private bool _hasGpeconControl;
	    public bool HasGpeconControl { get { return _hasGpeconControl; } }
	
	    private Linx.Demo.BM.DCLinxDemoBM _dbContext;
	    protected Linx.Demo.BM.DCLinxDemoBM DbContext 
	    { 
	    	get 
	    	{
	        	if (this._dbContext == null)
	        	{
	        		this._dbContext = new Linx.Demo.BM.DCLinxDemoBM(connectionString, this.Headers);
	        		((System.Data.Entity.Infrastructure.IObjectContextAdapter)this._dbContext).ObjectContext.CommandTimeout = 180;
	        		this._hasGpeconControl = (!(this._dbContext.IsUserMultiGpecon && this._dbContext.IdGpecon == this._dbContext.IdLinx) && this._dbContext.IdGpecon > 0);		
	        	}
	        	return this._dbContext;
	    	}
	    }

	    public string GetModelAssemblyName()
	    {
	        return typeof(Linx.Demo.BM.DCLinxDemoBM).Assembly.FullName;
	    }

	    public System.Data.Entity.Database Database
	    {
	        get { return this.DbContext.Database; }
	    }

		
	    public DiversosDomainService() : this("", null, null) { }
	    public DiversosDomainService(string connectionString) : this(connectionString, null, null) { }
	    public DiversosDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public DiversosDomainService(Linx.Demo.BM.DCLinxDemoBM dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public DiversosDomainService(string connectionString, Linx.Demo.BM.DCLinxDemoBM dataContext, Dictionary<string, string> headers) : base() 
	    { 
	    	this.connectionString = connectionString;
	    	this.Headers = headers;
	    	this._dbContext = dataContext; 


	    	this.OnCreate(); 
	    }

	    [Ignore]
	    public List<DataKeyMapping> SaveEntities(List<ChangeSetEntry> changeSetEntries)
	    {
	      return SaveEntities(changeSetEntries, true);
	    }

	    [Ignore]
	    public List<DataKeyMapping> SaveEntities(List<ChangeSetEntry> changeSetEntries, bool ctrlKeyMapping)
	    {
	      if (changeSetEntries.Count == 0) return null;
	      
	      this.Initialize();
	      _keyMappings.Clear();
	      _controlKeyMapping = ctrlKeyMapping;
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
	    public Linx.Demo.BM.DCLinxDemoBM GetEDM()
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
 	        var _VendaElements = changeSet.ChangeSetEntries.Where(e => e.Entity is Venda && e.Entity.GetType().Name == "Venda" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _VendaElements)
 	           if (((Venda)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 
 	        //Remove inconsistent details
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is VendaItem && e.Entity.GetType().Name == "VendaItem" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
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
	
		
			
        [Ignore]
	    //Get All LookUpCliente.
	    public IQueryable<LookUpCliente> GetAllLookUpCliente()
	    {
	        return this.GetLookUpCliente(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpCliente By EntitySearch.
	    public IQueryable<LookUpCliente> GetLookUpClienteByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpCliente(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpCliente.
	    public IQueryable<LookUpCliente> GetLookUpCliente(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "CLIENTE" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpCliente";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpCliente));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpCliente> query =  
	
	            (from entity in this.DbContext.CLIENTE.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpCliente()		
	            {
	            
                IdCliente = entity.ID_CLIENTE
	            });

	            
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpLoja.
	    public IQueryable<LookUpLoja> GetAllLookUpLoja()
	    {
	        return this.GetLookUpLoja(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpLoja By EntitySearch.
	    public IQueryable<LookUpLoja> GetLookUpLojaByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpLoja(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpLoja.
	    public IQueryable<LookUpLoja> GetLookUpLoja(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "LOJA" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpLoja";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpLoja));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpLoja> query =  
	
	            (from entity in this.DbContext.LOJA.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpLoja()		
	            {
	            
                IdLoja = entity.ID_LOJA
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
	
		

	        if (entityName.InList("VAREJO.BV.Diversos.Venda"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "Venda",
	        			NameSpace = "VAREJO.BV.Diversos",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "Venda",
	        			ClearMethodName = "ClearVenda",
	        			QueryMethodName  = "GetPagedVenda",	
	        			CountingMethodName  = "GetVenda" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("VAREJO.BV.Diversos.Venda"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("VAREJO.BV.Diversos.Venda"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("VAREJO.BV.Diversos.Venda", "VAREJO.BV.Diversos.VendaItem"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "VendaItem" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "VAREJO.BV.Diversos",
	        			HasQuickSearch = false,
	        			ParentClassName = "Venda",	
	        			DisplayName = "VendaItem",
	        			ClearMethodName = "ClearVendaItem" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedVendaItem" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetVendaItem" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("VAREJO.BV.Diversos.VendaItem"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("VAREJO.BV.Diversos.VendaItem" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
	
            return result;
        }
	
	    [Ignore]
	    public string[] GetClientDomains(bool erp)
        {	
	    		if (erp)
	    		{

         		    return new string[] { "VAREJO_ClientErpDataDomainsFactory", Linx.Tools.AssemblyHelper.ReadResourceContent("VAREJO.BV.ClientResources.ClientErpDataDomainsFactory.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
	    		}
	    		else 
	    		{

         		    return new string[] { "VAREJO_MobileDataDomains", Linx.Tools.AssemblyHelper.ReadResourceContent("VAREJO.BV.ClientResources.MobileDataDomains.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
	    		}

        }

	    [Ignore]
	    public string[] GetClientService(bool erp)
        {	

	    		if (erp)
	    		{

         		    return new string[] { "VAREJO_DiversosClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("VAREJO.BV.ClientResources.DiversosClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "VAREJO_diversosService", Linx.Tools.AssemblyHelper.ReadResourceContent("VAREJO.BV.ClientResources.diversosService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
	    		}

        }

	    [Ignore]
	    public string[] GetClientFactory(string entityName, bool erp)
        {	

	    		if (erp)
	    		{

         		    return new string[] { };	
	    		}
	    		else 
	    		{

         		    return new string[] { };	
	    		}

        }

	    [Ignore]
	    public string[] GetClientFactoryCustomEvents(string entityName, bool erp)
        {	

	    		if (erp)
	    		{

         		    return new string[] { };	
	    		}
	    		else 
	    		{

         		    return new string[] { };	
	    		}

        }
	
	    #endregion Get Meta Data.
	
	    #region Clear Methods Definitions.
	
		
	
	    [Ignore]
	    //Clear Venda.
	    public IEnumerable<Venda> ClearVenda()
	    {
	        List<Venda> result = new List<Venda>();
	        result.Add(new Venda());	
			
	        result[0].VendaItemList = new List<VendaItem>();
	        ((List<VendaItem>)result[0].VendaItemList).Add(new VendaItem());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear VendaItem.
	    public IEnumerable<VendaItem> ClearVendaItem()
	    {
	        List<VendaItem> result = new List<VendaItem>();
	        result.Add(new VendaItem());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    [VendaQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get Venda.
	    public IQueryable<Venda> GetVenda()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVenda")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<Venda> result = 
	            (from entity0 in this.DbContext.VENDA
                  let entity0Al2 = entity0.LOJA
                  let entity0Al1 = entity0.CLIENTE
	            
	            	
	            select new Venda()		
	            {
	            
                BigIntVenda = entity0.BIG_INT_VENDA
                , BitVenda = entity0.BIT_VENDA
                , ComboboxVenda = entity0.COMBOBOX_VENDA
                , ComboboxVendaName = ((entity0.COMBOBOX_VENDA) == 1 ? "VENDA 1" : ((entity0.COMBOBOX_VENDA) == 2 ? "VENDA 2" : ((entity0.COMBOBOX_VENDA) == 3 ? "VENDA 3" : "")))
                , DatetimeVenda = entity0.DATETIME_VENDA
                , DecimalVenda = entity0.DECIMAL_VENDA
                , GuidVenda = entity0.GUID_VENDA
                , IdCliente = entity0Al1.ID_CLIENTE
                , IdLoja = entity0Al2.ID_LOJA
                , IdVenda = entity0.ID_VENDA
                , IntVenda = entity0.INT_VENDA
                , SmallIntVenda = entity0.SMALL_INT_VENDA
                , StringVenda = entity0.STRING_VENDA
			
                ,VendaItemList = 
	                        (from entity1 in entity0.VENDA_ITEM_LISTA
                                  let entity1Al1 = entity1.VENDA
	                        
	                        	
	                        select new VendaItem()
	                        {
	                        
                                BigIntVendaItem = entity1.BIG_INT_VENDA_ITEM
                                , BitVendaItem = entity1.BIT_VENDA_ITEM
                                , ComboboxVendaItem = entity1.COMBOBOX_VENDA_ITEM
                                , ComboboxVendaItemName = ((entity1.COMBOBOX_VENDA_ITEM) == 1 ? "VENDA ITEM 1" : ((entity1.COMBOBOX_VENDA_ITEM) == 2 ? "VENDA ITEM 2" : ((entity1.COMBOBOX_VENDA_ITEM) == 3 ? "VENDA ITEM 3" : "")))
                                , DatetimeVendaItem = entity1.DATETIME_VENDA_ITEM
                                , DecimalVendaItem = entity1.DECIMAL_VENDA_ITEM
                                , GuidVendaItem = entity1.GUID_VENDA_ITEM
                                , IdVenda = entity1Al1.ID_VENDA
                                , IdVendaItem = entity1.ID_VENDA_ITEM
                                , IntVendaItem = entity1.INT_VENDA_ITEM
                                , SmallIntVendaItem = entity1.SMALL_INT_VENDA_ITEM
                                , StringVendaItem = entity1.STRING_VENDA_ITEM
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendaItemQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendaItem.
	    public IQueryable<VendaItem> GetVendaItem()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendaItem")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaItemQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<VendaItem> result = 
	            (from entity0 in this.DbContext.VENDA_ITEM
                  let entity0Al1 = entity0.VENDA
	            
	            	
	            select new VendaItem()		
	            {
	            
                BigIntVendaItem = entity0.BIG_INT_VENDA_ITEM
                , BitVendaItem = entity0.BIT_VENDA_ITEM
                , ComboboxVendaItem = entity0.COMBOBOX_VENDA_ITEM
                , ComboboxVendaItemName = ((entity0.COMBOBOX_VENDA_ITEM) == 1 ? "VENDA ITEM 1" : ((entity0.COMBOBOX_VENDA_ITEM) == 2 ? "VENDA ITEM 2" : ((entity0.COMBOBOX_VENDA_ITEM) == 3 ? "VENDA ITEM 3" : "")))
                , DatetimeVendaItem = entity0.DATETIME_VENDA_ITEM
                , DecimalVendaItem = entity0.DECIMAL_VENDA_ITEM
                , GuidVendaItem = entity0.GUID_VENDA_ITEM
                , IdVenda = entity0Al1.ID_VENDA
                , IdVendaItem = entity0.ID_VENDA_ITEM
                , IntVendaItem = entity0.INT_VENDA_ITEM
                , SmallIntVendaItem = entity0.SMALL_INT_VENDA_ITEM
                , StringVendaItem = entity0.STRING_VENDA_ITEM
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendaQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendaNoAssociations.
	    public IQueryable<Venda> GetVendaNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendaNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<Venda> result = 
	            (from entity0 in this.DbContext.VENDA
                  let entity0Al2 = entity0.LOJA
                  let entity0Al1 = entity0.CLIENTE
	            
	            	
	            select new Venda()		
	            {
	            
                BigIntVenda = entity0.BIG_INT_VENDA
                , BitVenda = entity0.BIT_VENDA
                , ComboboxVenda = entity0.COMBOBOX_VENDA
                , ComboboxVendaName = ((entity0.COMBOBOX_VENDA) == 1 ? "VENDA 1" : ((entity0.COMBOBOX_VENDA) == 2 ? "VENDA 2" : ((entity0.COMBOBOX_VENDA) == 3 ? "VENDA 3" : "")))
                , DatetimeVenda = entity0.DATETIME_VENDA
                , DecimalVenda = entity0.DECIMAL_VENDA
                , GuidVenda = entity0.GUID_VENDA
                , IdCliente = entity0Al1.ID_CLIENTE
                , IdLoja = entity0Al2.ID_LOJA
                , IdVenda = entity0.ID_VENDA
                , IntVenda = entity0.INT_VENDA
                , SmallIntVenda = entity0.SMALL_INT_VENDA
                , StringVenda = entity0.STRING_VENDA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendaItemQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendaItemNoAssociations.
	    public IQueryable<VendaItem> GetVendaItemNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendaItemNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaItemQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<VendaItem> result = 
	            (from entity0 in this.DbContext.VENDA_ITEM
                  let entity0Al1 = entity0.VENDA
	            
	            	
	            select new VendaItem()		
	            {
	            
                BigIntVendaItem = entity0.BIG_INT_VENDA_ITEM
                , BitVendaItem = entity0.BIT_VENDA_ITEM
                , ComboboxVendaItem = entity0.COMBOBOX_VENDA_ITEM
                , ComboboxVendaItemName = ((entity0.COMBOBOX_VENDA_ITEM) == 1 ? "VENDA ITEM 1" : ((entity0.COMBOBOX_VENDA_ITEM) == 2 ? "VENDA ITEM 2" : ((entity0.COMBOBOX_VENDA_ITEM) == 3 ? "VENDA ITEM 3" : "")))
                , DatetimeVendaItem = entity0.DATETIME_VENDA_ITEM
                , DecimalVendaItem = entity0.DECIMAL_VENDA_ITEM
                , GuidVendaItem = entity0.GUID_VENDA_ITEM
                , IdVenda = entity0Al1.ID_VENDA
                , IdVendaItem = entity0.ID_VENDA_ITEM
                , IntVendaItem = entity0.INT_VENDA_ITEM
                , SmallIntVendaItem = entity0.SMALL_INT_VENDA_ITEM
                , StringVendaItem = entity0.STRING_VENDA_ITEM
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	//Add filtering disabled property for VENDA
	    	string[] bmDisabledVendaList = this.GetEDM().GetFilteringDisabledList("VENDA");
	    	if (bmDisabledVendaList.Length > 0)
	    	{
	
	    		if (bmDisabledVendaList.Contains("VENDA.BIG_INT_VENDA"))
	    		{
	    			result.Add("Venda|BigIntVenda");
	    			result.Add("Venda|VENDA.BIG_INT_VENDA");
	    		}
	
	    		if (bmDisabledVendaList.Contains("VENDA.BIT_VENDA"))
	    		{
	    			result.Add("Venda|BitVenda");
	    			result.Add("Venda|VENDA.BIT_VENDA");
	    		}
	
	    		if (bmDisabledVendaList.Contains("VENDA.COMBOBOX_VENDA"))
	    		{
	    			result.Add("Venda|ComboboxVenda");
	    			result.Add("Venda|VENDA.COMBOBOX_VENDA");
	    		}
	
	    		if (bmDisabledVendaList.Contains("VENDA.DATETIME_VENDA"))
	    		{
	    			result.Add("Venda|DatetimeVenda");
	    			result.Add("Venda|VENDA.DATETIME_VENDA");
	    		}
	
	    		if (bmDisabledVendaList.Contains("VENDA.DECIMAL_VENDA"))
	    		{
	    			result.Add("Venda|DecimalVenda");
	    			result.Add("Venda|VENDA.DECIMAL_VENDA");
	    		}
	
	    		if (bmDisabledVendaList.Contains("VENDA.GUID_VENDA"))
	    		{
	    			result.Add("Venda|GuidVenda");
	    			result.Add("Venda|VENDA.GUID_VENDA");
	    		}
	
	    		if (bmDisabledVendaList.Contains("VENDA.ID_VENDA"))
	    		{
	    			result.Add("Venda|IdVenda");
	    			result.Add("Venda|VENDA.ID_VENDA");
	    		}
	
	    		if (bmDisabledVendaList.Contains("VENDA.INT_VENDA"))
	    		{
	    			result.Add("Venda|IntVenda");
	    			result.Add("Venda|VENDA.INT_VENDA");
	    		}
	
	    		if (bmDisabledVendaList.Contains("VENDA.SMALL_INT_VENDA"))
	    		{
	    			result.Add("Venda|SmallIntVenda");
	    			result.Add("Venda|VENDA.SMALL_INT_VENDA");
	    		}
	
	    		if (bmDisabledVendaList.Contains("VENDA.STRING_VENDA"))
	    		{
	    			result.Add("Venda|StringVenda");
	    			result.Add("Venda|VENDA.STRING_VENDA");
	    		}
	    	}
	    	//Add filtering disabled property for VENDA_ITEM
	    	string[] bmDisabledVendaItemList = this.GetEDM().GetFilteringDisabledList("VENDA_ITEM");
	    	if (bmDisabledVendaItemList.Length > 0)
	    	{
	
	    		if (bmDisabledVendaItemList.Contains("VENDA_ITEM.BIG_INT_VENDA_ITEM"))
	    		{
	    			result.Add("VendaItem|BigIntVendaItem");
	    			result.Add("VendaItem|VENDA_ITEM.BIG_INT_VENDA_ITEM");
	    		}
	
	    		if (bmDisabledVendaItemList.Contains("VENDA_ITEM.BIT_VENDA_ITEM"))
	    		{
	    			result.Add("VendaItem|BitVendaItem");
	    			result.Add("VendaItem|VENDA_ITEM.BIT_VENDA_ITEM");
	    		}
	
	    		if (bmDisabledVendaItemList.Contains("VENDA_ITEM.COMBOBOX_VENDA_ITEM"))
	    		{
	    			result.Add("VendaItem|ComboboxVendaItem");
	    			result.Add("VendaItem|VENDA_ITEM.COMBOBOX_VENDA_ITEM");
	    		}
	
	    		if (bmDisabledVendaItemList.Contains("VENDA_ITEM.DATETIME_VENDA_ITEM"))
	    		{
	    			result.Add("VendaItem|DatetimeVendaItem");
	    			result.Add("VendaItem|VENDA_ITEM.DATETIME_VENDA_ITEM");
	    		}
	
	    		if (bmDisabledVendaItemList.Contains("VENDA_ITEM.DECIMAL_VENDA_ITEM"))
	    		{
	    			result.Add("VendaItem|DecimalVendaItem");
	    			result.Add("VendaItem|VENDA_ITEM.DECIMAL_VENDA_ITEM");
	    		}
	
	    		if (bmDisabledVendaItemList.Contains("VENDA_ITEM.GUID_VENDA_ITEM"))
	    		{
	    			result.Add("VendaItem|GuidVendaItem");
	    			result.Add("VendaItem|VENDA_ITEM.GUID_VENDA_ITEM");
	    		}
	
	    		if (bmDisabledVendaItemList.Contains("VENDA_ITEM.ID_VENDA_ITEM"))
	    		{
	    			result.Add("VendaItem|IdVendaItem");
	    			result.Add("VendaItem|VENDA_ITEM.ID_VENDA_ITEM");
	    		}
	
	    		if (bmDisabledVendaItemList.Contains("VENDA_ITEM.INT_VENDA_ITEM"))
	    		{
	    			result.Add("VendaItem|IntVendaItem");
	    			result.Add("VendaItem|VENDA_ITEM.INT_VENDA_ITEM");
	    		}
	
	    		if (bmDisabledVendaItemList.Contains("VENDA_ITEM.SMALL_INT_VENDA_ITEM"))
	    		{
	    			result.Add("VendaItem|SmallIntVendaItem");
	    			result.Add("VendaItem|VENDA_ITEM.SMALL_INT_VENDA_ITEM");
	    		}
	
	    		if (bmDisabledVendaItemList.Contains("VENDA_ITEM.STRING_VENDA_ITEM"))
	    		{
	    			result.Add("VendaItem|StringVendaItem");
	    			result.Add("VendaItem|VENDA_ITEM.STRING_VENDA_ITEM");
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
	    //Get Venda By EntitySearchId.
	    public IQueryable<Venda> GetVendaByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetVendaByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get VendaItem By EntitySearchId.
	    public IQueryable<VendaItem> GetVendaItemByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetVendaItemByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get Venda By EntitySearchId.
	    public IQueryable<Venda> GetVendaByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetVendaByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get VendaItem By EntitySearchId.
	    public IQueryable<VendaItem> GetVendaItemByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetVendaItemByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get Venda By Example.
	    [Ignore]
	    public IQueryable<Venda> GetVendaByExample(Venda entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetVendaByEntitySearch(queryAnalysis);
	    }
			
	    //Get VendaItem By Example.
	    [Ignore]
	    public IQueryable<VendaItem> GetVendaItemByExample(VendaItem entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetVendaItemByEntitySearch(queryAnalysis);
	    }
			
	    //Get Venda By Example.
	    [Ignore]
	    public IQueryable<Venda> GetVendaByExampleNoAssociations(Venda entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetVendaByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get VendaItem By Example.
	    [Ignore]
	    public IQueryable<VendaItem> GetVendaItemByExampleNoAssociations(VendaItem entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetVendaItemByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public Venda GetVendaByKey(Int32 idVenda)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("Venda");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdVenda"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idVenda));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetVendaByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public VendaItem GetVendaItemByKey(Int32 idVendaItem)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("VendaItem");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdVendaItem"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idVendaItem));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetVendaItemByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    [VendaQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendaByEntitySearch.
	    public IQueryable<Venda> GetVendaByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendaByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Venda));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<Venda> result = 
	            (from entity0 in this.DbContext.VENDA.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.LOJA
                  let entity0Al1 = entity0.CLIENTE
	            
	            	
	            select new Venda()		
	            {
	            
                BigIntVenda = entity0.BIG_INT_VENDA
                , BitVenda = entity0.BIT_VENDA
                , ComboboxVenda = entity0.COMBOBOX_VENDA
                , ComboboxVendaName = ((entity0.COMBOBOX_VENDA) == 1 ? "VENDA 1" : ((entity0.COMBOBOX_VENDA) == 2 ? "VENDA 2" : ((entity0.COMBOBOX_VENDA) == 3 ? "VENDA 3" : "")))
                , DatetimeVenda = entity0.DATETIME_VENDA
                , DecimalVenda = entity0.DECIMAL_VENDA
                , GuidVenda = entity0.GUID_VENDA
                , IdCliente = entity0Al1.ID_CLIENTE
                , IdLoja = entity0Al2.ID_LOJA
                , IdVenda = entity0.ID_VENDA
                , IntVenda = entity0.INT_VENDA
                , SmallIntVenda = entity0.SMALL_INT_VENDA
                , StringVenda = entity0.STRING_VENDA
			
                ,VendaItemList = 
	                        (from entity1 in entity0.VENDA_ITEM_LISTA
                                  let entity1Al1 = entity1.VENDA
	                        
	                        	
	                        select new VendaItem()
	                        {
	                        
                                BigIntVendaItem = entity1.BIG_INT_VENDA_ITEM
                                , BitVendaItem = entity1.BIT_VENDA_ITEM
                                , ComboboxVendaItem = entity1.COMBOBOX_VENDA_ITEM
                                , ComboboxVendaItemName = ((entity1.COMBOBOX_VENDA_ITEM) == 1 ? "VENDA ITEM 1" : ((entity1.COMBOBOX_VENDA_ITEM) == 2 ? "VENDA ITEM 2" : ((entity1.COMBOBOX_VENDA_ITEM) == 3 ? "VENDA ITEM 3" : "")))
                                , DatetimeVendaItem = entity1.DATETIME_VENDA_ITEM
                                , DecimalVendaItem = entity1.DECIMAL_VENDA_ITEM
                                , GuidVendaItem = entity1.GUID_VENDA_ITEM
                                , IdVenda = entity1Al1.ID_VENDA
                                , IdVendaItem = entity1.ID_VENDA_ITEM
                                , IntVendaItem = entity1.INT_VENDA_ITEM
                                , SmallIntVendaItem = entity1.SMALL_INT_VENDA_ITEM
                                , StringVendaItem = entity1.STRING_VENDA_ITEM
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendaItemQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendaItemByEntitySearch.
	    public IQueryable<VendaItem> GetVendaItemByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendaItemByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaItemQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(VendaItem));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<VendaItem> result = 
	            (from entity0 in this.DbContext.VENDA_ITEM.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.VENDA
	            
	            	
	            select new VendaItem()		
	            {
	            
                BigIntVendaItem = entity0.BIG_INT_VENDA_ITEM
                , BitVendaItem = entity0.BIT_VENDA_ITEM
                , ComboboxVendaItem = entity0.COMBOBOX_VENDA_ITEM
                , ComboboxVendaItemName = ((entity0.COMBOBOX_VENDA_ITEM) == 1 ? "VENDA ITEM 1" : ((entity0.COMBOBOX_VENDA_ITEM) == 2 ? "VENDA ITEM 2" : ((entity0.COMBOBOX_VENDA_ITEM) == 3 ? "VENDA ITEM 3" : "")))
                , DatetimeVendaItem = entity0.DATETIME_VENDA_ITEM
                , DecimalVendaItem = entity0.DECIMAL_VENDA_ITEM
                , GuidVendaItem = entity0.GUID_VENDA_ITEM
                , IdVenda = entity0Al1.ID_VENDA
                , IdVendaItem = entity0.ID_VENDA_ITEM
                , IntVendaItem = entity0.INT_VENDA_ITEM
                , SmallIntVendaItem = entity0.SMALL_INT_VENDA_ITEM
                , StringVendaItem = entity0.STRING_VENDA_ITEM
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendaQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendaByEntitySearchNoAssociations.
	    public IQueryable<Venda> GetVendaByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendaByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Venda));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<Venda> result = 
	            (from entity0 in this.DbContext.VENDA.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.LOJA
                  let entity0Al1 = entity0.CLIENTE
	            
	            	
	            select new Venda()		
	            {
	            
                BigIntVenda = entity0.BIG_INT_VENDA
                , BitVenda = entity0.BIT_VENDA
                , ComboboxVenda = entity0.COMBOBOX_VENDA
                , ComboboxVendaName = ((entity0.COMBOBOX_VENDA) == 1 ? "VENDA 1" : ((entity0.COMBOBOX_VENDA) == 2 ? "VENDA 2" : ((entity0.COMBOBOX_VENDA) == 3 ? "VENDA 3" : "")))
                , DatetimeVenda = entity0.DATETIME_VENDA
                , DecimalVenda = entity0.DECIMAL_VENDA
                , GuidVenda = entity0.GUID_VENDA
                , IdCliente = entity0Al1.ID_CLIENTE
                , IdLoja = entity0Al2.ID_LOJA
                , IdVenda = entity0.ID_VENDA
                , IntVenda = entity0.INT_VENDA
                , SmallIntVenda = entity0.SMALL_INT_VENDA
                , StringVenda = entity0.STRING_VENDA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendaItemQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendaItemByEntitySearchNoAssociations.
	    public IQueryable<VendaItem> GetVendaItemByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendaItemByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaItemQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(VendaItem));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<VendaItem> result = 
	            (from entity0 in this.DbContext.VENDA_ITEM.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.VENDA
	            
	            	
	            select new VendaItem()		
	            {
	            
                BigIntVendaItem = entity0.BIG_INT_VENDA_ITEM
                , BitVendaItem = entity0.BIT_VENDA_ITEM
                , ComboboxVendaItem = entity0.COMBOBOX_VENDA_ITEM
                , ComboboxVendaItemName = ((entity0.COMBOBOX_VENDA_ITEM) == 1 ? "VENDA ITEM 1" : ((entity0.COMBOBOX_VENDA_ITEM) == 2 ? "VENDA ITEM 2" : ((entity0.COMBOBOX_VENDA_ITEM) == 3 ? "VENDA ITEM 3" : "")))
                , DatetimeVendaItem = entity0.DATETIME_VENDA_ITEM
                , DecimalVendaItem = entity0.DECIMAL_VENDA_ITEM
                , GuidVendaItem = entity0.GUID_VENDA_ITEM
                , IdVenda = entity0Al1.ID_VENDA
                , IdVendaItem = entity0.ID_VENDA_ITEM
                , IntVendaItem = entity0.INT_VENDA_ITEM
                , SmallIntVendaItem = entity0.SMALL_INT_VENDA_ITEM
                , StringVendaItem = entity0.STRING_VENDA_ITEM
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendaItemQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendaItemParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<VendaItemParentComposition> GetVendaItemParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendaItemParentCompositionByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaItemQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "VENDA", "VENDA_ITEM", "VENDA", typeof(VendaItemParentComposition));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<VendaItemParentComposition> result = 
	            (from entity0 in this.DbContext.VENDA_ITEM.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.VENDA
	            
	            	
	            select new VendaItemParentComposition()		
	            {
	            
                BigIntVendaItem = entity0.BIG_INT_VENDA_ITEM
                , BitVendaItem = entity0.BIT_VENDA_ITEM
                , ComboboxVendaItem = entity0.COMBOBOX_VENDA_ITEM
                , ComboboxVendaItemName = ((entity0.COMBOBOX_VENDA_ITEM) == 1 ? "VENDA ITEM 1" : ((entity0.COMBOBOX_VENDA_ITEM) == 2 ? "VENDA ITEM 2" : ((entity0.COMBOBOX_VENDA_ITEM) == 3 ? "VENDA ITEM 3" : "")))
                , DatetimeVendaItem = entity0.DATETIME_VENDA_ITEM
                , DecimalVendaItem = entity0.DECIMAL_VENDA_ITEM
                , GuidVendaItem = entity0.GUID_VENDA_ITEM
                , IdVenda = entity0Al1.ID_VENDA
                , IdVendaItem = entity0.ID_VENDA_ITEM
                , IntVendaItem = entity0.INT_VENDA_ITEM
                , SmallIntVendaItem = entity0.SMALL_INT_VENDA_ITEM
                , StringVendaItem = entity0.STRING_VENDA_ITEM
                //Venda Properties.
                , BigIntVenda = entity0.VENDA.BIG_INT_VENDA
                , BitVenda = entity0.VENDA.BIT_VENDA
                , ComboboxVenda = entity0.VENDA.COMBOBOX_VENDA
                , ComboboxVendaName = ((entity0.VENDA.COMBOBOX_VENDA) == 1 ? "VENDA 1" : ((entity0.VENDA.COMBOBOX_VENDA) == 2 ? "VENDA 2" : ((entity0.VENDA.COMBOBOX_VENDA) == 3 ? "VENDA 3" : "")))
                , DatetimeVenda = entity0.VENDA.DATETIME_VENDA
                , DecimalVenda = entity0.VENDA.DECIMAL_VENDA
                , GuidVenda = entity0.VENDA.GUID_VENDA
                , IdCliente = entity0.VENDA.CLIENTE.ID_CLIENTE
                , IdLoja = entity0.VENDA.LOJA.ID_LOJA
                , IntVenda = entity0.VENDA.INT_VENDA
                , SmallIntVenda = entity0.VENDA.SMALL_INT_VENDA
                , StringVenda = entity0.VENDA.STRING_VENDA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    [VendaQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedVenda.
	    public IQueryable<Venda> GetPagedVenda(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedVenda")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Venda));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<Venda> result = 
	            (from entity0 in this.DbContext.VENDA.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.LOJA
                  let entity0Al1 = entity0.CLIENTE
                orderby entity0.ID_VENDA ascending
	            
	            	
	            select new Venda()		
	            {
	            
                BigIntVenda = entity0.BIG_INT_VENDA
                , BitVenda = entity0.BIT_VENDA
                , ComboboxVenda = entity0.COMBOBOX_VENDA
                , ComboboxVendaName = ((entity0.COMBOBOX_VENDA) == 1 ? "VENDA 1" : ((entity0.COMBOBOX_VENDA) == 2 ? "VENDA 2" : ((entity0.COMBOBOX_VENDA) == 3 ? "VENDA 3" : "")))
                , DatetimeVenda = entity0.DATETIME_VENDA
                , DecimalVenda = entity0.DECIMAL_VENDA
                , GuidVenda = entity0.GUID_VENDA
                , IdCliente = entity0Al1.ID_CLIENTE
                , IdLoja = entity0Al2.ID_LOJA
                , IdVenda = entity0.ID_VENDA
                , IntVenda = entity0.INT_VENDA
                , SmallIntVenda = entity0.SMALL_INT_VENDA
                , StringVenda = entity0.STRING_VENDA
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendaItemQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedVendaItem.
	    public IQueryable<VendaItem> GetPagedVendaItem(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedVendaItem")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaItemQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(VendaItem));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<VendaItem> result = 
	            (from entity0 in this.DbContext.VENDA_ITEM.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.VENDA
                orderby entity0.ID_VENDA_ITEM ascending
	            
	            	
	            select new VendaItem()		
	            {
	            
                BigIntVendaItem = entity0.BIG_INT_VENDA_ITEM
                , BitVendaItem = entity0.BIT_VENDA_ITEM
                , ComboboxVendaItem = entity0.COMBOBOX_VENDA_ITEM
                , ComboboxVendaItemName = ((entity0.COMBOBOX_VENDA_ITEM) == 1 ? "VENDA ITEM 1" : ((entity0.COMBOBOX_VENDA_ITEM) == 2 ? "VENDA ITEM 2" : ((entity0.COMBOBOX_VENDA_ITEM) == 3 ? "VENDA ITEM 3" : "")))
                , DatetimeVendaItem = entity0.DATETIME_VENDA_ITEM
                , DecimalVendaItem = entity0.DECIMAL_VENDA_ITEM
                , GuidVendaItem = entity0.GUID_VENDA_ITEM
                , IdVenda = entity0Al1.ID_VENDA
                , IdVendaItem = entity0.ID_VENDA_ITEM
                , IntVendaItem = entity0.INT_VENDA_ITEM
                , SmallIntVendaItem = entity0.SMALL_INT_VENDA_ITEM
                , StringVendaItem = entity0.STRING_VENDA_ITEM
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetVendaCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Venda));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.VENDA.Where(dynQuery, parameters.ToArray())
                  let entityAl2 = entity.LOJA
                  let entityAl1 = entity.CLIENTE
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetVendaItemCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(VendaItem));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.VENDA_ITEM.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.VENDA
	            
	            select 1
	            ).Count();	
		
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    [VendaUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update Venda.
	    public void UpdateVenda(Venda entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateVenda")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    [VendaInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert Venda.
	    public void InsertVenda(Venda entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertVenda")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    [VendaDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete Venda.
	    public void DeleteVenda(Venda entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteVenda")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    [VendaItemUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update VendaItem.
	    public void UpdateVendaItem(VendaItem entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateVendaItem")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaItemUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.Venda.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.Venda) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.Venda); 	
	            

	
	        }
	
	    }

	    [VendaItemInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert VendaItem.
	    public void InsertVendaItem(VendaItem entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertVendaItem")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaItemInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.Venda.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.Venda) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.Venda);
	            

	
	        }
	
	    }

	    [VendaItemDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete VendaItem.
	    public void DeleteVendaItem(VendaItem entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteVendaItem")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaItemDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.Venda.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.Venda) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.Venda);
	            

	
	        }

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}