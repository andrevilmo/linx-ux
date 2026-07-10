					
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

namespace LinxTraining001.BV.tstTableMedias
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="Clientes.ID_Clientes", IsUpdatable=false, EdmName="LinxTraining002.BM.ModeloVendaCliente")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[ClientesView,ClientesView.VendasView];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IDClientes];ReadOnly[false];Entities[Clientes:IDClientes];SubQueryInfo[];EdmEntityName[Clientes];EntityRelations[];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "ClientesView")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "LinxTraining001.BV.tstTableMedias.ClientesView")]
	public partial class ClientesView : Linx.Data.Entity
	{

	
		
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.VendasViewList != null && this.VendasViewList.Count() > 0)
	      {
	         foreach (var entity in this.VendasViewList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.VendasViewList != null)
	      {
	         foreach (var detail in this.VendasViewList)
	         {
	            detail.ResetDetails();
	         }
	         this.VendasViewList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(tstTableMediasDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("VendasView"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("VendasView");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IDClientes"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IDClientes));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load VendasView and all sub-details
	         if (this.VendasViewList == null || this.VendasViewList.Count() == 0)
	         {
	             if (take > 0)
	                 this.VendasViewList = context.GetPagedVendasView(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.VendasViewList = (from r in context.GetVendasViewByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _VendasViewElements = changeSet.ChangeSetEntries.Where(e => e.Entity is VendasView && ((VendasView)e.Entity).ClientesView == null && e.Associations == null && e.OriginalAssociations == null && ((VendasView)e.Entity).IDClientes == this.IDClientes).ToList();
 	      if (_VendasViewElements.Count > 0 && this.VendasViewList.Count() == 0)
 	      {
 	          this.VendasViewList = _VendasViewElements.Select(e => (VendasView)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _VendasViewElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((VendasView)detail.Entity).ClientesView = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("ClientesView", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("VendasViewList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For IDClientes
	    partial void OnIDClientesChanging(System.Guid value);
	    partial void OnIDClientesChanged();

	    private System.Guid _IDClientes;

	    [DataMember(IsRequired = true, Name = "IDClientes", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Clientes", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[Clientes.ID_Clientes];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Clientes.ID_Clientes")]
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
	    [Display(Name = "Nome", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[Clientes.Nome];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Clientes.Nome")]
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
	    //Extensibility Partial Method Definitions For Tipo
	    partial void OnTipoChanging(System.Nullable<System.Int32> value);
	    partial void OnTipoChanged();

	    private System.Nullable<System.Int32> _Tipo;

	    [DataMember(Name = "Tipo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[LXTipoClientes];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[Clientes.Tipo];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Clientes.Tipo")]
	    public System.Nullable<System.Int32> Tipo
	    {
	    	    get
	    	    {
	    	          return _Tipo;
	    	    }
	    	    set
	    	    {
	    	          if (this._Tipo != value)
	    	          {
	    	              this.ValidateProperty("Tipo", value);
	    	              this.OnTipoChanging(value);
	    	              this.RaiseDataMemberChanging("Tipo");
	    	              this._Tipo = value;
	    	              this.RaiseDataMemberChanged("Tipo");
	    	              this.OnTipoChanged();
	    	          }
	    	    }
	    }

	    private System.Guid _TemporaryIDClientes;
	    [DataMember(Name = "TemporaryIDClientes", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Clientes (Tmp)", Description="Temporary Key", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public System.Guid TemporaryIDClientes
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIDClientes.IsNullOrEmpty())
	    	                this._TemporaryIDClientes = this._IDClientes;
	    	          return this._TemporaryIDClientes;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIDClientes != value)
	    	              this._TemporaryIDClientes = value;
	    	    }
	    }	

	    #endregion Data Properties

	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<VendasView> _VendasViewList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_ClientesView_VendasView", "IDClientes", "IDClientes", IsForeignKey=false)]
	    [DataMember(Name = "VendasViewList", EmitDefaultValue = true)]
	    public IEnumerable<VendasView> VendasViewList
	    {
	        get
	        {
	
	            if (this._VendasViewList == null)
	            	this._VendasViewList = new List<VendasView>();
	
	            return this._VendasViewList;
	        }
	        set
	        {
	            if (this._VendasViewList != value)
	            {
	                this._VendasViewList = value;
	                this.RaisePropertyChanged("VendasViewList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ModeloVendaCliente.Clientes").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(LinxTraining002.BM.Clientes), QualifiedEntitySetName = "ModeloVendaCliente.Clientes" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Clientes.Nome", Source = "Nome", Target = "Nome", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.Clientes", RelationPropertyName = "Clientes" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Clientes.Tipo", Source = "Tipo", Target = "Tipo", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.Clientes", RelationPropertyName = "Clientes" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Clientes.ID_Clientes", Source = "IDClientes", Target = "ID_Clientes", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.Clientes", RelationPropertyName = "Clientes" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 

	    [DataMember()]
	    public string TableMedia { get; set; }	


	    public void SaveMedia(DomainOperation operation)
	    {
	         if (!this.TableMedia.IsNullOrEmpty() && (operation == DomainOperation.Insert || operation == DomainOperation.Update))
	         {
	             Linx.Business.Tools.MediaHelper.SyncMedia("Clientes", null, this.IDClientes, this.TableMedia.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(e => new Guid(e)).ToList());
	         }
	         else if (operation == DomainOperation.Delete) {
	             Linx.Business.Tools.MediaHelper.SyncMedia("Clientes", null, this.IDClientes, new List<Guid>() { Guid.Empty });
	         }
	    }

	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetTipoValues()
	    {
	    	    return LinxTraining001.BV.Domains.LXTipoClientes.GetValues();
	    }
	    private string _tipoName;
	    [DataMember(IsRequired = false, Name = "TipoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string TipoName
	    {
	    	    get { if (this.Tipo.IsNullOrEmpty()) { _tipoName = String.Empty; } else { string key = this.Tipo.ToString(); var dmValues = this.GetTipoValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _tipoName) _tipoName = domainName; } return _tipoName; } set { _tipoName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="Vendas.ID_Vendas", IsUpdatable=false, EdmName="LinxTraining002.BM.ModeloVendaCliente")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[VendasView];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IDVendas];ReadOnly[false];Entities[Vendas:IDVendas];SubQueryInfo[Select 1 From #ParentAlias#.Vendas_LISTA as #Alias#];EdmEntityName[Vendas];EntityRelations[Clientes(Clientes)];EdmParentEntityName[Clientes];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "VendasView")]
	[Serializable()]
	public partial class VendasView : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(tstTableMediasDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("ClientesView");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IDClientes"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IDClientes));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load ClientesView
	         this.ClientesView = (from r in context.GetClientesViewByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	    [Display(Name = "ID Clientes", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[Vendas.Clientes.ID_Clientes];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For IDVendas
	    partial void OnIDVendasChanging(Int32 value);
	    partial void OnIDVendasChanged();

	    private Int32 _IDVendas;

	    [DataMember(IsRequired = true, Name = "IDVendas", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Vendas", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    [Display(Name = "ID Vendas (Tmp)", Description="Temporary Key", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
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

		

	    #region Parent Association
	 
	    private ClientesView _ClientesView;
	    [DataMember(Name = "ClientesView", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_ClientesView_VendasView", "IDClientes", "IDClientes", IsForeignKey=true)]
	    public ClientesView ClientesView
	    {
	        get
	        {
	            return this._ClientesView;
	        }
	        set
	        {
	            if (this._ClientesView != value)
	            {
	                this._ClientesView = value;
	                this.RaisePropertyChanged("ClientesViewList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
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
	 

	    [DataMember()]
	    public string TableMedia { get; set; }	


	    public void SaveMedia(DomainOperation operation)
	    {
	         if (!this.TableMedia.IsNullOrEmpty() && (operation == DomainOperation.Insert || operation == DomainOperation.Update))
	         {
	             Linx.Business.Tools.MediaHelper.SyncMedia("Vendas", this.IDVendas, null, this.TableMedia.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(e => new Guid(e)).ToList());
	         }
	         else if (operation == DomainOperation.Delete) {
	             Linx.Business.Tools.MediaHelper.SyncMedia("Vendas", this.IDVendas, null, new List<Guid>() { Guid.Empty });
	         }
	    }

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

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[VendasView];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IDVendas];ReadOnly[false];Entities[Vendas:IDVendas];SubQueryInfo[Select 1 From #ParentAlias#.Vendas_LISTA as #Alias#];EdmEntityName[Vendas];EntityRelations[Clientes(Clientes)];EdmParentEntityName[Clientes];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "VendasView")]
	[Serializable()]
	public partial class VendasViewParentComposition : Linx.Data.Entity
	{

	
	
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
	    [Display(Name = "ID Clientes", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[Vendas.Clientes.ID_Clientes];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For IDVendas
	    partial void OnIDVendasChanging(Int32 value);
	    partial void OnIDVendasChanged();

	    private Int32 _IDVendas;

	    [DataMember(IsRequired = true, Name = "IDVendas", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Vendas", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    //Extensibility Partial Method Definitions For Nome
	    partial void OnNomeChanging(System.String value);
	    partial void OnNomeChanged();

	    private System.String _Nome;

	    [DataMember(Name = "Nome", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[Vendas.Clientes.Nome];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Clientes.Nome")]
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
	    //Extensibility Partial Method Definitions For Tipo
	    partial void OnTipoChanging(System.Nullable<System.Int32> value);
	    partial void OnTipoChanged();

	    private System.Nullable<System.Int32> _Tipo;

	    [DataMember(Name = "Tipo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[LXTipoClientes];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[Vendas.Clientes.Tipo];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Clientes.Tipo")]
	    public System.Nullable<System.Int32> Tipo
	    {
	    	    get
	    	    {
	    	          return _Tipo;
	    	    }
	    	    set
	    	    {
	    	          if (this._Tipo != value)
	    	          {
	    	              this.ValidateProperty("Tipo", value);
	    	              this.OnTipoChanging(value);
	    	              this.RaiseDataMemberChanging("Tipo");
	    	              this._Tipo = value;
	    	              this.RaiseDataMemberChanged("Tipo");
	    	              this.OnTipoChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
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
	 

	    [DataMember()]
	    public string TableMedia { get; set; }	


	    public void SaveMedia(DomainOperation operation)
	    {
	         if (!this.TableMedia.IsNullOrEmpty() && (operation == DomainOperation.Insert || operation == DomainOperation.Update))
	         {
	             Linx.Business.Tools.MediaHelper.SyncMedia("Vendas", this.IDVendas, null, this.TableMedia.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(e => new Guid(e)).ToList());
	         }
	         else if (operation == DomainOperation.Delete) {
	             Linx.Business.Tools.MediaHelper.SyncMedia("Vendas", this.IDVendas, null, new List<Guid>() { Guid.Empty });
	         }
	    }

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
	    public Dictionary<string, string> GetTipoValues()
	    {
	    	    return LinxTraining001.BV.Domains.LXTipoClientes.GetValues();
	    }
	    private string _tipoName;
	    [DataMember(IsRequired = false, Name = "TipoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string TipoName
	    {
	    	    get { if (this.Tipo.IsNullOrEmpty()) { _tipoName = String.Empty; } else { string key = this.Tipo.ToString(); var dmValues = this.GetTipoValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _tipoName) _tipoName = domainName; } return _tipoName; } set { _tipoName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	//////////////////////// DomainService Class V1 ///////////////////////
	///////////////////////////////////////////////////////////////////////
	[EnableClientAccess()]	
	[DomainIdentifier("ProcessorOverviewtstTableMediasDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class tstTableMediasDomainService : DomainService, IDataServiceContext 
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

		
	    public tstTableMediasDomainService() : this("", null, null){ }
	    public tstTableMediasDomainService(string connectionString) : this(connectionString, null, null) { }
	    public tstTableMediasDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public tstTableMediasDomainService(LinxTraining002.BM.ModeloVendaCliente dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public tstTableMediasDomainService(string connectionString, LinxTraining002.BM.ModeloVendaCliente dataContext, Dictionary<string, string> headers) : base() 
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
	    			if (entry.Entity is ClientesView) ((ClientesView)entry.Entity).SaveMedia(entry.Operation);
	    			if (entry.Entity is VendasView) ((VendasView)entry.Entity).SaveMedia(entry.Operation);
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
 	        var _ClientesViewElements = changeSet.ChangeSetEntries.Where(e => e.Entity is ClientesView && e.Entity.GetType().Name == "ClientesView" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _ClientesViewElements)
 	           if (((ClientesView)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 
 	        //Remove inconsistent details
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is VendasView && e.Entity.GetType().Name == "VendasView" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
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
	
		

	        if (entityName.InList("LinxTraining001.BV.tstTableMedias.ClientesView"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "ClientesView",
	        			NameSpace = "LinxTraining001.BV.tstTableMedias",
	        			ParentClassName = null,	
	        			DisplayName = "ClientesView",
	        			ClearMethodName = "ClearClientesView",
	        			QueryMethodName  = "GetPagedClientesView",	
	        			CountingMethodName  = "GetClientesView" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("LinxTraining001.BV.tstTableMedias.ClientesView"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("LinxTraining001.BV.tstTableMedias.ClientesView"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("LinxTraining001.BV.tstTableMedias.ClientesView", "LinxTraining001.BV.tstTableMedias.VendasView"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "VendasView" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "LinxTraining001.BV.tstTableMedias",
	        			ParentClassName = "ClientesView",	
	        			DisplayName = "VendasView",
	        			ClearMethodName = "ClearVendasView" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedVendasView" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetVendasView" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("LinxTraining001.BV.tstTableMedias.VendasView"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("LinxTraining001.BV.tstTableMedias.VendasView" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
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


             return new string[] { "LinxTraining001_tstTableMediasService", Linx.Tools.AssemblyHelper.ReadResourceContent("LinxTraining001.BV.ClientResources.tstTableMediasService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	

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
	    //Clear ClientesView.
	    public IEnumerable<ClientesView> ClearClientesView()
	    {
	        List<ClientesView> result = new List<ClientesView>();
	        result.Add(new ClientesView());	
			
	        result[0].VendasViewList = new List<VendasView>();
	        ((List<VendasView>)result[0].VendasViewList).Add(new VendasView());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear VendasView.
	    public IEnumerable<VendasView> ClearVendasView()
	    {
	        List<VendasView> result = new List<VendasView>();
	        result.Add(new VendasView());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    [ClientesViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get ClientesView.
	    public IQueryable<ClientesView> GetClientesView()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetClientesView")))
 	        {
 	             AuthorizationResult authorizationResult = (new ClientesViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<ClientesView> result = 
	            (from entity0 in this.DbContext.Clientes
	            
	            	
	            select new ClientesView()		
	            {
	            
                IDClientes = entity0.ID_Clientes
                , Nome = entity0.Nome
                , Tipo = entity0.Tipo
                , TipoName = ((entity0.Tipo) == 3 ? "Fornecedor" : ((entity0.Tipo) == 1 ? "Pessoa Física" : ((entity0.Tipo) == 2 ? "Pessoa Jurídica" : "")))
			
                ,VendasViewList = 
	                        (from entity1 in entity0.Vendas_LISTA
                                  let entity1Al1 = entity1.Clientes
	                        
	                        	
	                        select new VendasView()
	                        {
	                        
                                Data = entity1.Data
                                , IDClientes = entity1Al1.ID_Clientes
                                , IDVendas = entity1.ID_Vendas
                                , Origem = entity1.Origem
                                , OrigemName = ((entity1.Origem) == 1 ? "Internet" : ((entity1.Origem) == 2 ? "Loja Física" : ""))
                                , ValorTotal = entity1.ValorTotal
                                , VendaVip = entity1.VendaVip
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
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
                , Origem = entity0.Origem
                , OrigemName = ((entity0.Origem) == 1 ? "Internet" : ((entity0.Origem) == 2 ? "Loja Física" : ""))
                , ValorTotal = entity0.ValorTotal
                , VendaVip = entity0.VendaVip
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [ClientesViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get ClientesViewNoAssociations.
	    public IQueryable<ClientesView> GetClientesViewNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetClientesViewNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new ClientesViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<ClientesView> result = 
	            (from entity0 in this.DbContext.Clientes
	            
	            	
	            select new ClientesView()		
	            {
	            
                IDClientes = entity0.ID_Clientes
                , Nome = entity0.Nome
                , Tipo = entity0.Tipo
                , TipoName = ((entity0.Tipo) == 3 ? "Fornecedor" : ((entity0.Tipo) == 1 ? "Pessoa Física" : ((entity0.Tipo) == 2 ? "Pessoa Jurídica" : "")))
		
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
                , Origem = entity0.Origem
                , OrigemName = ((entity0.Origem) == 1 ? "Internet" : ((entity0.Origem) == 2 ? "Loja Física" : ""))
                , ValorTotal = entity0.ValorTotal
                , VendaVip = entity0.VendaVip
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	//Add filtering disabled property for Clientes
	    	string[] bmDisabledClientesViewList = this.GetEDM().GetFilteringDisabledList("Clientes");
	    	if (bmDisabledClientesViewList.Length > 0)
	    	{
	
	    		if (bmDisabledClientesViewList.Contains("Clientes.ID_Clientes"))
	    		{
	    			result.Add("ClientesView|IDClientes");
	    			result.Add("ClientesView|Clientes.ID_Clientes");
	    		}
	
	    		if (bmDisabledClientesViewList.Contains("Clientes.Nome"))
	    		{
	    			result.Add("ClientesView|Nome");
	    			result.Add("ClientesView|Clientes.Nome");
	    		}
	
	    		if (bmDisabledClientesViewList.Contains("Clientes.Tipo"))
	    		{
	    			result.Add("ClientesView|Tipo");
	    			result.Add("ClientesView|Clientes.Tipo");
	    		}
	    	}
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
	    //Get ClientesView By EntitySearchId.
	    public IQueryable<ClientesView> GetClientesViewByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetClientesViewByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get VendasView By EntitySearchId.
	    public IQueryable<VendasView> GetVendasViewByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetVendasViewByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get ClientesView By EntitySearchId.
	    public IQueryable<ClientesView> GetClientesViewByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetClientesViewByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get VendasView By EntitySearchId.
	    public IQueryable<VendasView> GetVendasViewByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetVendasViewByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get ClientesView By Example.
	    [Ignore]
	    public IQueryable<ClientesView> GetClientesViewByExample(ClientesView entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetClientesViewByEntitySearch(queryAnalysis);
	    }
			
	    //Get VendasView By Example.
	    [Ignore]
	    public IQueryable<VendasView> GetVendasViewByExample(VendasView entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetVendasViewByEntitySearch(queryAnalysis);
	    }
			
	    //Get ClientesView By Example.
	    [Ignore]
	    public IQueryable<ClientesView> GetClientesViewByExampleNoAssociations(ClientesView entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetClientesViewByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get VendasView By Example.
	    [Ignore]
	    public IQueryable<VendasView> GetVendasViewByExampleNoAssociations(VendasView entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetVendasViewByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public ClientesView GetClientesViewByKey(System.Guid iDClientes)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("ClientesView");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IDClientes"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, iDClientes));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetClientesViewByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


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

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    [ClientesViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get ClientesViewByEntitySearch.
	    public IQueryable<ClientesView> GetClientesViewByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetClientesViewByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new ClientesViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(ClientesView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<ClientesView> result = 
	            (from entity0 in this.DbContext.Clientes.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new ClientesView()		
	            {
	            
                IDClientes = entity0.ID_Clientes
                , Nome = entity0.Nome
                , Tipo = entity0.Tipo
                , TipoName = ((entity0.Tipo) == 3 ? "Fornecedor" : ((entity0.Tipo) == 1 ? "Pessoa Física" : ((entity0.Tipo) == 2 ? "Pessoa Jurídica" : "")))
			
                ,VendasViewList = 
	                        (from entity1 in entity0.Vendas_LISTA
                                  let entity1Al1 = entity1.Clientes
	                        
	                        	
	                        select new VendasView()
	                        {
	                        
                                Data = entity1.Data
                                , IDClientes = entity1Al1.ID_Clientes
                                , IDVendas = entity1.ID_Vendas
                                , Origem = entity1.Origem
                                , OrigemName = ((entity1.Origem) == 1 ? "Internet" : ((entity1.Origem) == 2 ? "Loja Física" : ""))
                                , ValorTotal = entity1.ValorTotal
                                , VendaVip = entity1.VendaVip
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
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
                , Origem = entity0.Origem
                , OrigemName = ((entity0.Origem) == 1 ? "Internet" : ((entity0.Origem) == 2 ? "Loja Física" : ""))
                , ValorTotal = entity0.ValorTotal
                , VendaVip = entity0.VendaVip
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [ClientesViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get ClientesViewByEntitySearchNoAssociations.
	    public IQueryable<ClientesView> GetClientesViewByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetClientesViewByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new ClientesViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(ClientesView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<ClientesView> result = 
	            (from entity0 in this.DbContext.Clientes.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new ClientesView()		
	            {
	            
                IDClientes = entity0.ID_Clientes
                , Nome = entity0.Nome
                , Tipo = entity0.Tipo
                , TipoName = ((entity0.Tipo) == 3 ? "Fornecedor" : ((entity0.Tipo) == 1 ? "Pessoa Física" : ((entity0.Tipo) == 2 ? "Pessoa Jurídica" : "")))
		
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
                , Origem = entity0.Origem
                , OrigemName = ((entity0.Origem) == 1 ? "Internet" : ((entity0.Origem) == 2 ? "Loja Física" : ""))
                , ValorTotal = entity0.ValorTotal
                , VendaVip = entity0.VendaVip
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendasViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendasViewParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<VendasViewParentComposition> GetVendasViewParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendasViewParentCompositionByEntitySearchNoAssociations")))
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(VendasViewParentComposition));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<VendasViewParentComposition> result = 
	            (from entity0 in this.DbContext.Vendas.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.Clientes
	            
	            	
	            select new VendasViewParentComposition()		
	            {
	            
                Data = entity0.Data
                , IDClientes = entity0Al1.ID_Clientes
                , IDVendas = entity0.ID_Vendas
                , Origem = entity0.Origem
                , OrigemName = ((entity0.Origem) == 1 ? "Internet" : ((entity0.Origem) == 2 ? "Loja Física" : ""))
                , ValorTotal = entity0.ValorTotal
                , VendaVip = entity0.VendaVip
                //ClientesView Properties.
                , Nome = entity0.Clientes.Nome
                , Tipo = entity0.Clientes.Tipo
                , TipoName = ((entity0.Clientes.Tipo) == 3 ? "Fornecedor" : ((entity0.Clientes.Tipo) == 1 ? "Pessoa Física" : ((entity0.Clientes.Tipo) == 2 ? "Pessoa Jurídica" : "")))
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    [ClientesViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedClientesView.
	    public IQueryable<ClientesView> GetPagedClientesView(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedClientesView")))
 	        {
 	             AuthorizationResult authorizationResult = (new ClientesViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(ClientesView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<ClientesView> result = 
	            (from entity0 in this.DbContext.Clientes.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_Clientes ascending
	            
	            	
	            select new ClientesView()		
	            {
	            
                IDClientes = entity0.ID_Clientes
                , Nome = entity0.Nome
                , Tipo = entity0.Tipo
                , TipoName = ((entity0.Tipo) == 3 ? "Fornecedor" : ((entity0.Tipo) == 1 ? "Pessoa Física" : ((entity0.Tipo) == 2 ? "Pessoa Jurídica" : "")))
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
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
                , Origem = entity0.Origem
                , OrigemName = ((entity0.Origem) == 1 ? "Internet" : ((entity0.Origem) == 2 ? "Loja Física" : ""))
                , ValorTotal = entity0.ValorTotal
                , VendaVip = entity0.VendaVip
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetClientesViewCounting(string serializedEntitySearch)
	    {	
		 	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(ClientesView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.Clientes.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
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
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    [ClientesViewUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update ClientesView.
	    public void UpdateClientesView(ClientesView entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateClientesView")))
 	        {
 	             AuthorizationResult authorizationResult = (new ClientesViewUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    [ClientesViewInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert ClientesView.
	    public void InsertClientesView(ClientesView entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertClientesView")))
 	        {
 	             AuthorizationResult authorizationResult = (new ClientesViewInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    [ClientesViewDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete ClientesView.
	    public void DeleteClientesView(ClientesView entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteClientesView")))
 	        {
 	             AuthorizationResult authorizationResult = (new ClientesViewDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
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

	
	        if (entity.ClientesView.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.ClientesView) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.ClientesView); 	
	            

	
	        }
	
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

	
	        if (entity.ClientesView.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.ClientesView) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.ClientesView);
	            

	
	        }
	
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

	
	        if (entity.ClientesView.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.ClientesView) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.ClientesView);
	            

	
	        }

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}