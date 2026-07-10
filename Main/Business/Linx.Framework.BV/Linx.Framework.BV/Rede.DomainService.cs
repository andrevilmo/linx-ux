					
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

namespace Linx.Framework.BV.Rede
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TBC_BANDEIRA_REDE.ID_BANDEIRA_REDE", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TbcBandeiraRede];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdBandeiraRede];ReadOnly[false];Entities[TBC_BANDEIRA_REDE:IdBandeiraRede];SubQueryInfo[];EdmEntityName[TBC_BANDEIRA_REDE];EntityRelations[];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TbcBandeiraRede")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Rede.TbcBandeiraRede")]
	public partial class TbcBandeiraRede : Linx.Data.Entity
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

	    [DataMember(IsRequired = true, Name = "CodBandeiraRede", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cod Bandeira Rede", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(25)]
	    [FunctionalPoint("Precision[25:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBC_BANDEIRA_REDE.COD_BANDEIRA_REDE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_BANDEIRA_REDE.COD_BANDEIRA_REDE")]
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
	    //Extensibility Partial Method Definitions For DataAtualizacao
	    partial void OnDataAtualizacaoChanging(System.DateTime value);
	    partial void OnDataAtualizacaoChanged();

	    private System.DateTime _DataAtualizacao;

	    [DataMember(IsRequired = true, Name = "DataAtualizacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Data Atualizacao", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBC_BANDEIRA_REDE.DATA_ATUALIZACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_BANDEIRA_REDE.DATA_ATUALIZACAO")]
	    public System.DateTime DataAtualizacao
	    {
	    	    get
	    	    {
	    	          return _DataAtualizacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataAtualizacao != value)
	    	          {
	    	              this.ValidateProperty("DataAtualizacao", value);
	    	              this.OnDataAtualizacaoChanging(value);
	    	              this.RaiseDataMemberChanging("DataAtualizacao");
	    	              this._DataAtualizacao = value;
	    	              this.RaiseDataMemberChanged("DataAtualizacao");
	    	              this.OnDataAtualizacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DataCadastro
	    partial void OnDataCadastroChanging(System.DateTime value);
	    partial void OnDataCadastroChanged();

	    private System.DateTime _DataCadastro;

	    [DataMember(IsRequired = true, Name = "DataCadastro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Data Cadastro", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBC_BANDEIRA_REDE.DATA_CADASTRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_BANDEIRA_REDE.DATA_CADASTRO")]
	    public System.DateTime DataCadastro
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
	    //Extensibility Partial Method Definitions For DescBandeiraRede
	    partial void OnDescBandeiraRedeChanging(System.String value);
	    partial void OnDescBandeiraRedeChanged();

	    private System.String _DescBandeiraRede;

	    [DataMember(IsRequired = true, Name = "DescBandeiraRede", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Desc Bandeira Rede", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBC_BANDEIRA_REDE.DESC_BANDEIRA_REDE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_BANDEIRA_REDE.DESC_BANDEIRA_REDE")]
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
	    //Extensibility Partial Method Definitions For IdBandeiraRede
	    partial void OnIdBandeiraRedeChanging(Int32 value);
	    partial void OnIdBandeiraRedeChanged();

	    private Int32 _IdBandeiraRede;

	    [DataMember(IsRequired = true, Name = "IdBandeiraRede", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Bandeira Rede", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBC_BANDEIRA_REDE.ID_BANDEIRA_REDE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_BANDEIRA_REDE.ID_BANDEIRA_REDE")]
	    public Int32 IdBandeiraRede
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
	    //Extensibility Partial Method Definitions For Midia
	    partial void OnMidiaChanging(Linx.Framework.BV.Multimidia.DocMultimidiaInfo value);
	    partial void OnMidiaChanged();

	    private Linx.Framework.BV.Multimidia.DocMultimidiaInfo _Midia;

	    [DataMember(Name = "Midia", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public Linx.Framework.BV.Multimidia.DocMultimidiaInfo Midia
	    {
	    	    get
	    	    {
	    	          return _Midia;
	    	    }
	    	    set
	    	    {
	    	          if (this._Midia != value)
	    	          {
	    	              this.ValidateProperty("Midia", value);
	    	              this.OnMidiaChanging(value);
	    	              this.RaiseDataMemberChanging("Midia");
	    	              this._Midia = value;
	    	              this.RaiseDataMemberChanged("Midia");
	    	              this.OnMidiaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsAmbiente
	    partial void OnIdTcsAmbienteChanging(int value);
	    partial void OnIdTcsAmbienteChanged();

	    private int _IdTcsAmbiente;

	    [DataMember(IsRequired = true, Name = "IdTcsAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public int IdTcsAmbiente
	    {
	    	    get
	    	    {
	    	          return _IdTcsAmbiente;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAmbiente != value)
	    	          {
	    	              this.ValidateProperty("IdTcsAmbiente", value);
	    	              this.OnIdTcsAmbienteChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsAmbiente");
	    	              this._IdTcsAmbiente = value;
	    	              this.RaiseDataMemberChanged("IdTcsAmbiente");
	    	              this.OnIdTcsAmbienteChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIdBandeiraRede;
	    [DataMember(Name = "TemporaryIdBandeiraRede", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Bandeira Rede (Tmp)", Description="Temporary Key", Order = 5, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdBandeiraRede
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdBandeiraRede.IsNullOrEmpty())
	    	                this._TemporaryIdBandeiraRede = this._IdBandeiraRede;
	    	          return this._TemporaryIdBandeiraRede;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdBandeiraRede != value)
	    	              this._TemporaryIdBandeiraRede = value;
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TBC_BANDEIRA_REDE").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TBC_BANDEIRA_REDE), QualifiedEntitySetName = "ControleSistemaContext.TBC_BANDEIRA_REDE" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TBC_BANDEIRA_REDE.DATA_CADASTRO", Source = "DataCadastro", Target = "DATA_CADASTRO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TBC_BANDEIRA_REDE", RelationPropertyName = "TBC_BANDEIRA_REDE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TBC_BANDEIRA_REDE.DATA_ATUALIZACAO", Source = "DataAtualizacao", Target = "DATA_ATUALIZACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TBC_BANDEIRA_REDE", RelationPropertyName = "TBC_BANDEIRA_REDE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TBC_BANDEIRA_REDE.ID_BANDEIRA_REDE", Source = "IdBandeiraRede", Target = "ID_BANDEIRA_REDE", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TBC_BANDEIRA_REDE", RelationPropertyName = "TBC_BANDEIRA_REDE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TBC_BANDEIRA_REDE.COD_BANDEIRA_REDE", Source = "CodBandeiraRede", Target = "COD_BANDEIRA_REDE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TBC_BANDEIRA_REDE", RelationPropertyName = "TBC_BANDEIRA_REDE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TBC_BANDEIRA_REDE.DESC_BANDEIRA_REDE", Source = "DescBandeiraRede", Target = "DESC_BANDEIRA_REDE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TBC_BANDEIRA_REDE", RelationPropertyName = "TBC_BANDEIRA_REDE" });

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
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="BandeiraRedeCache.EntityUniqueKey", IsUpdatable=false, EdmName="")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[BandeiraRedeCache];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[];EdmEntityName[];EntityRelations[ ];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "BandeiraRedeCache")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Rede.BandeiraRedeCache")]
	public partial class BandeiraRedeCache : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For Hash
	    partial void OnHashChanging(string value);
	    partial void OnHashChanged();

	    private string _Hash;

	    [DataMember(IsRequired = true, Name = "Hash", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Hash
	    {
	    	    get
	    	    {
	    	          if (_Hash.IsNullOrEmpty())
	    	             _Hash =  String.Empty;
	    	          return _Hash;
	    	    }
	    	    set
	    	    {
	    	          if (this._Hash != value)
	    	          {
	    	              this.ValidateProperty("Hash", value);
	    	              this.OnHashChanging(value);
	    	              this.RaiseDataMemberChanging("Hash");
	    	              this._Hash = value;
	    	              this.RaiseDataMemberChanged("Hash");
	    	              this.OnHashChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UserBandeiraRede
	    partial void OnUserBandeiraRedeChanging(List<TbcBandeiraRede> value);
	    partial void OnUserBandeiraRedeChanged();

	    private List<TbcBandeiraRede> _UserBandeiraRede;

	    [DataMember(IsRequired = true, Name = "UserBandeiraRede", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public List<TbcBandeiraRede> UserBandeiraRede
	    {
	    	    get
	    	    {
	    	          return _UserBandeiraRede;
	    	    }
	    	    set
	    	    {
	    	          if (this._UserBandeiraRede != value)
	    	          {
	    	              this.ValidateProperty("UserBandeiraRede", value);
	    	              this.OnUserBandeiraRedeChanging(value);
	    	              this.RaiseDataMemberChanging("UserBandeiraRede");
	    	              this._UserBandeiraRede = value;
	    	              this.RaiseDataMemberChanged("UserBandeiraRede");
	    	              this.OnUserBandeiraRedeChanged();
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
	[DomainIdentifier("ProcessorOverviewRedeDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class RedeDomainService : DomainService, IDataServiceContext 
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

		
	    public RedeDomainService() : this("", null, null) { }
	    public RedeDomainService(string connectionString) : this(connectionString, null, null) { }
	    public RedeDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public RedeDomainService(Linx.Framework.ControleSistema.BM.ControleSistemaContext dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public RedeDomainService(string connectionString, Linx.Framework.ControleSistema.BM.ControleSistemaContext dataContext, Dictionary<string, string> headers) : base() 
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
	
		

	        if (entityName.InList("Linx.Framework.BV.Rede.TbcBandeiraRede"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TbcBandeiraRede",
	        			NameSpace = "Linx.Framework.BV.Rede",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TbcBandeiraRede",
	        			ClearMethodName = "ClearTbcBandeiraRede",
	        			QueryMethodName  = "GetPagedTbcBandeiraRede",	
	        			CountingMethodName  = "GetTbcBandeiraRede" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Rede.TbcBandeiraRede"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Rede.TbcBandeiraRede"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Rede.BandeiraRedeCache"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "BandeiraRedeCache",
	        			NameSpace = "Linx.Framework.BV.Rede",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "BandeiraRedeCache",
	        			ClearMethodName = "ClearBandeiraRedeCache",
	        			QueryMethodName  = "GetPagedBandeiraRedeCache",	
	        			CountingMethodName  = "GetBandeiraRedeCache" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Rede.BandeiraRedeCache"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Rede.BandeiraRedeCache"), forceAll: forceAll)
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

         		    return new string[] { "Framework_RedeClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.RedeClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Framework_redeService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.redeService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear TbcBandeiraRede.
	    public IEnumerable<TbcBandeiraRede> ClearTbcBandeiraRede()
	    {
	        List<TbcBandeiraRede> result = new List<TbcBandeiraRede>();
	        result.Add(new TbcBandeiraRede());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear BandeiraRedeCache.
	    public IEnumerable<BandeiraRedeCache> ClearBandeiraRedeCache()
	    {
	        List<BandeiraRedeCache> result = new List<BandeiraRedeCache>();
	        result.Add(new BandeiraRedeCache());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TbcBandeiraRede.
	    public IQueryable<TbcBandeiraRede> GetTbcBandeiraRede()
	    {




		

	        IQueryable<TbcBandeiraRede> result = 
	            (from entity0 in TbcBandeiraRede.OnSearchingReplacement(this.DbContext, null, null, null) select entity0);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TbcBandeiraRedeNoAssociations.
	    public IQueryable<TbcBandeiraRede> GetTbcBandeiraRedeNoAssociations()
	    {




		

	        IQueryable<TbcBandeiraRede> result = 
	            (from entity0 in TbcBandeiraRede.OnSearchingReplacement(this.DbContext, null, null, null) select entity0);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get BandeiraRedeCache.
	    public IEnumerable<BandeiraRedeCache> GetBandeiraRedeCache()
	    {




	
	        IEnumerable<BandeiraRedeCache> result = new List<BandeiraRedeCache>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get BandeiraRedeCacheNoAssociations.
	    public IEnumerable<BandeiraRedeCache> GetBandeiraRedeCacheNoAssociations()
	    {




	
	        IEnumerable<BandeiraRedeCache> result = new List<BandeiraRedeCache>();
	  	
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	//Add filtering disabled property for TBC_BANDEIRA_REDE
	    	string[] bmDisabledTbcBandeiraRedeList = this.GetEDM().GetFilteringDisabledList("TBC_BANDEIRA_REDE");
	    	if (bmDisabledTbcBandeiraRedeList.Length > 0)
	    	{
	
	    		if (bmDisabledTbcBandeiraRedeList.Contains("TBC_BANDEIRA_REDE.COD_BANDEIRA_REDE"))
	    		{
	    			result.Add("TbcBandeiraRede|CodBandeiraRede");
	    			result.Add("TbcBandeiraRede|TBC_BANDEIRA_REDE.COD_BANDEIRA_REDE");
	    		}
	
	    		if (bmDisabledTbcBandeiraRedeList.Contains("TBC_BANDEIRA_REDE.DATA_ATUALIZACAO"))
	    		{
	    			result.Add("TbcBandeiraRede|DataAtualizacao");
	    			result.Add("TbcBandeiraRede|TBC_BANDEIRA_REDE.DATA_ATUALIZACAO");
	    		}
	
	    		if (bmDisabledTbcBandeiraRedeList.Contains("TBC_BANDEIRA_REDE.DATA_CADASTRO"))
	    		{
	    			result.Add("TbcBandeiraRede|DataCadastro");
	    			result.Add("TbcBandeiraRede|TBC_BANDEIRA_REDE.DATA_CADASTRO");
	    		}
	
	    		if (bmDisabledTbcBandeiraRedeList.Contains("TBC_BANDEIRA_REDE.DESC_BANDEIRA_REDE"))
	    		{
	    			result.Add("TbcBandeiraRede|DescBandeiraRede");
	    			result.Add("TbcBandeiraRede|TBC_BANDEIRA_REDE.DESC_BANDEIRA_REDE");
	    		}
	
	    		if (bmDisabledTbcBandeiraRedeList.Contains("TBC_BANDEIRA_REDE.ID_BANDEIRA_REDE"))
	    		{
	    			result.Add("TbcBandeiraRede|IdBandeiraRede");
	    			result.Add("TbcBandeiraRede|TBC_BANDEIRA_REDE.ID_BANDEIRA_REDE");
	    		}
	    	}
	    	return result;
	    }

	    #endregion Excluded Filters

	    #region Get By EntitySearchId Methods Definitions.
	
				
	    [Ignore]
	    //Get TbcBandeiraRede By EntitySearchId.
	    public IQueryable<TbcBandeiraRede> GetTbcBandeiraRedeByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTbcBandeiraRedeByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TbcBandeiraRede By EntitySearchId.
	    public IQueryable<TbcBandeiraRede> GetTbcBandeiraRedeByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTbcBandeiraRedeByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get BandeiraRedeCache By EntitySearchId.
	    public IEnumerable<BandeiraRedeCache> GetBandeiraRedeCacheByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetBandeiraRedeCacheByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get BandeiraRedeCache By EntitySearchId.
	    public IEnumerable<BandeiraRedeCache> GetBandeiraRedeCacheByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetBandeiraRedeCacheByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get TbcBandeiraRede By Example.
	    [Ignore]
	    public IQueryable<TbcBandeiraRede> GetTbcBandeiraRedeByExample(TbcBandeiraRede entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTbcBandeiraRedeByEntitySearch(queryAnalysis);
	    }
			
	    //Get TbcBandeiraRede By Example.
	    [Ignore]
	    public IQueryable<TbcBandeiraRede> GetTbcBandeiraRedeByExampleNoAssociations(TbcBandeiraRede entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTbcBandeiraRedeByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get BandeiraRedeCache By Example.
	    [Ignore]
	    public IEnumerable<BandeiraRedeCache> GetBandeiraRedeCacheByExample(BandeiraRedeCache entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetBandeiraRedeCacheByEntitySearch(queryAnalysis);
	    }
			
	    //Get BandeiraRedeCache By Example.
	    [Ignore]
	    public IEnumerable<BandeiraRedeCache> GetBandeiraRedeCacheByExampleNoAssociations(BandeiraRedeCache entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetBandeiraRedeCacheByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public TbcBandeiraRede GetTbcBandeiraRedeByKey(Int32 idBandeiraRede)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TbcBandeiraRede");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdBandeiraRede"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idBandeiraRede));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTbcBandeiraRedeByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public BandeiraRedeCache GetBandeiraRedeCacheByKey(string hash)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("BandeiraRedeCache");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "Hash"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, hash));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetBandeiraRedeCacheByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get TbcBandeiraRedeByEntitySearch.
	    public IQueryable<TbcBandeiraRede> GetTbcBandeiraRedeByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TbcBandeiraRede));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		

	        IQueryable<TbcBandeiraRede> result = 
	            (from entity0 in TbcBandeiraRede.OnSearchingReplacement(this.DbContext, dynQuery, parameters, entitySearchList) select entity0);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TbcBandeiraRedeByEntitySearchNoAssociations.
	    public IQueryable<TbcBandeiraRede> GetTbcBandeiraRedeByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TbcBandeiraRede));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		

	        IQueryable<TbcBandeiraRede> result = 
	            (from entity0 in TbcBandeiraRede.OnSearchingReplacement(this.DbContext, dynQuery, parameters, entitySearchList) select entity0);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get BandeiraRedeCacheByEntitySearch.
	    public IEnumerable<BandeiraRedeCache> GetBandeiraRedeCacheByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<BandeiraRedeCache> result = new List<BandeiraRedeCache>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get BandeiraRedeCacheByEntitySearchNoAssociations.
	    public IEnumerable<BandeiraRedeCache> GetBandeiraRedeCacheByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<BandeiraRedeCache> result = new List<BandeiraRedeCache>();
	  	
	
	        	

	
	        return result;
	    }
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get PagedTbcBandeiraRede.
	    public IQueryable<TbcBandeiraRede> GetPagedTbcBandeiraRede(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TbcBandeiraRede));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		

	        IQueryable<TbcBandeiraRede> result = 
	            (from entity0 in TbcBandeiraRede.OnSearchingReplacement(this.DbContext, dynQuery, parameters, entitySearchList) select entity0);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTbcBandeiraRedeCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TbcBandeiraRede));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TBC_BANDEIRA_REDE.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedBandeiraRedeCache.
	    public IEnumerable<BandeiraRedeCache> GetPagedBandeiraRedeCache(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<BandeiraRedeCache> result = new List<BandeiraRedeCache>();
	  	
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetBandeiraRedeCacheCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    
	    [Update()]	
	    //Update TbcBandeiraRede.
	    public void UpdateTbcBandeiraRede(TbcBandeiraRede entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TbcBandeiraRede.
	    public void InsertTbcBandeiraRede(TbcBandeiraRede entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TbcBandeiraRede.
	    public void DeleteTbcBandeiraRede(TbcBandeiraRede entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update BandeiraRedeCache.
	    public void UpdateBandeiraRedeCache(BandeiraRedeCache entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert BandeiraRedeCache.
	    public void InsertBandeiraRedeCache(BandeiraRedeCache entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete BandeiraRedeCache.
	    public void DeleteBandeiraRedeCache(BandeiraRedeCache entity)
	    {



	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}