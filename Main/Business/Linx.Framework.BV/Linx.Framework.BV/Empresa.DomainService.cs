					
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

namespace Linx.Framework.BV.Empresa
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_EMPRESA_AUTENTICACAO.ID_LINX", IsUpdatable=true, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsEmpresaAutenticacao,TcsEmpresaAutenticacao.TcsEmpresaGpecon,TcsEmpresaAutenticacao.TcsAmbiente,TcsEmpresaAutenticacao.TcsEmpresaModulo,TcsEmpresaAutenticacao.TcsUsuarioAutenticacao];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[];EdmEntityName[TCS_EMPRESA_AUTENTICACAO];EntityRelations[];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsEmpresaAutenticacao")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Empresa.TcsEmpresaAutenticacao")]
	public partial class TcsEmpresaAutenticacao : Linx.Data.Entity
	{

	
		
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.TcsEmpresaGpeconList != null && this.TcsEmpresaGpeconList.Count() > 0)
	      {
	         foreach (var entity in this.TcsEmpresaGpeconList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      if (this.TcsAmbienteList != null && this.TcsAmbienteList.Count() > 0)
	      {
	         foreach (var entity in this.TcsAmbienteList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      if (this.TcsEmpresaModuloList != null && this.TcsEmpresaModuloList.Count() > 0)
	      {
	         foreach (var entity in this.TcsEmpresaModuloList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      if (this.TcsUsuarioAutenticacaoList != null && this.TcsUsuarioAutenticacaoList.Count() > 0)
	      {
	         foreach (var entity in this.TcsUsuarioAutenticacaoList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.TcsEmpresaGpeconList != null)
	      {
	         foreach (var detail in this.TcsEmpresaGpeconList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsEmpresaGpeconList = null;
	      }
	      if (this.TcsAmbienteList != null)
	      {
	         foreach (var detail in this.TcsAmbienteList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsAmbienteList = null;
	      }
	      if (this.TcsEmpresaModuloList != null)
	      {
	         foreach (var detail in this.TcsEmpresaModuloList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsEmpresaModuloList = null;
	      }
	      if (this.TcsUsuarioAutenticacaoList != null)
	      {
	         foreach (var detail in this.TcsUsuarioAutenticacaoList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsUsuarioAutenticacaoList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(EmpresaDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("TcsEmpresaGpecon"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsEmpresaGpecon");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdLinx"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdLinx));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsEmpresaGpecon and all sub-details
	         if (this.TcsEmpresaGpeconList == null || this.TcsEmpresaGpeconList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsEmpresaGpeconList = context.GetPagedTcsEmpresaGpecon(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsEmpresaGpeconList = (from r in context.GetTcsEmpresaGpeconByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	      if (viewNames == null || viewNames.Contains("TcsAmbiente"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsAmbiente");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdLinx"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdLinx));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsAmbiente and all sub-details
	         if (this.TcsAmbienteList == null || this.TcsAmbienteList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsAmbienteList = context.GetPagedTcsAmbiente(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsAmbienteList = (from r in context.GetTcsAmbienteByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	      if (viewNames == null || viewNames.Contains("TcsEmpresaModulo"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsEmpresaModulo");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdLinx"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdLinx));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsEmpresaModulo and all sub-details
	         if (this.TcsEmpresaModuloList == null || this.TcsEmpresaModuloList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsEmpresaModuloList = context.GetPagedTcsEmpresaModulo(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsEmpresaModuloList = (from r in context.GetTcsEmpresaModuloByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	      if (viewNames == null || viewNames.Contains("TcsUsuarioAutenticacao"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsUsuarioAutenticacao");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdLinx"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdLinx));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsUsuarioAutenticacao and all sub-details
	         if (this.TcsUsuarioAutenticacaoList == null || this.TcsUsuarioAutenticacaoList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsUsuarioAutenticacaoList = context.GetPagedTcsUsuarioAutenticacao(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsUsuarioAutenticacaoList = (from r in context.GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _TcsEmpresaGpeconElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsEmpresaGpecon && ((TcsEmpresaGpecon)e.Entity).TcsEmpresaAutenticacao == null && e.Associations == null && e.OriginalAssociations == null && ((TcsEmpresaGpecon)e.Entity).IdLinx == this.IdLinx).ToList();
 	      if (_TcsEmpresaGpeconElements.Count > 0 && this.TcsEmpresaGpeconList.Count() == 0)
 	      {
 	          this.TcsEmpresaGpeconList = _TcsEmpresaGpeconElements.Select(e => (TcsEmpresaGpecon)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsEmpresaGpeconElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsEmpresaGpecon)detail.Entity).TcsEmpresaAutenticacao = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsEmpresaAutenticacao", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsEmpresaGpeconList", indexDetails.ToArray());
 	      }
 
 	      var _TcsAmbienteElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsAmbiente && ((TcsAmbiente)e.Entity).TcsEmpresaAutenticacao == null && e.Associations == null && e.OriginalAssociations == null && ((TcsAmbiente)e.Entity).IdLinx == this.IdLinx).ToList();
 	      if (_TcsAmbienteElements.Count > 0 && this.TcsAmbienteList.Count() == 0)
 	      {
 	          this.TcsAmbienteList = _TcsAmbienteElements.Select(e => (TcsAmbiente)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsAmbienteElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsAmbiente)detail.Entity).TcsEmpresaAutenticacao = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsEmpresaAutenticacao", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsAmbienteList", indexDetails.ToArray());
 	      }
 
 	      var _TcsEmpresaModuloElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsEmpresaModulo && ((TcsEmpresaModulo)e.Entity).TcsEmpresaAutenticacao == null && e.Associations == null && e.OriginalAssociations == null && ((TcsEmpresaModulo)e.Entity).IdLinx == this.IdLinx).ToList();
 	      if (_TcsEmpresaModuloElements.Count > 0 && this.TcsEmpresaModuloList.Count() == 0)
 	      {
 	          this.TcsEmpresaModuloList = _TcsEmpresaModuloElements.Select(e => (TcsEmpresaModulo)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsEmpresaModuloElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsEmpresaModulo)detail.Entity).TcsEmpresaAutenticacao = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsEmpresaAutenticacao", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsEmpresaModuloList", indexDetails.ToArray());
 	      }
 
 	      var _TcsUsuarioAutenticacaoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioAutenticacao && ((TcsUsuarioAutenticacao)e.Entity).TcsEmpresaAutenticacao == null && e.Associations == null && e.OriginalAssociations == null && ((TcsUsuarioAutenticacao)e.Entity).IdLinx == this.IdLinx).ToList();
 	      if (_TcsUsuarioAutenticacaoElements.Count > 0 && this.TcsUsuarioAutenticacaoList.Count() == 0)
 	      {
 	          this.TcsUsuarioAutenticacaoList = _TcsUsuarioAutenticacaoElements.Select(e => (TcsUsuarioAutenticacao)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsUsuarioAutenticacaoElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsUsuarioAutenticacao)detail.Entity).TcsEmpresaAutenticacao = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsEmpresaAutenticacao", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsUsuarioAutenticacaoList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For CnpjCpf
	    partial void OnCnpjCpfChanging(string value);
	    partial void OnCnpjCpfChanged();

	    private string _CnpjCpf;

	    [DataMember(IsRequired = true, Name = "CnpjCpf", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cnpj", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[##.###.###/####-##];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_EMPRESA_AUTENTICACAO.CNPJ_CPF];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_EMPRESA_AUTENTICACAO.CNPJ_CPF")]
	    public string CnpjCpf
	    {
	    	    get
	    	    {
	    	          return _CnpjCpf;
	    	    }
	    	    set
	    	    {
	    	          if (this._CnpjCpf != value)
	    	          {
	    	              this.ValidateProperty("CnpjCpf", value);
	    	              this.OnCnpjCpfChanging(value);
	    	              this.RaiseDataMemberChanging("CnpjCpf");
	    	              this._CnpjCpf = value;
	    	              this.RaiseDataMemberChanged("CnpjCpf");
	    	              this.OnCnpjCpfChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLinx
	    partial void OnIdLinxChanging(int value);
	    partial void OnIdLinxChanged();

	    private int _IdLinx;

	    [DataMember(IsRequired = true, Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Linx", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
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
	    //Extensibility Partial Method Definitions For NomeEmpresa
	    partial void OnNomeEmpresaChanging(string value);
	    partial void OnNomeEmpresaChanged();

	    private string _NomeEmpresa;

	    [DataMember(IsRequired = true, Name = "NomeEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Empresa", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
	    public string NomeEmpresa
	    {
	    	    get
	    	    {
	    	          return _NomeEmpresa;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeEmpresa != value)
	    	          {
	    	              this.ValidateProperty("NomeEmpresa", value);
	    	              this.OnNomeEmpresaChanging(value);
	    	              this.RaiseDataMemberChanging("NomeEmpresa");
	    	              this._NomeEmpresa = value;
	    	              this.RaiseDataMemberChanged("NomeEmpresa");
	    	              this.OnNomeEmpresaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidEmpresa
	    partial void OnUidEmpresaChanging(Guid value);
	    partial void OnUidEmpresaChanged();

	    private Guid _UidEmpresa;

	    [DataMember(Name = "UidEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Empresa", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA")]
	    public Guid UidEmpresa
	    {
	    	    get
	    	    {
	    	          return _UidEmpresa;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidEmpresa != value)
	    	          {
	    	              this.ValidateProperty("UidEmpresa", value);
	    	              this.OnUidEmpresaChanging(value);
	    	              this.RaiseDataMemberChanging("UidEmpresa");
	    	              this._UidEmpresa = value;
	    	              this.RaiseDataMemberChanged("UidEmpresa");
	    	              this.OnUidEmpresaChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<TcsAmbiente> _TcsAmbienteList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsEmpresaAutenticacao_TcsAmbiente", "IdLinx", "IdLinx", IsForeignKey=false)]
	    [DataMember(Name = "TcsAmbienteList", EmitDefaultValue = true)]
	    public IEnumerable<TcsAmbiente> TcsAmbienteList
	    {
	        get
	        {
	
	            if (this._TcsAmbienteList == null)
	            	this._TcsAmbienteList = new List<TcsAmbiente>();
	
	            return this._TcsAmbienteList;
	        }
	        set
	        {
	            if (this._TcsAmbienteList != value)
	            {
	                this._TcsAmbienteList = value;
	                this.RaisePropertyChanged("TcsAmbienteList");
	            }
	        }
	    }	 
		
	    private IEnumerable<TcsEmpresaGpecon> _TcsEmpresaGpeconList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsEmpresaAutenticacao_TcsEmpresaGpecon", "IdLinx", "IdLinx", IsForeignKey=false)]
	    [DataMember(Name = "TcsEmpresaGpeconList", EmitDefaultValue = true)]
	    public IEnumerable<TcsEmpresaGpecon> TcsEmpresaGpeconList
	    {
	        get
	        {
	
	            if (this._TcsEmpresaGpeconList == null)
	            	this._TcsEmpresaGpeconList = new List<TcsEmpresaGpecon>();
	
	            return this._TcsEmpresaGpeconList;
	        }
	        set
	        {
	            if (this._TcsEmpresaGpeconList != value)
	            {
	                this._TcsEmpresaGpeconList = value;
	                this.RaisePropertyChanged("TcsEmpresaGpeconList");
	            }
	        }
	    }	 
		
	    private IEnumerable<TcsEmpresaModulo> _TcsEmpresaModuloList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsEmpresaAutenticacao_TcsEmpresaModulo", "IdLinx", "IdLinx", IsForeignKey=false)]
	    [DataMember(Name = "TcsEmpresaModuloList", EmitDefaultValue = true)]
	    public IEnumerable<TcsEmpresaModulo> TcsEmpresaModuloList
	    {
	        get
	        {
	
	            if (this._TcsEmpresaModuloList == null)
	            	this._TcsEmpresaModuloList = new List<TcsEmpresaModulo>();
	
	            return this._TcsEmpresaModuloList;
	        }
	        set
	        {
	            if (this._TcsEmpresaModuloList != value)
	            {
	                this._TcsEmpresaModuloList = value;
	                this.RaisePropertyChanged("TcsEmpresaModuloList");
	            }
	        }
	    }	 
		
	    private IEnumerable<TcsUsuarioAutenticacao> _TcsUsuarioAutenticacaoList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsEmpresaAutenticacao_TcsUsuarioAutenticacao", "IdLinx", "IdLinx", IsForeignKey=false)]
	    [DataMember(Name = "TcsUsuarioAutenticacaoList", EmitDefaultValue = true)]
	    public IEnumerable<TcsUsuarioAutenticacao> TcsUsuarioAutenticacaoList
	    {
	        get
	        {
	
	            if (this._TcsUsuarioAutenticacaoList == null)
	            	this._TcsUsuarioAutenticacaoList = new List<TcsUsuarioAutenticacao>();
	
	            return this._TcsUsuarioAutenticacaoList;
	        }
	        set
	        {
	            if (this._TcsUsuarioAutenticacaoList != value)
	            {
	                this._TcsUsuarioAutenticacaoList = value;
	                this.RaisePropertyChanged("TcsUsuarioAutenticacaoList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_EMPRESA_AUTENTICACAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_EMPRESA_AUTENTICACAO), QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_AUTENTICACAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_EMPRESA_AUTENTICACAO.ID_LINX", Source = "IdLinx", Target = "ID_LINX", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_AUTENTICACAO", RelationPropertyName = "TCS_EMPRESA_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_EMPRESA_AUTENTICACAO.CNPJ_CPF", Source = "CnpjCpf", Target = "CNPJ_CPF", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_AUTENTICACAO", RelationPropertyName = "TCS_EMPRESA_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA", Source = "UidEmpresa", Target = "UID_EMPRESA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_AUTENTICACAO", RelationPropertyName = "TCS_EMPRESA_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA", Source = "NomeEmpresa", Target = "NOME_EMPRESA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_AUTENTICACAO", RelationPropertyName = "TCS_EMPRESA_AUTENTICACAO" });

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

		

	[LinxPublicationView(PrimaryKeys="TCS_EMPRESA_GPECON.ID_LINX_GPECON,TCS_EMPRESA_GPECON.GPECON.ID_LINX,TCS_EMPRESA_GPECON.EMPRESA.ID_LINX", IsUpdatable=true, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Empresa / Grupo Econômico];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdGrupoEconomico];ReadOnly[false];Entities[TCS_EMPRESA_GPECON:IdGrupoEconomico];SubQueryInfo[Select 1 From #ParentAlias#.EMPRESA_LISTA as #Alias#];EdmEntityName[TCS_EMPRESA_GPECON];EntityRelations[GPECON(TCS_EMPRESA_AUTENTICACAO)#EMPRESA(TCS_EMPRESA_AUTENTICACAO)];EdmParentEntityName[TCS_EMPRESA_AUTENTICACAO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsEmpresaGpecon")]
	[Serializable()]
	public partial class TcsEmpresaGpecon : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(EmpresaDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsEmpresaAutenticacao");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdLinx"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdLinx));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsEmpresaAutenticacao
	         this.TcsEmpresaAutenticacao = (from r in context.GetTcsEmpresaAutenticacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For GrupoEconomico
	    partial void OnGrupoEconomicoChanging(System.String value);
	    partial void OnGrupoEconomicoChanged();

	    private System.String _GrupoEconomico;

	    [DataMember(IsRequired = true, Name = "GrupoEconomico", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Empresa / Grupo Econômico", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsEmpresaAutenticacaoGpecon];LookUpTitle[Seleção de (Empresa / Grupo Econômico)];LookUpQuery[executeLookUpTcsEmpresaAutenticacaoGpecon];LookUpFinalize[finalizeLookUpTcsEmpresaAutenticacaoGpecon];LookUpDisplayColumns[{\"IdLinxGpecon\" : \"Id Linx\", \"GrupoEconomico\" : \"Empresa / Grupo Econômico\"}];LookUpColumns[{\"IdLinxGpecon\" : true, \"GrupoEconomico\" : true}];FilterDataKey[TCS_EMPRESA_GPECON.EMPRESA.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#GrupoEconomico#false##2500##Empresa / Grupo Econômico#1#true##::LookUpTcsEmpresaAutenticacaoGpecon##true#false##TCS_EMPRESA_AUTENTICACAO#Linx.Framework.BV.Empresa#IQueryable###true#true", EdmKey="TCS_EMPRESA_GPECON.EMPRESA.NOME_EMPRESA")]
	    public System.String GrupoEconomico
	    {
	    	    get
	    	    {
	    	          return _GrupoEconomico;
	    	    }
	    	    set
	    	    {
	    	          if (this._GrupoEconomico != value)
	    	          {
	    	              this.ValidateProperty("GrupoEconomico", value);
	    	              this.OnGrupoEconomicoChanging(value);
	    	              this.RaiseDataMemberChanging("GrupoEconomico");
	    	              this._GrupoEconomico = value;
	    	              this.RaiseDataMemberChanged("GrupoEconomico");
	    	              this.OnGrupoEconomicoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdGrupoEconomico
	    partial void OnIdGrupoEconomicoChanging(Int32 value);
	    partial void OnIdGrupoEconomicoChanged();

	    private Int32 _IdGrupoEconomico;

	    [DataMember(IsRequired = true, Name = "IdGrupoEconomico", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx Gpecon", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_EMPRESA_GPECON.ID_LINX_GPECON];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_EMPRESA_GPECON.ID_LINX_GPECON")]
	    public Int32 IdGrupoEconomico
	    {
	    	    get
	    	    {
	    	          return _IdGrupoEconomico;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdGrupoEconomico != value)
	    	          {
	    	              this.ValidateProperty("IdGrupoEconomico", value);
	    	              this.OnIdGrupoEconomicoChanging(value);
	    	              this.RaiseDataMemberChanging("IdGrupoEconomico");
	    	              this._IdGrupoEconomico = value;
	    	              this.RaiseDataMemberChanged("IdGrupoEconomico");
	    	              this.OnIdGrupoEconomicoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLinx
	    partial void OnIdLinxChanging(Int32 value);
	    partial void OnIdLinxChanged();

	    private Int32 _IdLinx;

	    [DataMember(IsRequired = true, Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Linx", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_EMPRESA_GPECON.GPECON.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_EMPRESA_GPECON.GPECON.ID_LINX")]
	    public Int32 IdLinx
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
	    //Extensibility Partial Method Definitions For IdLinxGpecon
	    partial void OnIdLinxGpeconChanging(Int32 value);
	    partial void OnIdLinxGpeconChanged();

	    private Int32 _IdLinxGpecon;

	    [DataMember(IsRequired = true, Name = "IdLinxGpecon", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx Empresa / Grupo Econômico", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsEmpresaAutenticacaoGpecon];LookUpTitle[Seleção de (Id Linx Empresa / Grupo Econômico)];LookUpQuery[executeLookUpTcsEmpresaAutenticacaoGpecon];LookUpFinalize[finalizeLookUpTcsEmpresaAutenticacaoGpecon];LookUpDisplayColumns[{\"IdLinxGpecon\" : \"Id Linx\", \"GrupoEconomico\" : \"Empresa / Grupo Econômico\"}];LookUpColumns[{\"IdLinxGpecon\" : true, \"GrupoEconomico\" : true}];FilterDataKey[TCS_EMPRESA_GPECON.EMPRESA.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdLinxGpecon#true##12:0##Id Linx#0#true##::LookUpTcsEmpresaAutenticacaoGpecon##true#false##TCS_EMPRESA_AUTENTICACAO#Linx.Framework.BV.Empresa#IQueryable###true#true", EdmKey="TCS_EMPRESA_GPECON.EMPRESA.ID_LINX")]
	    public Int32 IdLinxGpecon
	    {
	    	    get
	    	    {
	    	          return _IdLinxGpecon;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLinxGpecon != value)
	    	          {
	    	              this.ValidateProperty("IdLinxGpecon", value);
	    	              this.OnIdLinxGpeconChanging(value);
	    	              this.RaiseDataMemberChanging("IdLinxGpecon");
	    	              this._IdLinxGpecon = value;
	    	              this.RaiseDataMemberChanged("IdLinxGpecon");
	    	              this.OnIdLinxGpeconChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIdGrupoEconomico;
	    [DataMember(Name = "TemporaryIdGrupoEconomico", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx Gpecon (Tmp)", Description="Temporary Key", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdGrupoEconomico
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdGrupoEconomico.IsNullOrEmpty())
	    	                this._TemporaryIdGrupoEconomico = this._IdGrupoEconomico;
	    	          return this._TemporaryIdGrupoEconomico;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdGrupoEconomico != value)
	    	              this._TemporaryIdGrupoEconomico = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsEmpresaAutenticacao _TcsEmpresaAutenticacao;
	    [DataMember(Name = "TcsEmpresaAutenticacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsEmpresaAutenticacao_TcsEmpresaGpecon", "IdLinx", "IdLinx", IsForeignKey=true)]
	    public TcsEmpresaAutenticacao TcsEmpresaAutenticacao
	    {
	        get
	        {
	            return this._TcsEmpresaAutenticacao;
	        }
	        set
	        {
	            if (this._TcsEmpresaAutenticacao != value)
	            {
	                this._TcsEmpresaAutenticacao = value;
	                this.RaisePropertyChanged("TcsEmpresaAutenticacaoList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_EMPRESA_GPECON").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_EMPRESA_GPECON), QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_GPECON" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_EMPRESA_GPECON.ID_LINX_GPECON", Source = "IdGrupoEconomico", Target = "ID_LINX_GPECON", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_GPECON", RelationPropertyName = "TCS_EMPRESA_GPECON" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_EMPRESA_GPECON.GPECON.ID_LINX", Source = "IdLinx", Target = "ID_LINX", TargetKeyName = "ID_LINX", NoUpdatable = false, IsKey = true, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_AUTENTICACAO", RelationPropertyName = "GPECON" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_EMPRESA_GPECON.EMPRESA.ID_LINX", Source = "IdLinxGpecon", Target = "ID_LINX", TargetKeyName = "ID_LINX", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_AUTENTICACAO", RelationPropertyName = "EMPRESA" });

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

		

	[LinxPublicationView(PrimaryKeys="TCS_AMBIENTE.ID_TCS_AMBIENTE", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Ambientes];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsAmbiente];ReadOnly[false];Entities[TCS_AMBIENTE:IdTcsAmbiente];SubQueryInfo[Select 1 From #ParentAlias#.TCS_AMBIENTE_LISTA as #Alias#];EdmEntityName[TCS_AMBIENTE];EntityRelations[TCS_APLICACAO(TCS_APLICACAO)#TCS_APLICATIVO(TCS_APLICATIVO)#TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)];EdmParentEntityName[TCS_EMPRESA_AUTENTICACAO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsAmbiente")]
	[Serializable()]
	public partial class TcsAmbiente : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(EmpresaDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsEmpresaAutenticacao");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdLinx"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdLinx));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsEmpresaAutenticacao
	         this.TcsEmpresaAutenticacao = (from r in context.GetTcsEmpresaAutenticacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For DescricaoAmbiente
	    partial void OnDescricaoAmbienteChanging(System.String value);
	    partial void OnDescricaoAmbienteChanged();

	    private System.String _DescricaoAmbiente;

	    [DataMember(IsRequired = true, Name = "DescricaoAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ambiente", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente];LookUpTitle[Seleção de (Ambiente)];LookUpQuery[executeLookUpTcsAmbiente];LookUpFinalize[finalizeLookUpTcsAmbiente];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"IdTcsAmbiente\" : false}];FilterDataKey[TCS_AMBIENTE.DESCRICAO_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoAmbiente#false##2500##Ambiente#0#true##::LookUpTcsAmbiente##false#false##TCS_AMBIENTE#Linx.Framework.BV.Empresa#IQueryable###true#false", EdmKey="TCS_AMBIENTE.DESCRICAO_AMBIENTE")]
	    public System.String DescricaoAmbiente
	    {
	    	    get
	    	    {
	    	          return _DescricaoAmbiente;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescricaoAmbiente != value)
	    	          {
	    	              this.ValidateProperty("DescricaoAmbiente", value);
	    	              this.OnDescricaoAmbienteChanging(value);
	    	              this.RaiseDataMemberChanging("DescricaoAmbiente");
	    	              this._DescricaoAmbiente = value;
	    	              this.RaiseDataMemberChanged("DescricaoAmbiente");
	    	              this.OnDescricaoAmbienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescricaoAplicacao
	    partial void OnDescricaoAplicacaoChanging(System.String value);
	    partial void OnDescricaoAplicacaoChanged();

	    private System.String _DescricaoAplicacao;

	    [DataMember(IsRequired = true, Name = "DescricaoAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicação", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicacao];LookUpTitle[Seleção de (Aplicação)];LookUpQuery[executeLookUpTcsAplicacao];LookUpFinalize[finalizeLookUpTcsAplicacao];LookUpDisplayColumns[{\"DescricaoAplicacao\" : \"Aplicação\", \"EmDesenvolvimento\" : \"Em Desenvolvimento\"}];LookUpColumns[{\"DescricaoAplicacao\" : true, \"EmDesenvolvimento\" : true}];FilterDataKey[TCS_AMBIENTE.TCS_APLICACAO.DESCRICAO_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoAplicacao#false##60:0##Aplicação#0#true##::LookUpTcsAplicacao##false#false#TCS_APLICACAO#TCS_APLICACAO#Linx.Framework.BV.Empresa#IQueryable###true#false", EdmKey="TCS_AMBIENTE.TCS_APLICACAO.DESCRICAO_APLICACAO")]
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
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicacao];LookUpTitle[Seleção de (Em Desenvolvimento)];LookUpQuery[executeLookUpTcsAplicacao];LookUpFinalize[finalizeLookUpTcsAplicacao];LookUpDisplayColumns[{\"DescricaoAplicacao\" : \"Aplicação\", \"EmDesenvolvimento\" : \"Em Desenvolvimento\"}];LookUpColumns[{\"DescricaoAplicacao\" : true, \"EmDesenvolvimento\" : true}];FilterDataKey[TCS_AMBIENTE.TCS_APLICACAO.EM_DESENVOLVIMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Boolean#EmDesenvolvimento#false##0:0##Em Desenvolvimento#1#true##::LookUpTcsAplicacao##false#false#TCS_APLICACAO#TCS_APLICACAO#Linx.Framework.BV.Empresa#IQueryable###true#false", EdmKey="TCS_AMBIENTE.TCS_APLICACAO.EM_DESENVOLVIMENTO")]
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
	    //Extensibility Partial Method Definitions For IdLinx
	    partial void OnIdLinxChanging(Int32 value);
	    partial void OnIdLinxChanged();

	    private Int32 _IdLinx;

	    [DataMember(IsRequired = true, Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Linx", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
	    public Int32 IdLinx
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
	    //Extensibility Partial Method Definitions For IdTcsAmbiente
	    partial void OnIdTcsAmbienteChanging(Int32 value);
	    partial void OnIdTcsAmbienteChanged();

	    private Int32 _IdTcsAmbiente;

	    [DataMember(IsRequired = true, Name = "IdTcsAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente];LookUpTitle[Seleção de (Id Tcs Ambiente)];LookUpQuery[executeLookUpTcsAmbiente];LookUpFinalize[finalizeLookUpTcsAmbiente];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"IdTcsAmbiente\" : false}];FilterDataKey[TCS_AMBIENTE.ID_TCS_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdTcsAmbiente#true##12:0##Id Tcs Ambiente#1#false##::LookUpTcsAmbiente##false#false##TCS_AMBIENTE#Linx.Framework.BV.Empresa#IQueryable###true#false", EdmKey="TCS_AMBIENTE.ID_TCS_AMBIENTE")]
	    public Int32 IdTcsAmbiente
	    {
	    	    get
	    	    {
	    	          return _IdTcsAmbiente;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAmbiente != value)
	    	          {
	    	              this.ValidateProperty("IdTcsAmbiente", value);
	    	              this.OnIdTcsAmbienteChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsAmbiente");
	    	              this._IdTcsAmbiente = value;
	    	              this.RaiseDataMemberChanged("IdTcsAmbiente");
	    	              this.OnIdTcsAmbienteChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIdTcsAmbiente;
	    [DataMember(Name = "TemporaryIdTcsAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente (Tmp)", Description="Temporary Key", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdTcsAmbiente
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTcsAmbiente.IsNullOrEmpty())
	    	                this._TemporaryIdTcsAmbiente = this._IdTcsAmbiente;
	    	          return this._TemporaryIdTcsAmbiente;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTcsAmbiente != value)
	    	              this._TemporaryIdTcsAmbiente = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsEmpresaAutenticacao _TcsEmpresaAutenticacao;
	    [DataMember(Name = "TcsEmpresaAutenticacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsEmpresaAutenticacao_TcsAmbiente", "IdLinx", "IdLinx", IsForeignKey=true)]
	    public TcsEmpresaAutenticacao TcsEmpresaAutenticacao
	    {
	        get
	        {
	            return this._TcsEmpresaAutenticacao;
	        }
	        set
	        {
	            if (this._TcsEmpresaAutenticacao != value)
	            {
	                this._TcsEmpresaAutenticacao = value;
	                this.RaisePropertyChanged("TcsEmpresaAutenticacaoList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_AMBIENTE").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_AMBIENTE), QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_AMBIENTE.ID_TCS_AMBIENTE", Source = "IdTcsAmbiente", Target = "ID_TCS_AMBIENTE", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE", RelationPropertyName = "TCS_AMBIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_AMBIENTE.DESCRICAO_AMBIENTE", Source = "DescricaoAmbiente", Target = "DESCRICAO_AMBIENTE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE", RelationPropertyName = "TCS_AMBIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX", Source = "IdLinx", Target = "ID_LINX", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_AUTENTICACAO", RelationPropertyName = "TCS_EMPRESA_AUTENTICACAO" });

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

		

	[LinxPublicationView(PrimaryKeys="TCS_EMPRESA_MODULO.ID_TCS_EMPRESA_MODULO", IsUpdatable=true, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Módulos Permitidos];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsEmpresaModulo];ReadOnly[false];Entities[TCS_EMPRESA_MODULO:IdTcsEmpresaModulo];SubQueryInfo[Select 1 From #ParentAlias#.TCS_EMPRESA_MODULO_LISTA as #Alias#];EdmEntityName[TCS_EMPRESA_MODULO];EntityRelations[TCS_MODULO_AUTORIZACAO(TCS_MODULO_AUTORIZACAO)#TCS_APLICATIVO(TCS_APLICATIVO)#TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)];EdmParentEntityName[TCS_EMPRESA_AUTENTICACAO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsEmpresaModulo")]
	[Serializable()]
	public partial class TcsEmpresaModulo : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(EmpresaDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsEmpresaAutenticacao");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdLinx"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdLinx));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsEmpresaAutenticacao
	         this.TcsEmpresaAutenticacao = (from r in context.GetTcsEmpresaAutenticacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For DescModulo
	    partial void OnDescModuloChanging(System.String value);
	    partial void OnDescModuloChanged();

	    private System.String _DescModulo;

	    [DataMember(IsRequired = true, Name = "DescModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Módulo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsModuloAutorizacao];LookUpTitle[Seleção de (Módulo)];LookUpQuery[executeLookUpTcsModuloAutorizacao];LookUpFinalize[finalizeLookUpTcsModuloAutorizacao];LookUpDisplayColumns[{\"DescModulo\" : \"Módulo\", \"IdModulo\" : \"Id Modulo\", \"DescricaoAplicativo\" : \"Aplicativo\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\"}];LookUpColumns[{\"DescModulo\" : true, \"IdModulo\" : false, \"DescricaoAplicativo\" : true, \"IdTcsAplicativo\" : false}];FilterDataKey[TCS_EMPRESA_MODULO.TCS_MODULO_AUTORIZACAO.DESC_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescModulo#false##60:0##Módulo#0#true##::LookUpTcsModuloAutorizacao##true#false#TCS_MODULO_AUTORIZACAO#TCS_MODULO_AUTORIZACAO#Linx.Framework.BV.Empresa#IQueryable#DescricaoAplicativo,IdTcsAplicativo[DescricaoAplicativo,IdTcsAplicativo]#DescModulo[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdModulo[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo]#true#true", EdmKey="TCS_EMPRESA_MODULO.TCS_MODULO_AUTORIZACAO.DESC_MODULO")]
	    public System.String DescModulo
	    {
	    	    get
	    	    {
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
	    //Extensibility Partial Method Definitions For DescricaoAplicativo
	    partial void OnDescricaoAplicativoChanging(System.String value);
	    partial void OnDescricaoAplicativoChanged();

	    private System.String _DescricaoAplicativo;

	    [DataMember(Name = "DescricaoAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicativo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsModuloAutorizacao];LookUpTitle[Seleção de (Aplicativo)];LookUpQuery[executeLookUpTcsModuloAutorizacao];LookUpFinalize[finalizeLookUpTcsModuloAutorizacao];LookUpDisplayColumns[{\"DescModulo\" : \"Módulo\", \"IdModulo\" : \"Id Modulo\", \"DescricaoAplicativo\" : \"Aplicativo\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\"}];LookUpColumns[{\"DescModulo\" : true, \"IdModulo\" : false, \"DescricaoAplicativo\" : true, \"IdTcsAplicativo\" : false}];FilterDataKey[TCS_EMPRESA_MODULO.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoAplicativo#false##250:0##Aplicativo#2#true##::LookUpTcsModuloAutorizacao##true#false#TCS_MODULO_AUTORIZACAO#TCS_MODULO_AUTORIZACAO#Linx.Framework.BV.Empresa#IQueryable#DescricaoAplicativo,IdTcsAplicativo[DescricaoAplicativo,IdTcsAplicativo]#DescModulo[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdModulo[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo]#true#true", EdmKey="TCS_EMPRESA_MODULO.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO")]
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
	    //Extensibility Partial Method Definitions For IdLinx
	    partial void OnIdLinxChanging(Int32 value);
	    partial void OnIdLinxChanged();

	    private Int32 _IdLinx;

	    [DataMember(IsRequired = true, Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Linx", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_EMPRESA_MODULO.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_EMPRESA_MODULO.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
	    public Int32 IdLinx
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
	    //Extensibility Partial Method Definitions For IdModulo
	    partial void OnIdModuloChanging(Int64 value);
	    partial void OnIdModuloChanged();

	    private Int64 _IdModulo;

	    [DataMember(IsRequired = true, Name = "IdModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsModuloAutorizacao];LookUpTitle[Seleção de (Id Modulo)];LookUpQuery[executeLookUpTcsModuloAutorizacao];LookUpFinalize[finalizeLookUpTcsModuloAutorizacao];LookUpDisplayColumns[{\"DescModulo\" : \"Módulo\", \"IdModulo\" : \"Id Modulo\", \"DescricaoAplicativo\" : \"Aplicativo\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\"}];LookUpColumns[{\"DescModulo\" : true, \"IdModulo\" : false, \"DescricaoAplicativo\" : true, \"IdTcsAplicativo\" : false}];FilterDataKey[TCS_EMPRESA_MODULO.TCS_MODULO_AUTORIZACAO.ID_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdModulo#true##24:0##Id Modulo#1#false##::LookUpTcsModuloAutorizacao##true#false#TCS_MODULO_AUTORIZACAO#TCS_MODULO_AUTORIZACAO#Linx.Framework.BV.Empresa#IQueryable#DescricaoAplicativo,IdTcsAplicativo[DescricaoAplicativo,IdTcsAplicativo]#DescModulo[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdModulo[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo]#true#true", EdmKey="TCS_EMPRESA_MODULO.TCS_MODULO_AUTORIZACAO.ID_MODULO")]
	    public Int64 IdModulo
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
	    //Extensibility Partial Method Definitions For IdTcsAplicativo
	    partial void OnIdTcsAplicativoChanging(Int32 value);
	    partial void OnIdTcsAplicativoChanged();

	    private Int32 _IdTcsAplicativo;

	    [DataMember(IsRequired = true, Name = "IdTcsAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Aplicativo", Description="", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsModuloAutorizacao];LookUpTitle[Seleção de (Id Tcs Aplicativo)];LookUpQuery[executeLookUpTcsModuloAutorizacao];LookUpFinalize[finalizeLookUpTcsModuloAutorizacao];LookUpDisplayColumns[{\"DescModulo\" : \"Módulo\", \"IdModulo\" : \"Id Modulo\", \"DescricaoAplicativo\" : \"Aplicativo\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\"}];LookUpColumns[{\"DescModulo\" : true, \"IdModulo\" : false, \"DescricaoAplicativo\" : true, \"IdTcsAplicativo\" : false}];FilterDataKey[TCS_EMPRESA_MODULO.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdTcsAplicativo#false##12:0##Id Tcs Aplicativo#3#false##::LookUpTcsModuloAutorizacao##true#false#TCS_MODULO_AUTORIZACAO#TCS_MODULO_AUTORIZACAO#Linx.Framework.BV.Empresa#IQueryable#DescricaoAplicativo,IdTcsAplicativo[DescricaoAplicativo,IdTcsAplicativo]#DescModulo[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdModulo[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo]#true#true", EdmKey="TCS_EMPRESA_MODULO.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO")]
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
	    //Extensibility Partial Method Definitions For IdTcsEmpresaModulo
	    partial void OnIdTcsEmpresaModuloChanging(Int32 value);
	    partial void OnIdTcsEmpresaModuloChanged();

	    private Int32 _IdTcsEmpresaModulo;

	    [DataMember(IsRequired = true, Name = "IdTcsEmpresaModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Empresa Modulo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_EMPRESA_MODULO.ID_TCS_EMPRESA_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_EMPRESA_MODULO.ID_TCS_EMPRESA_MODULO")]
	    public Int32 IdTcsEmpresaModulo
	    {
	    	    get
	    	    {
	    	          return _IdTcsEmpresaModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsEmpresaModulo != value)
	    	          {
	    	              this.ValidateProperty("IdTcsEmpresaModulo", value);
	    	              this.OnIdTcsEmpresaModuloChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsEmpresaModulo");
	    	              this._IdTcsEmpresaModulo = value;
	    	              this.RaiseDataMemberChanged("IdTcsEmpresaModulo");
	    	              this.OnIdTcsEmpresaModuloChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIdTcsEmpresaModulo;
	    [DataMember(Name = "TemporaryIdTcsEmpresaModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Empresa Modulo (Tmp)", Description="Temporary Key", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdTcsEmpresaModulo
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTcsEmpresaModulo.IsNullOrEmpty())
	    	                this._TemporaryIdTcsEmpresaModulo = this._IdTcsEmpresaModulo;
	    	          return this._TemporaryIdTcsEmpresaModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTcsEmpresaModulo != value)
	    	              this._TemporaryIdTcsEmpresaModulo = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsEmpresaAutenticacao _TcsEmpresaAutenticacao;
	    [DataMember(Name = "TcsEmpresaAutenticacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsEmpresaAutenticacao_TcsEmpresaModulo", "IdLinx", "IdLinx", IsForeignKey=true)]
	    public TcsEmpresaAutenticacao TcsEmpresaAutenticacao
	    {
	        get
	        {
	            return this._TcsEmpresaAutenticacao;
	        }
	        set
	        {
	            if (this._TcsEmpresaAutenticacao != value)
	            {
	                this._TcsEmpresaAutenticacao = value;
	                this.RaisePropertyChanged("TcsEmpresaAutenticacaoList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_EMPRESA_MODULO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_EMPRESA_MODULO), QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_MODULO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_EMPRESA_MODULO.ID_TCS_EMPRESA_MODULO", Source = "IdTcsEmpresaModulo", Target = "ID_TCS_EMPRESA_MODULO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_MODULO", RelationPropertyName = "TCS_EMPRESA_MODULO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_EMPRESA_MODULO.TCS_EMPRESA_AUTENTICACAO.ID_LINX", Source = "IdLinx", Target = "ID_LINX", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_AUTENTICACAO", RelationPropertyName = "TCS_EMPRESA_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_EMPRESA_MODULO.TCS_MODULO_AUTORIZACAO.ID_MODULO", Source = "IdModulo", Target = "ID_MODULO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_MODULO_AUTORIZACAO", RelationPropertyName = "TCS_MODULO_AUTORIZACAO" });

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

		

	[LinxPublicationView(PrimaryKeys="TCS_USUARIO_AUTENTICACAO.ID_USUARIO", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Usuários];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdUsuario];ReadOnly[true];Entities[TCS_USUARIO_AUTENTICACAO:IdUsuario];SubQueryInfo[Select 1 From #ParentAlias#.TCS_USUARIO_AUTENTICACAO_LISTA as #Alias#];EdmEntityName[TCS_USUARIO_AUTENTICACAO];EntityRelations[TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)];EdmParentEntityName[TCS_EMPRESA_AUTENTICACAO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioAutenticacao")]
	[Serializable()]
	public partial class TcsUsuarioAutenticacao : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(EmpresaDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsEmpresaAutenticacao");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdLinx"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdLinx));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsEmpresaAutenticacao
	         this.TcsEmpresaAutenticacao = (from r in context.GetTcsEmpresaAutenticacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	    partial void OnIdLinxChanging(Int32 value);
	    partial void OnIdLinxChanged();

	    private Int32 _IdLinx;

	    [DataMember(IsRequired = true, Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Linx", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacao];LookUpTitle[Seleção de (ID Linx)];LookUpQuery[executeLookUpTcsUsuarioAutenticacao];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacao];LookUpDisplayColumns[{\"IdLinx\" : \"Id Linx\", \"IdUsuario\" : \"Id Usuario\", \"NomeUsuario\" : \"Nome Usuário\", \"NomeAutenticacao\" : \"Usuário Autenticação\"}];LookUpColumns[{\"IdLinx\" : false, \"IdUsuario\" : false, \"NomeUsuario\" : true, \"NomeAutenticacao\" : true}];FilterDataKey[TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdLinx#true##12:0##Id Linx#0#false##::LookUpTcsUsuarioAutenticacao##false#false##TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.Empresa#IQueryable#IdLinx[IdLinx]#IdUsuario[IdLinx=IdLinx];NomeUsuario[IdLinx=IdLinx];NomeAutenticacao[IdLinx=IdLinx]#true#false", EdmKey="TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
	    public Int32 IdLinx
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
	    //Extensibility Partial Method Definitions For IdUsuario
	    partial void OnIdUsuarioChanging(Int64 value);
	    partial void OnIdUsuarioChanged();

	    private Int64 _IdUsuario;

	    [DataMember(IsRequired = true, Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 12, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacao];LookUpTitle[Seleção de (Id Usuario)];LookUpQuery[executeLookUpTcsUsuarioAutenticacao];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacao];LookUpDisplayColumns[{\"IdLinx\" : \"Id Linx\", \"IdUsuario\" : \"Id Usuario\", \"NomeUsuario\" : \"Nome Usuário\", \"NomeAutenticacao\" : \"Usuário Autenticação\"}];LookUpColumns[{\"IdLinx\" : false, \"IdUsuario\" : false, \"NomeUsuario\" : true, \"NomeAutenticacao\" : true}];FilterDataKey[TCS_USUARIO_AUTENTICACAO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdUsuario#true##24:0##Id Usuario#1#false##::LookUpTcsUsuarioAutenticacao##false#false##TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.Empresa#IQueryable#IdLinx[IdLinx]#IdUsuario[IdLinx=IdLinx];NomeUsuario[IdLinx=IdLinx];NomeAutenticacao[IdLinx=IdLinx]#true#false", EdmKey="TCS_USUARIO_AUTENTICACAO.ID_USUARIO")]
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
	    //Extensibility Partial Method Definitions For NomeAutenticacao
	    partial void OnNomeAutenticacaoChanging(System.String value);
	    partial void OnNomeAutenticacaoChanged();

	    private System.String _NomeAutenticacao;

	    [DataMember(IsRequired = true, Name = "NomeAutenticacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário Autenticação", Description="", Order = 20, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacao];LookUpTitle[Seleção de (Usuário Autenticação)];LookUpQuery[executeLookUpTcsUsuarioAutenticacao];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacao];LookUpDisplayColumns[{\"IdLinx\" : \"Id Linx\", \"IdUsuario\" : \"Id Usuario\", \"NomeUsuario\" : \"Nome Usuário\", \"NomeAutenticacao\" : \"Usuário Autenticação\"}];LookUpColumns[{\"IdLinx\" : false, \"IdUsuario\" : false, \"NomeUsuario\" : true, \"NomeAutenticacao\" : true}];FilterDataKey[TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeAutenticacao#false##2500##Usuário Autenticação#3#true##::LookUpTcsUsuarioAutenticacao##false#false##TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.Empresa#IQueryable#IdLinx[IdLinx]#IdUsuario[IdLinx=IdLinx];NomeUsuario[IdLinx=IdLinx];NomeAutenticacao[IdLinx=IdLinx]#true#false", EdmKey="TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO")]
	    public System.String NomeAutenticacao
	    {
	    	    get
	    	    {
	    	          return _NomeAutenticacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeAutenticacao != value)
	    	          {
	    	              this.ValidateProperty("NomeAutenticacao", value);
	    	              this.OnNomeAutenticacaoChanging(value);
	    	              this.RaiseDataMemberChanging("NomeAutenticacao");
	    	              this._NomeAutenticacao = value;
	    	              this.RaiseDataMemberChanged("NomeAutenticacao");
	    	              this.OnNomeAutenticacaoChanged();
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
	    [Display(Name = "Nome Usuário", Description="", Order = 22, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacao];LookUpTitle[Seleção de (Nome Usuário)];LookUpQuery[executeLookUpTcsUsuarioAutenticacao];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacao];LookUpDisplayColumns[{\"IdLinx\" : \"Id Linx\", \"IdUsuario\" : \"Id Usuario\", \"NomeUsuario\" : \"Nome Usuário\", \"NomeAutenticacao\" : \"Usuário Autenticação\"}];LookUpColumns[{\"IdLinx\" : false, \"IdUsuario\" : false, \"NomeUsuario\" : true, \"NomeAutenticacao\" : true}];FilterDataKey[TCS_USUARIO_AUTENTICACAO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeUsuario#false##2500##Nome Usuário#2#true##::LookUpTcsUsuarioAutenticacao##false#false##TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.Empresa#IQueryable#IdLinx[IdLinx]#IdUsuario[IdLinx=IdLinx];NomeUsuario[IdLinx=IdLinx];NomeAutenticacao[IdLinx=IdLinx]#true#false", EdmKey="TCS_USUARIO_AUTENTICACAO.NOME_USUARIO")]
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
	    [Display(Name = "Id Usuario (Tmp)", Description="Temporary Key", Order = 12, AutoGenerateField = false, GroupName="", ResourceType= null)]
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

		

	    #region Parent Association
	 
	    private TcsEmpresaAutenticacao _TcsEmpresaAutenticacao;
	    [DataMember(Name = "TcsEmpresaAutenticacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsEmpresaAutenticacao_TcsUsuarioAutenticacao", "IdLinx", "IdLinx", IsForeignKey=true)]
	    public TcsEmpresaAutenticacao TcsEmpresaAutenticacao
	    {
	        get
	        {
	            return this._TcsEmpresaAutenticacao;
	        }
	        set
	        {
	            if (this._TcsEmpresaAutenticacao != value)
	            {
	                this._TcsEmpresaAutenticacao = value;
	                this.RaisePropertyChanged("TcsEmpresaAutenticacaoList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_USUARIO_AUTENTICACAO), QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.NOME_USUARIO", Source = "NomeUsuario", Target = "NOME_USUARIO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO", Source = "NomeAutenticacao", Target = "NOME_AUTENTICACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.ID_LINX", Source = "IdLinx", Target = "ID_LINX", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_AUTENTICACAO", RelationPropertyName = "TCS_EMPRESA_AUTENTICACAO" });

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

		

	[LinxPublicationView(PrimaryKeys="TCS_EMPRESA_GPECON.ID_LINX,TCS_EMPRESA_GPECON.ID_LINX_GPECON", IsUpdatable=true, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsEmpresaGpeconP];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[];EdmEntityName[TCS_EMPRESA_GPECON];EntityRelations[GPECON(TCS_EMPRESA_AUTENTICACAO)#EMPRESA(TCS_EMPRESA_AUTENTICACAO)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsEmpresaGpeconP")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Empresa.TcsEmpresaGpeconP")]
	public partial class TcsEmpresaGpeconP : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For IdLinx
	    partial void OnIdLinxChanging(Int32 value);
	    partial void OnIdLinxChanged();

	    private Int32 _IdLinx;

	    [DataMember(IsRequired = true, Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_EMPRESA_GPECON.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_EMPRESA_GPECON.ID_LINX")]
	    public Int32 IdLinx
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
	    //Extensibility Partial Method Definitions For IdLinxGpecon
	    partial void OnIdLinxGpeconChanging(Int32 value);
	    partial void OnIdLinxGpeconChanged();

	    private Int32 _IdLinxGpecon;

	    [DataMember(IsRequired = true, Name = "IdLinxGpecon", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx Gpecon", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_EMPRESA_GPECON.ID_LINX_GPECON];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_EMPRESA_GPECON.ID_LINX_GPECON")]
	    public Int32 IdLinxGpecon
	    {
	    	    get
	    	    {
	    	          return _IdLinxGpecon;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLinxGpecon != value)
	    	          {
	    	              this.ValidateProperty("IdLinxGpecon", value);
	    	              this.OnIdLinxGpeconChanging(value);
	    	              this.RaiseDataMemberChanging("IdLinxGpecon");
	    	              this._IdLinxGpecon = value;
	    	              this.RaiseDataMemberChanged("IdLinxGpecon");
	    	              this.OnIdLinxGpeconChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_EMPRESA_GPECON").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_EMPRESA_GPECON), QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_GPECON" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_EMPRESA_GPECON.ID_LINX", Source = "IdLinx", Target = "ID_LINX", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_GPECON", RelationPropertyName = "TCS_EMPRESA_GPECON" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_EMPRESA_GPECON.ID_LINX_GPECON", Source = "IdLinxGpecon", Target = "ID_LINX_GPECON", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_GPECON", RelationPropertyName = "TCS_EMPRESA_GPECON" });

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

		

	[LinxPublicationView(PrimaryKeys="TCS_EMPRESA_AUTENTICACAO.ID_LINX", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsEmpresaAutenticacaoP];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdLinx];ReadOnly[false];Entities[TCS_EMPRESA_AUTENTICACAO:IdLinx];SubQueryInfo[];EdmEntityName[TCS_EMPRESA_AUTENTICACAO];EntityRelations[];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsEmpresaAutenticacaoP")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Empresa.TcsEmpresaAutenticacaoP")]
	public partial class TcsEmpresaAutenticacaoP : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For IdLinx
	    partial void OnIdLinxChanging(Int32 value);
	    partial void OnIdLinxChanged();

	    private Int32 _IdLinx;

	    [DataMember(IsRequired = true, Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsEmpresaAutenticacaoP];LookUpTitle[Seleção de (Id Linx)];LookUpQuery[executeLookUpTcsEmpresaAutenticacaoP];LookUpFinalize[finalizeLookUpTcsEmpresaAutenticacaoP];LookUpDisplayColumns[{\"IdLinx\" : \"Id Linx\", \"NomeEmpresa\" : \"Empresa\", \"UidEmpresa\" : \"Uid Empresa\"}];LookUpColumns[{\"IdLinx\" : false, \"NomeEmpresa\" : true, \"UidEmpresa\" : false}];FilterDataKey[TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdLinx#true##12:0##Id Linx#0#false##::LookUpTcsEmpresaAutenticacaoP##true#false##TCS_EMPRESA_AUTENTICACAO#Linx.Framework.BV.Empresa#IQueryable###true#false", EdmKey="TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
	    public Int32 IdLinx
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
	    //Extensibility Partial Method Definitions For NomeEmpresa
	    partial void OnNomeEmpresaChanging(System.String value);
	    partial void OnNomeEmpresaChanged();

	    private System.String _NomeEmpresa;

	    [DataMember(IsRequired = true, Name = "NomeEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Empresa", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsEmpresaAutenticacaoP];LookUpTitle[Seleção de (Nome Empresa)];LookUpQuery[executeLookUpTcsEmpresaAutenticacaoP];LookUpFinalize[finalizeLookUpTcsEmpresaAutenticacaoP];LookUpDisplayColumns[{\"IdLinx\" : \"Id Linx\", \"NomeEmpresa\" : \"Empresa\", \"UidEmpresa\" : \"Uid Empresa\"}];LookUpColumns[{\"IdLinx\" : false, \"NomeEmpresa\" : true, \"UidEmpresa\" : false}];FilterDataKey[TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeEmpresa#false##2500##Empresa#1#true##::LookUpTcsEmpresaAutenticacaoP##true#false##TCS_EMPRESA_AUTENTICACAO#Linx.Framework.BV.Empresa#IQueryable###true#false", EdmKey="TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
	    public System.String NomeEmpresa
	    {
	    	    get
	    	    {
	    	          return _NomeEmpresa;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeEmpresa != value)
	    	          {
	    	              this.ValidateProperty("NomeEmpresa", value);
	    	              this.OnNomeEmpresaChanging(value);
	    	              this.RaiseDataMemberChanging("NomeEmpresa");
	    	              this._NomeEmpresa = value;
	    	              this.RaiseDataMemberChanged("NomeEmpresa");
	    	              this.OnNomeEmpresaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidEmpresa
	    partial void OnUidEmpresaChanging(System.Guid value);
	    partial void OnUidEmpresaChanged();

	    private System.Guid _UidEmpresa;

	    [DataMember(IsRequired = true, Name = "UidEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Empresa", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsEmpresaAutenticacaoP];LookUpTitle[Seleção de (Uid Empresa)];LookUpQuery[executeLookUpTcsEmpresaAutenticacaoP];LookUpFinalize[finalizeLookUpTcsEmpresaAutenticacaoP];LookUpDisplayColumns[{\"IdLinx\" : \"Id Linx\", \"NomeEmpresa\" : \"Empresa\", \"UidEmpresa\" : \"Uid Empresa\"}];LookUpColumns[{\"IdLinx\" : false, \"NomeEmpresa\" : true, \"UidEmpresa\" : false}];FilterDataKey[TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Guid#UidEmpresa#false##36:0##Uid Empresa#2#false##::LookUpTcsEmpresaAutenticacaoP##true#false##TCS_EMPRESA_AUTENTICACAO#Linx.Framework.BV.Empresa#IQueryable###true#false", EdmKey="TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA")]
	    public System.Guid UidEmpresa
	    {
	    	    get
	    	    {
	    	          return _UidEmpresa;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidEmpresa != value)
	    	          {
	    	              this.ValidateProperty("UidEmpresa", value);
	    	              this.OnUidEmpresaChanging(value);
	    	              this.RaiseDataMemberChanging("UidEmpresa");
	    	              this._UidEmpresa = value;
	    	              this.RaiseDataMemberChanged("UidEmpresa");
	    	              this.OnUidEmpresaChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIdLinx;
	    [DataMember(Name = "TemporaryIdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx (Tmp)", Description="Temporary Key", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdLinx
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdLinx.IsNullOrEmpty())
	    	                this._TemporaryIdLinx = this._IdLinx;
	    	          return this._TemporaryIdLinx;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdLinx != value)
	    	              this._TemporaryIdLinx = value;
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_EMPRESA_AUTENTICACAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_EMPRESA_AUTENTICACAO), QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_AUTENTICACAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_EMPRESA_AUTENTICACAO.ID_LINX", Source = "IdLinx", Target = "ID_LINX", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_AUTENTICACAO", RelationPropertyName = "TCS_EMPRESA_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA", Source = "UidEmpresa", Target = "UID_EMPRESA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_AUTENTICACAO", RelationPropertyName = "TCS_EMPRESA_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA", Source = "NomeEmpresa", Target = "NOME_EMPRESA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_AUTENTICACAO", RelationPropertyName = "TCS_EMPRESA_AUTENTICACAO" });

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

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Empresa / Grupo Econômico];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdGrupoEconomico];ReadOnly[false];Entities[TCS_EMPRESA_GPECON:IdGrupoEconomico];SubQueryInfo[Select 1 From #ParentAlias#.EMPRESA_LISTA as #Alias#];EdmEntityName[TCS_EMPRESA_GPECON];EntityRelations[GPECON(TCS_EMPRESA_AUTENTICACAO)#EMPRESA(TCS_EMPRESA_AUTENTICACAO)];EdmParentEntityName[TCS_EMPRESA_AUTENTICACAO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsEmpresaGpecon")]
	[Serializable()]
	public partial class TcsEmpresaGpeconParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For GrupoEconomico
	    partial void OnGrupoEconomicoChanging(System.String value);
	    partial void OnGrupoEconomicoChanged();

	    private System.String _GrupoEconomico;

	    [DataMember(IsRequired = true, Name = "GrupoEconomico", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Empresa / Grupo Econômico", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsEmpresaAutenticacaoGpecon];LookUpTitle[Seleção de (Empresa / Grupo Econômico)];LookUpQuery[executeLookUpTcsEmpresaAutenticacaoGpecon];LookUpFinalize[finalizeLookUpTcsEmpresaAutenticacaoGpecon];LookUpDisplayColumns[{\"IdLinxGpecon\" : \"Id Linx\", \"GrupoEconomico\" : \"Empresa / Grupo Econômico\"}];LookUpColumns[{\"IdLinxGpecon\" : true, \"GrupoEconomico\" : true}];FilterDataKey[TCS_EMPRESA_GPECON.EMPRESA.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#GrupoEconomico#false##2500##Empresa / Grupo Econômico#1#true##::LookUpTcsEmpresaAutenticacaoGpecon##true#false##TCS_EMPRESA_AUTENTICACAO#Linx.Framework.BV.Empresa#IQueryable###true#true", EdmKey="TCS_EMPRESA_GPECON.EMPRESA.NOME_EMPRESA")]
	    public System.String GrupoEconomico
	    {
	    	    get
	    	    {
	    	          return _GrupoEconomico;
	    	    }
	    	    set
	    	    {
	    	          if (this._GrupoEconomico != value)
	    	          {
	    	              this.ValidateProperty("GrupoEconomico", value);
	    	              this.OnGrupoEconomicoChanging(value);
	    	              this.RaiseDataMemberChanging("GrupoEconomico");
	    	              this._GrupoEconomico = value;
	    	              this.RaiseDataMemberChanged("GrupoEconomico");
	    	              this.OnGrupoEconomicoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdGrupoEconomico
	    partial void OnIdGrupoEconomicoChanging(Int32 value);
	    partial void OnIdGrupoEconomicoChanged();

	    private Int32 _IdGrupoEconomico;

	    [DataMember(IsRequired = true, Name = "IdGrupoEconomico", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx Gpecon", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_EMPRESA_GPECON.ID_LINX_GPECON];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_EMPRESA_GPECON.ID_LINX_GPECON")]
	    public Int32 IdGrupoEconomico
	    {
	    	    get
	    	    {
	    	          return _IdGrupoEconomico;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdGrupoEconomico != value)
	    	          {
	    	              this.ValidateProperty("IdGrupoEconomico", value);
	    	              this.OnIdGrupoEconomicoChanging(value);
	    	              this.RaiseDataMemberChanging("IdGrupoEconomico");
	    	              this._IdGrupoEconomico = value;
	    	              this.RaiseDataMemberChanged("IdGrupoEconomico");
	    	              this.OnIdGrupoEconomicoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLinx
	    partial void OnIdLinxChanging(Int32 value);
	    partial void OnIdLinxChanged();

	    private Int32 _IdLinx;

	    [DataMember(IsRequired = true, Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Linx", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_EMPRESA_GPECON.GPECON.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_EMPRESA_GPECON.GPECON.ID_LINX")]
	    public Int32 IdLinx
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
	    //Extensibility Partial Method Definitions For IdLinxGpecon
	    partial void OnIdLinxGpeconChanging(Int32 value);
	    partial void OnIdLinxGpeconChanged();

	    private Int32 _IdLinxGpecon;

	    [DataMember(IsRequired = true, Name = "IdLinxGpecon", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx Empresa / Grupo Econômico", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsEmpresaAutenticacaoGpecon];LookUpTitle[Seleção de (Id Linx Empresa / Grupo Econômico)];LookUpQuery[executeLookUpTcsEmpresaAutenticacaoGpecon];LookUpFinalize[finalizeLookUpTcsEmpresaAutenticacaoGpecon];LookUpDisplayColumns[{\"IdLinxGpecon\" : \"Id Linx\", \"GrupoEconomico\" : \"Empresa / Grupo Econômico\"}];LookUpColumns[{\"IdLinxGpecon\" : true, \"GrupoEconomico\" : true}];FilterDataKey[TCS_EMPRESA_GPECON.EMPRESA.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdLinxGpecon#true##12:0##Id Linx#0#true##::LookUpTcsEmpresaAutenticacaoGpecon##true#false##TCS_EMPRESA_AUTENTICACAO#Linx.Framework.BV.Empresa#IQueryable###true#true", EdmKey="TCS_EMPRESA_GPECON.EMPRESA.ID_LINX")]
	    public Int32 IdLinxGpecon
	    {
	    	    get
	    	    {
	    	          return _IdLinxGpecon;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLinxGpecon != value)
	    	          {
	    	              this.ValidateProperty("IdLinxGpecon", value);
	    	              this.OnIdLinxGpeconChanging(value);
	    	              this.RaiseDataMemberChanging("IdLinxGpecon");
	    	              this._IdLinxGpecon = value;
	    	              this.RaiseDataMemberChanged("IdLinxGpecon");
	    	              this.OnIdLinxGpeconChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For CnpjCpf
	    partial void OnCnpjCpfChanging(string value);
	    partial void OnCnpjCpfChanged();

	    private string _CnpjCpf;

	    [DataMember(IsRequired = true, Name = "CnpjCpf", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cnpj", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[##.###.###/####-##];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_EMPRESA_GPECON.GPECON.CNPJ_CPF];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_EMPRESA_AUTENTICACAO.CNPJ_CPF")]
	    public string CnpjCpf
	    {
	    	    get
	    	    {
	    	          return _CnpjCpf;
	    	    }
	    	    set
	    	    {
	    	          if (this._CnpjCpf != value)
	    	          {
	    	              this.ValidateProperty("CnpjCpf", value);
	    	              this.OnCnpjCpfChanging(value);
	    	              this.RaiseDataMemberChanging("CnpjCpf");
	    	              this._CnpjCpf = value;
	    	              this.RaiseDataMemberChanged("CnpjCpf");
	    	              this.OnCnpjCpfChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeEmpresa
	    partial void OnNomeEmpresaChanging(string value);
	    partial void OnNomeEmpresaChanged();

	    private string _NomeEmpresa;

	    [DataMember(IsRequired = true, Name = "NomeEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Empresa", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_EMPRESA_GPECON.GPECON.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
	    public string NomeEmpresa
	    {
	    	    get
	    	    {
	    	          return _NomeEmpresa;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeEmpresa != value)
	    	          {
	    	              this.ValidateProperty("NomeEmpresa", value);
	    	              this.OnNomeEmpresaChanging(value);
	    	              this.RaiseDataMemberChanging("NomeEmpresa");
	    	              this._NomeEmpresa = value;
	    	              this.RaiseDataMemberChanged("NomeEmpresa");
	    	              this.OnNomeEmpresaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidEmpresa
	    partial void OnUidEmpresaChanging(Guid value);
	    partial void OnUidEmpresaChanged();

	    private Guid _UidEmpresa;

	    [DataMember(Name = "UidEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Empresa", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_EMPRESA_GPECON.GPECON.UID_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA")]
	    public Guid UidEmpresa
	    {
	    	    get
	    	    {
	    	          return _UidEmpresa;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidEmpresa != value)
	    	          {
	    	              this.ValidateProperty("UidEmpresa", value);
	    	              this.OnUidEmpresaChanging(value);
	    	              this.RaiseDataMemberChanging("UidEmpresa");
	    	              this._UidEmpresa = value;
	    	              this.RaiseDataMemberChanged("UidEmpresa");
	    	              this.OnUidEmpresaChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_EMPRESA_GPECON").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_EMPRESA_GPECON), QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_GPECON" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_EMPRESA_GPECON.ID_LINX_GPECON", Source = "IdGrupoEconomico", Target = "ID_LINX_GPECON", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_GPECON", RelationPropertyName = "TCS_EMPRESA_GPECON" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_EMPRESA_GPECON.GPECON.ID_LINX", Source = "IdLinx", Target = "ID_LINX", TargetKeyName = "ID_LINX", NoUpdatable = false, IsKey = true, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_AUTENTICACAO", RelationPropertyName = "GPECON" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_EMPRESA_GPECON.EMPRESA.ID_LINX", Source = "IdLinxGpecon", Target = "ID_LINX", TargetKeyName = "ID_LINX", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_AUTENTICACAO", RelationPropertyName = "EMPRESA" });

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

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Ambientes];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsAmbiente];ReadOnly[false];Entities[TCS_AMBIENTE:IdTcsAmbiente];SubQueryInfo[Select 1 From #ParentAlias#.TCS_AMBIENTE_LISTA as #Alias#];EdmEntityName[TCS_AMBIENTE];EntityRelations[TCS_APLICACAO(TCS_APLICACAO)#TCS_APLICATIVO(TCS_APLICATIVO)#TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)];EdmParentEntityName[TCS_EMPRESA_AUTENTICACAO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsAmbiente")]
	[Serializable()]
	public partial class TcsAmbienteParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For DescricaoAmbiente
	    partial void OnDescricaoAmbienteChanging(System.String value);
	    partial void OnDescricaoAmbienteChanged();

	    private System.String _DescricaoAmbiente;

	    [DataMember(IsRequired = true, Name = "DescricaoAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ambiente", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente];LookUpTitle[Seleção de (Ambiente)];LookUpQuery[executeLookUpTcsAmbiente];LookUpFinalize[finalizeLookUpTcsAmbiente];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"IdTcsAmbiente\" : false}];FilterDataKey[TCS_AMBIENTE.DESCRICAO_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoAmbiente#false##2500##Ambiente#0#true##::LookUpTcsAmbiente##false#false##TCS_AMBIENTE#Linx.Framework.BV.Empresa#IQueryable###true#false", EdmKey="TCS_AMBIENTE.DESCRICAO_AMBIENTE")]
	    public System.String DescricaoAmbiente
	    {
	    	    get
	    	    {
	    	          return _DescricaoAmbiente;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescricaoAmbiente != value)
	    	          {
	    	              this.ValidateProperty("DescricaoAmbiente", value);
	    	              this.OnDescricaoAmbienteChanging(value);
	    	              this.RaiseDataMemberChanging("DescricaoAmbiente");
	    	              this._DescricaoAmbiente = value;
	    	              this.RaiseDataMemberChanged("DescricaoAmbiente");
	    	              this.OnDescricaoAmbienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescricaoAplicacao
	    partial void OnDescricaoAplicacaoChanging(System.String value);
	    partial void OnDescricaoAplicacaoChanged();

	    private System.String _DescricaoAplicacao;

	    [DataMember(IsRequired = true, Name = "DescricaoAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicação", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicacao];LookUpTitle[Seleção de (Aplicação)];LookUpQuery[executeLookUpTcsAplicacao];LookUpFinalize[finalizeLookUpTcsAplicacao];LookUpDisplayColumns[{\"DescricaoAplicacao\" : \"Aplicação\", \"EmDesenvolvimento\" : \"Em Desenvolvimento\"}];LookUpColumns[{\"DescricaoAplicacao\" : true, \"EmDesenvolvimento\" : true}];FilterDataKey[TCS_AMBIENTE.TCS_APLICACAO.DESCRICAO_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoAplicacao#false##60:0##Aplicação#0#true##::LookUpTcsAplicacao##false#false#TCS_APLICACAO#TCS_APLICACAO#Linx.Framework.BV.Empresa#IQueryable###true#false", EdmKey="TCS_AMBIENTE.TCS_APLICACAO.DESCRICAO_APLICACAO")]
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
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicacao];LookUpTitle[Seleção de (Em Desenvolvimento)];LookUpQuery[executeLookUpTcsAplicacao];LookUpFinalize[finalizeLookUpTcsAplicacao];LookUpDisplayColumns[{\"DescricaoAplicacao\" : \"Aplicação\", \"EmDesenvolvimento\" : \"Em Desenvolvimento\"}];LookUpColumns[{\"DescricaoAplicacao\" : true, \"EmDesenvolvimento\" : true}];FilterDataKey[TCS_AMBIENTE.TCS_APLICACAO.EM_DESENVOLVIMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Boolean#EmDesenvolvimento#false##0:0##Em Desenvolvimento#1#true##::LookUpTcsAplicacao##false#false#TCS_APLICACAO#TCS_APLICACAO#Linx.Framework.BV.Empresa#IQueryable###true#false", EdmKey="TCS_AMBIENTE.TCS_APLICACAO.EM_DESENVOLVIMENTO")]
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
	    //Extensibility Partial Method Definitions For IdLinx
	    partial void OnIdLinxChanging(Int32 value);
	    partial void OnIdLinxChanged();

	    private Int32 _IdLinx;

	    [DataMember(IsRequired = true, Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Linx", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
	    public Int32 IdLinx
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
	    //Extensibility Partial Method Definitions For IdTcsAmbiente
	    partial void OnIdTcsAmbienteChanging(Int32 value);
	    partial void OnIdTcsAmbienteChanged();

	    private Int32 _IdTcsAmbiente;

	    [DataMember(IsRequired = true, Name = "IdTcsAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente];LookUpTitle[Seleção de (Id Tcs Ambiente)];LookUpQuery[executeLookUpTcsAmbiente];LookUpFinalize[finalizeLookUpTcsAmbiente];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"IdTcsAmbiente\" : false}];FilterDataKey[TCS_AMBIENTE.ID_TCS_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdTcsAmbiente#true##12:0##Id Tcs Ambiente#1#false##::LookUpTcsAmbiente##false#false##TCS_AMBIENTE#Linx.Framework.BV.Empresa#IQueryable###true#false", EdmKey="TCS_AMBIENTE.ID_TCS_AMBIENTE")]
	    public Int32 IdTcsAmbiente
	    {
	    	    get
	    	    {
	    	          return _IdTcsAmbiente;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAmbiente != value)
	    	          {
	    	              this.ValidateProperty("IdTcsAmbiente", value);
	    	              this.OnIdTcsAmbienteChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsAmbiente");
	    	              this._IdTcsAmbiente = value;
	    	              this.RaiseDataMemberChanged("IdTcsAmbiente");
	    	              this.OnIdTcsAmbienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For CnpjCpf
	    partial void OnCnpjCpfChanging(string value);
	    partial void OnCnpjCpfChanged();

	    private string _CnpjCpf;

	    [DataMember(IsRequired = true, Name = "CnpjCpf", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cnpj", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[##.###.###/####-##];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.CNPJ_CPF];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_EMPRESA_AUTENTICACAO.CNPJ_CPF")]
	    public string CnpjCpf
	    {
	    	    get
	    	    {
	    	          return _CnpjCpf;
	    	    }
	    	    set
	    	    {
	    	          if (this._CnpjCpf != value)
	    	          {
	    	              this.ValidateProperty("CnpjCpf", value);
	    	              this.OnCnpjCpfChanging(value);
	    	              this.RaiseDataMemberChanging("CnpjCpf");
	    	              this._CnpjCpf = value;
	    	              this.RaiseDataMemberChanged("CnpjCpf");
	    	              this.OnCnpjCpfChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeEmpresa
	    partial void OnNomeEmpresaChanging(string value);
	    partial void OnNomeEmpresaChanged();

	    private string _NomeEmpresa;

	    [DataMember(IsRequired = true, Name = "NomeEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Empresa", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
	    public string NomeEmpresa
	    {
	    	    get
	    	    {
	    	          return _NomeEmpresa;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeEmpresa != value)
	    	          {
	    	              this.ValidateProperty("NomeEmpresa", value);
	    	              this.OnNomeEmpresaChanging(value);
	    	              this.RaiseDataMemberChanging("NomeEmpresa");
	    	              this._NomeEmpresa = value;
	    	              this.RaiseDataMemberChanged("NomeEmpresa");
	    	              this.OnNomeEmpresaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidEmpresa
	    partial void OnUidEmpresaChanging(Guid value);
	    partial void OnUidEmpresaChanged();

	    private Guid _UidEmpresa;

	    [DataMember(Name = "UidEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Empresa", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA")]
	    public Guid UidEmpresa
	    {
	    	    get
	    	    {
	    	          return _UidEmpresa;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidEmpresa != value)
	    	          {
	    	              this.ValidateProperty("UidEmpresa", value);
	    	              this.OnUidEmpresaChanging(value);
	    	              this.RaiseDataMemberChanging("UidEmpresa");
	    	              this._UidEmpresa = value;
	    	              this.RaiseDataMemberChanged("UidEmpresa");
	    	              this.OnUidEmpresaChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_AMBIENTE").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_AMBIENTE), QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_AMBIENTE.ID_TCS_AMBIENTE", Source = "IdTcsAmbiente", Target = "ID_TCS_AMBIENTE", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE", RelationPropertyName = "TCS_AMBIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_AMBIENTE.DESCRICAO_AMBIENTE", Source = "DescricaoAmbiente", Target = "DESCRICAO_AMBIENTE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE", RelationPropertyName = "TCS_AMBIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX", Source = "IdLinx", Target = "ID_LINX", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_AUTENTICACAO", RelationPropertyName = "TCS_EMPRESA_AUTENTICACAO" });

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

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Módulos Permitidos];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsEmpresaModulo];ReadOnly[false];Entities[TCS_EMPRESA_MODULO:IdTcsEmpresaModulo];SubQueryInfo[Select 1 From #ParentAlias#.TCS_EMPRESA_MODULO_LISTA as #Alias#];EdmEntityName[TCS_EMPRESA_MODULO];EntityRelations[TCS_MODULO_AUTORIZACAO(TCS_MODULO_AUTORIZACAO)#TCS_APLICATIVO(TCS_APLICATIVO)#TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)];EdmParentEntityName[TCS_EMPRESA_AUTENTICACAO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsEmpresaModulo")]
	[Serializable()]
	public partial class TcsEmpresaModuloParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For DescModulo
	    partial void OnDescModuloChanging(System.String value);
	    partial void OnDescModuloChanged();

	    private System.String _DescModulo;

	    [DataMember(IsRequired = true, Name = "DescModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Módulo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsModuloAutorizacao];LookUpTitle[Seleção de (Módulo)];LookUpQuery[executeLookUpTcsModuloAutorizacao];LookUpFinalize[finalizeLookUpTcsModuloAutorizacao];LookUpDisplayColumns[{\"DescModulo\" : \"Módulo\", \"IdModulo\" : \"Id Modulo\", \"DescricaoAplicativo\" : \"Aplicativo\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\"}];LookUpColumns[{\"DescModulo\" : true, \"IdModulo\" : false, \"DescricaoAplicativo\" : true, \"IdTcsAplicativo\" : false}];FilterDataKey[TCS_EMPRESA_MODULO.TCS_MODULO_AUTORIZACAO.DESC_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescModulo#false##60:0##Módulo#0#true##::LookUpTcsModuloAutorizacao##true#false#TCS_MODULO_AUTORIZACAO#TCS_MODULO_AUTORIZACAO#Linx.Framework.BV.Empresa#IQueryable#DescricaoAplicativo,IdTcsAplicativo[DescricaoAplicativo,IdTcsAplicativo]#DescModulo[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdModulo[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo]#true#true", EdmKey="TCS_EMPRESA_MODULO.TCS_MODULO_AUTORIZACAO.DESC_MODULO")]
	    public System.String DescModulo
	    {
	    	    get
	    	    {
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
	    //Extensibility Partial Method Definitions For DescricaoAplicativo
	    partial void OnDescricaoAplicativoChanging(System.String value);
	    partial void OnDescricaoAplicativoChanged();

	    private System.String _DescricaoAplicativo;

	    [DataMember(Name = "DescricaoAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicativo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsModuloAutorizacao];LookUpTitle[Seleção de (Aplicativo)];LookUpQuery[executeLookUpTcsModuloAutorizacao];LookUpFinalize[finalizeLookUpTcsModuloAutorizacao];LookUpDisplayColumns[{\"DescModulo\" : \"Módulo\", \"IdModulo\" : \"Id Modulo\", \"DescricaoAplicativo\" : \"Aplicativo\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\"}];LookUpColumns[{\"DescModulo\" : true, \"IdModulo\" : false, \"DescricaoAplicativo\" : true, \"IdTcsAplicativo\" : false}];FilterDataKey[TCS_EMPRESA_MODULO.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoAplicativo#false##250:0##Aplicativo#2#true##::LookUpTcsModuloAutorizacao##true#false#TCS_MODULO_AUTORIZACAO#TCS_MODULO_AUTORIZACAO#Linx.Framework.BV.Empresa#IQueryable#DescricaoAplicativo,IdTcsAplicativo[DescricaoAplicativo,IdTcsAplicativo]#DescModulo[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdModulo[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo]#true#true", EdmKey="TCS_EMPRESA_MODULO.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO")]
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
	    //Extensibility Partial Method Definitions For IdLinx
	    partial void OnIdLinxChanging(Int32 value);
	    partial void OnIdLinxChanged();

	    private Int32 _IdLinx;

	    [DataMember(IsRequired = true, Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Linx", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_EMPRESA_MODULO.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_EMPRESA_MODULO.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
	    public Int32 IdLinx
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
	    //Extensibility Partial Method Definitions For IdModulo
	    partial void OnIdModuloChanging(Int64 value);
	    partial void OnIdModuloChanged();

	    private Int64 _IdModulo;

	    [DataMember(IsRequired = true, Name = "IdModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsModuloAutorizacao];LookUpTitle[Seleção de (Id Modulo)];LookUpQuery[executeLookUpTcsModuloAutorizacao];LookUpFinalize[finalizeLookUpTcsModuloAutorizacao];LookUpDisplayColumns[{\"DescModulo\" : \"Módulo\", \"IdModulo\" : \"Id Modulo\", \"DescricaoAplicativo\" : \"Aplicativo\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\"}];LookUpColumns[{\"DescModulo\" : true, \"IdModulo\" : false, \"DescricaoAplicativo\" : true, \"IdTcsAplicativo\" : false}];FilterDataKey[TCS_EMPRESA_MODULO.TCS_MODULO_AUTORIZACAO.ID_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdModulo#true##24:0##Id Modulo#1#false##::LookUpTcsModuloAutorizacao##true#false#TCS_MODULO_AUTORIZACAO#TCS_MODULO_AUTORIZACAO#Linx.Framework.BV.Empresa#IQueryable#DescricaoAplicativo,IdTcsAplicativo[DescricaoAplicativo,IdTcsAplicativo]#DescModulo[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdModulo[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo]#true#true", EdmKey="TCS_EMPRESA_MODULO.TCS_MODULO_AUTORIZACAO.ID_MODULO")]
	    public Int64 IdModulo
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
	    //Extensibility Partial Method Definitions For IdTcsAplicativo
	    partial void OnIdTcsAplicativoChanging(Int32 value);
	    partial void OnIdTcsAplicativoChanged();

	    private Int32 _IdTcsAplicativo;

	    [DataMember(IsRequired = true, Name = "IdTcsAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Aplicativo", Description="", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsModuloAutorizacao];LookUpTitle[Seleção de (Id Tcs Aplicativo)];LookUpQuery[executeLookUpTcsModuloAutorizacao];LookUpFinalize[finalizeLookUpTcsModuloAutorizacao];LookUpDisplayColumns[{\"DescModulo\" : \"Módulo\", \"IdModulo\" : \"Id Modulo\", \"DescricaoAplicativo\" : \"Aplicativo\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\"}];LookUpColumns[{\"DescModulo\" : true, \"IdModulo\" : false, \"DescricaoAplicativo\" : true, \"IdTcsAplicativo\" : false}];FilterDataKey[TCS_EMPRESA_MODULO.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdTcsAplicativo#false##12:0##Id Tcs Aplicativo#3#false##::LookUpTcsModuloAutorizacao##true#false#TCS_MODULO_AUTORIZACAO#TCS_MODULO_AUTORIZACAO#Linx.Framework.BV.Empresa#IQueryable#DescricaoAplicativo,IdTcsAplicativo[DescricaoAplicativo,IdTcsAplicativo]#DescModulo[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdModulo[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo]#true#true", EdmKey="TCS_EMPRESA_MODULO.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO")]
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
	    //Extensibility Partial Method Definitions For IdTcsEmpresaModulo
	    partial void OnIdTcsEmpresaModuloChanging(Int32 value);
	    partial void OnIdTcsEmpresaModuloChanged();

	    private Int32 _IdTcsEmpresaModulo;

	    [DataMember(IsRequired = true, Name = "IdTcsEmpresaModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Empresa Modulo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_EMPRESA_MODULO.ID_TCS_EMPRESA_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_EMPRESA_MODULO.ID_TCS_EMPRESA_MODULO")]
	    public Int32 IdTcsEmpresaModulo
	    {
	    	    get
	    	    {
	    	          return _IdTcsEmpresaModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsEmpresaModulo != value)
	    	          {
	    	              this.ValidateProperty("IdTcsEmpresaModulo", value);
	    	              this.OnIdTcsEmpresaModuloChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsEmpresaModulo");
	    	              this._IdTcsEmpresaModulo = value;
	    	              this.RaiseDataMemberChanged("IdTcsEmpresaModulo");
	    	              this.OnIdTcsEmpresaModuloChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For CnpjCpf
	    partial void OnCnpjCpfChanging(string value);
	    partial void OnCnpjCpfChanged();

	    private string _CnpjCpf;

	    [DataMember(IsRequired = true, Name = "CnpjCpf", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cnpj", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[##.###.###/####-##];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_EMPRESA_MODULO.TCS_EMPRESA_AUTENTICACAO.CNPJ_CPF];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_EMPRESA_AUTENTICACAO.CNPJ_CPF")]
	    public string CnpjCpf
	    {
	    	    get
	    	    {
	    	          return _CnpjCpf;
	    	    }
	    	    set
	    	    {
	    	          if (this._CnpjCpf != value)
	    	          {
	    	              this.ValidateProperty("CnpjCpf", value);
	    	              this.OnCnpjCpfChanging(value);
	    	              this.RaiseDataMemberChanging("CnpjCpf");
	    	              this._CnpjCpf = value;
	    	              this.RaiseDataMemberChanged("CnpjCpf");
	    	              this.OnCnpjCpfChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeEmpresa
	    partial void OnNomeEmpresaChanging(string value);
	    partial void OnNomeEmpresaChanged();

	    private string _NomeEmpresa;

	    [DataMember(IsRequired = true, Name = "NomeEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Empresa", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_EMPRESA_MODULO.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
	    public string NomeEmpresa
	    {
	    	    get
	    	    {
	    	          return _NomeEmpresa;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeEmpresa != value)
	    	          {
	    	              this.ValidateProperty("NomeEmpresa", value);
	    	              this.OnNomeEmpresaChanging(value);
	    	              this.RaiseDataMemberChanging("NomeEmpresa");
	    	              this._NomeEmpresa = value;
	    	              this.RaiseDataMemberChanged("NomeEmpresa");
	    	              this.OnNomeEmpresaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidEmpresa
	    partial void OnUidEmpresaChanging(Guid value);
	    partial void OnUidEmpresaChanged();

	    private Guid _UidEmpresa;

	    [DataMember(Name = "UidEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Empresa", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_EMPRESA_MODULO.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA")]
	    public Guid UidEmpresa
	    {
	    	    get
	    	    {
	    	          return _UidEmpresa;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidEmpresa != value)
	    	          {
	    	              this.ValidateProperty("UidEmpresa", value);
	    	              this.OnUidEmpresaChanging(value);
	    	              this.RaiseDataMemberChanging("UidEmpresa");
	    	              this._UidEmpresa = value;
	    	              this.RaiseDataMemberChanged("UidEmpresa");
	    	              this.OnUidEmpresaChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_EMPRESA_MODULO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_EMPRESA_MODULO), QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_MODULO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_EMPRESA_MODULO.ID_TCS_EMPRESA_MODULO", Source = "IdTcsEmpresaModulo", Target = "ID_TCS_EMPRESA_MODULO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_MODULO", RelationPropertyName = "TCS_EMPRESA_MODULO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_EMPRESA_MODULO.TCS_EMPRESA_AUTENTICACAO.ID_LINX", Source = "IdLinx", Target = "ID_LINX", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_AUTENTICACAO", RelationPropertyName = "TCS_EMPRESA_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_EMPRESA_MODULO.TCS_MODULO_AUTORIZACAO.ID_MODULO", Source = "IdModulo", Target = "ID_MODULO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_MODULO_AUTORIZACAO", RelationPropertyName = "TCS_MODULO_AUTORIZACAO" });

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

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Usuários];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdUsuario];ReadOnly[true];Entities[TCS_USUARIO_AUTENTICACAO:IdUsuario];SubQueryInfo[Select 1 From #ParentAlias#.TCS_USUARIO_AUTENTICACAO_LISTA as #Alias#];EdmEntityName[TCS_USUARIO_AUTENTICACAO];EntityRelations[TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)];EdmParentEntityName[TCS_EMPRESA_AUTENTICACAO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioAutenticacao")]
	[Serializable()]
	public partial class TcsUsuarioAutenticacaoParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For IdLinx
	    partial void OnIdLinxChanging(Int32 value);
	    partial void OnIdLinxChanged();

	    private Int32 _IdLinx;

	    [DataMember(IsRequired = true, Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Linx", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacao];LookUpTitle[Seleção de (ID Linx)];LookUpQuery[executeLookUpTcsUsuarioAutenticacao];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacao];LookUpDisplayColumns[{\"IdLinx\" : \"Id Linx\", \"IdUsuario\" : \"Id Usuario\", \"NomeUsuario\" : \"Nome Usuário\", \"NomeAutenticacao\" : \"Usuário Autenticação\"}];LookUpColumns[{\"IdLinx\" : false, \"IdUsuario\" : false, \"NomeUsuario\" : true, \"NomeAutenticacao\" : true}];FilterDataKey[TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdLinx#true##12:0##Id Linx#0#false##::LookUpTcsUsuarioAutenticacao##false#false##TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.Empresa#IQueryable#IdLinx[IdLinx]#IdUsuario[IdLinx=IdLinx];NomeUsuario[IdLinx=IdLinx];NomeAutenticacao[IdLinx=IdLinx]#true#false", EdmKey="TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
	    public Int32 IdLinx
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
	    //Extensibility Partial Method Definitions For IdUsuario
	    partial void OnIdUsuarioChanging(Int64 value);
	    partial void OnIdUsuarioChanged();

	    private Int64 _IdUsuario;

	    [DataMember(IsRequired = true, Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 12, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacao];LookUpTitle[Seleção de (Id Usuario)];LookUpQuery[executeLookUpTcsUsuarioAutenticacao];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacao];LookUpDisplayColumns[{\"IdLinx\" : \"Id Linx\", \"IdUsuario\" : \"Id Usuario\", \"NomeUsuario\" : \"Nome Usuário\", \"NomeAutenticacao\" : \"Usuário Autenticação\"}];LookUpColumns[{\"IdLinx\" : false, \"IdUsuario\" : false, \"NomeUsuario\" : true, \"NomeAutenticacao\" : true}];FilterDataKey[TCS_USUARIO_AUTENTICACAO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdUsuario#true##24:0##Id Usuario#1#false##::LookUpTcsUsuarioAutenticacao##false#false##TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.Empresa#IQueryable#IdLinx[IdLinx]#IdUsuario[IdLinx=IdLinx];NomeUsuario[IdLinx=IdLinx];NomeAutenticacao[IdLinx=IdLinx]#true#false", EdmKey="TCS_USUARIO_AUTENTICACAO.ID_USUARIO")]
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
	    //Extensibility Partial Method Definitions For NomeAutenticacao
	    partial void OnNomeAutenticacaoChanging(System.String value);
	    partial void OnNomeAutenticacaoChanged();

	    private System.String _NomeAutenticacao;

	    [DataMember(IsRequired = true, Name = "NomeAutenticacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário Autenticação", Description="", Order = 20, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacao];LookUpTitle[Seleção de (Usuário Autenticação)];LookUpQuery[executeLookUpTcsUsuarioAutenticacao];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacao];LookUpDisplayColumns[{\"IdLinx\" : \"Id Linx\", \"IdUsuario\" : \"Id Usuario\", \"NomeUsuario\" : \"Nome Usuário\", \"NomeAutenticacao\" : \"Usuário Autenticação\"}];LookUpColumns[{\"IdLinx\" : false, \"IdUsuario\" : false, \"NomeUsuario\" : true, \"NomeAutenticacao\" : true}];FilterDataKey[TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeAutenticacao#false##2500##Usuário Autenticação#3#true##::LookUpTcsUsuarioAutenticacao##false#false##TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.Empresa#IQueryable#IdLinx[IdLinx]#IdUsuario[IdLinx=IdLinx];NomeUsuario[IdLinx=IdLinx];NomeAutenticacao[IdLinx=IdLinx]#true#false", EdmKey="TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO")]
	    public System.String NomeAutenticacao
	    {
	    	    get
	    	    {
	    	          return _NomeAutenticacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeAutenticacao != value)
	    	          {
	    	              this.ValidateProperty("NomeAutenticacao", value);
	    	              this.OnNomeAutenticacaoChanging(value);
	    	              this.RaiseDataMemberChanging("NomeAutenticacao");
	    	              this._NomeAutenticacao = value;
	    	              this.RaiseDataMemberChanged("NomeAutenticacao");
	    	              this.OnNomeAutenticacaoChanged();
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
	    [Display(Name = "Nome Usuário", Description="", Order = 22, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacao];LookUpTitle[Seleção de (Nome Usuário)];LookUpQuery[executeLookUpTcsUsuarioAutenticacao];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacao];LookUpDisplayColumns[{\"IdLinx\" : \"Id Linx\", \"IdUsuario\" : \"Id Usuario\", \"NomeUsuario\" : \"Nome Usuário\", \"NomeAutenticacao\" : \"Usuário Autenticação\"}];LookUpColumns[{\"IdLinx\" : false, \"IdUsuario\" : false, \"NomeUsuario\" : true, \"NomeAutenticacao\" : true}];FilterDataKey[TCS_USUARIO_AUTENTICACAO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeUsuario#false##2500##Nome Usuário#2#true##::LookUpTcsUsuarioAutenticacao##false#false##TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.Empresa#IQueryable#IdLinx[IdLinx]#IdUsuario[IdLinx=IdLinx];NomeUsuario[IdLinx=IdLinx];NomeAutenticacao[IdLinx=IdLinx]#true#false", EdmKey="TCS_USUARIO_AUTENTICACAO.NOME_USUARIO")]
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
	    //Extensibility Partial Method Definitions For CnpjCpf
	    partial void OnCnpjCpfChanging(string value);
	    partial void OnCnpjCpfChanged();

	    private string _CnpjCpf;

	    [DataMember(IsRequired = true, Name = "CnpjCpf", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cnpj", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[##.###.###/####-##];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.CNPJ_CPF];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_EMPRESA_AUTENTICACAO.CNPJ_CPF")]
	    public string CnpjCpf
	    {
	    	    get
	    	    {
	    	          return _CnpjCpf;
	    	    }
	    	    set
	    	    {
	    	          if (this._CnpjCpf != value)
	    	          {
	    	              this.ValidateProperty("CnpjCpf", value);
	    	              this.OnCnpjCpfChanging(value);
	    	              this.RaiseDataMemberChanging("CnpjCpf");
	    	              this._CnpjCpf = value;
	    	              this.RaiseDataMemberChanged("CnpjCpf");
	    	              this.OnCnpjCpfChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeEmpresa
	    partial void OnNomeEmpresaChanging(string value);
	    partial void OnNomeEmpresaChanged();

	    private string _NomeEmpresa;

	    [DataMember(IsRequired = true, Name = "NomeEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Empresa", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
	    public string NomeEmpresa
	    {
	    	    get
	    	    {
	    	          return _NomeEmpresa;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeEmpresa != value)
	    	          {
	    	              this.ValidateProperty("NomeEmpresa", value);
	    	              this.OnNomeEmpresaChanging(value);
	    	              this.RaiseDataMemberChanging("NomeEmpresa");
	    	              this._NomeEmpresa = value;
	    	              this.RaiseDataMemberChanged("NomeEmpresa");
	    	              this.OnNomeEmpresaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidEmpresa
	    partial void OnUidEmpresaChanging(Guid value);
	    partial void OnUidEmpresaChanged();

	    private Guid _UidEmpresa;

	    [DataMember(Name = "UidEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Empresa", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA")]
	    public Guid UidEmpresa
	    {
	    	    get
	    	    {
	    	          return _UidEmpresa;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidEmpresa != value)
	    	          {
	    	              this.ValidateProperty("UidEmpresa", value);
	    	              this.OnUidEmpresaChanging(value);
	    	              this.RaiseDataMemberChanging("UidEmpresa");
	    	              this._UidEmpresa = value;
	    	              this.RaiseDataMemberChanged("UidEmpresa");
	    	              this.OnUidEmpresaChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_USUARIO_AUTENTICACAO), QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.NOME_USUARIO", Source = "NomeUsuario", Target = "NOME_USUARIO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO", Source = "NomeAutenticacao", Target = "NOME_AUTENTICACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.ID_LINX", Source = "IdLinx", Target = "ID_LINX", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_AUTENTICACAO", RelationPropertyName = "TCS_EMPRESA_AUTENTICACAO" });

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
	[DomainIdentifier("ProcessorOverviewEmpresaDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class EmpresaDomainService : DomainService, IDataServiceContext 
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

		
	    public EmpresaDomainService() : this("", null, null) { }
	    public EmpresaDomainService(string connectionString) : this(connectionString, null, null) { }
	    public EmpresaDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public EmpresaDomainService(Linx.Framework.Autorizacao.BM.AutorizacaoContext dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public EmpresaDomainService(string connectionString, Linx.Framework.Autorizacao.BM.AutorizacaoContext dataContext, Dictionary<string, string> headers) : base() 
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
	
	    
	        foreach (ChangeSetEntry entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsEmpresaAutenticacao))
	        {
	            ((TcsEmpresaAutenticacao)entry.Entity).OnSavingChanges(this, changeSet.GetChangeOperation(entry.Entity));
	        }
        
	        foreach (ChangeSetEntry entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsEmpresaGpecon))
	        {
	            ((TcsEmpresaGpecon)entry.Entity).OnSavingChanges(this, changeSet.GetChangeOperation(entry.Entity));
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
	
	
	        TcsEmpresaAutenticacao.OnTransactedContextChanges(this, changeSet.ChangeSetEntries.Where(e => e.Entity is TcsEmpresaAutenticacao).ToArray());
    
	        TcsEmpresaModulo.OnTransactedContextChanges(this, changeSet.ChangeSetEntries.Where(e => e.Entity is TcsEmpresaModulo).ToArray());
    	
	    }
		
	    #endregion Entity Event Call Definitions
	
	    #region Transaction Control.
	
	    TransactionScope transactionScope = null;	
	
	    //Adjust Hierarchy Composition
	    private ChangeSet AdjustHierarchyForSaving(ChangeSet changeSet)
	    {

		
 
 	        bool createNewChangeSet = false;
 
 	        //Adjust data hierarchy
 	        var _TcsEmpresaAutenticacaoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsEmpresaAutenticacao && e.Entity.GetType().Name == "TcsEmpresaAutenticacao" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _TcsEmpresaAutenticacaoElements)
 	           if (((TcsEmpresaAutenticacao)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 
 	        //Remove inconsistent details
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsEmpresaGpecon && e.Entity.GetType().Name == "TcsEmpresaGpecon" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsAmbiente && e.Entity.GetType().Name == "TcsAmbiente" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsEmpresaModulo && e.Entity.GetType().Name == "TcsEmpresaModulo" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioAutenticacao && e.Entity.GetType().Name == "TcsUsuarioAutenticacao" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
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
	    //Get All LookUpTcsEmpresaAutenticacaoGpecon.
	    public IQueryable<LookUpTcsEmpresaAutenticacaoGpecon> GetAllLookUpTcsEmpresaAutenticacaoGpecon()
	    {
	        return this.GetLookUpTcsEmpresaAutenticacaoGpecon(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsEmpresaAutenticacaoGpecon By EntitySearch.
	    public IQueryable<LookUpTcsEmpresaAutenticacaoGpecon> GetLookUpTcsEmpresaAutenticacaoGpeconByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsEmpresaAutenticacaoGpecon(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsEmpresaAutenticacaoGpecon.
	    public IQueryable<LookUpTcsEmpresaAutenticacaoGpecon> GetLookUpTcsEmpresaAutenticacaoGpecon(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TCS_EMPRESA_AUTENTICACAO" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsEmpresaAutenticacaoGpecon";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsEmpresaAutenticacaoGpecon));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsEmpresaAutenticacaoGpecon> query =  
	
	            (from entity in this.DbContext.TCS_EMPRESA_AUTENTICACAO.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpTcsEmpresaAutenticacaoGpecon()		
	            {
	            
                IdLinxGpecon = entity.ID_LINX
                , GrupoEconomico = entity.NOME_EMPRESA
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
	            });

	            
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpTcsAmbiente.
	    public IQueryable<LookUpTcsAmbiente> GetAllLookUpTcsAmbiente()
	    {
	        return this.GetLookUpTcsAmbiente(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsAmbiente By EntitySearch.
	    public IQueryable<LookUpTcsAmbiente> GetLookUpTcsAmbienteByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsAmbiente(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsAmbiente.
	    public IQueryable<LookUpTcsAmbiente> GetLookUpTcsAmbiente(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TCS_AMBIENTE" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsAmbiente";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsAmbiente));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsAmbiente> query =  
	
	            (from entity in this.DbContext.TCS_AMBIENTE.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpTcsAmbiente()		
	            {
	            
                DescricaoAmbiente = entity.DESCRICAO_AMBIENTE
                , IdTcsAmbiente = entity.ID_TCS_AMBIENTE
	            });

	            
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpTcsModuloAutorizacao.
	    public IQueryable<LookUpTcsModuloAutorizacao> GetAllLookUpTcsModuloAutorizacao()
	    {
	        return this.GetLookUpTcsModuloAutorizacao(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsModuloAutorizacao By EntitySearch.
	    public IQueryable<LookUpTcsModuloAutorizacao> GetLookUpTcsModuloAutorizacaoByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsModuloAutorizacao(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsModuloAutorizacao.
	    public IQueryable<LookUpTcsModuloAutorizacao> GetLookUpTcsModuloAutorizacao(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TCS_MODULO_AUTORIZACAO" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsModuloAutorizacao";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsModuloAutorizacao));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsModuloAutorizacao> query =  
	
	            (from entity in this.DbContext.TCS_MODULO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_APLICATIVO
	            
	            select new LookUpTcsModuloAutorizacao()		
	            {
	            
                DescModulo = entity.DESC_MODULO
                , IdModulo = entity.ID_MODULO
                , DescricaoAplicativo = entityAl1.DESCRICAO_APLICATIVO
                , IdTcsAplicativo = entityAl1.ID_TCS_APLICATIVO
	            });

	            
            //Inner Group Definition
            if (propertyName.InList("DescricaoAplicativo", "IdTcsAplicativo"))
            {
               query = (from r in query select new LookUpTcsModuloAutorizacao() {
               DescModulo = ""
               , IdModulo = default(Int64)
               , DescricaoAplicativo = r.DescricaoAplicativo
               , IdTcsAplicativo = r.IdTcsAplicativo
                }).Distinct();
            }
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpTcsUsuarioAutenticacao.
	    public IQueryable<LookUpTcsUsuarioAutenticacao> GetAllLookUpTcsUsuarioAutenticacao()
	    {
	        return this.GetLookUpTcsUsuarioAutenticacao(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsUsuarioAutenticacao By EntitySearch.
	    public IQueryable<LookUpTcsUsuarioAutenticacao> GetLookUpTcsUsuarioAutenticacaoByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsUsuarioAutenticacao(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsUsuarioAutenticacao.
	    public IQueryable<LookUpTcsUsuarioAutenticacao> GetLookUpTcsUsuarioAutenticacao(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TCS_USUARIO_AUTENTICACAO" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsUsuarioAutenticacao";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsUsuarioAutenticacao));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsUsuarioAutenticacao> query =  
	
	            (from entity in this.DbContext.TCS_USUARIO_AUTENTICACAO.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpTcsUsuarioAutenticacao()		
	            {
	            
                IdLinx = entity.TCS_EMPRESA_AUTENTICACAO.ID_LINX
                , IdUsuario = entity.ID_USUARIO
                , NomeUsuario = entity.NOME_USUARIO
                , NomeAutenticacao = entity.NOME_AUTENTICACAO
	            });

	            
            //Inner Group Definition
            if (propertyName.InList("IdLinx"))
            {
               query = (from r in query select new LookUpTcsUsuarioAutenticacao() {
               IdLinx = r.IdLinx
               , IdUsuario = default(Int64)
               , NomeUsuario = ""
               , NomeAutenticacao = ""
                }).Distinct();
            }
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpTcsEmpresaAutenticacaoP.
	    public IQueryable<LookUpTcsEmpresaAutenticacaoP> GetAllLookUpTcsEmpresaAutenticacaoP()
	    {
	        return this.GetLookUpTcsEmpresaAutenticacaoP(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsEmpresaAutenticacaoP By EntitySearch.
	    public IQueryable<LookUpTcsEmpresaAutenticacaoP> GetLookUpTcsEmpresaAutenticacaoPByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsEmpresaAutenticacaoP(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsEmpresaAutenticacaoP.
	    public IQueryable<LookUpTcsEmpresaAutenticacaoP> GetLookUpTcsEmpresaAutenticacaoP(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TCS_EMPRESA_AUTENTICACAO" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsEmpresaAutenticacaoP";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsEmpresaAutenticacaoP));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsEmpresaAutenticacaoP> query =  
	
	            (from entity in this.DbContext.TCS_EMPRESA_AUTENTICACAO.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpTcsEmpresaAutenticacaoP()		
	            {
	            
                IdLinx = entity.ID_LINX
                , NomeEmpresa = entity.NOME_EMPRESA
                , UidEmpresa = entity.UID_EMPRESA
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
	
		

	        if (entityName.InList("Linx.Framework.BV.Empresa.TcsEmpresaAutenticacao"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsEmpresaAutenticacao",
	        			NameSpace = "Linx.Framework.BV.Empresa",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsEmpresaAutenticacao",
	        			ClearMethodName = "ClearTcsEmpresaAutenticacao",
	        			QueryMethodName  = "GetPagedTcsEmpresaAutenticacao",	
	        			CountingMethodName  = "GetTcsEmpresaAutenticacao" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Empresa.TcsEmpresaAutenticacao"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Empresa.TcsEmpresaAutenticacao"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Empresa.TcsEmpresaAutenticacao", "Linx.Framework.BV.Empresa.TcsEmpresaGpecon"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsEmpresaGpecon" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.Empresa",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsEmpresaAutenticacao",	
	        			DisplayName = "Empresa / Grupo Econômico",
	        			ClearMethodName = "ClearTcsEmpresaGpecon" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsEmpresaGpecon" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsEmpresaGpecon" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Empresa.TcsEmpresaGpecon"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Empresa.TcsEmpresaGpecon" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Empresa.TcsEmpresaAutenticacao", "Linx.Framework.BV.Empresa.TcsAmbiente"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsAmbiente" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.Empresa",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsEmpresaAutenticacao",	
	        			DisplayName = "Ambientes",
	        			ClearMethodName = "ClearTcsAmbiente" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsAmbiente" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsAmbiente" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Empresa.TcsAmbiente"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Empresa.TcsAmbiente" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Empresa.TcsEmpresaAutenticacao", "Linx.Framework.BV.Empresa.TcsEmpresaModulo"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsEmpresaModulo" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.Empresa",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsEmpresaAutenticacao",	
	        			DisplayName = "Módulos Permitidos",
	        			ClearMethodName = "ClearTcsEmpresaModulo" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsEmpresaModulo" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsEmpresaModulo" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Empresa.TcsEmpresaModulo"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Empresa.TcsEmpresaModulo" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Empresa.TcsEmpresaAutenticacao", "Linx.Framework.BV.Empresa.TcsUsuarioAutenticacao"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsUsuarioAutenticacao" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.Empresa",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsEmpresaAutenticacao",	
	        			DisplayName = "Usuários",
	        			ClearMethodName = "ClearTcsUsuarioAutenticacao" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsUsuarioAutenticacao" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsUsuarioAutenticacao" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Empresa.TcsUsuarioAutenticacao"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Empresa.TcsUsuarioAutenticacao" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Empresa.TcsEmpresaGpeconP"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsEmpresaGpeconP",
	        			NameSpace = "Linx.Framework.BV.Empresa",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsEmpresaGpeconP",
	        			ClearMethodName = "ClearTcsEmpresaGpeconP",
	        			QueryMethodName  = "GetPagedTcsEmpresaGpeconP",	
	        			CountingMethodName  = "GetTcsEmpresaGpeconP" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Empresa.TcsEmpresaGpeconP"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Empresa.TcsEmpresaGpeconP"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Empresa.TcsEmpresaAutenticacaoP"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsEmpresaAutenticacaoP",
	        			NameSpace = "Linx.Framework.BV.Empresa",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsEmpresaAutenticacaoP",
	        			ClearMethodName = "ClearTcsEmpresaAutenticacaoP",
	        			QueryMethodName  = "GetPagedTcsEmpresaAutenticacaoP",	
	        			CountingMethodName  = "GetTcsEmpresaAutenticacaoP" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Empresa.TcsEmpresaAutenticacaoP"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Empresa.TcsEmpresaAutenticacaoP"), forceAll: forceAll)
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

         		    return new string[] { "Framework_EmpresaClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.EmpresaClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Framework_empresaService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.empresaService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear TcsEmpresaAutenticacao.
	    public IEnumerable<TcsEmpresaAutenticacao> ClearTcsEmpresaAutenticacao()
	    {
	        List<TcsEmpresaAutenticacao> result = new List<TcsEmpresaAutenticacao>();
	        result.Add(new TcsEmpresaAutenticacao());	
			
	        result[0].TcsEmpresaGpeconList = new List<TcsEmpresaGpecon>();
	        ((List<TcsEmpresaGpecon>)result[0].TcsEmpresaGpeconList).Add(new TcsEmpresaGpecon());
			
	        result[0].TcsAmbienteList = new List<TcsAmbiente>();
	        ((List<TcsAmbiente>)result[0].TcsAmbienteList).Add(new TcsAmbiente());
			
	        result[0].TcsEmpresaModuloList = new List<TcsEmpresaModulo>();
	        ((List<TcsEmpresaModulo>)result[0].TcsEmpresaModuloList).Add(new TcsEmpresaModulo());
			
	        result[0].TcsUsuarioAutenticacaoList = new List<TcsUsuarioAutenticacao>();
	        ((List<TcsUsuarioAutenticacao>)result[0].TcsUsuarioAutenticacaoList).Add(new TcsUsuarioAutenticacao());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsEmpresaGpecon.
	    public IEnumerable<TcsEmpresaGpecon> ClearTcsEmpresaGpecon()
	    {
	        List<TcsEmpresaGpecon> result = new List<TcsEmpresaGpecon>();
	        result.Add(new TcsEmpresaGpecon());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsAmbiente.
	    public IEnumerable<TcsAmbiente> ClearTcsAmbiente()
	    {
	        List<TcsAmbiente> result = new List<TcsAmbiente>();
	        result.Add(new TcsAmbiente());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsEmpresaModulo.
	    public IEnumerable<TcsEmpresaModulo> ClearTcsEmpresaModulo()
	    {
	        List<TcsEmpresaModulo> result = new List<TcsEmpresaModulo>();
	        result.Add(new TcsEmpresaModulo());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsUsuarioAutenticacao.
	    public IEnumerable<TcsUsuarioAutenticacao> ClearTcsUsuarioAutenticacao()
	    {
	        List<TcsUsuarioAutenticacao> result = new List<TcsUsuarioAutenticacao>();
	        result.Add(new TcsUsuarioAutenticacao());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsEmpresaGpeconP.
	    public IEnumerable<TcsEmpresaGpeconP> ClearTcsEmpresaGpeconP()
	    {
	        List<TcsEmpresaGpeconP> result = new List<TcsEmpresaGpeconP>();
	        result.Add(new TcsEmpresaGpeconP());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsEmpresaAutenticacaoP.
	    public IEnumerable<TcsEmpresaAutenticacaoP> ClearTcsEmpresaAutenticacaoP()
	    {
	        List<TcsEmpresaAutenticacaoP> result = new List<TcsEmpresaAutenticacaoP>();
	        result.Add(new TcsEmpresaAutenticacaoP());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsEmpresaAutenticacao.
	    public IQueryable<TcsEmpresaAutenticacao> GetTcsEmpresaAutenticacao()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsEmpresaAutenticacao> result = 
	            (from entity0 in this.DbContext.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsEmpresaAutenticacao()		
	            {
	            
                CnpjCpf = entity0.CNPJ_CPF
                , IdLinx = entity0.ID_LINX
                , NomeEmpresa = entity0.NOME_EMPRESA
                , UidEmpresa = entity0.UID_EMPRESA
			
                ,TcsEmpresaGpeconList = 
	                        (from entity1 in entity0.EMPRESA_LISTA
                                  let entity1Al2 = entity1.GPECON
                                  let entity1Al1 = entity1.EMPRESA
	                        
	                        	
	                        select new TcsEmpresaGpecon()
	                        {
	                        
                                GrupoEconomico = entity1Al1.NOME_EMPRESA
                                , IdGrupoEconomico = entity1.ID_LINX_GPECON
                                , IdLinx = entity1Al2.ID_LINX
                                , IdLinxGpecon = entity1Al1.ID_LINX
		
	                        }
	                        )
			
                ,TcsAmbienteList = 
	                        (from entity1 in entity0.TCS_AMBIENTE_LISTA
                                  let entity1Al1 = entity1.TCS_APLICACAO
                                  let entity1Al2 = entity1.TCS_EMPRESA_AUTENTICACAO
	                        
	                        	
	                        select new TcsAmbiente()
	                        {
	                        
                                DescricaoAmbiente = entity1.DESCRICAO_AMBIENTE
                                , DescricaoAplicacao = entity1Al1.DESCRICAO_APLICACAO
                                , EmDesenvolvimento = entity1Al1.EM_DESENVOLVIMENTO
                                , IdLinx = entity1Al2.ID_LINX
                                , IdTcsAmbiente = entity1.ID_TCS_AMBIENTE
		
	                        }
	                        )
			
                ,TcsEmpresaModuloList = 
	                        (from entity1 in entity0.TCS_EMPRESA_MODULO_LISTA
                                  let entity1Al1 = entity1.TCS_MODULO_AUTORIZACAO
                                  let entity1Al3 = entity1.TCS_EMPRESA_AUTENTICACAO
                                  let entity1Al2 = entity1.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO
	                        
	                        	
	                        select new TcsEmpresaModulo()
	                        {
	                        
                                DescModulo = entity1Al1.DESC_MODULO
                                , DescricaoAplicativo = entity1Al2.DESCRICAO_APLICATIVO
                                , IdLinx = entity1Al3.ID_LINX
                                , IdModulo = entity1Al1.ID_MODULO
                                , IdTcsAplicativo = entity1Al2.ID_TCS_APLICATIVO
                                , IdTcsEmpresaModulo = entity1.ID_TCS_EMPRESA_MODULO
		
	                        }
	                        )
			
                ,TcsUsuarioAutenticacaoList = 
	                        (from entity1 in entity0.TCS_USUARIO_AUTENTICACAO_LISTA
                                  let entity1Al1 = entity1.TCS_EMPRESA_AUTENTICACAO
                                orderby entity1.NOME_AUTENTICACAO ascending
	                        
	                        	
	                        select new TcsUsuarioAutenticacao()
	                        {
	                        
                                IdLinx = entity1Al1.ID_LINX
                                , IdUsuario = entity1.ID_USUARIO
                                , NomeAutenticacao = entity1.NOME_AUTENTICACAO
                                , NomeUsuario = entity1.NOME_USUARIO
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsEmpresaGpecon.
	    public IQueryable<TcsEmpresaGpecon> GetTcsEmpresaGpecon()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsEmpresaGpecon> result = 
	            (from entity0 in this.DbContext.TCS_EMPRESA_GPECON
                  let entity0Al2 = entity0.GPECON
                  let entity0Al1 = entity0.EMPRESA
	            
	            	
	            select new TcsEmpresaGpecon()		
	            {
	            
                GrupoEconomico = entity0Al1.NOME_EMPRESA
                , IdGrupoEconomico = entity0.ID_LINX_GPECON
                , IdLinx = entity0Al2.ID_LINX
                , IdLinxGpecon = entity0Al1.ID_LINX
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsAmbiente.
	    public IQueryable<TcsAmbiente> GetTcsAmbiente()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsAmbiente> result = 
	            (from entity0 in this.DbContext.TCS_AMBIENTE
                  let entity0Al1 = entity0.TCS_APLICACAO
                  let entity0Al2 = entity0.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsAmbiente()		
	            {
	            
                DescricaoAmbiente = entity0.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al1.DESCRICAO_APLICACAO
                , EmDesenvolvimento = entity0Al1.EM_DESENVOLVIMENTO
                , IdLinx = entity0Al2.ID_LINX
                , IdTcsAmbiente = entity0.ID_TCS_AMBIENTE
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsEmpresaModulo.
	    public IQueryable<TcsEmpresaModulo> GetTcsEmpresaModulo()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsEmpresaModulo> result = 
	            (from entity0 in this.DbContext.TCS_EMPRESA_MODULO
                  let entity0Al1 = entity0.TCS_MODULO_AUTORIZACAO
                  let entity0Al3 = entity0.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO
	            
	            	
	            select new TcsEmpresaModulo()		
	            {
	            
                DescModulo = entity0Al1.DESC_MODULO
                , DescricaoAplicativo = entity0Al2.DESCRICAO_APLICATIVO
                , IdLinx = entity0Al3.ID_LINX
                , IdModulo = entity0Al1.ID_MODULO
                , IdTcsAplicativo = entity0Al2.ID_TCS_APLICATIVO
                , IdTcsEmpresaModulo = entity0.ID_TCS_EMPRESA_MODULO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsUsuarioAutenticacao.
	    public IQueryable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacao()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioAutenticacao> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_AUTENTICACAO
                  let entity0Al1 = entity0.TCS_EMPRESA_AUTENTICACAO
                orderby entity0.NOME_AUTENTICACAO ascending
	            
	            	
	            select new TcsUsuarioAutenticacao()		
	            {
	            
                IdLinx = entity0Al1.ID_LINX
                , IdUsuario = entity0.ID_USUARIO
                , NomeAutenticacao = entity0.NOME_AUTENTICACAO
                , NomeUsuario = entity0.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsEmpresaAutenticacaoNoAssociations.
	    public IQueryable<TcsEmpresaAutenticacao> GetTcsEmpresaAutenticacaoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsEmpresaAutenticacao> result = 
	            (from entity0 in this.DbContext.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsEmpresaAutenticacao()		
	            {
	            
                CnpjCpf = entity0.CNPJ_CPF
                , IdLinx = entity0.ID_LINX
                , NomeEmpresa = entity0.NOME_EMPRESA
                , UidEmpresa = entity0.UID_EMPRESA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsEmpresaGpeconNoAssociations.
	    public IQueryable<TcsEmpresaGpecon> GetTcsEmpresaGpeconNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsEmpresaGpecon> result = 
	            (from entity0 in this.DbContext.TCS_EMPRESA_GPECON
                  let entity0Al2 = entity0.GPECON
                  let entity0Al1 = entity0.EMPRESA
	            
	            	
	            select new TcsEmpresaGpecon()		
	            {
	            
                GrupoEconomico = entity0Al1.NOME_EMPRESA
                , IdGrupoEconomico = entity0.ID_LINX_GPECON
                , IdLinx = entity0Al2.ID_LINX
                , IdLinxGpecon = entity0Al1.ID_LINX
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAmbienteNoAssociations.
	    public IQueryable<TcsAmbiente> GetTcsAmbienteNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsAmbiente> result = 
	            (from entity0 in this.DbContext.TCS_AMBIENTE
                  let entity0Al1 = entity0.TCS_APLICACAO
                  let entity0Al2 = entity0.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsAmbiente()		
	            {
	            
                DescricaoAmbiente = entity0.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al1.DESCRICAO_APLICACAO
                , EmDesenvolvimento = entity0Al1.EM_DESENVOLVIMENTO
                , IdLinx = entity0Al2.ID_LINX
                , IdTcsAmbiente = entity0.ID_TCS_AMBIENTE
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsEmpresaModuloNoAssociations.
	    public IQueryable<TcsEmpresaModulo> GetTcsEmpresaModuloNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsEmpresaModulo> result = 
	            (from entity0 in this.DbContext.TCS_EMPRESA_MODULO
                  let entity0Al1 = entity0.TCS_MODULO_AUTORIZACAO
                  let entity0Al3 = entity0.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO
	            
	            	
	            select new TcsEmpresaModulo()		
	            {
	            
                DescModulo = entity0Al1.DESC_MODULO
                , DescricaoAplicativo = entity0Al2.DESCRICAO_APLICATIVO
                , IdLinx = entity0Al3.ID_LINX
                , IdModulo = entity0Al1.ID_MODULO
                , IdTcsAplicativo = entity0Al2.ID_TCS_APLICATIVO
                , IdTcsEmpresaModulo = entity0.ID_TCS_EMPRESA_MODULO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioAutenticacaoNoAssociations.
	    public IQueryable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioAutenticacao> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_AUTENTICACAO
                  let entity0Al1 = entity0.TCS_EMPRESA_AUTENTICACAO
                orderby entity0.NOME_AUTENTICACAO ascending
	            
	            	
	            select new TcsUsuarioAutenticacao()		
	            {
	            
                IdLinx = entity0Al1.ID_LINX
                , IdUsuario = entity0.ID_USUARIO
                , NomeAutenticacao = entity0.NOME_AUTENTICACAO
                , NomeUsuario = entity0.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsEmpresaGpeconP.
	    public IQueryable<TcsEmpresaGpeconP> GetTcsEmpresaGpeconP()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsEmpresaGpeconP> result = 
	            (from entity0 in this.DbContext.TCS_EMPRESA_GPECON
	            
	            	
	            select new TcsEmpresaGpeconP()		
	            {
	            
                IdLinx = entity0.ID_LINX
                , IdLinxGpecon = entity0.ID_LINX_GPECON
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsEmpresaGpeconPNoAssociations.
	    public IQueryable<TcsEmpresaGpeconP> GetTcsEmpresaGpeconPNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsEmpresaGpeconP> result = 
	            (from entity0 in this.DbContext.TCS_EMPRESA_GPECON
	            
	            	
	            select new TcsEmpresaGpeconP()		
	            {
	            
                IdLinx = entity0.ID_LINX
                , IdLinxGpecon = entity0.ID_LINX_GPECON
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsEmpresaAutenticacaoP.
	    public IQueryable<TcsEmpresaAutenticacaoP> GetTcsEmpresaAutenticacaoP()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsEmpresaAutenticacaoP> result = 
	            (from entity0 in this.DbContext.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsEmpresaAutenticacaoP()		
	            {
	            
                IdLinx = entity0.ID_LINX
                , NomeEmpresa = entity0.NOME_EMPRESA
                , UidEmpresa = entity0.UID_EMPRESA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsEmpresaAutenticacaoPNoAssociations.
	    public IQueryable<TcsEmpresaAutenticacaoP> GetTcsEmpresaAutenticacaoPNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsEmpresaAutenticacaoP> result = 
	            (from entity0 in this.DbContext.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsEmpresaAutenticacaoP()		
	            {
	            
                IdLinx = entity0.ID_LINX
                , NomeEmpresa = entity0.NOME_EMPRESA
                , UidEmpresa = entity0.UID_EMPRESA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	//Add filtering disabled property for TCS_EMPRESA_AUTENTICACAO
	    	string[] bmDisabledTcsEmpresaAutenticacaoList = this.GetEDM().GetFilteringDisabledList("TCS_EMPRESA_AUTENTICACAO");
	    	if (bmDisabledTcsEmpresaAutenticacaoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsEmpresaAutenticacaoList.Contains("TCS_EMPRESA_AUTENTICACAO.CNPJ_CPF"))
	    		{
	    			result.Add("TcsEmpresaAutenticacao|CnpjCpf");
	    			result.Add("TcsEmpresaAutenticacao|TCS_EMPRESA_AUTENTICACAO.CNPJ_CPF");
	    		}
	
	    		if (bmDisabledTcsEmpresaAutenticacaoList.Contains("TCS_EMPRESA_AUTENTICACAO.ID_LINX"))
	    		{
	    			result.Add("TcsEmpresaAutenticacao|IdLinx");
	    			result.Add("TcsEmpresaAutenticacao|TCS_EMPRESA_AUTENTICACAO.ID_LINX");
	    		}
	
	    		if (bmDisabledTcsEmpresaAutenticacaoList.Contains("TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA"))
	    		{
	    			result.Add("TcsEmpresaAutenticacao|NomeEmpresa");
	    			result.Add("TcsEmpresaAutenticacao|TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA");
	    		}
	
	    		if (bmDisabledTcsEmpresaAutenticacaoList.Contains("TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA"))
	    		{
	    			result.Add("TcsEmpresaAutenticacao|UidEmpresa");
	    			result.Add("TcsEmpresaAutenticacao|TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_EMPRESA_GPECON
	    	string[] bmDisabledTcsEmpresaGpeconList = this.GetEDM().GetFilteringDisabledList("TCS_EMPRESA_GPECON");
	    	if (bmDisabledTcsEmpresaGpeconList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsEmpresaGpeconList.Contains("TCS_EMPRESA_GPECON.ID_LINX_GPECON"))
	    		{
	    			result.Add("TcsEmpresaGpecon|IdGrupoEconomico");
	    			result.Add("TcsEmpresaGpecon|TCS_EMPRESA_GPECON.ID_LINX_GPECON");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_AMBIENTE
	    	string[] bmDisabledTcsAmbienteList = this.GetEDM().GetFilteringDisabledList("TCS_AMBIENTE");
	    	if (bmDisabledTcsAmbienteList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsAmbienteList.Contains("TCS_AMBIENTE.DESCRICAO_AMBIENTE"))
	    		{
	    			result.Add("TcsAmbiente|DescricaoAmbiente");
	    			result.Add("TcsAmbiente|TCS_AMBIENTE.DESCRICAO_AMBIENTE");
	    		}
	
	    		if (bmDisabledTcsAmbienteList.Contains("TCS_AMBIENTE.ID_TCS_AMBIENTE"))
	    		{
	    			result.Add("TcsAmbiente|IdTcsAmbiente");
	    			result.Add("TcsAmbiente|TCS_AMBIENTE.ID_TCS_AMBIENTE");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_EMPRESA_MODULO
	    	string[] bmDisabledTcsEmpresaModuloList = this.GetEDM().GetFilteringDisabledList("TCS_EMPRESA_MODULO");
	    	if (bmDisabledTcsEmpresaModuloList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsEmpresaModuloList.Contains("TCS_EMPRESA_MODULO.ID_TCS_EMPRESA_MODULO"))
	    		{
	    			result.Add("TcsEmpresaModulo|IdTcsEmpresaModulo");
	    			result.Add("TcsEmpresaModulo|TCS_EMPRESA_MODULO.ID_TCS_EMPRESA_MODULO");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_USUARIO_AUTENTICACAO
	    	string[] bmDisabledTcsUsuarioAutenticacaoList = this.GetEDM().GetFilteringDisabledList("TCS_USUARIO_AUTENTICACAO");
	    	if (bmDisabledTcsUsuarioAutenticacaoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsUsuarioAutenticacaoList.Contains("TCS_USUARIO_AUTENTICACAO.ID_USUARIO"))
	    		{
	    			result.Add("TcsUsuarioAutenticacao|IdUsuario");
	    			result.Add("TcsUsuarioAutenticacao|TCS_USUARIO_AUTENTICACAO.ID_USUARIO");
	    		}
	
	    		if (bmDisabledTcsUsuarioAutenticacaoList.Contains("TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO"))
	    		{
	    			result.Add("TcsUsuarioAutenticacao|NomeAutenticacao");
	    			result.Add("TcsUsuarioAutenticacao|TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO");
	    		}
	
	    		if (bmDisabledTcsUsuarioAutenticacaoList.Contains("TCS_USUARIO_AUTENTICACAO.NOME_USUARIO"))
	    		{
	    			result.Add("TcsUsuarioAutenticacao|NomeUsuario");
	    			result.Add("TcsUsuarioAutenticacao|TCS_USUARIO_AUTENTICACAO.NOME_USUARIO");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_EMPRESA_GPECON
	    	string[] bmDisabledTcsEmpresaGpeconPList = this.GetEDM().GetFilteringDisabledList("TCS_EMPRESA_GPECON");
	    	if (bmDisabledTcsEmpresaGpeconPList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsEmpresaGpeconPList.Contains("TCS_EMPRESA_GPECON.ID_LINX"))
	    		{
	    			result.Add("TcsEmpresaGpeconP|IdLinx");
	    			result.Add("TcsEmpresaGpeconP|TCS_EMPRESA_GPECON.ID_LINX");
	    		}
	
	    		if (bmDisabledTcsEmpresaGpeconPList.Contains("TCS_EMPRESA_GPECON.ID_LINX_GPECON"))
	    		{
	    			result.Add("TcsEmpresaGpeconP|IdLinxGpecon");
	    			result.Add("TcsEmpresaGpeconP|TCS_EMPRESA_GPECON.ID_LINX_GPECON");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_EMPRESA_AUTENTICACAO
	    	string[] bmDisabledTcsEmpresaAutenticacaoPList = this.GetEDM().GetFilteringDisabledList("TCS_EMPRESA_AUTENTICACAO");
	    	if (bmDisabledTcsEmpresaAutenticacaoPList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsEmpresaAutenticacaoPList.Contains("TCS_EMPRESA_AUTENTICACAO.ID_LINX"))
	    		{
	    			result.Add("TcsEmpresaAutenticacaoP|IdLinx");
	    			result.Add("TcsEmpresaAutenticacaoP|TCS_EMPRESA_AUTENTICACAO.ID_LINX");
	    		}
	
	    		if (bmDisabledTcsEmpresaAutenticacaoPList.Contains("TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA"))
	    		{
	    			result.Add("TcsEmpresaAutenticacaoP|NomeEmpresa");
	    			result.Add("TcsEmpresaAutenticacaoP|TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA");
	    		}
	
	    		if (bmDisabledTcsEmpresaAutenticacaoPList.Contains("TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA"))
	    		{
	    			result.Add("TcsEmpresaAutenticacaoP|UidEmpresa");
	    			result.Add("TcsEmpresaAutenticacaoP|TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA");
	    		}
	    	}
	    	return result;
	    }

	    #endregion Excluded Filters

	    #region Get By EntitySearchId Methods Definitions.
	
				
	    [Ignore]
	    //Get TcsEmpresaAutenticacao By EntitySearchId.
	    public IQueryable<TcsEmpresaAutenticacao> GetTcsEmpresaAutenticacaoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsEmpresaAutenticacaoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsEmpresaGpecon By EntitySearchId.
	    public IQueryable<TcsEmpresaGpecon> GetTcsEmpresaGpeconByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsEmpresaGpeconByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsAmbiente By EntitySearchId.
	    public IQueryable<TcsAmbiente> GetTcsAmbienteByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsAmbienteByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsEmpresaModulo By EntitySearchId.
	    public IQueryable<TcsEmpresaModulo> GetTcsEmpresaModuloByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsEmpresaModuloByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioAutenticacao By EntitySearchId.
	    public IQueryable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioAutenticacaoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsEmpresaAutenticacao By EntitySearchId.
	    public IQueryable<TcsEmpresaAutenticacao> GetTcsEmpresaAutenticacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsEmpresaAutenticacaoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsEmpresaGpecon By EntitySearchId.
	    public IQueryable<TcsEmpresaGpecon> GetTcsEmpresaGpeconByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsEmpresaGpeconByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsAmbiente By EntitySearchId.
	    public IQueryable<TcsAmbiente> GetTcsAmbienteByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsAmbienteByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsEmpresaModulo By EntitySearchId.
	    public IQueryable<TcsEmpresaModulo> GetTcsEmpresaModuloByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsEmpresaModuloByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioAutenticacao By EntitySearchId.
	    public IQueryable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsEmpresaGpeconP By EntitySearchId.
	    public IQueryable<TcsEmpresaGpeconP> GetTcsEmpresaGpeconPByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsEmpresaGpeconPByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsEmpresaGpeconP By EntitySearchId.
	    public IQueryable<TcsEmpresaGpeconP> GetTcsEmpresaGpeconPByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsEmpresaGpeconPByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsEmpresaAutenticacaoP By EntitySearchId.
	    public IQueryable<TcsEmpresaAutenticacaoP> GetTcsEmpresaAutenticacaoPByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsEmpresaAutenticacaoPByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsEmpresaAutenticacaoP By EntitySearchId.
	    public IQueryable<TcsEmpresaAutenticacaoP> GetTcsEmpresaAutenticacaoPByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsEmpresaAutenticacaoPByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get TcsEmpresaAutenticacao By Example.
	    [Ignore]
	    public IQueryable<TcsEmpresaAutenticacao> GetTcsEmpresaAutenticacaoByExample(TcsEmpresaAutenticacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsEmpresaAutenticacaoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsEmpresaGpecon By Example.
	    [Ignore]
	    public IQueryable<TcsEmpresaGpecon> GetTcsEmpresaGpeconByExample(TcsEmpresaGpecon entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsEmpresaGpeconByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsAmbiente By Example.
	    [Ignore]
	    public IQueryable<TcsAmbiente> GetTcsAmbienteByExample(TcsAmbiente entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsAmbienteByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsEmpresaModulo By Example.
	    [Ignore]
	    public IQueryable<TcsEmpresaModulo> GetTcsEmpresaModuloByExample(TcsEmpresaModulo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsEmpresaModuloByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsUsuarioAutenticacao By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoByExample(TcsUsuarioAutenticacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioAutenticacaoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsEmpresaAutenticacao By Example.
	    [Ignore]
	    public IQueryable<TcsEmpresaAutenticacao> GetTcsEmpresaAutenticacaoByExampleNoAssociations(TcsEmpresaAutenticacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsEmpresaAutenticacaoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsEmpresaGpecon By Example.
	    [Ignore]
	    public IQueryable<TcsEmpresaGpecon> GetTcsEmpresaGpeconByExampleNoAssociations(TcsEmpresaGpecon entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsEmpresaGpeconByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsAmbiente By Example.
	    [Ignore]
	    public IQueryable<TcsAmbiente> GetTcsAmbienteByExampleNoAssociations(TcsAmbiente entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsAmbienteByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsEmpresaModulo By Example.
	    [Ignore]
	    public IQueryable<TcsEmpresaModulo> GetTcsEmpresaModuloByExampleNoAssociations(TcsEmpresaModulo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsEmpresaModuloByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsUsuarioAutenticacao By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoByExampleNoAssociations(TcsUsuarioAutenticacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsEmpresaGpeconP By Example.
	    [Ignore]
	    public IQueryable<TcsEmpresaGpeconP> GetTcsEmpresaGpeconPByExample(TcsEmpresaGpeconP entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsEmpresaGpeconPByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsEmpresaGpeconP By Example.
	    [Ignore]
	    public IQueryable<TcsEmpresaGpeconP> GetTcsEmpresaGpeconPByExampleNoAssociations(TcsEmpresaGpeconP entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsEmpresaGpeconPByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsEmpresaAutenticacaoP By Example.
	    [Ignore]
	    public IQueryable<TcsEmpresaAutenticacaoP> GetTcsEmpresaAutenticacaoPByExample(TcsEmpresaAutenticacaoP entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsEmpresaAutenticacaoPByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsEmpresaAutenticacaoP By Example.
	    [Ignore]
	    public IQueryable<TcsEmpresaAutenticacaoP> GetTcsEmpresaAutenticacaoPByExampleNoAssociations(TcsEmpresaAutenticacaoP entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsEmpresaAutenticacaoPByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public TcsEmpresaAutenticacao GetTcsEmpresaAutenticacaoByKey(int idLinx)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsEmpresaAutenticacao");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdLinx"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idLinx));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsEmpresaAutenticacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsEmpresaGpecon GetTcsEmpresaGpeconByKey(Int32 idGrupoEconomico, Int32 idLinx, Int32 idLinxGpecon)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsEmpresaGpecon");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdGrupoEconomico"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idGrupoEconomico));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdLinx"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idLinx));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdLinxGpecon"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idLinxGpecon));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsEmpresaGpeconByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsAmbiente GetTcsAmbienteByKey(Int32 idTcsAmbiente)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsAmbiente");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsAmbiente"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsAmbiente));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsAmbienteByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsEmpresaModulo GetTcsEmpresaModuloByKey(Int32 idTcsEmpresaModulo)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsEmpresaModulo");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsEmpresaModulo"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsEmpresaModulo));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsEmpresaModuloByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsUsuarioAutenticacao GetTcsUsuarioAutenticacaoByKey(Int64 idUsuario)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsUsuarioAutenticacao");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuario"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idUsuario));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsEmpresaGpeconP GetTcsEmpresaGpeconPByKey(Int32 idLinx, Int32 idLinxGpecon)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsEmpresaGpeconP");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdLinx"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idLinx));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdLinxGpecon"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idLinxGpecon));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsEmpresaGpeconPByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsEmpresaAutenticacaoP GetTcsEmpresaAutenticacaoPByKey(Int32 idLinx)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsEmpresaAutenticacaoP");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdLinx"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idLinx));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsEmpresaAutenticacaoPByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get TcsEmpresaAutenticacaoByEntitySearch.
	    public IQueryable<TcsEmpresaAutenticacao> GetTcsEmpresaAutenticacaoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsEmpresaAutenticacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsEmpresaAutenticacao> result = 
	            (from entity0 in this.DbContext.TCS_EMPRESA_AUTENTICACAO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsEmpresaAutenticacao()		
	            {
	            
                CnpjCpf = entity0.CNPJ_CPF
                , IdLinx = entity0.ID_LINX
                , NomeEmpresa = entity0.NOME_EMPRESA
                , UidEmpresa = entity0.UID_EMPRESA
			
                ,TcsEmpresaGpeconList = 
	                        (from entity1 in entity0.EMPRESA_LISTA
                                  let entity1Al2 = entity1.GPECON
                                  let entity1Al1 = entity1.EMPRESA
	                        
	                        	
	                        select new TcsEmpresaGpecon()
	                        {
	                        
                                GrupoEconomico = entity1Al1.NOME_EMPRESA
                                , IdGrupoEconomico = entity1.ID_LINX_GPECON
                                , IdLinx = entity1Al2.ID_LINX
                                , IdLinxGpecon = entity1Al1.ID_LINX
		
	                        }
	                        )
			
                ,TcsAmbienteList = 
	                        (from entity1 in entity0.TCS_AMBIENTE_LISTA
                                  let entity1Al1 = entity1.TCS_APLICACAO
                                  let entity1Al2 = entity1.TCS_EMPRESA_AUTENTICACAO
	                        
	                        	
	                        select new TcsAmbiente()
	                        {
	                        
                                DescricaoAmbiente = entity1.DESCRICAO_AMBIENTE
                                , DescricaoAplicacao = entity1Al1.DESCRICAO_APLICACAO
                                , EmDesenvolvimento = entity1Al1.EM_DESENVOLVIMENTO
                                , IdLinx = entity1Al2.ID_LINX
                                , IdTcsAmbiente = entity1.ID_TCS_AMBIENTE
		
	                        }
	                        )
			
                ,TcsEmpresaModuloList = 
	                        (from entity1 in entity0.TCS_EMPRESA_MODULO_LISTA
                                  let entity1Al1 = entity1.TCS_MODULO_AUTORIZACAO
                                  let entity1Al3 = entity1.TCS_EMPRESA_AUTENTICACAO
                                  let entity1Al2 = entity1.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO
	                        
	                        	
	                        select new TcsEmpresaModulo()
	                        {
	                        
                                DescModulo = entity1Al1.DESC_MODULO
                                , DescricaoAplicativo = entity1Al2.DESCRICAO_APLICATIVO
                                , IdLinx = entity1Al3.ID_LINX
                                , IdModulo = entity1Al1.ID_MODULO
                                , IdTcsAplicativo = entity1Al2.ID_TCS_APLICATIVO
                                , IdTcsEmpresaModulo = entity1.ID_TCS_EMPRESA_MODULO
		
	                        }
	                        )
			
                ,TcsUsuarioAutenticacaoList = 
	                        (from entity1 in entity0.TCS_USUARIO_AUTENTICACAO_LISTA
                                  let entity1Al1 = entity1.TCS_EMPRESA_AUTENTICACAO
                                orderby entity1.NOME_AUTENTICACAO ascending
	                        
	                        	
	                        select new TcsUsuarioAutenticacao()
	                        {
	                        
                                IdLinx = entity1Al1.ID_LINX
                                , IdUsuario = entity1.ID_USUARIO
                                , NomeAutenticacao = entity1.NOME_AUTENTICACAO
                                , NomeUsuario = entity1.NOME_USUARIO
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsEmpresaGpeconByEntitySearch.
	    public IQueryable<TcsEmpresaGpecon> GetTcsEmpresaGpeconByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsEmpresaGpecon));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsEmpresaGpecon> result = 
	            (from entity0 in this.DbContext.TCS_EMPRESA_GPECON.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.GPECON
                  let entity0Al1 = entity0.EMPRESA
	            
	            	
	            select new TcsEmpresaGpecon()		
	            {
	            
                GrupoEconomico = entity0Al1.NOME_EMPRESA
                , IdGrupoEconomico = entity0.ID_LINX_GPECON
                , IdLinx = entity0Al2.ID_LINX
                , IdLinxGpecon = entity0Al1.ID_LINX
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAmbienteByEntitySearch.
	    public IQueryable<TcsAmbiente> GetTcsAmbienteByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAmbiente));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAmbiente> result = 
	            (from entity0 in this.DbContext.TCS_AMBIENTE.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_APLICACAO
                  let entity0Al2 = entity0.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsAmbiente()		
	            {
	            
                DescricaoAmbiente = entity0.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al1.DESCRICAO_APLICACAO
                , EmDesenvolvimento = entity0Al1.EM_DESENVOLVIMENTO
                , IdLinx = entity0Al2.ID_LINX
                , IdTcsAmbiente = entity0.ID_TCS_AMBIENTE
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsEmpresaModuloByEntitySearch.
	    public IQueryable<TcsEmpresaModulo> GetTcsEmpresaModuloByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsEmpresaModulo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsEmpresaModulo> result = 
	            (from entity0 in this.DbContext.TCS_EMPRESA_MODULO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_MODULO_AUTORIZACAO
                  let entity0Al3 = entity0.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO
	            
	            	
	            select new TcsEmpresaModulo()		
	            {
	            
                DescModulo = entity0Al1.DESC_MODULO
                , DescricaoAplicativo = entity0Al2.DESCRICAO_APLICATIVO
                , IdLinx = entity0Al3.ID_LINX
                , IdModulo = entity0Al1.ID_MODULO
                , IdTcsAplicativo = entity0Al2.ID_TCS_APLICATIVO
                , IdTcsEmpresaModulo = entity0.ID_TCS_EMPRESA_MODULO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioAutenticacaoByEntitySearch.
	    public IQueryable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioAutenticacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioAutenticacao> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_AUTENTICACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_EMPRESA_AUTENTICACAO
                orderby entity0.NOME_AUTENTICACAO ascending
	            
	            	
	            select new TcsUsuarioAutenticacao()		
	            {
	            
                IdLinx = entity0Al1.ID_LINX
                , IdUsuario = entity0.ID_USUARIO
                , NomeAutenticacao = entity0.NOME_AUTENTICACAO
                , NomeUsuario = entity0.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsEmpresaAutenticacaoByEntitySearchNoAssociations.
	    public IQueryable<TcsEmpresaAutenticacao> GetTcsEmpresaAutenticacaoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsEmpresaAutenticacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsEmpresaAutenticacao> result = 
	            (from entity0 in this.DbContext.TCS_EMPRESA_AUTENTICACAO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsEmpresaAutenticacao()		
	            {
	            
                CnpjCpf = entity0.CNPJ_CPF
                , IdLinx = entity0.ID_LINX
                , NomeEmpresa = entity0.NOME_EMPRESA
                , UidEmpresa = entity0.UID_EMPRESA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsEmpresaGpeconByEntitySearchNoAssociations.
	    public IQueryable<TcsEmpresaGpecon> GetTcsEmpresaGpeconByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsEmpresaGpecon));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsEmpresaGpecon> result = 
	            (from entity0 in this.DbContext.TCS_EMPRESA_GPECON.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.GPECON
                  let entity0Al1 = entity0.EMPRESA
	            
	            	
	            select new TcsEmpresaGpecon()		
	            {
	            
                GrupoEconomico = entity0Al1.NOME_EMPRESA
                , IdGrupoEconomico = entity0.ID_LINX_GPECON
                , IdLinx = entity0Al2.ID_LINX
                , IdLinxGpecon = entity0Al1.ID_LINX
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAmbienteByEntitySearchNoAssociations.
	    public IQueryable<TcsAmbiente> GetTcsAmbienteByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAmbiente));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAmbiente> result = 
	            (from entity0 in this.DbContext.TCS_AMBIENTE.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_APLICACAO
                  let entity0Al2 = entity0.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsAmbiente()		
	            {
	            
                DescricaoAmbiente = entity0.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al1.DESCRICAO_APLICACAO
                , EmDesenvolvimento = entity0Al1.EM_DESENVOLVIMENTO
                , IdLinx = entity0Al2.ID_LINX
                , IdTcsAmbiente = entity0.ID_TCS_AMBIENTE
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsEmpresaModuloByEntitySearchNoAssociations.
	    public IQueryable<TcsEmpresaModulo> GetTcsEmpresaModuloByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsEmpresaModulo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsEmpresaModulo> result = 
	            (from entity0 in this.DbContext.TCS_EMPRESA_MODULO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_MODULO_AUTORIZACAO
                  let entity0Al3 = entity0.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO
	            
	            	
	            select new TcsEmpresaModulo()		
	            {
	            
                DescModulo = entity0Al1.DESC_MODULO
                , DescricaoAplicativo = entity0Al2.DESCRICAO_APLICATIVO
                , IdLinx = entity0Al3.ID_LINX
                , IdModulo = entity0Al1.ID_MODULO
                , IdTcsAplicativo = entity0Al2.ID_TCS_APLICATIVO
                , IdTcsEmpresaModulo = entity0.ID_TCS_EMPRESA_MODULO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioAutenticacaoByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioAutenticacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioAutenticacao> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_AUTENTICACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_EMPRESA_AUTENTICACAO
                orderby entity0.NOME_AUTENTICACAO ascending
	            
	            	
	            select new TcsUsuarioAutenticacao()		
	            {
	            
                IdLinx = entity0Al1.ID_LINX
                , IdUsuario = entity0.ID_USUARIO
                , NomeAutenticacao = entity0.NOME_AUTENTICACAO
                , NomeUsuario = entity0.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsEmpresaGpeconParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsEmpresaGpeconParentComposition> GetTcsEmpresaGpeconParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "TCS_EMPRESA_AUTENTICACAO", "TCS_EMPRESA_GPECON", "GPECON", typeof(TcsEmpresaGpeconParentComposition), typeof(TcsAmbiente), typeof(TcsEmpresaModulo), typeof(TcsUsuarioAutenticacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsEmpresaGpeconParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_EMPRESA_GPECON.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.GPECON
                  let entity0Al1 = entity0.EMPRESA
	            
	            	
	            select new TcsEmpresaGpeconParentComposition()		
	            {
	            
                GrupoEconomico = entity0Al1.NOME_EMPRESA
                , IdGrupoEconomico = entity0.ID_LINX_GPECON
                , IdLinx = entity0Al2.ID_LINX
                , IdLinxGpecon = entity0Al1.ID_LINX
                //TcsEmpresaAutenticacao Properties.
                , CnpjCpf = entity0.GPECON.CNPJ_CPF
                , NomeEmpresa = entity0.GPECON.NOME_EMPRESA
                , UidEmpresa = entity0.GPECON.UID_EMPRESA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAmbienteParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsAmbienteParentComposition> GetTcsAmbienteParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "TCS_EMPRESA_AUTENTICACAO", "TCS_AMBIENTE", "TCS_EMPRESA_AUTENTICACAO", typeof(TcsAmbienteParentComposition), typeof(TcsEmpresaGpecon), typeof(TcsEmpresaModulo), typeof(TcsUsuarioAutenticacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAmbienteParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_AMBIENTE.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_APLICACAO
                  let entity0Al2 = entity0.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsAmbienteParentComposition()		
	            {
	            
                DescricaoAmbiente = entity0.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al1.DESCRICAO_APLICACAO
                , EmDesenvolvimento = entity0Al1.EM_DESENVOLVIMENTO
                , IdLinx = entity0Al2.ID_LINX
                , IdTcsAmbiente = entity0.ID_TCS_AMBIENTE
                //TcsEmpresaAutenticacao Properties.
                , CnpjCpf = entity0.TCS_EMPRESA_AUTENTICACAO.CNPJ_CPF
                , NomeEmpresa = entity0.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA
                , UidEmpresa = entity0.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsEmpresaModuloParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsEmpresaModuloParentComposition> GetTcsEmpresaModuloParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "TCS_EMPRESA_AUTENTICACAO", "TCS_EMPRESA_MODULO", "TCS_EMPRESA_AUTENTICACAO", typeof(TcsEmpresaModuloParentComposition), typeof(TcsEmpresaGpecon), typeof(TcsAmbiente), typeof(TcsUsuarioAutenticacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsEmpresaModuloParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_EMPRESA_MODULO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_MODULO_AUTORIZACAO
                  let entity0Al3 = entity0.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO
	            
	            	
	            select new TcsEmpresaModuloParentComposition()		
	            {
	            
                DescModulo = entity0Al1.DESC_MODULO
                , DescricaoAplicativo = entity0Al2.DESCRICAO_APLICATIVO
                , IdLinx = entity0Al3.ID_LINX
                , IdModulo = entity0Al1.ID_MODULO
                , IdTcsAplicativo = entity0Al2.ID_TCS_APLICATIVO
                , IdTcsEmpresaModulo = entity0.ID_TCS_EMPRESA_MODULO
                //TcsEmpresaAutenticacao Properties.
                , CnpjCpf = entity0.TCS_EMPRESA_AUTENTICACAO.CNPJ_CPF
                , NomeEmpresa = entity0.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA
                , UidEmpresa = entity0.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioAutenticacaoParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioAutenticacaoParentComposition> GetTcsUsuarioAutenticacaoParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "TCS_EMPRESA_AUTENTICACAO", "TCS_USUARIO_AUTENTICACAO", "TCS_EMPRESA_AUTENTICACAO", typeof(TcsUsuarioAutenticacaoParentComposition), typeof(TcsEmpresaGpecon), typeof(TcsAmbiente), typeof(TcsEmpresaModulo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioAutenticacaoParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_AUTENTICACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_EMPRESA_AUTENTICACAO
                orderby entity0.NOME_AUTENTICACAO ascending
	            
	            	
	            select new TcsUsuarioAutenticacaoParentComposition()		
	            {
	            
                IdLinx = entity0Al1.ID_LINX
                , IdUsuario = entity0.ID_USUARIO
                , NomeAutenticacao = entity0.NOME_AUTENTICACAO
                , NomeUsuario = entity0.NOME_USUARIO
                //TcsEmpresaAutenticacao Properties.
                , CnpjCpf = entity0.TCS_EMPRESA_AUTENTICACAO.CNPJ_CPF
                , NomeEmpresa = entity0.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA
                , UidEmpresa = entity0.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsEmpresaGpeconPByEntitySearch.
	    public IQueryable<TcsEmpresaGpeconP> GetTcsEmpresaGpeconPByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsEmpresaGpeconP));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsEmpresaGpeconP> result = 
	            (from entity0 in this.DbContext.TCS_EMPRESA_GPECON.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsEmpresaGpeconP()		
	            {
	            
                IdLinx = entity0.ID_LINX
                , IdLinxGpecon = entity0.ID_LINX_GPECON
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsEmpresaGpeconPByEntitySearchNoAssociations.
	    public IQueryable<TcsEmpresaGpeconP> GetTcsEmpresaGpeconPByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsEmpresaGpeconP));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsEmpresaGpeconP> result = 
	            (from entity0 in this.DbContext.TCS_EMPRESA_GPECON.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsEmpresaGpeconP()		
	            {
	            
                IdLinx = entity0.ID_LINX
                , IdLinxGpecon = entity0.ID_LINX_GPECON
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsEmpresaAutenticacaoPByEntitySearch.
	    public IQueryable<TcsEmpresaAutenticacaoP> GetTcsEmpresaAutenticacaoPByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsEmpresaAutenticacaoP));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsEmpresaAutenticacaoP> result = 
	            (from entity0 in this.DbContext.TCS_EMPRESA_AUTENTICACAO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsEmpresaAutenticacaoP()		
	            {
	            
                IdLinx = entity0.ID_LINX
                , NomeEmpresa = entity0.NOME_EMPRESA
                , UidEmpresa = entity0.UID_EMPRESA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsEmpresaAutenticacaoPByEntitySearchNoAssociations.
	    public IQueryable<TcsEmpresaAutenticacaoP> GetTcsEmpresaAutenticacaoPByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsEmpresaAutenticacaoP));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsEmpresaAutenticacaoP> result = 
	            (from entity0 in this.DbContext.TCS_EMPRESA_AUTENTICACAO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsEmpresaAutenticacaoP()		
	            {
	            
                IdLinx = entity0.ID_LINX
                , NomeEmpresa = entity0.NOME_EMPRESA
                , UidEmpresa = entity0.UID_EMPRESA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get PagedTcsEmpresaAutenticacao.
	    public IQueryable<TcsEmpresaAutenticacao> GetPagedTcsEmpresaAutenticacao(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsEmpresaAutenticacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsEmpresaAutenticacao> result = 
	            (from entity0 in this.DbContext.TCS_EMPRESA_AUTENTICACAO.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_LINX ascending
	            
	            	
	            select new TcsEmpresaAutenticacao()		
	            {
	            
                CnpjCpf = entity0.CNPJ_CPF
                , IdLinx = entity0.ID_LINX
                , NomeEmpresa = entity0.NOME_EMPRESA
                , UidEmpresa = entity0.UID_EMPRESA
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsEmpresaGpecon.
	    public IQueryable<TcsEmpresaGpecon> GetPagedTcsEmpresaGpecon(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsEmpresaGpecon));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsEmpresaGpecon> result = 
	            (from entity0 in this.DbContext.TCS_EMPRESA_GPECON.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.GPECON
                  let entity0Al1 = entity0.EMPRESA
                orderby entity0.ID_LINX_GPECON ascending, entity0Al2.ID_LINX ascending
	            
	            	
	            select new TcsEmpresaGpecon()		
	            {
	            
                GrupoEconomico = entity0Al1.NOME_EMPRESA
                , IdGrupoEconomico = entity0.ID_LINX_GPECON
                , IdLinx = entity0Al2.ID_LINX
                , IdLinxGpecon = entity0Al1.ID_LINX
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsAmbiente.
	    public IQueryable<TcsAmbiente> GetPagedTcsAmbiente(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAmbiente));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAmbiente> result = 
	            (from entity0 in this.DbContext.TCS_AMBIENTE.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_APLICACAO
                  let entity0Al2 = entity0.TCS_EMPRESA_AUTENTICACAO
                orderby entity0.ID_TCS_AMBIENTE ascending
	            
	            	
	            select new TcsAmbiente()		
	            {
	            
                DescricaoAmbiente = entity0.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al1.DESCRICAO_APLICACAO
                , EmDesenvolvimento = entity0Al1.EM_DESENVOLVIMENTO
                , IdLinx = entity0Al2.ID_LINX
                , IdTcsAmbiente = entity0.ID_TCS_AMBIENTE
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsEmpresaModulo.
	    public IQueryable<TcsEmpresaModulo> GetPagedTcsEmpresaModulo(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsEmpresaModulo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsEmpresaModulo> result = 
	            (from entity0 in this.DbContext.TCS_EMPRESA_MODULO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_MODULO_AUTORIZACAO
                  let entity0Al3 = entity0.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO
                orderby entity0.ID_TCS_EMPRESA_MODULO ascending
	            
	            	
	            select new TcsEmpresaModulo()		
	            {
	            
                DescModulo = entity0Al1.DESC_MODULO
                , DescricaoAplicativo = entity0Al2.DESCRICAO_APLICATIVO
                , IdLinx = entity0Al3.ID_LINX
                , IdModulo = entity0Al1.ID_MODULO
                , IdTcsAplicativo = entity0Al2.ID_TCS_APLICATIVO
                , IdTcsEmpresaModulo = entity0.ID_TCS_EMPRESA_MODULO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsUsuarioAutenticacao.
	    public IQueryable<TcsUsuarioAutenticacao> GetPagedTcsUsuarioAutenticacao(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioAutenticacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioAutenticacao> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_AUTENTICACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_EMPRESA_AUTENTICACAO
                orderby entity0.ID_USUARIO ascending
	            
	            	
	            select new TcsUsuarioAutenticacao()		
	            {
	            
                IdLinx = entity0Al1.ID_LINX
                , IdUsuario = entity0.ID_USUARIO
                , NomeAutenticacao = entity0.NOME_AUTENTICACAO
                , NomeUsuario = entity0.NOME_USUARIO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsEmpresaAutenticacaoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsEmpresaAutenticacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_EMPRESA_AUTENTICACAO.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsEmpresaGpeconCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsEmpresaGpecon));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_EMPRESA_GPECON.Where(dynQuery, parameters.ToArray())
                  let entityAl2 = entity.GPECON
                  let entityAl1 = entity.EMPRESA
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsAmbienteCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAmbiente));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_AMBIENTE.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_APLICACAO
                  let entityAl2 = entity.TCS_EMPRESA_AUTENTICACAO
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsEmpresaModuloCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsEmpresaModulo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_EMPRESA_MODULO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_MODULO_AUTORIZACAO
                  let entityAl3 = entity.TCS_EMPRESA_AUTENTICACAO
                  let entityAl2 = entity.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsUsuarioAutenticacaoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioAutenticacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_USUARIO_AUTENTICACAO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_EMPRESA_AUTENTICACAO
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsEmpresaGpeconP.
	    public IQueryable<TcsEmpresaGpeconP> GetPagedTcsEmpresaGpeconP(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsEmpresaGpeconP));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsEmpresaGpeconP> result = 
	            (from entity0 in this.DbContext.TCS_EMPRESA_GPECON.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_LINX ascending, entity0.ID_LINX_GPECON ascending
	            
	            	
	            select new TcsEmpresaGpeconP()		
	            {
	            
                IdLinx = entity0.ID_LINX
                , IdLinxGpecon = entity0.ID_LINX_GPECON
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsEmpresaGpeconPCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsEmpresaGpeconP));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_EMPRESA_GPECON.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsEmpresaAutenticacaoP.
	    public IQueryable<TcsEmpresaAutenticacaoP> GetPagedTcsEmpresaAutenticacaoP(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsEmpresaAutenticacaoP));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsEmpresaAutenticacaoP> result = 
	            (from entity0 in this.DbContext.TCS_EMPRESA_AUTENTICACAO.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_LINX ascending
	            
	            	
	            select new TcsEmpresaAutenticacaoP()		
	            {
	            
                IdLinx = entity0.ID_LINX
                , NomeEmpresa = entity0.NOME_EMPRESA
                , UidEmpresa = entity0.UID_EMPRESA
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsEmpresaAutenticacaoPCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsEmpresaAutenticacaoP));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_EMPRESA_AUTENTICACAO.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    
	    [Update()]	
	    //Update TcsEmpresaAutenticacao.
	    public void UpdateTcsEmpresaAutenticacao(TcsEmpresaAutenticacao entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsEmpresaAutenticacao.
	    public void InsertTcsEmpresaAutenticacao(TcsEmpresaAutenticacao entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsEmpresaAutenticacao.
	    public void DeleteTcsEmpresaAutenticacao(TcsEmpresaAutenticacao entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsEmpresaGpecon.
	    public void UpdateTcsEmpresaGpecon(TcsEmpresaGpecon entity)
	    {



	
	        if (entity.TcsEmpresaAutenticacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsEmpresaAutenticacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsEmpresaAutenticacao); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsEmpresaGpecon.
	    public void InsertTcsEmpresaGpecon(TcsEmpresaGpecon entity)
	    {



	
	        if (entity.TcsEmpresaAutenticacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsEmpresaAutenticacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsEmpresaAutenticacao);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsEmpresaGpecon.
	    public void DeleteTcsEmpresaGpecon(TcsEmpresaGpecon entity)
	    {



	
	        if (entity.TcsEmpresaAutenticacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsEmpresaAutenticacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsEmpresaAutenticacao);
	            

	
	        }

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsAmbiente.
	    public void UpdateTcsAmbiente(TcsAmbiente entity)
	    {



	
	        if (entity.TcsEmpresaAutenticacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsEmpresaAutenticacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsEmpresaAutenticacao); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsAmbiente.
	    public void InsertTcsAmbiente(TcsAmbiente entity)
	    {



	
	        if (entity.TcsEmpresaAutenticacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsEmpresaAutenticacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsEmpresaAutenticacao);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsAmbiente.
	    public void DeleteTcsAmbiente(TcsAmbiente entity)
	    {



	
	        if (entity.TcsEmpresaAutenticacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsEmpresaAutenticacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsEmpresaAutenticacao);
	            

	
	        }

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsEmpresaModulo.
	    public void UpdateTcsEmpresaModulo(TcsEmpresaModulo entity)
	    {



	
	        if (entity.TcsEmpresaAutenticacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsEmpresaAutenticacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsEmpresaAutenticacao); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsEmpresaModulo.
	    public void InsertTcsEmpresaModulo(TcsEmpresaModulo entity)
	    {



	
	        if (entity.TcsEmpresaAutenticacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsEmpresaAutenticacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsEmpresaAutenticacao);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsEmpresaModulo.
	    public void DeleteTcsEmpresaModulo(TcsEmpresaModulo entity)
	    {



	
	        if (entity.TcsEmpresaAutenticacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsEmpresaAutenticacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsEmpresaAutenticacao);
	            

	
	        }

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsUsuarioAutenticacao.
	    public void UpdateTcsUsuarioAutenticacao(TcsUsuarioAutenticacao entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert TcsUsuarioAutenticacao.
	    public void InsertTcsUsuarioAutenticacao(TcsUsuarioAutenticacao entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete TcsUsuarioAutenticacao.
	    public void DeleteTcsUsuarioAutenticacao(TcsUsuarioAutenticacao entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsEmpresaGpeconP.
	    public void UpdateTcsEmpresaGpeconP(TcsEmpresaGpeconP entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsEmpresaGpeconP.
	    public void InsertTcsEmpresaGpeconP(TcsEmpresaGpeconP entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsEmpresaGpeconP.
	    public void DeleteTcsEmpresaGpeconP(TcsEmpresaGpeconP entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsEmpresaAutenticacaoP.
	    public void UpdateTcsEmpresaAutenticacaoP(TcsEmpresaAutenticacaoP entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsEmpresaAutenticacaoP.
	    public void InsertTcsEmpresaAutenticacaoP(TcsEmpresaAutenticacaoP entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsEmpresaAutenticacaoP.
	    public void DeleteTcsEmpresaAutenticacaoP(TcsEmpresaAutenticacaoP entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}