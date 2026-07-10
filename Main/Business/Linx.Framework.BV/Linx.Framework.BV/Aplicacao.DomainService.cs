					
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

namespace Linx.Framework.BV.Aplicacao
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_APLICACAO.ID_APLICACAO", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsAplicacao,TcsAplicacao.TcsAplicacaoVersaoHistorico,TcsAplicacao.TcsAmbiente];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdAplicacao];ReadOnly[false];Entities[TCS_APLICACAO:IdAplicacao|TCS_APLICATIVO:IdTcsAplicativo];SubQueryInfo[];EdmEntityName[TCS_APLICACAO];EntityRelations[TCS_APLICATIVO(TCS_APLICATIVO)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsAplicacao")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Aplicacao.TcsAplicacao")]
	public partial class TcsAplicacao : Linx.Data.Entity
	{

	
		
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.TcsAplicacaoVersaoHistoricoList != null && this.TcsAplicacaoVersaoHistoricoList.Count() > 0)
	      {
	         foreach (var entity in this.TcsAplicacaoVersaoHistoricoList)
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
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.TcsAplicacaoVersaoHistoricoList != null)
	      {
	         foreach (var detail in this.TcsAplicacaoVersaoHistoricoList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsAplicacaoVersaoHistoricoList = null;
	      }
	      if (this.TcsAmbienteList != null)
	      {
	         foreach (var detail in this.TcsAmbienteList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsAmbienteList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(AplicacaoDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("TcsAplicacaoVersaoHistorico"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsAplicacaoVersaoHistorico");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdAplicacao"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdAplicacao));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsAplicacaoVersaoHistorico and all sub-details
	         if (this.TcsAplicacaoVersaoHistoricoList == null || this.TcsAplicacaoVersaoHistoricoList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsAplicacaoVersaoHistoricoList = context.GetPagedTcsAplicacaoVersaoHistorico(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsAplicacaoVersaoHistoricoList = (from r in context.GetTcsAplicacaoVersaoHistoricoByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	      if (viewNames == null || viewNames.Contains("TcsAmbiente"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsAmbiente");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdAplicacao"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdAplicacao));
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
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _TcsAplicacaoVersaoHistoricoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsAplicacaoVersaoHistorico && ((TcsAplicacaoVersaoHistorico)e.Entity).TcsAplicacao == null && e.Associations == null && e.OriginalAssociations == null && ((TcsAplicacaoVersaoHistorico)e.Entity).IdAplicacao == this.IdAplicacao).ToList();
 	      if (_TcsAplicacaoVersaoHistoricoElements.Count > 0 && this.TcsAplicacaoVersaoHistoricoList.Count() == 0)
 	      {
 	          this.TcsAplicacaoVersaoHistoricoList = _TcsAplicacaoVersaoHistoricoElements.Select(e => (TcsAplicacaoVersaoHistorico)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsAplicacaoVersaoHistoricoElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsAplicacaoVersaoHistorico)detail.Entity).TcsAplicacao = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsAplicacao", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsAplicacaoVersaoHistoricoList", indexDetails.ToArray());
 	      }
 
 	      var _TcsAmbienteElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsAmbiente && ((TcsAmbiente)e.Entity).TcsAplicacao == null && e.Associations == null && e.OriginalAssociations == null && ((TcsAmbiente)e.Entity).IdAplicacao == this.IdAplicacao).ToList();
 	      if (_TcsAmbienteElements.Count > 0 && this.TcsAmbienteList.Count() == 0)
 	      {
 	          this.TcsAmbienteList = _TcsAmbienteElements.Select(e => (TcsAmbiente)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsAmbienteElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsAmbiente)detail.Entity).TcsAplicacao = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsAplicacao", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsAmbienteList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For DescricaoAplicacao
	    partial void OnDescricaoAplicacaoChanging(System.String value);
	    partial void OnDescricaoAplicacaoChanged();

	    private System.String _DescricaoAplicacao;

	    [DataMember(IsRequired = true, Name = "DescricaoAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[UidAplicacao];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_APLICACAO.DESCRICAO_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICACAO.DESCRICAO_APLICACAO")]
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
	    //Extensibility Partial Method Definitions For DescricaoAplicativo
	    partial void OnDescricaoAplicativoChanging(System.String value);
	    partial void OnDescricaoAplicativoChanged();

	    private System.String _DescricaoAplicativo;

	    [DataMember(Name = "DescricaoAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicativo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicativo];LookUpTitle[Seleção de (Aplicativo)];LookUpQuery[executeLookUpTcsAplicativo];LookUpFinalize[finalizeLookUpTcsAplicativo];LookUpDisplayColumns[{\"DescricaoAplicativo\" : \"Aplicativo\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\"}];LookUpColumns[{\"DescricaoAplicativo\" : true, \"IdTcsAplicativo\" : false}];FilterDataKey[TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoAplicativo#false##250:0##Aplicativo#0#true##::LookUpTcsAplicativo##false#false#TCS_APLICATIVO#TCS_APLICATIVO#Linx.Framework.BV.Aplicacao#IQueryable###true#false", EdmKey="TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO")]
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
	    //Extensibility Partial Method Definitions For EmDesenvolvimento
	    partial void OnEmDesenvolvimentoChanging(Boolean value);
	    partial void OnEmDesenvolvimentoChanged();

	    private Boolean _EmDesenvolvimento;

	    [DataMember(IsRequired = true, Name = "EmDesenvolvimento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Em Desenvolvimento", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_APLICACAO.EM_DESENVOLVIMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICACAO.EM_DESENVOLVIMENTO")]
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
	    //Extensibility Partial Method Definitions For IdAplicacao
	    partial void OnIdAplicacaoChanging(Int32 value);
	    partial void OnIdAplicacaoChanged();

	    private Int32 _IdAplicacao;

	    [DataMember(IsRequired = true, Name = "IdAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicação", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_APLICACAO.ID_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICACAO.ID_APLICACAO")]
	    public Int32 IdAplicacao
	    {
	    	    get
	    	    {
	    	          return _IdAplicacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdAplicacao != value)
	    	          {
	    	              this.ValidateProperty("IdAplicacao", value);
	    	              this.OnIdAplicacaoChanging(value);
	    	              this.RaiseDataMemberChanging("IdAplicacao");
	    	              this._IdAplicacao = value;
	    	              this.RaiseDataMemberChanged("IdAplicacao");
	    	              this.OnIdAplicacaoChanged();
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
	    [Display(Name = "Id Tcs Aplicativo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicativo];LookUpTitle[Seleção de (Id Tcs Aplicativo)];LookUpQuery[executeLookUpTcsAplicativo];LookUpFinalize[finalizeLookUpTcsAplicativo];LookUpDisplayColumns[{\"DescricaoAplicativo\" : \"Aplicativo\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\"}];LookUpColumns[{\"DescricaoAplicativo\" : true, \"IdTcsAplicativo\" : false}];FilterDataKey[TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdTcsAplicativo#true##12:0##Id Tcs Aplicativo#1#false##::LookUpTcsAplicativo##false#false#TCS_APLICATIVO#TCS_APLICATIVO#Linx.Framework.BV.Aplicacao#IQueryable###true#false", EdmKey="TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO")]
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
	    //Extensibility Partial Method Definitions For UidAplicacao
	    partial void OnUidAplicacaoChanging(System.Guid value);
	    partial void OnUidAplicacaoChanged();

	    private System.Guid _UidAplicacao;

	    [DataMember(Name = "UidAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicação", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_APLICACAO.UID_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICACAO.UID_APLICACAO")]
	    public System.Guid UidAplicacao
	    {
	    	    get
	    	    {
	    	          return _UidAplicacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidAplicacao != value)
	    	          {
	    	              this.ValidateProperty("UidAplicacao", value);
	    	              this.OnUidAplicacaoChanging(value);
	    	              this.RaiseDataMemberChanging("UidAplicacao");
	    	              this._UidAplicacao = value;
	    	              this.RaiseDataMemberChanged("UidAplicacao");
	    	              this.OnUidAplicacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Url
	    partial void OnUrlChanging(System.String value);
	    partial void OnUrlChanged();

	    private System.String _Url;

	    [DataMember(Name = "Url", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Url", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_APLICACAO.URL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICACAO.URL")]
	    public System.String Url
	    {
	    	    get
	    	    {
	    	          return _Url;
	    	    }
	    	    set
	    	    {
	    	          if (this._Url != value)
	    	          {
	    	              this.ValidateProperty("Url", value);
	    	              this.OnUrlChanging(value);
	    	              this.RaiseDataMemberChanging("Url");
	    	              this._Url = value;
	    	              this.RaiseDataMemberChanged("Url");
	    	              this.OnUrlChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UrlWorkArea
	    partial void OnUrlWorkAreaChanging(System.String value);
	    partial void OnUrlWorkAreaChanged();

	    private System.String _UrlWorkArea;

	    [DataMember(Name = "UrlWorkArea", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Url Work Area", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_APLICACAO.URL_WORK_AREA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICACAO.URL_WORK_AREA")]
	    public System.String UrlWorkArea
	    {
	    	    get
	    	    {
	    	          return _UrlWorkArea;
	    	    }
	    	    set
	    	    {
	    	          if (this._UrlWorkArea != value)
	    	          {
	    	              this.ValidateProperty("UrlWorkArea", value);
	    	              this.OnUrlWorkAreaChanging(value);
	    	              this.RaiseDataMemberChanging("UrlWorkArea");
	    	              this._UrlWorkArea = value;
	    	              this.RaiseDataMemberChanged("UrlWorkArea");
	    	              this.OnUrlWorkAreaChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIdAplicacao;
	    [DataMember(Name = "TemporaryIdAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicação (Tmp)", Description="Temporary Key", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdAplicacao
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdAplicacao.IsNullOrEmpty())
	    	                this._TemporaryIdAplicacao = this._IdAplicacao;
	    	          return this._TemporaryIdAplicacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdAplicacao != value)
	    	              this._TemporaryIdAplicacao = value;
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
	    [Association("FK_TcsAplicacao_TcsAmbiente", "IdAplicacao", "IdAplicacao", IsForeignKey=false)]
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
		
	    private IEnumerable<TcsAplicacaoVersaoHistorico> _TcsAplicacaoVersaoHistoricoList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsAplicacao_TcsAplicacaoVersaoHistorico", "IdAplicacao", "IdAplicacao", IsForeignKey=false)]
	    [DataMember(Name = "TcsAplicacaoVersaoHistoricoList", EmitDefaultValue = true)]
	    public IEnumerable<TcsAplicacaoVersaoHistorico> TcsAplicacaoVersaoHistoricoList
	    {
	        get
	        {
	
	            if (this._TcsAplicacaoVersaoHistoricoList == null)
	            	this._TcsAplicacaoVersaoHistoricoList = new List<TcsAplicacaoVersaoHistorico>();
	
	            return this._TcsAplicacaoVersaoHistoricoList;
	        }
	        set
	        {
	            if (this._TcsAplicacaoVersaoHistoricoList != value)
	            {
	                this._TcsAplicacaoVersaoHistoricoList = value;
	                this.RaisePropertyChanged("TcsAplicacaoVersaoHistoricoList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_APLICACAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_APLICACAO), QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICACAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_APLICACAO.URL", Source = "Url", Target = "URL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICACAO", RelationPropertyName = "TCS_APLICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_APLICACAO.ID_APLICACAO", Source = "IdAplicacao", Target = "ID_APLICACAO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICACAO", RelationPropertyName = "TCS_APLICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_APLICACAO.UID_APLICACAO", Source = "UidAplicacao", Target = "UID_APLICACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICACAO", RelationPropertyName = "TCS_APLICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_APLICACAO.URL_WORK_AREA", Source = "UrlWorkArea", Target = "URL_WORK_AREA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICACAO", RelationPropertyName = "TCS_APLICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_APLICACAO.EM_DESENVOLVIMENTO", Source = "EmDesenvolvimento", Target = "EM_DESENVOLVIMENTO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICACAO", RelationPropertyName = "TCS_APLICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_APLICACAO.DESCRICAO_APLICACAO", Source = "DescricaoAplicacao", Target = "DESCRICAO_APLICACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICACAO", RelationPropertyName = "TCS_APLICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO", Source = "IdTcsAplicativo", Target = "ID_TCS_APLICATIVO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICATIVO", RelationPropertyName = "TCS_APLICATIVO" });

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

		

	[LinxPublicationView(PrimaryKeys="TCS_APLICACAO_VERSAO_HISTORICO.ID_TCS_APLICACAO_VERSAO_HISTORICO", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Versão Histórico];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsAplicacaoVersaoHistorico];ReadOnly[false];Entities[TCS_APLICACAO_VERSAO_HISTORICO:IdTcsAplicacaoVersaoHistorico];SubQueryInfo[Select 1 From #ParentAlias#.TCS_APLICACAO_VERSAO_HISTORICO_LISTA as #Alias#];EdmEntityName[TCS_APLICACAO_VERSAO_HISTORICO];EntityRelations[TCS_APLICACAO(TCS_APLICACAO)#TCS_APLICATIVO(TCS_APLICATIVO)];EdmParentEntityName[TCS_APLICACAO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsAplicacaoVersaoHistorico")]
	[Serializable()]
	public partial class TcsAplicacaoVersaoHistorico : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(AplicacaoDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsAplicacao");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdAplicacao"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdAplicacao));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsAplicacao
	         this.TcsAplicacao = (from r in context.GetTcsAplicacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For DataAtualizacao
	    partial void OnDataAtualizacaoChanging(System.DateTime value);
	    partial void OnDataAtualizacaoChanged();

	    private System.DateTime _DataAtualizacao;

	    [DataMember(IsRequired = true, Name = "DataAtualizacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Data Atualização", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_APLICACAO_VERSAO_HISTORICO.DATA_ATUALIZACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICACAO_VERSAO_HISTORICO.DATA_ATUALIZACAO")]
	    public System.DateTime DataAtualizacao
	    {
	    	    get
	    	    {
	    	          return _DataAtualizacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataAtualizacao != value)
	    	          {
	    	              this.ValidateProperty("DataAtualizacao", value);
	    	              this.OnDataAtualizacaoChanging(value);
	    	              this.RaiseDataMemberChanging("DataAtualizacao");
	    	              this._DataAtualizacao = value;
	    	              this.RaiseDataMemberChanged("DataAtualizacao");
	    	              this.OnDataAtualizacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdAplicacao
	    partial void OnIdAplicacaoChanging(Int32 value);
	    partial void OnIdAplicacaoChanged();

	    private Int32 _IdAplicacao;

	    [DataMember(IsRequired = true, Name = "IdAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicação", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_APLICACAO_VERSAO_HISTORICO.TCS_APLICACAO.ID_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICACAO_VERSAO_HISTORICO.TCS_APLICACAO.ID_APLICACAO")]
	    public Int32 IdAplicacao
	    {
	    	    get
	    	    {
	    	          return _IdAplicacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdAplicacao != value)
	    	          {
	    	              this.ValidateProperty("IdAplicacao", value);
	    	              this.OnIdAplicacaoChanging(value);
	    	              this.RaiseDataMemberChanging("IdAplicacao");
	    	              this._IdAplicacao = value;
	    	              this.RaiseDataMemberChanged("IdAplicacao");
	    	              this.OnIdAplicacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsAplicacaoVersaoHistorico
	    partial void OnIdTcsAplicacaoVersaoHistoricoChanging(Int32 value);
	    partial void OnIdTcsAplicacaoVersaoHistoricoChanged();

	    private Int32 _IdTcsAplicacaoVersaoHistorico;

	    [DataMember(IsRequired = true, Name = "IdTcsAplicacaoVersaoHistorico", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Aplicacao Versao Historico", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_APLICACAO_VERSAO_HISTORICO.ID_TCS_APLICACAO_VERSAO_HISTORICO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICACAO_VERSAO_HISTORICO.ID_TCS_APLICACAO_VERSAO_HISTORICO")]
	    public Int32 IdTcsAplicacaoVersaoHistorico
	    {
	    	    get
	    	    {
	    	          return _IdTcsAplicacaoVersaoHistorico;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAplicacaoVersaoHistorico != value)
	    	          {
	    	              this.ValidateProperty("IdTcsAplicacaoVersaoHistorico", value);
	    	              this.OnIdTcsAplicacaoVersaoHistoricoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsAplicacaoVersaoHistorico");
	    	              this._IdTcsAplicacaoVersaoHistorico = value;
	    	              this.RaiseDataMemberChanged("IdTcsAplicacaoVersaoHistorico");
	    	              this.OnIdTcsAplicacaoVersaoHistoricoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Versao
	    partial void OnVersaoChanging(System.String value);
	    partial void OnVersaoChanged();

	    private System.String _Versao;

	    [DataMember(IsRequired = true, Name = "Versao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Versão", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(25)]
	    [FunctionalPoint("Precision[25:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_APLICACAO_VERSAO_HISTORICO.VERSAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICACAO_VERSAO_HISTORICO.VERSAO")]
	    public System.String Versao
	    {
	    	    get
	    	    {
	    	          return _Versao;
	    	    }
	    	    set
	    	    {
	    	          if (this._Versao != value)
	    	          {
	    	              this.ValidateProperty("Versao", value);
	    	              this.OnVersaoChanging(value);
	    	              this.RaiseDataMemberChanging("Versao");
	    	              this._Versao = value;
	    	              this.RaiseDataMemberChanged("Versao");
	    	              this.OnVersaoChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIdTcsAplicacaoVersaoHistorico;
	    [DataMember(Name = "TemporaryIdTcsAplicacaoVersaoHistorico", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Aplicacao Versao Historico (Tmp)", Description="Temporary Key", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdTcsAplicacaoVersaoHistorico
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTcsAplicacaoVersaoHistorico.IsNullOrEmpty())
	    	                this._TemporaryIdTcsAplicacaoVersaoHistorico = this._IdTcsAplicacaoVersaoHistorico;
	    	          return this._TemporaryIdTcsAplicacaoVersaoHistorico;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTcsAplicacaoVersaoHistorico != value)
	    	              this._TemporaryIdTcsAplicacaoVersaoHistorico = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsAplicacao _TcsAplicacao;
	    [DataMember(Name = "TcsAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsAplicacao_TcsAplicacaoVersaoHistorico", "IdAplicacao", "IdAplicacao", IsForeignKey=true)]
	    public TcsAplicacao TcsAplicacao
	    {
	        get
	        {
	            return this._TcsAplicacao;
	        }
	        set
	        {
	            if (this._TcsAplicacao != value)
	            {
	                this._TcsAplicacao = value;
	                this.RaisePropertyChanged("TcsAplicacaoList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_APLICACAO_VERSAO_HISTORICO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_APLICACAO_VERSAO_HISTORICO), QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICACAO_VERSAO_HISTORICO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_APLICACAO_VERSAO_HISTORICO.VERSAO", Source = "Versao", Target = "VERSAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICACAO_VERSAO_HISTORICO", RelationPropertyName = "TCS_APLICACAO_VERSAO_HISTORICO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_APLICACAO_VERSAO_HISTORICO.DATA_ATUALIZACAO", Source = "DataAtualizacao", Target = "DATA_ATUALIZACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICACAO_VERSAO_HISTORICO", RelationPropertyName = "TCS_APLICACAO_VERSAO_HISTORICO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_APLICACAO_VERSAO_HISTORICO.TCS_APLICACAO.ID_APLICACAO", Source = "IdAplicacao", Target = "ID_APLICACAO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICACAO", RelationPropertyName = "TCS_APLICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_APLICACAO_VERSAO_HISTORICO.ID_TCS_APLICACAO_VERSAO_HISTORICO", Source = "IdTcsAplicacaoVersaoHistorico", Target = "ID_TCS_APLICACAO_VERSAO_HISTORICO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICACAO_VERSAO_HISTORICO", RelationPropertyName = "TCS_APLICACAO_VERSAO_HISTORICO" });

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
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Ambientes];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsAmbiente];ReadOnly[false];Entities[TCS_AMBIENTE:IdTcsAmbiente|TCS_EMPRESA_AUTENTICACAO:IdLinx];SubQueryInfo[Select 1 From #ParentAlias#.TCS_AMBIENTE_LISTA as #Alias#];EdmEntityName[TCS_AMBIENTE];EntityRelations[TCS_APLICACAO(TCS_APLICACAO)#TCS_APLICATIVO(TCS_APLICATIVO)#TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)];EdmParentEntityName[TCS_APLICACAO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsAmbiente")]
	[Serializable()]
	public partial class TcsAmbiente : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(AplicacaoDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsAplicacao");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdAplicacao"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdAplicacao));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsAplicacao
	         this.TcsAplicacao = (from r in context.GetTcsAplicacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoAmbiente#false##2500##Ambiente#0#true##::LookUpTcsAmbiente##false#false##TCS_AMBIENTE#Linx.Framework.BV.Aplicacao#IQueryable###true#false", EdmKey="TCS_AMBIENTE.DESCRICAO_AMBIENTE")]
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
	    //Extensibility Partial Method Definitions For IdAplicacao
	    partial void OnIdAplicacaoChanging(Int32 value);
	    partial void OnIdAplicacaoChanged();

	    private Int32 _IdAplicacao;

	    [DataMember(IsRequired = true, Name = "IdAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicação", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_AMBIENTE.TCS_APLICACAO.ID_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.TCS_APLICACAO.ID_APLICACAO")]
	    public Int32 IdAplicacao
	    {
	    	    get
	    	    {
	    	          return _IdAplicacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdAplicacao != value)
	    	          {
	    	              this.ValidateProperty("IdAplicacao", value);
	    	              this.OnIdAplicacaoChanging(value);
	    	              this.RaiseDataMemberChanging("IdAplicacao");
	    	              this._IdAplicacao = value;
	    	              this.RaiseDataMemberChanged("IdAplicacao");
	    	              this.OnIdAplicacaoChanged();
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
	    [Display(Name = "Id Linx", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsEmpresaAutenticacao];LookUpTitle[Seleção de (Id Linx)];LookUpQuery[executeLookUpTcsEmpresaAutenticacao];LookUpFinalize[finalizeLookUpTcsEmpresaAutenticacao];LookUpDisplayColumns[{\"IdLinx\" : \"Id Linx\", \"NomeEmpresa\" : \"Empresa\"}];LookUpColumns[{\"IdLinx\" : false, \"NomeEmpresa\" : true}];FilterDataKey[TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdLinx#true##12:0##Id Linx#0#false##::LookUpTcsEmpresaAutenticacao##false#false##TCS_EMPRESA_AUTENTICACAO#Linx.Framework.BV.Aplicacao#IQueryable###true#false", EdmKey="TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
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
	    [Display(Name = "ID Ambiente", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente];LookUpTitle[Seleção de (ID Ambiente)];LookUpQuery[executeLookUpTcsAmbiente];LookUpFinalize[finalizeLookUpTcsAmbiente];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"IdTcsAmbiente\" : false}];FilterDataKey[TCS_AMBIENTE.ID_TCS_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdTcsAmbiente#true##12:0##Id Tcs Ambiente#1#false##::LookUpTcsAmbiente##false#false##TCS_AMBIENTE#Linx.Framework.BV.Aplicacao#IQueryable###true#false", EdmKey="TCS_AMBIENTE.ID_TCS_AMBIENTE")]
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
	    //Extensibility Partial Method Definitions For NomeEmpresa
	    partial void OnNomeEmpresaChanging(System.String value);
	    partial void OnNomeEmpresaChanged();

	    private System.String _NomeEmpresa;

	    [DataMember(IsRequired = true, Name = "NomeEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Empresa", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsEmpresaAutenticacao];LookUpTitle[Seleção de (Empresa)];LookUpQuery[executeLookUpTcsEmpresaAutenticacao];LookUpFinalize[finalizeLookUpTcsEmpresaAutenticacao];LookUpDisplayColumns[{\"IdLinx\" : \"Id Linx\", \"NomeEmpresa\" : \"Empresa\"}];LookUpColumns[{\"IdLinx\" : false, \"NomeEmpresa\" : true}];FilterDataKey[TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeEmpresa#false##2500##Empresa#1#true##::LookUpTcsEmpresaAutenticacao##false#false##TCS_EMPRESA_AUTENTICACAO#Linx.Framework.BV.Aplicacao#IQueryable###true#false", EdmKey="TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
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

	    private Int32 _TemporaryIdTcsAmbiente;
	    [DataMember(Name = "TemporaryIdTcsAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Ambiente (Tmp)", Description="Temporary Key", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
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
	 
	    private TcsAplicacao _TcsAplicacao;
	    [DataMember(Name = "TcsAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsAplicacao_TcsAmbiente", "IdAplicacao", "IdAplicacao", IsForeignKey=true)]
	    public TcsAplicacao TcsAplicacao
	    {
	        get
	        {
	            return this._TcsAplicacao;
	        }
	        set
	        {
	            if (this._TcsAplicacao != value)
	            {
	                this._TcsAplicacao = value;
	                this.RaisePropertyChanged("TcsAplicacaoList");
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
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_AMBIENTE.TCS_APLICACAO.ID_APLICACAO", Source = "IdAplicacao", Target = "ID_APLICACAO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICACAO", RelationPropertyName = "TCS_APLICACAO" });
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

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Versão Histórico];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsAplicacaoVersaoHistorico];ReadOnly[false];Entities[TCS_APLICACAO_VERSAO_HISTORICO:IdTcsAplicacaoVersaoHistorico];SubQueryInfo[Select 1 From #ParentAlias#.TCS_APLICACAO_VERSAO_HISTORICO_LISTA as #Alias#];EdmEntityName[TCS_APLICACAO_VERSAO_HISTORICO];EntityRelations[TCS_APLICACAO(TCS_APLICACAO)#TCS_APLICATIVO(TCS_APLICATIVO)];EdmParentEntityName[TCS_APLICACAO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsAplicacaoVersaoHistorico")]
	[Serializable()]
	public partial class TcsAplicacaoVersaoHistoricoParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For DataAtualizacao
	    partial void OnDataAtualizacaoChanging(System.DateTime value);
	    partial void OnDataAtualizacaoChanged();

	    private System.DateTime _DataAtualizacao;

	    [DataMember(IsRequired = true, Name = "DataAtualizacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Data Atualização", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_APLICACAO_VERSAO_HISTORICO.DATA_ATUALIZACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICACAO_VERSAO_HISTORICO.DATA_ATUALIZACAO")]
	    public System.DateTime DataAtualizacao
	    {
	    	    get
	    	    {
	    	          return _DataAtualizacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataAtualizacao != value)
	    	          {
	    	              this.ValidateProperty("DataAtualizacao", value);
	    	              this.OnDataAtualizacaoChanging(value);
	    	              this.RaiseDataMemberChanging("DataAtualizacao");
	    	              this._DataAtualizacao = value;
	    	              this.RaiseDataMemberChanged("DataAtualizacao");
	    	              this.OnDataAtualizacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdAplicacao
	    partial void OnIdAplicacaoChanging(Int32 value);
	    partial void OnIdAplicacaoChanged();

	    private Int32 _IdAplicacao;

	    [DataMember(IsRequired = true, Name = "IdAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicação", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_APLICACAO_VERSAO_HISTORICO.TCS_APLICACAO.ID_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICACAO_VERSAO_HISTORICO.TCS_APLICACAO.ID_APLICACAO")]
	    public Int32 IdAplicacao
	    {
	    	    get
	    	    {
	    	          return _IdAplicacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdAplicacao != value)
	    	          {
	    	              this.ValidateProperty("IdAplicacao", value);
	    	              this.OnIdAplicacaoChanging(value);
	    	              this.RaiseDataMemberChanging("IdAplicacao");
	    	              this._IdAplicacao = value;
	    	              this.RaiseDataMemberChanged("IdAplicacao");
	    	              this.OnIdAplicacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsAplicacaoVersaoHistorico
	    partial void OnIdTcsAplicacaoVersaoHistoricoChanging(Int32 value);
	    partial void OnIdTcsAplicacaoVersaoHistoricoChanged();

	    private Int32 _IdTcsAplicacaoVersaoHistorico;

	    [DataMember(IsRequired = true, Name = "IdTcsAplicacaoVersaoHistorico", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Aplicacao Versao Historico", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_APLICACAO_VERSAO_HISTORICO.ID_TCS_APLICACAO_VERSAO_HISTORICO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICACAO_VERSAO_HISTORICO.ID_TCS_APLICACAO_VERSAO_HISTORICO")]
	    public Int32 IdTcsAplicacaoVersaoHistorico
	    {
	    	    get
	    	    {
	    	          return _IdTcsAplicacaoVersaoHistorico;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAplicacaoVersaoHistorico != value)
	    	          {
	    	              this.ValidateProperty("IdTcsAplicacaoVersaoHistorico", value);
	    	              this.OnIdTcsAplicacaoVersaoHistoricoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsAplicacaoVersaoHistorico");
	    	              this._IdTcsAplicacaoVersaoHistorico = value;
	    	              this.RaiseDataMemberChanged("IdTcsAplicacaoVersaoHistorico");
	    	              this.OnIdTcsAplicacaoVersaoHistoricoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Versao
	    partial void OnVersaoChanging(System.String value);
	    partial void OnVersaoChanged();

	    private System.String _Versao;

	    [DataMember(IsRequired = true, Name = "Versao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Versão", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(25)]
	    [FunctionalPoint("Precision[25:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_APLICACAO_VERSAO_HISTORICO.VERSAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICACAO_VERSAO_HISTORICO.VERSAO")]
	    public System.String Versao
	    {
	    	    get
	    	    {
	    	          return _Versao;
	    	    }
	    	    set
	    	    {
	    	          if (this._Versao != value)
	    	          {
	    	              this.ValidateProperty("Versao", value);
	    	              this.OnVersaoChanging(value);
	    	              this.RaiseDataMemberChanging("Versao");
	    	              this._Versao = value;
	    	              this.RaiseDataMemberChanged("Versao");
	    	              this.OnVersaoChanged();
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
	    [Display(Name = "", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[UidAplicacao];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_APLICACAO_VERSAO_HISTORICO.TCS_APLICACAO.DESCRICAO_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICACAO.DESCRICAO_APLICACAO")]
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
	    //Extensibility Partial Method Definitions For DescricaoAplicativo
	    partial void OnDescricaoAplicativoChanging(System.String value);
	    partial void OnDescricaoAplicativoChanged();

	    private System.String _DescricaoAplicativo;

	    [DataMember(Name = "DescricaoAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicativo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_APLICACAO_VERSAO_HISTORICO.TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO")]
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
	    //Extensibility Partial Method Definitions For EmDesenvolvimento
	    partial void OnEmDesenvolvimentoChanging(Boolean value);
	    partial void OnEmDesenvolvimentoChanged();

	    private Boolean _EmDesenvolvimento;

	    [DataMember(IsRequired = true, Name = "EmDesenvolvimento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Em Desenvolvimento", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_APLICACAO_VERSAO_HISTORICO.TCS_APLICACAO.EM_DESENVOLVIMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICACAO.EM_DESENVOLVIMENTO")]
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
	    //Extensibility Partial Method Definitions For IdTcsAplicativo
	    partial void OnIdTcsAplicativoChanging(Int32 value);
	    partial void OnIdTcsAplicativoChanged();

	    private Int32 _IdTcsAplicativo;

	    [DataMember(IsRequired = true, Name = "IdTcsAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Aplicativo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_APLICACAO_VERSAO_HISTORICO.TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO")]
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
	    //Extensibility Partial Method Definitions For UidAplicacao
	    partial void OnUidAplicacaoChanging(System.Guid value);
	    partial void OnUidAplicacaoChanged();

	    private System.Guid _UidAplicacao;

	    [DataMember(Name = "UidAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicação", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_APLICACAO_VERSAO_HISTORICO.TCS_APLICACAO.UID_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICACAO.UID_APLICACAO")]
	    public System.Guid UidAplicacao
	    {
	    	    get
	    	    {
	    	          return _UidAplicacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidAplicacao != value)
	    	          {
	    	              this.ValidateProperty("UidAplicacao", value);
	    	              this.OnUidAplicacaoChanging(value);
	    	              this.RaiseDataMemberChanging("UidAplicacao");
	    	              this._UidAplicacao = value;
	    	              this.RaiseDataMemberChanged("UidAplicacao");
	    	              this.OnUidAplicacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Url
	    partial void OnUrlChanging(System.String value);
	    partial void OnUrlChanged();

	    private System.String _Url;

	    [DataMember(Name = "Url", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Url", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_APLICACAO_VERSAO_HISTORICO.TCS_APLICACAO.URL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICACAO.URL")]
	    public System.String Url
	    {
	    	    get
	    	    {
	    	          return _Url;
	    	    }
	    	    set
	    	    {
	    	          if (this._Url != value)
	    	          {
	    	              this.ValidateProperty("Url", value);
	    	              this.OnUrlChanging(value);
	    	              this.RaiseDataMemberChanging("Url");
	    	              this._Url = value;
	    	              this.RaiseDataMemberChanged("Url");
	    	              this.OnUrlChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UrlWorkArea
	    partial void OnUrlWorkAreaChanging(System.String value);
	    partial void OnUrlWorkAreaChanged();

	    private System.String _UrlWorkArea;

	    [DataMember(Name = "UrlWorkArea", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Url Work Area", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_APLICACAO_VERSAO_HISTORICO.TCS_APLICACAO.URL_WORK_AREA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICACAO.URL_WORK_AREA")]
	    public System.String UrlWorkArea
	    {
	    	    get
	    	    {
	    	          return _UrlWorkArea;
	    	    }
	    	    set
	    	    {
	    	          if (this._UrlWorkArea != value)
	    	          {
	    	              this.ValidateProperty("UrlWorkArea", value);
	    	              this.OnUrlWorkAreaChanging(value);
	    	              this.RaiseDataMemberChanging("UrlWorkArea");
	    	              this._UrlWorkArea = value;
	    	              this.RaiseDataMemberChanged("UrlWorkArea");
	    	              this.OnUrlWorkAreaChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_APLICACAO_VERSAO_HISTORICO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_APLICACAO_VERSAO_HISTORICO), QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICACAO_VERSAO_HISTORICO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_APLICACAO_VERSAO_HISTORICO.VERSAO", Source = "Versao", Target = "VERSAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICACAO_VERSAO_HISTORICO", RelationPropertyName = "TCS_APLICACAO_VERSAO_HISTORICO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_APLICACAO_VERSAO_HISTORICO.DATA_ATUALIZACAO", Source = "DataAtualizacao", Target = "DATA_ATUALIZACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICACAO_VERSAO_HISTORICO", RelationPropertyName = "TCS_APLICACAO_VERSAO_HISTORICO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_APLICACAO_VERSAO_HISTORICO.TCS_APLICACAO.ID_APLICACAO", Source = "IdAplicacao", Target = "ID_APLICACAO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICACAO", RelationPropertyName = "TCS_APLICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_APLICACAO_VERSAO_HISTORICO.ID_TCS_APLICACAO_VERSAO_HISTORICO", Source = "IdTcsAplicacaoVersaoHistorico", Target = "ID_TCS_APLICACAO_VERSAO_HISTORICO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICACAO_VERSAO_HISTORICO", RelationPropertyName = "TCS_APLICACAO_VERSAO_HISTORICO" });

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

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Ambientes];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsAmbiente];ReadOnly[false];Entities[TCS_AMBIENTE:IdTcsAmbiente|TCS_EMPRESA_AUTENTICACAO:IdLinx];SubQueryInfo[Select 1 From #ParentAlias#.TCS_AMBIENTE_LISTA as #Alias#];EdmEntityName[TCS_AMBIENTE];EntityRelations[TCS_APLICACAO(TCS_APLICACAO)#TCS_APLICATIVO(TCS_APLICATIVO)#TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)];EdmParentEntityName[TCS_APLICACAO];IsIQueryable[true]")]
		
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
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoAmbiente#false##2500##Ambiente#0#true##::LookUpTcsAmbiente##false#false##TCS_AMBIENTE#Linx.Framework.BV.Aplicacao#IQueryable###true#false", EdmKey="TCS_AMBIENTE.DESCRICAO_AMBIENTE")]
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
	    //Extensibility Partial Method Definitions For IdAplicacao
	    partial void OnIdAplicacaoChanging(Int32 value);
	    partial void OnIdAplicacaoChanged();

	    private Int32 _IdAplicacao;

	    [DataMember(IsRequired = true, Name = "IdAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicação", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_AMBIENTE.TCS_APLICACAO.ID_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.TCS_APLICACAO.ID_APLICACAO")]
	    public Int32 IdAplicacao
	    {
	    	    get
	    	    {
	    	          return _IdAplicacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdAplicacao != value)
	    	          {
	    	              this.ValidateProperty("IdAplicacao", value);
	    	              this.OnIdAplicacaoChanging(value);
	    	              this.RaiseDataMemberChanging("IdAplicacao");
	    	              this._IdAplicacao = value;
	    	              this.RaiseDataMemberChanged("IdAplicacao");
	    	              this.OnIdAplicacaoChanged();
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
	    [Display(Name = "Id Linx", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsEmpresaAutenticacao];LookUpTitle[Seleção de (Id Linx)];LookUpQuery[executeLookUpTcsEmpresaAutenticacao];LookUpFinalize[finalizeLookUpTcsEmpresaAutenticacao];LookUpDisplayColumns[{\"IdLinx\" : \"Id Linx\", \"NomeEmpresa\" : \"Empresa\"}];LookUpColumns[{\"IdLinx\" : false, \"NomeEmpresa\" : true}];FilterDataKey[TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdLinx#true##12:0##Id Linx#0#false##::LookUpTcsEmpresaAutenticacao##false#false##TCS_EMPRESA_AUTENTICACAO#Linx.Framework.BV.Aplicacao#IQueryable###true#false", EdmKey="TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
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
	    [Display(Name = "ID Ambiente", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente];LookUpTitle[Seleção de (ID Ambiente)];LookUpQuery[executeLookUpTcsAmbiente];LookUpFinalize[finalizeLookUpTcsAmbiente];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"IdTcsAmbiente\" : false}];FilterDataKey[TCS_AMBIENTE.ID_TCS_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdTcsAmbiente#true##12:0##Id Tcs Ambiente#1#false##::LookUpTcsAmbiente##false#false##TCS_AMBIENTE#Linx.Framework.BV.Aplicacao#IQueryable###true#false", EdmKey="TCS_AMBIENTE.ID_TCS_AMBIENTE")]
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
	    //Extensibility Partial Method Definitions For NomeEmpresa
	    partial void OnNomeEmpresaChanging(System.String value);
	    partial void OnNomeEmpresaChanged();

	    private System.String _NomeEmpresa;

	    [DataMember(IsRequired = true, Name = "NomeEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Empresa", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsEmpresaAutenticacao];LookUpTitle[Seleção de (Empresa)];LookUpQuery[executeLookUpTcsEmpresaAutenticacao];LookUpFinalize[finalizeLookUpTcsEmpresaAutenticacao];LookUpDisplayColumns[{\"IdLinx\" : \"Id Linx\", \"NomeEmpresa\" : \"Empresa\"}];LookUpColumns[{\"IdLinx\" : false, \"NomeEmpresa\" : true}];FilterDataKey[TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeEmpresa#false##2500##Empresa#1#true##::LookUpTcsEmpresaAutenticacao##false#false##TCS_EMPRESA_AUTENTICACAO#Linx.Framework.BV.Aplicacao#IQueryable###true#false", EdmKey="TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
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
	    //Extensibility Partial Method Definitions For DescricaoAplicacao
	    partial void OnDescricaoAplicacaoChanging(System.String value);
	    partial void OnDescricaoAplicacaoChanged();

	    private System.String _DescricaoAplicacao;

	    [DataMember(IsRequired = true, Name = "DescricaoAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[UidAplicacao];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_AMBIENTE.TCS_APLICACAO.DESCRICAO_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICACAO.DESCRICAO_APLICACAO")]
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
	    //Extensibility Partial Method Definitions For DescricaoAplicativo
	    partial void OnDescricaoAplicativoChanging(System.String value);
	    partial void OnDescricaoAplicativoChanged();

	    private System.String _DescricaoAplicativo;

	    [DataMember(Name = "DescricaoAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicativo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO")]
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
	    //Extensibility Partial Method Definitions For EmDesenvolvimento
	    partial void OnEmDesenvolvimentoChanging(Boolean value);
	    partial void OnEmDesenvolvimentoChanged();

	    private Boolean _EmDesenvolvimento;

	    [DataMember(IsRequired = true, Name = "EmDesenvolvimento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Em Desenvolvimento", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_AMBIENTE.TCS_APLICACAO.EM_DESENVOLVIMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICACAO.EM_DESENVOLVIMENTO")]
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
	    //Extensibility Partial Method Definitions For IdTcsAplicativo
	    partial void OnIdTcsAplicativoChanging(Int32 value);
	    partial void OnIdTcsAplicativoChanged();

	    private Int32 _IdTcsAplicativo;

	    [DataMember(IsRequired = true, Name = "IdTcsAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Aplicativo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO")]
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
	    //Extensibility Partial Method Definitions For UidAplicacao
	    partial void OnUidAplicacaoChanging(System.Guid value);
	    partial void OnUidAplicacaoChanged();

	    private System.Guid _UidAplicacao;

	    [DataMember(Name = "UidAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicação", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_AMBIENTE.TCS_APLICACAO.UID_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICACAO.UID_APLICACAO")]
	    public System.Guid UidAplicacao
	    {
	    	    get
	    	    {
	    	          return _UidAplicacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidAplicacao != value)
	    	          {
	    	              this.ValidateProperty("UidAplicacao", value);
	    	              this.OnUidAplicacaoChanging(value);
	    	              this.RaiseDataMemberChanging("UidAplicacao");
	    	              this._UidAplicacao = value;
	    	              this.RaiseDataMemberChanged("UidAplicacao");
	    	              this.OnUidAplicacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Url
	    partial void OnUrlChanging(System.String value);
	    partial void OnUrlChanged();

	    private System.String _Url;

	    [DataMember(Name = "Url", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Url", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_AMBIENTE.TCS_APLICACAO.URL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICACAO.URL")]
	    public System.String Url
	    {
	    	    get
	    	    {
	    	          return _Url;
	    	    }
	    	    set
	    	    {
	    	          if (this._Url != value)
	    	          {
	    	              this.ValidateProperty("Url", value);
	    	              this.OnUrlChanging(value);
	    	              this.RaiseDataMemberChanging("Url");
	    	              this._Url = value;
	    	              this.RaiseDataMemberChanged("Url");
	    	              this.OnUrlChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UrlWorkArea
	    partial void OnUrlWorkAreaChanging(System.String value);
	    partial void OnUrlWorkAreaChanged();

	    private System.String _UrlWorkArea;

	    [DataMember(Name = "UrlWorkArea", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Url Work Area", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_AMBIENTE.TCS_APLICACAO.URL_WORK_AREA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_APLICACAO.URL_WORK_AREA")]
	    public System.String UrlWorkArea
	    {
	    	    get
	    	    {
	    	          return _UrlWorkArea;
	    	    }
	    	    set
	    	    {
	    	          if (this._UrlWorkArea != value)
	    	          {
	    	              this.ValidateProperty("UrlWorkArea", value);
	    	              this.OnUrlWorkAreaChanging(value);
	    	              this.RaiseDataMemberChanging("UrlWorkArea");
	    	              this._UrlWorkArea = value;
	    	              this.RaiseDataMemberChanged("UrlWorkArea");
	    	              this.OnUrlWorkAreaChanged();
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
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_AMBIENTE.TCS_APLICACAO.ID_APLICACAO", Source = "IdAplicacao", Target = "ID_APLICACAO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICACAO", RelationPropertyName = "TCS_APLICACAO" });
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
	//////////////////////// DomainService Class V1 ///////////////////////
	///////////////////////////////////////////////////////////////////////
	[EnableClientAccess()]	
	[DomainIdentifier("ProcessorOverviewAplicacaoDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class AplicacaoDomainService : DomainService, IDataServiceContext 
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

		
	    public AplicacaoDomainService() : this("", null, null) { }
	    public AplicacaoDomainService(string connectionString) : this(connectionString, null, null) { }
	    public AplicacaoDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public AplicacaoDomainService(Linx.Framework.Autorizacao.BM.AutorizacaoContext dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public AplicacaoDomainService(string connectionString, Linx.Framework.Autorizacao.BM.AutorizacaoContext dataContext, Dictionary<string, string> headers) : base() 
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
	
	    
	        foreach (ChangeSetEntry entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsAplicacao))
	        {
	            ((TcsAplicacao)entry.Entity).OnSavingChanges(this, changeSet.GetChangeOperation(entry.Entity));
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
 	        var _TcsAplicacaoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsAplicacao && e.Entity.GetType().Name == "TcsAplicacao" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _TcsAplicacaoElements)
 	           if (((TcsAplicacao)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 
 	        //Remove inconsistent details
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsAplicacaoVersaoHistorico && e.Entity.GetType().Name == "TcsAplicacaoVersaoHistorico" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsAmbiente && e.Entity.GetType().Name == "TcsAmbiente" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
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
	    //Get All LookUpTcsAplicativo.
	    public IQueryable<LookUpTcsAplicativo> GetAllLookUpTcsAplicativo()
	    {
	        return this.GetLookUpTcsAplicativo(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsAplicativo By EntitySearch.
	    public IQueryable<LookUpTcsAplicativo> GetLookUpTcsAplicativoByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsAplicativo(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsAplicativo.
	    public IQueryable<LookUpTcsAplicativo> GetLookUpTcsAplicativo(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TCS_APLICATIVO" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsAplicativo";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsAplicativo));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsAplicativo> query =  
	
	            (from entity in this.DbContext.TCS_APLICATIVO.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpTcsAplicativo()		
	            {
	            
                DescricaoAplicativo = entity.DESCRICAO_APLICATIVO
                , IdTcsAplicativo = entity.ID_TCS_APLICATIVO
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
	    //Get All LookUpTcsEmpresaAutenticacao.
	    public IQueryable<LookUpTcsEmpresaAutenticacao> GetAllLookUpTcsEmpresaAutenticacao()
	    {
	        return this.GetLookUpTcsEmpresaAutenticacao(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsEmpresaAutenticacao By EntitySearch.
	    public IQueryable<LookUpTcsEmpresaAutenticacao> GetLookUpTcsEmpresaAutenticacaoByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsEmpresaAutenticacao(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsEmpresaAutenticacao.
	    public IQueryable<LookUpTcsEmpresaAutenticacao> GetLookUpTcsEmpresaAutenticacao(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TCS_EMPRESA_AUTENTICACAO" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsEmpresaAutenticacao";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsEmpresaAutenticacao));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsEmpresaAutenticacao> query =  
	
	            (from entity in this.DbContext.TCS_EMPRESA_AUTENTICACAO.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpTcsEmpresaAutenticacao()		
	            {
	            
                IdLinx = entity.ID_LINX
                , NomeEmpresa = entity.NOME_EMPRESA
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
	
		

	        if (entityName.InList("Linx.Framework.BV.Aplicacao.TcsAplicacao"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsAplicacao",
	        			NameSpace = "Linx.Framework.BV.Aplicacao",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsAplicacao",
	        			ClearMethodName = "ClearTcsAplicacao",
	        			QueryMethodName  = "GetPagedTcsAplicacao",	
	        			CountingMethodName  = "GetTcsAplicacao" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Aplicacao.TcsAplicacao"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Aplicacao.TcsAplicacao"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Aplicacao.TcsAplicacao", "Linx.Framework.BV.Aplicacao.TcsAplicacaoVersaoHistorico"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsAplicacaoVersaoHistorico" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.Aplicacao",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsAplicacao",	
	        			DisplayName = "Versão Histórico",
	        			ClearMethodName = "ClearTcsAplicacaoVersaoHistorico" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsAplicacaoVersaoHistorico" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsAplicacaoVersaoHistorico" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Aplicacao.TcsAplicacaoVersaoHistorico"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Aplicacao.TcsAplicacaoVersaoHistorico" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Aplicacao.TcsAplicacao", "Linx.Framework.BV.Aplicacao.TcsAmbiente"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsAmbiente" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.Aplicacao",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsAplicacao",	
	        			DisplayName = "Ambientes",
	        			ClearMethodName = "ClearTcsAmbiente" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsAmbiente" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsAmbiente" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Aplicacao.TcsAmbiente"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Aplicacao.TcsAmbiente" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
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

         		    return new string[] { "Framework_AplicacaoClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.AplicacaoClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Framework_aplicacaoService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.aplicacaoService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear TcsAplicacao.
	    public IEnumerable<TcsAplicacao> ClearTcsAplicacao()
	    {
	        List<TcsAplicacao> result = new List<TcsAplicacao>();
	        result.Add(new TcsAplicacao());	
			
	        result[0].TcsAplicacaoVersaoHistoricoList = new List<TcsAplicacaoVersaoHistorico>();
	        ((List<TcsAplicacaoVersaoHistorico>)result[0].TcsAplicacaoVersaoHistoricoList).Add(new TcsAplicacaoVersaoHistorico());
			
	        result[0].TcsAmbienteList = new List<TcsAmbiente>();
	        ((List<TcsAmbiente>)result[0].TcsAmbienteList).Add(new TcsAmbiente());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsAplicacaoVersaoHistorico.
	    public IEnumerable<TcsAplicacaoVersaoHistorico> ClearTcsAplicacaoVersaoHistorico()
	    {
	        List<TcsAplicacaoVersaoHistorico> result = new List<TcsAplicacaoVersaoHistorico>();
	        result.Add(new TcsAplicacaoVersaoHistorico());	
		
	        

	
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
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsAplicacao.
	    public IQueryable<TcsAplicacao> GetTcsAplicacao()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsAplicacao> result = 
	            (from entity0 in this.DbContext.TCS_APLICACAO
                  let entity0Al1 = entity0.TCS_APLICATIVO
	            
	            	
	            select new TcsAplicacao()		
	            {
	            
                DescricaoAplicacao = entity0.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0Al1.DESCRICAO_APLICATIVO
                , EmDesenvolvimento = entity0.EM_DESENVOLVIMENTO
                , IdAplicacao = entity0.ID_APLICACAO
                , IdTcsAplicativo = entity0Al1.ID_TCS_APLICATIVO
                , UidAplicacao = entity0.UID_APLICACAO
                , Url = entity0.URL
                , UrlWorkArea = entity0.URL_WORK_AREA
			
                ,TcsAplicacaoVersaoHistoricoList = 
	                        (from entity1 in entity0.TCS_APLICACAO_VERSAO_HISTORICO_LISTA
                                  let entity1Al1 = entity1.TCS_APLICACAO
	                        
	                        	
	                        select new TcsAplicacaoVersaoHistorico()
	                        {
	                        
                                DataAtualizacao = entity1.DATA_ATUALIZACAO
                                , IdAplicacao = entity1Al1.ID_APLICACAO
                                , IdTcsAplicacaoVersaoHistorico = entity1.ID_TCS_APLICACAO_VERSAO_HISTORICO
                                , Versao = entity1.VERSAO
		
	                        }
	                        )
			
                ,TcsAmbienteList = 
	                        (from entity1 in entity0.TCS_AMBIENTE_LISTA
                                  let entity1Al1 = entity1.TCS_APLICACAO
                                  let entity1Al2 = entity1.TCS_EMPRESA_AUTENTICACAO
	                        
	                        	
	                        select new TcsAmbiente()
	                        {
	                        
                                DescricaoAmbiente = entity1.DESCRICAO_AMBIENTE
                                , IdAplicacao = entity1Al1.ID_APLICACAO
                                , IdLinx = entity1Al2.ID_LINX
                                , IdTcsAmbiente = entity1.ID_TCS_AMBIENTE
                                , NomeEmpresa = entity1Al2.NOME_EMPRESA
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsAplicacaoVersaoHistorico.
	    public IQueryable<TcsAplicacaoVersaoHistorico> GetTcsAplicacaoVersaoHistorico()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsAplicacaoVersaoHistorico> result = 
	            (from entity0 in this.DbContext.TCS_APLICACAO_VERSAO_HISTORICO
                  let entity0Al1 = entity0.TCS_APLICACAO
	            
	            	
	            select new TcsAplicacaoVersaoHistorico()		
	            {
	            
                DataAtualizacao = entity0.DATA_ATUALIZACAO
                , IdAplicacao = entity0Al1.ID_APLICACAO
                , IdTcsAplicacaoVersaoHistorico = entity0.ID_TCS_APLICACAO_VERSAO_HISTORICO
                , Versao = entity0.VERSAO
		
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
                , IdAplicacao = entity0Al1.ID_APLICACAO
                , IdLinx = entity0Al2.ID_LINX
                , IdTcsAmbiente = entity0.ID_TCS_AMBIENTE
                , NomeEmpresa = entity0Al2.NOME_EMPRESA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAplicacaoNoAssociations.
	    public IQueryable<TcsAplicacao> GetTcsAplicacaoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsAplicacao> result = 
	            (from entity0 in this.DbContext.TCS_APLICACAO
                  let entity0Al1 = entity0.TCS_APLICATIVO
	            
	            	
	            select new TcsAplicacao()		
	            {
	            
                DescricaoAplicacao = entity0.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0Al1.DESCRICAO_APLICATIVO
                , EmDesenvolvimento = entity0.EM_DESENVOLVIMENTO
                , IdAplicacao = entity0.ID_APLICACAO
                , IdTcsAplicativo = entity0Al1.ID_TCS_APLICATIVO
                , UidAplicacao = entity0.UID_APLICACAO
                , Url = entity0.URL
                , UrlWorkArea = entity0.URL_WORK_AREA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAplicacaoVersaoHistoricoNoAssociations.
	    public IQueryable<TcsAplicacaoVersaoHistorico> GetTcsAplicacaoVersaoHistoricoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsAplicacaoVersaoHistorico> result = 
	            (from entity0 in this.DbContext.TCS_APLICACAO_VERSAO_HISTORICO
                  let entity0Al1 = entity0.TCS_APLICACAO
	            
	            	
	            select new TcsAplicacaoVersaoHistorico()		
	            {
	            
                DataAtualizacao = entity0.DATA_ATUALIZACAO
                , IdAplicacao = entity0Al1.ID_APLICACAO
                , IdTcsAplicacaoVersaoHistorico = entity0.ID_TCS_APLICACAO_VERSAO_HISTORICO
                , Versao = entity0.VERSAO
		
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
                , IdAplicacao = entity0Al1.ID_APLICACAO
                , IdLinx = entity0Al2.ID_LINX
                , IdTcsAmbiente = entity0.ID_TCS_AMBIENTE
                , NomeEmpresa = entity0Al2.NOME_EMPRESA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	//Add filtering disabled property for TCS_APLICACAO
	    	string[] bmDisabledTcsAplicacaoList = this.GetEDM().GetFilteringDisabledList("TCS_APLICACAO");
	    	if (bmDisabledTcsAplicacaoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsAplicacaoList.Contains("TCS_APLICACAO.DESCRICAO_APLICACAO"))
	    		{
	    			result.Add("TcsAplicacao|DescricaoAplicacao");
	    			result.Add("TcsAplicacao|TCS_APLICACAO.DESCRICAO_APLICACAO");
	    		}
	
	    		if (bmDisabledTcsAplicacaoList.Contains("TCS_APLICACAO.EM_DESENVOLVIMENTO"))
	    		{
	    			result.Add("TcsAplicacao|EmDesenvolvimento");
	    			result.Add("TcsAplicacao|TCS_APLICACAO.EM_DESENVOLVIMENTO");
	    		}
	
	    		if (bmDisabledTcsAplicacaoList.Contains("TCS_APLICACAO.ID_APLICACAO"))
	    		{
	    			result.Add("TcsAplicacao|IdAplicacao");
	    			result.Add("TcsAplicacao|TCS_APLICACAO.ID_APLICACAO");
	    		}
	
	    		if (bmDisabledTcsAplicacaoList.Contains("TCS_APLICACAO.UID_APLICACAO"))
	    		{
	    			result.Add("TcsAplicacao|UidAplicacao");
	    			result.Add("TcsAplicacao|TCS_APLICACAO.UID_APLICACAO");
	    		}
	
	    		if (bmDisabledTcsAplicacaoList.Contains("TCS_APLICACAO.URL"))
	    		{
	    			result.Add("TcsAplicacao|Url");
	    			result.Add("TcsAplicacao|TCS_APLICACAO.URL");
	    		}
	
	    		if (bmDisabledTcsAplicacaoList.Contains("TCS_APLICACAO.URL_WORK_AREA"))
	    		{
	    			result.Add("TcsAplicacao|UrlWorkArea");
	    			result.Add("TcsAplicacao|TCS_APLICACAO.URL_WORK_AREA");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_APLICACAO_VERSAO_HISTORICO
	    	string[] bmDisabledTcsAplicacaoVersaoHistoricoList = this.GetEDM().GetFilteringDisabledList("TCS_APLICACAO_VERSAO_HISTORICO");
	    	if (bmDisabledTcsAplicacaoVersaoHistoricoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsAplicacaoVersaoHistoricoList.Contains("TCS_APLICACAO_VERSAO_HISTORICO.DATA_ATUALIZACAO"))
	    		{
	    			result.Add("TcsAplicacaoVersaoHistorico|DataAtualizacao");
	    			result.Add("TcsAplicacaoVersaoHistorico|TCS_APLICACAO_VERSAO_HISTORICO.DATA_ATUALIZACAO");
	    		}
	
	    		if (bmDisabledTcsAplicacaoVersaoHistoricoList.Contains("TCS_APLICACAO_VERSAO_HISTORICO.ID_TCS_APLICACAO_VERSAO_HISTORICO"))
	    		{
	    			result.Add("TcsAplicacaoVersaoHistorico|IdTcsAplicacaoVersaoHistorico");
	    			result.Add("TcsAplicacaoVersaoHistorico|TCS_APLICACAO_VERSAO_HISTORICO.ID_TCS_APLICACAO_VERSAO_HISTORICO");
	    		}
	
	    		if (bmDisabledTcsAplicacaoVersaoHistoricoList.Contains("TCS_APLICACAO_VERSAO_HISTORICO.VERSAO"))
	    		{
	    			result.Add("TcsAplicacaoVersaoHistorico|Versao");
	    			result.Add("TcsAplicacaoVersaoHistorico|TCS_APLICACAO_VERSAO_HISTORICO.VERSAO");
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
	    	return result;
	    }

	    #endregion Excluded Filters

	    #region Get By EntitySearchId Methods Definitions.
	
				
	    [Ignore]
	    //Get TcsAplicacao By EntitySearchId.
	    public IQueryable<TcsAplicacao> GetTcsAplicacaoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsAplicacaoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsAplicacaoVersaoHistorico By EntitySearchId.
	    public IQueryable<TcsAplicacaoVersaoHistorico> GetTcsAplicacaoVersaoHistoricoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsAplicacaoVersaoHistoricoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsAmbiente By EntitySearchId.
	    public IQueryable<TcsAmbiente> GetTcsAmbienteByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsAmbienteByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsAplicacao By EntitySearchId.
	    public IQueryable<TcsAplicacao> GetTcsAplicacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsAplicacaoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsAplicacaoVersaoHistorico By EntitySearchId.
	    public IQueryable<TcsAplicacaoVersaoHistorico> GetTcsAplicacaoVersaoHistoricoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsAplicacaoVersaoHistoricoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsAmbiente By EntitySearchId.
	    public IQueryable<TcsAmbiente> GetTcsAmbienteByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsAmbienteByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get TcsAplicacao By Example.
	    [Ignore]
	    public IQueryable<TcsAplicacao> GetTcsAplicacaoByExample(TcsAplicacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsAplicacaoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsAplicacaoVersaoHistorico By Example.
	    [Ignore]
	    public IQueryable<TcsAplicacaoVersaoHistorico> GetTcsAplicacaoVersaoHistoricoByExample(TcsAplicacaoVersaoHistorico entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsAplicacaoVersaoHistoricoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsAmbiente By Example.
	    [Ignore]
	    public IQueryable<TcsAmbiente> GetTcsAmbienteByExample(TcsAmbiente entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsAmbienteByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsAplicacao By Example.
	    [Ignore]
	    public IQueryable<TcsAplicacao> GetTcsAplicacaoByExampleNoAssociations(TcsAplicacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsAplicacaoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsAplicacaoVersaoHistorico By Example.
	    [Ignore]
	    public IQueryable<TcsAplicacaoVersaoHistorico> GetTcsAplicacaoVersaoHistoricoByExampleNoAssociations(TcsAplicacaoVersaoHistorico entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsAplicacaoVersaoHistoricoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsAmbiente By Example.
	    [Ignore]
	    public IQueryable<TcsAmbiente> GetTcsAmbienteByExampleNoAssociations(TcsAmbiente entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsAmbienteByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public TcsAplicacao GetTcsAplicacaoByKey(Int32 idAplicacao)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsAplicacao");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdAplicacao"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idAplicacao));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsAplicacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsAplicacaoVersaoHistorico GetTcsAplicacaoVersaoHistoricoByKey(Int32 idTcsAplicacaoVersaoHistorico)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsAplicacaoVersaoHistorico");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsAplicacaoVersaoHistorico"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsAplicacaoVersaoHistorico));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsAplicacaoVersaoHistoricoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get TcsAplicacaoByEntitySearch.
	    public IQueryable<TcsAplicacao> GetTcsAplicacaoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAplicacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAplicacao> result = 
	            (from entity0 in this.DbContext.TCS_APLICACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_APLICATIVO
	            
	            	
	            select new TcsAplicacao()		
	            {
	            
                DescricaoAplicacao = entity0.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0Al1.DESCRICAO_APLICATIVO
                , EmDesenvolvimento = entity0.EM_DESENVOLVIMENTO
                , IdAplicacao = entity0.ID_APLICACAO
                , IdTcsAplicativo = entity0Al1.ID_TCS_APLICATIVO
                , UidAplicacao = entity0.UID_APLICACAO
                , Url = entity0.URL
                , UrlWorkArea = entity0.URL_WORK_AREA
			
                ,TcsAplicacaoVersaoHistoricoList = 
	                        (from entity1 in entity0.TCS_APLICACAO_VERSAO_HISTORICO_LISTA
                                  let entity1Al1 = entity1.TCS_APLICACAO
	                        
	                        	
	                        select new TcsAplicacaoVersaoHistorico()
	                        {
	                        
                                DataAtualizacao = entity1.DATA_ATUALIZACAO
                                , IdAplicacao = entity1Al1.ID_APLICACAO
                                , IdTcsAplicacaoVersaoHistorico = entity1.ID_TCS_APLICACAO_VERSAO_HISTORICO
                                , Versao = entity1.VERSAO
		
	                        }
	                        )
			
                ,TcsAmbienteList = 
	                        (from entity1 in entity0.TCS_AMBIENTE_LISTA
                                  let entity1Al1 = entity1.TCS_APLICACAO
                                  let entity1Al2 = entity1.TCS_EMPRESA_AUTENTICACAO
	                        
	                        	
	                        select new TcsAmbiente()
	                        {
	                        
                                DescricaoAmbiente = entity1.DESCRICAO_AMBIENTE
                                , IdAplicacao = entity1Al1.ID_APLICACAO
                                , IdLinx = entity1Al2.ID_LINX
                                , IdTcsAmbiente = entity1.ID_TCS_AMBIENTE
                                , NomeEmpresa = entity1Al2.NOME_EMPRESA
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAplicacaoVersaoHistoricoByEntitySearch.
	    public IQueryable<TcsAplicacaoVersaoHistorico> GetTcsAplicacaoVersaoHistoricoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAplicacaoVersaoHistorico));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAplicacaoVersaoHistorico> result = 
	            (from entity0 in this.DbContext.TCS_APLICACAO_VERSAO_HISTORICO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_APLICACAO
	            
	            	
	            select new TcsAplicacaoVersaoHistorico()		
	            {
	            
                DataAtualizacao = entity0.DATA_ATUALIZACAO
                , IdAplicacao = entity0Al1.ID_APLICACAO
                , IdTcsAplicacaoVersaoHistorico = entity0.ID_TCS_APLICACAO_VERSAO_HISTORICO
                , Versao = entity0.VERSAO
		
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
                , IdAplicacao = entity0Al1.ID_APLICACAO
                , IdLinx = entity0Al2.ID_LINX
                , IdTcsAmbiente = entity0.ID_TCS_AMBIENTE
                , NomeEmpresa = entity0Al2.NOME_EMPRESA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAplicacaoByEntitySearchNoAssociations.
	    public IQueryable<TcsAplicacao> GetTcsAplicacaoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAplicacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAplicacao> result = 
	            (from entity0 in this.DbContext.TCS_APLICACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_APLICATIVO
	            
	            	
	            select new TcsAplicacao()		
	            {
	            
                DescricaoAplicacao = entity0.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0Al1.DESCRICAO_APLICATIVO
                , EmDesenvolvimento = entity0.EM_DESENVOLVIMENTO
                , IdAplicacao = entity0.ID_APLICACAO
                , IdTcsAplicativo = entity0Al1.ID_TCS_APLICATIVO
                , UidAplicacao = entity0.UID_APLICACAO
                , Url = entity0.URL
                , UrlWorkArea = entity0.URL_WORK_AREA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAplicacaoVersaoHistoricoByEntitySearchNoAssociations.
	    public IQueryable<TcsAplicacaoVersaoHistorico> GetTcsAplicacaoVersaoHistoricoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAplicacaoVersaoHistorico));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAplicacaoVersaoHistorico> result = 
	            (from entity0 in this.DbContext.TCS_APLICACAO_VERSAO_HISTORICO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_APLICACAO
	            
	            	
	            select new TcsAplicacaoVersaoHistorico()		
	            {
	            
                DataAtualizacao = entity0.DATA_ATUALIZACAO
                , IdAplicacao = entity0Al1.ID_APLICACAO
                , IdTcsAplicacaoVersaoHistorico = entity0.ID_TCS_APLICACAO_VERSAO_HISTORICO
                , Versao = entity0.VERSAO
		
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
                , IdAplicacao = entity0Al1.ID_APLICACAO
                , IdLinx = entity0Al2.ID_LINX
                , IdTcsAmbiente = entity0.ID_TCS_AMBIENTE
                , NomeEmpresa = entity0Al2.NOME_EMPRESA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAplicacaoVersaoHistoricoParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsAplicacaoVersaoHistoricoParentComposition> GetTcsAplicacaoVersaoHistoricoParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "TCS_APLICACAO", "TCS_APLICACAO_VERSAO_HISTORICO", "TCS_APLICACAO", typeof(TcsAplicacaoVersaoHistoricoParentComposition), typeof(TcsAmbiente));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAplicacaoVersaoHistoricoParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_APLICACAO_VERSAO_HISTORICO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_APLICACAO
	            
	            	
	            select new TcsAplicacaoVersaoHistoricoParentComposition()		
	            {
	            
                DataAtualizacao = entity0.DATA_ATUALIZACAO
                , IdAplicacao = entity0Al1.ID_APLICACAO
                , IdTcsAplicacaoVersaoHistorico = entity0.ID_TCS_APLICACAO_VERSAO_HISTORICO
                , Versao = entity0.VERSAO
                //TcsAplicacao Properties.
                , DescricaoAplicacao = entity0.TCS_APLICACAO.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0.TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO
                , EmDesenvolvimento = entity0.TCS_APLICACAO.EM_DESENVOLVIMENTO
                , IdTcsAplicativo = entity0.TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO
                , UidAplicacao = entity0.TCS_APLICACAO.UID_APLICACAO
                , Url = entity0.TCS_APLICACAO.URL
                , UrlWorkArea = entity0.TCS_APLICACAO.URL_WORK_AREA
		
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "TCS_APLICACAO", "TCS_AMBIENTE", "TCS_APLICACAO", typeof(TcsAmbienteParentComposition), typeof(TcsAplicacaoVersaoHistorico));
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
                , IdAplicacao = entity0Al1.ID_APLICACAO
                , IdLinx = entity0Al2.ID_LINX
                , IdTcsAmbiente = entity0.ID_TCS_AMBIENTE
                , NomeEmpresa = entity0Al2.NOME_EMPRESA
                //TcsAplicacao Properties.
                , DescricaoAplicacao = entity0.TCS_APLICACAO.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0.TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO
                , EmDesenvolvimento = entity0.TCS_APLICACAO.EM_DESENVOLVIMENTO
                , IdTcsAplicativo = entity0.TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO
                , UidAplicacao = entity0.TCS_APLICACAO.UID_APLICACAO
                , Url = entity0.TCS_APLICACAO.URL
                , UrlWorkArea = entity0.TCS_APLICACAO.URL_WORK_AREA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get PagedTcsAplicacao.
	    public IQueryable<TcsAplicacao> GetPagedTcsAplicacao(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAplicacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAplicacao> result = 
	            (from entity0 in this.DbContext.TCS_APLICACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_APLICATIVO
                orderby entity0.ID_APLICACAO ascending
	            
	            	
	            select new TcsAplicacao()		
	            {
	            
                DescricaoAplicacao = entity0.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0Al1.DESCRICAO_APLICATIVO
                , EmDesenvolvimento = entity0.EM_DESENVOLVIMENTO
                , IdAplicacao = entity0.ID_APLICACAO
                , IdTcsAplicativo = entity0Al1.ID_TCS_APLICATIVO
                , UidAplicacao = entity0.UID_APLICACAO
                , Url = entity0.URL
                , UrlWorkArea = entity0.URL_WORK_AREA
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsAplicacaoVersaoHistorico.
	    public IQueryable<TcsAplicacaoVersaoHistorico> GetPagedTcsAplicacaoVersaoHistorico(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAplicacaoVersaoHistorico));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAplicacaoVersaoHistorico> result = 
	            (from entity0 in this.DbContext.TCS_APLICACAO_VERSAO_HISTORICO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_APLICACAO
                orderby entity0.ID_TCS_APLICACAO_VERSAO_HISTORICO ascending
	            
	            	
	            select new TcsAplicacaoVersaoHistorico()		
	            {
	            
                DataAtualizacao = entity0.DATA_ATUALIZACAO
                , IdAplicacao = entity0Al1.ID_APLICACAO
                , IdTcsAplicacaoVersaoHistorico = entity0.ID_TCS_APLICACAO_VERSAO_HISTORICO
                , Versao = entity0.VERSAO
		
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
                , IdAplicacao = entity0Al1.ID_APLICACAO
                , IdLinx = entity0Al2.ID_LINX
                , IdTcsAmbiente = entity0.ID_TCS_AMBIENTE
                , NomeEmpresa = entity0Al2.NOME_EMPRESA
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsAplicacaoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAplicacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_APLICACAO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_APLICATIVO
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsAplicacaoVersaoHistoricoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAplicacaoVersaoHistorico));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_APLICACAO_VERSAO_HISTORICO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_APLICACAO
	            
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
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    
	    [Update()]	
	    //Update TcsAplicacao.
	    public void UpdateTcsAplicacao(TcsAplicacao entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsAplicacao.
	    public void InsertTcsAplicacao(TcsAplicacao entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsAplicacao.
	    public void DeleteTcsAplicacao(TcsAplicacao entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsAplicacaoVersaoHistorico.
	    public void UpdateTcsAplicacaoVersaoHistorico(TcsAplicacaoVersaoHistorico entity)
	    {



	
	        if (entity.TcsAplicacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsAplicacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsAplicacao); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsAplicacaoVersaoHistorico.
	    public void InsertTcsAplicacaoVersaoHistorico(TcsAplicacaoVersaoHistorico entity)
	    {



	
	        if (entity.TcsAplicacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsAplicacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsAplicacao);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsAplicacaoVersaoHistorico.
	    public void DeleteTcsAplicacaoVersaoHistorico(TcsAplicacaoVersaoHistorico entity)
	    {



	
	        if (entity.TcsAplicacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsAplicacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsAplicacao);
	            

	
	        }

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsAmbiente.
	    public void UpdateTcsAmbiente(TcsAmbiente entity)
	    {



	
	        if (entity.TcsAplicacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsAplicacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsAplicacao); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsAmbiente.
	    public void InsertTcsAmbiente(TcsAmbiente entity)
	    {



	
	        if (entity.TcsAplicacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsAplicacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsAplicacao);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsAmbiente.
	    public void DeleteTcsAmbiente(TcsAmbiente entity)
	    {



	
	        if (entity.TcsAplicacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsAplicacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsAplicacao);
	            

	
	        }

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}