					
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

namespace LinxTraining001.BV.DetalhamentoVenda
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="Vendas.ID_Vendas", IsUpdatable=false, EdmName="LinxTraining002.BM.ModeloVendaCliente")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[VendasView,VendasView.VendaDetalheView];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IDVendas];ReadOnly[false];Entities[Vendas:IDVendas|Clientes:IDClientes];SubQueryInfo[];EdmEntityName[Vendas];EntityRelations[Clientes(Clientes)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "VendasView")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "LinxTraining001.BV.DetalhamentoVenda.VendasView")]
	public partial class VendasView : Linx.Data.Entity
	{

	
		
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.VendaDetalheViewList != null && this.VendaDetalheViewList.Count() > 0)
	      {
	         foreach (var entity in this.VendaDetalheViewList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.VendaDetalheViewList != null)
	      {
	         foreach (var detail in this.VendaDetalheViewList)
	         {
	            detail.ResetDetails();
	         }
	         this.VendaDetalheViewList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(DetalhamentoVendaDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("VendaDetalheView"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("VendaDetalheView");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IDVendas"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IDVendas));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load VendaDetalheView and all sub-details
	         if (this.VendaDetalheViewList == null || this.VendaDetalheViewList.Count() == 0)
	         {
	             if (take > 0)
	                 this.VendaDetalheViewList = context.GetPagedVendaDetalheView(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.VendaDetalheViewList = (from r in context.GetVendaDetalheViewByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _VendaDetalheViewElements = changeSet.ChangeSetEntries.Where(e => e.Entity is VendaDetalheView && ((VendaDetalheView)e.Entity).VendasView == null && e.Associations == null && e.OriginalAssociations == null && ((VendaDetalheView)e.Entity).IDVendas == this.IDVendas).ToList();
 	      if (_VendaDetalheViewElements.Count > 0 && this.VendaDetalheViewList.Count() == 0)
 	      {
 	          this.VendaDetalheViewList = _VendaDetalheViewElements.Select(e => (VendaDetalheView)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _VendaDetalheViewElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((VendaDetalheView)detail.Entity).VendasView = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("VendasView", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("VendaDetalheViewList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For Data
	    partial void OnDataChanging(System.DateTime value);
	    partial void OnDataChanged();

	    private System.DateTime _Data;

	    [DataMember(IsRequired = true, Name = "Data", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Data", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[Vendas.Data];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Vendas.Data")]
	    public System.DateTime Data
	    {
	    	    get
	    	    {
	    	          return _Data;
	    	    }
	    	    set
	    	    {
	    	          if (this._Data != value)
	    	          {
	    	              this.ValidateProperty("Data", value);
	    	              this.OnDataChanging(value);
	    	              this.RaiseDataMemberChanging("Data");
	    	              this._Data = value;
	    	              this.RaiseDataMemberChanged("Data");
	    	              this.OnDataChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IDClientes
	    partial void OnIDClientesChanging(System.Guid value);
	    partial void OnIDClientesChanged();

	    private System.Guid _IDClientes;

	    [DataMember(IsRequired = true, Name = "IDClientes", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Clientes", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpClientes];LookUpTitle[Seleção de (ID Clientes)];LookUpQuery[executeLookUpClientes];LookUpFinalize[finalizeLookUpClientes];LookUpDisplayColumns[{\"IDClientes\" : \"ID Clientes\", \"Nome\" : \"Nome\"}];LookUpColumns[{\"IDClientes\" : true, \"Nome\" : true}];FilterDataKey[Vendas.Clientes.ID_Clientes];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Guid#IDClientes#true##36:0##ID Clientes#0#true##::LookUpClientes##false#true#Clientes#Clientes#LinxTraining001.BV.DetalhamentoVenda#IQueryable###true#false", EdmKey="Vendas.Clientes.ID_Clientes")]
	    public System.Guid IDClientes
	    {
	    	    get
	    	    {
	    	          return _IDClientes;
	    	    }
	    	    set
	    	    {
	    	          if (this._IDClientes != value)
	    	          {
	    	              this.ValidateProperty("IDClientes", value);
	    	              this.OnIDClientesChanging(value);
	    	              this.RaiseDataMemberChanging("IDClientes");
	    	              this._IDClientes = value;
	    	              this.RaiseDataMemberChanged("IDClientes");
	    	              this.OnIDClientesChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IDVendas
	    partial void OnIDVendasChanging(Int32 value);
	    partial void OnIDVendasChanged();

	    private Int32 _IDVendas;

	    [DataMember(IsRequired = true, Name = "IDVendas", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "GOSTOSAOPai", Description="", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[Vendas.ID_Vendas];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Vendas.ID_Vendas")]
	    public Int32 IDVendas
	    {
	    	    get
	    	    {
	    	          return _IDVendas;
	    	    }
	    	    set
	    	    {
	    	          if (this._IDVendas != value)
	    	          {
	    	              this.ValidateProperty("IDVendas", value);
	    	              this.OnIDVendasChanging(value);
	    	              this.RaiseDataMemberChanging("IDVendas");
	    	              this._IDVendas = value;
	    	              this.RaiseDataMemberChanged("IDVendas");
	    	              this.OnIDVendasChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Nome
	    partial void OnNomeChanging(System.String value);
	    partial void OnNomeChanged();

	    private System.String _Nome;

	    [DataMember(Name = "Nome", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome2", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpClientes];LookUpTitle[Seleção de (Nome2)];LookUpQuery[executeLookUpClientes];LookUpFinalize[finalizeLookUpClientes];LookUpDisplayColumns[{\"IDClientes\" : \"ID Clientes\", \"Nome\" : \"Nome\"}];LookUpColumns[{\"IDClientes\" : true, \"Nome\" : true}];FilterDataKey[Vendas.Clientes.Nome];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#Nome#false##40:0##Nome#1#true##::LookUpClientes##false#true#Clientes#Clientes#LinxTraining001.BV.DetalhamentoVenda#IQueryable###true#false", EdmKey="Vendas.Clientes.Nome")]
	    public System.String Nome
	    {
	    	    get
	    	    {
	    	          return _Nome;
	    	    }
	    	    set
	    	    {
	    	          if (this._Nome != value)
	    	          {
	    	              this.ValidateProperty("Nome", value);
	    	              this.OnNomeChanging(value);
	    	              this.RaiseDataMemberChanging("Nome");
	    	              this._Nome = value;
	    	              this.RaiseDataMemberChanged("Nome");
	    	              this.OnNomeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Origem
	    partial void OnOrigemChanging(System.Nullable<System.Int32> value);
	    partial void OnOrigemChanged();

	    private System.Nullable<System.Int32> _Origem;

	    [DataMember(Name = "Origem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Origem", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[LXOrigem];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[Vendas.Origem];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Vendas.Origem")]
	    public System.Nullable<System.Int32> Origem
	    {
	    	    get
	    	    {
	    	          return _Origem;
	    	    }
	    	    set
	    	    {
	    	          if (this._Origem != value)
	    	          {
	    	              this.ValidateProperty("Origem", value);
	    	              this.OnOrigemChanging(value);
	    	              this.RaiseDataMemberChanging("Origem");
	    	              this._Origem = value;
	    	              this.RaiseDataMemberChanged("Origem");
	    	              this.OnOrigemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ValorTotal
	    partial void OnValorTotalChanging(System.Nullable<System.Decimal> value);
	    partial void OnValorTotalChanged();

	    private System.Nullable<System.Decimal> _ValorTotal;

	    [DataMember(Name = "ValorTotal", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ValorTotal", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[Vendas.ValorTotal];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Vendas.ValorTotal")]
	    public System.Nullable<System.Decimal> ValorTotal
	    {
	    	    get
	    	    {
	    	          return _ValorTotal;
	    	    }
	    	    set
	    	    {
	    	          if (this._ValorTotal != value)
	    	          {
	    	              this.ValidateProperty("ValorTotal", value);
	    	              this.OnValorTotalChanging(value);
	    	              this.RaiseDataMemberChanging("ValorTotal");
	    	              this._ValorTotal = value;
	    	              this.RaiseDataMemberChanged("ValorTotal");
	    	              this.OnValorTotalChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For VendaVip
	    partial void OnVendaVipChanging(System.Nullable<System.Boolean> value);
	    partial void OnVendaVipChanged();

	    private System.Nullable<System.Boolean> _VendaVip;

	    [DataMember(Name = "VendaVip", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "VendaVip", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[Vendas.VendaVip];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Vendas.VendaVip")]
	    public System.Nullable<System.Boolean> VendaVip
	    {
	    	    get
	    	    {
	    	          return _VendaVip;
	    	    }
	    	    set
	    	    {
	    	          if (this._VendaVip != value)
	    	          {
	    	              this.ValidateProperty("VendaVip", value);
	    	              this.OnVendaVipChanging(value);
	    	              this.RaiseDataMemberChanging("VendaVip");
	    	              this._VendaVip = value;
	    	              this.RaiseDataMemberChanged("VendaVip");
	    	              this.OnVendaVipChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIDVendas;
	    [DataMember(Name = "TemporaryIDVendas", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "GOSTOSAOPai (Tmp)", Description="Temporary Key", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIDVendas
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIDVendas.IsNullOrEmpty())
	    	                this._TemporaryIDVendas = this._IDVendas;
	    	          return this._TemporaryIDVendas;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIDVendas != value)
	    	              this._TemporaryIDVendas = value;
	    	    }
	    }	

	    #endregion Data Properties

	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<VendaDetalheView> _VendaDetalheViewList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_VendasView_VendaDetalheView", "IDVendas", "IDVendas", IsForeignKey=false)]
	    [DataMember(Name = "VendaDetalheViewList", EmitDefaultValue = true)]
	    public IEnumerable<VendaDetalheView> VendaDetalheViewList
	    {
	        get
	        {
	
	            if (this._VendaDetalheViewList == null)
	            	this._VendaDetalheViewList = new List<VendaDetalheView>();
	
	            return this._VendaDetalheViewList;
	        }
	        set
	        {
	            if (this._VendaDetalheViewList != value)
	            {
	                this._VendaDetalheViewList = value;
	                this.RaisePropertyChanged("VendaDetalheViewList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ModeloVendaCliente.Vendas").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(LinxTraining002.BM.Vendas), QualifiedEntitySetName = "ModeloVendaCliente.Vendas" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Vendas.Data", Source = "Data", Target = "Data", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.Vendas", RelationPropertyName = "Vendas" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Vendas.Origem", Source = "Origem", Target = "Origem", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.Vendas", RelationPropertyName = "Vendas" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Vendas.VendaVip", Source = "VendaVip", Target = "VendaVip", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.Vendas", RelationPropertyName = "Vendas" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Vendas.ID_Vendas", Source = "IDVendas", Target = "ID_Vendas", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.Vendas", RelationPropertyName = "Vendas" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Vendas.ValorTotal", Source = "ValorTotal", Target = "ValorTotal", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.Vendas", RelationPropertyName = "Vendas" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Vendas.Clientes.ID_Clientes", Source = "IDClientes", Target = "ID_Clientes", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ModeloVendaCliente.Clientes", RelationPropertyName = "Clientes" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetOrigemValues()
	    {
	    	    return LinxTraining001.BV.Domains.LXOrigem.GetValues();
	    }
	    private string _origemName;
	    [DataMember(IsRequired = false, Name = "OrigemName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Origem", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string OrigemName
	    {
	    	    get { if (this.Origem.IsNullOrEmpty()) { _origemName = String.Empty; } else { string key = this.Origem.ToString(); var dmValues = this.GetOrigemValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _origemName) _origemName = domainName; } return _origemName; } set { _origemName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="VendaDetalhe.ID_VendaDetalhe", IsUpdatable=false, EdmName="LinxTraining002.BM.ModeloVendaCliente")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[VendaDetalheView];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IDVendaDetalhe];ReadOnly[false];Entities[VendaDetalhe:IDVendaDetalhe];SubQueryInfo[Select 1 From #ParentAlias#.VendaDetalhe_LISTA as #Alias#];EdmEntityName[VendaDetalhe];EntityRelations[Vendas(Vendas)#Clientes(Clientes)];EdmParentEntityName[Vendas];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "VendaDetalheView")]
	[Serializable()]
	public partial class VendaDetalheView : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(DetalhamentoVendaDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("VendasView");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IDVendas"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IDVendas));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load VendasView
	         this.VendasView = (from r in context.GetVendasViewByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For Hora
	    partial void OnHoraChanging(System.Nullable<System.DateTime> value);
	    partial void OnHoraChanged();

	    private System.Nullable<System.DateTime> _Hora;

	    [DataMember(Name = "Hora", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Hora", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VendaDetalhe.Hora];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VendaDetalhe.Hora")]
	    public System.Nullable<System.DateTime> Hora
	    {
	    	    get
	    	    {
	    	          return _Hora;
	    	    }
	    	    set
	    	    {
	    	          if (this._Hora != value)
	    	          {
	    	              this.ValidateProperty("Hora", value);
	    	              this.OnHoraChanging(value);
	    	              this.RaiseDataMemberChanging("Hora");
	    	              this._Hora = value;
	    	              this.RaiseDataMemberChanged("Hora");
	    	              this.OnHoraChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IDVendaDetalhe
	    partial void OnIDVendaDetalheChanging(Int32 value);
	    partial void OnIDVendaDetalheChanged();

	    private Int32 _IDVendaDetalhe;

	    [DataMember(IsRequired = true, Name = "IDVendaDetalhe", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID VendaDetalhe", Description="", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VendaDetalhe.ID_VendaDetalhe];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VendaDetalhe.ID_VendaDetalhe")]
	    public Int32 IDVendaDetalhe
	    {
	    	    get
	    	    {
	    	          return _IDVendaDetalhe;
	    	    }
	    	    set
	    	    {
	    	          if (this._IDVendaDetalhe != value)
	    	          {
	    	              this.ValidateProperty("IDVendaDetalhe", value);
	    	              this.OnIDVendaDetalheChanging(value);
	    	              this.RaiseDataMemberChanging("IDVendaDetalhe");
	    	              this._IDVendaDetalhe = value;
	    	              this.RaiseDataMemberChanged("IDVendaDetalhe");
	    	              this.OnIDVendaDetalheChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IDVendas
	    partial void OnIDVendasChanging(Int32 value);
	    partial void OnIDVendasChanged();

	    private Int32 _IDVendas;

	    [DataMember(IsRequired = true, Name = "IDVendas", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "GOSTOSAOFIlho", Description="", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VendaDetalhe.Vendas.ID_Vendas];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VendaDetalhe.Vendas.ID_Vendas")]
	    public Int32 IDVendas
	    {
	    	    get
	    	    {
	    	          return _IDVendas;
	    	    }
	    	    set
	    	    {
	    	          if (this._IDVendas != value)
	    	          {
	    	              this.ValidateProperty("IDVendas", value);
	    	              this.OnIDVendasChanging(value);
	    	              this.RaiseDataMemberChanging("IDVendas");
	    	              this._IDVendas = value;
	    	              this.RaiseDataMemberChanged("IDVendas");
	    	              this.OnIDVendasChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Preco
	    partial void OnPrecoChanging(System.Nullable<System.Decimal> value);
	    partial void OnPrecoChanged();

	    private System.Nullable<System.Decimal> _Preco;

	    [DataMember(Name = "Preco", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Preço", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VendaDetalhe.Preco];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VendaDetalhe.Preco")]
	    public System.Nullable<System.Decimal> Preco
	    {
	    	    get
	    	    {
	    	          return _Preco;
	    	    }
	    	    set
	    	    {
	    	          if (this._Preco != value)
	    	          {
	    	              this.ValidateProperty("Preco", value);
	    	              this.OnPrecoChanging(value);
	    	              this.RaiseDataMemberChanging("Preco");
	    	              this._Preco = value;
	    	              this.RaiseDataMemberChanged("Preco");
	    	              this.OnPrecoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Produto
	    partial void OnProdutoChanging(System.String value);
	    partial void OnProdutoChanged();

	    private System.String _Produto;

	    [DataMember(Name = "Produto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Produto", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[ProdutoDomain];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VendaDetalhe.Produto];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VendaDetalhe.Produto")]
	    public System.String Produto
	    {
	    	    get
	    	    {
	    	          return _Produto;
	    	    }
	    	    set
	    	    {
	    	          if (this._Produto != value)
	    	          {
	    	              this.ValidateProperty("Produto", value);
	    	              this.OnProdutoChanging(value);
	    	              this.RaiseDataMemberChanging("Produto");
	    	              this._Produto = value;
	    	              this.RaiseDataMemberChanged("Produto");
	    	              this.OnProdutoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Quantidade
	    partial void OnQuantidadeChanging(System.Nullable<System.Int32> value);
	    partial void OnQuantidadeChanged();

	    private System.Nullable<System.Int32> _Quantidade;

	    [DataMember(Name = "Quantidade", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Quantidade", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VendaDetalhe.Quantidade];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VendaDetalhe.Quantidade")]
	    public System.Nullable<System.Int32> Quantidade
	    {
	    	    get
	    	    {
	    	          return _Quantidade;
	    	    }
	    	    set
	    	    {
	    	          if (this._Quantidade != value)
	    	          {
	    	              this.ValidateProperty("Quantidade", value);
	    	              this.OnQuantidadeChanging(value);
	    	              this.RaiseDataMemberChanging("Quantidade");
	    	              this._Quantidade = value;
	    	              this.RaiseDataMemberChanged("Quantidade");
	    	              this.OnQuantidadeChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIDVendaDetalhe;
	    [DataMember(Name = "TemporaryIDVendaDetalhe", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID VendaDetalhe (Tmp)", Description="Temporary Key", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIDVendaDetalhe
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIDVendaDetalhe.IsNullOrEmpty())
	    	                this._TemporaryIDVendaDetalhe = this._IDVendaDetalhe;
	    	          return this._TemporaryIDVendaDetalhe;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIDVendaDetalhe != value)
	    	              this._TemporaryIDVendaDetalhe = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private VendasView _VendasView;
	    [DataMember(Name = "VendasView", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_VendasView_VendaDetalheView", "IDVendas", "IDVendas", IsForeignKey=true)]
	    public VendasView VendasView
	    {
	        get
	        {
	            return this._VendasView;
	        }
	        set
	        {
	            if (this._VendasView != value)
	            {
	                this._VendasView = value;
	                this.RaisePropertyChanged("VendasViewList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ModeloVendaCliente.VendaDetalhe").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(LinxTraining002.BM.VendaDetalhe), QualifiedEntitySetName = "ModeloVendaCliente.VendaDetalhe" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VendaDetalhe.Hora", Source = "Hora", Target = "Hora", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.VendaDetalhe", RelationPropertyName = "VendaDetalhe" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VendaDetalhe.Preco", Source = "Preco", Target = "Preco", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.VendaDetalhe", RelationPropertyName = "VendaDetalhe" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VendaDetalhe.Produto", Source = "Produto", Target = "Produto", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.VendaDetalhe", RelationPropertyName = "VendaDetalhe" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VendaDetalhe.Quantidade", Source = "Quantidade", Target = "Quantidade", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.VendaDetalhe", RelationPropertyName = "VendaDetalhe" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VendaDetalhe.ID_VendaDetalhe", Source = "IDVendaDetalhe", Target = "ID_VendaDetalhe", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.VendaDetalhe", RelationPropertyName = "VendaDetalhe" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VendaDetalhe.Vendas.ID_Vendas", Source = "IDVendas", Target = "ID_Vendas", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ModeloVendaCliente.Vendas", RelationPropertyName = "Vendas" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetProdutoValues()
	    {
	    	    return LinxTraining001.BV.Domains.ProdutoDomain.GetValues();
	    }
	    private string _produtoName;
	    [DataMember(IsRequired = false, Name = "ProdutoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Produto", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ProdutoName
	    {
	    	    get { if (this.Produto.IsNullOrEmpty()) { _produtoName = String.Empty; } else { string key = this.Produto.ToString(); var dmValues = this.GetProdutoValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _produtoName) _produtoName = domainName; } return _produtoName; } set { _produtoName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[VendaDetalheView];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IDVendaDetalhe];ReadOnly[false];Entities[VendaDetalhe:IDVendaDetalhe];SubQueryInfo[Select 1 From #ParentAlias#.VendaDetalhe_LISTA as #Alias#];EdmEntityName[VendaDetalhe];EntityRelations[Vendas(Vendas)#Clientes(Clientes)];EdmParentEntityName[Vendas];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "VendaDetalheView")]
	[Serializable()]
	public partial class VendaDetalheViewParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For Hora
	    partial void OnHoraChanging(System.Nullable<System.DateTime> value);
	    partial void OnHoraChanged();

	    private System.Nullable<System.DateTime> _Hora;

	    [DataMember(Name = "Hora", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Hora", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VendaDetalhe.Hora];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VendaDetalhe.Hora")]
	    public System.Nullable<System.DateTime> Hora
	    {
	    	    get
	    	    {
	    	          return _Hora;
	    	    }
	    	    set
	    	    {
	    	          if (this._Hora != value)
	    	          {
	    	              this.ValidateProperty("Hora", value);
	    	              this.OnHoraChanging(value);
	    	              this.RaiseDataMemberChanging("Hora");
	    	              this._Hora = value;
	    	              this.RaiseDataMemberChanged("Hora");
	    	              this.OnHoraChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IDVendaDetalhe
	    partial void OnIDVendaDetalheChanging(Int32 value);
	    partial void OnIDVendaDetalheChanged();

	    private Int32 _IDVendaDetalhe;

	    [DataMember(IsRequired = true, Name = "IDVendaDetalhe", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID VendaDetalhe", Description="", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VendaDetalhe.ID_VendaDetalhe];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VendaDetalhe.ID_VendaDetalhe")]
	    public Int32 IDVendaDetalhe
	    {
	    	    get
	    	    {
	    	          return _IDVendaDetalhe;
	    	    }
	    	    set
	    	    {
	    	          if (this._IDVendaDetalhe != value)
	    	          {
	    	              this.ValidateProperty("IDVendaDetalhe", value);
	    	              this.OnIDVendaDetalheChanging(value);
	    	              this.RaiseDataMemberChanging("IDVendaDetalhe");
	    	              this._IDVendaDetalhe = value;
	    	              this.RaiseDataMemberChanged("IDVendaDetalhe");
	    	              this.OnIDVendaDetalheChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IDVendas
	    partial void OnIDVendasChanging(Int32 value);
	    partial void OnIDVendasChanged();

	    private Int32 _IDVendas;

	    [DataMember(IsRequired = true, Name = "IDVendas", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "GOSTOSAOFIlho", Description="", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VendaDetalhe.Vendas.ID_Vendas];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VendaDetalhe.Vendas.ID_Vendas")]
	    public Int32 IDVendas
	    {
	    	    get
	    	    {
	    	          return _IDVendas;
	    	    }
	    	    set
	    	    {
	    	          if (this._IDVendas != value)
	    	          {
	    	              this.ValidateProperty("IDVendas", value);
	    	              this.OnIDVendasChanging(value);
	    	              this.RaiseDataMemberChanging("IDVendas");
	    	              this._IDVendas = value;
	    	              this.RaiseDataMemberChanged("IDVendas");
	    	              this.OnIDVendasChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Preco
	    partial void OnPrecoChanging(System.Nullable<System.Decimal> value);
	    partial void OnPrecoChanged();

	    private System.Nullable<System.Decimal> _Preco;

	    [DataMember(Name = "Preco", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Preço", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VendaDetalhe.Preco];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VendaDetalhe.Preco")]
	    public System.Nullable<System.Decimal> Preco
	    {
	    	    get
	    	    {
	    	          return _Preco;
	    	    }
	    	    set
	    	    {
	    	          if (this._Preco != value)
	    	          {
	    	              this.ValidateProperty("Preco", value);
	    	              this.OnPrecoChanging(value);
	    	              this.RaiseDataMemberChanging("Preco");
	    	              this._Preco = value;
	    	              this.RaiseDataMemberChanged("Preco");
	    	              this.OnPrecoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Produto
	    partial void OnProdutoChanging(System.String value);
	    partial void OnProdutoChanged();

	    private System.String _Produto;

	    [DataMember(Name = "Produto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Produto", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[ProdutoDomain];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VendaDetalhe.Produto];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VendaDetalhe.Produto")]
	    public System.String Produto
	    {
	    	    get
	    	    {
	    	          return _Produto;
	    	    }
	    	    set
	    	    {
	    	          if (this._Produto != value)
	    	          {
	    	              this.ValidateProperty("Produto", value);
	    	              this.OnProdutoChanging(value);
	    	              this.RaiseDataMemberChanging("Produto");
	    	              this._Produto = value;
	    	              this.RaiseDataMemberChanged("Produto");
	    	              this.OnProdutoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Quantidade
	    partial void OnQuantidadeChanging(System.Nullable<System.Int32> value);
	    partial void OnQuantidadeChanged();

	    private System.Nullable<System.Int32> _Quantidade;

	    [DataMember(Name = "Quantidade", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Quantidade", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VendaDetalhe.Quantidade];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VendaDetalhe.Quantidade")]
	    public System.Nullable<System.Int32> Quantidade
	    {
	    	    get
	    	    {
	    	          return _Quantidade;
	    	    }
	    	    set
	    	    {
	    	          if (this._Quantidade != value)
	    	          {
	    	              this.ValidateProperty("Quantidade", value);
	    	              this.OnQuantidadeChanging(value);
	    	              this.RaiseDataMemberChanging("Quantidade");
	    	              this._Quantidade = value;
	    	              this.RaiseDataMemberChanged("Quantidade");
	    	              this.OnQuantidadeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Data
	    partial void OnDataChanging(System.DateTime value);
	    partial void OnDataChanged();

	    private System.DateTime _Data;

	    [DataMember(IsRequired = true, Name = "Data", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Data", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VendaDetalhe.Vendas.Data];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Vendas.Data")]
	    public System.DateTime Data
	    {
	    	    get
	    	    {
	    	          return _Data;
	    	    }
	    	    set
	    	    {
	    	          if (this._Data != value)
	    	          {
	    	              this.ValidateProperty("Data", value);
	    	              this.OnDataChanging(value);
	    	              this.RaiseDataMemberChanging("Data");
	    	              this._Data = value;
	    	              this.RaiseDataMemberChanged("Data");
	    	              this.OnDataChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IDClientes
	    partial void OnIDClientesChanging(System.Guid value);
	    partial void OnIDClientesChanged();

	    private System.Guid _IDClientes;

	    [DataMember(IsRequired = true, Name = "IDClientes", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Clientes", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VendaDetalhe.Vendas.Clientes.ID_Clientes];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Vendas.Clientes.ID_Clientes")]
	    public System.Guid IDClientes
	    {
	    	    get
	    	    {
	    	          return _IDClientes;
	    	    }
	    	    set
	    	    {
	    	          if (this._IDClientes != value)
	    	          {
	    	              this.ValidateProperty("IDClientes", value);
	    	              this.OnIDClientesChanging(value);
	    	              this.RaiseDataMemberChanging("IDClientes");
	    	              this._IDClientes = value;
	    	              this.RaiseDataMemberChanged("IDClientes");
	    	              this.OnIDClientesChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Nome
	    partial void OnNomeChanging(System.String value);
	    partial void OnNomeChanged();

	    private System.String _Nome;

	    [DataMember(Name = "Nome", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome2", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VendaDetalhe.Vendas.Clientes.Nome];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Vendas.Clientes.Nome")]
	    public System.String Nome
	    {
	    	    get
	    	    {
	    	          return _Nome;
	    	    }
	    	    set
	    	    {
	    	          if (this._Nome != value)
	    	          {
	    	              this.ValidateProperty("Nome", value);
	    	              this.OnNomeChanging(value);
	    	              this.RaiseDataMemberChanging("Nome");
	    	              this._Nome = value;
	    	              this.RaiseDataMemberChanged("Nome");
	    	              this.OnNomeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Origem
	    partial void OnOrigemChanging(System.Nullable<System.Int32> value);
	    partial void OnOrigemChanged();

	    private System.Nullable<System.Int32> _Origem;

	    [DataMember(Name = "Origem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Origem", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[LXOrigem];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VendaDetalhe.Vendas.Origem];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Vendas.Origem")]
	    public System.Nullable<System.Int32> Origem
	    {
	    	    get
	    	    {
	    	          return _Origem;
	    	    }
	    	    set
	    	    {
	    	          if (this._Origem != value)
	    	          {
	    	              this.ValidateProperty("Origem", value);
	    	              this.OnOrigemChanging(value);
	    	              this.RaiseDataMemberChanging("Origem");
	    	              this._Origem = value;
	    	              this.RaiseDataMemberChanged("Origem");
	    	              this.OnOrigemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ValorTotal
	    partial void OnValorTotalChanging(System.Nullable<System.Decimal> value);
	    partial void OnValorTotalChanged();

	    private System.Nullable<System.Decimal> _ValorTotal;

	    [DataMember(Name = "ValorTotal", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ValorTotal", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VendaDetalhe.Vendas.ValorTotal];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Vendas.ValorTotal")]
	    public System.Nullable<System.Decimal> ValorTotal
	    {
	    	    get
	    	    {
	    	          return _ValorTotal;
	    	    }
	    	    set
	    	    {
	    	          if (this._ValorTotal != value)
	    	          {
	    	              this.ValidateProperty("ValorTotal", value);
	    	              this.OnValorTotalChanging(value);
	    	              this.RaiseDataMemberChanging("ValorTotal");
	    	              this._ValorTotal = value;
	    	              this.RaiseDataMemberChanged("ValorTotal");
	    	              this.OnValorTotalChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For VendaVip
	    partial void OnVendaVipChanging(System.Nullable<System.Boolean> value);
	    partial void OnVendaVipChanged();

	    private System.Nullable<System.Boolean> _VendaVip;

	    [DataMember(Name = "VendaVip", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "VendaVip", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VendaDetalhe.Vendas.VendaVip];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Vendas.VendaVip")]
	    public System.Nullable<System.Boolean> VendaVip
	    {
	    	    get
	    	    {
	    	          return _VendaVip;
	    	    }
	    	    set
	    	    {
	    	          if (this._VendaVip != value)
	    	          {
	    	              this.ValidateProperty("VendaVip", value);
	    	              this.OnVendaVipChanging(value);
	    	              this.RaiseDataMemberChanging("VendaVip");
	    	              this._VendaVip = value;
	    	              this.RaiseDataMemberChanged("VendaVip");
	    	              this.OnVendaVipChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ModeloVendaCliente.VendaDetalhe").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(LinxTraining002.BM.VendaDetalhe), QualifiedEntitySetName = "ModeloVendaCliente.VendaDetalhe" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VendaDetalhe.Hora", Source = "Hora", Target = "Hora", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.VendaDetalhe", RelationPropertyName = "VendaDetalhe" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VendaDetalhe.Preco", Source = "Preco", Target = "Preco", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.VendaDetalhe", RelationPropertyName = "VendaDetalhe" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VendaDetalhe.Produto", Source = "Produto", Target = "Produto", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.VendaDetalhe", RelationPropertyName = "VendaDetalhe" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VendaDetalhe.Quantidade", Source = "Quantidade", Target = "Quantidade", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.VendaDetalhe", RelationPropertyName = "VendaDetalhe" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VendaDetalhe.ID_VendaDetalhe", Source = "IDVendaDetalhe", Target = "ID_VendaDetalhe", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.VendaDetalhe", RelationPropertyName = "VendaDetalhe" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VendaDetalhe.Vendas.ID_Vendas", Source = "IDVendas", Target = "ID_Vendas", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ModeloVendaCliente.Vendas", RelationPropertyName = "Vendas" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetProdutoValues()
	    {
	    	    return LinxTraining001.BV.Domains.ProdutoDomain.GetValues();
	    }
	    private string _produtoName;
	    [DataMember(IsRequired = false, Name = "ProdutoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Produto", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ProdutoName
	    {
	    	    get { if (this.Produto.IsNullOrEmpty()) { _produtoName = String.Empty; } else { string key = this.Produto.ToString(); var dmValues = this.GetProdutoValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _produtoName) _produtoName = domainName; } return _produtoName; } set { _produtoName = value;  }
	    }
	    public Dictionary<string, string> GetOrigemValues()
	    {
	    	    return LinxTraining001.BV.Domains.LXOrigem.GetValues();
	    }
	    private string _origemName;
	    [DataMember(IsRequired = false, Name = "OrigemName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Origem", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string OrigemName
	    {
	    	    get { if (this.Origem.IsNullOrEmpty()) { _origemName = String.Empty; } else { string key = this.Origem.ToString(); var dmValues = this.GetOrigemValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _origemName) _origemName = domainName; } return _origemName; } set { _origemName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	//////////////////////// DomainService Class V1 ///////////////////////
	///////////////////////////////////////////////////////////////////////
	[EnableClientAccess()]	
	[DomainIdentifier("ProcessorOverviewDetalhamentoVendaDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class DetalhamentoVendaDomainService : DomainService, IDataServiceContext 
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

		
	    public DetalhamentoVendaDomainService() : this("", null, null){ }
	    public DetalhamentoVendaDomainService(string connectionString) : this(connectionString, null, null) { }
	    public DetalhamentoVendaDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public DetalhamentoVendaDomainService(LinxTraining002.BM.ModeloVendaCliente dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public DetalhamentoVendaDomainService(string connectionString, LinxTraining002.BM.ModeloVendaCliente dataContext, Dictionary<string, string> headers) : base() 
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
 	        var _VendasViewElements = changeSet.ChangeSetEntries.Where(e => e.Entity is VendasView && e.Entity.GetType().Name == "VendasView" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _VendasViewElements)
 	           if (((VendasView)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 
 	        //Remove inconsistent details
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is VendaDetalheView && e.Entity.GetType().Name == "VendaDetalheView" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
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
	    //Get All LookUpClientes.
	    public IQueryable<LookUpClientes> GetAllLookUpClientes()
	    {
	        return this.GetLookUpClientes(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpClientes By EntitySearch.
	    public IQueryable<LookUpClientes> GetLookUpClientesByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpClientes(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpClientes.
	    public IQueryable<LookUpClientes> GetLookUpClientes(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "Clientes" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpClientes";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpClientes));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpClientes> query =  
	
	            (from entity in this.DbContext.Clientes.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpClientes()		
	            {
	            
                IDClientes = entity.ID_Clientes
                , Nome = entity.Nome
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
	
		

	        if (entityName.InList("LinxTraining001.BV.DetalhamentoVenda.VendasView"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "VendasView",
	        			NameSpace = "LinxTraining001.BV.DetalhamentoVenda",
	        			ParentClassName = null,	
	        			DisplayName = "VendasView",
	        			ClearMethodName = "ClearVendasView",
	        			QueryMethodName  = "GetPagedVendasView",	
	        			CountingMethodName  = "GetVendasView" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("LinxTraining001.BV.DetalhamentoVenda.VendasView"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("LinxTraining001.BV.DetalhamentoVenda.VendasView"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("LinxTraining001.BV.DetalhamentoVenda.VendasView", "LinxTraining001.BV.DetalhamentoVenda.VendaDetalheView"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "VendaDetalheView" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "LinxTraining001.BV.DetalhamentoVenda",
	        			ParentClassName = "VendasView",	
	        			DisplayName = "VendaDetalheView",
	        			ClearMethodName = "ClearVendaDetalheView" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedVendaDetalheView" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetVendaDetalheView" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("LinxTraining001.BV.DetalhamentoVenda.VendaDetalheView"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("LinxTraining001.BV.DetalhamentoVenda.VendaDetalheView" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
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


             return new string[] { "LinxTraining001_detalhamentoVendaService", Linx.Tools.AssemblyHelper.ReadResourceContent("LinxTraining001.BV.ClientResources.detalhamentoVendaService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	

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
	    //Clear VendasView.
	    public IEnumerable<VendasView> ClearVendasView()
	    {
	        List<VendasView> result = new List<VendasView>();
	        result.Add(new VendasView());	
			
	        result[0].VendaDetalheViewList = new List<VendaDetalheView>();
	        ((List<VendaDetalheView>)result[0].VendaDetalheViewList).Add(new VendaDetalheView());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear VendaDetalheView.
	    public IEnumerable<VendaDetalheView> ClearVendaDetalheView()
	    {
	        List<VendaDetalheView> result = new List<VendaDetalheView>();
	        result.Add(new VendaDetalheView());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    [VendasViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendasView.
	    public IQueryable<VendasView> GetVendasView()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendasView")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendasViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<VendasView> result = 
	            (from entity0 in this.DbContext.Vendas
                  let entity0Al1 = entity0.Clientes
	            
	            	
	            select new VendasView()		
	            {
	            
                Data = entity0.Data
                , IDClientes = entity0Al1.ID_Clientes
                , IDVendas = entity0.ID_Vendas
                , Nome = entity0Al1.Nome
                , Origem = entity0.Origem
                , OrigemName = ((entity0.Origem) == 1 ? "Internet" : ((entity0.Origem) == 2 ? "Loja Física" : ""))
                , ValorTotal = entity0.ValorTotal
                , VendaVip = entity0.VendaVip
			
                ,VendaDetalheViewList = 
	                        (from entity1 in entity0.VendaDetalhe_LISTA
                                  let entity1Al1 = entity1.Vendas
	                        
	                        	
	                        select new VendaDetalheView()
	                        {
	                        
                                Hora = entity1.Hora
                                , IDVendaDetalhe = entity1.ID_VendaDetalhe
                                , IDVendas = entity1Al1.ID_Vendas
                                , Preco = entity1.Preco
                                , Produto = entity1.Produto
                                , ProdutoName = ((entity1.Produto) == "Item1" ? "PRODUTO A" : ((entity1.Produto) == "Item2" ? "PRODUTO B" : ((entity1.Produto) == "Item3" ? "PRODUTO C" : ((entity1.Produto) == "Item4" ? "PRODUTO D" : ""))))
                                , Quantidade = entity1.Quantidade
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendaDetalheViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendaDetalheView.
	    public IQueryable<VendaDetalheView> GetVendaDetalheView()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendaDetalheView")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaDetalheViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<VendaDetalheView> result = 
	            (from entity0 in this.DbContext.VendaDetalhe
                  let entity0Al1 = entity0.Vendas
	            
	            	
	            select new VendaDetalheView()		
	            {
	            
                Hora = entity0.Hora
                , IDVendaDetalhe = entity0.ID_VendaDetalhe
                , IDVendas = entity0Al1.ID_Vendas
                , Preco = entity0.Preco
                , Produto = entity0.Produto
                , ProdutoName = ((entity0.Produto) == "Item1" ? "PRODUTO A" : ((entity0.Produto) == "Item2" ? "PRODUTO B" : ((entity0.Produto) == "Item3" ? "PRODUTO C" : ((entity0.Produto) == "Item4" ? "PRODUTO D" : ""))))
                , Quantidade = entity0.Quantidade
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendasViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendasViewNoAssociations.
	    public IQueryable<VendasView> GetVendasViewNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendasViewNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendasViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<VendasView> result = 
	            (from entity0 in this.DbContext.Vendas
                  let entity0Al1 = entity0.Clientes
	            
	            	
	            select new VendasView()		
	            {
	            
                Data = entity0.Data
                , IDClientes = entity0Al1.ID_Clientes
                , IDVendas = entity0.ID_Vendas
                , Nome = entity0Al1.Nome
                , Origem = entity0.Origem
                , OrigemName = ((entity0.Origem) == 1 ? "Internet" : ((entity0.Origem) == 2 ? "Loja Física" : ""))
                , ValorTotal = entity0.ValorTotal
                , VendaVip = entity0.VendaVip
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendaDetalheViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendaDetalheViewNoAssociations.
	    public IQueryable<VendaDetalheView> GetVendaDetalheViewNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendaDetalheViewNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaDetalheViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<VendaDetalheView> result = 
	            (from entity0 in this.DbContext.VendaDetalhe
                  let entity0Al1 = entity0.Vendas
	            
	            	
	            select new VendaDetalheView()		
	            {
	            
                Hora = entity0.Hora
                , IDVendaDetalhe = entity0.ID_VendaDetalhe
                , IDVendas = entity0Al1.ID_Vendas
                , Preco = entity0.Preco
                , Produto = entity0.Produto
                , ProdutoName = ((entity0.Produto) == "Item1" ? "PRODUTO A" : ((entity0.Produto) == "Item2" ? "PRODUTO B" : ((entity0.Produto) == "Item3" ? "PRODUTO C" : ((entity0.Produto) == "Item4" ? "PRODUTO D" : ""))))
                , Quantidade = entity0.Quantidade
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	//Add filtering disabled property for Vendas
	    	string[] bmDisabledVendasViewList = this.GetEDM().GetFilteringDisabledList("Vendas");
	    	if (bmDisabledVendasViewList.Length > 0)
	    	{
	
	    		if (bmDisabledVendasViewList.Contains("Vendas.Data"))
	    		{
	    			result.Add("VendasView|Data");
	    			result.Add("VendasView|Vendas.Data");
	    		}
	
	    		if (bmDisabledVendasViewList.Contains("Vendas.ID_Vendas"))
	    		{
	    			result.Add("VendasView|IDVendas");
	    			result.Add("VendasView|Vendas.ID_Vendas");
	    		}
	
	    		if (bmDisabledVendasViewList.Contains("Vendas.Origem"))
	    		{
	    			result.Add("VendasView|Origem");
	    			result.Add("VendasView|Vendas.Origem");
	    		}
	
	    		if (bmDisabledVendasViewList.Contains("Vendas.ValorTotal"))
	    		{
	    			result.Add("VendasView|ValorTotal");
	    			result.Add("VendasView|Vendas.ValorTotal");
	    		}
	
	    		if (bmDisabledVendasViewList.Contains("Vendas.VendaVip"))
	    		{
	    			result.Add("VendasView|VendaVip");
	    			result.Add("VendasView|Vendas.VendaVip");
	    		}
	    	}
	    	//Add filtering disabled property for VendaDetalhe
	    	string[] bmDisabledVendaDetalheViewList = this.GetEDM().GetFilteringDisabledList("VendaDetalhe");
	    	if (bmDisabledVendaDetalheViewList.Length > 0)
	    	{
	
	    		if (bmDisabledVendaDetalheViewList.Contains("VendaDetalhe.Hora"))
	    		{
	    			result.Add("VendaDetalheView|Hora");
	    			result.Add("VendaDetalheView|VendaDetalhe.Hora");
	    		}
	
	    		if (bmDisabledVendaDetalheViewList.Contains("VendaDetalhe.ID_VendaDetalhe"))
	    		{
	    			result.Add("VendaDetalheView|IDVendaDetalhe");
	    			result.Add("VendaDetalheView|VendaDetalhe.ID_VendaDetalhe");
	    		}
	
	    		if (bmDisabledVendaDetalheViewList.Contains("VendaDetalhe.Preco"))
	    		{
	    			result.Add("VendaDetalheView|Preco");
	    			result.Add("VendaDetalheView|VendaDetalhe.Preco");
	    		}
	
	    		if (bmDisabledVendaDetalheViewList.Contains("VendaDetalhe.Produto"))
	    		{
	    			result.Add("VendaDetalheView|Produto");
	    			result.Add("VendaDetalheView|VendaDetalhe.Produto");
	    		}
	
	    		if (bmDisabledVendaDetalheViewList.Contains("VendaDetalhe.Quantidade"))
	    		{
	    			result.Add("VendaDetalheView|Quantidade");
	    			result.Add("VendaDetalheView|VendaDetalhe.Quantidade");
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
	    //Get VendasView By EntitySearchId.
	    public IQueryable<VendasView> GetVendasViewByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetVendasViewByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get VendaDetalheView By EntitySearchId.
	    public IQueryable<VendaDetalheView> GetVendaDetalheViewByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetVendaDetalheViewByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get VendasView By EntitySearchId.
	    public IQueryable<VendasView> GetVendasViewByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetVendasViewByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get VendaDetalheView By EntitySearchId.
	    public IQueryable<VendaDetalheView> GetVendaDetalheViewByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetVendaDetalheViewByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get VendasView By Example.
	    [Ignore]
	    public IQueryable<VendasView> GetVendasViewByExample(VendasView entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetVendasViewByEntitySearch(queryAnalysis);
	    }
			
	    //Get VendaDetalheView By Example.
	    [Ignore]
	    public IQueryable<VendaDetalheView> GetVendaDetalheViewByExample(VendaDetalheView entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetVendaDetalheViewByEntitySearch(queryAnalysis);
	    }
			
	    //Get VendasView By Example.
	    [Ignore]
	    public IQueryable<VendasView> GetVendasViewByExampleNoAssociations(VendasView entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetVendasViewByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get VendaDetalheView By Example.
	    [Ignore]
	    public IQueryable<VendaDetalheView> GetVendaDetalheViewByExampleNoAssociations(VendaDetalheView entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetVendaDetalheViewByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public VendasView GetVendasViewByKey(Int32 iDVendas)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("VendasView");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IDVendas"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, iDVendas));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetVendasViewByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public VendaDetalheView GetVendaDetalheViewByKey(Int32 iDVendaDetalhe)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("VendaDetalheView");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IDVendaDetalhe"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, iDVendaDetalhe));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetVendaDetalheViewByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    [VendasViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendasViewByEntitySearch.
	    public IQueryable<VendasView> GetVendasViewByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendasViewByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendasViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(VendasView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<VendasView> result = 
	            (from entity0 in this.DbContext.Vendas.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.Clientes
	            
	            	
	            select new VendasView()		
	            {
	            
                Data = entity0.Data
                , IDClientes = entity0Al1.ID_Clientes
                , IDVendas = entity0.ID_Vendas
                , Nome = entity0Al1.Nome
                , Origem = entity0.Origem
                , OrigemName = ((entity0.Origem) == 1 ? "Internet" : ((entity0.Origem) == 2 ? "Loja Física" : ""))
                , ValorTotal = entity0.ValorTotal
                , VendaVip = entity0.VendaVip
			
                ,VendaDetalheViewList = 
	                        (from entity1 in entity0.VendaDetalhe_LISTA
                                  let entity1Al1 = entity1.Vendas
	                        
	                        	
	                        select new VendaDetalheView()
	                        {
	                        
                                Hora = entity1.Hora
                                , IDVendaDetalhe = entity1.ID_VendaDetalhe
                                , IDVendas = entity1Al1.ID_Vendas
                                , Preco = entity1.Preco
                                , Produto = entity1.Produto
                                , ProdutoName = ((entity1.Produto) == "Item1" ? "PRODUTO A" : ((entity1.Produto) == "Item2" ? "PRODUTO B" : ((entity1.Produto) == "Item3" ? "PRODUTO C" : ((entity1.Produto) == "Item4" ? "PRODUTO D" : ""))))
                                , Quantidade = entity1.Quantidade
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendaDetalheViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendaDetalheViewByEntitySearch.
	    public IQueryable<VendaDetalheView> GetVendaDetalheViewByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendaDetalheViewByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaDetalheViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(VendaDetalheView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<VendaDetalheView> result = 
	            (from entity0 in this.DbContext.VendaDetalhe.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.Vendas
	            
	            	
	            select new VendaDetalheView()		
	            {
	            
                Hora = entity0.Hora
                , IDVendaDetalhe = entity0.ID_VendaDetalhe
                , IDVendas = entity0Al1.ID_Vendas
                , Preco = entity0.Preco
                , Produto = entity0.Produto
                , ProdutoName = ((entity0.Produto) == "Item1" ? "PRODUTO A" : ((entity0.Produto) == "Item2" ? "PRODUTO B" : ((entity0.Produto) == "Item3" ? "PRODUTO C" : ((entity0.Produto) == "Item4" ? "PRODUTO D" : ""))))
                , Quantidade = entity0.Quantidade
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendasViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendasViewByEntitySearchNoAssociations.
	    public IQueryable<VendasView> GetVendasViewByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendasViewByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendasViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(VendasView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<VendasView> result = 
	            (from entity0 in this.DbContext.Vendas.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.Clientes
	            
	            	
	            select new VendasView()		
	            {
	            
                Data = entity0.Data
                , IDClientes = entity0Al1.ID_Clientes
                , IDVendas = entity0.ID_Vendas
                , Nome = entity0Al1.Nome
                , Origem = entity0.Origem
                , OrigemName = ((entity0.Origem) == 1 ? "Internet" : ((entity0.Origem) == 2 ? "Loja Física" : ""))
                , ValorTotal = entity0.ValorTotal
                , VendaVip = entity0.VendaVip
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendaDetalheViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendaDetalheViewByEntitySearchNoAssociations.
	    public IQueryable<VendaDetalheView> GetVendaDetalheViewByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendaDetalheViewByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaDetalheViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(VendaDetalheView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<VendaDetalheView> result = 
	            (from entity0 in this.DbContext.VendaDetalhe.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.Vendas
	            
	            	
	            select new VendaDetalheView()		
	            {
	            
                Hora = entity0.Hora
                , IDVendaDetalhe = entity0.ID_VendaDetalhe
                , IDVendas = entity0Al1.ID_Vendas
                , Preco = entity0.Preco
                , Produto = entity0.Produto
                , ProdutoName = ((entity0.Produto) == "Item1" ? "PRODUTO A" : ((entity0.Produto) == "Item2" ? "PRODUTO B" : ((entity0.Produto) == "Item3" ? "PRODUTO C" : ((entity0.Produto) == "Item4" ? "PRODUTO D" : ""))))
                , Quantidade = entity0.Quantidade
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendaDetalheViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendaDetalheViewParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<VendaDetalheViewParentComposition> GetVendaDetalheViewParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendaDetalheViewParentCompositionByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaDetalheViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(VendaDetalheViewParentComposition));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<VendaDetalheViewParentComposition> result = 
	            (from entity0 in this.DbContext.VendaDetalhe.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.Vendas
	            
	            	
	            select new VendaDetalheViewParentComposition()		
	            {
	            
                Hora = entity0.Hora
                , IDVendaDetalhe = entity0.ID_VendaDetalhe
                , IDVendas = entity0Al1.ID_Vendas
                , Preco = entity0.Preco
                , Produto = entity0.Produto
                , ProdutoName = ((entity0.Produto) == "Item1" ? "PRODUTO A" : ((entity0.Produto) == "Item2" ? "PRODUTO B" : ((entity0.Produto) == "Item3" ? "PRODUTO C" : ((entity0.Produto) == "Item4" ? "PRODUTO D" : ""))))
                , Quantidade = entity0.Quantidade
                //VendasView Properties.
                , Data = entity0.Vendas.Data
                , IDClientes = entity0.Vendas.Clientes.ID_Clientes
                , Nome = entity0.Vendas.Clientes.Nome
                , Origem = entity0.Vendas.Origem
                , OrigemName = ((entity0.Vendas.Origem) == 1 ? "Internet" : ((entity0.Vendas.Origem) == 2 ? "Loja Física" : ""))
                , ValorTotal = entity0.Vendas.ValorTotal
                , VendaVip = entity0.Vendas.VendaVip
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    [VendasViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedVendasView.
	    public IQueryable<VendasView> GetPagedVendasView(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedVendasView")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendasViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(VendasView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<VendasView> result = 
	            (from entity0 in this.DbContext.Vendas.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.Clientes
                orderby entity0.ID_Vendas ascending
	            
	            	
	            select new VendasView()		
	            {
	            
                Data = entity0.Data
                , IDClientes = entity0Al1.ID_Clientes
                , IDVendas = entity0.ID_Vendas
                , Nome = entity0Al1.Nome
                , Origem = entity0.Origem
                , OrigemName = ((entity0.Origem) == 1 ? "Internet" : ((entity0.Origem) == 2 ? "Loja Física" : ""))
                , ValorTotal = entity0.ValorTotal
                , VendaVip = entity0.VendaVip
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendaDetalheViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedVendaDetalheView.
	    public IQueryable<VendaDetalheView> GetPagedVendaDetalheView(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedVendaDetalheView")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaDetalheViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(VendaDetalheView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<VendaDetalheView> result = 
	            (from entity0 in this.DbContext.VendaDetalhe.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.Vendas
                orderby entity0.ID_VendaDetalhe ascending
	            
	            	
	            select new VendaDetalheView()		
	            {
	            
                Hora = entity0.Hora
                , IDVendaDetalhe = entity0.ID_VendaDetalhe
                , IDVendas = entity0Al1.ID_Vendas
                , Preco = entity0.Preco
                , Produto = entity0.Produto
                , ProdutoName = ((entity0.Produto) == "Item1" ? "PRODUTO A" : ((entity0.Produto) == "Item2" ? "PRODUTO B" : ((entity0.Produto) == "Item3" ? "PRODUTO C" : ((entity0.Produto) == "Item4" ? "PRODUTO D" : ""))))
                , Quantidade = entity0.Quantidade
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetVendasViewCounting(string serializedEntitySearch)
	    {	
		 	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(VendasView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.Vendas.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.Clientes
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetVendaDetalheViewCounting(string serializedEntitySearch)
	    {	
		 	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(VendaDetalheView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.VendaDetalhe.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.Vendas
	            
	            select 1
	            ).Count();	
		
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    [VendasViewUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update VendasView.
	    public void UpdateVendasView(VendasView entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateVendasView")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendasViewUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    [VendasViewInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert VendasView.
	    public void InsertVendasView(VendasView entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertVendasView")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendasViewInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    [VendasViewDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete VendasView.
	    public void DeleteVendasView(VendasView entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteVendasView")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendasViewDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    [VendaDetalheViewUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update VendaDetalheView.
	    public void UpdateVendaDetalheView(VendaDetalheView entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateVendaDetalheView")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaDetalheViewUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.VendasView.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.VendasView) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.VendasView); 	
	            

	
	        }
	
	    }

	    [VendaDetalheViewInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert VendaDetalheView.
	    public void InsertVendaDetalheView(VendaDetalheView entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertVendaDetalheView")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaDetalheViewInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.VendasView.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.VendasView) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.VendasView);
	            

	
	        }
	
	    }

	    [VendaDetalheViewDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete VendaDetalheView.
	    public void DeleteVendaDetalheView(VendaDetalheView entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteVendaDetalheView")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaDetalheViewDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.VendasView.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.VendasView) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.VendasView);
	            

	
	        }

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}