					
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

namespace Linx.Framework.BV.ParametroAutorizacao
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_PARAMETRO_AUTORIZACAO.ID_PARAMETRO", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsParametroAutorizacao,TcsParametroAutorizacao.TcsParametroTabelaSelecaoAutorizacao];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdParametro];ReadOnly[false];Entities[TCS_PARAMETRO_AUTORIZACAO:IdParametro|TCS_APLICATIVO:IdTcsAplicativo|TCS_TABELA_AUTORIZACAO:UidTabela];SubQueryInfo[];EdmEntityName[TCS_PARAMETRO_AUTORIZACAO];EntityRelations[TCS_TABELA_AUTORIZACAO(TCS_TABELA_AUTORIZACAO)#TCS_TRANSACAO_AUTORIZACAO(TCS_TRANSACAO_AUTORIZACAO)#TCS_MODULO_AUTORIZACAO(TCS_MODULO_AUTORIZACAO)#TCS_APLICATIVO(TCS_APLICATIVO)#TCS_OBJETO_AUTORIZACAO(TCS_OBJETO_AUTORIZACAO)#TCS_PARAMETRO_GRUPO_AUTORIZACAO(TCS_PARAMETRO_GRUPO_AUTORIZACAO)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsParametroAutorizacao")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.ParametroAutorizacao.TcsParametroAutorizacao")]
	public partial class TcsParametroAutorizacao : Linx.Data.Entity
	{

	

	    public TcsParametroAutorizacao() : this(true) { }

	    public TcsParametroAutorizacao(bool setDefaults) 
	    {

	        if (setDefaults)
	        {
	        	        IndicaParametroLinx = true;
	        	        LxTipoValidacaoParametro = 8;
	        }	

	    }

			
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.TcsParametroTabelaSelecaoAutorizacaoList != null && this.TcsParametroTabelaSelecaoAutorizacaoList.Count() > 0)
	      {
	         foreach (var entity in this.TcsParametroTabelaSelecaoAutorizacaoList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.TcsParametroTabelaSelecaoAutorizacaoList != null)
	      {
	         foreach (var detail in this.TcsParametroTabelaSelecaoAutorizacaoList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsParametroTabelaSelecaoAutorizacaoList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(ParametroAutorizacaoDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("TcsParametroTabelaSelecaoAutorizacao"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsParametroTabelaSelecaoAutorizacao");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdParametro"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdParametro));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsParametroTabelaSelecaoAutorizacao and all sub-details
	         if (this.TcsParametroTabelaSelecaoAutorizacaoList == null || this.TcsParametroTabelaSelecaoAutorizacaoList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsParametroTabelaSelecaoAutorizacaoList = context.GetPagedTcsParametroTabelaSelecaoAutorizacao(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsParametroTabelaSelecaoAutorizacaoList = (from r in context.GetTcsParametroTabelaSelecaoAutorizacaoByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _TcsParametroTabelaSelecaoAutorizacaoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsParametroTabelaSelecaoAutorizacao && ((TcsParametroTabelaSelecaoAutorizacao)e.Entity).TcsParametroAutorizacao == null && e.Associations == null && e.OriginalAssociations == null && ((TcsParametroTabelaSelecaoAutorizacao)e.Entity).IdParametro == this.IdParametro).ToList();
 	      if (_TcsParametroTabelaSelecaoAutorizacaoElements.Count > 0 && this.TcsParametroTabelaSelecaoAutorizacaoList.Count() == 0)
 	      {
 	          this.TcsParametroTabelaSelecaoAutorizacaoList = _TcsParametroTabelaSelecaoAutorizacaoElements.Select(e => (TcsParametroTabelaSelecaoAutorizacao)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsParametroTabelaSelecaoAutorizacaoElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsParametroTabelaSelecaoAutorizacao)detail.Entity).TcsParametroAutorizacao = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsParametroAutorizacao", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsParametroTabelaSelecaoAutorizacaoList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For ColunaCodValida
	    partial void OnColunaCodValidaChanging(System.String value);
	    partial void OnColunaCodValidaChanged();

	    private System.String _ColunaCodValida;

	    [DataMember(Name = "ColunaCodValida", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Coluna Cod Valida", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PARAMETRO_AUTORIZACAO.COLUNA_COD_VALIDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.COLUNA_COD_VALIDA")]
	    public System.String ColunaCodValida
	    {
	    	    get
	    	    {
	    	          return _ColunaCodValida;
	    	    }
	    	    set
	    	    {
	    	          if (this._ColunaCodValida != value)
	    	          {
	    	              this.ValidateProperty("ColunaCodValida", value);
	    	              this.OnColunaCodValidaChanging(value);
	    	              this.RaiseDataMemberChanging("ColunaCodValida");
	    	              this._ColunaCodValida = value;
	    	              this.RaiseDataMemberChanged("ColunaCodValida");
	    	              this.OnColunaCodValidaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ColunaDescValida
	    partial void OnColunaDescValidaChanging(System.String value);
	    partial void OnColunaDescValidaChanged();

	    private System.String _ColunaDescValida;

	    [DataMember(Name = "ColunaDescValida", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Coluna Desc Valida", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PARAMETRO_AUTORIZACAO.COLUNA_DESC_VALIDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.COLUNA_DESC_VALIDA")]
	    public System.String ColunaDescValida
	    {
	    	    get
	    	    {
	    	          return _ColunaDescValida;
	    	    }
	    	    set
	    	    {
	    	          if (this._ColunaDescValida != value)
	    	          {
	    	              this.ValidateProperty("ColunaDescValida", value);
	    	              this.OnColunaDescValidaChanging(value);
	    	              this.RaiseDataMemberChanging("ColunaDescValida");
	    	              this._ColunaDescValida = value;
	    	              this.RaiseDataMemberChanged("ColunaDescValida");
	    	              this.OnColunaDescValidaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescGrupoParametro
	    partial void OnDescGrupoParametroChanging(System.String value);
	    partial void OnDescGrupoParametroChanged();

	    private System.String _DescGrupoParametro;

	    [DataMember(IsRequired = true, Name = "DescGrupoParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Grupo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsParametroGrupoAutorizacao];LookUpTitle[Seleção de (Grupo)];LookUpQuery[executeLookUpTcsParametroGrupoAutorizacao];LookUpFinalize[finalizeLookUpTcsParametroGrupoAutorizacao];LookUpDisplayColumns[{\"DescGrupoParametro\" : \"Descrição Grupo\", \"IdGrupoParametro\" : \"Id Grupo\"}];LookUpColumns[{\"DescGrupoParametro\" : true, \"IdGrupoParametro\" : false}];FilterDataKey[TCS_PARAMETRO_AUTORIZACAO.TCS_PARAMETRO_GRUPO_AUTORIZACAO.DESC_GRUPO_PARAMETRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescGrupoParametro#false##60:0##Descrição Grupo#0#true##::LookUpTcsParametroGrupoAutorizacao##false#false#TCS_PARAMETRO_GRUPO_AUTORIZACAO#TCS_PARAMETRO_GRUPO_AUTORIZACAO#Linx.Framework.BV.ParametroAutorizacao#IQueryable###true#false", EdmKey="TCS_PARAMETRO_AUTORIZACAO.TCS_PARAMETRO_GRUPO_AUTORIZACAO.DESC_GRUPO_PARAMETRO")]
	    public System.String DescGrupoParametro
	    {
	    	    get
	    	    {
	    	          return _DescGrupoParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescGrupoParametro != value)
	    	          {
	    	              this.ValidateProperty("DescGrupoParametro", value);
	    	              this.OnDescGrupoParametroChanging(value);
	    	              this.RaiseDataMemberChanging("DescGrupoParametro");
	    	              this._DescGrupoParametro = value;
	    	              this.RaiseDataMemberChanged("DescGrupoParametro");
	    	              this.OnDescGrupoParametroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescParametro
	    partial void OnDescParametroChanging(System.String value);
	    partial void OnDescParametroChanged();

	    private System.String _DescParametro;

	    [DataMember(IsRequired = true, Name = "DescParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PARAMETRO_AUTORIZACAO.DESC_PARAMETRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.DESC_PARAMETRO")]
	    public System.String DescParametro
	    {
	    	    get
	    	    {
	    	          return _DescParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescParametro != value)
	    	          {
	    	              this.ValidateProperty("DescParametro", value);
	    	              this.OnDescParametroChanging(value);
	    	              this.RaiseDataMemberChanging("DescParametro");
	    	              this._DescParametro = value;
	    	              this.RaiseDataMemberChanged("DescParametro");
	    	              this.OnDescParametroChanged();
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
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[IdTcsAplicativo];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicativo];LookUpTitle[Seleção de (Aplicativo)];LookUpQuery[executeLookUpTcsAplicativo];LookUpFinalize[finalizeLookUpTcsAplicativo];LookUpDisplayColumns[{\"IdTcsAplicativo\" : \"Aplicativo\", \"DescricaoAplicativo\" : \"Descrição\"}];LookUpColumns[{\"IdTcsAplicativo\" : true, \"DescricaoAplicativo\" : true}];FilterDataKey[TCS_PARAMETRO_AUTORIZACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoAplicativo#false##250:0##Descrição#1#true##::LookUpTcsAplicativo##false#false#TCS_APLICATIVO#TCS_APLICATIVO#Linx.Framework.BV.ParametroAutorizacao#IQueryable###true#false", EdmKey="TCS_PARAMETRO_AUTORIZACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO")]
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
	    //Extensibility Partial Method Definitions For DescTabela
	    partial void OnDescTabelaChanging(System.String value);
	    partial void OnDescTabelaChanged();

	    private System.String _DescTabela;

	    [DataMember(Name = "DescTabela", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [LinxStringLength(80)]
	    [FunctionalPoint("Precision[80:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PARAMETRO_AUTORIZACAO.TCS_TABELA_AUTORIZACAO.DESC_TABELA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.TCS_TABELA_AUTORIZACAO.DESC_TABELA")]
	    public System.String DescTabela
	    {
	    	    get
	    	    {
	    	          return _DescTabela;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescTabela != value)
	    	          {
	    	              this.ValidateProperty("DescTabela", value);
	    	              this.OnDescTabelaChanging(value);
	    	              this.RaiseDataMemberChanging("DescTabela");
	    	              this._DescTabela = value;
	    	              this.RaiseDataMemberChanged("DescTabela");
	    	              this.OnDescTabelaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For FaixaFinal
	    partial void OnFaixaFinalChanging(System.String value);
	    partial void OnFaixaFinalChanged();

	    private System.String _FaixaFinal;

	    [DataMember(Name = "FaixaFinal", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Faixa Final", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(70)]
	    [FunctionalPoint("Precision[70:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PARAMETRO_AUTORIZACAO.FAIXA_FINAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.FAIXA_FINAL")]
	    public System.String FaixaFinal
	    {
	    	    get
	    	    {
	    	          return _FaixaFinal;
	    	    }
	    	    set
	    	    {
	    	          if (this._FaixaFinal != value)
	    	          {
	    	              this.ValidateProperty("FaixaFinal", value);
	    	              this.OnFaixaFinalChanging(value);
	    	              this.RaiseDataMemberChanging("FaixaFinal");
	    	              this._FaixaFinal = value;
	    	              this.RaiseDataMemberChanged("FaixaFinal");
	    	              this.OnFaixaFinalChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For FaixaInicial
	    partial void OnFaixaInicialChanging(System.String value);
	    partial void OnFaixaInicialChanged();

	    private System.String _FaixaInicial;

	    [DataMember(Name = "FaixaInicial", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Faixa Inicial", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(70)]
	    [FunctionalPoint("Precision[70:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PARAMETRO_AUTORIZACAO.FAIXA_INICIAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.FAIXA_INICIAL")]
	    public System.String FaixaInicial
	    {
	    	    get
	    	    {
	    	          return _FaixaInicial;
	    	    }
	    	    set
	    	    {
	    	          if (this._FaixaInicial != value)
	    	          {
	    	              this.ValidateProperty("FaixaInicial", value);
	    	              this.OnFaixaInicialChanging(value);
	    	              this.RaiseDataMemberChanging("FaixaInicial");
	    	              this._FaixaInicial = value;
	    	              this.RaiseDataMemberChanged("FaixaInicial");
	    	              this.OnFaixaInicialChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdGrupoParametro
	    partial void OnIdGrupoParametroChanging(Int16 value);
	    partial void OnIdGrupoParametroChanged();

	    private Int16 _IdGrupoParametro;

	    [DataMember(IsRequired = true, Name = "IdGrupoParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Grupo Parametro", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsParametroGrupoAutorizacao];LookUpTitle[Seleção de (Id Grupo Parametro)];LookUpQuery[executeLookUpTcsParametroGrupoAutorizacao];LookUpFinalize[finalizeLookUpTcsParametroGrupoAutorizacao];LookUpDisplayColumns[{\"DescGrupoParametro\" : \"Descrição Grupo\", \"IdGrupoParametro\" : \"Id Grupo\"}];LookUpColumns[{\"DescGrupoParametro\" : true, \"IdGrupoParametro\" : false}];FilterDataKey[TCS_PARAMETRO_AUTORIZACAO.TCS_PARAMETRO_GRUPO_AUTORIZACAO.ID_GRUPO_PARAMETRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int16#IdGrupoParametro#true##6:0##Id Grupo#1#false##::LookUpTcsParametroGrupoAutorizacao##false#false#TCS_PARAMETRO_GRUPO_AUTORIZACAO#TCS_PARAMETRO_GRUPO_AUTORIZACAO#Linx.Framework.BV.ParametroAutorizacao#IQueryable###true#false", EdmKey="TCS_PARAMETRO_AUTORIZACAO.TCS_PARAMETRO_GRUPO_AUTORIZACAO.ID_GRUPO_PARAMETRO")]
	    public Int16 IdGrupoParametro
	    {
	    	    get
	    	    {
	    	          return _IdGrupoParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdGrupoParametro != value)
	    	          {
	    	              this.ValidateProperty("IdGrupoParametro", value);
	    	              this.OnIdGrupoParametroChanging(value);
	    	              this.RaiseDataMemberChanging("IdGrupoParametro");
	    	              this._IdGrupoParametro = value;
	    	              this.RaiseDataMemberChanged("IdGrupoParametro");
	    	              this.OnIdGrupoParametroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdParametro
	    partial void OnIdParametroChanging(Int64 value);
	    partial void OnIdParametroChanged();

	    private Int64 _IdParametro;

	    [DataMember(IsRequired = true, Name = "IdParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Parametro", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PARAMETRO_AUTORIZACAO.ID_PARAMETRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.ID_PARAMETRO")]
	    public Int64 IdParametro
	    {
	    	    get
	    	    {
	    	          return _IdParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdParametro != value)
	    	          {
	    	              this.ValidateProperty("IdParametro", value);
	    	              this.OnIdParametroChanging(value);
	    	              this.RaiseDataMemberChanging("IdParametro");
	    	              this._IdParametro = value;
	    	              this.RaiseDataMemberChanged("IdParametro");
	    	              this.OnIdParametroChanged();
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
	    [Display(Name = "Aplicativo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicativo];LookUpTitle[Seleção de (Aplicativo)];LookUpQuery[executeLookUpTcsAplicativo];LookUpFinalize[finalizeLookUpTcsAplicativo];LookUpDisplayColumns[{\"IdTcsAplicativo\" : \"Aplicativo\", \"DescricaoAplicativo\" : \"Descrição\"}];LookUpColumns[{\"IdTcsAplicativo\" : true, \"DescricaoAplicativo\" : true}];FilterDataKey[TCS_PARAMETRO_AUTORIZACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdTcsAplicativo#true##12:0##Aplicativo#0#true##::LookUpTcsAplicativo##false#false#TCS_APLICATIVO#TCS_APLICATIVO#Linx.Framework.BV.ParametroAutorizacao#IQueryable###true#false", EdmKey="TCS_PARAMETRO_AUTORIZACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO")]
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
	    //Extensibility Partial Method Definitions For IndicaEnviaPdv
	    partial void OnIndicaEnviaPdvChanging(Boolean value);
	    partial void OnIndicaEnviaPdvChanged();

	    private Boolean _IndicaEnviaPdv;

	    [DataMember(IsRequired = true, Name = "IndicaEnviaPdv", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Envia PDV", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PARAMETRO_AUTORIZACAO.INDICA_ENVIA_PDV];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.INDICA_ENVIA_PDV")]
	    public Boolean IndicaEnviaPdv
	    {
	    	    get
	    	    {
	    	          return _IndicaEnviaPdv;
	    	    }
	    	    set
	    	    {
	    	          if (this._IndicaEnviaPdv != value)
	    	          {
	    	              this.ValidateProperty("IndicaEnviaPdv", value);
	    	              this.OnIndicaEnviaPdvChanging(value);
	    	              this.RaiseDataMemberChanging("IndicaEnviaPdv");
	    	              this._IndicaEnviaPdv = value;
	    	              this.RaiseDataMemberChanged("IndicaEnviaPdv");
	    	              this.OnIndicaEnviaPdvChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IndicaParametroLinx
	    partial void OnIndicaParametroLinxChanging(Boolean value);
	    partial void OnIndicaParametroLinxChanged();

	    private Boolean _IndicaParametroLinx;

	    [DataMember(IsRequired = true, Name = "IndicaParametroLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Parâmetro Linx", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[true];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[true];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="true")]
	    public Boolean IndicaParametroLinx
	    {
	    	    get
	    	    {
	    	          return _IndicaParametroLinx;
	    	    }
	    	    set
	    	    {
	    	          if (this._IndicaParametroLinx != value)
	    	          {
	    	              this.ValidateProperty("IndicaParametroLinx", value);
	    	              this.OnIndicaParametroLinxChanging(value);
	    	              this.RaiseDataMemberChanging("IndicaParametroLinx");
	    	              this._IndicaParametroLinx = value;
	    	              this.RaiseDataMemberChanged("IndicaParametroLinx");
	    	              this.OnIndicaParametroLinxChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxDatatypeParametro
	    partial void OnLxDatatypeParametroChanging(Byte value);
	    partial void OnLxDatatypeParametroChanged();

	    private Byte _LxDatatypeParametro;

	    [DataMember(IsRequired = true, Name = "LxDatatypeParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo do Dado", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoValorParametro];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PARAMETRO_AUTORIZACAO.LX_DATATYPE_PARAMETRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.LX_DATATYPE_PARAMETRO")]
	    public Byte LxDatatypeParametro
	    {
	    	    get
	    	    {
	    	          return _LxDatatypeParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxDatatypeParametro != value)
	    	          {
	    	              this.ValidateProperty("LxDatatypeParametro", value);
	    	              this.OnLxDatatypeParametroChanging(value);
	    	              this.RaiseDataMemberChanging("LxDatatypeParametro");
	    	              this._LxDatatypeParametro = value;
	    	              this.RaiseDataMemberChanged("LxDatatypeParametro");
	    	              this.OnLxDatatypeParametroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoValidacaoParametro
	    partial void OnLxTipoValidacaoParametroChanging(Byte value);
	    partial void OnLxTipoValidacaoParametroChanged();

	    private Byte _LxTipoValidacaoParametro;

	    [DataMember(IsRequired = true, Name = "LxTipoValidacaoParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo Validação", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoValidacaoParametro];KpiName[];KpiRelatedAttribute[];DefaultValue[8];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PARAMETRO_AUTORIZACAO.LX_TIPO_VALIDACAO_PARAMETRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.LX_TIPO_VALIDACAO_PARAMETRO")]
	    public Byte LxTipoValidacaoParametro
	    {
	    	    get
	    	    {
	    	          return _LxTipoValidacaoParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoValidacaoParametro != value)
	    	          {
	    	              this.ValidateProperty("LxTipoValidacaoParametro", value);
	    	              this.OnLxTipoValidacaoParametroChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoValidacaoParametro");
	    	              this._LxTipoValidacaoParametro = value;
	    	              this.RaiseDataMemberChanged("LxTipoValidacaoParametro");
	    	              this.OnLxTipoValidacaoParametroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NivelAcesso
	    partial void OnNivelAcessoChanging(Byte value);
	    partial void OnNivelAcessoChanged();

	    private Byte _NivelAcesso;

	    [DataMember(IsRequired = true, Name = "NivelAcesso", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nível Acesso Visualização", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PARAMETRO_AUTORIZACAO.NIVEL_ACESSO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.NIVEL_ACESSO")]
	    public Byte NivelAcesso
	    {
	    	    get
	    	    {
	    	          return _NivelAcesso;
	    	    }
	    	    set
	    	    {
	    	          if (this._NivelAcesso != value)
	    	          {
	    	              this.ValidateProperty("NivelAcesso", value);
	    	              this.OnNivelAcessoChanging(value);
	    	              this.RaiseDataMemberChanging("NivelAcesso");
	    	              this._NivelAcesso = value;
	    	              this.RaiseDataMemberChanged("NivelAcesso");
	    	              this.OnNivelAcessoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NivelAcessoEdicao
	    partial void OnNivelAcessoEdicaoChanging(Byte value);
	    partial void OnNivelAcessoEdicaoChanged();

	    private Byte _NivelAcessoEdicao;

	    [DataMember(IsRequired = true, Name = "NivelAcessoEdicao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nível Acesso Edição", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PARAMETRO_AUTORIZACAO.NIVEL_ACESSO_EDICAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.NIVEL_ACESSO_EDICAO")]
	    public Byte NivelAcessoEdicao
	    {
	    	    get
	    	    {
	    	          return _NivelAcessoEdicao;
	    	    }
	    	    set
	    	    {
	    	          if (this._NivelAcessoEdicao != value)
	    	          {
	    	              this.ValidateProperty("NivelAcessoEdicao", value);
	    	              this.OnNivelAcessoEdicaoChanging(value);
	    	              this.RaiseDataMemberChanging("NivelAcessoEdicao");
	    	              this._NivelAcessoEdicao = value;
	    	              this.RaiseDataMemberChanged("NivelAcessoEdicao");
	    	              this.OnNivelAcessoEdicaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ObsParametro
	    partial void OnObsParametroChanging(System.String value);
	    partial void OnObsParametroChanged();

	    private System.String _ObsParametro;

	    [DataMember(Name = "ObsParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Obs", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PARAMETRO_AUTORIZACAO.OBS_PARAMETRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.OBS_PARAMETRO")]
	    public System.String ObsParametro
	    {
	    	    get
	    	    {
	    	          return _ObsParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._ObsParametro != value)
	    	          {
	    	              this.ValidateProperty("ObsParametro", value);
	    	              this.OnObsParametroChanging(value);
	    	              this.RaiseDataMemberChanging("ObsParametro");
	    	              this._ObsParametro = value;
	    	              this.RaiseDataMemberChanged("ObsParametro");
	    	              this.OnObsParametroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For PermiteVariacaoPorEntidade
	    partial void OnPermiteVariacaoPorEntidadeChanging(Boolean value);
	    partial void OnPermiteVariacaoPorEntidadeChanged();

	    private Boolean _PermiteVariacaoPorEntidade;

	    [DataMember(IsRequired = true, Name = "PermiteVariacaoPorEntidade", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Permite Variação por Entidade", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PARAMETRO_AUTORIZACAO.PERMITE_VARIACAO_POR_ENTIDADE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.PERMITE_VARIACAO_POR_ENTIDADE")]
	    public Boolean PermiteVariacaoPorEntidade
	    {
	    	    get
	    	    {
	    	          return _PermiteVariacaoPorEntidade;
	    	    }
	    	    set
	    	    {
	    	          if (this._PermiteVariacaoPorEntidade != value)
	    	          {
	    	              this.ValidateProperty("PermiteVariacaoPorEntidade", value);
	    	              this.OnPermiteVariacaoPorEntidadeChanging(value);
	    	              this.RaiseDataMemberChanging("PermiteVariacaoPorEntidade");
	    	              this._PermiteVariacaoPorEntidade = value;
	    	              this.RaiseDataMemberChanged("PermiteVariacaoPorEntidade");
	    	              this.OnPermiteVariacaoPorEntidadeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For TituloParametro
	    partial void OnTituloParametroChanging(System.String value);
	    partial void OnTituloParametroChanged();

	    private System.String _TituloParametro;

	    [DataMember(IsRequired = true, Name = "TituloParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Título", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PARAMETRO_AUTORIZACAO.TITULO_PARAMETRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.TITULO_PARAMETRO")]
	    public System.String TituloParametro
	    {
	    	    get
	    	    {
	    	          return _TituloParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._TituloParametro != value)
	    	          {
	    	              this.ValidateProperty("TituloParametro", value);
	    	              this.OnTituloParametroChanging(value);
	    	              this.RaiseDataMemberChanging("TituloParametro");
	    	              this._TituloParametro = value;
	    	              this.RaiseDataMemberChanged("TituloParametro");
	    	              this.OnTituloParametroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidTabela
	    partial void OnUidTabelaChanging(System.Nullable<System.Guid> value);
	    partial void OnUidTabelaChanged();

	    private System.Nullable<System.Guid> _UidTabela;

	    [DataMember(Name = "UidTabela", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Tabela", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PARAMETRO_AUTORIZACAO.TCS_TABELA_AUTORIZACAO.UID_TABELA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.TCS_TABELA_AUTORIZACAO.UID_TABELA")]
	    public System.Nullable<System.Guid> UidTabela
	    {
	    	    get
	    	    {
	    	          return _UidTabela;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidTabela != value)
	    	          {
	    	              this.ValidateProperty("UidTabela", value);
	    	              this.OnUidTabelaChanging(value);
	    	              this.RaiseDataMemberChanging("UidTabela");
	    	              this._UidTabela = value;
	    	              this.RaiseDataMemberChanged("UidTabela");
	    	              this.OnUidTabelaChanged();
	    	          }
	    	    }
	    }

	    private Int64 _TemporaryIdParametro;
	    [DataMember(Name = "TemporaryIdParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Parametro (Tmp)", Description="Temporary Key", Order = 6, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdParametro
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdParametro.IsNullOrEmpty())
	    	                this._TemporaryIdParametro = this._IdParametro;
	    	          return this._TemporaryIdParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdParametro != value)
	    	              this._TemporaryIdParametro = value;
	    	    }
	    }	

	    #endregion Data Properties

	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<TcsParametroTabelaSelecaoAutorizacao> _TcsParametroTabelaSelecaoAutorizacaoList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsParametroAutorizacao_TcsParametroTabelaSelecaoAutorizacao", "IdParametro", "IdParametro", IsForeignKey=false)]
	    [DataMember(Name = "TcsParametroTabelaSelecaoAutorizacaoList", EmitDefaultValue = true)]
	    public IEnumerable<TcsParametroTabelaSelecaoAutorizacao> TcsParametroTabelaSelecaoAutorizacaoList
	    {
	        get
	        {
	
	            if (this._TcsParametroTabelaSelecaoAutorizacaoList == null)
	            	this._TcsParametroTabelaSelecaoAutorizacaoList = new List<TcsParametroTabelaSelecaoAutorizacao>();
	
	            return this._TcsParametroTabelaSelecaoAutorizacaoList;
	        }
	        set
	        {
	            if (this._TcsParametroTabelaSelecaoAutorizacaoList != value)
	            {
	                this._TcsParametroTabelaSelecaoAutorizacaoList = value;
	                this.RaisePropertyChanged("TcsParametroTabelaSelecaoAutorizacaoList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_PARAMETRO_AUTORIZACAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_PARAMETRO_AUTORIZACAO), QualifiedEntitySetName = "AutorizacaoContext.TCS_PARAMETRO_AUTORIZACAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PARAMETRO_AUTORIZACAO.FAIXA_FINAL", Source = "FaixaFinal", Target = "FAIXA_FINAL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_PARAMETRO_AUTORIZACAO", RelationPropertyName = "TCS_PARAMETRO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PARAMETRO_AUTORIZACAO.ID_PARAMETRO", Source = "IdParametro", Target = "ID_PARAMETRO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_PARAMETRO_AUTORIZACAO", RelationPropertyName = "TCS_PARAMETRO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PARAMETRO_AUTORIZACAO.NIVEL_ACESSO", Source = "NivelAcesso", Target = "NIVEL_ACESSO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_PARAMETRO_AUTORIZACAO", RelationPropertyName = "TCS_PARAMETRO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PARAMETRO_AUTORIZACAO.FAIXA_INICIAL", Source = "FaixaInicial", Target = "FAIXA_INICIAL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_PARAMETRO_AUTORIZACAO", RelationPropertyName = "TCS_PARAMETRO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PARAMETRO_AUTORIZACAO.OBS_PARAMETRO", Source = "ObsParametro", Target = "OBS_PARAMETRO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_PARAMETRO_AUTORIZACAO", RelationPropertyName = "TCS_PARAMETRO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PARAMETRO_AUTORIZACAO.DESC_PARAMETRO", Source = "DescParametro", Target = "DESC_PARAMETRO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_PARAMETRO_AUTORIZACAO", RelationPropertyName = "TCS_PARAMETRO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PARAMETRO_AUTORIZACAO.INDICA_ENVIA_PDV", Source = "IndicaEnviaPdv", Target = "INDICA_ENVIA_PDV", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_PARAMETRO_AUTORIZACAO", RelationPropertyName = "TCS_PARAMETRO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PARAMETRO_AUTORIZACAO.TITULO_PARAMETRO", Source = "TituloParametro", Target = "TITULO_PARAMETRO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_PARAMETRO_AUTORIZACAO", RelationPropertyName = "TCS_PARAMETRO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PARAMETRO_AUTORIZACAO.COLUNA_COD_VALIDA", Source = "ColunaCodValida", Target = "COLUNA_COD_VALIDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_PARAMETRO_AUTORIZACAO", RelationPropertyName = "TCS_PARAMETRO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PARAMETRO_AUTORIZACAO.COLUNA_DESC_VALIDA", Source = "ColunaDescValida", Target = "COLUNA_DESC_VALIDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_PARAMETRO_AUTORIZACAO", RelationPropertyName = "TCS_PARAMETRO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PARAMETRO_AUTORIZACAO.NIVEL_ACESSO_EDICAO", Source = "NivelAcessoEdicao", Target = "NIVEL_ACESSO_EDICAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_PARAMETRO_AUTORIZACAO", RelationPropertyName = "TCS_PARAMETRO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PARAMETRO_AUTORIZACAO.LX_DATATYPE_PARAMETRO", Source = "LxDatatypeParametro", Target = "LX_DATATYPE_PARAMETRO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_PARAMETRO_AUTORIZACAO", RelationPropertyName = "TCS_PARAMETRO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PARAMETRO_AUTORIZACAO.LX_TIPO_VALIDACAO_PARAMETRO", Source = "LxTipoValidacaoParametro", Target = "LX_TIPO_VALIDACAO_PARAMETRO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_PARAMETRO_AUTORIZACAO", RelationPropertyName = "TCS_PARAMETRO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PARAMETRO_AUTORIZACAO.PERMITE_VARIACAO_POR_ENTIDADE", Source = "PermiteVariacaoPorEntidade", Target = "PERMITE_VARIACAO_POR_ENTIDADE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_PARAMETRO_AUTORIZACAO", RelationPropertyName = "TCS_PARAMETRO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PARAMETRO_AUTORIZACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO", Source = "IdTcsAplicativo", Target = "ID_TCS_APLICATIVO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICATIVO", RelationPropertyName = "TCS_APLICATIVO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PARAMETRO_AUTORIZACAO.TCS_TABELA_AUTORIZACAO.UID_TABELA", Source = "UidTabela", Target = "UID_TABELA", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_TABELA_AUTORIZACAO", RelationPropertyName = "TCS_TABELA_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PARAMETRO_AUTORIZACAO.TCS_PARAMETRO_GRUPO_AUTORIZACAO.ID_GRUPO_PARAMETRO", Source = "IdGrupoParametro", Target = "ID_GRUPO_PARAMETRO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_PARAMETRO_GRUPO_AUTORIZACAO", RelationPropertyName = "TCS_PARAMETRO_GRUPO_AUTORIZACAO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxDatatypeParametroValues()
	    {
	    	    return Linx.Framework.BV.Domains.TipoValorParametro.GetValues();
	    }
	    private string _lxDatatypeParametroName;
	    [DataMember(IsRequired = false, Name = "LxDatatypeParametroName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo do Dado", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxDatatypeParametroName
	    {
	    	    get { if (this.LxDatatypeParametro.IsNull()) { _lxDatatypeParametroName = String.Empty; } else { string key = this.LxDatatypeParametro.ToString(); var dmValues = this.GetLxDatatypeParametroValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxDatatypeParametroName) _lxDatatypeParametroName = domainName; } return _lxDatatypeParametroName; } set { _lxDatatypeParametroName = value;  }
	    }
	    public Dictionary<string, string> GetLxTipoValidacaoParametroValues()
	    {
	    	    return Linx.Framework.BV.Domains.TipoValidacaoParametro.GetValues();
	    }
	    private string _lxTipoValidacaoParametroName;
	    [DataMember(IsRequired = false, Name = "LxTipoValidacaoParametroName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo Validação", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxTipoValidacaoParametroName
	    {
	    	    get { if (this.LxTipoValidacaoParametro.IsNull()) { _lxTipoValidacaoParametroName = String.Empty; } else { string key = this.LxTipoValidacaoParametro.ToString(); var dmValues = this.GetLxTipoValidacaoParametroValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxTipoValidacaoParametroName) _lxTipoValidacaoParametroName = domainName; } return _lxTipoValidacaoParametroName; } set { _lxTipoValidacaoParametroName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.ID_TABELA_SELECAO", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Variação];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTabelaSelecao];ReadOnly[false];Entities[TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO:IdTabelaSelecao|TCS_TABELA_AUTORIZACAO:UidTabela];SubQueryInfo[Select 1 From #ParentAlias#.TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO_LISTA as #Alias#];EdmEntityName[TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO];EntityRelations[TCS_PARAMETRO_AUTORIZACAO(TCS_PARAMETRO_AUTORIZACAO)#TCS_TABELA_AUTORIZACAO(TCS_TABELA_AUTORIZACAO)#TCS_TRANSACAO_AUTORIZACAO(TCS_TRANSACAO_AUTORIZACAO)#TCS_MODULO_AUTORIZACAO(TCS_MODULO_AUTORIZACAO)#TCS_OBJETO_AUTORIZACAO(TCS_OBJETO_AUTORIZACAO)#TCS_PARAMETRO_GRUPO_AUTORIZACAO(TCS_PARAMETRO_GRUPO_AUTORIZACAO)];EdmParentEntityName[TCS_PARAMETRO_AUTORIZACAO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsParametroTabelaSelecaoAutorizacao")]
	[Serializable()]
	public partial class TcsParametroTabelaSelecaoAutorizacao : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(ParametroAutorizacaoDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsParametroAutorizacao");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdParametro"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdParametro));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsParametroAutorizacao
	         this.TcsParametroAutorizacao = (from r in context.GetTcsParametroAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For DescTabela
	    partial void OnDescTabelaChanging(System.String value);
	    partial void OnDescTabelaChanged();

	    private System.String _DescTabela;

	    [DataMember(IsRequired = true, Name = "DescTabela", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(80)]
	    [FunctionalPoint("Precision[80:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsTabelaAutorizacaoSelecao];LookUpTitle[Seleção de (Descrição)];LookUpQuery[executeLookUpTcsTabelaAutorizacaoSelecao];LookUpFinalize[finalizeLookUpTcsTabelaAutorizacaoSelecao];LookUpDisplayColumns[{\"NomeTabela\" : \"Nome Tabela\", \"DescTabela\" : \"Descrição\", \"UidTabela\" : \"Uid Tabela\"}];LookUpColumns[{\"NomeTabela\" : true, \"DescTabela\" : true, \"UidTabela\" : false}];FilterDataKey[TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.TCS_TABELA_AUTORIZACAO.DESC_TABELA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescTabela#false##80:0##Descrição#1#true##::LookUpTcsTabelaAutorizacaoSelecao##false#false#TCS_TABELA_AUTORIZACAO#TCS_TABELA_AUTORIZACAO#Linx.Framework.BV.ParametroAutorizacao#IQueryable###true#false", EdmKey="TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.TCS_TABELA_AUTORIZACAO.DESC_TABELA")]
	    public System.String DescTabela
	    {
	    	    get
	    	    {
	    	          return _DescTabela;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescTabela != value)
	    	          {
	    	              this.ValidateProperty("DescTabela", value);
	    	              this.OnDescTabelaChanging(value);
	    	              this.RaiseDataMemberChanging("DescTabela");
	    	              this._DescTabela = value;
	    	              this.RaiseDataMemberChanged("DescTabela");
	    	              this.OnDescTabelaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdParametro
	    partial void OnIdParametroChanging(Int64 value);
	    partial void OnIdParametroChanged();

	    private Int64 _IdParametro;

	    [DataMember(IsRequired = true, Name = "IdParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Parametro", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.TCS_PARAMETRO_AUTORIZACAO.ID_PARAMETRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.TCS_PARAMETRO_AUTORIZACAO.ID_PARAMETRO")]
	    public Int64 IdParametro
	    {
	    	    get
	    	    {
	    	          return _IdParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdParametro != value)
	    	          {
	    	              this.ValidateProperty("IdParametro", value);
	    	              this.OnIdParametroChanging(value);
	    	              this.RaiseDataMemberChanging("IdParametro");
	    	              this._IdParametro = value;
	    	              this.RaiseDataMemberChanged("IdParametro");
	    	              this.OnIdParametroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTabelaSelecao
	    partial void OnIdTabelaSelecaoChanging(Int64 value);
	    partial void OnIdTabelaSelecaoChanged();

	    private Int64 _IdTabelaSelecao;

	    [DataMember(IsRequired = true, Name = "IdTabelaSelecao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tabela Selecao", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.ID_TABELA_SELECAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.ID_TABELA_SELECAO")]
	    public Int64 IdTabelaSelecao
	    {
	    	    get
	    	    {
	    	          return _IdTabelaSelecao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTabelaSelecao != value)
	    	          {
	    	              this.ValidateProperty("IdTabelaSelecao", value);
	    	              this.OnIdTabelaSelecaoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTabelaSelecao");
	    	              this._IdTabelaSelecao = value;
	    	              this.RaiseDataMemberChanged("IdTabelaSelecao");
	    	              this.OnIdTabelaSelecaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxParametroHierarquia
	    partial void OnLxParametroHierarquiaChanging(System.Nullable<System.Byte> value);
	    partial void OnLxParametroHierarquiaChanged();

	    private System.Nullable<System.Byte> _LxParametroHierarquia;

	    [DataMember(Name = "LxParametroHierarquia", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Hierarquia", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[ParametroHierarquia];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.LX_PARAMETRO_HIERARQUIA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.LX_PARAMETRO_HIERARQUIA")]
	    public System.Nullable<System.Byte> LxParametroHierarquia
	    {
	    	    get
	    	    {
	    	          return _LxParametroHierarquia;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxParametroHierarquia != value)
	    	          {
	    	              this.ValidateProperty("LxParametroHierarquia", value);
	    	              this.OnLxParametroHierarquiaChanging(value);
	    	              this.RaiseDataMemberChanging("LxParametroHierarquia");
	    	              this._LxParametroHierarquia = value;
	    	              this.RaiseDataMemberChanged("LxParametroHierarquia");
	    	              this.OnLxParametroHierarquiaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeTabela
	    partial void OnNomeTabelaChanging(System.String value);
	    partial void OnNomeTabelaChanged();

	    private System.String _NomeTabela;

	    [DataMember(IsRequired = true, Name = "NomeTabela", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Tabela", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsTabelaAutorizacaoSelecao];LookUpTitle[Seleção de (Nome Tabela)];LookUpQuery[executeLookUpTcsTabelaAutorizacaoSelecao];LookUpFinalize[finalizeLookUpTcsTabelaAutorizacaoSelecao];LookUpDisplayColumns[{\"NomeTabela\" : \"Nome Tabela\", \"DescTabela\" : \"Descrição\", \"UidTabela\" : \"Uid Tabela\"}];LookUpColumns[{\"NomeTabela\" : true, \"DescTabela\" : true, \"UidTabela\" : false}];FilterDataKey[TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.TCS_TABELA_AUTORIZACAO.NOME_TABELA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeTabela#false##250:0##Nome Tabela#0#true##::LookUpTcsTabelaAutorizacaoSelecao##false#false#TCS_TABELA_AUTORIZACAO#TCS_TABELA_AUTORIZACAO#Linx.Framework.BV.ParametroAutorizacao#IQueryable###true#false", EdmKey="TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.TCS_TABELA_AUTORIZACAO.NOME_TABELA")]
	    public System.String NomeTabela
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
	    //Extensibility Partial Method Definitions For UidTabela
	    partial void OnUidTabelaChanging(System.Guid value);
	    partial void OnUidTabelaChanged();

	    private System.Guid _UidTabela;

	    [DataMember(IsRequired = true, Name = "UidTabela", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Tabela", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsTabelaAutorizacaoSelecao];LookUpTitle[Seleção de (Uid Tabela)];LookUpQuery[executeLookUpTcsTabelaAutorizacaoSelecao];LookUpFinalize[finalizeLookUpTcsTabelaAutorizacaoSelecao];LookUpDisplayColumns[{\"NomeTabela\" : \"Nome Tabela\", \"DescTabela\" : \"Descrição\", \"UidTabela\" : \"Uid Tabela\"}];LookUpColumns[{\"NomeTabela\" : true, \"DescTabela\" : true, \"UidTabela\" : false}];FilterDataKey[TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.TCS_TABELA_AUTORIZACAO.UID_TABELA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Guid#UidTabela#true##12:0##Uid Tabela#2#false##::LookUpTcsTabelaAutorizacaoSelecao##false#false#TCS_TABELA_AUTORIZACAO#TCS_TABELA_AUTORIZACAO#Linx.Framework.BV.ParametroAutorizacao#IQueryable###true#false", EdmKey="TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.TCS_TABELA_AUTORIZACAO.UID_TABELA")]
	    public System.Guid UidTabela
	    {
	    	    get
	    	    {
	    	          return _UidTabela;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidTabela != value)
	    	          {
	    	              this.ValidateProperty("UidTabela", value);
	    	              this.OnUidTabelaChanging(value);
	    	              this.RaiseDataMemberChanging("UidTabela");
	    	              this._UidTabela = value;
	    	              this.RaiseDataMemberChanged("UidTabela");
	    	              this.OnUidTabelaChanged();
	    	          }
	    	    }
	    }

	    private Int64 _TemporaryIdTabelaSelecao;
	    [DataMember(Name = "TemporaryIdTabelaSelecao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tabela Selecao (Tmp)", Description="Temporary Key", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdTabelaSelecao
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTabelaSelecao.IsNullOrEmpty())
	    	                this._TemporaryIdTabelaSelecao = this._IdTabelaSelecao;
	    	          return this._TemporaryIdTabelaSelecao;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTabelaSelecao != value)
	    	              this._TemporaryIdTabelaSelecao = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsParametroAutorizacao _TcsParametroAutorizacao;
	    [DataMember(Name = "TcsParametroAutorizacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsParametroAutorizacao_TcsParametroTabelaSelecaoAutorizacao", "IdParametro", "IdParametro", IsForeignKey=true)]
	    public TcsParametroAutorizacao TcsParametroAutorizacao
	    {
	        get
	        {
	            return this._TcsParametroAutorizacao;
	        }
	        set
	        {
	            if (this._TcsParametroAutorizacao != value)
	            {
	                this._TcsParametroAutorizacao = value;
	                this.RaisePropertyChanged("TcsParametroAutorizacaoList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO), QualifiedEntitySetName = "AutorizacaoContext.TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.ID_TABELA_SELECAO", Source = "IdTabelaSelecao", Target = "ID_TABELA_SELECAO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO", RelationPropertyName = "TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.LX_PARAMETRO_HIERARQUIA", Source = "LxParametroHierarquia", Target = "LX_PARAMETRO_HIERARQUIA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO", RelationPropertyName = "TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.TCS_TABELA_AUTORIZACAO.UID_TABELA", Source = "UidTabela", Target = "UID_TABELA", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_TABELA_AUTORIZACAO", RelationPropertyName = "TCS_TABELA_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.TCS_PARAMETRO_AUTORIZACAO.ID_PARAMETRO", Source = "IdParametro", Target = "ID_PARAMETRO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_PARAMETRO_AUTORIZACAO", RelationPropertyName = "TCS_PARAMETRO_AUTORIZACAO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxParametroHierarquiaValues()
	    {
	    	    return Linx.Framework.BV.Domains.ParametroHierarquia.GetValues();
	    }
	    private string _lxParametroHierarquiaName;
	    [DataMember(IsRequired = false, Name = "LxParametroHierarquiaName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Hierarquia", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxParametroHierarquiaName
	    {
	    	    get { if (this.LxParametroHierarquia.IsNull()) { _lxParametroHierarquiaName = String.Empty; } else { string key = this.LxParametroHierarquia.ToString(); var dmValues = this.GetLxParametroHierarquiaValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxParametroHierarquiaName) _lxParametroHierarquiaName = domainName; } return _lxParametroHierarquiaName; } set { _lxParametroHierarquiaName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_PARAMETRO_GRUPO_AUTORIZACAO.ID_GRUPO_PARAMETRO", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsParametroGrupoAutorizacao,TcsParametroGrupoAutorizacao.TcsParametroAutorizacaoGrupo];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[];EdmEntityName[TCS_PARAMETRO_GRUPO_AUTORIZACAO];EntityRelations[];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsParametroGrupoAutorizacao")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.ParametroAutorizacao.TcsParametroGrupoAutorizacao")]
	public partial class TcsParametroGrupoAutorizacao : Linx.Data.Entity
	{

	
		
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.TcsParametroAutorizacaoGrupoList != null && this.TcsParametroAutorizacaoGrupoList.Count() > 0)
	      {
	         foreach (var entity in this.TcsParametroAutorizacaoGrupoList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.TcsParametroAutorizacaoGrupoList != null)
	      {
	         foreach (var detail in this.TcsParametroAutorizacaoGrupoList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsParametroAutorizacaoGrupoList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(ParametroAutorizacaoDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("TcsParametroAutorizacaoGrupo"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsParametroAutorizacaoGrupo");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdGrupoParametro"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdGrupoParametro));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsParametroAutorizacaoGrupo and all sub-details
	         if (this.TcsParametroAutorizacaoGrupoList == null || this.TcsParametroAutorizacaoGrupoList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsParametroAutorizacaoGrupoList = context.GetPagedTcsParametroAutorizacaoGrupo(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsParametroAutorizacaoGrupoList = (from r in context.GetTcsParametroAutorizacaoGrupoByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _TcsParametroAutorizacaoGrupoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsParametroAutorizacaoGrupo && ((TcsParametroAutorizacaoGrupo)e.Entity).TcsParametroGrupoAutorizacao == null && e.Associations == null && e.OriginalAssociations == null && ((TcsParametroAutorizacaoGrupo)e.Entity).IdGrupoParametro == this.IdGrupoParametro).ToList();
 	      if (_TcsParametroAutorizacaoGrupoElements.Count > 0 && this.TcsParametroAutorizacaoGrupoList.Count() == 0)
 	      {
 	          this.TcsParametroAutorizacaoGrupoList = _TcsParametroAutorizacaoGrupoElements.Select(e => (TcsParametroAutorizacaoGrupo)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsParametroAutorizacaoGrupoElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsParametroAutorizacaoGrupo)detail.Entity).TcsParametroGrupoAutorizacao = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsParametroGrupoAutorizacao", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsParametroAutorizacaoGrupoList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For DescGrupoParametro
	    partial void OnDescGrupoParametroChanging(System.String value);
	    partial void OnDescGrupoParametroChanged();

	    private System.String _DescGrupoParametro;

	    [DataMember(IsRequired = true, Name = "DescGrupoParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PARAMETRO_GRUPO_AUTORIZACAO.DESC_GRUPO_PARAMETRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_GRUPO_AUTORIZACAO.DESC_GRUPO_PARAMETRO")]
	    public System.String DescGrupoParametro
	    {
	    	    get
	    	    {
	    	          return _DescGrupoParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescGrupoParametro != value)
	    	          {
	    	              this.ValidateProperty("DescGrupoParametro", value);
	    	              this.OnDescGrupoParametroChanging(value);
	    	              this.RaiseDataMemberChanging("DescGrupoParametro");
	    	              this._DescGrupoParametro = value;
	    	              this.RaiseDataMemberChanged("DescGrupoParametro");
	    	              this.OnDescGrupoParametroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdGrupoParametro
	    partial void OnIdGrupoParametroChanging(Int16 value);
	    partial void OnIdGrupoParametroChanged();

	    private Int16 _IdGrupoParametro;

	    [DataMember(IsRequired = true, Name = "IdGrupoParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Grupo Parametro", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PARAMETRO_GRUPO_AUTORIZACAO.ID_GRUPO_PARAMETRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_GRUPO_AUTORIZACAO.ID_GRUPO_PARAMETRO")]
	    public Int16 IdGrupoParametro
	    {
	    	    get
	    	    {
	    	          return _IdGrupoParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdGrupoParametro != value)
	    	          {
	    	              this.ValidateProperty("IdGrupoParametro", value);
	    	              this.OnIdGrupoParametroChanging(value);
	    	              this.RaiseDataMemberChanging("IdGrupoParametro");
	    	              this._IdGrupoParametro = value;
	    	              this.RaiseDataMemberChanged("IdGrupoParametro");
	    	              this.OnIdGrupoParametroChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<TcsParametroAutorizacaoGrupo> _TcsParametroAutorizacaoGrupoList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsParametroGrupoAutorizacao_TcsParametroAutorizacaoGrupo", "IdGrupoParametro", "IdGrupoParametro", IsForeignKey=false)]
	    [DataMember(Name = "TcsParametroAutorizacaoGrupoList", EmitDefaultValue = true)]
	    public IEnumerable<TcsParametroAutorizacaoGrupo> TcsParametroAutorizacaoGrupoList
	    {
	        get
	        {
	
	            if (this._TcsParametroAutorizacaoGrupoList == null)
	            	this._TcsParametroAutorizacaoGrupoList = new List<TcsParametroAutorizacaoGrupo>();
	
	            return this._TcsParametroAutorizacaoGrupoList;
	        }
	        set
	        {
	            if (this._TcsParametroAutorizacaoGrupoList != value)
	            {
	                this._TcsParametroAutorizacaoGrupoList = value;
	                this.RaisePropertyChanged("TcsParametroAutorizacaoGrupoList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_PARAMETRO_GRUPO_AUTORIZACAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_PARAMETRO_GRUPO_AUTORIZACAO), QualifiedEntitySetName = "AutorizacaoContext.TCS_PARAMETRO_GRUPO_AUTORIZACAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PARAMETRO_GRUPO_AUTORIZACAO.ID_GRUPO_PARAMETRO", Source = "IdGrupoParametro", Target = "ID_GRUPO_PARAMETRO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_PARAMETRO_GRUPO_AUTORIZACAO", RelationPropertyName = "TCS_PARAMETRO_GRUPO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PARAMETRO_GRUPO_AUTORIZACAO.DESC_GRUPO_PARAMETRO", Source = "DescGrupoParametro", Target = "DESC_GRUPO_PARAMETRO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_PARAMETRO_GRUPO_AUTORIZACAO", RelationPropertyName = "TCS_PARAMETRO_GRUPO_AUTORIZACAO" });

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

		

	[LinxPublicationView(PrimaryKeys="TCS_PARAMETRO_AUTORIZACAO.ID_PARAMETRO", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[TcsParametroAutorizacao];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdParametro];ReadOnly[false];Entities[TCS_PARAMETRO_AUTORIZACAO:IdParametro];SubQueryInfo[Select 1 From #ParentAlias#.TCS_PARAMETRO_AUTORIZACAO_LISTA as #Alias#];EdmEntityName[TCS_PARAMETRO_AUTORIZACAO];EntityRelations[TCS_TABELA_AUTORIZACAO(TCS_TABELA_AUTORIZACAO)#TCS_TRANSACAO_AUTORIZACAO(TCS_TRANSACAO_AUTORIZACAO)#TCS_MODULO_AUTORIZACAO(TCS_MODULO_AUTORIZACAO)#TCS_APLICATIVO(TCS_APLICATIVO)#TCS_OBJETO_AUTORIZACAO(TCS_OBJETO_AUTORIZACAO)#TCS_PARAMETRO_GRUPO_AUTORIZACAO(TCS_PARAMETRO_GRUPO_AUTORIZACAO)];EdmParentEntityName[TCS_PARAMETRO_GRUPO_AUTORIZACAO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsParametroAutorizacaoGrupo")]
	[Serializable()]
	public partial class TcsParametroAutorizacaoGrupo : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(ParametroAutorizacaoDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsParametroGrupoAutorizacao");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdGrupoParametro"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdGrupoParametro));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsParametroGrupoAutorizacao
	         this.TcsParametroGrupoAutorizacao = (from r in context.GetTcsParametroGrupoAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For DescParametro
	    partial void OnDescParametroChanging(System.String value);
	    partial void OnDescParametroChanged();

	    private System.String _DescParametro;

	    [DataMember(IsRequired = true, Name = "DescParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PARAMETRO_AUTORIZACAO.DESC_PARAMETRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.DESC_PARAMETRO")]
	    public System.String DescParametro
	    {
	    	    get
	    	    {
	    	          return _DescParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescParametro != value)
	    	          {
	    	              this.ValidateProperty("DescParametro", value);
	    	              this.OnDescParametroChanging(value);
	    	              this.RaiseDataMemberChanging("DescParametro");
	    	              this._DescParametro = value;
	    	              this.RaiseDataMemberChanged("DescParametro");
	    	              this.OnDescParametroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdGrupoParametro
	    partial void OnIdGrupoParametroChanging(Int16 value);
	    partial void OnIdGrupoParametroChanged();

	    private Int16 _IdGrupoParametro;

	    [DataMember(IsRequired = true, Name = "IdGrupoParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Grupo Parametro", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PARAMETRO_AUTORIZACAO.TCS_PARAMETRO_GRUPO_AUTORIZACAO.ID_GRUPO_PARAMETRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.TCS_PARAMETRO_GRUPO_AUTORIZACAO.ID_GRUPO_PARAMETRO")]
	    public Int16 IdGrupoParametro
	    {
	    	    get
	    	    {
	    	          return _IdGrupoParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdGrupoParametro != value)
	    	          {
	    	              this.ValidateProperty("IdGrupoParametro", value);
	    	              this.OnIdGrupoParametroChanging(value);
	    	              this.RaiseDataMemberChanging("IdGrupoParametro");
	    	              this._IdGrupoParametro = value;
	    	              this.RaiseDataMemberChanged("IdGrupoParametro");
	    	              this.OnIdGrupoParametroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdParametro
	    partial void OnIdParametroChanging(Int64 value);
	    partial void OnIdParametroChanged();

	    private Int64 _IdParametro;

	    [DataMember(IsRequired = true, Name = "IdParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Parametro", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PARAMETRO_AUTORIZACAO.ID_PARAMETRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.ID_PARAMETRO")]
	    public Int64 IdParametro
	    {
	    	    get
	    	    {
	    	          return _IdParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdParametro != value)
	    	          {
	    	              this.ValidateProperty("IdParametro", value);
	    	              this.OnIdParametroChanging(value);
	    	              this.RaiseDataMemberChanging("IdParametro");
	    	              this._IdParametro = value;
	    	              this.RaiseDataMemberChanged("IdParametro");
	    	              this.OnIdParametroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For TituloParametro
	    partial void OnTituloParametroChanging(System.String value);
	    partial void OnTituloParametroChanged();

	    private System.String _TituloParametro;

	    [DataMember(IsRequired = true, Name = "TituloParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Título", Description="", Order = 14, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PARAMETRO_AUTORIZACAO.TITULO_PARAMETRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.TITULO_PARAMETRO")]
	    public System.String TituloParametro
	    {
	    	    get
	    	    {
	    	          return _TituloParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._TituloParametro != value)
	    	          {
	    	              this.ValidateProperty("TituloParametro", value);
	    	              this.OnTituloParametroChanging(value);
	    	              this.RaiseDataMemberChanging("TituloParametro");
	    	              this._TituloParametro = value;
	    	              this.RaiseDataMemberChanged("TituloParametro");
	    	              this.OnTituloParametroChanged();
	    	          }
	    	    }
	    }

	    private Int64 _TemporaryIdParametro;
	    [DataMember(Name = "TemporaryIdParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Parametro (Tmp)", Description="Temporary Key", Order = 6, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdParametro
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdParametro.IsNullOrEmpty())
	    	                this._TemporaryIdParametro = this._IdParametro;
	    	          return this._TemporaryIdParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdParametro != value)
	    	              this._TemporaryIdParametro = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsParametroGrupoAutorizacao _TcsParametroGrupoAutorizacao;
	    [DataMember(Name = "TcsParametroGrupoAutorizacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsParametroGrupoAutorizacao_TcsParametroAutorizacaoGrupo", "IdGrupoParametro", "IdGrupoParametro", IsForeignKey=true)]
	    public TcsParametroGrupoAutorizacao TcsParametroGrupoAutorizacao
	    {
	        get
	        {
	            return this._TcsParametroGrupoAutorizacao;
	        }
	        set
	        {
	            if (this._TcsParametroGrupoAutorizacao != value)
	            {
	                this._TcsParametroGrupoAutorizacao = value;
	                this.RaisePropertyChanged("TcsParametroGrupoAutorizacaoList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_PARAMETRO_AUTORIZACAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_PARAMETRO_AUTORIZACAO), QualifiedEntitySetName = "AutorizacaoContext.TCS_PARAMETRO_AUTORIZACAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PARAMETRO_AUTORIZACAO.ID_PARAMETRO", Source = "IdParametro", Target = "ID_PARAMETRO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_PARAMETRO_AUTORIZACAO", RelationPropertyName = "TCS_PARAMETRO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PARAMETRO_AUTORIZACAO.DESC_PARAMETRO", Source = "DescParametro", Target = "DESC_PARAMETRO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_PARAMETRO_AUTORIZACAO", RelationPropertyName = "TCS_PARAMETRO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PARAMETRO_AUTORIZACAO.TITULO_PARAMETRO", Source = "TituloParametro", Target = "TITULO_PARAMETRO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_PARAMETRO_AUTORIZACAO", RelationPropertyName = "TCS_PARAMETRO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PARAMETRO_AUTORIZACAO.TCS_PARAMETRO_GRUPO_AUTORIZACAO.ID_GRUPO_PARAMETRO", Source = "IdGrupoParametro", Target = "ID_GRUPO_PARAMETRO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_PARAMETRO_GRUPO_AUTORIZACAO", RelationPropertyName = "TCS_PARAMETRO_GRUPO_AUTORIZACAO" });

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

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Variação];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTabelaSelecao];ReadOnly[false];Entities[TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO:IdTabelaSelecao|TCS_TABELA_AUTORIZACAO:UidTabela];SubQueryInfo[Select 1 From #ParentAlias#.TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO_LISTA as #Alias#];EdmEntityName[TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO];EntityRelations[TCS_PARAMETRO_AUTORIZACAO(TCS_PARAMETRO_AUTORIZACAO)#TCS_TABELA_AUTORIZACAO(TCS_TABELA_AUTORIZACAO)#TCS_TRANSACAO_AUTORIZACAO(TCS_TRANSACAO_AUTORIZACAO)#TCS_MODULO_AUTORIZACAO(TCS_MODULO_AUTORIZACAO)#TCS_OBJETO_AUTORIZACAO(TCS_OBJETO_AUTORIZACAO)#TCS_PARAMETRO_GRUPO_AUTORIZACAO(TCS_PARAMETRO_GRUPO_AUTORIZACAO)];EdmParentEntityName[TCS_PARAMETRO_AUTORIZACAO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsParametroTabelaSelecaoAutorizacao")]
	[Serializable()]
	public partial class TcsParametroTabelaSelecaoAutorizacaoParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For DescTabela
	    partial void OnDescTabelaChanging(System.String value);
	    partial void OnDescTabelaChanged();

	    private System.String _DescTabela;

	    [DataMember(IsRequired = true, Name = "DescTabela", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(80)]
	    [FunctionalPoint("Precision[80:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsTabelaAutorizacaoSelecao];LookUpTitle[Seleção de (Descrição)];LookUpQuery[executeLookUpTcsTabelaAutorizacaoSelecao];LookUpFinalize[finalizeLookUpTcsTabelaAutorizacaoSelecao];LookUpDisplayColumns[{\"NomeTabela\" : \"Nome Tabela\", \"DescTabela\" : \"Descrição\", \"UidTabela\" : \"Uid Tabela\"}];LookUpColumns[{\"NomeTabela\" : true, \"DescTabela\" : true, \"UidTabela\" : false}];FilterDataKey[TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.TCS_TABELA_AUTORIZACAO.DESC_TABELA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescTabela#false##80:0##Descrição#1#true##::LookUpTcsTabelaAutorizacaoSelecao##false#false#TCS_TABELA_AUTORIZACAO#TCS_TABELA_AUTORIZACAO#Linx.Framework.BV.ParametroAutorizacao#IQueryable###true#false", EdmKey="TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.TCS_TABELA_AUTORIZACAO.DESC_TABELA")]
	    public System.String DescTabela
	    {
	    	    get
	    	    {
	    	          return _DescTabela;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescTabela != value)
	    	          {
	    	              this.ValidateProperty("DescTabela", value);
	    	              this.OnDescTabelaChanging(value);
	    	              this.RaiseDataMemberChanging("DescTabela");
	    	              this._DescTabela = value;
	    	              this.RaiseDataMemberChanged("DescTabela");
	    	              this.OnDescTabelaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdParametro
	    partial void OnIdParametroChanging(Int64 value);
	    partial void OnIdParametroChanged();

	    private Int64 _IdParametro;

	    [DataMember(IsRequired = true, Name = "IdParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Parametro", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.TCS_PARAMETRO_AUTORIZACAO.ID_PARAMETRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.TCS_PARAMETRO_AUTORIZACAO.ID_PARAMETRO")]
	    public Int64 IdParametro
	    {
	    	    get
	    	    {
	    	          return _IdParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdParametro != value)
	    	          {
	    	              this.ValidateProperty("IdParametro", value);
	    	              this.OnIdParametroChanging(value);
	    	              this.RaiseDataMemberChanging("IdParametro");
	    	              this._IdParametro = value;
	    	              this.RaiseDataMemberChanged("IdParametro");
	    	              this.OnIdParametroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTabelaSelecao
	    partial void OnIdTabelaSelecaoChanging(Int64 value);
	    partial void OnIdTabelaSelecaoChanged();

	    private Int64 _IdTabelaSelecao;

	    [DataMember(IsRequired = true, Name = "IdTabelaSelecao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tabela Selecao", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.ID_TABELA_SELECAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.ID_TABELA_SELECAO")]
	    public Int64 IdTabelaSelecao
	    {
	    	    get
	    	    {
	    	          return _IdTabelaSelecao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTabelaSelecao != value)
	    	          {
	    	              this.ValidateProperty("IdTabelaSelecao", value);
	    	              this.OnIdTabelaSelecaoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTabelaSelecao");
	    	              this._IdTabelaSelecao = value;
	    	              this.RaiseDataMemberChanged("IdTabelaSelecao");
	    	              this.OnIdTabelaSelecaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxParametroHierarquia
	    partial void OnLxParametroHierarquiaChanging(System.Nullable<System.Byte> value);
	    partial void OnLxParametroHierarquiaChanged();

	    private System.Nullable<System.Byte> _LxParametroHierarquia;

	    [DataMember(Name = "LxParametroHierarquia", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Hierarquia", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[ParametroHierarquia];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.LX_PARAMETRO_HIERARQUIA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.LX_PARAMETRO_HIERARQUIA")]
	    public System.Nullable<System.Byte> LxParametroHierarquia
	    {
	    	    get
	    	    {
	    	          return _LxParametroHierarquia;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxParametroHierarquia != value)
	    	          {
	    	              this.ValidateProperty("LxParametroHierarquia", value);
	    	              this.OnLxParametroHierarquiaChanging(value);
	    	              this.RaiseDataMemberChanging("LxParametroHierarquia");
	    	              this._LxParametroHierarquia = value;
	    	              this.RaiseDataMemberChanged("LxParametroHierarquia");
	    	              this.OnLxParametroHierarquiaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeTabela
	    partial void OnNomeTabelaChanging(System.String value);
	    partial void OnNomeTabelaChanged();

	    private System.String _NomeTabela;

	    [DataMember(IsRequired = true, Name = "NomeTabela", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Tabela", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsTabelaAutorizacaoSelecao];LookUpTitle[Seleção de (Nome Tabela)];LookUpQuery[executeLookUpTcsTabelaAutorizacaoSelecao];LookUpFinalize[finalizeLookUpTcsTabelaAutorizacaoSelecao];LookUpDisplayColumns[{\"NomeTabela\" : \"Nome Tabela\", \"DescTabela\" : \"Descrição\", \"UidTabela\" : \"Uid Tabela\"}];LookUpColumns[{\"NomeTabela\" : true, \"DescTabela\" : true, \"UidTabela\" : false}];FilterDataKey[TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.TCS_TABELA_AUTORIZACAO.NOME_TABELA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeTabela#false##250:0##Nome Tabela#0#true##::LookUpTcsTabelaAutorizacaoSelecao##false#false#TCS_TABELA_AUTORIZACAO#TCS_TABELA_AUTORIZACAO#Linx.Framework.BV.ParametroAutorizacao#IQueryable###true#false", EdmKey="TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.TCS_TABELA_AUTORIZACAO.NOME_TABELA")]
	    public System.String NomeTabela
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
	    //Extensibility Partial Method Definitions For UidTabela
	    partial void OnUidTabelaChanging(System.Guid value);
	    partial void OnUidTabelaChanged();

	    private System.Guid _UidTabela;

	    [DataMember(IsRequired = true, Name = "UidTabela", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Tabela", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsTabelaAutorizacaoSelecao];LookUpTitle[Seleção de (Uid Tabela)];LookUpQuery[executeLookUpTcsTabelaAutorizacaoSelecao];LookUpFinalize[finalizeLookUpTcsTabelaAutorizacaoSelecao];LookUpDisplayColumns[{\"NomeTabela\" : \"Nome Tabela\", \"DescTabela\" : \"Descrição\", \"UidTabela\" : \"Uid Tabela\"}];LookUpColumns[{\"NomeTabela\" : true, \"DescTabela\" : true, \"UidTabela\" : false}];FilterDataKey[TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.TCS_TABELA_AUTORIZACAO.UID_TABELA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Guid#UidTabela#true##12:0##Uid Tabela#2#false##::LookUpTcsTabelaAutorizacaoSelecao##false#false#TCS_TABELA_AUTORIZACAO#TCS_TABELA_AUTORIZACAO#Linx.Framework.BV.ParametroAutorizacao#IQueryable###true#false", EdmKey="TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.TCS_TABELA_AUTORIZACAO.UID_TABELA")]
	    public System.Guid UidTabela
	    {
	    	    get
	    	    {
	    	          return _UidTabela;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidTabela != value)
	    	          {
	    	              this.ValidateProperty("UidTabela", value);
	    	              this.OnUidTabelaChanging(value);
	    	              this.RaiseDataMemberChanging("UidTabela");
	    	              this._UidTabela = value;
	    	              this.RaiseDataMemberChanged("UidTabela");
	    	              this.OnUidTabelaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ColunaCodValida
	    partial void OnColunaCodValidaChanging(System.String value);
	    partial void OnColunaCodValidaChanged();

	    private System.String _ColunaCodValida;

	    [DataMember(Name = "ColunaCodValida", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Coluna Cod Valida", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.TCS_PARAMETRO_AUTORIZACAO.COLUNA_COD_VALIDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.COLUNA_COD_VALIDA")]
	    public System.String ColunaCodValida
	    {
	    	    get
	    	    {
	    	          return _ColunaCodValida;
	    	    }
	    	    set
	    	    {
	    	          if (this._ColunaCodValida != value)
	    	          {
	    	              this.ValidateProperty("ColunaCodValida", value);
	    	              this.OnColunaCodValidaChanging(value);
	    	              this.RaiseDataMemberChanging("ColunaCodValida");
	    	              this._ColunaCodValida = value;
	    	              this.RaiseDataMemberChanged("ColunaCodValida");
	    	              this.OnColunaCodValidaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ColunaDescValida
	    partial void OnColunaDescValidaChanging(System.String value);
	    partial void OnColunaDescValidaChanged();

	    private System.String _ColunaDescValida;

	    [DataMember(Name = "ColunaDescValida", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Coluna Desc Valida", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.TCS_PARAMETRO_AUTORIZACAO.COLUNA_DESC_VALIDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.COLUNA_DESC_VALIDA")]
	    public System.String ColunaDescValida
	    {
	    	    get
	    	    {
	    	          return _ColunaDescValida;
	    	    }
	    	    set
	    	    {
	    	          if (this._ColunaDescValida != value)
	    	          {
	    	              this.ValidateProperty("ColunaDescValida", value);
	    	              this.OnColunaDescValidaChanging(value);
	    	              this.RaiseDataMemberChanging("ColunaDescValida");
	    	              this._ColunaDescValida = value;
	    	              this.RaiseDataMemberChanged("ColunaDescValida");
	    	              this.OnColunaDescValidaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescGrupoParametro
	    partial void OnDescGrupoParametroChanging(System.String value);
	    partial void OnDescGrupoParametroChanged();

	    private System.String _DescGrupoParametro;

	    [DataMember(IsRequired = true, Name = "DescGrupoParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Grupo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.TCS_PARAMETRO_AUTORIZACAO.TCS_PARAMETRO_GRUPO_AUTORIZACAO.DESC_GRUPO_PARAMETRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.TCS_PARAMETRO_GRUPO_AUTORIZACAO.DESC_GRUPO_PARAMETRO")]
	    public System.String DescGrupoParametro
	    {
	    	    get
	    	    {
	    	          return _DescGrupoParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescGrupoParametro != value)
	    	          {
	    	              this.ValidateProperty("DescGrupoParametro", value);
	    	              this.OnDescGrupoParametroChanging(value);
	    	              this.RaiseDataMemberChanging("DescGrupoParametro");
	    	              this._DescGrupoParametro = value;
	    	              this.RaiseDataMemberChanged("DescGrupoParametro");
	    	              this.OnDescGrupoParametroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescParametro
	    partial void OnDescParametroChanging(System.String value);
	    partial void OnDescParametroChanged();

	    private System.String _DescParametro;

	    [DataMember(IsRequired = true, Name = "DescParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.TCS_PARAMETRO_AUTORIZACAO.DESC_PARAMETRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.DESC_PARAMETRO")]
	    public System.String DescParametro
	    {
	    	    get
	    	    {
	    	          return _DescParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescParametro != value)
	    	          {
	    	              this.ValidateProperty("DescParametro", value);
	    	              this.OnDescParametroChanging(value);
	    	              this.RaiseDataMemberChanging("DescParametro");
	    	              this._DescParametro = value;
	    	              this.RaiseDataMemberChanged("DescParametro");
	    	              this.OnDescParametroChanged();
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
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[IdTcsAplicativo];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.TCS_PARAMETRO_AUTORIZACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO")]
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
	    //Extensibility Partial Method Definitions For FaixaFinal
	    partial void OnFaixaFinalChanging(System.String value);
	    partial void OnFaixaFinalChanged();

	    private System.String _FaixaFinal;

	    [DataMember(Name = "FaixaFinal", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Faixa Final", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(70)]
	    [FunctionalPoint("Precision[70:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.TCS_PARAMETRO_AUTORIZACAO.FAIXA_FINAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.FAIXA_FINAL")]
	    public System.String FaixaFinal
	    {
	    	    get
	    	    {
	    	          return _FaixaFinal;
	    	    }
	    	    set
	    	    {
	    	          if (this._FaixaFinal != value)
	    	          {
	    	              this.ValidateProperty("FaixaFinal", value);
	    	              this.OnFaixaFinalChanging(value);
	    	              this.RaiseDataMemberChanging("FaixaFinal");
	    	              this._FaixaFinal = value;
	    	              this.RaiseDataMemberChanged("FaixaFinal");
	    	              this.OnFaixaFinalChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For FaixaInicial
	    partial void OnFaixaInicialChanging(System.String value);
	    partial void OnFaixaInicialChanged();

	    private System.String _FaixaInicial;

	    [DataMember(Name = "FaixaInicial", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Faixa Inicial", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(70)]
	    [FunctionalPoint("Precision[70:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.TCS_PARAMETRO_AUTORIZACAO.FAIXA_INICIAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.FAIXA_INICIAL")]
	    public System.String FaixaInicial
	    {
	    	    get
	    	    {
	    	          return _FaixaInicial;
	    	    }
	    	    set
	    	    {
	    	          if (this._FaixaInicial != value)
	    	          {
	    	              this.ValidateProperty("FaixaInicial", value);
	    	              this.OnFaixaInicialChanging(value);
	    	              this.RaiseDataMemberChanging("FaixaInicial");
	    	              this._FaixaInicial = value;
	    	              this.RaiseDataMemberChanged("FaixaInicial");
	    	              this.OnFaixaInicialChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdGrupoParametro
	    partial void OnIdGrupoParametroChanging(Int16 value);
	    partial void OnIdGrupoParametroChanged();

	    private Int16 _IdGrupoParametro;

	    [DataMember(IsRequired = true, Name = "IdGrupoParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Grupo Parametro", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.TCS_PARAMETRO_AUTORIZACAO.TCS_PARAMETRO_GRUPO_AUTORIZACAO.ID_GRUPO_PARAMETRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.TCS_PARAMETRO_GRUPO_AUTORIZACAO.ID_GRUPO_PARAMETRO")]
	    public Int16 IdGrupoParametro
	    {
	    	    get
	    	    {
	    	          return _IdGrupoParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdGrupoParametro != value)
	    	          {
	    	              this.ValidateProperty("IdGrupoParametro", value);
	    	              this.OnIdGrupoParametroChanging(value);
	    	              this.RaiseDataMemberChanging("IdGrupoParametro");
	    	              this._IdGrupoParametro = value;
	    	              this.RaiseDataMemberChanged("IdGrupoParametro");
	    	              this.OnIdGrupoParametroChanged();
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
	    [Display(Name = "Aplicativo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.TCS_PARAMETRO_AUTORIZACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO")]
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
	    //Extensibility Partial Method Definitions For IndicaEnviaPdv
	    partial void OnIndicaEnviaPdvChanging(Boolean value);
	    partial void OnIndicaEnviaPdvChanged();

	    private Boolean _IndicaEnviaPdv;

	    [DataMember(IsRequired = true, Name = "IndicaEnviaPdv", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Envia PDV", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.TCS_PARAMETRO_AUTORIZACAO.INDICA_ENVIA_PDV];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.INDICA_ENVIA_PDV")]
	    public Boolean IndicaEnviaPdv
	    {
	    	    get
	    	    {
	    	          return _IndicaEnviaPdv;
	    	    }
	    	    set
	    	    {
	    	          if (this._IndicaEnviaPdv != value)
	    	          {
	    	              this.ValidateProperty("IndicaEnviaPdv", value);
	    	              this.OnIndicaEnviaPdvChanging(value);
	    	              this.RaiseDataMemberChanging("IndicaEnviaPdv");
	    	              this._IndicaEnviaPdv = value;
	    	              this.RaiseDataMemberChanged("IndicaEnviaPdv");
	    	              this.OnIndicaEnviaPdvChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IndicaParametroLinx
	    partial void OnIndicaParametroLinxChanging(Boolean value);
	    partial void OnIndicaParametroLinxChanged();

	    private Boolean _IndicaParametroLinx;

	    [DataMember(IsRequired = true, Name = "IndicaParametroLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Parâmetro Linx", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[true];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[true];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="true")]
	    public Boolean IndicaParametroLinx
	    {
	    	    get
	    	    {
	    	          return _IndicaParametroLinx;
	    	    }
	    	    set
	    	    {
	    	          if (this._IndicaParametroLinx != value)
	    	          {
	    	              this.ValidateProperty("IndicaParametroLinx", value);
	    	              this.OnIndicaParametroLinxChanging(value);
	    	              this.RaiseDataMemberChanging("IndicaParametroLinx");
	    	              this._IndicaParametroLinx = value;
	    	              this.RaiseDataMemberChanged("IndicaParametroLinx");
	    	              this.OnIndicaParametroLinxChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxDatatypeParametro
	    partial void OnLxDatatypeParametroChanging(Byte value);
	    partial void OnLxDatatypeParametroChanged();

	    private Byte _LxDatatypeParametro;

	    [DataMember(IsRequired = true, Name = "LxDatatypeParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo do Dado", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoValorParametro];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.TCS_PARAMETRO_AUTORIZACAO.LX_DATATYPE_PARAMETRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.LX_DATATYPE_PARAMETRO")]
	    public Byte LxDatatypeParametro
	    {
	    	    get
	    	    {
	    	          return _LxDatatypeParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxDatatypeParametro != value)
	    	          {
	    	              this.ValidateProperty("LxDatatypeParametro", value);
	    	              this.OnLxDatatypeParametroChanging(value);
	    	              this.RaiseDataMemberChanging("LxDatatypeParametro");
	    	              this._LxDatatypeParametro = value;
	    	              this.RaiseDataMemberChanged("LxDatatypeParametro");
	    	              this.OnLxDatatypeParametroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoValidacaoParametro
	    partial void OnLxTipoValidacaoParametroChanging(Byte value);
	    partial void OnLxTipoValidacaoParametroChanged();

	    private Byte _LxTipoValidacaoParametro;

	    [DataMember(IsRequired = true, Name = "LxTipoValidacaoParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo Validação", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoValidacaoParametro];KpiName[];KpiRelatedAttribute[];DefaultValue[8];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.TCS_PARAMETRO_AUTORIZACAO.LX_TIPO_VALIDACAO_PARAMETRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.LX_TIPO_VALIDACAO_PARAMETRO")]
	    public Byte LxTipoValidacaoParametro
	    {
	    	    get
	    	    {
	    	          return _LxTipoValidacaoParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoValidacaoParametro != value)
	    	          {
	    	              this.ValidateProperty("LxTipoValidacaoParametro", value);
	    	              this.OnLxTipoValidacaoParametroChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoValidacaoParametro");
	    	              this._LxTipoValidacaoParametro = value;
	    	              this.RaiseDataMemberChanged("LxTipoValidacaoParametro");
	    	              this.OnLxTipoValidacaoParametroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NivelAcesso
	    partial void OnNivelAcessoChanging(Byte value);
	    partial void OnNivelAcessoChanged();

	    private Byte _NivelAcesso;

	    [DataMember(IsRequired = true, Name = "NivelAcesso", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nível Acesso Visualização", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.TCS_PARAMETRO_AUTORIZACAO.NIVEL_ACESSO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.NIVEL_ACESSO")]
	    public Byte NivelAcesso
	    {
	    	    get
	    	    {
	    	          return _NivelAcesso;
	    	    }
	    	    set
	    	    {
	    	          if (this._NivelAcesso != value)
	    	          {
	    	              this.ValidateProperty("NivelAcesso", value);
	    	              this.OnNivelAcessoChanging(value);
	    	              this.RaiseDataMemberChanging("NivelAcesso");
	    	              this._NivelAcesso = value;
	    	              this.RaiseDataMemberChanged("NivelAcesso");
	    	              this.OnNivelAcessoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NivelAcessoEdicao
	    partial void OnNivelAcessoEdicaoChanging(Byte value);
	    partial void OnNivelAcessoEdicaoChanged();

	    private Byte _NivelAcessoEdicao;

	    [DataMember(IsRequired = true, Name = "NivelAcessoEdicao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nível Acesso Edição", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.TCS_PARAMETRO_AUTORIZACAO.NIVEL_ACESSO_EDICAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.NIVEL_ACESSO_EDICAO")]
	    public Byte NivelAcessoEdicao
	    {
	    	    get
	    	    {
	    	          return _NivelAcessoEdicao;
	    	    }
	    	    set
	    	    {
	    	          if (this._NivelAcessoEdicao != value)
	    	          {
	    	              this.ValidateProperty("NivelAcessoEdicao", value);
	    	              this.OnNivelAcessoEdicaoChanging(value);
	    	              this.RaiseDataMemberChanging("NivelAcessoEdicao");
	    	              this._NivelAcessoEdicao = value;
	    	              this.RaiseDataMemberChanged("NivelAcessoEdicao");
	    	              this.OnNivelAcessoEdicaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ObsParametro
	    partial void OnObsParametroChanging(System.String value);
	    partial void OnObsParametroChanged();

	    private System.String _ObsParametro;

	    [DataMember(Name = "ObsParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Obs", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.TCS_PARAMETRO_AUTORIZACAO.OBS_PARAMETRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.OBS_PARAMETRO")]
	    public System.String ObsParametro
	    {
	    	    get
	    	    {
	    	          return _ObsParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._ObsParametro != value)
	    	          {
	    	              this.ValidateProperty("ObsParametro", value);
	    	              this.OnObsParametroChanging(value);
	    	              this.RaiseDataMemberChanging("ObsParametro");
	    	              this._ObsParametro = value;
	    	              this.RaiseDataMemberChanged("ObsParametro");
	    	              this.OnObsParametroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For PermiteVariacaoPorEntidade
	    partial void OnPermiteVariacaoPorEntidadeChanging(Boolean value);
	    partial void OnPermiteVariacaoPorEntidadeChanged();

	    private Boolean _PermiteVariacaoPorEntidade;

	    [DataMember(IsRequired = true, Name = "PermiteVariacaoPorEntidade", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Permite Variação por Entidade", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.TCS_PARAMETRO_AUTORIZACAO.PERMITE_VARIACAO_POR_ENTIDADE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.PERMITE_VARIACAO_POR_ENTIDADE")]
	    public Boolean PermiteVariacaoPorEntidade
	    {
	    	    get
	    	    {
	    	          return _PermiteVariacaoPorEntidade;
	    	    }
	    	    set
	    	    {
	    	          if (this._PermiteVariacaoPorEntidade != value)
	    	          {
	    	              this.ValidateProperty("PermiteVariacaoPorEntidade", value);
	    	              this.OnPermiteVariacaoPorEntidadeChanging(value);
	    	              this.RaiseDataMemberChanging("PermiteVariacaoPorEntidade");
	    	              this._PermiteVariacaoPorEntidade = value;
	    	              this.RaiseDataMemberChanged("PermiteVariacaoPorEntidade");
	    	              this.OnPermiteVariacaoPorEntidadeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For TituloParametro
	    partial void OnTituloParametroChanging(System.String value);
	    partial void OnTituloParametroChanged();

	    private System.String _TituloParametro;

	    [DataMember(IsRequired = true, Name = "TituloParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Título", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.TCS_PARAMETRO_AUTORIZACAO.TITULO_PARAMETRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.TITULO_PARAMETRO")]
	    public System.String TituloParametro
	    {
	    	    get
	    	    {
	    	          return _TituloParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._TituloParametro != value)
	    	          {
	    	              this.ValidateProperty("TituloParametro", value);
	    	              this.OnTituloParametroChanging(value);
	    	              this.RaiseDataMemberChanging("TituloParametro");
	    	              this._TituloParametro = value;
	    	              this.RaiseDataMemberChanged("TituloParametro");
	    	              this.OnTituloParametroChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO), QualifiedEntitySetName = "AutorizacaoContext.TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.ID_TABELA_SELECAO", Source = "IdTabelaSelecao", Target = "ID_TABELA_SELECAO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO", RelationPropertyName = "TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.LX_PARAMETRO_HIERARQUIA", Source = "LxParametroHierarquia", Target = "LX_PARAMETRO_HIERARQUIA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO", RelationPropertyName = "TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.TCS_TABELA_AUTORIZACAO.UID_TABELA", Source = "UidTabela", Target = "UID_TABELA", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_TABELA_AUTORIZACAO", RelationPropertyName = "TCS_TABELA_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.TCS_PARAMETRO_AUTORIZACAO.ID_PARAMETRO", Source = "IdParametro", Target = "ID_PARAMETRO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_PARAMETRO_AUTORIZACAO", RelationPropertyName = "TCS_PARAMETRO_AUTORIZACAO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxParametroHierarquiaValues()
	    {
	    	    return Linx.Framework.BV.Domains.ParametroHierarquia.GetValues();
	    }
	    private string _lxParametroHierarquiaName;
	    [DataMember(IsRequired = false, Name = "LxParametroHierarquiaName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Hierarquia", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxParametroHierarquiaName
	    {
	    	    get { if (this.LxParametroHierarquia.IsNull()) { _lxParametroHierarquiaName = String.Empty; } else { string key = this.LxParametroHierarquia.ToString(); var dmValues = this.GetLxParametroHierarquiaValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxParametroHierarquiaName) _lxParametroHierarquiaName = domainName; } return _lxParametroHierarquiaName; } set { _lxParametroHierarquiaName = value;  }
	    }
	    public Dictionary<string, string> GetLxDatatypeParametroValues()
	    {
	    	    return Linx.Framework.BV.Domains.TipoValorParametro.GetValues();
	    }
	    private string _lxDatatypeParametroName;
	    [DataMember(IsRequired = false, Name = "LxDatatypeParametroName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo do Dado", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxDatatypeParametroName
	    {
	    	    get { if (this.LxDatatypeParametro.IsNull()) { _lxDatatypeParametroName = String.Empty; } else { string key = this.LxDatatypeParametro.ToString(); var dmValues = this.GetLxDatatypeParametroValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxDatatypeParametroName) _lxDatatypeParametroName = domainName; } return _lxDatatypeParametroName; } set { _lxDatatypeParametroName = value;  }
	    }
	    public Dictionary<string, string> GetLxTipoValidacaoParametroValues()
	    {
	    	    return Linx.Framework.BV.Domains.TipoValidacaoParametro.GetValues();
	    }
	    private string _lxTipoValidacaoParametroName;
	    [DataMember(IsRequired = false, Name = "LxTipoValidacaoParametroName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo Validação", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxTipoValidacaoParametroName
	    {
	    	    get { if (this.LxTipoValidacaoParametro.IsNull()) { _lxTipoValidacaoParametroName = String.Empty; } else { string key = this.LxTipoValidacaoParametro.ToString(); var dmValues = this.GetLxTipoValidacaoParametroValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxTipoValidacaoParametroName) _lxTipoValidacaoParametroName = domainName; } return _lxTipoValidacaoParametroName; } set { _lxTipoValidacaoParametroName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[TcsParametroAutorizacao];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdParametro];ReadOnly[false];Entities[TCS_PARAMETRO_AUTORIZACAO:IdParametro];SubQueryInfo[Select 1 From #ParentAlias#.TCS_PARAMETRO_AUTORIZACAO_LISTA as #Alias#];EdmEntityName[TCS_PARAMETRO_AUTORIZACAO];EntityRelations[TCS_TABELA_AUTORIZACAO(TCS_TABELA_AUTORIZACAO)#TCS_TRANSACAO_AUTORIZACAO(TCS_TRANSACAO_AUTORIZACAO)#TCS_MODULO_AUTORIZACAO(TCS_MODULO_AUTORIZACAO)#TCS_APLICATIVO(TCS_APLICATIVO)#TCS_OBJETO_AUTORIZACAO(TCS_OBJETO_AUTORIZACAO)#TCS_PARAMETRO_GRUPO_AUTORIZACAO(TCS_PARAMETRO_GRUPO_AUTORIZACAO)];EdmParentEntityName[TCS_PARAMETRO_GRUPO_AUTORIZACAO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsParametroAutorizacaoGrupo")]
	[Serializable()]
	public partial class TcsParametroAutorizacaoGrupoParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For DescParametro
	    partial void OnDescParametroChanging(System.String value);
	    partial void OnDescParametroChanged();

	    private System.String _DescParametro;

	    [DataMember(IsRequired = true, Name = "DescParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PARAMETRO_AUTORIZACAO.DESC_PARAMETRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.DESC_PARAMETRO")]
	    public System.String DescParametro
	    {
	    	    get
	    	    {
	    	          return _DescParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescParametro != value)
	    	          {
	    	              this.ValidateProperty("DescParametro", value);
	    	              this.OnDescParametroChanging(value);
	    	              this.RaiseDataMemberChanging("DescParametro");
	    	              this._DescParametro = value;
	    	              this.RaiseDataMemberChanged("DescParametro");
	    	              this.OnDescParametroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdGrupoParametro
	    partial void OnIdGrupoParametroChanging(Int16 value);
	    partial void OnIdGrupoParametroChanged();

	    private Int16 _IdGrupoParametro;

	    [DataMember(IsRequired = true, Name = "IdGrupoParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Grupo Parametro", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PARAMETRO_AUTORIZACAO.TCS_PARAMETRO_GRUPO_AUTORIZACAO.ID_GRUPO_PARAMETRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.TCS_PARAMETRO_GRUPO_AUTORIZACAO.ID_GRUPO_PARAMETRO")]
	    public Int16 IdGrupoParametro
	    {
	    	    get
	    	    {
	    	          return _IdGrupoParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdGrupoParametro != value)
	    	          {
	    	              this.ValidateProperty("IdGrupoParametro", value);
	    	              this.OnIdGrupoParametroChanging(value);
	    	              this.RaiseDataMemberChanging("IdGrupoParametro");
	    	              this._IdGrupoParametro = value;
	    	              this.RaiseDataMemberChanged("IdGrupoParametro");
	    	              this.OnIdGrupoParametroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdParametro
	    partial void OnIdParametroChanging(Int64 value);
	    partial void OnIdParametroChanged();

	    private Int64 _IdParametro;

	    [DataMember(IsRequired = true, Name = "IdParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Parametro", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PARAMETRO_AUTORIZACAO.ID_PARAMETRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.ID_PARAMETRO")]
	    public Int64 IdParametro
	    {
	    	    get
	    	    {
	    	          return _IdParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdParametro != value)
	    	          {
	    	              this.ValidateProperty("IdParametro", value);
	    	              this.OnIdParametroChanging(value);
	    	              this.RaiseDataMemberChanging("IdParametro");
	    	              this._IdParametro = value;
	    	              this.RaiseDataMemberChanged("IdParametro");
	    	              this.OnIdParametroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For TituloParametro
	    partial void OnTituloParametroChanging(System.String value);
	    partial void OnTituloParametroChanged();

	    private System.String _TituloParametro;

	    [DataMember(IsRequired = true, Name = "TituloParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Título", Description="", Order = 14, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PARAMETRO_AUTORIZACAO.TITULO_PARAMETRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.TITULO_PARAMETRO")]
	    public System.String TituloParametro
	    {
	    	    get
	    	    {
	    	          return _TituloParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._TituloParametro != value)
	    	          {
	    	              this.ValidateProperty("TituloParametro", value);
	    	              this.OnTituloParametroChanging(value);
	    	              this.RaiseDataMemberChanging("TituloParametro");
	    	              this._TituloParametro = value;
	    	              this.RaiseDataMemberChanged("TituloParametro");
	    	              this.OnTituloParametroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescGrupoParametro
	    partial void OnDescGrupoParametroChanging(System.String value);
	    partial void OnDescGrupoParametroChanged();

	    private System.String _DescGrupoParametro;

	    [DataMember(IsRequired = true, Name = "DescGrupoParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_PARAMETRO_AUTORIZACAO.TCS_PARAMETRO_GRUPO_AUTORIZACAO.DESC_GRUPO_PARAMETRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_GRUPO_AUTORIZACAO.DESC_GRUPO_PARAMETRO")]
	    public System.String DescGrupoParametro
	    {
	    	    get
	    	    {
	    	          return _DescGrupoParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescGrupoParametro != value)
	    	          {
	    	              this.ValidateProperty("DescGrupoParametro", value);
	    	              this.OnDescGrupoParametroChanging(value);
	    	              this.RaiseDataMemberChanging("DescGrupoParametro");
	    	              this._DescGrupoParametro = value;
	    	              this.RaiseDataMemberChanged("DescGrupoParametro");
	    	              this.OnDescGrupoParametroChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_PARAMETRO_AUTORIZACAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_PARAMETRO_AUTORIZACAO), QualifiedEntitySetName = "AutorizacaoContext.TCS_PARAMETRO_AUTORIZACAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PARAMETRO_AUTORIZACAO.ID_PARAMETRO", Source = "IdParametro", Target = "ID_PARAMETRO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_PARAMETRO_AUTORIZACAO", RelationPropertyName = "TCS_PARAMETRO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PARAMETRO_AUTORIZACAO.DESC_PARAMETRO", Source = "DescParametro", Target = "DESC_PARAMETRO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_PARAMETRO_AUTORIZACAO", RelationPropertyName = "TCS_PARAMETRO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PARAMETRO_AUTORIZACAO.TITULO_PARAMETRO", Source = "TituloParametro", Target = "TITULO_PARAMETRO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_PARAMETRO_AUTORIZACAO", RelationPropertyName = "TCS_PARAMETRO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_PARAMETRO_AUTORIZACAO.TCS_PARAMETRO_GRUPO_AUTORIZACAO.ID_GRUPO_PARAMETRO", Source = "IdGrupoParametro", Target = "ID_GRUPO_PARAMETRO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_PARAMETRO_GRUPO_AUTORIZACAO", RelationPropertyName = "TCS_PARAMETRO_GRUPO_AUTORIZACAO" });

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
	[DomainIdentifier("ProcessorOverviewParametroAutorizacaoDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class ParametroAutorizacaoDomainService : DomainService, IDataServiceContext 
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

		
	    public ParametroAutorizacaoDomainService() : this("", null, null) { }
	    public ParametroAutorizacaoDomainService(string connectionString) : this(connectionString, null, null) { }
	    public ParametroAutorizacaoDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public ParametroAutorizacaoDomainService(Linx.Framework.Autorizacao.BM.AutorizacaoContext dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public ParametroAutorizacaoDomainService(string connectionString, Linx.Framework.Autorizacao.BM.AutorizacaoContext dataContext, Dictionary<string, string> headers) : base() 
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
 	        var _TcsParametroAutorizacaoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsParametroAutorizacao && e.Entity.GetType().Name == "TcsParametroAutorizacao" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _TcsParametroAutorizacaoElements)
 	           if (((TcsParametroAutorizacao)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 	        var _TcsParametroGrupoAutorizacaoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsParametroGrupoAutorizacao && e.Entity.GetType().Name == "TcsParametroGrupoAutorizacao" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _TcsParametroGrupoAutorizacaoElements)
 	           if (((TcsParametroGrupoAutorizacao)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 
 	        //Remove inconsistent details
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsParametroTabelaSelecaoAutorizacao && e.Entity.GetType().Name == "TcsParametroTabelaSelecaoAutorizacao" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsParametroAutorizacaoGrupo && e.Entity.GetType().Name == "TcsParametroAutorizacaoGrupo" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
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
	    //Get All LookUpTcsParametroGrupoAutorizacao.
	    public IQueryable<LookUpTcsParametroGrupoAutorizacao> GetAllLookUpTcsParametroGrupoAutorizacao()
	    {
	        return this.GetLookUpTcsParametroGrupoAutorizacao(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsParametroGrupoAutorizacao By EntitySearch.
	    public IQueryable<LookUpTcsParametroGrupoAutorizacao> GetLookUpTcsParametroGrupoAutorizacaoByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsParametroGrupoAutorizacao(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsParametroGrupoAutorizacao.
	    public IQueryable<LookUpTcsParametroGrupoAutorizacao> GetLookUpTcsParametroGrupoAutorizacao(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TCS_PARAMETRO_GRUPO_AUTORIZACAO" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsParametroGrupoAutorizacao";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsParametroGrupoAutorizacao));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsParametroGrupoAutorizacao> query =  
	
	            (from entity in this.DbContext.TCS_PARAMETRO_GRUPO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpTcsParametroGrupoAutorizacao()		
	            {
	            
                DescGrupoParametro = entity.DESC_GRUPO_PARAMETRO
                , IdGrupoParametro = entity.ID_GRUPO_PARAMETRO
	            });

	            
	
		
	
	
	        return query;

	    }
		
			
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
	            
                IdTcsAplicativo = entity.ID_TCS_APLICATIVO
                , DescricaoAplicativo = entity.DESCRICAO_APLICATIVO
	            });

	            
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpTcsTabelaAutorizacaoSelecao.
	    public IQueryable<LookUpTcsTabelaAutorizacaoSelecao> GetAllLookUpTcsTabelaAutorizacaoSelecao()
	    {
	        return this.GetLookUpTcsTabelaAutorizacaoSelecao(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsTabelaAutorizacaoSelecao By EntitySearch.
	    public IQueryable<LookUpTcsTabelaAutorizacaoSelecao> GetLookUpTcsTabelaAutorizacaoSelecaoByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsTabelaAutorizacaoSelecao(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsTabelaAutorizacaoSelecao.
	    public IQueryable<LookUpTcsTabelaAutorizacaoSelecao> GetLookUpTcsTabelaAutorizacaoSelecao(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TCS_TABELA_AUTORIZACAO" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsTabelaAutorizacaoSelecao";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsTabelaAutorizacaoSelecao));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsTabelaAutorizacaoSelecao> query =  
	
	            (from entity in this.DbContext.TCS_TABELA_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpTcsTabelaAutorizacaoSelecao()		
	            {
	            
                NomeTabela = entity.NOME_TABELA
                , DescTabela = entity.DESC_TABELA
                , UidTabela = entity.UID_TABELA
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
	
		

	        if (entityName.InList("Linx.Framework.BV.ParametroAutorizacao.TcsParametroAutorizacao"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsParametroAutorizacao",
	        			NameSpace = "Linx.Framework.BV.ParametroAutorizacao",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsParametroAutorizacao",
	        			ClearMethodName = "ClearTcsParametroAutorizacao",
	        			QueryMethodName  = "GetPagedTcsParametroAutorizacao",	
	        			CountingMethodName  = "GetTcsParametroAutorizacao" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.ParametroAutorizacao.TcsParametroAutorizacao"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.ParametroAutorizacao.TcsParametroAutorizacao"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.ParametroAutorizacao.TcsParametroAutorizacao", "Linx.Framework.BV.ParametroAutorizacao.TcsParametroTabelaSelecaoAutorizacao"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsParametroTabelaSelecaoAutorizacao" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.ParametroAutorizacao",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsParametroAutorizacao",	
	        			DisplayName = "Variação",
	        			ClearMethodName = "ClearTcsParametroTabelaSelecaoAutorizacao" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsParametroTabelaSelecaoAutorizacao" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsParametroTabelaSelecaoAutorizacao" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.ParametroAutorizacao.TcsParametroTabelaSelecaoAutorizacao"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.ParametroAutorizacao.TcsParametroTabelaSelecaoAutorizacao" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.ParametroAutorizacao.TcsParametroGrupoAutorizacao"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsParametroGrupoAutorizacao",
	        			NameSpace = "Linx.Framework.BV.ParametroAutorizacao",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsParametroGrupoAutorizacao",
	        			ClearMethodName = "ClearTcsParametroGrupoAutorizacao",
	        			QueryMethodName  = "GetPagedTcsParametroGrupoAutorizacao",	
	        			CountingMethodName  = "GetTcsParametroGrupoAutorizacao" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.ParametroAutorizacao.TcsParametroGrupoAutorizacao"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.ParametroAutorizacao.TcsParametroGrupoAutorizacao"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.ParametroAutorizacao.TcsParametroGrupoAutorizacao", "Linx.Framework.BV.ParametroAutorizacao.TcsParametroAutorizacaoGrupo"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsParametroAutorizacaoGrupo" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.ParametroAutorizacao",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsParametroGrupoAutorizacao",	
	        			DisplayName = "TcsParametroAutorizacao",
	        			ClearMethodName = "ClearTcsParametroAutorizacaoGrupo" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsParametroAutorizacaoGrupo" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsParametroAutorizacaoGrupo" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.ParametroAutorizacao.TcsParametroAutorizacaoGrupo"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.ParametroAutorizacao.TcsParametroAutorizacaoGrupo" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
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

         		    return new string[] { "Framework_ParametroAutorizacaoClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.ParametroAutorizacaoClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Framework_parametroAutorizacaoService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.parametroAutorizacaoService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear TcsParametroAutorizacao.
	    public IEnumerable<TcsParametroAutorizacao> ClearTcsParametroAutorizacao()
	    {
	        List<TcsParametroAutorizacao> result = new List<TcsParametroAutorizacao>();
	        result.Add(new TcsParametroAutorizacao(false));	
			
	        result[0].TcsParametroTabelaSelecaoAutorizacaoList = new List<TcsParametroTabelaSelecaoAutorizacao>();
	        ((List<TcsParametroTabelaSelecaoAutorizacao>)result[0].TcsParametroTabelaSelecaoAutorizacaoList).Add(new TcsParametroTabelaSelecaoAutorizacao());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsParametroTabelaSelecaoAutorizacao.
	    public IEnumerable<TcsParametroTabelaSelecaoAutorizacao> ClearTcsParametroTabelaSelecaoAutorizacao()
	    {
	        List<TcsParametroTabelaSelecaoAutorizacao> result = new List<TcsParametroTabelaSelecaoAutorizacao>();
	        result.Add(new TcsParametroTabelaSelecaoAutorizacao());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsParametroGrupoAutorizacao.
	    public IEnumerable<TcsParametroGrupoAutorizacao> ClearTcsParametroGrupoAutorizacao()
	    {
	        List<TcsParametroGrupoAutorizacao> result = new List<TcsParametroGrupoAutorizacao>();
	        result.Add(new TcsParametroGrupoAutorizacao());	
			
	        result[0].TcsParametroAutorizacaoGrupoList = new List<TcsParametroAutorizacaoGrupo>();
	        ((List<TcsParametroAutorizacaoGrupo>)result[0].TcsParametroAutorizacaoGrupoList).Add(new TcsParametroAutorizacaoGrupo());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsParametroAutorizacaoGrupo.
	    public IEnumerable<TcsParametroAutorizacaoGrupo> ClearTcsParametroAutorizacaoGrupo()
	    {
	        List<TcsParametroAutorizacaoGrupo> result = new List<TcsParametroAutorizacaoGrupo>();
	        result.Add(new TcsParametroAutorizacaoGrupo());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsParametroAutorizacao.
	    public IQueryable<TcsParametroAutorizacao> GetTcsParametroAutorizacao()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsParametroAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_PARAMETRO_AUTORIZACAO
                  let entity0Al2 = entity0.TCS_APLICATIVO
                  let entity0Al3 = entity0.TCS_TABELA_AUTORIZACAO
                  let entity0Al1 = entity0.TCS_PARAMETRO_GRUPO_AUTORIZACAO
	            
	            	
	            select new TcsParametroAutorizacao()		
	            {
	            
                ColunaCodValida = entity0.COLUNA_COD_VALIDA
                , ColunaDescValida = entity0.COLUNA_DESC_VALIDA
                , DescGrupoParametro = entity0Al1.DESC_GRUPO_PARAMETRO
                , DescParametro = entity0.DESC_PARAMETRO
                , DescricaoAplicativo = entity0Al2.DESCRICAO_APLICATIVO
                , DescTabela = entity0Al3.DESC_TABELA
                , FaixaFinal = entity0.FAIXA_FINAL
                , FaixaInicial = entity0.FAIXA_INICIAL
                , IdGrupoParametro = entity0Al1.ID_GRUPO_PARAMETRO
                , IdParametro = entity0.ID_PARAMETRO
                , IdTcsAplicativo = entity0Al2.ID_TCS_APLICATIVO
                , IndicaEnviaPdv = entity0.INDICA_ENVIA_PDV
                , IndicaParametroLinx = true
                , LxDatatypeParametro = entity0.LX_DATATYPE_PARAMETRO
                , LxDatatypeParametroName = ((entity0.LX_DATATYPE_PARAMETRO) == 2 ? "Caractere" : ((entity0.LX_DATATYPE_PARAMETRO) == 3 ? "Data" : ((entity0.LX_DATATYPE_PARAMETRO) == 4 ? "Lógico" : ((entity0.LX_DATATYPE_PARAMETRO) == 1 ? "Numérico" : ((entity0.LX_DATATYPE_PARAMETRO) == 5 ? "Senha" : "")))))
                , LxTipoValidacaoParametro = entity0.LX_TIPO_VALIDACAO_PARAMETRO
                , LxTipoValidacaoParametroName = ((entity0.LX_TIPO_VALIDACAO_PARAMETRO) == 8 ? "Sem Validação" : ((entity0.LX_TIPO_VALIDACAO_PARAMETRO) == 2 ? "Validação Contra Tabela (Combo)" : ((entity0.LX_TIPO_VALIDACAO_PARAMETRO) == 3 ? "Validação Contra Faixa" : ((entity0.LX_TIPO_VALIDACAO_PARAMETRO) == 4 ? "Validação Contra Objeto CRM" : ((entity0.LX_TIPO_VALIDACAO_PARAMETRO) == 1 ? "Validação Contra Tabela (Valida)" : "")))))
                , NivelAcesso = entity0.NIVEL_ACESSO
                , NivelAcessoEdicao = entity0.NIVEL_ACESSO_EDICAO
                , ObsParametro = entity0.OBS_PARAMETRO
                , PermiteVariacaoPorEntidade = entity0.PERMITE_VARIACAO_POR_ENTIDADE
                , TituloParametro = entity0.TITULO_PARAMETRO
                , UidTabela = entity0Al3.UID_TABELA
			
                ,TcsParametroTabelaSelecaoAutorizacaoList = 
	                        (from entity1 in entity0.TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO_LISTA
                                  let entity1Al1 = entity1.TCS_TABELA_AUTORIZACAO
                                  let entity1Al2 = entity1.TCS_PARAMETRO_AUTORIZACAO
	                        
	                        	
	                        select new TcsParametroTabelaSelecaoAutorizacao()
	                        {
	                        
                                DescTabela = entity1Al1.DESC_TABELA
                                , IdParametro = entity1Al2.ID_PARAMETRO
                                , IdTabelaSelecao = entity1.ID_TABELA_SELECAO
                                , LxParametroHierarquia = entity1.LX_PARAMETRO_HIERARQUIA
                                , LxParametroHierarquiaName = ((entity1.LX_PARAMETRO_HIERARQUIA) == 100 ? "Obrigatório" : ((entity1.LX_PARAMETRO_HIERARQUIA) == 1 ? "Variação Nível 1" : ((entity1.LX_PARAMETRO_HIERARQUIA) == 2 ? "Variação Nível 2" : ((entity1.LX_PARAMETRO_HIERARQUIA) == 3 ? "Variação Nível 3" : ((entity1.LX_PARAMETRO_HIERARQUIA) == 4 ? "Variação Nível 4" : ((entity1.LX_PARAMETRO_HIERARQUIA) == 5 ? "Variação Nível 5" : ""))))))
                                , NomeTabela = entity1Al1.NOME_TABELA
                                , UidTabela = entity1Al1.UID_TABELA
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsParametroTabelaSelecaoAutorizacao.
	    public IQueryable<TcsParametroTabelaSelecaoAutorizacao> GetTcsParametroTabelaSelecaoAutorizacao()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsParametroTabelaSelecaoAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO
                  let entity0Al1 = entity0.TCS_TABELA_AUTORIZACAO
                  let entity0Al2 = entity0.TCS_PARAMETRO_AUTORIZACAO
	            
	            	
	            select new TcsParametroTabelaSelecaoAutorizacao()		
	            {
	            
                DescTabela = entity0Al1.DESC_TABELA
                , IdParametro = entity0Al2.ID_PARAMETRO
                , IdTabelaSelecao = entity0.ID_TABELA_SELECAO
                , LxParametroHierarquia = entity0.LX_PARAMETRO_HIERARQUIA
                , LxParametroHierarquiaName = ((entity0.LX_PARAMETRO_HIERARQUIA) == 100 ? "Obrigatório" : ((entity0.LX_PARAMETRO_HIERARQUIA) == 1 ? "Variação Nível 1" : ((entity0.LX_PARAMETRO_HIERARQUIA) == 2 ? "Variação Nível 2" : ((entity0.LX_PARAMETRO_HIERARQUIA) == 3 ? "Variação Nível 3" : ((entity0.LX_PARAMETRO_HIERARQUIA) == 4 ? "Variação Nível 4" : ((entity0.LX_PARAMETRO_HIERARQUIA) == 5 ? "Variação Nível 5" : ""))))))
                , NomeTabela = entity0Al1.NOME_TABELA
                , UidTabela = entity0Al1.UID_TABELA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsParametroAutorizacaoNoAssociations.
	    public IQueryable<TcsParametroAutorizacao> GetTcsParametroAutorizacaoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsParametroAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_PARAMETRO_AUTORIZACAO
                  let entity0Al2 = entity0.TCS_APLICATIVO
                  let entity0Al3 = entity0.TCS_TABELA_AUTORIZACAO
                  let entity0Al1 = entity0.TCS_PARAMETRO_GRUPO_AUTORIZACAO
	            
	            	
	            select new TcsParametroAutorizacao()		
	            {
	            
                ColunaCodValida = entity0.COLUNA_COD_VALIDA
                , ColunaDescValida = entity0.COLUNA_DESC_VALIDA
                , DescGrupoParametro = entity0Al1.DESC_GRUPO_PARAMETRO
                , DescParametro = entity0.DESC_PARAMETRO
                , DescricaoAplicativo = entity0Al2.DESCRICAO_APLICATIVO
                , DescTabela = entity0Al3.DESC_TABELA
                , FaixaFinal = entity0.FAIXA_FINAL
                , FaixaInicial = entity0.FAIXA_INICIAL
                , IdGrupoParametro = entity0Al1.ID_GRUPO_PARAMETRO
                , IdParametro = entity0.ID_PARAMETRO
                , IdTcsAplicativo = entity0Al2.ID_TCS_APLICATIVO
                , IndicaEnviaPdv = entity0.INDICA_ENVIA_PDV
                , IndicaParametroLinx = true
                , LxDatatypeParametro = entity0.LX_DATATYPE_PARAMETRO
                , LxDatatypeParametroName = ((entity0.LX_DATATYPE_PARAMETRO) == 2 ? "Caractere" : ((entity0.LX_DATATYPE_PARAMETRO) == 3 ? "Data" : ((entity0.LX_DATATYPE_PARAMETRO) == 4 ? "Lógico" : ((entity0.LX_DATATYPE_PARAMETRO) == 1 ? "Numérico" : ((entity0.LX_DATATYPE_PARAMETRO) == 5 ? "Senha" : "")))))
                , LxTipoValidacaoParametro = entity0.LX_TIPO_VALIDACAO_PARAMETRO
                , LxTipoValidacaoParametroName = ((entity0.LX_TIPO_VALIDACAO_PARAMETRO) == 8 ? "Sem Validação" : ((entity0.LX_TIPO_VALIDACAO_PARAMETRO) == 2 ? "Validação Contra Tabela (Combo)" : ((entity0.LX_TIPO_VALIDACAO_PARAMETRO) == 3 ? "Validação Contra Faixa" : ((entity0.LX_TIPO_VALIDACAO_PARAMETRO) == 4 ? "Validação Contra Objeto CRM" : ((entity0.LX_TIPO_VALIDACAO_PARAMETRO) == 1 ? "Validação Contra Tabela (Valida)" : "")))))
                , NivelAcesso = entity0.NIVEL_ACESSO
                , NivelAcessoEdicao = entity0.NIVEL_ACESSO_EDICAO
                , ObsParametro = entity0.OBS_PARAMETRO
                , PermiteVariacaoPorEntidade = entity0.PERMITE_VARIACAO_POR_ENTIDADE
                , TituloParametro = entity0.TITULO_PARAMETRO
                , UidTabela = entity0Al3.UID_TABELA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsParametroTabelaSelecaoAutorizacaoNoAssociations.
	    public IQueryable<TcsParametroTabelaSelecaoAutorizacao> GetTcsParametroTabelaSelecaoAutorizacaoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsParametroTabelaSelecaoAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO
                  let entity0Al1 = entity0.TCS_TABELA_AUTORIZACAO
                  let entity0Al2 = entity0.TCS_PARAMETRO_AUTORIZACAO
	            
	            	
	            select new TcsParametroTabelaSelecaoAutorizacao()		
	            {
	            
                DescTabela = entity0Al1.DESC_TABELA
                , IdParametro = entity0Al2.ID_PARAMETRO
                , IdTabelaSelecao = entity0.ID_TABELA_SELECAO
                , LxParametroHierarquia = entity0.LX_PARAMETRO_HIERARQUIA
                , LxParametroHierarquiaName = ((entity0.LX_PARAMETRO_HIERARQUIA) == 100 ? "Obrigatório" : ((entity0.LX_PARAMETRO_HIERARQUIA) == 1 ? "Variação Nível 1" : ((entity0.LX_PARAMETRO_HIERARQUIA) == 2 ? "Variação Nível 2" : ((entity0.LX_PARAMETRO_HIERARQUIA) == 3 ? "Variação Nível 3" : ((entity0.LX_PARAMETRO_HIERARQUIA) == 4 ? "Variação Nível 4" : ((entity0.LX_PARAMETRO_HIERARQUIA) == 5 ? "Variação Nível 5" : ""))))))
                , NomeTabela = entity0Al1.NOME_TABELA
                , UidTabela = entity0Al1.UID_TABELA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsParametroGrupoAutorizacao.
	    public IQueryable<TcsParametroGrupoAutorizacao> GetTcsParametroGrupoAutorizacao()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsParametroGrupoAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_PARAMETRO_GRUPO_AUTORIZACAO
	            
	            	
	            select new TcsParametroGrupoAutorizacao()		
	            {
	            
                DescGrupoParametro = entity0.DESC_GRUPO_PARAMETRO
                , IdGrupoParametro = entity0.ID_GRUPO_PARAMETRO
			
                ,TcsParametroAutorizacaoGrupoList = 
	                        (from entity1 in entity0.TCS_PARAMETRO_AUTORIZACAO_LISTA
                                  let entity1Al1 = entity1.TCS_PARAMETRO_GRUPO_AUTORIZACAO
	                        
	                        	
	                        select new TcsParametroAutorizacaoGrupo()
	                        {
	                        
                                DescParametro = entity1.DESC_PARAMETRO
                                , IdGrupoParametro = entity1Al1.ID_GRUPO_PARAMETRO
                                , IdParametro = entity1.ID_PARAMETRO
                                , TituloParametro = entity1.TITULO_PARAMETRO
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsParametroAutorizacaoGrupo.
	    public IQueryable<TcsParametroAutorizacaoGrupo> GetTcsParametroAutorizacaoGrupo()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsParametroAutorizacaoGrupo> result = 
	            (from entity0 in this.DbContext.TCS_PARAMETRO_AUTORIZACAO
                  let entity0Al1 = entity0.TCS_PARAMETRO_GRUPO_AUTORIZACAO
	            
	            	
	            select new TcsParametroAutorizacaoGrupo()		
	            {
	            
                DescParametro = entity0.DESC_PARAMETRO
                , IdGrupoParametro = entity0Al1.ID_GRUPO_PARAMETRO
                , IdParametro = entity0.ID_PARAMETRO
                , TituloParametro = entity0.TITULO_PARAMETRO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsParametroGrupoAutorizacaoNoAssociations.
	    public IQueryable<TcsParametroGrupoAutorizacao> GetTcsParametroGrupoAutorizacaoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsParametroGrupoAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_PARAMETRO_GRUPO_AUTORIZACAO
	            
	            	
	            select new TcsParametroGrupoAutorizacao()		
	            {
	            
                DescGrupoParametro = entity0.DESC_GRUPO_PARAMETRO
                , IdGrupoParametro = entity0.ID_GRUPO_PARAMETRO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsParametroAutorizacaoGrupoNoAssociations.
	    public IQueryable<TcsParametroAutorizacaoGrupo> GetTcsParametroAutorizacaoGrupoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsParametroAutorizacaoGrupo> result = 
	            (from entity0 in this.DbContext.TCS_PARAMETRO_AUTORIZACAO
                  let entity0Al1 = entity0.TCS_PARAMETRO_GRUPO_AUTORIZACAO
	            
	            	
	            select new TcsParametroAutorizacaoGrupo()		
	            {
	            
                DescParametro = entity0.DESC_PARAMETRO
                , IdGrupoParametro = entity0Al1.ID_GRUPO_PARAMETRO
                , IdParametro = entity0.ID_PARAMETRO
                , TituloParametro = entity0.TITULO_PARAMETRO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	result.Add("TcsParametroAutorizacao|IndicaParametroLinx");
	    	result.Add("TcsParametroAutorizacao|true");
	    	//Add filtering disabled property for TCS_PARAMETRO_AUTORIZACAO
	    	string[] bmDisabledTcsParametroAutorizacaoList = this.GetEDM().GetFilteringDisabledList("TCS_PARAMETRO_AUTORIZACAO");
	    	if (bmDisabledTcsParametroAutorizacaoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsParametroAutorizacaoList.Contains("TCS_PARAMETRO_AUTORIZACAO.COLUNA_COD_VALIDA"))
	    		{
	    			result.Add("TcsParametroAutorizacao|ColunaCodValida");
	    			result.Add("TcsParametroAutorizacao|TCS_PARAMETRO_AUTORIZACAO.COLUNA_COD_VALIDA");
	    		}
	
	    		if (bmDisabledTcsParametroAutorizacaoList.Contains("TCS_PARAMETRO_AUTORIZACAO.COLUNA_DESC_VALIDA"))
	    		{
	    			result.Add("TcsParametroAutorizacao|ColunaDescValida");
	    			result.Add("TcsParametroAutorizacao|TCS_PARAMETRO_AUTORIZACAO.COLUNA_DESC_VALIDA");
	    		}
	
	    		if (bmDisabledTcsParametroAutorizacaoList.Contains("TCS_PARAMETRO_AUTORIZACAO.DESC_PARAMETRO"))
	    		{
	    			result.Add("TcsParametroAutorizacao|DescParametro");
	    			result.Add("TcsParametroAutorizacao|TCS_PARAMETRO_AUTORIZACAO.DESC_PARAMETRO");
	    		}
	
	    		if (bmDisabledTcsParametroAutorizacaoList.Contains("TCS_PARAMETRO_AUTORIZACAO.FAIXA_FINAL"))
	    		{
	    			result.Add("TcsParametroAutorizacao|FaixaFinal");
	    			result.Add("TcsParametroAutorizacao|TCS_PARAMETRO_AUTORIZACAO.FAIXA_FINAL");
	    		}
	
	    		if (bmDisabledTcsParametroAutorizacaoList.Contains("TCS_PARAMETRO_AUTORIZACAO.FAIXA_INICIAL"))
	    		{
	    			result.Add("TcsParametroAutorizacao|FaixaInicial");
	    			result.Add("TcsParametroAutorizacao|TCS_PARAMETRO_AUTORIZACAO.FAIXA_INICIAL");
	    		}
	
	    		if (bmDisabledTcsParametroAutorizacaoList.Contains("TCS_PARAMETRO_AUTORIZACAO.ID_PARAMETRO"))
	    		{
	    			result.Add("TcsParametroAutorizacao|IdParametro");
	    			result.Add("TcsParametroAutorizacao|TCS_PARAMETRO_AUTORIZACAO.ID_PARAMETRO");
	    		}
	
	    		if (bmDisabledTcsParametroAutorizacaoList.Contains("TCS_PARAMETRO_AUTORIZACAO.INDICA_ENVIA_PDV"))
	    		{
	    			result.Add("TcsParametroAutorizacao|IndicaEnviaPdv");
	    			result.Add("TcsParametroAutorizacao|TCS_PARAMETRO_AUTORIZACAO.INDICA_ENVIA_PDV");
	    		}
	
	    		if (bmDisabledTcsParametroAutorizacaoList.Contains("TCS_PARAMETRO_AUTORIZACAO.LX_DATATYPE_PARAMETRO"))
	    		{
	    			result.Add("TcsParametroAutorizacao|LxDatatypeParametro");
	    			result.Add("TcsParametroAutorizacao|TCS_PARAMETRO_AUTORIZACAO.LX_DATATYPE_PARAMETRO");
	    		}
	
	    		if (bmDisabledTcsParametroAutorizacaoList.Contains("TCS_PARAMETRO_AUTORIZACAO.LX_TIPO_VALIDACAO_PARAMETRO"))
	    		{
	    			result.Add("TcsParametroAutorizacao|LxTipoValidacaoParametro");
	    			result.Add("TcsParametroAutorizacao|TCS_PARAMETRO_AUTORIZACAO.LX_TIPO_VALIDACAO_PARAMETRO");
	    		}
	
	    		if (bmDisabledTcsParametroAutorizacaoList.Contains("TCS_PARAMETRO_AUTORIZACAO.NIVEL_ACESSO"))
	    		{
	    			result.Add("TcsParametroAutorizacao|NivelAcesso");
	    			result.Add("TcsParametroAutorizacao|TCS_PARAMETRO_AUTORIZACAO.NIVEL_ACESSO");
	    		}
	
	    		if (bmDisabledTcsParametroAutorizacaoList.Contains("TCS_PARAMETRO_AUTORIZACAO.NIVEL_ACESSO_EDICAO"))
	    		{
	    			result.Add("TcsParametroAutorizacao|NivelAcessoEdicao");
	    			result.Add("TcsParametroAutorizacao|TCS_PARAMETRO_AUTORIZACAO.NIVEL_ACESSO_EDICAO");
	    		}
	
	    		if (bmDisabledTcsParametroAutorizacaoList.Contains("TCS_PARAMETRO_AUTORIZACAO.OBS_PARAMETRO"))
	    		{
	    			result.Add("TcsParametroAutorizacao|ObsParametro");
	    			result.Add("TcsParametroAutorizacao|TCS_PARAMETRO_AUTORIZACAO.OBS_PARAMETRO");
	    		}
	
	    		if (bmDisabledTcsParametroAutorizacaoList.Contains("TCS_PARAMETRO_AUTORIZACAO.PERMITE_VARIACAO_POR_ENTIDADE"))
	    		{
	    			result.Add("TcsParametroAutorizacao|PermiteVariacaoPorEntidade");
	    			result.Add("TcsParametroAutorizacao|TCS_PARAMETRO_AUTORIZACAO.PERMITE_VARIACAO_POR_ENTIDADE");
	    		}
	
	    		if (bmDisabledTcsParametroAutorizacaoList.Contains("TCS_PARAMETRO_AUTORIZACAO.TITULO_PARAMETRO"))
	    		{
	    			result.Add("TcsParametroAutorizacao|TituloParametro");
	    			result.Add("TcsParametroAutorizacao|TCS_PARAMETRO_AUTORIZACAO.TITULO_PARAMETRO");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO
	    	string[] bmDisabledTcsParametroTabelaSelecaoAutorizacaoList = this.GetEDM().GetFilteringDisabledList("TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO");
	    	if (bmDisabledTcsParametroTabelaSelecaoAutorizacaoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsParametroTabelaSelecaoAutorizacaoList.Contains("TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.ID_TABELA_SELECAO"))
	    		{
	    			result.Add("TcsParametroTabelaSelecaoAutorizacao|IdTabelaSelecao");
	    			result.Add("TcsParametroTabelaSelecaoAutorizacao|TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.ID_TABELA_SELECAO");
	    		}
	
	    		if (bmDisabledTcsParametroTabelaSelecaoAutorizacaoList.Contains("TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.LX_PARAMETRO_HIERARQUIA"))
	    		{
	    			result.Add("TcsParametroTabelaSelecaoAutorizacao|LxParametroHierarquia");
	    			result.Add("TcsParametroTabelaSelecaoAutorizacao|TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.LX_PARAMETRO_HIERARQUIA");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_PARAMETRO_GRUPO_AUTORIZACAO
	    	string[] bmDisabledTcsParametroGrupoAutorizacaoList = this.GetEDM().GetFilteringDisabledList("TCS_PARAMETRO_GRUPO_AUTORIZACAO");
	    	if (bmDisabledTcsParametroGrupoAutorizacaoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsParametroGrupoAutorizacaoList.Contains("TCS_PARAMETRO_GRUPO_AUTORIZACAO.DESC_GRUPO_PARAMETRO"))
	    		{
	    			result.Add("TcsParametroGrupoAutorizacao|DescGrupoParametro");
	    			result.Add("TcsParametroGrupoAutorizacao|TCS_PARAMETRO_GRUPO_AUTORIZACAO.DESC_GRUPO_PARAMETRO");
	    		}
	
	    		if (bmDisabledTcsParametroGrupoAutorizacaoList.Contains("TCS_PARAMETRO_GRUPO_AUTORIZACAO.ID_GRUPO_PARAMETRO"))
	    		{
	    			result.Add("TcsParametroGrupoAutorizacao|IdGrupoParametro");
	    			result.Add("TcsParametroGrupoAutorizacao|TCS_PARAMETRO_GRUPO_AUTORIZACAO.ID_GRUPO_PARAMETRO");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_PARAMETRO_AUTORIZACAO
	    	string[] bmDisabledTcsParametroAutorizacaoGrupoList = this.GetEDM().GetFilteringDisabledList("TCS_PARAMETRO_AUTORIZACAO");
	    	if (bmDisabledTcsParametroAutorizacaoGrupoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsParametroAutorizacaoGrupoList.Contains("TCS_PARAMETRO_AUTORIZACAO.DESC_PARAMETRO"))
	    		{
	    			result.Add("TcsParametroAutorizacaoGrupo|DescParametro");
	    			result.Add("TcsParametroAutorizacaoGrupo|TCS_PARAMETRO_AUTORIZACAO.DESC_PARAMETRO");
	    		}
	
	    		if (bmDisabledTcsParametroAutorizacaoGrupoList.Contains("TCS_PARAMETRO_AUTORIZACAO.ID_PARAMETRO"))
	    		{
	    			result.Add("TcsParametroAutorizacaoGrupo|IdParametro");
	    			result.Add("TcsParametroAutorizacaoGrupo|TCS_PARAMETRO_AUTORIZACAO.ID_PARAMETRO");
	    		}
	
	    		if (bmDisabledTcsParametroAutorizacaoGrupoList.Contains("TCS_PARAMETRO_AUTORIZACAO.TITULO_PARAMETRO"))
	    		{
	    			result.Add("TcsParametroAutorizacaoGrupo|TituloParametro");
	    			result.Add("TcsParametroAutorizacaoGrupo|TCS_PARAMETRO_AUTORIZACAO.TITULO_PARAMETRO");
	    		}
	    	}
	    	return result;
	    }

	    #endregion Excluded Filters

	    #region Get By EntitySearchId Methods Definitions.
	
				
	    [Ignore]
	    //Get TcsParametroAutorizacao By EntitySearchId.
	    public IQueryable<TcsParametroAutorizacao> GetTcsParametroAutorizacaoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsParametroAutorizacaoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsParametroTabelaSelecaoAutorizacao By EntitySearchId.
	    public IQueryable<TcsParametroTabelaSelecaoAutorizacao> GetTcsParametroTabelaSelecaoAutorizacaoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsParametroTabelaSelecaoAutorizacaoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsParametroAutorizacao By EntitySearchId.
	    public IQueryable<TcsParametroAutorizacao> GetTcsParametroAutorizacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsParametroAutorizacaoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsParametroTabelaSelecaoAutorizacao By EntitySearchId.
	    public IQueryable<TcsParametroTabelaSelecaoAutorizacao> GetTcsParametroTabelaSelecaoAutorizacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsParametroTabelaSelecaoAutorizacaoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsParametroGrupoAutorizacao By EntitySearchId.
	    public IQueryable<TcsParametroGrupoAutorizacao> GetTcsParametroGrupoAutorizacaoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsParametroGrupoAutorizacaoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsParametroAutorizacaoGrupo By EntitySearchId.
	    public IQueryable<TcsParametroAutorizacaoGrupo> GetTcsParametroAutorizacaoGrupoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsParametroAutorizacaoGrupoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsParametroGrupoAutorizacao By EntitySearchId.
	    public IQueryable<TcsParametroGrupoAutorizacao> GetTcsParametroGrupoAutorizacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsParametroGrupoAutorizacaoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsParametroAutorizacaoGrupo By EntitySearchId.
	    public IQueryable<TcsParametroAutorizacaoGrupo> GetTcsParametroAutorizacaoGrupoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsParametroAutorizacaoGrupoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get TcsParametroAutorizacao By Example.
	    [Ignore]
	    public IQueryable<TcsParametroAutorizacao> GetTcsParametroAutorizacaoByExample(TcsParametroAutorizacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsParametroAutorizacaoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsParametroTabelaSelecaoAutorizacao By Example.
	    [Ignore]
	    public IQueryable<TcsParametroTabelaSelecaoAutorizacao> GetTcsParametroTabelaSelecaoAutorizacaoByExample(TcsParametroTabelaSelecaoAutorizacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsParametroTabelaSelecaoAutorizacaoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsParametroAutorizacao By Example.
	    [Ignore]
	    public IQueryable<TcsParametroAutorizacao> GetTcsParametroAutorizacaoByExampleNoAssociations(TcsParametroAutorizacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsParametroAutorizacaoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsParametroTabelaSelecaoAutorizacao By Example.
	    [Ignore]
	    public IQueryable<TcsParametroTabelaSelecaoAutorizacao> GetTcsParametroTabelaSelecaoAutorizacaoByExampleNoAssociations(TcsParametroTabelaSelecaoAutorizacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsParametroTabelaSelecaoAutorizacaoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsParametroGrupoAutorizacao By Example.
	    [Ignore]
	    public IQueryable<TcsParametroGrupoAutorizacao> GetTcsParametroGrupoAutorizacaoByExample(TcsParametroGrupoAutorizacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsParametroGrupoAutorizacaoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsParametroAutorizacaoGrupo By Example.
	    [Ignore]
	    public IQueryable<TcsParametroAutorizacaoGrupo> GetTcsParametroAutorizacaoGrupoByExample(TcsParametroAutorizacaoGrupo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsParametroAutorizacaoGrupoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsParametroGrupoAutorizacao By Example.
	    [Ignore]
	    public IQueryable<TcsParametroGrupoAutorizacao> GetTcsParametroGrupoAutorizacaoByExampleNoAssociations(TcsParametroGrupoAutorizacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsParametroGrupoAutorizacaoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsParametroAutorizacaoGrupo By Example.
	    [Ignore]
	    public IQueryable<TcsParametroAutorizacaoGrupo> GetTcsParametroAutorizacaoGrupoByExampleNoAssociations(TcsParametroAutorizacaoGrupo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsParametroAutorizacaoGrupoByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public TcsParametroAutorizacao GetTcsParametroAutorizacaoByKey(Int64 idParametro)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsParametroAutorizacao");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdParametro"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idParametro));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsParametroAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsParametroTabelaSelecaoAutorizacao GetTcsParametroTabelaSelecaoAutorizacaoByKey(Int64 idTabelaSelecao)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsParametroTabelaSelecaoAutorizacao");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTabelaSelecao"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTabelaSelecao));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsParametroTabelaSelecaoAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsParametroGrupoAutorizacao GetTcsParametroGrupoAutorizacaoByKey(Int16 idGrupoParametro)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsParametroGrupoAutorizacao");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdGrupoParametro"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idGrupoParametro));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsParametroGrupoAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsParametroAutorizacaoGrupo GetTcsParametroAutorizacaoGrupoByKey(Int64 idParametro)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsParametroAutorizacaoGrupo");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdParametro"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idParametro));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsParametroAutorizacaoGrupoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get TcsParametroAutorizacaoByEntitySearch.
	    public IQueryable<TcsParametroAutorizacao> GetTcsParametroAutorizacaoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsParametroAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsParametroAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_PARAMETRO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.TCS_APLICATIVO
                  let entity0Al3 = entity0.TCS_TABELA_AUTORIZACAO
                  let entity0Al1 = entity0.TCS_PARAMETRO_GRUPO_AUTORIZACAO
	            
	            	
	            select new TcsParametroAutorizacao()		
	            {
	            
                ColunaCodValida = entity0.COLUNA_COD_VALIDA
                , ColunaDescValida = entity0.COLUNA_DESC_VALIDA
                , DescGrupoParametro = entity0Al1.DESC_GRUPO_PARAMETRO
                , DescParametro = entity0.DESC_PARAMETRO
                , DescricaoAplicativo = entity0Al2.DESCRICAO_APLICATIVO
                , DescTabela = entity0Al3.DESC_TABELA
                , FaixaFinal = entity0.FAIXA_FINAL
                , FaixaInicial = entity0.FAIXA_INICIAL
                , IdGrupoParametro = entity0Al1.ID_GRUPO_PARAMETRO
                , IdParametro = entity0.ID_PARAMETRO
                , IdTcsAplicativo = entity0Al2.ID_TCS_APLICATIVO
                , IndicaEnviaPdv = entity0.INDICA_ENVIA_PDV
                , IndicaParametroLinx = true
                , LxDatatypeParametro = entity0.LX_DATATYPE_PARAMETRO
                , LxDatatypeParametroName = ((entity0.LX_DATATYPE_PARAMETRO) == 2 ? "Caractere" : ((entity0.LX_DATATYPE_PARAMETRO) == 3 ? "Data" : ((entity0.LX_DATATYPE_PARAMETRO) == 4 ? "Lógico" : ((entity0.LX_DATATYPE_PARAMETRO) == 1 ? "Numérico" : ((entity0.LX_DATATYPE_PARAMETRO) == 5 ? "Senha" : "")))))
                , LxTipoValidacaoParametro = entity0.LX_TIPO_VALIDACAO_PARAMETRO
                , LxTipoValidacaoParametroName = ((entity0.LX_TIPO_VALIDACAO_PARAMETRO) == 8 ? "Sem Validação" : ((entity0.LX_TIPO_VALIDACAO_PARAMETRO) == 2 ? "Validação Contra Tabela (Combo)" : ((entity0.LX_TIPO_VALIDACAO_PARAMETRO) == 3 ? "Validação Contra Faixa" : ((entity0.LX_TIPO_VALIDACAO_PARAMETRO) == 4 ? "Validação Contra Objeto CRM" : ((entity0.LX_TIPO_VALIDACAO_PARAMETRO) == 1 ? "Validação Contra Tabela (Valida)" : "")))))
                , NivelAcesso = entity0.NIVEL_ACESSO
                , NivelAcessoEdicao = entity0.NIVEL_ACESSO_EDICAO
                , ObsParametro = entity0.OBS_PARAMETRO
                , PermiteVariacaoPorEntidade = entity0.PERMITE_VARIACAO_POR_ENTIDADE
                , TituloParametro = entity0.TITULO_PARAMETRO
                , UidTabela = entity0Al3.UID_TABELA
			
                ,TcsParametroTabelaSelecaoAutorizacaoList = 
	                        (from entity1 in entity0.TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO_LISTA
                                  let entity1Al1 = entity1.TCS_TABELA_AUTORIZACAO
                                  let entity1Al2 = entity1.TCS_PARAMETRO_AUTORIZACAO
	                        
	                        	
	                        select new TcsParametroTabelaSelecaoAutorizacao()
	                        {
	                        
                                DescTabela = entity1Al1.DESC_TABELA
                                , IdParametro = entity1Al2.ID_PARAMETRO
                                , IdTabelaSelecao = entity1.ID_TABELA_SELECAO
                                , LxParametroHierarquia = entity1.LX_PARAMETRO_HIERARQUIA
                                , LxParametroHierarquiaName = ((entity1.LX_PARAMETRO_HIERARQUIA) == 100 ? "Obrigatório" : ((entity1.LX_PARAMETRO_HIERARQUIA) == 1 ? "Variação Nível 1" : ((entity1.LX_PARAMETRO_HIERARQUIA) == 2 ? "Variação Nível 2" : ((entity1.LX_PARAMETRO_HIERARQUIA) == 3 ? "Variação Nível 3" : ((entity1.LX_PARAMETRO_HIERARQUIA) == 4 ? "Variação Nível 4" : ((entity1.LX_PARAMETRO_HIERARQUIA) == 5 ? "Variação Nível 5" : ""))))))
                                , NomeTabela = entity1Al1.NOME_TABELA
                                , UidTabela = entity1Al1.UID_TABELA
		
	                        }
	                        )
		
	            }
	            );
	
	        SetTcsParametroAutorizacaoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsParametroTabelaSelecaoAutorizacaoByEntitySearch.
	    public IQueryable<TcsParametroTabelaSelecaoAutorizacao> GetTcsParametroTabelaSelecaoAutorizacaoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsParametroTabelaSelecaoAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsParametroTabelaSelecaoAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_TABELA_AUTORIZACAO
                  let entity0Al2 = entity0.TCS_PARAMETRO_AUTORIZACAO
	            
	            	
	            select new TcsParametroTabelaSelecaoAutorizacao()		
	            {
	            
                DescTabela = entity0Al1.DESC_TABELA
                , IdParametro = entity0Al2.ID_PARAMETRO
                , IdTabelaSelecao = entity0.ID_TABELA_SELECAO
                , LxParametroHierarquia = entity0.LX_PARAMETRO_HIERARQUIA
                , LxParametroHierarquiaName = ((entity0.LX_PARAMETRO_HIERARQUIA) == 100 ? "Obrigatório" : ((entity0.LX_PARAMETRO_HIERARQUIA) == 1 ? "Variação Nível 1" : ((entity0.LX_PARAMETRO_HIERARQUIA) == 2 ? "Variação Nível 2" : ((entity0.LX_PARAMETRO_HIERARQUIA) == 3 ? "Variação Nível 3" : ((entity0.LX_PARAMETRO_HIERARQUIA) == 4 ? "Variação Nível 4" : ((entity0.LX_PARAMETRO_HIERARQUIA) == 5 ? "Variação Nível 5" : ""))))))
                , NomeTabela = entity0Al1.NOME_TABELA
                , UidTabela = entity0Al1.UID_TABELA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsParametroAutorizacaoByEntitySearchNoAssociations.
	    public IQueryable<TcsParametroAutorizacao> GetTcsParametroAutorizacaoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsParametroAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsParametroAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_PARAMETRO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.TCS_APLICATIVO
                  let entity0Al3 = entity0.TCS_TABELA_AUTORIZACAO
                  let entity0Al1 = entity0.TCS_PARAMETRO_GRUPO_AUTORIZACAO
	            
	            	
	            select new TcsParametroAutorizacao()		
	            {
	            
                ColunaCodValida = entity0.COLUNA_COD_VALIDA
                , ColunaDescValida = entity0.COLUNA_DESC_VALIDA
                , DescGrupoParametro = entity0Al1.DESC_GRUPO_PARAMETRO
                , DescParametro = entity0.DESC_PARAMETRO
                , DescricaoAplicativo = entity0Al2.DESCRICAO_APLICATIVO
                , DescTabela = entity0Al3.DESC_TABELA
                , FaixaFinal = entity0.FAIXA_FINAL
                , FaixaInicial = entity0.FAIXA_INICIAL
                , IdGrupoParametro = entity0Al1.ID_GRUPO_PARAMETRO
                , IdParametro = entity0.ID_PARAMETRO
                , IdTcsAplicativo = entity0Al2.ID_TCS_APLICATIVO
                , IndicaEnviaPdv = entity0.INDICA_ENVIA_PDV
                , IndicaParametroLinx = true
                , LxDatatypeParametro = entity0.LX_DATATYPE_PARAMETRO
                , LxDatatypeParametroName = ((entity0.LX_DATATYPE_PARAMETRO) == 2 ? "Caractere" : ((entity0.LX_DATATYPE_PARAMETRO) == 3 ? "Data" : ((entity0.LX_DATATYPE_PARAMETRO) == 4 ? "Lógico" : ((entity0.LX_DATATYPE_PARAMETRO) == 1 ? "Numérico" : ((entity0.LX_DATATYPE_PARAMETRO) == 5 ? "Senha" : "")))))
                , LxTipoValidacaoParametro = entity0.LX_TIPO_VALIDACAO_PARAMETRO
                , LxTipoValidacaoParametroName = ((entity0.LX_TIPO_VALIDACAO_PARAMETRO) == 8 ? "Sem Validação" : ((entity0.LX_TIPO_VALIDACAO_PARAMETRO) == 2 ? "Validação Contra Tabela (Combo)" : ((entity0.LX_TIPO_VALIDACAO_PARAMETRO) == 3 ? "Validação Contra Faixa" : ((entity0.LX_TIPO_VALIDACAO_PARAMETRO) == 4 ? "Validação Contra Objeto CRM" : ((entity0.LX_TIPO_VALIDACAO_PARAMETRO) == 1 ? "Validação Contra Tabela (Valida)" : "")))))
                , NivelAcesso = entity0.NIVEL_ACESSO
                , NivelAcessoEdicao = entity0.NIVEL_ACESSO_EDICAO
                , ObsParametro = entity0.OBS_PARAMETRO
                , PermiteVariacaoPorEntidade = entity0.PERMITE_VARIACAO_POR_ENTIDADE
                , TituloParametro = entity0.TITULO_PARAMETRO
                , UidTabela = entity0Al3.UID_TABELA
		
	            }
	            );
	
	        SetTcsParametroAutorizacaoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsParametroTabelaSelecaoAutorizacaoByEntitySearchNoAssociations.
	    public IQueryable<TcsParametroTabelaSelecaoAutorizacao> GetTcsParametroTabelaSelecaoAutorizacaoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsParametroTabelaSelecaoAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsParametroTabelaSelecaoAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_TABELA_AUTORIZACAO
                  let entity0Al2 = entity0.TCS_PARAMETRO_AUTORIZACAO
	            
	            	
	            select new TcsParametroTabelaSelecaoAutorizacao()		
	            {
	            
                DescTabela = entity0Al1.DESC_TABELA
                , IdParametro = entity0Al2.ID_PARAMETRO
                , IdTabelaSelecao = entity0.ID_TABELA_SELECAO
                , LxParametroHierarquia = entity0.LX_PARAMETRO_HIERARQUIA
                , LxParametroHierarquiaName = ((entity0.LX_PARAMETRO_HIERARQUIA) == 100 ? "Obrigatório" : ((entity0.LX_PARAMETRO_HIERARQUIA) == 1 ? "Variação Nível 1" : ((entity0.LX_PARAMETRO_HIERARQUIA) == 2 ? "Variação Nível 2" : ((entity0.LX_PARAMETRO_HIERARQUIA) == 3 ? "Variação Nível 3" : ((entity0.LX_PARAMETRO_HIERARQUIA) == 4 ? "Variação Nível 4" : ((entity0.LX_PARAMETRO_HIERARQUIA) == 5 ? "Variação Nível 5" : ""))))))
                , NomeTabela = entity0Al1.NOME_TABELA
                , UidTabela = entity0Al1.UID_TABELA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsParametroTabelaSelecaoAutorizacaoParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsParametroTabelaSelecaoAutorizacaoParentComposition> GetTcsParametroTabelaSelecaoAutorizacaoParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "TCS_PARAMETRO_AUTORIZACAO", "TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO", "TCS_PARAMETRO_AUTORIZACAO", typeof(TcsParametroTabelaSelecaoAutorizacaoParentComposition));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsParametroTabelaSelecaoAutorizacaoParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_TABELA_AUTORIZACAO
                  let entity0Al2 = entity0.TCS_PARAMETRO_AUTORIZACAO
	            
	            	
	            select new TcsParametroTabelaSelecaoAutorizacaoParentComposition()		
	            {
	            
                DescTabela = entity0Al1.DESC_TABELA
                , IdParametro = entity0Al2.ID_PARAMETRO
                , IdTabelaSelecao = entity0.ID_TABELA_SELECAO
                , LxParametroHierarquia = entity0.LX_PARAMETRO_HIERARQUIA
                , LxParametroHierarquiaName = ((entity0.LX_PARAMETRO_HIERARQUIA) == 100 ? "Obrigatório" : ((entity0.LX_PARAMETRO_HIERARQUIA) == 1 ? "Variação Nível 1" : ((entity0.LX_PARAMETRO_HIERARQUIA) == 2 ? "Variação Nível 2" : ((entity0.LX_PARAMETRO_HIERARQUIA) == 3 ? "Variação Nível 3" : ((entity0.LX_PARAMETRO_HIERARQUIA) == 4 ? "Variação Nível 4" : ((entity0.LX_PARAMETRO_HIERARQUIA) == 5 ? "Variação Nível 5" : ""))))))
                , NomeTabela = entity0Al1.NOME_TABELA
                , UidTabela = entity0Al1.UID_TABELA
                //TcsParametroAutorizacao Properties.
                , ColunaCodValida = entity0.TCS_PARAMETRO_AUTORIZACAO.COLUNA_COD_VALIDA
                , ColunaDescValida = entity0.TCS_PARAMETRO_AUTORIZACAO.COLUNA_DESC_VALIDA
                , DescGrupoParametro = entity0.TCS_PARAMETRO_AUTORIZACAO.TCS_PARAMETRO_GRUPO_AUTORIZACAO.DESC_GRUPO_PARAMETRO
                , DescParametro = entity0.TCS_PARAMETRO_AUTORIZACAO.DESC_PARAMETRO
                , DescricaoAplicativo = entity0.TCS_PARAMETRO_AUTORIZACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO
                , FaixaFinal = entity0.TCS_PARAMETRO_AUTORIZACAO.FAIXA_FINAL
                , FaixaInicial = entity0.TCS_PARAMETRO_AUTORIZACAO.FAIXA_INICIAL
                , IdGrupoParametro = entity0.TCS_PARAMETRO_AUTORIZACAO.TCS_PARAMETRO_GRUPO_AUTORIZACAO.ID_GRUPO_PARAMETRO
                , IdTcsAplicativo = entity0.TCS_PARAMETRO_AUTORIZACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO
                , IndicaEnviaPdv = entity0.TCS_PARAMETRO_AUTORIZACAO.INDICA_ENVIA_PDV
                , IndicaParametroLinx = true
                , LxDatatypeParametro = entity0.TCS_PARAMETRO_AUTORIZACAO.LX_DATATYPE_PARAMETRO
                , LxDatatypeParametroName = ((entity0.TCS_PARAMETRO_AUTORIZACAO.LX_DATATYPE_PARAMETRO) == 2 ? "Caractere" : ((entity0.TCS_PARAMETRO_AUTORIZACAO.LX_DATATYPE_PARAMETRO) == 3 ? "Data" : ((entity0.TCS_PARAMETRO_AUTORIZACAO.LX_DATATYPE_PARAMETRO) == 4 ? "Lógico" : ((entity0.TCS_PARAMETRO_AUTORIZACAO.LX_DATATYPE_PARAMETRO) == 1 ? "Numérico" : ((entity0.TCS_PARAMETRO_AUTORIZACAO.LX_DATATYPE_PARAMETRO) == 5 ? "Senha" : "")))))
                , LxTipoValidacaoParametro = entity0.TCS_PARAMETRO_AUTORIZACAO.LX_TIPO_VALIDACAO_PARAMETRO
                , LxTipoValidacaoParametroName = ((entity0.TCS_PARAMETRO_AUTORIZACAO.LX_TIPO_VALIDACAO_PARAMETRO) == 8 ? "Sem Validação" : ((entity0.TCS_PARAMETRO_AUTORIZACAO.LX_TIPO_VALIDACAO_PARAMETRO) == 2 ? "Validação Contra Tabela (Combo)" : ((entity0.TCS_PARAMETRO_AUTORIZACAO.LX_TIPO_VALIDACAO_PARAMETRO) == 3 ? "Validação Contra Faixa" : ((entity0.TCS_PARAMETRO_AUTORIZACAO.LX_TIPO_VALIDACAO_PARAMETRO) == 4 ? "Validação Contra Objeto CRM" : ((entity0.TCS_PARAMETRO_AUTORIZACAO.LX_TIPO_VALIDACAO_PARAMETRO) == 1 ? "Validação Contra Tabela (Valida)" : "")))))
                , NivelAcesso = entity0.TCS_PARAMETRO_AUTORIZACAO.NIVEL_ACESSO
                , NivelAcessoEdicao = entity0.TCS_PARAMETRO_AUTORIZACAO.NIVEL_ACESSO_EDICAO
                , ObsParametro = entity0.TCS_PARAMETRO_AUTORIZACAO.OBS_PARAMETRO
                , PermiteVariacaoPorEntidade = entity0.TCS_PARAMETRO_AUTORIZACAO.PERMITE_VARIACAO_POR_ENTIDADE
                , TituloParametro = entity0.TCS_PARAMETRO_AUTORIZACAO.TITULO_PARAMETRO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
	
	    [Ignore()]
	    private void SetTcsParametroAutorizacaoBusinessFilter(ref IQueryable<TcsParametroAutorizacao> query, List<EntitySearch> entitySearchList)
	    {
	    		int idxElement;
	    		string operatorValue;
	    		object value;
	    		//Get query by functions
	    		if (entitySearchList.Count > 0)
	    		{
	    			foreach (EntitySearch search in entitySearchList.Where(e => e.EntityName == "TcsParametroAutorizacao"))
	    			{

	
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "IndicaParametroLinx" || e.Value.ToString() == "true")))
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
	    										Boolean tmpIndicaParametroLinx1 = (Boolean)value;
	    										query = from r in query where r.IndicaParametroLinx == tmpIndicaParametroLinx1 select r;
	    										break;
	    									case "!=":
	    										Boolean tmpIndicaParametroLinx2 = (Boolean)value;
	    										query = from r in query where r.IndicaParametroLinx != tmpIndicaParametroLinx2 select r;
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
	    //Get TcsParametroGrupoAutorizacaoByEntitySearch.
	    public IQueryable<TcsParametroGrupoAutorizacao> GetTcsParametroGrupoAutorizacaoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsParametroGrupoAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsParametroGrupoAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_PARAMETRO_GRUPO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsParametroGrupoAutorizacao()		
	            {
	            
                DescGrupoParametro = entity0.DESC_GRUPO_PARAMETRO
                , IdGrupoParametro = entity0.ID_GRUPO_PARAMETRO
			
                ,TcsParametroAutorizacaoGrupoList = 
	                        (from entity1 in entity0.TCS_PARAMETRO_AUTORIZACAO_LISTA
                                  let entity1Al1 = entity1.TCS_PARAMETRO_GRUPO_AUTORIZACAO
	                        
	                        	
	                        select new TcsParametroAutorizacaoGrupo()
	                        {
	                        
                                DescParametro = entity1.DESC_PARAMETRO
                                , IdGrupoParametro = entity1Al1.ID_GRUPO_PARAMETRO
                                , IdParametro = entity1.ID_PARAMETRO
                                , TituloParametro = entity1.TITULO_PARAMETRO
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsParametroAutorizacaoGrupoByEntitySearch.
	    public IQueryable<TcsParametroAutorizacaoGrupo> GetTcsParametroAutorizacaoGrupoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsParametroAutorizacaoGrupo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsParametroAutorizacaoGrupo> result = 
	            (from entity0 in this.DbContext.TCS_PARAMETRO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_PARAMETRO_GRUPO_AUTORIZACAO
	            
	            	
	            select new TcsParametroAutorizacaoGrupo()		
	            {
	            
                DescParametro = entity0.DESC_PARAMETRO
                , IdGrupoParametro = entity0Al1.ID_GRUPO_PARAMETRO
                , IdParametro = entity0.ID_PARAMETRO
                , TituloParametro = entity0.TITULO_PARAMETRO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsParametroGrupoAutorizacaoByEntitySearchNoAssociations.
	    public IQueryable<TcsParametroGrupoAutorizacao> GetTcsParametroGrupoAutorizacaoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsParametroGrupoAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsParametroGrupoAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_PARAMETRO_GRUPO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsParametroGrupoAutorizacao()		
	            {
	            
                DescGrupoParametro = entity0.DESC_GRUPO_PARAMETRO
                , IdGrupoParametro = entity0.ID_GRUPO_PARAMETRO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsParametroAutorizacaoGrupoByEntitySearchNoAssociations.
	    public IQueryable<TcsParametroAutorizacaoGrupo> GetTcsParametroAutorizacaoGrupoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsParametroAutorizacaoGrupo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsParametroAutorizacaoGrupo> result = 
	            (from entity0 in this.DbContext.TCS_PARAMETRO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_PARAMETRO_GRUPO_AUTORIZACAO
	            
	            	
	            select new TcsParametroAutorizacaoGrupo()		
	            {
	            
                DescParametro = entity0.DESC_PARAMETRO
                , IdGrupoParametro = entity0Al1.ID_GRUPO_PARAMETRO
                , IdParametro = entity0.ID_PARAMETRO
                , TituloParametro = entity0.TITULO_PARAMETRO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsParametroAutorizacaoGrupoParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsParametroAutorizacaoGrupoParentComposition> GetTcsParametroAutorizacaoGrupoParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "TCS_PARAMETRO_GRUPO_AUTORIZACAO", "TCS_PARAMETRO_AUTORIZACAO", "TCS_PARAMETRO_GRUPO_AUTORIZACAO", typeof(TcsParametroAutorizacaoGrupoParentComposition));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsParametroAutorizacaoGrupoParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_PARAMETRO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_PARAMETRO_GRUPO_AUTORIZACAO
	            
	            	
	            select new TcsParametroAutorizacaoGrupoParentComposition()		
	            {
	            
                DescParametro = entity0.DESC_PARAMETRO
                , IdGrupoParametro = entity0Al1.ID_GRUPO_PARAMETRO
                , IdParametro = entity0.ID_PARAMETRO
                , TituloParametro = entity0.TITULO_PARAMETRO
                //TcsParametroGrupoAutorizacao Properties.
                , DescGrupoParametro = entity0.TCS_PARAMETRO_GRUPO_AUTORIZACAO.DESC_GRUPO_PARAMETRO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get PagedTcsParametroAutorizacao.
	    public IQueryable<TcsParametroAutorizacao> GetPagedTcsParametroAutorizacao(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsParametroAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsParametroAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_PARAMETRO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.TCS_APLICATIVO
                  let entity0Al3 = entity0.TCS_TABELA_AUTORIZACAO
                  let entity0Al1 = entity0.TCS_PARAMETRO_GRUPO_AUTORIZACAO
                orderby entity0.ID_PARAMETRO ascending
	            
	            	
	            select new TcsParametroAutorizacao()		
	            {
	            
                ColunaCodValida = entity0.COLUNA_COD_VALIDA
                , ColunaDescValida = entity0.COLUNA_DESC_VALIDA
                , DescGrupoParametro = entity0Al1.DESC_GRUPO_PARAMETRO
                , DescParametro = entity0.DESC_PARAMETRO
                , DescricaoAplicativo = entity0Al2.DESCRICAO_APLICATIVO
                , DescTabela = entity0Al3.DESC_TABELA
                , FaixaFinal = entity0.FAIXA_FINAL
                , FaixaInicial = entity0.FAIXA_INICIAL
                , IdGrupoParametro = entity0Al1.ID_GRUPO_PARAMETRO
                , IdParametro = entity0.ID_PARAMETRO
                , IdTcsAplicativo = entity0Al2.ID_TCS_APLICATIVO
                , IndicaEnviaPdv = entity0.INDICA_ENVIA_PDV
                , IndicaParametroLinx = true
                , LxDatatypeParametro = entity0.LX_DATATYPE_PARAMETRO
                , LxDatatypeParametroName = ((entity0.LX_DATATYPE_PARAMETRO) == 2 ? "Caractere" : ((entity0.LX_DATATYPE_PARAMETRO) == 3 ? "Data" : ((entity0.LX_DATATYPE_PARAMETRO) == 4 ? "Lógico" : ((entity0.LX_DATATYPE_PARAMETRO) == 1 ? "Numérico" : ((entity0.LX_DATATYPE_PARAMETRO) == 5 ? "Senha" : "")))))
                , LxTipoValidacaoParametro = entity0.LX_TIPO_VALIDACAO_PARAMETRO
                , LxTipoValidacaoParametroName = ((entity0.LX_TIPO_VALIDACAO_PARAMETRO) == 8 ? "Sem Validação" : ((entity0.LX_TIPO_VALIDACAO_PARAMETRO) == 2 ? "Validação Contra Tabela (Combo)" : ((entity0.LX_TIPO_VALIDACAO_PARAMETRO) == 3 ? "Validação Contra Faixa" : ((entity0.LX_TIPO_VALIDACAO_PARAMETRO) == 4 ? "Validação Contra Objeto CRM" : ((entity0.LX_TIPO_VALIDACAO_PARAMETRO) == 1 ? "Validação Contra Tabela (Valida)" : "")))))
                , NivelAcesso = entity0.NIVEL_ACESSO
                , NivelAcessoEdicao = entity0.NIVEL_ACESSO_EDICAO
                , ObsParametro = entity0.OBS_PARAMETRO
                , PermiteVariacaoPorEntidade = entity0.PERMITE_VARIACAO_POR_ENTIDADE
                , TituloParametro = entity0.TITULO_PARAMETRO
                , UidTabela = entity0Al3.UID_TABELA
		
	            }
	            ).Skip(skip).Take(take);
	
	        SetTcsParametroAutorizacaoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsParametroTabelaSelecaoAutorizacao.
	    public IQueryable<TcsParametroTabelaSelecaoAutorizacao> GetPagedTcsParametroTabelaSelecaoAutorizacao(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsParametroTabelaSelecaoAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsParametroTabelaSelecaoAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_TABELA_AUTORIZACAO
                  let entity0Al2 = entity0.TCS_PARAMETRO_AUTORIZACAO
                orderby entity0.ID_TABELA_SELECAO ascending
	            
	            	
	            select new TcsParametroTabelaSelecaoAutorizacao()		
	            {
	            
                DescTabela = entity0Al1.DESC_TABELA
                , IdParametro = entity0Al2.ID_PARAMETRO
                , IdTabelaSelecao = entity0.ID_TABELA_SELECAO
                , LxParametroHierarquia = entity0.LX_PARAMETRO_HIERARQUIA
                , LxParametroHierarquiaName = ((entity0.LX_PARAMETRO_HIERARQUIA) == 100 ? "Obrigatório" : ((entity0.LX_PARAMETRO_HIERARQUIA) == 1 ? "Variação Nível 1" : ((entity0.LX_PARAMETRO_HIERARQUIA) == 2 ? "Variação Nível 2" : ((entity0.LX_PARAMETRO_HIERARQUIA) == 3 ? "Variação Nível 3" : ((entity0.LX_PARAMETRO_HIERARQUIA) == 4 ? "Variação Nível 4" : ((entity0.LX_PARAMETRO_HIERARQUIA) == 5 ? "Variação Nível 5" : ""))))))
                , NomeTabela = entity0Al1.NOME_TABELA
                , UidTabela = entity0Al1.UID_TABELA
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsParametroAutorizacaoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsParametroAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_PARAMETRO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entityAl2 = entity.TCS_APLICATIVO
                  let entityAl3 = entity.TCS_TABELA_AUTORIZACAO
                  let entityAl1 = entity.TCS_PARAMETRO_GRUPO_AUTORIZACAO
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsParametroTabelaSelecaoAutorizacaoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsParametroTabelaSelecaoAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_PARAMETRO_TABELA_SELECAO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_TABELA_AUTORIZACAO
                  let entityAl2 = entity.TCS_PARAMETRO_AUTORIZACAO
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsParametroGrupoAutorizacao.
	    public IQueryable<TcsParametroGrupoAutorizacao> GetPagedTcsParametroGrupoAutorizacao(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsParametroGrupoAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsParametroGrupoAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_PARAMETRO_GRUPO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_GRUPO_PARAMETRO ascending
	            
	            	
	            select new TcsParametroGrupoAutorizacao()		
	            {
	            
                DescGrupoParametro = entity0.DESC_GRUPO_PARAMETRO
                , IdGrupoParametro = entity0.ID_GRUPO_PARAMETRO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsParametroAutorizacaoGrupo.
	    public IQueryable<TcsParametroAutorizacaoGrupo> GetPagedTcsParametroAutorizacaoGrupo(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsParametroAutorizacaoGrupo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsParametroAutorizacaoGrupo> result = 
	            (from entity0 in this.DbContext.TCS_PARAMETRO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_PARAMETRO_GRUPO_AUTORIZACAO
                orderby entity0.ID_PARAMETRO ascending
	            
	            	
	            select new TcsParametroAutorizacaoGrupo()		
	            {
	            
                DescParametro = entity0.DESC_PARAMETRO
                , IdGrupoParametro = entity0Al1.ID_GRUPO_PARAMETRO
                , IdParametro = entity0.ID_PARAMETRO
                , TituloParametro = entity0.TITULO_PARAMETRO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsParametroGrupoAutorizacaoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsParametroGrupoAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_PARAMETRO_GRUPO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsParametroAutorizacaoGrupoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsParametroAutorizacaoGrupo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_PARAMETRO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_PARAMETRO_GRUPO_AUTORIZACAO
	            
	            select 1
	            ).Count();	
		
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    
	    [Update()]	
	    //Update TcsParametroAutorizacao.
	    public void UpdateTcsParametroAutorizacao(TcsParametroAutorizacao entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsParametroAutorizacao.
	    public void InsertTcsParametroAutorizacao(TcsParametroAutorizacao entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsParametroAutorizacao.
	    public void DeleteTcsParametroAutorizacao(TcsParametroAutorizacao entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsParametroTabelaSelecaoAutorizacao.
	    public void UpdateTcsParametroTabelaSelecaoAutorizacao(TcsParametroTabelaSelecaoAutorizacao entity)
	    {



	
	        if (entity.TcsParametroAutorizacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsParametroAutorizacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsParametroAutorizacao); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsParametroTabelaSelecaoAutorizacao.
	    public void InsertTcsParametroTabelaSelecaoAutorizacao(TcsParametroTabelaSelecaoAutorizacao entity)
	    {



	
	        if (entity.TcsParametroAutorizacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsParametroAutorizacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsParametroAutorizacao);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsParametroTabelaSelecaoAutorizacao.
	    public void DeleteTcsParametroTabelaSelecaoAutorizacao(TcsParametroTabelaSelecaoAutorizacao entity)
	    {



	
	        if (entity.TcsParametroAutorizacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsParametroAutorizacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsParametroAutorizacao);
	            

	
	        }

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsParametroGrupoAutorizacao.
	    public void UpdateTcsParametroGrupoAutorizacao(TcsParametroGrupoAutorizacao entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsParametroGrupoAutorizacao.
	    public void InsertTcsParametroGrupoAutorizacao(TcsParametroGrupoAutorizacao entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsParametroGrupoAutorizacao.
	    public void DeleteTcsParametroGrupoAutorizacao(TcsParametroGrupoAutorizacao entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsParametroAutorizacaoGrupo.
	    public void UpdateTcsParametroAutorizacaoGrupo(TcsParametroAutorizacaoGrupo entity)
	    {



	
	        if (entity.TcsParametroGrupoAutorizacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsParametroGrupoAutorizacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsParametroGrupoAutorizacao); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsParametroAutorizacaoGrupo.
	    public void InsertTcsParametroAutorizacaoGrupo(TcsParametroAutorizacaoGrupo entity)
	    {



	
	        if (entity.TcsParametroGrupoAutorizacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsParametroGrupoAutorizacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsParametroGrupoAutorizacao);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsParametroAutorizacaoGrupo.
	    public void DeleteTcsParametroAutorizacaoGrupo(TcsParametroAutorizacaoGrupo entity)
	    {



	
	        if (entity.TcsParametroGrupoAutorizacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsParametroGrupoAutorizacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsParametroGrupoAutorizacao);
	            

	
	        }

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}