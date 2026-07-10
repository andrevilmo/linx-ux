					
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

namespace Linx.Framework.BV.TransacaoAutorizacao
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_TRANSACAO_AUTORIZACAO.ID_TRANSACAO", IsUpdatable=true, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsTransacaoAutorizacao,TcsTransacaoAutorizacao.TcsTransacaoMenuAutorizacao,TcsTransacaoAutorizacao.TcsTransacaoDependenteAutorizacao];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[];EdmEntityName[TCS_TRANSACAO_AUTORIZACAO];EntityRelations[TCS_MODULO_AUTORIZACAO(TCS_MODULO_AUTORIZACAO)#TCS_APLICATIVO(TCS_APLICATIVO)#TCS_OBJETO_AUTORIZACAO(TCS_OBJETO_AUTORIZACAO)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsTransacaoAutorizacao")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.TransacaoAutorizacao.TcsTransacaoAutorizacao")]
	public partial class TcsTransacaoAutorizacao : Linx.Data.Entity
	{

	

	    public TcsTransacaoAutorizacao() : this(true) { }

	    public TcsTransacaoAutorizacao(bool setDefaults) 
	    {

	        if (setDefaults)
	        {
	        	        LxCorFundo = 7;
	        }	

	    }

			
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.TcsTransacaoMenuAutorizacaoList != null && this.TcsTransacaoMenuAutorizacaoList.Count() > 0)
	      {
	         foreach (var entity in this.TcsTransacaoMenuAutorizacaoList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      if (this.TcsTransacaoDependenteAutorizacaoList != null && this.TcsTransacaoDependenteAutorizacaoList.Count() > 0)
	      {
	         foreach (var entity in this.TcsTransacaoDependenteAutorizacaoList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.TcsTransacaoMenuAutorizacaoList != null)
	      {
	         foreach (var detail in this.TcsTransacaoMenuAutorizacaoList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsTransacaoMenuAutorizacaoList = null;
	      }
	      if (this.TcsTransacaoDependenteAutorizacaoList != null)
	      {
	         foreach (var detail in this.TcsTransacaoDependenteAutorizacaoList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsTransacaoDependenteAutorizacaoList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(TransacaoAutorizacaoDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("TcsTransacaoMenuAutorizacao"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsTransacaoMenuAutorizacao");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTransacao"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdTransacao));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsTransacaoMenuAutorizacao and all sub-details
	         if (this.TcsTransacaoMenuAutorizacaoList == null || this.TcsTransacaoMenuAutorizacaoList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsTransacaoMenuAutorizacaoList = context.GetPagedTcsTransacaoMenuAutorizacao(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsTransacaoMenuAutorizacaoList = (from r in context.GetTcsTransacaoMenuAutorizacaoByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	      if (viewNames == null || viewNames.Contains("TcsTransacaoDependenteAutorizacao"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsTransacaoDependenteAutorizacao");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTransacao"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdTransacao));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsTransacaoDependenteAutorizacao and all sub-details
	         if (this.TcsTransacaoDependenteAutorizacaoList == null || this.TcsTransacaoDependenteAutorizacaoList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsTransacaoDependenteAutorizacaoList = context.GetPagedTcsTransacaoDependenteAutorizacao(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsTransacaoDependenteAutorizacaoList = (from r in context.GetTcsTransacaoDependenteAutorizacaoByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _TcsTransacaoMenuAutorizacaoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsTransacaoMenuAutorizacao && ((TcsTransacaoMenuAutorizacao)e.Entity).TcsTransacaoAutorizacao == null && e.Associations == null && e.OriginalAssociations == null && ((TcsTransacaoMenuAutorizacao)e.Entity).IdTransacao == this.IdTransacao).ToList();
 	      if (_TcsTransacaoMenuAutorizacaoElements.Count > 0 && this.TcsTransacaoMenuAutorizacaoList.Count() == 0)
 	      {
 	          this.TcsTransacaoMenuAutorizacaoList = _TcsTransacaoMenuAutorizacaoElements.Select(e => (TcsTransacaoMenuAutorizacao)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsTransacaoMenuAutorizacaoElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsTransacaoMenuAutorizacao)detail.Entity).TcsTransacaoAutorizacao = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsTransacaoAutorizacao", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsTransacaoMenuAutorizacaoList", indexDetails.ToArray());
 	      }
 
 	      var _TcsTransacaoDependenteAutorizacaoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsTransacaoDependenteAutorizacao && ((TcsTransacaoDependenteAutorizacao)e.Entity).TcsTransacaoAutorizacao == null && e.Associations == null && e.OriginalAssociations == null && ((TcsTransacaoDependenteAutorizacao)e.Entity).IdTransacao == this.IdTransacao).ToList();
 	      if (_TcsTransacaoDependenteAutorizacaoElements.Count > 0 && this.TcsTransacaoDependenteAutorizacaoList.Count() == 0)
 	      {
 	          this.TcsTransacaoDependenteAutorizacaoList = _TcsTransacaoDependenteAutorizacaoElements.Select(e => (TcsTransacaoDependenteAutorizacao)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsTransacaoDependenteAutorizacaoElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsTransacaoDependenteAutorizacao)detail.Entity).TcsTransacaoAutorizacao = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsTransacaoAutorizacao", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsTransacaoDependenteAutorizacaoList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For ClasseNome
	    partial void OnClasseNomeChanging(string value);
	    partial void OnClasseNomeChanged();

	    private string _ClasseNome;

	    [DataMember(IsRequired = true, Name = "ClasseNome", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Formulário / Url", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(400)]
	    [FunctionalPoint("Precision[400:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_AUTORIZACAO.CLASSE_NOME];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.CLASSE_NOME")]
	    public string ClasseNome
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
	    partial void OnCodTransacaoChanging(string value);
	    partial void OnCodTransacaoChanged();

	    private string _CodTransacao;

	    [DataMember(IsRequired = true, Name = "CodTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Código", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_AUTORIZACAO.COD_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.COD_TRANSACAO")]
	    public string CodTransacao
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
	    //Extensibility Partial Method Definitions For DescModulo
	    partial void OnDescModuloChanging(string value);
	    partial void OnDescModuloChanged();

	    private string _DescModulo;

	    [DataMember(Name = "DescModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Módulo Base", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsModuloAutorizacao];LookUpTitle[Seleção de (Módulo Base)];LookUpQuery[executeLookUpTcsModuloAutorizacao];LookUpFinalize[finalizeLookUpTcsModuloAutorizacao];LookUpDisplayColumns[{\"DescModulo\" : \"Módulo\", \"IdModulo\" : \"Id Modulo\"}];LookUpColumns[{\"DescModulo\" : true, \"IdModulo\" : false}];FilterDataKey[TCS_TRANSACAO_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.DESC_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#DescModulo#false##100:0##Módulo#0#true##::LookUpTcsModuloAutorizacao##false#false#TCS_MODULO_AUTORIZACAO#TCS_MODULO_AUTORIZACAO#Linx.Framework.BV.TransacaoAutorizacao#IQueryable#DescricaoAplicativo[DescricaoAplicativo]##true#false", EdmKey="TCS_TRANSACAO_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.DESC_MODULO")]
	    public string DescModulo
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
	    //Extensibility Partial Method Definitions For DescObjeto
	    partial void OnDescObjetoChanging(string value);
	    partial void OnDescObjetoChanged();

	    private string _DescObjeto;

	    [DataMember(IsRequired = true, Name = "DescObjeto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Classe BO", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(400)]
	    [FunctionalPoint("Precision[400:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsObjetoAutorizacao];LookUpTitle[Seleção de (Classe BO)];LookUpQuery[executeLookUpTcsObjetoAutorizacao];LookUpFinalize[finalizeLookUpTcsObjetoAutorizacao];LookUpDisplayColumns[{\"DescObjeto\" : \"Classe BO\", \"ObjetoClasseNome\" : \"Classe Nome\", \"IdObjeto\" : \"Id Objeto\"}];LookUpColumns[{\"DescObjeto\" : true, \"ObjetoClasseNome\" : false, \"IdObjeto\" : false}];FilterDataKey[TCS_TRANSACAO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.DESC_OBJETO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#DescObjeto#false##60:0##Classe BO#0#true##::LookUpTcsObjetoAutorizacao##false#false#TCS_OBJETO_AUTORIZACAO#TCS_OBJETO_AUTORIZACAO#Linx.Framework.BV.TransacaoAutorizacao#IQueryable###true#false", EdmKey="TCS_TRANSACAO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.DESC_OBJETO")]
	    public string DescObjeto
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
	    //Extensibility Partial Method Definitions For DescTransacao
	    partial void OnDescTransacaoChanging(string value);
	    partial void OnDescTransacaoChanged();

	    private string _DescTransacao;

	    [DataMember(IsRequired = true, Name = "DescTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição Detalhada", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_AUTORIZACAO.DESC_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.DESC_TRANSACAO")]
	    public string DescTransacao
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
	    //Extensibility Partial Method Definitions For Icone
	    partial void OnIconeChanging(string value);
	    partial void OnIconeChanged();

	    private string _Icone;

	    [DataMember(Name = "Icone", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ícone", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_AUTORIZACAO.ICONE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.ICONE")]
	    public string Icone
	    {
	    	    get
	    	    {
	    	          return _Icone;
	    	    }
	    	    set
	    	    {
	    	          if (this._Icone != value)
	    	          {
	    	              this.ValidateProperty("Icone", value);
	    	              this.OnIconeChanging(value);
	    	              this.RaiseDataMemberChanging("Icone");
	    	              this._Icone = value;
	    	              this.RaiseDataMemberChanged("Icone");
	    	              this.OnIconeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdModulo
	    partial void OnIdModuloChanging(System.Nullable<long> value);
	    partial void OnIdModuloChanged();

	    private System.Nullable<long> _IdModulo;

	    [DataMember(Name = "IdModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsModuloAutorizacao];LookUpTitle[Seleção de (Id Modulo)];LookUpQuery[executeLookUpTcsModuloAutorizacao];LookUpFinalize[finalizeLookUpTcsModuloAutorizacao];LookUpDisplayColumns[{\"DescModulo\" : \"Módulo\", \"IdModulo\" : \"Id Modulo\"}];LookUpColumns[{\"DescModulo\" : true, \"IdModulo\" : false}];FilterDataKey[TCS_TRANSACAO_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.ID_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<long>#IdModulo#true##0:0##Id Modulo#1#false##::LookUpTcsModuloAutorizacao##false#false#TCS_MODULO_AUTORIZACAO#TCS_MODULO_AUTORIZACAO#Linx.Framework.BV.TransacaoAutorizacao#IQueryable#DescricaoAplicativo[DescricaoAplicativo]##true#false", EdmKey="TCS_TRANSACAO_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.ID_MODULO")]
	    public System.Nullable<long> IdModulo
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
	    //Extensibility Partial Method Definitions For IdObjeto
	    partial void OnIdObjetoChanging(long value);
	    partial void OnIdObjetoChanged();

	    private long _IdObjeto;

	    [DataMember(IsRequired = true, Name = "IdObjeto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Objeto", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsObjetoAutorizacao];LookUpTitle[Seleção de (Id Objeto)];LookUpQuery[executeLookUpTcsObjetoAutorizacao];LookUpFinalize[finalizeLookUpTcsObjetoAutorizacao];LookUpDisplayColumns[{\"DescObjeto\" : \"Classe BO\", \"ObjetoClasseNome\" : \"Classe Nome\", \"IdObjeto\" : \"Id Objeto\"}];LookUpColumns[{\"DescObjeto\" : true, \"ObjetoClasseNome\" : false, \"IdObjeto\" : false}];FilterDataKey[TCS_TRANSACAO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.ID_OBJETO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="long#IdObjeto#true##0:0##Id Objeto#2#false##::LookUpTcsObjetoAutorizacao##false#false#TCS_OBJETO_AUTORIZACAO#TCS_OBJETO_AUTORIZACAO#Linx.Framework.BV.TransacaoAutorizacao#IQueryable###true#false", EdmKey="TCS_TRANSACAO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.ID_OBJETO")]
	    public long IdObjeto
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
	    partial void OnIdTransacaoChanging(long value);
	    partial void OnIdTransacaoChanged();

	    private long _IdTransacao;

	    [DataMember(IsRequired = true, Name = "IdTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Transacao", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_AUTORIZACAO.ID_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.ID_TRANSACAO")]
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
	    //Extensibility Partial Method Definitions For Inativo
	    partial void OnInativoChanging(bool value);
	    partial void OnInativoChanged();

	    private bool _Inativo;

	    [DataMember(IsRequired = true, Name = "Inativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inativo", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_AUTORIZACAO.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.INATIVO")]
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
	    //Extensibility Partial Method Definitions For LxCorFundo
	    partial void OnLxCorFundoChanging(System.Nullable<int> value);
	    partial void OnLxCorFundoChanged();

	    private System.Nullable<int> _LxCorFundo;

	    [DataMember(Name = "LxCorFundo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cor de Fundo", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[CorFundo];KpiName[];KpiRelatedAttribute[];DefaultValue[7];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_TRANSACAO_AUTORIZACAO.LX_COR_FUNDO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.LX_COR_FUNDO")]
	    public System.Nullable<int> LxCorFundo
	    {
	    	    get
	    	    {
	    	          return _LxCorFundo;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxCorFundo != value)
	    	          {
	    	              this.ValidateProperty("LxCorFundo", value);
	    	              this.OnLxCorFundoChanging(value);
	    	              this.RaiseDataMemberChanging("LxCorFundo");
	    	              this._LxCorFundo = value;
	    	              this.RaiseDataMemberChanged("LxCorFundo");
	    	              this.OnLxCorFundoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoTransacao
	    partial void OnLxTipoTransacaoChanging(byte value);
	    partial void OnLxTipoTransacaoChanged();

	    private byte _LxTipoTransacao;

	    [DataMember(IsRequired = true, Name = "LxTipoTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo Transação", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoTransacao];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_AUTORIZACAO.LX_TIPO_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.LX_TIPO_TRANSACAO")]
	    public byte LxTipoTransacao
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
	    //Extensibility Partial Method Definitions For NomeCurto
	    partial void OnNomeCurtoChanging(string value);
	    partial void OnNomeCurtoChanged();

	    private string _NomeCurto;

	    [DataMember(IsRequired = true, Name = "NomeCurto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_AUTORIZACAO.NOME_CURTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.NOME_CURTO")]
	    public string NomeCurto
	    {
	    	    get
	    	    {
	    	          return _NomeCurto;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeCurto != value)
	    	          {
	    	              this.ValidateProperty("NomeCurto", value);
	    	              this.OnNomeCurtoChanging(value);
	    	              this.RaiseDataMemberChanging("NomeCurto");
	    	              this._NomeCurto = value;
	    	              this.RaiseDataMemberChanged("NomeCurto");
	    	              this.OnNomeCurtoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ObjetoClasseNome
	    partial void OnObjetoClasseNomeChanging(string value);
	    partial void OnObjetoClasseNomeChanged();

	    private string _ObjetoClasseNome;

	    [DataMember(IsRequired = true, Name = "ObjetoClasseNome", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Classe Nome", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsObjetoAutorizacao];LookUpTitle[Seleção de (Classe Nome)];LookUpQuery[executeLookUpTcsObjetoAutorizacao];LookUpFinalize[finalizeLookUpTcsObjetoAutorizacao];LookUpDisplayColumns[{\"DescObjeto\" : \"Classe BO\", \"ObjetoClasseNome\" : \"Classe Nome\", \"IdObjeto\" : \"Id Objeto\"}];LookUpColumns[{\"DescObjeto\" : true, \"ObjetoClasseNome\" : false, \"IdObjeto\" : false}];FilterDataKey[TCS_TRANSACAO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.CLASSE_NOME];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#ObjetoClasseNome#false##250:0##Classe Nome#1#false##::LookUpTcsObjetoAutorizacao##false#false#TCS_OBJETO_AUTORIZACAO#TCS_OBJETO_AUTORIZACAO#Linx.Framework.BV.TransacaoAutorizacao#IQueryable###true#false", EdmKey="TCS_TRANSACAO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.CLASSE_NOME")]
	    public string ObjetoClasseNome
	    {
	    	    get
	    	    {
	    	          return _ObjetoClasseNome;
	    	    }
	    	    set
	    	    {
	    	          if (this._ObjetoClasseNome != value)
	    	          {
	    	              this.ValidateProperty("ObjetoClasseNome", value);
	    	              this.OnObjetoClasseNomeChanging(value);
	    	              this.RaiseDataMemberChanging("ObjetoClasseNome");
	    	              this._ObjetoClasseNome = value;
	    	              this.RaiseDataMemberChanged("ObjetoClasseNome");
	    	              this.OnObjetoClasseNomeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Tag
	    partial void OnTagChanging(string value);
	    partial void OnTagChanged();

	    private string _Tag;

	    [DataMember(Name = "Tag", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tag", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(4000)]
	    [FunctionalPoint("Precision[4000:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_AUTORIZACAO.TAG];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.TAG")]
	    public string Tag
	    {
	    	    get
	    	    {
	    	          return _Tag;
	    	    }
	    	    set
	    	    {
	    	          if (this._Tag != value)
	    	          {
	    	              this.ValidateProperty("Tag", value);
	    	              this.OnTagChanging(value);
	    	              this.RaiseDataMemberChanging("Tag");
	    	              this._Tag = value;
	    	              this.RaiseDataMemberChanged("Tag");
	    	              this.OnTagChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<TcsTransacaoDependenteAutorizacao> _TcsTransacaoDependenteAutorizacaoList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsTransacaoAutorizacao_TcsTransacaoDependenteAutorizacao", "IdTransacao", "IdTransacao", IsForeignKey=false)]
	    [DataMember(Name = "TcsTransacaoDependenteAutorizacaoList", EmitDefaultValue = true)]
	    public IEnumerable<TcsTransacaoDependenteAutorizacao> TcsTransacaoDependenteAutorizacaoList
	    {
	        get
	        {
	
	            if (this._TcsTransacaoDependenteAutorizacaoList == null)
	            	this._TcsTransacaoDependenteAutorizacaoList = new List<TcsTransacaoDependenteAutorizacao>();
	
	            return this._TcsTransacaoDependenteAutorizacaoList;
	        }
	        set
	        {
	            if (this._TcsTransacaoDependenteAutorizacaoList != value)
	            {
	                this._TcsTransacaoDependenteAutorizacaoList = value;
	                this.RaisePropertyChanged("TcsTransacaoDependenteAutorizacaoList");
	            }
	        }
	    }	 
		
	    private IEnumerable<TcsTransacaoMenuAutorizacao> _TcsTransacaoMenuAutorizacaoList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsTransacaoAutorizacao_TcsTransacaoMenuAutorizacao", "IdTransacao", "IdTransacao", IsForeignKey=false)]
	    [DataMember(Name = "TcsTransacaoMenuAutorizacaoList", EmitDefaultValue = true)]
	    public IEnumerable<TcsTransacaoMenuAutorizacao> TcsTransacaoMenuAutorizacaoList
	    {
	        get
	        {
	
	            if (this._TcsTransacaoMenuAutorizacaoList == null)
	            	this._TcsTransacaoMenuAutorizacaoList = new List<TcsTransacaoMenuAutorizacao>();
	
	            return this._TcsTransacaoMenuAutorizacaoList;
	        }
	        set
	        {
	            if (this._TcsTransacaoMenuAutorizacaoList != value)
	            {
	                this._TcsTransacaoMenuAutorizacaoList = value;
	                this.RaisePropertyChanged("TcsTransacaoMenuAutorizacaoList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
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

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_AUTORIZACAO.TAG", Source = "Tag", Target = "TAG", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_AUTORIZACAO.ICONE", Source = "Icone", Target = "ICONE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_AUTORIZACAO.INATIVO", Source = "Inativo", Target = "INATIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_AUTORIZACAO.NOME_CURTO", Source = "NomeCurto", Target = "NOME_CURTO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_AUTORIZACAO.CLASSE_NOME", Source = "ClasseNome", Target = "CLASSE_NOME", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_AUTORIZACAO.ID_TRANSACAO", Source = "IdTransacao", Target = "ID_TRANSACAO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_AUTORIZACAO.LX_COR_FUNDO", Source = "LxCorFundo", Target = "LX_COR_FUNDO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_AUTORIZACAO.COD_TRANSACAO", Source = "CodTransacao", Target = "COD_TRANSACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_AUTORIZACAO.DESC_TRANSACAO", Source = "DescTransacao", Target = "DESC_TRANSACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_AUTORIZACAO.LX_TIPO_TRANSACAO", Source = "LxTipoTransacao", Target = "LX_TIPO_TRANSACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.ID_MODULO", Source = "IdModulo", Target = "ID_MODULO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_MODULO_AUTORIZACAO", RelationPropertyName = "TCS_MODULO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.ID_OBJETO", Source = "IdObjeto", Target = "ID_OBJETO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_OBJETO_AUTORIZACAO", RelationPropertyName = "TCS_OBJETO_AUTORIZACAO" });

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
	             Linx.Framework.BV.BusinessMediaHelper.SyncMedia("TCS_TRANSACAO_AUTORIZACAO", null, null, this.TableMedia.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(e => new Guid(e)).ToList());
	         }
	         else if (operation == DomainOperation.Delete) {
	             Linx.Framework.BV.BusinessMediaHelper.SyncMedia("TCS_TRANSACAO_AUTORIZACAO", null, null, new List<Guid>() { Guid.Empty });
	         }
	    }

	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxCorFundoValues()
	    {
	    	    return Linx.Framework.BV.Domains.CorFundo.GetValues();
	    }
	    private string _lxCorFundoName;
	    [DataMember(IsRequired = false, Name = "LxCorFundoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Cor de Fundo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxCorFundoName
	    {
	    	    get { if (this.LxCorFundo.IsNull()) { _lxCorFundoName = String.Empty; } else { string key = this.LxCorFundo.ToString(); var dmValues = this.GetLxCorFundoValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxCorFundoName) _lxCorFundoName = domainName; } return _lxCorFundoName; } set { _lxCorFundoName = value;  }
	    }
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

		

	[LinxPublicationView(PrimaryKeys="TCS_TRANSACAO_MENU_AUTORIZACAO.ID_TCS_TRANSACAO_MENU_AUTORIZACAO", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Menu];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#.TCS_TRANSACAO_MENU_AUTORIZACAO_LISTA as #Alias#];EdmEntityName[TCS_TRANSACAO_MENU_AUTORIZACAO];EntityRelations[TCS_MODULO_MENU_AUTORIZACAO(TCS_MODULO_MENU_AUTORIZACAO)#TCS_MODULO_AUTORIZACAO(TCS_MODULO_AUTORIZACAO)#TCS_APLICATIVO(TCS_APLICATIVO)#MODULO_MENU_SUPERIOR(TCS_MODULO_MENU_AUTORIZACAO)#TCS_TRANSACAO_AUTORIZACAO(TCS_TRANSACAO_AUTORIZACAO)#TCS_OBJETO_AUTORIZACAO(TCS_OBJETO_AUTORIZACAO)];EdmParentEntityName[TCS_TRANSACAO_AUTORIZACAO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsTransacaoMenuAutorizacao")]
	[Serializable()]
	public partial class TcsTransacaoMenuAutorizacao : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(TransacaoAutorizacaoDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsTransacaoAutorizacao");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTransacao"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdTransacao));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsTransacaoAutorizacao
	         this.TcsTransacaoAutorizacao = (from r in context.GetTcsTransacaoAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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

	    [DataMember(Name = "ClasseNome", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Formulário / Url", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [LinxStringLength(400)]
	    [FunctionalPoint("Precision[400:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.CLASSE_NOME];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.CLASSE_NOME")]
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
	    partial void OnCodTransacaoChanging(string value);
	    partial void OnCodTransacaoChanged();

	    private string _CodTransacao;

	    [DataMember(Name = "CodTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Código", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.COD_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.COD_TRANSACAO")]
	    public string CodTransacao
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
	    //Extensibility Partial Method Definitions For DescModulo
	    partial void OnDescModuloChanging(string value);
	    partial void OnDescModuloChanged();

	    private string _DescModulo;

	    [DataMember(IsRequired = true, Name = "DescModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Módulo Base", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsModuloMenuAutorizacao];LookUpTitle[Seleção de (Módulo Base)];LookUpQuery[executeLookUpTcsModuloMenuAutorizacao];LookUpFinalize[finalizeLookUpTcsModuloMenuAutorizacao];LookUpDisplayColumns[{\"DescricaoAplicativo\" : \"Aplicativo\", \"DescModulo\" : \"Módulo\", \"DescModuloMenu\" : \"Menu\", \"IdModulo\" : \"Id Modulo\", \"InativoModulo\" : \"Inativo\", \"IdModuloMenu\" : \"Id Modulo Menu\"}];LookUpColumns[{\"DescricaoAplicativo\" : true, \"DescModulo\" : true, \"DescModuloMenu\" : true, \"IdModulo\" : false, \"InativoModulo\" : true, \"IdModuloMenu\" : false}];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.DESC_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#DescModulo#false##100:0##Módulo#1#true##::LookUpTcsModuloMenuAutorizacao##true#false#TCS_MODULO_MENU_AUTORIZACAO#TCS_MODULO_MENU_AUTORIZACAO#Linx.Framework.BV.TransacaoAutorizacao#IQueryable###true#true", EdmKey="TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.DESC_MODULO")]
	    public string DescModulo
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
	    //Extensibility Partial Method Definitions For DescModuloMenu
	    partial void OnDescModuloMenuChanging(string value);
	    partial void OnDescModuloMenuChanged();

	    private string _DescModuloMenu;

	    [DataMember(IsRequired = true, Name = "DescModuloMenu", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Menu", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsModuloMenuAutorizacao];LookUpTitle[Seleção de (Menu)];LookUpQuery[executeLookUpTcsModuloMenuAutorizacao];LookUpFinalize[finalizeLookUpTcsModuloMenuAutorizacao];LookUpDisplayColumns[{\"DescricaoAplicativo\" : \"Aplicativo\", \"DescModulo\" : \"Módulo\", \"DescModuloMenu\" : \"Menu\", \"IdModulo\" : \"Id Modulo\", \"InativoModulo\" : \"Inativo\", \"IdModuloMenu\" : \"Id Modulo Menu\"}];LookUpColumns[{\"DescricaoAplicativo\" : true, \"DescModulo\" : true, \"DescModuloMenu\" : true, \"IdModulo\" : false, \"InativoModulo\" : true, \"IdModuloMenu\" : false}];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_MODULO_MENU_AUTORIZACAO.DESC_MODULO_MENU];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescModuloMenu#false##1000##Menu#2#true##::LookUpTcsModuloMenuAutorizacao##true#false#TCS_MODULO_MENU_AUTORIZACAO#TCS_MODULO_MENU_AUTORIZACAO#Linx.Framework.BV.TransacaoAutorizacao#IQueryable###true#true", EdmKey="TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_MODULO_MENU_AUTORIZACAO.DESC_MODULO_MENU")]
	    public string DescModuloMenu
	    {
	    	    get
	    	    {
	    	          return _DescModuloMenu;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescModuloMenu != value)
	    	          {
	    	              this.ValidateProperty("DescModuloMenu", value);
	    	              this.OnDescModuloMenuChanging(value);
	    	              this.RaiseDataMemberChanging("DescModuloMenu");
	    	              this._DescModuloMenu = value;
	    	              this.RaiseDataMemberChanged("DescModuloMenu");
	    	              this.OnDescModuloMenuChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescricaoAplicativo
	    partial void OnDescricaoAplicativoChanging(string value);
	    partial void OnDescricaoAplicativoChanged();

	    private string _DescricaoAplicativo;

	    [DataMember(Name = "DescricaoAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicativo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsModuloMenuAutorizacao];LookUpTitle[Seleção de (Aplicativo)];LookUpQuery[executeLookUpTcsModuloMenuAutorizacao];LookUpFinalize[finalizeLookUpTcsModuloMenuAutorizacao];LookUpDisplayColumns[{\"DescricaoAplicativo\" : \"Aplicativo\", \"DescModulo\" : \"Módulo\", \"DescModuloMenu\" : \"Menu\", \"IdModulo\" : \"Id Modulo\", \"InativoModulo\" : \"Inativo\", \"IdModuloMenu\" : \"Id Modulo Menu\"}];LookUpColumns[{\"DescricaoAplicativo\" : true, \"DescModulo\" : true, \"DescModuloMenu\" : true, \"IdModulo\" : false, \"InativoModulo\" : true, \"IdModuloMenu\" : false}];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#DescricaoAplicativo#false##250:0##Aplicativo#0#true##::LookUpTcsModuloMenuAutorizacao##true#false#TCS_MODULO_MENU_AUTORIZACAO#TCS_MODULO_MENU_AUTORIZACAO#Linx.Framework.BV.TransacaoAutorizacao#IQueryable###true#true", EdmKey="TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO")]
	    public string DescricaoAplicativo
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
	    //Extensibility Partial Method Definitions For DescTransacao
	    partial void OnDescTransacaoChanging(System.String value);
	    partial void OnDescTransacaoChanged();

	    private System.String _DescTransacao;

	    [DataMember(Name = "DescTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição Detalhada", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.DESC_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.DESC_TRANSACAO")]
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
	    //Extensibility Partial Method Definitions For IdModulo
	    partial void OnIdModuloChanging(long value);
	    partial void OnIdModuloChanged();

	    private long _IdModulo;

	    [DataMember(IsRequired = true, Name = "IdModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsModuloMenuAutorizacao];LookUpTitle[Seleção de (Id Modulo)];LookUpQuery[executeLookUpTcsModuloMenuAutorizacao];LookUpFinalize[finalizeLookUpTcsModuloMenuAutorizacao];LookUpDisplayColumns[{\"DescricaoAplicativo\" : \"Aplicativo\", \"DescModulo\" : \"Módulo\", \"DescModuloMenu\" : \"Menu\", \"IdModulo\" : \"Id Modulo\", \"InativoModulo\" : \"Inativo\", \"IdModuloMenu\" : \"Id Modulo Menu\"}];LookUpColumns[{\"DescricaoAplicativo\" : true, \"DescModulo\" : true, \"DescModuloMenu\" : true, \"IdModulo\" : false, \"InativoModulo\" : true, \"IdModuloMenu\" : false}];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.ID_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="long#IdModulo#true##0:0##Id Modulo#4#false##::LookUpTcsModuloMenuAutorizacao##true#false#TCS_MODULO_MENU_AUTORIZACAO#TCS_MODULO_MENU_AUTORIZACAO#Linx.Framework.BV.TransacaoAutorizacao#IQueryable###true#true", EdmKey="TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.ID_MODULO")]
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
	    //Extensibility Partial Method Definitions For IdModuloMenu
	    partial void OnIdModuloMenuChanging(long value);
	    partial void OnIdModuloMenuChanged();

	    private long _IdModuloMenu;

	    [DataMember(IsRequired = true, Name = "IdModuloMenu", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo Menu", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsModuloMenuAutorizacao];LookUpTitle[Seleção de (Id Modulo Menu)];LookUpQuery[executeLookUpTcsModuloMenuAutorizacao];LookUpFinalize[finalizeLookUpTcsModuloMenuAutorizacao];LookUpDisplayColumns[{\"DescricaoAplicativo\" : \"Aplicativo\", \"DescModulo\" : \"Módulo\", \"DescModuloMenu\" : \"Menu\", \"IdModulo\" : \"Id Modulo\", \"InativoModulo\" : \"Inativo\", \"IdModuloMenu\" : \"Id Modulo Menu\"}];LookUpColumns[{\"DescricaoAplicativo\" : true, \"DescModulo\" : true, \"DescModuloMenu\" : true, \"IdModulo\" : false, \"InativoModulo\" : true, \"IdModuloMenu\" : false}];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_MODULO_MENU_AUTORIZACAO.ID_MODULO_MENU];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="long#IdModuloMenu#true##0:0##Id Modulo Menu#6#false##::LookUpTcsModuloMenuAutorizacao##true#false#TCS_MODULO_MENU_AUTORIZACAO#TCS_MODULO_MENU_AUTORIZACAO#Linx.Framework.BV.TransacaoAutorizacao#IQueryable###true#true", EdmKey="TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_MODULO_MENU_AUTORIZACAO.ID_MODULO_MENU")]
	    public long IdModuloMenu
	    {
	    	    get
	    	    {
	    	          return _IdModuloMenu;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdModuloMenu != value)
	    	          {
	    	              this.ValidateProperty("IdModuloMenu", value);
	    	              this.OnIdModuloMenuChanging(value);
	    	              this.RaiseDataMemberChanging("IdModuloMenu");
	    	              this._IdModuloMenu = value;
	    	              this.RaiseDataMemberChanged("IdModuloMenu");
	    	              this.OnIdModuloMenuChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsTransacaoMenuAutorizacao
	    partial void OnIdTcsTransacaoMenuAutorizacaoChanging(int value);
	    partial void OnIdTcsTransacaoMenuAutorizacaoChanged();

	    private int _IdTcsTransacaoMenuAutorizacao;

	    [DataMember(IsRequired = true, Name = "IdTcsTransacaoMenuAutorizacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Transacao Menu Autorizacao", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.ID_TCS_TRANSACAO_MENU_AUTORIZACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_MENU_AUTORIZACAO.ID_TCS_TRANSACAO_MENU_AUTORIZACAO")]
	    public int IdTcsTransacaoMenuAutorizacao
	    {
	    	    get
	    	    {
	    	          return _IdTcsTransacaoMenuAutorizacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsTransacaoMenuAutorizacao != value)
	    	          {
	    	              this.ValidateProperty("IdTcsTransacaoMenuAutorizacao", value);
	    	              this.OnIdTcsTransacaoMenuAutorizacaoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsTransacaoMenuAutorizacao");
	    	              this._IdTcsTransacaoMenuAutorizacao = value;
	    	              this.RaiseDataMemberChanged("IdTcsTransacaoMenuAutorizacao");
	    	              this.OnIdTcsTransacaoMenuAutorizacaoChanged();
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
	    [Display(Name = "Id Transacao", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.ID_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.ID_TRANSACAO")]
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
	    //Extensibility Partial Method Definitions For Inativo
	    partial void OnInativoChanging(bool value);
	    partial void OnInativoChanged();

	    private bool _Inativo;

	    [DataMember(IsRequired = true, Name = "Inativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inativo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_MENU_AUTORIZACAO.INATIVO")]
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
	    //Extensibility Partial Method Definitions For InativoModulo
	    partial void OnInativoModuloChanging(bool value);
	    partial void OnInativoModuloChanged();

	    private bool _InativoModulo;

	    [DataMember(IsRequired = true, Name = "InativoModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inativo", Description="", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsModuloMenuAutorizacao];LookUpTitle[Seleção de (Inativo)];LookUpQuery[executeLookUpTcsModuloMenuAutorizacao];LookUpFinalize[finalizeLookUpTcsModuloMenuAutorizacao];LookUpDisplayColumns[{\"DescricaoAplicativo\" : \"Aplicativo\", \"DescModulo\" : \"Módulo\", \"DescModuloMenu\" : \"Menu\", \"IdModulo\" : \"Id Modulo\", \"InativoModulo\" : \"Inativo\", \"IdModuloMenu\" : \"Id Modulo Menu\"}];LookUpColumns[{\"DescricaoAplicativo\" : true, \"DescModulo\" : true, \"DescModuloMenu\" : true, \"IdModulo\" : false, \"InativoModulo\" : true, \"IdModuloMenu\" : false}];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="bool#InativoModulo#false##0:0##Inativo#5#true##::LookUpTcsModuloMenuAutorizacao##true#false#TCS_MODULO_MENU_AUTORIZACAO#TCS_MODULO_MENU_AUTORIZACAO#Linx.Framework.BV.TransacaoAutorizacao#IQueryable###true#true", EdmKey="TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.INATIVO")]
	    public bool InativoModulo
	    {
	    	    get
	    	    {
	    	          return _InativoModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._InativoModulo != value)
	    	          {
	    	              this.ValidateProperty("InativoModulo", value);
	    	              this.OnInativoModuloChanging(value);
	    	              this.RaiseDataMemberChanging("InativoModulo");
	    	              this._InativoModulo = value;
	    	              this.RaiseDataMemberChanged("InativoModulo");
	    	              this.OnInativoModuloChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For OrdemNavegacao
	    partial void OnOrdemNavegacaoChanging(byte value);
	    partial void OnOrdemNavegacaoChanged();

	    private byte _OrdemNavegacao;

	    [DataMember(IsRequired = true, Name = "OrdemNavegacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ordem", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.ORDEM_NAVEGACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_MENU_AUTORIZACAO.ORDEM_NAVEGACAO")]
	    public byte OrdemNavegacao
	    {
	    	    get
	    	    {
	    	          return _OrdemNavegacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._OrdemNavegacao != value)
	    	          {
	    	              this.ValidateProperty("OrdemNavegacao", value);
	    	              this.OnOrdemNavegacaoChanging(value);
	    	              this.RaiseDataMemberChanging("OrdemNavegacao");
	    	              this._OrdemNavegacao = value;
	    	              this.RaiseDataMemberChanged("OrdemNavegacao");
	    	              this.OnOrdemNavegacaoChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsTransacaoAutorizacao _TcsTransacaoAutorizacao;
	    [DataMember(Name = "TcsTransacaoAutorizacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsTransacaoAutorizacao_TcsTransacaoMenuAutorizacao", "IdTransacao", "IdTransacao", IsForeignKey=true)]
	    public TcsTransacaoAutorizacao TcsTransacaoAutorizacao
	    {
	        get
	        {
	            return this._TcsTransacaoAutorizacao;
	        }
	        set
	        {
	            if (this._TcsTransacaoAutorizacao != value)
	            {
	                this._TcsTransacaoAutorizacao = value;
	                this.RaisePropertyChanged("TcsTransacaoAutorizacaoList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_TRANSACAO_MENU_AUTORIZACAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_TRANSACAO_MENU_AUTORIZACAO), QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_MENU_AUTORIZACAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_MENU_AUTORIZACAO.INATIVO", Source = "Inativo", Target = "INATIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_MENU_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_MENU_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_MENU_AUTORIZACAO.ORDEM_NAVEGACAO", Source = "OrdemNavegacao", Target = "ORDEM_NAVEGACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_MENU_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_MENU_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_MENU_AUTORIZACAO.ID_TCS_TRANSACAO_MENU_AUTORIZACAO", Source = "IdTcsTransacaoMenuAutorizacao", Target = "ID_TCS_TRANSACAO_MENU_AUTORIZACAO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_MENU_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_MENU_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.ID_TRANSACAO", Source = "IdTransacao", Target = "ID_TRANSACAO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_MODULO_MENU_AUTORIZACAO.ID_MODULO_MENU", Source = "IdModuloMenu", Target = "ID_MODULO_MENU", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_MODULO_MENU_AUTORIZACAO", RelationPropertyName = "TCS_MODULO_MENU_AUTORIZACAO" });

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
	             Linx.Framework.BV.BusinessMediaHelper.SyncMedia("TCS_TRANSACAO_MENU_AUTORIZACAO", this.IdTcsTransacaoMenuAutorizacao, null, this.TableMedia.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(e => new Guid(e)).ToList());
	         }
	         else if (operation == DomainOperation.Delete) {
	             Linx.Framework.BV.BusinessMediaHelper.SyncMedia("TCS_TRANSACAO_MENU_AUTORIZACAO", this.IdTcsTransacaoMenuAutorizacao, null, new List<Guid>() { Guid.Empty });
	         }
	    }

	    #endregion Media Storage

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.ID_TRANSACAO_DEPENDENTE", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Transações Dependentes];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTransacaoDependente];ReadOnly[false];Entities[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO:IdTransacaoDependente];SubQueryInfo[Select 1 From #ParentAlias#.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO_LISTA as #Alias#];EdmEntityName[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO];EntityRelations[TCS_TRANSACAO_AUTORIZACAO(TCS_TRANSACAO_AUTORIZACAO)#TCS_MODULO_AUTORIZACAO(TCS_MODULO_AUTORIZACAO)#TCS_APLICATIVO(TCS_APLICATIVO)#TCS_OBJETO_AUTORIZACAO(TCS_OBJETO_AUTORIZACAO)#TRANSACAO_RELACIONADA(TCS_TRANSACAO_AUTORIZACAO)];EdmParentEntityName[TCS_TRANSACAO_AUTORIZACAO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsTransacaoDependenteAutorizacao")]
	[Serializable()]
	public partial class TcsTransacaoDependenteAutorizacao : Linx.Data.Entity
	{

	

	    public TcsTransacaoDependenteAutorizacao() : this(true) { }

	    public TcsTransacaoDependenteAutorizacao(bool setDefaults) 
	    {

	        if (setDefaults)
	        {
	        	        LxPosicaoDaTransacao = 1;
	        	        LxTipoLayout = 7;
	        	        MostraBotaoAdicao = true;
	        	        MostraBotaoEdicao = true;
	        	        MostraBotaoExclusao = true;
	        	        MostraBotaoImpressao = true;
	        	        MostraBotaoLayout = true;
	        	        MostraBotaoLimpa = true;
	        	        MostraBotaoNavegacao = true;
	        	        MostraBotaoPesquisa = true;
	        	        MostraBotaoPesquisaEsp = true;
	        	        Visivel = true;
	        }	

	    }

			
	

	
	    #region Load Data Parent
		

	    public void LoadParent(TransacaoAutorizacaoDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsTransacaoAutorizacao");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTransacao"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdTransacao));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsTransacaoAutorizacao
	         this.TcsTransacaoAutorizacao = (from r in context.GetTcsTransacaoAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	    [Display(Name = "Formulário / Url", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(400)]
	    [FunctionalPoint("Precision[400:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsTransacaoDependente];LookUpTitle[Seleção de (Formulário / Url)];LookUpQuery[executeLookUpTcsTransacaoDependente];LookUpFinalize[finalizeLookUpTcsTransacaoDependente];LookUpDisplayColumns[{\"IdTransacao\" : \"\", \"DescTransacao\" : \"Transação\", \"ClasseNome\" : \"Formulário\"}];LookUpColumns[{\"IdTransacao\" : false, \"DescTransacao\" : true, \"ClasseNome\" : true}];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.TRANSACAO_RELACIONADA.CLASSE_NOME];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#ClasseNome#false##40##Formulário#2#true##::LookUpTcsTransacaoDependente##false#false###Linx.Framework.BV.TransacaoAutorizacao#IQueryable###true#false", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.TRANSACAO_RELACIONADA.CLASSE_NOME")]
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
	    //Extensibility Partial Method Definitions For CompartilhaBoPrincipal
	    partial void OnCompartilhaBoPrincipalChanging(Boolean value);
	    partial void OnCompartilhaBoPrincipalChanged();

	    private Boolean _CompartilhaBoPrincipal;

	    [DataMember(IsRequired = true, Name = "CompartilhaBoPrincipal", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Compartilha BO Principal", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.COMPARTILHA_BO_PRINCIPAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.COMPARTILHA_BO_PRINCIPAL")]
	    public Boolean CompartilhaBoPrincipal
	    {
	    	    get
	    	    {
	    	          return _CompartilhaBoPrincipal;
	    	    }
	    	    set
	    	    {
	    	          if (this._CompartilhaBoPrincipal != value)
	    	          {
	    	              this.ValidateProperty("CompartilhaBoPrincipal", value);
	    	              this.OnCompartilhaBoPrincipalChanging(value);
	    	              this.RaiseDataMemberChanging("CompartilhaBoPrincipal");
	    	              this._CompartilhaBoPrincipal = value;
	    	              this.RaiseDataMemberChanged("CompartilhaBoPrincipal");
	    	              this.OnCompartilhaBoPrincipalChanged();
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
	    [Display(Name = "Descrição Detalhada", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsTransacaoDependente];LookUpTitle[Seleção de (Descrição Detalhada)];LookUpQuery[executeLookUpTcsTransacaoDependente];LookUpFinalize[finalizeLookUpTcsTransacaoDependente];LookUpDisplayColumns[{\"IdTransacao\" : \"\", \"DescTransacao\" : \"Transação\", \"ClasseNome\" : \"Formulário\"}];LookUpColumns[{\"IdTransacao\" : false, \"DescTransacao\" : true, \"ClasseNome\" : true}];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.TRANSACAO_RELACIONADA.DESC_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#DescTransacao#false##60##Transação#1#true##::LookUpTcsTransacaoDependente##false#false###Linx.Framework.BV.TransacaoAutorizacao#IQueryable###true#false", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.TRANSACAO_RELACIONADA.DESC_TRANSACAO")]
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
	    //Extensibility Partial Method Definitions For ExecutaPesquisa
	    partial void OnExecutaPesquisaChanging(Boolean value);
	    partial void OnExecutaPesquisaChanged();

	    private Boolean _ExecutaPesquisa;

	    [DataMember(IsRequired = true, Name = "ExecutaPesquisa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Sempre Executa Pesquisa", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.EXECUTA_PESQUISA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.EXECUTA_PESQUISA")]
	    public Boolean ExecutaPesquisa
	    {
	    	    get
	    	    {
	    	          return _ExecutaPesquisa;
	    	    }
	    	    set
	    	    {
	    	          if (this._ExecutaPesquisa != value)
	    	          {
	    	              this.ValidateProperty("ExecutaPesquisa", value);
	    	              this.OnExecutaPesquisaChanging(value);
	    	              this.RaiseDataMemberChanging("ExecutaPesquisa");
	    	              this._ExecutaPesquisa = value;
	    	              this.RaiseDataMemberChanged("ExecutaPesquisa");
	    	              this.OnExecutaPesquisaChanged();
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
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.ID_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.ID_TRANSACAO")]
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
	    //Extensibility Partial Method Definitions For IdTransacaoRelacionada
	    partial void OnIdTransacaoRelacionadaChanging(Int64 value);
	    partial void OnIdTransacaoRelacionadaChanged();

	    private Int64 _IdTransacaoRelacionada;

	    [DataMember(IsRequired = true, Name = "IdTransacaoRelacionada", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Transacao1", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsTransacaoDependente];LookUpTitle[Seleção de (Id Transacao1)];LookUpQuery[executeLookUpTcsTransacaoDependente];LookUpFinalize[finalizeLookUpTcsTransacaoDependente];LookUpDisplayColumns[{\"IdTransacao\" : \"\", \"DescTransacao\" : \"Transação\", \"ClasseNome\" : \"Formulário\"}];LookUpColumns[{\"IdTransacao\" : false, \"DescTransacao\" : true, \"ClasseNome\" : true}];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.TRANSACAO_RELACIONADA.ID_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdTransacao#true##12###0#false##::LookUpTcsTransacaoDependente##false#false###Linx.Framework.BV.TransacaoAutorizacao#IQueryable###true#false", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.TRANSACAO_RELACIONADA.ID_TRANSACAO")]
	    public Int64 IdTransacaoRelacionada
	    {
	    	    get
	    	    {
	    	          return _IdTransacaoRelacionada;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTransacaoRelacionada != value)
	    	          {
	    	              this.ValidateProperty("IdTransacaoRelacionada", value);
	    	              this.OnIdTransacaoRelacionadaChanging(value);
	    	              this.RaiseDataMemberChanging("IdTransacaoRelacionada");
	    	              this._IdTransacaoRelacionada = value;
	    	              this.RaiseDataMemberChanged("IdTransacaoRelacionada");
	    	              this.OnIdTransacaoRelacionadaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTransacaoDependente
	    partial void OnIdTransacaoDependenteChanging(Int64 value);
	    partial void OnIdTransacaoDependenteChanged();

	    private Int64 _IdTransacaoDependente;

	    [DataMember(IsRequired = true, Name = "IdTransacaoDependente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Transacao Dependente", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.ID_TRANSACAO_DEPENDENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.ID_TRANSACAO_DEPENDENTE")]
	    public Int64 IdTransacaoDependente
	    {
	    	    get
	    	    {
	    	          return _IdTransacaoDependente;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTransacaoDependente != value)
	    	          {
	    	              this.ValidateProperty("IdTransacaoDependente", value);
	    	              this.OnIdTransacaoDependenteChanging(value);
	    	              this.RaiseDataMemberChanging("IdTransacaoDependente");
	    	              this._IdTransacaoDependente = value;
	    	              this.RaiseDataMemberChanged("IdTransacaoDependente");
	    	              this.OnIdTransacaoDependenteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxPosicaoDaTransacao
	    partial void OnLxPosicaoDaTransacaoChanging(Byte value);
	    partial void OnLxPosicaoDaTransacaoChanged();

	    private Byte _LxPosicaoDaTransacao;

	    [DataMember(IsRequired = true, Name = "LxPosicaoDaTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Posição", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[PosicaoDaTransacao];KpiName[];KpiRelatedAttribute[];DefaultValue[1];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.LX_POSICAO_DA_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.LX_POSICAO_DA_TRANSACAO")]
	    public Byte LxPosicaoDaTransacao
	    {
	    	    get
	    	    {
	    	          return _LxPosicaoDaTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxPosicaoDaTransacao != value)
	    	          {
	    	              this.ValidateProperty("LxPosicaoDaTransacao", value);
	    	              this.OnLxPosicaoDaTransacaoChanging(value);
	    	              this.RaiseDataMemberChanging("LxPosicaoDaTransacao");
	    	              this._LxPosicaoDaTransacao = value;
	    	              this.RaiseDataMemberChanged("LxPosicaoDaTransacao");
	    	              this.OnLxPosicaoDaTransacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoLayout
	    partial void OnLxTipoLayoutChanging(Byte value);
	    partial void OnLxTipoLayoutChanged();

	    private Byte _LxTipoLayout;

	    [DataMember(IsRequired = true, Name = "LxTipoLayout", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo do Layout", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoLayoutDependente];KpiName[];KpiRelatedAttribute[];DefaultValue[7];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.LX_TIPO_LAYOUT];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.LX_TIPO_LAYOUT")]
	    public Byte LxTipoLayout
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
	    //Extensibility Partial Method Definitions For MostraBotaoAdicao
	    partial void OnMostraBotaoAdicaoChanging(Boolean value);
	    partial void OnMostraBotaoAdicaoChanged();

	    private Boolean _MostraBotaoAdicao;

	    [DataMember(IsRequired = true, Name = "MostraBotaoAdicao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Adição", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[true];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_ADICAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_ADICAO")]
	    public Boolean MostraBotaoAdicao
	    {
	    	    get
	    	    {
	    	          return _MostraBotaoAdicao;
	    	    }
	    	    set
	    	    {
	    	          if (this._MostraBotaoAdicao != value)
	    	          {
	    	              this.ValidateProperty("MostraBotaoAdicao", value);
	    	              this.OnMostraBotaoAdicaoChanging(value);
	    	              this.RaiseDataMemberChanging("MostraBotaoAdicao");
	    	              this._MostraBotaoAdicao = value;
	    	              this.RaiseDataMemberChanged("MostraBotaoAdicao");
	    	              this.OnMostraBotaoAdicaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For MostraBotaoEdicao
	    partial void OnMostraBotaoEdicaoChanging(Boolean value);
	    partial void OnMostraBotaoEdicaoChanged();

	    private Boolean _MostraBotaoEdicao;

	    [DataMember(IsRequired = true, Name = "MostraBotaoEdicao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Edição", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[true];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_EDICAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_EDICAO")]
	    public Boolean MostraBotaoEdicao
	    {
	    	    get
	    	    {
	    	          return _MostraBotaoEdicao;
	    	    }
	    	    set
	    	    {
	    	          if (this._MostraBotaoEdicao != value)
	    	          {
	    	              this.ValidateProperty("MostraBotaoEdicao", value);
	    	              this.OnMostraBotaoEdicaoChanging(value);
	    	              this.RaiseDataMemberChanging("MostraBotaoEdicao");
	    	              this._MostraBotaoEdicao = value;
	    	              this.RaiseDataMemberChanged("MostraBotaoEdicao");
	    	              this.OnMostraBotaoEdicaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For MostraBotaoExclusao
	    partial void OnMostraBotaoExclusaoChanging(Boolean value);
	    partial void OnMostraBotaoExclusaoChanged();

	    private Boolean _MostraBotaoExclusao;

	    [DataMember(IsRequired = true, Name = "MostraBotaoExclusao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Exclusão", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[true];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_EXCLUSAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_EXCLUSAO")]
	    public Boolean MostraBotaoExclusao
	    {
	    	    get
	    	    {
	    	          return _MostraBotaoExclusao;
	    	    }
	    	    set
	    	    {
	    	          if (this._MostraBotaoExclusao != value)
	    	          {
	    	              this.ValidateProperty("MostraBotaoExclusao", value);
	    	              this.OnMostraBotaoExclusaoChanging(value);
	    	              this.RaiseDataMemberChanging("MostraBotaoExclusao");
	    	              this._MostraBotaoExclusao = value;
	    	              this.RaiseDataMemberChanged("MostraBotaoExclusao");
	    	              this.OnMostraBotaoExclusaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For MostraBotaoImpressao
	    partial void OnMostraBotaoImpressaoChanging(Boolean value);
	    partial void OnMostraBotaoImpressaoChanged();

	    private Boolean _MostraBotaoImpressao;

	    [DataMember(IsRequired = true, Name = "MostraBotaoImpressao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Impressão", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[true];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_IMPRESSAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_IMPRESSAO")]
	    public Boolean MostraBotaoImpressao
	    {
	    	    get
	    	    {
	    	          return _MostraBotaoImpressao;
	    	    }
	    	    set
	    	    {
	    	          if (this._MostraBotaoImpressao != value)
	    	          {
	    	              this.ValidateProperty("MostraBotaoImpressao", value);
	    	              this.OnMostraBotaoImpressaoChanging(value);
	    	              this.RaiseDataMemberChanging("MostraBotaoImpressao");
	    	              this._MostraBotaoImpressao = value;
	    	              this.RaiseDataMemberChanged("MostraBotaoImpressao");
	    	              this.OnMostraBotaoImpressaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For MostraBotaoLayout
	    partial void OnMostraBotaoLayoutChanging(Boolean value);
	    partial void OnMostraBotaoLayoutChanged();

	    private Boolean _MostraBotaoLayout;

	    [DataMember(IsRequired = true, Name = "MostraBotaoLayout", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Layout", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[true];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_LAYOUT];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_LAYOUT")]
	    public Boolean MostraBotaoLayout
	    {
	    	    get
	    	    {
	    	          return _MostraBotaoLayout;
	    	    }
	    	    set
	    	    {
	    	          if (this._MostraBotaoLayout != value)
	    	          {
	    	              this.ValidateProperty("MostraBotaoLayout", value);
	    	              this.OnMostraBotaoLayoutChanging(value);
	    	              this.RaiseDataMemberChanging("MostraBotaoLayout");
	    	              this._MostraBotaoLayout = value;
	    	              this.RaiseDataMemberChanged("MostraBotaoLayout");
	    	              this.OnMostraBotaoLayoutChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For MostraBotaoLimpa
	    partial void OnMostraBotaoLimpaChanging(Boolean value);
	    partial void OnMostraBotaoLimpaChanged();

	    private Boolean _MostraBotaoLimpa;

	    [DataMember(IsRequired = true, Name = "MostraBotaoLimpa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Limpa", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[true];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_LIMPA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_LIMPA")]
	    public Boolean MostraBotaoLimpa
	    {
	    	    get
	    	    {
	    	          return _MostraBotaoLimpa;
	    	    }
	    	    set
	    	    {
	    	          if (this._MostraBotaoLimpa != value)
	    	          {
	    	              this.ValidateProperty("MostraBotaoLimpa", value);
	    	              this.OnMostraBotaoLimpaChanging(value);
	    	              this.RaiseDataMemberChanging("MostraBotaoLimpa");
	    	              this._MostraBotaoLimpa = value;
	    	              this.RaiseDataMemberChanged("MostraBotaoLimpa");
	    	              this.OnMostraBotaoLimpaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For MostraBotaoNavegacao
	    partial void OnMostraBotaoNavegacaoChanging(Boolean value);
	    partial void OnMostraBotaoNavegacaoChanged();

	    private Boolean _MostraBotaoNavegacao;

	    [DataMember(IsRequired = true, Name = "MostraBotaoNavegacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Navegação", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[true];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_NAVEGACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_NAVEGACAO")]
	    public Boolean MostraBotaoNavegacao
	    {
	    	    get
	    	    {
	    	          return _MostraBotaoNavegacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._MostraBotaoNavegacao != value)
	    	          {
	    	              this.ValidateProperty("MostraBotaoNavegacao", value);
	    	              this.OnMostraBotaoNavegacaoChanging(value);
	    	              this.RaiseDataMemberChanging("MostraBotaoNavegacao");
	    	              this._MostraBotaoNavegacao = value;
	    	              this.RaiseDataMemberChanged("MostraBotaoNavegacao");
	    	              this.OnMostraBotaoNavegacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For MostraBotaoPesquisa
	    partial void OnMostraBotaoPesquisaChanging(Boolean value);
	    partial void OnMostraBotaoPesquisaChanged();

	    private Boolean _MostraBotaoPesquisa;

	    [DataMember(IsRequired = true, Name = "MostraBotaoPesquisa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Pesquisa", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[true];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_PESQUISA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_PESQUISA")]
	    public Boolean MostraBotaoPesquisa
	    {
	    	    get
	    	    {
	    	          return _MostraBotaoPesquisa;
	    	    }
	    	    set
	    	    {
	    	          if (this._MostraBotaoPesquisa != value)
	    	          {
	    	              this.ValidateProperty("MostraBotaoPesquisa", value);
	    	              this.OnMostraBotaoPesquisaChanging(value);
	    	              this.RaiseDataMemberChanging("MostraBotaoPesquisa");
	    	              this._MostraBotaoPesquisa = value;
	    	              this.RaiseDataMemberChanged("MostraBotaoPesquisa");
	    	              this.OnMostraBotaoPesquisaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For MostraBotaoPesquisaEsp
	    partial void OnMostraBotaoPesquisaEspChanging(Boolean value);
	    partial void OnMostraBotaoPesquisaEspChanged();

	    private Boolean _MostraBotaoPesquisaEsp;

	    [DataMember(IsRequired = true, Name = "MostraBotaoPesquisaEsp", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Pesquisa Especial", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[true];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_PESQUISA_ESP];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_PESQUISA_ESP")]
	    public Boolean MostraBotaoPesquisaEsp
	    {
	    	    get
	    	    {
	    	          return _MostraBotaoPesquisaEsp;
	    	    }
	    	    set
	    	    {
	    	          if (this._MostraBotaoPesquisaEsp != value)
	    	          {
	    	              this.ValidateProperty("MostraBotaoPesquisaEsp", value);
	    	              this.OnMostraBotaoPesquisaEspChanging(value);
	    	              this.RaiseDataMemberChanging("MostraBotaoPesquisaEsp");
	    	              this._MostraBotaoPesquisaEsp = value;
	    	              this.RaiseDataMemberChanged("MostraBotaoPesquisaEsp");
	    	              this.OnMostraBotaoPesquisaEspChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For PossuiToolbar
	    partial void OnPossuiToolbarChanging(Boolean value);
	    partial void OnPossuiToolbarChanged();

	    private Boolean _PossuiToolbar;

	    [DataMember(IsRequired = true, Name = "PossuiToolbar", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Barra de Ferramentas", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.POSSUI_TOOLBAR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.POSSUI_TOOLBAR")]
	    public Boolean PossuiToolbar
	    {
	    	    get
	    	    {
	    	          return _PossuiToolbar;
	    	    }
	    	    set
	    	    {
	    	          if (this._PossuiToolbar != value)
	    	          {
	    	              this.ValidateProperty("PossuiToolbar", value);
	    	              this.OnPossuiToolbarChanging(value);
	    	              this.RaiseDataMemberChanging("PossuiToolbar");
	    	              this._PossuiToolbar = value;
	    	              this.RaiseDataMemberChanged("PossuiToolbar");
	    	              this.OnPossuiToolbarChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For PossuiVisaoTabular
	    partial void OnPossuiVisaoTabularChanging(Boolean value);
	    partial void OnPossuiVisaoTabularChanged();

	    private Boolean _PossuiVisaoTabular;

	    [DataMember(IsRequired = true, Name = "PossuiVisaoTabular", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Seletor de Visões", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.POSSUI_VISAO_TABULAR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.POSSUI_VISAO_TABULAR")]
	    public Boolean PossuiVisaoTabular
	    {
	    	    get
	    	    {
	    	          return _PossuiVisaoTabular;
	    	    }
	    	    set
	    	    {
	    	          if (this._PossuiVisaoTabular != value)
	    	          {
	    	              this.ValidateProperty("PossuiVisaoTabular", value);
	    	              this.OnPossuiVisaoTabularChanging(value);
	    	              this.RaiseDataMemberChanging("PossuiVisaoTabular");
	    	              this._PossuiVisaoTabular = value;
	    	              this.RaiseDataMemberChanged("PossuiVisaoTabular");
	    	              this.OnPossuiVisaoTabularChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For PropriedadesDoDetalhe
	    partial void OnPropriedadesDoDetalheChanging(System.String value);
	    partial void OnPropriedadesDoDetalheChanged();

	    private System.String _PropriedadesDoDetalhe;

	    [DataMember(Name = "PropriedadesDoDetalhe", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Propriedades do Detalhe", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.PROPRIEDADES_DO_DETALHE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.PROPRIEDADES_DO_DETALHE")]
	    public System.String PropriedadesDoDetalhe
	    {
	    	    get
	    	    {
	    	          return _PropriedadesDoDetalhe;
	    	    }
	    	    set
	    	    {
	    	          if (this._PropriedadesDoDetalhe != value)
	    	          {
	    	              this.ValidateProperty("PropriedadesDoDetalhe", value);
	    	              this.OnPropriedadesDoDetalheChanging(value);
	    	              this.RaiseDataMemberChanging("PropriedadesDoDetalhe");
	    	              this._PropriedadesDoDetalhe = value;
	    	              this.RaiseDataMemberChanged("PropriedadesDoDetalhe");
	    	              this.OnPropriedadesDoDetalheChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For PropriedadesDoMestre
	    partial void OnPropriedadesDoMestreChanging(System.String value);
	    partial void OnPropriedadesDoMestreChanged();

	    private System.String _PropriedadesDoMestre;

	    [DataMember(Name = "PropriedadesDoMestre", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Propriedades do Mestre", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.PROPRIEDADES_DO_MESTRE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.PROPRIEDADES_DO_MESTRE")]
	    public System.String PropriedadesDoMestre
	    {
	    	    get
	    	    {
	    	          return _PropriedadesDoMestre;
	    	    }
	    	    set
	    	    {
	    	          if (this._PropriedadesDoMestre != value)
	    	          {
	    	              this.ValidateProperty("PropriedadesDoMestre", value);
	    	              this.OnPropriedadesDoMestreChanging(value);
	    	              this.RaiseDataMemberChanging("PropriedadesDoMestre");
	    	              this._PropriedadesDoMestre = value;
	    	              this.RaiseDataMemberChanged("PropriedadesDoMestre");
	    	              this.OnPropriedadesDoMestreChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UsaFiltrosDoBoPrincipal
	    partial void OnUsaFiltrosDoBoPrincipalChanging(Boolean value);
	    partial void OnUsaFiltrosDoBoPrincipalChanged();

	    private Boolean _UsaFiltrosDoBoPrincipal;

	    [DataMember(IsRequired = true, Name = "UsaFiltrosDoBoPrincipal", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usa Filtros do BO Principal", Description="", Order = 21, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.USA_FILTROS_DO_BO_PRINCIPAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.USA_FILTROS_DO_BO_PRINCIPAL")]
	    public Boolean UsaFiltrosDoBoPrincipal
	    {
	    	    get
	    	    {
	    	          return _UsaFiltrosDoBoPrincipal;
	    	    }
	    	    set
	    	    {
	    	          if (this._UsaFiltrosDoBoPrincipal != value)
	    	          {
	    	              this.ValidateProperty("UsaFiltrosDoBoPrincipal", value);
	    	              this.OnUsaFiltrosDoBoPrincipalChanging(value);
	    	              this.RaiseDataMemberChanging("UsaFiltrosDoBoPrincipal");
	    	              this._UsaFiltrosDoBoPrincipal = value;
	    	              this.RaiseDataMemberChanged("UsaFiltrosDoBoPrincipal");
	    	              this.OnUsaFiltrosDoBoPrincipalChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Visivel
	    partial void OnVisivelChanging(Boolean value);
	    partial void OnVisivelChanged();

	    private Boolean _Visivel;

	    [DataMember(IsRequired = true, Name = "Visivel", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Visível", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[true];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.VISIVEL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.VISIVEL")]
	    public Boolean Visivel
	    {
	    	    get
	    	    {
	    	          return _Visivel;
	    	    }
	    	    set
	    	    {
	    	          if (this._Visivel != value)
	    	          {
	    	              this.ValidateProperty("Visivel", value);
	    	              this.OnVisivelChanging(value);
	    	              this.RaiseDataMemberChanging("Visivel");
	    	              this._Visivel = value;
	    	              this.RaiseDataMemberChanged("Visivel");
	    	              this.OnVisivelChanged();
	    	          }
	    	    }
	    }

	    private Int64 _TemporaryIdTransacaoDependente;
	    [DataMember(Name = "TemporaryIdTransacaoDependente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Transacao Dependente (Tmp)", Description="Temporary Key", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdTransacaoDependente
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTransacaoDependente.IsNullOrEmpty())
	    	                this._TemporaryIdTransacaoDependente = this._IdTransacaoDependente;
	    	          return this._TemporaryIdTransacaoDependente;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTransacaoDependente != value)
	    	              this._TemporaryIdTransacaoDependente = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsTransacaoAutorizacao _TcsTransacaoAutorizacao;
	    [DataMember(Name = "TcsTransacaoAutorizacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsTransacaoAutorizacao_TcsTransacaoDependenteAutorizacao", "IdTransacao", "IdTransacao", IsForeignKey=true)]
	    public TcsTransacaoAutorizacao TcsTransacaoAutorizacao
	    {
	        get
	        {
	            return this._TcsTransacaoAutorizacao;
	        }
	        set
	        {
	            if (this._TcsTransacaoAutorizacao != value)
	            {
	                this._TcsTransacaoAutorizacao = value;
	                this.RaisePropertyChanged("TcsTransacaoAutorizacaoList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO), QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.VISIVEL", Source = "Visivel", Target = "VISIVEL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.LX_TIPO_LAYOUT", Source = "LxTipoLayout", Target = "LX_TIPO_LAYOUT", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.POSSUI_TOOLBAR", Source = "PossuiToolbar", Target = "POSSUI_TOOLBAR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.EXECUTA_PESQUISA", Source = "ExecutaPesquisa", Target = "EXECUTA_PESQUISA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_LIMPA", Source = "MostraBotaoLimpa", Target = "MOSTRA_BOTAO_LIMPA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_ADICAO", Source = "MostraBotaoAdicao", Target = "MOSTRA_BOTAO_ADICAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_EDICAO", Source = "MostraBotaoEdicao", Target = "MOSTRA_BOTAO_EDICAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_LAYOUT", Source = "MostraBotaoLayout", Target = "MOSTRA_BOTAO_LAYOUT", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.POSSUI_VISAO_TABULAR", Source = "PossuiVisaoTabular", Target = "POSSUI_VISAO_TABULAR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_EXCLUSAO", Source = "MostraBotaoExclusao", Target = "MOSTRA_BOTAO_EXCLUSAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_PESQUISA", Source = "MostraBotaoPesquisa", Target = "MOSTRA_BOTAO_PESQUISA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_IMPRESSAO", Source = "MostraBotaoImpressao", Target = "MOSTRA_BOTAO_IMPRESSAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_NAVEGACAO", Source = "MostraBotaoNavegacao", Target = "MOSTRA_BOTAO_NAVEGACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.PROPRIEDADES_DO_MESTRE", Source = "PropriedadesDoMestre", Target = "PROPRIEDADES_DO_MESTRE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.ID_TRANSACAO_DEPENDENTE", Source = "IdTransacaoDependente", Target = "ID_TRANSACAO_DEPENDENTE", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.LX_POSICAO_DA_TRANSACAO", Source = "LxPosicaoDaTransacao", Target = "LX_POSICAO_DA_TRANSACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.PROPRIEDADES_DO_DETALHE", Source = "PropriedadesDoDetalhe", Target = "PROPRIEDADES_DO_DETALHE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.COMPARTILHA_BO_PRINCIPAL", Source = "CompartilhaBoPrincipal", Target = "COMPARTILHA_BO_PRINCIPAL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_PESQUISA_ESP", Source = "MostraBotaoPesquisaEsp", Target = "MOSTRA_BOTAO_PESQUISA_ESP", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.USA_FILTROS_DO_BO_PRINCIPAL", Source = "UsaFiltrosDoBoPrincipal", Target = "USA_FILTROS_DO_BO_PRINCIPAL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.TRANSACAO_RELACIONADA.ID_TRANSACAO", Source = "IdTransacaoRelacionada", Target = "ID_TRANSACAO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_AUTORIZACAO", RelationPropertyName = "TRANSACAO_RELACIONADA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.ID_TRANSACAO", Source = "IdTransacao", Target = "ID_TRANSACAO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_AUTORIZACAO" });

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
	             Linx.Framework.BV.BusinessMediaHelper.SyncMedia("TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", this.IdTransacaoDependente, null, this.TableMedia.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(e => new Guid(e)).ToList());
	         }
	         else if (operation == DomainOperation.Delete) {
	             Linx.Framework.BV.BusinessMediaHelper.SyncMedia("TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", this.IdTransacaoDependente, null, new List<Guid>() { Guid.Empty });
	         }
	    }

	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxPosicaoDaTransacaoValues()
	    {
	    	    return Linx.Framework.BV.Domains.PosicaoDaTransacao.GetValues();
	    }
	    private string _lxPosicaoDaTransacaoName;
	    [DataMember(IsRequired = false, Name = "LxPosicaoDaTransacaoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Posição", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxPosicaoDaTransacaoName
	    {
	    	    get { if (this.LxPosicaoDaTransacao.IsNull()) { _lxPosicaoDaTransacaoName = String.Empty; } else { string key = this.LxPosicaoDaTransacao.ToString(); var dmValues = this.GetLxPosicaoDaTransacaoValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxPosicaoDaTransacaoName) _lxPosicaoDaTransacaoName = domainName; } return _lxPosicaoDaTransacaoName; } set { _lxPosicaoDaTransacaoName = value;  }
	    }
	    public Dictionary<string, string> GetLxTipoLayoutValues()
	    {
	    	    return Linx.Framework.BV.Domains.TipoLayoutDependente.GetValues();
	    }
	    private string _lxTipoLayoutName;
	    [DataMember(IsRequired = false, Name = "LxTipoLayoutName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo do Layout", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
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

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Menu];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#.TCS_TRANSACAO_MENU_AUTORIZACAO_LISTA as #Alias#];EdmEntityName[TCS_TRANSACAO_MENU_AUTORIZACAO];EntityRelations[TCS_MODULO_MENU_AUTORIZACAO(TCS_MODULO_MENU_AUTORIZACAO)#TCS_MODULO_AUTORIZACAO(TCS_MODULO_AUTORIZACAO)#TCS_APLICATIVO(TCS_APLICATIVO)#MODULO_MENU_SUPERIOR(TCS_MODULO_MENU_AUTORIZACAO)#TCS_TRANSACAO_AUTORIZACAO(TCS_TRANSACAO_AUTORIZACAO)#TCS_OBJETO_AUTORIZACAO(TCS_OBJETO_AUTORIZACAO)];EdmParentEntityName[TCS_TRANSACAO_AUTORIZACAO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsTransacaoMenuAutorizacao")]
	[Serializable()]
	public partial class TcsTransacaoMenuAutorizacaoParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For ClasseNome
	    partial void OnClasseNomeChanging(System.String value);
	    partial void OnClasseNomeChanged();

	    private System.String _ClasseNome;

	    [DataMember(Name = "ClasseNome", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Formulário / Url", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [LinxStringLength(400)]
	    [FunctionalPoint("Precision[400:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.CLASSE_NOME];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.CLASSE_NOME")]
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
	    partial void OnCodTransacaoChanging(string value);
	    partial void OnCodTransacaoChanged();

	    private string _CodTransacao;

	    [DataMember(Name = "CodTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Código", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.COD_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.COD_TRANSACAO")]
	    public string CodTransacao
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
	    //Extensibility Partial Method Definitions For DescModulo
	    partial void OnDescModuloChanging(string value);
	    partial void OnDescModuloChanged();

	    private string _DescModulo;

	    [DataMember(IsRequired = true, Name = "DescModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Módulo Base", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsModuloMenuAutorizacao];LookUpTitle[Seleção de (Módulo Base)];LookUpQuery[executeLookUpTcsModuloMenuAutorizacao];LookUpFinalize[finalizeLookUpTcsModuloMenuAutorizacao];LookUpDisplayColumns[{\"DescricaoAplicativo\" : \"Aplicativo\", \"DescModulo\" : \"Módulo\", \"DescModuloMenu\" : \"Menu\", \"IdModulo\" : \"Id Modulo\", \"InativoModulo\" : \"Inativo\", \"IdModuloMenu\" : \"Id Modulo Menu\"}];LookUpColumns[{\"DescricaoAplicativo\" : true, \"DescModulo\" : true, \"DescModuloMenu\" : true, \"IdModulo\" : false, \"InativoModulo\" : true, \"IdModuloMenu\" : false}];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.DESC_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#DescModulo#false##100:0##Módulo#1#true##::LookUpTcsModuloMenuAutorizacao##true#false#TCS_MODULO_MENU_AUTORIZACAO#TCS_MODULO_MENU_AUTORIZACAO#Linx.Framework.BV.TransacaoAutorizacao#IQueryable###true#true", EdmKey="TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.DESC_MODULO")]
	    public string DescModulo
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
	    //Extensibility Partial Method Definitions For DescModuloMenu
	    partial void OnDescModuloMenuChanging(string value);
	    partial void OnDescModuloMenuChanged();

	    private string _DescModuloMenu;

	    [DataMember(IsRequired = true, Name = "DescModuloMenu", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Menu", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsModuloMenuAutorizacao];LookUpTitle[Seleção de (Menu)];LookUpQuery[executeLookUpTcsModuloMenuAutorizacao];LookUpFinalize[finalizeLookUpTcsModuloMenuAutorizacao];LookUpDisplayColumns[{\"DescricaoAplicativo\" : \"Aplicativo\", \"DescModulo\" : \"Módulo\", \"DescModuloMenu\" : \"Menu\", \"IdModulo\" : \"Id Modulo\", \"InativoModulo\" : \"Inativo\", \"IdModuloMenu\" : \"Id Modulo Menu\"}];LookUpColumns[{\"DescricaoAplicativo\" : true, \"DescModulo\" : true, \"DescModuloMenu\" : true, \"IdModulo\" : false, \"InativoModulo\" : true, \"IdModuloMenu\" : false}];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_MODULO_MENU_AUTORIZACAO.DESC_MODULO_MENU];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescModuloMenu#false##1000##Menu#2#true##::LookUpTcsModuloMenuAutorizacao##true#false#TCS_MODULO_MENU_AUTORIZACAO#TCS_MODULO_MENU_AUTORIZACAO#Linx.Framework.BV.TransacaoAutorizacao#IQueryable###true#true", EdmKey="TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_MODULO_MENU_AUTORIZACAO.DESC_MODULO_MENU")]
	    public string DescModuloMenu
	    {
	    	    get
	    	    {
	    	          return _DescModuloMenu;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescModuloMenu != value)
	    	          {
	    	              this.ValidateProperty("DescModuloMenu", value);
	    	              this.OnDescModuloMenuChanging(value);
	    	              this.RaiseDataMemberChanging("DescModuloMenu");
	    	              this._DescModuloMenu = value;
	    	              this.RaiseDataMemberChanged("DescModuloMenu");
	    	              this.OnDescModuloMenuChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescricaoAplicativo
	    partial void OnDescricaoAplicativoChanging(string value);
	    partial void OnDescricaoAplicativoChanged();

	    private string _DescricaoAplicativo;

	    [DataMember(Name = "DescricaoAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicativo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsModuloMenuAutorizacao];LookUpTitle[Seleção de (Aplicativo)];LookUpQuery[executeLookUpTcsModuloMenuAutorizacao];LookUpFinalize[finalizeLookUpTcsModuloMenuAutorizacao];LookUpDisplayColumns[{\"DescricaoAplicativo\" : \"Aplicativo\", \"DescModulo\" : \"Módulo\", \"DescModuloMenu\" : \"Menu\", \"IdModulo\" : \"Id Modulo\", \"InativoModulo\" : \"Inativo\", \"IdModuloMenu\" : \"Id Modulo Menu\"}];LookUpColumns[{\"DescricaoAplicativo\" : true, \"DescModulo\" : true, \"DescModuloMenu\" : true, \"IdModulo\" : false, \"InativoModulo\" : true, \"IdModuloMenu\" : false}];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#DescricaoAplicativo#false##250:0##Aplicativo#0#true##::LookUpTcsModuloMenuAutorizacao##true#false#TCS_MODULO_MENU_AUTORIZACAO#TCS_MODULO_MENU_AUTORIZACAO#Linx.Framework.BV.TransacaoAutorizacao#IQueryable###true#true", EdmKey="TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO")]
	    public string DescricaoAplicativo
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
	    //Extensibility Partial Method Definitions For DescTransacao
	    partial void OnDescTransacaoChanging(System.String value);
	    partial void OnDescTransacaoChanged();

	    private System.String _DescTransacao;

	    [DataMember(Name = "DescTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição Detalhada", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.DESC_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.DESC_TRANSACAO")]
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
	    //Extensibility Partial Method Definitions For IdModulo
	    partial void OnIdModuloChanging(long value);
	    partial void OnIdModuloChanged();

	    private long _IdModulo;

	    [DataMember(IsRequired = true, Name = "IdModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsModuloMenuAutorizacao];LookUpTitle[Seleção de (Id Modulo)];LookUpQuery[executeLookUpTcsModuloMenuAutorizacao];LookUpFinalize[finalizeLookUpTcsModuloMenuAutorizacao];LookUpDisplayColumns[{\"DescricaoAplicativo\" : \"Aplicativo\", \"DescModulo\" : \"Módulo\", \"DescModuloMenu\" : \"Menu\", \"IdModulo\" : \"Id Modulo\", \"InativoModulo\" : \"Inativo\", \"IdModuloMenu\" : \"Id Modulo Menu\"}];LookUpColumns[{\"DescricaoAplicativo\" : true, \"DescModulo\" : true, \"DescModuloMenu\" : true, \"IdModulo\" : false, \"InativoModulo\" : true, \"IdModuloMenu\" : false}];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.ID_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="long#IdModulo#true##0:0##Id Modulo#4#false##::LookUpTcsModuloMenuAutorizacao##true#false#TCS_MODULO_MENU_AUTORIZACAO#TCS_MODULO_MENU_AUTORIZACAO#Linx.Framework.BV.TransacaoAutorizacao#IQueryable###true#true", EdmKey="TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.ID_MODULO")]
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
	    //Extensibility Partial Method Definitions For IdModuloMenu
	    partial void OnIdModuloMenuChanging(long value);
	    partial void OnIdModuloMenuChanged();

	    private long _IdModuloMenu;

	    [DataMember(IsRequired = true, Name = "IdModuloMenu", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo Menu", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsModuloMenuAutorizacao];LookUpTitle[Seleção de (Id Modulo Menu)];LookUpQuery[executeLookUpTcsModuloMenuAutorizacao];LookUpFinalize[finalizeLookUpTcsModuloMenuAutorizacao];LookUpDisplayColumns[{\"DescricaoAplicativo\" : \"Aplicativo\", \"DescModulo\" : \"Módulo\", \"DescModuloMenu\" : \"Menu\", \"IdModulo\" : \"Id Modulo\", \"InativoModulo\" : \"Inativo\", \"IdModuloMenu\" : \"Id Modulo Menu\"}];LookUpColumns[{\"DescricaoAplicativo\" : true, \"DescModulo\" : true, \"DescModuloMenu\" : true, \"IdModulo\" : false, \"InativoModulo\" : true, \"IdModuloMenu\" : false}];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_MODULO_MENU_AUTORIZACAO.ID_MODULO_MENU];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="long#IdModuloMenu#true##0:0##Id Modulo Menu#6#false##::LookUpTcsModuloMenuAutorizacao##true#false#TCS_MODULO_MENU_AUTORIZACAO#TCS_MODULO_MENU_AUTORIZACAO#Linx.Framework.BV.TransacaoAutorizacao#IQueryable###true#true", EdmKey="TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_MODULO_MENU_AUTORIZACAO.ID_MODULO_MENU")]
	    public long IdModuloMenu
	    {
	    	    get
	    	    {
	    	          return _IdModuloMenu;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdModuloMenu != value)
	    	          {
	    	              this.ValidateProperty("IdModuloMenu", value);
	    	              this.OnIdModuloMenuChanging(value);
	    	              this.RaiseDataMemberChanging("IdModuloMenu");
	    	              this._IdModuloMenu = value;
	    	              this.RaiseDataMemberChanged("IdModuloMenu");
	    	              this.OnIdModuloMenuChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsTransacaoMenuAutorizacao
	    partial void OnIdTcsTransacaoMenuAutorizacaoChanging(int value);
	    partial void OnIdTcsTransacaoMenuAutorizacaoChanged();

	    private int _IdTcsTransacaoMenuAutorizacao;

	    [DataMember(IsRequired = true, Name = "IdTcsTransacaoMenuAutorizacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Transacao Menu Autorizacao", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.ID_TCS_TRANSACAO_MENU_AUTORIZACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_MENU_AUTORIZACAO.ID_TCS_TRANSACAO_MENU_AUTORIZACAO")]
	    public int IdTcsTransacaoMenuAutorizacao
	    {
	    	    get
	    	    {
	    	          return _IdTcsTransacaoMenuAutorizacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsTransacaoMenuAutorizacao != value)
	    	          {
	    	              this.ValidateProperty("IdTcsTransacaoMenuAutorizacao", value);
	    	              this.OnIdTcsTransacaoMenuAutorizacaoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsTransacaoMenuAutorizacao");
	    	              this._IdTcsTransacaoMenuAutorizacao = value;
	    	              this.RaiseDataMemberChanged("IdTcsTransacaoMenuAutorizacao");
	    	              this.OnIdTcsTransacaoMenuAutorizacaoChanged();
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
	    [Display(Name = "Id Transacao", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.ID_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.ID_TRANSACAO")]
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
	    //Extensibility Partial Method Definitions For Inativo
	    partial void OnInativoChanging(bool value);
	    partial void OnInativoChanged();

	    private bool _Inativo;

	    [DataMember(IsRequired = true, Name = "Inativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inativo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_MENU_AUTORIZACAO.INATIVO")]
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
	    //Extensibility Partial Method Definitions For InativoModulo
	    partial void OnInativoModuloChanging(bool value);
	    partial void OnInativoModuloChanged();

	    private bool _InativoModulo;

	    [DataMember(IsRequired = true, Name = "InativoModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inativo", Description="", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsModuloMenuAutorizacao];LookUpTitle[Seleção de (Inativo)];LookUpQuery[executeLookUpTcsModuloMenuAutorizacao];LookUpFinalize[finalizeLookUpTcsModuloMenuAutorizacao];LookUpDisplayColumns[{\"DescricaoAplicativo\" : \"Aplicativo\", \"DescModulo\" : \"Módulo\", \"DescModuloMenu\" : \"Menu\", \"IdModulo\" : \"Id Modulo\", \"InativoModulo\" : \"Inativo\", \"IdModuloMenu\" : \"Id Modulo Menu\"}];LookUpColumns[{\"DescricaoAplicativo\" : true, \"DescModulo\" : true, \"DescModuloMenu\" : true, \"IdModulo\" : false, \"InativoModulo\" : true, \"IdModuloMenu\" : false}];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="bool#InativoModulo#false##0:0##Inativo#5#true##::LookUpTcsModuloMenuAutorizacao##true#false#TCS_MODULO_MENU_AUTORIZACAO#TCS_MODULO_MENU_AUTORIZACAO#Linx.Framework.BV.TransacaoAutorizacao#IQueryable###true#true", EdmKey="TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.INATIVO")]
	    public bool InativoModulo
	    {
	    	    get
	    	    {
	    	          return _InativoModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._InativoModulo != value)
	    	          {
	    	              this.ValidateProperty("InativoModulo", value);
	    	              this.OnInativoModuloChanging(value);
	    	              this.RaiseDataMemberChanging("InativoModulo");
	    	              this._InativoModulo = value;
	    	              this.RaiseDataMemberChanged("InativoModulo");
	    	              this.OnInativoModuloChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For OrdemNavegacao
	    partial void OnOrdemNavegacaoChanging(byte value);
	    partial void OnOrdemNavegacaoChanged();

	    private byte _OrdemNavegacao;

	    [DataMember(IsRequired = true, Name = "OrdemNavegacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ordem", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.ORDEM_NAVEGACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_MENU_AUTORIZACAO.ORDEM_NAVEGACAO")]
	    public byte OrdemNavegacao
	    {
	    	    get
	    	    {
	    	          return _OrdemNavegacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._OrdemNavegacao != value)
	    	          {
	    	              this.ValidateProperty("OrdemNavegacao", value);
	    	              this.OnOrdemNavegacaoChanging(value);
	    	              this.RaiseDataMemberChanging("OrdemNavegacao");
	    	              this._OrdemNavegacao = value;
	    	              this.RaiseDataMemberChanged("OrdemNavegacao");
	    	              this.OnOrdemNavegacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescObjeto
	    partial void OnDescObjetoChanging(string value);
	    partial void OnDescObjetoChanged();

	    private string _DescObjeto;

	    [DataMember(IsRequired = true, Name = "DescObjeto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Classe BO", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(400)]
	    [FunctionalPoint("Precision[400:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.DESC_OBJETO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.DESC_OBJETO")]
	    public string DescObjeto
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
	    //Extensibility Partial Method Definitions For Icone
	    partial void OnIconeChanging(string value);
	    partial void OnIconeChanged();

	    private string _Icone;

	    [DataMember(Name = "Icone", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ícone", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.ICONE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.ICONE")]
	    public string Icone
	    {
	    	    get
	    	    {
	    	          return _Icone;
	    	    }
	    	    set
	    	    {
	    	          if (this._Icone != value)
	    	          {
	    	              this.ValidateProperty("Icone", value);
	    	              this.OnIconeChanging(value);
	    	              this.RaiseDataMemberChanging("Icone");
	    	              this._Icone = value;
	    	              this.RaiseDataMemberChanged("Icone");
	    	              this.OnIconeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdObjeto
	    partial void OnIdObjetoChanging(long value);
	    partial void OnIdObjetoChanged();

	    private long _IdObjeto;

	    [DataMember(IsRequired = true, Name = "IdObjeto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Objeto", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.ID_OBJETO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.ID_OBJETO")]
	    public long IdObjeto
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
	    //Extensibility Partial Method Definitions For LxCorFundo
	    partial void OnLxCorFundoChanging(System.Nullable<int> value);
	    partial void OnLxCorFundoChanged();

	    private System.Nullable<int> _LxCorFundo;

	    [DataMember(Name = "LxCorFundo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cor de Fundo", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[CorFundo];KpiName[];KpiRelatedAttribute[];DefaultValue[7];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.LX_COR_FUNDO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.LX_COR_FUNDO")]
	    public System.Nullable<int> LxCorFundo
	    {
	    	    get
	    	    {
	    	          return _LxCorFundo;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxCorFundo != value)
	    	          {
	    	              this.ValidateProperty("LxCorFundo", value);
	    	              this.OnLxCorFundoChanging(value);
	    	              this.RaiseDataMemberChanging("LxCorFundo");
	    	              this._LxCorFundo = value;
	    	              this.RaiseDataMemberChanged("LxCorFundo");
	    	              this.OnLxCorFundoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoTransacao
	    partial void OnLxTipoTransacaoChanging(byte value);
	    partial void OnLxTipoTransacaoChanged();

	    private byte _LxTipoTransacao;

	    [DataMember(IsRequired = true, Name = "LxTipoTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo Transação", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoTransacao];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.LX_TIPO_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.LX_TIPO_TRANSACAO")]
	    public byte LxTipoTransacao
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
	    //Extensibility Partial Method Definitions For NomeCurto
	    partial void OnNomeCurtoChanging(string value);
	    partial void OnNomeCurtoChanged();

	    private string _NomeCurto;

	    [DataMember(IsRequired = true, Name = "NomeCurto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.NOME_CURTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.NOME_CURTO")]
	    public string NomeCurto
	    {
	    	    get
	    	    {
	    	          return _NomeCurto;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeCurto != value)
	    	          {
	    	              this.ValidateProperty("NomeCurto", value);
	    	              this.OnNomeCurtoChanging(value);
	    	              this.RaiseDataMemberChanging("NomeCurto");
	    	              this._NomeCurto = value;
	    	              this.RaiseDataMemberChanged("NomeCurto");
	    	              this.OnNomeCurtoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ObjetoClasseNome
	    partial void OnObjetoClasseNomeChanging(string value);
	    partial void OnObjetoClasseNomeChanged();

	    private string _ObjetoClasseNome;

	    [DataMember(IsRequired = true, Name = "ObjetoClasseNome", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Classe Nome", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.CLASSE_NOME];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.CLASSE_NOME")]
	    public string ObjetoClasseNome
	    {
	    	    get
	    	    {
	    	          return _ObjetoClasseNome;
	    	    }
	    	    set
	    	    {
	    	          if (this._ObjetoClasseNome != value)
	    	          {
	    	              this.ValidateProperty("ObjetoClasseNome", value);
	    	              this.OnObjetoClasseNomeChanging(value);
	    	              this.RaiseDataMemberChanging("ObjetoClasseNome");
	    	              this._ObjetoClasseNome = value;
	    	              this.RaiseDataMemberChanged("ObjetoClasseNome");
	    	              this.OnObjetoClasseNomeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Tag
	    partial void OnTagChanging(string value);
	    partial void OnTagChanged();

	    private string _Tag;

	    [DataMember(Name = "Tag", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tag", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(4000)]
	    [FunctionalPoint("Precision[4000:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.TAG];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.TAG")]
	    public string Tag
	    {
	    	    get
	    	    {
	    	          return _Tag;
	    	    }
	    	    set
	    	    {
	    	          if (this._Tag != value)
	    	          {
	    	              this.ValidateProperty("Tag", value);
	    	              this.OnTagChanging(value);
	    	              this.RaiseDataMemberChanging("Tag");
	    	              this._Tag = value;
	    	              this.RaiseDataMemberChanged("Tag");
	    	              this.OnTagChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_TRANSACAO_MENU_AUTORIZACAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_TRANSACAO_MENU_AUTORIZACAO), QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_MENU_AUTORIZACAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_MENU_AUTORIZACAO.INATIVO", Source = "Inativo", Target = "INATIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_MENU_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_MENU_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_MENU_AUTORIZACAO.ORDEM_NAVEGACAO", Source = "OrdemNavegacao", Target = "ORDEM_NAVEGACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_MENU_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_MENU_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_MENU_AUTORIZACAO.ID_TCS_TRANSACAO_MENU_AUTORIZACAO", Source = "IdTcsTransacaoMenuAutorizacao", Target = "ID_TCS_TRANSACAO_MENU_AUTORIZACAO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_MENU_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_MENU_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.ID_TRANSACAO", Source = "IdTransacao", Target = "ID_TRANSACAO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_MODULO_MENU_AUTORIZACAO.ID_MODULO_MENU", Source = "IdModuloMenu", Target = "ID_MODULO_MENU", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_MODULO_MENU_AUTORIZACAO", RelationPropertyName = "TCS_MODULO_MENU_AUTORIZACAO" });

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
	             Linx.Framework.BV.BusinessMediaHelper.SyncMedia("TCS_TRANSACAO_MENU_AUTORIZACAO", this.IdTcsTransacaoMenuAutorizacao, null, this.TableMedia.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(e => new Guid(e)).ToList());
	         }
	         else if (operation == DomainOperation.Delete) {
	             Linx.Framework.BV.BusinessMediaHelper.SyncMedia("TCS_TRANSACAO_MENU_AUTORIZACAO", this.IdTcsTransacaoMenuAutorizacao, null, new List<Guid>() { Guid.Empty });
	         }
	    }

	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxCorFundoValues()
	    {
	    	    return Linx.Framework.BV.Domains.CorFundo.GetValues();
	    }
	    private string _lxCorFundoName;
	    [DataMember(IsRequired = false, Name = "LxCorFundoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Cor de Fundo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxCorFundoName
	    {
	    	    get { if (this.LxCorFundo.IsNull()) { _lxCorFundoName = String.Empty; } else { string key = this.LxCorFundo.ToString(); var dmValues = this.GetLxCorFundoValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxCorFundoName) _lxCorFundoName = domainName; } return _lxCorFundoName; } set { _lxCorFundoName = value;  }
	    }
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

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Transações Dependentes];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTransacaoDependente];ReadOnly[false];Entities[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO:IdTransacaoDependente];SubQueryInfo[Select 1 From #ParentAlias#.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO_LISTA as #Alias#];EdmEntityName[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO];EntityRelations[TCS_TRANSACAO_AUTORIZACAO(TCS_TRANSACAO_AUTORIZACAO)#TCS_MODULO_AUTORIZACAO(TCS_MODULO_AUTORIZACAO)#TCS_APLICATIVO(TCS_APLICATIVO)#TCS_OBJETO_AUTORIZACAO(TCS_OBJETO_AUTORIZACAO)#TRANSACAO_RELACIONADA(TCS_TRANSACAO_AUTORIZACAO)];EdmParentEntityName[TCS_TRANSACAO_AUTORIZACAO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsTransacaoDependenteAutorizacao")]
	[Serializable()]
	public partial class TcsTransacaoDependenteAutorizacaoParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For ClasseNome
	    partial void OnClasseNomeChanging(System.String value);
	    partial void OnClasseNomeChanged();

	    private System.String _ClasseNome;

	    [DataMember(IsRequired = true, Name = "ClasseNome", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Formulário / Url", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(400)]
	    [FunctionalPoint("Precision[400:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsTransacaoDependente];LookUpTitle[Seleção de (Formulário / Url)];LookUpQuery[executeLookUpTcsTransacaoDependente];LookUpFinalize[finalizeLookUpTcsTransacaoDependente];LookUpDisplayColumns[{\"IdTransacao\" : \"\", \"DescTransacao\" : \"Transação\", \"ClasseNome\" : \"Formulário\"}];LookUpColumns[{\"IdTransacao\" : false, \"DescTransacao\" : true, \"ClasseNome\" : true}];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.TRANSACAO_RELACIONADA.CLASSE_NOME];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#ClasseNome#false##40##Formulário#2#true##::LookUpTcsTransacaoDependente##false#false###Linx.Framework.BV.TransacaoAutorizacao#IQueryable###true#false", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.TRANSACAO_RELACIONADA.CLASSE_NOME")]
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
	    //Extensibility Partial Method Definitions For CompartilhaBoPrincipal
	    partial void OnCompartilhaBoPrincipalChanging(Boolean value);
	    partial void OnCompartilhaBoPrincipalChanged();

	    private Boolean _CompartilhaBoPrincipal;

	    [DataMember(IsRequired = true, Name = "CompartilhaBoPrincipal", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Compartilha BO Principal", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.COMPARTILHA_BO_PRINCIPAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.COMPARTILHA_BO_PRINCIPAL")]
	    public Boolean CompartilhaBoPrincipal
	    {
	    	    get
	    	    {
	    	          return _CompartilhaBoPrincipal;
	    	    }
	    	    set
	    	    {
	    	          if (this._CompartilhaBoPrincipal != value)
	    	          {
	    	              this.ValidateProperty("CompartilhaBoPrincipal", value);
	    	              this.OnCompartilhaBoPrincipalChanging(value);
	    	              this.RaiseDataMemberChanging("CompartilhaBoPrincipal");
	    	              this._CompartilhaBoPrincipal = value;
	    	              this.RaiseDataMemberChanged("CompartilhaBoPrincipal");
	    	              this.OnCompartilhaBoPrincipalChanged();
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
	    [Display(Name = "Descrição Detalhada", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsTransacaoDependente];LookUpTitle[Seleção de (Descrição Detalhada)];LookUpQuery[executeLookUpTcsTransacaoDependente];LookUpFinalize[finalizeLookUpTcsTransacaoDependente];LookUpDisplayColumns[{\"IdTransacao\" : \"\", \"DescTransacao\" : \"Transação\", \"ClasseNome\" : \"Formulário\"}];LookUpColumns[{\"IdTransacao\" : false, \"DescTransacao\" : true, \"ClasseNome\" : true}];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.TRANSACAO_RELACIONADA.DESC_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#DescTransacao#false##60##Transação#1#true##::LookUpTcsTransacaoDependente##false#false###Linx.Framework.BV.TransacaoAutorizacao#IQueryable###true#false", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.TRANSACAO_RELACIONADA.DESC_TRANSACAO")]
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
	    //Extensibility Partial Method Definitions For ExecutaPesquisa
	    partial void OnExecutaPesquisaChanging(Boolean value);
	    partial void OnExecutaPesquisaChanged();

	    private Boolean _ExecutaPesquisa;

	    [DataMember(IsRequired = true, Name = "ExecutaPesquisa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Sempre Executa Pesquisa", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.EXECUTA_PESQUISA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.EXECUTA_PESQUISA")]
	    public Boolean ExecutaPesquisa
	    {
	    	    get
	    	    {
	    	          return _ExecutaPesquisa;
	    	    }
	    	    set
	    	    {
	    	          if (this._ExecutaPesquisa != value)
	    	          {
	    	              this.ValidateProperty("ExecutaPesquisa", value);
	    	              this.OnExecutaPesquisaChanging(value);
	    	              this.RaiseDataMemberChanging("ExecutaPesquisa");
	    	              this._ExecutaPesquisa = value;
	    	              this.RaiseDataMemberChanged("ExecutaPesquisa");
	    	              this.OnExecutaPesquisaChanged();
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
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.ID_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.ID_TRANSACAO")]
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
	    //Extensibility Partial Method Definitions For IdTransacaoRelacionada
	    partial void OnIdTransacaoRelacionadaChanging(Int64 value);
	    partial void OnIdTransacaoRelacionadaChanged();

	    private Int64 _IdTransacaoRelacionada;

	    [DataMember(IsRequired = true, Name = "IdTransacaoRelacionada", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Transacao1", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsTransacaoDependente];LookUpTitle[Seleção de (Id Transacao1)];LookUpQuery[executeLookUpTcsTransacaoDependente];LookUpFinalize[finalizeLookUpTcsTransacaoDependente];LookUpDisplayColumns[{\"IdTransacao\" : \"\", \"DescTransacao\" : \"Transação\", \"ClasseNome\" : \"Formulário\"}];LookUpColumns[{\"IdTransacao\" : false, \"DescTransacao\" : true, \"ClasseNome\" : true}];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.TRANSACAO_RELACIONADA.ID_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdTransacao#true##12###0#false##::LookUpTcsTransacaoDependente##false#false###Linx.Framework.BV.TransacaoAutorizacao#IQueryable###true#false", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.TRANSACAO_RELACIONADA.ID_TRANSACAO")]
	    public Int64 IdTransacaoRelacionada
	    {
	    	    get
	    	    {
	    	          return _IdTransacaoRelacionada;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTransacaoRelacionada != value)
	    	          {
	    	              this.ValidateProperty("IdTransacaoRelacionada", value);
	    	              this.OnIdTransacaoRelacionadaChanging(value);
	    	              this.RaiseDataMemberChanging("IdTransacaoRelacionada");
	    	              this._IdTransacaoRelacionada = value;
	    	              this.RaiseDataMemberChanged("IdTransacaoRelacionada");
	    	              this.OnIdTransacaoRelacionadaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTransacaoDependente
	    partial void OnIdTransacaoDependenteChanging(Int64 value);
	    partial void OnIdTransacaoDependenteChanged();

	    private Int64 _IdTransacaoDependente;

	    [DataMember(IsRequired = true, Name = "IdTransacaoDependente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Transacao Dependente", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.ID_TRANSACAO_DEPENDENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.ID_TRANSACAO_DEPENDENTE")]
	    public Int64 IdTransacaoDependente
	    {
	    	    get
	    	    {
	    	          return _IdTransacaoDependente;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTransacaoDependente != value)
	    	          {
	    	              this.ValidateProperty("IdTransacaoDependente", value);
	    	              this.OnIdTransacaoDependenteChanging(value);
	    	              this.RaiseDataMemberChanging("IdTransacaoDependente");
	    	              this._IdTransacaoDependente = value;
	    	              this.RaiseDataMemberChanged("IdTransacaoDependente");
	    	              this.OnIdTransacaoDependenteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxPosicaoDaTransacao
	    partial void OnLxPosicaoDaTransacaoChanging(Byte value);
	    partial void OnLxPosicaoDaTransacaoChanged();

	    private Byte _LxPosicaoDaTransacao;

	    [DataMember(IsRequired = true, Name = "LxPosicaoDaTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Posição", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[PosicaoDaTransacao];KpiName[];KpiRelatedAttribute[];DefaultValue[1];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.LX_POSICAO_DA_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.LX_POSICAO_DA_TRANSACAO")]
	    public Byte LxPosicaoDaTransacao
	    {
	    	    get
	    	    {
	    	          return _LxPosicaoDaTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxPosicaoDaTransacao != value)
	    	          {
	    	              this.ValidateProperty("LxPosicaoDaTransacao", value);
	    	              this.OnLxPosicaoDaTransacaoChanging(value);
	    	              this.RaiseDataMemberChanging("LxPosicaoDaTransacao");
	    	              this._LxPosicaoDaTransacao = value;
	    	              this.RaiseDataMemberChanged("LxPosicaoDaTransacao");
	    	              this.OnLxPosicaoDaTransacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoLayout
	    partial void OnLxTipoLayoutChanging(Byte value);
	    partial void OnLxTipoLayoutChanged();

	    private Byte _LxTipoLayout;

	    [DataMember(IsRequired = true, Name = "LxTipoLayout", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo do Layout", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoLayoutDependente];KpiName[];KpiRelatedAttribute[];DefaultValue[7];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.LX_TIPO_LAYOUT];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.LX_TIPO_LAYOUT")]
	    public Byte LxTipoLayout
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
	    //Extensibility Partial Method Definitions For MostraBotaoAdicao
	    partial void OnMostraBotaoAdicaoChanging(Boolean value);
	    partial void OnMostraBotaoAdicaoChanged();

	    private Boolean _MostraBotaoAdicao;

	    [DataMember(IsRequired = true, Name = "MostraBotaoAdicao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Adição", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[true];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_ADICAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_ADICAO")]
	    public Boolean MostraBotaoAdicao
	    {
	    	    get
	    	    {
	    	          return _MostraBotaoAdicao;
	    	    }
	    	    set
	    	    {
	    	          if (this._MostraBotaoAdicao != value)
	    	          {
	    	              this.ValidateProperty("MostraBotaoAdicao", value);
	    	              this.OnMostraBotaoAdicaoChanging(value);
	    	              this.RaiseDataMemberChanging("MostraBotaoAdicao");
	    	              this._MostraBotaoAdicao = value;
	    	              this.RaiseDataMemberChanged("MostraBotaoAdicao");
	    	              this.OnMostraBotaoAdicaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For MostraBotaoEdicao
	    partial void OnMostraBotaoEdicaoChanging(Boolean value);
	    partial void OnMostraBotaoEdicaoChanged();

	    private Boolean _MostraBotaoEdicao;

	    [DataMember(IsRequired = true, Name = "MostraBotaoEdicao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Edição", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[true];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_EDICAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_EDICAO")]
	    public Boolean MostraBotaoEdicao
	    {
	    	    get
	    	    {
	    	          return _MostraBotaoEdicao;
	    	    }
	    	    set
	    	    {
	    	          if (this._MostraBotaoEdicao != value)
	    	          {
	    	              this.ValidateProperty("MostraBotaoEdicao", value);
	    	              this.OnMostraBotaoEdicaoChanging(value);
	    	              this.RaiseDataMemberChanging("MostraBotaoEdicao");
	    	              this._MostraBotaoEdicao = value;
	    	              this.RaiseDataMemberChanged("MostraBotaoEdicao");
	    	              this.OnMostraBotaoEdicaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For MostraBotaoExclusao
	    partial void OnMostraBotaoExclusaoChanging(Boolean value);
	    partial void OnMostraBotaoExclusaoChanged();

	    private Boolean _MostraBotaoExclusao;

	    [DataMember(IsRequired = true, Name = "MostraBotaoExclusao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Exclusão", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[true];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_EXCLUSAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_EXCLUSAO")]
	    public Boolean MostraBotaoExclusao
	    {
	    	    get
	    	    {
	    	          return _MostraBotaoExclusao;
	    	    }
	    	    set
	    	    {
	    	          if (this._MostraBotaoExclusao != value)
	    	          {
	    	              this.ValidateProperty("MostraBotaoExclusao", value);
	    	              this.OnMostraBotaoExclusaoChanging(value);
	    	              this.RaiseDataMemberChanging("MostraBotaoExclusao");
	    	              this._MostraBotaoExclusao = value;
	    	              this.RaiseDataMemberChanged("MostraBotaoExclusao");
	    	              this.OnMostraBotaoExclusaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For MostraBotaoImpressao
	    partial void OnMostraBotaoImpressaoChanging(Boolean value);
	    partial void OnMostraBotaoImpressaoChanged();

	    private Boolean _MostraBotaoImpressao;

	    [DataMember(IsRequired = true, Name = "MostraBotaoImpressao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Impressão", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[true];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_IMPRESSAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_IMPRESSAO")]
	    public Boolean MostraBotaoImpressao
	    {
	    	    get
	    	    {
	    	          return _MostraBotaoImpressao;
	    	    }
	    	    set
	    	    {
	    	          if (this._MostraBotaoImpressao != value)
	    	          {
	    	              this.ValidateProperty("MostraBotaoImpressao", value);
	    	              this.OnMostraBotaoImpressaoChanging(value);
	    	              this.RaiseDataMemberChanging("MostraBotaoImpressao");
	    	              this._MostraBotaoImpressao = value;
	    	              this.RaiseDataMemberChanged("MostraBotaoImpressao");
	    	              this.OnMostraBotaoImpressaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For MostraBotaoLayout
	    partial void OnMostraBotaoLayoutChanging(Boolean value);
	    partial void OnMostraBotaoLayoutChanged();

	    private Boolean _MostraBotaoLayout;

	    [DataMember(IsRequired = true, Name = "MostraBotaoLayout", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Layout", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[true];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_LAYOUT];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_LAYOUT")]
	    public Boolean MostraBotaoLayout
	    {
	    	    get
	    	    {
	    	          return _MostraBotaoLayout;
	    	    }
	    	    set
	    	    {
	    	          if (this._MostraBotaoLayout != value)
	    	          {
	    	              this.ValidateProperty("MostraBotaoLayout", value);
	    	              this.OnMostraBotaoLayoutChanging(value);
	    	              this.RaiseDataMemberChanging("MostraBotaoLayout");
	    	              this._MostraBotaoLayout = value;
	    	              this.RaiseDataMemberChanged("MostraBotaoLayout");
	    	              this.OnMostraBotaoLayoutChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For MostraBotaoLimpa
	    partial void OnMostraBotaoLimpaChanging(Boolean value);
	    partial void OnMostraBotaoLimpaChanged();

	    private Boolean _MostraBotaoLimpa;

	    [DataMember(IsRequired = true, Name = "MostraBotaoLimpa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Limpa", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[true];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_LIMPA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_LIMPA")]
	    public Boolean MostraBotaoLimpa
	    {
	    	    get
	    	    {
	    	          return _MostraBotaoLimpa;
	    	    }
	    	    set
	    	    {
	    	          if (this._MostraBotaoLimpa != value)
	    	          {
	    	              this.ValidateProperty("MostraBotaoLimpa", value);
	    	              this.OnMostraBotaoLimpaChanging(value);
	    	              this.RaiseDataMemberChanging("MostraBotaoLimpa");
	    	              this._MostraBotaoLimpa = value;
	    	              this.RaiseDataMemberChanged("MostraBotaoLimpa");
	    	              this.OnMostraBotaoLimpaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For MostraBotaoNavegacao
	    partial void OnMostraBotaoNavegacaoChanging(Boolean value);
	    partial void OnMostraBotaoNavegacaoChanged();

	    private Boolean _MostraBotaoNavegacao;

	    [DataMember(IsRequired = true, Name = "MostraBotaoNavegacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Navegação", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[true];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_NAVEGACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_NAVEGACAO")]
	    public Boolean MostraBotaoNavegacao
	    {
	    	    get
	    	    {
	    	          return _MostraBotaoNavegacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._MostraBotaoNavegacao != value)
	    	          {
	    	              this.ValidateProperty("MostraBotaoNavegacao", value);
	    	              this.OnMostraBotaoNavegacaoChanging(value);
	    	              this.RaiseDataMemberChanging("MostraBotaoNavegacao");
	    	              this._MostraBotaoNavegacao = value;
	    	              this.RaiseDataMemberChanged("MostraBotaoNavegacao");
	    	              this.OnMostraBotaoNavegacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For MostraBotaoPesquisa
	    partial void OnMostraBotaoPesquisaChanging(Boolean value);
	    partial void OnMostraBotaoPesquisaChanged();

	    private Boolean _MostraBotaoPesquisa;

	    [DataMember(IsRequired = true, Name = "MostraBotaoPesquisa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Pesquisa", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[true];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_PESQUISA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_PESQUISA")]
	    public Boolean MostraBotaoPesquisa
	    {
	    	    get
	    	    {
	    	          return _MostraBotaoPesquisa;
	    	    }
	    	    set
	    	    {
	    	          if (this._MostraBotaoPesquisa != value)
	    	          {
	    	              this.ValidateProperty("MostraBotaoPesquisa", value);
	    	              this.OnMostraBotaoPesquisaChanging(value);
	    	              this.RaiseDataMemberChanging("MostraBotaoPesquisa");
	    	              this._MostraBotaoPesquisa = value;
	    	              this.RaiseDataMemberChanged("MostraBotaoPesquisa");
	    	              this.OnMostraBotaoPesquisaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For MostraBotaoPesquisaEsp
	    partial void OnMostraBotaoPesquisaEspChanging(Boolean value);
	    partial void OnMostraBotaoPesquisaEspChanged();

	    private Boolean _MostraBotaoPesquisaEsp;

	    [DataMember(IsRequired = true, Name = "MostraBotaoPesquisaEsp", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Pesquisa Especial", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[true];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_PESQUISA_ESP];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_PESQUISA_ESP")]
	    public Boolean MostraBotaoPesquisaEsp
	    {
	    	    get
	    	    {
	    	          return _MostraBotaoPesquisaEsp;
	    	    }
	    	    set
	    	    {
	    	          if (this._MostraBotaoPesquisaEsp != value)
	    	          {
	    	              this.ValidateProperty("MostraBotaoPesquisaEsp", value);
	    	              this.OnMostraBotaoPesquisaEspChanging(value);
	    	              this.RaiseDataMemberChanging("MostraBotaoPesquisaEsp");
	    	              this._MostraBotaoPesquisaEsp = value;
	    	              this.RaiseDataMemberChanged("MostraBotaoPesquisaEsp");
	    	              this.OnMostraBotaoPesquisaEspChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For PossuiToolbar
	    partial void OnPossuiToolbarChanging(Boolean value);
	    partial void OnPossuiToolbarChanged();

	    private Boolean _PossuiToolbar;

	    [DataMember(IsRequired = true, Name = "PossuiToolbar", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Barra de Ferramentas", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.POSSUI_TOOLBAR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.POSSUI_TOOLBAR")]
	    public Boolean PossuiToolbar
	    {
	    	    get
	    	    {
	    	          return _PossuiToolbar;
	    	    }
	    	    set
	    	    {
	    	          if (this._PossuiToolbar != value)
	    	          {
	    	              this.ValidateProperty("PossuiToolbar", value);
	    	              this.OnPossuiToolbarChanging(value);
	    	              this.RaiseDataMemberChanging("PossuiToolbar");
	    	              this._PossuiToolbar = value;
	    	              this.RaiseDataMemberChanged("PossuiToolbar");
	    	              this.OnPossuiToolbarChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For PossuiVisaoTabular
	    partial void OnPossuiVisaoTabularChanging(Boolean value);
	    partial void OnPossuiVisaoTabularChanged();

	    private Boolean _PossuiVisaoTabular;

	    [DataMember(IsRequired = true, Name = "PossuiVisaoTabular", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Seletor de Visões", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.POSSUI_VISAO_TABULAR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.POSSUI_VISAO_TABULAR")]
	    public Boolean PossuiVisaoTabular
	    {
	    	    get
	    	    {
	    	          return _PossuiVisaoTabular;
	    	    }
	    	    set
	    	    {
	    	          if (this._PossuiVisaoTabular != value)
	    	          {
	    	              this.ValidateProperty("PossuiVisaoTabular", value);
	    	              this.OnPossuiVisaoTabularChanging(value);
	    	              this.RaiseDataMemberChanging("PossuiVisaoTabular");
	    	              this._PossuiVisaoTabular = value;
	    	              this.RaiseDataMemberChanged("PossuiVisaoTabular");
	    	              this.OnPossuiVisaoTabularChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For PropriedadesDoDetalhe
	    partial void OnPropriedadesDoDetalheChanging(System.String value);
	    partial void OnPropriedadesDoDetalheChanged();

	    private System.String _PropriedadesDoDetalhe;

	    [DataMember(Name = "PropriedadesDoDetalhe", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Propriedades do Detalhe", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.PROPRIEDADES_DO_DETALHE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.PROPRIEDADES_DO_DETALHE")]
	    public System.String PropriedadesDoDetalhe
	    {
	    	    get
	    	    {
	    	          return _PropriedadesDoDetalhe;
	    	    }
	    	    set
	    	    {
	    	          if (this._PropriedadesDoDetalhe != value)
	    	          {
	    	              this.ValidateProperty("PropriedadesDoDetalhe", value);
	    	              this.OnPropriedadesDoDetalheChanging(value);
	    	              this.RaiseDataMemberChanging("PropriedadesDoDetalhe");
	    	              this._PropriedadesDoDetalhe = value;
	    	              this.RaiseDataMemberChanged("PropriedadesDoDetalhe");
	    	              this.OnPropriedadesDoDetalheChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For PropriedadesDoMestre
	    partial void OnPropriedadesDoMestreChanging(System.String value);
	    partial void OnPropriedadesDoMestreChanged();

	    private System.String _PropriedadesDoMestre;

	    [DataMember(Name = "PropriedadesDoMestre", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Propriedades do Mestre", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.PROPRIEDADES_DO_MESTRE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.PROPRIEDADES_DO_MESTRE")]
	    public System.String PropriedadesDoMestre
	    {
	    	    get
	    	    {
	    	          return _PropriedadesDoMestre;
	    	    }
	    	    set
	    	    {
	    	          if (this._PropriedadesDoMestre != value)
	    	          {
	    	              this.ValidateProperty("PropriedadesDoMestre", value);
	    	              this.OnPropriedadesDoMestreChanging(value);
	    	              this.RaiseDataMemberChanging("PropriedadesDoMestre");
	    	              this._PropriedadesDoMestre = value;
	    	              this.RaiseDataMemberChanged("PropriedadesDoMestre");
	    	              this.OnPropriedadesDoMestreChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UsaFiltrosDoBoPrincipal
	    partial void OnUsaFiltrosDoBoPrincipalChanging(Boolean value);
	    partial void OnUsaFiltrosDoBoPrincipalChanged();

	    private Boolean _UsaFiltrosDoBoPrincipal;

	    [DataMember(IsRequired = true, Name = "UsaFiltrosDoBoPrincipal", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usa Filtros do BO Principal", Description="", Order = 21, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.USA_FILTROS_DO_BO_PRINCIPAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.USA_FILTROS_DO_BO_PRINCIPAL")]
	    public Boolean UsaFiltrosDoBoPrincipal
	    {
	    	    get
	    	    {
	    	          return _UsaFiltrosDoBoPrincipal;
	    	    }
	    	    set
	    	    {
	    	          if (this._UsaFiltrosDoBoPrincipal != value)
	    	          {
	    	              this.ValidateProperty("UsaFiltrosDoBoPrincipal", value);
	    	              this.OnUsaFiltrosDoBoPrincipalChanging(value);
	    	              this.RaiseDataMemberChanging("UsaFiltrosDoBoPrincipal");
	    	              this._UsaFiltrosDoBoPrincipal = value;
	    	              this.RaiseDataMemberChanged("UsaFiltrosDoBoPrincipal");
	    	              this.OnUsaFiltrosDoBoPrincipalChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Visivel
	    partial void OnVisivelChanging(Boolean value);
	    partial void OnVisivelChanged();

	    private Boolean _Visivel;

	    [DataMember(IsRequired = true, Name = "Visivel", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Visível", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[true];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.VISIVEL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.VISIVEL")]
	    public Boolean Visivel
	    {
	    	    get
	    	    {
	    	          return _Visivel;
	    	    }
	    	    set
	    	    {
	    	          if (this._Visivel != value)
	    	          {
	    	              this.ValidateProperty("Visivel", value);
	    	              this.OnVisivelChanging(value);
	    	              this.RaiseDataMemberChanging("Visivel");
	    	              this._Visivel = value;
	    	              this.RaiseDataMemberChanged("Visivel");
	    	              this.OnVisivelChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For CodTransacao
	    partial void OnCodTransacaoChanging(string value);
	    partial void OnCodTransacaoChanged();

	    private string _CodTransacao;

	    [DataMember(IsRequired = true, Name = "CodTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Código", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.COD_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.COD_TRANSACAO")]
	    public string CodTransacao
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
	    //Extensibility Partial Method Definitions For DescModulo
	    partial void OnDescModuloChanging(string value);
	    partial void OnDescModuloChanged();

	    private string _DescModulo;

	    [DataMember(Name = "DescModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Módulo Base", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.DESC_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.DESC_MODULO")]
	    public string DescModulo
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
	    //Extensibility Partial Method Definitions For DescObjeto
	    partial void OnDescObjetoChanging(string value);
	    partial void OnDescObjetoChanged();

	    private string _DescObjeto;

	    [DataMember(IsRequired = true, Name = "DescObjeto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Classe BO", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(400)]
	    [FunctionalPoint("Precision[400:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.DESC_OBJETO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.DESC_OBJETO")]
	    public string DescObjeto
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
	    //Extensibility Partial Method Definitions For Icone
	    partial void OnIconeChanging(string value);
	    partial void OnIconeChanged();

	    private string _Icone;

	    [DataMember(Name = "Icone", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ícone", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.ICONE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.ICONE")]
	    public string Icone
	    {
	    	    get
	    	    {
	    	          return _Icone;
	    	    }
	    	    set
	    	    {
	    	          if (this._Icone != value)
	    	          {
	    	              this.ValidateProperty("Icone", value);
	    	              this.OnIconeChanging(value);
	    	              this.RaiseDataMemberChanging("Icone");
	    	              this._Icone = value;
	    	              this.RaiseDataMemberChanged("Icone");
	    	              this.OnIconeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdModulo
	    partial void OnIdModuloChanging(System.Nullable<long> value);
	    partial void OnIdModuloChanged();

	    private System.Nullable<long> _IdModulo;

	    [DataMember(Name = "IdModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.ID_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.ID_MODULO")]
	    public System.Nullable<long> IdModulo
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
	    //Extensibility Partial Method Definitions For IdObjeto
	    partial void OnIdObjetoChanging(long value);
	    partial void OnIdObjetoChanged();

	    private long _IdObjeto;

	    [DataMember(IsRequired = true, Name = "IdObjeto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Objeto", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.ID_OBJETO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.ID_OBJETO")]
	    public long IdObjeto
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
	    //Extensibility Partial Method Definitions For Inativo
	    partial void OnInativoChanging(bool value);
	    partial void OnInativoChanged();

	    private bool _Inativo;

	    [DataMember(IsRequired = true, Name = "Inativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inativo", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.INATIVO")]
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
	    //Extensibility Partial Method Definitions For LxCorFundo
	    partial void OnLxCorFundoChanging(System.Nullable<int> value);
	    partial void OnLxCorFundoChanged();

	    private System.Nullable<int> _LxCorFundo;

	    [DataMember(Name = "LxCorFundo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cor de Fundo", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[CorFundo];KpiName[];KpiRelatedAttribute[];DefaultValue[7];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.LX_COR_FUNDO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.LX_COR_FUNDO")]
	    public System.Nullable<int> LxCorFundo
	    {
	    	    get
	    	    {
	    	          return _LxCorFundo;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxCorFundo != value)
	    	          {
	    	              this.ValidateProperty("LxCorFundo", value);
	    	              this.OnLxCorFundoChanging(value);
	    	              this.RaiseDataMemberChanging("LxCorFundo");
	    	              this._LxCorFundo = value;
	    	              this.RaiseDataMemberChanged("LxCorFundo");
	    	              this.OnLxCorFundoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoTransacao
	    partial void OnLxTipoTransacaoChanging(byte value);
	    partial void OnLxTipoTransacaoChanged();

	    private byte _LxTipoTransacao;

	    [DataMember(IsRequired = true, Name = "LxTipoTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo Transação", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoTransacao];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.LX_TIPO_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.LX_TIPO_TRANSACAO")]
	    public byte LxTipoTransacao
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
	    //Extensibility Partial Method Definitions For NomeCurto
	    partial void OnNomeCurtoChanging(string value);
	    partial void OnNomeCurtoChanged();

	    private string _NomeCurto;

	    [DataMember(IsRequired = true, Name = "NomeCurto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.NOME_CURTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.NOME_CURTO")]
	    public string NomeCurto
	    {
	    	    get
	    	    {
	    	          return _NomeCurto;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeCurto != value)
	    	          {
	    	              this.ValidateProperty("NomeCurto", value);
	    	              this.OnNomeCurtoChanging(value);
	    	              this.RaiseDataMemberChanging("NomeCurto");
	    	              this._NomeCurto = value;
	    	              this.RaiseDataMemberChanged("NomeCurto");
	    	              this.OnNomeCurtoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ObjetoClasseNome
	    partial void OnObjetoClasseNomeChanging(string value);
	    partial void OnObjetoClasseNomeChanged();

	    private string _ObjetoClasseNome;

	    [DataMember(IsRequired = true, Name = "ObjetoClasseNome", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Classe Nome", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.CLASSE_NOME];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.CLASSE_NOME")]
	    public string ObjetoClasseNome
	    {
	    	    get
	    	    {
	    	          return _ObjetoClasseNome;
	    	    }
	    	    set
	    	    {
	    	          if (this._ObjetoClasseNome != value)
	    	          {
	    	              this.ValidateProperty("ObjetoClasseNome", value);
	    	              this.OnObjetoClasseNomeChanging(value);
	    	              this.RaiseDataMemberChanging("ObjetoClasseNome");
	    	              this._ObjetoClasseNome = value;
	    	              this.RaiseDataMemberChanged("ObjetoClasseNome");
	    	              this.OnObjetoClasseNomeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Tag
	    partial void OnTagChanging(string value);
	    partial void OnTagChanged();

	    private string _Tag;

	    [DataMember(Name = "Tag", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tag", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(4000)]
	    [FunctionalPoint("Precision[4000:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.TAG];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_AUTORIZACAO.TAG")]
	    public string Tag
	    {
	    	    get
	    	    {
	    	          return _Tag;
	    	    }
	    	    set
	    	    {
	    	          if (this._Tag != value)
	    	          {
	    	              this.ValidateProperty("Tag", value);
	    	              this.OnTagChanging(value);
	    	              this.RaiseDataMemberChanging("Tag");
	    	              this._Tag = value;
	    	              this.RaiseDataMemberChanged("Tag");
	    	              this.OnTagChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO), QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.VISIVEL", Source = "Visivel", Target = "VISIVEL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.LX_TIPO_LAYOUT", Source = "LxTipoLayout", Target = "LX_TIPO_LAYOUT", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.POSSUI_TOOLBAR", Source = "PossuiToolbar", Target = "POSSUI_TOOLBAR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.EXECUTA_PESQUISA", Source = "ExecutaPesquisa", Target = "EXECUTA_PESQUISA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_LIMPA", Source = "MostraBotaoLimpa", Target = "MOSTRA_BOTAO_LIMPA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_ADICAO", Source = "MostraBotaoAdicao", Target = "MOSTRA_BOTAO_ADICAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_EDICAO", Source = "MostraBotaoEdicao", Target = "MOSTRA_BOTAO_EDICAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_LAYOUT", Source = "MostraBotaoLayout", Target = "MOSTRA_BOTAO_LAYOUT", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.POSSUI_VISAO_TABULAR", Source = "PossuiVisaoTabular", Target = "POSSUI_VISAO_TABULAR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_EXCLUSAO", Source = "MostraBotaoExclusao", Target = "MOSTRA_BOTAO_EXCLUSAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_PESQUISA", Source = "MostraBotaoPesquisa", Target = "MOSTRA_BOTAO_PESQUISA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_IMPRESSAO", Source = "MostraBotaoImpressao", Target = "MOSTRA_BOTAO_IMPRESSAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_NAVEGACAO", Source = "MostraBotaoNavegacao", Target = "MOSTRA_BOTAO_NAVEGACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.PROPRIEDADES_DO_MESTRE", Source = "PropriedadesDoMestre", Target = "PROPRIEDADES_DO_MESTRE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.ID_TRANSACAO_DEPENDENTE", Source = "IdTransacaoDependente", Target = "ID_TRANSACAO_DEPENDENTE", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.LX_POSICAO_DA_TRANSACAO", Source = "LxPosicaoDaTransacao", Target = "LX_POSICAO_DA_TRANSACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.PROPRIEDADES_DO_DETALHE", Source = "PropriedadesDoDetalhe", Target = "PROPRIEDADES_DO_DETALHE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.COMPARTILHA_BO_PRINCIPAL", Source = "CompartilhaBoPrincipal", Target = "COMPARTILHA_BO_PRINCIPAL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_PESQUISA_ESP", Source = "MostraBotaoPesquisaEsp", Target = "MOSTRA_BOTAO_PESQUISA_ESP", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.USA_FILTROS_DO_BO_PRINCIPAL", Source = "UsaFiltrosDoBoPrincipal", Target = "USA_FILTROS_DO_BO_PRINCIPAL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.TRANSACAO_RELACIONADA.ID_TRANSACAO", Source = "IdTransacaoRelacionada", Target = "ID_TRANSACAO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_AUTORIZACAO", RelationPropertyName = "TRANSACAO_RELACIONADA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.ID_TRANSACAO", Source = "IdTransacao", Target = "ID_TRANSACAO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_TRANSACAO_AUTORIZACAO", RelationPropertyName = "TCS_TRANSACAO_AUTORIZACAO" });

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
	             Linx.Framework.BV.BusinessMediaHelper.SyncMedia("TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", this.IdTransacaoDependente, null, this.TableMedia.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(e => new Guid(e)).ToList());
	         }
	         else if (operation == DomainOperation.Delete) {
	             Linx.Framework.BV.BusinessMediaHelper.SyncMedia("TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", this.IdTransacaoDependente, null, new List<Guid>() { Guid.Empty });
	         }
	    }

	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxPosicaoDaTransacaoValues()
	    {
	    	    return Linx.Framework.BV.Domains.PosicaoDaTransacao.GetValues();
	    }
	    private string _lxPosicaoDaTransacaoName;
	    [DataMember(IsRequired = false, Name = "LxPosicaoDaTransacaoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Posição", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxPosicaoDaTransacaoName
	    {
	    	    get { if (this.LxPosicaoDaTransacao.IsNull()) { _lxPosicaoDaTransacaoName = String.Empty; } else { string key = this.LxPosicaoDaTransacao.ToString(); var dmValues = this.GetLxPosicaoDaTransacaoValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxPosicaoDaTransacaoName) _lxPosicaoDaTransacaoName = domainName; } return _lxPosicaoDaTransacaoName; } set { _lxPosicaoDaTransacaoName = value;  }
	    }
	    public Dictionary<string, string> GetLxTipoLayoutValues()
	    {
	    	    return Linx.Framework.BV.Domains.TipoLayoutDependente.GetValues();
	    }
	    private string _lxTipoLayoutName;
	    [DataMember(IsRequired = false, Name = "LxTipoLayoutName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo do Layout", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxTipoLayoutName
	    {
	    	    get { if (this.LxTipoLayout.IsNull()) { _lxTipoLayoutName = String.Empty; } else { string key = this.LxTipoLayout.ToString(); var dmValues = this.GetLxTipoLayoutValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxTipoLayoutName) _lxTipoLayoutName = domainName; } return _lxTipoLayoutName; } set { _lxTipoLayoutName = value;  }
	    }
	    public Dictionary<string, string> GetLxCorFundoValues()
	    {
	    	    return Linx.Framework.BV.Domains.CorFundo.GetValues();
	    }
	    private string _lxCorFundoName;
	    [DataMember(IsRequired = false, Name = "LxCorFundoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Cor de Fundo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxCorFundoName
	    {
	    	    get { if (this.LxCorFundo.IsNull()) { _lxCorFundoName = String.Empty; } else { string key = this.LxCorFundo.ToString(); var dmValues = this.GetLxCorFundoValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxCorFundoName) _lxCorFundoName = domainName; } return _lxCorFundoName; } set { _lxCorFundoName = value;  }
	    }
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
	//////////////////////// DomainService Class V1 ///////////////////////
	///////////////////////////////////////////////////////////////////////
	[EnableClientAccess()]	
	[DomainIdentifier("ProcessorOverviewTransacaoAutorizacaoDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class TransacaoAutorizacaoDomainService : DomainService, IDataServiceContext 
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

		
	    public TransacaoAutorizacaoDomainService() : this("", null, null) { }
	    public TransacaoAutorizacaoDomainService(string connectionString) : this(connectionString, null, null) { }
	    public TransacaoAutorizacaoDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public TransacaoAutorizacaoDomainService(Linx.Framework.Autorizacao.BM.AutorizacaoContext dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public TransacaoAutorizacaoDomainService(string connectionString, Linx.Framework.Autorizacao.BM.AutorizacaoContext dataContext, Dictionary<string, string> headers) : base() 
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
	    			if (entry.Entity is TcsTransacaoAutorizacao) ((TcsTransacaoAutorizacao)entry.Entity).SaveMedia(entry.Operation);
	    			if (entry.Entity is TcsTransacaoMenuAutorizacao) ((TcsTransacaoMenuAutorizacao)entry.Entity).SaveMedia(entry.Operation);
	    			if (entry.Entity is TcsTransacaoDependenteAutorizacao) ((TcsTransacaoDependenteAutorizacao)entry.Entity).SaveMedia(entry.Operation);
	    		}
	    }

	    private void OnSavedChanges(ChangeSet changeSet)
	    {
	
	
	        TcsTransacaoAutorizacao.OnSavedContextChanges(this, changeSet.ChangeSetEntries.Where(e => e.Entity is TcsTransacaoAutorizacao).ToArray());
    	
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
 	        var _TcsTransacaoAutorizacaoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsTransacaoAutorizacao && e.Entity.GetType().Name == "TcsTransacaoAutorizacao" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _TcsTransacaoAutorizacaoElements)
 	           if (((TcsTransacaoAutorizacao)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 
 	        //Remove inconsistent details
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsTransacaoMenuAutorizacao && e.Entity.GetType().Name == "TcsTransacaoMenuAutorizacao" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsTransacaoDependenteAutorizacao && e.Entity.GetType().Name == "TcsTransacaoDependenteAutorizacao" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
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
	    //Get All LookUpTcsObjetoAutorizacao.
	    public IQueryable<LookUpTcsObjetoAutorizacao> GetAllLookUpTcsObjetoAutorizacao()
	    {
	        return this.GetLookUpTcsObjetoAutorizacao(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsObjetoAutorizacao By EntitySearch.
	    public IQueryable<LookUpTcsObjetoAutorizacao> GetLookUpTcsObjetoAutorizacaoByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsObjetoAutorizacao(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsObjetoAutorizacao.
	    public IQueryable<LookUpTcsObjetoAutorizacao> GetLookUpTcsObjetoAutorizacao(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TCS_OBJETO_AUTORIZACAO" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsObjetoAutorizacao";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsObjetoAutorizacao));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsObjetoAutorizacao> query =  
	
	            (from entity in this.DbContext.TCS_OBJETO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpTcsObjetoAutorizacao()		
	            {
	            
                DescObjeto = entity.DESC_OBJETO
                , ObjetoClasseNome = entity.CLASSE_NOME
                , IdObjeto = entity.ID_OBJETO
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
	            
	            select new LookUpTcsModuloAutorizacao()		
	            {
	            
                DescModulo = entity.DESC_MODULO
                , IdModulo = entity.ID_MODULO
                , DescricaoAplicativo = entity.TCS_APLICATIVO.DESCRICAO_APLICATIVO
	            });

	            
            //Inner Group Definition
            if (propertyName.InList("DescricaoAplicativo"))
            {
               query = (from r in query select new LookUpTcsModuloAutorizacao() {
               DescModulo = ""
               , IdModulo = default(System.Nullable<long>)
               , DescricaoAplicativo = r.DescricaoAplicativo
                }).Distinct();
            }
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpTcsModuloMenuAutorizacao.
	    public IQueryable<LookUpTcsModuloMenuAutorizacao> GetAllLookUpTcsModuloMenuAutorizacao()
	    {
	        return this.GetLookUpTcsModuloMenuAutorizacao(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsModuloMenuAutorizacao By EntitySearch.
	    public IQueryable<LookUpTcsModuloMenuAutorizacao> GetLookUpTcsModuloMenuAutorizacaoByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsModuloMenuAutorizacao(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsModuloMenuAutorizacao.
	    public IQueryable<LookUpTcsModuloMenuAutorizacao> GetLookUpTcsModuloMenuAutorizacao(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TCS_MODULO_MENU_AUTORIZACAO" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsModuloMenuAutorizacao";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsModuloMenuAutorizacao));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsModuloMenuAutorizacao> query =  
	
	            (from entity in this.DbContext.TCS_MODULO_MENU_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entityAl2 = entity.TCS_MODULO_AUTORIZACAO
                  let entityAl1 = entity.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO
	            
	            select new LookUpTcsModuloMenuAutorizacao()		
	            {
	            
                DescricaoAplicativo = entityAl1.DESCRICAO_APLICATIVO
                , DescModulo = entityAl2.DESC_MODULO
                , DescModuloMenu = entity.DESC_MODULO_MENU
                , DescModuloMenuSuperior = entity.MODULO_MENU_SUPERIOR.DESC_MODULO_MENU
                , IdModulo = entityAl2.ID_MODULO
                , InativoModulo = entityAl2.INATIVO
                , IdModuloMenu = entity.ID_MODULO_MENU
	            });

	            
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpTcsTransacaoDependente.
	    public IQueryable<LookUpTcsTransacaoDependente> GetAllLookUpTcsTransacaoDependente()
	    {
	        return this.GetLookUpTcsTransacaoDependente(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsTransacaoDependente By EntitySearch.
	    public IQueryable<LookUpTcsTransacaoDependente> GetLookUpTcsTransacaoDependenteByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsTransacaoDependente(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsTransacaoDependente.
	    public IQueryable<LookUpTcsTransacaoDependente> GetLookUpTcsTransacaoDependente(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsTransacaoDependente";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsTransacaoDependente));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsTransacaoDependente> query =  null;
		
			
		
	        TcsTransacaoDependenteAutorizacao.OnLookUpingLookUpTcsTransacaoDependente(ref query, propertyName, entitySearch);
	
	
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
	
		

	        if (entityName.InList("Linx.Framework.BV.TransacaoAutorizacao.TcsTransacaoAutorizacao"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsTransacaoAutorizacao",
	        			NameSpace = "Linx.Framework.BV.TransacaoAutorizacao",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsTransacaoAutorizacao",
	        			ClearMethodName = "ClearTcsTransacaoAutorizacao",
	        			QueryMethodName  = "GetPagedTcsTransacaoAutorizacao",	
	        			CountingMethodName  = "GetTcsTransacaoAutorizacao" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.TransacaoAutorizacao.TcsTransacaoAutorizacao"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.TransacaoAutorizacao.TcsTransacaoAutorizacao"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.TransacaoAutorizacao.TcsTransacaoAutorizacao", "Linx.Framework.BV.TransacaoAutorizacao.TcsTransacaoMenuAutorizacao"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsTransacaoMenuAutorizacao" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.TransacaoAutorizacao",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsTransacaoAutorizacao",	
	        			DisplayName = "Menu",
	        			ClearMethodName = "ClearTcsTransacaoMenuAutorizacao" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsTransacaoMenuAutorizacao" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsTransacaoMenuAutorizacao" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.TransacaoAutorizacao.TcsTransacaoMenuAutorizacao"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.TransacaoAutorizacao.TcsTransacaoMenuAutorizacao" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.TransacaoAutorizacao.TcsTransacaoAutorizacao", "Linx.Framework.BV.TransacaoAutorizacao.TcsTransacaoDependenteAutorizacao"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsTransacaoDependenteAutorizacao" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.TransacaoAutorizacao",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsTransacaoAutorizacao",	
	        			DisplayName = "Transações Dependentes",
	        			ClearMethodName = "ClearTcsTransacaoDependenteAutorizacao" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsTransacaoDependenteAutorizacao" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsTransacaoDependenteAutorizacao" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.TransacaoAutorizacao.TcsTransacaoDependenteAutorizacao"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.TransacaoAutorizacao.TcsTransacaoDependenteAutorizacao" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
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

         		    return new string[] { "Framework_TransacaoAutorizacaoClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.TransacaoAutorizacaoClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Framework_transacaoAutorizacaoService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.transacaoAutorizacaoService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear TcsTransacaoAutorizacao.
	    public IEnumerable<TcsTransacaoAutorizacao> ClearTcsTransacaoAutorizacao()
	    {
	        List<TcsTransacaoAutorizacao> result = new List<TcsTransacaoAutorizacao>();
	        result.Add(new TcsTransacaoAutorizacao(false));	
			
	        result[0].TcsTransacaoMenuAutorizacaoList = new List<TcsTransacaoMenuAutorizacao>();
	        ((List<TcsTransacaoMenuAutorizacao>)result[0].TcsTransacaoMenuAutorizacaoList).Add(new TcsTransacaoMenuAutorizacao());
			
	        result[0].TcsTransacaoDependenteAutorizacaoList = new List<TcsTransacaoDependenteAutorizacao>();
	        ((List<TcsTransacaoDependenteAutorizacao>)result[0].TcsTransacaoDependenteAutorizacaoList).Add(new TcsTransacaoDependenteAutorizacao(false));
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsTransacaoMenuAutorizacao.
	    public IEnumerable<TcsTransacaoMenuAutorizacao> ClearTcsTransacaoMenuAutorizacao()
	    {
	        List<TcsTransacaoMenuAutorizacao> result = new List<TcsTransacaoMenuAutorizacao>();
	        result.Add(new TcsTransacaoMenuAutorizacao());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsTransacaoDependenteAutorizacao.
	    public IEnumerable<TcsTransacaoDependenteAutorizacao> ClearTcsTransacaoDependenteAutorizacao()
	    {
	        List<TcsTransacaoDependenteAutorizacao> result = new List<TcsTransacaoDependenteAutorizacao>();
	        result.Add(new TcsTransacaoDependenteAutorizacao(false));	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsTransacaoAutorizacao.
	    public IQueryable<TcsTransacaoAutorizacao> GetTcsTransacaoAutorizacao()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_AUTORIZACAO
                  let entity0Al1 = entity0.TCS_MODULO_AUTORIZACAO
                  let entity0Al2 = entity0.TCS_OBJETO_AUTORIZACAO
	            
	            	
	            select new TcsTransacaoAutorizacao()		
	            {
	            
                ClasseNome = entity0.CLASSE_NOME
                , CodTransacao = entity0.COD_TRANSACAO
                , DescModulo = entity0Al1.DESC_MODULO
                , DescObjeto = entity0Al2.DESC_OBJETO
                , DescTransacao = entity0.DESC_TRANSACAO
                , Icone = entity0.ICONE
                , IdModulo = entity0Al1.ID_MODULO
                , IdObjeto = entity0Al2.ID_OBJETO
                , IdTransacao = entity0.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , LxCorFundoName = ((entity0.LX_COR_FUNDO) == 8 ? "Fundo Laranja" : ((entity0.LX_COR_FUNDO) == 10 ? "Fundo Roxo" : ((entity0.LX_COR_FUNDO) == 7 ? "Laranja" : ((entity0.LX_COR_FUNDO) == 9 ? "Roxo" : ""))))
                , LxTipoTransacao = entity0.LX_TIPO_TRANSACAO
                , LxTipoTransacaoName = ((entity0.LX_TIPO_TRANSACAO) == 7 ? "Assistente" : ((entity0.LX_TIPO_TRANSACAO) == 8 ? "Dashboard" : ((entity0.LX_TIPO_TRANSACAO) == 2 ? "ERP" : ((entity0.LX_TIPO_TRANSACAO) == 6 ? "ERP App" : ((entity0.LX_TIPO_TRANSACAO) == 4 ? "Excel" : ((entity0.LX_TIPO_TRANSACAO) == 3 ? "Loja" : ((entity0.LX_TIPO_TRANSACAO) == 5 ? "Mobile" : ((entity0.LX_TIPO_TRANSACAO) == 1 ? "Todos" : ""))))))))
                , NomeCurto = entity0.NOME_CURTO
                , ObjetoClasseNome = entity0Al2.CLASSE_NOME
                , Tag = entity0.TAG
			
                ,TcsTransacaoMenuAutorizacaoList = 
	                        (from entity1 in entity0.TCS_TRANSACAO_MENU_AUTORIZACAO_LISTA
                                  let entity1Al4 = entity1.TCS_TRANSACAO_AUTORIZACAO
                                  let entity1Al2 = entity1.TCS_MODULO_MENU_AUTORIZACAO
                                  let entity1Al1 = entity1.TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO
                                  let entity1Al3 = entity1.TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO
	                        
	                        	
	                        select new TcsTransacaoMenuAutorizacao()
	                        {
	                        
                                ClasseNome = entity1Al4.CLASSE_NOME
                                , CodTransacao = entity1Al4.COD_TRANSACAO
                                , DescModulo = entity1Al1.DESC_MODULO
                                , DescModuloMenu = entity1Al2.DESC_MODULO_MENU
                                , DescricaoAplicativo = entity1Al3.DESCRICAO_APLICATIVO
                                , DescTransacao = entity1Al4.DESC_TRANSACAO
                                , IdModulo = entity1Al1.ID_MODULO
                                , IdModuloMenu = entity1Al2.ID_MODULO_MENU
                                , IdTcsTransacaoMenuAutorizacao = entity1.ID_TCS_TRANSACAO_MENU_AUTORIZACAO
                                , IdTransacao = entity1Al4.ID_TRANSACAO
                                , Inativo = entity1.INATIVO
                                , InativoModulo = entity1Al1.INATIVO
                                , OrdemNavegacao = entity1.ORDEM_NAVEGACAO
		
	                        }
	                        )
			
                ,TcsTransacaoDependenteAutorizacaoList = 
	                        (from entity1 in entity0.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO_LISTA
                                  let entity1Al1 = entity1.TRANSACAO_RELACIONADA
                                  let entity1Al2 = entity1.TCS_TRANSACAO_AUTORIZACAO
	                        
	                        	
	                        select new TcsTransacaoDependenteAutorizacao()
	                        {
	                        
                                ClasseNome = entity1Al1.CLASSE_NOME
                                , CompartilhaBoPrincipal = entity1.COMPARTILHA_BO_PRINCIPAL
                                , DescTransacao = entity1Al1.DESC_TRANSACAO
                                , ExecutaPesquisa = entity1.EXECUTA_PESQUISA
                                , IdTransacao = entity1Al2.ID_TRANSACAO
                                , IdTransacaoRelacionada = entity1Al1.ID_TRANSACAO
                                , IdTransacaoDependente = entity1.ID_TRANSACAO_DEPENDENTE
                                , LxPosicaoDaTransacao = entity1.LX_POSICAO_DA_TRANSACAO
                                , LxPosicaoDaTransacaoName = ((entity1.LX_POSICAO_DA_TRANSACAO) == 5 ? "Painel Inferior" : ((entity1.LX_POSICAO_DA_TRANSACAO) == 6 ? "Painel Flutuante" : ((entity1.LX_POSICAO_DA_TRANSACAO) == 2 ? "Painel à Esquerda" : ((entity1.LX_POSICAO_DA_TRANSACAO) == 1 ? "Página" : ((entity1.LX_POSICAO_DA_TRANSACAO) == 4 ? "Painel à Direita" : ((entity1.LX_POSICAO_DA_TRANSACAO) == 3 ? "Painel Superior" : ""))))))
                                , LxTipoLayout = entity1.LX_TIPO_LAYOUT
                                , LxTipoLayoutName = ((entity1.LX_TIPO_LAYOUT) == 6 ? "Grade de Dados em Baixo/Formulário em Cima" : ((entity1.LX_TIPO_LAYOUT) == 2 ? "Formulário" : ((entity1.LX_TIPO_LAYOUT) == 7 ? "Padrão" : ((entity1.LX_TIPO_LAYOUT) == 1 ? "Grade de Dados" : ((entity1.LX_TIPO_LAYOUT) == 3 ? "Grade de Dados à Esquerda/Formulário à Direita" : ((entity1.LX_TIPO_LAYOUT) == 5 ? "Grade de Dados à Direita/Formulário à Esquerda" : ((entity1.LX_TIPO_LAYOUT) == 4 ? "Grade de Dados em Cima/Formulário em Baixo" : "")))))))
                                , MostraBotaoAdicao = entity1.MOSTRA_BOTAO_ADICAO
                                , MostraBotaoEdicao = entity1.MOSTRA_BOTAO_EDICAO
                                , MostraBotaoExclusao = entity1.MOSTRA_BOTAO_EXCLUSAO
                                , MostraBotaoImpressao = entity1.MOSTRA_BOTAO_IMPRESSAO
                                , MostraBotaoLayout = entity1.MOSTRA_BOTAO_LAYOUT
                                , MostraBotaoLimpa = entity1.MOSTRA_BOTAO_LIMPA
                                , MostraBotaoNavegacao = entity1.MOSTRA_BOTAO_NAVEGACAO
                                , MostraBotaoPesquisa = entity1.MOSTRA_BOTAO_PESQUISA
                                , MostraBotaoPesquisaEsp = entity1.MOSTRA_BOTAO_PESQUISA_ESP
                                , PossuiToolbar = entity1.POSSUI_TOOLBAR
                                , PossuiVisaoTabular = entity1.POSSUI_VISAO_TABULAR
                                , PropriedadesDoDetalhe = entity1.PROPRIEDADES_DO_DETALHE
                                , PropriedadesDoMestre = entity1.PROPRIEDADES_DO_MESTRE
                                , UsaFiltrosDoBoPrincipal = entity1.USA_FILTROS_DO_BO_PRINCIPAL
                                , Visivel = entity1.VISIVEL
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsTransacaoMenuAutorizacao.
	    public IQueryable<TcsTransacaoMenuAutorizacao> GetTcsTransacaoMenuAutorizacao()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoMenuAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_MENU_AUTORIZACAO
                  let entity0Al4 = entity0.TCS_TRANSACAO_AUTORIZACAO
                  let entity0Al2 = entity0.TCS_MODULO_MENU_AUTORIZACAO
                  let entity0Al1 = entity0.TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO
                  let entity0Al3 = entity0.TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO
	            
	            	
	            select new TcsTransacaoMenuAutorizacao()		
	            {
	            
                ClasseNome = entity0Al4.CLASSE_NOME
                , CodTransacao = entity0Al4.COD_TRANSACAO
                , DescModulo = entity0Al1.DESC_MODULO
                , DescModuloMenu = entity0Al2.DESC_MODULO_MENU
                , DescricaoAplicativo = entity0Al3.DESCRICAO_APLICATIVO
                , DescTransacao = entity0Al4.DESC_TRANSACAO
                , IdModulo = entity0Al1.ID_MODULO
                , IdModuloMenu = entity0Al2.ID_MODULO_MENU
                , IdTcsTransacaoMenuAutorizacao = entity0.ID_TCS_TRANSACAO_MENU_AUTORIZACAO
                , IdTransacao = entity0Al4.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , InativoModulo = entity0Al1.INATIVO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsTransacaoDependenteAutorizacao.
	    public IQueryable<TcsTransacaoDependenteAutorizacao> GetTcsTransacaoDependenteAutorizacao()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoDependenteAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO
                  let entity0Al1 = entity0.TRANSACAO_RELACIONADA
                  let entity0Al2 = entity0.TCS_TRANSACAO_AUTORIZACAO
	            
	            	
	            select new TcsTransacaoDependenteAutorizacao()		
	            {
	            
                ClasseNome = entity0Al1.CLASSE_NOME
                , CompartilhaBoPrincipal = entity0.COMPARTILHA_BO_PRINCIPAL
                , DescTransacao = entity0Al1.DESC_TRANSACAO
                , ExecutaPesquisa = entity0.EXECUTA_PESQUISA
                , IdTransacao = entity0Al2.ID_TRANSACAO
                , IdTransacaoRelacionada = entity0Al1.ID_TRANSACAO
                , IdTransacaoDependente = entity0.ID_TRANSACAO_DEPENDENTE
                , LxPosicaoDaTransacao = entity0.LX_POSICAO_DA_TRANSACAO
                , LxPosicaoDaTransacaoName = ((entity0.LX_POSICAO_DA_TRANSACAO) == 5 ? "Painel Inferior" : ((entity0.LX_POSICAO_DA_TRANSACAO) == 6 ? "Painel Flutuante" : ((entity0.LX_POSICAO_DA_TRANSACAO) == 2 ? "Painel à Esquerda" : ((entity0.LX_POSICAO_DA_TRANSACAO) == 1 ? "Página" : ((entity0.LX_POSICAO_DA_TRANSACAO) == 4 ? "Painel à Direita" : ((entity0.LX_POSICAO_DA_TRANSACAO) == 3 ? "Painel Superior" : ""))))))
                , LxTipoLayout = entity0.LX_TIPO_LAYOUT
                , LxTipoLayoutName = ((entity0.LX_TIPO_LAYOUT) == 6 ? "Grade de Dados em Baixo/Formulário em Cima" : ((entity0.LX_TIPO_LAYOUT) == 2 ? "Formulário" : ((entity0.LX_TIPO_LAYOUT) == 7 ? "Padrão" : ((entity0.LX_TIPO_LAYOUT) == 1 ? "Grade de Dados" : ((entity0.LX_TIPO_LAYOUT) == 3 ? "Grade de Dados à Esquerda/Formulário à Direita" : ((entity0.LX_TIPO_LAYOUT) == 5 ? "Grade de Dados à Direita/Formulário à Esquerda" : ((entity0.LX_TIPO_LAYOUT) == 4 ? "Grade de Dados em Cima/Formulário em Baixo" : "")))))))
                , MostraBotaoAdicao = entity0.MOSTRA_BOTAO_ADICAO
                , MostraBotaoEdicao = entity0.MOSTRA_BOTAO_EDICAO
                , MostraBotaoExclusao = entity0.MOSTRA_BOTAO_EXCLUSAO
                , MostraBotaoImpressao = entity0.MOSTRA_BOTAO_IMPRESSAO
                , MostraBotaoLayout = entity0.MOSTRA_BOTAO_LAYOUT
                , MostraBotaoLimpa = entity0.MOSTRA_BOTAO_LIMPA
                , MostraBotaoNavegacao = entity0.MOSTRA_BOTAO_NAVEGACAO
                , MostraBotaoPesquisa = entity0.MOSTRA_BOTAO_PESQUISA
                , MostraBotaoPesquisaEsp = entity0.MOSTRA_BOTAO_PESQUISA_ESP
                , PossuiToolbar = entity0.POSSUI_TOOLBAR
                , PossuiVisaoTabular = entity0.POSSUI_VISAO_TABULAR
                , PropriedadesDoDetalhe = entity0.PROPRIEDADES_DO_DETALHE
                , PropriedadesDoMestre = entity0.PROPRIEDADES_DO_MESTRE
                , UsaFiltrosDoBoPrincipal = entity0.USA_FILTROS_DO_BO_PRINCIPAL
                , Visivel = entity0.VISIVEL
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsTransacaoAutorizacaoNoAssociations.
	    public IQueryable<TcsTransacaoAutorizacao> GetTcsTransacaoAutorizacaoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_AUTORIZACAO
                  let entity0Al1 = entity0.TCS_MODULO_AUTORIZACAO
                  let entity0Al2 = entity0.TCS_OBJETO_AUTORIZACAO
	            
	            	
	            select new TcsTransacaoAutorizacao()		
	            {
	            
                ClasseNome = entity0.CLASSE_NOME
                , CodTransacao = entity0.COD_TRANSACAO
                , DescModulo = entity0Al1.DESC_MODULO
                , DescObjeto = entity0Al2.DESC_OBJETO
                , DescTransacao = entity0.DESC_TRANSACAO
                , Icone = entity0.ICONE
                , IdModulo = entity0Al1.ID_MODULO
                , IdObjeto = entity0Al2.ID_OBJETO
                , IdTransacao = entity0.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , LxCorFundoName = ((entity0.LX_COR_FUNDO) == 8 ? "Fundo Laranja" : ((entity0.LX_COR_FUNDO) == 10 ? "Fundo Roxo" : ((entity0.LX_COR_FUNDO) == 7 ? "Laranja" : ((entity0.LX_COR_FUNDO) == 9 ? "Roxo" : ""))))
                , LxTipoTransacao = entity0.LX_TIPO_TRANSACAO
                , LxTipoTransacaoName = ((entity0.LX_TIPO_TRANSACAO) == 7 ? "Assistente" : ((entity0.LX_TIPO_TRANSACAO) == 8 ? "Dashboard" : ((entity0.LX_TIPO_TRANSACAO) == 2 ? "ERP" : ((entity0.LX_TIPO_TRANSACAO) == 6 ? "ERP App" : ((entity0.LX_TIPO_TRANSACAO) == 4 ? "Excel" : ((entity0.LX_TIPO_TRANSACAO) == 3 ? "Loja" : ((entity0.LX_TIPO_TRANSACAO) == 5 ? "Mobile" : ((entity0.LX_TIPO_TRANSACAO) == 1 ? "Todos" : ""))))))))
                , NomeCurto = entity0.NOME_CURTO
                , ObjetoClasseNome = entity0Al2.CLASSE_NOME
                , Tag = entity0.TAG
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsTransacaoMenuAutorizacaoNoAssociations.
	    public IQueryable<TcsTransacaoMenuAutorizacao> GetTcsTransacaoMenuAutorizacaoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoMenuAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_MENU_AUTORIZACAO
                  let entity0Al4 = entity0.TCS_TRANSACAO_AUTORIZACAO
                  let entity0Al2 = entity0.TCS_MODULO_MENU_AUTORIZACAO
                  let entity0Al1 = entity0.TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO
                  let entity0Al3 = entity0.TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO
	            
	            	
	            select new TcsTransacaoMenuAutorizacao()		
	            {
	            
                ClasseNome = entity0Al4.CLASSE_NOME
                , CodTransacao = entity0Al4.COD_TRANSACAO
                , DescModulo = entity0Al1.DESC_MODULO
                , DescModuloMenu = entity0Al2.DESC_MODULO_MENU
                , DescricaoAplicativo = entity0Al3.DESCRICAO_APLICATIVO
                , DescTransacao = entity0Al4.DESC_TRANSACAO
                , IdModulo = entity0Al1.ID_MODULO
                , IdModuloMenu = entity0Al2.ID_MODULO_MENU
                , IdTcsTransacaoMenuAutorizacao = entity0.ID_TCS_TRANSACAO_MENU_AUTORIZACAO
                , IdTransacao = entity0Al4.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , InativoModulo = entity0Al1.INATIVO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsTransacaoDependenteAutorizacaoNoAssociations.
	    public IQueryable<TcsTransacaoDependenteAutorizacao> GetTcsTransacaoDependenteAutorizacaoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoDependenteAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO
                  let entity0Al1 = entity0.TRANSACAO_RELACIONADA
                  let entity0Al2 = entity0.TCS_TRANSACAO_AUTORIZACAO
	            
	            	
	            select new TcsTransacaoDependenteAutorizacao()		
	            {
	            
                ClasseNome = entity0Al1.CLASSE_NOME
                , CompartilhaBoPrincipal = entity0.COMPARTILHA_BO_PRINCIPAL
                , DescTransacao = entity0Al1.DESC_TRANSACAO
                , ExecutaPesquisa = entity0.EXECUTA_PESQUISA
                , IdTransacao = entity0Al2.ID_TRANSACAO
                , IdTransacaoRelacionada = entity0Al1.ID_TRANSACAO
                , IdTransacaoDependente = entity0.ID_TRANSACAO_DEPENDENTE
                , LxPosicaoDaTransacao = entity0.LX_POSICAO_DA_TRANSACAO
                , LxPosicaoDaTransacaoName = ((entity0.LX_POSICAO_DA_TRANSACAO) == 5 ? "Painel Inferior" : ((entity0.LX_POSICAO_DA_TRANSACAO) == 6 ? "Painel Flutuante" : ((entity0.LX_POSICAO_DA_TRANSACAO) == 2 ? "Painel à Esquerda" : ((entity0.LX_POSICAO_DA_TRANSACAO) == 1 ? "Página" : ((entity0.LX_POSICAO_DA_TRANSACAO) == 4 ? "Painel à Direita" : ((entity0.LX_POSICAO_DA_TRANSACAO) == 3 ? "Painel Superior" : ""))))))
                , LxTipoLayout = entity0.LX_TIPO_LAYOUT
                , LxTipoLayoutName = ((entity0.LX_TIPO_LAYOUT) == 6 ? "Grade de Dados em Baixo/Formulário em Cima" : ((entity0.LX_TIPO_LAYOUT) == 2 ? "Formulário" : ((entity0.LX_TIPO_LAYOUT) == 7 ? "Padrão" : ((entity0.LX_TIPO_LAYOUT) == 1 ? "Grade de Dados" : ((entity0.LX_TIPO_LAYOUT) == 3 ? "Grade de Dados à Esquerda/Formulário à Direita" : ((entity0.LX_TIPO_LAYOUT) == 5 ? "Grade de Dados à Direita/Formulário à Esquerda" : ((entity0.LX_TIPO_LAYOUT) == 4 ? "Grade de Dados em Cima/Formulário em Baixo" : "")))))))
                , MostraBotaoAdicao = entity0.MOSTRA_BOTAO_ADICAO
                , MostraBotaoEdicao = entity0.MOSTRA_BOTAO_EDICAO
                , MostraBotaoExclusao = entity0.MOSTRA_BOTAO_EXCLUSAO
                , MostraBotaoImpressao = entity0.MOSTRA_BOTAO_IMPRESSAO
                , MostraBotaoLayout = entity0.MOSTRA_BOTAO_LAYOUT
                , MostraBotaoLimpa = entity0.MOSTRA_BOTAO_LIMPA
                , MostraBotaoNavegacao = entity0.MOSTRA_BOTAO_NAVEGACAO
                , MostraBotaoPesquisa = entity0.MOSTRA_BOTAO_PESQUISA
                , MostraBotaoPesquisaEsp = entity0.MOSTRA_BOTAO_PESQUISA_ESP
                , PossuiToolbar = entity0.POSSUI_TOOLBAR
                , PossuiVisaoTabular = entity0.POSSUI_VISAO_TABULAR
                , PropriedadesDoDetalhe = entity0.PROPRIEDADES_DO_DETALHE
                , PropriedadesDoMestre = entity0.PROPRIEDADES_DO_MESTRE
                , UsaFiltrosDoBoPrincipal = entity0.USA_FILTROS_DO_BO_PRINCIPAL
                , Visivel = entity0.VISIVEL
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	result.Add("TcsTransacaoAutorizacao|LxCorFundo");
	    	result.Add("TcsTransacaoAutorizacao|TCS_TRANSACAO_AUTORIZACAO.LX_COR_FUNDO");
	    	//Add filtering disabled property for TCS_TRANSACAO_AUTORIZACAO
	    	string[] bmDisabledTcsTransacaoAutorizacaoList = this.GetEDM().GetFilteringDisabledList("TCS_TRANSACAO_AUTORIZACAO");
	    	if (bmDisabledTcsTransacaoAutorizacaoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsTransacaoAutorizacaoList.Contains("TCS_TRANSACAO_AUTORIZACAO.CLASSE_NOME"))
	    		{
	    			result.Add("TcsTransacaoAutorizacao|ClasseNome");
	    			result.Add("TcsTransacaoAutorizacao|TCS_TRANSACAO_AUTORIZACAO.CLASSE_NOME");
	    		}
	
	    		if (bmDisabledTcsTransacaoAutorizacaoList.Contains("TCS_TRANSACAO_AUTORIZACAO.COD_TRANSACAO"))
	    		{
	    			result.Add("TcsTransacaoAutorizacao|CodTransacao");
	    			result.Add("TcsTransacaoAutorizacao|TCS_TRANSACAO_AUTORIZACAO.COD_TRANSACAO");
	    		}
	
	    		if (bmDisabledTcsTransacaoAutorizacaoList.Contains("TCS_TRANSACAO_AUTORIZACAO.DESC_TRANSACAO"))
	    		{
	    			result.Add("TcsTransacaoAutorizacao|DescTransacao");
	    			result.Add("TcsTransacaoAutorizacao|TCS_TRANSACAO_AUTORIZACAO.DESC_TRANSACAO");
	    		}
	
	    		if (bmDisabledTcsTransacaoAutorizacaoList.Contains("TCS_TRANSACAO_AUTORIZACAO.ICONE"))
	    		{
	    			result.Add("TcsTransacaoAutorizacao|Icone");
	    			result.Add("TcsTransacaoAutorizacao|TCS_TRANSACAO_AUTORIZACAO.ICONE");
	    		}
	
	    		if (bmDisabledTcsTransacaoAutorizacaoList.Contains("TCS_TRANSACAO_AUTORIZACAO.ID_TRANSACAO"))
	    		{
	    			result.Add("TcsTransacaoAutorizacao|IdTransacao");
	    			result.Add("TcsTransacaoAutorizacao|TCS_TRANSACAO_AUTORIZACAO.ID_TRANSACAO");
	    		}
	
	    		if (bmDisabledTcsTransacaoAutorizacaoList.Contains("TCS_TRANSACAO_AUTORIZACAO.INATIVO"))
	    		{
	    			result.Add("TcsTransacaoAutorizacao|Inativo");
	    			result.Add("TcsTransacaoAutorizacao|TCS_TRANSACAO_AUTORIZACAO.INATIVO");
	    		}
	
	    		if (bmDisabledTcsTransacaoAutorizacaoList.Contains("TCS_TRANSACAO_AUTORIZACAO.LX_TIPO_TRANSACAO"))
	    		{
	    			result.Add("TcsTransacaoAutorizacao|LxTipoTransacao");
	    			result.Add("TcsTransacaoAutorizacao|TCS_TRANSACAO_AUTORIZACAO.LX_TIPO_TRANSACAO");
	    		}
	
	    		if (bmDisabledTcsTransacaoAutorizacaoList.Contains("TCS_TRANSACAO_AUTORIZACAO.NOME_CURTO"))
	    		{
	    			result.Add("TcsTransacaoAutorizacao|NomeCurto");
	    			result.Add("TcsTransacaoAutorizacao|TCS_TRANSACAO_AUTORIZACAO.NOME_CURTO");
	    		}
	
	    		if (bmDisabledTcsTransacaoAutorizacaoList.Contains("TCS_TRANSACAO_AUTORIZACAO.TAG"))
	    		{
	    			result.Add("TcsTransacaoAutorizacao|Tag");
	    			result.Add("TcsTransacaoAutorizacao|TCS_TRANSACAO_AUTORIZACAO.TAG");
	    		}
	    	}
	    	result.Add("TcsTransacaoMenuAutorizacao|ClasseNome");
	    	result.Add("TcsTransacaoMenuAutorizacao|TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.CLASSE_NOME");
	    	result.Add("TcsTransacaoMenuAutorizacao|CodTransacao");
	    	result.Add("TcsTransacaoMenuAutorizacao|TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.COD_TRANSACAO");
	    	result.Add("TcsTransacaoMenuAutorizacao|DescTransacao");
	    	result.Add("TcsTransacaoMenuAutorizacao|TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.DESC_TRANSACAO");
	    	//Add filtering disabled property for TCS_TRANSACAO_MENU_AUTORIZACAO
	    	string[] bmDisabledTcsTransacaoMenuAutorizacaoList = this.GetEDM().GetFilteringDisabledList("TCS_TRANSACAO_MENU_AUTORIZACAO");
	    	if (bmDisabledTcsTransacaoMenuAutorizacaoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsTransacaoMenuAutorizacaoList.Contains("TCS_TRANSACAO_MENU_AUTORIZACAO.ID_TCS_TRANSACAO_MENU_AUTORIZACAO"))
	    		{
	    			result.Add("TcsTransacaoMenuAutorizacao|IdTcsTransacaoMenuAutorizacao");
	    			result.Add("TcsTransacaoMenuAutorizacao|TCS_TRANSACAO_MENU_AUTORIZACAO.ID_TCS_TRANSACAO_MENU_AUTORIZACAO");
	    		}
	
	    		if (bmDisabledTcsTransacaoMenuAutorizacaoList.Contains("TCS_TRANSACAO_MENU_AUTORIZACAO.INATIVO"))
	    		{
	    			result.Add("TcsTransacaoMenuAutorizacao|Inativo");
	    			result.Add("TcsTransacaoMenuAutorizacao|TCS_TRANSACAO_MENU_AUTORIZACAO.INATIVO");
	    		}
	
	    		if (bmDisabledTcsTransacaoMenuAutorizacaoList.Contains("TCS_TRANSACAO_MENU_AUTORIZACAO.ORDEM_NAVEGACAO"))
	    		{
	    			result.Add("TcsTransacaoMenuAutorizacao|OrdemNavegacao");
	    			result.Add("TcsTransacaoMenuAutorizacao|TCS_TRANSACAO_MENU_AUTORIZACAO.ORDEM_NAVEGACAO");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO
	    	string[] bmDisabledTcsTransacaoDependenteAutorizacaoList = this.GetEDM().GetFilteringDisabledList("TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO");
	    	if (bmDisabledTcsTransacaoDependenteAutorizacaoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsTransacaoDependenteAutorizacaoList.Contains("TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.COMPARTILHA_BO_PRINCIPAL"))
	    		{
	    			result.Add("TcsTransacaoDependenteAutorizacao|CompartilhaBoPrincipal");
	    			result.Add("TcsTransacaoDependenteAutorizacao|TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.COMPARTILHA_BO_PRINCIPAL");
	    		}
	
	    		if (bmDisabledTcsTransacaoDependenteAutorizacaoList.Contains("TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.EXECUTA_PESQUISA"))
	    		{
	    			result.Add("TcsTransacaoDependenteAutorizacao|ExecutaPesquisa");
	    			result.Add("TcsTransacaoDependenteAutorizacao|TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.EXECUTA_PESQUISA");
	    		}
	
	    		if (bmDisabledTcsTransacaoDependenteAutorizacaoList.Contains("TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.ID_TRANSACAO_DEPENDENTE"))
	    		{
	    			result.Add("TcsTransacaoDependenteAutorizacao|IdTransacaoDependente");
	    			result.Add("TcsTransacaoDependenteAutorizacao|TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.ID_TRANSACAO_DEPENDENTE");
	    		}
	
	    		if (bmDisabledTcsTransacaoDependenteAutorizacaoList.Contains("TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.LX_POSICAO_DA_TRANSACAO"))
	    		{
	    			result.Add("TcsTransacaoDependenteAutorizacao|LxPosicaoDaTransacao");
	    			result.Add("TcsTransacaoDependenteAutorizacao|TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.LX_POSICAO_DA_TRANSACAO");
	    		}
	
	    		if (bmDisabledTcsTransacaoDependenteAutorizacaoList.Contains("TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.LX_TIPO_LAYOUT"))
	    		{
	    			result.Add("TcsTransacaoDependenteAutorizacao|LxTipoLayout");
	    			result.Add("TcsTransacaoDependenteAutorizacao|TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.LX_TIPO_LAYOUT");
	    		}
	
	    		if (bmDisabledTcsTransacaoDependenteAutorizacaoList.Contains("TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_ADICAO"))
	    		{
	    			result.Add("TcsTransacaoDependenteAutorizacao|MostraBotaoAdicao");
	    			result.Add("TcsTransacaoDependenteAutorizacao|TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_ADICAO");
	    		}
	
	    		if (bmDisabledTcsTransacaoDependenteAutorizacaoList.Contains("TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_EDICAO"))
	    		{
	    			result.Add("TcsTransacaoDependenteAutorizacao|MostraBotaoEdicao");
	    			result.Add("TcsTransacaoDependenteAutorizacao|TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_EDICAO");
	    		}
	
	    		if (bmDisabledTcsTransacaoDependenteAutorizacaoList.Contains("TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_EXCLUSAO"))
	    		{
	    			result.Add("TcsTransacaoDependenteAutorizacao|MostraBotaoExclusao");
	    			result.Add("TcsTransacaoDependenteAutorizacao|TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_EXCLUSAO");
	    		}
	
	    		if (bmDisabledTcsTransacaoDependenteAutorizacaoList.Contains("TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_IMPRESSAO"))
	    		{
	    			result.Add("TcsTransacaoDependenteAutorizacao|MostraBotaoImpressao");
	    			result.Add("TcsTransacaoDependenteAutorizacao|TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_IMPRESSAO");
	    		}
	
	    		if (bmDisabledTcsTransacaoDependenteAutorizacaoList.Contains("TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_LAYOUT"))
	    		{
	    			result.Add("TcsTransacaoDependenteAutorizacao|MostraBotaoLayout");
	    			result.Add("TcsTransacaoDependenteAutorizacao|TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_LAYOUT");
	    		}
	
	    		if (bmDisabledTcsTransacaoDependenteAutorizacaoList.Contains("TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_LIMPA"))
	    		{
	    			result.Add("TcsTransacaoDependenteAutorizacao|MostraBotaoLimpa");
	    			result.Add("TcsTransacaoDependenteAutorizacao|TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_LIMPA");
	    		}
	
	    		if (bmDisabledTcsTransacaoDependenteAutorizacaoList.Contains("TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_NAVEGACAO"))
	    		{
	    			result.Add("TcsTransacaoDependenteAutorizacao|MostraBotaoNavegacao");
	    			result.Add("TcsTransacaoDependenteAutorizacao|TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_NAVEGACAO");
	    		}
	
	    		if (bmDisabledTcsTransacaoDependenteAutorizacaoList.Contains("TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_PESQUISA"))
	    		{
	    			result.Add("TcsTransacaoDependenteAutorizacao|MostraBotaoPesquisa");
	    			result.Add("TcsTransacaoDependenteAutorizacao|TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_PESQUISA");
	    		}
	
	    		if (bmDisabledTcsTransacaoDependenteAutorizacaoList.Contains("TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_PESQUISA_ESP"))
	    		{
	    			result.Add("TcsTransacaoDependenteAutorizacao|MostraBotaoPesquisaEsp");
	    			result.Add("TcsTransacaoDependenteAutorizacao|TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.MOSTRA_BOTAO_PESQUISA_ESP");
	    		}
	
	    		if (bmDisabledTcsTransacaoDependenteAutorizacaoList.Contains("TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.POSSUI_TOOLBAR"))
	    		{
	    			result.Add("TcsTransacaoDependenteAutorizacao|PossuiToolbar");
	    			result.Add("TcsTransacaoDependenteAutorizacao|TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.POSSUI_TOOLBAR");
	    		}
	
	    		if (bmDisabledTcsTransacaoDependenteAutorizacaoList.Contains("TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.POSSUI_VISAO_TABULAR"))
	    		{
	    			result.Add("TcsTransacaoDependenteAutorizacao|PossuiVisaoTabular");
	    			result.Add("TcsTransacaoDependenteAutorizacao|TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.POSSUI_VISAO_TABULAR");
	    		}
	
	    		if (bmDisabledTcsTransacaoDependenteAutorizacaoList.Contains("TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.PROPRIEDADES_DO_DETALHE"))
	    		{
	    			result.Add("TcsTransacaoDependenteAutorizacao|PropriedadesDoDetalhe");
	    			result.Add("TcsTransacaoDependenteAutorizacao|TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.PROPRIEDADES_DO_DETALHE");
	    		}
	
	    		if (bmDisabledTcsTransacaoDependenteAutorizacaoList.Contains("TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.PROPRIEDADES_DO_MESTRE"))
	    		{
	    			result.Add("TcsTransacaoDependenteAutorizacao|PropriedadesDoMestre");
	    			result.Add("TcsTransacaoDependenteAutorizacao|TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.PROPRIEDADES_DO_MESTRE");
	    		}
	
	    		if (bmDisabledTcsTransacaoDependenteAutorizacaoList.Contains("TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.USA_FILTROS_DO_BO_PRINCIPAL"))
	    		{
	    			result.Add("TcsTransacaoDependenteAutorizacao|UsaFiltrosDoBoPrincipal");
	    			result.Add("TcsTransacaoDependenteAutorizacao|TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.USA_FILTROS_DO_BO_PRINCIPAL");
	    		}
	
	    		if (bmDisabledTcsTransacaoDependenteAutorizacaoList.Contains("TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.VISIVEL"))
	    		{
	    			result.Add("TcsTransacaoDependenteAutorizacao|Visivel");
	    			result.Add("TcsTransacaoDependenteAutorizacao|TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.VISIVEL");
	    		}
	    	}
	    	return result;
	    }

	    #endregion Excluded Filters

	    #region Get By EntitySearchId Methods Definitions.
	
				
	    [Ignore]
	    //Get TcsTransacaoAutorizacao By EntitySearchId.
	    public IQueryable<TcsTransacaoAutorizacao> GetTcsTransacaoAutorizacaoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsTransacaoAutorizacaoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsTransacaoMenuAutorizacao By EntitySearchId.
	    public IQueryable<TcsTransacaoMenuAutorizacao> GetTcsTransacaoMenuAutorizacaoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsTransacaoMenuAutorizacaoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsTransacaoDependenteAutorizacao By EntitySearchId.
	    public IQueryable<TcsTransacaoDependenteAutorizacao> GetTcsTransacaoDependenteAutorizacaoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsTransacaoDependenteAutorizacaoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsTransacaoAutorizacao By EntitySearchId.
	    public IQueryable<TcsTransacaoAutorizacao> GetTcsTransacaoAutorizacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsTransacaoAutorizacaoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsTransacaoMenuAutorizacao By EntitySearchId.
	    public IQueryable<TcsTransacaoMenuAutorizacao> GetTcsTransacaoMenuAutorizacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsTransacaoMenuAutorizacaoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsTransacaoDependenteAutorizacao By EntitySearchId.
	    public IQueryable<TcsTransacaoDependenteAutorizacao> GetTcsTransacaoDependenteAutorizacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsTransacaoDependenteAutorizacaoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get TcsTransacaoAutorizacao By Example.
	    [Ignore]
	    public IQueryable<TcsTransacaoAutorizacao> GetTcsTransacaoAutorizacaoByExample(TcsTransacaoAutorizacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsTransacaoAutorizacaoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsTransacaoMenuAutorizacao By Example.
	    [Ignore]
	    public IQueryable<TcsTransacaoMenuAutorizacao> GetTcsTransacaoMenuAutorizacaoByExample(TcsTransacaoMenuAutorizacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsTransacaoMenuAutorizacaoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsTransacaoDependenteAutorizacao By Example.
	    [Ignore]
	    public IQueryable<TcsTransacaoDependenteAutorizacao> GetTcsTransacaoDependenteAutorizacaoByExample(TcsTransacaoDependenteAutorizacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsTransacaoDependenteAutorizacaoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsTransacaoAutorizacao By Example.
	    [Ignore]
	    public IQueryable<TcsTransacaoAutorizacao> GetTcsTransacaoAutorizacaoByExampleNoAssociations(TcsTransacaoAutorizacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsTransacaoAutorizacaoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsTransacaoMenuAutorizacao By Example.
	    [Ignore]
	    public IQueryable<TcsTransacaoMenuAutorizacao> GetTcsTransacaoMenuAutorizacaoByExampleNoAssociations(TcsTransacaoMenuAutorizacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsTransacaoMenuAutorizacaoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsTransacaoDependenteAutorizacao By Example.
	    [Ignore]
	    public IQueryable<TcsTransacaoDependenteAutorizacao> GetTcsTransacaoDependenteAutorizacaoByExampleNoAssociations(TcsTransacaoDependenteAutorizacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsTransacaoDependenteAutorizacaoByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public TcsTransacaoAutorizacao GetTcsTransacaoAutorizacaoByKey(long idTransacao)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsTransacaoAutorizacao");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTransacao"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTransacao));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsTransacaoAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsTransacaoMenuAutorizacao GetTcsTransacaoMenuAutorizacaoByKey(int idTcsTransacaoMenuAutorizacao)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsTransacaoMenuAutorizacao");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsTransacaoMenuAutorizacao"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsTransacaoMenuAutorizacao));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsTransacaoMenuAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsTransacaoDependenteAutorizacao GetTcsTransacaoDependenteAutorizacaoByKey(Int64 idTransacaoDependente)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsTransacaoDependenteAutorizacao");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTransacaoDependente"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTransacaoDependente));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsTransacaoDependenteAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get TcsTransacaoAutorizacaoByEntitySearch.
	    public IQueryable<TcsTransacaoAutorizacao> GetTcsTransacaoAutorizacaoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacaoAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_MODULO_AUTORIZACAO
                  let entity0Al2 = entity0.TCS_OBJETO_AUTORIZACAO
	            
	            	
	            select new TcsTransacaoAutorizacao()		
	            {
	            
                ClasseNome = entity0.CLASSE_NOME
                , CodTransacao = entity0.COD_TRANSACAO
                , DescModulo = entity0Al1.DESC_MODULO
                , DescObjeto = entity0Al2.DESC_OBJETO
                , DescTransacao = entity0.DESC_TRANSACAO
                , Icone = entity0.ICONE
                , IdModulo = entity0Al1.ID_MODULO
                , IdObjeto = entity0Al2.ID_OBJETO
                , IdTransacao = entity0.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , LxCorFundoName = ((entity0.LX_COR_FUNDO) == 8 ? "Fundo Laranja" : ((entity0.LX_COR_FUNDO) == 10 ? "Fundo Roxo" : ((entity0.LX_COR_FUNDO) == 7 ? "Laranja" : ((entity0.LX_COR_FUNDO) == 9 ? "Roxo" : ""))))
                , LxTipoTransacao = entity0.LX_TIPO_TRANSACAO
                , LxTipoTransacaoName = ((entity0.LX_TIPO_TRANSACAO) == 7 ? "Assistente" : ((entity0.LX_TIPO_TRANSACAO) == 8 ? "Dashboard" : ((entity0.LX_TIPO_TRANSACAO) == 2 ? "ERP" : ((entity0.LX_TIPO_TRANSACAO) == 6 ? "ERP App" : ((entity0.LX_TIPO_TRANSACAO) == 4 ? "Excel" : ((entity0.LX_TIPO_TRANSACAO) == 3 ? "Loja" : ((entity0.LX_TIPO_TRANSACAO) == 5 ? "Mobile" : ((entity0.LX_TIPO_TRANSACAO) == 1 ? "Todos" : ""))))))))
                , NomeCurto = entity0.NOME_CURTO
                , ObjetoClasseNome = entity0Al2.CLASSE_NOME
                , Tag = entity0.TAG
			
                ,TcsTransacaoMenuAutorizacaoList = 
	                        (from entity1 in entity0.TCS_TRANSACAO_MENU_AUTORIZACAO_LISTA
                                  let entity1Al4 = entity1.TCS_TRANSACAO_AUTORIZACAO
                                  let entity1Al2 = entity1.TCS_MODULO_MENU_AUTORIZACAO
                                  let entity1Al1 = entity1.TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO
                                  let entity1Al3 = entity1.TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO
	                        
	                        	
	                        select new TcsTransacaoMenuAutorizacao()
	                        {
	                        
                                ClasseNome = entity1Al4.CLASSE_NOME
                                , CodTransacao = entity1Al4.COD_TRANSACAO
                                , DescModulo = entity1Al1.DESC_MODULO
                                , DescModuloMenu = entity1Al2.DESC_MODULO_MENU
                                , DescricaoAplicativo = entity1Al3.DESCRICAO_APLICATIVO
                                , DescTransacao = entity1Al4.DESC_TRANSACAO
                                , IdModulo = entity1Al1.ID_MODULO
                                , IdModuloMenu = entity1Al2.ID_MODULO_MENU
                                , IdTcsTransacaoMenuAutorizacao = entity1.ID_TCS_TRANSACAO_MENU_AUTORIZACAO
                                , IdTransacao = entity1Al4.ID_TRANSACAO
                                , Inativo = entity1.INATIVO
                                , InativoModulo = entity1Al1.INATIVO
                                , OrdemNavegacao = entity1.ORDEM_NAVEGACAO
		
	                        }
	                        )
			
                ,TcsTransacaoDependenteAutorizacaoList = 
	                        (from entity1 in entity0.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO_LISTA
                                  let entity1Al1 = entity1.TRANSACAO_RELACIONADA
                                  let entity1Al2 = entity1.TCS_TRANSACAO_AUTORIZACAO
	                        
	                        	
	                        select new TcsTransacaoDependenteAutorizacao()
	                        {
	                        
                                ClasseNome = entity1Al1.CLASSE_NOME
                                , CompartilhaBoPrincipal = entity1.COMPARTILHA_BO_PRINCIPAL
                                , DescTransacao = entity1Al1.DESC_TRANSACAO
                                , ExecutaPesquisa = entity1.EXECUTA_PESQUISA
                                , IdTransacao = entity1Al2.ID_TRANSACAO
                                , IdTransacaoRelacionada = entity1Al1.ID_TRANSACAO
                                , IdTransacaoDependente = entity1.ID_TRANSACAO_DEPENDENTE
                                , LxPosicaoDaTransacao = entity1.LX_POSICAO_DA_TRANSACAO
                                , LxPosicaoDaTransacaoName = ((entity1.LX_POSICAO_DA_TRANSACAO) == 5 ? "Painel Inferior" : ((entity1.LX_POSICAO_DA_TRANSACAO) == 6 ? "Painel Flutuante" : ((entity1.LX_POSICAO_DA_TRANSACAO) == 2 ? "Painel à Esquerda" : ((entity1.LX_POSICAO_DA_TRANSACAO) == 1 ? "Página" : ((entity1.LX_POSICAO_DA_TRANSACAO) == 4 ? "Painel à Direita" : ((entity1.LX_POSICAO_DA_TRANSACAO) == 3 ? "Painel Superior" : ""))))))
                                , LxTipoLayout = entity1.LX_TIPO_LAYOUT
                                , LxTipoLayoutName = ((entity1.LX_TIPO_LAYOUT) == 6 ? "Grade de Dados em Baixo/Formulário em Cima" : ((entity1.LX_TIPO_LAYOUT) == 2 ? "Formulário" : ((entity1.LX_TIPO_LAYOUT) == 7 ? "Padrão" : ((entity1.LX_TIPO_LAYOUT) == 1 ? "Grade de Dados" : ((entity1.LX_TIPO_LAYOUT) == 3 ? "Grade de Dados à Esquerda/Formulário à Direita" : ((entity1.LX_TIPO_LAYOUT) == 5 ? "Grade de Dados à Direita/Formulário à Esquerda" : ((entity1.LX_TIPO_LAYOUT) == 4 ? "Grade de Dados em Cima/Formulário em Baixo" : "")))))))
                                , MostraBotaoAdicao = entity1.MOSTRA_BOTAO_ADICAO
                                , MostraBotaoEdicao = entity1.MOSTRA_BOTAO_EDICAO
                                , MostraBotaoExclusao = entity1.MOSTRA_BOTAO_EXCLUSAO
                                , MostraBotaoImpressao = entity1.MOSTRA_BOTAO_IMPRESSAO
                                , MostraBotaoLayout = entity1.MOSTRA_BOTAO_LAYOUT
                                , MostraBotaoLimpa = entity1.MOSTRA_BOTAO_LIMPA
                                , MostraBotaoNavegacao = entity1.MOSTRA_BOTAO_NAVEGACAO
                                , MostraBotaoPesquisa = entity1.MOSTRA_BOTAO_PESQUISA
                                , MostraBotaoPesquisaEsp = entity1.MOSTRA_BOTAO_PESQUISA_ESP
                                , PossuiToolbar = entity1.POSSUI_TOOLBAR
                                , PossuiVisaoTabular = entity1.POSSUI_VISAO_TABULAR
                                , PropriedadesDoDetalhe = entity1.PROPRIEDADES_DO_DETALHE
                                , PropriedadesDoMestre = entity1.PROPRIEDADES_DO_MESTRE
                                , UsaFiltrosDoBoPrincipal = entity1.USA_FILTROS_DO_BO_PRINCIPAL
                                , Visivel = entity1.VISIVEL
		
	                        }
	                        )
		
	            }
	            );
	
	        SetTcsTransacaoAutorizacaoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsTransacaoMenuAutorizacaoByEntitySearch.
	    public IQueryable<TcsTransacaoMenuAutorizacao> GetTcsTransacaoMenuAutorizacaoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacaoMenuAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoMenuAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_MENU_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al4 = entity0.TCS_TRANSACAO_AUTORIZACAO
                  let entity0Al2 = entity0.TCS_MODULO_MENU_AUTORIZACAO
                  let entity0Al1 = entity0.TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO
                  let entity0Al3 = entity0.TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO
	            
	            	
	            select new TcsTransacaoMenuAutorizacao()		
	            {
	            
                ClasseNome = entity0Al4.CLASSE_NOME
                , CodTransacao = entity0Al4.COD_TRANSACAO
                , DescModulo = entity0Al1.DESC_MODULO
                , DescModuloMenu = entity0Al2.DESC_MODULO_MENU
                , DescricaoAplicativo = entity0Al3.DESCRICAO_APLICATIVO
                , DescTransacao = entity0Al4.DESC_TRANSACAO
                , IdModulo = entity0Al1.ID_MODULO
                , IdModuloMenu = entity0Al2.ID_MODULO_MENU
                , IdTcsTransacaoMenuAutorizacao = entity0.ID_TCS_TRANSACAO_MENU_AUTORIZACAO
                , IdTransacao = entity0Al4.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , InativoModulo = entity0Al1.INATIVO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            );
	
	        SetTcsTransacaoMenuAutorizacaoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsTransacaoDependenteAutorizacaoByEntitySearch.
	    public IQueryable<TcsTransacaoDependenteAutorizacao> GetTcsTransacaoDependenteAutorizacaoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacaoDependenteAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoDependenteAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TRANSACAO_RELACIONADA
                  let entity0Al2 = entity0.TCS_TRANSACAO_AUTORIZACAO
	            
	            	
	            select new TcsTransacaoDependenteAutorizacao()		
	            {
	            
                ClasseNome = entity0Al1.CLASSE_NOME
                , CompartilhaBoPrincipal = entity0.COMPARTILHA_BO_PRINCIPAL
                , DescTransacao = entity0Al1.DESC_TRANSACAO
                , ExecutaPesquisa = entity0.EXECUTA_PESQUISA
                , IdTransacao = entity0Al2.ID_TRANSACAO
                , IdTransacaoRelacionada = entity0Al1.ID_TRANSACAO
                , IdTransacaoDependente = entity0.ID_TRANSACAO_DEPENDENTE
                , LxPosicaoDaTransacao = entity0.LX_POSICAO_DA_TRANSACAO
                , LxPosicaoDaTransacaoName = ((entity0.LX_POSICAO_DA_TRANSACAO) == 5 ? "Painel Inferior" : ((entity0.LX_POSICAO_DA_TRANSACAO) == 6 ? "Painel Flutuante" : ((entity0.LX_POSICAO_DA_TRANSACAO) == 2 ? "Painel à Esquerda" : ((entity0.LX_POSICAO_DA_TRANSACAO) == 1 ? "Página" : ((entity0.LX_POSICAO_DA_TRANSACAO) == 4 ? "Painel à Direita" : ((entity0.LX_POSICAO_DA_TRANSACAO) == 3 ? "Painel Superior" : ""))))))
                , LxTipoLayout = entity0.LX_TIPO_LAYOUT
                , LxTipoLayoutName = ((entity0.LX_TIPO_LAYOUT) == 6 ? "Grade de Dados em Baixo/Formulário em Cima" : ((entity0.LX_TIPO_LAYOUT) == 2 ? "Formulário" : ((entity0.LX_TIPO_LAYOUT) == 7 ? "Padrão" : ((entity0.LX_TIPO_LAYOUT) == 1 ? "Grade de Dados" : ((entity0.LX_TIPO_LAYOUT) == 3 ? "Grade de Dados à Esquerda/Formulário à Direita" : ((entity0.LX_TIPO_LAYOUT) == 5 ? "Grade de Dados à Direita/Formulário à Esquerda" : ((entity0.LX_TIPO_LAYOUT) == 4 ? "Grade de Dados em Cima/Formulário em Baixo" : "")))))))
                , MostraBotaoAdicao = entity0.MOSTRA_BOTAO_ADICAO
                , MostraBotaoEdicao = entity0.MOSTRA_BOTAO_EDICAO
                , MostraBotaoExclusao = entity0.MOSTRA_BOTAO_EXCLUSAO
                , MostraBotaoImpressao = entity0.MOSTRA_BOTAO_IMPRESSAO
                , MostraBotaoLayout = entity0.MOSTRA_BOTAO_LAYOUT
                , MostraBotaoLimpa = entity0.MOSTRA_BOTAO_LIMPA
                , MostraBotaoNavegacao = entity0.MOSTRA_BOTAO_NAVEGACAO
                , MostraBotaoPesquisa = entity0.MOSTRA_BOTAO_PESQUISA
                , MostraBotaoPesquisaEsp = entity0.MOSTRA_BOTAO_PESQUISA_ESP
                , PossuiToolbar = entity0.POSSUI_TOOLBAR
                , PossuiVisaoTabular = entity0.POSSUI_VISAO_TABULAR
                , PropriedadesDoDetalhe = entity0.PROPRIEDADES_DO_DETALHE
                , PropriedadesDoMestre = entity0.PROPRIEDADES_DO_MESTRE
                , UsaFiltrosDoBoPrincipal = entity0.USA_FILTROS_DO_BO_PRINCIPAL
                , Visivel = entity0.VISIVEL
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsTransacaoAutorizacaoByEntitySearchNoAssociations.
	    public IQueryable<TcsTransacaoAutorizacao> GetTcsTransacaoAutorizacaoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacaoAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_MODULO_AUTORIZACAO
                  let entity0Al2 = entity0.TCS_OBJETO_AUTORIZACAO
	            
	            	
	            select new TcsTransacaoAutorizacao()		
	            {
	            
                ClasseNome = entity0.CLASSE_NOME
                , CodTransacao = entity0.COD_TRANSACAO
                , DescModulo = entity0Al1.DESC_MODULO
                , DescObjeto = entity0Al2.DESC_OBJETO
                , DescTransacao = entity0.DESC_TRANSACAO
                , Icone = entity0.ICONE
                , IdModulo = entity0Al1.ID_MODULO
                , IdObjeto = entity0Al2.ID_OBJETO
                , IdTransacao = entity0.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , LxCorFundoName = ((entity0.LX_COR_FUNDO) == 8 ? "Fundo Laranja" : ((entity0.LX_COR_FUNDO) == 10 ? "Fundo Roxo" : ((entity0.LX_COR_FUNDO) == 7 ? "Laranja" : ((entity0.LX_COR_FUNDO) == 9 ? "Roxo" : ""))))
                , LxTipoTransacao = entity0.LX_TIPO_TRANSACAO
                , LxTipoTransacaoName = ((entity0.LX_TIPO_TRANSACAO) == 7 ? "Assistente" : ((entity0.LX_TIPO_TRANSACAO) == 8 ? "Dashboard" : ((entity0.LX_TIPO_TRANSACAO) == 2 ? "ERP" : ((entity0.LX_TIPO_TRANSACAO) == 6 ? "ERP App" : ((entity0.LX_TIPO_TRANSACAO) == 4 ? "Excel" : ((entity0.LX_TIPO_TRANSACAO) == 3 ? "Loja" : ((entity0.LX_TIPO_TRANSACAO) == 5 ? "Mobile" : ((entity0.LX_TIPO_TRANSACAO) == 1 ? "Todos" : ""))))))))
                , NomeCurto = entity0.NOME_CURTO
                , ObjetoClasseNome = entity0Al2.CLASSE_NOME
                , Tag = entity0.TAG
		
	            }
	            );
	
	        SetTcsTransacaoAutorizacaoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsTransacaoMenuAutorizacaoByEntitySearchNoAssociations.
	    public IQueryable<TcsTransacaoMenuAutorizacao> GetTcsTransacaoMenuAutorizacaoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacaoMenuAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoMenuAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_MENU_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al4 = entity0.TCS_TRANSACAO_AUTORIZACAO
                  let entity0Al2 = entity0.TCS_MODULO_MENU_AUTORIZACAO
                  let entity0Al1 = entity0.TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO
                  let entity0Al3 = entity0.TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO
	            
	            	
	            select new TcsTransacaoMenuAutorizacao()		
	            {
	            
                ClasseNome = entity0Al4.CLASSE_NOME
                , CodTransacao = entity0Al4.COD_TRANSACAO
                , DescModulo = entity0Al1.DESC_MODULO
                , DescModuloMenu = entity0Al2.DESC_MODULO_MENU
                , DescricaoAplicativo = entity0Al3.DESCRICAO_APLICATIVO
                , DescTransacao = entity0Al4.DESC_TRANSACAO
                , IdModulo = entity0Al1.ID_MODULO
                , IdModuloMenu = entity0Al2.ID_MODULO_MENU
                , IdTcsTransacaoMenuAutorizacao = entity0.ID_TCS_TRANSACAO_MENU_AUTORIZACAO
                , IdTransacao = entity0Al4.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , InativoModulo = entity0Al1.INATIVO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            );
	
	        SetTcsTransacaoMenuAutorizacaoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsTransacaoDependenteAutorizacaoByEntitySearchNoAssociations.
	    public IQueryable<TcsTransacaoDependenteAutorizacao> GetTcsTransacaoDependenteAutorizacaoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacaoDependenteAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoDependenteAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TRANSACAO_RELACIONADA
                  let entity0Al2 = entity0.TCS_TRANSACAO_AUTORIZACAO
	            
	            	
	            select new TcsTransacaoDependenteAutorizacao()		
	            {
	            
                ClasseNome = entity0Al1.CLASSE_NOME
                , CompartilhaBoPrincipal = entity0.COMPARTILHA_BO_PRINCIPAL
                , DescTransacao = entity0Al1.DESC_TRANSACAO
                , ExecutaPesquisa = entity0.EXECUTA_PESQUISA
                , IdTransacao = entity0Al2.ID_TRANSACAO
                , IdTransacaoRelacionada = entity0Al1.ID_TRANSACAO
                , IdTransacaoDependente = entity0.ID_TRANSACAO_DEPENDENTE
                , LxPosicaoDaTransacao = entity0.LX_POSICAO_DA_TRANSACAO
                , LxPosicaoDaTransacaoName = ((entity0.LX_POSICAO_DA_TRANSACAO) == 5 ? "Painel Inferior" : ((entity0.LX_POSICAO_DA_TRANSACAO) == 6 ? "Painel Flutuante" : ((entity0.LX_POSICAO_DA_TRANSACAO) == 2 ? "Painel à Esquerda" : ((entity0.LX_POSICAO_DA_TRANSACAO) == 1 ? "Página" : ((entity0.LX_POSICAO_DA_TRANSACAO) == 4 ? "Painel à Direita" : ((entity0.LX_POSICAO_DA_TRANSACAO) == 3 ? "Painel Superior" : ""))))))
                , LxTipoLayout = entity0.LX_TIPO_LAYOUT
                , LxTipoLayoutName = ((entity0.LX_TIPO_LAYOUT) == 6 ? "Grade de Dados em Baixo/Formulário em Cima" : ((entity0.LX_TIPO_LAYOUT) == 2 ? "Formulário" : ((entity0.LX_TIPO_LAYOUT) == 7 ? "Padrão" : ((entity0.LX_TIPO_LAYOUT) == 1 ? "Grade de Dados" : ((entity0.LX_TIPO_LAYOUT) == 3 ? "Grade de Dados à Esquerda/Formulário à Direita" : ((entity0.LX_TIPO_LAYOUT) == 5 ? "Grade de Dados à Direita/Formulário à Esquerda" : ((entity0.LX_TIPO_LAYOUT) == 4 ? "Grade de Dados em Cima/Formulário em Baixo" : "")))))))
                , MostraBotaoAdicao = entity0.MOSTRA_BOTAO_ADICAO
                , MostraBotaoEdicao = entity0.MOSTRA_BOTAO_EDICAO
                , MostraBotaoExclusao = entity0.MOSTRA_BOTAO_EXCLUSAO
                , MostraBotaoImpressao = entity0.MOSTRA_BOTAO_IMPRESSAO
                , MostraBotaoLayout = entity0.MOSTRA_BOTAO_LAYOUT
                , MostraBotaoLimpa = entity0.MOSTRA_BOTAO_LIMPA
                , MostraBotaoNavegacao = entity0.MOSTRA_BOTAO_NAVEGACAO
                , MostraBotaoPesquisa = entity0.MOSTRA_BOTAO_PESQUISA
                , MostraBotaoPesquisaEsp = entity0.MOSTRA_BOTAO_PESQUISA_ESP
                , PossuiToolbar = entity0.POSSUI_TOOLBAR
                , PossuiVisaoTabular = entity0.POSSUI_VISAO_TABULAR
                , PropriedadesDoDetalhe = entity0.PROPRIEDADES_DO_DETALHE
                , PropriedadesDoMestre = entity0.PROPRIEDADES_DO_MESTRE
                , UsaFiltrosDoBoPrincipal = entity0.USA_FILTROS_DO_BO_PRINCIPAL
                , Visivel = entity0.VISIVEL
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsTransacaoMenuAutorizacaoParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsTransacaoMenuAutorizacaoParentComposition> GetTcsTransacaoMenuAutorizacaoParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "TCS_TRANSACAO_AUTORIZACAO", "TCS_TRANSACAO_MENU_AUTORIZACAO", "TCS_TRANSACAO_AUTORIZACAO", typeof(TcsTransacaoMenuAutorizacaoParentComposition), typeof(TcsTransacaoDependenteAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoMenuAutorizacaoParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_MENU_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al4 = entity0.TCS_TRANSACAO_AUTORIZACAO
                  let entity0Al2 = entity0.TCS_MODULO_MENU_AUTORIZACAO
                  let entity0Al1 = entity0.TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO
                  let entity0Al3 = entity0.TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO
	            
	            	
	            select new TcsTransacaoMenuAutorizacaoParentComposition()		
	            {
	            
                ClasseNome = entity0Al4.CLASSE_NOME
                , CodTransacao = entity0Al4.COD_TRANSACAO
                , DescModulo = entity0Al1.DESC_MODULO
                , DescModuloMenu = entity0Al2.DESC_MODULO_MENU
                , DescricaoAplicativo = entity0Al3.DESCRICAO_APLICATIVO
                , DescTransacao = entity0Al4.DESC_TRANSACAO
                , IdModulo = entity0Al1.ID_MODULO
                , IdModuloMenu = entity0Al2.ID_MODULO_MENU
                , IdTcsTransacaoMenuAutorizacao = entity0.ID_TCS_TRANSACAO_MENU_AUTORIZACAO
                , IdTransacao = entity0Al4.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , InativoModulo = entity0Al1.INATIVO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
                //TcsTransacaoAutorizacao Properties.
                , DescObjeto = entity0.TCS_TRANSACAO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.DESC_OBJETO
                , Icone = entity0.TCS_TRANSACAO_AUTORIZACAO.ICONE
                , IdObjeto = entity0.TCS_TRANSACAO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.ID_OBJETO
                , LxCorFundo = entity0.TCS_TRANSACAO_AUTORIZACAO.LX_COR_FUNDO
                , LxCorFundoName = ((entity0.TCS_TRANSACAO_AUTORIZACAO.LX_COR_FUNDO) == 8 ? "Fundo Laranja" : ((entity0.TCS_TRANSACAO_AUTORIZACAO.LX_COR_FUNDO) == 10 ? "Fundo Roxo" : ((entity0.TCS_TRANSACAO_AUTORIZACAO.LX_COR_FUNDO) == 7 ? "Laranja" : ((entity0.TCS_TRANSACAO_AUTORIZACAO.LX_COR_FUNDO) == 9 ? "Roxo" : ""))))
                , LxTipoTransacao = entity0.TCS_TRANSACAO_AUTORIZACAO.LX_TIPO_TRANSACAO
                , LxTipoTransacaoName = ((entity0.TCS_TRANSACAO_AUTORIZACAO.LX_TIPO_TRANSACAO) == 7 ? "Assistente" : ((entity0.TCS_TRANSACAO_AUTORIZACAO.LX_TIPO_TRANSACAO) == 8 ? "Dashboard" : ((entity0.TCS_TRANSACAO_AUTORIZACAO.LX_TIPO_TRANSACAO) == 2 ? "ERP" : ((entity0.TCS_TRANSACAO_AUTORIZACAO.LX_TIPO_TRANSACAO) == 6 ? "ERP App" : ((entity0.TCS_TRANSACAO_AUTORIZACAO.LX_TIPO_TRANSACAO) == 4 ? "Excel" : ((entity0.TCS_TRANSACAO_AUTORIZACAO.LX_TIPO_TRANSACAO) == 3 ? "Loja" : ((entity0.TCS_TRANSACAO_AUTORIZACAO.LX_TIPO_TRANSACAO) == 5 ? "Mobile" : ((entity0.TCS_TRANSACAO_AUTORIZACAO.LX_TIPO_TRANSACAO) == 1 ? "Todos" : ""))))))))
                , NomeCurto = entity0.TCS_TRANSACAO_AUTORIZACAO.NOME_CURTO
                , ObjetoClasseNome = entity0.TCS_TRANSACAO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.CLASSE_NOME
                , Tag = entity0.TCS_TRANSACAO_AUTORIZACAO.TAG
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsTransacaoDependenteAutorizacaoParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsTransacaoDependenteAutorizacaoParentComposition> GetTcsTransacaoDependenteAutorizacaoParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "TCS_TRANSACAO_AUTORIZACAO", "TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO", "TCS_TRANSACAO_AUTORIZACAO", typeof(TcsTransacaoDependenteAutorizacaoParentComposition), typeof(TcsTransacaoMenuAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoDependenteAutorizacaoParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TRANSACAO_RELACIONADA
                  let entity0Al2 = entity0.TCS_TRANSACAO_AUTORIZACAO
	            
	            	
	            select new TcsTransacaoDependenteAutorizacaoParentComposition()		
	            {
	            
                ClasseNome = entity0Al1.CLASSE_NOME
                , CompartilhaBoPrincipal = entity0.COMPARTILHA_BO_PRINCIPAL
                , DescTransacao = entity0Al1.DESC_TRANSACAO
                , ExecutaPesquisa = entity0.EXECUTA_PESQUISA
                , IdTransacao = entity0Al2.ID_TRANSACAO
                , IdTransacaoRelacionada = entity0Al1.ID_TRANSACAO
                , IdTransacaoDependente = entity0.ID_TRANSACAO_DEPENDENTE
                , LxPosicaoDaTransacao = entity0.LX_POSICAO_DA_TRANSACAO
                , LxPosicaoDaTransacaoName = ((entity0.LX_POSICAO_DA_TRANSACAO) == 5 ? "Painel Inferior" : ((entity0.LX_POSICAO_DA_TRANSACAO) == 6 ? "Painel Flutuante" : ((entity0.LX_POSICAO_DA_TRANSACAO) == 2 ? "Painel à Esquerda" : ((entity0.LX_POSICAO_DA_TRANSACAO) == 1 ? "Página" : ((entity0.LX_POSICAO_DA_TRANSACAO) == 4 ? "Painel à Direita" : ((entity0.LX_POSICAO_DA_TRANSACAO) == 3 ? "Painel Superior" : ""))))))
                , LxTipoLayout = entity0.LX_TIPO_LAYOUT
                , LxTipoLayoutName = ((entity0.LX_TIPO_LAYOUT) == 6 ? "Grade de Dados em Baixo/Formulário em Cima" : ((entity0.LX_TIPO_LAYOUT) == 2 ? "Formulário" : ((entity0.LX_TIPO_LAYOUT) == 7 ? "Padrão" : ((entity0.LX_TIPO_LAYOUT) == 1 ? "Grade de Dados" : ((entity0.LX_TIPO_LAYOUT) == 3 ? "Grade de Dados à Esquerda/Formulário à Direita" : ((entity0.LX_TIPO_LAYOUT) == 5 ? "Grade de Dados à Direita/Formulário à Esquerda" : ((entity0.LX_TIPO_LAYOUT) == 4 ? "Grade de Dados em Cima/Formulário em Baixo" : "")))))))
                , MostraBotaoAdicao = entity0.MOSTRA_BOTAO_ADICAO
                , MostraBotaoEdicao = entity0.MOSTRA_BOTAO_EDICAO
                , MostraBotaoExclusao = entity0.MOSTRA_BOTAO_EXCLUSAO
                , MostraBotaoImpressao = entity0.MOSTRA_BOTAO_IMPRESSAO
                , MostraBotaoLayout = entity0.MOSTRA_BOTAO_LAYOUT
                , MostraBotaoLimpa = entity0.MOSTRA_BOTAO_LIMPA
                , MostraBotaoNavegacao = entity0.MOSTRA_BOTAO_NAVEGACAO
                , MostraBotaoPesquisa = entity0.MOSTRA_BOTAO_PESQUISA
                , MostraBotaoPesquisaEsp = entity0.MOSTRA_BOTAO_PESQUISA_ESP
                , PossuiToolbar = entity0.POSSUI_TOOLBAR
                , PossuiVisaoTabular = entity0.POSSUI_VISAO_TABULAR
                , PropriedadesDoDetalhe = entity0.PROPRIEDADES_DO_DETALHE
                , PropriedadesDoMestre = entity0.PROPRIEDADES_DO_MESTRE
                , UsaFiltrosDoBoPrincipal = entity0.USA_FILTROS_DO_BO_PRINCIPAL
                , Visivel = entity0.VISIVEL
                //TcsTransacaoAutorizacao Properties.
                , CodTransacao = entity0.TCS_TRANSACAO_AUTORIZACAO.COD_TRANSACAO
                , DescModulo = entity0.TCS_TRANSACAO_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.DESC_MODULO
                , DescObjeto = entity0.TCS_TRANSACAO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.DESC_OBJETO
                , Icone = entity0.TCS_TRANSACAO_AUTORIZACAO.ICONE
                , IdModulo = entity0.TCS_TRANSACAO_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.ID_MODULO
                , IdObjeto = entity0.TCS_TRANSACAO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.ID_OBJETO
                , Inativo = entity0.TCS_TRANSACAO_AUTORIZACAO.INATIVO
                , LxCorFundo = entity0.TCS_TRANSACAO_AUTORIZACAO.LX_COR_FUNDO
                , LxCorFundoName = ((entity0.TCS_TRANSACAO_AUTORIZACAO.LX_COR_FUNDO) == 8 ? "Fundo Laranja" : ((entity0.TCS_TRANSACAO_AUTORIZACAO.LX_COR_FUNDO) == 10 ? "Fundo Roxo" : ((entity0.TCS_TRANSACAO_AUTORIZACAO.LX_COR_FUNDO) == 7 ? "Laranja" : ((entity0.TCS_TRANSACAO_AUTORIZACAO.LX_COR_FUNDO) == 9 ? "Roxo" : ""))))
                , LxTipoTransacao = entity0.TCS_TRANSACAO_AUTORIZACAO.LX_TIPO_TRANSACAO
                , LxTipoTransacaoName = ((entity0.TCS_TRANSACAO_AUTORIZACAO.LX_TIPO_TRANSACAO) == 7 ? "Assistente" : ((entity0.TCS_TRANSACAO_AUTORIZACAO.LX_TIPO_TRANSACAO) == 8 ? "Dashboard" : ((entity0.TCS_TRANSACAO_AUTORIZACAO.LX_TIPO_TRANSACAO) == 2 ? "ERP" : ((entity0.TCS_TRANSACAO_AUTORIZACAO.LX_TIPO_TRANSACAO) == 6 ? "ERP App" : ((entity0.TCS_TRANSACAO_AUTORIZACAO.LX_TIPO_TRANSACAO) == 4 ? "Excel" : ((entity0.TCS_TRANSACAO_AUTORIZACAO.LX_TIPO_TRANSACAO) == 3 ? "Loja" : ((entity0.TCS_TRANSACAO_AUTORIZACAO.LX_TIPO_TRANSACAO) == 5 ? "Mobile" : ((entity0.TCS_TRANSACAO_AUTORIZACAO.LX_TIPO_TRANSACAO) == 1 ? "Todos" : ""))))))))
                , NomeCurto = entity0.TCS_TRANSACAO_AUTORIZACAO.NOME_CURTO
                , ObjetoClasseNome = entity0.TCS_TRANSACAO_AUTORIZACAO.TCS_OBJETO_AUTORIZACAO.CLASSE_NOME
                , Tag = entity0.TCS_TRANSACAO_AUTORIZACAO.TAG
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
	
	    [Ignore()]
	    private void SetTcsTransacaoAutorizacaoBusinessFilter(ref IQueryable<TcsTransacaoAutorizacao> query, List<EntitySearch> entitySearchList)
	    {
	    		int idxElement;
	    		string operatorValue;
	    		object value;
	    		//Get query by functions
	    		if (entitySearchList.Count > 0)
	    		{
	    			foreach (EntitySearch search in entitySearchList.Where(e => e.EntityName == "TcsTransacaoAutorizacao"))
	    			{

	
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "LxCorFundo" || e.Value.ToString() == "TCS_TRANSACAO_AUTORIZACAO.LX_COR_FUNDO")))
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
	    										System.Nullable<int> tmpLxCorFundo1 = (System.Nullable<int>)value;
	    										query = from r in query where r.LxCorFundo == tmpLxCorFundo1 select r;
	    										break;
	    									case "!=":
	    										System.Nullable<int> tmpLxCorFundo2 = (System.Nullable<int>)value;
	    										query = from r in query where r.LxCorFundo != tmpLxCorFundo2 select r;
	    										break;

	
	    									case "<":
	    										System.Nullable<int> tmpLxCorFundo3 = (System.Nullable<int>)value;
	    										query = from r in query where r.LxCorFundo < tmpLxCorFundo3 select r;
	    										break;
	    									case "<=":
	    										System.Nullable<int> tmpLxCorFundo4 = (System.Nullable<int>)value;
	    										query = from r in query where r.LxCorFundo <= tmpLxCorFundo4 select r;
	    										break;
	    									case ">":
	    										System.Nullable<int> tmpLxCorFundo5 = (System.Nullable<int>)value;
	    										query = from r in query where r.LxCorFundo > tmpLxCorFundo5 select r;
	    										break;
	    									case ">=":
	    										System.Nullable<int> tmpLxCorFundo6 = (System.Nullable<int>)value;
	    										query = from r in query where r.LxCorFundo >= tmpLxCorFundo6 select r;
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
	    private void SetTcsTransacaoMenuAutorizacaoBusinessFilter(ref IQueryable<TcsTransacaoMenuAutorizacao> query, List<EntitySearch> entitySearchList)
	    {
	    		int idxElement;
	    		string operatorValue;
	    		object value;
	    		//Get query by functions
	    		if (entitySearchList.Count > 0)
	    		{
	    			foreach (EntitySearch search in entitySearchList.Where(e => e.EntityName == "TcsTransacaoMenuAutorizacao"))
	    			{

	
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "ClasseNome" || e.Value.ToString() == "TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.CLASSE_NOME")))
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
	    										System.String tmpClasseNome1 = (System.String)value;
	    										query = from r in query where r.ClasseNome == tmpClasseNome1 select r;
	    										break;
	    									case "!=":
	    										System.String tmpClasseNome2 = (System.String)value;
	    										query = from r in query where r.ClasseNome != tmpClasseNome2 select r;
	    										break;

	
	    									case "Contains":
	    										System.String tmpClasseNome7 = (System.String)value;
	    									    query = from r in query where r.ClasseNome.Contains(tmpClasseNome7) select r;
	    									    break;
	    									case "StartsWith":
	    										System.String tmpClasseNome8 = (System.String)value;
	    									    query = from r in query where r.ClasseNome.StartsWith(tmpClasseNome8) select r;
	    									    break;
	    									case "EndsWith":
	    										System.String tmpClasseNome9 = (System.String)value;
	    									    query = from r in query where r.ClasseNome.EndsWith(tmpClasseNome9) select r;
	    									    break;

	
	    									default:
	    										break;
	    								}                                
	    							}
	    						}
        					} 

    
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "CodTransacao" || e.Value.ToString() == "TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.COD_TRANSACAO")))
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
	    										string tmpCodTransacao1 = (string)value;
	    										query = from r in query where r.CodTransacao == tmpCodTransacao1 select r;
	    										break;
	    									case "!=":
	    										string tmpCodTransacao2 = (string)value;
	    										query = from r in query where r.CodTransacao != tmpCodTransacao2 select r;
	    										break;

	
	    									case "Contains":
	    										string tmpCodTransacao7 = (string)value;
	    									    query = from r in query where r.CodTransacao.Contains(tmpCodTransacao7) select r;
	    									    break;
	    									case "StartsWith":
	    										string tmpCodTransacao8 = (string)value;
	    									    query = from r in query where r.CodTransacao.StartsWith(tmpCodTransacao8) select r;
	    									    break;
	    									case "EndsWith":
	    										string tmpCodTransacao9 = (string)value;
	    									    query = from r in query where r.CodTransacao.EndsWith(tmpCodTransacao9) select r;
	    									    break;

	
	    									default:
	    										break;
	    								}                                
	    							}
	    						}
        					} 

    
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "DescTransacao" || e.Value.ToString() == "TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.DESC_TRANSACAO")))
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
	    										System.String tmpDescTransacao1 = (System.String)value;
	    										query = from r in query where r.DescTransacao == tmpDescTransacao1 select r;
	    										break;
	    									case "!=":
	    										System.String tmpDescTransacao2 = (System.String)value;
	    										query = from r in query where r.DescTransacao != tmpDescTransacao2 select r;
	    										break;

	
	    									case "Contains":
	    										System.String tmpDescTransacao7 = (System.String)value;
	    									    query = from r in query where r.DescTransacao.Contains(tmpDescTransacao7) select r;
	    									    break;
	    									case "StartsWith":
	    										System.String tmpDescTransacao8 = (System.String)value;
	    									    query = from r in query where r.DescTransacao.StartsWith(tmpDescTransacao8) select r;
	    									    break;
	    									case "EndsWith":
	    										System.String tmpDescTransacao9 = (System.String)value;
	    									    query = from r in query where r.DescTransacao.EndsWith(tmpDescTransacao9) select r;
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
	    //Get PagedTcsTransacaoAutorizacao.
	    public IQueryable<TcsTransacaoAutorizacao> GetPagedTcsTransacaoAutorizacao(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacaoAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_MODULO_AUTORIZACAO
                  let entity0Al2 = entity0.TCS_OBJETO_AUTORIZACAO
                orderby entity0.ID_TRANSACAO ascending
	            
	            	
	            select new TcsTransacaoAutorizacao()		
	            {
	            
                ClasseNome = entity0.CLASSE_NOME
                , CodTransacao = entity0.COD_TRANSACAO
                , DescModulo = entity0Al1.DESC_MODULO
                , DescObjeto = entity0Al2.DESC_OBJETO
                , DescTransacao = entity0.DESC_TRANSACAO
                , Icone = entity0.ICONE
                , IdModulo = entity0Al1.ID_MODULO
                , IdObjeto = entity0Al2.ID_OBJETO
                , IdTransacao = entity0.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , LxCorFundoName = ((entity0.LX_COR_FUNDO) == 8 ? "Fundo Laranja" : ((entity0.LX_COR_FUNDO) == 10 ? "Fundo Roxo" : ((entity0.LX_COR_FUNDO) == 7 ? "Laranja" : ((entity0.LX_COR_FUNDO) == 9 ? "Roxo" : ""))))
                , LxTipoTransacao = entity0.LX_TIPO_TRANSACAO
                , LxTipoTransacaoName = ((entity0.LX_TIPO_TRANSACAO) == 7 ? "Assistente" : ((entity0.LX_TIPO_TRANSACAO) == 8 ? "Dashboard" : ((entity0.LX_TIPO_TRANSACAO) == 2 ? "ERP" : ((entity0.LX_TIPO_TRANSACAO) == 6 ? "ERP App" : ((entity0.LX_TIPO_TRANSACAO) == 4 ? "Excel" : ((entity0.LX_TIPO_TRANSACAO) == 3 ? "Loja" : ((entity0.LX_TIPO_TRANSACAO) == 5 ? "Mobile" : ((entity0.LX_TIPO_TRANSACAO) == 1 ? "Todos" : ""))))))))
                , NomeCurto = entity0.NOME_CURTO
                , ObjetoClasseNome = entity0Al2.CLASSE_NOME
                , Tag = entity0.TAG
		
	            }
	            ).Skip(skip).Take(take);
	
	        SetTcsTransacaoAutorizacaoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsTransacaoMenuAutorizacao.
	    public IQueryable<TcsTransacaoMenuAutorizacao> GetPagedTcsTransacaoMenuAutorizacao(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacaoMenuAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoMenuAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_MENU_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al4 = entity0.TCS_TRANSACAO_AUTORIZACAO
                  let entity0Al2 = entity0.TCS_MODULO_MENU_AUTORIZACAO
                  let entity0Al1 = entity0.TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO
                  let entity0Al3 = entity0.TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO
                orderby entity0.ID_TCS_TRANSACAO_MENU_AUTORIZACAO ascending
	            
	            	
	            select new TcsTransacaoMenuAutorizacao()		
	            {
	            
                ClasseNome = entity0Al4.CLASSE_NOME
                , CodTransacao = entity0Al4.COD_TRANSACAO
                , DescModulo = entity0Al1.DESC_MODULO
                , DescModuloMenu = entity0Al2.DESC_MODULO_MENU
                , DescricaoAplicativo = entity0Al3.DESCRICAO_APLICATIVO
                , DescTransacao = entity0Al4.DESC_TRANSACAO
                , IdModulo = entity0Al1.ID_MODULO
                , IdModuloMenu = entity0Al2.ID_MODULO_MENU
                , IdTcsTransacaoMenuAutorizacao = entity0.ID_TCS_TRANSACAO_MENU_AUTORIZACAO
                , IdTransacao = entity0Al4.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , InativoModulo = entity0Al1.INATIVO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            ).Skip(skip).Take(take);
	
	        SetTcsTransacaoMenuAutorizacaoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsTransacaoDependenteAutorizacao.
	    public IQueryable<TcsTransacaoDependenteAutorizacao> GetPagedTcsTransacaoDependenteAutorizacao(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacaoDependenteAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoDependenteAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TRANSACAO_RELACIONADA
                  let entity0Al2 = entity0.TCS_TRANSACAO_AUTORIZACAO
                orderby entity0.ID_TRANSACAO_DEPENDENTE ascending
	            
	            	
	            select new TcsTransacaoDependenteAutorizacao()		
	            {
	            
                ClasseNome = entity0Al1.CLASSE_NOME
                , CompartilhaBoPrincipal = entity0.COMPARTILHA_BO_PRINCIPAL
                , DescTransacao = entity0Al1.DESC_TRANSACAO
                , ExecutaPesquisa = entity0.EXECUTA_PESQUISA
                , IdTransacao = entity0Al2.ID_TRANSACAO
                , IdTransacaoRelacionada = entity0Al1.ID_TRANSACAO
                , IdTransacaoDependente = entity0.ID_TRANSACAO_DEPENDENTE
                , LxPosicaoDaTransacao = entity0.LX_POSICAO_DA_TRANSACAO
                , LxPosicaoDaTransacaoName = ((entity0.LX_POSICAO_DA_TRANSACAO) == 5 ? "Painel Inferior" : ((entity0.LX_POSICAO_DA_TRANSACAO) == 6 ? "Painel Flutuante" : ((entity0.LX_POSICAO_DA_TRANSACAO) == 2 ? "Painel à Esquerda" : ((entity0.LX_POSICAO_DA_TRANSACAO) == 1 ? "Página" : ((entity0.LX_POSICAO_DA_TRANSACAO) == 4 ? "Painel à Direita" : ((entity0.LX_POSICAO_DA_TRANSACAO) == 3 ? "Painel Superior" : ""))))))
                , LxTipoLayout = entity0.LX_TIPO_LAYOUT
                , LxTipoLayoutName = ((entity0.LX_TIPO_LAYOUT) == 6 ? "Grade de Dados em Baixo/Formulário em Cima" : ((entity0.LX_TIPO_LAYOUT) == 2 ? "Formulário" : ((entity0.LX_TIPO_LAYOUT) == 7 ? "Padrão" : ((entity0.LX_TIPO_LAYOUT) == 1 ? "Grade de Dados" : ((entity0.LX_TIPO_LAYOUT) == 3 ? "Grade de Dados à Esquerda/Formulário à Direita" : ((entity0.LX_TIPO_LAYOUT) == 5 ? "Grade de Dados à Direita/Formulário à Esquerda" : ((entity0.LX_TIPO_LAYOUT) == 4 ? "Grade de Dados em Cima/Formulário em Baixo" : "")))))))
                , MostraBotaoAdicao = entity0.MOSTRA_BOTAO_ADICAO
                , MostraBotaoEdicao = entity0.MOSTRA_BOTAO_EDICAO
                , MostraBotaoExclusao = entity0.MOSTRA_BOTAO_EXCLUSAO
                , MostraBotaoImpressao = entity0.MOSTRA_BOTAO_IMPRESSAO
                , MostraBotaoLayout = entity0.MOSTRA_BOTAO_LAYOUT
                , MostraBotaoLimpa = entity0.MOSTRA_BOTAO_LIMPA
                , MostraBotaoNavegacao = entity0.MOSTRA_BOTAO_NAVEGACAO
                , MostraBotaoPesquisa = entity0.MOSTRA_BOTAO_PESQUISA
                , MostraBotaoPesquisaEsp = entity0.MOSTRA_BOTAO_PESQUISA_ESP
                , PossuiToolbar = entity0.POSSUI_TOOLBAR
                , PossuiVisaoTabular = entity0.POSSUI_VISAO_TABULAR
                , PropriedadesDoDetalhe = entity0.PROPRIEDADES_DO_DETALHE
                , PropriedadesDoMestre = entity0.PROPRIEDADES_DO_MESTRE
                , UsaFiltrosDoBoPrincipal = entity0.USA_FILTROS_DO_BO_PRINCIPAL
                , Visivel = entity0.VISIVEL
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsTransacaoAutorizacaoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacaoAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_TRANSACAO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_MODULO_AUTORIZACAO
                  let entityAl2 = entity.TCS_OBJETO_AUTORIZACAO
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsTransacaoMenuAutorizacaoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacaoMenuAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_TRANSACAO_MENU_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entityAl4 = entity.TCS_TRANSACAO_AUTORIZACAO
                  let entityAl2 = entity.TCS_MODULO_MENU_AUTORIZACAO
                  let entityAl1 = entity.TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO
                  let entityAl3 = entity.TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsTransacaoDependenteAutorizacaoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacaoDependenteAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_TRANSACAO_DEPENDENTE_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TRANSACAO_RELACIONADA
                  let entityAl2 = entity.TCS_TRANSACAO_AUTORIZACAO
	            
	            select 1
	            ).Count();	
		
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    
	    [Update()]	
	    //Update TcsTransacaoAutorizacao.
	    public void UpdateTcsTransacaoAutorizacao(TcsTransacaoAutorizacao entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsTransacaoAutorizacao.
	    public void InsertTcsTransacaoAutorizacao(TcsTransacaoAutorizacao entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsTransacaoAutorizacao.
	    public void DeleteTcsTransacaoAutorizacao(TcsTransacaoAutorizacao entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsTransacaoMenuAutorizacao.
	    public void UpdateTcsTransacaoMenuAutorizacao(TcsTransacaoMenuAutorizacao entity)
	    {



	
	        if (entity.TcsTransacaoAutorizacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsTransacaoAutorizacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsTransacaoAutorizacao); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsTransacaoMenuAutorizacao.
	    public void InsertTcsTransacaoMenuAutorizacao(TcsTransacaoMenuAutorizacao entity)
	    {



	
	        if (entity.TcsTransacaoAutorizacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsTransacaoAutorizacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsTransacaoAutorizacao);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsTransacaoMenuAutorizacao.
	    public void DeleteTcsTransacaoMenuAutorizacao(TcsTransacaoMenuAutorizacao entity)
	    {



	
	        if (entity.TcsTransacaoAutorizacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsTransacaoAutorizacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsTransacaoAutorizacao);
	            

	
	        }

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsTransacaoDependenteAutorizacao.
	    public void UpdateTcsTransacaoDependenteAutorizacao(TcsTransacaoDependenteAutorizacao entity)
	    {



	
	        if (entity.TcsTransacaoAutorizacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsTransacaoAutorizacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsTransacaoAutorizacao); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsTransacaoDependenteAutorizacao.
	    public void InsertTcsTransacaoDependenteAutorizacao(TcsTransacaoDependenteAutorizacao entity)
	    {



	
	        if (entity.TcsTransacaoAutorizacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsTransacaoAutorizacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsTransacaoAutorizacao);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsTransacaoDependenteAutorizacao.
	    public void DeleteTcsTransacaoDependenteAutorizacao(TcsTransacaoDependenteAutorizacao entity)
	    {



	
	        if (entity.TcsTransacaoAutorizacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsTransacaoAutorizacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsTransacaoAutorizacao);
	            

	
	        }

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}