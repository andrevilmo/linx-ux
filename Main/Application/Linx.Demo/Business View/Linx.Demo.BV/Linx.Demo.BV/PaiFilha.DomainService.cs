					
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
using LINXDEMO.BM;

namespace Linx.Demo.BV.PaiFilha
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="CLIENTE.ID_CLIENTE", IsUpdatable=false, EdmName="LINXDEMO.BM.BaseTeste")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[Cliente,Cliente.Venda,Venda.VendaItem,Cliente.VendaAtacado];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[];EdmEntityName[CLIENTE];EntityRelations[ESTADO(ESTADO)#PAIS(PAIS)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "Cliente")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Demo.BV.PaiFilha.Cliente")]
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
		

	    public virtual void FillDetails(PaiFilhaDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
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
	         foreach(Venda detail in this.VendaList)
	         {
	             detail.FillDetails(context, serializedEntitySearch, jEntitySearch, viewNames, take);
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
 	              ((Venda)detail.Entity).AdjustHierarchyForSaving(detail, changeSet);
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
	    partial void OnBigIntClienteChanging(System.Nullable<long> value);
	    partial void OnBigIntClienteChanged();

	    private System.Nullable<long> _BigIntCliente;

	    [DataMember(Name = "BigIntCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Big Int Cliente", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[CLIENTE.BIG_INT_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.BIG_INT_CLIENTE")]
	    public System.Nullable<long> BigIntCliente
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
	    partial void OnBitClienteChanging(System.Nullable<bool> value);
	    partial void OnBitClienteChanged();

	    private System.Nullable<bool> _BitCliente;

	    [DataMember(Name = "BitCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bit Cliente", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[CLIENTE.BIT_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.BIT_CLIENTE")]
	    public System.Nullable<bool> BitCliente
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
	    partial void OnComboboxClienteChanging(byte value);
	    partial void OnComboboxClienteChanged();

	    private byte _ComboboxCliente;

	    [DataMember(IsRequired = true, Name = "ComboboxCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Cliente", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_CLIENTE];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[CLIENTE.COMBOBOX_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.COMBOBOX_CLIENTE")]
	    public byte ComboboxCliente
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
	    partial void OnDatetimeClienteChanging(System.Nullable<DateTime> value);
	    partial void OnDatetimeClienteChanged();

	    private System.Nullable<DateTime> _DatetimeCliente;

	    [DataMember(Name = "DatetimeCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Datetime Cliente", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[CLIENTE.DATETIME_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.DATETIME_CLIENTE")]
	    public System.Nullable<DateTime> DatetimeCliente
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
	    partial void OnDecimalClienteChanging(System.Nullable<decimal> value);
	    partial void OnDecimalClienteChanged();

	    private System.Nullable<decimal> _DecimalCliente;

	    [DataMember(Name = "DecimalCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal Cliente", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[CLIENTE.DECIMAL_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.DECIMAL_CLIENTE")]
	    public System.Nullable<decimal> DecimalCliente
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
	    partial void OnGuidClienteChanging(System.Nullable<Guid> value);
	    partial void OnGuidClienteChanged();

	    private System.Nullable<Guid> _GuidCliente;

	    [DataMember(Name = "GuidCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Guid Cliente", Description="", Order = 6, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[CLIENTE.GUID_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.GUID_CLIENTE")]
	    public System.Nullable<Guid> GuidCliente
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
	    partial void OnIdClienteChanging(int value);
	    partial void OnIdClienteChanged();

	    private int _IdCliente;

	    [DataMember(IsRequired = true, Name = "IdCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Cliente", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[CLIENTE.ID_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.ID_CLIENTE")]
	    public int IdCliente
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
	    partial void OnIdEstadoChanging(System.Nullable<int> value);
	    partial void OnIdEstadoChanged();

	    private System.Nullable<int> _IdEstado;

	    [DataMember(Name = "IdEstado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Estado", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpEstado];LookUpTitle[Seleção de (Id Estado)];LookUpQuery[executeLookUpEstado];LookUpFinalize[finalizeLookUpEstado];LookUpDisplayColumns[{\"IdEstado\" : \"Id Estado\", \"StringEstado\" : \"String Estado\"}];LookUpColumns[{\"IdEstado\" : true, \"StringEstado\" : true}];FilterDataKey[CLIENTE.ESTADO.ID_ESTADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<int>#IdEstado#true##10:0##Id Estado#0#true##::LookUpEstado##false#false#ESTADO#ESTADO#Linx.Demo.BV.PaiFilha#IQueryable###true#false", EdmKey="CLIENTE.ESTADO.ID_ESTADO")]
	    public System.Nullable<int> IdEstado
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
	    partial void OnIntClienteChanging(System.Nullable<int> value);
	    partial void OnIntClienteChanged();

	    private System.Nullable<int> _IntCliente;

	    [DataMember(Name = "IntCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int Cliente", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[CLIENTE.INT_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.INT_CLIENTE")]
	    public System.Nullable<int> IntCliente
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
	    partial void OnSmallIntClienteChanging(System.Nullable<short> value);
	    partial void OnSmallIntClienteChanged();

	    private System.Nullable<short> _SmallIntCliente;

	    [DataMember(Name = "SmallIntCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Small Int Cliente", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[CLIENTE.SMALL_INT_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.SMALL_INT_CLIENTE")]
	    public System.Nullable<short> SmallIntCliente
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
	    partial void OnStringClienteChanging(string value);
	    partial void OnStringClienteChanged();

	    private string _StringCliente;

	    [DataMember(Name = "StringCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Cliente", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[CLIENTE.STRING_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.STRING_CLIENTE")]
	    public string StringCliente
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
	    //Extensibility Partial Method Definitions For StringEstado
	    partial void OnStringEstadoChanging(System.Nullable<string> value);
	    partial void OnStringEstadoChanged();

	    private System.Nullable<string> _StringEstado;

	    [DataMember(Name = "StringEstado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Estado", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpEstado];LookUpTitle[Seleção de (String Estado)];LookUpQuery[executeLookUpEstado];LookUpFinalize[finalizeLookUpEstado];LookUpDisplayColumns[{\"IdEstado\" : \"Id Estado\", \"StringEstado\" : \"String Estado\"}];LookUpColumns[{\"IdEstado\" : true, \"StringEstado\" : true}];FilterDataKey[CLIENTE.ESTADO.STRING_ESTADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<string>#StringEstado#false##50:0##String Estado#1#true##::LookUpEstado##false#false#ESTADO#ESTADO#Linx.Demo.BV.PaiFilha#IQueryable###true#false", EdmKey="CLIENTE.ESTADO.STRING_ESTADO")]
	    public System.Nullable<string> StringEstado
	    {
	    	    get
	    	    {
	    	          return _StringEstado;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringEstado != value)
	    	          {
	    	              this.ValidateProperty("StringEstado", value);
	    	              this.OnStringEstadoChanging(value);
	    	              this.RaiseDataMemberChanging("StringEstado");
	    	              this._StringEstado = value;
	    	              this.RaiseDataMemberChanged("StringEstado");
	    	              this.OnStringEstadoChanged();
	    	          }
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
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "BaseTeste.CLIENTE").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(LINXDEMO.BM.CLIENTE), QualifiedEntitySetName = "BaseTeste.CLIENTE" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.ID_CLIENTE", Source = "IdCliente", Target = "ID_CLIENTE", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "BaseTeste.CLIENTE", RelationPropertyName = "CLIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.BIT_CLIENTE", Source = "BitCliente", Target = "BIT_CLIENTE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.CLIENTE", RelationPropertyName = "CLIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.INT_CLIENTE", Source = "IntCliente", Target = "INT_CLIENTE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.CLIENTE", RelationPropertyName = "CLIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.GUID_CLIENTE", Source = "GuidCliente", Target = "GUID_CLIENTE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.CLIENTE", RelationPropertyName = "CLIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.STRING_CLIENTE", Source = "StringCliente", Target = "STRING_CLIENTE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.CLIENTE", RelationPropertyName = "CLIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.BIG_INT_CLIENTE", Source = "BigIntCliente", Target = "BIG_INT_CLIENTE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.CLIENTE", RelationPropertyName = "CLIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.DECIMAL_CLIENTE", Source = "DecimalCliente", Target = "DECIMAL_CLIENTE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.CLIENTE", RelationPropertyName = "CLIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.COMBOBOX_CLIENTE", Source = "ComboboxCliente", Target = "COMBOBOX_CLIENTE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.CLIENTE", RelationPropertyName = "CLIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.DATETIME_CLIENTE", Source = "DatetimeCliente", Target = "DATETIME_CLIENTE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.CLIENTE", RelationPropertyName = "CLIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.ESTADO.ID_ESTADO", Source = "IdEstado", Target = "ID_ESTADO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BaseTeste.ESTADO", RelationPropertyName = "ESTADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.SMALL_INT_CLIENTE", Source = "SmallIntCliente", Target = "SMALL_INT_CLIENTE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.CLIENTE", RelationPropertyName = "CLIENTE" });

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
	    	    return Linx.Demo.BV.Domains.LX_CLIENTE.GetValues();
	    }
	    private string _comboboxClienteName;
	    [DataMember(IsRequired = false, Name = "ComboboxClienteName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Combobox Cliente", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
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

		

	[LinxPublicationView(PrimaryKeys="VENDA.ID_VENDA", IsUpdatable=false, EdmName="LINXDEMO.BM.BaseTeste")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Venda];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#.VENDA_LISTA as #Alias#];EdmEntityName[VENDA];EntityRelations[CLIENTE(CLIENTE)#ESTADO(ESTADO)#PAIS(PAIS)#LOJA(LOJA)];EdmParentEntityName[CLIENTE];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "Venda")]
	[Serializable()]
	public partial class Venda : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(PaiFilhaDomainService context)
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
		

	    public virtual void FillDetails(PaiFilhaDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
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
	    partial void OnBigIntVendaChanging(System.Nullable<long> value);
	    partial void OnBigIntVendaChanged();

	    private System.Nullable<long> _BigIntVenda;

	    [DataMember(Name = "BigIntVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Big Int Venda", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.BIG_INT_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.BIG_INT_VENDA")]
	    public System.Nullable<long> BigIntVenda
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
	    partial void OnBitVendaChanging(System.Nullable<bool> value);
	    partial void OnBitVendaChanged();

	    private System.Nullable<bool> _BitVenda;

	    [DataMember(Name = "BitVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bit Venda", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.BIT_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.BIT_VENDA")]
	    public System.Nullable<bool> BitVenda
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
	    partial void OnComboboxVendaChanging(byte value);
	    partial void OnComboboxVendaChanged();

	    private byte _ComboboxVenda;

	    [DataMember(IsRequired = true, Name = "ComboboxVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Venda", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_VENDA];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.COMBOBOX_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.COMBOBOX_VENDA")]
	    public byte ComboboxVenda
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
	    partial void OnDatetimeVendaChanging(System.Nullable<DateTime> value);
	    partial void OnDatetimeVendaChanged();

	    private System.Nullable<DateTime> _DatetimeVenda;

	    [DataMember(Name = "DatetimeVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Datetime Venda", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.DATETIME_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.DATETIME_VENDA")]
	    public System.Nullable<DateTime> DatetimeVenda
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
	    partial void OnDecimalVendaChanging(System.Nullable<decimal> value);
	    partial void OnDecimalVendaChanged();

	    private System.Nullable<decimal> _DecimalVenda;

	    [DataMember(Name = "DecimalVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal Venda", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.DECIMAL_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.DECIMAL_VENDA")]
	    public System.Nullable<decimal> DecimalVenda
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
	    partial void OnGuidVendaChanging(System.Nullable<Guid> value);
	    partial void OnGuidVendaChanged();

	    private System.Nullable<Guid> _GuidVenda;

	    [DataMember(Name = "GuidVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Guid Venda", Description="", Order = 6, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.GUID_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.GUID_VENDA")]
	    public System.Nullable<Guid> GuidVenda
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
	    partial void OnIdClienteChanging(System.Nullable<int> value);
	    partial void OnIdClienteChanged();

	    private System.Nullable<int> _IdCliente;

	    [DataMember(Name = "IdCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Cliente", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.CLIENTE.ID_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.CLIENTE.ID_CLIENTE")]
	    public System.Nullable<int> IdCliente
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
	    partial void OnIdLojaChanging(System.Nullable<int> value);
	    partial void OnIdLojaChanged();

	    private System.Nullable<int> _IdLoja;

	    [DataMember(Name = "IdLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Loja", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpLoja];LookUpTitle[Seleção de (Id Loja)];LookUpQuery[executeLookUpLoja];LookUpFinalize[finalizeLookUpLoja];LookUpDisplayColumns[{\"IdLoja\" : \"Id Loja\"}];LookUpColumns[{\"IdLoja\" : true}];FilterDataKey[VENDA.LOJA.ID_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<int>#IdLoja#true##10:0##Id Loja#0#true##::LookUpLoja##false#false#LOJA#LOJA#Linx.Demo.BV.PaiFilha#IQueryable###true#false", EdmKey="VENDA.LOJA.ID_LOJA")]
	    public System.Nullable<int> IdLoja
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
	    partial void OnIdVendaChanging(int value);
	    partial void OnIdVendaChanged();

	    private int _IdVenda;

	    [DataMember(IsRequired = true, Name = "IdVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Venda", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.ID_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.ID_VENDA")]
	    public int IdVenda
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
	    partial void OnIntVendaChanging(System.Nullable<int> value);
	    partial void OnIntVendaChanged();

	    private System.Nullable<int> _IntVenda;

	    [DataMember(Name = "IntVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int Venda", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.INT_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.INT_VENDA")]
	    public System.Nullable<int> IntVenda
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
	    partial void OnSmallIntVendaChanging(System.Nullable<short> value);
	    partial void OnSmallIntVendaChanged();

	    private System.Nullable<short> _SmallIntVenda;

	    [DataMember(Name = "SmallIntVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Small Int Venda", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.SMALL_INT_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.SMALL_INT_VENDA")]
	    public System.Nullable<short> SmallIntVenda
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
	    partial void OnStringVendaChanging(string value);
	    partial void OnStringVendaChanged();

	    private string _StringVenda;

	    [DataMember(Name = "StringVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Venda", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.STRING_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.STRING_VENDA")]
	    public string StringVenda
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
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "BaseTeste.VENDA").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(LINXDEMO.BM.VENDA), QualifiedEntitySetName = "BaseTeste.VENDA" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.ID_VENDA", Source = "IdVenda", Target = "ID_VENDA", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.BIT_VENDA", Source = "BitVenda", Target = "BIT_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.INT_VENDA", Source = "IntVenda", Target = "INT_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.GUID_VENDA", Source = "GuidVenda", Target = "GUID_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.LOJA.ID_LOJA", Source = "IdLoja", Target = "ID_LOJA", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BaseTeste.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.STRING_VENDA", Source = "StringVenda", Target = "STRING_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.BIG_INT_VENDA", Source = "BigIntVenda", Target = "BIG_INT_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.DECIMAL_VENDA", Source = "DecimalVenda", Target = "DECIMAL_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.COMBOBOX_VENDA", Source = "ComboboxVenda", Target = "COMBOBOX_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.DATETIME_VENDA", Source = "DatetimeVenda", Target = "DATETIME_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.SMALL_INT_VENDA", Source = "SmallIntVenda", Target = "SMALL_INT_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.CLIENTE.ID_CLIENTE", Source = "IdCliente", Target = "ID_CLIENTE", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BaseTeste.CLIENTE", RelationPropertyName = "CLIENTE" });

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
	    	    return Linx.Demo.BV.Domains.LX_VENDA.GetValues();
	    }
	    private string _comboboxVendaName;
	    [DataMember(IsRequired = false, Name = "ComboboxVendaName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Combobox Venda", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
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

		

	[LinxPublicationView(PrimaryKeys="VENDA_ITEM.ID_VENDA_ITEM", IsUpdatable=false, EdmName="LINXDEMO.BM.BaseTeste")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[VendaItem];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#.VENDA_ITEM_LISTA as #Alias#];EdmEntityName[VENDA_ITEM];EntityRelations[VENDA(VENDA)#CLIENTE(CLIENTE)#ESTADO(ESTADO)#PAIS(PAIS)#LOJA(LOJA)];EdmParentEntityName[VENDA];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "VendaItem")]
	[Serializable()]
	public partial class VendaItem : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(PaiFilhaDomainService context)
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
	    partial void OnBigIntVendaItemChanging(System.Nullable<long> value);
	    partial void OnBigIntVendaItemChanged();

	    private System.Nullable<long> _BigIntVendaItem;

	    [DataMember(Name = "BigIntVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Big Int Venda Item", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.BIG_INT_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.BIG_INT_VENDA_ITEM")]
	    public System.Nullable<long> BigIntVendaItem
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
	    partial void OnBitVendaItemChanging(System.Nullable<bool> value);
	    partial void OnBitVendaItemChanged();

	    private System.Nullable<bool> _BitVendaItem;

	    [DataMember(Name = "BitVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bit Venda Item", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.BIT_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.BIT_VENDA_ITEM")]
	    public System.Nullable<bool> BitVendaItem
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
	    partial void OnComboboxVendaItemChanging(byte value);
	    partial void OnComboboxVendaItemChanged();

	    private byte _ComboboxVendaItem;

	    [DataMember(IsRequired = true, Name = "ComboboxVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Venda Item", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_VENDA_ITEM];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.COMBOBOX_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.COMBOBOX_VENDA_ITEM")]
	    public byte ComboboxVendaItem
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
	    partial void OnDatetimeVendaItemChanging(System.Nullable<DateTime> value);
	    partial void OnDatetimeVendaItemChanged();

	    private System.Nullable<DateTime> _DatetimeVendaItem;

	    [DataMember(Name = "DatetimeVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Datetime Venda Item", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.DATETIME_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.DATETIME_VENDA_ITEM")]
	    public System.Nullable<DateTime> DatetimeVendaItem
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
	    partial void OnDecimalVendaItemChanging(System.Nullable<decimal> value);
	    partial void OnDecimalVendaItemChanged();

	    private System.Nullable<decimal> _DecimalVendaItem;

	    [DataMember(Name = "DecimalVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal Venda Item", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.DECIMAL_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.DECIMAL_VENDA_ITEM")]
	    public System.Nullable<decimal> DecimalVendaItem
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
	    partial void OnGuidVendaItemChanging(System.Nullable<Guid> value);
	    partial void OnGuidVendaItemChanged();

	    private System.Nullable<Guid> _GuidVendaItem;

	    [DataMember(Name = "GuidVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Guid Venda Item", Description="", Order = 6, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.GUID_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.GUID_VENDA_ITEM")]
	    public System.Nullable<Guid> GuidVendaItem
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
	    partial void OnIdVendaChanging(System.Nullable<int> value);
	    partial void OnIdVendaChanged();

	    private System.Nullable<int> _IdVenda;

	    [DataMember(Name = "IdVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Venda", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.VENDA.ID_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.VENDA.ID_VENDA")]
	    public System.Nullable<int> IdVenda
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
	    partial void OnIdVendaItemChanging(int value);
	    partial void OnIdVendaItemChanged();

	    private int _IdVendaItem;

	    [DataMember(IsRequired = true, Name = "IdVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Venda Item", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.ID_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.ID_VENDA_ITEM")]
	    public int IdVendaItem
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
	    partial void OnIntVendaItemChanging(System.Nullable<int> value);
	    partial void OnIntVendaItemChanged();

	    private System.Nullable<int> _IntVendaItem;

	    [DataMember(Name = "IntVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int Venda Item", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.INT_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.INT_VENDA_ITEM")]
	    public System.Nullable<int> IntVendaItem
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
	    partial void OnSmallIntVendaItemChanging(System.Nullable<short> value);
	    partial void OnSmallIntVendaItemChanged();

	    private System.Nullable<short> _SmallIntVendaItem;

	    [DataMember(Name = "SmallIntVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Small Int Venda Item", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.SMALL_INT_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.SMALL_INT_VENDA_ITEM")]
	    public System.Nullable<short> SmallIntVendaItem
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
	    partial void OnStringVendaItemChanging(string value);
	    partial void OnStringVendaItemChanged();

	    private string _StringVendaItem;

	    [DataMember(Name = "StringVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Venda Item", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.STRING_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.STRING_VENDA_ITEM")]
	    public string StringVendaItem
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
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "BaseTeste.VENDA_ITEM").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(LINXDEMO.BM.VENDA_ITEM), QualifiedEntitySetName = "BaseTeste.VENDA_ITEM" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.ID_VENDA_ITEM", Source = "IdVendaItem", Target = "ID_VENDA_ITEM", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.BIT_VENDA_ITEM", Source = "BitVendaItem", Target = "BIT_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.VENDA.ID_VENDA", Source = "IdVenda", Target = "ID_VENDA", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BaseTeste.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.INT_VENDA_ITEM", Source = "IntVendaItem", Target = "INT_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.GUID_VENDA_ITEM", Source = "GuidVendaItem", Target = "GUID_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.STRING_VENDA_ITEM", Source = "StringVendaItem", Target = "STRING_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.BIG_INT_VENDA_ITEM", Source = "BigIntVendaItem", Target = "BIG_INT_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.DECIMAL_VENDA_ITEM", Source = "DecimalVendaItem", Target = "DECIMAL_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.COMBOBOX_VENDA_ITEM", Source = "ComboboxVendaItem", Target = "COMBOBOX_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.DATETIME_VENDA_ITEM", Source = "DatetimeVendaItem", Target = "DATETIME_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.SMALL_INT_VENDA_ITEM", Source = "SmallIntVendaItem", Target = "SMALL_INT_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });

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
	    	    return Linx.Demo.BV.Domains.LX_VENDA_ITEM.GetValues();
	    }
	    private string _comboboxVendaItemName;
	    [DataMember(IsRequired = false, Name = "ComboboxVendaItemName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Combobox Venda Item", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
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

		

	[LinxPublicationView(PrimaryKeys="VENDA_ATACADO.ID_VENDA_ATACADO", IsUpdatable=false, EdmName="LINXDEMO.BM.BaseTeste")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[VendaAtacado];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#.VENDA_ATACADO_LISTA as #Alias#];EdmEntityName[VENDA_ATACADO];EntityRelations[CLIENTE(CLIENTE)#ESTADO(ESTADO)#PAIS(PAIS)];EdmParentEntityName[CLIENTE];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "VendaAtacado")]
	[Serializable()]
	public partial class VendaAtacado : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(PaiFilhaDomainService context)
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
	    partial void OnBigIntVendaAtacadoChanging(System.Nullable<long> value);
	    partial void OnBigIntVendaAtacadoChanged();

	    private System.Nullable<long> _BigIntVendaAtacado;

	    [DataMember(Name = "BigIntVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Big Int Venda Atacado", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.BIG_INT_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.BIG_INT_VENDA_ATACADO")]
	    public System.Nullable<long> BigIntVendaAtacado
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
	    partial void OnBitVendaAtacadoChanging(System.Nullable<bool> value);
	    partial void OnBitVendaAtacadoChanged();

	    private System.Nullable<bool> _BitVendaAtacado;

	    [DataMember(Name = "BitVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bit Venda Atacado", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.BIT_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.BIT_VENDA_ATACADO")]
	    public System.Nullable<bool> BitVendaAtacado
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
	    partial void OnComboboxVendaAtacadoChanging(byte value);
	    partial void OnComboboxVendaAtacadoChanged();

	    private byte _ComboboxVendaAtacado;

	    [DataMember(IsRequired = true, Name = "ComboboxVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Venda Atacado", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_VENDA_ATACADO];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.COMBOBOX_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.COMBOBOX_VENDA_ATACADO")]
	    public byte ComboboxVendaAtacado
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
	    partial void OnDatetimeVendaAtacadoChanging(System.Nullable<DateTime> value);
	    partial void OnDatetimeVendaAtacadoChanged();

	    private System.Nullable<DateTime> _DatetimeVendaAtacado;

	    [DataMember(Name = "DatetimeVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Datetime Venda Atacado", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.DATETIME_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.DATETIME_VENDA_ATACADO")]
	    public System.Nullable<DateTime> DatetimeVendaAtacado
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
	    partial void OnDecimalVendaAtacadoChanging(System.Nullable<decimal> value);
	    partial void OnDecimalVendaAtacadoChanged();

	    private System.Nullable<decimal> _DecimalVendaAtacado;

	    [DataMember(Name = "DecimalVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal Venda Atacado", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.DECIMAL_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.DECIMAL_VENDA_ATACADO")]
	    public System.Nullable<decimal> DecimalVendaAtacado
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
	    partial void OnGuidVendaAtacadoChanging(System.Nullable<Guid> value);
	    partial void OnGuidVendaAtacadoChanged();

	    private System.Nullable<Guid> _GuidVendaAtacado;

	    [DataMember(Name = "GuidVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Guid Venda Atacado", Description="", Order = 6, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.GUID_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.GUID_VENDA_ATACADO")]
	    public System.Nullable<Guid> GuidVendaAtacado
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
	    partial void OnIdClienteChanging(System.Nullable<int> value);
	    partial void OnIdClienteChanged();

	    private System.Nullable<int> _IdCliente;

	    [DataMember(Name = "IdCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Cliente", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.CLIENTE.ID_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.CLIENTE.ID_CLIENTE")]
	    public System.Nullable<int> IdCliente
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
	    partial void OnIdVendaAtacadoChanging(int value);
	    partial void OnIdVendaAtacadoChanged();

	    private int _IdVendaAtacado;

	    [DataMember(IsRequired = true, Name = "IdVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Venda Atacado", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.ID_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.ID_VENDA_ATACADO")]
	    public int IdVendaAtacado
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
	    partial void OnIntVendaAtacadoChanging(System.Nullable<int> value);
	    partial void OnIntVendaAtacadoChanged();

	    private System.Nullable<int> _IntVendaAtacado;

	    [DataMember(Name = "IntVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int Venda Atacado", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.INT_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.INT_VENDA_ATACADO")]
	    public System.Nullable<int> IntVendaAtacado
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
	    partial void OnSmallIntVendaAtacadoChanging(System.Nullable<short> value);
	    partial void OnSmallIntVendaAtacadoChanged();

	    private System.Nullable<short> _SmallIntVendaAtacado;

	    [DataMember(Name = "SmallIntVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Small Int Venda Atacado", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.SMALL_INT_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.SMALL_INT_VENDA_ATACADO")]
	    public System.Nullable<short> SmallIntVendaAtacado
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
	    partial void OnStringVendaAtacadoChanging(string value);
	    partial void OnStringVendaAtacadoChanged();

	    private string _StringVendaAtacado;

	    [DataMember(Name = "StringVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Venda Atacado", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.STRING_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.STRING_VENDA_ATACADO")]
	    public string StringVendaAtacado
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
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "BaseTeste.VENDA_ATACADO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(LINXDEMO.BM.VENDA_ATACADO), QualifiedEntitySetName = "BaseTeste.VENDA_ATACADO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.ID_VENDA_ATACADO", Source = "IdVendaAtacado", Target = "ID_VENDA_ATACADO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.BIT_VENDA_ATACADO", Source = "BitVendaAtacado", Target = "BIT_VENDA_ATACADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.INT_VENDA_ATACADO", Source = "IntVendaAtacado", Target = "INT_VENDA_ATACADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.GUID_VENDA_ATACADO", Source = "GuidVendaAtacado", Target = "GUID_VENDA_ATACADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.CLIENTE.ID_CLIENTE", Source = "IdCliente", Target = "ID_CLIENTE", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BaseTeste.CLIENTE", RelationPropertyName = "CLIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.STRING_VENDA_ATACADO", Source = "StringVendaAtacado", Target = "STRING_VENDA_ATACADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.BIG_INT_VENDA_ATACADO", Source = "BigIntVendaAtacado", Target = "BIG_INT_VENDA_ATACADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.DECIMAL_VENDA_ATACADO", Source = "DecimalVendaAtacado", Target = "DECIMAL_VENDA_ATACADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.COMBOBOX_VENDA_ATACADO", Source = "ComboboxVendaAtacado", Target = "COMBOBOX_VENDA_ATACADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.DATETIME_VENDA_ATACADO", Source = "DatetimeVendaAtacado", Target = "DATETIME_VENDA_ATACADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.SMALL_INT_VENDA_ATACADO", Source = "SmallIntVendaAtacado", Target = "SMALL_INT_VENDA_ATACADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });

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
	    	    return Linx.Demo.BV.Domains.LX_VENDA_ATACADO.GetValues();
	    }
	    private string _comboboxVendaAtacadoName;
	    [DataMember(IsRequired = false, Name = "ComboboxVendaAtacadoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Combobox Venda Atacado", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
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

		

	[LinxPublicationView(PrimaryKeys="LOJA.ID_LOJA", IsUpdatable=false, EdmName="LINXDEMO.BM.BaseTeste")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[Loja,Loja.Vendedor];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[];EdmEntityName[LOJA];EntityRelations[];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "Loja")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Demo.BV.PaiFilha.Loja")]
	public partial class Loja : Linx.Data.Entity
	{

	
		
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.VendedorList != null && this.VendedorList.Count() > 0)
	      {
	         foreach (var entity in this.VendedorList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.VendedorList != null)
	      {
	         foreach (var detail in this.VendedorList)
	         {
	            detail.ResetDetails();
	         }
	         this.VendedorList = null;
	      }
	    }

	    public virtual void ResetChangeState()
	    {
	      this.ChangeState = "N";
	      if (this.VendedorList != null)
	      {
	         foreach (var detail in this.VendedorList.ToArray())
	         {
	            detail.ResetChangeState();
	         }
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(PaiFilhaDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("Vendedor"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("Vendedor");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdLoja"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdLoja));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load Vendedor and all sub-details
	         if (this.VendedorList == null || this.VendedorList.Count() == 0)
	         {
	             if (take > 0)
	                 this.VendedorList = context.GetPagedVendedor(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.VendedorList = (from r in context.GetVendedorByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _VendedorElements = changeSet.ChangeSetEntries.Where(e => e.Entity is Vendedor && ((Vendedor)e.Entity).Loja == null && e.Associations == null && e.OriginalAssociations == null && ((Vendedor)e.Entity).IdLoja == this.IdLoja).ToList();
 	      if (_VendedorElements.Count > 0 && this.VendedorList.Count() == 0)
 	      {
 	          this.VendedorList = _VendedorElements.Select(e => (Vendedor)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _VendedorElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((Vendedor)detail.Entity).Loja = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("Loja", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("VendedorList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For BigIntLoja
	    partial void OnBigIntLojaChanging(System.Nullable<long> value);
	    partial void OnBigIntLojaChanged();

	    private System.Nullable<long> _BigIntLoja;

	    [DataMember(Name = "BigIntLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Big Int Loja", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[19:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LOJA.BIG_INT_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.BIG_INT_LOJA")]
	    public System.Nullable<long> BigIntLoja
	    {
	    	    get
	    	    {
	    	          return _BigIntLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._BigIntLoja != value)
	    	          {
	    	              this.ValidateProperty("BigIntLoja", value);
	    	              this.OnBigIntLojaChanging(value);
	    	              this.RaiseDataMemberChanging("BigIntLoja");
	    	              this._BigIntLoja = value;
	    	              this.RaiseDataMemberChanged("BigIntLoja");
	    	              this.OnBigIntLojaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For BitLoja
	    partial void OnBitLojaChanging(System.Nullable<bool> value);
	    partial void OnBitLojaChanged();

	    private System.Nullable<bool> _BitLoja;

	    [DataMember(Name = "BitLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bit Loja", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LOJA.BIT_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.BIT_LOJA")]
	    public System.Nullable<bool> BitLoja
	    {
	    	    get
	    	    {
	    	          return _BitLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._BitLoja != value)
	    	          {
	    	              this.ValidateProperty("BitLoja", value);
	    	              this.OnBitLojaChanging(value);
	    	              this.RaiseDataMemberChanging("BitLoja");
	    	              this._BitLoja = value;
	    	              this.RaiseDataMemberChanged("BitLoja");
	    	              this.OnBitLojaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ComboboxLoja
	    partial void OnComboboxLojaChanging(byte value);
	    partial void OnComboboxLojaChanged();

	    private byte _ComboboxLoja;

	    [DataMember(IsRequired = true, Name = "ComboboxLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Loja", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_LOJA];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LOJA.COMBOBOX_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.COMBOBOX_LOJA")]
	    public byte ComboboxLoja
	    {
	    	    get
	    	    {
	    	          return _ComboboxLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._ComboboxLoja != value)
	    	          {
	    	              this.ValidateProperty("ComboboxLoja", value);
	    	              this.OnComboboxLojaChanging(value);
	    	              this.RaiseDataMemberChanging("ComboboxLoja");
	    	              this._ComboboxLoja = value;
	    	              this.RaiseDataMemberChanged("ComboboxLoja");
	    	              this.OnComboboxLojaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DatetimeLoja
	    partial void OnDatetimeLojaChanging(System.Nullable<DateTime> value);
	    partial void OnDatetimeLojaChanged();

	    private System.Nullable<DateTime> _DatetimeLoja;

	    [DataMember(Name = "DatetimeLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Datetime Loja", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LOJA.DATETIME_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.DATETIME_LOJA")]
	    public System.Nullable<DateTime> DatetimeLoja
	    {
	    	    get
	    	    {
	    	          return _DatetimeLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._DatetimeLoja != value)
	    	          {
	    	              this.ValidateProperty("DatetimeLoja", value);
	    	              this.OnDatetimeLojaChanging(value);
	    	              this.RaiseDataMemberChanging("DatetimeLoja");
	    	              this._DatetimeLoja = value;
	    	              this.RaiseDataMemberChanged("DatetimeLoja");
	    	              this.OnDatetimeLojaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DecimalLoja
	    partial void OnDecimalLojaChanging(System.Nullable<decimal> value);
	    partial void OnDecimalLojaChanged();

	    private System.Nullable<decimal> _DecimalLoja;

	    [DataMember(Name = "DecimalLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal Loja", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LOJA.DECIMAL_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.DECIMAL_LOJA")]
	    public System.Nullable<decimal> DecimalLoja
	    {
	    	    get
	    	    {
	    	          return _DecimalLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._DecimalLoja != value)
	    	          {
	    	              this.ValidateProperty("DecimalLoja", value);
	    	              this.OnDecimalLojaChanging(value);
	    	              this.RaiseDataMemberChanging("DecimalLoja");
	    	              this._DecimalLoja = value;
	    	              this.RaiseDataMemberChanged("DecimalLoja");
	    	              this.OnDecimalLojaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For GuidLoja
	    partial void OnGuidLojaChanging(System.Nullable<Guid> value);
	    partial void OnGuidLojaChanged();

	    private System.Nullable<Guid> _GuidLoja;

	    [DataMember(Name = "GuidLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Guid Loja", Description="", Order = 6, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LOJA.GUID_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.GUID_LOJA")]
	    public System.Nullable<Guid> GuidLoja
	    {
	    	    get
	    	    {
	    	          return _GuidLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._GuidLoja != value)
	    	          {
	    	              this.ValidateProperty("GuidLoja", value);
	    	              this.OnGuidLojaChanging(value);
	    	              this.RaiseDataMemberChanging("GuidLoja");
	    	              this._GuidLoja = value;
	    	              this.RaiseDataMemberChanged("GuidLoja");
	    	              this.OnGuidLojaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLoja
	    partial void OnIdLojaChanging(int value);
	    partial void OnIdLojaChanged();

	    private int _IdLoja;

	    [DataMember(IsRequired = true, Name = "IdLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Loja", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LOJA.ID_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.ID_LOJA")]
	    public int IdLoja
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
	    //Extensibility Partial Method Definitions For IntLoja
	    partial void OnIntLojaChanging(System.Nullable<int> value);
	    partial void OnIntLojaChanged();

	    private System.Nullable<int> _IntLoja;

	    [DataMember(Name = "IntLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int Loja", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LOJA.INT_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.INT_LOJA")]
	    public System.Nullable<int> IntLoja
	    {
	    	    get
	    	    {
	    	          return _IntLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._IntLoja != value)
	    	          {
	    	              this.ValidateProperty("IntLoja", value);
	    	              this.OnIntLojaChanging(value);
	    	              this.RaiseDataMemberChanging("IntLoja");
	    	              this._IntLoja = value;
	    	              this.RaiseDataMemberChanged("IntLoja");
	    	              this.OnIntLojaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For SmallIntLoja
	    partial void OnSmallIntLojaChanging(System.Nullable<short> value);
	    partial void OnSmallIntLojaChanged();

	    private System.Nullable<short> _SmallIntLoja;

	    [DataMember(Name = "SmallIntLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Small Int Loja", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[5:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LOJA.SMALL_INT_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.SMALL_INT_LOJA")]
	    public System.Nullable<short> SmallIntLoja
	    {
	    	    get
	    	    {
	    	          return _SmallIntLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._SmallIntLoja != value)
	    	          {
	    	              this.ValidateProperty("SmallIntLoja", value);
	    	              this.OnSmallIntLojaChanging(value);
	    	              this.RaiseDataMemberChanging("SmallIntLoja");
	    	              this._SmallIntLoja = value;
	    	              this.RaiseDataMemberChanged("SmallIntLoja");
	    	              this.OnSmallIntLojaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringLoja
	    partial void OnStringLojaChanging(string value);
	    partial void OnStringLojaChanged();

	    private string _StringLoja;

	    [DataMember(Name = "StringLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Loja", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LOJA.STRING_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.STRING_LOJA")]
	    public string StringLoja
	    {
	    	    get
	    	    {
	    	          return _StringLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringLoja != value)
	    	          {
	    	              this.ValidateProperty("StringLoja", value);
	    	              this.OnStringLojaChanging(value);
	    	              this.RaiseDataMemberChanging("StringLoja");
	    	              this._StringLoja = value;
	    	              this.RaiseDataMemberChanged("StringLoja");
	    	              this.OnStringLojaChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<Vendedor> _VendedorList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_Loja_Vendedor", "IdLoja", "IdLoja", IsForeignKey=false)]
	    [DataMember(Name = "VendedorList", EmitDefaultValue = true)]
	    public IEnumerable<Vendedor> VendedorList
	    {
	        get
	        {
	
	            if (this._VendedorList == null)
	            	this._VendedorList = new List<Vendedor>();
	
	            return this._VendedorList;
	        }
	        set
	        {
	            if (this._VendedorList != value)
	            {
	                this._VendedorList = value;
	                this.RaisePropertyChanged("VendedorList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "BaseTeste.LOJA").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(LINXDEMO.BM.LOJA), QualifiedEntitySetName = "BaseTeste.LOJA" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.ID_LOJA", Source = "IdLoja", Target = "ID_LOJA", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "BaseTeste.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.BIT_LOJA", Source = "BitLoja", Target = "BIT_LOJA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.INT_LOJA", Source = "IntLoja", Target = "INT_LOJA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.GUID_LOJA", Source = "GuidLoja", Target = "GUID_LOJA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.STRING_LOJA", Source = "StringLoja", Target = "STRING_LOJA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.BIG_INT_LOJA", Source = "BigIntLoja", Target = "BIG_INT_LOJA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.DECIMAL_LOJA", Source = "DecimalLoja", Target = "DECIMAL_LOJA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.COMBOBOX_LOJA", Source = "ComboboxLoja", Target = "COMBOBOX_LOJA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.DATETIME_LOJA", Source = "DatetimeLoja", Target = "DATETIME_LOJA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.SMALL_INT_LOJA", Source = "SmallIntLoja", Target = "SMALL_INT_LOJA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.LOJA", RelationPropertyName = "LOJA" });

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
	 

	    public Dictionary<string, string> GetComboboxLojaValues()
	    {
	    	    return Linx.Demo.BV.Domains.LX_LOJA.GetValues();
	    }
	    private string _comboboxLojaName;
	    [DataMember(IsRequired = false, Name = "ComboboxLojaName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Combobox Loja", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ComboboxLojaName
	    {
	    	    get { if (this.ComboboxLoja.IsNull()) { _comboboxLojaName = String.Empty; } else { string key = this.ComboboxLoja.ToString(); var dmValues = this.GetComboboxLojaValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _comboboxLojaName) _comboboxLojaName = domainName; } return _comboboxLojaName; } set { _comboboxLojaName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="VENDEDOR.ID_VENDEDOR", IsUpdatable=false, EdmName="LINXDEMO.BM.BaseTeste")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#.VENDEDOR_LISTA as #Alias#];EdmEntityName[VENDEDOR];EntityRelations[LOJA(LOJA)];EdmParentEntityName[LOJA];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "Vendedor")]
	[Serializable()]
	public partial class Vendedor : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(PaiFilhaDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("Loja");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdLoja"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdLoja));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load Loja
	         this.Loja = (from r in context.GetLojaByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For BitVendedor
	    partial void OnBitVendedorChanging(System.Nullable<bool> value);
	    partial void OnBitVendedorChanged();

	    private System.Nullable<bool> _BitVendedor;

	    [DataMember(Name = "BitVendedor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bit Vendedor", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDEDOR.BIT_VENDEDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDEDOR.BIT_VENDEDOR")]
	    public System.Nullable<bool> BitVendedor
	    {
	    	    get
	    	    {
	    	          return _BitVendedor;
	    	    }
	    	    set
	    	    {
	    	          if (this._BitVendedor != value)
	    	          {
	    	              this.ValidateProperty("BitVendedor", value);
	    	              this.OnBitVendedorChanging(value);
	    	              this.RaiseDataMemberChanging("BitVendedor");
	    	              this._BitVendedor = value;
	    	              this.RaiseDataMemberChanged("BitVendedor");
	    	              this.OnBitVendedorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ComboboxVendedor
	    partial void OnComboboxVendedorChanging(byte value);
	    partial void OnComboboxVendedorChanged();

	    private byte _ComboboxVendedor;

	    [DataMember(IsRequired = true, Name = "ComboboxVendedor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Vendedor", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_VENDEDOR];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDEDOR.COMBOBOX_VENDEDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDEDOR.COMBOBOX_VENDEDOR")]
	    public byte ComboboxVendedor
	    {
	    	    get
	    	    {
	    	          return _ComboboxVendedor;
	    	    }
	    	    set
	    	    {
	    	          if (this._ComboboxVendedor != value)
	    	          {
	    	              this.ValidateProperty("ComboboxVendedor", value);
	    	              this.OnComboboxVendedorChanging(value);
	    	              this.RaiseDataMemberChanging("ComboboxVendedor");
	    	              this._ComboboxVendedor = value;
	    	              this.RaiseDataMemberChanged("ComboboxVendedor");
	    	              this.OnComboboxVendedorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DatetimeVendedor
	    partial void OnDatetimeVendedorChanging(System.Nullable<DateTime> value);
	    partial void OnDatetimeVendedorChanged();

	    private System.Nullable<DateTime> _DatetimeVendedor;

	    [DataMember(Name = "DatetimeVendedor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Datetime Vendedor", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDEDOR.DATETIME_VENDEDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDEDOR.DATETIME_VENDEDOR")]
	    public System.Nullable<DateTime> DatetimeVendedor
	    {
	    	    get
	    	    {
	    	          return _DatetimeVendedor;
	    	    }
	    	    set
	    	    {
	    	          if (this._DatetimeVendedor != value)
	    	          {
	    	              this.ValidateProperty("DatetimeVendedor", value);
	    	              this.OnDatetimeVendedorChanging(value);
	    	              this.RaiseDataMemberChanging("DatetimeVendedor");
	    	              this._DatetimeVendedor = value;
	    	              this.RaiseDataMemberChanged("DatetimeVendedor");
	    	              this.OnDatetimeVendedorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DecimalVendedor
	    partial void OnDecimalVendedorChanging(System.Nullable<decimal> value);
	    partial void OnDecimalVendedorChanged();

	    private System.Nullable<decimal> _DecimalVendedor;

	    [DataMember(Name = "DecimalVendedor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal Vendedor", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDEDOR.DECIMAL_VENDEDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDEDOR.DECIMAL_VENDEDOR")]
	    public System.Nullable<decimal> DecimalVendedor
	    {
	    	    get
	    	    {
	    	          return _DecimalVendedor;
	    	    }
	    	    set
	    	    {
	    	          if (this._DecimalVendedor != value)
	    	          {
	    	              this.ValidateProperty("DecimalVendedor", value);
	    	              this.OnDecimalVendedorChanging(value);
	    	              this.RaiseDataMemberChanging("DecimalVendedor");
	    	              this._DecimalVendedor = value;
	    	              this.RaiseDataMemberChanged("DecimalVendedor");
	    	              this.OnDecimalVendedorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For GuidVendedor
	    partial void OnGuidVendedorChanging(System.Nullable<Guid> value);
	    partial void OnGuidVendedorChanged();

	    private System.Nullable<Guid> _GuidVendedor;

	    [DataMember(Name = "GuidVendedor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Guid Vendedor", Description="", Order = 6, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDEDOR.GUID_VENDEDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDEDOR.GUID_VENDEDOR")]
	    public System.Nullable<Guid> GuidVendedor
	    {
	    	    get
	    	    {
	    	          return _GuidVendedor;
	    	    }
	    	    set
	    	    {
	    	          if (this._GuidVendedor != value)
	    	          {
	    	              this.ValidateProperty("GuidVendedor", value);
	    	              this.OnGuidVendedorChanging(value);
	    	              this.RaiseDataMemberChanging("GuidVendedor");
	    	              this._GuidVendedor = value;
	    	              this.RaiseDataMemberChanged("GuidVendedor");
	    	              this.OnGuidVendedorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLoja
	    partial void OnIdLojaChanging(System.Nullable<int> value);
	    partial void OnIdLojaChanged();

	    private System.Nullable<int> _IdLoja;

	    [DataMember(Name = "IdLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Loja", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDEDOR.LOJA.ID_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDEDOR.LOJA.ID_LOJA")]
	    public System.Nullable<int> IdLoja
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
	    //Extensibility Partial Method Definitions For IdVendedor
	    partial void OnIdVendedorChanging(int value);
	    partial void OnIdVendedorChanged();

	    private int _IdVendedor;

	    [DataMember(IsRequired = true, Name = "IdVendedor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Vendedor", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDEDOR.ID_VENDEDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDEDOR.ID_VENDEDOR")]
	    public int IdVendedor
	    {
	    	    get
	    	    {
	    	          return _IdVendedor;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdVendedor != value)
	    	          {
	    	              this.ValidateProperty("IdVendedor", value);
	    	              this.OnIdVendedorChanging(value);
	    	              this.RaiseDataMemberChanging("IdVendedor");
	    	              this._IdVendedor = value;
	    	              this.RaiseDataMemberChanged("IdVendedor");
	    	              this.OnIdVendedorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IntVendedor
	    partial void OnIntVendedorChanging(System.Nullable<int> value);
	    partial void OnIntVendedorChanged();

	    private System.Nullable<int> _IntVendedor;

	    [DataMember(Name = "IntVendedor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int Vendedor", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDEDOR.INT_VENDEDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDEDOR.INT_VENDEDOR")]
	    public System.Nullable<int> IntVendedor
	    {
	    	    get
	    	    {
	    	          return _IntVendedor;
	    	    }
	    	    set
	    	    {
	    	          if (this._IntVendedor != value)
	    	          {
	    	              this.ValidateProperty("IntVendedor", value);
	    	              this.OnIntVendedorChanging(value);
	    	              this.RaiseDataMemberChanging("IntVendedor");
	    	              this._IntVendedor = value;
	    	              this.RaiseDataMemberChanged("IntVendedor");
	    	              this.OnIntVendedorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For SmallIntVendedor
	    partial void OnSmallIntVendedorChanging(System.Nullable<short> value);
	    partial void OnSmallIntVendedorChanged();

	    private System.Nullable<short> _SmallIntVendedor;

	    [DataMember(Name = "SmallIntVendedor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Small Int Vendedor", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[5:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDEDOR.SMALL_INT_VENDEDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDEDOR.SMALL_INT_VENDEDOR")]
	    public System.Nullable<short> SmallIntVendedor
	    {
	    	    get
	    	    {
	    	          return _SmallIntVendedor;
	    	    }
	    	    set
	    	    {
	    	          if (this._SmallIntVendedor != value)
	    	          {
	    	              this.ValidateProperty("SmallIntVendedor", value);
	    	              this.OnSmallIntVendedorChanging(value);
	    	              this.RaiseDataMemberChanging("SmallIntVendedor");
	    	              this._SmallIntVendedor = value;
	    	              this.RaiseDataMemberChanged("SmallIntVendedor");
	    	              this.OnSmallIntVendedorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringVendedor
	    partial void OnStringVendedorChanging(string value);
	    partial void OnStringVendedorChanged();

	    private string _StringVendedor;

	    [DataMember(Name = "StringVendedor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Vendedor", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDEDOR.STRING_VENDEDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDEDOR.STRING_VENDEDOR")]
	    public string StringVendedor
	    {
	    	    get
	    	    {
	    	          return _StringVendedor;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringVendedor != value)
	    	          {
	    	              this.ValidateProperty("StringVendedor", value);
	    	              this.OnStringVendedorChanging(value);
	    	              this.RaiseDataMemberChanging("StringVendedor");
	    	              this._StringVendedor = value;
	    	              this.RaiseDataMemberChanged("StringVendedor");
	    	              this.OnStringVendedorChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private Loja _Loja;
	    [DataMember(Name = "Loja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_Loja_Vendedor", "IdLoja", "IdLoja", IsForeignKey=true)]
	    public Loja Loja
	    {
	        get
	        {
	            return this._Loja;
	        }
	        set
	        {
	            if (this._Loja != value)
	            {
	                this._Loja = value;
	                this.RaisePropertyChanged("LojaList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "BaseTeste.VENDEDOR").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(LINXDEMO.BM.VENDEDOR), QualifiedEntitySetName = "BaseTeste.VENDEDOR" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDEDOR.ID_VENDEDOR", Source = "IdVendedor", Target = "ID_VENDEDOR", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDEDOR", RelationPropertyName = "VENDEDOR" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDEDOR.BIT_VENDEDOR", Source = "BitVendedor", Target = "BIT_VENDEDOR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDEDOR", RelationPropertyName = "VENDEDOR" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDEDOR.LOJA.ID_LOJA", Source = "IdLoja", Target = "ID_LOJA", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BaseTeste.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDEDOR.INT_VENDEDOR", Source = "IntVendedor", Target = "INT_VENDEDOR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDEDOR", RelationPropertyName = "VENDEDOR" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDEDOR.GUID_VENDEDOR", Source = "GuidVendedor", Target = "GUID_VENDEDOR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDEDOR", RelationPropertyName = "VENDEDOR" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDEDOR.STRING_VENDEDOR", Source = "StringVendedor", Target = "STRING_VENDEDOR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDEDOR", RelationPropertyName = "VENDEDOR" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDEDOR.DECIMAL_VENDEDOR", Source = "DecimalVendedor", Target = "DECIMAL_VENDEDOR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDEDOR", RelationPropertyName = "VENDEDOR" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDEDOR.COMBOBOX_VENDEDOR", Source = "ComboboxVendedor", Target = "COMBOBOX_VENDEDOR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDEDOR", RelationPropertyName = "VENDEDOR" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDEDOR.DATETIME_VENDEDOR", Source = "DatetimeVendedor", Target = "DATETIME_VENDEDOR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDEDOR", RelationPropertyName = "VENDEDOR" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDEDOR.SMALL_INT_VENDEDOR", Source = "SmallIntVendedor", Target = "SMALL_INT_VENDEDOR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDEDOR", RelationPropertyName = "VENDEDOR" });

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
	 

	    public Dictionary<string, string> GetComboboxVendedorValues()
	    {
	    	    return Linx.Demo.BV.Domains.LX_VENDEDOR.GetValues();
	    }
	    private string _comboboxVendedorName;
	    [DataMember(IsRequired = false, Name = "ComboboxVendedorName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Combobox Vendedor", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ComboboxVendedorName
	    {
	    	    get { if (this.ComboboxVendedor.IsNull()) { _comboboxVendedorName = String.Empty; } else { string key = this.ComboboxVendedor.ToString(); var dmValues = this.GetComboboxVendedorValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _comboboxVendedorName) _comboboxVendedorName = domainName; } return _comboboxVendedorName; } set { _comboboxVendedorName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Venda];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#.VENDA_LISTA as #Alias#];EdmEntityName[VENDA];EntityRelations[CLIENTE(CLIENTE)#ESTADO(ESTADO)#PAIS(PAIS)#LOJA(LOJA)];EdmParentEntityName[CLIENTE];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "Venda")]
	[Serializable()]
	public partial class VendaParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For BigIntVenda
	    partial void OnBigIntVendaChanging(System.Nullable<long> value);
	    partial void OnBigIntVendaChanged();

	    private System.Nullable<long> _BigIntVenda;

	    [DataMember(Name = "BigIntVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Big Int Venda", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.BIG_INT_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.BIG_INT_VENDA")]
	    public System.Nullable<long> BigIntVenda
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
	    partial void OnBitVendaChanging(System.Nullable<bool> value);
	    partial void OnBitVendaChanged();

	    private System.Nullable<bool> _BitVenda;

	    [DataMember(Name = "BitVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bit Venda", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.BIT_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.BIT_VENDA")]
	    public System.Nullable<bool> BitVenda
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
	    partial void OnComboboxVendaChanging(byte value);
	    partial void OnComboboxVendaChanged();

	    private byte _ComboboxVenda;

	    [DataMember(IsRequired = true, Name = "ComboboxVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Venda", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_VENDA];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.COMBOBOX_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.COMBOBOX_VENDA")]
	    public byte ComboboxVenda
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
	    partial void OnDatetimeVendaChanging(System.Nullable<DateTime> value);
	    partial void OnDatetimeVendaChanged();

	    private System.Nullable<DateTime> _DatetimeVenda;

	    [DataMember(Name = "DatetimeVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Datetime Venda", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.DATETIME_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.DATETIME_VENDA")]
	    public System.Nullable<DateTime> DatetimeVenda
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
	    partial void OnDecimalVendaChanging(System.Nullable<decimal> value);
	    partial void OnDecimalVendaChanged();

	    private System.Nullable<decimal> _DecimalVenda;

	    [DataMember(Name = "DecimalVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal Venda", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.DECIMAL_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.DECIMAL_VENDA")]
	    public System.Nullable<decimal> DecimalVenda
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
	    partial void OnGuidVendaChanging(System.Nullable<Guid> value);
	    partial void OnGuidVendaChanged();

	    private System.Nullable<Guid> _GuidVenda;

	    [DataMember(Name = "GuidVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Guid Venda", Description="", Order = 6, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.GUID_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.GUID_VENDA")]
	    public System.Nullable<Guid> GuidVenda
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
	    partial void OnIdClienteChanging(System.Nullable<int> value);
	    partial void OnIdClienteChanged();

	    private System.Nullable<int> _IdCliente;

	    [DataMember(Name = "IdCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Cliente", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.CLIENTE.ID_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.CLIENTE.ID_CLIENTE")]
	    public System.Nullable<int> IdCliente
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
	    partial void OnIdLojaChanging(System.Nullable<int> value);
	    partial void OnIdLojaChanged();

	    private System.Nullable<int> _IdLoja;

	    [DataMember(Name = "IdLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Loja", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpLoja];LookUpTitle[Seleção de (Id Loja)];LookUpQuery[executeLookUpLoja];LookUpFinalize[finalizeLookUpLoja];LookUpDisplayColumns[{\"IdLoja\" : \"Id Loja\"}];LookUpColumns[{\"IdLoja\" : true}];FilterDataKey[VENDA.LOJA.ID_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<int>#IdLoja#true##10:0##Id Loja#0#true##::LookUpLoja##false#false#LOJA#LOJA#Linx.Demo.BV.PaiFilha#IQueryable###true#false", EdmKey="VENDA.LOJA.ID_LOJA")]
	    public System.Nullable<int> IdLoja
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
	    partial void OnIdVendaChanging(int value);
	    partial void OnIdVendaChanged();

	    private int _IdVenda;

	    [DataMember(IsRequired = true, Name = "IdVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Venda", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.ID_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.ID_VENDA")]
	    public int IdVenda
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
	    partial void OnIntVendaChanging(System.Nullable<int> value);
	    partial void OnIntVendaChanged();

	    private System.Nullable<int> _IntVenda;

	    [DataMember(Name = "IntVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int Venda", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.INT_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.INT_VENDA")]
	    public System.Nullable<int> IntVenda
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
	    partial void OnSmallIntVendaChanging(System.Nullable<short> value);
	    partial void OnSmallIntVendaChanged();

	    private System.Nullable<short> _SmallIntVenda;

	    [DataMember(Name = "SmallIntVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Small Int Venda", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.SMALL_INT_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.SMALL_INT_VENDA")]
	    public System.Nullable<short> SmallIntVenda
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
	    partial void OnStringVendaChanging(string value);
	    partial void OnStringVendaChanged();

	    private string _StringVenda;

	    [DataMember(Name = "StringVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Venda", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.STRING_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.STRING_VENDA")]
	    public string StringVenda
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
	    partial void OnBigIntClienteChanging(System.Nullable<long> value);
	    partial void OnBigIntClienteChanged();

	    private System.Nullable<long> _BigIntCliente;

	    [DataMember(Name = "BigIntCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Big Int Cliente", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA.CLIENTE.BIG_INT_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.BIG_INT_CLIENTE")]
	    public System.Nullable<long> BigIntCliente
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
	    partial void OnBitClienteChanging(System.Nullable<bool> value);
	    partial void OnBitClienteChanged();

	    private System.Nullable<bool> _BitCliente;

	    [DataMember(Name = "BitCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bit Cliente", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA.CLIENTE.BIT_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.BIT_CLIENTE")]
	    public System.Nullable<bool> BitCliente
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
	    partial void OnComboboxClienteChanging(byte value);
	    partial void OnComboboxClienteChanged();

	    private byte _ComboboxCliente;

	    [DataMember(IsRequired = true, Name = "ComboboxCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Cliente", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_CLIENTE];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA.CLIENTE.COMBOBOX_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.COMBOBOX_CLIENTE")]
	    public byte ComboboxCliente
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
	    partial void OnDatetimeClienteChanging(System.Nullable<DateTime> value);
	    partial void OnDatetimeClienteChanged();

	    private System.Nullable<DateTime> _DatetimeCliente;

	    [DataMember(Name = "DatetimeCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Datetime Cliente", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA.CLIENTE.DATETIME_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.DATETIME_CLIENTE")]
	    public System.Nullable<DateTime> DatetimeCliente
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
	    partial void OnDecimalClienteChanging(System.Nullable<decimal> value);
	    partial void OnDecimalClienteChanged();

	    private System.Nullable<decimal> _DecimalCliente;

	    [DataMember(Name = "DecimalCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal Cliente", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA.CLIENTE.DECIMAL_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.DECIMAL_CLIENTE")]
	    public System.Nullable<decimal> DecimalCliente
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
	    partial void OnGuidClienteChanging(System.Nullable<Guid> value);
	    partial void OnGuidClienteChanged();

	    private System.Nullable<Guid> _GuidCliente;

	    [DataMember(Name = "GuidCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Guid Cliente", Description="", Order = 6, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA.CLIENTE.GUID_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.GUID_CLIENTE")]
	    public System.Nullable<Guid> GuidCliente
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
	    partial void OnIdEstadoChanging(System.Nullable<int> value);
	    partial void OnIdEstadoChanged();

	    private System.Nullable<int> _IdEstado;

	    [DataMember(Name = "IdEstado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Estado", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA.CLIENTE.ESTADO.ID_ESTADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.ESTADO.ID_ESTADO")]
	    public System.Nullable<int> IdEstado
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
	    partial void OnIntClienteChanging(System.Nullable<int> value);
	    partial void OnIntClienteChanged();

	    private System.Nullable<int> _IntCliente;

	    [DataMember(Name = "IntCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int Cliente", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA.CLIENTE.INT_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.INT_CLIENTE")]
	    public System.Nullable<int> IntCliente
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
	    partial void OnSmallIntClienteChanging(System.Nullable<short> value);
	    partial void OnSmallIntClienteChanged();

	    private System.Nullable<short> _SmallIntCliente;

	    [DataMember(Name = "SmallIntCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Small Int Cliente", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA.CLIENTE.SMALL_INT_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.SMALL_INT_CLIENTE")]
	    public System.Nullable<short> SmallIntCliente
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
	    partial void OnStringClienteChanging(string value);
	    partial void OnStringClienteChanged();

	    private string _StringCliente;

	    [DataMember(Name = "StringCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Cliente", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA.CLIENTE.STRING_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.STRING_CLIENTE")]
	    public string StringCliente
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
	    //Extensibility Partial Method Definitions For StringEstado
	    partial void OnStringEstadoChanging(System.Nullable<string> value);
	    partial void OnStringEstadoChanged();

	    private System.Nullable<string> _StringEstado;

	    [DataMember(Name = "StringEstado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Estado", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA.CLIENTE.ESTADO.STRING_ESTADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.ESTADO.STRING_ESTADO")]
	    public System.Nullable<string> StringEstado
	    {
	    	    get
	    	    {
	    	          return _StringEstado;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringEstado != value)
	    	          {
	    	              this.ValidateProperty("StringEstado", value);
	    	              this.OnStringEstadoChanging(value);
	    	              this.RaiseDataMemberChanging("StringEstado");
	    	              this._StringEstado = value;
	    	              this.RaiseDataMemberChanged("StringEstado");
	    	              this.OnStringEstadoChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "BaseTeste.VENDA").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(LINXDEMO.BM.VENDA), QualifiedEntitySetName = "BaseTeste.VENDA" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.ID_VENDA", Source = "IdVenda", Target = "ID_VENDA", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.BIT_VENDA", Source = "BitVenda", Target = "BIT_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.INT_VENDA", Source = "IntVenda", Target = "INT_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.GUID_VENDA", Source = "GuidVenda", Target = "GUID_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.LOJA.ID_LOJA", Source = "IdLoja", Target = "ID_LOJA", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BaseTeste.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.STRING_VENDA", Source = "StringVenda", Target = "STRING_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.BIG_INT_VENDA", Source = "BigIntVenda", Target = "BIG_INT_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.DECIMAL_VENDA", Source = "DecimalVenda", Target = "DECIMAL_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.COMBOBOX_VENDA", Source = "ComboboxVenda", Target = "COMBOBOX_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.DATETIME_VENDA", Source = "DatetimeVenda", Target = "DATETIME_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.SMALL_INT_VENDA", Source = "SmallIntVenda", Target = "SMALL_INT_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.CLIENTE.ID_CLIENTE", Source = "IdCliente", Target = "ID_CLIENTE", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BaseTeste.CLIENTE", RelationPropertyName = "CLIENTE" });

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
	    	    return Linx.Demo.BV.Domains.LX_VENDA.GetValues();
	    }
	    private string _comboboxVendaName;
	    [DataMember(IsRequired = false, Name = "ComboboxVendaName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Combobox Venda", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ComboboxVendaName
	    {
	    	    get { if (this.ComboboxVenda.IsNull()) { _comboboxVendaName = String.Empty; } else { string key = this.ComboboxVenda.ToString(); var dmValues = this.GetComboboxVendaValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _comboboxVendaName) _comboboxVendaName = domainName; } return _comboboxVendaName; } set { _comboboxVendaName = value;  }
	    }
	    public Dictionary<string, string> GetComboboxClienteValues()
	    {
	    	    return Linx.Demo.BV.Domains.LX_CLIENTE.GetValues();
	    }
	    private string _comboboxClienteName;
	    [DataMember(IsRequired = false, Name = "ComboboxClienteName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Combobox Cliente", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
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

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[VendaItem];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#.VENDA_ITEM_LISTA as #Alias#];EdmEntityName[VENDA_ITEM];EntityRelations[VENDA(VENDA)#CLIENTE(CLIENTE)#ESTADO(ESTADO)#PAIS(PAIS)#LOJA(LOJA)];EdmParentEntityName[VENDA];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "VendaItem")]
	[Serializable()]
	public partial class VendaItemParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For BigIntVendaItem
	    partial void OnBigIntVendaItemChanging(System.Nullable<long> value);
	    partial void OnBigIntVendaItemChanged();

	    private System.Nullable<long> _BigIntVendaItem;

	    [DataMember(Name = "BigIntVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Big Int Venda Item", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.BIG_INT_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.BIG_INT_VENDA_ITEM")]
	    public System.Nullable<long> BigIntVendaItem
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
	    partial void OnBitVendaItemChanging(System.Nullable<bool> value);
	    partial void OnBitVendaItemChanged();

	    private System.Nullable<bool> _BitVendaItem;

	    [DataMember(Name = "BitVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bit Venda Item", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.BIT_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.BIT_VENDA_ITEM")]
	    public System.Nullable<bool> BitVendaItem
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
	    partial void OnComboboxVendaItemChanging(byte value);
	    partial void OnComboboxVendaItemChanged();

	    private byte _ComboboxVendaItem;

	    [DataMember(IsRequired = true, Name = "ComboboxVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Venda Item", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_VENDA_ITEM];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.COMBOBOX_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.COMBOBOX_VENDA_ITEM")]
	    public byte ComboboxVendaItem
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
	    partial void OnDatetimeVendaItemChanging(System.Nullable<DateTime> value);
	    partial void OnDatetimeVendaItemChanged();

	    private System.Nullable<DateTime> _DatetimeVendaItem;

	    [DataMember(Name = "DatetimeVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Datetime Venda Item", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.DATETIME_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.DATETIME_VENDA_ITEM")]
	    public System.Nullable<DateTime> DatetimeVendaItem
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
	    partial void OnDecimalVendaItemChanging(System.Nullable<decimal> value);
	    partial void OnDecimalVendaItemChanged();

	    private System.Nullable<decimal> _DecimalVendaItem;

	    [DataMember(Name = "DecimalVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal Venda Item", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.DECIMAL_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.DECIMAL_VENDA_ITEM")]
	    public System.Nullable<decimal> DecimalVendaItem
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
	    partial void OnGuidVendaItemChanging(System.Nullable<Guid> value);
	    partial void OnGuidVendaItemChanged();

	    private System.Nullable<Guid> _GuidVendaItem;

	    [DataMember(Name = "GuidVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Guid Venda Item", Description="", Order = 6, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.GUID_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.GUID_VENDA_ITEM")]
	    public System.Nullable<Guid> GuidVendaItem
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
	    partial void OnIdVendaChanging(System.Nullable<int> value);
	    partial void OnIdVendaChanged();

	    private System.Nullable<int> _IdVenda;

	    [DataMember(Name = "IdVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Venda", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.VENDA.ID_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.VENDA.ID_VENDA")]
	    public System.Nullable<int> IdVenda
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
	    partial void OnIdVendaItemChanging(int value);
	    partial void OnIdVendaItemChanged();

	    private int _IdVendaItem;

	    [DataMember(IsRequired = true, Name = "IdVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Venda Item", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.ID_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.ID_VENDA_ITEM")]
	    public int IdVendaItem
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
	    partial void OnIntVendaItemChanging(System.Nullable<int> value);
	    partial void OnIntVendaItemChanged();

	    private System.Nullable<int> _IntVendaItem;

	    [DataMember(Name = "IntVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int Venda Item", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.INT_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.INT_VENDA_ITEM")]
	    public System.Nullable<int> IntVendaItem
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
	    partial void OnSmallIntVendaItemChanging(System.Nullable<short> value);
	    partial void OnSmallIntVendaItemChanged();

	    private System.Nullable<short> _SmallIntVendaItem;

	    [DataMember(Name = "SmallIntVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Small Int Venda Item", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.SMALL_INT_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.SMALL_INT_VENDA_ITEM")]
	    public System.Nullable<short> SmallIntVendaItem
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
	    partial void OnStringVendaItemChanging(string value);
	    partial void OnStringVendaItemChanged();

	    private string _StringVendaItem;

	    [DataMember(Name = "StringVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Venda Item", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.STRING_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.STRING_VENDA_ITEM")]
	    public string StringVendaItem
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
	    partial void OnBigIntVendaChanging(System.Nullable<long> value);
	    partial void OnBigIntVendaChanged();

	    private System.Nullable<long> _BigIntVenda;

	    [DataMember(Name = "BigIntVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Big Int Venda", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.BIG_INT_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.BIG_INT_VENDA")]
	    public System.Nullable<long> BigIntVenda
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
	    partial void OnBitVendaChanging(System.Nullable<bool> value);
	    partial void OnBitVendaChanged();

	    private System.Nullable<bool> _BitVenda;

	    [DataMember(Name = "BitVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bit Venda", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.BIT_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.BIT_VENDA")]
	    public System.Nullable<bool> BitVenda
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
	    partial void OnComboboxVendaChanging(byte value);
	    partial void OnComboboxVendaChanged();

	    private byte _ComboboxVenda;

	    [DataMember(IsRequired = true, Name = "ComboboxVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Venda", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_VENDA];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.COMBOBOX_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.COMBOBOX_VENDA")]
	    public byte ComboboxVenda
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
	    partial void OnDatetimeVendaChanging(System.Nullable<DateTime> value);
	    partial void OnDatetimeVendaChanged();

	    private System.Nullable<DateTime> _DatetimeVenda;

	    [DataMember(Name = "DatetimeVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Datetime Venda", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.DATETIME_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.DATETIME_VENDA")]
	    public System.Nullable<DateTime> DatetimeVenda
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
	    partial void OnDecimalVendaChanging(System.Nullable<decimal> value);
	    partial void OnDecimalVendaChanged();

	    private System.Nullable<decimal> _DecimalVenda;

	    [DataMember(Name = "DecimalVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal Venda", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.DECIMAL_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.DECIMAL_VENDA")]
	    public System.Nullable<decimal> DecimalVenda
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
	    partial void OnGuidVendaChanging(System.Nullable<Guid> value);
	    partial void OnGuidVendaChanged();

	    private System.Nullable<Guid> _GuidVenda;

	    [DataMember(Name = "GuidVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Guid Venda", Description="", Order = 6, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.GUID_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.GUID_VENDA")]
	    public System.Nullable<Guid> GuidVenda
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
	    partial void OnIdClienteChanging(System.Nullable<int> value);
	    partial void OnIdClienteChanged();

	    private System.Nullable<int> _IdCliente;

	    [DataMember(Name = "IdCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Cliente", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.CLIENTE.ID_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.CLIENTE.ID_CLIENTE")]
	    public System.Nullable<int> IdCliente
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
	    partial void OnIdLojaChanging(System.Nullable<int> value);
	    partial void OnIdLojaChanged();

	    private System.Nullable<int> _IdLoja;

	    [DataMember(Name = "IdLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Loja", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.LOJA.ID_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.LOJA.ID_LOJA")]
	    public System.Nullable<int> IdLoja
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
	    partial void OnIntVendaChanging(System.Nullable<int> value);
	    partial void OnIntVendaChanged();

	    private System.Nullable<int> _IntVenda;

	    [DataMember(Name = "IntVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int Venda", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.INT_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.INT_VENDA")]
	    public System.Nullable<int> IntVenda
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
	    partial void OnSmallIntVendaChanging(System.Nullable<short> value);
	    partial void OnSmallIntVendaChanged();

	    private System.Nullable<short> _SmallIntVenda;

	    [DataMember(Name = "SmallIntVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Small Int Venda", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.SMALL_INT_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.SMALL_INT_VENDA")]
	    public System.Nullable<short> SmallIntVenda
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
	    partial void OnStringVendaChanging(string value);
	    partial void OnStringVendaChanged();

	    private string _StringVenda;

	    [DataMember(Name = "StringVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Venda", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.STRING_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.STRING_VENDA")]
	    public string StringVenda
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
	    partial void OnBigIntClienteChanging(System.Nullable<long> value);
	    partial void OnBigIntClienteChanged();

	    private System.Nullable<long> _BigIntCliente;

	    [DataMember(Name = "BigIntCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Big Int Cliente", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.CLIENTE.BIG_INT_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.BIG_INT_CLIENTE")]
	    public System.Nullable<long> BigIntCliente
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
	    partial void OnBitClienteChanging(System.Nullable<bool> value);
	    partial void OnBitClienteChanged();

	    private System.Nullable<bool> _BitCliente;

	    [DataMember(Name = "BitCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bit Cliente", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.CLIENTE.BIT_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.BIT_CLIENTE")]
	    public System.Nullable<bool> BitCliente
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
	    partial void OnComboboxClienteChanging(byte value);
	    partial void OnComboboxClienteChanged();

	    private byte _ComboboxCliente;

	    [DataMember(IsRequired = true, Name = "ComboboxCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Cliente", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_CLIENTE];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.CLIENTE.COMBOBOX_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.COMBOBOX_CLIENTE")]
	    public byte ComboboxCliente
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
	    partial void OnDatetimeClienteChanging(System.Nullable<DateTime> value);
	    partial void OnDatetimeClienteChanged();

	    private System.Nullable<DateTime> _DatetimeCliente;

	    [DataMember(Name = "DatetimeCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Datetime Cliente", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.CLIENTE.DATETIME_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.DATETIME_CLIENTE")]
	    public System.Nullable<DateTime> DatetimeCliente
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
	    partial void OnDecimalClienteChanging(System.Nullable<decimal> value);
	    partial void OnDecimalClienteChanged();

	    private System.Nullable<decimal> _DecimalCliente;

	    [DataMember(Name = "DecimalCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal Cliente", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.CLIENTE.DECIMAL_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.DECIMAL_CLIENTE")]
	    public System.Nullable<decimal> DecimalCliente
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
	    partial void OnGuidClienteChanging(System.Nullable<Guid> value);
	    partial void OnGuidClienteChanged();

	    private System.Nullable<Guid> _GuidCliente;

	    [DataMember(Name = "GuidCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Guid Cliente", Description="", Order = 6, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.CLIENTE.GUID_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.GUID_CLIENTE")]
	    public System.Nullable<Guid> GuidCliente
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
	    partial void OnIdEstadoChanging(System.Nullable<int> value);
	    partial void OnIdEstadoChanged();

	    private System.Nullable<int> _IdEstado;

	    [DataMember(Name = "IdEstado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Estado", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.CLIENTE.ESTADO.ID_ESTADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.ESTADO.ID_ESTADO")]
	    public System.Nullable<int> IdEstado
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
	    partial void OnIntClienteChanging(System.Nullable<int> value);
	    partial void OnIntClienteChanged();

	    private System.Nullable<int> _IntCliente;

	    [DataMember(Name = "IntCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int Cliente", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.CLIENTE.INT_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.INT_CLIENTE")]
	    public System.Nullable<int> IntCliente
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
	    partial void OnSmallIntClienteChanging(System.Nullable<short> value);
	    partial void OnSmallIntClienteChanged();

	    private System.Nullable<short> _SmallIntCliente;

	    [DataMember(Name = "SmallIntCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Small Int Cliente", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.CLIENTE.SMALL_INT_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.SMALL_INT_CLIENTE")]
	    public System.Nullable<short> SmallIntCliente
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
	    partial void OnStringClienteChanging(string value);
	    partial void OnStringClienteChanged();

	    private string _StringCliente;

	    [DataMember(Name = "StringCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Cliente", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.CLIENTE.STRING_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.STRING_CLIENTE")]
	    public string StringCliente
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
	    //Extensibility Partial Method Definitions For StringEstado
	    partial void OnStringEstadoChanging(System.Nullable<string> value);
	    partial void OnStringEstadoChanged();

	    private System.Nullable<string> _StringEstado;

	    [DataMember(Name = "StringEstado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Estado", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.CLIENTE.ESTADO.STRING_ESTADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.ESTADO.STRING_ESTADO")]
	    public System.Nullable<string> StringEstado
	    {
	    	    get
	    	    {
	    	          return _StringEstado;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringEstado != value)
	    	          {
	    	              this.ValidateProperty("StringEstado", value);
	    	              this.OnStringEstadoChanging(value);
	    	              this.RaiseDataMemberChanging("StringEstado");
	    	              this._StringEstado = value;
	    	              this.RaiseDataMemberChanged("StringEstado");
	    	              this.OnStringEstadoChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "BaseTeste.VENDA_ITEM").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(LINXDEMO.BM.VENDA_ITEM), QualifiedEntitySetName = "BaseTeste.VENDA_ITEM" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.ID_VENDA_ITEM", Source = "IdVendaItem", Target = "ID_VENDA_ITEM", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.BIT_VENDA_ITEM", Source = "BitVendaItem", Target = "BIT_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.VENDA.ID_VENDA", Source = "IdVenda", Target = "ID_VENDA", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BaseTeste.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.INT_VENDA_ITEM", Source = "IntVendaItem", Target = "INT_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.GUID_VENDA_ITEM", Source = "GuidVendaItem", Target = "GUID_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.STRING_VENDA_ITEM", Source = "StringVendaItem", Target = "STRING_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.BIG_INT_VENDA_ITEM", Source = "BigIntVendaItem", Target = "BIG_INT_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.DECIMAL_VENDA_ITEM", Source = "DecimalVendaItem", Target = "DECIMAL_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.COMBOBOX_VENDA_ITEM", Source = "ComboboxVendaItem", Target = "COMBOBOX_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.DATETIME_VENDA_ITEM", Source = "DatetimeVendaItem", Target = "DATETIME_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.SMALL_INT_VENDA_ITEM", Source = "SmallIntVendaItem", Target = "SMALL_INT_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });

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
	    	    return Linx.Demo.BV.Domains.LX_VENDA_ITEM.GetValues();
	    }
	    private string _comboboxVendaItemName;
	    [DataMember(IsRequired = false, Name = "ComboboxVendaItemName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Combobox Venda Item", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ComboboxVendaItemName
	    {
	    	    get { if (this.ComboboxVendaItem.IsNull()) { _comboboxVendaItemName = String.Empty; } else { string key = this.ComboboxVendaItem.ToString(); var dmValues = this.GetComboboxVendaItemValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _comboboxVendaItemName) _comboboxVendaItemName = domainName; } return _comboboxVendaItemName; } set { _comboboxVendaItemName = value;  }
	    }
	    public Dictionary<string, string> GetComboboxVendaValues()
	    {
	    	    return Linx.Demo.BV.Domains.LX_VENDA.GetValues();
	    }
	    private string _comboboxVendaName;
	    [DataMember(IsRequired = false, Name = "ComboboxVendaName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Combobox Venda", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ComboboxVendaName
	    {
	    	    get { if (this.ComboboxVenda.IsNull()) { _comboboxVendaName = String.Empty; } else { string key = this.ComboboxVenda.ToString(); var dmValues = this.GetComboboxVendaValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _comboboxVendaName) _comboboxVendaName = domainName; } return _comboboxVendaName; } set { _comboboxVendaName = value;  }
	    }
	    public Dictionary<string, string> GetComboboxClienteValues()
	    {
	    	    return Linx.Demo.BV.Domains.LX_CLIENTE.GetValues();
	    }
	    private string _comboboxClienteName;
	    [DataMember(IsRequired = false, Name = "ComboboxClienteName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Combobox Cliente", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
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

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[VendaAtacado];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#.VENDA_ATACADO_LISTA as #Alias#];EdmEntityName[VENDA_ATACADO];EntityRelations[CLIENTE(CLIENTE)#ESTADO(ESTADO)#PAIS(PAIS)];EdmParentEntityName[CLIENTE];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "VendaAtacado")]
	[Serializable()]
	public partial class VendaAtacadoParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For BigIntVendaAtacado
	    partial void OnBigIntVendaAtacadoChanging(System.Nullable<long> value);
	    partial void OnBigIntVendaAtacadoChanged();

	    private System.Nullable<long> _BigIntVendaAtacado;

	    [DataMember(Name = "BigIntVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Big Int Venda Atacado", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.BIG_INT_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.BIG_INT_VENDA_ATACADO")]
	    public System.Nullable<long> BigIntVendaAtacado
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
	    partial void OnBitVendaAtacadoChanging(System.Nullable<bool> value);
	    partial void OnBitVendaAtacadoChanged();

	    private System.Nullable<bool> _BitVendaAtacado;

	    [DataMember(Name = "BitVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bit Venda Atacado", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.BIT_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.BIT_VENDA_ATACADO")]
	    public System.Nullable<bool> BitVendaAtacado
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
	    partial void OnComboboxVendaAtacadoChanging(byte value);
	    partial void OnComboboxVendaAtacadoChanged();

	    private byte _ComboboxVendaAtacado;

	    [DataMember(IsRequired = true, Name = "ComboboxVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Venda Atacado", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_VENDA_ATACADO];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.COMBOBOX_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.COMBOBOX_VENDA_ATACADO")]
	    public byte ComboboxVendaAtacado
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
	    partial void OnDatetimeVendaAtacadoChanging(System.Nullable<DateTime> value);
	    partial void OnDatetimeVendaAtacadoChanged();

	    private System.Nullable<DateTime> _DatetimeVendaAtacado;

	    [DataMember(Name = "DatetimeVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Datetime Venda Atacado", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.DATETIME_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.DATETIME_VENDA_ATACADO")]
	    public System.Nullable<DateTime> DatetimeVendaAtacado
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
	    partial void OnDecimalVendaAtacadoChanging(System.Nullable<decimal> value);
	    partial void OnDecimalVendaAtacadoChanged();

	    private System.Nullable<decimal> _DecimalVendaAtacado;

	    [DataMember(Name = "DecimalVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal Venda Atacado", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.DECIMAL_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.DECIMAL_VENDA_ATACADO")]
	    public System.Nullable<decimal> DecimalVendaAtacado
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
	    partial void OnGuidVendaAtacadoChanging(System.Nullable<Guid> value);
	    partial void OnGuidVendaAtacadoChanged();

	    private System.Nullable<Guid> _GuidVendaAtacado;

	    [DataMember(Name = "GuidVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Guid Venda Atacado", Description="", Order = 6, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.GUID_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.GUID_VENDA_ATACADO")]
	    public System.Nullable<Guid> GuidVendaAtacado
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
	    partial void OnIdClienteChanging(System.Nullable<int> value);
	    partial void OnIdClienteChanged();

	    private System.Nullable<int> _IdCliente;

	    [DataMember(Name = "IdCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Cliente", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.CLIENTE.ID_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.CLIENTE.ID_CLIENTE")]
	    public System.Nullable<int> IdCliente
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
	    partial void OnIdVendaAtacadoChanging(int value);
	    partial void OnIdVendaAtacadoChanged();

	    private int _IdVendaAtacado;

	    [DataMember(IsRequired = true, Name = "IdVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Venda Atacado", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.ID_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.ID_VENDA_ATACADO")]
	    public int IdVendaAtacado
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
	    partial void OnIntVendaAtacadoChanging(System.Nullable<int> value);
	    partial void OnIntVendaAtacadoChanged();

	    private System.Nullable<int> _IntVendaAtacado;

	    [DataMember(Name = "IntVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int Venda Atacado", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.INT_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.INT_VENDA_ATACADO")]
	    public System.Nullable<int> IntVendaAtacado
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
	    partial void OnSmallIntVendaAtacadoChanging(System.Nullable<short> value);
	    partial void OnSmallIntVendaAtacadoChanged();

	    private System.Nullable<short> _SmallIntVendaAtacado;

	    [DataMember(Name = "SmallIntVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Small Int Venda Atacado", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.SMALL_INT_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.SMALL_INT_VENDA_ATACADO")]
	    public System.Nullable<short> SmallIntVendaAtacado
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
	    partial void OnStringVendaAtacadoChanging(string value);
	    partial void OnStringVendaAtacadoChanged();

	    private string _StringVendaAtacado;

	    [DataMember(Name = "StringVendaAtacado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Venda Atacado", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ATACADO.STRING_VENDA_ATACADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ATACADO.STRING_VENDA_ATACADO")]
	    public string StringVendaAtacado
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
	    partial void OnBigIntClienteChanging(System.Nullable<long> value);
	    partial void OnBigIntClienteChanged();

	    private System.Nullable<long> _BigIntCliente;

	    [DataMember(Name = "BigIntCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Big Int Cliente", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ATACADO.CLIENTE.BIG_INT_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.BIG_INT_CLIENTE")]
	    public System.Nullable<long> BigIntCliente
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
	    partial void OnBitClienteChanging(System.Nullable<bool> value);
	    partial void OnBitClienteChanged();

	    private System.Nullable<bool> _BitCliente;

	    [DataMember(Name = "BitCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bit Cliente", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ATACADO.CLIENTE.BIT_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.BIT_CLIENTE")]
	    public System.Nullable<bool> BitCliente
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
	    partial void OnComboboxClienteChanging(byte value);
	    partial void OnComboboxClienteChanged();

	    private byte _ComboboxCliente;

	    [DataMember(IsRequired = true, Name = "ComboboxCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Cliente", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_CLIENTE];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ATACADO.CLIENTE.COMBOBOX_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.COMBOBOX_CLIENTE")]
	    public byte ComboboxCliente
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
	    partial void OnDatetimeClienteChanging(System.Nullable<DateTime> value);
	    partial void OnDatetimeClienteChanged();

	    private System.Nullable<DateTime> _DatetimeCliente;

	    [DataMember(Name = "DatetimeCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Datetime Cliente", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ATACADO.CLIENTE.DATETIME_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.DATETIME_CLIENTE")]
	    public System.Nullable<DateTime> DatetimeCliente
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
	    partial void OnDecimalClienteChanging(System.Nullable<decimal> value);
	    partial void OnDecimalClienteChanged();

	    private System.Nullable<decimal> _DecimalCliente;

	    [DataMember(Name = "DecimalCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal Cliente", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ATACADO.CLIENTE.DECIMAL_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.DECIMAL_CLIENTE")]
	    public System.Nullable<decimal> DecimalCliente
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
	    partial void OnGuidClienteChanging(System.Nullable<Guid> value);
	    partial void OnGuidClienteChanged();

	    private System.Nullable<Guid> _GuidCliente;

	    [DataMember(Name = "GuidCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Guid Cliente", Description="", Order = 6, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ATACADO.CLIENTE.GUID_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.GUID_CLIENTE")]
	    public System.Nullable<Guid> GuidCliente
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
	    partial void OnIdEstadoChanging(System.Nullable<int> value);
	    partial void OnIdEstadoChanged();

	    private System.Nullable<int> _IdEstado;

	    [DataMember(Name = "IdEstado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Estado", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ATACADO.CLIENTE.ESTADO.ID_ESTADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.ESTADO.ID_ESTADO")]
	    public System.Nullable<int> IdEstado
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
	    partial void OnIntClienteChanging(System.Nullable<int> value);
	    partial void OnIntClienteChanged();

	    private System.Nullable<int> _IntCliente;

	    [DataMember(Name = "IntCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int Cliente", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ATACADO.CLIENTE.INT_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.INT_CLIENTE")]
	    public System.Nullable<int> IntCliente
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
	    partial void OnSmallIntClienteChanging(System.Nullable<short> value);
	    partial void OnSmallIntClienteChanged();

	    private System.Nullable<short> _SmallIntCliente;

	    [DataMember(Name = "SmallIntCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Small Int Cliente", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ATACADO.CLIENTE.SMALL_INT_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.SMALL_INT_CLIENTE")]
	    public System.Nullable<short> SmallIntCliente
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
	    partial void OnStringClienteChanging(string value);
	    partial void OnStringClienteChanged();

	    private string _StringCliente;

	    [DataMember(Name = "StringCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Cliente", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ATACADO.CLIENTE.STRING_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.STRING_CLIENTE")]
	    public string StringCliente
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
	    //Extensibility Partial Method Definitions For StringEstado
	    partial void OnStringEstadoChanging(System.Nullable<string> value);
	    partial void OnStringEstadoChanged();

	    private System.Nullable<string> _StringEstado;

	    [DataMember(Name = "StringEstado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Estado", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ATACADO.CLIENTE.ESTADO.STRING_ESTADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.ESTADO.STRING_ESTADO")]
	    public System.Nullable<string> StringEstado
	    {
	    	    get
	    	    {
	    	          return _StringEstado;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringEstado != value)
	    	          {
	    	              this.ValidateProperty("StringEstado", value);
	    	              this.OnStringEstadoChanging(value);
	    	              this.RaiseDataMemberChanging("StringEstado");
	    	              this._StringEstado = value;
	    	              this.RaiseDataMemberChanged("StringEstado");
	    	              this.OnStringEstadoChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "BaseTeste.VENDA_ATACADO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(LINXDEMO.BM.VENDA_ATACADO), QualifiedEntitySetName = "BaseTeste.VENDA_ATACADO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.ID_VENDA_ATACADO", Source = "IdVendaAtacado", Target = "ID_VENDA_ATACADO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.BIT_VENDA_ATACADO", Source = "BitVendaAtacado", Target = "BIT_VENDA_ATACADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.INT_VENDA_ATACADO", Source = "IntVendaAtacado", Target = "INT_VENDA_ATACADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.GUID_VENDA_ATACADO", Source = "GuidVendaAtacado", Target = "GUID_VENDA_ATACADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.CLIENTE.ID_CLIENTE", Source = "IdCliente", Target = "ID_CLIENTE", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BaseTeste.CLIENTE", RelationPropertyName = "CLIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.STRING_VENDA_ATACADO", Source = "StringVendaAtacado", Target = "STRING_VENDA_ATACADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.BIG_INT_VENDA_ATACADO", Source = "BigIntVendaAtacado", Target = "BIG_INT_VENDA_ATACADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.DECIMAL_VENDA_ATACADO", Source = "DecimalVendaAtacado", Target = "DECIMAL_VENDA_ATACADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.COMBOBOX_VENDA_ATACADO", Source = "ComboboxVendaAtacado", Target = "COMBOBOX_VENDA_ATACADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.DATETIME_VENDA_ATACADO", Source = "DatetimeVendaAtacado", Target = "DATETIME_VENDA_ATACADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ATACADO.SMALL_INT_VENDA_ATACADO", Source = "SmallIntVendaAtacado", Target = "SMALL_INT_VENDA_ATACADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ATACADO", RelationPropertyName = "VENDA_ATACADO" });

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
	    	    return Linx.Demo.BV.Domains.LX_VENDA_ATACADO.GetValues();
	    }
	    private string _comboboxVendaAtacadoName;
	    [DataMember(IsRequired = false, Name = "ComboboxVendaAtacadoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Combobox Venda Atacado", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ComboboxVendaAtacadoName
	    {
	    	    get { if (this.ComboboxVendaAtacado.IsNull()) { _comboboxVendaAtacadoName = String.Empty; } else { string key = this.ComboboxVendaAtacado.ToString(); var dmValues = this.GetComboboxVendaAtacadoValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _comboboxVendaAtacadoName) _comboboxVendaAtacadoName = domainName; } return _comboboxVendaAtacadoName; } set { _comboboxVendaAtacadoName = value;  }
	    }
	    public Dictionary<string, string> GetComboboxClienteValues()
	    {
	    	    return Linx.Demo.BV.Domains.LX_CLIENTE.GetValues();
	    }
	    private string _comboboxClienteName;
	    [DataMember(IsRequired = false, Name = "ComboboxClienteName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Combobox Cliente", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
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

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#.VENDEDOR_LISTA as #Alias#];EdmEntityName[VENDEDOR];EntityRelations[LOJA(LOJA)];EdmParentEntityName[LOJA];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "Vendedor")]
	[Serializable()]
	public partial class VendedorParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For BitVendedor
	    partial void OnBitVendedorChanging(System.Nullable<bool> value);
	    partial void OnBitVendedorChanged();

	    private System.Nullable<bool> _BitVendedor;

	    [DataMember(Name = "BitVendedor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bit Vendedor", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDEDOR.BIT_VENDEDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDEDOR.BIT_VENDEDOR")]
	    public System.Nullable<bool> BitVendedor
	    {
	    	    get
	    	    {
	    	          return _BitVendedor;
	    	    }
	    	    set
	    	    {
	    	          if (this._BitVendedor != value)
	    	          {
	    	              this.ValidateProperty("BitVendedor", value);
	    	              this.OnBitVendedorChanging(value);
	    	              this.RaiseDataMemberChanging("BitVendedor");
	    	              this._BitVendedor = value;
	    	              this.RaiseDataMemberChanged("BitVendedor");
	    	              this.OnBitVendedorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ComboboxVendedor
	    partial void OnComboboxVendedorChanging(byte value);
	    partial void OnComboboxVendedorChanged();

	    private byte _ComboboxVendedor;

	    [DataMember(IsRequired = true, Name = "ComboboxVendedor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Vendedor", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_VENDEDOR];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDEDOR.COMBOBOX_VENDEDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDEDOR.COMBOBOX_VENDEDOR")]
	    public byte ComboboxVendedor
	    {
	    	    get
	    	    {
	    	          return _ComboboxVendedor;
	    	    }
	    	    set
	    	    {
	    	          if (this._ComboboxVendedor != value)
	    	          {
	    	              this.ValidateProperty("ComboboxVendedor", value);
	    	              this.OnComboboxVendedorChanging(value);
	    	              this.RaiseDataMemberChanging("ComboboxVendedor");
	    	              this._ComboboxVendedor = value;
	    	              this.RaiseDataMemberChanged("ComboboxVendedor");
	    	              this.OnComboboxVendedorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DatetimeVendedor
	    partial void OnDatetimeVendedorChanging(System.Nullable<DateTime> value);
	    partial void OnDatetimeVendedorChanged();

	    private System.Nullable<DateTime> _DatetimeVendedor;

	    [DataMember(Name = "DatetimeVendedor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Datetime Vendedor", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDEDOR.DATETIME_VENDEDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDEDOR.DATETIME_VENDEDOR")]
	    public System.Nullable<DateTime> DatetimeVendedor
	    {
	    	    get
	    	    {
	    	          return _DatetimeVendedor;
	    	    }
	    	    set
	    	    {
	    	          if (this._DatetimeVendedor != value)
	    	          {
	    	              this.ValidateProperty("DatetimeVendedor", value);
	    	              this.OnDatetimeVendedorChanging(value);
	    	              this.RaiseDataMemberChanging("DatetimeVendedor");
	    	              this._DatetimeVendedor = value;
	    	              this.RaiseDataMemberChanged("DatetimeVendedor");
	    	              this.OnDatetimeVendedorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DecimalVendedor
	    partial void OnDecimalVendedorChanging(System.Nullable<decimal> value);
	    partial void OnDecimalVendedorChanged();

	    private System.Nullable<decimal> _DecimalVendedor;

	    [DataMember(Name = "DecimalVendedor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal Vendedor", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDEDOR.DECIMAL_VENDEDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDEDOR.DECIMAL_VENDEDOR")]
	    public System.Nullable<decimal> DecimalVendedor
	    {
	    	    get
	    	    {
	    	          return _DecimalVendedor;
	    	    }
	    	    set
	    	    {
	    	          if (this._DecimalVendedor != value)
	    	          {
	    	              this.ValidateProperty("DecimalVendedor", value);
	    	              this.OnDecimalVendedorChanging(value);
	    	              this.RaiseDataMemberChanging("DecimalVendedor");
	    	              this._DecimalVendedor = value;
	    	              this.RaiseDataMemberChanged("DecimalVendedor");
	    	              this.OnDecimalVendedorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For GuidVendedor
	    partial void OnGuidVendedorChanging(System.Nullable<Guid> value);
	    partial void OnGuidVendedorChanged();

	    private System.Nullable<Guid> _GuidVendedor;

	    [DataMember(Name = "GuidVendedor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Guid Vendedor", Description="", Order = 6, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDEDOR.GUID_VENDEDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDEDOR.GUID_VENDEDOR")]
	    public System.Nullable<Guid> GuidVendedor
	    {
	    	    get
	    	    {
	    	          return _GuidVendedor;
	    	    }
	    	    set
	    	    {
	    	          if (this._GuidVendedor != value)
	    	          {
	    	              this.ValidateProperty("GuidVendedor", value);
	    	              this.OnGuidVendedorChanging(value);
	    	              this.RaiseDataMemberChanging("GuidVendedor");
	    	              this._GuidVendedor = value;
	    	              this.RaiseDataMemberChanged("GuidVendedor");
	    	              this.OnGuidVendedorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLoja
	    partial void OnIdLojaChanging(System.Nullable<int> value);
	    partial void OnIdLojaChanged();

	    private System.Nullable<int> _IdLoja;

	    [DataMember(Name = "IdLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Loja", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDEDOR.LOJA.ID_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDEDOR.LOJA.ID_LOJA")]
	    public System.Nullable<int> IdLoja
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
	    //Extensibility Partial Method Definitions For IdVendedor
	    partial void OnIdVendedorChanging(int value);
	    partial void OnIdVendedorChanged();

	    private int _IdVendedor;

	    [DataMember(IsRequired = true, Name = "IdVendedor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Vendedor", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDEDOR.ID_VENDEDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDEDOR.ID_VENDEDOR")]
	    public int IdVendedor
	    {
	    	    get
	    	    {
	    	          return _IdVendedor;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdVendedor != value)
	    	          {
	    	              this.ValidateProperty("IdVendedor", value);
	    	              this.OnIdVendedorChanging(value);
	    	              this.RaiseDataMemberChanging("IdVendedor");
	    	              this._IdVendedor = value;
	    	              this.RaiseDataMemberChanged("IdVendedor");
	    	              this.OnIdVendedorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IntVendedor
	    partial void OnIntVendedorChanging(System.Nullable<int> value);
	    partial void OnIntVendedorChanged();

	    private System.Nullable<int> _IntVendedor;

	    [DataMember(Name = "IntVendedor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int Vendedor", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDEDOR.INT_VENDEDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDEDOR.INT_VENDEDOR")]
	    public System.Nullable<int> IntVendedor
	    {
	    	    get
	    	    {
	    	          return _IntVendedor;
	    	    }
	    	    set
	    	    {
	    	          if (this._IntVendedor != value)
	    	          {
	    	              this.ValidateProperty("IntVendedor", value);
	    	              this.OnIntVendedorChanging(value);
	    	              this.RaiseDataMemberChanging("IntVendedor");
	    	              this._IntVendedor = value;
	    	              this.RaiseDataMemberChanged("IntVendedor");
	    	              this.OnIntVendedorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For SmallIntVendedor
	    partial void OnSmallIntVendedorChanging(System.Nullable<short> value);
	    partial void OnSmallIntVendedorChanged();

	    private System.Nullable<short> _SmallIntVendedor;

	    [DataMember(Name = "SmallIntVendedor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Small Int Vendedor", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[5:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDEDOR.SMALL_INT_VENDEDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDEDOR.SMALL_INT_VENDEDOR")]
	    public System.Nullable<short> SmallIntVendedor
	    {
	    	    get
	    	    {
	    	          return _SmallIntVendedor;
	    	    }
	    	    set
	    	    {
	    	          if (this._SmallIntVendedor != value)
	    	          {
	    	              this.ValidateProperty("SmallIntVendedor", value);
	    	              this.OnSmallIntVendedorChanging(value);
	    	              this.RaiseDataMemberChanging("SmallIntVendedor");
	    	              this._SmallIntVendedor = value;
	    	              this.RaiseDataMemberChanged("SmallIntVendedor");
	    	              this.OnSmallIntVendedorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringVendedor
	    partial void OnStringVendedorChanging(string value);
	    partial void OnStringVendedorChanged();

	    private string _StringVendedor;

	    [DataMember(Name = "StringVendedor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Vendedor", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDEDOR.STRING_VENDEDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDEDOR.STRING_VENDEDOR")]
	    public string StringVendedor
	    {
	    	    get
	    	    {
	    	          return _StringVendedor;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringVendedor != value)
	    	          {
	    	              this.ValidateProperty("StringVendedor", value);
	    	              this.OnStringVendedorChanging(value);
	    	              this.RaiseDataMemberChanging("StringVendedor");
	    	              this._StringVendedor = value;
	    	              this.RaiseDataMemberChanged("StringVendedor");
	    	              this.OnStringVendedorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For BigIntLoja
	    partial void OnBigIntLojaChanging(System.Nullable<long> value);
	    partial void OnBigIntLojaChanged();

	    private System.Nullable<long> _BigIntLoja;

	    [DataMember(Name = "BigIntLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Big Int Loja", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[19:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDEDOR.LOJA.BIG_INT_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.BIG_INT_LOJA")]
	    public System.Nullable<long> BigIntLoja
	    {
	    	    get
	    	    {
	    	          return _BigIntLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._BigIntLoja != value)
	    	          {
	    	              this.ValidateProperty("BigIntLoja", value);
	    	              this.OnBigIntLojaChanging(value);
	    	              this.RaiseDataMemberChanging("BigIntLoja");
	    	              this._BigIntLoja = value;
	    	              this.RaiseDataMemberChanged("BigIntLoja");
	    	              this.OnBigIntLojaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For BitLoja
	    partial void OnBitLojaChanging(System.Nullable<bool> value);
	    partial void OnBitLojaChanged();

	    private System.Nullable<bool> _BitLoja;

	    [DataMember(Name = "BitLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bit Loja", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDEDOR.LOJA.BIT_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.BIT_LOJA")]
	    public System.Nullable<bool> BitLoja
	    {
	    	    get
	    	    {
	    	          return _BitLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._BitLoja != value)
	    	          {
	    	              this.ValidateProperty("BitLoja", value);
	    	              this.OnBitLojaChanging(value);
	    	              this.RaiseDataMemberChanging("BitLoja");
	    	              this._BitLoja = value;
	    	              this.RaiseDataMemberChanged("BitLoja");
	    	              this.OnBitLojaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ComboboxLoja
	    partial void OnComboboxLojaChanging(byte value);
	    partial void OnComboboxLojaChanged();

	    private byte _ComboboxLoja;

	    [DataMember(IsRequired = true, Name = "ComboboxLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Loja", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_LOJA];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDEDOR.LOJA.COMBOBOX_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.COMBOBOX_LOJA")]
	    public byte ComboboxLoja
	    {
	    	    get
	    	    {
	    	          return _ComboboxLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._ComboboxLoja != value)
	    	          {
	    	              this.ValidateProperty("ComboboxLoja", value);
	    	              this.OnComboboxLojaChanging(value);
	    	              this.RaiseDataMemberChanging("ComboboxLoja");
	    	              this._ComboboxLoja = value;
	    	              this.RaiseDataMemberChanged("ComboboxLoja");
	    	              this.OnComboboxLojaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DatetimeLoja
	    partial void OnDatetimeLojaChanging(System.Nullable<DateTime> value);
	    partial void OnDatetimeLojaChanged();

	    private System.Nullable<DateTime> _DatetimeLoja;

	    [DataMember(Name = "DatetimeLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Datetime Loja", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDEDOR.LOJA.DATETIME_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.DATETIME_LOJA")]
	    public System.Nullable<DateTime> DatetimeLoja
	    {
	    	    get
	    	    {
	    	          return _DatetimeLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._DatetimeLoja != value)
	    	          {
	    	              this.ValidateProperty("DatetimeLoja", value);
	    	              this.OnDatetimeLojaChanging(value);
	    	              this.RaiseDataMemberChanging("DatetimeLoja");
	    	              this._DatetimeLoja = value;
	    	              this.RaiseDataMemberChanged("DatetimeLoja");
	    	              this.OnDatetimeLojaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DecimalLoja
	    partial void OnDecimalLojaChanging(System.Nullable<decimal> value);
	    partial void OnDecimalLojaChanged();

	    private System.Nullable<decimal> _DecimalLoja;

	    [DataMember(Name = "DecimalLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal Loja", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDEDOR.LOJA.DECIMAL_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.DECIMAL_LOJA")]
	    public System.Nullable<decimal> DecimalLoja
	    {
	    	    get
	    	    {
	    	          return _DecimalLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._DecimalLoja != value)
	    	          {
	    	              this.ValidateProperty("DecimalLoja", value);
	    	              this.OnDecimalLojaChanging(value);
	    	              this.RaiseDataMemberChanging("DecimalLoja");
	    	              this._DecimalLoja = value;
	    	              this.RaiseDataMemberChanged("DecimalLoja");
	    	              this.OnDecimalLojaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For GuidLoja
	    partial void OnGuidLojaChanging(System.Nullable<Guid> value);
	    partial void OnGuidLojaChanged();

	    private System.Nullable<Guid> _GuidLoja;

	    [DataMember(Name = "GuidLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Guid Loja", Description="", Order = 6, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDEDOR.LOJA.GUID_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.GUID_LOJA")]
	    public System.Nullable<Guid> GuidLoja
	    {
	    	    get
	    	    {
	    	          return _GuidLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._GuidLoja != value)
	    	          {
	    	              this.ValidateProperty("GuidLoja", value);
	    	              this.OnGuidLojaChanging(value);
	    	              this.RaiseDataMemberChanging("GuidLoja");
	    	              this._GuidLoja = value;
	    	              this.RaiseDataMemberChanged("GuidLoja");
	    	              this.OnGuidLojaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IntLoja
	    partial void OnIntLojaChanging(System.Nullable<int> value);
	    partial void OnIntLojaChanged();

	    private System.Nullable<int> _IntLoja;

	    [DataMember(Name = "IntLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int Loja", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDEDOR.LOJA.INT_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.INT_LOJA")]
	    public System.Nullable<int> IntLoja
	    {
	    	    get
	    	    {
	    	          return _IntLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._IntLoja != value)
	    	          {
	    	              this.ValidateProperty("IntLoja", value);
	    	              this.OnIntLojaChanging(value);
	    	              this.RaiseDataMemberChanging("IntLoja");
	    	              this._IntLoja = value;
	    	              this.RaiseDataMemberChanged("IntLoja");
	    	              this.OnIntLojaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For SmallIntLoja
	    partial void OnSmallIntLojaChanging(System.Nullable<short> value);
	    partial void OnSmallIntLojaChanged();

	    private System.Nullable<short> _SmallIntLoja;

	    [DataMember(Name = "SmallIntLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Small Int Loja", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[5:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDEDOR.LOJA.SMALL_INT_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.SMALL_INT_LOJA")]
	    public System.Nullable<short> SmallIntLoja
	    {
	    	    get
	    	    {
	    	          return _SmallIntLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._SmallIntLoja != value)
	    	          {
	    	              this.ValidateProperty("SmallIntLoja", value);
	    	              this.OnSmallIntLojaChanging(value);
	    	              this.RaiseDataMemberChanging("SmallIntLoja");
	    	              this._SmallIntLoja = value;
	    	              this.RaiseDataMemberChanged("SmallIntLoja");
	    	              this.OnSmallIntLojaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringLoja
	    partial void OnStringLojaChanging(string value);
	    partial void OnStringLojaChanged();

	    private string _StringLoja;

	    [DataMember(Name = "StringLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Loja", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDEDOR.LOJA.STRING_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.STRING_LOJA")]
	    public string StringLoja
	    {
	    	    get
	    	    {
	    	          return _StringLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringLoja != value)
	    	          {
	    	              this.ValidateProperty("StringLoja", value);
	    	              this.OnStringLojaChanging(value);
	    	              this.RaiseDataMemberChanging("StringLoja");
	    	              this._StringLoja = value;
	    	              this.RaiseDataMemberChanged("StringLoja");
	    	              this.OnStringLojaChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "BaseTeste.VENDEDOR").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(LINXDEMO.BM.VENDEDOR), QualifiedEntitySetName = "BaseTeste.VENDEDOR" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDEDOR.ID_VENDEDOR", Source = "IdVendedor", Target = "ID_VENDEDOR", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDEDOR", RelationPropertyName = "VENDEDOR" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDEDOR.BIT_VENDEDOR", Source = "BitVendedor", Target = "BIT_VENDEDOR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDEDOR", RelationPropertyName = "VENDEDOR" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDEDOR.LOJA.ID_LOJA", Source = "IdLoja", Target = "ID_LOJA", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BaseTeste.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDEDOR.INT_VENDEDOR", Source = "IntVendedor", Target = "INT_VENDEDOR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDEDOR", RelationPropertyName = "VENDEDOR" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDEDOR.GUID_VENDEDOR", Source = "GuidVendedor", Target = "GUID_VENDEDOR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDEDOR", RelationPropertyName = "VENDEDOR" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDEDOR.STRING_VENDEDOR", Source = "StringVendedor", Target = "STRING_VENDEDOR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDEDOR", RelationPropertyName = "VENDEDOR" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDEDOR.DECIMAL_VENDEDOR", Source = "DecimalVendedor", Target = "DECIMAL_VENDEDOR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDEDOR", RelationPropertyName = "VENDEDOR" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDEDOR.COMBOBOX_VENDEDOR", Source = "ComboboxVendedor", Target = "COMBOBOX_VENDEDOR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDEDOR", RelationPropertyName = "VENDEDOR" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDEDOR.DATETIME_VENDEDOR", Source = "DatetimeVendedor", Target = "DATETIME_VENDEDOR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDEDOR", RelationPropertyName = "VENDEDOR" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDEDOR.SMALL_INT_VENDEDOR", Source = "SmallIntVendedor", Target = "SMALL_INT_VENDEDOR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDEDOR", RelationPropertyName = "VENDEDOR" });

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
	 

	    public Dictionary<string, string> GetComboboxVendedorValues()
	    {
	    	    return Linx.Demo.BV.Domains.LX_VENDEDOR.GetValues();
	    }
	    private string _comboboxVendedorName;
	    [DataMember(IsRequired = false, Name = "ComboboxVendedorName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Combobox Vendedor", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ComboboxVendedorName
	    {
	    	    get { if (this.ComboboxVendedor.IsNull()) { _comboboxVendedorName = String.Empty; } else { string key = this.ComboboxVendedor.ToString(); var dmValues = this.GetComboboxVendedorValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _comboboxVendedorName) _comboboxVendedorName = domainName; } return _comboboxVendedorName; } set { _comboboxVendedorName = value;  }
	    }
	    public Dictionary<string, string> GetComboboxLojaValues()
	    {
	    	    return Linx.Demo.BV.Domains.LX_LOJA.GetValues();
	    }
	    private string _comboboxLojaName;
	    [DataMember(IsRequired = false, Name = "ComboboxLojaName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Combobox Loja", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ComboboxLojaName
	    {
	    	    get { if (this.ComboboxLoja.IsNull()) { _comboboxLojaName = String.Empty; } else { string key = this.ComboboxLoja.ToString(); var dmValues = this.GetComboboxLojaValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _comboboxLojaName) _comboboxLojaName = domainName; } return _comboboxLojaName; } set { _comboboxLojaName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	//////////////////////// DomainService Class V1 ///////////////////////
	///////////////////////////////////////////////////////////////////////
	[EnableClientAccess()]	
	[DomainIdentifier("ProcessorOverviewPaiFilhaDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class PaiFilhaDomainService : DomainService, IDataServiceContext 
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
	
	    private LINXDEMO.BM.BaseTeste _dbContext;
	    protected LINXDEMO.BM.BaseTeste DbContext 
	    { 
	    	get 
	    	{
	        	if (this._dbContext == null)
	        	{
	        		this._dbContext = new LINXDEMO.BM.BaseTeste(connectionString, this.Headers);
	        		((System.Data.Entity.Infrastructure.IObjectContextAdapter)this._dbContext).ObjectContext.CommandTimeout = 180;
	        		this._hasGpeconControl = (!(this._dbContext.IsUserMultiGpecon && this._dbContext.IdGpecon == this._dbContext.IdLinx) && this._dbContext.IdGpecon > 0);		
	        	}
	        	return this._dbContext;
	    	}
	    }

	    public string GetModelAssemblyName()
	    {
	        return typeof(LINXDEMO.BM.BaseTeste).Assembly.FullName;
	    }

	    public System.Data.Entity.Database Database
	    {
	        get { return this.DbContext.Database; }
	    }

		
	    public PaiFilhaDomainService() : this("", null, null) { }
	    public PaiFilhaDomainService(string connectionString) : this(connectionString, null, null) { }
	    public PaiFilhaDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public PaiFilhaDomainService(LINXDEMO.BM.BaseTeste dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public PaiFilhaDomainService(string connectionString, LINXDEMO.BM.BaseTeste dataContext, Dictionary<string, string> headers) : base() 
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
	    public LINXDEMO.BM.BaseTeste GetEDM()
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
 	        var _LojaElements = changeSet.ChangeSetEntries.Where(e => e.Entity is Loja && e.Entity.GetType().Name == "Loja" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _LojaElements)
 	           if (((Loja)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 
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
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is VendaItem && e.Entity.GetType().Name == "VendaItem" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is Vendedor && e.Entity.GetType().Name == "Vendedor" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
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
                , StringEstado = entity.STRING_ESTADO
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
	
		

	        if (entityName.InList("Linx.Demo.BV.PaiFilha.Cliente"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "Cliente",
	        			NameSpace = "Linx.Demo.BV.PaiFilha",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "Cliente",
	        			ClearMethodName = "ClearCliente",
	        			QueryMethodName  = "GetPagedCliente",	
	        			CountingMethodName  = "GetCliente" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Demo.BV.PaiFilha.Cliente"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Demo.BV.PaiFilha.Cliente"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Demo.BV.PaiFilha.Cliente", "Linx.Demo.BV.PaiFilha.Venda"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "Venda" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Demo.BV.PaiFilha",
	        			HasQuickSearch = false,
	        			ParentClassName = "Cliente",	
	        			DisplayName = "Venda",
	        			ClearMethodName = "ClearVenda" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedVenda" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetVenda" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Demo.BV.PaiFilha.Venda"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Demo.BV.PaiFilha.Venda" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Demo.BV.PaiFilha.Cliente", "Linx.Demo.BV.PaiFilha.VendaItem"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "VendaItem" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Demo.BV.PaiFilha",
	        			HasQuickSearch = false,
	        			ParentClassName = "Venda",	
	        			DisplayName = "VendaItem",
	        			ClearMethodName = "ClearVendaItem" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedVendaItem" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetVendaItem" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Demo.BV.PaiFilha.VendaItem"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Demo.BV.PaiFilha.VendaItem" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Demo.BV.PaiFilha.Cliente", "Linx.Demo.BV.PaiFilha.VendaAtacado"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "VendaAtacado" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Demo.BV.PaiFilha",
	        			HasQuickSearch = false,
	        			ParentClassName = "Cliente",	
	        			DisplayName = "VendaAtacado",
	        			ClearMethodName = "ClearVendaAtacado" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedVendaAtacado" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetVendaAtacado" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Demo.BV.PaiFilha.VendaAtacado"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Demo.BV.PaiFilha.VendaAtacado" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Demo.BV.PaiFilha.Loja"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "Loja",
	        			NameSpace = "Linx.Demo.BV.PaiFilha",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "Loja",
	        			ClearMethodName = "ClearLoja",
	        			QueryMethodName  = "GetPagedLoja",	
	        			CountingMethodName  = "GetLoja" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Demo.BV.PaiFilha.Loja"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Demo.BV.PaiFilha.Loja"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Demo.BV.PaiFilha.Loja", "Linx.Demo.BV.PaiFilha.Vendedor"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "Vendedor" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Demo.BV.PaiFilha",
	        			HasQuickSearch = false,
	        			ParentClassName = "Loja",	
	        			DisplayName = "Vendedor",
	        			ClearMethodName = "ClearVendedor" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedVendedor" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetVendedor" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Demo.BV.PaiFilha.Vendedor"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Demo.BV.PaiFilha.Vendedor" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
	
            return result;
        }
	
	    [Ignore]
	    public string[] GetClientDomains(bool erp)
        {	
	    		if (erp)
	    		{

         		    return new string[] { "Demo_ClientErpDataDomainsFactory", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Demo.BV.ClientResources.ClientErpDataDomainsFactory.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
	    		}
	    		else 
	    		{

         		    return new string[] { "Demo_MobileDataDomains", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Demo.BV.ClientResources.MobileDataDomains.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
	    		}

        }

	    [Ignore]
	    public string[] GetClientService(bool erp)
        {	

	    		if (erp)
	    		{

         		    return new string[] { "Demo_PaiFilhaClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Demo.BV.ClientResources.PaiFilhaClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Demo_paiFilhaService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Demo.BV.ClientResources.paiFilhaService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
	    		}

        }

	    [Ignore]
	    public string[] GetClientFactory(string entityName, bool erp)
        {	

	    		if (erp)
	    		{

         		    if (entityName == "Cliente") return new string[] { "Demo_ClientLocalService1ClientErpFactory", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Demo.BV.ClientResources.ClientLocalService1ClientErpFactory.res", System.Reflection.Assembly.GetExecutingAssembly()) };
         		    if (entityName == "Loja") return new string[] { "Demo_ClientLocalService2ClientErpFactory", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Demo.BV.ClientResources.ClientLocalService2ClientErpFactory.res", System.Reflection.Assembly.GetExecutingAssembly()) };
         		    else return new string[] { };	
	    		}
	    		else 
	    		{

         		    if (entityName == "Cliente") return new string[] { "Demo_clientLocalService1Factory", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Demo.BV.ClientResources.clientLocalService1Factory.res", System.Reflection.Assembly.GetExecutingAssembly()) };
         		    if (entityName == "Loja") return new string[] { "Demo_clientLocalService2Factory", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Demo.BV.ClientResources.clientLocalService2Factory.res", System.Reflection.Assembly.GetExecutingAssembly()) };
         		    else return new string[] { };	
	    		}

        }

	    [Ignore]
	    public string[] GetClientFactoryCustomEvents(string entityName, bool erp)
        {	

	    		if (erp)
	    		{

         		    if (entityName == "Cliente") return new string[] { "Demo_ClientLocalService1ExtendedClientErpFactory", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Demo.BV.ClientResources.ClientLocalService1ExtendedClientErpFactory.res", System.Reflection.Assembly.GetExecutingAssembly()) };
         		    if (entityName == "Loja") return new string[] { "Demo_ClientLocalService2ExtendedClientErpFactory", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Demo.BV.ClientResources.ClientLocalService2ExtendedClientErpFactory.res", System.Reflection.Assembly.GetExecutingAssembly()) };
         		    else return new string[] { };	
	    		}
	    		else 
	    		{

         		    if (entityName == "Cliente") return new string[] { "Demo_clientLocalService1ExtendedFactory", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Demo.BV.ClientResources.clientLocalService1ExtendedFactory.res", System.Reflection.Assembly.GetExecutingAssembly()) };
         		    if (entityName == "Loja") return new string[] { "Demo_clientLocalService2ExtendedFactory", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Demo.BV.ClientResources.clientLocalService2ExtendedFactory.res", System.Reflection.Assembly.GetExecutingAssembly()) };
         		    else return new string[] { };	
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
			
	        ((List<Venda>)result[0].VendaList)[0].VendaItemList = new List<VendaItem>();
	        ((List<VendaItem>)((List<Venda>)result[0].VendaList)[0].VendaItemList).Add(new VendaItem());
			
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
		
	
	    [Ignore]
	    //Clear VendaAtacado.
	    public IEnumerable<VendaAtacado> ClearVendaAtacado()
	    {
	        List<VendaAtacado> result = new List<VendaAtacado>();
	        result.Add(new VendaAtacado());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear Loja.
	    public IEnumerable<Loja> ClearLoja()
	    {
	        List<Loja> result = new List<Loja>();
	        result.Add(new Loja());	
			
	        result[0].VendedorList = new List<Vendedor>();
	        ((List<Vendedor>)result[0].VendedorList).Add(new Vendedor());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear Vendedor.
	    public IEnumerable<Vendedor> ClearVendedor()
	    {
	        List<Vendedor> result = new List<Vendedor>();
	        result.Add(new Vendedor());	
		
	        

	
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
                , StringEstado = entity0Al1.STRING_ESTADO
			
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
			
                                ,VendaItemList = 
	                                                (from entity2 in entity1.VENDA_ITEM_LISTA
                                                                  let entity2Al1 = entity2.VENDA
	                                                
	                                                	
	                                                select new VendaItem()
	                                                {
	                                                
                                                                BigIntVendaItem = entity2.BIG_INT_VENDA_ITEM
                                                                , BitVendaItem = entity2.BIT_VENDA_ITEM
                                                                , ComboboxVendaItem = entity2.COMBOBOX_VENDA_ITEM
                                                                , ComboboxVendaItemName = ((entity2.COMBOBOX_VENDA_ITEM) == 1 ? "VENDA ITEM 1" : ((entity2.COMBOBOX_VENDA_ITEM) == 2 ? "VENDA ITEM 2" : ((entity2.COMBOBOX_VENDA_ITEM) == 3 ? "VENDA ITEM 3" : "")))
                                                                , DatetimeVendaItem = entity2.DATETIME_VENDA_ITEM
                                                                , DecimalVendaItem = entity2.DECIMAL_VENDA_ITEM
                                                                , GuidVendaItem = entity2.GUID_VENDA_ITEM
                                                                , IdVenda = entity2Al1.ID_VENDA
                                                                , IdVendaItem = entity2.ID_VENDA_ITEM
                                                                , IntVendaItem = entity2.INT_VENDA_ITEM
                                                                , SmallIntVendaItem = entity2.SMALL_INT_VENDA_ITEM
                                                                , StringVendaItem = entity2.STRING_VENDA_ITEM
		
	                                                }
	                                                )
		
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
                , StringEstado = entity0Al1.STRING_ESTADO
		
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
			
	
	    [LojaQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get Loja.
	    public IQueryable<Loja> GetLoja()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetLoja")))
 	        {
 	             AuthorizationResult authorizationResult = (new LojaQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<Loja> result = 
	            (from entity0 in this.DbContext.LOJA
	            
	            	
	            select new Loja()		
	            {
	            
                BigIntLoja = entity0.BIG_INT_LOJA
                , BitLoja = entity0.BIT_LOJA
                , ComboboxLoja = entity0.COMBOBOX_LOJA
                , ComboboxLojaName = ((entity0.COMBOBOX_LOJA) == 1 ? "LOJA 1" : ((entity0.COMBOBOX_LOJA) == 2 ? "LOJA 2" : ((entity0.COMBOBOX_LOJA) == 3 ? "LOJA 3" : "")))
                , DatetimeLoja = entity0.DATETIME_LOJA
                , DecimalLoja = entity0.DECIMAL_LOJA
                , GuidLoja = entity0.GUID_LOJA
                , IdLoja = entity0.ID_LOJA
                , IntLoja = entity0.INT_LOJA
                , SmallIntLoja = entity0.SMALL_INT_LOJA
                , StringLoja = entity0.STRING_LOJA
			
                ,VendedorList = 
	                        (from entity1 in entity0.VENDEDOR_LISTA
                                  let entity1Al1 = entity1.LOJA
	                        
	                        	
	                        select new Vendedor()
	                        {
	                        
                                BitVendedor = entity1.BIT_VENDEDOR
                                , ComboboxVendedor = entity1.COMBOBOX_VENDEDOR
                                , ComboboxVendedorName = ((entity1.COMBOBOX_VENDEDOR) == 1 ? "VENDEDOR 1" : ((entity1.COMBOBOX_VENDEDOR) == 2 ? "VENDEDOR 2" : ((entity1.COMBOBOX_VENDEDOR) == 3 ? "VENDEDOR 3" : "")))
                                , DatetimeVendedor = entity1.DATETIME_VENDEDOR
                                , DecimalVendedor = entity1.DECIMAL_VENDEDOR
                                , GuidVendedor = entity1.GUID_VENDEDOR
                                , IdLoja = entity1Al1.ID_LOJA
                                , IdVendedor = entity1.ID_VENDEDOR
                                , IntVendedor = entity1.INT_VENDEDOR
                                , SmallIntVendedor = entity1.SMALL_INT_VENDEDOR
                                , StringVendedor = entity1.STRING_VENDEDOR
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendedorQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get Vendedor.
	    public IQueryable<Vendedor> GetVendedor()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendedor")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendedorQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<Vendedor> result = 
	            (from entity0 in this.DbContext.VENDEDOR
                  let entity0Al1 = entity0.LOJA
	            
	            	
	            select new Vendedor()		
	            {
	            
                BitVendedor = entity0.BIT_VENDEDOR
                , ComboboxVendedor = entity0.COMBOBOX_VENDEDOR
                , ComboboxVendedorName = ((entity0.COMBOBOX_VENDEDOR) == 1 ? "VENDEDOR 1" : ((entity0.COMBOBOX_VENDEDOR) == 2 ? "VENDEDOR 2" : ((entity0.COMBOBOX_VENDEDOR) == 3 ? "VENDEDOR 3" : "")))
                , DatetimeVendedor = entity0.DATETIME_VENDEDOR
                , DecimalVendedor = entity0.DECIMAL_VENDEDOR
                , GuidVendedor = entity0.GUID_VENDEDOR
                , IdLoja = entity0Al1.ID_LOJA
                , IdVendedor = entity0.ID_VENDEDOR
                , IntVendedor = entity0.INT_VENDEDOR
                , SmallIntVendedor = entity0.SMALL_INT_VENDEDOR
                , StringVendedor = entity0.STRING_VENDEDOR
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [LojaQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get LojaNoAssociations.
	    public IQueryable<Loja> GetLojaNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetLojaNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new LojaQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<Loja> result = 
	            (from entity0 in this.DbContext.LOJA
	            
	            	
	            select new Loja()		
	            {
	            
                BigIntLoja = entity0.BIG_INT_LOJA
                , BitLoja = entity0.BIT_LOJA
                , ComboboxLoja = entity0.COMBOBOX_LOJA
                , ComboboxLojaName = ((entity0.COMBOBOX_LOJA) == 1 ? "LOJA 1" : ((entity0.COMBOBOX_LOJA) == 2 ? "LOJA 2" : ((entity0.COMBOBOX_LOJA) == 3 ? "LOJA 3" : "")))
                , DatetimeLoja = entity0.DATETIME_LOJA
                , DecimalLoja = entity0.DECIMAL_LOJA
                , GuidLoja = entity0.GUID_LOJA
                , IdLoja = entity0.ID_LOJA
                , IntLoja = entity0.INT_LOJA
                , SmallIntLoja = entity0.SMALL_INT_LOJA
                , StringLoja = entity0.STRING_LOJA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendedorQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendedorNoAssociations.
	    public IQueryable<Vendedor> GetVendedorNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendedorNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendedorQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<Vendedor> result = 
	            (from entity0 in this.DbContext.VENDEDOR
                  let entity0Al1 = entity0.LOJA
	            
	            	
	            select new Vendedor()		
	            {
	            
                BitVendedor = entity0.BIT_VENDEDOR
                , ComboboxVendedor = entity0.COMBOBOX_VENDEDOR
                , ComboboxVendedorName = ((entity0.COMBOBOX_VENDEDOR) == 1 ? "VENDEDOR 1" : ((entity0.COMBOBOX_VENDEDOR) == 2 ? "VENDEDOR 2" : ((entity0.COMBOBOX_VENDEDOR) == 3 ? "VENDEDOR 3" : "")))
                , DatetimeVendedor = entity0.DATETIME_VENDEDOR
                , DecimalVendedor = entity0.DECIMAL_VENDEDOR
                , GuidVendedor = entity0.GUID_VENDEDOR
                , IdLoja = entity0Al1.ID_LOJA
                , IdVendedor = entity0.ID_VENDEDOR
                , IntVendedor = entity0.INT_VENDEDOR
                , SmallIntVendedor = entity0.SMALL_INT_VENDEDOR
                , StringVendedor = entity0.STRING_VENDEDOR
		
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
	    	//Add filtering disabled property for LOJA
	    	string[] bmDisabledLojaList = this.GetEDM().GetFilteringDisabledList("LOJA");
	    	if (bmDisabledLojaList.Length > 0)
	    	{
	
	    		if (bmDisabledLojaList.Contains("LOJA.BIG_INT_LOJA"))
	    		{
	    			result.Add("Loja|BigIntLoja");
	    			result.Add("Loja|LOJA.BIG_INT_LOJA");
	    		}
	
	    		if (bmDisabledLojaList.Contains("LOJA.BIT_LOJA"))
	    		{
	    			result.Add("Loja|BitLoja");
	    			result.Add("Loja|LOJA.BIT_LOJA");
	    		}
	
	    		if (bmDisabledLojaList.Contains("LOJA.COMBOBOX_LOJA"))
	    		{
	    			result.Add("Loja|ComboboxLoja");
	    			result.Add("Loja|LOJA.COMBOBOX_LOJA");
	    		}
	
	    		if (bmDisabledLojaList.Contains("LOJA.DATETIME_LOJA"))
	    		{
	    			result.Add("Loja|DatetimeLoja");
	    			result.Add("Loja|LOJA.DATETIME_LOJA");
	    		}
	
	    		if (bmDisabledLojaList.Contains("LOJA.DECIMAL_LOJA"))
	    		{
	    			result.Add("Loja|DecimalLoja");
	    			result.Add("Loja|LOJA.DECIMAL_LOJA");
	    		}
	
	    		if (bmDisabledLojaList.Contains("LOJA.GUID_LOJA"))
	    		{
	    			result.Add("Loja|GuidLoja");
	    			result.Add("Loja|LOJA.GUID_LOJA");
	    		}
	
	    		if (bmDisabledLojaList.Contains("LOJA.ID_LOJA"))
	    		{
	    			result.Add("Loja|IdLoja");
	    			result.Add("Loja|LOJA.ID_LOJA");
	    		}
	
	    		if (bmDisabledLojaList.Contains("LOJA.INT_LOJA"))
	    		{
	    			result.Add("Loja|IntLoja");
	    			result.Add("Loja|LOJA.INT_LOJA");
	    		}
	
	    		if (bmDisabledLojaList.Contains("LOJA.SMALL_INT_LOJA"))
	    		{
	    			result.Add("Loja|SmallIntLoja");
	    			result.Add("Loja|LOJA.SMALL_INT_LOJA");
	    		}
	
	    		if (bmDisabledLojaList.Contains("LOJA.STRING_LOJA"))
	    		{
	    			result.Add("Loja|StringLoja");
	    			result.Add("Loja|LOJA.STRING_LOJA");
	    		}
	    	}
	    	//Add filtering disabled property for VENDEDOR
	    	string[] bmDisabledVendedorList = this.GetEDM().GetFilteringDisabledList("VENDEDOR");
	    	if (bmDisabledVendedorList.Length > 0)
	    	{
	
	    		if (bmDisabledVendedorList.Contains("VENDEDOR.BIT_VENDEDOR"))
	    		{
	    			result.Add("Vendedor|BitVendedor");
	    			result.Add("Vendedor|VENDEDOR.BIT_VENDEDOR");
	    		}
	
	    		if (bmDisabledVendedorList.Contains("VENDEDOR.COMBOBOX_VENDEDOR"))
	    		{
	    			result.Add("Vendedor|ComboboxVendedor");
	    			result.Add("Vendedor|VENDEDOR.COMBOBOX_VENDEDOR");
	    		}
	
	    		if (bmDisabledVendedorList.Contains("VENDEDOR.DATETIME_VENDEDOR"))
	    		{
	    			result.Add("Vendedor|DatetimeVendedor");
	    			result.Add("Vendedor|VENDEDOR.DATETIME_VENDEDOR");
	    		}
	
	    		if (bmDisabledVendedorList.Contains("VENDEDOR.DECIMAL_VENDEDOR"))
	    		{
	    			result.Add("Vendedor|DecimalVendedor");
	    			result.Add("Vendedor|VENDEDOR.DECIMAL_VENDEDOR");
	    		}
	
	    		if (bmDisabledVendedorList.Contains("VENDEDOR.GUID_VENDEDOR"))
	    		{
	    			result.Add("Vendedor|GuidVendedor");
	    			result.Add("Vendedor|VENDEDOR.GUID_VENDEDOR");
	    		}
	
	    		if (bmDisabledVendedorList.Contains("VENDEDOR.ID_VENDEDOR"))
	    		{
	    			result.Add("Vendedor|IdVendedor");
	    			result.Add("Vendedor|VENDEDOR.ID_VENDEDOR");
	    		}
	
	    		if (bmDisabledVendedorList.Contains("VENDEDOR.INT_VENDEDOR"))
	    		{
	    			result.Add("Vendedor|IntVendedor");
	    			result.Add("Vendedor|VENDEDOR.INT_VENDEDOR");
	    		}
	
	    		if (bmDisabledVendedorList.Contains("VENDEDOR.SMALL_INT_VENDEDOR"))
	    		{
	    			result.Add("Vendedor|SmallIntVendedor");
	    			result.Add("Vendedor|VENDEDOR.SMALL_INT_VENDEDOR");
	    		}
	
	    		if (bmDisabledVendedorList.Contains("VENDEDOR.STRING_VENDEDOR"))
	    		{
	    			result.Add("Vendedor|StringVendedor");
	    			result.Add("Vendedor|VENDEDOR.STRING_VENDEDOR");
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
	    //Get VendaItem By EntitySearchId.
	    public IQueryable<VendaItem> GetVendaItemByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetVendaItemByEntitySearch(queryAnalysis);
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
	    //Get VendaItem By EntitySearchId.
	    public IQueryable<VendaItem> GetVendaItemByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetVendaItemByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get VendaAtacado By EntitySearchId.
	    public IQueryable<VendaAtacado> GetVendaAtacadoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetVendaAtacadoByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get Loja By EntitySearchId.
	    public IQueryable<Loja> GetLojaByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetLojaByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get Vendedor By EntitySearchId.
	    public IQueryable<Vendedor> GetVendedorByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetVendedorByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get Loja By EntitySearchId.
	    public IQueryable<Loja> GetLojaByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetLojaByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get Vendedor By EntitySearchId.
	    public IQueryable<Vendedor> GetVendedorByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetVendedorByEntitySearchNoAssociations(queryAnalysis);
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
			
	    //Get VendaItem By Example.
	    [Ignore]
	    public IQueryable<VendaItem> GetVendaItemByExample(VendaItem entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetVendaItemByEntitySearch(queryAnalysis);
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
			
	    //Get VendaItem By Example.
	    [Ignore]
	    public IQueryable<VendaItem> GetVendaItemByExampleNoAssociations(VendaItem entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetVendaItemByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get VendaAtacado By Example.
	    [Ignore]
	    public IQueryable<VendaAtacado> GetVendaAtacadoByExampleNoAssociations(VendaAtacado entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetVendaAtacadoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get Loja By Example.
	    [Ignore]
	    public IQueryable<Loja> GetLojaByExample(Loja entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetLojaByEntitySearch(queryAnalysis);
	    }
			
	    //Get Vendedor By Example.
	    [Ignore]
	    public IQueryable<Vendedor> GetVendedorByExample(Vendedor entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetVendedorByEntitySearch(queryAnalysis);
	    }
			
	    //Get Loja By Example.
	    [Ignore]
	    public IQueryable<Loja> GetLojaByExampleNoAssociations(Loja entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetLojaByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get Vendedor By Example.
	    [Ignore]
	    public IQueryable<Vendedor> GetVendedorByExampleNoAssociations(Vendedor entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetVendedorByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public Cliente GetClienteByKey(int idCliente)
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
	    public Venda GetVendaByKey(int idVenda)
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
	    public VendaAtacado GetVendaAtacadoByKey(int idVendaAtacado)
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


	    [Ignore]
	    public VendaItem GetVendaItemByKey(int idVendaItem)
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


	    [Ignore]
	    public Loja GetLojaByKey(int idLoja)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("Loja");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdLoja"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idLoja));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetLojaByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public Vendedor GetVendedorByKey(int idVendedor)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("Vendedor");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdVendedor"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idVendedor));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetVendedorByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
                , StringEstado = entity0Al1.STRING_ESTADO
			
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
			
                                ,VendaItemList = 
	                                                (from entity2 in entity1.VENDA_ITEM_LISTA
                                                                  let entity2Al1 = entity2.VENDA
	                                                
	                                                	
	                                                select new VendaItem()
	                                                {
	                                                
                                                                BigIntVendaItem = entity2.BIG_INT_VENDA_ITEM
                                                                , BitVendaItem = entity2.BIT_VENDA_ITEM
                                                                , ComboboxVendaItem = entity2.COMBOBOX_VENDA_ITEM
                                                                , ComboboxVendaItemName = ((entity2.COMBOBOX_VENDA_ITEM) == 1 ? "VENDA ITEM 1" : ((entity2.COMBOBOX_VENDA_ITEM) == 2 ? "VENDA ITEM 2" : ((entity2.COMBOBOX_VENDA_ITEM) == 3 ? "VENDA ITEM 3" : "")))
                                                                , DatetimeVendaItem = entity2.DATETIME_VENDA_ITEM
                                                                , DecimalVendaItem = entity2.DECIMAL_VENDA_ITEM
                                                                , GuidVendaItem = entity2.GUID_VENDA_ITEM
                                                                , IdVenda = entity2Al1.ID_VENDA
                                                                , IdVendaItem = entity2.ID_VENDA_ITEM
                                                                , IntVendaItem = entity2.INT_VENDA_ITEM
                                                                , SmallIntVendaItem = entity2.SMALL_INT_VENDA_ITEM
                                                                , StringVendaItem = entity2.STRING_VENDA_ITEM
		
	                                                }
	                                                )
		
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
                , StringEstado = entity0Al1.STRING_ESTADO
		
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
                , StringEstado = entity0.CLIENTE.ESTADO.STRING_ESTADO
		
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "CLIENTE", "VENDA_ITEM", "VENDA.CLIENTE", typeof(VendaItemParentComposition), typeof(VendaAtacado));
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
                //Cliente Properties.
                , BigIntCliente = entity0.VENDA.CLIENTE.BIG_INT_CLIENTE
                , BitCliente = entity0.VENDA.CLIENTE.BIT_CLIENTE
                , ComboboxCliente = entity0.VENDA.CLIENTE.COMBOBOX_CLIENTE
                , ComboboxClienteName = ((entity0.VENDA.CLIENTE.COMBOBOX_CLIENTE) == 1 ? "CLIENTE 1" : ((entity0.VENDA.CLIENTE.COMBOBOX_CLIENTE) == 2 ? "CLIENTE 2" : ((entity0.VENDA.CLIENTE.COMBOBOX_CLIENTE) == 3 ? "CLIENTE 3" : "")))
                , DatetimeCliente = entity0.VENDA.CLIENTE.DATETIME_CLIENTE
                , DecimalCliente = entity0.VENDA.CLIENTE.DECIMAL_CLIENTE
                , GuidCliente = entity0.VENDA.CLIENTE.GUID_CLIENTE
                , IdEstado = entity0.VENDA.CLIENTE.ESTADO.ID_ESTADO
                , IntCliente = entity0.VENDA.CLIENTE.INT_CLIENTE
                , SmallIntCliente = entity0.VENDA.CLIENTE.SMALL_INT_CLIENTE
                , StringCliente = entity0.VENDA.CLIENTE.STRING_CLIENTE
                , StringEstado = entity0.VENDA.CLIENTE.ESTADO.STRING_ESTADO
		
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
                , StringEstado = entity0.CLIENTE.ESTADO.STRING_ESTADO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [LojaQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get LojaByEntitySearch.
	    public IQueryable<Loja> GetLojaByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetLojaByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new LojaQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Loja));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<Loja> result = 
	            (from entity0 in this.DbContext.LOJA.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new Loja()		
	            {
	            
                BigIntLoja = entity0.BIG_INT_LOJA
                , BitLoja = entity0.BIT_LOJA
                , ComboboxLoja = entity0.COMBOBOX_LOJA
                , ComboboxLojaName = ((entity0.COMBOBOX_LOJA) == 1 ? "LOJA 1" : ((entity0.COMBOBOX_LOJA) == 2 ? "LOJA 2" : ((entity0.COMBOBOX_LOJA) == 3 ? "LOJA 3" : "")))
                , DatetimeLoja = entity0.DATETIME_LOJA
                , DecimalLoja = entity0.DECIMAL_LOJA
                , GuidLoja = entity0.GUID_LOJA
                , IdLoja = entity0.ID_LOJA
                , IntLoja = entity0.INT_LOJA
                , SmallIntLoja = entity0.SMALL_INT_LOJA
                , StringLoja = entity0.STRING_LOJA
			
                ,VendedorList = 
	                        (from entity1 in entity0.VENDEDOR_LISTA
                                  let entity1Al1 = entity1.LOJA
	                        
	                        	
	                        select new Vendedor()
	                        {
	                        
                                BitVendedor = entity1.BIT_VENDEDOR
                                , ComboboxVendedor = entity1.COMBOBOX_VENDEDOR
                                , ComboboxVendedorName = ((entity1.COMBOBOX_VENDEDOR) == 1 ? "VENDEDOR 1" : ((entity1.COMBOBOX_VENDEDOR) == 2 ? "VENDEDOR 2" : ((entity1.COMBOBOX_VENDEDOR) == 3 ? "VENDEDOR 3" : "")))
                                , DatetimeVendedor = entity1.DATETIME_VENDEDOR
                                , DecimalVendedor = entity1.DECIMAL_VENDEDOR
                                , GuidVendedor = entity1.GUID_VENDEDOR
                                , IdLoja = entity1Al1.ID_LOJA
                                , IdVendedor = entity1.ID_VENDEDOR
                                , IntVendedor = entity1.INT_VENDEDOR
                                , SmallIntVendedor = entity1.SMALL_INT_VENDEDOR
                                , StringVendedor = entity1.STRING_VENDEDOR
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendedorQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendedorByEntitySearch.
	    public IQueryable<Vendedor> GetVendedorByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendedorByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendedorQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Vendedor));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<Vendedor> result = 
	            (from entity0 in this.DbContext.VENDEDOR.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.LOJA
	            
	            	
	            select new Vendedor()		
	            {
	            
                BitVendedor = entity0.BIT_VENDEDOR
                , ComboboxVendedor = entity0.COMBOBOX_VENDEDOR
                , ComboboxVendedorName = ((entity0.COMBOBOX_VENDEDOR) == 1 ? "VENDEDOR 1" : ((entity0.COMBOBOX_VENDEDOR) == 2 ? "VENDEDOR 2" : ((entity0.COMBOBOX_VENDEDOR) == 3 ? "VENDEDOR 3" : "")))
                , DatetimeVendedor = entity0.DATETIME_VENDEDOR
                , DecimalVendedor = entity0.DECIMAL_VENDEDOR
                , GuidVendedor = entity0.GUID_VENDEDOR
                , IdLoja = entity0Al1.ID_LOJA
                , IdVendedor = entity0.ID_VENDEDOR
                , IntVendedor = entity0.INT_VENDEDOR
                , SmallIntVendedor = entity0.SMALL_INT_VENDEDOR
                , StringVendedor = entity0.STRING_VENDEDOR
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [LojaQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get LojaByEntitySearchNoAssociations.
	    public IQueryable<Loja> GetLojaByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetLojaByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new LojaQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Loja));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<Loja> result = 
	            (from entity0 in this.DbContext.LOJA.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new Loja()		
	            {
	            
                BigIntLoja = entity0.BIG_INT_LOJA
                , BitLoja = entity0.BIT_LOJA
                , ComboboxLoja = entity0.COMBOBOX_LOJA
                , ComboboxLojaName = ((entity0.COMBOBOX_LOJA) == 1 ? "LOJA 1" : ((entity0.COMBOBOX_LOJA) == 2 ? "LOJA 2" : ((entity0.COMBOBOX_LOJA) == 3 ? "LOJA 3" : "")))
                , DatetimeLoja = entity0.DATETIME_LOJA
                , DecimalLoja = entity0.DECIMAL_LOJA
                , GuidLoja = entity0.GUID_LOJA
                , IdLoja = entity0.ID_LOJA
                , IntLoja = entity0.INT_LOJA
                , SmallIntLoja = entity0.SMALL_INT_LOJA
                , StringLoja = entity0.STRING_LOJA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendedorQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendedorByEntitySearchNoAssociations.
	    public IQueryable<Vendedor> GetVendedorByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendedorByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendedorQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Vendedor));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<Vendedor> result = 
	            (from entity0 in this.DbContext.VENDEDOR.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.LOJA
	            
	            	
	            select new Vendedor()		
	            {
	            
                BitVendedor = entity0.BIT_VENDEDOR
                , ComboboxVendedor = entity0.COMBOBOX_VENDEDOR
                , ComboboxVendedorName = ((entity0.COMBOBOX_VENDEDOR) == 1 ? "VENDEDOR 1" : ((entity0.COMBOBOX_VENDEDOR) == 2 ? "VENDEDOR 2" : ((entity0.COMBOBOX_VENDEDOR) == 3 ? "VENDEDOR 3" : "")))
                , DatetimeVendedor = entity0.DATETIME_VENDEDOR
                , DecimalVendedor = entity0.DECIMAL_VENDEDOR
                , GuidVendedor = entity0.GUID_VENDEDOR
                , IdLoja = entity0Al1.ID_LOJA
                , IdVendedor = entity0.ID_VENDEDOR
                , IntVendedor = entity0.INT_VENDEDOR
                , SmallIntVendedor = entity0.SMALL_INT_VENDEDOR
                , StringVendedor = entity0.STRING_VENDEDOR
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendedorQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendedorParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<VendedorParentComposition> GetVendedorParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendedorParentCompositionByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendedorQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "LOJA", "VENDEDOR", "LOJA", typeof(VendedorParentComposition));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<VendedorParentComposition> result = 
	            (from entity0 in this.DbContext.VENDEDOR.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.LOJA
	            
	            	
	            select new VendedorParentComposition()		
	            {
	            
                BitVendedor = entity0.BIT_VENDEDOR
                , ComboboxVendedor = entity0.COMBOBOX_VENDEDOR
                , ComboboxVendedorName = ((entity0.COMBOBOX_VENDEDOR) == 1 ? "VENDEDOR 1" : ((entity0.COMBOBOX_VENDEDOR) == 2 ? "VENDEDOR 2" : ((entity0.COMBOBOX_VENDEDOR) == 3 ? "VENDEDOR 3" : "")))
                , DatetimeVendedor = entity0.DATETIME_VENDEDOR
                , DecimalVendedor = entity0.DECIMAL_VENDEDOR
                , GuidVendedor = entity0.GUID_VENDEDOR
                , IdLoja = entity0Al1.ID_LOJA
                , IdVendedor = entity0.ID_VENDEDOR
                , IntVendedor = entity0.INT_VENDEDOR
                , SmallIntVendedor = entity0.SMALL_INT_VENDEDOR
                , StringVendedor = entity0.STRING_VENDEDOR
                //Loja Properties.
                , BigIntLoja = entity0.LOJA.BIG_INT_LOJA
                , BitLoja = entity0.LOJA.BIT_LOJA
                , ComboboxLoja = entity0.LOJA.COMBOBOX_LOJA
                , ComboboxLojaName = ((entity0.LOJA.COMBOBOX_LOJA) == 1 ? "LOJA 1" : ((entity0.LOJA.COMBOBOX_LOJA) == 2 ? "LOJA 2" : ((entity0.LOJA.COMBOBOX_LOJA) == 3 ? "LOJA 3" : "")))
                , DatetimeLoja = entity0.LOJA.DATETIME_LOJA
                , DecimalLoja = entity0.LOJA.DECIMAL_LOJA
                , GuidLoja = entity0.LOJA.GUID_LOJA
                , IntLoja = entity0.LOJA.INT_LOJA
                , SmallIntLoja = entity0.LOJA.SMALL_INT_LOJA
                , StringLoja = entity0.LOJA.STRING_LOJA
		
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
                , StringEstado = entity0Al1.STRING_ESTADO
		
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
			
	
	    [LojaQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedLoja.
	    public IQueryable<Loja> GetPagedLoja(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedLoja")))
 	        {
 	             AuthorizationResult authorizationResult = (new LojaQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Loja));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<Loja> result = 
	            (from entity0 in this.DbContext.LOJA.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_LOJA ascending
	            
	            	
	            select new Loja()		
	            {
	            
                BigIntLoja = entity0.BIG_INT_LOJA
                , BitLoja = entity0.BIT_LOJA
                , ComboboxLoja = entity0.COMBOBOX_LOJA
                , ComboboxLojaName = ((entity0.COMBOBOX_LOJA) == 1 ? "LOJA 1" : ((entity0.COMBOBOX_LOJA) == 2 ? "LOJA 2" : ((entity0.COMBOBOX_LOJA) == 3 ? "LOJA 3" : "")))
                , DatetimeLoja = entity0.DATETIME_LOJA
                , DecimalLoja = entity0.DECIMAL_LOJA
                , GuidLoja = entity0.GUID_LOJA
                , IdLoja = entity0.ID_LOJA
                , IntLoja = entity0.INT_LOJA
                , SmallIntLoja = entity0.SMALL_INT_LOJA
                , StringLoja = entity0.STRING_LOJA
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendedorQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedVendedor.
	    public IQueryable<Vendedor> GetPagedVendedor(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedVendedor")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendedorQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Vendedor));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<Vendedor> result = 
	            (from entity0 in this.DbContext.VENDEDOR.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.LOJA
                orderby entity0.ID_VENDEDOR ascending
	            
	            	
	            select new Vendedor()		
	            {
	            
                BitVendedor = entity0.BIT_VENDEDOR
                , ComboboxVendedor = entity0.COMBOBOX_VENDEDOR
                , ComboboxVendedorName = ((entity0.COMBOBOX_VENDEDOR) == 1 ? "VENDEDOR 1" : ((entity0.COMBOBOX_VENDEDOR) == 2 ? "VENDEDOR 2" : ((entity0.COMBOBOX_VENDEDOR) == 3 ? "VENDEDOR 3" : "")))
                , DatetimeVendedor = entity0.DATETIME_VENDEDOR
                , DecimalVendedor = entity0.DECIMAL_VENDEDOR
                , GuidVendedor = entity0.GUID_VENDEDOR
                , IdLoja = entity0Al1.ID_LOJA
                , IdVendedor = entity0.ID_VENDEDOR
                , IntVendedor = entity0.INT_VENDEDOR
                , SmallIntVendedor = entity0.SMALL_INT_VENDEDOR
                , StringVendedor = entity0.STRING_VENDEDOR
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetLojaCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Loja));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.LOJA.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetVendedorCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Vendedor));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.VENDEDOR.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.LOJA
	            
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
		
			
	    [LojaUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update Loja.
	    public void UpdateLoja(Loja entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateLoja")))
 	        {
 	             AuthorizationResult authorizationResult = (new LojaUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    [LojaInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert Loja.
	    public void InsertLoja(Loja entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertLoja")))
 	        {
 	             AuthorizationResult authorizationResult = (new LojaInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    [LojaDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete Loja.
	    public void DeleteLoja(Loja entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteLoja")))
 	        {
 	             AuthorizationResult authorizationResult = (new LojaDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    [VendedorUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update Vendedor.
	    public void UpdateVendedor(Vendedor entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateVendedor")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendedorUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.Loja.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.Loja) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.Loja); 	
	            

	
	        }
	
	    }

	    [VendedorInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert Vendedor.
	    public void InsertVendedor(Vendedor entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertVendedor")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendedorInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.Loja.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.Loja) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.Loja);
	            

	
	        }
	
	    }

	    [VendedorDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete Vendedor.
	    public void DeleteVendedor(Vendedor entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteVendedor")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendedorDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.Loja.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.Loja) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.Loja);
	            

	
	        }

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}