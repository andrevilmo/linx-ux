					
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

namespace Linx.Framework.BV.Aplicativo
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_APLICATIVO.ID_TCS_APLICATIVO", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsAplicativo,TcsAplicativo.TcsAplicativoConexao,TcsAplicativo.TcsAplicacao];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsAplicativo];ReadOnly[false];Entities[TCS_APLICATIVO:IdTcsAplicativo];SubQueryInfo[];EdmEntityName[TCS_APLICATIVO];EntityRelations[];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsAplicativo")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Aplicativo.TcsAplicativo")]
	public partial class TcsAplicativo : Linx.Data.Entity
	{

	
		
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.TcsAplicativoConexaoList != null && this.TcsAplicativoConexaoList.Count() > 0)
	      {
	         foreach (var entity in this.TcsAplicativoConexaoList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      if (this.TcsAplicacaoList != null && this.TcsAplicacaoList.Count() > 0)
	      {
	         foreach (var entity in this.TcsAplicacaoList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.TcsAplicativoConexaoList != null)
	      {
	         foreach (var detail in this.TcsAplicativoConexaoList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsAplicativoConexaoList = null;
	      }
	      if (this.TcsAplicacaoList != null)
	      {
	         foreach (var detail in this.TcsAplicacaoList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsAplicacaoList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(AplicativoDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("TcsAplicativoConexao"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsAplicativoConexao");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsAplicativo"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdTcsAplicativo));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsAplicativoConexao and all sub-details
	         if (this.TcsAplicativoConexaoList == null || this.TcsAplicativoConexaoList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsAplicativoConexaoList = context.GetPagedTcsAplicativoConexao(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsAplicativoConexaoList = (from r in context.GetTcsAplicativoConexaoByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	      if (viewNames == null || viewNames.Contains("TcsAplicacao"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsAplicacao");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsAplicativo"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdTcsAplicativo));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsAplicacao and all sub-details
	         if (this.TcsAplicacaoList == null || this.TcsAplicacaoList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsAplicacaoList = context.GetPagedTcsAplicacao(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsAplicacaoList = (from r in context.GetTcsAplicacaoByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _TcsAplicativoConexaoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsAplicativoConexao && ((TcsAplicativoConexao)e.Entity).TcsAplicativo == null && e.Associations == null && e.OriginalAssociations == null && ((TcsAplicativoConexao)e.Entity).IdTcsAplicativo == this.IdTcsAplicativo).ToList();
 	      if (_TcsAplicativoConexaoElements.Count > 0 && this.TcsAplicativoConexaoList.Count() == 0)
 	      {
 	          this.TcsAplicativoConexaoList = _TcsAplicativoConexaoElements.Select(e => (TcsAplicativoConexao)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsAplicativoConexaoElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsAplicativoConexao)detail.Entity).TcsAplicativo = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsAplicativo", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsAplicativoConexaoList", indexDetails.ToArray());
 	      }
 
 	      var _TcsAplicacaoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsAplicacao && ((TcsAplicacao)e.Entity).TcsAplicativo == null && e.Associations == null && e.OriginalAssociations == null && ((TcsAplicacao)e.Entity).IdTcsAplicativo == this.IdTcsAplicativo).ToList();
 	      if (_TcsAplicacaoElements.Count > 0 && this.TcsAplicacaoList.Count() == 0)
 	      {
 	          this.TcsAplicacaoList = _TcsAplicacaoElements.Select(e => (TcsAplicacao)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsAplicacaoElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsAplicacao)detail.Entity).TcsAplicativo = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsAplicativo", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsAplicacaoList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For DescricaoAplicativo
	    partial void OnDescricaoAplicativoChanging(System.String value);
	    partial void OnDescricaoAplicativoChanged();

	    private System.String _DescricaoAplicativo;

	    [DataMember(Name = "DescricaoAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_APLICATIVO.DESCRICAO_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICATIVO.DESCRICAO_APLICATIVO")]
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
	    //Extensibility Partial Method Definitions For IdTcsAplicativo
	    partial void OnIdTcsAplicativoChanging(Int32 value);
	    partial void OnIdTcsAplicativoChanged();

	    private Int32 _IdTcsAplicativo;

	    [DataMember(IsRequired = true, Name = "IdTcsAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Aplicativo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_APLICATIVO.ID_TCS_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICATIVO.ID_TCS_APLICATIVO")]
	    public Int32 IdTcsAplicativo
	    {
	    	    get
	    	    {
	    	          return _IdTcsAplicativo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAplicativo != value)
	    	          {
	    	              this.ValidateProperty("IdTcsAplicativo", value);
	    	              this.OnIdTcsAplicativoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsAplicativo");
	    	              this._IdTcsAplicativo = value;
	    	              this.RaiseDataMemberChanged("IdTcsAplicativo");
	    	              this.OnIdTcsAplicativoChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIdTcsAplicativo;
	    [DataMember(Name = "TemporaryIdTcsAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Aplicativo (Tmp)", Description="Temporary Key", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdTcsAplicativo
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTcsAplicativo.IsNullOrEmpty())
	    	                this._TemporaryIdTcsAplicativo = this._IdTcsAplicativo;
	    	          return this._TemporaryIdTcsAplicativo;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTcsAplicativo != value)
	    	              this._TemporaryIdTcsAplicativo = value;
	    	    }
	    }	

	    #endregion Data Properties

	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<TcsAplicacao> _TcsAplicacaoList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsAplicativo_TcsAplicacao", "IdTcsAplicativo", "IdTcsAplicativo", IsForeignKey=false)]
	    [DataMember(Name = "TcsAplicacaoList", EmitDefaultValue = true)]
	    public IEnumerable<TcsAplicacao> TcsAplicacaoList
	    {
	        get
	        {
	
	            if (this._TcsAplicacaoList == null)
	            	this._TcsAplicacaoList = new List<TcsAplicacao>();
	
	            return this._TcsAplicacaoList;
	        }
	        set
	        {
	            if (this._TcsAplicacaoList != value)
	            {
	                this._TcsAplicacaoList = value;
	                this.RaisePropertyChanged("TcsAplicacaoList");
	            }
	        }
	    }	 
		
	    private IEnumerable<TcsAplicativoConexao> _TcsAplicativoConexaoList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsAplicativo_TcsAplicativoConexao", "IdTcsAplicativo", "IdTcsAplicativo", IsForeignKey=false)]
	    [DataMember(Name = "TcsAplicativoConexaoList", EmitDefaultValue = true)]
	    public IEnumerable<TcsAplicativoConexao> TcsAplicativoConexaoList
	    {
	        get
	        {
	
	            if (this._TcsAplicativoConexaoList == null)
	            	this._TcsAplicativoConexaoList = new List<TcsAplicativoConexao>();
	
	            return this._TcsAplicativoConexaoList;
	        }
	        set
	        {
	            if (this._TcsAplicativoConexaoList != value)
	            {
	                this._TcsAplicativoConexaoList = value;
	                this.RaisePropertyChanged("TcsAplicativoConexaoList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_APLICATIVO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_APLICATIVO), QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICATIVO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_APLICATIVO.ID_TCS_APLICATIVO", Source = "IdTcsAplicativo", Target = "ID_TCS_APLICATIVO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICATIVO", RelationPropertyName = "TCS_APLICATIVO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_APLICATIVO.DESCRICAO_APLICATIVO", Source = "DescricaoAplicativo", Target = "DESCRICAO_APLICATIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICATIVO", RelationPropertyName = "TCS_APLICATIVO" });

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

		

	[LinxPublicationView(PrimaryKeys="TCS_APLICATIVO_CONEXAO.ID_TCS_APLICATIVO_CONEXAO", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Providers - BM];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsAplicativoConexao];ReadOnly[false];Entities[TCS_APLICATIVO_CONEXAO:IdTcsAplicativoConexao|TCS_CONEXAO_DB:IdConexaoDb];SubQueryInfo[Select 1 From #ParentAlias#.TCS_APLICATIVO_CONEXAO_LISTA as #Alias#];EdmEntityName[TCS_APLICATIVO_CONEXAO];EntityRelations[TCS_CONEXAO_DB(TCS_CONEXAO_DB)#TCS_APLICATIVO(TCS_APLICATIVO)];EdmParentEntityName[TCS_APLICATIVO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsAplicativoConexao")]
	[Serializable()]
	public partial class TcsAplicativoConexao : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(AplicativoDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsAplicativo");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsAplicativo"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdTcsAplicativo));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsAplicativo
	         this.TcsAplicativo = (from r in context.GetTcsAplicativoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For IdConexaoDb
	    partial void OnIdConexaoDbChanging(Int32 value);
	    partial void OnIdConexaoDbChanged();

	    private Int32 _IdConexaoDb;

	    [DataMember(IsRequired = true, Name = "IdConexaoDb", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Conexao Db", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsConexaoDb];LookUpTitle[Seleção de (Id Conexao Db)];LookUpQuery[executeLookUpTcsConexaoDb];LookUpFinalize[finalizeLookUpTcsConexaoDb];LookUpDisplayColumns[{\"IdConexaoDb\" : \"Id Conexao Db\", \"NomeConexao\" : \"Nome Provider BM\"}];LookUpColumns[{\"IdConexaoDb\" : false, \"NomeConexao\" : true}];FilterDataKey[TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB.ID_CONEXAO_DB];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdConexaoDb#true##12:0##Id Conexao Db#0#false##::LookUpTcsConexaoDb##true#false#TCS_CONEXAO_DB#TCS_CONEXAO_DB#Linx.Framework.BV.Aplicativo#IQueryable###true#true", EdmKey="TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB.ID_CONEXAO_DB")]
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
	    //Extensibility Partial Method Definitions For IdTcsAplicativo
	    partial void OnIdTcsAplicativoChanging(Int32 value);
	    partial void OnIdTcsAplicativoChanged();

	    private Int32 _IdTcsAplicativo;

	    [DataMember(IsRequired = true, Name = "IdTcsAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Aplicativo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_APLICATIVO_CONEXAO.TCS_APLICATIVO.ID_TCS_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICATIVO_CONEXAO.TCS_APLICATIVO.ID_TCS_APLICATIVO")]
	    public Int32 IdTcsAplicativo
	    {
	    	    get
	    	    {
	    	          return _IdTcsAplicativo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAplicativo != value)
	    	          {
	    	              this.ValidateProperty("IdTcsAplicativo", value);
	    	              this.OnIdTcsAplicativoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsAplicativo");
	    	              this._IdTcsAplicativo = value;
	    	              this.RaiseDataMemberChanged("IdTcsAplicativo");
	    	              this.OnIdTcsAplicativoChanged();
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
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_APLICATIVO_CONEXAO.ID_TCS_APLICATIVO_CONEXAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICATIVO_CONEXAO.ID_TCS_APLICATIVO_CONEXAO")]
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
	    //Extensibility Partial Method Definitions For NomeConexao
	    partial void OnNomeConexaoChanging(System.String value);
	    partial void OnNomeConexaoChanged();

	    private System.String _NomeConexao;

	    [DataMember(IsRequired = true, Name = "NomeConexao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Provider BM", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsConexaoDb];LookUpTitle[Seleção de (Nome Provider BM)];LookUpQuery[executeLookUpTcsConexaoDb];LookUpFinalize[finalizeLookUpTcsConexaoDb];LookUpDisplayColumns[{\"IdConexaoDb\" : \"Id Conexao Db\", \"NomeConexao\" : \"Nome Provider BM\"}];LookUpColumns[{\"IdConexaoDb\" : false, \"NomeConexao\" : true}];FilterDataKey[TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB.NOME_CONEXAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeConexao#false##250:0##Nome Provider BM#1#true##::LookUpTcsConexaoDb##true#false#TCS_CONEXAO_DB#TCS_CONEXAO_DB#Linx.Framework.BV.Aplicativo#IQueryable###true#true", EdmKey="TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB.NOME_CONEXAO")]
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

	    private Int32 _TemporaryIdTcsAplicativoConexao;
	    [DataMember(Name = "TemporaryIdTcsAplicativoConexao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Aplicativo Conexao (Tmp)", Description="Temporary Key", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdTcsAplicativoConexao
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTcsAplicativoConexao.IsNullOrEmpty())
	    	                this._TemporaryIdTcsAplicativoConexao = this._IdTcsAplicativoConexao;
	    	          return this._TemporaryIdTcsAplicativoConexao;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTcsAplicativoConexao != value)
	    	              this._TemporaryIdTcsAplicativoConexao = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsAplicativo _TcsAplicativo;
	    [DataMember(Name = "TcsAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsAplicativo_TcsAplicativoConexao", "IdTcsAplicativo", "IdTcsAplicativo", IsForeignKey=true)]
	    public TcsAplicativo TcsAplicativo
	    {
	        get
	        {
	            return this._TcsAplicativo;
	        }
	        set
	        {
	            if (this._TcsAplicativo != value)
	            {
	                this._TcsAplicativo = value;
	                this.RaisePropertyChanged("TcsAplicativoList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_APLICATIVO_CONEXAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_APLICATIVO_CONEXAO), QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICATIVO_CONEXAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_APLICATIVO_CONEXAO.ID_TCS_APLICATIVO_CONEXAO", Source = "IdTcsAplicativoConexao", Target = "ID_TCS_APLICATIVO_CONEXAO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICATIVO_CONEXAO", RelationPropertyName = "TCS_APLICATIVO_CONEXAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB.ID_CONEXAO_DB", Source = "IdConexaoDb", Target = "ID_CONEXAO_DB", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_CONEXAO_DB", RelationPropertyName = "TCS_CONEXAO_DB" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_APLICATIVO_CONEXAO.TCS_APLICATIVO.ID_TCS_APLICATIVO", Source = "IdTcsAplicativo", Target = "ID_TCS_APLICATIVO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICATIVO", RelationPropertyName = "TCS_APLICATIVO" });

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

		

	[LinxPublicationView(PrimaryKeys="TCS_APLICACAO.ID_APLICACAO", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Aplicações Relacionadas];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdAplicacao];ReadOnly[false];Entities[TCS_APLICACAO:IdAplicacao];SubQueryInfo[Select 1 From #ParentAlias#.TCS_APLICACAO_LISTA as #Alias#];EdmEntityName[TCS_APLICACAO];EntityRelations[TCS_APLICATIVO(TCS_APLICATIVO)];EdmParentEntityName[TCS_APLICATIVO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsAplicacao")]
	[Serializable()]
	public partial class TcsAplicacao : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(AplicativoDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsAplicativo");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsAplicativo"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdTcsAplicativo));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsAplicativo
	         this.TcsAplicativo = (from r in context.GetTcsAplicativoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For DescricaoAplicacao
	    partial void OnDescricaoAplicacaoChanging(System.String value);
	    partial void OnDescricaoAplicacaoChanged();

	    private System.String _DescricaoAplicacao;

	    [DataMember(IsRequired = true, Name = "DescricaoAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicação", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicacao];LookUpTitle[Seleção de (Aplicação)];LookUpQuery[executeLookUpTcsAplicacao];LookUpFinalize[finalizeLookUpTcsAplicacao];LookUpDisplayColumns[{\"DescricaoAplicacao\" : \"Aplicação\", \"EmDesenvolvimento\" : \"Em Desenvolvimento\", \"IdAplicacao\" : \"Id Aplicacao\"}];LookUpColumns[{\"DescricaoAplicacao\" : true, \"EmDesenvolvimento\" : true, \"IdAplicacao\" : false}];FilterDataKey[TCS_APLICACAO.DESCRICAO_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoAplicacao#false##600##Aplicação#0#true##::LookUpTcsAplicacao##false#false##TCS_APLICACAO#Linx.Framework.BV.Aplicativo#IQueryable###true#false", EdmKey="TCS_APLICACAO.DESCRICAO_APLICACAO")]
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
	    //Extensibility Partial Method Definitions For EmDesenvolvimento
	    partial void OnEmDesenvolvimentoChanging(Boolean value);
	    partial void OnEmDesenvolvimentoChanged();

	    private Boolean _EmDesenvolvimento;

	    [DataMember(IsRequired = true, Name = "EmDesenvolvimento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Em Desenvolvimento", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicacao];LookUpTitle[Seleção de (Em Desenvolvimento)];LookUpQuery[executeLookUpTcsAplicacao];LookUpFinalize[finalizeLookUpTcsAplicacao];LookUpDisplayColumns[{\"DescricaoAplicacao\" : \"Aplicação\", \"EmDesenvolvimento\" : \"Em Desenvolvimento\", \"IdAplicacao\" : \"Id Aplicacao\"}];LookUpColumns[{\"DescricaoAplicacao\" : true, \"EmDesenvolvimento\" : true, \"IdAplicacao\" : false}];FilterDataKey[TCS_APLICACAO.EM_DESENVOLVIMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Boolean#EmDesenvolvimento#false##0##Em Desenvolvimento#1#true##::LookUpTcsAplicacao##false#false##TCS_APLICACAO#Linx.Framework.BV.Aplicativo#IQueryable###true#false", EdmKey="TCS_APLICACAO.EM_DESENVOLVIMENTO")]
	    public Boolean EmDesenvolvimento
	    {
	    	    get
	    	    {
	    	          return _EmDesenvolvimento;
	    	    }
	    	    set
	    	    {
	    	          if (this._EmDesenvolvimento != value)
	    	          {
	    	              this.ValidateProperty("EmDesenvolvimento", value);
	    	              this.OnEmDesenvolvimentoChanging(value);
	    	              this.RaiseDataMemberChanging("EmDesenvolvimento");
	    	              this._EmDesenvolvimento = value;
	    	              this.RaiseDataMemberChanged("EmDesenvolvimento");
	    	              this.OnEmDesenvolvimentoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdAplicacao
	    partial void OnIdAplicacaoChanging(Int32 value);
	    partial void OnIdAplicacaoChanged();

	    private Int32 _IdAplicacao;

	    [DataMember(IsRequired = true, Name = "IdAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Aplicacao", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicacao];LookUpTitle[Seleção de (Id Aplicacao)];LookUpQuery[executeLookUpTcsAplicacao];LookUpFinalize[finalizeLookUpTcsAplicacao];LookUpDisplayColumns[{\"DescricaoAplicacao\" : \"Aplicação\", \"EmDesenvolvimento\" : \"Em Desenvolvimento\", \"IdAplicacao\" : \"Id Aplicacao\"}];LookUpColumns[{\"DescricaoAplicacao\" : true, \"EmDesenvolvimento\" : true, \"IdAplicacao\" : false}];FilterDataKey[TCS_APLICACAO.ID_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdAplicacao#true##12:0##Id Aplicacao#2#false##::LookUpTcsAplicacao##false#false##TCS_APLICACAO#Linx.Framework.BV.Aplicativo#IQueryable###true#false", EdmKey="TCS_APLICACAO.ID_APLICACAO")]
	    public Int32 IdAplicacao
	    {
	    	    get
	    	    {
	    	          return _IdAplicacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdAplicacao != value)
	    	          {
	    	              this.ValidateProperty("IdAplicacao", value);
	    	              this.OnIdAplicacaoChanging(value);
	    	              this.RaiseDataMemberChanging("IdAplicacao");
	    	              this._IdAplicacao = value;
	    	              this.RaiseDataMemberChanged("IdAplicacao");
	    	              this.OnIdAplicacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsAplicativo
	    partial void OnIdTcsAplicativoChanging(Int32 value);
	    partial void OnIdTcsAplicativoChanged();

	    private Int32 _IdTcsAplicativo;

	    [DataMember(IsRequired = true, Name = "IdTcsAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Aplicativo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO")]
	    public Int32 IdTcsAplicativo
	    {
	    	    get
	    	    {
	    	          return _IdTcsAplicativo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAplicativo != value)
	    	          {
	    	              this.ValidateProperty("IdTcsAplicativo", value);
	    	              this.OnIdTcsAplicativoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsAplicativo");
	    	              this._IdTcsAplicativo = value;
	    	              this.RaiseDataMemberChanged("IdTcsAplicativo");
	    	              this.OnIdTcsAplicativoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Url
	    partial void OnUrlChanging(System.String value);
	    partial void OnUrlChanged();

	    private System.String _Url;

	    [DataMember(Name = "Url", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Url", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_APLICACAO.URL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICACAO.URL")]
	    public System.String Url
	    {
	    	    get
	    	    {
	    	          return _Url;
	    	    }
	    	    set
	    	    {
	    	          if (this._Url != value)
	    	          {
	    	              this.ValidateProperty("Url", value);
	    	              this.OnUrlChanging(value);
	    	              this.RaiseDataMemberChanging("Url");
	    	              this._Url = value;
	    	              this.RaiseDataMemberChanged("Url");
	    	              this.OnUrlChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UrlWorkArea
	    partial void OnUrlWorkAreaChanging(System.String value);
	    partial void OnUrlWorkAreaChanged();

	    private System.String _UrlWorkArea;

	    [DataMember(Name = "UrlWorkArea", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Url Work Area", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_APLICACAO.URL_WORK_AREA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICACAO.URL_WORK_AREA")]
	    public System.String UrlWorkArea
	    {
	    	    get
	    	    {
	    	          return _UrlWorkArea;
	    	    }
	    	    set
	    	    {
	    	          if (this._UrlWorkArea != value)
	    	          {
	    	              this.ValidateProperty("UrlWorkArea", value);
	    	              this.OnUrlWorkAreaChanging(value);
	    	              this.RaiseDataMemberChanging("UrlWorkArea");
	    	              this._UrlWorkArea = value;
	    	              this.RaiseDataMemberChanged("UrlWorkArea");
	    	              this.OnUrlWorkAreaChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIdAplicacao;
	    [DataMember(Name = "TemporaryIdAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Aplicacao (Tmp)", Description="Temporary Key", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdAplicacao
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdAplicacao.IsNullOrEmpty())
	    	                this._TemporaryIdAplicacao = this._IdAplicacao;
	    	          return this._TemporaryIdAplicacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdAplicacao != value)
	    	              this._TemporaryIdAplicacao = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsAplicativo _TcsAplicativo;
	    [DataMember(Name = "TcsAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsAplicativo_TcsAplicacao", "IdTcsAplicativo", "IdTcsAplicativo", IsForeignKey=true)]
	    public TcsAplicativo TcsAplicativo
	    {
	        get
	        {
	            return this._TcsAplicativo;
	        }
	        set
	        {
	            if (this._TcsAplicativo != value)
	            {
	                this._TcsAplicativo = value;
	                this.RaisePropertyChanged("TcsAplicativoList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_APLICACAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_APLICACAO), QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICACAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_APLICACAO.URL", Source = "Url", Target = "URL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICACAO", RelationPropertyName = "TCS_APLICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_APLICACAO.ID_APLICACAO", Source = "IdAplicacao", Target = "ID_APLICACAO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICACAO", RelationPropertyName = "TCS_APLICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_APLICACAO.URL_WORK_AREA", Source = "UrlWorkArea", Target = "URL_WORK_AREA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICACAO", RelationPropertyName = "TCS_APLICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_APLICACAO.EM_DESENVOLVIMENTO", Source = "EmDesenvolvimento", Target = "EM_DESENVOLVIMENTO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICACAO", RelationPropertyName = "TCS_APLICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_APLICACAO.DESCRICAO_APLICACAO", Source = "DescricaoAplicacao", Target = "DESCRICAO_APLICACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICACAO", RelationPropertyName = "TCS_APLICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO", Source = "IdTcsAplicativo", Target = "ID_TCS_APLICATIVO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICATIVO", RelationPropertyName = "TCS_APLICATIVO" });

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

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Providers - BM];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsAplicativoConexao];ReadOnly[false];Entities[TCS_APLICATIVO_CONEXAO:IdTcsAplicativoConexao|TCS_CONEXAO_DB:IdConexaoDb];SubQueryInfo[Select 1 From #ParentAlias#.TCS_APLICATIVO_CONEXAO_LISTA as #Alias#];EdmEntityName[TCS_APLICATIVO_CONEXAO];EntityRelations[TCS_CONEXAO_DB(TCS_CONEXAO_DB)#TCS_APLICATIVO(TCS_APLICATIVO)];EdmParentEntityName[TCS_APLICATIVO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsAplicativoConexao")]
	[Serializable()]
	public partial class TcsAplicativoConexaoParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For IdConexaoDb
	    partial void OnIdConexaoDbChanging(Int32 value);
	    partial void OnIdConexaoDbChanged();

	    private Int32 _IdConexaoDb;

	    [DataMember(IsRequired = true, Name = "IdConexaoDb", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Conexao Db", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsConexaoDb];LookUpTitle[Seleção de (Id Conexao Db)];LookUpQuery[executeLookUpTcsConexaoDb];LookUpFinalize[finalizeLookUpTcsConexaoDb];LookUpDisplayColumns[{\"IdConexaoDb\" : \"Id Conexao Db\", \"NomeConexao\" : \"Nome Provider BM\"}];LookUpColumns[{\"IdConexaoDb\" : false, \"NomeConexao\" : true}];FilterDataKey[TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB.ID_CONEXAO_DB];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdConexaoDb#true##12:0##Id Conexao Db#0#false##::LookUpTcsConexaoDb##true#false#TCS_CONEXAO_DB#TCS_CONEXAO_DB#Linx.Framework.BV.Aplicativo#IQueryable###true#true", EdmKey="TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB.ID_CONEXAO_DB")]
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
	    //Extensibility Partial Method Definitions For IdTcsAplicativo
	    partial void OnIdTcsAplicativoChanging(Int32 value);
	    partial void OnIdTcsAplicativoChanged();

	    private Int32 _IdTcsAplicativo;

	    [DataMember(IsRequired = true, Name = "IdTcsAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Aplicativo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_APLICATIVO_CONEXAO.TCS_APLICATIVO.ID_TCS_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICATIVO_CONEXAO.TCS_APLICATIVO.ID_TCS_APLICATIVO")]
	    public Int32 IdTcsAplicativo
	    {
	    	    get
	    	    {
	    	          return _IdTcsAplicativo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAplicativo != value)
	    	          {
	    	              this.ValidateProperty("IdTcsAplicativo", value);
	    	              this.OnIdTcsAplicativoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsAplicativo");
	    	              this._IdTcsAplicativo = value;
	    	              this.RaiseDataMemberChanged("IdTcsAplicativo");
	    	              this.OnIdTcsAplicativoChanged();
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
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_APLICATIVO_CONEXAO.ID_TCS_APLICATIVO_CONEXAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICATIVO_CONEXAO.ID_TCS_APLICATIVO_CONEXAO")]
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
	    //Extensibility Partial Method Definitions For NomeConexao
	    partial void OnNomeConexaoChanging(System.String value);
	    partial void OnNomeConexaoChanged();

	    private System.String _NomeConexao;

	    [DataMember(IsRequired = true, Name = "NomeConexao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Provider BM", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsConexaoDb];LookUpTitle[Seleção de (Nome Provider BM)];LookUpQuery[executeLookUpTcsConexaoDb];LookUpFinalize[finalizeLookUpTcsConexaoDb];LookUpDisplayColumns[{\"IdConexaoDb\" : \"Id Conexao Db\", \"NomeConexao\" : \"Nome Provider BM\"}];LookUpColumns[{\"IdConexaoDb\" : false, \"NomeConexao\" : true}];FilterDataKey[TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB.NOME_CONEXAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeConexao#false##250:0##Nome Provider BM#1#true##::LookUpTcsConexaoDb##true#false#TCS_CONEXAO_DB#TCS_CONEXAO_DB#Linx.Framework.BV.Aplicativo#IQueryable###true#true", EdmKey="TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB.NOME_CONEXAO")]
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
	    //Extensibility Partial Method Definitions For DescricaoAplicativo
	    partial void OnDescricaoAplicativoChanging(System.String value);
	    partial void OnDescricaoAplicativoChanged();

	    private System.String _DescricaoAplicativo;

	    [DataMember(Name = "DescricaoAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_APLICATIVO_CONEXAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICATIVO.DESCRICAO_APLICATIVO")]
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

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_APLICATIVO_CONEXAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_APLICATIVO_CONEXAO), QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICATIVO_CONEXAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_APLICATIVO_CONEXAO.ID_TCS_APLICATIVO_CONEXAO", Source = "IdTcsAplicativoConexao", Target = "ID_TCS_APLICATIVO_CONEXAO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICATIVO_CONEXAO", RelationPropertyName = "TCS_APLICATIVO_CONEXAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB.ID_CONEXAO_DB", Source = "IdConexaoDb", Target = "ID_CONEXAO_DB", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_CONEXAO_DB", RelationPropertyName = "TCS_CONEXAO_DB" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_APLICATIVO_CONEXAO.TCS_APLICATIVO.ID_TCS_APLICATIVO", Source = "IdTcsAplicativo", Target = "ID_TCS_APLICATIVO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICATIVO", RelationPropertyName = "TCS_APLICATIVO" });

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

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Aplicações Relacionadas];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdAplicacao];ReadOnly[false];Entities[TCS_APLICACAO:IdAplicacao];SubQueryInfo[Select 1 From #ParentAlias#.TCS_APLICACAO_LISTA as #Alias#];EdmEntityName[TCS_APLICACAO];EntityRelations[TCS_APLICATIVO(TCS_APLICATIVO)];EdmParentEntityName[TCS_APLICATIVO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsAplicacao")]
	[Serializable()]
	public partial class TcsAplicacaoParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For DescricaoAplicacao
	    partial void OnDescricaoAplicacaoChanging(System.String value);
	    partial void OnDescricaoAplicacaoChanged();

	    private System.String _DescricaoAplicacao;

	    [DataMember(IsRequired = true, Name = "DescricaoAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicação", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicacao];LookUpTitle[Seleção de (Aplicação)];LookUpQuery[executeLookUpTcsAplicacao];LookUpFinalize[finalizeLookUpTcsAplicacao];LookUpDisplayColumns[{\"DescricaoAplicacao\" : \"Aplicação\", \"EmDesenvolvimento\" : \"Em Desenvolvimento\", \"IdAplicacao\" : \"Id Aplicacao\"}];LookUpColumns[{\"DescricaoAplicacao\" : true, \"EmDesenvolvimento\" : true, \"IdAplicacao\" : false}];FilterDataKey[TCS_APLICACAO.DESCRICAO_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoAplicacao#false##600##Aplicação#0#true##::LookUpTcsAplicacao##false#false##TCS_APLICACAO#Linx.Framework.BV.Aplicativo#IQueryable###true#false", EdmKey="TCS_APLICACAO.DESCRICAO_APLICACAO")]
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
	    //Extensibility Partial Method Definitions For EmDesenvolvimento
	    partial void OnEmDesenvolvimentoChanging(Boolean value);
	    partial void OnEmDesenvolvimentoChanged();

	    private Boolean _EmDesenvolvimento;

	    [DataMember(IsRequired = true, Name = "EmDesenvolvimento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Em Desenvolvimento", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicacao];LookUpTitle[Seleção de (Em Desenvolvimento)];LookUpQuery[executeLookUpTcsAplicacao];LookUpFinalize[finalizeLookUpTcsAplicacao];LookUpDisplayColumns[{\"DescricaoAplicacao\" : \"Aplicação\", \"EmDesenvolvimento\" : \"Em Desenvolvimento\", \"IdAplicacao\" : \"Id Aplicacao\"}];LookUpColumns[{\"DescricaoAplicacao\" : true, \"EmDesenvolvimento\" : true, \"IdAplicacao\" : false}];FilterDataKey[TCS_APLICACAO.EM_DESENVOLVIMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Boolean#EmDesenvolvimento#false##0##Em Desenvolvimento#1#true##::LookUpTcsAplicacao##false#false##TCS_APLICACAO#Linx.Framework.BV.Aplicativo#IQueryable###true#false", EdmKey="TCS_APLICACAO.EM_DESENVOLVIMENTO")]
	    public Boolean EmDesenvolvimento
	    {
	    	    get
	    	    {
	    	          return _EmDesenvolvimento;
	    	    }
	    	    set
	    	    {
	    	          if (this._EmDesenvolvimento != value)
	    	          {
	    	              this.ValidateProperty("EmDesenvolvimento", value);
	    	              this.OnEmDesenvolvimentoChanging(value);
	    	              this.RaiseDataMemberChanging("EmDesenvolvimento");
	    	              this._EmDesenvolvimento = value;
	    	              this.RaiseDataMemberChanged("EmDesenvolvimento");
	    	              this.OnEmDesenvolvimentoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdAplicacao
	    partial void OnIdAplicacaoChanging(Int32 value);
	    partial void OnIdAplicacaoChanged();

	    private Int32 _IdAplicacao;

	    [DataMember(IsRequired = true, Name = "IdAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Aplicacao", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicacao];LookUpTitle[Seleção de (Id Aplicacao)];LookUpQuery[executeLookUpTcsAplicacao];LookUpFinalize[finalizeLookUpTcsAplicacao];LookUpDisplayColumns[{\"DescricaoAplicacao\" : \"Aplicação\", \"EmDesenvolvimento\" : \"Em Desenvolvimento\", \"IdAplicacao\" : \"Id Aplicacao\"}];LookUpColumns[{\"DescricaoAplicacao\" : true, \"EmDesenvolvimento\" : true, \"IdAplicacao\" : false}];FilterDataKey[TCS_APLICACAO.ID_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdAplicacao#true##12:0##Id Aplicacao#2#false##::LookUpTcsAplicacao##false#false##TCS_APLICACAO#Linx.Framework.BV.Aplicativo#IQueryable###true#false", EdmKey="TCS_APLICACAO.ID_APLICACAO")]
	    public Int32 IdAplicacao
	    {
	    	    get
	    	    {
	    	          return _IdAplicacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdAplicacao != value)
	    	          {
	    	              this.ValidateProperty("IdAplicacao", value);
	    	              this.OnIdAplicacaoChanging(value);
	    	              this.RaiseDataMemberChanging("IdAplicacao");
	    	              this._IdAplicacao = value;
	    	              this.RaiseDataMemberChanged("IdAplicacao");
	    	              this.OnIdAplicacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsAplicativo
	    partial void OnIdTcsAplicativoChanging(Int32 value);
	    partial void OnIdTcsAplicativoChanged();

	    private Int32 _IdTcsAplicativo;

	    [DataMember(IsRequired = true, Name = "IdTcsAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Aplicativo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO")]
	    public Int32 IdTcsAplicativo
	    {
	    	    get
	    	    {
	    	          return _IdTcsAplicativo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAplicativo != value)
	    	          {
	    	              this.ValidateProperty("IdTcsAplicativo", value);
	    	              this.OnIdTcsAplicativoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsAplicativo");
	    	              this._IdTcsAplicativo = value;
	    	              this.RaiseDataMemberChanged("IdTcsAplicativo");
	    	              this.OnIdTcsAplicativoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Url
	    partial void OnUrlChanging(System.String value);
	    partial void OnUrlChanged();

	    private System.String _Url;

	    [DataMember(Name = "Url", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Url", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_APLICACAO.URL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICACAO.URL")]
	    public System.String Url
	    {
	    	    get
	    	    {
	    	          return _Url;
	    	    }
	    	    set
	    	    {
	    	          if (this._Url != value)
	    	          {
	    	              this.ValidateProperty("Url", value);
	    	              this.OnUrlChanging(value);
	    	              this.RaiseDataMemberChanging("Url");
	    	              this._Url = value;
	    	              this.RaiseDataMemberChanged("Url");
	    	              this.OnUrlChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UrlWorkArea
	    partial void OnUrlWorkAreaChanging(System.String value);
	    partial void OnUrlWorkAreaChanged();

	    private System.String _UrlWorkArea;

	    [DataMember(Name = "UrlWorkArea", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Url Work Area", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_APLICACAO.URL_WORK_AREA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICACAO.URL_WORK_AREA")]
	    public System.String UrlWorkArea
	    {
	    	    get
	    	    {
	    	          return _UrlWorkArea;
	    	    }
	    	    set
	    	    {
	    	          if (this._UrlWorkArea != value)
	    	          {
	    	              this.ValidateProperty("UrlWorkArea", value);
	    	              this.OnUrlWorkAreaChanging(value);
	    	              this.RaiseDataMemberChanging("UrlWorkArea");
	    	              this._UrlWorkArea = value;
	    	              this.RaiseDataMemberChanged("UrlWorkArea");
	    	              this.OnUrlWorkAreaChanged();
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
	    [Display(Name = "Descrição", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICATIVO.DESCRICAO_APLICATIVO")]
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

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_APLICACAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_APLICACAO), QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICACAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_APLICACAO.URL", Source = "Url", Target = "URL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICACAO", RelationPropertyName = "TCS_APLICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_APLICACAO.ID_APLICACAO", Source = "IdAplicacao", Target = "ID_APLICACAO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICACAO", RelationPropertyName = "TCS_APLICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_APLICACAO.URL_WORK_AREA", Source = "UrlWorkArea", Target = "URL_WORK_AREA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICACAO", RelationPropertyName = "TCS_APLICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_APLICACAO.EM_DESENVOLVIMENTO", Source = "EmDesenvolvimento", Target = "EM_DESENVOLVIMENTO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICACAO", RelationPropertyName = "TCS_APLICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_APLICACAO.DESCRICAO_APLICACAO", Source = "DescricaoAplicacao", Target = "DESCRICAO_APLICACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICACAO", RelationPropertyName = "TCS_APLICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO", Source = "IdTcsAplicativo", Target = "ID_TCS_APLICATIVO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICATIVO", RelationPropertyName = "TCS_APLICATIVO" });

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
	[DomainIdentifier("ProcessorOverviewAplicativoDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class AplicativoDomainService : DomainService, IDataServiceContext 
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

		
	    public AplicativoDomainService() : this("", null, null) { }
	    public AplicativoDomainService(string connectionString) : this(connectionString, null, null) { }
	    public AplicativoDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public AplicativoDomainService(Linx.Framework.Autorizacao.BM.AutorizacaoContext dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public AplicativoDomainService(string connectionString, Linx.Framework.Autorizacao.BM.AutorizacaoContext dataContext, Dictionary<string, string> headers) : base() 
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
	
	    
	        foreach (ChangeSetEntry entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsAplicativo))
	        {
	            ((TcsAplicativo)entry.Entity).OnSavedChanges(this, changeSet.GetChangeOperation(entry.Entity));
	        }
    	
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
 	        var _TcsAplicativoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsAplicativo && e.Entity.GetType().Name == "TcsAplicativo" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _TcsAplicativoElements)
 	           if (((TcsAplicativo)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 
 	        //Remove inconsistent details
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsAplicativoConexao && e.Entity.GetType().Name == "TcsAplicativoConexao" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsAplicacao && e.Entity.GetType().Name == "TcsAplicacao" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
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
	    //Get All LookUpTcsConexaoDb.
	    public IQueryable<LookUpTcsConexaoDb> GetAllLookUpTcsConexaoDb()
	    {
	        return this.GetLookUpTcsConexaoDb(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsConexaoDb By EntitySearch.
	    public IQueryable<LookUpTcsConexaoDb> GetLookUpTcsConexaoDbByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsConexaoDb(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsConexaoDb.
	    public IQueryable<LookUpTcsConexaoDb> GetLookUpTcsConexaoDb(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TCS_CONEXAO_DB" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsConexaoDb";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsConexaoDb));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsConexaoDb> query =  
	
	            (from entity in this.DbContext.TCS_CONEXAO_DB.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpTcsConexaoDb()		
	            {
	            
                IdConexaoDb = entity.ID_CONEXAO_DB
                , NomeConexao = entity.NOME_CONEXAO
	            });

	            
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpTcsAplicacao.
	    public IQueryable<LookUpTcsAplicacao> GetAllLookUpTcsAplicacao()
	    {
	        return this.GetLookUpTcsAplicacao(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsAplicacao By EntitySearch.
	    public IQueryable<LookUpTcsAplicacao> GetLookUpTcsAplicacaoByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsAplicacao(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsAplicacao.
	    public IQueryable<LookUpTcsAplicacao> GetLookUpTcsAplicacao(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TCS_APLICACAO" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsAplicacao";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsAplicacao));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsAplicacao> query =  
	
	            (from entity in this.DbContext.TCS_APLICACAO.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpTcsAplicacao()		
	            {
	            
                DescricaoAplicacao = entity.DESCRICAO_APLICACAO
                , EmDesenvolvimento = entity.EM_DESENVOLVIMENTO
                , IdAplicacao = entity.ID_APLICACAO
                , Url = entity.URL
                , UrlWorkArea = entity.URL_WORK_AREA
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
	
		

	        if (entityName.InList("Linx.Framework.BV.Aplicativo.TcsAplicativo"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsAplicativo",
	        			NameSpace = "Linx.Framework.BV.Aplicativo",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsAplicativo",
	        			ClearMethodName = "ClearTcsAplicativo",
	        			QueryMethodName  = "GetPagedTcsAplicativo",	
	        			CountingMethodName  = "GetTcsAplicativo" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Aplicativo.TcsAplicativo"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Aplicativo.TcsAplicativo"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Aplicativo.TcsAplicativo", "Linx.Framework.BV.Aplicativo.TcsAplicativoConexao"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsAplicativoConexao" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.Aplicativo",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsAplicativo",	
	        			DisplayName = "Providers - BM",
	        			ClearMethodName = "ClearTcsAplicativoConexao" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsAplicativoConexao" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsAplicativoConexao" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Aplicativo.TcsAplicativoConexao"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Aplicativo.TcsAplicativoConexao" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Aplicativo.TcsAplicativo", "Linx.Framework.BV.Aplicativo.TcsAplicacao"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsAplicacao" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.Aplicativo",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsAplicativo",	
	        			DisplayName = "Aplicações Relacionadas",
	        			ClearMethodName = "ClearTcsAplicacao" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsAplicacao" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsAplicacao" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Aplicativo.TcsAplicacao"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Aplicativo.TcsAplicacao" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
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

         		    return new string[] { "Framework_AplicativoClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.AplicativoClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Framework_aplicativoService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.aplicativoService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear TcsAplicativo.
	    public IEnumerable<TcsAplicativo> ClearTcsAplicativo()
	    {
	        List<TcsAplicativo> result = new List<TcsAplicativo>();
	        result.Add(new TcsAplicativo());	
			
	        result[0].TcsAplicativoConexaoList = new List<TcsAplicativoConexao>();
	        ((List<TcsAplicativoConexao>)result[0].TcsAplicativoConexaoList).Add(new TcsAplicativoConexao());
			
	        result[0].TcsAplicacaoList = new List<TcsAplicacao>();
	        ((List<TcsAplicacao>)result[0].TcsAplicacaoList).Add(new TcsAplicacao());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsAplicativoConexao.
	    public IEnumerable<TcsAplicativoConexao> ClearTcsAplicativoConexao()
	    {
	        List<TcsAplicativoConexao> result = new List<TcsAplicativoConexao>();
	        result.Add(new TcsAplicativoConexao());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsAplicacao.
	    public IEnumerable<TcsAplicacao> ClearTcsAplicacao()
	    {
	        List<TcsAplicacao> result = new List<TcsAplicacao>();
	        result.Add(new TcsAplicacao());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsAplicativo.
	    public IQueryable<TcsAplicativo> GetTcsAplicativo()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsAplicativo> result = 
	            (from entity0 in this.DbContext.TCS_APLICATIVO
	            
	            	
	            select new TcsAplicativo()		
	            {
	            
                DescricaoAplicativo = entity0.DESCRICAO_APLICATIVO
                , IdTcsAplicativo = entity0.ID_TCS_APLICATIVO
			
                ,TcsAplicativoConexaoList = 
	                        (from entity1 in entity0.TCS_APLICATIVO_CONEXAO_LISTA
                                  let entity1Al1 = entity1.TCS_CONEXAO_DB
                                  let entity1Al2 = entity1.TCS_APLICATIVO
	                        
	                        	
	                        select new TcsAplicativoConexao()
	                        {
	                        
                                IdConexaoDb = entity1Al1.ID_CONEXAO_DB
                                , IdTcsAplicativo = entity1Al2.ID_TCS_APLICATIVO
                                , IdTcsAplicativoConexao = entity1.ID_TCS_APLICATIVO_CONEXAO
                                , NomeConexao = entity1Al1.NOME_CONEXAO
		
	                        }
	                        )
			
                ,TcsAplicacaoList = 
	                        (from entity1 in entity0.TCS_APLICACAO_LISTA
                                  let entity1Al1 = entity1.TCS_APLICATIVO
	                        
	                        	
	                        select new TcsAplicacao()
	                        {
	                        
                                DescricaoAplicacao = entity1.DESCRICAO_APLICACAO
                                , EmDesenvolvimento = entity1.EM_DESENVOLVIMENTO
                                , IdAplicacao = entity1.ID_APLICACAO
                                , IdTcsAplicativo = entity1Al1.ID_TCS_APLICATIVO
                                , Url = entity1.URL
                                , UrlWorkArea = entity1.URL_WORK_AREA
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsAplicativoConexao.
	    public IQueryable<TcsAplicativoConexao> GetTcsAplicativoConexao()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsAplicativoConexao> result = 
	            (from entity0 in this.DbContext.TCS_APLICATIVO_CONEXAO
                  let entity0Al1 = entity0.TCS_CONEXAO_DB
                  let entity0Al2 = entity0.TCS_APLICATIVO
	            
	            	
	            select new TcsAplicativoConexao()		
	            {
	            
                IdConexaoDb = entity0Al1.ID_CONEXAO_DB
                , IdTcsAplicativo = entity0Al2.ID_TCS_APLICATIVO
                , IdTcsAplicativoConexao = entity0.ID_TCS_APLICATIVO_CONEXAO
                , NomeConexao = entity0Al1.NOME_CONEXAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsAplicacao.
	    public IQueryable<TcsAplicacao> GetTcsAplicacao()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsAplicacao> result = 
	            (from entity0 in this.DbContext.TCS_APLICACAO
                  let entity0Al1 = entity0.TCS_APLICATIVO
	            
	            	
	            select new TcsAplicacao()		
	            {
	            
                DescricaoAplicacao = entity0.DESCRICAO_APLICACAO
                , EmDesenvolvimento = entity0.EM_DESENVOLVIMENTO
                , IdAplicacao = entity0.ID_APLICACAO
                , IdTcsAplicativo = entity0Al1.ID_TCS_APLICATIVO
                , Url = entity0.URL
                , UrlWorkArea = entity0.URL_WORK_AREA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAplicativoNoAssociations.
	    public IQueryable<TcsAplicativo> GetTcsAplicativoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsAplicativo> result = 
	            (from entity0 in this.DbContext.TCS_APLICATIVO
	            
	            	
	            select new TcsAplicativo()		
	            {
	            
                DescricaoAplicativo = entity0.DESCRICAO_APLICATIVO
                , IdTcsAplicativo = entity0.ID_TCS_APLICATIVO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAplicativoConexaoNoAssociations.
	    public IQueryable<TcsAplicativoConexao> GetTcsAplicativoConexaoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsAplicativoConexao> result = 
	            (from entity0 in this.DbContext.TCS_APLICATIVO_CONEXAO
                  let entity0Al1 = entity0.TCS_CONEXAO_DB
                  let entity0Al2 = entity0.TCS_APLICATIVO
	            
	            	
	            select new TcsAplicativoConexao()		
	            {
	            
                IdConexaoDb = entity0Al1.ID_CONEXAO_DB
                , IdTcsAplicativo = entity0Al2.ID_TCS_APLICATIVO
                , IdTcsAplicativoConexao = entity0.ID_TCS_APLICATIVO_CONEXAO
                , NomeConexao = entity0Al1.NOME_CONEXAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAplicacaoNoAssociations.
	    public IQueryable<TcsAplicacao> GetTcsAplicacaoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsAplicacao> result = 
	            (from entity0 in this.DbContext.TCS_APLICACAO
                  let entity0Al1 = entity0.TCS_APLICATIVO
	            
	            	
	            select new TcsAplicacao()		
	            {
	            
                DescricaoAplicacao = entity0.DESCRICAO_APLICACAO
                , EmDesenvolvimento = entity0.EM_DESENVOLVIMENTO
                , IdAplicacao = entity0.ID_APLICACAO
                , IdTcsAplicativo = entity0Al1.ID_TCS_APLICATIVO
                , Url = entity0.URL
                , UrlWorkArea = entity0.URL_WORK_AREA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	//Add filtering disabled property for TCS_APLICATIVO
	    	string[] bmDisabledTcsAplicativoList = this.GetEDM().GetFilteringDisabledList("TCS_APLICATIVO");
	    	if (bmDisabledTcsAplicativoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsAplicativoList.Contains("TCS_APLICATIVO.DESCRICAO_APLICATIVO"))
	    		{
	    			result.Add("TcsAplicativo|DescricaoAplicativo");
	    			result.Add("TcsAplicativo|TCS_APLICATIVO.DESCRICAO_APLICATIVO");
	    		}
	
	    		if (bmDisabledTcsAplicativoList.Contains("TCS_APLICATIVO.ID_TCS_APLICATIVO"))
	    		{
	    			result.Add("TcsAplicativo|IdTcsAplicativo");
	    			result.Add("TcsAplicativo|TCS_APLICATIVO.ID_TCS_APLICATIVO");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_APLICATIVO_CONEXAO
	    	string[] bmDisabledTcsAplicativoConexaoList = this.GetEDM().GetFilteringDisabledList("TCS_APLICATIVO_CONEXAO");
	    	if (bmDisabledTcsAplicativoConexaoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsAplicativoConexaoList.Contains("TCS_APLICATIVO_CONEXAO.ID_TCS_APLICATIVO_CONEXAO"))
	    		{
	    			result.Add("TcsAplicativoConexao|IdTcsAplicativoConexao");
	    			result.Add("TcsAplicativoConexao|TCS_APLICATIVO_CONEXAO.ID_TCS_APLICATIVO_CONEXAO");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_APLICACAO
	    	string[] bmDisabledTcsAplicacaoList = this.GetEDM().GetFilteringDisabledList("TCS_APLICACAO");
	    	if (bmDisabledTcsAplicacaoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsAplicacaoList.Contains("TCS_APLICACAO.DESCRICAO_APLICACAO"))
	    		{
	    			result.Add("TcsAplicacao|DescricaoAplicacao");
	    			result.Add("TcsAplicacao|TCS_APLICACAO.DESCRICAO_APLICACAO");
	    		}
	
	    		if (bmDisabledTcsAplicacaoList.Contains("TCS_APLICACAO.EM_DESENVOLVIMENTO"))
	    		{
	    			result.Add("TcsAplicacao|EmDesenvolvimento");
	    			result.Add("TcsAplicacao|TCS_APLICACAO.EM_DESENVOLVIMENTO");
	    		}
	
	    		if (bmDisabledTcsAplicacaoList.Contains("TCS_APLICACAO.ID_APLICACAO"))
	    		{
	    			result.Add("TcsAplicacao|IdAplicacao");
	    			result.Add("TcsAplicacao|TCS_APLICACAO.ID_APLICACAO");
	    		}
	
	    		if (bmDisabledTcsAplicacaoList.Contains("TCS_APLICACAO.URL"))
	    		{
	    			result.Add("TcsAplicacao|Url");
	    			result.Add("TcsAplicacao|TCS_APLICACAO.URL");
	    		}
	
	    		if (bmDisabledTcsAplicacaoList.Contains("TCS_APLICACAO.URL_WORK_AREA"))
	    		{
	    			result.Add("TcsAplicacao|UrlWorkArea");
	    			result.Add("TcsAplicacao|TCS_APLICACAO.URL_WORK_AREA");
	    		}
	    	}
	    	return result;
	    }

	    #endregion Excluded Filters

	    #region Get By EntitySearchId Methods Definitions.
	
				
	    [Ignore]
	    //Get TcsAplicativo By EntitySearchId.
	    public IQueryable<TcsAplicativo> GetTcsAplicativoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsAplicativoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsAplicativoConexao By EntitySearchId.
	    public IQueryable<TcsAplicativoConexao> GetTcsAplicativoConexaoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsAplicativoConexaoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsAplicacao By EntitySearchId.
	    public IQueryable<TcsAplicacao> GetTcsAplicacaoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsAplicacaoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsAplicativo By EntitySearchId.
	    public IQueryable<TcsAplicativo> GetTcsAplicativoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsAplicativoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsAplicativoConexao By EntitySearchId.
	    public IQueryable<TcsAplicativoConexao> GetTcsAplicativoConexaoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsAplicativoConexaoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsAplicacao By EntitySearchId.
	    public IQueryable<TcsAplicacao> GetTcsAplicacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsAplicacaoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get TcsAplicativo By Example.
	    [Ignore]
	    public IQueryable<TcsAplicativo> GetTcsAplicativoByExample(TcsAplicativo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsAplicativoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsAplicativoConexao By Example.
	    [Ignore]
	    public IQueryable<TcsAplicativoConexao> GetTcsAplicativoConexaoByExample(TcsAplicativoConexao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsAplicativoConexaoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsAplicacao By Example.
	    [Ignore]
	    public IQueryable<TcsAplicacao> GetTcsAplicacaoByExample(TcsAplicacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsAplicacaoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsAplicativo By Example.
	    [Ignore]
	    public IQueryable<TcsAplicativo> GetTcsAplicativoByExampleNoAssociations(TcsAplicativo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsAplicativoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsAplicativoConexao By Example.
	    [Ignore]
	    public IQueryable<TcsAplicativoConexao> GetTcsAplicativoConexaoByExampleNoAssociations(TcsAplicativoConexao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsAplicativoConexaoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsAplicacao By Example.
	    [Ignore]
	    public IQueryable<TcsAplicacao> GetTcsAplicacaoByExampleNoAssociations(TcsAplicacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsAplicacaoByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public TcsAplicativo GetTcsAplicativoByKey(Int32 idTcsAplicativo)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsAplicativo");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsAplicativo"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsAplicativo));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsAplicativoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsAplicativoConexao GetTcsAplicativoConexaoByKey(Int32 idTcsAplicativoConexao)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsAplicativoConexao");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsAplicativoConexao"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsAplicativoConexao));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsAplicativoConexaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsAplicacao GetTcsAplicacaoByKey(Int32 idAplicacao)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsAplicacao");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdAplicacao"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idAplicacao));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsAplicacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get TcsAplicativoByEntitySearch.
	    public IQueryable<TcsAplicativo> GetTcsAplicativoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAplicativo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAplicativo> result = 
	            (from entity0 in this.DbContext.TCS_APLICATIVO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsAplicativo()		
	            {
	            
                DescricaoAplicativo = entity0.DESCRICAO_APLICATIVO
                , IdTcsAplicativo = entity0.ID_TCS_APLICATIVO
			
                ,TcsAplicativoConexaoList = 
	                        (from entity1 in entity0.TCS_APLICATIVO_CONEXAO_LISTA
                                  let entity1Al1 = entity1.TCS_CONEXAO_DB
                                  let entity1Al2 = entity1.TCS_APLICATIVO
	                        
	                        	
	                        select new TcsAplicativoConexao()
	                        {
	                        
                                IdConexaoDb = entity1Al1.ID_CONEXAO_DB
                                , IdTcsAplicativo = entity1Al2.ID_TCS_APLICATIVO
                                , IdTcsAplicativoConexao = entity1.ID_TCS_APLICATIVO_CONEXAO
                                , NomeConexao = entity1Al1.NOME_CONEXAO
		
	                        }
	                        )
			
                ,TcsAplicacaoList = 
	                        (from entity1 in entity0.TCS_APLICACAO_LISTA
                                  let entity1Al1 = entity1.TCS_APLICATIVO
	                        
	                        	
	                        select new TcsAplicacao()
	                        {
	                        
                                DescricaoAplicacao = entity1.DESCRICAO_APLICACAO
                                , EmDesenvolvimento = entity1.EM_DESENVOLVIMENTO
                                , IdAplicacao = entity1.ID_APLICACAO
                                , IdTcsAplicativo = entity1Al1.ID_TCS_APLICATIVO
                                , Url = entity1.URL
                                , UrlWorkArea = entity1.URL_WORK_AREA
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAplicativoConexaoByEntitySearch.
	    public IQueryable<TcsAplicativoConexao> GetTcsAplicativoConexaoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAplicativoConexao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAplicativoConexao> result = 
	            (from entity0 in this.DbContext.TCS_APLICATIVO_CONEXAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_CONEXAO_DB
                  let entity0Al2 = entity0.TCS_APLICATIVO
	            
	            	
	            select new TcsAplicativoConexao()		
	            {
	            
                IdConexaoDb = entity0Al1.ID_CONEXAO_DB
                , IdTcsAplicativo = entity0Al2.ID_TCS_APLICATIVO
                , IdTcsAplicativoConexao = entity0.ID_TCS_APLICATIVO_CONEXAO
                , NomeConexao = entity0Al1.NOME_CONEXAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAplicacaoByEntitySearch.
	    public IQueryable<TcsAplicacao> GetTcsAplicacaoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAplicacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAplicacao> result = 
	            (from entity0 in this.DbContext.TCS_APLICACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_APLICATIVO
	            
	            	
	            select new TcsAplicacao()		
	            {
	            
                DescricaoAplicacao = entity0.DESCRICAO_APLICACAO
                , EmDesenvolvimento = entity0.EM_DESENVOLVIMENTO
                , IdAplicacao = entity0.ID_APLICACAO
                , IdTcsAplicativo = entity0Al1.ID_TCS_APLICATIVO
                , Url = entity0.URL
                , UrlWorkArea = entity0.URL_WORK_AREA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAplicativoByEntitySearchNoAssociations.
	    public IQueryable<TcsAplicativo> GetTcsAplicativoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAplicativo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAplicativo> result = 
	            (from entity0 in this.DbContext.TCS_APLICATIVO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsAplicativo()		
	            {
	            
                DescricaoAplicativo = entity0.DESCRICAO_APLICATIVO
                , IdTcsAplicativo = entity0.ID_TCS_APLICATIVO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAplicativoConexaoByEntitySearchNoAssociations.
	    public IQueryable<TcsAplicativoConexao> GetTcsAplicativoConexaoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAplicativoConexao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAplicativoConexao> result = 
	            (from entity0 in this.DbContext.TCS_APLICATIVO_CONEXAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_CONEXAO_DB
                  let entity0Al2 = entity0.TCS_APLICATIVO
	            
	            	
	            select new TcsAplicativoConexao()		
	            {
	            
                IdConexaoDb = entity0Al1.ID_CONEXAO_DB
                , IdTcsAplicativo = entity0Al2.ID_TCS_APLICATIVO
                , IdTcsAplicativoConexao = entity0.ID_TCS_APLICATIVO_CONEXAO
                , NomeConexao = entity0Al1.NOME_CONEXAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAplicacaoByEntitySearchNoAssociations.
	    public IQueryable<TcsAplicacao> GetTcsAplicacaoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAplicacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAplicacao> result = 
	            (from entity0 in this.DbContext.TCS_APLICACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_APLICATIVO
	            
	            	
	            select new TcsAplicacao()		
	            {
	            
                DescricaoAplicacao = entity0.DESCRICAO_APLICACAO
                , EmDesenvolvimento = entity0.EM_DESENVOLVIMENTO
                , IdAplicacao = entity0.ID_APLICACAO
                , IdTcsAplicativo = entity0Al1.ID_TCS_APLICATIVO
                , Url = entity0.URL
                , UrlWorkArea = entity0.URL_WORK_AREA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAplicativoConexaoParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsAplicativoConexaoParentComposition> GetTcsAplicativoConexaoParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "TCS_APLICATIVO", "TCS_APLICATIVO_CONEXAO", "TCS_APLICATIVO", typeof(TcsAplicativoConexaoParentComposition), typeof(TcsAplicacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAplicativoConexaoParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_APLICATIVO_CONEXAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_CONEXAO_DB
                  let entity0Al2 = entity0.TCS_APLICATIVO
	            
	            	
	            select new TcsAplicativoConexaoParentComposition()		
	            {
	            
                IdConexaoDb = entity0Al1.ID_CONEXAO_DB
                , IdTcsAplicativo = entity0Al2.ID_TCS_APLICATIVO
                , IdTcsAplicativoConexao = entity0.ID_TCS_APLICATIVO_CONEXAO
                , NomeConexao = entity0Al1.NOME_CONEXAO
                //TcsAplicativo Properties.
                , DescricaoAplicativo = entity0.TCS_APLICATIVO.DESCRICAO_APLICATIVO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAplicacaoParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsAplicacaoParentComposition> GetTcsAplicacaoParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "TCS_APLICATIVO", "TCS_APLICACAO", "TCS_APLICATIVO", typeof(TcsAplicacaoParentComposition), typeof(TcsAplicativoConexao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAplicacaoParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_APLICACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_APLICATIVO
	            
	            	
	            select new TcsAplicacaoParentComposition()		
	            {
	            
                DescricaoAplicacao = entity0.DESCRICAO_APLICACAO
                , EmDesenvolvimento = entity0.EM_DESENVOLVIMENTO
                , IdAplicacao = entity0.ID_APLICACAO
                , IdTcsAplicativo = entity0Al1.ID_TCS_APLICATIVO
                , Url = entity0.URL
                , UrlWorkArea = entity0.URL_WORK_AREA
                //TcsAplicativo Properties.
                , DescricaoAplicativo = entity0.TCS_APLICATIVO.DESCRICAO_APLICATIVO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get PagedTcsAplicativo.
	    public IQueryable<TcsAplicativo> GetPagedTcsAplicativo(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAplicativo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAplicativo> result = 
	            (from entity0 in this.DbContext.TCS_APLICATIVO.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_TCS_APLICATIVO ascending
	            
	            	
	            select new TcsAplicativo()		
	            {
	            
                DescricaoAplicativo = entity0.DESCRICAO_APLICATIVO
                , IdTcsAplicativo = entity0.ID_TCS_APLICATIVO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsAplicativoConexao.
	    public IQueryable<TcsAplicativoConexao> GetPagedTcsAplicativoConexao(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAplicativoConexao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAplicativoConexao> result = 
	            (from entity0 in this.DbContext.TCS_APLICATIVO_CONEXAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_CONEXAO_DB
                  let entity0Al2 = entity0.TCS_APLICATIVO
                orderby entity0.ID_TCS_APLICATIVO_CONEXAO ascending
	            
	            	
	            select new TcsAplicativoConexao()		
	            {
	            
                IdConexaoDb = entity0Al1.ID_CONEXAO_DB
                , IdTcsAplicativo = entity0Al2.ID_TCS_APLICATIVO
                , IdTcsAplicativoConexao = entity0.ID_TCS_APLICATIVO_CONEXAO
                , NomeConexao = entity0Al1.NOME_CONEXAO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsAplicacao.
	    public IQueryable<TcsAplicacao> GetPagedTcsAplicacao(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAplicacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAplicacao> result = 
	            (from entity0 in this.DbContext.TCS_APLICACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_APLICATIVO
                orderby entity0.ID_APLICACAO ascending
	            
	            	
	            select new TcsAplicacao()		
	            {
	            
                DescricaoAplicacao = entity0.DESCRICAO_APLICACAO
                , EmDesenvolvimento = entity0.EM_DESENVOLVIMENTO
                , IdAplicacao = entity0.ID_APLICACAO
                , IdTcsAplicativo = entity0Al1.ID_TCS_APLICATIVO
                , Url = entity0.URL
                , UrlWorkArea = entity0.URL_WORK_AREA
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsAplicativoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAplicativo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_APLICATIVO.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsAplicativoConexaoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAplicativoConexao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_APLICATIVO_CONEXAO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_CONEXAO_DB
                  let entityAl2 = entity.TCS_APLICATIVO
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsAplicacaoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAplicacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_APLICACAO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_APLICATIVO
	            
	            select 1
	            ).Count();	
		
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    
	    [Update()]	
	    //Update TcsAplicativo.
	    public void UpdateTcsAplicativo(TcsAplicativo entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsAplicativo.
	    public void InsertTcsAplicativo(TcsAplicativo entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsAplicativo.
	    public void DeleteTcsAplicativo(TcsAplicativo entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsAplicativoConexao.
	    public void UpdateTcsAplicativoConexao(TcsAplicativoConexao entity)
	    {



	
	        if (entity.TcsAplicativo.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsAplicativo) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsAplicativo); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsAplicativoConexao.
	    public void InsertTcsAplicativoConexao(TcsAplicativoConexao entity)
	    {



	
	        if (entity.TcsAplicativo.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsAplicativo) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsAplicativo);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsAplicativoConexao.
	    public void DeleteTcsAplicativoConexao(TcsAplicativoConexao entity)
	    {



	
	        if (entity.TcsAplicativo.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsAplicativo) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsAplicativo);
	            

	
	        }

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsAplicacao.
	    public void UpdateTcsAplicacao(TcsAplicacao entity)
	    {



	
	        if (entity.TcsAplicativo.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsAplicativo) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsAplicativo); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsAplicacao.
	    public void InsertTcsAplicacao(TcsAplicacao entity)
	    {



	
	        if (entity.TcsAplicativo.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsAplicativo) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsAplicativo);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsAplicacao.
	    public void DeleteTcsAplicacao(TcsAplicacao entity)
	    {



	
	        if (entity.TcsAplicativo.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsAplicativo) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsAplicativo);
	            

	
	        }

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}