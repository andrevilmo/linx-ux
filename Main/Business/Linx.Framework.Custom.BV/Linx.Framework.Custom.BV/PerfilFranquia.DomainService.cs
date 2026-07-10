					
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

namespace Linx.Framework.Custom.BV.PerfilFranquia
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_PERFIL.ID_PERFIL", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsPerfil,TcsPerfil.TcsUsuarioPerfil,TcsPerfil.TcsPerfilRegraModulo,TcsPerfil.TcsPerfilRegraTransacao,TcsPerfil.TcsPerfilBandeiraRede,TcsPerfil.TcsPerfilFilial];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[];EdmEntityName[TCS_PERFIL];EntityRelations[TBC_GRUPO_ECONOMICO(TBC_GRUPO_ECONOMICO)#GPECON_SUPERIOR(TBC_GRUPO_ECONOMICO)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsPerfil")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.Custom.BV.PerfilFranquia.TcsPerfil")]
	public partial class TcsPerfil : Linx.Data.Entity
	{

	
		
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.TcsUsuarioPerfilList != null && this.TcsUsuarioPerfilList.Count() > 0)
	      {
	         foreach (var entity in this.TcsUsuarioPerfilList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      if (this.TcsPerfilRegraModuloList != null && this.TcsPerfilRegraModuloList.Count() > 0)
	      {
	         foreach (var entity in this.TcsPerfilRegraModuloList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      if (this.TcsPerfilRegraTransacaoList != null && this.TcsPerfilRegraTransacaoList.Count() > 0)
	      {
	         foreach (var entity in this.TcsPerfilRegraTransacaoList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      if (this.TcsPerfilBandeiraRedeList != null && this.TcsPerfilBandeiraRedeList.Count() > 0)
	      {
	         foreach (var entity in this.TcsPerfilBandeiraRedeList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      if (this.TcsPerfilFilialList != null && this.TcsPerfilFilialList.Count() > 0)
	      {
	         foreach (var entity in this.TcsPerfilFilialList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.TcsUsuarioPerfilList != null)
	      {
	         foreach (var detail in this.TcsUsuarioPerfilList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsUsuarioPerfilList = null;
	      }
	      if (this.TcsPerfilRegraModuloList != null)
	      {
	         foreach (var detail in this.TcsPerfilRegraModuloList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsPerfilRegraModuloList = null;
	      }
	      if (this.TcsPerfilRegraTransacaoList != null)
	      {
	         foreach (var detail in this.TcsPerfilRegraTransacaoList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsPerfilRegraTransacaoList = null;
	      }
	      if (this.TcsPerfilBandeiraRedeList != null)
	      {
	         foreach (var detail in this.TcsPerfilBandeiraRedeList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsPerfilBandeiraRedeList = null;
	      }
	      if (this.TcsPerfilFilialList != null)
	      {
	         foreach (var detail in this.TcsPerfilFilialList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsPerfilFilialList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(PerfilFranquiaDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("TcsUsuarioPerfil"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsUsuarioPerfil");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdPerfil"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdPerfil));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsUsuarioPerfil and all sub-details
	         if (this.TcsUsuarioPerfilList == null || this.TcsUsuarioPerfilList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsUsuarioPerfilList = context.GetPagedTcsUsuarioPerfil(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsUsuarioPerfilList = (from r in context.GetTcsUsuarioPerfilByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	      if (viewNames == null || viewNames.Contains("TcsPerfilRegraModulo"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsPerfilRegraModulo");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdPerfil"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdPerfil));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsPerfilRegraModulo and all sub-details
	         if (this.TcsPerfilRegraModuloList == null || this.TcsPerfilRegraModuloList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsPerfilRegraModuloList = context.GetPagedTcsPerfilRegraModulo(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsPerfilRegraModuloList = (from r in context.GetTcsPerfilRegraModuloByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	      if (viewNames == null || viewNames.Contains("TcsPerfilRegraTransacao"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsPerfilRegraTransacao");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdPerfil"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdPerfil));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsPerfilRegraTransacao and all sub-details
	         if (this.TcsPerfilRegraTransacaoList == null || this.TcsPerfilRegraTransacaoList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsPerfilRegraTransacaoList = context.GetPagedTcsPerfilRegraTransacao(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsPerfilRegraTransacaoList = (from r in context.GetTcsPerfilRegraTransacaoByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	      if (viewNames == null || viewNames.Contains("TcsPerfilBandeiraRede"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsPerfilBandeiraRede");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdPerfil"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdPerfil));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsPerfilBandeiraRede and all sub-details
	         if (this.TcsPerfilBandeiraRedeList == null || this.TcsPerfilBandeiraRedeList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsPerfilBandeiraRedeList = context.GetPagedTcsPerfilBandeiraRede(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsPerfilBandeiraRedeList = (from r in context.GetTcsPerfilBandeiraRedeByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	      if (viewNames == null || viewNames.Contains("TcsPerfilFilial"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsPerfilFilial");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdPerfil"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdPerfil));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsPerfilFilial and all sub-details
	         if (this.TcsPerfilFilialList == null || this.TcsPerfilFilialList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsPerfilFilialList = context.GetPagedTcsPerfilFilial(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsPerfilFilialList = (from r in context.GetTcsPerfilFilialByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _TcsUsuarioPerfilElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioPerfil && ((TcsUsuarioPerfil)e.Entity).TcsPerfil == null && e.Associations == null && e.OriginalAssociations == null && ((TcsUsuarioPerfil)e.Entity).IdPerfil == this.IdPerfil).ToList();
 	      if (_TcsUsuarioPerfilElements.Count > 0 && this.TcsUsuarioPerfilList.Count() == 0)
 	      {
 	          this.TcsUsuarioPerfilList = _TcsUsuarioPerfilElements.Select(e => (TcsUsuarioPerfil)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsUsuarioPerfilElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsUsuarioPerfil)detail.Entity).TcsPerfil = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsPerfil", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsUsuarioPerfilList", indexDetails.ToArray());
 	      }
 
 	      var _TcsPerfilRegraModuloElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsPerfilRegraModulo && ((TcsPerfilRegraModulo)e.Entity).TcsPerfil == null && e.Associations == null && e.OriginalAssociations == null && ((TcsPerfilRegraModulo)e.Entity).IdPerfil == this.IdPerfil).ToList();
 	      if (_TcsPerfilRegraModuloElements.Count > 0 && this.TcsPerfilRegraModuloList.Count() == 0)
 	      {
 	          this.TcsPerfilRegraModuloList = _TcsPerfilRegraModuloElements.Select(e => (TcsPerfilRegraModulo)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsPerfilRegraModuloElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsPerfilRegraModulo)detail.Entity).TcsPerfil = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsPerfil", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsPerfilRegraModuloList", indexDetails.ToArray());
 	      }
 
 	      var _TcsPerfilRegraTransacaoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsPerfilRegraTransacao && ((TcsPerfilRegraTransacao)e.Entity).TcsPerfil == null && e.Associations == null && e.OriginalAssociations == null && ((TcsPerfilRegraTransacao)e.Entity).IdPerfil == this.IdPerfil).ToList();
 	      if (_TcsPerfilRegraTransacaoElements.Count > 0 && this.TcsPerfilRegraTransacaoList.Count() == 0)
 	      {
 	          this.TcsPerfilRegraTransacaoList = _TcsPerfilRegraTransacaoElements.Select(e => (TcsPerfilRegraTransacao)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsPerfilRegraTransacaoElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsPerfilRegraTransacao)detail.Entity).TcsPerfil = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsPerfil", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsPerfilRegraTransacaoList", indexDetails.ToArray());
 	      }
 
 	      var _TcsPerfilBandeiraRedeElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsPerfilBandeiraRede && ((TcsPerfilBandeiraRede)e.Entity).TcsPerfil == null && e.Associations == null && e.OriginalAssociations == null && ((TcsPerfilBandeiraRede)e.Entity).IdPerfil == this.IdPerfil).ToList();
 	      if (_TcsPerfilBandeiraRedeElements.Count > 0 && this.TcsPerfilBandeiraRedeList.Count() == 0)
 	      {
 	          this.TcsPerfilBandeiraRedeList = _TcsPerfilBandeiraRedeElements.Select(e => (TcsPerfilBandeiraRede)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsPerfilBandeiraRedeElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsPerfilBandeiraRede)detail.Entity).TcsPerfil = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsPerfil", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsPerfilBandeiraRedeList", indexDetails.ToArray());
 	      }
 
 	      var _TcsPerfilFilialElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsPerfilFilial && ((TcsPerfilFilial)e.Entity).TcsPerfil == null && e.Associations == null && e.OriginalAssociations == null && ((TcsPerfilFilial)e.Entity).IdPerfil == this.IdPerfil).ToList();
 	      if (_TcsPerfilFilialElements.Count > 0 && this.TcsPerfilFilialList.Count() == 0)
 	      {
 	          this.TcsPerfilFilialList = _TcsPerfilFilialElements.Select(e => (TcsPerfilFilial)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsPerfilFilialElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsPerfilFilial)detail.Entity).TcsPerfil = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsPerfil", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsPerfilFilialList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For DescPerfil
	    partial void OnDescPerfilChanging(string value);
	    partial void OnDescPerfilChanged();

	    private string _DescPerfil;

	    [DataMember(IsRequired = true, Name = "DescPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PERFIL.DESC_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL.DESC_PERFIL")]
	    public string DescPerfil
	    {
	    	    get
	    	    {
	    	          return _DescPerfil;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescPerfil != value)
	    	          {
	    	              this.ValidateProperty("DescPerfil", value);
	    	              this.OnDescPerfilChanging(value);
	    	              this.RaiseDataMemberChanging("DescPerfil");
	    	              this._DescPerfil = value;
	    	              this.RaiseDataMemberChanged("DescPerfil");
	    	              this.OnDescPerfilChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdGpeconFranquia
	    partial void OnIdGpeconFranquiaChanging(System.Nullable<int> value);
	    partial void OnIdGpeconFranquiaChanged();

	    private System.Nullable<int> _IdGpeconFranquia;

	    [DataMember(Name = "IdGpeconFranquia", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Gpecon", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PERFIL.TBC_GRUPO_ECONOMICO.ID_GPECON];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL.TBC_GRUPO_ECONOMICO.ID_GPECON")]
	    public System.Nullable<int> IdGpeconFranquia
	    {
	    	    get
	    	    {
	    	          return _IdGpeconFranquia;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdGpeconFranquia != value)
	    	          {
	    	              this.ValidateProperty("IdGpeconFranquia", value);
	    	              this.OnIdGpeconFranquiaChanging(value);
	    	              this.RaiseDataMemberChanging("IdGpeconFranquia");
	    	              this._IdGpeconFranquia = value;
	    	              this.RaiseDataMemberChanged("IdGpeconFranquia");
	    	              this.OnIdGpeconFranquiaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdPerfil
	    partial void OnIdPerfilChanging(long value);
	    partial void OnIdPerfilChanged();

	    private long _IdPerfil;

	    [DataMember(IsRequired = true, Name = "IdPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PERFIL.ID_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL.ID_PERFIL")]
	    public long IdPerfil
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
	    //Extensibility Partial Method Definitions For IdPerfilOrigem
	    partial void OnIdPerfilOrigemChanging(System.Nullable<long> value);
	    partial void OnIdPerfilOrigemChanged();

	    private System.Nullable<long> _IdPerfilOrigem;

	    [DataMember(Name = "IdPerfilOrigem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Perfil Origem", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PERFIL.ID_PERFIL_ORIGEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL.ID_PERFIL_ORIGEM")]
	    public System.Nullable<long> IdPerfilOrigem
	    {
	    	    get
	    	    {
	    	          return _IdPerfilOrigem;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdPerfilOrigem != value)
	    	          {
	    	              this.ValidateProperty("IdPerfilOrigem", value);
	    	              this.OnIdPerfilOrigemChanging(value);
	    	              this.RaiseDataMemberChanging("IdPerfilOrigem");
	    	              this._IdPerfilOrigem = value;
	    	              this.RaiseDataMemberChanged("IdPerfilOrigem");
	    	              this.OnIdPerfilOrigemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Inativo
	    partial void OnInativoChanging(bool value);
	    partial void OnInativoChanged();

	    private bool _Inativo;

	    [DataMember(IsRequired = true, Name = "Inativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inativo", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PERFIL.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL.INATIVO")]
	    public bool Inativo
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

	    #endregion Data Properties

	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<TcsPerfilBandeiraRede> _TcsPerfilBandeiraRedeList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsPerfil_TcsPerfilBandeiraRede", "IdPerfil", "IdPerfil", IsForeignKey=false)]
	    [DataMember(Name = "TcsPerfilBandeiraRedeList", EmitDefaultValue = true)]
	    public IEnumerable<TcsPerfilBandeiraRede> TcsPerfilBandeiraRedeList
	    {
	        get
	        {
	
	            if (this._TcsPerfilBandeiraRedeList == null)
	            	this._TcsPerfilBandeiraRedeList = new List<TcsPerfilBandeiraRede>();
	
	            return this._TcsPerfilBandeiraRedeList;
	        }
	        set
	        {
	            if (this._TcsPerfilBandeiraRedeList != value)
	            {
	                this._TcsPerfilBandeiraRedeList = value;
	                this.RaisePropertyChanged("TcsPerfilBandeiraRedeList");
	            }
	        }
	    }	 
		
	    private IEnumerable<TcsPerfilFilial> _TcsPerfilFilialList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsPerfil_TcsPerfilFilial", "IdPerfil", "IdPerfil", IsForeignKey=false)]
	    [DataMember(Name = "TcsPerfilFilialList", EmitDefaultValue = true)]
	    public IEnumerable<TcsPerfilFilial> TcsPerfilFilialList
	    {
	        get
	        {
	
	            if (this._TcsPerfilFilialList == null)
	            	this._TcsPerfilFilialList = new List<TcsPerfilFilial>();
	
	            return this._TcsPerfilFilialList;
	        }
	        set
	        {
	            if (this._TcsPerfilFilialList != value)
	            {
	                this._TcsPerfilFilialList = value;
	                this.RaisePropertyChanged("TcsPerfilFilialList");
	            }
	        }
	    }	 
		
	    private IEnumerable<TcsPerfilRegraModulo> _TcsPerfilRegraModuloList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsPerfil_TcsPerfilRegraModulo", "IdPerfil", "IdPerfil", IsForeignKey=false)]
	    [DataMember(Name = "TcsPerfilRegraModuloList", EmitDefaultValue = true)]
	    public IEnumerable<TcsPerfilRegraModulo> TcsPerfilRegraModuloList
	    {
	        get
	        {
	
	            if (this._TcsPerfilRegraModuloList == null)
	            	this._TcsPerfilRegraModuloList = new List<TcsPerfilRegraModulo>();
	
	            return this._TcsPerfilRegraModuloList;
	        }
	        set
	        {
	            if (this._TcsPerfilRegraModuloList != value)
	            {
	                this._TcsPerfilRegraModuloList = value;
	                this.RaisePropertyChanged("TcsPerfilRegraModuloList");
	            }
	        }
	    }	 
		
	    private IEnumerable<TcsPerfilRegraTransacao> _TcsPerfilRegraTransacaoList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsPerfil_TcsPerfilRegraTransacao", "IdPerfil", "IdPerfil", IsForeignKey=false)]
	    [DataMember(Name = "TcsPerfilRegraTransacaoList", EmitDefaultValue = true)]
	    public IEnumerable<TcsPerfilRegraTransacao> TcsPerfilRegraTransacaoList
	    {
	        get
	        {
	
	            if (this._TcsPerfilRegraTransacaoList == null)
	            	this._TcsPerfilRegraTransacaoList = new List<TcsPerfilRegraTransacao>();
	
	            return this._TcsPerfilRegraTransacaoList;
	        }
	        set
	        {
	            if (this._TcsPerfilRegraTransacaoList != value)
	            {
	                this._TcsPerfilRegraTransacaoList = value;
	                this.RaisePropertyChanged("TcsPerfilRegraTransacaoList");
	            }
	        }
	    }	 
		
	    private IEnumerable<TcsUsuarioPerfil> _TcsUsuarioPerfilList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsPerfil_TcsUsuarioPerfil", "IdPerfil", "IdPerfil", IsForeignKey=false)]
	    [DataMember(Name = "TcsUsuarioPerfilList", EmitDefaultValue = true)]
	    public IEnumerable<TcsUsuarioPerfil> TcsUsuarioPerfilList
	    {
	        get
	        {
	
	            if (this._TcsUsuarioPerfilList == null)
	            	this._TcsUsuarioPerfilList = new List<TcsUsuarioPerfil>();
	
	            return this._TcsUsuarioPerfilList;
	        }
	        set
	        {
	            if (this._TcsUsuarioPerfilList != value)
	            {
	                this._TcsUsuarioPerfilList = value;
	                this.RaisePropertyChanged("TcsUsuarioPerfilList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_PERFIL").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = true, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_PERFIL), QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PERFIL.INATIVO", Source = "Inativo", Target = "INATIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL", RelationPropertyName = "TCS_PERFIL" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PERFIL.ID_PERFIL", Source = "IdPerfil", Target = "ID_PERFIL", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL", RelationPropertyName = "TCS_PERFIL" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PERFIL.DESC_PERFIL", Source = "DescPerfil", Target = "DESC_PERFIL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL", RelationPropertyName = "TCS_PERFIL" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PERFIL.ID_PERFIL_ORIGEM", Source = "IdPerfilOrigem", Target = "ID_PERFIL_ORIGEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL", RelationPropertyName = "TCS_PERFIL" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PERFIL.TBC_GRUPO_ECONOMICO.ID_GPECON", Source = "IdGpeconFranquia", Target = "ID_GPECON", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TBC_GRUPO_ECONOMICO", RelationPropertyName = "TBC_GRUPO_ECONOMICO" });

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

		

	[LinxPublicationView(PrimaryKeys="TCS_USUARIO_PERFIL.ID_TCS_USUARIO_PERFIL", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Usuários Vinculados];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#.TCS_USUARIO_PERFIL_LISTA as #Alias#];EdmEntityName[TCS_USUARIO_PERFIL];EntityRelations[TCS_PERFIL(TCS_PERFIL)#TBC_GRUPO_ECONOMICO(TBC_GRUPO_ECONOMICO)#GPECON_SUPERIOR(TBC_GRUPO_ECONOMICO)#TCS_USUARIO(TCS_USUARIO)];EdmParentEntityName[TCS_PERFIL];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioPerfil")]
	[Serializable()]
	public partial class TcsUsuarioPerfil : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(PerfilFranquiaDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsPerfil");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdPerfil"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdPerfil));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsPerfil
	         this.TcsPerfil = (from r in context.GetTcsPerfilByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For IdLinx
	    partial void OnIdLinxChanging(int value);
	    partial void OnIdLinxChanged();

	    private int _IdLinx;

	    [DataMember(IsRequired = true, Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuario];LookUpTitle[Seleção de (Id Linx)];LookUpQuery[executeLookUpTcsUsuario];LookUpFinalize[finalizeLookUpTcsUsuario];LookUpDisplayColumns[{\"IdUsuario\" : \"Id Usuario\", \"NomeUsuario\" : \"Nome Usuario\", \"IdLinx\" : \"Id Linx\", \"UidUsuario\" : \"Uid Usuario\"}];LookUpColumns[{\"IdUsuario\" : false, \"NomeUsuario\" : true, \"IdLinx\" : false, \"UidUsuario\" : false}];FilterDataKey[TCS_USUARIO_PERFIL.TCS_USUARIO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="int#IdLinx#false##0:0##Id Linx#2#false##::LookUpTcsUsuario##true#false#TCS_USUARIO#TCS_USUARIO#Linx.Framework.Custom.BV.PerfilFranquia#IQueryable###true#true", EdmKey="TCS_USUARIO_PERFIL.TCS_USUARIO.ID_LINX")]
	    public int IdLinx
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
	    //Extensibility Partial Method Definitions For IdPerfil
	    partial void OnIdPerfilChanging(long value);
	    partial void OnIdPerfilChanged();

	    private long _IdPerfil;

	    [DataMember(IsRequired = true, Name = "IdPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_PERFIL.TCS_PERFIL.ID_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_PERFIL.TCS_PERFIL.ID_PERFIL")]
	    public long IdPerfil
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
	    //Extensibility Partial Method Definitions For IdTcsUsuarioPerfil
	    partial void OnIdTcsUsuarioPerfilChanging(long value);
	    partial void OnIdTcsUsuarioPerfilChanged();

	    private long _IdTcsUsuarioPerfil;

	    [DataMember(IsRequired = true, Name = "IdTcsUsuarioPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Perfil", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_PERFIL.ID_TCS_USUARIO_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_PERFIL.ID_TCS_USUARIO_PERFIL")]
	    public long IdTcsUsuarioPerfil
	    {
	    	    get
	    	    {
	    	          return _IdTcsUsuarioPerfil;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsUsuarioPerfil != value)
	    	          {
	    	              this.ValidateProperty("IdTcsUsuarioPerfil", value);
	    	              this.OnIdTcsUsuarioPerfilChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsUsuarioPerfil");
	    	              this._IdTcsUsuarioPerfil = value;
	    	              this.RaiseDataMemberChanged("IdTcsUsuarioPerfil");
	    	              this.OnIdTcsUsuarioPerfilChanged();
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
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuario];LookUpTitle[Seleção de (Id Usuario)];LookUpQuery[executeLookUpTcsUsuario];LookUpFinalize[finalizeLookUpTcsUsuario];LookUpDisplayColumns[{\"IdUsuario\" : \"Id Usuario\", \"NomeUsuario\" : \"Nome Usuario\", \"IdLinx\" : \"Id Linx\", \"UidUsuario\" : \"Uid Usuario\"}];LookUpColumns[{\"IdUsuario\" : false, \"NomeUsuario\" : true, \"IdLinx\" : false, \"UidUsuario\" : false}];FilterDataKey[TCS_USUARIO_PERFIL.TCS_USUARIO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="long#IdUsuario#true##0:0##Id Usuario#0#false##::LookUpTcsUsuario##true#false#TCS_USUARIO#TCS_USUARIO#Linx.Framework.Custom.BV.PerfilFranquia#IQueryable###true#true", EdmKey="TCS_USUARIO_PERFIL.TCS_USUARIO.ID_USUARIO")]
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
	    [Display(Name = "Usuário", Description="", Order = 17, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuario];LookUpTitle[Seleção de (Usuário)];LookUpQuery[executeLookUpTcsUsuario];LookUpFinalize[finalizeLookUpTcsUsuario];LookUpDisplayColumns[{\"IdUsuario\" : \"Id Usuario\", \"NomeUsuario\" : \"Nome Usuario\", \"IdLinx\" : \"Id Linx\", \"UidUsuario\" : \"Uid Usuario\"}];LookUpColumns[{\"IdUsuario\" : false, \"NomeUsuario\" : true, \"IdLinx\" : false, \"UidUsuario\" : false}];FilterDataKey[TCS_USUARIO_PERFIL.TCS_USUARIO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#NomeUsuario#false##250:0##Nome Usuario#1#true##::LookUpTcsUsuario##true#false#TCS_USUARIO#TCS_USUARIO#Linx.Framework.Custom.BV.PerfilFranquia#IQueryable###true#true", EdmKey="TCS_USUARIO_PERFIL.TCS_USUARIO.NOME_USUARIO")]
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
	    //Extensibility Partial Method Definitions For UidUsuario
	    partial void OnUidUsuarioChanging(Guid value);
	    partial void OnUidUsuarioChanged();

	    private Guid _UidUsuario;

	    [DataMember(IsRequired = true, Name = "UidUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Usuario", Description="", Order = 22, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuario];LookUpTitle[Seleção de (Uid Usuario)];LookUpQuery[executeLookUpTcsUsuario];LookUpFinalize[finalizeLookUpTcsUsuario];LookUpDisplayColumns[{\"IdUsuario\" : \"Id Usuario\", \"NomeUsuario\" : \"Nome Usuario\", \"IdLinx\" : \"Id Linx\", \"UidUsuario\" : \"Uid Usuario\"}];LookUpColumns[{\"IdUsuario\" : false, \"NomeUsuario\" : true, \"IdLinx\" : false, \"UidUsuario\" : false}];FilterDataKey[TCS_USUARIO_PERFIL.TCS_USUARIO.UID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Guid#UidUsuario#false##36:0##Uid Usuario#3#false##::LookUpTcsUsuario##true#false#TCS_USUARIO#TCS_USUARIO#Linx.Framework.Custom.BV.PerfilFranquia#IQueryable###true#true", EdmKey="TCS_USUARIO_PERFIL.TCS_USUARIO.UID_USUARIO")]
	    public Guid UidUsuario
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

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsPerfil _TcsPerfil;
	    [DataMember(Name = "TcsPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsPerfil_TcsUsuarioPerfil", "IdPerfil", "IdPerfil", IsForeignKey=true)]
	    public TcsPerfil TcsPerfil
	    {
	        get
	        {
	            return this._TcsPerfil;
	        }
	        set
	        {
	            if (this._TcsPerfil != value)
	            {
	                this._TcsPerfil = value;
	                this.RaisePropertyChanged("TcsPerfilList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_USUARIO_PERFIL").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_USUARIO_PERFIL), QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_PERFIL" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_PERFIL.TCS_PERFIL.ID_PERFIL", Source = "IdPerfil", Target = "ID_PERFIL", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL", RelationPropertyName = "TCS_PERFIL" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_PERFIL.ID_TCS_USUARIO_PERFIL", Source = "IdTcsUsuarioPerfil", Target = "ID_TCS_USUARIO_PERFIL", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_PERFIL", RelationPropertyName = "TCS_USUARIO_PERFIL" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_PERFIL.TCS_USUARIO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });

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

		

	[LinxPublicationView(PrimaryKeys="TCS_PERFIL_REGRA_MODULO.ID_PERFIL_REGRA_MODULO", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Módulo];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#.TCS_PERFIL_REGRA_MODULO_LISTA as #Alias#];EdmEntityName[TCS_PERFIL_REGRA_MODULO];EntityRelations[TCS_PERFIL(TCS_PERFIL)#TBC_GRUPO_ECONOMICO(TBC_GRUPO_ECONOMICO)#GPECON_SUPERIOR(TBC_GRUPO_ECONOMICO)];EdmParentEntityName[TCS_PERFIL];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsPerfilRegraModulo")]
	[Serializable()]
	public partial class TcsPerfilRegraModulo : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(PerfilFranquiaDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsPerfil");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdPerfil"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdPerfil));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsPerfil
	         this.TcsPerfil = (from r in context.GetTcsPerfilByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For IdModulo
	    partial void OnIdModuloChanging(long value);
	    partial void OnIdModuloChanged();

	    private long _IdModulo;

	    [DataMember(IsRequired = true, Name = "IdModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsPerfilRegraModulo];LookUpTitle[Seleção de (Id Modulo)];LookUpQuery[executeLookUpTcsPerfilRegraModulo];LookUpFinalize[finalizeLookUpTcsPerfilRegraModulo];LookUpDisplayColumns[{\"IdModulo\" : \"\", \"DescModulo\" : \"Módulo\", \"DescAplicativo\" : \"Aplicativo\", \"Origem\" : \"\"}];LookUpColumns[{\"IdModulo\" : false, \"DescModulo\" : true, \"DescAplicativo\" : true, \"Origem\" : true}];FilterDataKey[TCS_PERFIL_REGRA_MODULO.ID_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdModulo#true##12###0#false##::LookUpTcsPerfilRegraModulo##true#false###Linx.Framework.Custom.BV.PerfilFranquia#IQueryable###true#false", EdmKey="TCS_PERFIL_REGRA_MODULO.ID_MODULO")]
	    public long IdModulo
	    {
	    	    get
	    	    {
	    	          return _IdModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdModulo != value)
	    	          {
	    	              this.ValidateProperty("IdModulo", value);
	    	              this.OnIdModuloChanging(value);
	    	              this.RaiseDataMemberChanging("IdModulo");
	    	              this._IdModulo = value;
	    	              this.RaiseDataMemberChanged("IdModulo");
	    	              this.OnIdModuloChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdPerfil
	    partial void OnIdPerfilChanging(long value);
	    partial void OnIdPerfilChanged();

	    private long _IdPerfil;

	    [DataMember(IsRequired = true, Name = "IdPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PERFIL_REGRA_MODULO.TCS_PERFIL.ID_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL_REGRA_MODULO.TCS_PERFIL.ID_PERFIL")]
	    public long IdPerfil
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
	    //Extensibility Partial Method Definitions For IdPerfilRegraModulo
	    partial void OnIdPerfilRegraModuloChanging(long value);
	    partial void OnIdPerfilRegraModuloChanged();

	    private long _IdPerfilRegraModulo;

	    [DataMember(IsRequired = true, Name = "IdPerfilRegraModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Perfil Regra Modulo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PERFIL_REGRA_MODULO.ID_PERFIL_REGRA_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL_REGRA_MODULO.ID_PERFIL_REGRA_MODULO")]
	    public long IdPerfilRegraModulo
	    {
	    	    get
	    	    {
	    	          return _IdPerfilRegraModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdPerfilRegraModulo != value)
	    	          {
	    	              this.ValidateProperty("IdPerfilRegraModulo", value);
	    	              this.OnIdPerfilRegraModuloChanging(value);
	    	              this.RaiseDataMemberChanging("IdPerfilRegraModulo");
	    	              this._IdPerfilRegraModulo = value;
	    	              this.RaiseDataMemberChanged("IdPerfilRegraModulo");
	    	              this.OnIdPerfilRegraModuloChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxRegraAcessoModulo
	    partial void OnLxRegraAcessoModuloChanging(byte value);
	    partial void OnLxRegraAcessoModuloChanged();

	    private byte _LxRegraAcessoModulo;

	    [DataMember(IsRequired = true, Name = "LxRegraAcessoModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Regra Módulo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[RegraAcesso];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpLxRegraAcessoModulo];LookUpTitle[Seleção de (Regra Módulo)];LookUpQuery[executeLookUpLxRegraAcessoModulo];LookUpFinalize[finalizeLookUpLxRegraAcessoModulo];LookUpDisplayColumns[{\"LxRegraAcessoModulo\" : \"Regra Módulo\", \"LxRegraAcessoModuloName\" : \"Regra Acesso\"}];LookUpColumns[{\"LxRegraAcessoModulo\" : false, \"LxRegraAcessoModuloName\" : true}];FilterDataKey[TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="byte#LxRegraAcessoModulo#true##0##Regra Módulo#0#false##::LookUpLxRegraAcessoModulo##true#false###Linx.Framework.Custom.BV.PerfilFranquia#IQueryable###true#false", EdmKey="TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO")]
	    public byte LxRegraAcessoModulo
	    {
	    	    get
	    	    {
	    	          return _LxRegraAcessoModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxRegraAcessoModulo != value)
	    	          {
	    	              this.ValidateProperty("LxRegraAcessoModulo", value);
	    	              this.OnLxRegraAcessoModuloChanging(value);
	    	              this.RaiseDataMemberChanging("LxRegraAcessoModulo");
	    	              this._LxRegraAcessoModulo = value;
	    	              this.RaiseDataMemberChanged("LxRegraAcessoModulo");
	    	              this.OnLxRegraAcessoModuloChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Acesso
	    partial void OnAcessoChanging(string value);
	    partial void OnAcessoChanged();

	    private string _Acesso;

	    [DataMember(IsRequired = true, Name = "Acesso", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Regra Acesso", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];LookUpName[LookUpLxRegraAcessoModulo];LookUpTitle[Seleção de (Regra Acesso)];LookUpQuery[executeLookUpLxRegraAcessoModulo];LookUpFinalize[finalizeLookUpLxRegraAcessoModulo];LookUpDisplayColumns[{\"LxRegraAcessoModulo\" : \"Regra Módulo\", \"LxRegraAcessoModuloName\" : \"Regra Acesso\"}];LookUpColumns[{\"LxRegraAcessoModulo\" : false, \"LxRegraAcessoModuloName\" : true}];FilterDataKey[((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 1 ? 'Acesso Bloqueado' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 2 ? 'Acesso Total' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 13 ? 'Acesso por Transação' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 5 ? 'Alterar' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 12 ? 'Criar Pesquisa' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 10 ? 'Criar Relatório' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 6 ? 'Excluir' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 9 ? 'Exportar' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 8 ? 'Imprimir' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 4 ? 'Incluir' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 11 ? 'Layout' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 7 ? 'Pesquisa Especial' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 3 ? 'Pesquisar' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 99 ? 'Regra Transação' : ''))))))))))))))];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#LxRegraAcessoModuloName#false##250:0##Regra Acesso#1#true##::LookUpLxRegraAcessoModulo##true#false###Linx.Framework.Custom.BV.PerfilFranquia#IQueryable###true#false", EdmKey="((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) == 1 ? \"Acesso Bloqueado\" : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) == 2 ? \"Acesso Total\" : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) == 13 ? \"Acesso por Transação\" : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) == 5 ? \"Alterar\" : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) == 12 ? \"Criar Pesquisa\" : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) == 10 ? \"Criar Relatório\" : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) == 6 ? \"Excluir\" : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) == 9 ? \"Exportar\" : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) == 8 ? \"Imprimir\" : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) == 4 ? \"Incluir\" : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) == 11 ? \"Layout\" : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) == 7 ? \"Pesquisa Especial\" : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) == 3 ? \"Pesquisar\" : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) == 99 ? \"Regra Transação\" : \"\"))))))))))))))")]
	    public string Acesso
	    {
	    	    get
	    	    {
	    	          return _Acesso;
	    	    }
	    	    set
	    	    {
	    	          if (this._Acesso != value)
	    	          {
	    	              this.ValidateProperty("Acesso", value);
	    	              this.OnAcessoChanging(value);
	    	              this.RaiseDataMemberChanging("Acesso");
	    	              this._Acesso = value;
	    	              this.RaiseDataMemberChanged("Acesso");
	    	              this.OnAcessoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescModulo
	    partial void OnDescModuloChanging(string value);
	    partial void OnDescModuloChanged();

	    private string _DescModulo;

	    [DataMember(IsRequired = true, Name = "DescModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Módulo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#DescModulo#false##60:0##Módulo#1#true##::LookUpTcsPerfilRegraModulo##true#false###Linx.Framework.Custom.BV.PerfilFranquia#IQueryable###true#false", EdmKey="")]
	    public string DescModulo
	    {
	    	    get
	    	    {
	    	          if (_DescModulo != (GetDescModulo()))
	    	             _DescModulo =  GetDescModulo();
	    	          return _DescModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescModulo != value)
	    	          {
	    	              this.ValidateProperty("DescModulo", value);
	    	              this.OnDescModuloChanging(value);
	    	              this.RaiseDataMemberChanging("DescModulo");
	    	              this._DescModulo = value;
	    	              this.RaiseDataMemberChanged("DescModulo");
	    	              this.OnDescModuloChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescAplicativo
	    partial void OnDescAplicativoChanging(string value);
	    partial void OnDescAplicativoChanged();

	    private string _DescAplicativo;

	    [DataMember(IsRequired = true, Name = "DescAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicativo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#DescAplicativo#false##250:0##Aplicativo#2#true##::LookUpTcsPerfilRegraModulo##true#false###Linx.Framework.Custom.BV.PerfilFranquia#IQueryable###true#false", EdmKey="")]
	    public string DescAplicativo
	    {
	    	    get
	    	    {
	    	          if (_DescAplicativo != (GetDescAplicativo()))
	    	             _DescAplicativo =  GetDescAplicativo();
	    	          return _DescAplicativo;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescAplicativo != value)
	    	          {
	    	              this.ValidateProperty("DescAplicativo", value);
	    	              this.OnDescAplicativoChanging(value);
	    	              this.RaiseDataMemberChanging("DescAplicativo");
	    	              this._DescAplicativo = value;
	    	              this.RaiseDataMemberChanged("DescAplicativo");
	    	              this.OnDescAplicativoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Origem
	    partial void OnOrigemChanging(string value);
	    partial void OnOrigemChanged();

	    private string _Origem;

	    [DataMember(IsRequired = true, Name = "Origem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Origem", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#Origem#false##0###3#true##::LookUpTcsPerfilRegraModulo##true#false###Linx.Framework.Custom.BV.PerfilFranquia#IQueryable###true#false", EdmKey="")]
	    public string Origem
	    {
	    	    get
	    	    {
	    	          if (_Origem != (GetOrigem()))
	    	             _Origem =  GetOrigem();
	    	          return _Origem;
	    	    }
	    	    set
	    	    {
	    	          if (this._Origem != value)
	    	          {
	    	              this.ValidateProperty("Origem", value);
	    	              this.OnOrigemChanging(value);
	    	              this.RaiseDataMemberChanging("Origem");
	    	              this._Origem = value;
	    	              this.RaiseDataMemberChanged("Origem");
	    	              this.OnOrigemChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsPerfil _TcsPerfil;
	    [DataMember(Name = "TcsPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsPerfil_TcsPerfilRegraModulo", "IdPerfil", "IdPerfil", IsForeignKey=true)]
	    public TcsPerfil TcsPerfil
	    {
	        get
	        {
	            return this._TcsPerfil;
	        }
	        set
	        {
	            if (this._TcsPerfil != value)
	            {
	                this._TcsPerfil = value;
	                this.RaisePropertyChanged("TcsPerfilList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_PERFIL_REGRA_MODULO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_PERFIL_REGRA_MODULO), QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL_REGRA_MODULO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PERFIL_REGRA_MODULO.ID_MODULO", Source = "IdModulo", Target = "ID_MODULO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL_REGRA_MODULO", RelationPropertyName = "TCS_PERFIL_REGRA_MODULO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PERFIL_REGRA_MODULO.TCS_PERFIL.ID_PERFIL", Source = "IdPerfil", Target = "ID_PERFIL", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL", RelationPropertyName = "TCS_PERFIL" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PERFIL_REGRA_MODULO.ID_PERFIL_REGRA_MODULO", Source = "IdPerfilRegraModulo", Target = "ID_PERFIL_REGRA_MODULO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL_REGRA_MODULO", RelationPropertyName = "TCS_PERFIL_REGRA_MODULO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO", Source = "LxRegraAcessoModulo", Target = "LX_REGRA_ACESSO_MODULO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL_REGRA_MODULO", RelationPropertyName = "TCS_PERFIL_REGRA_MODULO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxRegraAcessoModuloValues()
	    {
	    	    return Linx.Framework.Custom.BV.Domains.RegraAcesso.GetValues();
	    }
	    private string _lxRegraAcessoModuloName;
	    [DataMember(IsRequired = false, Name = "LxRegraAcessoModuloName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Regra Módulo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxRegraAcessoModuloName
	    {
	    	    get { if (this.LxRegraAcessoModulo.IsNull()) { _lxRegraAcessoModuloName = String.Empty; } else { string key = this.LxRegraAcessoModulo.ToString(); var dmValues = this.GetLxRegraAcessoModuloValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxRegraAcessoModuloName) _lxRegraAcessoModuloName = domainName; } return _lxRegraAcessoModuloName; } set { _lxRegraAcessoModuloName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_PERFIL_REGRA_TRANSACAO.ID_PERFIL_REGRA_TRANSACAO", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Transação];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#.TCS_PERFIL_REGRA_TRANSACAO_LISTA as #Alias#];EdmEntityName[TCS_PERFIL_REGRA_TRANSACAO];EntityRelations[TCS_PERFIL(TCS_PERFIL)#TBC_GRUPO_ECONOMICO(TBC_GRUPO_ECONOMICO)#GPECON_SUPERIOR(TBC_GRUPO_ECONOMICO)];EdmParentEntityName[TCS_PERFIL];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsPerfilRegraTransacao")]
	[Serializable()]
	public partial class TcsPerfilRegraTransacao : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(PerfilFranquiaDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsPerfil");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdPerfil"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdPerfil));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsPerfil
	         this.TcsPerfil = (from r in context.GetTcsPerfilByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For Acesso
	    partial void OnAcessoChanging(string value);
	    partial void OnAcessoChanged();

	    private string _Acesso;

	    [DataMember(IsRequired = true, Name = "Acesso", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Regra Acesso", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];LookUpName[LookupLxRegraAcessoTransacao];LookUpTitle[Seleção de (Regra Acesso)];LookUpQuery[executeLookupLxRegraAcessoTransacao];LookUpFinalize[finalizeLookupLxRegraAcessoTransacao];LookUpDisplayColumns[{\"LxRegraAcessoTransacao\" : \"\", \"LxRegraAcessoTransacaoName\" : \"Regra Acesso\"}];LookUpColumns[{\"LxRegraAcessoTransacao\" : false, \"LxRegraAcessoTransacaoName\" : true}];FilterDataKey[((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 1 ? 'Acesso Bloqueado' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 2 ? 'Acesso Total' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 13 ? 'Acesso por Transação' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 5 ? 'Alterar' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 12 ? 'Criar Pesquisa' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 10 ? 'Criar Relatório' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 6 ? 'Excluir' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 9 ? 'Exportar' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 8 ? 'Imprimir' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 4 ? 'Incluir' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 11 ? 'Layout' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 7 ? 'Pesquisa Especial' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 3 ? 'Pesquisar' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 99 ? 'Regra Transação' : ''))))))))))))))];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#LxRegraAcessoTransacaoName#false##60:0##Regra Acesso#1#true##::LookupLxRegraAcessoTransacao##true#false###Linx.Framework.Custom.BV.PerfilFranquia#IQueryable###true#false", EdmKey="((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) == 1 ? \"Acesso Bloqueado\" : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) == 2 ? \"Acesso Total\" : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) == 13 ? \"Acesso por Transação\" : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) == 5 ? \"Alterar\" : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) == 12 ? \"Criar Pesquisa\" : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) == 10 ? \"Criar Relatório\" : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) == 6 ? \"Excluir\" : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) == 9 ? \"Exportar\" : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) == 8 ? \"Imprimir\" : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) == 4 ? \"Incluir\" : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) == 11 ? \"Layout\" : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) == 7 ? \"Pesquisa Especial\" : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) == 3 ? \"Pesquisar\" : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) == 99 ? \"Regra Transação\" : \"\"))))))))))))))")]
	    public string Acesso
	    {
	    	    get
	    	    {
	    	          return _Acesso;
	    	    }
	    	    set
	    	    {
	    	          if (this._Acesso != value)
	    	          {
	    	              this.ValidateProperty("Acesso", value);
	    	              this.OnAcessoChanging(value);
	    	              this.RaiseDataMemberChanging("Acesso");
	    	              this._Acesso = value;
	    	              this.RaiseDataMemberChanged("Acesso");
	    	              this.OnAcessoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdPerfil
	    partial void OnIdPerfilChanging(long value);
	    partial void OnIdPerfilChanged();

	    private long _IdPerfil;

	    [DataMember(IsRequired = true, Name = "IdPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PERFIL_REGRA_TRANSACAO.TCS_PERFIL.ID_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL_REGRA_TRANSACAO.TCS_PERFIL.ID_PERFIL")]
	    public long IdPerfil
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
	    //Extensibility Partial Method Definitions For IdPerfilRegraTransacao
	    partial void OnIdPerfilRegraTransacaoChanging(long value);
	    partial void OnIdPerfilRegraTransacaoChanged();

	    private long _IdPerfilRegraTransacao;

	    [DataMember(IsRequired = true, Name = "IdPerfilRegraTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Perfil Regra Transacao", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PERFIL_REGRA_TRANSACAO.ID_PERFIL_REGRA_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL_REGRA_TRANSACAO.ID_PERFIL_REGRA_TRANSACAO")]
	    public long IdPerfilRegraTransacao
	    {
	    	    get
	    	    {
	    	          return _IdPerfilRegraTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdPerfilRegraTransacao != value)
	    	          {
	    	              this.ValidateProperty("IdPerfilRegraTransacao", value);
	    	              this.OnIdPerfilRegraTransacaoChanging(value);
	    	              this.RaiseDataMemberChanging("IdPerfilRegraTransacao");
	    	              this._IdPerfilRegraTransacao = value;
	    	              this.RaiseDataMemberChanged("IdPerfilRegraTransacao");
	    	              this.OnIdPerfilRegraTransacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTransacao
	    partial void OnIdTransacaoChanging(long value);
	    partial void OnIdTransacaoChanged();

	    private long _IdTransacao;

	    [DataMember(IsRequired = true, Name = "IdTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Transacao", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsPerfilRegraTransacao];LookUpTitle[Seleção de (Id Transacao)];LookUpQuery[executeLookUpTcsPerfilRegraTransacao];LookUpFinalize[finalizeLookUpTcsPerfilRegraTransacao];LookUpDisplayColumns[{\"IdTransacao\" : \"\", \"DescTransacao\" : \"Transação\", \"Origem\" : \"\"}];LookUpColumns[{\"IdTransacao\" : false, \"DescTransacao\" : true, \"Origem\" : true}];FilterDataKey[TCS_PERFIL_REGRA_TRANSACAO.ID_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdTransacao#true##12:0###0#false##::LookUpTcsPerfilRegraTransacao##true#false###Linx.Framework.Custom.BV.PerfilFranquia#IQueryable###true#false", EdmKey="TCS_PERFIL_REGRA_TRANSACAO.ID_TRANSACAO")]
	    public long IdTransacao
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
	    //Extensibility Partial Method Definitions For LxRegraAcessoTransacao
	    partial void OnLxRegraAcessoTransacaoChanging(byte value);
	    partial void OnLxRegraAcessoTransacaoChanged();

	    private byte _LxRegraAcessoTransacao;

	    [DataMember(IsRequired = true, Name = "LxRegraAcessoTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Regra Acesso Transação", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[RegraAcesso];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookupLxRegraAcessoTransacao];LookUpTitle[Seleção de (Regra Acesso Transação)];LookUpQuery[executeLookupLxRegraAcessoTransacao];LookUpFinalize[finalizeLookupLxRegraAcessoTransacao];LookUpDisplayColumns[{\"LxRegraAcessoTransacao\" : \"\", \"LxRegraAcessoTransacaoName\" : \"Regra Acesso\"}];LookUpColumns[{\"LxRegraAcessoTransacao\" : false, \"LxRegraAcessoTransacaoName\" : true}];FilterDataKey[TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="byte#LxRegraAcessoTransacao#true##60:0###0#false##::LookupLxRegraAcessoTransacao##true#false###Linx.Framework.Custom.BV.PerfilFranquia#IQueryable###true#false", EdmKey="TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO")]
	    public byte LxRegraAcessoTransacao
	    {
	    	    get
	    	    {
	    	          return _LxRegraAcessoTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxRegraAcessoTransacao != value)
	    	          {
	    	              this.ValidateProperty("LxRegraAcessoTransacao", value);
	    	              this.OnLxRegraAcessoTransacaoChanging(value);
	    	              this.RaiseDataMemberChanging("LxRegraAcessoTransacao");
	    	              this._LxRegraAcessoTransacao = value;
	    	              this.RaiseDataMemberChanged("LxRegraAcessoTransacao");
	    	              this.OnLxRegraAcessoTransacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescTransacao
	    partial void OnDescTransacaoChanging(string value);
	    partial void OnDescTransacaoChanged();

	    private string _DescTransacao;

	    [DataMember(IsRequired = true, Name = "DescTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Transação", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#DescTransacao#false##60:0##Transação#1#true##::LookUpTcsPerfilRegraTransacao##true#false###Linx.Framework.Custom.BV.PerfilFranquia#IQueryable###true#false", EdmKey="")]
	    public string DescTransacao
	    {
	    	    get
	    	    {
	    	          if (_DescTransacao != (GetDescTransacao()))
	    	             _DescTransacao =  GetDescTransacao();
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
	    //Extensibility Partial Method Definitions For Origem
	    partial void OnOrigemChanging(string value);
	    partial void OnOrigemChanged();

	    private string _Origem;

	    [DataMember(IsRequired = true, Name = "Origem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Origem", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#Origem#false##60:0###3#true##::LookUpTcsPerfilRegraTransacao##true#false###Linx.Framework.Custom.BV.PerfilFranquia#IQueryable###true#false", EdmKey="")]
	    public string Origem
	    {
	    	    get
	    	    {
	    	          if (_Origem != (GetOrigem()))
	    	             _Origem =  GetOrigem();
	    	          return _Origem;
	    	    }
	    	    set
	    	    {
	    	          if (this._Origem != value)
	    	          {
	    	              this.ValidateProperty("Origem", value);
	    	              this.OnOrigemChanging(value);
	    	              this.RaiseDataMemberChanging("Origem");
	    	              this._Origem = value;
	    	              this.RaiseDataMemberChanged("Origem");
	    	              this.OnOrigemChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsPerfil _TcsPerfil;
	    [DataMember(Name = "TcsPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsPerfil_TcsPerfilRegraTransacao", "IdPerfil", "IdPerfil", IsForeignKey=true)]
	    public TcsPerfil TcsPerfil
	    {
	        get
	        {
	            return this._TcsPerfil;
	        }
	        set
	        {
	            if (this._TcsPerfil != value)
	            {
	                this._TcsPerfil = value;
	                this.RaisePropertyChanged("TcsPerfilList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_PERFIL_REGRA_TRANSACAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_PERFIL_REGRA_TRANSACAO), QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL_REGRA_TRANSACAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PERFIL_REGRA_TRANSACAO.ID_TRANSACAO", Source = "IdTransacao", Target = "ID_TRANSACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL_REGRA_TRANSACAO", RelationPropertyName = "TCS_PERFIL_REGRA_TRANSACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PERFIL_REGRA_TRANSACAO.TCS_PERFIL.ID_PERFIL", Source = "IdPerfil", Target = "ID_PERFIL", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL", RelationPropertyName = "TCS_PERFIL" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PERFIL_REGRA_TRANSACAO.ID_PERFIL_REGRA_TRANSACAO", Source = "IdPerfilRegraTransacao", Target = "ID_PERFIL_REGRA_TRANSACAO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL_REGRA_TRANSACAO", RelationPropertyName = "TCS_PERFIL_REGRA_TRANSACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO", Source = "LxRegraAcessoTransacao", Target = "LX_REGRA_ACESSO_TRANSACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL_REGRA_TRANSACAO", RelationPropertyName = "TCS_PERFIL_REGRA_TRANSACAO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxRegraAcessoTransacaoValues()
	    {
	    	    return Linx.Framework.Custom.BV.Domains.RegraAcesso.GetValues();
	    }
	    private string _lxRegraAcessoTransacaoName;
	    [DataMember(IsRequired = false, Name = "LxRegraAcessoTransacaoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Regra Acesso Transação", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxRegraAcessoTransacaoName
	    {
	    	    get { if (this.LxRegraAcessoTransacao.IsNull()) { _lxRegraAcessoTransacaoName = String.Empty; } else { string key = this.LxRegraAcessoTransacao.ToString(); var dmValues = this.GetLxRegraAcessoTransacaoValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxRegraAcessoTransacaoName) _lxRegraAcessoTransacaoName = domainName; } return _lxRegraAcessoTransacaoName; } set { _lxRegraAcessoTransacaoName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_PERFIL_BANDEIRA_REDE.TBC_BANDEIRA_REDE.ID_BANDEIRA_REDE,TCS_PERFIL_BANDEIRA_REDE.TCS_PERFIL.ID_PERFIL", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Bandeira / Rede];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#.TCS_PERFIL_BANDEIRA_REDE_LISTA as #Alias#];EdmEntityName[TCS_PERFIL_BANDEIRA_REDE];EntityRelations[TCS_PERFIL(TCS_PERFIL)#TBC_GRUPO_ECONOMICO(TBC_GRUPO_ECONOMICO)#GPECON_SUPERIOR(TBC_GRUPO_ECONOMICO)#TBC_BANDEIRA_REDE(TBC_BANDEIRA_REDE)];EdmParentEntityName[TCS_PERFIL];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsPerfilBandeiraRede")]
	[Serializable()]
	public partial class TcsPerfilBandeiraRede : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(PerfilFranquiaDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsPerfil");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdPerfil"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdPerfil));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsPerfil
	         this.TcsPerfil = (from r in context.GetTcsPerfilByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For DescBandeiraRede
	    partial void OnDescBandeiraRedeChanging(string value);
	    partial void OnDescBandeiraRedeChanged();

	    private string _DescBandeiraRede;

	    [DataMember(IsRequired = true, Name = "DescBandeiraRede", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bandeira / Rede", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(120)]
	    [FunctionalPoint("Precision[120:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTbcBandeiraRede];LookUpTitle[Seleção de (Bandeira / Rede)];LookUpQuery[executeLookUpTbcBandeiraRede];LookUpFinalize[finalizeLookUpTbcBandeiraRede];LookUpDisplayColumns[{\"IdBandeiraR\" : \"Id Bandeira / Rede\", \"DescBandeiraRede\" : \"Bandeira / Rede\"}];LookUpColumns[{\"IdBandeiraR\" : true, \"DescBandeiraRede\" : true}];FilterDataKey[TCS_PERFIL_BANDEIRA_REDE.TBC_BANDEIRA_REDE.DESC_BANDEIRA_REDE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#DescBandeiraRede#false##60:0##Bandeira / Rede#1#true##::LookUpTbcBandeiraRede##true#false#TBC_BANDEIRA_REDE#TBC_BANDEIRA_REDE#Linx.Framework.Custom.BV.PerfilFranquia#IQueryable###true#true", EdmKey="TCS_PERFIL_BANDEIRA_REDE.TBC_BANDEIRA_REDE.DESC_BANDEIRA_REDE")]
	    public string DescBandeiraRede
	    {
	    	    get
	    	    {
	    	          return _DescBandeiraRede;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescBandeiraRede != value)
	    	          {
	    	              this.ValidateProperty("DescBandeiraRede", value);
	    	              this.OnDescBandeiraRedeChanging(value);
	    	              this.RaiseDataMemberChanging("DescBandeiraRede");
	    	              this._DescBandeiraRede = value;
	    	              this.RaiseDataMemberChanged("DescBandeiraRede");
	    	              this.OnDescBandeiraRedeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdBandeiraR
	    partial void OnIdBandeiraRChanging(int value);
	    partial void OnIdBandeiraRChanged();

	    private int _IdBandeiraR;

	    [DataMember(IsRequired = true, Name = "IdBandeiraR", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Bandeira Rede", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTbcBandeiraRede];LookUpTitle[Seleção de (Id Bandeira Rede)];LookUpQuery[executeLookUpTbcBandeiraRede];LookUpFinalize[finalizeLookUpTbcBandeiraRede];LookUpDisplayColumns[{\"IdBandeiraR\" : \"Id Bandeira / Rede\", \"DescBandeiraRede\" : \"Bandeira / Rede\"}];LookUpColumns[{\"IdBandeiraR\" : true, \"DescBandeiraRede\" : true}];FilterDataKey[TCS_PERFIL_BANDEIRA_REDE.TBC_BANDEIRA_REDE.ID_BANDEIRA_REDE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="int#IdBandeiraR#true##10:0##Id Bandeira / Rede#0#true##::LookUpTbcBandeiraRede##true#false#TBC_BANDEIRA_REDE#TBC_BANDEIRA_REDE#Linx.Framework.Custom.BV.PerfilFranquia#IQueryable###true#true", EdmKey="TCS_PERFIL_BANDEIRA_REDE.TBC_BANDEIRA_REDE.ID_BANDEIRA_REDE")]
	    public int IdBandeiraR
	    {
	    	    get
	    	    {
	    	          return _IdBandeiraR;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdBandeiraR != value)
	    	          {
	    	              this.ValidateProperty("IdBandeiraR", value);
	    	              this.OnIdBandeiraRChanging(value);
	    	              this.RaiseDataMemberChanging("IdBandeiraR");
	    	              this._IdBandeiraR = value;
	    	              this.RaiseDataMemberChanged("IdBandeiraR");
	    	              this.OnIdBandeiraRChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdPerfil
	    partial void OnIdPerfilChanging(long value);
	    partial void OnIdPerfilChanged();

	    private long _IdPerfil;

	    [DataMember(IsRequired = true, Name = "IdPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PERFIL_BANDEIRA_REDE.TCS_PERFIL.ID_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL_BANDEIRA_REDE.TCS_PERFIL.ID_PERFIL")]
	    public long IdPerfil
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

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsPerfil _TcsPerfil;
	    [DataMember(Name = "TcsPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsPerfil_TcsPerfilBandeiraRede", "IdPerfil", "IdPerfil", IsForeignKey=true)]
	    public TcsPerfil TcsPerfil
	    {
	        get
	        {
	            return this._TcsPerfil;
	        }
	        set
	        {
	            if (this._TcsPerfil != value)
	            {
	                this._TcsPerfil = value;
	                this.RaisePropertyChanged("TcsPerfilList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_PERFIL_BANDEIRA_REDE").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_PERFIL_BANDEIRA_REDE), QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL_BANDEIRA_REDE" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PERFIL_BANDEIRA_REDE.TCS_PERFIL.ID_PERFIL", Source = "IdPerfil", Target = "ID_PERFIL", TargetKeyName = "ID_PERFIL", NoUpdatable = false, IsKey = true, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL", RelationPropertyName = "TCS_PERFIL" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PERFIL_BANDEIRA_REDE.TBC_BANDEIRA_REDE.ID_BANDEIRA_REDE", Source = "IdBandeiraR", Target = "ID_BANDEIRA_REDE", TargetKeyName = "ID_BANDEIRA_REDE", NoUpdatable = false, IsKey = true, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TBC_BANDEIRA_REDE", RelationPropertyName = "TBC_BANDEIRA_REDE" });

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

		

	[LinxPublicationView(PrimaryKeys="TCS_PERFIL_FILIAL.ID_TCS_PERFIL_FILIAL", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Filial];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#.TCS_PERFIL_FILIAL_LISTA as #Alias#];EdmEntityName[TCS_PERFIL_FILIAL];EntityRelations[TCS_PERFIL(TCS_PERFIL)#TBC_GRUPO_ECONOMICO(TBC_GRUPO_ECONOMICO)#GPECON_SUPERIOR(TBC_GRUPO_ECONOMICO)#TBC_FILIAL(TBC_FILIAL)#MATRIZ_CONTABIL(TBC_FILIAL)#TBC_PFJ(TBC_PFJ)#TBC_FILIAL_LISTA(TBC_FILIAL)];EdmParentEntityName[TCS_PERFIL];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsPerfilFilial")]
	[Serializable()]
	public partial class TcsPerfilFilial : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(PerfilFranquiaDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsPerfil");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdPerfil"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdPerfil));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsPerfil
	         this.TcsPerfil = (from r in context.GetTcsPerfilByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For CodigoFilial
	    partial void OnCodigoFilialChanging(string value);
	    partial void OnCodigoFilialChanged();

	    private string _CodigoFilial;

	    [DataMember(Name = "CodigoFilial", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Código Filial", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(18)]
	    [FunctionalPoint("Precision[18:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTbcFilial];LookUpTitle[Seleção de (Código Filial)];LookUpQuery[executeLookUpTbcFilial];LookUpFinalize[finalizeLookUpTbcFilial];LookUpDisplayColumns[{\"CodigoFilial\" : \"Código Filial\", \"IdFilialPfj\" : \"Id Filial Pfj\", \"NomeFilial\" : \"Nome Fantasia\"}];LookUpColumns[{\"CodigoFilial\" : true, \"IdFilialPfj\" : false, \"NomeFilial\" : true}];FilterDataKey[TCS_PERFIL_FILIAL.TBC_FILIAL.CODIGO_FILIAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#CodigoFilial#false##18:0##Código Filial#0#true##::LookUpTbcFilial##true#false#TBC_FILIAL#TBC_FILIAL#Linx.Framework.Custom.BV.PerfilFranquia#IQueryable###true#true", EdmKey="TCS_PERFIL_FILIAL.TBC_FILIAL.CODIGO_FILIAL")]
	    public string CodigoFilial
	    {
	    	    get
	    	    {
	    	          return _CodigoFilial;
	    	    }
	    	    set
	    	    {
	    	          if (this._CodigoFilial != value)
	    	          {
	    	              this.ValidateProperty("CodigoFilial", value);
	    	              this.OnCodigoFilialChanging(value);
	    	              this.RaiseDataMemberChanging("CodigoFilial");
	    	              this._CodigoFilial = value;
	    	              this.RaiseDataMemberChanged("CodigoFilial");
	    	              this.OnCodigoFilialChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdFilialPfj
	    partial void OnIdFilialPfjChanging(int value);
	    partial void OnIdFilialPfjChanged();

	    private int _IdFilialPfj;

	    [DataMember(IsRequired = true, Name = "IdFilialPfj", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Filial Pfj", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTbcFilial];LookUpTitle[Seleção de (Id Filial Pfj)];LookUpQuery[executeLookUpTbcFilial];LookUpFinalize[finalizeLookUpTbcFilial];LookUpDisplayColumns[{\"CodigoFilial\" : \"Código Filial\", \"IdFilialPfj\" : \"Id Filial Pfj\", \"NomeFilial\" : \"Nome Fantasia\"}];LookUpColumns[{\"CodigoFilial\" : true, \"IdFilialPfj\" : false, \"NomeFilial\" : true}];FilterDataKey[TCS_PERFIL_FILIAL.TBC_FILIAL.ID_FILIAL_PFJ];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="int#IdFilialPfj#true##10:0##Id Filial Pfj#1#false##::LookUpTbcFilial##true#false#TBC_FILIAL#TBC_FILIAL#Linx.Framework.Custom.BV.PerfilFranquia#IQueryable###true#true", EdmKey="TCS_PERFIL_FILIAL.TBC_FILIAL.ID_FILIAL_PFJ")]
	    public int IdFilialPfj
	    {
	    	    get
	    	    {
	    	          return _IdFilialPfj;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdFilialPfj != value)
	    	          {
	    	              this.ValidateProperty("IdFilialPfj", value);
	    	              this.OnIdFilialPfjChanging(value);
	    	              this.RaiseDataMemberChanging("IdFilialPfj");
	    	              this._IdFilialPfj = value;
	    	              this.RaiseDataMemberChanged("IdFilialPfj");
	    	              this.OnIdFilialPfjChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdPerfil
	    partial void OnIdPerfilChanging(long value);
	    partial void OnIdPerfilChanged();

	    private long _IdPerfil;

	    [DataMember(IsRequired = true, Name = "IdPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PERFIL_FILIAL.TCS_PERFIL.ID_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL_FILIAL.TCS_PERFIL.ID_PERFIL")]
	    public long IdPerfil
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
	    //Extensibility Partial Method Definitions For IdTcsPerfilFilial
	    partial void OnIdTcsPerfilFilialChanging(long value);
	    partial void OnIdTcsPerfilFilialChanged();

	    private long _IdTcsPerfilFilial;

	    [DataMember(IsRequired = true, Name = "IdTcsPerfilFilial", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Perfil Filial", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PERFIL_FILIAL.ID_TCS_PERFIL_FILIAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL_FILIAL.ID_TCS_PERFIL_FILIAL")]
	    public long IdTcsPerfilFilial
	    {
	    	    get
	    	    {
	    	          return _IdTcsPerfilFilial;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsPerfilFilial != value)
	    	          {
	    	              this.ValidateProperty("IdTcsPerfilFilial", value);
	    	              this.OnIdTcsPerfilFilialChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsPerfilFilial");
	    	              this._IdTcsPerfilFilial = value;
	    	              this.RaiseDataMemberChanged("IdTcsPerfilFilial");
	    	              this.OnIdTcsPerfilFilialChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeFilial
	    partial void OnNomeFilialChanging(string value);
	    partial void OnNomeFilialChanged();

	    private string _NomeFilial;

	    [DataMember(Name = "NomeFilial", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Fantasia", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTbcFilial];LookUpTitle[Seleção de (Nome Fantasia)];LookUpQuery[executeLookUpTbcFilial];LookUpFinalize[finalizeLookUpTbcFilial];LookUpDisplayColumns[{\"CodigoFilial\" : \"Código Filial\", \"IdFilialPfj\" : \"Id Filial Pfj\", \"NomeFilial\" : \"Nome Fantasia\"}];LookUpColumns[{\"CodigoFilial\" : true, \"IdFilialPfj\" : false, \"NomeFilial\" : true}];FilterDataKey[TCS_PERFIL_FILIAL.TBC_FILIAL.NOME_FILIAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#NomeFilial#false##60:0##Nome Fantasia#2#true##::LookUpTbcFilial##true#false#TBC_FILIAL#TBC_FILIAL#Linx.Framework.Custom.BV.PerfilFranquia#IQueryable###true#true", EdmKey="TCS_PERFIL_FILIAL.TBC_FILIAL.NOME_FILIAL")]
	    public string NomeFilial
	    {
	    	    get
	    	    {
	    	          return _NomeFilial;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeFilial != value)
	    	          {
	    	              this.ValidateProperty("NomeFilial", value);
	    	              this.OnNomeFilialChanging(value);
	    	              this.RaiseDataMemberChanging("NomeFilial");
	    	              this._NomeFilial = value;
	    	              this.RaiseDataMemberChanged("NomeFilial");
	    	              this.OnNomeFilialChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsPerfil _TcsPerfil;
	    [DataMember(Name = "TcsPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsPerfil_TcsPerfilFilial", "IdPerfil", "IdPerfil", IsForeignKey=true)]
	    public TcsPerfil TcsPerfil
	    {
	        get
	        {
	            return this._TcsPerfil;
	        }
	        set
	        {
	            if (this._TcsPerfil != value)
	            {
	                this._TcsPerfil = value;
	                this.RaisePropertyChanged("TcsPerfilList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_PERFIL_FILIAL").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_PERFIL_FILIAL), QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL_FILIAL" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PERFIL_FILIAL.TCS_PERFIL.ID_PERFIL", Source = "IdPerfil", Target = "ID_PERFIL", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL", RelationPropertyName = "TCS_PERFIL" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PERFIL_FILIAL.ID_TCS_PERFIL_FILIAL", Source = "IdTcsPerfilFilial", Target = "ID_TCS_PERFIL_FILIAL", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL_FILIAL", RelationPropertyName = "TCS_PERFIL_FILIAL" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PERFIL_FILIAL.TBC_FILIAL.ID_FILIAL_PFJ", Source = "IdFilialPfj", Target = "ID_FILIAL_PFJ", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TBC_FILIAL", RelationPropertyName = "TBC_FILIAL" });

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

		

	[LinxPublicationView(PrimaryKeys="TBC_FILIAL.ID_FILIAL_PFJ", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TbcFilial];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[];EdmEntityName[TBC_FILIAL];EntityRelations[MATRIZ_CONTABIL(TBC_FILIAL)#TBC_GRUPO_ECONOMICO(TBC_GRUPO_ECONOMICO)#GPECON_SUPERIOR(TBC_GRUPO_ECONOMICO)#TBC_PFJ(TBC_PFJ)#TBC_FILIAL_LISTA(TBC_FILIAL)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TbcFilial")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.Custom.BV.PerfilFranquia.TbcFilial")]
	public partial class TbcFilial : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For CodigoFilial
	    partial void OnCodigoFilialChanging(string value);
	    partial void OnCodigoFilialChanged();

	    private string _CodigoFilial;

	    [DataMember(Name = "CodigoFilial", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Codigo Filial", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(18)]
	    [FunctionalPoint("Precision[18:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBC_FILIAL.CODIGO_FILIAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_FILIAL.CODIGO_FILIAL")]
	    public string CodigoFilial
	    {
	    	    get
	    	    {
	    	          return _CodigoFilial;
	    	    }
	    	    set
	    	    {
	    	          if (this._CodigoFilial != value)
	    	          {
	    	              this.ValidateProperty("CodigoFilial", value);
	    	              this.OnCodigoFilialChanging(value);
	    	              this.RaiseDataMemberChanging("CodigoFilial");
	    	              this._CodigoFilial = value;
	    	              this.RaiseDataMemberChanged("CodigoFilial");
	    	              this.OnCodigoFilialChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdFilialPfj
	    partial void OnIdFilialPfjChanging(int value);
	    partial void OnIdFilialPfjChanged();

	    private int _IdFilialPfj;

	    [DataMember(IsRequired = true, Name = "IdFilialPfj", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Filial Pfj", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBC_FILIAL.ID_FILIAL_PFJ];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_FILIAL.ID_FILIAL_PFJ")]
	    public int IdFilialPfj
	    {
	    	    get
	    	    {
	    	          return _IdFilialPfj;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdFilialPfj != value)
	    	          {
	    	              this.ValidateProperty("IdFilialPfj", value);
	    	              this.OnIdFilialPfjChanging(value);
	    	              this.RaiseDataMemberChanging("IdFilialPfj");
	    	              this._IdFilialPfj = value;
	    	              this.RaiseDataMemberChanged("IdFilialPfj");
	    	              this.OnIdFilialPfjChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeFilial
	    partial void OnNomeFilialChanging(string value);
	    partial void OnNomeFilialChanged();

	    private string _NomeFilial;

	    [DataMember(Name = "NomeFilial", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Filial", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBC_FILIAL.NOME_FILIAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_FILIAL.NOME_FILIAL")]
	    public string NomeFilial
	    {
	    	    get
	    	    {
	    	          return _NomeFilial;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeFilial != value)
	    	          {
	    	              this.ValidateProperty("NomeFilial", value);
	    	              this.OnNomeFilialChanging(value);
	    	              this.RaiseDataMemberChanging("NomeFilial");
	    	              this._NomeFilial = value;
	    	              this.RaiseDataMemberChanged("NomeFilial");
	    	              this.OnNomeFilialChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TBC_FILIAL").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TBC_FILIAL), QualifiedEntitySetName = "ControleSistemaContext.TBC_FILIAL" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TBC_FILIAL.NOME_FILIAL", Source = "NomeFilial", Target = "NOME_FILIAL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TBC_FILIAL", RelationPropertyName = "TBC_FILIAL" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TBC_FILIAL.CODIGO_FILIAL", Source = "CodigoFilial", Target = "CODIGO_FILIAL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TBC_FILIAL", RelationPropertyName = "TBC_FILIAL" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TBC_FILIAL.ID_FILIAL_PFJ", Source = "IdFilialPfj", Target = "ID_FILIAL_PFJ", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TBC_FILIAL", RelationPropertyName = "TBC_FILIAL" });

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

		

	[LinxPublicationView(PrimaryKeys="SyncInfo.EntityUniqueKey", IsUpdatable=false, EdmName="")]
		
	[DataContract(IsReference = false, Name = "SyncInfo")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.Custom.BV.PerfilFranquia.SyncInfo")]
	public partial class SyncInfo 
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
	 


	    private string _Operacao;

	    [DataMember(Name = "Operacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Operacao
	    {
	    	    get
	    	    {
	    	          if (_Operacao.IsNullOrEmpty())
	    	             _Operacao =  String.Empty;
	    	          return _Operacao;
	    	    }
	    	    set
	    	    {
	    	          this._Operacao = value;
	    	    }
	    }

	    private Dictionary<string, string> _Headers;

	    [DataMember(Name = "Headers", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public Dictionary<string, string> Headers
	    {
	    	    get
	    	    {
	    	          return _Headers;
	    	    }
	    	    set
	    	    {
	    	          this._Headers = value;
	    	    }
	    }

	    private long _IdPerfil;

	    [DataMember(Name = "IdPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public long IdPerfil
	    {
	    	    get
	    	    {
	    	          return _IdPerfil;
	    	    }
	    	    set
	    	    {
	    	          this._IdPerfil = value;
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

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Usuários Vinculados];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#.TCS_USUARIO_PERFIL_LISTA as #Alias#];EdmEntityName[TCS_USUARIO_PERFIL];EntityRelations[TCS_PERFIL(TCS_PERFIL)#TBC_GRUPO_ECONOMICO(TBC_GRUPO_ECONOMICO)#GPECON_SUPERIOR(TBC_GRUPO_ECONOMICO)#TCS_USUARIO(TCS_USUARIO)];EdmParentEntityName[TCS_PERFIL];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioPerfil")]
	[Serializable()]
	public partial class TcsUsuarioPerfilParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For IdLinx
	    partial void OnIdLinxChanging(int value);
	    partial void OnIdLinxChanged();

	    private int _IdLinx;

	    [DataMember(IsRequired = true, Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuario];LookUpTitle[Seleção de (Id Linx)];LookUpQuery[executeLookUpTcsUsuario];LookUpFinalize[finalizeLookUpTcsUsuario];LookUpDisplayColumns[{\"IdUsuario\" : \"Id Usuario\", \"NomeUsuario\" : \"Nome Usuario\", \"IdLinx\" : \"Id Linx\", \"UidUsuario\" : \"Uid Usuario\"}];LookUpColumns[{\"IdUsuario\" : false, \"NomeUsuario\" : true, \"IdLinx\" : false, \"UidUsuario\" : false}];FilterDataKey[TCS_USUARIO_PERFIL.TCS_USUARIO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="int#IdLinx#false##0:0##Id Linx#2#false##::LookUpTcsUsuario##true#false#TCS_USUARIO#TCS_USUARIO#Linx.Framework.Custom.BV.PerfilFranquia#IQueryable###true#true", EdmKey="TCS_USUARIO_PERFIL.TCS_USUARIO.ID_LINX")]
	    public int IdLinx
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
	    //Extensibility Partial Method Definitions For IdPerfil
	    partial void OnIdPerfilChanging(long value);
	    partial void OnIdPerfilChanged();

	    private long _IdPerfil;

	    [DataMember(IsRequired = true, Name = "IdPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_PERFIL.TCS_PERFIL.ID_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_PERFIL.TCS_PERFIL.ID_PERFIL")]
	    public long IdPerfil
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
	    //Extensibility Partial Method Definitions For IdTcsUsuarioPerfil
	    partial void OnIdTcsUsuarioPerfilChanging(long value);
	    partial void OnIdTcsUsuarioPerfilChanged();

	    private long _IdTcsUsuarioPerfil;

	    [DataMember(IsRequired = true, Name = "IdTcsUsuarioPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Perfil", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_PERFIL.ID_TCS_USUARIO_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_PERFIL.ID_TCS_USUARIO_PERFIL")]
	    public long IdTcsUsuarioPerfil
	    {
	    	    get
	    	    {
	    	          return _IdTcsUsuarioPerfil;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsUsuarioPerfil != value)
	    	          {
	    	              this.ValidateProperty("IdTcsUsuarioPerfil", value);
	    	              this.OnIdTcsUsuarioPerfilChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsUsuarioPerfil");
	    	              this._IdTcsUsuarioPerfil = value;
	    	              this.RaiseDataMemberChanged("IdTcsUsuarioPerfil");
	    	              this.OnIdTcsUsuarioPerfilChanged();
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
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuario];LookUpTitle[Seleção de (Id Usuario)];LookUpQuery[executeLookUpTcsUsuario];LookUpFinalize[finalizeLookUpTcsUsuario];LookUpDisplayColumns[{\"IdUsuario\" : \"Id Usuario\", \"NomeUsuario\" : \"Nome Usuario\", \"IdLinx\" : \"Id Linx\", \"UidUsuario\" : \"Uid Usuario\"}];LookUpColumns[{\"IdUsuario\" : false, \"NomeUsuario\" : true, \"IdLinx\" : false, \"UidUsuario\" : false}];FilterDataKey[TCS_USUARIO_PERFIL.TCS_USUARIO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="long#IdUsuario#true##0:0##Id Usuario#0#false##::LookUpTcsUsuario##true#false#TCS_USUARIO#TCS_USUARIO#Linx.Framework.Custom.BV.PerfilFranquia#IQueryable###true#true", EdmKey="TCS_USUARIO_PERFIL.TCS_USUARIO.ID_USUARIO")]
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
	    [Display(Name = "Usuário", Description="", Order = 17, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuario];LookUpTitle[Seleção de (Usuário)];LookUpQuery[executeLookUpTcsUsuario];LookUpFinalize[finalizeLookUpTcsUsuario];LookUpDisplayColumns[{\"IdUsuario\" : \"Id Usuario\", \"NomeUsuario\" : \"Nome Usuario\", \"IdLinx\" : \"Id Linx\", \"UidUsuario\" : \"Uid Usuario\"}];LookUpColumns[{\"IdUsuario\" : false, \"NomeUsuario\" : true, \"IdLinx\" : false, \"UidUsuario\" : false}];FilterDataKey[TCS_USUARIO_PERFIL.TCS_USUARIO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#NomeUsuario#false##250:0##Nome Usuario#1#true##::LookUpTcsUsuario##true#false#TCS_USUARIO#TCS_USUARIO#Linx.Framework.Custom.BV.PerfilFranquia#IQueryable###true#true", EdmKey="TCS_USUARIO_PERFIL.TCS_USUARIO.NOME_USUARIO")]
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
	    //Extensibility Partial Method Definitions For UidUsuario
	    partial void OnUidUsuarioChanging(Guid value);
	    partial void OnUidUsuarioChanged();

	    private Guid _UidUsuario;

	    [DataMember(IsRequired = true, Name = "UidUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Usuario", Description="", Order = 22, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuario];LookUpTitle[Seleção de (Uid Usuario)];LookUpQuery[executeLookUpTcsUsuario];LookUpFinalize[finalizeLookUpTcsUsuario];LookUpDisplayColumns[{\"IdUsuario\" : \"Id Usuario\", \"NomeUsuario\" : \"Nome Usuario\", \"IdLinx\" : \"Id Linx\", \"UidUsuario\" : \"Uid Usuario\"}];LookUpColumns[{\"IdUsuario\" : false, \"NomeUsuario\" : true, \"IdLinx\" : false, \"UidUsuario\" : false}];FilterDataKey[TCS_USUARIO_PERFIL.TCS_USUARIO.UID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Guid#UidUsuario#false##36:0##Uid Usuario#3#false##::LookUpTcsUsuario##true#false#TCS_USUARIO#TCS_USUARIO#Linx.Framework.Custom.BV.PerfilFranquia#IQueryable###true#true", EdmKey="TCS_USUARIO_PERFIL.TCS_USUARIO.UID_USUARIO")]
	    public Guid UidUsuario
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
	    //Extensibility Partial Method Definitions For DescPerfil
	    partial void OnDescPerfilChanging(string value);
	    partial void OnDescPerfilChanged();

	    private string _DescPerfil;

	    [DataMember(IsRequired = true, Name = "DescPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_PERFIL.TCS_PERFIL.DESC_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL.DESC_PERFIL")]
	    public string DescPerfil
	    {
	    	    get
	    	    {
	    	          return _DescPerfil;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescPerfil != value)
	    	          {
	    	              this.ValidateProperty("DescPerfil", value);
	    	              this.OnDescPerfilChanging(value);
	    	              this.RaiseDataMemberChanging("DescPerfil");
	    	              this._DescPerfil = value;
	    	              this.RaiseDataMemberChanged("DescPerfil");
	    	              this.OnDescPerfilChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdGpeconFranquia
	    partial void OnIdGpeconFranquiaChanging(System.Nullable<int> value);
	    partial void OnIdGpeconFranquiaChanged();

	    private System.Nullable<int> _IdGpeconFranquia;

	    [DataMember(Name = "IdGpeconFranquia", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Gpecon", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_PERFIL.TCS_PERFIL.TBC_GRUPO_ECONOMICO.ID_GPECON];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL.TBC_GRUPO_ECONOMICO.ID_GPECON")]
	    public System.Nullable<int> IdGpeconFranquia
	    {
	    	    get
	    	    {
	    	          return _IdGpeconFranquia;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdGpeconFranquia != value)
	    	          {
	    	              this.ValidateProperty("IdGpeconFranquia", value);
	    	              this.OnIdGpeconFranquiaChanging(value);
	    	              this.RaiseDataMemberChanging("IdGpeconFranquia");
	    	              this._IdGpeconFranquia = value;
	    	              this.RaiseDataMemberChanged("IdGpeconFranquia");
	    	              this.OnIdGpeconFranquiaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdPerfilOrigem
	    partial void OnIdPerfilOrigemChanging(System.Nullable<long> value);
	    partial void OnIdPerfilOrigemChanged();

	    private System.Nullable<long> _IdPerfilOrigem;

	    [DataMember(Name = "IdPerfilOrigem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Perfil Origem", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_PERFIL.TCS_PERFIL.ID_PERFIL_ORIGEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL.ID_PERFIL_ORIGEM")]
	    public System.Nullable<long> IdPerfilOrigem
	    {
	    	    get
	    	    {
	    	          return _IdPerfilOrigem;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdPerfilOrigem != value)
	    	          {
	    	              this.ValidateProperty("IdPerfilOrigem", value);
	    	              this.OnIdPerfilOrigemChanging(value);
	    	              this.RaiseDataMemberChanging("IdPerfilOrigem");
	    	              this._IdPerfilOrigem = value;
	    	              this.RaiseDataMemberChanged("IdPerfilOrigem");
	    	              this.OnIdPerfilOrigemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Inativo
	    partial void OnInativoChanging(bool value);
	    partial void OnInativoChanged();

	    private bool _Inativo;

	    [DataMember(IsRequired = true, Name = "Inativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inativo", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_PERFIL.TCS_PERFIL.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL.INATIVO")]
	    public bool Inativo
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

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_USUARIO_PERFIL").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_USUARIO_PERFIL), QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_PERFIL" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_PERFIL.TCS_PERFIL.ID_PERFIL", Source = "IdPerfil", Target = "ID_PERFIL", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL", RelationPropertyName = "TCS_PERFIL" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_PERFIL.ID_TCS_USUARIO_PERFIL", Source = "IdTcsUsuarioPerfil", Target = "ID_TCS_USUARIO_PERFIL", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_PERFIL", RelationPropertyName = "TCS_USUARIO_PERFIL" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_PERFIL.TCS_USUARIO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });

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

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Módulo];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#.TCS_PERFIL_REGRA_MODULO_LISTA as #Alias#];EdmEntityName[TCS_PERFIL_REGRA_MODULO];EntityRelations[TCS_PERFIL(TCS_PERFIL)#TBC_GRUPO_ECONOMICO(TBC_GRUPO_ECONOMICO)#GPECON_SUPERIOR(TBC_GRUPO_ECONOMICO)];EdmParentEntityName[TCS_PERFIL];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsPerfilRegraModulo")]
	[Serializable()]
	public partial class TcsPerfilRegraModuloParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For IdModulo
	    partial void OnIdModuloChanging(long value);
	    partial void OnIdModuloChanged();

	    private long _IdModulo;

	    [DataMember(IsRequired = true, Name = "IdModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsPerfilRegraModulo];LookUpTitle[Seleção de (Id Modulo)];LookUpQuery[executeLookUpTcsPerfilRegraModulo];LookUpFinalize[finalizeLookUpTcsPerfilRegraModulo];LookUpDisplayColumns[{\"IdModulo\" : \"\", \"DescModulo\" : \"Módulo\", \"DescAplicativo\" : \"Aplicativo\", \"Origem\" : \"\"}];LookUpColumns[{\"IdModulo\" : false, \"DescModulo\" : true, \"DescAplicativo\" : true, \"Origem\" : true}];FilterDataKey[TCS_PERFIL_REGRA_MODULO.ID_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdModulo#true##12###0#false##::LookUpTcsPerfilRegraModulo##true#false###Linx.Framework.Custom.BV.PerfilFranquia#IQueryable###true#false", EdmKey="TCS_PERFIL_REGRA_MODULO.ID_MODULO")]
	    public long IdModulo
	    {
	    	    get
	    	    {
	    	          return _IdModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdModulo != value)
	    	          {
	    	              this.ValidateProperty("IdModulo", value);
	    	              this.OnIdModuloChanging(value);
	    	              this.RaiseDataMemberChanging("IdModulo");
	    	              this._IdModulo = value;
	    	              this.RaiseDataMemberChanged("IdModulo");
	    	              this.OnIdModuloChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdPerfil
	    partial void OnIdPerfilChanging(long value);
	    partial void OnIdPerfilChanged();

	    private long _IdPerfil;

	    [DataMember(IsRequired = true, Name = "IdPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PERFIL_REGRA_MODULO.TCS_PERFIL.ID_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL_REGRA_MODULO.TCS_PERFIL.ID_PERFIL")]
	    public long IdPerfil
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
	    //Extensibility Partial Method Definitions For IdPerfilRegraModulo
	    partial void OnIdPerfilRegraModuloChanging(long value);
	    partial void OnIdPerfilRegraModuloChanged();

	    private long _IdPerfilRegraModulo;

	    [DataMember(IsRequired = true, Name = "IdPerfilRegraModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Perfil Regra Modulo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PERFIL_REGRA_MODULO.ID_PERFIL_REGRA_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL_REGRA_MODULO.ID_PERFIL_REGRA_MODULO")]
	    public long IdPerfilRegraModulo
	    {
	    	    get
	    	    {
	    	          return _IdPerfilRegraModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdPerfilRegraModulo != value)
	    	          {
	    	              this.ValidateProperty("IdPerfilRegraModulo", value);
	    	              this.OnIdPerfilRegraModuloChanging(value);
	    	              this.RaiseDataMemberChanging("IdPerfilRegraModulo");
	    	              this._IdPerfilRegraModulo = value;
	    	              this.RaiseDataMemberChanged("IdPerfilRegraModulo");
	    	              this.OnIdPerfilRegraModuloChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxRegraAcessoModulo
	    partial void OnLxRegraAcessoModuloChanging(byte value);
	    partial void OnLxRegraAcessoModuloChanged();

	    private byte _LxRegraAcessoModulo;

	    [DataMember(IsRequired = true, Name = "LxRegraAcessoModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Regra Módulo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[RegraAcesso];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpLxRegraAcessoModulo];LookUpTitle[Seleção de (Regra Módulo)];LookUpQuery[executeLookUpLxRegraAcessoModulo];LookUpFinalize[finalizeLookUpLxRegraAcessoModulo];LookUpDisplayColumns[{\"LxRegraAcessoModulo\" : \"Regra Módulo\", \"LxRegraAcessoModuloName\" : \"Regra Acesso\"}];LookUpColumns[{\"LxRegraAcessoModulo\" : false, \"LxRegraAcessoModuloName\" : true}];FilterDataKey[TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="byte#LxRegraAcessoModulo#true##0##Regra Módulo#0#false##::LookUpLxRegraAcessoModulo##true#false###Linx.Framework.Custom.BV.PerfilFranquia#IQueryable###true#false", EdmKey="TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO")]
	    public byte LxRegraAcessoModulo
	    {
	    	    get
	    	    {
	    	          return _LxRegraAcessoModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxRegraAcessoModulo != value)
	    	          {
	    	              this.ValidateProperty("LxRegraAcessoModulo", value);
	    	              this.OnLxRegraAcessoModuloChanging(value);
	    	              this.RaiseDataMemberChanging("LxRegraAcessoModulo");
	    	              this._LxRegraAcessoModulo = value;
	    	              this.RaiseDataMemberChanged("LxRegraAcessoModulo");
	    	              this.OnLxRegraAcessoModuloChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Acesso
	    partial void OnAcessoChanging(string value);
	    partial void OnAcessoChanged();

	    private string _Acesso;

	    [DataMember(IsRequired = true, Name = "Acesso", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Regra Acesso", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];LookUpName[LookUpLxRegraAcessoModulo];LookUpTitle[Seleção de (Regra Acesso)];LookUpQuery[executeLookUpLxRegraAcessoModulo];LookUpFinalize[finalizeLookUpLxRegraAcessoModulo];LookUpDisplayColumns[{\"LxRegraAcessoModulo\" : \"Regra Módulo\", \"LxRegraAcessoModuloName\" : \"Regra Acesso\"}];LookUpColumns[{\"LxRegraAcessoModulo\" : false, \"LxRegraAcessoModuloName\" : true}];FilterDataKey[((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 1 ? 'Acesso Bloqueado' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 2 ? 'Acesso Total' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 13 ? 'Acesso por Transação' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 5 ? 'Alterar' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 12 ? 'Criar Pesquisa' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 10 ? 'Criar Relatório' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 6 ? 'Excluir' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 9 ? 'Exportar' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 8 ? 'Imprimir' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 4 ? 'Incluir' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 11 ? 'Layout' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 7 ? 'Pesquisa Especial' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 3 ? 'Pesquisar' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 99 ? 'Regra Transação' : ''))))))))))))))];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#LxRegraAcessoModuloName#false##250:0##Regra Acesso#1#true##::LookUpLxRegraAcessoModulo##true#false###Linx.Framework.Custom.BV.PerfilFranquia#IQueryable###true#false", EdmKey="((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) == 1 ? \"Acesso Bloqueado\" : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) == 2 ? \"Acesso Total\" : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) == 13 ? \"Acesso por Transação\" : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) == 5 ? \"Alterar\" : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) == 12 ? \"Criar Pesquisa\" : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) == 10 ? \"Criar Relatório\" : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) == 6 ? \"Excluir\" : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) == 9 ? \"Exportar\" : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) == 8 ? \"Imprimir\" : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) == 4 ? \"Incluir\" : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) == 11 ? \"Layout\" : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) == 7 ? \"Pesquisa Especial\" : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) == 3 ? \"Pesquisar\" : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) == 99 ? \"Regra Transação\" : \"\"))))))))))))))")]
	    public string Acesso
	    {
	    	    get
	    	    {
	    	          return _Acesso;
	    	    }
	    	    set
	    	    {
	    	          if (this._Acesso != value)
	    	          {
	    	              this.ValidateProperty("Acesso", value);
	    	              this.OnAcessoChanging(value);
	    	              this.RaiseDataMemberChanging("Acesso");
	    	              this._Acesso = value;
	    	              this.RaiseDataMemberChanged("Acesso");
	    	              this.OnAcessoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescPerfil
	    partial void OnDescPerfilChanging(string value);
	    partial void OnDescPerfilChanged();

	    private string _DescPerfil;

	    [DataMember(IsRequired = true, Name = "DescPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_PERFIL_REGRA_MODULO.TCS_PERFIL.DESC_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL.DESC_PERFIL")]
	    public string DescPerfil
	    {
	    	    get
	    	    {
	    	          return _DescPerfil;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescPerfil != value)
	    	          {
	    	              this.ValidateProperty("DescPerfil", value);
	    	              this.OnDescPerfilChanging(value);
	    	              this.RaiseDataMemberChanging("DescPerfil");
	    	              this._DescPerfil = value;
	    	              this.RaiseDataMemberChanged("DescPerfil");
	    	              this.OnDescPerfilChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdGpeconFranquia
	    partial void OnIdGpeconFranquiaChanging(System.Nullable<int> value);
	    partial void OnIdGpeconFranquiaChanged();

	    private System.Nullable<int> _IdGpeconFranquia;

	    [DataMember(Name = "IdGpeconFranquia", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Gpecon", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_PERFIL_REGRA_MODULO.TCS_PERFIL.TBC_GRUPO_ECONOMICO.ID_GPECON];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL.TBC_GRUPO_ECONOMICO.ID_GPECON")]
	    public System.Nullable<int> IdGpeconFranquia
	    {
	    	    get
	    	    {
	    	          return _IdGpeconFranquia;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdGpeconFranquia != value)
	    	          {
	    	              this.ValidateProperty("IdGpeconFranquia", value);
	    	              this.OnIdGpeconFranquiaChanging(value);
	    	              this.RaiseDataMemberChanging("IdGpeconFranquia");
	    	              this._IdGpeconFranquia = value;
	    	              this.RaiseDataMemberChanged("IdGpeconFranquia");
	    	              this.OnIdGpeconFranquiaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdPerfilOrigem
	    partial void OnIdPerfilOrigemChanging(System.Nullable<long> value);
	    partial void OnIdPerfilOrigemChanged();

	    private System.Nullable<long> _IdPerfilOrigem;

	    [DataMember(Name = "IdPerfilOrigem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Perfil Origem", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_PERFIL_REGRA_MODULO.TCS_PERFIL.ID_PERFIL_ORIGEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL.ID_PERFIL_ORIGEM")]
	    public System.Nullable<long> IdPerfilOrigem
	    {
	    	    get
	    	    {
	    	          return _IdPerfilOrigem;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdPerfilOrigem != value)
	    	          {
	    	              this.ValidateProperty("IdPerfilOrigem", value);
	    	              this.OnIdPerfilOrigemChanging(value);
	    	              this.RaiseDataMemberChanging("IdPerfilOrigem");
	    	              this._IdPerfilOrigem = value;
	    	              this.RaiseDataMemberChanged("IdPerfilOrigem");
	    	              this.OnIdPerfilOrigemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Inativo
	    partial void OnInativoChanging(bool value);
	    partial void OnInativoChanged();

	    private bool _Inativo;

	    [DataMember(IsRequired = true, Name = "Inativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inativo", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_PERFIL_REGRA_MODULO.TCS_PERFIL.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL.INATIVO")]
	    public bool Inativo
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

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_PERFIL_REGRA_MODULO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_PERFIL_REGRA_MODULO), QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL_REGRA_MODULO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PERFIL_REGRA_MODULO.ID_MODULO", Source = "IdModulo", Target = "ID_MODULO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL_REGRA_MODULO", RelationPropertyName = "TCS_PERFIL_REGRA_MODULO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PERFIL_REGRA_MODULO.TCS_PERFIL.ID_PERFIL", Source = "IdPerfil", Target = "ID_PERFIL", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL", RelationPropertyName = "TCS_PERFIL" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PERFIL_REGRA_MODULO.ID_PERFIL_REGRA_MODULO", Source = "IdPerfilRegraModulo", Target = "ID_PERFIL_REGRA_MODULO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL_REGRA_MODULO", RelationPropertyName = "TCS_PERFIL_REGRA_MODULO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO", Source = "LxRegraAcessoModulo", Target = "LX_REGRA_ACESSO_MODULO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL_REGRA_MODULO", RelationPropertyName = "TCS_PERFIL_REGRA_MODULO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxRegraAcessoModuloValues()
	    {
	    	    return Linx.Framework.Custom.BV.Domains.RegraAcesso.GetValues();
	    }
	    private string _lxRegraAcessoModuloName;
	    [DataMember(IsRequired = false, Name = "LxRegraAcessoModuloName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Regra Módulo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxRegraAcessoModuloName
	    {
	    	    get { if (this.LxRegraAcessoModulo.IsNull()) { _lxRegraAcessoModuloName = String.Empty; } else { string key = this.LxRegraAcessoModulo.ToString(); var dmValues = this.GetLxRegraAcessoModuloValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxRegraAcessoModuloName) _lxRegraAcessoModuloName = domainName; } return _lxRegraAcessoModuloName; } set { _lxRegraAcessoModuloName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Transação];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#.TCS_PERFIL_REGRA_TRANSACAO_LISTA as #Alias#];EdmEntityName[TCS_PERFIL_REGRA_TRANSACAO];EntityRelations[TCS_PERFIL(TCS_PERFIL)#TBC_GRUPO_ECONOMICO(TBC_GRUPO_ECONOMICO)#GPECON_SUPERIOR(TBC_GRUPO_ECONOMICO)];EdmParentEntityName[TCS_PERFIL];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsPerfilRegraTransacao")]
	[Serializable()]
	public partial class TcsPerfilRegraTransacaoParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For Acesso
	    partial void OnAcessoChanging(string value);
	    partial void OnAcessoChanged();

	    private string _Acesso;

	    [DataMember(IsRequired = true, Name = "Acesso", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Regra Acesso", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];LookUpName[LookupLxRegraAcessoTransacao];LookUpTitle[Seleção de (Regra Acesso)];LookUpQuery[executeLookupLxRegraAcessoTransacao];LookUpFinalize[finalizeLookupLxRegraAcessoTransacao];LookUpDisplayColumns[{\"LxRegraAcessoTransacao\" : \"\", \"LxRegraAcessoTransacaoName\" : \"Regra Acesso\"}];LookUpColumns[{\"LxRegraAcessoTransacao\" : false, \"LxRegraAcessoTransacaoName\" : true}];FilterDataKey[((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 1 ? 'Acesso Bloqueado' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 2 ? 'Acesso Total' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 13 ? 'Acesso por Transação' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 5 ? 'Alterar' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 12 ? 'Criar Pesquisa' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 10 ? 'Criar Relatório' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 6 ? 'Excluir' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 9 ? 'Exportar' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 8 ? 'Imprimir' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 4 ? 'Incluir' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 11 ? 'Layout' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 7 ? 'Pesquisa Especial' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 3 ? 'Pesquisar' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 99 ? 'Regra Transação' : ''))))))))))))))];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#LxRegraAcessoTransacaoName#false##60:0##Regra Acesso#1#true##::LookupLxRegraAcessoTransacao##true#false###Linx.Framework.Custom.BV.PerfilFranquia#IQueryable###true#false", EdmKey="((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) == 1 ? \"Acesso Bloqueado\" : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) == 2 ? \"Acesso Total\" : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) == 13 ? \"Acesso por Transação\" : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) == 5 ? \"Alterar\" : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) == 12 ? \"Criar Pesquisa\" : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) == 10 ? \"Criar Relatório\" : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) == 6 ? \"Excluir\" : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) == 9 ? \"Exportar\" : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) == 8 ? \"Imprimir\" : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) == 4 ? \"Incluir\" : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) == 11 ? \"Layout\" : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) == 7 ? \"Pesquisa Especial\" : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) == 3 ? \"Pesquisar\" : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) == 99 ? \"Regra Transação\" : \"\"))))))))))))))")]
	    public string Acesso
	    {
	    	    get
	    	    {
	    	          return _Acesso;
	    	    }
	    	    set
	    	    {
	    	          if (this._Acesso != value)
	    	          {
	    	              this.ValidateProperty("Acesso", value);
	    	              this.OnAcessoChanging(value);
	    	              this.RaiseDataMemberChanging("Acesso");
	    	              this._Acesso = value;
	    	              this.RaiseDataMemberChanged("Acesso");
	    	              this.OnAcessoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdPerfil
	    partial void OnIdPerfilChanging(long value);
	    partial void OnIdPerfilChanged();

	    private long _IdPerfil;

	    [DataMember(IsRequired = true, Name = "IdPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PERFIL_REGRA_TRANSACAO.TCS_PERFIL.ID_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL_REGRA_TRANSACAO.TCS_PERFIL.ID_PERFIL")]
	    public long IdPerfil
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
	    //Extensibility Partial Method Definitions For IdPerfilRegraTransacao
	    partial void OnIdPerfilRegraTransacaoChanging(long value);
	    partial void OnIdPerfilRegraTransacaoChanged();

	    private long _IdPerfilRegraTransacao;

	    [DataMember(IsRequired = true, Name = "IdPerfilRegraTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Perfil Regra Transacao", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PERFIL_REGRA_TRANSACAO.ID_PERFIL_REGRA_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL_REGRA_TRANSACAO.ID_PERFIL_REGRA_TRANSACAO")]
	    public long IdPerfilRegraTransacao
	    {
	    	    get
	    	    {
	    	          return _IdPerfilRegraTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdPerfilRegraTransacao != value)
	    	          {
	    	              this.ValidateProperty("IdPerfilRegraTransacao", value);
	    	              this.OnIdPerfilRegraTransacaoChanging(value);
	    	              this.RaiseDataMemberChanging("IdPerfilRegraTransacao");
	    	              this._IdPerfilRegraTransacao = value;
	    	              this.RaiseDataMemberChanged("IdPerfilRegraTransacao");
	    	              this.OnIdPerfilRegraTransacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTransacao
	    partial void OnIdTransacaoChanging(long value);
	    partial void OnIdTransacaoChanged();

	    private long _IdTransacao;

	    [DataMember(IsRequired = true, Name = "IdTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Transacao", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsPerfilRegraTransacao];LookUpTitle[Seleção de (Id Transacao)];LookUpQuery[executeLookUpTcsPerfilRegraTransacao];LookUpFinalize[finalizeLookUpTcsPerfilRegraTransacao];LookUpDisplayColumns[{\"IdTransacao\" : \"\", \"DescTransacao\" : \"Transação\", \"Origem\" : \"\"}];LookUpColumns[{\"IdTransacao\" : false, \"DescTransacao\" : true, \"Origem\" : true}];FilterDataKey[TCS_PERFIL_REGRA_TRANSACAO.ID_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdTransacao#true##12:0###0#false##::LookUpTcsPerfilRegraTransacao##true#false###Linx.Framework.Custom.BV.PerfilFranquia#IQueryable###true#false", EdmKey="TCS_PERFIL_REGRA_TRANSACAO.ID_TRANSACAO")]
	    public long IdTransacao
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
	    //Extensibility Partial Method Definitions For LxRegraAcessoTransacao
	    partial void OnLxRegraAcessoTransacaoChanging(byte value);
	    partial void OnLxRegraAcessoTransacaoChanged();

	    private byte _LxRegraAcessoTransacao;

	    [DataMember(IsRequired = true, Name = "LxRegraAcessoTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Regra Acesso Transação", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[RegraAcesso];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookupLxRegraAcessoTransacao];LookUpTitle[Seleção de (Regra Acesso Transação)];LookUpQuery[executeLookupLxRegraAcessoTransacao];LookUpFinalize[finalizeLookupLxRegraAcessoTransacao];LookUpDisplayColumns[{\"LxRegraAcessoTransacao\" : \"\", \"LxRegraAcessoTransacaoName\" : \"Regra Acesso\"}];LookUpColumns[{\"LxRegraAcessoTransacao\" : false, \"LxRegraAcessoTransacaoName\" : true}];FilterDataKey[TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="byte#LxRegraAcessoTransacao#true##60:0###0#false##::LookupLxRegraAcessoTransacao##true#false###Linx.Framework.Custom.BV.PerfilFranquia#IQueryable###true#false", EdmKey="TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO")]
	    public byte LxRegraAcessoTransacao
	    {
	    	    get
	    	    {
	    	          return _LxRegraAcessoTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxRegraAcessoTransacao != value)
	    	          {
	    	              this.ValidateProperty("LxRegraAcessoTransacao", value);
	    	              this.OnLxRegraAcessoTransacaoChanging(value);
	    	              this.RaiseDataMemberChanging("LxRegraAcessoTransacao");
	    	              this._LxRegraAcessoTransacao = value;
	    	              this.RaiseDataMemberChanged("LxRegraAcessoTransacao");
	    	              this.OnLxRegraAcessoTransacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescPerfil
	    partial void OnDescPerfilChanging(string value);
	    partial void OnDescPerfilChanged();

	    private string _DescPerfil;

	    [DataMember(IsRequired = true, Name = "DescPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_PERFIL_REGRA_TRANSACAO.TCS_PERFIL.DESC_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL.DESC_PERFIL")]
	    public string DescPerfil
	    {
	    	    get
	    	    {
	    	          return _DescPerfil;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescPerfil != value)
	    	          {
	    	              this.ValidateProperty("DescPerfil", value);
	    	              this.OnDescPerfilChanging(value);
	    	              this.RaiseDataMemberChanging("DescPerfil");
	    	              this._DescPerfil = value;
	    	              this.RaiseDataMemberChanged("DescPerfil");
	    	              this.OnDescPerfilChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdGpeconFranquia
	    partial void OnIdGpeconFranquiaChanging(System.Nullable<int> value);
	    partial void OnIdGpeconFranquiaChanged();

	    private System.Nullable<int> _IdGpeconFranquia;

	    [DataMember(Name = "IdGpeconFranquia", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Gpecon", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_PERFIL_REGRA_TRANSACAO.TCS_PERFIL.TBC_GRUPO_ECONOMICO.ID_GPECON];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL.TBC_GRUPO_ECONOMICO.ID_GPECON")]
	    public System.Nullable<int> IdGpeconFranquia
	    {
	    	    get
	    	    {
	    	          return _IdGpeconFranquia;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdGpeconFranquia != value)
	    	          {
	    	              this.ValidateProperty("IdGpeconFranquia", value);
	    	              this.OnIdGpeconFranquiaChanging(value);
	    	              this.RaiseDataMemberChanging("IdGpeconFranquia");
	    	              this._IdGpeconFranquia = value;
	    	              this.RaiseDataMemberChanged("IdGpeconFranquia");
	    	              this.OnIdGpeconFranquiaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdPerfilOrigem
	    partial void OnIdPerfilOrigemChanging(System.Nullable<long> value);
	    partial void OnIdPerfilOrigemChanged();

	    private System.Nullable<long> _IdPerfilOrigem;

	    [DataMember(Name = "IdPerfilOrigem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Perfil Origem", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_PERFIL_REGRA_TRANSACAO.TCS_PERFIL.ID_PERFIL_ORIGEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL.ID_PERFIL_ORIGEM")]
	    public System.Nullable<long> IdPerfilOrigem
	    {
	    	    get
	    	    {
	    	          return _IdPerfilOrigem;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdPerfilOrigem != value)
	    	          {
	    	              this.ValidateProperty("IdPerfilOrigem", value);
	    	              this.OnIdPerfilOrigemChanging(value);
	    	              this.RaiseDataMemberChanging("IdPerfilOrigem");
	    	              this._IdPerfilOrigem = value;
	    	              this.RaiseDataMemberChanged("IdPerfilOrigem");
	    	              this.OnIdPerfilOrigemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Inativo
	    partial void OnInativoChanging(bool value);
	    partial void OnInativoChanged();

	    private bool _Inativo;

	    [DataMember(IsRequired = true, Name = "Inativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inativo", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_PERFIL_REGRA_TRANSACAO.TCS_PERFIL.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL.INATIVO")]
	    public bool Inativo
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

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_PERFIL_REGRA_TRANSACAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_PERFIL_REGRA_TRANSACAO), QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL_REGRA_TRANSACAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PERFIL_REGRA_TRANSACAO.ID_TRANSACAO", Source = "IdTransacao", Target = "ID_TRANSACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL_REGRA_TRANSACAO", RelationPropertyName = "TCS_PERFIL_REGRA_TRANSACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PERFIL_REGRA_TRANSACAO.TCS_PERFIL.ID_PERFIL", Source = "IdPerfil", Target = "ID_PERFIL", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL", RelationPropertyName = "TCS_PERFIL" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PERFIL_REGRA_TRANSACAO.ID_PERFIL_REGRA_TRANSACAO", Source = "IdPerfilRegraTransacao", Target = "ID_PERFIL_REGRA_TRANSACAO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL_REGRA_TRANSACAO", RelationPropertyName = "TCS_PERFIL_REGRA_TRANSACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO", Source = "LxRegraAcessoTransacao", Target = "LX_REGRA_ACESSO_TRANSACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL_REGRA_TRANSACAO", RelationPropertyName = "TCS_PERFIL_REGRA_TRANSACAO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxRegraAcessoTransacaoValues()
	    {
	    	    return Linx.Framework.Custom.BV.Domains.RegraAcesso.GetValues();
	    }
	    private string _lxRegraAcessoTransacaoName;
	    [DataMember(IsRequired = false, Name = "LxRegraAcessoTransacaoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Regra Acesso Transação", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxRegraAcessoTransacaoName
	    {
	    	    get { if (this.LxRegraAcessoTransacao.IsNull()) { _lxRegraAcessoTransacaoName = String.Empty; } else { string key = this.LxRegraAcessoTransacao.ToString(); var dmValues = this.GetLxRegraAcessoTransacaoValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxRegraAcessoTransacaoName) _lxRegraAcessoTransacaoName = domainName; } return _lxRegraAcessoTransacaoName; } set { _lxRegraAcessoTransacaoName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Bandeira / Rede];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#.TCS_PERFIL_BANDEIRA_REDE_LISTA as #Alias#];EdmEntityName[TCS_PERFIL_BANDEIRA_REDE];EntityRelations[TCS_PERFIL(TCS_PERFIL)#TBC_GRUPO_ECONOMICO(TBC_GRUPO_ECONOMICO)#GPECON_SUPERIOR(TBC_GRUPO_ECONOMICO)#TBC_BANDEIRA_REDE(TBC_BANDEIRA_REDE)];EdmParentEntityName[TCS_PERFIL];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsPerfilBandeiraRede")]
	[Serializable()]
	public partial class TcsPerfilBandeiraRedeParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For DescBandeiraRede
	    partial void OnDescBandeiraRedeChanging(string value);
	    partial void OnDescBandeiraRedeChanged();

	    private string _DescBandeiraRede;

	    [DataMember(IsRequired = true, Name = "DescBandeiraRede", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bandeira / Rede", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(120)]
	    [FunctionalPoint("Precision[120:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTbcBandeiraRede];LookUpTitle[Seleção de (Bandeira / Rede)];LookUpQuery[executeLookUpTbcBandeiraRede];LookUpFinalize[finalizeLookUpTbcBandeiraRede];LookUpDisplayColumns[{\"IdBandeiraR\" : \"Id Bandeira / Rede\", \"DescBandeiraRede\" : \"Bandeira / Rede\"}];LookUpColumns[{\"IdBandeiraR\" : true, \"DescBandeiraRede\" : true}];FilterDataKey[TCS_PERFIL_BANDEIRA_REDE.TBC_BANDEIRA_REDE.DESC_BANDEIRA_REDE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#DescBandeiraRede#false##60:0##Bandeira / Rede#1#true##::LookUpTbcBandeiraRede##true#false#TBC_BANDEIRA_REDE#TBC_BANDEIRA_REDE#Linx.Framework.Custom.BV.PerfilFranquia#IQueryable###true#true", EdmKey="TCS_PERFIL_BANDEIRA_REDE.TBC_BANDEIRA_REDE.DESC_BANDEIRA_REDE")]
	    public string DescBandeiraRede
	    {
	    	    get
	    	    {
	    	          return _DescBandeiraRede;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescBandeiraRede != value)
	    	          {
	    	              this.ValidateProperty("DescBandeiraRede", value);
	    	              this.OnDescBandeiraRedeChanging(value);
	    	              this.RaiseDataMemberChanging("DescBandeiraRede");
	    	              this._DescBandeiraRede = value;
	    	              this.RaiseDataMemberChanged("DescBandeiraRede");
	    	              this.OnDescBandeiraRedeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdBandeiraR
	    partial void OnIdBandeiraRChanging(int value);
	    partial void OnIdBandeiraRChanged();

	    private int _IdBandeiraR;

	    [DataMember(IsRequired = true, Name = "IdBandeiraR", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Bandeira Rede", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTbcBandeiraRede];LookUpTitle[Seleção de (Id Bandeira Rede)];LookUpQuery[executeLookUpTbcBandeiraRede];LookUpFinalize[finalizeLookUpTbcBandeiraRede];LookUpDisplayColumns[{\"IdBandeiraR\" : \"Id Bandeira / Rede\", \"DescBandeiraRede\" : \"Bandeira / Rede\"}];LookUpColumns[{\"IdBandeiraR\" : true, \"DescBandeiraRede\" : true}];FilterDataKey[TCS_PERFIL_BANDEIRA_REDE.TBC_BANDEIRA_REDE.ID_BANDEIRA_REDE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="int#IdBandeiraR#true##10:0##Id Bandeira / Rede#0#true##::LookUpTbcBandeiraRede##true#false#TBC_BANDEIRA_REDE#TBC_BANDEIRA_REDE#Linx.Framework.Custom.BV.PerfilFranquia#IQueryable###true#true", EdmKey="TCS_PERFIL_BANDEIRA_REDE.TBC_BANDEIRA_REDE.ID_BANDEIRA_REDE")]
	    public int IdBandeiraR
	    {
	    	    get
	    	    {
	    	          return _IdBandeiraR;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdBandeiraR != value)
	    	          {
	    	              this.ValidateProperty("IdBandeiraR", value);
	    	              this.OnIdBandeiraRChanging(value);
	    	              this.RaiseDataMemberChanging("IdBandeiraR");
	    	              this._IdBandeiraR = value;
	    	              this.RaiseDataMemberChanged("IdBandeiraR");
	    	              this.OnIdBandeiraRChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdPerfil
	    partial void OnIdPerfilChanging(long value);
	    partial void OnIdPerfilChanged();

	    private long _IdPerfil;

	    [DataMember(IsRequired = true, Name = "IdPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PERFIL_BANDEIRA_REDE.TCS_PERFIL.ID_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL_BANDEIRA_REDE.TCS_PERFIL.ID_PERFIL")]
	    public long IdPerfil
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
	    //Extensibility Partial Method Definitions For DescPerfil
	    partial void OnDescPerfilChanging(string value);
	    partial void OnDescPerfilChanged();

	    private string _DescPerfil;

	    [DataMember(IsRequired = true, Name = "DescPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_PERFIL_BANDEIRA_REDE.TCS_PERFIL.DESC_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL.DESC_PERFIL")]
	    public string DescPerfil
	    {
	    	    get
	    	    {
	    	          return _DescPerfil;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescPerfil != value)
	    	          {
	    	              this.ValidateProperty("DescPerfil", value);
	    	              this.OnDescPerfilChanging(value);
	    	              this.RaiseDataMemberChanging("DescPerfil");
	    	              this._DescPerfil = value;
	    	              this.RaiseDataMemberChanged("DescPerfil");
	    	              this.OnDescPerfilChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdGpeconFranquia
	    partial void OnIdGpeconFranquiaChanging(System.Nullable<int> value);
	    partial void OnIdGpeconFranquiaChanged();

	    private System.Nullable<int> _IdGpeconFranquia;

	    [DataMember(Name = "IdGpeconFranquia", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Gpecon", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_PERFIL_BANDEIRA_REDE.TCS_PERFIL.TBC_GRUPO_ECONOMICO.ID_GPECON];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL.TBC_GRUPO_ECONOMICO.ID_GPECON")]
	    public System.Nullable<int> IdGpeconFranquia
	    {
	    	    get
	    	    {
	    	          return _IdGpeconFranquia;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdGpeconFranquia != value)
	    	          {
	    	              this.ValidateProperty("IdGpeconFranquia", value);
	    	              this.OnIdGpeconFranquiaChanging(value);
	    	              this.RaiseDataMemberChanging("IdGpeconFranquia");
	    	              this._IdGpeconFranquia = value;
	    	              this.RaiseDataMemberChanged("IdGpeconFranquia");
	    	              this.OnIdGpeconFranquiaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdPerfilOrigem
	    partial void OnIdPerfilOrigemChanging(System.Nullable<long> value);
	    partial void OnIdPerfilOrigemChanged();

	    private System.Nullable<long> _IdPerfilOrigem;

	    [DataMember(Name = "IdPerfilOrigem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Perfil Origem", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_PERFIL_BANDEIRA_REDE.TCS_PERFIL.ID_PERFIL_ORIGEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL.ID_PERFIL_ORIGEM")]
	    public System.Nullable<long> IdPerfilOrigem
	    {
	    	    get
	    	    {
	    	          return _IdPerfilOrigem;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdPerfilOrigem != value)
	    	          {
	    	              this.ValidateProperty("IdPerfilOrigem", value);
	    	              this.OnIdPerfilOrigemChanging(value);
	    	              this.RaiseDataMemberChanging("IdPerfilOrigem");
	    	              this._IdPerfilOrigem = value;
	    	              this.RaiseDataMemberChanged("IdPerfilOrigem");
	    	              this.OnIdPerfilOrigemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Inativo
	    partial void OnInativoChanging(bool value);
	    partial void OnInativoChanged();

	    private bool _Inativo;

	    [DataMember(IsRequired = true, Name = "Inativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inativo", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_PERFIL_BANDEIRA_REDE.TCS_PERFIL.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL.INATIVO")]
	    public bool Inativo
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

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_PERFIL_BANDEIRA_REDE").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_PERFIL_BANDEIRA_REDE), QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL_BANDEIRA_REDE" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PERFIL_BANDEIRA_REDE.TCS_PERFIL.ID_PERFIL", Source = "IdPerfil", Target = "ID_PERFIL", TargetKeyName = "ID_PERFIL", NoUpdatable = false, IsKey = true, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL", RelationPropertyName = "TCS_PERFIL" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PERFIL_BANDEIRA_REDE.TBC_BANDEIRA_REDE.ID_BANDEIRA_REDE", Source = "IdBandeiraR", Target = "ID_BANDEIRA_REDE", TargetKeyName = "ID_BANDEIRA_REDE", NoUpdatable = false, IsKey = true, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TBC_BANDEIRA_REDE", RelationPropertyName = "TBC_BANDEIRA_REDE" });

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

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Filial];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#.TCS_PERFIL_FILIAL_LISTA as #Alias#];EdmEntityName[TCS_PERFIL_FILIAL];EntityRelations[TCS_PERFIL(TCS_PERFIL)#TBC_GRUPO_ECONOMICO(TBC_GRUPO_ECONOMICO)#GPECON_SUPERIOR(TBC_GRUPO_ECONOMICO)#TBC_FILIAL(TBC_FILIAL)#MATRIZ_CONTABIL(TBC_FILIAL)#TBC_PFJ(TBC_PFJ)#TBC_FILIAL_LISTA(TBC_FILIAL)];EdmParentEntityName[TCS_PERFIL];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsPerfilFilial")]
	[Serializable()]
	public partial class TcsPerfilFilialParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For CodigoFilial
	    partial void OnCodigoFilialChanging(string value);
	    partial void OnCodigoFilialChanged();

	    private string _CodigoFilial;

	    [DataMember(Name = "CodigoFilial", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Código Filial", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(18)]
	    [FunctionalPoint("Precision[18:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTbcFilial];LookUpTitle[Seleção de (Código Filial)];LookUpQuery[executeLookUpTbcFilial];LookUpFinalize[finalizeLookUpTbcFilial];LookUpDisplayColumns[{\"CodigoFilial\" : \"Código Filial\", \"IdFilialPfj\" : \"Id Filial Pfj\", \"NomeFilial\" : \"Nome Fantasia\"}];LookUpColumns[{\"CodigoFilial\" : true, \"IdFilialPfj\" : false, \"NomeFilial\" : true}];FilterDataKey[TCS_PERFIL_FILIAL.TBC_FILIAL.CODIGO_FILIAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#CodigoFilial#false##18:0##Código Filial#0#true##::LookUpTbcFilial##true#false#TBC_FILIAL#TBC_FILIAL#Linx.Framework.Custom.BV.PerfilFranquia#IQueryable###true#true", EdmKey="TCS_PERFIL_FILIAL.TBC_FILIAL.CODIGO_FILIAL")]
	    public string CodigoFilial
	    {
	    	    get
	    	    {
	    	          return _CodigoFilial;
	    	    }
	    	    set
	    	    {
	    	          if (this._CodigoFilial != value)
	    	          {
	    	              this.ValidateProperty("CodigoFilial", value);
	    	              this.OnCodigoFilialChanging(value);
	    	              this.RaiseDataMemberChanging("CodigoFilial");
	    	              this._CodigoFilial = value;
	    	              this.RaiseDataMemberChanged("CodigoFilial");
	    	              this.OnCodigoFilialChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdFilialPfj
	    partial void OnIdFilialPfjChanging(int value);
	    partial void OnIdFilialPfjChanged();

	    private int _IdFilialPfj;

	    [DataMember(IsRequired = true, Name = "IdFilialPfj", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Filial Pfj", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTbcFilial];LookUpTitle[Seleção de (Id Filial Pfj)];LookUpQuery[executeLookUpTbcFilial];LookUpFinalize[finalizeLookUpTbcFilial];LookUpDisplayColumns[{\"CodigoFilial\" : \"Código Filial\", \"IdFilialPfj\" : \"Id Filial Pfj\", \"NomeFilial\" : \"Nome Fantasia\"}];LookUpColumns[{\"CodigoFilial\" : true, \"IdFilialPfj\" : false, \"NomeFilial\" : true}];FilterDataKey[TCS_PERFIL_FILIAL.TBC_FILIAL.ID_FILIAL_PFJ];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="int#IdFilialPfj#true##10:0##Id Filial Pfj#1#false##::LookUpTbcFilial##true#false#TBC_FILIAL#TBC_FILIAL#Linx.Framework.Custom.BV.PerfilFranquia#IQueryable###true#true", EdmKey="TCS_PERFIL_FILIAL.TBC_FILIAL.ID_FILIAL_PFJ")]
	    public int IdFilialPfj
	    {
	    	    get
	    	    {
	    	          return _IdFilialPfj;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdFilialPfj != value)
	    	          {
	    	              this.ValidateProperty("IdFilialPfj", value);
	    	              this.OnIdFilialPfjChanging(value);
	    	              this.RaiseDataMemberChanging("IdFilialPfj");
	    	              this._IdFilialPfj = value;
	    	              this.RaiseDataMemberChanged("IdFilialPfj");
	    	              this.OnIdFilialPfjChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdPerfil
	    partial void OnIdPerfilChanging(long value);
	    partial void OnIdPerfilChanged();

	    private long _IdPerfil;

	    [DataMember(IsRequired = true, Name = "IdPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PERFIL_FILIAL.TCS_PERFIL.ID_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL_FILIAL.TCS_PERFIL.ID_PERFIL")]
	    public long IdPerfil
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
	    //Extensibility Partial Method Definitions For IdTcsPerfilFilial
	    partial void OnIdTcsPerfilFilialChanging(long value);
	    partial void OnIdTcsPerfilFilialChanged();

	    private long _IdTcsPerfilFilial;

	    [DataMember(IsRequired = true, Name = "IdTcsPerfilFilial", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Perfil Filial", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PERFIL_FILIAL.ID_TCS_PERFIL_FILIAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL_FILIAL.ID_TCS_PERFIL_FILIAL")]
	    public long IdTcsPerfilFilial
	    {
	    	    get
	    	    {
	    	          return _IdTcsPerfilFilial;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsPerfilFilial != value)
	    	          {
	    	              this.ValidateProperty("IdTcsPerfilFilial", value);
	    	              this.OnIdTcsPerfilFilialChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsPerfilFilial");
	    	              this._IdTcsPerfilFilial = value;
	    	              this.RaiseDataMemberChanged("IdTcsPerfilFilial");
	    	              this.OnIdTcsPerfilFilialChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeFilial
	    partial void OnNomeFilialChanging(string value);
	    partial void OnNomeFilialChanged();

	    private string _NomeFilial;

	    [DataMember(Name = "NomeFilial", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Fantasia", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTbcFilial];LookUpTitle[Seleção de (Nome Fantasia)];LookUpQuery[executeLookUpTbcFilial];LookUpFinalize[finalizeLookUpTbcFilial];LookUpDisplayColumns[{\"CodigoFilial\" : \"Código Filial\", \"IdFilialPfj\" : \"Id Filial Pfj\", \"NomeFilial\" : \"Nome Fantasia\"}];LookUpColumns[{\"CodigoFilial\" : true, \"IdFilialPfj\" : false, \"NomeFilial\" : true}];FilterDataKey[TCS_PERFIL_FILIAL.TBC_FILIAL.NOME_FILIAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#NomeFilial#false##60:0##Nome Fantasia#2#true##::LookUpTbcFilial##true#false#TBC_FILIAL#TBC_FILIAL#Linx.Framework.Custom.BV.PerfilFranquia#IQueryable###true#true", EdmKey="TCS_PERFIL_FILIAL.TBC_FILIAL.NOME_FILIAL")]
	    public string NomeFilial
	    {
	    	    get
	    	    {
	    	          return _NomeFilial;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeFilial != value)
	    	          {
	    	              this.ValidateProperty("NomeFilial", value);
	    	              this.OnNomeFilialChanging(value);
	    	              this.RaiseDataMemberChanging("NomeFilial");
	    	              this._NomeFilial = value;
	    	              this.RaiseDataMemberChanged("NomeFilial");
	    	              this.OnNomeFilialChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescPerfil
	    partial void OnDescPerfilChanging(string value);
	    partial void OnDescPerfilChanged();

	    private string _DescPerfil;

	    [DataMember(IsRequired = true, Name = "DescPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_PERFIL_FILIAL.TCS_PERFIL.DESC_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL.DESC_PERFIL")]
	    public string DescPerfil
	    {
	    	    get
	    	    {
	    	          return _DescPerfil;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescPerfil != value)
	    	          {
	    	              this.ValidateProperty("DescPerfil", value);
	    	              this.OnDescPerfilChanging(value);
	    	              this.RaiseDataMemberChanging("DescPerfil");
	    	              this._DescPerfil = value;
	    	              this.RaiseDataMemberChanged("DescPerfil");
	    	              this.OnDescPerfilChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdGpeconFranquia
	    partial void OnIdGpeconFranquiaChanging(System.Nullable<int> value);
	    partial void OnIdGpeconFranquiaChanged();

	    private System.Nullable<int> _IdGpeconFranquia;

	    [DataMember(Name = "IdGpeconFranquia", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Gpecon", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_PERFIL_FILIAL.TCS_PERFIL.TBC_GRUPO_ECONOMICO.ID_GPECON];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL.TBC_GRUPO_ECONOMICO.ID_GPECON")]
	    public System.Nullable<int> IdGpeconFranquia
	    {
	    	    get
	    	    {
	    	          return _IdGpeconFranquia;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdGpeconFranquia != value)
	    	          {
	    	              this.ValidateProperty("IdGpeconFranquia", value);
	    	              this.OnIdGpeconFranquiaChanging(value);
	    	              this.RaiseDataMemberChanging("IdGpeconFranquia");
	    	              this._IdGpeconFranquia = value;
	    	              this.RaiseDataMemberChanged("IdGpeconFranquia");
	    	              this.OnIdGpeconFranquiaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdPerfilOrigem
	    partial void OnIdPerfilOrigemChanging(System.Nullable<long> value);
	    partial void OnIdPerfilOrigemChanged();

	    private System.Nullable<long> _IdPerfilOrigem;

	    [DataMember(Name = "IdPerfilOrigem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Perfil Origem", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_PERFIL_FILIAL.TCS_PERFIL.ID_PERFIL_ORIGEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL.ID_PERFIL_ORIGEM")]
	    public System.Nullable<long> IdPerfilOrigem
	    {
	    	    get
	    	    {
	    	          return _IdPerfilOrigem;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdPerfilOrigem != value)
	    	          {
	    	              this.ValidateProperty("IdPerfilOrigem", value);
	    	              this.OnIdPerfilOrigemChanging(value);
	    	              this.RaiseDataMemberChanging("IdPerfilOrigem");
	    	              this._IdPerfilOrigem = value;
	    	              this.RaiseDataMemberChanged("IdPerfilOrigem");
	    	              this.OnIdPerfilOrigemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Inativo
	    partial void OnInativoChanging(bool value);
	    partial void OnInativoChanged();

	    private bool _Inativo;

	    [DataMember(IsRequired = true, Name = "Inativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inativo", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_PERFIL_FILIAL.TCS_PERFIL.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL.INATIVO")]
	    public bool Inativo
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

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_PERFIL_FILIAL").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_PERFIL_FILIAL), QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL_FILIAL" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PERFIL_FILIAL.TCS_PERFIL.ID_PERFIL", Source = "IdPerfil", Target = "ID_PERFIL", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL", RelationPropertyName = "TCS_PERFIL" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PERFIL_FILIAL.ID_TCS_PERFIL_FILIAL", Source = "IdTcsPerfilFilial", Target = "ID_TCS_PERFIL_FILIAL", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL_FILIAL", RelationPropertyName = "TCS_PERFIL_FILIAL" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PERFIL_FILIAL.TBC_FILIAL.ID_FILIAL_PFJ", Source = "IdFilialPfj", Target = "ID_FILIAL_PFJ", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TBC_FILIAL", RelationPropertyName = "TBC_FILIAL" });

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
	[DomainIdentifier("ProcessorOverviewPerfilFranquiaDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class PerfilFranquiaDomainService : DomainService, IDataServiceContext 
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

		
	    public PerfilFranquiaDomainService() : this("", null, null) { }
	    public PerfilFranquiaDomainService(string connectionString) : this(connectionString, null, null) { }
	    public PerfilFranquiaDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public PerfilFranquiaDomainService(Linx.Framework.ControleSistema.BM.ControleSistemaContext dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public PerfilFranquiaDomainService(string connectionString, Linx.Framework.ControleSistema.BM.ControleSistemaContext dataContext, Dictionary<string, string> headers) : base() 
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
	
	    
	        foreach (ChangeSetEntry entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsPerfil))
	        {
	            ((TcsPerfil)entry.Entity).OnSavingChanges(this, changeSet.GetChangeOperation(entry.Entity));
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
	
	
	        TcsUsuarioPerfil.OnSavedContextChanges(this, changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioPerfil).ToArray());
    
	        TcsPerfilRegraModulo.OnSavedContextChanges(this, changeSet.ChangeSetEntries.Where(e => e.Entity is TcsPerfilRegraModulo).ToArray());
    
	        TcsPerfilRegraTransacao.OnSavedContextChanges(this, changeSet.ChangeSetEntries.Where(e => e.Entity is TcsPerfilRegraTransacao).ToArray());
    
	        TcsPerfilBandeiraRede.OnSavedContextChanges(this, changeSet.ChangeSetEntries.Where(e => e.Entity is TcsPerfilBandeiraRede).ToArray());
    
	        TcsPerfilFilial.OnSavedContextChanges(this, changeSet.ChangeSetEntries.Where(e => e.Entity is TcsPerfilFilial).ToArray());
    	
	    }
		
	    private void OnTransactingChanges(ChangeSet changeSet)
	    {
	
		
	    }
	
	    private void OnTransactedChanges(ChangeSet changeSet)
	    {
	
	    
	        foreach (ChangeSetEntry entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsPerfil))
	        {
	            ((TcsPerfil)entry.Entity).OnTransactedChanges(this, changeSet.GetChangeOperation(entry.Entity));
	        }
    	
	    }
		
	    #endregion Entity Event Call Definitions
	
	    #region Transaction Control.
	
	    TransactionScope transactionScope = null;	
	
	    //Adjust Hierarchy Composition
	    private ChangeSet AdjustHierarchyForSaving(ChangeSet changeSet)
	    {

		
 
 	        bool createNewChangeSet = false;
 
 	        //Adjust data hierarchy
 	        var _TcsPerfilElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsPerfil && e.Entity.GetType().Name == "TcsPerfil" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _TcsPerfilElements)
 	           if (((TcsPerfil)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 
 	        //Remove inconsistent details
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioPerfil && e.Entity.GetType().Name == "TcsUsuarioPerfil" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsPerfilRegraModulo && e.Entity.GetType().Name == "TcsPerfilRegraModulo" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsPerfilRegraTransacao && e.Entity.GetType().Name == "TcsPerfilRegraTransacao" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsPerfilBandeiraRede && e.Entity.GetType().Name == "TcsPerfilBandeiraRede" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsPerfilFilial && e.Entity.GetType().Name == "TcsPerfilFilial" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
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
	            
                IdUsuario = entity.ID_USUARIO
                , NomeUsuario = entity.NOME_USUARIO
                , IdLinx = entity.ID_LINX
                , UidUsuario = entity.UID_USUARIO
	            });

	            
	
		
			
		
	        TcsUsuarioPerfil.OnLookingUpLookUpTcsUsuario(ref query, propertyName, entitySearch);
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpTcsPerfilRegraModulo.
	    public IQueryable<LookUpTcsPerfilRegraModulo> GetAllLookUpTcsPerfilRegraModulo()
	    {
	        return this.GetLookUpTcsPerfilRegraModulo(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsPerfilRegraModulo By EntitySearch.
	    public IQueryable<LookUpTcsPerfilRegraModulo> GetLookUpTcsPerfilRegraModuloByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsPerfilRegraModulo(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsPerfilRegraModulo.
	    public IQueryable<LookUpTcsPerfilRegraModulo> GetLookUpTcsPerfilRegraModulo(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsPerfilRegraModulo";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsPerfilRegraModulo));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsPerfilRegraModulo> query =  null;
		
			
		
	        TcsPerfilRegraModulo.OnLookingUpLookUpTcsPerfilRegraModulo(ref query, propertyName, entitySearch);
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpLxRegraAcessoModulo.
	    public IQueryable<LookUpLxRegraAcessoModulo> GetAllLookUpLxRegraAcessoModulo()
	    {
	        return this.GetLookUpLxRegraAcessoModulo(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpLxRegraAcessoModulo By EntitySearch.
	    public IQueryable<LookUpLxRegraAcessoModulo> GetLookUpLxRegraAcessoModuloByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpLxRegraAcessoModulo(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpLxRegraAcessoModulo.
	    public IQueryable<LookUpLxRegraAcessoModulo> GetLookUpLxRegraAcessoModulo(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpLxRegraAcessoModulo";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpLxRegraAcessoModulo));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpLxRegraAcessoModulo> query =  null;
		
			
		
	        TcsPerfilRegraModulo.OnLookingUpLookUpLxRegraAcessoModulo(ref query, propertyName, entitySearch);
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpTcsPerfilRegraTransacao.
	    public IQueryable<LookUpTcsPerfilRegraTransacao> GetAllLookUpTcsPerfilRegraTransacao()
	    {
	        return this.GetLookUpTcsPerfilRegraTransacao(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsPerfilRegraTransacao By EntitySearch.
	    public IQueryable<LookUpTcsPerfilRegraTransacao> GetLookUpTcsPerfilRegraTransacaoByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsPerfilRegraTransacao(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsPerfilRegraTransacao.
	    public IQueryable<LookUpTcsPerfilRegraTransacao> GetLookUpTcsPerfilRegraTransacao(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsPerfilRegraTransacao";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsPerfilRegraTransacao));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsPerfilRegraTransacao> query =  null;
		
			
		
	        TcsPerfilRegraTransacao.OnLookingUpLookUpTcsPerfilRegraTransacao(ref query, propertyName, entitySearch);
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookupLxRegraAcessoTransacao.
	    public IQueryable<LookupLxRegraAcessoTransacao> GetAllLookupLxRegraAcessoTransacao()
	    {
	        return this.GetLookupLxRegraAcessoTransacao(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookupLxRegraAcessoTransacao By EntitySearch.
	    public IQueryable<LookupLxRegraAcessoTransacao> GetLookupLxRegraAcessoTransacaoByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookupLxRegraAcessoTransacao(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookupLxRegraAcessoTransacao.
	    public IQueryable<LookupLxRegraAcessoTransacao> GetLookupLxRegraAcessoTransacao(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookupLxRegraAcessoTransacao";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookupLxRegraAcessoTransacao));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookupLxRegraAcessoTransacao> query =  null;
		
			
		
	        TcsPerfilRegraTransacao.OnLookingUpLookupLxRegraAcessoTransacao(ref query, propertyName, entitySearch);
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpTbcBandeiraRede.
	    public IQueryable<LookUpTbcBandeiraRede> GetAllLookUpTbcBandeiraRede()
	    {
	        return this.GetLookUpTbcBandeiraRede(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTbcBandeiraRede By EntitySearch.
	    public IQueryable<LookUpTbcBandeiraRede> GetLookUpTbcBandeiraRedeByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTbcBandeiraRede(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTbcBandeiraRede.
	    public IQueryable<LookUpTbcBandeiraRede> GetLookUpTbcBandeiraRede(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TBC_BANDEIRA_REDE" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTbcBandeiraRede";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTbcBandeiraRede));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTbcBandeiraRede> query =  
	
	            (from entity in this.DbContext.TBC_BANDEIRA_REDE.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpTbcBandeiraRede()		
	            {
	            
                IdBandeiraR = entity.ID_BANDEIRA_REDE
                , DescBandeiraRede = entity.DESC_BANDEIRA_REDE
	            });

	            
	
		
			
		
	        TcsPerfilBandeiraRede.OnLookingUpLookUpTbcBandeiraRede(ref query, propertyName, entitySearch);
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpTbcFilial.
	    public IQueryable<LookUpTbcFilial> GetAllLookUpTbcFilial()
	    {
	        return this.GetLookUpTbcFilial(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTbcFilial By EntitySearch.
	    public IQueryable<LookUpTbcFilial> GetLookUpTbcFilialByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTbcFilial(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTbcFilial.
	    public IQueryable<LookUpTbcFilial> GetLookUpTbcFilial(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TBC_FILIAL" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTbcFilial";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTbcFilial));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTbcFilial> query =  
	
	            (from entity in this.DbContext.TBC_FILIAL.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpTbcFilial()		
	            {
	            
                CodigoFilial = entity.CODIGO_FILIAL
                , IdFilialPfj = entity.ID_FILIAL_PFJ
                , NomeFilial = entity.NOME_FILIAL
	            });

	            
	
		
			
		
	        TcsPerfilFilial.OnLookingUpLookUpTbcFilial(ref query, propertyName, entitySearch);
	
	
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
	
		

	        if (entityName.InList("Linx.Framework.Custom.BV.PerfilFranquia.TcsPerfil"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsPerfil",
	        			NameSpace = "Linx.Framework.Custom.BV.PerfilFranquia",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsPerfil",
	        			ClearMethodName = "ClearTcsPerfil",
	        			QueryMethodName  = "GetPagedTcsPerfil",	
	        			CountingMethodName  = "GetTcsPerfil" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.Custom.BV.PerfilFranquia.TcsPerfil"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.Custom.BV.PerfilFranquia.TcsPerfil"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.Custom.BV.PerfilFranquia.TcsPerfil", "Linx.Framework.Custom.BV.PerfilFranquia.TcsUsuarioPerfil"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsUsuarioPerfil" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.Custom.BV.PerfilFranquia",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsPerfil",	
	        			DisplayName = "Usuários Vinculados",
	        			ClearMethodName = "ClearTcsUsuarioPerfil" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsUsuarioPerfil" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsUsuarioPerfil" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.Custom.BV.PerfilFranquia.TcsUsuarioPerfil"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.Custom.BV.PerfilFranquia.TcsUsuarioPerfil" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.Custom.BV.PerfilFranquia.TcsPerfil", "Linx.Framework.Custom.BV.PerfilFranquia.TcsPerfilRegraModulo"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsPerfilRegraModulo" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.Custom.BV.PerfilFranquia",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsPerfil",	
	        			DisplayName = "Módulo",
	        			ClearMethodName = "ClearTcsPerfilRegraModulo" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsPerfilRegraModulo" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsPerfilRegraModulo" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.Custom.BV.PerfilFranquia.TcsPerfilRegraModulo"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.Custom.BV.PerfilFranquia.TcsPerfilRegraModulo" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.Custom.BV.PerfilFranquia.TcsPerfil", "Linx.Framework.Custom.BV.PerfilFranquia.TcsPerfilRegraTransacao"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsPerfilRegraTransacao" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.Custom.BV.PerfilFranquia",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsPerfil",	
	        			DisplayName = "Transação",
	        			ClearMethodName = "ClearTcsPerfilRegraTransacao" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsPerfilRegraTransacao" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsPerfilRegraTransacao" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.Custom.BV.PerfilFranquia.TcsPerfilRegraTransacao"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.Custom.BV.PerfilFranquia.TcsPerfilRegraTransacao" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.Custom.BV.PerfilFranquia.TcsPerfil", "Linx.Framework.Custom.BV.PerfilFranquia.TcsPerfilBandeiraRede"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsPerfilBandeiraRede" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.Custom.BV.PerfilFranquia",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsPerfil",	
	        			DisplayName = "Bandeira / Rede",
	        			ClearMethodName = "ClearTcsPerfilBandeiraRede" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsPerfilBandeiraRede" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsPerfilBandeiraRede" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.Custom.BV.PerfilFranquia.TcsPerfilBandeiraRede"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.Custom.BV.PerfilFranquia.TcsPerfilBandeiraRede" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.Custom.BV.PerfilFranquia.TcsPerfil", "Linx.Framework.Custom.BV.PerfilFranquia.TcsPerfilFilial"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsPerfilFilial" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.Custom.BV.PerfilFranquia",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsPerfil",	
	        			DisplayName = "Filial",
	        			ClearMethodName = "ClearTcsPerfilFilial" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsPerfilFilial" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsPerfilFilial" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.Custom.BV.PerfilFranquia.TcsPerfilFilial"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.Custom.BV.PerfilFranquia.TcsPerfilFilial" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.Custom.BV.PerfilFranquia.TbcFilial"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TbcFilial",
	        			NameSpace = "Linx.Framework.Custom.BV.PerfilFranquia",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TbcFilial",
	        			ClearMethodName = "ClearTbcFilial",
	        			QueryMethodName  = "GetPagedTbcFilial",	
	        			CountingMethodName  = "GetTbcFilial" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.Custom.BV.PerfilFranquia.TbcFilial"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.Custom.BV.PerfilFranquia.TbcFilial"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.Custom.BV.PerfilFranquia.SyncInfo"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "SyncInfo",
	        			NameSpace = "Linx.Framework.Custom.BV.PerfilFranquia",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "SyncInfo",
	        			ClearMethodName = "ClearSyncInfo",
	        			QueryMethodName  = "GetPagedSyncInfo",	
	        			CountingMethodName  = "GetSyncInfo" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.Custom.BV.PerfilFranquia.SyncInfo"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.Custom.BV.PerfilFranquia.SyncInfo"), forceAll: forceAll)
	        		});
	        }
	
            return result;
        }
	
	    [Ignore]
	    public string[] GetClientDomains(bool erp)
        {	
	    		if (erp)
	    		{

         		    return new string[] { "FrameworkCustom_ClientErpDataDomainsFactory", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.Custom.BV.ClientResources.ClientErpDataDomainsFactory.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
	    		}
	    		else 
	    		{

         		    return new string[] { "FrameworkCustom_MobileDataDomains", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.Custom.BV.ClientResources.MobileDataDomains.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
	    		}

        }

	    [Ignore]
	    public string[] GetClientService(bool erp)
        {	

	    		if (erp)
	    		{

         		    return new string[] { "FrameworkCustom_PerfilFranquiaClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.Custom.BV.ClientResources.PerfilFranquiaClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "FrameworkCustom_perfilFranquiaService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.Custom.BV.ClientResources.perfilFranquiaService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear TcsPerfil.
	    public IEnumerable<TcsPerfil> ClearTcsPerfil()
	    {
	        List<TcsPerfil> result = new List<TcsPerfil>();
	        result.Add(new TcsPerfil());	
			
	        result[0].TcsUsuarioPerfilList = new List<TcsUsuarioPerfil>();
	        ((List<TcsUsuarioPerfil>)result[0].TcsUsuarioPerfilList).Add(new TcsUsuarioPerfil());
			
	        result[0].TcsPerfilRegraModuloList = new List<TcsPerfilRegraModulo>();
	        ((List<TcsPerfilRegraModulo>)result[0].TcsPerfilRegraModuloList).Add(new TcsPerfilRegraModulo());
			
	        result[0].TcsPerfilRegraTransacaoList = new List<TcsPerfilRegraTransacao>();
	        ((List<TcsPerfilRegraTransacao>)result[0].TcsPerfilRegraTransacaoList).Add(new TcsPerfilRegraTransacao());
			
	        result[0].TcsPerfilBandeiraRedeList = new List<TcsPerfilBandeiraRede>();
	        ((List<TcsPerfilBandeiraRede>)result[0].TcsPerfilBandeiraRedeList).Add(new TcsPerfilBandeiraRede());
			
	        result[0].TcsPerfilFilialList = new List<TcsPerfilFilial>();
	        ((List<TcsPerfilFilial>)result[0].TcsPerfilFilialList).Add(new TcsPerfilFilial());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsUsuarioPerfil.
	    public IEnumerable<TcsUsuarioPerfil> ClearTcsUsuarioPerfil()
	    {
	        List<TcsUsuarioPerfil> result = new List<TcsUsuarioPerfil>();
	        result.Add(new TcsUsuarioPerfil());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsPerfilRegraModulo.
	    public IEnumerable<TcsPerfilRegraModulo> ClearTcsPerfilRegraModulo()
	    {
	        List<TcsPerfilRegraModulo> result = new List<TcsPerfilRegraModulo>();
	        result.Add(new TcsPerfilRegraModulo());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsPerfilRegraTransacao.
	    public IEnumerable<TcsPerfilRegraTransacao> ClearTcsPerfilRegraTransacao()
	    {
	        List<TcsPerfilRegraTransacao> result = new List<TcsPerfilRegraTransacao>();
	        result.Add(new TcsPerfilRegraTransacao());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsPerfilBandeiraRede.
	    public IEnumerable<TcsPerfilBandeiraRede> ClearTcsPerfilBandeiraRede()
	    {
	        List<TcsPerfilBandeiraRede> result = new List<TcsPerfilBandeiraRede>();
	        result.Add(new TcsPerfilBandeiraRede());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsPerfilFilial.
	    public IEnumerable<TcsPerfilFilial> ClearTcsPerfilFilial()
	    {
	        List<TcsPerfilFilial> result = new List<TcsPerfilFilial>();
	        result.Add(new TcsPerfilFilial());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TbcFilial.
	    public IEnumerable<TbcFilial> ClearTbcFilial()
	    {
	        List<TbcFilial> result = new List<TbcFilial>();
	        result.Add(new TbcFilial());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear SyncInfo.
	    public IEnumerable<SyncInfo> ClearSyncInfo()
	    {
	        List<SyncInfo> result = new List<SyncInfo>();
	        result.Add(new SyncInfo());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    [TcsPerfilQueryCustomAuthorizationAuto()]
	    [Query(HasSideEffects = false)]
	    //Get TcsPerfil.
	    public IQueryable<TcsPerfil> GetTcsPerfil()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTcsPerfil")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<TcsPerfil> result = 
	            (from entity0 in this.DbContext.TCS_PERFIL
                  let entity0Al1 = entity0.TBC_GRUPO_ECONOMICO
	            
	            	
	            select new TcsPerfil()		
	            {
	            
                DescPerfil = entity0.DESC_PERFIL
                , IdGpeconFranquia = entity0Al1.ID_GPECON
                , IdPerfil = entity0.ID_PERFIL
                , IdPerfilOrigem = entity0.ID_PERFIL_ORIGEM
                , Inativo = entity0.INATIVO
			
                ,TcsUsuarioPerfilList = 
	                        (from entity1 in entity0.TCS_USUARIO_PERFIL_LISTA
                                  let entity1Al2 = entity1.TCS_PERFIL
                                  let entity1Al1 = entity1.TCS_USUARIO
	                        
	                        	
	                        select new TcsUsuarioPerfil()
	                        {
	                        
                                IdLinx = entity1Al1.ID_LINX
                                , IdPerfil = entity1Al2.ID_PERFIL
                                , IdTcsUsuarioPerfil = entity1.ID_TCS_USUARIO_PERFIL
                                , IdUsuario = entity1Al1.ID_USUARIO
                                , NomeUsuario = entity1Al1.NOME_USUARIO
                                , UidUsuario = entity1Al1.UID_USUARIO
		
	                        }
	                        )
			
                ,TcsPerfilRegraModuloList = 
	                        (from entity1 in entity0.TCS_PERFIL_REGRA_MODULO_LISTA
                                  let entity1Al1 = entity1.TCS_PERFIL
                                orderby entity1.ID_MODULO ascending
	                        
	                        	
	                        select new TcsPerfilRegraModulo()
	                        {
	                        
                                IdModulo = entity1.ID_MODULO
                                , IdPerfil = entity1Al1.ID_PERFIL
                                , IdPerfilRegraModulo = entity1.ID_PERFIL_REGRA_MODULO
                                , LxRegraAcessoModulo = entity1.LX_REGRA_ACESSO_MODULO
                                , LxRegraAcessoModuloName = ((entity1.LX_REGRA_ACESSO_MODULO) == 1 ? "Acesso Bloqueado" : ((entity1.LX_REGRA_ACESSO_MODULO) == 2 ? "Acesso Total" : ((entity1.LX_REGRA_ACESSO_MODULO) == 13 ? "Acesso por Transação" : ((entity1.LX_REGRA_ACESSO_MODULO) == 5 ? "Alterar" : ((entity1.LX_REGRA_ACESSO_MODULO) == 12 ? "Criar Pesquisa" : ((entity1.LX_REGRA_ACESSO_MODULO) == 10 ? "Criar Relatório" : ((entity1.LX_REGRA_ACESSO_MODULO) == 6 ? "Excluir" : ((entity1.LX_REGRA_ACESSO_MODULO) == 9 ? "Exportar" : ((entity1.LX_REGRA_ACESSO_MODULO) == 8 ? "Imprimir" : ((entity1.LX_REGRA_ACESSO_MODULO) == 4 ? "Incluir" : ((entity1.LX_REGRA_ACESSO_MODULO) == 11 ? "Layout" : ((entity1.LX_REGRA_ACESSO_MODULO) == 7 ? "Pesquisa Especial" : ((entity1.LX_REGRA_ACESSO_MODULO) == 3 ? "Pesquisar" : ((entity1.LX_REGRA_ACESSO_MODULO) == 99 ? "Regra Transação" : ""))))))))))))))
                                , Acesso = ((entity1.LX_REGRA_ACESSO_MODULO) == 1 ? "Acesso Bloqueado" : ((entity1.LX_REGRA_ACESSO_MODULO) == 2 ? "Acesso Total" : ((entity1.LX_REGRA_ACESSO_MODULO) == 13 ? "Acesso por Transação" : ((entity1.LX_REGRA_ACESSO_MODULO) == 5 ? "Alterar" : ((entity1.LX_REGRA_ACESSO_MODULO) == 12 ? "Criar Pesquisa" : ((entity1.LX_REGRA_ACESSO_MODULO) == 10 ? "Criar Relatório" : ((entity1.LX_REGRA_ACESSO_MODULO) == 6 ? "Excluir" : ((entity1.LX_REGRA_ACESSO_MODULO) == 9 ? "Exportar" : ((entity1.LX_REGRA_ACESSO_MODULO) == 8 ? "Imprimir" : ((entity1.LX_REGRA_ACESSO_MODULO) == 4 ? "Incluir" : ((entity1.LX_REGRA_ACESSO_MODULO) == 11 ? "Layout" : ((entity1.LX_REGRA_ACESSO_MODULO) == 7 ? "Pesquisa Especial" : ((entity1.LX_REGRA_ACESSO_MODULO) == 3 ? "Pesquisar" : ((entity1.LX_REGRA_ACESSO_MODULO) == 99 ? "Regra Transação" : ""))))))))))))))
		
	                        }
	                        )
			
                ,TcsPerfilRegraTransacaoList = 
	                        (from entity1 in entity0.TCS_PERFIL_REGRA_TRANSACAO_LISTA
                                  let entity1Al1 = entity1.TCS_PERFIL
                                orderby entity1.ID_TRANSACAO ascending
	                        
	                        	
	                        select new TcsPerfilRegraTransacao()
	                        {
	                        
                                Acesso = ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 1 ? "Acesso Bloqueado" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 2 ? "Acesso Total" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 13 ? "Acesso por Transação" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 5 ? "Alterar" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 12 ? "Criar Pesquisa" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 10 ? "Criar Relatório" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 6 ? "Excluir" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 9 ? "Exportar" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 8 ? "Imprimir" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 4 ? "Incluir" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 11 ? "Layout" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 7 ? "Pesquisa Especial" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 3 ? "Pesquisar" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 99 ? "Regra Transação" : ""))))))))))))))
                                , IdPerfil = entity1Al1.ID_PERFIL
                                , IdPerfilRegraTransacao = entity1.ID_PERFIL_REGRA_TRANSACAO
                                , IdTransacao = entity1.ID_TRANSACAO
                                , LxRegraAcessoTransacao = entity1.LX_REGRA_ACESSO_TRANSACAO
                                , LxRegraAcessoTransacaoName = ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 1 ? "Acesso Bloqueado" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 2 ? "Acesso Total" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 13 ? "Acesso por Transação" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 5 ? "Alterar" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 12 ? "Criar Pesquisa" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 10 ? "Criar Relatório" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 6 ? "Excluir" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 9 ? "Exportar" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 8 ? "Imprimir" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 4 ? "Incluir" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 11 ? "Layout" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 7 ? "Pesquisa Especial" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 3 ? "Pesquisar" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 99 ? "Regra Transação" : ""))))))))))))))
		
	                        }
	                        )
			
                ,TcsPerfilBandeiraRedeList = 
	                        (from entity1 in entity0.TCS_PERFIL_BANDEIRA_REDE_LISTA
                                  let entity1Al2 = entity1.TCS_PERFIL
                                  let entity1Al1 = entity1.TBC_BANDEIRA_REDE
	                        
	                        	
	                        select new TcsPerfilBandeiraRede()
	                        {
	                        
                                DescBandeiraRede = entity1Al1.DESC_BANDEIRA_REDE
                                , IdBandeiraR = entity1Al1.ID_BANDEIRA_REDE
                                , IdPerfil = entity1Al2.ID_PERFIL
		
	                        }
	                        )
			
                ,TcsPerfilFilialList = 
	                        (from entity1 in entity0.TCS_PERFIL_FILIAL_LISTA
                                  let entity1Al1 = entity1.TBC_FILIAL
                                  let entity1Al2 = entity1.TCS_PERFIL
	                        
	                        	
	                        select new TcsPerfilFilial()
	                        {
	                        
                                CodigoFilial = entity1Al1.CODIGO_FILIAL
                                , IdFilialPfj = entity1Al1.ID_FILIAL_PFJ
                                , IdPerfil = entity1Al2.ID_PERFIL
                                , IdTcsPerfilFilial = entity1.ID_TCS_PERFIL_FILIAL
                                , NomeFilial = entity1Al1.NOME_FILIAL
		
	                        }
	                        )
		
	            }
	            );
		
	
	        TcsPerfil.OnSearching(ref result, false, null);	

	
	        return result;
	    }
			
	
	    [TcsUsuarioPerfilQueryCustomAuthorizationAuto()]
	    [Query(HasSideEffects = false)]
	    //Get TcsUsuarioPerfil.
	    public IQueryable<TcsUsuarioPerfil> GetTcsUsuarioPerfil()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTcsUsuarioPerfil")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsUsuarioPerfilQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioPerfil> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_PERFIL
                  let entity0Al2 = entity0.TCS_PERFIL
                  let entity0Al1 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioPerfil()		
	            {
	            
                IdLinx = entity0Al1.ID_LINX
                , IdPerfil = entity0Al2.ID_PERFIL
                , IdTcsUsuarioPerfil = entity0.ID_TCS_USUARIO_PERFIL
                , IdUsuario = entity0Al1.ID_USUARIO
                , NomeUsuario = entity0Al1.NOME_USUARIO
                , UidUsuario = entity0Al1.UID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TcsPerfilRegraModuloQueryCustomAuthorizationAuto()]
	    [Query(HasSideEffects = false)]
	    //Get TcsPerfilRegraModulo.
	    public IQueryable<TcsPerfilRegraModulo> GetTcsPerfilRegraModulo()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTcsPerfilRegraModulo")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilRegraModuloQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<TcsPerfilRegraModulo> result = 
	            (from entity0 in this.DbContext.TCS_PERFIL_REGRA_MODULO
                  let entity0Al1 = entity0.TCS_PERFIL
                orderby entity0.ID_MODULO ascending
	            
	            	
	            select new TcsPerfilRegraModulo()		
	            {
	            
                IdModulo = entity0.ID_MODULO
                , IdPerfil = entity0Al1.ID_PERFIL
                , IdPerfilRegraModulo = entity0.ID_PERFIL_REGRA_MODULO
                , LxRegraAcessoModulo = entity0.LX_REGRA_ACESSO_MODULO
                , LxRegraAcessoModuloName = ((entity0.LX_REGRA_ACESSO_MODULO) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_MODULO) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_MODULO) == 13 ? "Acesso por Transação" : ((entity0.LX_REGRA_ACESSO_MODULO) == 5 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 12 ? "Criar Pesquisa" : ((entity0.LX_REGRA_ACESSO_MODULO) == 10 ? "Criar Relatório" : ((entity0.LX_REGRA_ACESSO_MODULO) == 6 ? "Excluir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 9 ? "Exportar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 8 ? "Imprimir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 4 ? "Incluir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 11 ? "Layout" : ((entity0.LX_REGRA_ACESSO_MODULO) == 7 ? "Pesquisa Especial" : ((entity0.LX_REGRA_ACESSO_MODULO) == 3 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 99 ? "Regra Transação" : ""))))))))))))))
                , Acesso = ((entity0.LX_REGRA_ACESSO_MODULO) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_MODULO) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_MODULO) == 13 ? "Acesso por Transação" : ((entity0.LX_REGRA_ACESSO_MODULO) == 5 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 12 ? "Criar Pesquisa" : ((entity0.LX_REGRA_ACESSO_MODULO) == 10 ? "Criar Relatório" : ((entity0.LX_REGRA_ACESSO_MODULO) == 6 ? "Excluir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 9 ? "Exportar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 8 ? "Imprimir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 4 ? "Incluir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 11 ? "Layout" : ((entity0.LX_REGRA_ACESSO_MODULO) == 7 ? "Pesquisa Especial" : ((entity0.LX_REGRA_ACESSO_MODULO) == 3 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 99 ? "Regra Transação" : ""))))))))))))))
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TcsPerfilRegraTransacaoQueryCustomAuthorizationAuto()]
	    [Query(HasSideEffects = false)]
	    //Get TcsPerfilRegraTransacao.
	    public IQueryable<TcsPerfilRegraTransacao> GetTcsPerfilRegraTransacao()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTcsPerfilRegraTransacao")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilRegraTransacaoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<TcsPerfilRegraTransacao> result = 
	            (from entity0 in this.DbContext.TCS_PERFIL_REGRA_TRANSACAO
                  let entity0Al1 = entity0.TCS_PERFIL
                orderby entity0.ID_TRANSACAO ascending
	            
	            	
	            select new TcsPerfilRegraTransacao()		
	            {
	            
                Acesso = ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 13 ? "Acesso por Transação" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 5 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 12 ? "Criar Pesquisa" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 10 ? "Criar Relatório" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 6 ? "Excluir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 9 ? "Exportar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 8 ? "Imprimir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 4 ? "Incluir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 11 ? "Layout" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 7 ? "Pesquisa Especial" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 3 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 99 ? "Regra Transação" : ""))))))))))))))
                , IdPerfil = entity0Al1.ID_PERFIL
                , IdPerfilRegraTransacao = entity0.ID_PERFIL_REGRA_TRANSACAO
                , IdTransacao = entity0.ID_TRANSACAO
                , LxRegraAcessoTransacao = entity0.LX_REGRA_ACESSO_TRANSACAO
                , LxRegraAcessoTransacaoName = ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 13 ? "Acesso por Transação" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 5 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 12 ? "Criar Pesquisa" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 10 ? "Criar Relatório" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 6 ? "Excluir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 9 ? "Exportar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 8 ? "Imprimir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 4 ? "Incluir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 11 ? "Layout" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 7 ? "Pesquisa Especial" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 3 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 99 ? "Regra Transação" : ""))))))))))))))
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TcsPerfilBandeiraRedeQueryCustomAuthorizationAuto()]
	    [Query(HasSideEffects = false)]
	    //Get TcsPerfilBandeiraRede.
	    public IQueryable<TcsPerfilBandeiraRede> GetTcsPerfilBandeiraRede()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTcsPerfilBandeiraRede")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilBandeiraRedeQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<TcsPerfilBandeiraRede> result = 
	            (from entity0 in this.DbContext.TCS_PERFIL_BANDEIRA_REDE
                  let entity0Al2 = entity0.TCS_PERFIL
                  let entity0Al1 = entity0.TBC_BANDEIRA_REDE
	            
	            	
	            select new TcsPerfilBandeiraRede()		
	            {
	            
                DescBandeiraRede = entity0Al1.DESC_BANDEIRA_REDE
                , IdBandeiraR = entity0Al1.ID_BANDEIRA_REDE
                , IdPerfil = entity0Al2.ID_PERFIL
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TcsPerfilFilialQueryCustomAuthorizationAuto()]
	    [Query(HasSideEffects = false)]
	    //Get TcsPerfilFilial.
	    public IQueryable<TcsPerfilFilial> GetTcsPerfilFilial()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTcsPerfilFilial")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilFilialQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<TcsPerfilFilial> result = 
	            (from entity0 in this.DbContext.TCS_PERFIL_FILIAL
                  let entity0Al1 = entity0.TBC_FILIAL
                  let entity0Al2 = entity0.TCS_PERFIL
	            
	            	
	            select new TcsPerfilFilial()		
	            {
	            
                CodigoFilial = entity0Al1.CODIGO_FILIAL
                , IdFilialPfj = entity0Al1.ID_FILIAL_PFJ
                , IdPerfil = entity0Al2.ID_PERFIL
                , IdTcsPerfilFilial = entity0.ID_TCS_PERFIL_FILIAL
                , NomeFilial = entity0Al1.NOME_FILIAL
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TcsPerfilQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TcsPerfilNoAssociations.
	    public IQueryable<TcsPerfil> GetTcsPerfilNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTcsPerfilNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<TcsPerfil> result = 
	            (from entity0 in this.DbContext.TCS_PERFIL
                  let entity0Al1 = entity0.TBC_GRUPO_ECONOMICO
	            
	            	
	            select new TcsPerfil()		
	            {
	            
                DescPerfil = entity0.DESC_PERFIL
                , IdGpeconFranquia = entity0Al1.ID_GPECON
                , IdPerfil = entity0.ID_PERFIL
                , IdPerfilOrigem = entity0.ID_PERFIL_ORIGEM
                , Inativo = entity0.INATIVO
		
	            }
	            );
		
	
	        TcsPerfil.OnSearching(ref result, true, null);	

	
	        return result;
	    }
			
	
	    [TcsUsuarioPerfilQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TcsUsuarioPerfilNoAssociations.
	    public IQueryable<TcsUsuarioPerfil> GetTcsUsuarioPerfilNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTcsUsuarioPerfilNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsUsuarioPerfilQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioPerfil> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_PERFIL
                  let entity0Al2 = entity0.TCS_PERFIL
                  let entity0Al1 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioPerfil()		
	            {
	            
                IdLinx = entity0Al1.ID_LINX
                , IdPerfil = entity0Al2.ID_PERFIL
                , IdTcsUsuarioPerfil = entity0.ID_TCS_USUARIO_PERFIL
                , IdUsuario = entity0Al1.ID_USUARIO
                , NomeUsuario = entity0Al1.NOME_USUARIO
                , UidUsuario = entity0Al1.UID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TcsPerfilRegraModuloQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TcsPerfilRegraModuloNoAssociations.
	    public IQueryable<TcsPerfilRegraModulo> GetTcsPerfilRegraModuloNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTcsPerfilRegraModuloNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilRegraModuloQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<TcsPerfilRegraModulo> result = 
	            (from entity0 in this.DbContext.TCS_PERFIL_REGRA_MODULO
                  let entity0Al1 = entity0.TCS_PERFIL
                orderby entity0.ID_MODULO ascending
	            
	            	
	            select new TcsPerfilRegraModulo()		
	            {
	            
                IdModulo = entity0.ID_MODULO
                , IdPerfil = entity0Al1.ID_PERFIL
                , IdPerfilRegraModulo = entity0.ID_PERFIL_REGRA_MODULO
                , LxRegraAcessoModulo = entity0.LX_REGRA_ACESSO_MODULO
                , LxRegraAcessoModuloName = ((entity0.LX_REGRA_ACESSO_MODULO) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_MODULO) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_MODULO) == 13 ? "Acesso por Transação" : ((entity0.LX_REGRA_ACESSO_MODULO) == 5 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 12 ? "Criar Pesquisa" : ((entity0.LX_REGRA_ACESSO_MODULO) == 10 ? "Criar Relatório" : ((entity0.LX_REGRA_ACESSO_MODULO) == 6 ? "Excluir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 9 ? "Exportar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 8 ? "Imprimir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 4 ? "Incluir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 11 ? "Layout" : ((entity0.LX_REGRA_ACESSO_MODULO) == 7 ? "Pesquisa Especial" : ((entity0.LX_REGRA_ACESSO_MODULO) == 3 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 99 ? "Regra Transação" : ""))))))))))))))
                , Acesso = ((entity0.LX_REGRA_ACESSO_MODULO) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_MODULO) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_MODULO) == 13 ? "Acesso por Transação" : ((entity0.LX_REGRA_ACESSO_MODULO) == 5 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 12 ? "Criar Pesquisa" : ((entity0.LX_REGRA_ACESSO_MODULO) == 10 ? "Criar Relatório" : ((entity0.LX_REGRA_ACESSO_MODULO) == 6 ? "Excluir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 9 ? "Exportar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 8 ? "Imprimir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 4 ? "Incluir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 11 ? "Layout" : ((entity0.LX_REGRA_ACESSO_MODULO) == 7 ? "Pesquisa Especial" : ((entity0.LX_REGRA_ACESSO_MODULO) == 3 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 99 ? "Regra Transação" : ""))))))))))))))
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TcsPerfilRegraTransacaoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TcsPerfilRegraTransacaoNoAssociations.
	    public IQueryable<TcsPerfilRegraTransacao> GetTcsPerfilRegraTransacaoNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTcsPerfilRegraTransacaoNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilRegraTransacaoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<TcsPerfilRegraTransacao> result = 
	            (from entity0 in this.DbContext.TCS_PERFIL_REGRA_TRANSACAO
                  let entity0Al1 = entity0.TCS_PERFIL
                orderby entity0.ID_TRANSACAO ascending
	            
	            	
	            select new TcsPerfilRegraTransacao()		
	            {
	            
                Acesso = ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 13 ? "Acesso por Transação" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 5 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 12 ? "Criar Pesquisa" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 10 ? "Criar Relatório" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 6 ? "Excluir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 9 ? "Exportar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 8 ? "Imprimir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 4 ? "Incluir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 11 ? "Layout" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 7 ? "Pesquisa Especial" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 3 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 99 ? "Regra Transação" : ""))))))))))))))
                , IdPerfil = entity0Al1.ID_PERFIL
                , IdPerfilRegraTransacao = entity0.ID_PERFIL_REGRA_TRANSACAO
                , IdTransacao = entity0.ID_TRANSACAO
                , LxRegraAcessoTransacao = entity0.LX_REGRA_ACESSO_TRANSACAO
                , LxRegraAcessoTransacaoName = ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 13 ? "Acesso por Transação" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 5 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 12 ? "Criar Pesquisa" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 10 ? "Criar Relatório" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 6 ? "Excluir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 9 ? "Exportar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 8 ? "Imprimir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 4 ? "Incluir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 11 ? "Layout" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 7 ? "Pesquisa Especial" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 3 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 99 ? "Regra Transação" : ""))))))))))))))
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TcsPerfilBandeiraRedeQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TcsPerfilBandeiraRedeNoAssociations.
	    public IQueryable<TcsPerfilBandeiraRede> GetTcsPerfilBandeiraRedeNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTcsPerfilBandeiraRedeNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilBandeiraRedeQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<TcsPerfilBandeiraRede> result = 
	            (from entity0 in this.DbContext.TCS_PERFIL_BANDEIRA_REDE
                  let entity0Al2 = entity0.TCS_PERFIL
                  let entity0Al1 = entity0.TBC_BANDEIRA_REDE
	            
	            	
	            select new TcsPerfilBandeiraRede()		
	            {
	            
                DescBandeiraRede = entity0Al1.DESC_BANDEIRA_REDE
                , IdBandeiraR = entity0Al1.ID_BANDEIRA_REDE
                , IdPerfil = entity0Al2.ID_PERFIL
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TcsPerfilFilialQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TcsPerfilFilialNoAssociations.
	    public IQueryable<TcsPerfilFilial> GetTcsPerfilFilialNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTcsPerfilFilialNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilFilialQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<TcsPerfilFilial> result = 
	            (from entity0 in this.DbContext.TCS_PERFIL_FILIAL
                  let entity0Al1 = entity0.TBC_FILIAL
                  let entity0Al2 = entity0.TCS_PERFIL
	            
	            	
	            select new TcsPerfilFilial()		
	            {
	            
                CodigoFilial = entity0Al1.CODIGO_FILIAL
                , IdFilialPfj = entity0Al1.ID_FILIAL_PFJ
                , IdPerfil = entity0Al2.ID_PERFIL
                , IdTcsPerfilFilial = entity0.ID_TCS_PERFIL_FILIAL
                , NomeFilial = entity0Al1.NOME_FILIAL
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TbcFilialQueryCustomAuthorizationAuto()]
	    [Query(HasSideEffects = false)]
	    //Get TbcFilial.
	    public IQueryable<TbcFilial> GetTbcFilial()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTbcFilial")))
 	        {
 	             AuthorizationResult authorizationResult = (new TbcFilialQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<TbcFilial> result = 
	            (from entity0 in this.DbContext.TBC_FILIAL
	            
	            	
	            select new TbcFilial()		
	            {
	            
                CodigoFilial = entity0.CODIGO_FILIAL
                , IdFilialPfj = entity0.ID_FILIAL_PFJ
                , NomeFilial = entity0.NOME_FILIAL
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TbcFilialQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TbcFilialNoAssociations.
	    public IQueryable<TbcFilial> GetTbcFilialNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTbcFilialNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new TbcFilialQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<TbcFilial> result = 
	            (from entity0 in this.DbContext.TBC_FILIAL
	            
	            	
	            select new TbcFilial()		
	            {
	            
                CodigoFilial = entity0.CODIGO_FILIAL
                , IdFilialPfj = entity0.ID_FILIAL_PFJ
                , NomeFilial = entity0.NOME_FILIAL
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [SyncInfoQueryCustomAuthorizationAuto()]
	    [Query(HasSideEffects = false)]
	    //Get SyncInfo.
	    public IEnumerable<SyncInfo> GetSyncInfo()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetSyncInfo")))
 	        {
 	             AuthorizationResult authorizationResult = (new SyncInfoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        IEnumerable<SyncInfo> result = new List<SyncInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    [SyncInfoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get SyncInfoNoAssociations.
	    public IEnumerable<SyncInfo> GetSyncInfoNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetSyncInfoNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new SyncInfoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        IEnumerable<SyncInfo> result = new List<SyncInfo>();
	  	
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	//Add filtering disabled property for TCS_PERFIL
	    	string[] bmDisabledTcsPerfilList = this.GetEDM().GetFilteringDisabledList("TCS_PERFIL");
	    	if (bmDisabledTcsPerfilList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsPerfilList.Contains("TCS_PERFIL.DESC_PERFIL"))
	    		{
	    			result.Add("TcsPerfil|DescPerfil");
	    			result.Add("TcsPerfil|TCS_PERFIL.DESC_PERFIL");
	    		}
	
	    		if (bmDisabledTcsPerfilList.Contains("TCS_PERFIL.ID_PERFIL"))
	    		{
	    			result.Add("TcsPerfil|IdPerfil");
	    			result.Add("TcsPerfil|TCS_PERFIL.ID_PERFIL");
	    		}
	
	    		if (bmDisabledTcsPerfilList.Contains("TCS_PERFIL.ID_PERFIL_ORIGEM"))
	    		{
	    			result.Add("TcsPerfil|IdPerfilOrigem");
	    			result.Add("TcsPerfil|TCS_PERFIL.ID_PERFIL_ORIGEM");
	    		}
	
	    		if (bmDisabledTcsPerfilList.Contains("TCS_PERFIL.INATIVO"))
	    		{
	    			result.Add("TcsPerfil|Inativo");
	    			result.Add("TcsPerfil|TCS_PERFIL.INATIVO");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_USUARIO_PERFIL
	    	string[] bmDisabledTcsUsuarioPerfilList = this.GetEDM().GetFilteringDisabledList("TCS_USUARIO_PERFIL");
	    	if (bmDisabledTcsUsuarioPerfilList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsUsuarioPerfilList.Contains("TCS_USUARIO_PERFIL.ID_TCS_USUARIO_PERFIL"))
	    		{
	    			result.Add("TcsUsuarioPerfil|IdTcsUsuarioPerfil");
	    			result.Add("TcsUsuarioPerfil|TCS_USUARIO_PERFIL.ID_TCS_USUARIO_PERFIL");
	    		}
	    	}
	    	result.Add("TcsPerfilRegraModulo|Acesso");
	    	result.Add("TcsPerfilRegraModulo|((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 1 ? 'Acesso Bloqueado' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 2 ? 'Acesso Total' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 13 ? 'Acesso por Transação' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 5 ? 'Alterar' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 12 ? 'Criar Pesquisa' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 10 ? 'Criar Relatório' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 6 ? 'Excluir' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 9 ? 'Exportar' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 8 ? 'Imprimir' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 4 ? 'Incluir' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 11 ? 'Layout' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 7 ? 'Pesquisa Especial' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 3 ? 'Pesquisar' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 99 ? 'Regra Transação' : ''))))))))))))))");
	    	//Add filtering disabled property for TCS_PERFIL_REGRA_MODULO
	    	string[] bmDisabledTcsPerfilRegraModuloList = this.GetEDM().GetFilteringDisabledList("TCS_PERFIL_REGRA_MODULO");
	    	if (bmDisabledTcsPerfilRegraModuloList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsPerfilRegraModuloList.Contains("TCS_PERFIL_REGRA_MODULO.ID_MODULO"))
	    		{
	    			result.Add("TcsPerfilRegraModulo|IdModulo");
	    			result.Add("TcsPerfilRegraModulo|TCS_PERFIL_REGRA_MODULO.ID_MODULO");
	    		}
	
	    		if (bmDisabledTcsPerfilRegraModuloList.Contains("TCS_PERFIL_REGRA_MODULO.ID_PERFIL_REGRA_MODULO"))
	    		{
	    			result.Add("TcsPerfilRegraModulo|IdPerfilRegraModulo");
	    			result.Add("TcsPerfilRegraModulo|TCS_PERFIL_REGRA_MODULO.ID_PERFIL_REGRA_MODULO");
	    		}
	
	    		if (bmDisabledTcsPerfilRegraModuloList.Contains("TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO"))
	    		{
	    			result.Add("TcsPerfilRegraModulo|LxRegraAcessoModulo");
	    			result.Add("TcsPerfilRegraModulo|TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO");
	    		}
	    	}
	    	result.Add("TcsPerfilRegraTransacao|Acesso");
	    	result.Add("TcsPerfilRegraTransacao|((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 1 ? 'Acesso Bloqueado' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 2 ? 'Acesso Total' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 13 ? 'Acesso por Transação' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 5 ? 'Alterar' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 12 ? 'Criar Pesquisa' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 10 ? 'Criar Relatório' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 6 ? 'Excluir' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 9 ? 'Exportar' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 8 ? 'Imprimir' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 4 ? 'Incluir' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 11 ? 'Layout' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 7 ? 'Pesquisa Especial' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 3 ? 'Pesquisar' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 99 ? 'Regra Transação' : ''))))))))))))))");
	    	//Add filtering disabled property for TCS_PERFIL_REGRA_TRANSACAO
	    	string[] bmDisabledTcsPerfilRegraTransacaoList = this.GetEDM().GetFilteringDisabledList("TCS_PERFIL_REGRA_TRANSACAO");
	    	if (bmDisabledTcsPerfilRegraTransacaoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsPerfilRegraTransacaoList.Contains("TCS_PERFIL_REGRA_TRANSACAO.ID_PERFIL_REGRA_TRANSACAO"))
	    		{
	    			result.Add("TcsPerfilRegraTransacao|IdPerfilRegraTransacao");
	    			result.Add("TcsPerfilRegraTransacao|TCS_PERFIL_REGRA_TRANSACAO.ID_PERFIL_REGRA_TRANSACAO");
	    		}
	
	    		if (bmDisabledTcsPerfilRegraTransacaoList.Contains("TCS_PERFIL_REGRA_TRANSACAO.ID_TRANSACAO"))
	    		{
	    			result.Add("TcsPerfilRegraTransacao|IdTransacao");
	    			result.Add("TcsPerfilRegraTransacao|TCS_PERFIL_REGRA_TRANSACAO.ID_TRANSACAO");
	    		}
	
	    		if (bmDisabledTcsPerfilRegraTransacaoList.Contains("TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO"))
	    		{
	    			result.Add("TcsPerfilRegraTransacao|LxRegraAcessoTransacao");
	    			result.Add("TcsPerfilRegraTransacao|TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_PERFIL_BANDEIRA_REDE
	    	string[] bmDisabledTcsPerfilBandeiraRedeList = this.GetEDM().GetFilteringDisabledList("TCS_PERFIL_BANDEIRA_REDE");
	    	if (bmDisabledTcsPerfilBandeiraRedeList.Length > 0)
	    	{
	    	}
	    	//Add filtering disabled property for TCS_PERFIL_FILIAL
	    	string[] bmDisabledTcsPerfilFilialList = this.GetEDM().GetFilteringDisabledList("TCS_PERFIL_FILIAL");
	    	if (bmDisabledTcsPerfilFilialList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsPerfilFilialList.Contains("TCS_PERFIL_FILIAL.ID_TCS_PERFIL_FILIAL"))
	    		{
	    			result.Add("TcsPerfilFilial|IdTcsPerfilFilial");
	    			result.Add("TcsPerfilFilial|TCS_PERFIL_FILIAL.ID_TCS_PERFIL_FILIAL");
	    		}
	    	}
	    	//Add filtering disabled property for TBC_FILIAL
	    	string[] bmDisabledTbcFilialList = this.GetEDM().GetFilteringDisabledList("TBC_FILIAL");
	    	if (bmDisabledTbcFilialList.Length > 0)
	    	{
	
	    		if (bmDisabledTbcFilialList.Contains("TBC_FILIAL.CODIGO_FILIAL"))
	    		{
	    			result.Add("TbcFilial|CodigoFilial");
	    			result.Add("TbcFilial|TBC_FILIAL.CODIGO_FILIAL");
	    		}
	
	    		if (bmDisabledTbcFilialList.Contains("TBC_FILIAL.ID_FILIAL_PFJ"))
	    		{
	    			result.Add("TbcFilial|IdFilialPfj");
	    			result.Add("TbcFilial|TBC_FILIAL.ID_FILIAL_PFJ");
	    		}
	
	    		if (bmDisabledTbcFilialList.Contains("TBC_FILIAL.NOME_FILIAL"))
	    		{
	    			result.Add("TbcFilial|NomeFilial");
	    			result.Add("TbcFilial|TBC_FILIAL.NOME_FILIAL");
	    		}
	    	}
	    	return result;
	    }

	    #endregion Excluded Filters

	    #region Get By EntitySearchId Methods Definitions.
	
				
	    [Ignore]
	    //Get TcsPerfil By EntitySearchId.
	    public IQueryable<TcsPerfil> GetTcsPerfilByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = Linx.Business.Tools.UserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsPerfilByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioPerfil By EntitySearchId.
	    public IQueryable<TcsUsuarioPerfil> GetTcsUsuarioPerfilByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = Linx.Business.Tools.UserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioPerfilByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsPerfilRegraModulo By EntitySearchId.
	    public IQueryable<TcsPerfilRegraModulo> GetTcsPerfilRegraModuloByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = Linx.Business.Tools.UserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsPerfilRegraModuloByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsPerfilRegraTransacao By EntitySearchId.
	    public IQueryable<TcsPerfilRegraTransacao> GetTcsPerfilRegraTransacaoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = Linx.Business.Tools.UserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsPerfilRegraTransacaoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsPerfilBandeiraRede By EntitySearchId.
	    public IQueryable<TcsPerfilBandeiraRede> GetTcsPerfilBandeiraRedeByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = Linx.Business.Tools.UserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsPerfilBandeiraRedeByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsPerfilFilial By EntitySearchId.
	    public IQueryable<TcsPerfilFilial> GetTcsPerfilFilialByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = Linx.Business.Tools.UserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsPerfilFilialByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsPerfil By EntitySearchId.
	    public IQueryable<TcsPerfil> GetTcsPerfilByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = Linx.Business.Tools.UserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsPerfilByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioPerfil By EntitySearchId.
	    public IQueryable<TcsUsuarioPerfil> GetTcsUsuarioPerfilByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = Linx.Business.Tools.UserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioPerfilByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsPerfilRegraModulo By EntitySearchId.
	    public IQueryable<TcsPerfilRegraModulo> GetTcsPerfilRegraModuloByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = Linx.Business.Tools.UserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsPerfilRegraModuloByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsPerfilRegraTransacao By EntitySearchId.
	    public IQueryable<TcsPerfilRegraTransacao> GetTcsPerfilRegraTransacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = Linx.Business.Tools.UserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsPerfilRegraTransacaoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsPerfilBandeiraRede By EntitySearchId.
	    public IQueryable<TcsPerfilBandeiraRede> GetTcsPerfilBandeiraRedeByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = Linx.Business.Tools.UserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsPerfilBandeiraRedeByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsPerfilFilial By EntitySearchId.
	    public IQueryable<TcsPerfilFilial> GetTcsPerfilFilialByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = Linx.Business.Tools.UserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsPerfilFilialByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TbcFilial By EntitySearchId.
	    public IQueryable<TbcFilial> GetTbcFilialByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = Linx.Business.Tools.UserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTbcFilialByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TbcFilial By EntitySearchId.
	    public IQueryable<TbcFilial> GetTbcFilialByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = Linx.Business.Tools.UserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTbcFilialByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get SyncInfo By EntitySearchId.
	    public IEnumerable<SyncInfo> GetSyncInfoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = Linx.Business.Tools.UserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetSyncInfoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get SyncInfo By EntitySearchId.
	    public IEnumerable<SyncInfo> GetSyncInfoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = Linx.Business.Tools.UserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetSyncInfoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get TcsPerfil By Example.
	    [Ignore]
	    public IQueryable<TcsPerfil> GetTcsPerfilByExample(TcsPerfil entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsPerfilByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsUsuarioPerfil By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioPerfil> GetTcsUsuarioPerfilByExample(TcsUsuarioPerfil entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioPerfilByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsPerfilRegraModulo By Example.
	    [Ignore]
	    public IQueryable<TcsPerfilRegraModulo> GetTcsPerfilRegraModuloByExample(TcsPerfilRegraModulo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsPerfilRegraModuloByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsPerfilRegraTransacao By Example.
	    [Ignore]
	    public IQueryable<TcsPerfilRegraTransacao> GetTcsPerfilRegraTransacaoByExample(TcsPerfilRegraTransacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsPerfilRegraTransacaoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsPerfilBandeiraRede By Example.
	    [Ignore]
	    public IQueryable<TcsPerfilBandeiraRede> GetTcsPerfilBandeiraRedeByExample(TcsPerfilBandeiraRede entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsPerfilBandeiraRedeByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsPerfilFilial By Example.
	    [Ignore]
	    public IQueryable<TcsPerfilFilial> GetTcsPerfilFilialByExample(TcsPerfilFilial entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsPerfilFilialByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsPerfil By Example.
	    [Ignore]
	    public IQueryable<TcsPerfil> GetTcsPerfilByExampleNoAssociations(TcsPerfil entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsPerfilByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsUsuarioPerfil By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioPerfil> GetTcsUsuarioPerfilByExampleNoAssociations(TcsUsuarioPerfil entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioPerfilByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsPerfilRegraModulo By Example.
	    [Ignore]
	    public IQueryable<TcsPerfilRegraModulo> GetTcsPerfilRegraModuloByExampleNoAssociations(TcsPerfilRegraModulo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsPerfilRegraModuloByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsPerfilRegraTransacao By Example.
	    [Ignore]
	    public IQueryable<TcsPerfilRegraTransacao> GetTcsPerfilRegraTransacaoByExampleNoAssociations(TcsPerfilRegraTransacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsPerfilRegraTransacaoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsPerfilBandeiraRede By Example.
	    [Ignore]
	    public IQueryable<TcsPerfilBandeiraRede> GetTcsPerfilBandeiraRedeByExampleNoAssociations(TcsPerfilBandeiraRede entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsPerfilBandeiraRedeByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsPerfilFilial By Example.
	    [Ignore]
	    public IQueryable<TcsPerfilFilial> GetTcsPerfilFilialByExampleNoAssociations(TcsPerfilFilial entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsPerfilFilialByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TbcFilial By Example.
	    [Ignore]
	    public IQueryable<TbcFilial> GetTbcFilialByExample(TbcFilial entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTbcFilialByEntitySearch(queryAnalysis);
	    }
			
	    //Get TbcFilial By Example.
	    [Ignore]
	    public IQueryable<TbcFilial> GetTbcFilialByExampleNoAssociations(TbcFilial entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTbcFilialByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get SyncInfo By Example.
	    [Ignore]
	    public IEnumerable<SyncInfo> GetSyncInfoByExample(SyncInfo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetSyncInfoByEntitySearch(queryAnalysis);
	    }
			
	    //Get SyncInfo By Example.
	    [Ignore]
	    public IEnumerable<SyncInfo> GetSyncInfoByExampleNoAssociations(SyncInfo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetSyncInfoByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public TcsPerfil GetTcsPerfilByKey(long idPerfil)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsPerfil");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdPerfil"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idPerfil));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsPerfilByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsUsuarioPerfil GetTcsUsuarioPerfilByKey(long idTcsUsuarioPerfil)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsUsuarioPerfil");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsUsuarioPerfil"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsUsuarioPerfil));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsUsuarioPerfilByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsPerfilRegraModulo GetTcsPerfilRegraModuloByKey(long idPerfilRegraModulo)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsPerfilRegraModulo");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdPerfilRegraModulo"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idPerfilRegraModulo));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsPerfilRegraModuloByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsPerfilRegraTransacao GetTcsPerfilRegraTransacaoByKey(long idPerfilRegraTransacao)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsPerfilRegraTransacao");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdPerfilRegraTransacao"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idPerfilRegraTransacao));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsPerfilRegraTransacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsPerfilBandeiraRede GetTcsPerfilBandeiraRedeByKey(int idBandeiraR, long idPerfil)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsPerfilBandeiraRede");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdBandeiraR"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idBandeiraR));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdPerfil"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idPerfil));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsPerfilBandeiraRedeByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsPerfilFilial GetTcsPerfilFilialByKey(long idTcsPerfilFilial)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsPerfilFilial");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsPerfilFilial"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsPerfilFilial));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsPerfilFilialByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TbcFilial GetTbcFilialByKey(int idFilialPfj)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TbcFilial");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdFilialPfj"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idFilialPfj));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTbcFilialByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public SyncInfo GetSyncInfoByKey(string operacao)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("SyncInfo");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "Operacao"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, operacao));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetSyncInfoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    [TcsPerfilQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TcsPerfilByEntitySearch.
	    public IQueryable<TcsPerfil> GetTcsPerfilByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTcsPerfilByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsPerfil));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsPerfil> result = 
	            (from entity0 in this.DbContext.TCS_PERFIL.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TBC_GRUPO_ECONOMICO
	            
	            	
	            select new TcsPerfil()		
	            {
	            
                DescPerfil = entity0.DESC_PERFIL
                , IdGpeconFranquia = entity0Al1.ID_GPECON
                , IdPerfil = entity0.ID_PERFIL
                , IdPerfilOrigem = entity0.ID_PERFIL_ORIGEM
                , Inativo = entity0.INATIVO
			
                ,TcsUsuarioPerfilList = 
	                        (from entity1 in entity0.TCS_USUARIO_PERFIL_LISTA
                                  let entity1Al2 = entity1.TCS_PERFIL
                                  let entity1Al1 = entity1.TCS_USUARIO
	                        
	                        	
	                        select new TcsUsuarioPerfil()
	                        {
	                        
                                IdLinx = entity1Al1.ID_LINX
                                , IdPerfil = entity1Al2.ID_PERFIL
                                , IdTcsUsuarioPerfil = entity1.ID_TCS_USUARIO_PERFIL
                                , IdUsuario = entity1Al1.ID_USUARIO
                                , NomeUsuario = entity1Al1.NOME_USUARIO
                                , UidUsuario = entity1Al1.UID_USUARIO
		
	                        }
	                        )
			
                ,TcsPerfilRegraModuloList = 
	                        (from entity1 in entity0.TCS_PERFIL_REGRA_MODULO_LISTA
                                  let entity1Al1 = entity1.TCS_PERFIL
                                orderby entity1.ID_MODULO ascending
	                        
	                        	
	                        select new TcsPerfilRegraModulo()
	                        {
	                        
                                IdModulo = entity1.ID_MODULO
                                , IdPerfil = entity1Al1.ID_PERFIL
                                , IdPerfilRegraModulo = entity1.ID_PERFIL_REGRA_MODULO
                                , LxRegraAcessoModulo = entity1.LX_REGRA_ACESSO_MODULO
                                , LxRegraAcessoModuloName = ((entity1.LX_REGRA_ACESSO_MODULO) == 1 ? "Acesso Bloqueado" : ((entity1.LX_REGRA_ACESSO_MODULO) == 2 ? "Acesso Total" : ((entity1.LX_REGRA_ACESSO_MODULO) == 13 ? "Acesso por Transação" : ((entity1.LX_REGRA_ACESSO_MODULO) == 5 ? "Alterar" : ((entity1.LX_REGRA_ACESSO_MODULO) == 12 ? "Criar Pesquisa" : ((entity1.LX_REGRA_ACESSO_MODULO) == 10 ? "Criar Relatório" : ((entity1.LX_REGRA_ACESSO_MODULO) == 6 ? "Excluir" : ((entity1.LX_REGRA_ACESSO_MODULO) == 9 ? "Exportar" : ((entity1.LX_REGRA_ACESSO_MODULO) == 8 ? "Imprimir" : ((entity1.LX_REGRA_ACESSO_MODULO) == 4 ? "Incluir" : ((entity1.LX_REGRA_ACESSO_MODULO) == 11 ? "Layout" : ((entity1.LX_REGRA_ACESSO_MODULO) == 7 ? "Pesquisa Especial" : ((entity1.LX_REGRA_ACESSO_MODULO) == 3 ? "Pesquisar" : ((entity1.LX_REGRA_ACESSO_MODULO) == 99 ? "Regra Transação" : ""))))))))))))))
                                , Acesso = ((entity1.LX_REGRA_ACESSO_MODULO) == 1 ? "Acesso Bloqueado" : ((entity1.LX_REGRA_ACESSO_MODULO) == 2 ? "Acesso Total" : ((entity1.LX_REGRA_ACESSO_MODULO) == 13 ? "Acesso por Transação" : ((entity1.LX_REGRA_ACESSO_MODULO) == 5 ? "Alterar" : ((entity1.LX_REGRA_ACESSO_MODULO) == 12 ? "Criar Pesquisa" : ((entity1.LX_REGRA_ACESSO_MODULO) == 10 ? "Criar Relatório" : ((entity1.LX_REGRA_ACESSO_MODULO) == 6 ? "Excluir" : ((entity1.LX_REGRA_ACESSO_MODULO) == 9 ? "Exportar" : ((entity1.LX_REGRA_ACESSO_MODULO) == 8 ? "Imprimir" : ((entity1.LX_REGRA_ACESSO_MODULO) == 4 ? "Incluir" : ((entity1.LX_REGRA_ACESSO_MODULO) == 11 ? "Layout" : ((entity1.LX_REGRA_ACESSO_MODULO) == 7 ? "Pesquisa Especial" : ((entity1.LX_REGRA_ACESSO_MODULO) == 3 ? "Pesquisar" : ((entity1.LX_REGRA_ACESSO_MODULO) == 99 ? "Regra Transação" : ""))))))))))))))
		
	                        }
	                        )
			
                ,TcsPerfilRegraTransacaoList = 
	                        (from entity1 in entity0.TCS_PERFIL_REGRA_TRANSACAO_LISTA
                                  let entity1Al1 = entity1.TCS_PERFIL
                                orderby entity1.ID_TRANSACAO ascending
	                        
	                        	
	                        select new TcsPerfilRegraTransacao()
	                        {
	                        
                                Acesso = ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 1 ? "Acesso Bloqueado" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 2 ? "Acesso Total" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 13 ? "Acesso por Transação" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 5 ? "Alterar" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 12 ? "Criar Pesquisa" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 10 ? "Criar Relatório" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 6 ? "Excluir" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 9 ? "Exportar" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 8 ? "Imprimir" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 4 ? "Incluir" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 11 ? "Layout" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 7 ? "Pesquisa Especial" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 3 ? "Pesquisar" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 99 ? "Regra Transação" : ""))))))))))))))
                                , IdPerfil = entity1Al1.ID_PERFIL
                                , IdPerfilRegraTransacao = entity1.ID_PERFIL_REGRA_TRANSACAO
                                , IdTransacao = entity1.ID_TRANSACAO
                                , LxRegraAcessoTransacao = entity1.LX_REGRA_ACESSO_TRANSACAO
                                , LxRegraAcessoTransacaoName = ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 1 ? "Acesso Bloqueado" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 2 ? "Acesso Total" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 13 ? "Acesso por Transação" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 5 ? "Alterar" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 12 ? "Criar Pesquisa" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 10 ? "Criar Relatório" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 6 ? "Excluir" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 9 ? "Exportar" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 8 ? "Imprimir" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 4 ? "Incluir" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 11 ? "Layout" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 7 ? "Pesquisa Especial" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 3 ? "Pesquisar" : ((entity1.LX_REGRA_ACESSO_TRANSACAO) == 99 ? "Regra Transação" : ""))))))))))))))
		
	                        }
	                        )
			
                ,TcsPerfilBandeiraRedeList = 
	                        (from entity1 in entity0.TCS_PERFIL_BANDEIRA_REDE_LISTA
                                  let entity1Al2 = entity1.TCS_PERFIL
                                  let entity1Al1 = entity1.TBC_BANDEIRA_REDE
	                        
	                        	
	                        select new TcsPerfilBandeiraRede()
	                        {
	                        
                                DescBandeiraRede = entity1Al1.DESC_BANDEIRA_REDE
                                , IdBandeiraR = entity1Al1.ID_BANDEIRA_REDE
                                , IdPerfil = entity1Al2.ID_PERFIL
		
	                        }
	                        )
			
                ,TcsPerfilFilialList = 
	                        (from entity1 in entity0.TCS_PERFIL_FILIAL_LISTA
                                  let entity1Al1 = entity1.TBC_FILIAL
                                  let entity1Al2 = entity1.TCS_PERFIL
	                        
	                        	
	                        select new TcsPerfilFilial()
	                        {
	                        
                                CodigoFilial = entity1Al1.CODIGO_FILIAL
                                , IdFilialPfj = entity1Al1.ID_FILIAL_PFJ
                                , IdPerfil = entity1Al2.ID_PERFIL
                                , IdTcsPerfilFilial = entity1.ID_TCS_PERFIL_FILIAL
                                , NomeFilial = entity1Al1.NOME_FILIAL
		
	                        }
	                        )
		
	            }
	            );
		
	
	        TcsPerfil.OnSearching(ref result, false, entitySearchList);	

	
	        return result;
	    }
			
	
	    [TcsUsuarioPerfilQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TcsUsuarioPerfilByEntitySearch.
	    public IQueryable<TcsUsuarioPerfil> GetTcsUsuarioPerfilByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTcsUsuarioPerfilByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsUsuarioPerfilQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioPerfil));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioPerfil> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_PERFIL.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.TCS_PERFIL
                  let entity0Al1 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioPerfil()		
	            {
	            
                IdLinx = entity0Al1.ID_LINX
                , IdPerfil = entity0Al2.ID_PERFIL
                , IdTcsUsuarioPerfil = entity0.ID_TCS_USUARIO_PERFIL
                , IdUsuario = entity0Al1.ID_USUARIO
                , NomeUsuario = entity0Al1.NOME_USUARIO
                , UidUsuario = entity0Al1.UID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TcsPerfilRegraModuloQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TcsPerfilRegraModuloByEntitySearch.
	    public IQueryable<TcsPerfilRegraModulo> GetTcsPerfilRegraModuloByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTcsPerfilRegraModuloByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilRegraModuloQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsPerfilRegraModulo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsPerfilRegraModulo> result = 
	            (from entity0 in this.DbContext.TCS_PERFIL_REGRA_MODULO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_PERFIL
                orderby entity0.ID_MODULO ascending
	            
	            	
	            select new TcsPerfilRegraModulo()		
	            {
	            
                IdModulo = entity0.ID_MODULO
                , IdPerfil = entity0Al1.ID_PERFIL
                , IdPerfilRegraModulo = entity0.ID_PERFIL_REGRA_MODULO
                , LxRegraAcessoModulo = entity0.LX_REGRA_ACESSO_MODULO
                , LxRegraAcessoModuloName = ((entity0.LX_REGRA_ACESSO_MODULO) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_MODULO) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_MODULO) == 13 ? "Acesso por Transação" : ((entity0.LX_REGRA_ACESSO_MODULO) == 5 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 12 ? "Criar Pesquisa" : ((entity0.LX_REGRA_ACESSO_MODULO) == 10 ? "Criar Relatório" : ((entity0.LX_REGRA_ACESSO_MODULO) == 6 ? "Excluir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 9 ? "Exportar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 8 ? "Imprimir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 4 ? "Incluir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 11 ? "Layout" : ((entity0.LX_REGRA_ACESSO_MODULO) == 7 ? "Pesquisa Especial" : ((entity0.LX_REGRA_ACESSO_MODULO) == 3 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 99 ? "Regra Transação" : ""))))))))))))))
                , Acesso = ((entity0.LX_REGRA_ACESSO_MODULO) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_MODULO) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_MODULO) == 13 ? "Acesso por Transação" : ((entity0.LX_REGRA_ACESSO_MODULO) == 5 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 12 ? "Criar Pesquisa" : ((entity0.LX_REGRA_ACESSO_MODULO) == 10 ? "Criar Relatório" : ((entity0.LX_REGRA_ACESSO_MODULO) == 6 ? "Excluir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 9 ? "Exportar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 8 ? "Imprimir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 4 ? "Incluir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 11 ? "Layout" : ((entity0.LX_REGRA_ACESSO_MODULO) == 7 ? "Pesquisa Especial" : ((entity0.LX_REGRA_ACESSO_MODULO) == 3 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 99 ? "Regra Transação" : ""))))))))))))))
		
	            }
	            );
	
	        SetTcsPerfilRegraModuloBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    [TcsPerfilRegraTransacaoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TcsPerfilRegraTransacaoByEntitySearch.
	    public IQueryable<TcsPerfilRegraTransacao> GetTcsPerfilRegraTransacaoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTcsPerfilRegraTransacaoByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilRegraTransacaoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsPerfilRegraTransacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsPerfilRegraTransacao> result = 
	            (from entity0 in this.DbContext.TCS_PERFIL_REGRA_TRANSACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_PERFIL
                orderby entity0.ID_TRANSACAO ascending
	            
	            	
	            select new TcsPerfilRegraTransacao()		
	            {
	            
                Acesso = ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 13 ? "Acesso por Transação" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 5 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 12 ? "Criar Pesquisa" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 10 ? "Criar Relatório" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 6 ? "Excluir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 9 ? "Exportar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 8 ? "Imprimir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 4 ? "Incluir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 11 ? "Layout" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 7 ? "Pesquisa Especial" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 3 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 99 ? "Regra Transação" : ""))))))))))))))
                , IdPerfil = entity0Al1.ID_PERFIL
                , IdPerfilRegraTransacao = entity0.ID_PERFIL_REGRA_TRANSACAO
                , IdTransacao = entity0.ID_TRANSACAO
                , LxRegraAcessoTransacao = entity0.LX_REGRA_ACESSO_TRANSACAO
                , LxRegraAcessoTransacaoName = ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 13 ? "Acesso por Transação" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 5 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 12 ? "Criar Pesquisa" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 10 ? "Criar Relatório" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 6 ? "Excluir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 9 ? "Exportar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 8 ? "Imprimir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 4 ? "Incluir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 11 ? "Layout" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 7 ? "Pesquisa Especial" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 3 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 99 ? "Regra Transação" : ""))))))))))))))
		
	            }
	            );
	
	        SetTcsPerfilRegraTransacaoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    [TcsPerfilBandeiraRedeQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TcsPerfilBandeiraRedeByEntitySearch.
	    public IQueryable<TcsPerfilBandeiraRede> GetTcsPerfilBandeiraRedeByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTcsPerfilBandeiraRedeByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilBandeiraRedeQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsPerfilBandeiraRede));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsPerfilBandeiraRede> result = 
	            (from entity0 in this.DbContext.TCS_PERFIL_BANDEIRA_REDE.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.TCS_PERFIL
                  let entity0Al1 = entity0.TBC_BANDEIRA_REDE
	            
	            	
	            select new TcsPerfilBandeiraRede()		
	            {
	            
                DescBandeiraRede = entity0Al1.DESC_BANDEIRA_REDE
                , IdBandeiraR = entity0Al1.ID_BANDEIRA_REDE
                , IdPerfil = entity0Al2.ID_PERFIL
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TcsPerfilFilialQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TcsPerfilFilialByEntitySearch.
	    public IQueryable<TcsPerfilFilial> GetTcsPerfilFilialByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTcsPerfilFilialByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilFilialQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsPerfilFilial));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsPerfilFilial> result = 
	            (from entity0 in this.DbContext.TCS_PERFIL_FILIAL.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TBC_FILIAL
                  let entity0Al2 = entity0.TCS_PERFIL
	            
	            	
	            select new TcsPerfilFilial()		
	            {
	            
                CodigoFilial = entity0Al1.CODIGO_FILIAL
                , IdFilialPfj = entity0Al1.ID_FILIAL_PFJ
                , IdPerfil = entity0Al2.ID_PERFIL
                , IdTcsPerfilFilial = entity0.ID_TCS_PERFIL_FILIAL
                , NomeFilial = entity0Al1.NOME_FILIAL
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TcsPerfilQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TcsPerfilByEntitySearchNoAssociations.
	    public IQueryable<TcsPerfil> GetTcsPerfilByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTcsPerfilByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsPerfil));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsPerfil> result = 
	            (from entity0 in this.DbContext.TCS_PERFIL.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TBC_GRUPO_ECONOMICO
	            
	            	
	            select new TcsPerfil()		
	            {
	            
                DescPerfil = entity0.DESC_PERFIL
                , IdGpeconFranquia = entity0Al1.ID_GPECON
                , IdPerfil = entity0.ID_PERFIL
                , IdPerfilOrigem = entity0.ID_PERFIL_ORIGEM
                , Inativo = entity0.INATIVO
		
	            }
	            );
		
	
	        TcsPerfil.OnSearching(ref result, true, entitySearchList);	

	
	        return result;
	    }
			
	
	    [TcsUsuarioPerfilQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TcsUsuarioPerfilByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioPerfil> GetTcsUsuarioPerfilByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTcsUsuarioPerfilByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsUsuarioPerfilQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioPerfil));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioPerfil> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_PERFIL.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.TCS_PERFIL
                  let entity0Al1 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioPerfil()		
	            {
	            
                IdLinx = entity0Al1.ID_LINX
                , IdPerfil = entity0Al2.ID_PERFIL
                , IdTcsUsuarioPerfil = entity0.ID_TCS_USUARIO_PERFIL
                , IdUsuario = entity0Al1.ID_USUARIO
                , NomeUsuario = entity0Al1.NOME_USUARIO
                , UidUsuario = entity0Al1.UID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TcsPerfilRegraModuloQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TcsPerfilRegraModuloByEntitySearchNoAssociations.
	    public IQueryable<TcsPerfilRegraModulo> GetTcsPerfilRegraModuloByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTcsPerfilRegraModuloByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilRegraModuloQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsPerfilRegraModulo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsPerfilRegraModulo> result = 
	            (from entity0 in this.DbContext.TCS_PERFIL_REGRA_MODULO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_PERFIL
                orderby entity0.ID_MODULO ascending
	            
	            	
	            select new TcsPerfilRegraModulo()		
	            {
	            
                IdModulo = entity0.ID_MODULO
                , IdPerfil = entity0Al1.ID_PERFIL
                , IdPerfilRegraModulo = entity0.ID_PERFIL_REGRA_MODULO
                , LxRegraAcessoModulo = entity0.LX_REGRA_ACESSO_MODULO
                , LxRegraAcessoModuloName = ((entity0.LX_REGRA_ACESSO_MODULO) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_MODULO) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_MODULO) == 13 ? "Acesso por Transação" : ((entity0.LX_REGRA_ACESSO_MODULO) == 5 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 12 ? "Criar Pesquisa" : ((entity0.LX_REGRA_ACESSO_MODULO) == 10 ? "Criar Relatório" : ((entity0.LX_REGRA_ACESSO_MODULO) == 6 ? "Excluir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 9 ? "Exportar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 8 ? "Imprimir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 4 ? "Incluir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 11 ? "Layout" : ((entity0.LX_REGRA_ACESSO_MODULO) == 7 ? "Pesquisa Especial" : ((entity0.LX_REGRA_ACESSO_MODULO) == 3 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 99 ? "Regra Transação" : ""))))))))))))))
                , Acesso = ((entity0.LX_REGRA_ACESSO_MODULO) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_MODULO) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_MODULO) == 13 ? "Acesso por Transação" : ((entity0.LX_REGRA_ACESSO_MODULO) == 5 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 12 ? "Criar Pesquisa" : ((entity0.LX_REGRA_ACESSO_MODULO) == 10 ? "Criar Relatório" : ((entity0.LX_REGRA_ACESSO_MODULO) == 6 ? "Excluir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 9 ? "Exportar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 8 ? "Imprimir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 4 ? "Incluir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 11 ? "Layout" : ((entity0.LX_REGRA_ACESSO_MODULO) == 7 ? "Pesquisa Especial" : ((entity0.LX_REGRA_ACESSO_MODULO) == 3 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 99 ? "Regra Transação" : ""))))))))))))))
		
	            }
	            );
	
	        SetTcsPerfilRegraModuloBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    [TcsPerfilRegraTransacaoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TcsPerfilRegraTransacaoByEntitySearchNoAssociations.
	    public IQueryable<TcsPerfilRegraTransacao> GetTcsPerfilRegraTransacaoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTcsPerfilRegraTransacaoByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilRegraTransacaoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsPerfilRegraTransacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsPerfilRegraTransacao> result = 
	            (from entity0 in this.DbContext.TCS_PERFIL_REGRA_TRANSACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_PERFIL
                orderby entity0.ID_TRANSACAO ascending
	            
	            	
	            select new TcsPerfilRegraTransacao()		
	            {
	            
                Acesso = ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 13 ? "Acesso por Transação" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 5 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 12 ? "Criar Pesquisa" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 10 ? "Criar Relatório" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 6 ? "Excluir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 9 ? "Exportar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 8 ? "Imprimir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 4 ? "Incluir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 11 ? "Layout" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 7 ? "Pesquisa Especial" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 3 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 99 ? "Regra Transação" : ""))))))))))))))
                , IdPerfil = entity0Al1.ID_PERFIL
                , IdPerfilRegraTransacao = entity0.ID_PERFIL_REGRA_TRANSACAO
                , IdTransacao = entity0.ID_TRANSACAO
                , LxRegraAcessoTransacao = entity0.LX_REGRA_ACESSO_TRANSACAO
                , LxRegraAcessoTransacaoName = ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 13 ? "Acesso por Transação" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 5 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 12 ? "Criar Pesquisa" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 10 ? "Criar Relatório" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 6 ? "Excluir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 9 ? "Exportar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 8 ? "Imprimir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 4 ? "Incluir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 11 ? "Layout" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 7 ? "Pesquisa Especial" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 3 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 99 ? "Regra Transação" : ""))))))))))))))
		
	            }
	            );
	
	        SetTcsPerfilRegraTransacaoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    [TcsPerfilBandeiraRedeQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TcsPerfilBandeiraRedeByEntitySearchNoAssociations.
	    public IQueryable<TcsPerfilBandeiraRede> GetTcsPerfilBandeiraRedeByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTcsPerfilBandeiraRedeByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilBandeiraRedeQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsPerfilBandeiraRede));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsPerfilBandeiraRede> result = 
	            (from entity0 in this.DbContext.TCS_PERFIL_BANDEIRA_REDE.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.TCS_PERFIL
                  let entity0Al1 = entity0.TBC_BANDEIRA_REDE
	            
	            	
	            select new TcsPerfilBandeiraRede()		
	            {
	            
                DescBandeiraRede = entity0Al1.DESC_BANDEIRA_REDE
                , IdBandeiraR = entity0Al1.ID_BANDEIRA_REDE
                , IdPerfil = entity0Al2.ID_PERFIL
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TcsPerfilFilialQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TcsPerfilFilialByEntitySearchNoAssociations.
	    public IQueryable<TcsPerfilFilial> GetTcsPerfilFilialByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTcsPerfilFilialByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilFilialQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsPerfilFilial));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsPerfilFilial> result = 
	            (from entity0 in this.DbContext.TCS_PERFIL_FILIAL.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TBC_FILIAL
                  let entity0Al2 = entity0.TCS_PERFIL
	            
	            	
	            select new TcsPerfilFilial()		
	            {
	            
                CodigoFilial = entity0Al1.CODIGO_FILIAL
                , IdFilialPfj = entity0Al1.ID_FILIAL_PFJ
                , IdPerfil = entity0Al2.ID_PERFIL
                , IdTcsPerfilFilial = entity0.ID_TCS_PERFIL_FILIAL
                , NomeFilial = entity0Al1.NOME_FILIAL
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TcsUsuarioPerfilQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TcsUsuarioPerfilParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioPerfilParentComposition> GetTcsUsuarioPerfilParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTcsUsuarioPerfilParentCompositionByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsUsuarioPerfilQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "TCS_PERFIL", "TCS_USUARIO_PERFIL", "TCS_PERFIL", typeof(TcsUsuarioPerfilParentComposition), typeof(TcsPerfilRegraModulo), typeof(TcsPerfilRegraTransacao), typeof(TcsPerfilBandeiraRede), typeof(TcsPerfilFilial));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioPerfilParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_PERFIL.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.TCS_PERFIL
                  let entity0Al1 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioPerfilParentComposition()		
	            {
	            
                IdLinx = entity0Al1.ID_LINX
                , IdPerfil = entity0Al2.ID_PERFIL
                , IdTcsUsuarioPerfil = entity0.ID_TCS_USUARIO_PERFIL
                , IdUsuario = entity0Al1.ID_USUARIO
                , NomeUsuario = entity0Al1.NOME_USUARIO
                , UidUsuario = entity0Al1.UID_USUARIO
                //TcsPerfil Properties.
                , DescPerfil = entity0.TCS_PERFIL.DESC_PERFIL
                , IdGpeconFranquia = entity0.TCS_PERFIL.TBC_GRUPO_ECONOMICO.ID_GPECON
                , IdPerfilOrigem = entity0.TCS_PERFIL.ID_PERFIL_ORIGEM
                , Inativo = entity0.TCS_PERFIL.INATIVO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TcsPerfilRegraModuloQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TcsPerfilRegraModuloParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsPerfilRegraModuloParentComposition> GetTcsPerfilRegraModuloParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTcsPerfilRegraModuloParentCompositionByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilRegraModuloQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "TCS_PERFIL", "TCS_PERFIL_REGRA_MODULO", "TCS_PERFIL", typeof(TcsPerfilRegraModuloParentComposition), typeof(TcsUsuarioPerfil), typeof(TcsPerfilRegraTransacao), typeof(TcsPerfilBandeiraRede), typeof(TcsPerfilFilial));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsPerfilRegraModuloParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_PERFIL_REGRA_MODULO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_PERFIL
                orderby entity0.ID_MODULO ascending
	            
	            	
	            select new TcsPerfilRegraModuloParentComposition()		
	            {
	            
                IdModulo = entity0.ID_MODULO
                , IdPerfil = entity0Al1.ID_PERFIL
                , IdPerfilRegraModulo = entity0.ID_PERFIL_REGRA_MODULO
                , LxRegraAcessoModulo = entity0.LX_REGRA_ACESSO_MODULO
                , LxRegraAcessoModuloName = ((entity0.LX_REGRA_ACESSO_MODULO) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_MODULO) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_MODULO) == 13 ? "Acesso por Transação" : ((entity0.LX_REGRA_ACESSO_MODULO) == 5 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 12 ? "Criar Pesquisa" : ((entity0.LX_REGRA_ACESSO_MODULO) == 10 ? "Criar Relatório" : ((entity0.LX_REGRA_ACESSO_MODULO) == 6 ? "Excluir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 9 ? "Exportar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 8 ? "Imprimir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 4 ? "Incluir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 11 ? "Layout" : ((entity0.LX_REGRA_ACESSO_MODULO) == 7 ? "Pesquisa Especial" : ((entity0.LX_REGRA_ACESSO_MODULO) == 3 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 99 ? "Regra Transação" : ""))))))))))))))
                , Acesso = ((entity0.LX_REGRA_ACESSO_MODULO) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_MODULO) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_MODULO) == 13 ? "Acesso por Transação" : ((entity0.LX_REGRA_ACESSO_MODULO) == 5 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 12 ? "Criar Pesquisa" : ((entity0.LX_REGRA_ACESSO_MODULO) == 10 ? "Criar Relatório" : ((entity0.LX_REGRA_ACESSO_MODULO) == 6 ? "Excluir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 9 ? "Exportar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 8 ? "Imprimir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 4 ? "Incluir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 11 ? "Layout" : ((entity0.LX_REGRA_ACESSO_MODULO) == 7 ? "Pesquisa Especial" : ((entity0.LX_REGRA_ACESSO_MODULO) == 3 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 99 ? "Regra Transação" : ""))))))))))))))
                //TcsPerfil Properties.
                , DescPerfil = entity0.TCS_PERFIL.DESC_PERFIL
                , IdGpeconFranquia = entity0.TCS_PERFIL.TBC_GRUPO_ECONOMICO.ID_GPECON
                , IdPerfilOrigem = entity0.TCS_PERFIL.ID_PERFIL_ORIGEM
                , Inativo = entity0.TCS_PERFIL.INATIVO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TcsPerfilRegraTransacaoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TcsPerfilRegraTransacaoParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsPerfilRegraTransacaoParentComposition> GetTcsPerfilRegraTransacaoParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTcsPerfilRegraTransacaoParentCompositionByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilRegraTransacaoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "TCS_PERFIL", "TCS_PERFIL_REGRA_TRANSACAO", "TCS_PERFIL", typeof(TcsPerfilRegraTransacaoParentComposition), typeof(TcsUsuarioPerfil), typeof(TcsPerfilRegraModulo), typeof(TcsPerfilBandeiraRede), typeof(TcsPerfilFilial));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsPerfilRegraTransacaoParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_PERFIL_REGRA_TRANSACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_PERFIL
                orderby entity0.ID_TRANSACAO ascending
	            
	            	
	            select new TcsPerfilRegraTransacaoParentComposition()		
	            {
	            
                Acesso = ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 13 ? "Acesso por Transação" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 5 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 12 ? "Criar Pesquisa" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 10 ? "Criar Relatório" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 6 ? "Excluir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 9 ? "Exportar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 8 ? "Imprimir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 4 ? "Incluir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 11 ? "Layout" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 7 ? "Pesquisa Especial" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 3 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 99 ? "Regra Transação" : ""))))))))))))))
                , IdPerfil = entity0Al1.ID_PERFIL
                , IdPerfilRegraTransacao = entity0.ID_PERFIL_REGRA_TRANSACAO
                , IdTransacao = entity0.ID_TRANSACAO
                , LxRegraAcessoTransacao = entity0.LX_REGRA_ACESSO_TRANSACAO
                , LxRegraAcessoTransacaoName = ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 13 ? "Acesso por Transação" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 5 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 12 ? "Criar Pesquisa" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 10 ? "Criar Relatório" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 6 ? "Excluir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 9 ? "Exportar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 8 ? "Imprimir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 4 ? "Incluir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 11 ? "Layout" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 7 ? "Pesquisa Especial" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 3 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 99 ? "Regra Transação" : ""))))))))))))))
                //TcsPerfil Properties.
                , DescPerfil = entity0.TCS_PERFIL.DESC_PERFIL
                , IdGpeconFranquia = entity0.TCS_PERFIL.TBC_GRUPO_ECONOMICO.ID_GPECON
                , IdPerfilOrigem = entity0.TCS_PERFIL.ID_PERFIL_ORIGEM
                , Inativo = entity0.TCS_PERFIL.INATIVO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TcsPerfilBandeiraRedeQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TcsPerfilBandeiraRedeParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsPerfilBandeiraRedeParentComposition> GetTcsPerfilBandeiraRedeParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTcsPerfilBandeiraRedeParentCompositionByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilBandeiraRedeQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "TCS_PERFIL", "TCS_PERFIL_BANDEIRA_REDE", "TCS_PERFIL", typeof(TcsPerfilBandeiraRedeParentComposition), typeof(TcsUsuarioPerfil), typeof(TcsPerfilRegraModulo), typeof(TcsPerfilRegraTransacao), typeof(TcsPerfilFilial));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsPerfilBandeiraRedeParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_PERFIL_BANDEIRA_REDE.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.TCS_PERFIL
                  let entity0Al1 = entity0.TBC_BANDEIRA_REDE
	            
	            	
	            select new TcsPerfilBandeiraRedeParentComposition()		
	            {
	            
                DescBandeiraRede = entity0Al1.DESC_BANDEIRA_REDE
                , IdBandeiraR = entity0Al1.ID_BANDEIRA_REDE
                , IdPerfil = entity0Al2.ID_PERFIL
                //TcsPerfil Properties.
                , DescPerfil = entity0.TCS_PERFIL.DESC_PERFIL
                , IdGpeconFranquia = entity0.TCS_PERFIL.TBC_GRUPO_ECONOMICO.ID_GPECON
                , IdPerfilOrigem = entity0.TCS_PERFIL.ID_PERFIL_ORIGEM
                , Inativo = entity0.TCS_PERFIL.INATIVO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TcsPerfilFilialQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TcsPerfilFilialParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsPerfilFilialParentComposition> GetTcsPerfilFilialParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTcsPerfilFilialParentCompositionByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilFilialQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "TCS_PERFIL", "TCS_PERFIL_FILIAL", "TCS_PERFIL", typeof(TcsPerfilFilialParentComposition), typeof(TcsUsuarioPerfil), typeof(TcsPerfilRegraModulo), typeof(TcsPerfilRegraTransacao), typeof(TcsPerfilBandeiraRede));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsPerfilFilialParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_PERFIL_FILIAL.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TBC_FILIAL
                  let entity0Al2 = entity0.TCS_PERFIL
	            
	            	
	            select new TcsPerfilFilialParentComposition()		
	            {
	            
                CodigoFilial = entity0Al1.CODIGO_FILIAL
                , IdFilialPfj = entity0Al1.ID_FILIAL_PFJ
                , IdPerfil = entity0Al2.ID_PERFIL
                , IdTcsPerfilFilial = entity0.ID_TCS_PERFIL_FILIAL
                , NomeFilial = entity0Al1.NOME_FILIAL
                //TcsPerfil Properties.
                , DescPerfil = entity0.TCS_PERFIL.DESC_PERFIL
                , IdGpeconFranquia = entity0.TCS_PERFIL.TBC_GRUPO_ECONOMICO.ID_GPECON
                , IdPerfilOrigem = entity0.TCS_PERFIL.ID_PERFIL_ORIGEM
                , Inativo = entity0.TCS_PERFIL.INATIVO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
	
	    [Ignore()]
	    private void SetTcsPerfilRegraModuloBusinessFilter(ref IQueryable<TcsPerfilRegraModulo> query, List<EntitySearch> entitySearchList)
	    {
	    		int idxElement;
	    		string operatorValue;
	    		object value;
	    		//Get query by functions
	    		if (entitySearchList.Count > 0)
	    		{
	    			foreach (EntitySearch search in entitySearchList.Where(e => e.EntityName == "TcsPerfilRegraModulo"))
	    			{

	
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "Acesso" || e.Value.ToString() == "((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 1 ? 'Acesso Bloqueado' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 2 ? 'Acesso Total' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 13 ? 'Acesso por Transação' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 5 ? 'Alterar' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 12 ? 'Criar Pesquisa' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 10 ? 'Criar Relatório' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 6 ? 'Excluir' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 9 ? 'Exportar' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 8 ? 'Imprimir' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 4 ? 'Incluir' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 11 ? 'Layout' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 7 ? 'Pesquisa Especial' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 3 ? 'Pesquisar' : ((TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO) = 99 ? 'Regra Transação' : ''))))))))))))))")))
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

	
	    								//Adjust Like operator
	    								if (operatorValue == "Like")
	    								{
	    								    string enteredVal = value.ToString();
	    								    if (enteredVal.Right(1) == "%" && enteredVal.Left(1) == "%")
	    								    {
	    								        enteredVal = enteredVal.Replace("%", "");
	    								        operatorValue = "Contains";
	    								    }
	    								    else if (enteredVal.Left(1) == "%")
	    								    {
	    								        enteredVal = enteredVal.Replace("%", "");
	    								        operatorValue = "EndsWith";
	    								    }
	    								    else
	    								    {
	    								        enteredVal = enteredVal.Replace("%", "");
	    								        operatorValue = "StartsWith";
	    								    }
	    								    value = enteredVal;
	    								}

	
	    								switch (operatorValue)
	    								{
	    									case "==":
	    										string tmpAcesso1 = (string)value;
	    										query = from r in query where r.Acesso == tmpAcesso1 select r;
	    										break;
	    									case "!=":
	    										string tmpAcesso2 = (string)value;
	    										query = from r in query where r.Acesso != tmpAcesso2 select r;
	    										break;

	
	    									case "Contains":
	    										string tmpAcesso7 = (string)value;
	    									    query = from r in query where r.Acesso.Contains(tmpAcesso7) select r;
	    									    break;
	    									case "StartsWith":
	    										string tmpAcesso8 = (string)value;
	    									    query = from r in query where r.Acesso.StartsWith(tmpAcesso8) select r;
	    									    break;
	    									case "EndsWith":
	    										string tmpAcesso9 = (string)value;
	    									    query = from r in query where r.Acesso.EndsWith(tmpAcesso9) select r;
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
	    private void SetTcsPerfilRegraTransacaoBusinessFilter(ref IQueryable<TcsPerfilRegraTransacao> query, List<EntitySearch> entitySearchList)
	    {
	    		int idxElement;
	    		string operatorValue;
	    		object value;
	    		//Get query by functions
	    		if (entitySearchList.Count > 0)
	    		{
	    			foreach (EntitySearch search in entitySearchList.Where(e => e.EntityName == "TcsPerfilRegraTransacao"))
	    			{

	
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "Acesso" || e.Value.ToString() == "((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 1 ? 'Acesso Bloqueado' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 2 ? 'Acesso Total' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 13 ? 'Acesso por Transação' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 5 ? 'Alterar' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 12 ? 'Criar Pesquisa' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 10 ? 'Criar Relatório' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 6 ? 'Excluir' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 9 ? 'Exportar' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 8 ? 'Imprimir' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 4 ? 'Incluir' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 11 ? 'Layout' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 7 ? 'Pesquisa Especial' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 3 ? 'Pesquisar' : ((TCS_PERFIL_REGRA_TRANSACAO.LX_REGRA_ACESSO_TRANSACAO) = 99 ? 'Regra Transação' : ''))))))))))))))")))
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

	
	    								//Adjust Like operator
	    								if (operatorValue == "Like")
	    								{
	    								    string enteredVal = value.ToString();
	    								    if (enteredVal.Right(1) == "%" && enteredVal.Left(1) == "%")
	    								    {
	    								        enteredVal = enteredVal.Replace("%", "");
	    								        operatorValue = "Contains";
	    								    }
	    								    else if (enteredVal.Left(1) == "%")
	    								    {
	    								        enteredVal = enteredVal.Replace("%", "");
	    								        operatorValue = "EndsWith";
	    								    }
	    								    else
	    								    {
	    								        enteredVal = enteredVal.Replace("%", "");
	    								        operatorValue = "StartsWith";
	    								    }
	    								    value = enteredVal;
	    								}

	
	    								switch (operatorValue)
	    								{
	    									case "==":
	    										string tmpAcesso1 = (string)value;
	    										query = from r in query where r.Acesso == tmpAcesso1 select r;
	    										break;
	    									case "!=":
	    										string tmpAcesso2 = (string)value;
	    										query = from r in query where r.Acesso != tmpAcesso2 select r;
	    										break;

	
	    									case "Contains":
	    										string tmpAcesso7 = (string)value;
	    									    query = from r in query where r.Acesso.Contains(tmpAcesso7) select r;
	    									    break;
	    									case "StartsWith":
	    										string tmpAcesso8 = (string)value;
	    									    query = from r in query where r.Acesso.StartsWith(tmpAcesso8) select r;
	    									    break;
	    									case "EndsWith":
	    										string tmpAcesso9 = (string)value;
	    									    query = from r in query where r.Acesso.EndsWith(tmpAcesso9) select r;
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


		
	
	    [TbcFilialQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TbcFilialByEntitySearch.
	    public IQueryable<TbcFilial> GetTbcFilialByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTbcFilialByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new TbcFilialQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TbcFilial));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TbcFilial> result = 
	            (from entity0 in this.DbContext.TBC_FILIAL.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TbcFilial()		
	            {
	            
                CodigoFilial = entity0.CODIGO_FILIAL
                , IdFilialPfj = entity0.ID_FILIAL_PFJ
                , NomeFilial = entity0.NOME_FILIAL
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TbcFilialQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TbcFilialByEntitySearchNoAssociations.
	    public IQueryable<TbcFilial> GetTbcFilialByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTbcFilialByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new TbcFilialQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TbcFilial));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TbcFilial> result = 
	            (from entity0 in this.DbContext.TBC_FILIAL.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TbcFilial()		
	            {
	            
                CodigoFilial = entity0.CODIGO_FILIAL
                , IdFilialPfj = entity0.ID_FILIAL_PFJ
                , NomeFilial = entity0.NOME_FILIAL
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [SyncInfoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get SyncInfoByEntitySearch.
	    public IEnumerable<SyncInfo> GetSyncInfoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetSyncInfoByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new SyncInfoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<SyncInfo> result = new List<SyncInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    [SyncInfoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get SyncInfoByEntitySearchNoAssociations.
	    public IEnumerable<SyncInfo> GetSyncInfoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetSyncInfoByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new SyncInfoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<SyncInfo> result = new List<SyncInfo>();
	  	
	
	        	

	
	        return result;
	    }
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    [TcsPerfilQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedTcsPerfil.
	    public IQueryable<TcsPerfil> GetPagedTcsPerfil(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedTcsPerfil")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsPerfil));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsPerfil> result = 
	            (from entity0 in this.DbContext.TCS_PERFIL.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TBC_GRUPO_ECONOMICO
                orderby entity0.ID_PERFIL ascending
	            
	            	
	            select new TcsPerfil()		
	            {
	            
                DescPerfil = entity0.DESC_PERFIL
                , IdGpeconFranquia = entity0Al1.ID_GPECON
                , IdPerfil = entity0.ID_PERFIL
                , IdPerfilOrigem = entity0.ID_PERFIL_ORIGEM
                , Inativo = entity0.INATIVO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        TcsPerfil.OnSearching(ref result, true, entitySearchList);	

	
	        return result;
	    }
			
	
	    [TcsUsuarioPerfilQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedTcsUsuarioPerfil.
	    public IQueryable<TcsUsuarioPerfil> GetPagedTcsUsuarioPerfil(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedTcsUsuarioPerfil")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsUsuarioPerfilQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioPerfil));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioPerfil> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_PERFIL.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.TCS_PERFIL
                  let entity0Al1 = entity0.TCS_USUARIO
                orderby entity0.ID_TCS_USUARIO_PERFIL ascending
	            
	            	
	            select new TcsUsuarioPerfil()		
	            {
	            
                IdLinx = entity0Al1.ID_LINX
                , IdPerfil = entity0Al2.ID_PERFIL
                , IdTcsUsuarioPerfil = entity0.ID_TCS_USUARIO_PERFIL
                , IdUsuario = entity0Al1.ID_USUARIO
                , NomeUsuario = entity0Al1.NOME_USUARIO
                , UidUsuario = entity0Al1.UID_USUARIO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    [TcsPerfilRegraModuloQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedTcsPerfilRegraModulo.
	    public IQueryable<TcsPerfilRegraModulo> GetPagedTcsPerfilRegraModulo(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedTcsPerfilRegraModulo")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilRegraModuloQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsPerfilRegraModulo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsPerfilRegraModulo> result = 
	            (from entity0 in this.DbContext.TCS_PERFIL_REGRA_MODULO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_PERFIL
                orderby entity0.ID_PERFIL_REGRA_MODULO ascending
	            
	            	
	            select new TcsPerfilRegraModulo()		
	            {
	            
                IdModulo = entity0.ID_MODULO
                , IdPerfil = entity0Al1.ID_PERFIL
                , IdPerfilRegraModulo = entity0.ID_PERFIL_REGRA_MODULO
                , LxRegraAcessoModulo = entity0.LX_REGRA_ACESSO_MODULO
                , LxRegraAcessoModuloName = ((entity0.LX_REGRA_ACESSO_MODULO) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_MODULO) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_MODULO) == 13 ? "Acesso por Transação" : ((entity0.LX_REGRA_ACESSO_MODULO) == 5 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 12 ? "Criar Pesquisa" : ((entity0.LX_REGRA_ACESSO_MODULO) == 10 ? "Criar Relatório" : ((entity0.LX_REGRA_ACESSO_MODULO) == 6 ? "Excluir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 9 ? "Exportar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 8 ? "Imprimir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 4 ? "Incluir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 11 ? "Layout" : ((entity0.LX_REGRA_ACESSO_MODULO) == 7 ? "Pesquisa Especial" : ((entity0.LX_REGRA_ACESSO_MODULO) == 3 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 99 ? "Regra Transação" : ""))))))))))))))
                , Acesso = ((entity0.LX_REGRA_ACESSO_MODULO) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_MODULO) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_MODULO) == 13 ? "Acesso por Transação" : ((entity0.LX_REGRA_ACESSO_MODULO) == 5 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 12 ? "Criar Pesquisa" : ((entity0.LX_REGRA_ACESSO_MODULO) == 10 ? "Criar Relatório" : ((entity0.LX_REGRA_ACESSO_MODULO) == 6 ? "Excluir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 9 ? "Exportar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 8 ? "Imprimir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 4 ? "Incluir" : ((entity0.LX_REGRA_ACESSO_MODULO) == 11 ? "Layout" : ((entity0.LX_REGRA_ACESSO_MODULO) == 7 ? "Pesquisa Especial" : ((entity0.LX_REGRA_ACESSO_MODULO) == 3 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_MODULO) == 99 ? "Regra Transação" : ""))))))))))))))
		
	            }
	            ).Skip(skip).Take(take);
	
	        SetTcsPerfilRegraModuloBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    [TcsPerfilRegraTransacaoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedTcsPerfilRegraTransacao.
	    public IQueryable<TcsPerfilRegraTransacao> GetPagedTcsPerfilRegraTransacao(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedTcsPerfilRegraTransacao")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilRegraTransacaoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsPerfilRegraTransacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsPerfilRegraTransacao> result = 
	            (from entity0 in this.DbContext.TCS_PERFIL_REGRA_TRANSACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_PERFIL
                orderby entity0.ID_PERFIL_REGRA_TRANSACAO ascending
	            
	            	
	            select new TcsPerfilRegraTransacao()		
	            {
	            
                Acesso = ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 13 ? "Acesso por Transação" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 5 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 12 ? "Criar Pesquisa" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 10 ? "Criar Relatório" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 6 ? "Excluir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 9 ? "Exportar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 8 ? "Imprimir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 4 ? "Incluir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 11 ? "Layout" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 7 ? "Pesquisa Especial" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 3 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 99 ? "Regra Transação" : ""))))))))))))))
                , IdPerfil = entity0Al1.ID_PERFIL
                , IdPerfilRegraTransacao = entity0.ID_PERFIL_REGRA_TRANSACAO
                , IdTransacao = entity0.ID_TRANSACAO
                , LxRegraAcessoTransacao = entity0.LX_REGRA_ACESSO_TRANSACAO
                , LxRegraAcessoTransacaoName = ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 1 ? "Acesso Bloqueado" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 2 ? "Acesso Total" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 13 ? "Acesso por Transação" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 5 ? "Alterar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 12 ? "Criar Pesquisa" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 10 ? "Criar Relatório" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 6 ? "Excluir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 9 ? "Exportar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 8 ? "Imprimir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 4 ? "Incluir" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 11 ? "Layout" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 7 ? "Pesquisa Especial" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 3 ? "Pesquisar" : ((entity0.LX_REGRA_ACESSO_TRANSACAO) == 99 ? "Regra Transação" : ""))))))))))))))
		
	            }
	            ).Skip(skip).Take(take);
	
	        SetTcsPerfilRegraTransacaoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    [TcsPerfilBandeiraRedeQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedTcsPerfilBandeiraRede.
	    public IQueryable<TcsPerfilBandeiraRede> GetPagedTcsPerfilBandeiraRede(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedTcsPerfilBandeiraRede")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilBandeiraRedeQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsPerfilBandeiraRede));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsPerfilBandeiraRede> result = 
	            (from entity0 in this.DbContext.TCS_PERFIL_BANDEIRA_REDE.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.TCS_PERFIL
                  let entity0Al1 = entity0.TBC_BANDEIRA_REDE
                orderby entity0Al1.ID_BANDEIRA_REDE ascending, entity0Al2.ID_PERFIL ascending
	            
	            	
	            select new TcsPerfilBandeiraRede()		
	            {
	            
                DescBandeiraRede = entity0Al1.DESC_BANDEIRA_REDE
                , IdBandeiraR = entity0Al1.ID_BANDEIRA_REDE
                , IdPerfil = entity0Al2.ID_PERFIL
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    [TcsPerfilFilialQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedTcsPerfilFilial.
	    public IQueryable<TcsPerfilFilial> GetPagedTcsPerfilFilial(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedTcsPerfilFilial")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilFilialQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsPerfilFilial));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsPerfilFilial> result = 
	            (from entity0 in this.DbContext.TCS_PERFIL_FILIAL.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TBC_FILIAL
                  let entity0Al2 = entity0.TCS_PERFIL
                orderby entity0.ID_TCS_PERFIL_FILIAL ascending
	            
	            	
	            select new TcsPerfilFilial()		
	            {
	            
                CodigoFilial = entity0Al1.CODIGO_FILIAL
                , IdFilialPfj = entity0Al1.ID_FILIAL_PFJ
                , IdPerfil = entity0Al2.ID_PERFIL
                , IdTcsPerfilFilial = entity0.ID_TCS_PERFIL_FILIAL
                , NomeFilial = entity0Al1.NOME_FILIAL
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsPerfilCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsPerfil));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_PERFIL.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TBC_GRUPO_ECONOMICO
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsUsuarioPerfilCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioPerfil));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_USUARIO_PERFIL.Where(dynQuery, parameters.ToArray())
                  let entityAl2 = entity.TCS_PERFIL
                  let entityAl1 = entity.TCS_USUARIO
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsPerfilRegraModuloCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsPerfilRegraModulo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_PERFIL_REGRA_MODULO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_PERFIL
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsPerfilRegraTransacaoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsPerfilRegraTransacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_PERFIL_REGRA_TRANSACAO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_PERFIL
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsPerfilBandeiraRedeCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsPerfilBandeiraRede));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_PERFIL_BANDEIRA_REDE.Where(dynQuery, parameters.ToArray())
                  let entityAl2 = entity.TCS_PERFIL
                  let entityAl1 = entity.TBC_BANDEIRA_REDE
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsPerfilFilialCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsPerfilFilial));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_PERFIL_FILIAL.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TBC_FILIAL
                  let entityAl2 = entity.TCS_PERFIL
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    [TbcFilialQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedTbcFilial.
	    public IQueryable<TbcFilial> GetPagedTbcFilial(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedTbcFilial")))
 	        {
 	             AuthorizationResult authorizationResult = (new TbcFilialQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TbcFilial));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TbcFilial> result = 
	            (from entity0 in this.DbContext.TBC_FILIAL.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_FILIAL_PFJ ascending
	            
	            	
	            select new TbcFilial()		
	            {
	            
                CodigoFilial = entity0.CODIGO_FILIAL
                , IdFilialPfj = entity0.ID_FILIAL_PFJ
                , NomeFilial = entity0.NOME_FILIAL
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTbcFilialCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TbcFilial));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TBC_FILIAL.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    [SyncInfoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedSyncInfo.
	    public IEnumerable<SyncInfo> GetPagedSyncInfo(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedSyncInfo")))
 	        {
 	             AuthorizationResult authorizationResult = (new SyncInfoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<SyncInfo> result = new List<SyncInfo>();
	  	
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetSyncInfoCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    [TcsPerfilUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update TcsPerfil.
	    public void UpdateTcsPerfil(TcsPerfil entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateTcsPerfil")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    [TcsPerfilInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert TcsPerfil.
	    public void InsertTcsPerfil(TcsPerfil entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertTcsPerfil")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    [TcsPerfilDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete TcsPerfil.
	    public void DeleteTcsPerfil(TcsPerfil entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteTcsPerfil")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    [TcsUsuarioPerfilUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update TcsUsuarioPerfil.
	    public void UpdateTcsUsuarioPerfil(TcsUsuarioPerfil entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateTcsUsuarioPerfil")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsUsuarioPerfilUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.TcsPerfil.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsPerfil) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsPerfil); 	
	            

	
	        }
	
	    }

	    [TcsUsuarioPerfilInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert TcsUsuarioPerfil.
	    public void InsertTcsUsuarioPerfil(TcsUsuarioPerfil entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertTcsUsuarioPerfil")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsUsuarioPerfilInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.TcsPerfil.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsPerfil) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsPerfil);
	            

	
	        }
	
	    }

	    [TcsUsuarioPerfilDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete TcsUsuarioPerfil.
	    public void DeleteTcsUsuarioPerfil(TcsUsuarioPerfil entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteTcsUsuarioPerfil")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsUsuarioPerfilDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.TcsPerfil.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsPerfil) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsPerfil);
	            

	
	        }

	
	    }
		
			
	    [TcsPerfilRegraModuloUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update TcsPerfilRegraModulo.
	    public void UpdateTcsPerfilRegraModulo(TcsPerfilRegraModulo entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateTcsPerfilRegraModulo")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilRegraModuloUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.TcsPerfil.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsPerfil) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsPerfil); 	
	            

	
	        }
	
	    }

	    [TcsPerfilRegraModuloInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert TcsPerfilRegraModulo.
	    public void InsertTcsPerfilRegraModulo(TcsPerfilRegraModulo entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertTcsPerfilRegraModulo")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilRegraModuloInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.TcsPerfil.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsPerfil) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsPerfil);
	            

	
	        }
	
	    }

	    [TcsPerfilRegraModuloDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete TcsPerfilRegraModulo.
	    public void DeleteTcsPerfilRegraModulo(TcsPerfilRegraModulo entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteTcsPerfilRegraModulo")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilRegraModuloDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.TcsPerfil.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsPerfil) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsPerfil);
	            

	
	        }

	
	    }
		
			
	    [TcsPerfilRegraTransacaoUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update TcsPerfilRegraTransacao.
	    public void UpdateTcsPerfilRegraTransacao(TcsPerfilRegraTransacao entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateTcsPerfilRegraTransacao")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilRegraTransacaoUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.TcsPerfil.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsPerfil) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsPerfil); 	
	            

	
	        }
	
	    }

	    [TcsPerfilRegraTransacaoInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert TcsPerfilRegraTransacao.
	    public void InsertTcsPerfilRegraTransacao(TcsPerfilRegraTransacao entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertTcsPerfilRegraTransacao")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilRegraTransacaoInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.TcsPerfil.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsPerfil) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsPerfil);
	            

	
	        }
	
	    }

	    [TcsPerfilRegraTransacaoDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete TcsPerfilRegraTransacao.
	    public void DeleteTcsPerfilRegraTransacao(TcsPerfilRegraTransacao entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteTcsPerfilRegraTransacao")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilRegraTransacaoDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.TcsPerfil.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsPerfil) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsPerfil);
	            

	
	        }

	
	    }
		
			
	    [TcsPerfilBandeiraRedeUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update TcsPerfilBandeiraRede.
	    public void UpdateTcsPerfilBandeiraRede(TcsPerfilBandeiraRede entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateTcsPerfilBandeiraRede")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilBandeiraRedeUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.TcsPerfil.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsPerfil) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsPerfil); 	
	            

	
	        }
	
	    }

	    [TcsPerfilBandeiraRedeInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert TcsPerfilBandeiraRede.
	    public void InsertTcsPerfilBandeiraRede(TcsPerfilBandeiraRede entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertTcsPerfilBandeiraRede")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilBandeiraRedeInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.TcsPerfil.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsPerfil) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsPerfil);
	            

	
	        }
	
	    }

	    [TcsPerfilBandeiraRedeDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete TcsPerfilBandeiraRede.
	    public void DeleteTcsPerfilBandeiraRede(TcsPerfilBandeiraRede entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteTcsPerfilBandeiraRede")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilBandeiraRedeDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.TcsPerfil.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsPerfil) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsPerfil);
	            

	
	        }

	
	    }
		
			
	    [TcsPerfilFilialUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update TcsPerfilFilial.
	    public void UpdateTcsPerfilFilial(TcsPerfilFilial entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateTcsPerfilFilial")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilFilialUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.TcsPerfil.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsPerfil) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsPerfil); 	
	            

	
	        }
	
	    }

	    [TcsPerfilFilialInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert TcsPerfilFilial.
	    public void InsertTcsPerfilFilial(TcsPerfilFilial entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertTcsPerfilFilial")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilFilialInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.TcsPerfil.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsPerfil) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsPerfil);
	            

	
	        }
	
	    }

	    [TcsPerfilFilialDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete TcsPerfilFilial.
	    public void DeleteTcsPerfilFilial(TcsPerfilFilial entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteTcsPerfilFilial")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsPerfilFilialDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.TcsPerfil.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsPerfil) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsPerfil);
	            

	
	        }

	
	    }
		
			
	    [TbcFilialUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update TbcFilial.
	    public void UpdateTbcFilial(TbcFilial entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateTbcFilial")))
 	        {
 	             AuthorizationResult authorizationResult = (new TbcFilialUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    [TbcFilialInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert TbcFilial.
	    public void InsertTbcFilial(TbcFilial entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertTbcFilial")))
 	        {
 	             AuthorizationResult authorizationResult = (new TbcFilialInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    [TbcFilialDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete TbcFilial.
	    public void DeleteTbcFilial(TbcFilial entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteTbcFilial")))
 	        {
 	             AuthorizationResult authorizationResult = (new TbcFilialDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    [SyncInfoUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update SyncInfo.
	    public void UpdateSyncInfo(SyncInfo entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateSyncInfo")))
 	        {
 	             AuthorizationResult authorizationResult = (new SyncInfoUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	    }

	    [SyncInfoInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert SyncInfo.
	    public void InsertSyncInfo(SyncInfo entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertSyncInfo")))
 	        {
 	             AuthorizationResult authorizationResult = (new SyncInfoInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	    }

	    [SyncInfoDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete SyncInfo.
	    public void DeleteSyncInfo(SyncInfo entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteSyncInfo")))
 	        {
 	             AuthorizationResult authorizationResult = (new SyncInfoDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}