					
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

namespace Linx.Demo.BV.PaiFilha
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="LOJA.ID_LOJA", IsUpdatable=false, EdmName="Linx.Demo.BM.BMDTesteFrame")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[Loja,Loja.Venda,Venda.VendaItem];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[];EdmEntityName[LOJA];EntityRelations[CIDADE(CIDADE)#ESTADO(ESTADO)#PAIS(PAIS)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "Loja")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Demo.BV.PaiFilha.Loja")]
	public partial class Loja : Linx.Data.Entity
	{

	

	    public Loja() : this(true) { }

	    public Loja(bool setDefaults) 
	    {

	        if (setDefaults)
	        {
	        	        DatetimeLoja = DateTime.Now;
	        }	

	    }

			
	

	
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
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(PaiFilhaDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("Venda"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("Venda");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdLoja"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdLoja));
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
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _VendaElements = changeSet.ChangeSetEntries.Where(e => e.Entity is Venda && ((Venda)e.Entity).Loja == null && e.Associations == null && e.OriginalAssociations == null && ((Venda)e.Entity).IdLoja == this.IdLoja).ToList();
 	      if (_VendaElements.Count > 0 && this.VendaList.Count() == 0)
 	      {
 	          this.VendaList = _VendaElements.Select(e => (Venda)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _VendaElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((Venda)detail.Entity).Loja = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("Loja", new int[] { masterIndex });
 	              ((Venda)detail.Entity).AdjustHierarchyForSaving(detail, changeSet);
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("VendaList", indexDetails.ToArray());
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
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LOJA.BIG_INT_LOJA];IsMeasure[false]")]
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
	    [FunctionalPoint("Precision[10];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LOJA.BIT_LOJA];IsMeasure[false]")]
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
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_COMBOBOX_LOJA];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LOJA.COMBOBOX_LOJA];IsMeasure[false]")]
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
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[DateTime.Now];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LOJA.DATETIME_LOJA];IsMeasure[false]")]
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
	    [Display(Name = "Guid Loja", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    //Extensibility Partial Method Definitions For IdCidade
	    partial void OnIdCidadeChanging(System.Nullable<int> value);
	    partial void OnIdCidadeChanged();

	    private System.Nullable<int> _IdCidade;

	    [DataMember(Name = "IdCidade", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Cidade", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpCidade];LookUpTitle[Seleção de (Id Cidade)];LookUpQuery[executeLookUpCidade];LookUpFinalize[finalizeLookUpCidade];LookUpDisplayColumns[{\"IdEstado\" : \"Cod UF\", \"IdPais\" : \"Cod Pais\", \"StringPais\" : \"PAIS\", \"StringEstado\" : \"UF\", \"IdCidade\" : \"Cod Cidade\", \"NomeCidade\" : \"Cidade\"}];LookUpColumns[{\"IdEstado\" : true, \"IdPais\" : true, \"StringPais\" : true, \"StringEstado\" : true, \"IdCidade\" : true, \"NomeCidade\" : true}];FilterDataKey[LOJA.CIDADE.ID_CIDADE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<int>#IdCidade#true##10:0##Cod Cidade#4#true##::LookUpCidade##false#false#CIDADE#CIDADE#Linx.Demo.BV.PaiFilha#IQueryable#IdEstado,StringEstado[IdEstado,IdPais,StringPais,StringEstado];IdPais,StringPais[IdPais,StringPais]#IdEstado[IdPais=IdPais,StringPais=StringPais];StringEstado[IdPais=IdPais,StringPais=StringPais];IdCidade[IdEstado=IdEstado,IdPais=IdPais,StringPais=StringPais,StringEstado=StringEstado];NomeCidade[IdEstado=IdEstado,IdPais=IdPais,StringPais=StringPais,StringEstado=StringEstado]#true#false", EdmKey="LOJA.CIDADE.ID_CIDADE")]
	    public System.Nullable<int> IdCidade
	    {
	    	    get
	    	    {
	    	          return _IdCidade;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdCidade != value)
	    	          {
	    	              this.ValidateProperty("IdCidade", value);
	    	              this.OnIdCidadeChanging(value);
	    	              this.RaiseDataMemberChanging("IdCidade");
	    	              this._IdCidade = value;
	    	              this.RaiseDataMemberChanged("IdCidade");
	    	              this.OnIdCidadeChanged();
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
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpCidade];LookUpTitle[Seleção de (Id Estado)];LookUpQuery[executeLookUpCidade];LookUpFinalize[finalizeLookUpCidade];LookUpDisplayColumns[{\"IdEstado\" : \"Cod UF\", \"IdPais\" : \"Cod Pais\", \"StringPais\" : \"PAIS\", \"StringEstado\" : \"UF\", \"IdCidade\" : \"Cod Cidade\", \"NomeCidade\" : \"Cidade\"}];LookUpColumns[{\"IdEstado\" : true, \"IdPais\" : true, \"StringPais\" : true, \"StringEstado\" : true, \"IdCidade\" : true, \"NomeCidade\" : true}];FilterDataKey[LOJA.CIDADE.ESTADO.ID_ESTADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<int>#IdEstado#false##10:0##Cod UF#0#true##::LookUpCidade##false#false#CIDADE#CIDADE#Linx.Demo.BV.PaiFilha#IQueryable#IdEstado,StringEstado[IdEstado,IdPais,StringPais,StringEstado];IdPais,StringPais[IdPais,StringPais]#IdEstado[IdPais=IdPais,StringPais=StringPais];StringEstado[IdPais=IdPais,StringPais=StringPais];IdCidade[IdEstado=IdEstado,IdPais=IdPais,StringPais=StringPais,StringEstado=StringEstado];NomeCidade[IdEstado=IdEstado,IdPais=IdPais,StringPais=StringPais,StringEstado=StringEstado]#true#false", EdmKey="LOJA.CIDADE.ESTADO.ID_ESTADO")]
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
	    //Extensibility Partial Method Definitions For IdLoja
	    partial void OnIdLojaChanging(int value);
	    partial void OnIdLojaChanged();

	    private int _IdLoja;

	    [DataMember(IsRequired = true, Name = "IdLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Loja", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LOJA.ID_LOJA];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For IdPais
	    partial void OnIdPaisChanging(System.Nullable<int> value);
	    partial void OnIdPaisChanged();

	    private System.Nullable<int> _IdPais;

	    [DataMember(Name = "IdPais", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Pais", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpCidade];LookUpTitle[Seleção de (Id Pais)];LookUpQuery[executeLookUpCidade];LookUpFinalize[finalizeLookUpCidade];LookUpDisplayColumns[{\"IdEstado\" : \"Cod UF\", \"IdPais\" : \"Cod Pais\", \"StringPais\" : \"PAIS\", \"StringEstado\" : \"UF\", \"IdCidade\" : \"Cod Cidade\", \"NomeCidade\" : \"Cidade\"}];LookUpColumns[{\"IdEstado\" : true, \"IdPais\" : true, \"StringPais\" : true, \"StringEstado\" : true, \"IdCidade\" : true, \"NomeCidade\" : true}];FilterDataKey[LOJA.CIDADE.ESTADO.PAIS.ID_PAIS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<int>#IdPais#false##10:0##Cod Pais#1#true##::LookUpCidade##false#false#CIDADE#CIDADE#Linx.Demo.BV.PaiFilha#IQueryable#IdEstado,StringEstado[IdEstado,IdPais,StringPais,StringEstado];IdPais,StringPais[IdPais,StringPais]#IdEstado[IdPais=IdPais,StringPais=StringPais];StringEstado[IdPais=IdPais,StringPais=StringPais];IdCidade[IdEstado=IdEstado,IdPais=IdPais,StringPais=StringPais,StringEstado=StringEstado];NomeCidade[IdEstado=IdEstado,IdPais=IdPais,StringPais=StringPais,StringEstado=StringEstado]#true#false", EdmKey="LOJA.CIDADE.ESTADO.PAIS.ID_PAIS")]
	    public System.Nullable<int> IdPais
	    {
	    	    get
	    	    {
	    	          return _IdPais;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdPais != value)
	    	          {
	    	              this.ValidateProperty("IdPais", value);
	    	              this.OnIdPaisChanging(value);
	    	              this.RaiseDataMemberChanging("IdPais");
	    	              this._IdPais = value;
	    	              this.RaiseDataMemberChanged("IdPais");
	    	              this.OnIdPaisChanged();
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
	    [Display(Name = "Int Loja", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LOJA.INT_LOJA];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For NomeCidade
	    partial void OnNomeCidadeChanging(string value);
	    partial void OnNomeCidadeChanged();

	    private string _NomeCidade;

	    [DataMember(Name = "NomeCidade", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Cidade", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpCidade];LookUpTitle[Seleção de (Nome Cidade)];LookUpQuery[executeLookUpCidade];LookUpFinalize[finalizeLookUpCidade];LookUpDisplayColumns[{\"IdEstado\" : \"Cod UF\", \"IdPais\" : \"Cod Pais\", \"StringPais\" : \"PAIS\", \"StringEstado\" : \"UF\", \"IdCidade\" : \"Cod Cidade\", \"NomeCidade\" : \"Cidade\"}];LookUpColumns[{\"IdEstado\" : true, \"IdPais\" : true, \"StringPais\" : true, \"StringEstado\" : true, \"IdCidade\" : true, \"NomeCidade\" : true}];FilterDataKey[LOJA.CIDADE.NOME_CIDADE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#NomeCidade#false##50:0##Cidade#5#true##::LookUpCidade##false#false#CIDADE#CIDADE#Linx.Demo.BV.PaiFilha#IQueryable#IdEstado,StringEstado[IdEstado,IdPais,StringPais,StringEstado];IdPais,StringPais[IdPais,StringPais]#IdEstado[IdPais=IdPais,StringPais=StringPais];StringEstado[IdPais=IdPais,StringPais=StringPais];IdCidade[IdEstado=IdEstado,IdPais=IdPais,StringPais=StringPais,StringEstado=StringEstado];NomeCidade[IdEstado=IdEstado,IdPais=IdPais,StringPais=StringPais,StringEstado=StringEstado]#true#false", EdmKey="LOJA.CIDADE.NOME_CIDADE")]
	    public string NomeCidade
	    {
	    	    get
	    	    {
	    	          return _NomeCidade;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeCidade != value)
	    	          {
	    	              this.ValidateProperty("NomeCidade", value);
	    	              this.OnNomeCidadeChanging(value);
	    	              this.RaiseDataMemberChanging("NomeCidade");
	    	              this._NomeCidade = value;
	    	              this.RaiseDataMemberChanged("NomeCidade");
	    	              this.OnNomeCidadeChanged();
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
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LOJA.SMALL_INT_LOJA];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For StringEstado
	    partial void OnStringEstadoChanging(string value);
	    partial void OnStringEstadoChanged();

	    private string _StringEstado;

	    [DataMember(Name = "StringEstado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Estado", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpCidade];LookUpTitle[Seleção de (Nome Estado)];LookUpQuery[executeLookUpCidade];LookUpFinalize[finalizeLookUpCidade];LookUpDisplayColumns[{\"IdEstado\" : \"Cod UF\", \"IdPais\" : \"Cod Pais\", \"StringPais\" : \"PAIS\", \"StringEstado\" : \"UF\", \"IdCidade\" : \"Cod Cidade\", \"NomeCidade\" : \"Cidade\"}];LookUpColumns[{\"IdEstado\" : true, \"IdPais\" : true, \"StringPais\" : true, \"StringEstado\" : true, \"IdCidade\" : true, \"NomeCidade\" : true}];FilterDataKey[LOJA.CIDADE.ESTADO.STRING_ESTADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#StringEstado#false##50:0##UF#3#true##::LookUpCidade##false#false#CIDADE#CIDADE#Linx.Demo.BV.PaiFilha#IQueryable#IdEstado,StringEstado[IdEstado,IdPais,StringPais,StringEstado];IdPais,StringPais[IdPais,StringPais]#IdEstado[IdPais=IdPais,StringPais=StringPais];StringEstado[IdPais=IdPais,StringPais=StringPais];IdCidade[IdEstado=IdEstado,IdPais=IdPais,StringPais=StringPais,StringEstado=StringEstado];NomeCidade[IdEstado=IdEstado,IdPais=IdPais,StringPais=StringPais,StringEstado=StringEstado]#true#false", EdmKey="LOJA.CIDADE.ESTADO.STRING_ESTADO")]
	    public string StringEstado
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
	    //Extensibility Partial Method Definitions For StringPais
	    partial void OnStringPaisChanging(string value);
	    partial void OnStringPaisChanged();

	    private string _StringPais;

	    [DataMember(Name = "StringPais", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Pais", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpCidade];LookUpTitle[Seleção de (Nome Pais)];LookUpQuery[executeLookUpCidade];LookUpFinalize[finalizeLookUpCidade];LookUpDisplayColumns[{\"IdEstado\" : \"Cod UF\", \"IdPais\" : \"Cod Pais\", \"StringPais\" : \"PAIS\", \"StringEstado\" : \"UF\", \"IdCidade\" : \"Cod Cidade\", \"NomeCidade\" : \"Cidade\"}];LookUpColumns[{\"IdEstado\" : true, \"IdPais\" : true, \"StringPais\" : true, \"StringEstado\" : true, \"IdCidade\" : true, \"NomeCidade\" : true}];FilterDataKey[LOJA.CIDADE.ESTADO.PAIS.STRING_PAIS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#StringPais#false##50:0##PAIS#2#true##::LookUpCidade##false#false#CIDADE#CIDADE#Linx.Demo.BV.PaiFilha#IQueryable#IdEstado,StringEstado[IdEstado,IdPais,StringPais,StringEstado];IdPais,StringPais[IdPais,StringPais]#IdEstado[IdPais=IdPais,StringPais=StringPais];StringEstado[IdPais=IdPais,StringPais=StringPais];IdCidade[IdEstado=IdEstado,IdPais=IdPais,StringPais=StringPais,StringEstado=StringEstado];NomeCidade[IdEstado=IdEstado,IdPais=IdPais,StringPais=StringPais,StringEstado=StringEstado]#true#false", EdmKey="LOJA.CIDADE.ESTADO.PAIS.STRING_PAIS")]
	    public string StringPais
	    {
	    	    get
	    	    {
	    	          return _StringPais;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringPais != value)
	    	          {
	    	              this.ValidateProperty("StringPais", value);
	    	              this.OnStringPaisChanging(value);
	    	              this.RaiseDataMemberChanging("StringPais");
	    	              this._StringPais = value;
	    	              this.RaiseDataMemberChanged("StringPais");
	    	              this.OnStringPaisChanged();
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
	    [Association("FK_Loja_Venda", "IdLoja", "IdLoja", IsForeignKey=false)]
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
		 
	    #endregion Detail Associations		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "BMDTesteFrame.LOJA").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Demo.BM.LOJA), QualifiedEntitySetName = "BMDTesteFrame.LOJA" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.ID_LOJA", Source = "IdLoja", Target = "ID_LOJA", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.BIT_LOJA", Source = "BitLoja", Target = "BIT_LOJA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.INT_LOJA", Source = "IntLoja", Target = "INT_LOJA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.GUID_LOJA", Source = "GuidLoja", Target = "GUID_LOJA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.STRING_LOJA", Source = "StringLoja", Target = "STRING_LOJA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.BIG_INT_LOJA", Source = "BigIntLoja", Target = "BIG_INT_LOJA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.DECIMAL_LOJA", Source = "DecimalLoja", Target = "DECIMAL_LOJA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.COMBOBOX_LOJA", Source = "ComboboxLoja", Target = "COMBOBOX_LOJA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.DATETIME_LOJA", Source = "DatetimeLoja", Target = "DATETIME_LOJA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.SMALL_INT_LOJA", Source = "SmallIntLoja", Target = "SMALL_INT_LOJA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.CIDADE.ID_CIDADE", Source = "IdCidade", Target = "ID_CIDADE", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BMDTesteFrame.CIDADE", RelationPropertyName = "CIDADE" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetComboboxLojaValues()
	    {
	    	    return Linx.Demo.BV.Domains.LX_COMBOBOX_LOJA.GetValues();
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

		

	[LinxPublicationView(PrimaryKeys="VENDA.ID_VENDA", IsUpdatable=false, EdmName="Linx.Demo.BM.BMDTesteFrame")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Venda];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#.VENDA_LISTA as #Alias#];EdmEntityName[VENDA];EntityRelations[LOJA(LOJA)#CIDADE(CIDADE)#ESTADO(ESTADO)#PAIS(PAIS)#VENDEDOR(VENDEDOR)];EdmParentEntityName[LOJA];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "Venda")]
	[Serializable()]
	public partial class Venda : Linx.Data.Entity
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
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_VENDA];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.COMBOBOX_VENDA];IsMeasure[true]")]
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
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.DECIMAL_VENDA];IsMeasure[true]")]
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
	    //Extensibility Partial Method Definitions For IdLoja
	    partial void OnIdLojaChanging(System.Nullable<int> value);
	    partial void OnIdLojaChanged();

	    private System.Nullable<int> _IdLoja;

	    [DataMember(Name = "IdLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Loja", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.LOJA.ID_LOJA];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For IdVenda
	    partial void OnIdVendaChanging(int value);
	    partial void OnIdVendaChanged();

	    private int _IdVenda;

	    [DataMember(IsRequired = true, Name = "IdVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Venda", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.ID_VENDA];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For IdVendedor
	    partial void OnIdVendedorChanging(System.Nullable<int> value);
	    partial void OnIdVendedorChanged();

	    private System.Nullable<int> _IdVendedor;

	    [DataMember(Name = "IdVendedor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Vendedor", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpVendedor];LookUpTitle[Seleção de (Id Vendedor)];LookUpQuery[executeLookUpVendedor];LookUpFinalize[finalizeLookUpVendedor];LookUpDisplayColumns[{\"IdVendedor\" : \"Id Vendedor\", \"StringVendedor\" : \"String Vendedor\"}];LookUpColumns[{\"IdVendedor\" : true, \"StringVendedor\" : true}];FilterDataKey[VENDA.VENDEDOR.ID_VENDEDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<int>#IdVendedor#true##10:0##Id Vendedor#0#true##::LookUpVendedor##true#false#VENDEDOR#VENDEDOR#Linx.Demo.BV.PaiFilha#IQueryable###true#false", EdmKey="VENDA.VENDEDOR.ID_VENDEDOR")]
	    public System.Nullable<int> IdVendedor
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
	    //Extensibility Partial Method Definitions For IntVenda
	    partial void OnIntVendaChanging(System.Nullable<int> value);
	    partial void OnIntVendaChanged();

	    private System.Nullable<int> _IntVenda;

	    [DataMember(Name = "IntVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int Venda", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.INT_VENDA];IsMeasure[true]")]
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
	    [Display(Name = "Small Int Venda", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[5:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.SMALL_INT_VENDA];IsMeasure[true]")]
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
	    [Display(Name = "String Venda", Description="", Order = 14, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    //Extensibility Partial Method Definitions For StringVendedor
	    partial void OnStringVendedorChanging(string value);
	    partial void OnStringVendedorChanged();

	    private string _StringVendedor;

	    [DataMember(Name = "StringVendedor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Vendedor", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpVendedor];LookUpTitle[Seleção de (String Vendedor)];LookUpQuery[executeLookUpVendedor];LookUpFinalize[finalizeLookUpVendedor];LookUpDisplayColumns[{\"IdVendedor\" : \"Id Vendedor\", \"StringVendedor\" : \"String Vendedor\"}];LookUpColumns[{\"IdVendedor\" : true, \"StringVendedor\" : true}];FilterDataKey[VENDA.VENDEDOR.STRING_VENDEDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#StringVendedor#false##50:0##String Vendedor#1#true##::LookUpVendedor##true#false#VENDEDOR#VENDEDOR#Linx.Demo.BV.PaiFilha#IQueryable###true#false", EdmKey="VENDA.VENDEDOR.STRING_VENDEDOR")]
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
	    [Association("FK_Loja_Venda", "IdLoja", "IdLoja", IsForeignKey=true)]
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
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "BMDTesteFrame.VENDA").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Demo.BM.VENDA), QualifiedEntitySetName = "BMDTesteFrame.VENDA" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.ID_VENDA", Source = "IdVenda", Target = "ID_VENDA", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.BIT_VENDA", Source = "BitVenda", Target = "BIT_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.INT_VENDA", Source = "IntVenda", Target = "INT_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.LOJA.ID_LOJA", Source = "IdLoja", Target = "ID_LOJA", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BMDTesteFrame.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.STRING_VENDA", Source = "StringVenda", Target = "STRING_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.DECIMAL_VENDA", Source = "DecimalVenda", Target = "DECIMAL_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.COMBOBOX_VENDA", Source = "ComboboxVenda", Target = "COMBOBOX_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.DATETIME_VENDA", Source = "DatetimeVenda", Target = "DATETIME_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.SMALL_INT_VENDA", Source = "SmallIntVenda", Target = "SMALL_INT_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.VENDEDOR.ID_VENDEDOR", Source = "IdVendedor", Target = "ID_VENDEDOR", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BMDTesteFrame.VENDEDOR", RelationPropertyName = "VENDEDOR" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

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

		

	[LinxPublicationView(PrimaryKeys="VENDA_ITEM.ID_VENDA_ITEM", IsUpdatable=false, EdmName="Linx.Demo.BM.BMDTesteFrame")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[VendaItem];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#.VENDA_ITEM_LISTA as #Alias#];EdmEntityName[VENDA_ITEM];EntityRelations[VENDA(VENDA)#LOJA(LOJA)#CIDADE(CIDADE)#ESTADO(ESTADO)#VENDEDOR(VENDEDOR)];EdmParentEntityName[VENDA];IsIQueryable[true]")]
		
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

	    #endregion Flat Entities

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For ComboboxVendaItem
	    partial void OnComboboxVendaItemChanging(byte value);
	    partial void OnComboboxVendaItemChanged();

	    private byte _ComboboxVendaItem;

	    [DataMember(IsRequired = true, Name = "ComboboxVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Venda Item", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_VENDA_ITEM];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.COMBOBOX_VENDA_ITEM];IsMeasure[true]")]
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
	    [Display(Name = "Datetime Venda Item", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    [Display(Name = "Decimal Venda Item", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.DECIMAL_VENDA_ITEM];IsMeasure[true]")]
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
	    //Extensibility Partial Method Definitions For IdVenda
	    partial void OnIdVendaChanging(System.Nullable<int> value);
	    partial void OnIdVendaChanged();

	    private System.Nullable<int> _IdVenda;

	    [DataMember(Name = "IdVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Venda", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.VENDA.ID_VENDA];IsMeasure[false]")]
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
	    [Display(Name = "Id Venda Item", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.ID_VENDA_ITEM];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For StringVendaItem
	    partial void OnStringVendaItemChanging(string value);
	    partial void OnStringVendaItemChanged();

	    private string _StringVendaItem;

	    [DataMember(Name = "StringVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Venda Item", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "BMDTesteFrame.VENDA_ITEM").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Demo.BM.VENDA_ITEM), QualifiedEntitySetName = "BMDTesteFrame.VENDA_ITEM" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.ID_VENDA_ITEM", Source = "IdVendaItem", Target = "ID_VENDA_ITEM", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.VENDA.ID_VENDA", Source = "IdVenda", Target = "ID_VENDA", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BMDTesteFrame.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.STRING_VENDA_ITEM", Source = "StringVendaItem", Target = "STRING_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.DECIMAL_VENDA_ITEM", Source = "DecimalVendaItem", Target = "DECIMAL_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.COMBOBOX_VENDA_ITEM", Source = "ComboboxVendaItem", Target = "COMBOBOX_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.DATETIME_VENDA_ITEM", Source = "DatetimeVendaItem", Target = "DATETIME_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

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

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Venda];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#.VENDA_LISTA as #Alias#];EdmEntityName[VENDA];EntityRelations[LOJA(LOJA)#CIDADE(CIDADE)#ESTADO(ESTADO)#PAIS(PAIS)#VENDEDOR(VENDEDOR)];EdmParentEntityName[LOJA];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "Venda")]
	[Serializable()]
	public partial class VendaParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

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
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_VENDA];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.COMBOBOX_VENDA];IsMeasure[true]")]
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
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.DECIMAL_VENDA];IsMeasure[true]")]
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
	    //Extensibility Partial Method Definitions For IdLoja
	    partial void OnIdLojaChanging(System.Nullable<int> value);
	    partial void OnIdLojaChanged();

	    private System.Nullable<int> _IdLoja;

	    [DataMember(Name = "IdLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Loja", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.LOJA.ID_LOJA];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For IdVenda
	    partial void OnIdVendaChanging(int value);
	    partial void OnIdVendaChanged();

	    private int _IdVenda;

	    [DataMember(IsRequired = true, Name = "IdVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Venda", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.ID_VENDA];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For IdVendedor
	    partial void OnIdVendedorChanging(System.Nullable<int> value);
	    partial void OnIdVendedorChanged();

	    private System.Nullable<int> _IdVendedor;

	    [DataMember(Name = "IdVendedor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Vendedor", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpVendedor];LookUpTitle[Seleção de (Id Vendedor)];LookUpQuery[executeLookUpVendedor];LookUpFinalize[finalizeLookUpVendedor];LookUpDisplayColumns[{\"IdVendedor\" : \"Id Vendedor\", \"StringVendedor\" : \"String Vendedor\"}];LookUpColumns[{\"IdVendedor\" : true, \"StringVendedor\" : true}];FilterDataKey[VENDA.VENDEDOR.ID_VENDEDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<int>#IdVendedor#true##10:0##Id Vendedor#0#true##::LookUpVendedor##true#false#VENDEDOR#VENDEDOR#Linx.Demo.BV.PaiFilha#IQueryable###true#false", EdmKey="VENDA.VENDEDOR.ID_VENDEDOR")]
	    public System.Nullable<int> IdVendedor
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
	    //Extensibility Partial Method Definitions For IntVenda
	    partial void OnIntVendaChanging(System.Nullable<int> value);
	    partial void OnIntVendaChanged();

	    private System.Nullable<int> _IntVenda;

	    [DataMember(Name = "IntVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int Venda", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.INT_VENDA];IsMeasure[true]")]
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
	    [Display(Name = "Small Int Venda", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[5:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.SMALL_INT_VENDA];IsMeasure[true]")]
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
	    [Display(Name = "String Venda", Description="", Order = 14, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    //Extensibility Partial Method Definitions For StringVendedor
	    partial void OnStringVendedorChanging(string value);
	    partial void OnStringVendedorChanged();

	    private string _StringVendedor;

	    [DataMember(Name = "StringVendedor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Vendedor", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpVendedor];LookUpTitle[Seleção de (String Vendedor)];LookUpQuery[executeLookUpVendedor];LookUpFinalize[finalizeLookUpVendedor];LookUpDisplayColumns[{\"IdVendedor\" : \"Id Vendedor\", \"StringVendedor\" : \"String Vendedor\"}];LookUpColumns[{\"IdVendedor\" : true, \"StringVendedor\" : true}];FilterDataKey[VENDA.VENDEDOR.STRING_VENDEDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#StringVendedor#false##50:0##String Vendedor#1#true##::LookUpVendedor##true#false#VENDEDOR#VENDEDOR#Linx.Demo.BV.PaiFilha#IQueryable###true#false", EdmKey="VENDA.VENDEDOR.STRING_VENDEDOR")]
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
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA.LOJA.BIG_INT_LOJA];IsMeasure[false]")]
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
	    [FunctionalPoint("Precision[10];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA.LOJA.BIT_LOJA];IsMeasure[false]")]
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
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_COMBOBOX_LOJA];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA.LOJA.COMBOBOX_LOJA];IsMeasure[false]")]
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
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[DateTime.Now];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA.LOJA.DATETIME_LOJA];IsMeasure[false]")]
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
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA.LOJA.DECIMAL_LOJA];IsMeasure[false]")]
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
	    [Display(Name = "Guid Loja", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA.LOJA.GUID_LOJA];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For IdCidade
	    partial void OnIdCidadeChanging(System.Nullable<int> value);
	    partial void OnIdCidadeChanged();

	    private System.Nullable<int> _IdCidade;

	    [DataMember(Name = "IdCidade", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Cidade", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA.LOJA.CIDADE.ID_CIDADE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.CIDADE.ID_CIDADE")]
	    public System.Nullable<int> IdCidade
	    {
	    	    get
	    	    {
	    	          return _IdCidade;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdCidade != value)
	    	          {
	    	              this.ValidateProperty("IdCidade", value);
	    	              this.OnIdCidadeChanging(value);
	    	              this.RaiseDataMemberChanging("IdCidade");
	    	              this._IdCidade = value;
	    	              this.RaiseDataMemberChanged("IdCidade");
	    	              this.OnIdCidadeChanged();
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
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA.LOJA.CIDADE.ESTADO.ID_ESTADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.CIDADE.ESTADO.ID_ESTADO")]
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
	    //Extensibility Partial Method Definitions For IdPais
	    partial void OnIdPaisChanging(System.Nullable<int> value);
	    partial void OnIdPaisChanged();

	    private System.Nullable<int> _IdPais;

	    [DataMember(Name = "IdPais", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Pais", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA.LOJA.CIDADE.ESTADO.PAIS.ID_PAIS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.CIDADE.ESTADO.PAIS.ID_PAIS")]
	    public System.Nullable<int> IdPais
	    {
	    	    get
	    	    {
	    	          return _IdPais;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdPais != value)
	    	          {
	    	              this.ValidateProperty("IdPais", value);
	    	              this.OnIdPaisChanging(value);
	    	              this.RaiseDataMemberChanging("IdPais");
	    	              this._IdPais = value;
	    	              this.RaiseDataMemberChanged("IdPais");
	    	              this.OnIdPaisChanged();
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
	    [Display(Name = "Int Loja", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA.LOJA.INT_LOJA];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For NomeCidade
	    partial void OnNomeCidadeChanging(string value);
	    partial void OnNomeCidadeChanged();

	    private string _NomeCidade;

	    [DataMember(Name = "NomeCidade", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Cidade", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA.LOJA.CIDADE.NOME_CIDADE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.CIDADE.NOME_CIDADE")]
	    public string NomeCidade
	    {
	    	    get
	    	    {
	    	          return _NomeCidade;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeCidade != value)
	    	          {
	    	              this.ValidateProperty("NomeCidade", value);
	    	              this.OnNomeCidadeChanging(value);
	    	              this.RaiseDataMemberChanging("NomeCidade");
	    	              this._NomeCidade = value;
	    	              this.RaiseDataMemberChanged("NomeCidade");
	    	              this.OnNomeCidadeChanged();
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
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA.LOJA.SMALL_INT_LOJA];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For StringEstado
	    partial void OnStringEstadoChanging(string value);
	    partial void OnStringEstadoChanged();

	    private string _StringEstado;

	    [DataMember(Name = "StringEstado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Estado", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA.LOJA.CIDADE.ESTADO.STRING_ESTADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.CIDADE.ESTADO.STRING_ESTADO")]
	    public string StringEstado
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
	    //Extensibility Partial Method Definitions For StringLoja
	    partial void OnStringLojaChanging(string value);
	    partial void OnStringLojaChanged();

	    private string _StringLoja;

	    [DataMember(Name = "StringLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Loja", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA.LOJA.STRING_LOJA];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For StringPais
	    partial void OnStringPaisChanging(string value);
	    partial void OnStringPaisChanged();

	    private string _StringPais;

	    [DataMember(Name = "StringPais", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Pais", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA.LOJA.CIDADE.ESTADO.PAIS.STRING_PAIS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.CIDADE.ESTADO.PAIS.STRING_PAIS")]
	    public string StringPais
	    {
	    	    get
	    	    {
	    	          return _StringPais;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringPais != value)
	    	          {
	    	              this.ValidateProperty("StringPais", value);
	    	              this.OnStringPaisChanging(value);
	    	              this.RaiseDataMemberChanging("StringPais");
	    	              this._StringPais = value;
	    	              this.RaiseDataMemberChanged("StringPais");
	    	              this.OnStringPaisChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "BMDTesteFrame.VENDA").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Demo.BM.VENDA), QualifiedEntitySetName = "BMDTesteFrame.VENDA" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.ID_VENDA", Source = "IdVenda", Target = "ID_VENDA", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.BIT_VENDA", Source = "BitVenda", Target = "BIT_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.INT_VENDA", Source = "IntVenda", Target = "INT_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.LOJA.ID_LOJA", Source = "IdLoja", Target = "ID_LOJA", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BMDTesteFrame.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.STRING_VENDA", Source = "StringVenda", Target = "STRING_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.DECIMAL_VENDA", Source = "DecimalVenda", Target = "DECIMAL_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.COMBOBOX_VENDA", Source = "ComboboxVenda", Target = "COMBOBOX_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.DATETIME_VENDA", Source = "DatetimeVenda", Target = "DATETIME_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.SMALL_INT_VENDA", Source = "SmallIntVenda", Target = "SMALL_INT_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.VENDEDOR.ID_VENDEDOR", Source = "IdVendedor", Target = "ID_VENDEDOR", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BMDTesteFrame.VENDEDOR", RelationPropertyName = "VENDEDOR" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

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
	    public Dictionary<string, string> GetComboboxLojaValues()
	    {
	    	    return Linx.Demo.BV.Domains.LX_COMBOBOX_LOJA.GetValues();
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

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[VendaItem];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#.VENDA_ITEM_LISTA as #Alias#];EdmEntityName[VENDA_ITEM];EntityRelations[VENDA(VENDA)#LOJA(LOJA)#CIDADE(CIDADE)#ESTADO(ESTADO)#VENDEDOR(VENDEDOR)];EdmParentEntityName[VENDA];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "VendaItem")]
	[Serializable()]
	public partial class VendaItemParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For ComboboxVendaItem
	    partial void OnComboboxVendaItemChanging(byte value);
	    partial void OnComboboxVendaItemChanged();

	    private byte _ComboboxVendaItem;

	    [DataMember(IsRequired = true, Name = "ComboboxVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Venda Item", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_VENDA_ITEM];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.COMBOBOX_VENDA_ITEM];IsMeasure[true]")]
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
	    [Display(Name = "Datetime Venda Item", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    [Display(Name = "Decimal Venda Item", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.DECIMAL_VENDA_ITEM];IsMeasure[true]")]
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
	    //Extensibility Partial Method Definitions For IdVenda
	    partial void OnIdVendaChanging(System.Nullable<int> value);
	    partial void OnIdVendaChanged();

	    private System.Nullable<int> _IdVenda;

	    [DataMember(Name = "IdVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Venda", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.VENDA.ID_VENDA];IsMeasure[false]")]
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
	    [Display(Name = "Id Venda Item", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.ID_VENDA_ITEM];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For StringVendaItem
	    partial void OnStringVendaItemChanging(string value);
	    partial void OnStringVendaItemChanged();

	    private string _StringVendaItem;

	    [DataMember(Name = "StringVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Venda Item", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_VENDA];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.COMBOBOX_VENDA];IsMeasure[true]")]
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
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.DECIMAL_VENDA];IsMeasure[true]")]
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
	    //Extensibility Partial Method Definitions For IdLoja
	    partial void OnIdLojaChanging(System.Nullable<int> value);
	    partial void OnIdLojaChanged();

	    private System.Nullable<int> _IdLoja;

	    [DataMember(Name = "IdLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Loja", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.LOJA.ID_LOJA];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For IdVendedor
	    partial void OnIdVendedorChanging(System.Nullable<int> value);
	    partial void OnIdVendedorChanged();

	    private System.Nullable<int> _IdVendedor;

	    [DataMember(Name = "IdVendedor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Vendedor", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.VENDEDOR.ID_VENDEDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.VENDEDOR.ID_VENDEDOR")]
	    public System.Nullable<int> IdVendedor
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
	    //Extensibility Partial Method Definitions For IntVenda
	    partial void OnIntVendaChanging(System.Nullable<int> value);
	    partial void OnIntVendaChanged();

	    private System.Nullable<int> _IntVenda;

	    [DataMember(Name = "IntVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int Venda", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.INT_VENDA];IsMeasure[true]")]
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
	    [Display(Name = "Small Int Venda", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[5:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.SMALL_INT_VENDA];IsMeasure[true]")]
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
	    [Display(Name = "String Venda", Description="", Order = 14, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    //Extensibility Partial Method Definitions For StringVendedor
	    partial void OnStringVendedorChanging(string value);
	    partial void OnStringVendedorChanged();

	    private string _StringVendedor;

	    [DataMember(Name = "StringVendedor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Vendedor", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.VENDEDOR.STRING_VENDEDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.VENDEDOR.STRING_VENDEDOR")]
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
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.LOJA.BIG_INT_LOJA];IsMeasure[false]")]
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
	    [FunctionalPoint("Precision[10];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.LOJA.BIT_LOJA];IsMeasure[false]")]
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
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_COMBOBOX_LOJA];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.LOJA.COMBOBOX_LOJA];IsMeasure[false]")]
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
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[DateTime.Now];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.LOJA.DATETIME_LOJA];IsMeasure[false]")]
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
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.LOJA.DECIMAL_LOJA];IsMeasure[false]")]
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
	    [Display(Name = "Guid Loja", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.LOJA.GUID_LOJA];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For IdCidade
	    partial void OnIdCidadeChanging(System.Nullable<int> value);
	    partial void OnIdCidadeChanged();

	    private System.Nullable<int> _IdCidade;

	    [DataMember(Name = "IdCidade", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Cidade", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.LOJA.CIDADE.ID_CIDADE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.CIDADE.ID_CIDADE")]
	    public System.Nullable<int> IdCidade
	    {
	    	    get
	    	    {
	    	          return _IdCidade;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdCidade != value)
	    	          {
	    	              this.ValidateProperty("IdCidade", value);
	    	              this.OnIdCidadeChanging(value);
	    	              this.RaiseDataMemberChanging("IdCidade");
	    	              this._IdCidade = value;
	    	              this.RaiseDataMemberChanged("IdCidade");
	    	              this.OnIdCidadeChanged();
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
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.LOJA.CIDADE.ESTADO.ID_ESTADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.CIDADE.ESTADO.ID_ESTADO")]
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
	    //Extensibility Partial Method Definitions For IdPais
	    partial void OnIdPaisChanging(System.Nullable<int> value);
	    partial void OnIdPaisChanged();

	    private System.Nullable<int> _IdPais;

	    [DataMember(Name = "IdPais", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Pais", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.LOJA.CIDADE.ESTADO.PAIS.ID_PAIS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.CIDADE.ESTADO.PAIS.ID_PAIS")]
	    public System.Nullable<int> IdPais
	    {
	    	    get
	    	    {
	    	          return _IdPais;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdPais != value)
	    	          {
	    	              this.ValidateProperty("IdPais", value);
	    	              this.OnIdPaisChanging(value);
	    	              this.RaiseDataMemberChanging("IdPais");
	    	              this._IdPais = value;
	    	              this.RaiseDataMemberChanged("IdPais");
	    	              this.OnIdPaisChanged();
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
	    [Display(Name = "Int Loja", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.LOJA.INT_LOJA];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For NomeCidade
	    partial void OnNomeCidadeChanging(string value);
	    partial void OnNomeCidadeChanged();

	    private string _NomeCidade;

	    [DataMember(Name = "NomeCidade", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Cidade", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.LOJA.CIDADE.NOME_CIDADE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.CIDADE.NOME_CIDADE")]
	    public string NomeCidade
	    {
	    	    get
	    	    {
	    	          return _NomeCidade;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeCidade != value)
	    	          {
	    	              this.ValidateProperty("NomeCidade", value);
	    	              this.OnNomeCidadeChanging(value);
	    	              this.RaiseDataMemberChanging("NomeCidade");
	    	              this._NomeCidade = value;
	    	              this.RaiseDataMemberChanged("NomeCidade");
	    	              this.OnNomeCidadeChanged();
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
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.LOJA.SMALL_INT_LOJA];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For StringEstado
	    partial void OnStringEstadoChanging(string value);
	    partial void OnStringEstadoChanged();

	    private string _StringEstado;

	    [DataMember(Name = "StringEstado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Estado", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.LOJA.CIDADE.ESTADO.STRING_ESTADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.CIDADE.ESTADO.STRING_ESTADO")]
	    public string StringEstado
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
	    //Extensibility Partial Method Definitions For StringLoja
	    partial void OnStringLojaChanging(string value);
	    partial void OnStringLojaChanged();

	    private string _StringLoja;

	    [DataMember(Name = "StringLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Loja", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.LOJA.STRING_LOJA];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For StringPais
	    partial void OnStringPaisChanging(string value);
	    partial void OnStringPaisChanged();

	    private string _StringPais;

	    [DataMember(Name = "StringPais", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Pais", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.LOJA.CIDADE.ESTADO.PAIS.STRING_PAIS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.CIDADE.ESTADO.PAIS.STRING_PAIS")]
	    public string StringPais
	    {
	    	    get
	    	    {
	    	          return _StringPais;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringPais != value)
	    	          {
	    	              this.ValidateProperty("StringPais", value);
	    	              this.OnStringPaisChanging(value);
	    	              this.RaiseDataMemberChanging("StringPais");
	    	              this._StringPais = value;
	    	              this.RaiseDataMemberChanged("StringPais");
	    	              this.OnStringPaisChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "BMDTesteFrame.VENDA_ITEM").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Demo.BM.VENDA_ITEM), QualifiedEntitySetName = "BMDTesteFrame.VENDA_ITEM" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.ID_VENDA_ITEM", Source = "IdVendaItem", Target = "ID_VENDA_ITEM", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.VENDA.ID_VENDA", Source = "IdVenda", Target = "ID_VENDA", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BMDTesteFrame.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.STRING_VENDA_ITEM", Source = "StringVendaItem", Target = "STRING_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.DECIMAL_VENDA_ITEM", Source = "DecimalVendaItem", Target = "DECIMAL_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.COMBOBOX_VENDA_ITEM", Source = "ComboboxVendaItem", Target = "COMBOBOX_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.DATETIME_VENDA_ITEM", Source = "DatetimeVendaItem", Target = "DATETIME_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

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
	    public Dictionary<string, string> GetComboboxLojaValues()
	    {
	    	    return Linx.Demo.BV.Domains.LX_COMBOBOX_LOJA.GetValues();
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
	
	    private Linx.Demo.BM.BMDTesteFrame _dbContext;
	    protected Linx.Demo.BM.BMDTesteFrame DbContext 
	    { 
	    	get 
	    	{
	        	if (this._dbContext == null)
	        	{
	        		this._dbContext = new Linx.Demo.BM.BMDTesteFrame(connectionString, this.Headers);
	        		((System.Data.Entity.Infrastructure.IObjectContextAdapter)this._dbContext).ObjectContext.CommandTimeout = 180;
	        		this._hasGpeconControl = (!(this._dbContext.IsUserMultiGpecon && this._dbContext.IdGpecon == this._dbContext.IdLinx) && this._dbContext.IdGpecon > 0);		
	        	}
	        	return this._dbContext;
	    	}
	    }

	    public string GetModelAssemblyName()
	    {
	        return typeof(Linx.Demo.BM.BMDTesteFrame).Assembly.FullName;
	    }

	    public System.Data.Entity.Database Database
	    {
	        get { return this.DbContext.Database; }
	    }

		
	    public PaiFilhaDomainService() : this("", null, null) { }
	    public PaiFilhaDomainService(string connectionString) : this(connectionString, null, null) { }
	    public PaiFilhaDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public PaiFilhaDomainService(Linx.Demo.BM.BMDTesteFrame dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public PaiFilhaDomainService(string connectionString, Linx.Demo.BM.BMDTesteFrame dataContext, Dictionary<string, string> headers) : base() 
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
	    public Linx.Demo.BM.BMDTesteFrame GetEDM()
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
 	        var _LojaElements = changeSet.ChangeSetEntries.Where(e => e.Entity is Loja && e.Entity.GetType().Name == "Loja" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _LojaElements)
 	           if (((Loja)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 
 	        //Remove inconsistent details
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is Venda && e.Entity.GetType().Name == "Venda" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
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
	    //Get All LookUpCidade.
	    public IQueryable<LookUpCidade> GetAllLookUpCidade()
	    {
	        return this.GetLookUpCidade(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpCidade By EntitySearch.
	    public IQueryable<LookUpCidade> GetLookUpCidadeByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpCidade(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpCidade.
	    public IQueryable<LookUpCidade> GetLookUpCidade(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "CIDADE" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpCidade";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpCidade));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpCidade> query =  
	
	            (from entity in this.DbContext.CIDADE.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.ESTADO
                  let entityAl2 = entity.ESTADO.PAIS
	            
	            select new LookUpCidade()		
	            {
	            
                IdEstado = entityAl1.ID_ESTADO
                , IdPais = entityAl2.ID_PAIS
                , StringPais = entityAl2.STRING_PAIS
                , StringEstado = entityAl1.STRING_ESTADO
                , IdCidade = entity.ID_CIDADE
                , NomeCidade = entity.NOME_CIDADE
	            });

	            
            //Inner Group Definition
            if (propertyName.InList("IdEstado", "StringEstado"))
            {
               query = (from r in query select new LookUpCidade() {
               IdEstado = r.IdEstado
               , IdPais = r.IdPais
               , StringPais = r.StringPais
               , StringEstado = r.StringEstado
               , IdCidade = default(System.Nullable<int>)
               , NomeCidade = ""
                }).Distinct();
            }
            else if (propertyName.InList("IdPais", "StringPais"))
            {
               query = (from r in query select new LookUpCidade() {
               IdEstado = default(System.Nullable<int>)
               , IdPais = r.IdPais
               , StringPais = r.StringPais
               , StringEstado = ""
               , IdCidade = default(System.Nullable<int>)
               , NomeCidade = ""
                }).Distinct();
            }
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpVendedor.
	    public IQueryable<LookUpVendedor> GetAllLookUpVendedor()
	    {
	        return this.GetLookUpVendedor(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpVendedor By EntitySearch.
	    public IQueryable<LookUpVendedor> GetLookUpVendedorByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpVendedor(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpVendedor.
	    public IQueryable<LookUpVendedor> GetLookUpVendedor(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "VENDEDOR" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpVendedor";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpVendedor));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpVendedor> query =  
	
	            (from entity in this.DbContext.VENDEDOR.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpVendedor()		
	            {
	            
                IdVendedor = entity.ID_VENDEDOR
                , StringVendedor = entity.STRING_VENDEDOR
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
		

	        if (entityName.InList("Linx.Demo.BV.PaiFilha.Loja", "Linx.Demo.BV.PaiFilha.Venda"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "Venda" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Demo.BV.PaiFilha",
	        			HasQuickSearch = false,
	        			ParentClassName = "Loja",	
	        			DisplayName = "Venda",
	        			ClearMethodName = "ClearVenda" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedVenda" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetVenda" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Demo.BV.PaiFilha.Venda"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Demo.BV.PaiFilha.Venda" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Demo.BV.PaiFilha.Loja", "Linx.Demo.BV.PaiFilha.VendaItem"))
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
	    //Clear Loja.
	    public IEnumerable<Loja> ClearLoja()
	    {
	        List<Loja> result = new List<Loja>();
	        result.Add(new Loja(false));	
			
	        result[0].VendaList = new List<Venda>();
	        ((List<Venda>)result[0].VendaList).Add(new Venda());
			
	        ((List<Venda>)result[0].VendaList)[0].VendaItemList = new List<VendaItem>();
	        ((List<VendaItem>)((List<Venda>)result[0].VendaList)[0].VendaItemList).Add(new VendaItem());
		
	        

	
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
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    [LojaQueryCustomAuthorizationAuto()]
	    [Query(HasSideEffects = false)]
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
                  let entity0Al1 = entity0.CIDADE
                  let entity0Al2 = entity0.CIDADE.ESTADO
                  let entity0Al3 = entity0.CIDADE.ESTADO.PAIS
	            
	            	
	            select new Loja()		
	            {
	            
                BigIntLoja = entity0.BIG_INT_LOJA
                , BitLoja = entity0.BIT_LOJA
                , ComboboxLoja = entity0.COMBOBOX_LOJA
                , ComboboxLojaName = ((entity0.COMBOBOX_LOJA) == 1 ? "LOJA1" : ((entity0.COMBOBOX_LOJA) == 2 ? "LOJA2" : ((entity0.COMBOBOX_LOJA) == 3 ? "LOJA3" : ((entity0.COMBOBOX_LOJA) == 4 ? "LOJA4" : ""))))
                , DatetimeLoja = entity0.DATETIME_LOJA
                , DecimalLoja = entity0.DECIMAL_LOJA
                , GuidLoja = entity0.GUID_LOJA
                , IdCidade = entity0Al1.ID_CIDADE
                , IdEstado = entity0Al2.ID_ESTADO
                , IdLoja = entity0.ID_LOJA
                , IdPais = entity0Al3.ID_PAIS
                , IntLoja = entity0.INT_LOJA
                , NomeCidade = entity0Al1.NOME_CIDADE
                , SmallIntLoja = entity0.SMALL_INT_LOJA
                , StringEstado = entity0Al2.STRING_ESTADO
                , StringLoja = entity0.STRING_LOJA
                , StringPais = entity0Al3.STRING_PAIS
			
                ,VendaList = 
	                        (from entity1 in entity0.VENDA_LISTA
                                  let entity1Al1 = entity1.LOJA
                                  let entity1Al2 = entity1.VENDEDOR
	                        
	                        	
	                        select new Venda()
	                        {
	                        
                                BitVenda = entity1.BIT_VENDA
                                , ComboboxVenda = entity1.COMBOBOX_VENDA
                                , ComboboxVendaName = ((entity1.COMBOBOX_VENDA) == 1 ? "VENDA1" : ((entity1.COMBOBOX_VENDA) == 2 ? "VENDA2" : ((entity1.COMBOBOX_VENDA) == 3 ? "VENDA3" : "")))
                                , DatetimeVenda = entity1.DATETIME_VENDA
                                , DecimalVenda = entity1.DECIMAL_VENDA
                                , IdLoja = entity1Al1.ID_LOJA
                                , IdVenda = entity1.ID_VENDA
                                , IdVendedor = entity1Al2.ID_VENDEDOR
                                , IntVenda = entity1.INT_VENDA
                                , SmallIntVenda = entity1.SMALL_INT_VENDA
                                , StringVenda = entity1.STRING_VENDA
                                , StringVendedor = entity1Al2.STRING_VENDEDOR
			
                                ,VendaItemList = 
	                                                (from entity2 in entity1.VENDA_ITEM_LISTA
                                                                  let entity2Al1 = entity2.VENDA
	                                                
	                                                	
	                                                select new VendaItem()
	                                                {
	                                                
                                                                ComboboxVendaItem = entity2.COMBOBOX_VENDA_ITEM
                                                                , ComboboxVendaItemName = ((entity2.COMBOBOX_VENDA_ITEM) == 1 ? "VENDA_ITEM1" : ((entity2.COMBOBOX_VENDA_ITEM) == 2 ? "VENDA_ITEM2" : ((entity2.COMBOBOX_VENDA_ITEM) == 3 ? "VENDA_ITEM3" : ((entity2.COMBOBOX_VENDA_ITEM) == 4 ? "VENDA_ITEM4" : ""))))
                                                                , DatetimeVendaItem = entity2.DATETIME_VENDA_ITEM
                                                                , DecimalVendaItem = entity2.DECIMAL_VENDA_ITEM
                                                                , IdVenda = entity2Al1.ID_VENDA
                                                                , IdVendaItem = entity2.ID_VENDA_ITEM
                                                                , StringVendaItem = entity2.STRING_VENDA_ITEM
		
	                                                }
	                                                )
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendaQueryCustomAuthorizationAuto()]
	    [Query(HasSideEffects = false)]
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
                  let entity0Al1 = entity0.LOJA
                  let entity0Al2 = entity0.VENDEDOR
	            
	            	
	            select new Venda()		
	            {
	            
                BitVenda = entity0.BIT_VENDA
                , ComboboxVenda = entity0.COMBOBOX_VENDA
                , ComboboxVendaName = ((entity0.COMBOBOX_VENDA) == 1 ? "VENDA1" : ((entity0.COMBOBOX_VENDA) == 2 ? "VENDA2" : ((entity0.COMBOBOX_VENDA) == 3 ? "VENDA3" : "")))
                , DatetimeVenda = entity0.DATETIME_VENDA
                , DecimalVenda = entity0.DECIMAL_VENDA
                , IdLoja = entity0Al1.ID_LOJA
                , IdVenda = entity0.ID_VENDA
                , IdVendedor = entity0Al2.ID_VENDEDOR
                , IntVenda = entity0.INT_VENDA
                , SmallIntVenda = entity0.SMALL_INT_VENDA
                , StringVenda = entity0.STRING_VENDA
                , StringVendedor = entity0Al2.STRING_VENDEDOR
			
                ,VendaItemList = 
	                        (from entity1 in entity0.VENDA_ITEM_LISTA
                                  let entity1Al1 = entity1.VENDA
	                        
	                        	
	                        select new VendaItem()
	                        {
	                        
                                ComboboxVendaItem = entity1.COMBOBOX_VENDA_ITEM
                                , ComboboxVendaItemName = ((entity1.COMBOBOX_VENDA_ITEM) == 1 ? "VENDA_ITEM1" : ((entity1.COMBOBOX_VENDA_ITEM) == 2 ? "VENDA_ITEM2" : ((entity1.COMBOBOX_VENDA_ITEM) == 3 ? "VENDA_ITEM3" : ((entity1.COMBOBOX_VENDA_ITEM) == 4 ? "VENDA_ITEM4" : ""))))
                                , DatetimeVendaItem = entity1.DATETIME_VENDA_ITEM
                                , DecimalVendaItem = entity1.DECIMAL_VENDA_ITEM
                                , IdVenda = entity1Al1.ID_VENDA
                                , IdVendaItem = entity1.ID_VENDA_ITEM
                                , StringVendaItem = entity1.STRING_VENDA_ITEM
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendaItemQueryCustomAuthorizationAuto()]
	    [Query(HasSideEffects = false)]
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
	            
                ComboboxVendaItem = entity0.COMBOBOX_VENDA_ITEM
                , ComboboxVendaItemName = ((entity0.COMBOBOX_VENDA_ITEM) == 1 ? "VENDA_ITEM1" : ((entity0.COMBOBOX_VENDA_ITEM) == 2 ? "VENDA_ITEM2" : ((entity0.COMBOBOX_VENDA_ITEM) == 3 ? "VENDA_ITEM3" : ((entity0.COMBOBOX_VENDA_ITEM) == 4 ? "VENDA_ITEM4" : ""))))
                , DatetimeVendaItem = entity0.DATETIME_VENDA_ITEM
                , DecimalVendaItem = entity0.DECIMAL_VENDA_ITEM
                , IdVenda = entity0Al1.ID_VENDA
                , IdVendaItem = entity0.ID_VENDA_ITEM
                , StringVendaItem = entity0.STRING_VENDA_ITEM
		
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
                  let entity0Al1 = entity0.CIDADE
                  let entity0Al2 = entity0.CIDADE.ESTADO
                  let entity0Al3 = entity0.CIDADE.ESTADO.PAIS
	            
	            	
	            select new Loja()		
	            {
	            
                BigIntLoja = entity0.BIG_INT_LOJA
                , BitLoja = entity0.BIT_LOJA
                , ComboboxLoja = entity0.COMBOBOX_LOJA
                , ComboboxLojaName = ((entity0.COMBOBOX_LOJA) == 1 ? "LOJA1" : ((entity0.COMBOBOX_LOJA) == 2 ? "LOJA2" : ((entity0.COMBOBOX_LOJA) == 3 ? "LOJA3" : ((entity0.COMBOBOX_LOJA) == 4 ? "LOJA4" : ""))))
                , DatetimeLoja = entity0.DATETIME_LOJA
                , DecimalLoja = entity0.DECIMAL_LOJA
                , GuidLoja = entity0.GUID_LOJA
                , IdCidade = entity0Al1.ID_CIDADE
                , IdEstado = entity0Al2.ID_ESTADO
                , IdLoja = entity0.ID_LOJA
                , IdPais = entity0Al3.ID_PAIS
                , IntLoja = entity0.INT_LOJA
                , NomeCidade = entity0Al1.NOME_CIDADE
                , SmallIntLoja = entity0.SMALL_INT_LOJA
                , StringEstado = entity0Al2.STRING_ESTADO
                , StringLoja = entity0.STRING_LOJA
                , StringPais = entity0Al3.STRING_PAIS
		
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
                  let entity0Al1 = entity0.LOJA
                  let entity0Al2 = entity0.VENDEDOR
	            
	            	
	            select new Venda()		
	            {
	            
                BitVenda = entity0.BIT_VENDA
                , ComboboxVenda = entity0.COMBOBOX_VENDA
                , ComboboxVendaName = ((entity0.COMBOBOX_VENDA) == 1 ? "VENDA1" : ((entity0.COMBOBOX_VENDA) == 2 ? "VENDA2" : ((entity0.COMBOBOX_VENDA) == 3 ? "VENDA3" : "")))
                , DatetimeVenda = entity0.DATETIME_VENDA
                , DecimalVenda = entity0.DECIMAL_VENDA
                , IdLoja = entity0Al1.ID_LOJA
                , IdVenda = entity0.ID_VENDA
                , IdVendedor = entity0Al2.ID_VENDEDOR
                , IntVenda = entity0.INT_VENDA
                , SmallIntVenda = entity0.SMALL_INT_VENDA
                , StringVenda = entity0.STRING_VENDA
                , StringVendedor = entity0Al2.STRING_VENDEDOR
		
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
	            
                ComboboxVendaItem = entity0.COMBOBOX_VENDA_ITEM
                , ComboboxVendaItemName = ((entity0.COMBOBOX_VENDA_ITEM) == 1 ? "VENDA_ITEM1" : ((entity0.COMBOBOX_VENDA_ITEM) == 2 ? "VENDA_ITEM2" : ((entity0.COMBOBOX_VENDA_ITEM) == 3 ? "VENDA_ITEM3" : ((entity0.COMBOBOX_VENDA_ITEM) == 4 ? "VENDA_ITEM4" : ""))))
                , DatetimeVendaItem = entity0.DATETIME_VENDA_ITEM
                , DecimalVendaItem = entity0.DECIMAL_VENDA_ITEM
                , IdVenda = entity0Al1.ID_VENDA
                , IdVendaItem = entity0.ID_VENDA_ITEM
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
	    	//Add filtering disabled property for VENDA
	    	string[] bmDisabledVendaList = this.GetEDM().GetFilteringDisabledList("VENDA");
	    	if (bmDisabledVendaList.Length > 0)
	    	{
	
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
	
	    		if (bmDisabledVendaItemList.Contains("VENDA_ITEM.ID_VENDA_ITEM"))
	    		{
	    			result.Add("VendaItem|IdVendaItem");
	    			result.Add("VendaItem|VENDA_ITEM.ID_VENDA_ITEM");
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
				
	    [Ignore]
	    //Get Loja By EntitySearchId.
	    public IQueryable<Loja> GetLojaByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetLojaByEntitySearch(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get Venda By EntitySearchId.
	    public IQueryable<Venda> GetVendaByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetVendaByEntitySearch(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get VendaItem By EntitySearchId.
	    public IQueryable<VendaItem> GetVendaItemByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetVendaItemByEntitySearch(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get Loja By EntitySearchId.
	    public IQueryable<Loja> GetLojaByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetLojaByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get Venda By EntitySearchId.
	    public IQueryable<Venda> GetVendaByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetVendaByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get VendaItem By EntitySearchId.
	    public IQueryable<VendaItem> GetVendaItemByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetVendaItemByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get Loja By Example.
	    [Ignore]
	    public IQueryable<Loja> GetLojaByExample(Loja entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetLojaByEntitySearch(queryAnalysis);
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
			
	    //Get Loja By Example.
	    [Ignore]
	    public IQueryable<Loja> GetLojaByExampleNoAssociations(Loja entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetLojaByEntitySearchNoAssociations(queryAnalysis);
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

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
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
                  let entity0Al1 = entity0.CIDADE
                  let entity0Al2 = entity0.CIDADE.ESTADO
                  let entity0Al3 = entity0.CIDADE.ESTADO.PAIS
	            
	            	
	            select new Loja()		
	            {
	            
                BigIntLoja = entity0.BIG_INT_LOJA
                , BitLoja = entity0.BIT_LOJA
                , ComboboxLoja = entity0.COMBOBOX_LOJA
                , ComboboxLojaName = ((entity0.COMBOBOX_LOJA) == 1 ? "LOJA1" : ((entity0.COMBOBOX_LOJA) == 2 ? "LOJA2" : ((entity0.COMBOBOX_LOJA) == 3 ? "LOJA3" : ((entity0.COMBOBOX_LOJA) == 4 ? "LOJA4" : ""))))
                , DatetimeLoja = entity0.DATETIME_LOJA
                , DecimalLoja = entity0.DECIMAL_LOJA
                , GuidLoja = entity0.GUID_LOJA
                , IdCidade = entity0Al1.ID_CIDADE
                , IdEstado = entity0Al2.ID_ESTADO
                , IdLoja = entity0.ID_LOJA
                , IdPais = entity0Al3.ID_PAIS
                , IntLoja = entity0.INT_LOJA
                , NomeCidade = entity0Al1.NOME_CIDADE
                , SmallIntLoja = entity0.SMALL_INT_LOJA
                , StringEstado = entity0Al2.STRING_ESTADO
                , StringLoja = entity0.STRING_LOJA
                , StringPais = entity0Al3.STRING_PAIS
			
                ,VendaList = 
	                        (from entity1 in entity0.VENDA_LISTA
                                  let entity1Al1 = entity1.LOJA
                                  let entity1Al2 = entity1.VENDEDOR
	                        
	                        	
	                        select new Venda()
	                        {
	                        
                                BitVenda = entity1.BIT_VENDA
                                , ComboboxVenda = entity1.COMBOBOX_VENDA
                                , ComboboxVendaName = ((entity1.COMBOBOX_VENDA) == 1 ? "VENDA1" : ((entity1.COMBOBOX_VENDA) == 2 ? "VENDA2" : ((entity1.COMBOBOX_VENDA) == 3 ? "VENDA3" : "")))
                                , DatetimeVenda = entity1.DATETIME_VENDA
                                , DecimalVenda = entity1.DECIMAL_VENDA
                                , IdLoja = entity1Al1.ID_LOJA
                                , IdVenda = entity1.ID_VENDA
                                , IdVendedor = entity1Al2.ID_VENDEDOR
                                , IntVenda = entity1.INT_VENDA
                                , SmallIntVenda = entity1.SMALL_INT_VENDA
                                , StringVenda = entity1.STRING_VENDA
                                , StringVendedor = entity1Al2.STRING_VENDEDOR
			
                                ,VendaItemList = 
	                                                (from entity2 in entity1.VENDA_ITEM_LISTA
                                                                  let entity2Al1 = entity2.VENDA
	                                                
	                                                	
	                                                select new VendaItem()
	                                                {
	                                                
                                                                ComboboxVendaItem = entity2.COMBOBOX_VENDA_ITEM
                                                                , ComboboxVendaItemName = ((entity2.COMBOBOX_VENDA_ITEM) == 1 ? "VENDA_ITEM1" : ((entity2.COMBOBOX_VENDA_ITEM) == 2 ? "VENDA_ITEM2" : ((entity2.COMBOBOX_VENDA_ITEM) == 3 ? "VENDA_ITEM3" : ((entity2.COMBOBOX_VENDA_ITEM) == 4 ? "VENDA_ITEM4" : ""))))
                                                                , DatetimeVendaItem = entity2.DATETIME_VENDA_ITEM
                                                                , DecimalVendaItem = entity2.DECIMAL_VENDA_ITEM
                                                                , IdVenda = entity2Al1.ID_VENDA
                                                                , IdVendaItem = entity2.ID_VENDA_ITEM
                                                                , StringVendaItem = entity2.STRING_VENDA_ITEM
		
	                                                }
	                                                )
		
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
                  let entity0Al1 = entity0.LOJA
                  let entity0Al2 = entity0.VENDEDOR
	            
	            	
	            select new Venda()		
	            {
	            
                BitVenda = entity0.BIT_VENDA
                , ComboboxVenda = entity0.COMBOBOX_VENDA
                , ComboboxVendaName = ((entity0.COMBOBOX_VENDA) == 1 ? "VENDA1" : ((entity0.COMBOBOX_VENDA) == 2 ? "VENDA2" : ((entity0.COMBOBOX_VENDA) == 3 ? "VENDA3" : "")))
                , DatetimeVenda = entity0.DATETIME_VENDA
                , DecimalVenda = entity0.DECIMAL_VENDA
                , IdLoja = entity0Al1.ID_LOJA
                , IdVenda = entity0.ID_VENDA
                , IdVendedor = entity0Al2.ID_VENDEDOR
                , IntVenda = entity0.INT_VENDA
                , SmallIntVenda = entity0.SMALL_INT_VENDA
                , StringVenda = entity0.STRING_VENDA
                , StringVendedor = entity0Al2.STRING_VENDEDOR
			
                ,VendaItemList = 
	                        (from entity1 in entity0.VENDA_ITEM_LISTA
                                  let entity1Al1 = entity1.VENDA
	                        
	                        	
	                        select new VendaItem()
	                        {
	                        
                                ComboboxVendaItem = entity1.COMBOBOX_VENDA_ITEM
                                , ComboboxVendaItemName = ((entity1.COMBOBOX_VENDA_ITEM) == 1 ? "VENDA_ITEM1" : ((entity1.COMBOBOX_VENDA_ITEM) == 2 ? "VENDA_ITEM2" : ((entity1.COMBOBOX_VENDA_ITEM) == 3 ? "VENDA_ITEM3" : ((entity1.COMBOBOX_VENDA_ITEM) == 4 ? "VENDA_ITEM4" : ""))))
                                , DatetimeVendaItem = entity1.DATETIME_VENDA_ITEM
                                , DecimalVendaItem = entity1.DECIMAL_VENDA_ITEM
                                , IdVenda = entity1Al1.ID_VENDA
                                , IdVendaItem = entity1.ID_VENDA_ITEM
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
	            
                ComboboxVendaItem = entity0.COMBOBOX_VENDA_ITEM
                , ComboboxVendaItemName = ((entity0.COMBOBOX_VENDA_ITEM) == 1 ? "VENDA_ITEM1" : ((entity0.COMBOBOX_VENDA_ITEM) == 2 ? "VENDA_ITEM2" : ((entity0.COMBOBOX_VENDA_ITEM) == 3 ? "VENDA_ITEM3" : ((entity0.COMBOBOX_VENDA_ITEM) == 4 ? "VENDA_ITEM4" : ""))))
                , DatetimeVendaItem = entity0.DATETIME_VENDA_ITEM
                , DecimalVendaItem = entity0.DECIMAL_VENDA_ITEM
                , IdVenda = entity0Al1.ID_VENDA
                , IdVendaItem = entity0.ID_VENDA_ITEM
                , StringVendaItem = entity0.STRING_VENDA_ITEM
		
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
                  let entity0Al1 = entity0.CIDADE
                  let entity0Al2 = entity0.CIDADE.ESTADO
                  let entity0Al3 = entity0.CIDADE.ESTADO.PAIS
	            
	            	
	            select new Loja()		
	            {
	            
                BigIntLoja = entity0.BIG_INT_LOJA
                , BitLoja = entity0.BIT_LOJA
                , ComboboxLoja = entity0.COMBOBOX_LOJA
                , ComboboxLojaName = ((entity0.COMBOBOX_LOJA) == 1 ? "LOJA1" : ((entity0.COMBOBOX_LOJA) == 2 ? "LOJA2" : ((entity0.COMBOBOX_LOJA) == 3 ? "LOJA3" : ((entity0.COMBOBOX_LOJA) == 4 ? "LOJA4" : ""))))
                , DatetimeLoja = entity0.DATETIME_LOJA
                , DecimalLoja = entity0.DECIMAL_LOJA
                , GuidLoja = entity0.GUID_LOJA
                , IdCidade = entity0Al1.ID_CIDADE
                , IdEstado = entity0Al2.ID_ESTADO
                , IdLoja = entity0.ID_LOJA
                , IdPais = entity0Al3.ID_PAIS
                , IntLoja = entity0.INT_LOJA
                , NomeCidade = entity0Al1.NOME_CIDADE
                , SmallIntLoja = entity0.SMALL_INT_LOJA
                , StringEstado = entity0Al2.STRING_ESTADO
                , StringLoja = entity0.STRING_LOJA
                , StringPais = entity0Al3.STRING_PAIS
		
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
                  let entity0Al1 = entity0.LOJA
                  let entity0Al2 = entity0.VENDEDOR
	            
	            	
	            select new Venda()		
	            {
	            
                BitVenda = entity0.BIT_VENDA
                , ComboboxVenda = entity0.COMBOBOX_VENDA
                , ComboboxVendaName = ((entity0.COMBOBOX_VENDA) == 1 ? "VENDA1" : ((entity0.COMBOBOX_VENDA) == 2 ? "VENDA2" : ((entity0.COMBOBOX_VENDA) == 3 ? "VENDA3" : "")))
                , DatetimeVenda = entity0.DATETIME_VENDA
                , DecimalVenda = entity0.DECIMAL_VENDA
                , IdLoja = entity0Al1.ID_LOJA
                , IdVenda = entity0.ID_VENDA
                , IdVendedor = entity0Al2.ID_VENDEDOR
                , IntVenda = entity0.INT_VENDA
                , SmallIntVenda = entity0.SMALL_INT_VENDA
                , StringVenda = entity0.STRING_VENDA
                , StringVendedor = entity0Al2.STRING_VENDEDOR
		
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
	            
                ComboboxVendaItem = entity0.COMBOBOX_VENDA_ITEM
                , ComboboxVendaItemName = ((entity0.COMBOBOX_VENDA_ITEM) == 1 ? "VENDA_ITEM1" : ((entity0.COMBOBOX_VENDA_ITEM) == 2 ? "VENDA_ITEM2" : ((entity0.COMBOBOX_VENDA_ITEM) == 3 ? "VENDA_ITEM3" : ((entity0.COMBOBOX_VENDA_ITEM) == 4 ? "VENDA_ITEM4" : ""))))
                , DatetimeVendaItem = entity0.DATETIME_VENDA_ITEM
                , DecimalVendaItem = entity0.DECIMAL_VENDA_ITEM
                , IdVenda = entity0Al1.ID_VENDA
                , IdVendaItem = entity0.ID_VENDA_ITEM
                , StringVendaItem = entity0.STRING_VENDA_ITEM
		
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "LOJA", "VENDA", "LOJA", typeof(VendaParentComposition));
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
                  let entity0Al1 = entity0.LOJA
                  let entity0Al2 = entity0.VENDEDOR
	            
	            	
	            select new VendaParentComposition()		
	            {
	            
                BitVenda = entity0.BIT_VENDA
                , ComboboxVenda = entity0.COMBOBOX_VENDA
                , ComboboxVendaName = ((entity0.COMBOBOX_VENDA) == 1 ? "VENDA1" : ((entity0.COMBOBOX_VENDA) == 2 ? "VENDA2" : ((entity0.COMBOBOX_VENDA) == 3 ? "VENDA3" : "")))
                , DatetimeVenda = entity0.DATETIME_VENDA
                , DecimalVenda = entity0.DECIMAL_VENDA
                , IdLoja = entity0Al1.ID_LOJA
                , IdVenda = entity0.ID_VENDA
                , IdVendedor = entity0Al2.ID_VENDEDOR
                , IntVenda = entity0.INT_VENDA
                , SmallIntVenda = entity0.SMALL_INT_VENDA
                , StringVenda = entity0.STRING_VENDA
                , StringVendedor = entity0Al2.STRING_VENDEDOR
                //Loja Properties.
                , BigIntLoja = entity0.LOJA.BIG_INT_LOJA
                , BitLoja = entity0.LOJA.BIT_LOJA
                , ComboboxLoja = entity0.LOJA.COMBOBOX_LOJA
                , ComboboxLojaName = ((entity0.LOJA.COMBOBOX_LOJA) == 1 ? "LOJA1" : ((entity0.LOJA.COMBOBOX_LOJA) == 2 ? "LOJA2" : ((entity0.LOJA.COMBOBOX_LOJA) == 3 ? "LOJA3" : ((entity0.LOJA.COMBOBOX_LOJA) == 4 ? "LOJA4" : ""))))
                , DatetimeLoja = entity0.LOJA.DATETIME_LOJA
                , DecimalLoja = entity0.LOJA.DECIMAL_LOJA
                , GuidLoja = entity0.LOJA.GUID_LOJA
                , IdCidade = entity0.LOJA.CIDADE.ID_CIDADE
                , IdEstado = entity0.LOJA.CIDADE.ESTADO.ID_ESTADO
                , IdPais = entity0.LOJA.CIDADE.ESTADO.PAIS.ID_PAIS
                , IntLoja = entity0.LOJA.INT_LOJA
                , NomeCidade = entity0.LOJA.CIDADE.NOME_CIDADE
                , SmallIntLoja = entity0.LOJA.SMALL_INT_LOJA
                , StringEstado = entity0.LOJA.CIDADE.ESTADO.STRING_ESTADO
                , StringLoja = entity0.LOJA.STRING_LOJA
                , StringPais = entity0.LOJA.CIDADE.ESTADO.PAIS.STRING_PAIS
		
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "LOJA", "VENDA_ITEM", "VENDA.LOJA", typeof(VendaItemParentComposition));
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
	            
                ComboboxVendaItem = entity0.COMBOBOX_VENDA_ITEM
                , ComboboxVendaItemName = ((entity0.COMBOBOX_VENDA_ITEM) == 1 ? "VENDA_ITEM1" : ((entity0.COMBOBOX_VENDA_ITEM) == 2 ? "VENDA_ITEM2" : ((entity0.COMBOBOX_VENDA_ITEM) == 3 ? "VENDA_ITEM3" : ((entity0.COMBOBOX_VENDA_ITEM) == 4 ? "VENDA_ITEM4" : ""))))
                , DatetimeVendaItem = entity0.DATETIME_VENDA_ITEM
                , DecimalVendaItem = entity0.DECIMAL_VENDA_ITEM
                , IdVenda = entity0Al1.ID_VENDA
                , IdVendaItem = entity0.ID_VENDA_ITEM
                , StringVendaItem = entity0.STRING_VENDA_ITEM
                //Venda Properties.
                , BitVenda = entity0.VENDA.BIT_VENDA
                , ComboboxVenda = entity0.VENDA.COMBOBOX_VENDA
                , ComboboxVendaName = ((entity0.VENDA.COMBOBOX_VENDA) == 1 ? "VENDA1" : ((entity0.VENDA.COMBOBOX_VENDA) == 2 ? "VENDA2" : ((entity0.VENDA.COMBOBOX_VENDA) == 3 ? "VENDA3" : "")))
                , DatetimeVenda = entity0.VENDA.DATETIME_VENDA
                , DecimalVenda = entity0.VENDA.DECIMAL_VENDA
                , IdLoja = entity0.VENDA.LOJA.ID_LOJA
                , IdVendedor = entity0.VENDA.VENDEDOR.ID_VENDEDOR
                , IntVenda = entity0.VENDA.INT_VENDA
                , SmallIntVenda = entity0.VENDA.SMALL_INT_VENDA
                , StringVenda = entity0.VENDA.STRING_VENDA
                , StringVendedor = entity0.VENDA.VENDEDOR.STRING_VENDEDOR
                //Loja Properties.
                , BigIntLoja = entity0.VENDA.LOJA.BIG_INT_LOJA
                , BitLoja = entity0.VENDA.LOJA.BIT_LOJA
                , ComboboxLoja = entity0.VENDA.LOJA.COMBOBOX_LOJA
                , ComboboxLojaName = ((entity0.VENDA.LOJA.COMBOBOX_LOJA) == 1 ? "LOJA1" : ((entity0.VENDA.LOJA.COMBOBOX_LOJA) == 2 ? "LOJA2" : ((entity0.VENDA.LOJA.COMBOBOX_LOJA) == 3 ? "LOJA3" : ((entity0.VENDA.LOJA.COMBOBOX_LOJA) == 4 ? "LOJA4" : ""))))
                , DatetimeLoja = entity0.VENDA.LOJA.DATETIME_LOJA
                , DecimalLoja = entity0.VENDA.LOJA.DECIMAL_LOJA
                , GuidLoja = entity0.VENDA.LOJA.GUID_LOJA
                , IdCidade = entity0.VENDA.LOJA.CIDADE.ID_CIDADE
                , IdEstado = entity0.VENDA.LOJA.CIDADE.ESTADO.ID_ESTADO
                , IdPais = entity0.VENDA.LOJA.CIDADE.ESTADO.PAIS.ID_PAIS
                , IntLoja = entity0.VENDA.LOJA.INT_LOJA
                , NomeCidade = entity0.VENDA.LOJA.CIDADE.NOME_CIDADE
                , SmallIntLoja = entity0.VENDA.LOJA.SMALL_INT_LOJA
                , StringEstado = entity0.VENDA.LOJA.CIDADE.ESTADO.STRING_ESTADO
                , StringLoja = entity0.VENDA.LOJA.STRING_LOJA
                , StringPais = entity0.VENDA.LOJA.CIDADE.ESTADO.PAIS.STRING_PAIS
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
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
                  let entity0Al1 = entity0.CIDADE
                  let entity0Al2 = entity0.CIDADE.ESTADO
                  let entity0Al3 = entity0.CIDADE.ESTADO.PAIS
                orderby entity0.ID_LOJA ascending
	            
	            	
	            select new Loja()		
	            {
	            
                BigIntLoja = entity0.BIG_INT_LOJA
                , BitLoja = entity0.BIT_LOJA
                , ComboboxLoja = entity0.COMBOBOX_LOJA
                , ComboboxLojaName = ((entity0.COMBOBOX_LOJA) == 1 ? "LOJA1" : ((entity0.COMBOBOX_LOJA) == 2 ? "LOJA2" : ((entity0.COMBOBOX_LOJA) == 3 ? "LOJA3" : ((entity0.COMBOBOX_LOJA) == 4 ? "LOJA4" : ""))))
                , DatetimeLoja = entity0.DATETIME_LOJA
                , DecimalLoja = entity0.DECIMAL_LOJA
                , GuidLoja = entity0.GUID_LOJA
                , IdCidade = entity0Al1.ID_CIDADE
                , IdEstado = entity0Al2.ID_ESTADO
                , IdLoja = entity0.ID_LOJA
                , IdPais = entity0Al3.ID_PAIS
                , IntLoja = entity0.INT_LOJA
                , NomeCidade = entity0Al1.NOME_CIDADE
                , SmallIntLoja = entity0.SMALL_INT_LOJA
                , StringEstado = entity0Al2.STRING_ESTADO
                , StringLoja = entity0.STRING_LOJA
                , StringPais = entity0Al3.STRING_PAIS
		
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
                  let entity0Al1 = entity0.LOJA
                  let entity0Al2 = entity0.VENDEDOR
                orderby entity0.ID_VENDA ascending
	            
	            	
	            select new Venda()		
	            {
	            
                BitVenda = entity0.BIT_VENDA
                , ComboboxVenda = entity0.COMBOBOX_VENDA
                , ComboboxVendaName = ((entity0.COMBOBOX_VENDA) == 1 ? "VENDA1" : ((entity0.COMBOBOX_VENDA) == 2 ? "VENDA2" : ((entity0.COMBOBOX_VENDA) == 3 ? "VENDA3" : "")))
                , DatetimeVenda = entity0.DATETIME_VENDA
                , DecimalVenda = entity0.DECIMAL_VENDA
                , IdLoja = entity0Al1.ID_LOJA
                , IdVenda = entity0.ID_VENDA
                , IdVendedor = entity0Al2.ID_VENDEDOR
                , IntVenda = entity0.INT_VENDA
                , SmallIntVenda = entity0.SMALL_INT_VENDA
                , StringVenda = entity0.STRING_VENDA
                , StringVendedor = entity0Al2.STRING_VENDEDOR
		
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
	            
                ComboboxVendaItem = entity0.COMBOBOX_VENDA_ITEM
                , ComboboxVendaItemName = ((entity0.COMBOBOX_VENDA_ITEM) == 1 ? "VENDA_ITEM1" : ((entity0.COMBOBOX_VENDA_ITEM) == 2 ? "VENDA_ITEM2" : ((entity0.COMBOBOX_VENDA_ITEM) == 3 ? "VENDA_ITEM3" : ((entity0.COMBOBOX_VENDA_ITEM) == 4 ? "VENDA_ITEM4" : ""))))
                , DatetimeVendaItem = entity0.DATETIME_VENDA_ITEM
                , DecimalVendaItem = entity0.DECIMAL_VENDA_ITEM
                , IdVenda = entity0Al1.ID_VENDA
                , IdVendaItem = entity0.ID_VENDA_ITEM
                , StringVendaItem = entity0.STRING_VENDA_ITEM
		
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
                  let entityAl1 = entity.CIDADE
                  let entityAl2 = entity.CIDADE.ESTADO
                  let entityAl3 = entity.CIDADE.ESTADO.PAIS
	            
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
                  let entityAl1 = entity.LOJA
                  let entityAl2 = entity.VENDEDOR
	            
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

	
	        if (entity.Loja.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.Loja) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.Loja); 	
	            

	
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

	
	        if (entity.Loja.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.Loja) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.Loja);
	            

	
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

	
	        if (entity.Loja.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.Loja) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.Loja);
	            

	
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
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}