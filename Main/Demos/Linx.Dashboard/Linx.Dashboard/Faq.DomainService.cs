					
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

namespace Linx.Dashboard.Faq
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="Form_BV.EntityUniqueKey", IsUpdatable=false, EdmName="")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[Form_BV];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[];EdmEntityName[];EntityRelations[ ];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "Form_BV")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Dashboard.Faq.Form_BV")]
	public partial class Form_BV : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For Titulo
	    partial void OnTituloChanging(string value);
	    partial void OnTituloChanged();

	    private string _Titulo;

	    [DataMember(IsRequired = true, Name = "Titulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Título", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Titulo
	    {
	    	    get
	    	    {
	    	          return _Titulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._Titulo != value)
	    	          {
	    	              this.ValidateProperty("Titulo", value);
	    	              this.OnTituloChanging(value);
	    	              this.RaiseDataMemberChanging("Titulo");
	    	              this._Titulo = value;
	    	              this.RaiseDataMemberChanged("Titulo");
	    	              this.OnTituloChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Duvida
	    partial void OnDuvidaChanging(string value);
	    partial void OnDuvidaChanged();

	    private string _Duvida;

	    [DataMember(IsRequired = true, Name = "Duvida", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Dúvida", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Duvida
	    {
	    	    get
	    	    {
	    	          return _Duvida;
	    	    }
	    	    set
	    	    {
	    	          if (this._Duvida != value)
	    	          {
	    	              this.ValidateProperty("Duvida", value);
	    	              this.OnDuvidaChanging(value);
	    	              this.RaiseDataMemberChanging("Duvida");
	    	              this._Duvida = value;
	    	              this.RaiseDataMemberChanged("Duvida");
	    	              this.OnDuvidaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Empresa
	    partial void OnEmpresaChanging(string value);
	    partial void OnEmpresaChanged();

	    private string _Empresa;

	    [DataMember(IsRequired = true, Name = "Empresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Empresa", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Empresa
	    {
	    	    get
	    	    {
	    	          return _Empresa;
	    	    }
	    	    set
	    	    {
	    	          if (this._Empresa != value)
	    	          {
	    	              this.ValidateProperty("Empresa", value);
	    	              this.OnEmpresaChanging(value);
	    	              this.RaiseDataMemberChanging("Empresa");
	    	              this._Empresa = value;
	    	              this.RaiseDataMemberChanged("Empresa");
	    	              this.OnEmpresaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Nome
	    partial void OnNomeChanging(string value);
	    partial void OnNomeChanged();

	    private string _Nome;

	    [DataMember(IsRequired = true, Name = "Nome", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Nome
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
	    //Extensibility Partial Method Definitions For Email
	    partial void OnEmailChanging(string value);
	    partial void OnEmailChanged();

	    private string _Email;

	    [DataMember(IsRequired = true, Name = "Email", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Email", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Email
	    {
	    	    get
	    	    {
	    	          return _Email;
	    	    }
	    	    set
	    	    {
	    	          if (this._Email != value)
	    	          {
	    	              this.ValidateProperty("Email", value);
	    	              this.OnEmailChanging(value);
	    	              this.RaiseDataMemberChanging("Email");
	    	              this._Email = value;
	    	              this.RaiseDataMemberChanged("Email");
	    	              this.OnEmailChanged();
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
	[DomainIdentifier("ProcessorOverviewFaqDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class FaqDomainService : DomainService, IDataServiceContext 
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

		
	    public FaqDomainService() : this("", null, null) { }
	    public FaqDomainService(string connectionString) : this(connectionString, null, null) { }
	    public FaqDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public FaqDomainService(object dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public FaqDomainService(string connectionString, object dataContext, Dictionary<string, string> headers) : base() 
	    { 
	    	this.connectionString = connectionString;
	    	this.Headers = headers;
	    	 


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
	
			
	
	    #endregion Get OLAP Definitions.


	    #region Get LookUp Definitions.
	
			
	    #endregion Get LookUp Definitions.
			

	    #region Get Meta Data.

	    [Ignore]
	    public List<BmMetaDataProperty> GetBmEntityProperties(string entityName, string parentDataPath)
	    {
		        var olapCatalog = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Dashboard.Faq." + entityName), "OlapCatalogName");
            var cubeName =  ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Dashboard.Faq." + entityName), "CubeName");
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
	
		

	        if (entityName.InList("Linx.Dashboard.Faq.Form_BV"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "Form_BV",
	        			NameSpace = "Linx.Dashboard.Faq",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "Form_BV",
	        			ClearMethodName = "ClearForm_BV",
	        			QueryMethodName  = "GetPagedForm_BV",	
	        			CountingMethodName  = "GetForm_BV" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Dashboard.Faq.Form_BV"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Dashboard.Faq.Form_BV"), forceAll: forceAll)
	        		});
	        }
	
            return result;
        }
	
	    [Ignore]
	    public string[] GetClientDomains(bool erp)
        {	
	    		if (erp)
	    		{

         		    return new string[] { "Dashboard_ClientErpDataDomainsFactory", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Dashboard.ClientResources.ClientErpDataDomainsFactory.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
	    		}
	    		else 
	    		{

         		    return new string[] { "Dashboard_MobileDataDomains", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Dashboard.ClientResources.MobileDataDomains.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
	    		}

        }

	    [Ignore]
	    public string[] GetClientService(bool erp)
        {	

	    		if (erp)
	    		{

         		    return new string[] { "Dashboard_FaqClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Dashboard.ClientResources.FaqClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Dashboard_faqService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Dashboard.ClientResources.faqService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear Form_BV.
	    public IEnumerable<Form_BV> ClearForm_BV()
	    {
	        List<Form_BV> result = new List<Form_BV>();
	        result.Add(new Form_BV());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    [Form_BVQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get Form_BV.
	    public IEnumerable<Form_BV> GetForm_BV()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetForm_BV")))
 	        {
 	             AuthorizationResult authorizationResult = (new Form_BVQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        IEnumerable<Form_BV> result = new List<Form_BV>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    [Form_BVQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get Form_BVNoAssociations.
	    public IEnumerable<Form_BV> GetForm_BVNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetForm_BVNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new Form_BVQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        IEnumerable<Form_BV> result = new List<Form_BV>();
	  	
	
	        	

	
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
	    //Get Form_BV By EntitySearchId.
	    public IEnumerable<Form_BV> GetForm_BVByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetForm_BVByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get Form_BV By EntitySearchId.
	    public IEnumerable<Form_BV> GetForm_BVByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetForm_BVByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get Form_BV By Example.
	    [Ignore]
	    public IEnumerable<Form_BV> GetForm_BVByExample(Form_BV entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetForm_BVByEntitySearch(queryAnalysis);
	    }
			
	    //Get Form_BV By Example.
	    [Ignore]
	    public IEnumerable<Form_BV> GetForm_BVByExampleNoAssociations(Form_BV entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetForm_BVByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key




	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    [Form_BVQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get Form_BVByEntitySearch.
	    public IEnumerable<Form_BV> GetForm_BVByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetForm_BVByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new Form_BVQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<Form_BV> result = new List<Form_BV>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    [Form_BVQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get Form_BVByEntitySearchNoAssociations.
	    public IEnumerable<Form_BV> GetForm_BVByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetForm_BVByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new Form_BVQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<Form_BV> result = new List<Form_BV>();
	  	
	
	        	

	
	        return result;
	    }
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    [Form_BVQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedForm_BV.
	    public IEnumerable<Form_BV> GetPagedForm_BV(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedForm_BV")))
 	        {
 	             AuthorizationResult authorizationResult = (new Form_BVQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<Form_BV> result = new List<Form_BV>();
	  	
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetForm_BVCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    [Form_BVUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update Form_BV.
	    public void UpdateForm_BV(Form_BV entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateForm_BV")))
 	        {
 	             AuthorizationResult authorizationResult = (new Form_BVUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	    }

	    [Form_BVInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert Form_BV.
	    public void InsertForm_BV(Form_BV entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertForm_BV")))
 	        {
 	             AuthorizationResult authorizationResult = (new Form_BVInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	    }

	    [Form_BVDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete Form_BV.
	    public void DeleteForm_BV(Form_BV entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteForm_BV")))
 	        {
 	             AuthorizationResult authorizationResult = (new Form_BVDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}