					
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
using Linx.Demo.BM;

namespace Linx.Demo.BV.MacrosEventosValidacoes
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="Arquivo.EntityUniqueKey", IsUpdatable=false, EdmName="")]
		
	[DataContract(IsReference = false, Name = "Arquivo")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Demo.BV.MacrosEventosValidacoes.Arquivo")]
	public partial class Arquivo 
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
	 


	    private string _NomeArquivo;

	    [DataMember(Name = "NomeArquivo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Arquivo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string NomeArquivo
	    {
	    	    get
	    	    {
	    	          if (_NomeArquivo.IsNullOrEmpty())
	    	             _NomeArquivo =  String.Empty;
	    	          return _NomeArquivo;
	    	    }
	    	    set
	    	    {
	    	          this._NomeArquivo = value;
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

		

	[LinxPublicationView(PrimaryKeys="PAIS.ID_PAIS", IsUpdatable=false, EdmName="Linx.Demo.BM.BMDTesteFrame")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[Pais,Pais.Estado];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[];EdmEntityName[PAIS];EntityRelations[];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "Pais")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Demo.BV.MacrosEventosValidacoes.Pais")]
	public partial class Pais : Linx.Data.Entity
	{

	
		
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.EstadoList != null && this.EstadoList.Count() > 0)
	      {
	         foreach (var entity in this.EstadoList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.EstadoList != null)
	      {
	         foreach (var detail in this.EstadoList)
	         {
	            detail.ResetDetails();
	         }
	         this.EstadoList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(MacrosEventosValidacoesDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("Estado"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("Estado");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdPais"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdPais));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load Estado and all sub-details
	         if (this.EstadoList == null || this.EstadoList.Count() == 0)
	         {
	             if (take > 0)
	                 this.EstadoList = context.GetPagedEstado(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.EstadoList = (from r in context.GetEstadoByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _EstadoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is Estado && ((Estado)e.Entity).Pais == null && e.Associations == null && e.OriginalAssociations == null && ((Estado)e.Entity).IdPais == this.IdPais).ToList();
 	      if (_EstadoElements.Count > 0 && this.EstadoList.Count() == 0)
 	      {
 	          this.EstadoList = _EstadoElements.Select(e => (Estado)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _EstadoElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((Estado)detail.Entity).Pais = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("Pais", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("EstadoList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For ComboboxPais
	    partial void OnComboboxPaisChanging(byte value);
	    partial void OnComboboxPaisChanged();

	    private byte _ComboboxPais;

	    [DataMember(IsRequired = true, Name = "ComboboxPais", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Pais", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_COMBOBOX_PAIS];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[PAIS.COMBOBOX_PAIS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="PAIS.COMBOBOX_PAIS")]
	    public byte ComboboxPais
	    {
	    	    get
	    	    {
	    	          return _ComboboxPais;
	    	    }
	    	    set
	    	    {
	    	          if (this._ComboboxPais != value)
	    	          {
	    	              this.ValidateProperty("ComboboxPais", value);
	    	              this.OnComboboxPaisChanging(value);
	    	              this.RaiseDataMemberChanging("ComboboxPais");
	    	              this._ComboboxPais = value;
	    	              this.RaiseDataMemberChanged("ComboboxPais");
	    	              this.OnComboboxPaisChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DatetimePais
	    partial void OnDatetimePaisChanging(System.Nullable<DateTime> value);
	    partial void OnDatetimePaisChanged();

	    private System.Nullable<DateTime> _DatetimePais;

	    [DataMember(Name = "DatetimePais", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Datetime Pais", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[PAIS.DATETIME_PAIS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="PAIS.DATETIME_PAIS")]
	    public System.Nullable<DateTime> DatetimePais
	    {
	    	    get
	    	    {
	    	          return _DatetimePais;
	    	    }
	    	    set
	    	    {
	    	          if (this._DatetimePais != value)
	    	          {
	    	              this.ValidateProperty("DatetimePais", value);
	    	              this.OnDatetimePaisChanging(value);
	    	              this.RaiseDataMemberChanging("DatetimePais");
	    	              this._DatetimePais = value;
	    	              this.RaiseDataMemberChanged("DatetimePais");
	    	              this.OnDatetimePaisChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DecimalPais
	    partial void OnDecimalPaisChanging(System.Nullable<decimal> value);
	    partial void OnDecimalPaisChanged();

	    private System.Nullable<decimal> _DecimalPais;

	    [DataMember(Name = "DecimalPais", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal Pais", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[PAIS.DECIMAL_PAIS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="PAIS.DECIMAL_PAIS")]
	    public System.Nullable<decimal> DecimalPais
	    {
	    	    get
	    	    {
	    	          return _DecimalPais;
	    	    }
	    	    set
	    	    {
	    	          if (this._DecimalPais != value)
	    	          {
	    	              this.ValidateProperty("DecimalPais", value);
	    	              this.OnDecimalPaisChanging(value);
	    	              this.RaiseDataMemberChanging("DecimalPais");
	    	              this._DecimalPais = value;
	    	              this.RaiseDataMemberChanged("DecimalPais");
	    	              this.OnDecimalPaisChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdPais
	    partial void OnIdPaisChanging(int value);
	    partial void OnIdPaisChanged();

	    private int _IdPais;

	    [DataMember(IsRequired = true, Name = "IdPais", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Pais", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[PAIS.ID_PAIS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="PAIS.ID_PAIS")]
	    public int IdPais
	    {
	    	    get
	    	    {
	    	          return _IdPais;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdPais != value)
	    	          {
	    	              this.ValidateProperty("IdPais", value);
	    	              this.OnIdPaisChanging(value);
	    	              this.RaiseDataMemberChanging("IdPais");
	    	              this._IdPais = value;
	    	              this.RaiseDataMemberChanged("IdPais");
	    	              this.OnIdPaisChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringPais
	    partial void OnStringPaisChanging(string value);
	    partial void OnStringPaisChanged();

	    private string _StringPais;

	    [DataMember(Name = "StringPais", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Pais", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[PAIS.STRING_PAIS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="PAIS.STRING_PAIS")]
	    public string StringPais
	    {
	    	    get
	    	    {
	    	          return _StringPais;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringPais != value)
	    	          {
	    	              this.ValidateProperty("StringPais", value);
	    	              this.OnStringPaisChanging(value);
	    	              this.RaiseDataMemberChanging("StringPais");
	    	              this._StringPais = value;
	    	              this.RaiseDataMemberChanged("StringPais");
	    	              this.OnStringPaisChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<Estado> _EstadoList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_Pais_Estado", "IdPais", "IdPais", IsForeignKey=false)]
	    [DataMember(Name = "EstadoList", EmitDefaultValue = true)]
	    public IEnumerable<Estado> EstadoList
	    {
	        get
	        {
	
	            if (this._EstadoList == null)
	            	this._EstadoList = new List<Estado>();
	
	            return this._EstadoList;
	        }
	        set
	        {
	            if (this._EstadoList != value)
	            {
	                this._EstadoList = value;
	                this.RaisePropertyChanged("EstadoList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "BMDTesteFrame.PAIS").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Demo.BM.PAIS), QualifiedEntitySetName = "BMDTesteFrame.PAIS" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="PAIS.ID_PAIS", Source = "IdPais", Target = "ID_PAIS", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.PAIS", RelationPropertyName = "PAIS" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="PAIS.STRING_PAIS", Source = "StringPais", Target = "STRING_PAIS", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.PAIS", RelationPropertyName = "PAIS" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="PAIS.DECIMAL_PAIS", Source = "DecimalPais", Target = "DECIMAL_PAIS", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.PAIS", RelationPropertyName = "PAIS" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="PAIS.COMBOBOX_PAIS", Source = "ComboboxPais", Target = "COMBOBOX_PAIS", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.PAIS", RelationPropertyName = "PAIS" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="PAIS.DATETIME_PAIS", Source = "DatetimePais", Target = "DATETIME_PAIS", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.PAIS", RelationPropertyName = "PAIS" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 

	    [DataMember()]
	    public string TableMedia { get; set; }	


	    public void SaveMedia(DomainOperation operation)
	    {
	         if (!this.TableMedia.IsNullOrEmpty() && (operation == DomainOperation.Insert || operation == DomainOperation.Update))
	         {
	             Linx.Business.Tools.MediaHelper.SyncMedia("PAIS", this.IdPais, null, this.TableMedia.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(e => new Guid(e)).ToList());
	         }
	         else if (operation == DomainOperation.Delete) {
	             Linx.Business.Tools.MediaHelper.SyncMedia("PAIS", this.IdPais, null, new List<Guid>() { Guid.Empty });
	         }
	    }

	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetComboboxPaisValues()
	    {
	    	    return Linx.Demo.BV.Domains.LX_COMBOBOX_PAIS.GetValues();
	    }
	    private string _comboboxPaisName;
	    [DataMember(IsRequired = false, Name = "ComboboxPaisName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Combobox Pais", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ComboboxPaisName
	    {
	    	    get { if (this.ComboboxPais.IsNull()) { _comboboxPaisName = String.Empty; } else { string key = this.ComboboxPais.ToString(); var dmValues = this.GetComboboxPaisValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _comboboxPaisName) _comboboxPaisName = domainName; } return _comboboxPaisName; } set { _comboboxPaisName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="ESTADO.ID_ESTADO", IsUpdatable=false, EdmName="Linx.Demo.BM.BMDTesteFrame")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Estado];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#.ESTADO_LISTA as #Alias#];EdmEntityName[ESTADO];EntityRelations[PAIS(PAIS)];EdmParentEntityName[PAIS];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "Estado")]
	[Serializable()]
	public partial class Estado : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(MacrosEventosValidacoesDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("Pais");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdPais"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdPais));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load Pais
	         this.Pais = (from r in context.GetPaisByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For ComboboxEstado
	    partial void OnComboboxEstadoChanging(byte value);
	    partial void OnComboboxEstadoChanged();

	    private byte _ComboboxEstado;

	    [DataMember(IsRequired = true, Name = "ComboboxEstado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Estado", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_COMBOBOX_ESTADO];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ESTADO.COMBOBOX_ESTADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ESTADO.COMBOBOX_ESTADO")]
	    public byte ComboboxEstado
	    {
	    	    get
	    	    {
	    	          return _ComboboxEstado;
	    	    }
	    	    set
	    	    {
	    	          if (this._ComboboxEstado != value)
	    	          {
	    	              this.ValidateProperty("ComboboxEstado", value);
	    	              this.OnComboboxEstadoChanging(value);
	    	              this.RaiseDataMemberChanging("ComboboxEstado");
	    	              this._ComboboxEstado = value;
	    	              this.RaiseDataMemberChanged("ComboboxEstado");
	    	              this.OnComboboxEstadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DecimalEstado
	    partial void OnDecimalEstadoChanging(System.Nullable<decimal> value);
	    partial void OnDecimalEstadoChanged();

	    private System.Nullable<decimal> _DecimalEstado;

	    [DataMember(Name = "DecimalEstado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal Estado", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ESTADO.DECIMAL_ESTADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ESTADO.DECIMAL_ESTADO")]
	    public System.Nullable<decimal> DecimalEstado
	    {
	    	    get
	    	    {
	    	          return _DecimalEstado;
	    	    }
	    	    set
	    	    {
	    	          if (this._DecimalEstado != value)
	    	          {
	    	              this.ValidateProperty("DecimalEstado", value);
	    	              this.OnDecimalEstadoChanging(value);
	    	              this.RaiseDataMemberChanging("DecimalEstado");
	    	              this._DecimalEstado = value;
	    	              this.RaiseDataMemberChanged("DecimalEstado");
	    	              this.OnDecimalEstadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdEstado
	    partial void OnIdEstadoChanging(int value);
	    partial void OnIdEstadoChanged();

	    private int _IdEstado;

	    [DataMember(IsRequired = true, Name = "IdEstado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Estado", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ESTADO.ID_ESTADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ESTADO.ID_ESTADO")]
	    public int IdEstado
	    {
	    	    get
	    	    {
	    	          return _IdEstado;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdEstado != value)
	    	          {
	    	              this.ValidateProperty("IdEstado", value);
	    	              this.OnIdEstadoChanging(value);
	    	              this.RaiseDataMemberChanging("IdEstado");
	    	              this._IdEstado = value;
	    	              this.RaiseDataMemberChanged("IdEstado");
	    	              this.OnIdEstadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdPais
	    partial void OnIdPaisChanging(System.Nullable<int> value);
	    partial void OnIdPaisChanged();

	    private System.Nullable<int> _IdPais;

	    [DataMember(Name = "IdPais", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Pais", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ESTADO.PAIS.ID_PAIS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ESTADO.PAIS.ID_PAIS")]
	    public System.Nullable<int> IdPais
	    {
	    	    get
	    	    {
	    	          return _IdPais;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdPais != value)
	    	          {
	    	              this.ValidateProperty("IdPais", value);
	    	              this.OnIdPaisChanging(value);
	    	              this.RaiseDataMemberChanging("IdPais");
	    	              this._IdPais = value;
	    	              this.RaiseDataMemberChanged("IdPais");
	    	              this.OnIdPaisChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private Pais _Pais;
	    [DataMember(Name = "Pais", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_Pais_Estado", "IdPais", "IdPais", IsForeignKey=true)]
	    public Pais Pais
	    {
	        get
	        {
	            return this._Pais;
	        }
	        set
	        {
	            if (this._Pais != value)
	            {
	                this._Pais = value;
	                this.RaisePropertyChanged("PaisList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "BMDTesteFrame.ESTADO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Demo.BM.ESTADO), QualifiedEntitySetName = "BMDTesteFrame.ESTADO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ESTADO.ID_ESTADO", Source = "IdEstado", Target = "ID_ESTADO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.ESTADO", RelationPropertyName = "ESTADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ESTADO.PAIS.ID_PAIS", Source = "IdPais", Target = "ID_PAIS", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BMDTesteFrame.PAIS", RelationPropertyName = "PAIS" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ESTADO.DECIMAL_ESTADO", Source = "DecimalEstado", Target = "DECIMAL_ESTADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.ESTADO", RelationPropertyName = "ESTADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ESTADO.COMBOBOX_ESTADO", Source = "ComboboxEstado", Target = "COMBOBOX_ESTADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.ESTADO", RelationPropertyName = "ESTADO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 

	    [DataMember()]
	    public string TableMedia { get; set; }	


	    public void SaveMedia(DomainOperation operation)
	    {
	         if (!this.TableMedia.IsNullOrEmpty() && (operation == DomainOperation.Insert || operation == DomainOperation.Update))
	         {
	             Linx.Business.Tools.MediaHelper.SyncMedia("ESTADO", this.IdEstado, null, this.TableMedia.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(e => new Guid(e)).ToList());
	         }
	         else if (operation == DomainOperation.Delete) {
	             Linx.Business.Tools.MediaHelper.SyncMedia("ESTADO", this.IdEstado, null, new List<Guid>() { Guid.Empty });
	         }
	    }

	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetComboboxEstadoValues()
	    {
	    	    return Linx.Demo.BV.Domains.LX_COMBOBOX_ESTADO.GetValues();
	    }
	    private string _comboboxEstadoName;
	    [DataMember(IsRequired = false, Name = "ComboboxEstadoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Combobox Estado", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ComboboxEstadoName
	    {
	    	    get { if (this.ComboboxEstado.IsNull()) { _comboboxEstadoName = String.Empty; } else { string key = this.ComboboxEstado.ToString(); var dmValues = this.GetComboboxEstadoValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _comboboxEstadoName) _comboboxEstadoName = domainName; } return _comboboxEstadoName; } set { _comboboxEstadoName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="ValorVendas.EntityUniqueKey", IsUpdatable=false, EdmName="")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[ValorVendas];IsOlap[true];OlapCatalogName[DEV-BI-Omni];CubeName[ITENS];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdBandeiraRede];ReadOnly[false];Entities[:IdBandeiraRede];SubQueryInfo[];EdmEntityName[];EntityRelations[ ];EdmParentEntityName[];IsIQueryable[false]")]
		
	[DataContract(IsReference = false, Name = "ValorVendas")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Demo.BV.MacrosEventosValidacoes.ValorVendas")]
	public partial class ValorVendas : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For Cliente
	    partial void OnClienteChanging(String value);
	    partial void OnClienteChanged();

	    private String _Cliente;

	    [DataMember(Name = "Cliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cliente", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];LookUpName[LookUpEntityAdapter1Cliente];LookUpTitle[Seleção de (Cliente)];LookUpQuery[executeLookUpEntityAdapter1Cliente];LookUpFinalize[finalizeLookUpEntityAdapter1Cliente];LookUpDisplayColumns[{\"Cliente\" : \"Cliente\"}];LookUpColumns[{\"Cliente\" : true}];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="String#Cliente#true##0##Cliente#0#true##::LookUpEntityAdapter1Cliente##false#false#EntityAdapter1_Cliente#EntityAdapter1_Cliente#Linx.Demo.BV.MacrosEventosValidacoes#IQueryable###true#false", EdmKey="")]
	    public String Cliente
	    {
	    	    get
	    	    {
	    	          return _Cliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._Cliente != value)
	    	          {
	    	              this.ValidateProperty("Cliente", value);
	    	              this.OnClienteChanging(value);
	    	              this.RaiseDataMemberChanging("Cliente");
	    	              this._Cliente = value;
	    	              this.RaiseDataMemberChanged("Cliente");
	    	              this.OnClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For CodLoja
	    partial void OnCodLojaChanging(String value);
	    partial void OnCodLojaChanged();

	    private String _CodLoja;

	    [DataMember(Name = "CodLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cod Loja", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];LookUpName[LookUpEntityAdapter1CodLoja];LookUpTitle[Seleção de (Cod Loja)];LookUpQuery[executeLookUpEntityAdapter1CodLoja];LookUpFinalize[finalizeLookUpEntityAdapter1CodLoja];LookUpDisplayColumns[{\"CodLoja\" : \"Cod Loja\"}];LookUpColumns[{\"CodLoja\" : true}];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="String#CodLoja#true##0##Cod Loja#0#true##::LookUpEntityAdapter1CodLoja##false#false#EntityAdapter1_CodLoja#EntityAdapter1_CodLoja#Linx.Demo.BV.MacrosEventosValidacoes#IQueryable###true#false", EdmKey="")]
	    public String CodLoja
	    {
	    	    get
	    	    {
	    	          return _CodLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._CodLoja != value)
	    	          {
	    	              this.ValidateProperty("CodLoja", value);
	    	              this.OnCodLojaChanging(value);
	    	              this.RaiseDataMemberChanging("CodLoja");
	    	              this._CodLoja = value;
	    	              this.RaiseDataMemberChanged("CodLoja");
	    	              this.OnCodLojaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Data
	    partial void OnDataChanging(DateTime value);
	    partial void OnDataChanged();

	    private DateTime _Data;

	    [DataMember(IsRequired = true, Name = "Data", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Data", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];LookUpName[LookUpEntityAdapter1Data];LookUpTitle[Seleção de (Data)];LookUpQuery[executeLookUpEntityAdapter1Data];LookUpFinalize[finalizeLookUpEntityAdapter1Data];LookUpDisplayColumns[{\"Data\" : \"Data\"}];LookUpColumns[{\"Data\" : true}];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="DateTime#Data#true##0##Data#0#true##::LookUpEntityAdapter1Data##false#false#EntityAdapter1_Data#EntityAdapter1_Data#Linx.Demo.BV.MacrosEventosValidacoes#IQueryable###true#false", EdmKey="")]
	    public DateTime Data
	    {
	    	    get
	    	    {
	    	          return _Data;
	    	    }
	    	    set
	    	    {
	    	          if (this._Data != value)
	    	          {
	    	              this.ValidateProperty("Data", value);
	    	              this.OnDataChanging(value);
	    	              this.RaiseDataMemberChanging("Data");
	    	              this._Data = value;
	    	              this.RaiseDataMemberChanged("Data");
	    	              this.OnDataChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdBandeiraRede
	    partial void OnIdBandeiraRedeChanging(Int64 value);
	    partial void OnIdBandeiraRedeChanged();

	    private Int64 _IdBandeiraRede;

	    [DataMember(IsRequired = true, Name = "IdBandeiraRede", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Bandeira Rede", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];LookUpName[LookUpEntityAdapter1IdBandeiraRede];LookUpTitle[Seleção de (Id Bandeira Rede)];LookUpQuery[executeLookUpEntityAdapter1IdBandeiraRede];LookUpFinalize[finalizeLookUpEntityAdapter1IdBandeiraRede];LookUpDisplayColumns[{\"IdBandeiraRede\" : \"Id Bandeira Rede\"}];LookUpColumns[{\"IdBandeiraRede\" : true}];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdBandeiraRede#true##0##Id Bandeira Rede#0#true##::LookUpEntityAdapter1IdBandeiraRede##false#false#EntityAdapter1_IdBandeiraRede#EntityAdapter1_IdBandeiraRede#Linx.Demo.BV.MacrosEventosValidacoes#IQueryable###true#false", EdmKey="")]
	    public Int64 IdBandeiraRede
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
	    //Extensibility Partial Method Definitions For Loja
	    partial void OnLojaChanging(String value);
	    partial void OnLojaChanged();

	    private String _Loja;

	    [DataMember(Name = "Loja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Loja", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];LookUpName[LookUpEntityAdapter1Loja];LookUpTitle[Seleção de (Loja)];LookUpQuery[executeLookUpEntityAdapter1Loja];LookUpFinalize[finalizeLookUpEntityAdapter1Loja];LookUpDisplayColumns[{\"Loja\" : \"Loja\"}];LookUpColumns[{\"Loja\" : true}];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="String#Loja#true##0##Loja#0#true##::LookUpEntityAdapter1Loja##false#false#EntityAdapter1_Loja#EntityAdapter1_Loja#Linx.Demo.BV.MacrosEventosValidacoes#IQueryable###true#false", EdmKey="")]
	    public String Loja
	    {
	    	    get
	    	    {
	    	          return _Loja;
	    	    }
	    	    set
	    	    {
	    	          if (this._Loja != value)
	    	          {
	    	              this.ValidateProperty("Loja", value);
	    	              this.OnLojaChanging(value);
	    	              this.RaiseDataMemberChanging("Loja");
	    	              this._Loja = value;
	    	              this.RaiseDataMemberChanged("Loja");
	    	              this.OnLojaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For QtdItemBruto
	    partial void OnQtdItemBrutoChanging(Double value);
	    partial void OnQtdItemBrutoChanged();

	    private Double _QtdItemBruto;

	    [DataMember(IsRequired = true, Name = "QtdItemBruto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Qtd Item Bruto", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[20:2];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[true]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public Double QtdItemBruto
	    {
	    	    get
	    	    {
	    	          return _QtdItemBruto;
	    	    }
	    	    set
	    	    {
	    	          if (this._QtdItemBruto != value)
	    	          {
	    	              this.ValidateProperty("QtdItemBruto", value);
	    	              this.OnQtdItemBrutoChanging(value);
	    	              this.RaiseDataMemberChanging("QtdItemBruto");
	    	              this._QtdItemBruto = value;
	    	              this.RaiseDataMemberChanged("QtdItemBruto");
	    	              this.OnQtdItemBrutoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For VlrItemPago
	    partial void OnVlrItemPagoChanging(Double value);
	    partial void OnVlrItemPagoChanged();

	    private Double _VlrItemPago;

	    [DataMember(IsRequired = true, Name = "VlrItemPago", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Vlr Item Pago", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[20:2];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[true]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public Double VlrItemPago
	    {
	    	    get
	    	    {
	    	          return _VlrItemPago;
	    	    }
	    	    set
	    	    {
	    	          if (this._VlrItemPago != value)
	    	          {
	    	              this.ValidateProperty("VlrItemPago", value);
	    	              this.OnVlrItemPagoChanging(value);
	    	              this.RaiseDataMemberChanging("VlrItemPago");
	    	              this._VlrItemPago = value;
	    	              this.RaiseDataMemberChanged("VlrItemPago");
	    	              this.OnVlrItemPagoChanged();
	    	          }
	    	    }
	    }

	    private Int64 _TemporaryIdBandeiraRede;
	    [DataMember(Name = "TemporaryIdBandeiraRede", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Bandeira Rede (Tmp)", Description="Temporary Key", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdBandeiraRede
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

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Estado];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#.ESTADO_LISTA as #Alias#];EdmEntityName[ESTADO];EntityRelations[PAIS(PAIS)];EdmParentEntityName[PAIS];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "Estado")]
	[Serializable()]
	public partial class EstadoParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For ComboboxEstado
	    partial void OnComboboxEstadoChanging(byte value);
	    partial void OnComboboxEstadoChanged();

	    private byte _ComboboxEstado;

	    [DataMember(IsRequired = true, Name = "ComboboxEstado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Estado", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_COMBOBOX_ESTADO];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ESTADO.COMBOBOX_ESTADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ESTADO.COMBOBOX_ESTADO")]
	    public byte ComboboxEstado
	    {
	    	    get
	    	    {
	    	          return _ComboboxEstado;
	    	    }
	    	    set
	    	    {
	    	          if (this._ComboboxEstado != value)
	    	          {
	    	              this.ValidateProperty("ComboboxEstado", value);
	    	              this.OnComboboxEstadoChanging(value);
	    	              this.RaiseDataMemberChanging("ComboboxEstado");
	    	              this._ComboboxEstado = value;
	    	              this.RaiseDataMemberChanged("ComboboxEstado");
	    	              this.OnComboboxEstadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DecimalEstado
	    partial void OnDecimalEstadoChanging(System.Nullable<decimal> value);
	    partial void OnDecimalEstadoChanged();

	    private System.Nullable<decimal> _DecimalEstado;

	    [DataMember(Name = "DecimalEstado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal Estado", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ESTADO.DECIMAL_ESTADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ESTADO.DECIMAL_ESTADO")]
	    public System.Nullable<decimal> DecimalEstado
	    {
	    	    get
	    	    {
	    	          return _DecimalEstado;
	    	    }
	    	    set
	    	    {
	    	          if (this._DecimalEstado != value)
	    	          {
	    	              this.ValidateProperty("DecimalEstado", value);
	    	              this.OnDecimalEstadoChanging(value);
	    	              this.RaiseDataMemberChanging("DecimalEstado");
	    	              this._DecimalEstado = value;
	    	              this.RaiseDataMemberChanged("DecimalEstado");
	    	              this.OnDecimalEstadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdEstado
	    partial void OnIdEstadoChanging(int value);
	    partial void OnIdEstadoChanged();

	    private int _IdEstado;

	    [DataMember(IsRequired = true, Name = "IdEstado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Estado", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ESTADO.ID_ESTADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ESTADO.ID_ESTADO")]
	    public int IdEstado
	    {
	    	    get
	    	    {
	    	          return _IdEstado;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdEstado != value)
	    	          {
	    	              this.ValidateProperty("IdEstado", value);
	    	              this.OnIdEstadoChanging(value);
	    	              this.RaiseDataMemberChanging("IdEstado");
	    	              this._IdEstado = value;
	    	              this.RaiseDataMemberChanged("IdEstado");
	    	              this.OnIdEstadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdPais
	    partial void OnIdPaisChanging(System.Nullable<int> value);
	    partial void OnIdPaisChanged();

	    private System.Nullable<int> _IdPais;

	    [DataMember(Name = "IdPais", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Pais", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ESTADO.PAIS.ID_PAIS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ESTADO.PAIS.ID_PAIS")]
	    public System.Nullable<int> IdPais
	    {
	    	    get
	    	    {
	    	          return _IdPais;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdPais != value)
	    	          {
	    	              this.ValidateProperty("IdPais", value);
	    	              this.OnIdPaisChanging(value);
	    	              this.RaiseDataMemberChanging("IdPais");
	    	              this._IdPais = value;
	    	              this.RaiseDataMemberChanged("IdPais");
	    	              this.OnIdPaisChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ComboboxPais
	    partial void OnComboboxPaisChanging(byte value);
	    partial void OnComboboxPaisChanged();

	    private byte _ComboboxPais;

	    [DataMember(IsRequired = true, Name = "ComboboxPais", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Pais", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_COMBOBOX_PAIS];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[ESTADO.PAIS.COMBOBOX_PAIS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="PAIS.COMBOBOX_PAIS")]
	    public byte ComboboxPais
	    {
	    	    get
	    	    {
	    	          return _ComboboxPais;
	    	    }
	    	    set
	    	    {
	    	          if (this._ComboboxPais != value)
	    	          {
	    	              this.ValidateProperty("ComboboxPais", value);
	    	              this.OnComboboxPaisChanging(value);
	    	              this.RaiseDataMemberChanging("ComboboxPais");
	    	              this._ComboboxPais = value;
	    	              this.RaiseDataMemberChanged("ComboboxPais");
	    	              this.OnComboboxPaisChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DatetimePais
	    partial void OnDatetimePaisChanging(System.Nullable<DateTime> value);
	    partial void OnDatetimePaisChanged();

	    private System.Nullable<DateTime> _DatetimePais;

	    [DataMember(Name = "DatetimePais", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Datetime Pais", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[ESTADO.PAIS.DATETIME_PAIS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="PAIS.DATETIME_PAIS")]
	    public System.Nullable<DateTime> DatetimePais
	    {
	    	    get
	    	    {
	    	          return _DatetimePais;
	    	    }
	    	    set
	    	    {
	    	          if (this._DatetimePais != value)
	    	          {
	    	              this.ValidateProperty("DatetimePais", value);
	    	              this.OnDatetimePaisChanging(value);
	    	              this.RaiseDataMemberChanging("DatetimePais");
	    	              this._DatetimePais = value;
	    	              this.RaiseDataMemberChanged("DatetimePais");
	    	              this.OnDatetimePaisChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DecimalPais
	    partial void OnDecimalPaisChanging(System.Nullable<decimal> value);
	    partial void OnDecimalPaisChanged();

	    private System.Nullable<decimal> _DecimalPais;

	    [DataMember(Name = "DecimalPais", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal Pais", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[ESTADO.PAIS.DECIMAL_PAIS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="PAIS.DECIMAL_PAIS")]
	    public System.Nullable<decimal> DecimalPais
	    {
	    	    get
	    	    {
	    	          return _DecimalPais;
	    	    }
	    	    set
	    	    {
	    	          if (this._DecimalPais != value)
	    	          {
	    	              this.ValidateProperty("DecimalPais", value);
	    	              this.OnDecimalPaisChanging(value);
	    	              this.RaiseDataMemberChanging("DecimalPais");
	    	              this._DecimalPais = value;
	    	              this.RaiseDataMemberChanged("DecimalPais");
	    	              this.OnDecimalPaisChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringPais
	    partial void OnStringPaisChanging(string value);
	    partial void OnStringPaisChanged();

	    private string _StringPais;

	    [DataMember(Name = "StringPais", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Pais", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[ESTADO.PAIS.STRING_PAIS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="PAIS.STRING_PAIS")]
	    public string StringPais
	    {
	    	    get
	    	    {
	    	          return _StringPais;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringPais != value)
	    	          {
	    	              this.ValidateProperty("StringPais", value);
	    	              this.OnStringPaisChanging(value);
	    	              this.RaiseDataMemberChanging("StringPais");
	    	              this._StringPais = value;
	    	              this.RaiseDataMemberChanged("StringPais");
	    	              this.OnStringPaisChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "BMDTesteFrame.ESTADO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Demo.BM.ESTADO), QualifiedEntitySetName = "BMDTesteFrame.ESTADO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ESTADO.ID_ESTADO", Source = "IdEstado", Target = "ID_ESTADO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.ESTADO", RelationPropertyName = "ESTADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ESTADO.PAIS.ID_PAIS", Source = "IdPais", Target = "ID_PAIS", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BMDTesteFrame.PAIS", RelationPropertyName = "PAIS" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ESTADO.DECIMAL_ESTADO", Source = "DecimalEstado", Target = "DECIMAL_ESTADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.ESTADO", RelationPropertyName = "ESTADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ESTADO.COMBOBOX_ESTADO", Source = "ComboboxEstado", Target = "COMBOBOX_ESTADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.ESTADO", RelationPropertyName = "ESTADO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 

	    [DataMember()]
	    public string TableMedia { get; set; }	


	    public void SaveMedia(DomainOperation operation)
	    {
	         if (!this.TableMedia.IsNullOrEmpty() && (operation == DomainOperation.Insert || operation == DomainOperation.Update))
	         {
	             Linx.Business.Tools.MediaHelper.SyncMedia("ESTADO", this.IdEstado, null, this.TableMedia.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(e => new Guid(e)).ToList());
	         }
	         else if (operation == DomainOperation.Delete) {
	             Linx.Business.Tools.MediaHelper.SyncMedia("ESTADO", this.IdEstado, null, new List<Guid>() { Guid.Empty });
	         }
	    }

	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetComboboxEstadoValues()
	    {
	    	    return Linx.Demo.BV.Domains.LX_COMBOBOX_ESTADO.GetValues();
	    }
	    private string _comboboxEstadoName;
	    [DataMember(IsRequired = false, Name = "ComboboxEstadoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Combobox Estado", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ComboboxEstadoName
	    {
	    	    get { if (this.ComboboxEstado.IsNull()) { _comboboxEstadoName = String.Empty; } else { string key = this.ComboboxEstado.ToString(); var dmValues = this.GetComboboxEstadoValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _comboboxEstadoName) _comboboxEstadoName = domainName; } return _comboboxEstadoName; } set { _comboboxEstadoName = value;  }
	    }
	    public Dictionary<string, string> GetComboboxPaisValues()
	    {
	    	    return Linx.Demo.BV.Domains.LX_COMBOBOX_PAIS.GetValues();
	    }
	    private string _comboboxPaisName;
	    [DataMember(IsRequired = false, Name = "ComboboxPaisName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Combobox Pais", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ComboboxPaisName
	    {
	    	    get { if (this.ComboboxPais.IsNull()) { _comboboxPaisName = String.Empty; } else { string key = this.ComboboxPais.ToString(); var dmValues = this.GetComboboxPaisValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _comboboxPaisName) _comboboxPaisName = domainName; } return _comboboxPaisName; } set { _comboboxPaisName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	//////////////////////// DomainService Class V1 ///////////////////////
	///////////////////////////////////////////////////////////////////////
	[EnableClientAccess()]	
	[DomainIdentifier("ProcessorOverviewMacrosEventosValidacoesDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class MacrosEventosValidacoesDomainService : DomainService, IDataServiceContext 
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
	
	    private Linx.Demo.BM.BMDTesteFrame _dbContext;
	    protected Linx.Demo.BM.BMDTesteFrame DbContext 
	    { 
	    	get 
	    	{
	        	if (this._dbContext == null)
	        	{
	        		this._dbContext = new Linx.Demo.BM.BMDTesteFrame(connectionString, this.Headers);
	        		((System.Data.Entity.Infrastructure.IObjectContextAdapter)this._dbContext).ObjectContext.CommandTimeout = 180;
	        		this._hasGpeconControl = (!(this._dbContext.IsUserMultiGpecon && this._dbContext.IdGpecon == this._dbContext.IdLinx) && this._dbContext.IdGpecon > 0);		
	        	}
	        	return this._dbContext;
	    	}
	    }

	    public string GetModelAssemblyName()
	    {
	        return typeof(Linx.Demo.BM.BMDTesteFrame).Assembly.FullName;
	    }

	    public System.Data.Entity.Database Database
	    {
	        get { return this.DbContext.Database; }
	    }

		
	    public MacrosEventosValidacoesDomainService() : this("", null, null) { }
	    public MacrosEventosValidacoesDomainService(string connectionString) : this(connectionString, null, null) { }
	    public MacrosEventosValidacoesDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public MacrosEventosValidacoesDomainService(Linx.Demo.BM.BMDTesteFrame dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public MacrosEventosValidacoesDomainService(string connectionString, Linx.Demo.BM.BMDTesteFrame dataContext, Dictionary<string, string> headers) : base() 
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
	    public Linx.Demo.BM.BMDTesteFrame GetEDM()
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
	    			if (entry.Entity is Pais) ((Pais)entry.Entity).SaveMedia(entry.Operation);
	    			if (entry.Entity is Estado) ((Estado)entry.Entity).SaveMedia(entry.Operation);
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
 	        var _PaisElements = changeSet.ChangeSetEntries.Where(e => e.Entity is Pais && e.Entity.GetType().Name == "Pais" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _PaisElements)
 	           if (((Pais)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 
 	        //Remove inconsistent details
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is Estado && e.Entity.GetType().Name == "Estado" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
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
	
	
		    
    [Ignore()]
    public IEnumerable<ValorVendas> GetOlapValorVendas(List<EntitySearch> entitySearchList)
    {
       List<MDXField> fieldsMap = new List<MDXField>();
       fieldsMap.Add(new MDXField("Cliente", "[CRM_PFJ].[CLIENTE].[CLIENTE]", false));
       fieldsMap.Add(new MDXField("CodLoja", "[LJV_LOJA].[COD_LOJA].[COD_LOJA]", false));
       fieldsMap.Add(new MDXField("Data", "[DATAS].[DATA].[DATA]", false));
       fieldsMap.Add(new MDXField("IdBandeiraRede", "[TBC_BANDEIRA_REDE].[ID_BANDEIRA_REDE].[ID_BANDEIRA_REDE]", false));
       fieldsMap.Add(new MDXField("Loja", "[LJV_LOJA].[LOJA].[LOJA]", false));
       fieldsMap.Add(new MDXField("QtdItemBruto", "[Measures].[QTD_ITEM_BRUTO]", true));
       fieldsMap.Add(new MDXField("VlrItemPago", "[Measures].[VLR_ITEM_PAGO]", true));
       var fields = entitySearchList.SelectMany(e => e.Expressions).Where(ex => ex.Name == "Field" && ((string)ex.Value ?? "").StartsWith("(PEsp)")).Select(s => ((string)s.Value).Right("(PEsp)"));
       fields.Foreach(i => { fieldsMap.Add(new MDXField(i, i, i.Contains("[Measures]"))); });
       entitySearchList.ForEach(e => e.Expressions.ToList().ForEach(f => { if (f.Name == "Field" && ((string)f.Value).Contains("(PEsp)")) f.Value = ((string)f.Value).Replace("(PEsp)", ""); }));
       //Verify valid properties for LINQ
       string[] validProperties = (entitySearchList == null ? new string[] { } : EntitySearch.GetValidProperties(entitySearchList));
    
       validProperties = EntitySearch.GetLinqValidProperties(validProperties, fieldsMap.ToDictionary(f => f.Name, f => f.MDX));
       MDXQueryFilterBuilder builder = new MDXQueryFilterBuilder(fieldsMap);
       builder.Conditions(entitySearchList);
       string connString = Linx.Business.Tools.CacheAccessHelper.GetConnectionString("DEV-BI-Omni");
       if (connString == "name=DEV-BI-Omni") connString = Linx.Tools.ConnectionManager.GetConnectionString("DEV-BI-Omni");
       using (Microsoft.AnalysisServices.AdomdClient.AdomdConnection connection = new Microsoft.AnalysisServices.AdomdClient.AdomdConnection(connString))
       {
           string mdxScript = (new MDXHelper("ITENS"))
              .Measures("[Measures].[QTD_ITEM_BRUTO]|QtdItemBruto","[Measures].[VLR_ITEM_PAGO]|VlrItemPago")
              .Rows("[CRM_PFJ].[CLIENTE].[CLIENTE]", "[LJV_LOJA].[COD_LOJA].[COD_LOJA]", "[DATAS].[DATA].[DATA]", "[TBC_BANDEIRA_REDE].[ID_BANDEIRA_REDE].[ID_BANDEIRA_REDE]", "[LJV_LOJA].[LOJA].[LOJA]")
              .SetIdLinxDimensions("")
              .SetIdGpeconDimensions("")
              .SetIdBandeiraRedeDimensions("")
              .SetMeasuresDimensions("")
              .Where(builder).FilterMetaData(validProperties)
              .SubqueryFilter("")
              .SetIdGpEcon(CurrentIdGpEcon())
              .SetIdLinx(CurrentIdLinx("DEV-BI-Omni"))
              .GetCommand(new MDXQuerySettings(){ NonEmptyColumns = true, NonEmptyRows = true});
           
           var command = connection.CreateCommand();
           command.Properties.Add("DbpropMsmdFlattened2", true);
           command.CommandText = mdxScript;
           connection.Open();
           IEnumerable<ValorVendas> result = null;
           using (var reader = command.ExecuteReader())
           {
               List<string> columnInReader = new List<string>();
               var dt = reader.GetSchemaTable();
               foreach (DataRow row in dt.Rows) columnInReader.Add(row["ColumnName"].ToString());
               result = reader.Select(r => new ValorVendas {
               Cliente = !columnInReader.Contains("[CRM_PFJ].[CLIENTE].[CLIENTE].[MEMBER_CAPTION]") || (validProperties.Length > 0 && !validProperties.Contains("[CRM_PFJ].[CLIENTE].[CLIENTE]")) || r["[CRM_PFJ].[CLIENTE].[CLIENTE].[MEMBER_CAPTION]"] is DBNull || r["[CRM_PFJ].[CLIENTE].[CLIENTE].[MEMBER_CAPTION]"] == null ? default(String) : ((string)r["[CRM_PFJ].[CLIENTE].[CLIENTE].[MEMBER_CAPTION]"])
               , CodLoja = !columnInReader.Contains("[LJV_LOJA].[COD_LOJA].[COD_LOJA].[MEMBER_CAPTION]") || (validProperties.Length > 0 && !validProperties.Contains("[LJV_LOJA].[COD_LOJA].[COD_LOJA]")) || r["[LJV_LOJA].[COD_LOJA].[COD_LOJA].[MEMBER_CAPTION]"] is DBNull || r["[LJV_LOJA].[COD_LOJA].[COD_LOJA].[MEMBER_CAPTION]"] == null ? default(String) : ((string)r["[LJV_LOJA].[COD_LOJA].[COD_LOJA].[MEMBER_CAPTION]"])
               , Data = !columnInReader.Contains("[DATAS].[DATA].[DATA].[MEMBER_CAPTION]") || (validProperties.Length > 0 && !validProperties.Contains("[DATAS].[DATA].[DATA]")) || r["[DATAS].[DATA].[DATA].[MEMBER_CAPTION]"] is DBNull || r["[DATAS].[DATA].[DATA].[MEMBER_CAPTION]"] == null ? default(DateTime) : DateTime.Parse((string)r["[DATAS].[DATA].[DATA].[MEMBER_CAPTION]"])
               , IdBandeiraRede = !columnInReader.Contains("[TBC_BANDEIRA_REDE].[ID_BANDEIRA_REDE].[ID_BANDEIRA_REDE].[MEMBER_CAPTION]") || (validProperties.Length > 0 && !validProperties.Contains("[TBC_BANDEIRA_REDE].[ID_BANDEIRA_REDE].[ID_BANDEIRA_REDE]")) || r["[TBC_BANDEIRA_REDE].[ID_BANDEIRA_REDE].[ID_BANDEIRA_REDE].[MEMBER_CAPTION]"] is DBNull || r["[TBC_BANDEIRA_REDE].[ID_BANDEIRA_REDE].[ID_BANDEIRA_REDE].[MEMBER_CAPTION]"] == null ? default(Int64) : Int64.Parse((string)r["[TBC_BANDEIRA_REDE].[ID_BANDEIRA_REDE].[ID_BANDEIRA_REDE].[MEMBER_CAPTION]"])
               , Loja = !columnInReader.Contains("[LJV_LOJA].[LOJA].[LOJA].[MEMBER_CAPTION]") || (validProperties.Length > 0 && !validProperties.Contains("[LJV_LOJA].[LOJA].[LOJA]")) || r["[LJV_LOJA].[LOJA].[LOJA].[MEMBER_CAPTION]"] is DBNull || r["[LJV_LOJA].[LOJA].[LOJA].[MEMBER_CAPTION]"] == null ? default(String) : ((string)r["[LJV_LOJA].[LOJA].[LOJA].[MEMBER_CAPTION]"])
               , QtdItemBruto = !columnInReader.Contains("[Measures].[QtdItemBruto]") || (validProperties.Length > 0 && !validProperties.Contains("[Measures].[QTD_ITEM_BRUTO]"))  || r["[Measures].[QtdItemBruto]"] is DBNull || r["[Measures].[QtdItemBruto]"] == null ? default(Double) : System.Convert.ToDouble(r["[Measures].[QtdItemBruto]"]).GetValue()
               , VlrItemPago = !columnInReader.Contains("[Measures].[VlrItemPago]") || (validProperties.Length > 0 && !validProperties.Contains("[Measures].[VLR_ITEM_PAGO]"))  || r["[Measures].[VlrItemPago]"] is DBNull || r["[Measures].[VlrItemPago]"] == null ? default(Double) : System.Convert.ToDouble(r["[Measures].[VlrItemPago]"]).GetValue()
               }).ToList();
           }
           return result;
       }
    }


				
	
		    
    [Ignore()]
    public IEnumerable<LookUpEntityAdapter1Cliente> GetOlapLookUpEntityAdapter1Cliente(List<EntitySearch> entitySearchList)
    {
       List<MDXField> fieldsMap = new List<MDXField>();
       fieldsMap.Add(new MDXField("Cliente", "[CRM_PFJ].[CLIENTE].[CLIENTE]", false));
       string[] validProperties = (entitySearchList == null ? new string[] {} : EntitySearch.GetValidProperties(entitySearchList));
       validProperties = EntitySearch.GetLinqValidProperties(validProperties, fieldsMap.ToDictionary(f => f.Name, f => f.MDX));
       MDXQueryFilterBuilder builder = new MDXQueryFilterBuilder(fieldsMap);
       builder.Conditions(entitySearchList);
       string connString = Linx.Business.Tools.CacheAccessHelper.GetConnectionString("DEV-BI-Omni");
       if (connString == "name=DEV-BI-Omni") connString = Linx.Tools.ConnectionManager.GetConnectionString("DEV-BI-Omni");
       using (Microsoft.AnalysisServices.AdomdClient.AdomdConnection connection = new Microsoft.AnalysisServices.AdomdClient.AdomdConnection(connString))
       {
           string mdxScript = (new MDXHelper("ITENS"))
              .SetIdLinxDimensions("")
              .SetIdGpeconDimensions("")
              .SetIdBandeiraRedeDimensions("")
              .Rows("[CRM_PFJ].[CLIENTE].[CLIENTE]")
              .Where(builder).FilterMetaData(validProperties)
              .SubqueryFilter("")
              .SetIdGpEcon(CurrentIdGpEcon())
              .SetIdLinx(CurrentIdLinx("DEV-BI-Omni"))
              .GetCommand(new MDXQuerySettings(){ NonEmptyColumns = true, NonEmptyRows = true});
           
           var command = connection.CreateCommand();
           command.Properties.Add("DbpropMsmdFlattened2", true);
           command.CommandText = mdxScript;
           connection.Open();
           IEnumerable<LookUpEntityAdapter1Cliente> result = null;
           using (var reader = command.ExecuteReader())
           {
               List<string> columnInReader = new List<string>();
               var dt = reader.GetSchemaTable();
               foreach (DataRow row in dt.Rows) columnInReader.Add(row["ColumnName"].ToString());
               result = reader.Select(r => new LookUpEntityAdapter1Cliente {
               Cliente = !columnInReader.Contains("[CRM_PFJ].[CLIENTE].[CLIENTE].[MEMBER_CAPTION]") || (validProperties.Length > 0 && !validProperties.Contains("[CRM_PFJ].[CLIENTE].[CLIENTE]")) || r["[CRM_PFJ].[CLIENTE].[CLIENTE].[MEMBER_CAPTION]"] is DBNull || r["[CRM_PFJ].[CLIENTE].[CLIENTE].[MEMBER_CAPTION]"] == null ? default(String) : ((string)r["[CRM_PFJ].[CLIENTE].[CLIENTE].[MEMBER_CAPTION]"])
               }).ToList();
           }
           return result;
       }
    }


		
		    
    [Ignore()]
    public IEnumerable<LookUpEntityAdapter1CodLoja> GetOlapLookUpEntityAdapter1CodLoja(List<EntitySearch> entitySearchList)
    {
       List<MDXField> fieldsMap = new List<MDXField>();
       fieldsMap.Add(new MDXField("CodLoja", "[LJV_LOJA].[COD_LOJA].[COD_LOJA]", false));
       string[] validProperties = (entitySearchList == null ? new string[] {} : EntitySearch.GetValidProperties(entitySearchList));
       validProperties = EntitySearch.GetLinqValidProperties(validProperties, fieldsMap.ToDictionary(f => f.Name, f => f.MDX));
       MDXQueryFilterBuilder builder = new MDXQueryFilterBuilder(fieldsMap);
       builder.Conditions(entitySearchList);
       string connString = Linx.Business.Tools.CacheAccessHelper.GetConnectionString("DEV-BI-Omni");
       if (connString == "name=DEV-BI-Omni") connString = Linx.Tools.ConnectionManager.GetConnectionString("DEV-BI-Omni");
       using (Microsoft.AnalysisServices.AdomdClient.AdomdConnection connection = new Microsoft.AnalysisServices.AdomdClient.AdomdConnection(connString))
       {
           string mdxScript = (new MDXHelper("ITENS"))
              .SetIdLinxDimensions("")
              .SetIdGpeconDimensions("")
              .SetIdBandeiraRedeDimensions("")
              .Rows("[LJV_LOJA].[COD_LOJA].[COD_LOJA]")
              .Where(builder).FilterMetaData(validProperties)
              .SubqueryFilter("")
              .SetIdGpEcon(CurrentIdGpEcon())
              .SetIdLinx(CurrentIdLinx("DEV-BI-Omni"))
              .GetCommand(new MDXQuerySettings(){ NonEmptyColumns = true, NonEmptyRows = true});
           
           var command = connection.CreateCommand();
           command.Properties.Add("DbpropMsmdFlattened2", true);
           command.CommandText = mdxScript;
           connection.Open();
           IEnumerable<LookUpEntityAdapter1CodLoja> result = null;
           using (var reader = command.ExecuteReader())
           {
               List<string> columnInReader = new List<string>();
               var dt = reader.GetSchemaTable();
               foreach (DataRow row in dt.Rows) columnInReader.Add(row["ColumnName"].ToString());
               result = reader.Select(r => new LookUpEntityAdapter1CodLoja {
               CodLoja = !columnInReader.Contains("[LJV_LOJA].[COD_LOJA].[COD_LOJA].[MEMBER_CAPTION]") || (validProperties.Length > 0 && !validProperties.Contains("[LJV_LOJA].[COD_LOJA].[COD_LOJA]")) || r["[LJV_LOJA].[COD_LOJA].[COD_LOJA].[MEMBER_CAPTION]"] is DBNull || r["[LJV_LOJA].[COD_LOJA].[COD_LOJA].[MEMBER_CAPTION]"] == null ? default(String) : ((string)r["[LJV_LOJA].[COD_LOJA].[COD_LOJA].[MEMBER_CAPTION]"])
               }).ToList();
           }
           return result;
       }
    }


		
		    
    [Ignore()]
    public IEnumerable<LookUpEntityAdapter1Data> GetOlapLookUpEntityAdapter1Data(List<EntitySearch> entitySearchList)
    {
       List<MDXField> fieldsMap = new List<MDXField>();
       fieldsMap.Add(new MDXField("Data", "[DATAS].[DATA].[DATA]", false));
       string[] validProperties = (entitySearchList == null ? new string[] {} : EntitySearch.GetValidProperties(entitySearchList));
       validProperties = EntitySearch.GetLinqValidProperties(validProperties, fieldsMap.ToDictionary(f => f.Name, f => f.MDX));
       MDXQueryFilterBuilder builder = new MDXQueryFilterBuilder(fieldsMap);
       builder.Conditions(entitySearchList);
       string connString = Linx.Business.Tools.CacheAccessHelper.GetConnectionString("DEV-BI-Omni");
       if (connString == "name=DEV-BI-Omni") connString = Linx.Tools.ConnectionManager.GetConnectionString("DEV-BI-Omni");
       using (Microsoft.AnalysisServices.AdomdClient.AdomdConnection connection = new Microsoft.AnalysisServices.AdomdClient.AdomdConnection(connString))
       {
           string mdxScript = (new MDXHelper("ITENS"))
              .SetIdLinxDimensions("")
              .SetIdGpeconDimensions("")
              .SetIdBandeiraRedeDimensions("")
              .Rows("[DATAS].[DATA].[DATA]")
              .Where(builder).FilterMetaData(validProperties)
              .SubqueryFilter("")
              .SetIdGpEcon(CurrentIdGpEcon())
              .SetIdLinx(CurrentIdLinx("DEV-BI-Omni"))
              .GetCommand(new MDXQuerySettings(){ NonEmptyColumns = true, NonEmptyRows = true});
           
           var command = connection.CreateCommand();
           command.Properties.Add("DbpropMsmdFlattened2", true);
           command.CommandText = mdxScript;
           connection.Open();
           IEnumerable<LookUpEntityAdapter1Data> result = null;
           using (var reader = command.ExecuteReader())
           {
               List<string> columnInReader = new List<string>();
               var dt = reader.GetSchemaTable();
               foreach (DataRow row in dt.Rows) columnInReader.Add(row["ColumnName"].ToString());
               result = reader.Select(r => new LookUpEntityAdapter1Data {
               Data = !columnInReader.Contains("[DATAS].[DATA].[DATA].[MEMBER_CAPTION]") || (validProperties.Length > 0 && !validProperties.Contains("[DATAS].[DATA].[DATA]")) || r["[DATAS].[DATA].[DATA].[MEMBER_CAPTION]"] is DBNull || r["[DATAS].[DATA].[DATA].[MEMBER_CAPTION]"] == null ? default(DateTime) : DateTime.Parse((string)r["[DATAS].[DATA].[DATA].[MEMBER_CAPTION]"])
               }).ToList();
           }
           return result;
       }
    }


		
		    
    [Ignore()]
    public IEnumerable<LookUpEntityAdapter1IdBandeiraRede> GetOlapLookUpEntityAdapter1IdBandeiraRede(List<EntitySearch> entitySearchList)
    {
       List<MDXField> fieldsMap = new List<MDXField>();
       fieldsMap.Add(new MDXField("IdBandeiraRede", "[TBC_BANDEIRA_REDE].[ID_BANDEIRA_REDE].[ID_BANDEIRA_REDE]", false));
       string[] validProperties = (entitySearchList == null ? new string[] {} : EntitySearch.GetValidProperties(entitySearchList));
       validProperties = EntitySearch.GetLinqValidProperties(validProperties, fieldsMap.ToDictionary(f => f.Name, f => f.MDX));
       MDXQueryFilterBuilder builder = new MDXQueryFilterBuilder(fieldsMap);
       builder.Conditions(entitySearchList);
       string connString = Linx.Business.Tools.CacheAccessHelper.GetConnectionString("DEV-BI-Omni");
       if (connString == "name=DEV-BI-Omni") connString = Linx.Tools.ConnectionManager.GetConnectionString("DEV-BI-Omni");
       using (Microsoft.AnalysisServices.AdomdClient.AdomdConnection connection = new Microsoft.AnalysisServices.AdomdClient.AdomdConnection(connString))
       {
           string mdxScript = (new MDXHelper("ITENS"))
              .SetIdLinxDimensions("")
              .SetIdGpeconDimensions("")
              .SetIdBandeiraRedeDimensions("")
              .Rows("[TBC_BANDEIRA_REDE].[ID_BANDEIRA_REDE].[ID_BANDEIRA_REDE]")
              .Where(builder).FilterMetaData(validProperties)
              .SubqueryFilter("")
              .SetIdGpEcon(CurrentIdGpEcon())
              .SetIdLinx(CurrentIdLinx("DEV-BI-Omni"))
              .GetCommand(new MDXQuerySettings(){ NonEmptyColumns = true, NonEmptyRows = true});
           
           var command = connection.CreateCommand();
           command.Properties.Add("DbpropMsmdFlattened2", true);
           command.CommandText = mdxScript;
           connection.Open();
           IEnumerable<LookUpEntityAdapter1IdBandeiraRede> result = null;
           using (var reader = command.ExecuteReader())
           {
               List<string> columnInReader = new List<string>();
               var dt = reader.GetSchemaTable();
               foreach (DataRow row in dt.Rows) columnInReader.Add(row["ColumnName"].ToString());
               result = reader.Select(r => new LookUpEntityAdapter1IdBandeiraRede {
               IdBandeiraRede = !columnInReader.Contains("[TBC_BANDEIRA_REDE].[ID_BANDEIRA_REDE].[ID_BANDEIRA_REDE].[MEMBER_CAPTION]") || (validProperties.Length > 0 && !validProperties.Contains("[TBC_BANDEIRA_REDE].[ID_BANDEIRA_REDE].[ID_BANDEIRA_REDE]")) || r["[TBC_BANDEIRA_REDE].[ID_BANDEIRA_REDE].[ID_BANDEIRA_REDE].[MEMBER_CAPTION]"] is DBNull || r["[TBC_BANDEIRA_REDE].[ID_BANDEIRA_REDE].[ID_BANDEIRA_REDE].[MEMBER_CAPTION]"] == null ? default(Int64) : Int64.Parse((string)r["[TBC_BANDEIRA_REDE].[ID_BANDEIRA_REDE].[ID_BANDEIRA_REDE].[MEMBER_CAPTION]"])
               }).ToList();
           }
           return result;
       }
    }


		
		    
    [Ignore()]
    public IEnumerable<LookUpEntityAdapter1Loja> GetOlapLookUpEntityAdapter1Loja(List<EntitySearch> entitySearchList)
    {
       List<MDXField> fieldsMap = new List<MDXField>();
       fieldsMap.Add(new MDXField("Loja", "[LJV_LOJA].[LOJA].[LOJA]", false));
       string[] validProperties = (entitySearchList == null ? new string[] {} : EntitySearch.GetValidProperties(entitySearchList));
       validProperties = EntitySearch.GetLinqValidProperties(validProperties, fieldsMap.ToDictionary(f => f.Name, f => f.MDX));
       MDXQueryFilterBuilder builder = new MDXQueryFilterBuilder(fieldsMap);
       builder.Conditions(entitySearchList);
       string connString = Linx.Business.Tools.CacheAccessHelper.GetConnectionString("DEV-BI-Omni");
       if (connString == "name=DEV-BI-Omni") connString = Linx.Tools.ConnectionManager.GetConnectionString("DEV-BI-Omni");
       using (Microsoft.AnalysisServices.AdomdClient.AdomdConnection connection = new Microsoft.AnalysisServices.AdomdClient.AdomdConnection(connString))
       {
           string mdxScript = (new MDXHelper("ITENS"))
              .SetIdLinxDimensions("")
              .SetIdGpeconDimensions("")
              .SetIdBandeiraRedeDimensions("")
              .Rows("[LJV_LOJA].[LOJA].[LOJA]")
              .Where(builder).FilterMetaData(validProperties)
              .SubqueryFilter("")
              .SetIdGpEcon(CurrentIdGpEcon())
              .SetIdLinx(CurrentIdLinx("DEV-BI-Omni"))
              .GetCommand(new MDXQuerySettings(){ NonEmptyColumns = true, NonEmptyRows = true});
           
           var command = connection.CreateCommand();
           command.Properties.Add("DbpropMsmdFlattened2", true);
           command.CommandText = mdxScript;
           connection.Open();
           IEnumerable<LookUpEntityAdapter1Loja> result = null;
           using (var reader = command.ExecuteReader())
           {
               List<string> columnInReader = new List<string>();
               var dt = reader.GetSchemaTable();
               foreach (DataRow row in dt.Rows) columnInReader.Add(row["ColumnName"].ToString());
               result = reader.Select(r => new LookUpEntityAdapter1Loja {
               Loja = !columnInReader.Contains("[LJV_LOJA].[LOJA].[LOJA].[MEMBER_CAPTION]") || (validProperties.Length > 0 && !validProperties.Contains("[LJV_LOJA].[LOJA].[LOJA]")) || r["[LJV_LOJA].[LOJA].[LOJA].[MEMBER_CAPTION]"] is DBNull || r["[LJV_LOJA].[LOJA].[LOJA].[MEMBER_CAPTION]"] == null ? default(String) : ((string)r["[LJV_LOJA].[LOJA].[LOJA].[MEMBER_CAPTION]"])
               }).ToList();
           }
           return result;
       }
    }


		
	    #endregion Get OLAP Definitions.


	    #region Get LookUp Definitions.
	
		
			
        [Ignore]
	    //Get All LookUpEntityAdapter1Cliente.
	    public IQueryable<LookUpEntityAdapter1Cliente> GetAllLookUpEntityAdapter1Cliente()
	    {
	        return this.GetLookUpEntityAdapter1Cliente(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpEntityAdapter1Cliente By EntitySearch.
	    public IQueryable<LookUpEntityAdapter1Cliente> GetLookUpEntityAdapter1ClienteByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpEntityAdapter1Cliente(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpEntityAdapter1Cliente.
	    public IQueryable<LookUpEntityAdapter1Cliente> GetLookUpEntityAdapter1Cliente(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "EntityAdapter1_Cliente" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpEntityAdapter1Cliente";
	        object propvalue = (propertyName.IsNullOrEmpty() || serializedPropertyValue.IsNullOrEmpty() ? null : SerializationManager<object>.StringToObject(serializedPropertyValue));
	        if (!propvalue.IsNullOrEmpty())
	        {
	        	if (entitySearch.Expressions.Count > 0)
	        		entitySearch.Expressions.Add(new EntitySearchExpression("Condition", "&&"));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Field", propertyName));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Operator", (propvalue != null && propvalue is string && ((string)propvalue).Contains("%") ? "Like" : "==")));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Value", propvalue));
	        }
	
			
	        List<EntitySearch> entitySearchList = new List<EntitySearch>();
	        entitySearchList.Add(entitySearch);

	        var query = this.GetOlapLookUpEntityAdapter1Cliente(entitySearchList).AsQueryable();
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpEntityAdapter1CodLoja.
	    public IQueryable<LookUpEntityAdapter1CodLoja> GetAllLookUpEntityAdapter1CodLoja()
	    {
	        return this.GetLookUpEntityAdapter1CodLoja(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpEntityAdapter1CodLoja By EntitySearch.
	    public IQueryable<LookUpEntityAdapter1CodLoja> GetLookUpEntityAdapter1CodLojaByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpEntityAdapter1CodLoja(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpEntityAdapter1CodLoja.
	    public IQueryable<LookUpEntityAdapter1CodLoja> GetLookUpEntityAdapter1CodLoja(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "EntityAdapter1_CodLoja" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpEntityAdapter1CodLoja";
	        object propvalue = (propertyName.IsNullOrEmpty() || serializedPropertyValue.IsNullOrEmpty() ? null : SerializationManager<object>.StringToObject(serializedPropertyValue));
	        if (!propvalue.IsNullOrEmpty())
	        {
	        	if (entitySearch.Expressions.Count > 0)
	        		entitySearch.Expressions.Add(new EntitySearchExpression("Condition", "&&"));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Field", propertyName));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Operator", (propvalue != null && propvalue is string && ((string)propvalue).Contains("%") ? "Like" : "==")));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Value", propvalue));
	        }
	
			
	        List<EntitySearch> entitySearchList = new List<EntitySearch>();
	        entitySearchList.Add(entitySearch);

	        var query = this.GetOlapLookUpEntityAdapter1CodLoja(entitySearchList).AsQueryable();
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpEntityAdapter1Data.
	    public IQueryable<LookUpEntityAdapter1Data> GetAllLookUpEntityAdapter1Data()
	    {
	        return this.GetLookUpEntityAdapter1Data(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpEntityAdapter1Data By EntitySearch.
	    public IQueryable<LookUpEntityAdapter1Data> GetLookUpEntityAdapter1DataByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpEntityAdapter1Data(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpEntityAdapter1Data.
	    public IQueryable<LookUpEntityAdapter1Data> GetLookUpEntityAdapter1Data(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "EntityAdapter1_Data" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpEntityAdapter1Data";
	        object propvalue = (propertyName.IsNullOrEmpty() || serializedPropertyValue.IsNullOrEmpty() ? null : SerializationManager<object>.StringToObject(serializedPropertyValue));
	        if (!propvalue.IsNullOrEmpty())
	        {
	        	if (entitySearch.Expressions.Count > 0)
	        		entitySearch.Expressions.Add(new EntitySearchExpression("Condition", "&&"));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Field", propertyName));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Operator", (propvalue != null && propvalue is string && ((string)propvalue).Contains("%") ? "Like" : "==")));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Value", propvalue));
	        }
	
			
	        List<EntitySearch> entitySearchList = new List<EntitySearch>();
	        entitySearchList.Add(entitySearch);

	        var query = this.GetOlapLookUpEntityAdapter1Data(entitySearchList).AsQueryable();
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpEntityAdapter1IdBandeiraRede.
	    public IQueryable<LookUpEntityAdapter1IdBandeiraRede> GetAllLookUpEntityAdapter1IdBandeiraRede()
	    {
	        return this.GetLookUpEntityAdapter1IdBandeiraRede(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpEntityAdapter1IdBandeiraRede By EntitySearch.
	    public IQueryable<LookUpEntityAdapter1IdBandeiraRede> GetLookUpEntityAdapter1IdBandeiraRedeByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpEntityAdapter1IdBandeiraRede(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpEntityAdapter1IdBandeiraRede.
	    public IQueryable<LookUpEntityAdapter1IdBandeiraRede> GetLookUpEntityAdapter1IdBandeiraRede(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "EntityAdapter1_IdBandeiraRede" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpEntityAdapter1IdBandeiraRede";
	        object propvalue = (propertyName.IsNullOrEmpty() || serializedPropertyValue.IsNullOrEmpty() ? null : SerializationManager<object>.StringToObject(serializedPropertyValue));
	        if (!propvalue.IsNullOrEmpty())
	        {
	        	if (entitySearch.Expressions.Count > 0)
	        		entitySearch.Expressions.Add(new EntitySearchExpression("Condition", "&&"));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Field", propertyName));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Operator", (propvalue != null && propvalue is string && ((string)propvalue).Contains("%") ? "Like" : "==")));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Value", propvalue));
	        }
	
			
	        List<EntitySearch> entitySearchList = new List<EntitySearch>();
	        entitySearchList.Add(entitySearch);

	        var query = this.GetOlapLookUpEntityAdapter1IdBandeiraRede(entitySearchList).AsQueryable();
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpEntityAdapter1Loja.
	    public IQueryable<LookUpEntityAdapter1Loja> GetAllLookUpEntityAdapter1Loja()
	    {
	        return this.GetLookUpEntityAdapter1Loja(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpEntityAdapter1Loja By EntitySearch.
	    public IQueryable<LookUpEntityAdapter1Loja> GetLookUpEntityAdapter1LojaByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpEntityAdapter1Loja(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpEntityAdapter1Loja.
	    public IQueryable<LookUpEntityAdapter1Loja> GetLookUpEntityAdapter1Loja(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "EntityAdapter1_Loja" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpEntityAdapter1Loja";
	        object propvalue = (propertyName.IsNullOrEmpty() || serializedPropertyValue.IsNullOrEmpty() ? null : SerializationManager<object>.StringToObject(serializedPropertyValue));
	        if (!propvalue.IsNullOrEmpty())
	        {
	        	if (entitySearch.Expressions.Count > 0)
	        		entitySearch.Expressions.Add(new EntitySearchExpression("Condition", "&&"));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Field", propertyName));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Operator", (propvalue != null && propvalue is string && ((string)propvalue).Contains("%") ? "Like" : "==")));
	        	entitySearch.Expressions.Add(new EntitySearchExpression("Value", propvalue));
	        }
	
			
	        List<EntitySearch> entitySearchList = new List<EntitySearch>();
	        entitySearchList.Add(entitySearch);

	        var query = this.GetOlapLookUpEntityAdapter1Loja(entitySearchList).AsQueryable();
	
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
	
		

	        if (entityName.InList("Linx.Demo.BV.MacrosEventosValidacoes.Arquivo"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "Arquivo",
	        			NameSpace = "Linx.Demo.BV.MacrosEventosValidacoes",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "Arquivo",
	        			ClearMethodName = "ClearArquivo",
	        			QueryMethodName  = "GetPagedArquivo",	
	        			CountingMethodName  = "GetArquivo" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Demo.BV.MacrosEventosValidacoes.Arquivo"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Demo.BV.MacrosEventosValidacoes.Arquivo"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Demo.BV.MacrosEventosValidacoes.Pais"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "Pais",
	        			NameSpace = "Linx.Demo.BV.MacrosEventosValidacoes",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "Pais",
	        			ClearMethodName = "ClearPais",
	        			QueryMethodName  = "GetPagedPais",	
	        			CountingMethodName  = "GetPais" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Demo.BV.MacrosEventosValidacoes.Pais"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Demo.BV.MacrosEventosValidacoes.Pais"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Demo.BV.MacrosEventosValidacoes.Pais", "Linx.Demo.BV.MacrosEventosValidacoes.Estado"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "Estado" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Demo.BV.MacrosEventosValidacoes",
	        			HasQuickSearch = false,
	        			ParentClassName = "Pais",	
	        			DisplayName = "Estado",
	        			ClearMethodName = "ClearEstado" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedEstado" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetEstado" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Demo.BV.MacrosEventosValidacoes.Estado"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Demo.BV.MacrosEventosValidacoes.Estado" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Demo.BV.MacrosEventosValidacoes.ValorVendas"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "ValorVendas",
	        			NameSpace = "Linx.Demo.BV.MacrosEventosValidacoes",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "ValorVendas",
	        			ClearMethodName = "ClearValorVendas",
	        			QueryMethodName  = "GetPagedValorVendas",	
	        			CountingMethodName  = "GetValorVendas" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Demo.BV.MacrosEventosValidacoes.ValorVendas"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Demo.BV.MacrosEventosValidacoes.ValorVendas"), forceAll: forceAll)
	        		});
	        }
	
            return result;
        }
	
	    [Ignore]
	    public string[] GetClientDomains(bool erp)
        {	
	    		if (erp)
	    		{

         		    return new string[] { "Demo_ClientErpDataDomainsFactory", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Demo.BV.ClientResources.ClientErpDataDomainsFactory.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
	    		}
	    		else 
	    		{

         		    return new string[] { "Demo_MobileDataDomains", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Demo.BV.ClientResources.MobileDataDomains.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
	    		}

        }

	    [Ignore]
	    public string[] GetClientService(bool erp)
        {	

	    		if (erp)
	    		{

         		    return new string[] { "Demo_MacrosEventosValidacoesClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Demo.BV.ClientResources.MacrosEventosValidacoesClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Demo_macrosEventosValidacoesService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Demo.BV.ClientResources.macrosEventosValidacoesService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear Arquivo.
	    public IEnumerable<Arquivo> ClearArquivo()
	    {
	        List<Arquivo> result = new List<Arquivo>();
	        result.Add(new Arquivo());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear Pais.
	    public IEnumerable<Pais> ClearPais()
	    {
	        List<Pais> result = new List<Pais>();
	        result.Add(new Pais());	
			
	        result[0].EstadoList = new List<Estado>();
	        ((List<Estado>)result[0].EstadoList).Add(new Estado());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear Estado.
	    public IEnumerable<Estado> ClearEstado()
	    {
	        List<Estado> result = new List<Estado>();
	        result.Add(new Estado());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear ValorVendas.
	    public IEnumerable<ValorVendas> ClearValorVendas()
	    {
	        List<ValorVendas> result = new List<ValorVendas>();
	        result.Add(new ValorVendas());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    [ArquivoQueryCustomAuthorizationAuto()]
	    [Query(HasSideEffects = false)]
	    //Get Arquivo.
	    public IEnumerable<Arquivo> GetArquivo()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetArquivo")))
 	        {
 	             AuthorizationResult authorizationResult = (new ArquivoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		

	        IEnumerable<Arquivo> result = 
	            (from entity0 in Arquivo.OnSearchingReplacement(null) select entity0);
		
	
	        	

	
	        return result;
	    }
			
	
	    [ArquivoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get ArquivoNoAssociations.
	    public IEnumerable<Arquivo> GetArquivoNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetArquivoNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new ArquivoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		

	        IEnumerable<Arquivo> result = 
	            (from entity0 in Arquivo.OnSearchingReplacement(null) select entity0);
		
	
	        	

	
	        return result;
	    }
			
	
	    [PaisQueryCustomAuthorizationAuto()]
	    [Query(HasSideEffects = false)]
	    //Get Pais.
	    public IQueryable<Pais> GetPais()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPais")))
 	        {
 	             AuthorizationResult authorizationResult = (new PaisQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<Pais> result = 
	            (from entity0 in this.DbContext.PAIS
	            
	            	
	            select new Pais()		
	            {
	            
                ComboboxPais = entity0.COMBOBOX_PAIS
                , ComboboxPaisName = ((entity0.COMBOBOX_PAIS) == 1 ? "PAIS1" : ((entity0.COMBOBOX_PAIS) == 2 ? "PAIS2" : ((entity0.COMBOBOX_PAIS) == 3 ? "PAIS3" : "")))
                , DatetimePais = entity0.DATETIME_PAIS
                , DecimalPais = entity0.DECIMAL_PAIS
                , IdPais = entity0.ID_PAIS
                , StringPais = entity0.STRING_PAIS
			
                ,EstadoList = 
	                        (from entity1 in entity0.ESTADO_LISTA
                                  let entity1Al1 = entity1.PAIS
	                        
	                        	
	                        select new Estado()
	                        {
	                        
                                ComboboxEstado = entity1.COMBOBOX_ESTADO
                                , ComboboxEstadoName = ((entity1.COMBOBOX_ESTADO) == 1 ? "ESTADO1" : ((entity1.COMBOBOX_ESTADO) == 2 ? "ESTADO2" : ((entity1.COMBOBOX_ESTADO) == 3 ? "ESTADO3" : ((entity1.COMBOBOX_ESTADO) == 4 ? "ESTADO4" : ""))))
                                , DecimalEstado = entity1.DECIMAL_ESTADO
                                , IdEstado = entity1.ID_ESTADO
                                , IdPais = entity1Al1.ID_PAIS
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [EstadoQueryCustomAuthorizationAuto()]
	    [Query(HasSideEffects = false)]
	    //Get Estado.
	    public IQueryable<Estado> GetEstado()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetEstado")))
 	        {
 	             AuthorizationResult authorizationResult = (new EstadoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<Estado> result = 
	            (from entity0 in this.DbContext.ESTADO
                  let entity0Al1 = entity0.PAIS
	            
	            	
	            select new Estado()		
	            {
	            
                ComboboxEstado = entity0.COMBOBOX_ESTADO
                , ComboboxEstadoName = ((entity0.COMBOBOX_ESTADO) == 1 ? "ESTADO1" : ((entity0.COMBOBOX_ESTADO) == 2 ? "ESTADO2" : ((entity0.COMBOBOX_ESTADO) == 3 ? "ESTADO3" : ((entity0.COMBOBOX_ESTADO) == 4 ? "ESTADO4" : ""))))
                , DecimalEstado = entity0.DECIMAL_ESTADO
                , IdEstado = entity0.ID_ESTADO
                , IdPais = entity0Al1.ID_PAIS
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [PaisQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PaisNoAssociations.
	    public IQueryable<Pais> GetPaisNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPaisNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new PaisQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<Pais> result = 
	            (from entity0 in this.DbContext.PAIS
	            
	            	
	            select new Pais()		
	            {
	            
                ComboboxPais = entity0.COMBOBOX_PAIS
                , ComboboxPaisName = ((entity0.COMBOBOX_PAIS) == 1 ? "PAIS1" : ((entity0.COMBOBOX_PAIS) == 2 ? "PAIS2" : ((entity0.COMBOBOX_PAIS) == 3 ? "PAIS3" : "")))
                , DatetimePais = entity0.DATETIME_PAIS
                , DecimalPais = entity0.DECIMAL_PAIS
                , IdPais = entity0.ID_PAIS
                , StringPais = entity0.STRING_PAIS
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [EstadoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get EstadoNoAssociations.
	    public IQueryable<Estado> GetEstadoNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetEstadoNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new EstadoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<Estado> result = 
	            (from entity0 in this.DbContext.ESTADO
                  let entity0Al1 = entity0.PAIS
	            
	            	
	            select new Estado()		
	            {
	            
                ComboboxEstado = entity0.COMBOBOX_ESTADO
                , ComboboxEstadoName = ((entity0.COMBOBOX_ESTADO) == 1 ? "ESTADO1" : ((entity0.COMBOBOX_ESTADO) == 2 ? "ESTADO2" : ((entity0.COMBOBOX_ESTADO) == 3 ? "ESTADO3" : ((entity0.COMBOBOX_ESTADO) == 4 ? "ESTADO4" : ""))))
                , DecimalEstado = entity0.DECIMAL_ESTADO
                , IdEstado = entity0.ID_ESTADO
                , IdPais = entity0Al1.ID_PAIS
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [ValorVendasQueryCustomAuthorizationAuto()]
	    [Query(HasSideEffects = false)]
	    //Get ValorVendas.
	    public IEnumerable<ValorVendas> GetValorVendas()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetValorVendas")))
 	        {
 	             AuthorizationResult authorizationResult = (new ValorVendasQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        IEnumerable<ValorVendas> result = this.GetOlapValorVendas(null);
	  	
	
	        	

	
	        return result;
	    }
			
	
	    [ValorVendasQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get ValorVendasNoAssociations.
	    public IEnumerable<ValorVendas> GetValorVendasNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetValorVendasNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new ValorVendasQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        IEnumerable<ValorVendas> result = this.GetOlapValorVendas(null);
	  	
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	//Add filtering disabled property for PAIS
	    	string[] bmDisabledPaisList = this.GetEDM().GetFilteringDisabledList("PAIS");
	    	if (bmDisabledPaisList.Length > 0)
	    	{
	
	    		if (bmDisabledPaisList.Contains("PAIS.COMBOBOX_PAIS"))
	    		{
	    			result.Add("Pais|ComboboxPais");
	    			result.Add("Pais|PAIS.COMBOBOX_PAIS");
	    		}
	
	    		if (bmDisabledPaisList.Contains("PAIS.DATETIME_PAIS"))
	    		{
	    			result.Add("Pais|DatetimePais");
	    			result.Add("Pais|PAIS.DATETIME_PAIS");
	    		}
	
	    		if (bmDisabledPaisList.Contains("PAIS.DECIMAL_PAIS"))
	    		{
	    			result.Add("Pais|DecimalPais");
	    			result.Add("Pais|PAIS.DECIMAL_PAIS");
	    		}
	
	    		if (bmDisabledPaisList.Contains("PAIS.ID_PAIS"))
	    		{
	    			result.Add("Pais|IdPais");
	    			result.Add("Pais|PAIS.ID_PAIS");
	    		}
	
	    		if (bmDisabledPaisList.Contains("PAIS.STRING_PAIS"))
	    		{
	    			result.Add("Pais|StringPais");
	    			result.Add("Pais|PAIS.STRING_PAIS");
	    		}
	    	}
	    	//Add filtering disabled property for ESTADO
	    	string[] bmDisabledEstadoList = this.GetEDM().GetFilteringDisabledList("ESTADO");
	    	if (bmDisabledEstadoList.Length > 0)
	    	{
	
	    		if (bmDisabledEstadoList.Contains("ESTADO.COMBOBOX_ESTADO"))
	    		{
	    			result.Add("Estado|ComboboxEstado");
	    			result.Add("Estado|ESTADO.COMBOBOX_ESTADO");
	    		}
	
	    		if (bmDisabledEstadoList.Contains("ESTADO.DECIMAL_ESTADO"))
	    		{
	    			result.Add("Estado|DecimalEstado");
	    			result.Add("Estado|ESTADO.DECIMAL_ESTADO");
	    		}
	
	    		if (bmDisabledEstadoList.Contains("ESTADO.ID_ESTADO"))
	    		{
	    			result.Add("Estado|IdEstado");
	    			result.Add("Estado|ESTADO.ID_ESTADO");
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
				
	    [Ignore]
	    //Get Arquivo By EntitySearchId.
	    public IEnumerable<Arquivo> GetArquivoByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetArquivoByEntitySearch(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get Arquivo By EntitySearchId.
	    public IEnumerable<Arquivo> GetArquivoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetArquivoByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get Pais By EntitySearchId.
	    public IQueryable<Pais> GetPaisByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetPaisByEntitySearch(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get Estado By EntitySearchId.
	    public IQueryable<Estado> GetEstadoByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetEstadoByEntitySearch(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get Pais By EntitySearchId.
	    public IQueryable<Pais> GetPaisByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetPaisByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get Estado By EntitySearchId.
	    public IQueryable<Estado> GetEstadoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetEstadoByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get ValorVendas By EntitySearchId.
	    public IEnumerable<ValorVendas> GetValorVendasByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetValorVendasByEntitySearch(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get ValorVendas By EntitySearchId.
	    public IEnumerable<ValorVendas> GetValorVendasByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetValorVendasByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get Arquivo By Example.
	    [Ignore]
	    public IEnumerable<Arquivo> GetArquivoByExample(Arquivo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetArquivoByEntitySearch(queryAnalysis);
	    }
			
	    //Get Arquivo By Example.
	    [Ignore]
	    public IEnumerable<Arquivo> GetArquivoByExampleNoAssociations(Arquivo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetArquivoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get Pais By Example.
	    [Ignore]
	    public IQueryable<Pais> GetPaisByExample(Pais entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetPaisByEntitySearch(queryAnalysis);
	    }
			
	    //Get Estado By Example.
	    [Ignore]
	    public IQueryable<Estado> GetEstadoByExample(Estado entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetEstadoByEntitySearch(queryAnalysis);
	    }
			
	    //Get Pais By Example.
	    [Ignore]
	    public IQueryable<Pais> GetPaisByExampleNoAssociations(Pais entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetPaisByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get Estado By Example.
	    [Ignore]
	    public IQueryable<Estado> GetEstadoByExampleNoAssociations(Estado entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetEstadoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get ValorVendas By Example.
	    [Ignore]
	    public IEnumerable<ValorVendas> GetValorVendasByExample(ValorVendas entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetValorVendasByEntitySearch(queryAnalysis);
	    }
			
	    //Get ValorVendas By Example.
	    [Ignore]
	    public IEnumerable<ValorVendas> GetValorVendasByExampleNoAssociations(ValorVendas entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetValorVendasByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public Arquivo GetArquivoByKey(string nomeArquivo)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("Arquivo");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "NomeArquivo"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, nomeArquivo));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetArquivoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public Pais GetPaisByKey(int idPais)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("Pais");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdPais"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idPais));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetPaisByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public Estado GetEstadoByKey(int idEstado)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("Estado");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdEstado"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idEstado));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetEstadoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public ValorVendas GetValorVendasByKey(Int64 idBandeiraRede)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("ValorVendas");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdBandeiraRede"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idBandeiraRede));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetValorVendasByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    [ArquivoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get ArquivoByEntitySearch.
	    public IEnumerable<Arquivo> GetArquivoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetArquivoByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new ArquivoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		

	        IEnumerable<Arquivo> result = 
	            (from entity0 in Arquivo.OnSearchingReplacement(entitySearchList) select entity0);
		
	
	        	

	
	        return result;
	    }
			
	
	    [ArquivoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get ArquivoByEntitySearchNoAssociations.
	    public IEnumerable<Arquivo> GetArquivoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetArquivoByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new ArquivoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		

	        IEnumerable<Arquivo> result = 
	            (from entity0 in Arquivo.OnSearchingReplacement(entitySearchList) select entity0);
		
	
	        	

	
	        return result;
	    }
			
	
	    [PaisQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PaisByEntitySearch.
	    public IQueryable<Pais> GetPaisByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPaisByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new PaisQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Pais));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<Pais> result = 
	            (from entity0 in this.DbContext.PAIS.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new Pais()		
	            {
	            
                ComboboxPais = entity0.COMBOBOX_PAIS
                , ComboboxPaisName = ((entity0.COMBOBOX_PAIS) == 1 ? "PAIS1" : ((entity0.COMBOBOX_PAIS) == 2 ? "PAIS2" : ((entity0.COMBOBOX_PAIS) == 3 ? "PAIS3" : "")))
                , DatetimePais = entity0.DATETIME_PAIS
                , DecimalPais = entity0.DECIMAL_PAIS
                , IdPais = entity0.ID_PAIS
                , StringPais = entity0.STRING_PAIS
			
                ,EstadoList = 
	                        (from entity1 in entity0.ESTADO_LISTA
                                  let entity1Al1 = entity1.PAIS
	                        
	                        	
	                        select new Estado()
	                        {
	                        
                                ComboboxEstado = entity1.COMBOBOX_ESTADO
                                , ComboboxEstadoName = ((entity1.COMBOBOX_ESTADO) == 1 ? "ESTADO1" : ((entity1.COMBOBOX_ESTADO) == 2 ? "ESTADO2" : ((entity1.COMBOBOX_ESTADO) == 3 ? "ESTADO3" : ((entity1.COMBOBOX_ESTADO) == 4 ? "ESTADO4" : ""))))
                                , DecimalEstado = entity1.DECIMAL_ESTADO
                                , IdEstado = entity1.ID_ESTADO
                                , IdPais = entity1Al1.ID_PAIS
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [EstadoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get EstadoByEntitySearch.
	    public IQueryable<Estado> GetEstadoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetEstadoByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new EstadoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Estado));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<Estado> result = 
	            (from entity0 in this.DbContext.ESTADO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.PAIS
	            
	            	
	            select new Estado()		
	            {
	            
                ComboboxEstado = entity0.COMBOBOX_ESTADO
                , ComboboxEstadoName = ((entity0.COMBOBOX_ESTADO) == 1 ? "ESTADO1" : ((entity0.COMBOBOX_ESTADO) == 2 ? "ESTADO2" : ((entity0.COMBOBOX_ESTADO) == 3 ? "ESTADO3" : ((entity0.COMBOBOX_ESTADO) == 4 ? "ESTADO4" : ""))))
                , DecimalEstado = entity0.DECIMAL_ESTADO
                , IdEstado = entity0.ID_ESTADO
                , IdPais = entity0Al1.ID_PAIS
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [PaisQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PaisByEntitySearchNoAssociations.
	    public IQueryable<Pais> GetPaisByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPaisByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new PaisQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Pais));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<Pais> result = 
	            (from entity0 in this.DbContext.PAIS.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new Pais()		
	            {
	            
                ComboboxPais = entity0.COMBOBOX_PAIS
                , ComboboxPaisName = ((entity0.COMBOBOX_PAIS) == 1 ? "PAIS1" : ((entity0.COMBOBOX_PAIS) == 2 ? "PAIS2" : ((entity0.COMBOBOX_PAIS) == 3 ? "PAIS3" : "")))
                , DatetimePais = entity0.DATETIME_PAIS
                , DecimalPais = entity0.DECIMAL_PAIS
                , IdPais = entity0.ID_PAIS
                , StringPais = entity0.STRING_PAIS
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [EstadoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get EstadoByEntitySearchNoAssociations.
	    public IQueryable<Estado> GetEstadoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetEstadoByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new EstadoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Estado));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<Estado> result = 
	            (from entity0 in this.DbContext.ESTADO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.PAIS
	            
	            	
	            select new Estado()		
	            {
	            
                ComboboxEstado = entity0.COMBOBOX_ESTADO
                , ComboboxEstadoName = ((entity0.COMBOBOX_ESTADO) == 1 ? "ESTADO1" : ((entity0.COMBOBOX_ESTADO) == 2 ? "ESTADO2" : ((entity0.COMBOBOX_ESTADO) == 3 ? "ESTADO3" : ((entity0.COMBOBOX_ESTADO) == 4 ? "ESTADO4" : ""))))
                , DecimalEstado = entity0.DECIMAL_ESTADO
                , IdEstado = entity0.ID_ESTADO
                , IdPais = entity0Al1.ID_PAIS
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [EstadoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get EstadoParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<EstadoParentComposition> GetEstadoParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetEstadoParentCompositionByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new EstadoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "PAIS", "ESTADO", "PAIS", typeof(EstadoParentComposition));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<EstadoParentComposition> result = 
	            (from entity0 in this.DbContext.ESTADO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.PAIS
	            
	            	
	            select new EstadoParentComposition()		
	            {
	            
                ComboboxEstado = entity0.COMBOBOX_ESTADO
                , ComboboxEstadoName = ((entity0.COMBOBOX_ESTADO) == 1 ? "ESTADO1" : ((entity0.COMBOBOX_ESTADO) == 2 ? "ESTADO2" : ((entity0.COMBOBOX_ESTADO) == 3 ? "ESTADO3" : ((entity0.COMBOBOX_ESTADO) == 4 ? "ESTADO4" : ""))))
                , DecimalEstado = entity0.DECIMAL_ESTADO
                , IdEstado = entity0.ID_ESTADO
                , IdPais = entity0Al1.ID_PAIS
                //Pais Properties.
                , ComboboxPais = entity0.PAIS.COMBOBOX_PAIS
                , ComboboxPaisName = ((entity0.PAIS.COMBOBOX_PAIS) == 1 ? "PAIS1" : ((entity0.PAIS.COMBOBOX_PAIS) == 2 ? "PAIS2" : ((entity0.PAIS.COMBOBOX_PAIS) == 3 ? "PAIS3" : "")))
                , DatetimePais = entity0.PAIS.DATETIME_PAIS
                , DecimalPais = entity0.PAIS.DECIMAL_PAIS
                , StringPais = entity0.PAIS.STRING_PAIS
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [ValorVendasQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get ValorVendasByEntitySearch.
	    public IEnumerable<ValorVendas> GetValorVendasByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetValorVendasByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new ValorVendasQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<ValorVendas> result = this.GetOlapValorVendas(entitySearchList);
	  	
	
	        	

	
	        return result;
	    }
			
	
	    [ValorVendasQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get ValorVendasByEntitySearchNoAssociations.
	    public IEnumerable<ValorVendas> GetValorVendasByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetValorVendasByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new ValorVendasQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<ValorVendas> result = this.GetOlapValorVendas(entitySearchList);
	  	
	
	        	

	
	        return result;
	    }
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    [ArquivoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedArquivo.
	    public IEnumerable<Arquivo> GetPagedArquivo(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedArquivo")))
 	        {
 	             AuthorizationResult authorizationResult = (new ArquivoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		

	        IEnumerable<Arquivo> result = 
	            (from entity0 in Arquivo.OnSearchingReplacement(entitySearchList) select entity0);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetArquivoCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
			
	
	    [PaisQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedPais.
	    public IQueryable<Pais> GetPagedPais(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedPais")))
 	        {
 	             AuthorizationResult authorizationResult = (new PaisQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Pais));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<Pais> result = 
	            (from entity0 in this.DbContext.PAIS.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_PAIS ascending
	            
	            	
	            select new Pais()		
	            {
	            
                ComboboxPais = entity0.COMBOBOX_PAIS
                , ComboboxPaisName = ((entity0.COMBOBOX_PAIS) == 1 ? "PAIS1" : ((entity0.COMBOBOX_PAIS) == 2 ? "PAIS2" : ((entity0.COMBOBOX_PAIS) == 3 ? "PAIS3" : "")))
                , DatetimePais = entity0.DATETIME_PAIS
                , DecimalPais = entity0.DECIMAL_PAIS
                , IdPais = entity0.ID_PAIS
                , StringPais = entity0.STRING_PAIS
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    [EstadoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedEstado.
	    public IQueryable<Estado> GetPagedEstado(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedEstado")))
 	        {
 	             AuthorizationResult authorizationResult = (new EstadoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Estado));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<Estado> result = 
	            (from entity0 in this.DbContext.ESTADO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.PAIS
                orderby entity0.ID_ESTADO ascending
	            
	            	
	            select new Estado()		
	            {
	            
                ComboboxEstado = entity0.COMBOBOX_ESTADO
                , ComboboxEstadoName = ((entity0.COMBOBOX_ESTADO) == 1 ? "ESTADO1" : ((entity0.COMBOBOX_ESTADO) == 2 ? "ESTADO2" : ((entity0.COMBOBOX_ESTADO) == 3 ? "ESTADO3" : ((entity0.COMBOBOX_ESTADO) == 4 ? "ESTADO4" : ""))))
                , DecimalEstado = entity0.DECIMAL_ESTADO
                , IdEstado = entity0.ID_ESTADO
                , IdPais = entity0Al1.ID_PAIS
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetPaisCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Pais));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.PAIS.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetEstadoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Estado));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.ESTADO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.PAIS
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    [ValorVendasQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedValorVendas.
	    public IEnumerable<ValorVendas> GetPagedValorVendas(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedValorVendas")))
 	        {
 	             AuthorizationResult authorizationResult = (new ValorVendasQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<ValorVendas> result = this.GetOlapValorVendas(entitySearchList);
	  	
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetValorVendasCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    [ArquivoUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update Arquivo.
	    public void UpdateArquivo(Arquivo entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateArquivo")))
 	        {
 	             AuthorizationResult authorizationResult = (new ArquivoUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	    }

	    [ArquivoInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert Arquivo.
	    public void InsertArquivo(Arquivo entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertArquivo")))
 	        {
 	             AuthorizationResult authorizationResult = (new ArquivoInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	    }

	    [ArquivoDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete Arquivo.
	    public void DeleteArquivo(Arquivo entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteArquivo")))
 	        {
 	             AuthorizationResult authorizationResult = (new ArquivoDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	    }
		
			
	    [PaisUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update Pais.
	    public void UpdatePais(Pais entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdatePais")))
 	        {
 	             AuthorizationResult authorizationResult = (new PaisUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    [PaisInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert Pais.
	    public void InsertPais(Pais entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertPais")))
 	        {
 	             AuthorizationResult authorizationResult = (new PaisInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    [PaisDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete Pais.
	    public void DeletePais(Pais entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeletePais")))
 	        {
 	             AuthorizationResult authorizationResult = (new PaisDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    [EstadoUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update Estado.
	    public void UpdateEstado(Estado entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateEstado")))
 	        {
 	             AuthorizationResult authorizationResult = (new EstadoUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.Pais.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.Pais) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.Pais); 	
	            

	
	        }
	
	    }

	    [EstadoInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert Estado.
	    public void InsertEstado(Estado entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertEstado")))
 	        {
 	             AuthorizationResult authorizationResult = (new EstadoInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.Pais.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.Pais) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.Pais);
	            

	
	        }
	
	    }

	    [EstadoDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete Estado.
	    public void DeleteEstado(Estado entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteEstado")))
 	        {
 	             AuthorizationResult authorizationResult = (new EstadoDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.Pais.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.Pais) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.Pais);
	            

	
	        }

	
	    }
		
			
	    [ValorVendasUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update ValorVendas.
	    public void UpdateValorVendas(ValorVendas entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateValorVendas")))
 	        {
 	             AuthorizationResult authorizationResult = (new ValorVendasUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	    }

	    [ValorVendasInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert ValorVendas.
	    public void InsertValorVendas(ValorVendas entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertValorVendas")))
 	        {
 	             AuthorizationResult authorizationResult = (new ValorVendasInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	    }

	    [ValorVendasDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete ValorVendas.
	    public void DeleteValorVendas(ValorVendas entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteValorVendas")))
 	        {
 	             AuthorizationResult authorizationResult = (new ValorVendasDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}