					
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

namespace Linx.Framework.BV.Objeto
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_OBJETO.ID_OBJETO", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsObjeto,TcsObjeto.TcsTransacao];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdObjeto];ReadOnly[false];Entities[TCS_OBJETO:IdObjeto];SubQueryInfo[];EdmEntityName[TCS_OBJETO];EntityRelations[];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsObjeto")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Objeto.TcsObjeto")]
	public partial class TcsObjeto : Linx.Data.Entity
	{

	
		
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.TcsTransacaoList != null && this.TcsTransacaoList.Count() > 0)
	      {
	         foreach (var entity in this.TcsTransacaoList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.TcsTransacaoList != null)
	      {
	         foreach (var detail in this.TcsTransacaoList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsTransacaoList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(ObjetoDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("TcsTransacao"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsTransacao");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdObjeto"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdObjeto));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsTransacao and all sub-details
	         if (this.TcsTransacaoList == null || this.TcsTransacaoList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsTransacaoList = context.GetPagedTcsTransacao(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsTransacaoList = (from r in context.GetTcsTransacaoByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _TcsTransacaoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsTransacao && ((TcsTransacao)e.Entity).TcsObjeto == null && e.Associations == null && e.OriginalAssociations == null && ((TcsTransacao)e.Entity).IdObjeto == this.IdObjeto).ToList();
 	      if (_TcsTransacaoElements.Count > 0 && this.TcsTransacaoList.Count() == 0)
 	      {
 	          this.TcsTransacaoList = _TcsTransacaoElements.Select(e => (TcsTransacao)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsTransacaoElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsTransacao)detail.Entity).TcsObjeto = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsObjeto", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsTransacaoList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For ClasseNome
	    partial void OnClasseNomeChanging(System.String value);
	    partial void OnClasseNomeChanged();

	    private System.String _ClasseNome;

	    [DataMember(Name = "ClasseNome", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Classe", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_OBJETO.CLASSE_NOME];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO.CLASSE_NOME")]
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
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_OBJETO.DESC_OBJETO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO.DESC_OBJETO")]
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
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_OBJETO.ID_OBJETO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO.ID_OBJETO")]
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
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoObjeto];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_OBJETO.LX_TIPO_OBJETO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO.LX_TIPO_OBJETO")]
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
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[false];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="false")]
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
	    [Display(Name = "Path", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(2000)]
	    [FunctionalPoint("Precision[2000:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_OBJETO.PATH_OBJETO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO.PATH_OBJETO")]
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
	 
		
	    private IEnumerable<TcsTransacao> _TcsTransacaoList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsObjeto_TcsTransacao", "IdObjeto", "IdObjeto", IsForeignKey=false)]
	    [DataMember(Name = "TcsTransacaoList", EmitDefaultValue = true)]
	    public IEnumerable<TcsTransacao> TcsTransacaoList
	    {
	        get
	        {
	
	            if (this._TcsTransacaoList == null)
	            	this._TcsTransacaoList = new List<TcsTransacao>();
	
	            return this._TcsTransacaoList;
	        }
	        set
	        {
	            if (this._TcsTransacaoList != value)
	            {
	                this._TcsTransacaoList = value;
	                this.RaisePropertyChanged("TcsTransacaoList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_OBJETO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_OBJETO), QualifiedEntitySetName = "ControleSistemaContext.TCS_OBJETO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO.ID_OBJETO", Source = "IdObjeto", Target = "ID_OBJETO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_OBJETO", RelationPropertyName = "TCS_OBJETO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO.CLASSE_NOME", Source = "ClasseNome", Target = "CLASSE_NOME", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_OBJETO", RelationPropertyName = "TCS_OBJETO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO.DESC_OBJETO", Source = "DescObjeto", Target = "DESC_OBJETO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_OBJETO", RelationPropertyName = "TCS_OBJETO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO.PATH_OBJETO", Source = "PathObjeto", Target = "PATH_OBJETO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_OBJETO", RelationPropertyName = "TCS_OBJETO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO.LX_TIPO_OBJETO", Source = "LxTipoObjeto", Target = "LX_TIPO_OBJETO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_OBJETO", RelationPropertyName = "TCS_OBJETO" });

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

		

	[LinxPublicationView(PrimaryKeys="TCS_TRANSACAO.ID_TRANSACAO", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Transação];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTransacao];ReadOnly[true];Entities[TCS_TRANSACAO:IdTransacao];SubQueryInfo[Select 1 From #ParentAlias#. as #Alias#];EdmEntityName[TCS_TRANSACAO];EntityRelations[];EdmParentEntityName[TCS_OBJETO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsTransacao")]
	[Serializable()]
	public partial class TcsTransacao : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(ObjetoDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsObjeto");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdObjeto"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdObjeto));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsObjeto
	         this.TcsObjeto = (from r in context.GetTcsObjetoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO.CLASSE_NOME];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO.CLASSE_NOME")]
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
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO.COD_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO.COD_TRANSACAO")]
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
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO.DESC_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO.DESC_TRANSACAO")]
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
	    [Display(Name = "Id Objeto", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO.ID_OBJETO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO.ID_OBJETO")]
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
	    [Display(Name = "Id Transacao", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO.ID_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO.ID_TRANSACAO")]
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
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO.INATIVO")]
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
	    [Display(Name = "Tipo transação", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoTransacao];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO.LX_TIPO_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO.LX_TIPO_TRANSACAO")]
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
	    [Display(Name = "Id Transacao (Tmp)", Description="Temporary Key", Order = 6, AutoGenerateField = false, GroupName="", ResourceType= null)]
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
	 
	    private TcsObjeto _TcsObjeto;
	    [DataMember(Name = "TcsObjeto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsObjeto_TcsTransacao", "IdObjeto", "IdObjeto", IsForeignKey=true)]
	    public TcsObjeto TcsObjeto
	    {
	        get
	        {
	            return this._TcsObjeto;
	        }
	        set
	        {
	            if (this._TcsObjeto != value)
	            {
	                this._TcsObjeto = value;
	                this.RaisePropertyChanged("TcsObjetoList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_TRANSACAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_TRANSACAO), QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO.INATIVO", Source = "Inativo", Target = "INATIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO", RelationPropertyName = "TCS_TRANSACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO.ID_OBJETO", Source = "IdObjeto", Target = "ID_OBJETO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO", RelationPropertyName = "TCS_TRANSACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO.CLASSE_NOME", Source = "ClasseNome", Target = "CLASSE_NOME", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO", RelationPropertyName = "TCS_TRANSACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO.ID_TRANSACAO", Source = "IdTransacao", Target = "ID_TRANSACAO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO", RelationPropertyName = "TCS_TRANSACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO.COD_TRANSACAO", Source = "CodTransacao", Target = "COD_TRANSACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO", RelationPropertyName = "TCS_TRANSACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO.DESC_TRANSACAO", Source = "DescTransacao", Target = "DESC_TRANSACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO", RelationPropertyName = "TCS_TRANSACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO.LX_TIPO_TRANSACAO", Source = "LxTipoTransacao", Target = "LX_TIPO_TRANSACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO", RelationPropertyName = "TCS_TRANSACAO" });

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
	    [Display(Name = "Tipo transação", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
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

		

	[LinxPublicationView(PrimaryKeys="TCS_OBJETO_CONTEUDO.ID_OBJETO_CONTEUDO", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsObjetoConteudoMnt];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdObjetoConteudo];ReadOnly[false];Entities[TCS_OBJETO_CONTEUDO:IdObjetoConteudo];SubQueryInfo[];EdmEntityName[TCS_OBJETO_CONTEUDO];EntityRelations[TCS_LAYOUT_LISTA(TCS_LAYOUT)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsObjetoConteudoMnt")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Objeto.TcsObjetoConteudoMnt")]
	public partial class TcsObjetoConteudoMnt : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For ConteudoXml
	    partial void OnConteudoXmlChanging(System.String value);
	    partial void OnConteudoXmlChanged();

	    private System.String _ConteudoXml;

	    [DataMember(IsRequired = true, Name = "ConteudoXml", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Conteudo Xml", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_OBJETO_CONTEUDO.CONTEUDO_XML];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO_CONTEUDO.CONTEUDO_XML")]
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
	    //Extensibility Partial Method Definitions For IdObjeto
	    partial void OnIdObjetoChanging(Int64 value);
	    partial void OnIdObjetoChanged();

	    private Int64 _IdObjeto;

	    [DataMember(IsRequired = true, Name = "IdObjeto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Objeto", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_OBJETO_CONTEUDO.ID_OBJETO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO_CONTEUDO.ID_OBJETO")]
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
	    [Display(Name = "Id Objeto Conteudo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_OBJETO_CONTEUDO.ID_OBJETO_CONTEUDO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO_CONTEUDO.ID_OBJETO_CONTEUDO")]
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

	    private Int64 _TemporaryIdObjetoConteudo;
	    [DataMember(Name = "TemporaryIdObjetoConteudo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Objeto Conteudo (Tmp)", Description="Temporary Key", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
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

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_OBJETO_CONTEUDO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_OBJETO_CONTEUDO), QualifiedEntitySetName = "ControleSistemaContext.TCS_OBJETO_CONTEUDO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO_CONTEUDO.ID_OBJETO", Source = "IdObjeto", Target = "ID_OBJETO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_OBJETO_CONTEUDO", RelationPropertyName = "TCS_OBJETO_CONTEUDO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO_CONTEUDO.CONTEUDO_XML", Source = "ConteudoXml", Target = "CONTEUDO_XML", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_OBJETO_CONTEUDO", RelationPropertyName = "TCS_OBJETO_CONTEUDO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO_CONTEUDO.ID_OBJETO_CONTEUDO", Source = "IdObjetoConteudo", Target = "ID_OBJETO_CONTEUDO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_OBJETO_CONTEUDO", RelationPropertyName = "TCS_OBJETO_CONTEUDO" });

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

		

	[LinxPublicationView(PrimaryKeys="ConfiguracaoExportacao.EntityUniqueKey", IsUpdatable=false, EdmName="")]
		
	[DataContract(IsReference = false, Name = "ConfiguracaoExportacao")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Objeto.ConfiguracaoExportacao")]
	public partial class ConfiguracaoExportacao 
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
	 


	    private Int64 _Id;

	    [DataMember(Name = "Id", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public Int64 Id
	    {
	    	    get
	    	    {
	    	          return _Id;
	    	    }
	    	    set
	    	    {
	    	          this._Id = value;
	    	    }
	    }

	    private string _Name;

	    [DataMember(Name = "Name", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Name
	    {
	    	    get
	    	    {
	    	          return _Name;
	    	    }
	    	    set
	    	    {
	    	          this._Name = value;
	    	    }
	    }

	    private string _Adapter;

	    [DataMember(Name = "Adapter", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Adapter
	    {
	    	    get
	    	    {
	    	          return _Adapter;
	    	    }
	    	    set
	    	    {
	    	          this._Adapter = value;
	    	    }
	    }

	    private string _JEntitySearch;

	    [DataMember(Name = "JEntitySearch", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string JEntitySearch
	    {
	    	    get
	    	    {
	    	          return _JEntitySearch;
	    	    }
	    	    set
	    	    {
	    	          this._JEntitySearch = value;
	    	    }
	    }

	    private string _TranslatedJEntitySearch;

	    [DataMember(Name = "TranslatedJEntitySearch", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string TranslatedJEntitySearch
	    {
	    	    get
	    	    {
	    	          return _TranslatedJEntitySearch;
	    	    }
	    	    set
	    	    {
	    	          this._TranslatedJEntitySearch = value;
	    	    }
	    }

	    private string _Columns;

	    [DataMember(Name = "Columns", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Columns
	    {
	    	    get
	    	    {
	    	          return _Columns;
	    	    }
	    	    set
	    	    {
	    	          this._Columns = value;
	    	    }
	    }

	    private string _BasicFeedUrl;

	    [DataMember(Name = "BasicFeedUrl", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string BasicFeedUrl
	    {
	    	    get
	    	    {
	    	          return _BasicFeedUrl;
	    	    }
	    	    set
	    	    {
	    	          this._BasicFeedUrl = value;
	    	    }
	    }

	    private string _ParentFullTypeName;

	    [DataMember(Name = "ParentFullTypeName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ParentFullTypeName
	    {
	    	    get
	    	    {
	    	          return _ParentFullTypeName;
	    	    }
	    	    set
	    	    {
	    	          this._ParentFullTypeName = value;
	    	    }
	    }

	    private Boolean _ExportMedia;

	    [DataMember(Name = "ExportMedia", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public Boolean ExportMedia
	    {
	    	    get
	    	    {
	    	          return _ExportMedia;
	    	    }
	    	    set
	    	    {
	    	          this._ExportMedia = value;
	    	    }
	    }

	    private bool _IsExcelDataSource;

	    [DataMember(Name = "IsExcelDataSource", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="true")]
	    public bool IsExcelDataSource
	    {
	    	    get
	    	    {
	    	          return _IsExcelDataSource;
	    	    }
	    	    set
	    	    {
	    	          this._IsExcelDataSource = value;
	    	    }
	    }

	    private string _Content;

	    [DataMember(Name = "Content", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Content
	    {
	    	    get
	    	    {
	    	          return _Content;
	    	    }
	    	    set
	    	    {
	    	          this._Content = value;
	    	    }
	    }

	    private string _ProjectName;

	    [DataMember(Name = "ProjectName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ProjectName
	    {
	    	    get
	    	    {
	    	          return _ProjectName;
	    	    }
	    	    set
	    	    {
	    	          this._ProjectName = value;
	    	    }
	    }

	    private string _ViewName;

	    [DataMember(Name = "ViewName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ViewName
	    {
	    	    get
	    	    {
	    	          return _ViewName;
	    	    }
	    	    set
	    	    {
	    	          this._ViewName = value;
	    	    }
	    }

	    private string _LayoutName;

	    [DataMember(Name = "LayoutName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LayoutName
	    {
	    	    get
	    	    {
	    	          return _LayoutName;
	    	    }
	    	    set
	    	    {
	    	          this._LayoutName = value;
	    	    }
	    }

	    private string _PivotName;

	    [DataMember(Name = "PivotName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string PivotName
	    {
	    	    get
	    	    {
	    	          return _PivotName;
	    	    }
	    	    set
	    	    {
	    	          this._PivotName = value;
	    	    }
	    }

	    private bool _Selected;

	    [DataMember(Name = "Selected", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public bool Selected
	    {
	    	    get
	    	    {
	    	          return _Selected;
	    	    }
	    	    set
	    	    {
	    	          this._Selected = value;
	    	    }
	    }

	    private string _PivotContainerId;

	    [DataMember(Name = "PivotContainerId", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string PivotContainerId
	    {
	    	    get
	    	    {
	    	          return _PivotContainerId;
	    	    }
	    	    set
	    	    {
	    	          this._PivotContainerId = value;
	    	    }
	    }

	    private long _UserId;

	    [DataMember(Name = "UserId", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public long UserId
	    {
	    	    get
	    	    {
	    	          return _UserId;
	    	    }
	    	    set
	    	    {
	    	          this._UserId = value;
	    	    }
	    }

	    private string _UserName;

	    [DataMember(Name = "UserName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string UserName
	    {
	    	    get
	    	    {
	    	          return _UserName;
	    	    }
	    	    set
	    	    {
	    	          this._UserName = value;
	    	    }
	    }

	    private bool _IsUserLayout;

	    [DataMember(Name = "IsUserLayout", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public bool IsUserLayout
	    {
	    	    get
	    	    {
	    	          return _IsUserLayout;
	    	    }
	    	    set
	    	    {
	    	          this._IsUserLayout = value;
	    	    }
	    }

	    private string _Users;

	    [DataMember(Name = "Users", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Users
	    {
	    	    get
	    	    {
	    	          return _Users;
	    	    }
	    	    set
	    	    {
	    	          this._Users = value;
	    	    }
	    }

	    private string _Profiles;

	    [DataMember(Name = "Profiles", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Profiles
	    {
	    	    get
	    	    {
	    	          return _Profiles;
	    	    }
	    	    set
	    	    {
	    	          this._Profiles = value;
	    	    }
	    }

	    private bool _AllowMultipleGpecon;

	    [DataMember(Name = "AllowMultipleGpecon", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public bool AllowMultipleGpecon
	    {
	    	    get
	    	    {
	    	          return _AllowMultipleGpecon;
	    	    }
	    	    set
	    	    {
	    	          this._AllowMultipleGpecon = value;
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

		

	[LinxPublicationView(PrimaryKeys="TCS_OBJETO_PERMISSAO.ID_TCS_OBJETO_PERMISSAO", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsObjetoPermissao];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsObjetoPermissao];ReadOnly[false];Entities[TCS_OBJETO_PERMISSAO:IdTcsObjetoPermissao];SubQueryInfo[];EdmEntityName[TCS_OBJETO_PERMISSAO];EntityRelations[TCS_PERFIL(TCS_PERFIL)#TCS_USUARIO(TCS_USUARIO)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsObjetoPermissao")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Objeto.TcsObjetoPermissao")]
	public partial class TcsObjetoPermissao : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For IdObjeto
	    partial void OnIdObjetoChanging(Int64 value);
	    partial void OnIdObjetoChanged();

	    private Int64 _IdObjeto;

	    [DataMember(IsRequired = true, Name = "IdObjeto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Objeto", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_OBJETO_PERMISSAO.ID_OBJETO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO_PERMISSAO.ID_OBJETO")]
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
	    [Display(Name = "Id Objeto Conteudo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_OBJETO_PERMISSAO.ID_OBJETO_CONTEUDO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO_PERMISSAO.ID_OBJETO_CONTEUDO")]
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
	    //Extensibility Partial Method Definitions For IdPerfil
	    partial void OnIdPerfilChanging(System.Nullable<Int64> value);
	    partial void OnIdPerfilChanged();

	    private System.Nullable<Int64> _IdPerfil;

	    [DataMember(Name = "IdPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Perfil", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_OBJETO_PERMISSAO.TCS_PERFIL.ID_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO_PERMISSAO.TCS_PERFIL.ID_PERFIL")]
	    public System.Nullable<Int64> IdPerfil
	    {
	    	    get
	    	    {
	    	          return _IdPerfil;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdPerfil != value)
	    	          {
	    	              this.ValidateProperty("IdPerfil", value);
	    	              this.OnIdPerfilChanging(value);
	    	              this.RaiseDataMemberChanging("IdPerfil");
	    	              this._IdPerfil = value;
	    	              this.RaiseDataMemberChanged("IdPerfil");
	    	              this.OnIdPerfilChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsObjetoPermissao
	    partial void OnIdTcsObjetoPermissaoChanging(Int32 value);
	    partial void OnIdTcsObjetoPermissaoChanged();

	    private Int32 _IdTcsObjetoPermissao;

	    [DataMember(IsRequired = true, Name = "IdTcsObjetoPermissao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Objeto Permissao", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_OBJETO_PERMISSAO.ID_TCS_OBJETO_PERMISSAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO_PERMISSAO.ID_TCS_OBJETO_PERMISSAO")]
	    public Int32 IdTcsObjetoPermissao
	    {
	    	    get
	    	    {
	    	          return _IdTcsObjetoPermissao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsObjetoPermissao != value)
	    	          {
	    	              this.ValidateProperty("IdTcsObjetoPermissao", value);
	    	              this.OnIdTcsObjetoPermissaoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsObjetoPermissao");
	    	              this._IdTcsObjetoPermissao = value;
	    	              this.RaiseDataMemberChanged("IdTcsObjetoPermissao");
	    	              this.OnIdTcsObjetoPermissaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuario
	    partial void OnIdUsuarioChanging(System.Nullable<Int64> value);
	    partial void OnIdUsuarioChanged();

	    private System.Nullable<Int64> _IdUsuario;

	    [DataMember(Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_OBJETO_PERMISSAO.TCS_USUARIO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO_PERMISSAO.TCS_USUARIO.ID_USUARIO")]
	    public System.Nullable<Int64> IdUsuario
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

	    private Int32 _TemporaryIdTcsObjetoPermissao;
	    [DataMember(Name = "TemporaryIdTcsObjetoPermissao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Objeto Permissao (Tmp)", Description="Temporary Key", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdTcsObjetoPermissao
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTcsObjetoPermissao.IsNullOrEmpty())
	    	                this._TemporaryIdTcsObjetoPermissao = this._IdTcsObjetoPermissao;
	    	          return this._TemporaryIdTcsObjetoPermissao;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTcsObjetoPermissao != value)
	    	              this._TemporaryIdTcsObjetoPermissao = value;
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_OBJETO_PERMISSAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_OBJETO_PERMISSAO), QualifiedEntitySetName = "ControleSistemaContext.TCS_OBJETO_PERMISSAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO_PERMISSAO.ID_OBJETO", Source = "IdObjeto", Target = "ID_OBJETO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_OBJETO_PERMISSAO", RelationPropertyName = "TCS_OBJETO_PERMISSAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO_PERMISSAO.ID_OBJETO_CONTEUDO", Source = "IdObjetoConteudo", Target = "ID_OBJETO_CONTEUDO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_OBJETO_PERMISSAO", RelationPropertyName = "TCS_OBJETO_PERMISSAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO_PERMISSAO.TCS_PERFIL.ID_PERFIL", Source = "IdPerfil", Target = "ID_PERFIL", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL", RelationPropertyName = "TCS_PERFIL" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO_PERMISSAO.TCS_USUARIO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO_PERMISSAO.ID_TCS_OBJETO_PERMISSAO", Source = "IdTcsObjetoPermissao", Target = "ID_TCS_OBJETO_PERMISSAO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_OBJETO_PERMISSAO", RelationPropertyName = "TCS_OBJETO_PERMISSAO" });

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

		

	[LinxPublicationView(PrimaryKeys="TCS_USUARIO.ID_USUARIO", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsUsuario];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdUsuario];ReadOnly[false];Entities[TCS_USUARIO:IdUsuario];SubQueryInfo[];EdmEntityName[TCS_USUARIO];EntityRelations[];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuario")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Objeto.TcsUsuario")]
	public partial class TcsUsuario : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For IdUsuario
	    partial void OnIdUsuarioChanging(Int64 value);
	    partial void OnIdUsuarioChanged();

	    private Int64 _IdUsuario;

	    [DataMember(IsRequired = true, Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.ID_USUARIO")]
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
	    //Extensibility Partial Method Definitions For NomeUsuario
	    partial void OnNomeUsuarioChanging(System.String value);
	    partial void OnNomeUsuarioChanged();

	    private System.String _NomeUsuario;

	    [DataMember(IsRequired = true, Name = "NomeUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Usuario", Description="", Order = 17, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO.NOME_USUARIO")]
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

	    private Int64 _TemporaryIdUsuario;
	    [DataMember(Name = "TemporaryIdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario (Tmp)", Description="Temporary Key", Order = 11, AutoGenerateField = false, GroupName="", ResourceType= null)]
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
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_USUARIO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_USUARIO), QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO.NOME_USUARIO", Source = "NomeUsuario", Target = "NOME_USUARIO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });

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

		

	[LinxPublicationView(PrimaryKeys="LayoutInfo.EntityUniqueKey", IsUpdatable=false, EdmName="")]
		
	[DataContract(IsReference = false, Name = "LayoutInfo")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Objeto.LayoutInfo")]
	public partial class LayoutInfo 
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
	 


	    private Int64 _Id;

	    [DataMember(Name = "Id", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public Int64 Id
	    {
	    	    get
	    	    {
	    	          return _Id;
	    	    }
	    	    set
	    	    {
	    	          this._Id = value;
	    	    }
	    }

	    private string _NomeLayout;

	    [DataMember(Name = "NomeLayout", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string NomeLayout
	    {
	    	    get
	    	    {
	    	          return _NomeLayout;
	    	    }
	    	    set
	    	    {
	    	          this._NomeLayout = value;
	    	    }
	    }

	    private string _Modulo;

	    [DataMember(Name = "Modulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Modulo
	    {
	    	    get
	    	    {
	    	          return _Modulo;
	    	    }
	    	    set
	    	    {
	    	          this._Modulo = value;
	    	    }
	    }

	    private string _NomeObjeto;

	    [DataMember(Name = "NomeObjeto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string NomeObjeto
	    {
	    	    get
	    	    {
	    	          return _NomeObjeto;
	    	    }
	    	    set
	    	    {
	    	          this._NomeObjeto = value;
	    	    }
	    }

	    private string _ConteudoJson;

	    [DataMember(Name = "ConteudoJson", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ConteudoJson
	    {
	    	    get
	    	    {
	    	          return _ConteudoJson;
	    	    }
	    	    set
	    	    {
	    	          this._ConteudoJson = value;
	    	    }
	    }

	    private string _PermissaoUsuario;

	    [DataMember(Name = "PermissaoUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string PermissaoUsuario
	    {
	    	    get
	    	    {
	    	          return _PermissaoUsuario;
	    	    }
	    	    set
	    	    {
	    	          this._PermissaoUsuario = value;
	    	    }
	    }

	    private string _PermissaoPerfil;

	    [DataMember(Name = "PermissaoPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string PermissaoPerfil
	    {
	    	    get
	    	    {
	    	          return _PermissaoPerfil;
	    	    }
	    	    set
	    	    {
	    	          this._PermissaoPerfil = value;
	    	    }
	    }

	    private bool _LayoutPadrao;

	    [DataMember(Name = "LayoutPadrao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public bool LayoutPadrao
	    {
	    	    get
	    	    {
	    	          return _LayoutPadrao;
	    	    }
	    	    set
	    	    {
	    	          this._LayoutPadrao = value;
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
	[DomainIdentifier("ProcessorOverviewObjetoDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class ObjetoDomainService : DomainService, IDataServiceContext 
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

		
	    public ObjetoDomainService() : this("", null, null) { }
	    public ObjetoDomainService(string connectionString) : this(connectionString, null, null) { }
	    public ObjetoDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public ObjetoDomainService(Linx.Framework.ControleSistema.BM.ControleSistemaContext dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public ObjetoDomainService(string connectionString, Linx.Framework.ControleSistema.BM.ControleSistemaContext dataContext, Dictionary<string, string> headers) : base() 
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
	
	    
	        foreach (ChangeSetEntry entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsObjeto))
	        {
	            ((TcsObjeto)entry.Entity).OnSavingChanges(this, changeSet.GetChangeOperation(entry.Entity));
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
	
	
	        TcsObjeto.OnSavedContextChanges(this, changeSet.ChangeSetEntries.Where(e => e.Entity is TcsObjeto).ToArray());
    	
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
 	        var _TcsObjetoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsObjeto && e.Entity.GetType().Name == "TcsObjeto" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _TcsObjetoElements)
 	           if (((TcsObjeto)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 
 	        //Remove inconsistent details
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsTransacao && e.Entity.GetType().Name == "TcsTransacao" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
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
	
		

	        if (entityName.InList("Linx.Framework.BV.Objeto.TcsObjeto"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsObjeto",
	        			NameSpace = "Linx.Framework.BV.Objeto",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsObjeto",
	        			ClearMethodName = "ClearTcsObjeto",
	        			QueryMethodName  = "GetPagedTcsObjeto",	
	        			CountingMethodName  = "GetTcsObjeto" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Objeto.TcsObjeto"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Objeto.TcsObjeto"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Objeto.TcsObjeto", "Linx.Framework.BV.Objeto.TcsTransacao"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsTransacao",
	        			NameSpace = "Linx.Framework.BV.Objeto",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsObjeto",	
	        			DisplayName = "Transação",
	        			ClearMethodName = "ClearTcsTransacao",
	        			QueryMethodName  = "GetPagedTcsTransacao",	
	        			CountingMethodName  = "GetTcsTransacao" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Objeto.TcsTransacao"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Objeto.TcsTransacao"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Objeto.TcsObjetoConteudoMnt"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsObjetoConteudoMnt",
	        			NameSpace = "Linx.Framework.BV.Objeto",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsObjetoConteudoMnt",
	        			ClearMethodName = "ClearTcsObjetoConteudoMnt",
	        			QueryMethodName  = "GetPagedTcsObjetoConteudoMnt",	
	        			CountingMethodName  = "GetTcsObjetoConteudoMnt" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Objeto.TcsObjetoConteudoMnt"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Objeto.TcsObjetoConteudoMnt"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Objeto.ConfiguracaoExportacao"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "ConfiguracaoExportacao",
	        			NameSpace = "Linx.Framework.BV.Objeto",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "ConfiguracaoExportacao",
	        			ClearMethodName = "ClearConfiguracaoExportacao",
	        			QueryMethodName  = "GetPagedConfiguracaoExportacao",	
	        			CountingMethodName  = "GetConfiguracaoExportacao" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Objeto.ConfiguracaoExportacao"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Objeto.ConfiguracaoExportacao"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Objeto.TcsObjetoPermissao"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsObjetoPermissao",
	        			NameSpace = "Linx.Framework.BV.Objeto",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsObjetoPermissao",
	        			ClearMethodName = "ClearTcsObjetoPermissao",
	        			QueryMethodName  = "GetPagedTcsObjetoPermissao",	
	        			CountingMethodName  = "GetTcsObjetoPermissao" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Objeto.TcsObjetoPermissao"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Objeto.TcsObjetoPermissao"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Objeto.TcsUsuario"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsUsuario",
	        			NameSpace = "Linx.Framework.BV.Objeto",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsUsuario",
	        			ClearMethodName = "ClearTcsUsuario",
	        			QueryMethodName  = "GetPagedTcsUsuario",	
	        			CountingMethodName  = "GetTcsUsuario" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Objeto.TcsUsuario"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Objeto.TcsUsuario"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Objeto.LayoutInfo"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "LayoutInfo",
	        			NameSpace = "Linx.Framework.BV.Objeto",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "LayoutInfo",
	        			ClearMethodName = "ClearLayoutInfo",
	        			QueryMethodName  = "GetPagedLayoutInfo",	
	        			CountingMethodName  = "GetLayoutInfo" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Objeto.LayoutInfo"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Objeto.LayoutInfo"), forceAll: forceAll)
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

         		    return new string[] { "Framework_ObjetoClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.ObjetoClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Framework_objetoService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.objetoService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear TcsObjeto.
	    public IEnumerable<TcsObjeto> ClearTcsObjeto()
	    {
	        List<TcsObjeto> result = new List<TcsObjeto>();
	        result.Add(new TcsObjeto());	
			
	        result[0].TcsTransacaoList = new List<TcsTransacao>();
	        ((List<TcsTransacao>)result[0].TcsTransacaoList).Add(new TcsTransacao());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsTransacao.
	    public IEnumerable<TcsTransacao> ClearTcsTransacao()
	    {
	        List<TcsTransacao> result = new List<TcsTransacao>();
	        result.Add(new TcsTransacao());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsObjetoConteudoMnt.
	    public IEnumerable<TcsObjetoConteudoMnt> ClearTcsObjetoConteudoMnt()
	    {
	        List<TcsObjetoConteudoMnt> result = new List<TcsObjetoConteudoMnt>();
	        result.Add(new TcsObjetoConteudoMnt());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear ConfiguracaoExportacao.
	    public IEnumerable<ConfiguracaoExportacao> ClearConfiguracaoExportacao()
	    {
	        List<ConfiguracaoExportacao> result = new List<ConfiguracaoExportacao>();
	        result.Add(new ConfiguracaoExportacao());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsObjetoPermissao.
	    public IEnumerable<TcsObjetoPermissao> ClearTcsObjetoPermissao()
	    {
	        List<TcsObjetoPermissao> result = new List<TcsObjetoPermissao>();
	        result.Add(new TcsObjetoPermissao());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsUsuario.
	    public IEnumerable<TcsUsuario> ClearTcsUsuario()
	    {
	        List<TcsUsuario> result = new List<TcsUsuario>();
	        result.Add(new TcsUsuario());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear LayoutInfo.
	    public IEnumerable<LayoutInfo> ClearLayoutInfo()
	    {
	        List<LayoutInfo> result = new List<LayoutInfo>();
	        result.Add(new LayoutInfo());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsObjeto.
	    public IQueryable<TcsObjeto> GetTcsObjeto()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsObjeto> result = 
	            (from entity0 in this.DbContext.TCS_OBJETO
	            
	            	
	            select new TcsObjeto()		
	            {
	            
                ClasseNome = entity0.CLASSE_NOME
                , DescObjeto = entity0.DESC_OBJETO
                , IdObjeto = entity0.ID_OBJETO
                , LxTipoObjeto = entity0.LX_TIPO_OBJETO
                , LxTipoObjetoName = ((entity0.LX_TIPO_OBJETO) == 1 ? "BO" : ((entity0.LX_TIPO_OBJETO) == 3 ? "Campo" : ((entity0.LX_TIPO_OBJETO) == 10 ? "Filtro" : ((entity0.LX_TIPO_OBJETO) == 9 ? "Layout" : ((entity0.LX_TIPO_OBJETO) == 6 ? "Relatório" : ((entity0.LX_TIPO_OBJETO) == 5 ? "Stored Procedure" : ((entity0.LX_TIPO_OBJETO) == 8 ? "Template de ação de Workflow" : ((entity0.LX_TIPO_OBJETO) == 2 ? "Transação" : ((entity0.LX_TIPO_OBJETO) == 4 ? "Trigger" : ((entity0.LX_TIPO_OBJETO) == 11 ? "Extensão (Objeto de entrada)" : ((entity0.LX_TIPO_OBJETO) == 7 ? "Workflow" : "")))))))))))
                , ObjetoLinx = false
                , PathObjeto = entity0.PATH_OBJETO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsTransacao.
	    public IQueryable<TcsTransacao> GetTcsTransacao()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsTransacao> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO
	            
	            	
	            select new TcsTransacao()		
	            {
	            
                ClasseNome = entity0.CLASSE_NOME
                , CodTransacao = entity0.COD_TRANSACAO
                , DescTransacao = entity0.DESC_TRANSACAO
                , IdObjeto = entity0.ID_OBJETO
                , IdTransacao = entity0.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , LxTipoTransacao = entity0.LX_TIPO_TRANSACAO
                , LxTipoTransacaoName = ((entity0.LX_TIPO_TRANSACAO) == 7 ? "Assistente" : ((entity0.LX_TIPO_TRANSACAO) == 8 ? "Dashboard" : ((entity0.LX_TIPO_TRANSACAO) == 2 ? "ERP" : ((entity0.LX_TIPO_TRANSACAO) == 6 ? "ERP App" : ((entity0.LX_TIPO_TRANSACAO) == 4 ? "Excel" : ((entity0.LX_TIPO_TRANSACAO) == 3 ? "Loja" : ((entity0.LX_TIPO_TRANSACAO) == 5 ? "Mobile" : ((entity0.LX_TIPO_TRANSACAO) == 1 ? "Todos" : ""))))))))
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsObjetoNoAssociations.
	    public IQueryable<TcsObjeto> GetTcsObjetoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsObjeto> result = 
	            (from entity0 in this.DbContext.TCS_OBJETO
	            
	            	
	            select new TcsObjeto()		
	            {
	            
                ClasseNome = entity0.CLASSE_NOME
                , DescObjeto = entity0.DESC_OBJETO
                , IdObjeto = entity0.ID_OBJETO
                , LxTipoObjeto = entity0.LX_TIPO_OBJETO
                , LxTipoObjetoName = ((entity0.LX_TIPO_OBJETO) == 1 ? "BO" : ((entity0.LX_TIPO_OBJETO) == 3 ? "Campo" : ((entity0.LX_TIPO_OBJETO) == 10 ? "Filtro" : ((entity0.LX_TIPO_OBJETO) == 9 ? "Layout" : ((entity0.LX_TIPO_OBJETO) == 6 ? "Relatório" : ((entity0.LX_TIPO_OBJETO) == 5 ? "Stored Procedure" : ((entity0.LX_TIPO_OBJETO) == 8 ? "Template de ação de Workflow" : ((entity0.LX_TIPO_OBJETO) == 2 ? "Transação" : ((entity0.LX_TIPO_OBJETO) == 4 ? "Trigger" : ((entity0.LX_TIPO_OBJETO) == 11 ? "Extensão (Objeto de entrada)" : ((entity0.LX_TIPO_OBJETO) == 7 ? "Workflow" : "")))))))))))
                , ObjetoLinx = false
                , PathObjeto = entity0.PATH_OBJETO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsTransacaoNoAssociations.
	    public IQueryable<TcsTransacao> GetTcsTransacaoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsTransacao> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO
	            
	            	
	            select new TcsTransacao()		
	            {
	            
                ClasseNome = entity0.CLASSE_NOME
                , CodTransacao = entity0.COD_TRANSACAO
                , DescTransacao = entity0.DESC_TRANSACAO
                , IdObjeto = entity0.ID_OBJETO
                , IdTransacao = entity0.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , LxTipoTransacao = entity0.LX_TIPO_TRANSACAO
                , LxTipoTransacaoName = ((entity0.LX_TIPO_TRANSACAO) == 7 ? "Assistente" : ((entity0.LX_TIPO_TRANSACAO) == 8 ? "Dashboard" : ((entity0.LX_TIPO_TRANSACAO) == 2 ? "ERP" : ((entity0.LX_TIPO_TRANSACAO) == 6 ? "ERP App" : ((entity0.LX_TIPO_TRANSACAO) == 4 ? "Excel" : ((entity0.LX_TIPO_TRANSACAO) == 3 ? "Loja" : ((entity0.LX_TIPO_TRANSACAO) == 5 ? "Mobile" : ((entity0.LX_TIPO_TRANSACAO) == 1 ? "Todos" : ""))))))))
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsObjetoConteudoMnt.
	    public IQueryable<TcsObjetoConteudoMnt> GetTcsObjetoConteudoMnt()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsObjetoConteudoMnt> result = 
	            (from entity0 in this.DbContext.TCS_OBJETO_CONTEUDO
                orderby entity0.CONTEUDO_XML ascending
	            
	            	
	            select new TcsObjetoConteudoMnt()		
	            {
	            
                ConteudoXml = entity0.CONTEUDO_XML
                , IdObjeto = entity0.ID_OBJETO
                , IdObjetoConteudo = entity0.ID_OBJETO_CONTEUDO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsObjetoConteudoMntNoAssociations.
	    public IQueryable<TcsObjetoConteudoMnt> GetTcsObjetoConteudoMntNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsObjetoConteudoMnt> result = 
	            (from entity0 in this.DbContext.TCS_OBJETO_CONTEUDO
                orderby entity0.CONTEUDO_XML ascending
	            
	            	
	            select new TcsObjetoConteudoMnt()		
	            {
	            
                ConteudoXml = entity0.CONTEUDO_XML
                , IdObjeto = entity0.ID_OBJETO
                , IdObjetoConteudo = entity0.ID_OBJETO_CONTEUDO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get ConfiguracaoExportacao.
	    public IEnumerable<ConfiguracaoExportacao> GetConfiguracaoExportacao()
	    {




	
	        IEnumerable<ConfiguracaoExportacao> result = new List<ConfiguracaoExportacao>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get ConfiguracaoExportacaoNoAssociations.
	    public IEnumerable<ConfiguracaoExportacao> GetConfiguracaoExportacaoNoAssociations()
	    {




	
	        IEnumerable<ConfiguracaoExportacao> result = new List<ConfiguracaoExportacao>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsObjetoPermissao.
	    public IQueryable<TcsObjetoPermissao> GetTcsObjetoPermissao()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsObjetoPermissao> result = 
	            (from entity0 in this.DbContext.TCS_OBJETO_PERMISSAO
                  let entity0Al1 = entity0.TCS_PERFIL
                  let entity0Al2 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsObjetoPermissao()		
	            {
	            
                IdObjeto = entity0.ID_OBJETO
                , IdObjetoConteudo = entity0.ID_OBJETO_CONTEUDO
                , IdPerfil = entity0Al1.ID_PERFIL
                , IdTcsObjetoPermissao = entity0.ID_TCS_OBJETO_PERMISSAO
                , IdUsuario = entity0Al2.ID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsObjetoPermissaoNoAssociations.
	    public IQueryable<TcsObjetoPermissao> GetTcsObjetoPermissaoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsObjetoPermissao> result = 
	            (from entity0 in this.DbContext.TCS_OBJETO_PERMISSAO
                  let entity0Al1 = entity0.TCS_PERFIL
                  let entity0Al2 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsObjetoPermissao()		
	            {
	            
                IdObjeto = entity0.ID_OBJETO
                , IdObjetoConteudo = entity0.ID_OBJETO_CONTEUDO
                , IdPerfil = entity0Al1.ID_PERFIL
                , IdTcsObjetoPermissao = entity0.ID_TCS_OBJETO_PERMISSAO
                , IdUsuario = entity0Al2.ID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsUsuario.
	    public IQueryable<TcsUsuario> GetTcsUsuario()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuario> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO
	            
	            	
	            select new TcsUsuario()		
	            {
	            
                IdUsuario = entity0.ID_USUARIO
                , NomeUsuario = entity0.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioNoAssociations.
	    public IQueryable<TcsUsuario> GetTcsUsuarioNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuario> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO
	            
	            	
	            select new TcsUsuario()		
	            {
	            
                IdUsuario = entity0.ID_USUARIO
                , NomeUsuario = entity0.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get LayoutInfo.
	    public IEnumerable<LayoutInfo> GetLayoutInfo()
	    {




	
	        IEnumerable<LayoutInfo> result = new List<LayoutInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get LayoutInfoNoAssociations.
	    public IEnumerable<LayoutInfo> GetLayoutInfoNoAssociations()
	    {




	
	        IEnumerable<LayoutInfo> result = new List<LayoutInfo>();
	  	
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	result.Add("TcsObjeto|ObjetoLinx");
	    	result.Add("TcsObjeto|false");
	    	//Add filtering disabled property for TCS_OBJETO
	    	string[] bmDisabledTcsObjetoList = this.GetEDM().GetFilteringDisabledList("TCS_OBJETO");
	    	if (bmDisabledTcsObjetoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsObjetoList.Contains("TCS_OBJETO.CLASSE_NOME"))
	    		{
	    			result.Add("TcsObjeto|ClasseNome");
	    			result.Add("TcsObjeto|TCS_OBJETO.CLASSE_NOME");
	    		}
	
	    		if (bmDisabledTcsObjetoList.Contains("TCS_OBJETO.DESC_OBJETO"))
	    		{
	    			result.Add("TcsObjeto|DescObjeto");
	    			result.Add("TcsObjeto|TCS_OBJETO.DESC_OBJETO");
	    		}
	
	    		if (bmDisabledTcsObjetoList.Contains("TCS_OBJETO.ID_OBJETO"))
	    		{
	    			result.Add("TcsObjeto|IdObjeto");
	    			result.Add("TcsObjeto|TCS_OBJETO.ID_OBJETO");
	    		}
	
	    		if (bmDisabledTcsObjetoList.Contains("TCS_OBJETO.LX_TIPO_OBJETO"))
	    		{
	    			result.Add("TcsObjeto|LxTipoObjeto");
	    			result.Add("TcsObjeto|TCS_OBJETO.LX_TIPO_OBJETO");
	    		}
	
	    		if (bmDisabledTcsObjetoList.Contains("TCS_OBJETO.PATH_OBJETO"))
	    		{
	    			result.Add("TcsObjeto|PathObjeto");
	    			result.Add("TcsObjeto|TCS_OBJETO.PATH_OBJETO");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_OBJETO_CONTEUDO
	    	string[] bmDisabledTcsObjetoConteudoMntList = this.GetEDM().GetFilteringDisabledList("TCS_OBJETO_CONTEUDO");
	    	if (bmDisabledTcsObjetoConteudoMntList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsObjetoConteudoMntList.Contains("TCS_OBJETO_CONTEUDO.CONTEUDO_XML"))
	    		{
	    			result.Add("TcsObjetoConteudoMnt|ConteudoXml");
	    			result.Add("TcsObjetoConteudoMnt|TCS_OBJETO_CONTEUDO.CONTEUDO_XML");
	    		}
	
	    		if (bmDisabledTcsObjetoConteudoMntList.Contains("TCS_OBJETO_CONTEUDO.ID_OBJETO"))
	    		{
	    			result.Add("TcsObjetoConteudoMnt|IdObjeto");
	    			result.Add("TcsObjetoConteudoMnt|TCS_OBJETO_CONTEUDO.ID_OBJETO");
	    		}
	
	    		if (bmDisabledTcsObjetoConteudoMntList.Contains("TCS_OBJETO_CONTEUDO.ID_OBJETO_CONTEUDO"))
	    		{
	    			result.Add("TcsObjetoConteudoMnt|IdObjetoConteudo");
	    			result.Add("TcsObjetoConteudoMnt|TCS_OBJETO_CONTEUDO.ID_OBJETO_CONTEUDO");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_TRANSACAO
	    	string[] bmDisabledTcsTransacaoList = this.GetEDM().GetFilteringDisabledList("TCS_TRANSACAO");
	    	if (bmDisabledTcsTransacaoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsTransacaoList.Contains("TCS_TRANSACAO.CLASSE_NOME"))
	    		{
	    			result.Add("TcsTransacao|ClasseNome");
	    			result.Add("TcsTransacao|TCS_TRANSACAO.CLASSE_NOME");
	    		}
	
	    		if (bmDisabledTcsTransacaoList.Contains("TCS_TRANSACAO.COD_TRANSACAO"))
	    		{
	    			result.Add("TcsTransacao|CodTransacao");
	    			result.Add("TcsTransacao|TCS_TRANSACAO.COD_TRANSACAO");
	    		}
	
	    		if (bmDisabledTcsTransacaoList.Contains("TCS_TRANSACAO.DESC_TRANSACAO"))
	    		{
	    			result.Add("TcsTransacao|DescTransacao");
	    			result.Add("TcsTransacao|TCS_TRANSACAO.DESC_TRANSACAO");
	    		}
	
	    		if (bmDisabledTcsTransacaoList.Contains("TCS_TRANSACAO.ID_OBJETO"))
	    		{
	    			result.Add("TcsTransacao|IdObjeto");
	    			result.Add("TcsTransacao|TCS_TRANSACAO.ID_OBJETO");
	    		}
	
	    		if (bmDisabledTcsTransacaoList.Contains("TCS_TRANSACAO.ID_TRANSACAO"))
	    		{
	    			result.Add("TcsTransacao|IdTransacao");
	    			result.Add("TcsTransacao|TCS_TRANSACAO.ID_TRANSACAO");
	    		}
	
	    		if (bmDisabledTcsTransacaoList.Contains("TCS_TRANSACAO.INATIVO"))
	    		{
	    			result.Add("TcsTransacao|Inativo");
	    			result.Add("TcsTransacao|TCS_TRANSACAO.INATIVO");
	    		}
	
	    		if (bmDisabledTcsTransacaoList.Contains("TCS_TRANSACAO.LX_TIPO_TRANSACAO"))
	    		{
	    			result.Add("TcsTransacao|LxTipoTransacao");
	    			result.Add("TcsTransacao|TCS_TRANSACAO.LX_TIPO_TRANSACAO");
	    		}
	    	}
	    	result.Add("ConfiguracaoExportacao|IsExcelDataSource");
	    	result.Add("ConfiguracaoExportacao|true");
	    	//Add filtering disabled property for TCS_OBJETO_PERMISSAO
	    	string[] bmDisabledTcsObjetoPermissaoList = this.GetEDM().GetFilteringDisabledList("TCS_OBJETO_PERMISSAO");
	    	if (bmDisabledTcsObjetoPermissaoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsObjetoPermissaoList.Contains("TCS_OBJETO_PERMISSAO.ID_OBJETO"))
	    		{
	    			result.Add("TcsObjetoPermissao|IdObjeto");
	    			result.Add("TcsObjetoPermissao|TCS_OBJETO_PERMISSAO.ID_OBJETO");
	    		}
	
	    		if (bmDisabledTcsObjetoPermissaoList.Contains("TCS_OBJETO_PERMISSAO.ID_OBJETO_CONTEUDO"))
	    		{
	    			result.Add("TcsObjetoPermissao|IdObjetoConteudo");
	    			result.Add("TcsObjetoPermissao|TCS_OBJETO_PERMISSAO.ID_OBJETO_CONTEUDO");
	    		}
	
	    		if (bmDisabledTcsObjetoPermissaoList.Contains("TCS_OBJETO_PERMISSAO.ID_TCS_OBJETO_PERMISSAO"))
	    		{
	    			result.Add("TcsObjetoPermissao|IdTcsObjetoPermissao");
	    			result.Add("TcsObjetoPermissao|TCS_OBJETO_PERMISSAO.ID_TCS_OBJETO_PERMISSAO");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_USUARIO
	    	string[] bmDisabledTcsUsuarioList = this.GetEDM().GetFilteringDisabledList("TCS_USUARIO");
	    	if (bmDisabledTcsUsuarioList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO.ID_USUARIO"))
	    		{
	    			result.Add("TcsUsuario|IdUsuario");
	    			result.Add("TcsUsuario|TCS_USUARIO.ID_USUARIO");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO.NOME_USUARIO"))
	    		{
	    			result.Add("TcsUsuario|NomeUsuario");
	    			result.Add("TcsUsuario|TCS_USUARIO.NOME_USUARIO");
	    		}
	    	}
	    	return result;
	    }

	    #endregion Excluded Filters

	    #region Get By EntitySearchId Methods Definitions.
	
				
	    [Ignore]
	    //Get TcsObjeto By EntitySearchId.
	    public IQueryable<TcsObjeto> GetTcsObjetoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsObjetoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsTransacao By EntitySearchId.
	    public IQueryable<TcsTransacao> GetTcsTransacaoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsTransacaoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsObjeto By EntitySearchId.
	    public IQueryable<TcsObjeto> GetTcsObjetoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsObjetoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsTransacao By EntitySearchId.
	    public IQueryable<TcsTransacao> GetTcsTransacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsTransacaoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsObjetoConteudoMnt By EntitySearchId.
	    public IQueryable<TcsObjetoConteudoMnt> GetTcsObjetoConteudoMntByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsObjetoConteudoMntByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsObjetoConteudoMnt By EntitySearchId.
	    public IQueryable<TcsObjetoConteudoMnt> GetTcsObjetoConteudoMntByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsObjetoConteudoMntByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get ConfiguracaoExportacao By EntitySearchId.
	    public IEnumerable<ConfiguracaoExportacao> GetConfiguracaoExportacaoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetConfiguracaoExportacaoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get ConfiguracaoExportacao By EntitySearchId.
	    public IEnumerable<ConfiguracaoExportacao> GetConfiguracaoExportacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetConfiguracaoExportacaoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsObjetoPermissao By EntitySearchId.
	    public IQueryable<TcsObjetoPermissao> GetTcsObjetoPermissaoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsObjetoPermissaoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsObjetoPermissao By EntitySearchId.
	    public IQueryable<TcsObjetoPermissao> GetTcsObjetoPermissaoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsObjetoPermissaoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuario By EntitySearchId.
	    public IQueryable<TcsUsuario> GetTcsUsuarioByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuario By EntitySearchId.
	    public IQueryable<TcsUsuario> GetTcsUsuarioByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get LayoutInfo By EntitySearchId.
	    public IEnumerable<LayoutInfo> GetLayoutInfoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetLayoutInfoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get LayoutInfo By EntitySearchId.
	    public IEnumerable<LayoutInfo> GetLayoutInfoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetLayoutInfoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get TcsObjeto By Example.
	    [Ignore]
	    public IQueryable<TcsObjeto> GetTcsObjetoByExample(TcsObjeto entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsObjetoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsTransacao By Example.
	    [Ignore]
	    public IQueryable<TcsTransacao> GetTcsTransacaoByExample(TcsTransacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsTransacaoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsObjeto By Example.
	    [Ignore]
	    public IQueryable<TcsObjeto> GetTcsObjetoByExampleNoAssociations(TcsObjeto entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsObjetoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsTransacao By Example.
	    [Ignore]
	    public IQueryable<TcsTransacao> GetTcsTransacaoByExampleNoAssociations(TcsTransacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsTransacaoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsObjetoConteudoMnt By Example.
	    [Ignore]
	    public IQueryable<TcsObjetoConteudoMnt> GetTcsObjetoConteudoMntByExample(TcsObjetoConteudoMnt entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsObjetoConteudoMntByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsObjetoConteudoMnt By Example.
	    [Ignore]
	    public IQueryable<TcsObjetoConteudoMnt> GetTcsObjetoConteudoMntByExampleNoAssociations(TcsObjetoConteudoMnt entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsObjetoConteudoMntByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get ConfiguracaoExportacao By Example.
	    [Ignore]
	    public IEnumerable<ConfiguracaoExportacao> GetConfiguracaoExportacaoByExample(ConfiguracaoExportacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetConfiguracaoExportacaoByEntitySearch(queryAnalysis);
	    }
			
	    //Get ConfiguracaoExportacao By Example.
	    [Ignore]
	    public IEnumerable<ConfiguracaoExportacao> GetConfiguracaoExportacaoByExampleNoAssociations(ConfiguracaoExportacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetConfiguracaoExportacaoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsObjetoPermissao By Example.
	    [Ignore]
	    public IQueryable<TcsObjetoPermissao> GetTcsObjetoPermissaoByExample(TcsObjetoPermissao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsObjetoPermissaoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsObjetoPermissao By Example.
	    [Ignore]
	    public IQueryable<TcsObjetoPermissao> GetTcsObjetoPermissaoByExampleNoAssociations(TcsObjetoPermissao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsObjetoPermissaoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsUsuario By Example.
	    [Ignore]
	    public IQueryable<TcsUsuario> GetTcsUsuarioByExample(TcsUsuario entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsUsuario By Example.
	    [Ignore]
	    public IQueryable<TcsUsuario> GetTcsUsuarioByExampleNoAssociations(TcsUsuario entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get LayoutInfo By Example.
	    [Ignore]
	    public IEnumerable<LayoutInfo> GetLayoutInfoByExample(LayoutInfo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetLayoutInfoByEntitySearch(queryAnalysis);
	    }
			
	    //Get LayoutInfo By Example.
	    [Ignore]
	    public IEnumerable<LayoutInfo> GetLayoutInfoByExampleNoAssociations(LayoutInfo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetLayoutInfoByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public TcsObjeto GetTcsObjetoByKey(Int64 idObjeto)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsObjeto");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdObjeto"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idObjeto));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsObjetoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsObjetoConteudoMnt GetTcsObjetoConteudoMntByKey(Int64 idObjetoConteudo)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsObjetoConteudoMnt");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdObjetoConteudo"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idObjetoConteudo));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsObjetoConteudoMntByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsTransacao GetTcsTransacaoByKey(Int64 idTransacao)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsTransacao");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTransacao"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTransacao));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsTransacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public ConfiguracaoExportacao GetConfiguracaoExportacaoByKey(Int64 id)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("ConfiguracaoExportacao");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "Id"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, id));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetConfiguracaoExportacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsObjetoPermissao GetTcsObjetoPermissaoByKey(Int32 idTcsObjetoPermissao)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsObjetoPermissao");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsObjetoPermissao"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsObjetoPermissao));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsObjetoPermissaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsUsuario GetTcsUsuarioByKey(Int64 idUsuario)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsUsuario");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuario"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idUsuario));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsUsuarioByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public LayoutInfo GetLayoutInfoByKey(Int64 id)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("LayoutInfo");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "Id"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, id));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetLayoutInfoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get TcsObjetoByEntitySearch.
	    public IQueryable<TcsObjeto> GetTcsObjetoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsObjeto));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsObjeto> result = 
	            (from entity0 in this.DbContext.TCS_OBJETO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsObjeto()		
	            {
	            
                ClasseNome = entity0.CLASSE_NOME
                , DescObjeto = entity0.DESC_OBJETO
                , IdObjeto = entity0.ID_OBJETO
                , LxTipoObjeto = entity0.LX_TIPO_OBJETO
                , LxTipoObjetoName = ((entity0.LX_TIPO_OBJETO) == 1 ? "BO" : ((entity0.LX_TIPO_OBJETO) == 3 ? "Campo" : ((entity0.LX_TIPO_OBJETO) == 10 ? "Filtro" : ((entity0.LX_TIPO_OBJETO) == 9 ? "Layout" : ((entity0.LX_TIPO_OBJETO) == 6 ? "Relatório" : ((entity0.LX_TIPO_OBJETO) == 5 ? "Stored Procedure" : ((entity0.LX_TIPO_OBJETO) == 8 ? "Template de ação de Workflow" : ((entity0.LX_TIPO_OBJETO) == 2 ? "Transação" : ((entity0.LX_TIPO_OBJETO) == 4 ? "Trigger" : ((entity0.LX_TIPO_OBJETO) == 11 ? "Extensão (Objeto de entrada)" : ((entity0.LX_TIPO_OBJETO) == 7 ? "Workflow" : "")))))))))))
                , ObjetoLinx = false
                , PathObjeto = entity0.PATH_OBJETO
		
	            }
	            );
	
	        SetTcsObjetoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsTransacaoByEntitySearch.
	    public IQueryable<TcsTransacao> GetTcsTransacaoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsTransacao> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsTransacao()		
	            {
	            
                ClasseNome = entity0.CLASSE_NOME
                , CodTransacao = entity0.COD_TRANSACAO
                , DescTransacao = entity0.DESC_TRANSACAO
                , IdObjeto = entity0.ID_OBJETO
                , IdTransacao = entity0.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , LxTipoTransacao = entity0.LX_TIPO_TRANSACAO
                , LxTipoTransacaoName = ((entity0.LX_TIPO_TRANSACAO) == 7 ? "Assistente" : ((entity0.LX_TIPO_TRANSACAO) == 8 ? "Dashboard" : ((entity0.LX_TIPO_TRANSACAO) == 2 ? "ERP" : ((entity0.LX_TIPO_TRANSACAO) == 6 ? "ERP App" : ((entity0.LX_TIPO_TRANSACAO) == 4 ? "Excel" : ((entity0.LX_TIPO_TRANSACAO) == 3 ? "Loja" : ((entity0.LX_TIPO_TRANSACAO) == 5 ? "Mobile" : ((entity0.LX_TIPO_TRANSACAO) == 1 ? "Todos" : ""))))))))
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsObjetoByEntitySearchNoAssociations.
	    public IQueryable<TcsObjeto> GetTcsObjetoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsObjeto));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsObjeto> result = 
	            (from entity0 in this.DbContext.TCS_OBJETO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsObjeto()		
	            {
	            
                ClasseNome = entity0.CLASSE_NOME
                , DescObjeto = entity0.DESC_OBJETO
                , IdObjeto = entity0.ID_OBJETO
                , LxTipoObjeto = entity0.LX_TIPO_OBJETO
                , LxTipoObjetoName = ((entity0.LX_TIPO_OBJETO) == 1 ? "BO" : ((entity0.LX_TIPO_OBJETO) == 3 ? "Campo" : ((entity0.LX_TIPO_OBJETO) == 10 ? "Filtro" : ((entity0.LX_TIPO_OBJETO) == 9 ? "Layout" : ((entity0.LX_TIPO_OBJETO) == 6 ? "Relatório" : ((entity0.LX_TIPO_OBJETO) == 5 ? "Stored Procedure" : ((entity0.LX_TIPO_OBJETO) == 8 ? "Template de ação de Workflow" : ((entity0.LX_TIPO_OBJETO) == 2 ? "Transação" : ((entity0.LX_TIPO_OBJETO) == 4 ? "Trigger" : ((entity0.LX_TIPO_OBJETO) == 11 ? "Extensão (Objeto de entrada)" : ((entity0.LX_TIPO_OBJETO) == 7 ? "Workflow" : "")))))))))))
                , ObjetoLinx = false
                , PathObjeto = entity0.PATH_OBJETO
		
	            }
	            );
	
	        SetTcsObjetoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsTransacaoByEntitySearchNoAssociations.
	    public IQueryable<TcsTransacao> GetTcsTransacaoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsTransacao> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsTransacao()		
	            {
	            
                ClasseNome = entity0.CLASSE_NOME
                , CodTransacao = entity0.COD_TRANSACAO
                , DescTransacao = entity0.DESC_TRANSACAO
                , IdObjeto = entity0.ID_OBJETO
                , IdTransacao = entity0.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , LxTipoTransacao = entity0.LX_TIPO_TRANSACAO
                , LxTipoTransacaoName = ((entity0.LX_TIPO_TRANSACAO) == 7 ? "Assistente" : ((entity0.LX_TIPO_TRANSACAO) == 8 ? "Dashboard" : ((entity0.LX_TIPO_TRANSACAO) == 2 ? "ERP" : ((entity0.LX_TIPO_TRANSACAO) == 6 ? "ERP App" : ((entity0.LX_TIPO_TRANSACAO) == 4 ? "Excel" : ((entity0.LX_TIPO_TRANSACAO) == 3 ? "Loja" : ((entity0.LX_TIPO_TRANSACAO) == 5 ? "Mobile" : ((entity0.LX_TIPO_TRANSACAO) == 1 ? "Todos" : ""))))))))
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
	
	    [Ignore()]
	    private void SetTcsObjetoBusinessFilter(ref IQueryable<TcsObjeto> query, List<EntitySearch> entitySearchList)
	    {
	    		int idxElement;
	    		string operatorValue;
	    		object value;
	    		//Get query by functions
	    		if (entitySearchList.Count > 0)
	    		{
	    			foreach (EntitySearch search in entitySearchList.Where(e => e.EntityName == "TcsObjeto"))
	    			{

	
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "ObjetoLinx" || e.Value.ToString() == "false")))
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


		
	
	    
	    [Ignore]
	    //Get TcsObjetoConteudoMntByEntitySearch.
	    public IQueryable<TcsObjetoConteudoMnt> GetTcsObjetoConteudoMntByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsObjetoConteudoMnt));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsObjetoConteudoMnt> result = 
	            (from entity0 in this.DbContext.TCS_OBJETO_CONTEUDO.Where(dynQuery, parameters.ToArray())
                orderby entity0.CONTEUDO_XML ascending
	            
	            	
	            select new TcsObjetoConteudoMnt()		
	            {
	            
                ConteudoXml = entity0.CONTEUDO_XML
                , IdObjeto = entity0.ID_OBJETO
                , IdObjetoConteudo = entity0.ID_OBJETO_CONTEUDO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsObjetoConteudoMntByEntitySearchNoAssociations.
	    public IQueryable<TcsObjetoConteudoMnt> GetTcsObjetoConteudoMntByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsObjetoConteudoMnt));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsObjetoConteudoMnt> result = 
	            (from entity0 in this.DbContext.TCS_OBJETO_CONTEUDO.Where(dynQuery, parameters.ToArray())
                orderby entity0.CONTEUDO_XML ascending
	            
	            	
	            select new TcsObjetoConteudoMnt()		
	            {
	            
                ConteudoXml = entity0.CONTEUDO_XML
                , IdObjeto = entity0.ID_OBJETO
                , IdObjetoConteudo = entity0.ID_OBJETO_CONTEUDO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get ConfiguracaoExportacaoByEntitySearch.
	    public IEnumerable<ConfiguracaoExportacao> GetConfiguracaoExportacaoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<ConfiguracaoExportacao> result = new List<ConfiguracaoExportacao>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get ConfiguracaoExportacaoByEntitySearchNoAssociations.
	    public IEnumerable<ConfiguracaoExportacao> GetConfiguracaoExportacaoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<ConfiguracaoExportacao> result = new List<ConfiguracaoExportacao>();
	  	
	
	        	

	
	        return result;
	    }
	
	    [Ignore()]
	    private void SetConfiguracaoExportacaoBusinessFilter(ref IQueryable<ConfiguracaoExportacao> query, List<EntitySearch> entitySearchList)
	    {
	    		int idxElement;
	    		string operatorValue;
	    		object value;
	    		//Get query by functions
	    		if (entitySearchList.Count > 0)
	    		{
	    			foreach (EntitySearch search in entitySearchList.Where(e => e.EntityName == "ConfiguracaoExportacao"))
	    			{

	
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "IsExcelDataSource" || e.Value.ToString() == "true")))
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
	    										bool tmpIsExcelDataSource1 = (bool)value;
	    										query = from r in query where r.IsExcelDataSource == tmpIsExcelDataSource1 select r;
	    										break;
	    									case "!=":
	    										bool tmpIsExcelDataSource2 = (bool)value;
	    										query = from r in query where r.IsExcelDataSource != tmpIsExcelDataSource2 select r;
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


		
	
	    
	    [Ignore]
	    //Get TcsObjetoPermissaoByEntitySearch.
	    public IQueryable<TcsObjetoPermissao> GetTcsObjetoPermissaoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsObjetoPermissao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsObjetoPermissao> result = 
	            (from entity0 in this.DbContext.TCS_OBJETO_PERMISSAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_PERFIL
                  let entity0Al2 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsObjetoPermissao()		
	            {
	            
                IdObjeto = entity0.ID_OBJETO
                , IdObjetoConteudo = entity0.ID_OBJETO_CONTEUDO
                , IdPerfil = entity0Al1.ID_PERFIL
                , IdTcsObjetoPermissao = entity0.ID_TCS_OBJETO_PERMISSAO
                , IdUsuario = entity0Al2.ID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsObjetoPermissaoByEntitySearchNoAssociations.
	    public IQueryable<TcsObjetoPermissao> GetTcsObjetoPermissaoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsObjetoPermissao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsObjetoPermissao> result = 
	            (from entity0 in this.DbContext.TCS_OBJETO_PERMISSAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_PERFIL
                  let entity0Al2 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsObjetoPermissao()		
	            {
	            
                IdObjeto = entity0.ID_OBJETO
                , IdObjetoConteudo = entity0.ID_OBJETO_CONTEUDO
                , IdPerfil = entity0Al1.ID_PERFIL
                , IdTcsObjetoPermissao = entity0.ID_TCS_OBJETO_PERMISSAO
                , IdUsuario = entity0Al2.ID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioByEntitySearch.
	    public IQueryable<TcsUsuario> GetTcsUsuarioByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuario));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuario> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsUsuario()		
	            {
	            
                IdUsuario = entity0.ID_USUARIO
                , NomeUsuario = entity0.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuario> GetTcsUsuarioByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuario));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuario> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsUsuario()		
	            {
	            
                IdUsuario = entity0.ID_USUARIO
                , NomeUsuario = entity0.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get LayoutInfoByEntitySearch.
	    public IEnumerable<LayoutInfo> GetLayoutInfoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<LayoutInfo> result = new List<LayoutInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get LayoutInfoByEntitySearchNoAssociations.
	    public IEnumerable<LayoutInfo> GetLayoutInfoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<LayoutInfo> result = new List<LayoutInfo>();
	  	
	
	        	

	
	        return result;
	    }
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get PagedTcsObjeto.
	    public IQueryable<TcsObjeto> GetPagedTcsObjeto(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsObjeto));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsObjeto> result = 
	            (from entity0 in this.DbContext.TCS_OBJETO.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_OBJETO ascending
	            
	            	
	            select new TcsObjeto()		
	            {
	            
                ClasseNome = entity0.CLASSE_NOME
                , DescObjeto = entity0.DESC_OBJETO
                , IdObjeto = entity0.ID_OBJETO
                , LxTipoObjeto = entity0.LX_TIPO_OBJETO
                , LxTipoObjetoName = ((entity0.LX_TIPO_OBJETO) == 1 ? "BO" : ((entity0.LX_TIPO_OBJETO) == 3 ? "Campo" : ((entity0.LX_TIPO_OBJETO) == 10 ? "Filtro" : ((entity0.LX_TIPO_OBJETO) == 9 ? "Layout" : ((entity0.LX_TIPO_OBJETO) == 6 ? "Relatório" : ((entity0.LX_TIPO_OBJETO) == 5 ? "Stored Procedure" : ((entity0.LX_TIPO_OBJETO) == 8 ? "Template de ação de Workflow" : ((entity0.LX_TIPO_OBJETO) == 2 ? "Transação" : ((entity0.LX_TIPO_OBJETO) == 4 ? "Trigger" : ((entity0.LX_TIPO_OBJETO) == 11 ? "Extensão (Objeto de entrada)" : ((entity0.LX_TIPO_OBJETO) == 7 ? "Workflow" : "")))))))))))
                , ObjetoLinx = false
                , PathObjeto = entity0.PATH_OBJETO
		
	            }
	            ).Skip(skip).Take(take);
	
	        SetTcsObjetoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsTransacao.
	    public IQueryable<TcsTransacao> GetPagedTcsTransacao(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsTransacao> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_TRANSACAO ascending
	            
	            	
	            select new TcsTransacao()		
	            {
	            
                ClasseNome = entity0.CLASSE_NOME
                , CodTransacao = entity0.COD_TRANSACAO
                , DescTransacao = entity0.DESC_TRANSACAO
                , IdObjeto = entity0.ID_OBJETO
                , IdTransacao = entity0.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , LxTipoTransacao = entity0.LX_TIPO_TRANSACAO
                , LxTipoTransacaoName = ((entity0.LX_TIPO_TRANSACAO) == 7 ? "Assistente" : ((entity0.LX_TIPO_TRANSACAO) == 8 ? "Dashboard" : ((entity0.LX_TIPO_TRANSACAO) == 2 ? "ERP" : ((entity0.LX_TIPO_TRANSACAO) == 6 ? "ERP App" : ((entity0.LX_TIPO_TRANSACAO) == 4 ? "Excel" : ((entity0.LX_TIPO_TRANSACAO) == 3 ? "Loja" : ((entity0.LX_TIPO_TRANSACAO) == 5 ? "Mobile" : ((entity0.LX_TIPO_TRANSACAO) == 1 ? "Todos" : ""))))))))
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsObjetoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsObjeto));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_OBJETO.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsTransacaoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_TRANSACAO.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsObjetoConteudoMnt.
	    public IQueryable<TcsObjetoConteudoMnt> GetPagedTcsObjetoConteudoMnt(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsObjetoConteudoMnt));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsObjetoConteudoMnt> result = 
	            (from entity0 in this.DbContext.TCS_OBJETO_CONTEUDO.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_OBJETO_CONTEUDO ascending
	            
	            	
	            select new TcsObjetoConteudoMnt()		
	            {
	            
                ConteudoXml = entity0.CONTEUDO_XML
                , IdObjeto = entity0.ID_OBJETO
                , IdObjetoConteudo = entity0.ID_OBJETO_CONTEUDO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsObjetoConteudoMntCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsObjetoConteudoMnt));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_OBJETO_CONTEUDO.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedConfiguracaoExportacao.
	    public IEnumerable<ConfiguracaoExportacao> GetPagedConfiguracaoExportacao(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<ConfiguracaoExportacao> result = new List<ConfiguracaoExportacao>();
	  	
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetConfiguracaoExportacaoCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsObjetoPermissao.
	    public IQueryable<TcsObjetoPermissao> GetPagedTcsObjetoPermissao(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsObjetoPermissao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsObjetoPermissao> result = 
	            (from entity0 in this.DbContext.TCS_OBJETO_PERMISSAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_PERFIL
                  let entity0Al2 = entity0.TCS_USUARIO
                orderby entity0.ID_TCS_OBJETO_PERMISSAO ascending
	            
	            	
	            select new TcsObjetoPermissao()		
	            {
	            
                IdObjeto = entity0.ID_OBJETO
                , IdObjetoConteudo = entity0.ID_OBJETO_CONTEUDO
                , IdPerfil = entity0Al1.ID_PERFIL
                , IdTcsObjetoPermissao = entity0.ID_TCS_OBJETO_PERMISSAO
                , IdUsuario = entity0Al2.ID_USUARIO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsObjetoPermissaoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsObjetoPermissao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_OBJETO_PERMISSAO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_PERFIL
                  let entityAl2 = entity.TCS_USUARIO
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsUsuario.
	    public IQueryable<TcsUsuario> GetPagedTcsUsuario(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuario));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuario> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_USUARIO ascending
	            
	            	
	            select new TcsUsuario()		
	            {
	            
                IdUsuario = entity0.ID_USUARIO
                , NomeUsuario = entity0.NOME_USUARIO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsUsuarioCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuario));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_USUARIO.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedLayoutInfo.
	    public IEnumerable<LayoutInfo> GetPagedLayoutInfo(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<LayoutInfo> result = new List<LayoutInfo>();
	  	
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetLayoutInfoCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    
	    [Update()]	
	    //Update TcsObjeto.
	    public void UpdateTcsObjeto(TcsObjeto entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsObjeto.
	    public void InsertTcsObjeto(TcsObjeto entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsObjeto.
	    public void DeleteTcsObjeto(TcsObjeto entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsTransacao.
	    public void UpdateTcsTransacao(TcsTransacao entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert TcsTransacao.
	    public void InsertTcsTransacao(TcsTransacao entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete TcsTransacao.
	    public void DeleteTcsTransacao(TcsTransacao entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsObjetoConteudoMnt.
	    public void UpdateTcsObjetoConteudoMnt(TcsObjetoConteudoMnt entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsObjetoConteudoMnt.
	    public void InsertTcsObjetoConteudoMnt(TcsObjetoConteudoMnt entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsObjetoConteudoMnt.
	    public void DeleteTcsObjetoConteudoMnt(TcsObjetoConteudoMnt entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update ConfiguracaoExportacao.
	    public void UpdateConfiguracaoExportacao(ConfiguracaoExportacao entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert ConfiguracaoExportacao.
	    public void InsertConfiguracaoExportacao(ConfiguracaoExportacao entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete ConfiguracaoExportacao.
	    public void DeleteConfiguracaoExportacao(ConfiguracaoExportacao entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsObjetoPermissao.
	    public void UpdateTcsObjetoPermissao(TcsObjetoPermissao entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsObjetoPermissao.
	    public void InsertTcsObjetoPermissao(TcsObjetoPermissao entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsObjetoPermissao.
	    public void DeleteTcsObjetoPermissao(TcsObjetoPermissao entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsUsuario.
	    public void UpdateTcsUsuario(TcsUsuario entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsUsuario.
	    public void InsertTcsUsuario(TcsUsuario entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsUsuario.
	    public void DeleteTcsUsuario(TcsUsuario entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update LayoutInfo.
	    public void UpdateLayoutInfo(LayoutInfo entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert LayoutInfo.
	    public void InsertLayoutInfo(LayoutInfo entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete LayoutInfo.
	    public void DeleteLayoutInfo(LayoutInfo entity)
	    {



	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}