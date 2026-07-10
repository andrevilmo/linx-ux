					
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

namespace Linx.Framework.BV.GrupoEconomico
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TBC_GRUPO_ECONOMICO.ID_GPECON", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TbcGrupoEconomico,TbcGrupoEconomico.TcsUsuarioGpecon];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdGpecon];ReadOnly[false];Entities[TBC_GRUPO_ECONOMICO:IdGpecon];SubQueryInfo[];EdmEntityName[TBC_GRUPO_ECONOMICO];EntityRelations[GPECON_SUPERIOR(TBC_GRUPO_ECONOMICO)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TbcGrupoEconomico")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.GrupoEconomico.TbcGrupoEconomico")]
	public partial class TbcGrupoEconomico : Linx.Data.Entity
	{

	

	    public TbcGrupoEconomico() : this(true) { }

	    public TbcGrupoEconomico(bool setDefaults) 
	    {

	        if (setDefaults)
	        {
	        	        FatorCambio = 1;
	        	        IndicaMoedaForte = true;
	        }	

	    }

			
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.TcsUsuarioGpeconList != null && this.TcsUsuarioGpeconList.Count() > 0)
	      {
	         foreach (var entity in this.TcsUsuarioGpeconList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.TcsUsuarioGpeconList != null)
	      {
	         foreach (var detail in this.TcsUsuarioGpeconList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsUsuarioGpeconList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(GrupoEconomicoDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("TcsUsuarioGpecon"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsUsuarioGpecon");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdParentGpecon"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdGpecon));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsUsuarioGpecon and all sub-details
	         if (this.TcsUsuarioGpeconList == null || this.TcsUsuarioGpeconList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsUsuarioGpeconList = context.GetPagedTcsUsuarioGpecon(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsUsuarioGpeconList = (from r in context.GetTcsUsuarioGpeconByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _TcsUsuarioGpeconElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioGpecon && ((TcsUsuarioGpecon)e.Entity).TbcGrupoEconomico == null && e.Associations == null && e.OriginalAssociations == null && ((TcsUsuarioGpecon)e.Entity).IdParentGpecon == this.IdGpecon).ToList();
 	      if (_TcsUsuarioGpeconElements.Count > 0 && this.TcsUsuarioGpeconList.Count() == 0)
 	      {
 	          this.TcsUsuarioGpeconList = _TcsUsuarioGpeconElements.Select(e => (TcsUsuarioGpecon)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsUsuarioGpeconElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsUsuarioGpecon)detail.Entity).TbcGrupoEconomico = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TbcGrupoEconomico", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsUsuarioGpeconList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For DescGrupoEconomico
	    partial void OnDescGrupoEconomicoChanging(System.String value);
	    partial void OnDescGrupoEconomicoChanged();

	    private System.String _DescGrupoEconomico;

	    [DataMember(IsRequired = true, Name = "DescGrupoEconomico", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBC_GRUPO_ECONOMICO.DESC_GRUPO_ECONOMICO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_GRUPO_ECONOMICO.DESC_GRUPO_ECONOMICO")]
	    public System.String DescGrupoEconomico
	    {
	    	    get
	    	    {
	    	          return _DescGrupoEconomico;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescGrupoEconomico != value)
	    	          {
	    	              this.ValidateProperty("DescGrupoEconomico", value);
	    	              this.OnDescGrupoEconomicoChanging(value);
	    	              this.RaiseDataMemberChanging("DescGrupoEconomico");
	    	              this._DescGrupoEconomico = value;
	    	              this.RaiseDataMemberChanged("DescGrupoEconomico");
	    	              this.OnDescGrupoEconomicoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For FatorCambio
	    partial void OnFatorCambioChanging(Byte value);
	    partial void OnFatorCambioChanged();

	    private Byte _FatorCambio;

	    [DataMember(IsRequired = true, Name = "FatorCambio", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Fator do Câmbio", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[1];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBC_GRUPO_ECONOMICO.FATOR_CAMBIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_GRUPO_ECONOMICO.FATOR_CAMBIO")]
	    public Byte FatorCambio
	    {
	    	    get
	    	    {
	    	          return _FatorCambio;
	    	    }
	    	    set
	    	    {
	    	          if (this._FatorCambio != value)
	    	          {
	    	              this.ValidateProperty("FatorCambio", value);
	    	              this.OnFatorCambioChanging(value);
	    	              this.RaiseDataMemberChanging("FatorCambio");
	    	              this._FatorCambio = value;
	    	              this.RaiseDataMemberChanged("FatorCambio");
	    	              this.OnFatorCambioChanged();
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
	    [Display(Name = "Id Gpecon", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBC_GRUPO_ECONOMICO.ID_GPECON];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_GRUPO_ECONOMICO.ID_GPECON")]
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
	    //Extensibility Partial Method Definitions For IdMoedaIndicador
	    partial void OnIdMoedaIndicadorChanging(System.Nullable<System.Int16> value);
	    partial void OnIdMoedaIndicadorChanged();

	    private System.Nullable<System.Int16> _IdMoedaIndicador;

	    [DataMember(Name = "IdMoedaIndicador", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Moeda Indicador", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsMoedaIndicador];LookUpTitle[Seleção de (Id Moeda Indicador)];LookUpQuery[executeLookUpTcsMoedaIndicador];LookUpFinalize[finalizeLookUpTcsMoedaIndicador];LookUpDisplayColumns[{\"IdMoedaIndicador\" : \"Id Moeda Indicador\", \"NomeMoeda\" : \"Moeda\"}];LookUpColumns[{\"IdMoedaIndicador\" : false, \"NomeMoeda\" : true}];FilterDataKey[TBC_GRUPO_ECONOMICO.ID_MOEDA_INDICADOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int16#IdMoedaIndicador#true##6:0##Id Moeda Indicador#0#false##::LookUpTcsMoedaIndicador##false#false#TCS_MOEDA_INDICADOR#TCS_MOEDA_INDICADOR#Linx.Framework.BV.GrupoEconomico#IQueryable###true#false", EdmKey="TBC_GRUPO_ECONOMICO.ID_MOEDA_INDICADOR")]
	    public System.Nullable<System.Int16> IdMoedaIndicador
	    {
	    	    get
	    	    {
	    	          return _IdMoedaIndicador;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdMoedaIndicador != value)
	    	          {
	    	              this.ValidateProperty("IdMoedaIndicador", value);
	    	              this.OnIdMoedaIndicadorChanging(value);
	    	              this.RaiseDataMemberChanging("IdMoedaIndicador");
	    	              this._IdMoedaIndicador = value;
	    	              this.RaiseDataMemberChanged("IdMoedaIndicador");
	    	              this.OnIdMoedaIndicadorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IndicaMoedaForte
	    partial void OnIndicaMoedaForteChanging(Boolean value);
	    partial void OnIndicaMoedaForteChanged();

	    private Boolean _IndicaMoedaForte;

	    [DataMember(IsRequired = true, Name = "IndicaMoedaForte", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Moeda Forte", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[true];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBC_GRUPO_ECONOMICO.INDICA_MOEDA_FORTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_GRUPO_ECONOMICO.INDICA_MOEDA_FORTE")]
	    public Boolean IndicaMoedaForte
	    {
	    	    get
	    	    {
	    	          return _IndicaMoedaForte;
	    	    }
	    	    set
	    	    {
	    	          if (this._IndicaMoedaForte != value)
	    	          {
	    	              this.ValidateProperty("IndicaMoedaForte", value);
	    	              this.OnIndicaMoedaForteChanging(value);
	    	              this.RaiseDataMemberChanging("IndicaMoedaForte");
	    	              this._IndicaMoedaForte = value;
	    	              this.RaiseDataMemberChanged("IndicaMoedaForte");
	    	              this.OnIndicaMoedaForteChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIdGpecon;
	    [DataMember(Name = "TemporaryIdGpecon", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Gpecon (Tmp)", Description="Temporary Key", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdGpecon
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdGpecon.IsNullOrEmpty())
	    	                this._TemporaryIdGpecon = this._IdGpecon;
	    	          return this._TemporaryIdGpecon;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdGpecon != value)
	    	              this._TemporaryIdGpecon = value;
	    	    }
	    }	

	    #endregion Data Properties

	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<TcsUsuarioGpecon> _TcsUsuarioGpeconList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TbcGrupoEconomico_TcsUsuarioGpecon", "IdGpecon", "IdParentGpecon", IsForeignKey=false)]
	    [DataMember(Name = "TcsUsuarioGpeconList", EmitDefaultValue = true)]
	    public IEnumerable<TcsUsuarioGpecon> TcsUsuarioGpeconList
	    {
	        get
	        {
	
	            if (this._TcsUsuarioGpeconList == null)
	            	this._TcsUsuarioGpeconList = new List<TcsUsuarioGpecon>();
	
	            return this._TcsUsuarioGpeconList;
	        }
	        set
	        {
	            if (this._TcsUsuarioGpeconList != value)
	            {
	                this._TcsUsuarioGpeconList = value;
	                this.RaisePropertyChanged("TcsUsuarioGpeconList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TBC_GRUPO_ECONOMICO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TBC_GRUPO_ECONOMICO), QualifiedEntitySetName = "ControleSistemaContext.TBC_GRUPO_ECONOMICO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TBC_GRUPO_ECONOMICO.ID_GPECON", Source = "IdGpecon", Target = "ID_GPECON", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TBC_GRUPO_ECONOMICO", RelationPropertyName = "TBC_GRUPO_ECONOMICO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TBC_GRUPO_ECONOMICO.FATOR_CAMBIO", Source = "FatorCambio", Target = "FATOR_CAMBIO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TBC_GRUPO_ECONOMICO", RelationPropertyName = "TBC_GRUPO_ECONOMICO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TBC_GRUPO_ECONOMICO.ID_MOEDA_INDICADOR", Source = "IdMoedaIndicador", Target = "ID_MOEDA_INDICADOR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TBC_GRUPO_ECONOMICO", RelationPropertyName = "TBC_GRUPO_ECONOMICO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TBC_GRUPO_ECONOMICO.INDICA_MOEDA_FORTE", Source = "IndicaMoedaForte", Target = "INDICA_MOEDA_FORTE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TBC_GRUPO_ECONOMICO", RelationPropertyName = "TBC_GRUPO_ECONOMICO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TBC_GRUPO_ECONOMICO.DESC_GRUPO_ECONOMICO", Source = "DescGrupoEconomico", Target = "DESC_GRUPO_ECONOMICO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TBC_GRUPO_ECONOMICO", RelationPropertyName = "TBC_GRUPO_ECONOMICO" });

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

		

	[LinxPublicationView(PrimaryKeys="TCS_USUARIO_GPECON.ID_USUARIO_GPECON", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Usuários];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdUsuarioGpecon];ReadOnly[false];Entities[TCS_USUARIO_GPECON:IdUsuarioGpecon];SubQueryInfo[Select 1 From #ParentAlias#.TCS_USUARIO_GPECON_LISTA as #Alias#];EdmEntityName[TCS_USUARIO_GPECON];EntityRelations[TCS_USUARIO(TCS_USUARIO)#TBC_GRUPO_ECONOMICO(TBC_GRUPO_ECONOMICO)#GPECON_SUPERIOR(TBC_GRUPO_ECONOMICO)];EdmParentEntityName[TBC_GRUPO_ECONOMICO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioGpecon")]
	[Serializable()]
	public partial class TcsUsuarioGpecon : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(GrupoEconomicoDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TbcGrupoEconomico");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdGpecon"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdParentGpecon));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TbcGrupoEconomico
	         this.TbcGrupoEconomico = (from r in context.GetTbcGrupoEconomicoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For IdParentGpecon
	    partial void OnIdParentGpeconChanging(Int32 value);
	    partial void OnIdParentGpeconChanged();

	    private Int32 _IdParentGpecon;

	    [DataMember(IsRequired = true, Name = "IdParentGpecon", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Gpecon", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_GPECON.TBC_GRUPO_ECONOMICO.ID_GPECON];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_GPECON.TBC_GRUPO_ECONOMICO.ID_GPECON")]
	    public Int32 IdParentGpecon
	    {
	    	    get
	    	    {
	    	          return _IdParentGpecon;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdParentGpecon != value)
	    	          {
	    	              this.ValidateProperty("IdParentGpecon", value);
	    	              this.OnIdParentGpeconChanging(value);
	    	              this.RaiseDataMemberChanging("IdParentGpecon");
	    	              this._IdParentGpecon = value;
	    	              this.RaiseDataMemberChanged("IdParentGpecon");
	    	              this.OnIdParentGpeconChanged();
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
	    [Display(Name = "Id Usuario", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuario];LookUpTitle[Seleção de (Id Usuario)];LookUpQuery[executeLookUpTcsUsuario];LookUpFinalize[finalizeLookUpTcsUsuario];LookUpDisplayColumns[{\"NomeUsuario\" : \"Nome\", \"UidUsuario\" : \"Uid Usuario\", \"IdUsuario\" : \"Id Usuario\"}];LookUpColumns[{\"NomeUsuario\" : true, \"UidUsuario\" : true, \"IdUsuario\" : true}];FilterDataKey[TCS_USUARIO_GPECON.TCS_USUARIO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdUsuario#true##24:0##Id Usuario#2#true##::LookUpTcsUsuario##false#false#TCS_USUARIO#TCS_USUARIO#Linx.Framework.BV.GrupoEconomico#IQueryable###true#false", EdmKey="TCS_USUARIO_GPECON.TCS_USUARIO.ID_USUARIO")]
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
	    //Extensibility Partial Method Definitions For IdUsuarioGpecon
	    partial void OnIdUsuarioGpeconChanging(Int32 value);
	    partial void OnIdUsuarioGpeconChanged();

	    private Int32 _IdUsuarioGpecon;

	    [DataMember(IsRequired = true, Name = "IdUsuarioGpecon", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario Gpecon", Description="", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_GPECON.ID_USUARIO_GPECON];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_GPECON.ID_USUARIO_GPECON")]
	    public Int32 IdUsuarioGpecon
	    {
	    	    get
	    	    {
	    	          return _IdUsuarioGpecon;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdUsuarioGpecon != value)
	    	          {
	    	              this.ValidateProperty("IdUsuarioGpecon", value);
	    	              this.OnIdUsuarioGpeconChanging(value);
	    	              this.RaiseDataMemberChanging("IdUsuarioGpecon");
	    	              this._IdUsuarioGpecon = value;
	    	              this.RaiseDataMemberChanged("IdUsuarioGpecon");
	    	              this.OnIdUsuarioGpeconChanged();
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
	    [Display(Name = "Nome", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuario];LookUpTitle[Seleção de (Nome)];LookUpQuery[executeLookUpTcsUsuario];LookUpFinalize[finalizeLookUpTcsUsuario];LookUpDisplayColumns[{\"NomeUsuario\" : \"Nome\", \"UidUsuario\" : \"Uid Usuario\", \"IdUsuario\" : \"Id Usuario\"}];LookUpColumns[{\"NomeUsuario\" : true, \"UidUsuario\" : true, \"IdUsuario\" : true}];FilterDataKey[TCS_USUARIO_GPECON.TCS_USUARIO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeUsuario#false##250:0##Nome#0#true##::LookUpTcsUsuario##false#false#TCS_USUARIO#TCS_USUARIO#Linx.Framework.BV.GrupoEconomico#IQueryable###true#false", EdmKey="TCS_USUARIO_GPECON.TCS_USUARIO.NOME_USUARIO")]
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
	    //Extensibility Partial Method Definitions For UidUsuario
	    partial void OnUidUsuarioChanging(System.Guid value);
	    partial void OnUidUsuarioChanged();

	    private System.Guid _UidUsuario;

	    [DataMember(IsRequired = true, Name = "UidUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Usuario", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuario];LookUpTitle[Seleção de (Uid Usuario)];LookUpQuery[executeLookUpTcsUsuario];LookUpFinalize[finalizeLookUpTcsUsuario];LookUpDisplayColumns[{\"NomeUsuario\" : \"Nome\", \"UidUsuario\" : \"Uid Usuario\", \"IdUsuario\" : \"Id Usuario\"}];LookUpColumns[{\"NomeUsuario\" : true, \"UidUsuario\" : true, \"IdUsuario\" : true}];FilterDataKey[TCS_USUARIO_GPECON.TCS_USUARIO.UID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Guid#UidUsuario#false##12:0##Uid Usuario#1#true##::LookUpTcsUsuario##false#false#TCS_USUARIO#TCS_USUARIO#Linx.Framework.BV.GrupoEconomico#IQueryable###true#false", EdmKey="TCS_USUARIO_GPECON.TCS_USUARIO.UID_USUARIO")]
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

	    private Int32 _TemporaryIdUsuarioGpecon;
	    [DataMember(Name = "TemporaryIdUsuarioGpecon", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario Gpecon (Tmp)", Description="Temporary Key", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdUsuarioGpecon
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdUsuarioGpecon.IsNullOrEmpty())
	    	                this._TemporaryIdUsuarioGpecon = this._IdUsuarioGpecon;
	    	          return this._TemporaryIdUsuarioGpecon;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdUsuarioGpecon != value)
	    	              this._TemporaryIdUsuarioGpecon = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TbcGrupoEconomico _TbcGrupoEconomico;
	    [DataMember(Name = "TbcGrupoEconomico", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TbcGrupoEconomico_TcsUsuarioGpecon", "IdParentGpecon", "IdGpecon", IsForeignKey=true)]
	    public TbcGrupoEconomico TbcGrupoEconomico
	    {
	        get
	        {
	            return this._TbcGrupoEconomico;
	        }
	        set
	        {
	            if (this._TbcGrupoEconomico != value)
	            {
	                this._TbcGrupoEconomico = value;
	                this.RaisePropertyChanged("TbcGrupoEconomicoList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_USUARIO_GPECON").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_USUARIO_GPECON), QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_GPECON" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_GPECON.ID_USUARIO_GPECON", Source = "IdUsuarioGpecon", Target = "ID_USUARIO_GPECON", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_GPECON", RelationPropertyName = "TCS_USUARIO_GPECON" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_GPECON.TCS_USUARIO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_GPECON.TBC_GRUPO_ECONOMICO.ID_GPECON", Source = "IdParentGpecon", Target = "ID_GPECON", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TBC_GRUPO_ECONOMICO", RelationPropertyName = "TBC_GRUPO_ECONOMICO" });

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

		

	[LinxPublicationView(PrimaryKeys="TBC_GRUPO_ECONOMICO.ID_GPECON", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[EconomicGroupView];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdGpecon];ReadOnly[false];Entities[TBC_GRUPO_ECONOMICO:IdGpecon];SubQueryInfo[];EdmEntityName[TBC_GRUPO_ECONOMICO];EntityRelations[GPECON_SUPERIOR(TBC_GRUPO_ECONOMICO)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "EconomicGroupView")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.GrupoEconomico.EconomicGroupView")]
	public partial class EconomicGroupView : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For DescGrupoEconomico
	    partial void OnDescGrupoEconomicoChanging(System.String value);
	    partial void OnDescGrupoEconomicoChanged();

	    private System.String _DescGrupoEconomico;

	    [DataMember(IsRequired = true, Name = "DescGrupoEconomico", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBC_GRUPO_ECONOMICO.DESC_GRUPO_ECONOMICO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_GRUPO_ECONOMICO.DESC_GRUPO_ECONOMICO")]
	    public System.String DescGrupoEconomico
	    {
	    	    get
	    	    {
	    	          return _DescGrupoEconomico;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescGrupoEconomico != value)
	    	          {
	    	              this.ValidateProperty("DescGrupoEconomico", value);
	    	              this.OnDescGrupoEconomicoChanging(value);
	    	              this.RaiseDataMemberChanging("DescGrupoEconomico");
	    	              this._DescGrupoEconomico = value;
	    	              this.RaiseDataMemberChanged("DescGrupoEconomico");
	    	              this.OnDescGrupoEconomicoChanged();
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
	    [Display(Name = "Id Gpecon", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBC_GRUPO_ECONOMICO.ID_GPECON];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_GRUPO_ECONOMICO.ID_GPECON")]
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
	    //Extensibility Partial Method Definitions For IndicaGpeconMaster
	    partial void OnIndicaGpeconMasterChanging(System.String value);
	    partial void OnIndicaGpeconMasterChanged();

	    private System.String _IndicaGpeconMaster;

	    [DataMember(Name = "IndicaGpeconMaster", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Indica Gpecon Master", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBC_GRUPO_ECONOMICO.GPECON_SUPERIOR.INDICA_GPECON_MASTER];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_GRUPO_ECONOMICO.GPECON_SUPERIOR.INDICA_GPECON_MASTER")]
	    public System.String IndicaGpeconMaster
	    {
	    	    get
	    	    {
	    	          return _IndicaGpeconMaster;
	    	    }
	    	    set
	    	    {
	    	          if (this._IndicaGpeconMaster != value)
	    	          {
	    	              this.ValidateProperty("IndicaGpeconMaster", value);
	    	              this.OnIndicaGpeconMasterChanging(value);
	    	              this.RaiseDataMemberChanging("IndicaGpeconMaster");
	    	              this._IndicaGpeconMaster = value;
	    	              this.RaiseDataMemberChanged("IndicaGpeconMaster");
	    	              this.OnIndicaGpeconMasterChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIdGpecon;
	    [DataMember(Name = "TemporaryIdGpecon", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Gpecon (Tmp)", Description="Temporary Key", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdGpecon
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdGpecon.IsNullOrEmpty())
	    	                this._TemporaryIdGpecon = this._IdGpecon;
	    	          return this._TemporaryIdGpecon;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdGpecon != value)
	    	              this._TemporaryIdGpecon = value;
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TBC_GRUPO_ECONOMICO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TBC_GRUPO_ECONOMICO), QualifiedEntitySetName = "ControleSistemaContext.TBC_GRUPO_ECONOMICO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TBC_GRUPO_ECONOMICO.ID_GPECON", Source = "IdGpecon", Target = "ID_GPECON", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TBC_GRUPO_ECONOMICO", RelationPropertyName = "TBC_GRUPO_ECONOMICO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TBC_GRUPO_ECONOMICO.DESC_GRUPO_ECONOMICO", Source = "DescGrupoEconomico", Target = "DESC_GRUPO_ECONOMICO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TBC_GRUPO_ECONOMICO", RelationPropertyName = "TBC_GRUPO_ECONOMICO" });

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

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Usuários];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdUsuarioGpecon];ReadOnly[false];Entities[TCS_USUARIO_GPECON:IdUsuarioGpecon];SubQueryInfo[Select 1 From #ParentAlias#.TCS_USUARIO_GPECON_LISTA as #Alias#];EdmEntityName[TCS_USUARIO_GPECON];EntityRelations[TCS_USUARIO(TCS_USUARIO)#TBC_GRUPO_ECONOMICO(TBC_GRUPO_ECONOMICO)#GPECON_SUPERIOR(TBC_GRUPO_ECONOMICO)];EdmParentEntityName[TBC_GRUPO_ECONOMICO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioGpecon")]
	[Serializable()]
	public partial class TcsUsuarioGpeconParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For IdParentGpecon
	    partial void OnIdParentGpeconChanging(Int32 value);
	    partial void OnIdParentGpeconChanged();

	    private Int32 _IdParentGpecon;

	    [DataMember(IsRequired = true, Name = "IdParentGpecon", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Gpecon", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_GPECON.TBC_GRUPO_ECONOMICO.ID_GPECON];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_GPECON.TBC_GRUPO_ECONOMICO.ID_GPECON")]
	    public Int32 IdParentGpecon
	    {
	    	    get
	    	    {
	    	          return _IdParentGpecon;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdParentGpecon != value)
	    	          {
	    	              this.ValidateProperty("IdParentGpecon", value);
	    	              this.OnIdParentGpeconChanging(value);
	    	              this.RaiseDataMemberChanging("IdParentGpecon");
	    	              this._IdParentGpecon = value;
	    	              this.RaiseDataMemberChanged("IdParentGpecon");
	    	              this.OnIdParentGpeconChanged();
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
	    [Display(Name = "Id Usuario", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuario];LookUpTitle[Seleção de (Id Usuario)];LookUpQuery[executeLookUpTcsUsuario];LookUpFinalize[finalizeLookUpTcsUsuario];LookUpDisplayColumns[{\"NomeUsuario\" : \"Nome\", \"UidUsuario\" : \"Uid Usuario\", \"IdUsuario\" : \"Id Usuario\"}];LookUpColumns[{\"NomeUsuario\" : true, \"UidUsuario\" : true, \"IdUsuario\" : true}];FilterDataKey[TCS_USUARIO_GPECON.TCS_USUARIO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdUsuario#true##24:0##Id Usuario#2#true##::LookUpTcsUsuario##false#false#TCS_USUARIO#TCS_USUARIO#Linx.Framework.BV.GrupoEconomico#IQueryable###true#false", EdmKey="TCS_USUARIO_GPECON.TCS_USUARIO.ID_USUARIO")]
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
	    //Extensibility Partial Method Definitions For IdUsuarioGpecon
	    partial void OnIdUsuarioGpeconChanging(Int32 value);
	    partial void OnIdUsuarioGpeconChanged();

	    private Int32 _IdUsuarioGpecon;

	    [DataMember(IsRequired = true, Name = "IdUsuarioGpecon", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario Gpecon", Description="", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_GPECON.ID_USUARIO_GPECON];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_GPECON.ID_USUARIO_GPECON")]
	    public Int32 IdUsuarioGpecon
	    {
	    	    get
	    	    {
	    	          return _IdUsuarioGpecon;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdUsuarioGpecon != value)
	    	          {
	    	              this.ValidateProperty("IdUsuarioGpecon", value);
	    	              this.OnIdUsuarioGpeconChanging(value);
	    	              this.RaiseDataMemberChanging("IdUsuarioGpecon");
	    	              this._IdUsuarioGpecon = value;
	    	              this.RaiseDataMemberChanged("IdUsuarioGpecon");
	    	              this.OnIdUsuarioGpeconChanged();
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
	    [Display(Name = "Nome", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuario];LookUpTitle[Seleção de (Nome)];LookUpQuery[executeLookUpTcsUsuario];LookUpFinalize[finalizeLookUpTcsUsuario];LookUpDisplayColumns[{\"NomeUsuario\" : \"Nome\", \"UidUsuario\" : \"Uid Usuario\", \"IdUsuario\" : \"Id Usuario\"}];LookUpColumns[{\"NomeUsuario\" : true, \"UidUsuario\" : true, \"IdUsuario\" : true}];FilterDataKey[TCS_USUARIO_GPECON.TCS_USUARIO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeUsuario#false##250:0##Nome#0#true##::LookUpTcsUsuario##false#false#TCS_USUARIO#TCS_USUARIO#Linx.Framework.BV.GrupoEconomico#IQueryable###true#false", EdmKey="TCS_USUARIO_GPECON.TCS_USUARIO.NOME_USUARIO")]
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
	    //Extensibility Partial Method Definitions For UidUsuario
	    partial void OnUidUsuarioChanging(System.Guid value);
	    partial void OnUidUsuarioChanged();

	    private System.Guid _UidUsuario;

	    [DataMember(IsRequired = true, Name = "UidUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Usuario", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuario];LookUpTitle[Seleção de (Uid Usuario)];LookUpQuery[executeLookUpTcsUsuario];LookUpFinalize[finalizeLookUpTcsUsuario];LookUpDisplayColumns[{\"NomeUsuario\" : \"Nome\", \"UidUsuario\" : \"Uid Usuario\", \"IdUsuario\" : \"Id Usuario\"}];LookUpColumns[{\"NomeUsuario\" : true, \"UidUsuario\" : true, \"IdUsuario\" : true}];FilterDataKey[TCS_USUARIO_GPECON.TCS_USUARIO.UID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Guid#UidUsuario#false##12:0##Uid Usuario#1#true##::LookUpTcsUsuario##false#false#TCS_USUARIO#TCS_USUARIO#Linx.Framework.BV.GrupoEconomico#IQueryable###true#false", EdmKey="TCS_USUARIO_GPECON.TCS_USUARIO.UID_USUARIO")]
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
	    //Extensibility Partial Method Definitions For DescGrupoEconomico
	    partial void OnDescGrupoEconomicoChanging(System.String value);
	    partial void OnDescGrupoEconomicoChanged();

	    private System.String _DescGrupoEconomico;

	    [DataMember(IsRequired = true, Name = "DescGrupoEconomico", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_GPECON.TBC_GRUPO_ECONOMICO.DESC_GRUPO_ECONOMICO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_GRUPO_ECONOMICO.DESC_GRUPO_ECONOMICO")]
	    public System.String DescGrupoEconomico
	    {
	    	    get
	    	    {
	    	          return _DescGrupoEconomico;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescGrupoEconomico != value)
	    	          {
	    	              this.ValidateProperty("DescGrupoEconomico", value);
	    	              this.OnDescGrupoEconomicoChanging(value);
	    	              this.RaiseDataMemberChanging("DescGrupoEconomico");
	    	              this._DescGrupoEconomico = value;
	    	              this.RaiseDataMemberChanged("DescGrupoEconomico");
	    	              this.OnDescGrupoEconomicoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For FatorCambio
	    partial void OnFatorCambioChanging(Byte value);
	    partial void OnFatorCambioChanged();

	    private Byte _FatorCambio;

	    [DataMember(IsRequired = true, Name = "FatorCambio", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Fator do Câmbio", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[1];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_GPECON.TBC_GRUPO_ECONOMICO.FATOR_CAMBIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_GRUPO_ECONOMICO.FATOR_CAMBIO")]
	    public Byte FatorCambio
	    {
	    	    get
	    	    {
	    	          return _FatorCambio;
	    	    }
	    	    set
	    	    {
	    	          if (this._FatorCambio != value)
	    	          {
	    	              this.ValidateProperty("FatorCambio", value);
	    	              this.OnFatorCambioChanging(value);
	    	              this.RaiseDataMemberChanging("FatorCambio");
	    	              this._FatorCambio = value;
	    	              this.RaiseDataMemberChanged("FatorCambio");
	    	              this.OnFatorCambioChanged();
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
	    [Display(Name = "Id Gpecon", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_GPECON.TBC_GRUPO_ECONOMICO.ID_GPECON];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_GRUPO_ECONOMICO.ID_GPECON")]
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
	    //Extensibility Partial Method Definitions For IdMoedaIndicador
	    partial void OnIdMoedaIndicadorChanging(System.Nullable<System.Int16> value);
	    partial void OnIdMoedaIndicadorChanged();

	    private System.Nullable<System.Int16> _IdMoedaIndicador;

	    [DataMember(Name = "IdMoedaIndicador", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Moeda Indicador", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_GPECON.TBC_GRUPO_ECONOMICO.ID_MOEDA_INDICADOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_GRUPO_ECONOMICO.ID_MOEDA_INDICADOR")]
	    public System.Nullable<System.Int16> IdMoedaIndicador
	    {
	    	    get
	    	    {
	    	          return _IdMoedaIndicador;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdMoedaIndicador != value)
	    	          {
	    	              this.ValidateProperty("IdMoedaIndicador", value);
	    	              this.OnIdMoedaIndicadorChanging(value);
	    	              this.RaiseDataMemberChanging("IdMoedaIndicador");
	    	              this._IdMoedaIndicador = value;
	    	              this.RaiseDataMemberChanged("IdMoedaIndicador");
	    	              this.OnIdMoedaIndicadorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IndicaMoedaForte
	    partial void OnIndicaMoedaForteChanging(Boolean value);
	    partial void OnIndicaMoedaForteChanged();

	    private Boolean _IndicaMoedaForte;

	    [DataMember(IsRequired = true, Name = "IndicaMoedaForte", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Moeda Forte", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[true];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_GPECON.TBC_GRUPO_ECONOMICO.INDICA_MOEDA_FORTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_GRUPO_ECONOMICO.INDICA_MOEDA_FORTE")]
	    public Boolean IndicaMoedaForte
	    {
	    	    get
	    	    {
	    	          return _IndicaMoedaForte;
	    	    }
	    	    set
	    	    {
	    	          if (this._IndicaMoedaForte != value)
	    	          {
	    	              this.ValidateProperty("IndicaMoedaForte", value);
	    	              this.OnIndicaMoedaForteChanging(value);
	    	              this.RaiseDataMemberChanging("IndicaMoedaForte");
	    	              this._IndicaMoedaForte = value;
	    	              this.RaiseDataMemberChanged("IndicaMoedaForte");
	    	              this.OnIndicaMoedaForteChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_USUARIO_GPECON").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_USUARIO_GPECON), QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_GPECON" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_GPECON.ID_USUARIO_GPECON", Source = "IdUsuarioGpecon", Target = "ID_USUARIO_GPECON", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_GPECON", RelationPropertyName = "TCS_USUARIO_GPECON" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_GPECON.TCS_USUARIO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_GPECON.TBC_GRUPO_ECONOMICO.ID_GPECON", Source = "IdParentGpecon", Target = "ID_GPECON", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TBC_GRUPO_ECONOMICO", RelationPropertyName = "TBC_GRUPO_ECONOMICO" });

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
	[DomainIdentifier("ProcessorOverviewGrupoEconomicoDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class GrupoEconomicoDomainService : DomainService, IDataServiceContext 
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

		
	    public GrupoEconomicoDomainService() : this("", null, null) { }
	    public GrupoEconomicoDomainService(string connectionString) : this(connectionString, null, null) { }
	    public GrupoEconomicoDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public GrupoEconomicoDomainService(Linx.Framework.ControleSistema.BM.ControleSistemaContext dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public GrupoEconomicoDomainService(string connectionString, Linx.Framework.ControleSistema.BM.ControleSistemaContext dataContext, Dictionary<string, string> headers) : base() 
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
 	        var _TbcGrupoEconomicoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TbcGrupoEconomico && e.Entity.GetType().Name == "TbcGrupoEconomico" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _TbcGrupoEconomicoElements)
 	           if (((TbcGrupoEconomico)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 
 	        //Remove inconsistent details
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioGpecon && e.Entity.GetType().Name == "TcsUsuarioGpecon" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
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
	    //Get All LookUpTcsMoedaIndicador.
	    public IQueryable<LookUpTcsMoedaIndicador> GetAllLookUpTcsMoedaIndicador()
	    {
	        return this.GetLookUpTcsMoedaIndicador(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsMoedaIndicador By EntitySearch.
	    public IQueryable<LookUpTcsMoedaIndicador> GetLookUpTcsMoedaIndicadorByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsMoedaIndicador(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsMoedaIndicador.
	    public IQueryable<LookUpTcsMoedaIndicador> GetLookUpTcsMoedaIndicador(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TCS_MOEDA_INDICADOR" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsMoedaIndicador";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsMoedaIndicador));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsMoedaIndicador> query =  
	
	            (from entity in this.DbContext.TCS_MOEDA_INDICADOR.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpTcsMoedaIndicador()		
	            {
	            
                IdMoedaIndicador = entity.ID_MOEDA_INDICADOR
                , NomeMoeda = entity.NOME_MOEDA
	            });

	            
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpTcsUsuario.
	    public IQueryable<LookUpTcsUsuario> GetAllLookUpTcsUsuario()
	    {
	        return this.GetLookUpTcsUsuario(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsUsuario By EntitySearch.
	    public IQueryable<LookUpTcsUsuario> GetLookUpTcsUsuarioByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsUsuario(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsUsuario.
	    public IQueryable<LookUpTcsUsuario> GetLookUpTcsUsuario(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TCS_USUARIO" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsUsuario";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsUsuario));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsUsuario> query =  
	
	            (from entity in this.DbContext.TCS_USUARIO.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpTcsUsuario()		
	            {
	            
                NomeUsuario = entity.NOME_USUARIO
                , UidUsuario = entity.UID_USUARIO
                , IdUsuario = entity.ID_USUARIO
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
	
		

	        if (entityName.InList("Linx.Framework.BV.GrupoEconomico.TbcGrupoEconomico"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TbcGrupoEconomico",
	        			NameSpace = "Linx.Framework.BV.GrupoEconomico",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TbcGrupoEconomico",
	        			ClearMethodName = "ClearTbcGrupoEconomico",
	        			QueryMethodName  = "GetPagedTbcGrupoEconomico",	
	        			CountingMethodName  = "GetTbcGrupoEconomico" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.GrupoEconomico.TbcGrupoEconomico"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.GrupoEconomico.TbcGrupoEconomico"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.GrupoEconomico.TbcGrupoEconomico", "Linx.Framework.BV.GrupoEconomico.TcsUsuarioGpecon"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsUsuarioGpecon" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.GrupoEconomico",
	        			HasQuickSearch = false,
	        			ParentClassName = "TbcGrupoEconomico",	
	        			DisplayName = "Usuários",
	        			ClearMethodName = "ClearTcsUsuarioGpecon" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsUsuarioGpecon" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsUsuarioGpecon" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.GrupoEconomico.TcsUsuarioGpecon"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.GrupoEconomico.TcsUsuarioGpecon" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.GrupoEconomico.EconomicGroupView"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "EconomicGroupView",
	        			NameSpace = "Linx.Framework.BV.GrupoEconomico",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "EconomicGroupView",
	        			ClearMethodName = "ClearEconomicGroupView",
	        			QueryMethodName  = "GetPagedEconomicGroupView",	
	        			CountingMethodName  = "GetEconomicGroupView" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.GrupoEconomico.EconomicGroupView"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.GrupoEconomico.EconomicGroupView"), forceAll: forceAll)
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

         		    return new string[] { "Framework_GrupoEconomicoClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.GrupoEconomicoClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Framework_grupoEconomicoService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.grupoEconomicoService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear TbcGrupoEconomico.
	    public IEnumerable<TbcGrupoEconomico> ClearTbcGrupoEconomico()
	    {
	        List<TbcGrupoEconomico> result = new List<TbcGrupoEconomico>();
	        result.Add(new TbcGrupoEconomico(false));	
			
	        result[0].TcsUsuarioGpeconList = new List<TcsUsuarioGpecon>();
	        ((List<TcsUsuarioGpecon>)result[0].TcsUsuarioGpeconList).Add(new TcsUsuarioGpecon());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsUsuarioGpecon.
	    public IEnumerable<TcsUsuarioGpecon> ClearTcsUsuarioGpecon()
	    {
	        List<TcsUsuarioGpecon> result = new List<TcsUsuarioGpecon>();
	        result.Add(new TcsUsuarioGpecon());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear EconomicGroupView.
	    public IEnumerable<EconomicGroupView> ClearEconomicGroupView()
	    {
	        List<EconomicGroupView> result = new List<EconomicGroupView>();
	        result.Add(new EconomicGroupView());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TbcGrupoEconomico.
	    public IQueryable<TbcGrupoEconomico> GetTbcGrupoEconomico()
	    {




		
	
	        
		
	        
	
	        IQueryable<TbcGrupoEconomico> result = 
	            (from entity0 in this.DbContext.TBC_GRUPO_ECONOMICO
	            
	            	
	            select new TbcGrupoEconomico()		
	            {
	            
                DescGrupoEconomico = entity0.DESC_GRUPO_ECONOMICO
                , FatorCambio = entity0.FATOR_CAMBIO
                , IdGpecon = entity0.ID_GPECON
                , IdMoedaIndicador = entity0.ID_MOEDA_INDICADOR
                , IndicaMoedaForte = entity0.INDICA_MOEDA_FORTE
			
                ,TcsUsuarioGpeconList = 
	                        (from entity1 in entity0.TCS_USUARIO_GPECON_LISTA
                                  let entity1Al2 = entity1.TCS_USUARIO
                                  let entity1Al1 = entity1.TBC_GRUPO_ECONOMICO
	                        
	                        	
	                        select new TcsUsuarioGpecon()
	                        {
	                        
                                IdParentGpecon = entity1Al1.ID_GPECON
                                , IdUsuario = entity1Al2.ID_USUARIO
                                , IdUsuarioGpecon = entity1.ID_USUARIO_GPECON
                                , NomeUsuario = entity1Al2.NOME_USUARIO
                                , UidUsuario = entity1Al2.UID_USUARIO
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsUsuarioGpecon.
	    public IQueryable<TcsUsuarioGpecon> GetTcsUsuarioGpecon()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioGpecon> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_GPECON
                  let entity0Al2 = entity0.TCS_USUARIO
                  let entity0Al1 = entity0.TBC_GRUPO_ECONOMICO
	            
	            	
	            select new TcsUsuarioGpecon()		
	            {
	            
                IdParentGpecon = entity0Al1.ID_GPECON
                , IdUsuario = entity0Al2.ID_USUARIO
                , IdUsuarioGpecon = entity0.ID_USUARIO_GPECON
                , NomeUsuario = entity0Al2.NOME_USUARIO
                , UidUsuario = entity0Al2.UID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TbcGrupoEconomicoNoAssociations.
	    public IQueryable<TbcGrupoEconomico> GetTbcGrupoEconomicoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TbcGrupoEconomico> result = 
	            (from entity0 in this.DbContext.TBC_GRUPO_ECONOMICO
	            
	            	
	            select new TbcGrupoEconomico()		
	            {
	            
                DescGrupoEconomico = entity0.DESC_GRUPO_ECONOMICO
                , FatorCambio = entity0.FATOR_CAMBIO
                , IdGpecon = entity0.ID_GPECON
                , IdMoedaIndicador = entity0.ID_MOEDA_INDICADOR
                , IndicaMoedaForte = entity0.INDICA_MOEDA_FORTE
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioGpeconNoAssociations.
	    public IQueryable<TcsUsuarioGpecon> GetTcsUsuarioGpeconNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioGpecon> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_GPECON
                  let entity0Al2 = entity0.TCS_USUARIO
                  let entity0Al1 = entity0.TBC_GRUPO_ECONOMICO
	            
	            	
	            select new TcsUsuarioGpecon()		
	            {
	            
                IdParentGpecon = entity0Al1.ID_GPECON
                , IdUsuario = entity0Al2.ID_USUARIO
                , IdUsuarioGpecon = entity0.ID_USUARIO_GPECON
                , NomeUsuario = entity0Al2.NOME_USUARIO
                , UidUsuario = entity0Al2.UID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get EconomicGroupView.
	    public IQueryable<EconomicGroupView> GetEconomicGroupView()
	    {




		
	
	        
		
	        
	
	        IQueryable<EconomicGroupView> result = 
	            (from entity0 in this.DbContext.TBC_GRUPO_ECONOMICO
                  let entity0Al1 = entity0.GPECON_SUPERIOR
	            
	            	
	            select new EconomicGroupView()		
	            {
	            
                DescGrupoEconomico = entity0.DESC_GRUPO_ECONOMICO
                , IdGpecon = entity0.ID_GPECON
                , IndicaGpeconMaster = entity0Al1.INDICA_GPECON_MASTER
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get EconomicGroupViewNoAssociations.
	    public IQueryable<EconomicGroupView> GetEconomicGroupViewNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<EconomicGroupView> result = 
	            (from entity0 in this.DbContext.TBC_GRUPO_ECONOMICO
                  let entity0Al1 = entity0.GPECON_SUPERIOR
	            
	            	
	            select new EconomicGroupView()		
	            {
	            
                DescGrupoEconomico = entity0.DESC_GRUPO_ECONOMICO
                , IdGpecon = entity0.ID_GPECON
                , IndicaGpeconMaster = entity0Al1.INDICA_GPECON_MASTER
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	//Add filtering disabled property for TBC_GRUPO_ECONOMICO
	    	string[] bmDisabledTbcGrupoEconomicoList = this.GetEDM().GetFilteringDisabledList("TBC_GRUPO_ECONOMICO");
	    	if (bmDisabledTbcGrupoEconomicoList.Length > 0)
	    	{
	
	    		if (bmDisabledTbcGrupoEconomicoList.Contains("TBC_GRUPO_ECONOMICO.DESC_GRUPO_ECONOMICO"))
	    		{
	    			result.Add("TbcGrupoEconomico|DescGrupoEconomico");
	    			result.Add("TbcGrupoEconomico|TBC_GRUPO_ECONOMICO.DESC_GRUPO_ECONOMICO");
	    		}
	
	    		if (bmDisabledTbcGrupoEconomicoList.Contains("TBC_GRUPO_ECONOMICO.FATOR_CAMBIO"))
	    		{
	    			result.Add("TbcGrupoEconomico|FatorCambio");
	    			result.Add("TbcGrupoEconomico|TBC_GRUPO_ECONOMICO.FATOR_CAMBIO");
	    		}
	
	    		if (bmDisabledTbcGrupoEconomicoList.Contains("TBC_GRUPO_ECONOMICO.ID_GPECON"))
	    		{
	    			result.Add("TbcGrupoEconomico|IdGpecon");
	    			result.Add("TbcGrupoEconomico|TBC_GRUPO_ECONOMICO.ID_GPECON");
	    		}
	
	    		if (bmDisabledTbcGrupoEconomicoList.Contains("TBC_GRUPO_ECONOMICO.ID_MOEDA_INDICADOR"))
	    		{
	    			result.Add("TbcGrupoEconomico|IdMoedaIndicador");
	    			result.Add("TbcGrupoEconomico|TBC_GRUPO_ECONOMICO.ID_MOEDA_INDICADOR");
	    		}
	
	    		if (bmDisabledTbcGrupoEconomicoList.Contains("TBC_GRUPO_ECONOMICO.INDICA_MOEDA_FORTE"))
	    		{
	    			result.Add("TbcGrupoEconomico|IndicaMoedaForte");
	    			result.Add("TbcGrupoEconomico|TBC_GRUPO_ECONOMICO.INDICA_MOEDA_FORTE");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_USUARIO_GPECON
	    	string[] bmDisabledTcsUsuarioGpeconList = this.GetEDM().GetFilteringDisabledList("TCS_USUARIO_GPECON");
	    	if (bmDisabledTcsUsuarioGpeconList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsUsuarioGpeconList.Contains("TCS_USUARIO_GPECON.ID_USUARIO_GPECON"))
	    		{
	    			result.Add("TcsUsuarioGpecon|IdUsuarioGpecon");
	    			result.Add("TcsUsuarioGpecon|TCS_USUARIO_GPECON.ID_USUARIO_GPECON");
	    		}
	    	}
	    	//Add filtering disabled property for TBC_GRUPO_ECONOMICO
	    	string[] bmDisabledEconomicGroupViewList = this.GetEDM().GetFilteringDisabledList("TBC_GRUPO_ECONOMICO");
	    	if (bmDisabledEconomicGroupViewList.Length > 0)
	    	{
	
	    		if (bmDisabledEconomicGroupViewList.Contains("TBC_GRUPO_ECONOMICO.DESC_GRUPO_ECONOMICO"))
	    		{
	    			result.Add("EconomicGroupView|DescGrupoEconomico");
	    			result.Add("EconomicGroupView|TBC_GRUPO_ECONOMICO.DESC_GRUPO_ECONOMICO");
	    		}
	
	    		if (bmDisabledEconomicGroupViewList.Contains("TBC_GRUPO_ECONOMICO.ID_GPECON"))
	    		{
	    			result.Add("EconomicGroupView|IdGpecon");
	    			result.Add("EconomicGroupView|TBC_GRUPO_ECONOMICO.ID_GPECON");
	    		}
	    	}
	    	return result;
	    }

	    #endregion Excluded Filters

	    #region Get By EntitySearchId Methods Definitions.
	
				
	    [Ignore]
	    //Get TbcGrupoEconomico By EntitySearchId.
	    public IQueryable<TbcGrupoEconomico> GetTbcGrupoEconomicoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTbcGrupoEconomicoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioGpecon By EntitySearchId.
	    public IQueryable<TcsUsuarioGpecon> GetTcsUsuarioGpeconByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioGpeconByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TbcGrupoEconomico By EntitySearchId.
	    public IQueryable<TbcGrupoEconomico> GetTbcGrupoEconomicoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTbcGrupoEconomicoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioGpecon By EntitySearchId.
	    public IQueryable<TcsUsuarioGpecon> GetTcsUsuarioGpeconByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioGpeconByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get EconomicGroupView By EntitySearchId.
	    public IQueryable<EconomicGroupView> GetEconomicGroupViewByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetEconomicGroupViewByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get EconomicGroupView By EntitySearchId.
	    public IQueryable<EconomicGroupView> GetEconomicGroupViewByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetEconomicGroupViewByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get TbcGrupoEconomico By Example.
	    [Ignore]
	    public IQueryable<TbcGrupoEconomico> GetTbcGrupoEconomicoByExample(TbcGrupoEconomico entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTbcGrupoEconomicoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsUsuarioGpecon By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioGpecon> GetTcsUsuarioGpeconByExample(TcsUsuarioGpecon entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioGpeconByEntitySearch(queryAnalysis);
	    }
			
	    //Get TbcGrupoEconomico By Example.
	    [Ignore]
	    public IQueryable<TbcGrupoEconomico> GetTbcGrupoEconomicoByExampleNoAssociations(TbcGrupoEconomico entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTbcGrupoEconomicoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsUsuarioGpecon By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioGpecon> GetTcsUsuarioGpeconByExampleNoAssociations(TcsUsuarioGpecon entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioGpeconByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get EconomicGroupView By Example.
	    [Ignore]
	    public IQueryable<EconomicGroupView> GetEconomicGroupViewByExample(EconomicGroupView entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetEconomicGroupViewByEntitySearch(queryAnalysis);
	    }
			
	    //Get EconomicGroupView By Example.
	    [Ignore]
	    public IQueryable<EconomicGroupView> GetEconomicGroupViewByExampleNoAssociations(EconomicGroupView entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetEconomicGroupViewByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public TbcGrupoEconomico GetTbcGrupoEconomicoByKey(Int32 idGpecon)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TbcGrupoEconomico");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdGpecon"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idGpecon));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTbcGrupoEconomicoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsUsuarioGpecon GetTcsUsuarioGpeconByKey(Int32 idUsuarioGpecon)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsUsuarioGpecon");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuarioGpecon"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idUsuarioGpecon));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsUsuarioGpeconByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public EconomicGroupView GetEconomicGroupViewByKey(Int32 idGpecon)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("EconomicGroupView");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdGpecon"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idGpecon));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetEconomicGroupViewByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get TbcGrupoEconomicoByEntitySearch.
	    public IQueryable<TbcGrupoEconomico> GetTbcGrupoEconomicoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TbcGrupoEconomico));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TbcGrupoEconomico> result = 
	            (from entity0 in this.DbContext.TBC_GRUPO_ECONOMICO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TbcGrupoEconomico()		
	            {
	            
                DescGrupoEconomico = entity0.DESC_GRUPO_ECONOMICO
                , FatorCambio = entity0.FATOR_CAMBIO
                , IdGpecon = entity0.ID_GPECON
                , IdMoedaIndicador = entity0.ID_MOEDA_INDICADOR
                , IndicaMoedaForte = entity0.INDICA_MOEDA_FORTE
			
                ,TcsUsuarioGpeconList = 
	                        (from entity1 in entity0.TCS_USUARIO_GPECON_LISTA
                                  let entity1Al2 = entity1.TCS_USUARIO
                                  let entity1Al1 = entity1.TBC_GRUPO_ECONOMICO
	                        
	                        	
	                        select new TcsUsuarioGpecon()
	                        {
	                        
                                IdParentGpecon = entity1Al1.ID_GPECON
                                , IdUsuario = entity1Al2.ID_USUARIO
                                , IdUsuarioGpecon = entity1.ID_USUARIO_GPECON
                                , NomeUsuario = entity1Al2.NOME_USUARIO
                                , UidUsuario = entity1Al2.UID_USUARIO
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioGpeconByEntitySearch.
	    public IQueryable<TcsUsuarioGpecon> GetTcsUsuarioGpeconByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioGpecon));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioGpecon> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_GPECON.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.TCS_USUARIO
                  let entity0Al1 = entity0.TBC_GRUPO_ECONOMICO
	            
	            	
	            select new TcsUsuarioGpecon()		
	            {
	            
                IdParentGpecon = entity0Al1.ID_GPECON
                , IdUsuario = entity0Al2.ID_USUARIO
                , IdUsuarioGpecon = entity0.ID_USUARIO_GPECON
                , NomeUsuario = entity0Al2.NOME_USUARIO
                , UidUsuario = entity0Al2.UID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TbcGrupoEconomicoByEntitySearchNoAssociations.
	    public IQueryable<TbcGrupoEconomico> GetTbcGrupoEconomicoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TbcGrupoEconomico));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TbcGrupoEconomico> result = 
	            (from entity0 in this.DbContext.TBC_GRUPO_ECONOMICO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TbcGrupoEconomico()		
	            {
	            
                DescGrupoEconomico = entity0.DESC_GRUPO_ECONOMICO
                , FatorCambio = entity0.FATOR_CAMBIO
                , IdGpecon = entity0.ID_GPECON
                , IdMoedaIndicador = entity0.ID_MOEDA_INDICADOR
                , IndicaMoedaForte = entity0.INDICA_MOEDA_FORTE
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioGpeconByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioGpecon> GetTcsUsuarioGpeconByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioGpecon));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioGpecon> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_GPECON.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.TCS_USUARIO
                  let entity0Al1 = entity0.TBC_GRUPO_ECONOMICO
	            
	            	
	            select new TcsUsuarioGpecon()		
	            {
	            
                IdParentGpecon = entity0Al1.ID_GPECON
                , IdUsuario = entity0Al2.ID_USUARIO
                , IdUsuarioGpecon = entity0.ID_USUARIO_GPECON
                , NomeUsuario = entity0Al2.NOME_USUARIO
                , UidUsuario = entity0Al2.UID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioGpeconParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioGpeconParentComposition> GetTcsUsuarioGpeconParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "TBC_GRUPO_ECONOMICO", "TCS_USUARIO_GPECON", "TBC_GRUPO_ECONOMICO", typeof(TcsUsuarioGpeconParentComposition));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioGpeconParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_GPECON.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.TCS_USUARIO
                  let entity0Al1 = entity0.TBC_GRUPO_ECONOMICO
	            
	            	
	            select new TcsUsuarioGpeconParentComposition()		
	            {
	            
                IdParentGpecon = entity0Al1.ID_GPECON
                , IdUsuario = entity0Al2.ID_USUARIO
                , IdUsuarioGpecon = entity0.ID_USUARIO_GPECON
                , NomeUsuario = entity0Al2.NOME_USUARIO
                , UidUsuario = entity0Al2.UID_USUARIO
                //TbcGrupoEconomico Properties.
                , DescGrupoEconomico = entity0.TBC_GRUPO_ECONOMICO.DESC_GRUPO_ECONOMICO
                , FatorCambio = entity0.TBC_GRUPO_ECONOMICO.FATOR_CAMBIO
                , IdGpecon = entity0.TBC_GRUPO_ECONOMICO.ID_GPECON
                , IdMoedaIndicador = entity0.TBC_GRUPO_ECONOMICO.ID_MOEDA_INDICADOR
                , IndicaMoedaForte = entity0.TBC_GRUPO_ECONOMICO.INDICA_MOEDA_FORTE
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get EconomicGroupViewByEntitySearch.
	    public IQueryable<EconomicGroupView> GetEconomicGroupViewByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(EconomicGroupView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<EconomicGroupView> result = 
	            (from entity0 in this.DbContext.TBC_GRUPO_ECONOMICO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.GPECON_SUPERIOR
	            
	            	
	            select new EconomicGroupView()		
	            {
	            
                DescGrupoEconomico = entity0.DESC_GRUPO_ECONOMICO
                , IdGpecon = entity0.ID_GPECON
                , IndicaGpeconMaster = entity0Al1.INDICA_GPECON_MASTER
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get EconomicGroupViewByEntitySearchNoAssociations.
	    public IQueryable<EconomicGroupView> GetEconomicGroupViewByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(EconomicGroupView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<EconomicGroupView> result = 
	            (from entity0 in this.DbContext.TBC_GRUPO_ECONOMICO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.GPECON_SUPERIOR
	            
	            	
	            select new EconomicGroupView()		
	            {
	            
                DescGrupoEconomico = entity0.DESC_GRUPO_ECONOMICO
                , IdGpecon = entity0.ID_GPECON
                , IndicaGpeconMaster = entity0Al1.INDICA_GPECON_MASTER
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get PagedTbcGrupoEconomico.
	    public IQueryable<TbcGrupoEconomico> GetPagedTbcGrupoEconomico(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TbcGrupoEconomico));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TbcGrupoEconomico> result = 
	            (from entity0 in this.DbContext.TBC_GRUPO_ECONOMICO.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_GPECON ascending
	            
	            	
	            select new TbcGrupoEconomico()		
	            {
	            
                DescGrupoEconomico = entity0.DESC_GRUPO_ECONOMICO
                , FatorCambio = entity0.FATOR_CAMBIO
                , IdGpecon = entity0.ID_GPECON
                , IdMoedaIndicador = entity0.ID_MOEDA_INDICADOR
                , IndicaMoedaForte = entity0.INDICA_MOEDA_FORTE
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsUsuarioGpecon.
	    public IQueryable<TcsUsuarioGpecon> GetPagedTcsUsuarioGpecon(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioGpecon));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioGpecon> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_GPECON.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.TCS_USUARIO
                  let entity0Al1 = entity0.TBC_GRUPO_ECONOMICO
                orderby entity0.ID_USUARIO_GPECON ascending
	            
	            	
	            select new TcsUsuarioGpecon()		
	            {
	            
                IdParentGpecon = entity0Al1.ID_GPECON
                , IdUsuario = entity0Al2.ID_USUARIO
                , IdUsuarioGpecon = entity0.ID_USUARIO_GPECON
                , NomeUsuario = entity0Al2.NOME_USUARIO
                , UidUsuario = entity0Al2.UID_USUARIO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTbcGrupoEconomicoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TbcGrupoEconomico));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TBC_GRUPO_ECONOMICO.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsUsuarioGpeconCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioGpecon));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_USUARIO_GPECON.Where(dynQuery, parameters.ToArray())
                  let entityAl2 = entity.TCS_USUARIO
                  let entityAl1 = entity.TBC_GRUPO_ECONOMICO
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedEconomicGroupView.
	    public IQueryable<EconomicGroupView> GetPagedEconomicGroupView(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(EconomicGroupView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<EconomicGroupView> result = 
	            (from entity0 in this.DbContext.TBC_GRUPO_ECONOMICO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.GPECON_SUPERIOR
                orderby entity0.ID_GPECON ascending
	            
	            	
	            select new EconomicGroupView()		
	            {
	            
                DescGrupoEconomico = entity0.DESC_GRUPO_ECONOMICO
                , IdGpecon = entity0.ID_GPECON
                , IndicaGpeconMaster = entity0Al1.INDICA_GPECON_MASTER
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetEconomicGroupViewCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(EconomicGroupView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TBC_GRUPO_ECONOMICO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.GPECON_SUPERIOR
	            
	            select 1
	            ).Count();	
		
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    
	    [Update()]	
	    //Update TbcGrupoEconomico.
	    public void UpdateTbcGrupoEconomico(TbcGrupoEconomico entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TbcGrupoEconomico.
	    public void InsertTbcGrupoEconomico(TbcGrupoEconomico entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TbcGrupoEconomico.
	    public void DeleteTbcGrupoEconomico(TbcGrupoEconomico entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsUsuarioGpecon.
	    public void UpdateTcsUsuarioGpecon(TcsUsuarioGpecon entity)
	    {



	
	        if (entity.TbcGrupoEconomico.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TbcGrupoEconomico) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TbcGrupoEconomico); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsUsuarioGpecon.
	    public void InsertTcsUsuarioGpecon(TcsUsuarioGpecon entity)
	    {



	
	        if (entity.TbcGrupoEconomico.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TbcGrupoEconomico) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TbcGrupoEconomico);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsUsuarioGpecon.
	    public void DeleteTcsUsuarioGpecon(TcsUsuarioGpecon entity)
	    {



	
	        if (entity.TbcGrupoEconomico.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TbcGrupoEconomico) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TbcGrupoEconomico);
	            

	
	        }

	
	    }
		
			
	    
	    [Update()]	
	    //Update EconomicGroupView.
	    public void UpdateEconomicGroupView(EconomicGroupView entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert EconomicGroupView.
	    public void InsertEconomicGroupView(EconomicGroupView entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete EconomicGroupView.
	    public void DeleteEconomicGroupView(EconomicGroupView entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}