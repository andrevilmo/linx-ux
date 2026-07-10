					
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

namespace Linx.Framework.BV.Conexao
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_CONEXAO_DB.ID_CONEXAO_DB", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsConexaoDb];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdConexaoDb];ReadOnly[false];Entities[TCS_CONEXAO_DB:IdConexaoDb];SubQueryInfo[];EdmEntityName[TCS_CONEXAO_DB];EntityRelations[];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsConexaoDb")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Conexao.TcsConexaoDb")]
	public partial class TcsConexaoDb : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For IdConexaoDb
	    partial void OnIdConexaoDbChanging(Int32 value);
	    partial void OnIdConexaoDbChanged();

	    private Int32 _IdConexaoDb;

	    [DataMember(IsRequired = true, Name = "IdConexaoDb", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Conexao Db", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_CONEXAO_DB.ID_CONEXAO_DB];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_CONEXAO_DB.ID_CONEXAO_DB")]
	    public Int32 IdConexaoDb
	    {
	    	    get
	    	    {
	    	          return _IdConexaoDb;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdConexaoDb != value)
	    	          {
	    	              this.ValidateProperty("IdConexaoDb", value);
	    	              this.OnIdConexaoDbChanging(value);
	    	              this.RaiseDataMemberChanging("IdConexaoDb");
	    	              this._IdConexaoDb = value;
	    	              this.RaiseDataMemberChanged("IdConexaoDb");
	    	              this.OnIdConexaoDbChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeConexao
	    partial void OnNomeConexaoChanging(System.String value);
	    partial void OnNomeConexaoChanged();

	    private System.String _NomeConexao;

	    [DataMember(IsRequired = true, Name = "NomeConexao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Provider BM", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_CONEXAO_DB.NOME_CONEXAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_CONEXAO_DB.NOME_CONEXAO")]
	    public System.String NomeConexao
	    {
	    	    get
	    	    {
	    	          return _NomeConexao;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeConexao != value)
	    	          {
	    	              this.ValidateProperty("NomeConexao", value);
	    	              this.OnNomeConexaoChanging(value);
	    	              this.RaiseDataMemberChanging("NomeConexao");
	    	              this._NomeConexao = value;
	    	              this.RaiseDataMemberChanged("NomeConexao");
	    	              this.OnNomeConexaoChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIdConexaoDb;
	    [DataMember(Name = "TemporaryIdConexaoDb", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Conexao Db (Tmp)", Description="Temporary Key", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdConexaoDb
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdConexaoDb.IsNullOrEmpty())
	    	                this._TemporaryIdConexaoDb = this._IdConexaoDb;
	    	          return this._TemporaryIdConexaoDb;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdConexaoDb != value)
	    	              this._TemporaryIdConexaoDb = value;
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_CONEXAO_DB").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_CONEXAO_DB), QualifiedEntitySetName = "AutorizacaoContext.TCS_CONEXAO_DB" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_CONEXAO_DB.NOME_CONEXAO", Source = "NomeConexao", Target = "NOME_CONEXAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_CONEXAO_DB", RelationPropertyName = "TCS_CONEXAO_DB" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_CONEXAO_DB.ID_CONEXAO_DB", Source = "IdConexaoDb", Target = "ID_CONEXAO_DB", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_CONEXAO_DB", RelationPropertyName = "TCS_CONEXAO_DB" });

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

		

	[LinxPublicationView(PrimaryKeys="TCS_BANCO_SERVIDOR.ID_TCS_BANCO_SERVIDOR", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsBancoServidor,TcsBancoServidor.TcsAmbienteConexao];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsBancoServidor];ReadOnly[false];Entities[TCS_BANCO_SERVIDOR:IdTcsBancoServidor];SubQueryInfo[];EdmEntityName[TCS_BANCO_SERVIDOR];EntityRelations[];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsBancoServidor")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Conexao.TcsBancoServidor")]
	public partial class TcsBancoServidor : Linx.Data.Entity
	{

	
		
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.TcsAmbienteConexaoList != null && this.TcsAmbienteConexaoList.Count() > 0)
	      {
	         foreach (var entity in this.TcsAmbienteConexaoList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.TcsAmbienteConexaoList != null)
	      {
	         foreach (var detail in this.TcsAmbienteConexaoList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsAmbienteConexaoList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(ConexaoDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("TcsAmbienteConexao"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsAmbienteConexao");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsBancoServidor"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdTcsBancoServidor));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsAmbienteConexao and all sub-details
	         if (this.TcsAmbienteConexaoList == null || this.TcsAmbienteConexaoList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsAmbienteConexaoList = context.GetPagedTcsAmbienteConexao(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsAmbienteConexaoList = (from r in context.GetTcsAmbienteConexaoByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _TcsAmbienteConexaoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsAmbienteConexao && ((TcsAmbienteConexao)e.Entity).TcsBancoServidor == null && e.Associations == null && e.OriginalAssociations == null && ((TcsAmbienteConexao)e.Entity).IdTcsBancoServidor == this.IdTcsBancoServidor).ToList();
 	      if (_TcsAmbienteConexaoElements.Count > 0 && this.TcsAmbienteConexaoList.Count() == 0)
 	      {
 	          this.TcsAmbienteConexaoList = _TcsAmbienteConexaoElements.Select(e => (TcsAmbienteConexao)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsAmbienteConexaoElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsAmbienteConexao)detail.Entity).TcsBancoServidor = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsBancoServidor", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsAmbienteConexaoList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For DescricaoBancoServidor
	    partial void OnDescricaoBancoServidorChanging(System.String value);
	    partial void OnDescricaoBancoServidorChanged();

	    private System.String _DescricaoBancoServidor;

	    [DataMember(IsRequired = true, Name = "DescricaoBancoServidor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição Conexão Banco/Servidor", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(80)]
	    [FunctionalPoint("Precision[80:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_BANCO_SERVIDOR.DESCRICAO_BANCO_SERVIDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_BANCO_SERVIDOR.DESCRICAO_BANCO_SERVIDOR")]
	    public System.String DescricaoBancoServidor
	    {
	    	    get
	    	    {
	    	          return _DescricaoBancoServidor;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescricaoBancoServidor != value)
	    	          {
	    	              this.ValidateProperty("DescricaoBancoServidor", value);
	    	              this.OnDescricaoBancoServidorChanging(value);
	    	              this.RaiseDataMemberChanging("DescricaoBancoServidor");
	    	              this._DescricaoBancoServidor = value;
	    	              this.RaiseDataMemberChanged("DescricaoBancoServidor");
	    	              this.OnDescricaoBancoServidorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsBancoServidor
	    partial void OnIdTcsBancoServidorChanging(Int32 value);
	    partial void OnIdTcsBancoServidorChanged();

	    private Int32 _IdTcsBancoServidor;

	    [DataMember(IsRequired = true, Name = "IdTcsBancoServidor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Banco Servidor", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_BANCO_SERVIDOR.ID_TCS_BANCO_SERVIDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_BANCO_SERVIDOR.ID_TCS_BANCO_SERVIDOR")]
	    public Int32 IdTcsBancoServidor
	    {
	    	    get
	    	    {
	    	          return _IdTcsBancoServidor;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsBancoServidor != value)
	    	          {
	    	              this.ValidateProperty("IdTcsBancoServidor", value);
	    	              this.OnIdTcsBancoServidorChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsBancoServidor");
	    	              this._IdTcsBancoServidor = value;
	    	              this.RaiseDataMemberChanged("IdTcsBancoServidor");
	    	              this.OnIdTcsBancoServidorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Incremento
	    partial void OnIncrementoChanging(Int32 value);
	    partial void OnIncrementoChanged();

	    private Int32 _Incremento;

	    [DataMember(IsRequired = true, Name = "Incremento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Incremento", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_BANCO_SERVIDOR.INCREMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_BANCO_SERVIDOR.INCREMENTO")]
	    public Int32 Incremento
	    {
	    	    get
	    	    {
	    	          return _Incremento;
	    	    }
	    	    set
	    	    {
	    	          if (this._Incremento != value)
	    	          {
	    	              this.ValidateProperty("Incremento", value);
	    	              this.OnIncrementoChanging(value);
	    	              this.RaiseDataMemberChanging("Incremento");
	    	              this._Incremento = value;
	    	              this.RaiseDataMemberChanged("Incremento");
	    	              this.OnIncrementoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoServidor
	    partial void OnLxTipoServidorChanging(Byte value);
	    partial void OnLxTipoServidorChanged();

	    private Byte _LxTipoServidor;

	    [DataMember(IsRequired = true, Name = "LxTipoServidor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo Servidor", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoServidor];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_BANCO_SERVIDOR.LX_TIPO_SERVIDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_BANCO_SERVIDOR.LX_TIPO_SERVIDOR")]
	    public Byte LxTipoServidor
	    {
	    	    get
	    	    {
	    	          return _LxTipoServidor;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoServidor != value)
	    	          {
	    	              this.ValidateProperty("LxTipoServidor", value);
	    	              this.OnLxTipoServidorChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoServidor");
	    	              this._LxTipoServidor = value;
	    	              this.RaiseDataMemberChanged("LxTipoServidor");
	    	              this.OnLxTipoServidorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeBanco
	    partial void OnNomeBancoChanging(System.String value);
	    partial void OnNomeBancoChanged();

	    private System.String _NomeBanco;

	    [DataMember(IsRequired = true, Name = "NomeBanco", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Banco de Dados", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_BANCO_SERVIDOR.NOME_BANCO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_BANCO_SERVIDOR.NOME_BANCO")]
	    public System.String NomeBanco
	    {
	    	    get
	    	    {
	    	          return _NomeBanco;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeBanco != value)
	    	          {
	    	              this.ValidateProperty("NomeBanco", value);
	    	              this.OnNomeBancoChanging(value);
	    	              this.RaiseDataMemberChanging("NomeBanco");
	    	              this._NomeBanco = value;
	    	              this.RaiseDataMemberChanged("NomeBanco");
	    	              this.OnNomeBancoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeServidor
	    partial void OnNomeServidorChanging(System.String value);
	    partial void OnNomeServidorChanged();

	    private System.String _NomeServidor;

	    [DataMember(IsRequired = true, Name = "NomeServidor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Servidor", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_BANCO_SERVIDOR.NOME_SERVIDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_BANCO_SERVIDOR.NOME_SERVIDOR")]
	    public System.String NomeServidor
	    {
	    	    get
	    	    {
	    	          return _NomeServidor;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeServidor != value)
	    	          {
	    	              this.ValidateProperty("NomeServidor", value);
	    	              this.OnNomeServidorChanging(value);
	    	              this.RaiseDataMemberChanging("NomeServidor");
	    	              this._NomeServidor = value;
	    	              this.RaiseDataMemberChanged("NomeServidor");
	    	              this.OnNomeServidorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For SequencialInicial
	    partial void OnSequencialInicialChanging(Int64 value);
	    partial void OnSequencialInicialChanged();

	    private Int64 _SequencialInicial;

	    [DataMember(IsRequired = true, Name = "SequencialInicial", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Sequencial Inicial", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_BANCO_SERVIDOR.SEQUENCIAL_INICIAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_BANCO_SERVIDOR.SEQUENCIAL_INICIAL")]
	    public Int64 SequencialInicial
	    {
	    	    get
	    	    {
	    	          return _SequencialInicial;
	    	    }
	    	    set
	    	    {
	    	          if (this._SequencialInicial != value)
	    	          {
	    	              this.ValidateProperty("SequencialInicial", value);
	    	              this.OnSequencialInicialChanging(value);
	    	              this.RaiseDataMemberChanging("SequencialInicial");
	    	              this._SequencialInicial = value;
	    	              this.RaiseDataMemberChanged("SequencialInicial");
	    	              this.OnSequencialInicialChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringConexao
	    partial void OnStringConexaoChanging(System.String value);
	    partial void OnStringConexaoChanged();

	    private System.String _StringConexao;

	    [DataMember(IsRequired = true, Name = "StringConexao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Conexão", Description="Variáveis disponíveis para substituição :\r\n@banco = Banco de Dados\r\n@provider = Nome Provider - BM\r\n@servidor = Servidor ", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(1000)]
	    [FunctionalPoint("Precision[1000:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_BANCO_SERVIDOR.STRING_CONEXAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_BANCO_SERVIDOR.STRING_CONEXAO")]
	    public System.String StringConexao
	    {
	    	    get
	    	    {
	    	          return _StringConexao;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringConexao != value)
	    	          {
	    	              this.ValidateProperty("StringConexao", value);
	    	              this.OnStringConexaoChanging(value);
	    	              this.RaiseDataMemberChanging("StringConexao");
	    	              this._StringConexao = value;
	    	              this.RaiseDataMemberChanged("StringConexao");
	    	              this.OnStringConexaoChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIdTcsBancoServidor;
	    [DataMember(Name = "TemporaryIdTcsBancoServidor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Banco Servidor (Tmp)", Description="Temporary Key", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdTcsBancoServidor
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTcsBancoServidor.IsNullOrEmpty())
	    	                this._TemporaryIdTcsBancoServidor = this._IdTcsBancoServidor;
	    	          return this._TemporaryIdTcsBancoServidor;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTcsBancoServidor != value)
	    	              this._TemporaryIdTcsBancoServidor = value;
	    	    }
	    }	

	    #endregion Data Properties

	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<TcsAmbienteConexao> _TcsAmbienteConexaoList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsBancoServidor_TcsAmbienteConexao", "IdTcsBancoServidor", "IdTcsBancoServidor", IsForeignKey=false)]
	    [DataMember(Name = "TcsAmbienteConexaoList", EmitDefaultValue = true)]
	    public IEnumerable<TcsAmbienteConexao> TcsAmbienteConexaoList
	    {
	        get
	        {
	
	            if (this._TcsAmbienteConexaoList == null)
	            	this._TcsAmbienteConexaoList = new List<TcsAmbienteConexao>();
	
	            return this._TcsAmbienteConexaoList;
	        }
	        set
	        {
	            if (this._TcsAmbienteConexaoList != value)
	            {
	                this._TcsAmbienteConexaoList = value;
	                this.RaisePropertyChanged("TcsAmbienteConexaoList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_BANCO_SERVIDOR").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_BANCO_SERVIDOR), QualifiedEntitySetName = "AutorizacaoContext.TCS_BANCO_SERVIDOR" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_BANCO_SERVIDOR.INCREMENTO", Source = "Incremento", Target = "INCREMENTO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_BANCO_SERVIDOR", RelationPropertyName = "TCS_BANCO_SERVIDOR" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_BANCO_SERVIDOR.NOME_BANCO", Source = "NomeBanco", Target = "NOME_BANCO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_BANCO_SERVIDOR", RelationPropertyName = "TCS_BANCO_SERVIDOR" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_BANCO_SERVIDOR.NOME_SERVIDOR", Source = "NomeServidor", Target = "NOME_SERVIDOR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_BANCO_SERVIDOR", RelationPropertyName = "TCS_BANCO_SERVIDOR" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_BANCO_SERVIDOR.STRING_CONEXAO", Source = "StringConexao", Target = "STRING_CONEXAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_BANCO_SERVIDOR", RelationPropertyName = "TCS_BANCO_SERVIDOR" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_BANCO_SERVIDOR.LX_TIPO_SERVIDOR", Source = "LxTipoServidor", Target = "LX_TIPO_SERVIDOR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_BANCO_SERVIDOR", RelationPropertyName = "TCS_BANCO_SERVIDOR" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_BANCO_SERVIDOR.SEQUENCIAL_INICIAL", Source = "SequencialInicial", Target = "SEQUENCIAL_INICIAL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_BANCO_SERVIDOR", RelationPropertyName = "TCS_BANCO_SERVIDOR" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_BANCO_SERVIDOR.ID_TCS_BANCO_SERVIDOR", Source = "IdTcsBancoServidor", Target = "ID_TCS_BANCO_SERVIDOR", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_BANCO_SERVIDOR", RelationPropertyName = "TCS_BANCO_SERVIDOR" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_BANCO_SERVIDOR.DESCRICAO_BANCO_SERVIDOR", Source = "DescricaoBancoServidor", Target = "DESCRICAO_BANCO_SERVIDOR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_BANCO_SERVIDOR", RelationPropertyName = "TCS_BANCO_SERVIDOR" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxTipoServidorValues()
	    {
	    	    return Linx.Framework.BV.Domains.TipoServidor.GetValues();
	    }
	    private string _lxTipoServidorName;
	    [DataMember(IsRequired = false, Name = "LxTipoServidorName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo Servidor", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxTipoServidorName
	    {
	    	    get { if (this.LxTipoServidor.IsNull()) { _lxTipoServidorName = String.Empty; } else { string key = this.LxTipoServidor.ToString(); var dmValues = this.GetLxTipoServidorValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxTipoServidorName) _lxTipoServidorName = domainName; } return _lxTipoServidorName; } set { _lxTipoServidorName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_AMBIENTE_CONEXAO.ID_TCS_AMBIENTE_CONEXAO", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Providers - BM];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsAmbienteConexao];ReadOnly[false];Entities[TCS_AMBIENTE_CONEXAO:IdTcsAmbienteConexao|TCS_AMBIENTE:IdTcsAmbiente|TCS_APLICATIVO_CONEXAO:IdTcsAplicativoConexao];SubQueryInfo[Select 1 From #ParentAlias#.TCS_AMBIENTE_CONEXAO_LISTA as #Alias#];EdmEntityName[TCS_AMBIENTE_CONEXAO];EntityRelations[TCS_AMBIENTE(TCS_AMBIENTE)#TCS_APLICACAO(TCS_APLICACAO)#TCS_APLICATIVO(TCS_APLICATIVO)#TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)#TCS_BANCO_SERVIDOR(TCS_BANCO_SERVIDOR)#TCS_APLICATIVO_CONEXAO(TCS_APLICATIVO_CONEXAO)#TCS_CONEXAO_DB(TCS_CONEXAO_DB)];EdmParentEntityName[TCS_BANCO_SERVIDOR];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsAmbienteConexao")]
	[Serializable()]
	public partial class TcsAmbienteConexao : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(ConexaoDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsBancoServidor");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsBancoServidor"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdTcsBancoServidor));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsBancoServidor
	         this.TcsBancoServidor = (from r in context.GetTcsBancoServidorByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For DescricaoAmbiente
	    partial void OnDescricaoAmbienteChanging(System.String value);
	    partial void OnDescricaoAmbienteChanged();

	    private System.String _DescricaoAmbiente;

	    [DataMember(IsRequired = true, Name = "DescricaoAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ambiente", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente];LookUpTitle[Seleção de (Ambiente)];LookUpQuery[executeLookUpTcsAmbiente];LookUpFinalize[finalizeLookUpTcsAmbiente];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\", \"DescricaoAplicacao\" : \"Aplicação\", \"NomeEmpresa\" : \"Empresa (Id Linx)\", \"IdLinx\" : \"(Id Linx)\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"IdTcsAmbiente\" : false, \"DescricaoAplicacao\" : true, \"NomeEmpresa\" : true, \"IdLinx\" : true}];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.DESCRICAO_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoAmbiente#false##250:0##Ambiente#0#true##::LookUpTcsAmbiente##false#false#TCS_AMBIENTE#TCS_AMBIENTE#Linx.Framework.BV.Conexao#IQueryable#DescricaoAplicacao[DescricaoAplicacao]#DescricaoAmbiente[DescricaoAplicacao=DescricaoAplicacao,NomeEmpresa=NomeEmpresa,IdLinx=IdLinx];IdTcsAmbiente[DescricaoAplicacao=DescricaoAplicacao,NomeEmpresa=NomeEmpresa,IdLinx=IdLinx]#true#false", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.DESCRICAO_AMBIENTE")]
	    public System.String DescricaoAmbiente
	    {
	    	    get
	    	    {
	    	          return _DescricaoAmbiente;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescricaoAmbiente != value)
	    	          {
	    	              this.ValidateProperty("DescricaoAmbiente", value);
	    	              this.OnDescricaoAmbienteChanging(value);
	    	              this.RaiseDataMemberChanging("DescricaoAmbiente");
	    	              this._DescricaoAmbiente = value;
	    	              this.RaiseDataMemberChanged("DescricaoAmbiente");
	    	              this.OnDescricaoAmbienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescricaoAplicacao
	    partial void OnDescricaoAplicacaoChanging(System.String value);
	    partial void OnDescricaoAplicacaoChanged();

	    private System.String _DescricaoAplicacao;

	    [DataMember(IsRequired = true, Name = "DescricaoAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicação", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente];LookUpTitle[Seleção de (Aplicação)];LookUpQuery[executeLookUpTcsAmbiente];LookUpFinalize[finalizeLookUpTcsAmbiente];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\", \"DescricaoAplicacao\" : \"Aplicação\", \"NomeEmpresa\" : \"Empresa (Id Linx)\", \"IdLinx\" : \"(Id Linx)\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"IdTcsAmbiente\" : false, \"DescricaoAplicacao\" : true, \"NomeEmpresa\" : true, \"IdLinx\" : true}];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.TCS_APLICACAO.DESCRICAO_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoAplicacao#false##60:0##Aplicação#2#true##::LookUpTcsAmbiente##false#false#TCS_AMBIENTE#TCS_AMBIENTE#Linx.Framework.BV.Conexao#IQueryable#DescricaoAplicacao[DescricaoAplicacao]#DescricaoAmbiente[DescricaoAplicacao=DescricaoAplicacao,NomeEmpresa=NomeEmpresa,IdLinx=IdLinx];IdTcsAmbiente[DescricaoAplicacao=DescricaoAplicacao,NomeEmpresa=NomeEmpresa,IdLinx=IdLinx]#true#false", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.TCS_APLICACAO.DESCRICAO_APLICACAO")]
	    public System.String DescricaoAplicacao
	    {
	    	    get
	    	    {
	    	          return _DescricaoAplicacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescricaoAplicacao != value)
	    	          {
	    	              this.ValidateProperty("DescricaoAplicacao", value);
	    	              this.OnDescricaoAplicacaoChanging(value);
	    	              this.RaiseDataMemberChanging("DescricaoAplicacao");
	    	              this._DescricaoAplicacao = value;
	    	              this.RaiseDataMemberChanged("DescricaoAplicacao");
	    	              this.OnDescricaoAplicacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescricaoAplicativo
	    partial void OnDescricaoAplicativoChanging(System.String value);
	    partial void OnDescricaoAplicativoChanged();

	    private System.String _DescricaoAplicativo;

	    [DataMember(Name = "DescricaoAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicativo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicativoConexao];LookUpTitle[Seleção de (Aplicativo)];LookUpQuery[executeLookUpTcsAplicativoConexao];LookUpFinalize[finalizeLookUpTcsAplicativoConexao];LookUpDisplayColumns[{\"IdTcsAplicativoConexao\" : \"Id Tcs Aplicativo Conexao\", \"NomeConexao\" : \"Nome Provider BM\", \"DescricaoAplicativo\" : \"Aplicativo\"}];LookUpColumns[{\"IdTcsAplicativoConexao\" : false, \"NomeConexao\" : true, \"DescricaoAplicativo\" : true}];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_APLICATIVO_CONEXAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoAplicativo#false##250:0##Aplicativo#2#true##::LookUpTcsAplicativoConexao##false#false#TCS_APLICATIVO_CONEXAO#TCS_APLICATIVO_CONEXAO#Linx.Framework.BV.Conexao#IQueryable#NomeConexao[NomeConexao]#IdTcsAplicativoConexao[NomeConexao=NomeConexao,DescricaoAplicativo=DescricaoAplicativo]#true#false", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_APLICATIVO_CONEXAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO")]
	    public System.String DescricaoAplicativo
	    {
	    	    get
	    	    {
	    	          return _DescricaoAplicativo;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescricaoAplicativo != value)
	    	          {
	    	              this.ValidateProperty("DescricaoAplicativo", value);
	    	              this.OnDescricaoAplicativoChanging(value);
	    	              this.RaiseDataMemberChanging("DescricaoAplicativo");
	    	              this._DescricaoAplicativo = value;
	    	              this.RaiseDataMemberChanged("DescricaoAplicativo");
	    	              this.OnDescricaoAplicativoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLinx
	    partial void OnIdLinxChanging(Int32 value);
	    partial void OnIdLinxChanged();

	    private Int32 _IdLinx;

	    [DataMember(IsRequired = true, Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "(Id Linx)", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente];LookUpTitle[Seleção de ((Id Linx))];LookUpQuery[executeLookUpTcsAmbiente];LookUpFinalize[finalizeLookUpTcsAmbiente];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\", \"DescricaoAplicacao\" : \"Aplicação\", \"NomeEmpresa\" : \"Empresa (Id Linx)\", \"IdLinx\" : \"(Id Linx)\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"IdTcsAmbiente\" : false, \"DescricaoAplicacao\" : true, \"NomeEmpresa\" : true, \"IdLinx\" : true}];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdLinx#false##12:0##(Id Linx)#4#true##::LookUpTcsAmbiente##false#false#TCS_AMBIENTE#TCS_AMBIENTE#Linx.Framework.BV.Conexao#IQueryable#DescricaoAplicacao[DescricaoAplicacao]#DescricaoAmbiente[DescricaoAplicacao=DescricaoAplicacao,NomeEmpresa=NomeEmpresa,IdLinx=IdLinx];IdTcsAmbiente[DescricaoAplicacao=DescricaoAplicacao,NomeEmpresa=NomeEmpresa,IdLinx=IdLinx]#true#false", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
	    public Int32 IdLinx
	    {
	    	    get
	    	    {
	    	          return _IdLinx;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLinx != value)
	    	          {
	    	              this.ValidateProperty("IdLinx", value);
	    	              this.OnIdLinxChanging(value);
	    	              this.RaiseDataMemberChanging("IdLinx");
	    	              this._IdLinx = value;
	    	              this.RaiseDataMemberChanged("IdLinx");
	    	              this.OnIdLinxChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsAmbiente
	    partial void OnIdTcsAmbienteChanging(Int32 value);
	    partial void OnIdTcsAmbienteChanged();

	    private Int32 _IdTcsAmbiente;

	    [DataMember(IsRequired = true, Name = "IdTcsAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente];LookUpTitle[Seleção de (Id Tcs Ambiente)];LookUpQuery[executeLookUpTcsAmbiente];LookUpFinalize[finalizeLookUpTcsAmbiente];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\", \"DescricaoAplicacao\" : \"Aplicação\", \"NomeEmpresa\" : \"Empresa (Id Linx)\", \"IdLinx\" : \"(Id Linx)\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"IdTcsAmbiente\" : false, \"DescricaoAplicacao\" : true, \"NomeEmpresa\" : true, \"IdLinx\" : true}];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.ID_TCS_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdTcsAmbiente#true##12:0##Id Tcs Ambiente#1#false##::LookUpTcsAmbiente##false#false#TCS_AMBIENTE#TCS_AMBIENTE#Linx.Framework.BV.Conexao#IQueryable#DescricaoAplicacao[DescricaoAplicacao]#DescricaoAmbiente[DescricaoAplicacao=DescricaoAplicacao,NomeEmpresa=NomeEmpresa,IdLinx=IdLinx];IdTcsAmbiente[DescricaoAplicacao=DescricaoAplicacao,NomeEmpresa=NomeEmpresa,IdLinx=IdLinx]#true#false", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.ID_TCS_AMBIENTE")]
	    public Int32 IdTcsAmbiente
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
	    //Extensibility Partial Method Definitions For IdTcsAmbienteConexao
	    partial void OnIdTcsAmbienteConexaoChanging(Int32 value);
	    partial void OnIdTcsAmbienteConexaoChanged();

	    private Int32 _IdTcsAmbienteConexao;

	    [DataMember(IsRequired = true, Name = "IdTcsAmbienteConexao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente Conexao", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_AMBIENTE_CONEXAO.ID_TCS_AMBIENTE_CONEXAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE_CONEXAO.ID_TCS_AMBIENTE_CONEXAO")]
	    public Int32 IdTcsAmbienteConexao
	    {
	    	    get
	    	    {
	    	          return _IdTcsAmbienteConexao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAmbienteConexao != value)
	    	          {
	    	              this.ValidateProperty("IdTcsAmbienteConexao", value);
	    	              this.OnIdTcsAmbienteConexaoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsAmbienteConexao");
	    	              this._IdTcsAmbienteConexao = value;
	    	              this.RaiseDataMemberChanged("IdTcsAmbienteConexao");
	    	              this.OnIdTcsAmbienteConexaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsAplicativoConexao
	    partial void OnIdTcsAplicativoConexaoChanging(Int32 value);
	    partial void OnIdTcsAplicativoConexaoChanged();

	    private Int32 _IdTcsAplicativoConexao;

	    [DataMember(IsRequired = true, Name = "IdTcsAplicativoConexao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Aplicativo Conexao", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicativoConexao];LookUpTitle[Seleção de (Id Tcs Aplicativo Conexao)];LookUpQuery[executeLookUpTcsAplicativoConexao];LookUpFinalize[finalizeLookUpTcsAplicativoConexao];LookUpDisplayColumns[{\"IdTcsAplicativoConexao\" : \"Id Tcs Aplicativo Conexao\", \"NomeConexao\" : \"Nome Provider BM\", \"DescricaoAplicativo\" : \"Aplicativo\"}];LookUpColumns[{\"IdTcsAplicativoConexao\" : false, \"NomeConexao\" : true, \"DescricaoAplicativo\" : true}];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_APLICATIVO_CONEXAO.ID_TCS_APLICATIVO_CONEXAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdTcsAplicativoConexao#true##12:0##Id Tcs Aplicativo Conexao#0#false##::LookUpTcsAplicativoConexao##false#false#TCS_APLICATIVO_CONEXAO#TCS_APLICATIVO_CONEXAO#Linx.Framework.BV.Conexao#IQueryable#NomeConexao[NomeConexao]#IdTcsAplicativoConexao[NomeConexao=NomeConexao,DescricaoAplicativo=DescricaoAplicativo]#true#false", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_APLICATIVO_CONEXAO.ID_TCS_APLICATIVO_CONEXAO")]
	    public Int32 IdTcsAplicativoConexao
	    {
	    	    get
	    	    {
	    	          return _IdTcsAplicativoConexao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAplicativoConexao != value)
	    	          {
	    	              this.ValidateProperty("IdTcsAplicativoConexao", value);
	    	              this.OnIdTcsAplicativoConexaoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsAplicativoConexao");
	    	              this._IdTcsAplicativoConexao = value;
	    	              this.RaiseDataMemberChanged("IdTcsAplicativoConexao");
	    	              this.OnIdTcsAplicativoConexaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsBancoServidor
	    partial void OnIdTcsBancoServidorChanging(Int32 value);
	    partial void OnIdTcsBancoServidorChanged();

	    private Int32 _IdTcsBancoServidor;

	    [DataMember(IsRequired = true, Name = "IdTcsBancoServidor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Banco Servidor", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.ID_TCS_BANCO_SERVIDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.ID_TCS_BANCO_SERVIDOR")]
	    public Int32 IdTcsBancoServidor
	    {
	    	    get
	    	    {
	    	          return _IdTcsBancoServidor;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsBancoServidor != value)
	    	          {
	    	              this.ValidateProperty("IdTcsBancoServidor", value);
	    	              this.OnIdTcsBancoServidorChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsBancoServidor");
	    	              this._IdTcsBancoServidor = value;
	    	              this.RaiseDataMemberChanged("IdTcsBancoServidor");
	    	              this.OnIdTcsBancoServidorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeConexao
	    partial void OnNomeConexaoChanging(System.String value);
	    partial void OnNomeConexaoChanged();

	    private System.String _NomeConexao;

	    [DataMember(IsRequired = true, Name = "NomeConexao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Provider BM", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicativoConexao];LookUpTitle[Seleção de (Nome Provider BM)];LookUpQuery[executeLookUpTcsAplicativoConexao];LookUpFinalize[finalizeLookUpTcsAplicativoConexao];LookUpDisplayColumns[{\"IdTcsAplicativoConexao\" : \"Id Tcs Aplicativo Conexao\", \"NomeConexao\" : \"Nome Provider BM\", \"DescricaoAplicativo\" : \"Aplicativo\"}];LookUpColumns[{\"IdTcsAplicativoConexao\" : false, \"NomeConexao\" : true, \"DescricaoAplicativo\" : true}];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB.NOME_CONEXAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeConexao#false##250:0##Nome Provider BM#1#true##::LookUpTcsAplicativoConexao##false#false#TCS_APLICATIVO_CONEXAO#TCS_APLICATIVO_CONEXAO#Linx.Framework.BV.Conexao#IQueryable#NomeConexao[NomeConexao]#IdTcsAplicativoConexao[NomeConexao=NomeConexao,DescricaoAplicativo=DescricaoAplicativo]#true#false", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB.NOME_CONEXAO")]
	    public System.String NomeConexao
	    {
	    	    get
	    	    {
	    	          return _NomeConexao;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeConexao != value)
	    	          {
	    	              this.ValidateProperty("NomeConexao", value);
	    	              this.OnNomeConexaoChanging(value);
	    	              this.RaiseDataMemberChanging("NomeConexao");
	    	              this._NomeConexao = value;
	    	              this.RaiseDataMemberChanged("NomeConexao");
	    	              this.OnNomeConexaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeEmpresa
	    partial void OnNomeEmpresaChanging(System.String value);
	    partial void OnNomeEmpresaChanged();

	    private System.String _NomeEmpresa;

	    [DataMember(IsRequired = true, Name = "NomeEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Empresa (Id Linx)", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente];LookUpTitle[Seleção de (Empresa (Id Linx))];LookUpQuery[executeLookUpTcsAmbiente];LookUpFinalize[finalizeLookUpTcsAmbiente];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\", \"DescricaoAplicacao\" : \"Aplicação\", \"NomeEmpresa\" : \"Empresa (Id Linx)\", \"IdLinx\" : \"(Id Linx)\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"IdTcsAmbiente\" : false, \"DescricaoAplicacao\" : true, \"NomeEmpresa\" : true, \"IdLinx\" : true}];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeEmpresa#false##250:0##Empresa (Id Linx)#3#true##::LookUpTcsAmbiente##false#false#TCS_AMBIENTE#TCS_AMBIENTE#Linx.Framework.BV.Conexao#IQueryable#DescricaoAplicacao[DescricaoAplicacao]#DescricaoAmbiente[DescricaoAplicacao=DescricaoAplicacao,NomeEmpresa=NomeEmpresa,IdLinx=IdLinx];IdTcsAmbiente[DescricaoAplicacao=DescricaoAplicacao,NomeEmpresa=NomeEmpresa,IdLinx=IdLinx]#true#false", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
	    public System.String NomeEmpresa
	    {
	    	    get
	    	    {
	    	          return _NomeEmpresa;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeEmpresa != value)
	    	          {
	    	              this.ValidateProperty("NomeEmpresa", value);
	    	              this.OnNomeEmpresaChanging(value);
	    	              this.RaiseDataMemberChanging("NomeEmpresa");
	    	              this._NomeEmpresa = value;
	    	              this.RaiseDataMemberChanged("NomeEmpresa");
	    	              this.OnNomeEmpresaChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIdTcsAmbienteConexao;
	    [DataMember(Name = "TemporaryIdTcsAmbienteConexao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente Conexao (Tmp)", Description="Temporary Key", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdTcsAmbienteConexao
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTcsAmbienteConexao.IsNullOrEmpty())
	    	                this._TemporaryIdTcsAmbienteConexao = this._IdTcsAmbienteConexao;
	    	          return this._TemporaryIdTcsAmbienteConexao;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTcsAmbienteConexao != value)
	    	              this._TemporaryIdTcsAmbienteConexao = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsBancoServidor _TcsBancoServidor;
	    [DataMember(Name = "TcsBancoServidor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsBancoServidor_TcsAmbienteConexao", "IdTcsBancoServidor", "IdTcsBancoServidor", IsForeignKey=true)]
	    public TcsBancoServidor TcsBancoServidor
	    {
	        get
	        {
	            return this._TcsBancoServidor;
	        }
	        set
	        {
	            if (this._TcsBancoServidor != value)
	            {
	                this._TcsBancoServidor = value;
	                this.RaisePropertyChanged("TcsBancoServidorList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_AMBIENTE_CONEXAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_AMBIENTE_CONEXAO), QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE_CONEXAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_AMBIENTE_CONEXAO.ID_TCS_AMBIENTE_CONEXAO", Source = "IdTcsAmbienteConexao", Target = "ID_TCS_AMBIENTE_CONEXAO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE_CONEXAO", RelationPropertyName = "TCS_AMBIENTE_CONEXAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.ID_TCS_AMBIENTE", Source = "IdTcsAmbiente", Target = "ID_TCS_AMBIENTE", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE", RelationPropertyName = "TCS_AMBIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.ID_TCS_BANCO_SERVIDOR", Source = "IdTcsBancoServidor", Target = "ID_TCS_BANCO_SERVIDOR", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_BANCO_SERVIDOR", RelationPropertyName = "TCS_BANCO_SERVIDOR" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_AMBIENTE_CONEXAO.TCS_APLICATIVO_CONEXAO.ID_TCS_APLICATIVO_CONEXAO", Source = "IdTcsAplicativoConexao", Target = "ID_TCS_APLICATIVO_CONEXAO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICATIVO_CONEXAO", RelationPropertyName = "TCS_APLICATIVO_CONEXAO" });

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

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Providers - BM];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsAmbienteConexao];ReadOnly[false];Entities[TCS_AMBIENTE_CONEXAO:IdTcsAmbienteConexao|TCS_AMBIENTE:IdTcsAmbiente|TCS_APLICATIVO_CONEXAO:IdTcsAplicativoConexao];SubQueryInfo[Select 1 From #ParentAlias#.TCS_AMBIENTE_CONEXAO_LISTA as #Alias#];EdmEntityName[TCS_AMBIENTE_CONEXAO];EntityRelations[TCS_AMBIENTE(TCS_AMBIENTE)#TCS_APLICACAO(TCS_APLICACAO)#TCS_APLICATIVO(TCS_APLICATIVO)#TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)#TCS_BANCO_SERVIDOR(TCS_BANCO_SERVIDOR)#TCS_APLICATIVO_CONEXAO(TCS_APLICATIVO_CONEXAO)#TCS_CONEXAO_DB(TCS_CONEXAO_DB)];EdmParentEntityName[TCS_BANCO_SERVIDOR];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsAmbienteConexao")]
	[Serializable()]
	public partial class TcsAmbienteConexaoParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For DescricaoAmbiente
	    partial void OnDescricaoAmbienteChanging(System.String value);
	    partial void OnDescricaoAmbienteChanged();

	    private System.String _DescricaoAmbiente;

	    [DataMember(IsRequired = true, Name = "DescricaoAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ambiente", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente];LookUpTitle[Seleção de (Ambiente)];LookUpQuery[executeLookUpTcsAmbiente];LookUpFinalize[finalizeLookUpTcsAmbiente];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\", \"DescricaoAplicacao\" : \"Aplicação\", \"NomeEmpresa\" : \"Empresa (Id Linx)\", \"IdLinx\" : \"(Id Linx)\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"IdTcsAmbiente\" : false, \"DescricaoAplicacao\" : true, \"NomeEmpresa\" : true, \"IdLinx\" : true}];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.DESCRICAO_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoAmbiente#false##250:0##Ambiente#0#true##::LookUpTcsAmbiente##false#false#TCS_AMBIENTE#TCS_AMBIENTE#Linx.Framework.BV.Conexao#IQueryable#DescricaoAplicacao[DescricaoAplicacao]#DescricaoAmbiente[DescricaoAplicacao=DescricaoAplicacao,NomeEmpresa=NomeEmpresa,IdLinx=IdLinx];IdTcsAmbiente[DescricaoAplicacao=DescricaoAplicacao,NomeEmpresa=NomeEmpresa,IdLinx=IdLinx]#true#false", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.DESCRICAO_AMBIENTE")]
	    public System.String DescricaoAmbiente
	    {
	    	    get
	    	    {
	    	          return _DescricaoAmbiente;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescricaoAmbiente != value)
	    	          {
	    	              this.ValidateProperty("DescricaoAmbiente", value);
	    	              this.OnDescricaoAmbienteChanging(value);
	    	              this.RaiseDataMemberChanging("DescricaoAmbiente");
	    	              this._DescricaoAmbiente = value;
	    	              this.RaiseDataMemberChanged("DescricaoAmbiente");
	    	              this.OnDescricaoAmbienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescricaoAplicacao
	    partial void OnDescricaoAplicacaoChanging(System.String value);
	    partial void OnDescricaoAplicacaoChanged();

	    private System.String _DescricaoAplicacao;

	    [DataMember(IsRequired = true, Name = "DescricaoAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicação", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente];LookUpTitle[Seleção de (Aplicação)];LookUpQuery[executeLookUpTcsAmbiente];LookUpFinalize[finalizeLookUpTcsAmbiente];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\", \"DescricaoAplicacao\" : \"Aplicação\", \"NomeEmpresa\" : \"Empresa (Id Linx)\", \"IdLinx\" : \"(Id Linx)\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"IdTcsAmbiente\" : false, \"DescricaoAplicacao\" : true, \"NomeEmpresa\" : true, \"IdLinx\" : true}];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.TCS_APLICACAO.DESCRICAO_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoAplicacao#false##60:0##Aplicação#2#true##::LookUpTcsAmbiente##false#false#TCS_AMBIENTE#TCS_AMBIENTE#Linx.Framework.BV.Conexao#IQueryable#DescricaoAplicacao[DescricaoAplicacao]#DescricaoAmbiente[DescricaoAplicacao=DescricaoAplicacao,NomeEmpresa=NomeEmpresa,IdLinx=IdLinx];IdTcsAmbiente[DescricaoAplicacao=DescricaoAplicacao,NomeEmpresa=NomeEmpresa,IdLinx=IdLinx]#true#false", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.TCS_APLICACAO.DESCRICAO_APLICACAO")]
	    public System.String DescricaoAplicacao
	    {
	    	    get
	    	    {
	    	          return _DescricaoAplicacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescricaoAplicacao != value)
	    	          {
	    	              this.ValidateProperty("DescricaoAplicacao", value);
	    	              this.OnDescricaoAplicacaoChanging(value);
	    	              this.RaiseDataMemberChanging("DescricaoAplicacao");
	    	              this._DescricaoAplicacao = value;
	    	              this.RaiseDataMemberChanged("DescricaoAplicacao");
	    	              this.OnDescricaoAplicacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescricaoAplicativo
	    partial void OnDescricaoAplicativoChanging(System.String value);
	    partial void OnDescricaoAplicativoChanged();

	    private System.String _DescricaoAplicativo;

	    [DataMember(Name = "DescricaoAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicativo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicativoConexao];LookUpTitle[Seleção de (Aplicativo)];LookUpQuery[executeLookUpTcsAplicativoConexao];LookUpFinalize[finalizeLookUpTcsAplicativoConexao];LookUpDisplayColumns[{\"IdTcsAplicativoConexao\" : \"Id Tcs Aplicativo Conexao\", \"NomeConexao\" : \"Nome Provider BM\", \"DescricaoAplicativo\" : \"Aplicativo\"}];LookUpColumns[{\"IdTcsAplicativoConexao\" : false, \"NomeConexao\" : true, \"DescricaoAplicativo\" : true}];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_APLICATIVO_CONEXAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoAplicativo#false##250:0##Aplicativo#2#true##::LookUpTcsAplicativoConexao##false#false#TCS_APLICATIVO_CONEXAO#TCS_APLICATIVO_CONEXAO#Linx.Framework.BV.Conexao#IQueryable#NomeConexao[NomeConexao]#IdTcsAplicativoConexao[NomeConexao=NomeConexao,DescricaoAplicativo=DescricaoAplicativo]#true#false", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_APLICATIVO_CONEXAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO")]
	    public System.String DescricaoAplicativo
	    {
	    	    get
	    	    {
	    	          return _DescricaoAplicativo;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescricaoAplicativo != value)
	    	          {
	    	              this.ValidateProperty("DescricaoAplicativo", value);
	    	              this.OnDescricaoAplicativoChanging(value);
	    	              this.RaiseDataMemberChanging("DescricaoAplicativo");
	    	              this._DescricaoAplicativo = value;
	    	              this.RaiseDataMemberChanged("DescricaoAplicativo");
	    	              this.OnDescricaoAplicativoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLinx
	    partial void OnIdLinxChanging(Int32 value);
	    partial void OnIdLinxChanged();

	    private Int32 _IdLinx;

	    [DataMember(IsRequired = true, Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "(Id Linx)", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente];LookUpTitle[Seleção de ((Id Linx))];LookUpQuery[executeLookUpTcsAmbiente];LookUpFinalize[finalizeLookUpTcsAmbiente];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\", \"DescricaoAplicacao\" : \"Aplicação\", \"NomeEmpresa\" : \"Empresa (Id Linx)\", \"IdLinx\" : \"(Id Linx)\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"IdTcsAmbiente\" : false, \"DescricaoAplicacao\" : true, \"NomeEmpresa\" : true, \"IdLinx\" : true}];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdLinx#false##12:0##(Id Linx)#4#true##::LookUpTcsAmbiente##false#false#TCS_AMBIENTE#TCS_AMBIENTE#Linx.Framework.BV.Conexao#IQueryable#DescricaoAplicacao[DescricaoAplicacao]#DescricaoAmbiente[DescricaoAplicacao=DescricaoAplicacao,NomeEmpresa=NomeEmpresa,IdLinx=IdLinx];IdTcsAmbiente[DescricaoAplicacao=DescricaoAplicacao,NomeEmpresa=NomeEmpresa,IdLinx=IdLinx]#true#false", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
	    public Int32 IdLinx
	    {
	    	    get
	    	    {
	    	          return _IdLinx;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLinx != value)
	    	          {
	    	              this.ValidateProperty("IdLinx", value);
	    	              this.OnIdLinxChanging(value);
	    	              this.RaiseDataMemberChanging("IdLinx");
	    	              this._IdLinx = value;
	    	              this.RaiseDataMemberChanged("IdLinx");
	    	              this.OnIdLinxChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsAmbiente
	    partial void OnIdTcsAmbienteChanging(Int32 value);
	    partial void OnIdTcsAmbienteChanged();

	    private Int32 _IdTcsAmbiente;

	    [DataMember(IsRequired = true, Name = "IdTcsAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente];LookUpTitle[Seleção de (Id Tcs Ambiente)];LookUpQuery[executeLookUpTcsAmbiente];LookUpFinalize[finalizeLookUpTcsAmbiente];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\", \"DescricaoAplicacao\" : \"Aplicação\", \"NomeEmpresa\" : \"Empresa (Id Linx)\", \"IdLinx\" : \"(Id Linx)\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"IdTcsAmbiente\" : false, \"DescricaoAplicacao\" : true, \"NomeEmpresa\" : true, \"IdLinx\" : true}];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.ID_TCS_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdTcsAmbiente#true##12:0##Id Tcs Ambiente#1#false##::LookUpTcsAmbiente##false#false#TCS_AMBIENTE#TCS_AMBIENTE#Linx.Framework.BV.Conexao#IQueryable#DescricaoAplicacao[DescricaoAplicacao]#DescricaoAmbiente[DescricaoAplicacao=DescricaoAplicacao,NomeEmpresa=NomeEmpresa,IdLinx=IdLinx];IdTcsAmbiente[DescricaoAplicacao=DescricaoAplicacao,NomeEmpresa=NomeEmpresa,IdLinx=IdLinx]#true#false", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.ID_TCS_AMBIENTE")]
	    public Int32 IdTcsAmbiente
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
	    //Extensibility Partial Method Definitions For IdTcsAmbienteConexao
	    partial void OnIdTcsAmbienteConexaoChanging(Int32 value);
	    partial void OnIdTcsAmbienteConexaoChanged();

	    private Int32 _IdTcsAmbienteConexao;

	    [DataMember(IsRequired = true, Name = "IdTcsAmbienteConexao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente Conexao", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_AMBIENTE_CONEXAO.ID_TCS_AMBIENTE_CONEXAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE_CONEXAO.ID_TCS_AMBIENTE_CONEXAO")]
	    public Int32 IdTcsAmbienteConexao
	    {
	    	    get
	    	    {
	    	          return _IdTcsAmbienteConexao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAmbienteConexao != value)
	    	          {
	    	              this.ValidateProperty("IdTcsAmbienteConexao", value);
	    	              this.OnIdTcsAmbienteConexaoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsAmbienteConexao");
	    	              this._IdTcsAmbienteConexao = value;
	    	              this.RaiseDataMemberChanged("IdTcsAmbienteConexao");
	    	              this.OnIdTcsAmbienteConexaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsAplicativoConexao
	    partial void OnIdTcsAplicativoConexaoChanging(Int32 value);
	    partial void OnIdTcsAplicativoConexaoChanged();

	    private Int32 _IdTcsAplicativoConexao;

	    [DataMember(IsRequired = true, Name = "IdTcsAplicativoConexao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Aplicativo Conexao", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicativoConexao];LookUpTitle[Seleção de (Id Tcs Aplicativo Conexao)];LookUpQuery[executeLookUpTcsAplicativoConexao];LookUpFinalize[finalizeLookUpTcsAplicativoConexao];LookUpDisplayColumns[{\"IdTcsAplicativoConexao\" : \"Id Tcs Aplicativo Conexao\", \"NomeConexao\" : \"Nome Provider BM\", \"DescricaoAplicativo\" : \"Aplicativo\"}];LookUpColumns[{\"IdTcsAplicativoConexao\" : false, \"NomeConexao\" : true, \"DescricaoAplicativo\" : true}];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_APLICATIVO_CONEXAO.ID_TCS_APLICATIVO_CONEXAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdTcsAplicativoConexao#true##12:0##Id Tcs Aplicativo Conexao#0#false##::LookUpTcsAplicativoConexao##false#false#TCS_APLICATIVO_CONEXAO#TCS_APLICATIVO_CONEXAO#Linx.Framework.BV.Conexao#IQueryable#NomeConexao[NomeConexao]#IdTcsAplicativoConexao[NomeConexao=NomeConexao,DescricaoAplicativo=DescricaoAplicativo]#true#false", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_APLICATIVO_CONEXAO.ID_TCS_APLICATIVO_CONEXAO")]
	    public Int32 IdTcsAplicativoConexao
	    {
	    	    get
	    	    {
	    	          return _IdTcsAplicativoConexao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAplicativoConexao != value)
	    	          {
	    	              this.ValidateProperty("IdTcsAplicativoConexao", value);
	    	              this.OnIdTcsAplicativoConexaoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsAplicativoConexao");
	    	              this._IdTcsAplicativoConexao = value;
	    	              this.RaiseDataMemberChanged("IdTcsAplicativoConexao");
	    	              this.OnIdTcsAplicativoConexaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsBancoServidor
	    partial void OnIdTcsBancoServidorChanging(Int32 value);
	    partial void OnIdTcsBancoServidorChanged();

	    private Int32 _IdTcsBancoServidor;

	    [DataMember(IsRequired = true, Name = "IdTcsBancoServidor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Banco Servidor", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.ID_TCS_BANCO_SERVIDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.ID_TCS_BANCO_SERVIDOR")]
	    public Int32 IdTcsBancoServidor
	    {
	    	    get
	    	    {
	    	          return _IdTcsBancoServidor;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsBancoServidor != value)
	    	          {
	    	              this.ValidateProperty("IdTcsBancoServidor", value);
	    	              this.OnIdTcsBancoServidorChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsBancoServidor");
	    	              this._IdTcsBancoServidor = value;
	    	              this.RaiseDataMemberChanged("IdTcsBancoServidor");
	    	              this.OnIdTcsBancoServidorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeConexao
	    partial void OnNomeConexaoChanging(System.String value);
	    partial void OnNomeConexaoChanged();

	    private System.String _NomeConexao;

	    [DataMember(IsRequired = true, Name = "NomeConexao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Provider BM", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicativoConexao];LookUpTitle[Seleção de (Nome Provider BM)];LookUpQuery[executeLookUpTcsAplicativoConexao];LookUpFinalize[finalizeLookUpTcsAplicativoConexao];LookUpDisplayColumns[{\"IdTcsAplicativoConexao\" : \"Id Tcs Aplicativo Conexao\", \"NomeConexao\" : \"Nome Provider BM\", \"DescricaoAplicativo\" : \"Aplicativo\"}];LookUpColumns[{\"IdTcsAplicativoConexao\" : false, \"NomeConexao\" : true, \"DescricaoAplicativo\" : true}];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB.NOME_CONEXAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeConexao#false##250:0##Nome Provider BM#1#true##::LookUpTcsAplicativoConexao##false#false#TCS_APLICATIVO_CONEXAO#TCS_APLICATIVO_CONEXAO#Linx.Framework.BV.Conexao#IQueryable#NomeConexao[NomeConexao]#IdTcsAplicativoConexao[NomeConexao=NomeConexao,DescricaoAplicativo=DescricaoAplicativo]#true#false", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB.NOME_CONEXAO")]
	    public System.String NomeConexao
	    {
	    	    get
	    	    {
	    	          return _NomeConexao;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeConexao != value)
	    	          {
	    	              this.ValidateProperty("NomeConexao", value);
	    	              this.OnNomeConexaoChanging(value);
	    	              this.RaiseDataMemberChanging("NomeConexao");
	    	              this._NomeConexao = value;
	    	              this.RaiseDataMemberChanged("NomeConexao");
	    	              this.OnNomeConexaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeEmpresa
	    partial void OnNomeEmpresaChanging(System.String value);
	    partial void OnNomeEmpresaChanged();

	    private System.String _NomeEmpresa;

	    [DataMember(IsRequired = true, Name = "NomeEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Empresa (Id Linx)", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente];LookUpTitle[Seleção de (Empresa (Id Linx))];LookUpQuery[executeLookUpTcsAmbiente];LookUpFinalize[finalizeLookUpTcsAmbiente];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\", \"DescricaoAplicacao\" : \"Aplicação\", \"NomeEmpresa\" : \"Empresa (Id Linx)\", \"IdLinx\" : \"(Id Linx)\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"IdTcsAmbiente\" : false, \"DescricaoAplicacao\" : true, \"NomeEmpresa\" : true, \"IdLinx\" : true}];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeEmpresa#false##250:0##Empresa (Id Linx)#3#true##::LookUpTcsAmbiente##false#false#TCS_AMBIENTE#TCS_AMBIENTE#Linx.Framework.BV.Conexao#IQueryable#DescricaoAplicacao[DescricaoAplicacao]#DescricaoAmbiente[DescricaoAplicacao=DescricaoAplicacao,NomeEmpresa=NomeEmpresa,IdLinx=IdLinx];IdTcsAmbiente[DescricaoAplicacao=DescricaoAplicacao,NomeEmpresa=NomeEmpresa,IdLinx=IdLinx]#true#false", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
	    public System.String NomeEmpresa
	    {
	    	    get
	    	    {
	    	          return _NomeEmpresa;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeEmpresa != value)
	    	          {
	    	              this.ValidateProperty("NomeEmpresa", value);
	    	              this.OnNomeEmpresaChanging(value);
	    	              this.RaiseDataMemberChanging("NomeEmpresa");
	    	              this._NomeEmpresa = value;
	    	              this.RaiseDataMemberChanged("NomeEmpresa");
	    	              this.OnNomeEmpresaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescricaoBancoServidor
	    partial void OnDescricaoBancoServidorChanging(System.String value);
	    partial void OnDescricaoBancoServidorChanged();

	    private System.String _DescricaoBancoServidor;

	    [DataMember(IsRequired = true, Name = "DescricaoBancoServidor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição Conexão Banco/Servidor", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(80)]
	    [FunctionalPoint("Precision[80:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.DESCRICAO_BANCO_SERVIDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_BANCO_SERVIDOR.DESCRICAO_BANCO_SERVIDOR")]
	    public System.String DescricaoBancoServidor
	    {
	    	    get
	    	    {
	    	          return _DescricaoBancoServidor;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescricaoBancoServidor != value)
	    	          {
	    	              this.ValidateProperty("DescricaoBancoServidor", value);
	    	              this.OnDescricaoBancoServidorChanging(value);
	    	              this.RaiseDataMemberChanging("DescricaoBancoServidor");
	    	              this._DescricaoBancoServidor = value;
	    	              this.RaiseDataMemberChanged("DescricaoBancoServidor");
	    	              this.OnDescricaoBancoServidorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Incremento
	    partial void OnIncrementoChanging(Int32 value);
	    partial void OnIncrementoChanged();

	    private Int32 _Incremento;

	    [DataMember(IsRequired = true, Name = "Incremento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Incremento", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.INCREMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_BANCO_SERVIDOR.INCREMENTO")]
	    public Int32 Incremento
	    {
	    	    get
	    	    {
	    	          return _Incremento;
	    	    }
	    	    set
	    	    {
	    	          if (this._Incremento != value)
	    	          {
	    	              this.ValidateProperty("Incremento", value);
	    	              this.OnIncrementoChanging(value);
	    	              this.RaiseDataMemberChanging("Incremento");
	    	              this._Incremento = value;
	    	              this.RaiseDataMemberChanged("Incremento");
	    	              this.OnIncrementoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoServidor
	    partial void OnLxTipoServidorChanging(Byte value);
	    partial void OnLxTipoServidorChanged();

	    private Byte _LxTipoServidor;

	    [DataMember(IsRequired = true, Name = "LxTipoServidor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo Servidor", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoServidor];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.LX_TIPO_SERVIDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_BANCO_SERVIDOR.LX_TIPO_SERVIDOR")]
	    public Byte LxTipoServidor
	    {
	    	    get
	    	    {
	    	          return _LxTipoServidor;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoServidor != value)
	    	          {
	    	              this.ValidateProperty("LxTipoServidor", value);
	    	              this.OnLxTipoServidorChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoServidor");
	    	              this._LxTipoServidor = value;
	    	              this.RaiseDataMemberChanged("LxTipoServidor");
	    	              this.OnLxTipoServidorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeBanco
	    partial void OnNomeBancoChanging(System.String value);
	    partial void OnNomeBancoChanged();

	    private System.String _NomeBanco;

	    [DataMember(IsRequired = true, Name = "NomeBanco", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Banco de Dados", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.NOME_BANCO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_BANCO_SERVIDOR.NOME_BANCO")]
	    public System.String NomeBanco
	    {
	    	    get
	    	    {
	    	          return _NomeBanco;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeBanco != value)
	    	          {
	    	              this.ValidateProperty("NomeBanco", value);
	    	              this.OnNomeBancoChanging(value);
	    	              this.RaiseDataMemberChanging("NomeBanco");
	    	              this._NomeBanco = value;
	    	              this.RaiseDataMemberChanged("NomeBanco");
	    	              this.OnNomeBancoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeServidor
	    partial void OnNomeServidorChanging(System.String value);
	    partial void OnNomeServidorChanged();

	    private System.String _NomeServidor;

	    [DataMember(IsRequired = true, Name = "NomeServidor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Servidor", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.NOME_SERVIDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_BANCO_SERVIDOR.NOME_SERVIDOR")]
	    public System.String NomeServidor
	    {
	    	    get
	    	    {
	    	          return _NomeServidor;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeServidor != value)
	    	          {
	    	              this.ValidateProperty("NomeServidor", value);
	    	              this.OnNomeServidorChanging(value);
	    	              this.RaiseDataMemberChanging("NomeServidor");
	    	              this._NomeServidor = value;
	    	              this.RaiseDataMemberChanged("NomeServidor");
	    	              this.OnNomeServidorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For SequencialInicial
	    partial void OnSequencialInicialChanging(Int64 value);
	    partial void OnSequencialInicialChanged();

	    private Int64 _SequencialInicial;

	    [DataMember(IsRequired = true, Name = "SequencialInicial", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Sequencial Inicial", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.SEQUENCIAL_INICIAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_BANCO_SERVIDOR.SEQUENCIAL_INICIAL")]
	    public Int64 SequencialInicial
	    {
	    	    get
	    	    {
	    	          return _SequencialInicial;
	    	    }
	    	    set
	    	    {
	    	          if (this._SequencialInicial != value)
	    	          {
	    	              this.ValidateProperty("SequencialInicial", value);
	    	              this.OnSequencialInicialChanging(value);
	    	              this.RaiseDataMemberChanging("SequencialInicial");
	    	              this._SequencialInicial = value;
	    	              this.RaiseDataMemberChanged("SequencialInicial");
	    	              this.OnSequencialInicialChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringConexao
	    partial void OnStringConexaoChanging(System.String value);
	    partial void OnStringConexaoChanged();

	    private System.String _StringConexao;

	    [DataMember(IsRequired = true, Name = "StringConexao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Conexão", Description="Variáveis disponíveis para substituição :\r\n@banco = Banco de Dados\r\n@provider = Nome Provider - BM\r\n@servidor = Servidor ", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(1000)]
	    [FunctionalPoint("Precision[1000:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.STRING_CONEXAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_BANCO_SERVIDOR.STRING_CONEXAO")]
	    public System.String StringConexao
	    {
	    	    get
	    	    {
	    	          return _StringConexao;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringConexao != value)
	    	          {
	    	              this.ValidateProperty("StringConexao", value);
	    	              this.OnStringConexaoChanging(value);
	    	              this.RaiseDataMemberChanging("StringConexao");
	    	              this._StringConexao = value;
	    	              this.RaiseDataMemberChanged("StringConexao");
	    	              this.OnStringConexaoChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_AMBIENTE_CONEXAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_AMBIENTE_CONEXAO), QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE_CONEXAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_AMBIENTE_CONEXAO.ID_TCS_AMBIENTE_CONEXAO", Source = "IdTcsAmbienteConexao", Target = "ID_TCS_AMBIENTE_CONEXAO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE_CONEXAO", RelationPropertyName = "TCS_AMBIENTE_CONEXAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.ID_TCS_AMBIENTE", Source = "IdTcsAmbiente", Target = "ID_TCS_AMBIENTE", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE", RelationPropertyName = "TCS_AMBIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.ID_TCS_BANCO_SERVIDOR", Source = "IdTcsBancoServidor", Target = "ID_TCS_BANCO_SERVIDOR", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_BANCO_SERVIDOR", RelationPropertyName = "TCS_BANCO_SERVIDOR" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_AMBIENTE_CONEXAO.TCS_APLICATIVO_CONEXAO.ID_TCS_APLICATIVO_CONEXAO", Source = "IdTcsAplicativoConexao", Target = "ID_TCS_APLICATIVO_CONEXAO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICATIVO_CONEXAO", RelationPropertyName = "TCS_APLICATIVO_CONEXAO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxTipoServidorValues()
	    {
	    	    return Linx.Framework.BV.Domains.TipoServidor.GetValues();
	    }
	    private string _lxTipoServidorName;
	    [DataMember(IsRequired = false, Name = "LxTipoServidorName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo Servidor", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxTipoServidorName
	    {
	    	    get { if (this.LxTipoServidor.IsNull()) { _lxTipoServidorName = String.Empty; } else { string key = this.LxTipoServidor.ToString(); var dmValues = this.GetLxTipoServidorValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxTipoServidorName) _lxTipoServidorName = domainName; } return _lxTipoServidorName; } set { _lxTipoServidorName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	//////////////////////// DomainService Class V1 ///////////////////////
	///////////////////////////////////////////////////////////////////////
	[EnableClientAccess()]	
	[DomainIdentifier("ProcessorOverviewConexaoDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class ConexaoDomainService : DomainService, IDataServiceContext 
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

		
	    public ConexaoDomainService() : this("", null, null) { }
	    public ConexaoDomainService(string connectionString) : this(connectionString, null, null) { }
	    public ConexaoDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public ConexaoDomainService(Linx.Framework.Autorizacao.BM.AutorizacaoContext dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public ConexaoDomainService(string connectionString, Linx.Framework.Autorizacao.BM.AutorizacaoContext dataContext, Dictionary<string, string> headers) : base() 
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

		
 
 	        bool createNewChangeSet = false;
 
 	        //Adjust data hierarchy
 	        var _TcsBancoServidorElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsBancoServidor && e.Entity.GetType().Name == "TcsBancoServidor" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _TcsBancoServidorElements)
 	           if (((TcsBancoServidor)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 
 	        //Remove inconsistent details
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsAmbienteConexao && e.Entity.GetType().Name == "TcsAmbienteConexao" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
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
	    //Get All LookUpTcsAmbiente.
	    public IQueryable<LookUpTcsAmbiente> GetAllLookUpTcsAmbiente()
	    {
	        return this.GetLookUpTcsAmbiente(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsAmbiente By EntitySearch.
	    public IQueryable<LookUpTcsAmbiente> GetLookUpTcsAmbienteByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsAmbiente(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsAmbiente.
	    public IQueryable<LookUpTcsAmbiente> GetLookUpTcsAmbiente(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TCS_AMBIENTE" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsAmbiente";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsAmbiente));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsAmbiente> query =  
	
	            (from entity in this.DbContext.TCS_AMBIENTE.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_APLICACAO
                  let entityAl2 = entity.TCS_EMPRESA_AUTENTICACAO
	            
	            select new LookUpTcsAmbiente()		
	            {
	            
                DescricaoAmbiente = entity.DESCRICAO_AMBIENTE
                , IdTcsAmbiente = entity.ID_TCS_AMBIENTE
                , DescricaoAplicacao = entityAl1.DESCRICAO_APLICACAO
                , NomeEmpresa = entityAl2.NOME_EMPRESA
                , IdLinx = entityAl2.ID_LINX
	            });

	            
            //Inner Group Definition
            if (propertyName.InList("DescricaoAplicacao"))
            {
               query = (from r in query select new LookUpTcsAmbiente() {
               DescricaoAmbiente = ""
               , IdTcsAmbiente = default(Int32)
               , DescricaoAplicacao = r.DescricaoAplicacao
               , NomeEmpresa = ""
               , IdLinx = default(Int32)
                }).Distinct();
            }
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpTcsAplicativoConexao.
	    public IQueryable<LookUpTcsAplicativoConexao> GetAllLookUpTcsAplicativoConexao()
	    {
	        return this.GetLookUpTcsAplicativoConexao(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsAplicativoConexao By EntitySearch.
	    public IQueryable<LookUpTcsAplicativoConexao> GetLookUpTcsAplicativoConexaoByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsAplicativoConexao(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsAplicativoConexao.
	    public IQueryable<LookUpTcsAplicativoConexao> GetLookUpTcsAplicativoConexao(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TCS_APLICATIVO_CONEXAO" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsAplicativoConexao";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsAplicativoConexao));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsAplicativoConexao> query =  
	
	            (from entity in this.DbContext.TCS_APLICATIVO_CONEXAO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_CONEXAO_DB
                  let entityAl2 = entity.TCS_APLICATIVO
	            
	            select new LookUpTcsAplicativoConexao()		
	            {
	            
                IdTcsAplicativoConexao = entity.ID_TCS_APLICATIVO_CONEXAO
                , NomeConexao = entityAl1.NOME_CONEXAO
                , DescricaoAplicativo = entityAl2.DESCRICAO_APLICATIVO
	            });

	            
            //Inner Group Definition
            if (propertyName.InList("NomeConexao"))
            {
               query = (from r in query select new LookUpTcsAplicativoConexao() {
               IdTcsAplicativoConexao = default(Int32)
               , NomeConexao = r.NomeConexao
               , DescricaoAplicativo = ""
                }).Distinct();
            }
	
		
	
	
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
	
		

	        if (entityName.InList("Linx.Framework.BV.Conexao.TcsConexaoDb"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsConexaoDb",
	        			NameSpace = "Linx.Framework.BV.Conexao",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsConexaoDb",
	        			ClearMethodName = "ClearTcsConexaoDb",
	        			QueryMethodName  = "GetPagedTcsConexaoDb",	
	        			CountingMethodName  = "GetTcsConexaoDb" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Conexao.TcsConexaoDb"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Conexao.TcsConexaoDb"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Conexao.TcsBancoServidor"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsBancoServidor",
	        			NameSpace = "Linx.Framework.BV.Conexao",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsBancoServidor",
	        			ClearMethodName = "ClearTcsBancoServidor",
	        			QueryMethodName  = "GetPagedTcsBancoServidor",	
	        			CountingMethodName  = "GetTcsBancoServidor" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Conexao.TcsBancoServidor"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Conexao.TcsBancoServidor"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Conexao.TcsBancoServidor", "Linx.Framework.BV.Conexao.TcsAmbienteConexao"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsAmbienteConexao" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.Conexao",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsBancoServidor",	
	        			DisplayName = "Providers - BM",
	        			ClearMethodName = "ClearTcsAmbienteConexao" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsAmbienteConexao" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsAmbienteConexao" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Conexao.TcsAmbienteConexao"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Conexao.TcsAmbienteConexao" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
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

         		    return new string[] { "Framework_ConexaoClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.ConexaoClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Framework_conexaoService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.conexaoService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear TcsConexaoDb.
	    public IEnumerable<TcsConexaoDb> ClearTcsConexaoDb()
	    {
	        List<TcsConexaoDb> result = new List<TcsConexaoDb>();
	        result.Add(new TcsConexaoDb());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsBancoServidor.
	    public IEnumerable<TcsBancoServidor> ClearTcsBancoServidor()
	    {
	        List<TcsBancoServidor> result = new List<TcsBancoServidor>();
	        result.Add(new TcsBancoServidor());	
			
	        result[0].TcsAmbienteConexaoList = new List<TcsAmbienteConexao>();
	        ((List<TcsAmbienteConexao>)result[0].TcsAmbienteConexaoList).Add(new TcsAmbienteConexao());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsAmbienteConexao.
	    public IEnumerable<TcsAmbienteConexao> ClearTcsAmbienteConexao()
	    {
	        List<TcsAmbienteConexao> result = new List<TcsAmbienteConexao>();
	        result.Add(new TcsAmbienteConexao());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsConexaoDb.
	    public IQueryable<TcsConexaoDb> GetTcsConexaoDb()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsConexaoDb> result = 
	            (from entity0 in this.DbContext.TCS_CONEXAO_DB
	            
	            	
	            select new TcsConexaoDb()		
	            {
	            
                IdConexaoDb = entity0.ID_CONEXAO_DB
                , NomeConexao = entity0.NOME_CONEXAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsConexaoDbNoAssociations.
	    public IQueryable<TcsConexaoDb> GetTcsConexaoDbNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsConexaoDb> result = 
	            (from entity0 in this.DbContext.TCS_CONEXAO_DB
	            
	            	
	            select new TcsConexaoDb()		
	            {
	            
                IdConexaoDb = entity0.ID_CONEXAO_DB
                , NomeConexao = entity0.NOME_CONEXAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsBancoServidor.
	    public IQueryable<TcsBancoServidor> GetTcsBancoServidor()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsBancoServidor> result = 
	            (from entity0 in this.DbContext.TCS_BANCO_SERVIDOR
	            
	            	
	            select new TcsBancoServidor()		
	            {
	            
                DescricaoBancoServidor = entity0.DESCRICAO_BANCO_SERVIDOR
                , IdTcsBancoServidor = entity0.ID_TCS_BANCO_SERVIDOR
                , Incremento = entity0.INCREMENTO
                , LxTipoServidor = entity0.LX_TIPO_SERVIDOR
                , LxTipoServidorName = ((entity0.LX_TIPO_SERVIDOR) == 2 ? "Oracle" : ((entity0.LX_TIPO_SERVIDOR) == 3 ? "SQLite" : ((entity0.LX_TIPO_SERVIDOR) == 1 ? "SQL Server" : "")))
                , NomeBanco = entity0.NOME_BANCO
                , NomeServidor = entity0.NOME_SERVIDOR
                , SequencialInicial = entity0.SEQUENCIAL_INICIAL
                , StringConexao = entity0.STRING_CONEXAO
			
                ,TcsAmbienteConexaoList = 
	                        (from entity1 in entity0.TCS_AMBIENTE_CONEXAO_LISTA
                                  let entity1Al1 = entity1.TCS_AMBIENTE
                                  let entity1Al6 = entity1.TCS_BANCO_SERVIDOR
                                  let entity1Al5 = entity1.TCS_APLICATIVO_CONEXAO
                                  let entity1Al2 = entity1.TCS_AMBIENTE.TCS_APLICACAO
                                  let entity1Al3 = entity1.TCS_APLICATIVO_CONEXAO.TCS_APLICATIVO
                                  let entity1Al4 = entity1.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                                  let entity1Al7 = entity1.TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB
	                        
	                        	
	                        select new TcsAmbienteConexao()
	                        {
	                        
                                DescricaoAmbiente = entity1Al1.DESCRICAO_AMBIENTE
                                , DescricaoAplicacao = entity1Al2.DESCRICAO_APLICACAO
                                , DescricaoAplicativo = entity1Al3.DESCRICAO_APLICATIVO
                                , IdLinx = entity1Al4.ID_LINX
                                , IdTcsAmbiente = entity1Al1.ID_TCS_AMBIENTE
                                , IdTcsAmbienteConexao = entity1.ID_TCS_AMBIENTE_CONEXAO
                                , IdTcsAplicativoConexao = entity1Al5.ID_TCS_APLICATIVO_CONEXAO
                                , IdTcsBancoServidor = entity1Al6.ID_TCS_BANCO_SERVIDOR
                                , NomeConexao = entity1Al7.NOME_CONEXAO
                                , NomeEmpresa = entity1Al4.NOME_EMPRESA
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsAmbienteConexao.
	    public IQueryable<TcsAmbienteConexao> GetTcsAmbienteConexao()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsAmbienteConexao> result = 
	            (from entity0 in this.DbContext.TCS_AMBIENTE_CONEXAO
                  let entity0Al1 = entity0.TCS_AMBIENTE
                  let entity0Al6 = entity0.TCS_BANCO_SERVIDOR
                  let entity0Al5 = entity0.TCS_APLICATIVO_CONEXAO
                  let entity0Al2 = entity0.TCS_AMBIENTE.TCS_APLICACAO
                  let entity0Al3 = entity0.TCS_APLICATIVO_CONEXAO.TCS_APLICATIVO
                  let entity0Al4 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al7 = entity0.TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB
	            
	            	
	            select new TcsAmbienteConexao()		
	            {
	            
                DescricaoAmbiente = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al2.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0Al3.DESCRICAO_APLICATIVO
                , IdLinx = entity0Al4.ID_LINX
                , IdTcsAmbiente = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsAmbienteConexao = entity0.ID_TCS_AMBIENTE_CONEXAO
                , IdTcsAplicativoConexao = entity0Al5.ID_TCS_APLICATIVO_CONEXAO
                , IdTcsBancoServidor = entity0Al6.ID_TCS_BANCO_SERVIDOR
                , NomeConexao = entity0Al7.NOME_CONEXAO
                , NomeEmpresa = entity0Al4.NOME_EMPRESA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsBancoServidorNoAssociations.
	    public IQueryable<TcsBancoServidor> GetTcsBancoServidorNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsBancoServidor> result = 
	            (from entity0 in this.DbContext.TCS_BANCO_SERVIDOR
	            
	            	
	            select new TcsBancoServidor()		
	            {
	            
                DescricaoBancoServidor = entity0.DESCRICAO_BANCO_SERVIDOR
                , IdTcsBancoServidor = entity0.ID_TCS_BANCO_SERVIDOR
                , Incremento = entity0.INCREMENTO
                , LxTipoServidor = entity0.LX_TIPO_SERVIDOR
                , LxTipoServidorName = ((entity0.LX_TIPO_SERVIDOR) == 2 ? "Oracle" : ((entity0.LX_TIPO_SERVIDOR) == 3 ? "SQLite" : ((entity0.LX_TIPO_SERVIDOR) == 1 ? "SQL Server" : "")))
                , NomeBanco = entity0.NOME_BANCO
                , NomeServidor = entity0.NOME_SERVIDOR
                , SequencialInicial = entity0.SEQUENCIAL_INICIAL
                , StringConexao = entity0.STRING_CONEXAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAmbienteConexaoNoAssociations.
	    public IQueryable<TcsAmbienteConexao> GetTcsAmbienteConexaoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsAmbienteConexao> result = 
	            (from entity0 in this.DbContext.TCS_AMBIENTE_CONEXAO
                  let entity0Al1 = entity0.TCS_AMBIENTE
                  let entity0Al6 = entity0.TCS_BANCO_SERVIDOR
                  let entity0Al5 = entity0.TCS_APLICATIVO_CONEXAO
                  let entity0Al2 = entity0.TCS_AMBIENTE.TCS_APLICACAO
                  let entity0Al3 = entity0.TCS_APLICATIVO_CONEXAO.TCS_APLICATIVO
                  let entity0Al4 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al7 = entity0.TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB
	            
	            	
	            select new TcsAmbienteConexao()		
	            {
	            
                DescricaoAmbiente = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al2.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0Al3.DESCRICAO_APLICATIVO
                , IdLinx = entity0Al4.ID_LINX
                , IdTcsAmbiente = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsAmbienteConexao = entity0.ID_TCS_AMBIENTE_CONEXAO
                , IdTcsAplicativoConexao = entity0Al5.ID_TCS_APLICATIVO_CONEXAO
                , IdTcsBancoServidor = entity0Al6.ID_TCS_BANCO_SERVIDOR
                , NomeConexao = entity0Al7.NOME_CONEXAO
                , NomeEmpresa = entity0Al4.NOME_EMPRESA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	//Add filtering disabled property for TCS_CONEXAO_DB
	    	string[] bmDisabledTcsConexaoDbList = this.GetEDM().GetFilteringDisabledList("TCS_CONEXAO_DB");
	    	if (bmDisabledTcsConexaoDbList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsConexaoDbList.Contains("TCS_CONEXAO_DB.ID_CONEXAO_DB"))
	    		{
	    			result.Add("TcsConexaoDb|IdConexaoDb");
	    			result.Add("TcsConexaoDb|TCS_CONEXAO_DB.ID_CONEXAO_DB");
	    		}
	
	    		if (bmDisabledTcsConexaoDbList.Contains("TCS_CONEXAO_DB.NOME_CONEXAO"))
	    		{
	    			result.Add("TcsConexaoDb|NomeConexao");
	    			result.Add("TcsConexaoDb|TCS_CONEXAO_DB.NOME_CONEXAO");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_BANCO_SERVIDOR
	    	string[] bmDisabledTcsBancoServidorList = this.GetEDM().GetFilteringDisabledList("TCS_BANCO_SERVIDOR");
	    	if (bmDisabledTcsBancoServidorList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsBancoServidorList.Contains("TCS_BANCO_SERVIDOR.DESCRICAO_BANCO_SERVIDOR"))
	    		{
	    			result.Add("TcsBancoServidor|DescricaoBancoServidor");
	    			result.Add("TcsBancoServidor|TCS_BANCO_SERVIDOR.DESCRICAO_BANCO_SERVIDOR");
	    		}
	
	    		if (bmDisabledTcsBancoServidorList.Contains("TCS_BANCO_SERVIDOR.ID_TCS_BANCO_SERVIDOR"))
	    		{
	    			result.Add("TcsBancoServidor|IdTcsBancoServidor");
	    			result.Add("TcsBancoServidor|TCS_BANCO_SERVIDOR.ID_TCS_BANCO_SERVIDOR");
	    		}
	
	    		if (bmDisabledTcsBancoServidorList.Contains("TCS_BANCO_SERVIDOR.INCREMENTO"))
	    		{
	    			result.Add("TcsBancoServidor|Incremento");
	    			result.Add("TcsBancoServidor|TCS_BANCO_SERVIDOR.INCREMENTO");
	    		}
	
	    		if (bmDisabledTcsBancoServidorList.Contains("TCS_BANCO_SERVIDOR.LX_TIPO_SERVIDOR"))
	    		{
	    			result.Add("TcsBancoServidor|LxTipoServidor");
	    			result.Add("TcsBancoServidor|TCS_BANCO_SERVIDOR.LX_TIPO_SERVIDOR");
	    		}
	
	    		if (bmDisabledTcsBancoServidorList.Contains("TCS_BANCO_SERVIDOR.NOME_BANCO"))
	    		{
	    			result.Add("TcsBancoServidor|NomeBanco");
	    			result.Add("TcsBancoServidor|TCS_BANCO_SERVIDOR.NOME_BANCO");
	    		}
	
	    		if (bmDisabledTcsBancoServidorList.Contains("TCS_BANCO_SERVIDOR.NOME_SERVIDOR"))
	    		{
	    			result.Add("TcsBancoServidor|NomeServidor");
	    			result.Add("TcsBancoServidor|TCS_BANCO_SERVIDOR.NOME_SERVIDOR");
	    		}
	
	    		if (bmDisabledTcsBancoServidorList.Contains("TCS_BANCO_SERVIDOR.SEQUENCIAL_INICIAL"))
	    		{
	    			result.Add("TcsBancoServidor|SequencialInicial");
	    			result.Add("TcsBancoServidor|TCS_BANCO_SERVIDOR.SEQUENCIAL_INICIAL");
	    		}
	
	    		if (bmDisabledTcsBancoServidorList.Contains("TCS_BANCO_SERVIDOR.STRING_CONEXAO"))
	    		{
	    			result.Add("TcsBancoServidor|StringConexao");
	    			result.Add("TcsBancoServidor|TCS_BANCO_SERVIDOR.STRING_CONEXAO");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_AMBIENTE_CONEXAO
	    	string[] bmDisabledTcsAmbienteConexaoList = this.GetEDM().GetFilteringDisabledList("TCS_AMBIENTE_CONEXAO");
	    	if (bmDisabledTcsAmbienteConexaoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsAmbienteConexaoList.Contains("TCS_AMBIENTE_CONEXAO.ID_TCS_AMBIENTE_CONEXAO"))
	    		{
	    			result.Add("TcsAmbienteConexao|IdTcsAmbienteConexao");
	    			result.Add("TcsAmbienteConexao|TCS_AMBIENTE_CONEXAO.ID_TCS_AMBIENTE_CONEXAO");
	    		}
	    	}
	    	return result;
	    }

	    #endregion Excluded Filters

	    #region Get By EntitySearchId Methods Definitions.
	
				
	    [Ignore]
	    //Get TcsConexaoDb By EntitySearchId.
	    public IQueryable<TcsConexaoDb> GetTcsConexaoDbByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsConexaoDbByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsConexaoDb By EntitySearchId.
	    public IQueryable<TcsConexaoDb> GetTcsConexaoDbByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsConexaoDbByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsBancoServidor By EntitySearchId.
	    public IQueryable<TcsBancoServidor> GetTcsBancoServidorByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsBancoServidorByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsAmbienteConexao By EntitySearchId.
	    public IQueryable<TcsAmbienteConexao> GetTcsAmbienteConexaoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsAmbienteConexaoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsBancoServidor By EntitySearchId.
	    public IQueryable<TcsBancoServidor> GetTcsBancoServidorByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsBancoServidorByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsAmbienteConexao By EntitySearchId.
	    public IQueryable<TcsAmbienteConexao> GetTcsAmbienteConexaoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsAmbienteConexaoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get TcsConexaoDb By Example.
	    [Ignore]
	    public IQueryable<TcsConexaoDb> GetTcsConexaoDbByExample(TcsConexaoDb entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsConexaoDbByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsConexaoDb By Example.
	    [Ignore]
	    public IQueryable<TcsConexaoDb> GetTcsConexaoDbByExampleNoAssociations(TcsConexaoDb entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsConexaoDbByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsBancoServidor By Example.
	    [Ignore]
	    public IQueryable<TcsBancoServidor> GetTcsBancoServidorByExample(TcsBancoServidor entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsBancoServidorByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsAmbienteConexao By Example.
	    [Ignore]
	    public IQueryable<TcsAmbienteConexao> GetTcsAmbienteConexaoByExample(TcsAmbienteConexao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsAmbienteConexaoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsBancoServidor By Example.
	    [Ignore]
	    public IQueryable<TcsBancoServidor> GetTcsBancoServidorByExampleNoAssociations(TcsBancoServidor entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsBancoServidorByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsAmbienteConexao By Example.
	    [Ignore]
	    public IQueryable<TcsAmbienteConexao> GetTcsAmbienteConexaoByExampleNoAssociations(TcsAmbienteConexao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsAmbienteConexaoByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public TcsConexaoDb GetTcsConexaoDbByKey(Int32 idConexaoDb)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsConexaoDb");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdConexaoDb"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idConexaoDb));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsConexaoDbByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsBancoServidor GetTcsBancoServidorByKey(Int32 idTcsBancoServidor)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsBancoServidor");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsBancoServidor"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsBancoServidor));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsBancoServidorByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsAmbienteConexao GetTcsAmbienteConexaoByKey(Int32 idTcsAmbienteConexao)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsAmbienteConexao");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsAmbienteConexao"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsAmbienteConexao));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsAmbienteConexaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get TcsConexaoDbByEntitySearch.
	    public IQueryable<TcsConexaoDb> GetTcsConexaoDbByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsConexaoDb));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsConexaoDb> result = 
	            (from entity0 in this.DbContext.TCS_CONEXAO_DB.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsConexaoDb()		
	            {
	            
                IdConexaoDb = entity0.ID_CONEXAO_DB
                , NomeConexao = entity0.NOME_CONEXAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsConexaoDbByEntitySearchNoAssociations.
	    public IQueryable<TcsConexaoDb> GetTcsConexaoDbByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsConexaoDb));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsConexaoDb> result = 
	            (from entity0 in this.DbContext.TCS_CONEXAO_DB.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsConexaoDb()		
	            {
	            
                IdConexaoDb = entity0.ID_CONEXAO_DB
                , NomeConexao = entity0.NOME_CONEXAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsBancoServidorByEntitySearch.
	    public IQueryable<TcsBancoServidor> GetTcsBancoServidorByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsBancoServidor));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsBancoServidor> result = 
	            (from entity0 in this.DbContext.TCS_BANCO_SERVIDOR.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsBancoServidor()		
	            {
	            
                DescricaoBancoServidor = entity0.DESCRICAO_BANCO_SERVIDOR
                , IdTcsBancoServidor = entity0.ID_TCS_BANCO_SERVIDOR
                , Incremento = entity0.INCREMENTO
                , LxTipoServidor = entity0.LX_TIPO_SERVIDOR
                , LxTipoServidorName = ((entity0.LX_TIPO_SERVIDOR) == 2 ? "Oracle" : ((entity0.LX_TIPO_SERVIDOR) == 3 ? "SQLite" : ((entity0.LX_TIPO_SERVIDOR) == 1 ? "SQL Server" : "")))
                , NomeBanco = entity0.NOME_BANCO
                , NomeServidor = entity0.NOME_SERVIDOR
                , SequencialInicial = entity0.SEQUENCIAL_INICIAL
                , StringConexao = entity0.STRING_CONEXAO
			
                ,TcsAmbienteConexaoList = 
	                        (from entity1 in entity0.TCS_AMBIENTE_CONEXAO_LISTA
                                  let entity1Al1 = entity1.TCS_AMBIENTE
                                  let entity1Al6 = entity1.TCS_BANCO_SERVIDOR
                                  let entity1Al5 = entity1.TCS_APLICATIVO_CONEXAO
                                  let entity1Al2 = entity1.TCS_AMBIENTE.TCS_APLICACAO
                                  let entity1Al3 = entity1.TCS_APLICATIVO_CONEXAO.TCS_APLICATIVO
                                  let entity1Al4 = entity1.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                                  let entity1Al7 = entity1.TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB
	                        
	                        	
	                        select new TcsAmbienteConexao()
	                        {
	                        
                                DescricaoAmbiente = entity1Al1.DESCRICAO_AMBIENTE
                                , DescricaoAplicacao = entity1Al2.DESCRICAO_APLICACAO
                                , DescricaoAplicativo = entity1Al3.DESCRICAO_APLICATIVO
                                , IdLinx = entity1Al4.ID_LINX
                                , IdTcsAmbiente = entity1Al1.ID_TCS_AMBIENTE
                                , IdTcsAmbienteConexao = entity1.ID_TCS_AMBIENTE_CONEXAO
                                , IdTcsAplicativoConexao = entity1Al5.ID_TCS_APLICATIVO_CONEXAO
                                , IdTcsBancoServidor = entity1Al6.ID_TCS_BANCO_SERVIDOR
                                , NomeConexao = entity1Al7.NOME_CONEXAO
                                , NomeEmpresa = entity1Al4.NOME_EMPRESA
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAmbienteConexaoByEntitySearch.
	    public IQueryable<TcsAmbienteConexao> GetTcsAmbienteConexaoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAmbienteConexao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAmbienteConexao> result = 
	            (from entity0 in this.DbContext.TCS_AMBIENTE_CONEXAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_AMBIENTE
                  let entity0Al6 = entity0.TCS_BANCO_SERVIDOR
                  let entity0Al5 = entity0.TCS_APLICATIVO_CONEXAO
                  let entity0Al2 = entity0.TCS_AMBIENTE.TCS_APLICACAO
                  let entity0Al3 = entity0.TCS_APLICATIVO_CONEXAO.TCS_APLICATIVO
                  let entity0Al4 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al7 = entity0.TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB
	            
	            	
	            select new TcsAmbienteConexao()		
	            {
	            
                DescricaoAmbiente = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al2.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0Al3.DESCRICAO_APLICATIVO
                , IdLinx = entity0Al4.ID_LINX
                , IdTcsAmbiente = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsAmbienteConexao = entity0.ID_TCS_AMBIENTE_CONEXAO
                , IdTcsAplicativoConexao = entity0Al5.ID_TCS_APLICATIVO_CONEXAO
                , IdTcsBancoServidor = entity0Al6.ID_TCS_BANCO_SERVIDOR
                , NomeConexao = entity0Al7.NOME_CONEXAO
                , NomeEmpresa = entity0Al4.NOME_EMPRESA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsBancoServidorByEntitySearchNoAssociations.
	    public IQueryable<TcsBancoServidor> GetTcsBancoServidorByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsBancoServidor));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsBancoServidor> result = 
	            (from entity0 in this.DbContext.TCS_BANCO_SERVIDOR.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsBancoServidor()		
	            {
	            
                DescricaoBancoServidor = entity0.DESCRICAO_BANCO_SERVIDOR
                , IdTcsBancoServidor = entity0.ID_TCS_BANCO_SERVIDOR
                , Incremento = entity0.INCREMENTO
                , LxTipoServidor = entity0.LX_TIPO_SERVIDOR
                , LxTipoServidorName = ((entity0.LX_TIPO_SERVIDOR) == 2 ? "Oracle" : ((entity0.LX_TIPO_SERVIDOR) == 3 ? "SQLite" : ((entity0.LX_TIPO_SERVIDOR) == 1 ? "SQL Server" : "")))
                , NomeBanco = entity0.NOME_BANCO
                , NomeServidor = entity0.NOME_SERVIDOR
                , SequencialInicial = entity0.SEQUENCIAL_INICIAL
                , StringConexao = entity0.STRING_CONEXAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAmbienteConexaoByEntitySearchNoAssociations.
	    public IQueryable<TcsAmbienteConexao> GetTcsAmbienteConexaoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAmbienteConexao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAmbienteConexao> result = 
	            (from entity0 in this.DbContext.TCS_AMBIENTE_CONEXAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_AMBIENTE
                  let entity0Al6 = entity0.TCS_BANCO_SERVIDOR
                  let entity0Al5 = entity0.TCS_APLICATIVO_CONEXAO
                  let entity0Al2 = entity0.TCS_AMBIENTE.TCS_APLICACAO
                  let entity0Al3 = entity0.TCS_APLICATIVO_CONEXAO.TCS_APLICATIVO
                  let entity0Al4 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al7 = entity0.TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB
	            
	            	
	            select new TcsAmbienteConexao()		
	            {
	            
                DescricaoAmbiente = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al2.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0Al3.DESCRICAO_APLICATIVO
                , IdLinx = entity0Al4.ID_LINX
                , IdTcsAmbiente = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsAmbienteConexao = entity0.ID_TCS_AMBIENTE_CONEXAO
                , IdTcsAplicativoConexao = entity0Al5.ID_TCS_APLICATIVO_CONEXAO
                , IdTcsBancoServidor = entity0Al6.ID_TCS_BANCO_SERVIDOR
                , NomeConexao = entity0Al7.NOME_CONEXAO
                , NomeEmpresa = entity0Al4.NOME_EMPRESA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAmbienteConexaoParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsAmbienteConexaoParentComposition> GetTcsAmbienteConexaoParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "TCS_BANCO_SERVIDOR", "TCS_AMBIENTE_CONEXAO", "TCS_BANCO_SERVIDOR", typeof(TcsAmbienteConexaoParentComposition));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAmbienteConexaoParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_AMBIENTE_CONEXAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_AMBIENTE
                  let entity0Al6 = entity0.TCS_BANCO_SERVIDOR
                  let entity0Al5 = entity0.TCS_APLICATIVO_CONEXAO
                  let entity0Al2 = entity0.TCS_AMBIENTE.TCS_APLICACAO
                  let entity0Al3 = entity0.TCS_APLICATIVO_CONEXAO.TCS_APLICATIVO
                  let entity0Al4 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al7 = entity0.TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB
	            
	            	
	            select new TcsAmbienteConexaoParentComposition()		
	            {
	            
                DescricaoAmbiente = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al2.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0Al3.DESCRICAO_APLICATIVO
                , IdLinx = entity0Al4.ID_LINX
                , IdTcsAmbiente = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsAmbienteConexao = entity0.ID_TCS_AMBIENTE_CONEXAO
                , IdTcsAplicativoConexao = entity0Al5.ID_TCS_APLICATIVO_CONEXAO
                , IdTcsBancoServidor = entity0Al6.ID_TCS_BANCO_SERVIDOR
                , NomeConexao = entity0Al7.NOME_CONEXAO
                , NomeEmpresa = entity0Al4.NOME_EMPRESA
                //TcsBancoServidor Properties.
                , DescricaoBancoServidor = entity0.TCS_BANCO_SERVIDOR.DESCRICAO_BANCO_SERVIDOR
                , Incremento = entity0.TCS_BANCO_SERVIDOR.INCREMENTO
                , LxTipoServidor = entity0.TCS_BANCO_SERVIDOR.LX_TIPO_SERVIDOR
                , LxTipoServidorName = ((entity0.TCS_BANCO_SERVIDOR.LX_TIPO_SERVIDOR) == 2 ? "Oracle" : ((entity0.TCS_BANCO_SERVIDOR.LX_TIPO_SERVIDOR) == 3 ? "SQLite" : ((entity0.TCS_BANCO_SERVIDOR.LX_TIPO_SERVIDOR) == 1 ? "SQL Server" : "")))
                , NomeBanco = entity0.TCS_BANCO_SERVIDOR.NOME_BANCO
                , NomeServidor = entity0.TCS_BANCO_SERVIDOR.NOME_SERVIDOR
                , SequencialInicial = entity0.TCS_BANCO_SERVIDOR.SEQUENCIAL_INICIAL
                , StringConexao = entity0.TCS_BANCO_SERVIDOR.STRING_CONEXAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get PagedTcsConexaoDb.
	    public IQueryable<TcsConexaoDb> GetPagedTcsConexaoDb(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsConexaoDb));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsConexaoDb> result = 
	            (from entity0 in this.DbContext.TCS_CONEXAO_DB.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_CONEXAO_DB ascending
	            
	            	
	            select new TcsConexaoDb()		
	            {
	            
                IdConexaoDb = entity0.ID_CONEXAO_DB
                , NomeConexao = entity0.NOME_CONEXAO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsConexaoDbCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsConexaoDb));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_CONEXAO_DB.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsBancoServidor.
	    public IQueryable<TcsBancoServidor> GetPagedTcsBancoServidor(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsBancoServidor));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsBancoServidor> result = 
	            (from entity0 in this.DbContext.TCS_BANCO_SERVIDOR.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_TCS_BANCO_SERVIDOR ascending
	            
	            	
	            select new TcsBancoServidor()		
	            {
	            
                DescricaoBancoServidor = entity0.DESCRICAO_BANCO_SERVIDOR
                , IdTcsBancoServidor = entity0.ID_TCS_BANCO_SERVIDOR
                , Incremento = entity0.INCREMENTO
                , LxTipoServidor = entity0.LX_TIPO_SERVIDOR
                , LxTipoServidorName = ((entity0.LX_TIPO_SERVIDOR) == 2 ? "Oracle" : ((entity0.LX_TIPO_SERVIDOR) == 3 ? "SQLite" : ((entity0.LX_TIPO_SERVIDOR) == 1 ? "SQL Server" : "")))
                , NomeBanco = entity0.NOME_BANCO
                , NomeServidor = entity0.NOME_SERVIDOR
                , SequencialInicial = entity0.SEQUENCIAL_INICIAL
                , StringConexao = entity0.STRING_CONEXAO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsAmbienteConexao.
	    public IQueryable<TcsAmbienteConexao> GetPagedTcsAmbienteConexao(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAmbienteConexao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAmbienteConexao> result = 
	            (from entity0 in this.DbContext.TCS_AMBIENTE_CONEXAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_AMBIENTE
                  let entity0Al6 = entity0.TCS_BANCO_SERVIDOR
                  let entity0Al5 = entity0.TCS_APLICATIVO_CONEXAO
                  let entity0Al2 = entity0.TCS_AMBIENTE.TCS_APLICACAO
                  let entity0Al3 = entity0.TCS_APLICATIVO_CONEXAO.TCS_APLICATIVO
                  let entity0Al4 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al7 = entity0.TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB
                orderby entity0.ID_TCS_AMBIENTE_CONEXAO ascending
	            
	            	
	            select new TcsAmbienteConexao()		
	            {
	            
                DescricaoAmbiente = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al2.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0Al3.DESCRICAO_APLICATIVO
                , IdLinx = entity0Al4.ID_LINX
                , IdTcsAmbiente = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsAmbienteConexao = entity0.ID_TCS_AMBIENTE_CONEXAO
                , IdTcsAplicativoConexao = entity0Al5.ID_TCS_APLICATIVO_CONEXAO
                , IdTcsBancoServidor = entity0Al6.ID_TCS_BANCO_SERVIDOR
                , NomeConexao = entity0Al7.NOME_CONEXAO
                , NomeEmpresa = entity0Al4.NOME_EMPRESA
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsBancoServidorCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsBancoServidor));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_BANCO_SERVIDOR.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsAmbienteConexaoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAmbienteConexao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_AMBIENTE_CONEXAO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_AMBIENTE
                  let entityAl6 = entity.TCS_BANCO_SERVIDOR
                  let entityAl5 = entity.TCS_APLICATIVO_CONEXAO
                  let entityAl2 = entity.TCS_AMBIENTE.TCS_APLICACAO
                  let entityAl3 = entity.TCS_APLICATIVO_CONEXAO.TCS_APLICATIVO
                  let entityAl4 = entity.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entityAl7 = entity.TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB
	            
	            select 1
	            ).Count();	
		
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    
	    [Update()]	
	    //Update TcsConexaoDb.
	    public void UpdateTcsConexaoDb(TcsConexaoDb entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsConexaoDb.
	    public void InsertTcsConexaoDb(TcsConexaoDb entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsConexaoDb.
	    public void DeleteTcsConexaoDb(TcsConexaoDb entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsBancoServidor.
	    public void UpdateTcsBancoServidor(TcsBancoServidor entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsBancoServidor.
	    public void InsertTcsBancoServidor(TcsBancoServidor entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsBancoServidor.
	    public void DeleteTcsBancoServidor(TcsBancoServidor entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsAmbienteConexao.
	    public void UpdateTcsAmbienteConexao(TcsAmbienteConexao entity)
	    {



	
	        if (entity.TcsBancoServidor.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsBancoServidor) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsBancoServidor); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsAmbienteConexao.
	    public void InsertTcsAmbienteConexao(TcsAmbienteConexao entity)
	    {



	
	        if (entity.TcsBancoServidor.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsBancoServidor) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsBancoServidor);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsAmbienteConexao.
	    public void DeleteTcsAmbienteConexao(TcsAmbienteConexao entity)
	    {



	
	        if (entity.TcsBancoServidor.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsBancoServidor) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsBancoServidor);
	            

	
	        }

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}