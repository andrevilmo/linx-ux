					
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

namespace VAREJO.BV.WizardExample
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="CLIENTE.ID_CLIENTE", IsUpdatable=false, EdmName="Linx.Demo.BM.DCLinxDemoBM")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[Cliente,Cliente.Venda,Cliente.VendaAtacado];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdCliente];ReadOnly[false];Entities[CLIENTE:IdCliente|ESTADO:IdEstado];SubQueryInfo[];EdmEntityName[CLIENTE];EntityRelations[ESTADO(ESTADO)#PAIS(PAIS)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "Cliente")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "VAREJO.BV.WizardExample.Cliente")]
	public partial class Cliente : Linx.Data.Entity
	{

	
		
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.VendaList != null && this.VendaList.Count() > 0)
	      {
	         foreach (var entity in this.VendaList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      if (this.VendaAtacadoList != null && this.VendaAtacadoList.Count() > 0)
	      {
	         foreach (var entity in this.VendaAtacadoList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.VendaList != null)
	      {
	         foreach (var detail in this.VendaList)
	         {
	            detail.ResetDetails();
	         }
	         this.VendaList = null;
	      }
	      if (this.VendaAtacadoList != null)
	      {
	         foreach (var detail in this.VendaAtacadoList)
	         {
	            detail.ResetDetails();
	         }
	         this.VendaAtacadoList = null;
	      }
	    }

	    public virtual void ResetChangeState()
	    {
	      this.ChangeState = "N";
	      if (this.VendaList != null)
	      {
	         foreach (var detail in this.VendaList.ToArray())
	         {
	            detail.ResetChangeState();
	         }
	      }
	      if (this.VendaAtacadoList != null)
	      {
	         foreach (var detail in this.VendaAtacadoList.ToArray())
	         {
	            detail.ResetChangeState();
	         }
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(WizardExampleDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("Venda"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("Venda");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdCliente"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdCliente));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load Venda and all sub-details
	         if (this.VendaList == null || this.VendaList.Count() == 0)
	         {
	             if (take > 0)
	                 this.VendaList = context.GetPagedVenda(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.VendaList = (from r in context.GetVendaByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	      if (viewNames == null || viewNames.Contains("VendaAtacado"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("VendaAtacado");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdCliente"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdCliente));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load VendaAtacado and all sub-details
	         if (this.VendaAtacadoList == null || this.VendaAtacadoList.Count() == 0)
	         {
	             if (take > 0)
	                 this.VendaAtacadoList = context.GetPagedVendaAtacado(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.VendaAtacadoList = (from r in context.GetVendaAtacadoByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _VendaElements = changeSet.ChangeSetEntries.Where(e => e.Entity is Venda && ((Venda)e.Entity).Cliente == null && e.Associations == null && e.OriginalAssociations == null && ((Venda)e.Entity).IdCliente == this.IdCliente).ToList();
 	      if (_VendaElements.Count > 0 && this.VendaList.Count() == 0)
 	      {
 	          this.VendaList = _VendaElements.Select(e => (Venda)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _VendaElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((Venda)detail.Entity).Cliente = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("Cliente", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("VendaList", indexDetails.ToArray());
 	      }
 
 	      var _VendaAtacadoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is VendaAtacado && ((VendaAtacado)e.Entity).Cliente == null && e.Associations == null && e.OriginalAssociations == null && ((VendaAtacado)e.Entity).IdCliente == this.IdCliente).ToList();
 	      if (_VendaAtacadoElements.Count > 0 && this.VendaAtacadoList.Count() == 0)
 	      {
 	          this.VendaAtacadoList = _VendaAtacadoElements.Select(e => (VendaAtacado)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _VendaAtacadoElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((VendaAtacado)detail.Entity).Cliente = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("Cliente", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("VendaAtacadoList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For BigIntCliente
	    partial void OnBigIntClienteChanging(System.Nullable<System.Int64> value);
	    partial void OnBigIntClienteChanged();

	    private System.Nullable<System.Int64> _BigIntCliente;

	    [DataMember(Name = "BigIntCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Big Int Cliente", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[CLIENTE.BIG_INT_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.BIG_INT_CLIENTE")]
	    public System.Nullable<System.Int64> BigIntCliente
	    {
	    	    get
	    	    {
	    	          return _BigIntCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._BigIntCliente != value)
	    	          {
	    	              this.ValidateProperty("BigIntCliente", value);
	    	              this.OnBigIntClienteChanging(value);
	    	              this.RaiseDataMemberChanging("BigIntCliente");
	    	              this._BigIntCliente = value;
	    	              this.RaiseDataMemberChanged("BigIntCliente");
	    	              this.OnBigIntClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For BitCliente
	    partial void OnBitClienteChanging(System.Nullable<System.Boolean> value);
	    partial void OnBitClienteChanged();

	    private System.Nullable<System.Boolean> _BitCliente;

	    [DataMember(Name = "BitCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bit Cliente", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[CLIENTE.BIT_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.BIT_CLIENTE")]
	    public System.Nullable<System.Boolean> BitCliente
	    {
	    	    get
	    	    {
	    	          return _BitCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._BitCliente != value)
	    	          {
	    	              this.ValidateProperty("BitCliente", value);
	    	              this.OnBitClienteChanging(value);
	    	              this.RaiseDataMemberChanging("BitCliente");
	    	              this._BitCliente = value;
	    	              this.RaiseDataMemberChanged("BitCliente");
	    	              this.OnBitClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ComboboxCliente
	    partial void OnComboboxClienteChanging(Byte value);
	    partial void OnComboboxClienteChanged();

	    private Byte _ComboboxCliente;

	    [DataMember(IsRequired = true, Name = "ComboboxCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Cliente", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_CLIENTE];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[CLIENTE.COMBOBOX_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.COMBOBOX_CLIENTE")]
	    public Byte ComboboxCliente
	    {
	    	    get
	    	    {
	    	          return _ComboboxCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._ComboboxCliente != value)
	    	          {
	    	              this.ValidateProperty("ComboboxCliente", value);
	    	              this.OnComboboxClienteChanging(value);
	    	              this.RaiseDataMemberChanging("ComboboxCliente");
	    	              this._ComboboxCliente = value;
	    	              this.RaiseDataMemberChanged("ComboboxCliente");
	    	              this.OnComboboxClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DatetimeCliente
	    partial void OnDatetimeClienteChanging(System.Nullable<System.DateTime> value);
	    partial void OnDatetimeClienteChanged();

	    private System.Nullable<System.DateTime> _DatetimeCliente;

	    [DataMember(Name = "DatetimeCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Datetime Cliente", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[CLIENTE.DATETIME_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.DATETIME_CLIENTE")]
	    public System.Nullable<System.DateTime> DatetimeCliente
	    {
	    	    get
	    	    {
	    	          return _DatetimeCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._DatetimeCliente != value)
	    	          {
	    	              this.ValidateProperty("DatetimeCliente", value);
	    	              this.OnDatetimeClienteChanging(value);
	    	              this.RaiseDataMemberChanging("DatetimeCliente");
	    	              this._DatetimeCliente = value;
	    	              this.RaiseDataMemberChanged("DatetimeCliente");
	    	              this.OnDatetimeClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DecimalCliente
	    partial void OnDecimalClienteChanging(System.Nullable<System.Decimal> value);
	    partial void OnDecimalClienteChanged();

	    private System.Nullable<System.Decimal> _DecimalCliente;

	    [DataMember(Name = "DecimalCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal Cliente", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[CLIENTE.DECIMAL_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.DECIMAL_CLIENTE")]
	    public System.Nullable<System.Decimal> DecimalCliente
	    {
	    	    get
	    	    {
	    	          return _DecimalCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._DecimalCliente != value)
	    	          {
	    	              this.ValidateProperty("DecimalCliente", value);
	    	              this.OnDecimalClienteChanging(value);
	    	              this.RaiseDataMemberChanging("DecimalCliente");
	    	              this._DecimalCliente = value;
	    	              this.RaiseDataMemberChanged("DecimalCliente");
	    	              this.OnDecimalClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For GuidCliente
	    partial void OnGuidClienteChanging(System.Nullable<System.Guid> value);
	    partial void OnGuidClienteChanged();

	    private System.Nullable<System.Guid> _GuidCliente;

	    [DataMember(Name = "GuidCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Guid Cliente", Description="", Order = 6, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[CLIENTE.GUID_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.GUID_CLIENTE")]
	    public System.Nullable<System.Guid> GuidCliente
	    {
	    	    get
	    	    {
	    	          return _GuidCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._GuidCliente != value)
	    	          {
	    	              this.ValidateProperty("GuidCliente", value);
	    	              this.OnGuidClienteChanging(value);
	    	              this.RaiseDataMemberChanging("GuidCliente");
	    	              this._GuidCliente = value;
	    	              this.RaiseDataMemberChanged("GuidCliente");
	    	              this.OnGuidClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdCliente
	    partial void OnIdClienteChanging(Int32 value);
	    partial void OnIdClienteChanged();

	    private Int32 _IdCliente;

	    [DataMember(IsRequired = true, Name = "IdCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Cliente", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[CLIENTE.ID_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.ID_CLIENTE")]
	    public Int32 IdCliente
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
	    //Extensibility Partial Method Definitions For IdEstado
	    partial void OnIdEstadoChanging(System.Nullable<Int32> value);
	    partial void OnIdEstadoChanged();

	    private System.Nullable<Int32> _IdEstado;

	    [DataMember(Name = "IdEstado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Estado", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpEstado];LookUpTitle[Seleção de (Id Estado)];LookUpQuery[executeLookUpEstado];LookUpFinalize[finalizeLookUpEstado];LookUpDisplayColumns[{\"IdEstado\" : \"Id Estado\"}];LookUpColumns[{\"IdEstado\" : true}];FilterDataKey[CLIENTE.ESTADO.ID_ESTADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<Int32>#IdEstado#true##12:0##Id Estado#0#true##::LookUpEstado##false#false#ESTADO#ESTADO#VAREJO.BV.WizardExample#IQueryable###true#false", EdmKey="CLIENTE.ESTADO.ID_ESTADO")]
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
	    	              this.ValidateProperty("IdEstado", value);
	    	              this.OnIdEstadoChanging(value);
	    	              this.RaiseDataMemberChanging("IdEstado");
	    	              this._IdEstado = value;
	    	              this.RaiseDataMemberChanged("IdEstado");
	    	              this.OnIdEstadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IntCliente
	    partial void OnIntClienteChanging(System.Nullable<System.Int32> value);
	    partial void OnIntClienteChanged();

	    private System.Nullable<System.Int32> _IntCliente;

	    [DataMember(Name = "IntCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int Cliente", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[CLIENTE.INT_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.INT_CLIENTE")]
	    public System.Nullable<System.Int32> IntCliente
	    {
	    	    get
	    	    {
	    	          return _IntCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._IntCliente != value)
	    	          {
	    	              this.ValidateProperty("IntCliente", value);
	    	              this.OnIntClienteChanging(value);
	    	              this.RaiseDataMemberChanging("IntCliente");
	    	              this._IntCliente = value;
	    	              this.RaiseDataMemberChanged("IntCliente");
	    	              this.OnIntClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For SmallIntCliente
	    partial void OnSmallIntClienteChanging(System.Nullable<System.Int16> value);
	    partial void OnSmallIntClienteChanged();

	    private System.Nullable<System.Int16> _SmallIntCliente;

	    [DataMember(Name = "SmallIntCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Small Int Cliente", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[CLIENTE.SMALL_INT_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.SMALL_INT_CLIENTE")]
	    public System.Nullable<System.Int16> SmallIntCliente
	    {
	    	    get
	    	    {
	    	          return _SmallIntCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._SmallIntCliente != value)
	    	          {
	    	              this.ValidateProperty("SmallIntCliente", value);
	    	              this.OnSmallIntClienteChanging(value);
	    	              this.RaiseDataMemberChanging("SmallIntCliente");
	    	              this._SmallIntCliente = value;
	    	              this.RaiseDataMemberChanged("SmallIntCliente");
	    	              this.OnSmallIntClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringCliente
	    partial void OnStringClienteChanging(System.String value);
	    partial void OnStringClienteChanged();

	    private System.String _StringCliente;

	    [DataMember(Name = "StringCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Cliente", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[CLIENTE.STRING_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.STRING_CLIENTE")]
	    public System.String StringCliente
	    {
	    	    get
	    	    {
	    	          return _StringCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringCliente != value)
	    	          {
	    	              this.ValidateProperty("StringCliente", value);
	    	              this.OnStringClienteChanging(value);
	    	              this.RaiseDataMemberChanging("StringCliente");
	    	              this._StringCliente = value;
	    	              this.RaiseDataMemberChanged("StringCliente");
	    	              this.OnStringClienteChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIdCliente;
	    [DataMember(Name = "TemporaryIdCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Cliente (Tmp)", Description="Temporary Key", Order = 7, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdCliente
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdCliente.IsNullOrEmpty())
	    	                this._TemporaryIdCliente = this._IdCliente;
	    	          return this._TemporaryIdCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdCliente != value)
	    	              this._TemporaryIdCliente = value;
	    	    }
	    }	

	    #endregion Data Properties

	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<Venda> _VendaList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_Cliente_Venda", "IdCliente", "IdCliente", IsForeignKey=false)]
	    [DataMember(Name = "VendaList", EmitDefaultValue = true)]
	    public IEnumerable<Venda> VendaList
	    {
	        get
	        {
	
	            if (this._VendaList == null)
	            	this._VendaList = new List<Venda>();
	
	            return this._VendaList;
	        }
	        set
	        {
	            if (this._VendaList != value)
	            {
	                this._VendaList = value;
	                this.RaisePropertyChanged("VendaList");
	            }
	        }
	    }	 
		
	    private IEnumerable<VendaAtacado> _VendaAtacadoList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_Cliente_VendaAtacado", "IdCliente", "IdCliente", IsForeignKey=false)]
	    [DataMember(Name = "VendaAtacadoList", EmitDefaultValue = true)]
	    public IEnumerable<VendaAtacado> VendaAtacadoList
	    {
	        get
	        {
	
	            if (this._VendaAtacadoList == null)
	            	this._VendaAtacadoList = new List<VendaAtacado>();
	
	            return this._VendaAtacadoList;
	        }
	        set
	        {
	            if (this._VendaAtacadoList != value)
	            {
	                this._VendaAtacadoList = value;
	                this.RaisePropertyChanged("VendaAtacadoList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "DCLinxDemoBM.CLIENTE").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Demo.BM.CLIENTE), QualifiedEntitySetName = "DCLinxDemoBM.CLIENTE" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.ID_CLIENTE", Source = "IdCliente", Target = "ID_CLIENTE", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.CLIENTE", RelationPropertyName = "CLIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.BIT_CLIENTE", Source = "BitCliente", Target = "BIT_CLIENTE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.CLIENTE", RelationPropertyName = "CLIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.INT_CLIENTE", Source = "IntCliente", Target = "INT_CLIENTE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.CLIENTE", RelationPropertyName = "CLIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.GUID_CLIENTE", Source = "GuidCliente", Target = "GUID_CLIENTE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.CLIENTE", RelationPropertyName = "CLIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.STRING_CLIENTE", Source = "StringCliente", Target = "STRING_CLIENTE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.CLIENTE", RelationPropertyName = "CLIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.BIG_INT_CLIENTE", Source = "BigIntCliente", Target = "BIG_INT_CLIENTE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.CLIENTE", RelationPropertyName = "CLIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.DECIMAL_CLIENTE", Source = "DecimalCliente", Target = "DECIMAL_CLIENTE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.CLIENTE", RelationPropertyName = "CLIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.COMBOBOX_CLIENTE", Source = "ComboboxCliente", Target = "COMBOBOX_CLIENTE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.CLIENTE", RelationPropertyName = "CLIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.DATETIME_CLIENTE", Source = "DatetimeCliente", Target = "DATETIME_CLIENTE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.CLIENTE", RelationPropertyName = "CLIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.ESTADO.ID_ESTADO", Source = "IdEstado", Target = "ID_ESTADO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "DCLinxDemoBM.ESTADO", RelationPropertyName = "ESTADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.SMALL_INT_CLIENTE", Source = "SmallIntCliente", Target = "SMALL_INT_CLIENTE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.CLIENTE", RelationPropertyName = "CLIENTE" });

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
	 

	    public Dictionary<string, string> GetComboboxClienteValues()
	    {
	    	    return VAREJO.BV.Domains.LX_CLIENTE.GetValues();
	    }
	    private string _comboboxClienteName;
	    [DataMember(IsRequired = false, Name = "ComboboxClienteName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Combobox Cliente", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ComboboxClienteName
	    {
	    	    get { if (this.ComboboxCliente.IsNull()) { _comboboxClienteName = String.Empty; } else { string key = this.ComboboxCliente.ToString(); var dmValues = this.GetComboboxClienteValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _comboboxClienteName) _comboboxClienteName = domainName; } return _comboboxClienteName; } set { _comboboxClienteName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="VENDA.ID_VENDA", IsUpdatable=false, EdmName="Linx.Demo.BM.DCLinxDemoBM")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[VendawwwwwwwwwwWWWWW];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdVenda];ReadOnly[false];Entities[VENDA:IdVenda|LOJA:IdLoja];SubQueryInfo[Select 1 From #ParentAlias#.VENDA_LISTA as #Alias#];EdmEntityName[VENDA];EntityRelations[CLIENTE(CLIENTE)#ESTADO(ESTADO)#PAIS(PAIS)#LOJA(LOJA)];EdmParentEntityName[CLIENTE];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "Venda")]
	[Serializable()]
	public partial class Venda : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(WizardExampleDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("Cliente");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdCliente"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdCliente));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load Cliente
	         this.Cliente = (from r in context.GetClienteByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.CLIENTE.ID_CLIENTE];IsMeasure[false]")]
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
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpLoja];LookUpTitle[Seleção de (Id Loja)];LookUpQuery[executeLookUpLoja];LookUpFinalize[finalizeLookUpLoja];LookUpDisplayColumns[{\"IdLoja\" : \"Id Loja\"}];LookUpColumns[{\"IdLoja\" : true}];FilterDataKey[VENDA.LOJA.ID_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<Int32>#IdLoja#true##12:0##Id Loja#0#true##::LookUpLoja##false#false#LOJA#LOJA#VAREJO.BV.WizardExample#IQueryable###true#false", EdmKey="VENDA.LOJA.ID_LOJA")]
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

		

	    #region Parent Association
	 
	    private Cliente _Cliente;
	    [DataMember(Name = "Cliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_Cliente_Venda", "IdCliente", "IdCliente", IsForeignKey=true)]
	    public Cliente Cliente
	    {
	        get
	        {
	            return this._Cliente;
	        }
	        set
	        {
	            if (this._Cliente != value)
	            {
	                this._Cliente = value;
	                this.RaisePropertyChanged("ClienteList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
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

		

	[LinxPublicationView(PrimaryKeys="VENDA_ATACADO.ID_VENDA_ATACADO", IsUpdatable=false, EdmName="Linx.Demo.BM.DCLinxDemoBM")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[VendaAtacadommmmmmmMMMMMM];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdVendaAtacado];ReadOnly[false];Entities[VENDA_ATACADO:IdVendaAtacado];SubQueryInfo[Select 1 From #ParentAlias#.VENDA_ATACADO_LISTA as #Alias#];EdmEntityName[VENDA_ATACADO];EntityRelations[CLIENTE(CLIENTE)#ESTADO(ESTADO)#PAIS(PAIS)];EdmParentEntityName[CLIENTE];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "VendaAtacado")]
	[Serializable()]
	public partial class VendaAtacado : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(WizardExampleDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("Cliente");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdCliente"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdCliente));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load Cliente
	         this.Cliente = (from r in context.GetClienteByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For BigIntVendaAtacado
	    partial void OnBigIntVendaAtacadoChanging(System.Nullable<System.Int64> value);
	    partial void OnBigIntVendaAtacadoChanged();

	    private System.Nullable<System.Int64> _BigIntVendaAtacado;

	    [DataMember(Name = "BigIntVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Big Int Venda Atacado", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.BIG_INT_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.BIG_INT_VENDA_ATACADO")]
	    public System.Nullable<System.Int64> BigIntVendaAtacado
	    {
	    	    get
	    	    {
	    	          return _BigIntVendaAtacado;
	    	    }
	    	    set
	    	    {
	    	          if (this._BigIntVendaAtacado != value)
	    	          {
	    	              this.ValidateProperty("BigIntVendaAtacado", value);
	    	              this.OnBigIntVendaAtacadoChanging(value);
	    	              this.RaiseDataMemberChanging("BigIntVendaAtacado");
	    	              this._BigIntVendaAtacado = value;
	    	              this.RaiseDataMemberChanged("BigIntVendaAtacado");
	    	              this.OnBigIntVendaAtacadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For BitVendaAtacado
	    partial void OnBitVendaAtacadoChanging(System.Nullable<System.Boolean> value);
	    partial void OnBitVendaAtacadoChanged();

	    private System.Nullable<System.Boolean> _BitVendaAtacado;

	    [DataMember(Name = "BitVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bit Venda Atacado", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.BIT_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.BIT_VENDA_ATACADO")]
	    public System.Nullable<System.Boolean> BitVendaAtacado
	    {
	    	    get
	    	    {
	    	          return _BitVendaAtacado;
	    	    }
	    	    set
	    	    {
	    	          if (this._BitVendaAtacado != value)
	    	          {
	    	              this.ValidateProperty("BitVendaAtacado", value);
	    	              this.OnBitVendaAtacadoChanging(value);
	    	              this.RaiseDataMemberChanging("BitVendaAtacado");
	    	              this._BitVendaAtacado = value;
	    	              this.RaiseDataMemberChanged("BitVendaAtacado");
	    	              this.OnBitVendaAtacadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ComboboxVendaAtacado
	    partial void OnComboboxVendaAtacadoChanging(Byte value);
	    partial void OnComboboxVendaAtacadoChanged();

	    private Byte _ComboboxVendaAtacado;

	    [DataMember(IsRequired = true, Name = "ComboboxVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Venda Atacado", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_VENDA_ATACADO];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.COMBOBOX_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.COMBOBOX_VENDA_ATACADO")]
	    public Byte ComboboxVendaAtacado
	    {
	    	    get
	    	    {
	    	          return _ComboboxVendaAtacado;
	    	    }
	    	    set
	    	    {
	    	          if (this._ComboboxVendaAtacado != value)
	    	          {
	    	              this.ValidateProperty("ComboboxVendaAtacado", value);
	    	              this.OnComboboxVendaAtacadoChanging(value);
	    	              this.RaiseDataMemberChanging("ComboboxVendaAtacado");
	    	              this._ComboboxVendaAtacado = value;
	    	              this.RaiseDataMemberChanged("ComboboxVendaAtacado");
	    	              this.OnComboboxVendaAtacadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DatetimeVendaAtacado
	    partial void OnDatetimeVendaAtacadoChanging(System.Nullable<System.DateTime> value);
	    partial void OnDatetimeVendaAtacadoChanged();

	    private System.Nullable<System.DateTime> _DatetimeVendaAtacado;

	    [DataMember(Name = "DatetimeVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Datetime Venda Atacado", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.DATETIME_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.DATETIME_VENDA_ATACADO")]
	    public System.Nullable<System.DateTime> DatetimeVendaAtacado
	    {
	    	    get
	    	    {
	    	          return _DatetimeVendaAtacado;
	    	    }
	    	    set
	    	    {
	    	          if (this._DatetimeVendaAtacado != value)
	    	          {
	    	              this.ValidateProperty("DatetimeVendaAtacado", value);
	    	              this.OnDatetimeVendaAtacadoChanging(value);
	    	              this.RaiseDataMemberChanging("DatetimeVendaAtacado");
	    	              this._DatetimeVendaAtacado = value;
	    	              this.RaiseDataMemberChanged("DatetimeVendaAtacado");
	    	              this.OnDatetimeVendaAtacadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DecimalVendaAtacado
	    partial void OnDecimalVendaAtacadoChanging(System.Nullable<System.Decimal> value);
	    partial void OnDecimalVendaAtacadoChanged();

	    private System.Nullable<System.Decimal> _DecimalVendaAtacado;

	    [DataMember(Name = "DecimalVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal Venda Atacado", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.DECIMAL_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.DECIMAL_VENDA_ATACADO")]
	    public System.Nullable<System.Decimal> DecimalVendaAtacado
	    {
	    	    get
	    	    {
	    	          return _DecimalVendaAtacado;
	    	    }
	    	    set
	    	    {
	    	          if (this._DecimalVendaAtacado != value)
	    	          {
	    	              this.ValidateProperty("DecimalVendaAtacado", value);
	    	              this.OnDecimalVendaAtacadoChanging(value);
	    	              this.RaiseDataMemberChanging("DecimalVendaAtacado");
	    	              this._DecimalVendaAtacado = value;
	    	              this.RaiseDataMemberChanged("DecimalVendaAtacado");
	    	              this.OnDecimalVendaAtacadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For GuidVendaAtacado
	    partial void OnGuidVendaAtacadoChanging(System.Nullable<System.Guid> value);
	    partial void OnGuidVendaAtacadoChanged();

	    private System.Nullable<System.Guid> _GuidVendaAtacado;

	    [DataMember(Name = "GuidVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Guid Venda Atacado", Description="", Order = 6, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.GUID_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.GUID_VENDA_ATACADO")]
	    public System.Nullable<System.Guid> GuidVendaAtacado
	    {
	    	    get
	    	    {
	    	          return _GuidVendaAtacado;
	    	    }
	    	    set
	    	    {
	    	          if (this._GuidVendaAtacado != value)
	    	          {
	    	              this.ValidateProperty("GuidVendaAtacado", value);
	    	              this.OnGuidVendaAtacadoChanging(value);
	    	              this.RaiseDataMemberChanging("GuidVendaAtacado");
	    	              this._GuidVendaAtacado = value;
	    	              this.RaiseDataMemberChanged("GuidVendaAtacado");
	    	              this.OnGuidVendaAtacadoChanged();
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
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.CLIENTE.ID_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.CLIENTE.ID_CLIENTE")]
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
	    //Extensibility Partial Method Definitions For IdVendaAtacado
	    partial void OnIdVendaAtacadoChanging(Int32 value);
	    partial void OnIdVendaAtacadoChanged();

	    private Int32 _IdVendaAtacado;

	    [DataMember(IsRequired = true, Name = "IdVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Venda Atacado", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.ID_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.ID_VENDA_ATACADO")]
	    public Int32 IdVendaAtacado
	    {
	    	    get
	    	    {
	    	          return _IdVendaAtacado;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdVendaAtacado != value)
	    	          {
	    	              this.ValidateProperty("IdVendaAtacado", value);
	    	              this.OnIdVendaAtacadoChanging(value);
	    	              this.RaiseDataMemberChanging("IdVendaAtacado");
	    	              this._IdVendaAtacado = value;
	    	              this.RaiseDataMemberChanged("IdVendaAtacado");
	    	              this.OnIdVendaAtacadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IntVendaAtacado
	    partial void OnIntVendaAtacadoChanging(System.Nullable<System.Int32> value);
	    partial void OnIntVendaAtacadoChanged();

	    private System.Nullable<System.Int32> _IntVendaAtacado;

	    [DataMember(Name = "IntVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int Venda Atacado", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.INT_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.INT_VENDA_ATACADO")]
	    public System.Nullable<System.Int32> IntVendaAtacado
	    {
	    	    get
	    	    {
	    	          return _IntVendaAtacado;
	    	    }
	    	    set
	    	    {
	    	          if (this._IntVendaAtacado != value)
	    	          {
	    	              this.ValidateProperty("IntVendaAtacado", value);
	    	              this.OnIntVendaAtacadoChanging(value);
	    	              this.RaiseDataMemberChanging("IntVendaAtacado");
	    	              this._IntVendaAtacado = value;
	    	              this.RaiseDataMemberChanged("IntVendaAtacado");
	    	              this.OnIntVendaAtacadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For SmallIntVendaAtacado
	    partial void OnSmallIntVendaAtacadoChanging(System.Nullable<System.Int16> value);
	    partial void OnSmallIntVendaAtacadoChanged();

	    private System.Nullable<System.Int16> _SmallIntVendaAtacado;

	    [DataMember(Name = "SmallIntVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Small Int Venda Atacado", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.SMALL_INT_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.SMALL_INT_VENDA_ATACADO")]
	    public System.Nullable<System.Int16> SmallIntVendaAtacado
	    {
	    	    get
	    	    {
	    	          return _SmallIntVendaAtacado;
	    	    }
	    	    set
	    	    {
	    	          if (this._SmallIntVendaAtacado != value)
	    	          {
	    	              this.ValidateProperty("SmallIntVendaAtacado", value);
	    	              this.OnSmallIntVendaAtacadoChanging(value);
	    	              this.RaiseDataMemberChanging("SmallIntVendaAtacado");
	    	              this._SmallIntVendaAtacado = value;
	    	              this.RaiseDataMemberChanged("SmallIntVendaAtacado");
	    	              this.OnSmallIntVendaAtacadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringVendaAtacado
	    partial void OnStringVendaAtacadoChanging(System.String value);
	    partial void OnStringVendaAtacadoChanged();

	    private System.String _StringVendaAtacado;

	    [DataMember(Name = "StringVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Venda Atacado", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.STRING_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.STRING_VENDA_ATACADO")]
	    public System.String StringVendaAtacado
	    {
	    	    get
	    	    {
	    	          return _StringVendaAtacado;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringVendaAtacado != value)
	    	          {
	    	              this.ValidateProperty("StringVendaAtacado", value);
	    	              this.OnStringVendaAtacadoChanging(value);
	    	              this.RaiseDataMemberChanging("StringVendaAtacado");
	    	              this._StringVendaAtacado = value;
	    	              this.RaiseDataMemberChanged("StringVendaAtacado");
	    	              this.OnStringVendaAtacadoChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIdVendaAtacado;
	    [DataMember(Name = "TemporaryIdVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Venda Atacado (Tmp)", Description="Temporary Key", Order = 7, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdVendaAtacado
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdVendaAtacado.IsNullOrEmpty())
	    	                this._TemporaryIdVendaAtacado = this._IdVendaAtacado;
	    	          return this._TemporaryIdVendaAtacado;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdVendaAtacado != value)
	    	              this._TemporaryIdVendaAtacado = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private Cliente _Cliente;
	    [DataMember(Name = "Cliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_Cliente_VendaAtacado", "IdCliente", "IdCliente", IsForeignKey=true)]
	    public Cliente Cliente
	    {
	        get
	        {
	            return this._Cliente;
	        }
	        set
	        {
	            if (this._Cliente != value)
	            {
	                this._Cliente = value;
	                this.RaisePropertyChanged("ClienteList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "DCLinxDemoBM.VENDA_ATACADO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Demo.BM.VENDA_ATACADO), QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ATACADO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.ID_VENDA_ATACADO", Source = "IdVendaAtacado", Target = "ID_VENDA_ATACADO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.BIT_VENDA_ATACADO", Source = "BitVendaAtacado", Target = "BIT_VENDA_ATACADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.INT_VENDA_ATACADO", Source = "IntVendaAtacado", Target = "INT_VENDA_ATACADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.GUID_VENDA_ATACADO", Source = "GuidVendaAtacado", Target = "GUID_VENDA_ATACADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.CLIENTE.ID_CLIENTE", Source = "IdCliente", Target = "ID_CLIENTE", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "DCLinxDemoBM.CLIENTE", RelationPropertyName = "CLIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.STRING_VENDA_ATACADO", Source = "StringVendaAtacado", Target = "STRING_VENDA_ATACADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.BIG_INT_VENDA_ATACADO", Source = "BigIntVendaAtacado", Target = "BIG_INT_VENDA_ATACADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.DECIMAL_VENDA_ATACADO", Source = "DecimalVendaAtacado", Target = "DECIMAL_VENDA_ATACADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.COMBOBOX_VENDA_ATACADO", Source = "ComboboxVendaAtacado", Target = "COMBOBOX_VENDA_ATACADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.DATETIME_VENDA_ATACADO", Source = "DatetimeVendaAtacado", Target = "DATETIME_VENDA_ATACADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.SMALL_INT_VENDA_ATACADO", Source = "SmallIntVendaAtacado", Target = "SMALL_INT_VENDA_ATACADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });

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
	 

	    public Dictionary<string, string> GetComboboxVendaAtacadoValues()
	    {
	    	    return VAREJO.BV.Domains.LX_VENDA_ATACADO.GetValues();
	    }
	    private string _comboboxVendaAtacadoName;
	    [DataMember(IsRequired = false, Name = "ComboboxVendaAtacadoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Combobox Venda Atacado", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ComboboxVendaAtacadoName
	    {
	    	    get { if (this.ComboboxVendaAtacado.IsNull()) { _comboboxVendaAtacadoName = String.Empty; } else { string key = this.ComboboxVendaAtacado.ToString(); var dmValues = this.GetComboboxVendaAtacadoValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _comboboxVendaAtacadoName) _comboboxVendaAtacadoName = domainName; } return _comboboxVendaAtacadoName; } set { _comboboxVendaAtacadoName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[VendawwwwwwwwwwWWWWW];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdVenda];ReadOnly[false];Entities[VENDA:IdVenda|LOJA:IdLoja];SubQueryInfo[Select 1 From #ParentAlias#.VENDA_LISTA as #Alias#];EdmEntityName[VENDA];EntityRelations[CLIENTE(CLIENTE)#ESTADO(ESTADO)#PAIS(PAIS)#LOJA(LOJA)];EdmParentEntityName[CLIENTE];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "Venda")]
	[Serializable()]
	public partial class VendaParentComposition : Linx.Data.Entity
	{

	
	
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
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.CLIENTE.ID_CLIENTE];IsMeasure[false]")]
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
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpLoja];LookUpTitle[Seleção de (Id Loja)];LookUpQuery[executeLookUpLoja];LookUpFinalize[finalizeLookUpLoja];LookUpDisplayColumns[{\"IdLoja\" : \"Id Loja\"}];LookUpColumns[{\"IdLoja\" : true}];FilterDataKey[VENDA.LOJA.ID_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<Int32>#IdLoja#true##12:0##Id Loja#0#true##::LookUpLoja##false#false#LOJA#LOJA#VAREJO.BV.WizardExample#IQueryable###true#false", EdmKey="VENDA.LOJA.ID_LOJA")]
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
	    //Extensibility Partial Method Definitions For BigIntCliente
	    partial void OnBigIntClienteChanging(System.Nullable<System.Int64> value);
	    partial void OnBigIntClienteChanged();

	    private System.Nullable<System.Int64> _BigIntCliente;

	    [DataMember(Name = "BigIntCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Big Int Cliente", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA.CLIENTE.BIG_INT_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.BIG_INT_CLIENTE")]
	    public System.Nullable<System.Int64> BigIntCliente
	    {
	    	    get
	    	    {
	    	          return _BigIntCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._BigIntCliente != value)
	    	          {
	    	              this.ValidateProperty("BigIntCliente", value);
	    	              this.OnBigIntClienteChanging(value);
	    	              this.RaiseDataMemberChanging("BigIntCliente");
	    	              this._BigIntCliente = value;
	    	              this.RaiseDataMemberChanged("BigIntCliente");
	    	              this.OnBigIntClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For BitCliente
	    partial void OnBitClienteChanging(System.Nullable<System.Boolean> value);
	    partial void OnBitClienteChanged();

	    private System.Nullable<System.Boolean> _BitCliente;

	    [DataMember(Name = "BitCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bit Cliente", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA.CLIENTE.BIT_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.BIT_CLIENTE")]
	    public System.Nullable<System.Boolean> BitCliente
	    {
	    	    get
	    	    {
	    	          return _BitCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._BitCliente != value)
	    	          {
	    	              this.ValidateProperty("BitCliente", value);
	    	              this.OnBitClienteChanging(value);
	    	              this.RaiseDataMemberChanging("BitCliente");
	    	              this._BitCliente = value;
	    	              this.RaiseDataMemberChanged("BitCliente");
	    	              this.OnBitClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ComboboxCliente
	    partial void OnComboboxClienteChanging(Byte value);
	    partial void OnComboboxClienteChanged();

	    private Byte _ComboboxCliente;

	    [DataMember(IsRequired = true, Name = "ComboboxCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Cliente", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_CLIENTE];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA.CLIENTE.COMBOBOX_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.COMBOBOX_CLIENTE")]
	    public Byte ComboboxCliente
	    {
	    	    get
	    	    {
	    	          return _ComboboxCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._ComboboxCliente != value)
	    	          {
	    	              this.ValidateProperty("ComboboxCliente", value);
	    	              this.OnComboboxClienteChanging(value);
	    	              this.RaiseDataMemberChanging("ComboboxCliente");
	    	              this._ComboboxCliente = value;
	    	              this.RaiseDataMemberChanged("ComboboxCliente");
	    	              this.OnComboboxClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DatetimeCliente
	    partial void OnDatetimeClienteChanging(System.Nullable<System.DateTime> value);
	    partial void OnDatetimeClienteChanged();

	    private System.Nullable<System.DateTime> _DatetimeCliente;

	    [DataMember(Name = "DatetimeCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Datetime Cliente", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA.CLIENTE.DATETIME_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.DATETIME_CLIENTE")]
	    public System.Nullable<System.DateTime> DatetimeCliente
	    {
	    	    get
	    	    {
	    	          return _DatetimeCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._DatetimeCliente != value)
	    	          {
	    	              this.ValidateProperty("DatetimeCliente", value);
	    	              this.OnDatetimeClienteChanging(value);
	    	              this.RaiseDataMemberChanging("DatetimeCliente");
	    	              this._DatetimeCliente = value;
	    	              this.RaiseDataMemberChanged("DatetimeCliente");
	    	              this.OnDatetimeClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DecimalCliente
	    partial void OnDecimalClienteChanging(System.Nullable<System.Decimal> value);
	    partial void OnDecimalClienteChanged();

	    private System.Nullable<System.Decimal> _DecimalCliente;

	    [DataMember(Name = "DecimalCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal Cliente", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA.CLIENTE.DECIMAL_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.DECIMAL_CLIENTE")]
	    public System.Nullable<System.Decimal> DecimalCliente
	    {
	    	    get
	    	    {
	    	          return _DecimalCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._DecimalCliente != value)
	    	          {
	    	              this.ValidateProperty("DecimalCliente", value);
	    	              this.OnDecimalClienteChanging(value);
	    	              this.RaiseDataMemberChanging("DecimalCliente");
	    	              this._DecimalCliente = value;
	    	              this.RaiseDataMemberChanged("DecimalCliente");
	    	              this.OnDecimalClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For GuidCliente
	    partial void OnGuidClienteChanging(System.Nullable<System.Guid> value);
	    partial void OnGuidClienteChanged();

	    private System.Nullable<System.Guid> _GuidCliente;

	    [DataMember(Name = "GuidCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Guid Cliente", Description="", Order = 6, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA.CLIENTE.GUID_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.GUID_CLIENTE")]
	    public System.Nullable<System.Guid> GuidCliente
	    {
	    	    get
	    	    {
	    	          return _GuidCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._GuidCliente != value)
	    	          {
	    	              this.ValidateProperty("GuidCliente", value);
	    	              this.OnGuidClienteChanging(value);
	    	              this.RaiseDataMemberChanging("GuidCliente");
	    	              this._GuidCliente = value;
	    	              this.RaiseDataMemberChanged("GuidCliente");
	    	              this.OnGuidClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdEstado
	    partial void OnIdEstadoChanging(System.Nullable<Int32> value);
	    partial void OnIdEstadoChanged();

	    private System.Nullable<Int32> _IdEstado;

	    [DataMember(Name = "IdEstado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Estado", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA.CLIENTE.ESTADO.ID_ESTADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.ESTADO.ID_ESTADO")]
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
	    	              this.ValidateProperty("IdEstado", value);
	    	              this.OnIdEstadoChanging(value);
	    	              this.RaiseDataMemberChanging("IdEstado");
	    	              this._IdEstado = value;
	    	              this.RaiseDataMemberChanged("IdEstado");
	    	              this.OnIdEstadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IntCliente
	    partial void OnIntClienteChanging(System.Nullable<System.Int32> value);
	    partial void OnIntClienteChanged();

	    private System.Nullable<System.Int32> _IntCliente;

	    [DataMember(Name = "IntCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int Cliente", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA.CLIENTE.INT_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.INT_CLIENTE")]
	    public System.Nullable<System.Int32> IntCliente
	    {
	    	    get
	    	    {
	    	          return _IntCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._IntCliente != value)
	    	          {
	    	              this.ValidateProperty("IntCliente", value);
	    	              this.OnIntClienteChanging(value);
	    	              this.RaiseDataMemberChanging("IntCliente");
	    	              this._IntCliente = value;
	    	              this.RaiseDataMemberChanged("IntCliente");
	    	              this.OnIntClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For SmallIntCliente
	    partial void OnSmallIntClienteChanging(System.Nullable<System.Int16> value);
	    partial void OnSmallIntClienteChanged();

	    private System.Nullable<System.Int16> _SmallIntCliente;

	    [DataMember(Name = "SmallIntCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Small Int Cliente", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA.CLIENTE.SMALL_INT_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.SMALL_INT_CLIENTE")]
	    public System.Nullable<System.Int16> SmallIntCliente
	    {
	    	    get
	    	    {
	    	          return _SmallIntCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._SmallIntCliente != value)
	    	          {
	    	              this.ValidateProperty("SmallIntCliente", value);
	    	              this.OnSmallIntClienteChanging(value);
	    	              this.RaiseDataMemberChanging("SmallIntCliente");
	    	              this._SmallIntCliente = value;
	    	              this.RaiseDataMemberChanged("SmallIntCliente");
	    	              this.OnSmallIntClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringCliente
	    partial void OnStringClienteChanging(System.String value);
	    partial void OnStringClienteChanged();

	    private System.String _StringCliente;

	    [DataMember(Name = "StringCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Cliente", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA.CLIENTE.STRING_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.STRING_CLIENTE")]
	    public System.String StringCliente
	    {
	    	    get
	    	    {
	    	          return _StringCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringCliente != value)
	    	          {
	    	              this.ValidateProperty("StringCliente", value);
	    	              this.OnStringClienteChanging(value);
	    	              this.RaiseDataMemberChanging("StringCliente");
	    	              this._StringCliente = value;
	    	              this.RaiseDataMemberChanged("StringCliente");
	    	              this.OnStringClienteChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
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
	    public Dictionary<string, string> GetComboboxClienteValues()
	    {
	    	    return VAREJO.BV.Domains.LX_CLIENTE.GetValues();
	    }
	    private string _comboboxClienteName;
	    [DataMember(IsRequired = false, Name = "ComboboxClienteName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Combobox Cliente", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ComboboxClienteName
	    {
	    	    get { if (this.ComboboxCliente.IsNull()) { _comboboxClienteName = String.Empty; } else { string key = this.ComboboxCliente.ToString(); var dmValues = this.GetComboboxClienteValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _comboboxClienteName) _comboboxClienteName = domainName; } return _comboboxClienteName; } set { _comboboxClienteName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[VendaAtacadommmmmmmMMMMMM];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdVendaAtacado];ReadOnly[false];Entities[VENDA_ATACADO:IdVendaAtacado];SubQueryInfo[Select 1 From #ParentAlias#.VENDA_ATACADO_LISTA as #Alias#];EdmEntityName[VENDA_ATACADO];EntityRelations[CLIENTE(CLIENTE)#ESTADO(ESTADO)#PAIS(PAIS)];EdmParentEntityName[CLIENTE];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "VendaAtacado")]
	[Serializable()]
	public partial class VendaAtacadoParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For BigIntVendaAtacado
	    partial void OnBigIntVendaAtacadoChanging(System.Nullable<System.Int64> value);
	    partial void OnBigIntVendaAtacadoChanged();

	    private System.Nullable<System.Int64> _BigIntVendaAtacado;

	    [DataMember(Name = "BigIntVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Big Int Venda Atacado", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.BIG_INT_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.BIG_INT_VENDA_ATACADO")]
	    public System.Nullable<System.Int64> BigIntVendaAtacado
	    {
	    	    get
	    	    {
	    	          return _BigIntVendaAtacado;
	    	    }
	    	    set
	    	    {
	    	          if (this._BigIntVendaAtacado != value)
	    	          {
	    	              this.ValidateProperty("BigIntVendaAtacado", value);
	    	              this.OnBigIntVendaAtacadoChanging(value);
	    	              this.RaiseDataMemberChanging("BigIntVendaAtacado");
	    	              this._BigIntVendaAtacado = value;
	    	              this.RaiseDataMemberChanged("BigIntVendaAtacado");
	    	              this.OnBigIntVendaAtacadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For BitVendaAtacado
	    partial void OnBitVendaAtacadoChanging(System.Nullable<System.Boolean> value);
	    partial void OnBitVendaAtacadoChanged();

	    private System.Nullable<System.Boolean> _BitVendaAtacado;

	    [DataMember(Name = "BitVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bit Venda Atacado", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.BIT_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.BIT_VENDA_ATACADO")]
	    public System.Nullable<System.Boolean> BitVendaAtacado
	    {
	    	    get
	    	    {
	    	          return _BitVendaAtacado;
	    	    }
	    	    set
	    	    {
	    	          if (this._BitVendaAtacado != value)
	    	          {
	    	              this.ValidateProperty("BitVendaAtacado", value);
	    	              this.OnBitVendaAtacadoChanging(value);
	    	              this.RaiseDataMemberChanging("BitVendaAtacado");
	    	              this._BitVendaAtacado = value;
	    	              this.RaiseDataMemberChanged("BitVendaAtacado");
	    	              this.OnBitVendaAtacadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ComboboxVendaAtacado
	    partial void OnComboboxVendaAtacadoChanging(Byte value);
	    partial void OnComboboxVendaAtacadoChanged();

	    private Byte _ComboboxVendaAtacado;

	    [DataMember(IsRequired = true, Name = "ComboboxVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Venda Atacado", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_VENDA_ATACADO];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.COMBOBOX_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.COMBOBOX_VENDA_ATACADO")]
	    public Byte ComboboxVendaAtacado
	    {
	    	    get
	    	    {
	    	          return _ComboboxVendaAtacado;
	    	    }
	    	    set
	    	    {
	    	          if (this._ComboboxVendaAtacado != value)
	    	          {
	    	              this.ValidateProperty("ComboboxVendaAtacado", value);
	    	              this.OnComboboxVendaAtacadoChanging(value);
	    	              this.RaiseDataMemberChanging("ComboboxVendaAtacado");
	    	              this._ComboboxVendaAtacado = value;
	    	              this.RaiseDataMemberChanged("ComboboxVendaAtacado");
	    	              this.OnComboboxVendaAtacadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DatetimeVendaAtacado
	    partial void OnDatetimeVendaAtacadoChanging(System.Nullable<System.DateTime> value);
	    partial void OnDatetimeVendaAtacadoChanged();

	    private System.Nullable<System.DateTime> _DatetimeVendaAtacado;

	    [DataMember(Name = "DatetimeVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Datetime Venda Atacado", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.DATETIME_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.DATETIME_VENDA_ATACADO")]
	    public System.Nullable<System.DateTime> DatetimeVendaAtacado
	    {
	    	    get
	    	    {
	    	          return _DatetimeVendaAtacado;
	    	    }
	    	    set
	    	    {
	    	          if (this._DatetimeVendaAtacado != value)
	    	          {
	    	              this.ValidateProperty("DatetimeVendaAtacado", value);
	    	              this.OnDatetimeVendaAtacadoChanging(value);
	    	              this.RaiseDataMemberChanging("DatetimeVendaAtacado");
	    	              this._DatetimeVendaAtacado = value;
	    	              this.RaiseDataMemberChanged("DatetimeVendaAtacado");
	    	              this.OnDatetimeVendaAtacadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DecimalVendaAtacado
	    partial void OnDecimalVendaAtacadoChanging(System.Nullable<System.Decimal> value);
	    partial void OnDecimalVendaAtacadoChanged();

	    private System.Nullable<System.Decimal> _DecimalVendaAtacado;

	    [DataMember(Name = "DecimalVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal Venda Atacado", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.DECIMAL_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.DECIMAL_VENDA_ATACADO")]
	    public System.Nullable<System.Decimal> DecimalVendaAtacado
	    {
	    	    get
	    	    {
	    	          return _DecimalVendaAtacado;
	    	    }
	    	    set
	    	    {
	    	          if (this._DecimalVendaAtacado != value)
	    	          {
	    	              this.ValidateProperty("DecimalVendaAtacado", value);
	    	              this.OnDecimalVendaAtacadoChanging(value);
	    	              this.RaiseDataMemberChanging("DecimalVendaAtacado");
	    	              this._DecimalVendaAtacado = value;
	    	              this.RaiseDataMemberChanged("DecimalVendaAtacado");
	    	              this.OnDecimalVendaAtacadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For GuidVendaAtacado
	    partial void OnGuidVendaAtacadoChanging(System.Nullable<System.Guid> value);
	    partial void OnGuidVendaAtacadoChanged();

	    private System.Nullable<System.Guid> _GuidVendaAtacado;

	    [DataMember(Name = "GuidVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Guid Venda Atacado", Description="", Order = 6, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.GUID_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.GUID_VENDA_ATACADO")]
	    public System.Nullable<System.Guid> GuidVendaAtacado
	    {
	    	    get
	    	    {
	    	          return _GuidVendaAtacado;
	    	    }
	    	    set
	    	    {
	    	          if (this._GuidVendaAtacado != value)
	    	          {
	    	              this.ValidateProperty("GuidVendaAtacado", value);
	    	              this.OnGuidVendaAtacadoChanging(value);
	    	              this.RaiseDataMemberChanging("GuidVendaAtacado");
	    	              this._GuidVendaAtacado = value;
	    	              this.RaiseDataMemberChanged("GuidVendaAtacado");
	    	              this.OnGuidVendaAtacadoChanged();
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
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.CLIENTE.ID_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.CLIENTE.ID_CLIENTE")]
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
	    //Extensibility Partial Method Definitions For IdVendaAtacado
	    partial void OnIdVendaAtacadoChanging(Int32 value);
	    partial void OnIdVendaAtacadoChanged();

	    private Int32 _IdVendaAtacado;

	    [DataMember(IsRequired = true, Name = "IdVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Venda Atacado", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.ID_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.ID_VENDA_ATACADO")]
	    public Int32 IdVendaAtacado
	    {
	    	    get
	    	    {
	    	          return _IdVendaAtacado;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdVendaAtacado != value)
	    	          {
	    	              this.ValidateProperty("IdVendaAtacado", value);
	    	              this.OnIdVendaAtacadoChanging(value);
	    	              this.RaiseDataMemberChanging("IdVendaAtacado");
	    	              this._IdVendaAtacado = value;
	    	              this.RaiseDataMemberChanged("IdVendaAtacado");
	    	              this.OnIdVendaAtacadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IntVendaAtacado
	    partial void OnIntVendaAtacadoChanging(System.Nullable<System.Int32> value);
	    partial void OnIntVendaAtacadoChanged();

	    private System.Nullable<System.Int32> _IntVendaAtacado;

	    [DataMember(Name = "IntVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int Venda Atacado", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.INT_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.INT_VENDA_ATACADO")]
	    public System.Nullable<System.Int32> IntVendaAtacado
	    {
	    	    get
	    	    {
	    	          return _IntVendaAtacado;
	    	    }
	    	    set
	    	    {
	    	          if (this._IntVendaAtacado != value)
	    	          {
	    	              this.ValidateProperty("IntVendaAtacado", value);
	    	              this.OnIntVendaAtacadoChanging(value);
	    	              this.RaiseDataMemberChanging("IntVendaAtacado");
	    	              this._IntVendaAtacado = value;
	    	              this.RaiseDataMemberChanged("IntVendaAtacado");
	    	              this.OnIntVendaAtacadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For SmallIntVendaAtacado
	    partial void OnSmallIntVendaAtacadoChanging(System.Nullable<System.Int16> value);
	    partial void OnSmallIntVendaAtacadoChanged();

	    private System.Nullable<System.Int16> _SmallIntVendaAtacado;

	    [DataMember(Name = "SmallIntVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Small Int Venda Atacado", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.SMALL_INT_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.SMALL_INT_VENDA_ATACADO")]
	    public System.Nullable<System.Int16> SmallIntVendaAtacado
	    {
	    	    get
	    	    {
	    	          return _SmallIntVendaAtacado;
	    	    }
	    	    set
	    	    {
	    	          if (this._SmallIntVendaAtacado != value)
	    	          {
	    	              this.ValidateProperty("SmallIntVendaAtacado", value);
	    	              this.OnSmallIntVendaAtacadoChanging(value);
	    	              this.RaiseDataMemberChanging("SmallIntVendaAtacado");
	    	              this._SmallIntVendaAtacado = value;
	    	              this.RaiseDataMemberChanged("SmallIntVendaAtacado");
	    	              this.OnSmallIntVendaAtacadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringVendaAtacado
	    partial void OnStringVendaAtacadoChanging(System.String value);
	    partial void OnStringVendaAtacadoChanged();

	    private System.String _StringVendaAtacado;

	    [DataMember(Name = "StringVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Venda Atacado", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.STRING_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.STRING_VENDA_ATACADO")]
	    public System.String StringVendaAtacado
	    {
	    	    get
	    	    {
	    	          return _StringVendaAtacado;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringVendaAtacado != value)
	    	          {
	    	              this.ValidateProperty("StringVendaAtacado", value);
	    	              this.OnStringVendaAtacadoChanging(value);
	    	              this.RaiseDataMemberChanging("StringVendaAtacado");
	    	              this._StringVendaAtacado = value;
	    	              this.RaiseDataMemberChanged("StringVendaAtacado");
	    	              this.OnStringVendaAtacadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For BigIntCliente
	    partial void OnBigIntClienteChanging(System.Nullable<System.Int64> value);
	    partial void OnBigIntClienteChanged();

	    private System.Nullable<System.Int64> _BigIntCliente;

	    [DataMember(Name = "BigIntCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Big Int Cliente", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ATACADO.CLIENTE.BIG_INT_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.BIG_INT_CLIENTE")]
	    public System.Nullable<System.Int64> BigIntCliente
	    {
	    	    get
	    	    {
	    	          return _BigIntCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._BigIntCliente != value)
	    	          {
	    	              this.ValidateProperty("BigIntCliente", value);
	    	              this.OnBigIntClienteChanging(value);
	    	              this.RaiseDataMemberChanging("BigIntCliente");
	    	              this._BigIntCliente = value;
	    	              this.RaiseDataMemberChanged("BigIntCliente");
	    	              this.OnBigIntClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For BitCliente
	    partial void OnBitClienteChanging(System.Nullable<System.Boolean> value);
	    partial void OnBitClienteChanged();

	    private System.Nullable<System.Boolean> _BitCliente;

	    [DataMember(Name = "BitCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bit Cliente", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ATACADO.CLIENTE.BIT_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.BIT_CLIENTE")]
	    public System.Nullable<System.Boolean> BitCliente
	    {
	    	    get
	    	    {
	    	          return _BitCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._BitCliente != value)
	    	          {
	    	              this.ValidateProperty("BitCliente", value);
	    	              this.OnBitClienteChanging(value);
	    	              this.RaiseDataMemberChanging("BitCliente");
	    	              this._BitCliente = value;
	    	              this.RaiseDataMemberChanged("BitCliente");
	    	              this.OnBitClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ComboboxCliente
	    partial void OnComboboxClienteChanging(Byte value);
	    partial void OnComboboxClienteChanged();

	    private Byte _ComboboxCliente;

	    [DataMember(IsRequired = true, Name = "ComboboxCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Cliente", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_CLIENTE];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ATACADO.CLIENTE.COMBOBOX_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.COMBOBOX_CLIENTE")]
	    public Byte ComboboxCliente
	    {
	    	    get
	    	    {
	    	          return _ComboboxCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._ComboboxCliente != value)
	    	          {
	    	              this.ValidateProperty("ComboboxCliente", value);
	    	              this.OnComboboxClienteChanging(value);
	    	              this.RaiseDataMemberChanging("ComboboxCliente");
	    	              this._ComboboxCliente = value;
	    	              this.RaiseDataMemberChanged("ComboboxCliente");
	    	              this.OnComboboxClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DatetimeCliente
	    partial void OnDatetimeClienteChanging(System.Nullable<System.DateTime> value);
	    partial void OnDatetimeClienteChanged();

	    private System.Nullable<System.DateTime> _DatetimeCliente;

	    [DataMember(Name = "DatetimeCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Datetime Cliente", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ATACADO.CLIENTE.DATETIME_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.DATETIME_CLIENTE")]
	    public System.Nullable<System.DateTime> DatetimeCliente
	    {
	    	    get
	    	    {
	    	          return _DatetimeCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._DatetimeCliente != value)
	    	          {
	    	              this.ValidateProperty("DatetimeCliente", value);
	    	              this.OnDatetimeClienteChanging(value);
	    	              this.RaiseDataMemberChanging("DatetimeCliente");
	    	              this._DatetimeCliente = value;
	    	              this.RaiseDataMemberChanged("DatetimeCliente");
	    	              this.OnDatetimeClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DecimalCliente
	    partial void OnDecimalClienteChanging(System.Nullable<System.Decimal> value);
	    partial void OnDecimalClienteChanged();

	    private System.Nullable<System.Decimal> _DecimalCliente;

	    [DataMember(Name = "DecimalCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal Cliente", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ATACADO.CLIENTE.DECIMAL_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.DECIMAL_CLIENTE")]
	    public System.Nullable<System.Decimal> DecimalCliente
	    {
	    	    get
	    	    {
	    	          return _DecimalCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._DecimalCliente != value)
	    	          {
	    	              this.ValidateProperty("DecimalCliente", value);
	    	              this.OnDecimalClienteChanging(value);
	    	              this.RaiseDataMemberChanging("DecimalCliente");
	    	              this._DecimalCliente = value;
	    	              this.RaiseDataMemberChanged("DecimalCliente");
	    	              this.OnDecimalClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For GuidCliente
	    partial void OnGuidClienteChanging(System.Nullable<System.Guid> value);
	    partial void OnGuidClienteChanged();

	    private System.Nullable<System.Guid> _GuidCliente;

	    [DataMember(Name = "GuidCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Guid Cliente", Description="", Order = 6, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ATACADO.CLIENTE.GUID_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.GUID_CLIENTE")]
	    public System.Nullable<System.Guid> GuidCliente
	    {
	    	    get
	    	    {
	    	          return _GuidCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._GuidCliente != value)
	    	          {
	    	              this.ValidateProperty("GuidCliente", value);
	    	              this.OnGuidClienteChanging(value);
	    	              this.RaiseDataMemberChanging("GuidCliente");
	    	              this._GuidCliente = value;
	    	              this.RaiseDataMemberChanged("GuidCliente");
	    	              this.OnGuidClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdEstado
	    partial void OnIdEstadoChanging(System.Nullable<Int32> value);
	    partial void OnIdEstadoChanged();

	    private System.Nullable<Int32> _IdEstado;

	    [DataMember(Name = "IdEstado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Estado", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ATACADO.CLIENTE.ESTADO.ID_ESTADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.ESTADO.ID_ESTADO")]
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
	    	              this.ValidateProperty("IdEstado", value);
	    	              this.OnIdEstadoChanging(value);
	    	              this.RaiseDataMemberChanging("IdEstado");
	    	              this._IdEstado = value;
	    	              this.RaiseDataMemberChanged("IdEstado");
	    	              this.OnIdEstadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IntCliente
	    partial void OnIntClienteChanging(System.Nullable<System.Int32> value);
	    partial void OnIntClienteChanged();

	    private System.Nullable<System.Int32> _IntCliente;

	    [DataMember(Name = "IntCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int Cliente", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ATACADO.CLIENTE.INT_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.INT_CLIENTE")]
	    public System.Nullable<System.Int32> IntCliente
	    {
	    	    get
	    	    {
	    	          return _IntCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._IntCliente != value)
	    	          {
	    	              this.ValidateProperty("IntCliente", value);
	    	              this.OnIntClienteChanging(value);
	    	              this.RaiseDataMemberChanging("IntCliente");
	    	              this._IntCliente = value;
	    	              this.RaiseDataMemberChanged("IntCliente");
	    	              this.OnIntClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For SmallIntCliente
	    partial void OnSmallIntClienteChanging(System.Nullable<System.Int16> value);
	    partial void OnSmallIntClienteChanged();

	    private System.Nullable<System.Int16> _SmallIntCliente;

	    [DataMember(Name = "SmallIntCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Small Int Cliente", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ATACADO.CLIENTE.SMALL_INT_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.SMALL_INT_CLIENTE")]
	    public System.Nullable<System.Int16> SmallIntCliente
	    {
	    	    get
	    	    {
	    	          return _SmallIntCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._SmallIntCliente != value)
	    	          {
	    	              this.ValidateProperty("SmallIntCliente", value);
	    	              this.OnSmallIntClienteChanging(value);
	    	              this.RaiseDataMemberChanging("SmallIntCliente");
	    	              this._SmallIntCliente = value;
	    	              this.RaiseDataMemberChanged("SmallIntCliente");
	    	              this.OnSmallIntClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringCliente
	    partial void OnStringClienteChanging(System.String value);
	    partial void OnStringClienteChanged();

	    private System.String _StringCliente;

	    [DataMember(Name = "StringCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Cliente", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ATACADO.CLIENTE.STRING_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.STRING_CLIENTE")]
	    public System.String StringCliente
	    {
	    	    get
	    	    {
	    	          return _StringCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringCliente != value)
	    	          {
	    	              this.ValidateProperty("StringCliente", value);
	    	              this.OnStringClienteChanging(value);
	    	              this.RaiseDataMemberChanging("StringCliente");
	    	              this._StringCliente = value;
	    	              this.RaiseDataMemberChanged("StringCliente");
	    	              this.OnStringClienteChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "DCLinxDemoBM.VENDA_ATACADO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Demo.BM.VENDA_ATACADO), QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ATACADO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.ID_VENDA_ATACADO", Source = "IdVendaAtacado", Target = "ID_VENDA_ATACADO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.BIT_VENDA_ATACADO", Source = "BitVendaAtacado", Target = "BIT_VENDA_ATACADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.INT_VENDA_ATACADO", Source = "IntVendaAtacado", Target = "INT_VENDA_ATACADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.GUID_VENDA_ATACADO", Source = "GuidVendaAtacado", Target = "GUID_VENDA_ATACADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.CLIENTE.ID_CLIENTE", Source = "IdCliente", Target = "ID_CLIENTE", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "DCLinxDemoBM.CLIENTE", RelationPropertyName = "CLIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.STRING_VENDA_ATACADO", Source = "StringVendaAtacado", Target = "STRING_VENDA_ATACADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.BIG_INT_VENDA_ATACADO", Source = "BigIntVendaAtacado", Target = "BIG_INT_VENDA_ATACADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.DECIMAL_VENDA_ATACADO", Source = "DecimalVendaAtacado", Target = "DECIMAL_VENDA_ATACADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.COMBOBOX_VENDA_ATACADO", Source = "ComboboxVendaAtacado", Target = "COMBOBOX_VENDA_ATACADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.DATETIME_VENDA_ATACADO", Source = "DatetimeVendaAtacado", Target = "DATETIME_VENDA_ATACADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.SMALL_INT_VENDA_ATACADO", Source = "SmallIntVendaAtacado", Target = "SMALL_INT_VENDA_ATACADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "DCLinxDemoBM.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });

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
	 

	    public Dictionary<string, string> GetComboboxVendaAtacadoValues()
	    {
	    	    return VAREJO.BV.Domains.LX_VENDA_ATACADO.GetValues();
	    }
	    private string _comboboxVendaAtacadoName;
	    [DataMember(IsRequired = false, Name = "ComboboxVendaAtacadoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Combobox Venda Atacado", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ComboboxVendaAtacadoName
	    {
	    	    get { if (this.ComboboxVendaAtacado.IsNull()) { _comboboxVendaAtacadoName = String.Empty; } else { string key = this.ComboboxVendaAtacado.ToString(); var dmValues = this.GetComboboxVendaAtacadoValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _comboboxVendaAtacadoName) _comboboxVendaAtacadoName = domainName; } return _comboboxVendaAtacadoName; } set { _comboboxVendaAtacadoName = value;  }
	    }
	    public Dictionary<string, string> GetComboboxClienteValues()
	    {
	    	    return VAREJO.BV.Domains.LX_CLIENTE.GetValues();
	    }
	    private string _comboboxClienteName;
	    [DataMember(IsRequired = false, Name = "ComboboxClienteName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Combobox Cliente", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ComboboxClienteName
	    {
	    	    get { if (this.ComboboxCliente.IsNull()) { _comboboxClienteName = String.Empty; } else { string key = this.ComboboxCliente.ToString(); var dmValues = this.GetComboboxClienteValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _comboboxClienteName) _comboboxClienteName = domainName; } return _comboboxClienteName; } set { _comboboxClienteName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	//////////////////////// DomainService Class V1 ///////////////////////
	///////////////////////////////////////////////////////////////////////
	[EnableClientAccess()]	
	[DomainIdentifier("ProcessorOverviewWizardExampleDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class WizardExampleDomainService : DomainService, IDataServiceContext 
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

		
	    public WizardExampleDomainService() : this("", null, null) { }
	    public WizardExampleDomainService(string connectionString) : this(connectionString, null, null) { }
	    public WizardExampleDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public WizardExampleDomainService(Linx.Demo.BM.DCLinxDemoBM dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public WizardExampleDomainService(string connectionString, Linx.Demo.BM.DCLinxDemoBM dataContext, Dictionary<string, string> headers) : base() 
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
 	        var _ClienteElements = changeSet.ChangeSetEntries.Where(e => e.Entity is Cliente && e.Entity.GetType().Name == "Cliente" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _ClienteElements)
 	           if (((Cliente)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 
 	        //Remove inconsistent details
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is Venda && e.Entity.GetType().Name == "Venda" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is VendaAtacado && e.Entity.GetType().Name == "VendaAtacado" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
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
	    //Get All LookUpEstado.
	    public IQueryable<LookUpEstado> GetAllLookUpEstado()
	    {
	        return this.GetLookUpEstado(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpEstado By EntitySearch.
	    public IQueryable<LookUpEstado> GetLookUpEstadoByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpEstado(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpEstado.
	    public IQueryable<LookUpEstado> GetLookUpEstado(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "ESTADO" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpEstado";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpEstado));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpEstado> query =  
	
	            (from entity in this.DbContext.ESTADO.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpEstado()		
	            {
	            
                IdEstado = entity.ID_ESTADO
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
	
		

	        if (entityName.InList("VAREJO.BV.WizardExample.Cliente"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "Cliente",
	        			NameSpace = "VAREJO.BV.WizardExample",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "Cliente",
	        			ClearMethodName = "ClearCliente",
	        			QueryMethodName  = "GetPagedCliente",	
	        			CountingMethodName  = "GetCliente" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("VAREJO.BV.WizardExample.Cliente"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("VAREJO.BV.WizardExample.Cliente"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("VAREJO.BV.WizardExample.Cliente", "VAREJO.BV.WizardExample.Venda"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "Venda" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "VAREJO.BV.WizardExample",
	        			HasQuickSearch = false,
	        			ParentClassName = "Cliente",	
	        			DisplayName = "VendawwwwwwwwwwWWWWW",
	        			ClearMethodName = "ClearVenda" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedVenda" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetVenda" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("VAREJO.BV.WizardExample.Venda"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("VAREJO.BV.WizardExample.Venda" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("VAREJO.BV.WizardExample.Cliente", "VAREJO.BV.WizardExample.VendaAtacado"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "VendaAtacado" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "VAREJO.BV.WizardExample",
	        			HasQuickSearch = false,
	        			ParentClassName = "Cliente",	
	        			DisplayName = "VendaAtacadommmmmmmMMMMMM",
	        			ClearMethodName = "ClearVendaAtacado" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedVendaAtacado" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetVendaAtacado" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("VAREJO.BV.WizardExample.VendaAtacado"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("VAREJO.BV.WizardExample.VendaAtacado" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
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

         		    return new string[] { "VAREJO_WizardExampleClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("VAREJO.BV.ClientResources.WizardExampleClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "VAREJO_wizardExampleService", Linx.Tools.AssemblyHelper.ReadResourceContent("VAREJO.BV.ClientResources.wizardExampleService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear Cliente.
	    public IEnumerable<Cliente> ClearCliente()
	    {
	        List<Cliente> result = new List<Cliente>();
	        result.Add(new Cliente());	
			
	        result[0].VendaList = new List<Venda>();
	        ((List<Venda>)result[0].VendaList).Add(new Venda());
			
	        result[0].VendaAtacadoList = new List<VendaAtacado>();
	        ((List<VendaAtacado>)result[0].VendaAtacadoList).Add(new VendaAtacado());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear Venda.
	    public IEnumerable<Venda> ClearVenda()
	    {
	        List<Venda> result = new List<Venda>();
	        result.Add(new Venda());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear VendaAtacado.
	    public IEnumerable<VendaAtacado> ClearVendaAtacado()
	    {
	        List<VendaAtacado> result = new List<VendaAtacado>();
	        result.Add(new VendaAtacado());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    [ClienteQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get Cliente.
	    public IQueryable<Cliente> GetCliente()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetCliente")))
 	        {
 	             AuthorizationResult authorizationResult = (new ClienteQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<Cliente> result = 
	            (from entity0 in this.DbContext.CLIENTE
                  let entity0Al1 = entity0.ESTADO
	            
	            	
	            select new Cliente()		
	            {
	            
                BigIntCliente = entity0.BIG_INT_CLIENTE
                , BitCliente = entity0.BIT_CLIENTE
                , ComboboxCliente = entity0.COMBOBOX_CLIENTE
                , ComboboxClienteName = ((entity0.COMBOBOX_CLIENTE) == 1 ? "CLIENTE 1" : ((entity0.COMBOBOX_CLIENTE) == 2 ? "CLIENTE 2" : ((entity0.COMBOBOX_CLIENTE) == 3 ? "CLIENTE 3" : "")))
                , DatetimeCliente = entity0.DATETIME_CLIENTE
                , DecimalCliente = entity0.DECIMAL_CLIENTE
                , GuidCliente = entity0.GUID_CLIENTE
                , IdCliente = entity0.ID_CLIENTE
                , IdEstado = entity0Al1.ID_ESTADO
                , IntCliente = entity0.INT_CLIENTE
                , SmallIntCliente = entity0.SMALL_INT_CLIENTE
                , StringCliente = entity0.STRING_CLIENTE
			
                ,VendaList = 
	                        (from entity1 in entity0.VENDA_LISTA
                                  let entity1Al2 = entity1.LOJA
                                  let entity1Al1 = entity1.CLIENTE
	                        
	                        	
	                        select new Venda()
	                        {
	                        
                                BigIntVenda = entity1.BIG_INT_VENDA
                                , BitVenda = entity1.BIT_VENDA
                                , ComboboxVenda = entity1.COMBOBOX_VENDA
                                , ComboboxVendaName = ((entity1.COMBOBOX_VENDA) == 1 ? "VENDA 1" : ((entity1.COMBOBOX_VENDA) == 2 ? "VENDA 2" : ((entity1.COMBOBOX_VENDA) == 3 ? "VENDA 3" : "")))
                                , DatetimeVenda = entity1.DATETIME_VENDA
                                , DecimalVenda = entity1.DECIMAL_VENDA
                                , GuidVenda = entity1.GUID_VENDA
                                , IdCliente = entity1Al1.ID_CLIENTE
                                , IdLoja = entity1Al2.ID_LOJA
                                , IdVenda = entity1.ID_VENDA
                                , IntVenda = entity1.INT_VENDA
                                , SmallIntVenda = entity1.SMALL_INT_VENDA
                                , StringVenda = entity1.STRING_VENDA
		
	                        }
	                        )
			
                ,VendaAtacadoList = 
	                        (from entity1 in entity0.VENDA_ATACADO_LISTA
                                  let entity1Al1 = entity1.CLIENTE
	                        
	                        	
	                        select new VendaAtacado()
	                        {
	                        
                                BigIntVendaAtacado = entity1.BIG_INT_VENDA_ATACADO
                                , BitVendaAtacado = entity1.BIT_VENDA_ATACADO
                                , ComboboxVendaAtacado = entity1.COMBOBOX_VENDA_ATACADO
                                , ComboboxVendaAtacadoName = ((entity1.COMBOBOX_VENDA_ATACADO) == 1 ? "VENDA 1" : ((entity1.COMBOBOX_VENDA_ATACADO) == 2 ? "VENDA 2" : ((entity1.COMBOBOX_VENDA_ATACADO) == 3 ? "VENDA 3" : "")))
                                , DatetimeVendaAtacado = entity1.DATETIME_VENDA_ATACADO
                                , DecimalVendaAtacado = entity1.DECIMAL_VENDA_ATACADO
                                , GuidVendaAtacado = entity1.GUID_VENDA_ATACADO
                                , IdCliente = entity1Al1.ID_CLIENTE
                                , IdVendaAtacado = entity1.ID_VENDA_ATACADO
                                , IntVendaAtacado = entity1.INT_VENDA_ATACADO
                                , SmallIntVendaAtacado = entity1.SMALL_INT_VENDA_ATACADO
                                , StringVendaAtacado = entity1.STRING_VENDA_ATACADO
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
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
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendaAtacadoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendaAtacado.
	    public IQueryable<VendaAtacado> GetVendaAtacado()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendaAtacado")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaAtacadoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<VendaAtacado> result = 
	            (from entity0 in this.DbContext.VENDA_ATACADO
                  let entity0Al1 = entity0.CLIENTE
	            
	            	
	            select new VendaAtacado()		
	            {
	            
                BigIntVendaAtacado = entity0.BIG_INT_VENDA_ATACADO
                , BitVendaAtacado = entity0.BIT_VENDA_ATACADO
                , ComboboxVendaAtacado = entity0.COMBOBOX_VENDA_ATACADO
                , ComboboxVendaAtacadoName = ((entity0.COMBOBOX_VENDA_ATACADO) == 1 ? "VENDA 1" : ((entity0.COMBOBOX_VENDA_ATACADO) == 2 ? "VENDA 2" : ((entity0.COMBOBOX_VENDA_ATACADO) == 3 ? "VENDA 3" : "")))
                , DatetimeVendaAtacado = entity0.DATETIME_VENDA_ATACADO
                , DecimalVendaAtacado = entity0.DECIMAL_VENDA_ATACADO
                , GuidVendaAtacado = entity0.GUID_VENDA_ATACADO
                , IdCliente = entity0Al1.ID_CLIENTE
                , IdVendaAtacado = entity0.ID_VENDA_ATACADO
                , IntVendaAtacado = entity0.INT_VENDA_ATACADO
                , SmallIntVendaAtacado = entity0.SMALL_INT_VENDA_ATACADO
                , StringVendaAtacado = entity0.STRING_VENDA_ATACADO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [ClienteQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get ClienteNoAssociations.
	    public IQueryable<Cliente> GetClienteNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetClienteNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new ClienteQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<Cliente> result = 
	            (from entity0 in this.DbContext.CLIENTE
                  let entity0Al1 = entity0.ESTADO
	            
	            	
	            select new Cliente()		
	            {
	            
                BigIntCliente = entity0.BIG_INT_CLIENTE
                , BitCliente = entity0.BIT_CLIENTE
                , ComboboxCliente = entity0.COMBOBOX_CLIENTE
                , ComboboxClienteName = ((entity0.COMBOBOX_CLIENTE) == 1 ? "CLIENTE 1" : ((entity0.COMBOBOX_CLIENTE) == 2 ? "CLIENTE 2" : ((entity0.COMBOBOX_CLIENTE) == 3 ? "CLIENTE 3" : "")))
                , DatetimeCliente = entity0.DATETIME_CLIENTE
                , DecimalCliente = entity0.DECIMAL_CLIENTE
                , GuidCliente = entity0.GUID_CLIENTE
                , IdCliente = entity0.ID_CLIENTE
                , IdEstado = entity0Al1.ID_ESTADO
                , IntCliente = entity0.INT_CLIENTE
                , SmallIntCliente = entity0.SMALL_INT_CLIENTE
                , StringCliente = entity0.STRING_CLIENTE
		
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
			
	
	    [VendaAtacadoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendaAtacadoNoAssociations.
	    public IQueryable<VendaAtacado> GetVendaAtacadoNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendaAtacadoNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaAtacadoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<VendaAtacado> result = 
	            (from entity0 in this.DbContext.VENDA_ATACADO
                  let entity0Al1 = entity0.CLIENTE
	            
	            	
	            select new VendaAtacado()		
	            {
	            
                BigIntVendaAtacado = entity0.BIG_INT_VENDA_ATACADO
                , BitVendaAtacado = entity0.BIT_VENDA_ATACADO
                , ComboboxVendaAtacado = entity0.COMBOBOX_VENDA_ATACADO
                , ComboboxVendaAtacadoName = ((entity0.COMBOBOX_VENDA_ATACADO) == 1 ? "VENDA 1" : ((entity0.COMBOBOX_VENDA_ATACADO) == 2 ? "VENDA 2" : ((entity0.COMBOBOX_VENDA_ATACADO) == 3 ? "VENDA 3" : "")))
                , DatetimeVendaAtacado = entity0.DATETIME_VENDA_ATACADO
                , DecimalVendaAtacado = entity0.DECIMAL_VENDA_ATACADO
                , GuidVendaAtacado = entity0.GUID_VENDA_ATACADO
                , IdCliente = entity0Al1.ID_CLIENTE
                , IdVendaAtacado = entity0.ID_VENDA_ATACADO
                , IntVendaAtacado = entity0.INT_VENDA_ATACADO
                , SmallIntVendaAtacado = entity0.SMALL_INT_VENDA_ATACADO
                , StringVendaAtacado = entity0.STRING_VENDA_ATACADO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	//Add filtering disabled property for CLIENTE
	    	string[] bmDisabledClienteList = this.GetEDM().GetFilteringDisabledList("CLIENTE");
	    	if (bmDisabledClienteList.Length > 0)
	    	{
	
	    		if (bmDisabledClienteList.Contains("CLIENTE.BIG_INT_CLIENTE"))
	    		{
	    			result.Add("Cliente|BigIntCliente");
	    			result.Add("Cliente|CLIENTE.BIG_INT_CLIENTE");
	    		}
	
	    		if (bmDisabledClienteList.Contains("CLIENTE.BIT_CLIENTE"))
	    		{
	    			result.Add("Cliente|BitCliente");
	    			result.Add("Cliente|CLIENTE.BIT_CLIENTE");
	    		}
	
	    		if (bmDisabledClienteList.Contains("CLIENTE.COMBOBOX_CLIENTE"))
	    		{
	    			result.Add("Cliente|ComboboxCliente");
	    			result.Add("Cliente|CLIENTE.COMBOBOX_CLIENTE");
	    		}
	
	    		if (bmDisabledClienteList.Contains("CLIENTE.DATETIME_CLIENTE"))
	    		{
	    			result.Add("Cliente|DatetimeCliente");
	    			result.Add("Cliente|CLIENTE.DATETIME_CLIENTE");
	    		}
	
	    		if (bmDisabledClienteList.Contains("CLIENTE.DECIMAL_CLIENTE"))
	    		{
	    			result.Add("Cliente|DecimalCliente");
	    			result.Add("Cliente|CLIENTE.DECIMAL_CLIENTE");
	    		}
	
	    		if (bmDisabledClienteList.Contains("CLIENTE.GUID_CLIENTE"))
	    		{
	    			result.Add("Cliente|GuidCliente");
	    			result.Add("Cliente|CLIENTE.GUID_CLIENTE");
	    		}
	
	    		if (bmDisabledClienteList.Contains("CLIENTE.ID_CLIENTE"))
	    		{
	    			result.Add("Cliente|IdCliente");
	    			result.Add("Cliente|CLIENTE.ID_CLIENTE");
	    		}
	
	    		if (bmDisabledClienteList.Contains("CLIENTE.INT_CLIENTE"))
	    		{
	    			result.Add("Cliente|IntCliente");
	    			result.Add("Cliente|CLIENTE.INT_CLIENTE");
	    		}
	
	    		if (bmDisabledClienteList.Contains("CLIENTE.SMALL_INT_CLIENTE"))
	    		{
	    			result.Add("Cliente|SmallIntCliente");
	    			result.Add("Cliente|CLIENTE.SMALL_INT_CLIENTE");
	    		}
	
	    		if (bmDisabledClienteList.Contains("CLIENTE.STRING_CLIENTE"))
	    		{
	    			result.Add("Cliente|StringCliente");
	    			result.Add("Cliente|CLIENTE.STRING_CLIENTE");
	    		}
	    	}
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
	    	//Add filtering disabled property for VENDA_ATACADO
	    	string[] bmDisabledVendaAtacadoList = this.GetEDM().GetFilteringDisabledList("VENDA_ATACADO");
	    	if (bmDisabledVendaAtacadoList.Length > 0)
	    	{
	
	    		if (bmDisabledVendaAtacadoList.Contains("VENDA_ATACADO.BIG_INT_VENDA_ATACADO"))
	    		{
	    			result.Add("VendaAtacado|BigIntVendaAtacado");
	    			result.Add("VendaAtacado|VENDA_ATACADO.BIG_INT_VENDA_ATACADO");
	    		}
	
	    		if (bmDisabledVendaAtacadoList.Contains("VENDA_ATACADO.BIT_VENDA_ATACADO"))
	    		{
	    			result.Add("VendaAtacado|BitVendaAtacado");
	    			result.Add("VendaAtacado|VENDA_ATACADO.BIT_VENDA_ATACADO");
	    		}
	
	    		if (bmDisabledVendaAtacadoList.Contains("VENDA_ATACADO.COMBOBOX_VENDA_ATACADO"))
	    		{
	    			result.Add("VendaAtacado|ComboboxVendaAtacado");
	    			result.Add("VendaAtacado|VENDA_ATACADO.COMBOBOX_VENDA_ATACADO");
	    		}
	
	    		if (bmDisabledVendaAtacadoList.Contains("VENDA_ATACADO.DATETIME_VENDA_ATACADO"))
	    		{
	    			result.Add("VendaAtacado|DatetimeVendaAtacado");
	    			result.Add("VendaAtacado|VENDA_ATACADO.DATETIME_VENDA_ATACADO");
	    		}
	
	    		if (bmDisabledVendaAtacadoList.Contains("VENDA_ATACADO.DECIMAL_VENDA_ATACADO"))
	    		{
	    			result.Add("VendaAtacado|DecimalVendaAtacado");
	    			result.Add("VendaAtacado|VENDA_ATACADO.DECIMAL_VENDA_ATACADO");
	    		}
	
	    		if (bmDisabledVendaAtacadoList.Contains("VENDA_ATACADO.GUID_VENDA_ATACADO"))
	    		{
	    			result.Add("VendaAtacado|GuidVendaAtacado");
	    			result.Add("VendaAtacado|VENDA_ATACADO.GUID_VENDA_ATACADO");
	    		}
	
	    		if (bmDisabledVendaAtacadoList.Contains("VENDA_ATACADO.ID_VENDA_ATACADO"))
	    		{
	    			result.Add("VendaAtacado|IdVendaAtacado");
	    			result.Add("VendaAtacado|VENDA_ATACADO.ID_VENDA_ATACADO");
	    		}
	
	    		if (bmDisabledVendaAtacadoList.Contains("VENDA_ATACADO.INT_VENDA_ATACADO"))
	    		{
	    			result.Add("VendaAtacado|IntVendaAtacado");
	    			result.Add("VendaAtacado|VENDA_ATACADO.INT_VENDA_ATACADO");
	    		}
	
	    		if (bmDisabledVendaAtacadoList.Contains("VENDA_ATACADO.SMALL_INT_VENDA_ATACADO"))
	    		{
	    			result.Add("VendaAtacado|SmallIntVendaAtacado");
	    			result.Add("VendaAtacado|VENDA_ATACADO.SMALL_INT_VENDA_ATACADO");
	    		}
	
	    		if (bmDisabledVendaAtacadoList.Contains("VENDA_ATACADO.STRING_VENDA_ATACADO"))
	    		{
	    			result.Add("VendaAtacado|StringVendaAtacado");
	    			result.Add("VendaAtacado|VENDA_ATACADO.STRING_VENDA_ATACADO");
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
	    //Get Cliente By EntitySearchId.
	    public IQueryable<Cliente> GetClienteByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetClienteByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get Venda By EntitySearchId.
	    public IQueryable<Venda> GetVendaByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetVendaByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get VendaAtacado By EntitySearchId.
	    public IQueryable<VendaAtacado> GetVendaAtacadoByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetVendaAtacadoByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get Cliente By EntitySearchId.
	    public IQueryable<Cliente> GetClienteByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetClienteByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get Venda By EntitySearchId.
	    public IQueryable<Venda> GetVendaByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetVendaByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get VendaAtacado By EntitySearchId.
	    public IQueryable<VendaAtacado> GetVendaAtacadoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetVendaAtacadoByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get Cliente By Example.
	    [Ignore]
	    public IQueryable<Cliente> GetClienteByExample(Cliente entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetClienteByEntitySearch(queryAnalysis);
	    }
			
	    //Get Venda By Example.
	    [Ignore]
	    public IQueryable<Venda> GetVendaByExample(Venda entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetVendaByEntitySearch(queryAnalysis);
	    }
			
	    //Get VendaAtacado By Example.
	    [Ignore]
	    public IQueryable<VendaAtacado> GetVendaAtacadoByExample(VendaAtacado entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetVendaAtacadoByEntitySearch(queryAnalysis);
	    }
			
	    //Get Cliente By Example.
	    [Ignore]
	    public IQueryable<Cliente> GetClienteByExampleNoAssociations(Cliente entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetClienteByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get Venda By Example.
	    [Ignore]
	    public IQueryable<Venda> GetVendaByExampleNoAssociations(Venda entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetVendaByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get VendaAtacado By Example.
	    [Ignore]
	    public IQueryable<VendaAtacado> GetVendaAtacadoByExampleNoAssociations(VendaAtacado entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetVendaAtacadoByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public Cliente GetClienteByKey(Int32 idCliente)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("Cliente");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdCliente"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idCliente));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetClienteByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


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
	    public VendaAtacado GetVendaAtacadoByKey(Int32 idVendaAtacado)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("VendaAtacado");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdVendaAtacado"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idVendaAtacado));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetVendaAtacadoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    [ClienteQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get ClienteByEntitySearch.
	    public IQueryable<Cliente> GetClienteByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetClienteByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new ClienteQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Cliente));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<Cliente> result = 
	            (from entity0 in this.DbContext.CLIENTE.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.ESTADO
	            
	            	
	            select new Cliente()		
	            {
	            
                BigIntCliente = entity0.BIG_INT_CLIENTE
                , BitCliente = entity0.BIT_CLIENTE
                , ComboboxCliente = entity0.COMBOBOX_CLIENTE
                , ComboboxClienteName = ((entity0.COMBOBOX_CLIENTE) == 1 ? "CLIENTE 1" : ((entity0.COMBOBOX_CLIENTE) == 2 ? "CLIENTE 2" : ((entity0.COMBOBOX_CLIENTE) == 3 ? "CLIENTE 3" : "")))
                , DatetimeCliente = entity0.DATETIME_CLIENTE
                , DecimalCliente = entity0.DECIMAL_CLIENTE
                , GuidCliente = entity0.GUID_CLIENTE
                , IdCliente = entity0.ID_CLIENTE
                , IdEstado = entity0Al1.ID_ESTADO
                , IntCliente = entity0.INT_CLIENTE
                , SmallIntCliente = entity0.SMALL_INT_CLIENTE
                , StringCliente = entity0.STRING_CLIENTE
			
                ,VendaList = 
	                        (from entity1 in entity0.VENDA_LISTA
                                  let entity1Al2 = entity1.LOJA
                                  let entity1Al1 = entity1.CLIENTE
	                        
	                        	
	                        select new Venda()
	                        {
	                        
                                BigIntVenda = entity1.BIG_INT_VENDA
                                , BitVenda = entity1.BIT_VENDA
                                , ComboboxVenda = entity1.COMBOBOX_VENDA
                                , ComboboxVendaName = ((entity1.COMBOBOX_VENDA) == 1 ? "VENDA 1" : ((entity1.COMBOBOX_VENDA) == 2 ? "VENDA 2" : ((entity1.COMBOBOX_VENDA) == 3 ? "VENDA 3" : "")))
                                , DatetimeVenda = entity1.DATETIME_VENDA
                                , DecimalVenda = entity1.DECIMAL_VENDA
                                , GuidVenda = entity1.GUID_VENDA
                                , IdCliente = entity1Al1.ID_CLIENTE
                                , IdLoja = entity1Al2.ID_LOJA
                                , IdVenda = entity1.ID_VENDA
                                , IntVenda = entity1.INT_VENDA
                                , SmallIntVenda = entity1.SMALL_INT_VENDA
                                , StringVenda = entity1.STRING_VENDA
		
	                        }
	                        )
			
                ,VendaAtacadoList = 
	                        (from entity1 in entity0.VENDA_ATACADO_LISTA
                                  let entity1Al1 = entity1.CLIENTE
	                        
	                        	
	                        select new VendaAtacado()
	                        {
	                        
                                BigIntVendaAtacado = entity1.BIG_INT_VENDA_ATACADO
                                , BitVendaAtacado = entity1.BIT_VENDA_ATACADO
                                , ComboboxVendaAtacado = entity1.COMBOBOX_VENDA_ATACADO
                                , ComboboxVendaAtacadoName = ((entity1.COMBOBOX_VENDA_ATACADO) == 1 ? "VENDA 1" : ((entity1.COMBOBOX_VENDA_ATACADO) == 2 ? "VENDA 2" : ((entity1.COMBOBOX_VENDA_ATACADO) == 3 ? "VENDA 3" : "")))
                                , DatetimeVendaAtacado = entity1.DATETIME_VENDA_ATACADO
                                , DecimalVendaAtacado = entity1.DECIMAL_VENDA_ATACADO
                                , GuidVendaAtacado = entity1.GUID_VENDA_ATACADO
                                , IdCliente = entity1Al1.ID_CLIENTE
                                , IdVendaAtacado = entity1.ID_VENDA_ATACADO
                                , IntVendaAtacado = entity1.INT_VENDA_ATACADO
                                , SmallIntVendaAtacado = entity1.SMALL_INT_VENDA_ATACADO
                                , StringVendaAtacado = entity1.STRING_VENDA_ATACADO
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
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
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendaAtacadoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendaAtacadoByEntitySearch.
	    public IQueryable<VendaAtacado> GetVendaAtacadoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendaAtacadoByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaAtacadoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(VendaAtacado));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<VendaAtacado> result = 
	            (from entity0 in this.DbContext.VENDA_ATACADO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.CLIENTE
	            
	            	
	            select new VendaAtacado()		
	            {
	            
                BigIntVendaAtacado = entity0.BIG_INT_VENDA_ATACADO
                , BitVendaAtacado = entity0.BIT_VENDA_ATACADO
                , ComboboxVendaAtacado = entity0.COMBOBOX_VENDA_ATACADO
                , ComboboxVendaAtacadoName = ((entity0.COMBOBOX_VENDA_ATACADO) == 1 ? "VENDA 1" : ((entity0.COMBOBOX_VENDA_ATACADO) == 2 ? "VENDA 2" : ((entity0.COMBOBOX_VENDA_ATACADO) == 3 ? "VENDA 3" : "")))
                , DatetimeVendaAtacado = entity0.DATETIME_VENDA_ATACADO
                , DecimalVendaAtacado = entity0.DECIMAL_VENDA_ATACADO
                , GuidVendaAtacado = entity0.GUID_VENDA_ATACADO
                , IdCliente = entity0Al1.ID_CLIENTE
                , IdVendaAtacado = entity0.ID_VENDA_ATACADO
                , IntVendaAtacado = entity0.INT_VENDA_ATACADO
                , SmallIntVendaAtacado = entity0.SMALL_INT_VENDA_ATACADO
                , StringVendaAtacado = entity0.STRING_VENDA_ATACADO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [ClienteQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get ClienteByEntitySearchNoAssociations.
	    public IQueryable<Cliente> GetClienteByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetClienteByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new ClienteQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Cliente));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<Cliente> result = 
	            (from entity0 in this.DbContext.CLIENTE.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.ESTADO
	            
	            	
	            select new Cliente()		
	            {
	            
                BigIntCliente = entity0.BIG_INT_CLIENTE
                , BitCliente = entity0.BIT_CLIENTE
                , ComboboxCliente = entity0.COMBOBOX_CLIENTE
                , ComboboxClienteName = ((entity0.COMBOBOX_CLIENTE) == 1 ? "CLIENTE 1" : ((entity0.COMBOBOX_CLIENTE) == 2 ? "CLIENTE 2" : ((entity0.COMBOBOX_CLIENTE) == 3 ? "CLIENTE 3" : "")))
                , DatetimeCliente = entity0.DATETIME_CLIENTE
                , DecimalCliente = entity0.DECIMAL_CLIENTE
                , GuidCliente = entity0.GUID_CLIENTE
                , IdCliente = entity0.ID_CLIENTE
                , IdEstado = entity0Al1.ID_ESTADO
                , IntCliente = entity0.INT_CLIENTE
                , SmallIntCliente = entity0.SMALL_INT_CLIENTE
                , StringCliente = entity0.STRING_CLIENTE
		
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
			
	
	    [VendaAtacadoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendaAtacadoByEntitySearchNoAssociations.
	    public IQueryable<VendaAtacado> GetVendaAtacadoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendaAtacadoByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaAtacadoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(VendaAtacado));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<VendaAtacado> result = 
	            (from entity0 in this.DbContext.VENDA_ATACADO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.CLIENTE
	            
	            	
	            select new VendaAtacado()		
	            {
	            
                BigIntVendaAtacado = entity0.BIG_INT_VENDA_ATACADO
                , BitVendaAtacado = entity0.BIT_VENDA_ATACADO
                , ComboboxVendaAtacado = entity0.COMBOBOX_VENDA_ATACADO
                , ComboboxVendaAtacadoName = ((entity0.COMBOBOX_VENDA_ATACADO) == 1 ? "VENDA 1" : ((entity0.COMBOBOX_VENDA_ATACADO) == 2 ? "VENDA 2" : ((entity0.COMBOBOX_VENDA_ATACADO) == 3 ? "VENDA 3" : "")))
                , DatetimeVendaAtacado = entity0.DATETIME_VENDA_ATACADO
                , DecimalVendaAtacado = entity0.DECIMAL_VENDA_ATACADO
                , GuidVendaAtacado = entity0.GUID_VENDA_ATACADO
                , IdCliente = entity0Al1.ID_CLIENTE
                , IdVendaAtacado = entity0.ID_VENDA_ATACADO
                , IntVendaAtacado = entity0.INT_VENDA_ATACADO
                , SmallIntVendaAtacado = entity0.SMALL_INT_VENDA_ATACADO
                , StringVendaAtacado = entity0.STRING_VENDA_ATACADO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendaQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendaParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<VendaParentComposition> GetVendaParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendaParentCompositionByEntitySearchNoAssociations")))
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "CLIENTE", "VENDA", "CLIENTE", typeof(VendaParentComposition), typeof(VendaAtacado));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<VendaParentComposition> result = 
	            (from entity0 in this.DbContext.VENDA.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.LOJA
                  let entity0Al1 = entity0.CLIENTE
	            
	            	
	            select new VendaParentComposition()		
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
                //Cliente Properties.
                , BigIntCliente = entity0.CLIENTE.BIG_INT_CLIENTE
                , BitCliente = entity0.CLIENTE.BIT_CLIENTE
                , ComboboxCliente = entity0.CLIENTE.COMBOBOX_CLIENTE
                , ComboboxClienteName = ((entity0.CLIENTE.COMBOBOX_CLIENTE) == 1 ? "CLIENTE 1" : ((entity0.CLIENTE.COMBOBOX_CLIENTE) == 2 ? "CLIENTE 2" : ((entity0.CLIENTE.COMBOBOX_CLIENTE) == 3 ? "CLIENTE 3" : "")))
                , DatetimeCliente = entity0.CLIENTE.DATETIME_CLIENTE
                , DecimalCliente = entity0.CLIENTE.DECIMAL_CLIENTE
                , GuidCliente = entity0.CLIENTE.GUID_CLIENTE
                , IdEstado = entity0.CLIENTE.ESTADO.ID_ESTADO
                , IntCliente = entity0.CLIENTE.INT_CLIENTE
                , SmallIntCliente = entity0.CLIENTE.SMALL_INT_CLIENTE
                , StringCliente = entity0.CLIENTE.STRING_CLIENTE
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendaAtacadoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendaAtacadoParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<VendaAtacadoParentComposition> GetVendaAtacadoParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendaAtacadoParentCompositionByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaAtacadoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "CLIENTE", "VENDA_ATACADO", "CLIENTE", typeof(VendaAtacadoParentComposition), typeof(Venda));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<VendaAtacadoParentComposition> result = 
	            (from entity0 in this.DbContext.VENDA_ATACADO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.CLIENTE
	            
	            	
	            select new VendaAtacadoParentComposition()		
	            {
	            
                BigIntVendaAtacado = entity0.BIG_INT_VENDA_ATACADO
                , BitVendaAtacado = entity0.BIT_VENDA_ATACADO
                , ComboboxVendaAtacado = entity0.COMBOBOX_VENDA_ATACADO
                , ComboboxVendaAtacadoName = ((entity0.COMBOBOX_VENDA_ATACADO) == 1 ? "VENDA 1" : ((entity0.COMBOBOX_VENDA_ATACADO) == 2 ? "VENDA 2" : ((entity0.COMBOBOX_VENDA_ATACADO) == 3 ? "VENDA 3" : "")))
                , DatetimeVendaAtacado = entity0.DATETIME_VENDA_ATACADO
                , DecimalVendaAtacado = entity0.DECIMAL_VENDA_ATACADO
                , GuidVendaAtacado = entity0.GUID_VENDA_ATACADO
                , IdCliente = entity0Al1.ID_CLIENTE
                , IdVendaAtacado = entity0.ID_VENDA_ATACADO
                , IntVendaAtacado = entity0.INT_VENDA_ATACADO
                , SmallIntVendaAtacado = entity0.SMALL_INT_VENDA_ATACADO
                , StringVendaAtacado = entity0.STRING_VENDA_ATACADO
                //Cliente Properties.
                , BigIntCliente = entity0.CLIENTE.BIG_INT_CLIENTE
                , BitCliente = entity0.CLIENTE.BIT_CLIENTE
                , ComboboxCliente = entity0.CLIENTE.COMBOBOX_CLIENTE
                , ComboboxClienteName = ((entity0.CLIENTE.COMBOBOX_CLIENTE) == 1 ? "CLIENTE 1" : ((entity0.CLIENTE.COMBOBOX_CLIENTE) == 2 ? "CLIENTE 2" : ((entity0.CLIENTE.COMBOBOX_CLIENTE) == 3 ? "CLIENTE 3" : "")))
                , DatetimeCliente = entity0.CLIENTE.DATETIME_CLIENTE
                , DecimalCliente = entity0.CLIENTE.DECIMAL_CLIENTE
                , GuidCliente = entity0.CLIENTE.GUID_CLIENTE
                , IdEstado = entity0.CLIENTE.ESTADO.ID_ESTADO
                , IntCliente = entity0.CLIENTE.INT_CLIENTE
                , SmallIntCliente = entity0.CLIENTE.SMALL_INT_CLIENTE
                , StringCliente = entity0.CLIENTE.STRING_CLIENTE
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    [ClienteQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedCliente.
	    public IQueryable<Cliente> GetPagedCliente(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedCliente")))
 	        {
 	             AuthorizationResult authorizationResult = (new ClienteQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Cliente));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<Cliente> result = 
	            (from entity0 in this.DbContext.CLIENTE.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.ESTADO
                orderby entity0.ID_CLIENTE ascending
	            
	            	
	            select new Cliente()		
	            {
	            
                BigIntCliente = entity0.BIG_INT_CLIENTE
                , BitCliente = entity0.BIT_CLIENTE
                , ComboboxCliente = entity0.COMBOBOX_CLIENTE
                , ComboboxClienteName = ((entity0.COMBOBOX_CLIENTE) == 1 ? "CLIENTE 1" : ((entity0.COMBOBOX_CLIENTE) == 2 ? "CLIENTE 2" : ((entity0.COMBOBOX_CLIENTE) == 3 ? "CLIENTE 3" : "")))
                , DatetimeCliente = entity0.DATETIME_CLIENTE
                , DecimalCliente = entity0.DECIMAL_CLIENTE
                , GuidCliente = entity0.GUID_CLIENTE
                , IdCliente = entity0.ID_CLIENTE
                , IdEstado = entity0Al1.ID_ESTADO
                , IntCliente = entity0.INT_CLIENTE
                , SmallIntCliente = entity0.SMALL_INT_CLIENTE
                , StringCliente = entity0.STRING_CLIENTE
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
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
			
	
	    [VendaAtacadoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedVendaAtacado.
	    public IQueryable<VendaAtacado> GetPagedVendaAtacado(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedVendaAtacado")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaAtacadoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(VendaAtacado));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<VendaAtacado> result = 
	            (from entity0 in this.DbContext.VENDA_ATACADO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.CLIENTE
                orderby entity0.ID_VENDA_ATACADO ascending
	            
	            	
	            select new VendaAtacado()		
	            {
	            
                BigIntVendaAtacado = entity0.BIG_INT_VENDA_ATACADO
                , BitVendaAtacado = entity0.BIT_VENDA_ATACADO
                , ComboboxVendaAtacado = entity0.COMBOBOX_VENDA_ATACADO
                , ComboboxVendaAtacadoName = ((entity0.COMBOBOX_VENDA_ATACADO) == 1 ? "VENDA 1" : ((entity0.COMBOBOX_VENDA_ATACADO) == 2 ? "VENDA 2" : ((entity0.COMBOBOX_VENDA_ATACADO) == 3 ? "VENDA 3" : "")))
                , DatetimeVendaAtacado = entity0.DATETIME_VENDA_ATACADO
                , DecimalVendaAtacado = entity0.DECIMAL_VENDA_ATACADO
                , GuidVendaAtacado = entity0.GUID_VENDA_ATACADO
                , IdCliente = entity0Al1.ID_CLIENTE
                , IdVendaAtacado = entity0.ID_VENDA_ATACADO
                , IntVendaAtacado = entity0.INT_VENDA_ATACADO
                , SmallIntVendaAtacado = entity0.SMALL_INT_VENDA_ATACADO
                , StringVendaAtacado = entity0.STRING_VENDA_ATACADO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetClienteCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Cliente));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.CLIENTE.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.ESTADO
	            
	            select 1
	            ).Count();	
		
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
	    public int GetVendaAtacadoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(VendaAtacado));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.VENDA_ATACADO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.CLIENTE
	            
	            select 1
	            ).Count();	
		
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    [ClienteUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update Cliente.
	    public void UpdateCliente(Cliente entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateCliente")))
 	        {
 	             AuthorizationResult authorizationResult = (new ClienteUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    [ClienteInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert Cliente.
	    public void InsertCliente(Cliente entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertCliente")))
 	        {
 	             AuthorizationResult authorizationResult = (new ClienteInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    [ClienteDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete Cliente.
	    public void DeleteCliente(Cliente entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteCliente")))
 	        {
 	             AuthorizationResult authorizationResult = (new ClienteDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
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

	
	        if (entity.Cliente.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.Cliente) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.Cliente); 	
	            

	
	        }
	
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

	
	        if (entity.Cliente.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.Cliente) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.Cliente);
	            

	
	        }
	
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

	
	        if (entity.Cliente.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.Cliente) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.Cliente);
	            

	
	        }

	
	    }
		
			
	    [VendaAtacadoUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update VendaAtacado.
	    public void UpdateVendaAtacado(VendaAtacado entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateVendaAtacado")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaAtacadoUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.Cliente.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.Cliente) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.Cliente); 	
	            

	
	        }
	
	    }

	    [VendaAtacadoInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert VendaAtacado.
	    public void InsertVendaAtacado(VendaAtacado entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertVendaAtacado")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaAtacadoInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.Cliente.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.Cliente) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.Cliente);
	            

	
	        }
	
	    }

	    [VendaAtacadoDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete VendaAtacado.
	    public void DeleteVendaAtacado(VendaAtacado entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteVendaAtacado")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaAtacadoDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.Cliente.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.Cliente) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.Cliente);
	            

	
	        }

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}