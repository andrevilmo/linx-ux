					
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

namespace LinxTraining001.BV.TestePivotGrid
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="PivoGridOlap.EntityUniqueKey", IsUpdatable=false, EdmName="")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[PivoGridOlap,PivoGridOlap.PivotGridOlapFilha];IsOlap[true];OlapCatalogName[DEV-BI-Omni];CubeName[ATENDIMENTO];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[];EdmEntityName[];EntityRelations[ ];EdmParentEntityName[];IsIQueryable[false]")]
		
	[DataContract(IsReference = false, Name = "PivoGridOlap")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "LinxTraining001.BV.TestePivotGrid.PivoGridOlap")]
	public partial class PivoGridOlap : Linx.Data.Entity
	{

	
		
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.PivotGridOlapFilhaList != null && this.PivotGridOlapFilhaList.Count() > 0)
	      {
	         foreach (var entity in this.PivotGridOlapFilhaList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.PivotGridOlapFilhaList != null)
	      {
	         foreach (var detail in this.PivotGridOlapFilhaList)
	         {
	            detail.ResetDetails();
	         }
	         this.PivotGridOlapFilhaList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(TestePivotGridDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("PivotGridOlapFilha"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load PivotGridOlapFilha and all sub-details
	         if (this.PivotGridOlapFilhaList == null || this.PivotGridOlapFilhaList.Count() == 0)
	         {
	             if (take > 0)
	                 this.PivotGridOlapFilhaList = context.GetPagedPivotGridOlapFilha(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.PivotGridOlapFilhaList = (from r in context.GetPivotGridOlapFilhaByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _PivotGridOlapFilhaElements = changeSet.ChangeSetEntries.Where(e => e.Entity is PivotGridOlapFilha && ((PivotGridOlapFilha)e.Entity).PivoGridOlap == null && e.Associations == null && e.OriginalAssociations == null).ToList();
 	      if (_PivotGridOlapFilhaElements.Count > 0 && this.PivotGridOlapFilhaList.Count() == 0)
 	      {
 	          this.PivotGridOlapFilhaList = _PivotGridOlapFilhaElements.Select(e => (PivotGridOlapFilha)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _PivotGridOlapFilhaElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((PivotGridOlapFilha)detail.Entity).PivoGridOlap = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("PivoGridOlap", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("PivotGridOlapFilhaList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For Ano
	    partial void OnAnoChanging(Int16 value);
	    partial void OnAnoChanged();

	    private Int16 _Ano;

	    [DataMember(IsRequired = true, Name = "Ano", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "AnoPai", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];LookUpName[LookUpPivoGridOlapAno];LookUpTitle[Seleção de (AnoPai)];LookUpQuery[executeLookUpPivoGridOlapAno];LookUpFinalize[finalizeLookUpPivoGridOlapAno];LookUpDisplayColumns[{\"Ano\" : \"Ano\"}];LookUpColumns[{\"Ano\" : true}];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int16#Ano#true##0##Ano#0#true##::LookUpPivoGridOlapAno##false#false#PivoGridOlap_Ano#PivoGridOlap_Ano#LinxTraining001.BV.TestePivotGrid#IQueryable###true#false", EdmKey="")]
	    public Int16 Ano
	    {
	    	    get
	    	    {
	    	          return _Ano;
	    	    }
	    	    set
	    	    {
	    	          if (this._Ano != value)
	    	          {
	    	              this.ValidateProperty("Ano", value);
	    	              this.OnAnoChanging(value);
	    	              this.RaiseDataMemberChanging("Ano");
	    	              this._Ano = value;
	    	              this.RaiseDataMemberChanged("Ano");
	    	              this.OnAnoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For VlrBruto
	    partial void OnVlrBrutoChanging(Double value);
	    partial void OnVlrBrutoChanged();

	    private Double _VlrBruto;

	    [DataMember(IsRequired = true, Name = "VlrBruto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Vlr Bruto", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[20:2];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[true]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public Double VlrBruto
	    {
	    	    get
	    	    {
	    	          return _VlrBruto;
	    	    }
	    	    set
	    	    {
	    	          if (this._VlrBruto != value)
	    	          {
	    	              this.ValidateProperty("VlrBruto", value);
	    	              this.OnVlrBrutoChanging(value);
	    	              this.RaiseDataMemberChanging("VlrBruto");
	    	              this._VlrBruto = value;
	    	              this.RaiseDataMemberChanged("VlrBruto");
	    	              this.OnVlrBrutoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For EntityUniqueKey
	    partial void OnEntityUniqueKeyChanging(System.Guid value);
	    partial void OnEntityUniqueKeyChanged();

	    private System.Guid _entityUniqueKey;
	    [DataMember(Name = "EntityUniqueKey", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [RoundtripOriginal()]
	    [Editable(true)]
	    [Key()]
	    public System.Guid EntityUniqueKey
	    {
	    	    get
	    	    {
	    	          if (_entityUniqueKey.IsNullOrEmpty())
	    	             _entityUniqueKey =  System.Guid.NewGuid();
	    	          return _entityUniqueKey; 
	    	    }
	    	    set
	    	    {
	    	          if (this._entityUniqueKey != value)
	    	          {
	    	              this.ValidateProperty("EntityUniqueKey", value);
	    	              this.OnEntityUniqueKeyChanging(value);
	    	              this.RaiseDataMemberChanging("EntityUniqueKey");
	    	              this._entityUniqueKey = value;
	    	              this.RaiseDataMemberChanged("EntityUniqueKey");
	    	              this.OnEntityUniqueKeyChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<PivotGridOlapFilha> _PivotGridOlapFilhaList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_PivoGridOlap_PivotGridOlapFilha", "EntityUniqueKey", "EntityParentUniqueKey", IsForeignKey=false)]
	    [DataMember(Name = "PivotGridOlapFilhaList", EmitDefaultValue = true)]
	    public IEnumerable<PivotGridOlapFilha> PivotGridOlapFilhaList
	    {
	        get
	        {
	
	            if (this._PivotGridOlapFilhaList == null)
	            	this._PivotGridOlapFilhaList = new List<PivotGridOlapFilha>();
	
	            return this._PivotGridOlapFilhaList;
	        }
	        set
	        {
	            if (this._PivotGridOlapFilhaList != value)
	            {
	                this._PivotGridOlapFilhaList = value;
	                this.RaisePropertyChanged("PivotGridOlapFilhaList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		
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

		

	[LinxPublicationView(PrimaryKeys="PivotGridOlapFilha.EntityUniqueKey", IsUpdatable=false, EdmName="")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[PivotGridOlapFilha];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[true];OlapCatalogName[DEV-BI-Omni];CubeName[ATENDIMENTO];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#. as #Alias#];EdmEntityName[];EntityRelations[ ];EdmParentEntityName[];IsIQueryable[false]")]
		
	[DataContract(IsReference = false, Name = "PivotGridOlapFilha")]
	[Serializable()]
	public partial class PivotGridOlapFilha : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(TestePivotGridDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load PivoGridOlap
	         this.PivoGridOlap = (from r in context.GetPivoGridOlapByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For Ano
	    partial void OnAnoChanging(Int16 value);
	    partial void OnAnoChanged();

	    private Int16 _Ano;

	    [DataMember(IsRequired = true, Name = "Ano", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "AnoPai", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];LookUpName[LookUpEntityAdapter1Ano];LookUpTitle[Seleção de (AnoPai)];LookUpQuery[executeLookUpEntityAdapter1Ano];LookUpFinalize[finalizeLookUpEntityAdapter1Ano];LookUpDisplayColumns[{\"Ano\" : \"Ano\"}];LookUpColumns[{\"Ano\" : true}];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int16#Ano#true##0##Ano#0#true##::LookUpEntityAdapter1Ano##false#false#EntityAdapter1_Ano#EntityAdapter1_Ano#LinxTraining001.BV.TestePivotGrid#IQueryable###true#false", EdmKey="")]
	    public Int16 Ano
	    {
	    	    get
	    	    {
	    	          return _Ano;
	    	    }
	    	    set
	    	    {
	    	          if (this._Ano != value)
	    	          {
	    	              this.ValidateProperty("Ano", value);
	    	              this.OnAnoChanging(value);
	    	              this.RaiseDataMemberChanging("Ano");
	    	              this._Ano = value;
	    	              this.RaiseDataMemberChanged("Ano");
	    	              this.OnAnoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For VlrBruto
	    partial void OnVlrBrutoChanging(Double value);
	    partial void OnVlrBrutoChanged();

	    private Double _VlrBruto;

	    [DataMember(IsRequired = true, Name = "VlrBruto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Vlr Bruto", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[20:2];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[true]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public Double VlrBruto
	    {
	    	    get
	    	    {
	    	          return _VlrBruto;
	    	    }
	    	    set
	    	    {
	    	          if (this._VlrBruto != value)
	    	          {
	    	              this.ValidateProperty("VlrBruto", value);
	    	              this.OnVlrBrutoChanging(value);
	    	              this.RaiseDataMemberChanging("VlrBruto");
	    	              this._VlrBruto = value;
	    	              this.RaiseDataMemberChanged("VlrBruto");
	    	              this.OnVlrBrutoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For VlrPago
	    partial void OnVlrPagoChanging(Double value);
	    partial void OnVlrPagoChanged();

	    private Double _VlrPago;

	    [DataMember(IsRequired = true, Name = "VlrPago", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Vlr Pago", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[20:2];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[true]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public Double VlrPago
	    {
	    	    get
	    	    {
	    	          return _VlrPago;
	    	    }
	    	    set
	    	    {
	    	          if (this._VlrPago != value)
	    	          {
	    	              this.ValidateProperty("VlrPago", value);
	    	              this.OnVlrPagoChanging(value);
	    	              this.RaiseDataMemberChanging("VlrPago");
	    	              this._VlrPago = value;
	    	              this.RaiseDataMemberChanged("VlrPago");
	    	              this.OnVlrPagoChanged();
	    	          }
	    	    }
	    }
	    [DataMember(Name = "EntityParentUniqueKey", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [RoundtripOriginal()]
	    [Editable(true)]
	    public System.Guid EntityParentUniqueKey { get; set; }
	    //Extensibility Partial Method Definitions For EntityUniqueKey
	    partial void OnEntityUniqueKeyChanging(System.Guid value);
	    partial void OnEntityUniqueKeyChanged();

	    private System.Guid _entityUniqueKey;
	    [DataMember(Name = "EntityUniqueKey", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [RoundtripOriginal()]
	    [Editable(true)]
	    [Key()]
	    public System.Guid EntityUniqueKey
	    {
	    	    get
	    	    {
	    	          if (_entityUniqueKey.IsNullOrEmpty())
	    	             _entityUniqueKey =  System.Guid.NewGuid();
	    	          return _entityUniqueKey; 
	    	    }
	    	    set
	    	    {
	    	          if (this._entityUniqueKey != value)
	    	          {
	    	              this.ValidateProperty("EntityUniqueKey", value);
	    	              this.OnEntityUniqueKeyChanging(value);
	    	              this.RaiseDataMemberChanging("EntityUniqueKey");
	    	              this._entityUniqueKey = value;
	    	              this.RaiseDataMemberChanged("EntityUniqueKey");
	    	              this.OnEntityUniqueKeyChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private PivoGridOlap _PivoGridOlap;
	    [DataMember(Name = "PivoGridOlap", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_PivoGridOlap_PivotGridOlapFilha", "EntityParentUniqueKey", "EntityUniqueKey", IsForeignKey=true)]
	    public PivoGridOlap PivoGridOlap
	    {
	        get
	        {
	            return this._PivoGridOlap;
	        }
	        set
	        {
	            if (this._PivoGridOlap != value)
	            {
	                this._PivoGridOlap = value;
	                this.RaisePropertyChanged("PivoGridOlapList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		
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
	[DomainIdentifier("ProcessorOverviewTestePivotGridDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class TestePivotGridDomainService : DomainService, IDataServiceContext 
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

		
	    public TestePivotGridDomainService() : this("", null, null){ }
	    public TestePivotGridDomainService(string connectionString) : this(connectionString, null, null) { }
	    public TestePivotGridDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public TestePivotGridDomainService(object dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public TestePivotGridDomainService(string connectionString, object dataContext, Dictionary<string, string> headers) : base() 
	    { 
	    	this.connectionString = connectionString;
	    	this.Headers = headers;
	    	 


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
	    public void AddCustomChanges(Entity changedEntity, Entity originalEntity, ChangeOperation operation)
	    {
	
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
 	        var _PivoGridOlapElements = changeSet.ChangeSetEntries.Where(e => e.Entity is PivoGridOlap && e.Entity.GetType().Name == "PivoGridOlap" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _PivoGridOlapElements)
 	           if (((PivoGridOlap)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 
 	        //Remove inconsistent details
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is PivotGridOlapFilha && e.Entity.GetType().Name == "PivotGridOlapFilha" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
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
	        transactionScope = null;
	        OnTransactingChanges(this.ChangeSet);
	        bool result = base.PersistChangeSet();
	        
	        return result;
	    }
	
	    #endregion Transaction Control.
		


	    #region Get OLAP Definitions.
	
	
		    
    [Ignore()]
    public IEnumerable<PivoGridOlap> GetOlapPivoGridOlap(List<EntitySearch> entitySearchList)
    {
       List<MDXField> fieldsMap = new List<MDXField>();
       fieldsMap.Add(new MDXField("Ano", "[DATAS].[ANO].[ANO]", false));
       fieldsMap.Add(new MDXField("VlrBruto", "[Measures].[VLR_BRUTO]", true));
       var fields = entitySearchList.SelectMany(e => e.Expressions).Where(ex => ex.Name == "Field" && ((string)ex.Value ?? "").StartsWith("(PEsp)")).Select(s => ((string)s.Value).Right("(PEsp)"));
       fields.Foreach(i => { fieldsMap.Add(new MDXField(i, i, i.Contains("[Measures]"))); });
       entitySearchList.ForEach(e => e.Expressions.ToList().ForEach(f => { if (f.Name == "Field" && ((string)f.Value).Contains("(PEsp)")) f.Value = ((string)f.Value).Replace("(PEsp)", ""); }));
       //Verify valid properties for LINQ
       string[] validProperties = (entitySearchList == null ? new string[] { } : EntitySearch.GetValidProperties(entitySearchList));
    
       validProperties = EntitySearch.GetLinqValidProperties(validProperties, fieldsMap.ToDictionary(f => f.Name, f => f.MDX));
       MDXQueryFilterBuilder builder = new MDXQueryFilterBuilder(fieldsMap);
       builder.Conditions(entitySearchList);
       string connString = Linx.Business.Tools.CacheAccessHelper.GetConnectionString("DEV-BI-Omni");
       if (connString == "name=DEV-BI-Omni") connString = Linx.Tools.ConnectionManager.GetConnectionString("DEV-BI-Omni");
       using (Microsoft.AnalysisServices.AdomdClient.AdomdConnection connection = new Microsoft.AnalysisServices.AdomdClient.AdomdConnection(connString))
       {
           string mdxScript = (new MDXHelper("ATENDIMENTO"))
              .Measures("[Measures].[VLR_BRUTO]|VlrBruto")
              .Rows("[DATAS].[ANO].[ANO]")
              .SetIdLinxDimensions("")
              .SetIdGpeconDimensions("")
              .SetIdBandeiraRedeDimensions("")
              .SetMeasuresDimensions("")
              .Where(builder).FilterMetaData(validProperties)
              .SubqueryFilter("")
              .SetIdGpEcon(CurrentIdGpEcon())
              .SetIdLinx(CurrentIdLinx("DEV-BI-Omni"))
              .GetCommand(new MDXQuerySettings(){ NonEmptyColumns = true, NonEmptyRows = true});
           
           var command = connection.CreateCommand();
           command.Properties.Add("DbpropMsmdFlattened2", true);
           command.CommandText = mdxScript;
           connection.Open();
           IEnumerable<PivoGridOlap> result = null;
           using (var reader = command.ExecuteReader())
           {
               List<string> columnInReader = new List<string>();
               var dt = reader.GetSchemaTable();
               foreach (DataRow row in dt.Rows) columnInReader.Add(row["ColumnName"].ToString());
               result = reader.Select(r => new PivoGridOlap {
               Ano = !columnInReader.Contains("[DATAS].[ANO].[ANO].[MEMBER_CAPTION]") || (validProperties.Length > 0 && !validProperties.Contains("[DATAS].[ANO].[ANO]")) || r["[DATAS].[ANO].[ANO].[MEMBER_CAPTION]"] is DBNull || r["[DATAS].[ANO].[ANO].[MEMBER_CAPTION]"] == null ? default(Int16) : Int16.Parse((string)r["[DATAS].[ANO].[ANO].[MEMBER_CAPTION]"])
               , VlrBruto = !columnInReader.Contains("[Measures].[VlrBruto]") || (validProperties.Length > 0 && !validProperties.Contains("[Measures].[VLR_BRUTO]"))  || r["[Measures].[VlrBruto]"] is DBNull || r["[Measures].[VlrBruto]"] == null ? default(Double) : System.Convert.ToDouble(r["[Measures].[VlrBruto]"]).GetValue()
               }).ToList();
           }
           return result;
       }
    }


		
		    
    [Ignore()]
    public IEnumerable<PivotGridOlapFilha> GetOlapPivotGridOlapFilha(List<EntitySearch> entitySearchList)
    {
       List<MDXField> fieldsMap = new List<MDXField>();
       fieldsMap.Add(new MDXField("Ano", "[DATAS].[ANO].[ANO]", false));
       fieldsMap.Add(new MDXField("VlrBruto", "[Measures].[VLR_BRUTO]", true));
       fieldsMap.Add(new MDXField("VlrPago", "[Measures].[VLR_PAGO]", true));
       var fields = entitySearchList.SelectMany(e => e.Expressions).Where(ex => ex.Name == "Field" && ((string)ex.Value ?? "").StartsWith("(PEsp)")).Select(s => ((string)s.Value).Right("(PEsp)"));
       fields.Foreach(i => { fieldsMap.Add(new MDXField(i, i, i.Contains("[Measures]"))); });
       entitySearchList.ForEach(e => e.Expressions.ToList().ForEach(f => { if (f.Name == "Field" && ((string)f.Value).Contains("(PEsp)")) f.Value = ((string)f.Value).Replace("(PEsp)", ""); }));
       //Verify valid properties for LINQ
       string[] validProperties = (entitySearchList == null ? new string[] { } : EntitySearch.GetValidProperties(entitySearchList));
    
       validProperties = EntitySearch.GetLinqValidProperties(validProperties, fieldsMap.ToDictionary(f => f.Name, f => f.MDX));
       MDXQueryFilterBuilder builder = new MDXQueryFilterBuilder(fieldsMap);
       builder.Conditions(entitySearchList);
       string connString = Linx.Business.Tools.CacheAccessHelper.GetConnectionString("DEV-BI-Omni");
       if (connString == "name=DEV-BI-Omni") connString = Linx.Tools.ConnectionManager.GetConnectionString("DEV-BI-Omni");
       using (Microsoft.AnalysisServices.AdomdClient.AdomdConnection connection = new Microsoft.AnalysisServices.AdomdClient.AdomdConnection(connString))
       {
           string mdxScript = (new MDXHelper("ATENDIMENTO"))
              .Measures("[Measures].[VLR_BRUTO]|VlrBruto","[Measures].[VLR_PAGO]|VlrPago")
              .Rows("[DATAS].[ANO].[ANO]")
              .SetIdLinxDimensions("")
              .SetIdGpeconDimensions("")
              .SetIdBandeiraRedeDimensions("")
              .SetMeasuresDimensions("")
              .Where(builder).FilterMetaData(validProperties)
              .SubqueryFilter("")
              .SetIdGpEcon(CurrentIdGpEcon())
              .SetIdLinx(CurrentIdLinx("DEV-BI-Omni"))
              .GetCommand(new MDXQuerySettings(){ NonEmptyColumns = true, NonEmptyRows = true});
           
           var command = connection.CreateCommand();
           command.Properties.Add("DbpropMsmdFlattened2", true);
           command.CommandText = mdxScript;
           connection.Open();
           IEnumerable<PivotGridOlapFilha> result = null;
           using (var reader = command.ExecuteReader())
           {
               List<string> columnInReader = new List<string>();
               var dt = reader.GetSchemaTable();
               foreach (DataRow row in dt.Rows) columnInReader.Add(row["ColumnName"].ToString());
               result = reader.Select(r => new PivotGridOlapFilha {
               Ano = !columnInReader.Contains("[DATAS].[ANO].[ANO].[MEMBER_CAPTION]") || (validProperties.Length > 0 && !validProperties.Contains("[DATAS].[ANO].[ANO]")) || r["[DATAS].[ANO].[ANO].[MEMBER_CAPTION]"] is DBNull || r["[DATAS].[ANO].[ANO].[MEMBER_CAPTION]"] == null ? default(Int16) : Int16.Parse((string)r["[DATAS].[ANO].[ANO].[MEMBER_CAPTION]"])
               , VlrBruto = !columnInReader.Contains("[Measures].[VlrBruto]") || (validProperties.Length > 0 && !validProperties.Contains("[Measures].[VLR_BRUTO]"))  || r["[Measures].[VlrBruto]"] is DBNull || r["[Measures].[VlrBruto]"] == null ? default(Double) : System.Convert.ToDouble(r["[Measures].[VlrBruto]"]).GetValue()
               , VlrPago = !columnInReader.Contains("[Measures].[VlrPago]") || (validProperties.Length > 0 && !validProperties.Contains("[Measures].[VLR_PAGO]"))  || r["[Measures].[VlrPago]"] is DBNull || r["[Measures].[VlrPago]"] == null ? default(Double) : System.Convert.ToDouble(r["[Measures].[VlrPago]"]).GetValue()
               }).ToList();
           }
           return result;
       }
    }


				
	
		    
    [Ignore()]
    public IEnumerable<LookUpPivoGridOlapAno> GetOlapLookUpPivoGridOlapAno(List<EntitySearch> entitySearchList)
    {
       List<MDXField> fieldsMap = new List<MDXField>();
       fieldsMap.Add(new MDXField("Ano", "[DATAS].[ANO].[ANO]", false));
       string[] validProperties = (entitySearchList == null ? new string[] {} : EntitySearch.GetValidProperties(entitySearchList));
       validProperties = EntitySearch.GetLinqValidProperties(validProperties, fieldsMap.ToDictionary(f => f.Name, f => f.MDX));
       MDXQueryFilterBuilder builder = new MDXQueryFilterBuilder(fieldsMap);
       builder.Conditions(entitySearchList);
       string connString = Linx.Business.Tools.CacheAccessHelper.GetConnectionString("DEV-BI-Omni");
       if (connString == "name=DEV-BI-Omni") connString = Linx.Tools.ConnectionManager.GetConnectionString("DEV-BI-Omni");
       using (Microsoft.AnalysisServices.AdomdClient.AdomdConnection connection = new Microsoft.AnalysisServices.AdomdClient.AdomdConnection(connString))
       {
           string mdxScript = (new MDXHelper("ATENDIMENTO"))
              .SetIdLinxDimensions("")
              .SetIdGpeconDimensions("")
              .SetIdBandeiraRedeDimensions("")
              .Rows("[DATAS].[ANO].[ANO]")
              .Where(builder).FilterMetaData(validProperties)
              .SubqueryFilter("")
              .SetIdGpEcon(CurrentIdGpEcon())
              .SetIdLinx(CurrentIdLinx("DEV-BI-Omni"))
              .GetCommand(new MDXQuerySettings(){ NonEmptyColumns = true, NonEmptyRows = true});
           
           var command = connection.CreateCommand();
           command.Properties.Add("DbpropMsmdFlattened2", true);
           command.CommandText = mdxScript;
           connection.Open();
           IEnumerable<LookUpPivoGridOlapAno> result = null;
           using (var reader = command.ExecuteReader())
           {
               List<string> columnInReader = new List<string>();
               var dt = reader.GetSchemaTable();
               foreach (DataRow row in dt.Rows) columnInReader.Add(row["ColumnName"].ToString());
               result = reader.Select(r => new LookUpPivoGridOlapAno {
               Ano = !columnInReader.Contains("[DATAS].[ANO].[ANO].[MEMBER_CAPTION]") || (validProperties.Length > 0 && !validProperties.Contains("[DATAS].[ANO].[ANO]")) || r["[DATAS].[ANO].[ANO].[MEMBER_CAPTION]"] is DBNull || r["[DATAS].[ANO].[ANO].[MEMBER_CAPTION]"] == null ? default(Int16) : Int16.Parse((string)r["[DATAS].[ANO].[ANO].[MEMBER_CAPTION]"])
               }).ToList();
           }
           return result;
       }
    }


		
		    
    [Ignore()]
    public IEnumerable<LookUpEntityAdapter1Ano> GetOlapLookUpEntityAdapter1Ano(List<EntitySearch> entitySearchList)
    {
       List<MDXField> fieldsMap = new List<MDXField>();
       fieldsMap.Add(new MDXField("Ano", "[DATAS].[ANO].[ANO]", false));
       string[] validProperties = (entitySearchList == null ? new string[] {} : EntitySearch.GetValidProperties(entitySearchList));
       validProperties = EntitySearch.GetLinqValidProperties(validProperties, fieldsMap.ToDictionary(f => f.Name, f => f.MDX));
       MDXQueryFilterBuilder builder = new MDXQueryFilterBuilder(fieldsMap);
       builder.Conditions(entitySearchList);
       string connString = Linx.Business.Tools.CacheAccessHelper.GetConnectionString("DEV-BI-Omni");
       if (connString == "name=DEV-BI-Omni") connString = Linx.Tools.ConnectionManager.GetConnectionString("DEV-BI-Omni");
       using (Microsoft.AnalysisServices.AdomdClient.AdomdConnection connection = new Microsoft.AnalysisServices.AdomdClient.AdomdConnection(connString))
       {
           string mdxScript = (new MDXHelper("ATENDIMENTO"))
              .SetIdLinxDimensions("")
              .SetIdGpeconDimensions("")
              .SetIdBandeiraRedeDimensions("")
              .Rows("[DATAS].[ANO].[ANO]")
              .Where(builder).FilterMetaData(validProperties)
              .SubqueryFilter("")
              .SetIdGpEcon(CurrentIdGpEcon())
              .SetIdLinx(CurrentIdLinx("DEV-BI-Omni"))
              .GetCommand(new MDXQuerySettings(){ NonEmptyColumns = true, NonEmptyRows = true});
           
           var command = connection.CreateCommand();
           command.Properties.Add("DbpropMsmdFlattened2", true);
           command.CommandText = mdxScript;
           connection.Open();
           IEnumerable<LookUpEntityAdapter1Ano> result = null;
           using (var reader = command.ExecuteReader())
           {
               List<string> columnInReader = new List<string>();
               var dt = reader.GetSchemaTable();
               foreach (DataRow row in dt.Rows) columnInReader.Add(row["ColumnName"].ToString());
               result = reader.Select(r => new LookUpEntityAdapter1Ano {
               Ano = !columnInReader.Contains("[DATAS].[ANO].[ANO].[MEMBER_CAPTION]") || (validProperties.Length > 0 && !validProperties.Contains("[DATAS].[ANO].[ANO]")) || r["[DATAS].[ANO].[ANO].[MEMBER_CAPTION]"] is DBNull || r["[DATAS].[ANO].[ANO].[MEMBER_CAPTION]"] == null ? default(Int16) : Int16.Parse((string)r["[DATAS].[ANO].[ANO].[MEMBER_CAPTION]"])
               }).ToList();
           }
           return result;
       }
    }


		
	    #endregion Get OLAP Definitions.


	    #region Get LookUp Definitions.
	
		
			
        [Ignore]
	    //Get All LookUpPivoGridOlapAno.
	    public IQueryable<LookUpPivoGridOlapAno> GetAllLookUpPivoGridOlapAno()
	    {
	        return this.GetLookUpPivoGridOlapAno(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpPivoGridOlapAno By EntitySearch.
	    public IQueryable<LookUpPivoGridOlapAno> GetLookUpPivoGridOlapAnoByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpPivoGridOlapAno(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpPivoGridOlapAno.
	    public IQueryable<LookUpPivoGridOlapAno> GetLookUpPivoGridOlapAno(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "PivoGridOlap_Ano" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpPivoGridOlapAno";
	        object propvalue = (propertyName.IsNullOrEmpty() || serializedPropertyValue.IsNullOrEmpty() ? null : SerializationManager<object>.StringToObject(serializedPropertyValue));
	        if (!propvalue.IsNullOrEmpty())
	        {
	        	if (entitySearch.Expressions.Count > 0)
	        		entitySearch.Expressions.Add(new EntitySearchExpression("Condition", "&&"));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Field", propertyName));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Operator", (propvalue != null && propvalue is string && ((string)propvalue).Contains("%") ? "Like" : "==")));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Value", propvalue));
	        }
	
			
	        List<EntitySearch> entitySearchList = new List<EntitySearch>();
	        entitySearchList.Add(entitySearch);

	        var query = this.GetOlapLookUpPivoGridOlapAno(entitySearchList).AsQueryable();
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpEntityAdapter1Ano.
	    public IQueryable<LookUpEntityAdapter1Ano> GetAllLookUpEntityAdapter1Ano()
	    {
	        return this.GetLookUpEntityAdapter1Ano(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpEntityAdapter1Ano By EntitySearch.
	    public IQueryable<LookUpEntityAdapter1Ano> GetLookUpEntityAdapter1AnoByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpEntityAdapter1Ano(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpEntityAdapter1Ano.
	    public IQueryable<LookUpEntityAdapter1Ano> GetLookUpEntityAdapter1Ano(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "EntityAdapter1_Ano" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpEntityAdapter1Ano";
	        object propvalue = (propertyName.IsNullOrEmpty() || serializedPropertyValue.IsNullOrEmpty() ? null : SerializationManager<object>.StringToObject(serializedPropertyValue));
	        if (!propvalue.IsNullOrEmpty())
	        {
	        	if (entitySearch.Expressions.Count > 0)
	        		entitySearch.Expressions.Add(new EntitySearchExpression("Condition", "&&"));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Field", propertyName));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Operator", (propvalue != null && propvalue is string && ((string)propvalue).Contains("%") ? "Like" : "==")));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Value", propvalue));
	        }
	
			
	        List<EntitySearch> entitySearchList = new List<EntitySearch>();
	        entitySearchList.Add(entitySearch);

	        var query = this.GetOlapLookUpEntityAdapter1Ano(entitySearchList).AsQueryable();
	
	        return query;

	    }
			
	    #endregion Get LookUp Definitions.
			

	    #region Get Meta Data.

	    [Ignore]
	    public List<BmMetaDataProperty> GetBmEntityProperties(string entityName, string parentDataPath)
	    {
		        var olapCatalog = ObjectExtension.GetFunctionalPointOfType(Type.GetType("LinxTraining001.BV.TestePivotGrid." + entityName), "OlapCatalogName");
            var cubeName =  ObjectExtension.GetFunctionalPointOfType(Type.GetType("LinxTraining001.BV.TestePivotGrid." + entityName), "CubeName");
	        string connString = Linx.Business.Tools.CacheAccessHelper.GetConnectionString(olapCatalog);
            if (connString == "name=" + olapCatalog) connString = Linx.Tools.ConnectionManager.GetConnectionString(olapCatalog);
            var olap = new OlapReader();
            if (parentDataPath.IsNullOrEmpty())
                return olap.GetOlapMetaDataProperty(connString, cubeName, entityName).OfType<BmMetaDataProperty>().Where(x => x.id != "Measures").ToList();
            else
                return olap.GetOlapMetaDataPropertyDetails(connString, cubeName, parentDataPath).OfType<BmMetaDataProperty>().Where(x => x.id != "Measures").ToList();
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
	
		

	        if (entityName.InList("LinxTraining001.BV.TestePivotGrid.PivoGridOlap"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "PivoGridOlap",
	        			NameSpace = "LinxTraining001.BV.TestePivotGrid",
	        			ParentClassName = null,	
	        			DisplayName = "PivoGridOlap",
	        			ClearMethodName = "ClearPivoGridOlap",
	        			QueryMethodName  = "GetPagedPivoGridOlap",	
	        			CountingMethodName  = "GetPivoGridOlap" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("LinxTraining001.BV.TestePivotGrid.PivoGridOlap"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("LinxTraining001.BV.TestePivotGrid.PivoGridOlap"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("LinxTraining001.BV.TestePivotGrid.PivoGridOlap", "LinxTraining001.BV.TestePivotGrid.PivotGridOlapFilha"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "PivotGridOlapFilha",
	        			NameSpace = "LinxTraining001.BV.TestePivotGrid",
	        			ParentClassName = "PivoGridOlap",	
	        			DisplayName = "PivotGridOlapFilha",
	        			ClearMethodName = "ClearPivotGridOlapFilha",
	        			QueryMethodName  = "GetPagedPivotGridOlapFilha",	
	        			CountingMethodName  = "GetPivotGridOlapFilha" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("LinxTraining001.BV.TestePivotGrid.PivotGridOlapFilha"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("LinxTraining001.BV.TestePivotGrid.PivotGridOlapFilha"), forceAll: forceAll)
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


             return new string[] { "LinxTraining001_testePivotGridService", Linx.Tools.AssemblyHelper.ReadResourceContent("LinxTraining001.BV.ClientResources.testePivotGridService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	

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
	    //Clear PivoGridOlap.
	    public IEnumerable<PivoGridOlap> ClearPivoGridOlap()
	    {
	        List<PivoGridOlap> result = new List<PivoGridOlap>();
	        result.Add(new PivoGridOlap());	
			
	        result[0].PivotGridOlapFilhaList = new List<PivotGridOlapFilha>();
	        ((List<PivotGridOlapFilha>)result[0].PivotGridOlapFilhaList).Add(new PivotGridOlapFilha());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear PivotGridOlapFilha.
	    public IEnumerable<PivotGridOlapFilha> ClearPivotGridOlapFilha()
	    {
	        List<PivotGridOlapFilha> result = new List<PivotGridOlapFilha>();
	        result.Add(new PivotGridOlapFilha());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    [PivoGridOlapQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PivoGridOlap.
	    public IEnumerable<PivoGridOlap> GetPivoGridOlap()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPivoGridOlap")))
 	        {
 	             AuthorizationResult authorizationResult = (new PivoGridOlapQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        IEnumerable<PivoGridOlap> result = this.GetOlapPivoGridOlap(null);
	  	
	
	        	

	
	        return result;
	    }
			
	
	    [PivotGridOlapFilhaQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PivotGridOlapFilha.
	    public IEnumerable<PivotGridOlapFilha> GetPivotGridOlapFilha()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPivotGridOlapFilha")))
 	        {
 	             AuthorizationResult authorizationResult = (new PivotGridOlapFilhaQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        IEnumerable<PivotGridOlapFilha> result = this.GetOlapPivotGridOlapFilha(null);
	  	
	
	        	

	
	        return result;
	    }
			
	
	    [PivoGridOlapQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PivoGridOlapNoAssociations.
	    public IEnumerable<PivoGridOlap> GetPivoGridOlapNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPivoGridOlapNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new PivoGridOlapQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        IEnumerable<PivoGridOlap> result = this.GetOlapPivoGridOlap(null);
	  	
	
	        	

	
	        return result;
	    }
			
	
	    [PivotGridOlapFilhaQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PivotGridOlapFilhaNoAssociations.
	    public IEnumerable<PivotGridOlapFilha> GetPivotGridOlapFilhaNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPivotGridOlapFilhaNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new PivotGridOlapFilhaQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        IEnumerable<PivotGridOlapFilha> result = this.GetOlapPivotGridOlapFilha(null);
	  	
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
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
	    //Get PivoGridOlap By EntitySearchId.
	    public IEnumerable<PivoGridOlap> GetPivoGridOlapByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetPivoGridOlapByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get PivotGridOlapFilha By EntitySearchId.
	    public IEnumerable<PivotGridOlapFilha> GetPivotGridOlapFilhaByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetPivotGridOlapFilhaByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get PivoGridOlap By EntitySearchId.
	    public IEnumerable<PivoGridOlap> GetPivoGridOlapByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetPivoGridOlapByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get PivotGridOlapFilha By EntitySearchId.
	    public IEnumerable<PivotGridOlapFilha> GetPivotGridOlapFilhaByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetPivotGridOlapFilhaByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get PivoGridOlap By Example.
	    [Ignore]
	    public IEnumerable<PivoGridOlap> GetPivoGridOlapByExample(PivoGridOlap entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetPivoGridOlapByEntitySearch(queryAnalysis);
	    }
			
	    //Get PivotGridOlapFilha By Example.
	    [Ignore]
	    public IEnumerable<PivotGridOlapFilha> GetPivotGridOlapFilhaByExample(PivotGridOlapFilha entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetPivotGridOlapFilhaByEntitySearch(queryAnalysis);
	    }
			
	    //Get PivoGridOlap By Example.
	    [Ignore]
	    public IEnumerable<PivoGridOlap> GetPivoGridOlapByExampleNoAssociations(PivoGridOlap entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetPivoGridOlapByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get PivotGridOlapFilha By Example.
	    [Ignore]
	    public IEnumerable<PivotGridOlapFilha> GetPivotGridOlapFilhaByExampleNoAssociations(PivotGridOlapFilha entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetPivotGridOlapFilhaByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key






	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    [PivoGridOlapQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PivoGridOlapByEntitySearch.
	    public IEnumerable<PivoGridOlap> GetPivoGridOlapByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPivoGridOlapByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new PivoGridOlapQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<PivoGridOlap> result = this.GetOlapPivoGridOlap(entitySearchList);
	  	
	
	        	

	
	        return result;
	    }
			
	
	    [PivotGridOlapFilhaQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PivotGridOlapFilhaByEntitySearch.
	    public IEnumerable<PivotGridOlapFilha> GetPivotGridOlapFilhaByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPivotGridOlapFilhaByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new PivotGridOlapFilhaQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<PivotGridOlapFilha> result = this.GetOlapPivotGridOlapFilha(entitySearchList);
	  	
	
	        	

	
	        return result;
	    }
			
	
	    [PivoGridOlapQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PivoGridOlapByEntitySearchNoAssociations.
	    public IEnumerable<PivoGridOlap> GetPivoGridOlapByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPivoGridOlapByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new PivoGridOlapQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<PivoGridOlap> result = this.GetOlapPivoGridOlap(entitySearchList);
	  	
	
	        	

	
	        return result;
	    }
			
	
	    [PivotGridOlapFilhaQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PivotGridOlapFilhaByEntitySearchNoAssociations.
	    public IEnumerable<PivotGridOlapFilha> GetPivotGridOlapFilhaByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPivotGridOlapFilhaByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new PivotGridOlapFilhaQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<PivotGridOlapFilha> result = this.GetOlapPivotGridOlapFilha(entitySearchList);
	  	
	
	        	

	
	        return result;
	    }
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    [PivoGridOlapQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedPivoGridOlap.
	    public IEnumerable<PivoGridOlap> GetPagedPivoGridOlap(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedPivoGridOlap")))
 	        {
 	             AuthorizationResult authorizationResult = (new PivoGridOlapQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<PivoGridOlap> result = this.GetOlapPivoGridOlap(entitySearchList);
	  	
	
	        	

	
	        return result;
	    }
			
	
	    [PivotGridOlapFilhaQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedPivotGridOlapFilha.
	    public IEnumerable<PivotGridOlapFilha> GetPagedPivotGridOlapFilha(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedPivotGridOlapFilha")))
 	        {
 	             AuthorizationResult authorizationResult = (new PivotGridOlapFilhaQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<PivotGridOlapFilha> result = this.GetOlapPivotGridOlapFilha(entitySearchList);
	  	
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetPivoGridOlapCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
		
	    [Ignore]
	    public int GetPivotGridOlapFilhaCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    [PivoGridOlapUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update PivoGridOlap.
	    public void UpdatePivoGridOlap(PivoGridOlap entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdatePivoGridOlap")))
 	        {
 	             AuthorizationResult authorizationResult = (new PivoGridOlapUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	    }

	    [PivoGridOlapInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert PivoGridOlap.
	    public void InsertPivoGridOlap(PivoGridOlap entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertPivoGridOlap")))
 	        {
 	             AuthorizationResult authorizationResult = (new PivoGridOlapInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	    }

	    [PivoGridOlapDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete PivoGridOlap.
	    public void DeletePivoGridOlap(PivoGridOlap entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeletePivoGridOlap")))
 	        {
 	             AuthorizationResult authorizationResult = (new PivoGridOlapDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	    }
		
			
	    [PivotGridOlapFilhaUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update PivotGridOlapFilha.
	    public void UpdatePivotGridOlapFilha(PivotGridOlapFilha entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdatePivotGridOlapFilha")))
 	        {
 	             AuthorizationResult authorizationResult = (new PivotGridOlapFilhaUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	    }

	    [PivotGridOlapFilhaInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert PivotGridOlapFilha.
	    public void InsertPivotGridOlapFilha(PivotGridOlapFilha entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertPivotGridOlapFilha")))
 	        {
 	             AuthorizationResult authorizationResult = (new PivotGridOlapFilhaInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	    }

	    [PivotGridOlapFilhaDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete PivotGridOlapFilha.
	    public void DeletePivotGridOlapFilha(PivotGridOlapFilha entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeletePivotGridOlapFilha")))
 	        {
 	             AuthorizationResult authorizationResult = (new PivotGridOlapFilhaDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}