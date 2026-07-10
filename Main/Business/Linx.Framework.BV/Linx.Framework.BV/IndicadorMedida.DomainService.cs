					
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

namespace Linx.Framework.BV.IndicadorMedida
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_INDICADOR_MEDIDA.ID_INDICADOR_MEDIDA", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Indicador de Medidas];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsIndicadorMedida,TcsIndicadorMedida.TcsIndicadorIndice];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdIndicadorMedida];ReadOnly[false];Entities[TCS_INDICADOR_MEDIDA:IdIndicadorMedida];SubQueryInfo[];EdmEntityName[TCS_INDICADOR_MEDIDA];EntityRelations[];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsIndicadorMedida")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.IndicadorMedida.TcsIndicadorMedida")]
	public partial class TcsIndicadorMedida : Linx.Data.Entity
	{

	
		
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.TcsIndicadorIndiceList != null && this.TcsIndicadorIndiceList.Count() > 0)
	      {
	         foreach (var entity in this.TcsIndicadorIndiceList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.TcsIndicadorIndiceList != null)
	      {
	         foreach (var detail in this.TcsIndicadorIndiceList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsIndicadorIndiceList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(IndicadorMedidaDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("TcsIndicadorIndice"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsIndicadorIndice");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdIndicadorMedida"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdIndicadorMedida));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsIndicadorIndice and all sub-details
	         if (this.TcsIndicadorIndiceList == null || this.TcsIndicadorIndiceList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsIndicadorIndiceList = context.GetPagedTcsIndicadorIndice(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsIndicadorIndiceList = (from r in context.GetTcsIndicadorIndiceByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _TcsIndicadorIndiceElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsIndicadorIndice && ((TcsIndicadorIndice)e.Entity).TcsIndicadorMedida == null && e.Associations == null && e.OriginalAssociations == null && ((TcsIndicadorIndice)e.Entity).IdIndicadorMedida == this.IdIndicadorMedida).ToList();
 	      if (_TcsIndicadorIndiceElements.Count > 0 && this.TcsIndicadorIndiceList.Count() == 0)
 	      {
 	          this.TcsIndicadorIndiceList = _TcsIndicadorIndiceElements.Select(e => (TcsIndicadorIndice)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsIndicadorIndiceElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsIndicadorIndice)detail.Entity).TcsIndicadorMedida = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsIndicadorMedida", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsIndicadorIndiceList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For CodIndicadorMedida
	    partial void OnCodIndicadorMedidaChanging(System.String value);
	    partial void OnCodIndicadorMedidaChanged();

	    private System.String _CodIndicadorMedida;

	    [DataMember(IsRequired = true, Name = "CodIndicadorMedida", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Código", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_INDICADOR_MEDIDA.COD_INDICADOR_MEDIDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_INDICADOR_MEDIDA.COD_INDICADOR_MEDIDA")]
	    public System.String CodIndicadorMedida
	    {
	    	    get
	    	    {
	    	          return _CodIndicadorMedida;
	    	    }
	    	    set
	    	    {
	    	          if (this._CodIndicadorMedida != value)
	    	          {
	    	              this.ValidateProperty("CodIndicadorMedida", value);
	    	              this.OnCodIndicadorMedidaChanging(value);
	    	              this.RaiseDataMemberChanging("CodIndicadorMedida");
	    	              this._CodIndicadorMedida = value;
	    	              this.RaiseDataMemberChanged("CodIndicadorMedida");
	    	              this.OnCodIndicadorMedidaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescIndicadorMedida
	    partial void OnDescIndicadorMedidaChanging(System.String value);
	    partial void OnDescIndicadorMedidaChanged();

	    private System.String _DescIndicadorMedida;

	    [DataMember(IsRequired = true, Name = "DescIndicadorMedida", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_INDICADOR_MEDIDA.DESC_INDICADOR_MEDIDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_INDICADOR_MEDIDA.DESC_INDICADOR_MEDIDA")]
	    public System.String DescIndicadorMedida
	    {
	    	    get
	    	    {
	    	          return _DescIndicadorMedida;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescIndicadorMedida != value)
	    	          {
	    	              this.ValidateProperty("DescIndicadorMedida", value);
	    	              this.OnDescIndicadorMedidaChanging(value);
	    	              this.RaiseDataMemberChanging("DescIndicadorMedida");
	    	              this._DescIndicadorMedida = value;
	    	              this.RaiseDataMemberChanged("DescIndicadorMedida");
	    	              this.OnDescIndicadorMedidaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdIndicadorMedida
	    partial void OnIdIndicadorMedidaChanging(Int64 value);
	    partial void OnIdIndicadorMedidaChanged();

	    private Int64 _IdIndicadorMedida;

	    [DataMember(IsRequired = true, Name = "IdIndicadorMedida", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Indicador Medida", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_INDICADOR_MEDIDA.ID_INDICADOR_MEDIDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_INDICADOR_MEDIDA.ID_INDICADOR_MEDIDA")]
	    public Int64 IdIndicadorMedida
	    {
	    	    get
	    	    {
	    	          return _IdIndicadorMedida;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdIndicadorMedida != value)
	    	          {
	    	              this.ValidateProperty("IdIndicadorMedida", value);
	    	              this.OnIdIndicadorMedidaChanging(value);
	    	              this.RaiseDataMemberChanging("IdIndicadorMedida");
	    	              this._IdIndicadorMedida = value;
	    	              this.RaiseDataMemberChanged("IdIndicadorMedida");
	    	              this.OnIdIndicadorMedidaChanged();
	    	          }
	    	    }
	    }

	    private Int64 _TemporaryIdIndicadorMedida;
	    [DataMember(Name = "TemporaryIdIndicadorMedida", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Indicador Medida (Tmp)", Description="Temporary Key", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdIndicadorMedida
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdIndicadorMedida.IsNullOrEmpty())
	    	                this._TemporaryIdIndicadorMedida = this._IdIndicadorMedida;
	    	          return this._TemporaryIdIndicadorMedida;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdIndicadorMedida != value)
	    	              this._TemporaryIdIndicadorMedida = value;
	    	    }
	    }	

	    #endregion Data Properties

	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<TcsIndicadorIndice> _TcsIndicadorIndiceList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsIndicadorMedida_TcsIndicadorIndice", "IdIndicadorMedida", "IdIndicadorMedida", IsForeignKey=false)]
	    [DataMember(Name = "TcsIndicadorIndiceList", EmitDefaultValue = true)]
	    public IEnumerable<TcsIndicadorIndice> TcsIndicadorIndiceList
	    {
	        get
	        {
	
	            if (this._TcsIndicadorIndiceList == null)
	            	this._TcsIndicadorIndiceList = new List<TcsIndicadorIndice>();
	
	            return this._TcsIndicadorIndiceList;
	        }
	        set
	        {
	            if (this._TcsIndicadorIndiceList != value)
	            {
	                this._TcsIndicadorIndiceList = value;
	                this.RaisePropertyChanged("TcsIndicadorIndiceList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_INDICADOR_MEDIDA").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_INDICADOR_MEDIDA), QualifiedEntitySetName = "ControleSistemaContext.TCS_INDICADOR_MEDIDA" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_INDICADOR_MEDIDA.ID_INDICADOR_MEDIDA", Source = "IdIndicadorMedida", Target = "ID_INDICADOR_MEDIDA", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_INDICADOR_MEDIDA", RelationPropertyName = "TCS_INDICADOR_MEDIDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_INDICADOR_MEDIDA.COD_INDICADOR_MEDIDA", Source = "CodIndicadorMedida", Target = "COD_INDICADOR_MEDIDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_INDICADOR_MEDIDA", RelationPropertyName = "TCS_INDICADOR_MEDIDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_INDICADOR_MEDIDA.DESC_INDICADOR_MEDIDA", Source = "DescIndicadorMedida", Target = "DESC_INDICADOR_MEDIDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_INDICADOR_MEDIDA", RelationPropertyName = "TCS_INDICADOR_MEDIDA" });

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

		

	[LinxPublicationView(PrimaryKeys="TCS_INDICADOR_INDICE.ID_INDICE_MEDIDA", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Faixas];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdIndiceMedida];ReadOnly[false];Entities[TCS_INDICADOR_INDICE:IdIndiceMedida];SubQueryInfo[Select 1 From #ParentAlias#.TCS_INDICADOR_INDICE_LISTA as #Alias#];EdmEntityName[TCS_INDICADOR_INDICE];EntityRelations[TCS_INDICADOR_MEDIDA(TCS_INDICADOR_MEDIDA)];EdmParentEntityName[TCS_INDICADOR_MEDIDA];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsIndicadorIndice")]
	[Serializable()]
	public partial class TcsIndicadorIndice : Linx.Data.Entity
	{

	

	    public TcsIndicadorIndice() : this(true) { }

	    public TcsIndicadorIndice(bool setDefaults) 
	    {

	        if (setDefaults)
	        {
	        	        Rgb = 0;
	        }	

	    }

			
	

	
	    #region Load Data Parent
		

	    public void LoadParent(IndicadorMedidaDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsIndicadorMedida");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdIndicadorMedida"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdIndicadorMedida));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsIndicadorMedida
	         this.TcsIndicadorMedida = (from r in context.GetTcsIndicadorMedidaByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For CodIndiceMedida
	    partial void OnCodIndiceMedidaChanging(System.String value);
	    partial void OnCodIndiceMedidaChanged();

	    private System.String _CodIndiceMedida;

	    [DataMember(Name = "CodIndiceMedida", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Código", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_INDICADOR_INDICE.COD_INDICE_MEDIDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_INDICADOR_INDICE.COD_INDICE_MEDIDA")]
	    public System.String CodIndiceMedida
	    {
	    	    get
	    	    {
	    	          return _CodIndiceMedida;
	    	    }
	    	    set
	    	    {
	    	          if (this._CodIndiceMedida != value)
	    	          {
	    	              this.ValidateProperty("CodIndiceMedida", value);
	    	              this.OnCodIndiceMedidaChanging(value);
	    	              this.RaiseDataMemberChanging("CodIndiceMedida");
	    	              this._CodIndiceMedida = value;
	    	              this.RaiseDataMemberChanged("CodIndiceMedida");
	    	              this.OnCodIndiceMedidaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescIndiceMedida
	    partial void OnDescIndiceMedidaChanging(System.String value);
	    partial void OnDescIndiceMedidaChanged();

	    private System.String _DescIndiceMedida;

	    [DataMember(IsRequired = true, Name = "DescIndiceMedida", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_INDICADOR_INDICE.DESC_INDICE_MEDIDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_INDICADOR_INDICE.DESC_INDICE_MEDIDA")]
	    public System.String DescIndiceMedida
	    {
	    	    get
	    	    {
	    	          return _DescIndiceMedida;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescIndiceMedida != value)
	    	          {
	    	              this.ValidateProperty("DescIndiceMedida", value);
	    	              this.OnDescIndiceMedidaChanging(value);
	    	              this.RaiseDataMemberChanging("DescIndiceMedida");
	    	              this._DescIndiceMedida = value;
	    	              this.RaiseDataMemberChanged("DescIndiceMedida");
	    	              this.OnDescIndiceMedidaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdIndicadorMedida
	    partial void OnIdIndicadorMedidaChanging(Int64 value);
	    partial void OnIdIndicadorMedidaChanged();

	    private Int64 _IdIndicadorMedida;

	    [DataMember(IsRequired = true, Name = "IdIndicadorMedida", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Indicador Medida", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_INDICADOR_INDICE.TCS_INDICADOR_MEDIDA.ID_INDICADOR_MEDIDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_INDICADOR_INDICE.TCS_INDICADOR_MEDIDA.ID_INDICADOR_MEDIDA")]
	    public Int64 IdIndicadorMedida
	    {
	    	    get
	    	    {
	    	          return _IdIndicadorMedida;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdIndicadorMedida != value)
	    	          {
	    	              this.ValidateProperty("IdIndicadorMedida", value);
	    	              this.OnIdIndicadorMedidaChanging(value);
	    	              this.RaiseDataMemberChanging("IdIndicadorMedida");
	    	              this._IdIndicadorMedida = value;
	    	              this.RaiseDataMemberChanged("IdIndicadorMedida");
	    	              this.OnIdIndicadorMedidaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdIndiceMedida
	    partial void OnIdIndiceMedidaChanging(Int64 value);
	    partial void OnIdIndiceMedidaChanged();

	    private Int64 _IdIndiceMedida;

	    [DataMember(IsRequired = true, Name = "IdIndiceMedida", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Indice Medida", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_INDICADOR_INDICE.ID_INDICE_MEDIDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_INDICADOR_INDICE.ID_INDICE_MEDIDA")]
	    public Int64 IdIndiceMedida
	    {
	    	    get
	    	    {
	    	          return _IdIndiceMedida;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdIndiceMedida != value)
	    	          {
	    	              this.ValidateProperty("IdIndiceMedida", value);
	    	              this.OnIdIndiceMedidaChanging(value);
	    	              this.RaiseDataMemberChanging("IdIndiceMedida");
	    	              this._IdIndiceMedida = value;
	    	              this.RaiseDataMemberChanged("IdIndiceMedida");
	    	              this.OnIdIndiceMedidaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IndicaMedidaAlvo
	    partial void OnIndicaMedidaAlvoChanging(System.Nullable<System.Boolean> value);
	    partial void OnIndicaMedidaAlvoChanged();

	    private System.Nullable<System.Boolean> _IndicaMedidaAlvo;

	    [DataMember(Name = "IndicaMedidaAlvo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ativo", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_INDICADOR_INDICE.INDICA_MEDIDA_ALVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_INDICADOR_INDICE.INDICA_MEDIDA_ALVO")]
	    public System.Nullable<System.Boolean> IndicaMedidaAlvo
	    {
	    	    get
	    	    {
	    	          return _IndicaMedidaAlvo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IndicaMedidaAlvo != value)
	    	          {
	    	              this.ValidateProperty("IndicaMedidaAlvo", value);
	    	              this.OnIndicaMedidaAlvoChanging(value);
	    	              this.RaiseDataMemberChanging("IndicaMedidaAlvo");
	    	              this._IndicaMedidaAlvo = value;
	    	              this.RaiseDataMemberChanged("IndicaMedidaAlvo");
	    	              this.OnIndicaMedidaAlvoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LimiteInferior
	    partial void OnLimiteInferiorChanging(System.Decimal value);
	    partial void OnLimiteInferiorChanged();

	    private System.Decimal _LimiteInferior;

	    [DataMember(IsRequired = true, Name = "LimiteInferior", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Limite Inferior", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[20:5];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N5];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_INDICADOR_INDICE.LIMITE_INFERIOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_INDICADOR_INDICE.LIMITE_INFERIOR")]
	    public System.Decimal LimiteInferior
	    {
	    	    get
	    	    {
	    	          return _LimiteInferior;
	    	    }
	    	    set
	    	    {
	    	          if (this._LimiteInferior != value)
	    	          {
	    	              this.ValidateProperty("LimiteInferior", value);
	    	              this.OnLimiteInferiorChanging(value);
	    	              this.RaiseDataMemberChanging("LimiteInferior");
	    	              this._LimiteInferior = value;
	    	              this.RaiseDataMemberChanged("LimiteInferior");
	    	              this.OnLimiteInferiorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LimiteSuperior
	    partial void OnLimiteSuperiorChanging(System.Decimal value);
	    partial void OnLimiteSuperiorChanged();

	    private System.Decimal _LimiteSuperior;

	    [DataMember(IsRequired = true, Name = "LimiteSuperior", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Limite Superior", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[20:5];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N5];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_INDICADOR_INDICE.LIMITE_SUPERIOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_INDICADOR_INDICE.LIMITE_SUPERIOR")]
	    public System.Decimal LimiteSuperior
	    {
	    	    get
	    	    {
	    	          return _LimiteSuperior;
	    	    }
	    	    set
	    	    {
	    	          if (this._LimiteSuperior != value)
	    	          {
	    	              this.ValidateProperty("LimiteSuperior", value);
	    	              this.OnLimiteSuperiorChanging(value);
	    	              this.RaiseDataMemberChanging("LimiteSuperior");
	    	              this._LimiteSuperior = value;
	    	              this.RaiseDataMemberChanged("LimiteSuperior");
	    	              this.OnLimiteSuperiorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Rgb
	    partial void OnRgbChanging(System.Nullable<System.Int32> value);
	    partial void OnRgbChanged();

	    private System.Nullable<System.Int32> _Rgb;

	    [DataMember(Name = "Rgb", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cor", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[0];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_INDICADOR_INDICE.RGB];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_INDICADOR_INDICE.RGB")]
	    public System.Nullable<System.Int32> Rgb
	    {
	    	    get
	    	    {
	    	          return _Rgb;
	    	    }
	    	    set
	    	    {
	    	          if (this._Rgb != value)
	    	          {
	    	              this.ValidateProperty("Rgb", value);
	    	              this.OnRgbChanging(value);
	    	              this.RaiseDataMemberChanging("Rgb");
	    	              this._Rgb = value;
	    	              this.RaiseDataMemberChanged("Rgb");
	    	              this.OnRgbChanged();
	    	          }
	    	    }
	    }

	    private Int64 _TemporaryIdIndiceMedida;
	    [DataMember(Name = "TemporaryIdIndiceMedida", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Indice Medida (Tmp)", Description="Temporary Key", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdIndiceMedida
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdIndiceMedida.IsNullOrEmpty())
	    	                this._TemporaryIdIndiceMedida = this._IdIndiceMedida;
	    	          return this._TemporaryIdIndiceMedida;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdIndiceMedida != value)
	    	              this._TemporaryIdIndiceMedida = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsIndicadorMedida _TcsIndicadorMedida;
	    [DataMember(Name = "TcsIndicadorMedida", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsIndicadorMedida_TcsIndicadorIndice", "IdIndicadorMedida", "IdIndicadorMedida", IsForeignKey=true)]
	    public TcsIndicadorMedida TcsIndicadorMedida
	    {
	        get
	        {
	            return this._TcsIndicadorMedida;
	        }
	        set
	        {
	            if (this._TcsIndicadorMedida != value)
	            {
	                this._TcsIndicadorMedida = value;
	                this.RaisePropertyChanged("TcsIndicadorMedidaList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_INDICADOR_INDICE").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_INDICADOR_INDICE), QualifiedEntitySetName = "ControleSistemaContext.TCS_INDICADOR_INDICE" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_INDICADOR_INDICE.RGB", Source = "Rgb", Target = "RGB", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_INDICADOR_INDICE", RelationPropertyName = "TCS_INDICADOR_INDICE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_INDICADOR_INDICE.LIMITE_INFERIOR", Source = "LimiteInferior", Target = "LIMITE_INFERIOR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_INDICADOR_INDICE", RelationPropertyName = "TCS_INDICADOR_INDICE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_INDICADOR_INDICE.LIMITE_SUPERIOR", Source = "LimiteSuperior", Target = "LIMITE_SUPERIOR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_INDICADOR_INDICE", RelationPropertyName = "TCS_INDICADOR_INDICE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_INDICADOR_INDICE.ID_INDICE_MEDIDA", Source = "IdIndiceMedida", Target = "ID_INDICE_MEDIDA", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_INDICADOR_INDICE", RelationPropertyName = "TCS_INDICADOR_INDICE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_INDICADOR_INDICE.COD_INDICE_MEDIDA", Source = "CodIndiceMedida", Target = "COD_INDICE_MEDIDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_INDICADOR_INDICE", RelationPropertyName = "TCS_INDICADOR_INDICE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_INDICADOR_INDICE.DESC_INDICE_MEDIDA", Source = "DescIndiceMedida", Target = "DESC_INDICE_MEDIDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_INDICADOR_INDICE", RelationPropertyName = "TCS_INDICADOR_INDICE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_INDICADOR_INDICE.INDICA_MEDIDA_ALVO", Source = "IndicaMedidaAlvo", Target = "INDICA_MEDIDA_ALVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_INDICADOR_INDICE", RelationPropertyName = "TCS_INDICADOR_INDICE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_INDICADOR_INDICE.TCS_INDICADOR_MEDIDA.ID_INDICADOR_MEDIDA", Source = "IdIndicadorMedida", Target = "ID_INDICADOR_MEDIDA", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_INDICADOR_MEDIDA", RelationPropertyName = "TCS_INDICADOR_MEDIDA" });

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

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Faixas];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdIndiceMedida];ReadOnly[false];Entities[TCS_INDICADOR_INDICE:IdIndiceMedida];SubQueryInfo[Select 1 From #ParentAlias#.TCS_INDICADOR_INDICE_LISTA as #Alias#];EdmEntityName[TCS_INDICADOR_INDICE];EntityRelations[TCS_INDICADOR_MEDIDA(TCS_INDICADOR_MEDIDA)];EdmParentEntityName[TCS_INDICADOR_MEDIDA];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsIndicadorIndice")]
	[Serializable()]
	public partial class TcsIndicadorIndiceParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For CodIndiceMedida
	    partial void OnCodIndiceMedidaChanging(System.String value);
	    partial void OnCodIndiceMedidaChanged();

	    private System.String _CodIndiceMedida;

	    [DataMember(Name = "CodIndiceMedida", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Código", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_INDICADOR_INDICE.COD_INDICE_MEDIDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_INDICADOR_INDICE.COD_INDICE_MEDIDA")]
	    public System.String CodIndiceMedida
	    {
	    	    get
	    	    {
	    	          return _CodIndiceMedida;
	    	    }
	    	    set
	    	    {
	    	          if (this._CodIndiceMedida != value)
	    	          {
	    	              this.ValidateProperty("CodIndiceMedida", value);
	    	              this.OnCodIndiceMedidaChanging(value);
	    	              this.RaiseDataMemberChanging("CodIndiceMedida");
	    	              this._CodIndiceMedida = value;
	    	              this.RaiseDataMemberChanged("CodIndiceMedida");
	    	              this.OnCodIndiceMedidaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescIndiceMedida
	    partial void OnDescIndiceMedidaChanging(System.String value);
	    partial void OnDescIndiceMedidaChanged();

	    private System.String _DescIndiceMedida;

	    [DataMember(IsRequired = true, Name = "DescIndiceMedida", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_INDICADOR_INDICE.DESC_INDICE_MEDIDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_INDICADOR_INDICE.DESC_INDICE_MEDIDA")]
	    public System.String DescIndiceMedida
	    {
	    	    get
	    	    {
	    	          return _DescIndiceMedida;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescIndiceMedida != value)
	    	          {
	    	              this.ValidateProperty("DescIndiceMedida", value);
	    	              this.OnDescIndiceMedidaChanging(value);
	    	              this.RaiseDataMemberChanging("DescIndiceMedida");
	    	              this._DescIndiceMedida = value;
	    	              this.RaiseDataMemberChanged("DescIndiceMedida");
	    	              this.OnDescIndiceMedidaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdIndicadorMedida
	    partial void OnIdIndicadorMedidaChanging(Int64 value);
	    partial void OnIdIndicadorMedidaChanged();

	    private Int64 _IdIndicadorMedida;

	    [DataMember(IsRequired = true, Name = "IdIndicadorMedida", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Indicador Medida", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_INDICADOR_INDICE.TCS_INDICADOR_MEDIDA.ID_INDICADOR_MEDIDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_INDICADOR_INDICE.TCS_INDICADOR_MEDIDA.ID_INDICADOR_MEDIDA")]
	    public Int64 IdIndicadorMedida
	    {
	    	    get
	    	    {
	    	          return _IdIndicadorMedida;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdIndicadorMedida != value)
	    	          {
	    	              this.ValidateProperty("IdIndicadorMedida", value);
	    	              this.OnIdIndicadorMedidaChanging(value);
	    	              this.RaiseDataMemberChanging("IdIndicadorMedida");
	    	              this._IdIndicadorMedida = value;
	    	              this.RaiseDataMemberChanged("IdIndicadorMedida");
	    	              this.OnIdIndicadorMedidaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdIndiceMedida
	    partial void OnIdIndiceMedidaChanging(Int64 value);
	    partial void OnIdIndiceMedidaChanged();

	    private Int64 _IdIndiceMedida;

	    [DataMember(IsRequired = true, Name = "IdIndiceMedida", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Indice Medida", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_INDICADOR_INDICE.ID_INDICE_MEDIDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_INDICADOR_INDICE.ID_INDICE_MEDIDA")]
	    public Int64 IdIndiceMedida
	    {
	    	    get
	    	    {
	    	          return _IdIndiceMedida;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdIndiceMedida != value)
	    	          {
	    	              this.ValidateProperty("IdIndiceMedida", value);
	    	              this.OnIdIndiceMedidaChanging(value);
	    	              this.RaiseDataMemberChanging("IdIndiceMedida");
	    	              this._IdIndiceMedida = value;
	    	              this.RaiseDataMemberChanged("IdIndiceMedida");
	    	              this.OnIdIndiceMedidaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IndicaMedidaAlvo
	    partial void OnIndicaMedidaAlvoChanging(System.Nullable<System.Boolean> value);
	    partial void OnIndicaMedidaAlvoChanged();

	    private System.Nullable<System.Boolean> _IndicaMedidaAlvo;

	    [DataMember(Name = "IndicaMedidaAlvo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ativo", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_INDICADOR_INDICE.INDICA_MEDIDA_ALVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_INDICADOR_INDICE.INDICA_MEDIDA_ALVO")]
	    public System.Nullable<System.Boolean> IndicaMedidaAlvo
	    {
	    	    get
	    	    {
	    	          return _IndicaMedidaAlvo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IndicaMedidaAlvo != value)
	    	          {
	    	              this.ValidateProperty("IndicaMedidaAlvo", value);
	    	              this.OnIndicaMedidaAlvoChanging(value);
	    	              this.RaiseDataMemberChanging("IndicaMedidaAlvo");
	    	              this._IndicaMedidaAlvo = value;
	    	              this.RaiseDataMemberChanged("IndicaMedidaAlvo");
	    	              this.OnIndicaMedidaAlvoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LimiteInferior
	    partial void OnLimiteInferiorChanging(System.Decimal value);
	    partial void OnLimiteInferiorChanged();

	    private System.Decimal _LimiteInferior;

	    [DataMember(IsRequired = true, Name = "LimiteInferior", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Limite Inferior", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[20:5];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N5];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_INDICADOR_INDICE.LIMITE_INFERIOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_INDICADOR_INDICE.LIMITE_INFERIOR")]
	    public System.Decimal LimiteInferior
	    {
	    	    get
	    	    {
	    	          return _LimiteInferior;
	    	    }
	    	    set
	    	    {
	    	          if (this._LimiteInferior != value)
	    	          {
	    	              this.ValidateProperty("LimiteInferior", value);
	    	              this.OnLimiteInferiorChanging(value);
	    	              this.RaiseDataMemberChanging("LimiteInferior");
	    	              this._LimiteInferior = value;
	    	              this.RaiseDataMemberChanged("LimiteInferior");
	    	              this.OnLimiteInferiorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LimiteSuperior
	    partial void OnLimiteSuperiorChanging(System.Decimal value);
	    partial void OnLimiteSuperiorChanged();

	    private System.Decimal _LimiteSuperior;

	    [DataMember(IsRequired = true, Name = "LimiteSuperior", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Limite Superior", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[20:5];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N5];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_INDICADOR_INDICE.LIMITE_SUPERIOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_INDICADOR_INDICE.LIMITE_SUPERIOR")]
	    public System.Decimal LimiteSuperior
	    {
	    	    get
	    	    {
	    	          return _LimiteSuperior;
	    	    }
	    	    set
	    	    {
	    	          if (this._LimiteSuperior != value)
	    	          {
	    	              this.ValidateProperty("LimiteSuperior", value);
	    	              this.OnLimiteSuperiorChanging(value);
	    	              this.RaiseDataMemberChanging("LimiteSuperior");
	    	              this._LimiteSuperior = value;
	    	              this.RaiseDataMemberChanged("LimiteSuperior");
	    	              this.OnLimiteSuperiorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Rgb
	    partial void OnRgbChanging(System.Nullable<System.Int32> value);
	    partial void OnRgbChanged();

	    private System.Nullable<System.Int32> _Rgb;

	    [DataMember(Name = "Rgb", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cor", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[0];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_INDICADOR_INDICE.RGB];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_INDICADOR_INDICE.RGB")]
	    public System.Nullable<System.Int32> Rgb
	    {
	    	    get
	    	    {
	    	          return _Rgb;
	    	    }
	    	    set
	    	    {
	    	          if (this._Rgb != value)
	    	          {
	    	              this.ValidateProperty("Rgb", value);
	    	              this.OnRgbChanging(value);
	    	              this.RaiseDataMemberChanging("Rgb");
	    	              this._Rgb = value;
	    	              this.RaiseDataMemberChanged("Rgb");
	    	              this.OnRgbChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For CodIndicadorMedida
	    partial void OnCodIndicadorMedidaChanging(System.String value);
	    partial void OnCodIndicadorMedidaChanged();

	    private System.String _CodIndicadorMedida;

	    [DataMember(IsRequired = true, Name = "CodIndicadorMedida", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Código", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_INDICADOR_INDICE.TCS_INDICADOR_MEDIDA.COD_INDICADOR_MEDIDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_INDICADOR_MEDIDA.COD_INDICADOR_MEDIDA")]
	    public System.String CodIndicadorMedida
	    {
	    	    get
	    	    {
	    	          return _CodIndicadorMedida;
	    	    }
	    	    set
	    	    {
	    	          if (this._CodIndicadorMedida != value)
	    	          {
	    	              this.ValidateProperty("CodIndicadorMedida", value);
	    	              this.OnCodIndicadorMedidaChanging(value);
	    	              this.RaiseDataMemberChanging("CodIndicadorMedida");
	    	              this._CodIndicadorMedida = value;
	    	              this.RaiseDataMemberChanged("CodIndicadorMedida");
	    	              this.OnCodIndicadorMedidaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescIndicadorMedida
	    partial void OnDescIndicadorMedidaChanging(System.String value);
	    partial void OnDescIndicadorMedidaChanged();

	    private System.String _DescIndicadorMedida;

	    [DataMember(IsRequired = true, Name = "DescIndicadorMedida", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_INDICADOR_INDICE.TCS_INDICADOR_MEDIDA.DESC_INDICADOR_MEDIDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_INDICADOR_MEDIDA.DESC_INDICADOR_MEDIDA")]
	    public System.String DescIndicadorMedida
	    {
	    	    get
	    	    {
	    	          return _DescIndicadorMedida;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescIndicadorMedida != value)
	    	          {
	    	              this.ValidateProperty("DescIndicadorMedida", value);
	    	              this.OnDescIndicadorMedidaChanging(value);
	    	              this.RaiseDataMemberChanging("DescIndicadorMedida");
	    	              this._DescIndicadorMedida = value;
	    	              this.RaiseDataMemberChanged("DescIndicadorMedida");
	    	              this.OnDescIndicadorMedidaChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_INDICADOR_INDICE").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_INDICADOR_INDICE), QualifiedEntitySetName = "ControleSistemaContext.TCS_INDICADOR_INDICE" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_INDICADOR_INDICE.RGB", Source = "Rgb", Target = "RGB", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_INDICADOR_INDICE", RelationPropertyName = "TCS_INDICADOR_INDICE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_INDICADOR_INDICE.LIMITE_INFERIOR", Source = "LimiteInferior", Target = "LIMITE_INFERIOR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_INDICADOR_INDICE", RelationPropertyName = "TCS_INDICADOR_INDICE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_INDICADOR_INDICE.LIMITE_SUPERIOR", Source = "LimiteSuperior", Target = "LIMITE_SUPERIOR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_INDICADOR_INDICE", RelationPropertyName = "TCS_INDICADOR_INDICE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_INDICADOR_INDICE.ID_INDICE_MEDIDA", Source = "IdIndiceMedida", Target = "ID_INDICE_MEDIDA", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_INDICADOR_INDICE", RelationPropertyName = "TCS_INDICADOR_INDICE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_INDICADOR_INDICE.COD_INDICE_MEDIDA", Source = "CodIndiceMedida", Target = "COD_INDICE_MEDIDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_INDICADOR_INDICE", RelationPropertyName = "TCS_INDICADOR_INDICE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_INDICADOR_INDICE.DESC_INDICE_MEDIDA", Source = "DescIndiceMedida", Target = "DESC_INDICE_MEDIDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_INDICADOR_INDICE", RelationPropertyName = "TCS_INDICADOR_INDICE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_INDICADOR_INDICE.INDICA_MEDIDA_ALVO", Source = "IndicaMedidaAlvo", Target = "INDICA_MEDIDA_ALVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_INDICADOR_INDICE", RelationPropertyName = "TCS_INDICADOR_INDICE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_INDICADOR_INDICE.TCS_INDICADOR_MEDIDA.ID_INDICADOR_MEDIDA", Source = "IdIndicadorMedida", Target = "ID_INDICADOR_MEDIDA", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_INDICADOR_MEDIDA", RelationPropertyName = "TCS_INDICADOR_MEDIDA" });

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
	[DomainIdentifier("ProcessorOverviewIndicadorMedidaDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class IndicadorMedidaDomainService : DomainService, IDataServiceContext 
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

		
	    public IndicadorMedidaDomainService() : this("", null, null) { }
	    public IndicadorMedidaDomainService(string connectionString) : this(connectionString, null, null) { }
	    public IndicadorMedidaDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public IndicadorMedidaDomainService(Linx.Framework.ControleSistema.BM.ControleSistemaContext dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public IndicadorMedidaDomainService(string connectionString, Linx.Framework.ControleSistema.BM.ControleSistemaContext dataContext, Dictionary<string, string> headers) : base() 
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

		
 
 	        bool createNewChangeSet = false;
 
 	        //Adjust data hierarchy
 	        var _TcsIndicadorMedidaElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsIndicadorMedida && e.Entity.GetType().Name == "TcsIndicadorMedida" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _TcsIndicadorMedidaElements)
 	           if (((TcsIndicadorMedida)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 
 	        //Remove inconsistent details
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsIndicadorIndice && e.Entity.GetType().Name == "TcsIndicadorIndice" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
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
	
		

	        if (entityName.InList("Linx.Framework.BV.IndicadorMedida.TcsIndicadorMedida"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsIndicadorMedida",
	        			NameSpace = "Linx.Framework.BV.IndicadorMedida",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "Indicador de Medidas",
	        			ClearMethodName = "ClearTcsIndicadorMedida",
	        			QueryMethodName  = "GetPagedTcsIndicadorMedida",	
	        			CountingMethodName  = "GetTcsIndicadorMedida" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.IndicadorMedida.TcsIndicadorMedida"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.IndicadorMedida.TcsIndicadorMedida"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.IndicadorMedida.TcsIndicadorMedida", "Linx.Framework.BV.IndicadorMedida.TcsIndicadorIndice"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsIndicadorIndice" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.IndicadorMedida",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsIndicadorMedida",	
	        			DisplayName = "Faixas",
	        			ClearMethodName = "ClearTcsIndicadorIndice" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsIndicadorIndice" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsIndicadorIndice" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.IndicadorMedida.TcsIndicadorIndice"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.IndicadorMedida.TcsIndicadorIndice" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
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

         		    return new string[] { "Framework_IndicadorMedidaClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.IndicadorMedidaClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Framework_indicadorMedidaService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.indicadorMedidaService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear TcsIndicadorMedida.
	    public IEnumerable<TcsIndicadorMedida> ClearTcsIndicadorMedida()
	    {
	        List<TcsIndicadorMedida> result = new List<TcsIndicadorMedida>();
	        result.Add(new TcsIndicadorMedida());	
			
	        result[0].TcsIndicadorIndiceList = new List<TcsIndicadorIndice>();
	        ((List<TcsIndicadorIndice>)result[0].TcsIndicadorIndiceList).Add(new TcsIndicadorIndice(false));
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsIndicadorIndice.
	    public IEnumerable<TcsIndicadorIndice> ClearTcsIndicadorIndice()
	    {
	        List<TcsIndicadorIndice> result = new List<TcsIndicadorIndice>();
	        result.Add(new TcsIndicadorIndice(false));	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsIndicadorMedida.
	    public IQueryable<TcsIndicadorMedida> GetTcsIndicadorMedida()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsIndicadorMedida> result = 
	            (from entity0 in this.DbContext.TCS_INDICADOR_MEDIDA
	            
	            	
	            select new TcsIndicadorMedida()		
	            {
	            
                CodIndicadorMedida = entity0.COD_INDICADOR_MEDIDA
                , DescIndicadorMedida = entity0.DESC_INDICADOR_MEDIDA
                , IdIndicadorMedida = entity0.ID_INDICADOR_MEDIDA
			
                ,TcsIndicadorIndiceList = 
	                        (from entity1 in entity0.TCS_INDICADOR_INDICE_LISTA
                                  let entity1Al1 = entity1.TCS_INDICADOR_MEDIDA
	                        
	                        	
	                        select new TcsIndicadorIndice()
	                        {
	                        
                                CodIndiceMedida = entity1.COD_INDICE_MEDIDA
                                , DescIndiceMedida = entity1.DESC_INDICE_MEDIDA
                                , IdIndicadorMedida = entity1Al1.ID_INDICADOR_MEDIDA
                                , IdIndiceMedida = entity1.ID_INDICE_MEDIDA
                                , IndicaMedidaAlvo = entity1.INDICA_MEDIDA_ALVO
                                , LimiteInferior = entity1.LIMITE_INFERIOR
                                , LimiteSuperior = entity1.LIMITE_SUPERIOR
                                , Rgb = entity1.RGB
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsIndicadorIndice.
	    public IQueryable<TcsIndicadorIndice> GetTcsIndicadorIndice()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsIndicadorIndice> result = 
	            (from entity0 in this.DbContext.TCS_INDICADOR_INDICE
                  let entity0Al1 = entity0.TCS_INDICADOR_MEDIDA
	            
	            	
	            select new TcsIndicadorIndice()		
	            {
	            
                CodIndiceMedida = entity0.COD_INDICE_MEDIDA
                , DescIndiceMedida = entity0.DESC_INDICE_MEDIDA
                , IdIndicadorMedida = entity0Al1.ID_INDICADOR_MEDIDA
                , IdIndiceMedida = entity0.ID_INDICE_MEDIDA
                , IndicaMedidaAlvo = entity0.INDICA_MEDIDA_ALVO
                , LimiteInferior = entity0.LIMITE_INFERIOR
                , LimiteSuperior = entity0.LIMITE_SUPERIOR
                , Rgb = entity0.RGB
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsIndicadorMedidaNoAssociations.
	    public IQueryable<TcsIndicadorMedida> GetTcsIndicadorMedidaNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsIndicadorMedida> result = 
	            (from entity0 in this.DbContext.TCS_INDICADOR_MEDIDA
	            
	            	
	            select new TcsIndicadorMedida()		
	            {
	            
                CodIndicadorMedida = entity0.COD_INDICADOR_MEDIDA
                , DescIndicadorMedida = entity0.DESC_INDICADOR_MEDIDA
                , IdIndicadorMedida = entity0.ID_INDICADOR_MEDIDA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsIndicadorIndiceNoAssociations.
	    public IQueryable<TcsIndicadorIndice> GetTcsIndicadorIndiceNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsIndicadorIndice> result = 
	            (from entity0 in this.DbContext.TCS_INDICADOR_INDICE
                  let entity0Al1 = entity0.TCS_INDICADOR_MEDIDA
	            
	            	
	            select new TcsIndicadorIndice()		
	            {
	            
                CodIndiceMedida = entity0.COD_INDICE_MEDIDA
                , DescIndiceMedida = entity0.DESC_INDICE_MEDIDA
                , IdIndicadorMedida = entity0Al1.ID_INDICADOR_MEDIDA
                , IdIndiceMedida = entity0.ID_INDICE_MEDIDA
                , IndicaMedidaAlvo = entity0.INDICA_MEDIDA_ALVO
                , LimiteInferior = entity0.LIMITE_INFERIOR
                , LimiteSuperior = entity0.LIMITE_SUPERIOR
                , Rgb = entity0.RGB
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	//Add filtering disabled property for TCS_INDICADOR_MEDIDA
	    	string[] bmDisabledTcsIndicadorMedidaList = this.GetEDM().GetFilteringDisabledList("TCS_INDICADOR_MEDIDA");
	    	if (bmDisabledTcsIndicadorMedidaList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsIndicadorMedidaList.Contains("TCS_INDICADOR_MEDIDA.COD_INDICADOR_MEDIDA"))
	    		{
	    			result.Add("TcsIndicadorMedida|CodIndicadorMedida");
	    			result.Add("TcsIndicadorMedida|TCS_INDICADOR_MEDIDA.COD_INDICADOR_MEDIDA");
	    		}
	
	    		if (bmDisabledTcsIndicadorMedidaList.Contains("TCS_INDICADOR_MEDIDA.DESC_INDICADOR_MEDIDA"))
	    		{
	    			result.Add("TcsIndicadorMedida|DescIndicadorMedida");
	    			result.Add("TcsIndicadorMedida|TCS_INDICADOR_MEDIDA.DESC_INDICADOR_MEDIDA");
	    		}
	
	    		if (bmDisabledTcsIndicadorMedidaList.Contains("TCS_INDICADOR_MEDIDA.ID_INDICADOR_MEDIDA"))
	    		{
	    			result.Add("TcsIndicadorMedida|IdIndicadorMedida");
	    			result.Add("TcsIndicadorMedida|TCS_INDICADOR_MEDIDA.ID_INDICADOR_MEDIDA");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_INDICADOR_INDICE
	    	string[] bmDisabledTcsIndicadorIndiceList = this.GetEDM().GetFilteringDisabledList("TCS_INDICADOR_INDICE");
	    	if (bmDisabledTcsIndicadorIndiceList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsIndicadorIndiceList.Contains("TCS_INDICADOR_INDICE.COD_INDICE_MEDIDA"))
	    		{
	    			result.Add("TcsIndicadorIndice|CodIndiceMedida");
	    			result.Add("TcsIndicadorIndice|TCS_INDICADOR_INDICE.COD_INDICE_MEDIDA");
	    		}
	
	    		if (bmDisabledTcsIndicadorIndiceList.Contains("TCS_INDICADOR_INDICE.DESC_INDICE_MEDIDA"))
	    		{
	    			result.Add("TcsIndicadorIndice|DescIndiceMedida");
	    			result.Add("TcsIndicadorIndice|TCS_INDICADOR_INDICE.DESC_INDICE_MEDIDA");
	    		}
	
	    		if (bmDisabledTcsIndicadorIndiceList.Contains("TCS_INDICADOR_INDICE.ID_INDICE_MEDIDA"))
	    		{
	    			result.Add("TcsIndicadorIndice|IdIndiceMedida");
	    			result.Add("TcsIndicadorIndice|TCS_INDICADOR_INDICE.ID_INDICE_MEDIDA");
	    		}
	
	    		if (bmDisabledTcsIndicadorIndiceList.Contains("TCS_INDICADOR_INDICE.INDICA_MEDIDA_ALVO"))
	    		{
	    			result.Add("TcsIndicadorIndice|IndicaMedidaAlvo");
	    			result.Add("TcsIndicadorIndice|TCS_INDICADOR_INDICE.INDICA_MEDIDA_ALVO");
	    		}
	
	    		if (bmDisabledTcsIndicadorIndiceList.Contains("TCS_INDICADOR_INDICE.LIMITE_INFERIOR"))
	    		{
	    			result.Add("TcsIndicadorIndice|LimiteInferior");
	    			result.Add("TcsIndicadorIndice|TCS_INDICADOR_INDICE.LIMITE_INFERIOR");
	    		}
	
	    		if (bmDisabledTcsIndicadorIndiceList.Contains("TCS_INDICADOR_INDICE.LIMITE_SUPERIOR"))
	    		{
	    			result.Add("TcsIndicadorIndice|LimiteSuperior");
	    			result.Add("TcsIndicadorIndice|TCS_INDICADOR_INDICE.LIMITE_SUPERIOR");
	    		}
	
	    		if (bmDisabledTcsIndicadorIndiceList.Contains("TCS_INDICADOR_INDICE.RGB"))
	    		{
	    			result.Add("TcsIndicadorIndice|Rgb");
	    			result.Add("TcsIndicadorIndice|TCS_INDICADOR_INDICE.RGB");
	    		}
	    	}
	    	return result;
	    }

	    #endregion Excluded Filters

	    #region Get By EntitySearchId Methods Definitions.
	
				
	    [Ignore]
	    //Get TcsIndicadorMedida By EntitySearchId.
	    public IQueryable<TcsIndicadorMedida> GetTcsIndicadorMedidaByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsIndicadorMedidaByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsIndicadorIndice By EntitySearchId.
	    public IQueryable<TcsIndicadorIndice> GetTcsIndicadorIndiceByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsIndicadorIndiceByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsIndicadorMedida By EntitySearchId.
	    public IQueryable<TcsIndicadorMedida> GetTcsIndicadorMedidaByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsIndicadorMedidaByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsIndicadorIndice By EntitySearchId.
	    public IQueryable<TcsIndicadorIndice> GetTcsIndicadorIndiceByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsIndicadorIndiceByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get TcsIndicadorMedida By Example.
	    [Ignore]
	    public IQueryable<TcsIndicadorMedida> GetTcsIndicadorMedidaByExample(TcsIndicadorMedida entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsIndicadorMedidaByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsIndicadorIndice By Example.
	    [Ignore]
	    public IQueryable<TcsIndicadorIndice> GetTcsIndicadorIndiceByExample(TcsIndicadorIndice entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsIndicadorIndiceByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsIndicadorMedida By Example.
	    [Ignore]
	    public IQueryable<TcsIndicadorMedida> GetTcsIndicadorMedidaByExampleNoAssociations(TcsIndicadorMedida entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsIndicadorMedidaByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsIndicadorIndice By Example.
	    [Ignore]
	    public IQueryable<TcsIndicadorIndice> GetTcsIndicadorIndiceByExampleNoAssociations(TcsIndicadorIndice entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsIndicadorIndiceByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public TcsIndicadorMedida GetTcsIndicadorMedidaByKey(Int64 idIndicadorMedida)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsIndicadorMedida");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdIndicadorMedida"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idIndicadorMedida));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsIndicadorMedidaByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsIndicadorIndice GetTcsIndicadorIndiceByKey(Int64 idIndiceMedida)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsIndicadorIndice");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdIndiceMedida"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idIndiceMedida));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsIndicadorIndiceByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get TcsIndicadorMedidaByEntitySearch.
	    public IQueryable<TcsIndicadorMedida> GetTcsIndicadorMedidaByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsIndicadorMedida));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsIndicadorMedida> result = 
	            (from entity0 in this.DbContext.TCS_INDICADOR_MEDIDA.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsIndicadorMedida()		
	            {
	            
                CodIndicadorMedida = entity0.COD_INDICADOR_MEDIDA
                , DescIndicadorMedida = entity0.DESC_INDICADOR_MEDIDA
                , IdIndicadorMedida = entity0.ID_INDICADOR_MEDIDA
			
                ,TcsIndicadorIndiceList = 
	                        (from entity1 in entity0.TCS_INDICADOR_INDICE_LISTA
                                  let entity1Al1 = entity1.TCS_INDICADOR_MEDIDA
	                        
	                        	
	                        select new TcsIndicadorIndice()
	                        {
	                        
                                CodIndiceMedida = entity1.COD_INDICE_MEDIDA
                                , DescIndiceMedida = entity1.DESC_INDICE_MEDIDA
                                , IdIndicadorMedida = entity1Al1.ID_INDICADOR_MEDIDA
                                , IdIndiceMedida = entity1.ID_INDICE_MEDIDA
                                , IndicaMedidaAlvo = entity1.INDICA_MEDIDA_ALVO
                                , LimiteInferior = entity1.LIMITE_INFERIOR
                                , LimiteSuperior = entity1.LIMITE_SUPERIOR
                                , Rgb = entity1.RGB
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsIndicadorIndiceByEntitySearch.
	    public IQueryable<TcsIndicadorIndice> GetTcsIndicadorIndiceByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsIndicadorIndice));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsIndicadorIndice> result = 
	            (from entity0 in this.DbContext.TCS_INDICADOR_INDICE.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_INDICADOR_MEDIDA
	            
	            	
	            select new TcsIndicadorIndice()		
	            {
	            
                CodIndiceMedida = entity0.COD_INDICE_MEDIDA
                , DescIndiceMedida = entity0.DESC_INDICE_MEDIDA
                , IdIndicadorMedida = entity0Al1.ID_INDICADOR_MEDIDA
                , IdIndiceMedida = entity0.ID_INDICE_MEDIDA
                , IndicaMedidaAlvo = entity0.INDICA_MEDIDA_ALVO
                , LimiteInferior = entity0.LIMITE_INFERIOR
                , LimiteSuperior = entity0.LIMITE_SUPERIOR
                , Rgb = entity0.RGB
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsIndicadorMedidaByEntitySearchNoAssociations.
	    public IQueryable<TcsIndicadorMedida> GetTcsIndicadorMedidaByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsIndicadorMedida));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsIndicadorMedida> result = 
	            (from entity0 in this.DbContext.TCS_INDICADOR_MEDIDA.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsIndicadorMedida()		
	            {
	            
                CodIndicadorMedida = entity0.COD_INDICADOR_MEDIDA
                , DescIndicadorMedida = entity0.DESC_INDICADOR_MEDIDA
                , IdIndicadorMedida = entity0.ID_INDICADOR_MEDIDA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsIndicadorIndiceByEntitySearchNoAssociations.
	    public IQueryable<TcsIndicadorIndice> GetTcsIndicadorIndiceByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsIndicadorIndice));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsIndicadorIndice> result = 
	            (from entity0 in this.DbContext.TCS_INDICADOR_INDICE.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_INDICADOR_MEDIDA
	            
	            	
	            select new TcsIndicadorIndice()		
	            {
	            
                CodIndiceMedida = entity0.COD_INDICE_MEDIDA
                , DescIndiceMedida = entity0.DESC_INDICE_MEDIDA
                , IdIndicadorMedida = entity0Al1.ID_INDICADOR_MEDIDA
                , IdIndiceMedida = entity0.ID_INDICE_MEDIDA
                , IndicaMedidaAlvo = entity0.INDICA_MEDIDA_ALVO
                , LimiteInferior = entity0.LIMITE_INFERIOR
                , LimiteSuperior = entity0.LIMITE_SUPERIOR
                , Rgb = entity0.RGB
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsIndicadorIndiceParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsIndicadorIndiceParentComposition> GetTcsIndicadorIndiceParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "TCS_INDICADOR_MEDIDA", "TCS_INDICADOR_INDICE", "TCS_INDICADOR_MEDIDA", typeof(TcsIndicadorIndiceParentComposition));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsIndicadorIndiceParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_INDICADOR_INDICE.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_INDICADOR_MEDIDA
	            
	            	
	            select new TcsIndicadorIndiceParentComposition()		
	            {
	            
                CodIndiceMedida = entity0.COD_INDICE_MEDIDA
                , DescIndiceMedida = entity0.DESC_INDICE_MEDIDA
                , IdIndicadorMedida = entity0Al1.ID_INDICADOR_MEDIDA
                , IdIndiceMedida = entity0.ID_INDICE_MEDIDA
                , IndicaMedidaAlvo = entity0.INDICA_MEDIDA_ALVO
                , LimiteInferior = entity0.LIMITE_INFERIOR
                , LimiteSuperior = entity0.LIMITE_SUPERIOR
                , Rgb = entity0.RGB
                //TcsIndicadorMedida Properties.
                , CodIndicadorMedida = entity0.TCS_INDICADOR_MEDIDA.COD_INDICADOR_MEDIDA
                , DescIndicadorMedida = entity0.TCS_INDICADOR_MEDIDA.DESC_INDICADOR_MEDIDA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get PagedTcsIndicadorMedida.
	    public IQueryable<TcsIndicadorMedida> GetPagedTcsIndicadorMedida(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsIndicadorMedida));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsIndicadorMedida> result = 
	            (from entity0 in this.DbContext.TCS_INDICADOR_MEDIDA.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_INDICADOR_MEDIDA ascending
	            
	            	
	            select new TcsIndicadorMedida()		
	            {
	            
                CodIndicadorMedida = entity0.COD_INDICADOR_MEDIDA
                , DescIndicadorMedida = entity0.DESC_INDICADOR_MEDIDA
                , IdIndicadorMedida = entity0.ID_INDICADOR_MEDIDA
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsIndicadorIndice.
	    public IQueryable<TcsIndicadorIndice> GetPagedTcsIndicadorIndice(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsIndicadorIndice));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsIndicadorIndice> result = 
	            (from entity0 in this.DbContext.TCS_INDICADOR_INDICE.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_INDICADOR_MEDIDA
                orderby entity0.ID_INDICE_MEDIDA ascending
	            
	            	
	            select new TcsIndicadorIndice()		
	            {
	            
                CodIndiceMedida = entity0.COD_INDICE_MEDIDA
                , DescIndiceMedida = entity0.DESC_INDICE_MEDIDA
                , IdIndicadorMedida = entity0Al1.ID_INDICADOR_MEDIDA
                , IdIndiceMedida = entity0.ID_INDICE_MEDIDA
                , IndicaMedidaAlvo = entity0.INDICA_MEDIDA_ALVO
                , LimiteInferior = entity0.LIMITE_INFERIOR
                , LimiteSuperior = entity0.LIMITE_SUPERIOR
                , Rgb = entity0.RGB
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsIndicadorMedidaCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsIndicadorMedida));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_INDICADOR_MEDIDA.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsIndicadorIndiceCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsIndicadorIndice));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_INDICADOR_INDICE.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_INDICADOR_MEDIDA
	            
	            select 1
	            ).Count();	
		
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    
	    [Update()]	
	    //Update TcsIndicadorMedida.
	    public void UpdateTcsIndicadorMedida(TcsIndicadorMedida entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsIndicadorMedida.
	    public void InsertTcsIndicadorMedida(TcsIndicadorMedida entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsIndicadorMedida.
	    public void DeleteTcsIndicadorMedida(TcsIndicadorMedida entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsIndicadorIndice.
	    public void UpdateTcsIndicadorIndice(TcsIndicadorIndice entity)
	    {



	
	        if (entity.TcsIndicadorMedida.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsIndicadorMedida) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsIndicadorMedida); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsIndicadorIndice.
	    public void InsertTcsIndicadorIndice(TcsIndicadorIndice entity)
	    {



	
	        if (entity.TcsIndicadorMedida.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsIndicadorMedida) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsIndicadorMedida);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsIndicadorIndice.
	    public void DeleteTcsIndicadorIndice(TcsIndicadorIndice entity)
	    {



	
	        if (entity.TcsIndicadorMedida.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsIndicadorMedida) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsIndicadorMedida);
	            

	
	        }

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}