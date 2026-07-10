					
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
using Linx.Framework.ControleSistema.BM;

namespace Linx.Framework.BV.TransacaoTag
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_TRANSACAO_TAG.ID_TCS_TRANSACAO_TAG", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsTransacaoTag];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[];EdmEntityName[TCS_TRANSACAO_TAG];EntityRelations[];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsTransacaoTag")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.TransacaoTag.TcsTransacaoTag")]
	public partial class TcsTransacaoTag : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For IdTcsTransacaoTag
	    partial void OnIdTcsTransacaoTagChanging(int value);
	    partial void OnIdTcsTransacaoTagChanged();

	    private int _IdTcsTransacaoTag;

	    [DataMember(IsRequired = true, Name = "IdTcsTransacaoTag", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Transacao Tag", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_TAG.ID_TCS_TRANSACAO_TAG];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_TAG.ID_TCS_TRANSACAO_TAG")]
	    public int IdTcsTransacaoTag
	    {
	    	    get
	    	    {
	    	          return _IdTcsTransacaoTag;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsTransacaoTag != value)
	    	          {
	    	              this.ValidateProperty("IdTcsTransacaoTag", value);
	    	              this.OnIdTcsTransacaoTagChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsTransacaoTag");
	    	              this._IdTcsTransacaoTag = value;
	    	              this.RaiseDataMemberChanged("IdTcsTransacaoTag");
	    	              this.OnIdTcsTransacaoTagChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTransacao
	    partial void OnIdTransacaoChanging(long value);
	    partial void OnIdTransacaoChanged();

	    private long _IdTransacao;

	    [DataMember(IsRequired = true, Name = "IdTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Transacao", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsTransacao];LookUpTitle[Seleção de (Id Transacao)];LookUpQuery[executeLookUpTcsTransacao];LookUpFinalize[finalizeLookUpTcsTransacao];LookUpDisplayColumns[{\"IdTransacao\" : \"\", \"DescTransacao\" : \"Transação\", \"CodTransacao\" : \"Código\"}];LookUpColumns[{\"IdTransacao\" : false, \"DescTransacao\" : true, \"CodTransacao\" : true}];FilterDataKey[TCS_TRANSACAO_TAG.ID_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdTransacao#true##12###0#false##::LookUpTcsTransacao##true#false###Linx.Framework.BV.TransacaoTag#IQueryable###true#true", EdmKey="TCS_TRANSACAO_TAG.ID_TRANSACAO")]
	    public long IdTransacao
	    {
	    	    get
	    	    {
	    	          return _IdTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTransacao != value)
	    	          {
	    	              this.ValidateProperty("IdTransacao", value);
	    	              this.OnIdTransacaoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTransacao");
	    	              this._IdTransacao = value;
	    	              this.RaiseDataMemberChanged("IdTransacao");
	    	              this.OnIdTransacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Tag
	    partial void OnTagChanging(string value);
	    partial void OnTagChanged();

	    private string _Tag;

	    [DataMember(IsRequired = true, Name = "Tag", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tag", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(4000)]
	    [FunctionalPoint("Precision[4000:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_TAG.TAG];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_TAG.TAG")]
	    public string Tag
	    {
	    	    get
	    	    {
	    	          return _Tag;
	    	    }
	    	    set
	    	    {
	    	          if (this._Tag != value)
	    	          {
	    	              this.ValidateProperty("Tag", value);
	    	              this.OnTagChanging(value);
	    	              this.RaiseDataMemberChanging("Tag");
	    	              this._Tag = value;
	    	              this.RaiseDataMemberChanged("Tag");
	    	              this.OnTagChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescTransacao
	    partial void OnDescTransacaoChanging(string value);
	    partial void OnDescTransacaoChanged();

	    private string _DescTransacao;

	    [DataMember(IsRequired = true, Name = "DescTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Transação", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[''];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#DescTransacao#false##60:0##Transação#1#true##::LookUpTcsTransacao##true#false###Linx.Framework.BV.TransacaoTag#IQueryable###true#true", EdmKey="\"\"")]
	    public string DescTransacao
	    {
	    	    get
	    	    {
	    	          if (_DescTransacao != (GetDescTransacao()))
	    	             _DescTransacao =  GetDescTransacao();
	    	          return _DescTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescTransacao != value)
	    	          {
	    	              this.ValidateProperty("DescTransacao", value);
	    	              this.OnDescTransacaoChanging(value);
	    	              this.RaiseDataMemberChanging("DescTransacao");
	    	              this._DescTransacao = value;
	    	              this.RaiseDataMemberChanged("DescTransacao");
	    	              this.OnDescTransacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For CodTransacao
	    partial void OnCodTransacaoChanging(string value);
	    partial void OnCodTransacaoChanged();

	    private string _CodTransacao;

	    [DataMember(IsRequired = true, Name = "CodTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Código", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[''];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#CodTransacao#false##10:0##Código#2#true##::LookUpTcsTransacao##true#false###Linx.Framework.BV.TransacaoTag#IQueryable###true#true", EdmKey="\"\"")]
	    public string CodTransacao
	    {
	    	    get
	    	    {
	    	          if (_CodTransacao != (GetCodTransacao()))
	    	             _CodTransacao =  GetCodTransacao();
	    	          return _CodTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._CodTransacao != value)
	    	          {
	    	              this.ValidateProperty("CodTransacao", value);
	    	              this.OnCodTransacaoChanging(value);
	    	              this.RaiseDataMemberChanging("CodTransacao");
	    	              this._CodTransacao = value;
	    	              this.RaiseDataMemberChanged("CodTransacao");
	    	              this.OnCodTransacaoChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_TRANSACAO_TAG").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_TRANSACAO_TAG), QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_TAG" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_TAG.TAG", Source = "Tag", Target = "TAG", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_TAG", RelationPropertyName = "TCS_TRANSACAO_TAG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_TAG.ID_TRANSACAO", Source = "IdTransacao", Target = "ID_TRANSACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_TAG", RelationPropertyName = "TCS_TRANSACAO_TAG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_TAG.ID_TCS_TRANSACAO_TAG", Source = "IdTcsTransacaoTag", Target = "ID_TCS_TRANSACAO_TAG", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_TAG", RelationPropertyName = "TCS_TRANSACAO_TAG" });

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
	[DomainIdentifier("ProcessorOverviewTransacaoTagDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class TransacaoTagDomainService : DomainService, IDataServiceContext 
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
	
	    private Linx.Framework.ControleSistema.BM.ControleSistemaContext _dbContext;
	    protected Linx.Framework.ControleSistema.BM.ControleSistemaContext DbContext 
	    { 
	    	get 
	    	{
	        	if (this._dbContext == null)
	        	{
	        		this._dbContext = new Linx.Framework.ControleSistema.BM.ControleSistemaContext(connectionString, this.Headers);
	        		((System.Data.Entity.Infrastructure.IObjectContextAdapter)this._dbContext).ObjectContext.CommandTimeout = 180;
	        		this._hasGpeconControl = (!(this._dbContext.IsUserMultiGpecon && this._dbContext.IdGpecon == this._dbContext.IdLinx) && this._dbContext.IdGpecon > 0);		
	        	}
	        	return this._dbContext;
	    	}
	    }

	    public string GetModelAssemblyName()
	    {
	        return typeof(Linx.Framework.ControleSistema.BM.ControleSistemaContext).Assembly.FullName;
	    }

	    public System.Data.Entity.Database Database
	    {
	        get { return this.DbContext.Database; }
	    }

		
	    public TransacaoTagDomainService() : this("", null, null) { }
	    public TransacaoTagDomainService(string connectionString) : this(connectionString, null, null) { }
	    public TransacaoTagDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public TransacaoTagDomainService(Linx.Framework.ControleSistema.BM.ControleSistemaContext dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public TransacaoTagDomainService(string connectionString, Linx.Framework.ControleSistema.BM.ControleSistemaContext dataContext, Dictionary<string, string> headers) : base() 
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
	    public Linx.Framework.ControleSistema.BM.ControleSistemaContext GetEDM()
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
	    private int[] CurrentIdFiliais()
        {
	        if(SecurityHelper.IsNull()) return new int[0] ;
            var idFiliais = SecurityHelper.GetCurrentUserBrandInfo(this.Headers);
            return idFiliais ?? new int[0] ;
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
	
	
	        TcsTransacaoTag.OnSavedContextChanges(this, changeSet.ChangeSetEntries.Where(e => e.Entity is TcsTransacaoTag).ToArray());
    	
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
	    //Get All LookUpTcsTransacao.
	    public IQueryable<LookUpTcsTransacao> GetAllLookUpTcsTransacao()
	    {
	        return this.GetLookUpTcsTransacao(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsTransacao By EntitySearch.
	    public IQueryable<LookUpTcsTransacao> GetLookUpTcsTransacaoByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsTransacao(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsTransacao.
	    public IQueryable<LookUpTcsTransacao> GetLookUpTcsTransacao(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsTransacao";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsTransacao));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsTransacao> query =  null;
		
			
		
	        TcsTransacaoTag.OnLookingUpLookUpTcsTransacao(ref query, propertyName, entitySearch);
	
	
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
	
		

	        if (entityName.InList("Linx.Framework.BV.TransacaoTag.TcsTransacaoTag"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsTransacaoTag",
	        			NameSpace = "Linx.Framework.BV.TransacaoTag",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsTransacaoTag",
	        			ClearMethodName = "ClearTcsTransacaoTag",
	        			QueryMethodName  = "GetPagedTcsTransacaoTag",	
	        			CountingMethodName  = "GetTcsTransacaoTag" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.TransacaoTag.TcsTransacaoTag"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.TransacaoTag.TcsTransacaoTag"), forceAll: forceAll)
	        		});
	        }
	
            return result;
        }
	
	    [Ignore]
	    public string[] GetClientDomains(bool erp)
        {	
	    		if (erp)
	    		{

         		    return new string[] { "Framework_ClientErpDataDomainsFactory", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.ClientErpDataDomainsFactory.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
	    		}
	    		else 
	    		{

         		    return new string[] { "Framework_MobileDataDomains", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.MobileDataDomains.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
	    		}

        }

	    [Ignore]
	    public string[] GetClientService(bool erp)
        {	

	    		if (erp)
	    		{

         		    return new string[] { "Framework_TransacaoTagClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.TransacaoTagClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Framework_transacaoTagService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.transacaoTagService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear TcsTransacaoTag.
	    public IEnumerable<TcsTransacaoTag> ClearTcsTransacaoTag()
	    {
	        List<TcsTransacaoTag> result = new List<TcsTransacaoTag>();
	        result.Add(new TcsTransacaoTag());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsTransacaoTag.
	    public IQueryable<TcsTransacaoTag> GetTcsTransacaoTag()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoTag> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_TAG
	            
	            	
	            select new TcsTransacaoTag()		
	            {
	            
                IdTcsTransacaoTag = entity0.ID_TCS_TRANSACAO_TAG
                , IdTransacao = entity0.ID_TRANSACAO
                , Tag = entity0.TAG
                , DescTransacao = ""
                , CodTransacao = ""
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsTransacaoTagNoAssociations.
	    public IQueryable<TcsTransacaoTag> GetTcsTransacaoTagNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoTag> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_TAG
	            
	            	
	            select new TcsTransacaoTag()		
	            {
	            
                IdTcsTransacaoTag = entity0.ID_TCS_TRANSACAO_TAG
                , IdTransacao = entity0.ID_TRANSACAO
                , Tag = entity0.TAG
                , DescTransacao = ""
                , CodTransacao = ""
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	result.Add("TcsTransacaoTag|DescTransacao");
	    	result.Add("TcsTransacaoTag|''");
	    	result.Add("TcsTransacaoTag|CodTransacao");
	    	result.Add("TcsTransacaoTag|''");
	    	//Add filtering disabled property for TCS_TRANSACAO_TAG
	    	string[] bmDisabledTcsTransacaoTagList = this.GetEDM().GetFilteringDisabledList("TCS_TRANSACAO_TAG");
	    	if (bmDisabledTcsTransacaoTagList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsTransacaoTagList.Contains("TCS_TRANSACAO_TAG.ID_TCS_TRANSACAO_TAG"))
	    		{
	    			result.Add("TcsTransacaoTag|IdTcsTransacaoTag");
	    			result.Add("TcsTransacaoTag|TCS_TRANSACAO_TAG.ID_TCS_TRANSACAO_TAG");
	    		}
	
	    		if (bmDisabledTcsTransacaoTagList.Contains("TCS_TRANSACAO_TAG.ID_TRANSACAO"))
	    		{
	    			result.Add("TcsTransacaoTag|IdTransacao");
	    			result.Add("TcsTransacaoTag|TCS_TRANSACAO_TAG.ID_TRANSACAO");
	    		}
	
	    		if (bmDisabledTcsTransacaoTagList.Contains("TCS_TRANSACAO_TAG.TAG"))
	    		{
	    			result.Add("TcsTransacaoTag|Tag");
	    			result.Add("TcsTransacaoTag|TCS_TRANSACAO_TAG.TAG");
	    		}
	    	}
	    	return result;
	    }

	    #endregion Excluded Filters

	    #region Get By EntitySearchId Methods Definitions.
	
				
	    [Ignore]
	    //Get TcsTransacaoTag By EntitySearchId.
	    public IQueryable<TcsTransacaoTag> GetTcsTransacaoTagByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsTransacaoTagByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsTransacaoTag By EntitySearchId.
	    public IQueryable<TcsTransacaoTag> GetTcsTransacaoTagByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsTransacaoTagByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get TcsTransacaoTag By Example.
	    [Ignore]
	    public IQueryable<TcsTransacaoTag> GetTcsTransacaoTagByExample(TcsTransacaoTag entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsTransacaoTagByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsTransacaoTag By Example.
	    [Ignore]
	    public IQueryable<TcsTransacaoTag> GetTcsTransacaoTagByExampleNoAssociations(TcsTransacaoTag entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsTransacaoTagByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public TcsTransacaoTag GetTcsTransacaoTagByKey(int idTcsTransacaoTag)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsTransacaoTag");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsTransacaoTag"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsTransacaoTag));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsTransacaoTagByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get TcsTransacaoTagByEntitySearch.
	    public IQueryable<TcsTransacaoTag> GetTcsTransacaoTagByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacaoTag));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoTag> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_TAG.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsTransacaoTag()		
	            {
	            
                IdTcsTransacaoTag = entity0.ID_TCS_TRANSACAO_TAG
                , IdTransacao = entity0.ID_TRANSACAO
                , Tag = entity0.TAG
                , DescTransacao = ""
                , CodTransacao = ""
		
	            }
	            );
	
	        SetTcsTransacaoTagBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsTransacaoTagByEntitySearchNoAssociations.
	    public IQueryable<TcsTransacaoTag> GetTcsTransacaoTagByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacaoTag));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoTag> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_TAG.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsTransacaoTag()		
	            {
	            
                IdTcsTransacaoTag = entity0.ID_TCS_TRANSACAO_TAG
                , IdTransacao = entity0.ID_TRANSACAO
                , Tag = entity0.TAG
                , DescTransacao = ""
                , CodTransacao = ""
		
	            }
	            );
	
	        SetTcsTransacaoTagBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
	
	    [Ignore()]
	    private void SetTcsTransacaoTagBusinessFilter(ref IQueryable<TcsTransacaoTag> query, List<EntitySearch> entitySearchList)
	    {
	    		int idxElement;
	    		string operatorValue;
	    		object value;
	    		//Get query by functions
	    		if (entitySearchList.Count > 0)
	    		{
	    			foreach (EntitySearch search in entitySearchList.Where(e => e.EntityName == "TcsTransacaoTag"))
	    			{

	
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "DescTransacao" || e.Value.ToString() == "''")))
	    				{
	    					idxElement = search.Expressions.IndexOf(exp);
	    					if ((idxElement + 2) < search.Expressions.Count)
	    					{
	    						if (search.Expressions[idxElement + 1].Name == "Operator" && search.Expressions[idxElement + 2].Name == "Value")
	    						{
	    								operatorValue = search.Expressions[idxElement + 1].Value.ToString();
	    								value = search.Expressions[idxElement + 2].Value;
	    								if (value.IsNullOrEmpty())
												continue;

	
	    								//Adjust Like operator
	    								if (operatorValue == "Like")
	    								{
	    								    string enteredVal = value.ToString();
	    								    if (enteredVal.Right(1) == "%" && enteredVal.Left(1) == "%")
	    								    {
	    								        enteredVal = enteredVal.Replace("%", "");
	    								        operatorValue = "Contains";
	    								    }
	    								    else if (enteredVal.Left(1) == "%")
	    								    {
	    								        enteredVal = enteredVal.Replace("%", "");
	    								        operatorValue = "EndsWith";
	    								    }
	    								    else
	    								    {
	    								        enteredVal = enteredVal.Replace("%", "");
	    								        operatorValue = "StartsWith";
	    								    }
	    								    value = enteredVal;
	    								}

	
	    								switch (operatorValue)
	    								{
	    									case "==":
	    										string tmpDescTransacao1 = (string)value;
	    										query = from r in query where r.DescTransacao == tmpDescTransacao1 select r;
	    										break;
	    									case "!=":
	    										string tmpDescTransacao2 = (string)value;
	    										query = from r in query where r.DescTransacao != tmpDescTransacao2 select r;
	    										break;

	
	    									case "Contains":
	    										string tmpDescTransacao7 = (string)value;
	    									    query = from r in query where r.DescTransacao.Contains(tmpDescTransacao7) select r;
	    									    break;
	    									case "StartsWith":
	    										string tmpDescTransacao8 = (string)value;
	    									    query = from r in query where r.DescTransacao.StartsWith(tmpDescTransacao8) select r;
	    									    break;
	    									case "EndsWith":
	    										string tmpDescTransacao9 = (string)value;
	    									    query = from r in query where r.DescTransacao.EndsWith(tmpDescTransacao9) select r;
	    									    break;

	
	    									default:
	    										break;
	    								}                                
	    							}
	    						}
        					} 

    
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "CodTransacao" || e.Value.ToString() == "''")))
	    				{
	    					idxElement = search.Expressions.IndexOf(exp);
	    					if ((idxElement + 2) < search.Expressions.Count)
	    					{
	    						if (search.Expressions[idxElement + 1].Name == "Operator" && search.Expressions[idxElement + 2].Name == "Value")
	    						{
	    								operatorValue = search.Expressions[idxElement + 1].Value.ToString();
	    								value = search.Expressions[idxElement + 2].Value;
	    								if (value.IsNullOrEmpty())
												continue;

	
	    								//Adjust Like operator
	    								if (operatorValue == "Like")
	    								{
	    								    string enteredVal = value.ToString();
	    								    if (enteredVal.Right(1) == "%" && enteredVal.Left(1) == "%")
	    								    {
	    								        enteredVal = enteredVal.Replace("%", "");
	    								        operatorValue = "Contains";
	    								    }
	    								    else if (enteredVal.Left(1) == "%")
	    								    {
	    								        enteredVal = enteredVal.Replace("%", "");
	    								        operatorValue = "EndsWith";
	    								    }
	    								    else
	    								    {
	    								        enteredVal = enteredVal.Replace("%", "");
	    								        operatorValue = "StartsWith";
	    								    }
	    								    value = enteredVal;
	    								}

	
	    								switch (operatorValue)
	    								{
	    									case "==":
	    										string tmpCodTransacao1 = (string)value;
	    										query = from r in query where r.CodTransacao == tmpCodTransacao1 select r;
	    										break;
	    									case "!=":
	    										string tmpCodTransacao2 = (string)value;
	    										query = from r in query where r.CodTransacao != tmpCodTransacao2 select r;
	    										break;

	
	    									case "Contains":
	    										string tmpCodTransacao7 = (string)value;
	    									    query = from r in query where r.CodTransacao.Contains(tmpCodTransacao7) select r;
	    									    break;
	    									case "StartsWith":
	    										string tmpCodTransacao8 = (string)value;
	    									    query = from r in query where r.CodTransacao.StartsWith(tmpCodTransacao8) select r;
	    									    break;
	    									case "EndsWith":
	    										string tmpCodTransacao9 = (string)value;
	    									    query = from r in query where r.CodTransacao.EndsWith(tmpCodTransacao9) select r;
	    									    break;

	
	    									default:
	    										break;
	    								}                                
	    							}
	    						}
        					} 

    	
	    				}
	    			}   
	    }


	
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get PagedTcsTransacaoTag.
	    public IQueryable<TcsTransacaoTag> GetPagedTcsTransacaoTag(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacaoTag));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoTag> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_TAG.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_TCS_TRANSACAO_TAG ascending
	            
	            	
	            select new TcsTransacaoTag()		
	            {
	            
                IdTcsTransacaoTag = entity0.ID_TCS_TRANSACAO_TAG
                , IdTransacao = entity0.ID_TRANSACAO
                , Tag = entity0.TAG
                , DescTransacao = ""
                , CodTransacao = ""
		
	            }
	            ).Skip(skip).Take(take);
	
	        SetTcsTransacaoTagBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsTransacaoTagCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacaoTag));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_TRANSACAO_TAG.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    
	    [Update()]	
	    //Update TcsTransacaoTag.
	    public void UpdateTcsTransacaoTag(TcsTransacaoTag entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsTransacaoTag.
	    public void InsertTcsTransacaoTag(TcsTransacaoTag entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsTransacaoTag.
	    public void DeleteTcsTransacaoTag(TcsTransacaoTag entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}