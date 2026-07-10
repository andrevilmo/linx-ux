					
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

namespace Linx.Framework.BV.Ambiente
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_AMBIENTE.ID_TCS_AMBIENTE", IsUpdatable=true, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsAmbiente,TcsAmbiente.TcsAmbienteUsuarioAcesso,TcsAmbiente.TcsAmbienteConexao,TcsAmbiente.TcsAmbienteServicoExcecao];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsAmbiente];ReadOnly[false];Entities[TCS_AMBIENTE:IdTcsAmbiente|TCS_APLICACAO:IdAplicacao|TCS_EMPRESA_AUTENTICACAO:IdLinx];SubQueryInfo[];EdmEntityName[TCS_AMBIENTE];EntityRelations[TCS_APLICACAO(TCS_APLICACAO)#TCS_APLICATIVO(TCS_APLICATIVO)#TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsAmbiente")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Ambiente.TcsAmbiente")]
	public partial class TcsAmbiente : Linx.Data.Entity
	{

	
		
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.TcsAmbienteUsuarioAcessoList != null && this.TcsAmbienteUsuarioAcessoList.Count() > 0)
	      {
	         foreach (var entity in this.TcsAmbienteUsuarioAcessoList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      if (this.TcsAmbienteConexaoList != null && this.TcsAmbienteConexaoList.Count() > 0)
	      {
	         foreach (var entity in this.TcsAmbienteConexaoList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      if (this.TcsAmbienteServicoExcecaoList != null && this.TcsAmbienteServicoExcecaoList.Count() > 0)
	      {
	         foreach (var entity in this.TcsAmbienteServicoExcecaoList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.TcsAmbienteUsuarioAcessoList != null)
	      {
	         foreach (var detail in this.TcsAmbienteUsuarioAcessoList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsAmbienteUsuarioAcessoList = null;
	      }
	      if (this.TcsAmbienteConexaoList != null)
	      {
	         foreach (var detail in this.TcsAmbienteConexaoList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsAmbienteConexaoList = null;
	      }
	      if (this.TcsAmbienteServicoExcecaoList != null)
	      {
	         foreach (var detail in this.TcsAmbienteServicoExcecaoList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsAmbienteServicoExcecaoList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(AmbienteDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("TcsAmbienteUsuarioAcesso"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsAmbienteUsuarioAcesso");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsAmbiente"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdTcsAmbiente));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsAmbienteUsuarioAcesso and all sub-details
	         if (this.TcsAmbienteUsuarioAcessoList == null || this.TcsAmbienteUsuarioAcessoList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsAmbienteUsuarioAcessoList = context.GetPagedTcsAmbienteUsuarioAcesso(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsAmbienteUsuarioAcessoList = (from r in context.GetTcsAmbienteUsuarioAcessoByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	      if (viewNames == null || viewNames.Contains("TcsAmbienteConexao"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsAmbienteConexao");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsAmbiente"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdTcsAmbiente));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsAmbienteConexao and all sub-details
	         if (this.TcsAmbienteConexaoList == null || this.TcsAmbienteConexaoList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsAmbienteConexaoList = context.GetPagedTcsAmbienteConexao(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsAmbienteConexaoList = (from r in context.GetTcsAmbienteConexaoByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	      if (viewNames == null || viewNames.Contains("TcsAmbienteServicoExcecao"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsAmbienteServicoExcecao");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsAmbiente"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdTcsAmbiente));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsAmbienteServicoExcecao and all sub-details
	         if (this.TcsAmbienteServicoExcecaoList == null || this.TcsAmbienteServicoExcecaoList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsAmbienteServicoExcecaoList = context.GetPagedTcsAmbienteServicoExcecao(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsAmbienteServicoExcecaoList = (from r in context.GetTcsAmbienteServicoExcecaoByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _TcsAmbienteUsuarioAcessoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsAmbienteUsuarioAcesso && ((TcsAmbienteUsuarioAcesso)e.Entity).TcsAmbiente == null && e.Associations == null && e.OriginalAssociations == null && ((TcsAmbienteUsuarioAcesso)e.Entity).IdTcsAmbiente == this.IdTcsAmbiente).ToList();
 	      if (_TcsAmbienteUsuarioAcessoElements.Count > 0 && this.TcsAmbienteUsuarioAcessoList.Count() == 0)
 	      {
 	          this.TcsAmbienteUsuarioAcessoList = _TcsAmbienteUsuarioAcessoElements.Select(e => (TcsAmbienteUsuarioAcesso)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsAmbienteUsuarioAcessoElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsAmbienteUsuarioAcesso)detail.Entity).TcsAmbiente = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsAmbiente", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsAmbienteUsuarioAcessoList", indexDetails.ToArray());
 	      }
 
 	      var _TcsAmbienteConexaoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsAmbienteConexao && ((TcsAmbienteConexao)e.Entity).TcsAmbiente == null && e.Associations == null && e.OriginalAssociations == null && ((TcsAmbienteConexao)e.Entity).IdTcsAmbiente == this.IdTcsAmbiente).ToList();
 	      if (_TcsAmbienteConexaoElements.Count > 0 && this.TcsAmbienteConexaoList.Count() == 0)
 	      {
 	          this.TcsAmbienteConexaoList = _TcsAmbienteConexaoElements.Select(e => (TcsAmbienteConexao)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsAmbienteConexaoElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsAmbienteConexao)detail.Entity).TcsAmbiente = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsAmbiente", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsAmbienteConexaoList", indexDetails.ToArray());
 	      }
 
 	      var _TcsAmbienteServicoExcecaoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsAmbienteServicoExcecao && ((TcsAmbienteServicoExcecao)e.Entity).TcsAmbiente == null && e.Associations == null && e.OriginalAssociations == null && ((TcsAmbienteServicoExcecao)e.Entity).IdTcsAmbiente == this.IdTcsAmbiente).ToList();
 	      if (_TcsAmbienteServicoExcecaoElements.Count > 0 && this.TcsAmbienteServicoExcecaoList.Count() == 0)
 	      {
 	          this.TcsAmbienteServicoExcecaoList = _TcsAmbienteServicoExcecaoElements.Select(e => (TcsAmbienteServicoExcecao)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsAmbienteServicoExcecaoElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsAmbienteServicoExcecao)detail.Entity).TcsAmbiente = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsAmbiente", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsAmbienteServicoExcecaoList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
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
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[IdTcsAmbiente];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_AMBIENTE.DESCRICAO_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.DESCRICAO_AMBIENTE")]
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
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicacao];LookUpTitle[Seleção de (Aplicação)];LookUpQuery[executeLookUpTcsAplicacao];LookUpFinalize[finalizeLookUpTcsAplicacao];LookUpDisplayColumns[{\"DescricaoAplicacao\" : \"Aplicação\", \"DescricaoAplicativo\" : \"Aplicativo\", \"EmDesenvolvimento\" : \"Em Desenvolvimento\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\", \"IdAplicacao\" : \"Id Aplicacao\", \"UidAplicacao\" : \"Uid Aplicacao\", \"Url\" : \"Url\", \"UrlWorkArea\" : \"Url Work Area\"}];LookUpColumns[{\"DescricaoAplicacao\" : true, \"DescricaoAplicativo\" : true, \"EmDesenvolvimento\" : true, \"IdTcsAplicativo\" : false, \"IdAplicacao\" : false, \"UidAplicacao\" : false, \"Url\" : false, \"UrlWorkArea\" : true}];FilterDataKey[TCS_AMBIENTE.TCS_APLICACAO.DESCRICAO_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoAplicacao#false##60:0##Aplicação#0#true##::LookUpTcsAplicacao##false#false#TCS_APLICACAO#TCS_APLICACAO#Linx.Framework.BV.Ambiente#IQueryable#DescricaoAplicativo,IdTcsAplicativo[DescricaoAplicativo,IdTcsAplicativo]#DescricaoAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];EmDesenvolvimento[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UidAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];Url[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UrlWorkArea[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo]#true#false", EdmKey="TCS_AMBIENTE.TCS_APLICACAO.DESCRICAO_APLICACAO")]
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
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicacao];LookUpTitle[Seleção de (Aplicativo)];LookUpQuery[executeLookUpTcsAplicacao];LookUpFinalize[finalizeLookUpTcsAplicacao];LookUpDisplayColumns[{\"DescricaoAplicacao\" : \"Aplicação\", \"DescricaoAplicativo\" : \"Aplicativo\", \"EmDesenvolvimento\" : \"Em Desenvolvimento\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\", \"IdAplicacao\" : \"Id Aplicacao\", \"UidAplicacao\" : \"Uid Aplicacao\", \"Url\" : \"Url\", \"UrlWorkArea\" : \"Url Work Area\"}];LookUpColumns[{\"DescricaoAplicacao\" : true, \"DescricaoAplicativo\" : true, \"EmDesenvolvimento\" : true, \"IdTcsAplicativo\" : false, \"IdAplicacao\" : false, \"UidAplicacao\" : false, \"Url\" : false, \"UrlWorkArea\" : true}];FilterDataKey[TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoAplicativo#false##2500##Aplicativo#1#true##::LookUpTcsAplicacao##false#false#TCS_APLICACAO#TCS_APLICACAO#Linx.Framework.BV.Ambiente#IQueryable#DescricaoAplicativo,IdTcsAplicativo[DescricaoAplicativo,IdTcsAplicativo]#DescricaoAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];EmDesenvolvimento[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UidAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];Url[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UrlWorkArea[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo]#true#false", EdmKey="TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO")]
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
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicacao];LookUpTitle[Seleção de (Em Desenvolvimento)];LookUpQuery[executeLookUpTcsAplicacao];LookUpFinalize[finalizeLookUpTcsAplicacao];LookUpDisplayColumns[{\"DescricaoAplicacao\" : \"Aplicação\", \"DescricaoAplicativo\" : \"Aplicativo\", \"EmDesenvolvimento\" : \"Em Desenvolvimento\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\", \"IdAplicacao\" : \"Id Aplicacao\", \"UidAplicacao\" : \"Uid Aplicacao\", \"Url\" : \"Url\", \"UrlWorkArea\" : \"Url Work Area\"}];LookUpColumns[{\"DescricaoAplicacao\" : true, \"DescricaoAplicativo\" : true, \"EmDesenvolvimento\" : true, \"IdTcsAplicativo\" : false, \"IdAplicacao\" : false, \"UidAplicacao\" : false, \"Url\" : false, \"UrlWorkArea\" : true}];FilterDataKey[TCS_AMBIENTE.TCS_APLICACAO.EM_DESENVOLVIMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Boolean#EmDesenvolvimento#false##0:0##Em Desenvolvimento#2#true##::LookUpTcsAplicacao##false#false#TCS_APLICACAO#TCS_APLICACAO#Linx.Framework.BV.Ambiente#IQueryable#DescricaoAplicativo,IdTcsAplicativo[DescricaoAplicativo,IdTcsAplicativo]#DescricaoAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];EmDesenvolvimento[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UidAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];Url[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UrlWorkArea[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo]#true#false", EdmKey="TCS_AMBIENTE.TCS_APLICACAO.EM_DESENVOLVIMENTO")]
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
	    [Display(Name = "Id Aplicacao", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicacao];LookUpTitle[Seleção de (Id Aplicacao)];LookUpQuery[executeLookUpTcsAplicacao];LookUpFinalize[finalizeLookUpTcsAplicacao];LookUpDisplayColumns[{\"DescricaoAplicacao\" : \"Aplicação\", \"DescricaoAplicativo\" : \"Aplicativo\", \"EmDesenvolvimento\" : \"Em Desenvolvimento\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\", \"IdAplicacao\" : \"Id Aplicacao\", \"UidAplicacao\" : \"Uid Aplicacao\", \"Url\" : \"Url\", \"UrlWorkArea\" : \"Url Work Area\"}];LookUpColumns[{\"DescricaoAplicacao\" : true, \"DescricaoAplicativo\" : true, \"EmDesenvolvimento\" : true, \"IdTcsAplicativo\" : false, \"IdAplicacao\" : false, \"UidAplicacao\" : false, \"Url\" : false, \"UrlWorkArea\" : true}];FilterDataKey[TCS_AMBIENTE.TCS_APLICACAO.ID_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdAplicacao#true##12:0##Id Aplicacao#4#false##::LookUpTcsAplicacao##false#false#TCS_APLICACAO#TCS_APLICACAO#Linx.Framework.BV.Ambiente#IQueryable#DescricaoAplicativo,IdTcsAplicativo[DescricaoAplicativo,IdTcsAplicativo]#DescricaoAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];EmDesenvolvimento[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UidAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];Url[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UrlWorkArea[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo]#true#false", EdmKey="TCS_AMBIENTE.TCS_APLICACAO.ID_APLICACAO")]
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
	    [Display(Name = "ID Linx Ambiente", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsEmpresaAutenticacao];LookUpTitle[Seleção de (ID Linx Ambiente)];LookUpQuery[executeLookUpTcsEmpresaAutenticacao];LookUpFinalize[finalizeLookUpTcsEmpresaAutenticacao];LookUpDisplayColumns[{\"IdLinx\" : \"ID Linx\", \"NomeEmpresa\" : \"Empresa (Id Linx)\", \"UidEmpresa\" : \"Uid Empresa\"}];LookUpColumns[{\"IdLinx\" : true, \"NomeEmpresa\" : true, \"UidEmpresa\" : false}];FilterDataKey[TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdLinx#true##12:0##ID Linx#0#true##::LookUpTcsEmpresaAutenticacao##false#false#TCS_EMPRESA_AUTENTICACAO#TCS_EMPRESA_AUTENTICACAO#Linx.Framework.BV.Ambiente#IQueryable###true#false", EdmKey="TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
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
	    [Display(Name = "Ambiente", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_AMBIENTE.ID_TCS_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.ID_TCS_AMBIENTE")]
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
	    //Extensibility Partial Method Definitions For IdTcsAplicativo
	    partial void OnIdTcsAplicativoChanging(Int32 value);
	    partial void OnIdTcsAplicativoChanged();

	    private Int32 _IdTcsAplicativo;

	    [DataMember(IsRequired = true, Name = "IdTcsAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Aplicativo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicacao];LookUpTitle[Seleção de (Id Tcs Aplicativo)];LookUpQuery[executeLookUpTcsAplicacao];LookUpFinalize[finalizeLookUpTcsAplicacao];LookUpDisplayColumns[{\"DescricaoAplicacao\" : \"Aplicação\", \"DescricaoAplicativo\" : \"Aplicativo\", \"EmDesenvolvimento\" : \"Em Desenvolvimento\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\", \"IdAplicacao\" : \"Id Aplicacao\", \"UidAplicacao\" : \"Uid Aplicacao\", \"Url\" : \"Url\", \"UrlWorkArea\" : \"Url Work Area\"}];LookUpColumns[{\"DescricaoAplicacao\" : true, \"DescricaoAplicativo\" : true, \"EmDesenvolvimento\" : true, \"IdTcsAplicativo\" : false, \"IdAplicacao\" : false, \"UidAplicacao\" : false, \"Url\" : false, \"UrlWorkArea\" : true}];FilterDataKey[TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdTcsAplicativo#false##12:0##Id Tcs Aplicativo#3#false##::LookUpTcsAplicacao##false#false#TCS_APLICACAO#TCS_APLICACAO#Linx.Framework.BV.Ambiente#IQueryable#DescricaoAplicativo,IdTcsAplicativo[DescricaoAplicativo,IdTcsAplicativo]#DescricaoAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];EmDesenvolvimento[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UidAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];Url[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UrlWorkArea[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo]#true#false", EdmKey="TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO")]
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
	    //Extensibility Partial Method Definitions For NomeEmpresa
	    partial void OnNomeEmpresaChanging(System.String value);
	    partial void OnNomeEmpresaChanged();

	    private System.String _NomeEmpresa;

	    [DataMember(IsRequired = true, Name = "NomeEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Empresa (Id Linx)", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsEmpresaAutenticacao];LookUpTitle[Seleção de (Empresa (Id Linx))];LookUpQuery[executeLookUpTcsEmpresaAutenticacao];LookUpFinalize[finalizeLookUpTcsEmpresaAutenticacao];LookUpDisplayColumns[{\"IdLinx\" : \"ID Linx\", \"NomeEmpresa\" : \"Empresa (Id Linx)\", \"UidEmpresa\" : \"Uid Empresa\"}];LookUpColumns[{\"IdLinx\" : true, \"NomeEmpresa\" : true, \"UidEmpresa\" : false}];FilterDataKey[TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeEmpresa#false##250:0##Empresa (Id Linx)#1#true##::LookUpTcsEmpresaAutenticacao##false#false#TCS_EMPRESA_AUTENTICACAO#TCS_EMPRESA_AUTENTICACAO#Linx.Framework.BV.Ambiente#IQueryable###true#false", EdmKey="TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
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
	    //Extensibility Partial Method Definitions For UidAplicacao
	    partial void OnUidAplicacaoChanging(System.Guid value);
	    partial void OnUidAplicacaoChanged();

	    private System.Guid _UidAplicacao;

	    [DataMember(IsRequired = true, Name = "UidAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Aplicacao", Description="", Order = 4, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicacao];LookUpTitle[Seleção de (Uid Aplicacao)];LookUpQuery[executeLookUpTcsAplicacao];LookUpFinalize[finalizeLookUpTcsAplicacao];LookUpDisplayColumns[{\"DescricaoAplicacao\" : \"Aplicação\", \"DescricaoAplicativo\" : \"Aplicativo\", \"EmDesenvolvimento\" : \"Em Desenvolvimento\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\", \"IdAplicacao\" : \"Id Aplicacao\", \"UidAplicacao\" : \"Uid Aplicacao\", \"Url\" : \"Url\", \"UrlWorkArea\" : \"Url Work Area\"}];LookUpColumns[{\"DescricaoAplicacao\" : true, \"DescricaoAplicativo\" : true, \"EmDesenvolvimento\" : true, \"IdTcsAplicativo\" : false, \"IdAplicacao\" : false, \"UidAplicacao\" : false, \"Url\" : false, \"UrlWorkArea\" : true}];FilterDataKey[TCS_AMBIENTE.TCS_APLICACAO.UID_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Guid#UidAplicacao#false##36:0##Uid Aplicacao#5#false##::LookUpTcsAplicacao##false#false#TCS_APLICACAO#TCS_APLICACAO#Linx.Framework.BV.Ambiente#IQueryable#DescricaoAplicativo,IdTcsAplicativo[DescricaoAplicativo,IdTcsAplicativo]#DescricaoAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];EmDesenvolvimento[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UidAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];Url[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UrlWorkArea[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo]#true#false", EdmKey="TCS_AMBIENTE.TCS_APLICACAO.UID_APLICACAO")]
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
	    //Extensibility Partial Method Definitions For UidEmpresa
	    partial void OnUidEmpresaChanging(System.Guid value);
	    partial void OnUidEmpresaChanged();

	    private System.Guid _UidEmpresa;

	    [DataMember(IsRequired = true, Name = "UidEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Empresa", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsEmpresaAutenticacao];LookUpTitle[Seleção de (Uid Empresa)];LookUpQuery[executeLookUpTcsEmpresaAutenticacao];LookUpFinalize[finalizeLookUpTcsEmpresaAutenticacao];LookUpDisplayColumns[{\"IdLinx\" : \"ID Linx\", \"NomeEmpresa\" : \"Empresa (Id Linx)\", \"UidEmpresa\" : \"Uid Empresa\"}];LookUpColumns[{\"IdLinx\" : true, \"NomeEmpresa\" : true, \"UidEmpresa\" : false}];FilterDataKey[TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Guid#UidEmpresa#false##36:0##Uid Empresa#2#false##::LookUpTcsEmpresaAutenticacao##false#false#TCS_EMPRESA_AUTENTICACAO#TCS_EMPRESA_AUTENTICACAO#Linx.Framework.BV.Ambiente#IQueryable###true#false", EdmKey="TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA")]
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
	    //Extensibility Partial Method Definitions For Url
	    partial void OnUrlChanging(System.String value);
	    partial void OnUrlChanged();

	    private System.String _Url;

	    [DataMember(Name = "Url", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Url Alternativa", Description="", Order = 5, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicacao];LookUpTitle[Seleção de (Url Alternativa)];LookUpQuery[executeLookUpTcsAplicacao];LookUpFinalize[finalizeLookUpTcsAplicacao];LookUpDisplayColumns[{\"DescricaoAplicacao\" : \"Aplicação\", \"DescricaoAplicativo\" : \"Aplicativo\", \"EmDesenvolvimento\" : \"Em Desenvolvimento\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\", \"IdAplicacao\" : \"Id Aplicacao\", \"UidAplicacao\" : \"Uid Aplicacao\", \"Url\" : \"Url\", \"UrlWorkArea\" : \"Url Work Area\"}];LookUpColumns[{\"DescricaoAplicacao\" : true, \"DescricaoAplicativo\" : true, \"EmDesenvolvimento\" : true, \"IdTcsAplicativo\" : false, \"IdAplicacao\" : false, \"UidAplicacao\" : false, \"Url\" : false, \"UrlWorkArea\" : true}];FilterDataKey[TCS_AMBIENTE.TCS_APLICACAO.URL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#Url#false##250:0##Url#6#false##::LookUpTcsAplicacao##false#false#TCS_APLICACAO#TCS_APLICACAO#Linx.Framework.BV.Ambiente#IQueryable#DescricaoAplicativo,IdTcsAplicativo[DescricaoAplicativo,IdTcsAplicativo]#DescricaoAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];EmDesenvolvimento[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UidAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];Url[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UrlWorkArea[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo]#true#false", EdmKey="TCS_AMBIENTE.TCS_APLICACAO.URL")]
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
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicacao];LookUpTitle[Seleção de (Url Work Area)];LookUpQuery[executeLookUpTcsAplicacao];LookUpFinalize[finalizeLookUpTcsAplicacao];LookUpDisplayColumns[{\"DescricaoAplicacao\" : \"Aplicação\", \"DescricaoAplicativo\" : \"Aplicativo\", \"EmDesenvolvimento\" : \"Em Desenvolvimento\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\", \"IdAplicacao\" : \"Id Aplicacao\", \"UidAplicacao\" : \"Uid Aplicacao\", \"Url\" : \"Url\", \"UrlWorkArea\" : \"Url Work Area\"}];LookUpColumns[{\"DescricaoAplicacao\" : true, \"DescricaoAplicativo\" : true, \"EmDesenvolvimento\" : true, \"IdTcsAplicativo\" : false, \"IdAplicacao\" : false, \"UidAplicacao\" : false, \"Url\" : false, \"UrlWorkArea\" : true}];FilterDataKey[TCS_AMBIENTE.TCS_APLICACAO.URL_WORK_AREA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#UrlWorkArea#false##250:0##Url Work Area#7#true##::LookUpTcsAplicacao##false#false#TCS_APLICACAO#TCS_APLICACAO#Linx.Framework.BV.Ambiente#IQueryable#DescricaoAplicativo,IdTcsAplicativo[DescricaoAplicativo,IdTcsAplicativo]#DescricaoAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];EmDesenvolvimento[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UidAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];Url[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UrlWorkArea[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo]#true#false", EdmKey="TCS_AMBIENTE.TCS_APLICACAO.URL_WORK_AREA")]
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

	    private Int32 _TemporaryIdTcsAmbiente;
	    [DataMember(Name = "TemporaryIdTcsAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ambiente (Tmp)", Description="Temporary Key", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
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

	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<TcsAmbienteConexao> _TcsAmbienteConexaoList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsAmbiente_TcsAmbienteConexao", "IdTcsAmbiente", "IdTcsAmbiente", IsForeignKey=false)]
	    [DataMember(Name = "TcsAmbienteConexaoList", EmitDefaultValue = true)]
	    public IEnumerable<TcsAmbienteConexao> TcsAmbienteConexaoList
	    {
	        get
	        {
	
	            if (this._TcsAmbienteConexaoList == null)
	            	this._TcsAmbienteConexaoList = new List<TcsAmbienteConexao>();
	
	            return this._TcsAmbienteConexaoList;
	        }
	        set
	        {
	            if (this._TcsAmbienteConexaoList != value)
	            {
	                this._TcsAmbienteConexaoList = value;
	                this.RaisePropertyChanged("TcsAmbienteConexaoList");
	            }
	        }
	    }	 
		
	    private IEnumerable<TcsAmbienteServicoExcecao> _TcsAmbienteServicoExcecaoList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsAmbiente_TcsAmbienteServicoExcecao", "IdTcsAmbiente", "IdTcsAmbiente", IsForeignKey=false)]
	    [DataMember(Name = "TcsAmbienteServicoExcecaoList", EmitDefaultValue = true)]
	    public IEnumerable<TcsAmbienteServicoExcecao> TcsAmbienteServicoExcecaoList
	    {
	        get
	        {
	
	            if (this._TcsAmbienteServicoExcecaoList == null)
	            	this._TcsAmbienteServicoExcecaoList = new List<TcsAmbienteServicoExcecao>();
	
	            return this._TcsAmbienteServicoExcecaoList;
	        }
	        set
	        {
	            if (this._TcsAmbienteServicoExcecaoList != value)
	            {
	                this._TcsAmbienteServicoExcecaoList = value;
	                this.RaisePropertyChanged("TcsAmbienteServicoExcecaoList");
	            }
	        }
	    }	 
		
	    private IEnumerable<TcsAmbienteUsuarioAcesso> _TcsAmbienteUsuarioAcessoList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsAmbiente_TcsAmbienteUsuarioAcesso", "IdTcsAmbiente", "IdTcsAmbiente", IsForeignKey=false)]
	    [DataMember(Name = "TcsAmbienteUsuarioAcessoList", EmitDefaultValue = true)]
	    public IEnumerable<TcsAmbienteUsuarioAcesso> TcsAmbienteUsuarioAcessoList
	    {
	        get
	        {
	
	            if (this._TcsAmbienteUsuarioAcessoList == null)
	            	this._TcsAmbienteUsuarioAcessoList = new List<TcsAmbienteUsuarioAcesso>();
	
	            return this._TcsAmbienteUsuarioAcessoList;
	        }
	        set
	        {
	            if (this._TcsAmbienteUsuarioAcessoList != value)
	            {
	                this._TcsAmbienteUsuarioAcessoList = value;
	                this.RaisePropertyChanged("TcsAmbienteUsuarioAcessoList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
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

		

	[LinxPublicationView(PrimaryKeys="TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO", IsUpdatable=true, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Usuários];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsUsuarioAcesso];ReadOnly[false];Entities[TCS_USUARIO_ACESSO:IdTcsUsuarioAcesso];SubQueryInfo[Select 1 From #ParentAlias#.TCS_USUARIO_ACESSO_LISTA as #Alias#];EdmEntityName[TCS_USUARIO_ACESSO];EntityRelations[TCS_USUARIO_AUTENTICACAO(TCS_USUARIO_AUTENTICACAO)#TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)#TCS_AMBIENTE(TCS_AMBIENTE)#TCS_APLICACAO(TCS_APLICACAO)#TCS_APLICATIVO(TCS_APLICATIVO)#TCS_AMBIENTE1(TCS_AMBIENTE)];EdmParentEntityName[TCS_AMBIENTE];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsAmbienteUsuarioAcesso")]
	[Serializable()]
	public partial class TcsAmbienteUsuarioAcesso : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(AmbienteDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsAmbiente");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsAmbiente"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdTcsAmbiente));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsAmbiente
	         this.TcsAmbiente = (from r in context.GetTcsAmbienteByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For DescricaoAmbienteRelacionado
	    partial void OnDescricaoAmbienteRelacionadoChanging(System.String value);
	    partial void OnDescricaoAmbienteRelacionadoChanged();

	    private System.String _DescricaoAmbienteRelacionado;

	    [DataMember(Name = "DescricaoAmbienteRelacionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ambiente Relacionado", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbienteAdministrativo];LookUpTitle[Seleção de (Ambiente Relacionado)];LookUpQuery[executeLookUpTcsAmbienteAdministrativo];LookUpFinalize[finalizeLookUpTcsAmbienteAdministrativo];LookUpDisplayColumns[{\"IdTcsAmbienteRelacionado\" : \"\", \"IdLinxAmbienteRelacionado\" : \"\", \"NomeEmpresaAmbienteRelacionado\" : \"Empresa\", \"DescricaoAmbienteRelacionado\" : \"Ambiente\", \"DescricaoAplicacaoAmbienteRelacionado\" : \"Aplicação\"}];LookUpColumns[{\"IdTcsAmbienteRelacionado\" : false, \"IdLinxAmbienteRelacionado\" : false, \"NomeEmpresaAmbienteRelacionado\" : true, \"DescricaoAmbienteRelacionado\" : true, \"DescricaoAplicacaoAmbienteRelacionado\" : true}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE1.DESCRICAO_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#DescricaoAmbienteRelacionado#false##250:0##Ambiente#3#true##::LookUpTcsAmbienteAdministrativo##false#false###Linx.Framework.BV.Ambiente#IQueryable###true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.DESCRICAO_AMBIENTE")]
	    public System.String DescricaoAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _DescricaoAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescricaoAmbienteRelacionado != value)
	    	          {
	    	              this.ValidateProperty("DescricaoAmbienteRelacionado", value);
	    	              this.OnDescricaoAmbienteRelacionadoChanging(value);
	    	              this.RaiseDataMemberChanging("DescricaoAmbienteRelacionado");
	    	              this._DescricaoAmbienteRelacionado = value;
	    	              this.RaiseDataMemberChanged("DescricaoAmbienteRelacionado");
	    	              this.OnDescricaoAmbienteRelacionadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescricaoAplicacaoAmbienteRelacionado
	    partial void OnDescricaoAplicacaoAmbienteRelacionadoChanging(System.String value);
	    partial void OnDescricaoAplicacaoAmbienteRelacionadoChanged();

	    private System.String _DescricaoAplicacaoAmbienteRelacionado;

	    [DataMember(Name = "DescricaoAplicacaoAmbienteRelacionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicação Ambiente Relacionado", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbienteAdministrativo];LookUpTitle[Seleção de (Aplicação Ambiente Relacionado)];LookUpQuery[executeLookUpTcsAmbienteAdministrativo];LookUpFinalize[finalizeLookUpTcsAmbienteAdministrativo];LookUpDisplayColumns[{\"IdTcsAmbienteRelacionado\" : \"\", \"IdLinxAmbienteRelacionado\" : \"\", \"NomeEmpresaAmbienteRelacionado\" : \"Empresa\", \"DescricaoAmbienteRelacionado\" : \"Ambiente\", \"DescricaoAplicacaoAmbienteRelacionado\" : \"Aplicação\"}];LookUpColumns[{\"IdTcsAmbienteRelacionado\" : false, \"IdLinxAmbienteRelacionado\" : false, \"NomeEmpresaAmbienteRelacionado\" : true, \"DescricaoAmbienteRelacionado\" : true, \"DescricaoAplicacaoAmbienteRelacionado\" : true}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE1.TCS_APLICACAO.DESCRICAO_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#DescricaoAplicacaoAmbienteRelacionado#false##60:0##Aplicação#4#true##::LookUpTcsAmbienteAdministrativo##false#false###Linx.Framework.BV.Ambiente#IQueryable###true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.TCS_APLICACAO.DESCRICAO_APLICACAO")]
	    public System.String DescricaoAplicacaoAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _DescricaoAplicacaoAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescricaoAplicacaoAmbienteRelacionado != value)
	    	          {
	    	              this.ValidateProperty("DescricaoAplicacaoAmbienteRelacionado", value);
	    	              this.OnDescricaoAplicacaoAmbienteRelacionadoChanging(value);
	    	              this.RaiseDataMemberChanging("DescricaoAplicacaoAmbienteRelacionado");
	    	              this._DescricaoAplicacaoAmbienteRelacionado = value;
	    	              this.RaiseDataMemberChanged("DescricaoAplicacaoAmbienteRelacionado");
	    	              this.OnDescricaoAplicacaoAmbienteRelacionadoChanged();
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
	    [Display(Name = "ID Linx Ambiente", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacao];LookUpTitle[Seleção de (ID Linx Ambiente)];LookUpQuery[executeLookUpTcsUsuarioAutenticacao];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacao];LookUpDisplayColumns[{\"NomeUsuario\" : \"Nome\", \"NomeEmpresa\" : \"Grupo Econômico\", \"IdLinx\" : \"Id Grupo Econômico\", \"IdUsuario\" : \"Id Usuario\", \"UidUsuario\" : \"Uid Usuario\", \"NomeAutenticacao\" : \"Nome Autenticacao\"}];LookUpColumns[{\"NomeUsuario\" : true, \"NomeEmpresa\" : true, \"IdLinx\" : true, \"IdUsuario\" : false, \"UidUsuario\" : false, \"NomeAutenticacao\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdLinx#false##12:0##Id Grupo Econômico#2#true##::LookUpTcsUsuarioAutenticacao##true#false#TCS_USUARIO_AUTENTICACAO#TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.Ambiente#IQueryable###true#true", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
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
	    //Extensibility Partial Method Definitions For IdLinxAmbienteRelacionado
	    partial void OnIdLinxAmbienteRelacionadoChanging(System.Nullable<Int32> value);
	    partial void OnIdLinxAmbienteRelacionadoChanged();

	    private System.Nullable<Int32> _IdLinxAmbienteRelacionado;

	    [DataMember(Name = "IdLinxAmbienteRelacionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx1", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbienteAdministrativo];LookUpTitle[Seleção de (Id Linx1)];LookUpQuery[executeLookUpTcsAmbienteAdministrativo];LookUpFinalize[finalizeLookUpTcsAmbienteAdministrativo];LookUpDisplayColumns[{\"IdTcsAmbienteRelacionado\" : \"\", \"IdLinxAmbienteRelacionado\" : \"\", \"NomeEmpresaAmbienteRelacionado\" : \"Empresa\", \"DescricaoAmbienteRelacionado\" : \"Ambiente\", \"DescricaoAplicacaoAmbienteRelacionado\" : \"Aplicação\"}];LookUpColumns[{\"IdTcsAmbienteRelacionado\" : false, \"IdLinxAmbienteRelacionado\" : false, \"NomeEmpresaAmbienteRelacionado\" : true, \"DescricaoAmbienteRelacionado\" : true, \"DescricaoAplicacaoAmbienteRelacionado\" : true}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="int#IdLinxAmbienteRelacionado#false##12:0###1#false##::LookUpTcsAmbienteAdministrativo##false#false###Linx.Framework.BV.Ambiente#IQueryable###true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
	    public System.Nullable<Int32> IdLinxAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _IdLinxAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLinxAmbienteRelacionado != value)
	    	          {
	    	              this.ValidateProperty("IdLinxAmbienteRelacionado", value);
	    	              this.OnIdLinxAmbienteRelacionadoChanging(value);
	    	              this.RaiseDataMemberChanging("IdLinxAmbienteRelacionado");
	    	              this._IdLinxAmbienteRelacionado = value;
	    	              this.RaiseDataMemberChanged("IdLinxAmbienteRelacionado");
	    	              this.OnIdLinxAmbienteRelacionadoChanged();
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
	    [Display(Name = "Ambiente", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.ID_TCS_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.ID_TCS_AMBIENTE")]
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
	    //Extensibility Partial Method Definitions For IdTcsAmbienteRelacionado
	    partial void OnIdTcsAmbienteRelacionadoChanging(System.Nullable<Int32> value);
	    partial void OnIdTcsAmbienteRelacionadoChanged();

	    private System.Nullable<Int32> _IdTcsAmbienteRelacionado;

	    [DataMember(Name = "IdTcsAmbienteRelacionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente1", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbienteAdministrativo];LookUpTitle[Seleção de (Id Tcs Ambiente1)];LookUpQuery[executeLookUpTcsAmbienteAdministrativo];LookUpFinalize[finalizeLookUpTcsAmbienteAdministrativo];LookUpDisplayColumns[{\"IdTcsAmbienteRelacionado\" : \"\", \"IdLinxAmbienteRelacionado\" : \"\", \"NomeEmpresaAmbienteRelacionado\" : \"Empresa\", \"DescricaoAmbienteRelacionado\" : \"Ambiente\", \"DescricaoAplicacaoAmbienteRelacionado\" : \"Aplicação\"}];LookUpColumns[{\"IdTcsAmbienteRelacionado\" : false, \"IdLinxAmbienteRelacionado\" : false, \"NomeEmpresaAmbienteRelacionado\" : true, \"DescricaoAmbienteRelacionado\" : true, \"DescricaoAplicacaoAmbienteRelacionado\" : true}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE1.ID_TCS_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="int#IdTcsAmbienteRelacionado#false##12:0###0#false##::LookUpTcsAmbienteAdministrativo##false#false###Linx.Framework.BV.Ambiente#IQueryable###true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.ID_TCS_AMBIENTE")]
	    public System.Nullable<Int32> IdTcsAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _IdTcsAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAmbienteRelacionado != value)
	    	          {
	    	              this.ValidateProperty("IdTcsAmbienteRelacionado", value);
	    	              this.OnIdTcsAmbienteRelacionadoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsAmbienteRelacionado");
	    	              this._IdTcsAmbienteRelacionado = value;
	    	              this.RaiseDataMemberChanged("IdTcsAmbienteRelacionado");
	    	              this.OnIdTcsAmbienteRelacionadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsUsuarioAcesso
	    partial void OnIdTcsUsuarioAcessoChanging(Int32 value);
	    partial void OnIdTcsUsuarioAcessoChanged();

	    private Int32 _IdTcsUsuarioAcesso;

	    [DataMember(IsRequired = true, Name = "IdTcsUsuarioAcesso", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Acesso", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO")]
	    public Int32 IdTcsUsuarioAcesso
	    {
	    	    get
	    	    {
	    	          return _IdTcsUsuarioAcesso;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsUsuarioAcesso != value)
	    	          {
	    	              this.ValidateProperty("IdTcsUsuarioAcesso", value);
	    	              this.OnIdTcsUsuarioAcessoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsUsuarioAcesso");
	    	              this._IdTcsUsuarioAcesso = value;
	    	              this.RaiseDataMemberChanged("IdTcsUsuarioAcesso");
	    	              this.OnIdTcsUsuarioAcessoChanged();
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
	    [Display(Name = "Id Usuario", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacao];LookUpTitle[Seleção de (Id Usuario)];LookUpQuery[executeLookUpTcsUsuarioAutenticacao];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacao];LookUpDisplayColumns[{\"NomeUsuario\" : \"Nome\", \"NomeEmpresa\" : \"Grupo Econômico\", \"IdLinx\" : \"Id Grupo Econômico\", \"IdUsuario\" : \"Id Usuario\", \"UidUsuario\" : \"Uid Usuario\", \"NomeAutenticacao\" : \"Nome Autenticacao\"}];LookUpColumns[{\"NomeUsuario\" : true, \"NomeEmpresa\" : true, \"IdLinx\" : true, \"IdUsuario\" : false, \"UidUsuario\" : false, \"NomeAutenticacao\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdUsuario#true##24:0##Id Usuario#3#false##::LookUpTcsUsuarioAutenticacao##true#false#TCS_USUARIO_AUTENTICACAO#TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.Ambiente#IQueryable###true#true", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.ID_USUARIO")]
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
	    //Extensibility Partial Method Definitions For IndicaAdministrador
	    partial void OnIndicaAdministradorChanging(Boolean value);
	    partial void OnIndicaAdministradorChanged();

	    private Boolean _IndicaAdministrador;

	    [DataMember(IsRequired = true, Name = "IndicaAdministrador", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Administrador", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.INDICA_ADMINISTRADOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.INDICA_ADMINISTRADOR")]
	    public Boolean IndicaAdministrador
	    {
	    	    get
	    	    {
	    	          return _IndicaAdministrador;
	    	    }
	    	    set
	    	    {
	    	          if (this._IndicaAdministrador != value)
	    	          {
	    	              this.ValidateProperty("IndicaAdministrador", value);
	    	              this.OnIndicaAdministradorChanging(value);
	    	              this.RaiseDataMemberChanging("IndicaAdministrador");
	    	              this._IndicaAdministrador = value;
	    	              this.RaiseDataMemberChanged("IndicaAdministrador");
	    	              this.OnIndicaAdministradorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IndicaMultiGpecon
	    partial void OnIndicaMultiGpeconChanging(Boolean value);
	    partial void OnIndicaMultiGpeconChanged();

	    private Boolean _IndicaMultiGpecon;

	    [DataMember(IsRequired = true, Name = "IndicaMultiGpecon", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Multi Grupo Econômico", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.INDICA_MULTI_GPECON];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.INDICA_MULTI_GPECON")]
	    public Boolean IndicaMultiGpecon
	    {
	    	    get
	    	    {
	    	          return _IndicaMultiGpecon;
	    	    }
	    	    set
	    	    {
	    	          if (this._IndicaMultiGpecon != value)
	    	          {
	    	              this.ValidateProperty("IndicaMultiGpecon", value);
	    	              this.OnIndicaMultiGpeconChanging(value);
	    	              this.RaiseDataMemberChanging("IndicaMultiGpecon");
	    	              this._IndicaMultiGpecon = value;
	    	              this.RaiseDataMemberChanged("IndicaMultiGpecon");
	    	              this.OnIndicaMultiGpeconChanged();
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
	    [Display(Name = "Empresa (Id Linx)", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacao];LookUpTitle[Seleção de (Empresa (Id Linx))];LookUpQuery[executeLookUpTcsUsuarioAutenticacao];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacao];LookUpDisplayColumns[{\"NomeUsuario\" : \"Nome\", \"NomeEmpresa\" : \"Grupo Econômico\", \"IdLinx\" : \"Id Grupo Econômico\", \"IdUsuario\" : \"Id Usuario\", \"UidUsuario\" : \"Uid Usuario\", \"NomeAutenticacao\" : \"Nome Autenticacao\"}];LookUpColumns[{\"NomeUsuario\" : true, \"NomeEmpresa\" : true, \"IdLinx\" : true, \"IdUsuario\" : false, \"UidUsuario\" : false, \"NomeAutenticacao\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeEmpresa#false##250:0##Grupo Econômico#1#true##::LookUpTcsUsuarioAutenticacao##true#false#TCS_USUARIO_AUTENTICACAO#TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.Ambiente#IQueryable###true#true", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
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
	    //Extensibility Partial Method Definitions For NomeEmpresaAmbienteRelacionado
	    partial void OnNomeEmpresaAmbienteRelacionadoChanging(System.String value);
	    partial void OnNomeEmpresaAmbienteRelacionadoChanged();

	    private System.String _NomeEmpresaAmbienteRelacionado;

	    [DataMember(Name = "NomeEmpresaAmbienteRelacionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Empresa Ambiente Relacionado", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbienteAdministrativo];LookUpTitle[Seleção de (Empresa Ambiente Relacionado)];LookUpQuery[executeLookUpTcsAmbienteAdministrativo];LookUpFinalize[finalizeLookUpTcsAmbienteAdministrativo];LookUpDisplayColumns[{\"IdTcsAmbienteRelacionado\" : \"\", \"IdLinxAmbienteRelacionado\" : \"\", \"NomeEmpresaAmbienteRelacionado\" : \"Empresa\", \"DescricaoAmbienteRelacionado\" : \"Ambiente\", \"DescricaoAplicacaoAmbienteRelacionado\" : \"Aplicação\"}];LookUpColumns[{\"IdTcsAmbienteRelacionado\" : false, \"IdLinxAmbienteRelacionado\" : false, \"NomeEmpresaAmbienteRelacionado\" : true, \"DescricaoAmbienteRelacionado\" : true, \"DescricaoAplicacaoAmbienteRelacionado\" : true}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#NomeEmpresaAmbienteRelacionado#false##250:0##Empresa#2#true##::LookUpTcsAmbienteAdministrativo##false#false###Linx.Framework.BV.Ambiente#IQueryable###true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
	    public System.String NomeEmpresaAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _NomeEmpresaAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeEmpresaAmbienteRelacionado != value)
	    	          {
	    	              this.ValidateProperty("NomeEmpresaAmbienteRelacionado", value);
	    	              this.OnNomeEmpresaAmbienteRelacionadoChanging(value);
	    	              this.RaiseDataMemberChanging("NomeEmpresaAmbienteRelacionado");
	    	              this._NomeEmpresaAmbienteRelacionado = value;
	    	              this.RaiseDataMemberChanged("NomeEmpresaAmbienteRelacionado");
	    	              this.OnNomeEmpresaAmbienteRelacionadoChanged();
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
	    [Display(Name = "Uid Usuario", Description="", Order = 26, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacao];LookUpTitle[Seleção de (Uid Usuario)];LookUpQuery[executeLookUpTcsUsuarioAutenticacao];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacao];LookUpDisplayColumns[{\"NomeUsuario\" : \"Nome\", \"NomeEmpresa\" : \"Grupo Econômico\", \"IdLinx\" : \"Id Grupo Econômico\", \"IdUsuario\" : \"Id Usuario\", \"UidUsuario\" : \"Uid Usuario\", \"NomeAutenticacao\" : \"Nome Autenticacao\"}];LookUpColumns[{\"NomeUsuario\" : true, \"NomeEmpresa\" : true, \"IdLinx\" : true, \"IdUsuario\" : false, \"UidUsuario\" : false, \"NomeAutenticacao\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.UID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Guid#UidUsuario#false##12:0##Uid Usuario#4#false##::LookUpTcsUsuarioAutenticacao##true#false#TCS_USUARIO_AUTENTICACAO#TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.Ambiente#IQueryable###true#true", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.UID_USUARIO")]
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
	    //Extensibility Partial Method Definitions For NomeAutenticacao
	    partial void OnNomeAutenticacaoChanging(String value);
	    partial void OnNomeAutenticacaoChanged();

	    private String _NomeAutenticacao;

	    [DataMember(Name = "NomeAutenticacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário Autenticação", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacao];LookUpTitle[Seleção de (Usuário Autenticação)];LookUpQuery[executeLookUpTcsUsuarioAutenticacao];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacao];LookUpDisplayColumns[{\"NomeUsuario\" : \"Nome\", \"NomeEmpresa\" : \"Grupo Econômico\", \"IdLinx\" : \"Id Grupo Econômico\", \"IdUsuario\" : \"Id Usuario\", \"UidUsuario\" : \"Uid Usuario\", \"NomeAutenticacao\" : \"Nome Autenticacao\"}];LookUpColumns[{\"NomeUsuario\" : true, \"NomeEmpresa\" : true, \"IdLinx\" : true, \"IdUsuario\" : false, \"UidUsuario\" : false, \"NomeAutenticacao\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeAutenticacao#false##2500##Nome Autenticacao#5#false##::LookUpTcsUsuarioAutenticacao##true#false#TCS_USUARIO_AUTENTICACAO#TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.Ambiente#IQueryable###true#true", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO")]
	    public String NomeAutenticacao
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
	    partial void OnNomeUsuarioChanging(String value);
	    partial void OnNomeUsuarioChanged();

	    private String _NomeUsuario;

	    [DataMember(Name = "NomeUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacao];LookUpTitle[Seleção de (Usuário)];LookUpQuery[executeLookUpTcsUsuarioAutenticacao];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacao];LookUpDisplayColumns[{\"NomeUsuario\" : \"Nome\", \"NomeEmpresa\" : \"Grupo Econômico\", \"IdLinx\" : \"Id Grupo Econômico\", \"IdUsuario\" : \"Id Usuario\", \"UidUsuario\" : \"Uid Usuario\", \"NomeAutenticacao\" : \"Nome Autenticacao\"}];LookUpColumns[{\"NomeUsuario\" : true, \"NomeEmpresa\" : true, \"IdLinx\" : true, \"IdUsuario\" : false, \"UidUsuario\" : false, \"NomeAutenticacao\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="String#NomeUsuario#false##250:0##Nome#0#true##::LookUpTcsUsuarioAutenticacao##true#false#TCS_USUARIO_AUTENTICACAO#TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.Ambiente#IQueryable###true#true", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO")]
	    public String NomeUsuario
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

	    private Int32 _TemporaryIdTcsUsuarioAcesso;
	    [DataMember(Name = "TemporaryIdTcsUsuarioAcesso", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Acesso (Tmp)", Description="Temporary Key", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdTcsUsuarioAcesso
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTcsUsuarioAcesso.IsNullOrEmpty())
	    	                this._TemporaryIdTcsUsuarioAcesso = this._IdTcsUsuarioAcesso;
	    	          return this._TemporaryIdTcsUsuarioAcesso;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTcsUsuarioAcesso != value)
	    	              this._TemporaryIdTcsUsuarioAcesso = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsAmbiente _TcsAmbiente;
	    [DataMember(Name = "TcsAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsAmbiente_TcsAmbienteUsuarioAcesso", "IdTcsAmbiente", "IdTcsAmbiente", IsForeignKey=true)]
	    public TcsAmbiente TcsAmbiente
	    {
	        get
	        {
	            return this._TcsAmbiente;
	        }
	        set
	        {
	            if (this._TcsAmbiente != value)
	            {
	                this._TcsAmbiente = value;
	                this.RaisePropertyChanged("TcsAmbienteList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_USUARIO_ACESSO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_USUARIO_ACESSO), QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_ACESSO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_ACESSO.INDICA_MULTI_GPECON", Source = "IndicaMultiGpecon", Target = "INDICA_MULTI_GPECON", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_ACESSO", RelationPropertyName = "TCS_USUARIO_ACESSO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_ACESSO.INDICA_ADMINISTRADOR", Source = "IndicaAdministrador", Target = "INDICA_ADMINISTRADOR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_ACESSO", RelationPropertyName = "TCS_USUARIO_ACESSO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO", Source = "IdTcsUsuarioAcesso", Target = "ID_TCS_USUARIO_ACESSO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_ACESSO", RelationPropertyName = "TCS_USUARIO_ACESSO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_ACESSO.TCS_AMBIENTE.ID_TCS_AMBIENTE", Source = "IdTcsAmbiente", Target = "ID_TCS_AMBIENTE", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE", RelationPropertyName = "TCS_AMBIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.ID_TCS_AMBIENTE", Source = "IdTcsAmbienteRelacionado", Target = "ID_TCS_AMBIENTE", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE", RelationPropertyName = "TCS_AMBIENTE1" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });

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

		

	[LinxPublicationView(PrimaryKeys="TCS_AMBIENTE_CONEXAO.ID_TCS_AMBIENTE_CONEXAO", IsUpdatable=true, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Providers];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsAmbienteConexao];ReadOnly[false];Entities[TCS_AMBIENTE_CONEXAO:IdTcsAmbienteConexao|TCS_APLICATIVO_CONEXAO:IdTcsAplicativoConexao|TCS_BANCO_SERVIDOR:IdTcsBancoServidor];SubQueryInfo[Select 1 From #ParentAlias#.TCS_AMBIENTE_CONEXAO_LISTA as #Alias#];EdmEntityName[TCS_AMBIENTE_CONEXAO];EntityRelations[TCS_AMBIENTE(TCS_AMBIENTE)#TCS_APLICACAO(TCS_APLICACAO)#TCS_APLICATIVO(TCS_APLICATIVO)#TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)#TCS_BANCO_SERVIDOR(TCS_BANCO_SERVIDOR)#TCS_APLICATIVO_CONEXAO(TCS_APLICATIVO_CONEXAO)#TCS_CONEXAO_DB(TCS_CONEXAO_DB)];EdmParentEntityName[TCS_AMBIENTE];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsAmbienteConexao")]
	[Serializable()]
	public partial class TcsAmbienteConexao : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(AmbienteDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsAmbiente");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsAmbiente"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdTcsAmbiente));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsAmbiente
	         this.TcsAmbiente = (from r in context.GetTcsAmbienteByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For DescricaoBancoServidor
	    partial void OnDescricaoBancoServidorChanging(System.String value);
	    partial void OnDescricaoBancoServidorChanged();

	    private System.String _DescricaoBancoServidor;

	    [DataMember(IsRequired = true, Name = "DescricaoBancoServidor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Conexão Banco/Servidor", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(80)]
	    [FunctionalPoint("Precision[80:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsBancoServidor];LookUpTitle[Seleção de (Conexão Banco/Servidor)];LookUpQuery[executeLookUpTcsBancoServidor];LookUpFinalize[finalizeLookUpTcsBancoServidor];LookUpDisplayColumns[{\"IdTcsBancoServidor\" : \"Id Tcs Banco Servidor\", \"DescricaoBancoServidor\" : \"Descrição\", \"NomeServidor\" : \"Servidor\", \"NomeBanco\" : \"Banco de Dados\", \"LxTipoServidor\" : \"Lx Tipo Servidor\", \"StringConexao\" : \"String Conexao\"}];LookUpColumns[{\"IdTcsBancoServidor\" : false, \"DescricaoBancoServidor\" : true, \"NomeServidor\" : true, \"NomeBanco\" : true, \"LxTipoServidor\" : false, \"StringConexao\" : false}];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.DESCRICAO_BANCO_SERVIDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoBancoServidor#false##80:0##Descrição#1#true##::LookUpTcsBancoServidor##false#false#TCS_BANCO_SERVIDOR#TCS_BANCO_SERVIDOR#Linx.Framework.BV.Ambiente#IQueryable###true#false", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.DESCRICAO_BANCO_SERVIDOR")]
	    public System.String DescricaoBancoServidor
	    {
	    	    get
	    	    {
	    	          return _DescricaoBancoServidor;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescricaoBancoServidor != value)
	    	          {
	    	              this.ValidateProperty("DescricaoBancoServidor", value);
	    	              this.OnDescricaoBancoServidorChanging(value);
	    	              this.RaiseDataMemberChanging("DescricaoBancoServidor");
	    	              this._DescricaoBancoServidor = value;
	    	              this.RaiseDataMemberChanged("DescricaoBancoServidor");
	    	              this.OnDescricaoBancoServidorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdConexaoDb
	    partial void OnIdConexaoDbChanging(Int32 value);
	    partial void OnIdConexaoDbChanged();

	    private Int32 _IdConexaoDb;

	    [DataMember(IsRequired = true, Name = "IdConexaoDb", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Conexao Db", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicativoConexao];LookUpTitle[Seleção de (Id Conexao Db)];LookUpQuery[executeLookUpTcsAplicativoConexao];LookUpFinalize[finalizeLookUpTcsAplicativoConexao];LookUpDisplayColumns[{\"IdTcsAplicativoConexao\" : \"Id Tcs Aplicativo Conexao\", \"IdConexaoDb\" : \"Id Conexao Db\", \"NomeConexao\" : \"Nome Provider BM\"}];LookUpColumns[{\"IdTcsAplicativoConexao\" : false, \"IdConexaoDb\" : false, \"NomeConexao\" : true}];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB.ID_CONEXAO_DB];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdConexaoDb#false##12:0##Id Conexao Db#1#false##::LookUpTcsAplicativoConexao##true#false#TCS_APLICATIVO_CONEXAO#TCS_APLICATIVO_CONEXAO#Linx.Framework.BV.Ambiente#IQueryable###true#false", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB.ID_CONEXAO_DB")]
	    public Int32 IdConexaoDb
	    {
	    	    get
	    	    {
	    	          return _IdConexaoDb;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdConexaoDb != value)
	    	          {
	    	              this.ValidateProperty("IdConexaoDb", value);
	    	              this.OnIdConexaoDbChanging(value);
	    	              this.RaiseDataMemberChanging("IdConexaoDb");
	    	              this._IdConexaoDb = value;
	    	              this.RaiseDataMemberChanged("IdConexaoDb");
	    	              this.OnIdConexaoDbChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLinx
	    partial void OnIdLinxChanging(Int32 value);
	    partial void OnIdLinxChanged();

	    private Int32 _IdLinx;

	    [DataMember(Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
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
	    [Display(Name = "Ambiente", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.ID_TCS_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.ID_TCS_AMBIENTE")]
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
	    //Extensibility Partial Method Definitions For IdTcsAmbienteConexao
	    partial void OnIdTcsAmbienteConexaoChanging(Int32 value);
	    partial void OnIdTcsAmbienteConexaoChanged();

	    private Int32 _IdTcsAmbienteConexao;

	    [DataMember(IsRequired = true, Name = "IdTcsAmbienteConexao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente Conexao", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_AMBIENTE_CONEXAO.ID_TCS_AMBIENTE_CONEXAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE_CONEXAO.ID_TCS_AMBIENTE_CONEXAO")]
	    public Int32 IdTcsAmbienteConexao
	    {
	    	    get
	    	    {
	    	          return _IdTcsAmbienteConexao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAmbienteConexao != value)
	    	          {
	    	              this.ValidateProperty("IdTcsAmbienteConexao", value);
	    	              this.OnIdTcsAmbienteConexaoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsAmbienteConexao");
	    	              this._IdTcsAmbienteConexao = value;
	    	              this.RaiseDataMemberChanged("IdTcsAmbienteConexao");
	    	              this.OnIdTcsAmbienteConexaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsAplicativoConexao
	    partial void OnIdTcsAplicativoConexaoChanging(Int32 value);
	    partial void OnIdTcsAplicativoConexaoChanged();

	    private Int32 _IdTcsAplicativoConexao;

	    [DataMember(IsRequired = true, Name = "IdTcsAplicativoConexao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Aplicativo Conexao", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicativoConexao];LookUpTitle[Seleção de (Id Tcs Aplicativo Conexao)];LookUpQuery[executeLookUpTcsAplicativoConexao];LookUpFinalize[finalizeLookUpTcsAplicativoConexao];LookUpDisplayColumns[{\"IdTcsAplicativoConexao\" : \"Id Tcs Aplicativo Conexao\", \"IdConexaoDb\" : \"Id Conexao Db\", \"NomeConexao\" : \"Nome Provider BM\"}];LookUpColumns[{\"IdTcsAplicativoConexao\" : false, \"IdConexaoDb\" : false, \"NomeConexao\" : true}];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_APLICATIVO_CONEXAO.ID_TCS_APLICATIVO_CONEXAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdTcsAplicativoConexao#true##12:0##Id Tcs Aplicativo Conexao#0#false##::LookUpTcsAplicativoConexao##true#false#TCS_APLICATIVO_CONEXAO#TCS_APLICATIVO_CONEXAO#Linx.Framework.BV.Ambiente#IQueryable###true#false", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_APLICATIVO_CONEXAO.ID_TCS_APLICATIVO_CONEXAO")]
	    public Int32 IdTcsAplicativoConexao
	    {
	    	    get
	    	    {
	    	          return _IdTcsAplicativoConexao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAplicativoConexao != value)
	    	          {
	    	              this.ValidateProperty("IdTcsAplicativoConexao", value);
	    	              this.OnIdTcsAplicativoConexaoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsAplicativoConexao");
	    	              this._IdTcsAplicativoConexao = value;
	    	              this.RaiseDataMemberChanged("IdTcsAplicativoConexao");
	    	              this.OnIdTcsAplicativoConexaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsBancoServidor
	    partial void OnIdTcsBancoServidorChanging(Int32 value);
	    partial void OnIdTcsBancoServidorChanged();

	    private Int32 _IdTcsBancoServidor;

	    [DataMember(IsRequired = true, Name = "IdTcsBancoServidor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Banco Servidor", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsBancoServidor];LookUpTitle[Seleção de (Id Tcs Banco Servidor)];LookUpQuery[executeLookUpTcsBancoServidor];LookUpFinalize[finalizeLookUpTcsBancoServidor];LookUpDisplayColumns[{\"IdTcsBancoServidor\" : \"Id Tcs Banco Servidor\", \"DescricaoBancoServidor\" : \"Descrição\", \"NomeServidor\" : \"Servidor\", \"NomeBanco\" : \"Banco de Dados\", \"LxTipoServidor\" : \"Lx Tipo Servidor\", \"StringConexao\" : \"String Conexao\"}];LookUpColumns[{\"IdTcsBancoServidor\" : false, \"DescricaoBancoServidor\" : true, \"NomeServidor\" : true, \"NomeBanco\" : true, \"LxTipoServidor\" : false, \"StringConexao\" : false}];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.ID_TCS_BANCO_SERVIDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdTcsBancoServidor#true##12:0##Id Tcs Banco Servidor#0#false##::LookUpTcsBancoServidor##false#false#TCS_BANCO_SERVIDOR#TCS_BANCO_SERVIDOR#Linx.Framework.BV.Ambiente#IQueryable###true#false", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.ID_TCS_BANCO_SERVIDOR")]
	    public Int32 IdTcsBancoServidor
	    {
	    	    get
	    	    {
	    	          return _IdTcsBancoServidor;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsBancoServidor != value)
	    	          {
	    	              this.ValidateProperty("IdTcsBancoServidor", value);
	    	              this.OnIdTcsBancoServidorChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsBancoServidor");
	    	              this._IdTcsBancoServidor = value;
	    	              this.RaiseDataMemberChanged("IdTcsBancoServidor");
	    	              this.OnIdTcsBancoServidorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoServidor
	    partial void OnLxTipoServidorChanging(Byte value);
	    partial void OnLxTipoServidorChanged();

	    private Byte _LxTipoServidor;

	    [DataMember(IsRequired = true, Name = "LxTipoServidor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo Servidor", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoServidor];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsBancoServidor];LookUpTitle[Seleção de (Tipo Servidor)];LookUpQuery[executeLookUpTcsBancoServidor];LookUpFinalize[finalizeLookUpTcsBancoServidor];LookUpDisplayColumns[{\"IdTcsBancoServidor\" : \"Id Tcs Banco Servidor\", \"DescricaoBancoServidor\" : \"Descrição\", \"NomeServidor\" : \"Servidor\", \"NomeBanco\" : \"Banco de Dados\", \"LxTipoServidor\" : \"Lx Tipo Servidor\", \"StringConexao\" : \"String Conexao\"}];LookUpColumns[{\"IdTcsBancoServidor\" : false, \"DescricaoBancoServidor\" : true, \"NomeServidor\" : true, \"NomeBanco\" : true, \"LxTipoServidor\" : false, \"StringConexao\" : false}];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.LX_TIPO_SERVIDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Byte#LxTipoServidor#false##3:0##Lx Tipo Servidor#4#false##::LookUpTcsBancoServidor##false#false#TCS_BANCO_SERVIDOR#TCS_BANCO_SERVIDOR#Linx.Framework.BV.Ambiente#IQueryable###true#false", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.LX_TIPO_SERVIDOR")]
	    public Byte LxTipoServidor
	    {
	    	    get
	    	    {
	    	          return _LxTipoServidor;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoServidor != value)
	    	          {
	    	              this.ValidateProperty("LxTipoServidor", value);
	    	              this.OnLxTipoServidorChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoServidor");
	    	              this._LxTipoServidor = value;
	    	              this.RaiseDataMemberChanged("LxTipoServidor");
	    	              this.OnLxTipoServidorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeBanco
	    partial void OnNomeBancoChanging(System.String value);
	    partial void OnNomeBancoChanged();

	    private System.String _NomeBanco;

	    [DataMember(IsRequired = true, Name = "NomeBanco", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Banco de Dados", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsBancoServidor];LookUpTitle[Seleção de (Banco de Dados)];LookUpQuery[executeLookUpTcsBancoServidor];LookUpFinalize[finalizeLookUpTcsBancoServidor];LookUpDisplayColumns[{\"IdTcsBancoServidor\" : \"Id Tcs Banco Servidor\", \"DescricaoBancoServidor\" : \"Descrição\", \"NomeServidor\" : \"Servidor\", \"NomeBanco\" : \"Banco de Dados\", \"LxTipoServidor\" : \"Lx Tipo Servidor\", \"StringConexao\" : \"String Conexao\"}];LookUpColumns[{\"IdTcsBancoServidor\" : false, \"DescricaoBancoServidor\" : true, \"NomeServidor\" : true, \"NomeBanco\" : true, \"LxTipoServidor\" : false, \"StringConexao\" : false}];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.NOME_BANCO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeBanco#false##250:0##Banco de Dados#3#true##::LookUpTcsBancoServidor##false#false#TCS_BANCO_SERVIDOR#TCS_BANCO_SERVIDOR#Linx.Framework.BV.Ambiente#IQueryable###true#false", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.NOME_BANCO")]
	    public System.String NomeBanco
	    {
	    	    get
	    	    {
	    	          return _NomeBanco;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeBanco != value)
	    	          {
	    	              this.ValidateProperty("NomeBanco", value);
	    	              this.OnNomeBancoChanging(value);
	    	              this.RaiseDataMemberChanging("NomeBanco");
	    	              this._NomeBanco = value;
	    	              this.RaiseDataMemberChanged("NomeBanco");
	    	              this.OnNomeBancoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeConexao
	    partial void OnNomeConexaoChanging(System.String value);
	    partial void OnNomeConexaoChanged();

	    private System.String _NomeConexao;

	    [DataMember(IsRequired = true, Name = "NomeConexao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Provider BM", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicativoConexao];LookUpTitle[Seleção de (Nome Provider BM)];LookUpQuery[executeLookUpTcsAplicativoConexao];LookUpFinalize[finalizeLookUpTcsAplicativoConexao];LookUpDisplayColumns[{\"IdTcsAplicativoConexao\" : \"Id Tcs Aplicativo Conexao\", \"IdConexaoDb\" : \"Id Conexao Db\", \"NomeConexao\" : \"Nome Provider BM\"}];LookUpColumns[{\"IdTcsAplicativoConexao\" : false, \"IdConexaoDb\" : false, \"NomeConexao\" : true}];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB.NOME_CONEXAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeConexao#false##2500##Nome Provider BM#2#true##::LookUpTcsAplicativoConexao##true#false#TCS_APLICATIVO_CONEXAO#TCS_APLICATIVO_CONEXAO#Linx.Framework.BV.Ambiente#IQueryable###true#false", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB.NOME_CONEXAO")]
	    public System.String NomeConexao
	    {
	    	    get
	    	    {
	    	          return _NomeConexao;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeConexao != value)
	    	          {
	    	              this.ValidateProperty("NomeConexao", value);
	    	              this.OnNomeConexaoChanging(value);
	    	              this.RaiseDataMemberChanging("NomeConexao");
	    	              this._NomeConexao = value;
	    	              this.RaiseDataMemberChanged("NomeConexao");
	    	              this.OnNomeConexaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeServidor
	    partial void OnNomeServidorChanging(System.String value);
	    partial void OnNomeServidorChanged();

	    private System.String _NomeServidor;

	    [DataMember(IsRequired = true, Name = "NomeServidor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Servidor", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsBancoServidor];LookUpTitle[Seleção de (Servidor)];LookUpQuery[executeLookUpTcsBancoServidor];LookUpFinalize[finalizeLookUpTcsBancoServidor];LookUpDisplayColumns[{\"IdTcsBancoServidor\" : \"Id Tcs Banco Servidor\", \"DescricaoBancoServidor\" : \"Descrição\", \"NomeServidor\" : \"Servidor\", \"NomeBanco\" : \"Banco de Dados\", \"LxTipoServidor\" : \"Lx Tipo Servidor\", \"StringConexao\" : \"String Conexao\"}];LookUpColumns[{\"IdTcsBancoServidor\" : false, \"DescricaoBancoServidor\" : true, \"NomeServidor\" : true, \"NomeBanco\" : true, \"LxTipoServidor\" : false, \"StringConexao\" : false}];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.NOME_SERVIDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeServidor#false##250:0##Servidor#2#true##::LookUpTcsBancoServidor##false#false#TCS_BANCO_SERVIDOR#TCS_BANCO_SERVIDOR#Linx.Framework.BV.Ambiente#IQueryable###true#false", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.NOME_SERVIDOR")]
	    public System.String NomeServidor
	    {
	    	    get
	    	    {
	    	          return _NomeServidor;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeServidor != value)
	    	          {
	    	              this.ValidateProperty("NomeServidor", value);
	    	              this.OnNomeServidorChanging(value);
	    	              this.RaiseDataMemberChanging("NomeServidor");
	    	              this._NomeServidor = value;
	    	              this.RaiseDataMemberChanged("NomeServidor");
	    	              this.OnNomeServidorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringConexao
	    partial void OnStringConexaoChanging(System.String value);
	    partial void OnStringConexaoChanged();

	    private System.String _StringConexao;

	    [DataMember(IsRequired = true, Name = "StringConexao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Conexao", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(1000)]
	    [FunctionalPoint("Precision[1000:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsBancoServidor];LookUpTitle[Seleção de (String Conexao)];LookUpQuery[executeLookUpTcsBancoServidor];LookUpFinalize[finalizeLookUpTcsBancoServidor];LookUpDisplayColumns[{\"IdTcsBancoServidor\" : \"Id Tcs Banco Servidor\", \"DescricaoBancoServidor\" : \"Descrição\", \"NomeServidor\" : \"Servidor\", \"NomeBanco\" : \"Banco de Dados\", \"LxTipoServidor\" : \"Lx Tipo Servidor\", \"StringConexao\" : \"String Conexao\"}];LookUpColumns[{\"IdTcsBancoServidor\" : false, \"DescricaoBancoServidor\" : true, \"NomeServidor\" : true, \"NomeBanco\" : true, \"LxTipoServidor\" : false, \"StringConexao\" : false}];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.STRING_CONEXAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#StringConexao#false##1000:0##String Conexao#5#false##::LookUpTcsBancoServidor##false#false#TCS_BANCO_SERVIDOR#TCS_BANCO_SERVIDOR#Linx.Framework.BV.Ambiente#IQueryable###true#false", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.STRING_CONEXAO")]
	    public System.String StringConexao
	    {
	    	    get
	    	    {
	    	          return _StringConexao;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringConexao != value)
	    	          {
	    	              this.ValidateProperty("StringConexao", value);
	    	              this.OnStringConexaoChanging(value);
	    	              this.RaiseDataMemberChanging("StringConexao");
	    	              this._StringConexao = value;
	    	              this.RaiseDataMemberChanged("StringConexao");
	    	              this.OnStringConexaoChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIdTcsAmbienteConexao;
	    [DataMember(Name = "TemporaryIdTcsAmbienteConexao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente Conexao (Tmp)", Description="Temporary Key", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdTcsAmbienteConexao
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTcsAmbienteConexao.IsNullOrEmpty())
	    	                this._TemporaryIdTcsAmbienteConexao = this._IdTcsAmbienteConexao;
	    	          return this._TemporaryIdTcsAmbienteConexao;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTcsAmbienteConexao != value)
	    	              this._TemporaryIdTcsAmbienteConexao = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsAmbiente _TcsAmbiente;
	    [DataMember(Name = "TcsAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsAmbiente_TcsAmbienteConexao", "IdTcsAmbiente", "IdTcsAmbiente", IsForeignKey=true)]
	    public TcsAmbiente TcsAmbiente
	    {
	        get
	        {
	            return this._TcsAmbiente;
	        }
	        set
	        {
	            if (this._TcsAmbiente != value)
	            {
	                this._TcsAmbiente = value;
	                this.RaisePropertyChanged("TcsAmbienteList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_AMBIENTE_CONEXAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_AMBIENTE_CONEXAO), QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE_CONEXAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_AMBIENTE_CONEXAO.ID_TCS_AMBIENTE_CONEXAO", Source = "IdTcsAmbienteConexao", Target = "ID_TCS_AMBIENTE_CONEXAO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE_CONEXAO", RelationPropertyName = "TCS_AMBIENTE_CONEXAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.ID_TCS_AMBIENTE", Source = "IdTcsAmbiente", Target = "ID_TCS_AMBIENTE", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE", RelationPropertyName = "TCS_AMBIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.ID_TCS_BANCO_SERVIDOR", Source = "IdTcsBancoServidor", Target = "ID_TCS_BANCO_SERVIDOR", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_BANCO_SERVIDOR", RelationPropertyName = "TCS_BANCO_SERVIDOR" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_AMBIENTE_CONEXAO.TCS_APLICATIVO_CONEXAO.ID_TCS_APLICATIVO_CONEXAO", Source = "IdTcsAplicativoConexao", Target = "ID_TCS_APLICATIVO_CONEXAO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICATIVO_CONEXAO", RelationPropertyName = "TCS_APLICATIVO_CONEXAO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxTipoServidorValues()
	    {
	    	    return Linx.Framework.BV.Domains.TipoServidor.GetValues();
	    }
	    private string _lxTipoServidorName;
	    [DataMember(IsRequired = false, Name = "LxTipoServidorName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo Servidor", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxTipoServidorName
	    {
	    	    get { if (this.LxTipoServidor.IsNull()) { _lxTipoServidorName = String.Empty; } else { string key = this.LxTipoServidor.ToString(); var dmValues = this.GetLxTipoServidorValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxTipoServidorName) _lxTipoServidorName = domainName; } return _lxTipoServidorName; } set { _lxTipoServidorName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_AMBIENTE_SERVICO_EXCECAO.ID_TCS_AMBIENTE_SERVICO_EXCECAO", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Serviços];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsAmbienteServicoExcecao];ReadOnly[false];Entities[TCS_AMBIENTE_SERVICO_EXCECAO:IdTcsAmbienteServicoExcecao|TCS_SERVICO:IdTcsServico];SubQueryInfo[Select 1 From #ParentAlias#.TCS_AMBIENTE_SERVICO_EXCECAO_LISTA as #Alias#];EdmEntityName[TCS_AMBIENTE_SERVICO_EXCECAO];EntityRelations[TCS_AMBIENTE(TCS_AMBIENTE)#TCS_APLICACAO(TCS_APLICACAO)#TCS_APLICATIVO(TCS_APLICATIVO)#TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)#TCS_SERVICO(TCS_SERVICO)];EdmParentEntityName[TCS_AMBIENTE];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsAmbienteServicoExcecao")]
	[Serializable()]
	public partial class TcsAmbienteServicoExcecao : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(AmbienteDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsAmbiente");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsAmbiente"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdTcsAmbiente));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsAmbiente
	         this.TcsAmbiente = (from r in context.GetTcsAmbienteByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For IdTcsAmbiente
	    partial void OnIdTcsAmbienteChanging(Int32 value);
	    partial void OnIdTcsAmbienteChanged();

	    private Int32 _IdTcsAmbiente;

	    [DataMember(IsRequired = true, Name = "IdTcsAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ambiente", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_AMBIENTE_SERVICO_EXCECAO.TCS_AMBIENTE.ID_TCS_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE_SERVICO_EXCECAO.TCS_AMBIENTE.ID_TCS_AMBIENTE")]
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
	    //Extensibility Partial Method Definitions For IdTcsAmbienteServicoExcecao
	    partial void OnIdTcsAmbienteServicoExcecaoChanging(Int32 value);
	    partial void OnIdTcsAmbienteServicoExcecaoChanged();

	    private Int32 _IdTcsAmbienteServicoExcecao;

	    [DataMember(IsRequired = true, Name = "IdTcsAmbienteServicoExcecao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente Servico Excecao", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_AMBIENTE_SERVICO_EXCECAO.ID_TCS_AMBIENTE_SERVICO_EXCECAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE_SERVICO_EXCECAO.ID_TCS_AMBIENTE_SERVICO_EXCECAO")]
	    public Int32 IdTcsAmbienteServicoExcecao
	    {
	    	    get
	    	    {
	    	          return _IdTcsAmbienteServicoExcecao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAmbienteServicoExcecao != value)
	    	          {
	    	              this.ValidateProperty("IdTcsAmbienteServicoExcecao", value);
	    	              this.OnIdTcsAmbienteServicoExcecaoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsAmbienteServicoExcecao");
	    	              this._IdTcsAmbienteServicoExcecao = value;
	    	              this.RaiseDataMemberChanged("IdTcsAmbienteServicoExcecao");
	    	              this.OnIdTcsAmbienteServicoExcecaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsServico
	    partial void OnIdTcsServicoChanging(Int32 value);
	    partial void OnIdTcsServicoChanged();

	    private Int32 _IdTcsServico;

	    [DataMember(IsRequired = true, Name = "IdTcsServico", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Servico", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsServico];LookUpTitle[Seleção de (Id Tcs Servico)];LookUpQuery[executeLookUpTcsServico];LookUpFinalize[finalizeLookUpTcsServico];LookUpDisplayColumns[{\"IdTcsServico\" : \"Id Tcs Servico\", \"NomeServico\" : \"Nome Serviço\"}];LookUpColumns[{\"IdTcsServico\" : false, \"NomeServico\" : true}];FilterDataKey[TCS_AMBIENTE_SERVICO_EXCECAO.TCS_SERVICO.ID_TCS_SERVICO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdTcsServico#true##12:0##Id Tcs Servico#0#false##::LookUpTcsServico##true#false#TCS_SERVICO#TCS_SERVICO#Linx.Framework.BV.Ambiente#IQueryable###true#false", EdmKey="TCS_AMBIENTE_SERVICO_EXCECAO.TCS_SERVICO.ID_TCS_SERVICO")]
	    public Int32 IdTcsServico
	    {
	    	    get
	    	    {
	    	          return _IdTcsServico;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsServico != value)
	    	          {
	    	              this.ValidateProperty("IdTcsServico", value);
	    	              this.OnIdTcsServicoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsServico");
	    	              this._IdTcsServico = value;
	    	              this.RaiseDataMemberChanged("IdTcsServico");
	    	              this.OnIdTcsServicoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeServico
	    partial void OnNomeServicoChanging(System.String value);
	    partial void OnNomeServicoChanged();

	    private System.String _NomeServico;

	    [DataMember(IsRequired = true, Name = "NomeServico", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Serviço", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsServico];LookUpTitle[Seleção de (Nome Serviço)];LookUpQuery[executeLookUpTcsServico];LookUpFinalize[finalizeLookUpTcsServico];LookUpDisplayColumns[{\"IdTcsServico\" : \"Id Tcs Servico\", \"NomeServico\" : \"Nome Serviço\"}];LookUpColumns[{\"IdTcsServico\" : false, \"NomeServico\" : true}];FilterDataKey[TCS_AMBIENTE_SERVICO_EXCECAO.TCS_SERVICO.NOME_SERVICO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeServico#false##250:0##Nome Serviço#1#true##::LookUpTcsServico##true#false#TCS_SERVICO#TCS_SERVICO#Linx.Framework.BV.Ambiente#IQueryable###true#false", EdmKey="TCS_AMBIENTE_SERVICO_EXCECAO.TCS_SERVICO.NOME_SERVICO")]
	    public System.String NomeServico
	    {
	    	    get
	    	    {
	    	          return _NomeServico;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeServico != value)
	    	          {
	    	              this.ValidateProperty("NomeServico", value);
	    	              this.OnNomeServicoChanging(value);
	    	              this.RaiseDataMemberChanging("NomeServico");
	    	              this._NomeServico = value;
	    	              this.RaiseDataMemberChanged("NomeServico");
	    	              this.OnNomeServicoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Url
	    partial void OnUrlChanging(System.String value);
	    partial void OnUrlChanged();

	    private System.String _Url;

	    [DataMember(IsRequired = true, Name = "Url", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Url Alternativa", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_AMBIENTE_SERVICO_EXCECAO.URL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE_SERVICO_EXCECAO.URL")]
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

	    private Int32 _TemporaryIdTcsAmbienteServicoExcecao;
	    [DataMember(Name = "TemporaryIdTcsAmbienteServicoExcecao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente Servico Excecao (Tmp)", Description="Temporary Key", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdTcsAmbienteServicoExcecao
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTcsAmbienteServicoExcecao.IsNullOrEmpty())
	    	                this._TemporaryIdTcsAmbienteServicoExcecao = this._IdTcsAmbienteServicoExcecao;
	    	          return this._TemporaryIdTcsAmbienteServicoExcecao;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTcsAmbienteServicoExcecao != value)
	    	              this._TemporaryIdTcsAmbienteServicoExcecao = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsAmbiente _TcsAmbiente;
	    [DataMember(Name = "TcsAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsAmbiente_TcsAmbienteServicoExcecao", "IdTcsAmbiente", "IdTcsAmbiente", IsForeignKey=true)]
	    public TcsAmbiente TcsAmbiente
	    {
	        get
	        {
	            return this._TcsAmbiente;
	        }
	        set
	        {
	            if (this._TcsAmbiente != value)
	            {
	                this._TcsAmbiente = value;
	                this.RaisePropertyChanged("TcsAmbienteList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_AMBIENTE_SERVICO_EXCECAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_AMBIENTE_SERVICO_EXCECAO), QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE_SERVICO_EXCECAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_AMBIENTE_SERVICO_EXCECAO.URL", Source = "Url", Target = "URL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE_SERVICO_EXCECAO", RelationPropertyName = "TCS_AMBIENTE_SERVICO_EXCECAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_AMBIENTE_SERVICO_EXCECAO.TCS_SERVICO.ID_TCS_SERVICO", Source = "IdTcsServico", Target = "ID_TCS_SERVICO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_SERVICO", RelationPropertyName = "TCS_SERVICO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_AMBIENTE_SERVICO_EXCECAO.TCS_AMBIENTE.ID_TCS_AMBIENTE", Source = "IdTcsAmbiente", Target = "ID_TCS_AMBIENTE", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE", RelationPropertyName = "TCS_AMBIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_AMBIENTE_SERVICO_EXCECAO.ID_TCS_AMBIENTE_SERVICO_EXCECAO", Source = "IdTcsAmbienteServicoExcecao", Target = "ID_TCS_AMBIENTE_SERVICO_EXCECAO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE_SERVICO_EXCECAO", RelationPropertyName = "TCS_AMBIENTE_SERVICO_EXCECAO" });

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

		

	[LinxPublicationView(PrimaryKeys="TCS_SERVICO.ID_TCS_SERVICO", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsServico];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsServico];ReadOnly[false];Entities[TCS_SERVICO:IdTcsServico];SubQueryInfo[];EdmEntityName[TCS_SERVICO];EntityRelations[];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsServico")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Ambiente.TcsServico")]
	public partial class TcsServico : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For IdTcsServico
	    partial void OnIdTcsServicoChanging(Int32 value);
	    partial void OnIdTcsServicoChanged();

	    private Int32 _IdTcsServico;

	    [DataMember(IsRequired = true, Name = "IdTcsServico", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Servico", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_SERVICO.ID_TCS_SERVICO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_SERVICO.ID_TCS_SERVICO")]
	    public Int32 IdTcsServico
	    {
	    	    get
	    	    {
	    	          return _IdTcsServico;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsServico != value)
	    	          {
	    	              this.ValidateProperty("IdTcsServico", value);
	    	              this.OnIdTcsServicoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsServico");
	    	              this._IdTcsServico = value;
	    	              this.RaiseDataMemberChanged("IdTcsServico");
	    	              this.OnIdTcsServicoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeServico
	    partial void OnNomeServicoChanging(System.String value);
	    partial void OnNomeServicoChanged();

	    private System.String _NomeServico;

	    [DataMember(IsRequired = true, Name = "NomeServico", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Serviço / Controlador", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_SERVICO.NOME_SERVICO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_SERVICO.NOME_SERVICO")]
	    public System.String NomeServico
	    {
	    	    get
	    	    {
	    	          return _NomeServico;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeServico != value)
	    	          {
	    	              this.ValidateProperty("NomeServico", value);
	    	              this.OnNomeServicoChanging(value);
	    	              this.RaiseDataMemberChanging("NomeServico");
	    	              this._NomeServico = value;
	    	              this.RaiseDataMemberChanged("NomeServico");
	    	              this.OnNomeServicoChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIdTcsServico;
	    [DataMember(Name = "TemporaryIdTcsServico", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Servico (Tmp)", Description="Temporary Key", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdTcsServico
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTcsServico.IsNullOrEmpty())
	    	                this._TemporaryIdTcsServico = this._IdTcsServico;
	    	          return this._TemporaryIdTcsServico;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTcsServico != value)
	    	              this._TemporaryIdTcsServico = value;
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_SERVICO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_SERVICO), QualifiedEntitySetName = "AutorizacaoContext.TCS_SERVICO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_SERVICO.NOME_SERVICO", Source = "NomeServico", Target = "NOME_SERVICO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_SERVICO", RelationPropertyName = "TCS_SERVICO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_SERVICO.ID_TCS_SERVICO", Source = "IdTcsServico", Target = "ID_TCS_SERVICO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_SERVICO", RelationPropertyName = "TCS_SERVICO" });

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

		

	[LinxPublicationView(PrimaryKeys="TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsAmbienteRelacionado];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsUsuarioAcesso];ReadOnly[false];Entities[TCS_USUARIO_ACESSO:IdTcsUsuarioAcesso|TCS_AMBIENTE:IdTcsAmbienteRelacionado];SubQueryInfo[];EdmEntityName[TCS_USUARIO_ACESSO];EntityRelations[TCS_USUARIO_AUTENTICACAO(TCS_USUARIO_AUTENTICACAO)#TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)#TCS_AMBIENTE(TCS_AMBIENTE)#TCS_APLICACAO(TCS_APLICACAO)#TCS_APLICATIVO(TCS_APLICATIVO)#TCS_AMBIENTE1(TCS_AMBIENTE)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsAmbienteRelacionado")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Ambiente.TcsAmbienteRelacionado")]
	public partial class TcsAmbienteRelacionado : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For DescricaoAmbienteRelacionado
	    partial void OnDescricaoAmbienteRelacionadoChanging(System.String value);
	    partial void OnDescricaoAmbienteRelacionadoChanged();

	    private System.String _DescricaoAmbienteRelacionado;

	    [DataMember(IsRequired = true, Name = "DescricaoAmbienteRelacionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descricao Ambiente", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.DESCRICAO_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.DESCRICAO_AMBIENTE")]
	    public System.String DescricaoAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _DescricaoAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescricaoAmbienteRelacionado != value)
	    	          {
	    	              this.ValidateProperty("DescricaoAmbienteRelacionado", value);
	    	              this.OnDescricaoAmbienteRelacionadoChanging(value);
	    	              this.RaiseDataMemberChanging("DescricaoAmbienteRelacionado");
	    	              this._DescricaoAmbienteRelacionado = value;
	    	              this.RaiseDataMemberChanged("DescricaoAmbienteRelacionado");
	    	              this.OnDescricaoAmbienteRelacionadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescricaoAplicacaoAmbienteRelacionado
	    partial void OnDescricaoAplicacaoAmbienteRelacionadoChanging(System.String value);
	    partial void OnDescricaoAplicacaoAmbienteRelacionadoChanged();

	    private System.String _DescricaoAplicacaoAmbienteRelacionado;

	    [DataMember(IsRequired = true, Name = "DescricaoAplicacaoAmbienteRelacionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descricao Aplicacao", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.DESCRICAO_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.DESCRICAO_APLICACAO")]
	    public System.String DescricaoAplicacaoAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _DescricaoAplicacaoAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescricaoAplicacaoAmbienteRelacionado != value)
	    	          {
	    	              this.ValidateProperty("DescricaoAplicacaoAmbienteRelacionado", value);
	    	              this.OnDescricaoAplicacaoAmbienteRelacionadoChanging(value);
	    	              this.RaiseDataMemberChanging("DescricaoAplicacaoAmbienteRelacionado");
	    	              this._DescricaoAplicacaoAmbienteRelacionado = value;
	    	              this.RaiseDataMemberChanged("DescricaoAplicacaoAmbienteRelacionado");
	    	              this.OnDescricaoAplicacaoAmbienteRelacionadoChanged();
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
	    [Display(Name = "Descricao Aplicativo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO")]
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
	    //Extensibility Partial Method Definitions For IdAplicacao
	    partial void OnIdAplicacaoChanging(Int32 value);
	    partial void OnIdAplicacaoChanged();

	    private Int32 _IdAplicacao;

	    [DataMember(IsRequired = true, Name = "IdAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Aplicacao", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.ID_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.ID_APLICACAO")]
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
	    //Extensibility Partial Method Definitions For IdLinxAmbienteRelacionado
	    partial void OnIdLinxAmbienteRelacionadoChanging(Int32 value);
	    partial void OnIdLinxAmbienteRelacionadoChanged();

	    private Int32 _IdLinxAmbienteRelacionado;

	    [DataMember(IsRequired = true, Name = "IdLinxAmbienteRelacionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
	    public Int32 IdLinxAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _IdLinxAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLinxAmbienteRelacionado != value)
	    	          {
	    	              this.ValidateProperty("IdLinxAmbienteRelacionado", value);
	    	              this.OnIdLinxAmbienteRelacionadoChanging(value);
	    	              this.RaiseDataMemberChanging("IdLinxAmbienteRelacionado");
	    	              this._IdLinxAmbienteRelacionado = value;
	    	              this.RaiseDataMemberChanged("IdLinxAmbienteRelacionado");
	    	              this.OnIdLinxAmbienteRelacionadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsAmbienteRelacionado
	    partial void OnIdTcsAmbienteRelacionadoChanging(Int32 value);
	    partial void OnIdTcsAmbienteRelacionadoChanged();

	    private Int32 _IdTcsAmbienteRelacionado;

	    [DataMember(IsRequired = true, Name = "IdTcsAmbienteRelacionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.ID_TCS_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.ID_TCS_AMBIENTE")]
	    public Int32 IdTcsAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _IdTcsAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAmbienteRelacionado != value)
	    	          {
	    	              this.ValidateProperty("IdTcsAmbienteRelacionado", value);
	    	              this.OnIdTcsAmbienteRelacionadoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsAmbienteRelacionado");
	    	              this._IdTcsAmbienteRelacionado = value;
	    	              this.RaiseDataMemberChanged("IdTcsAmbienteRelacionado");
	    	              this.OnIdTcsAmbienteRelacionadoChanged();
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
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO")]
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
	    //Extensibility Partial Method Definitions For IdTcsUsuarioAcesso
	    partial void OnIdTcsUsuarioAcessoChanging(Int32 value);
	    partial void OnIdTcsUsuarioAcessoChanged();

	    private Int32 _IdTcsUsuarioAcesso;

	    [DataMember(IsRequired = true, Name = "IdTcsUsuarioAcesso", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Acesso", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO")]
	    public Int32 IdTcsUsuarioAcesso
	    {
	    	    get
	    	    {
	    	          return _IdTcsUsuarioAcesso;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsUsuarioAcesso != value)
	    	          {
	    	              this.ValidateProperty("IdTcsUsuarioAcesso", value);
	    	              this.OnIdTcsUsuarioAcessoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsUsuarioAcesso");
	    	              this._IdTcsUsuarioAcesso = value;
	    	              this.RaiseDataMemberChanged("IdTcsUsuarioAcesso");
	    	              this.OnIdTcsUsuarioAcessoChanged();
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
	    [Display(Name = "Id Usuario", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.ID_USUARIO")]
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
	    //Extensibility Partial Method Definitions For NomeEmpresaAmbienteRelacionado
	    partial void OnNomeEmpresaAmbienteRelacionadoChanging(System.String value);
	    partial void OnNomeEmpresaAmbienteRelacionadoChanged();

	    private System.String _NomeEmpresaAmbienteRelacionado;

	    [DataMember(IsRequired = true, Name = "NomeEmpresaAmbienteRelacionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Empresa", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
	    public System.String NomeEmpresaAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _NomeEmpresaAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeEmpresaAmbienteRelacionado != value)
	    	          {
	    	              this.ValidateProperty("NomeEmpresaAmbienteRelacionado", value);
	    	              this.OnNomeEmpresaAmbienteRelacionadoChanging(value);
	    	              this.RaiseDataMemberChanging("NomeEmpresaAmbienteRelacionado");
	    	              this._NomeEmpresaAmbienteRelacionado = value;
	    	              this.RaiseDataMemberChanged("NomeEmpresaAmbienteRelacionado");
	    	              this.OnNomeEmpresaAmbienteRelacionadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeAutenticacao
	    partial void OnNomeAutenticacaoChanging(String value);
	    partial void OnNomeAutenticacaoChanged();

	    private String _NomeAutenticacao;

	    [DataMember(IsRequired = true, Name = "NomeAutenticacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário Autenticação", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO")]
	    public String NomeAutenticacao
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
	    partial void OnNomeUsuarioChanging(String value);
	    partial void OnNomeUsuarioChanged();

	    private String _NomeUsuario;

	    [DataMember(IsRequired = true, Name = "NomeUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO")]
	    public String NomeUsuario
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

	    private Int32 _TemporaryIdTcsUsuarioAcesso;
	    [DataMember(Name = "TemporaryIdTcsUsuarioAcesso", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Acesso (Tmp)", Description="Temporary Key", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdTcsUsuarioAcesso
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTcsUsuarioAcesso.IsNullOrEmpty())
	    	                this._TemporaryIdTcsUsuarioAcesso = this._IdTcsUsuarioAcesso;
	    	          return this._TemporaryIdTcsUsuarioAcesso;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTcsUsuarioAcesso != value)
	    	              this._TemporaryIdTcsUsuarioAcesso = value;
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_USUARIO_ACESSO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_USUARIO_ACESSO), QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_ACESSO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO", Source = "IdTcsUsuarioAcesso", Target = "ID_TCS_USUARIO_ACESSO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_ACESSO", RelationPropertyName = "TCS_USUARIO_ACESSO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_ACESSO.TCS_AMBIENTE.ID_TCS_AMBIENTE", Source = "IdTcsAmbienteRelacionado", Target = "ID_TCS_AMBIENTE", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE", RelationPropertyName = "TCS_AMBIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });

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

		

	[LinxPublicationView(PrimaryKeys="ServicoExcecaoInfo.EntityUniqueKey", IsUpdatable=false, EdmName="")]
		
	[DataContract(IsReference = false, Name = "ServicoExcecaoInfo")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Ambiente.ServicoExcecaoInfo")]
	public partial class ServicoExcecaoInfo 
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
	 


	    private int _IdTcsAmbiente;

	    [DataMember(Name = "IdTcsAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public int IdTcsAmbiente
	    {
	    	    get
	    	    {
	    	          return _IdTcsAmbiente;
	    	    }
	    	    set
	    	    {
	    	          this._IdTcsAmbiente = value;
	    	    }
	    }

	    private string _Servico;

	    [DataMember(Name = "Servico", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Servico
	    {
	    	    get
	    	    {
	    	          if (_Servico.IsNullOrEmpty())
	    	             _Servico =  String.Empty;
	    	          return _Servico;
	    	    }
	    	    set
	    	    {
	    	          this._Servico = value;
	    	    }
	    }

	    private string _Url;

	    [DataMember(Name = "Url", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Url
	    {
	    	    get
	    	    {
	    	          return _Url;
	    	    }
	    	    set
	    	    {
	    	          this._Url = value;
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

		

	[LinxPublicationView(PrimaryKeys="AmbienteServicoInfo.EntityUniqueKey", IsUpdatable=false, EdmName="")]
		
	[DataContract(IsReference = false, Name = "AmbienteServicoInfo")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Ambiente.AmbienteServicoInfo")]
	public partial class AmbienteServicoInfo 
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
	 


	    private string _Hash;

	    [DataMember(Name = "Hash", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Hash
	    {
	    	    get
	    	    {
	    	          if (_Hash.IsNullOrEmpty())
	    	             _Hash =  String.Empty;
	    	          return _Hash;
	    	    }
	    	    set
	    	    {
	    	          this._Hash = value;
	    	    }
	    }

	    private List<ServicoExcecaoInfo> _Servicos;

	    [DataMember(Name = "Servicos", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public List<ServicoExcecaoInfo> Servicos
	    {
	    	    get
	    	    {
	    	          return _Servicos;
	    	    }
	    	    set
	    	    {
	    	          this._Servicos = value;
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

		

	[LinxPublicationView(PrimaryKeys="EnvironmentInfo.EntityUniqueKey", IsUpdatable=false, EdmName="")]
		
	[DataContract(IsReference = false, Name = "EnvironmentInfo")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Ambiente.EnvironmentInfo")]
	public partial class EnvironmentInfo 
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
	 


	    private Guid _Hash;

	    [DataMember(Name = "Hash", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public Guid Hash
	    {
	    	    get
	    	    {
	    	          return _Hash;
	    	    }
	    	    set
	    	    {
	    	          this._Hash = value;
	    	    }
	    }

	    private int _EnvironmentId;

	    [DataMember(Name = "EnvironmentId", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public int EnvironmentId
	    {
	    	    get
	    	    {
	    	          return _EnvironmentId;
	    	    }
	    	    set
	    	    {
	    	          this._EnvironmentId = value;
	    	    }
	    }

	    private Guid _ApplicationUid;

	    [DataMember(Name = "ApplicationUid", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public Guid ApplicationUid
	    {
	    	    get
	    	    {
	    	          return _ApplicationUid;
	    	    }
	    	    set
	    	    {
	    	          this._ApplicationUid = value;
	    	    }
	    }

	    private Guid _CompanyUid;

	    [DataMember(Name = "CompanyUid", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public Guid CompanyUid
	    {
	    	    get
	    	    {
	    	          return _CompanyUid;
	    	    }
	    	    set
	    	    {
	    	          this._CompanyUid = value;
	    	    }
	    }

	    private int _AplicativeId;

	    [DataMember(Name = "AplicativeId", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public int AplicativeId
	    {
	    	    get
	    	    {
	    	          return _AplicativeId;
	    	    }
	    	    set
	    	    {
	    	          this._AplicativeId = value;
	    	    }
	    }

	    private string _ParameterList;

	    [DataMember(Name = "ParameterList", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ParameterList
	    {
	    	    get
	    	    {
	    	          return _ParameterList;
	    	    }
	    	    set
	    	    {
	    	          this._ParameterList = value;
	    	    }
	    }

	    private int? _IdLoja;

	    [DataMember(Name = "IdLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public int? IdLoja
	    {
	    	    get
	    	    {
	    	          return _IdLoja;
	    	    }
	    	    set
	    	    {
	    	          this._IdLoja = value;
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

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Usuários];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsUsuarioAcesso];ReadOnly[false];Entities[TCS_USUARIO_ACESSO:IdTcsUsuarioAcesso];SubQueryInfo[Select 1 From #ParentAlias#.TCS_USUARIO_ACESSO_LISTA as #Alias#];EdmEntityName[TCS_USUARIO_ACESSO];EntityRelations[TCS_USUARIO_AUTENTICACAO(TCS_USUARIO_AUTENTICACAO)#TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)#TCS_AMBIENTE(TCS_AMBIENTE)#TCS_APLICACAO(TCS_APLICACAO)#TCS_APLICATIVO(TCS_APLICATIVO)#TCS_AMBIENTE1(TCS_AMBIENTE)];EdmParentEntityName[TCS_AMBIENTE];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsAmbienteUsuarioAcesso")]
	[Serializable()]
	public partial class TcsAmbienteUsuarioAcessoParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For DescricaoAmbienteRelacionado
	    partial void OnDescricaoAmbienteRelacionadoChanging(System.String value);
	    partial void OnDescricaoAmbienteRelacionadoChanged();

	    private System.String _DescricaoAmbienteRelacionado;

	    [DataMember(Name = "DescricaoAmbienteRelacionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ambiente Relacionado", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbienteAdministrativo];LookUpTitle[Seleção de (Ambiente Relacionado)];LookUpQuery[executeLookUpTcsAmbienteAdministrativo];LookUpFinalize[finalizeLookUpTcsAmbienteAdministrativo];LookUpDisplayColumns[{\"IdTcsAmbienteRelacionado\" : \"\", \"IdLinxAmbienteRelacionado\" : \"\", \"NomeEmpresaAmbienteRelacionado\" : \"Empresa\", \"DescricaoAmbienteRelacionado\" : \"Ambiente\", \"DescricaoAplicacaoAmbienteRelacionado\" : \"Aplicação\"}];LookUpColumns[{\"IdTcsAmbienteRelacionado\" : false, \"IdLinxAmbienteRelacionado\" : false, \"NomeEmpresaAmbienteRelacionado\" : true, \"DescricaoAmbienteRelacionado\" : true, \"DescricaoAplicacaoAmbienteRelacionado\" : true}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE1.DESCRICAO_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#DescricaoAmbienteRelacionado#false##250:0##Ambiente#3#true##::LookUpTcsAmbienteAdministrativo##false#false###Linx.Framework.BV.Ambiente#IQueryable###true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.DESCRICAO_AMBIENTE")]
	    public System.String DescricaoAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _DescricaoAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescricaoAmbienteRelacionado != value)
	    	          {
	    	              this.ValidateProperty("DescricaoAmbienteRelacionado", value);
	    	              this.OnDescricaoAmbienteRelacionadoChanging(value);
	    	              this.RaiseDataMemberChanging("DescricaoAmbienteRelacionado");
	    	              this._DescricaoAmbienteRelacionado = value;
	    	              this.RaiseDataMemberChanged("DescricaoAmbienteRelacionado");
	    	              this.OnDescricaoAmbienteRelacionadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescricaoAplicacaoAmbienteRelacionado
	    partial void OnDescricaoAplicacaoAmbienteRelacionadoChanging(System.String value);
	    partial void OnDescricaoAplicacaoAmbienteRelacionadoChanged();

	    private System.String _DescricaoAplicacaoAmbienteRelacionado;

	    [DataMember(Name = "DescricaoAplicacaoAmbienteRelacionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicação Ambiente Relacionado", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbienteAdministrativo];LookUpTitle[Seleção de (Aplicação Ambiente Relacionado)];LookUpQuery[executeLookUpTcsAmbienteAdministrativo];LookUpFinalize[finalizeLookUpTcsAmbienteAdministrativo];LookUpDisplayColumns[{\"IdTcsAmbienteRelacionado\" : \"\", \"IdLinxAmbienteRelacionado\" : \"\", \"NomeEmpresaAmbienteRelacionado\" : \"Empresa\", \"DescricaoAmbienteRelacionado\" : \"Ambiente\", \"DescricaoAplicacaoAmbienteRelacionado\" : \"Aplicação\"}];LookUpColumns[{\"IdTcsAmbienteRelacionado\" : false, \"IdLinxAmbienteRelacionado\" : false, \"NomeEmpresaAmbienteRelacionado\" : true, \"DescricaoAmbienteRelacionado\" : true, \"DescricaoAplicacaoAmbienteRelacionado\" : true}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE1.TCS_APLICACAO.DESCRICAO_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#DescricaoAplicacaoAmbienteRelacionado#false##60:0##Aplicação#4#true##::LookUpTcsAmbienteAdministrativo##false#false###Linx.Framework.BV.Ambiente#IQueryable###true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.TCS_APLICACAO.DESCRICAO_APLICACAO")]
	    public System.String DescricaoAplicacaoAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _DescricaoAplicacaoAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescricaoAplicacaoAmbienteRelacionado != value)
	    	          {
	    	              this.ValidateProperty("DescricaoAplicacaoAmbienteRelacionado", value);
	    	              this.OnDescricaoAplicacaoAmbienteRelacionadoChanging(value);
	    	              this.RaiseDataMemberChanging("DescricaoAplicacaoAmbienteRelacionado");
	    	              this._DescricaoAplicacaoAmbienteRelacionado = value;
	    	              this.RaiseDataMemberChanged("DescricaoAplicacaoAmbienteRelacionado");
	    	              this.OnDescricaoAplicacaoAmbienteRelacionadoChanged();
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
	    [Display(Name = "ID Linx Ambiente", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacao];LookUpTitle[Seleção de (ID Linx Ambiente)];LookUpQuery[executeLookUpTcsUsuarioAutenticacao];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacao];LookUpDisplayColumns[{\"NomeUsuario\" : \"Nome\", \"NomeEmpresa\" : \"Grupo Econômico\", \"IdLinx\" : \"Id Grupo Econômico\", \"IdUsuario\" : \"Id Usuario\", \"UidUsuario\" : \"Uid Usuario\", \"NomeAutenticacao\" : \"Nome Autenticacao\"}];LookUpColumns[{\"NomeUsuario\" : true, \"NomeEmpresa\" : true, \"IdLinx\" : true, \"IdUsuario\" : false, \"UidUsuario\" : false, \"NomeAutenticacao\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdLinx#false##12:0##Id Grupo Econômico#2#true##::LookUpTcsUsuarioAutenticacao##true#false#TCS_USUARIO_AUTENTICACAO#TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.Ambiente#IQueryable###true#true", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
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
	    //Extensibility Partial Method Definitions For IdLinxAmbienteRelacionado
	    partial void OnIdLinxAmbienteRelacionadoChanging(System.Nullable<Int32> value);
	    partial void OnIdLinxAmbienteRelacionadoChanged();

	    private System.Nullable<Int32> _IdLinxAmbienteRelacionado;

	    [DataMember(Name = "IdLinxAmbienteRelacionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx1", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbienteAdministrativo];LookUpTitle[Seleção de (Id Linx1)];LookUpQuery[executeLookUpTcsAmbienteAdministrativo];LookUpFinalize[finalizeLookUpTcsAmbienteAdministrativo];LookUpDisplayColumns[{\"IdTcsAmbienteRelacionado\" : \"\", \"IdLinxAmbienteRelacionado\" : \"\", \"NomeEmpresaAmbienteRelacionado\" : \"Empresa\", \"DescricaoAmbienteRelacionado\" : \"Ambiente\", \"DescricaoAplicacaoAmbienteRelacionado\" : \"Aplicação\"}];LookUpColumns[{\"IdTcsAmbienteRelacionado\" : false, \"IdLinxAmbienteRelacionado\" : false, \"NomeEmpresaAmbienteRelacionado\" : true, \"DescricaoAmbienteRelacionado\" : true, \"DescricaoAplicacaoAmbienteRelacionado\" : true}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="int#IdLinxAmbienteRelacionado#false##12:0###1#false##::LookUpTcsAmbienteAdministrativo##false#false###Linx.Framework.BV.Ambiente#IQueryable###true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
	    public System.Nullable<Int32> IdLinxAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _IdLinxAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLinxAmbienteRelacionado != value)
	    	          {
	    	              this.ValidateProperty("IdLinxAmbienteRelacionado", value);
	    	              this.OnIdLinxAmbienteRelacionadoChanging(value);
	    	              this.RaiseDataMemberChanging("IdLinxAmbienteRelacionado");
	    	              this._IdLinxAmbienteRelacionado = value;
	    	              this.RaiseDataMemberChanged("IdLinxAmbienteRelacionado");
	    	              this.OnIdLinxAmbienteRelacionadoChanged();
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
	    [Display(Name = "Ambiente", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.ID_TCS_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.ID_TCS_AMBIENTE")]
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
	    //Extensibility Partial Method Definitions For IdTcsAmbienteRelacionado
	    partial void OnIdTcsAmbienteRelacionadoChanging(System.Nullable<Int32> value);
	    partial void OnIdTcsAmbienteRelacionadoChanged();

	    private System.Nullable<Int32> _IdTcsAmbienteRelacionado;

	    [DataMember(Name = "IdTcsAmbienteRelacionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente1", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbienteAdministrativo];LookUpTitle[Seleção de (Id Tcs Ambiente1)];LookUpQuery[executeLookUpTcsAmbienteAdministrativo];LookUpFinalize[finalizeLookUpTcsAmbienteAdministrativo];LookUpDisplayColumns[{\"IdTcsAmbienteRelacionado\" : \"\", \"IdLinxAmbienteRelacionado\" : \"\", \"NomeEmpresaAmbienteRelacionado\" : \"Empresa\", \"DescricaoAmbienteRelacionado\" : \"Ambiente\", \"DescricaoAplicacaoAmbienteRelacionado\" : \"Aplicação\"}];LookUpColumns[{\"IdTcsAmbienteRelacionado\" : false, \"IdLinxAmbienteRelacionado\" : false, \"NomeEmpresaAmbienteRelacionado\" : true, \"DescricaoAmbienteRelacionado\" : true, \"DescricaoAplicacaoAmbienteRelacionado\" : true}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE1.ID_TCS_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="int#IdTcsAmbienteRelacionado#false##12:0###0#false##::LookUpTcsAmbienteAdministrativo##false#false###Linx.Framework.BV.Ambiente#IQueryable###true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.ID_TCS_AMBIENTE")]
	    public System.Nullable<Int32> IdTcsAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _IdTcsAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAmbienteRelacionado != value)
	    	          {
	    	              this.ValidateProperty("IdTcsAmbienteRelacionado", value);
	    	              this.OnIdTcsAmbienteRelacionadoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsAmbienteRelacionado");
	    	              this._IdTcsAmbienteRelacionado = value;
	    	              this.RaiseDataMemberChanged("IdTcsAmbienteRelacionado");
	    	              this.OnIdTcsAmbienteRelacionadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsUsuarioAcesso
	    partial void OnIdTcsUsuarioAcessoChanging(Int32 value);
	    partial void OnIdTcsUsuarioAcessoChanged();

	    private Int32 _IdTcsUsuarioAcesso;

	    [DataMember(IsRequired = true, Name = "IdTcsUsuarioAcesso", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Acesso", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO")]
	    public Int32 IdTcsUsuarioAcesso
	    {
	    	    get
	    	    {
	    	          return _IdTcsUsuarioAcesso;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsUsuarioAcesso != value)
	    	          {
	    	              this.ValidateProperty("IdTcsUsuarioAcesso", value);
	    	              this.OnIdTcsUsuarioAcessoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsUsuarioAcesso");
	    	              this._IdTcsUsuarioAcesso = value;
	    	              this.RaiseDataMemberChanged("IdTcsUsuarioAcesso");
	    	              this.OnIdTcsUsuarioAcessoChanged();
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
	    [Display(Name = "Id Usuario", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacao];LookUpTitle[Seleção de (Id Usuario)];LookUpQuery[executeLookUpTcsUsuarioAutenticacao];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacao];LookUpDisplayColumns[{\"NomeUsuario\" : \"Nome\", \"NomeEmpresa\" : \"Grupo Econômico\", \"IdLinx\" : \"Id Grupo Econômico\", \"IdUsuario\" : \"Id Usuario\", \"UidUsuario\" : \"Uid Usuario\", \"NomeAutenticacao\" : \"Nome Autenticacao\"}];LookUpColumns[{\"NomeUsuario\" : true, \"NomeEmpresa\" : true, \"IdLinx\" : true, \"IdUsuario\" : false, \"UidUsuario\" : false, \"NomeAutenticacao\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdUsuario#true##24:0##Id Usuario#3#false##::LookUpTcsUsuarioAutenticacao##true#false#TCS_USUARIO_AUTENTICACAO#TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.Ambiente#IQueryable###true#true", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.ID_USUARIO")]
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
	    //Extensibility Partial Method Definitions For IndicaAdministrador
	    partial void OnIndicaAdministradorChanging(Boolean value);
	    partial void OnIndicaAdministradorChanged();

	    private Boolean _IndicaAdministrador;

	    [DataMember(IsRequired = true, Name = "IndicaAdministrador", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Administrador", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.INDICA_ADMINISTRADOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.INDICA_ADMINISTRADOR")]
	    public Boolean IndicaAdministrador
	    {
	    	    get
	    	    {
	    	          return _IndicaAdministrador;
	    	    }
	    	    set
	    	    {
	    	          if (this._IndicaAdministrador != value)
	    	          {
	    	              this.ValidateProperty("IndicaAdministrador", value);
	    	              this.OnIndicaAdministradorChanging(value);
	    	              this.RaiseDataMemberChanging("IndicaAdministrador");
	    	              this._IndicaAdministrador = value;
	    	              this.RaiseDataMemberChanged("IndicaAdministrador");
	    	              this.OnIndicaAdministradorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IndicaMultiGpecon
	    partial void OnIndicaMultiGpeconChanging(Boolean value);
	    partial void OnIndicaMultiGpeconChanged();

	    private Boolean _IndicaMultiGpecon;

	    [DataMember(IsRequired = true, Name = "IndicaMultiGpecon", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Multi Grupo Econômico", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.INDICA_MULTI_GPECON];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.INDICA_MULTI_GPECON")]
	    public Boolean IndicaMultiGpecon
	    {
	    	    get
	    	    {
	    	          return _IndicaMultiGpecon;
	    	    }
	    	    set
	    	    {
	    	          if (this._IndicaMultiGpecon != value)
	    	          {
	    	              this.ValidateProperty("IndicaMultiGpecon", value);
	    	              this.OnIndicaMultiGpeconChanging(value);
	    	              this.RaiseDataMemberChanging("IndicaMultiGpecon");
	    	              this._IndicaMultiGpecon = value;
	    	              this.RaiseDataMemberChanged("IndicaMultiGpecon");
	    	              this.OnIndicaMultiGpeconChanged();
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
	    [Display(Name = "Empresa (Id Linx)", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacao];LookUpTitle[Seleção de (Empresa (Id Linx))];LookUpQuery[executeLookUpTcsUsuarioAutenticacao];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacao];LookUpDisplayColumns[{\"NomeUsuario\" : \"Nome\", \"NomeEmpresa\" : \"Grupo Econômico\", \"IdLinx\" : \"Id Grupo Econômico\", \"IdUsuario\" : \"Id Usuario\", \"UidUsuario\" : \"Uid Usuario\", \"NomeAutenticacao\" : \"Nome Autenticacao\"}];LookUpColumns[{\"NomeUsuario\" : true, \"NomeEmpresa\" : true, \"IdLinx\" : true, \"IdUsuario\" : false, \"UidUsuario\" : false, \"NomeAutenticacao\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeEmpresa#false##250:0##Grupo Econômico#1#true##::LookUpTcsUsuarioAutenticacao##true#false#TCS_USUARIO_AUTENTICACAO#TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.Ambiente#IQueryable###true#true", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
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
	    //Extensibility Partial Method Definitions For NomeEmpresaAmbienteRelacionado
	    partial void OnNomeEmpresaAmbienteRelacionadoChanging(System.String value);
	    partial void OnNomeEmpresaAmbienteRelacionadoChanged();

	    private System.String _NomeEmpresaAmbienteRelacionado;

	    [DataMember(Name = "NomeEmpresaAmbienteRelacionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Empresa Ambiente Relacionado", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbienteAdministrativo];LookUpTitle[Seleção de (Empresa Ambiente Relacionado)];LookUpQuery[executeLookUpTcsAmbienteAdministrativo];LookUpFinalize[finalizeLookUpTcsAmbienteAdministrativo];LookUpDisplayColumns[{\"IdTcsAmbienteRelacionado\" : \"\", \"IdLinxAmbienteRelacionado\" : \"\", \"NomeEmpresaAmbienteRelacionado\" : \"Empresa\", \"DescricaoAmbienteRelacionado\" : \"Ambiente\", \"DescricaoAplicacaoAmbienteRelacionado\" : \"Aplicação\"}];LookUpColumns[{\"IdTcsAmbienteRelacionado\" : false, \"IdLinxAmbienteRelacionado\" : false, \"NomeEmpresaAmbienteRelacionado\" : true, \"DescricaoAmbienteRelacionado\" : true, \"DescricaoAplicacaoAmbienteRelacionado\" : true}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#NomeEmpresaAmbienteRelacionado#false##250:0##Empresa#2#true##::LookUpTcsAmbienteAdministrativo##false#false###Linx.Framework.BV.Ambiente#IQueryable###true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
	    public System.String NomeEmpresaAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _NomeEmpresaAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeEmpresaAmbienteRelacionado != value)
	    	          {
	    	              this.ValidateProperty("NomeEmpresaAmbienteRelacionado", value);
	    	              this.OnNomeEmpresaAmbienteRelacionadoChanging(value);
	    	              this.RaiseDataMemberChanging("NomeEmpresaAmbienteRelacionado");
	    	              this._NomeEmpresaAmbienteRelacionado = value;
	    	              this.RaiseDataMemberChanged("NomeEmpresaAmbienteRelacionado");
	    	              this.OnNomeEmpresaAmbienteRelacionadoChanged();
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
	    [Display(Name = "Uid Usuario", Description="", Order = 26, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacao];LookUpTitle[Seleção de (Uid Usuario)];LookUpQuery[executeLookUpTcsUsuarioAutenticacao];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacao];LookUpDisplayColumns[{\"NomeUsuario\" : \"Nome\", \"NomeEmpresa\" : \"Grupo Econômico\", \"IdLinx\" : \"Id Grupo Econômico\", \"IdUsuario\" : \"Id Usuario\", \"UidUsuario\" : \"Uid Usuario\", \"NomeAutenticacao\" : \"Nome Autenticacao\"}];LookUpColumns[{\"NomeUsuario\" : true, \"NomeEmpresa\" : true, \"IdLinx\" : true, \"IdUsuario\" : false, \"UidUsuario\" : false, \"NomeAutenticacao\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.UID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Guid#UidUsuario#false##12:0##Uid Usuario#4#false##::LookUpTcsUsuarioAutenticacao##true#false#TCS_USUARIO_AUTENTICACAO#TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.Ambiente#IQueryable###true#true", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.UID_USUARIO")]
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
	    //Extensibility Partial Method Definitions For NomeAutenticacao
	    partial void OnNomeAutenticacaoChanging(String value);
	    partial void OnNomeAutenticacaoChanged();

	    private String _NomeAutenticacao;

	    [DataMember(Name = "NomeAutenticacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário Autenticação", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacao];LookUpTitle[Seleção de (Usuário Autenticação)];LookUpQuery[executeLookUpTcsUsuarioAutenticacao];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacao];LookUpDisplayColumns[{\"NomeUsuario\" : \"Nome\", \"NomeEmpresa\" : \"Grupo Econômico\", \"IdLinx\" : \"Id Grupo Econômico\", \"IdUsuario\" : \"Id Usuario\", \"UidUsuario\" : \"Uid Usuario\", \"NomeAutenticacao\" : \"Nome Autenticacao\"}];LookUpColumns[{\"NomeUsuario\" : true, \"NomeEmpresa\" : true, \"IdLinx\" : true, \"IdUsuario\" : false, \"UidUsuario\" : false, \"NomeAutenticacao\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeAutenticacao#false##2500##Nome Autenticacao#5#false##::LookUpTcsUsuarioAutenticacao##true#false#TCS_USUARIO_AUTENTICACAO#TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.Ambiente#IQueryable###true#true", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO")]
	    public String NomeAutenticacao
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
	    partial void OnNomeUsuarioChanging(String value);
	    partial void OnNomeUsuarioChanged();

	    private String _NomeUsuario;

	    [DataMember(Name = "NomeUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacao];LookUpTitle[Seleção de (Usuário)];LookUpQuery[executeLookUpTcsUsuarioAutenticacao];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacao];LookUpDisplayColumns[{\"NomeUsuario\" : \"Nome\", \"NomeEmpresa\" : \"Grupo Econômico\", \"IdLinx\" : \"Id Grupo Econômico\", \"IdUsuario\" : \"Id Usuario\", \"UidUsuario\" : \"Uid Usuario\", \"NomeAutenticacao\" : \"Nome Autenticacao\"}];LookUpColumns[{\"NomeUsuario\" : true, \"NomeEmpresa\" : true, \"IdLinx\" : true, \"IdUsuario\" : false, \"UidUsuario\" : false, \"NomeAutenticacao\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="String#NomeUsuario#false##250:0##Nome#0#true##::LookUpTcsUsuarioAutenticacao##true#false#TCS_USUARIO_AUTENTICACAO#TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.Ambiente#IQueryable###true#true", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO")]
	    public String NomeUsuario
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
	    //Extensibility Partial Method Definitions For DescricaoAmbiente
	    partial void OnDescricaoAmbienteChanging(System.String value);
	    partial void OnDescricaoAmbienteChanged();

	    private System.String _DescricaoAmbiente;

	    [DataMember(IsRequired = true, Name = "DescricaoAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ambiente", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[IdTcsAmbiente];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.DESCRICAO_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.DESCRICAO_AMBIENTE")]
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
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.DESCRICAO_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.TCS_APLICACAO.DESCRICAO_APLICACAO")]
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
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO")]
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
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.EM_DESENVOLVIMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.TCS_APLICACAO.EM_DESENVOLVIMENTO")]
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
	    [Display(Name = "Id Aplicacao", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.ID_APLICACAO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For IdTcsAplicativo
	    partial void OnIdTcsAplicativoChanging(Int32 value);
	    partial void OnIdTcsAplicativoChanged();

	    private Int32 _IdTcsAplicativo;

	    [DataMember(IsRequired = true, Name = "IdTcsAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Aplicativo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO")]
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

	    [DataMember(IsRequired = true, Name = "UidAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Aplicacao", Description="", Order = 4, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.UID_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.TCS_APLICACAO.UID_APLICACAO")]
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
	    //Extensibility Partial Method Definitions For UidEmpresa
	    partial void OnUidEmpresaChanging(System.Guid value);
	    partial void OnUidEmpresaChanged();

	    private System.Guid _UidEmpresa;

	    [DataMember(IsRequired = true, Name = "UidEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Empresa", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA")]
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
	    //Extensibility Partial Method Definitions For Url
	    partial void OnUrlChanging(System.String value);
	    partial void OnUrlChanged();

	    private System.String _Url;

	    [DataMember(Name = "Url", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Url Alternativa", Description="", Order = 5, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.URL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.TCS_APLICACAO.URL")]
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
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.URL_WORK_AREA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.TCS_APLICACAO.URL_WORK_AREA")]
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
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_USUARIO_ACESSO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_USUARIO_ACESSO), QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_ACESSO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_ACESSO.INDICA_MULTI_GPECON", Source = "IndicaMultiGpecon", Target = "INDICA_MULTI_GPECON", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_ACESSO", RelationPropertyName = "TCS_USUARIO_ACESSO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_ACESSO.INDICA_ADMINISTRADOR", Source = "IndicaAdministrador", Target = "INDICA_ADMINISTRADOR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_ACESSO", RelationPropertyName = "TCS_USUARIO_ACESSO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO", Source = "IdTcsUsuarioAcesso", Target = "ID_TCS_USUARIO_ACESSO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_ACESSO", RelationPropertyName = "TCS_USUARIO_ACESSO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_ACESSO.TCS_AMBIENTE.ID_TCS_AMBIENTE", Source = "IdTcsAmbiente", Target = "ID_TCS_AMBIENTE", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE", RelationPropertyName = "TCS_AMBIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.ID_TCS_AMBIENTE", Source = "IdTcsAmbienteRelacionado", Target = "ID_TCS_AMBIENTE", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE", RelationPropertyName = "TCS_AMBIENTE1" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });

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

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Providers];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsAmbienteConexao];ReadOnly[false];Entities[TCS_AMBIENTE_CONEXAO:IdTcsAmbienteConexao|TCS_APLICATIVO_CONEXAO:IdTcsAplicativoConexao|TCS_BANCO_SERVIDOR:IdTcsBancoServidor];SubQueryInfo[Select 1 From #ParentAlias#.TCS_AMBIENTE_CONEXAO_LISTA as #Alias#];EdmEntityName[TCS_AMBIENTE_CONEXAO];EntityRelations[TCS_AMBIENTE(TCS_AMBIENTE)#TCS_APLICACAO(TCS_APLICACAO)#TCS_APLICATIVO(TCS_APLICATIVO)#TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)#TCS_BANCO_SERVIDOR(TCS_BANCO_SERVIDOR)#TCS_APLICATIVO_CONEXAO(TCS_APLICATIVO_CONEXAO)#TCS_CONEXAO_DB(TCS_CONEXAO_DB)];EdmParentEntityName[TCS_AMBIENTE];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsAmbienteConexao")]
	[Serializable()]
	public partial class TcsAmbienteConexaoParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For DescricaoBancoServidor
	    partial void OnDescricaoBancoServidorChanging(System.String value);
	    partial void OnDescricaoBancoServidorChanged();

	    private System.String _DescricaoBancoServidor;

	    [DataMember(IsRequired = true, Name = "DescricaoBancoServidor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Conexão Banco/Servidor", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(80)]
	    [FunctionalPoint("Precision[80:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsBancoServidor];LookUpTitle[Seleção de (Conexão Banco/Servidor)];LookUpQuery[executeLookUpTcsBancoServidor];LookUpFinalize[finalizeLookUpTcsBancoServidor];LookUpDisplayColumns[{\"IdTcsBancoServidor\" : \"Id Tcs Banco Servidor\", \"DescricaoBancoServidor\" : \"Descrição\", \"NomeServidor\" : \"Servidor\", \"NomeBanco\" : \"Banco de Dados\", \"LxTipoServidor\" : \"Lx Tipo Servidor\", \"StringConexao\" : \"String Conexao\"}];LookUpColumns[{\"IdTcsBancoServidor\" : false, \"DescricaoBancoServidor\" : true, \"NomeServidor\" : true, \"NomeBanco\" : true, \"LxTipoServidor\" : false, \"StringConexao\" : false}];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.DESCRICAO_BANCO_SERVIDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoBancoServidor#false##80:0##Descrição#1#true##::LookUpTcsBancoServidor##false#false#TCS_BANCO_SERVIDOR#TCS_BANCO_SERVIDOR#Linx.Framework.BV.Ambiente#IQueryable###true#false", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.DESCRICAO_BANCO_SERVIDOR")]
	    public System.String DescricaoBancoServidor
	    {
	    	    get
	    	    {
	    	          return _DescricaoBancoServidor;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescricaoBancoServidor != value)
	    	          {
	    	              this.ValidateProperty("DescricaoBancoServidor", value);
	    	              this.OnDescricaoBancoServidorChanging(value);
	    	              this.RaiseDataMemberChanging("DescricaoBancoServidor");
	    	              this._DescricaoBancoServidor = value;
	    	              this.RaiseDataMemberChanged("DescricaoBancoServidor");
	    	              this.OnDescricaoBancoServidorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdConexaoDb
	    partial void OnIdConexaoDbChanging(Int32 value);
	    partial void OnIdConexaoDbChanged();

	    private Int32 _IdConexaoDb;

	    [DataMember(IsRequired = true, Name = "IdConexaoDb", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Conexao Db", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicativoConexao];LookUpTitle[Seleção de (Id Conexao Db)];LookUpQuery[executeLookUpTcsAplicativoConexao];LookUpFinalize[finalizeLookUpTcsAplicativoConexao];LookUpDisplayColumns[{\"IdTcsAplicativoConexao\" : \"Id Tcs Aplicativo Conexao\", \"IdConexaoDb\" : \"Id Conexao Db\", \"NomeConexao\" : \"Nome Provider BM\"}];LookUpColumns[{\"IdTcsAplicativoConexao\" : false, \"IdConexaoDb\" : false, \"NomeConexao\" : true}];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB.ID_CONEXAO_DB];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdConexaoDb#false##12:0##Id Conexao Db#1#false##::LookUpTcsAplicativoConexao##true#false#TCS_APLICATIVO_CONEXAO#TCS_APLICATIVO_CONEXAO#Linx.Framework.BV.Ambiente#IQueryable###true#false", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB.ID_CONEXAO_DB")]
	    public Int32 IdConexaoDb
	    {
	    	    get
	    	    {
	    	          return _IdConexaoDb;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdConexaoDb != value)
	    	          {
	    	              this.ValidateProperty("IdConexaoDb", value);
	    	              this.OnIdConexaoDbChanging(value);
	    	              this.RaiseDataMemberChanging("IdConexaoDb");
	    	              this._IdConexaoDb = value;
	    	              this.RaiseDataMemberChanged("IdConexaoDb");
	    	              this.OnIdConexaoDbChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLinx
	    partial void OnIdLinxChanging(Int32 value);
	    partial void OnIdLinxChanged();

	    private Int32 _IdLinx;

	    [DataMember(Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
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
	    [Display(Name = "Ambiente", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.ID_TCS_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.ID_TCS_AMBIENTE")]
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
	    //Extensibility Partial Method Definitions For IdTcsAmbienteConexao
	    partial void OnIdTcsAmbienteConexaoChanging(Int32 value);
	    partial void OnIdTcsAmbienteConexaoChanged();

	    private Int32 _IdTcsAmbienteConexao;

	    [DataMember(IsRequired = true, Name = "IdTcsAmbienteConexao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente Conexao", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_AMBIENTE_CONEXAO.ID_TCS_AMBIENTE_CONEXAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE_CONEXAO.ID_TCS_AMBIENTE_CONEXAO")]
	    public Int32 IdTcsAmbienteConexao
	    {
	    	    get
	    	    {
	    	          return _IdTcsAmbienteConexao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAmbienteConexao != value)
	    	          {
	    	              this.ValidateProperty("IdTcsAmbienteConexao", value);
	    	              this.OnIdTcsAmbienteConexaoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsAmbienteConexao");
	    	              this._IdTcsAmbienteConexao = value;
	    	              this.RaiseDataMemberChanged("IdTcsAmbienteConexao");
	    	              this.OnIdTcsAmbienteConexaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsAplicativoConexao
	    partial void OnIdTcsAplicativoConexaoChanging(Int32 value);
	    partial void OnIdTcsAplicativoConexaoChanged();

	    private Int32 _IdTcsAplicativoConexao;

	    [DataMember(IsRequired = true, Name = "IdTcsAplicativoConexao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Aplicativo Conexao", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicativoConexao];LookUpTitle[Seleção de (Id Tcs Aplicativo Conexao)];LookUpQuery[executeLookUpTcsAplicativoConexao];LookUpFinalize[finalizeLookUpTcsAplicativoConexao];LookUpDisplayColumns[{\"IdTcsAplicativoConexao\" : \"Id Tcs Aplicativo Conexao\", \"IdConexaoDb\" : \"Id Conexao Db\", \"NomeConexao\" : \"Nome Provider BM\"}];LookUpColumns[{\"IdTcsAplicativoConexao\" : false, \"IdConexaoDb\" : false, \"NomeConexao\" : true}];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_APLICATIVO_CONEXAO.ID_TCS_APLICATIVO_CONEXAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdTcsAplicativoConexao#true##12:0##Id Tcs Aplicativo Conexao#0#false##::LookUpTcsAplicativoConexao##true#false#TCS_APLICATIVO_CONEXAO#TCS_APLICATIVO_CONEXAO#Linx.Framework.BV.Ambiente#IQueryable###true#false", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_APLICATIVO_CONEXAO.ID_TCS_APLICATIVO_CONEXAO")]
	    public Int32 IdTcsAplicativoConexao
	    {
	    	    get
	    	    {
	    	          return _IdTcsAplicativoConexao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAplicativoConexao != value)
	    	          {
	    	              this.ValidateProperty("IdTcsAplicativoConexao", value);
	    	              this.OnIdTcsAplicativoConexaoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsAplicativoConexao");
	    	              this._IdTcsAplicativoConexao = value;
	    	              this.RaiseDataMemberChanged("IdTcsAplicativoConexao");
	    	              this.OnIdTcsAplicativoConexaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsBancoServidor
	    partial void OnIdTcsBancoServidorChanging(Int32 value);
	    partial void OnIdTcsBancoServidorChanged();

	    private Int32 _IdTcsBancoServidor;

	    [DataMember(IsRequired = true, Name = "IdTcsBancoServidor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Banco Servidor", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsBancoServidor];LookUpTitle[Seleção de (Id Tcs Banco Servidor)];LookUpQuery[executeLookUpTcsBancoServidor];LookUpFinalize[finalizeLookUpTcsBancoServidor];LookUpDisplayColumns[{\"IdTcsBancoServidor\" : \"Id Tcs Banco Servidor\", \"DescricaoBancoServidor\" : \"Descrição\", \"NomeServidor\" : \"Servidor\", \"NomeBanco\" : \"Banco de Dados\", \"LxTipoServidor\" : \"Lx Tipo Servidor\", \"StringConexao\" : \"String Conexao\"}];LookUpColumns[{\"IdTcsBancoServidor\" : false, \"DescricaoBancoServidor\" : true, \"NomeServidor\" : true, \"NomeBanco\" : true, \"LxTipoServidor\" : false, \"StringConexao\" : false}];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.ID_TCS_BANCO_SERVIDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdTcsBancoServidor#true##12:0##Id Tcs Banco Servidor#0#false##::LookUpTcsBancoServidor##false#false#TCS_BANCO_SERVIDOR#TCS_BANCO_SERVIDOR#Linx.Framework.BV.Ambiente#IQueryable###true#false", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.ID_TCS_BANCO_SERVIDOR")]
	    public Int32 IdTcsBancoServidor
	    {
	    	    get
	    	    {
	    	          return _IdTcsBancoServidor;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsBancoServidor != value)
	    	          {
	    	              this.ValidateProperty("IdTcsBancoServidor", value);
	    	              this.OnIdTcsBancoServidorChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsBancoServidor");
	    	              this._IdTcsBancoServidor = value;
	    	              this.RaiseDataMemberChanged("IdTcsBancoServidor");
	    	              this.OnIdTcsBancoServidorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoServidor
	    partial void OnLxTipoServidorChanging(Byte value);
	    partial void OnLxTipoServidorChanged();

	    private Byte _LxTipoServidor;

	    [DataMember(IsRequired = true, Name = "LxTipoServidor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo Servidor", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoServidor];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsBancoServidor];LookUpTitle[Seleção de (Tipo Servidor)];LookUpQuery[executeLookUpTcsBancoServidor];LookUpFinalize[finalizeLookUpTcsBancoServidor];LookUpDisplayColumns[{\"IdTcsBancoServidor\" : \"Id Tcs Banco Servidor\", \"DescricaoBancoServidor\" : \"Descrição\", \"NomeServidor\" : \"Servidor\", \"NomeBanco\" : \"Banco de Dados\", \"LxTipoServidor\" : \"Lx Tipo Servidor\", \"StringConexao\" : \"String Conexao\"}];LookUpColumns[{\"IdTcsBancoServidor\" : false, \"DescricaoBancoServidor\" : true, \"NomeServidor\" : true, \"NomeBanco\" : true, \"LxTipoServidor\" : false, \"StringConexao\" : false}];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.LX_TIPO_SERVIDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Byte#LxTipoServidor#false##3:0##Lx Tipo Servidor#4#false##::LookUpTcsBancoServidor##false#false#TCS_BANCO_SERVIDOR#TCS_BANCO_SERVIDOR#Linx.Framework.BV.Ambiente#IQueryable###true#false", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.LX_TIPO_SERVIDOR")]
	    public Byte LxTipoServidor
	    {
	    	    get
	    	    {
	    	          return _LxTipoServidor;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoServidor != value)
	    	          {
	    	              this.ValidateProperty("LxTipoServidor", value);
	    	              this.OnLxTipoServidorChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoServidor");
	    	              this._LxTipoServidor = value;
	    	              this.RaiseDataMemberChanged("LxTipoServidor");
	    	              this.OnLxTipoServidorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeBanco
	    partial void OnNomeBancoChanging(System.String value);
	    partial void OnNomeBancoChanged();

	    private System.String _NomeBanco;

	    [DataMember(IsRequired = true, Name = "NomeBanco", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Banco de Dados", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsBancoServidor];LookUpTitle[Seleção de (Banco de Dados)];LookUpQuery[executeLookUpTcsBancoServidor];LookUpFinalize[finalizeLookUpTcsBancoServidor];LookUpDisplayColumns[{\"IdTcsBancoServidor\" : \"Id Tcs Banco Servidor\", \"DescricaoBancoServidor\" : \"Descrição\", \"NomeServidor\" : \"Servidor\", \"NomeBanco\" : \"Banco de Dados\", \"LxTipoServidor\" : \"Lx Tipo Servidor\", \"StringConexao\" : \"String Conexao\"}];LookUpColumns[{\"IdTcsBancoServidor\" : false, \"DescricaoBancoServidor\" : true, \"NomeServidor\" : true, \"NomeBanco\" : true, \"LxTipoServidor\" : false, \"StringConexao\" : false}];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.NOME_BANCO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeBanco#false##250:0##Banco de Dados#3#true##::LookUpTcsBancoServidor##false#false#TCS_BANCO_SERVIDOR#TCS_BANCO_SERVIDOR#Linx.Framework.BV.Ambiente#IQueryable###true#false", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.NOME_BANCO")]
	    public System.String NomeBanco
	    {
	    	    get
	    	    {
	    	          return _NomeBanco;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeBanco != value)
	    	          {
	    	              this.ValidateProperty("NomeBanco", value);
	    	              this.OnNomeBancoChanging(value);
	    	              this.RaiseDataMemberChanging("NomeBanco");
	    	              this._NomeBanco = value;
	    	              this.RaiseDataMemberChanged("NomeBanco");
	    	              this.OnNomeBancoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeConexao
	    partial void OnNomeConexaoChanging(System.String value);
	    partial void OnNomeConexaoChanged();

	    private System.String _NomeConexao;

	    [DataMember(IsRequired = true, Name = "NomeConexao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Provider BM", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicativoConexao];LookUpTitle[Seleção de (Nome Provider BM)];LookUpQuery[executeLookUpTcsAplicativoConexao];LookUpFinalize[finalizeLookUpTcsAplicativoConexao];LookUpDisplayColumns[{\"IdTcsAplicativoConexao\" : \"Id Tcs Aplicativo Conexao\", \"IdConexaoDb\" : \"Id Conexao Db\", \"NomeConexao\" : \"Nome Provider BM\"}];LookUpColumns[{\"IdTcsAplicativoConexao\" : false, \"IdConexaoDb\" : false, \"NomeConexao\" : true}];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB.NOME_CONEXAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeConexao#false##2500##Nome Provider BM#2#true##::LookUpTcsAplicativoConexao##true#false#TCS_APLICATIVO_CONEXAO#TCS_APLICATIVO_CONEXAO#Linx.Framework.BV.Ambiente#IQueryable###true#false", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB.NOME_CONEXAO")]
	    public System.String NomeConexao
	    {
	    	    get
	    	    {
	    	          return _NomeConexao;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeConexao != value)
	    	          {
	    	              this.ValidateProperty("NomeConexao", value);
	    	              this.OnNomeConexaoChanging(value);
	    	              this.RaiseDataMemberChanging("NomeConexao");
	    	              this._NomeConexao = value;
	    	              this.RaiseDataMemberChanged("NomeConexao");
	    	              this.OnNomeConexaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeServidor
	    partial void OnNomeServidorChanging(System.String value);
	    partial void OnNomeServidorChanged();

	    private System.String _NomeServidor;

	    [DataMember(IsRequired = true, Name = "NomeServidor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Servidor", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsBancoServidor];LookUpTitle[Seleção de (Servidor)];LookUpQuery[executeLookUpTcsBancoServidor];LookUpFinalize[finalizeLookUpTcsBancoServidor];LookUpDisplayColumns[{\"IdTcsBancoServidor\" : \"Id Tcs Banco Servidor\", \"DescricaoBancoServidor\" : \"Descrição\", \"NomeServidor\" : \"Servidor\", \"NomeBanco\" : \"Banco de Dados\", \"LxTipoServidor\" : \"Lx Tipo Servidor\", \"StringConexao\" : \"String Conexao\"}];LookUpColumns[{\"IdTcsBancoServidor\" : false, \"DescricaoBancoServidor\" : true, \"NomeServidor\" : true, \"NomeBanco\" : true, \"LxTipoServidor\" : false, \"StringConexao\" : false}];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.NOME_SERVIDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeServidor#false##250:0##Servidor#2#true##::LookUpTcsBancoServidor##false#false#TCS_BANCO_SERVIDOR#TCS_BANCO_SERVIDOR#Linx.Framework.BV.Ambiente#IQueryable###true#false", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.NOME_SERVIDOR")]
	    public System.String NomeServidor
	    {
	    	    get
	    	    {
	    	          return _NomeServidor;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeServidor != value)
	    	          {
	    	              this.ValidateProperty("NomeServidor", value);
	    	              this.OnNomeServidorChanging(value);
	    	              this.RaiseDataMemberChanging("NomeServidor");
	    	              this._NomeServidor = value;
	    	              this.RaiseDataMemberChanged("NomeServidor");
	    	              this.OnNomeServidorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringConexao
	    partial void OnStringConexaoChanging(System.String value);
	    partial void OnStringConexaoChanged();

	    private System.String _StringConexao;

	    [DataMember(IsRequired = true, Name = "StringConexao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Conexao", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(1000)]
	    [FunctionalPoint("Precision[1000:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsBancoServidor];LookUpTitle[Seleção de (String Conexao)];LookUpQuery[executeLookUpTcsBancoServidor];LookUpFinalize[finalizeLookUpTcsBancoServidor];LookUpDisplayColumns[{\"IdTcsBancoServidor\" : \"Id Tcs Banco Servidor\", \"DescricaoBancoServidor\" : \"Descrição\", \"NomeServidor\" : \"Servidor\", \"NomeBanco\" : \"Banco de Dados\", \"LxTipoServidor\" : \"Lx Tipo Servidor\", \"StringConexao\" : \"String Conexao\"}];LookUpColumns[{\"IdTcsBancoServidor\" : false, \"DescricaoBancoServidor\" : true, \"NomeServidor\" : true, \"NomeBanco\" : true, \"LxTipoServidor\" : false, \"StringConexao\" : false}];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.STRING_CONEXAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#StringConexao#false##1000:0##String Conexao#5#false##::LookUpTcsBancoServidor##false#false#TCS_BANCO_SERVIDOR#TCS_BANCO_SERVIDOR#Linx.Framework.BV.Ambiente#IQueryable###true#false", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.STRING_CONEXAO")]
	    public System.String StringConexao
	    {
	    	    get
	    	    {
	    	          return _StringConexao;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringConexao != value)
	    	          {
	    	              this.ValidateProperty("StringConexao", value);
	    	              this.OnStringConexaoChanging(value);
	    	              this.RaiseDataMemberChanging("StringConexao");
	    	              this._StringConexao = value;
	    	              this.RaiseDataMemberChanged("StringConexao");
	    	              this.OnStringConexaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescricaoAmbiente
	    partial void OnDescricaoAmbienteChanging(System.String value);
	    partial void OnDescricaoAmbienteChanged();

	    private System.String _DescricaoAmbiente;

	    [DataMember(IsRequired = true, Name = "DescricaoAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ambiente", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[IdTcsAmbiente];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.DESCRICAO_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.DESCRICAO_AMBIENTE")]
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
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.TCS_APLICACAO.DESCRICAO_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.TCS_APLICACAO.DESCRICAO_APLICACAO")]
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
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO")]
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
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.TCS_APLICACAO.EM_DESENVOLVIMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.TCS_APLICACAO.EM_DESENVOLVIMENTO")]
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
	    [Display(Name = "Id Aplicacao", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.TCS_APLICACAO.ID_APLICACAO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For IdTcsAplicativo
	    partial void OnIdTcsAplicativoChanging(Int32 value);
	    partial void OnIdTcsAplicativoChanged();

	    private Int32 _IdTcsAplicativo;

	    [DataMember(IsRequired = true, Name = "IdTcsAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Aplicativo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO")]
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
	    //Extensibility Partial Method Definitions For NomeEmpresa
	    partial void OnNomeEmpresaChanging(System.String value);
	    partial void OnNomeEmpresaChanged();

	    private System.String _NomeEmpresa;

	    [DataMember(IsRequired = true, Name = "NomeEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Empresa (Id Linx)", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
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
	    //Extensibility Partial Method Definitions For UidAplicacao
	    partial void OnUidAplicacaoChanging(System.Guid value);
	    partial void OnUidAplicacaoChanged();

	    private System.Guid _UidAplicacao;

	    [DataMember(IsRequired = true, Name = "UidAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Aplicacao", Description="", Order = 4, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.TCS_APLICACAO.UID_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.TCS_APLICACAO.UID_APLICACAO")]
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
	    //Extensibility Partial Method Definitions For UidEmpresa
	    partial void OnUidEmpresaChanging(System.Guid value);
	    partial void OnUidEmpresaChanged();

	    private System.Guid _UidEmpresa;

	    [DataMember(IsRequired = true, Name = "UidEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Empresa", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA")]
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
	    //Extensibility Partial Method Definitions For Url
	    partial void OnUrlChanging(System.String value);
	    partial void OnUrlChanged();

	    private System.String _Url;

	    [DataMember(Name = "Url", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Url Alternativa", Description="", Order = 5, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.TCS_APLICACAO.URL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.TCS_APLICACAO.URL")]
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
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.TCS_APLICACAO.URL_WORK_AREA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.TCS_APLICACAO.URL_WORK_AREA")]
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
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_AMBIENTE_CONEXAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_AMBIENTE_CONEXAO), QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE_CONEXAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_AMBIENTE_CONEXAO.ID_TCS_AMBIENTE_CONEXAO", Source = "IdTcsAmbienteConexao", Target = "ID_TCS_AMBIENTE_CONEXAO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE_CONEXAO", RelationPropertyName = "TCS_AMBIENTE_CONEXAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.ID_TCS_AMBIENTE", Source = "IdTcsAmbiente", Target = "ID_TCS_AMBIENTE", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE", RelationPropertyName = "TCS_AMBIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.ID_TCS_BANCO_SERVIDOR", Source = "IdTcsBancoServidor", Target = "ID_TCS_BANCO_SERVIDOR", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_BANCO_SERVIDOR", RelationPropertyName = "TCS_BANCO_SERVIDOR" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_AMBIENTE_CONEXAO.TCS_APLICATIVO_CONEXAO.ID_TCS_APLICATIVO_CONEXAO", Source = "IdTcsAplicativoConexao", Target = "ID_TCS_APLICATIVO_CONEXAO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICATIVO_CONEXAO", RelationPropertyName = "TCS_APLICATIVO_CONEXAO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxTipoServidorValues()
	    {
	    	    return Linx.Framework.BV.Domains.TipoServidor.GetValues();
	    }
	    private string _lxTipoServidorName;
	    [DataMember(IsRequired = false, Name = "LxTipoServidorName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo Servidor", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxTipoServidorName
	    {
	    	    get { if (this.LxTipoServidor.IsNull()) { _lxTipoServidorName = String.Empty; } else { string key = this.LxTipoServidor.ToString(); var dmValues = this.GetLxTipoServidorValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxTipoServidorName) _lxTipoServidorName = domainName; } return _lxTipoServidorName; } set { _lxTipoServidorName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Serviços];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsAmbienteServicoExcecao];ReadOnly[false];Entities[TCS_AMBIENTE_SERVICO_EXCECAO:IdTcsAmbienteServicoExcecao|TCS_SERVICO:IdTcsServico];SubQueryInfo[Select 1 From #ParentAlias#.TCS_AMBIENTE_SERVICO_EXCECAO_LISTA as #Alias#];EdmEntityName[TCS_AMBIENTE_SERVICO_EXCECAO];EntityRelations[TCS_AMBIENTE(TCS_AMBIENTE)#TCS_APLICACAO(TCS_APLICACAO)#TCS_APLICATIVO(TCS_APLICATIVO)#TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)#TCS_SERVICO(TCS_SERVICO)];EdmParentEntityName[TCS_AMBIENTE];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsAmbienteServicoExcecao")]
	[Serializable()]
	public partial class TcsAmbienteServicoExcecaoParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For IdTcsAmbiente
	    partial void OnIdTcsAmbienteChanging(Int32 value);
	    partial void OnIdTcsAmbienteChanged();

	    private Int32 _IdTcsAmbiente;

	    [DataMember(IsRequired = true, Name = "IdTcsAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ambiente", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_AMBIENTE_SERVICO_EXCECAO.TCS_AMBIENTE.ID_TCS_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE_SERVICO_EXCECAO.TCS_AMBIENTE.ID_TCS_AMBIENTE")]
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
	    //Extensibility Partial Method Definitions For IdTcsAmbienteServicoExcecao
	    partial void OnIdTcsAmbienteServicoExcecaoChanging(Int32 value);
	    partial void OnIdTcsAmbienteServicoExcecaoChanged();

	    private Int32 _IdTcsAmbienteServicoExcecao;

	    [DataMember(IsRequired = true, Name = "IdTcsAmbienteServicoExcecao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente Servico Excecao", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_AMBIENTE_SERVICO_EXCECAO.ID_TCS_AMBIENTE_SERVICO_EXCECAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE_SERVICO_EXCECAO.ID_TCS_AMBIENTE_SERVICO_EXCECAO")]
	    public Int32 IdTcsAmbienteServicoExcecao
	    {
	    	    get
	    	    {
	    	          return _IdTcsAmbienteServicoExcecao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAmbienteServicoExcecao != value)
	    	          {
	    	              this.ValidateProperty("IdTcsAmbienteServicoExcecao", value);
	    	              this.OnIdTcsAmbienteServicoExcecaoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsAmbienteServicoExcecao");
	    	              this._IdTcsAmbienteServicoExcecao = value;
	    	              this.RaiseDataMemberChanged("IdTcsAmbienteServicoExcecao");
	    	              this.OnIdTcsAmbienteServicoExcecaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsServico
	    partial void OnIdTcsServicoChanging(Int32 value);
	    partial void OnIdTcsServicoChanged();

	    private Int32 _IdTcsServico;

	    [DataMember(IsRequired = true, Name = "IdTcsServico", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Servico", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsServico];LookUpTitle[Seleção de (Id Tcs Servico)];LookUpQuery[executeLookUpTcsServico];LookUpFinalize[finalizeLookUpTcsServico];LookUpDisplayColumns[{\"IdTcsServico\" : \"Id Tcs Servico\", \"NomeServico\" : \"Nome Serviço\"}];LookUpColumns[{\"IdTcsServico\" : false, \"NomeServico\" : true}];FilterDataKey[TCS_AMBIENTE_SERVICO_EXCECAO.TCS_SERVICO.ID_TCS_SERVICO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdTcsServico#true##12:0##Id Tcs Servico#0#false##::LookUpTcsServico##true#false#TCS_SERVICO#TCS_SERVICO#Linx.Framework.BV.Ambiente#IQueryable###true#false", EdmKey="TCS_AMBIENTE_SERVICO_EXCECAO.TCS_SERVICO.ID_TCS_SERVICO")]
	    public Int32 IdTcsServico
	    {
	    	    get
	    	    {
	    	          return _IdTcsServico;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsServico != value)
	    	          {
	    	              this.ValidateProperty("IdTcsServico", value);
	    	              this.OnIdTcsServicoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsServico");
	    	              this._IdTcsServico = value;
	    	              this.RaiseDataMemberChanged("IdTcsServico");
	    	              this.OnIdTcsServicoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeServico
	    partial void OnNomeServicoChanging(System.String value);
	    partial void OnNomeServicoChanged();

	    private System.String _NomeServico;

	    [DataMember(IsRequired = true, Name = "NomeServico", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Serviço", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsServico];LookUpTitle[Seleção de (Nome Serviço)];LookUpQuery[executeLookUpTcsServico];LookUpFinalize[finalizeLookUpTcsServico];LookUpDisplayColumns[{\"IdTcsServico\" : \"Id Tcs Servico\", \"NomeServico\" : \"Nome Serviço\"}];LookUpColumns[{\"IdTcsServico\" : false, \"NomeServico\" : true}];FilterDataKey[TCS_AMBIENTE_SERVICO_EXCECAO.TCS_SERVICO.NOME_SERVICO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeServico#false##250:0##Nome Serviço#1#true##::LookUpTcsServico##true#false#TCS_SERVICO#TCS_SERVICO#Linx.Framework.BV.Ambiente#IQueryable###true#false", EdmKey="TCS_AMBIENTE_SERVICO_EXCECAO.TCS_SERVICO.NOME_SERVICO")]
	    public System.String NomeServico
	    {
	    	    get
	    	    {
	    	          return _NomeServico;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeServico != value)
	    	          {
	    	              this.ValidateProperty("NomeServico", value);
	    	              this.OnNomeServicoChanging(value);
	    	              this.RaiseDataMemberChanging("NomeServico");
	    	              this._NomeServico = value;
	    	              this.RaiseDataMemberChanged("NomeServico");
	    	              this.OnNomeServicoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Url
	    partial void OnUrlChanging(System.String value);
	    partial void OnUrlChanged();

	    private System.String _Url;

	    [DataMember(IsRequired = true, Name = "Url", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Url Alternativa", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_AMBIENTE_SERVICO_EXCECAO.URL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE_SERVICO_EXCECAO.URL")]
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
	    //Extensibility Partial Method Definitions For DescricaoAmbiente
	    partial void OnDescricaoAmbienteChanging(System.String value);
	    partial void OnDescricaoAmbienteChanged();

	    private System.String _DescricaoAmbiente;

	    [DataMember(IsRequired = true, Name = "DescricaoAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ambiente", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[IdTcsAmbiente];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_AMBIENTE_SERVICO_EXCECAO.TCS_AMBIENTE.DESCRICAO_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.DESCRICAO_AMBIENTE")]
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
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_AMBIENTE_SERVICO_EXCECAO.TCS_AMBIENTE.TCS_APLICACAO.DESCRICAO_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.TCS_APLICACAO.DESCRICAO_APLICACAO")]
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
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_AMBIENTE_SERVICO_EXCECAO.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO")]
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
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_AMBIENTE_SERVICO_EXCECAO.TCS_AMBIENTE.TCS_APLICACAO.EM_DESENVOLVIMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.TCS_APLICACAO.EM_DESENVOLVIMENTO")]
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
	    [Display(Name = "Id Aplicacao", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_AMBIENTE_SERVICO_EXCECAO.TCS_AMBIENTE.TCS_APLICACAO.ID_APLICACAO];IsMeasure[false]")]
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
	    [Display(Name = "ID Linx Ambiente", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_AMBIENTE_SERVICO_EXCECAO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For IdTcsAplicativo
	    partial void OnIdTcsAplicativoChanging(Int32 value);
	    partial void OnIdTcsAplicativoChanged();

	    private Int32 _IdTcsAplicativo;

	    [DataMember(IsRequired = true, Name = "IdTcsAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Aplicativo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_AMBIENTE_SERVICO_EXCECAO.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO")]
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
	    //Extensibility Partial Method Definitions For NomeEmpresa
	    partial void OnNomeEmpresaChanging(System.String value);
	    partial void OnNomeEmpresaChanged();

	    private System.String _NomeEmpresa;

	    [DataMember(IsRequired = true, Name = "NomeEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Empresa (Id Linx)", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_AMBIENTE_SERVICO_EXCECAO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
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
	    //Extensibility Partial Method Definitions For UidAplicacao
	    partial void OnUidAplicacaoChanging(System.Guid value);
	    partial void OnUidAplicacaoChanged();

	    private System.Guid _UidAplicacao;

	    [DataMember(IsRequired = true, Name = "UidAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Aplicacao", Description="", Order = 4, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_AMBIENTE_SERVICO_EXCECAO.TCS_AMBIENTE.TCS_APLICACAO.UID_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.TCS_APLICACAO.UID_APLICACAO")]
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
	    //Extensibility Partial Method Definitions For UidEmpresa
	    partial void OnUidEmpresaChanging(System.Guid value);
	    partial void OnUidEmpresaChanged();

	    private System.Guid _UidEmpresa;

	    [DataMember(IsRequired = true, Name = "UidEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Empresa", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_AMBIENTE_SERVICO_EXCECAO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA")]
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
	    //Extensibility Partial Method Definitions For UrlWorkArea
	    partial void OnUrlWorkAreaChanging(System.String value);
	    partial void OnUrlWorkAreaChanged();

	    private System.String _UrlWorkArea;

	    [DataMember(Name = "UrlWorkArea", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Url Work Area", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_AMBIENTE_SERVICO_EXCECAO.TCS_AMBIENTE.TCS_APLICACAO.URL_WORK_AREA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.TCS_APLICACAO.URL_WORK_AREA")]
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
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_AMBIENTE_SERVICO_EXCECAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_AMBIENTE_SERVICO_EXCECAO), QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE_SERVICO_EXCECAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_AMBIENTE_SERVICO_EXCECAO.URL", Source = "Url", Target = "URL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE_SERVICO_EXCECAO", RelationPropertyName = "TCS_AMBIENTE_SERVICO_EXCECAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_AMBIENTE_SERVICO_EXCECAO.TCS_SERVICO.ID_TCS_SERVICO", Source = "IdTcsServico", Target = "ID_TCS_SERVICO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_SERVICO", RelationPropertyName = "TCS_SERVICO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_AMBIENTE_SERVICO_EXCECAO.TCS_AMBIENTE.ID_TCS_AMBIENTE", Source = "IdTcsAmbiente", Target = "ID_TCS_AMBIENTE", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE", RelationPropertyName = "TCS_AMBIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_AMBIENTE_SERVICO_EXCECAO.ID_TCS_AMBIENTE_SERVICO_EXCECAO", Source = "IdTcsAmbienteServicoExcecao", Target = "ID_TCS_AMBIENTE_SERVICO_EXCECAO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE_SERVICO_EXCECAO", RelationPropertyName = "TCS_AMBIENTE_SERVICO_EXCECAO" });

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
	[DomainIdentifier("ProcessorOverviewAmbienteDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class AmbienteDomainService : DomainService, IDataServiceContext 
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

		
	    public AmbienteDomainService() : this("", null, null) { }
	    public AmbienteDomainService(string connectionString) : this(connectionString, null, null) { }
	    public AmbienteDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public AmbienteDomainService(Linx.Framework.Autorizacao.BM.AutorizacaoContext dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public AmbienteDomainService(string connectionString, Linx.Framework.Autorizacao.BM.AutorizacaoContext dataContext, Dictionary<string, string> headers) : base() 
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
	
	
	        TcsAmbienteUsuarioAcesso.OnSavedContextChanges(this, changeSet.ChangeSetEntries.Where(e => e.Entity is TcsAmbienteUsuarioAcesso).ToArray());
    	
	    }
		
	    private void OnTransactingChanges(ChangeSet changeSet)
	    {
	
		
	    }
	
	    private void OnTransactedChanges(ChangeSet changeSet)
	    {
	
	    
	        foreach (ChangeSetEntry entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsAmbiente))
	        {
	            ((TcsAmbiente)entry.Entity).OnTransactedChanges(this, changeSet.GetChangeOperation(entry.Entity));
	        }
        
	        foreach (ChangeSetEntry entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsAmbienteUsuarioAcesso))
	        {
	            ((TcsAmbienteUsuarioAcesso)entry.Entity).OnTransactedChanges(this, changeSet.GetChangeOperation(entry.Entity));
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
 	        var _TcsAmbienteElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsAmbiente && e.Entity.GetType().Name == "TcsAmbiente" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _TcsAmbienteElements)
 	           if (((TcsAmbiente)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 
 	        //Remove inconsistent details
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsAmbienteUsuarioAcesso && e.Entity.GetType().Name == "TcsAmbienteUsuarioAcesso" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsAmbienteConexao && e.Entity.GetType().Name == "TcsAmbienteConexao" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsAmbienteServicoExcecao && e.Entity.GetType().Name == "TcsAmbienteServicoExcecao" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
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
                  let entityAl1 = entity.TCS_APLICATIVO
	            
	            select new LookUpTcsAplicacao()		
	            {
	            
                DescricaoAplicacao = entity.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entityAl1.DESCRICAO_APLICATIVO
                , EmDesenvolvimento = entity.EM_DESENVOLVIMENTO
                , IdTcsAplicativo = entityAl1.ID_TCS_APLICATIVO
                , IdAplicacao = entity.ID_APLICACAO
                , UidAplicacao = entity.UID_APLICACAO
                , Url = entity.URL
                , UrlWorkArea = entity.URL_WORK_AREA
	            });

	            
            //Inner Group Definition
            if (propertyName.InList("DescricaoAplicativo", "IdTcsAplicativo"))
            {
               query = (from r in query select new LookUpTcsAplicacao() {
               DescricaoAplicacao = ""
               , DescricaoAplicativo = r.DescricaoAplicativo
               , EmDesenvolvimento = default(Boolean)
               , IdTcsAplicativo = r.IdTcsAplicativo
               , IdAplicacao = default(Int32)
               , UidAplicacao = default(System.Guid)
               , Url = ""
               , UrlWorkArea = ""
                }).Distinct();
            }
	
		
	
	
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
                , UidEmpresa = entity.UID_EMPRESA
	            });

	            
	
		
	
	
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
                  let entityAl1 = entity.TCS_EMPRESA_AUTENTICACAO
	            
	            select new LookUpTcsUsuarioAutenticacao()		
	            {
	            
                NomeUsuario = entity.NOME_USUARIO
                , NomeEmpresa = entityAl1.NOME_EMPRESA
                , IdLinx = entityAl1.ID_LINX
                , IdUsuario = entity.ID_USUARIO
                , UidUsuario = entity.UID_USUARIO
                , NomeAutenticacao = entity.NOME_AUTENTICACAO
	            });

	            
	
		
			
		
	        TcsAmbienteUsuarioAcesso.OnLookUpingLookUpTcsUsuarioAutenticacao(ref query, propertyName, entitySearch);
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpTcsAmbienteAdministrativo.
	    public IQueryable<LookUpTcsAmbienteAdministrativo> GetAllLookUpTcsAmbienteAdministrativo()
	    {
	        return this.GetLookUpTcsAmbienteAdministrativo(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsAmbienteAdministrativo By EntitySearch.
	    public IQueryable<LookUpTcsAmbienteAdministrativo> GetLookUpTcsAmbienteAdministrativoByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsAmbienteAdministrativo(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsAmbienteAdministrativo.
	    public IQueryable<LookUpTcsAmbienteAdministrativo> GetLookUpTcsAmbienteAdministrativo(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsAmbienteAdministrativo";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsAmbienteAdministrativo));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsAmbienteAdministrativo> query =  null;
		
			
		
	        TcsAmbienteUsuarioAcesso.OnLookingUpLookUpTcsAmbienteAdministrativo(ref query, propertyName, entitySearch);
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpTcsBancoServidor.
	    public IQueryable<LookUpTcsBancoServidor> GetAllLookUpTcsBancoServidor()
	    {
	        return this.GetLookUpTcsBancoServidor(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsBancoServidor By EntitySearch.
	    public IQueryable<LookUpTcsBancoServidor> GetLookUpTcsBancoServidorByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsBancoServidor(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsBancoServidor.
	    public IQueryable<LookUpTcsBancoServidor> GetLookUpTcsBancoServidor(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TCS_BANCO_SERVIDOR" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsBancoServidor";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsBancoServidor));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsBancoServidor> query =  
	
	            (from entity in this.DbContext.TCS_BANCO_SERVIDOR.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpTcsBancoServidor()		
	            {
	            
                IdTcsBancoServidor = entity.ID_TCS_BANCO_SERVIDOR
                , DescricaoBancoServidor = entity.DESCRICAO_BANCO_SERVIDOR
                , NomeServidor = entity.NOME_SERVIDOR
                , NomeBanco = entity.NOME_BANCO
                , LxTipoServidor = entity.LX_TIPO_SERVIDOR
                , StringConexao = entity.STRING_CONEXAO
	            });

	            
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpTcsAplicativoConexao.
	    public IQueryable<LookUpTcsAplicativoConexao> GetAllLookUpTcsAplicativoConexao()
	    {
	        return this.GetLookUpTcsAplicativoConexao(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsAplicativoConexao By EntitySearch.
	    public IQueryable<LookUpTcsAplicativoConexao> GetLookUpTcsAplicativoConexaoByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsAplicativoConexao(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsAplicativoConexao.
	    public IQueryable<LookUpTcsAplicativoConexao> GetLookUpTcsAplicativoConexao(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TCS_APLICATIVO_CONEXAO" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsAplicativoConexao";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsAplicativoConexao));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsAplicativoConexao> query =  
	
	            (from entity in this.DbContext.TCS_APLICATIVO_CONEXAO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_CONEXAO_DB
	            
	            select new LookUpTcsAplicativoConexao()		
	            {
	            
                IdTcsAplicativoConexao = entity.ID_TCS_APLICATIVO_CONEXAO
                , IdConexaoDb = entityAl1.ID_CONEXAO_DB
                , NomeConexao = entityAl1.NOME_CONEXAO
                , IdTcsAplicativo = entity.TCS_APLICATIVO.ID_TCS_APLICATIVO
	            });

	            
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpTcsServico.
	    public IQueryable<LookUpTcsServico> GetAllLookUpTcsServico()
	    {
	        return this.GetLookUpTcsServico(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsServico By EntitySearch.
	    public IQueryable<LookUpTcsServico> GetLookUpTcsServicoByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsServico(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsServico.
	    public IQueryable<LookUpTcsServico> GetLookUpTcsServico(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TCS_SERVICO" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsServico";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsServico));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsServico> query =  
	
	            (from entity in this.DbContext.TCS_SERVICO.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpTcsServico()		
	            {
	            
                IdTcsServico = entity.ID_TCS_SERVICO
                , NomeServico = entity.NOME_SERVICO
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
	
		

	        if (entityName.InList("Linx.Framework.BV.Ambiente.TcsAmbiente"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsAmbiente",
	        			NameSpace = "Linx.Framework.BV.Ambiente",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsAmbiente",
	        			ClearMethodName = "ClearTcsAmbiente",
	        			QueryMethodName  = "GetPagedTcsAmbiente",	
	        			CountingMethodName  = "GetTcsAmbiente" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Ambiente.TcsAmbiente"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Ambiente.TcsAmbiente"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Ambiente.TcsAmbiente", "Linx.Framework.BV.Ambiente.TcsAmbienteUsuarioAcesso"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsAmbienteUsuarioAcesso" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.Ambiente",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsAmbiente",	
	        			DisplayName = "Usuários",
	        			ClearMethodName = "ClearTcsAmbienteUsuarioAcesso" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsAmbienteUsuarioAcesso" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsAmbienteUsuarioAcesso" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Ambiente.TcsAmbienteUsuarioAcesso"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Ambiente.TcsAmbienteUsuarioAcesso" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Ambiente.TcsAmbiente", "Linx.Framework.BV.Ambiente.TcsAmbienteConexao"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsAmbienteConexao" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.Ambiente",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsAmbiente",	
	        			DisplayName = "Providers",
	        			ClearMethodName = "ClearTcsAmbienteConexao" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsAmbienteConexao" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsAmbienteConexao" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Ambiente.TcsAmbienteConexao"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Ambiente.TcsAmbienteConexao" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Ambiente.TcsAmbiente", "Linx.Framework.BV.Ambiente.TcsAmbienteServicoExcecao"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsAmbienteServicoExcecao" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.Ambiente",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsAmbiente",	
	        			DisplayName = "Serviços",
	        			ClearMethodName = "ClearTcsAmbienteServicoExcecao" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsAmbienteServicoExcecao" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsAmbienteServicoExcecao" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Ambiente.TcsAmbienteServicoExcecao"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Ambiente.TcsAmbienteServicoExcecao" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Ambiente.TcsServico"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsServico",
	        			NameSpace = "Linx.Framework.BV.Ambiente",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsServico",
	        			ClearMethodName = "ClearTcsServico",
	        			QueryMethodName  = "GetPagedTcsServico",	
	        			CountingMethodName  = "GetTcsServico" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Ambiente.TcsServico"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Ambiente.TcsServico"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Ambiente.TcsAmbienteRelacionado"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsAmbienteRelacionado",
	        			NameSpace = "Linx.Framework.BV.Ambiente",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsAmbienteRelacionado",
	        			ClearMethodName = "ClearTcsAmbienteRelacionado",
	        			QueryMethodName  = "GetPagedTcsAmbienteRelacionado",	
	        			CountingMethodName  = "GetTcsAmbienteRelacionado" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Ambiente.TcsAmbienteRelacionado"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Ambiente.TcsAmbienteRelacionado"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Ambiente.ServicoExcecaoInfo"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "ServicoExcecaoInfo",
	        			NameSpace = "Linx.Framework.BV.Ambiente",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "ServicoExcecaoInfo",
	        			ClearMethodName = "ClearServicoExcecaoInfo",
	        			QueryMethodName  = "GetPagedServicoExcecaoInfo",	
	        			CountingMethodName  = "GetServicoExcecaoInfo" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Ambiente.ServicoExcecaoInfo"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Ambiente.ServicoExcecaoInfo"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Ambiente.AmbienteServicoInfo"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "AmbienteServicoInfo",
	        			NameSpace = "Linx.Framework.BV.Ambiente",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "AmbienteServicoInfo",
	        			ClearMethodName = "ClearAmbienteServicoInfo",
	        			QueryMethodName  = "GetPagedAmbienteServicoInfo",	
	        			CountingMethodName  = "GetAmbienteServicoInfo" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Ambiente.AmbienteServicoInfo"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Ambiente.AmbienteServicoInfo"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Ambiente.EnvironmentInfo"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "EnvironmentInfo",
	        			NameSpace = "Linx.Framework.BV.Ambiente",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "EnvironmentInfo",
	        			ClearMethodName = "ClearEnvironmentInfo",
	        			QueryMethodName  = "GetPagedEnvironmentInfo",	
	        			CountingMethodName  = "GetEnvironmentInfo" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Ambiente.EnvironmentInfo"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Ambiente.EnvironmentInfo"), forceAll: forceAll)
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

         		    return new string[] { "Framework_AmbienteClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.AmbienteClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Framework_ambienteService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.ambienteService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear TcsAmbiente.
	    public IEnumerable<TcsAmbiente> ClearTcsAmbiente()
	    {
	        List<TcsAmbiente> result = new List<TcsAmbiente>();
	        result.Add(new TcsAmbiente());	
			
	        result[0].TcsAmbienteUsuarioAcessoList = new List<TcsAmbienteUsuarioAcesso>();
	        ((List<TcsAmbienteUsuarioAcesso>)result[0].TcsAmbienteUsuarioAcessoList).Add(new TcsAmbienteUsuarioAcesso());
			
	        result[0].TcsAmbienteConexaoList = new List<TcsAmbienteConexao>();
	        ((List<TcsAmbienteConexao>)result[0].TcsAmbienteConexaoList).Add(new TcsAmbienteConexao());
			
	        result[0].TcsAmbienteServicoExcecaoList = new List<TcsAmbienteServicoExcecao>();
	        ((List<TcsAmbienteServicoExcecao>)result[0].TcsAmbienteServicoExcecaoList).Add(new TcsAmbienteServicoExcecao());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsAmbienteUsuarioAcesso.
	    public IEnumerable<TcsAmbienteUsuarioAcesso> ClearTcsAmbienteUsuarioAcesso()
	    {
	        List<TcsAmbienteUsuarioAcesso> result = new List<TcsAmbienteUsuarioAcesso>();
	        result.Add(new TcsAmbienteUsuarioAcesso());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsAmbienteConexao.
	    public IEnumerable<TcsAmbienteConexao> ClearTcsAmbienteConexao()
	    {
	        List<TcsAmbienteConexao> result = new List<TcsAmbienteConexao>();
	        result.Add(new TcsAmbienteConexao());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsAmbienteServicoExcecao.
	    public IEnumerable<TcsAmbienteServicoExcecao> ClearTcsAmbienteServicoExcecao()
	    {
	        List<TcsAmbienteServicoExcecao> result = new List<TcsAmbienteServicoExcecao>();
	        result.Add(new TcsAmbienteServicoExcecao());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsServico.
	    public IEnumerable<TcsServico> ClearTcsServico()
	    {
	        List<TcsServico> result = new List<TcsServico>();
	        result.Add(new TcsServico());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsAmbienteRelacionado.
	    public IEnumerable<TcsAmbienteRelacionado> ClearTcsAmbienteRelacionado()
	    {
	        List<TcsAmbienteRelacionado> result = new List<TcsAmbienteRelacionado>();
	        result.Add(new TcsAmbienteRelacionado());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear ServicoExcecaoInfo.
	    public IEnumerable<ServicoExcecaoInfo> ClearServicoExcecaoInfo()
	    {
	        List<ServicoExcecaoInfo> result = new List<ServicoExcecaoInfo>();
	        result.Add(new ServicoExcecaoInfo());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear AmbienteServicoInfo.
	    public IEnumerable<AmbienteServicoInfo> ClearAmbienteServicoInfo()
	    {
	        List<AmbienteServicoInfo> result = new List<AmbienteServicoInfo>();
	        result.Add(new AmbienteServicoInfo());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear EnvironmentInfo.
	    public IEnumerable<EnvironmentInfo> ClearEnvironmentInfo()
	    {
	        List<EnvironmentInfo> result = new List<EnvironmentInfo>();
	        result.Add(new EnvironmentInfo());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsAmbiente.
	    public IQueryable<TcsAmbiente> GetTcsAmbiente()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsAmbiente> result = 
	            (from entity0 in this.DbContext.TCS_AMBIENTE
                  let entity0Al1 = entity0.TCS_APLICACAO
                  let entity0Al3 = entity0.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_APLICACAO.TCS_APLICATIVO
	            
	            	
	            select new TcsAmbiente()		
	            {
	            
                DescricaoAmbiente = entity0.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al1.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0Al2.DESCRICAO_APLICATIVO
                , EmDesenvolvimento = entity0Al1.EM_DESENVOLVIMENTO
                , IdAplicacao = entity0Al1.ID_APLICACAO
                , IdLinx = entity0Al3.ID_LINX
                , IdTcsAmbiente = entity0.ID_TCS_AMBIENTE
                , IdTcsAplicativo = entity0Al2.ID_TCS_APLICATIVO
                , NomeEmpresa = entity0Al3.NOME_EMPRESA
                , UidAplicacao = entity0Al1.UID_APLICACAO
                , UidEmpresa = entity0Al3.UID_EMPRESA
                , Url = entity0Al1.URL
                , UrlWorkArea = entity0Al1.URL_WORK_AREA
			
                ,TcsAmbienteUsuarioAcessoList = 
	                        (from entity1 in entity0.TCS_USUARIO_ACESSO_LISTA
                                  let entity1Al5 = entity1.TCS_AMBIENTE
                                  let entity1Al1 = entity1.TCS_AMBIENTE1
                                  let entity1Al6 = entity1.TCS_USUARIO_AUTENTICACAO
                                  let entity1Al2 = entity1.TCS_AMBIENTE1.TCS_APLICACAO
                                  let entity1Al4 = entity1.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO
                                  let entity1Al3 = entity1.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO
	                        
	                        	
	                        select new TcsAmbienteUsuarioAcesso()
	                        {
	                        
                                DescricaoAmbienteRelacionado = entity1Al1.DESCRICAO_AMBIENTE
                                , DescricaoAplicacaoAmbienteRelacionado = entity1Al2.DESCRICAO_APLICACAO
                                , IdLinx = entity1Al3.ID_LINX
                                , IdLinxAmbienteRelacionado = entity1Al4.ID_LINX
                                , IdTcsAmbiente = entity1Al5.ID_TCS_AMBIENTE
                                , IdTcsAmbienteRelacionado = entity1Al1.ID_TCS_AMBIENTE
                                , IdTcsUsuarioAcesso = entity1.ID_TCS_USUARIO_ACESSO
                                , IdUsuario = entity1Al6.ID_USUARIO
                                , IndicaAdministrador = entity1.INDICA_ADMINISTRADOR
                                , IndicaMultiGpecon = entity1.INDICA_MULTI_GPECON
                                , NomeEmpresa = entity1Al3.NOME_EMPRESA
                                , NomeEmpresaAmbienteRelacionado = entity1Al4.NOME_EMPRESA
                                , UidUsuario = entity1Al6.UID_USUARIO
                                , NomeAutenticacao = entity1Al6.NOME_AUTENTICACAO
                                , NomeUsuario = entity1Al6.NOME_USUARIO
		
	                        }
	                        )
			
                ,TcsAmbienteConexaoList = 
	                        (from entity1 in entity0.TCS_AMBIENTE_CONEXAO_LISTA
                                  let entity1Al4 = entity1.TCS_AMBIENTE
                                  let entity1Al1 = entity1.TCS_BANCO_SERVIDOR
                                  let entity1Al5 = entity1.TCS_APLICATIVO_CONEXAO
                                  let entity1Al2 = entity1.TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB
                                  let entity1Al3 = entity1.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
	                        
	                        	
	                        select new TcsAmbienteConexao()
	                        {
	                        
                                DescricaoBancoServidor = entity1Al1.DESCRICAO_BANCO_SERVIDOR
                                , IdConexaoDb = entity1Al2.ID_CONEXAO_DB
                                , IdLinx = entity1Al3.ID_LINX
                                , IdTcsAmbiente = entity1Al4.ID_TCS_AMBIENTE
                                , IdTcsAmbienteConexao = entity1.ID_TCS_AMBIENTE_CONEXAO
                                , IdTcsAplicativoConexao = entity1Al5.ID_TCS_APLICATIVO_CONEXAO
                                , IdTcsBancoServidor = entity1Al1.ID_TCS_BANCO_SERVIDOR
                                , LxTipoServidor = entity1Al1.LX_TIPO_SERVIDOR
                                , LxTipoServidorName = ((entity1Al1.LX_TIPO_SERVIDOR) == 2 ? "Oracle" : ((entity1Al1.LX_TIPO_SERVIDOR) == 3 ? "SQLite" : ((entity1Al1.LX_TIPO_SERVIDOR) == 1 ? "SQL Server" : "")))
                                , NomeBanco = entity1Al1.NOME_BANCO
                                , NomeConexao = entity1Al2.NOME_CONEXAO
                                , NomeServidor = entity1Al1.NOME_SERVIDOR
                                , StringConexao = entity1Al1.STRING_CONEXAO
		
	                        }
	                        )
			
                ,TcsAmbienteServicoExcecaoList = 
	                        (from entity1 in entity0.TCS_AMBIENTE_SERVICO_EXCECAO_LISTA
                                  let entity1Al2 = entity1.TCS_SERVICO
                                  let entity1Al1 = entity1.TCS_AMBIENTE
	                        
	                        	
	                        select new TcsAmbienteServicoExcecao()
	                        {
	                        
                                IdTcsAmbiente = entity1Al1.ID_TCS_AMBIENTE
                                , IdTcsAmbienteServicoExcecao = entity1.ID_TCS_AMBIENTE_SERVICO_EXCECAO
                                , IdTcsServico = entity1Al2.ID_TCS_SERVICO
                                , NomeServico = entity1Al2.NOME_SERVICO
                                , Url = entity1.URL
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsAmbienteUsuarioAcesso.
	    public IQueryable<TcsAmbienteUsuarioAcesso> GetTcsAmbienteUsuarioAcesso()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsAmbienteUsuarioAcesso> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_ACESSO
                  let entity0Al5 = entity0.TCS_AMBIENTE
                  let entity0Al1 = entity0.TCS_AMBIENTE1
                  let entity0Al6 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_AMBIENTE1.TCS_APLICACAO
                  let entity0Al4 = entity0.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al3 = entity0.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsAmbienteUsuarioAcesso()		
	            {
	            
                DescricaoAmbienteRelacionado = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAplicacaoAmbienteRelacionado = entity0Al2.DESCRICAO_APLICACAO
                , IdLinx = entity0Al3.ID_LINX
                , IdLinxAmbienteRelacionado = entity0Al4.ID_LINX
                , IdTcsAmbiente = entity0Al5.ID_TCS_AMBIENTE
                , IdTcsAmbienteRelacionado = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsUsuarioAcesso = entity0.ID_TCS_USUARIO_ACESSO
                , IdUsuario = entity0Al6.ID_USUARIO
                , IndicaAdministrador = entity0.INDICA_ADMINISTRADOR
                , IndicaMultiGpecon = entity0.INDICA_MULTI_GPECON
                , NomeEmpresa = entity0Al3.NOME_EMPRESA
                , NomeEmpresaAmbienteRelacionado = entity0Al4.NOME_EMPRESA
                , UidUsuario = entity0Al6.UID_USUARIO
                , NomeAutenticacao = entity0Al6.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al6.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsAmbienteConexao.
	    public IQueryable<TcsAmbienteConexao> GetTcsAmbienteConexao()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsAmbienteConexao> result = 
	            (from entity0 in this.DbContext.TCS_AMBIENTE_CONEXAO
                  let entity0Al4 = entity0.TCS_AMBIENTE
                  let entity0Al1 = entity0.TCS_BANCO_SERVIDOR
                  let entity0Al5 = entity0.TCS_APLICATIVO_CONEXAO
                  let entity0Al2 = entity0.TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB
                  let entity0Al3 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsAmbienteConexao()		
	            {
	            
                DescricaoBancoServidor = entity0Al1.DESCRICAO_BANCO_SERVIDOR
                , IdConexaoDb = entity0Al2.ID_CONEXAO_DB
                , IdLinx = entity0Al3.ID_LINX
                , IdTcsAmbiente = entity0Al4.ID_TCS_AMBIENTE
                , IdTcsAmbienteConexao = entity0.ID_TCS_AMBIENTE_CONEXAO
                , IdTcsAplicativoConexao = entity0Al5.ID_TCS_APLICATIVO_CONEXAO
                , IdTcsBancoServidor = entity0Al1.ID_TCS_BANCO_SERVIDOR
                , LxTipoServidor = entity0Al1.LX_TIPO_SERVIDOR
                , LxTipoServidorName = ((entity0Al1.LX_TIPO_SERVIDOR) == 2 ? "Oracle" : ((entity0Al1.LX_TIPO_SERVIDOR) == 3 ? "SQLite" : ((entity0Al1.LX_TIPO_SERVIDOR) == 1 ? "SQL Server" : "")))
                , NomeBanco = entity0Al1.NOME_BANCO
                , NomeConexao = entity0Al2.NOME_CONEXAO
                , NomeServidor = entity0Al1.NOME_SERVIDOR
                , StringConexao = entity0Al1.STRING_CONEXAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsAmbienteServicoExcecao.
	    public IQueryable<TcsAmbienteServicoExcecao> GetTcsAmbienteServicoExcecao()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsAmbienteServicoExcecao> result = 
	            (from entity0 in this.DbContext.TCS_AMBIENTE_SERVICO_EXCECAO
                  let entity0Al2 = entity0.TCS_SERVICO
                  let entity0Al1 = entity0.TCS_AMBIENTE
	            
	            	
	            select new TcsAmbienteServicoExcecao()		
	            {
	            
                IdTcsAmbiente = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsAmbienteServicoExcecao = entity0.ID_TCS_AMBIENTE_SERVICO_EXCECAO
                , IdTcsServico = entity0Al2.ID_TCS_SERVICO
                , NomeServico = entity0Al2.NOME_SERVICO
                , Url = entity0.URL
		
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
                  let entity0Al3 = entity0.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_APLICACAO.TCS_APLICATIVO
	            
	            	
	            select new TcsAmbiente()		
	            {
	            
                DescricaoAmbiente = entity0.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al1.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0Al2.DESCRICAO_APLICATIVO
                , EmDesenvolvimento = entity0Al1.EM_DESENVOLVIMENTO
                , IdAplicacao = entity0Al1.ID_APLICACAO
                , IdLinx = entity0Al3.ID_LINX
                , IdTcsAmbiente = entity0.ID_TCS_AMBIENTE
                , IdTcsAplicativo = entity0Al2.ID_TCS_APLICATIVO
                , NomeEmpresa = entity0Al3.NOME_EMPRESA
                , UidAplicacao = entity0Al1.UID_APLICACAO
                , UidEmpresa = entity0Al3.UID_EMPRESA
                , Url = entity0Al1.URL
                , UrlWorkArea = entity0Al1.URL_WORK_AREA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAmbienteUsuarioAcessoNoAssociations.
	    public IQueryable<TcsAmbienteUsuarioAcesso> GetTcsAmbienteUsuarioAcessoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsAmbienteUsuarioAcesso> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_ACESSO
                  let entity0Al5 = entity0.TCS_AMBIENTE
                  let entity0Al1 = entity0.TCS_AMBIENTE1
                  let entity0Al6 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_AMBIENTE1.TCS_APLICACAO
                  let entity0Al4 = entity0.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al3 = entity0.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsAmbienteUsuarioAcesso()		
	            {
	            
                DescricaoAmbienteRelacionado = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAplicacaoAmbienteRelacionado = entity0Al2.DESCRICAO_APLICACAO
                , IdLinx = entity0Al3.ID_LINX
                , IdLinxAmbienteRelacionado = entity0Al4.ID_LINX
                , IdTcsAmbiente = entity0Al5.ID_TCS_AMBIENTE
                , IdTcsAmbienteRelacionado = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsUsuarioAcesso = entity0.ID_TCS_USUARIO_ACESSO
                , IdUsuario = entity0Al6.ID_USUARIO
                , IndicaAdministrador = entity0.INDICA_ADMINISTRADOR
                , IndicaMultiGpecon = entity0.INDICA_MULTI_GPECON
                , NomeEmpresa = entity0Al3.NOME_EMPRESA
                , NomeEmpresaAmbienteRelacionado = entity0Al4.NOME_EMPRESA
                , UidUsuario = entity0Al6.UID_USUARIO
                , NomeAutenticacao = entity0Al6.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al6.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAmbienteConexaoNoAssociations.
	    public IQueryable<TcsAmbienteConexao> GetTcsAmbienteConexaoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsAmbienteConexao> result = 
	            (from entity0 in this.DbContext.TCS_AMBIENTE_CONEXAO
                  let entity0Al4 = entity0.TCS_AMBIENTE
                  let entity0Al1 = entity0.TCS_BANCO_SERVIDOR
                  let entity0Al5 = entity0.TCS_APLICATIVO_CONEXAO
                  let entity0Al2 = entity0.TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB
                  let entity0Al3 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsAmbienteConexao()		
	            {
	            
                DescricaoBancoServidor = entity0Al1.DESCRICAO_BANCO_SERVIDOR
                , IdConexaoDb = entity0Al2.ID_CONEXAO_DB
                , IdLinx = entity0Al3.ID_LINX
                , IdTcsAmbiente = entity0Al4.ID_TCS_AMBIENTE
                , IdTcsAmbienteConexao = entity0.ID_TCS_AMBIENTE_CONEXAO
                , IdTcsAplicativoConexao = entity0Al5.ID_TCS_APLICATIVO_CONEXAO
                , IdTcsBancoServidor = entity0Al1.ID_TCS_BANCO_SERVIDOR
                , LxTipoServidor = entity0Al1.LX_TIPO_SERVIDOR
                , LxTipoServidorName = ((entity0Al1.LX_TIPO_SERVIDOR) == 2 ? "Oracle" : ((entity0Al1.LX_TIPO_SERVIDOR) == 3 ? "SQLite" : ((entity0Al1.LX_TIPO_SERVIDOR) == 1 ? "SQL Server" : "")))
                , NomeBanco = entity0Al1.NOME_BANCO
                , NomeConexao = entity0Al2.NOME_CONEXAO
                , NomeServidor = entity0Al1.NOME_SERVIDOR
                , StringConexao = entity0Al1.STRING_CONEXAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAmbienteServicoExcecaoNoAssociations.
	    public IQueryable<TcsAmbienteServicoExcecao> GetTcsAmbienteServicoExcecaoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsAmbienteServicoExcecao> result = 
	            (from entity0 in this.DbContext.TCS_AMBIENTE_SERVICO_EXCECAO
                  let entity0Al2 = entity0.TCS_SERVICO
                  let entity0Al1 = entity0.TCS_AMBIENTE
	            
	            	
	            select new TcsAmbienteServicoExcecao()		
	            {
	            
                IdTcsAmbiente = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsAmbienteServicoExcecao = entity0.ID_TCS_AMBIENTE_SERVICO_EXCECAO
                , IdTcsServico = entity0Al2.ID_TCS_SERVICO
                , NomeServico = entity0Al2.NOME_SERVICO
                , Url = entity0.URL
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsServico.
	    public IQueryable<TcsServico> GetTcsServico()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsServico> result = 
	            (from entity0 in this.DbContext.TCS_SERVICO
	            
	            	
	            select new TcsServico()		
	            {
	            
                IdTcsServico = entity0.ID_TCS_SERVICO
                , NomeServico = entity0.NOME_SERVICO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsServicoNoAssociations.
	    public IQueryable<TcsServico> GetTcsServicoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsServico> result = 
	            (from entity0 in this.DbContext.TCS_SERVICO
	            
	            	
	            select new TcsServico()		
	            {
	            
                IdTcsServico = entity0.ID_TCS_SERVICO
                , NomeServico = entity0.NOME_SERVICO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsAmbienteRelacionado.
	    public IQueryable<TcsAmbienteRelacionado> GetTcsAmbienteRelacionado()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsAmbienteRelacionado> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_ACESSO
                  let entity0Al1 = entity0.TCS_AMBIENTE
                  let entity0Al5 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_AMBIENTE.TCS_APLICACAO
                  let entity0Al4 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al3 = entity0.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
	            
	            	
	            select new TcsAmbienteRelacionado()		
	            {
	            
                DescricaoAmbienteRelacionado = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAplicacaoAmbienteRelacionado = entity0Al2.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0Al3.DESCRICAO_APLICATIVO
                , IdAplicacao = entity0Al2.ID_APLICACAO
                , IdLinxAmbienteRelacionado = entity0Al4.ID_LINX
                , IdTcsAmbienteRelacionado = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsAplicativo = entity0Al3.ID_TCS_APLICATIVO
                , IdTcsUsuarioAcesso = entity0.ID_TCS_USUARIO_ACESSO
                , IdUsuario = entity0Al5.ID_USUARIO
                , NomeEmpresaAmbienteRelacionado = entity0Al4.NOME_EMPRESA
                , NomeAutenticacao = entity0Al5.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al5.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAmbienteRelacionadoNoAssociations.
	    public IQueryable<TcsAmbienteRelacionado> GetTcsAmbienteRelacionadoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsAmbienteRelacionado> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_ACESSO
                  let entity0Al1 = entity0.TCS_AMBIENTE
                  let entity0Al5 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_AMBIENTE.TCS_APLICACAO
                  let entity0Al4 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al3 = entity0.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
	            
	            	
	            select new TcsAmbienteRelacionado()		
	            {
	            
                DescricaoAmbienteRelacionado = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAplicacaoAmbienteRelacionado = entity0Al2.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0Al3.DESCRICAO_APLICATIVO
                , IdAplicacao = entity0Al2.ID_APLICACAO
                , IdLinxAmbienteRelacionado = entity0Al4.ID_LINX
                , IdTcsAmbienteRelacionado = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsAplicativo = entity0Al3.ID_TCS_APLICATIVO
                , IdTcsUsuarioAcesso = entity0.ID_TCS_USUARIO_ACESSO
                , IdUsuario = entity0Al5.ID_USUARIO
                , NomeEmpresaAmbienteRelacionado = entity0Al4.NOME_EMPRESA
                , NomeAutenticacao = entity0Al5.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al5.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get ServicoExcecaoInfo.
	    public IEnumerable<ServicoExcecaoInfo> GetServicoExcecaoInfo()
	    {




	
	        IEnumerable<ServicoExcecaoInfo> result = new List<ServicoExcecaoInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get ServicoExcecaoInfoNoAssociations.
	    public IEnumerable<ServicoExcecaoInfo> GetServicoExcecaoInfoNoAssociations()
	    {




	
	        IEnumerable<ServicoExcecaoInfo> result = new List<ServicoExcecaoInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get AmbienteServicoInfo.
	    public IEnumerable<AmbienteServicoInfo> GetAmbienteServicoInfo()
	    {




	
	        IEnumerable<AmbienteServicoInfo> result = new List<AmbienteServicoInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get AmbienteServicoInfoNoAssociations.
	    public IEnumerable<AmbienteServicoInfo> GetAmbienteServicoInfoNoAssociations()
	    {




	
	        IEnumerable<AmbienteServicoInfo> result = new List<AmbienteServicoInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get EnvironmentInfo.
	    public IEnumerable<EnvironmentInfo> GetEnvironmentInfo()
	    {




	
	        IEnumerable<EnvironmentInfo> result = new List<EnvironmentInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get EnvironmentInfoNoAssociations.
	    public IEnumerable<EnvironmentInfo> GetEnvironmentInfoNoAssociations()
	    {




	
	        IEnumerable<EnvironmentInfo> result = new List<EnvironmentInfo>();
	  	
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	//Add filtering disabled property for TCS_USUARIO_ACESSO
	    	string[] bmDisabledTcsAmbienteUsuarioAcessoList = this.GetEDM().GetFilteringDisabledList("TCS_USUARIO_ACESSO");
	    	if (bmDisabledTcsAmbienteUsuarioAcessoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsAmbienteUsuarioAcessoList.Contains("TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO"))
	    		{
	    			result.Add("TcsAmbienteUsuarioAcesso|IdTcsUsuarioAcesso");
	    			result.Add("TcsAmbienteUsuarioAcesso|TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO");
	    		}
	
	    		if (bmDisabledTcsAmbienteUsuarioAcessoList.Contains("TCS_USUARIO_ACESSO.INDICA_ADMINISTRADOR"))
	    		{
	    			result.Add("TcsAmbienteUsuarioAcesso|IndicaAdministrador");
	    			result.Add("TcsAmbienteUsuarioAcesso|TCS_USUARIO_ACESSO.INDICA_ADMINISTRADOR");
	    		}
	
	    		if (bmDisabledTcsAmbienteUsuarioAcessoList.Contains("TCS_USUARIO_ACESSO.INDICA_MULTI_GPECON"))
	    		{
	    			result.Add("TcsAmbienteUsuarioAcesso|IndicaMultiGpecon");
	    			result.Add("TcsAmbienteUsuarioAcesso|TCS_USUARIO_ACESSO.INDICA_MULTI_GPECON");
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
	    	//Add filtering disabled property for TCS_AMBIENTE_CONEXAO
	    	string[] bmDisabledTcsAmbienteConexaoList = this.GetEDM().GetFilteringDisabledList("TCS_AMBIENTE_CONEXAO");
	    	if (bmDisabledTcsAmbienteConexaoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsAmbienteConexaoList.Contains("TCS_AMBIENTE_CONEXAO.ID_TCS_AMBIENTE_CONEXAO"))
	    		{
	    			result.Add("TcsAmbienteConexao|IdTcsAmbienteConexao");
	    			result.Add("TcsAmbienteConexao|TCS_AMBIENTE_CONEXAO.ID_TCS_AMBIENTE_CONEXAO");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_AMBIENTE_SERVICO_EXCECAO
	    	string[] bmDisabledTcsAmbienteServicoExcecaoList = this.GetEDM().GetFilteringDisabledList("TCS_AMBIENTE_SERVICO_EXCECAO");
	    	if (bmDisabledTcsAmbienteServicoExcecaoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsAmbienteServicoExcecaoList.Contains("TCS_AMBIENTE_SERVICO_EXCECAO.ID_TCS_AMBIENTE_SERVICO_EXCECAO"))
	    		{
	    			result.Add("TcsAmbienteServicoExcecao|IdTcsAmbienteServicoExcecao");
	    			result.Add("TcsAmbienteServicoExcecao|TCS_AMBIENTE_SERVICO_EXCECAO.ID_TCS_AMBIENTE_SERVICO_EXCECAO");
	    		}
	
	    		if (bmDisabledTcsAmbienteServicoExcecaoList.Contains("TCS_AMBIENTE_SERVICO_EXCECAO.URL"))
	    		{
	    			result.Add("TcsAmbienteServicoExcecao|Url");
	    			result.Add("TcsAmbienteServicoExcecao|TCS_AMBIENTE_SERVICO_EXCECAO.URL");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_SERVICO
	    	string[] bmDisabledTcsServicoList = this.GetEDM().GetFilteringDisabledList("TCS_SERVICO");
	    	if (bmDisabledTcsServicoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsServicoList.Contains("TCS_SERVICO.ID_TCS_SERVICO"))
	    		{
	    			result.Add("TcsServico|IdTcsServico");
	    			result.Add("TcsServico|TCS_SERVICO.ID_TCS_SERVICO");
	    		}
	
	    		if (bmDisabledTcsServicoList.Contains("TCS_SERVICO.NOME_SERVICO"))
	    		{
	    			result.Add("TcsServico|NomeServico");
	    			result.Add("TcsServico|TCS_SERVICO.NOME_SERVICO");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_USUARIO_ACESSO
	    	string[] bmDisabledTcsAmbienteRelacionadoList = this.GetEDM().GetFilteringDisabledList("TCS_USUARIO_ACESSO");
	    	if (bmDisabledTcsAmbienteRelacionadoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsAmbienteRelacionadoList.Contains("TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO"))
	    		{
	    			result.Add("TcsAmbienteRelacionado|IdTcsUsuarioAcesso");
	    			result.Add("TcsAmbienteRelacionado|TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO");
	    		}
	    	}
	    	return result;
	    }

	    #endregion Excluded Filters

	    #region Get By EntitySearchId Methods Definitions.
	
				
	    [Ignore]
	    //Get TcsAmbiente By EntitySearchId.
	    public IQueryable<TcsAmbiente> GetTcsAmbienteByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsAmbienteByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsAmbienteUsuarioAcesso By EntitySearchId.
	    public IQueryable<TcsAmbienteUsuarioAcesso> GetTcsAmbienteUsuarioAcessoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsAmbienteUsuarioAcessoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsAmbienteConexao By EntitySearchId.
	    public IQueryable<TcsAmbienteConexao> GetTcsAmbienteConexaoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsAmbienteConexaoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsAmbienteServicoExcecao By EntitySearchId.
	    public IQueryable<TcsAmbienteServicoExcecao> GetTcsAmbienteServicoExcecaoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsAmbienteServicoExcecaoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsAmbiente By EntitySearchId.
	    public IQueryable<TcsAmbiente> GetTcsAmbienteByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsAmbienteByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsAmbienteUsuarioAcesso By EntitySearchId.
	    public IQueryable<TcsAmbienteUsuarioAcesso> GetTcsAmbienteUsuarioAcessoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsAmbienteUsuarioAcessoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsAmbienteConexao By EntitySearchId.
	    public IQueryable<TcsAmbienteConexao> GetTcsAmbienteConexaoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsAmbienteConexaoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsAmbienteServicoExcecao By EntitySearchId.
	    public IQueryable<TcsAmbienteServicoExcecao> GetTcsAmbienteServicoExcecaoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsAmbienteServicoExcecaoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsServico By EntitySearchId.
	    public IQueryable<TcsServico> GetTcsServicoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsServicoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsServico By EntitySearchId.
	    public IQueryable<TcsServico> GetTcsServicoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsServicoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsAmbienteRelacionado By EntitySearchId.
	    public IQueryable<TcsAmbienteRelacionado> GetTcsAmbienteRelacionadoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsAmbienteRelacionadoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsAmbienteRelacionado By EntitySearchId.
	    public IQueryable<TcsAmbienteRelacionado> GetTcsAmbienteRelacionadoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsAmbienteRelacionadoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get ServicoExcecaoInfo By EntitySearchId.
	    public IEnumerable<ServicoExcecaoInfo> GetServicoExcecaoInfoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetServicoExcecaoInfoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get ServicoExcecaoInfo By EntitySearchId.
	    public IEnumerable<ServicoExcecaoInfo> GetServicoExcecaoInfoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetServicoExcecaoInfoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get AmbienteServicoInfo By EntitySearchId.
	    public IEnumerable<AmbienteServicoInfo> GetAmbienteServicoInfoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetAmbienteServicoInfoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get AmbienteServicoInfo By EntitySearchId.
	    public IEnumerable<AmbienteServicoInfo> GetAmbienteServicoInfoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetAmbienteServicoInfoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get EnvironmentInfo By EntitySearchId.
	    public IEnumerable<EnvironmentInfo> GetEnvironmentInfoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetEnvironmentInfoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get EnvironmentInfo By EntitySearchId.
	    public IEnumerable<EnvironmentInfo> GetEnvironmentInfoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetEnvironmentInfoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get TcsAmbiente By Example.
	    [Ignore]
	    public IQueryable<TcsAmbiente> GetTcsAmbienteByExample(TcsAmbiente entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsAmbienteByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsAmbienteUsuarioAcesso By Example.
	    [Ignore]
	    public IQueryable<TcsAmbienteUsuarioAcesso> GetTcsAmbienteUsuarioAcessoByExample(TcsAmbienteUsuarioAcesso entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsAmbienteUsuarioAcessoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsAmbienteConexao By Example.
	    [Ignore]
	    public IQueryable<TcsAmbienteConexao> GetTcsAmbienteConexaoByExample(TcsAmbienteConexao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsAmbienteConexaoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsAmbienteServicoExcecao By Example.
	    [Ignore]
	    public IQueryable<TcsAmbienteServicoExcecao> GetTcsAmbienteServicoExcecaoByExample(TcsAmbienteServicoExcecao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsAmbienteServicoExcecaoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsAmbiente By Example.
	    [Ignore]
	    public IQueryable<TcsAmbiente> GetTcsAmbienteByExampleNoAssociations(TcsAmbiente entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsAmbienteByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsAmbienteUsuarioAcesso By Example.
	    [Ignore]
	    public IQueryable<TcsAmbienteUsuarioAcesso> GetTcsAmbienteUsuarioAcessoByExampleNoAssociations(TcsAmbienteUsuarioAcesso entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsAmbienteUsuarioAcessoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsAmbienteConexao By Example.
	    [Ignore]
	    public IQueryable<TcsAmbienteConexao> GetTcsAmbienteConexaoByExampleNoAssociations(TcsAmbienteConexao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsAmbienteConexaoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsAmbienteServicoExcecao By Example.
	    [Ignore]
	    public IQueryable<TcsAmbienteServicoExcecao> GetTcsAmbienteServicoExcecaoByExampleNoAssociations(TcsAmbienteServicoExcecao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsAmbienteServicoExcecaoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsServico By Example.
	    [Ignore]
	    public IQueryable<TcsServico> GetTcsServicoByExample(TcsServico entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsServicoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsServico By Example.
	    [Ignore]
	    public IQueryable<TcsServico> GetTcsServicoByExampleNoAssociations(TcsServico entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsServicoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsAmbienteRelacionado By Example.
	    [Ignore]
	    public IQueryable<TcsAmbienteRelacionado> GetTcsAmbienteRelacionadoByExample(TcsAmbienteRelacionado entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsAmbienteRelacionadoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsAmbienteRelacionado By Example.
	    [Ignore]
	    public IQueryable<TcsAmbienteRelacionado> GetTcsAmbienteRelacionadoByExampleNoAssociations(TcsAmbienteRelacionado entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsAmbienteRelacionadoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get ServicoExcecaoInfo By Example.
	    [Ignore]
	    public IEnumerable<ServicoExcecaoInfo> GetServicoExcecaoInfoByExample(ServicoExcecaoInfo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetServicoExcecaoInfoByEntitySearch(queryAnalysis);
	    }
			
	    //Get ServicoExcecaoInfo By Example.
	    [Ignore]
	    public IEnumerable<ServicoExcecaoInfo> GetServicoExcecaoInfoByExampleNoAssociations(ServicoExcecaoInfo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetServicoExcecaoInfoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get AmbienteServicoInfo By Example.
	    [Ignore]
	    public IEnumerable<AmbienteServicoInfo> GetAmbienteServicoInfoByExample(AmbienteServicoInfo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetAmbienteServicoInfoByEntitySearch(queryAnalysis);
	    }
			
	    //Get AmbienteServicoInfo By Example.
	    [Ignore]
	    public IEnumerable<AmbienteServicoInfo> GetAmbienteServicoInfoByExampleNoAssociations(AmbienteServicoInfo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetAmbienteServicoInfoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get EnvironmentInfo By Example.
	    [Ignore]
	    public IEnumerable<EnvironmentInfo> GetEnvironmentInfoByExample(EnvironmentInfo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetEnvironmentInfoByEntitySearch(queryAnalysis);
	    }
			
	    //Get EnvironmentInfo By Example.
	    [Ignore]
	    public IEnumerable<EnvironmentInfo> GetEnvironmentInfoByExampleNoAssociations(EnvironmentInfo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetEnvironmentInfoByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public TcsAmbienteUsuarioAcesso GetTcsAmbienteUsuarioAcessoByKey(Int32 idTcsUsuarioAcesso)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsAmbienteUsuarioAcesso");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsUsuarioAcesso"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsUsuarioAcesso));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsAmbienteUsuarioAcessoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	    public TcsAmbienteConexao GetTcsAmbienteConexaoByKey(Int32 idTcsAmbienteConexao)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsAmbienteConexao");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsAmbienteConexao"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsAmbienteConexao));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsAmbienteConexaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsAmbienteServicoExcecao GetTcsAmbienteServicoExcecaoByKey(Int32 idTcsAmbienteServicoExcecao)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsAmbienteServicoExcecao");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsAmbienteServicoExcecao"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsAmbienteServicoExcecao));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsAmbienteServicoExcecaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsServico GetTcsServicoByKey(Int32 idTcsServico)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsServico");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsServico"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsServico));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsServicoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsAmbienteRelacionado GetTcsAmbienteRelacionadoByKey(Int32 idTcsUsuarioAcesso)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsAmbienteRelacionado");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsUsuarioAcesso"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsUsuarioAcesso));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsAmbienteRelacionadoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public ServicoExcecaoInfo GetServicoExcecaoInfoByKey(int idTcsAmbiente, string servico)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("ServicoExcecaoInfo");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsAmbiente"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsAmbiente));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "Servico"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, servico));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetServicoExcecaoInfoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public AmbienteServicoInfo GetAmbienteServicoInfoByKey(string hash)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("AmbienteServicoInfo");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "Hash"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, hash));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetAmbienteServicoInfoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public EnvironmentInfo GetEnvironmentInfoByKey(int environmentId)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("EnvironmentInfo");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "EnvironmentId"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, environmentId));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetEnvironmentInfoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    
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
                  let entity0Al3 = entity0.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_APLICACAO.TCS_APLICATIVO
	            
	            	
	            select new TcsAmbiente()		
	            {
	            
                DescricaoAmbiente = entity0.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al1.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0Al2.DESCRICAO_APLICATIVO
                , EmDesenvolvimento = entity0Al1.EM_DESENVOLVIMENTO
                , IdAplicacao = entity0Al1.ID_APLICACAO
                , IdLinx = entity0Al3.ID_LINX
                , IdTcsAmbiente = entity0.ID_TCS_AMBIENTE
                , IdTcsAplicativo = entity0Al2.ID_TCS_APLICATIVO
                , NomeEmpresa = entity0Al3.NOME_EMPRESA
                , UidAplicacao = entity0Al1.UID_APLICACAO
                , UidEmpresa = entity0Al3.UID_EMPRESA
                , Url = entity0Al1.URL
                , UrlWorkArea = entity0Al1.URL_WORK_AREA
			
                ,TcsAmbienteUsuarioAcessoList = 
	                        (from entity1 in entity0.TCS_USUARIO_ACESSO_LISTA
                                  let entity1Al5 = entity1.TCS_AMBIENTE
                                  let entity1Al1 = entity1.TCS_AMBIENTE1
                                  let entity1Al6 = entity1.TCS_USUARIO_AUTENTICACAO
                                  let entity1Al2 = entity1.TCS_AMBIENTE1.TCS_APLICACAO
                                  let entity1Al4 = entity1.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO
                                  let entity1Al3 = entity1.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO
	                        
	                        	
	                        select new TcsAmbienteUsuarioAcesso()
	                        {
	                        
                                DescricaoAmbienteRelacionado = entity1Al1.DESCRICAO_AMBIENTE
                                , DescricaoAplicacaoAmbienteRelacionado = entity1Al2.DESCRICAO_APLICACAO
                                , IdLinx = entity1Al3.ID_LINX
                                , IdLinxAmbienteRelacionado = entity1Al4.ID_LINX
                                , IdTcsAmbiente = entity1Al5.ID_TCS_AMBIENTE
                                , IdTcsAmbienteRelacionado = entity1Al1.ID_TCS_AMBIENTE
                                , IdTcsUsuarioAcesso = entity1.ID_TCS_USUARIO_ACESSO
                                , IdUsuario = entity1Al6.ID_USUARIO
                                , IndicaAdministrador = entity1.INDICA_ADMINISTRADOR
                                , IndicaMultiGpecon = entity1.INDICA_MULTI_GPECON
                                , NomeEmpresa = entity1Al3.NOME_EMPRESA
                                , NomeEmpresaAmbienteRelacionado = entity1Al4.NOME_EMPRESA
                                , UidUsuario = entity1Al6.UID_USUARIO
                                , NomeAutenticacao = entity1Al6.NOME_AUTENTICACAO
                                , NomeUsuario = entity1Al6.NOME_USUARIO
		
	                        }
	                        )
			
                ,TcsAmbienteConexaoList = 
	                        (from entity1 in entity0.TCS_AMBIENTE_CONEXAO_LISTA
                                  let entity1Al4 = entity1.TCS_AMBIENTE
                                  let entity1Al1 = entity1.TCS_BANCO_SERVIDOR
                                  let entity1Al5 = entity1.TCS_APLICATIVO_CONEXAO
                                  let entity1Al2 = entity1.TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB
                                  let entity1Al3 = entity1.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
	                        
	                        	
	                        select new TcsAmbienteConexao()
	                        {
	                        
                                DescricaoBancoServidor = entity1Al1.DESCRICAO_BANCO_SERVIDOR
                                , IdConexaoDb = entity1Al2.ID_CONEXAO_DB
                                , IdLinx = entity1Al3.ID_LINX
                                , IdTcsAmbiente = entity1Al4.ID_TCS_AMBIENTE
                                , IdTcsAmbienteConexao = entity1.ID_TCS_AMBIENTE_CONEXAO
                                , IdTcsAplicativoConexao = entity1Al5.ID_TCS_APLICATIVO_CONEXAO
                                , IdTcsBancoServidor = entity1Al1.ID_TCS_BANCO_SERVIDOR
                                , LxTipoServidor = entity1Al1.LX_TIPO_SERVIDOR
                                , LxTipoServidorName = ((entity1Al1.LX_TIPO_SERVIDOR) == 2 ? "Oracle" : ((entity1Al1.LX_TIPO_SERVIDOR) == 3 ? "SQLite" : ((entity1Al1.LX_TIPO_SERVIDOR) == 1 ? "SQL Server" : "")))
                                , NomeBanco = entity1Al1.NOME_BANCO
                                , NomeConexao = entity1Al2.NOME_CONEXAO
                                , NomeServidor = entity1Al1.NOME_SERVIDOR
                                , StringConexao = entity1Al1.STRING_CONEXAO
		
	                        }
	                        )
			
                ,TcsAmbienteServicoExcecaoList = 
	                        (from entity1 in entity0.TCS_AMBIENTE_SERVICO_EXCECAO_LISTA
                                  let entity1Al2 = entity1.TCS_SERVICO
                                  let entity1Al1 = entity1.TCS_AMBIENTE
	                        
	                        	
	                        select new TcsAmbienteServicoExcecao()
	                        {
	                        
                                IdTcsAmbiente = entity1Al1.ID_TCS_AMBIENTE
                                , IdTcsAmbienteServicoExcecao = entity1.ID_TCS_AMBIENTE_SERVICO_EXCECAO
                                , IdTcsServico = entity1Al2.ID_TCS_SERVICO
                                , NomeServico = entity1Al2.NOME_SERVICO
                                , Url = entity1.URL
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAmbienteUsuarioAcessoByEntitySearch.
	    public IQueryable<TcsAmbienteUsuarioAcesso> GetTcsAmbienteUsuarioAcessoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAmbienteUsuarioAcesso));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAmbienteUsuarioAcesso> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_ACESSO.Where(dynQuery, parameters.ToArray())
                  let entity0Al5 = entity0.TCS_AMBIENTE
                  let entity0Al1 = entity0.TCS_AMBIENTE1
                  let entity0Al6 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_AMBIENTE1.TCS_APLICACAO
                  let entity0Al4 = entity0.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al3 = entity0.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsAmbienteUsuarioAcesso()		
	            {
	            
                DescricaoAmbienteRelacionado = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAplicacaoAmbienteRelacionado = entity0Al2.DESCRICAO_APLICACAO
                , IdLinx = entity0Al3.ID_LINX
                , IdLinxAmbienteRelacionado = entity0Al4.ID_LINX
                , IdTcsAmbiente = entity0Al5.ID_TCS_AMBIENTE
                , IdTcsAmbienteRelacionado = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsUsuarioAcesso = entity0.ID_TCS_USUARIO_ACESSO
                , IdUsuario = entity0Al6.ID_USUARIO
                , IndicaAdministrador = entity0.INDICA_ADMINISTRADOR
                , IndicaMultiGpecon = entity0.INDICA_MULTI_GPECON
                , NomeEmpresa = entity0Al3.NOME_EMPRESA
                , NomeEmpresaAmbienteRelacionado = entity0Al4.NOME_EMPRESA
                , UidUsuario = entity0Al6.UID_USUARIO
                , NomeAutenticacao = entity0Al6.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al6.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAmbienteConexaoByEntitySearch.
	    public IQueryable<TcsAmbienteConexao> GetTcsAmbienteConexaoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAmbienteConexao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAmbienteConexao> result = 
	            (from entity0 in this.DbContext.TCS_AMBIENTE_CONEXAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al4 = entity0.TCS_AMBIENTE
                  let entity0Al1 = entity0.TCS_BANCO_SERVIDOR
                  let entity0Al5 = entity0.TCS_APLICATIVO_CONEXAO
                  let entity0Al2 = entity0.TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB
                  let entity0Al3 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsAmbienteConexao()		
	            {
	            
                DescricaoBancoServidor = entity0Al1.DESCRICAO_BANCO_SERVIDOR
                , IdConexaoDb = entity0Al2.ID_CONEXAO_DB
                , IdLinx = entity0Al3.ID_LINX
                , IdTcsAmbiente = entity0Al4.ID_TCS_AMBIENTE
                , IdTcsAmbienteConexao = entity0.ID_TCS_AMBIENTE_CONEXAO
                , IdTcsAplicativoConexao = entity0Al5.ID_TCS_APLICATIVO_CONEXAO
                , IdTcsBancoServidor = entity0Al1.ID_TCS_BANCO_SERVIDOR
                , LxTipoServidor = entity0Al1.LX_TIPO_SERVIDOR
                , LxTipoServidorName = ((entity0Al1.LX_TIPO_SERVIDOR) == 2 ? "Oracle" : ((entity0Al1.LX_TIPO_SERVIDOR) == 3 ? "SQLite" : ((entity0Al1.LX_TIPO_SERVIDOR) == 1 ? "SQL Server" : "")))
                , NomeBanco = entity0Al1.NOME_BANCO
                , NomeConexao = entity0Al2.NOME_CONEXAO
                , NomeServidor = entity0Al1.NOME_SERVIDOR
                , StringConexao = entity0Al1.STRING_CONEXAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAmbienteServicoExcecaoByEntitySearch.
	    public IQueryable<TcsAmbienteServicoExcecao> GetTcsAmbienteServicoExcecaoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAmbienteServicoExcecao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAmbienteServicoExcecao> result = 
	            (from entity0 in this.DbContext.TCS_AMBIENTE_SERVICO_EXCECAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.TCS_SERVICO
                  let entity0Al1 = entity0.TCS_AMBIENTE
	            
	            	
	            select new TcsAmbienteServicoExcecao()		
	            {
	            
                IdTcsAmbiente = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsAmbienteServicoExcecao = entity0.ID_TCS_AMBIENTE_SERVICO_EXCECAO
                , IdTcsServico = entity0Al2.ID_TCS_SERVICO
                , NomeServico = entity0Al2.NOME_SERVICO
                , Url = entity0.URL
		
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
                  let entity0Al3 = entity0.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_APLICACAO.TCS_APLICATIVO
	            
	            	
	            select new TcsAmbiente()		
	            {
	            
                DescricaoAmbiente = entity0.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al1.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0Al2.DESCRICAO_APLICATIVO
                , EmDesenvolvimento = entity0Al1.EM_DESENVOLVIMENTO
                , IdAplicacao = entity0Al1.ID_APLICACAO
                , IdLinx = entity0Al3.ID_LINX
                , IdTcsAmbiente = entity0.ID_TCS_AMBIENTE
                , IdTcsAplicativo = entity0Al2.ID_TCS_APLICATIVO
                , NomeEmpresa = entity0Al3.NOME_EMPRESA
                , UidAplicacao = entity0Al1.UID_APLICACAO
                , UidEmpresa = entity0Al3.UID_EMPRESA
                , Url = entity0Al1.URL
                , UrlWorkArea = entity0Al1.URL_WORK_AREA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAmbienteUsuarioAcessoByEntitySearchNoAssociations.
	    public IQueryable<TcsAmbienteUsuarioAcesso> GetTcsAmbienteUsuarioAcessoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAmbienteUsuarioAcesso));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAmbienteUsuarioAcesso> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_ACESSO.Where(dynQuery, parameters.ToArray())
                  let entity0Al5 = entity0.TCS_AMBIENTE
                  let entity0Al1 = entity0.TCS_AMBIENTE1
                  let entity0Al6 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_AMBIENTE1.TCS_APLICACAO
                  let entity0Al4 = entity0.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al3 = entity0.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsAmbienteUsuarioAcesso()		
	            {
	            
                DescricaoAmbienteRelacionado = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAplicacaoAmbienteRelacionado = entity0Al2.DESCRICAO_APLICACAO
                , IdLinx = entity0Al3.ID_LINX
                , IdLinxAmbienteRelacionado = entity0Al4.ID_LINX
                , IdTcsAmbiente = entity0Al5.ID_TCS_AMBIENTE
                , IdTcsAmbienteRelacionado = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsUsuarioAcesso = entity0.ID_TCS_USUARIO_ACESSO
                , IdUsuario = entity0Al6.ID_USUARIO
                , IndicaAdministrador = entity0.INDICA_ADMINISTRADOR
                , IndicaMultiGpecon = entity0.INDICA_MULTI_GPECON
                , NomeEmpresa = entity0Al3.NOME_EMPRESA
                , NomeEmpresaAmbienteRelacionado = entity0Al4.NOME_EMPRESA
                , UidUsuario = entity0Al6.UID_USUARIO
                , NomeAutenticacao = entity0Al6.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al6.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAmbienteConexaoByEntitySearchNoAssociations.
	    public IQueryable<TcsAmbienteConexao> GetTcsAmbienteConexaoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAmbienteConexao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAmbienteConexao> result = 
	            (from entity0 in this.DbContext.TCS_AMBIENTE_CONEXAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al4 = entity0.TCS_AMBIENTE
                  let entity0Al1 = entity0.TCS_BANCO_SERVIDOR
                  let entity0Al5 = entity0.TCS_APLICATIVO_CONEXAO
                  let entity0Al2 = entity0.TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB
                  let entity0Al3 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsAmbienteConexao()		
	            {
	            
                DescricaoBancoServidor = entity0Al1.DESCRICAO_BANCO_SERVIDOR
                , IdConexaoDb = entity0Al2.ID_CONEXAO_DB
                , IdLinx = entity0Al3.ID_LINX
                , IdTcsAmbiente = entity0Al4.ID_TCS_AMBIENTE
                , IdTcsAmbienteConexao = entity0.ID_TCS_AMBIENTE_CONEXAO
                , IdTcsAplicativoConexao = entity0Al5.ID_TCS_APLICATIVO_CONEXAO
                , IdTcsBancoServidor = entity0Al1.ID_TCS_BANCO_SERVIDOR
                , LxTipoServidor = entity0Al1.LX_TIPO_SERVIDOR
                , LxTipoServidorName = ((entity0Al1.LX_TIPO_SERVIDOR) == 2 ? "Oracle" : ((entity0Al1.LX_TIPO_SERVIDOR) == 3 ? "SQLite" : ((entity0Al1.LX_TIPO_SERVIDOR) == 1 ? "SQL Server" : "")))
                , NomeBanco = entity0Al1.NOME_BANCO
                , NomeConexao = entity0Al2.NOME_CONEXAO
                , NomeServidor = entity0Al1.NOME_SERVIDOR
                , StringConexao = entity0Al1.STRING_CONEXAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAmbienteServicoExcecaoByEntitySearchNoAssociations.
	    public IQueryable<TcsAmbienteServicoExcecao> GetTcsAmbienteServicoExcecaoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAmbienteServicoExcecao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAmbienteServicoExcecao> result = 
	            (from entity0 in this.DbContext.TCS_AMBIENTE_SERVICO_EXCECAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.TCS_SERVICO
                  let entity0Al1 = entity0.TCS_AMBIENTE
	            
	            	
	            select new TcsAmbienteServicoExcecao()		
	            {
	            
                IdTcsAmbiente = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsAmbienteServicoExcecao = entity0.ID_TCS_AMBIENTE_SERVICO_EXCECAO
                , IdTcsServico = entity0Al2.ID_TCS_SERVICO
                , NomeServico = entity0Al2.NOME_SERVICO
                , Url = entity0.URL
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAmbienteUsuarioAcessoParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsAmbienteUsuarioAcessoParentComposition> GetTcsAmbienteUsuarioAcessoParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "TCS_AMBIENTE", "TCS_USUARIO_ACESSO", "TCS_AMBIENTE", typeof(TcsAmbienteUsuarioAcessoParentComposition), typeof(TcsAmbienteConexao), typeof(TcsAmbienteServicoExcecao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAmbienteUsuarioAcessoParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_ACESSO.Where(dynQuery, parameters.ToArray())
                  let entity0Al5 = entity0.TCS_AMBIENTE
                  let entity0Al1 = entity0.TCS_AMBIENTE1
                  let entity0Al6 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_AMBIENTE1.TCS_APLICACAO
                  let entity0Al4 = entity0.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al3 = entity0.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsAmbienteUsuarioAcessoParentComposition()		
	            {
	            
                DescricaoAmbienteRelacionado = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAplicacaoAmbienteRelacionado = entity0Al2.DESCRICAO_APLICACAO
                , IdLinx = entity0Al3.ID_LINX
                , IdLinxAmbienteRelacionado = entity0Al4.ID_LINX
                , IdTcsAmbiente = entity0Al5.ID_TCS_AMBIENTE
                , IdTcsAmbienteRelacionado = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsUsuarioAcesso = entity0.ID_TCS_USUARIO_ACESSO
                , IdUsuario = entity0Al6.ID_USUARIO
                , IndicaAdministrador = entity0.INDICA_ADMINISTRADOR
                , IndicaMultiGpecon = entity0.INDICA_MULTI_GPECON
                , NomeEmpresa = entity0Al3.NOME_EMPRESA
                , NomeEmpresaAmbienteRelacionado = entity0Al4.NOME_EMPRESA
                , UidUsuario = entity0Al6.UID_USUARIO
                , NomeAutenticacao = entity0Al6.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al6.NOME_USUARIO
                //TcsAmbiente Properties.
                , DescricaoAmbiente = entity0.TCS_AMBIENTE.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0.TCS_AMBIENTE.TCS_APLICACAO.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO
                , EmDesenvolvimento = entity0.TCS_AMBIENTE.TCS_APLICACAO.EM_DESENVOLVIMENTO
                , IdAplicacao = entity0.TCS_AMBIENTE.TCS_APLICACAO.ID_APLICACAO
                , IdTcsAplicativo = entity0.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO
                , UidAplicacao = entity0.TCS_AMBIENTE.TCS_APLICACAO.UID_APLICACAO
                , UidEmpresa = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA
                , Url = entity0.TCS_AMBIENTE.TCS_APLICACAO.URL
                , UrlWorkArea = entity0.TCS_AMBIENTE.TCS_APLICACAO.URL_WORK_AREA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAmbienteConexaoParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsAmbienteConexaoParentComposition> GetTcsAmbienteConexaoParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "TCS_AMBIENTE", "TCS_AMBIENTE_CONEXAO", "TCS_AMBIENTE", typeof(TcsAmbienteConexaoParentComposition), typeof(TcsAmbienteUsuarioAcesso), typeof(TcsAmbienteServicoExcecao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAmbienteConexaoParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_AMBIENTE_CONEXAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al4 = entity0.TCS_AMBIENTE
                  let entity0Al1 = entity0.TCS_BANCO_SERVIDOR
                  let entity0Al5 = entity0.TCS_APLICATIVO_CONEXAO
                  let entity0Al2 = entity0.TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB
                  let entity0Al3 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsAmbienteConexaoParentComposition()		
	            {
	            
                DescricaoBancoServidor = entity0Al1.DESCRICAO_BANCO_SERVIDOR
                , IdConexaoDb = entity0Al2.ID_CONEXAO_DB
                , IdLinx = entity0Al3.ID_LINX
                , IdTcsAmbiente = entity0Al4.ID_TCS_AMBIENTE
                , IdTcsAmbienteConexao = entity0.ID_TCS_AMBIENTE_CONEXAO
                , IdTcsAplicativoConexao = entity0Al5.ID_TCS_APLICATIVO_CONEXAO
                , IdTcsBancoServidor = entity0Al1.ID_TCS_BANCO_SERVIDOR
                , LxTipoServidor = entity0Al1.LX_TIPO_SERVIDOR
                , LxTipoServidorName = ((entity0Al1.LX_TIPO_SERVIDOR) == 2 ? "Oracle" : ((entity0Al1.LX_TIPO_SERVIDOR) == 3 ? "SQLite" : ((entity0Al1.LX_TIPO_SERVIDOR) == 1 ? "SQL Server" : "")))
                , NomeBanco = entity0Al1.NOME_BANCO
                , NomeConexao = entity0Al2.NOME_CONEXAO
                , NomeServidor = entity0Al1.NOME_SERVIDOR
                , StringConexao = entity0Al1.STRING_CONEXAO
                //TcsAmbiente Properties.
                , DescricaoAmbiente = entity0.TCS_AMBIENTE.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0.TCS_AMBIENTE.TCS_APLICACAO.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO
                , EmDesenvolvimento = entity0.TCS_AMBIENTE.TCS_APLICACAO.EM_DESENVOLVIMENTO
                , IdAplicacao = entity0.TCS_AMBIENTE.TCS_APLICACAO.ID_APLICACAO
                , IdTcsAplicativo = entity0.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO
                , NomeEmpresa = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA
                , UidAplicacao = entity0.TCS_AMBIENTE.TCS_APLICACAO.UID_APLICACAO
                , UidEmpresa = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA
                , Url = entity0.TCS_AMBIENTE.TCS_APLICACAO.URL
                , UrlWorkArea = entity0.TCS_AMBIENTE.TCS_APLICACAO.URL_WORK_AREA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAmbienteServicoExcecaoParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsAmbienteServicoExcecaoParentComposition> GetTcsAmbienteServicoExcecaoParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "TCS_AMBIENTE", "TCS_AMBIENTE_SERVICO_EXCECAO", "TCS_AMBIENTE", typeof(TcsAmbienteServicoExcecaoParentComposition), typeof(TcsAmbienteUsuarioAcesso), typeof(TcsAmbienteConexao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAmbienteServicoExcecaoParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_AMBIENTE_SERVICO_EXCECAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.TCS_SERVICO
                  let entity0Al1 = entity0.TCS_AMBIENTE
	            
	            	
	            select new TcsAmbienteServicoExcecaoParentComposition()		
	            {
	            
                IdTcsAmbiente = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsAmbienteServicoExcecao = entity0.ID_TCS_AMBIENTE_SERVICO_EXCECAO
                , IdTcsServico = entity0Al2.ID_TCS_SERVICO
                , NomeServico = entity0Al2.NOME_SERVICO
                , Url = entity0.URL
                //TcsAmbiente Properties.
                , DescricaoAmbiente = entity0.TCS_AMBIENTE.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0.TCS_AMBIENTE.TCS_APLICACAO.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO
                , EmDesenvolvimento = entity0.TCS_AMBIENTE.TCS_APLICACAO.EM_DESENVOLVIMENTO
                , IdAplicacao = entity0.TCS_AMBIENTE.TCS_APLICACAO.ID_APLICACAO
                , IdLinx = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX
                , IdTcsAplicativo = entity0.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO
                , NomeEmpresa = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA
                , UidAplicacao = entity0.TCS_AMBIENTE.TCS_APLICACAO.UID_APLICACAO
                , UidEmpresa = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA
                , UrlWorkArea = entity0.TCS_AMBIENTE.TCS_APLICACAO.URL_WORK_AREA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsServicoByEntitySearch.
	    public IQueryable<TcsServico> GetTcsServicoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsServico));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsServico> result = 
	            (from entity0 in this.DbContext.TCS_SERVICO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsServico()		
	            {
	            
                IdTcsServico = entity0.ID_TCS_SERVICO
                , NomeServico = entity0.NOME_SERVICO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsServicoByEntitySearchNoAssociations.
	    public IQueryable<TcsServico> GetTcsServicoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsServico));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsServico> result = 
	            (from entity0 in this.DbContext.TCS_SERVICO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsServico()		
	            {
	            
                IdTcsServico = entity0.ID_TCS_SERVICO
                , NomeServico = entity0.NOME_SERVICO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAmbienteRelacionadoByEntitySearch.
	    public IQueryable<TcsAmbienteRelacionado> GetTcsAmbienteRelacionadoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAmbienteRelacionado));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAmbienteRelacionado> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_ACESSO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_AMBIENTE
                  let entity0Al5 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_AMBIENTE.TCS_APLICACAO
                  let entity0Al4 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al3 = entity0.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
	            
	            	
	            select new TcsAmbienteRelacionado()		
	            {
	            
                DescricaoAmbienteRelacionado = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAplicacaoAmbienteRelacionado = entity0Al2.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0Al3.DESCRICAO_APLICATIVO
                , IdAplicacao = entity0Al2.ID_APLICACAO
                , IdLinxAmbienteRelacionado = entity0Al4.ID_LINX
                , IdTcsAmbienteRelacionado = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsAplicativo = entity0Al3.ID_TCS_APLICATIVO
                , IdTcsUsuarioAcesso = entity0.ID_TCS_USUARIO_ACESSO
                , IdUsuario = entity0Al5.ID_USUARIO
                , NomeEmpresaAmbienteRelacionado = entity0Al4.NOME_EMPRESA
                , NomeAutenticacao = entity0Al5.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al5.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAmbienteRelacionadoByEntitySearchNoAssociations.
	    public IQueryable<TcsAmbienteRelacionado> GetTcsAmbienteRelacionadoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAmbienteRelacionado));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAmbienteRelacionado> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_ACESSO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_AMBIENTE
                  let entity0Al5 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_AMBIENTE.TCS_APLICACAO
                  let entity0Al4 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al3 = entity0.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
	            
	            	
	            select new TcsAmbienteRelacionado()		
	            {
	            
                DescricaoAmbienteRelacionado = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAplicacaoAmbienteRelacionado = entity0Al2.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0Al3.DESCRICAO_APLICATIVO
                , IdAplicacao = entity0Al2.ID_APLICACAO
                , IdLinxAmbienteRelacionado = entity0Al4.ID_LINX
                , IdTcsAmbienteRelacionado = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsAplicativo = entity0Al3.ID_TCS_APLICATIVO
                , IdTcsUsuarioAcesso = entity0.ID_TCS_USUARIO_ACESSO
                , IdUsuario = entity0Al5.ID_USUARIO
                , NomeEmpresaAmbienteRelacionado = entity0Al4.NOME_EMPRESA
                , NomeAutenticacao = entity0Al5.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al5.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get ServicoExcecaoInfoByEntitySearch.
	    public IEnumerable<ServicoExcecaoInfo> GetServicoExcecaoInfoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<ServicoExcecaoInfo> result = new List<ServicoExcecaoInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get ServicoExcecaoInfoByEntitySearchNoAssociations.
	    public IEnumerable<ServicoExcecaoInfo> GetServicoExcecaoInfoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<ServicoExcecaoInfo> result = new List<ServicoExcecaoInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get AmbienteServicoInfoByEntitySearch.
	    public IEnumerable<AmbienteServicoInfo> GetAmbienteServicoInfoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<AmbienteServicoInfo> result = new List<AmbienteServicoInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get AmbienteServicoInfoByEntitySearchNoAssociations.
	    public IEnumerable<AmbienteServicoInfo> GetAmbienteServicoInfoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<AmbienteServicoInfo> result = new List<AmbienteServicoInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get EnvironmentInfoByEntitySearch.
	    public IEnumerable<EnvironmentInfo> GetEnvironmentInfoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<EnvironmentInfo> result = new List<EnvironmentInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get EnvironmentInfoByEntitySearchNoAssociations.
	    public IEnumerable<EnvironmentInfo> GetEnvironmentInfoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<EnvironmentInfo> result = new List<EnvironmentInfo>();
	  	
	
	        	

	
	        return result;
	    }
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    
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
                  let entity0Al3 = entity0.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_APLICACAO.TCS_APLICATIVO
                orderby entity0.ID_TCS_AMBIENTE ascending
	            
	            	
	            select new TcsAmbiente()		
	            {
	            
                DescricaoAmbiente = entity0.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al1.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0Al2.DESCRICAO_APLICATIVO
                , EmDesenvolvimento = entity0Al1.EM_DESENVOLVIMENTO
                , IdAplicacao = entity0Al1.ID_APLICACAO
                , IdLinx = entity0Al3.ID_LINX
                , IdTcsAmbiente = entity0.ID_TCS_AMBIENTE
                , IdTcsAplicativo = entity0Al2.ID_TCS_APLICATIVO
                , NomeEmpresa = entity0Al3.NOME_EMPRESA
                , UidAplicacao = entity0Al1.UID_APLICACAO
                , UidEmpresa = entity0Al3.UID_EMPRESA
                , Url = entity0Al1.URL
                , UrlWorkArea = entity0Al1.URL_WORK_AREA
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsAmbienteUsuarioAcesso.
	    public IQueryable<TcsAmbienteUsuarioAcesso> GetPagedTcsAmbienteUsuarioAcesso(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAmbienteUsuarioAcesso));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAmbienteUsuarioAcesso> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_ACESSO.Where(dynQuery, parameters.ToArray())
                  let entity0Al5 = entity0.TCS_AMBIENTE
                  let entity0Al1 = entity0.TCS_AMBIENTE1
                  let entity0Al6 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_AMBIENTE1.TCS_APLICACAO
                  let entity0Al4 = entity0.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al3 = entity0.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO
                orderby entity0.ID_TCS_USUARIO_ACESSO ascending
	            
	            	
	            select new TcsAmbienteUsuarioAcesso()		
	            {
	            
                DescricaoAmbienteRelacionado = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAplicacaoAmbienteRelacionado = entity0Al2.DESCRICAO_APLICACAO
                , IdLinx = entity0Al3.ID_LINX
                , IdLinxAmbienteRelacionado = entity0Al4.ID_LINX
                , IdTcsAmbiente = entity0Al5.ID_TCS_AMBIENTE
                , IdTcsAmbienteRelacionado = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsUsuarioAcesso = entity0.ID_TCS_USUARIO_ACESSO
                , IdUsuario = entity0Al6.ID_USUARIO
                , IndicaAdministrador = entity0.INDICA_ADMINISTRADOR
                , IndicaMultiGpecon = entity0.INDICA_MULTI_GPECON
                , NomeEmpresa = entity0Al3.NOME_EMPRESA
                , NomeEmpresaAmbienteRelacionado = entity0Al4.NOME_EMPRESA
                , UidUsuario = entity0Al6.UID_USUARIO
                , NomeAutenticacao = entity0Al6.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al6.NOME_USUARIO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsAmbienteConexao.
	    public IQueryable<TcsAmbienteConexao> GetPagedTcsAmbienteConexao(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAmbienteConexao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAmbienteConexao> result = 
	            (from entity0 in this.DbContext.TCS_AMBIENTE_CONEXAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al4 = entity0.TCS_AMBIENTE
                  let entity0Al1 = entity0.TCS_BANCO_SERVIDOR
                  let entity0Al5 = entity0.TCS_APLICATIVO_CONEXAO
                  let entity0Al2 = entity0.TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB
                  let entity0Al3 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                orderby entity0.ID_TCS_AMBIENTE_CONEXAO ascending
	            
	            	
	            select new TcsAmbienteConexao()		
	            {
	            
                DescricaoBancoServidor = entity0Al1.DESCRICAO_BANCO_SERVIDOR
                , IdConexaoDb = entity0Al2.ID_CONEXAO_DB
                , IdLinx = entity0Al3.ID_LINX
                , IdTcsAmbiente = entity0Al4.ID_TCS_AMBIENTE
                , IdTcsAmbienteConexao = entity0.ID_TCS_AMBIENTE_CONEXAO
                , IdTcsAplicativoConexao = entity0Al5.ID_TCS_APLICATIVO_CONEXAO
                , IdTcsBancoServidor = entity0Al1.ID_TCS_BANCO_SERVIDOR
                , LxTipoServidor = entity0Al1.LX_TIPO_SERVIDOR
                , LxTipoServidorName = ((entity0Al1.LX_TIPO_SERVIDOR) == 2 ? "Oracle" : ((entity0Al1.LX_TIPO_SERVIDOR) == 3 ? "SQLite" : ((entity0Al1.LX_TIPO_SERVIDOR) == 1 ? "SQL Server" : "")))
                , NomeBanco = entity0Al1.NOME_BANCO
                , NomeConexao = entity0Al2.NOME_CONEXAO
                , NomeServidor = entity0Al1.NOME_SERVIDOR
                , StringConexao = entity0Al1.STRING_CONEXAO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsAmbienteServicoExcecao.
	    public IQueryable<TcsAmbienteServicoExcecao> GetPagedTcsAmbienteServicoExcecao(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAmbienteServicoExcecao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAmbienteServicoExcecao> result = 
	            (from entity0 in this.DbContext.TCS_AMBIENTE_SERVICO_EXCECAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.TCS_SERVICO
                  let entity0Al1 = entity0.TCS_AMBIENTE
                orderby entity0.ID_TCS_AMBIENTE_SERVICO_EXCECAO ascending
	            
	            	
	            select new TcsAmbienteServicoExcecao()		
	            {
	            
                IdTcsAmbiente = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsAmbienteServicoExcecao = entity0.ID_TCS_AMBIENTE_SERVICO_EXCECAO
                , IdTcsServico = entity0Al2.ID_TCS_SERVICO
                , NomeServico = entity0Al2.NOME_SERVICO
                , Url = entity0.URL
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
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
                  let entityAl3 = entity.TCS_EMPRESA_AUTENTICACAO
                  let entityAl2 = entity.TCS_APLICACAO.TCS_APLICATIVO
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsAmbienteUsuarioAcessoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAmbienteUsuarioAcesso));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_USUARIO_ACESSO.Where(dynQuery, parameters.ToArray())
                  let entityAl5 = entity.TCS_AMBIENTE
                  let entityAl1 = entity.TCS_AMBIENTE1
                  let entityAl6 = entity.TCS_USUARIO_AUTENTICACAO
                  let entityAl2 = entity.TCS_AMBIENTE1.TCS_APLICACAO
                  let entityAl4 = entity.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO
                  let entityAl3 = entity.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsAmbienteConexaoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAmbienteConexao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_AMBIENTE_CONEXAO.Where(dynQuery, parameters.ToArray())
                  let entityAl4 = entity.TCS_AMBIENTE
                  let entityAl1 = entity.TCS_BANCO_SERVIDOR
                  let entityAl5 = entity.TCS_APLICATIVO_CONEXAO
                  let entityAl2 = entity.TCS_APLICATIVO_CONEXAO.TCS_CONEXAO_DB
                  let entityAl3 = entity.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsAmbienteServicoExcecaoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAmbienteServicoExcecao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_AMBIENTE_SERVICO_EXCECAO.Where(dynQuery, parameters.ToArray())
                  let entityAl2 = entity.TCS_SERVICO
                  let entityAl1 = entity.TCS_AMBIENTE
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsServico.
	    public IQueryable<TcsServico> GetPagedTcsServico(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsServico));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsServico> result = 
	            (from entity0 in this.DbContext.TCS_SERVICO.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_TCS_SERVICO ascending
	            
	            	
	            select new TcsServico()		
	            {
	            
                IdTcsServico = entity0.ID_TCS_SERVICO
                , NomeServico = entity0.NOME_SERVICO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsServicoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsServico));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_SERVICO.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsAmbienteRelacionado.
	    public IQueryable<TcsAmbienteRelacionado> GetPagedTcsAmbienteRelacionado(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAmbienteRelacionado));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAmbienteRelacionado> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_ACESSO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_AMBIENTE
                  let entity0Al5 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_AMBIENTE.TCS_APLICACAO
                  let entity0Al4 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al3 = entity0.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
                orderby entity0.ID_TCS_USUARIO_ACESSO ascending
	            
	            	
	            select new TcsAmbienteRelacionado()		
	            {
	            
                DescricaoAmbienteRelacionado = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAplicacaoAmbienteRelacionado = entity0Al2.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0Al3.DESCRICAO_APLICATIVO
                , IdAplicacao = entity0Al2.ID_APLICACAO
                , IdLinxAmbienteRelacionado = entity0Al4.ID_LINX
                , IdTcsAmbienteRelacionado = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsAplicativo = entity0Al3.ID_TCS_APLICATIVO
                , IdTcsUsuarioAcesso = entity0.ID_TCS_USUARIO_ACESSO
                , IdUsuario = entity0Al5.ID_USUARIO
                , NomeEmpresaAmbienteRelacionado = entity0Al4.NOME_EMPRESA
                , NomeAutenticacao = entity0Al5.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al5.NOME_USUARIO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsAmbienteRelacionadoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAmbienteRelacionado));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_USUARIO_ACESSO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_AMBIENTE
                  let entityAl5 = entity.TCS_USUARIO_AUTENTICACAO
                  let entityAl2 = entity.TCS_AMBIENTE.TCS_APLICACAO
                  let entityAl4 = entity.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entityAl3 = entity.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedServicoExcecaoInfo.
	    public IEnumerable<ServicoExcecaoInfo> GetPagedServicoExcecaoInfo(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<ServicoExcecaoInfo> result = new List<ServicoExcecaoInfo>();
	  	
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetServicoExcecaoInfoCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
			
	
	    
	    [Ignore]
	    //Get PagedAmbienteServicoInfo.
	    public IEnumerable<AmbienteServicoInfo> GetPagedAmbienteServicoInfo(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<AmbienteServicoInfo> result = new List<AmbienteServicoInfo>();
	  	
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetAmbienteServicoInfoCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
			
	
	    
	    [Ignore]
	    //Get PagedEnvironmentInfo.
	    public IEnumerable<EnvironmentInfo> GetPagedEnvironmentInfo(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<EnvironmentInfo> result = new List<EnvironmentInfo>();
	  	
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetEnvironmentInfoCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    
	    [Update()]	
	    //Update TcsAmbiente.
	    public void UpdateTcsAmbiente(TcsAmbiente entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsAmbiente.
	    public void InsertTcsAmbiente(TcsAmbiente entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsAmbiente.
	    public void DeleteTcsAmbiente(TcsAmbiente entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsAmbienteUsuarioAcesso.
	    public void UpdateTcsAmbienteUsuarioAcesso(TcsAmbienteUsuarioAcesso entity)
	    {



	
	        if (entity.TcsAmbiente.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsAmbiente) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsAmbiente); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsAmbienteUsuarioAcesso.
	    public void InsertTcsAmbienteUsuarioAcesso(TcsAmbienteUsuarioAcesso entity)
	    {



	
	        if (entity.TcsAmbiente.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsAmbiente) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsAmbiente);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsAmbienteUsuarioAcesso.
	    public void DeleteTcsAmbienteUsuarioAcesso(TcsAmbienteUsuarioAcesso entity)
	    {



	
	        if (entity.TcsAmbiente.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsAmbiente) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsAmbiente);
	            

	
	        }

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsAmbienteConexao.
	    public void UpdateTcsAmbienteConexao(TcsAmbienteConexao entity)
	    {



	
	        if (entity.TcsAmbiente.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsAmbiente) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsAmbiente); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsAmbienteConexao.
	    public void InsertTcsAmbienteConexao(TcsAmbienteConexao entity)
	    {



	
	        if (entity.TcsAmbiente.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsAmbiente) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsAmbiente);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsAmbienteConexao.
	    public void DeleteTcsAmbienteConexao(TcsAmbienteConexao entity)
	    {



	
	        if (entity.TcsAmbiente.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsAmbiente) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsAmbiente);
	            

	
	        }

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsAmbienteServicoExcecao.
	    public void UpdateTcsAmbienteServicoExcecao(TcsAmbienteServicoExcecao entity)
	    {



	
	        if (entity.TcsAmbiente.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsAmbiente) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsAmbiente); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsAmbienteServicoExcecao.
	    public void InsertTcsAmbienteServicoExcecao(TcsAmbienteServicoExcecao entity)
	    {



	
	        if (entity.TcsAmbiente.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsAmbiente) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsAmbiente);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsAmbienteServicoExcecao.
	    public void DeleteTcsAmbienteServicoExcecao(TcsAmbienteServicoExcecao entity)
	    {



	
	        if (entity.TcsAmbiente.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsAmbiente) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsAmbiente);
	            

	
	        }

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsServico.
	    public void UpdateTcsServico(TcsServico entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsServico.
	    public void InsertTcsServico(TcsServico entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsServico.
	    public void DeleteTcsServico(TcsServico entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsAmbienteRelacionado.
	    public void UpdateTcsAmbienteRelacionado(TcsAmbienteRelacionado entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsAmbienteRelacionado.
	    public void InsertTcsAmbienteRelacionado(TcsAmbienteRelacionado entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsAmbienteRelacionado.
	    public void DeleteTcsAmbienteRelacionado(TcsAmbienteRelacionado entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update ServicoExcecaoInfo.
	    public void UpdateServicoExcecaoInfo(ServicoExcecaoInfo entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert ServicoExcecaoInfo.
	    public void InsertServicoExcecaoInfo(ServicoExcecaoInfo entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete ServicoExcecaoInfo.
	    public void DeleteServicoExcecaoInfo(ServicoExcecaoInfo entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update AmbienteServicoInfo.
	    public void UpdateAmbienteServicoInfo(AmbienteServicoInfo entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert AmbienteServicoInfo.
	    public void InsertAmbienteServicoInfo(AmbienteServicoInfo entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete AmbienteServicoInfo.
	    public void DeleteAmbienteServicoInfo(AmbienteServicoInfo entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update EnvironmentInfo.
	    public void UpdateEnvironmentInfo(EnvironmentInfo entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert EnvironmentInfo.
	    public void InsertEnvironmentInfo(EnvironmentInfo entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete EnvironmentInfo.
	    public void DeleteEnvironmentInfo(EnvironmentInfo entity)
	    {



	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}