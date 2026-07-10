					
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
using Linx.Framework.Autorizacao.BM;

namespace Linx.Framework.BV.Utilitarios
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_USUARIO_AUTENTICACAO.ID_USUARIO", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsUsuarioAutenticacao];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdUsuario];ReadOnly[false];Entities[TCS_USUARIO_AUTENTICACAO:IdUsuario];SubQueryInfo[];EdmEntityName[TCS_USUARIO_AUTENTICACAO];EntityRelations[TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioAutenticacao")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Utilitarios.TcsUsuarioAutenticacao")]
	public partial class TcsUsuarioAutenticacao : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For BandeiraRede
	    partial void OnBandeiraRedeChanging(bool value);
	    partial void OnBandeiraRedeChanged();

	    private bool _BandeiraRede;

	    [DataMember(IsRequired = true, Name = "BandeiraRede", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bandeira / Rede / Filiais / Grupo Econômico", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[false];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="false")]
	    public bool BandeiraRede
	    {
	    	    get
	    	    {
	    	          return _BandeiraRede;
	    	    }
	    	    set
	    	    {
	    	          if (this._BandeiraRede != value)
	    	          {
	    	              this.ValidateProperty("BandeiraRede", value);
	    	              this.OnBandeiraRedeChanging(value);
	    	              this.RaiseDataMemberChanging("BandeiraRede");
	    	              this._BandeiraRede = value;
	    	              this.RaiseDataMemberChanged("BandeiraRede");
	    	              this.OnBandeiraRedeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Conexao
	    partial void OnConexaoChanging(bool value);
	    partial void OnConexaoChanged();

	    private bool _Conexao;

	    [DataMember(IsRequired = true, Name = "Conexao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Conexões", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[false];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="false")]
	    public bool Conexao
	    {
	    	    get
	    	    {
	    	          return _Conexao;
	    	    }
	    	    set
	    	    {
	    	          if (this._Conexao != value)
	    	          {
	    	              this.ValidateProperty("Conexao", value);
	    	              this.OnConexaoChanging(value);
	    	              this.RaiseDataMemberChanging("Conexao");
	    	              this._Conexao = value;
	    	              this.RaiseDataMemberChanged("Conexao");
	    	              this.OnConexaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Geral
	    partial void OnGeralChanging(bool value);
	    partial void OnGeralChanged();

	    private bool _Geral;

	    [DataMember(IsRequired = true, Name = "Geral", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cache Completo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[false];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="false")]
	    public bool Geral
	    {
	    	    get
	    	    {
	    	          return _Geral;
	    	    }
	    	    set
	    	    {
	    	          if (this._Geral != value)
	    	          {
	    	              this.ValidateProperty("Geral", value);
	    	              this.OnGeralChanging(value);
	    	              this.RaiseDataMemberChanging("Geral");
	    	              this._Geral = value;
	    	              this.RaiseDataMemberChanged("Geral");
	    	              this.OnGeralChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuario
	    partial void OnIdUsuarioChanging(Int64 value);
	    partial void OnIdUsuarioChanged();

	    private Int64 _IdUsuario;

	    [DataMember(IsRequired = true, Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacao];LookUpTitle[Seleção de (Id Usuario)];LookUpQuery[executeLookUpTcsUsuarioAutenticacao];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacao];LookUpDisplayColumns[{\"NomeUsuario\" : \"Usuário\", \"IdUsuario\" : \"Id Usuario\"}];LookUpColumns[{\"NomeUsuario\" : true, \"IdUsuario\" : false}];FilterDataKey[TCS_USUARIO_AUTENTICACAO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdUsuario#true##12:0##Id Usuario#1#false##::LookUpTcsUsuarioAutenticacao##false#false##TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.Utilitarios#IQueryable###true#false", EdmKey="TCS_USUARIO_AUTENTICACAO.ID_USUARIO")]
	    public Int64 IdUsuario
	    {
	    	    get
	    	    {
	    	          return _IdUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdUsuario != value)
	    	          {
	    	              this.ValidateProperty("IdUsuario", value);
	    	              this.OnIdUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("IdUsuario");
	    	              this._IdUsuario = value;
	    	              this.RaiseDataMemberChanged("IdUsuario");
	    	              this.OnIdUsuarioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Modulo
	    partial void OnModuloChanging(bool value);
	    partial void OnModuloChanged();

	    private bool _Modulo;

	    [DataMember(IsRequired = true, Name = "Modulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Módulos / Menus / Transações", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[false];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="false")]
	    public bool Modulo
	    {
	    	    get
	    	    {
	    	          return _Modulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._Modulo != value)
	    	          {
	    	              this.ValidateProperty("Modulo", value);
	    	              this.OnModuloChanging(value);
	    	              this.RaiseDataMemberChanging("Modulo");
	    	              this._Modulo = value;
	    	              this.RaiseDataMemberChanged("Modulo");
	    	              this.OnModuloChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeUsuario
	    partial void OnNomeUsuarioChanging(System.String value);
	    partial void OnNomeUsuarioChanged();

	    private System.String _NomeUsuario;

	    [DataMember(IsRequired = true, Name = "NomeUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário", Description="", Order = 20, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacao];LookUpTitle[Seleção de (Usuário)];LookUpQuery[executeLookUpTcsUsuarioAutenticacao];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacao];LookUpDisplayColumns[{\"NomeUsuario\" : \"Usuário\", \"IdUsuario\" : \"Id Usuario\"}];LookUpColumns[{\"NomeUsuario\" : true, \"IdUsuario\" : false}];FilterDataKey[TCS_USUARIO_AUTENTICACAO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeUsuario#false##2500##Usuário#0#true##::LookUpTcsUsuarioAutenticacao##false#false##TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.Utilitarios#IQueryable###true#false", EdmKey="TCS_USUARIO_AUTENTICACAO.NOME_USUARIO")]
	    public System.String NomeUsuario
	    {
	    	    get
	    	    {
	    	          return _NomeUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeUsuario != value)
	    	          {
	    	              this.ValidateProperty("NomeUsuario", value);
	    	              this.OnNomeUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("NomeUsuario");
	    	              this._NomeUsuario = value;
	    	              this.RaiseDataMemberChanged("NomeUsuario");
	    	              this.OnNomeUsuarioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Relatorio
	    partial void OnRelatorioChanging(bool value);
	    partial void OnRelatorioChanged();

	    private bool _Relatorio;

	    [DataMember(IsRequired = true, Name = "Relatorio", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Relatórios", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[false];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="false")]
	    public bool Relatorio
	    {
	    	    get
	    	    {
	    	          return _Relatorio;
	    	    }
	    	    set
	    	    {
	    	          if (this._Relatorio != value)
	    	          {
	    	              this.ValidateProperty("Relatorio", value);
	    	              this.OnRelatorioChanging(value);
	    	              this.RaiseDataMemberChanging("Relatorio");
	    	              this._Relatorio = value;
	    	              this.RaiseDataMemberChanged("Relatorio");
	    	              this.OnRelatorioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidUsuario
	    partial void OnUidUsuarioChanging(System.Guid value);
	    partial void OnUidUsuarioChanged();

	    private System.Guid _UidUsuario;

	    [DataMember(IsRequired = true, Name = "UidUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Usuario", Description="", Order = 26, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.UID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.UID_USUARIO")]
	    public System.Guid UidUsuario
	    {
	    	    get
	    	    {
	    	          return _UidUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidUsuario != value)
	    	          {
	    	              this.ValidateProperty("UidUsuario", value);
	    	              this.OnUidUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("UidUsuario");
	    	              this._UidUsuario = value;
	    	              this.RaiseDataMemberChanged("UidUsuario");
	    	              this.OnUidUsuarioChanged();
	    	          }
	    	    }
	    }

	    private Int64 _TemporaryIdUsuario;
	    [DataMember(Name = "TemporaryIdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario (Tmp)", Description="Temporary Key", Order = 12, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdUsuario
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdUsuario.IsNullOrEmpty())
	    	                this._TemporaryIdUsuario = this._IdUsuario;
	    	          return this._TemporaryIdUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdUsuario != value)
	    	              this._TemporaryIdUsuario = value;
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_USUARIO_AUTENTICACAO), QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.UID_USUARIO", Source = "UidUsuario", Target = "UID_USUARIO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.NOME_USUARIO", Source = "NomeUsuario", Target = "NOME_USUARIO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });

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
	[DomainIdentifier("ProcessorOverviewUtilitariosDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class UtilitariosDomainService : DomainService, IDataServiceContext 
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

	
	    private Linx.Framework.Autorizacao.BM.AutorizacaoContext _dbContext;
	    protected Linx.Framework.Autorizacao.BM.AutorizacaoContext DbContext 
	    { 
	    	get 
	    	{
	        	if (this._dbContext == null)
	        	{
	        		this._dbContext = new Linx.Framework.Autorizacao.BM.AutorizacaoContext(connectionString, this.Headers);
	        		((System.Data.Entity.Infrastructure.IObjectContextAdapter)this._dbContext).ObjectContext.CommandTimeout = 180;
	        	}
	        	return this._dbContext;
	    	}
	    }

	    public string GetModelAssemblyName()
	    {
	        return typeof(Linx.Framework.Autorizacao.BM.AutorizacaoContext).Assembly.FullName;
	    }

	    public System.Data.Entity.Database Database
	    {
	        get { return this.DbContext.Database; }
	    }

		
	    public UtilitariosDomainService() : this("", null, null) { }
	    public UtilitariosDomainService(string connectionString) : this(connectionString, null, null) { }
	    public UtilitariosDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public UtilitariosDomainService(Linx.Framework.Autorizacao.BM.AutorizacaoContext dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public UtilitariosDomainService(string connectionString, Linx.Framework.Autorizacao.BM.AutorizacaoContext dataContext, Dictionary<string, string> headers) : base() 
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
	    public Linx.Framework.Autorizacao.BM.AutorizacaoContext GetEDM()
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
	
		
			
        [Ignore]
	    //Get All LookUpTcsUsuarioAutenticacao.
	    public IQueryable<LookUpTcsUsuarioAutenticacao> GetAllLookUpTcsUsuarioAutenticacao()
	    {
	        return this.GetLookUpTcsUsuarioAutenticacao(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsUsuarioAutenticacao By EntitySearch.
	    public IQueryable<LookUpTcsUsuarioAutenticacao> GetLookUpTcsUsuarioAutenticacaoByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsUsuarioAutenticacao(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsUsuarioAutenticacao.
	    public IQueryable<LookUpTcsUsuarioAutenticacao> GetLookUpTcsUsuarioAutenticacao(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TCS_USUARIO_AUTENTICACAO" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsUsuarioAutenticacao";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsUsuarioAutenticacao));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsUsuarioAutenticacao> query =  
	
	            (from entity in this.DbContext.TCS_USUARIO_AUTENTICACAO.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpTcsUsuarioAutenticacao()		
	            {
	            
                NomeUsuario = entity.NOME_USUARIO
                , IdUsuario = entity.ID_USUARIO
                , UidUsuario = entity.UID_USUARIO
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
	
		

	        if (entityName.InList("Linx.Framework.BV.Utilitarios.TcsUsuarioAutenticacao"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsUsuarioAutenticacao",
	        			NameSpace = "Linx.Framework.BV.Utilitarios",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsUsuarioAutenticacao",
	        			ClearMethodName = "ClearTcsUsuarioAutenticacao",
	        			QueryMethodName  = "GetPagedTcsUsuarioAutenticacao",	
	        			CountingMethodName  = "GetTcsUsuarioAutenticacao" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Utilitarios.TcsUsuarioAutenticacao"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Utilitarios.TcsUsuarioAutenticacao"), forceAll: forceAll)
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

         		    return new string[] { "Framework_UtilitariosClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.UtilitariosClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Framework_utilitariosService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.utilitariosService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear TcsUsuarioAutenticacao.
	    public IEnumerable<TcsUsuarioAutenticacao> ClearTcsUsuarioAutenticacao()
	    {
	        List<TcsUsuarioAutenticacao> result = new List<TcsUsuarioAutenticacao>();
	        result.Add(new TcsUsuarioAutenticacao());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsUsuarioAutenticacao.
	    public IQueryable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacao()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioAutenticacao> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_AUTENTICACAO
	            
	            	
	            select new TcsUsuarioAutenticacao()		
	            {
	            
                BandeiraRede = false
                , Conexao = false
                , Geral = false
                , IdUsuario = entity0.ID_USUARIO
                , Modulo = false
                , NomeUsuario = entity0.NOME_USUARIO
                , Relatorio = false
                , UidUsuario = entity0.UID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioAutenticacaoNoAssociations.
	    public IQueryable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioAutenticacao> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_AUTENTICACAO
	            
	            	
	            select new TcsUsuarioAutenticacao()		
	            {
	            
                BandeiraRede = false
                , Conexao = false
                , Geral = false
                , IdUsuario = entity0.ID_USUARIO
                , Modulo = false
                , NomeUsuario = entity0.NOME_USUARIO
                , Relatorio = false
                , UidUsuario = entity0.UID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	result.Add("TcsUsuarioAutenticacao|BandeiraRede");
	    	result.Add("TcsUsuarioAutenticacao|false");
	    	result.Add("TcsUsuarioAutenticacao|Conexao");
	    	result.Add("TcsUsuarioAutenticacao|false");
	    	result.Add("TcsUsuarioAutenticacao|Geral");
	    	result.Add("TcsUsuarioAutenticacao|false");
	    	result.Add("TcsUsuarioAutenticacao|Modulo");
	    	result.Add("TcsUsuarioAutenticacao|false");
	    	result.Add("TcsUsuarioAutenticacao|Relatorio");
	    	result.Add("TcsUsuarioAutenticacao|false");
	    	//Add filtering disabled property for TCS_USUARIO_AUTENTICACAO
	    	string[] bmDisabledTcsUsuarioAutenticacaoList = this.GetEDM().GetFilteringDisabledList("TCS_USUARIO_AUTENTICACAO");
	    	if (bmDisabledTcsUsuarioAutenticacaoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsUsuarioAutenticacaoList.Contains("TCS_USUARIO_AUTENTICACAO.ID_USUARIO"))
	    		{
	    			result.Add("TcsUsuarioAutenticacao|IdUsuario");
	    			result.Add("TcsUsuarioAutenticacao|TCS_USUARIO_AUTENTICACAO.ID_USUARIO");
	    		}
	
	    		if (bmDisabledTcsUsuarioAutenticacaoList.Contains("TCS_USUARIO_AUTENTICACAO.NOME_USUARIO"))
	    		{
	    			result.Add("TcsUsuarioAutenticacao|NomeUsuario");
	    			result.Add("TcsUsuarioAutenticacao|TCS_USUARIO_AUTENTICACAO.NOME_USUARIO");
	    		}
	
	    		if (bmDisabledTcsUsuarioAutenticacaoList.Contains("TCS_USUARIO_AUTENTICACAO.UID_USUARIO"))
	    		{
	    			result.Add("TcsUsuarioAutenticacao|UidUsuario");
	    			result.Add("TcsUsuarioAutenticacao|TCS_USUARIO_AUTENTICACAO.UID_USUARIO");
	    		}
	    	}
	    	return result;
	    }

	    #endregion Excluded Filters

	    #region Get By EntitySearchId Methods Definitions.
	
				
	    [Ignore]
	    //Get TcsUsuarioAutenticacao By EntitySearchId.
	    public IQueryable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioAutenticacaoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioAutenticacao By EntitySearchId.
	    public IQueryable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get TcsUsuarioAutenticacao By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoByExample(TcsUsuarioAutenticacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioAutenticacaoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsUsuarioAutenticacao By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoByExampleNoAssociations(TcsUsuarioAutenticacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public TcsUsuarioAutenticacao GetTcsUsuarioAutenticacaoByKey(Int64 idUsuario)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsUsuarioAutenticacao");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuario"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idUsuario));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioAutenticacaoByEntitySearch.
	    public IQueryable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioAutenticacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioAutenticacao> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_AUTENTICACAO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsUsuarioAutenticacao()		
	            {
	            
                BandeiraRede = false
                , Conexao = false
                , Geral = false
                , IdUsuario = entity0.ID_USUARIO
                , Modulo = false
                , NomeUsuario = entity0.NOME_USUARIO
                , Relatorio = false
                , UidUsuario = entity0.UID_USUARIO
		
	            }
	            );
	
	        SetTcsUsuarioAutenticacaoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioAutenticacaoByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioAutenticacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioAutenticacao> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_AUTENTICACAO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsUsuarioAutenticacao()		
	            {
	            
                BandeiraRede = false
                , Conexao = false
                , Geral = false
                , IdUsuario = entity0.ID_USUARIO
                , Modulo = false
                , NomeUsuario = entity0.NOME_USUARIO
                , Relatorio = false
                , UidUsuario = entity0.UID_USUARIO
		
	            }
	            );
	
	        SetTcsUsuarioAutenticacaoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
	
	    [Ignore()]
	    private void SetTcsUsuarioAutenticacaoBusinessFilter(ref IQueryable<TcsUsuarioAutenticacao> query, List<EntitySearch> entitySearchList)
	    {
	    		int idxElement;
	    		string operatorValue;
	    		object value;
	    		//Get query by functions
	    		if (entitySearchList.Count > 0)
	    		{
	    			foreach (EntitySearch search in entitySearchList.Where(e => e.EntityName == "TcsUsuarioAutenticacao"))
	    			{

	
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "BandeiraRede" || e.Value.ToString() == "false")))
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

	
	    								switch (operatorValue)
	    								{
	    									case "==":
	    										bool tmpBandeiraRede1 = (bool)value;
	    										query = from r in query where r.BandeiraRede == tmpBandeiraRede1 select r;
	    										break;
	    									case "!=":
	    										bool tmpBandeiraRede2 = (bool)value;
	    										query = from r in query where r.BandeiraRede != tmpBandeiraRede2 select r;
	    										break;

	
	    									default:
	    										break;
	    								}                                
	    							}
	    						}
        					} 

    
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "Conexao" || e.Value.ToString() == "false")))
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

	
	    								switch (operatorValue)
	    								{
	    									case "==":
	    										bool tmpConexao1 = (bool)value;
	    										query = from r in query where r.Conexao == tmpConexao1 select r;
	    										break;
	    									case "!=":
	    										bool tmpConexao2 = (bool)value;
	    										query = from r in query where r.Conexao != tmpConexao2 select r;
	    										break;

	
	    									default:
	    										break;
	    								}                                
	    							}
	    						}
        					} 

    
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "Geral" || e.Value.ToString() == "false")))
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

	
	    								switch (operatorValue)
	    								{
	    									case "==":
	    										bool tmpGeral1 = (bool)value;
	    										query = from r in query where r.Geral == tmpGeral1 select r;
	    										break;
	    									case "!=":
	    										bool tmpGeral2 = (bool)value;
	    										query = from r in query where r.Geral != tmpGeral2 select r;
	    										break;

	
	    									default:
	    										break;
	    								}                                
	    							}
	    						}
        					} 

    
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "Modulo" || e.Value.ToString() == "false")))
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

	
	    								switch (operatorValue)
	    								{
	    									case "==":
	    										bool tmpModulo1 = (bool)value;
	    										query = from r in query where r.Modulo == tmpModulo1 select r;
	    										break;
	    									case "!=":
	    										bool tmpModulo2 = (bool)value;
	    										query = from r in query where r.Modulo != tmpModulo2 select r;
	    										break;

	
	    									default:
	    										break;
	    								}                                
	    							}
	    						}
        					} 

    
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "Relatorio" || e.Value.ToString() == "false")))
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

	
	    								switch (operatorValue)
	    								{
	    									case "==":
	    										bool tmpRelatorio1 = (bool)value;
	    										query = from r in query where r.Relatorio == tmpRelatorio1 select r;
	    										break;
	    									case "!=":
	    										bool tmpRelatorio2 = (bool)value;
	    										query = from r in query where r.Relatorio != tmpRelatorio2 select r;
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
	    //Get PagedTcsUsuarioAutenticacao.
	    public IQueryable<TcsUsuarioAutenticacao> GetPagedTcsUsuarioAutenticacao(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioAutenticacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioAutenticacao> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_AUTENTICACAO.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_USUARIO ascending
	            
	            	
	            select new TcsUsuarioAutenticacao()		
	            {
	            
                BandeiraRede = false
                , Conexao = false
                , Geral = false
                , IdUsuario = entity0.ID_USUARIO
                , Modulo = false
                , NomeUsuario = entity0.NOME_USUARIO
                , Relatorio = false
                , UidUsuario = entity0.UID_USUARIO
		
	            }
	            ).Skip(skip).Take(take);
	
	        SetTcsUsuarioAutenticacaoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsUsuarioAutenticacaoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioAutenticacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_USUARIO_AUTENTICACAO.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    
	    [Update()]	
	    //Update TcsUsuarioAutenticacao.
	    public void UpdateTcsUsuarioAutenticacao(TcsUsuarioAutenticacao entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsUsuarioAutenticacao.
	    public void InsertTcsUsuarioAutenticacao(TcsUsuarioAutenticacao entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsUsuarioAutenticacao.
	    public void DeleteTcsUsuarioAutenticacao(TcsUsuarioAutenticacao entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}