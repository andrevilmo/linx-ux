					
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

namespace Linx.Framework.BV.ExecutaImportacao
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_ARQUIVO.ID_ARQUIVO", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Arquivos];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsArquivo,TcsArquivo.TcsArquivoImportar,TcsArquivoImportar.TcsArquivoLog];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdArquivo];ReadOnly[false];Entities[TCS_ARQUIVO:IdArquivo];SubQueryInfo[];EdmEntityName[TCS_ARQUIVO];EntityRelations[];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsArquivo")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.ExecutaImportacao.TcsArquivo")]
	public partial class TcsArquivo : Linx.Data.Entity
	{

	
		
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.TcsArquivoImportarList != null && this.TcsArquivoImportarList.Count() > 0)
	      {
	         foreach (var entity in this.TcsArquivoImportarList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      this.TcsArquivoImportarList = null;
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(ExecutaImportacaoDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("TcsArquivoImportar"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsArquivoImportar");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdArquivoFk"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdArquivo));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsArquivoImportar and all sub-details
	         if (this.TcsArquivoImportarList == null || this.TcsArquivoImportarList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsArquivoImportarList = context.GetPagedTcsArquivoImportar(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsArquivoImportarList = (from r in context.GetTcsArquivoImportarByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	         foreach(TcsArquivoImportar detail in this.TcsArquivoImportarList)
	         {
	             detail.FillDetails(context, serializedEntitySearch, jEntitySearch, viewNames, take);
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _TcsArquivoImportarElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsArquivoImportar && ((TcsArquivoImportar)e.Entity).TcsArquivo == null && e.Associations == null && e.OriginalAssociations == null && ((TcsArquivoImportar)e.Entity).IdArquivoFk == this.IdArquivo).ToList();
 	      if (_TcsArquivoImportarElements.Count > 0 && this.TcsArquivoImportarList.Count() == 0)
 	      {
 	          this.TcsArquivoImportarList = _TcsArquivoImportarElements.Select(e => (TcsArquivoImportar)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsArquivoImportarElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsArquivoImportar)detail.Entity).TcsArquivo = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsArquivo", new int[] { masterIndex });
 	              ((TcsArquivoImportar)detail.Entity).AdjustHierarchyForSaving(detail, changeSet);
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsArquivoImportarList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For IdArquivo
	    partial void OnIdArquivoChanging(Int32 value);
	    partial void OnIdArquivoChanged();

	    private Int32 _IdArquivo;

	    [DataMember(IsRequired = true, Name = "IdArquivo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO.ID_ARQUIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.ID_ARQUIVO")]
	    public Int32 IdArquivo
	    {
	    	    get
	    	    {
	    	          return _IdArquivo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdArquivo != value)
	    	          {
	    	              this.ValidateProperty("IdArquivo", value);
	    	              this.OnIdArquivoChanging(value);
	    	              this.RaiseDataMemberChanging("IdArquivo");
	    	              this._IdArquivo = value;
	    	              this.RaiseDataMemberChanged("IdArquivo");
	    	              this.OnIdArquivoChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIdArquivo;
	    [DataMember(Name = "TemporaryIdArquivo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID (Tmp)", Description="Temporary Key", Order = 6, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdArquivo
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdArquivo.IsNullOrEmpty())
	    	                this._TemporaryIdArquivo = this._IdArquivo;
	    	          return this._TemporaryIdArquivo;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdArquivo != value)
	    	              this._TemporaryIdArquivo = value;
	    	    }
	    }	

	    #endregion Data Properties

	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<TcsArquivoImportar> _TcsArquivoImportarList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsArquivo_TcsArquivoImportar", "IdArquivo", "IdArquivoFk", IsForeignKey=false)]
	    [DataMember(Name = "TcsArquivoImportarList", EmitDefaultValue = true)]
	    public IEnumerable<TcsArquivoImportar> TcsArquivoImportarList
	    {
	        get
	        {
	
	            if (this._TcsArquivoImportarList == null)
	            	this._TcsArquivoImportarList = new List<TcsArquivoImportar>();
	
	            return this._TcsArquivoImportarList;
	        }
	        set
	        {
	            if (this._TcsArquivoImportarList != value)
	            {
	                this._TcsArquivoImportarList = value;
	                this.RaisePropertyChanged("TcsArquivoImportarList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_ARQUIVO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_ARQUIVO), QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO.ID_ARQUIVO", Source = "IdArquivo", Target = "ID_ARQUIVO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO", RelationPropertyName = "TCS_ARQUIVO" });

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

		

	[LinxPublicationView(PrimaryKeys="TCS_ARQUIVO.ID_ARQUIVO", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Arquivos];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdArquivo];ReadOnly[false];Entities[TCS_ARQUIVO:IdArquivo];SubQueryInfo[Select 1 From #ParentAlias#. as #Alias#];EdmEntityName[TCS_ARQUIVO];EntityRelations[];EdmParentEntityName[TCS_ARQUIVO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsArquivoImportar")]
	[Serializable()]
	public partial class TcsArquivoImportar : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(ExecutaImportacaoDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsArquivo");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdArquivo"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdArquivoFk));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsArquivo
	         this.TcsArquivo = (from r in context.GetTcsArquivoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Load Data Parent

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.TcsArquivoLogList != null && this.TcsArquivoLogList.Count() > 0)
	      {
	         foreach (var entity in this.TcsArquivoLogList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      this.TcsArquivoLogList = null;
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(ExecutaImportacaoDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("TcsArquivoLog"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsArquivoLog");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdArquivoFk"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdArquivo));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsArquivoLog and all sub-details
	         if (this.TcsArquivoLogList == null || this.TcsArquivoLogList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsArquivoLogList = context.GetPagedTcsArquivoLog(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsArquivoLogList = (from r in context.GetTcsArquivoLogByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _TcsArquivoLogElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsArquivoLog && ((TcsArquivoLog)e.Entity).TcsArquivoImportar == null && e.Associations == null && e.OriginalAssociations == null && ((TcsArquivoLog)e.Entity).IdArquivoFk == this.IdArquivo).ToList();
 	      if (_TcsArquivoLogElements.Count > 0 && this.TcsArquivoLogList.Count() == 0)
 	      {
 	          this.TcsArquivoLogList = _TcsArquivoLogElements.Select(e => (TcsArquivoLog)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsArquivoLogElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsArquivoLog)detail.Entity).TcsArquivoImportar = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsArquivoImportar", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsArquivoLogList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For CaminhoArquivo
	    partial void OnCaminhoArquivoChanging(System.String value);
	    partial void OnCaminhoArquivoChanged();

	    private System.String _CaminhoArquivo;

	    [DataMember(IsRequired = true, Name = "CaminhoArquivo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Diretório", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO.CAMINHO_ARQUIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.CAMINHO_ARQUIVO")]
	    public System.String CaminhoArquivo
	    {
	    	    get
	    	    {
	    	          return _CaminhoArquivo;
	    	    }
	    	    set
	    	    {
	    	          if (this._CaminhoArquivo != value)
	    	          {
	    	              this.ValidateProperty("CaminhoArquivo", value);
	    	              this.OnCaminhoArquivoChanging(value);
	    	              this.RaiseDataMemberChanging("CaminhoArquivo");
	    	              this._CaminhoArquivo = value;
	    	              this.RaiseDataMemberChanged("CaminhoArquivo");
	    	              this.OnCaminhoArquivoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For CodArquivo
	    partial void OnCodArquivoChanging(System.String value);
	    partial void OnCodArquivoChanged();

	    private System.String _CodArquivo;

	    [DataMember(IsRequired = true, Name = "CodArquivo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Código", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO.COD_ARQUIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.COD_ARQUIVO")]
	    public System.String CodArquivo
	    {
	    	    get
	    	    {
	    	          return _CodArquivo;
	    	    }
	    	    set
	    	    {
	    	          if (this._CodArquivo != value)
	    	          {
	    	              this.ValidateProperty("CodArquivo", value);
	    	              this.OnCodArquivoChanging(value);
	    	              this.RaiseDataMemberChanging("CodArquivo");
	    	              this._CodArquivo = value;
	    	              this.RaiseDataMemberChanged("CodArquivo");
	    	              this.OnCodArquivoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescArquivo
	    partial void OnDescArquivoChanging(System.String value);
	    partial void OnDescArquivoChanged();

	    private System.String _DescArquivo;

	    [DataMember(IsRequired = true, Name = "DescArquivo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(90)]
	    [FunctionalPoint("Precision[90:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO.DESC_ARQUIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.DESC_ARQUIVO")]
	    public System.String DescArquivo
	    {
	    	    get
	    	    {
	    	          return _DescArquivo;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescArquivo != value)
	    	          {
	    	              this.ValidateProperty("DescArquivo", value);
	    	              this.OnDescArquivoChanging(value);
	    	              this.RaiseDataMemberChanging("DescArquivo");
	    	              this._DescArquivo = value;
	    	              this.RaiseDataMemberChanged("DescArquivo");
	    	              this.OnDescArquivoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdArquivo
	    partial void OnIdArquivoChanging(Int32 value);
	    partial void OnIdArquivoChanged();

	    private Int32 _IdArquivo;

	    [DataMember(IsRequired = true, Name = "IdArquivo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO.ID_ARQUIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.ID_ARQUIVO")]
	    public Int32 IdArquivo
	    {
	    	    get
	    	    {
	    	          return _IdArquivo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdArquivo != value)
	    	          {
	    	              this.ValidateProperty("IdArquivo", value);
	    	              this.OnIdArquivoChanging(value);
	    	              this.RaiseDataMemberChanging("IdArquivo");
	    	              this._IdArquivo = value;
	    	              this.RaiseDataMemberChanged("IdArquivo");
	    	              this.OnIdArquivoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdArquivoFk
	    partial void OnIdArquivoFkChanging(Int32 value);
	    partial void OnIdArquivoFkChanged();

	    private Int32 _IdArquivoFk;

	    [DataMember(IsRequired = true, Name = "IdArquivoFk", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Arquivo Fk", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.ID_ARQUIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.ID_ARQUIVO")]
	    public Int32 IdArquivoFk
	    {
	    	    get
	    	    {
	    	          return _IdArquivoFk;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdArquivoFk != value)
	    	          {
	    	              this.ValidateProperty("IdArquivoFk", value);
	    	              this.OnIdArquivoFkChanging(value);
	    	              this.RaiseDataMemberChanging("IdArquivoFk");
	    	              this._IdArquivoFk = value;
	    	              this.RaiseDataMemberChanged("IdArquivoFk");
	    	              this.OnIdArquivoFkChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoArquivo
	    partial void OnLxTipoArquivoChanging(System.String value);
	    partial void OnLxTipoArquivoChanged();

	    private System.String _LxTipoArquivo;

	    [DataMember(IsRequired = true, Name = "LxTipoArquivo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(1)]
	    [FunctionalPoint("Precision[1:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoArquivo];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO.LX_TIPO_ARQUIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.LX_TIPO_ARQUIVO")]
	    public System.String LxTipoArquivo
	    {
	    	    get
	    	    {
	    	          return _LxTipoArquivo;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoArquivo != value)
	    	          {
	    	              this.ValidateProperty("LxTipoArquivo", value);
	    	              this.OnLxTipoArquivoChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoArquivo");
	    	              this._LxTipoArquivo = value;
	    	              this.RaiseDataMemberChanged("LxTipoArquivo");
	    	              this.OnLxTipoArquivoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeArquivo
	    partial void OnNomeArquivoChanging(System.String value);
	    partial void OnNomeArquivoChanged();

	    private System.String _NomeArquivo;

	    [DataMember(IsRequired = true, Name = "NomeArquivo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO.NOME_ARQUIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.NOME_ARQUIVO")]
	    public System.String NomeArquivo
	    {
	    	    get
	    	    {
	    	          return _NomeArquivo;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeArquivo != value)
	    	          {
	    	              this.ValidateProperty("NomeArquivo", value);
	    	              this.OnNomeArquivoChanging(value);
	    	              this.RaiseDataMemberChanging("NomeArquivo");
	    	              this._NomeArquivo = value;
	    	              this.RaiseDataMemberChanged("NomeArquivo");
	    	              this.OnNomeArquivoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Selecao
	    partial void OnSelecaoChanging(Boolean value);
	    partial void OnSelecaoChanged();

	    private Boolean _Selecao;

	    [DataMember(IsRequired = true, Name = "Selecao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Importar", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[false];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="false")]
	    public Boolean Selecao
	    {
	    	    get
	    	    {
	    	          return _Selecao;
	    	    }
	    	    set
	    	    {
	    	          if (this._Selecao != value)
	    	          {
	    	              this.ValidateProperty("Selecao", value);
	    	              this.OnSelecaoChanging(value);
	    	              this.RaiseDataMemberChanging("Selecao");
	    	              this._Selecao = value;
	    	              this.RaiseDataMemberChanged("Selecao");
	    	              this.OnSelecaoChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIdArquivo;
	    [DataMember(Name = "TemporaryIdArquivo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID (Tmp)", Description="Temporary Key", Order = 6, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdArquivo
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdArquivo.IsNullOrEmpty())
	    	                this._TemporaryIdArquivo = this._IdArquivo;
	    	          return this._TemporaryIdArquivo;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdArquivo != value)
	    	              this._TemporaryIdArquivo = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsArquivo _TcsArquivo;
	    [DataMember(Name = "TcsArquivo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsArquivo_TcsArquivoImportar", "IdArquivoFk", "IdArquivo", IsForeignKey=true)]
	    public TcsArquivo TcsArquivo
	    {
	        get
	        {
	            return this._TcsArquivo;
	        }
	        set
	        {
	            if (this._TcsArquivo != value)
	            {
	                this._TcsArquivo = value;
	                this.RaisePropertyChanged("TcsArquivoList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<TcsArquivoLog> _TcsArquivoLogList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsArquivoImportar_TcsArquivoLog", "IdArquivo", "IdArquivoFk", IsForeignKey=false)]
	    [DataMember(Name = "TcsArquivoLogList", EmitDefaultValue = true)]
	    public IEnumerable<TcsArquivoLog> TcsArquivoLogList
	    {
	        get
	        {
	
	            if (this._TcsArquivoLogList == null)
	            	this._TcsArquivoLogList = new List<TcsArquivoLog>();
	
	            return this._TcsArquivoLogList;
	        }
	        set
	        {
	            if (this._TcsArquivoLogList != value)
	            {
	                this._TcsArquivoLogList = value;
	                this.RaisePropertyChanged("TcsArquivoLogList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_ARQUIVO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_ARQUIVO), QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO.ID_ARQUIVO", Source = "IdArquivo", Target = "ID_ARQUIVO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO", RelationPropertyName = "TCS_ARQUIVO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO.ID_ARQUIVO", Source = "IdArquivoFk", Target = "ID_ARQUIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO", RelationPropertyName = "TCS_ARQUIVO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO.COD_ARQUIVO", Source = "CodArquivo", Target = "COD_ARQUIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO", RelationPropertyName = "TCS_ARQUIVO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO.DESC_ARQUIVO", Source = "DescArquivo", Target = "DESC_ARQUIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO", RelationPropertyName = "TCS_ARQUIVO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO.NOME_ARQUIVO", Source = "NomeArquivo", Target = "NOME_ARQUIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO", RelationPropertyName = "TCS_ARQUIVO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO.CAMINHO_ARQUIVO", Source = "CaminhoArquivo", Target = "CAMINHO_ARQUIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO", RelationPropertyName = "TCS_ARQUIVO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO.LX_TIPO_ARQUIVO", Source = "LxTipoArquivo", Target = "LX_TIPO_ARQUIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO", RelationPropertyName = "TCS_ARQUIVO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxTipoArquivoValues()
	    {
	    	    return Linx.Framework.BV.Domains.TipoArquivo.GetValues();
	    }
	    private string _lxTipoArquivoName;
	    [DataMember(IsRequired = false, Name = "LxTipoArquivoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxTipoArquivoName
	    {
	    	    get { if (this.LxTipoArquivo.IsNullOrEmpty()) { _lxTipoArquivoName = String.Empty; } else { string key = this.LxTipoArquivo.ToString(); var dmValues = this.GetLxTipoArquivoValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxTipoArquivoName) _lxTipoArquivoName = domainName; } return _lxTipoArquivoName; } set { _lxTipoArquivoName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_ARQUIVO_LOG.ID_ARQUIVO_LOG", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Log];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdArquivoLog];ReadOnly[false];Entities[TCS_ARQUIVO_LOG:IdArquivoLog];SubQueryInfo[Select 1 From #ParentAlias#.TCS_ARQUIVO_LOG_LISTA as #Alias#];EdmEntityName[TCS_ARQUIVO_LOG];EntityRelations[TCS_ARQUIVO(TCS_ARQUIVO)];EdmParentEntityName[TCS_ARQUIVO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsArquivoLog")]
	[Serializable()]
	public partial class TcsArquivoLog : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(ExecutaImportacaoDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsArquivoImportar");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdArquivo"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdArquivoFk));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsArquivoImportar
	         this.TcsArquivoImportar = (from r in context.GetTcsArquivoImportarByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For DataLog
	    partial void OnDataLogChanging(System.DateTime value);
	    partial void OnDataLogChanged();

	    private System.DateTime _DataLog;

	    [DataMember(IsRequired = true, Name = "DataLog", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Data", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_LOG.DATA_LOG];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_LOG.DATA_LOG")]
	    public System.DateTime DataLog
	    {
	    	    get
	    	    {
	    	          return _DataLog;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataLog != value)
	    	          {
	    	              this.ValidateProperty("DataLog", value);
	    	              this.OnDataLogChanging(value);
	    	              this.RaiseDataMemberChanging("DataLog");
	    	              this._DataLog = value;
	    	              this.RaiseDataMemberChanged("DataLog");
	    	              this.OnDataLogChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescLog
	    partial void OnDescLogChanging(System.String value);
	    partial void OnDescLogChanged();

	    private System.String _DescLog;

	    [DataMember(Name = "DescLog", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(1000)]
	    [FunctionalPoint("Precision[1000:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_LOG.DESC_LOG];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_LOG.DESC_LOG")]
	    public System.String DescLog
	    {
	    	    get
	    	    {
	    	          return _DescLog;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescLog != value)
	    	          {
	    	              this.ValidateProperty("DescLog", value);
	    	              this.OnDescLogChanging(value);
	    	              this.RaiseDataMemberChanging("DescLog");
	    	              this._DescLog = value;
	    	              this.RaiseDataMemberChanged("DescLog");
	    	              this.OnDescLogChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdArquivoFk
	    partial void OnIdArquivoFkChanging(Int32 value);
	    partial void OnIdArquivoFkChanged();

	    private Int32 _IdArquivoFk;

	    [DataMember(IsRequired = true, Name = "IdArquivoFk", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Arquivo Fk", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_LOG.ID_ARQUIVO_FK];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_LOG.ID_ARQUIVO_FK")]
	    public Int32 IdArquivoFk
	    {
	    	    get
	    	    {
	    	          return _IdArquivoFk;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdArquivoFk != value)
	    	          {
	    	              this.ValidateProperty("IdArquivoFk", value);
	    	              this.OnIdArquivoFkChanging(value);
	    	              this.RaiseDataMemberChanging("IdArquivoFk");
	    	              this._IdArquivoFk = value;
	    	              this.RaiseDataMemberChanged("IdArquivoFk");
	    	              this.OnIdArquivoFkChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdArquivoLog
	    partial void OnIdArquivoLogChanging(Int32 value);
	    partial void OnIdArquivoLogChanged();

	    private Int32 _IdArquivoLog;

	    [DataMember(IsRequired = true, Name = "IdArquivoLog", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Arquivo Log", Description="", Order = 5, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_LOG.ID_ARQUIVO_LOG];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_LOG.ID_ARQUIVO_LOG")]
	    public Int32 IdArquivoLog
	    {
	    	    get
	    	    {
	    	          return _IdArquivoLog;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdArquivoLog != value)
	    	          {
	    	              this.ValidateProperty("IdArquivoLog", value);
	    	              this.OnIdArquivoLogChanging(value);
	    	              this.RaiseDataMemberChanging("IdArquivoLog");
	    	              this._IdArquivoLog = value;
	    	              this.RaiseDataMemberChanged("IdArquivoLog");
	    	              this.OnIdArquivoLogChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoLog
	    partial void OnLxTipoLogChanging(Int32 value);
	    partial void OnLxTipoLogChanged();

	    private Int32 _LxTipoLog;

	    [DataMember(IsRequired = true, Name = "LxTipoLog", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoLog];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_LOG.LX_TIPO_LOG];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_LOG.LX_TIPO_LOG")]
	    public Int32 LxTipoLog
	    {
	    	    get
	    	    {
	    	          return _LxTipoLog;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoLog != value)
	    	          {
	    	              this.ValidateProperty("LxTipoLog", value);
	    	              this.OnLxTipoLogChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoLog");
	    	              this._LxTipoLog = value;
	    	              this.RaiseDataMemberChanged("LxTipoLog");
	    	              this.OnLxTipoLogChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIdArquivoLog;
	    [DataMember(Name = "TemporaryIdArquivoLog", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Arquivo Log (Tmp)", Description="Temporary Key", Order = 5, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdArquivoLog
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdArquivoLog.IsNullOrEmpty())
	    	                this._TemporaryIdArquivoLog = this._IdArquivoLog;
	    	          return this._TemporaryIdArquivoLog;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdArquivoLog != value)
	    	              this._TemporaryIdArquivoLog = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsArquivoImportar _TcsArquivoImportar;
	    [DataMember(Name = "TcsArquivoImportar", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsArquivoImportar_TcsArquivoLog", "IdArquivoFk", "IdArquivo", IsForeignKey=true)]
	    public TcsArquivoImportar TcsArquivoImportar
	    {
	        get
	        {
	            return this._TcsArquivoImportar;
	        }
	        set
	        {
	            if (this._TcsArquivoImportar != value)
	            {
	                this._TcsArquivoImportar = value;
	                this.RaisePropertyChanged("TcsArquivoImportarList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_ARQUIVO_LOG").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_ARQUIVO_LOG), QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_LOG" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_LOG.DATA_LOG", Source = "DataLog", Target = "DATA_LOG", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_LOG", RelationPropertyName = "TCS_ARQUIVO_LOG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_LOG.DESC_LOG", Source = "DescLog", Target = "DESC_LOG", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_LOG", RelationPropertyName = "TCS_ARQUIVO_LOG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_LOG.LX_TIPO_LOG", Source = "LxTipoLog", Target = "LX_TIPO_LOG", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_LOG", RelationPropertyName = "TCS_ARQUIVO_LOG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_LOG.ID_ARQUIVO_FK", Source = "IdArquivoFk", Target = "ID_ARQUIVO_FK", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_LOG", RelationPropertyName = "TCS_ARQUIVO_LOG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_LOG.ID_ARQUIVO_LOG", Source = "IdArquivoLog", Target = "ID_ARQUIVO_LOG", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_LOG", RelationPropertyName = "TCS_ARQUIVO_LOG" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxTipoLogValues()
	    {
	    	    return Linx.Framework.BV.Domains.TipoLog.GetValues();
	    }
	    private string _lxTipoLogName;
	    [DataMember(IsRequired = false, Name = "LxTipoLogName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxTipoLogName
	    {
	    	    get { if (this.LxTipoLog.IsNullOrEmpty()) { _lxTipoLogName = String.Empty; } else { string key = this.LxTipoLog.ToString(); var dmValues = this.GetLxTipoLogValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxTipoLogName) _lxTipoLogName = domainName; } return _lxTipoLogName; } set { _lxTipoLogName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	//////////////////////// DomainService Class V1 ///////////////////////
	///////////////////////////////////////////////////////////////////////
	[EnableClientAccess()]	
	[DomainIdentifier("ProcessorOverviewExecutaImportacaoDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class ExecutaImportacaoDomainService : DomainService, IDataServiceContext 
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

	
	    private Linx.Framework.ControleSistema.BM.ControleSistemaContext _dbContext;
	    protected Linx.Framework.ControleSistema.BM.ControleSistemaContext DbContext 
	    { 
	    	get 
	    	{
	        	if (this._dbContext == null)
	        	{
	        		this._dbContext = new Linx.Framework.ControleSistema.BM.ControleSistemaContext(connectionString, this.Headers);
	        		((System.Data.Entity.Infrastructure.IObjectContextAdapter)this._dbContext).ObjectContext.CommandTimeout = 180;
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

		
	    public ExecutaImportacaoDomainService() : this("", null, null){ }
	    public ExecutaImportacaoDomainService(string connectionString) : this(connectionString, null, null) { }
	    public ExecutaImportacaoDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public ExecutaImportacaoDomainService(Linx.Framework.ControleSistema.BM.ControleSistemaContext dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public ExecutaImportacaoDomainService(string connectionString, Linx.Framework.ControleSistema.BM.ControleSistemaContext dataContext, Dictionary<string, string> headers) : base() 
	    { 
	    	this.connectionString = connectionString;
	    	this.Headers = headers;
	    	this._dbContext = dataContext; 


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
 	        var _TcsArquivoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsArquivo && e.Entity.GetType().Name == "TcsArquivo" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _TcsArquivoElements)
 	           if (((TcsArquivo)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 
 	        //Remove inconsistent details
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsArquivoImportar && e.Entity.GetType().Name == "TcsArquivoImportar" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsArquivoLog && e.Entity.GetType().Name == "TcsArquivoLog" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
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
	
		

	        if (entityName.InList("Linx.Framework.BV.ExecutaImportacao.TcsArquivo"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsArquivo",
	        			NameSpace = "Linx.Framework.BV.ExecutaImportacao",
	        			ParentClassName = null,	
	        			DisplayName = "Arquivos",
	        			ClearMethodName = "ClearTcsArquivo",
	        			QueryMethodName  = "GetPagedTcsArquivo",	
	        			CountingMethodName  = "GetTcsArquivo" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.ExecutaImportacao.TcsArquivo"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.ExecutaImportacao.TcsArquivo"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.ExecutaImportacao.TcsArquivo", "Linx.Framework.BV.ExecutaImportacao.TcsArquivoImportar"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsArquivoImportar",
	        			NameSpace = "Linx.Framework.BV.ExecutaImportacao",
	        			ParentClassName = "TcsArquivo",	
	        			DisplayName = "Arquivos",
	        			ClearMethodName = "ClearTcsArquivoImportar",
	        			QueryMethodName  = "GetPagedTcsArquivoImportar",	
	        			CountingMethodName  = "GetTcsArquivoImportar" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.ExecutaImportacao.TcsArquivoImportar"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.ExecutaImportacao.TcsArquivoImportar"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.ExecutaImportacao.TcsArquivo", "Linx.Framework.BV.ExecutaImportacao.TcsArquivoLog"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsArquivoLog",
	        			NameSpace = "Linx.Framework.BV.ExecutaImportacao",
	        			ParentClassName = "TcsArquivoImportar",	
	        			DisplayName = "Log",
	        			ClearMethodName = "ClearTcsArquivoLog",
	        			QueryMethodName  = "GetPagedTcsArquivoLog",	
	        			CountingMethodName  = "GetTcsArquivoLog" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.ExecutaImportacao.TcsArquivoLog"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.ExecutaImportacao.TcsArquivoLog"), forceAll: forceAll)
	        		});
	        }
	
            return result;
        }
	
	    [Ignore]
	    public string[] GetClientDomains()
        {	


             return new string[] { "Framework_MobileDataDomains", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.MobileDataDomains.res", System.Reflection.Assembly.GetExecutingAssembly()) };	

        }

	    [Ignore]
	    public string[] GetClientService()
        {	


             return new string[] { "Framework_executaImportacaoService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.executaImportacaoService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	

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
	    //Clear TcsArquivo.
	    public IEnumerable<TcsArquivo> ClearTcsArquivo()
	    {
	        List<TcsArquivo> result = new List<TcsArquivo>();
	        result.Add(new TcsArquivo());	
			
	        result[0].TcsArquivoImportarList = new List<TcsArquivoImportar>();
	        ((List<TcsArquivoImportar>)result[0].TcsArquivoImportarList).Add(new TcsArquivoImportar());
			
	        ((List<TcsArquivoImportar>)result[0].TcsArquivoImportarList)[0].TcsArquivoLogList = new List<TcsArquivoLog>();
	        ((List<TcsArquivoLog>)((List<TcsArquivoImportar>)result[0].TcsArquivoImportarList)[0].TcsArquivoLogList).Add(new TcsArquivoLog());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsArquivoImportar.
	    public IEnumerable<TcsArquivoImportar> ClearTcsArquivoImportar()
	    {
	        List<TcsArquivoImportar> result = new List<TcsArquivoImportar>();
	        result.Add(new TcsArquivoImportar());	
			
	        result[0].TcsArquivoLogList = new List<TcsArquivoLog>();
	        ((List<TcsArquivoLog>)result[0].TcsArquivoLogList).Add(new TcsArquivoLog());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsArquivoLog.
	    public IEnumerable<TcsArquivoLog> ClearTcsArquivoLog()
	    {
	        List<TcsArquivoLog> result = new List<TcsArquivoLog>();
	        result.Add(new TcsArquivoLog());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    
	    [Ignore]
	    //Get TcsArquivo.
	    public IQueryable<TcsArquivo> GetTcsArquivo()
	    {




		

	        IQueryable<TcsArquivo> result = 
	            (from entity0 in TcsArquivo.OnSearchingReplacement(this.DbContext, null, null, null) select entity0);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsArquivoImportar.
	    public IQueryable<TcsArquivoImportar> GetTcsArquivoImportar()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsArquivoImportar> result = 
	            (from entity0 in this.DbContext.TCS_ARQUIVO
	            
	            	
	            select new TcsArquivoImportar()		
	            {
	            
                CaminhoArquivo = entity0.CAMINHO_ARQUIVO
                , CodArquivo = entity0.COD_ARQUIVO
                , DescArquivo = entity0.DESC_ARQUIVO
                , IdArquivo = entity0.ID_ARQUIVO
                , IdArquivoFk = entity0.ID_ARQUIVO
                , LxTipoArquivo = entity0.LX_TIPO_ARQUIVO
                , LxTipoArquivoName = ((entity0.LX_TIPO_ARQUIVO) == "E" ? "Excel" : ((entity0.LX_TIPO_ARQUIVO) == "T" ? "Text" : ((entity0.LX_TIPO_ARQUIVO) == "G" ? "Todos" : ((entity0.LX_TIPO_ARQUIVO) == "X" ? "XML" : ""))))
                , NomeArquivo = entity0.NOME_ARQUIVO
                , Selecao = false
			
                ,TcsArquivoLogList = 
	                        (from entity1 in entity0.TCS_ARQUIVO_LOG_LISTA
	                        
	                        	
	                        select new TcsArquivoLog()
	                        {
	                        
                                DataLog = entity1.DATA_LOG
                                , DescLog = entity1.DESC_LOG
                                , IdArquivoFk = entity1.ID_ARQUIVO_FK
                                , IdArquivoLog = entity1.ID_ARQUIVO_LOG
                                , LxTipoLog = entity1.LX_TIPO_LOG
                                , LxTipoLogName = ((entity1.LX_TIPO_LOG) == 2 ? "Geração de Arquivo" : ((entity1.LX_TIPO_LOG) == 3 ? "Importação de Layout" : ((entity1.LX_TIPO_LOG) == 1 ? "Leitura de Arquivo" : "")))
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsArquivoLog.
	    public IQueryable<TcsArquivoLog> GetTcsArquivoLog()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsArquivoLog> result = 
	            (from entity0 in this.DbContext.TCS_ARQUIVO_LOG
	            
	            	
	            select new TcsArquivoLog()		
	            {
	            
                DataLog = entity0.DATA_LOG
                , DescLog = entity0.DESC_LOG
                , IdArquivoFk = entity0.ID_ARQUIVO_FK
                , IdArquivoLog = entity0.ID_ARQUIVO_LOG
                , LxTipoLog = entity0.LX_TIPO_LOG
                , LxTipoLogName = ((entity0.LX_TIPO_LOG) == 2 ? "Geração de Arquivo" : ((entity0.LX_TIPO_LOG) == 3 ? "Importação de Layout" : ((entity0.LX_TIPO_LOG) == 1 ? "Leitura de Arquivo" : "")))
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsArquivoNoAssociations.
	    public IQueryable<TcsArquivo> GetTcsArquivoNoAssociations()
	    {




		

	        IQueryable<TcsArquivo> result = 
	            (from entity0 in TcsArquivo.OnSearchingReplacement(this.DbContext, null, null, null) select entity0);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsArquivoImportarNoAssociations.
	    public IQueryable<TcsArquivoImportar> GetTcsArquivoImportarNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsArquivoImportar> result = 
	            (from entity0 in this.DbContext.TCS_ARQUIVO
	            
	            	
	            select new TcsArquivoImportar()		
	            {
	            
                CaminhoArquivo = entity0.CAMINHO_ARQUIVO
                , CodArquivo = entity0.COD_ARQUIVO
                , DescArquivo = entity0.DESC_ARQUIVO
                , IdArquivo = entity0.ID_ARQUIVO
                , IdArquivoFk = entity0.ID_ARQUIVO
                , LxTipoArquivo = entity0.LX_TIPO_ARQUIVO
                , LxTipoArquivoName = ((entity0.LX_TIPO_ARQUIVO) == "E" ? "Excel" : ((entity0.LX_TIPO_ARQUIVO) == "T" ? "Text" : ((entity0.LX_TIPO_ARQUIVO) == "G" ? "Todos" : ((entity0.LX_TIPO_ARQUIVO) == "X" ? "XML" : ""))))
                , NomeArquivo = entity0.NOME_ARQUIVO
                , Selecao = false
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsArquivoLogNoAssociations.
	    public IQueryable<TcsArquivoLog> GetTcsArquivoLogNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsArquivoLog> result = 
	            (from entity0 in this.DbContext.TCS_ARQUIVO_LOG
	            
	            	
	            select new TcsArquivoLog()		
	            {
	            
                DataLog = entity0.DATA_LOG
                , DescLog = entity0.DESC_LOG
                , IdArquivoFk = entity0.ID_ARQUIVO_FK
                , IdArquivoLog = entity0.ID_ARQUIVO_LOG
                , LxTipoLog = entity0.LX_TIPO_LOG
                , LxTipoLogName = ((entity0.LX_TIPO_LOG) == 2 ? "Geração de Arquivo" : ((entity0.LX_TIPO_LOG) == 3 ? "Importação de Layout" : ((entity0.LX_TIPO_LOG) == 1 ? "Leitura de Arquivo" : "")))
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	result.Add("TcsArquivoImportar|IdArquivoFk");
	    	result.Add("TcsArquivoImportar|TCS_ARQUIVO.ID_ARQUIVO");
	    	result.Add("TcsArquivoImportar|Selecao");
	    	result.Add("TcsArquivoImportar|false");
	    	//Add filtering disabled property for TCS_ARQUIVO
	    	string[] bmDisabledTcsArquivoImportarList = this.GetEDM().GetFilteringDisabledList("TCS_ARQUIVO");
	    	if (bmDisabledTcsArquivoImportarList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsArquivoImportarList.Contains("TCS_ARQUIVO.CAMINHO_ARQUIVO"))
	    		{
	    			result.Add("TcsArquivoImportar|CaminhoArquivo");
	    			result.Add("TcsArquivoImportar|TCS_ARQUIVO.CAMINHO_ARQUIVO");
	    		}
	
	    		if (bmDisabledTcsArquivoImportarList.Contains("TCS_ARQUIVO.COD_ARQUIVO"))
	    		{
	    			result.Add("TcsArquivoImportar|CodArquivo");
	    			result.Add("TcsArquivoImportar|TCS_ARQUIVO.COD_ARQUIVO");
	    		}
	
	    		if (bmDisabledTcsArquivoImportarList.Contains("TCS_ARQUIVO.DESC_ARQUIVO"))
	    		{
	    			result.Add("TcsArquivoImportar|DescArquivo");
	    			result.Add("TcsArquivoImportar|TCS_ARQUIVO.DESC_ARQUIVO");
	    		}
	
	    		if (bmDisabledTcsArquivoImportarList.Contains("TCS_ARQUIVO.ID_ARQUIVO"))
	    		{
	    			result.Add("TcsArquivoImportar|IdArquivo");
	    			result.Add("TcsArquivoImportar|TCS_ARQUIVO.ID_ARQUIVO");
	    		}
	
	    		if (bmDisabledTcsArquivoImportarList.Contains("TCS_ARQUIVO.LX_TIPO_ARQUIVO"))
	    		{
	    			result.Add("TcsArquivoImportar|LxTipoArquivo");
	    			result.Add("TcsArquivoImportar|TCS_ARQUIVO.LX_TIPO_ARQUIVO");
	    		}
	
	    		if (bmDisabledTcsArquivoImportarList.Contains("TCS_ARQUIVO.NOME_ARQUIVO"))
	    		{
	    			result.Add("TcsArquivoImportar|NomeArquivo");
	    			result.Add("TcsArquivoImportar|TCS_ARQUIVO.NOME_ARQUIVO");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_ARQUIVO_LOG
	    	string[] bmDisabledTcsArquivoLogList = this.GetEDM().GetFilteringDisabledList("TCS_ARQUIVO_LOG");
	    	if (bmDisabledTcsArquivoLogList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsArquivoLogList.Contains("TCS_ARQUIVO_LOG.DATA_LOG"))
	    		{
	    			result.Add("TcsArquivoLog|DataLog");
	    			result.Add("TcsArquivoLog|TCS_ARQUIVO_LOG.DATA_LOG");
	    		}
	
	    		if (bmDisabledTcsArquivoLogList.Contains("TCS_ARQUIVO_LOG.DESC_LOG"))
	    		{
	    			result.Add("TcsArquivoLog|DescLog");
	    			result.Add("TcsArquivoLog|TCS_ARQUIVO_LOG.DESC_LOG");
	    		}
	
	    		if (bmDisabledTcsArquivoLogList.Contains("TCS_ARQUIVO_LOG.ID_ARQUIVO_FK"))
	    		{
	    			result.Add("TcsArquivoLog|IdArquivoFk");
	    			result.Add("TcsArquivoLog|TCS_ARQUIVO_LOG.ID_ARQUIVO_FK");
	    		}
	
	    		if (bmDisabledTcsArquivoLogList.Contains("TCS_ARQUIVO_LOG.ID_ARQUIVO_LOG"))
	    		{
	    			result.Add("TcsArquivoLog|IdArquivoLog");
	    			result.Add("TcsArquivoLog|TCS_ARQUIVO_LOG.ID_ARQUIVO_LOG");
	    		}
	
	    		if (bmDisabledTcsArquivoLogList.Contains("TCS_ARQUIVO_LOG.LX_TIPO_LOG"))
	    		{
	    			result.Add("TcsArquivoLog|LxTipoLog");
	    			result.Add("TcsArquivoLog|TCS_ARQUIVO_LOG.LX_TIPO_LOG");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_ARQUIVO
	    	string[] bmDisabledTcsArquivoList = this.GetEDM().GetFilteringDisabledList("TCS_ARQUIVO");
	    	if (bmDisabledTcsArquivoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsArquivoList.Contains("TCS_ARQUIVO.ID_ARQUIVO"))
	    		{
	    			result.Add("TcsArquivo|IdArquivo");
	    			result.Add("TcsArquivo|TCS_ARQUIVO.ID_ARQUIVO");
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
				
	    [Query(HasSideEffects = false)]
	    //Get TcsArquivo By EntitySearchId.
	    public IQueryable<TcsArquivo> GetTcsArquivoByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsArquivoByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get TcsArquivoImportar By EntitySearchId.
	    public IQueryable<TcsArquivoImportar> GetTcsArquivoImportarByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsArquivoImportarByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get TcsArquivoLog By EntitySearchId.
	    public IQueryable<TcsArquivoLog> GetTcsArquivoLogByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsArquivoLogByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get TcsArquivo By EntitySearchId.
	    public IQueryable<TcsArquivo> GetTcsArquivoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsArquivoByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get TcsArquivoImportar By EntitySearchId.
	    public IQueryable<TcsArquivoImportar> GetTcsArquivoImportarByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsArquivoImportarByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get TcsArquivoLog By EntitySearchId.
	    public IQueryable<TcsArquivoLog> GetTcsArquivoLogByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsArquivoLogByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get TcsArquivo By Example.
	    [Ignore]
	    public IQueryable<TcsArquivo> GetTcsArquivoByExample(TcsArquivo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsArquivoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsArquivoImportar By Example.
	    [Ignore]
	    public IQueryable<TcsArquivoImportar> GetTcsArquivoImportarByExample(TcsArquivoImportar entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsArquivoImportarByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsArquivoLog By Example.
	    [Ignore]
	    public IQueryable<TcsArquivoLog> GetTcsArquivoLogByExample(TcsArquivoLog entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsArquivoLogByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsArquivo By Example.
	    [Ignore]
	    public IQueryable<TcsArquivo> GetTcsArquivoByExampleNoAssociations(TcsArquivo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsArquivoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsArquivoImportar By Example.
	    [Ignore]
	    public IQueryable<TcsArquivoImportar> GetTcsArquivoImportarByExampleNoAssociations(TcsArquivoImportar entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsArquivoImportarByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsArquivoLog By Example.
	    [Ignore]
	    public IQueryable<TcsArquivoLog> GetTcsArquivoLogByExampleNoAssociations(TcsArquivoLog entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsArquivoLogByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public TcsArquivoImportar GetTcsArquivoImportarByKey(Int32 idArquivo)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsArquivoImportar");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdArquivo"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idArquivo));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsArquivoImportarByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsArquivoLog GetTcsArquivoLogByKey(Int32 idArquivoLog)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsArquivoLog");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdArquivoLog"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idArquivoLog));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsArquivoLogByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsArquivo GetTcsArquivoByKey(Int32 idArquivo)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsArquivo");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdArquivo"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idArquivo));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsArquivoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get TcsArquivoByEntitySearch.
	    public IQueryable<TcsArquivo> GetTcsArquivoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsArquivo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

		

	        IQueryable<TcsArquivo> result = 
	            (from entity0 in TcsArquivo.OnSearchingReplacement(this.DbContext, dynQuery, parameters, entitySearchList) select entity0);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsArquivoImportarByEntitySearch.
	    public IQueryable<TcsArquivoImportar> GetTcsArquivoImportarByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsArquivoImportar));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

		
	
	        
		
	        
	
	        IQueryable<TcsArquivoImportar> result = 
	            (from entity0 in this.DbContext.TCS_ARQUIVO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsArquivoImportar()		
	            {
	            
                CaminhoArquivo = entity0.CAMINHO_ARQUIVO
                , CodArquivo = entity0.COD_ARQUIVO
                , DescArquivo = entity0.DESC_ARQUIVO
                , IdArquivo = entity0.ID_ARQUIVO
                , IdArquivoFk = entity0.ID_ARQUIVO
                , LxTipoArquivo = entity0.LX_TIPO_ARQUIVO
                , LxTipoArquivoName = ((entity0.LX_TIPO_ARQUIVO) == "E" ? "Excel" : ((entity0.LX_TIPO_ARQUIVO) == "T" ? "Text" : ((entity0.LX_TIPO_ARQUIVO) == "G" ? "Todos" : ((entity0.LX_TIPO_ARQUIVO) == "X" ? "XML" : ""))))
                , NomeArquivo = entity0.NOME_ARQUIVO
                , Selecao = false
			
                ,TcsArquivoLogList = 
	                        (from entity1 in entity0.TCS_ARQUIVO_LOG_LISTA
	                        
	                        	
	                        select new TcsArquivoLog()
	                        {
	                        
                                DataLog = entity1.DATA_LOG
                                , DescLog = entity1.DESC_LOG
                                , IdArquivoFk = entity1.ID_ARQUIVO_FK
                                , IdArquivoLog = entity1.ID_ARQUIVO_LOG
                                , LxTipoLog = entity1.LX_TIPO_LOG
                                , LxTipoLogName = ((entity1.LX_TIPO_LOG) == 2 ? "Geração de Arquivo" : ((entity1.LX_TIPO_LOG) == 3 ? "Importação de Layout" : ((entity1.LX_TIPO_LOG) == 1 ? "Leitura de Arquivo" : "")))
		
	                        }
	                        )
		
	            }
	            );
	
	        SetTcsArquivoImportarBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsArquivoLogByEntitySearch.
	    public IQueryable<TcsArquivoLog> GetTcsArquivoLogByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsArquivoLog));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

		
	
	        
		
	        
	
	        IQueryable<TcsArquivoLog> result = 
	            (from entity0 in this.DbContext.TCS_ARQUIVO_LOG.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsArquivoLog()		
	            {
	            
                DataLog = entity0.DATA_LOG
                , DescLog = entity0.DESC_LOG
                , IdArquivoFk = entity0.ID_ARQUIVO_FK
                , IdArquivoLog = entity0.ID_ARQUIVO_LOG
                , LxTipoLog = entity0.LX_TIPO_LOG
                , LxTipoLogName = ((entity0.LX_TIPO_LOG) == 2 ? "Geração de Arquivo" : ((entity0.LX_TIPO_LOG) == 3 ? "Importação de Layout" : ((entity0.LX_TIPO_LOG) == 1 ? "Leitura de Arquivo" : "")))
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsArquivoByEntitySearchNoAssociations.
	    public IQueryable<TcsArquivo> GetTcsArquivoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsArquivo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

		

	        IQueryable<TcsArquivo> result = 
	            (from entity0 in TcsArquivo.OnSearchingReplacement(this.DbContext, dynQuery, parameters, entitySearchList) select entity0);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsArquivoImportarByEntitySearchNoAssociations.
	    public IQueryable<TcsArquivoImportar> GetTcsArquivoImportarByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsArquivoImportar));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

		
	
	        
		
	        
	
	        IQueryable<TcsArquivoImportar> result = 
	            (from entity0 in this.DbContext.TCS_ARQUIVO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsArquivoImportar()		
	            {
	            
                CaminhoArquivo = entity0.CAMINHO_ARQUIVO
                , CodArquivo = entity0.COD_ARQUIVO
                , DescArquivo = entity0.DESC_ARQUIVO
                , IdArquivo = entity0.ID_ARQUIVO
                , IdArquivoFk = entity0.ID_ARQUIVO
                , LxTipoArquivo = entity0.LX_TIPO_ARQUIVO
                , LxTipoArquivoName = ((entity0.LX_TIPO_ARQUIVO) == "E" ? "Excel" : ((entity0.LX_TIPO_ARQUIVO) == "T" ? "Text" : ((entity0.LX_TIPO_ARQUIVO) == "G" ? "Todos" : ((entity0.LX_TIPO_ARQUIVO) == "X" ? "XML" : ""))))
                , NomeArquivo = entity0.NOME_ARQUIVO
                , Selecao = false
		
	            }
	            );
	
	        SetTcsArquivoImportarBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsArquivoLogByEntitySearchNoAssociations.
	    public IQueryable<TcsArquivoLog> GetTcsArquivoLogByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsArquivoLog));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

		
	
	        
		
	        
	
	        IQueryable<TcsArquivoLog> result = 
	            (from entity0 in this.DbContext.TCS_ARQUIVO_LOG.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsArquivoLog()		
	            {
	            
                DataLog = entity0.DATA_LOG
                , DescLog = entity0.DESC_LOG
                , IdArquivoFk = entity0.ID_ARQUIVO_FK
                , IdArquivoLog = entity0.ID_ARQUIVO_LOG
                , LxTipoLog = entity0.LX_TIPO_LOG
                , LxTipoLogName = ((entity0.LX_TIPO_LOG) == 2 ? "Geração de Arquivo" : ((entity0.LX_TIPO_LOG) == 3 ? "Importação de Layout" : ((entity0.LX_TIPO_LOG) == 1 ? "Leitura de Arquivo" : "")))
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
	
	    [Ignore()]
	    private void SetTcsArquivoImportarBusinessFilter(ref IQueryable<TcsArquivoImportar> query, List<EntitySearch> entitySearchList)
	    {
	    		int idxElement;
	    		string operatorValue;
	    		object value;
	    		//Get query by functions
	    		if (entitySearchList.Count > 0)
	    		{
	    			foreach (EntitySearch search in entitySearchList.Where(e => e.EntityName == "TcsArquivoImportar"))
	    			{

	
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "IdArquivoFk" || e.Value.ToString() == "TCS_ARQUIVO.ID_ARQUIVO")))
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
	    										Int32 tmpIdArquivoFk1 = (Int32)value;
	    										query = from r in query where r.IdArquivoFk == tmpIdArquivoFk1 select r;
	    										break;
	    									case "!=":
	    										Int32 tmpIdArquivoFk2 = (Int32)value;
	    										query = from r in query where r.IdArquivoFk != tmpIdArquivoFk2 select r;
	    										break;

	
	    									case "<":
	    										Int32 tmpIdArquivoFk3 = (Int32)value;
	    										query = from r in query where r.IdArquivoFk < tmpIdArquivoFk3 select r;
	    										break;
	    									case "<=":
	    										Int32 tmpIdArquivoFk4 = (Int32)value;
	    										query = from r in query where r.IdArquivoFk <= tmpIdArquivoFk4 select r;
	    										break;
	    									case ">":
	    										Int32 tmpIdArquivoFk5 = (Int32)value;
	    										query = from r in query where r.IdArquivoFk > tmpIdArquivoFk5 select r;
	    										break;
	    									case ">=":
	    										Int32 tmpIdArquivoFk6 = (Int32)value;
	    										query = from r in query where r.IdArquivoFk >= tmpIdArquivoFk6 select r;
	    										break;	

	
	    									default:
	    										break;
	    								}                                
	    							}
	    						}
        					} 

    
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "Selecao" || e.Value.ToString() == "false")))
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
	    										Boolean tmpSelecao1 = (Boolean)value;
	    										query = from r in query where r.Selecao == tmpSelecao1 select r;
	    										break;
	    									case "!=":
	    										Boolean tmpSelecao2 = (Boolean)value;
	    										query = from r in query where r.Selecao != tmpSelecao2 select r;
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
	    //Get PagedTcsArquivo.
	    public IQueryable<TcsArquivo> GetPagedTcsArquivo(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsArquivo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

		

	        IQueryable<TcsArquivo> result = 
	            (from entity0 in TcsArquivo.OnSearchingReplacement(this.DbContext, dynQuery, parameters, entitySearchList) select entity0);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsArquivoImportar.
	    public IQueryable<TcsArquivoImportar> GetPagedTcsArquivoImportar(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsArquivoImportar));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

		
	
	        
		
	        
	
	        IQueryable<TcsArquivoImportar> result = 
	            (from entity0 in this.DbContext.TCS_ARQUIVO.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_ARQUIVO ascending
	            
	            	
	            select new TcsArquivoImportar()		
	            {
	            
                CaminhoArquivo = entity0.CAMINHO_ARQUIVO
                , CodArquivo = entity0.COD_ARQUIVO
                , DescArquivo = entity0.DESC_ARQUIVO
                , IdArquivo = entity0.ID_ARQUIVO
                , IdArquivoFk = entity0.ID_ARQUIVO
                , LxTipoArquivo = entity0.LX_TIPO_ARQUIVO
                , LxTipoArquivoName = ((entity0.LX_TIPO_ARQUIVO) == "E" ? "Excel" : ((entity0.LX_TIPO_ARQUIVO) == "T" ? "Text" : ((entity0.LX_TIPO_ARQUIVO) == "G" ? "Todos" : ((entity0.LX_TIPO_ARQUIVO) == "X" ? "XML" : ""))))
                , NomeArquivo = entity0.NOME_ARQUIVO
                , Selecao = false
		
	            }
	            ).Skip(skip).Take(take);
	
	        SetTcsArquivoImportarBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsArquivoLog.
	    public IQueryable<TcsArquivoLog> GetPagedTcsArquivoLog(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsArquivoLog));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

		
	
	        
		
	        
	
	        IQueryable<TcsArquivoLog> result = 
	            (from entity0 in this.DbContext.TCS_ARQUIVO_LOG.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_ARQUIVO_LOG ascending
	            
	            	
	            select new TcsArquivoLog()		
	            {
	            
                DataLog = entity0.DATA_LOG
                , DescLog = entity0.DESC_LOG
                , IdArquivoFk = entity0.ID_ARQUIVO_FK
                , IdArquivoLog = entity0.ID_ARQUIVO_LOG
                , LxTipoLog = entity0.LX_TIPO_LOG
                , LxTipoLogName = ((entity0.LX_TIPO_LOG) == 2 ? "Geração de Arquivo" : ((entity0.LX_TIPO_LOG) == 3 ? "Importação de Layout" : ((entity0.LX_TIPO_LOG) == 1 ? "Leitura de Arquivo" : "")))
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsArquivoCounting(string serializedEntitySearch)
	    {	
		 	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsArquivo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_ARQUIVO.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsArquivoImportarCounting(string serializedEntitySearch)
	    {	
		 	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsArquivoImportar));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_ARQUIVO.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsArquivoLogCounting(string serializedEntitySearch)
	    {	
		 	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsArquivoLog));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_ARQUIVO_LOG.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    
	    [Update()]	
	    //Update TcsArquivo.
	    public void UpdateTcsArquivo(TcsArquivo entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsArquivo.
	    public void InsertTcsArquivo(TcsArquivo entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsArquivo.
	    public void DeleteTcsArquivo(TcsArquivo entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsArquivoImportar.
	    public void UpdateTcsArquivoImportar(TcsArquivoImportar entity)
	    {



	
	        if (entity.TcsArquivo.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsArquivo) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsArquivo); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsArquivoImportar.
	    public void InsertTcsArquivoImportar(TcsArquivoImportar entity)
	    {



	
	        if (entity.TcsArquivo.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsArquivo) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsArquivo);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsArquivoImportar.
	    public void DeleteTcsArquivoImportar(TcsArquivoImportar entity)
	    {



	
	        if (entity.TcsArquivo.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsArquivo) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsArquivo);
	            

	
	        }

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsArquivoLog.
	    public void UpdateTcsArquivoLog(TcsArquivoLog entity)
	    {



	
	        if (entity.TcsArquivoImportar.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsArquivoImportar) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsArquivoImportar); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsArquivoLog.
	    public void InsertTcsArquivoLog(TcsArquivoLog entity)
	    {



	
	        if (entity.TcsArquivoImportar.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsArquivoImportar) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsArquivoImportar);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsArquivoLog.
	    public void DeleteTcsArquivoLog(TcsArquivoLog entity)
	    {



	
	        if (entity.TcsArquivoImportar.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsArquivoImportar) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsArquivoImportar);
	            

	
	        }

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}