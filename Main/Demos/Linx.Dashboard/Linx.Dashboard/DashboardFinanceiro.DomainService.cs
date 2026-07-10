					
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
using Linx.Operacional.BM;

namespace Linx.Dashboard.DashboardFinanceiro
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="LjvAtendimento.EntityUniqueKey", IsUpdatable=false, EdmName="Linx.Operacional.BM.LinxOperacional")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[LjvAtendimento];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[true];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[true];Entities[TBC_FILIAL:IdFilialPfj|TBC_GRUPO_ECONOMICO:IdGpecon|LJV_LOJA:IdLoja];SubQueryInfo[];EdmEntityName[LJV_ATENDIMENTO];EntityRelations[LJV_MOTIVO_CANCELAMENTO(LJV_MOTIVO_CANCELAMENTO)#LJV_MOTIVO_DEVOLUCAO(LJV_MOTIVO_DEVOLUCAO)#PRD_TABELA_PRECO(PRD_TABELA_PRECO)#PRD_TABELA_PRECO_GRUPO(PRD_TABELA_PRECO_GRUPO)#TBC_GRUPO_ECONOMICO(TBC_GRUPO_ECONOMICO)#GPECON_SUPERIOR(TBC_GRUPO_ECONOMICO)#TBC_PFJ(TBC_PFJ)#TCS_MOEDA_INDICADOR(TCS_MOEDA_INDICADOR)#TAB_PRECO_CUSTO(PRD_TABELA_PRECO)#TAB_PRECO_BASE(PRD_TABELA_PRECO)#TBC_BANDEIRA_REDE(TBC_BANDEIRA_REDE)#TBC_FILIAL(TBC_FILIAL)#MATRIZ_CONTABIL(TBC_FILIAL)#LCF_AGRUPADOR_REGRA_FILIAL(LCF_AGRUPADOR_REGRA_FILIAL)#GEO_PAIS(GEO_PAIS)#LJV_LOJA(LJV_LOJA)#TBC_REGIAO_COMERCIAL(TBC_REGIAO_COMERCIAL)#REGIAO_SUPERIOR(TBC_REGIAO_COMERCIAL)#LJV_CANAL_VENDA(LJV_CANAL_VENDA)#STK_DEPOSITO(STK_DEPOSITO)#GEO_CEP(GEO_CEP)#GEO_MUNICIPIO(GEO_MUNICIPIO)#GEO_UNIDADE_FEDERACAO(GEO_UNIDADE_FEDERACAO)#AGRUPAMENTO_SORTIMENTO(LJV_LOJA_AGRUPAMENTO)#AGRUPAMENTO_COMERCIAL(LJV_LOJA_AGRUPAMENTO)#LJV_OPERACAO_VENDA(LJV_OPERACAO_VENDA)#GPECON_FILTRO(TBC_GRUPO_ECONOMICO)#LJV_CAIXA_CTRL(LJV_CAIXA_CTRL)#LJV_CTRL(LJV_CTRL)#LJV_VENDEDOR(LJV_VENDEDOR)#SUPERVISOR(LJV_VENDEDOR)#TCS_USUARIO(TCS_USUARIO)#STK_ROMANEIO(STK_ROMANEIO)#STK_ROMANEIO1(STK_ROMANEIO)#TBC_FILIAL1(TBC_FILIAL)#LCF_OPERACAO_FINALIDADE(LCF_OPERACAO_FINALIDADE)#MVD_DOCUMENTO_TIPO(MVD_DOCUMENTO_TIPO)#LJV_LOJA1(LJV_LOJA)#STK_ROMANEIO_AJUSTE_LISTA(STK_ROMANEIO_AJUSTE)#STK_ROMANEIO_NF_LISTA(STK_ROMANEIO_NF)#LJV_VENDEDOR1(LJV_VENDEDOR)#LJV_TERMINAL(LJV_TERMINAL)#LJV_ECF_OPERACAO(LJV_ECF_OPERACAO)#LJV_ECF(LJV_ECF)#LJV_ECF_REDUCAO_LISTA(LJV_ECF_REDUCAO)#CRM_PFJ(CRM_PFJ)#CRM_ESCOLARIDADE(CRM_ESCOLARIDADE)#CRM_PROFISSAO(CRM_PROFISSAO)#CRM_FAIXA_RENDA(CRM_FAIXA_RENDA)#CRM_TIPO_CLIENTE(CRM_TIPO_CLIENTE)#CRM_SUBTIPO_CLIENTE(CRM_SUBTIPO_CLIENTE)#CRM_CLASSIFICACAO_CLIENTE(CRM_CLASSIFICACAO_CLIENTE)#CRM_SEGMENTO_CLIENTE(CRM_SEGMENTO_CLIENTE)#LCF_REGIME_TRIBUTARIO(LCF_REGIME_TRIBUTARIO)#LCF_INDICADOR_FISCAL_PFJ(LCF_INDICADOR_FISCAL_PFJ)#CRM_CARGA_LOJA_VENDA(CRM_CARGA_LOJA_VENDA)#NTS_CARGA_LOJA_VENDEDOR_GERENTE(CRM_CARGA_LOJA_VENDEDOR)#NTS_CARGA_LOJA_VENDEDOR_CAIXA(CRM_CARGA_LOJA_VENDEDOR)#LJV_ATENDIMENTO_INTEGRACAO_FISCAL_LISTA(LJV_ATENDIMENTO_INTEGRACAO_FISCAL)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "LjvAtendimento")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Dashboard.DashboardFinanceiro.LjvAtendimento")]
	public partial class LjvAtendimento : Linx.Data.Entity
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

	    public virtual void ResetChangeState()
	    {
	      this.ChangeState = "N";
	    }

	    #endregion Flat Entities

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For CodBandeiraRede
	    partial void OnCodBandeiraRedeChanging(System.String value);
	    partial void OnCodBandeiraRedeChanged();

	    private System.String _CodBandeiraRede;

	    [DataMember(Name = "CodBandeiraRede", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cod. Bandeira Rede", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(25)]
	    [FunctionalPoint("Precision[25:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpLjvLoja];LookUpTitle[Seleção de (Cod. Bandeira Rede)];LookUpQuery[executeLookUpLjvLoja];LookUpFinalize[finalizeLookUpLjvLoja];LookUpDisplayColumns[{\"CodLoja\" : \"Cod Loja\", \"DescLoja\" : \"Desc Loja\", \"IdLoja\" : \"Id Loja\", \"CodBandeiraRede\" : \"Cod Bandeira Rede\", \"DescBandeiraRede\" : \"Desc Bandeira Rede\", \"IdBandeiraRede\" : \"Id Bandeira Rede\"}];LookUpColumns[{\"CodLoja\" : true, \"DescLoja\" : true, \"IdLoja\" : true, \"CodBandeiraRede\" : true, \"DescBandeiraRede\" : true, \"IdBandeiraRede\" : true}];FilterDataKey[LJV_ATENDIMENTO.LJV_LOJA.TBC_BANDEIRA_REDE.COD_BANDEIRA_REDE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#CodBandeiraRede#false##25:0##Cod Bandeira Rede#3#true##::LookUpLjvLoja##false#false#LJV_LOJA#LJV_LOJA#Linx.Dashboard.DashboardFinanceiro#IQueryable#CodBandeiraRede,DescBandeiraRede,IdBandeiraRede[CodBandeiraRede,DescBandeiraRede,IdBandeiraRede]#CodLoja[CodBandeiraRede=CodBandeiraRede,DescBandeiraRede=DescBandeiraRede,IdBandeiraRede=IdBandeiraRede];DescLoja[CodBandeiraRede=CodBandeiraRede,DescBandeiraRede=DescBandeiraRede,IdBandeiraRede=IdBandeiraRede];IdLoja[CodBandeiraRede=CodBandeiraRede,DescBandeiraRede=DescBandeiraRede,IdBandeiraRede=IdBandeiraRede]#true#false", EdmKey="LJV_ATENDIMENTO.LJV_LOJA.TBC_BANDEIRA_REDE.COD_BANDEIRA_REDE")]
	    public System.String CodBandeiraRede
	    {
	    	    get
	    	    {
	    	          return _CodBandeiraRede;
	    	    }
	    	    set
	    	    {
	    	          if (this._CodBandeiraRede != value)
	    	          {
	    	              this.ValidateProperty("CodBandeiraRede", value);
	    	              this.OnCodBandeiraRedeChanging(value);
	    	              this.RaiseDataMemberChanging("CodBandeiraRede");
	    	              this._CodBandeiraRede = value;
	    	              this.RaiseDataMemberChanged("CodBandeiraRede");
	    	              this.OnCodBandeiraRedeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For CodigoFilial
	    partial void OnCodigoFilialChanging(System.String value);
	    partial void OnCodigoFilialChanged();

	    private System.String _CodigoFilial;

	    [DataMember(Name = "CodigoFilial", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Codigo Filial", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(18)]
	    [FunctionalPoint("Precision[18:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTbcFilial];LookUpTitle[Seleção de (Codigo Filial)];LookUpQuery[executeLookUpTbcFilial];LookUpFinalize[finalizeLookUpTbcFilial];LookUpDisplayColumns[{\"IdFilialPfj\" : \"Id Filial Pfj\", \"CodigoFilial\" : \"Codigo Filial\"}];LookUpColumns[{\"IdFilialPfj\" : true, \"CodigoFilial\" : true}];FilterDataKey[LJV_ATENDIMENTO.TBC_FILIAL.CODIGO_FILIAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#CodigoFilial#false##18:0##Codigo Filial#1#true##::LookUpTbcFilial##false#false#TBC_FILIAL#TBC_FILIAL#Linx.Dashboard.DashboardFinanceiro#IQueryable###true#false", EdmKey="LJV_ATENDIMENTO.TBC_FILIAL.CODIGO_FILIAL")]
	    public System.String CodigoFilial
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
	    //Extensibility Partial Method Definitions For CodLoja
	    partial void OnCodLojaChanging(System.String value);
	    partial void OnCodLojaChanged();

	    private System.String _CodLoja;

	    [DataMember(IsRequired = true, Name = "CodLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cod. Loja", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpLjvLoja];LookUpTitle[Seleção de (Cod. Loja)];LookUpQuery[executeLookUpLjvLoja];LookUpFinalize[finalizeLookUpLjvLoja];LookUpDisplayColumns[{\"CodLoja\" : \"Cod Loja\", \"DescLoja\" : \"Desc Loja\", \"IdLoja\" : \"Id Loja\", \"CodBandeiraRede\" : \"Cod Bandeira Rede\", \"DescBandeiraRede\" : \"Desc Bandeira Rede\", \"IdBandeiraRede\" : \"Id Bandeira Rede\"}];LookUpColumns[{\"CodLoja\" : true, \"DescLoja\" : true, \"IdLoja\" : true, \"CodBandeiraRede\" : true, \"DescBandeiraRede\" : true, \"IdBandeiraRede\" : true}];FilterDataKey[LJV_ATENDIMENTO.LJV_LOJA.COD_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#CodLoja#false##20:0##Cod Loja#0#true##::LookUpLjvLoja##false#false#LJV_LOJA#LJV_LOJA#Linx.Dashboard.DashboardFinanceiro#IQueryable#CodBandeiraRede,DescBandeiraRede,IdBandeiraRede[CodBandeiraRede,DescBandeiraRede,IdBandeiraRede]#CodLoja[CodBandeiraRede=CodBandeiraRede,DescBandeiraRede=DescBandeiraRede,IdBandeiraRede=IdBandeiraRede];DescLoja[CodBandeiraRede=CodBandeiraRede,DescBandeiraRede=DescBandeiraRede,IdBandeiraRede=IdBandeiraRede];IdLoja[CodBandeiraRede=CodBandeiraRede,DescBandeiraRede=DescBandeiraRede,IdBandeiraRede=IdBandeiraRede]#true#false", EdmKey="LJV_ATENDIMENTO.LJV_LOJA.COD_LOJA")]
	    public System.String CodLoja
	    {
	    	    get
	    	    {
	    	          if (_CodLoja.IsNullOrEmpty())
	    	             _CodLoja =  String.Empty;
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
	    //Extensibility Partial Method Definitions For DataAtendimento
	    partial void OnDataAtendimentoChanging(System.DateTime value);
	    partial void OnDataAtendimentoChanged();

	    private System.DateTime _DataAtendimento;

	    [DataMember(IsRequired = true, Name = "DataAtendimento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Data Atendimento", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_ATENDIMENTO.DATA_ATENDIMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_ATENDIMENTO.DATA_ATENDIMENTO")]
	    public System.DateTime DataAtendimento
	    {
	    	    get
	    	    {
	    	          return _DataAtendimento;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataAtendimento != value)
	    	          {
	    	              this.ValidateProperty("DataAtendimento", value);
	    	              this.OnDataAtendimentoChanging(value);
	    	              this.RaiseDataMemberChanging("DataAtendimento");
	    	              this._DataAtendimento = value;
	    	              this.RaiseDataMemberChanged("DataAtendimento");
	    	              this.OnDataAtendimentoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescBandeiraRede
	    partial void OnDescBandeiraRedeChanging(System.String value);
	    partial void OnDescBandeiraRedeChanged();

	    private System.String _DescBandeiraRede;

	    [DataMember(Name = "DescBandeiraRede", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bandeira Rede", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[CodBandeiraRede];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpLjvLoja];LookUpTitle[Seleção de (Bandeira Rede)];LookUpQuery[executeLookUpLjvLoja];LookUpFinalize[finalizeLookUpLjvLoja];LookUpDisplayColumns[{\"CodLoja\" : \"Cod Loja\", \"DescLoja\" : \"Desc Loja\", \"IdLoja\" : \"Id Loja\", \"CodBandeiraRede\" : \"Cod Bandeira Rede\", \"DescBandeiraRede\" : \"Desc Bandeira Rede\", \"IdBandeiraRede\" : \"Id Bandeira Rede\"}];LookUpColumns[{\"CodLoja\" : true, \"DescLoja\" : true, \"IdLoja\" : true, \"CodBandeiraRede\" : true, \"DescBandeiraRede\" : true, \"IdBandeiraRede\" : true}];FilterDataKey[LJV_ATENDIMENTO.LJV_LOJA.TBC_BANDEIRA_REDE.DESC_BANDEIRA_REDE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescBandeiraRede#false##60:0##Desc Bandeira Rede#4#true##::LookUpLjvLoja##false#false#LJV_LOJA#LJV_LOJA#Linx.Dashboard.DashboardFinanceiro#IQueryable#CodBandeiraRede,DescBandeiraRede,IdBandeiraRede[CodBandeiraRede,DescBandeiraRede,IdBandeiraRede]#CodLoja[CodBandeiraRede=CodBandeiraRede,DescBandeiraRede=DescBandeiraRede,IdBandeiraRede=IdBandeiraRede];DescLoja[CodBandeiraRede=CodBandeiraRede,DescBandeiraRede=DescBandeiraRede,IdBandeiraRede=IdBandeiraRede];IdLoja[CodBandeiraRede=CodBandeiraRede,DescBandeiraRede=DescBandeiraRede,IdBandeiraRede=IdBandeiraRede]#true#false", EdmKey="LJV_ATENDIMENTO.LJV_LOJA.TBC_BANDEIRA_REDE.DESC_BANDEIRA_REDE")]
	    public System.String DescBandeiraRede
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
	    //Extensibility Partial Method Definitions For DescLoja
	    partial void OnDescLojaChanging(System.String value);
	    partial void OnDescLojaChanged();

	    private System.String _DescLoja;

	    [DataMember(IsRequired = true, Name = "DescLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Loja", Description="", Order = 14, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[CodLoja];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpLjvLoja];LookUpTitle[Seleção de (Loja)];LookUpQuery[executeLookUpLjvLoja];LookUpFinalize[finalizeLookUpLjvLoja];LookUpDisplayColumns[{\"CodLoja\" : \"Cod Loja\", \"DescLoja\" : \"Desc Loja\", \"IdLoja\" : \"Id Loja\", \"CodBandeiraRede\" : \"Cod Bandeira Rede\", \"DescBandeiraRede\" : \"Desc Bandeira Rede\", \"IdBandeiraRede\" : \"Id Bandeira Rede\"}];LookUpColumns[{\"CodLoja\" : true, \"DescLoja\" : true, \"IdLoja\" : true, \"CodBandeiraRede\" : true, \"DescBandeiraRede\" : true, \"IdBandeiraRede\" : true}];FilterDataKey[LJV_ATENDIMENTO.LJV_LOJA.DESC_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescLoja#false##60:0##Desc Loja#1#true##::LookUpLjvLoja##false#false#LJV_LOJA#LJV_LOJA#Linx.Dashboard.DashboardFinanceiro#IQueryable#CodBandeiraRede,DescBandeiraRede,IdBandeiraRede[CodBandeiraRede,DescBandeiraRede,IdBandeiraRede]#CodLoja[CodBandeiraRede=CodBandeiraRede,DescBandeiraRede=DescBandeiraRede,IdBandeiraRede=IdBandeiraRede];DescLoja[CodBandeiraRede=CodBandeiraRede,DescBandeiraRede=DescBandeiraRede,IdBandeiraRede=IdBandeiraRede];IdLoja[CodBandeiraRede=CodBandeiraRede,DescBandeiraRede=DescBandeiraRede,IdBandeiraRede=IdBandeiraRede]#true#false", EdmKey="LJV_ATENDIMENTO.LJV_LOJA.DESC_LOJA")]
	    public System.String DescLoja
	    {
	    	    get
	    	    {
	    	          if (_DescLoja.IsNullOrEmpty())
	    	             _DescLoja =  String.Empty;
	    	          return _DescLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescLoja != value)
	    	          {
	    	              this.ValidateProperty("DescLoja", value);
	    	              this.OnDescLojaChanging(value);
	    	              this.RaiseDataMemberChanging("DescLoja");
	    	              this._DescLoja = value;
	    	              this.RaiseDataMemberChanged("DescLoja");
	    	              this.OnDescLojaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdBandeiraRede
	    partial void OnIdBandeiraRedeChanging(System.Nullable<Int32> value);
	    partial void OnIdBandeiraRedeChanged();

	    private System.Nullable<Int32> _IdBandeiraRede;

	    [DataMember(Name = "IdBandeiraRede", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Bandeira Rede", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpLjvLoja];LookUpTitle[Seleção de (Id Bandeira Rede)];LookUpQuery[executeLookUpLjvLoja];LookUpFinalize[finalizeLookUpLjvLoja];LookUpDisplayColumns[{\"CodLoja\" : \"Cod Loja\", \"DescLoja\" : \"Desc Loja\", \"IdLoja\" : \"Id Loja\", \"CodBandeiraRede\" : \"Cod Bandeira Rede\", \"DescBandeiraRede\" : \"Desc Bandeira Rede\", \"IdBandeiraRede\" : \"Id Bandeira Rede\"}];LookUpColumns[{\"CodLoja\" : true, \"DescLoja\" : true, \"IdLoja\" : true, \"CodBandeiraRede\" : true, \"DescBandeiraRede\" : true, \"IdBandeiraRede\" : true}];FilterDataKey[LJV_ATENDIMENTO.LJV_LOJA.TBC_BANDEIRA_REDE.ID_BANDEIRA_REDE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<Int32>#IdBandeiraRede#false##12:0##Id Bandeira Rede#5#true##::LookUpLjvLoja##false#false#LJV_LOJA#LJV_LOJA#Linx.Dashboard.DashboardFinanceiro#IQueryable#CodBandeiraRede,DescBandeiraRede,IdBandeiraRede[CodBandeiraRede,DescBandeiraRede,IdBandeiraRede]#CodLoja[CodBandeiraRede=CodBandeiraRede,DescBandeiraRede=DescBandeiraRede,IdBandeiraRede=IdBandeiraRede];DescLoja[CodBandeiraRede=CodBandeiraRede,DescBandeiraRede=DescBandeiraRede,IdBandeiraRede=IdBandeiraRede];IdLoja[CodBandeiraRede=CodBandeiraRede,DescBandeiraRede=DescBandeiraRede,IdBandeiraRede=IdBandeiraRede]#true#false", EdmKey="LJV_ATENDIMENTO.LJV_LOJA.TBC_BANDEIRA_REDE.ID_BANDEIRA_REDE")]
	    public System.Nullable<Int32> IdBandeiraRede
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
	    //Extensibility Partial Method Definitions For IdFilialPfj
	    partial void OnIdFilialPfjChanging(System.Nullable<Int32> value);
	    partial void OnIdFilialPfjChanged();

	    private System.Nullable<Int32> _IdFilialPfj;

	    [DataMember(Name = "IdFilialPfj", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Filial Pfj", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTbcFilial];LookUpTitle[Seleção de (Id Filial Pfj)];LookUpQuery[executeLookUpTbcFilial];LookUpFinalize[finalizeLookUpTbcFilial];LookUpDisplayColumns[{\"IdFilialPfj\" : \"Id Filial Pfj\", \"CodigoFilial\" : \"Codigo Filial\"}];LookUpColumns[{\"IdFilialPfj\" : true, \"CodigoFilial\" : true}];FilterDataKey[LJV_ATENDIMENTO.TBC_FILIAL.ID_FILIAL_PFJ];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<Int32>#IdFilialPfj#true##12:0##Id Filial Pfj#0#true##::LookUpTbcFilial##false#false#TBC_FILIAL#TBC_FILIAL#Linx.Dashboard.DashboardFinanceiro#IQueryable###true#false", EdmKey="LJV_ATENDIMENTO.TBC_FILIAL.ID_FILIAL_PFJ")]
	    public System.Nullable<Int32> IdFilialPfj
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
	    //Extensibility Partial Method Definitions For IdGpecon
	    partial void OnIdGpeconChanging(Int32 value);
	    partial void OnIdGpeconChanged();

	    private Int32 _IdGpecon;

	    [DataMember(IsRequired = true, Name = "IdGpecon", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Gpecon", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTbcGrupoEconomico];LookUpTitle[Seleção de (Id Gpecon)];LookUpQuery[executeLookUpTbcGrupoEconomico];LookUpFinalize[finalizeLookUpTbcGrupoEconomico];LookUpDisplayColumns[{\"IdGpecon\" : \"Id Gpecon\"}];LookUpColumns[{\"IdGpecon\" : true}];FilterDataKey[LJV_ATENDIMENTO.TBC_GRUPO_ECONOMICO.ID_GPECON];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdGpecon#true##12:0##Id Gpecon#0#true##::LookUpTbcGrupoEconomico##false#false#TBC_GRUPO_ECONOMICO#TBC_GRUPO_ECONOMICO#Linx.Dashboard.DashboardFinanceiro#IQueryable###true#false", EdmKey="LJV_ATENDIMENTO.TBC_GRUPO_ECONOMICO.ID_GPECON")]
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
	    //Extensibility Partial Method Definitions For IdLoja
	    partial void OnIdLojaChanging(Int32 value);
	    partial void OnIdLojaChanged();

	    private Int32 _IdLoja;

	    [DataMember(IsRequired = true, Name = "IdLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Loja", Description="", Order = 24, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpLjvLoja];LookUpTitle[Seleção de (Id Loja)];LookUpQuery[executeLookUpLjvLoja];LookUpFinalize[finalizeLookUpLjvLoja];LookUpDisplayColumns[{\"CodLoja\" : \"Cod Loja\", \"DescLoja\" : \"Desc Loja\", \"IdLoja\" : \"Id Loja\", \"CodBandeiraRede\" : \"Cod Bandeira Rede\", \"DescBandeiraRede\" : \"Desc Bandeira Rede\", \"IdBandeiraRede\" : \"Id Bandeira Rede\"}];LookUpColumns[{\"CodLoja\" : true, \"DescLoja\" : true, \"IdLoja\" : true, \"CodBandeiraRede\" : true, \"DescBandeiraRede\" : true, \"IdBandeiraRede\" : true}];FilterDataKey[LJV_ATENDIMENTO.LJV_LOJA.ID_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdLoja#true##12:0##Id Loja#2#true##::LookUpLjvLoja##false#false#LJV_LOJA#LJV_LOJA#Linx.Dashboard.DashboardFinanceiro#IQueryable#CodBandeiraRede,DescBandeiraRede,IdBandeiraRede[CodBandeiraRede,DescBandeiraRede,IdBandeiraRede]#CodLoja[CodBandeiraRede=CodBandeiraRede,DescBandeiraRede=DescBandeiraRede,IdBandeiraRede=IdBandeiraRede];DescLoja[CodBandeiraRede=CodBandeiraRede,DescBandeiraRede=DescBandeiraRede,IdBandeiraRede=IdBandeiraRede];IdLoja[CodBandeiraRede=CodBandeiraRede,DescBandeiraRede=DescBandeiraRede,IdBandeiraRede=IdBandeiraRede]#true#false", EdmKey="LJV_ATENDIMENTO.LJV_LOJA.ID_LOJA")]
	    public Int32 IdLoja
	    {
	    	    get
	    	    {
	    	          return _IdLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLoja != value)
	    	          {
	    	              this.ValidateProperty("IdLoja", value);
	    	              this.OnIdLojaChanging(value);
	    	              this.RaiseDataMemberChanging("IdLoja");
	    	              this._IdLoja = value;
	    	              this.RaiseDataMemberChanged("IdLoja");
	    	              this.OnIdLojaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ValorCupomFiscal
	    partial void OnValorCupomFiscalChanging(System.Nullable<System.Decimal> value);
	    partial void OnValorCupomFiscalChanged();

	    private System.Nullable<System.Decimal> _ValorCupomFiscal;

	    [DataMember(Name = "ValorCupomFiscal", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Valor Cupom Fiscal", Description="", Order = 18, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[14:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[Sum];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[LJV_ATENDIMENTO.VALOR_CUPOM_FISCAL];IsMeasure[true]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_ATENDIMENTO.VALOR_CUPOM_FISCAL")]
	    public System.Nullable<System.Decimal> ValorCupomFiscal
	    {
	    	    get
	    	    {
	    	          return _ValorCupomFiscal;
	    	    }
	    	    set
	    	    {
	    	          if (this._ValorCupomFiscal != value)
	    	          {
	    	              this.ValidateProperty("ValorCupomFiscal", value);
	    	              this.OnValorCupomFiscalChanging(value);
	    	              this.RaiseDataMemberChanging("ValorCupomFiscal");
	    	              this._ValorCupomFiscal = value;
	    	              this.RaiseDataMemberChanged("ValorCupomFiscal");
	    	              this.OnValorCupomFiscalChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ValorDescontoSubtotal
	    partial void OnValorDescontoSubtotalChanging(System.Nullable<System.Decimal> value);
	    partial void OnValorDescontoSubtotalChanged();

	    private System.Nullable<System.Decimal> _ValorDescontoSubtotal;

	    [DataMember(Name = "ValorDescontoSubtotal", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Valor Desconto Subtotal", Description="", Order = 20, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[14:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[Sum];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[LJV_ATENDIMENTO.VALOR_DESCONTO_SUBTOTAL];IsMeasure[true]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_ATENDIMENTO.VALOR_DESCONTO_SUBTOTAL")]
	    public System.Nullable<System.Decimal> ValorDescontoSubtotal
	    {
	    	    get
	    	    {
	    	          return _ValorDescontoSubtotal;
	    	    }
	    	    set
	    	    {
	    	          if (this._ValorDescontoSubtotal != value)
	    	          {
	    	              this.ValidateProperty("ValorDescontoSubtotal", value);
	    	              this.OnValorDescontoSubtotalChanging(value);
	    	              this.RaiseDataMemberChanging("ValorDescontoSubtotal");
	    	              this._ValorDescontoSubtotal = value;
	    	              this.RaiseDataMemberChanged("ValorDescontoSubtotal");
	    	              this.OnValorDescontoSubtotalChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ValorTotal
	    partial void OnValorTotalChanging(System.Nullable<System.Decimal> value);
	    partial void OnValorTotalChanged();

	    private System.Nullable<System.Decimal> _ValorTotal;

	    [DataMember(Name = "ValorTotal", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Valor Total", Description="", Order = 26, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[14:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[Sum];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[LJV_ATENDIMENTO.VALOR_TOTAL];IsMeasure[true]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_ATENDIMENTO.VALOR_TOTAL")]
	    public System.Nullable<System.Decimal> ValorTotal
	    {
	    	    get
	    	    {
	    	          return _ValorTotal;
	    	    }
	    	    set
	    	    {
	    	          if (this._ValorTotal != value)
	    	          {
	    	              this.ValidateProperty("ValorTotal", value);
	    	              this.OnValorTotalChanging(value);
	    	              this.RaiseDataMemberChanging("ValorTotal");
	    	              this._ValorTotal = value;
	    	              this.RaiseDataMemberChanged("ValorTotal");
	    	              this.OnValorTotalChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For QtdeAtendimento
	    partial void OnQtdeAtendimentoChanging(int value);
	    partial void OnQtdeAtendimentoChanged();

	    private int _QtdeAtendimento;

	    [DataMember(IsRequired = true, Name = "QtdeAtendimento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Qtde Atendimento", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[Sum];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1];IsMeasure[true]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="1")]
	    public int QtdeAtendimento
	    {
	    	    get
	    	    {
	    	          return _QtdeAtendimento;
	    	    }
	    	    set
	    	    {
	    	          if (this._QtdeAtendimento != value)
	    	          {
	    	              this.ValidateProperty("QtdeAtendimento", value);
	    	              this.OnQtdeAtendimentoChanging(value);
	    	              this.RaiseDataMemberChanging("QtdeAtendimento");
	    	              this._QtdeAtendimento = value;
	    	              this.RaiseDataMemberChanged("QtdeAtendimento");
	    	              this.OnQtdeAtendimentoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For TicketMedio
	    partial void OnTicketMedioChanging(decimal value);
	    partial void OnTicketMedioChanged();

	    private decimal _TicketMedio;

	    [DataMember(IsRequired = true, Name = "TicketMedio", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ticket Médio", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[Sum];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[CASE WHEN (QtdeAtendimento) IS NULL OR (QtdeAtendimento) = 0 THEN 0 ELSE (ValorCupomFiscal) / (QtdeAtendimento) END];IsMeasure[true]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="@Divide[ValorCupomFiscal|QtdeAtendimento]")]
	    public decimal TicketMedio
	    {
	    	    get
	    	    {
	    	          return _TicketMedio;
	    	    }
	    	    set
	    	    {
	    	          if (this._TicketMedio != value)
	    	          {
	    	              this.ValidateProperty("TicketMedio", value);
	    	              this.OnTicketMedioChanging(value);
	    	              this.RaiseDataMemberChanging("TicketMedio");
	    	              this._TicketMedio = value;
	    	              this.RaiseDataMemberChanged("TicketMedio");
	    	              this.OnTicketMedioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For EntityUniqueKey
	    partial void OnEntityUniqueKeyChanging(System.Guid value);
	    partial void OnEntityUniqueKeyChanged();

	    private System.Guid _entityUniqueKey;
	    [DataMember(Name = "EntityUniqueKey", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [RoundtripOriginal()]
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

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "LinxOperacional.LJV_ATENDIMENTO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Operacional.BM.LJV_ATENDIMENTO), QualifiedEntitySetName = "LinxOperacional.LJV_ATENDIMENTO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_ATENDIMENTO.VALOR_TOTAL", Source = "ValorTotal", Target = "VALOR_TOTAL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "LinxOperacional.LJV_ATENDIMENTO", RelationPropertyName = "LJV_ATENDIMENTO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_ATENDIMENTO.DATA_ATENDIMENTO", Source = "DataAtendimento", Target = "DATA_ATENDIMENTO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "LinxOperacional.LJV_ATENDIMENTO", RelationPropertyName = "LJV_ATENDIMENTO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_ATENDIMENTO.LJV_LOJA.ID_LOJA", Source = "IdLoja", Target = "ID_LOJA", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "LinxOperacional.LJV_LOJA", RelationPropertyName = "LJV_LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_ATENDIMENTO.VALOR_CUPOM_FISCAL", Source = "ValorCupomFiscal", Target = "VALOR_CUPOM_FISCAL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "LinxOperacional.LJV_ATENDIMENTO", RelationPropertyName = "LJV_ATENDIMENTO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_ATENDIMENTO.VALOR_DESCONTO_SUBTOTAL", Source = "ValorDescontoSubtotal", Target = "VALOR_DESCONTO_SUBTOTAL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "LinxOperacional.LJV_ATENDIMENTO", RelationPropertyName = "LJV_ATENDIMENTO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_ATENDIMENTO.TBC_FILIAL.ID_FILIAL_PFJ", Source = "IdFilialPfj", Target = "ID_FILIAL_PFJ", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "LinxOperacional.TBC_FILIAL", RelationPropertyName = "TBC_FILIAL" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_ATENDIMENTO.TBC_GRUPO_ECONOMICO.ID_GPECON", Source = "IdGpecon", Target = "ID_GPECON", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "LinxOperacional.TBC_GRUPO_ECONOMICO", RelationPropertyName = "TBC_GRUPO_ECONOMICO" });

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

		

	[LinxPublicationView(PrimaryKeys="LjvAtendimentoVendedor.EntityUniqueKey", IsUpdatable=false, EdmName="Linx.Operacional.BM.LinxOperacional")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[LjvAtendimentoVendedor];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[true];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[true];SubQueryInfo[];EdmEntityName[LJV_ATENDIMENTO_VENDEDOR];EntityRelations[LJV_ATENDIMENTO(LJV_ATENDIMENTO)#LJV_MOTIVO_CANCELAMENTO(LJV_MOTIVO_CANCELAMENTO)#LJV_MOTIVO_DEVOLUCAO(LJV_MOTIVO_DEVOLUCAO)#PRD_TABELA_PRECO(PRD_TABELA_PRECO)#PRD_TABELA_PRECO_GRUPO(PRD_TABELA_PRECO_GRUPO)#TBC_GRUPO_ECONOMICO(TBC_GRUPO_ECONOMICO)#TAB_PRECO_CUSTO(PRD_TABELA_PRECO)#TAB_PRECO_BASE(PRD_TABELA_PRECO)#TCS_MOEDA_INDICADOR(TCS_MOEDA_INDICADOR)#TBC_BANDEIRA_REDE(TBC_BANDEIRA_REDE)#TBC_FILIAL(TBC_FILIAL)#MATRIZ_CONTABIL(TBC_FILIAL)#TBC_PFJ(TBC_PFJ)#LCF_AGRUPADOR_REGRA_FILIAL(LCF_AGRUPADOR_REGRA_FILIAL)#LJV_LOJA(LJV_LOJA)#TBC_REGIAO_COMERCIAL(TBC_REGIAO_COMERCIAL)#REGIAO_SUPERIOR(TBC_REGIAO_COMERCIAL)#LJV_CANAL_VENDA(LJV_CANAL_VENDA)#STK_DEPOSITO(STK_DEPOSITO)#GEO_CEP(GEO_CEP)#GEO_MUNICIPIO(GEO_MUNICIPIO)#AGRUPAMENTO_SORTIMENTO(LJV_LOJA_AGRUPAMENTO)#AGRUPAMENTO_COMERCIAL(LJV_LOJA_AGRUPAMENTO)#LJV_OPERACAO_VENDA(LJV_OPERACAO_VENDA)#GPECON_FILTRO(TBC_GRUPO_ECONOMICO)#GPECON_SUPERIOR(TBC_GRUPO_ECONOMICO)#LJV_CAIXA_CTRL(LJV_CAIXA_CTRL)#LJV_CTRL(LJV_CTRL)#LJV_VENDEDOR(LJV_VENDEDOR)#STK_ROMANEIO(STK_ROMANEIO)#LJV_VENDEDOR1(LJV_VENDEDOR)#SUPERVISOR(LJV_VENDEDOR)#TCS_USUARIO(TCS_USUARIO)#LJV_TERMINAL(LJV_TERMINAL)#LJV_ECF_OPERACAO(LJV_ECF_OPERACAO)#LJV_ECF(LJV_ECF)#LJV_ECF_REDUCAO_LISTA(LJV_ECF_REDUCAO)#CRM_PFJ(CRM_PFJ)#CRM_ESCOLARIDADE(CRM_ESCOLARIDADE)#CRM_PROFISSAO(CRM_PROFISSAO)#CRM_FAIXA_RENDA(CRM_FAIXA_RENDA)#CRM_TIPO_CLIENTE(CRM_TIPO_CLIENTE)#CRM_SUBTIPO_CLIENTE(CRM_SUBTIPO_CLIENTE)#CRM_CLASSIFICACAO_CLIENTE(CRM_CLASSIFICACAO_CLIENTE)#CRM_SEGMENTO_CLIENTE(CRM_SEGMENTO_CLIENTE)#GEO_PAIS(GEO_PAIS)#LCF_REGIME_TRIBUTARIO(LCF_REGIME_TRIBUTARIO)#LCF_INDICADOR_FISCAL_PFJ(LCF_INDICADOR_FISCAL_PFJ)#CRM_CARGA_LOJA_VENDA(CRM_CARGA_LOJA_VENDA)#NTS_CARGA_LOJA_VENDEDOR_GERENTE(CRM_CARGA_LOJA_VENDEDOR)#NTS_CARGA_LOJA_VENDEDOR_CAIXA(CRM_CARGA_LOJA_VENDEDOR)#LJV_ATENDIMENTO_INTEGRACAO_FISCAL_LISTA(LJV_ATENDIMENTO_INTEGRACAO_FISCAL)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "LjvAtendimentoVendedor")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Dashboard.DashboardFinanceiro.LjvAtendimentoVendedor")]
	public partial class LjvAtendimentoVendedor : Linx.Data.Entity
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

	    public virtual void ResetChangeState()
	    {
	      this.ChangeState = "N";
	    }

	    #endregion Flat Entities

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For CodBandeiraRede
	    partial void OnCodBandeiraRedeChanging(System.String value);
	    partial void OnCodBandeiraRedeChanged();

	    private System.String _CodBandeiraRede;

	    [DataMember(Name = "CodBandeiraRede", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cod Bandeira Rede", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(25)]
	    [FunctionalPoint("Precision[25:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpLjvAtendimento];LookUpTitle[Seleção de (Cod Bandeira Rede)];LookUpQuery[executeLookUpLjvAtendimento];LookUpFinalize[finalizeLookUpLjvAtendimento];LookUpDisplayColumns[{\"DataAtendimento\" : \"Data Atendimento\", \"IdAtendimento\" : \"Qtde. Atendimento\", \"CodBandeiraRede\" : \"Cod Bandeira Rede\", \"DescBandeiraRede\" : \"Desc Bandeira Rede\", \"IdBandeiraRede\" : \"Id Bandeira Rede\", \"ValorCupomFiscal\" : \"Valor Cupom Fiscal\"}];LookUpColumns[{\"DataAtendimento\" : true, \"IdAtendimento\" : true, \"CodBandeiraRede\" : true, \"DescBandeiraRede\" : true, \"IdBandeiraRede\" : true, \"ValorCupomFiscal\" : true}];FilterDataKey[LJV_ATENDIMENTO_VENDEDOR.LJV_ATENDIMENTO.LJV_LOJA.TBC_BANDEIRA_REDE.COD_BANDEIRA_REDE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#CodBandeiraRede#false##25:0##Cod Bandeira Rede#2#true##::LookUpLjvAtendimento##false#false#LJV_ATENDIMENTO#LJV_ATENDIMENTO#Linx.Dashboard.DashboardFinanceiro#IQueryable#[CodBandeiraRede,DescBandeiraRede,IdBandeiraRede];CodBandeiraRede,DescBandeiraRede,IdBandeiraRede[CodBandeiraRede,DescBandeiraRede,IdBandeiraRede]#DataAtendimento[CodBandeiraRede=CodBandeiraRede,DescBandeiraRede=DescBandeiraRede,IdBandeiraRede=IdBandeiraRede];IdAtendimento[CodBandeiraRede=CodBandeiraRede,DescBandeiraRede=DescBandeiraRede,IdBandeiraRede=IdBandeiraRede];ValorCupomFiscal[CodBandeiraRede=CodBandeiraRede,DescBandeiraRede=DescBandeiraRede,IdBandeiraRede=IdBandeiraRede]#true#false", EdmKey="LJV_ATENDIMENTO_VENDEDOR.LJV_ATENDIMENTO.LJV_LOJA.TBC_BANDEIRA_REDE.COD_BANDEIRA_REDE")]
	    public System.String CodBandeiraRede
	    {
	    	    get
	    	    {
	    	          return _CodBandeiraRede;
	    	    }
	    	    set
	    	    {
	    	          if (this._CodBandeiraRede != value)
	    	          {
	    	              this.ValidateProperty("CodBandeiraRede", value);
	    	              this.OnCodBandeiraRedeChanging(value);
	    	              this.RaiseDataMemberChanging("CodBandeiraRede");
	    	              this._CodBandeiraRede = value;
	    	              this.RaiseDataMemberChanged("CodBandeiraRede");
	    	              this.OnCodBandeiraRedeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DataAtendimento
	    partial void OnDataAtendimentoChanging(System.DateTime value);
	    partial void OnDataAtendimentoChanged();

	    private System.DateTime _DataAtendimento;

	    [DataMember(IsRequired = true, Name = "DataAtendimento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Data Atendimento", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpLjvAtendimento];LookUpTitle[Seleção de (Data Atendimento)];LookUpQuery[executeLookUpLjvAtendimento];LookUpFinalize[finalizeLookUpLjvAtendimento];LookUpDisplayColumns[{\"DataAtendimento\" : \"Data Atendimento\", \"IdAtendimento\" : \"Qtde. Atendimento\", \"CodBandeiraRede\" : \"Cod Bandeira Rede\", \"DescBandeiraRede\" : \"Desc Bandeira Rede\", \"IdBandeiraRede\" : \"Id Bandeira Rede\", \"ValorCupomFiscal\" : \"Valor Cupom Fiscal\"}];LookUpColumns[{\"DataAtendimento\" : true, \"IdAtendimento\" : true, \"CodBandeiraRede\" : true, \"DescBandeiraRede\" : true, \"IdBandeiraRede\" : true, \"ValorCupomFiscal\" : true}];FilterDataKey[LJV_ATENDIMENTO_VENDEDOR.LJV_ATENDIMENTO.DATA_ATENDIMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.DateTime#DataAtendimento#false##10:0##Data Atendimento#0#true##::LookUpLjvAtendimento##false#false#LJV_ATENDIMENTO#LJV_ATENDIMENTO#Linx.Dashboard.DashboardFinanceiro#IQueryable#[CodBandeiraRede,DescBandeiraRede,IdBandeiraRede];CodBandeiraRede,DescBandeiraRede,IdBandeiraRede[CodBandeiraRede,DescBandeiraRede,IdBandeiraRede]#DataAtendimento[CodBandeiraRede=CodBandeiraRede,DescBandeiraRede=DescBandeiraRede,IdBandeiraRede=IdBandeiraRede];IdAtendimento[CodBandeiraRede=CodBandeiraRede,DescBandeiraRede=DescBandeiraRede,IdBandeiraRede=IdBandeiraRede];ValorCupomFiscal[CodBandeiraRede=CodBandeiraRede,DescBandeiraRede=DescBandeiraRede,IdBandeiraRede=IdBandeiraRede]#true#false", EdmKey="LJV_ATENDIMENTO_VENDEDOR.LJV_ATENDIMENTO.DATA_ATENDIMENTO")]
	    public System.DateTime DataAtendimento
	    {
	    	    get
	    	    {
	    	          return _DataAtendimento;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataAtendimento != value)
	    	          {
	    	              this.ValidateProperty("DataAtendimento", value);
	    	              this.OnDataAtendimentoChanging(value);
	    	              this.RaiseDataMemberChanging("DataAtendimento");
	    	              this._DataAtendimento = value;
	    	              this.RaiseDataMemberChanged("DataAtendimento");
	    	              this.OnDataAtendimentoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescBandeiraRede
	    partial void OnDescBandeiraRedeChanging(System.String value);
	    partial void OnDescBandeiraRedeChanged();

	    private System.String _DescBandeiraRede;

	    [DataMember(Name = "DescBandeiraRede", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Desc Bandeira Rede", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpLjvAtendimento];LookUpTitle[Seleção de (Desc Bandeira Rede)];LookUpQuery[executeLookUpLjvAtendimento];LookUpFinalize[finalizeLookUpLjvAtendimento];LookUpDisplayColumns[{\"DataAtendimento\" : \"Data Atendimento\", \"IdAtendimento\" : \"Qtde. Atendimento\", \"CodBandeiraRede\" : \"Cod Bandeira Rede\", \"DescBandeiraRede\" : \"Desc Bandeira Rede\", \"IdBandeiraRede\" : \"Id Bandeira Rede\", \"ValorCupomFiscal\" : \"Valor Cupom Fiscal\"}];LookUpColumns[{\"DataAtendimento\" : true, \"IdAtendimento\" : true, \"CodBandeiraRede\" : true, \"DescBandeiraRede\" : true, \"IdBandeiraRede\" : true, \"ValorCupomFiscal\" : true}];FilterDataKey[LJV_ATENDIMENTO_VENDEDOR.LJV_ATENDIMENTO.LJV_LOJA.TBC_BANDEIRA_REDE.DESC_BANDEIRA_REDE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescBandeiraRede#false##60:0##Desc Bandeira Rede#3#true##::LookUpLjvAtendimento##false#false#LJV_ATENDIMENTO#LJV_ATENDIMENTO#Linx.Dashboard.DashboardFinanceiro#IQueryable#[CodBandeiraRede,DescBandeiraRede,IdBandeiraRede];CodBandeiraRede,DescBandeiraRede,IdBandeiraRede[CodBandeiraRede,DescBandeiraRede,IdBandeiraRede]#DataAtendimento[CodBandeiraRede=CodBandeiraRede,DescBandeiraRede=DescBandeiraRede,IdBandeiraRede=IdBandeiraRede];IdAtendimento[CodBandeiraRede=CodBandeiraRede,DescBandeiraRede=DescBandeiraRede,IdBandeiraRede=IdBandeiraRede];ValorCupomFiscal[CodBandeiraRede=CodBandeiraRede,DescBandeiraRede=DescBandeiraRede,IdBandeiraRede=IdBandeiraRede]#true#false", EdmKey="LJV_ATENDIMENTO_VENDEDOR.LJV_ATENDIMENTO.LJV_LOJA.TBC_BANDEIRA_REDE.DESC_BANDEIRA_REDE")]
	    public System.String DescBandeiraRede
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
	    //Extensibility Partial Method Definitions For IdAtendimento
	    partial void OnIdAtendimentoChanging(Int64 value);
	    partial void OnIdAtendimentoChanged();

	    private Int64 _IdAtendimento;

	    [DataMember(IsRequired = true, Name = "IdAtendimento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Qtde. Atendimento", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Descending];OrderBySequence[0];AggregationFunction[Sum];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];LookUpName[LookUpLjvAtendimento];LookUpTitle[Seleção de (Qtde. Atendimento)];LookUpQuery[executeLookUpLjvAtendimento];LookUpFinalize[finalizeLookUpLjvAtendimento];LookUpDisplayColumns[{\"DataAtendimento\" : \"Data Atendimento\", \"IdAtendimento\" : \"Qtde. Atendimento\", \"CodBandeiraRede\" : \"Cod Bandeira Rede\", \"DescBandeiraRede\" : \"Desc Bandeira Rede\", \"IdBandeiraRede\" : \"Id Bandeira Rede\", \"ValorCupomFiscal\" : \"Valor Cupom Fiscal\"}];LookUpColumns[{\"DataAtendimento\" : true, \"IdAtendimento\" : true, \"CodBandeiraRede\" : true, \"DescBandeiraRede\" : true, \"IdBandeiraRede\" : true, \"ValorCupomFiscal\" : true}];FilterDataKey[LJV_ATENDIMENTO_VENDEDOR.LJV_ATENDIMENTO.ID_ATENDIMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdAtendimento#true##24:0##Qtde. Atendimento#1#true##::LookUpLjvAtendimento##false#false#LJV_ATENDIMENTO#LJV_ATENDIMENTO#Linx.Dashboard.DashboardFinanceiro#IQueryable#[CodBandeiraRede,DescBandeiraRede,IdBandeiraRede];CodBandeiraRede,DescBandeiraRede,IdBandeiraRede[CodBandeiraRede,DescBandeiraRede,IdBandeiraRede]#DataAtendimento[CodBandeiraRede=CodBandeiraRede,DescBandeiraRede=DescBandeiraRede,IdBandeiraRede=IdBandeiraRede];IdAtendimento[CodBandeiraRede=CodBandeiraRede,DescBandeiraRede=DescBandeiraRede,IdBandeiraRede=IdBandeiraRede];ValorCupomFiscal[CodBandeiraRede=CodBandeiraRede,DescBandeiraRede=DescBandeiraRede,IdBandeiraRede=IdBandeiraRede]#true#false", EdmKey="LJV_ATENDIMENTO_VENDEDOR.LJV_ATENDIMENTO.ID_ATENDIMENTO")]
	    public Int64 IdAtendimento
	    {
	    	    get
	    	    {
	    	          return _IdAtendimento;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdAtendimento != value)
	    	          {
	    	              this.ValidateProperty("IdAtendimento", value);
	    	              this.OnIdAtendimentoChanging(value);
	    	              this.RaiseDataMemberChanging("IdAtendimento");
	    	              this._IdAtendimento = value;
	    	              this.RaiseDataMemberChanged("IdAtendimento");
	    	              this.OnIdAtendimentoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdBandeiraRede
	    partial void OnIdBandeiraRedeChanging(System.Nullable<Int32> value);
	    partial void OnIdBandeiraRedeChanged();

	    private System.Nullable<Int32> _IdBandeiraRede;

	    [DataMember(Name = "IdBandeiraRede", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Bandeira Rede", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpLjvAtendimento];LookUpTitle[Seleção de (Id Bandeira Rede)];LookUpQuery[executeLookUpLjvAtendimento];LookUpFinalize[finalizeLookUpLjvAtendimento];LookUpDisplayColumns[{\"DataAtendimento\" : \"Data Atendimento\", \"IdAtendimento\" : \"Qtde. Atendimento\", \"CodBandeiraRede\" : \"Cod Bandeira Rede\", \"DescBandeiraRede\" : \"Desc Bandeira Rede\", \"IdBandeiraRede\" : \"Id Bandeira Rede\", \"ValorCupomFiscal\" : \"Valor Cupom Fiscal\"}];LookUpColumns[{\"DataAtendimento\" : true, \"IdAtendimento\" : true, \"CodBandeiraRede\" : true, \"DescBandeiraRede\" : true, \"IdBandeiraRede\" : true, \"ValorCupomFiscal\" : true}];FilterDataKey[LJV_ATENDIMENTO_VENDEDOR.LJV_ATENDIMENTO.LJV_LOJA.TBC_BANDEIRA_REDE.ID_BANDEIRA_REDE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<Int32>#IdBandeiraRede#false##12:0##Id Bandeira Rede#4#true##::LookUpLjvAtendimento##false#false#LJV_ATENDIMENTO#LJV_ATENDIMENTO#Linx.Dashboard.DashboardFinanceiro#IQueryable#[CodBandeiraRede,DescBandeiraRede,IdBandeiraRede];CodBandeiraRede,DescBandeiraRede,IdBandeiraRede[CodBandeiraRede,DescBandeiraRede,IdBandeiraRede]#DataAtendimento[CodBandeiraRede=CodBandeiraRede,DescBandeiraRede=DescBandeiraRede,IdBandeiraRede=IdBandeiraRede];IdAtendimento[CodBandeiraRede=CodBandeiraRede,DescBandeiraRede=DescBandeiraRede,IdBandeiraRede=IdBandeiraRede];ValorCupomFiscal[CodBandeiraRede=CodBandeiraRede,DescBandeiraRede=DescBandeiraRede,IdBandeiraRede=IdBandeiraRede]#true#false", EdmKey="LJV_ATENDIMENTO_VENDEDOR.LJV_ATENDIMENTO.LJV_LOJA.TBC_BANDEIRA_REDE.ID_BANDEIRA_REDE")]
	    public System.Nullable<Int32> IdBandeiraRede
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
	    //Extensibility Partial Method Definitions For NomeVendedor
	    partial void OnNomeVendedorChanging(System.String value);
	    partial void OnNomeVendedorChanged();

	    private System.String _NomeVendedor;

	    [DataMember(Name = "NomeVendedor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Vendedor", Description="", Order = 15, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(120)]
	    [FunctionalPoint("Precision[120:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpLjvVendedor];LookUpTitle[Seleção de (Nome Vendedor)];LookUpQuery[executeLookUpLjvVendedor];LookUpFinalize[finalizeLookUpLjvVendedor];LookUpDisplayColumns[{\"NomeVendedor\" : \"Nome Vendedor\", \"VendedorApelido\" : \"Vendedor Apelido\"}];LookUpColumns[{\"NomeVendedor\" : true, \"VendedorApelido\" : true}];FilterDataKey[LJV_ATENDIMENTO_VENDEDOR.LJV_VENDEDOR.NOME_VENDEDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeVendedor#false##120:0##Nome Vendedor#0#true##::LookUpLjvVendedor##false#false#LJV_VENDEDOR#LJV_VENDEDOR#Linx.Dashboard.DashboardFinanceiro#IQueryable###true#false", EdmKey="LJV_ATENDIMENTO_VENDEDOR.LJV_VENDEDOR.NOME_VENDEDOR")]
	    public System.String NomeVendedor
	    {
	    	    get
	    	    {
	    	          return _NomeVendedor;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeVendedor != value)
	    	          {
	    	              this.ValidateProperty("NomeVendedor", value);
	    	              this.OnNomeVendedorChanging(value);
	    	              this.RaiseDataMemberChanging("NomeVendedor");
	    	              this._NomeVendedor = value;
	    	              this.RaiseDataMemberChanged("NomeVendedor");
	    	              this.OnNomeVendedorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ValorComissao
	    partial void OnValorComissaoChanging(System.Nullable<System.Decimal> value);
	    partial void OnValorComissaoChanged();

	    private System.Nullable<System.Decimal> _ValorComissao;

	    [DataMember(Name = "ValorComissao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Valor Comissao", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[14:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[C2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[Sum];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[LJV_ATENDIMENTO_VENDEDOR.VALOR_COMISSAO];IsMeasure[true]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_ATENDIMENTO_VENDEDOR.VALOR_COMISSAO")]
	    public System.Nullable<System.Decimal> ValorComissao
	    {
	    	    get
	    	    {
	    	          return _ValorComissao;
	    	    }
	    	    set
	    	    {
	    	          if (this._ValorComissao != value)
	    	          {
	    	              this.ValidateProperty("ValorComissao", value);
	    	              this.OnValorComissaoChanging(value);
	    	              this.RaiseDataMemberChanging("ValorComissao");
	    	              this._ValorComissao = value;
	    	              this.RaiseDataMemberChanged("ValorComissao");
	    	              this.OnValorComissaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ValorCupomFiscal
	    partial void OnValorCupomFiscalChanging(System.Nullable<System.Decimal> value);
	    partial void OnValorCupomFiscalChanged();

	    private System.Nullable<System.Decimal> _ValorCupomFiscal;

	    [DataMember(Name = "ValorCupomFiscal", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Valor Cupom Fiscal", Description="", Order = 18, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[14:2];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[C2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[Sum];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];LookUpName[LookUpLjvAtendimento];LookUpTitle[Seleção de (Valor Cupom Fiscal)];LookUpQuery[executeLookUpLjvAtendimento];LookUpFinalize[finalizeLookUpLjvAtendimento];LookUpDisplayColumns[{\"DataAtendimento\" : \"Data Atendimento\", \"IdAtendimento\" : \"Qtde. Atendimento\", \"CodBandeiraRede\" : \"Cod Bandeira Rede\", \"DescBandeiraRede\" : \"Desc Bandeira Rede\", \"IdBandeiraRede\" : \"Id Bandeira Rede\", \"ValorCupomFiscal\" : \"Valor Cupom Fiscal\"}];LookUpColumns[{\"DataAtendimento\" : true, \"IdAtendimento\" : true, \"CodBandeiraRede\" : true, \"DescBandeiraRede\" : true, \"IdBandeiraRede\" : true, \"ValorCupomFiscal\" : true}];FilterDataKey[LJV_ATENDIMENTO_VENDEDOR.LJV_ATENDIMENTO.VALOR_CUPOM_FISCAL];IsMeasure[true]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<System.Decimal>#ValorCupomFiscal#false##14:2##Valor Cupom Fiscal#5#true##::LookUpLjvAtendimento##false#false#LJV_ATENDIMENTO#LJV_ATENDIMENTO#Linx.Dashboard.DashboardFinanceiro#IQueryable#[CodBandeiraRede,DescBandeiraRede,IdBandeiraRede];CodBandeiraRede,DescBandeiraRede,IdBandeiraRede[CodBandeiraRede,DescBandeiraRede,IdBandeiraRede]#DataAtendimento[CodBandeiraRede=CodBandeiraRede,DescBandeiraRede=DescBandeiraRede,IdBandeiraRede=IdBandeiraRede];IdAtendimento[CodBandeiraRede=CodBandeiraRede,DescBandeiraRede=DescBandeiraRede,IdBandeiraRede=IdBandeiraRede];ValorCupomFiscal[CodBandeiraRede=CodBandeiraRede,DescBandeiraRede=DescBandeiraRede,IdBandeiraRede=IdBandeiraRede]#true#false", EdmKey="LJV_ATENDIMENTO_VENDEDOR.LJV_ATENDIMENTO.VALOR_CUPOM_FISCAL")]
	    public System.Nullable<System.Decimal> ValorCupomFiscal
	    {
	    	    get
	    	    {
	    	          return _ValorCupomFiscal;
	    	    }
	    	    set
	    	    {
	    	          if (this._ValorCupomFiscal != value)
	    	          {
	    	              this.ValidateProperty("ValorCupomFiscal", value);
	    	              this.OnValorCupomFiscalChanging(value);
	    	              this.RaiseDataMemberChanging("ValorCupomFiscal");
	    	              this._ValorCupomFiscal = value;
	    	              this.RaiseDataMemberChanged("ValorCupomFiscal");
	    	              this.OnValorCupomFiscalChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For VendedorApelido
	    partial void OnVendedorApelidoChanging(System.String value);
	    partial void OnVendedorApelidoChanged();

	    private System.String _VendedorApelido;

	    [DataMember(Name = "VendedorApelido", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Vendedor Apelido", Description="", Order = 18, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpLjvVendedor];LookUpTitle[Seleção de (Vendedor Apelido)];LookUpQuery[executeLookUpLjvVendedor];LookUpFinalize[finalizeLookUpLjvVendedor];LookUpDisplayColumns[{\"NomeVendedor\" : \"Nome Vendedor\", \"VendedorApelido\" : \"Vendedor Apelido\"}];LookUpColumns[{\"NomeVendedor\" : true, \"VendedorApelido\" : true}];FilterDataKey[LJV_ATENDIMENTO_VENDEDOR.LJV_VENDEDOR.VENDEDOR_APELIDO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#VendedorApelido#false##40:0##Vendedor Apelido#1#true##::LookUpLjvVendedor##false#false#LJV_VENDEDOR#LJV_VENDEDOR#Linx.Dashboard.DashboardFinanceiro#IQueryable###true#false", EdmKey="LJV_ATENDIMENTO_VENDEDOR.LJV_VENDEDOR.VENDEDOR_APELIDO")]
	    public System.String VendedorApelido
	    {
	    	    get
	    	    {
	    	          return _VendedorApelido;
	    	    }
	    	    set
	    	    {
	    	          if (this._VendedorApelido != value)
	    	          {
	    	              this.ValidateProperty("VendedorApelido", value);
	    	              this.OnVendedorApelidoChanging(value);
	    	              this.RaiseDataMemberChanging("VendedorApelido");
	    	              this._VendedorApelido = value;
	    	              this.RaiseDataMemberChanged("VendedorApelido");
	    	              this.OnVendedorApelidoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For EntityUniqueKey
	    partial void OnEntityUniqueKeyChanging(System.Guid value);
	    partial void OnEntityUniqueKeyChanged();

	    private System.Guid _entityUniqueKey;
	    [DataMember(Name = "EntityUniqueKey", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [RoundtripOriginal()]
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

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "LinxOperacional.LJV_ATENDIMENTO_VENDEDOR").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Operacional.BM.LJV_ATENDIMENTO_VENDEDOR), QualifiedEntitySetName = "LinxOperacional.LJV_ATENDIMENTO_VENDEDOR" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_ATENDIMENTO_VENDEDOR.VALOR_COMISSAO", Source = "ValorComissao", Target = "VALOR_COMISSAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "LinxOperacional.LJV_ATENDIMENTO_VENDEDOR", RelationPropertyName = "LJV_ATENDIMENTO_VENDEDOR" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_ATENDIMENTO_VENDEDOR.LJV_ATENDIMENTO.ID_ATENDIMENTO", Source = "IdAtendimento", Target = "ID_ATENDIMENTO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "LinxOperacional.LJV_ATENDIMENTO", RelationPropertyName = "LJV_ATENDIMENTO" });

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
	[DomainIdentifier("ProcessorOverviewDashboardFinanceiroDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class DashboardFinanceiroDomainService : DomainService, IDataServiceContext 
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
	
	    private Linx.Operacional.BM.LinxOperacional _dbContext;
	    protected Linx.Operacional.BM.LinxOperacional DbContext 
	    { 
	    	get 
	    	{
	        	if (this._dbContext == null)
	        	{
	        		this._dbContext = new Linx.Operacional.BM.LinxOperacional(connectionString, this.Headers);
	        		((System.Data.Entity.Infrastructure.IObjectContextAdapter)this._dbContext).ObjectContext.CommandTimeout = 180;
	        		this._hasGpeconControl = (!(this._dbContext.IsUserMultiGpecon && this._dbContext.IdGpecon == this._dbContext.IdLinx) && this._dbContext.IdGpecon > 0);		
	        	}
	        	return this._dbContext;
	    	}
	    }

	    public string GetModelAssemblyName()
	    {
	        return typeof(Linx.Operacional.BM.LinxOperacional).Assembly.FullName;
	    }

	    public System.Data.Entity.Database Database
	    {
	        get { return this.DbContext.Database; }
	    }

		
	    public DashboardFinanceiroDomainService() : this("", null, null) { }
	    public DashboardFinanceiroDomainService(string connectionString) : this(connectionString, null, null) { }
	    public DashboardFinanceiroDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public DashboardFinanceiroDomainService(Linx.Operacional.BM.LinxOperacional dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public DashboardFinanceiroDomainService(string connectionString, Linx.Operacional.BM.LinxOperacional dataContext, Dictionary<string, string> headers) : base() 
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
	    public Linx.Operacional.BM.LinxOperacional GetEDM()
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
	    //Get All LookUpLjvLoja.
	    public IQueryable<LookUpLjvLoja> GetAllLookUpLjvLoja()
	    {
	        return this.GetLookUpLjvLoja(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpLjvLoja By EntitySearch.
	    public IQueryable<LookUpLjvLoja> GetLookUpLjvLojaByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpLjvLoja(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpLjvLoja.
	    public IQueryable<LookUpLjvLoja> GetLookUpLjvLoja(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "LJV_LOJA" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpLjvLoja";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpLjvLoja));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpLjvLoja> query =  
	
	            (from entity in this.DbContext.LJV_LOJA.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TBC_BANDEIRA_REDE
	            
	            select new LookUpLjvLoja()		
	            {
	            
                CodLoja = entity.COD_LOJA
                , DescLoja = entity.DESC_LOJA
                , IdLoja = entity.ID_LOJA
                , CodBandeiraRede = entityAl1.COD_BANDEIRA_REDE
                , DescBandeiraRede = entityAl1.DESC_BANDEIRA_REDE
                , IdBandeiraRede = entityAl1.ID_BANDEIRA_REDE
	            });

	            
            //Inner Group Definition
            if (propertyName.InList("CodBandeiraRede", "DescBandeiraRede", "IdBandeiraRede"))
            {
               query = (from r in query select new LookUpLjvLoja() {
               CodLoja = ""
               , DescLoja = ""
               , IdLoja = default(Int32)
               , CodBandeiraRede = r.CodBandeiraRede
               , DescBandeiraRede = r.DescBandeiraRede
               , IdBandeiraRede = r.IdBandeiraRede
                }).Distinct();
            }
	
		
	
	
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
	            
                IdFilialPfj = entity.ID_FILIAL_PFJ
                , CodigoFilial = entity.CODIGO_FILIAL
	            });

	            
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpTbcGrupoEconomico.
	    public IQueryable<LookUpTbcGrupoEconomico> GetAllLookUpTbcGrupoEconomico()
	    {
	        return this.GetLookUpTbcGrupoEconomico(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTbcGrupoEconomico By EntitySearch.
	    public IQueryable<LookUpTbcGrupoEconomico> GetLookUpTbcGrupoEconomicoByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTbcGrupoEconomico(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTbcGrupoEconomico.
	    public IQueryable<LookUpTbcGrupoEconomico> GetLookUpTbcGrupoEconomico(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TBC_GRUPO_ECONOMICO" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTbcGrupoEconomico";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTbcGrupoEconomico));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTbcGrupoEconomico> query =  
	
	            (from entity in this.DbContext.TBC_GRUPO_ECONOMICO.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpTbcGrupoEconomico()		
	            {
	            
                IdGpecon = entity.ID_GPECON
	            });

	            
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpLjvAtendimento.
	    public IQueryable<LookUpLjvAtendimento> GetAllLookUpLjvAtendimento()
	    {
	        return this.GetLookUpLjvAtendimento(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpLjvAtendimento By EntitySearch.
	    public IQueryable<LookUpLjvAtendimento> GetLookUpLjvAtendimentoByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpLjvAtendimento(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpLjvAtendimento.
	    public IQueryable<LookUpLjvAtendimento> GetLookUpLjvAtendimento(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "LJV_ATENDIMENTO" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpLjvAtendimento";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpLjvAtendimento));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpLjvAtendimento> query =  
	
	            (from entity in this.DbContext.LJV_ATENDIMENTO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.LJV_LOJA.TBC_BANDEIRA_REDE
	            
	            select new LookUpLjvAtendimento()		
	            {
	            
                DataAtendimento = entity.DATA_ATENDIMENTO
                , IdAtendimento = entity.ID_ATENDIMENTO
                , CodBandeiraRede = entityAl1.COD_BANDEIRA_REDE
                , DescBandeiraRede = entityAl1.DESC_BANDEIRA_REDE
                , IdBandeiraRede = entityAl1.ID_BANDEIRA_REDE
                , ValorCupomFiscal = entity.VALOR_CUPOM_FISCAL
	            });

	            
            //Inner Group Definition
            if (propertyName.InList())
            {
               query = (from r in query select new LookUpLjvAtendimento() {
               DataAtendimento = System.DateTime.MinValue
               , IdAtendimento = default(Int64)
               , CodBandeiraRede = r.CodBandeiraRede
               , DescBandeiraRede = r.DescBandeiraRede
               , IdBandeiraRede = r.IdBandeiraRede
               , ValorCupomFiscal = default(System.Nullable<System.Decimal>)
                }).Distinct();
            }
            else if (propertyName.InList("CodBandeiraRede", "DescBandeiraRede", "IdBandeiraRede"))
            {
               query = (from r in query select new LookUpLjvAtendimento() {
               DataAtendimento = System.DateTime.MinValue
               , IdAtendimento = default(Int64)
               , CodBandeiraRede = r.CodBandeiraRede
               , DescBandeiraRede = r.DescBandeiraRede
               , IdBandeiraRede = r.IdBandeiraRede
               , ValorCupomFiscal = default(System.Nullable<System.Decimal>)
                }).Distinct();
            }
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpLjvVendedor.
	    public IQueryable<LookUpLjvVendedor> GetAllLookUpLjvVendedor()
	    {
	        return this.GetLookUpLjvVendedor(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpLjvVendedor By EntitySearch.
	    public IQueryable<LookUpLjvVendedor> GetLookUpLjvVendedorByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpLjvVendedor(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpLjvVendedor.
	    public IQueryable<LookUpLjvVendedor> GetLookUpLjvVendedor(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "LJV_VENDEDOR" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpLjvVendedor";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpLjvVendedor));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpLjvVendedor> query =  
	
	            (from entity in this.DbContext.LJV_VENDEDOR.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpLjvVendedor()		
	            {
	            
                NomeVendedor = entity.NOME_VENDEDOR
                , VendedorApelido = entity.VENDEDOR_APELIDO
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
	
		

	        if (entityName.InList("Linx.Dashboard.DashboardFinanceiro.LjvAtendimento"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "LjvAtendimento",
	        			NameSpace = "Linx.Dashboard.DashboardFinanceiro",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "LjvAtendimento",
	        			ClearMethodName = "ClearLjvAtendimento",
	        			QueryMethodName  = "GetPagedLjvAtendimento",	
	        			CountingMethodName  = "GetLjvAtendimento" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Dashboard.DashboardFinanceiro.LjvAtendimento"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Dashboard.DashboardFinanceiro.LjvAtendimento"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Dashboard.DashboardFinanceiro.LjvAtendimentoVendedor"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "LjvAtendimentoVendedor",
	        			NameSpace = "Linx.Dashboard.DashboardFinanceiro",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "LjvAtendimentoVendedor",
	        			ClearMethodName = "ClearLjvAtendimentoVendedor",
	        			QueryMethodName  = "GetPagedLjvAtendimentoVendedor",	
	        			CountingMethodName  = "GetLjvAtendimentoVendedor" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Dashboard.DashboardFinanceiro.LjvAtendimentoVendedor"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Dashboard.DashboardFinanceiro.LjvAtendimentoVendedor"), forceAll: forceAll)
	        		});
	        }
	
            return result;
        }
	
	    [Ignore]
	    public string[] GetClientDomains(bool erp)
        {	
	    		if (erp)
	    		{

         		    return new string[] { "Dashboard_ClientErpDataDomainsFactory", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Dashboard.ClientResources.ClientErpDataDomainsFactory.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
	    		}
	    		else 
	    		{

         		    return new string[] { "Dashboard_MobileDataDomains", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Dashboard.ClientResources.MobileDataDomains.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
	    		}

        }

	    [Ignore]
	    public string[] GetClientService(bool erp)
        {	

	    		if (erp)
	    		{

         		    return new string[] { "Dashboard_DashboardFinanceiroClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Dashboard.ClientResources.DashboardFinanceiroClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Dashboard_dashboardFinanceiroService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Dashboard.ClientResources.dashboardFinanceiroService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear LjvAtendimento.
	    public IEnumerable<LjvAtendimento> ClearLjvAtendimento()
	    {
	        List<LjvAtendimento> result = new List<LjvAtendimento>();
	        result.Add(new LjvAtendimento());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear LjvAtendimentoVendedor.
	    public IEnumerable<LjvAtendimentoVendedor> ClearLjvAtendimentoVendedor()
	    {
	        List<LjvAtendimentoVendedor> result = new List<LjvAtendimentoVendedor>();
	        result.Add(new LjvAtendimentoVendedor());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    
	    [Ignore]
	    //Get LjvAtendimento.
	    public IQueryable<LjvAtendimento> GetLjvAtendimento()
	    {




		
	
	        
		
	        
	
	        IQueryable<LjvAtendimento> result = 
	            (from entity0 in this.DbContext.LJV_ATENDIMENTO
                  let entity0Al3 = entity0.LJV_LOJA
                  let entity0Al2 = entity0.TBC_FILIAL
                  let entity0Al4 = entity0.TBC_GRUPO_ECONOMICO
                  let entity0Al1 = entity0.LJV_LOJA.TBC_BANDEIRA_REDE
	            
	            group entity0  by new { 
                CodBandeiraRede = entity0Al1.COD_BANDEIRA_REDE
                , CodigoFilial = entity0Al2.CODIGO_FILIAL
                , CodLoja = entity0Al3.COD_LOJA
                , DataAtendimento = entity0.DATA_ATENDIMENTO
                , DescBandeiraRede = entity0Al1.DESC_BANDEIRA_REDE
                , DescLoja = entity0Al3.DESC_LOJA
                , IdBandeiraRede = entity0Al1.ID_BANDEIRA_REDE
                , IdFilialPfj = entity0Al2.ID_FILIAL_PFJ
                , IdGpecon = entity0Al4.ID_GPECON
                , IdLoja = entity0Al3.ID_LOJA } into rg0	
	            select new LjvAtendimento()		
	            {
	            
                CodBandeiraRede = rg0.Key.CodBandeiraRede
                , CodigoFilial = rg0.Key.CodigoFilial
                , CodLoja = rg0.Key.CodLoja
                , DataAtendimento = rg0.Key.DataAtendimento
                , DescBandeiraRede = rg0.Key.DescBandeiraRede
                , DescLoja = rg0.Key.DescLoja
                , IdBandeiraRede = rg0.Key.IdBandeiraRede
                , IdFilialPfj = rg0.Key.IdFilialPfj
                , IdGpecon = rg0.Key.IdGpecon
                , IdLoja = rg0.Key.IdLoja
                , ValorCupomFiscal = rg0.Sum(e => e.VALOR_CUPOM_FISCAL)
                , ValorDescontoSubtotal = rg0.Sum(e => e.VALOR_DESCONTO_SUBTOTAL)
                , ValorTotal = rg0.Sum(e => e.VALOR_TOTAL)
                , QtdeAtendimento = rg0.Count()
                , TicketMedio = rg0.Count()
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get LjvAtendimentoNoAssociations.
	    public IQueryable<LjvAtendimento> GetLjvAtendimentoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<LjvAtendimento> result = 
	            (from entity0 in this.DbContext.LJV_ATENDIMENTO
                  let entity0Al3 = entity0.LJV_LOJA
                  let entity0Al2 = entity0.TBC_FILIAL
                  let entity0Al4 = entity0.TBC_GRUPO_ECONOMICO
                  let entity0Al1 = entity0.LJV_LOJA.TBC_BANDEIRA_REDE
	            
	            group entity0  by new { 
                CodBandeiraRede = entity0Al1.COD_BANDEIRA_REDE
                , CodigoFilial = entity0Al2.CODIGO_FILIAL
                , CodLoja = entity0Al3.COD_LOJA
                , DataAtendimento = entity0.DATA_ATENDIMENTO
                , DescBandeiraRede = entity0Al1.DESC_BANDEIRA_REDE
                , DescLoja = entity0Al3.DESC_LOJA
                , IdBandeiraRede = entity0Al1.ID_BANDEIRA_REDE
                , IdFilialPfj = entity0Al2.ID_FILIAL_PFJ
                , IdGpecon = entity0Al4.ID_GPECON
                , IdLoja = entity0Al3.ID_LOJA } into rg0	
	            select new LjvAtendimento()		
	            {
	            
                CodBandeiraRede = rg0.Key.CodBandeiraRede
                , CodigoFilial = rg0.Key.CodigoFilial
                , CodLoja = rg0.Key.CodLoja
                , DataAtendimento = rg0.Key.DataAtendimento
                , DescBandeiraRede = rg0.Key.DescBandeiraRede
                , DescLoja = rg0.Key.DescLoja
                , IdBandeiraRede = rg0.Key.IdBandeiraRede
                , IdFilialPfj = rg0.Key.IdFilialPfj
                , IdGpecon = rg0.Key.IdGpecon
                , IdLoja = rg0.Key.IdLoja
                , ValorCupomFiscal = rg0.Sum(e => e.VALOR_CUPOM_FISCAL)
                , ValorDescontoSubtotal = rg0.Sum(e => e.VALOR_DESCONTO_SUBTOTAL)
                , ValorTotal = rg0.Sum(e => e.VALOR_TOTAL)
                , QtdeAtendimento = rg0.Count()
                , TicketMedio = rg0.Count()
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get LjvAtendimentoVendedor.
	    public IQueryable<LjvAtendimentoVendedor> GetLjvAtendimentoVendedor()
	    {




		
	
	        
		
	        
	
	        IQueryable<LjvAtendimentoVendedor> result = 
	            (from entity0 in this.DbContext.LJV_ATENDIMENTO_VENDEDOR
                  let entity0Al3 = entity0.LJV_VENDEDOR
                  let entity0Al2 = entity0.LJV_ATENDIMENTO
                  let entity0Al1 = entity0.LJV_ATENDIMENTO.LJV_LOJA.TBC_BANDEIRA_REDE
                orderby entity0Al2.ID_ATENDIMENTO descending
	            where ((!this.HasGpeconControl || entity0Al3.ID_GPECON == this.DbContext.IdGpecon))
	            group entity0  by new { 
                CodBandeiraRede = entity0Al1.COD_BANDEIRA_REDE
                , DataAtendimento = entity0Al2.DATA_ATENDIMENTO
                , DescBandeiraRede = entity0Al1.DESC_BANDEIRA_REDE
                , IdBandeiraRede = entity0Al1.ID_BANDEIRA_REDE
                , NomeVendedor = entity0Al3.NOME_VENDEDOR
                , VendedorApelido = entity0Al3.VENDEDOR_APELIDO } into rg0	
	            select new LjvAtendimentoVendedor()		
	            {
	            
                CodBandeiraRede = rg0.Key.CodBandeiraRede
                , DataAtendimento = rg0.Key.DataAtendimento
                , DescBandeiraRede = rg0.Key.DescBandeiraRede
                , IdAtendimento = rg0.Count()
                , IdBandeiraRede = rg0.Key.IdBandeiraRede
                , NomeVendedor = rg0.Key.NomeVendedor
                , ValorComissao = rg0.Sum(e => e.VALOR_COMISSAO)
                , ValorCupomFiscal = rg0.Sum(e => e.LJV_ATENDIMENTO.VALOR_CUPOM_FISCAL)
                , VendedorApelido = rg0.Key.VendedorApelido
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get LjvAtendimentoVendedorNoAssociations.
	    public IQueryable<LjvAtendimentoVendedor> GetLjvAtendimentoVendedorNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<LjvAtendimentoVendedor> result = 
	            (from entity0 in this.DbContext.LJV_ATENDIMENTO_VENDEDOR
                  let entity0Al3 = entity0.LJV_VENDEDOR
                  let entity0Al2 = entity0.LJV_ATENDIMENTO
                  let entity0Al1 = entity0.LJV_ATENDIMENTO.LJV_LOJA.TBC_BANDEIRA_REDE
                orderby entity0Al2.ID_ATENDIMENTO descending
	            where ((!this.HasGpeconControl || entity0Al3.ID_GPECON == this.DbContext.IdGpecon))
	            group entity0  by new { 
                CodBandeiraRede = entity0Al1.COD_BANDEIRA_REDE
                , DataAtendimento = entity0Al2.DATA_ATENDIMENTO
                , DescBandeiraRede = entity0Al1.DESC_BANDEIRA_REDE
                , IdBandeiraRede = entity0Al1.ID_BANDEIRA_REDE
                , NomeVendedor = entity0Al3.NOME_VENDEDOR
                , VendedorApelido = entity0Al3.VENDEDOR_APELIDO } into rg0	
	            select new LjvAtendimentoVendedor()		
	            {
	            
                CodBandeiraRede = rg0.Key.CodBandeiraRede
                , DataAtendimento = rg0.Key.DataAtendimento
                , DescBandeiraRede = rg0.Key.DescBandeiraRede
                , IdAtendimento = rg0.Count()
                , IdBandeiraRede = rg0.Key.IdBandeiraRede
                , NomeVendedor = rg0.Key.NomeVendedor
                , ValorComissao = rg0.Sum(e => e.VALOR_COMISSAO)
                , ValorCupomFiscal = rg0.Sum(e => e.LJV_ATENDIMENTO.VALOR_CUPOM_FISCAL)
                , VendedorApelido = rg0.Key.VendedorApelido
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	result.Add("LjvAtendimento|ValorCupomFiscal");
	    	result.Add("LjvAtendimento|LJV_ATENDIMENTO.VALOR_CUPOM_FISCAL");
	    	result.Add("LjvAtendimento|ValorDescontoSubtotal");
	    	result.Add("LjvAtendimento|LJV_ATENDIMENTO.VALOR_DESCONTO_SUBTOTAL");
	    	result.Add("LjvAtendimento|ValorTotal");
	    	result.Add("LjvAtendimento|LJV_ATENDIMENTO.VALOR_TOTAL");
	    	result.Add("LjvAtendimento|QtdeAtendimento");
	    	result.Add("LjvAtendimento|1");
	    	//Add filtering disabled property for LJV_ATENDIMENTO
	    	string[] bmDisabledLjvAtendimentoList = this.GetEDM().GetFilteringDisabledList("LJV_ATENDIMENTO");
	    	if (bmDisabledLjvAtendimentoList.Length > 0)
	    	{
	
	    		if (bmDisabledLjvAtendimentoList.Contains("LJV_ATENDIMENTO.DATA_ATENDIMENTO"))
	    		{
	    			result.Add("LjvAtendimento|DataAtendimento");
	    			result.Add("LjvAtendimento|LJV_ATENDIMENTO.DATA_ATENDIMENTO");
	    		}
	    	}
	    	result.Add("LjvAtendimentoVendedor|IdAtendimento");
	    	result.Add("LjvAtendimentoVendedor|LJV_ATENDIMENTO_VENDEDOR.LJV_ATENDIMENTO.ID_ATENDIMENTO");
	    	result.Add("LjvAtendimentoVendedor|ValorComissao");
	    	result.Add("LjvAtendimentoVendedor|LJV_ATENDIMENTO_VENDEDOR.VALOR_COMISSAO");
	    	result.Add("LjvAtendimentoVendedor|ValorCupomFiscal");
	    	result.Add("LjvAtendimentoVendedor|LJV_ATENDIMENTO_VENDEDOR.LJV_ATENDIMENTO.VALOR_CUPOM_FISCAL");
	    	//Add filtering disabled property for LJV_ATENDIMENTO_VENDEDOR
	    	string[] bmDisabledLjvAtendimentoVendedorList = this.GetEDM().GetFilteringDisabledList("LJV_ATENDIMENTO_VENDEDOR");
	    	if (bmDisabledLjvAtendimentoVendedorList.Length > 0)
	    	{
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
				
	    [Query(HasSideEffects = false)]
	    //Get LjvAtendimento By EntitySearchId.
	    public IQueryable<LjvAtendimento> GetLjvAtendimentoByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetLjvAtendimentoByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get LjvAtendimento By EntitySearchId.
	    public IQueryable<LjvAtendimento> GetLjvAtendimentoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetLjvAtendimentoByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get LjvAtendimentoVendedor By EntitySearchId.
	    public IQueryable<LjvAtendimentoVendedor> GetLjvAtendimentoVendedorByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetLjvAtendimentoVendedorByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get LjvAtendimentoVendedor By EntitySearchId.
	    public IQueryable<LjvAtendimentoVendedor> GetLjvAtendimentoVendedorByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetLjvAtendimentoVendedorByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get LjvAtendimento By Example.
	    [Ignore]
	    public IQueryable<LjvAtendimento> GetLjvAtendimentoByExample(LjvAtendimento entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetLjvAtendimentoByEntitySearch(queryAnalysis);
	    }
			
	    //Get LjvAtendimento By Example.
	    [Ignore]
	    public IQueryable<LjvAtendimento> GetLjvAtendimentoByExampleNoAssociations(LjvAtendimento entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetLjvAtendimentoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get LjvAtendimentoVendedor By Example.
	    [Ignore]
	    public IQueryable<LjvAtendimentoVendedor> GetLjvAtendimentoVendedorByExample(LjvAtendimentoVendedor entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetLjvAtendimentoVendedorByEntitySearch(queryAnalysis);
	    }
			
	    //Get LjvAtendimentoVendedor By Example.
	    [Ignore]
	    public IQueryable<LjvAtendimentoVendedor> GetLjvAtendimentoVendedorByExampleNoAssociations(LjvAtendimentoVendedor entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetLjvAtendimentoVendedorByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key






	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get LjvAtendimentoByEntitySearch.
	    public IQueryable<LjvAtendimento> GetLjvAtendimentoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LjvAtendimento));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<LjvAtendimento> result = 
	            (from entity0 in this.DbContext.LJV_ATENDIMENTO.Where(dynQuery, parameters.ToArray())
                  let entity0Al3 = entity0.LJV_LOJA
                  let entity0Al2 = entity0.TBC_FILIAL
                  let entity0Al4 = entity0.TBC_GRUPO_ECONOMICO
                  let entity0Al1 = entity0.LJV_LOJA.TBC_BANDEIRA_REDE
	            
	            group entity0  by new { 
                CodBandeiraRede = entity0Al1.COD_BANDEIRA_REDE
                , CodigoFilial = entity0Al2.CODIGO_FILIAL
                , CodLoja = entity0Al3.COD_LOJA
                , DataAtendimento = entity0.DATA_ATENDIMENTO
                , DescBandeiraRede = entity0Al1.DESC_BANDEIRA_REDE
                , DescLoja = entity0Al3.DESC_LOJA
                , IdBandeiraRede = entity0Al1.ID_BANDEIRA_REDE
                , IdFilialPfj = entity0Al2.ID_FILIAL_PFJ
                , IdGpecon = entity0Al4.ID_GPECON
                , IdLoja = entity0Al3.ID_LOJA } into rg0	
	            select new LjvAtendimento()		
	            {
	            
                CodBandeiraRede = rg0.Key.CodBandeiraRede
                , CodigoFilial = rg0.Key.CodigoFilial
                , CodLoja = rg0.Key.CodLoja
                , DataAtendimento = rg0.Key.DataAtendimento
                , DescBandeiraRede = rg0.Key.DescBandeiraRede
                , DescLoja = rg0.Key.DescLoja
                , IdBandeiraRede = rg0.Key.IdBandeiraRede
                , IdFilialPfj = rg0.Key.IdFilialPfj
                , IdGpecon = rg0.Key.IdGpecon
                , IdLoja = rg0.Key.IdLoja
                , ValorCupomFiscal = rg0.Sum(e => e.VALOR_CUPOM_FISCAL)
                , ValorDescontoSubtotal = rg0.Sum(e => e.VALOR_DESCONTO_SUBTOTAL)
                , ValorTotal = rg0.Sum(e => e.VALOR_TOTAL)
                , QtdeAtendimento = rg0.Count()
                , TicketMedio = rg0.Count()
		
	            }
	            );
	
	        SetLjvAtendimentoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get LjvAtendimentoByEntitySearchNoAssociations.
	    public IQueryable<LjvAtendimento> GetLjvAtendimentoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LjvAtendimento));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<LjvAtendimento> result = 
	            (from entity0 in this.DbContext.LJV_ATENDIMENTO.Where(dynQuery, parameters.ToArray())
                  let entity0Al3 = entity0.LJV_LOJA
                  let entity0Al2 = entity0.TBC_FILIAL
                  let entity0Al4 = entity0.TBC_GRUPO_ECONOMICO
                  let entity0Al1 = entity0.LJV_LOJA.TBC_BANDEIRA_REDE
	            
	            group entity0  by new { 
                CodBandeiraRede = entity0Al1.COD_BANDEIRA_REDE
                , CodigoFilial = entity0Al2.CODIGO_FILIAL
                , CodLoja = entity0Al3.COD_LOJA
                , DataAtendimento = entity0.DATA_ATENDIMENTO
                , DescBandeiraRede = entity0Al1.DESC_BANDEIRA_REDE
                , DescLoja = entity0Al3.DESC_LOJA
                , IdBandeiraRede = entity0Al1.ID_BANDEIRA_REDE
                , IdFilialPfj = entity0Al2.ID_FILIAL_PFJ
                , IdGpecon = entity0Al4.ID_GPECON
                , IdLoja = entity0Al3.ID_LOJA } into rg0	
	            select new LjvAtendimento()		
	            {
	            
                CodBandeiraRede = rg0.Key.CodBandeiraRede
                , CodigoFilial = rg0.Key.CodigoFilial
                , CodLoja = rg0.Key.CodLoja
                , DataAtendimento = rg0.Key.DataAtendimento
                , DescBandeiraRede = rg0.Key.DescBandeiraRede
                , DescLoja = rg0.Key.DescLoja
                , IdBandeiraRede = rg0.Key.IdBandeiraRede
                , IdFilialPfj = rg0.Key.IdFilialPfj
                , IdGpecon = rg0.Key.IdGpecon
                , IdLoja = rg0.Key.IdLoja
                , ValorCupomFiscal = rg0.Sum(e => e.VALOR_CUPOM_FISCAL)
                , ValorDescontoSubtotal = rg0.Sum(e => e.VALOR_DESCONTO_SUBTOTAL)
                , ValorTotal = rg0.Sum(e => e.VALOR_TOTAL)
                , QtdeAtendimento = rg0.Count()
                , TicketMedio = rg0.Count()
		
	            }
	            );
	
	        SetLjvAtendimentoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
	
	    [Ignore()]
	    private void SetLjvAtendimentoBusinessFilter(ref IQueryable<LjvAtendimento> query, List<EntitySearch> entitySearchList)
	    {
	    		int idxElement;
	    		string operatorValue;
	    		object value;
	    		//Get query by functions
	    		if (entitySearchList.Count > 0)
	    		{
	    			foreach (EntitySearch search in entitySearchList.Where(e => e.EntityName == "LjvAtendimento"))
	    			{

	
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "ValorCupomFiscal" || e.Value.ToString() == "LJV_ATENDIMENTO.VALOR_CUPOM_FISCAL")))
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
	    										System.Nullable<System.Decimal> tmpValorCupomFiscal1 = (System.Nullable<System.Decimal>)value;
	    										query = from r in query where r.ValorCupomFiscal == tmpValorCupomFiscal1 select r;
	    										break;
	    									case "!=":
	    										System.Nullable<System.Decimal> tmpValorCupomFiscal2 = (System.Nullable<System.Decimal>)value;
	    										query = from r in query where r.ValorCupomFiscal != tmpValorCupomFiscal2 select r;
	    										break;

	
	    									case "<":
	    										System.Nullable<System.Decimal> tmpValorCupomFiscal3 = (System.Nullable<System.Decimal>)value;
	    										query = from r in query where r.ValorCupomFiscal < tmpValorCupomFiscal3 select r;
	    										break;
	    									case "<=":
	    										System.Nullable<System.Decimal> tmpValorCupomFiscal4 = (System.Nullable<System.Decimal>)value;
	    										query = from r in query where r.ValorCupomFiscal <= tmpValorCupomFiscal4 select r;
	    										break;
	    									case ">":
	    										System.Nullable<System.Decimal> tmpValorCupomFiscal5 = (System.Nullable<System.Decimal>)value;
	    										query = from r in query where r.ValorCupomFiscal > tmpValorCupomFiscal5 select r;
	    										break;
	    									case ">=":
	    										System.Nullable<System.Decimal> tmpValorCupomFiscal6 = (System.Nullable<System.Decimal>)value;
	    										query = from r in query where r.ValorCupomFiscal >= tmpValorCupomFiscal6 select r;
	    										break;	

	
	    									default:
	    										break;
	    								}                                
	    							}
	    						}
        					} 

    
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "ValorDescontoSubtotal" || e.Value.ToString() == "LJV_ATENDIMENTO.VALOR_DESCONTO_SUBTOTAL")))
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
	    										System.Nullable<System.Decimal> tmpValorDescontoSubtotal1 = (System.Nullable<System.Decimal>)value;
	    										query = from r in query where r.ValorDescontoSubtotal == tmpValorDescontoSubtotal1 select r;
	    										break;
	    									case "!=":
	    										System.Nullable<System.Decimal> tmpValorDescontoSubtotal2 = (System.Nullable<System.Decimal>)value;
	    										query = from r in query where r.ValorDescontoSubtotal != tmpValorDescontoSubtotal2 select r;
	    										break;

	
	    									case "<":
	    										System.Nullable<System.Decimal> tmpValorDescontoSubtotal3 = (System.Nullable<System.Decimal>)value;
	    										query = from r in query where r.ValorDescontoSubtotal < tmpValorDescontoSubtotal3 select r;
	    										break;
	    									case "<=":
	    										System.Nullable<System.Decimal> tmpValorDescontoSubtotal4 = (System.Nullable<System.Decimal>)value;
	    										query = from r in query where r.ValorDescontoSubtotal <= tmpValorDescontoSubtotal4 select r;
	    										break;
	    									case ">":
	    										System.Nullable<System.Decimal> tmpValorDescontoSubtotal5 = (System.Nullable<System.Decimal>)value;
	    										query = from r in query where r.ValorDescontoSubtotal > tmpValorDescontoSubtotal5 select r;
	    										break;
	    									case ">=":
	    										System.Nullable<System.Decimal> tmpValorDescontoSubtotal6 = (System.Nullable<System.Decimal>)value;
	    										query = from r in query where r.ValorDescontoSubtotal >= tmpValorDescontoSubtotal6 select r;
	    										break;	

	
	    									default:
	    										break;
	    								}                                
	    							}
	    						}
        					} 

    
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "ValorTotal" || e.Value.ToString() == "LJV_ATENDIMENTO.VALOR_TOTAL")))
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
	    										System.Nullable<System.Decimal> tmpValorTotal1 = (System.Nullable<System.Decimal>)value;
	    										query = from r in query where r.ValorTotal == tmpValorTotal1 select r;
	    										break;
	    									case "!=":
	    										System.Nullable<System.Decimal> tmpValorTotal2 = (System.Nullable<System.Decimal>)value;
	    										query = from r in query where r.ValorTotal != tmpValorTotal2 select r;
	    										break;

	
	    									case "<":
	    										System.Nullable<System.Decimal> tmpValorTotal3 = (System.Nullable<System.Decimal>)value;
	    										query = from r in query where r.ValorTotal < tmpValorTotal3 select r;
	    										break;
	    									case "<=":
	    										System.Nullable<System.Decimal> tmpValorTotal4 = (System.Nullable<System.Decimal>)value;
	    										query = from r in query where r.ValorTotal <= tmpValorTotal4 select r;
	    										break;
	    									case ">":
	    										System.Nullable<System.Decimal> tmpValorTotal5 = (System.Nullable<System.Decimal>)value;
	    										query = from r in query where r.ValorTotal > tmpValorTotal5 select r;
	    										break;
	    									case ">=":
	    										System.Nullable<System.Decimal> tmpValorTotal6 = (System.Nullable<System.Decimal>)value;
	    										query = from r in query where r.ValorTotal >= tmpValorTotal6 select r;
	    										break;	

	
	    									default:
	    										break;
	    								}                                
	    							}
	    						}
        					} 

    
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "QtdeAtendimento" || e.Value.ToString() == "1")))
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
	    										int tmpQtdeAtendimento1 = (int)value;
	    										query = from r in query where r.QtdeAtendimento == tmpQtdeAtendimento1 select r;
	    										break;
	    									case "!=":
	    										int tmpQtdeAtendimento2 = (int)value;
	    										query = from r in query where r.QtdeAtendimento != tmpQtdeAtendimento2 select r;
	    										break;

	
	    									case "<":
	    										int tmpQtdeAtendimento3 = (int)value;
	    										query = from r in query where r.QtdeAtendimento < tmpQtdeAtendimento3 select r;
	    										break;
	    									case "<=":
	    										int tmpQtdeAtendimento4 = (int)value;
	    										query = from r in query where r.QtdeAtendimento <= tmpQtdeAtendimento4 select r;
	    										break;
	    									case ">":
	    										int tmpQtdeAtendimento5 = (int)value;
	    										query = from r in query where r.QtdeAtendimento > tmpQtdeAtendimento5 select r;
	    										break;
	    									case ">=":
	    										int tmpQtdeAtendimento6 = (int)value;
	    										query = from r in query where r.QtdeAtendimento >= tmpQtdeAtendimento6 select r;
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
	    //Get LjvAtendimentoVendedorByEntitySearch.
	    public IQueryable<LjvAtendimentoVendedor> GetLjvAtendimentoVendedorByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LjvAtendimentoVendedor));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<LjvAtendimentoVendedor> result = 
	            (from entity0 in this.DbContext.LJV_ATENDIMENTO_VENDEDOR.Where(dynQuery, parameters.ToArray())
                  let entity0Al3 = entity0.LJV_VENDEDOR
                  let entity0Al2 = entity0.LJV_ATENDIMENTO
                  let entity0Al1 = entity0.LJV_ATENDIMENTO.LJV_LOJA.TBC_BANDEIRA_REDE
                orderby entity0Al2.ID_ATENDIMENTO descending
	            where ((!this.HasGpeconControl || entity0Al3.ID_GPECON == this.DbContext.IdGpecon))
	            group entity0  by new { 
                CodBandeiraRede = entity0Al1.COD_BANDEIRA_REDE
                , DataAtendimento = entity0Al2.DATA_ATENDIMENTO
                , DescBandeiraRede = entity0Al1.DESC_BANDEIRA_REDE
                , IdBandeiraRede = entity0Al1.ID_BANDEIRA_REDE
                , NomeVendedor = entity0Al3.NOME_VENDEDOR
                , VendedorApelido = entity0Al3.VENDEDOR_APELIDO } into rg0	
	            select new LjvAtendimentoVendedor()		
	            {
	            
                CodBandeiraRede = rg0.Key.CodBandeiraRede
                , DataAtendimento = rg0.Key.DataAtendimento
                , DescBandeiraRede = rg0.Key.DescBandeiraRede
                , IdAtendimento = rg0.Count()
                , IdBandeiraRede = rg0.Key.IdBandeiraRede
                , NomeVendedor = rg0.Key.NomeVendedor
                , ValorComissao = rg0.Sum(e => e.VALOR_COMISSAO)
                , ValorCupomFiscal = rg0.Sum(e => e.LJV_ATENDIMENTO.VALOR_CUPOM_FISCAL)
                , VendedorApelido = rg0.Key.VendedorApelido
		
	            }
	            );
	
	        SetLjvAtendimentoVendedorBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get LjvAtendimentoVendedorByEntitySearchNoAssociations.
	    public IQueryable<LjvAtendimentoVendedor> GetLjvAtendimentoVendedorByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LjvAtendimentoVendedor));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<LjvAtendimentoVendedor> result = 
	            (from entity0 in this.DbContext.LJV_ATENDIMENTO_VENDEDOR.Where(dynQuery, parameters.ToArray())
                  let entity0Al3 = entity0.LJV_VENDEDOR
                  let entity0Al2 = entity0.LJV_ATENDIMENTO
                  let entity0Al1 = entity0.LJV_ATENDIMENTO.LJV_LOJA.TBC_BANDEIRA_REDE
                orderby entity0Al2.ID_ATENDIMENTO descending
	            where ((!this.HasGpeconControl || entity0Al3.ID_GPECON == this.DbContext.IdGpecon))
	            group entity0  by new { 
                CodBandeiraRede = entity0Al1.COD_BANDEIRA_REDE
                , DataAtendimento = entity0Al2.DATA_ATENDIMENTO
                , DescBandeiraRede = entity0Al1.DESC_BANDEIRA_REDE
                , IdBandeiraRede = entity0Al1.ID_BANDEIRA_REDE
                , NomeVendedor = entity0Al3.NOME_VENDEDOR
                , VendedorApelido = entity0Al3.VENDEDOR_APELIDO } into rg0	
	            select new LjvAtendimentoVendedor()		
	            {
	            
                CodBandeiraRede = rg0.Key.CodBandeiraRede
                , DataAtendimento = rg0.Key.DataAtendimento
                , DescBandeiraRede = rg0.Key.DescBandeiraRede
                , IdAtendimento = rg0.Count()
                , IdBandeiraRede = rg0.Key.IdBandeiraRede
                , NomeVendedor = rg0.Key.NomeVendedor
                , ValorComissao = rg0.Sum(e => e.VALOR_COMISSAO)
                , ValorCupomFiscal = rg0.Sum(e => e.LJV_ATENDIMENTO.VALOR_CUPOM_FISCAL)
                , VendedorApelido = rg0.Key.VendedorApelido
		
	            }
	            );
	
	        SetLjvAtendimentoVendedorBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
	
	    [Ignore()]
	    private void SetLjvAtendimentoVendedorBusinessFilter(ref IQueryable<LjvAtendimentoVendedor> query, List<EntitySearch> entitySearchList)
	    {
	    		int idxElement;
	    		string operatorValue;
	    		object value;
	    		//Get query by functions
	    		if (entitySearchList.Count > 0)
	    		{
	    			foreach (EntitySearch search in entitySearchList.Where(e => e.EntityName == "LjvAtendimentoVendedor"))
	    			{

	
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "IdAtendimento" || e.Value.ToString() == "LJV_ATENDIMENTO_VENDEDOR.LJV_ATENDIMENTO.ID_ATENDIMENTO")))
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
	    										Int64 tmpIdAtendimento1 = (Int64)value;
	    										query = from r in query where r.IdAtendimento == tmpIdAtendimento1 select r;
	    										break;
	    									case "!=":
	    										Int64 tmpIdAtendimento2 = (Int64)value;
	    										query = from r in query where r.IdAtendimento != tmpIdAtendimento2 select r;
	    										break;

	
	    									case "<":
	    										Int64 tmpIdAtendimento3 = (Int64)value;
	    										query = from r in query where r.IdAtendimento < tmpIdAtendimento3 select r;
	    										break;
	    									case "<=":
	    										Int64 tmpIdAtendimento4 = (Int64)value;
	    										query = from r in query where r.IdAtendimento <= tmpIdAtendimento4 select r;
	    										break;
	    									case ">":
	    										Int64 tmpIdAtendimento5 = (Int64)value;
	    										query = from r in query where r.IdAtendimento > tmpIdAtendimento5 select r;
	    										break;
	    									case ">=":
	    										Int64 tmpIdAtendimento6 = (Int64)value;
	    										query = from r in query where r.IdAtendimento >= tmpIdAtendimento6 select r;
	    										break;	

	
	    									default:
	    										break;
	    								}                                
	    							}
	    						}
        					} 

    
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "ValorComissao" || e.Value.ToString() == "LJV_ATENDIMENTO_VENDEDOR.VALOR_COMISSAO")))
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
	    										System.Nullable<System.Decimal> tmpValorComissao1 = (System.Nullable<System.Decimal>)value;
	    										query = from r in query where r.ValorComissao == tmpValorComissao1 select r;
	    										break;
	    									case "!=":
	    										System.Nullable<System.Decimal> tmpValorComissao2 = (System.Nullable<System.Decimal>)value;
	    										query = from r in query where r.ValorComissao != tmpValorComissao2 select r;
	    										break;

	
	    									case "<":
	    										System.Nullable<System.Decimal> tmpValorComissao3 = (System.Nullable<System.Decimal>)value;
	    										query = from r in query where r.ValorComissao < tmpValorComissao3 select r;
	    										break;
	    									case "<=":
	    										System.Nullable<System.Decimal> tmpValorComissao4 = (System.Nullable<System.Decimal>)value;
	    										query = from r in query where r.ValorComissao <= tmpValorComissao4 select r;
	    										break;
	    									case ">":
	    										System.Nullable<System.Decimal> tmpValorComissao5 = (System.Nullable<System.Decimal>)value;
	    										query = from r in query where r.ValorComissao > tmpValorComissao5 select r;
	    										break;
	    									case ">=":
	    										System.Nullable<System.Decimal> tmpValorComissao6 = (System.Nullable<System.Decimal>)value;
	    										query = from r in query where r.ValorComissao >= tmpValorComissao6 select r;
	    										break;	

	
	    									default:
	    										break;
	    								}                                
	    							}
	    						}
        					} 

    
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "ValorCupomFiscal" || e.Value.ToString() == "LJV_ATENDIMENTO_VENDEDOR.LJV_ATENDIMENTO.VALOR_CUPOM_FISCAL")))
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
	    										System.Nullable<System.Decimal> tmpValorCupomFiscal1 = (System.Nullable<System.Decimal>)value;
	    										query = from r in query where r.ValorCupomFiscal == tmpValorCupomFiscal1 select r;
	    										break;
	    									case "!=":
	    										System.Nullable<System.Decimal> tmpValorCupomFiscal2 = (System.Nullable<System.Decimal>)value;
	    										query = from r in query where r.ValorCupomFiscal != tmpValorCupomFiscal2 select r;
	    										break;

	
	    									case "<":
	    										System.Nullable<System.Decimal> tmpValorCupomFiscal3 = (System.Nullable<System.Decimal>)value;
	    										query = from r in query where r.ValorCupomFiscal < tmpValorCupomFiscal3 select r;
	    										break;
	    									case "<=":
	    										System.Nullable<System.Decimal> tmpValorCupomFiscal4 = (System.Nullable<System.Decimal>)value;
	    										query = from r in query where r.ValorCupomFiscal <= tmpValorCupomFiscal4 select r;
	    										break;
	    									case ">":
	    										System.Nullable<System.Decimal> tmpValorCupomFiscal5 = (System.Nullable<System.Decimal>)value;
	    										query = from r in query where r.ValorCupomFiscal > tmpValorCupomFiscal5 select r;
	    										break;
	    									case ">=":
	    										System.Nullable<System.Decimal> tmpValorCupomFiscal6 = (System.Nullable<System.Decimal>)value;
	    										query = from r in query where r.ValorCupomFiscal >= tmpValorCupomFiscal6 select r;
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
	    //Get PagedLjvAtendimento.
	    public IQueryable<LjvAtendimento> GetPagedLjvAtendimento(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LjvAtendimento));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<LjvAtendimento> result = 
	            (from entity0 in this.DbContext.LJV_ATENDIMENTO.Where(dynQuery, parameters.ToArray())
                  let entity0Al3 = entity0.LJV_LOJA
                  let entity0Al2 = entity0.TBC_FILIAL
                  let entity0Al4 = entity0.TBC_GRUPO_ECONOMICO
                  let entity0Al1 = entity0.LJV_LOJA.TBC_BANDEIRA_REDE
                orderby entity0Al1.COD_BANDEIRA_REDE ascending
	            
	            group entity0  by new { 
                CodBandeiraRede = entity0Al1.COD_BANDEIRA_REDE
                , CodigoFilial = entity0Al2.CODIGO_FILIAL
                , CodLoja = entity0Al3.COD_LOJA
                , DataAtendimento = entity0.DATA_ATENDIMENTO
                , DescBandeiraRede = entity0Al1.DESC_BANDEIRA_REDE
                , DescLoja = entity0Al3.DESC_LOJA
                , IdBandeiraRede = entity0Al1.ID_BANDEIRA_REDE
                , IdFilialPfj = entity0Al2.ID_FILIAL_PFJ
                , IdGpecon = entity0Al4.ID_GPECON
                , IdLoja = entity0Al3.ID_LOJA } into rg0	
	            select new LjvAtendimento()		
	            {
	            
                CodBandeiraRede = rg0.Key.CodBandeiraRede
                , CodigoFilial = rg0.Key.CodigoFilial
                , CodLoja = rg0.Key.CodLoja
                , DataAtendimento = rg0.Key.DataAtendimento
                , DescBandeiraRede = rg0.Key.DescBandeiraRede
                , DescLoja = rg0.Key.DescLoja
                , IdBandeiraRede = rg0.Key.IdBandeiraRede
                , IdFilialPfj = rg0.Key.IdFilialPfj
                , IdGpecon = rg0.Key.IdGpecon
                , IdLoja = rg0.Key.IdLoja
                , ValorCupomFiscal = rg0.Sum(e => e.VALOR_CUPOM_FISCAL)
                , ValorDescontoSubtotal = rg0.Sum(e => e.VALOR_DESCONTO_SUBTOTAL)
                , ValorTotal = rg0.Sum(e => e.VALOR_TOTAL)
                , QtdeAtendimento = rg0.Count()
                , TicketMedio = rg0.Count()
		
	            }
	            );
	
	        SetLjvAtendimentoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetLjvAtendimentoCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
			
	
	    
	    [Ignore]
	    //Get PagedLjvAtendimentoVendedor.
	    public IQueryable<LjvAtendimentoVendedor> GetPagedLjvAtendimentoVendedor(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LjvAtendimentoVendedor));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<LjvAtendimentoVendedor> result = 
	            (from entity0 in this.DbContext.LJV_ATENDIMENTO_VENDEDOR.Where(dynQuery, parameters.ToArray())
                  let entity0Al3 = entity0.LJV_VENDEDOR
                  let entity0Al2 = entity0.LJV_ATENDIMENTO
                  let entity0Al1 = entity0.LJV_ATENDIMENTO.LJV_LOJA.TBC_BANDEIRA_REDE
                orderby entity0Al1.COD_BANDEIRA_REDE ascending
	            where ((!this.HasGpeconControl || entity0Al3.ID_GPECON == this.DbContext.IdGpecon))
	            group entity0  by new { 
                CodBandeiraRede = entity0Al1.COD_BANDEIRA_REDE
                , DataAtendimento = entity0Al2.DATA_ATENDIMENTO
                , DescBandeiraRede = entity0Al1.DESC_BANDEIRA_REDE
                , IdBandeiraRede = entity0Al1.ID_BANDEIRA_REDE
                , NomeVendedor = entity0Al3.NOME_VENDEDOR
                , VendedorApelido = entity0Al3.VENDEDOR_APELIDO } into rg0	
	            select new LjvAtendimentoVendedor()		
	            {
	            
                CodBandeiraRede = rg0.Key.CodBandeiraRede
                , DataAtendimento = rg0.Key.DataAtendimento
                , DescBandeiraRede = rg0.Key.DescBandeiraRede
                , IdAtendimento = rg0.Count()
                , IdBandeiraRede = rg0.Key.IdBandeiraRede
                , NomeVendedor = rg0.Key.NomeVendedor
                , ValorComissao = rg0.Sum(e => e.VALOR_COMISSAO)
                , ValorCupomFiscal = rg0.Sum(e => e.LJV_ATENDIMENTO.VALOR_CUPOM_FISCAL)
                , VendedorApelido = rg0.Key.VendedorApelido
		
	            }
	            );
	
	        SetLjvAtendimentoVendedorBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetLjvAtendimentoVendedorCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    
	    [Update()]	
	    //Update LjvAtendimento.
	    public void UpdateLjvAtendimento(LjvAtendimento entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert LjvAtendimento.
	    public void InsertLjvAtendimento(LjvAtendimento entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete LjvAtendimento.
	    public void DeleteLjvAtendimento(LjvAtendimento entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update LjvAtendimentoVendedor.
	    public void UpdateLjvAtendimentoVendedor(LjvAtendimentoVendedor entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert LjvAtendimentoVendedor.
	    public void InsertLjvAtendimentoVendedor(LjvAtendimentoVendedor entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete LjvAtendimentoVendedor.
	    public void DeleteLjvAtendimentoVendedor(LjvAtendimentoVendedor entity)
	    {



	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}