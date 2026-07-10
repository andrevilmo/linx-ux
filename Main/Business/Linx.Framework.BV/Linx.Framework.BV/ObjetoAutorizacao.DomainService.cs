					
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

namespace Linx.Framework.BV.ObjetoAutorizacao
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_OBJETO_AUTORIZACAO.ID_OBJETO", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsObjetoAutorizacao,TcsObjetoAutorizacao.TcsTransacaoAutorizacaoChild,TcsObjetoAutorizacao.TcsObjetoConteudoAutorizacao];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdObjeto];ReadOnly[false];Entities[TCS_OBJETO_AUTORIZACAO:IdObjeto];SubQueryInfo[];EdmEntityName[TCS_OBJETO_AUTORIZACAO];EntityRelations[];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsObjetoAutorizacao")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.ObjetoAutorizacao.TcsObjetoAutorizacao")]
	public partial class TcsObjetoAutorizacao : Linx.Data.Entity
	{

	
		
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.TcsTransacaoAutorizacaoChildList != null && this.TcsTransacaoAutorizacaoChildList.Count() > 0)
	      {
	         foreach (var entity in this.TcsTransacaoAutorizacaoChildList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      if (this.TcsObjetoConteudoAutorizacaoList != null && this.TcsObjetoConteudoAutorizacaoList.Count() > 0)
	      {
	         foreach (var entity in this.TcsObjetoConteudoAutorizacaoList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.TcsTransacaoAutorizacaoChildList != null)
	      {
	         foreach (var detail in this.TcsTransacaoAutorizacaoChildList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsTransacaoAutorizacaoChildList = null;
	      }
	      if (this.TcsObjetoConteudoAutorizacaoList != null)
	      {
	         foreach (var detail in this.TcsObjetoConteudoAutorizacaoList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsObjetoConteudoAutorizacaoList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(ObjetoAutorizacaoDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("TcsTransacaoAutorizacaoChild"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsTransacaoAutorizacaoChild");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdObjeto"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdObjeto));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsTransacaoAutorizacaoChild and all sub-details
	         if (this.TcsTransacaoAutorizacaoChildList == null || this.TcsTransacaoAutorizacaoChildList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsTransacaoAutorizacaoChildList = context.GetPagedTcsTransacaoAutorizacaoChild(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsTransacaoAutorizacaoChildList = (from r in context.GetTcsTransacaoAutorizacaoChildByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	      if (viewNames == null || viewNames.Contains("TcsObjetoConteudoAutorizacao"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsObjetoConteudoAutorizacao");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdObjeto"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdObjeto));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsObjetoConteudoAutorizacao and all sub-details
	         if (this.TcsObjetoConteudoAutorizacaoList == null || this.TcsObjetoConteudoAutorizacaoList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsObjetoConteudoAutorizacaoList = context.GetPagedTcsObjetoConteudoAutorizacao(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsObjetoConteudoAutorizacaoList = (from r in context.GetTcsObjetoConteudoAutorizacaoByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _TcsTransacaoAutorizacaoChildElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsTransacaoAutorizacaoChild && ((TcsTransacaoAutorizacaoChild)e.Entity).TcsObjetoAutorizacao == null && e.Associations == null && e.OriginalAssociations == null && ((TcsTransacaoAutorizacaoChild)e.Entity).IdObjeto == this.IdObjeto).ToList();
 	      if (_TcsTransacaoAutorizacaoChildElements.Count > 0 && this.TcsTransacaoAutorizacaoChildList.Count() == 0)
 	      {
 	          this.TcsTransacaoAutorizacaoChildList = _TcsTransacaoAutorizacaoChildElements.Select(e => (TcsTransacaoAutorizacaoChild)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsTransacaoAutorizacaoChildElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsTransacaoAutorizacaoChild)detail.Entity).TcsObjetoAutorizacao = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsObjetoAutorizacao", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsTransacaoAutorizacaoChildList", indexDetails.ToArray());
 	      }
 
 	      var _TcsObjetoConteudoAutorizacaoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsObjetoConteudoAutorizacao && ((TcsObjetoConteudoAutorizacao)e.Entity).TcsObjetoAutorizacao == null && e.Associations == null && e.OriginalAssociations == null && ((TcsObjetoConteudoAutorizacao)e.Entity).IdObjeto == this.IdObjeto).ToList();
 	      if (_TcsObjetoConteudoAutorizacaoElements.Count > 0 && this.TcsObjetoConteudoAutorizacaoList.Count() == 0)
 	      {
 	          this.TcsObjetoConteudoAutorizacaoList = _TcsObjetoConteudoAutorizacaoElements.Select(e => (TcsObjetoConteudoAutorizacao)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsObjetoConteudoAutorizacaoElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsObjetoConteudoAutorizacao)detail.Entity).TcsObjetoAutorizacao = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsObjetoAutorizacao", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsObjetoConteudoAutorizacaoList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For ClasseNome
	    partial void OnClasseNomeChanging(System.String value);
	    partial void OnClasseNomeChanged();

	    private System.String _ClasseNome;

	    [DataMember(IsRequired = true, Name = "ClasseNome", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Classe", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_OBJETO_AUTORIZACAO.CLASSE_NOME];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO_AUTORIZACAO.CLASSE_NOME")]
	    public System.String ClasseNome
	    {
	    	    get
	    	    {
	    	          return _ClasseNome;
	    	    }
	    	    set
	    	    {
	    	          if (this._ClasseNome != value)
	    	          {
	    	              this.ValidateProperty("ClasseNome", value);
	    	              this.OnClasseNomeChanging(value);
	    	              this.RaiseDataMemberChanging("ClasseNome");
	    	              this._ClasseNome = value;
	    	              this.RaiseDataMemberChanged("ClasseNome");
	    	              this.OnClasseNomeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescObjeto
	    partial void OnDescObjetoChanging(System.String value);
	    partial void OnDescObjetoChanged();

	    private System.String _DescObjeto;

	    [DataMember(IsRequired = true, Name = "DescObjeto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_OBJETO_AUTORIZACAO.DESC_OBJETO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO_AUTORIZACAO.DESC_OBJETO")]
	    public System.String DescObjeto
	    {
	    	    get
	    	    {
	    	          return _DescObjeto;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescObjeto != value)
	    	          {
	    	              this.ValidateProperty("DescObjeto", value);
	    	              this.OnDescObjetoChanging(value);
	    	              this.RaiseDataMemberChanging("DescObjeto");
	    	              this._DescObjeto = value;
	    	              this.RaiseDataMemberChanged("DescObjeto");
	    	              this.OnDescObjetoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdObjeto
	    partial void OnIdObjetoChanging(Int64 value);
	    partial void OnIdObjetoChanged();

	    private Int64 _IdObjeto;

	    [DataMember(IsRequired = true, Name = "IdObjeto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Objeto", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_OBJETO_AUTORIZACAO.ID_OBJETO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO_AUTORIZACAO.ID_OBJETO")]
	    public Int64 IdObjeto
	    {
	    	    get
	    	    {
	    	          return _IdObjeto;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdObjeto != value)
	    	          {
	    	              this.ValidateProperty("IdObjeto", value);
	    	              this.OnIdObjetoChanging(value);
	    	              this.RaiseDataMemberChanging("IdObjeto");
	    	              this._IdObjeto = value;
	    	              this.RaiseDataMemberChanged("IdObjeto");
	    	              this.OnIdObjetoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoObjeto
	    partial void OnLxTipoObjetoChanging(Byte value);
	    partial void OnLxTipoObjetoChanged();

	    private Byte _LxTipoObjeto;

	    [DataMember(IsRequired = true, Name = "LxTipoObjeto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo Objeto", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoObjeto];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_OBJETO_AUTORIZACAO.LX_TIPO_OBJETO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO_AUTORIZACAO.LX_TIPO_OBJETO")]
	    public Byte LxTipoObjeto
	    {
	    	    get
	    	    {
	    	          return _LxTipoObjeto;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoObjeto != value)
	    	          {
	    	              this.ValidateProperty("LxTipoObjeto", value);
	    	              this.OnLxTipoObjetoChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoObjeto");
	    	              this._LxTipoObjeto = value;
	    	              this.RaiseDataMemberChanged("LxTipoObjeto");
	    	              this.OnLxTipoObjetoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ObjetoLinx
	    partial void OnObjetoLinxChanging(bool value);
	    partial void OnObjetoLinxChanged();

	    private bool _ObjetoLinx;

	    [DataMember(IsRequired = true, Name = "ObjetoLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[true];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="true")]
	    public bool ObjetoLinx
	    {
	    	    get
	    	    {
	    	          return _ObjetoLinx;
	    	    }
	    	    set
	    	    {
	    	          if (this._ObjetoLinx != value)
	    	          {
	    	              this.ValidateProperty("ObjetoLinx", value);
	    	              this.OnObjetoLinxChanging(value);
	    	              this.RaiseDataMemberChanging("ObjetoLinx");
	    	              this._ObjetoLinx = value;
	    	              this.RaiseDataMemberChanged("ObjetoLinx");
	    	              this.OnObjetoLinxChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For PathObjeto
	    partial void OnPathObjetoChanging(System.String value);
	    partial void OnPathObjetoChanged();

	    private System.String _PathObjeto;

	    [DataMember(Name = "PathObjeto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Path Objeto", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(2000)]
	    [FunctionalPoint("Precision[2000:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_OBJETO_AUTORIZACAO.PATH_OBJETO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO_AUTORIZACAO.PATH_OBJETO")]
	    public System.String PathObjeto
	    {
	    	    get
	    	    {
	    	          return _PathObjeto;
	    	    }
	    	    set
	    	    {
	    	          if (this._PathObjeto != value)
	    	          {
	    	              this.ValidateProperty("PathObjeto", value);
	    	              this.OnPathObjetoChanging(value);
	    	              this.RaiseDataMemberChanging("PathObjeto");
	    	              this._PathObjeto = value;
	    	              this.RaiseDataMemberChanged("PathObjeto");
	    	              this.OnPathObjetoChanged();
	    	          }
	    	    }
	    }

	    private Int64 _TemporaryIdObjeto;
	    [DataMember(Name = "TemporaryIdObjeto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Objeto (Tmp)", Description="Temporary Key", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdObjeto
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdObjeto.IsNullOrEmpty())
	    	                this._TemporaryIdObjeto = this._IdObjeto;
	    	          return this._TemporaryIdObjeto;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdObjeto != value)
	    	              this._TemporaryIdObjeto = value;
	    	    }
	    }	

	    #endregion Data Properties

	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<TcsObjetoConteudoAutorizacao> _TcsObjetoConteudoAutorizacaoList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsObjetoAutorizacao_TcsObjetoConteudoAutorizacao", "IdObjeto", "IdObjeto", IsForeignKey=false)]
	    [DataMember(Name = "TcsObjetoConteudoAutorizacaoList", EmitDefaultValue = true)]
	    public IEnumerable<TcsObjetoConteudoAutorizacao> TcsObjetoConteudoAutorizacaoList
	    {
	        get
	        {
	
	            if (this._TcsObjetoConteudoAutorizacaoList == null)
	            	this._TcsObjetoConteudoAutorizacaoList = new List<TcsObjetoConteudoAutorizacao>();
	
	            return this._TcsObjetoConteudoAutorizacaoList;
	        }
	        set
	        {
	            if (this._TcsObjetoConteudoAutorizacaoList != value)
	            {
	                this._TcsObjetoConteudoAutorizacaoList = value;
	                this.RaisePropertyChanged("TcsObjetoConteudoAutorizacaoList");
	            }
	        }
	    }	 
		
	    private IEnumerable<TcsTransacaoAutorizacaoChild> _TcsTransacaoAutorizacaoChildList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsObjetoAutorizacao_TcsTransacaoAutorizacaoChild", "IdObjeto", "IdObjeto", IsForeignKey=false)]
	    [DataMember(Name = "TcsTransacaoAutorizacaoChildList", EmitDefaultValue = true)]
	    public IEnumerable<TcsTransacaoAutorizacaoChild> TcsTransacaoAutorizacaoChildList
	    {
	        get
	        {
	
	            if (this._TcsTransacaoAutorizacaoChildList == null)
	            	this._TcsTransacaoAutorizacaoChildList = new List<TcsTransacaoAutorizacaoChild>();
	
	            return this._TcsTransacaoAutorizacaoChildList;
	        }
	        set
	        {
	            if (this._TcsTransacaoAutorizacaoChildList != value)
	            {
	                this._TcsTransacaoAutorizacaoChildList = value;
	                this.RaisePropertyChanged("TcsTransacaoAutorizacaoChildList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_OBJETO_AUTORIZACAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_OBJETO_AUTORIZACAO), QualifiedEntitySetName = "AutorizacaoContext.TCS_OBJETO_AUTORIZACAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO_AUTORIZACAO.ID_OBJETO", Source = "IdObjeto", Target = "ID_OBJETO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_OBJETO_AUTORIZACAO", RelationPropertyName = "TCS_OBJETO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO_AUTORIZACAO.CLASSE_NOME", Source = "ClasseNome", Target = "CLASSE_NOME", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_OBJETO_AUTORIZACAO", RelationPropertyName = "TCS_OBJETO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO_AUTORIZACAO.DESC_OBJETO", Source = "DescObjeto", Target = "DESC_OBJETO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_OBJETO_AUTORIZACAO", RelationPropertyName = "TCS_OBJETO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO_AUTORIZACAO.PATH_OBJETO", Source = "PathObjeto", Target = "PATH_OBJETO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_OBJETO_AUTORIZACAO", RelationPropertyName = "TCS_OBJETO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO_AUTORIZACAO.LX_TIPO_OBJETO", Source = "LxTipoObjeto", Target = "LX_TIPO_OBJETO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_OBJETO_AUTORIZACAO", RelationPropertyName = "TCS_OBJETO_AUTORIZACAO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxTipoObjetoValues()
	    {
	    	    return Linx.Framework.BV.Domains.TipoObjeto.GetValues();
	    }
	    private string _lxTipoObjetoName;
	    [DataMember(IsRequired = false, Name = "LxTipoObjetoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo Objeto", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxTipoObjetoName
	    {
	    	    get { if (this.LxTipoObjeto.IsNull()) { _lxTipoObjetoName = String.Empty; } else { string key = this.LxTipoObjeto.ToString(); var dmValues = this.GetLxTipoObjetoValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxTipoObjetoName) _lxTipoObjetoName = domainName; } return _lxTipoObjetoName; } set { _lxTipoObjetoName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_TRANSACAO_AUTORIZACAO.ID_TRANSACAO", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Transação];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTransacao];ReadOnly[true];Entities[TCS_TRANSACAO_AUTORIZACAO:IdTransacao];SubQueryInfo[Select 1 From #ParentAlias#.TCS_TRANSACAO_AUTORIZACAO_LISTA as #Alias#];EdmEntityName[TCS_TRANSACAO_AUTORIZACAO];EntityRelations[TCS_MODULO_AUTORIZACAO(TCS_MODULO_AUTORIZACAO)#TCS_APLICATIVO(TCS_APLICATIVO)#TCS_OBJETO_AUTORIZACAO(TCS_OBJETO_AUTORIZACAO)];EdmParentEntityName[TCS_OBJETO_AUTORIZACAO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsTransacaoAutorizacaoChild")]
	[Serializable()]
	public partial class TcsTransacaoAutorizacaoChild : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(ObjetoAutorizacaoDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsObjetoAutorizacao");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdObjeto"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdObjeto));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsObjetoAutorizacao
	         this.TcsObjetoAutorizacao = (from r in context.GetTcsObjetoAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For ClasseNome
	    partial void OnClasseNomeChanging(System.String value);
	    partial void OnClasseNomeChanged();

	    private System.String _ClasseNome;

	    [DataMember(IsRequired = true, Name = "ClasseNome", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Classe", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_AUTORIZACAO.CLASSE_NOME];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.CLASSE_NOME")]
	    public System.String ClasseNome
	    {
	    	    get
	    	    {
	    	          return _ClasseNome;
	    	    }
	    	    set
	    	    {
	    	          if (this._ClasseNome != value)
	    	          {
	    	              this.ValidateProperty("ClasseNome", value);
	    	              this.OnClasseNomeChanging(value);
	    	              this.RaiseDataMemberChanging("ClasseNome");
	    	              this._ClasseNome = value;
	    	              this.RaiseDataMemberChanged("ClasseNome");
	    	              this.OnClasseNomeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For CodTransacao
	    partial void OnCodTransacaoChanging(System.String value);
	    partial void OnCodTransacaoChanged();

	    private System.String _CodTransacao;

	    [DataMember(IsRequired = true, Name = "CodTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Código", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_AUTORIZACAO.COD_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.COD_TRANSACAO")]
	    public System.String CodTransacao
	    {
	    	    get
	    	    {
	    	          return _CodTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._CodTransacao != value)
	    	          {
	    	              this.ValidateProperty("CodTransacao", value);
	    	              this.OnCodTransacaoChanging(value);
	    	              this.RaiseDataMemberChanging("CodTransacao");
	    	              this._CodTransacao = value;
	    	              this.RaiseDataMemberChanged("CodTransacao");
	    	              this.OnCodTransacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescTransacao
	    partial void OnDescTransacaoChanging(System.String value);
	    partial void OnDescTransacaoChanged();

	    private System.String _DescTransacao;

	    [DataMember(IsRequired = true, Name = "DescTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_AUTORIZACAO.DESC_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.DESC_TRANSACAO")]
	    public System.String DescTransacao
	    {
	    	    get
	    	    {
	    	          return _DescTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescTransacao != value)
	    	          {
	    	              this.ValidateProperty("DescTransacao", value);
	    	              this.OnDescTransacaoChanging(value);
	    	              this.RaiseDataMemberChanging("DescTransacao");
	    	              this._DescTransacao = value;
	    	              this.RaiseDataMemberChanged("DescTransacao");
	    	              this.OnDescTransacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdObjeto
	    partial void OnIdObjetoChanging(Int64 value);
	    partial void OnIdObjetoChanged();

	    private Int64 _IdObjeto;

	    [DataMember(IsRequired = true, Name = "IdObjeto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Objeto", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.ID_OBJETO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.ID_OBJETO")]
	    public Int64 IdObjeto
	    {
	    	    get
	    	    {
	    	          return _IdObjeto;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdObjeto != value)
	    	          {
	    	              this.ValidateProperty("IdObjeto", value);
	    	              this.OnIdObjetoChanging(value);
	    	              this.RaiseDataMemberChanging("IdObjeto");
	    	              this._IdObjeto = value;
	    	              this.RaiseDataMemberChanged("IdObjeto");
	    	              this.OnIdObjetoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTransacao
	    partial void OnIdTransacaoChanging(Int64 value);
	    partial void OnIdTransacaoChanged();

	    private Int64 _IdTransacao;

	    [DataMember(IsRequired = true, Name = "IdTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Transacao", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_AUTORIZACAO.ID_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.ID_TRANSACAO")]
	    public Int64 IdTransacao
	    {
	    	    get
	    	    {
	    	          return _IdTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTransacao != value)
	    	          {
	    	              this.ValidateProperty("IdTransacao", value);
	    	              this.OnIdTransacaoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTransacao");
	    	              this._IdTransacao = value;
	    	              this.RaiseDataMemberChanged("IdTransacao");
	    	              this.OnIdTransacaoChanged();
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
	    [Display(Name = "Inativo", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_AUTORIZACAO.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.INATIVO")]
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
	    //Extensibility Partial Method Definitions For LxTipoTransacao
	    partial void OnLxTipoTransacaoChanging(Byte value);
	    partial void OnLxTipoTransacaoChanged();

	    private Byte _LxTipoTransacao;

	    [DataMember(IsRequired = true, Name = "LxTipoTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo Transação", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoTransacao];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_AUTORIZACAO.LX_TIPO_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.LX_TIPO_TRANSACAO")]
	    public Byte LxTipoTransacao
	    {
	    	    get
	    	    {
	    	          return _LxTipoTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoTransacao != value)
	    	          {
	    	              this.ValidateProperty("LxTipoTransacao", value);
	    	              this.OnLxTipoTransacaoChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoTransacao");
	    	              this._LxTipoTransacao = value;
	    	              this.RaiseDataMemberChanged("LxTipoTransacao");
	    	              this.OnLxTipoTransacaoChanged();
	    	          }
	    	    }
	    }

	    private Int64 _TemporaryIdTransacao;
	    [DataMember(Name = "TemporaryIdTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Transacao (Tmp)", Description="Temporary Key", Order = 5, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdTransacao
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTransacao.IsNullOrEmpty())
	    	                this._TemporaryIdTransacao = this._IdTransacao;
	    	          return this._TemporaryIdTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTransacao != value)
	    	              this._TemporaryIdTransacao = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsObjetoAutorizacao _TcsObjetoAutorizacao;
	    [DataMember(Name = "TcsObjetoAutorizacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsObjetoAutorizacao_TcsTransacaoAutorizacaoChild", "IdObjeto", "IdObjeto", IsForeignKey=true)]
	    public TcsObjetoAutorizacao TcsObjetoAutorizacao
	    {
	        get
	        {
	            return this._TcsObjetoAutorizacao;
	        }
	        set
	        {
	            if (this._TcsObjetoAutorizacao != value)
	            {
	                this._TcsObjetoAutorizacao = value;
	                this.RaisePropertyChanged("TcsObjetoAutorizacaoList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_TRANSACAO_AUTORIZACAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_TRANSACAO_AUTORIZACAO), QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_AUTORIZACAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_AUTORIZACAO.INATIVO", Source = "Inativo", Target = "INATIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_AUTORIZACAO.CLASSE_NOME", Source = "ClasseNome", Target = "CLASSE_NOME", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_AUTORIZACAO.ID_TRANSACAO", Source = "IdTransacao", Target = "ID_TRANSACAO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_AUTORIZACAO.COD_TRANSACAO", Source = "CodTransacao", Target = "COD_TRANSACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_AUTORIZACAO.DESC_TRANSACAO", Source = "DescTransacao", Target = "DESC_TRANSACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_AUTORIZACAO.LX_TIPO_TRANSACAO", Source = "LxTipoTransacao", Target = "LX_TIPO_TRANSACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.ID_OBJETO", Source = "IdObjeto", Target = "ID_OBJETO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_OBJETO_AUTORIZACAO", RelationPropertyName = "TCS_OBJETO_AUTORIZACAO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxTipoTransacaoValues()
	    {
	    	    return Linx.Framework.BV.Domains.TipoTransacao.GetValues();
	    }
	    private string _lxTipoTransacaoName;
	    [DataMember(IsRequired = false, Name = "LxTipoTransacaoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo Transação", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxTipoTransacaoName
	    {
	    	    get { if (this.LxTipoTransacao.IsNull()) { _lxTipoTransacaoName = String.Empty; } else { string key = this.LxTipoTransacao.ToString(); var dmValues = this.GetLxTipoTransacaoValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxTipoTransacaoName) _lxTipoTransacaoName = domainName; } return _lxTipoTransacaoName; } set { _lxTipoTransacaoName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_OBJETO_CONTEUDO_AUTORIZACAO.ID_OBJETO_CONTEUDO", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[TcsObjetoConteudoAutorizacao];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdObjetoConteudo];ReadOnly[false];Entities[TCS_OBJETO_CONTEUDO_AUTORIZACAO:IdObjetoConteudo];SubQueryInfo[Select 1 From #ParentAlias#.TCS_OBJETO_CONTEUDO_AUTORIZACAO_LISTA as #Alias#];EdmEntityName[TCS_OBJETO_CONTEUDO_AUTORIZACAO];EntityRelations[TCS_OBJETO_AUTORIZACAO(TCS_OBJETO_AUTORIZACAO)#TCS_LAYOUT_AUTORIZACAO_LISTA(TCS_LAYOUT_AUTORIZACAO)];EdmParentEntityName[TCS_OBJETO_AUTORIZACAO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsObjetoConteudoAutorizacao")]
	[Serializable()]
	public partial class TcsObjetoConteudoAutorizacao : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(ObjetoAutorizacaoDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsObjetoAutorizacao");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdObjeto"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdObjeto));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsObjetoAutorizacao
	         this.TcsObjetoAutorizacao = (from r in context.GetTcsObjetoAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For ConteudoXml
	    partial void OnConteudoXmlChanging(System.String value);
	    partial void OnConteudoXmlChanged();

	    private System.String _ConteudoXml;

	    [DataMember(IsRequired = true, Name = "ConteudoXml", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Conteudo Xml", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_OBJETO_CONTEUDO_AUTORIZACAO.CONTEUDO_XML];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO_CONTEUDO_AUTORIZACAO.CONTEUDO_XML")]
	    public System.String ConteudoXml
	    {
	    	    get
	    	    {
	    	          return _ConteudoXml;
	    	    }
	    	    set
	    	    {
	    	          if (this._ConteudoXml != value)
	    	          {
	    	              this.ValidateProperty("ConteudoXml", value);
	    	              this.OnConteudoXmlChanging(value);
	    	              this.RaiseDataMemberChanging("ConteudoXml");
	    	              this._ConteudoXml = value;
	    	              this.RaiseDataMemberChanged("ConteudoXml");
	    	              this.OnConteudoXmlChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescLayout
	    partial void OnDescLayoutChanging(System.String value);
	    partial void OnDescLayoutChanged();

	    private System.String _DescLayout;

	    [DataMember(Name = "DescLayout", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsLayoutAutorizacaoLista];LookUpTitle[Seleção de (Descrição)];LookUpQuery[executeLookUpTcsLayoutAutorizacaoLista];LookUpFinalize[finalizeLookUpTcsLayoutAutorizacaoLista];LookUpDisplayColumns[{\"DescLayout\" : \"Desc Layout\", \"Detalhes\" : \"Detalhes\", \"Idioma\" : \"Idioma\", \"Inativo\" : \"Inativo\", \"LayoutPadrao\" : \"Layout Padrao\", \"LxTipoLayout\" : \"Lx Tipo Layout\", \"PossuiFiltro\" : \"Possui Filtro\", \"IdObjetoConteudo\" : \"Id Objeto Conteudo\", \"UltAtualizacao\" : \"Ult Atualizacao\", \"IdLayout\" : \"Id Objeto Conteudo\"}];LookUpColumns[{\"DescLayout\" : true, \"Detalhes\" : true, \"Idioma\" : true, \"Inativo\" : true, \"LayoutPadrao\" : true, \"LxTipoLayout\" : true, \"PossuiFiltro\" : true, \"IdObjetoConteudo\" : true, \"UltAtualizacao\" : true, \"IdLayout\" : true}];FilterDataKey[TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_LAYOUT_AUTORIZACAO_LISTA.DESC_LAYOUT];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescLayout#false##60:0##Desc Layout#0#true##::LookUpTcsLayoutAutorizacaoLista##false#false#TCS_LAYOUT_AUTORIZACAO_LISTA#TCS_LAYOUT_AUTORIZACAO#Linx.Framework.BV.ObjetoAutorizacao#IQueryable###true#false", EdmKey="TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_LAYOUT_AUTORIZACAO_LISTA.DESC_LAYOUT")]
	    public System.String DescLayout
	    {
	    	    get
	    	    {
	    	          return _DescLayout;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescLayout != value)
	    	          {
	    	              this.ValidateProperty("DescLayout", value);
	    	              this.OnDescLayoutChanging(value);
	    	              this.RaiseDataMemberChanging("DescLayout");
	    	              this._DescLayout = value;
	    	              this.RaiseDataMemberChanged("DescLayout");
	    	              this.OnDescLayoutChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Detalhes
	    partial void OnDetalhesChanging(System.String value);
	    partial void OnDetalhesChanged();

	    private System.String _Detalhes;

	    [DataMember(Name = "Detalhes", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Detalhes", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(500)]
	    [FunctionalPoint("Precision[500:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsLayoutAutorizacaoLista];LookUpTitle[Seleção de (Detalhes)];LookUpQuery[executeLookUpTcsLayoutAutorizacaoLista];LookUpFinalize[finalizeLookUpTcsLayoutAutorizacaoLista];LookUpDisplayColumns[{\"DescLayout\" : \"Desc Layout\", \"Detalhes\" : \"Detalhes\", \"Idioma\" : \"Idioma\", \"Inativo\" : \"Inativo\", \"LayoutPadrao\" : \"Layout Padrao\", \"LxTipoLayout\" : \"Lx Tipo Layout\", \"PossuiFiltro\" : \"Possui Filtro\", \"IdObjetoConteudo\" : \"Id Objeto Conteudo\", \"UltAtualizacao\" : \"Ult Atualizacao\", \"IdLayout\" : \"Id Objeto Conteudo\"}];LookUpColumns[{\"DescLayout\" : true, \"Detalhes\" : true, \"Idioma\" : true, \"Inativo\" : true, \"LayoutPadrao\" : true, \"LxTipoLayout\" : true, \"PossuiFiltro\" : true, \"IdObjetoConteudo\" : true, \"UltAtualizacao\" : true, \"IdLayout\" : true}];FilterDataKey[TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_LAYOUT_AUTORIZACAO_LISTA.DETALHES];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#Detalhes#false##500:0##Detalhes#1#true##::LookUpTcsLayoutAutorizacaoLista##false#false#TCS_LAYOUT_AUTORIZACAO_LISTA#TCS_LAYOUT_AUTORIZACAO#Linx.Framework.BV.ObjetoAutorizacao#IQueryable###true#false", EdmKey="TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_LAYOUT_AUTORIZACAO_LISTA.DETALHES")]
	    public System.String Detalhes
	    {
	    	    get
	    	    {
	    	          return _Detalhes;
	    	    }
	    	    set
	    	    {
	    	          if (this._Detalhes != value)
	    	          {
	    	              this.ValidateProperty("Detalhes", value);
	    	              this.OnDetalhesChanging(value);
	    	              this.RaiseDataMemberChanging("Detalhes");
	    	              this._Detalhes = value;
	    	              this.RaiseDataMemberChanged("Detalhes");
	    	              this.OnDetalhesChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Idioma
	    partial void OnIdiomaChanging(System.String value);
	    partial void OnIdiomaChanged();

	    private System.String _Idioma;

	    [DataMember(Name = "Idioma", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Idioma", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(18)]
	    [FunctionalPoint("Precision[18:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsLayoutAutorizacaoLista];LookUpTitle[Seleção de (Idioma)];LookUpQuery[executeLookUpTcsLayoutAutorizacaoLista];LookUpFinalize[finalizeLookUpTcsLayoutAutorizacaoLista];LookUpDisplayColumns[{\"DescLayout\" : \"Desc Layout\", \"Detalhes\" : \"Detalhes\", \"Idioma\" : \"Idioma\", \"Inativo\" : \"Inativo\", \"LayoutPadrao\" : \"Layout Padrao\", \"LxTipoLayout\" : \"Lx Tipo Layout\", \"PossuiFiltro\" : \"Possui Filtro\", \"IdObjetoConteudo\" : \"Id Objeto Conteudo\", \"UltAtualizacao\" : \"Ult Atualizacao\", \"IdLayout\" : \"Id Objeto Conteudo\"}];LookUpColumns[{\"DescLayout\" : true, \"Detalhes\" : true, \"Idioma\" : true, \"Inativo\" : true, \"LayoutPadrao\" : true, \"LxTipoLayout\" : true, \"PossuiFiltro\" : true, \"IdObjetoConteudo\" : true, \"UltAtualizacao\" : true, \"IdLayout\" : true}];FilterDataKey[TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_LAYOUT_AUTORIZACAO_LISTA.IDIOMA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#Idioma#false##18:0##Idioma#2#true##::LookUpTcsLayoutAutorizacaoLista##false#false#TCS_LAYOUT_AUTORIZACAO_LISTA#TCS_LAYOUT_AUTORIZACAO#Linx.Framework.BV.ObjetoAutorizacao#IQueryable###true#false", EdmKey="TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_LAYOUT_AUTORIZACAO_LISTA.IDIOMA")]
	    public System.String Idioma
	    {
	    	    get
	    	    {
	    	          return _Idioma;
	    	    }
	    	    set
	    	    {
	    	          if (this._Idioma != value)
	    	          {
	    	              this.ValidateProperty("Idioma", value);
	    	              this.OnIdiomaChanging(value);
	    	              this.RaiseDataMemberChanging("Idioma");
	    	              this._Idioma = value;
	    	              this.RaiseDataMemberChanged("Idioma");
	    	              this.OnIdiomaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLayout
	    partial void OnIdLayoutChanging(System.Nullable<Int64> value);
	    partial void OnIdLayoutChanged();

	    private System.Nullable<Int64> _IdLayout;

	    [DataMember(Name = "IdLayout", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Objeto Conteudo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsLayoutAutorizacaoLista];LookUpTitle[Seleção de (Id Objeto Conteudo)];LookUpQuery[executeLookUpTcsLayoutAutorizacaoLista];LookUpFinalize[finalizeLookUpTcsLayoutAutorizacaoLista];LookUpDisplayColumns[{\"DescLayout\" : \"Desc Layout\", \"Detalhes\" : \"Detalhes\", \"Idioma\" : \"Idioma\", \"Inativo\" : \"Inativo\", \"LayoutPadrao\" : \"Layout Padrao\", \"LxTipoLayout\" : \"Lx Tipo Layout\", \"PossuiFiltro\" : \"Possui Filtro\", \"IdObjetoConteudo\" : \"Id Objeto Conteudo\", \"UltAtualizacao\" : \"Ult Atualizacao\", \"IdLayout\" : \"Id Objeto Conteudo\"}];LookUpColumns[{\"DescLayout\" : true, \"Detalhes\" : true, \"Idioma\" : true, \"Inativo\" : true, \"LayoutPadrao\" : true, \"LxTipoLayout\" : true, \"PossuiFiltro\" : true, \"IdObjetoConteudo\" : true, \"UltAtualizacao\" : true, \"IdLayout\" : true}];FilterDataKey[TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_LAYOUT_AUTORIZACAO_LISTA.ID_OBJETO_CONTEUDO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<Int64>#IdLayout#true##24:0##Id Objeto Conteudo#9#true##::LookUpTcsLayoutAutorizacaoLista##false#false#TCS_LAYOUT_AUTORIZACAO_LISTA#TCS_LAYOUT_AUTORIZACAO#Linx.Framework.BV.ObjetoAutorizacao#IQueryable###true#false", EdmKey="TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_LAYOUT_AUTORIZACAO_LISTA.ID_OBJETO_CONTEUDO")]
	    public System.Nullable<Int64> IdLayout
	    {
	    	    get
	    	    {
	    	          return _IdLayout;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLayout != value)
	    	          {
	    	              this.ValidateProperty("IdLayout", value);
	    	              this.OnIdLayoutChanging(value);
	    	              this.RaiseDataMemberChanging("IdLayout");
	    	              this._IdLayout = value;
	    	              this.RaiseDataMemberChanged("IdLayout");
	    	              this.OnIdLayoutChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdObjeto
	    partial void OnIdObjetoChanging(Int64 value);
	    partial void OnIdObjetoChanged();

	    private Int64 _IdObjeto;

	    [DataMember(IsRequired = true, Name = "IdObjeto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Objeto", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.ID_OBJETO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.ID_OBJETO")]
	    public Int64 IdObjeto
	    {
	    	    get
	    	    {
	    	          return _IdObjeto;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdObjeto != value)
	    	          {
	    	              this.ValidateProperty("IdObjeto", value);
	    	              this.OnIdObjetoChanging(value);
	    	              this.RaiseDataMemberChanging("IdObjeto");
	    	              this._IdObjeto = value;
	    	              this.RaiseDataMemberChanged("IdObjeto");
	    	              this.OnIdObjetoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdObjetoConteudo
	    partial void OnIdObjetoConteudoChanging(Int64 value);
	    partial void OnIdObjetoConteudoChanged();

	    private Int64 _IdObjetoConteudo;

	    [DataMember(IsRequired = true, Name = "IdObjetoConteudo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Objeto Conteudo1", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsLayoutAutorizacaoLista];LookUpTitle[Seleção de (Id Objeto Conteudo1)];LookUpQuery[executeLookUpTcsLayoutAutorizacaoLista];LookUpFinalize[finalizeLookUpTcsLayoutAutorizacaoLista];LookUpDisplayColumns[{\"DescLayout\" : \"Desc Layout\", \"Detalhes\" : \"Detalhes\", \"Idioma\" : \"Idioma\", \"Inativo\" : \"Inativo\", \"LayoutPadrao\" : \"Layout Padrao\", \"LxTipoLayout\" : \"Lx Tipo Layout\", \"PossuiFiltro\" : \"Possui Filtro\", \"IdObjetoConteudo\" : \"Id Objeto Conteudo\", \"UltAtualizacao\" : \"Ult Atualizacao\", \"IdLayout\" : \"Id Objeto Conteudo\"}];LookUpColumns[{\"DescLayout\" : true, \"Detalhes\" : true, \"Idioma\" : true, \"Inativo\" : true, \"LayoutPadrao\" : true, \"LxTipoLayout\" : true, \"PossuiFiltro\" : true, \"IdObjetoConteudo\" : true, \"UltAtualizacao\" : true, \"IdLayout\" : true}];FilterDataKey[TCS_OBJETO_CONTEUDO_AUTORIZACAO.ID_OBJETO_CONTEUDO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<Int64>#IdObjetoConteudo#true##24:0##Id Objeto Conteudo#7#true##::LookUpTcsLayoutAutorizacaoLista##false#false#TCS_LAYOUT_AUTORIZACAO_LISTA#TCS_LAYOUT_AUTORIZACAO#Linx.Framework.BV.ObjetoAutorizacao#IQueryable###true#false", EdmKey="TCS_OBJETO_CONTEUDO_AUTORIZACAO.ID_OBJETO_CONTEUDO")]
	    public Int64 IdObjetoConteudo
	    {
	    	    get
	    	    {
	    	          return _IdObjetoConteudo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdObjetoConteudo != value)
	    	          {
	    	              this.ValidateProperty("IdObjetoConteudo", value);
	    	              this.OnIdObjetoConteudoChanging(value);
	    	              this.RaiseDataMemberChanging("IdObjetoConteudo");
	    	              this._IdObjetoConteudo = value;
	    	              this.RaiseDataMemberChanged("IdObjetoConteudo");
	    	              this.OnIdObjetoConteudoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Inativo
	    partial void OnInativoChanging(System.Nullable<Boolean> value);
	    partial void OnInativoChanged();

	    private System.Nullable<Boolean> _Inativo;

	    [DataMember(Name = "Inativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inativo", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsLayoutAutorizacaoLista];LookUpTitle[Seleção de (Inativo)];LookUpQuery[executeLookUpTcsLayoutAutorizacaoLista];LookUpFinalize[finalizeLookUpTcsLayoutAutorizacaoLista];LookUpDisplayColumns[{\"DescLayout\" : \"Desc Layout\", \"Detalhes\" : \"Detalhes\", \"Idioma\" : \"Idioma\", \"Inativo\" : \"Inativo\", \"LayoutPadrao\" : \"Layout Padrao\", \"LxTipoLayout\" : \"Lx Tipo Layout\", \"PossuiFiltro\" : \"Possui Filtro\", \"IdObjetoConteudo\" : \"Id Objeto Conteudo\", \"UltAtualizacao\" : \"Ult Atualizacao\", \"IdLayout\" : \"Id Objeto Conteudo\"}];LookUpColumns[{\"DescLayout\" : true, \"Detalhes\" : true, \"Idioma\" : true, \"Inativo\" : true, \"LayoutPadrao\" : true, \"LxTipoLayout\" : true, \"PossuiFiltro\" : true, \"IdObjetoConteudo\" : true, \"UltAtualizacao\" : true, \"IdLayout\" : true}];FilterDataKey[TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_LAYOUT_AUTORIZACAO_LISTA.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<Boolean>#Inativo#false##0:0##Inativo#3#true##::LookUpTcsLayoutAutorizacaoLista##false#false#TCS_LAYOUT_AUTORIZACAO_LISTA#TCS_LAYOUT_AUTORIZACAO#Linx.Framework.BV.ObjetoAutorizacao#IQueryable###true#false", EdmKey="TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_LAYOUT_AUTORIZACAO_LISTA.INATIVO")]
	    public System.Nullable<Boolean> Inativo
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
	    //Extensibility Partial Method Definitions For LayoutLinx
	    partial void OnLayoutLinxChanging(bool value);
	    partial void OnLayoutLinxChanged();

	    private bool _LayoutLinx;

	    [DataMember(IsRequired = true, Name = "LayoutLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[true];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="true")]
	    public bool LayoutLinx
	    {
	    	    get
	    	    {
	    	          return _LayoutLinx;
	    	    }
	    	    set
	    	    {
	    	          if (this._LayoutLinx != value)
	    	          {
	    	              this.ValidateProperty("LayoutLinx", value);
	    	              this.OnLayoutLinxChanging(value);
	    	              this.RaiseDataMemberChanging("LayoutLinx");
	    	              this._LayoutLinx = value;
	    	              this.RaiseDataMemberChanged("LayoutLinx");
	    	              this.OnLayoutLinxChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LayoutPadrao
	    partial void OnLayoutPadraoChanging(System.Nullable<Boolean> value);
	    partial void OnLayoutPadraoChanged();

	    private System.Nullable<Boolean> _LayoutPadrao;

	    [DataMember(Name = "LayoutPadrao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Padrão", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsLayoutAutorizacaoLista];LookUpTitle[Seleção de (Padrão)];LookUpQuery[executeLookUpTcsLayoutAutorizacaoLista];LookUpFinalize[finalizeLookUpTcsLayoutAutorizacaoLista];LookUpDisplayColumns[{\"DescLayout\" : \"Desc Layout\", \"Detalhes\" : \"Detalhes\", \"Idioma\" : \"Idioma\", \"Inativo\" : \"Inativo\", \"LayoutPadrao\" : \"Layout Padrao\", \"LxTipoLayout\" : \"Lx Tipo Layout\", \"PossuiFiltro\" : \"Possui Filtro\", \"IdObjetoConteudo\" : \"Id Objeto Conteudo\", \"UltAtualizacao\" : \"Ult Atualizacao\", \"IdLayout\" : \"Id Objeto Conteudo\"}];LookUpColumns[{\"DescLayout\" : true, \"Detalhes\" : true, \"Idioma\" : true, \"Inativo\" : true, \"LayoutPadrao\" : true, \"LxTipoLayout\" : true, \"PossuiFiltro\" : true, \"IdObjetoConteudo\" : true, \"UltAtualizacao\" : true, \"IdLayout\" : true}];FilterDataKey[TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_LAYOUT_AUTORIZACAO_LISTA.LAYOUT_PADRAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<Boolean>#LayoutPadrao#false##0:0##Layout Padrao#4#true##::LookUpTcsLayoutAutorizacaoLista##false#false#TCS_LAYOUT_AUTORIZACAO_LISTA#TCS_LAYOUT_AUTORIZACAO#Linx.Framework.BV.ObjetoAutorizacao#IQueryable###true#false", EdmKey="TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_LAYOUT_AUTORIZACAO_LISTA.LAYOUT_PADRAO")]
	    public System.Nullable<Boolean> LayoutPadrao
	    {
	    	    get
	    	    {
	    	          return _LayoutPadrao;
	    	    }
	    	    set
	    	    {
	    	          if (this._LayoutPadrao != value)
	    	          {
	    	              this.ValidateProperty("LayoutPadrao", value);
	    	              this.OnLayoutPadraoChanging(value);
	    	              this.RaiseDataMemberChanging("LayoutPadrao");
	    	              this._LayoutPadrao = value;
	    	              this.RaiseDataMemberChanged("LayoutPadrao");
	    	              this.OnLayoutPadraoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxConteudoObjeto
	    partial void OnLxConteudoObjetoChanging(System.String value);
	    partial void OnLxConteudoObjetoChanged();

	    private System.String _LxConteudoObjeto;

	    [DataMember(Name = "LxConteudoObjeto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo Conteúdo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[TipoConteudoObjeto];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_OBJETO_CONTEUDO_AUTORIZACAO.LX_CONTEUDO_OBJETO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO_CONTEUDO_AUTORIZACAO.LX_CONTEUDO_OBJETO")]
	    public System.String LxConteudoObjeto
	    {
	    	    get
	    	    {
	    	          return _LxConteudoObjeto;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxConteudoObjeto != value)
	    	          {
	    	              this.ValidateProperty("LxConteudoObjeto", value);
	    	              this.OnLxConteudoObjetoChanging(value);
	    	              this.RaiseDataMemberChanging("LxConteudoObjeto");
	    	              this._LxConteudoObjeto = value;
	    	              this.RaiseDataMemberChanged("LxConteudoObjeto");
	    	              this.OnLxConteudoObjetoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoLayout
	    partial void OnLxTipoLayoutChanging(System.Nullable<Byte> value);
	    partial void OnLxTipoLayoutChanged();

	    private System.Nullable<Byte> _LxTipoLayout;

	    [DataMember(Name = "LxTipoLayout", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[TipoLayout];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsLayoutAutorizacaoLista];LookUpTitle[Seleção de (Tipo)];LookUpQuery[executeLookUpTcsLayoutAutorizacaoLista];LookUpFinalize[finalizeLookUpTcsLayoutAutorizacaoLista];LookUpDisplayColumns[{\"DescLayout\" : \"Desc Layout\", \"Detalhes\" : \"Detalhes\", \"Idioma\" : \"Idioma\", \"Inativo\" : \"Inativo\", \"LayoutPadrao\" : \"Layout Padrao\", \"LxTipoLayout\" : \"Lx Tipo Layout\", \"PossuiFiltro\" : \"Possui Filtro\", \"IdObjetoConteudo\" : \"Id Objeto Conteudo\", \"UltAtualizacao\" : \"Ult Atualizacao\", \"IdLayout\" : \"Id Objeto Conteudo\"}];LookUpColumns[{\"DescLayout\" : true, \"Detalhes\" : true, \"Idioma\" : true, \"Inativo\" : true, \"LayoutPadrao\" : true, \"LxTipoLayout\" : true, \"PossuiFiltro\" : true, \"IdObjetoConteudo\" : true, \"UltAtualizacao\" : true, \"IdLayout\" : true}];FilterDataKey[TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_LAYOUT_AUTORIZACAO_LISTA.LX_TIPO_LAYOUT];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<Byte>#LxTipoLayout#false##3:0##Lx Tipo Layout#5#true##::LookUpTcsLayoutAutorizacaoLista##false#false#TCS_LAYOUT_AUTORIZACAO_LISTA#TCS_LAYOUT_AUTORIZACAO#Linx.Framework.BV.ObjetoAutorizacao#IQueryable###true#false", EdmKey="TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_LAYOUT_AUTORIZACAO_LISTA.LX_TIPO_LAYOUT")]
	    public System.Nullable<Byte> LxTipoLayout
	    {
	    	    get
	    	    {
	    	          return _LxTipoLayout;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoLayout != value)
	    	          {
	    	              this.ValidateProperty("LxTipoLayout", value);
	    	              this.OnLxTipoLayoutChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoLayout");
	    	              this._LxTipoLayout = value;
	    	              this.RaiseDataMemberChanged("LxTipoLayout");
	    	              this.OnLxTipoLayoutChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For PossuiFiltro
	    partial void OnPossuiFiltroChanging(System.Nullable<Boolean> value);
	    partial void OnPossuiFiltroChanged();

	    private System.Nullable<Boolean> _PossuiFiltro;

	    [DataMember(Name = "PossuiFiltro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Possui Filtro", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsLayoutAutorizacaoLista];LookUpTitle[Seleção de (Possui Filtro)];LookUpQuery[executeLookUpTcsLayoutAutorizacaoLista];LookUpFinalize[finalizeLookUpTcsLayoutAutorizacaoLista];LookUpDisplayColumns[{\"DescLayout\" : \"Desc Layout\", \"Detalhes\" : \"Detalhes\", \"Idioma\" : \"Idioma\", \"Inativo\" : \"Inativo\", \"LayoutPadrao\" : \"Layout Padrao\", \"LxTipoLayout\" : \"Lx Tipo Layout\", \"PossuiFiltro\" : \"Possui Filtro\", \"IdObjetoConteudo\" : \"Id Objeto Conteudo\", \"UltAtualizacao\" : \"Ult Atualizacao\", \"IdLayout\" : \"Id Objeto Conteudo\"}];LookUpColumns[{\"DescLayout\" : true, \"Detalhes\" : true, \"Idioma\" : true, \"Inativo\" : true, \"LayoutPadrao\" : true, \"LxTipoLayout\" : true, \"PossuiFiltro\" : true, \"IdObjetoConteudo\" : true, \"UltAtualizacao\" : true, \"IdLayout\" : true}];FilterDataKey[TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_LAYOUT_AUTORIZACAO_LISTA.POSSUI_FILTRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<Boolean>#PossuiFiltro#false##0:0##Possui Filtro#6#true##::LookUpTcsLayoutAutorizacaoLista##false#false#TCS_LAYOUT_AUTORIZACAO_LISTA#TCS_LAYOUT_AUTORIZACAO#Linx.Framework.BV.ObjetoAutorizacao#IQueryable###true#false", EdmKey="TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_LAYOUT_AUTORIZACAO_LISTA.POSSUI_FILTRO")]
	    public System.Nullable<Boolean> PossuiFiltro
	    {
	    	    get
	    	    {
	    	          return _PossuiFiltro;
	    	    }
	    	    set
	    	    {
	    	          if (this._PossuiFiltro != value)
	    	          {
	    	              this.ValidateProperty("PossuiFiltro", value);
	    	              this.OnPossuiFiltroChanging(value);
	    	              this.RaiseDataMemberChanging("PossuiFiltro");
	    	              this._PossuiFiltro = value;
	    	              this.RaiseDataMemberChanged("PossuiFiltro");
	    	              this.OnPossuiFiltroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Publico
	    partial void OnPublicoChanging(System.Nullable<Boolean> value);
	    partial void OnPublicoChanged();

	    private System.Nullable<Boolean> _Publico;

	    [DataMember(IsRequired = true, Name = "Publico", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[true];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="true")]
	    public System.Nullable<Boolean> Publico
	    {
	    	    get
	    	    {
	    	          return _Publico;
	    	    }
	    	    set
	    	    {
	    	          if (this._Publico != value)
	    	          {
	    	              this.ValidateProperty("Publico", value);
	    	              this.OnPublicoChanging(value);
	    	              this.RaiseDataMemberChanging("Publico");
	    	              this._Publico = value;
	    	              this.RaiseDataMemberChanged("Publico");
	    	              this.OnPublicoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UltAtualizacao
	    partial void OnUltAtualizacaoChanging(System.Nullable<System.DateTime> value);
	    partial void OnUltAtualizacaoChanged();

	    private System.Nullable<System.DateTime> _UltAtualizacao;

	    [DataMember(Name = "UltAtualizacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Última Atualização", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsLayoutAutorizacaoLista];LookUpTitle[Seleção de (Última Atualização)];LookUpQuery[executeLookUpTcsLayoutAutorizacaoLista];LookUpFinalize[finalizeLookUpTcsLayoutAutorizacaoLista];LookUpDisplayColumns[{\"DescLayout\" : \"Desc Layout\", \"Detalhes\" : \"Detalhes\", \"Idioma\" : \"Idioma\", \"Inativo\" : \"Inativo\", \"LayoutPadrao\" : \"Layout Padrao\", \"LxTipoLayout\" : \"Lx Tipo Layout\", \"PossuiFiltro\" : \"Possui Filtro\", \"IdObjetoConteudo\" : \"Id Objeto Conteudo\", \"UltAtualizacao\" : \"Ult Atualizacao\", \"IdLayout\" : \"Id Objeto Conteudo\"}];LookUpColumns[{\"DescLayout\" : true, \"Detalhes\" : true, \"Idioma\" : true, \"Inativo\" : true, \"LayoutPadrao\" : true, \"LxTipoLayout\" : true, \"PossuiFiltro\" : true, \"IdObjetoConteudo\" : true, \"UltAtualizacao\" : true, \"IdLayout\" : true}];FilterDataKey[TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_LAYOUT_AUTORIZACAO_LISTA.ULT_ATUALIZACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<System.DateTime>#UltAtualizacao#false##10:0##Ult Atualizacao#8#true##::LookUpTcsLayoutAutorizacaoLista##false#false#TCS_LAYOUT_AUTORIZACAO_LISTA#TCS_LAYOUT_AUTORIZACAO#Linx.Framework.BV.ObjetoAutorizacao#IQueryable###true#false", EdmKey="TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_LAYOUT_AUTORIZACAO_LISTA.ULT_ATUALIZACAO")]
	    public System.Nullable<System.DateTime> UltAtualizacao
	    {
	    	    get
	    	    {
	    	          return _UltAtualizacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._UltAtualizacao != value)
	    	          {
	    	              this.ValidateProperty("UltAtualizacao", value);
	    	              this.OnUltAtualizacaoChanging(value);
	    	              this.RaiseDataMemberChanging("UltAtualizacao");
	    	              this._UltAtualizacao = value;
	    	              this.RaiseDataMemberChanged("UltAtualizacao");
	    	              this.OnUltAtualizacaoChanged();
	    	          }
	    	    }
	    }

	    private Int64 _TemporaryIdObjetoConteudo;
	    [DataMember(Name = "TemporaryIdObjetoConteudo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Objeto Conteudo1 (Tmp)", Description="Temporary Key", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdObjetoConteudo
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdObjetoConteudo.IsNullOrEmpty())
	    	                this._TemporaryIdObjetoConteudo = this._IdObjetoConteudo;
	    	          return this._TemporaryIdObjetoConteudo;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdObjetoConteudo != value)
	    	              this._TemporaryIdObjetoConteudo = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsObjetoAutorizacao _TcsObjetoAutorizacao;
	    [DataMember(Name = "TcsObjetoAutorizacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsObjetoAutorizacao_TcsObjetoConteudoAutorizacao", "IdObjeto", "IdObjeto", IsForeignKey=true)]
	    public TcsObjetoAutorizacao TcsObjetoAutorizacao
	    {
	        get
	        {
	            return this._TcsObjetoAutorizacao;
	        }
	        set
	        {
	            if (this._TcsObjetoAutorizacao != value)
	            {
	                this._TcsObjetoAutorizacao = value;
	                this.RaisePropertyChanged("TcsObjetoAutorizacaoList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_OBJETO_CONTEUDO_AUTORIZACAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_OBJETO_CONTEUDO_AUTORIZACAO), QualifiedEntitySetName = "AutorizacaoContext.TCS_OBJETO_CONTEUDO_AUTORIZACAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO_CONTEUDO_AUTORIZACAO.CONTEUDO_XML", Source = "ConteudoXml", Target = "CONTEUDO_XML", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_OBJETO_CONTEUDO_AUTORIZACAO", RelationPropertyName = "TCS_OBJETO_CONTEUDO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO_CONTEUDO_AUTORIZACAO.ID_OBJETO_CONTEUDO", Source = "IdObjetoConteudo", Target = "ID_OBJETO_CONTEUDO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_OBJETO_CONTEUDO_AUTORIZACAO", RelationPropertyName = "TCS_OBJETO_CONTEUDO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO_CONTEUDO_AUTORIZACAO.LX_CONTEUDO_OBJETO", Source = "LxConteudoObjeto", Target = "LX_CONTEUDO_OBJETO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_OBJETO_CONTEUDO_AUTORIZACAO", RelationPropertyName = "TCS_OBJETO_CONTEUDO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.ID_OBJETO", Source = "IdObjeto", Target = "ID_OBJETO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_OBJETO_AUTORIZACAO", RelationPropertyName = "TCS_OBJETO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_LAYOUT_AUTORIZACAO_LISTA.ID_OBJETO_CONTEUDO", Source = "IdLayout", Target = "ID_OBJETO_CONTEUDO", TargetKeyName = "ID_OBJETO_CONTEUDO", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_LAYOUT_AUTORIZACAO", RelationPropertyName = "TCS_LAYOUT_AUTORIZACAO_LISTA" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxConteudoObjetoValues()
	    {
	    	    return Linx.Framework.BV.Domains.TipoConteudoObjeto.GetValues();
	    }
	    private string _lxConteudoObjetoName;
	    [DataMember(IsRequired = false, Name = "LxConteudoObjetoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo Conteúdo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxConteudoObjetoName
	    {
	    	    get { if (this.LxConteudoObjeto.IsNull()) { _lxConteudoObjetoName = String.Empty; } else { string key = this.LxConteudoObjeto.ToString(); var dmValues = this.GetLxConteudoObjetoValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxConteudoObjetoName) _lxConteudoObjetoName = domainName; } return _lxConteudoObjetoName; } set { _lxConteudoObjetoName = value;  }
	    }
	    public Dictionary<string, string> GetLxTipoLayoutValues()
	    {
	    	    return Linx.Framework.BV.Domains.TipoLayout.GetValues();
	    }
	    private string _lxTipoLayoutName;
	    [DataMember(IsRequired = false, Name = "LxTipoLayoutName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxTipoLayoutName
	    {
	    	    get { if (this.LxTipoLayout.IsNull()) { _lxTipoLayoutName = String.Empty; } else { string key = this.LxTipoLayout.ToString(); var dmValues = this.GetLxTipoLayoutValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxTipoLayoutName) _lxTipoLayoutName = domainName; } return _lxTipoLayoutName; } set { _lxTipoLayoutName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Transação];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTransacao];ReadOnly[true];Entities[TCS_TRANSACAO_AUTORIZACAO:IdTransacao];SubQueryInfo[Select 1 From #ParentAlias#.TCS_TRANSACAO_AUTORIZACAO_LISTA as #Alias#];EdmEntityName[TCS_TRANSACAO_AUTORIZACAO];EntityRelations[TCS_MODULO_AUTORIZACAO(TCS_MODULO_AUTORIZACAO)#TCS_APLICATIVO(TCS_APLICATIVO)#TCS_OBJETO_AUTORIZACAO(TCS_OBJETO_AUTORIZACAO)];EdmParentEntityName[TCS_OBJETO_AUTORIZACAO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsTransacaoAutorizacaoChild")]
	[Serializable()]
	public partial class TcsTransacaoAutorizacaoChildParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For ClasseNome
	    partial void OnClasseNomeChanging(System.String value);
	    partial void OnClasseNomeChanged();

	    private System.String _ClasseNome;

	    [DataMember(IsRequired = true, Name = "ClasseNome", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Classe", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_AUTORIZACAO.CLASSE_NOME];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.CLASSE_NOME")]
	    public System.String ClasseNome
	    {
	    	    get
	    	    {
	    	          return _ClasseNome;
	    	    }
	    	    set
	    	    {
	    	          if (this._ClasseNome != value)
	    	          {
	    	              this.ValidateProperty("ClasseNome", value);
	    	              this.OnClasseNomeChanging(value);
	    	              this.RaiseDataMemberChanging("ClasseNome");
	    	              this._ClasseNome = value;
	    	              this.RaiseDataMemberChanged("ClasseNome");
	    	              this.OnClasseNomeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For CodTransacao
	    partial void OnCodTransacaoChanging(System.String value);
	    partial void OnCodTransacaoChanged();

	    private System.String _CodTransacao;

	    [DataMember(IsRequired = true, Name = "CodTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Código", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_AUTORIZACAO.COD_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.COD_TRANSACAO")]
	    public System.String CodTransacao
	    {
	    	    get
	    	    {
	    	          return _CodTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._CodTransacao != value)
	    	          {
	    	              this.ValidateProperty("CodTransacao", value);
	    	              this.OnCodTransacaoChanging(value);
	    	              this.RaiseDataMemberChanging("CodTransacao");
	    	              this._CodTransacao = value;
	    	              this.RaiseDataMemberChanged("CodTransacao");
	    	              this.OnCodTransacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescTransacao
	    partial void OnDescTransacaoChanging(System.String value);
	    partial void OnDescTransacaoChanged();

	    private System.String _DescTransacao;

	    [DataMember(IsRequired = true, Name = "DescTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_AUTORIZACAO.DESC_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.DESC_TRANSACAO")]
	    public System.String DescTransacao
	    {
	    	    get
	    	    {
	    	          return _DescTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescTransacao != value)
	    	          {
	    	              this.ValidateProperty("DescTransacao", value);
	    	              this.OnDescTransacaoChanging(value);
	    	              this.RaiseDataMemberChanging("DescTransacao");
	    	              this._DescTransacao = value;
	    	              this.RaiseDataMemberChanged("DescTransacao");
	    	              this.OnDescTransacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdObjeto
	    partial void OnIdObjetoChanging(Int64 value);
	    partial void OnIdObjetoChanged();

	    private Int64 _IdObjeto;

	    [DataMember(IsRequired = true, Name = "IdObjeto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Objeto", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.ID_OBJETO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.ID_OBJETO")]
	    public Int64 IdObjeto
	    {
	    	    get
	    	    {
	    	          return _IdObjeto;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdObjeto != value)
	    	          {
	    	              this.ValidateProperty("IdObjeto", value);
	    	              this.OnIdObjetoChanging(value);
	    	              this.RaiseDataMemberChanging("IdObjeto");
	    	              this._IdObjeto = value;
	    	              this.RaiseDataMemberChanged("IdObjeto");
	    	              this.OnIdObjetoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTransacao
	    partial void OnIdTransacaoChanging(Int64 value);
	    partial void OnIdTransacaoChanged();

	    private Int64 _IdTransacao;

	    [DataMember(IsRequired = true, Name = "IdTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Transacao", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_AUTORIZACAO.ID_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.ID_TRANSACAO")]
	    public Int64 IdTransacao
	    {
	    	    get
	    	    {
	    	          return _IdTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTransacao != value)
	    	          {
	    	              this.ValidateProperty("IdTransacao", value);
	    	              this.OnIdTransacaoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTransacao");
	    	              this._IdTransacao = value;
	    	              this.RaiseDataMemberChanged("IdTransacao");
	    	              this.OnIdTransacaoChanged();
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
	    [Display(Name = "Inativo", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_AUTORIZACAO.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.INATIVO")]
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
	    //Extensibility Partial Method Definitions For LxTipoTransacao
	    partial void OnLxTipoTransacaoChanging(Byte value);
	    partial void OnLxTipoTransacaoChanged();

	    private Byte _LxTipoTransacao;

	    [DataMember(IsRequired = true, Name = "LxTipoTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo Transação", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoTransacao];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_AUTORIZACAO.LX_TIPO_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.LX_TIPO_TRANSACAO")]
	    public Byte LxTipoTransacao
	    {
	    	    get
	    	    {
	    	          return _LxTipoTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoTransacao != value)
	    	          {
	    	              this.ValidateProperty("LxTipoTransacao", value);
	    	              this.OnLxTipoTransacaoChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoTransacao");
	    	              this._LxTipoTransacao = value;
	    	              this.RaiseDataMemberChanged("LxTipoTransacao");
	    	              this.OnLxTipoTransacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescObjeto
	    partial void OnDescObjetoChanging(System.String value);
	    partial void OnDescObjetoChanged();

	    private System.String _DescObjeto;

	    [DataMember(IsRequired = true, Name = "DescObjeto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_TRANSACAO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.DESC_OBJETO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO_AUTORIZACAO.DESC_OBJETO")]
	    public System.String DescObjeto
	    {
	    	    get
	    	    {
	    	          return _DescObjeto;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescObjeto != value)
	    	          {
	    	              this.ValidateProperty("DescObjeto", value);
	    	              this.OnDescObjetoChanging(value);
	    	              this.RaiseDataMemberChanging("DescObjeto");
	    	              this._DescObjeto = value;
	    	              this.RaiseDataMemberChanged("DescObjeto");
	    	              this.OnDescObjetoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoObjeto
	    partial void OnLxTipoObjetoChanging(Byte value);
	    partial void OnLxTipoObjetoChanged();

	    private Byte _LxTipoObjeto;

	    [DataMember(IsRequired = true, Name = "LxTipoObjeto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo Objeto", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoObjeto];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_TRANSACAO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.LX_TIPO_OBJETO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO_AUTORIZACAO.LX_TIPO_OBJETO")]
	    public Byte LxTipoObjeto
	    {
	    	    get
	    	    {
	    	          return _LxTipoObjeto;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoObjeto != value)
	    	          {
	    	              this.ValidateProperty("LxTipoObjeto", value);
	    	              this.OnLxTipoObjetoChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoObjeto");
	    	              this._LxTipoObjeto = value;
	    	              this.RaiseDataMemberChanged("LxTipoObjeto");
	    	              this.OnLxTipoObjetoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ObjetoLinx
	    partial void OnObjetoLinxChanging(bool value);
	    partial void OnObjetoLinxChanged();

	    private bool _ObjetoLinx;

	    [DataMember(IsRequired = true, Name = "ObjetoLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[true];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="true")]
	    public bool ObjetoLinx
	    {
	    	    get
	    	    {
	    	          return _ObjetoLinx;
	    	    }
	    	    set
	    	    {
	    	          if (this._ObjetoLinx != value)
	    	          {
	    	              this.ValidateProperty("ObjetoLinx", value);
	    	              this.OnObjetoLinxChanging(value);
	    	              this.RaiseDataMemberChanging("ObjetoLinx");
	    	              this._ObjetoLinx = value;
	    	              this.RaiseDataMemberChanged("ObjetoLinx");
	    	              this.OnObjetoLinxChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For PathObjeto
	    partial void OnPathObjetoChanging(System.String value);
	    partial void OnPathObjetoChanged();

	    private System.String _PathObjeto;

	    [DataMember(Name = "PathObjeto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Path Objeto", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(2000)]
	    [FunctionalPoint("Precision[2000:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_TRANSACAO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.PATH_OBJETO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO_AUTORIZACAO.PATH_OBJETO")]
	    public System.String PathObjeto
	    {
	    	    get
	    	    {
	    	          return _PathObjeto;
	    	    }
	    	    set
	    	    {
	    	          if (this._PathObjeto != value)
	    	          {
	    	              this.ValidateProperty("PathObjeto", value);
	    	              this.OnPathObjetoChanging(value);
	    	              this.RaiseDataMemberChanging("PathObjeto");
	    	              this._PathObjeto = value;
	    	              this.RaiseDataMemberChanged("PathObjeto");
	    	              this.OnPathObjetoChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_TRANSACAO_AUTORIZACAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_TRANSACAO_AUTORIZACAO), QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_AUTORIZACAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_AUTORIZACAO.INATIVO", Source = "Inativo", Target = "INATIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_AUTORIZACAO.CLASSE_NOME", Source = "ClasseNome", Target = "CLASSE_NOME", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_AUTORIZACAO.ID_TRANSACAO", Source = "IdTransacao", Target = "ID_TRANSACAO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_AUTORIZACAO.COD_TRANSACAO", Source = "CodTransacao", Target = "COD_TRANSACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_AUTORIZACAO.DESC_TRANSACAO", Source = "DescTransacao", Target = "DESC_TRANSACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_AUTORIZACAO.LX_TIPO_TRANSACAO", Source = "LxTipoTransacao", Target = "LX_TIPO_TRANSACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.ID_OBJETO", Source = "IdObjeto", Target = "ID_OBJETO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_OBJETO_AUTORIZACAO", RelationPropertyName = "TCS_OBJETO_AUTORIZACAO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxTipoTransacaoValues()
	    {
	    	    return Linx.Framework.BV.Domains.TipoTransacao.GetValues();
	    }
	    private string _lxTipoTransacaoName;
	    [DataMember(IsRequired = false, Name = "LxTipoTransacaoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo Transação", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxTipoTransacaoName
	    {
	    	    get { if (this.LxTipoTransacao.IsNull()) { _lxTipoTransacaoName = String.Empty; } else { string key = this.LxTipoTransacao.ToString(); var dmValues = this.GetLxTipoTransacaoValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxTipoTransacaoName) _lxTipoTransacaoName = domainName; } return _lxTipoTransacaoName; } set { _lxTipoTransacaoName = value;  }
	    }
	    public Dictionary<string, string> GetLxTipoObjetoValues()
	    {
	    	    return Linx.Framework.BV.Domains.TipoObjeto.GetValues();
	    }
	    private string _lxTipoObjetoName;
	    [DataMember(IsRequired = false, Name = "LxTipoObjetoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo Objeto", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxTipoObjetoName
	    {
	    	    get { if (this.LxTipoObjeto.IsNull()) { _lxTipoObjetoName = String.Empty; } else { string key = this.LxTipoObjeto.ToString(); var dmValues = this.GetLxTipoObjetoValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxTipoObjetoName) _lxTipoObjetoName = domainName; } return _lxTipoObjetoName; } set { _lxTipoObjetoName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[TcsObjetoConteudoAutorizacao];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdObjetoConteudo];ReadOnly[false];Entities[TCS_OBJETO_CONTEUDO_AUTORIZACAO:IdObjetoConteudo];SubQueryInfo[Select 1 From #ParentAlias#.TCS_OBJETO_CONTEUDO_AUTORIZACAO_LISTA as #Alias#];EdmEntityName[TCS_OBJETO_CONTEUDO_AUTORIZACAO];EntityRelations[TCS_OBJETO_AUTORIZACAO(TCS_OBJETO_AUTORIZACAO)#TCS_LAYOUT_AUTORIZACAO_LISTA(TCS_LAYOUT_AUTORIZACAO)];EdmParentEntityName[TCS_OBJETO_AUTORIZACAO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsObjetoConteudoAutorizacao")]
	[Serializable()]
	public partial class TcsObjetoConteudoAutorizacaoParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For ConteudoXml
	    partial void OnConteudoXmlChanging(System.String value);
	    partial void OnConteudoXmlChanged();

	    private System.String _ConteudoXml;

	    [DataMember(IsRequired = true, Name = "ConteudoXml", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Conteudo Xml", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_OBJETO_CONTEUDO_AUTORIZACAO.CONTEUDO_XML];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO_CONTEUDO_AUTORIZACAO.CONTEUDO_XML")]
	    public System.String ConteudoXml
	    {
	    	    get
	    	    {
	    	          return _ConteudoXml;
	    	    }
	    	    set
	    	    {
	    	          if (this._ConteudoXml != value)
	    	          {
	    	              this.ValidateProperty("ConteudoXml", value);
	    	              this.OnConteudoXmlChanging(value);
	    	              this.RaiseDataMemberChanging("ConteudoXml");
	    	              this._ConteudoXml = value;
	    	              this.RaiseDataMemberChanged("ConteudoXml");
	    	              this.OnConteudoXmlChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescLayout
	    partial void OnDescLayoutChanging(System.String value);
	    partial void OnDescLayoutChanged();

	    private System.String _DescLayout;

	    [DataMember(Name = "DescLayout", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsLayoutAutorizacaoLista];LookUpTitle[Seleção de (Descrição)];LookUpQuery[executeLookUpTcsLayoutAutorizacaoLista];LookUpFinalize[finalizeLookUpTcsLayoutAutorizacaoLista];LookUpDisplayColumns[{\"DescLayout\" : \"Desc Layout\", \"Detalhes\" : \"Detalhes\", \"Idioma\" : \"Idioma\", \"Inativo\" : \"Inativo\", \"LayoutPadrao\" : \"Layout Padrao\", \"LxTipoLayout\" : \"Lx Tipo Layout\", \"PossuiFiltro\" : \"Possui Filtro\", \"IdObjetoConteudo\" : \"Id Objeto Conteudo\", \"UltAtualizacao\" : \"Ult Atualizacao\", \"IdLayout\" : \"Id Objeto Conteudo\"}];LookUpColumns[{\"DescLayout\" : true, \"Detalhes\" : true, \"Idioma\" : true, \"Inativo\" : true, \"LayoutPadrao\" : true, \"LxTipoLayout\" : true, \"PossuiFiltro\" : true, \"IdObjetoConteudo\" : true, \"UltAtualizacao\" : true, \"IdLayout\" : true}];FilterDataKey[TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_LAYOUT_AUTORIZACAO_LISTA.DESC_LAYOUT];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescLayout#false##60:0##Desc Layout#0#true##::LookUpTcsLayoutAutorizacaoLista##false#false#TCS_LAYOUT_AUTORIZACAO_LISTA#TCS_LAYOUT_AUTORIZACAO#Linx.Framework.BV.ObjetoAutorizacao#IQueryable###true#false", EdmKey="TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_LAYOUT_AUTORIZACAO_LISTA.DESC_LAYOUT")]
	    public System.String DescLayout
	    {
	    	    get
	    	    {
	    	          return _DescLayout;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescLayout != value)
	    	          {
	    	              this.ValidateProperty("DescLayout", value);
	    	              this.OnDescLayoutChanging(value);
	    	              this.RaiseDataMemberChanging("DescLayout");
	    	              this._DescLayout = value;
	    	              this.RaiseDataMemberChanged("DescLayout");
	    	              this.OnDescLayoutChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Detalhes
	    partial void OnDetalhesChanging(System.String value);
	    partial void OnDetalhesChanged();

	    private System.String _Detalhes;

	    [DataMember(Name = "Detalhes", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Detalhes", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(500)]
	    [FunctionalPoint("Precision[500:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsLayoutAutorizacaoLista];LookUpTitle[Seleção de (Detalhes)];LookUpQuery[executeLookUpTcsLayoutAutorizacaoLista];LookUpFinalize[finalizeLookUpTcsLayoutAutorizacaoLista];LookUpDisplayColumns[{\"DescLayout\" : \"Desc Layout\", \"Detalhes\" : \"Detalhes\", \"Idioma\" : \"Idioma\", \"Inativo\" : \"Inativo\", \"LayoutPadrao\" : \"Layout Padrao\", \"LxTipoLayout\" : \"Lx Tipo Layout\", \"PossuiFiltro\" : \"Possui Filtro\", \"IdObjetoConteudo\" : \"Id Objeto Conteudo\", \"UltAtualizacao\" : \"Ult Atualizacao\", \"IdLayout\" : \"Id Objeto Conteudo\"}];LookUpColumns[{\"DescLayout\" : true, \"Detalhes\" : true, \"Idioma\" : true, \"Inativo\" : true, \"LayoutPadrao\" : true, \"LxTipoLayout\" : true, \"PossuiFiltro\" : true, \"IdObjetoConteudo\" : true, \"UltAtualizacao\" : true, \"IdLayout\" : true}];FilterDataKey[TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_LAYOUT_AUTORIZACAO_LISTA.DETALHES];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#Detalhes#false##500:0##Detalhes#1#true##::LookUpTcsLayoutAutorizacaoLista##false#false#TCS_LAYOUT_AUTORIZACAO_LISTA#TCS_LAYOUT_AUTORIZACAO#Linx.Framework.BV.ObjetoAutorizacao#IQueryable###true#false", EdmKey="TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_LAYOUT_AUTORIZACAO_LISTA.DETALHES")]
	    public System.String Detalhes
	    {
	    	    get
	    	    {
	    	          return _Detalhes;
	    	    }
	    	    set
	    	    {
	    	          if (this._Detalhes != value)
	    	          {
	    	              this.ValidateProperty("Detalhes", value);
	    	              this.OnDetalhesChanging(value);
	    	              this.RaiseDataMemberChanging("Detalhes");
	    	              this._Detalhes = value;
	    	              this.RaiseDataMemberChanged("Detalhes");
	    	              this.OnDetalhesChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Idioma
	    partial void OnIdiomaChanging(System.String value);
	    partial void OnIdiomaChanged();

	    private System.String _Idioma;

	    [DataMember(Name = "Idioma", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Idioma", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(18)]
	    [FunctionalPoint("Precision[18:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsLayoutAutorizacaoLista];LookUpTitle[Seleção de (Idioma)];LookUpQuery[executeLookUpTcsLayoutAutorizacaoLista];LookUpFinalize[finalizeLookUpTcsLayoutAutorizacaoLista];LookUpDisplayColumns[{\"DescLayout\" : \"Desc Layout\", \"Detalhes\" : \"Detalhes\", \"Idioma\" : \"Idioma\", \"Inativo\" : \"Inativo\", \"LayoutPadrao\" : \"Layout Padrao\", \"LxTipoLayout\" : \"Lx Tipo Layout\", \"PossuiFiltro\" : \"Possui Filtro\", \"IdObjetoConteudo\" : \"Id Objeto Conteudo\", \"UltAtualizacao\" : \"Ult Atualizacao\", \"IdLayout\" : \"Id Objeto Conteudo\"}];LookUpColumns[{\"DescLayout\" : true, \"Detalhes\" : true, \"Idioma\" : true, \"Inativo\" : true, \"LayoutPadrao\" : true, \"LxTipoLayout\" : true, \"PossuiFiltro\" : true, \"IdObjetoConteudo\" : true, \"UltAtualizacao\" : true, \"IdLayout\" : true}];FilterDataKey[TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_LAYOUT_AUTORIZACAO_LISTA.IDIOMA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#Idioma#false##18:0##Idioma#2#true##::LookUpTcsLayoutAutorizacaoLista##false#false#TCS_LAYOUT_AUTORIZACAO_LISTA#TCS_LAYOUT_AUTORIZACAO#Linx.Framework.BV.ObjetoAutorizacao#IQueryable###true#false", EdmKey="TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_LAYOUT_AUTORIZACAO_LISTA.IDIOMA")]
	    public System.String Idioma
	    {
	    	    get
	    	    {
	    	          return _Idioma;
	    	    }
	    	    set
	    	    {
	    	          if (this._Idioma != value)
	    	          {
	    	              this.ValidateProperty("Idioma", value);
	    	              this.OnIdiomaChanging(value);
	    	              this.RaiseDataMemberChanging("Idioma");
	    	              this._Idioma = value;
	    	              this.RaiseDataMemberChanged("Idioma");
	    	              this.OnIdiomaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLayout
	    partial void OnIdLayoutChanging(System.Nullable<Int64> value);
	    partial void OnIdLayoutChanged();

	    private System.Nullable<Int64> _IdLayout;

	    [DataMember(Name = "IdLayout", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Objeto Conteudo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsLayoutAutorizacaoLista];LookUpTitle[Seleção de (Id Objeto Conteudo)];LookUpQuery[executeLookUpTcsLayoutAutorizacaoLista];LookUpFinalize[finalizeLookUpTcsLayoutAutorizacaoLista];LookUpDisplayColumns[{\"DescLayout\" : \"Desc Layout\", \"Detalhes\" : \"Detalhes\", \"Idioma\" : \"Idioma\", \"Inativo\" : \"Inativo\", \"LayoutPadrao\" : \"Layout Padrao\", \"LxTipoLayout\" : \"Lx Tipo Layout\", \"PossuiFiltro\" : \"Possui Filtro\", \"IdObjetoConteudo\" : \"Id Objeto Conteudo\", \"UltAtualizacao\" : \"Ult Atualizacao\", \"IdLayout\" : \"Id Objeto Conteudo\"}];LookUpColumns[{\"DescLayout\" : true, \"Detalhes\" : true, \"Idioma\" : true, \"Inativo\" : true, \"LayoutPadrao\" : true, \"LxTipoLayout\" : true, \"PossuiFiltro\" : true, \"IdObjetoConteudo\" : true, \"UltAtualizacao\" : true, \"IdLayout\" : true}];FilterDataKey[TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_LAYOUT_AUTORIZACAO_LISTA.ID_OBJETO_CONTEUDO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<Int64>#IdLayout#true##24:0##Id Objeto Conteudo#9#true##::LookUpTcsLayoutAutorizacaoLista##false#false#TCS_LAYOUT_AUTORIZACAO_LISTA#TCS_LAYOUT_AUTORIZACAO#Linx.Framework.BV.ObjetoAutorizacao#IQueryable###true#false", EdmKey="TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_LAYOUT_AUTORIZACAO_LISTA.ID_OBJETO_CONTEUDO")]
	    public System.Nullable<Int64> IdLayout
	    {
	    	    get
	    	    {
	    	          return _IdLayout;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLayout != value)
	    	          {
	    	              this.ValidateProperty("IdLayout", value);
	    	              this.OnIdLayoutChanging(value);
	    	              this.RaiseDataMemberChanging("IdLayout");
	    	              this._IdLayout = value;
	    	              this.RaiseDataMemberChanged("IdLayout");
	    	              this.OnIdLayoutChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdObjeto
	    partial void OnIdObjetoChanging(Int64 value);
	    partial void OnIdObjetoChanged();

	    private Int64 _IdObjeto;

	    [DataMember(IsRequired = true, Name = "IdObjeto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Objeto", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.ID_OBJETO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.ID_OBJETO")]
	    public Int64 IdObjeto
	    {
	    	    get
	    	    {
	    	          return _IdObjeto;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdObjeto != value)
	    	          {
	    	              this.ValidateProperty("IdObjeto", value);
	    	              this.OnIdObjetoChanging(value);
	    	              this.RaiseDataMemberChanging("IdObjeto");
	    	              this._IdObjeto = value;
	    	              this.RaiseDataMemberChanged("IdObjeto");
	    	              this.OnIdObjetoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdObjetoConteudo
	    partial void OnIdObjetoConteudoChanging(Int64 value);
	    partial void OnIdObjetoConteudoChanged();

	    private Int64 _IdObjetoConteudo;

	    [DataMember(IsRequired = true, Name = "IdObjetoConteudo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Objeto Conteudo1", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsLayoutAutorizacaoLista];LookUpTitle[Seleção de (Id Objeto Conteudo1)];LookUpQuery[executeLookUpTcsLayoutAutorizacaoLista];LookUpFinalize[finalizeLookUpTcsLayoutAutorizacaoLista];LookUpDisplayColumns[{\"DescLayout\" : \"Desc Layout\", \"Detalhes\" : \"Detalhes\", \"Idioma\" : \"Idioma\", \"Inativo\" : \"Inativo\", \"LayoutPadrao\" : \"Layout Padrao\", \"LxTipoLayout\" : \"Lx Tipo Layout\", \"PossuiFiltro\" : \"Possui Filtro\", \"IdObjetoConteudo\" : \"Id Objeto Conteudo\", \"UltAtualizacao\" : \"Ult Atualizacao\", \"IdLayout\" : \"Id Objeto Conteudo\"}];LookUpColumns[{\"DescLayout\" : true, \"Detalhes\" : true, \"Idioma\" : true, \"Inativo\" : true, \"LayoutPadrao\" : true, \"LxTipoLayout\" : true, \"PossuiFiltro\" : true, \"IdObjetoConteudo\" : true, \"UltAtualizacao\" : true, \"IdLayout\" : true}];FilterDataKey[TCS_OBJETO_CONTEUDO_AUTORIZACAO.ID_OBJETO_CONTEUDO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<Int64>#IdObjetoConteudo#true##24:0##Id Objeto Conteudo#7#true##::LookUpTcsLayoutAutorizacaoLista##false#false#TCS_LAYOUT_AUTORIZACAO_LISTA#TCS_LAYOUT_AUTORIZACAO#Linx.Framework.BV.ObjetoAutorizacao#IQueryable###true#false", EdmKey="TCS_OBJETO_CONTEUDO_AUTORIZACAO.ID_OBJETO_CONTEUDO")]
	    public Int64 IdObjetoConteudo
	    {
	    	    get
	    	    {
	    	          return _IdObjetoConteudo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdObjetoConteudo != value)
	    	          {
	    	              this.ValidateProperty("IdObjetoConteudo", value);
	    	              this.OnIdObjetoConteudoChanging(value);
	    	              this.RaiseDataMemberChanging("IdObjetoConteudo");
	    	              this._IdObjetoConteudo = value;
	    	              this.RaiseDataMemberChanged("IdObjetoConteudo");
	    	              this.OnIdObjetoConteudoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Inativo
	    partial void OnInativoChanging(System.Nullable<Boolean> value);
	    partial void OnInativoChanged();

	    private System.Nullable<Boolean> _Inativo;

	    [DataMember(Name = "Inativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inativo", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsLayoutAutorizacaoLista];LookUpTitle[Seleção de (Inativo)];LookUpQuery[executeLookUpTcsLayoutAutorizacaoLista];LookUpFinalize[finalizeLookUpTcsLayoutAutorizacaoLista];LookUpDisplayColumns[{\"DescLayout\" : \"Desc Layout\", \"Detalhes\" : \"Detalhes\", \"Idioma\" : \"Idioma\", \"Inativo\" : \"Inativo\", \"LayoutPadrao\" : \"Layout Padrao\", \"LxTipoLayout\" : \"Lx Tipo Layout\", \"PossuiFiltro\" : \"Possui Filtro\", \"IdObjetoConteudo\" : \"Id Objeto Conteudo\", \"UltAtualizacao\" : \"Ult Atualizacao\", \"IdLayout\" : \"Id Objeto Conteudo\"}];LookUpColumns[{\"DescLayout\" : true, \"Detalhes\" : true, \"Idioma\" : true, \"Inativo\" : true, \"LayoutPadrao\" : true, \"LxTipoLayout\" : true, \"PossuiFiltro\" : true, \"IdObjetoConteudo\" : true, \"UltAtualizacao\" : true, \"IdLayout\" : true}];FilterDataKey[TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_LAYOUT_AUTORIZACAO_LISTA.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<Boolean>#Inativo#false##0:0##Inativo#3#true##::LookUpTcsLayoutAutorizacaoLista##false#false#TCS_LAYOUT_AUTORIZACAO_LISTA#TCS_LAYOUT_AUTORIZACAO#Linx.Framework.BV.ObjetoAutorizacao#IQueryable###true#false", EdmKey="TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_LAYOUT_AUTORIZACAO_LISTA.INATIVO")]
	    public System.Nullable<Boolean> Inativo
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
	    //Extensibility Partial Method Definitions For LayoutLinx
	    partial void OnLayoutLinxChanging(bool value);
	    partial void OnLayoutLinxChanged();

	    private bool _LayoutLinx;

	    [DataMember(IsRequired = true, Name = "LayoutLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[true];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="true")]
	    public bool LayoutLinx
	    {
	    	    get
	    	    {
	    	          return _LayoutLinx;
	    	    }
	    	    set
	    	    {
	    	          if (this._LayoutLinx != value)
	    	          {
	    	              this.ValidateProperty("LayoutLinx", value);
	    	              this.OnLayoutLinxChanging(value);
	    	              this.RaiseDataMemberChanging("LayoutLinx");
	    	              this._LayoutLinx = value;
	    	              this.RaiseDataMemberChanged("LayoutLinx");
	    	              this.OnLayoutLinxChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LayoutPadrao
	    partial void OnLayoutPadraoChanging(System.Nullable<Boolean> value);
	    partial void OnLayoutPadraoChanged();

	    private System.Nullable<Boolean> _LayoutPadrao;

	    [DataMember(Name = "LayoutPadrao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Padrão", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsLayoutAutorizacaoLista];LookUpTitle[Seleção de (Padrão)];LookUpQuery[executeLookUpTcsLayoutAutorizacaoLista];LookUpFinalize[finalizeLookUpTcsLayoutAutorizacaoLista];LookUpDisplayColumns[{\"DescLayout\" : \"Desc Layout\", \"Detalhes\" : \"Detalhes\", \"Idioma\" : \"Idioma\", \"Inativo\" : \"Inativo\", \"LayoutPadrao\" : \"Layout Padrao\", \"LxTipoLayout\" : \"Lx Tipo Layout\", \"PossuiFiltro\" : \"Possui Filtro\", \"IdObjetoConteudo\" : \"Id Objeto Conteudo\", \"UltAtualizacao\" : \"Ult Atualizacao\", \"IdLayout\" : \"Id Objeto Conteudo\"}];LookUpColumns[{\"DescLayout\" : true, \"Detalhes\" : true, \"Idioma\" : true, \"Inativo\" : true, \"LayoutPadrao\" : true, \"LxTipoLayout\" : true, \"PossuiFiltro\" : true, \"IdObjetoConteudo\" : true, \"UltAtualizacao\" : true, \"IdLayout\" : true}];FilterDataKey[TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_LAYOUT_AUTORIZACAO_LISTA.LAYOUT_PADRAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<Boolean>#LayoutPadrao#false##0:0##Layout Padrao#4#true##::LookUpTcsLayoutAutorizacaoLista##false#false#TCS_LAYOUT_AUTORIZACAO_LISTA#TCS_LAYOUT_AUTORIZACAO#Linx.Framework.BV.ObjetoAutorizacao#IQueryable###true#false", EdmKey="TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_LAYOUT_AUTORIZACAO_LISTA.LAYOUT_PADRAO")]
	    public System.Nullable<Boolean> LayoutPadrao
	    {
	    	    get
	    	    {
	    	          return _LayoutPadrao;
	    	    }
	    	    set
	    	    {
	    	          if (this._LayoutPadrao != value)
	    	          {
	    	              this.ValidateProperty("LayoutPadrao", value);
	    	              this.OnLayoutPadraoChanging(value);
	    	              this.RaiseDataMemberChanging("LayoutPadrao");
	    	              this._LayoutPadrao = value;
	    	              this.RaiseDataMemberChanged("LayoutPadrao");
	    	              this.OnLayoutPadraoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxConteudoObjeto
	    partial void OnLxConteudoObjetoChanging(System.String value);
	    partial void OnLxConteudoObjetoChanged();

	    private System.String _LxConteudoObjeto;

	    [DataMember(Name = "LxConteudoObjeto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo Conteúdo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[TipoConteudoObjeto];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_OBJETO_CONTEUDO_AUTORIZACAO.LX_CONTEUDO_OBJETO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO_CONTEUDO_AUTORIZACAO.LX_CONTEUDO_OBJETO")]
	    public System.String LxConteudoObjeto
	    {
	    	    get
	    	    {
	    	          return _LxConteudoObjeto;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxConteudoObjeto != value)
	    	          {
	    	              this.ValidateProperty("LxConteudoObjeto", value);
	    	              this.OnLxConteudoObjetoChanging(value);
	    	              this.RaiseDataMemberChanging("LxConteudoObjeto");
	    	              this._LxConteudoObjeto = value;
	    	              this.RaiseDataMemberChanged("LxConteudoObjeto");
	    	              this.OnLxConteudoObjetoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoLayout
	    partial void OnLxTipoLayoutChanging(System.Nullable<Byte> value);
	    partial void OnLxTipoLayoutChanged();

	    private System.Nullable<Byte> _LxTipoLayout;

	    [DataMember(Name = "LxTipoLayout", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[TipoLayout];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsLayoutAutorizacaoLista];LookUpTitle[Seleção de (Tipo)];LookUpQuery[executeLookUpTcsLayoutAutorizacaoLista];LookUpFinalize[finalizeLookUpTcsLayoutAutorizacaoLista];LookUpDisplayColumns[{\"DescLayout\" : \"Desc Layout\", \"Detalhes\" : \"Detalhes\", \"Idioma\" : \"Idioma\", \"Inativo\" : \"Inativo\", \"LayoutPadrao\" : \"Layout Padrao\", \"LxTipoLayout\" : \"Lx Tipo Layout\", \"PossuiFiltro\" : \"Possui Filtro\", \"IdObjetoConteudo\" : \"Id Objeto Conteudo\", \"UltAtualizacao\" : \"Ult Atualizacao\", \"IdLayout\" : \"Id Objeto Conteudo\"}];LookUpColumns[{\"DescLayout\" : true, \"Detalhes\" : true, \"Idioma\" : true, \"Inativo\" : true, \"LayoutPadrao\" : true, \"LxTipoLayout\" : true, \"PossuiFiltro\" : true, \"IdObjetoConteudo\" : true, \"UltAtualizacao\" : true, \"IdLayout\" : true}];FilterDataKey[TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_LAYOUT_AUTORIZACAO_LISTA.LX_TIPO_LAYOUT];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<Byte>#LxTipoLayout#false##3:0##Lx Tipo Layout#5#true##::LookUpTcsLayoutAutorizacaoLista##false#false#TCS_LAYOUT_AUTORIZACAO_LISTA#TCS_LAYOUT_AUTORIZACAO#Linx.Framework.BV.ObjetoAutorizacao#IQueryable###true#false", EdmKey="TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_LAYOUT_AUTORIZACAO_LISTA.LX_TIPO_LAYOUT")]
	    public System.Nullable<Byte> LxTipoLayout
	    {
	    	    get
	    	    {
	    	          return _LxTipoLayout;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoLayout != value)
	    	          {
	    	              this.ValidateProperty("LxTipoLayout", value);
	    	              this.OnLxTipoLayoutChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoLayout");
	    	              this._LxTipoLayout = value;
	    	              this.RaiseDataMemberChanged("LxTipoLayout");
	    	              this.OnLxTipoLayoutChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For PossuiFiltro
	    partial void OnPossuiFiltroChanging(System.Nullable<Boolean> value);
	    partial void OnPossuiFiltroChanged();

	    private System.Nullable<Boolean> _PossuiFiltro;

	    [DataMember(Name = "PossuiFiltro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Possui Filtro", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsLayoutAutorizacaoLista];LookUpTitle[Seleção de (Possui Filtro)];LookUpQuery[executeLookUpTcsLayoutAutorizacaoLista];LookUpFinalize[finalizeLookUpTcsLayoutAutorizacaoLista];LookUpDisplayColumns[{\"DescLayout\" : \"Desc Layout\", \"Detalhes\" : \"Detalhes\", \"Idioma\" : \"Idioma\", \"Inativo\" : \"Inativo\", \"LayoutPadrao\" : \"Layout Padrao\", \"LxTipoLayout\" : \"Lx Tipo Layout\", \"PossuiFiltro\" : \"Possui Filtro\", \"IdObjetoConteudo\" : \"Id Objeto Conteudo\", \"UltAtualizacao\" : \"Ult Atualizacao\", \"IdLayout\" : \"Id Objeto Conteudo\"}];LookUpColumns[{\"DescLayout\" : true, \"Detalhes\" : true, \"Idioma\" : true, \"Inativo\" : true, \"LayoutPadrao\" : true, \"LxTipoLayout\" : true, \"PossuiFiltro\" : true, \"IdObjetoConteudo\" : true, \"UltAtualizacao\" : true, \"IdLayout\" : true}];FilterDataKey[TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_LAYOUT_AUTORIZACAO_LISTA.POSSUI_FILTRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<Boolean>#PossuiFiltro#false##0:0##Possui Filtro#6#true##::LookUpTcsLayoutAutorizacaoLista##false#false#TCS_LAYOUT_AUTORIZACAO_LISTA#TCS_LAYOUT_AUTORIZACAO#Linx.Framework.BV.ObjetoAutorizacao#IQueryable###true#false", EdmKey="TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_LAYOUT_AUTORIZACAO_LISTA.POSSUI_FILTRO")]
	    public System.Nullable<Boolean> PossuiFiltro
	    {
	    	    get
	    	    {
	    	          return _PossuiFiltro;
	    	    }
	    	    set
	    	    {
	    	          if (this._PossuiFiltro != value)
	    	          {
	    	              this.ValidateProperty("PossuiFiltro", value);
	    	              this.OnPossuiFiltroChanging(value);
	    	              this.RaiseDataMemberChanging("PossuiFiltro");
	    	              this._PossuiFiltro = value;
	    	              this.RaiseDataMemberChanged("PossuiFiltro");
	    	              this.OnPossuiFiltroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Publico
	    partial void OnPublicoChanging(System.Nullable<Boolean> value);
	    partial void OnPublicoChanged();

	    private System.Nullable<Boolean> _Publico;

	    [DataMember(IsRequired = true, Name = "Publico", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[true];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="true")]
	    public System.Nullable<Boolean> Publico
	    {
	    	    get
	    	    {
	    	          return _Publico;
	    	    }
	    	    set
	    	    {
	    	          if (this._Publico != value)
	    	          {
	    	              this.ValidateProperty("Publico", value);
	    	              this.OnPublicoChanging(value);
	    	              this.RaiseDataMemberChanging("Publico");
	    	              this._Publico = value;
	    	              this.RaiseDataMemberChanged("Publico");
	    	              this.OnPublicoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UltAtualizacao
	    partial void OnUltAtualizacaoChanging(System.Nullable<System.DateTime> value);
	    partial void OnUltAtualizacaoChanged();

	    private System.Nullable<System.DateTime> _UltAtualizacao;

	    [DataMember(Name = "UltAtualizacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Última Atualização", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsLayoutAutorizacaoLista];LookUpTitle[Seleção de (Última Atualização)];LookUpQuery[executeLookUpTcsLayoutAutorizacaoLista];LookUpFinalize[finalizeLookUpTcsLayoutAutorizacaoLista];LookUpDisplayColumns[{\"DescLayout\" : \"Desc Layout\", \"Detalhes\" : \"Detalhes\", \"Idioma\" : \"Idioma\", \"Inativo\" : \"Inativo\", \"LayoutPadrao\" : \"Layout Padrao\", \"LxTipoLayout\" : \"Lx Tipo Layout\", \"PossuiFiltro\" : \"Possui Filtro\", \"IdObjetoConteudo\" : \"Id Objeto Conteudo\", \"UltAtualizacao\" : \"Ult Atualizacao\", \"IdLayout\" : \"Id Objeto Conteudo\"}];LookUpColumns[{\"DescLayout\" : true, \"Detalhes\" : true, \"Idioma\" : true, \"Inativo\" : true, \"LayoutPadrao\" : true, \"LxTipoLayout\" : true, \"PossuiFiltro\" : true, \"IdObjetoConteudo\" : true, \"UltAtualizacao\" : true, \"IdLayout\" : true}];FilterDataKey[TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_LAYOUT_AUTORIZACAO_LISTA.ULT_ATUALIZACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<System.DateTime>#UltAtualizacao#false##10:0##Ult Atualizacao#8#true##::LookUpTcsLayoutAutorizacaoLista##false#false#TCS_LAYOUT_AUTORIZACAO_LISTA#TCS_LAYOUT_AUTORIZACAO#Linx.Framework.BV.ObjetoAutorizacao#IQueryable###true#false", EdmKey="TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_LAYOUT_AUTORIZACAO_LISTA.ULT_ATUALIZACAO")]
	    public System.Nullable<System.DateTime> UltAtualizacao
	    {
	    	    get
	    	    {
	    	          return _UltAtualizacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._UltAtualizacao != value)
	    	          {
	    	              this.ValidateProperty("UltAtualizacao", value);
	    	              this.OnUltAtualizacaoChanging(value);
	    	              this.RaiseDataMemberChanging("UltAtualizacao");
	    	              this._UltAtualizacao = value;
	    	              this.RaiseDataMemberChanged("UltAtualizacao");
	    	              this.OnUltAtualizacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ClasseNome
	    partial void OnClasseNomeChanging(System.String value);
	    partial void OnClasseNomeChanged();

	    private System.String _ClasseNome;

	    [DataMember(IsRequired = true, Name = "ClasseNome", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Classe", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.CLASSE_NOME];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO_AUTORIZACAO.CLASSE_NOME")]
	    public System.String ClasseNome
	    {
	    	    get
	    	    {
	    	          return _ClasseNome;
	    	    }
	    	    set
	    	    {
	    	          if (this._ClasseNome != value)
	    	          {
	    	              this.ValidateProperty("ClasseNome", value);
	    	              this.OnClasseNomeChanging(value);
	    	              this.RaiseDataMemberChanging("ClasseNome");
	    	              this._ClasseNome = value;
	    	              this.RaiseDataMemberChanged("ClasseNome");
	    	              this.OnClasseNomeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescObjeto
	    partial void OnDescObjetoChanging(System.String value);
	    partial void OnDescObjetoChanged();

	    private System.String _DescObjeto;

	    [DataMember(IsRequired = true, Name = "DescObjeto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.DESC_OBJETO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO_AUTORIZACAO.DESC_OBJETO")]
	    public System.String DescObjeto
	    {
	    	    get
	    	    {
	    	          return _DescObjeto;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescObjeto != value)
	    	          {
	    	              this.ValidateProperty("DescObjeto", value);
	    	              this.OnDescObjetoChanging(value);
	    	              this.RaiseDataMemberChanging("DescObjeto");
	    	              this._DescObjeto = value;
	    	              this.RaiseDataMemberChanged("DescObjeto");
	    	              this.OnDescObjetoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoObjeto
	    partial void OnLxTipoObjetoChanging(Byte value);
	    partial void OnLxTipoObjetoChanged();

	    private Byte _LxTipoObjeto;

	    [DataMember(IsRequired = true, Name = "LxTipoObjeto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo Objeto", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoObjeto];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.LX_TIPO_OBJETO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO_AUTORIZACAO.LX_TIPO_OBJETO")]
	    public Byte LxTipoObjeto
	    {
	    	    get
	    	    {
	    	          return _LxTipoObjeto;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoObjeto != value)
	    	          {
	    	              this.ValidateProperty("LxTipoObjeto", value);
	    	              this.OnLxTipoObjetoChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoObjeto");
	    	              this._LxTipoObjeto = value;
	    	              this.RaiseDataMemberChanged("LxTipoObjeto");
	    	              this.OnLxTipoObjetoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ObjetoLinx
	    partial void OnObjetoLinxChanging(bool value);
	    partial void OnObjetoLinxChanged();

	    private bool _ObjetoLinx;

	    [DataMember(IsRequired = true, Name = "ObjetoLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[true];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="true")]
	    public bool ObjetoLinx
	    {
	    	    get
	    	    {
	    	          return _ObjetoLinx;
	    	    }
	    	    set
	    	    {
	    	          if (this._ObjetoLinx != value)
	    	          {
	    	              this.ValidateProperty("ObjetoLinx", value);
	    	              this.OnObjetoLinxChanging(value);
	    	              this.RaiseDataMemberChanging("ObjetoLinx");
	    	              this._ObjetoLinx = value;
	    	              this.RaiseDataMemberChanged("ObjetoLinx");
	    	              this.OnObjetoLinxChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For PathObjeto
	    partial void OnPathObjetoChanging(System.String value);
	    partial void OnPathObjetoChanged();

	    private System.String _PathObjeto;

	    [DataMember(Name = "PathObjeto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Path Objeto", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(2000)]
	    [FunctionalPoint("Precision[2000:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.PATH_OBJETO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO_AUTORIZACAO.PATH_OBJETO")]
	    public System.String PathObjeto
	    {
	    	    get
	    	    {
	    	          return _PathObjeto;
	    	    }
	    	    set
	    	    {
	    	          if (this._PathObjeto != value)
	    	          {
	    	              this.ValidateProperty("PathObjeto", value);
	    	              this.OnPathObjetoChanging(value);
	    	              this.RaiseDataMemberChanging("PathObjeto");
	    	              this._PathObjeto = value;
	    	              this.RaiseDataMemberChanged("PathObjeto");
	    	              this.OnPathObjetoChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_OBJETO_CONTEUDO_AUTORIZACAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_OBJETO_CONTEUDO_AUTORIZACAO), QualifiedEntitySetName = "AutorizacaoContext.TCS_OBJETO_CONTEUDO_AUTORIZACAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO_CONTEUDO_AUTORIZACAO.CONTEUDO_XML", Source = "ConteudoXml", Target = "CONTEUDO_XML", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_OBJETO_CONTEUDO_AUTORIZACAO", RelationPropertyName = "TCS_OBJETO_CONTEUDO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO_CONTEUDO_AUTORIZACAO.ID_OBJETO_CONTEUDO", Source = "IdObjetoConteudo", Target = "ID_OBJETO_CONTEUDO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_OBJETO_CONTEUDO_AUTORIZACAO", RelationPropertyName = "TCS_OBJETO_CONTEUDO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO_CONTEUDO_AUTORIZACAO.LX_CONTEUDO_OBJETO", Source = "LxConteudoObjeto", Target = "LX_CONTEUDO_OBJETO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_OBJETO_CONTEUDO_AUTORIZACAO", RelationPropertyName = "TCS_OBJETO_CONTEUDO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.ID_OBJETO", Source = "IdObjeto", Target = "ID_OBJETO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_OBJETO_AUTORIZACAO", RelationPropertyName = "TCS_OBJETO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO_CONTEUDO_AUTORIZACAO.TCS_LAYOUT_AUTORIZACAO_LISTA.ID_OBJETO_CONTEUDO", Source = "IdLayout", Target = "ID_OBJETO_CONTEUDO", TargetKeyName = "ID_OBJETO_CONTEUDO", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_LAYOUT_AUTORIZACAO", RelationPropertyName = "TCS_LAYOUT_AUTORIZACAO_LISTA" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxConteudoObjetoValues()
	    {
	    	    return Linx.Framework.BV.Domains.TipoConteudoObjeto.GetValues();
	    }
	    private string _lxConteudoObjetoName;
	    [DataMember(IsRequired = false, Name = "LxConteudoObjetoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo Conteúdo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxConteudoObjetoName
	    {
	    	    get { if (this.LxConteudoObjeto.IsNull()) { _lxConteudoObjetoName = String.Empty; } else { string key = this.LxConteudoObjeto.ToString(); var dmValues = this.GetLxConteudoObjetoValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxConteudoObjetoName) _lxConteudoObjetoName = domainName; } return _lxConteudoObjetoName; } set { _lxConteudoObjetoName = value;  }
	    }
	    public Dictionary<string, string> GetLxTipoLayoutValues()
	    {
	    	    return Linx.Framework.BV.Domains.TipoLayout.GetValues();
	    }
	    private string _lxTipoLayoutName;
	    [DataMember(IsRequired = false, Name = "LxTipoLayoutName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxTipoLayoutName
	    {
	    	    get { if (this.LxTipoLayout.IsNull()) { _lxTipoLayoutName = String.Empty; } else { string key = this.LxTipoLayout.ToString(); var dmValues = this.GetLxTipoLayoutValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxTipoLayoutName) _lxTipoLayoutName = domainName; } return _lxTipoLayoutName; } set { _lxTipoLayoutName = value;  }
	    }
	    public Dictionary<string, string> GetLxTipoObjetoValues()
	    {
	    	    return Linx.Framework.BV.Domains.TipoObjeto.GetValues();
	    }
	    private string _lxTipoObjetoName;
	    [DataMember(IsRequired = false, Name = "LxTipoObjetoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo Objeto", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxTipoObjetoName
	    {
	    	    get { if (this.LxTipoObjeto.IsNull()) { _lxTipoObjetoName = String.Empty; } else { string key = this.LxTipoObjeto.ToString(); var dmValues = this.GetLxTipoObjetoValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxTipoObjetoName) _lxTipoObjetoName = domainName; } return _lxTipoObjetoName; } set { _lxTipoObjetoName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	//////////////////////// DomainService Class V1 ///////////////////////
	///////////////////////////////////////////////////////////////////////
	[EnableClientAccess()]	
	[DomainIdentifier("ProcessorOverviewObjetoAutorizacaoDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class ObjetoAutorizacaoDomainService : DomainService, IDataServiceContext 
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

		
	    public ObjetoAutorizacaoDomainService() : this("", null, null) { }
	    public ObjetoAutorizacaoDomainService(string connectionString) : this(connectionString, null, null) { }
	    public ObjetoAutorizacaoDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public ObjetoAutorizacaoDomainService(Linx.Framework.Autorizacao.BM.AutorizacaoContext dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public ObjetoAutorizacaoDomainService(string connectionString, Linx.Framework.Autorizacao.BM.AutorizacaoContext dataContext, Dictionary<string, string> headers) : base() 
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
	
	
	        TcsObjetoAutorizacao.OnSavedContextChanges(this, changeSet.ChangeSetEntries.Where(e => e.Entity is TcsObjetoAutorizacao).ToArray());
    	
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
 	        var _TcsObjetoAutorizacaoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsObjetoAutorizacao && e.Entity.GetType().Name == "TcsObjetoAutorizacao" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _TcsObjetoAutorizacaoElements)
 	           if (((TcsObjetoAutorizacao)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 
 	        //Remove inconsistent details
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsTransacaoAutorizacaoChild && e.Entity.GetType().Name == "TcsTransacaoAutorizacaoChild" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsObjetoConteudoAutorizacao && e.Entity.GetType().Name == "TcsObjetoConteudoAutorizacao" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
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
	    //Get All LookUpTcsLayoutAutorizacaoLista.
	    public IQueryable<LookUpTcsLayoutAutorizacaoLista> GetAllLookUpTcsLayoutAutorizacaoLista()
	    {
	        return this.GetLookUpTcsLayoutAutorizacaoLista(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsLayoutAutorizacaoLista By EntitySearch.
	    public IQueryable<LookUpTcsLayoutAutorizacaoLista> GetLookUpTcsLayoutAutorizacaoListaByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsLayoutAutorizacaoLista(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsLayoutAutorizacaoLista.
	    public IQueryable<LookUpTcsLayoutAutorizacaoLista> GetLookUpTcsLayoutAutorizacaoLista(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TCS_LAYOUT_AUTORIZACAO" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsLayoutAutorizacaoLista";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsLayoutAutorizacaoLista));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsLayoutAutorizacaoLista> query =  
	
	            (from entity in this.DbContext.TCS_LAYOUT_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpTcsLayoutAutorizacaoLista()		
	            {
	            
                DescLayout = entity.DESC_LAYOUT
                , Detalhes = entity.DETALHES
                , Idioma = entity.IDIOMA
                , Inativo = entity.INATIVO
                , LayoutPadrao = entity.LAYOUT_PADRAO
                , LxTipoLayout = entity.LX_TIPO_LAYOUT
                , PossuiFiltro = entity.POSSUI_FILTRO
                , IdObjetoConteudo = entity.ID_OBJETO_CONTEUDO
                , UltAtualizacao = entity.ULT_ATUALIZACAO
                , IdLayout = entity.ID_OBJETO_CONTEUDO
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
	
		

	        if (entityName.InList("Linx.Framework.BV.ObjetoAutorizacao.TcsObjetoAutorizacao"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsObjetoAutorizacao",
	        			NameSpace = "Linx.Framework.BV.ObjetoAutorizacao",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsObjetoAutorizacao",
	        			ClearMethodName = "ClearTcsObjetoAutorizacao",
	        			QueryMethodName  = "GetPagedTcsObjetoAutorizacao",	
	        			CountingMethodName  = "GetTcsObjetoAutorizacao" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.ObjetoAutorizacao.TcsObjetoAutorizacao"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.ObjetoAutorizacao.TcsObjetoAutorizacao"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.ObjetoAutorizacao.TcsObjetoAutorizacao", "Linx.Framework.BV.ObjetoAutorizacao.TcsTransacaoAutorizacaoChild"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsTransacaoAutorizacaoChild" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.ObjetoAutorizacao",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsObjetoAutorizacao",	
	        			DisplayName = "Transação",
	        			ClearMethodName = "ClearTcsTransacaoAutorizacaoChild" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsTransacaoAutorizacaoChild" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsTransacaoAutorizacaoChild" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.ObjetoAutorizacao.TcsTransacaoAutorizacaoChild"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.ObjetoAutorizacao.TcsTransacaoAutorizacaoChild" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.ObjetoAutorizacao.TcsObjetoAutorizacao", "Linx.Framework.BV.ObjetoAutorizacao.TcsObjetoConteudoAutorizacao"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsObjetoConteudoAutorizacao" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.ObjetoAutorizacao",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsObjetoAutorizacao",	
	        			DisplayName = "TcsObjetoConteudoAutorizacao",
	        			ClearMethodName = "ClearTcsObjetoConteudoAutorizacao" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsObjetoConteudoAutorizacao" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsObjetoConteudoAutorizacao" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.ObjetoAutorizacao.TcsObjetoConteudoAutorizacao"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.ObjetoAutorizacao.TcsObjetoConteudoAutorizacao" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
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

         		    return new string[] { "Framework_ObjetoAutorizacaoClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.ObjetoAutorizacaoClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Framework_objetoAutorizacaoService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.objetoAutorizacaoService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear TcsObjetoAutorizacao.
	    public IEnumerable<TcsObjetoAutorizacao> ClearTcsObjetoAutorizacao()
	    {
	        List<TcsObjetoAutorizacao> result = new List<TcsObjetoAutorizacao>();
	        result.Add(new TcsObjetoAutorizacao());	
			
	        result[0].TcsTransacaoAutorizacaoChildList = new List<TcsTransacaoAutorizacaoChild>();
	        ((List<TcsTransacaoAutorizacaoChild>)result[0].TcsTransacaoAutorizacaoChildList).Add(new TcsTransacaoAutorizacaoChild());
			
	        result[0].TcsObjetoConteudoAutorizacaoList = new List<TcsObjetoConteudoAutorizacao>();
	        ((List<TcsObjetoConteudoAutorizacao>)result[0].TcsObjetoConteudoAutorizacaoList).Add(new TcsObjetoConteudoAutorizacao());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsTransacaoAutorizacaoChild.
	    public IEnumerable<TcsTransacaoAutorizacaoChild> ClearTcsTransacaoAutorizacaoChild()
	    {
	        List<TcsTransacaoAutorizacaoChild> result = new List<TcsTransacaoAutorizacaoChild>();
	        result.Add(new TcsTransacaoAutorizacaoChild());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsObjetoConteudoAutorizacao.
	    public IEnumerable<TcsObjetoConteudoAutorizacao> ClearTcsObjetoConteudoAutorizacao()
	    {
	        List<TcsObjetoConteudoAutorizacao> result = new List<TcsObjetoConteudoAutorizacao>();
	        result.Add(new TcsObjetoConteudoAutorizacao());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsObjetoAutorizacao.
	    public IQueryable<TcsObjetoAutorizacao> GetTcsObjetoAutorizacao()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsObjetoAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_OBJETO_AUTORIZACAO
	            
	            	
	            select new TcsObjetoAutorizacao()		
	            {
	            
                ClasseNome = entity0.CLASSE_NOME
                , DescObjeto = entity0.DESC_OBJETO
                , IdObjeto = entity0.ID_OBJETO
                , LxTipoObjeto = entity0.LX_TIPO_OBJETO
                , LxTipoObjetoName = ((entity0.LX_TIPO_OBJETO) == 1 ? "BO" : ((entity0.LX_TIPO_OBJETO) == 3 ? "Campo" : ((entity0.LX_TIPO_OBJETO) == 10 ? "Filtro" : ((entity0.LX_TIPO_OBJETO) == 9 ? "Layout" : ((entity0.LX_TIPO_OBJETO) == 6 ? "Relatório" : ((entity0.LX_TIPO_OBJETO) == 5 ? "Stored Procedure" : ((entity0.LX_TIPO_OBJETO) == 8 ? "Template de ação de Workflow" : ((entity0.LX_TIPO_OBJETO) == 2 ? "Transação" : ((entity0.LX_TIPO_OBJETO) == 4 ? "Trigger" : ((entity0.LX_TIPO_OBJETO) == 11 ? "Extensão (Objeto de entrada)" : ((entity0.LX_TIPO_OBJETO) == 7 ? "Workflow" : "")))))))))))
                , ObjetoLinx = true
                , PathObjeto = entity0.PATH_OBJETO
			
                ,TcsTransacaoAutorizacaoChildList = 
	                        (from entity1 in entity0.TCS_TRANSACAO_AUTORIZACAO_LISTA
                                  let entity1Al1 = entity1.TCS_OBJETO_AUTORIZACAO
	                        
	                        	
	                        select new TcsTransacaoAutorizacaoChild()
	                        {
	                        
                                ClasseNome = entity1.CLASSE_NOME
                                , CodTransacao = entity1.COD_TRANSACAO
                                , DescTransacao = entity1.DESC_TRANSACAO
                                , IdObjeto = entity1Al1.ID_OBJETO
                                , IdTransacao = entity1.ID_TRANSACAO
                                , Inativo = entity1.INATIVO
                                , LxTipoTransacao = entity1.LX_TIPO_TRANSACAO
                                , LxTipoTransacaoName = ((entity1.LX_TIPO_TRANSACAO) == 7 ? "Assistente" : ((entity1.LX_TIPO_TRANSACAO) == 8 ? "Dashboard" : ((entity1.LX_TIPO_TRANSACAO) == 2 ? "ERP" : ((entity1.LX_TIPO_TRANSACAO) == 6 ? "ERP App" : ((entity1.LX_TIPO_TRANSACAO) == 4 ? "Excel" : ((entity1.LX_TIPO_TRANSACAO) == 3 ? "Loja" : ((entity1.LX_TIPO_TRANSACAO) == 5 ? "Mobile" : ((entity1.LX_TIPO_TRANSACAO) == 1 ? "Todos" : ""))))))))
		
	                        }
	                        )
			
                ,TcsObjetoConteudoAutorizacaoList = 
	                        (from entity1 in entity0.TCS_OBJETO_CONTEUDO_AUTORIZACAO_LISTA
                                  let entity1Al2 = entity1.TCS_OBJETO_AUTORIZACAO
                                  let entity1Al1 = entity1.TCS_LAYOUT_AUTORIZACAO_LISTA
	                        
	                        	
	                        select new TcsObjetoConteudoAutorizacao()
	                        {
	                        
                                ConteudoXml = entity1.CONTEUDO_XML
                                , DescLayout = entity1Al1.DESC_LAYOUT
                                , Detalhes = entity1Al1.DETALHES
                                , Idioma = entity1Al1.IDIOMA
                                , IdLayout = entity1Al1.ID_OBJETO_CONTEUDO
                                , IdObjeto = entity1Al2.ID_OBJETO
                                , IdObjetoConteudo = entity1.ID_OBJETO_CONTEUDO
                                , Inativo = entity1Al1.INATIVO
                                , LayoutLinx = true
                                , LayoutPadrao = entity1Al1.LAYOUT_PADRAO
                                , LxConteudoObjeto = entity1.LX_CONTEUDO_OBJETO
                                , LxConteudoObjetoName = ((entity1.LX_CONTEUDO_OBJETO) == "3" ? "Configuração de Exportação para Excel" : ((entity1.LX_CONTEUDO_OBJETO) == "4" ? "Configuração de Exportação para Report" : ((entity1.LX_CONTEUDO_OBJETO) == "6" ? "Gravação de Layout para Grid" : ((entity1.LX_CONTEUDO_OBJETO) == "1" ? "Layout" : ((entity1.LX_CONTEUDO_OBJETO) == "2" ? "Mídia" : ((entity1.LX_CONTEUDO_OBJETO) == "5" ? "Gravação de Layout para Pivot Table" : ""))))))
                                , LxTipoLayout = entity1Al1.LX_TIPO_LAYOUT
                                , LxTipoLayoutName = ((entity1Al1.LX_TIPO_LAYOUT) == 1 ? "Layout do Sistema" : ((entity1Al1.LX_TIPO_LAYOUT) == 2 ? "Layout do Usuário" : ""))
                                , PossuiFiltro = entity1Al1.POSSUI_FILTRO
                                , Publico = true
                                , UltAtualizacao = entity1Al1.ULT_ATUALIZACAO
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsTransacaoAutorizacaoChild.
	    public IQueryable<TcsTransacaoAutorizacaoChild> GetTcsTransacaoAutorizacaoChild()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoAutorizacaoChild> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_AUTORIZACAO
                  let entity0Al1 = entity0.TCS_OBJETO_AUTORIZACAO
	            
	            	
	            select new TcsTransacaoAutorizacaoChild()		
	            {
	            
                ClasseNome = entity0.CLASSE_NOME
                , CodTransacao = entity0.COD_TRANSACAO
                , DescTransacao = entity0.DESC_TRANSACAO
                , IdObjeto = entity0Al1.ID_OBJETO
                , IdTransacao = entity0.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , LxTipoTransacao = entity0.LX_TIPO_TRANSACAO
                , LxTipoTransacaoName = ((entity0.LX_TIPO_TRANSACAO) == 7 ? "Assistente" : ((entity0.LX_TIPO_TRANSACAO) == 8 ? "Dashboard" : ((entity0.LX_TIPO_TRANSACAO) == 2 ? "ERP" : ((entity0.LX_TIPO_TRANSACAO) == 6 ? "ERP App" : ((entity0.LX_TIPO_TRANSACAO) == 4 ? "Excel" : ((entity0.LX_TIPO_TRANSACAO) == 3 ? "Loja" : ((entity0.LX_TIPO_TRANSACAO) == 5 ? "Mobile" : ((entity0.LX_TIPO_TRANSACAO) == 1 ? "Todos" : ""))))))))
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsObjetoConteudoAutorizacao.
	    public IQueryable<TcsObjetoConteudoAutorizacao> GetTcsObjetoConteudoAutorizacao()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsObjetoConteudoAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_OBJETO_CONTEUDO_AUTORIZACAO
                  let entity0Al2 = entity0.TCS_OBJETO_AUTORIZACAO
                  let entity0Al1 = entity0.TCS_LAYOUT_AUTORIZACAO_LISTA
	            
	            	
	            select new TcsObjetoConteudoAutorizacao()		
	            {
	            
                ConteudoXml = entity0.CONTEUDO_XML
                , DescLayout = entity0Al1.DESC_LAYOUT
                , Detalhes = entity0Al1.DETALHES
                , Idioma = entity0Al1.IDIOMA
                , IdLayout = entity0Al1.ID_OBJETO_CONTEUDO
                , IdObjeto = entity0Al2.ID_OBJETO
                , IdObjetoConteudo = entity0.ID_OBJETO_CONTEUDO
                , Inativo = entity0Al1.INATIVO
                , LayoutLinx = true
                , LayoutPadrao = entity0Al1.LAYOUT_PADRAO
                , LxConteudoObjeto = entity0.LX_CONTEUDO_OBJETO
                , LxConteudoObjetoName = ((entity0.LX_CONTEUDO_OBJETO) == "3" ? "Configuração de Exportação para Excel" : ((entity0.LX_CONTEUDO_OBJETO) == "4" ? "Configuração de Exportação para Report" : ((entity0.LX_CONTEUDO_OBJETO) == "6" ? "Gravação de Layout para Grid" : ((entity0.LX_CONTEUDO_OBJETO) == "1" ? "Layout" : ((entity0.LX_CONTEUDO_OBJETO) == "2" ? "Mídia" : ((entity0.LX_CONTEUDO_OBJETO) == "5" ? "Gravação de Layout para Pivot Table" : ""))))))
                , LxTipoLayout = entity0Al1.LX_TIPO_LAYOUT
                , LxTipoLayoutName = ((entity0Al1.LX_TIPO_LAYOUT) == 1 ? "Layout do Sistema" : ((entity0Al1.LX_TIPO_LAYOUT) == 2 ? "Layout do Usuário" : ""))
                , PossuiFiltro = entity0Al1.POSSUI_FILTRO
                , Publico = true
                , UltAtualizacao = entity0Al1.ULT_ATUALIZACAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsObjetoAutorizacaoNoAssociations.
	    public IQueryable<TcsObjetoAutorizacao> GetTcsObjetoAutorizacaoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsObjetoAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_OBJETO_AUTORIZACAO
	            
	            	
	            select new TcsObjetoAutorizacao()		
	            {
	            
                ClasseNome = entity0.CLASSE_NOME
                , DescObjeto = entity0.DESC_OBJETO
                , IdObjeto = entity0.ID_OBJETO
                , LxTipoObjeto = entity0.LX_TIPO_OBJETO
                , LxTipoObjetoName = ((entity0.LX_TIPO_OBJETO) == 1 ? "BO" : ((entity0.LX_TIPO_OBJETO) == 3 ? "Campo" : ((entity0.LX_TIPO_OBJETO) == 10 ? "Filtro" : ((entity0.LX_TIPO_OBJETO) == 9 ? "Layout" : ((entity0.LX_TIPO_OBJETO) == 6 ? "Relatório" : ((entity0.LX_TIPO_OBJETO) == 5 ? "Stored Procedure" : ((entity0.LX_TIPO_OBJETO) == 8 ? "Template de ação de Workflow" : ((entity0.LX_TIPO_OBJETO) == 2 ? "Transação" : ((entity0.LX_TIPO_OBJETO) == 4 ? "Trigger" : ((entity0.LX_TIPO_OBJETO) == 11 ? "Extensão (Objeto de entrada)" : ((entity0.LX_TIPO_OBJETO) == 7 ? "Workflow" : "")))))))))))
                , ObjetoLinx = true
                , PathObjeto = entity0.PATH_OBJETO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsTransacaoAutorizacaoChildNoAssociations.
	    public IQueryable<TcsTransacaoAutorizacaoChild> GetTcsTransacaoAutorizacaoChildNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoAutorizacaoChild> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_AUTORIZACAO
                  let entity0Al1 = entity0.TCS_OBJETO_AUTORIZACAO
	            
	            	
	            select new TcsTransacaoAutorizacaoChild()		
	            {
	            
                ClasseNome = entity0.CLASSE_NOME
                , CodTransacao = entity0.COD_TRANSACAO
                , DescTransacao = entity0.DESC_TRANSACAO
                , IdObjeto = entity0Al1.ID_OBJETO
                , IdTransacao = entity0.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , LxTipoTransacao = entity0.LX_TIPO_TRANSACAO
                , LxTipoTransacaoName = ((entity0.LX_TIPO_TRANSACAO) == 7 ? "Assistente" : ((entity0.LX_TIPO_TRANSACAO) == 8 ? "Dashboard" : ((entity0.LX_TIPO_TRANSACAO) == 2 ? "ERP" : ((entity0.LX_TIPO_TRANSACAO) == 6 ? "ERP App" : ((entity0.LX_TIPO_TRANSACAO) == 4 ? "Excel" : ((entity0.LX_TIPO_TRANSACAO) == 3 ? "Loja" : ((entity0.LX_TIPO_TRANSACAO) == 5 ? "Mobile" : ((entity0.LX_TIPO_TRANSACAO) == 1 ? "Todos" : ""))))))))
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsObjetoConteudoAutorizacaoNoAssociations.
	    public IQueryable<TcsObjetoConteudoAutorizacao> GetTcsObjetoConteudoAutorizacaoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsObjetoConteudoAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_OBJETO_CONTEUDO_AUTORIZACAO
                  let entity0Al2 = entity0.TCS_OBJETO_AUTORIZACAO
                  let entity0Al1 = entity0.TCS_LAYOUT_AUTORIZACAO_LISTA
	            
	            	
	            select new TcsObjetoConteudoAutorizacao()		
	            {
	            
                ConteudoXml = entity0.CONTEUDO_XML
                , DescLayout = entity0Al1.DESC_LAYOUT
                , Detalhes = entity0Al1.DETALHES
                , Idioma = entity0Al1.IDIOMA
                , IdLayout = entity0Al1.ID_OBJETO_CONTEUDO
                , IdObjeto = entity0Al2.ID_OBJETO
                , IdObjetoConteudo = entity0.ID_OBJETO_CONTEUDO
                , Inativo = entity0Al1.INATIVO
                , LayoutLinx = true
                , LayoutPadrao = entity0Al1.LAYOUT_PADRAO
                , LxConteudoObjeto = entity0.LX_CONTEUDO_OBJETO
                , LxConteudoObjetoName = ((entity0.LX_CONTEUDO_OBJETO) == "3" ? "Configuração de Exportação para Excel" : ((entity0.LX_CONTEUDO_OBJETO) == "4" ? "Configuração de Exportação para Report" : ((entity0.LX_CONTEUDO_OBJETO) == "6" ? "Gravação de Layout para Grid" : ((entity0.LX_CONTEUDO_OBJETO) == "1" ? "Layout" : ((entity0.LX_CONTEUDO_OBJETO) == "2" ? "Mídia" : ((entity0.LX_CONTEUDO_OBJETO) == "5" ? "Gravação de Layout para Pivot Table" : ""))))))
                , LxTipoLayout = entity0Al1.LX_TIPO_LAYOUT
                , LxTipoLayoutName = ((entity0Al1.LX_TIPO_LAYOUT) == 1 ? "Layout do Sistema" : ((entity0Al1.LX_TIPO_LAYOUT) == 2 ? "Layout do Usuário" : ""))
                , PossuiFiltro = entity0Al1.POSSUI_FILTRO
                , Publico = true
                , UltAtualizacao = entity0Al1.ULT_ATUALIZACAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	result.Add("TcsObjetoAutorizacao|ObjetoLinx");
	    	result.Add("TcsObjetoAutorizacao|true");
	    	//Add filtering disabled property for TCS_OBJETO_AUTORIZACAO
	    	string[] bmDisabledTcsObjetoAutorizacaoList = this.GetEDM().GetFilteringDisabledList("TCS_OBJETO_AUTORIZACAO");
	    	if (bmDisabledTcsObjetoAutorizacaoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsObjetoAutorizacaoList.Contains("TCS_OBJETO_AUTORIZACAO.CLASSE_NOME"))
	    		{
	    			result.Add("TcsObjetoAutorizacao|ClasseNome");
	    			result.Add("TcsObjetoAutorizacao|TCS_OBJETO_AUTORIZACAO.CLASSE_NOME");
	    		}
	
	    		if (bmDisabledTcsObjetoAutorizacaoList.Contains("TCS_OBJETO_AUTORIZACAO.DESC_OBJETO"))
	    		{
	    			result.Add("TcsObjetoAutorizacao|DescObjeto");
	    			result.Add("TcsObjetoAutorizacao|TCS_OBJETO_AUTORIZACAO.DESC_OBJETO");
	    		}
	
	    		if (bmDisabledTcsObjetoAutorizacaoList.Contains("TCS_OBJETO_AUTORIZACAO.ID_OBJETO"))
	    		{
	    			result.Add("TcsObjetoAutorizacao|IdObjeto");
	    			result.Add("TcsObjetoAutorizacao|TCS_OBJETO_AUTORIZACAO.ID_OBJETO");
	    		}
	
	    		if (bmDisabledTcsObjetoAutorizacaoList.Contains("TCS_OBJETO_AUTORIZACAO.LX_TIPO_OBJETO"))
	    		{
	    			result.Add("TcsObjetoAutorizacao|LxTipoObjeto");
	    			result.Add("TcsObjetoAutorizacao|TCS_OBJETO_AUTORIZACAO.LX_TIPO_OBJETO");
	    		}
	
	    		if (bmDisabledTcsObjetoAutorizacaoList.Contains("TCS_OBJETO_AUTORIZACAO.PATH_OBJETO"))
	    		{
	    			result.Add("TcsObjetoAutorizacao|PathObjeto");
	    			result.Add("TcsObjetoAutorizacao|TCS_OBJETO_AUTORIZACAO.PATH_OBJETO");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_TRANSACAO_AUTORIZACAO
	    	string[] bmDisabledTcsTransacaoAutorizacaoChildList = this.GetEDM().GetFilteringDisabledList("TCS_TRANSACAO_AUTORIZACAO");
	    	if (bmDisabledTcsTransacaoAutorizacaoChildList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsTransacaoAutorizacaoChildList.Contains("TCS_TRANSACAO_AUTORIZACAO.CLASSE_NOME"))
	    		{
	    			result.Add("TcsTransacaoAutorizacaoChild|ClasseNome");
	    			result.Add("TcsTransacaoAutorizacaoChild|TCS_TRANSACAO_AUTORIZACAO.CLASSE_NOME");
	    		}
	
	    		if (bmDisabledTcsTransacaoAutorizacaoChildList.Contains("TCS_TRANSACAO_AUTORIZACAO.COD_TRANSACAO"))
	    		{
	    			result.Add("TcsTransacaoAutorizacaoChild|CodTransacao");
	    			result.Add("TcsTransacaoAutorizacaoChild|TCS_TRANSACAO_AUTORIZACAO.COD_TRANSACAO");
	    		}
	
	    		if (bmDisabledTcsTransacaoAutorizacaoChildList.Contains("TCS_TRANSACAO_AUTORIZACAO.DESC_TRANSACAO"))
	    		{
	    			result.Add("TcsTransacaoAutorizacaoChild|DescTransacao");
	    			result.Add("TcsTransacaoAutorizacaoChild|TCS_TRANSACAO_AUTORIZACAO.DESC_TRANSACAO");
	    		}
	
	    		if (bmDisabledTcsTransacaoAutorizacaoChildList.Contains("TCS_TRANSACAO_AUTORIZACAO.ID_TRANSACAO"))
	    		{
	    			result.Add("TcsTransacaoAutorizacaoChild|IdTransacao");
	    			result.Add("TcsTransacaoAutorizacaoChild|TCS_TRANSACAO_AUTORIZACAO.ID_TRANSACAO");
	    		}
	
	    		if (bmDisabledTcsTransacaoAutorizacaoChildList.Contains("TCS_TRANSACAO_AUTORIZACAO.INATIVO"))
	    		{
	    			result.Add("TcsTransacaoAutorizacaoChild|Inativo");
	    			result.Add("TcsTransacaoAutorizacaoChild|TCS_TRANSACAO_AUTORIZACAO.INATIVO");
	    		}
	
	    		if (bmDisabledTcsTransacaoAutorizacaoChildList.Contains("TCS_TRANSACAO_AUTORIZACAO.LX_TIPO_TRANSACAO"))
	    		{
	    			result.Add("TcsTransacaoAutorizacaoChild|LxTipoTransacao");
	    			result.Add("TcsTransacaoAutorizacaoChild|TCS_TRANSACAO_AUTORIZACAO.LX_TIPO_TRANSACAO");
	    		}
	    	}
	    	result.Add("TcsObjetoConteudoAutorizacao|LayoutLinx");
	    	result.Add("TcsObjetoConteudoAutorizacao|true");
	    	result.Add("TcsObjetoConteudoAutorizacao|Publico");
	    	result.Add("TcsObjetoConteudoAutorizacao|true");
	    	//Add filtering disabled property for TCS_OBJETO_CONTEUDO_AUTORIZACAO
	    	string[] bmDisabledTcsObjetoConteudoAutorizacaoList = this.GetEDM().GetFilteringDisabledList("TCS_OBJETO_CONTEUDO_AUTORIZACAO");
	    	if (bmDisabledTcsObjetoConteudoAutorizacaoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsObjetoConteudoAutorizacaoList.Contains("TCS_OBJETO_CONTEUDO_AUTORIZACAO.CONTEUDO_XML"))
	    		{
	    			result.Add("TcsObjetoConteudoAutorizacao|ConteudoXml");
	    			result.Add("TcsObjetoConteudoAutorizacao|TCS_OBJETO_CONTEUDO_AUTORIZACAO.CONTEUDO_XML");
	    		}
	
	    		if (bmDisabledTcsObjetoConteudoAutorizacaoList.Contains("TCS_OBJETO_CONTEUDO_AUTORIZACAO.ID_OBJETO_CONTEUDO"))
	    		{
	    			result.Add("TcsObjetoConteudoAutorizacao|IdObjetoConteudo");
	    			result.Add("TcsObjetoConteudoAutorizacao|TCS_OBJETO_CONTEUDO_AUTORIZACAO.ID_OBJETO_CONTEUDO");
	    		}
	
	    		if (bmDisabledTcsObjetoConteudoAutorizacaoList.Contains("TCS_OBJETO_CONTEUDO_AUTORIZACAO.LX_CONTEUDO_OBJETO"))
	    		{
	    			result.Add("TcsObjetoConteudoAutorizacao|LxConteudoObjeto");
	    			result.Add("TcsObjetoConteudoAutorizacao|TCS_OBJETO_CONTEUDO_AUTORIZACAO.LX_CONTEUDO_OBJETO");
	    		}
	    	}
	    	return result;
	    }

	    #endregion Excluded Filters

	    #region Get By EntitySearchId Methods Definitions.
	
				
	    [Ignore]
	    //Get TcsObjetoAutorizacao By EntitySearchId.
	    public IQueryable<TcsObjetoAutorizacao> GetTcsObjetoAutorizacaoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsObjetoAutorizacaoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsTransacaoAutorizacaoChild By EntitySearchId.
	    public IQueryable<TcsTransacaoAutorizacaoChild> GetTcsTransacaoAutorizacaoChildByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsTransacaoAutorizacaoChildByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsObjetoConteudoAutorizacao By EntitySearchId.
	    public IQueryable<TcsObjetoConteudoAutorizacao> GetTcsObjetoConteudoAutorizacaoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsObjetoConteudoAutorizacaoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsObjetoAutorizacao By EntitySearchId.
	    public IQueryable<TcsObjetoAutorizacao> GetTcsObjetoAutorizacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsObjetoAutorizacaoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsTransacaoAutorizacaoChild By EntitySearchId.
	    public IQueryable<TcsTransacaoAutorizacaoChild> GetTcsTransacaoAutorizacaoChildByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsTransacaoAutorizacaoChildByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsObjetoConteudoAutorizacao By EntitySearchId.
	    public IQueryable<TcsObjetoConteudoAutorizacao> GetTcsObjetoConteudoAutorizacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsObjetoConteudoAutorizacaoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get TcsObjetoAutorizacao By Example.
	    [Ignore]
	    public IQueryable<TcsObjetoAutorizacao> GetTcsObjetoAutorizacaoByExample(TcsObjetoAutorizacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsObjetoAutorizacaoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsTransacaoAutorizacaoChild By Example.
	    [Ignore]
	    public IQueryable<TcsTransacaoAutorizacaoChild> GetTcsTransacaoAutorizacaoChildByExample(TcsTransacaoAutorizacaoChild entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsTransacaoAutorizacaoChildByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsObjetoConteudoAutorizacao By Example.
	    [Ignore]
	    public IQueryable<TcsObjetoConteudoAutorizacao> GetTcsObjetoConteudoAutorizacaoByExample(TcsObjetoConteudoAutorizacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsObjetoConteudoAutorizacaoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsObjetoAutorizacao By Example.
	    [Ignore]
	    public IQueryable<TcsObjetoAutorizacao> GetTcsObjetoAutorizacaoByExampleNoAssociations(TcsObjetoAutorizacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsObjetoAutorizacaoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsTransacaoAutorizacaoChild By Example.
	    [Ignore]
	    public IQueryable<TcsTransacaoAutorizacaoChild> GetTcsTransacaoAutorizacaoChildByExampleNoAssociations(TcsTransacaoAutorizacaoChild entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsTransacaoAutorizacaoChildByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsObjetoConteudoAutorizacao By Example.
	    [Ignore]
	    public IQueryable<TcsObjetoConteudoAutorizacao> GetTcsObjetoConteudoAutorizacaoByExampleNoAssociations(TcsObjetoConteudoAutorizacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsObjetoConteudoAutorizacaoByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public TcsObjetoAutorizacao GetTcsObjetoAutorizacaoByKey(Int64 idObjeto)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsObjetoAutorizacao");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdObjeto"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idObjeto));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsObjetoAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsTransacaoAutorizacaoChild GetTcsTransacaoAutorizacaoChildByKey(Int64 idTransacao)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsTransacaoAutorizacaoChild");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTransacao"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTransacao));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsTransacaoAutorizacaoChildByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsObjetoConteudoAutorizacao GetTcsObjetoConteudoAutorizacaoByKey(Int64 idObjetoConteudo)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsObjetoConteudoAutorizacao");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdObjetoConteudo"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idObjetoConteudo));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsObjetoConteudoAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get TcsObjetoAutorizacaoByEntitySearch.
	    public IQueryable<TcsObjetoAutorizacao> GetTcsObjetoAutorizacaoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsObjetoAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsObjetoAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_OBJETO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsObjetoAutorizacao()		
	            {
	            
                ClasseNome = entity0.CLASSE_NOME
                , DescObjeto = entity0.DESC_OBJETO
                , IdObjeto = entity0.ID_OBJETO
                , LxTipoObjeto = entity0.LX_TIPO_OBJETO
                , LxTipoObjetoName = ((entity0.LX_TIPO_OBJETO) == 1 ? "BO" : ((entity0.LX_TIPO_OBJETO) == 3 ? "Campo" : ((entity0.LX_TIPO_OBJETO) == 10 ? "Filtro" : ((entity0.LX_TIPO_OBJETO) == 9 ? "Layout" : ((entity0.LX_TIPO_OBJETO) == 6 ? "Relatório" : ((entity0.LX_TIPO_OBJETO) == 5 ? "Stored Procedure" : ((entity0.LX_TIPO_OBJETO) == 8 ? "Template de ação de Workflow" : ((entity0.LX_TIPO_OBJETO) == 2 ? "Transação" : ((entity0.LX_TIPO_OBJETO) == 4 ? "Trigger" : ((entity0.LX_TIPO_OBJETO) == 11 ? "Extensão (Objeto de entrada)" : ((entity0.LX_TIPO_OBJETO) == 7 ? "Workflow" : "")))))))))))
                , ObjetoLinx = true
                , PathObjeto = entity0.PATH_OBJETO
			
                ,TcsTransacaoAutorizacaoChildList = 
	                        (from entity1 in entity0.TCS_TRANSACAO_AUTORIZACAO_LISTA
                                  let entity1Al1 = entity1.TCS_OBJETO_AUTORIZACAO
	                        
	                        	
	                        select new TcsTransacaoAutorizacaoChild()
	                        {
	                        
                                ClasseNome = entity1.CLASSE_NOME
                                , CodTransacao = entity1.COD_TRANSACAO
                                , DescTransacao = entity1.DESC_TRANSACAO
                                , IdObjeto = entity1Al1.ID_OBJETO
                                , IdTransacao = entity1.ID_TRANSACAO
                                , Inativo = entity1.INATIVO
                                , LxTipoTransacao = entity1.LX_TIPO_TRANSACAO
                                , LxTipoTransacaoName = ((entity1.LX_TIPO_TRANSACAO) == 7 ? "Assistente" : ((entity1.LX_TIPO_TRANSACAO) == 8 ? "Dashboard" : ((entity1.LX_TIPO_TRANSACAO) == 2 ? "ERP" : ((entity1.LX_TIPO_TRANSACAO) == 6 ? "ERP App" : ((entity1.LX_TIPO_TRANSACAO) == 4 ? "Excel" : ((entity1.LX_TIPO_TRANSACAO) == 3 ? "Loja" : ((entity1.LX_TIPO_TRANSACAO) == 5 ? "Mobile" : ((entity1.LX_TIPO_TRANSACAO) == 1 ? "Todos" : ""))))))))
		
	                        }
	                        )
			
                ,TcsObjetoConteudoAutorizacaoList = 
	                        (from entity1 in entity0.TCS_OBJETO_CONTEUDO_AUTORIZACAO_LISTA
                                  let entity1Al2 = entity1.TCS_OBJETO_AUTORIZACAO
                                  let entity1Al1 = entity1.TCS_LAYOUT_AUTORIZACAO_LISTA
	                        
	                        	
	                        select new TcsObjetoConteudoAutorizacao()
	                        {
	                        
                                ConteudoXml = entity1.CONTEUDO_XML
                                , DescLayout = entity1Al1.DESC_LAYOUT
                                , Detalhes = entity1Al1.DETALHES
                                , Idioma = entity1Al1.IDIOMA
                                , IdLayout = entity1Al1.ID_OBJETO_CONTEUDO
                                , IdObjeto = entity1Al2.ID_OBJETO
                                , IdObjetoConteudo = entity1.ID_OBJETO_CONTEUDO
                                , Inativo = entity1Al1.INATIVO
                                , LayoutLinx = true
                                , LayoutPadrao = entity1Al1.LAYOUT_PADRAO
                                , LxConteudoObjeto = entity1.LX_CONTEUDO_OBJETO
                                , LxConteudoObjetoName = ((entity1.LX_CONTEUDO_OBJETO) == "3" ? "Configuração de Exportação para Excel" : ((entity1.LX_CONTEUDO_OBJETO) == "4" ? "Configuração de Exportação para Report" : ((entity1.LX_CONTEUDO_OBJETO) == "6" ? "Gravação de Layout para Grid" : ((entity1.LX_CONTEUDO_OBJETO) == "1" ? "Layout" : ((entity1.LX_CONTEUDO_OBJETO) == "2" ? "Mídia" : ((entity1.LX_CONTEUDO_OBJETO) == "5" ? "Gravação de Layout para Pivot Table" : ""))))))
                                , LxTipoLayout = entity1Al1.LX_TIPO_LAYOUT
                                , LxTipoLayoutName = ((entity1Al1.LX_TIPO_LAYOUT) == 1 ? "Layout do Sistema" : ((entity1Al1.LX_TIPO_LAYOUT) == 2 ? "Layout do Usuário" : ""))
                                , PossuiFiltro = entity1Al1.POSSUI_FILTRO
                                , Publico = true
                                , UltAtualizacao = entity1Al1.ULT_ATUALIZACAO
		
	                        }
	                        )
		
	            }
	            );
	
	        SetTcsObjetoAutorizacaoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsTransacaoAutorizacaoChildByEntitySearch.
	    public IQueryable<TcsTransacaoAutorizacaoChild> GetTcsTransacaoAutorizacaoChildByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacaoAutorizacaoChild));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoAutorizacaoChild> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_OBJETO_AUTORIZACAO
	            
	            	
	            select new TcsTransacaoAutorizacaoChild()		
	            {
	            
                ClasseNome = entity0.CLASSE_NOME
                , CodTransacao = entity0.COD_TRANSACAO
                , DescTransacao = entity0.DESC_TRANSACAO
                , IdObjeto = entity0Al1.ID_OBJETO
                , IdTransacao = entity0.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , LxTipoTransacao = entity0.LX_TIPO_TRANSACAO
                , LxTipoTransacaoName = ((entity0.LX_TIPO_TRANSACAO) == 7 ? "Assistente" : ((entity0.LX_TIPO_TRANSACAO) == 8 ? "Dashboard" : ((entity0.LX_TIPO_TRANSACAO) == 2 ? "ERP" : ((entity0.LX_TIPO_TRANSACAO) == 6 ? "ERP App" : ((entity0.LX_TIPO_TRANSACAO) == 4 ? "Excel" : ((entity0.LX_TIPO_TRANSACAO) == 3 ? "Loja" : ((entity0.LX_TIPO_TRANSACAO) == 5 ? "Mobile" : ((entity0.LX_TIPO_TRANSACAO) == 1 ? "Todos" : ""))))))))
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsObjetoConteudoAutorizacaoByEntitySearch.
	    public IQueryable<TcsObjetoConteudoAutorizacao> GetTcsObjetoConteudoAutorizacaoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsObjetoConteudoAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsObjetoConteudoAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_OBJETO_CONTEUDO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.TCS_OBJETO_AUTORIZACAO
                  let entity0Al1 = entity0.TCS_LAYOUT_AUTORIZACAO_LISTA
	            
	            	
	            select new TcsObjetoConteudoAutorizacao()		
	            {
	            
                ConteudoXml = entity0.CONTEUDO_XML
                , DescLayout = entity0Al1.DESC_LAYOUT
                , Detalhes = entity0Al1.DETALHES
                , Idioma = entity0Al1.IDIOMA
                , IdLayout = entity0Al1.ID_OBJETO_CONTEUDO
                , IdObjeto = entity0Al2.ID_OBJETO
                , IdObjetoConteudo = entity0.ID_OBJETO_CONTEUDO
                , Inativo = entity0Al1.INATIVO
                , LayoutLinx = true
                , LayoutPadrao = entity0Al1.LAYOUT_PADRAO
                , LxConteudoObjeto = entity0.LX_CONTEUDO_OBJETO
                , LxConteudoObjetoName = ((entity0.LX_CONTEUDO_OBJETO) == "3" ? "Configuração de Exportação para Excel" : ((entity0.LX_CONTEUDO_OBJETO) == "4" ? "Configuração de Exportação para Report" : ((entity0.LX_CONTEUDO_OBJETO) == "6" ? "Gravação de Layout para Grid" : ((entity0.LX_CONTEUDO_OBJETO) == "1" ? "Layout" : ((entity0.LX_CONTEUDO_OBJETO) == "2" ? "Mídia" : ((entity0.LX_CONTEUDO_OBJETO) == "5" ? "Gravação de Layout para Pivot Table" : ""))))))
                , LxTipoLayout = entity0Al1.LX_TIPO_LAYOUT
                , LxTipoLayoutName = ((entity0Al1.LX_TIPO_LAYOUT) == 1 ? "Layout do Sistema" : ((entity0Al1.LX_TIPO_LAYOUT) == 2 ? "Layout do Usuário" : ""))
                , PossuiFiltro = entity0Al1.POSSUI_FILTRO
                , Publico = true
                , UltAtualizacao = entity0Al1.ULT_ATUALIZACAO
		
	            }
	            );
	
	        SetTcsObjetoConteudoAutorizacaoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsObjetoAutorizacaoByEntitySearchNoAssociations.
	    public IQueryable<TcsObjetoAutorizacao> GetTcsObjetoAutorizacaoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsObjetoAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsObjetoAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_OBJETO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsObjetoAutorizacao()		
	            {
	            
                ClasseNome = entity0.CLASSE_NOME
                , DescObjeto = entity0.DESC_OBJETO
                , IdObjeto = entity0.ID_OBJETO
                , LxTipoObjeto = entity0.LX_TIPO_OBJETO
                , LxTipoObjetoName = ((entity0.LX_TIPO_OBJETO) == 1 ? "BO" : ((entity0.LX_TIPO_OBJETO) == 3 ? "Campo" : ((entity0.LX_TIPO_OBJETO) == 10 ? "Filtro" : ((entity0.LX_TIPO_OBJETO) == 9 ? "Layout" : ((entity0.LX_TIPO_OBJETO) == 6 ? "Relatório" : ((entity0.LX_TIPO_OBJETO) == 5 ? "Stored Procedure" : ((entity0.LX_TIPO_OBJETO) == 8 ? "Template de ação de Workflow" : ((entity0.LX_TIPO_OBJETO) == 2 ? "Transação" : ((entity0.LX_TIPO_OBJETO) == 4 ? "Trigger" : ((entity0.LX_TIPO_OBJETO) == 11 ? "Extensão (Objeto de entrada)" : ((entity0.LX_TIPO_OBJETO) == 7 ? "Workflow" : "")))))))))))
                , ObjetoLinx = true
                , PathObjeto = entity0.PATH_OBJETO
		
	            }
	            );
	
	        SetTcsObjetoAutorizacaoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsTransacaoAutorizacaoChildByEntitySearchNoAssociations.
	    public IQueryable<TcsTransacaoAutorizacaoChild> GetTcsTransacaoAutorizacaoChildByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacaoAutorizacaoChild));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoAutorizacaoChild> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_OBJETO_AUTORIZACAO
	            
	            	
	            select new TcsTransacaoAutorizacaoChild()		
	            {
	            
                ClasseNome = entity0.CLASSE_NOME
                , CodTransacao = entity0.COD_TRANSACAO
                , DescTransacao = entity0.DESC_TRANSACAO
                , IdObjeto = entity0Al1.ID_OBJETO
                , IdTransacao = entity0.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , LxTipoTransacao = entity0.LX_TIPO_TRANSACAO
                , LxTipoTransacaoName = ((entity0.LX_TIPO_TRANSACAO) == 7 ? "Assistente" : ((entity0.LX_TIPO_TRANSACAO) == 8 ? "Dashboard" : ((entity0.LX_TIPO_TRANSACAO) == 2 ? "ERP" : ((entity0.LX_TIPO_TRANSACAO) == 6 ? "ERP App" : ((entity0.LX_TIPO_TRANSACAO) == 4 ? "Excel" : ((entity0.LX_TIPO_TRANSACAO) == 3 ? "Loja" : ((entity0.LX_TIPO_TRANSACAO) == 5 ? "Mobile" : ((entity0.LX_TIPO_TRANSACAO) == 1 ? "Todos" : ""))))))))
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsObjetoConteudoAutorizacaoByEntitySearchNoAssociations.
	    public IQueryable<TcsObjetoConteudoAutorizacao> GetTcsObjetoConteudoAutorizacaoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsObjetoConteudoAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsObjetoConteudoAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_OBJETO_CONTEUDO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.TCS_OBJETO_AUTORIZACAO
                  let entity0Al1 = entity0.TCS_LAYOUT_AUTORIZACAO_LISTA
	            
	            	
	            select new TcsObjetoConteudoAutorizacao()		
	            {
	            
                ConteudoXml = entity0.CONTEUDO_XML
                , DescLayout = entity0Al1.DESC_LAYOUT
                , Detalhes = entity0Al1.DETALHES
                , Idioma = entity0Al1.IDIOMA
                , IdLayout = entity0Al1.ID_OBJETO_CONTEUDO
                , IdObjeto = entity0Al2.ID_OBJETO
                , IdObjetoConteudo = entity0.ID_OBJETO_CONTEUDO
                , Inativo = entity0Al1.INATIVO
                , LayoutLinx = true
                , LayoutPadrao = entity0Al1.LAYOUT_PADRAO
                , LxConteudoObjeto = entity0.LX_CONTEUDO_OBJETO
                , LxConteudoObjetoName = ((entity0.LX_CONTEUDO_OBJETO) == "3" ? "Configuração de Exportação para Excel" : ((entity0.LX_CONTEUDO_OBJETO) == "4" ? "Configuração de Exportação para Report" : ((entity0.LX_CONTEUDO_OBJETO) == "6" ? "Gravação de Layout para Grid" : ((entity0.LX_CONTEUDO_OBJETO) == "1" ? "Layout" : ((entity0.LX_CONTEUDO_OBJETO) == "2" ? "Mídia" : ((entity0.LX_CONTEUDO_OBJETO) == "5" ? "Gravação de Layout para Pivot Table" : ""))))))
                , LxTipoLayout = entity0Al1.LX_TIPO_LAYOUT
                , LxTipoLayoutName = ((entity0Al1.LX_TIPO_LAYOUT) == 1 ? "Layout do Sistema" : ((entity0Al1.LX_TIPO_LAYOUT) == 2 ? "Layout do Usuário" : ""))
                , PossuiFiltro = entity0Al1.POSSUI_FILTRO
                , Publico = true
                , UltAtualizacao = entity0Al1.ULT_ATUALIZACAO
		
	            }
	            );
	
	        SetTcsObjetoConteudoAutorizacaoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsTransacaoAutorizacaoChildParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsTransacaoAutorizacaoChildParentComposition> GetTcsTransacaoAutorizacaoChildParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "TCS_OBJETO_AUTORIZACAO", "TCS_TRANSACAO_AUTORIZACAO", "TCS_OBJETO_AUTORIZACAO", typeof(TcsTransacaoAutorizacaoChildParentComposition), typeof(TcsObjetoConteudoAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoAutorizacaoChildParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_OBJETO_AUTORIZACAO
	            
	            	
	            select new TcsTransacaoAutorizacaoChildParentComposition()		
	            {
	            
                ClasseNome = entity0.CLASSE_NOME
                , CodTransacao = entity0.COD_TRANSACAO
                , DescTransacao = entity0.DESC_TRANSACAO
                , IdObjeto = entity0Al1.ID_OBJETO
                , IdTransacao = entity0.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , LxTipoTransacao = entity0.LX_TIPO_TRANSACAO
                , LxTipoTransacaoName = ((entity0.LX_TIPO_TRANSACAO) == 7 ? "Assistente" : ((entity0.LX_TIPO_TRANSACAO) == 8 ? "Dashboard" : ((entity0.LX_TIPO_TRANSACAO) == 2 ? "ERP" : ((entity0.LX_TIPO_TRANSACAO) == 6 ? "ERP App" : ((entity0.LX_TIPO_TRANSACAO) == 4 ? "Excel" : ((entity0.LX_TIPO_TRANSACAO) == 3 ? "Loja" : ((entity0.LX_TIPO_TRANSACAO) == 5 ? "Mobile" : ((entity0.LX_TIPO_TRANSACAO) == 1 ? "Todos" : ""))))))))
                //TcsObjetoAutorizacao Properties.
                , DescObjeto = entity0.TCS_OBJETO_AUTORIZACAO.DESC_OBJETO
                , LxTipoObjeto = entity0.TCS_OBJETO_AUTORIZACAO.LX_TIPO_OBJETO
                , LxTipoObjetoName = ((entity0.TCS_OBJETO_AUTORIZACAO.LX_TIPO_OBJETO) == 1 ? "BO" : ((entity0.TCS_OBJETO_AUTORIZACAO.LX_TIPO_OBJETO) == 3 ? "Campo" : ((entity0.TCS_OBJETO_AUTORIZACAO.LX_TIPO_OBJETO) == 10 ? "Filtro" : ((entity0.TCS_OBJETO_AUTORIZACAO.LX_TIPO_OBJETO) == 9 ? "Layout" : ((entity0.TCS_OBJETO_AUTORIZACAO.LX_TIPO_OBJETO) == 6 ? "Relatório" : ((entity0.TCS_OBJETO_AUTORIZACAO.LX_TIPO_OBJETO) == 5 ? "Stored Procedure" : ((entity0.TCS_OBJETO_AUTORIZACAO.LX_TIPO_OBJETO) == 8 ? "Template de ação de Workflow" : ((entity0.TCS_OBJETO_AUTORIZACAO.LX_TIPO_OBJETO) == 2 ? "Transação" : ((entity0.TCS_OBJETO_AUTORIZACAO.LX_TIPO_OBJETO) == 4 ? "Trigger" : ((entity0.TCS_OBJETO_AUTORIZACAO.LX_TIPO_OBJETO) == 11 ? "Extensão (Objeto de entrada)" : ((entity0.TCS_OBJETO_AUTORIZACAO.LX_TIPO_OBJETO) == 7 ? "Workflow" : "")))))))))))
                , ObjetoLinx = true
                , PathObjeto = entity0.TCS_OBJETO_AUTORIZACAO.PATH_OBJETO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsObjetoConteudoAutorizacaoParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsObjetoConteudoAutorizacaoParentComposition> GetTcsObjetoConteudoAutorizacaoParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "TCS_OBJETO_AUTORIZACAO", "TCS_OBJETO_CONTEUDO_AUTORIZACAO", "TCS_OBJETO_AUTORIZACAO", typeof(TcsObjetoConteudoAutorizacaoParentComposition), typeof(TcsTransacaoAutorizacaoChild));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsObjetoConteudoAutorizacaoParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_OBJETO_CONTEUDO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.TCS_OBJETO_AUTORIZACAO
                  let entity0Al1 = entity0.TCS_LAYOUT_AUTORIZACAO_LISTA
	            
	            	
	            select new TcsObjetoConteudoAutorizacaoParentComposition()		
	            {
	            
                ConteudoXml = entity0.CONTEUDO_XML
                , DescLayout = entity0Al1.DESC_LAYOUT
                , Detalhes = entity0Al1.DETALHES
                , Idioma = entity0Al1.IDIOMA
                , IdLayout = entity0Al1.ID_OBJETO_CONTEUDO
                , IdObjeto = entity0Al2.ID_OBJETO
                , IdObjetoConteudo = entity0.ID_OBJETO_CONTEUDO
                , Inativo = entity0Al1.INATIVO
                , LayoutLinx = true
                , LayoutPadrao = entity0Al1.LAYOUT_PADRAO
                , LxConteudoObjeto = entity0.LX_CONTEUDO_OBJETO
                , LxConteudoObjetoName = ((entity0.LX_CONTEUDO_OBJETO) == "3" ? "Configuração de Exportação para Excel" : ((entity0.LX_CONTEUDO_OBJETO) == "4" ? "Configuração de Exportação para Report" : ((entity0.LX_CONTEUDO_OBJETO) == "6" ? "Gravação de Layout para Grid" : ((entity0.LX_CONTEUDO_OBJETO) == "1" ? "Layout" : ((entity0.LX_CONTEUDO_OBJETO) == "2" ? "Mídia" : ((entity0.LX_CONTEUDO_OBJETO) == "5" ? "Gravação de Layout para Pivot Table" : ""))))))
                , LxTipoLayout = entity0Al1.LX_TIPO_LAYOUT
                , LxTipoLayoutName = ((entity0Al1.LX_TIPO_LAYOUT) == 1 ? "Layout do Sistema" : ((entity0Al1.LX_TIPO_LAYOUT) == 2 ? "Layout do Usuário" : ""))
                , PossuiFiltro = entity0Al1.POSSUI_FILTRO
                , Publico = true
                , UltAtualizacao = entity0Al1.ULT_ATUALIZACAO
                //TcsObjetoAutorizacao Properties.
                , ClasseNome = entity0.TCS_OBJETO_AUTORIZACAO.CLASSE_NOME
                , DescObjeto = entity0.TCS_OBJETO_AUTORIZACAO.DESC_OBJETO
                , LxTipoObjeto = entity0.TCS_OBJETO_AUTORIZACAO.LX_TIPO_OBJETO
                , LxTipoObjetoName = ((entity0.TCS_OBJETO_AUTORIZACAO.LX_TIPO_OBJETO) == 1 ? "BO" : ((entity0.TCS_OBJETO_AUTORIZACAO.LX_TIPO_OBJETO) == 3 ? "Campo" : ((entity0.TCS_OBJETO_AUTORIZACAO.LX_TIPO_OBJETO) == 10 ? "Filtro" : ((entity0.TCS_OBJETO_AUTORIZACAO.LX_TIPO_OBJETO) == 9 ? "Layout" : ((entity0.TCS_OBJETO_AUTORIZACAO.LX_TIPO_OBJETO) == 6 ? "Relatório" : ((entity0.TCS_OBJETO_AUTORIZACAO.LX_TIPO_OBJETO) == 5 ? "Stored Procedure" : ((entity0.TCS_OBJETO_AUTORIZACAO.LX_TIPO_OBJETO) == 8 ? "Template de ação de Workflow" : ((entity0.TCS_OBJETO_AUTORIZACAO.LX_TIPO_OBJETO) == 2 ? "Transação" : ((entity0.TCS_OBJETO_AUTORIZACAO.LX_TIPO_OBJETO) == 4 ? "Trigger" : ((entity0.TCS_OBJETO_AUTORIZACAO.LX_TIPO_OBJETO) == 11 ? "Extensão (Objeto de entrada)" : ((entity0.TCS_OBJETO_AUTORIZACAO.LX_TIPO_OBJETO) == 7 ? "Workflow" : "")))))))))))
                , ObjetoLinx = true
                , PathObjeto = entity0.TCS_OBJETO_AUTORIZACAO.PATH_OBJETO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
	
	    [Ignore()]
	    private void SetTcsObjetoAutorizacaoBusinessFilter(ref IQueryable<TcsObjetoAutorizacao> query, List<EntitySearch> entitySearchList)
	    {
	    		int idxElement;
	    		string operatorValue;
	    		object value;
	    		//Get query by functions
	    		if (entitySearchList.Count > 0)
	    		{
	    			foreach (EntitySearch search in entitySearchList.Where(e => e.EntityName == "TcsObjetoAutorizacao"))
	    			{

	
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "ObjetoLinx" || e.Value.ToString() == "true")))
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
	    										bool tmpObjetoLinx1 = (bool)value;
	    										query = from r in query where r.ObjetoLinx == tmpObjetoLinx1 select r;
	    										break;
	    									case "!=":
	    										bool tmpObjetoLinx2 = (bool)value;
	    										query = from r in query where r.ObjetoLinx != tmpObjetoLinx2 select r;
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



	    [Ignore()]
	    private void SetTcsObjetoConteudoAutorizacaoBusinessFilter(ref IQueryable<TcsObjetoConteudoAutorizacao> query, List<EntitySearch> entitySearchList)
	    {
	    		int idxElement;
	    		string operatorValue;
	    		object value;
	    		//Get query by functions
	    		if (entitySearchList.Count > 0)
	    		{
	    			foreach (EntitySearch search in entitySearchList.Where(e => e.EntityName == "TcsObjetoConteudoAutorizacao"))
	    			{

	
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "LayoutLinx" || e.Value.ToString() == "true")))
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
	    										bool tmpLayoutLinx1 = (bool)value;
	    										query = from r in query where r.LayoutLinx == tmpLayoutLinx1 select r;
	    										break;
	    									case "!=":
	    										bool tmpLayoutLinx2 = (bool)value;
	    										query = from r in query where r.LayoutLinx != tmpLayoutLinx2 select r;
	    										break;

	
	    									default:
	    										break;
	    								}                                
	    							}
	    						}
        					} 

    
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "Publico" || e.Value.ToString() == "true")))
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
	    										System.Nullable<Boolean> tmpPublico1 = (System.Nullable<Boolean>)value;
	    										query = from r in query where r.Publico == tmpPublico1 select r;
	    										break;
	    									case "!=":
	    										System.Nullable<Boolean> tmpPublico2 = (System.Nullable<Boolean>)value;
	    										query = from r in query where r.Publico != tmpPublico2 select r;
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
	    //Get PagedTcsObjetoAutorizacao.
	    public IQueryable<TcsObjetoAutorizacao> GetPagedTcsObjetoAutorizacao(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsObjetoAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsObjetoAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_OBJETO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_OBJETO ascending
	            
	            	
	            select new TcsObjetoAutorizacao()		
	            {
	            
                ClasseNome = entity0.CLASSE_NOME
                , DescObjeto = entity0.DESC_OBJETO
                , IdObjeto = entity0.ID_OBJETO
                , LxTipoObjeto = entity0.LX_TIPO_OBJETO
                , LxTipoObjetoName = ((entity0.LX_TIPO_OBJETO) == 1 ? "BO" : ((entity0.LX_TIPO_OBJETO) == 3 ? "Campo" : ((entity0.LX_TIPO_OBJETO) == 10 ? "Filtro" : ((entity0.LX_TIPO_OBJETO) == 9 ? "Layout" : ((entity0.LX_TIPO_OBJETO) == 6 ? "Relatório" : ((entity0.LX_TIPO_OBJETO) == 5 ? "Stored Procedure" : ((entity0.LX_TIPO_OBJETO) == 8 ? "Template de ação de Workflow" : ((entity0.LX_TIPO_OBJETO) == 2 ? "Transação" : ((entity0.LX_TIPO_OBJETO) == 4 ? "Trigger" : ((entity0.LX_TIPO_OBJETO) == 11 ? "Extensão (Objeto de entrada)" : ((entity0.LX_TIPO_OBJETO) == 7 ? "Workflow" : "")))))))))))
                , ObjetoLinx = true
                , PathObjeto = entity0.PATH_OBJETO
		
	            }
	            ).Skip(skip).Take(take);
	
	        SetTcsObjetoAutorizacaoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsTransacaoAutorizacaoChild.
	    public IQueryable<TcsTransacaoAutorizacaoChild> GetPagedTcsTransacaoAutorizacaoChild(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacaoAutorizacaoChild));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoAutorizacaoChild> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_OBJETO_AUTORIZACAO
                orderby entity0.ID_TRANSACAO ascending
	            
	            	
	            select new TcsTransacaoAutorizacaoChild()		
	            {
	            
                ClasseNome = entity0.CLASSE_NOME
                , CodTransacao = entity0.COD_TRANSACAO
                , DescTransacao = entity0.DESC_TRANSACAO
                , IdObjeto = entity0Al1.ID_OBJETO
                , IdTransacao = entity0.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , LxTipoTransacao = entity0.LX_TIPO_TRANSACAO
                , LxTipoTransacaoName = ((entity0.LX_TIPO_TRANSACAO) == 7 ? "Assistente" : ((entity0.LX_TIPO_TRANSACAO) == 8 ? "Dashboard" : ((entity0.LX_TIPO_TRANSACAO) == 2 ? "ERP" : ((entity0.LX_TIPO_TRANSACAO) == 6 ? "ERP App" : ((entity0.LX_TIPO_TRANSACAO) == 4 ? "Excel" : ((entity0.LX_TIPO_TRANSACAO) == 3 ? "Loja" : ((entity0.LX_TIPO_TRANSACAO) == 5 ? "Mobile" : ((entity0.LX_TIPO_TRANSACAO) == 1 ? "Todos" : ""))))))))
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsObjetoConteudoAutorizacao.
	    public IQueryable<TcsObjetoConteudoAutorizacao> GetPagedTcsObjetoConteudoAutorizacao(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsObjetoConteudoAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsObjetoConteudoAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_OBJETO_CONTEUDO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.TCS_OBJETO_AUTORIZACAO
                  let entity0Al1 = entity0.TCS_LAYOUT_AUTORIZACAO_LISTA
                orderby entity0.ID_OBJETO_CONTEUDO ascending
	            
	            	
	            select new TcsObjetoConteudoAutorizacao()		
	            {
	            
                ConteudoXml = entity0.CONTEUDO_XML
                , DescLayout = entity0Al1.DESC_LAYOUT
                , Detalhes = entity0Al1.DETALHES
                , Idioma = entity0Al1.IDIOMA
                , IdLayout = entity0Al1.ID_OBJETO_CONTEUDO
                , IdObjeto = entity0Al2.ID_OBJETO
                , IdObjetoConteudo = entity0.ID_OBJETO_CONTEUDO
                , Inativo = entity0Al1.INATIVO
                , LayoutLinx = true
                , LayoutPadrao = entity0Al1.LAYOUT_PADRAO
                , LxConteudoObjeto = entity0.LX_CONTEUDO_OBJETO
                , LxConteudoObjetoName = ((entity0.LX_CONTEUDO_OBJETO) == "3" ? "Configuração de Exportação para Excel" : ((entity0.LX_CONTEUDO_OBJETO) == "4" ? "Configuração de Exportação para Report" : ((entity0.LX_CONTEUDO_OBJETO) == "6" ? "Gravação de Layout para Grid" : ((entity0.LX_CONTEUDO_OBJETO) == "1" ? "Layout" : ((entity0.LX_CONTEUDO_OBJETO) == "2" ? "Mídia" : ((entity0.LX_CONTEUDO_OBJETO) == "5" ? "Gravação de Layout para Pivot Table" : ""))))))
                , LxTipoLayout = entity0Al1.LX_TIPO_LAYOUT
                , LxTipoLayoutName = ((entity0Al1.LX_TIPO_LAYOUT) == 1 ? "Layout do Sistema" : ((entity0Al1.LX_TIPO_LAYOUT) == 2 ? "Layout do Usuário" : ""))
                , PossuiFiltro = entity0Al1.POSSUI_FILTRO
                , Publico = true
                , UltAtualizacao = entity0Al1.ULT_ATUALIZACAO
		
	            }
	            ).Skip(skip).Take(take);
	
	        SetTcsObjetoConteudoAutorizacaoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsObjetoAutorizacaoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsObjetoAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_OBJETO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsTransacaoAutorizacaoChildCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacaoAutorizacaoChild));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_TRANSACAO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_OBJETO_AUTORIZACAO
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsObjetoConteudoAutorizacaoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsObjetoConteudoAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_OBJETO_CONTEUDO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entityAl2 = entity.TCS_OBJETO_AUTORIZACAO
                  let entityAl1 = entity.TCS_LAYOUT_AUTORIZACAO_LISTA
	            
	            select 1
	            ).Count();	
		
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    
	    [Update()]	
	    //Update TcsObjetoAutorizacao.
	    public void UpdateTcsObjetoAutorizacao(TcsObjetoAutorizacao entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsObjetoAutorizacao.
	    public void InsertTcsObjetoAutorizacao(TcsObjetoAutorizacao entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsObjetoAutorizacao.
	    public void DeleteTcsObjetoAutorizacao(TcsObjetoAutorizacao entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsTransacaoAutorizacaoChild.
	    public void UpdateTcsTransacaoAutorizacaoChild(TcsTransacaoAutorizacaoChild entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert TcsTransacaoAutorizacaoChild.
	    public void InsertTcsTransacaoAutorizacaoChild(TcsTransacaoAutorizacaoChild entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete TcsTransacaoAutorizacaoChild.
	    public void DeleteTcsTransacaoAutorizacaoChild(TcsTransacaoAutorizacaoChild entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsObjetoConteudoAutorizacao.
	    public void UpdateTcsObjetoConteudoAutorizacao(TcsObjetoConteudoAutorizacao entity)
	    {



	
	        if (entity.TcsObjetoAutorizacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsObjetoAutorizacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsObjetoAutorizacao); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsObjetoConteudoAutorizacao.
	    public void InsertTcsObjetoConteudoAutorizacao(TcsObjetoConteudoAutorizacao entity)
	    {



	
	        if (entity.TcsObjetoAutorizacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsObjetoAutorizacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsObjetoAutorizacao);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsObjetoConteudoAutorizacao.
	    public void DeleteTcsObjetoConteudoAutorizacao(TcsObjetoConteudoAutorizacao entity)
	    {



	
	        if (entity.TcsObjetoAutorizacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsObjetoAutorizacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsObjetoAutorizacao);
	            

	
	        }

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}