					
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
using Linx.Framework.Loja.BM;

namespace Linx.Framework.BV.LojaParametro
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="LJV_PARAMETRO.ID_PARAMETRO", IsUpdatable=false, EdmName="Linx.Framework.Loja.BM.ConectorPos")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[LjvParametro];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdParametro];ReadOnly[false];Entities[LJV_PARAMETRO:IdParametro];SubQueryInfo[];EdmEntityName[LJV_PARAMETRO];EntityRelations[];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "LjvParametro")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.LojaParametro.LjvParametro")]
	public partial class LjvParametro : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For DescParametro
	    partial void OnDescParametroChanging(System.String value);
	    partial void OnDescParametroChanged();

	    private System.String _DescParametro;

	    [DataMember(IsRequired = true, Name = "DescParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Desc Parametro", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_PARAMETRO.DESC_PARAMETRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_PARAMETRO.DESC_PARAMETRO")]
	    public System.String DescParametro
	    {
	    	    get
	    	    {
	    	          return _DescParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescParametro != value)
	    	          {
	    	              this.ValidateProperty("DescParametro", value);
	    	              this.OnDescParametroChanging(value);
	    	              this.RaiseDataMemberChanging("DescParametro");
	    	              this._DescParametro = value;
	    	              this.RaiseDataMemberChanged("DescParametro");
	    	              this.OnDescParametroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdParametro
	    partial void OnIdParametroChanging(Int32 value);
	    partial void OnIdParametroChanged();

	    private Int32 _IdParametro;

	    [DataMember(IsRequired = true, Name = "IdParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Parametro", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_PARAMETRO.ID_PARAMETRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_PARAMETRO.ID_PARAMETRO")]
	    public Int32 IdParametro
	    {
	    	    get
	    	    {
	    	          return _IdParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdParametro != value)
	    	          {
	    	              this.ValidateProperty("IdParametro", value);
	    	              this.OnIdParametroChanging(value);
	    	              this.RaiseDataMemberChanging("IdParametro");
	    	              this._IdParametro = value;
	    	              this.RaiseDataMemberChanged("IdParametro");
	    	              this.OnIdParametroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxDatatypeParametro
	    partial void OnLxDatatypeParametroChanging(Byte value);
	    partial void OnLxDatatypeParametroChanged();

	    private Byte _LxDatatypeParametro;

	    [DataMember(IsRequired = true, Name = "LxDatatypeParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Lx Datatype Parametro", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_PARAMETRO.LX_DATATYPE_PARAMETRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_PARAMETRO.LX_DATATYPE_PARAMETRO")]
	    public Byte LxDatatypeParametro
	    {
	    	    get
	    	    {
	    	          return _LxDatatypeParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxDatatypeParametro != value)
	    	          {
	    	              this.ValidateProperty("LxDatatypeParametro", value);
	    	              this.OnLxDatatypeParametroChanging(value);
	    	              this.RaiseDataMemberChanging("LxDatatypeParametro");
	    	              this._LxDatatypeParametro = value;
	    	              this.RaiseDataMemberChanged("LxDatatypeParametro");
	    	              this.OnLxDatatypeParametroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For TituloParametro
	    partial void OnTituloParametroChanging(System.String value);
	    partial void OnTituloParametroChanged();

	    private System.String _TituloParametro;

	    [DataMember(IsRequired = true, Name = "TituloParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Titulo Parametro", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_PARAMETRO.TITULO_PARAMETRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_PARAMETRO.TITULO_PARAMETRO")]
	    public System.String TituloParametro
	    {
	    	    get
	    	    {
	    	          return _TituloParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._TituloParametro != value)
	    	          {
	    	              this.ValidateProperty("TituloParametro", value);
	    	              this.OnTituloParametroChanging(value);
	    	              this.RaiseDataMemberChanging("TituloParametro");
	    	              this._TituloParametro = value;
	    	              this.RaiseDataMemberChanged("TituloParametro");
	    	              this.OnTituloParametroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ValorParametro
	    partial void OnValorParametroChanging(System.String value);
	    partial void OnValorParametroChanged();

	    private System.String _ValorParametro;

	    [DataMember(Name = "ValorParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Valor Parametro", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_PARAMETRO.VALOR_PARAMETRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_PARAMETRO.VALOR_PARAMETRO")]
	    public System.String ValorParametro
	    {
	    	    get
	    	    {
	    	          return _ValorParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._ValorParametro != value)
	    	          {
	    	              this.ValidateProperty("ValorParametro", value);
	    	              this.OnValorParametroChanging(value);
	    	              this.RaiseDataMemberChanging("ValorParametro");
	    	              this._ValorParametro = value;
	    	              this.RaiseDataMemberChanged("ValorParametro");
	    	              this.OnValorParametroChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIdParametro;
	    [DataMember(Name = "TemporaryIdParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Parametro (Tmp)", Description="Temporary Key", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdParametro
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdParametro.IsNullOrEmpty())
	    	                this._TemporaryIdParametro = this._IdParametro;
	    	          return this._TemporaryIdParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdParametro != value)
	    	              this._TemporaryIdParametro = value;
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ConectorPos.LJV_PARAMETRO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Loja.BM.LJV_PARAMETRO), QualifiedEntitySetName = "ConectorPos.LJV_PARAMETRO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_PARAMETRO.ID_PARAMETRO", Source = "IdParametro", Target = "ID_PARAMETRO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_PARAMETRO", RelationPropertyName = "LJV_PARAMETRO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_PARAMETRO.DESC_PARAMETRO", Source = "DescParametro", Target = "DESC_PARAMETRO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_PARAMETRO", RelationPropertyName = "LJV_PARAMETRO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_PARAMETRO.VALOR_PARAMETRO", Source = "ValorParametro", Target = "VALOR_PARAMETRO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_PARAMETRO", RelationPropertyName = "LJV_PARAMETRO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_PARAMETRO.TITULO_PARAMETRO", Source = "TituloParametro", Target = "TITULO_PARAMETRO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_PARAMETRO", RelationPropertyName = "LJV_PARAMETRO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_PARAMETRO.LX_DATATYPE_PARAMETRO", Source = "LxDatatypeParametro", Target = "LX_DATATYPE_PARAMETRO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_PARAMETRO", RelationPropertyName = "LJV_PARAMETRO" });

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
	[DomainIdentifier("ProcessorOverviewLojaParametroDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class LojaParametroDomainService : DomainService, IDataServiceContext 
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
	
	    private Linx.Framework.Loja.BM.ConectorPos _dbContext;
	    protected Linx.Framework.Loja.BM.ConectorPos DbContext 
	    { 
	    	get 
	    	{
	        	if (this._dbContext == null)
	        	{
	        		this._dbContext = new Linx.Framework.Loja.BM.ConectorPos(connectionString, this.Headers);
	        		((System.Data.Entity.Infrastructure.IObjectContextAdapter)this._dbContext).ObjectContext.CommandTimeout = 180;
	        		this._hasGpeconControl = (!(this._dbContext.IsUserMultiGpecon && this._dbContext.IdGpecon == this._dbContext.IdLinx) && this._dbContext.IdGpecon > 0);		
	        	}
	        	return this._dbContext;
	    	}
	    }

	    public string GetModelAssemblyName()
	    {
	        return typeof(Linx.Framework.Loja.BM.ConectorPos).Assembly.FullName;
	    }

	    public System.Data.Entity.Database Database
	    {
	        get { return this.DbContext.Database; }
	    }

		
	    public LojaParametroDomainService() : this("", null, null) { }
	    public LojaParametroDomainService(string connectionString) : this(connectionString, null, null) { }
	    public LojaParametroDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public LojaParametroDomainService(Linx.Framework.Loja.BM.ConectorPos dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public LojaParametroDomainService(string connectionString, Linx.Framework.Loja.BM.ConectorPos dataContext, Dictionary<string, string> headers) : base() 
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
	    public Linx.Framework.Loja.BM.ConectorPos GetEDM()
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
	
		

	        if (entityName.InList("Linx.Framework.BV.LojaParametro.LjvParametro"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "LjvParametro",
	        			NameSpace = "Linx.Framework.BV.LojaParametro",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "LjvParametro",
	        			ClearMethodName = "ClearLjvParametro",
	        			QueryMethodName  = "GetPagedLjvParametro",	
	        			CountingMethodName  = "GetLjvParametro" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.LojaParametro.LjvParametro"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.LojaParametro.LjvParametro"), forceAll: forceAll)
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

         		    return new string[] { "Framework_LojaParametroClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.LojaParametroClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Framework_lojaParametroService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.lojaParametroService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear LjvParametro.
	    public IEnumerable<LjvParametro> ClearLjvParametro()
	    {
	        List<LjvParametro> result = new List<LjvParametro>();
	        result.Add(new LjvParametro());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    
	    [Query(HasSideEffects = false)]
	    //Get LjvParametro.
	    public IQueryable<LjvParametro> GetLjvParametro()
	    {




		
	
	        
		
	        
	
	        IQueryable<LjvParametro> result = 
	            (from entity0 in this.DbContext.LJV_PARAMETRO
	            
	            	
	            select new LjvParametro()		
	            {
	            
                DescParametro = entity0.DESC_PARAMETRO
                , IdParametro = entity0.ID_PARAMETRO
                , LxDatatypeParametro = entity0.LX_DATATYPE_PARAMETRO
                , TituloParametro = entity0.TITULO_PARAMETRO
                , ValorParametro = entity0.VALOR_PARAMETRO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get LjvParametroNoAssociations.
	    public IQueryable<LjvParametro> GetLjvParametroNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<LjvParametro> result = 
	            (from entity0 in this.DbContext.LJV_PARAMETRO
	            
	            	
	            select new LjvParametro()		
	            {
	            
                DescParametro = entity0.DESC_PARAMETRO
                , IdParametro = entity0.ID_PARAMETRO
                , LxDatatypeParametro = entity0.LX_DATATYPE_PARAMETRO
                , TituloParametro = entity0.TITULO_PARAMETRO
                , ValorParametro = entity0.VALOR_PARAMETRO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	//Add filtering disabled property for LJV_PARAMETRO
	    	string[] bmDisabledLjvParametroList = this.GetEDM().GetFilteringDisabledList("LJV_PARAMETRO");
	    	if (bmDisabledLjvParametroList.Length > 0)
	    	{
	
	    		if (bmDisabledLjvParametroList.Contains("LJV_PARAMETRO.DESC_PARAMETRO"))
	    		{
	    			result.Add("LjvParametro|DescParametro");
	    			result.Add("LjvParametro|LJV_PARAMETRO.DESC_PARAMETRO");
	    		}
	
	    		if (bmDisabledLjvParametroList.Contains("LJV_PARAMETRO.ID_PARAMETRO"))
	    		{
	    			result.Add("LjvParametro|IdParametro");
	    			result.Add("LjvParametro|LJV_PARAMETRO.ID_PARAMETRO");
	    		}
	
	    		if (bmDisabledLjvParametroList.Contains("LJV_PARAMETRO.LX_DATATYPE_PARAMETRO"))
	    		{
	    			result.Add("LjvParametro|LxDatatypeParametro");
	    			result.Add("LjvParametro|LJV_PARAMETRO.LX_DATATYPE_PARAMETRO");
	    		}
	
	    		if (bmDisabledLjvParametroList.Contains("LJV_PARAMETRO.TITULO_PARAMETRO"))
	    		{
	    			result.Add("LjvParametro|TituloParametro");
	    			result.Add("LjvParametro|LJV_PARAMETRO.TITULO_PARAMETRO");
	    		}
	
	    		if (bmDisabledLjvParametroList.Contains("LJV_PARAMETRO.VALOR_PARAMETRO"))
	    		{
	    			result.Add("LjvParametro|ValorParametro");
	    			result.Add("LjvParametro|LJV_PARAMETRO.VALOR_PARAMETRO");
	    		}
	    	}
	    	return result;
	    }

	    #endregion Excluded Filters

	    #region Get By EntitySearchId Methods Definitions.
	
				
	    [Ignore]
	    //Get LjvParametro By EntitySearchId.
	    public IQueryable<LjvParametro> GetLjvParametroByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetLjvParametroByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get LjvParametro By EntitySearchId.
	    public IQueryable<LjvParametro> GetLjvParametroByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetLjvParametroByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get LjvParametro By Example.
	    [Ignore]
	    public IQueryable<LjvParametro> GetLjvParametroByExample(LjvParametro entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetLjvParametroByEntitySearch(queryAnalysis);
	    }
			
	    //Get LjvParametro By Example.
	    [Ignore]
	    public IQueryable<LjvParametro> GetLjvParametroByExampleNoAssociations(LjvParametro entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetLjvParametroByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public LjvParametro GetLjvParametroByKey(Int32 idParametro)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("LjvParametro");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdParametro"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idParametro));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetLjvParametroByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get LjvParametroByEntitySearch.
	    public IQueryable<LjvParametro> GetLjvParametroByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LjvParametro));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<LjvParametro> result = 
	            (from entity0 in this.DbContext.LJV_PARAMETRO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new LjvParametro()		
	            {
	            
                DescParametro = entity0.DESC_PARAMETRO
                , IdParametro = entity0.ID_PARAMETRO
                , LxDatatypeParametro = entity0.LX_DATATYPE_PARAMETRO
                , TituloParametro = entity0.TITULO_PARAMETRO
                , ValorParametro = entity0.VALOR_PARAMETRO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get LjvParametroByEntitySearchNoAssociations.
	    public IQueryable<LjvParametro> GetLjvParametroByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LjvParametro));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<LjvParametro> result = 
	            (from entity0 in this.DbContext.LJV_PARAMETRO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new LjvParametro()		
	            {
	            
                DescParametro = entity0.DESC_PARAMETRO
                , IdParametro = entity0.ID_PARAMETRO
                , LxDatatypeParametro = entity0.LX_DATATYPE_PARAMETRO
                , TituloParametro = entity0.TITULO_PARAMETRO
                , ValorParametro = entity0.VALOR_PARAMETRO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get PagedLjvParametro.
	    public IQueryable<LjvParametro> GetPagedLjvParametro(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LjvParametro));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<LjvParametro> result = 
	            (from entity0 in this.DbContext.LJV_PARAMETRO.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_PARAMETRO ascending
	            
	            	
	            select new LjvParametro()		
	            {
	            
                DescParametro = entity0.DESC_PARAMETRO
                , IdParametro = entity0.ID_PARAMETRO
                , LxDatatypeParametro = entity0.LX_DATATYPE_PARAMETRO
                , TituloParametro = entity0.TITULO_PARAMETRO
                , ValorParametro = entity0.VALOR_PARAMETRO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetLjvParametroCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LjvParametro));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.LJV_PARAMETRO.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    
	    [Update()]	
	    //Update LjvParametro.
	    public void UpdateLjvParametro(LjvParametro entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert LjvParametro.
	    public void InsertLjvParametro(LjvParametro entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete LjvParametro.
	    public void DeleteLjvParametro(LjvParametro entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}