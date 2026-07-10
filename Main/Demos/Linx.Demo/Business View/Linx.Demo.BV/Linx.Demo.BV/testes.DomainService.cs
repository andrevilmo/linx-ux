					
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

namespace Linx.Demo.BV.testes
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="PRODUTO.ID_PRODUTO", IsUpdatable=false, EdmName="Linx.Demo.BM.BMDTesteFrame")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[Produto];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[];EdmEntityName[PRODUTO];EntityRelations[];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "Produto")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Demo.BV.testes.Produto")]
	public partial class Produto : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For DataCadastro
	    partial void OnDataCadastroChanging(System.Nullable<DateTime> value);
	    partial void OnDataCadastroChanged();

	    private System.Nullable<DateTime> _DataCadastro;

	    [DataMember(Name = "DataCadastro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Data Cadastro", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[PRODUTO.DATA_CADASTRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="PRODUTO.DATA_CADASTRO")]
	    public System.Nullable<DateTime> DataCadastro
	    {
	    	    get
	    	    {
	    	          return _DataCadastro;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataCadastro != value)
	    	          {
	    	              this.ValidateProperty("DataCadastro", value);
	    	              this.OnDataCadastroChanging(value);
	    	              this.RaiseDataMemberChanging("DataCadastro");
	    	              this._DataCadastro = value;
	    	              this.RaiseDataMemberChanged("DataCadastro");
	    	              this.OnDataCadastroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescProduto
	    partial void OnDescProdutoChanging(string value);
	    partial void OnDescProdutoChanged();

	    private string _DescProduto;

	    [DataMember(Name = "DescProduto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Desc Produto", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(100)]
	    [FunctionalPoint("Precision[100:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[PRODUTO.DESC_PRODUTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="PRODUTO.DESC_PRODUTO")]
	    public string DescProduto
	    {
	    	    get
	    	    {
	    	          return _DescProduto;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescProduto != value)
	    	          {
	    	              this.ValidateProperty("DescProduto", value);
	    	              this.OnDescProdutoChanging(value);
	    	              this.RaiseDataMemberChanging("DescProduto");
	    	              this._DescProduto = value;
	    	              this.RaiseDataMemberChanged("DescProduto");
	    	              this.OnDescProdutoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdProduto
	    partial void OnIdProdutoChanging(int value);
	    partial void OnIdProdutoChanged();

	    private int _IdProduto;

	    [DataMember(IsRequired = true, Name = "IdProduto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Produto", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[PRODUTO.ID_PRODUTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="PRODUTO.ID_PRODUTO")]
	    public int IdProduto
	    {
	    	    get
	    	    {
	    	          return _IdProduto;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdProduto != value)
	    	          {
	    	              this.ValidateProperty("IdProduto", value);
	    	              this.OnIdProdutoChanging(value);
	    	              this.RaiseDataMemberChanging("IdProduto");
	    	              this._IdProduto = value;
	    	              this.RaiseDataMemberChanged("IdProduto");
	    	              this.OnIdProdutoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NmProduto
	    partial void OnNmProdutoChanging(string value);
	    partial void OnNmProdutoChanged();

	    private string _NmProduto;

	    [DataMember(Name = "NmProduto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nm Produto", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[PRODUTO.NM_PRODUTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="PRODUTO.NM_PRODUTO")]
	    public string NmProduto
	    {
	    	    get
	    	    {
	    	          return _NmProduto;
	    	    }
	    	    set
	    	    {
	    	          if (this._NmProduto != value)
	    	          {
	    	              this.ValidateProperty("NmProduto", value);
	    	              this.OnNmProdutoChanging(value);
	    	              this.RaiseDataMemberChanging("NmProduto");
	    	              this._NmProduto = value;
	    	              this.RaiseDataMemberChanged("NmProduto");
	    	              this.OnNmProdutoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For TipoUnidade
	    partial void OnTipoUnidadeChanging(byte value);
	    partial void OnTipoUnidadeChanged();

	    private byte _TipoUnidade;

	    [DataMember(IsRequired = true, Name = "TipoUnidade", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo Unidade", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[PRODUTO.TIPO_UNIDADE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="PRODUTO.TIPO_UNIDADE")]
	    public byte TipoUnidade
	    {
	    	    get
	    	    {
	    	          return _TipoUnidade;
	    	    }
	    	    set
	    	    {
	    	          if (this._TipoUnidade != value)
	    	          {
	    	              this.ValidateProperty("TipoUnidade", value);
	    	              this.OnTipoUnidadeChanging(value);
	    	              this.RaiseDataMemberChanging("TipoUnidade");
	    	              this._TipoUnidade = value;
	    	              this.RaiseDataMemberChanged("TipoUnidade");
	    	              this.OnTipoUnidadeChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "BMDTesteFrame.PRODUTO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Demo.BM.PRODUTO), QualifiedEntitySetName = "BMDTesteFrame.PRODUTO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="PRODUTO.ID_PRODUTO", Source = "IdProduto", Target = "ID_PRODUTO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.PRODUTO", RelationPropertyName = "PRODUTO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="PRODUTO.NM_PRODUTO", Source = "NmProduto", Target = "NM_PRODUTO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.PRODUTO", RelationPropertyName = "PRODUTO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="PRODUTO.DESC_PRODUTO", Source = "DescProduto", Target = "DESC_PRODUTO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.PRODUTO", RelationPropertyName = "PRODUTO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="PRODUTO.TIPO_UNIDADE", Source = "TipoUnidade", Target = "TIPO_UNIDADE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.PRODUTO", RelationPropertyName = "PRODUTO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="PRODUTO.DATA_CADASTRO", Source = "DataCadastro", Target = "DATA_CADASTRO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.PRODUTO", RelationPropertyName = "PRODUTO" });

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
	[DomainIdentifier("ProcessorOverviewtestesDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class testesDomainService : DomainService, IDataServiceContext 
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

		
	    public testesDomainService() : this("", null, null) { }
	    public testesDomainService(string connectionString) : this(connectionString, null, null) { }
	    public testesDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public testesDomainService(Linx.Demo.BM.BMDTesteFrame dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public testesDomainService(string connectionString, Linx.Demo.BM.BMDTesteFrame dataContext, Dictionary<string, string> headers) : base() 
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
	
		

	        if (entityName.InList("Linx.Demo.BV.testes.Produto"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "Produto",
	        			NameSpace = "Linx.Demo.BV.testes",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "Produto",
	        			ClearMethodName = "ClearProduto",
	        			QueryMethodName  = "GetPagedProduto",	
	        			CountingMethodName  = "GetProduto" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Demo.BV.testes.Produto"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Demo.BV.testes.Produto"), forceAll: forceAll)
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

         		    return new string[] { "Demo_testesClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Demo.BV.ClientResources.testesClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Demo_testesService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Demo.BV.ClientResources.testesService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear Produto.
	    public IEnumerable<Produto> ClearProduto()
	    {
	        List<Produto> result = new List<Produto>();
	        result.Add(new Produto());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    [ProdutoQueryCustomAuthorizationAuto()]
	    [Query(HasSideEffects = false)]
	    //Get Produto.
	    public IQueryable<Produto> GetProduto()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetProduto")))
 	        {
 	             AuthorizationResult authorizationResult = (new ProdutoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<Produto> result = 
	            (from entity0 in this.DbContext.PRODUTO
	            
	            	
	            select new Produto()		
	            {
	            
                DataCadastro = entity0.DATA_CADASTRO
                , DescProduto = entity0.DESC_PRODUTO
                , IdProduto = entity0.ID_PRODUTO
                , NmProduto = entity0.NM_PRODUTO
                , TipoUnidade = entity0.TIPO_UNIDADE
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [ProdutoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get ProdutoNoAssociations.
	    public IQueryable<Produto> GetProdutoNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetProdutoNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new ProdutoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<Produto> result = 
	            (from entity0 in this.DbContext.PRODUTO
	            
	            	
	            select new Produto()		
	            {
	            
                DataCadastro = entity0.DATA_CADASTRO
                , DescProduto = entity0.DESC_PRODUTO
                , IdProduto = entity0.ID_PRODUTO
                , NmProduto = entity0.NM_PRODUTO
                , TipoUnidade = entity0.TIPO_UNIDADE
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	//Add filtering disabled property for PRODUTO
	    	string[] bmDisabledProdutoList = this.GetEDM().GetFilteringDisabledList("PRODUTO");
	    	if (bmDisabledProdutoList.Length > 0)
	    	{
	
	    		if (bmDisabledProdutoList.Contains("PRODUTO.DATA_CADASTRO"))
	    		{
	    			result.Add("Produto|DataCadastro");
	    			result.Add("Produto|PRODUTO.DATA_CADASTRO");
	    		}
	
	    		if (bmDisabledProdutoList.Contains("PRODUTO.DESC_PRODUTO"))
	    		{
	    			result.Add("Produto|DescProduto");
	    			result.Add("Produto|PRODUTO.DESC_PRODUTO");
	    		}
	
	    		if (bmDisabledProdutoList.Contains("PRODUTO.ID_PRODUTO"))
	    		{
	    			result.Add("Produto|IdProduto");
	    			result.Add("Produto|PRODUTO.ID_PRODUTO");
	    		}
	
	    		if (bmDisabledProdutoList.Contains("PRODUTO.NM_PRODUTO"))
	    		{
	    			result.Add("Produto|NmProduto");
	    			result.Add("Produto|PRODUTO.NM_PRODUTO");
	    		}
	
	    		if (bmDisabledProdutoList.Contains("PRODUTO.TIPO_UNIDADE"))
	    		{
	    			result.Add("Produto|TipoUnidade");
	    			result.Add("Produto|PRODUTO.TIPO_UNIDADE");
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
	    //Get Produto By EntitySearchId.
	    public IQueryable<Produto> GetProdutoByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetProdutoByEntitySearch(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get Produto By EntitySearchId.
	    public IQueryable<Produto> GetProdutoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetProdutoByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get Produto By Example.
	    [Ignore]
	    public IQueryable<Produto> GetProdutoByExample(Produto entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetProdutoByEntitySearch(queryAnalysis);
	    }
			
	    //Get Produto By Example.
	    [Ignore]
	    public IQueryable<Produto> GetProdutoByExampleNoAssociations(Produto entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetProdutoByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public Produto GetProdutoByKey(int idProduto)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("Produto");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdProduto"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idProduto));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetProdutoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    [ProdutoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get ProdutoByEntitySearch.
	    public IQueryable<Produto> GetProdutoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetProdutoByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new ProdutoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Produto));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<Produto> result = 
	            (from entity0 in this.DbContext.PRODUTO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new Produto()		
	            {
	            
                DataCadastro = entity0.DATA_CADASTRO
                , DescProduto = entity0.DESC_PRODUTO
                , IdProduto = entity0.ID_PRODUTO
                , NmProduto = entity0.NM_PRODUTO
                , TipoUnidade = entity0.TIPO_UNIDADE
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [ProdutoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get ProdutoByEntitySearchNoAssociations.
	    public IQueryable<Produto> GetProdutoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetProdutoByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new ProdutoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Produto));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<Produto> result = 
	            (from entity0 in this.DbContext.PRODUTO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new Produto()		
	            {
	            
                DataCadastro = entity0.DATA_CADASTRO
                , DescProduto = entity0.DESC_PRODUTO
                , IdProduto = entity0.ID_PRODUTO
                , NmProduto = entity0.NM_PRODUTO
                , TipoUnidade = entity0.TIPO_UNIDADE
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    [ProdutoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedProduto.
	    public IQueryable<Produto> GetPagedProduto(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedProduto")))
 	        {
 	             AuthorizationResult authorizationResult = (new ProdutoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Produto));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<Produto> result = 
	            (from entity0 in this.DbContext.PRODUTO.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_PRODUTO ascending
	            
	            	
	            select new Produto()		
	            {
	            
                DataCadastro = entity0.DATA_CADASTRO
                , DescProduto = entity0.DESC_PRODUTO
                , IdProduto = entity0.ID_PRODUTO
                , NmProduto = entity0.NM_PRODUTO
                , TipoUnidade = entity0.TIPO_UNIDADE
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetProdutoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Produto));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.PRODUTO.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    [ProdutoUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update Produto.
	    public void UpdateProduto(Produto entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateProduto")))
 	        {
 	             AuthorizationResult authorizationResult = (new ProdutoUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    [ProdutoInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert Produto.
	    public void InsertProduto(Produto entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertProduto")))
 	        {
 	             AuthorizationResult authorizationResult = (new ProdutoInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    [ProdutoDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete Produto.
	    public void DeleteProduto(Produto entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteProduto")))
 	        {
 	             AuthorizationResult authorizationResult = (new ProdutoDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
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