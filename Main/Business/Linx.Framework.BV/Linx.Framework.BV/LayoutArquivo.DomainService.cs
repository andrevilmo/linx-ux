					
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

namespace Linx.Framework.BV.LayoutArquivo
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_ARQUIVO.ID_ARQUIVO", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsArquivo,TcsArquivo.TcsArquivoItem,TcsArquivoItem.TcsArquivoItemCampo,TcsArquivo.TcsArquivoLog,TcsArquivo.TcsArquivoGrupoVinculo];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdArquivo];ReadOnly[false];Entities[TCS_ARQUIVO:IdArquivo];SubQueryInfo[];EdmEntityName[TCS_ARQUIVO];EntityRelations[];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsArquivo")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.LayoutArquivo.TcsArquivo")]
	public partial class TcsArquivo : Linx.Data.Entity
	{

	
		
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.TcsArquivoItemList != null && this.TcsArquivoItemList.Count() > 0)
	      {
	         foreach (var entity in this.TcsArquivoItemList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      if (this.TcsArquivoLogList != null && this.TcsArquivoLogList.Count() > 0)
	      {
	         foreach (var entity in this.TcsArquivoLogList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      if (this.TcsArquivoGrupoVinculoList != null && this.TcsArquivoGrupoVinculoList.Count() > 0)
	      {
	         foreach (var entity in this.TcsArquivoGrupoVinculoList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      this.TcsArquivoItemList = null;
	      this.TcsArquivoLogList = null;
	      this.TcsArquivoGrupoVinculoList = null;
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(LayoutArquivoDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("TcsArquivoItem"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsArquivoItem");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdArquivoFk"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdArquivo));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsArquivoItem and all sub-details
	         if (this.TcsArquivoItemList == null || this.TcsArquivoItemList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsArquivoItemList = context.GetPagedTcsArquivoItem(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsArquivoItemList = (from r in context.GetTcsArquivoItemByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	         foreach(TcsArquivoItem detail in this.TcsArquivoItemList)
	         {
	             detail.FillDetails(context, serializedEntitySearch, jEntitySearch, viewNames, take);
	         }
	      }
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
	      if (viewNames == null || viewNames.Contains("TcsArquivoGrupoVinculo"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsArquivoGrupoVinculo");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdArquivo"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdArquivo));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsArquivoGrupoVinculo and all sub-details
	         if (this.TcsArquivoGrupoVinculoList == null || this.TcsArquivoGrupoVinculoList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsArquivoGrupoVinculoList = context.GetPagedTcsArquivoGrupoVinculo(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsArquivoGrupoVinculoList = (from r in context.GetTcsArquivoGrupoVinculoByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _TcsArquivoItemElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsArquivoItem && ((TcsArquivoItem)e.Entity).TcsArquivo == null && e.Associations == null && e.OriginalAssociations == null && ((TcsArquivoItem)e.Entity).IdArquivoFk == this.IdArquivo).ToList();
 	      if (_TcsArquivoItemElements.Count > 0 && this.TcsArquivoItemList.Count() == 0)
 	      {
 	          this.TcsArquivoItemList = _TcsArquivoItemElements.Select(e => (TcsArquivoItem)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsArquivoItemElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsArquivoItem)detail.Entity).TcsArquivo = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsArquivo", new int[] { masterIndex });
 	              ((TcsArquivoItem)detail.Entity).AdjustHierarchyForSaving(detail, changeSet);
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsArquivoItemList", indexDetails.ToArray());
 	      }
 
 	      var _TcsArquivoLogElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsArquivoLog && ((TcsArquivoLog)e.Entity).TcsArquivo == null && e.Associations == null && e.OriginalAssociations == null && ((TcsArquivoLog)e.Entity).IdArquivoFk == this.IdArquivo).ToList();
 	      if (_TcsArquivoLogElements.Count > 0 && this.TcsArquivoLogList.Count() == 0)
 	      {
 	          this.TcsArquivoLogList = _TcsArquivoLogElements.Select(e => (TcsArquivoLog)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsArquivoLogElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsArquivoLog)detail.Entity).TcsArquivo = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsArquivo", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsArquivoLogList", indexDetails.ToArray());
 	      }
 
 	      var _TcsArquivoGrupoVinculoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsArquivoGrupoVinculo && ((TcsArquivoGrupoVinculo)e.Entity).TcsArquivo == null && e.Associations == null && e.OriginalAssociations == null && ((TcsArquivoGrupoVinculo)e.Entity).IdArquivo == this.IdArquivo).ToList();
 	      if (_TcsArquivoGrupoVinculoElements.Count > 0 && this.TcsArquivoGrupoVinculoList.Count() == 0)
 	      {
 	          this.TcsArquivoGrupoVinculoList = _TcsArquivoGrupoVinculoElements.Select(e => (TcsArquivoGrupoVinculo)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsArquivoGrupoVinculoElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsArquivoGrupoVinculo)detail.Entity).TcsArquivo = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsArquivo", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsArquivoGrupoVinculoList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For ArquivoDll
	    partial void OnArquivoDllChanging(System.String value);
	    partial void OnArquivoDllChanged();

	    private System.String _ArquivoDll;

	    [DataMember(Name = "ArquivoDll", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Arquivo DLL", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO.ARQUIVO_DLL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.ARQUIVO_DLL")]
	    public System.String ArquivoDll
	    {
	    	    get
	    	    {
	    	          return _ArquivoDll;
	    	    }
	    	    set
	    	    {
	    	          if (this._ArquivoDll != value)
	    	          {
	    	              this.ValidateProperty("ArquivoDll", value);
	    	              this.OnArquivoDllChanging(value);
	    	              this.RaiseDataMemberChanging("ArquivoDll");
	    	              this._ArquivoDll = value;
	    	              this.RaiseDataMemberChanged("ArquivoDll");
	    	              this.OnArquivoDllChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For CaminhoArquivo
	    partial void OnCaminhoArquivoChanging(System.String value);
	    partial void OnCaminhoArquivoChanged();

	    private System.String _CaminhoArquivo;

	    [DataMember(IsRequired = true, Name = "CaminhoArquivo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Caminho do Arquivo", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO.CAMINHO_ARQUIVO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For Classe
	    partial void OnClasseChanging(System.String value);
	    partial void OnClasseChanged();

	    private System.String _Classe;

	    [DataMember(Name = "Classe", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Classe", Description="", Order = 14, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(100)]
	    [FunctionalPoint("Precision[100:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO.CLASSE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.CLASSE")]
	    public System.String Classe
	    {
	    	    get
	    	    {
	    	          return _Classe;
	    	    }
	    	    set
	    	    {
	    	          if (this._Classe != value)
	    	          {
	    	              this.ValidateProperty("Classe", value);
	    	              this.OnClasseChanging(value);
	    	              this.RaiseDataMemberChanging("Classe");
	    	              this._Classe = value;
	    	              this.RaiseDataMemberChanged("Classe");
	    	              this.OnClasseChanged();
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
	    [Display(Name = "Código", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO.COD_ARQUIVO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For Delimitador
	    partial void OnDelimitadorChanging(System.String value);
	    partial void OnDelimitadorChanged();

	    private System.String _Delimitador;

	    [DataMember(Name = "Delimitador", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Delimitador", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(1)]
	    [FunctionalPoint("Precision[1:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO.DELIMITADOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.DELIMITADOR")]
	    public System.String Delimitador
	    {
	    	    get
	    	    {
	    	          return _Delimitador;
	    	    }
	    	    set
	    	    {
	    	          if (this._Delimitador != value)
	    	          {
	    	              this.ValidateProperty("Delimitador", value);
	    	              this.OnDelimitadorChanging(value);
	    	              this.RaiseDataMemberChanging("Delimitador");
	    	              this._Delimitador = value;
	    	              this.RaiseDataMemberChanged("Delimitador");
	    	              this.OnDelimitadorChanged();
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
	    [Display(Name = "Descrição", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(90)]
	    [FunctionalPoint("Precision[90:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO.DESC_ARQUIVO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For DetalheArquivo
	    partial void OnDetalheArquivoChanging(System.String value);
	    partial void OnDetalheArquivoChanged();

	    private System.String _DetalheArquivo;

	    [DataMember(Name = "DetalheArquivo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Detalhe Arquivo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(1000)]
	    [FunctionalPoint("Precision[1000:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO.DETALHE_ARQUIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.DETALHE_ARQUIVO")]
	    public System.String DetalheArquivo
	    {
	    	    get
	    	    {
	    	          return _DetalheArquivo;
	    	    }
	    	    set
	    	    {
	    	          if (this._DetalheArquivo != value)
	    	          {
	    	              this.ValidateProperty("DetalheArquivo", value);
	    	              this.OnDetalheArquivoChanging(value);
	    	              this.RaiseDataMemberChanging("DetalheArquivo");
	    	              this._DetalheArquivo = value;
	    	              this.RaiseDataMemberChanged("DetalheArquivo");
	    	              this.OnDetalheArquivoChanged();
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
	    [Display(Name = "ID", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO.ID_ARQUIVO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For Inativo
	    partial void OnInativoChanging(Boolean value);
	    partial void OnInativoChanged();

	    private Boolean _Inativo;

	    [DataMember(IsRequired = true, Name = "Inativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inativo", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.INATIVO")]
	    public Boolean Inativo
	    {
	    	    get
	    	    {
	    	          return _Inativo;
	    	    }
	    	    set
	    	    {
	    	          if (this._Inativo != value)
	    	          {
	    	              this.ValidateProperty("Inativo", value);
	    	              this.OnInativoChanging(value);
	    	              this.RaiseDataMemberChanging("Inativo");
	    	              this._Inativo = value;
	    	              this.RaiseDataMemberChanged("Inativo");
	    	              this.OnInativoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxFormatoData
	    partial void OnLxFormatoDataChanging(System.String value);
	    partial void OnLxFormatoDataChanged();

	    private System.String _LxFormatoData;

	    [DataMember(Name = "LxFormatoData", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Formato de Data", Description="", Order = 16, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(1)]
	    [FunctionalPoint("Precision[1:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[FormatoData];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO.LX_FORMATO_DATA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.LX_FORMATO_DATA")]
	    public System.String LxFormatoData
	    {
	    	    get
	    	    {
	    	          return _LxFormatoData;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxFormatoData != value)
	    	          {
	    	              this.ValidateProperty("LxFormatoData", value);
	    	              this.OnLxFormatoDataChanging(value);
	    	              this.RaiseDataMemberChanging("LxFormatoData");
	    	              this._LxFormatoData = value;
	    	              this.RaiseDataMemberChanged("LxFormatoData");
	    	              this.OnLxFormatoDataChanged();
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
	    [Display(Name = "Tipo do Arquivo", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(1)]
	    [FunctionalPoint("Precision[1:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoArquivo];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO.LX_TIPO_ARQUIVO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For Metodo
	    partial void OnMetodoChanging(System.String value);
	    partial void OnMetodoChanged();

	    private System.String _Metodo;

	    [DataMember(Name = "Metodo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Método", Description="", Order = 15, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO.METODO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.METODO")]
	    public System.String Metodo
	    {
	    	    get
	    	    {
	    	          return _Metodo;
	    	    }
	    	    set
	    	    {
	    	          if (this._Metodo != value)
	    	          {
	    	              this.ValidateProperty("Metodo", value);
	    	              this.OnMetodoChanging(value);
	    	              this.RaiseDataMemberChanging("Metodo");
	    	              this._Metodo = value;
	    	              this.RaiseDataMemberChanged("Metodo");
	    	              this.OnMetodoChanged();
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
	    [Display(Name = "Nome do Arquivo", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO.NOME_ARQUIVO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For TagMestre
	    partial void OnTagMestreChanging(System.String value);
	    partial void OnTagMestreChanged();

	    private System.String _TagMestre;

	    [DataMember(IsRequired = true, Name = "TagMestre", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tag Mestre", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO.TAG_MESTRE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.TAG_MESTRE")]
	    public System.String TagMestre
	    {
	    	    get
	    	    {
	    	          return _TagMestre;
	    	    }
	    	    set
	    	    {
	    	          if (this._TagMestre != value)
	    	          {
	    	              this.ValidateProperty("TagMestre", value);
	    	              this.OnTagMestreChanging(value);
	    	              this.RaiseDataMemberChanging("TagMestre");
	    	              this._TagMestre = value;
	    	              this.RaiseDataMemberChanged("TagMestre");
	    	              this.OnTagMestreChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Xmlns
	    partial void OnXmlnsChanging(System.String value);
	    partial void OnXmlnsChanged();

	    private System.String _Xmlns;

	    [DataMember(Name = "Xmlns", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Namespace do XSD", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO.XMLNS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.XMLNS")]
	    public System.String Xmlns
	    {
	    	    get
	    	    {
	    	          return _Xmlns;
	    	    }
	    	    set
	    	    {
	    	          if (this._Xmlns != value)
	    	          {
	    	              this.ValidateProperty("Xmlns", value);
	    	              this.OnXmlnsChanging(value);
	    	              this.RaiseDataMemberChanging("Xmlns");
	    	              this._Xmlns = value;
	    	              this.RaiseDataMemberChanged("Xmlns");
	    	              this.OnXmlnsChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Xsd
	    partial void OnXsdChanging(System.String value);
	    partial void OnXsdChanged();

	    private System.String _Xsd;

	    [DataMember(Name = "Xsd", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "XSD", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO.XSD];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.XSD")]
	    public System.String Xsd
	    {
	    	    get
	    	    {
	    	          return _Xsd;
	    	    }
	    	    set
	    	    {
	    	          if (this._Xsd != value)
	    	          {
	    	              this.ValidateProperty("Xsd", value);
	    	              this.OnXsdChanging(value);
	    	              this.RaiseDataMemberChanging("Xsd");
	    	              this._Xsd = value;
	    	              this.RaiseDataMemberChanged("Xsd");
	    	              this.OnXsdChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIdArquivo;
	    [DataMember(Name = "TemporaryIdArquivo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID (Tmp)", Description="Temporary Key", Order = 8, AutoGenerateField = false, GroupName="", ResourceType= null)]
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
	 
		
	    private IEnumerable<TcsArquivoGrupoVinculo> _TcsArquivoGrupoVinculoList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsArquivo_TcsArquivoGrupoVinculo", "IdArquivo", "IdArquivo", IsForeignKey=false)]
	    [DataMember(Name = "TcsArquivoGrupoVinculoList", EmitDefaultValue = true)]
	    public IEnumerable<TcsArquivoGrupoVinculo> TcsArquivoGrupoVinculoList
	    {
	        get
	        {
	
	            if (this._TcsArquivoGrupoVinculoList == null)
	            	this._TcsArquivoGrupoVinculoList = new List<TcsArquivoGrupoVinculo>();
	
	            return this._TcsArquivoGrupoVinculoList;
	        }
	        set
	        {
	            if (this._TcsArquivoGrupoVinculoList != value)
	            {
	                this._TcsArquivoGrupoVinculoList = value;
	                this.RaisePropertyChanged("TcsArquivoGrupoVinculoList");
	            }
	        }
	    }	 
		
	    private IEnumerable<TcsArquivoItem> _TcsArquivoItemList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsArquivo_TcsArquivoItem", "IdArquivo", "IdArquivoFk", IsForeignKey=false)]
	    [DataMember(Name = "TcsArquivoItemList", EmitDefaultValue = true)]
	    public IEnumerable<TcsArquivoItem> TcsArquivoItemList
	    {
	        get
	        {
	
	            if (this._TcsArquivoItemList == null)
	            	this._TcsArquivoItemList = new List<TcsArquivoItem>();
	
	            return this._TcsArquivoItemList;
	        }
	        set
	        {
	            if (this._TcsArquivoItemList != value)
	            {
	                this._TcsArquivoItemList = value;
	                this.RaisePropertyChanged("TcsArquivoItemList");
	            }
	        }
	    }	 
		
	    private IEnumerable<TcsArquivoLog> _TcsArquivoLogList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsArquivo_TcsArquivoLog", "IdArquivo", "IdArquivoFk", IsForeignKey=false)]
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

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO.XSD", Source = "Xsd", Target = "XSD", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO", RelationPropertyName = "TCS_ARQUIVO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO.XMLNS", Source = "Xmlns", Target = "XMLNS", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO", RelationPropertyName = "TCS_ARQUIVO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO.CLASSE", Source = "Classe", Target = "CLASSE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO", RelationPropertyName = "TCS_ARQUIVO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO.METODO", Source = "Metodo", Target = "METODO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO", RelationPropertyName = "TCS_ARQUIVO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO.INATIVO", Source = "Inativo", Target = "INATIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO", RelationPropertyName = "TCS_ARQUIVO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO.ID_ARQUIVO", Source = "IdArquivo", Target = "ID_ARQUIVO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO", RelationPropertyName = "TCS_ARQUIVO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO.TAG_MESTRE", Source = "TagMestre", Target = "TAG_MESTRE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO", RelationPropertyName = "TCS_ARQUIVO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO.ARQUIVO_DLL", Source = "ArquivoDll", Target = "ARQUIVO_DLL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO", RelationPropertyName = "TCS_ARQUIVO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO.COD_ARQUIVO", Source = "CodArquivo", Target = "COD_ARQUIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO", RelationPropertyName = "TCS_ARQUIVO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO.DELIMITADOR", Source = "Delimitador", Target = "DELIMITADOR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO", RelationPropertyName = "TCS_ARQUIVO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO.DESC_ARQUIVO", Source = "DescArquivo", Target = "DESC_ARQUIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO", RelationPropertyName = "TCS_ARQUIVO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO.NOME_ARQUIVO", Source = "NomeArquivo", Target = "NOME_ARQUIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO", RelationPropertyName = "TCS_ARQUIVO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO.CAMINHO_ARQUIVO", Source = "CaminhoArquivo", Target = "CAMINHO_ARQUIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO", RelationPropertyName = "TCS_ARQUIVO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO.DETALHE_ARQUIVO", Source = "DetalheArquivo", Target = "DETALHE_ARQUIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO", RelationPropertyName = "TCS_ARQUIVO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO.LX_FORMATO_DATA", Source = "LxFormatoData", Target = "LX_FORMATO_DATA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO", RelationPropertyName = "TCS_ARQUIVO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO.LX_TIPO_ARQUIVO", Source = "LxTipoArquivo", Target = "LX_TIPO_ARQUIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO", RelationPropertyName = "TCS_ARQUIVO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxFormatoDataValues()
	    {
	    	    return Linx.Framework.BV.Domains.FormatoData.GetValues();
	    }
	    private string _lxFormatoDataName;
	    [DataMember(IsRequired = false, Name = "LxFormatoDataName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Formato de Data", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxFormatoDataName
	    {
	    	    get { if (this.LxFormatoData.IsNullOrEmpty()) { _lxFormatoDataName = String.Empty; } else { string key = this.LxFormatoData.ToString(); var dmValues = this.GetLxFormatoDataValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxFormatoDataName) _lxFormatoDataName = domainName; } return _lxFormatoDataName; } set { _lxFormatoDataName = value;  }
	    }
	    public Dictionary<string, string> GetLxTipoArquivoValues()
	    {
	    	    return Linx.Framework.BV.Domains.TipoArquivo.GetValues();
	    }
	    private string _lxTipoArquivoName;
	    [DataMember(IsRequired = false, Name = "LxTipoArquivoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo do Arquivo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
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

		

	[LinxPublicationView(PrimaryKeys="TCS_ARQUIVO_ITEM.ID_ARQUIVO_ITEM", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Elementos];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdArquivoItem];ReadOnly[false];Entities[TCS_ARQUIVO_ITEM:IdArquivoItem|TCS_ARQUIVO_ITEM:IdArquivoItemPai];SubQueryInfo[Select 1 From #ParentAlias#.TCS_ARQUIVO_ITEM_LISTA as #Alias#];EdmEntityName[TCS_ARQUIVO_ITEM];EntityRelations[ARQUIVO_ITEM_PAI(TCS_ARQUIVO_ITEM)#TCS_ARQUIVO(TCS_ARQUIVO)#TCS_ARQUIVO_GRUPO(TCS_ARQUIVO_GRUPO)];EdmParentEntityName[TCS_ARQUIVO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsArquivoItem")]
	[Serializable()]
	public partial class TcsArquivoItem : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(LayoutArquivoDomainService context)
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
	      if (this.TcsArquivoItemCampoList != null && this.TcsArquivoItemCampoList.Count() > 0)
	      {
	         foreach (var entity in this.TcsArquivoItemCampoList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      this.TcsArquivoItemCampoList = null;
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(LayoutArquivoDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("TcsArquivoItemCampo"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsArquivoItemCampo");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdArquivoItemFk"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdArquivoItem));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsArquivoItemCampo and all sub-details
	         if (this.TcsArquivoItemCampoList == null || this.TcsArquivoItemCampoList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsArquivoItemCampoList = context.GetPagedTcsArquivoItemCampo(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsArquivoItemCampoList = (from r in context.GetTcsArquivoItemCampoByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _TcsArquivoItemCampoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsArquivoItemCampo && ((TcsArquivoItemCampo)e.Entity).TcsArquivoItem == null && e.Associations == null && e.OriginalAssociations == null && ((TcsArquivoItemCampo)e.Entity).IdArquivoItemFk == this.IdArquivoItem).ToList();
 	      if (_TcsArquivoItemCampoElements.Count > 0 && this.TcsArquivoItemCampoList.Count() == 0)
 	      {
 	          this.TcsArquivoItemCampoList = _TcsArquivoItemCampoElements.Select(e => (TcsArquivoItemCampo)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsArquivoItemCampoElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsArquivoItemCampo)detail.Entity).TcsArquivoItem = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsArquivoItem", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsArquivoItemCampoList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For IdArquivoFk
	    partial void OnIdArquivoFkChanging(Int32 value);
	    partial void OnIdArquivoFkChanged();

	    private Int32 _IdArquivoFk;

	    [DataMember(IsRequired = true, Name = "IdArquivoFk", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Arquivo Fk", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_ITEM.ID_ARQUIVO_FK];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM.ID_ARQUIVO_FK")]
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
	    //Extensibility Partial Method Definitions For IdArquivoItemPai
	    partial void OnIdArquivoItemPaiChanging(System.Nullable<Int32> value);
	    partial void OnIdArquivoItemPaiChanged();

	    private System.Nullable<Int32> _IdArquivoItemPai;

	    [DataMember(Name = "IdArquivoItemPai", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Elemento Pai", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpArquivoItemPai];LookUpTitle[Seleção de (ID Elemento Pai)];LookUpQuery[executeLookUpArquivoItemPai];LookUpFinalize[finalizeLookUpArquivoItemPai];LookUpDisplayColumns[{\"IdArquivoItemPai\" : \"ID Elemento Pai\", \"TagItemPai\" : \"Elemento Pai\"}];LookUpColumns[{\"IdArquivoItemPai\" : true, \"TagItemPai\" : true}];FilterDataKey[TCS_ARQUIVO_ITEM.ARQUIVO_ITEM_PAI.ID_ARQUIVO_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdArquivoItemPai#true##12:0##ID Elemento Pai#0#true##::LookUpArquivoItemPai##false#false#ARQUIVO_ITEM_PAI#TCS_ARQUIVO_ITEM#Linx.Framework.BV.LayoutArquivo#IQueryable###true#false", EdmKey="TCS_ARQUIVO_ITEM.ARQUIVO_ITEM_PAI.ID_ARQUIVO_ITEM")]
	    public System.Nullable<Int32> IdArquivoItemPai
	    {
	    	    get
	    	    {
	    	          return _IdArquivoItemPai;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdArquivoItemPai != value)
	    	          {
	    	              this.ValidateProperty("IdArquivoItemPai", value);
	    	              this.OnIdArquivoItemPaiChanging(value);
	    	              this.RaiseDataMemberChanging("IdArquivoItemPai");
	    	              this._IdArquivoItemPai = value;
	    	              this.RaiseDataMemberChanged("IdArquivoItemPai");
	    	              this.OnIdArquivoItemPaiChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdArquivoItem
	    partial void OnIdArquivoItemChanging(Int32 value);
	    partial void OnIdArquivoItemChanged();

	    private Int32 _IdArquivoItem;

	    [DataMember(IsRequired = true, Name = "IdArquivoItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Elemento", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_ITEM.ID_ARQUIVO_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM.ID_ARQUIVO_ITEM")]
	    public Int32 IdArquivoItem
	    {
	    	    get
	    	    {
	    	          return _IdArquivoItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdArquivoItem != value)
	    	          {
	    	              this.ValidateProperty("IdArquivoItem", value);
	    	              this.OnIdArquivoItemChanging(value);
	    	              this.RaiseDataMemberChanging("IdArquivoItem");
	    	              this._IdArquivoItem = value;
	    	              this.RaiseDataMemberChanged("IdArquivoItem");
	    	              this.OnIdArquivoItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IndicaNotnull
	    partial void OnIndicaNotnullChanging(Boolean value);
	    partial void OnIndicaNotnullChanged();

	    private Boolean _IndicaNotnull;

	    [DataMember(IsRequired = true, Name = "IndicaNotnull", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Obrigatório", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_ITEM.INDICA_NOTNULL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM.INDICA_NOTNULL")]
	    public Boolean IndicaNotnull
	    {
	    	    get
	    	    {
	    	          return _IndicaNotnull;
	    	    }
	    	    set
	    	    {
	    	          if (this._IndicaNotnull != value)
	    	          {
	    	              this.ValidateProperty("IndicaNotnull", value);
	    	              this.OnIndicaNotnullChanging(value);
	    	              this.RaiseDataMemberChanging("IndicaNotnull");
	    	              this._IndicaNotnull = value;
	    	              this.RaiseDataMemberChanged("IndicaNotnull");
	    	              this.OnIndicaNotnullChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Ordem
	    partial void OnOrdemChanging(Int32 value);
	    partial void OnOrdemChanged();

	    private Int32 _Ordem;

	    [DataMember(IsRequired = true, Name = "Ordem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ordem do Elemento", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_ITEM.ORDEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM.ORDEM")]
	    public Int32 Ordem
	    {
	    	    get
	    	    {
	    	          return _Ordem;
	    	    }
	    	    set
	    	    {
	    	          if (this._Ordem != value)
	    	          {
	    	              this.ValidateProperty("Ordem", value);
	    	              this.OnOrdemChanging(value);
	    	              this.RaiseDataMemberChanging("Ordem");
	    	              this._Ordem = value;
	    	              this.RaiseDataMemberChanged("Ordem");
	    	              this.OnOrdemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For TagItemPai
	    partial void OnTagItemPaiChanging(System.String value);
	    partial void OnTagItemPaiChanged();

	    private System.String _TagItemPai;

	    [DataMember(Name = "TagItemPai", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Elemento Pai", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpArquivoItemPai];LookUpTitle[Seleção de (Elemento Pai)];LookUpQuery[executeLookUpArquivoItemPai];LookUpFinalize[finalizeLookUpArquivoItemPai];LookUpDisplayColumns[{\"IdArquivoItemPai\" : \"ID Elemento Pai\", \"TagItemPai\" : \"Elemento Pai\"}];LookUpColumns[{\"IdArquivoItemPai\" : true, \"TagItemPai\" : true}];FilterDataKey[TCS_ARQUIVO_ITEM.ARQUIVO_ITEM_PAI.TAG_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#TagItemPai#false##40:0##Elemento Pai#1#true##::LookUpArquivoItemPai##false#false#ARQUIVO_ITEM_PAI#TCS_ARQUIVO_ITEM#Linx.Framework.BV.LayoutArquivo#IQueryable###true#false", EdmKey="TCS_ARQUIVO_ITEM.ARQUIVO_ITEM_PAI.TAG_ITEM")]
	    public System.String TagItemPai
	    {
	    	    get
	    	    {
	    	          return _TagItemPai;
	    	    }
	    	    set
	    	    {
	    	          if (this._TagItemPai != value)
	    	          {
	    	              this.ValidateProperty("TagItemPai", value);
	    	              this.OnTagItemPaiChanging(value);
	    	              this.RaiseDataMemberChanging("TagItemPai");
	    	              this._TagItemPai = value;
	    	              this.RaiseDataMemberChanged("TagItemPai");
	    	              this.OnTagItemPaiChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For TagItem
	    partial void OnTagItemChanging(System.String value);
	    partial void OnTagItemChanged();

	    private System.String _TagItem;

	    [DataMember(IsRequired = true, Name = "TagItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Elemento", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_ITEM.TAG_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM.TAG_ITEM")]
	    public System.String TagItem
	    {
	    	    get
	    	    {
	    	          return _TagItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._TagItem != value)
	    	          {
	    	              this.ValidateProperty("TagItem", value);
	    	              this.OnTagItemChanging(value);
	    	              this.RaiseDataMemberChanging("TagItem");
	    	              this._TagItem = value;
	    	              this.RaiseDataMemberChanged("TagItem");
	    	              this.OnTagItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Xmlns
	    partial void OnXmlnsChanging(System.String value);
	    partial void OnXmlnsChanged();

	    private System.String _Xmlns;

	    [DataMember(Name = "Xmlns", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Namespace do XSD", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_ITEM.XMLNS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM.XMLNS")]
	    public System.String Xmlns
	    {
	    	    get
	    	    {
	    	          return _Xmlns;
	    	    }
	    	    set
	    	    {
	    	          if (this._Xmlns != value)
	    	          {
	    	              this.ValidateProperty("Xmlns", value);
	    	              this.OnXmlnsChanging(value);
	    	              this.RaiseDataMemberChanging("Xmlns");
	    	              this._Xmlns = value;
	    	              this.RaiseDataMemberChanged("Xmlns");
	    	              this.OnXmlnsChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIdArquivoItem;
	    [DataMember(Name = "TemporaryIdArquivoItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Elemento (Tmp)", Description="Temporary Key", Order = 4, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdArquivoItem
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdArquivoItem.IsNullOrEmpty())
	    	                this._TemporaryIdArquivoItem = this._IdArquivoItem;
	    	          return this._TemporaryIdArquivoItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdArquivoItem != value)
	    	              this._TemporaryIdArquivoItem = value;
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
	    [Association("FK_TcsArquivo_TcsArquivoItem", "IdArquivoFk", "IdArquivo", IsForeignKey=true)]
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
	 
		
	    private IEnumerable<TcsArquivoItemCampo> _TcsArquivoItemCampoList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsArquivoItem_TcsArquivoItemCampo", "IdArquivoItem", "IdArquivoItemFk", IsForeignKey=false)]
	    [DataMember(Name = "TcsArquivoItemCampoList", EmitDefaultValue = true)]
	    public IEnumerable<TcsArquivoItemCampo> TcsArquivoItemCampoList
	    {
	        get
	        {
	
	            if (this._TcsArquivoItemCampoList == null)
	            	this._TcsArquivoItemCampoList = new List<TcsArquivoItemCampo>();
	
	            return this._TcsArquivoItemCampoList;
	        }
	        set
	        {
	            if (this._TcsArquivoItemCampoList != value)
	            {
	                this._TcsArquivoItemCampoList = value;
	                this.RaisePropertyChanged("TcsArquivoItemCampoList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_ARQUIVO_ITEM").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_ARQUIVO_ITEM), QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_ITEM.ORDEM", Source = "Ordem", Target = "ORDEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM", RelationPropertyName = "TCS_ARQUIVO_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_ITEM.XMLNS", Source = "Xmlns", Target = "XMLNS", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM", RelationPropertyName = "TCS_ARQUIVO_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_ITEM.TAG_ITEM", Source = "TagItem", Target = "TAG_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM", RelationPropertyName = "TCS_ARQUIVO_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_ITEM.ID_ARQUIVO_FK", Source = "IdArquivoFk", Target = "ID_ARQUIVO_FK", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM", RelationPropertyName = "TCS_ARQUIVO_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_ITEM.INDICA_NOTNULL", Source = "IndicaNotnull", Target = "INDICA_NOTNULL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM", RelationPropertyName = "TCS_ARQUIVO_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_ITEM.ID_ARQUIVO_ITEM", Source = "IdArquivoItem", Target = "ID_ARQUIVO_ITEM", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM", RelationPropertyName = "TCS_ARQUIVO_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_ITEM.ARQUIVO_ITEM_PAI.ID_ARQUIVO_ITEM", Source = "IdArquivoItemPai", Target = "ID_ARQUIVO_ITEM", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM", RelationPropertyName = "ARQUIVO_ITEM_PAI" });

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

		

	[LinxPublicationView(PrimaryKeys="TCS_ARQUIVO_ITEM_CAMPO.ID_ARQUIVO_ITEM_CAMPO", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Campos];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdArquivoItemCampo];ReadOnly[false];Entities[TCS_ARQUIVO_ITEM_CAMPO:IdArquivoItemCampo];SubQueryInfo[Select 1 From #ParentAlias#.TCS_ARQUIVO_ITEM_CAMPO_LISTA as #Alias#];EdmEntityName[TCS_ARQUIVO_ITEM_CAMPO];EntityRelations[TCS_ARQUIVO_ITEM(TCS_ARQUIVO_ITEM)#ARQUIVO_ITEM_PAI(TCS_ARQUIVO_ITEM)#TCS_ARQUIVO(TCS_ARQUIVO)];EdmParentEntityName[TCS_ARQUIVO_ITEM];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsArquivoItemCampo")]
	[Serializable()]
	public partial class TcsArquivoItemCampo : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(LayoutArquivoDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsArquivoItem");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdArquivoItem"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdArquivoItemFk));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsArquivoItem
	         this.TcsArquivoItem = (from r in context.GetTcsArquivoItemByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For ChaveIdentificacao
	    partial void OnChaveIdentificacaoChanging(System.String value);
	    partial void OnChaveIdentificacaoChanged();

	    private System.String _ChaveIdentificacao;

	    [DataMember(Name = "ChaveIdentificacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Chave de Identificação", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(25)]
	    [FunctionalPoint("Precision[25:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_ITEM_CAMPO.CHAVE_IDENTIFICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM_CAMPO.CHAVE_IDENTIFICACAO")]
	    public System.String ChaveIdentificacao
	    {
	    	    get
	    	    {
	    	          return _ChaveIdentificacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._ChaveIdentificacao != value)
	    	          {
	    	              this.ValidateProperty("ChaveIdentificacao", value);
	    	              this.OnChaveIdentificacaoChanging(value);
	    	              this.RaiseDataMemberChanging("ChaveIdentificacao");
	    	              this._ChaveIdentificacao = value;
	    	              this.RaiseDataMemberChanged("ChaveIdentificacao");
	    	              this.OnChaveIdentificacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Decimais
	    partial void OnDecimaisChanging(Byte value);
	    partial void OnDecimaisChanged();

	    private Byte _Decimais;

	    [DataMember(IsRequired = true, Name = "Decimais", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Casas Decimais", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_ITEM_CAMPO.DECIMAIS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM_CAMPO.DECIMAIS")]
	    public Byte Decimais
	    {
	    	    get
	    	    {
	    	          return _Decimais;
	    	    }
	    	    set
	    	    {
	    	          if (this._Decimais != value)
	    	          {
	    	              this.ValidateProperty("Decimais", value);
	    	              this.OnDecimaisChanging(value);
	    	              this.RaiseDataMemberChanging("Decimais");
	    	              this._Decimais = value;
	    	              this.RaiseDataMemberChanged("Decimais");
	    	              this.OnDecimaisChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdArquivoItemCampo
	    partial void OnIdArquivoItemCampoChanging(Int32 value);
	    partial void OnIdArquivoItemCampoChanged();

	    private Int32 _IdArquivoItemCampo;

	    [DataMember(IsRequired = true, Name = "IdArquivoItemCampo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Campo", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_ITEM_CAMPO.ID_ARQUIVO_ITEM_CAMPO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM_CAMPO.ID_ARQUIVO_ITEM_CAMPO")]
	    public Int32 IdArquivoItemCampo
	    {
	    	    get
	    	    {
	    	          return _IdArquivoItemCampo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdArquivoItemCampo != value)
	    	          {
	    	              this.ValidateProperty("IdArquivoItemCampo", value);
	    	              this.OnIdArquivoItemCampoChanging(value);
	    	              this.RaiseDataMemberChanging("IdArquivoItemCampo");
	    	              this._IdArquivoItemCampo = value;
	    	              this.RaiseDataMemberChanged("IdArquivoItemCampo");
	    	              this.OnIdArquivoItemCampoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdArquivoItemFk
	    partial void OnIdArquivoItemFkChanging(Int32 value);
	    partial void OnIdArquivoItemFkChanged();

	    private Int32 _IdArquivoItemFk;

	    [DataMember(IsRequired = true, Name = "IdArquivoItemFk", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Arquivo Item Fk", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_ITEM_CAMPO.ID_ARQUIVO_ITEM_FK];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM_CAMPO.ID_ARQUIVO_ITEM_FK")]
	    public Int32 IdArquivoItemFk
	    {
	    	    get
	    	    {
	    	          return _IdArquivoItemFk;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdArquivoItemFk != value)
	    	          {
	    	              this.ValidateProperty("IdArquivoItemFk", value);
	    	              this.OnIdArquivoItemFkChanging(value);
	    	              this.RaiseDataMemberChanging("IdArquivoItemFk");
	    	              this._IdArquivoItemFk = value;
	    	              this.RaiseDataMemberChanged("IdArquivoItemFk");
	    	              this.OnIdArquivoItemFkChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IndicaNotnull
	    partial void OnIndicaNotnullChanging(Boolean value);
	    partial void OnIndicaNotnullChanged();

	    private Boolean _IndicaNotnull;

	    [DataMember(IsRequired = true, Name = "IndicaNotnull", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Obrigatório", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_ITEM_CAMPO.INDICA_NOTNULL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM_CAMPO.INDICA_NOTNULL")]
	    public Boolean IndicaNotnull
	    {
	    	    get
	    	    {
	    	          return _IndicaNotnull;
	    	    }
	    	    set
	    	    {
	    	          if (this._IndicaNotnull != value)
	    	          {
	    	              this.ValidateProperty("IndicaNotnull", value);
	    	              this.OnIndicaNotnullChanging(value);
	    	              this.RaiseDataMemberChanging("IndicaNotnull");
	    	              this._IndicaNotnull = value;
	    	              this.RaiseDataMemberChanged("IndicaNotnull");
	    	              this.OnIndicaNotnullChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IndicaPk
	    partial void OnIndicaPkChanging(Boolean value);
	    partial void OnIndicaPkChanged();

	    private Boolean _IndicaPk;

	    [DataMember(IsRequired = true, Name = "IndicaPk", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Campo Chave", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_ITEM_CAMPO.INDICA_PK];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM_CAMPO.INDICA_PK")]
	    public Boolean IndicaPk
	    {
	    	    get
	    	    {
	    	          return _IndicaPk;
	    	    }
	    	    set
	    	    {
	    	          if (this._IndicaPk != value)
	    	          {
	    	              this.ValidateProperty("IndicaPk", value);
	    	              this.OnIndicaPkChanging(value);
	    	              this.RaiseDataMemberChanging("IndicaPk");
	    	              this._IndicaPk = value;
	    	              this.RaiseDataMemberChanged("IndicaPk");
	    	              this.OnIndicaPkChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxFormatoData
	    partial void OnLxFormatoDataChanging(System.String value);
	    partial void OnLxFormatoDataChanged();

	    private System.String _LxFormatoData;

	    [DataMember(Name = "LxFormatoData", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Formato de Data", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(1)]
	    [FunctionalPoint("Precision[1:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[FormatoData];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_ITEM_CAMPO.LX_FORMATO_DATA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM_CAMPO.LX_FORMATO_DATA")]
	    public System.String LxFormatoData
	    {
	    	    get
	    	    {
	    	          return _LxFormatoData;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxFormatoData != value)
	    	          {
	    	              this.ValidateProperty("LxFormatoData", value);
	    	              this.OnLxFormatoDataChanging(value);
	    	              this.RaiseDataMemberChanging("LxFormatoData");
	    	              this._LxFormatoData = value;
	    	              this.RaiseDataMemberChanged("LxFormatoData");
	    	              this.OnLxFormatoDataChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoDado
	    partial void OnLxTipoDadoChanging(System.String value);
	    partial void OnLxTipoDadoChanged();

	    private System.String _LxTipoDado;

	    [DataMember(IsRequired = true, Name = "LxTipoDado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo de Dado", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(3)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoDado];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_ITEM_CAMPO.LX_TIPO_DADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM_CAMPO.LX_TIPO_DADO")]
	    public System.String LxTipoDado
	    {
	    	    get
	    	    {
	    	          return _LxTipoDado;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoDado != value)
	    	          {
	    	              this.ValidateProperty("LxTipoDado", value);
	    	              this.OnLxTipoDadoChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoDado");
	    	              this._LxTipoDado = value;
	    	              this.RaiseDataMemberChanged("LxTipoDado");
	    	              this.OnLxTipoDadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Ordem
	    partial void OnOrdemChanging(Int32 value);
	    partial void OnOrdemChanged();

	    private Int32 _Ordem;

	    [DataMember(IsRequired = true, Name = "Ordem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ordem do Elemento", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_ITEM_CAMPO.ORDEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM_CAMPO.ORDEM")]
	    public Int32 Ordem
	    {
	    	    get
	    	    {
	    	          return _Ordem;
	    	    }
	    	    set
	    	    {
	    	          if (this._Ordem != value)
	    	          {
	    	              this.ValidateProperty("Ordem", value);
	    	              this.OnOrdemChanging(value);
	    	              this.RaiseDataMemberChanging("Ordem");
	    	              this._Ordem = value;
	    	              this.RaiseDataMemberChanged("Ordem");
	    	              this.OnOrdemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For TagCampo
	    partial void OnTagCampoChanging(System.String value);
	    partial void OnTagCampoChanged();

	    private System.String _TagCampo;

	    [DataMember(IsRequired = true, Name = "TagCampo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome do Campo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_ITEM_CAMPO.TAG_CAMPO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM_CAMPO.TAG_CAMPO")]
	    public System.String TagCampo
	    {
	    	    get
	    	    {
	    	          return _TagCampo;
	    	    }
	    	    set
	    	    {
	    	          if (this._TagCampo != value)
	    	          {
	    	              this.ValidateProperty("TagCampo", value);
	    	              this.OnTagCampoChanging(value);
	    	              this.RaiseDataMemberChanging("TagCampo");
	    	              this._TagCampo = value;
	    	              this.RaiseDataMemberChanged("TagCampo");
	    	              this.OnTagCampoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Tamanho
	    partial void OnTamanhoChanging(Int32 value);
	    partial void OnTamanhoChanged();

	    private Int32 _Tamanho;

	    [DataMember(IsRequired = true, Name = "Tamanho", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tamanho Total", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_ITEM_CAMPO.TAMANHO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM_CAMPO.TAMANHO")]
	    public Int32 Tamanho
	    {
	    	    get
	    	    {
	    	          return _Tamanho;
	    	    }
	    	    set
	    	    {
	    	          if (this._Tamanho != value)
	    	          {
	    	              this.ValidateProperty("Tamanho", value);
	    	              this.OnTamanhoChanging(value);
	    	              this.RaiseDataMemberChanging("Tamanho");
	    	              this._Tamanho = value;
	    	              this.RaiseDataMemberChanged("Tamanho");
	    	              this.OnTamanhoChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIdArquivoItemCampo;
	    [DataMember(Name = "TemporaryIdArquivoItemCampo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Campo (Tmp)", Description="Temporary Key", Order = 7, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdArquivoItemCampo
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdArquivoItemCampo.IsNullOrEmpty())
	    	                this._TemporaryIdArquivoItemCampo = this._IdArquivoItemCampo;
	    	          return this._TemporaryIdArquivoItemCampo;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdArquivoItemCampo != value)
	    	              this._TemporaryIdArquivoItemCampo = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsArquivoItem _TcsArquivoItem;
	    [DataMember(Name = "TcsArquivoItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsArquivoItem_TcsArquivoItemCampo", "IdArquivoItemFk", "IdArquivoItem", IsForeignKey=true)]
	    public TcsArquivoItem TcsArquivoItem
	    {
	        get
	        {
	            return this._TcsArquivoItem;
	        }
	        set
	        {
	            if (this._TcsArquivoItem != value)
	            {
	                this._TcsArquivoItem = value;
	                this.RaisePropertyChanged("TcsArquivoItemList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_ARQUIVO_ITEM_CAMPO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_ARQUIVO_ITEM_CAMPO), QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM_CAMPO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_ITEM_CAMPO.ORDEM", Source = "Ordem", Target = "ORDEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM_CAMPO", RelationPropertyName = "TCS_ARQUIVO_ITEM_CAMPO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_ITEM_CAMPO.TAMANHO", Source = "Tamanho", Target = "TAMANHO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM_CAMPO", RelationPropertyName = "TCS_ARQUIVO_ITEM_CAMPO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_ITEM_CAMPO.DECIMAIS", Source = "Decimais", Target = "DECIMAIS", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM_CAMPO", RelationPropertyName = "TCS_ARQUIVO_ITEM_CAMPO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_ITEM_CAMPO.INDICA_PK", Source = "IndicaPk", Target = "INDICA_PK", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM_CAMPO", RelationPropertyName = "TCS_ARQUIVO_ITEM_CAMPO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_ITEM_CAMPO.TAG_CAMPO", Source = "TagCampo", Target = "TAG_CAMPO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM_CAMPO", RelationPropertyName = "TCS_ARQUIVO_ITEM_CAMPO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_ITEM_CAMPO.LX_TIPO_DADO", Source = "LxTipoDado", Target = "LX_TIPO_DADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM_CAMPO", RelationPropertyName = "TCS_ARQUIVO_ITEM_CAMPO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_ITEM_CAMPO.INDICA_NOTNULL", Source = "IndicaNotnull", Target = "INDICA_NOTNULL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM_CAMPO", RelationPropertyName = "TCS_ARQUIVO_ITEM_CAMPO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_ITEM_CAMPO.LX_FORMATO_DATA", Source = "LxFormatoData", Target = "LX_FORMATO_DATA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM_CAMPO", RelationPropertyName = "TCS_ARQUIVO_ITEM_CAMPO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_ITEM_CAMPO.ID_ARQUIVO_ITEM_FK", Source = "IdArquivoItemFk", Target = "ID_ARQUIVO_ITEM_FK", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM_CAMPO", RelationPropertyName = "TCS_ARQUIVO_ITEM_CAMPO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_ITEM_CAMPO.CHAVE_IDENTIFICACAO", Source = "ChaveIdentificacao", Target = "CHAVE_IDENTIFICACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM_CAMPO", RelationPropertyName = "TCS_ARQUIVO_ITEM_CAMPO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_ITEM_CAMPO.ID_ARQUIVO_ITEM_CAMPO", Source = "IdArquivoItemCampo", Target = "ID_ARQUIVO_ITEM_CAMPO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM_CAMPO", RelationPropertyName = "TCS_ARQUIVO_ITEM_CAMPO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxFormatoDataValues()
	    {
	    	    return Linx.Framework.BV.Domains.FormatoData.GetValues();
	    }
	    private string _lxFormatoDataName;
	    [DataMember(IsRequired = false, Name = "LxFormatoDataName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Formato de Data", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxFormatoDataName
	    {
	    	    get { if (this.LxFormatoData.IsNullOrEmpty()) { _lxFormatoDataName = String.Empty; } else { string key = this.LxFormatoData.ToString(); var dmValues = this.GetLxFormatoDataValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxFormatoDataName) _lxFormatoDataName = domainName; } return _lxFormatoDataName; } set { _lxFormatoDataName = value;  }
	    }
	    public Dictionary<string, string> GetLxTipoDadoValues()
	    {
	    	    return Linx.Framework.BV.Domains.TipoDado.GetValues();
	    }
	    private string _lxTipoDadoName;
	    [DataMember(IsRequired = false, Name = "LxTipoDadoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo de Dado", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxTipoDadoName
	    {
	    	    get { if (this.LxTipoDado.IsNullOrEmpty()) { _lxTipoDadoName = String.Empty; } else { string key = this.LxTipoDado.ToString(); var dmValues = this.GetLxTipoDadoValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxTipoDadoName) _lxTipoDadoName = domainName; } return _lxTipoDadoName; } set { _lxTipoDadoName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_ARQUIVO_LOG.ID_ARQUIVO_LOG", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Logs];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdArquivoLog];ReadOnly[false];Entities[TCS_ARQUIVO_LOG:IdArquivoLog];SubQueryInfo[Select 1 From #ParentAlias#.TCS_ARQUIVO_LOG_LISTA as #Alias#];EdmEntityName[TCS_ARQUIVO_LOG];EntityRelations[TCS_ARQUIVO(TCS_ARQUIVO)#TCS_ARQUIVO_GRUPO(TCS_ARQUIVO_GRUPO)];EdmParentEntityName[TCS_ARQUIVO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsArquivoLog")]
	[Serializable()]
	public partial class TcsArquivoLog : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(LayoutArquivoDomainService context)
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
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_LOG.DATA_LOG];IsMeasure[false]")]
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
	    [Display(Name = "Descrição do Log", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(1000)]
	    [FunctionalPoint("Precision[1000:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_LOG.DESC_LOG];IsMeasure[false]")]
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
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_LOG.ID_ARQUIVO_FK];IsMeasure[false]")]
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
	    [Display(Name = "ID Log", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_LOG.ID_ARQUIVO_LOG];IsMeasure[false]")]
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
	    [Display(Name = "Tipo de Log", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoLog];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_LOG.LX_TIPO_LOG];IsMeasure[false]")]
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
	    [Display(Name = "ID Log (Tmp)", Description="Temporary Key", Order = 4, AutoGenerateField = false, GroupName="", ResourceType= null)]
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
	 
	    private TcsArquivo _TcsArquivo;
	    [DataMember(Name = "TcsArquivo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsArquivo_TcsArquivoLog", "IdArquivoFk", "IdArquivo", IsForeignKey=true)]
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
	    [Display(Name = "Tipo de Log", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_ARQUIVO_GRUPO_VINCULO.ID_ARQUIVO,TCS_ARQUIVO_GRUPO_VINCULO.TCS_ARQUIVO_GRUPO.ID_ARQUIVO_GRUPO", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdArquivo];ReadOnly[false];Entities[TCS_ARQUIVO_GRUPO_VINCULO:IdArquivo|TCS_ARQUIVO_GRUPO:IdArquivoGrupo];SubQueryInfo[Select 1 From #ParentAlias#.TCS_ARQUIVO_GRUPO_VINCULO_LISTA as #Alias#];EdmEntityName[TCS_ARQUIVO_GRUPO_VINCULO];EntityRelations[TCS_ARQUIVO(TCS_ARQUIVO)#TCS_ARQUIVO_GRUPO(TCS_ARQUIVO_GRUPO)];EdmParentEntityName[TCS_ARQUIVO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsArquivoGrupoVinculo")]
	[Serializable()]
	public partial class TcsArquivoGrupoVinculo : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(LayoutArquivoDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsArquivo");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdArquivo"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdArquivo));
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
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	    }

	    #endregion Flat Entities

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For CodArquivoGrupo
	    partial void OnCodArquivoGrupoChanging(System.String value);
	    partial void OnCodArquivoGrupoChanged();

	    private System.String _CodArquivoGrupo;

	    [DataMember(IsRequired = true, Name = "CodArquivoGrupo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Grupo de Arquivo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsArquivoGrupo];LookUpTitle[Seleção de (Grupo de Arquivo)];LookUpQuery[executeLookUpTcsArquivoGrupo];LookUpFinalize[finalizeLookUpTcsArquivoGrupo];LookUpDisplayColumns[{\"CodArquivoGrupo\" : \"Código\", \"DescArquivoGrupo\" : \"Descrição\", \"IdArquivoGrupo\" : \"ID\"}];LookUpColumns[{\"CodArquivoGrupo\" : true, \"DescArquivoGrupo\" : true, \"IdArquivoGrupo\" : true}];FilterDataKey[TCS_ARQUIVO_GRUPO_VINCULO.TCS_ARQUIVO_GRUPO.COD_ARQUIVO_GRUPO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#CodArquivoGrupo#false##10:0##Código#0#true##::LookUpTcsArquivoGrupo##false#false#TCS_ARQUIVO_GRUPO#TCS_ARQUIVO_GRUPO#Linx.Framework.BV.LayoutArquivo#IQueryable###true#false", EdmKey="TCS_ARQUIVO_GRUPO_VINCULO.TCS_ARQUIVO_GRUPO.COD_ARQUIVO_GRUPO")]
	    public System.String CodArquivoGrupo
	    {
	    	    get
	    	    {
	    	          return _CodArquivoGrupo;
	    	    }
	    	    set
	    	    {
	    	          if (this._CodArquivoGrupo != value)
	    	          {
	    	              this.ValidateProperty("CodArquivoGrupo", value);
	    	              this.OnCodArquivoGrupoChanging(value);
	    	              this.RaiseDataMemberChanging("CodArquivoGrupo");
	    	              this._CodArquivoGrupo = value;
	    	              this.RaiseDataMemberChanged("CodArquivoGrupo");
	    	              this.OnCodArquivoGrupoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescArquivoGrupo
	    partial void OnDescArquivoGrupoChanging(System.String value);
	    partial void OnDescArquivoGrupoChanged();

	    private System.String _DescArquivoGrupo;

	    [DataMember(IsRequired = true, Name = "DescArquivoGrupo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição do Grupo de Arquivo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(90)]
	    [FunctionalPoint("Precision[90:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[CodArquivoGrupo];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsArquivoGrupo];LookUpTitle[Seleção de (Descrição do Grupo de Arquivo)];LookUpQuery[executeLookUpTcsArquivoGrupo];LookUpFinalize[finalizeLookUpTcsArquivoGrupo];LookUpDisplayColumns[{\"CodArquivoGrupo\" : \"Código\", \"DescArquivoGrupo\" : \"Descrição\", \"IdArquivoGrupo\" : \"ID\"}];LookUpColumns[{\"CodArquivoGrupo\" : true, \"DescArquivoGrupo\" : true, \"IdArquivoGrupo\" : true}];FilterDataKey[TCS_ARQUIVO_GRUPO_VINCULO.TCS_ARQUIVO_GRUPO.DESC_ARQUIVO_GRUPO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescArquivoGrupo#false##90:0##Descrição#1#true##::LookUpTcsArquivoGrupo##false#false#TCS_ARQUIVO_GRUPO#TCS_ARQUIVO_GRUPO#Linx.Framework.BV.LayoutArquivo#IQueryable###true#false", EdmKey="TCS_ARQUIVO_GRUPO_VINCULO.TCS_ARQUIVO_GRUPO.DESC_ARQUIVO_GRUPO")]
	    public System.String DescArquivoGrupo
	    {
	    	    get
	    	    {
	    	          return _DescArquivoGrupo;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescArquivoGrupo != value)
	    	          {
	    	              this.ValidateProperty("DescArquivoGrupo", value);
	    	              this.OnDescArquivoGrupoChanging(value);
	    	              this.RaiseDataMemberChanging("DescArquivoGrupo");
	    	              this._DescArquivoGrupo = value;
	    	              this.RaiseDataMemberChanged("DescArquivoGrupo");
	    	              this.OnDescArquivoGrupoChanged();
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
	    [Display(Name = "ID", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_GRUPO_VINCULO.ID_ARQUIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_GRUPO_VINCULO.ID_ARQUIVO")]
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
	    //Extensibility Partial Method Definitions For IdArquivoGrupo
	    partial void OnIdArquivoGrupoChanging(Int32 value);
	    partial void OnIdArquivoGrupoChanged();

	    private Int32 _IdArquivoGrupo;

	    [DataMember(IsRequired = true, Name = "IdArquivoGrupo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Arquivo Grupo", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsArquivoGrupo];LookUpTitle[Seleção de (Id Arquivo Grupo)];LookUpQuery[executeLookUpTcsArquivoGrupo];LookUpFinalize[finalizeLookUpTcsArquivoGrupo];LookUpDisplayColumns[{\"CodArquivoGrupo\" : \"Código\", \"DescArquivoGrupo\" : \"Descrição\", \"IdArquivoGrupo\" : \"ID\"}];LookUpColumns[{\"CodArquivoGrupo\" : true, \"DescArquivoGrupo\" : true, \"IdArquivoGrupo\" : true}];FilterDataKey[TCS_ARQUIVO_GRUPO_VINCULO.TCS_ARQUIVO_GRUPO.ID_ARQUIVO_GRUPO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdArquivoGrupo#true##12:0##ID#2#true##::LookUpTcsArquivoGrupo##false#false#TCS_ARQUIVO_GRUPO#TCS_ARQUIVO_GRUPO#Linx.Framework.BV.LayoutArquivo#IQueryable###true#false", EdmKey="TCS_ARQUIVO_GRUPO_VINCULO.TCS_ARQUIVO_GRUPO.ID_ARQUIVO_GRUPO")]
	    public Int32 IdArquivoGrupo
	    {
	    	    get
	    	    {
	    	          return _IdArquivoGrupo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdArquivoGrupo != value)
	    	          {
	    	              this.ValidateProperty("IdArquivoGrupo", value);
	    	              this.OnIdArquivoGrupoChanging(value);
	    	              this.RaiseDataMemberChanging("IdArquivoGrupo");
	    	              this._IdArquivoGrupo = value;
	    	              this.RaiseDataMemberChanged("IdArquivoGrupo");
	    	              this.OnIdArquivoGrupoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Inativo
	    partial void OnInativoChanging(Boolean value);
	    partial void OnInativoChanged();

	    private Boolean _Inativo;

	    [DataMember(IsRequired = true, Name = "Inativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inativo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_GRUPO_VINCULO.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_GRUPO_VINCULO.INATIVO")]
	    public Boolean Inativo
	    {
	    	    get
	    	    {
	    	          return _Inativo;
	    	    }
	    	    set
	    	    {
	    	          if (this._Inativo != value)
	    	          {
	    	              this.ValidateProperty("Inativo", value);
	    	              this.OnInativoChanging(value);
	    	              this.RaiseDataMemberChanging("Inativo");
	    	              this._Inativo = value;
	    	              this.RaiseDataMemberChanged("Inativo");
	    	              this.OnInativoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Ordem
	    partial void OnOrdemChanging(Int32 value);
	    partial void OnOrdemChanged();

	    private Int32 _Ordem;

	    [DataMember(IsRequired = true, Name = "Ordem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ordem do Elemento", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_GRUPO_VINCULO.ORDEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_GRUPO_VINCULO.ORDEM")]
	    public Int32 Ordem
	    {
	    	    get
	    	    {
	    	          return _Ordem;
	    	    }
	    	    set
	    	    {
	    	          if (this._Ordem != value)
	    	          {
	    	              this.ValidateProperty("Ordem", value);
	    	              this.OnOrdemChanging(value);
	    	              this.RaiseDataMemberChanging("Ordem");
	    	              this._Ordem = value;
	    	              this.RaiseDataMemberChanged("Ordem");
	    	              this.OnOrdemChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIdArquivo;
	    [DataMember(Name = "TemporaryIdArquivo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID (Tmp)", Description="Temporary Key", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
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
	    [Association("FK_TcsArquivo_TcsArquivoGrupoVinculo", "IdArquivo", "IdArquivo", IsForeignKey=true)]
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
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_ARQUIVO_GRUPO_VINCULO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_ARQUIVO_GRUPO_VINCULO), QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_GRUPO_VINCULO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_GRUPO_VINCULO.ORDEM", Source = "Ordem", Target = "ORDEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_GRUPO_VINCULO", RelationPropertyName = "TCS_ARQUIVO_GRUPO_VINCULO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_GRUPO_VINCULO.INATIVO", Source = "Inativo", Target = "INATIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_GRUPO_VINCULO", RelationPropertyName = "TCS_ARQUIVO_GRUPO_VINCULO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_GRUPO_VINCULO.ID_ARQUIVO", Source = "IdArquivo", Target = "ID_ARQUIVO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_GRUPO_VINCULO", RelationPropertyName = "TCS_ARQUIVO_GRUPO_VINCULO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_GRUPO_VINCULO.TCS_ARQUIVO_GRUPO.ID_ARQUIVO_GRUPO", Source = "IdArquivoGrupo", Target = "ID_ARQUIVO_GRUPO", TargetKeyName = "ID_ARQUIVO_GRUPO", NoUpdatable = false, IsKey = true, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_GRUPO", RelationPropertyName = "TCS_ARQUIVO_GRUPO" });

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

		

	[LinxPublicationView(PrimaryKeys="TCS_ARQUIVO_GRUPO.ID_ARQUIVO_GRUPO", IsUpdatable=false, EdmName="")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsArquivoGrupo];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdArquivoGrupo];ReadOnly[false];Entities[TCS_ARQUIVO_GRUPO:IdArquivoGrupo];SubQueryInfo[];EdmEntityName[TCS_ARQUIVO_GRUPO];EntityRelations[];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsArquivoGrupo")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.LayoutArquivo.TcsArquivoGrupo")]
	public partial class TcsArquivoGrupo : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For CodArquivoGrupo
	    partial void OnCodArquivoGrupoChanging(System.String value);
	    partial void OnCodArquivoGrupoChanged();

	    private System.String _CodArquivoGrupo;

	    [DataMember(IsRequired = true, Name = "CodArquivoGrupo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Código", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_GRUPO.COD_ARQUIVO_GRUPO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_GRUPO.COD_ARQUIVO_GRUPO")]
	    public System.String CodArquivoGrupo
	    {
	    	    get
	    	    {
	    	          return _CodArquivoGrupo;
	    	    }
	    	    set
	    	    {
	    	          if (this._CodArquivoGrupo != value)
	    	          {
	    	              this.ValidateProperty("CodArquivoGrupo", value);
	    	              this.OnCodArquivoGrupoChanging(value);
	    	              this.RaiseDataMemberChanging("CodArquivoGrupo");
	    	              this._CodArquivoGrupo = value;
	    	              this.RaiseDataMemberChanged("CodArquivoGrupo");
	    	              this.OnCodArquivoGrupoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescArquivoGrupo
	    partial void OnDescArquivoGrupoChanging(System.String value);
	    partial void OnDescArquivoGrupoChanged();

	    private System.String _DescArquivoGrupo;

	    [DataMember(IsRequired = true, Name = "DescArquivoGrupo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(90)]
	    [FunctionalPoint("Precision[90:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_GRUPO.DESC_ARQUIVO_GRUPO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_GRUPO.DESC_ARQUIVO_GRUPO")]
	    public System.String DescArquivoGrupo
	    {
	    	    get
	    	    {
	    	          return _DescArquivoGrupo;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescArquivoGrupo != value)
	    	          {
	    	              this.ValidateProperty("DescArquivoGrupo", value);
	    	              this.OnDescArquivoGrupoChanging(value);
	    	              this.RaiseDataMemberChanging("DescArquivoGrupo");
	    	              this._DescArquivoGrupo = value;
	    	              this.RaiseDataMemberChanged("DescArquivoGrupo");
	    	              this.OnDescArquivoGrupoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdArquivoGrupo
	    partial void OnIdArquivoGrupoChanging(Int32 value);
	    partial void OnIdArquivoGrupoChanged();

	    private Int32 _IdArquivoGrupo;

	    [DataMember(IsRequired = true, Name = "IdArquivoGrupo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_GRUPO.ID_ARQUIVO_GRUPO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_GRUPO.ID_ARQUIVO_GRUPO")]
	    public Int32 IdArquivoGrupo
	    {
	    	    get
	    	    {
	    	          return _IdArquivoGrupo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdArquivoGrupo != value)
	    	          {
	    	              this.ValidateProperty("IdArquivoGrupo", value);
	    	              this.OnIdArquivoGrupoChanging(value);
	    	              this.RaiseDataMemberChanging("IdArquivoGrupo");
	    	              this._IdArquivoGrupo = value;
	    	              this.RaiseDataMemberChanged("IdArquivoGrupo");
	    	              this.OnIdArquivoGrupoChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIdArquivoGrupo;
	    [DataMember(Name = "TemporaryIdArquivoGrupo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID (Tmp)", Description="Temporary Key", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdArquivoGrupo
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdArquivoGrupo.IsNullOrEmpty())
	    	                this._TemporaryIdArquivoGrupo = this._IdArquivoGrupo;
	    	          return this._TemporaryIdArquivoGrupo;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdArquivoGrupo != value)
	    	              this._TemporaryIdArquivoGrupo = value;
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
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Elementos];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdArquivoItem];ReadOnly[false];Entities[TCS_ARQUIVO_ITEM:IdArquivoItem|TCS_ARQUIVO_ITEM:IdArquivoItemPai];SubQueryInfo[Select 1 From #ParentAlias#.TCS_ARQUIVO_ITEM_LISTA as #Alias#];EdmEntityName[TCS_ARQUIVO_ITEM];EntityRelations[ARQUIVO_ITEM_PAI(TCS_ARQUIVO_ITEM)#TCS_ARQUIVO(TCS_ARQUIVO)#TCS_ARQUIVO_GRUPO(TCS_ARQUIVO_GRUPO)];EdmParentEntityName[TCS_ARQUIVO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsArquivoItem")]
	[Serializable()]
	public partial class TcsArquivoItemParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For IdArquivoFk
	    partial void OnIdArquivoFkChanging(Int32 value);
	    partial void OnIdArquivoFkChanged();

	    private Int32 _IdArquivoFk;

	    [DataMember(IsRequired = true, Name = "IdArquivoFk", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Arquivo Fk", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_ITEM.ID_ARQUIVO_FK];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM.ID_ARQUIVO_FK")]
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
	    //Extensibility Partial Method Definitions For IdArquivoItemPai
	    partial void OnIdArquivoItemPaiChanging(System.Nullable<Int32> value);
	    partial void OnIdArquivoItemPaiChanged();

	    private System.Nullable<Int32> _IdArquivoItemPai;

	    [DataMember(Name = "IdArquivoItemPai", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Elemento Pai", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpArquivoItemPai];LookUpTitle[Seleção de (ID Elemento Pai)];LookUpQuery[executeLookUpArquivoItemPai];LookUpFinalize[finalizeLookUpArquivoItemPai];LookUpDisplayColumns[{\"IdArquivoItemPai\" : \"ID Elemento Pai\", \"TagItemPai\" : \"Elemento Pai\"}];LookUpColumns[{\"IdArquivoItemPai\" : true, \"TagItemPai\" : true}];FilterDataKey[TCS_ARQUIVO_ITEM.ARQUIVO_ITEM_PAI.ID_ARQUIVO_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdArquivoItemPai#true##12:0##ID Elemento Pai#0#true##::LookUpArquivoItemPai##false#false#ARQUIVO_ITEM_PAI#TCS_ARQUIVO_ITEM#Linx.Framework.BV.LayoutArquivo#IQueryable###true#false", EdmKey="TCS_ARQUIVO_ITEM.ARQUIVO_ITEM_PAI.ID_ARQUIVO_ITEM")]
	    public System.Nullable<Int32> IdArquivoItemPai
	    {
	    	    get
	    	    {
	    	          return _IdArquivoItemPai;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdArquivoItemPai != value)
	    	          {
	    	              this.ValidateProperty("IdArquivoItemPai", value);
	    	              this.OnIdArquivoItemPaiChanging(value);
	    	              this.RaiseDataMemberChanging("IdArquivoItemPai");
	    	              this._IdArquivoItemPai = value;
	    	              this.RaiseDataMemberChanged("IdArquivoItemPai");
	    	              this.OnIdArquivoItemPaiChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdArquivoItem
	    partial void OnIdArquivoItemChanging(Int32 value);
	    partial void OnIdArquivoItemChanged();

	    private Int32 _IdArquivoItem;

	    [DataMember(IsRequired = true, Name = "IdArquivoItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Elemento", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_ITEM.ID_ARQUIVO_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM.ID_ARQUIVO_ITEM")]
	    public Int32 IdArquivoItem
	    {
	    	    get
	    	    {
	    	          return _IdArquivoItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdArquivoItem != value)
	    	          {
	    	              this.ValidateProperty("IdArquivoItem", value);
	    	              this.OnIdArquivoItemChanging(value);
	    	              this.RaiseDataMemberChanging("IdArquivoItem");
	    	              this._IdArquivoItem = value;
	    	              this.RaiseDataMemberChanged("IdArquivoItem");
	    	              this.OnIdArquivoItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IndicaNotnull
	    partial void OnIndicaNotnullChanging(Boolean value);
	    partial void OnIndicaNotnullChanged();

	    private Boolean _IndicaNotnull;

	    [DataMember(IsRequired = true, Name = "IndicaNotnull", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Obrigatório", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_ITEM.INDICA_NOTNULL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM.INDICA_NOTNULL")]
	    public Boolean IndicaNotnull
	    {
	    	    get
	    	    {
	    	          return _IndicaNotnull;
	    	    }
	    	    set
	    	    {
	    	          if (this._IndicaNotnull != value)
	    	          {
	    	              this.ValidateProperty("IndicaNotnull", value);
	    	              this.OnIndicaNotnullChanging(value);
	    	              this.RaiseDataMemberChanging("IndicaNotnull");
	    	              this._IndicaNotnull = value;
	    	              this.RaiseDataMemberChanged("IndicaNotnull");
	    	              this.OnIndicaNotnullChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Ordem
	    partial void OnOrdemChanging(Int32 value);
	    partial void OnOrdemChanged();

	    private Int32 _Ordem;

	    [DataMember(IsRequired = true, Name = "Ordem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ordem do Elemento", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_ITEM.ORDEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM.ORDEM")]
	    public Int32 Ordem
	    {
	    	    get
	    	    {
	    	          return _Ordem;
	    	    }
	    	    set
	    	    {
	    	          if (this._Ordem != value)
	    	          {
	    	              this.ValidateProperty("Ordem", value);
	    	              this.OnOrdemChanging(value);
	    	              this.RaiseDataMemberChanging("Ordem");
	    	              this._Ordem = value;
	    	              this.RaiseDataMemberChanged("Ordem");
	    	              this.OnOrdemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For TagItemPai
	    partial void OnTagItemPaiChanging(System.String value);
	    partial void OnTagItemPaiChanged();

	    private System.String _TagItemPai;

	    [DataMember(Name = "TagItemPai", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Elemento Pai", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpArquivoItemPai];LookUpTitle[Seleção de (Elemento Pai)];LookUpQuery[executeLookUpArquivoItemPai];LookUpFinalize[finalizeLookUpArquivoItemPai];LookUpDisplayColumns[{\"IdArquivoItemPai\" : \"ID Elemento Pai\", \"TagItemPai\" : \"Elemento Pai\"}];LookUpColumns[{\"IdArquivoItemPai\" : true, \"TagItemPai\" : true}];FilterDataKey[TCS_ARQUIVO_ITEM.ARQUIVO_ITEM_PAI.TAG_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#TagItemPai#false##40:0##Elemento Pai#1#true##::LookUpArquivoItemPai##false#false#ARQUIVO_ITEM_PAI#TCS_ARQUIVO_ITEM#Linx.Framework.BV.LayoutArquivo#IQueryable###true#false", EdmKey="TCS_ARQUIVO_ITEM.ARQUIVO_ITEM_PAI.TAG_ITEM")]
	    public System.String TagItemPai
	    {
	    	    get
	    	    {
	    	          return _TagItemPai;
	    	    }
	    	    set
	    	    {
	    	          if (this._TagItemPai != value)
	    	          {
	    	              this.ValidateProperty("TagItemPai", value);
	    	              this.OnTagItemPaiChanging(value);
	    	              this.RaiseDataMemberChanging("TagItemPai");
	    	              this._TagItemPai = value;
	    	              this.RaiseDataMemberChanged("TagItemPai");
	    	              this.OnTagItemPaiChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For TagItem
	    partial void OnTagItemChanging(System.String value);
	    partial void OnTagItemChanged();

	    private System.String _TagItem;

	    [DataMember(IsRequired = true, Name = "TagItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Elemento", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_ITEM.TAG_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM.TAG_ITEM")]
	    public System.String TagItem
	    {
	    	    get
	    	    {
	    	          return _TagItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._TagItem != value)
	    	          {
	    	              this.ValidateProperty("TagItem", value);
	    	              this.OnTagItemChanging(value);
	    	              this.RaiseDataMemberChanging("TagItem");
	    	              this._TagItem = value;
	    	              this.RaiseDataMemberChanged("TagItem");
	    	              this.OnTagItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Xmlns
	    partial void OnXmlnsChanging(System.String value);
	    partial void OnXmlnsChanged();

	    private System.String _Xmlns;

	    [DataMember(Name = "Xmlns", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Namespace do XSD", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_ITEM.XMLNS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM.XMLNS")]
	    public System.String Xmlns
	    {
	    	    get
	    	    {
	    	          return _Xmlns;
	    	    }
	    	    set
	    	    {
	    	          if (this._Xmlns != value)
	    	          {
	    	              this.ValidateProperty("Xmlns", value);
	    	              this.OnXmlnsChanging(value);
	    	              this.RaiseDataMemberChanging("Xmlns");
	    	              this._Xmlns = value;
	    	              this.RaiseDataMemberChanged("Xmlns");
	    	              this.OnXmlnsChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ArquivoDll
	    partial void OnArquivoDllChanging(System.String value);
	    partial void OnArquivoDllChanged();

	    private System.String _ArquivoDll;

	    [DataMember(Name = "ArquivoDll", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Arquivo DLL", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.ARQUIVO_DLL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.ARQUIVO_DLL")]
	    public System.String ArquivoDll
	    {
	    	    get
	    	    {
	    	          return _ArquivoDll;
	    	    }
	    	    set
	    	    {
	    	          if (this._ArquivoDll != value)
	    	          {
	    	              this.ValidateProperty("ArquivoDll", value);
	    	              this.OnArquivoDllChanging(value);
	    	              this.RaiseDataMemberChanging("ArquivoDll");
	    	              this._ArquivoDll = value;
	    	              this.RaiseDataMemberChanged("ArquivoDll");
	    	              this.OnArquivoDllChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For CaminhoArquivo
	    partial void OnCaminhoArquivoChanging(System.String value);
	    partial void OnCaminhoArquivoChanged();

	    private System.String _CaminhoArquivo;

	    [DataMember(IsRequired = true, Name = "CaminhoArquivo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Caminho do Arquivo", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.CAMINHO_ARQUIVO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For Classe
	    partial void OnClasseChanging(System.String value);
	    partial void OnClasseChanged();

	    private System.String _Classe;

	    [DataMember(Name = "Classe", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Classe", Description="", Order = 14, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(100)]
	    [FunctionalPoint("Precision[100:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.CLASSE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.CLASSE")]
	    public System.String Classe
	    {
	    	    get
	    	    {
	    	          return _Classe;
	    	    }
	    	    set
	    	    {
	    	          if (this._Classe != value)
	    	          {
	    	              this.ValidateProperty("Classe", value);
	    	              this.OnClasseChanging(value);
	    	              this.RaiseDataMemberChanging("Classe");
	    	              this._Classe = value;
	    	              this.RaiseDataMemberChanged("Classe");
	    	              this.OnClasseChanged();
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
	    [Display(Name = "Código", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.COD_ARQUIVO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For Delimitador
	    partial void OnDelimitadorChanging(System.String value);
	    partial void OnDelimitadorChanged();

	    private System.String _Delimitador;

	    [DataMember(Name = "Delimitador", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Delimitador", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(1)]
	    [FunctionalPoint("Precision[1:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.DELIMITADOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.DELIMITADOR")]
	    public System.String Delimitador
	    {
	    	    get
	    	    {
	    	          return _Delimitador;
	    	    }
	    	    set
	    	    {
	    	          if (this._Delimitador != value)
	    	          {
	    	              this.ValidateProperty("Delimitador", value);
	    	              this.OnDelimitadorChanging(value);
	    	              this.RaiseDataMemberChanging("Delimitador");
	    	              this._Delimitador = value;
	    	              this.RaiseDataMemberChanged("Delimitador");
	    	              this.OnDelimitadorChanged();
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
	    [Display(Name = "Descrição", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(90)]
	    [FunctionalPoint("Precision[90:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.DESC_ARQUIVO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For DetalheArquivo
	    partial void OnDetalheArquivoChanging(System.String value);
	    partial void OnDetalheArquivoChanged();

	    private System.String _DetalheArquivo;

	    [DataMember(Name = "DetalheArquivo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Detalhe Arquivo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(1000)]
	    [FunctionalPoint("Precision[1000:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.DETALHE_ARQUIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.DETALHE_ARQUIVO")]
	    public System.String DetalheArquivo
	    {
	    	    get
	    	    {
	    	          return _DetalheArquivo;
	    	    }
	    	    set
	    	    {
	    	          if (this._DetalheArquivo != value)
	    	          {
	    	              this.ValidateProperty("DetalheArquivo", value);
	    	              this.OnDetalheArquivoChanging(value);
	    	              this.RaiseDataMemberChanging("DetalheArquivo");
	    	              this._DetalheArquivo = value;
	    	              this.RaiseDataMemberChanged("DetalheArquivo");
	    	              this.OnDetalheArquivoChanged();
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
	    [Display(Name = "ID", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.ID_ARQUIVO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For Inativo
	    partial void OnInativoChanging(Boolean value);
	    partial void OnInativoChanged();

	    private Boolean _Inativo;

	    [DataMember(IsRequired = true, Name = "Inativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inativo", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.INATIVO")]
	    public Boolean Inativo
	    {
	    	    get
	    	    {
	    	          return _Inativo;
	    	    }
	    	    set
	    	    {
	    	          if (this._Inativo != value)
	    	          {
	    	              this.ValidateProperty("Inativo", value);
	    	              this.OnInativoChanging(value);
	    	              this.RaiseDataMemberChanging("Inativo");
	    	              this._Inativo = value;
	    	              this.RaiseDataMemberChanged("Inativo");
	    	              this.OnInativoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxFormatoData
	    partial void OnLxFormatoDataChanging(System.String value);
	    partial void OnLxFormatoDataChanged();

	    private System.String _LxFormatoData;

	    [DataMember(Name = "LxFormatoData", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Formato de Data", Description="", Order = 16, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(1)]
	    [FunctionalPoint("Precision[1:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[FormatoData];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.LX_FORMATO_DATA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.LX_FORMATO_DATA")]
	    public System.String LxFormatoData
	    {
	    	    get
	    	    {
	    	          return _LxFormatoData;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxFormatoData != value)
	    	          {
	    	              this.ValidateProperty("LxFormatoData", value);
	    	              this.OnLxFormatoDataChanging(value);
	    	              this.RaiseDataMemberChanging("LxFormatoData");
	    	              this._LxFormatoData = value;
	    	              this.RaiseDataMemberChanged("LxFormatoData");
	    	              this.OnLxFormatoDataChanged();
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
	    [Display(Name = "Tipo do Arquivo", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(1)]
	    [FunctionalPoint("Precision[1:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoArquivo];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.LX_TIPO_ARQUIVO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For Metodo
	    partial void OnMetodoChanging(System.String value);
	    partial void OnMetodoChanged();

	    private System.String _Metodo;

	    [DataMember(Name = "Metodo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Método", Description="", Order = 15, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.METODO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.METODO")]
	    public System.String Metodo
	    {
	    	    get
	    	    {
	    	          return _Metodo;
	    	    }
	    	    set
	    	    {
	    	          if (this._Metodo != value)
	    	          {
	    	              this.ValidateProperty("Metodo", value);
	    	              this.OnMetodoChanging(value);
	    	              this.RaiseDataMemberChanging("Metodo");
	    	              this._Metodo = value;
	    	              this.RaiseDataMemberChanged("Metodo");
	    	              this.OnMetodoChanged();
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
	    [Display(Name = "Nome do Arquivo", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.NOME_ARQUIVO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For TagMestre
	    partial void OnTagMestreChanging(System.String value);
	    partial void OnTagMestreChanged();

	    private System.String _TagMestre;

	    [DataMember(IsRequired = true, Name = "TagMestre", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tag Mestre", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.TAG_MESTRE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.TAG_MESTRE")]
	    public System.String TagMestre
	    {
	    	    get
	    	    {
	    	          return _TagMestre;
	    	    }
	    	    set
	    	    {
	    	          if (this._TagMestre != value)
	    	          {
	    	              this.ValidateProperty("TagMestre", value);
	    	              this.OnTagMestreChanging(value);
	    	              this.RaiseDataMemberChanging("TagMestre");
	    	              this._TagMestre = value;
	    	              this.RaiseDataMemberChanged("TagMestre");
	    	              this.OnTagMestreChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Xsd
	    partial void OnXsdChanging(System.String value);
	    partial void OnXsdChanged();

	    private System.String _Xsd;

	    [DataMember(Name = "Xsd", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "XSD", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.XSD];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.XSD")]
	    public System.String Xsd
	    {
	    	    get
	    	    {
	    	          return _Xsd;
	    	    }
	    	    set
	    	    {
	    	          if (this._Xsd != value)
	    	          {
	    	              this.ValidateProperty("Xsd", value);
	    	              this.OnXsdChanging(value);
	    	              this.RaiseDataMemberChanging("Xsd");
	    	              this._Xsd = value;
	    	              this.RaiseDataMemberChanged("Xsd");
	    	              this.OnXsdChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_ARQUIVO_ITEM").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_ARQUIVO_ITEM), QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_ITEM.ORDEM", Source = "Ordem", Target = "ORDEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM", RelationPropertyName = "TCS_ARQUIVO_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_ITEM.XMLNS", Source = "Xmlns", Target = "XMLNS", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM", RelationPropertyName = "TCS_ARQUIVO_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_ITEM.TAG_ITEM", Source = "TagItem", Target = "TAG_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM", RelationPropertyName = "TCS_ARQUIVO_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_ITEM.ID_ARQUIVO_FK", Source = "IdArquivoFk", Target = "ID_ARQUIVO_FK", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM", RelationPropertyName = "TCS_ARQUIVO_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_ITEM.INDICA_NOTNULL", Source = "IndicaNotnull", Target = "INDICA_NOTNULL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM", RelationPropertyName = "TCS_ARQUIVO_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_ITEM.ID_ARQUIVO_ITEM", Source = "IdArquivoItem", Target = "ID_ARQUIVO_ITEM", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM", RelationPropertyName = "TCS_ARQUIVO_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_ITEM.ARQUIVO_ITEM_PAI.ID_ARQUIVO_ITEM", Source = "IdArquivoItemPai", Target = "ID_ARQUIVO_ITEM", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM", RelationPropertyName = "ARQUIVO_ITEM_PAI" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxFormatoDataValues()
	    {
	    	    return Linx.Framework.BV.Domains.FormatoData.GetValues();
	    }
	    private string _lxFormatoDataName;
	    [DataMember(IsRequired = false, Name = "LxFormatoDataName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Formato de Data", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxFormatoDataName
	    {
	    	    get { if (this.LxFormatoData.IsNullOrEmpty()) { _lxFormatoDataName = String.Empty; } else { string key = this.LxFormatoData.ToString(); var dmValues = this.GetLxFormatoDataValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxFormatoDataName) _lxFormatoDataName = domainName; } return _lxFormatoDataName; } set { _lxFormatoDataName = value;  }
	    }
	    public Dictionary<string, string> GetLxTipoArquivoValues()
	    {
	    	    return Linx.Framework.BV.Domains.TipoArquivo.GetValues();
	    }
	    private string _lxTipoArquivoName;
	    [DataMember(IsRequired = false, Name = "LxTipoArquivoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo do Arquivo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
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

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Campos];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdArquivoItemCampo];ReadOnly[false];Entities[TCS_ARQUIVO_ITEM_CAMPO:IdArquivoItemCampo];SubQueryInfo[Select 1 From #ParentAlias#.TCS_ARQUIVO_ITEM_CAMPO_LISTA as #Alias#];EdmEntityName[TCS_ARQUIVO_ITEM_CAMPO];EntityRelations[TCS_ARQUIVO_ITEM(TCS_ARQUIVO_ITEM)#ARQUIVO_ITEM_PAI(TCS_ARQUIVO_ITEM)#TCS_ARQUIVO(TCS_ARQUIVO)];EdmParentEntityName[TCS_ARQUIVO_ITEM];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsArquivoItemCampo")]
	[Serializable()]
	public partial class TcsArquivoItemCampoParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For ChaveIdentificacao
	    partial void OnChaveIdentificacaoChanging(System.String value);
	    partial void OnChaveIdentificacaoChanged();

	    private System.String _ChaveIdentificacao;

	    [DataMember(Name = "ChaveIdentificacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Chave de Identificação", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(25)]
	    [FunctionalPoint("Precision[25:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_ITEM_CAMPO.CHAVE_IDENTIFICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM_CAMPO.CHAVE_IDENTIFICACAO")]
	    public System.String ChaveIdentificacao
	    {
	    	    get
	    	    {
	    	          return _ChaveIdentificacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._ChaveIdentificacao != value)
	    	          {
	    	              this.ValidateProperty("ChaveIdentificacao", value);
	    	              this.OnChaveIdentificacaoChanging(value);
	    	              this.RaiseDataMemberChanging("ChaveIdentificacao");
	    	              this._ChaveIdentificacao = value;
	    	              this.RaiseDataMemberChanged("ChaveIdentificacao");
	    	              this.OnChaveIdentificacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Decimais
	    partial void OnDecimaisChanging(Byte value);
	    partial void OnDecimaisChanged();

	    private Byte _Decimais;

	    [DataMember(IsRequired = true, Name = "Decimais", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Casas Decimais", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_ITEM_CAMPO.DECIMAIS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM_CAMPO.DECIMAIS")]
	    public Byte Decimais
	    {
	    	    get
	    	    {
	    	          return _Decimais;
	    	    }
	    	    set
	    	    {
	    	          if (this._Decimais != value)
	    	          {
	    	              this.ValidateProperty("Decimais", value);
	    	              this.OnDecimaisChanging(value);
	    	              this.RaiseDataMemberChanging("Decimais");
	    	              this._Decimais = value;
	    	              this.RaiseDataMemberChanged("Decimais");
	    	              this.OnDecimaisChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdArquivoItemCampo
	    partial void OnIdArquivoItemCampoChanging(Int32 value);
	    partial void OnIdArquivoItemCampoChanged();

	    private Int32 _IdArquivoItemCampo;

	    [DataMember(IsRequired = true, Name = "IdArquivoItemCampo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Campo", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_ITEM_CAMPO.ID_ARQUIVO_ITEM_CAMPO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM_CAMPO.ID_ARQUIVO_ITEM_CAMPO")]
	    public Int32 IdArquivoItemCampo
	    {
	    	    get
	    	    {
	    	          return _IdArquivoItemCampo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdArquivoItemCampo != value)
	    	          {
	    	              this.ValidateProperty("IdArquivoItemCampo", value);
	    	              this.OnIdArquivoItemCampoChanging(value);
	    	              this.RaiseDataMemberChanging("IdArquivoItemCampo");
	    	              this._IdArquivoItemCampo = value;
	    	              this.RaiseDataMemberChanged("IdArquivoItemCampo");
	    	              this.OnIdArquivoItemCampoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdArquivoItemFk
	    partial void OnIdArquivoItemFkChanging(Int32 value);
	    partial void OnIdArquivoItemFkChanged();

	    private Int32 _IdArquivoItemFk;

	    [DataMember(IsRequired = true, Name = "IdArquivoItemFk", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Arquivo Item Fk", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_ITEM_CAMPO.ID_ARQUIVO_ITEM_FK];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM_CAMPO.ID_ARQUIVO_ITEM_FK")]
	    public Int32 IdArquivoItemFk
	    {
	    	    get
	    	    {
	    	          return _IdArquivoItemFk;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdArquivoItemFk != value)
	    	          {
	    	              this.ValidateProperty("IdArquivoItemFk", value);
	    	              this.OnIdArquivoItemFkChanging(value);
	    	              this.RaiseDataMemberChanging("IdArquivoItemFk");
	    	              this._IdArquivoItemFk = value;
	    	              this.RaiseDataMemberChanged("IdArquivoItemFk");
	    	              this.OnIdArquivoItemFkChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IndicaNotnull
	    partial void OnIndicaNotnullChanging(Boolean value);
	    partial void OnIndicaNotnullChanged();

	    private Boolean _IndicaNotnull;

	    [DataMember(IsRequired = true, Name = "IndicaNotnull", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Obrigatório", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_ITEM_CAMPO.INDICA_NOTNULL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM_CAMPO.INDICA_NOTNULL")]
	    public Boolean IndicaNotnull
	    {
	    	    get
	    	    {
	    	          return _IndicaNotnull;
	    	    }
	    	    set
	    	    {
	    	          if (this._IndicaNotnull != value)
	    	          {
	    	              this.ValidateProperty("IndicaNotnull", value);
	    	              this.OnIndicaNotnullChanging(value);
	    	              this.RaiseDataMemberChanging("IndicaNotnull");
	    	              this._IndicaNotnull = value;
	    	              this.RaiseDataMemberChanged("IndicaNotnull");
	    	              this.OnIndicaNotnullChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IndicaPk
	    partial void OnIndicaPkChanging(Boolean value);
	    partial void OnIndicaPkChanged();

	    private Boolean _IndicaPk;

	    [DataMember(IsRequired = true, Name = "IndicaPk", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Campo Chave", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_ITEM_CAMPO.INDICA_PK];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM_CAMPO.INDICA_PK")]
	    public Boolean IndicaPk
	    {
	    	    get
	    	    {
	    	          return _IndicaPk;
	    	    }
	    	    set
	    	    {
	    	          if (this._IndicaPk != value)
	    	          {
	    	              this.ValidateProperty("IndicaPk", value);
	    	              this.OnIndicaPkChanging(value);
	    	              this.RaiseDataMemberChanging("IndicaPk");
	    	              this._IndicaPk = value;
	    	              this.RaiseDataMemberChanged("IndicaPk");
	    	              this.OnIndicaPkChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxFormatoData
	    partial void OnLxFormatoDataChanging(System.String value);
	    partial void OnLxFormatoDataChanged();

	    private System.String _LxFormatoData;

	    [DataMember(Name = "LxFormatoData", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Formato de Data", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(1)]
	    [FunctionalPoint("Precision[1:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[FormatoData];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_ITEM_CAMPO.LX_FORMATO_DATA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM_CAMPO.LX_FORMATO_DATA")]
	    public System.String LxFormatoData
	    {
	    	    get
	    	    {
	    	          return _LxFormatoData;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxFormatoData != value)
	    	          {
	    	              this.ValidateProperty("LxFormatoData", value);
	    	              this.OnLxFormatoDataChanging(value);
	    	              this.RaiseDataMemberChanging("LxFormatoData");
	    	              this._LxFormatoData = value;
	    	              this.RaiseDataMemberChanged("LxFormatoData");
	    	              this.OnLxFormatoDataChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoDado
	    partial void OnLxTipoDadoChanging(System.String value);
	    partial void OnLxTipoDadoChanged();

	    private System.String _LxTipoDado;

	    [DataMember(IsRequired = true, Name = "LxTipoDado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo de Dado", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(3)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoDado];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_ITEM_CAMPO.LX_TIPO_DADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM_CAMPO.LX_TIPO_DADO")]
	    public System.String LxTipoDado
	    {
	    	    get
	    	    {
	    	          return _LxTipoDado;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoDado != value)
	    	          {
	    	              this.ValidateProperty("LxTipoDado", value);
	    	              this.OnLxTipoDadoChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoDado");
	    	              this._LxTipoDado = value;
	    	              this.RaiseDataMemberChanged("LxTipoDado");
	    	              this.OnLxTipoDadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Ordem
	    partial void OnOrdemChanging(Int32 value);
	    partial void OnOrdemChanged();

	    private Int32 _Ordem;

	    [DataMember(IsRequired = true, Name = "Ordem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ordem do Elemento", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_ITEM_CAMPO.ORDEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM_CAMPO.ORDEM")]
	    public Int32 Ordem
	    {
	    	    get
	    	    {
	    	          return _Ordem;
	    	    }
	    	    set
	    	    {
	    	          if (this._Ordem != value)
	    	          {
	    	              this.ValidateProperty("Ordem", value);
	    	              this.OnOrdemChanging(value);
	    	              this.RaiseDataMemberChanging("Ordem");
	    	              this._Ordem = value;
	    	              this.RaiseDataMemberChanged("Ordem");
	    	              this.OnOrdemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For TagCampo
	    partial void OnTagCampoChanging(System.String value);
	    partial void OnTagCampoChanged();

	    private System.String _TagCampo;

	    [DataMember(IsRequired = true, Name = "TagCampo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome do Campo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_ITEM_CAMPO.TAG_CAMPO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM_CAMPO.TAG_CAMPO")]
	    public System.String TagCampo
	    {
	    	    get
	    	    {
	    	          return _TagCampo;
	    	    }
	    	    set
	    	    {
	    	          if (this._TagCampo != value)
	    	          {
	    	              this.ValidateProperty("TagCampo", value);
	    	              this.OnTagCampoChanging(value);
	    	              this.RaiseDataMemberChanging("TagCampo");
	    	              this._TagCampo = value;
	    	              this.RaiseDataMemberChanged("TagCampo");
	    	              this.OnTagCampoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Tamanho
	    partial void OnTamanhoChanging(Int32 value);
	    partial void OnTamanhoChanged();

	    private Int32 _Tamanho;

	    [DataMember(IsRequired = true, Name = "Tamanho", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tamanho Total", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_ITEM_CAMPO.TAMANHO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM_CAMPO.TAMANHO")]
	    public Int32 Tamanho
	    {
	    	    get
	    	    {
	    	          return _Tamanho;
	    	    }
	    	    set
	    	    {
	    	          if (this._Tamanho != value)
	    	          {
	    	              this.ValidateProperty("Tamanho", value);
	    	              this.OnTamanhoChanging(value);
	    	              this.RaiseDataMemberChanging("Tamanho");
	    	              this._Tamanho = value;
	    	              this.RaiseDataMemberChanged("Tamanho");
	    	              this.OnTamanhoChanged();
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
	    [Display(Name = "Id Arquivo Fk", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO_ITEM.ID_ARQUIVO_FK];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM.ID_ARQUIVO_FK")]
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
	    //Extensibility Partial Method Definitions For IdArquivoItemPai
	    partial void OnIdArquivoItemPaiChanging(System.Nullable<Int32> value);
	    partial void OnIdArquivoItemPaiChanged();

	    private System.Nullable<Int32> _IdArquivoItemPai;

	    [DataMember(Name = "IdArquivoItemPai", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Elemento Pai", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO_ITEM.ARQUIVO_ITEM_PAI.ID_ARQUIVO_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM.ARQUIVO_ITEM_PAI.ID_ARQUIVO_ITEM")]
	    public System.Nullable<Int32> IdArquivoItemPai
	    {
	    	    get
	    	    {
	    	          return _IdArquivoItemPai;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdArquivoItemPai != value)
	    	          {
	    	              this.ValidateProperty("IdArquivoItemPai", value);
	    	              this.OnIdArquivoItemPaiChanging(value);
	    	              this.RaiseDataMemberChanging("IdArquivoItemPai");
	    	              this._IdArquivoItemPai = value;
	    	              this.RaiseDataMemberChanged("IdArquivoItemPai");
	    	              this.OnIdArquivoItemPaiChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdArquivoItem
	    partial void OnIdArquivoItemChanging(Int32 value);
	    partial void OnIdArquivoItemChanged();

	    private Int32 _IdArquivoItem;

	    [DataMember(IsRequired = true, Name = "IdArquivoItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Elemento", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO_ITEM.ID_ARQUIVO_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM.ID_ARQUIVO_ITEM")]
	    public Int32 IdArquivoItem
	    {
	    	    get
	    	    {
	    	          return _IdArquivoItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdArquivoItem != value)
	    	          {
	    	              this.ValidateProperty("IdArquivoItem", value);
	    	              this.OnIdArquivoItemChanging(value);
	    	              this.RaiseDataMemberChanging("IdArquivoItem");
	    	              this._IdArquivoItem = value;
	    	              this.RaiseDataMemberChanged("IdArquivoItem");
	    	              this.OnIdArquivoItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For TagItemPai
	    partial void OnTagItemPaiChanging(System.String value);
	    partial void OnTagItemPaiChanged();

	    private System.String _TagItemPai;

	    [DataMember(Name = "TagItemPai", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Elemento Pai", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO_ITEM.ARQUIVO_ITEM_PAI.TAG_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM.ARQUIVO_ITEM_PAI.TAG_ITEM")]
	    public System.String TagItemPai
	    {
	    	    get
	    	    {
	    	          return _TagItemPai;
	    	    }
	    	    set
	    	    {
	    	          if (this._TagItemPai != value)
	    	          {
	    	              this.ValidateProperty("TagItemPai", value);
	    	              this.OnTagItemPaiChanging(value);
	    	              this.RaiseDataMemberChanging("TagItemPai");
	    	              this._TagItemPai = value;
	    	              this.RaiseDataMemberChanged("TagItemPai");
	    	              this.OnTagItemPaiChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For TagItem
	    partial void OnTagItemChanging(System.String value);
	    partial void OnTagItemChanged();

	    private System.String _TagItem;

	    [DataMember(IsRequired = true, Name = "TagItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Elemento", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO_ITEM.TAG_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM.TAG_ITEM")]
	    public System.String TagItem
	    {
	    	    get
	    	    {
	    	          return _TagItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._TagItem != value)
	    	          {
	    	              this.ValidateProperty("TagItem", value);
	    	              this.OnTagItemChanging(value);
	    	              this.RaiseDataMemberChanging("TagItem");
	    	              this._TagItem = value;
	    	              this.RaiseDataMemberChanged("TagItem");
	    	              this.OnTagItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Xmlns
	    partial void OnXmlnsChanging(System.String value);
	    partial void OnXmlnsChanged();

	    private System.String _Xmlns;

	    [DataMember(Name = "Xmlns", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Namespace do XSD", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO_ITEM.XMLNS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_ITEM.XMLNS")]
	    public System.String Xmlns
	    {
	    	    get
	    	    {
	    	          return _Xmlns;
	    	    }
	    	    set
	    	    {
	    	          if (this._Xmlns != value)
	    	          {
	    	              this.ValidateProperty("Xmlns", value);
	    	              this.OnXmlnsChanging(value);
	    	              this.RaiseDataMemberChanging("Xmlns");
	    	              this._Xmlns = value;
	    	              this.RaiseDataMemberChanged("Xmlns");
	    	              this.OnXmlnsChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ArquivoDll
	    partial void OnArquivoDllChanging(System.String value);
	    partial void OnArquivoDllChanged();

	    private System.String _ArquivoDll;

	    [DataMember(Name = "ArquivoDll", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Arquivo DLL", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.ARQUIVO_DLL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.ARQUIVO_DLL")]
	    public System.String ArquivoDll
	    {
	    	    get
	    	    {
	    	          return _ArquivoDll;
	    	    }
	    	    set
	    	    {
	    	          if (this._ArquivoDll != value)
	    	          {
	    	              this.ValidateProperty("ArquivoDll", value);
	    	              this.OnArquivoDllChanging(value);
	    	              this.RaiseDataMemberChanging("ArquivoDll");
	    	              this._ArquivoDll = value;
	    	              this.RaiseDataMemberChanged("ArquivoDll");
	    	              this.OnArquivoDllChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For CaminhoArquivo
	    partial void OnCaminhoArquivoChanging(System.String value);
	    partial void OnCaminhoArquivoChanged();

	    private System.String _CaminhoArquivo;

	    [DataMember(IsRequired = true, Name = "CaminhoArquivo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Caminho do Arquivo", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.CAMINHO_ARQUIVO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For Classe
	    partial void OnClasseChanging(System.String value);
	    partial void OnClasseChanged();

	    private System.String _Classe;

	    [DataMember(Name = "Classe", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Classe", Description="", Order = 14, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(100)]
	    [FunctionalPoint("Precision[100:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.CLASSE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.CLASSE")]
	    public System.String Classe
	    {
	    	    get
	    	    {
	    	          return _Classe;
	    	    }
	    	    set
	    	    {
	    	          if (this._Classe != value)
	    	          {
	    	              this.ValidateProperty("Classe", value);
	    	              this.OnClasseChanging(value);
	    	              this.RaiseDataMemberChanging("Classe");
	    	              this._Classe = value;
	    	              this.RaiseDataMemberChanged("Classe");
	    	              this.OnClasseChanged();
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
	    [Display(Name = "Código", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.COD_ARQUIVO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For Delimitador
	    partial void OnDelimitadorChanging(System.String value);
	    partial void OnDelimitadorChanged();

	    private System.String _Delimitador;

	    [DataMember(Name = "Delimitador", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Delimitador", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(1)]
	    [FunctionalPoint("Precision[1:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.DELIMITADOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.DELIMITADOR")]
	    public System.String Delimitador
	    {
	    	    get
	    	    {
	    	          return _Delimitador;
	    	    }
	    	    set
	    	    {
	    	          if (this._Delimitador != value)
	    	          {
	    	              this.ValidateProperty("Delimitador", value);
	    	              this.OnDelimitadorChanging(value);
	    	              this.RaiseDataMemberChanging("Delimitador");
	    	              this._Delimitador = value;
	    	              this.RaiseDataMemberChanged("Delimitador");
	    	              this.OnDelimitadorChanged();
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
	    [Display(Name = "Descrição", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(90)]
	    [FunctionalPoint("Precision[90:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.DESC_ARQUIVO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For DetalheArquivo
	    partial void OnDetalheArquivoChanging(System.String value);
	    partial void OnDetalheArquivoChanged();

	    private System.String _DetalheArquivo;

	    [DataMember(Name = "DetalheArquivo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Detalhe Arquivo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(1000)]
	    [FunctionalPoint("Precision[1000:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.DETALHE_ARQUIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.DETALHE_ARQUIVO")]
	    public System.String DetalheArquivo
	    {
	    	    get
	    	    {
	    	          return _DetalheArquivo;
	    	    }
	    	    set
	    	    {
	    	          if (this._DetalheArquivo != value)
	    	          {
	    	              this.ValidateProperty("DetalheArquivo", value);
	    	              this.OnDetalheArquivoChanging(value);
	    	              this.RaiseDataMemberChanging("DetalheArquivo");
	    	              this._DetalheArquivo = value;
	    	              this.RaiseDataMemberChanged("DetalheArquivo");
	    	              this.OnDetalheArquivoChanged();
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
	    [Display(Name = "ID", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.ID_ARQUIVO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For Inativo
	    partial void OnInativoChanging(Boolean value);
	    partial void OnInativoChanged();

	    private Boolean _Inativo;

	    [DataMember(IsRequired = true, Name = "Inativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inativo", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.INATIVO")]
	    public Boolean Inativo
	    {
	    	    get
	    	    {
	    	          return _Inativo;
	    	    }
	    	    set
	    	    {
	    	          if (this._Inativo != value)
	    	          {
	    	              this.ValidateProperty("Inativo", value);
	    	              this.OnInativoChanging(value);
	    	              this.RaiseDataMemberChanging("Inativo");
	    	              this._Inativo = value;
	    	              this.RaiseDataMemberChanged("Inativo");
	    	              this.OnInativoChanged();
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
	    [Display(Name = "Tipo do Arquivo", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(1)]
	    [FunctionalPoint("Precision[1:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoArquivo];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.LX_TIPO_ARQUIVO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For Metodo
	    partial void OnMetodoChanging(System.String value);
	    partial void OnMetodoChanged();

	    private System.String _Metodo;

	    [DataMember(Name = "Metodo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Método", Description="", Order = 15, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.METODO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.METODO")]
	    public System.String Metodo
	    {
	    	    get
	    	    {
	    	          return _Metodo;
	    	    }
	    	    set
	    	    {
	    	          if (this._Metodo != value)
	    	          {
	    	              this.ValidateProperty("Metodo", value);
	    	              this.OnMetodoChanging(value);
	    	              this.RaiseDataMemberChanging("Metodo");
	    	              this._Metodo = value;
	    	              this.RaiseDataMemberChanged("Metodo");
	    	              this.OnMetodoChanged();
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
	    [Display(Name = "Nome do Arquivo", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.NOME_ARQUIVO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For TagMestre
	    partial void OnTagMestreChanging(System.String value);
	    partial void OnTagMestreChanged();

	    private System.String _TagMestre;

	    [DataMember(IsRequired = true, Name = "TagMestre", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tag Mestre", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.TAG_MESTRE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.TAG_MESTRE")]
	    public System.String TagMestre
	    {
	    	    get
	    	    {
	    	          return _TagMestre;
	    	    }
	    	    set
	    	    {
	    	          if (this._TagMestre != value)
	    	          {
	    	              this.ValidateProperty("TagMestre", value);
	    	              this.OnTagMestreChanging(value);
	    	              this.RaiseDataMemberChanging("TagMestre");
	    	              this._TagMestre = value;
	    	              this.RaiseDataMemberChanged("TagMestre");
	    	              this.OnTagMestreChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Xsd
	    partial void OnXsdChanging(System.String value);
	    partial void OnXsdChanged();

	    private System.String _Xsd;

	    [DataMember(Name = "Xsd", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "XSD", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.XSD];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.XSD")]
	    public System.String Xsd
	    {
	    	    get
	    	    {
	    	          return _Xsd;
	    	    }
	    	    set
	    	    {
	    	          if (this._Xsd != value)
	    	          {
	    	              this.ValidateProperty("Xsd", value);
	    	              this.OnXsdChanging(value);
	    	              this.RaiseDataMemberChanging("Xsd");
	    	              this._Xsd = value;
	    	              this.RaiseDataMemberChanged("Xsd");
	    	              this.OnXsdChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_ARQUIVO_ITEM_CAMPO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_ARQUIVO_ITEM_CAMPO), QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM_CAMPO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_ITEM_CAMPO.ORDEM", Source = "Ordem", Target = "ORDEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM_CAMPO", RelationPropertyName = "TCS_ARQUIVO_ITEM_CAMPO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_ITEM_CAMPO.TAMANHO", Source = "Tamanho", Target = "TAMANHO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM_CAMPO", RelationPropertyName = "TCS_ARQUIVO_ITEM_CAMPO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_ITEM_CAMPO.DECIMAIS", Source = "Decimais", Target = "DECIMAIS", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM_CAMPO", RelationPropertyName = "TCS_ARQUIVO_ITEM_CAMPO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_ITEM_CAMPO.INDICA_PK", Source = "IndicaPk", Target = "INDICA_PK", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM_CAMPO", RelationPropertyName = "TCS_ARQUIVO_ITEM_CAMPO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_ITEM_CAMPO.TAG_CAMPO", Source = "TagCampo", Target = "TAG_CAMPO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM_CAMPO", RelationPropertyName = "TCS_ARQUIVO_ITEM_CAMPO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_ITEM_CAMPO.LX_TIPO_DADO", Source = "LxTipoDado", Target = "LX_TIPO_DADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM_CAMPO", RelationPropertyName = "TCS_ARQUIVO_ITEM_CAMPO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_ITEM_CAMPO.INDICA_NOTNULL", Source = "IndicaNotnull", Target = "INDICA_NOTNULL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM_CAMPO", RelationPropertyName = "TCS_ARQUIVO_ITEM_CAMPO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_ITEM_CAMPO.LX_FORMATO_DATA", Source = "LxFormatoData", Target = "LX_FORMATO_DATA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM_CAMPO", RelationPropertyName = "TCS_ARQUIVO_ITEM_CAMPO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_ITEM_CAMPO.ID_ARQUIVO_ITEM_FK", Source = "IdArquivoItemFk", Target = "ID_ARQUIVO_ITEM_FK", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM_CAMPO", RelationPropertyName = "TCS_ARQUIVO_ITEM_CAMPO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_ITEM_CAMPO.CHAVE_IDENTIFICACAO", Source = "ChaveIdentificacao", Target = "CHAVE_IDENTIFICACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM_CAMPO", RelationPropertyName = "TCS_ARQUIVO_ITEM_CAMPO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_ITEM_CAMPO.ID_ARQUIVO_ITEM_CAMPO", Source = "IdArquivoItemCampo", Target = "ID_ARQUIVO_ITEM_CAMPO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_ITEM_CAMPO", RelationPropertyName = "TCS_ARQUIVO_ITEM_CAMPO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxFormatoDataValues()
	    {
	    	    return Linx.Framework.BV.Domains.FormatoData.GetValues();
	    }
	    private string _lxFormatoDataName;
	    [DataMember(IsRequired = false, Name = "LxFormatoDataName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Formato de Data", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxFormatoDataName
	    {
	    	    get { if (this.LxFormatoData.IsNullOrEmpty()) { _lxFormatoDataName = String.Empty; } else { string key = this.LxFormatoData.ToString(); var dmValues = this.GetLxFormatoDataValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxFormatoDataName) _lxFormatoDataName = domainName; } return _lxFormatoDataName; } set { _lxFormatoDataName = value;  }
	    }
	    public Dictionary<string, string> GetLxTipoDadoValues()
	    {
	    	    return Linx.Framework.BV.Domains.TipoDado.GetValues();
	    }
	    private string _lxTipoDadoName;
	    [DataMember(IsRequired = false, Name = "LxTipoDadoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo de Dado", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxTipoDadoName
	    {
	    	    get { if (this.LxTipoDado.IsNullOrEmpty()) { _lxTipoDadoName = String.Empty; } else { string key = this.LxTipoDado.ToString(); var dmValues = this.GetLxTipoDadoValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxTipoDadoName) _lxTipoDadoName = domainName; } return _lxTipoDadoName; } set { _lxTipoDadoName = value;  }
	    }
	    public Dictionary<string, string> GetLxTipoArquivoValues()
	    {
	    	    return Linx.Framework.BV.Domains.TipoArquivo.GetValues();
	    }
	    private string _lxTipoArquivoName;
	    [DataMember(IsRequired = false, Name = "LxTipoArquivoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo do Arquivo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
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

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Logs];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdArquivoLog];ReadOnly[false];Entities[TCS_ARQUIVO_LOG:IdArquivoLog];SubQueryInfo[Select 1 From #ParentAlias#.TCS_ARQUIVO_LOG_LISTA as #Alias#];EdmEntityName[TCS_ARQUIVO_LOG];EntityRelations[TCS_ARQUIVO(TCS_ARQUIVO)#TCS_ARQUIVO_GRUPO(TCS_ARQUIVO_GRUPO)];EdmParentEntityName[TCS_ARQUIVO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsArquivoLog")]
	[Serializable()]
	public partial class TcsArquivoLogParentComposition : Linx.Data.Entity
	{

	
	
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
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_LOG.DATA_LOG];IsMeasure[false]")]
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
	    [Display(Name = "Descrição do Log", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(1000)]
	    [FunctionalPoint("Precision[1000:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_LOG.DESC_LOG];IsMeasure[false]")]
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
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_LOG.ID_ARQUIVO_FK];IsMeasure[false]")]
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
	    [Display(Name = "ID Log", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_LOG.ID_ARQUIVO_LOG];IsMeasure[false]")]
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
	    [Display(Name = "Tipo de Log", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoLog];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_LOG.LX_TIPO_LOG];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For ArquivoDll
	    partial void OnArquivoDllChanging(System.String value);
	    partial void OnArquivoDllChanged();

	    private System.String _ArquivoDll;

	    [DataMember(Name = "ArquivoDll", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Arquivo DLL", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.ARQUIVO_DLL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.ARQUIVO_DLL")]
	    public System.String ArquivoDll
	    {
	    	    get
	    	    {
	    	          return _ArquivoDll;
	    	    }
	    	    set
	    	    {
	    	          if (this._ArquivoDll != value)
	    	          {
	    	              this.ValidateProperty("ArquivoDll", value);
	    	              this.OnArquivoDllChanging(value);
	    	              this.RaiseDataMemberChanging("ArquivoDll");
	    	              this._ArquivoDll = value;
	    	              this.RaiseDataMemberChanged("ArquivoDll");
	    	              this.OnArquivoDllChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For CaminhoArquivo
	    partial void OnCaminhoArquivoChanging(System.String value);
	    partial void OnCaminhoArquivoChanged();

	    private System.String _CaminhoArquivo;

	    [DataMember(IsRequired = true, Name = "CaminhoArquivo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Caminho do Arquivo", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.CAMINHO_ARQUIVO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For Classe
	    partial void OnClasseChanging(System.String value);
	    partial void OnClasseChanged();

	    private System.String _Classe;

	    [DataMember(Name = "Classe", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Classe", Description="", Order = 14, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(100)]
	    [FunctionalPoint("Precision[100:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.CLASSE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.CLASSE")]
	    public System.String Classe
	    {
	    	    get
	    	    {
	    	          return _Classe;
	    	    }
	    	    set
	    	    {
	    	          if (this._Classe != value)
	    	          {
	    	              this.ValidateProperty("Classe", value);
	    	              this.OnClasseChanging(value);
	    	              this.RaiseDataMemberChanging("Classe");
	    	              this._Classe = value;
	    	              this.RaiseDataMemberChanged("Classe");
	    	              this.OnClasseChanged();
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
	    [Display(Name = "Código", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.COD_ARQUIVO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For Delimitador
	    partial void OnDelimitadorChanging(System.String value);
	    partial void OnDelimitadorChanged();

	    private System.String _Delimitador;

	    [DataMember(Name = "Delimitador", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Delimitador", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(1)]
	    [FunctionalPoint("Precision[1:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.DELIMITADOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.DELIMITADOR")]
	    public System.String Delimitador
	    {
	    	    get
	    	    {
	    	          return _Delimitador;
	    	    }
	    	    set
	    	    {
	    	          if (this._Delimitador != value)
	    	          {
	    	              this.ValidateProperty("Delimitador", value);
	    	              this.OnDelimitadorChanging(value);
	    	              this.RaiseDataMemberChanging("Delimitador");
	    	              this._Delimitador = value;
	    	              this.RaiseDataMemberChanged("Delimitador");
	    	              this.OnDelimitadorChanged();
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
	    [Display(Name = "Descrição", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(90)]
	    [FunctionalPoint("Precision[90:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.DESC_ARQUIVO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For DetalheArquivo
	    partial void OnDetalheArquivoChanging(System.String value);
	    partial void OnDetalheArquivoChanged();

	    private System.String _DetalheArquivo;

	    [DataMember(Name = "DetalheArquivo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Detalhe Arquivo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(1000)]
	    [FunctionalPoint("Precision[1000:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.DETALHE_ARQUIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.DETALHE_ARQUIVO")]
	    public System.String DetalheArquivo
	    {
	    	    get
	    	    {
	    	          return _DetalheArquivo;
	    	    }
	    	    set
	    	    {
	    	          if (this._DetalheArquivo != value)
	    	          {
	    	              this.ValidateProperty("DetalheArquivo", value);
	    	              this.OnDetalheArquivoChanging(value);
	    	              this.RaiseDataMemberChanging("DetalheArquivo");
	    	              this._DetalheArquivo = value;
	    	              this.RaiseDataMemberChanged("DetalheArquivo");
	    	              this.OnDetalheArquivoChanged();
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
	    [Display(Name = "ID", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.ID_ARQUIVO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For Inativo
	    partial void OnInativoChanging(Boolean value);
	    partial void OnInativoChanged();

	    private Boolean _Inativo;

	    [DataMember(IsRequired = true, Name = "Inativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inativo", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.INATIVO")]
	    public Boolean Inativo
	    {
	    	    get
	    	    {
	    	          return _Inativo;
	    	    }
	    	    set
	    	    {
	    	          if (this._Inativo != value)
	    	          {
	    	              this.ValidateProperty("Inativo", value);
	    	              this.OnInativoChanging(value);
	    	              this.RaiseDataMemberChanging("Inativo");
	    	              this._Inativo = value;
	    	              this.RaiseDataMemberChanged("Inativo");
	    	              this.OnInativoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxFormatoData
	    partial void OnLxFormatoDataChanging(System.String value);
	    partial void OnLxFormatoDataChanged();

	    private System.String _LxFormatoData;

	    [DataMember(Name = "LxFormatoData", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Formato de Data", Description="", Order = 16, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(1)]
	    [FunctionalPoint("Precision[1:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[FormatoData];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.LX_FORMATO_DATA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.LX_FORMATO_DATA")]
	    public System.String LxFormatoData
	    {
	    	    get
	    	    {
	    	          return _LxFormatoData;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxFormatoData != value)
	    	          {
	    	              this.ValidateProperty("LxFormatoData", value);
	    	              this.OnLxFormatoDataChanging(value);
	    	              this.RaiseDataMemberChanging("LxFormatoData");
	    	              this._LxFormatoData = value;
	    	              this.RaiseDataMemberChanged("LxFormatoData");
	    	              this.OnLxFormatoDataChanged();
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
	    [Display(Name = "Tipo do Arquivo", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(1)]
	    [FunctionalPoint("Precision[1:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoArquivo];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.LX_TIPO_ARQUIVO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For Metodo
	    partial void OnMetodoChanging(System.String value);
	    partial void OnMetodoChanged();

	    private System.String _Metodo;

	    [DataMember(Name = "Metodo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Método", Description="", Order = 15, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.METODO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.METODO")]
	    public System.String Metodo
	    {
	    	    get
	    	    {
	    	          return _Metodo;
	    	    }
	    	    set
	    	    {
	    	          if (this._Metodo != value)
	    	          {
	    	              this.ValidateProperty("Metodo", value);
	    	              this.OnMetodoChanging(value);
	    	              this.RaiseDataMemberChanging("Metodo");
	    	              this._Metodo = value;
	    	              this.RaiseDataMemberChanged("Metodo");
	    	              this.OnMetodoChanged();
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
	    [Display(Name = "Nome do Arquivo", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.NOME_ARQUIVO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For TagMestre
	    partial void OnTagMestreChanging(System.String value);
	    partial void OnTagMestreChanged();

	    private System.String _TagMestre;

	    [DataMember(IsRequired = true, Name = "TagMestre", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tag Mestre", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.TAG_MESTRE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.TAG_MESTRE")]
	    public System.String TagMestre
	    {
	    	    get
	    	    {
	    	          return _TagMestre;
	    	    }
	    	    set
	    	    {
	    	          if (this._TagMestre != value)
	    	          {
	    	              this.ValidateProperty("TagMestre", value);
	    	              this.OnTagMestreChanging(value);
	    	              this.RaiseDataMemberChanging("TagMestre");
	    	              this._TagMestre = value;
	    	              this.RaiseDataMemberChanged("TagMestre");
	    	              this.OnTagMestreChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Xmlns
	    partial void OnXmlnsChanging(System.String value);
	    partial void OnXmlnsChanged();

	    private System.String _Xmlns;

	    [DataMember(Name = "Xmlns", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Namespace do XSD", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.XMLNS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.XMLNS")]
	    public System.String Xmlns
	    {
	    	    get
	    	    {
	    	          return _Xmlns;
	    	    }
	    	    set
	    	    {
	    	          if (this._Xmlns != value)
	    	          {
	    	              this.ValidateProperty("Xmlns", value);
	    	              this.OnXmlnsChanging(value);
	    	              this.RaiseDataMemberChanging("Xmlns");
	    	              this._Xmlns = value;
	    	              this.RaiseDataMemberChanged("Xmlns");
	    	              this.OnXmlnsChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Xsd
	    partial void OnXsdChanging(System.String value);
	    partial void OnXsdChanged();

	    private System.String _Xsd;

	    [DataMember(Name = "Xsd", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "XSD", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.XSD];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.XSD")]
	    public System.String Xsd
	    {
	    	    get
	    	    {
	    	          return _Xsd;
	    	    }
	    	    set
	    	    {
	    	          if (this._Xsd != value)
	    	          {
	    	              this.ValidateProperty("Xsd", value);
	    	              this.OnXsdChanging(value);
	    	              this.RaiseDataMemberChanging("Xsd");
	    	              this._Xsd = value;
	    	              this.RaiseDataMemberChanged("Xsd");
	    	              this.OnXsdChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
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
	    [Display(Name = "Tipo de Log", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxTipoLogName
	    {
	    	    get { if (this.LxTipoLog.IsNullOrEmpty()) { _lxTipoLogName = String.Empty; } else { string key = this.LxTipoLog.ToString(); var dmValues = this.GetLxTipoLogValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxTipoLogName) _lxTipoLogName = domainName; } return _lxTipoLogName; } set { _lxTipoLogName = value;  }
	    }
	    public Dictionary<string, string> GetLxFormatoDataValues()
	    {
	    	    return Linx.Framework.BV.Domains.FormatoData.GetValues();
	    }
	    private string _lxFormatoDataName;
	    [DataMember(IsRequired = false, Name = "LxFormatoDataName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Formato de Data", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxFormatoDataName
	    {
	    	    get { if (this.LxFormatoData.IsNullOrEmpty()) { _lxFormatoDataName = String.Empty; } else { string key = this.LxFormatoData.ToString(); var dmValues = this.GetLxFormatoDataValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxFormatoDataName) _lxFormatoDataName = domainName; } return _lxFormatoDataName; } set { _lxFormatoDataName = value;  }
	    }
	    public Dictionary<string, string> GetLxTipoArquivoValues()
	    {
	    	    return Linx.Framework.BV.Domains.TipoArquivo.GetValues();
	    }
	    private string _lxTipoArquivoName;
	    [DataMember(IsRequired = false, Name = "LxTipoArquivoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo do Arquivo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
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

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdArquivo];ReadOnly[false];Entities[TCS_ARQUIVO_GRUPO_VINCULO:IdArquivo|TCS_ARQUIVO_GRUPO:IdArquivoGrupo];SubQueryInfo[Select 1 From #ParentAlias#.TCS_ARQUIVO_GRUPO_VINCULO_LISTA as #Alias#];EdmEntityName[TCS_ARQUIVO_GRUPO_VINCULO];EntityRelations[TCS_ARQUIVO(TCS_ARQUIVO)#TCS_ARQUIVO_GRUPO(TCS_ARQUIVO_GRUPO)];EdmParentEntityName[TCS_ARQUIVO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsArquivoGrupoVinculo")]
	[Serializable()]
	public partial class TcsArquivoGrupoVinculoParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For CodArquivoGrupo
	    partial void OnCodArquivoGrupoChanging(System.String value);
	    partial void OnCodArquivoGrupoChanged();

	    private System.String _CodArquivoGrupo;

	    [DataMember(IsRequired = true, Name = "CodArquivoGrupo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Grupo de Arquivo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsArquivoGrupo];LookUpTitle[Seleção de (Grupo de Arquivo)];LookUpQuery[executeLookUpTcsArquivoGrupo];LookUpFinalize[finalizeLookUpTcsArquivoGrupo];LookUpDisplayColumns[{\"CodArquivoGrupo\" : \"Código\", \"DescArquivoGrupo\" : \"Descrição\", \"IdArquivoGrupo\" : \"ID\"}];LookUpColumns[{\"CodArquivoGrupo\" : true, \"DescArquivoGrupo\" : true, \"IdArquivoGrupo\" : true}];FilterDataKey[TCS_ARQUIVO_GRUPO_VINCULO.TCS_ARQUIVO_GRUPO.COD_ARQUIVO_GRUPO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#CodArquivoGrupo#false##10:0##Código#0#true##::LookUpTcsArquivoGrupo##false#false#TCS_ARQUIVO_GRUPO#TCS_ARQUIVO_GRUPO#Linx.Framework.BV.LayoutArquivo#IQueryable###true#false", EdmKey="TCS_ARQUIVO_GRUPO_VINCULO.TCS_ARQUIVO_GRUPO.COD_ARQUIVO_GRUPO")]
	    public System.String CodArquivoGrupo
	    {
	    	    get
	    	    {
	    	          return _CodArquivoGrupo;
	    	    }
	    	    set
	    	    {
	    	          if (this._CodArquivoGrupo != value)
	    	          {
	    	              this.ValidateProperty("CodArquivoGrupo", value);
	    	              this.OnCodArquivoGrupoChanging(value);
	    	              this.RaiseDataMemberChanging("CodArquivoGrupo");
	    	              this._CodArquivoGrupo = value;
	    	              this.RaiseDataMemberChanged("CodArquivoGrupo");
	    	              this.OnCodArquivoGrupoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescArquivoGrupo
	    partial void OnDescArquivoGrupoChanging(System.String value);
	    partial void OnDescArquivoGrupoChanged();

	    private System.String _DescArquivoGrupo;

	    [DataMember(IsRequired = true, Name = "DescArquivoGrupo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição do Grupo de Arquivo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(90)]
	    [FunctionalPoint("Precision[90:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[CodArquivoGrupo];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsArquivoGrupo];LookUpTitle[Seleção de (Descrição do Grupo de Arquivo)];LookUpQuery[executeLookUpTcsArquivoGrupo];LookUpFinalize[finalizeLookUpTcsArquivoGrupo];LookUpDisplayColumns[{\"CodArquivoGrupo\" : \"Código\", \"DescArquivoGrupo\" : \"Descrição\", \"IdArquivoGrupo\" : \"ID\"}];LookUpColumns[{\"CodArquivoGrupo\" : true, \"DescArquivoGrupo\" : true, \"IdArquivoGrupo\" : true}];FilterDataKey[TCS_ARQUIVO_GRUPO_VINCULO.TCS_ARQUIVO_GRUPO.DESC_ARQUIVO_GRUPO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescArquivoGrupo#false##90:0##Descrição#1#true##::LookUpTcsArquivoGrupo##false#false#TCS_ARQUIVO_GRUPO#TCS_ARQUIVO_GRUPO#Linx.Framework.BV.LayoutArquivo#IQueryable###true#false", EdmKey="TCS_ARQUIVO_GRUPO_VINCULO.TCS_ARQUIVO_GRUPO.DESC_ARQUIVO_GRUPO")]
	    public System.String DescArquivoGrupo
	    {
	    	    get
	    	    {
	    	          return _DescArquivoGrupo;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescArquivoGrupo != value)
	    	          {
	    	              this.ValidateProperty("DescArquivoGrupo", value);
	    	              this.OnDescArquivoGrupoChanging(value);
	    	              this.RaiseDataMemberChanging("DescArquivoGrupo");
	    	              this._DescArquivoGrupo = value;
	    	              this.RaiseDataMemberChanged("DescArquivoGrupo");
	    	              this.OnDescArquivoGrupoChanged();
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
	    [Display(Name = "ID", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_GRUPO_VINCULO.ID_ARQUIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_GRUPO_VINCULO.ID_ARQUIVO")]
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
	    //Extensibility Partial Method Definitions For IdArquivoGrupo
	    partial void OnIdArquivoGrupoChanging(Int32 value);
	    partial void OnIdArquivoGrupoChanged();

	    private Int32 _IdArquivoGrupo;

	    [DataMember(IsRequired = true, Name = "IdArquivoGrupo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Arquivo Grupo", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsArquivoGrupo];LookUpTitle[Seleção de (Id Arquivo Grupo)];LookUpQuery[executeLookUpTcsArquivoGrupo];LookUpFinalize[finalizeLookUpTcsArquivoGrupo];LookUpDisplayColumns[{\"CodArquivoGrupo\" : \"Código\", \"DescArquivoGrupo\" : \"Descrição\", \"IdArquivoGrupo\" : \"ID\"}];LookUpColumns[{\"CodArquivoGrupo\" : true, \"DescArquivoGrupo\" : true, \"IdArquivoGrupo\" : true}];FilterDataKey[TCS_ARQUIVO_GRUPO_VINCULO.TCS_ARQUIVO_GRUPO.ID_ARQUIVO_GRUPO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdArquivoGrupo#true##12:0##ID#2#true##::LookUpTcsArquivoGrupo##false#false#TCS_ARQUIVO_GRUPO#TCS_ARQUIVO_GRUPO#Linx.Framework.BV.LayoutArquivo#IQueryable###true#false", EdmKey="TCS_ARQUIVO_GRUPO_VINCULO.TCS_ARQUIVO_GRUPO.ID_ARQUIVO_GRUPO")]
	    public Int32 IdArquivoGrupo
	    {
	    	    get
	    	    {
	    	          return _IdArquivoGrupo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdArquivoGrupo != value)
	    	          {
	    	              this.ValidateProperty("IdArquivoGrupo", value);
	    	              this.OnIdArquivoGrupoChanging(value);
	    	              this.RaiseDataMemberChanging("IdArquivoGrupo");
	    	              this._IdArquivoGrupo = value;
	    	              this.RaiseDataMemberChanged("IdArquivoGrupo");
	    	              this.OnIdArquivoGrupoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Inativo
	    partial void OnInativoChanging(Boolean value);
	    partial void OnInativoChanged();

	    private Boolean _Inativo;

	    [DataMember(IsRequired = true, Name = "Inativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inativo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_GRUPO_VINCULO.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_GRUPO_VINCULO.INATIVO")]
	    public Boolean Inativo
	    {
	    	    get
	    	    {
	    	          return _Inativo;
	    	    }
	    	    set
	    	    {
	    	          if (this._Inativo != value)
	    	          {
	    	              this.ValidateProperty("Inativo", value);
	    	              this.OnInativoChanging(value);
	    	              this.RaiseDataMemberChanging("Inativo");
	    	              this._Inativo = value;
	    	              this.RaiseDataMemberChanged("Inativo");
	    	              this.OnInativoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Ordem
	    partial void OnOrdemChanging(Int32 value);
	    partial void OnOrdemChanged();

	    private Int32 _Ordem;

	    [DataMember(IsRequired = true, Name = "Ordem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ordem do Elemento", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_ARQUIVO_GRUPO_VINCULO.ORDEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO_GRUPO_VINCULO.ORDEM")]
	    public Int32 Ordem
	    {
	    	    get
	    	    {
	    	          return _Ordem;
	    	    }
	    	    set
	    	    {
	    	          if (this._Ordem != value)
	    	          {
	    	              this.ValidateProperty("Ordem", value);
	    	              this.OnOrdemChanging(value);
	    	              this.RaiseDataMemberChanging("Ordem");
	    	              this._Ordem = value;
	    	              this.RaiseDataMemberChanged("Ordem");
	    	              this.OnOrdemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ArquivoDll
	    partial void OnArquivoDllChanging(System.String value);
	    partial void OnArquivoDllChanged();

	    private System.String _ArquivoDll;

	    [DataMember(Name = "ArquivoDll", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Arquivo DLL", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.ARQUIVO_DLL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.ARQUIVO_DLL")]
	    public System.String ArquivoDll
	    {
	    	    get
	    	    {
	    	          return _ArquivoDll;
	    	    }
	    	    set
	    	    {
	    	          if (this._ArquivoDll != value)
	    	          {
	    	              this.ValidateProperty("ArquivoDll", value);
	    	              this.OnArquivoDllChanging(value);
	    	              this.RaiseDataMemberChanging("ArquivoDll");
	    	              this._ArquivoDll = value;
	    	              this.RaiseDataMemberChanged("ArquivoDll");
	    	              this.OnArquivoDllChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For CaminhoArquivo
	    partial void OnCaminhoArquivoChanging(System.String value);
	    partial void OnCaminhoArquivoChanged();

	    private System.String _CaminhoArquivo;

	    [DataMember(IsRequired = true, Name = "CaminhoArquivo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Caminho do Arquivo", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.CAMINHO_ARQUIVO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For Classe
	    partial void OnClasseChanging(System.String value);
	    partial void OnClasseChanged();

	    private System.String _Classe;

	    [DataMember(Name = "Classe", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Classe", Description="", Order = 14, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(100)]
	    [FunctionalPoint("Precision[100:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.CLASSE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.CLASSE")]
	    public System.String Classe
	    {
	    	    get
	    	    {
	    	          return _Classe;
	    	    }
	    	    set
	    	    {
	    	          if (this._Classe != value)
	    	          {
	    	              this.ValidateProperty("Classe", value);
	    	              this.OnClasseChanging(value);
	    	              this.RaiseDataMemberChanging("Classe");
	    	              this._Classe = value;
	    	              this.RaiseDataMemberChanged("Classe");
	    	              this.OnClasseChanged();
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
	    [Display(Name = "Código", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.COD_ARQUIVO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For Delimitador
	    partial void OnDelimitadorChanging(System.String value);
	    partial void OnDelimitadorChanged();

	    private System.String _Delimitador;

	    [DataMember(Name = "Delimitador", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Delimitador", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(1)]
	    [FunctionalPoint("Precision[1:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.DELIMITADOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.DELIMITADOR")]
	    public System.String Delimitador
	    {
	    	    get
	    	    {
	    	          return _Delimitador;
	    	    }
	    	    set
	    	    {
	    	          if (this._Delimitador != value)
	    	          {
	    	              this.ValidateProperty("Delimitador", value);
	    	              this.OnDelimitadorChanging(value);
	    	              this.RaiseDataMemberChanging("Delimitador");
	    	              this._Delimitador = value;
	    	              this.RaiseDataMemberChanged("Delimitador");
	    	              this.OnDelimitadorChanged();
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
	    [Display(Name = "Descrição", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(90)]
	    [FunctionalPoint("Precision[90:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.DESC_ARQUIVO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For DetalheArquivo
	    partial void OnDetalheArquivoChanging(System.String value);
	    partial void OnDetalheArquivoChanged();

	    private System.String _DetalheArquivo;

	    [DataMember(Name = "DetalheArquivo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Detalhe Arquivo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(1000)]
	    [FunctionalPoint("Precision[1000:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.DETALHE_ARQUIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.DETALHE_ARQUIVO")]
	    public System.String DetalheArquivo
	    {
	    	    get
	    	    {
	    	          return _DetalheArquivo;
	    	    }
	    	    set
	    	    {
	    	          if (this._DetalheArquivo != value)
	    	          {
	    	              this.ValidateProperty("DetalheArquivo", value);
	    	              this.OnDetalheArquivoChanging(value);
	    	              this.RaiseDataMemberChanging("DetalheArquivo");
	    	              this._DetalheArquivo = value;
	    	              this.RaiseDataMemberChanged("DetalheArquivo");
	    	              this.OnDetalheArquivoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxFormatoData
	    partial void OnLxFormatoDataChanging(System.String value);
	    partial void OnLxFormatoDataChanged();

	    private System.String _LxFormatoData;

	    [DataMember(Name = "LxFormatoData", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Formato de Data", Description="", Order = 16, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(1)]
	    [FunctionalPoint("Precision[1:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[FormatoData];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.LX_FORMATO_DATA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.LX_FORMATO_DATA")]
	    public System.String LxFormatoData
	    {
	    	    get
	    	    {
	    	          return _LxFormatoData;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxFormatoData != value)
	    	          {
	    	              this.ValidateProperty("LxFormatoData", value);
	    	              this.OnLxFormatoDataChanging(value);
	    	              this.RaiseDataMemberChanging("LxFormatoData");
	    	              this._LxFormatoData = value;
	    	              this.RaiseDataMemberChanged("LxFormatoData");
	    	              this.OnLxFormatoDataChanged();
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
	    [Display(Name = "Tipo do Arquivo", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(1)]
	    [FunctionalPoint("Precision[1:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoArquivo];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.LX_TIPO_ARQUIVO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For Metodo
	    partial void OnMetodoChanging(System.String value);
	    partial void OnMetodoChanged();

	    private System.String _Metodo;

	    [DataMember(Name = "Metodo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Método", Description="", Order = 15, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.METODO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.METODO")]
	    public System.String Metodo
	    {
	    	    get
	    	    {
	    	          return _Metodo;
	    	    }
	    	    set
	    	    {
	    	          if (this._Metodo != value)
	    	          {
	    	              this.ValidateProperty("Metodo", value);
	    	              this.OnMetodoChanging(value);
	    	              this.RaiseDataMemberChanging("Metodo");
	    	              this._Metodo = value;
	    	              this.RaiseDataMemberChanged("Metodo");
	    	              this.OnMetodoChanged();
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
	    [Display(Name = "Nome do Arquivo", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.NOME_ARQUIVO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For TagMestre
	    partial void OnTagMestreChanging(System.String value);
	    partial void OnTagMestreChanged();

	    private System.String _TagMestre;

	    [DataMember(IsRequired = true, Name = "TagMestre", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tag Mestre", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.TAG_MESTRE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.TAG_MESTRE")]
	    public System.String TagMestre
	    {
	    	    get
	    	    {
	    	          return _TagMestre;
	    	    }
	    	    set
	    	    {
	    	          if (this._TagMestre != value)
	    	          {
	    	              this.ValidateProperty("TagMestre", value);
	    	              this.OnTagMestreChanging(value);
	    	              this.RaiseDataMemberChanging("TagMestre");
	    	              this._TagMestre = value;
	    	              this.RaiseDataMemberChanged("TagMestre");
	    	              this.OnTagMestreChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Xmlns
	    partial void OnXmlnsChanging(System.String value);
	    partial void OnXmlnsChanged();

	    private System.String _Xmlns;

	    [DataMember(Name = "Xmlns", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Namespace do XSD", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.XMLNS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.XMLNS")]
	    public System.String Xmlns
	    {
	    	    get
	    	    {
	    	          return _Xmlns;
	    	    }
	    	    set
	    	    {
	    	          if (this._Xmlns != value)
	    	          {
	    	              this.ValidateProperty("Xmlns", value);
	    	              this.OnXmlnsChanging(value);
	    	              this.RaiseDataMemberChanging("Xmlns");
	    	              this._Xmlns = value;
	    	              this.RaiseDataMemberChanged("Xmlns");
	    	              this.OnXmlnsChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Xsd
	    partial void OnXsdChanging(System.String value);
	    partial void OnXsdChanged();

	    private System.String _Xsd;

	    [DataMember(Name = "Xsd", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "XSD", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_ARQUIVO.XSD];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_ARQUIVO.XSD")]
	    public System.String Xsd
	    {
	    	    get
	    	    {
	    	          return _Xsd;
	    	    }
	    	    set
	    	    {
	    	          if (this._Xsd != value)
	    	          {
	    	              this.ValidateProperty("Xsd", value);
	    	              this.OnXsdChanging(value);
	    	              this.RaiseDataMemberChanging("Xsd");
	    	              this._Xsd = value;
	    	              this.RaiseDataMemberChanged("Xsd");
	    	              this.OnXsdChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_ARQUIVO_GRUPO_VINCULO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_ARQUIVO_GRUPO_VINCULO), QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_GRUPO_VINCULO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_GRUPO_VINCULO.ORDEM", Source = "Ordem", Target = "ORDEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_GRUPO_VINCULO", RelationPropertyName = "TCS_ARQUIVO_GRUPO_VINCULO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_GRUPO_VINCULO.INATIVO", Source = "Inativo", Target = "INATIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_GRUPO_VINCULO", RelationPropertyName = "TCS_ARQUIVO_GRUPO_VINCULO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_GRUPO_VINCULO.ID_ARQUIVO", Source = "IdArquivo", Target = "ID_ARQUIVO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_GRUPO_VINCULO", RelationPropertyName = "TCS_ARQUIVO_GRUPO_VINCULO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_ARQUIVO_GRUPO_VINCULO.TCS_ARQUIVO_GRUPO.ID_ARQUIVO_GRUPO", Source = "IdArquivoGrupo", Target = "ID_ARQUIVO_GRUPO", TargetKeyName = "ID_ARQUIVO_GRUPO", NoUpdatable = false, IsKey = true, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_ARQUIVO_GRUPO", RelationPropertyName = "TCS_ARQUIVO_GRUPO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxFormatoDataValues()
	    {
	    	    return Linx.Framework.BV.Domains.FormatoData.GetValues();
	    }
	    private string _lxFormatoDataName;
	    [DataMember(IsRequired = false, Name = "LxFormatoDataName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Formato de Data", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxFormatoDataName
	    {
	    	    get { if (this.LxFormatoData.IsNullOrEmpty()) { _lxFormatoDataName = String.Empty; } else { string key = this.LxFormatoData.ToString(); var dmValues = this.GetLxFormatoDataValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxFormatoDataName) _lxFormatoDataName = domainName; } return _lxFormatoDataName; } set { _lxFormatoDataName = value;  }
	    }
	    public Dictionary<string, string> GetLxTipoArquivoValues()
	    {
	    	    return Linx.Framework.BV.Domains.TipoArquivo.GetValues();
	    }
	    private string _lxTipoArquivoName;
	    [DataMember(IsRequired = false, Name = "LxTipoArquivoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo do Arquivo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	//////////////////////// DomainService Class V1 ///////////////////////
	///////////////////////////////////////////////////////////////////////
	[EnableClientAccess()]	
	[DomainIdentifier("ProcessorOverviewLayoutArquivoDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class LayoutArquivoDomainService : DomainService, IDataServiceContext 
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

		
	    public LayoutArquivoDomainService() : this("", null, null){ }
	    public LayoutArquivoDomainService(string connectionString) : this(connectionString, null, null) { }
	    public LayoutArquivoDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public LayoutArquivoDomainService(Linx.Framework.ControleSistema.BM.ControleSistemaContext dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public LayoutArquivoDomainService(string connectionString, Linx.Framework.ControleSistema.BM.ControleSistemaContext dataContext, Dictionary<string, string> headers) : base() 
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
	
	    
	        foreach (ChangeSetEntry entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsArquivo))
	        {
	            ((TcsArquivo)entry.Entity).OnSavingChanges(this, changeSet.GetChangeOperation(entry.Entity));
	        }
    	
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
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsArquivoItem && e.Entity.GetType().Name == "TcsArquivoItem" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsArquivoItemCampo && e.Entity.GetType().Name == "TcsArquivoItemCampo" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsArquivoLog && e.Entity.GetType().Name == "TcsArquivoLog" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsArquivoGrupoVinculo && e.Entity.GetType().Name == "TcsArquivoGrupoVinculo" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
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
	    //Get All LookUpArquivoItemPai.
	    public IQueryable<LookUpArquivoItemPai> GetAllLookUpArquivoItemPai()
	    {
	        return this.GetLookUpArquivoItemPai(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpArquivoItemPai By EntitySearch.
	    public IQueryable<LookUpArquivoItemPai> GetLookUpArquivoItemPaiByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpArquivoItemPai(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpArquivoItemPai.
	    public IQueryable<LookUpArquivoItemPai> GetLookUpArquivoItemPai(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TCS_ARQUIVO_ITEM" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpArquivoItemPai";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpArquivoItemPai));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpArquivoItemPai> query =  
	
	            (from entity in this.DbContext.TCS_ARQUIVO_ITEM.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpArquivoItemPai()		
	            {
	            
                IdArquivoItemPai = entity.ID_ARQUIVO_ITEM
                , TagItemPai = entity.TAG_ITEM
                , IdArquivoFk = entity.ID_ARQUIVO_FK
	            });

	            
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpTcsArquivoGrupo.
	    public IQueryable<LookUpTcsArquivoGrupo> GetAllLookUpTcsArquivoGrupo()
	    {
	        return this.GetLookUpTcsArquivoGrupo(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsArquivoGrupo By EntitySearch.
	    public IQueryable<LookUpTcsArquivoGrupo> GetLookUpTcsArquivoGrupoByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsArquivoGrupo(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsArquivoGrupo.
	    public IQueryable<LookUpTcsArquivoGrupo> GetLookUpTcsArquivoGrupo(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TCS_ARQUIVO_GRUPO" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsArquivoGrupo";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsArquivoGrupo));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsArquivoGrupo> query =  
	
	            (from entity in this.DbContext.TCS_ARQUIVO_GRUPO.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpTcsArquivoGrupo()		
	            {
	            
                CodArquivoGrupo = entity.COD_ARQUIVO_GRUPO
                , DescArquivoGrupo = entity.DESC_ARQUIVO_GRUPO
                , IdArquivoGrupo = entity.ID_ARQUIVO_GRUPO
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
	
		

	        if (entityName.InList("Linx.Framework.BV.LayoutArquivo.TcsArquivo"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsArquivo",
	        			NameSpace = "Linx.Framework.BV.LayoutArquivo",
	        			ParentClassName = null,	
	        			DisplayName = "TcsArquivo",
	        			ClearMethodName = "ClearTcsArquivo",
	        			QueryMethodName  = "GetPagedTcsArquivo",	
	        			CountingMethodName  = "GetTcsArquivo" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.LayoutArquivo.TcsArquivo"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.LayoutArquivo.TcsArquivo"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.LayoutArquivo.TcsArquivo", "Linx.Framework.BV.LayoutArquivo.TcsArquivoItem"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsArquivoItem" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.LayoutArquivo",
	        			ParentClassName = "TcsArquivo",	
	        			DisplayName = "Elementos",
	        			ClearMethodName = "ClearTcsArquivoItem" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsArquivoItem" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsArquivoItem" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.LayoutArquivo.TcsArquivoItem"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.LayoutArquivo.TcsArquivoItem" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.LayoutArquivo.TcsArquivo", "Linx.Framework.BV.LayoutArquivo.TcsArquivoItemCampo"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsArquivoItemCampo" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.LayoutArquivo",
	        			ParentClassName = "TcsArquivoItem",	
	        			DisplayName = "Campos",
	        			ClearMethodName = "ClearTcsArquivoItemCampo" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsArquivoItemCampo" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsArquivoItemCampo" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.LayoutArquivo.TcsArquivoItemCampo"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.LayoutArquivo.TcsArquivoItemCampo" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.LayoutArquivo.TcsArquivo", "Linx.Framework.BV.LayoutArquivo.TcsArquivoLog"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsArquivoLog" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.LayoutArquivo",
	        			ParentClassName = "TcsArquivo",	
	        			DisplayName = "Logs",
	        			ClearMethodName = "ClearTcsArquivoLog" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsArquivoLog" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsArquivoLog" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.LayoutArquivo.TcsArquivoLog"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.LayoutArquivo.TcsArquivoLog" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.LayoutArquivo.TcsArquivo", "Linx.Framework.BV.LayoutArquivo.TcsArquivoGrupoVinculo"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsArquivoGrupoVinculo" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.LayoutArquivo",
	        			ParentClassName = "TcsArquivo",	
	        			DisplayName = "TcsArquivoGrupoVinculo",
	        			ClearMethodName = "ClearTcsArquivoGrupoVinculo" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsArquivoGrupoVinculo" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsArquivoGrupoVinculo" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.LayoutArquivo.TcsArquivoGrupoVinculo"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.LayoutArquivo.TcsArquivoGrupoVinculo" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.LayoutArquivo.TcsArquivoGrupo"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsArquivoGrupo",
	        			NameSpace = "Linx.Framework.BV.LayoutArquivo",
	        			ParentClassName = null,	
	        			DisplayName = "TcsArquivoGrupo",
	        			ClearMethodName = "ClearTcsArquivoGrupo",
	        			QueryMethodName  = "GetPagedTcsArquivoGrupo",	
	        			CountingMethodName  = "GetTcsArquivoGrupo" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.LayoutArquivo.TcsArquivoGrupo"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.LayoutArquivo.TcsArquivoGrupo"), forceAll: forceAll)
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


             return new string[] { "Framework_layoutArquivoService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.layoutArquivoService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	

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
			
	        result[0].TcsArquivoItemList = new List<TcsArquivoItem>();
	        ((List<TcsArquivoItem>)result[0].TcsArquivoItemList).Add(new TcsArquivoItem());
			
	        ((List<TcsArquivoItem>)result[0].TcsArquivoItemList)[0].TcsArquivoItemCampoList = new List<TcsArquivoItemCampo>();
	        ((List<TcsArquivoItemCampo>)((List<TcsArquivoItem>)result[0].TcsArquivoItemList)[0].TcsArquivoItemCampoList).Add(new TcsArquivoItemCampo());
			
	        result[0].TcsArquivoLogList = new List<TcsArquivoLog>();
	        ((List<TcsArquivoLog>)result[0].TcsArquivoLogList).Add(new TcsArquivoLog());
			
	        result[0].TcsArquivoGrupoVinculoList = new List<TcsArquivoGrupoVinculo>();
	        ((List<TcsArquivoGrupoVinculo>)result[0].TcsArquivoGrupoVinculoList).Add(new TcsArquivoGrupoVinculo());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsArquivoItem.
	    public IEnumerable<TcsArquivoItem> ClearTcsArquivoItem()
	    {
	        List<TcsArquivoItem> result = new List<TcsArquivoItem>();
	        result.Add(new TcsArquivoItem());	
			
	        result[0].TcsArquivoItemCampoList = new List<TcsArquivoItemCampo>();
	        ((List<TcsArquivoItemCampo>)result[0].TcsArquivoItemCampoList).Add(new TcsArquivoItemCampo());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsArquivoItemCampo.
	    public IEnumerable<TcsArquivoItemCampo> ClearTcsArquivoItemCampo()
	    {
	        List<TcsArquivoItemCampo> result = new List<TcsArquivoItemCampo>();
	        result.Add(new TcsArquivoItemCampo());	
		
	        

	
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
		
	
	    [Ignore]
	    //Clear TcsArquivoGrupoVinculo.
	    public IEnumerable<TcsArquivoGrupoVinculo> ClearTcsArquivoGrupoVinculo()
	    {
	        List<TcsArquivoGrupoVinculo> result = new List<TcsArquivoGrupoVinculo>();
	        result.Add(new TcsArquivoGrupoVinculo());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsArquivoGrupo.
	    public IEnumerable<TcsArquivoGrupo> ClearTcsArquivoGrupo()
	    {
	        List<TcsArquivoGrupo> result = new List<TcsArquivoGrupo>();
	        result.Add(new TcsArquivoGrupo());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    
	    [Ignore]
	    //Get TcsArquivo.
	    public IQueryable<TcsArquivo> GetTcsArquivo()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsArquivo> result = 
	            (from entity0 in this.DbContext.TCS_ARQUIVO
	            
	            	
	            select new TcsArquivo()		
	            {
	            
                ArquivoDll = entity0.ARQUIVO_DLL
                , CaminhoArquivo = entity0.CAMINHO_ARQUIVO
                , Classe = entity0.CLASSE
                , CodArquivo = entity0.COD_ARQUIVO
                , Delimitador = entity0.DELIMITADOR
                , DescArquivo = entity0.DESC_ARQUIVO
                , DetalheArquivo = entity0.DETALHE_ARQUIVO
                , IdArquivo = entity0.ID_ARQUIVO
                , Inativo = entity0.INATIVO
                , LxFormatoData = entity0.LX_FORMATO_DATA
                , LxFormatoDataName = ((entity0.LX_FORMATO_DATA) == "1" ? "AAAAMMDD" : ((entity0.LX_FORMATO_DATA) == "4" ? "AAMMDD" : ((entity0.LX_FORMATO_DATA) == "5" ? "DDMMAA" : ((entity0.LX_FORMATO_DATA) == "2" ? "DDMMAAAA" : ((entity0.LX_FORMATO_DATA) == "6" ? "MMDDAA" : ((entity0.LX_FORMATO_DATA) == "3" ? "MMDDAAAA" : ""))))))
                , LxTipoArquivo = entity0.LX_TIPO_ARQUIVO
                , LxTipoArquivoName = ((entity0.LX_TIPO_ARQUIVO) == "E" ? "Excel" : ((entity0.LX_TIPO_ARQUIVO) == "T" ? "Text" : ((entity0.LX_TIPO_ARQUIVO) == "G" ? "Todos" : ((entity0.LX_TIPO_ARQUIVO) == "X" ? "XML" : ""))))
                , Metodo = entity0.METODO
                , NomeArquivo = entity0.NOME_ARQUIVO
                , TagMestre = entity0.TAG_MESTRE
                , Xmlns = entity0.XMLNS
                , Xsd = entity0.XSD
			
                ,TcsArquivoItemList = 
	                        (from entity1 in entity0.TCS_ARQUIVO_ITEM_LISTA
                                  let entity1Al1 = entity1.ARQUIVO_ITEM_PAI
                                orderby entity1.ORDEM ascending
	                        
	                        	
	                        select new TcsArquivoItem()
	                        {
	                        
                                IdArquivoFk = entity1.ID_ARQUIVO_FK
                                , IdArquivoItemPai = entity1Al1.ID_ARQUIVO_ITEM
                                , IdArquivoItem = entity1.ID_ARQUIVO_ITEM
                                , IndicaNotnull = entity1.INDICA_NOTNULL
                                , Ordem = entity1.ORDEM
                                , TagItemPai = entity1Al1.TAG_ITEM
                                , TagItem = entity1.TAG_ITEM
                                , Xmlns = entity1.XMLNS
			
                                ,TcsArquivoItemCampoList = 
	                                                (from entity2 in entity1.TCS_ARQUIVO_ITEM_CAMPO_LISTA
                                                                orderby entity2.ORDEM ascending
	                                                
	                                                	
	                                                select new TcsArquivoItemCampo()
	                                                {
	                                                
                                                                ChaveIdentificacao = entity2.CHAVE_IDENTIFICACAO
                                                                , Decimais = entity2.DECIMAIS
                                                                , IdArquivoItemCampo = entity2.ID_ARQUIVO_ITEM_CAMPO
                                                                , IdArquivoItemFk = entity2.ID_ARQUIVO_ITEM_FK
                                                                , IndicaNotnull = entity2.INDICA_NOTNULL
                                                                , IndicaPk = entity2.INDICA_PK
                                                                , LxFormatoData = entity2.LX_FORMATO_DATA
                                                                , LxFormatoDataName = ((entity2.LX_FORMATO_DATA) == "1" ? "AAAAMMDD" : ((entity2.LX_FORMATO_DATA) == "4" ? "AAMMDD" : ((entity2.LX_FORMATO_DATA) == "5" ? "DDMMAA" : ((entity2.LX_FORMATO_DATA) == "2" ? "DDMMAAAA" : ((entity2.LX_FORMATO_DATA) == "6" ? "MMDDAA" : ((entity2.LX_FORMATO_DATA) == "3" ? "MMDDAAAA" : ""))))))
                                                                , LxTipoDado = entity2.LX_TIPO_DADO
                                                                , LxTipoDadoName = ((entity2.LX_TIPO_DADO) == "BLN" ? "Boolean" : ((entity2.LX_TIPO_DADO) == "BYT" ? "Byte" : ((entity2.LX_TIPO_DADO) == "DTE" ? "Date" : ((entity2.LX_TIPO_DADO) == "DEC" ? "Decimal" : ((entity2.LX_TIPO_DADO) == "DBL" ? "Double" : ((entity2.LX_TIPO_DADO) == "INT" ? "Integer" : ((entity2.LX_TIPO_DADO) == "LNG" ? "Long" : ((entity2.LX_TIPO_DADO) == "POS" ? "PositiveInteger" : ((entity2.LX_TIPO_DADO) == "STR" ? "String" : ((entity2.LX_TIPO_DADO) == "TME" ? "Time" : ""))))))))))
                                                                , Ordem = entity2.ORDEM
                                                                , TagCampo = entity2.TAG_CAMPO
                                                                , Tamanho = entity2.TAMANHO
		
	                                                }
	                                                )
		
	                        }
	                        )
			
                ,TcsArquivoLogList = 
	                        (from entity1 in entity0.TCS_ARQUIVO_LOG_LISTA
                                orderby entity1.ID_ARQUIVO_LOG ascending
	                        
	                        	
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
			
                ,TcsArquivoGrupoVinculoList = 
	                        (from entity1 in entity0.TCS_ARQUIVO_GRUPO_VINCULO_LISTA
                                  let entity1Al1 = entity1.TCS_ARQUIVO_GRUPO
	                        
	                        	
	                        select new TcsArquivoGrupoVinculo()
	                        {
	                        
                                CodArquivoGrupo = entity1Al1.COD_ARQUIVO_GRUPO
                                , DescArquivoGrupo = entity1Al1.DESC_ARQUIVO_GRUPO
                                , IdArquivo = entity1.ID_ARQUIVO
                                , IdArquivoGrupo = entity1Al1.ID_ARQUIVO_GRUPO
                                , Inativo = entity1.INATIVO
                                , Ordem = entity1.ORDEM
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsArquivoItem.
	    public IQueryable<TcsArquivoItem> GetTcsArquivoItem()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsArquivoItem> result = 
	            (from entity0 in this.DbContext.TCS_ARQUIVO_ITEM
                  let entity0Al1 = entity0.ARQUIVO_ITEM_PAI
                orderby entity0.ORDEM ascending
	            
	            	
	            select new TcsArquivoItem()		
	            {
	            
                IdArquivoFk = entity0.ID_ARQUIVO_FK
                , IdArquivoItemPai = entity0Al1.ID_ARQUIVO_ITEM
                , IdArquivoItem = entity0.ID_ARQUIVO_ITEM
                , IndicaNotnull = entity0.INDICA_NOTNULL
                , Ordem = entity0.ORDEM
                , TagItemPai = entity0Al1.TAG_ITEM
                , TagItem = entity0.TAG_ITEM
                , Xmlns = entity0.XMLNS
			
                ,TcsArquivoItemCampoList = 
	                        (from entity1 in entity0.TCS_ARQUIVO_ITEM_CAMPO_LISTA
                                orderby entity1.ORDEM ascending
	                        
	                        	
	                        select new TcsArquivoItemCampo()
	                        {
	                        
                                ChaveIdentificacao = entity1.CHAVE_IDENTIFICACAO
                                , Decimais = entity1.DECIMAIS
                                , IdArquivoItemCampo = entity1.ID_ARQUIVO_ITEM_CAMPO
                                , IdArquivoItemFk = entity1.ID_ARQUIVO_ITEM_FK
                                , IndicaNotnull = entity1.INDICA_NOTNULL
                                , IndicaPk = entity1.INDICA_PK
                                , LxFormatoData = entity1.LX_FORMATO_DATA
                                , LxFormatoDataName = ((entity1.LX_FORMATO_DATA) == "1" ? "AAAAMMDD" : ((entity1.LX_FORMATO_DATA) == "4" ? "AAMMDD" : ((entity1.LX_FORMATO_DATA) == "5" ? "DDMMAA" : ((entity1.LX_FORMATO_DATA) == "2" ? "DDMMAAAA" : ((entity1.LX_FORMATO_DATA) == "6" ? "MMDDAA" : ((entity1.LX_FORMATO_DATA) == "3" ? "MMDDAAAA" : ""))))))
                                , LxTipoDado = entity1.LX_TIPO_DADO
                                , LxTipoDadoName = ((entity1.LX_TIPO_DADO) == "BLN" ? "Boolean" : ((entity1.LX_TIPO_DADO) == "BYT" ? "Byte" : ((entity1.LX_TIPO_DADO) == "DTE" ? "Date" : ((entity1.LX_TIPO_DADO) == "DEC" ? "Decimal" : ((entity1.LX_TIPO_DADO) == "DBL" ? "Double" : ((entity1.LX_TIPO_DADO) == "INT" ? "Integer" : ((entity1.LX_TIPO_DADO) == "LNG" ? "Long" : ((entity1.LX_TIPO_DADO) == "POS" ? "PositiveInteger" : ((entity1.LX_TIPO_DADO) == "STR" ? "String" : ((entity1.LX_TIPO_DADO) == "TME" ? "Time" : ""))))))))))
                                , Ordem = entity1.ORDEM
                                , TagCampo = entity1.TAG_CAMPO
                                , Tamanho = entity1.TAMANHO
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsArquivoItemCampo.
	    public IQueryable<TcsArquivoItemCampo> GetTcsArquivoItemCampo()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsArquivoItemCampo> result = 
	            (from entity0 in this.DbContext.TCS_ARQUIVO_ITEM_CAMPO
                orderby entity0.ORDEM ascending
	            
	            	
	            select new TcsArquivoItemCampo()		
	            {
	            
                ChaveIdentificacao = entity0.CHAVE_IDENTIFICACAO
                , Decimais = entity0.DECIMAIS
                , IdArquivoItemCampo = entity0.ID_ARQUIVO_ITEM_CAMPO
                , IdArquivoItemFk = entity0.ID_ARQUIVO_ITEM_FK
                , IndicaNotnull = entity0.INDICA_NOTNULL
                , IndicaPk = entity0.INDICA_PK
                , LxFormatoData = entity0.LX_FORMATO_DATA
                , LxFormatoDataName = ((entity0.LX_FORMATO_DATA) == "1" ? "AAAAMMDD" : ((entity0.LX_FORMATO_DATA) == "4" ? "AAMMDD" : ((entity0.LX_FORMATO_DATA) == "5" ? "DDMMAA" : ((entity0.LX_FORMATO_DATA) == "2" ? "DDMMAAAA" : ((entity0.LX_FORMATO_DATA) == "6" ? "MMDDAA" : ((entity0.LX_FORMATO_DATA) == "3" ? "MMDDAAAA" : ""))))))
                , LxTipoDado = entity0.LX_TIPO_DADO
                , LxTipoDadoName = ((entity0.LX_TIPO_DADO) == "BLN" ? "Boolean" : ((entity0.LX_TIPO_DADO) == "BYT" ? "Byte" : ((entity0.LX_TIPO_DADO) == "DTE" ? "Date" : ((entity0.LX_TIPO_DADO) == "DEC" ? "Decimal" : ((entity0.LX_TIPO_DADO) == "DBL" ? "Double" : ((entity0.LX_TIPO_DADO) == "INT" ? "Integer" : ((entity0.LX_TIPO_DADO) == "LNG" ? "Long" : ((entity0.LX_TIPO_DADO) == "POS" ? "PositiveInteger" : ((entity0.LX_TIPO_DADO) == "STR" ? "String" : ((entity0.LX_TIPO_DADO) == "TME" ? "Time" : ""))))))))))
                , Ordem = entity0.ORDEM
                , TagCampo = entity0.TAG_CAMPO
                , Tamanho = entity0.TAMANHO
		
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
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsArquivoGrupoVinculo.
	    public IQueryable<TcsArquivoGrupoVinculo> GetTcsArquivoGrupoVinculo()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsArquivoGrupoVinculo> result = 
	            (from entity0 in this.DbContext.TCS_ARQUIVO_GRUPO_VINCULO
                  let entity0Al1 = entity0.TCS_ARQUIVO_GRUPO
	            
	            	
	            select new TcsArquivoGrupoVinculo()		
	            {
	            
                CodArquivoGrupo = entity0Al1.COD_ARQUIVO_GRUPO
                , DescArquivoGrupo = entity0Al1.DESC_ARQUIVO_GRUPO
                , IdArquivo = entity0.ID_ARQUIVO
                , IdArquivoGrupo = entity0Al1.ID_ARQUIVO_GRUPO
                , Inativo = entity0.INATIVO
                , Ordem = entity0.ORDEM
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsArquivoNoAssociations.
	    public IQueryable<TcsArquivo> GetTcsArquivoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsArquivo> result = 
	            (from entity0 in this.DbContext.TCS_ARQUIVO
	            
	            	
	            select new TcsArquivo()		
	            {
	            
                ArquivoDll = entity0.ARQUIVO_DLL
                , CaminhoArquivo = entity0.CAMINHO_ARQUIVO
                , Classe = entity0.CLASSE
                , CodArquivo = entity0.COD_ARQUIVO
                , Delimitador = entity0.DELIMITADOR
                , DescArquivo = entity0.DESC_ARQUIVO
                , DetalheArquivo = entity0.DETALHE_ARQUIVO
                , IdArquivo = entity0.ID_ARQUIVO
                , Inativo = entity0.INATIVO
                , LxFormatoData = entity0.LX_FORMATO_DATA
                , LxFormatoDataName = ((entity0.LX_FORMATO_DATA) == "1" ? "AAAAMMDD" : ((entity0.LX_FORMATO_DATA) == "4" ? "AAMMDD" : ((entity0.LX_FORMATO_DATA) == "5" ? "DDMMAA" : ((entity0.LX_FORMATO_DATA) == "2" ? "DDMMAAAA" : ((entity0.LX_FORMATO_DATA) == "6" ? "MMDDAA" : ((entity0.LX_FORMATO_DATA) == "3" ? "MMDDAAAA" : ""))))))
                , LxTipoArquivo = entity0.LX_TIPO_ARQUIVO
                , LxTipoArquivoName = ((entity0.LX_TIPO_ARQUIVO) == "E" ? "Excel" : ((entity0.LX_TIPO_ARQUIVO) == "T" ? "Text" : ((entity0.LX_TIPO_ARQUIVO) == "G" ? "Todos" : ((entity0.LX_TIPO_ARQUIVO) == "X" ? "XML" : ""))))
                , Metodo = entity0.METODO
                , NomeArquivo = entity0.NOME_ARQUIVO
                , TagMestre = entity0.TAG_MESTRE
                , Xmlns = entity0.XMLNS
                , Xsd = entity0.XSD
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsArquivoItemNoAssociations.
	    public IQueryable<TcsArquivoItem> GetTcsArquivoItemNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsArquivoItem> result = 
	            (from entity0 in this.DbContext.TCS_ARQUIVO_ITEM
                  let entity0Al1 = entity0.ARQUIVO_ITEM_PAI
                orderby entity0.ORDEM ascending
	            
	            	
	            select new TcsArquivoItem()		
	            {
	            
                IdArquivoFk = entity0.ID_ARQUIVO_FK
                , IdArquivoItemPai = entity0Al1.ID_ARQUIVO_ITEM
                , IdArquivoItem = entity0.ID_ARQUIVO_ITEM
                , IndicaNotnull = entity0.INDICA_NOTNULL
                , Ordem = entity0.ORDEM
                , TagItemPai = entity0Al1.TAG_ITEM
                , TagItem = entity0.TAG_ITEM
                , Xmlns = entity0.XMLNS
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsArquivoItemCampoNoAssociations.
	    public IQueryable<TcsArquivoItemCampo> GetTcsArquivoItemCampoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsArquivoItemCampo> result = 
	            (from entity0 in this.DbContext.TCS_ARQUIVO_ITEM_CAMPO
                orderby entity0.ORDEM ascending
	            
	            	
	            select new TcsArquivoItemCampo()		
	            {
	            
                ChaveIdentificacao = entity0.CHAVE_IDENTIFICACAO
                , Decimais = entity0.DECIMAIS
                , IdArquivoItemCampo = entity0.ID_ARQUIVO_ITEM_CAMPO
                , IdArquivoItemFk = entity0.ID_ARQUIVO_ITEM_FK
                , IndicaNotnull = entity0.INDICA_NOTNULL
                , IndicaPk = entity0.INDICA_PK
                , LxFormatoData = entity0.LX_FORMATO_DATA
                , LxFormatoDataName = ((entity0.LX_FORMATO_DATA) == "1" ? "AAAAMMDD" : ((entity0.LX_FORMATO_DATA) == "4" ? "AAMMDD" : ((entity0.LX_FORMATO_DATA) == "5" ? "DDMMAA" : ((entity0.LX_FORMATO_DATA) == "2" ? "DDMMAAAA" : ((entity0.LX_FORMATO_DATA) == "6" ? "MMDDAA" : ((entity0.LX_FORMATO_DATA) == "3" ? "MMDDAAAA" : ""))))))
                , LxTipoDado = entity0.LX_TIPO_DADO
                , LxTipoDadoName = ((entity0.LX_TIPO_DADO) == "BLN" ? "Boolean" : ((entity0.LX_TIPO_DADO) == "BYT" ? "Byte" : ((entity0.LX_TIPO_DADO) == "DTE" ? "Date" : ((entity0.LX_TIPO_DADO) == "DEC" ? "Decimal" : ((entity0.LX_TIPO_DADO) == "DBL" ? "Double" : ((entity0.LX_TIPO_DADO) == "INT" ? "Integer" : ((entity0.LX_TIPO_DADO) == "LNG" ? "Long" : ((entity0.LX_TIPO_DADO) == "POS" ? "PositiveInteger" : ((entity0.LX_TIPO_DADO) == "STR" ? "String" : ((entity0.LX_TIPO_DADO) == "TME" ? "Time" : ""))))))))))
                , Ordem = entity0.ORDEM
                , TagCampo = entity0.TAG_CAMPO
                , Tamanho = entity0.TAMANHO
		
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
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsArquivoGrupoVinculoNoAssociations.
	    public IQueryable<TcsArquivoGrupoVinculo> GetTcsArquivoGrupoVinculoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsArquivoGrupoVinculo> result = 
	            (from entity0 in this.DbContext.TCS_ARQUIVO_GRUPO_VINCULO
                  let entity0Al1 = entity0.TCS_ARQUIVO_GRUPO
	            
	            	
	            select new TcsArquivoGrupoVinculo()		
	            {
	            
                CodArquivoGrupo = entity0Al1.COD_ARQUIVO_GRUPO
                , DescArquivoGrupo = entity0Al1.DESC_ARQUIVO_GRUPO
                , IdArquivo = entity0.ID_ARQUIVO
                , IdArquivoGrupo = entity0Al1.ID_ARQUIVO_GRUPO
                , Inativo = entity0.INATIVO
                , Ordem = entity0.ORDEM
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsArquivoGrupo.
	    public IQueryable<TcsArquivoGrupo> GetTcsArquivoGrupo()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsArquivoGrupo> result = 
	            (from entity0 in this.DbContext.TCS_ARQUIVO_GRUPO
	            
	            	
	            select new TcsArquivoGrupo()		
	            {
	            
                CodArquivoGrupo = entity0.COD_ARQUIVO_GRUPO
                , DescArquivoGrupo = entity0.DESC_ARQUIVO_GRUPO
                , IdArquivoGrupo = entity0.ID_ARQUIVO_GRUPO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsArquivoGrupoNoAssociations.
	    public IQueryable<TcsArquivoGrupo> GetTcsArquivoGrupoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsArquivoGrupo> result = 
	            (from entity0 in this.DbContext.TCS_ARQUIVO_GRUPO
	            
	            	
	            select new TcsArquivoGrupo()		
	            {
	            
                CodArquivoGrupo = entity0.COD_ARQUIVO_GRUPO
                , DescArquivoGrupo = entity0.DESC_ARQUIVO_GRUPO
                , IdArquivoGrupo = entity0.ID_ARQUIVO_GRUPO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	//Add filtering disabled property for TCS_ARQUIVO
	    	string[] bmDisabledTcsArquivoList = this.GetEDM().GetFilteringDisabledList("TCS_ARQUIVO");
	    	if (bmDisabledTcsArquivoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsArquivoList.Contains("TCS_ARQUIVO.ARQUIVO_DLL"))
	    		{
	    			result.Add("TcsArquivo|ArquivoDll");
	    			result.Add("TcsArquivo|TCS_ARQUIVO.ARQUIVO_DLL");
	    		}
	
	    		if (bmDisabledTcsArquivoList.Contains("TCS_ARQUIVO.CAMINHO_ARQUIVO"))
	    		{
	    			result.Add("TcsArquivo|CaminhoArquivo");
	    			result.Add("TcsArquivo|TCS_ARQUIVO.CAMINHO_ARQUIVO");
	    		}
	
	    		if (bmDisabledTcsArquivoList.Contains("TCS_ARQUIVO.CLASSE"))
	    		{
	    			result.Add("TcsArquivo|Classe");
	    			result.Add("TcsArquivo|TCS_ARQUIVO.CLASSE");
	    		}
	
	    		if (bmDisabledTcsArquivoList.Contains("TCS_ARQUIVO.COD_ARQUIVO"))
	    		{
	    			result.Add("TcsArquivo|CodArquivo");
	    			result.Add("TcsArquivo|TCS_ARQUIVO.COD_ARQUIVO");
	    		}
	
	    		if (bmDisabledTcsArquivoList.Contains("TCS_ARQUIVO.DELIMITADOR"))
	    		{
	    			result.Add("TcsArquivo|Delimitador");
	    			result.Add("TcsArquivo|TCS_ARQUIVO.DELIMITADOR");
	    		}
	
	    		if (bmDisabledTcsArquivoList.Contains("TCS_ARQUIVO.DESC_ARQUIVO"))
	    		{
	    			result.Add("TcsArquivo|DescArquivo");
	    			result.Add("TcsArquivo|TCS_ARQUIVO.DESC_ARQUIVO");
	    		}
	
	    		if (bmDisabledTcsArquivoList.Contains("TCS_ARQUIVO.DETALHE_ARQUIVO"))
	    		{
	    			result.Add("TcsArquivo|DetalheArquivo");
	    			result.Add("TcsArquivo|TCS_ARQUIVO.DETALHE_ARQUIVO");
	    		}
	
	    		if (bmDisabledTcsArquivoList.Contains("TCS_ARQUIVO.ID_ARQUIVO"))
	    		{
	    			result.Add("TcsArquivo|IdArquivo");
	    			result.Add("TcsArquivo|TCS_ARQUIVO.ID_ARQUIVO");
	    		}
	
	    		if (bmDisabledTcsArquivoList.Contains("TCS_ARQUIVO.INATIVO"))
	    		{
	    			result.Add("TcsArquivo|Inativo");
	    			result.Add("TcsArquivo|TCS_ARQUIVO.INATIVO");
	    		}
	
	    		if (bmDisabledTcsArquivoList.Contains("TCS_ARQUIVO.LX_FORMATO_DATA"))
	    		{
	    			result.Add("TcsArquivo|LxFormatoData");
	    			result.Add("TcsArquivo|TCS_ARQUIVO.LX_FORMATO_DATA");
	    		}
	
	    		if (bmDisabledTcsArquivoList.Contains("TCS_ARQUIVO.LX_TIPO_ARQUIVO"))
	    		{
	    			result.Add("TcsArquivo|LxTipoArquivo");
	    			result.Add("TcsArquivo|TCS_ARQUIVO.LX_TIPO_ARQUIVO");
	    		}
	
	    		if (bmDisabledTcsArquivoList.Contains("TCS_ARQUIVO.METODO"))
	    		{
	    			result.Add("TcsArquivo|Metodo");
	    			result.Add("TcsArquivo|TCS_ARQUIVO.METODO");
	    		}
	
	    		if (bmDisabledTcsArquivoList.Contains("TCS_ARQUIVO.NOME_ARQUIVO"))
	    		{
	    			result.Add("TcsArquivo|NomeArquivo");
	    			result.Add("TcsArquivo|TCS_ARQUIVO.NOME_ARQUIVO");
	    		}
	
	    		if (bmDisabledTcsArquivoList.Contains("TCS_ARQUIVO.TAG_MESTRE"))
	    		{
	    			result.Add("TcsArquivo|TagMestre");
	    			result.Add("TcsArquivo|TCS_ARQUIVO.TAG_MESTRE");
	    		}
	
	    		if (bmDisabledTcsArquivoList.Contains("TCS_ARQUIVO.XMLNS"))
	    		{
	    			result.Add("TcsArquivo|Xmlns");
	    			result.Add("TcsArquivo|TCS_ARQUIVO.XMLNS");
	    		}
	
	    		if (bmDisabledTcsArquivoList.Contains("TCS_ARQUIVO.XSD"))
	    		{
	    			result.Add("TcsArquivo|Xsd");
	    			result.Add("TcsArquivo|TCS_ARQUIVO.XSD");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_ARQUIVO_ITEM
	    	string[] bmDisabledTcsArquivoItemList = this.GetEDM().GetFilteringDisabledList("TCS_ARQUIVO_ITEM");
	    	if (bmDisabledTcsArquivoItemList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsArquivoItemList.Contains("TCS_ARQUIVO_ITEM.ID_ARQUIVO_FK"))
	    		{
	    			result.Add("TcsArquivoItem|IdArquivoFk");
	    			result.Add("TcsArquivoItem|TCS_ARQUIVO_ITEM.ID_ARQUIVO_FK");
	    		}
	
	    		if (bmDisabledTcsArquivoItemList.Contains("TCS_ARQUIVO_ITEM.ID_ARQUIVO_ITEM"))
	    		{
	    			result.Add("TcsArquivoItem|IdArquivoItem");
	    			result.Add("TcsArquivoItem|TCS_ARQUIVO_ITEM.ID_ARQUIVO_ITEM");
	    		}
	
	    		if (bmDisabledTcsArquivoItemList.Contains("TCS_ARQUIVO_ITEM.INDICA_NOTNULL"))
	    		{
	    			result.Add("TcsArquivoItem|IndicaNotnull");
	    			result.Add("TcsArquivoItem|TCS_ARQUIVO_ITEM.INDICA_NOTNULL");
	    		}
	
	    		if (bmDisabledTcsArquivoItemList.Contains("TCS_ARQUIVO_ITEM.ORDEM"))
	    		{
	    			result.Add("TcsArquivoItem|Ordem");
	    			result.Add("TcsArquivoItem|TCS_ARQUIVO_ITEM.ORDEM");
	    		}
	
	    		if (bmDisabledTcsArquivoItemList.Contains("TCS_ARQUIVO_ITEM.TAG_ITEM"))
	    		{
	    			result.Add("TcsArquivoItem|TagItem");
	    			result.Add("TcsArquivoItem|TCS_ARQUIVO_ITEM.TAG_ITEM");
	    		}
	
	    		if (bmDisabledTcsArquivoItemList.Contains("TCS_ARQUIVO_ITEM.XMLNS"))
	    		{
	    			result.Add("TcsArquivoItem|Xmlns");
	    			result.Add("TcsArquivoItem|TCS_ARQUIVO_ITEM.XMLNS");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_ARQUIVO_ITEM_CAMPO
	    	string[] bmDisabledTcsArquivoItemCampoList = this.GetEDM().GetFilteringDisabledList("TCS_ARQUIVO_ITEM_CAMPO");
	    	if (bmDisabledTcsArquivoItemCampoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsArquivoItemCampoList.Contains("TCS_ARQUIVO_ITEM_CAMPO.CHAVE_IDENTIFICACAO"))
	    		{
	    			result.Add("TcsArquivoItemCampo|ChaveIdentificacao");
	    			result.Add("TcsArquivoItemCampo|TCS_ARQUIVO_ITEM_CAMPO.CHAVE_IDENTIFICACAO");
	    		}
	
	    		if (bmDisabledTcsArquivoItemCampoList.Contains("TCS_ARQUIVO_ITEM_CAMPO.DECIMAIS"))
	    		{
	    			result.Add("TcsArquivoItemCampo|Decimais");
	    			result.Add("TcsArquivoItemCampo|TCS_ARQUIVO_ITEM_CAMPO.DECIMAIS");
	    		}
	
	    		if (bmDisabledTcsArquivoItemCampoList.Contains("TCS_ARQUIVO_ITEM_CAMPO.ID_ARQUIVO_ITEM_CAMPO"))
	    		{
	    			result.Add("TcsArquivoItemCampo|IdArquivoItemCampo");
	    			result.Add("TcsArquivoItemCampo|TCS_ARQUIVO_ITEM_CAMPO.ID_ARQUIVO_ITEM_CAMPO");
	    		}
	
	    		if (bmDisabledTcsArquivoItemCampoList.Contains("TCS_ARQUIVO_ITEM_CAMPO.ID_ARQUIVO_ITEM_FK"))
	    		{
	    			result.Add("TcsArquivoItemCampo|IdArquivoItemFk");
	    			result.Add("TcsArquivoItemCampo|TCS_ARQUIVO_ITEM_CAMPO.ID_ARQUIVO_ITEM_FK");
	    		}
	
	    		if (bmDisabledTcsArquivoItemCampoList.Contains("TCS_ARQUIVO_ITEM_CAMPO.INDICA_NOTNULL"))
	    		{
	    			result.Add("TcsArquivoItemCampo|IndicaNotnull");
	    			result.Add("TcsArquivoItemCampo|TCS_ARQUIVO_ITEM_CAMPO.INDICA_NOTNULL");
	    		}
	
	    		if (bmDisabledTcsArquivoItemCampoList.Contains("TCS_ARQUIVO_ITEM_CAMPO.INDICA_PK"))
	    		{
	    			result.Add("TcsArquivoItemCampo|IndicaPk");
	    			result.Add("TcsArquivoItemCampo|TCS_ARQUIVO_ITEM_CAMPO.INDICA_PK");
	    		}
	
	    		if (bmDisabledTcsArquivoItemCampoList.Contains("TCS_ARQUIVO_ITEM_CAMPO.LX_FORMATO_DATA"))
	    		{
	    			result.Add("TcsArquivoItemCampo|LxFormatoData");
	    			result.Add("TcsArquivoItemCampo|TCS_ARQUIVO_ITEM_CAMPO.LX_FORMATO_DATA");
	    		}
	
	    		if (bmDisabledTcsArquivoItemCampoList.Contains("TCS_ARQUIVO_ITEM_CAMPO.LX_TIPO_DADO"))
	    		{
	    			result.Add("TcsArquivoItemCampo|LxTipoDado");
	    			result.Add("TcsArquivoItemCampo|TCS_ARQUIVO_ITEM_CAMPO.LX_TIPO_DADO");
	    		}
	
	    		if (bmDisabledTcsArquivoItemCampoList.Contains("TCS_ARQUIVO_ITEM_CAMPO.ORDEM"))
	    		{
	    			result.Add("TcsArquivoItemCampo|Ordem");
	    			result.Add("TcsArquivoItemCampo|TCS_ARQUIVO_ITEM_CAMPO.ORDEM");
	    		}
	
	    		if (bmDisabledTcsArquivoItemCampoList.Contains("TCS_ARQUIVO_ITEM_CAMPO.TAG_CAMPO"))
	    		{
	    			result.Add("TcsArquivoItemCampo|TagCampo");
	    			result.Add("TcsArquivoItemCampo|TCS_ARQUIVO_ITEM_CAMPO.TAG_CAMPO");
	    		}
	
	    		if (bmDisabledTcsArquivoItemCampoList.Contains("TCS_ARQUIVO_ITEM_CAMPO.TAMANHO"))
	    		{
	    			result.Add("TcsArquivoItemCampo|Tamanho");
	    			result.Add("TcsArquivoItemCampo|TCS_ARQUIVO_ITEM_CAMPO.TAMANHO");
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
	    	//Add filtering disabled property for TCS_ARQUIVO_GRUPO_VINCULO
	    	string[] bmDisabledTcsArquivoGrupoVinculoList = this.GetEDM().GetFilteringDisabledList("TCS_ARQUIVO_GRUPO_VINCULO");
	    	if (bmDisabledTcsArquivoGrupoVinculoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsArquivoGrupoVinculoList.Contains("TCS_ARQUIVO_GRUPO_VINCULO.ID_ARQUIVO"))
	    		{
	    			result.Add("TcsArquivoGrupoVinculo|IdArquivo");
	    			result.Add("TcsArquivoGrupoVinculo|TCS_ARQUIVO_GRUPO_VINCULO.ID_ARQUIVO");
	    		}
	
	    		if (bmDisabledTcsArquivoGrupoVinculoList.Contains("TCS_ARQUIVO_GRUPO_VINCULO.INATIVO"))
	    		{
	    			result.Add("TcsArquivoGrupoVinculo|Inativo");
	    			result.Add("TcsArquivoGrupoVinculo|TCS_ARQUIVO_GRUPO_VINCULO.INATIVO");
	    		}
	
	    		if (bmDisabledTcsArquivoGrupoVinculoList.Contains("TCS_ARQUIVO_GRUPO_VINCULO.ORDEM"))
	    		{
	    			result.Add("TcsArquivoGrupoVinculo|Ordem");
	    			result.Add("TcsArquivoGrupoVinculo|TCS_ARQUIVO_GRUPO_VINCULO.ORDEM");
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
	    //Get TcsArquivoItem By EntitySearchId.
	    public IQueryable<TcsArquivoItem> GetTcsArquivoItemByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsArquivoItemByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get TcsArquivoItemCampo By EntitySearchId.
	    public IQueryable<TcsArquivoItemCampo> GetTcsArquivoItemCampoByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsArquivoItemCampoByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get TcsArquivoLog By EntitySearchId.
	    public IQueryable<TcsArquivoLog> GetTcsArquivoLogByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsArquivoLogByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get TcsArquivoGrupoVinculo By EntitySearchId.
	    public IQueryable<TcsArquivoGrupoVinculo> GetTcsArquivoGrupoVinculoByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsArquivoGrupoVinculoByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get TcsArquivo By EntitySearchId.
	    public IQueryable<TcsArquivo> GetTcsArquivoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsArquivoByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get TcsArquivoItem By EntitySearchId.
	    public IQueryable<TcsArquivoItem> GetTcsArquivoItemByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsArquivoItemByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get TcsArquivoItemCampo By EntitySearchId.
	    public IQueryable<TcsArquivoItemCampo> GetTcsArquivoItemCampoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsArquivoItemCampoByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get TcsArquivoLog By EntitySearchId.
	    public IQueryable<TcsArquivoLog> GetTcsArquivoLogByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsArquivoLogByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get TcsArquivoGrupoVinculo By EntitySearchId.
	    public IQueryable<TcsArquivoGrupoVinculo> GetTcsArquivoGrupoVinculoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsArquivoGrupoVinculoByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get TcsArquivoGrupo By EntitySearchId.
	    public IQueryable<TcsArquivoGrupo> GetTcsArquivoGrupoByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsArquivoGrupoByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get TcsArquivoGrupo By EntitySearchId.
	    public IQueryable<TcsArquivoGrupo> GetTcsArquivoGrupoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsArquivoGrupoByEntitySearchNoAssociations(queryAnalysis);
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
			
	    //Get TcsArquivoItem By Example.
	    [Ignore]
	    public IQueryable<TcsArquivoItem> GetTcsArquivoItemByExample(TcsArquivoItem entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsArquivoItemByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsArquivoItemCampo By Example.
	    [Ignore]
	    public IQueryable<TcsArquivoItemCampo> GetTcsArquivoItemCampoByExample(TcsArquivoItemCampo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsArquivoItemCampoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsArquivoLog By Example.
	    [Ignore]
	    public IQueryable<TcsArquivoLog> GetTcsArquivoLogByExample(TcsArquivoLog entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsArquivoLogByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsArquivoGrupoVinculo By Example.
	    [Ignore]
	    public IQueryable<TcsArquivoGrupoVinculo> GetTcsArquivoGrupoVinculoByExample(TcsArquivoGrupoVinculo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsArquivoGrupoVinculoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsArquivo By Example.
	    [Ignore]
	    public IQueryable<TcsArquivo> GetTcsArquivoByExampleNoAssociations(TcsArquivo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsArquivoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsArquivoItem By Example.
	    [Ignore]
	    public IQueryable<TcsArquivoItem> GetTcsArquivoItemByExampleNoAssociations(TcsArquivoItem entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsArquivoItemByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsArquivoItemCampo By Example.
	    [Ignore]
	    public IQueryable<TcsArquivoItemCampo> GetTcsArquivoItemCampoByExampleNoAssociations(TcsArquivoItemCampo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsArquivoItemCampoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsArquivoLog By Example.
	    [Ignore]
	    public IQueryable<TcsArquivoLog> GetTcsArquivoLogByExampleNoAssociations(TcsArquivoLog entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsArquivoLogByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsArquivoGrupoVinculo By Example.
	    [Ignore]
	    public IQueryable<TcsArquivoGrupoVinculo> GetTcsArquivoGrupoVinculoByExampleNoAssociations(TcsArquivoGrupoVinculo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsArquivoGrupoVinculoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsArquivoGrupo By Example.
	    [Ignore]
	    public IQueryable<TcsArquivoGrupo> GetTcsArquivoGrupoByExample(TcsArquivoGrupo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsArquivoGrupoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsArquivoGrupo By Example.
	    [Ignore]
	    public IQueryable<TcsArquivoGrupo> GetTcsArquivoGrupoByExampleNoAssociations(TcsArquivoGrupo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsArquivoGrupoByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



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


	    [Ignore]
	    public TcsArquivoItem GetTcsArquivoItemByKey(Int32 idArquivoItem)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsArquivoItem");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdArquivoItem"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idArquivoItem));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsArquivoItemByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsArquivoItemCampo GetTcsArquivoItemCampoByKey(Int32 idArquivoItemCampo)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsArquivoItemCampo");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdArquivoItemCampo"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idArquivoItemCampo));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsArquivoItemCampoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	    public TcsArquivoGrupoVinculo GetTcsArquivoGrupoVinculoByKey(Int32 idArquivo, Int32 idArquivoGrupo)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsArquivoGrupoVinculo");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdArquivo"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idArquivo));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdArquivoGrupo"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idArquivoGrupo));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsArquivoGrupoVinculoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsArquivoGrupo GetTcsArquivoGrupoByKey(Int32 idArquivoGrupo)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsArquivoGrupo");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdArquivoGrupo"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idArquivoGrupo));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsArquivoGrupoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	            (from entity0 in this.DbContext.TCS_ARQUIVO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsArquivo()		
	            {
	            
                ArquivoDll = entity0.ARQUIVO_DLL
                , CaminhoArquivo = entity0.CAMINHO_ARQUIVO
                , Classe = entity0.CLASSE
                , CodArquivo = entity0.COD_ARQUIVO
                , Delimitador = entity0.DELIMITADOR
                , DescArquivo = entity0.DESC_ARQUIVO
                , DetalheArquivo = entity0.DETALHE_ARQUIVO
                , IdArquivo = entity0.ID_ARQUIVO
                , Inativo = entity0.INATIVO
                , LxFormatoData = entity0.LX_FORMATO_DATA
                , LxFormatoDataName = ((entity0.LX_FORMATO_DATA) == "1" ? "AAAAMMDD" : ((entity0.LX_FORMATO_DATA) == "4" ? "AAMMDD" : ((entity0.LX_FORMATO_DATA) == "5" ? "DDMMAA" : ((entity0.LX_FORMATO_DATA) == "2" ? "DDMMAAAA" : ((entity0.LX_FORMATO_DATA) == "6" ? "MMDDAA" : ((entity0.LX_FORMATO_DATA) == "3" ? "MMDDAAAA" : ""))))))
                , LxTipoArquivo = entity0.LX_TIPO_ARQUIVO
                , LxTipoArquivoName = ((entity0.LX_TIPO_ARQUIVO) == "E" ? "Excel" : ((entity0.LX_TIPO_ARQUIVO) == "T" ? "Text" : ((entity0.LX_TIPO_ARQUIVO) == "G" ? "Todos" : ((entity0.LX_TIPO_ARQUIVO) == "X" ? "XML" : ""))))
                , Metodo = entity0.METODO
                , NomeArquivo = entity0.NOME_ARQUIVO
                , TagMestre = entity0.TAG_MESTRE
                , Xmlns = entity0.XMLNS
                , Xsd = entity0.XSD
			
                ,TcsArquivoItemList = 
	                        (from entity1 in entity0.TCS_ARQUIVO_ITEM_LISTA
                                  let entity1Al1 = entity1.ARQUIVO_ITEM_PAI
                                orderby entity1.ORDEM ascending
	                        
	                        	
	                        select new TcsArquivoItem()
	                        {
	                        
                                IdArquivoFk = entity1.ID_ARQUIVO_FK
                                , IdArquivoItemPai = entity1Al1.ID_ARQUIVO_ITEM
                                , IdArquivoItem = entity1.ID_ARQUIVO_ITEM
                                , IndicaNotnull = entity1.INDICA_NOTNULL
                                , Ordem = entity1.ORDEM
                                , TagItemPai = entity1Al1.TAG_ITEM
                                , TagItem = entity1.TAG_ITEM
                                , Xmlns = entity1.XMLNS
			
                                ,TcsArquivoItemCampoList = 
	                                                (from entity2 in entity1.TCS_ARQUIVO_ITEM_CAMPO_LISTA
                                                                orderby entity2.ORDEM ascending
	                                                
	                                                	
	                                                select new TcsArquivoItemCampo()
	                                                {
	                                                
                                                                ChaveIdentificacao = entity2.CHAVE_IDENTIFICACAO
                                                                , Decimais = entity2.DECIMAIS
                                                                , IdArquivoItemCampo = entity2.ID_ARQUIVO_ITEM_CAMPO
                                                                , IdArquivoItemFk = entity2.ID_ARQUIVO_ITEM_FK
                                                                , IndicaNotnull = entity2.INDICA_NOTNULL
                                                                , IndicaPk = entity2.INDICA_PK
                                                                , LxFormatoData = entity2.LX_FORMATO_DATA
                                                                , LxFormatoDataName = ((entity2.LX_FORMATO_DATA) == "1" ? "AAAAMMDD" : ((entity2.LX_FORMATO_DATA) == "4" ? "AAMMDD" : ((entity2.LX_FORMATO_DATA) == "5" ? "DDMMAA" : ((entity2.LX_FORMATO_DATA) == "2" ? "DDMMAAAA" : ((entity2.LX_FORMATO_DATA) == "6" ? "MMDDAA" : ((entity2.LX_FORMATO_DATA) == "3" ? "MMDDAAAA" : ""))))))
                                                                , LxTipoDado = entity2.LX_TIPO_DADO
                                                                , LxTipoDadoName = ((entity2.LX_TIPO_DADO) == "BLN" ? "Boolean" : ((entity2.LX_TIPO_DADO) == "BYT" ? "Byte" : ((entity2.LX_TIPO_DADO) == "DTE" ? "Date" : ((entity2.LX_TIPO_DADO) == "DEC" ? "Decimal" : ((entity2.LX_TIPO_DADO) == "DBL" ? "Double" : ((entity2.LX_TIPO_DADO) == "INT" ? "Integer" : ((entity2.LX_TIPO_DADO) == "LNG" ? "Long" : ((entity2.LX_TIPO_DADO) == "POS" ? "PositiveInteger" : ((entity2.LX_TIPO_DADO) == "STR" ? "String" : ((entity2.LX_TIPO_DADO) == "TME" ? "Time" : ""))))))))))
                                                                , Ordem = entity2.ORDEM
                                                                , TagCampo = entity2.TAG_CAMPO
                                                                , Tamanho = entity2.TAMANHO
		
	                                                }
	                                                )
		
	                        }
	                        )
			
                ,TcsArquivoLogList = 
	                        (from entity1 in entity0.TCS_ARQUIVO_LOG_LISTA
                                orderby entity1.ID_ARQUIVO_LOG ascending
	                        
	                        	
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
			
                ,TcsArquivoGrupoVinculoList = 
	                        (from entity1 in entity0.TCS_ARQUIVO_GRUPO_VINCULO_LISTA
                                  let entity1Al1 = entity1.TCS_ARQUIVO_GRUPO
	                        
	                        	
	                        select new TcsArquivoGrupoVinculo()
	                        {
	                        
                                CodArquivoGrupo = entity1Al1.COD_ARQUIVO_GRUPO
                                , DescArquivoGrupo = entity1Al1.DESC_ARQUIVO_GRUPO
                                , IdArquivo = entity1.ID_ARQUIVO
                                , IdArquivoGrupo = entity1Al1.ID_ARQUIVO_GRUPO
                                , Inativo = entity1.INATIVO
                                , Ordem = entity1.ORDEM
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsArquivoItemByEntitySearch.
	    public IQueryable<TcsArquivoItem> GetTcsArquivoItemByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsArquivoItem));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

		
	
	        
		
	        
	
	        IQueryable<TcsArquivoItem> result = 
	            (from entity0 in this.DbContext.TCS_ARQUIVO_ITEM.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.ARQUIVO_ITEM_PAI
                orderby entity0.ORDEM ascending
	            
	            	
	            select new TcsArquivoItem()		
	            {
	            
                IdArquivoFk = entity0.ID_ARQUIVO_FK
                , IdArquivoItemPai = entity0Al1.ID_ARQUIVO_ITEM
                , IdArquivoItem = entity0.ID_ARQUIVO_ITEM
                , IndicaNotnull = entity0.INDICA_NOTNULL
                , Ordem = entity0.ORDEM
                , TagItemPai = entity0Al1.TAG_ITEM
                , TagItem = entity0.TAG_ITEM
                , Xmlns = entity0.XMLNS
			
                ,TcsArquivoItemCampoList = 
	                        (from entity1 in entity0.TCS_ARQUIVO_ITEM_CAMPO_LISTA
                                orderby entity1.ORDEM ascending
	                        
	                        	
	                        select new TcsArquivoItemCampo()
	                        {
	                        
                                ChaveIdentificacao = entity1.CHAVE_IDENTIFICACAO
                                , Decimais = entity1.DECIMAIS
                                , IdArquivoItemCampo = entity1.ID_ARQUIVO_ITEM_CAMPO
                                , IdArquivoItemFk = entity1.ID_ARQUIVO_ITEM_FK
                                , IndicaNotnull = entity1.INDICA_NOTNULL
                                , IndicaPk = entity1.INDICA_PK
                                , LxFormatoData = entity1.LX_FORMATO_DATA
                                , LxFormatoDataName = ((entity1.LX_FORMATO_DATA) == "1" ? "AAAAMMDD" : ((entity1.LX_FORMATO_DATA) == "4" ? "AAMMDD" : ((entity1.LX_FORMATO_DATA) == "5" ? "DDMMAA" : ((entity1.LX_FORMATO_DATA) == "2" ? "DDMMAAAA" : ((entity1.LX_FORMATO_DATA) == "6" ? "MMDDAA" : ((entity1.LX_FORMATO_DATA) == "3" ? "MMDDAAAA" : ""))))))
                                , LxTipoDado = entity1.LX_TIPO_DADO
                                , LxTipoDadoName = ((entity1.LX_TIPO_DADO) == "BLN" ? "Boolean" : ((entity1.LX_TIPO_DADO) == "BYT" ? "Byte" : ((entity1.LX_TIPO_DADO) == "DTE" ? "Date" : ((entity1.LX_TIPO_DADO) == "DEC" ? "Decimal" : ((entity1.LX_TIPO_DADO) == "DBL" ? "Double" : ((entity1.LX_TIPO_DADO) == "INT" ? "Integer" : ((entity1.LX_TIPO_DADO) == "LNG" ? "Long" : ((entity1.LX_TIPO_DADO) == "POS" ? "PositiveInteger" : ((entity1.LX_TIPO_DADO) == "STR" ? "String" : ((entity1.LX_TIPO_DADO) == "TME" ? "Time" : ""))))))))))
                                , Ordem = entity1.ORDEM
                                , TagCampo = entity1.TAG_CAMPO
                                , Tamanho = entity1.TAMANHO
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsArquivoItemCampoByEntitySearch.
	    public IQueryable<TcsArquivoItemCampo> GetTcsArquivoItemCampoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsArquivoItemCampo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

		
	
	        
		
	        
	
	        IQueryable<TcsArquivoItemCampo> result = 
	            (from entity0 in this.DbContext.TCS_ARQUIVO_ITEM_CAMPO.Where(dynQuery, parameters.ToArray())
                orderby entity0.ORDEM ascending
	            
	            	
	            select new TcsArquivoItemCampo()		
	            {
	            
                ChaveIdentificacao = entity0.CHAVE_IDENTIFICACAO
                , Decimais = entity0.DECIMAIS
                , IdArquivoItemCampo = entity0.ID_ARQUIVO_ITEM_CAMPO
                , IdArquivoItemFk = entity0.ID_ARQUIVO_ITEM_FK
                , IndicaNotnull = entity0.INDICA_NOTNULL
                , IndicaPk = entity0.INDICA_PK
                , LxFormatoData = entity0.LX_FORMATO_DATA
                , LxFormatoDataName = ((entity0.LX_FORMATO_DATA) == "1" ? "AAAAMMDD" : ((entity0.LX_FORMATO_DATA) == "4" ? "AAMMDD" : ((entity0.LX_FORMATO_DATA) == "5" ? "DDMMAA" : ((entity0.LX_FORMATO_DATA) == "2" ? "DDMMAAAA" : ((entity0.LX_FORMATO_DATA) == "6" ? "MMDDAA" : ((entity0.LX_FORMATO_DATA) == "3" ? "MMDDAAAA" : ""))))))
                , LxTipoDado = entity0.LX_TIPO_DADO
                , LxTipoDadoName = ((entity0.LX_TIPO_DADO) == "BLN" ? "Boolean" : ((entity0.LX_TIPO_DADO) == "BYT" ? "Byte" : ((entity0.LX_TIPO_DADO) == "DTE" ? "Date" : ((entity0.LX_TIPO_DADO) == "DEC" ? "Decimal" : ((entity0.LX_TIPO_DADO) == "DBL" ? "Double" : ((entity0.LX_TIPO_DADO) == "INT" ? "Integer" : ((entity0.LX_TIPO_DADO) == "LNG" ? "Long" : ((entity0.LX_TIPO_DADO) == "POS" ? "PositiveInteger" : ((entity0.LX_TIPO_DADO) == "STR" ? "String" : ((entity0.LX_TIPO_DADO) == "TME" ? "Time" : ""))))))))))
                , Ordem = entity0.ORDEM
                , TagCampo = entity0.TAG_CAMPO
                , Tamanho = entity0.TAMANHO
		
	            }
	            );
		
	
	        	

	
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
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsArquivoGrupoVinculoByEntitySearch.
	    public IQueryable<TcsArquivoGrupoVinculo> GetTcsArquivoGrupoVinculoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsArquivoGrupoVinculo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

		
	
	        
		
	        
	
	        IQueryable<TcsArquivoGrupoVinculo> result = 
	            (from entity0 in this.DbContext.TCS_ARQUIVO_GRUPO_VINCULO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_ARQUIVO_GRUPO
	            
	            	
	            select new TcsArquivoGrupoVinculo()		
	            {
	            
                CodArquivoGrupo = entity0Al1.COD_ARQUIVO_GRUPO
                , DescArquivoGrupo = entity0Al1.DESC_ARQUIVO_GRUPO
                , IdArquivo = entity0.ID_ARQUIVO
                , IdArquivoGrupo = entity0Al1.ID_ARQUIVO_GRUPO
                , Inativo = entity0.INATIVO
                , Ordem = entity0.ORDEM
		
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
	            (from entity0 in this.DbContext.TCS_ARQUIVO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsArquivo()		
	            {
	            
                ArquivoDll = entity0.ARQUIVO_DLL
                , CaminhoArquivo = entity0.CAMINHO_ARQUIVO
                , Classe = entity0.CLASSE
                , CodArquivo = entity0.COD_ARQUIVO
                , Delimitador = entity0.DELIMITADOR
                , DescArquivo = entity0.DESC_ARQUIVO
                , DetalheArquivo = entity0.DETALHE_ARQUIVO
                , IdArquivo = entity0.ID_ARQUIVO
                , Inativo = entity0.INATIVO
                , LxFormatoData = entity0.LX_FORMATO_DATA
                , LxFormatoDataName = ((entity0.LX_FORMATO_DATA) == "1" ? "AAAAMMDD" : ((entity0.LX_FORMATO_DATA) == "4" ? "AAMMDD" : ((entity0.LX_FORMATO_DATA) == "5" ? "DDMMAA" : ((entity0.LX_FORMATO_DATA) == "2" ? "DDMMAAAA" : ((entity0.LX_FORMATO_DATA) == "6" ? "MMDDAA" : ((entity0.LX_FORMATO_DATA) == "3" ? "MMDDAAAA" : ""))))))
                , LxTipoArquivo = entity0.LX_TIPO_ARQUIVO
                , LxTipoArquivoName = ((entity0.LX_TIPO_ARQUIVO) == "E" ? "Excel" : ((entity0.LX_TIPO_ARQUIVO) == "T" ? "Text" : ((entity0.LX_TIPO_ARQUIVO) == "G" ? "Todos" : ((entity0.LX_TIPO_ARQUIVO) == "X" ? "XML" : ""))))
                , Metodo = entity0.METODO
                , NomeArquivo = entity0.NOME_ARQUIVO
                , TagMestre = entity0.TAG_MESTRE
                , Xmlns = entity0.XMLNS
                , Xsd = entity0.XSD
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsArquivoItemByEntitySearchNoAssociations.
	    public IQueryable<TcsArquivoItem> GetTcsArquivoItemByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsArquivoItem));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

		
	
	        
		
	        
	
	        IQueryable<TcsArquivoItem> result = 
	            (from entity0 in this.DbContext.TCS_ARQUIVO_ITEM.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.ARQUIVO_ITEM_PAI
                orderby entity0.ORDEM ascending
	            
	            	
	            select new TcsArquivoItem()		
	            {
	            
                IdArquivoFk = entity0.ID_ARQUIVO_FK
                , IdArquivoItemPai = entity0Al1.ID_ARQUIVO_ITEM
                , IdArquivoItem = entity0.ID_ARQUIVO_ITEM
                , IndicaNotnull = entity0.INDICA_NOTNULL
                , Ordem = entity0.ORDEM
                , TagItemPai = entity0Al1.TAG_ITEM
                , TagItem = entity0.TAG_ITEM
                , Xmlns = entity0.XMLNS
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsArquivoItemCampoByEntitySearchNoAssociations.
	    public IQueryable<TcsArquivoItemCampo> GetTcsArquivoItemCampoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsArquivoItemCampo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

		
	
	        
		
	        
	
	        IQueryable<TcsArquivoItemCampo> result = 
	            (from entity0 in this.DbContext.TCS_ARQUIVO_ITEM_CAMPO.Where(dynQuery, parameters.ToArray())
                orderby entity0.ORDEM ascending
	            
	            	
	            select new TcsArquivoItemCampo()		
	            {
	            
                ChaveIdentificacao = entity0.CHAVE_IDENTIFICACAO
                , Decimais = entity0.DECIMAIS
                , IdArquivoItemCampo = entity0.ID_ARQUIVO_ITEM_CAMPO
                , IdArquivoItemFk = entity0.ID_ARQUIVO_ITEM_FK
                , IndicaNotnull = entity0.INDICA_NOTNULL
                , IndicaPk = entity0.INDICA_PK
                , LxFormatoData = entity0.LX_FORMATO_DATA
                , LxFormatoDataName = ((entity0.LX_FORMATO_DATA) == "1" ? "AAAAMMDD" : ((entity0.LX_FORMATO_DATA) == "4" ? "AAMMDD" : ((entity0.LX_FORMATO_DATA) == "5" ? "DDMMAA" : ((entity0.LX_FORMATO_DATA) == "2" ? "DDMMAAAA" : ((entity0.LX_FORMATO_DATA) == "6" ? "MMDDAA" : ((entity0.LX_FORMATO_DATA) == "3" ? "MMDDAAAA" : ""))))))
                , LxTipoDado = entity0.LX_TIPO_DADO
                , LxTipoDadoName = ((entity0.LX_TIPO_DADO) == "BLN" ? "Boolean" : ((entity0.LX_TIPO_DADO) == "BYT" ? "Byte" : ((entity0.LX_TIPO_DADO) == "DTE" ? "Date" : ((entity0.LX_TIPO_DADO) == "DEC" ? "Decimal" : ((entity0.LX_TIPO_DADO) == "DBL" ? "Double" : ((entity0.LX_TIPO_DADO) == "INT" ? "Integer" : ((entity0.LX_TIPO_DADO) == "LNG" ? "Long" : ((entity0.LX_TIPO_DADO) == "POS" ? "PositiveInteger" : ((entity0.LX_TIPO_DADO) == "STR" ? "String" : ((entity0.LX_TIPO_DADO) == "TME" ? "Time" : ""))))))))))
                , Ordem = entity0.ORDEM
                , TagCampo = entity0.TAG_CAMPO
                , Tamanho = entity0.TAMANHO
		
	            }
	            );
		
	
	        	

	
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
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsArquivoGrupoVinculoByEntitySearchNoAssociations.
	    public IQueryable<TcsArquivoGrupoVinculo> GetTcsArquivoGrupoVinculoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsArquivoGrupoVinculo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

		
	
	        
		
	        
	
	        IQueryable<TcsArquivoGrupoVinculo> result = 
	            (from entity0 in this.DbContext.TCS_ARQUIVO_GRUPO_VINCULO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_ARQUIVO_GRUPO
	            
	            	
	            select new TcsArquivoGrupoVinculo()		
	            {
	            
                CodArquivoGrupo = entity0Al1.COD_ARQUIVO_GRUPO
                , DescArquivoGrupo = entity0Al1.DESC_ARQUIVO_GRUPO
                , IdArquivo = entity0.ID_ARQUIVO
                , IdArquivoGrupo = entity0Al1.ID_ARQUIVO_GRUPO
                , Inativo = entity0.INATIVO
                , Ordem = entity0.ORDEM
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsArquivoItemParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsArquivoItemParentComposition> GetTcsArquivoItemParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsArquivoItemParentComposition));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

		
	
	        
		
	        
	
	        IQueryable<TcsArquivoItemParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_ARQUIVO_ITEM.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.ARQUIVO_ITEM_PAI
                orderby entity0.ORDEM ascending
	            
	            	
	            select new TcsArquivoItemParentComposition()		
	            {
	            
                IdArquivoFk = entity0.ID_ARQUIVO_FK
                , IdArquivoItemPai = entity0Al1.ID_ARQUIVO_ITEM
                , IdArquivoItem = entity0.ID_ARQUIVO_ITEM
                , IndicaNotnull = entity0.INDICA_NOTNULL
                , Ordem = entity0.ORDEM
                , TagItemPai = entity0Al1.TAG_ITEM
                , TagItem = entity0.TAG_ITEM
                , Xmlns = entity0.XMLNS
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsArquivoItemCampoParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsArquivoItemCampoParentComposition> GetTcsArquivoItemCampoParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsArquivoItemCampoParentComposition));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

		
	
	        
		
	        
	
	        IQueryable<TcsArquivoItemCampoParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_ARQUIVO_ITEM_CAMPO.Where(dynQuery, parameters.ToArray())
                orderby entity0.ORDEM ascending
	            
	            	
	            select new TcsArquivoItemCampoParentComposition()		
	            {
	            
                ChaveIdentificacao = entity0.CHAVE_IDENTIFICACAO
                , Decimais = entity0.DECIMAIS
                , IdArquivoItemCampo = entity0.ID_ARQUIVO_ITEM_CAMPO
                , IdArquivoItemFk = entity0.ID_ARQUIVO_ITEM_FK
                , IndicaNotnull = entity0.INDICA_NOTNULL
                , IndicaPk = entity0.INDICA_PK
                , LxFormatoData = entity0.LX_FORMATO_DATA
                , LxFormatoDataName = ((entity0.LX_FORMATO_DATA) == "1" ? "AAAAMMDD" : ((entity0.LX_FORMATO_DATA) == "4" ? "AAMMDD" : ((entity0.LX_FORMATO_DATA) == "5" ? "DDMMAA" : ((entity0.LX_FORMATO_DATA) == "2" ? "DDMMAAAA" : ((entity0.LX_FORMATO_DATA) == "6" ? "MMDDAA" : ((entity0.LX_FORMATO_DATA) == "3" ? "MMDDAAAA" : ""))))))
                , LxTipoDado = entity0.LX_TIPO_DADO
                , LxTipoDadoName = ((entity0.LX_TIPO_DADO) == "BLN" ? "Boolean" : ((entity0.LX_TIPO_DADO) == "BYT" ? "Byte" : ((entity0.LX_TIPO_DADO) == "DTE" ? "Date" : ((entity0.LX_TIPO_DADO) == "DEC" ? "Decimal" : ((entity0.LX_TIPO_DADO) == "DBL" ? "Double" : ((entity0.LX_TIPO_DADO) == "INT" ? "Integer" : ((entity0.LX_TIPO_DADO) == "LNG" ? "Long" : ((entity0.LX_TIPO_DADO) == "POS" ? "PositiveInteger" : ((entity0.LX_TIPO_DADO) == "STR" ? "String" : ((entity0.LX_TIPO_DADO) == "TME" ? "Time" : ""))))))))))
                , Ordem = entity0.ORDEM
                , TagCampo = entity0.TAG_CAMPO
                , Tamanho = entity0.TAMANHO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsArquivoLogParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsArquivoLogParentComposition> GetTcsArquivoLogParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsArquivoLogParentComposition));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

		
	
	        
		
	        
	
	        IQueryable<TcsArquivoLogParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_ARQUIVO_LOG.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_ARQUIVO_LOG ascending
	            
	            	
	            select new TcsArquivoLogParentComposition()		
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
	    //Get TcsArquivoGrupoVinculoParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsArquivoGrupoVinculoParentComposition> GetTcsArquivoGrupoVinculoParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsArquivoGrupoVinculoParentComposition));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

		
	
	        
		
	        
	
	        IQueryable<TcsArquivoGrupoVinculoParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_ARQUIVO_GRUPO_VINCULO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_ARQUIVO_GRUPO
	            
	            	
	            select new TcsArquivoGrupoVinculoParentComposition()		
	            {
	            
                CodArquivoGrupo = entity0Al1.COD_ARQUIVO_GRUPO
                , DescArquivoGrupo = entity0Al1.DESC_ARQUIVO_GRUPO
                , IdArquivo = entity0.ID_ARQUIVO
                , IdArquivoGrupo = entity0Al1.ID_ARQUIVO_GRUPO
                , Inativo = entity0.INATIVO
                , Ordem = entity0.ORDEM
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsArquivoGrupoByEntitySearch.
	    public IQueryable<TcsArquivoGrupo> GetTcsArquivoGrupoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsArquivoGrupo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

		
	
	        
		
	        
	
	        IQueryable<TcsArquivoGrupo> result = 
	            (from entity0 in this.DbContext.TCS_ARQUIVO_GRUPO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsArquivoGrupo()		
	            {
	            
                CodArquivoGrupo = entity0.COD_ARQUIVO_GRUPO
                , DescArquivoGrupo = entity0.DESC_ARQUIVO_GRUPO
                , IdArquivoGrupo = entity0.ID_ARQUIVO_GRUPO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsArquivoGrupoByEntitySearchNoAssociations.
	    public IQueryable<TcsArquivoGrupo> GetTcsArquivoGrupoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsArquivoGrupo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

		
	
	        
		
	        
	
	        IQueryable<TcsArquivoGrupo> result = 
	            (from entity0 in this.DbContext.TCS_ARQUIVO_GRUPO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsArquivoGrupo()		
	            {
	            
                CodArquivoGrupo = entity0.COD_ARQUIVO_GRUPO
                , DescArquivoGrupo = entity0.DESC_ARQUIVO_GRUPO
                , IdArquivoGrupo = entity0.ID_ARQUIVO_GRUPO
		
	            }
	            );
		
	
	        	

	
	        return result;
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
	            (from entity0 in this.DbContext.TCS_ARQUIVO.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_ARQUIVO ascending
	            
	            	
	            select new TcsArquivo()		
	            {
	            
                ArquivoDll = entity0.ARQUIVO_DLL
                , CaminhoArquivo = entity0.CAMINHO_ARQUIVO
                , Classe = entity0.CLASSE
                , CodArquivo = entity0.COD_ARQUIVO
                , Delimitador = entity0.DELIMITADOR
                , DescArquivo = entity0.DESC_ARQUIVO
                , DetalheArquivo = entity0.DETALHE_ARQUIVO
                , IdArquivo = entity0.ID_ARQUIVO
                , Inativo = entity0.INATIVO
                , LxFormatoData = entity0.LX_FORMATO_DATA
                , LxFormatoDataName = ((entity0.LX_FORMATO_DATA) == "1" ? "AAAAMMDD" : ((entity0.LX_FORMATO_DATA) == "4" ? "AAMMDD" : ((entity0.LX_FORMATO_DATA) == "5" ? "DDMMAA" : ((entity0.LX_FORMATO_DATA) == "2" ? "DDMMAAAA" : ((entity0.LX_FORMATO_DATA) == "6" ? "MMDDAA" : ((entity0.LX_FORMATO_DATA) == "3" ? "MMDDAAAA" : ""))))))
                , LxTipoArquivo = entity0.LX_TIPO_ARQUIVO
                , LxTipoArquivoName = ((entity0.LX_TIPO_ARQUIVO) == "E" ? "Excel" : ((entity0.LX_TIPO_ARQUIVO) == "T" ? "Text" : ((entity0.LX_TIPO_ARQUIVO) == "G" ? "Todos" : ((entity0.LX_TIPO_ARQUIVO) == "X" ? "XML" : ""))))
                , Metodo = entity0.METODO
                , NomeArquivo = entity0.NOME_ARQUIVO
                , TagMestre = entity0.TAG_MESTRE
                , Xmlns = entity0.XMLNS
                , Xsd = entity0.XSD
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsArquivoItem.
	    public IQueryable<TcsArquivoItem> GetPagedTcsArquivoItem(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsArquivoItem));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

		
	
	        
		
	        
	
	        IQueryable<TcsArquivoItem> result = 
	            (from entity0 in this.DbContext.TCS_ARQUIVO_ITEM.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.ARQUIVO_ITEM_PAI
                orderby entity0.ID_ARQUIVO_ITEM ascending
	            
	            	
	            select new TcsArquivoItem()		
	            {
	            
                IdArquivoFk = entity0.ID_ARQUIVO_FK
                , IdArquivoItemPai = entity0Al1.ID_ARQUIVO_ITEM
                , IdArquivoItem = entity0.ID_ARQUIVO_ITEM
                , IndicaNotnull = entity0.INDICA_NOTNULL
                , Ordem = entity0.ORDEM
                , TagItemPai = entity0Al1.TAG_ITEM
                , TagItem = entity0.TAG_ITEM
                , Xmlns = entity0.XMLNS
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsArquivoItemCampo.
	    public IQueryable<TcsArquivoItemCampo> GetPagedTcsArquivoItemCampo(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsArquivoItemCampo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

		
	
	        
		
	        
	
	        IQueryable<TcsArquivoItemCampo> result = 
	            (from entity0 in this.DbContext.TCS_ARQUIVO_ITEM_CAMPO.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_ARQUIVO_ITEM_CAMPO ascending
	            
	            	
	            select new TcsArquivoItemCampo()		
	            {
	            
                ChaveIdentificacao = entity0.CHAVE_IDENTIFICACAO
                , Decimais = entity0.DECIMAIS
                , IdArquivoItemCampo = entity0.ID_ARQUIVO_ITEM_CAMPO
                , IdArquivoItemFk = entity0.ID_ARQUIVO_ITEM_FK
                , IndicaNotnull = entity0.INDICA_NOTNULL
                , IndicaPk = entity0.INDICA_PK
                , LxFormatoData = entity0.LX_FORMATO_DATA
                , LxFormatoDataName = ((entity0.LX_FORMATO_DATA) == "1" ? "AAAAMMDD" : ((entity0.LX_FORMATO_DATA) == "4" ? "AAMMDD" : ((entity0.LX_FORMATO_DATA) == "5" ? "DDMMAA" : ((entity0.LX_FORMATO_DATA) == "2" ? "DDMMAAAA" : ((entity0.LX_FORMATO_DATA) == "6" ? "MMDDAA" : ((entity0.LX_FORMATO_DATA) == "3" ? "MMDDAAAA" : ""))))))
                , LxTipoDado = entity0.LX_TIPO_DADO
                , LxTipoDadoName = ((entity0.LX_TIPO_DADO) == "BLN" ? "Boolean" : ((entity0.LX_TIPO_DADO) == "BYT" ? "Byte" : ((entity0.LX_TIPO_DADO) == "DTE" ? "Date" : ((entity0.LX_TIPO_DADO) == "DEC" ? "Decimal" : ((entity0.LX_TIPO_DADO) == "DBL" ? "Double" : ((entity0.LX_TIPO_DADO) == "INT" ? "Integer" : ((entity0.LX_TIPO_DADO) == "LNG" ? "Long" : ((entity0.LX_TIPO_DADO) == "POS" ? "PositiveInteger" : ((entity0.LX_TIPO_DADO) == "STR" ? "String" : ((entity0.LX_TIPO_DADO) == "TME" ? "Time" : ""))))))))))
                , Ordem = entity0.ORDEM
                , TagCampo = entity0.TAG_CAMPO
                , Tamanho = entity0.TAMANHO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
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
	    //Get PagedTcsArquivoGrupoVinculo.
	    public IQueryable<TcsArquivoGrupoVinculo> GetPagedTcsArquivoGrupoVinculo(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsArquivoGrupoVinculo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

		
	
	        
		
	        
	
	        IQueryable<TcsArquivoGrupoVinculo> result = 
	            (from entity0 in this.DbContext.TCS_ARQUIVO_GRUPO_VINCULO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_ARQUIVO_GRUPO
                orderby entity0.ID_ARQUIVO ascending, entity0Al1.ID_ARQUIVO_GRUPO ascending
	            
	            	
	            select new TcsArquivoGrupoVinculo()		
	            {
	            
                CodArquivoGrupo = entity0Al1.COD_ARQUIVO_GRUPO
                , DescArquivoGrupo = entity0Al1.DESC_ARQUIVO_GRUPO
                , IdArquivo = entity0.ID_ARQUIVO
                , IdArquivoGrupo = entity0Al1.ID_ARQUIVO_GRUPO
                , Inativo = entity0.INATIVO
                , Ordem = entity0.ORDEM
		
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
	    public int GetTcsArquivoItemCounting(string serializedEntitySearch)
	    {	
		 	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsArquivoItem));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_ARQUIVO_ITEM.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.ARQUIVO_ITEM_PAI
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsArquivoItemCampoCounting(string serializedEntitySearch)
	    {	
		 	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsArquivoItemCampo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_ARQUIVO_ITEM_CAMPO.Where(dynQuery, parameters.ToArray())
	            
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
		
	    [Ignore]
	    public int GetTcsArquivoGrupoVinculoCounting(string serializedEntitySearch)
	    {	
		 	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsArquivoGrupoVinculo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_ARQUIVO_GRUPO_VINCULO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_ARQUIVO_GRUPO
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsArquivoGrupo.
	    public IQueryable<TcsArquivoGrupo> GetPagedTcsArquivoGrupo(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsArquivoGrupo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

		
	
	        
		
	        
	
	        IQueryable<TcsArquivoGrupo> result = 
	            (from entity0 in this.DbContext.TCS_ARQUIVO_GRUPO.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_ARQUIVO_GRUPO ascending
	            
	            	
	            select new TcsArquivoGrupo()		
	            {
	            
                CodArquivoGrupo = entity0.COD_ARQUIVO_GRUPO
                , DescArquivoGrupo = entity0.DESC_ARQUIVO_GRUPO
                , IdArquivoGrupo = entity0.ID_ARQUIVO_GRUPO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsArquivoGrupoCounting(string serializedEntitySearch)
	    {	
		 	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsArquivoGrupo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_ARQUIVO_GRUPO.Where(dynQuery, parameters.ToArray())
	            
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
	    //Update TcsArquivoItem.
	    public void UpdateTcsArquivoItem(TcsArquivoItem entity)
	    {



	
	        if (entity.TcsArquivo.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsArquivo) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsArquivo); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsArquivoItem.
	    public void InsertTcsArquivoItem(TcsArquivoItem entity)
	    {



	
	        if (entity.TcsArquivo.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsArquivo) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsArquivo);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsArquivoItem.
	    public void DeleteTcsArquivoItem(TcsArquivoItem entity)
	    {



	
	        if (entity.TcsArquivo.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsArquivo) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsArquivo);
	            

	
	        }

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsArquivoItemCampo.
	    public void UpdateTcsArquivoItemCampo(TcsArquivoItemCampo entity)
	    {



	
	        if (entity.TcsArquivoItem.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsArquivoItem) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsArquivoItem); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsArquivoItemCampo.
	    public void InsertTcsArquivoItemCampo(TcsArquivoItemCampo entity)
	    {



	
	        if (entity.TcsArquivoItem.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsArquivoItem) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsArquivoItem);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsArquivoItemCampo.
	    public void DeleteTcsArquivoItemCampo(TcsArquivoItemCampo entity)
	    {



	
	        if (entity.TcsArquivoItem.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsArquivoItem) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsArquivoItem);
	            

	
	        }

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsArquivoLog.
	    public void UpdateTcsArquivoLog(TcsArquivoLog entity)
	    {



	
	        if (entity.TcsArquivo.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsArquivo) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsArquivo); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsArquivoLog.
	    public void InsertTcsArquivoLog(TcsArquivoLog entity)
	    {



	
	        if (entity.TcsArquivo.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsArquivo) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsArquivo);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsArquivoLog.
	    public void DeleteTcsArquivoLog(TcsArquivoLog entity)
	    {



	
	        if (entity.TcsArquivo.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsArquivo) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsArquivo);
	            

	
	        }

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsArquivoGrupoVinculo.
	    public void UpdateTcsArquivoGrupoVinculo(TcsArquivoGrupoVinculo entity)
	    {



	
	        if (entity.TcsArquivo.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsArquivo) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsArquivo); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsArquivoGrupoVinculo.
	    public void InsertTcsArquivoGrupoVinculo(TcsArquivoGrupoVinculo entity)
	    {



	
	        if (entity.TcsArquivo.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsArquivo) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsArquivo);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsArquivoGrupoVinculo.
	    public void DeleteTcsArquivoGrupoVinculo(TcsArquivoGrupoVinculo entity)
	    {



	
	        if (entity.TcsArquivo.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsArquivo) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsArquivo);
	            

	
	        }

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsArquivoGrupo.
	    public void UpdateTcsArquivoGrupo(TcsArquivoGrupo entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsArquivoGrupo.
	    public void InsertTcsArquivoGrupo(TcsArquivoGrupo entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsArquivoGrupo.
	    public void DeleteTcsArquivoGrupo(TcsArquivoGrupo entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}