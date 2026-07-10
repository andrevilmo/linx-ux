					
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

namespace Linx.Framework.BV.Auditoria
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="ADT_AUDITORIA.ID_ADT_AUDITORIA", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[AdtAuditoria,AdtAuditoria.AdtAuditoriaItem];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[];EdmEntityName[ADT_AUDITORIA];EntityRelations[TCS_USUARIO(TCS_USUARIO)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "AdtAuditoria")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Auditoria.AdtAuditoria")]
	public partial class AdtAuditoria : Linx.Data.Entity
	{

	
		
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.AdtAuditoriaItemList != null && this.AdtAuditoriaItemList.Count() > 0)
	      {
	         foreach (var entity in this.AdtAuditoriaItemList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.AdtAuditoriaItemList != null)
	      {
	         foreach (var detail in this.AdtAuditoriaItemList)
	         {
	            detail.ResetDetails();
	         }
	         this.AdtAuditoriaItemList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(AuditoriaDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("AdtAuditoriaItem"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("AdtAuditoriaItem");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdAdtAuditoria"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdAdtAuditoria));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load AdtAuditoriaItem and all sub-details
	         if (this.AdtAuditoriaItemList == null || this.AdtAuditoriaItemList.Count() == 0)
	         {
	             if (take > 0)
	                 this.AdtAuditoriaItemList = context.GetPagedAdtAuditoriaItem(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.AdtAuditoriaItemList = (from r in context.GetAdtAuditoriaItemByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _AdtAuditoriaItemElements = changeSet.ChangeSetEntries.Where(e => e.Entity is AdtAuditoriaItem && ((AdtAuditoriaItem)e.Entity).AdtAuditoria == null && e.Associations == null && e.OriginalAssociations == null && ((AdtAuditoriaItem)e.Entity).IdAdtAuditoria == this.IdAdtAuditoria).ToList();
 	      if (_AdtAuditoriaItemElements.Count > 0 && this.AdtAuditoriaItemList.Count() == 0)
 	      {
 	          this.AdtAuditoriaItemList = _AdtAuditoriaItemElements.Select(e => (AdtAuditoriaItem)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _AdtAuditoriaItemElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((AdtAuditoriaItem)detail.Entity).AdtAuditoria = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("AdtAuditoria", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("AdtAuditoriaItemList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For AssemblyName
	    partial void OnAssemblyNameChanging(string value);
	    partial void OnAssemblyNameChanged();

	    private string _AssemblyName;

	    [DataMember(IsRequired = true, Name = "AssemblyName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Assembly Name", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(500)]
	    [FunctionalPoint("Precision[500:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ADT_AUDITORIA.ASSEMBLY_NAME];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ADT_AUDITORIA.ASSEMBLY_NAME")]
	    public string AssemblyName
	    {
	    	    get
	    	    {
	    	          return _AssemblyName;
	    	    }
	    	    set
	    	    {
	    	          if (this._AssemblyName != value)
	    	          {
	    	              this.ValidateProperty("AssemblyName", value);
	    	              this.OnAssemblyNameChanging(value);
	    	              this.RaiseDataMemberChanging("AssemblyName");
	    	              this._AssemblyName = value;
	    	              this.RaiseDataMemberChanged("AssemblyName");
	    	              this.OnAssemblyNameChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ConnectionString
	    partial void OnConnectionStringChanging(string value);
	    partial void OnConnectionStringChanged();

	    private string _ConnectionString;

	    [DataMember(IsRequired = true, Name = "ConnectionString", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Connection String", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(500)]
	    [FunctionalPoint("Precision[500:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ADT_AUDITORIA.CONNECTION_STRING];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ADT_AUDITORIA.CONNECTION_STRING")]
	    public string ConnectionString
	    {
	    	    get
	    	    {
	    	          return _ConnectionString;
	    	    }
	    	    set
	    	    {
	    	          if (this._ConnectionString != value)
	    	          {
	    	              this.ValidateProperty("ConnectionString", value);
	    	              this.OnConnectionStringChanging(value);
	    	              this.RaiseDataMemberChanging("ConnectionString");
	    	              this._ConnectionString = value;
	    	              this.RaiseDataMemberChanged("ConnectionString");
	    	              this.OnConnectionStringChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DataHora
	    partial void OnDataHoraChanging(DateTime value);
	    partial void OnDataHoraChanged();

	    private DateTime _DataHora;

	    [DataMember(IsRequired = true, Name = "DataHora", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Data Hora", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ADT_AUDITORIA.DATA_HORA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ADT_AUDITORIA.DATA_HORA")]
	    public DateTime DataHora
	    {
	    	    get
	    	    {
	    	          return _DataHora;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataHora != value)
	    	          {
	    	              this.ValidateProperty("DataHora", value);
	    	              this.OnDataHoraChanging(value);
	    	              this.RaiseDataMemberChanging("DataHora");
	    	              this._DataHora = value;
	    	              this.RaiseDataMemberChanged("DataHora");
	    	              this.OnDataHoraChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Email
	    partial void OnEmailChanging(string value);
	    partial void OnEmailChanged();

	    private string _Email;

	    [DataMember(Name = "Email", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Email", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuario];LookUpTitle[Seleção de (Email)];LookUpQuery[executeLookUpTcsUsuario];LookUpFinalize[finalizeLookUpTcsUsuario];LookUpDisplayColumns[{\"Email\" : \"Email\", \"IdUsuario\" : \"Id Usuario\", \"NomeUsuario\" : \"Nome Usuario\"}];LookUpColumns[{\"Email\" : true, \"IdUsuario\" : true, \"NomeUsuario\" : true}];FilterDataKey[ADT_AUDITORIA.TCS_USUARIO.EMAIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#Email#false##250:0##Email#0#true##::LookUpTcsUsuario##false#false#TCS_USUARIO#TCS_USUARIO#Linx.Framework.BV.Auditoria#IQueryable###true#false", EdmKey="ADT_AUDITORIA.TCS_USUARIO.EMAIL")]
	    public string Email
	    {
	    	    get
	    	    {
	    	          return _Email;
	    	    }
	    	    set
	    	    {
	    	          if (this._Email != value)
	    	          {
	    	              this.ValidateProperty("Email", value);
	    	              this.OnEmailChanging(value);
	    	              this.RaiseDataMemberChanging("Email");
	    	              this._Email = value;
	    	              this.RaiseDataMemberChanged("Email");
	    	              this.OnEmailChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdAdtAuditoria
	    partial void OnIdAdtAuditoriaChanging(long value);
	    partial void OnIdAdtAuditoriaChanged();

	    private long _IdAdtAuditoria;

	    [DataMember(IsRequired = true, Name = "IdAdtAuditoria", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Adt Auditoria", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ADT_AUDITORIA.ID_ADT_AUDITORIA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ADT_AUDITORIA.ID_ADT_AUDITORIA")]
	    public long IdAdtAuditoria
	    {
	    	    get
	    	    {
	    	          return _IdAdtAuditoria;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdAdtAuditoria != value)
	    	          {
	    	              this.ValidateProperty("IdAdtAuditoria", value);
	    	              this.OnIdAdtAuditoriaChanging(value);
	    	              this.RaiseDataMemberChanging("IdAdtAuditoria");
	    	              this._IdAdtAuditoria = value;
	    	              this.RaiseDataMemberChanged("IdAdtAuditoria");
	    	              this.OnIdAdtAuditoriaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuario
	    partial void OnIdUsuarioChanging(long value);
	    partial void OnIdUsuarioChanged();

	    private long _IdUsuario;

	    [DataMember(IsRequired = true, Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuario];LookUpTitle[Seleção de (Id Usuario)];LookUpQuery[executeLookUpTcsUsuario];LookUpFinalize[finalizeLookUpTcsUsuario];LookUpDisplayColumns[{\"Email\" : \"Email\", \"IdUsuario\" : \"Id Usuario\", \"NomeUsuario\" : \"Nome Usuario\"}];LookUpColumns[{\"Email\" : true, \"IdUsuario\" : true, \"NomeUsuario\" : true}];FilterDataKey[ADT_AUDITORIA.TCS_USUARIO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="long#IdUsuario#true##0:0##Id Usuario#1#true##::LookUpTcsUsuario##false#false#TCS_USUARIO#TCS_USUARIO#Linx.Framework.BV.Auditoria#IQueryable###true#false", EdmKey="ADT_AUDITORIA.TCS_USUARIO.ID_USUARIO")]
	    public long IdUsuario
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
	    partial void OnNomeUsuarioChanging(string value);
	    partial void OnNomeUsuarioChanged();

	    private string _NomeUsuario;

	    [DataMember(IsRequired = true, Name = "NomeUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Usuario", Description="", Order = 17, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuario];LookUpTitle[Seleção de (Nome Usuario)];LookUpQuery[executeLookUpTcsUsuario];LookUpFinalize[finalizeLookUpTcsUsuario];LookUpDisplayColumns[{\"Email\" : \"Email\", \"IdUsuario\" : \"Id Usuario\", \"NomeUsuario\" : \"Nome Usuario\"}];LookUpColumns[{\"Email\" : true, \"IdUsuario\" : true, \"NomeUsuario\" : true}];FilterDataKey[ADT_AUDITORIA.TCS_USUARIO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#NomeUsuario#false##250:0##Nome Usuario#2#true##::LookUpTcsUsuario##false#false#TCS_USUARIO#TCS_USUARIO#Linx.Framework.BV.Auditoria#IQueryable###true#false", EdmKey="ADT_AUDITORIA.TCS_USUARIO.NOME_USUARIO")]
	    public string NomeUsuario
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

	    #endregion Data Properties

	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<AdtAuditoriaItem> _AdtAuditoriaItemList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_AdtAuditoria_AdtAuditoriaItem", "IdAdtAuditoria", "IdAdtAuditoria", IsForeignKey=false)]
	    [DataMember(Name = "AdtAuditoriaItemList", EmitDefaultValue = true)]
	    public IEnumerable<AdtAuditoriaItem> AdtAuditoriaItemList
	    {
	        get
	        {
	
	            if (this._AdtAuditoriaItemList == null)
	            	this._AdtAuditoriaItemList = new List<AdtAuditoriaItem>();
	
	            return this._AdtAuditoriaItemList;
	        }
	        set
	        {
	            if (this._AdtAuditoriaItemList != value)
	            {
	                this._AdtAuditoriaItemList = value;
	                this.RaisePropertyChanged("AdtAuditoriaItemList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.ADT_AUDITORIA").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.ADT_AUDITORIA), QualifiedEntitySetName = "ControleSistemaContext.ADT_AUDITORIA" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ADT_AUDITORIA.DATA_HORA", Source = "DataHora", Target = "DATA_HORA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.ADT_AUDITORIA", RelationPropertyName = "ADT_AUDITORIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ADT_AUDITORIA.ASSEMBLY_NAME", Source = "AssemblyName", Target = "ASSEMBLY_NAME", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.ADT_AUDITORIA", RelationPropertyName = "ADT_AUDITORIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ADT_AUDITORIA.ID_ADT_AUDITORIA", Source = "IdAdtAuditoria", Target = "ID_ADT_AUDITORIA", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.ADT_AUDITORIA", RelationPropertyName = "ADT_AUDITORIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ADT_AUDITORIA.CONNECTION_STRING", Source = "ConnectionString", Target = "CONNECTION_STRING", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.ADT_AUDITORIA", RelationPropertyName = "ADT_AUDITORIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ADT_AUDITORIA.TCS_USUARIO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });

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

		

	[LinxPublicationView(PrimaryKeys="ADT_AUDITORIA_ITEM.ID_ADT_AUDITORIA_ITEM", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#.ADT_AUDITORIA_ITEM_LISTA as #Alias#];EdmEntityName[ADT_AUDITORIA_ITEM];EntityRelations[ADT_AUDITORIA(ADT_AUDITORIA)#TCS_USUARIO(TCS_USUARIO)];EdmParentEntityName[ADT_AUDITORIA];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "AdtAuditoriaItem")]
	[Serializable()]
	public partial class AdtAuditoriaItem : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(AuditoriaDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("AdtAuditoria");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdAdtAuditoria"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdAdtAuditoria));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load AdtAuditoria
	         this.AdtAuditoria = (from r in context.GetAdtAuditoriaByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For IdAdtAuditoria
	    partial void OnIdAdtAuditoriaChanging(long value);
	    partial void OnIdAdtAuditoriaChanged();

	    private long _IdAdtAuditoria;

	    [DataMember(IsRequired = true, Name = "IdAdtAuditoria", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Adt Auditoria", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ADT_AUDITORIA_ITEM.ADT_AUDITORIA.ID_ADT_AUDITORIA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ADT_AUDITORIA_ITEM.ADT_AUDITORIA.ID_ADT_AUDITORIA")]
	    public long IdAdtAuditoria
	    {
	    	    get
	    	    {
	    	          return _IdAdtAuditoria;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdAdtAuditoria != value)
	    	          {
	    	              this.ValidateProperty("IdAdtAuditoria", value);
	    	              this.OnIdAdtAuditoriaChanging(value);
	    	              this.RaiseDataMemberChanging("IdAdtAuditoria");
	    	              this._IdAdtAuditoria = value;
	    	              this.RaiseDataMemberChanged("IdAdtAuditoria");
	    	              this.OnIdAdtAuditoriaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdAdtAuditoriaItem
	    partial void OnIdAdtAuditoriaItemChanging(long value);
	    partial void OnIdAdtAuditoriaItemChanged();

	    private long _IdAdtAuditoriaItem;

	    [DataMember(IsRequired = true, Name = "IdAdtAuditoriaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Adt Auditoria Item", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ADT_AUDITORIA_ITEM.ID_ADT_AUDITORIA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ADT_AUDITORIA_ITEM.ID_ADT_AUDITORIA_ITEM")]
	    public long IdAdtAuditoriaItem
	    {
	    	    get
	    	    {
	    	          return _IdAdtAuditoriaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdAdtAuditoriaItem != value)
	    	          {
	    	              this.ValidateProperty("IdAdtAuditoriaItem", value);
	    	              this.OnIdAdtAuditoriaItemChanging(value);
	    	              this.RaiseDataMemberChanging("IdAdtAuditoriaItem");
	    	              this._IdAdtAuditoriaItem = value;
	    	              this.RaiseDataMemberChanged("IdAdtAuditoriaItem");
	    	              this.OnIdAdtAuditoriaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeTabela
	    partial void OnNomeTabelaChanging(string value);
	    partial void OnNomeTabelaChanged();

	    private string _NomeTabela;

	    [DataMember(IsRequired = true, Name = "NomeTabela", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Tabela", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(100)]
	    [FunctionalPoint("Precision[100:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ADT_AUDITORIA_ITEM.NOME_TABELA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ADT_AUDITORIA_ITEM.NOME_TABELA")]
	    public string NomeTabela
	    {
	    	    get
	    	    {
	    	          return _NomeTabela;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeTabela != value)
	    	          {
	    	              this.ValidateProperty("NomeTabela", value);
	    	              this.OnNomeTabelaChanging(value);
	    	              this.RaiseDataMemberChanging("NomeTabela");
	    	              this._NomeTabela = value;
	    	              this.RaiseDataMemberChanged("NomeTabela");
	    	              this.OnNomeTabelaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For SchemaTabela
	    partial void OnSchemaTabelaChanging(string value);
	    partial void OnSchemaTabelaChanged();

	    private string _SchemaTabela;

	    [DataMember(IsRequired = true, Name = "SchemaTabela", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Schema Tabela", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ADT_AUDITORIA_ITEM.SCHEMA_TABELA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ADT_AUDITORIA_ITEM.SCHEMA_TABELA")]
	    public string SchemaTabela
	    {
	    	    get
	    	    {
	    	          return _SchemaTabela;
	    	    }
	    	    set
	    	    {
	    	          if (this._SchemaTabela != value)
	    	          {
	    	              this.ValidateProperty("SchemaTabela", value);
	    	              this.OnSchemaTabelaChanging(value);
	    	              this.RaiseDataMemberChanging("SchemaTabela");
	    	              this._SchemaTabela = value;
	    	              this.RaiseDataMemberChanged("SchemaTabela");
	    	              this.OnSchemaTabelaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For TipoOperacao
	    partial void OnTipoOperacaoChanging(string value);
	    partial void OnTipoOperacaoChanged();

	    private string _TipoOperacao;

	    [DataMember(IsRequired = true, Name = "TipoOperacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo Operacao", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(1)]
	    [FunctionalPoint("Precision[1:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TIPO_OPERACAO];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ADT_AUDITORIA_ITEM.TIPO_OPERACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ADT_AUDITORIA_ITEM.TIPO_OPERACAO")]
	    public string TipoOperacao
	    {
	    	    get
	    	    {
	    	          return _TipoOperacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._TipoOperacao != value)
	    	          {
	    	              this.ValidateProperty("TipoOperacao", value);
	    	              this.OnTipoOperacaoChanging(value);
	    	              this.RaiseDataMemberChanging("TipoOperacao");
	    	              this._TipoOperacao = value;
	    	              this.RaiseDataMemberChanged("TipoOperacao");
	    	              this.OnTipoOperacaoChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private AdtAuditoria _AdtAuditoria;
	    [DataMember(Name = "AdtAuditoria", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_AdtAuditoria_AdtAuditoriaItem", "IdAdtAuditoria", "IdAdtAuditoria", IsForeignKey=true)]
	    public AdtAuditoria AdtAuditoria
	    {
	        get
	        {
	            return this._AdtAuditoria;
	        }
	        set
	        {
	            if (this._AdtAuditoria != value)
	            {
	                this._AdtAuditoria = value;
	                this.RaisePropertyChanged("AdtAuditoriaList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.ADT_AUDITORIA_ITEM").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.ADT_AUDITORIA_ITEM), QualifiedEntitySetName = "ControleSistemaContext.ADT_AUDITORIA_ITEM" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ADT_AUDITORIA_ITEM.NOME_TABELA", Source = "NomeTabela", Target = "NOME_TABELA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.ADT_AUDITORIA_ITEM", RelationPropertyName = "ADT_AUDITORIA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ADT_AUDITORIA_ITEM.SCHEMA_TABELA", Source = "SchemaTabela", Target = "SCHEMA_TABELA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.ADT_AUDITORIA_ITEM", RelationPropertyName = "ADT_AUDITORIA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ADT_AUDITORIA_ITEM.TIPO_OPERACAO", Source = "TipoOperacao", Target = "TIPO_OPERACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.ADT_AUDITORIA_ITEM", RelationPropertyName = "ADT_AUDITORIA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ADT_AUDITORIA_ITEM.ID_ADT_AUDITORIA_ITEM", Source = "IdAdtAuditoriaItem", Target = "ID_ADT_AUDITORIA_ITEM", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.ADT_AUDITORIA_ITEM", RelationPropertyName = "ADT_AUDITORIA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ADT_AUDITORIA_ITEM.ADT_AUDITORIA.ID_ADT_AUDITORIA", Source = "IdAdtAuditoria", Target = "ID_ADT_AUDITORIA", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.ADT_AUDITORIA", RelationPropertyName = "ADT_AUDITORIA" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetTipoOperacaoValues()
	    {
	    	    return Linx.Framework.BV.Domains.TIPO_OPERACAO.GetValues();
	    }
	    private string _tipoOperacaoName;
	    [DataMember(IsRequired = false, Name = "TipoOperacaoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo Operacao", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string TipoOperacaoName
	    {
	    	    get { if (this.TipoOperacao.IsNull()) { _tipoOperacaoName = String.Empty; } else { string key = this.TipoOperacao.ToString(); var dmValues = this.GetTipoOperacaoValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _tipoOperacaoName) _tipoOperacaoName = domainName; } return _tipoOperacaoName; } set { _tipoOperacaoName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="ADT_AUDITORIA_ITEM_DETALHE.ID_ADT_AUDITORIA_ITEM_DETALHE", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[AdtAuditoriaItemDetalhe];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[];EdmEntityName[ADT_AUDITORIA_ITEM_DETALHE];EntityRelations[ADT_AUDITORIA_ITEM(ADT_AUDITORIA_ITEM)#ADT_AUDITORIA(ADT_AUDITORIA)#TCS_USUARIO(TCS_USUARIO)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "AdtAuditoriaItemDetalhe")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Auditoria.AdtAuditoriaItemDetalhe")]
	public partial class AdtAuditoriaItemDetalhe : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For IdAdtAuditoriaItem
	    partial void OnIdAdtAuditoriaItemChanging(long value);
	    partial void OnIdAdtAuditoriaItemChanged();

	    private long _IdAdtAuditoriaItem;

	    [DataMember(IsRequired = true, Name = "IdAdtAuditoriaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Adt Auditoria Item", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ADT_AUDITORIA_ITEM_DETALHE.ADT_AUDITORIA_ITEM.ID_ADT_AUDITORIA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ADT_AUDITORIA_ITEM_DETALHE.ADT_AUDITORIA_ITEM.ID_ADT_AUDITORIA_ITEM")]
	    public long IdAdtAuditoriaItem
	    {
	    	    get
	    	    {
	    	          return _IdAdtAuditoriaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdAdtAuditoriaItem != value)
	    	          {
	    	              this.ValidateProperty("IdAdtAuditoriaItem", value);
	    	              this.OnIdAdtAuditoriaItemChanging(value);
	    	              this.RaiseDataMemberChanging("IdAdtAuditoriaItem");
	    	              this._IdAdtAuditoriaItem = value;
	    	              this.RaiseDataMemberChanged("IdAdtAuditoriaItem");
	    	              this.OnIdAdtAuditoriaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdAdtAuditoriaItemDetalhe
	    partial void OnIdAdtAuditoriaItemDetalheChanging(long value);
	    partial void OnIdAdtAuditoriaItemDetalheChanged();

	    private long _IdAdtAuditoriaItemDetalhe;

	    [DataMember(IsRequired = true, Name = "IdAdtAuditoriaItemDetalhe", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Adt Auditoria Item Detalhe", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ADT_AUDITORIA_ITEM_DETALHE.ID_ADT_AUDITORIA_ITEM_DETALHE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ADT_AUDITORIA_ITEM_DETALHE.ID_ADT_AUDITORIA_ITEM_DETALHE")]
	    public long IdAdtAuditoriaItemDetalhe
	    {
	    	    get
	    	    {
	    	          return _IdAdtAuditoriaItemDetalhe;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdAdtAuditoriaItemDetalhe != value)
	    	          {
	    	              this.ValidateProperty("IdAdtAuditoriaItemDetalhe", value);
	    	              this.OnIdAdtAuditoriaItemDetalheChanging(value);
	    	              this.RaiseDataMemberChanging("IdAdtAuditoriaItemDetalhe");
	    	              this._IdAdtAuditoriaItemDetalhe = value;
	    	              this.RaiseDataMemberChanged("IdAdtAuditoriaItemDetalhe");
	    	              this.OnIdAdtAuditoriaItemDetalheChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Propriedade
	    partial void OnPropriedadeChanging(string value);
	    partial void OnPropriedadeChanged();

	    private string _Propriedade;

	    [DataMember(Name = "Propriedade", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Propriedade", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(100)]
	    [FunctionalPoint("Precision[100:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ADT_AUDITORIA_ITEM_DETALHE.PROPRIEDADE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ADT_AUDITORIA_ITEM_DETALHE.PROPRIEDADE")]
	    public string Propriedade
	    {
	    	    get
	    	    {
	    	          return _Propriedade;
	    	    }
	    	    set
	    	    {
	    	          if (this._Propriedade != value)
	    	          {
	    	              this.ValidateProperty("Propriedade", value);
	    	              this.OnPropriedadeChanging(value);
	    	              this.RaiseDataMemberChanging("Propriedade");
	    	              this._Propriedade = value;
	    	              this.RaiseDataMemberChanged("Propriedade");
	    	              this.OnPropriedadeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ValorAntigo
	    partial void OnValorAntigoChanging(string value);
	    partial void OnValorAntigoChanged();

	    private string _ValorAntigo;

	    [DataMember(Name = "ValorAntigo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Valor Antigo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ADT_AUDITORIA_ITEM_DETALHE.VALOR_ANTIGO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ADT_AUDITORIA_ITEM_DETALHE.VALOR_ANTIGO")]
	    public string ValorAntigo
	    {
	    	    get
	    	    {
	    	          return _ValorAntigo;
	    	    }
	    	    set
	    	    {
	    	          if (this._ValorAntigo != value)
	    	          {
	    	              this.ValidateProperty("ValorAntigo", value);
	    	              this.OnValorAntigoChanging(value);
	    	              this.RaiseDataMemberChanging("ValorAntigo");
	    	              this._ValorAntigo = value;
	    	              this.RaiseDataMemberChanged("ValorAntigo");
	    	              this.OnValorAntigoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ValorNovo
	    partial void OnValorNovoChanging(string value);
	    partial void OnValorNovoChanged();

	    private string _ValorNovo;

	    [DataMember(Name = "ValorNovo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Valor Novo", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ADT_AUDITORIA_ITEM_DETALHE.VALOR_NOVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ADT_AUDITORIA_ITEM_DETALHE.VALOR_NOVO")]
	    public string ValorNovo
	    {
	    	    get
	    	    {
	    	          return _ValorNovo;
	    	    }
	    	    set
	    	    {
	    	          if (this._ValorNovo != value)
	    	          {
	    	              this.ValidateProperty("ValorNovo", value);
	    	              this.OnValorNovoChanging(value);
	    	              this.RaiseDataMemberChanging("ValorNovo");
	    	              this._ValorNovo = value;
	    	              this.RaiseDataMemberChanged("ValorNovo");
	    	              this.OnValorNovoChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.ADT_AUDITORIA_ITEM_DETALHE").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.ADT_AUDITORIA_ITEM_DETALHE), QualifiedEntitySetName = "ControleSistemaContext.ADT_AUDITORIA_ITEM_DETALHE" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ADT_AUDITORIA_ITEM_DETALHE.VALOR_NOVO", Source = "ValorNovo", Target = "VALOR_NOVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.ADT_AUDITORIA_ITEM_DETALHE", RelationPropertyName = "ADT_AUDITORIA_ITEM_DETALHE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ADT_AUDITORIA_ITEM_DETALHE.PROPRIEDADE", Source = "Propriedade", Target = "PROPRIEDADE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.ADT_AUDITORIA_ITEM_DETALHE", RelationPropertyName = "ADT_AUDITORIA_ITEM_DETALHE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ADT_AUDITORIA_ITEM_DETALHE.VALOR_ANTIGO", Source = "ValorAntigo", Target = "VALOR_ANTIGO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.ADT_AUDITORIA_ITEM_DETALHE", RelationPropertyName = "ADT_AUDITORIA_ITEM_DETALHE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ADT_AUDITORIA_ITEM_DETALHE.ID_ADT_AUDITORIA_ITEM_DETALHE", Source = "IdAdtAuditoriaItemDetalhe", Target = "ID_ADT_AUDITORIA_ITEM_DETALHE", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.ADT_AUDITORIA_ITEM_DETALHE", RelationPropertyName = "ADT_AUDITORIA_ITEM_DETALHE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ADT_AUDITORIA_ITEM_DETALHE.ADT_AUDITORIA_ITEM.ID_ADT_AUDITORIA_ITEM", Source = "IdAdtAuditoriaItem", Target = "ID_ADT_AUDITORIA_ITEM", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.ADT_AUDITORIA_ITEM", RelationPropertyName = "ADT_AUDITORIA_ITEM" });

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

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#.ADT_AUDITORIA_ITEM_LISTA as #Alias#];EdmEntityName[ADT_AUDITORIA_ITEM];EntityRelations[ADT_AUDITORIA(ADT_AUDITORIA)#TCS_USUARIO(TCS_USUARIO)];EdmParentEntityName[ADT_AUDITORIA];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "AdtAuditoriaItem")]
	[Serializable()]
	public partial class AdtAuditoriaItemParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For IdAdtAuditoria
	    partial void OnIdAdtAuditoriaChanging(long value);
	    partial void OnIdAdtAuditoriaChanged();

	    private long _IdAdtAuditoria;

	    [DataMember(IsRequired = true, Name = "IdAdtAuditoria", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Adt Auditoria", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ADT_AUDITORIA_ITEM.ADT_AUDITORIA.ID_ADT_AUDITORIA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ADT_AUDITORIA_ITEM.ADT_AUDITORIA.ID_ADT_AUDITORIA")]
	    public long IdAdtAuditoria
	    {
	    	    get
	    	    {
	    	          return _IdAdtAuditoria;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdAdtAuditoria != value)
	    	          {
	    	              this.ValidateProperty("IdAdtAuditoria", value);
	    	              this.OnIdAdtAuditoriaChanging(value);
	    	              this.RaiseDataMemberChanging("IdAdtAuditoria");
	    	              this._IdAdtAuditoria = value;
	    	              this.RaiseDataMemberChanged("IdAdtAuditoria");
	    	              this.OnIdAdtAuditoriaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdAdtAuditoriaItem
	    partial void OnIdAdtAuditoriaItemChanging(long value);
	    partial void OnIdAdtAuditoriaItemChanged();

	    private long _IdAdtAuditoriaItem;

	    [DataMember(IsRequired = true, Name = "IdAdtAuditoriaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Adt Auditoria Item", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ADT_AUDITORIA_ITEM.ID_ADT_AUDITORIA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ADT_AUDITORIA_ITEM.ID_ADT_AUDITORIA_ITEM")]
	    public long IdAdtAuditoriaItem
	    {
	    	    get
	    	    {
	    	          return _IdAdtAuditoriaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdAdtAuditoriaItem != value)
	    	          {
	    	              this.ValidateProperty("IdAdtAuditoriaItem", value);
	    	              this.OnIdAdtAuditoriaItemChanging(value);
	    	              this.RaiseDataMemberChanging("IdAdtAuditoriaItem");
	    	              this._IdAdtAuditoriaItem = value;
	    	              this.RaiseDataMemberChanged("IdAdtAuditoriaItem");
	    	              this.OnIdAdtAuditoriaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeTabela
	    partial void OnNomeTabelaChanging(string value);
	    partial void OnNomeTabelaChanged();

	    private string _NomeTabela;

	    [DataMember(IsRequired = true, Name = "NomeTabela", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Tabela", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(100)]
	    [FunctionalPoint("Precision[100:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ADT_AUDITORIA_ITEM.NOME_TABELA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ADT_AUDITORIA_ITEM.NOME_TABELA")]
	    public string NomeTabela
	    {
	    	    get
	    	    {
	    	          return _NomeTabela;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeTabela != value)
	    	          {
	    	              this.ValidateProperty("NomeTabela", value);
	    	              this.OnNomeTabelaChanging(value);
	    	              this.RaiseDataMemberChanging("NomeTabela");
	    	              this._NomeTabela = value;
	    	              this.RaiseDataMemberChanged("NomeTabela");
	    	              this.OnNomeTabelaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For SchemaTabela
	    partial void OnSchemaTabelaChanging(string value);
	    partial void OnSchemaTabelaChanged();

	    private string _SchemaTabela;

	    [DataMember(IsRequired = true, Name = "SchemaTabela", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Schema Tabela", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ADT_AUDITORIA_ITEM.SCHEMA_TABELA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ADT_AUDITORIA_ITEM.SCHEMA_TABELA")]
	    public string SchemaTabela
	    {
	    	    get
	    	    {
	    	          return _SchemaTabela;
	    	    }
	    	    set
	    	    {
	    	          if (this._SchemaTabela != value)
	    	          {
	    	              this.ValidateProperty("SchemaTabela", value);
	    	              this.OnSchemaTabelaChanging(value);
	    	              this.RaiseDataMemberChanging("SchemaTabela");
	    	              this._SchemaTabela = value;
	    	              this.RaiseDataMemberChanged("SchemaTabela");
	    	              this.OnSchemaTabelaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For TipoOperacao
	    partial void OnTipoOperacaoChanging(string value);
	    partial void OnTipoOperacaoChanged();

	    private string _TipoOperacao;

	    [DataMember(IsRequired = true, Name = "TipoOperacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo Operacao", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(1)]
	    [FunctionalPoint("Precision[1:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TIPO_OPERACAO];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ADT_AUDITORIA_ITEM.TIPO_OPERACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ADT_AUDITORIA_ITEM.TIPO_OPERACAO")]
	    public string TipoOperacao
	    {
	    	    get
	    	    {
	    	          return _TipoOperacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._TipoOperacao != value)
	    	          {
	    	              this.ValidateProperty("TipoOperacao", value);
	    	              this.OnTipoOperacaoChanging(value);
	    	              this.RaiseDataMemberChanging("TipoOperacao");
	    	              this._TipoOperacao = value;
	    	              this.RaiseDataMemberChanged("TipoOperacao");
	    	              this.OnTipoOperacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For AssemblyName
	    partial void OnAssemblyNameChanging(string value);
	    partial void OnAssemblyNameChanged();

	    private string _AssemblyName;

	    [DataMember(IsRequired = true, Name = "AssemblyName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Assembly Name", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(500)]
	    [FunctionalPoint("Precision[500:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[ADT_AUDITORIA_ITEM.ADT_AUDITORIA.ASSEMBLY_NAME];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ADT_AUDITORIA.ASSEMBLY_NAME")]
	    public string AssemblyName
	    {
	    	    get
	    	    {
	    	          return _AssemblyName;
	    	    }
	    	    set
	    	    {
	    	          if (this._AssemblyName != value)
	    	          {
	    	              this.ValidateProperty("AssemblyName", value);
	    	              this.OnAssemblyNameChanging(value);
	    	              this.RaiseDataMemberChanging("AssemblyName");
	    	              this._AssemblyName = value;
	    	              this.RaiseDataMemberChanged("AssemblyName");
	    	              this.OnAssemblyNameChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ConnectionString
	    partial void OnConnectionStringChanging(string value);
	    partial void OnConnectionStringChanged();

	    private string _ConnectionString;

	    [DataMember(IsRequired = true, Name = "ConnectionString", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Connection String", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(500)]
	    [FunctionalPoint("Precision[500:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[ADT_AUDITORIA_ITEM.ADT_AUDITORIA.CONNECTION_STRING];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ADT_AUDITORIA.CONNECTION_STRING")]
	    public string ConnectionString
	    {
	    	    get
	    	    {
	    	          return _ConnectionString;
	    	    }
	    	    set
	    	    {
	    	          if (this._ConnectionString != value)
	    	          {
	    	              this.ValidateProperty("ConnectionString", value);
	    	              this.OnConnectionStringChanging(value);
	    	              this.RaiseDataMemberChanging("ConnectionString");
	    	              this._ConnectionString = value;
	    	              this.RaiseDataMemberChanged("ConnectionString");
	    	              this.OnConnectionStringChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DataHora
	    partial void OnDataHoraChanging(DateTime value);
	    partial void OnDataHoraChanged();

	    private DateTime _DataHora;

	    [DataMember(IsRequired = true, Name = "DataHora", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Data Hora", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[ADT_AUDITORIA_ITEM.ADT_AUDITORIA.DATA_HORA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ADT_AUDITORIA.DATA_HORA")]
	    public DateTime DataHora
	    {
	    	    get
	    	    {
	    	          return _DataHora;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataHora != value)
	    	          {
	    	              this.ValidateProperty("DataHora", value);
	    	              this.OnDataHoraChanging(value);
	    	              this.RaiseDataMemberChanging("DataHora");
	    	              this._DataHora = value;
	    	              this.RaiseDataMemberChanged("DataHora");
	    	              this.OnDataHoraChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Email
	    partial void OnEmailChanging(string value);
	    partial void OnEmailChanged();

	    private string _Email;

	    [DataMember(Name = "Email", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Email", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[ADT_AUDITORIA_ITEM.ADT_AUDITORIA.TCS_USUARIO.EMAIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ADT_AUDITORIA.TCS_USUARIO.EMAIL")]
	    public string Email
	    {
	    	    get
	    	    {
	    	          return _Email;
	    	    }
	    	    set
	    	    {
	    	          if (this._Email != value)
	    	          {
	    	              this.ValidateProperty("Email", value);
	    	              this.OnEmailChanging(value);
	    	              this.RaiseDataMemberChanging("Email");
	    	              this._Email = value;
	    	              this.RaiseDataMemberChanged("Email");
	    	              this.OnEmailChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuario
	    partial void OnIdUsuarioChanging(long value);
	    partial void OnIdUsuarioChanged();

	    private long _IdUsuario;

	    [DataMember(IsRequired = true, Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[ADT_AUDITORIA_ITEM.ADT_AUDITORIA.TCS_USUARIO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ADT_AUDITORIA.TCS_USUARIO.ID_USUARIO")]
	    public long IdUsuario
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
	    partial void OnNomeUsuarioChanging(string value);
	    partial void OnNomeUsuarioChanged();

	    private string _NomeUsuario;

	    [DataMember(IsRequired = true, Name = "NomeUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Usuario", Description="", Order = 17, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[ADT_AUDITORIA_ITEM.ADT_AUDITORIA.TCS_USUARIO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ADT_AUDITORIA.TCS_USUARIO.NOME_USUARIO")]
	    public string NomeUsuario
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

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.ADT_AUDITORIA_ITEM").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.ADT_AUDITORIA_ITEM), QualifiedEntitySetName = "ControleSistemaContext.ADT_AUDITORIA_ITEM" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ADT_AUDITORIA_ITEM.NOME_TABELA", Source = "NomeTabela", Target = "NOME_TABELA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.ADT_AUDITORIA_ITEM", RelationPropertyName = "ADT_AUDITORIA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ADT_AUDITORIA_ITEM.SCHEMA_TABELA", Source = "SchemaTabela", Target = "SCHEMA_TABELA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.ADT_AUDITORIA_ITEM", RelationPropertyName = "ADT_AUDITORIA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ADT_AUDITORIA_ITEM.TIPO_OPERACAO", Source = "TipoOperacao", Target = "TIPO_OPERACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.ADT_AUDITORIA_ITEM", RelationPropertyName = "ADT_AUDITORIA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ADT_AUDITORIA_ITEM.ID_ADT_AUDITORIA_ITEM", Source = "IdAdtAuditoriaItem", Target = "ID_ADT_AUDITORIA_ITEM", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.ADT_AUDITORIA_ITEM", RelationPropertyName = "ADT_AUDITORIA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ADT_AUDITORIA_ITEM.ADT_AUDITORIA.ID_ADT_AUDITORIA", Source = "IdAdtAuditoria", Target = "ID_ADT_AUDITORIA", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.ADT_AUDITORIA", RelationPropertyName = "ADT_AUDITORIA" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetTipoOperacaoValues()
	    {
	    	    return Linx.Framework.BV.Domains.TIPO_OPERACAO.GetValues();
	    }
	    private string _tipoOperacaoName;
	    [DataMember(IsRequired = false, Name = "TipoOperacaoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo Operacao", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string TipoOperacaoName
	    {
	    	    get { if (this.TipoOperacao.IsNull()) { _tipoOperacaoName = String.Empty; } else { string key = this.TipoOperacao.ToString(); var dmValues = this.GetTipoOperacaoValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _tipoOperacaoName) _tipoOperacaoName = domainName; } return _tipoOperacaoName; } set { _tipoOperacaoName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	//////////////////////// DomainService Class V1 ///////////////////////
	///////////////////////////////////////////////////////////////////////
	[EnableClientAccess()]	
	[DomainIdentifier("ProcessorOverviewAuditoriaDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class AuditoriaDomainService : DomainService, IDataServiceContext 
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

		
	    public AuditoriaDomainService() : this("", null, null) { }
	    public AuditoriaDomainService(string connectionString) : this(connectionString, null, null) { }
	    public AuditoriaDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public AuditoriaDomainService(Linx.Framework.ControleSistema.BM.ControleSistemaContext dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public AuditoriaDomainService(string connectionString, Linx.Framework.ControleSistema.BM.ControleSistemaContext dataContext, Dictionary<string, string> headers) : base() 
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
 	        var _AdtAuditoriaElements = changeSet.ChangeSetEntries.Where(e => e.Entity is AdtAuditoria && e.Entity.GetType().Name == "AdtAuditoria" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _AdtAuditoriaElements)
 	           if (((AdtAuditoria)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 
 	        //Remove inconsistent details
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is AdtAuditoriaItem && e.Entity.GetType().Name == "AdtAuditoriaItem" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
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
	            
                Email = entity.EMAIL
                , IdUsuario = entity.ID_USUARIO
                , NomeUsuario = entity.NOME_USUARIO
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
	
		

	        if (entityName.InList("Linx.Framework.BV.Auditoria.AdtAuditoria"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "AdtAuditoria",
	        			NameSpace = "Linx.Framework.BV.Auditoria",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "AdtAuditoria",
	        			ClearMethodName = "ClearAdtAuditoria",
	        			QueryMethodName  = "GetPagedAdtAuditoria",	
	        			CountingMethodName  = "GetAdtAuditoria" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Auditoria.AdtAuditoria"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Auditoria.AdtAuditoria"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Auditoria.AdtAuditoria", "Linx.Framework.BV.Auditoria.AdtAuditoriaItem"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "AdtAuditoriaItem" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.Auditoria",
	        			HasQuickSearch = false,
	        			ParentClassName = "AdtAuditoria",	
	        			DisplayName = "AdtAuditoriaItem",
	        			ClearMethodName = "ClearAdtAuditoriaItem" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedAdtAuditoriaItem" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetAdtAuditoriaItem" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Auditoria.AdtAuditoriaItem"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Auditoria.AdtAuditoriaItem" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Auditoria.AdtAuditoriaItemDetalhe"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "AdtAuditoriaItemDetalhe",
	        			NameSpace = "Linx.Framework.BV.Auditoria",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "AdtAuditoriaItemDetalhe",
	        			ClearMethodName = "ClearAdtAuditoriaItemDetalhe",
	        			QueryMethodName  = "GetPagedAdtAuditoriaItemDetalhe",	
	        			CountingMethodName  = "GetAdtAuditoriaItemDetalhe" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Auditoria.AdtAuditoriaItemDetalhe"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Auditoria.AdtAuditoriaItemDetalhe"), forceAll: forceAll)
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

         		    return new string[] { "Framework_AuditoriaClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.AuditoriaClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Framework_auditoriaService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.auditoriaService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear AdtAuditoria.
	    public IEnumerable<AdtAuditoria> ClearAdtAuditoria()
	    {
	        List<AdtAuditoria> result = new List<AdtAuditoria>();
	        result.Add(new AdtAuditoria());	
			
	        result[0].AdtAuditoriaItemList = new List<AdtAuditoriaItem>();
	        ((List<AdtAuditoriaItem>)result[0].AdtAuditoriaItemList).Add(new AdtAuditoriaItem());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear AdtAuditoriaItem.
	    public IEnumerable<AdtAuditoriaItem> ClearAdtAuditoriaItem()
	    {
	        List<AdtAuditoriaItem> result = new List<AdtAuditoriaItem>();
	        result.Add(new AdtAuditoriaItem());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear AdtAuditoriaItemDetalhe.
	    public IEnumerable<AdtAuditoriaItemDetalhe> ClearAdtAuditoriaItemDetalhe()
	    {
	        List<AdtAuditoriaItemDetalhe> result = new List<AdtAuditoriaItemDetalhe>();
	        result.Add(new AdtAuditoriaItemDetalhe());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    [AdtAuditoriaQueryCustomAuthorizationAuto()]
	    [Query(HasSideEffects = false)]
	    //Get AdtAuditoria.
	    public IQueryable<AdtAuditoria> GetAdtAuditoria()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetAdtAuditoria")))
 	        {
 	             AuthorizationResult authorizationResult = (new AdtAuditoriaQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<AdtAuditoria> result = 
	            (from entity0 in this.DbContext.ADT_AUDITORIA
                  let entity0Al1 = entity0.TCS_USUARIO
	            
	            	
	            select new AdtAuditoria()		
	            {
	            
                AssemblyName = entity0.ASSEMBLY_NAME
                , ConnectionString = entity0.CONNECTION_STRING
                , DataHora = entity0.DATA_HORA
                , Email = entity0Al1.EMAIL
                , IdAdtAuditoria = entity0.ID_ADT_AUDITORIA
                , IdUsuario = entity0Al1.ID_USUARIO
                , NomeUsuario = entity0Al1.NOME_USUARIO
			
                ,AdtAuditoriaItemList = 
	                        (from entity1 in entity0.ADT_AUDITORIA_ITEM_LISTA
                                  let entity1Al1 = entity1.ADT_AUDITORIA
	                        
	                        	
	                        select new AdtAuditoriaItem()
	                        {
	                        
                                IdAdtAuditoria = entity1Al1.ID_ADT_AUDITORIA
                                , IdAdtAuditoriaItem = entity1.ID_ADT_AUDITORIA_ITEM
                                , NomeTabela = entity1.NOME_TABELA
                                , SchemaTabela = entity1.SCHEMA_TABELA
                                , TipoOperacao = entity1.TIPO_OPERACAO
                                , TipoOperacaoName = ((entity1.TIPO_OPERACAO) == "I" ? "Inserção" : ((entity1.TIPO_OPERACAO) == "E" ? "Alteração" : ((entity1.TIPO_OPERACAO) == "D" ? "Exclusão" : "")))
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [AdtAuditoriaItemQueryCustomAuthorizationAuto()]
	    [Query(HasSideEffects = false)]
	    //Get AdtAuditoriaItem.
	    public IQueryable<AdtAuditoriaItem> GetAdtAuditoriaItem()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetAdtAuditoriaItem")))
 	        {
 	             AuthorizationResult authorizationResult = (new AdtAuditoriaItemQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<AdtAuditoriaItem> result = 
	            (from entity0 in this.DbContext.ADT_AUDITORIA_ITEM
                  let entity0Al1 = entity0.ADT_AUDITORIA
	            
	            	
	            select new AdtAuditoriaItem()		
	            {
	            
                IdAdtAuditoria = entity0Al1.ID_ADT_AUDITORIA
                , IdAdtAuditoriaItem = entity0.ID_ADT_AUDITORIA_ITEM
                , NomeTabela = entity0.NOME_TABELA
                , SchemaTabela = entity0.SCHEMA_TABELA
                , TipoOperacao = entity0.TIPO_OPERACAO
                , TipoOperacaoName = ((entity0.TIPO_OPERACAO) == "I" ? "Inserção" : ((entity0.TIPO_OPERACAO) == "E" ? "Alteração" : ((entity0.TIPO_OPERACAO) == "D" ? "Exclusão" : "")))
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [AdtAuditoriaQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get AdtAuditoriaNoAssociations.
	    public IQueryable<AdtAuditoria> GetAdtAuditoriaNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetAdtAuditoriaNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new AdtAuditoriaQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<AdtAuditoria> result = 
	            (from entity0 in this.DbContext.ADT_AUDITORIA
                  let entity0Al1 = entity0.TCS_USUARIO
	            
	            	
	            select new AdtAuditoria()		
	            {
	            
                AssemblyName = entity0.ASSEMBLY_NAME
                , ConnectionString = entity0.CONNECTION_STRING
                , DataHora = entity0.DATA_HORA
                , Email = entity0Al1.EMAIL
                , IdAdtAuditoria = entity0.ID_ADT_AUDITORIA
                , IdUsuario = entity0Al1.ID_USUARIO
                , NomeUsuario = entity0Al1.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [AdtAuditoriaItemQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get AdtAuditoriaItemNoAssociations.
	    public IQueryable<AdtAuditoriaItem> GetAdtAuditoriaItemNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetAdtAuditoriaItemNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new AdtAuditoriaItemQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<AdtAuditoriaItem> result = 
	            (from entity0 in this.DbContext.ADT_AUDITORIA_ITEM
                  let entity0Al1 = entity0.ADT_AUDITORIA
	            
	            	
	            select new AdtAuditoriaItem()		
	            {
	            
                IdAdtAuditoria = entity0Al1.ID_ADT_AUDITORIA
                , IdAdtAuditoriaItem = entity0.ID_ADT_AUDITORIA_ITEM
                , NomeTabela = entity0.NOME_TABELA
                , SchemaTabela = entity0.SCHEMA_TABELA
                , TipoOperacao = entity0.TIPO_OPERACAO
                , TipoOperacaoName = ((entity0.TIPO_OPERACAO) == "I" ? "Inserção" : ((entity0.TIPO_OPERACAO) == "E" ? "Alteração" : ((entity0.TIPO_OPERACAO) == "D" ? "Exclusão" : "")))
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [AdtAuditoriaItemDetalheQueryCustomAuthorizationAuto()]
	    [Query(HasSideEffects = false)]
	    //Get AdtAuditoriaItemDetalhe.
	    public IQueryable<AdtAuditoriaItemDetalhe> GetAdtAuditoriaItemDetalhe()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetAdtAuditoriaItemDetalhe")))
 	        {
 	             AuthorizationResult authorizationResult = (new AdtAuditoriaItemDetalheQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<AdtAuditoriaItemDetalhe> result = 
	            (from entity0 in this.DbContext.ADT_AUDITORIA_ITEM_DETALHE
                  let entity0Al1 = entity0.ADT_AUDITORIA_ITEM
	            
	            	
	            select new AdtAuditoriaItemDetalhe()		
	            {
	            
                IdAdtAuditoriaItem = entity0Al1.ID_ADT_AUDITORIA_ITEM
                , IdAdtAuditoriaItemDetalhe = entity0.ID_ADT_AUDITORIA_ITEM_DETALHE
                , Propriedade = entity0.PROPRIEDADE
                , ValorAntigo = entity0.VALOR_ANTIGO
                , ValorNovo = entity0.VALOR_NOVO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [AdtAuditoriaItemDetalheQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get AdtAuditoriaItemDetalheNoAssociations.
	    public IQueryable<AdtAuditoriaItemDetalhe> GetAdtAuditoriaItemDetalheNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetAdtAuditoriaItemDetalheNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new AdtAuditoriaItemDetalheQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<AdtAuditoriaItemDetalhe> result = 
	            (from entity0 in this.DbContext.ADT_AUDITORIA_ITEM_DETALHE
                  let entity0Al1 = entity0.ADT_AUDITORIA_ITEM
	            
	            	
	            select new AdtAuditoriaItemDetalhe()		
	            {
	            
                IdAdtAuditoriaItem = entity0Al1.ID_ADT_AUDITORIA_ITEM
                , IdAdtAuditoriaItemDetalhe = entity0.ID_ADT_AUDITORIA_ITEM_DETALHE
                , Propriedade = entity0.PROPRIEDADE
                , ValorAntigo = entity0.VALOR_ANTIGO
                , ValorNovo = entity0.VALOR_NOVO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	//Add filtering disabled property for ADT_AUDITORIA
	    	string[] bmDisabledAdtAuditoriaList = this.GetEDM().GetFilteringDisabledList("ADT_AUDITORIA");
	    	if (bmDisabledAdtAuditoriaList.Length > 0)
	    	{
	
	    		if (bmDisabledAdtAuditoriaList.Contains("ADT_AUDITORIA.ASSEMBLY_NAME"))
	    		{
	    			result.Add("AdtAuditoria|AssemblyName");
	    			result.Add("AdtAuditoria|ADT_AUDITORIA.ASSEMBLY_NAME");
	    		}
	
	    		if (bmDisabledAdtAuditoriaList.Contains("ADT_AUDITORIA.CONNECTION_STRING"))
	    		{
	    			result.Add("AdtAuditoria|ConnectionString");
	    			result.Add("AdtAuditoria|ADT_AUDITORIA.CONNECTION_STRING");
	    		}
	
	    		if (bmDisabledAdtAuditoriaList.Contains("ADT_AUDITORIA.DATA_HORA"))
	    		{
	    			result.Add("AdtAuditoria|DataHora");
	    			result.Add("AdtAuditoria|ADT_AUDITORIA.DATA_HORA");
	    		}
	
	    		if (bmDisabledAdtAuditoriaList.Contains("ADT_AUDITORIA.ID_ADT_AUDITORIA"))
	    		{
	    			result.Add("AdtAuditoria|IdAdtAuditoria");
	    			result.Add("AdtAuditoria|ADT_AUDITORIA.ID_ADT_AUDITORIA");
	    		}
	    	}
	    	//Add filtering disabled property for ADT_AUDITORIA_ITEM
	    	string[] bmDisabledAdtAuditoriaItemList = this.GetEDM().GetFilteringDisabledList("ADT_AUDITORIA_ITEM");
	    	if (bmDisabledAdtAuditoriaItemList.Length > 0)
	    	{
	
	    		if (bmDisabledAdtAuditoriaItemList.Contains("ADT_AUDITORIA_ITEM.ID_ADT_AUDITORIA_ITEM"))
	    		{
	    			result.Add("AdtAuditoriaItem|IdAdtAuditoriaItem");
	    			result.Add("AdtAuditoriaItem|ADT_AUDITORIA_ITEM.ID_ADT_AUDITORIA_ITEM");
	    		}
	
	    		if (bmDisabledAdtAuditoriaItemList.Contains("ADT_AUDITORIA_ITEM.NOME_TABELA"))
	    		{
	    			result.Add("AdtAuditoriaItem|NomeTabela");
	    			result.Add("AdtAuditoriaItem|ADT_AUDITORIA_ITEM.NOME_TABELA");
	    		}
	
	    		if (bmDisabledAdtAuditoriaItemList.Contains("ADT_AUDITORIA_ITEM.SCHEMA_TABELA"))
	    		{
	    			result.Add("AdtAuditoriaItem|SchemaTabela");
	    			result.Add("AdtAuditoriaItem|ADT_AUDITORIA_ITEM.SCHEMA_TABELA");
	    		}
	
	    		if (bmDisabledAdtAuditoriaItemList.Contains("ADT_AUDITORIA_ITEM.TIPO_OPERACAO"))
	    		{
	    			result.Add("AdtAuditoriaItem|TipoOperacao");
	    			result.Add("AdtAuditoriaItem|ADT_AUDITORIA_ITEM.TIPO_OPERACAO");
	    		}
	    	}
	    	//Add filtering disabled property for ADT_AUDITORIA_ITEM_DETALHE
	    	string[] bmDisabledAdtAuditoriaItemDetalheList = this.GetEDM().GetFilteringDisabledList("ADT_AUDITORIA_ITEM_DETALHE");
	    	if (bmDisabledAdtAuditoriaItemDetalheList.Length > 0)
	    	{
	
	    		if (bmDisabledAdtAuditoriaItemDetalheList.Contains("ADT_AUDITORIA_ITEM_DETALHE.ID_ADT_AUDITORIA_ITEM_DETALHE"))
	    		{
	    			result.Add("AdtAuditoriaItemDetalhe|IdAdtAuditoriaItemDetalhe");
	    			result.Add("AdtAuditoriaItemDetalhe|ADT_AUDITORIA_ITEM_DETALHE.ID_ADT_AUDITORIA_ITEM_DETALHE");
	    		}
	
	    		if (bmDisabledAdtAuditoriaItemDetalheList.Contains("ADT_AUDITORIA_ITEM_DETALHE.PROPRIEDADE"))
	    		{
	    			result.Add("AdtAuditoriaItemDetalhe|Propriedade");
	    			result.Add("AdtAuditoriaItemDetalhe|ADT_AUDITORIA_ITEM_DETALHE.PROPRIEDADE");
	    		}
	
	    		if (bmDisabledAdtAuditoriaItemDetalheList.Contains("ADT_AUDITORIA_ITEM_DETALHE.VALOR_ANTIGO"))
	    		{
	    			result.Add("AdtAuditoriaItemDetalhe|ValorAntigo");
	    			result.Add("AdtAuditoriaItemDetalhe|ADT_AUDITORIA_ITEM_DETALHE.VALOR_ANTIGO");
	    		}
	
	    		if (bmDisabledAdtAuditoriaItemDetalheList.Contains("ADT_AUDITORIA_ITEM_DETALHE.VALOR_NOVO"))
	    		{
	    			result.Add("AdtAuditoriaItemDetalhe|ValorNovo");
	    			result.Add("AdtAuditoriaItemDetalhe|ADT_AUDITORIA_ITEM_DETALHE.VALOR_NOVO");
	    		}
	    	}
	    	return result;
	    }

	    #endregion Excluded Filters

	    #region Get By EntitySearchId Methods Definitions.
	
				
	    [Ignore]
	    //Get AdtAuditoria By EntitySearchId.
	    public IQueryable<AdtAuditoria> GetAdtAuditoriaByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetAdtAuditoriaByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get AdtAuditoriaItem By EntitySearchId.
	    public IQueryable<AdtAuditoriaItem> GetAdtAuditoriaItemByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetAdtAuditoriaItemByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get AdtAuditoria By EntitySearchId.
	    public IQueryable<AdtAuditoria> GetAdtAuditoriaByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetAdtAuditoriaByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get AdtAuditoriaItem By EntitySearchId.
	    public IQueryable<AdtAuditoriaItem> GetAdtAuditoriaItemByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetAdtAuditoriaItemByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get AdtAuditoriaItemDetalhe By EntitySearchId.
	    public IQueryable<AdtAuditoriaItemDetalhe> GetAdtAuditoriaItemDetalheByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetAdtAuditoriaItemDetalheByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get AdtAuditoriaItemDetalhe By EntitySearchId.
	    public IQueryable<AdtAuditoriaItemDetalhe> GetAdtAuditoriaItemDetalheByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetAdtAuditoriaItemDetalheByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get AdtAuditoria By Example.
	    [Ignore]
	    public IQueryable<AdtAuditoria> GetAdtAuditoriaByExample(AdtAuditoria entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetAdtAuditoriaByEntitySearch(queryAnalysis);
	    }
			
	    //Get AdtAuditoriaItem By Example.
	    [Ignore]
	    public IQueryable<AdtAuditoriaItem> GetAdtAuditoriaItemByExample(AdtAuditoriaItem entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetAdtAuditoriaItemByEntitySearch(queryAnalysis);
	    }
			
	    //Get AdtAuditoria By Example.
	    [Ignore]
	    public IQueryable<AdtAuditoria> GetAdtAuditoriaByExampleNoAssociations(AdtAuditoria entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetAdtAuditoriaByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get AdtAuditoriaItem By Example.
	    [Ignore]
	    public IQueryable<AdtAuditoriaItem> GetAdtAuditoriaItemByExampleNoAssociations(AdtAuditoriaItem entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetAdtAuditoriaItemByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get AdtAuditoriaItemDetalhe By Example.
	    [Ignore]
	    public IQueryable<AdtAuditoriaItemDetalhe> GetAdtAuditoriaItemDetalheByExample(AdtAuditoriaItemDetalhe entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetAdtAuditoriaItemDetalheByEntitySearch(queryAnalysis);
	    }
			
	    //Get AdtAuditoriaItemDetalhe By Example.
	    [Ignore]
	    public IQueryable<AdtAuditoriaItemDetalhe> GetAdtAuditoriaItemDetalheByExampleNoAssociations(AdtAuditoriaItemDetalhe entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetAdtAuditoriaItemDetalheByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public AdtAuditoria GetAdtAuditoriaByKey(long idAdtAuditoria)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("AdtAuditoria");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdAdtAuditoria"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idAdtAuditoria));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetAdtAuditoriaByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public AdtAuditoriaItem GetAdtAuditoriaItemByKey(long idAdtAuditoriaItem)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("AdtAuditoriaItem");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdAdtAuditoriaItem"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idAdtAuditoriaItem));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetAdtAuditoriaItemByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public AdtAuditoriaItemDetalhe GetAdtAuditoriaItemDetalheByKey(long idAdtAuditoriaItemDetalhe)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("AdtAuditoriaItemDetalhe");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdAdtAuditoriaItemDetalhe"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idAdtAuditoriaItemDetalhe));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetAdtAuditoriaItemDetalheByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    [AdtAuditoriaQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get AdtAuditoriaByEntitySearch.
	    public IQueryable<AdtAuditoria> GetAdtAuditoriaByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetAdtAuditoriaByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new AdtAuditoriaQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(AdtAuditoria));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<AdtAuditoria> result = 
	            (from entity0 in this.DbContext.ADT_AUDITORIA.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_USUARIO
	            
	            	
	            select new AdtAuditoria()		
	            {
	            
                AssemblyName = entity0.ASSEMBLY_NAME
                , ConnectionString = entity0.CONNECTION_STRING
                , DataHora = entity0.DATA_HORA
                , Email = entity0Al1.EMAIL
                , IdAdtAuditoria = entity0.ID_ADT_AUDITORIA
                , IdUsuario = entity0Al1.ID_USUARIO
                , NomeUsuario = entity0Al1.NOME_USUARIO
			
                ,AdtAuditoriaItemList = 
	                        (from entity1 in entity0.ADT_AUDITORIA_ITEM_LISTA
                                  let entity1Al1 = entity1.ADT_AUDITORIA
	                        
	                        	
	                        select new AdtAuditoriaItem()
	                        {
	                        
                                IdAdtAuditoria = entity1Al1.ID_ADT_AUDITORIA
                                , IdAdtAuditoriaItem = entity1.ID_ADT_AUDITORIA_ITEM
                                , NomeTabela = entity1.NOME_TABELA
                                , SchemaTabela = entity1.SCHEMA_TABELA
                                , TipoOperacao = entity1.TIPO_OPERACAO
                                , TipoOperacaoName = ((entity1.TIPO_OPERACAO) == "I" ? "Inserção" : ((entity1.TIPO_OPERACAO) == "E" ? "Alteração" : ((entity1.TIPO_OPERACAO) == "D" ? "Exclusão" : "")))
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [AdtAuditoriaItemQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get AdtAuditoriaItemByEntitySearch.
	    public IQueryable<AdtAuditoriaItem> GetAdtAuditoriaItemByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetAdtAuditoriaItemByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new AdtAuditoriaItemQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(AdtAuditoriaItem));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<AdtAuditoriaItem> result = 
	            (from entity0 in this.DbContext.ADT_AUDITORIA_ITEM.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.ADT_AUDITORIA
	            
	            	
	            select new AdtAuditoriaItem()		
	            {
	            
                IdAdtAuditoria = entity0Al1.ID_ADT_AUDITORIA
                , IdAdtAuditoriaItem = entity0.ID_ADT_AUDITORIA_ITEM
                , NomeTabela = entity0.NOME_TABELA
                , SchemaTabela = entity0.SCHEMA_TABELA
                , TipoOperacao = entity0.TIPO_OPERACAO
                , TipoOperacaoName = ((entity0.TIPO_OPERACAO) == "I" ? "Inserção" : ((entity0.TIPO_OPERACAO) == "E" ? "Alteração" : ((entity0.TIPO_OPERACAO) == "D" ? "Exclusão" : "")))
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [AdtAuditoriaQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get AdtAuditoriaByEntitySearchNoAssociations.
	    public IQueryable<AdtAuditoria> GetAdtAuditoriaByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetAdtAuditoriaByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new AdtAuditoriaQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(AdtAuditoria));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<AdtAuditoria> result = 
	            (from entity0 in this.DbContext.ADT_AUDITORIA.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_USUARIO
	            
	            	
	            select new AdtAuditoria()		
	            {
	            
                AssemblyName = entity0.ASSEMBLY_NAME
                , ConnectionString = entity0.CONNECTION_STRING
                , DataHora = entity0.DATA_HORA
                , Email = entity0Al1.EMAIL
                , IdAdtAuditoria = entity0.ID_ADT_AUDITORIA
                , IdUsuario = entity0Al1.ID_USUARIO
                , NomeUsuario = entity0Al1.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [AdtAuditoriaItemQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get AdtAuditoriaItemByEntitySearchNoAssociations.
	    public IQueryable<AdtAuditoriaItem> GetAdtAuditoriaItemByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetAdtAuditoriaItemByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new AdtAuditoriaItemQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(AdtAuditoriaItem));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<AdtAuditoriaItem> result = 
	            (from entity0 in this.DbContext.ADT_AUDITORIA_ITEM.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.ADT_AUDITORIA
	            
	            	
	            select new AdtAuditoriaItem()		
	            {
	            
                IdAdtAuditoria = entity0Al1.ID_ADT_AUDITORIA
                , IdAdtAuditoriaItem = entity0.ID_ADT_AUDITORIA_ITEM
                , NomeTabela = entity0.NOME_TABELA
                , SchemaTabela = entity0.SCHEMA_TABELA
                , TipoOperacao = entity0.TIPO_OPERACAO
                , TipoOperacaoName = ((entity0.TIPO_OPERACAO) == "I" ? "Inserção" : ((entity0.TIPO_OPERACAO) == "E" ? "Alteração" : ((entity0.TIPO_OPERACAO) == "D" ? "Exclusão" : "")))
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [AdtAuditoriaItemQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get AdtAuditoriaItemParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<AdtAuditoriaItemParentComposition> GetAdtAuditoriaItemParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetAdtAuditoriaItemParentCompositionByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new AdtAuditoriaItemQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "ADT_AUDITORIA", "ADT_AUDITORIA_ITEM", "ADT_AUDITORIA", typeof(AdtAuditoriaItemParentComposition));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<AdtAuditoriaItemParentComposition> result = 
	            (from entity0 in this.DbContext.ADT_AUDITORIA_ITEM.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.ADT_AUDITORIA
	            
	            	
	            select new AdtAuditoriaItemParentComposition()		
	            {
	            
                IdAdtAuditoria = entity0Al1.ID_ADT_AUDITORIA
                , IdAdtAuditoriaItem = entity0.ID_ADT_AUDITORIA_ITEM
                , NomeTabela = entity0.NOME_TABELA
                , SchemaTabela = entity0.SCHEMA_TABELA
                , TipoOperacao = entity0.TIPO_OPERACAO
                , TipoOperacaoName = ((entity0.TIPO_OPERACAO) == "I" ? "Inserção" : ((entity0.TIPO_OPERACAO) == "E" ? "Alteração" : ((entity0.TIPO_OPERACAO) == "D" ? "Exclusão" : "")))
                //AdtAuditoria Properties.
                , AssemblyName = entity0.ADT_AUDITORIA.ASSEMBLY_NAME
                , ConnectionString = entity0.ADT_AUDITORIA.CONNECTION_STRING
                , DataHora = entity0.ADT_AUDITORIA.DATA_HORA
                , Email = entity0.ADT_AUDITORIA.TCS_USUARIO.EMAIL
                , IdUsuario = entity0.ADT_AUDITORIA.TCS_USUARIO.ID_USUARIO
                , NomeUsuario = entity0.ADT_AUDITORIA.TCS_USUARIO.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [AdtAuditoriaItemDetalheQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get AdtAuditoriaItemDetalheByEntitySearch.
	    public IQueryable<AdtAuditoriaItemDetalhe> GetAdtAuditoriaItemDetalheByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetAdtAuditoriaItemDetalheByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new AdtAuditoriaItemDetalheQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(AdtAuditoriaItemDetalhe));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<AdtAuditoriaItemDetalhe> result = 
	            (from entity0 in this.DbContext.ADT_AUDITORIA_ITEM_DETALHE.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.ADT_AUDITORIA_ITEM
	            
	            	
	            select new AdtAuditoriaItemDetalhe()		
	            {
	            
                IdAdtAuditoriaItem = entity0Al1.ID_ADT_AUDITORIA_ITEM
                , IdAdtAuditoriaItemDetalhe = entity0.ID_ADT_AUDITORIA_ITEM_DETALHE
                , Propriedade = entity0.PROPRIEDADE
                , ValorAntigo = entity0.VALOR_ANTIGO
                , ValorNovo = entity0.VALOR_NOVO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [AdtAuditoriaItemDetalheQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get AdtAuditoriaItemDetalheByEntitySearchNoAssociations.
	    public IQueryable<AdtAuditoriaItemDetalhe> GetAdtAuditoriaItemDetalheByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetAdtAuditoriaItemDetalheByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new AdtAuditoriaItemDetalheQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(AdtAuditoriaItemDetalhe));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<AdtAuditoriaItemDetalhe> result = 
	            (from entity0 in this.DbContext.ADT_AUDITORIA_ITEM_DETALHE.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.ADT_AUDITORIA_ITEM
	            
	            	
	            select new AdtAuditoriaItemDetalhe()		
	            {
	            
                IdAdtAuditoriaItem = entity0Al1.ID_ADT_AUDITORIA_ITEM
                , IdAdtAuditoriaItemDetalhe = entity0.ID_ADT_AUDITORIA_ITEM_DETALHE
                , Propriedade = entity0.PROPRIEDADE
                , ValorAntigo = entity0.VALOR_ANTIGO
                , ValorNovo = entity0.VALOR_NOVO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    [AdtAuditoriaQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedAdtAuditoria.
	    public IQueryable<AdtAuditoria> GetPagedAdtAuditoria(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedAdtAuditoria")))
 	        {
 	             AuthorizationResult authorizationResult = (new AdtAuditoriaQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(AdtAuditoria));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<AdtAuditoria> result = 
	            (from entity0 in this.DbContext.ADT_AUDITORIA.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_USUARIO
                orderby entity0.ID_ADT_AUDITORIA ascending
	            
	            	
	            select new AdtAuditoria()		
	            {
	            
                AssemblyName = entity0.ASSEMBLY_NAME
                , ConnectionString = entity0.CONNECTION_STRING
                , DataHora = entity0.DATA_HORA
                , Email = entity0Al1.EMAIL
                , IdAdtAuditoria = entity0.ID_ADT_AUDITORIA
                , IdUsuario = entity0Al1.ID_USUARIO
                , NomeUsuario = entity0Al1.NOME_USUARIO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    [AdtAuditoriaItemQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedAdtAuditoriaItem.
	    public IQueryable<AdtAuditoriaItem> GetPagedAdtAuditoriaItem(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedAdtAuditoriaItem")))
 	        {
 	             AuthorizationResult authorizationResult = (new AdtAuditoriaItemQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(AdtAuditoriaItem));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<AdtAuditoriaItem> result = 
	            (from entity0 in this.DbContext.ADT_AUDITORIA_ITEM.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.ADT_AUDITORIA
                orderby entity0.ID_ADT_AUDITORIA_ITEM ascending
	            
	            	
	            select new AdtAuditoriaItem()		
	            {
	            
                IdAdtAuditoria = entity0Al1.ID_ADT_AUDITORIA
                , IdAdtAuditoriaItem = entity0.ID_ADT_AUDITORIA_ITEM
                , NomeTabela = entity0.NOME_TABELA
                , SchemaTabela = entity0.SCHEMA_TABELA
                , TipoOperacao = entity0.TIPO_OPERACAO
                , TipoOperacaoName = ((entity0.TIPO_OPERACAO) == "I" ? "Inserção" : ((entity0.TIPO_OPERACAO) == "E" ? "Alteração" : ((entity0.TIPO_OPERACAO) == "D" ? "Exclusão" : "")))
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetAdtAuditoriaCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(AdtAuditoria));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.ADT_AUDITORIA.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_USUARIO
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetAdtAuditoriaItemCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(AdtAuditoriaItem));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.ADT_AUDITORIA_ITEM.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.ADT_AUDITORIA
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    [AdtAuditoriaItemDetalheQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedAdtAuditoriaItemDetalhe.
	    public IQueryable<AdtAuditoriaItemDetalhe> GetPagedAdtAuditoriaItemDetalhe(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedAdtAuditoriaItemDetalhe")))
 	        {
 	             AuthorizationResult authorizationResult = (new AdtAuditoriaItemDetalheQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(AdtAuditoriaItemDetalhe));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<AdtAuditoriaItemDetalhe> result = 
	            (from entity0 in this.DbContext.ADT_AUDITORIA_ITEM_DETALHE.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.ADT_AUDITORIA_ITEM
                orderby entity0.ID_ADT_AUDITORIA_ITEM_DETALHE ascending
	            
	            	
	            select new AdtAuditoriaItemDetalhe()		
	            {
	            
                IdAdtAuditoriaItem = entity0Al1.ID_ADT_AUDITORIA_ITEM
                , IdAdtAuditoriaItemDetalhe = entity0.ID_ADT_AUDITORIA_ITEM_DETALHE
                , Propriedade = entity0.PROPRIEDADE
                , ValorAntigo = entity0.VALOR_ANTIGO
                , ValorNovo = entity0.VALOR_NOVO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetAdtAuditoriaItemDetalheCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(AdtAuditoriaItemDetalhe));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.ADT_AUDITORIA_ITEM_DETALHE.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.ADT_AUDITORIA_ITEM
	            
	            select 1
	            ).Count();	
		
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    [AdtAuditoriaUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update AdtAuditoria.
	    public void UpdateAdtAuditoria(AdtAuditoria entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateAdtAuditoria")))
 	        {
 	             AuthorizationResult authorizationResult = (new AdtAuditoriaUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    [AdtAuditoriaInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert AdtAuditoria.
	    public void InsertAdtAuditoria(AdtAuditoria entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertAdtAuditoria")))
 	        {
 	             AuthorizationResult authorizationResult = (new AdtAuditoriaInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    [AdtAuditoriaDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete AdtAuditoria.
	    public void DeleteAdtAuditoria(AdtAuditoria entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteAdtAuditoria")))
 	        {
 	             AuthorizationResult authorizationResult = (new AdtAuditoriaDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    [AdtAuditoriaItemUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update AdtAuditoriaItem.
	    public void UpdateAdtAuditoriaItem(AdtAuditoriaItem entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateAdtAuditoriaItem")))
 	        {
 	             AuthorizationResult authorizationResult = (new AdtAuditoriaItemUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.AdtAuditoria.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.AdtAuditoria) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.AdtAuditoria); 	
	            

	
	        }
	
	    }

	    [AdtAuditoriaItemInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert AdtAuditoriaItem.
	    public void InsertAdtAuditoriaItem(AdtAuditoriaItem entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertAdtAuditoriaItem")))
 	        {
 	             AuthorizationResult authorizationResult = (new AdtAuditoriaItemInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.AdtAuditoria.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.AdtAuditoria) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.AdtAuditoria);
	            

	
	        }
	
	    }

	    [AdtAuditoriaItemDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete AdtAuditoriaItem.
	    public void DeleteAdtAuditoriaItem(AdtAuditoriaItem entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteAdtAuditoriaItem")))
 	        {
 	             AuthorizationResult authorizationResult = (new AdtAuditoriaItemDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.AdtAuditoria.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.AdtAuditoria) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.AdtAuditoria);
	            

	
	        }

	
	    }
		
			
	    [AdtAuditoriaItemDetalheUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update AdtAuditoriaItemDetalhe.
	    public void UpdateAdtAuditoriaItemDetalhe(AdtAuditoriaItemDetalhe entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateAdtAuditoriaItemDetalhe")))
 	        {
 	             AuthorizationResult authorizationResult = (new AdtAuditoriaItemDetalheUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    [AdtAuditoriaItemDetalheInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert AdtAuditoriaItemDetalhe.
	    public void InsertAdtAuditoriaItemDetalhe(AdtAuditoriaItemDetalhe entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertAdtAuditoriaItemDetalhe")))
 	        {
 	             AuthorizationResult authorizationResult = (new AdtAuditoriaItemDetalheInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    [AdtAuditoriaItemDetalheDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete AdtAuditoriaItemDetalhe.
	    public void DeleteAdtAuditoriaItemDetalhe(AdtAuditoriaItemDetalhe entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteAdtAuditoriaItemDetalhe")))
 	        {
 	             AuthorizationResult authorizationResult = (new AdtAuditoriaItemDetalheDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}