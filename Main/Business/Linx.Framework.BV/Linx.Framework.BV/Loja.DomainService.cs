					
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

namespace Linx.Framework.BV.Loja
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="LJV_LOJA.ID_LOJA", IsUpdatable=false, EdmName="Linx.Framework.Loja.BM.ConectorPos")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[LjvLoja];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdLoja];ReadOnly[false];Entities[LJV_LOJA:IdLoja];SubQueryInfo[];EdmEntityName[LJV_LOJA];EntityRelations[];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "LjvLoja")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Loja.LjvLoja")]
	public partial class LjvLoja : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For CodBandeiraRede
	    partial void OnCodBandeiraRedeChanging(System.String value);
	    partial void OnCodBandeiraRedeChanged();

	    private System.String _CodBandeiraRede;

	    [DataMember(Name = "CodBandeiraRede", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cod Bandeira Rede", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_LOJA.COD_BANDEIRA_REDE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_LOJA.COD_BANDEIRA_REDE")]
	    public System.String CodBandeiraRede
	    {
	    	    get
	    	    {
	    	          return _CodBandeiraRede;
	    	    }
	    	    set
	    	    {
	    	          if (this._CodBandeiraRede != value)
	    	          {
	    	              this.ValidateProperty("CodBandeiraRede", value);
	    	              this.OnCodBandeiraRedeChanging(value);
	    	              this.RaiseDataMemberChanging("CodBandeiraRede");
	    	              this._CodBandeiraRede = value;
	    	              this.RaiseDataMemberChanged("CodBandeiraRede");
	    	              this.OnCodBandeiraRedeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For CodLoja
	    partial void OnCodLojaChanging(System.String value);
	    partial void OnCodLojaChanged();

	    private System.String _CodLoja;

	    [DataMember(IsRequired = true, Name = "CodLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cod Loja", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_LOJA.COD_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_LOJA.COD_LOJA")]
	    public System.String CodLoja
	    {
	    	    get
	    	    {
	    	          return _CodLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._CodLoja != value)
	    	          {
	    	              this.ValidateProperty("CodLoja", value);
	    	              this.OnCodLojaChanging(value);
	    	              this.RaiseDataMemberChanging("CodLoja");
	    	              this._CodLoja = value;
	    	              this.RaiseDataMemberChanged("CodLoja");
	    	              this.OnCodLojaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescBandeiraRede
	    partial void OnDescBandeiraRedeChanging(System.String value);
	    partial void OnDescBandeiraRedeChanged();

	    private System.String _DescBandeiraRede;

	    [DataMember(Name = "DescBandeiraRede", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Desc Bandeira Rede", Description="", Order = 15, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_LOJA.DESC_BANDEIRA_REDE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_LOJA.DESC_BANDEIRA_REDE")]
	    public System.String DescBandeiraRede
	    {
	    	    get
	    	    {
	    	          return _DescBandeiraRede;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescBandeiraRede != value)
	    	          {
	    	              this.ValidateProperty("DescBandeiraRede", value);
	    	              this.OnDescBandeiraRedeChanging(value);
	    	              this.RaiseDataMemberChanging("DescBandeiraRede");
	    	              this._DescBandeiraRede = value;
	    	              this.RaiseDataMemberChanged("DescBandeiraRede");
	    	              this.OnDescBandeiraRedeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescLoja
	    partial void OnDescLojaChanging(System.String value);
	    partial void OnDescLojaChanged();

	    private System.String _DescLoja;

	    [DataMember(IsRequired = true, Name = "DescLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Desc Loja", Description="", Order = 16, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_LOJA.DESC_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_LOJA.DESC_LOJA")]
	    public System.String DescLoja
	    {
	    	    get
	    	    {
	    	          return _DescLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescLoja != value)
	    	          {
	    	              this.ValidateProperty("DescLoja", value);
	    	              this.OnDescLojaChanging(value);
	    	              this.RaiseDataMemberChanging("DescLoja");
	    	              this._DescLoja = value;
	    	              this.RaiseDataMemberChanged("DescLoja");
	    	              this.OnDescLojaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdBandeiraRede
	    partial void OnIdBandeiraRedeChanging(System.Nullable<System.Int32> value);
	    partial void OnIdBandeiraRedeChanged();

	    private System.Nullable<System.Int32> _IdBandeiraRede;

	    [DataMember(Name = "IdBandeiraRede", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Bandeira Rede", Description="", Order = 20, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_LOJA.ID_BANDEIRA_REDE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_LOJA.ID_BANDEIRA_REDE")]
	    public System.Nullable<System.Int32> IdBandeiraRede
	    {
	    	    get
	    	    {
	    	          return _IdBandeiraRede;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdBandeiraRede != value)
	    	          {
	    	              this.ValidateProperty("IdBandeiraRede", value);
	    	              this.OnIdBandeiraRedeChanging(value);
	    	              this.RaiseDataMemberChanging("IdBandeiraRede");
	    	              this._IdBandeiraRede = value;
	    	              this.RaiseDataMemberChanged("IdBandeiraRede");
	    	              this.OnIdBandeiraRedeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdFilialPfj
	    partial void OnIdFilialPfjChanging(Int32 value);
	    partial void OnIdFilialPfjChanged();

	    private Int32 _IdFilialPfj;

	    [DataMember(IsRequired = true, Name = "IdFilialPfj", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Filial Pfj", Description="", Order = 21, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_LOJA.ID_FILIAL_PFJ];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_LOJA.ID_FILIAL_PFJ")]
	    public Int32 IdFilialPfj
	    {
	    	    get
	    	    {
	    	          return _IdFilialPfj;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdFilialPfj != value)
	    	          {
	    	              this.ValidateProperty("IdFilialPfj", value);
	    	              this.OnIdFilialPfjChanging(value);
	    	              this.RaiseDataMemberChanging("IdFilialPfj");
	    	              this._IdFilialPfj = value;
	    	              this.RaiseDataMemberChanged("IdFilialPfj");
	    	              this.OnIdFilialPfjChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdGpecon
	    partial void OnIdGpeconChanging(Int32 value);
	    partial void OnIdGpeconChanged();

	    private Int32 _IdGpecon;

	    [DataMember(IsRequired = true, Name = "IdGpecon", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Gpecon", Description="", Order = 22, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_LOJA.ID_GPECON];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_LOJA.ID_GPECON")]
	    public Int32 IdGpecon
	    {
	    	    get
	    	    {
	    	          return _IdGpecon;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdGpecon != value)
	    	          {
	    	              this.ValidateProperty("IdGpecon", value);
	    	              this.OnIdGpeconChanging(value);
	    	              this.RaiseDataMemberChanging("IdGpecon");
	    	              this._IdGpecon = value;
	    	              this.RaiseDataMemberChanged("IdGpecon");
	    	              this.OnIdGpeconChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLoja
	    partial void OnIdLojaChanging(Int32 value);
	    partial void OnIdLojaChanged();

	    private Int32 _IdLoja;

	    [DataMember(IsRequired = true, Name = "IdLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Loja", Description="", Order = 23, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_LOJA.ID_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_LOJA.ID_LOJA")]
	    public Int32 IdLoja
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

	    private Int32 _TemporaryIdLoja;
	    [DataMember(Name = "TemporaryIdLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Loja (Tmp)", Description="Temporary Key", Order = 23, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdLoja
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdLoja.IsNullOrEmpty())
	    	                this._TemporaryIdLoja = this._IdLoja;
	    	          return this._TemporaryIdLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdLoja != value)
	    	              this._TemporaryIdLoja = value;
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ConectorPos.LJV_LOJA").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Loja.BM.LJV_LOJA), QualifiedEntitySetName = "ConectorPos.LJV_LOJA" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_LOJA.ID_LOJA", Source = "IdLoja", Target = "ID_LOJA", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_LOJA", RelationPropertyName = "LJV_LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_LOJA.COD_LOJA", Source = "CodLoja", Target = "COD_LOJA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_LOJA", RelationPropertyName = "LJV_LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_LOJA.DESC_LOJA", Source = "DescLoja", Target = "DESC_LOJA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_LOJA", RelationPropertyName = "LJV_LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_LOJA.ID_GPECON", Source = "IdGpecon", Target = "ID_GPECON", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_LOJA", RelationPropertyName = "LJV_LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_LOJA.ID_FILIAL_PFJ", Source = "IdFilialPfj", Target = "ID_FILIAL_PFJ", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_LOJA", RelationPropertyName = "LJV_LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_LOJA.ID_BANDEIRA_REDE", Source = "IdBandeiraRede", Target = "ID_BANDEIRA_REDE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_LOJA", RelationPropertyName = "LJV_LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_LOJA.COD_BANDEIRA_REDE", Source = "CodBandeiraRede", Target = "COD_BANDEIRA_REDE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_LOJA", RelationPropertyName = "LJV_LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_LOJA.DESC_BANDEIRA_REDE", Source = "DescBandeiraRede", Target = "DESC_BANDEIRA_REDE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_LOJA", RelationPropertyName = "LJV_LOJA" });

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
	[DomainIdentifier("ProcessorOverviewLojaDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class LojaDomainService : DomainService, IDataServiceContext 
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

		
	    public LojaDomainService() : this("", null, null) { }
	    public LojaDomainService(string connectionString) : this(connectionString, null, null) { }
	    public LojaDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public LojaDomainService(Linx.Framework.Loja.BM.ConectorPos dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public LojaDomainService(string connectionString, Linx.Framework.Loja.BM.ConectorPos dataContext, Dictionary<string, string> headers) : base() 
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
	
		

	        if (entityName.InList("Linx.Framework.BV.Loja.LjvLoja"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "LjvLoja",
	        			NameSpace = "Linx.Framework.BV.Loja",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "LjvLoja",
	        			ClearMethodName = "ClearLjvLoja",
	        			QueryMethodName  = "GetPagedLjvLoja",	
	        			CountingMethodName  = "GetLjvLoja" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Loja.LjvLoja"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Loja.LjvLoja"), forceAll: forceAll)
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

         		    return new string[] { "Framework_LojaClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.LojaClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Framework_lojaService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.lojaService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear LjvLoja.
	    public IEnumerable<LjvLoja> ClearLjvLoja()
	    {
	        List<LjvLoja> result = new List<LjvLoja>();
	        result.Add(new LjvLoja());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    
	    [Query(HasSideEffects = false)]
	    //Get LjvLoja.
	    public IQueryable<LjvLoja> GetLjvLoja()
	    {




		
	
	        
		
	        
	
	        IQueryable<LjvLoja> result = 
	            (from entity0 in this.DbContext.LJV_LOJA
	            
	            	
	            select new LjvLoja()		
	            {
	            
                CodBandeiraRede = entity0.COD_BANDEIRA_REDE
                , CodLoja = entity0.COD_LOJA
                , DescBandeiraRede = entity0.DESC_BANDEIRA_REDE
                , DescLoja = entity0.DESC_LOJA
                , IdBandeiraRede = entity0.ID_BANDEIRA_REDE
                , IdFilialPfj = entity0.ID_FILIAL_PFJ
                , IdGpecon = entity0.ID_GPECON
                , IdLoja = entity0.ID_LOJA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get LjvLojaNoAssociations.
	    public IQueryable<LjvLoja> GetLjvLojaNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<LjvLoja> result = 
	            (from entity0 in this.DbContext.LJV_LOJA
	            
	            	
	            select new LjvLoja()		
	            {
	            
                CodBandeiraRede = entity0.COD_BANDEIRA_REDE
                , CodLoja = entity0.COD_LOJA
                , DescBandeiraRede = entity0.DESC_BANDEIRA_REDE
                , DescLoja = entity0.DESC_LOJA
                , IdBandeiraRede = entity0.ID_BANDEIRA_REDE
                , IdFilialPfj = entity0.ID_FILIAL_PFJ
                , IdGpecon = entity0.ID_GPECON
                , IdLoja = entity0.ID_LOJA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	//Add filtering disabled property for LJV_LOJA
	    	string[] bmDisabledLjvLojaList = this.GetEDM().GetFilteringDisabledList("LJV_LOJA");
	    	if (bmDisabledLjvLojaList.Length > 0)
	    	{
	
	    		if (bmDisabledLjvLojaList.Contains("LJV_LOJA.COD_BANDEIRA_REDE"))
	    		{
	    			result.Add("LjvLoja|CodBandeiraRede");
	    			result.Add("LjvLoja|LJV_LOJA.COD_BANDEIRA_REDE");
	    		}
	
	    		if (bmDisabledLjvLojaList.Contains("LJV_LOJA.COD_LOJA"))
	    		{
	    			result.Add("LjvLoja|CodLoja");
	    			result.Add("LjvLoja|LJV_LOJA.COD_LOJA");
	    		}
	
	    		if (bmDisabledLjvLojaList.Contains("LJV_LOJA.DESC_BANDEIRA_REDE"))
	    		{
	    			result.Add("LjvLoja|DescBandeiraRede");
	    			result.Add("LjvLoja|LJV_LOJA.DESC_BANDEIRA_REDE");
	    		}
	
	    		if (bmDisabledLjvLojaList.Contains("LJV_LOJA.DESC_LOJA"))
	    		{
	    			result.Add("LjvLoja|DescLoja");
	    			result.Add("LjvLoja|LJV_LOJA.DESC_LOJA");
	    		}
	
	    		if (bmDisabledLjvLojaList.Contains("LJV_LOJA.ID_BANDEIRA_REDE"))
	    		{
	    			result.Add("LjvLoja|IdBandeiraRede");
	    			result.Add("LjvLoja|LJV_LOJA.ID_BANDEIRA_REDE");
	    		}
	
	    		if (bmDisabledLjvLojaList.Contains("LJV_LOJA.ID_FILIAL_PFJ"))
	    		{
	    			result.Add("LjvLoja|IdFilialPfj");
	    			result.Add("LjvLoja|LJV_LOJA.ID_FILIAL_PFJ");
	    		}
	
	    		if (bmDisabledLjvLojaList.Contains("LJV_LOJA.ID_GPECON"))
	    		{
	    			result.Add("LjvLoja|IdGpecon");
	    			result.Add("LjvLoja|LJV_LOJA.ID_GPECON");
	    		}
	
	    		if (bmDisabledLjvLojaList.Contains("LJV_LOJA.ID_LOJA"))
	    		{
	    			result.Add("LjvLoja|IdLoja");
	    			result.Add("LjvLoja|LJV_LOJA.ID_LOJA");
	    		}
	    	}
	    	return result;
	    }

	    #endregion Excluded Filters

	    #region Get By EntitySearchId Methods Definitions.
	
				
	    [Ignore]
	    //Get LjvLoja By EntitySearchId.
	    public IQueryable<LjvLoja> GetLjvLojaByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetLjvLojaByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get LjvLoja By EntitySearchId.
	    public IQueryable<LjvLoja> GetLjvLojaByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetLjvLojaByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get LjvLoja By Example.
	    [Ignore]
	    public IQueryable<LjvLoja> GetLjvLojaByExample(LjvLoja entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetLjvLojaByEntitySearch(queryAnalysis);
	    }
			
	    //Get LjvLoja By Example.
	    [Ignore]
	    public IQueryable<LjvLoja> GetLjvLojaByExampleNoAssociations(LjvLoja entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetLjvLojaByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public LjvLoja GetLjvLojaByKey(Int32 idLoja)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("LjvLoja");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdLoja"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idLoja));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetLjvLojaByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get LjvLojaByEntitySearch.
	    public IQueryable<LjvLoja> GetLjvLojaByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LjvLoja));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<LjvLoja> result = 
	            (from entity0 in this.DbContext.LJV_LOJA.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new LjvLoja()		
	            {
	            
                CodBandeiraRede = entity0.COD_BANDEIRA_REDE
                , CodLoja = entity0.COD_LOJA
                , DescBandeiraRede = entity0.DESC_BANDEIRA_REDE
                , DescLoja = entity0.DESC_LOJA
                , IdBandeiraRede = entity0.ID_BANDEIRA_REDE
                , IdFilialPfj = entity0.ID_FILIAL_PFJ
                , IdGpecon = entity0.ID_GPECON
                , IdLoja = entity0.ID_LOJA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get LjvLojaByEntitySearchNoAssociations.
	    public IQueryable<LjvLoja> GetLjvLojaByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LjvLoja));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<LjvLoja> result = 
	            (from entity0 in this.DbContext.LJV_LOJA.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new LjvLoja()		
	            {
	            
                CodBandeiraRede = entity0.COD_BANDEIRA_REDE
                , CodLoja = entity0.COD_LOJA
                , DescBandeiraRede = entity0.DESC_BANDEIRA_REDE
                , DescLoja = entity0.DESC_LOJA
                , IdBandeiraRede = entity0.ID_BANDEIRA_REDE
                , IdFilialPfj = entity0.ID_FILIAL_PFJ
                , IdGpecon = entity0.ID_GPECON
                , IdLoja = entity0.ID_LOJA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get PagedLjvLoja.
	    public IQueryable<LjvLoja> GetPagedLjvLoja(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LjvLoja));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<LjvLoja> result = 
	            (from entity0 in this.DbContext.LJV_LOJA.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_LOJA ascending
	            
	            	
	            select new LjvLoja()		
	            {
	            
                CodBandeiraRede = entity0.COD_BANDEIRA_REDE
                , CodLoja = entity0.COD_LOJA
                , DescBandeiraRede = entity0.DESC_BANDEIRA_REDE
                , DescLoja = entity0.DESC_LOJA
                , IdBandeiraRede = entity0.ID_BANDEIRA_REDE
                , IdFilialPfj = entity0.ID_FILIAL_PFJ
                , IdGpecon = entity0.ID_GPECON
                , IdLoja = entity0.ID_LOJA
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetLjvLojaCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LjvLoja));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.LJV_LOJA.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    
	    [Update()]	
	    //Update LjvLoja.
	    public void UpdateLjvLoja(LjvLoja entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert LjvLoja.
	    public void InsertLjvLoja(LjvLoja entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete LjvLoja.
	    public void DeleteLjvLoja(LjvLoja entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}