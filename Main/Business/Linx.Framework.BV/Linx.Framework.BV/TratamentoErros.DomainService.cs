					
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

namespace Linx.Framework.BV.TratamentoErros
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TcsLogErrosDash.EntityUniqueKey", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsLogErrosDash,TcsLogErrosDash.LogFile,TcsLogErrosDash.TcsLogErros];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];Entities[TCS_LOG_ERROS:IdTcsLogErros|TCS_APLICACAO:IdAplicacao|TCS_EMPRESA_AUTENTICACAO:IdLinxEmpresa|TCS_EMPRESA_AUTENTICACAO:IdLinxGpecon|TCS_AMBIENTE:IdTcsAmbiente];SubQueryInfo[];EdmEntityName[TCS_LOG_ERROS];EntityRelations[TCS_AMBIENTE(TCS_AMBIENTE)#TCS_APLICACAO(TCS_APLICACAO)#TCS_APLICATIVO(TCS_APLICATIVO)#TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)#GPECON(TCS_EMPRESA_AUTENTICACAO)#TCS_USUARIO_AUTENTICACAO(TCS_USUARIO_AUTENTICACAO)];EdmParentEntityName[];IsIQueryable[false]")]
		
	[DataContract(IsReference = false, Name = "TcsLogErrosDash")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.TratamentoErros.TcsLogErrosDash")]
	public partial class TcsLogErrosDash : Linx.Data.Entity
	{

	

	    public static IEnumerable<TcsLogErrosDash> OnSearchingReplacement(Linx.Framework.Autorizacao.BM.AutorizacaoContext context, string dynQuery, List<ObjectParameter> parameters, List<EntitySearch> entitySearchList)
	    {
	    		List<TcsLogErrosDash> result = new List<TcsLogErrosDash>() { new TcsLogErrosDash() };
	    		result[0].CopyFromSearch(entitySearchList);
	    		return result;
	    }
		
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.LogFileList != null && this.LogFileList.Count() > 0)
	      {
	         foreach (var entity in this.LogFileList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      if (this.TcsLogErrosList != null && this.TcsLogErrosList.Count() > 0)
	      {
	         foreach (var entity in this.TcsLogErrosList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.LogFileList != null)
	      {
	         foreach (var detail in this.LogFileList)
	         {
	            detail.ResetDetails();
	         }
	         this.LogFileList = null;
	      }
	      if (this.TcsLogErrosList != null)
	      {
	         foreach (var detail in this.TcsLogErrosList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsLogErrosList = null;
	      }
	    }

	    public virtual void ResetChangeState()
	    {
	      this.ChangeState = "N";
	      if (this.LogFileList != null)
	      {
	         foreach (var detail in this.LogFileList.ToArray())
	         {
	            detail.ResetChangeState();
	         }
	      }
	      if (this.TcsLogErrosList != null)
	      {
	         foreach (var detail in this.TcsLogErrosList.ToArray())
	         {
	            detail.ResetChangeState();
	         }
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(TratamentoErrosDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("LogFile"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load LogFile and all sub-details
	         if (this.LogFileList == null || this.LogFileList.Count() == 0)
	         {
	             if (take > 0)
	                 this.LogFileList = context.GetPagedLogFile(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.LogFileList = (from r in context.GetLogFileByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	      if (viewNames == null || viewNames.Contains("TcsLogErros"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsLogErros and all sub-details
	         if (this.TcsLogErrosList == null || this.TcsLogErrosList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsLogErrosList = context.GetPagedTcsLogErros(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsLogErrosList = (from r in context.GetTcsLogErrosByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _LogFileElements = changeSet.ChangeSetEntries.Where(e => e.Entity is LogFile && ((LogFile)e.Entity).TcsLogErrosDash == null && e.Associations == null && e.OriginalAssociations == null).ToList();
 	      if (_LogFileElements.Count > 0 && this.LogFileList.Count() == 0)
 	      {
 	          this.LogFileList = _LogFileElements.Select(e => (LogFile)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _LogFileElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((LogFile)detail.Entity).TcsLogErrosDash = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsLogErrosDash", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("LogFileList", indexDetails.ToArray());
 	      }
 
 	      var _TcsLogErrosElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsLogErros && ((TcsLogErros)e.Entity).TcsLogErrosDash == null && e.Associations == null && e.OriginalAssociations == null).ToList();
 	      if (_TcsLogErrosElements.Count > 0 && this.TcsLogErrosList.Count() == 0)
 	      {
 	          this.TcsLogErrosList = _TcsLogErrosElements.Select(e => (TcsLogErros)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsLogErrosElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsLogErros)detail.Entity).TcsLogErrosDash = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsLogErrosDash", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsLogErrosList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For DataErro
	    partial void OnDataErroChanging(System.DateTime value);
	    partial void OnDataErroChanged();

	    private System.DateTime _DataErro;

	    [DataMember(IsRequired = true, Name = "DataErro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Data", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_LOG_ERROS.DATA_ERRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_LOG_ERROS.DATA_ERRO")]
	    public System.DateTime DataErro
	    {
	    	    get
	    	    {
	    	          return _DataErro;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataErro != value)
	    	          {
	    	              this.ValidateProperty("DataErro", value);
	    	              this.OnDataErroChanging(value);
	    	              this.RaiseDataMemberChanging("DataErro");
	    	              this._DataErro = value;
	    	              this.RaiseDataMemberChanged("DataErro");
	    	              this.OnDataErroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescricaoAmbiente
	    partial void OnDescricaoAmbienteChanging(System.String value);
	    partial void OnDescricaoAmbienteChanged();

	    private System.String _DescricaoAmbiente;

	    [DataMember(Name = "DescricaoAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ambiente", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente];LookUpTitle[Seleção de (Ambiente)];LookUpQuery[executeLookUpTcsAmbiente];LookUpFinalize[finalizeLookUpTcsAmbiente];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Descricao Ambiente\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"IdTcsAmbiente\" : true}];FilterDataKey[TCS_LOG_ERROS.TCS_AMBIENTE.DESCRICAO_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoAmbiente#false##250:0##Descricao Ambiente#0#true##::LookUpTcsAmbiente##false#false#TCS_AMBIENTE#TCS_AMBIENTE#Linx.Framework.BV.TratamentoErros#IQueryable###true#false", EdmKey="TCS_LOG_ERROS.TCS_AMBIENTE.DESCRICAO_AMBIENTE")]
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

	    [DataMember(Name = "DescricaoAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicação", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicacao];LookUpTitle[Seleção de (Aplicação)];LookUpQuery[executeLookUpTcsAplicacao];LookUpFinalize[finalizeLookUpTcsAplicacao];LookUpDisplayColumns[{\"DescricaoAplicacao\" : \"Descricao Aplicacao\", \"IdAplicacao\" : \"Id Aplicacao\"}];LookUpColumns[{\"DescricaoAplicacao\" : true, \"IdAplicacao\" : true}];FilterDataKey[TCS_LOG_ERROS.TCS_APLICACAO.DESCRICAO_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoAplicacao#false##60:0##Descricao Aplicacao#0#true##::LookUpTcsAplicacao##false#false#TCS_APLICACAO#TCS_APLICACAO#Linx.Framework.BV.TratamentoErros#IQueryable###true#false", EdmKey="TCS_LOG_ERROS.TCS_APLICACAO.DESCRICAO_APLICACAO")]
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
	    //Extensibility Partial Method Definitions For EnderecoWeb
	    partial void OnEnderecoWebChanging(System.String value);
	    partial void OnEnderecoWebChanged();

	    private System.String _EnderecoWeb;

	    [DataMember(IsRequired = true, Name = "EnderecoWeb", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Endereço Web", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(8000)]
	    [FunctionalPoint("Precision[8000:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_LOG_ERROS.ENDERECO_WEB];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_LOG_ERROS.ENDERECO_WEB")]
	    public System.String EnderecoWeb
	    {
	    	    get
	    	    {
	    	          return _EnderecoWeb;
	    	    }
	    	    set
	    	    {
	    	          if (this._EnderecoWeb != value)
	    	          {
	    	              this.ValidateProperty("EnderecoWeb", value);
	    	              this.OnEnderecoWebChanging(value);
	    	              this.RaiseDataMemberChanging("EnderecoWeb");
	    	              this._EnderecoWeb = value;
	    	              this.RaiseDataMemberChanged("EnderecoWeb");
	    	              this.OnEnderecoWebChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Gpecon
	    partial void OnGpeconChanging(System.String value);
	    partial void OnGpeconChanged();

	    private System.String _Gpecon;

	    [DataMember(Name = "Gpecon", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Grupo Econômico", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpGpecon];LookUpTitle[Seleção de (Grupo Econômico)];LookUpQuery[executeLookUpGpecon];LookUpFinalize[finalizeLookUpGpecon];LookUpDisplayColumns[{\"IdLinxGpecon\" : \"Id Linx Gpecon\", \"Gpecon\" : \"Grupo Economico\"}];LookUpColumns[{\"IdLinxGpecon\" : true, \"Gpecon\" : true}];FilterDataKey[TCS_LOG_ERROS.GPECON.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#Gpecon#false##250:0##Grupo Economico#1#true##::LookUpGpecon##false#false#GPECON#TCS_EMPRESA_AUTENTICACAO#Linx.Framework.BV.TratamentoErros#IQueryable###true#false", EdmKey="TCS_LOG_ERROS.GPECON.NOME_EMPRESA")]
	    public System.String Gpecon
	    {
	    	    get
	    	    {
	    	          return _Gpecon;
	    	    }
	    	    set
	    	    {
	    	          if (this._Gpecon != value)
	    	          {
	    	              this.ValidateProperty("Gpecon", value);
	    	              this.OnGpeconChanging(value);
	    	              this.RaiseDataMemberChanging("Gpecon");
	    	              this._Gpecon = value;
	    	              this.RaiseDataMemberChanged("Gpecon");
	    	              this.OnGpeconChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdAplicacao
	    partial void OnIdAplicacaoChanging(System.Nullable<Int32> value);
	    partial void OnIdAplicacaoChanged();

	    private System.Nullable<Int32> _IdAplicacao;

	    [DataMember(Name = "IdAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Aplicacao", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicacao];LookUpTitle[Seleção de (Id Aplicacao)];LookUpQuery[executeLookUpTcsAplicacao];LookUpFinalize[finalizeLookUpTcsAplicacao];LookUpDisplayColumns[{\"DescricaoAplicacao\" : \"Descricao Aplicacao\", \"IdAplicacao\" : \"Id Aplicacao\"}];LookUpColumns[{\"DescricaoAplicacao\" : true, \"IdAplicacao\" : true}];FilterDataKey[TCS_LOG_ERROS.TCS_APLICACAO.ID_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<Int32>#IdAplicacao#true##12:0##Id Aplicacao#1#true##::LookUpTcsAplicacao##false#false#TCS_APLICACAO#TCS_APLICACAO#Linx.Framework.BV.TratamentoErros#IQueryable###true#false", EdmKey="TCS_LOG_ERROS.TCS_APLICACAO.ID_APLICACAO")]
	    public System.Nullable<Int32> IdAplicacao
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
	    //Extensibility Partial Method Definitions For IdLinxEmpresa
	    partial void OnIdLinxEmpresaChanging(System.Nullable<Int32> value);
	    partial void OnIdLinxEmpresaChanged();

	    private System.Nullable<Int32> _IdLinxEmpresa;

	    [DataMember(Name = "IdLinxEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx Empresa", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsEmpresaAutenticacao];LookUpTitle[Seleção de (Id Linx Empresa)];LookUpQuery[executeLookUpTcsEmpresaAutenticacao];LookUpFinalize[finalizeLookUpTcsEmpresaAutenticacao];LookUpDisplayColumns[{\"IdLinxEmpresa\" : \"Id Linx Empresa\", \"NomeEmpresa\" : \"Nome Empresa\"}];LookUpColumns[{\"IdLinxEmpresa\" : true, \"NomeEmpresa\" : true}];FilterDataKey[TCS_LOG_ERROS.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<Int32>#IdLinxEmpresa#true##12:0##Id Linx Empresa#0#true##::LookUpTcsEmpresaAutenticacao##false#false#TCS_EMPRESA_AUTENTICACAO#TCS_EMPRESA_AUTENTICACAO#Linx.Framework.BV.TratamentoErros#IQueryable###true#false", EdmKey="TCS_LOG_ERROS.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
	    public System.Nullable<Int32> IdLinxEmpresa
	    {
	    	    get
	    	    {
	    	          return _IdLinxEmpresa;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLinxEmpresa != value)
	    	          {
	    	              this.ValidateProperty("IdLinxEmpresa", value);
	    	              this.OnIdLinxEmpresaChanging(value);
	    	              this.RaiseDataMemberChanging("IdLinxEmpresa");
	    	              this._IdLinxEmpresa = value;
	    	              this.RaiseDataMemberChanged("IdLinxEmpresa");
	    	              this.OnIdLinxEmpresaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLinxGpecon
	    partial void OnIdLinxGpeconChanging(System.Nullable<Int32> value);
	    partial void OnIdLinxGpeconChanged();

	    private System.Nullable<Int32> _IdLinxGpecon;

	    [DataMember(Name = "IdLinxGpecon", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx Gpecon", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpGpecon];LookUpTitle[Seleção de (Id Linx Gpecon)];LookUpQuery[executeLookUpGpecon];LookUpFinalize[finalizeLookUpGpecon];LookUpDisplayColumns[{\"IdLinxGpecon\" : \"Id Linx Gpecon\", \"Gpecon\" : \"Grupo Economico\"}];LookUpColumns[{\"IdLinxGpecon\" : true, \"Gpecon\" : true}];FilterDataKey[TCS_LOG_ERROS.GPECON.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<Int32>#IdLinxGpecon#true##12:0##Id Linx Gpecon#0#true##::LookUpGpecon##false#false#GPECON#TCS_EMPRESA_AUTENTICACAO#Linx.Framework.BV.TratamentoErros#IQueryable###true#false", EdmKey="TCS_LOG_ERROS.GPECON.ID_LINX")]
	    public System.Nullable<Int32> IdLinxGpecon
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
	    //Extensibility Partial Method Definitions For IdTcsAmbiente
	    partial void OnIdTcsAmbienteChanging(System.Nullable<Int32> value);
	    partial void OnIdTcsAmbienteChanged();

	    private System.Nullable<Int32> _IdTcsAmbiente;

	    [DataMember(Name = "IdTcsAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente];LookUpTitle[Seleção de (Id Tcs Ambiente)];LookUpQuery[executeLookUpTcsAmbiente];LookUpFinalize[finalizeLookUpTcsAmbiente];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Descricao Ambiente\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"IdTcsAmbiente\" : true}];FilterDataKey[TCS_LOG_ERROS.TCS_AMBIENTE.ID_TCS_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<Int32>#IdTcsAmbiente#true##12:0##Id Tcs Ambiente#1#true##::LookUpTcsAmbiente##false#false#TCS_AMBIENTE#TCS_AMBIENTE#Linx.Framework.BV.TratamentoErros#IQueryable###true#false", EdmKey="TCS_LOG_ERROS.TCS_AMBIENTE.ID_TCS_AMBIENTE")]
	    public System.Nullable<Int32> IdTcsAmbiente
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
	    //Extensibility Partial Method Definitions For IdTcsLogErros
	    partial void OnIdTcsLogErrosChanging(Int32 value);
	    partial void OnIdTcsLogErrosChanged();

	    private Int32 _IdTcsLogErros;

	    [DataMember(IsRequired = true, Name = "IdTcsLogErros", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_LOG_ERROS.ID_TCS_LOG_ERROS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_LOG_ERROS.ID_TCS_LOG_ERROS")]
	    public Int32 IdTcsLogErros
	    {
	    	    get
	    	    {
	    	          return _IdTcsLogErros;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsLogErros != value)
	    	          {
	    	              this.ValidateProperty("IdTcsLogErros", value);
	    	              this.OnIdTcsLogErrosChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsLogErros");
	    	              this._IdTcsLogErros = value;
	    	              this.RaiseDataMemberChanged("IdTcsLogErros");
	    	              this.OnIdTcsLogErrosChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuario
	    partial void OnIdUsuarioChanging(System.Nullable<Int64> value);
	    partial void OnIdUsuarioChanged();

	    private System.Nullable<Int64> _IdUsuario;

	    [DataMember(Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacao];LookUpTitle[Seleção de (Id Usuario)];LookUpQuery[executeLookUpTcsUsuarioAutenticacao];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacao];LookUpDisplayColumns[{\"IdUsuario\" : \"Id Usuario\", \"NomeAutenticacao\" : \"Usuário Autenticação\", \"NomeUsuario\" : \"Nome\"}];LookUpColumns[{\"IdUsuario\" : true, \"NomeAutenticacao\" : true, \"NomeUsuario\" : true}];FilterDataKey[TCS_LOG_ERROS.TCS_USUARIO_AUTENTICACAO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<Int64>#IdUsuario#true##24:0##Id Usuario#0#true##::LookUpTcsUsuarioAutenticacao##false#false#TCS_USUARIO_AUTENTICACAO#TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.TratamentoErros#IQueryable###true#false", EdmKey="TCS_LOG_ERROS.TCS_USUARIO_AUTENTICACAO.ID_USUARIO")]
	    public System.Nullable<Int64> IdUsuario
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
	    //Extensibility Partial Method Definitions For MensagemExcecao
	    partial void OnMensagemExcecaoChanging(System.String value);
	    partial void OnMensagemExcecaoChanged();

	    private System.String _MensagemExcecao;

	    [DataMember(IsRequired = true, Name = "MensagemExcecao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Exceção", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(1000)]
	    [FunctionalPoint("Precision[1000:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_LOG_ERROS.MENSAGEM_EXCECAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_LOG_ERROS.MENSAGEM_EXCECAO")]
	    public System.String MensagemExcecao
	    {
	    	    get
	    	    {
	    	          return _MensagemExcecao;
	    	    }
	    	    set
	    	    {
	    	          if (this._MensagemExcecao != value)
	    	          {
	    	              this.ValidateProperty("MensagemExcecao", value);
	    	              this.OnMensagemExcecaoChanging(value);
	    	              this.RaiseDataMemberChanging("MensagemExcecao");
	    	              this._MensagemExcecao = value;
	    	              this.RaiseDataMemberChanged("MensagemExcecao");
	    	              this.OnMensagemExcecaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For MensagemExcecaoInterna
	    partial void OnMensagemExcecaoInternaChanging(System.String value);
	    partial void OnMensagemExcecaoInternaChanged();

	    private System.String _MensagemExcecaoInterna;

	    [DataMember(Name = "MensagemExcecaoInterna", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Exceção Interna", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(1000)]
	    [FunctionalPoint("Precision[1000:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_LOG_ERROS.MENSAGEM_EXCECAO_INTERNA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_LOG_ERROS.MENSAGEM_EXCECAO_INTERNA")]
	    public System.String MensagemExcecaoInterna
	    {
	    	    get
	    	    {
	    	          return _MensagemExcecaoInterna;
	    	    }
	    	    set
	    	    {
	    	          if (this._MensagemExcecaoInterna != value)
	    	          {
	    	              this.ValidateProperty("MensagemExcecaoInterna", value);
	    	              this.OnMensagemExcecaoInternaChanging(value);
	    	              this.RaiseDataMemberChanging("MensagemExcecaoInterna");
	    	              this._MensagemExcecaoInterna = value;
	    	              this.RaiseDataMemberChanged("MensagemExcecaoInterna");
	    	              this.OnMensagemExcecaoInternaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For MetodoHttp
	    partial void OnMetodoHttpChanging(System.String value);
	    partial void OnMetodoHttpChanged();

	    private System.String _MetodoHttp;

	    [DataMember(IsRequired = true, Name = "MetodoHttp", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Método Http", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(6)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_LOG_ERROS.METODO_HTTP];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_LOG_ERROS.METODO_HTTP")]
	    public System.String MetodoHttp
	    {
	    	    get
	    	    {
	    	          return _MetodoHttp;
	    	    }
	    	    set
	    	    {
	    	          if (this._MetodoHttp != value)
	    	          {
	    	              this.ValidateProperty("MetodoHttp", value);
	    	              this.OnMetodoHttpChanging(value);
	    	              this.RaiseDataMemberChanging("MetodoHttp");
	    	              this._MetodoHttp = value;
	    	              this.RaiseDataMemberChanged("MetodoHttp");
	    	              this.OnMetodoHttpChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeAcao
	    partial void OnNomeAcaoChanging(System.String value);
	    partial void OnNomeAcaoChanged();

	    private System.String _NomeAcao;

	    [DataMember(IsRequired = true, Name = "NomeAcao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ação", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(256)]
	    [FunctionalPoint("Precision[256:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_LOG_ERROS.NOME_ACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_LOG_ERROS.NOME_ACAO")]
	    public System.String NomeAcao
	    {
	    	    get
	    	    {
	    	          return _NomeAcao;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeAcao != value)
	    	          {
	    	              this.ValidateProperty("NomeAcao", value);
	    	              this.OnNomeAcaoChanging(value);
	    	              this.RaiseDataMemberChanging("NomeAcao");
	    	              this._NomeAcao = value;
	    	              this.RaiseDataMemberChanged("NomeAcao");
	    	              this.OnNomeAcaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeControlador
	    partial void OnNomeControladorChanging(System.String value);
	    partial void OnNomeControladorChanged();

	    private System.String _NomeControlador;

	    [DataMember(IsRequired = true, Name = "NomeControlador", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Controlador", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(200)]
	    [FunctionalPoint("Precision[200:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_LOG_ERROS.NOME_CONTROLADOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_LOG_ERROS.NOME_CONTROLADOR")]
	    public System.String NomeControlador
	    {
	    	    get
	    	    {
	    	          return _NomeControlador;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeControlador != value)
	    	          {
	    	              this.ValidateProperty("NomeControlador", value);
	    	              this.OnNomeControladorChanging(value);
	    	              this.RaiseDataMemberChanging("NomeControlador");
	    	              this._NomeControlador = value;
	    	              this.RaiseDataMemberChanged("NomeControlador");
	    	              this.OnNomeControladorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeEmpresa
	    partial void OnNomeEmpresaChanging(System.String value);
	    partial void OnNomeEmpresaChanged();

	    private System.String _NomeEmpresa;

	    [DataMember(Name = "NomeEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Empresa", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsEmpresaAutenticacao];LookUpTitle[Seleção de (Empresa)];LookUpQuery[executeLookUpTcsEmpresaAutenticacao];LookUpFinalize[finalizeLookUpTcsEmpresaAutenticacao];LookUpDisplayColumns[{\"IdLinxEmpresa\" : \"Id Linx Empresa\", \"NomeEmpresa\" : \"Nome Empresa\"}];LookUpColumns[{\"IdLinxEmpresa\" : true, \"NomeEmpresa\" : true}];FilterDataKey[TCS_LOG_ERROS.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeEmpresa#false##250:0##Nome Empresa#1#true##::LookUpTcsEmpresaAutenticacao##false#false#TCS_EMPRESA_AUTENTICACAO#TCS_EMPRESA_AUTENTICACAO#Linx.Framework.BV.TratamentoErros#IQueryable###true#false", EdmKey="TCS_LOG_ERROS.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
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
	    //Extensibility Partial Method Definitions For NomeServidor
	    partial void OnNomeServidorChanging(System.String value);
	    partial void OnNomeServidorChanged();

	    private System.String _NomeServidor;

	    [DataMember(IsRequired = true, Name = "NomeServidor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Servidor", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(256)]
	    [FunctionalPoint("Precision[256:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_LOG_ERROS.NOME_SERVIDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_LOG_ERROS.NOME_SERVIDOR")]
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
	    //Extensibility Partial Method Definitions For PilhaExcecao
	    partial void OnPilhaExcecaoChanging(System.String value);
	    partial void OnPilhaExcecaoChanged();

	    private System.String _PilhaExcecao;

	    [DataMember(IsRequired = true, Name = "PilhaExcecao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Pilha Exceção", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_LOG_ERROS.PILHA_EXCECAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_LOG_ERROS.PILHA_EXCECAO")]
	    public System.String PilhaExcecao
	    {
	    	    get
	    	    {
	    	          return _PilhaExcecao;
	    	    }
	    	    set
	    	    {
	    	          if (this._PilhaExcecao != value)
	    	          {
	    	              this.ValidateProperty("PilhaExcecao", value);
	    	              this.OnPilhaExcecaoChanging(value);
	    	              this.RaiseDataMemberChanging("PilhaExcecao");
	    	              this._PilhaExcecao = value;
	    	              this.RaiseDataMemberChanged("PilhaExcecao");
	    	              this.OnPilhaExcecaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UsuarioWindows
	    partial void OnUsuarioWindowsChanging(System.String value);
	    partial void OnUsuarioWindowsChanged();

	    private System.String _UsuarioWindows;

	    [DataMember(IsRequired = true, Name = "UsuarioWindows", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário Servidor", Description="", Order = 15, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(256)]
	    [FunctionalPoint("Precision[256:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_LOG_ERROS.USUARIO_WINDOWS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_LOG_ERROS.USUARIO_WINDOWS")]
	    public System.String UsuarioWindows
	    {
	    	    get
	    	    {
	    	          return _UsuarioWindows;
	    	    }
	    	    set
	    	    {
	    	          if (this._UsuarioWindows != value)
	    	          {
	    	              this.ValidateProperty("UsuarioWindows", value);
	    	              this.OnUsuarioWindowsChanging(value);
	    	              this.RaiseDataMemberChanging("UsuarioWindows");
	    	              this._UsuarioWindows = value;
	    	              this.RaiseDataMemberChanged("UsuarioWindows");
	    	              this.OnUsuarioWindowsChanged();
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
	    [Display(Name = "Nome Autenticação", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacao];LookUpTitle[Seleção de (Nome Autenticação)];LookUpQuery[executeLookUpTcsUsuarioAutenticacao];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacao];LookUpDisplayColumns[{\"IdUsuario\" : \"Id Usuario\", \"NomeAutenticacao\" : \"Usuário Autenticação\", \"NomeUsuario\" : \"Nome\"}];LookUpColumns[{\"IdUsuario\" : true, \"NomeAutenticacao\" : true, \"NomeUsuario\" : true}];FilterDataKey[TCS_LOG_ERROS.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="String#NomeAutenticacao#false##250:0##Usuário Autenticação#1#true##::LookUpTcsUsuarioAutenticacao##false#false#TCS_USUARIO_AUTENTICACAO#TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.TratamentoErros#IQueryable###true#false", EdmKey="TCS_LOG_ERROS.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO")]
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
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacao];LookUpTitle[Seleção de (Usuário)];LookUpQuery[executeLookUpTcsUsuarioAutenticacao];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacao];LookUpDisplayColumns[{\"IdUsuario\" : \"Id Usuario\", \"NomeAutenticacao\" : \"Usuário Autenticação\", \"NomeUsuario\" : \"Nome\"}];LookUpColumns[{\"IdUsuario\" : true, \"NomeAutenticacao\" : true, \"NomeUsuario\" : true}];FilterDataKey[TCS_LOG_ERROS.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="String#NomeUsuario#false##250:0##Nome#2#true##::LookUpTcsUsuarioAutenticacao##false#false#TCS_USUARIO_AUTENTICACAO#TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.TratamentoErros#IQueryable###true#false", EdmKey="TCS_LOG_ERROS.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO")]
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
	    //Extensibility Partial Method Definitions For EntityUniqueKey
	    partial void OnEntityUniqueKeyChanging(System.Guid value);
	    partial void OnEntityUniqueKeyChanged();

	    private System.Guid _entityUniqueKey;
	    [DataMember(Name = "EntityUniqueKey", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Key()]
	    public System.Guid EntityUniqueKey
	    {
	    	    get
	    	    {
	    	          if (_entityUniqueKey.IsNullOrEmpty())
	    	             _entityUniqueKey =  System.Guid.NewGuid();
	    	          return _entityUniqueKey; 
	    	    }
	    	    set
	    	    {
	    	          if (this._entityUniqueKey != value)
	    	          {
	    	              this.ValidateProperty("EntityUniqueKey", value);
	    	              this.OnEntityUniqueKeyChanging(value);
	    	              this.RaiseDataMemberChanging("EntityUniqueKey");
	    	              this._entityUniqueKey = value;
	    	              this.RaiseDataMemberChanged("EntityUniqueKey");
	    	              this.OnEntityUniqueKeyChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<LogFile> _LogFileList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsLogErrosDash_LogFile", "EntityUniqueKey", "EntityParentUniqueKey", IsForeignKey=false)]
	    [DataMember(Name = "LogFileList", EmitDefaultValue = true)]
	    public IEnumerable<LogFile> LogFileList
	    {
	        get
	        {
	
	            if (this._LogFileList == null)
	            	this._LogFileList = new List<LogFile>();
	
	            return this._LogFileList;
	        }
	        set
	        {
	            if (this._LogFileList != value)
	            {
	                this._LogFileList = value;
	                this.RaisePropertyChanged("LogFileList");
	            }
	        }
	    }	 
		
	    private IEnumerable<TcsLogErros> _TcsLogErrosList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsLogErrosDash_TcsLogErros", "EntityUniqueKey", "EntityParentUniqueKey", IsForeignKey=false)]
	    [DataMember(Name = "TcsLogErrosList", EmitDefaultValue = true)]
	    public IEnumerable<TcsLogErros> TcsLogErrosList
	    {
	        get
	        {
	
	            if (this._TcsLogErrosList == null)
	            	this._TcsLogErrosList = new List<TcsLogErros>();
	
	            return this._TcsLogErrosList;
	        }
	        set
	        {
	            if (this._TcsLogErrosList != value)
	            {
	                this._TcsLogErrosList = value;
	                this.RaisePropertyChanged("TcsLogErrosList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_LOG_ERROS").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_LOG_ERROS), QualifiedEntitySetName = "AutorizacaoContext.TCS_LOG_ERROS" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_LOG_ERROS.DATA_ERRO", Source = "DataErro", Target = "DATA_ERRO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_LOG_ERROS", RelationPropertyName = "TCS_LOG_ERROS" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_LOG_ERROS.NOME_ACAO", Source = "NomeAcao", Target = "NOME_ACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_LOG_ERROS", RelationPropertyName = "TCS_LOG_ERROS" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_LOG_ERROS.METODO_HTTP", Source = "MetodoHttp", Target = "METODO_HTTP", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_LOG_ERROS", RelationPropertyName = "TCS_LOG_ERROS" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_LOG_ERROS.ENDERECO_WEB", Source = "EnderecoWeb", Target = "ENDERECO_WEB", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_LOG_ERROS", RelationPropertyName = "TCS_LOG_ERROS" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_LOG_ERROS.NOME_SERVIDOR", Source = "NomeServidor", Target = "NOME_SERVIDOR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_LOG_ERROS", RelationPropertyName = "TCS_LOG_ERROS" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_LOG_ERROS.PILHA_EXCECAO", Source = "PilhaExcecao", Target = "PILHA_EXCECAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_LOG_ERROS", RelationPropertyName = "TCS_LOG_ERROS" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_LOG_ERROS.GPECON.ID_LINX", Source = "IdLinxGpecon", Target = "ID_LINX", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_AUTENTICACAO", RelationPropertyName = "GPECON" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_LOG_ERROS.USUARIO_WINDOWS", Source = "UsuarioWindows", Target = "USUARIO_WINDOWS", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_LOG_ERROS", RelationPropertyName = "TCS_LOG_ERROS" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_LOG_ERROS.ID_TCS_LOG_ERROS", Source = "IdTcsLogErros", Target = "ID_TCS_LOG_ERROS", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_LOG_ERROS", RelationPropertyName = "TCS_LOG_ERROS" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_LOG_ERROS.MENSAGEM_EXCECAO", Source = "MensagemExcecao", Target = "MENSAGEM_EXCECAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_LOG_ERROS", RelationPropertyName = "TCS_LOG_ERROS" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_LOG_ERROS.NOME_CONTROLADOR", Source = "NomeControlador", Target = "NOME_CONTROLADOR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_LOG_ERROS", RelationPropertyName = "TCS_LOG_ERROS" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_LOG_ERROS.MENSAGEM_EXCECAO_INTERNA", Source = "MensagemExcecaoInterna", Target = "MENSAGEM_EXCECAO_INTERNA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_LOG_ERROS", RelationPropertyName = "TCS_LOG_ERROS" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_LOG_ERROS.TCS_APLICACAO.ID_APLICACAO", Source = "IdAplicacao", Target = "ID_APLICACAO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICACAO", RelationPropertyName = "TCS_APLICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_LOG_ERROS.TCS_AMBIENTE.ID_TCS_AMBIENTE", Source = "IdTcsAmbiente", Target = "ID_TCS_AMBIENTE", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE", RelationPropertyName = "TCS_AMBIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_LOG_ERROS.TCS_EMPRESA_AUTENTICACAO.ID_LINX", Source = "IdLinxEmpresa", Target = "ID_LINX", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_AUTENTICACAO", RelationPropertyName = "TCS_EMPRESA_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_LOG_ERROS.TCS_USUARIO_AUTENTICACAO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 

	    private string _changeState = "N";
	    [DataMember()]
	    public string ChangeState { get { return _changeState; } set { _changeState = value; } }	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="System.String.Empty", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
		
	[DataContract(IsReference = false, Name = "LogFile")]
	[Serializable()]
	public partial class LogFile 
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(TratamentoErrosDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsLogErrosDash
	         this.TcsLogErrosDash = (from r in context.GetTcsLogErrosDashByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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

	    public virtual void ResetChangeState()
	    {
	      this.ChangeState = "N";
	    }

	    #endregion Flat Entities

		
	    #region Data Properties	
	 


	    private System.String _FileName;

	    [DataMember(Name = "FileName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="System.String.Empty")]
	    public System.String FileName
	    {
	    	    get
	    	    {
	    	          if (_FileName.IsNullOrEmpty())
	    	             _FileName =  String.Empty;
	    	          return _FileName;
	    	    }
	    	    set
	    	    {
	    	          this._FileName = value;
	    	    }
	    }

	    private string _Download;

	    [DataMember(Name = "Download", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Download
	    {
	    	    get
	    	    {
	    	          return _Download;
	    	    }
	    	    set
	    	    {
	    	          this._Download = value;
	    	    }
	    }
	    [DataMember(Name = "EntityParentUniqueKey", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    public System.Guid EntityParentUniqueKey { get; set; }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsLogErrosDash _TcsLogErrosDash;
	    [DataMember(Name = "TcsLogErrosDash", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsLogErrosDash_LogFile", "EntityParentUniqueKey", "EntityUniqueKey", IsForeignKey=true)]
	    public TcsLogErrosDash TcsLogErrosDash
	    {
	        get
	        {
	            return this._TcsLogErrosDash;
	        }
	        set
	        {
	            if (this._TcsLogErrosDash != value)
	            {
	                this._TcsLogErrosDash = value;
	                
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		
	    #region Change State Control
	 

	    private string _changeState = "N";
	    [DataMember()]
	    public string ChangeState { get { return _changeState; } set { _changeState = value; } }	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_LOG_ERROS.ID_TCS_LOG_ERROS", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Banco de Dados];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsLogErros];ReadOnly[false];Entities[TCS_LOG_ERROS:IdTcsLogErros|TCS_APLICACAO:IdAplicacao|TCS_EMPRESA_AUTENTICACAO:IdLinxEmpresa|TCS_EMPRESA_AUTENTICACAO:IdLinxGpecon|TCS_AMBIENTE:IdTcsAmbiente];SubQueryInfo[Select 1 From #ParentAlias#.TCS_AMBIENTE.TCS_LOG_ERROS_LISTA as #Alias#];EdmEntityName[TCS_LOG_ERROS];EntityRelations[TCS_AMBIENTE(TCS_AMBIENTE)#TCS_APLICACAO(TCS_APLICACAO)#TCS_APLICATIVO(TCS_APLICATIVO)#TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)#GPECON(TCS_EMPRESA_AUTENTICACAO)#TCS_USUARIO_AUTENTICACAO(TCS_USUARIO_AUTENTICACAO)];EdmParentEntityName[TCS_LOG_ERROS];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsLogErros")]
	[Serializable()]
	public partial class TcsLogErros : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(TratamentoErrosDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsLogErrosDash
	         this.TcsLogErrosDash = (from r in context.GetTcsLogErrosDashByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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

	    public virtual void ResetChangeState()
	    {
	      this.ChangeState = "N";
	    }

	    #endregion Flat Entities

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For DataErro
	    partial void OnDataErroChanging(System.DateTime value);
	    partial void OnDataErroChanged();

	    private System.DateTime _DataErro;

	    [DataMember(IsRequired = true, Name = "DataErro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Data", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Descending];OrderBySequence[0];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_LOG_ERROS.DATA_ERRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_LOG_ERROS.DATA_ERRO")]
	    public System.DateTime DataErro
	    {
	    	    get
	    	    {
	    	          return _DataErro;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataErro != value)
	    	          {
	    	              this.ValidateProperty("DataErro", value);
	    	              this.OnDataErroChanging(value);
	    	              this.RaiseDataMemberChanging("DataErro");
	    	              this._DataErro = value;
	    	              this.RaiseDataMemberChanged("DataErro");
	    	              this.OnDataErroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescricaoAmbiente
	    partial void OnDescricaoAmbienteChanging(System.String value);
	    partial void OnDescricaoAmbienteChanged();

	    private System.String _DescricaoAmbiente;

	    [DataMember(Name = "DescricaoAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ambiente", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_LOG_ERROS.TCS_AMBIENTE.DESCRICAO_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_LOG_ERROS.TCS_AMBIENTE.DESCRICAO_AMBIENTE")]
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

	    [DataMember(Name = "DescricaoAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicação", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_LOG_ERROS.TCS_APLICACAO.DESCRICAO_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_LOG_ERROS.TCS_APLICACAO.DESCRICAO_APLICACAO")]
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
	    //Extensibility Partial Method Definitions For Empresa
	    partial void OnEmpresaChanging(System.String value);
	    partial void OnEmpresaChanged();

	    private System.String _Empresa;

	    [DataMember(Name = "Empresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Empresa", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_LOG_ERROS.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_LOG_ERROS.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
	    public System.String Empresa
	    {
	    	    get
	    	    {
	    	          return _Empresa;
	    	    }
	    	    set
	    	    {
	    	          if (this._Empresa != value)
	    	          {
	    	              this.ValidateProperty("Empresa", value);
	    	              this.OnEmpresaChanging(value);
	    	              this.RaiseDataMemberChanging("Empresa");
	    	              this._Empresa = value;
	    	              this.RaiseDataMemberChanged("Empresa");
	    	              this.OnEmpresaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For EnderecoWeb
	    partial void OnEnderecoWebChanging(System.String value);
	    partial void OnEnderecoWebChanged();

	    private System.String _EnderecoWeb;

	    [DataMember(IsRequired = true, Name = "EnderecoWeb", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Endereço Web", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(8000)]
	    [FunctionalPoint("Precision[8000:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_LOG_ERROS.ENDERECO_WEB];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_LOG_ERROS.ENDERECO_WEB")]
	    public System.String EnderecoWeb
	    {
	    	    get
	    	    {
	    	          return _EnderecoWeb;
	    	    }
	    	    set
	    	    {
	    	          if (this._EnderecoWeb != value)
	    	          {
	    	              this.ValidateProperty("EnderecoWeb", value);
	    	              this.OnEnderecoWebChanging(value);
	    	              this.RaiseDataMemberChanging("EnderecoWeb");
	    	              this._EnderecoWeb = value;
	    	              this.RaiseDataMemberChanged("EnderecoWeb");
	    	              this.OnEnderecoWebChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Gpecon
	    partial void OnGpeconChanging(System.String value);
	    partial void OnGpeconChanged();

	    private System.String _Gpecon;

	    [DataMember(Name = "Gpecon", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Grupo Econômico", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_LOG_ERROS.GPECON.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_LOG_ERROS.GPECON.NOME_EMPRESA")]
	    public System.String Gpecon
	    {
	    	    get
	    	    {
	    	          return _Gpecon;
	    	    }
	    	    set
	    	    {
	    	          if (this._Gpecon != value)
	    	          {
	    	              this.ValidateProperty("Gpecon", value);
	    	              this.OnGpeconChanging(value);
	    	              this.RaiseDataMemberChanging("Gpecon");
	    	              this._Gpecon = value;
	    	              this.RaiseDataMemberChanged("Gpecon");
	    	              this.OnGpeconChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdAplicacao
	    partial void OnIdAplicacaoChanging(System.Nullable<Int32> value);
	    partial void OnIdAplicacaoChanged();

	    private System.Nullable<Int32> _IdAplicacao;

	    [DataMember(Name = "IdAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Aplicacao", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_LOG_ERROS.TCS_APLICACAO.ID_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_LOG_ERROS.TCS_APLICACAO.ID_APLICACAO")]
	    public System.Nullable<Int32> IdAplicacao
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
	    //Extensibility Partial Method Definitions For IdLinxEmpresa
	    partial void OnIdLinxEmpresaChanging(System.Nullable<Int32> value);
	    partial void OnIdLinxEmpresaChanged();

	    private System.Nullable<Int32> _IdLinxEmpresa;

	    [DataMember(Name = "IdLinxEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx Empresa", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_LOG_ERROS.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_LOG_ERROS.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
	    public System.Nullable<Int32> IdLinxEmpresa
	    {
	    	    get
	    	    {
	    	          return _IdLinxEmpresa;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLinxEmpresa != value)
	    	          {
	    	              this.ValidateProperty("IdLinxEmpresa", value);
	    	              this.OnIdLinxEmpresaChanging(value);
	    	              this.RaiseDataMemberChanging("IdLinxEmpresa");
	    	              this._IdLinxEmpresa = value;
	    	              this.RaiseDataMemberChanged("IdLinxEmpresa");
	    	              this.OnIdLinxEmpresaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLinxGpecon
	    partial void OnIdLinxGpeconChanging(System.Nullable<Int32> value);
	    partial void OnIdLinxGpeconChanged();

	    private System.Nullable<Int32> _IdLinxGpecon;

	    [DataMember(Name = "IdLinxGpecon", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx Gpecon", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_LOG_ERROS.GPECON.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_LOG_ERROS.GPECON.ID_LINX")]
	    public System.Nullable<Int32> IdLinxGpecon
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
	    //Extensibility Partial Method Definitions For IdTcsAmbiente
	    partial void OnIdTcsAmbienteChanging(System.Nullable<Int32> value);
	    partial void OnIdTcsAmbienteChanged();

	    private System.Nullable<Int32> _IdTcsAmbiente;

	    [DataMember(Name = "IdTcsAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_LOG_ERROS.TCS_AMBIENTE.ID_TCS_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_LOG_ERROS.TCS_AMBIENTE.ID_TCS_AMBIENTE")]
	    public System.Nullable<Int32> IdTcsAmbiente
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
	    //Extensibility Partial Method Definitions For IdTcsLogErros
	    partial void OnIdTcsLogErrosChanging(Int32 value);
	    partial void OnIdTcsLogErrosChanged();

	    private Int32 _IdTcsLogErros;

	    [DataMember(IsRequired = true, Name = "IdTcsLogErros", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_LOG_ERROS.ID_TCS_LOG_ERROS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_LOG_ERROS.ID_TCS_LOG_ERROS")]
	    public Int32 IdTcsLogErros
	    {
	    	    get
	    	    {
	    	          return _IdTcsLogErros;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsLogErros != value)
	    	          {
	    	              this.ValidateProperty("IdTcsLogErros", value);
	    	              this.OnIdTcsLogErrosChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsLogErros");
	    	              this._IdTcsLogErros = value;
	    	              this.RaiseDataMemberChanged("IdTcsLogErros");
	    	              this.OnIdTcsLogErrosChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuario
	    partial void OnIdUsuarioChanging(System.Nullable<Int64> value);
	    partial void OnIdUsuarioChanged();

	    private System.Nullable<Int64> _IdUsuario;

	    [DataMember(Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_LOG_ERROS.TCS_USUARIO_AUTENTICACAO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_LOG_ERROS.TCS_USUARIO_AUTENTICACAO.ID_USUARIO")]
	    public System.Nullable<Int64> IdUsuario
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
	    //Extensibility Partial Method Definitions For MensagemExcecao
	    partial void OnMensagemExcecaoChanging(System.String value);
	    partial void OnMensagemExcecaoChanged();

	    private System.String _MensagemExcecao;

	    [DataMember(IsRequired = true, Name = "MensagemExcecao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Exceção", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(1000)]
	    [FunctionalPoint("Precision[1000:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_LOG_ERROS.MENSAGEM_EXCECAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_LOG_ERROS.MENSAGEM_EXCECAO")]
	    public System.String MensagemExcecao
	    {
	    	    get
	    	    {
	    	          return _MensagemExcecao;
	    	    }
	    	    set
	    	    {
	    	          if (this._MensagemExcecao != value)
	    	          {
	    	              this.ValidateProperty("MensagemExcecao", value);
	    	              this.OnMensagemExcecaoChanging(value);
	    	              this.RaiseDataMemberChanging("MensagemExcecao");
	    	              this._MensagemExcecao = value;
	    	              this.RaiseDataMemberChanged("MensagemExcecao");
	    	              this.OnMensagemExcecaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For MensagemExcecaoInterna
	    partial void OnMensagemExcecaoInternaChanging(System.String value);
	    partial void OnMensagemExcecaoInternaChanged();

	    private System.String _MensagemExcecaoInterna;

	    [DataMember(Name = "MensagemExcecaoInterna", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Exceção Interna", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(1000)]
	    [FunctionalPoint("Precision[1000:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_LOG_ERROS.MENSAGEM_EXCECAO_INTERNA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_LOG_ERROS.MENSAGEM_EXCECAO_INTERNA")]
	    public System.String MensagemExcecaoInterna
	    {
	    	    get
	    	    {
	    	          return _MensagemExcecaoInterna;
	    	    }
	    	    set
	    	    {
	    	          if (this._MensagemExcecaoInterna != value)
	    	          {
	    	              this.ValidateProperty("MensagemExcecaoInterna", value);
	    	              this.OnMensagemExcecaoInternaChanging(value);
	    	              this.RaiseDataMemberChanging("MensagemExcecaoInterna");
	    	              this._MensagemExcecaoInterna = value;
	    	              this.RaiseDataMemberChanged("MensagemExcecaoInterna");
	    	              this.OnMensagemExcecaoInternaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For MetodoHttp
	    partial void OnMetodoHttpChanging(System.String value);
	    partial void OnMetodoHttpChanged();

	    private System.String _MetodoHttp;

	    [DataMember(IsRequired = true, Name = "MetodoHttp", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Método Http", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(6)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_LOG_ERROS.METODO_HTTP];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_LOG_ERROS.METODO_HTTP")]
	    public System.String MetodoHttp
	    {
	    	    get
	    	    {
	    	          return _MetodoHttp;
	    	    }
	    	    set
	    	    {
	    	          if (this._MetodoHttp != value)
	    	          {
	    	              this.ValidateProperty("MetodoHttp", value);
	    	              this.OnMetodoHttpChanging(value);
	    	              this.RaiseDataMemberChanging("MetodoHttp");
	    	              this._MetodoHttp = value;
	    	              this.RaiseDataMemberChanged("MetodoHttp");
	    	              this.OnMetodoHttpChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeAcao
	    partial void OnNomeAcaoChanging(System.String value);
	    partial void OnNomeAcaoChanged();

	    private System.String _NomeAcao;

	    [DataMember(IsRequired = true, Name = "NomeAcao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ação", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(256)]
	    [FunctionalPoint("Precision[256:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_LOG_ERROS.NOME_ACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_LOG_ERROS.NOME_ACAO")]
	    public System.String NomeAcao
	    {
	    	    get
	    	    {
	    	          return _NomeAcao;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeAcao != value)
	    	          {
	    	              this.ValidateProperty("NomeAcao", value);
	    	              this.OnNomeAcaoChanging(value);
	    	              this.RaiseDataMemberChanging("NomeAcao");
	    	              this._NomeAcao = value;
	    	              this.RaiseDataMemberChanged("NomeAcao");
	    	              this.OnNomeAcaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeControlador
	    partial void OnNomeControladorChanging(System.String value);
	    partial void OnNomeControladorChanged();

	    private System.String _NomeControlador;

	    [DataMember(IsRequired = true, Name = "NomeControlador", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Controlador", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(200)]
	    [FunctionalPoint("Precision[200:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_LOG_ERROS.NOME_CONTROLADOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_LOG_ERROS.NOME_CONTROLADOR")]
	    public System.String NomeControlador
	    {
	    	    get
	    	    {
	    	          return _NomeControlador;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeControlador != value)
	    	          {
	    	              this.ValidateProperty("NomeControlador", value);
	    	              this.OnNomeControladorChanging(value);
	    	              this.RaiseDataMemberChanging("NomeControlador");
	    	              this._NomeControlador = value;
	    	              this.RaiseDataMemberChanged("NomeControlador");
	    	              this.OnNomeControladorChanged();
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
	    [Display(Name = "Servidor", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(256)]
	    [FunctionalPoint("Precision[256:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_LOG_ERROS.NOME_SERVIDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_LOG_ERROS.NOME_SERVIDOR")]
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
	    //Extensibility Partial Method Definitions For PilhaExcecao
	    partial void OnPilhaExcecaoChanging(System.String value);
	    partial void OnPilhaExcecaoChanged();

	    private System.String _PilhaExcecao;

	    [DataMember(IsRequired = true, Name = "PilhaExcecao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Pilha Exceção", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_LOG_ERROS.PILHA_EXCECAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_LOG_ERROS.PILHA_EXCECAO")]
	    public System.String PilhaExcecao
	    {
	    	    get
	    	    {
	    	          return _PilhaExcecao;
	    	    }
	    	    set
	    	    {
	    	          if (this._PilhaExcecao != value)
	    	          {
	    	              this.ValidateProperty("PilhaExcecao", value);
	    	              this.OnPilhaExcecaoChanging(value);
	    	              this.RaiseDataMemberChanging("PilhaExcecao");
	    	              this._PilhaExcecao = value;
	    	              this.RaiseDataMemberChanged("PilhaExcecao");
	    	              this.OnPilhaExcecaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UsuarioWindows
	    partial void OnUsuarioWindowsChanging(System.String value);
	    partial void OnUsuarioWindowsChanged();

	    private System.String _UsuarioWindows;

	    [DataMember(IsRequired = true, Name = "UsuarioWindows", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário Servidor", Description="", Order = 15, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(256)]
	    [FunctionalPoint("Precision[256:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_LOG_ERROS.USUARIO_WINDOWS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_LOG_ERROS.USUARIO_WINDOWS")]
	    public System.String UsuarioWindows
	    {
	    	    get
	    	    {
	    	          return _UsuarioWindows;
	    	    }
	    	    set
	    	    {
	    	          if (this._UsuarioWindows != value)
	    	          {
	    	              this.ValidateProperty("UsuarioWindows", value);
	    	              this.OnUsuarioWindowsChanging(value);
	    	              this.RaiseDataMemberChanging("UsuarioWindows");
	    	              this._UsuarioWindows = value;
	    	              this.RaiseDataMemberChanged("UsuarioWindows");
	    	              this.OnUsuarioWindowsChanged();
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
	    [Display(Name = "Nome Autenticação", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_LOG_ERROS.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_LOG_ERROS.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO")]
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
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_LOG_ERROS.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_LOG_ERROS.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO")]
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
	    [DataMember(Name = "EntityParentUniqueKey", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    public System.Guid EntityParentUniqueKey { get; set; }

	    private Int32 _TemporaryIdTcsLogErros;
	    [DataMember(Name = "TemporaryIdTcsLogErros", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id (Tmp)", Description="Temporary Key", Order = 7, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdTcsLogErros
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTcsLogErros.IsNullOrEmpty())
	    	                this._TemporaryIdTcsLogErros = this._IdTcsLogErros;
	    	          return this._TemporaryIdTcsLogErros;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTcsLogErros != value)
	    	              this._TemporaryIdTcsLogErros = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsLogErrosDash _TcsLogErrosDash;
	    [DataMember(Name = "TcsLogErrosDash", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsLogErrosDash_TcsLogErros", "EntityParentUniqueKey", "EntityUniqueKey", IsForeignKey=true)]
	    public TcsLogErrosDash TcsLogErrosDash
	    {
	        get
	        {
	            return this._TcsLogErrosDash;
	        }
	        set
	        {
	            if (this._TcsLogErrosDash != value)
	            {
	                this._TcsLogErrosDash = value;
	                this.RaisePropertyChanged("TcsLogErrosDashList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_LOG_ERROS").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_LOG_ERROS), QualifiedEntitySetName = "AutorizacaoContext.TCS_LOG_ERROS" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_LOG_ERROS.DATA_ERRO", Source = "DataErro", Target = "DATA_ERRO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_LOG_ERROS", RelationPropertyName = "TCS_LOG_ERROS" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_LOG_ERROS.NOME_ACAO", Source = "NomeAcao", Target = "NOME_ACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_LOG_ERROS", RelationPropertyName = "TCS_LOG_ERROS" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_LOG_ERROS.METODO_HTTP", Source = "MetodoHttp", Target = "METODO_HTTP", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_LOG_ERROS", RelationPropertyName = "TCS_LOG_ERROS" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_LOG_ERROS.ENDERECO_WEB", Source = "EnderecoWeb", Target = "ENDERECO_WEB", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_LOG_ERROS", RelationPropertyName = "TCS_LOG_ERROS" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_LOG_ERROS.NOME_SERVIDOR", Source = "NomeServidor", Target = "NOME_SERVIDOR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_LOG_ERROS", RelationPropertyName = "TCS_LOG_ERROS" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_LOG_ERROS.PILHA_EXCECAO", Source = "PilhaExcecao", Target = "PILHA_EXCECAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_LOG_ERROS", RelationPropertyName = "TCS_LOG_ERROS" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_LOG_ERROS.GPECON.ID_LINX", Source = "IdLinxGpecon", Target = "ID_LINX", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_AUTENTICACAO", RelationPropertyName = "GPECON" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_LOG_ERROS.USUARIO_WINDOWS", Source = "UsuarioWindows", Target = "USUARIO_WINDOWS", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_LOG_ERROS", RelationPropertyName = "TCS_LOG_ERROS" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_LOG_ERROS.ID_TCS_LOG_ERROS", Source = "IdTcsLogErros", Target = "ID_TCS_LOG_ERROS", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_LOG_ERROS", RelationPropertyName = "TCS_LOG_ERROS" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_LOG_ERROS.MENSAGEM_EXCECAO", Source = "MensagemExcecao", Target = "MENSAGEM_EXCECAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_LOG_ERROS", RelationPropertyName = "TCS_LOG_ERROS" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_LOG_ERROS.NOME_CONTROLADOR", Source = "NomeControlador", Target = "NOME_CONTROLADOR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_LOG_ERROS", RelationPropertyName = "TCS_LOG_ERROS" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_LOG_ERROS.MENSAGEM_EXCECAO_INTERNA", Source = "MensagemExcecaoInterna", Target = "MENSAGEM_EXCECAO_INTERNA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_LOG_ERROS", RelationPropertyName = "TCS_LOG_ERROS" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_LOG_ERROS.TCS_APLICACAO.ID_APLICACAO", Source = "IdAplicacao", Target = "ID_APLICACAO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICACAO", RelationPropertyName = "TCS_APLICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_LOG_ERROS.TCS_AMBIENTE.ID_TCS_AMBIENTE", Source = "IdTcsAmbiente", Target = "ID_TCS_AMBIENTE", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE", RelationPropertyName = "TCS_AMBIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_LOG_ERROS.TCS_EMPRESA_AUTENTICACAO.ID_LINX", Source = "IdLinxEmpresa", Target = "ID_LINX", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_AUTENTICACAO", RelationPropertyName = "TCS_EMPRESA_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_LOG_ERROS.TCS_USUARIO_AUTENTICACAO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 

	    private string _changeState = "N";
	    [DataMember()]
	    public string ChangeState { get { return _changeState; } set { _changeState = value; } }	

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
	[DomainIdentifier("ProcessorOverviewTratamentoErrosDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class TratamentoErrosDomainService : DomainService, IDataServiceContext 
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

		
	    public TratamentoErrosDomainService() : this("", null, null) { }
	    public TratamentoErrosDomainService(string connectionString) : this(connectionString, null, null) { }
	    public TratamentoErrosDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public TratamentoErrosDomainService(Linx.Framework.Autorizacao.BM.AutorizacaoContext dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public TratamentoErrosDomainService(string connectionString, Linx.Framework.Autorizacao.BM.AutorizacaoContext dataContext, Dictionary<string, string> headers) : base() 
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
 	        var _TcsLogErrosDashElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsLogErrosDash && e.Entity.GetType().Name == "TcsLogErrosDash" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _TcsLogErrosDashElements)
 	           if (((TcsLogErrosDash)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 
 	        //Remove inconsistent details
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is LogFile && e.Entity.GetType().Name == "LogFile" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsLogErros && e.Entity.GetType().Name == "TcsLogErros" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
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
	    //Get All LookUpGpecon.
	    public IQueryable<LookUpGpecon> GetAllLookUpGpecon()
	    {
	        return this.GetLookUpGpecon(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpGpecon By EntitySearch.
	    public IQueryable<LookUpGpecon> GetLookUpGpeconByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpGpecon(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpGpecon.
	    public IQueryable<LookUpGpecon> GetLookUpGpecon(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TCS_EMPRESA_AUTENTICACAO" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpGpecon";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpGpecon));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpGpecon> query =  
	
	            (from entity in this.DbContext.TCS_EMPRESA_AUTENTICACAO.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpGpecon()		
	            {
	            
                IdLinxGpecon = entity.ID_LINX
                , Gpecon = entity.NOME_EMPRESA
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
                , IdAplicacao = entity.ID_APLICACAO
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
	            
                IdLinxEmpresa = entity.ID_LINX
                , NomeEmpresa = entity.NOME_EMPRESA
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
	            
	            select new LookUpTcsUsuarioAutenticacao()		
	            {
	            
                IdUsuario = entity.ID_USUARIO
                , NomeAutenticacao = entity.NOME_AUTENTICACAO
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
	
		

	        if (entityName.InList("Linx.Framework.BV.TratamentoErros.TcsLogErrosDash"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsLogErrosDash",
	        			NameSpace = "Linx.Framework.BV.TratamentoErros",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsLogErrosDash",
	        			ClearMethodName = "ClearTcsLogErrosDash",
	        			QueryMethodName  = "GetPagedTcsLogErrosDash",	
	        			CountingMethodName  = "GetTcsLogErrosDash" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.TratamentoErros.TcsLogErrosDash"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.TratamentoErros.TcsLogErrosDash"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.TratamentoErros.TcsLogErrosDash", "Linx.Framework.BV.TratamentoErros.LogFile"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "LogFile",
	        			NameSpace = "Linx.Framework.BV.TratamentoErros",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsLogErrosDash",	
	        			DisplayName = "Arquivo",
	        			ClearMethodName = "ClearLogFile",
	        			QueryMethodName  = "GetPagedLogFile",	
	        			CountingMethodName  = "GetLogFile" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.TratamentoErros.LogFile"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.TratamentoErros.LogFile"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.TratamentoErros.TcsLogErrosDash", "Linx.Framework.BV.TratamentoErros.TcsLogErros"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsLogErros",
	        			NameSpace = "Linx.Framework.BV.TratamentoErros",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsLogErrosDash",	
	        			DisplayName = "Banco de Dados",
	        			ClearMethodName = "ClearTcsLogErros",
	        			QueryMethodName  = "GetPagedTcsLogErros",	
	        			CountingMethodName  = "GetTcsLogErros" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.TratamentoErros.TcsLogErros"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.TratamentoErros.TcsLogErros"), forceAll: forceAll)
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

         		    return new string[] { "Framework_TratamentoErrosClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.TratamentoErrosClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Framework_tratamentoErrosService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.tratamentoErrosService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear TcsLogErrosDash.
	    public IEnumerable<TcsLogErrosDash> ClearTcsLogErrosDash()
	    {
	        List<TcsLogErrosDash> result = new List<TcsLogErrosDash>();
	        result.Add(new TcsLogErrosDash());	
			
	        result[0].TcsLogErrosList = new List<TcsLogErros>();
	        ((List<TcsLogErros>)result[0].TcsLogErrosList).Add(new TcsLogErros());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsLogErros.
	    public IEnumerable<TcsLogErros> ClearTcsLogErros()
	    {
	        List<TcsLogErros> result = new List<TcsLogErros>();
	        result.Add(new TcsLogErros());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    [TcsLogErrosDashQueryCustomAuthorizationAuto()]
	    [Query(HasSideEffects = false)]
	    //Get TcsLogErrosDash.
	    public IEnumerable<TcsLogErrosDash> GetTcsLogErrosDash()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTcsLogErrosDash")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsLogErrosDashQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		

	        IEnumerable<TcsLogErrosDash> result = 
	            (from entity0 in TcsLogErrosDash.OnSearchingReplacement(this.DbContext, null, null, null) select entity0);
		
	
	        	

	
	        return result;
	    }
			
	
	    [LogFileQueryCustomAuthorizationAuto()]
	    [Query(HasSideEffects = false)]
	    //Get LogFile.
	    public IEnumerable<LogFile> GetLogFile()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetLogFile")))
 	        {
 	             AuthorizationResult authorizationResult = (new LogFileQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		

	        IEnumerable<LogFile> result = 
	            (from entity0 in LogFile.OnSearchingReplacement(null) select entity0);
		
	
	        	

	
	        return result;
	    }
			
	
	    [TcsLogErrosQueryCustomAuthorizationAuto()]
	    [Query(HasSideEffects = false)]
	    //Get TcsLogErros.
	    public IQueryable<TcsLogErros> GetTcsLogErros()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTcsLogErros")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsLogErrosQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<TcsLogErros> result = 
	            (from entity0 in this.DbContext.TCS_LOG_ERROS
                  let entity0Al4 = entity0.GPECON
                  let entity0Al1 = entity0.TCS_AMBIENTE
                  let entity0Al2 = entity0.TCS_APLICACAO
                  let entity0Al3 = entity0.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al5 = entity0.TCS_USUARIO_AUTENTICACAO
                orderby entity0.DATA_ERRO descending
	            
	            	
	            select new TcsLogErros()		
	            {
	            
                DataErro = entity0.DATA_ERRO
                , DescricaoAmbiente = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al2.DESCRICAO_APLICACAO
                , Empresa = entity0Al3.NOME_EMPRESA
                , EnderecoWeb = entity0.ENDERECO_WEB
                , Gpecon = entity0Al4.NOME_EMPRESA
                , IdAplicacao = entity0Al2.ID_APLICACAO
                , IdLinxEmpresa = entity0Al3.ID_LINX
                , IdLinxGpecon = entity0Al4.ID_LINX
                , IdTcsAmbiente = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsLogErros = entity0.ID_TCS_LOG_ERROS
                , IdUsuario = entity0Al5.ID_USUARIO
                , MensagemExcecao = entity0.MENSAGEM_EXCECAO
                , MensagemExcecaoInterna = entity0.MENSAGEM_EXCECAO_INTERNA
                , MetodoHttp = entity0.METODO_HTTP
                , NomeAcao = entity0.NOME_ACAO
                , NomeControlador = entity0.NOME_CONTROLADOR
                , NomeServidor = entity0.NOME_SERVIDOR
                , PilhaExcecao = entity0.PILHA_EXCECAO
                , UsuarioWindows = entity0.USUARIO_WINDOWS
                , NomeAutenticacao = entity0Al5.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al5.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TcsLogErrosDashQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TcsLogErrosDashNoAssociations.
	    public IEnumerable<TcsLogErrosDash> GetTcsLogErrosDashNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTcsLogErrosDashNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsLogErrosDashQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		

	        IEnumerable<TcsLogErrosDash> result = 
	            (from entity0 in TcsLogErrosDash.OnSearchingReplacement(this.DbContext, null, null, null) select entity0);
		
	
	        	

	
	        return result;
	    }
			
	
	    [LogFileQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get LogFileNoAssociations.
	    public IEnumerable<LogFile> GetLogFileNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetLogFileNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new LogFileQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		

	        IEnumerable<LogFile> result = 
	            (from entity0 in LogFile.OnSearchingReplacement(null) select entity0);
		
	
	        	

	
	        return result;
	    }
			
	
	    [TcsLogErrosQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TcsLogErrosNoAssociations.
	    public IQueryable<TcsLogErros> GetTcsLogErrosNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTcsLogErrosNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsLogErrosQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<TcsLogErros> result = 
	            (from entity0 in this.DbContext.TCS_LOG_ERROS
                  let entity0Al4 = entity0.GPECON
                  let entity0Al1 = entity0.TCS_AMBIENTE
                  let entity0Al2 = entity0.TCS_APLICACAO
                  let entity0Al3 = entity0.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al5 = entity0.TCS_USUARIO_AUTENTICACAO
                orderby entity0.DATA_ERRO descending
	            
	            	
	            select new TcsLogErros()		
	            {
	            
                DataErro = entity0.DATA_ERRO
                , DescricaoAmbiente = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al2.DESCRICAO_APLICACAO
                , Empresa = entity0Al3.NOME_EMPRESA
                , EnderecoWeb = entity0.ENDERECO_WEB
                , Gpecon = entity0Al4.NOME_EMPRESA
                , IdAplicacao = entity0Al2.ID_APLICACAO
                , IdLinxEmpresa = entity0Al3.ID_LINX
                , IdLinxGpecon = entity0Al4.ID_LINX
                , IdTcsAmbiente = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsLogErros = entity0.ID_TCS_LOG_ERROS
                , IdUsuario = entity0Al5.ID_USUARIO
                , MensagemExcecao = entity0.MENSAGEM_EXCECAO
                , MensagemExcecaoInterna = entity0.MENSAGEM_EXCECAO_INTERNA
                , MetodoHttp = entity0.METODO_HTTP
                , NomeAcao = entity0.NOME_ACAO
                , NomeControlador = entity0.NOME_CONTROLADOR
                , NomeServidor = entity0.NOME_SERVIDOR
                , PilhaExcecao = entity0.PILHA_EXCECAO
                , UsuarioWindows = entity0.USUARIO_WINDOWS
                , NomeAutenticacao = entity0Al5.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al5.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	result.Add("LogFile|FileName");
	    	result.Add("LogFile|System.String.Empty");
	    	result.Add("LogFile|DataErro");
	    	result.Add("LogFile|TCS_LOG_ERROS.DATA_ERRO");
	    	result.Add("LogFile|DescricaoAmbiente");
	    	result.Add("LogFile|TCS_LOG_ERROS.TCS_AMBIENTE.DESCRICAO_AMBIENTE");
	    	result.Add("LogFile|DescricaoAplicacao");
	    	result.Add("LogFile|TCS_LOG_ERROS.TCS_APLICACAO.DESCRICAO_APLICACAO");
	    	result.Add("LogFile|EnderecoWeb");
	    	result.Add("LogFile|TCS_LOG_ERROS.ENDERECO_WEB");
	    	result.Add("LogFile|Gpecon");
	    	result.Add("LogFile|TCS_LOG_ERROS.GPECON.NOME_EMPRESA");
	    	result.Add("LogFile|IdAplicacao");
	    	result.Add("LogFile|TCS_LOG_ERROS.TCS_APLICACAO.ID_APLICACAO");
	    	result.Add("LogFile|IdLinxEmpresa");
	    	result.Add("LogFile|TCS_LOG_ERROS.TCS_EMPRESA_AUTENTICACAO.ID_LINX");
	    	result.Add("LogFile|IdLinxGpecon");
	    	result.Add("LogFile|TCS_LOG_ERROS.GPECON.ID_LINX");
	    	result.Add("LogFile|IdTcsAmbiente");
	    	result.Add("LogFile|TCS_LOG_ERROS.TCS_AMBIENTE.ID_TCS_AMBIENTE");
	    	result.Add("LogFile|IdTcsLogErros");
	    	result.Add("LogFile|TCS_LOG_ERROS.ID_TCS_LOG_ERROS");
	    	result.Add("LogFile|IdUsuario");
	    	result.Add("LogFile|TCS_LOG_ERROS.TCS_USUARIO_AUTENTICACAO.ID_USUARIO");
	    	result.Add("LogFile|MensagemExcecao");
	    	result.Add("LogFile|TCS_LOG_ERROS.MENSAGEM_EXCECAO");
	    	result.Add("LogFile|MensagemExcecaoInterna");
	    	result.Add("LogFile|TCS_LOG_ERROS.MENSAGEM_EXCECAO_INTERNA");
	    	result.Add("LogFile|MetodoHttp");
	    	result.Add("LogFile|TCS_LOG_ERROS.METODO_HTTP");
	    	result.Add("LogFile|NomeAcao");
	    	result.Add("LogFile|TCS_LOG_ERROS.NOME_ACAO");
	    	result.Add("LogFile|NomeControlador");
	    	result.Add("LogFile|TCS_LOG_ERROS.NOME_CONTROLADOR");
	    	result.Add("LogFile|NomeEmpresa");
	    	result.Add("LogFile|TCS_LOG_ERROS.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA");
	    	result.Add("LogFile|NomeServidor");
	    	result.Add("LogFile|TCS_LOG_ERROS.NOME_SERVIDOR");
	    	result.Add("LogFile|PilhaExcecao");
	    	result.Add("LogFile|TCS_LOG_ERROS.PILHA_EXCECAO");
	    	result.Add("LogFile|UsuarioWindows");
	    	result.Add("LogFile|TCS_LOG_ERROS.USUARIO_WINDOWS");
	    	result.Add("LogFile|NomeAutenticacao");
	    	result.Add("LogFile|TCS_LOG_ERROS.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO");
	    	result.Add("LogFile|NomeUsuario");
	    	result.Add("LogFile|TCS_LOG_ERROS.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO");
	    	result.Add("TcsLogErros|NomeEmpresa");
	    	result.Add("TcsLogErros|TCS_LOG_ERROS.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA");
	    	//Add filtering disabled property for TCS_LOG_ERROS
	    	string[] bmDisabledTcsLogErrosList = this.GetEDM().GetFilteringDisabledList("TCS_LOG_ERROS");
	    	if (bmDisabledTcsLogErrosList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsLogErrosList.Contains("TCS_LOG_ERROS.DATA_ERRO"))
	    		{
	    			result.Add("TcsLogErros|DataErro");
	    			result.Add("TcsLogErros|TCS_LOG_ERROS.DATA_ERRO");
	    		}
	
	    		if (bmDisabledTcsLogErrosList.Contains("TCS_LOG_ERROS.ENDERECO_WEB"))
	    		{
	    			result.Add("TcsLogErros|EnderecoWeb");
	    			result.Add("TcsLogErros|TCS_LOG_ERROS.ENDERECO_WEB");
	    		}
	
	    		if (bmDisabledTcsLogErrosList.Contains("TCS_LOG_ERROS.ID_TCS_LOG_ERROS"))
	    		{
	    			result.Add("TcsLogErros|IdTcsLogErros");
	    			result.Add("TcsLogErros|TCS_LOG_ERROS.ID_TCS_LOG_ERROS");
	    		}
	
	    		if (bmDisabledTcsLogErrosList.Contains("TCS_LOG_ERROS.MENSAGEM_EXCECAO"))
	    		{
	    			result.Add("TcsLogErros|MensagemExcecao");
	    			result.Add("TcsLogErros|TCS_LOG_ERROS.MENSAGEM_EXCECAO");
	    		}
	
	    		if (bmDisabledTcsLogErrosList.Contains("TCS_LOG_ERROS.MENSAGEM_EXCECAO_INTERNA"))
	    		{
	    			result.Add("TcsLogErros|MensagemExcecaoInterna");
	    			result.Add("TcsLogErros|TCS_LOG_ERROS.MENSAGEM_EXCECAO_INTERNA");
	    		}
	
	    		if (bmDisabledTcsLogErrosList.Contains("TCS_LOG_ERROS.METODO_HTTP"))
	    		{
	    			result.Add("TcsLogErros|MetodoHttp");
	    			result.Add("TcsLogErros|TCS_LOG_ERROS.METODO_HTTP");
	    		}
	
	    		if (bmDisabledTcsLogErrosList.Contains("TCS_LOG_ERROS.NOME_ACAO"))
	    		{
	    			result.Add("TcsLogErros|NomeAcao");
	    			result.Add("TcsLogErros|TCS_LOG_ERROS.NOME_ACAO");
	    		}
	
	    		if (bmDisabledTcsLogErrosList.Contains("TCS_LOG_ERROS.NOME_CONTROLADOR"))
	    		{
	    			result.Add("TcsLogErros|NomeControlador");
	    			result.Add("TcsLogErros|TCS_LOG_ERROS.NOME_CONTROLADOR");
	    		}
	
	    		if (bmDisabledTcsLogErrosList.Contains("TCS_LOG_ERROS.NOME_SERVIDOR"))
	    		{
	    			result.Add("TcsLogErros|NomeServidor");
	    			result.Add("TcsLogErros|TCS_LOG_ERROS.NOME_SERVIDOR");
	    		}
	
	    		if (bmDisabledTcsLogErrosList.Contains("TCS_LOG_ERROS.PILHA_EXCECAO"))
	    		{
	    			result.Add("TcsLogErros|PilhaExcecao");
	    			result.Add("TcsLogErros|TCS_LOG_ERROS.PILHA_EXCECAO");
	    		}
	
	    		if (bmDisabledTcsLogErrosList.Contains("TCS_LOG_ERROS.USUARIO_WINDOWS"))
	    		{
	    			result.Add("TcsLogErros|UsuarioWindows");
	    			result.Add("TcsLogErros|TCS_LOG_ERROS.USUARIO_WINDOWS");
	    		}
	    	}
	    	return result;
	    }

	    #endregion Excluded Filters

	    #region Get By EntitySearchId Methods Definitions.
	
				
	    [Ignore]
	    //Get TcsLogErrosDash By EntitySearchId.
	    public IEnumerable<TcsLogErrosDash> GetTcsLogErrosDashByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsLogErrosDashByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get LogFile By EntitySearchId.
	    public IEnumerable<LogFile> GetLogFileByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetLogFileByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsLogErros By EntitySearchId.
	    public IQueryable<TcsLogErros> GetTcsLogErrosByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsLogErrosByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsLogErrosDash By EntitySearchId.
	    public IEnumerable<TcsLogErrosDash> GetTcsLogErrosDashByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsLogErrosDashByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get LogFile By EntitySearchId.
	    public IEnumerable<LogFile> GetLogFileByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetLogFileByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsLogErros By EntitySearchId.
	    public IQueryable<TcsLogErros> GetTcsLogErrosByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsLogErrosByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get TcsLogErrosDash By Example.
	    [Ignore]
	    public IEnumerable<TcsLogErrosDash> GetTcsLogErrosDashByExample(TcsLogErrosDash entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsLogErrosDashByEntitySearch(queryAnalysis);
	    }
			
	    //Get LogFile By Example.
	    [Ignore]
	    public IEnumerable<LogFile> GetLogFileByExample(LogFile entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetLogFileByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsLogErros By Example.
	    [Ignore]
	    public IQueryable<TcsLogErros> GetTcsLogErrosByExample(TcsLogErros entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsLogErrosByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsLogErrosDash By Example.
	    [Ignore]
	    public IEnumerable<TcsLogErrosDash> GetTcsLogErrosDashByExampleNoAssociations(TcsLogErrosDash entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsLogErrosDashByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get LogFile By Example.
	    [Ignore]
	    public IEnumerable<LogFile> GetLogFileByExampleNoAssociations(LogFile entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetLogFileByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsLogErros By Example.
	    [Ignore]
	    public IQueryable<TcsLogErros> GetTcsLogErrosByExampleNoAssociations(TcsLogErros entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsLogErrosByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key





	    [Ignore]
	    public LogFile GetLogFileByKey(System.String fileName)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("LogFile");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "FileName"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, fileName));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetLogFileByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsLogErros GetTcsLogErrosByKey(Int32 idTcsLogErros)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsLogErros");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsLogErros"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsLogErros));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsLogErrosByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    [TcsLogErrosDashQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TcsLogErrosDashByEntitySearch.
	    public IEnumerable<TcsLogErrosDash> GetTcsLogErrosDashByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTcsLogErrosDashByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsLogErrosDashQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsLogErrosDash));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		

	        IEnumerable<TcsLogErrosDash> result = 
	            (from entity0 in TcsLogErrosDash.OnSearchingReplacement(this.DbContext, dynQuery, parameters, entitySearchList) select entity0);
		
	
	        	

	
	        return result;
	    }
			
	
	    [LogFileQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get LogFileByEntitySearch.
	    public IEnumerable<LogFile> GetLogFileByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetLogFileByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new LogFileQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

 	        //Adjust EntityName for MasterFiltering
 	        var thisES = entitySearchList.FirstOrDefault(e => e.EntityName == "LogFile");
 	        if (thisES != null)
 	        {
 	            foreach (var es in entitySearchList.Where(e => e.EntityName == "TcsLogErrosDash").ToArray())
 	            {
 	              es.EntityName = thisES.EntityName;
 	              es.SubQueryInfo = thisES.SubQueryInfo;
 	              es.EdmEntityName = thisES.EdmEntityName;
 	              es.EdmParentEntityName = thisES.EdmParentEntityName;
 	              es.BaseEntityNames = thisES.BaseEntityNames;
 	            }
 	        }
 
		

	        IEnumerable<LogFile> result = 
	            (from entity0 in LogFile.OnSearchingReplacement(entitySearchList) select entity0);
		
	
	        	

	
	        return result;
	    }
			
	
	    [TcsLogErrosQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TcsLogErrosByEntitySearch.
	    public IQueryable<TcsLogErros> GetTcsLogErrosByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTcsLogErrosByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsLogErrosQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

 	        //Adjust EntityName for MasterFiltering
 	        var thisES = entitySearchList.FirstOrDefault(e => e.EntityName == "TcsLogErros");
 	        if (thisES != null)
 	        {
 	            foreach (var es in entitySearchList.Where(e => e.EntityName == "TcsLogErrosDash").ToArray())
 	            {
 	              es.EntityName = thisES.EntityName;
 	              es.SubQueryInfo = thisES.SubQueryInfo;
 	              es.EdmEntityName = thisES.EdmEntityName;
 	              es.EdmParentEntityName = thisES.EdmParentEntityName;
 	              es.BaseEntityNames = thisES.BaseEntityNames;
 	            }
 	        }
 
	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsLogErros));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsLogErros> result = 
	            (from entity0 in this.DbContext.TCS_LOG_ERROS.Where(dynQuery, parameters.ToArray())
                  let entity0Al4 = entity0.GPECON
                  let entity0Al1 = entity0.TCS_AMBIENTE
                  let entity0Al2 = entity0.TCS_APLICACAO
                  let entity0Al3 = entity0.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al5 = entity0.TCS_USUARIO_AUTENTICACAO
                orderby entity0.DATA_ERRO descending
	            
	            	
	            select new TcsLogErros()		
	            {
	            
                DataErro = entity0.DATA_ERRO
                , DescricaoAmbiente = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al2.DESCRICAO_APLICACAO
                , Empresa = entity0Al3.NOME_EMPRESA
                , EnderecoWeb = entity0.ENDERECO_WEB
                , Gpecon = entity0Al4.NOME_EMPRESA
                , IdAplicacao = entity0Al2.ID_APLICACAO
                , IdLinxEmpresa = entity0Al3.ID_LINX
                , IdLinxGpecon = entity0Al4.ID_LINX
                , IdTcsAmbiente = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsLogErros = entity0.ID_TCS_LOG_ERROS
                , IdUsuario = entity0Al5.ID_USUARIO
                , MensagemExcecao = entity0.MENSAGEM_EXCECAO
                , MensagemExcecaoInterna = entity0.MENSAGEM_EXCECAO_INTERNA
                , MetodoHttp = entity0.METODO_HTTP
                , NomeAcao = entity0.NOME_ACAO
                , NomeControlador = entity0.NOME_CONTROLADOR
                , NomeServidor = entity0.NOME_SERVIDOR
                , PilhaExcecao = entity0.PILHA_EXCECAO
                , UsuarioWindows = entity0.USUARIO_WINDOWS
                , NomeAutenticacao = entity0Al5.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al5.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TcsLogErrosDashQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TcsLogErrosDashByEntitySearchNoAssociations.
	    public IEnumerable<TcsLogErrosDash> GetTcsLogErrosDashByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTcsLogErrosDashByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsLogErrosDashQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsLogErrosDash));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		

	        IEnumerable<TcsLogErrosDash> result = 
	            (from entity0 in TcsLogErrosDash.OnSearchingReplacement(this.DbContext, dynQuery, parameters, entitySearchList) select entity0);
		
	
	        	

	
	        return result;
	    }
			
	
	    [LogFileQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get LogFileByEntitySearchNoAssociations.
	    public IEnumerable<LogFile> GetLogFileByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetLogFileByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new LogFileQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

 	        //Adjust EntityName for MasterFiltering
 	        var thisES = entitySearchList.FirstOrDefault(e => e.EntityName == "LogFile");
 	        if (thisES != null)
 	        {
 	            foreach (var es in entitySearchList.Where(e => e.EntityName == "TcsLogErrosDash").ToArray())
 	            {
 	              es.EntityName = thisES.EntityName;
 	              es.SubQueryInfo = thisES.SubQueryInfo;
 	              es.EdmEntityName = thisES.EdmEntityName;
 	              es.EdmParentEntityName = thisES.EdmParentEntityName;
 	              es.BaseEntityNames = thisES.BaseEntityNames;
 	            }
 	        }
 
		

	        IEnumerable<LogFile> result = 
	            (from entity0 in LogFile.OnSearchingReplacement(entitySearchList) select entity0);
		
	
	        	

	
	        return result;
	    }
			
	
	    [TcsLogErrosQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TcsLogErrosByEntitySearchNoAssociations.
	    public IQueryable<TcsLogErros> GetTcsLogErrosByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTcsLogErrosByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsLogErrosQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

 	        //Adjust EntityName for MasterFiltering
 	        var thisES = entitySearchList.FirstOrDefault(e => e.EntityName == "TcsLogErros");
 	        if (thisES != null)
 	        {
 	            foreach (var es in entitySearchList.Where(e => e.EntityName == "TcsLogErrosDash").ToArray())
 	            {
 	              es.EntityName = thisES.EntityName;
 	              es.SubQueryInfo = thisES.SubQueryInfo;
 	              es.EdmEntityName = thisES.EdmEntityName;
 	              es.EdmParentEntityName = thisES.EdmParentEntityName;
 	              es.BaseEntityNames = thisES.BaseEntityNames;
 	            }
 	        }
 
	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsLogErros));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsLogErros> result = 
	            (from entity0 in this.DbContext.TCS_LOG_ERROS.Where(dynQuery, parameters.ToArray())
                  let entity0Al4 = entity0.GPECON
                  let entity0Al1 = entity0.TCS_AMBIENTE
                  let entity0Al2 = entity0.TCS_APLICACAO
                  let entity0Al3 = entity0.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al5 = entity0.TCS_USUARIO_AUTENTICACAO
                orderby entity0.DATA_ERRO descending
	            
	            	
	            select new TcsLogErros()		
	            {
	            
                DataErro = entity0.DATA_ERRO
                , DescricaoAmbiente = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al2.DESCRICAO_APLICACAO
                , Empresa = entity0Al3.NOME_EMPRESA
                , EnderecoWeb = entity0.ENDERECO_WEB
                , Gpecon = entity0Al4.NOME_EMPRESA
                , IdAplicacao = entity0Al2.ID_APLICACAO
                , IdLinxEmpresa = entity0Al3.ID_LINX
                , IdLinxGpecon = entity0Al4.ID_LINX
                , IdTcsAmbiente = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsLogErros = entity0.ID_TCS_LOG_ERROS
                , IdUsuario = entity0Al5.ID_USUARIO
                , MensagemExcecao = entity0.MENSAGEM_EXCECAO
                , MensagemExcecaoInterna = entity0.MENSAGEM_EXCECAO_INTERNA
                , MetodoHttp = entity0.METODO_HTTP
                , NomeAcao = entity0.NOME_ACAO
                , NomeControlador = entity0.NOME_CONTROLADOR
                , NomeServidor = entity0.NOME_SERVIDOR
                , PilhaExcecao = entity0.PILHA_EXCECAO
                , UsuarioWindows = entity0.USUARIO_WINDOWS
                , NomeAutenticacao = entity0Al5.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al5.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
	
	    [Ignore()]
	    private void SetLogFileBusinessFilter(ref IQueryable<LogFile> query, List<EntitySearch> entitySearchList)
	    {
	    		int idxElement;
	    		string operatorValue;
	    		object value;
	    		//Get query by functions
	    		if (entitySearchList.Count > 0)
	    		{
	    			foreach (EntitySearch search in entitySearchList.Where(e => e.EntityName == "LogFile"))
	    			{

	
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "FileName" || e.Value.ToString() == "System.String.Empty")))
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
	    										System.String tmpFileName1 = (System.String)value;
	    										query = from r in query where r.FileName == tmpFileName1 select r;
	    										break;
	    									case "!=":
	    										System.String tmpFileName2 = (System.String)value;
	    										query = from r in query where r.FileName != tmpFileName2 select r;
	    										break;

	
	    									case "Contains":
	    										System.String tmpFileName7 = (System.String)value;
	    									    query = from r in query where r.FileName.Contains(tmpFileName7) select r;
	    									    break;
	    									case "StartsWith":
	    										System.String tmpFileName8 = (System.String)value;
	    									    query = from r in query where r.FileName.StartsWith(tmpFileName8) select r;
	    									    break;
	    									case "EndsWith":
	    										System.String tmpFileName9 = (System.String)value;
	    									    query = from r in query where r.FileName.EndsWith(tmpFileName9) select r;
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
	
			
	
	    [TcsLogErrosDashQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedTcsLogErrosDash.
	    public IEnumerable<TcsLogErrosDash> GetPagedTcsLogErrosDash(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedTcsLogErrosDash")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsLogErrosDashQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsLogErrosDash));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		

	        IEnumerable<TcsLogErrosDash> result = 
	            (from entity0 in TcsLogErrosDash.OnSearchingReplacement(this.DbContext, dynQuery, parameters, entitySearchList) select entity0);
		
	
	        	

	
	        return result;
	    }
			
	
	    [LogFileQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedLogFile.
	    public IEnumerable<LogFile> GetPagedLogFile(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedLogFile")))
 	        {
 	             AuthorizationResult authorizationResult = (new LogFileQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

 	        //Adjust EntityName for MasterFiltering
 	        var thisES = entitySearchList.FirstOrDefault(e => e.EntityName == "LogFile");
 	        if (thisES != null)
 	        {
 	            foreach (var es in entitySearchList.Where(e => e.EntityName == "TcsLogErrosDash").ToArray())
 	            {
 	              es.EntityName = thisES.EntityName;
 	              es.SubQueryInfo = thisES.SubQueryInfo;
 	              es.EdmEntityName = thisES.EdmEntityName;
 	              es.EdmParentEntityName = thisES.EdmParentEntityName;
 	              es.BaseEntityNames = thisES.BaseEntityNames;
 	            }
 	        }
 
		

	        IEnumerable<LogFile> result = 
	            (from entity0 in LogFile.OnSearchingReplacement(entitySearchList) select entity0);
		
	
	        	

	
	        return result;
	    }
			
	
	    [TcsLogErrosQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedTcsLogErros.
	    public IQueryable<TcsLogErros> GetPagedTcsLogErros(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedTcsLogErros")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsLogErrosQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

 	        //Adjust EntityName for MasterFiltering
 	        var thisES = entitySearchList.FirstOrDefault(e => e.EntityName == "TcsLogErros");
 	        if (thisES != null)
 	        {
 	            foreach (var es in entitySearchList.Where(e => e.EntityName == "TcsLogErrosDash").ToArray())
 	            {
 	              es.EntityName = thisES.EntityName;
 	              es.SubQueryInfo = thisES.SubQueryInfo;
 	              es.EdmEntityName = thisES.EdmEntityName;
 	              es.EdmParentEntityName = thisES.EdmParentEntityName;
 	              es.BaseEntityNames = thisES.BaseEntityNames;
 	            }
 	        }
 
	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsLogErros));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsLogErros> result = 
	            (from entity0 in this.DbContext.TCS_LOG_ERROS.Where(dynQuery, parameters.ToArray())
                  let entity0Al4 = entity0.GPECON
                  let entity0Al1 = entity0.TCS_AMBIENTE
                  let entity0Al2 = entity0.TCS_APLICACAO
                  let entity0Al3 = entity0.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al5 = entity0.TCS_USUARIO_AUTENTICACAO
                orderby entity0.ID_TCS_LOG_ERROS ascending
	            
	            	
	            select new TcsLogErros()		
	            {
	            
                DataErro = entity0.DATA_ERRO
                , DescricaoAmbiente = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al2.DESCRICAO_APLICACAO
                , Empresa = entity0Al3.NOME_EMPRESA
                , EnderecoWeb = entity0.ENDERECO_WEB
                , Gpecon = entity0Al4.NOME_EMPRESA
                , IdAplicacao = entity0Al2.ID_APLICACAO
                , IdLinxEmpresa = entity0Al3.ID_LINX
                , IdLinxGpecon = entity0Al4.ID_LINX
                , IdTcsAmbiente = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsLogErros = entity0.ID_TCS_LOG_ERROS
                , IdUsuario = entity0Al5.ID_USUARIO
                , MensagemExcecao = entity0.MENSAGEM_EXCECAO
                , MensagemExcecaoInterna = entity0.MENSAGEM_EXCECAO_INTERNA
                , MetodoHttp = entity0.METODO_HTTP
                , NomeAcao = entity0.NOME_ACAO
                , NomeControlador = entity0.NOME_CONTROLADOR
                , NomeServidor = entity0.NOME_SERVIDOR
                , PilhaExcecao = entity0.PILHA_EXCECAO
                , UsuarioWindows = entity0.USUARIO_WINDOWS
                , NomeAutenticacao = entity0Al5.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al5.NOME_USUARIO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsLogErrosDashCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsLogErrosDash));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_LOG_ERROS.Where(dynQuery, parameters.ToArray())
                  let entityAl3 = entity.GPECON
                  let entityAl1 = entity.TCS_AMBIENTE
                  let entityAl2 = entity.TCS_APLICACAO
                  let entityAl4 = entity.TCS_EMPRESA_AUTENTICACAO
                  let entityAl5 = entity.TCS_USUARIO_AUTENTICACAO
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetLogFileCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
		
	    [Ignore]
	    public int GetTcsLogErrosCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsLogErros));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_LOG_ERROS.Where(dynQuery, parameters.ToArray())
                  let entityAl4 = entity.GPECON
                  let entityAl1 = entity.TCS_AMBIENTE
                  let entityAl2 = entity.TCS_APLICACAO
                  let entityAl3 = entity.TCS_EMPRESA_AUTENTICACAO
                  let entityAl5 = entity.TCS_USUARIO_AUTENTICACAO
	            
	            select 1
	            ).Count();	
		
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    [TcsLogErrosDashUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update TcsLogErrosDash.
	    public void UpdateTcsLogErrosDash(TcsLogErrosDash entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateTcsLogErrosDash")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsLogErrosDashUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    [TcsLogErrosDashInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert TcsLogErrosDash.
	    public void InsertTcsLogErrosDash(TcsLogErrosDash entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertTcsLogErrosDash")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsLogErrosDashInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    [TcsLogErrosDashDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete TcsLogErrosDash.
	    public void DeleteTcsLogErrosDash(TcsLogErrosDash entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteTcsLogErrosDash")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsLogErrosDashDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    [LogFileUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update LogFile.
	    public void UpdateLogFile(LogFile entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateLogFile")))
 	        {
 	             AuthorizationResult authorizationResult = (new LogFileUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	    }

	    [LogFileInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert LogFile.
	    public void InsertLogFile(LogFile entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertLogFile")))
 	        {
 	             AuthorizationResult authorizationResult = (new LogFileInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	    }

	    [LogFileDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete LogFile.
	    public void DeleteLogFile(LogFile entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteLogFile")))
 	        {
 	             AuthorizationResult authorizationResult = (new LogFileDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	    }
		
			
	    [TcsLogErrosUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update TcsLogErros.
	    public void UpdateTcsLogErros(TcsLogErros entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateTcsLogErros")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsLogErrosUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.TcsLogErrosDash.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsLogErrosDash) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsLogErrosDash); 	
	            

	
	        }
	
	    }

	    [TcsLogErrosInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert TcsLogErros.
	    public void InsertTcsLogErros(TcsLogErros entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertTcsLogErros")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsLogErrosInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.TcsLogErrosDash.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsLogErrosDash) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsLogErrosDash);
	            

	
	        }
	
	    }

	    [TcsLogErrosDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete TcsLogErros.
	    public void DeleteTcsLogErros(TcsLogErros entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteTcsLogErros")))
 	        {
 	             AuthorizationResult authorizationResult = (new TcsLogErrosDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.TcsLogErrosDash.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsLogErrosDash) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsLogErrosDash);
	            

	
	        }

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}