					
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

namespace Linx.Framework.BV.Multimidia
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="DOC_MULTIMIDIA_TABELA.ID_CHAVE,DOC_MULTIMIDIA_TABELA.UID_CHAVE,DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.UID_DOCUMENTO,DOC_MULTIMIDIA_TABELA.UID_TABELA", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[DocMultimidiaTabela];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];Entities[DOC_MULTIMIDIA:UidDocumento];SubQueryInfo[];EdmEntityName[DOC_MULTIMIDIA_TABELA];EntityRelations[DOC_MULTIMIDIA(DOC_MULTIMIDIA)#DOC_CLASSIFICADOR(DOC_CLASSIFICADOR)#DOC_MULTIMIDIA1(DOC_MULTIMIDIA)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "DocMultimidiaTabela")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Multimidia.DocMultimidiaTabela")]
	public partial class DocMultimidiaTabela : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For Conteudo
	    partial void OnConteudoChanging(Byte[] value);
	    partial void OnConteudoChanged();

	    private Byte[] _Conteudo;

	    [DataMember(Name = "Conteudo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Conteudo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidia];LookUpTitle[Seleção de (Conteudo)];LookUpQuery[executeLookUpDocMultimidia];LookUpFinalize[finalizeLookUpDocMultimidia];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DescDocClassificador\" : \"DescDocClassificador\", \"DescDocumento\" : \"DescDocumento\", \"IdDocClassificador\" : \"IdDocClassificador\", \"LxTipoDocumento\" : \"LxTipoDocumento\", \"LxTipoExtensao\" : \"LxTipoExtensao\", \"Obs\" : \"Obs\", \"Thumbnail\" : \"Thumbnail\", \"UidDocumento\" : \"UidDocumento\", \"Url\" : \"Url\", \"XmlMapeamento\" : \"XmlMapeamento\"}];LookUpColumns[{\"Conteudo\" : true, \"DescDocClassificador\" : true, \"DescDocumento\" : true, \"IdDocClassificador\" : true, \"LxTipoDocumento\" : true, \"LxTipoExtensao\" : true, \"Obs\" : true, \"Thumbnail\" : true, \"UidDocumento\" : true, \"Url\" : true, \"XmlMapeamento\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.CONTEUDO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Byte[]#Conteudo#false##30##Conteudo#0#true##::LookUpDocMultimidia##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#DescDocClassificador,IdDocClassificador[DescDocClassificador,IdDocClassificador]#Conteudo[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];DescDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoExtensao[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Obs[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Thumbnail[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];UidDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Url[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];XmlMapeamento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.CONTEUDO")]
	    public Byte[] Conteudo
	    {
	    	    get
	    	    {
	    	          return _Conteudo;
	    	    }
	    	    set
	    	    {
	    	          if (this._Conteudo != value)
	    	          {
	    	              this.ValidateProperty("Conteudo", value);
	    	              this.OnConteudoChanging(value);
	    	              this.RaiseDataMemberChanging("Conteudo");
	    	              this._Conteudo = value;
	    	              this.RaiseDataMemberChanged("Conteudo");
	    	              this.OnConteudoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DataCriacao
	    partial void OnDataCriacaoChanging(System.DateTime value);
	    partial void OnDataCriacaoChanged();

	    private System.DateTime _DataCriacao;

	    [DataMember(IsRequired = true, Name = "DataCriacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Data Criacao", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.DATA_CRIACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.DATA_CRIACAO")]
	    public System.DateTime DataCriacao
	    {
	    	    get
	    	    {
	    	          return _DataCriacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataCriacao != value)
	    	          {
	    	              this.ValidateProperty("DataCriacao", value);
	    	              this.OnDataCriacaoChanging(value);
	    	              this.RaiseDataMemberChanging("DataCriacao");
	    	              this._DataCriacao = value;
	    	              this.RaiseDataMemberChanged("DataCriacao");
	    	              this.OnDataCriacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescDocClassificador
	    partial void OnDescDocClassificadorChanging(System.String value);
	    partial void OnDescDocClassificadorChanged();

	    private System.String _DescDocClassificador;

	    [DataMember(IsRequired = true, Name = "DescDocClassificador", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "DescDocClassificador", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidia];LookUpTitle[Seleção de (DescDocClassificador)];LookUpQuery[executeLookUpDocMultimidia];LookUpFinalize[finalizeLookUpDocMultimidia];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DescDocClassificador\" : \"DescDocClassificador\", \"DescDocumento\" : \"DescDocumento\", \"IdDocClassificador\" : \"IdDocClassificador\", \"LxTipoDocumento\" : \"LxTipoDocumento\", \"LxTipoExtensao\" : \"LxTipoExtensao\", \"Obs\" : \"Obs\", \"Thumbnail\" : \"Thumbnail\", \"UidDocumento\" : \"UidDocumento\", \"Url\" : \"Url\", \"XmlMapeamento\" : \"XmlMapeamento\"}];LookUpColumns[{\"Conteudo\" : true, \"DescDocClassificador\" : true, \"DescDocumento\" : true, \"IdDocClassificador\" : true, \"LxTipoDocumento\" : true, \"LxTipoExtensao\" : true, \"Obs\" : true, \"Thumbnail\" : true, \"UidDocumento\" : true, \"Url\" : true, \"XmlMapeamento\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.DOC_CLASSIFICADOR.DESC_DOC_CLASSIFICADOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescDocClassificador#false##600##DescDocClassificador#1#true##::LookUpDocMultimidia##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#DescDocClassificador,IdDocClassificador[DescDocClassificador,IdDocClassificador]#Conteudo[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];DescDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoExtensao[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Obs[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Thumbnail[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];UidDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Url[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];XmlMapeamento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.DOC_CLASSIFICADOR.DESC_DOC_CLASSIFICADOR")]
	    public System.String DescDocClassificador
	    {
	    	    get
	    	    {
	    	          return _DescDocClassificador;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescDocClassificador != value)
	    	          {
	    	              this.ValidateProperty("DescDocClassificador", value);
	    	              this.OnDescDocClassificadorChanging(value);
	    	              this.RaiseDataMemberChanging("DescDocClassificador");
	    	              this._DescDocClassificador = value;
	    	              this.RaiseDataMemberChanged("DescDocClassificador");
	    	              this.OnDescDocClassificadorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescDocumento
	    partial void OnDescDocumentoChanging(System.String value);
	    partial void OnDescDocumentoChanged();

	    private System.String _DescDocumento;

	    [DataMember(IsRequired = true, Name = "DescDocumento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "DescDocumento", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidia];LookUpTitle[Seleção de (DescDocumento)];LookUpQuery[executeLookUpDocMultimidia];LookUpFinalize[finalizeLookUpDocMultimidia];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DescDocClassificador\" : \"DescDocClassificador\", \"DescDocumento\" : \"DescDocumento\", \"IdDocClassificador\" : \"IdDocClassificador\", \"LxTipoDocumento\" : \"LxTipoDocumento\", \"LxTipoExtensao\" : \"LxTipoExtensao\", \"Obs\" : \"Obs\", \"Thumbnail\" : \"Thumbnail\", \"UidDocumento\" : \"UidDocumento\", \"Url\" : \"Url\", \"XmlMapeamento\" : \"XmlMapeamento\"}];LookUpColumns[{\"Conteudo\" : true, \"DescDocClassificador\" : true, \"DescDocumento\" : true, \"IdDocClassificador\" : true, \"LxTipoDocumento\" : true, \"LxTipoExtensao\" : true, \"Obs\" : true, \"Thumbnail\" : true, \"UidDocumento\" : true, \"Url\" : true, \"XmlMapeamento\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.DESC_DOCUMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescDocumento#false##600##DescDocumento#2#true##::LookUpDocMultimidia##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#DescDocClassificador,IdDocClassificador[DescDocClassificador,IdDocClassificador]#Conteudo[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];DescDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoExtensao[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Obs[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Thumbnail[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];UidDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Url[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];XmlMapeamento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.DESC_DOCUMENTO")]
	    public System.String DescDocumento
	    {
	    	    get
	    	    {
	    	          return _DescDocumento;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescDocumento != value)
	    	          {
	    	              this.ValidateProperty("DescDocumento", value);
	    	              this.OnDescDocumentoChanging(value);
	    	              this.RaiseDataMemberChanging("DescDocumento");
	    	              this._DescDocumento = value;
	    	              this.RaiseDataMemberChanged("DescDocumento");
	    	              this.OnDescDocumentoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdChave
	    partial void OnIdChaveChanging(Int64 value);
	    partial void OnIdChaveChanged();

	    private Int64 _IdChave;

	    [DataMember(IsRequired = true, Name = "IdChave", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "IdChave", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA.ID_CHAVE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.ID_CHAVE")]
	    public Int64 IdChave
	    {
	    	    get
	    	    {
	    	          return _IdChave;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdChave != value)
	    	          {
	    	              this.ValidateProperty("IdChave", value);
	    	              this.OnIdChaveChanging(value);
	    	              this.RaiseDataMemberChanging("IdChave");
	    	              this._IdChave = value;
	    	              this.RaiseDataMemberChanged("IdChave");
	    	              this.OnIdChaveChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdDocClassificador
	    partial void OnIdDocClassificadorChanging(Int64 value);
	    partial void OnIdDocClassificadorChanged();

	    private Int64 _IdDocClassificador;

	    [DataMember(IsRequired = true, Name = "IdDocClassificador", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "IdDocClassificador", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidia];LookUpTitle[Seleção de (IdDocClassificador)];LookUpQuery[executeLookUpDocMultimidia];LookUpFinalize[finalizeLookUpDocMultimidia];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DescDocClassificador\" : \"DescDocClassificador\", \"DescDocumento\" : \"DescDocumento\", \"IdDocClassificador\" : \"IdDocClassificador\", \"LxTipoDocumento\" : \"LxTipoDocumento\", \"LxTipoExtensao\" : \"LxTipoExtensao\", \"Obs\" : \"Obs\", \"Thumbnail\" : \"Thumbnail\", \"UidDocumento\" : \"UidDocumento\", \"Url\" : \"Url\", \"XmlMapeamento\" : \"XmlMapeamento\"}];LookUpColumns[{\"Conteudo\" : true, \"DescDocClassificador\" : true, \"DescDocumento\" : true, \"IdDocClassificador\" : true, \"LxTipoDocumento\" : true, \"LxTipoExtensao\" : true, \"Obs\" : true, \"Thumbnail\" : true, \"UidDocumento\" : true, \"Url\" : true, \"XmlMapeamento\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.DOC_CLASSIFICADOR.ID_DOC_CLASSIFICADOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdDocClassificador#true##24:0##IdDocClassificador#3#true##::LookUpDocMultimidia##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#DescDocClassificador,IdDocClassificador[DescDocClassificador,IdDocClassificador]#Conteudo[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];DescDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoExtensao[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Obs[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Thumbnail[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];UidDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Url[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];XmlMapeamento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.DOC_CLASSIFICADOR.ID_DOC_CLASSIFICADOR")]
	    public Int64 IdDocClassificador
	    {
	    	    get
	    	    {
	    	          return _IdDocClassificador;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdDocClassificador != value)
	    	          {
	    	              this.ValidateProperty("IdDocClassificador", value);
	    	              this.OnIdDocClassificadorChanging(value);
	    	              this.RaiseDataMemberChanging("IdDocClassificador");
	    	              this._IdDocClassificador = value;
	    	              this.RaiseDataMemberChanged("IdDocClassificador");
	    	              this.OnIdDocClassificadorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoDocumento
	    partial void OnLxTipoDocumentoChanging(Byte value);
	    partial void OnLxTipoDocumentoChanged();

	    private Byte _LxTipoDocumento;

	    [DataMember(IsRequired = true, Name = "LxTipoDocumento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "LxTipoDocumento", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidia];LookUpTitle[Seleção de (LxTipoDocumento)];LookUpQuery[executeLookUpDocMultimidia];LookUpFinalize[finalizeLookUpDocMultimidia];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DescDocClassificador\" : \"DescDocClassificador\", \"DescDocumento\" : \"DescDocumento\", \"IdDocClassificador\" : \"IdDocClassificador\", \"LxTipoDocumento\" : \"LxTipoDocumento\", \"LxTipoExtensao\" : \"LxTipoExtensao\", \"Obs\" : \"Obs\", \"Thumbnail\" : \"Thumbnail\", \"UidDocumento\" : \"UidDocumento\", \"Url\" : \"Url\", \"XmlMapeamento\" : \"XmlMapeamento\"}];LookUpColumns[{\"Conteudo\" : true, \"DescDocClassificador\" : true, \"DescDocumento\" : true, \"IdDocClassificador\" : true, \"LxTipoDocumento\" : true, \"LxTipoExtensao\" : true, \"Obs\" : true, \"Thumbnail\" : true, \"UidDocumento\" : true, \"Url\" : true, \"XmlMapeamento\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.LX_TIPO_DOCUMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Byte#LxTipoDocumento#false##30##LxTipoDocumento#4#true##::LookUpDocMultimidia##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#DescDocClassificador,IdDocClassificador[DescDocClassificador,IdDocClassificador]#Conteudo[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];DescDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoExtensao[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Obs[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Thumbnail[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];UidDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Url[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];XmlMapeamento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.LX_TIPO_DOCUMENTO")]
	    public Byte LxTipoDocumento
	    {
	    	    get
	    	    {
	    	          return _LxTipoDocumento;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoDocumento != value)
	    	          {
	    	              this.ValidateProperty("LxTipoDocumento", value);
	    	              this.OnLxTipoDocumentoChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoDocumento");
	    	              this._LxTipoDocumento = value;
	    	              this.RaiseDataMemberChanged("LxTipoDocumento");
	    	              this.OnLxTipoDocumentoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoExtensao
	    partial void OnLxTipoExtensaoChanging(Byte value);
	    partial void OnLxTipoExtensaoChanged();

	    private Byte _LxTipoExtensao;

	    [DataMember(IsRequired = true, Name = "LxTipoExtensao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "LxTipoExtensao", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidia];LookUpTitle[Seleção de (LxTipoExtensao)];LookUpQuery[executeLookUpDocMultimidia];LookUpFinalize[finalizeLookUpDocMultimidia];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DescDocClassificador\" : \"DescDocClassificador\", \"DescDocumento\" : \"DescDocumento\", \"IdDocClassificador\" : \"IdDocClassificador\", \"LxTipoDocumento\" : \"LxTipoDocumento\", \"LxTipoExtensao\" : \"LxTipoExtensao\", \"Obs\" : \"Obs\", \"Thumbnail\" : \"Thumbnail\", \"UidDocumento\" : \"UidDocumento\", \"Url\" : \"Url\", \"XmlMapeamento\" : \"XmlMapeamento\"}];LookUpColumns[{\"Conteudo\" : true, \"DescDocClassificador\" : true, \"DescDocumento\" : true, \"IdDocClassificador\" : true, \"LxTipoDocumento\" : true, \"LxTipoExtensao\" : true, \"Obs\" : true, \"Thumbnail\" : true, \"UidDocumento\" : true, \"Url\" : true, \"XmlMapeamento\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.LX_TIPO_EXTENSAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Byte#LxTipoExtensao#false##30##LxTipoExtensao#5#true##::LookUpDocMultimidia##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#DescDocClassificador,IdDocClassificador[DescDocClassificador,IdDocClassificador]#Conteudo[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];DescDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoExtensao[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Obs[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Thumbnail[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];UidDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Url[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];XmlMapeamento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.LX_TIPO_EXTENSAO")]
	    public Byte LxTipoExtensao
	    {
	    	    get
	    	    {
	    	          return _LxTipoExtensao;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoExtensao != value)
	    	          {
	    	              this.ValidateProperty("LxTipoExtensao", value);
	    	              this.OnLxTipoExtensaoChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoExtensao");
	    	              this._LxTipoExtensao = value;
	    	              this.RaiseDataMemberChanged("LxTipoExtensao");
	    	              this.OnLxTipoExtensaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoMidia
	    partial void OnLxTipoMidiaChanging(Byte value);
	    partial void OnLxTipoMidiaChanged();

	    private Byte _LxTipoMidia;

	    [DataMember(IsRequired = true, Name = "LxTipoMidia", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Lx Tipo Midia", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.LX_TIPO_MIDIA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.LX_TIPO_MIDIA")]
	    public Byte LxTipoMidia
	    {
	    	    get
	    	    {
	    	          return _LxTipoMidia;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoMidia != value)
	    	          {
	    	              this.ValidateProperty("LxTipoMidia", value);
	    	              this.OnLxTipoMidiaChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoMidia");
	    	              this._LxTipoMidia = value;
	    	              this.RaiseDataMemberChanged("LxTipoMidia");
	    	              this.OnLxTipoMidiaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeArquivo
	    partial void OnNomeArquivoChanging(System.String value);
	    partial void OnNomeArquivoChanged();

	    private System.String _NomeArquivo;

	    [DataMember(Name = "NomeArquivo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Arquivo", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(150)]
	    [FunctionalPoint("Precision[150:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.NOME_ARQUIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.NOME_ARQUIVO")]
	    public System.String NomeArquivo
	    {
	    	    get
	    	    {
	    	          return _NomeArquivo;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeArquivo != value)
	    	          {
	    	              this.ValidateProperty("NomeArquivo", value);
	    	              this.OnNomeArquivoChanging(value);
	    	              this.RaiseDataMemberChanging("NomeArquivo");
	    	              this._NomeArquivo = value;
	    	              this.RaiseDataMemberChanged("NomeArquivo");
	    	              this.OnNomeArquivoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Obs
	    partial void OnObsChanging(System.String value);
	    partial void OnObsChanged();

	    private System.String _Obs;

	    [DataMember(Name = "Obs", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Obs", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidia];LookUpTitle[Seleção de (Obs)];LookUpQuery[executeLookUpDocMultimidia];LookUpFinalize[finalizeLookUpDocMultimidia];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DescDocClassificador\" : \"DescDocClassificador\", \"DescDocumento\" : \"DescDocumento\", \"IdDocClassificador\" : \"IdDocClassificador\", \"LxTipoDocumento\" : \"LxTipoDocumento\", \"LxTipoExtensao\" : \"LxTipoExtensao\", \"Obs\" : \"Obs\", \"Thumbnail\" : \"Thumbnail\", \"UidDocumento\" : \"UidDocumento\", \"Url\" : \"Url\", \"XmlMapeamento\" : \"XmlMapeamento\"}];LookUpColumns[{\"Conteudo\" : true, \"DescDocClassificador\" : true, \"DescDocumento\" : true, \"IdDocClassificador\" : true, \"LxTipoDocumento\" : true, \"LxTipoExtensao\" : true, \"Obs\" : true, \"Thumbnail\" : true, \"UidDocumento\" : true, \"Url\" : true, \"XmlMapeamento\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.OBS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#Obs#false##0##Obs#6#true##::LookUpDocMultimidia##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#DescDocClassificador,IdDocClassificador[DescDocClassificador,IdDocClassificador]#Conteudo[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];DescDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoExtensao[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Obs[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Thumbnail[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];UidDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Url[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];XmlMapeamento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.OBS")]
	    public System.String Obs
	    {
	    	    get
	    	    {
	    	          return _Obs;
	    	    }
	    	    set
	    	    {
	    	          if (this._Obs != value)
	    	          {
	    	              this.ValidateProperty("Obs", value);
	    	              this.OnObsChanging(value);
	    	              this.RaiseDataMemberChanging("Obs");
	    	              this._Obs = value;
	    	              this.RaiseDataMemberChanged("Obs");
	    	              this.OnObsChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For OrdemApresentacao
	    partial void OnOrdemApresentacaoChanging(Int16 value);
	    partial void OnOrdemApresentacaoChanged();

	    private Int16 _OrdemApresentacao;

	    [DataMember(IsRequired = true, Name = "OrdemApresentacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "OrdemApresentacao", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA.ORDEM_APRESENTACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.ORDEM_APRESENTACAO")]
	    public Int16 OrdemApresentacao
	    {
	    	    get
	    	    {
	    	          return _OrdemApresentacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._OrdemApresentacao != value)
	    	          {
	    	              this.ValidateProperty("OrdemApresentacao", value);
	    	              this.OnOrdemApresentacaoChanging(value);
	    	              this.RaiseDataMemberChanging("OrdemApresentacao");
	    	              this._OrdemApresentacao = value;
	    	              this.RaiseDataMemberChanged("OrdemApresentacao");
	    	              this.OnOrdemApresentacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For TamanhoMidia
	    partial void OnTamanhoMidiaChanging(System.Nullable<System.Int32> value);
	    partial void OnTamanhoMidiaChanged();

	    private System.Nullable<System.Int32> _TamanhoMidia;

	    [DataMember(Name = "TamanhoMidia", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tamanho Midia", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.TAMANHO_MIDIA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.TAMANHO_MIDIA")]
	    public System.Nullable<System.Int32> TamanhoMidia
	    {
	    	    get
	    	    {
	    	          return _TamanhoMidia;
	    	    }
	    	    set
	    	    {
	    	          if (this._TamanhoMidia != value)
	    	          {
	    	              this.ValidateProperty("TamanhoMidia", value);
	    	              this.OnTamanhoMidiaChanging(value);
	    	              this.RaiseDataMemberChanging("TamanhoMidia");
	    	              this._TamanhoMidia = value;
	    	              this.RaiseDataMemberChanged("TamanhoMidia");
	    	              this.OnTamanhoMidiaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Thumbnail
	    partial void OnThumbnailChanging(Byte[] value);
	    partial void OnThumbnailChanged();

	    private Byte[] _Thumbnail;

	    [DataMember(Name = "Thumbnail", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Thumbnail", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidia];LookUpTitle[Seleção de (Thumbnail)];LookUpQuery[executeLookUpDocMultimidia];LookUpFinalize[finalizeLookUpDocMultimidia];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DescDocClassificador\" : \"DescDocClassificador\", \"DescDocumento\" : \"DescDocumento\", \"IdDocClassificador\" : \"IdDocClassificador\", \"LxTipoDocumento\" : \"LxTipoDocumento\", \"LxTipoExtensao\" : \"LxTipoExtensao\", \"Obs\" : \"Obs\", \"Thumbnail\" : \"Thumbnail\", \"UidDocumento\" : \"UidDocumento\", \"Url\" : \"Url\", \"XmlMapeamento\" : \"XmlMapeamento\"}];LookUpColumns[{\"Conteudo\" : true, \"DescDocClassificador\" : true, \"DescDocumento\" : true, \"IdDocClassificador\" : true, \"LxTipoDocumento\" : true, \"LxTipoExtensao\" : true, \"Obs\" : true, \"Thumbnail\" : true, \"UidDocumento\" : true, \"Url\" : true, \"XmlMapeamento\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.THUMBNAIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Byte[]#Thumbnail#false##30##Thumbnail#7#true##::LookUpDocMultimidia##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#DescDocClassificador,IdDocClassificador[DescDocClassificador,IdDocClassificador]#Conteudo[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];DescDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoExtensao[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Obs[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Thumbnail[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];UidDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Url[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];XmlMapeamento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.THUMBNAIL")]
	    public Byte[] Thumbnail
	    {
	    	    get
	    	    {
	    	          return _Thumbnail;
	    	    }
	    	    set
	    	    {
	    	          if (this._Thumbnail != value)
	    	          {
	    	              this.ValidateProperty("Thumbnail", value);
	    	              this.OnThumbnailChanging(value);
	    	              this.RaiseDataMemberChanging("Thumbnail");
	    	              this._Thumbnail = value;
	    	              this.RaiseDataMemberChanged("Thumbnail");
	    	              this.OnThumbnailChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For TipoConteudoHttp
	    partial void OnTipoConteudoHttpChanging(System.String value);
	    partial void OnTipoConteudoHttpChanged();

	    private System.String _TipoConteudoHttp;

	    [DataMember(Name = "TipoConteudoHttp", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo Conteudo Http", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(100)]
	    [FunctionalPoint("Precision[100:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.TIPO_CONTEUDO_HTTP];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.TIPO_CONTEUDO_HTTP")]
	    public System.String TipoConteudoHttp
	    {
	    	    get
	    	    {
	    	          return _TipoConteudoHttp;
	    	    }
	    	    set
	    	    {
	    	          if (this._TipoConteudoHttp != value)
	    	          {
	    	              this.ValidateProperty("TipoConteudoHttp", value);
	    	              this.OnTipoConteudoHttpChanging(value);
	    	              this.RaiseDataMemberChanging("TipoConteudoHttp");
	    	              this._TipoConteudoHttp = value;
	    	              this.RaiseDataMemberChanged("TipoConteudoHttp");
	    	              this.OnTipoConteudoHttpChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidChave
	    partial void OnUidChaveChanging(System.Guid value);
	    partial void OnUidChaveChanged();

	    private System.Guid _UidChave;

	    [DataMember(IsRequired = true, Name = "UidChave", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "UidChave", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA.UID_CHAVE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.UID_CHAVE")]
	    public System.Guid UidChave
	    {
	    	    get
	    	    {
	    	          return _UidChave;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidChave != value)
	    	          {
	    	              this.ValidateProperty("UidChave", value);
	    	              this.OnUidChaveChanging(value);
	    	              this.RaiseDataMemberChanging("UidChave");
	    	              this._UidChave = value;
	    	              this.RaiseDataMemberChanged("UidChave");
	    	              this.OnUidChaveChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidDocumento
	    partial void OnUidDocumentoChanging(System.Guid value);
	    partial void OnUidDocumentoChanged();

	    private System.Guid _UidDocumento;

	    [DataMember(IsRequired = true, Name = "UidDocumento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "UidDocumento", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidia];LookUpTitle[Seleção de (UidDocumento)];LookUpQuery[executeLookUpDocMultimidia];LookUpFinalize[finalizeLookUpDocMultimidia];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DescDocClassificador\" : \"DescDocClassificador\", \"DescDocumento\" : \"DescDocumento\", \"IdDocClassificador\" : \"IdDocClassificador\", \"LxTipoDocumento\" : \"LxTipoDocumento\", \"LxTipoExtensao\" : \"LxTipoExtensao\", \"Obs\" : \"Obs\", \"Thumbnail\" : \"Thumbnail\", \"UidDocumento\" : \"UidDocumento\", \"Url\" : \"Url\", \"XmlMapeamento\" : \"XmlMapeamento\"}];LookUpColumns[{\"Conteudo\" : true, \"DescDocClassificador\" : true, \"DescDocumento\" : true, \"IdDocClassificador\" : true, \"LxTipoDocumento\" : true, \"LxTipoExtensao\" : true, \"Obs\" : true, \"Thumbnail\" : true, \"UidDocumento\" : true, \"Url\" : true, \"XmlMapeamento\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.UID_DOCUMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Guid#UidDocumento#true##12:0##UidDocumento#8#true##::LookUpDocMultimidia##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#DescDocClassificador,IdDocClassificador[DescDocClassificador,IdDocClassificador]#Conteudo[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];DescDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoExtensao[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Obs[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Thumbnail[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];UidDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Url[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];XmlMapeamento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.UID_DOCUMENTO")]
	    public System.Guid UidDocumento
	    {
	    	    get
	    	    {
	    	          return _UidDocumento;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidDocumento != value)
	    	          {
	    	              this.ValidateProperty("UidDocumento", value);
	    	              this.OnUidDocumentoChanging(value);
	    	              this.RaiseDataMemberChanging("UidDocumento");
	    	              this._UidDocumento = value;
	    	              this.RaiseDataMemberChanged("UidDocumento");
	    	              this.OnUidDocumentoChanged();
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
	    [Display(Name = "Uid Tabela", Description="", Order = 5, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA.UID_TABELA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.UID_TABELA")]
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
	    //Extensibility Partial Method Definitions For Url
	    partial void OnUrlChanging(System.String value);
	    partial void OnUrlChanged();

	    private System.String _Url;

	    [DataMember(Name = "Url", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Url", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(500)]
	    [FunctionalPoint("Precision[500:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidia];LookUpTitle[Seleção de (Url)];LookUpQuery[executeLookUpDocMultimidia];LookUpFinalize[finalizeLookUpDocMultimidia];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DescDocClassificador\" : \"DescDocClassificador\", \"DescDocumento\" : \"DescDocumento\", \"IdDocClassificador\" : \"IdDocClassificador\", \"LxTipoDocumento\" : \"LxTipoDocumento\", \"LxTipoExtensao\" : \"LxTipoExtensao\", \"Obs\" : \"Obs\", \"Thumbnail\" : \"Thumbnail\", \"UidDocumento\" : \"UidDocumento\", \"Url\" : \"Url\", \"XmlMapeamento\" : \"XmlMapeamento\"}];LookUpColumns[{\"Conteudo\" : true, \"DescDocClassificador\" : true, \"DescDocumento\" : true, \"IdDocClassificador\" : true, \"LxTipoDocumento\" : true, \"LxTipoExtensao\" : true, \"Obs\" : true, \"Thumbnail\" : true, \"UidDocumento\" : true, \"Url\" : true, \"XmlMapeamento\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.URL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#Url#false##5000##Url#9#true##::LookUpDocMultimidia##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#DescDocClassificador,IdDocClassificador[DescDocClassificador,IdDocClassificador]#Conteudo[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];DescDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoExtensao[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Obs[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Thumbnail[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];UidDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Url[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];XmlMapeamento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.URL")]
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
	    //Extensibility Partial Method Definitions For XmlMapeamento
	    partial void OnXmlMapeamentoChanging(System.String value);
	    partial void OnXmlMapeamentoChanged();

	    private System.String _XmlMapeamento;

	    [DataMember(Name = "XmlMapeamento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "XmlMapeamento", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidia];LookUpTitle[Seleção de (XmlMapeamento)];LookUpQuery[executeLookUpDocMultimidia];LookUpFinalize[finalizeLookUpDocMultimidia];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DescDocClassificador\" : \"DescDocClassificador\", \"DescDocumento\" : \"DescDocumento\", \"IdDocClassificador\" : \"IdDocClassificador\", \"LxTipoDocumento\" : \"LxTipoDocumento\", \"LxTipoExtensao\" : \"LxTipoExtensao\", \"Obs\" : \"Obs\", \"Thumbnail\" : \"Thumbnail\", \"UidDocumento\" : \"UidDocumento\", \"Url\" : \"Url\", \"XmlMapeamento\" : \"XmlMapeamento\"}];LookUpColumns[{\"Conteudo\" : true, \"DescDocClassificador\" : true, \"DescDocumento\" : true, \"IdDocClassificador\" : true, \"LxTipoDocumento\" : true, \"LxTipoExtensao\" : true, \"Obs\" : true, \"Thumbnail\" : true, \"UidDocumento\" : true, \"Url\" : true, \"XmlMapeamento\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.XML_MAPEAMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#XmlMapeamento#false##0##XmlMapeamento#10#true##::LookUpDocMultimidia##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#DescDocClassificador,IdDocClassificador[DescDocClassificador,IdDocClassificador]#Conteudo[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];DescDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoExtensao[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Obs[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Thumbnail[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];UidDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Url[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];XmlMapeamento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.XML_MAPEAMENTO")]
	    public System.String XmlMapeamento
	    {
	    	    get
	    	    {
	    	          return _XmlMapeamento;
	    	    }
	    	    set
	    	    {
	    	          if (this._XmlMapeamento != value)
	    	          {
	    	              this.ValidateProperty("XmlMapeamento", value);
	    	              this.OnXmlMapeamentoChanging(value);
	    	              this.RaiseDataMemberChanging("XmlMapeamento");
	    	              this._XmlMapeamento = value;
	    	              this.RaiseDataMemberChanged("XmlMapeamento");
	    	              this.OnXmlMapeamentoChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.DOC_MULTIMIDIA_TABELA").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.DOC_MULTIMIDIA_TABELA), QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA_TABELA" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.ID_CHAVE", Source = "IdChave", Target = "ID_CHAVE", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA_TABELA", RelationPropertyName = "DOC_MULTIMIDIA_TABELA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.UID_CHAVE", Source = "UidChave", Target = "UID_CHAVE", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA_TABELA", RelationPropertyName = "DOC_MULTIMIDIA_TABELA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.UID_TABELA", Source = "UidTabela", Target = "UID_TABELA", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA_TABELA", RelationPropertyName = "DOC_MULTIMIDIA_TABELA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.ORDEM_APRESENTACAO", Source = "OrdemApresentacao", Target = "ORDEM_APRESENTACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA_TABELA", RelationPropertyName = "DOC_MULTIMIDIA_TABELA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.UID_DOCUMENTO", Source = "UidDocumento", Target = "UID_DOCUMENTO", TargetKeyName = "UID_DOCUMENTO", NoUpdatable = false, IsKey = true, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.DOC_MULTIMIDIA").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.DOC_MULTIMIDIA), QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.OBS", Source = "Obs", Target = "OBS", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.URL", Source = "Url", Target = "URL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.CONTEUDO", Source = "Conteudo", Target = "CONTEUDO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.THUMBNAIL", Source = "Thumbnail", Target = "THUMBNAIL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.DATA_CRIACAO", Source = "DataCriacao", Target = "DATA_CRIACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.NOME_ARQUIVO", Source = "NomeArquivo", Target = "NOME_ARQUIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.LX_TIPO_MIDIA", Source = "LxTipoMidia", Target = "LX_TIPO_MIDIA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.TAMANHO_MIDIA", Source = "TamanhoMidia", Target = "TAMANHO_MIDIA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.UID_DOCUMENTO", Source = "UidDocumento", Target = "UID_DOCUMENTO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.DESC_DOCUMENTO", Source = "DescDocumento", Target = "DESC_DOCUMENTO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.XML_MAPEAMENTO", Source = "XmlMapeamento", Target = "XML_MAPEAMENTO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.LX_TIPO_EXTENSAO", Source = "LxTipoExtensao", Target = "LX_TIPO_EXTENSAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.LX_TIPO_DOCUMENTO", Source = "LxTipoDocumento", Target = "LX_TIPO_DOCUMENTO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.TIPO_CONTEUDO_HTTP", Source = "TipoConteudoHttp", Target = "TIPO_CONTEUDO_HTTP", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.DOC_CLASSIFICADOR.ID_DOC_CLASSIFICADOR", Source = "IdDocClassificador", Target = "ID_DOC_CLASSIFICADOR", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.DOC_CLASSIFICADOR", RelationPropertyName = "DOC_CLASSIFICADOR" });

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

		

	[LinxPublicationView(PrimaryKeys="DOC_MULTIMIDIA_TABELA.ID_CHAVE,DOC_MULTIMIDIA_TABELA.UID_CHAVE,DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.UID_DOCUMENTO,DOC_MULTIMIDIA_TABELA.UID_TABELA", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[DocMultimidiaCompact];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];Entities[DOC_MULTIMIDIA:UidDocumento];SubQueryInfo[];EdmEntityName[DOC_MULTIMIDIA_TABELA];EntityRelations[DOC_MULTIMIDIA(DOC_MULTIMIDIA)#DOC_CLASSIFICADOR(DOC_CLASSIFICADOR)#DOC_MULTIMIDIA1(DOC_MULTIMIDIA)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "DocMultimidiaCompact")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Multimidia.DocMultimidiaCompact")]
	public partial class DocMultimidiaCompact : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For Conteudo
	    partial void OnConteudoChanging(Byte[] value);
	    partial void OnConteudoChanged();

	    private Byte[] _Conteudo;

	    [DataMember(Name = "Conteudo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Conteudo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidiaCompact];LookUpTitle[Seleção de (Conteudo)];LookUpQuery[executeLookUpDocMultimidiaCompact];LookUpFinalize[finalizeLookUpDocMultimidiaCompact];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DescDocumento\" : \"DescDocumento\", \"Thumbnail\" : \"Thumbnail\", \"UidDocumento\" : \"UidDocumento\", \"Url\" : \"Url\"}];LookUpColumns[{\"Conteudo\" : true, \"DescDocumento\" : true, \"Thumbnail\" : true, \"UidDocumento\" : true, \"Url\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.CONTEUDO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Byte[]#Conteudo#false##30##Conteudo#0#true##::LookUpDocMultimidiaCompact##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable###true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.CONTEUDO")]
	    public Byte[] Conteudo
	    {
	    	    get
	    	    {
	    	          return _Conteudo;
	    	    }
	    	    set
	    	    {
	    	          if (this._Conteudo != value)
	    	          {
	    	              this.ValidateProperty("Conteudo", value);
	    	              this.OnConteudoChanging(value);
	    	              this.RaiseDataMemberChanging("Conteudo");
	    	              this._Conteudo = value;
	    	              this.RaiseDataMemberChanged("Conteudo");
	    	              this.OnConteudoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescDocumento
	    partial void OnDescDocumentoChanging(System.String value);
	    partial void OnDescDocumentoChanged();

	    private System.String _DescDocumento;

	    [DataMember(IsRequired = true, Name = "DescDocumento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "DescDocumento", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidiaCompact];LookUpTitle[Seleção de (DescDocumento)];LookUpQuery[executeLookUpDocMultimidiaCompact];LookUpFinalize[finalizeLookUpDocMultimidiaCompact];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DescDocumento\" : \"DescDocumento\", \"Thumbnail\" : \"Thumbnail\", \"UidDocumento\" : \"UidDocumento\", \"Url\" : \"Url\"}];LookUpColumns[{\"Conteudo\" : true, \"DescDocumento\" : true, \"Thumbnail\" : true, \"UidDocumento\" : true, \"Url\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.DESC_DOCUMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescDocumento#false##600##DescDocumento#1#true##::LookUpDocMultimidiaCompact##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable###true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.DESC_DOCUMENTO")]
	    public System.String DescDocumento
	    {
	    	    get
	    	    {
	    	          return _DescDocumento;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescDocumento != value)
	    	          {
	    	              this.ValidateProperty("DescDocumento", value);
	    	              this.OnDescDocumentoChanging(value);
	    	              this.RaiseDataMemberChanging("DescDocumento");
	    	              this._DescDocumento = value;
	    	              this.RaiseDataMemberChanged("DescDocumento");
	    	              this.OnDescDocumentoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdChave
	    partial void OnIdChaveChanging(Int64 value);
	    partial void OnIdChaveChanged();

	    private Int64 _IdChave;

	    [DataMember(IsRequired = true, Name = "IdChave", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "IdChave", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA.ID_CHAVE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.ID_CHAVE")]
	    public Int64 IdChave
	    {
	    	    get
	    	    {
	    	          return _IdChave;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdChave != value)
	    	          {
	    	              this.ValidateProperty("IdChave", value);
	    	              this.OnIdChaveChanging(value);
	    	              this.RaiseDataMemberChanging("IdChave");
	    	              this._IdChave = value;
	    	              this.RaiseDataMemberChanged("IdChave");
	    	              this.OnIdChaveChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Thumbnail
	    partial void OnThumbnailChanging(Byte[] value);
	    partial void OnThumbnailChanged();

	    private Byte[] _Thumbnail;

	    [DataMember(Name = "Thumbnail", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Thumbnail", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidiaCompact];LookUpTitle[Seleção de (Thumbnail)];LookUpQuery[executeLookUpDocMultimidiaCompact];LookUpFinalize[finalizeLookUpDocMultimidiaCompact];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DescDocumento\" : \"DescDocumento\", \"Thumbnail\" : \"Thumbnail\", \"UidDocumento\" : \"UidDocumento\", \"Url\" : \"Url\"}];LookUpColumns[{\"Conteudo\" : true, \"DescDocumento\" : true, \"Thumbnail\" : true, \"UidDocumento\" : true, \"Url\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.THUMBNAIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Byte[]#Thumbnail#false##30##Thumbnail#2#true##::LookUpDocMultimidiaCompact##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable###true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.THUMBNAIL")]
	    public Byte[] Thumbnail
	    {
	    	    get
	    	    {
	    	          return _Thumbnail;
	    	    }
	    	    set
	    	    {
	    	          if (this._Thumbnail != value)
	    	          {
	    	              this.ValidateProperty("Thumbnail", value);
	    	              this.OnThumbnailChanging(value);
	    	              this.RaiseDataMemberChanging("Thumbnail");
	    	              this._Thumbnail = value;
	    	              this.RaiseDataMemberChanged("Thumbnail");
	    	              this.OnThumbnailChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidChave
	    partial void OnUidChaveChanging(System.Guid value);
	    partial void OnUidChaveChanged();

	    private System.Guid _UidChave;

	    [DataMember(IsRequired = true, Name = "UidChave", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "UidChave", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA.UID_CHAVE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.UID_CHAVE")]
	    public System.Guid UidChave
	    {
	    	    get
	    	    {
	    	          return _UidChave;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidChave != value)
	    	          {
	    	              this.ValidateProperty("UidChave", value);
	    	              this.OnUidChaveChanging(value);
	    	              this.RaiseDataMemberChanging("UidChave");
	    	              this._UidChave = value;
	    	              this.RaiseDataMemberChanged("UidChave");
	    	              this.OnUidChaveChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidDocumento
	    partial void OnUidDocumentoChanging(System.Guid value);
	    partial void OnUidDocumentoChanged();

	    private System.Guid _UidDocumento;

	    [DataMember(IsRequired = true, Name = "UidDocumento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "UidDocumento", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidiaCompact];LookUpTitle[Seleção de (UidDocumento)];LookUpQuery[executeLookUpDocMultimidiaCompact];LookUpFinalize[finalizeLookUpDocMultimidiaCompact];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DescDocumento\" : \"DescDocumento\", \"Thumbnail\" : \"Thumbnail\", \"UidDocumento\" : \"UidDocumento\", \"Url\" : \"Url\"}];LookUpColumns[{\"Conteudo\" : true, \"DescDocumento\" : true, \"Thumbnail\" : true, \"UidDocumento\" : true, \"Url\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.UID_DOCUMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Guid#UidDocumento#true##12:0##UidDocumento#3#true##::LookUpDocMultimidiaCompact##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable###true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.UID_DOCUMENTO")]
	    public System.Guid UidDocumento
	    {
	    	    get
	    	    {
	    	          return _UidDocumento;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidDocumento != value)
	    	          {
	    	              this.ValidateProperty("UidDocumento", value);
	    	              this.OnUidDocumentoChanging(value);
	    	              this.RaiseDataMemberChanging("UidDocumento");
	    	              this._UidDocumento = value;
	    	              this.RaiseDataMemberChanged("UidDocumento");
	    	              this.OnUidDocumentoChanged();
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
	    [Display(Name = "Uid Tabela", Description="", Order = 5, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA.UID_TABELA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.UID_TABELA")]
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
	    //Extensibility Partial Method Definitions For Url
	    partial void OnUrlChanging(System.String value);
	    partial void OnUrlChanged();

	    private System.String _Url;

	    [DataMember(Name = "Url", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Url", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(500)]
	    [FunctionalPoint("Precision[500:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidiaCompact];LookUpTitle[Seleção de (Url)];LookUpQuery[executeLookUpDocMultimidiaCompact];LookUpFinalize[finalizeLookUpDocMultimidiaCompact];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DescDocumento\" : \"DescDocumento\", \"Thumbnail\" : \"Thumbnail\", \"UidDocumento\" : \"UidDocumento\", \"Url\" : \"Url\"}];LookUpColumns[{\"Conteudo\" : true, \"DescDocumento\" : true, \"Thumbnail\" : true, \"UidDocumento\" : true, \"Url\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.URL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#Url#false##5000##Url#4#true##::LookUpDocMultimidiaCompact##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable###true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.URL")]
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

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.DOC_MULTIMIDIA_TABELA").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.DOC_MULTIMIDIA_TABELA), QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA_TABELA" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.ID_CHAVE", Source = "IdChave", Target = "ID_CHAVE", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA_TABELA", RelationPropertyName = "DOC_MULTIMIDIA_TABELA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.UID_CHAVE", Source = "UidChave", Target = "UID_CHAVE", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA_TABELA", RelationPropertyName = "DOC_MULTIMIDIA_TABELA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.UID_TABELA", Source = "UidTabela", Target = "UID_TABELA", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA_TABELA", RelationPropertyName = "DOC_MULTIMIDIA_TABELA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.UID_DOCUMENTO", Source = "UidDocumento", Target = "UID_DOCUMENTO", TargetKeyName = "UID_DOCUMENTO", NoUpdatable = false, IsKey = true, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });

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

		

	[LinxPublicationView(PrimaryKeys="DOC_MULTIMIDIA_TABELA.ID_CHAVE,DOC_MULTIMIDIA_TABELA.UID_CHAVE,DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.UID_DOCUMENTO,DOC_MULTIMIDIA_TABELA.UID_TABELA", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[MultimidiaCompact2BO];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];Entities[DOC_MULTIMIDIA:UidDocumento];SubQueryInfo[];EdmEntityName[DOC_MULTIMIDIA_TABELA];EntityRelations[DOC_MULTIMIDIA(DOC_MULTIMIDIA)#DOC_CLASSIFICADOR(DOC_CLASSIFICADOR)#DOC_MULTIMIDIA1(DOC_MULTIMIDIA)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "MultimidiaCompact2BO")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Multimidia.MultimidiaCompact2BO")]
	public partial class MultimidiaCompact2BO : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For Conteudo
	    partial void OnConteudoChanging(Byte[] value);
	    partial void OnConteudoChanged();

	    private Byte[] _Conteudo;

	    [DataMember(Name = "Conteudo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Conteudo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidiaCompact2];LookUpTitle[Seleção de (Conteudo)];LookUpQuery[executeLookUpDocMultimidiaCompact2];LookUpFinalize[finalizeLookUpDocMultimidiaCompact2];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DescDocumento\" : \"DescDocumento\", \"IdDocClassificador\" : \"Id Doc Classificador\", \"LxTipoDocumento\" : \"Lx Tipo Documento\", \"LxTipoExtensao\" : \"Lx Tipo Extensao\", \"Obs\" : \"Obs\", \"Thumbnail\" : \"Thumbnail\", \"UidDocumento\" : \"Uid Documento\", \"Url\" : \"Url\", \"XmlMapeamento\" : \"XmlMapeamento\"}];LookUpColumns[{\"Conteudo\" : true, \"DescDocumento\" : true, \"IdDocClassificador\" : true, \"LxTipoDocumento\" : true, \"LxTipoExtensao\" : true, \"Obs\" : true, \"Thumbnail\" : true, \"UidDocumento\" : true, \"Url\" : true, \"XmlMapeamento\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.CONTEUDO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Byte[]#Conteudo#false##30##Conteudo#0#true##::LookUpDocMultimidiaCompact2##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#IdDocClassificador[IdDocClassificador]#Conteudo[IdDocClassificador=IdDocClassificador];DescDocumento[IdDocClassificador=IdDocClassificador];LxTipoDocumento[IdDocClassificador=IdDocClassificador];LxTipoExtensao[IdDocClassificador=IdDocClassificador];Obs[IdDocClassificador=IdDocClassificador];Thumbnail[IdDocClassificador=IdDocClassificador];UidDocumento[IdDocClassificador=IdDocClassificador];Url[IdDocClassificador=IdDocClassificador];XmlMapeamento[IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.CONTEUDO")]
	    public Byte[] Conteudo
	    {
	    	    get
	    	    {
	    	          return _Conteudo;
	    	    }
	    	    set
	    	    {
	    	          if (this._Conteudo != value)
	    	          {
	    	              this.ValidateProperty("Conteudo", value);
	    	              this.OnConteudoChanging(value);
	    	              this.RaiseDataMemberChanging("Conteudo");
	    	              this._Conteudo = value;
	    	              this.RaiseDataMemberChanged("Conteudo");
	    	              this.OnConteudoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescDocumento
	    partial void OnDescDocumentoChanging(System.String value);
	    partial void OnDescDocumentoChanged();

	    private System.String _DescDocumento;

	    [DataMember(Name = "DescDocumento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "DescDocumento", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidiaCompact2];LookUpTitle[Seleção de (DescDocumento)];LookUpQuery[executeLookUpDocMultimidiaCompact2];LookUpFinalize[finalizeLookUpDocMultimidiaCompact2];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DescDocumento\" : \"DescDocumento\", \"IdDocClassificador\" : \"Id Doc Classificador\", \"LxTipoDocumento\" : \"Lx Tipo Documento\", \"LxTipoExtensao\" : \"Lx Tipo Extensao\", \"Obs\" : \"Obs\", \"Thumbnail\" : \"Thumbnail\", \"UidDocumento\" : \"Uid Documento\", \"Url\" : \"Url\", \"XmlMapeamento\" : \"XmlMapeamento\"}];LookUpColumns[{\"Conteudo\" : true, \"DescDocumento\" : true, \"IdDocClassificador\" : true, \"LxTipoDocumento\" : true, \"LxTipoExtensao\" : true, \"Obs\" : true, \"Thumbnail\" : true, \"UidDocumento\" : true, \"Url\" : true, \"XmlMapeamento\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.DESC_DOCUMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescDocumento#false##600##DescDocumento#1#true##::LookUpDocMultimidiaCompact2##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#IdDocClassificador[IdDocClassificador]#Conteudo[IdDocClassificador=IdDocClassificador];DescDocumento[IdDocClassificador=IdDocClassificador];LxTipoDocumento[IdDocClassificador=IdDocClassificador];LxTipoExtensao[IdDocClassificador=IdDocClassificador];Obs[IdDocClassificador=IdDocClassificador];Thumbnail[IdDocClassificador=IdDocClassificador];UidDocumento[IdDocClassificador=IdDocClassificador];Url[IdDocClassificador=IdDocClassificador];XmlMapeamento[IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.DESC_DOCUMENTO")]
	    public System.String DescDocumento
	    {
	    	    get
	    	    {
	    	          return _DescDocumento;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescDocumento != value)
	    	          {
	    	              this.ValidateProperty("DescDocumento", value);
	    	              this.OnDescDocumentoChanging(value);
	    	              this.RaiseDataMemberChanging("DescDocumento");
	    	              this._DescDocumento = value;
	    	              this.RaiseDataMemberChanged("DescDocumento");
	    	              this.OnDescDocumentoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdChave
	    partial void OnIdChaveChanging(Int64 value);
	    partial void OnIdChaveChanged();

	    private Int64 _IdChave;

	    [DataMember(Name = "IdChave", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "IdChave", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA.ID_CHAVE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.ID_CHAVE")]
	    public Int64 IdChave
	    {
	    	    get
	    	    {
	    	          return _IdChave;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdChave != value)
	    	          {
	    	              this.ValidateProperty("IdChave", value);
	    	              this.OnIdChaveChanging(value);
	    	              this.RaiseDataMemberChanging("IdChave");
	    	              this._IdChave = value;
	    	              this.RaiseDataMemberChanged("IdChave");
	    	              this.OnIdChaveChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdDocClassificador
	    partial void OnIdDocClassificadorChanging(Int64 value);
	    partial void OnIdDocClassificadorChanged();

	    private Int64 _IdDocClassificador;

	    [DataMember(Name = "IdDocClassificador", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Doc Classificador", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidiaCompact2];LookUpTitle[Seleção de (Id Doc Classificador)];LookUpQuery[executeLookUpDocMultimidiaCompact2];LookUpFinalize[finalizeLookUpDocMultimidiaCompact2];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DescDocumento\" : \"DescDocumento\", \"IdDocClassificador\" : \"Id Doc Classificador\", \"LxTipoDocumento\" : \"Lx Tipo Documento\", \"LxTipoExtensao\" : \"Lx Tipo Extensao\", \"Obs\" : \"Obs\", \"Thumbnail\" : \"Thumbnail\", \"UidDocumento\" : \"Uid Documento\", \"Url\" : \"Url\", \"XmlMapeamento\" : \"XmlMapeamento\"}];LookUpColumns[{\"Conteudo\" : true, \"DescDocumento\" : true, \"IdDocClassificador\" : true, \"LxTipoDocumento\" : true, \"LxTipoExtensao\" : true, \"Obs\" : true, \"Thumbnail\" : true, \"UidDocumento\" : true, \"Url\" : true, \"XmlMapeamento\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.DOC_CLASSIFICADOR.ID_DOC_CLASSIFICADOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdDocClassificador#true##24:0##Id Doc Classificador#2#true##::LookUpDocMultimidiaCompact2##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#IdDocClassificador[IdDocClassificador]#Conteudo[IdDocClassificador=IdDocClassificador];DescDocumento[IdDocClassificador=IdDocClassificador];LxTipoDocumento[IdDocClassificador=IdDocClassificador];LxTipoExtensao[IdDocClassificador=IdDocClassificador];Obs[IdDocClassificador=IdDocClassificador];Thumbnail[IdDocClassificador=IdDocClassificador];UidDocumento[IdDocClassificador=IdDocClassificador];Url[IdDocClassificador=IdDocClassificador];XmlMapeamento[IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.DOC_CLASSIFICADOR.ID_DOC_CLASSIFICADOR")]
	    public Int64 IdDocClassificador
	    {
	    	    get
	    	    {
	    	          return _IdDocClassificador;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdDocClassificador != value)
	    	          {
	    	              this.ValidateProperty("IdDocClassificador", value);
	    	              this.OnIdDocClassificadorChanging(value);
	    	              this.RaiseDataMemberChanging("IdDocClassificador");
	    	              this._IdDocClassificador = value;
	    	              this.RaiseDataMemberChanged("IdDocClassificador");
	    	              this.OnIdDocClassificadorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoDocumento
	    partial void OnLxTipoDocumentoChanging(Byte value);
	    partial void OnLxTipoDocumentoChanged();

	    private Byte _LxTipoDocumento;

	    [DataMember(Name = "LxTipoDocumento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Lx Tipo Documento", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoDocumento];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidiaCompact2];LookUpTitle[Seleção de (Lx Tipo Documento)];LookUpQuery[executeLookUpDocMultimidiaCompact2];LookUpFinalize[finalizeLookUpDocMultimidiaCompact2];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DescDocumento\" : \"DescDocumento\", \"IdDocClassificador\" : \"Id Doc Classificador\", \"LxTipoDocumento\" : \"Lx Tipo Documento\", \"LxTipoExtensao\" : \"Lx Tipo Extensao\", \"Obs\" : \"Obs\", \"Thumbnail\" : \"Thumbnail\", \"UidDocumento\" : \"Uid Documento\", \"Url\" : \"Url\", \"XmlMapeamento\" : \"XmlMapeamento\"}];LookUpColumns[{\"Conteudo\" : true, \"DescDocumento\" : true, \"IdDocClassificador\" : true, \"LxTipoDocumento\" : true, \"LxTipoExtensao\" : true, \"Obs\" : true, \"Thumbnail\" : true, \"UidDocumento\" : true, \"Url\" : true, \"XmlMapeamento\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.LX_TIPO_DOCUMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Byte#LxTipoDocumento#false##30##Lx Tipo Documento#3#true##::LookUpDocMultimidiaCompact2##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#IdDocClassificador[IdDocClassificador]#Conteudo[IdDocClassificador=IdDocClassificador];DescDocumento[IdDocClassificador=IdDocClassificador];LxTipoDocumento[IdDocClassificador=IdDocClassificador];LxTipoExtensao[IdDocClassificador=IdDocClassificador];Obs[IdDocClassificador=IdDocClassificador];Thumbnail[IdDocClassificador=IdDocClassificador];UidDocumento[IdDocClassificador=IdDocClassificador];Url[IdDocClassificador=IdDocClassificador];XmlMapeamento[IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.LX_TIPO_DOCUMENTO")]
	    public Byte LxTipoDocumento
	    {
	    	    get
	    	    {
	    	          return _LxTipoDocumento;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoDocumento != value)
	    	          {
	    	              this.ValidateProperty("LxTipoDocumento", value);
	    	              this.OnLxTipoDocumentoChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoDocumento");
	    	              this._LxTipoDocumento = value;
	    	              this.RaiseDataMemberChanged("LxTipoDocumento");
	    	              this.OnLxTipoDocumentoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoExtensao
	    partial void OnLxTipoExtensaoChanging(Byte value);
	    partial void OnLxTipoExtensaoChanged();

	    private Byte _LxTipoExtensao;

	    [DataMember(Name = "LxTipoExtensao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Lx Tipo Extensao", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidiaCompact2];LookUpTitle[Seleção de (Lx Tipo Extensao)];LookUpQuery[executeLookUpDocMultimidiaCompact2];LookUpFinalize[finalizeLookUpDocMultimidiaCompact2];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DescDocumento\" : \"DescDocumento\", \"IdDocClassificador\" : \"Id Doc Classificador\", \"LxTipoDocumento\" : \"Lx Tipo Documento\", \"LxTipoExtensao\" : \"Lx Tipo Extensao\", \"Obs\" : \"Obs\", \"Thumbnail\" : \"Thumbnail\", \"UidDocumento\" : \"Uid Documento\", \"Url\" : \"Url\", \"XmlMapeamento\" : \"XmlMapeamento\"}];LookUpColumns[{\"Conteudo\" : true, \"DescDocumento\" : true, \"IdDocClassificador\" : true, \"LxTipoDocumento\" : true, \"LxTipoExtensao\" : true, \"Obs\" : true, \"Thumbnail\" : true, \"UidDocumento\" : true, \"Url\" : true, \"XmlMapeamento\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.LX_TIPO_EXTENSAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Byte#LxTipoExtensao#false##30##Lx Tipo Extensao#4#true##::LookUpDocMultimidiaCompact2##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#IdDocClassificador[IdDocClassificador]#Conteudo[IdDocClassificador=IdDocClassificador];DescDocumento[IdDocClassificador=IdDocClassificador];LxTipoDocumento[IdDocClassificador=IdDocClassificador];LxTipoExtensao[IdDocClassificador=IdDocClassificador];Obs[IdDocClassificador=IdDocClassificador];Thumbnail[IdDocClassificador=IdDocClassificador];UidDocumento[IdDocClassificador=IdDocClassificador];Url[IdDocClassificador=IdDocClassificador];XmlMapeamento[IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.LX_TIPO_EXTENSAO")]
	    public Byte LxTipoExtensao
	    {
	    	    get
	    	    {
	    	          return _LxTipoExtensao;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoExtensao != value)
	    	          {
	    	              this.ValidateProperty("LxTipoExtensao", value);
	    	              this.OnLxTipoExtensaoChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoExtensao");
	    	              this._LxTipoExtensao = value;
	    	              this.RaiseDataMemberChanged("LxTipoExtensao");
	    	              this.OnLxTipoExtensaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Obs
	    partial void OnObsChanging(System.String value);
	    partial void OnObsChanged();

	    private System.String _Obs;

	    [DataMember(Name = "Obs", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Obs", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidiaCompact2];LookUpTitle[Seleção de (Obs)];LookUpQuery[executeLookUpDocMultimidiaCompact2];LookUpFinalize[finalizeLookUpDocMultimidiaCompact2];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DescDocumento\" : \"DescDocumento\", \"IdDocClassificador\" : \"Id Doc Classificador\", \"LxTipoDocumento\" : \"Lx Tipo Documento\", \"LxTipoExtensao\" : \"Lx Tipo Extensao\", \"Obs\" : \"Obs\", \"Thumbnail\" : \"Thumbnail\", \"UidDocumento\" : \"Uid Documento\", \"Url\" : \"Url\", \"XmlMapeamento\" : \"XmlMapeamento\"}];LookUpColumns[{\"Conteudo\" : true, \"DescDocumento\" : true, \"IdDocClassificador\" : true, \"LxTipoDocumento\" : true, \"LxTipoExtensao\" : true, \"Obs\" : true, \"Thumbnail\" : true, \"UidDocumento\" : true, \"Url\" : true, \"XmlMapeamento\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.OBS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#Obs#false##0##Obs#5#true##::LookUpDocMultimidiaCompact2##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#IdDocClassificador[IdDocClassificador]#Conteudo[IdDocClassificador=IdDocClassificador];DescDocumento[IdDocClassificador=IdDocClassificador];LxTipoDocumento[IdDocClassificador=IdDocClassificador];LxTipoExtensao[IdDocClassificador=IdDocClassificador];Obs[IdDocClassificador=IdDocClassificador];Thumbnail[IdDocClassificador=IdDocClassificador];UidDocumento[IdDocClassificador=IdDocClassificador];Url[IdDocClassificador=IdDocClassificador];XmlMapeamento[IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.OBS")]
	    public System.String Obs
	    {
	    	    get
	    	    {
	    	          return _Obs;
	    	    }
	    	    set
	    	    {
	    	          if (this._Obs != value)
	    	          {
	    	              this.ValidateProperty("Obs", value);
	    	              this.OnObsChanging(value);
	    	              this.RaiseDataMemberChanging("Obs");
	    	              this._Obs = value;
	    	              this.RaiseDataMemberChanged("Obs");
	    	              this.OnObsChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For OrdemApresentacao
	    partial void OnOrdemApresentacaoChanging(Int16 value);
	    partial void OnOrdemApresentacaoChanged();

	    private Int16 _OrdemApresentacao;

	    [DataMember(Name = "OrdemApresentacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "OrdemApresentacao", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA.ORDEM_APRESENTACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.ORDEM_APRESENTACAO")]
	    public Int16 OrdemApresentacao
	    {
	    	    get
	    	    {
	    	          return _OrdemApresentacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._OrdemApresentacao != value)
	    	          {
	    	              this.ValidateProperty("OrdemApresentacao", value);
	    	              this.OnOrdemApresentacaoChanging(value);
	    	              this.RaiseDataMemberChanging("OrdemApresentacao");
	    	              this._OrdemApresentacao = value;
	    	              this.RaiseDataMemberChanged("OrdemApresentacao");
	    	              this.OnOrdemApresentacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Thumbnail
	    partial void OnThumbnailChanging(Byte[] value);
	    partial void OnThumbnailChanged();

	    private Byte[] _Thumbnail;

	    [DataMember(Name = "Thumbnail", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Thumbnail", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidiaCompact2];LookUpTitle[Seleção de (Thumbnail)];LookUpQuery[executeLookUpDocMultimidiaCompact2];LookUpFinalize[finalizeLookUpDocMultimidiaCompact2];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DescDocumento\" : \"DescDocumento\", \"IdDocClassificador\" : \"Id Doc Classificador\", \"LxTipoDocumento\" : \"Lx Tipo Documento\", \"LxTipoExtensao\" : \"Lx Tipo Extensao\", \"Obs\" : \"Obs\", \"Thumbnail\" : \"Thumbnail\", \"UidDocumento\" : \"Uid Documento\", \"Url\" : \"Url\", \"XmlMapeamento\" : \"XmlMapeamento\"}];LookUpColumns[{\"Conteudo\" : true, \"DescDocumento\" : true, \"IdDocClassificador\" : true, \"LxTipoDocumento\" : true, \"LxTipoExtensao\" : true, \"Obs\" : true, \"Thumbnail\" : true, \"UidDocumento\" : true, \"Url\" : true, \"XmlMapeamento\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.THUMBNAIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Byte[]#Thumbnail#false##30##Thumbnail#6#true##::LookUpDocMultimidiaCompact2##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#IdDocClassificador[IdDocClassificador]#Conteudo[IdDocClassificador=IdDocClassificador];DescDocumento[IdDocClassificador=IdDocClassificador];LxTipoDocumento[IdDocClassificador=IdDocClassificador];LxTipoExtensao[IdDocClassificador=IdDocClassificador];Obs[IdDocClassificador=IdDocClassificador];Thumbnail[IdDocClassificador=IdDocClassificador];UidDocumento[IdDocClassificador=IdDocClassificador];Url[IdDocClassificador=IdDocClassificador];XmlMapeamento[IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.THUMBNAIL")]
	    public Byte[] Thumbnail
	    {
	    	    get
	    	    {
	    	          return _Thumbnail;
	    	    }
	    	    set
	    	    {
	    	          if (this._Thumbnail != value)
	    	          {
	    	              this.ValidateProperty("Thumbnail", value);
	    	              this.OnThumbnailChanging(value);
	    	              this.RaiseDataMemberChanging("Thumbnail");
	    	              this._Thumbnail = value;
	    	              this.RaiseDataMemberChanged("Thumbnail");
	    	              this.OnThumbnailChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidChave
	    partial void OnUidChaveChanging(System.Guid value);
	    partial void OnUidChaveChanged();

	    private System.Guid _UidChave;

	    [DataMember(Name = "UidChave", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "UidChave", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA.UID_CHAVE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.UID_CHAVE")]
	    public System.Guid UidChave
	    {
	    	    get
	    	    {
	    	          return _UidChave;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidChave != value)
	    	          {
	    	              this.ValidateProperty("UidChave", value);
	    	              this.OnUidChaveChanging(value);
	    	              this.RaiseDataMemberChanging("UidChave");
	    	              this._UidChave = value;
	    	              this.RaiseDataMemberChanged("UidChave");
	    	              this.OnUidChaveChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidDocumento
	    partial void OnUidDocumentoChanging(System.Guid value);
	    partial void OnUidDocumentoChanged();

	    private System.Guid _UidDocumento;

	    [DataMember(Name = "UidDocumento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Documento", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidiaCompact2];LookUpTitle[Seleção de (Uid Documento)];LookUpQuery[executeLookUpDocMultimidiaCompact2];LookUpFinalize[finalizeLookUpDocMultimidiaCompact2];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DescDocumento\" : \"DescDocumento\", \"IdDocClassificador\" : \"Id Doc Classificador\", \"LxTipoDocumento\" : \"Lx Tipo Documento\", \"LxTipoExtensao\" : \"Lx Tipo Extensao\", \"Obs\" : \"Obs\", \"Thumbnail\" : \"Thumbnail\", \"UidDocumento\" : \"Uid Documento\", \"Url\" : \"Url\", \"XmlMapeamento\" : \"XmlMapeamento\"}];LookUpColumns[{\"Conteudo\" : true, \"DescDocumento\" : true, \"IdDocClassificador\" : true, \"LxTipoDocumento\" : true, \"LxTipoExtensao\" : true, \"Obs\" : true, \"Thumbnail\" : true, \"UidDocumento\" : true, \"Url\" : true, \"XmlMapeamento\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.UID_DOCUMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Guid#UidDocumento#true##12:0##Uid Documento#7#true##::LookUpDocMultimidiaCompact2##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#IdDocClassificador[IdDocClassificador]#Conteudo[IdDocClassificador=IdDocClassificador];DescDocumento[IdDocClassificador=IdDocClassificador];LxTipoDocumento[IdDocClassificador=IdDocClassificador];LxTipoExtensao[IdDocClassificador=IdDocClassificador];Obs[IdDocClassificador=IdDocClassificador];Thumbnail[IdDocClassificador=IdDocClassificador];UidDocumento[IdDocClassificador=IdDocClassificador];Url[IdDocClassificador=IdDocClassificador];XmlMapeamento[IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.UID_DOCUMENTO")]
	    public System.Guid UidDocumento
	    {
	    	    get
	    	    {
	    	          return _UidDocumento;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidDocumento != value)
	    	          {
	    	              this.ValidateProperty("UidDocumento", value);
	    	              this.OnUidDocumentoChanging(value);
	    	              this.RaiseDataMemberChanging("UidDocumento");
	    	              this._UidDocumento = value;
	    	              this.RaiseDataMemberChanged("UidDocumento");
	    	              this.OnUidDocumentoChanged();
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
	    [Display(Name = "Uid Tabela", Description="", Order = 5, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA.UID_TABELA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.UID_TABELA")]
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
	    //Extensibility Partial Method Definitions For Url
	    partial void OnUrlChanging(System.String value);
	    partial void OnUrlChanged();

	    private System.String _Url;

	    [DataMember(Name = "Url", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Url", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[500:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidiaCompact2];LookUpTitle[Seleção de (Url)];LookUpQuery[executeLookUpDocMultimidiaCompact2];LookUpFinalize[finalizeLookUpDocMultimidiaCompact2];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DescDocumento\" : \"DescDocumento\", \"IdDocClassificador\" : \"Id Doc Classificador\", \"LxTipoDocumento\" : \"Lx Tipo Documento\", \"LxTipoExtensao\" : \"Lx Tipo Extensao\", \"Obs\" : \"Obs\", \"Thumbnail\" : \"Thumbnail\", \"UidDocumento\" : \"Uid Documento\", \"Url\" : \"Url\", \"XmlMapeamento\" : \"XmlMapeamento\"}];LookUpColumns[{\"Conteudo\" : true, \"DescDocumento\" : true, \"IdDocClassificador\" : true, \"LxTipoDocumento\" : true, \"LxTipoExtensao\" : true, \"Obs\" : true, \"Thumbnail\" : true, \"UidDocumento\" : true, \"Url\" : true, \"XmlMapeamento\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.URL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#Url#false##5000##Url#8#true##::LookUpDocMultimidiaCompact2##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#IdDocClassificador[IdDocClassificador]#Conteudo[IdDocClassificador=IdDocClassificador];DescDocumento[IdDocClassificador=IdDocClassificador];LxTipoDocumento[IdDocClassificador=IdDocClassificador];LxTipoExtensao[IdDocClassificador=IdDocClassificador];Obs[IdDocClassificador=IdDocClassificador];Thumbnail[IdDocClassificador=IdDocClassificador];UidDocumento[IdDocClassificador=IdDocClassificador];Url[IdDocClassificador=IdDocClassificador];XmlMapeamento[IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.URL")]
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
	    //Extensibility Partial Method Definitions For XmlMapeamento
	    partial void OnXmlMapeamentoChanging(System.String value);
	    partial void OnXmlMapeamentoChanged();

	    private System.String _XmlMapeamento;

	    [DataMember(Name = "XmlMapeamento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "XmlMapeamento", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidiaCompact2];LookUpTitle[Seleção de (XmlMapeamento)];LookUpQuery[executeLookUpDocMultimidiaCompact2];LookUpFinalize[finalizeLookUpDocMultimidiaCompact2];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DescDocumento\" : \"DescDocumento\", \"IdDocClassificador\" : \"Id Doc Classificador\", \"LxTipoDocumento\" : \"Lx Tipo Documento\", \"LxTipoExtensao\" : \"Lx Tipo Extensao\", \"Obs\" : \"Obs\", \"Thumbnail\" : \"Thumbnail\", \"UidDocumento\" : \"Uid Documento\", \"Url\" : \"Url\", \"XmlMapeamento\" : \"XmlMapeamento\"}];LookUpColumns[{\"Conteudo\" : true, \"DescDocumento\" : true, \"IdDocClassificador\" : true, \"LxTipoDocumento\" : true, \"LxTipoExtensao\" : true, \"Obs\" : true, \"Thumbnail\" : true, \"UidDocumento\" : true, \"Url\" : true, \"XmlMapeamento\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.XML_MAPEAMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#XmlMapeamento#false##0##XmlMapeamento#9#true##::LookUpDocMultimidiaCompact2##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#IdDocClassificador[IdDocClassificador]#Conteudo[IdDocClassificador=IdDocClassificador];DescDocumento[IdDocClassificador=IdDocClassificador];LxTipoDocumento[IdDocClassificador=IdDocClassificador];LxTipoExtensao[IdDocClassificador=IdDocClassificador];Obs[IdDocClassificador=IdDocClassificador];Thumbnail[IdDocClassificador=IdDocClassificador];UidDocumento[IdDocClassificador=IdDocClassificador];Url[IdDocClassificador=IdDocClassificador];XmlMapeamento[IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.XML_MAPEAMENTO")]
	    public System.String XmlMapeamento
	    {
	    	    get
	    	    {
	    	          return _XmlMapeamento;
	    	    }
	    	    set
	    	    {
	    	          if (this._XmlMapeamento != value)
	    	          {
	    	              this.ValidateProperty("XmlMapeamento", value);
	    	              this.OnXmlMapeamentoChanging(value);
	    	              this.RaiseDataMemberChanging("XmlMapeamento");
	    	              this._XmlMapeamento = value;
	    	              this.RaiseDataMemberChanged("XmlMapeamento");
	    	              this.OnXmlMapeamentoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For TipoExtensao
	    partial void OnTipoExtensaoChanging(System.String value);
	    partial void OnTipoExtensaoChanged();

	    private System.String _TipoExtensao;

	    [DataMember(Name = "TipoExtensao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public System.String TipoExtensao
	    {
	    	    get
	    	    {
	    	          if (_TipoExtensao != (GetExtensionDomainString(LxTipoExtensao)))
	    	             _TipoExtensao =  GetExtensionDomainString(LxTipoExtensao);
	    	          return _TipoExtensao;
	    	    }
	    	    set
	    	    {
	    	          if (this._TipoExtensao != value)
	    	          {
	    	              this.ValidateProperty("TipoExtensao", value);
	    	              this.OnTipoExtensaoChanging(value);
	    	              this.RaiseDataMemberChanging("TipoExtensao");
	    	              this._TipoExtensao = value;
	    	              this.RaiseDataMemberChanged("TipoExtensao");
	    	              this.OnTipoExtensaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescTabela
	    partial void OnDescTabelaChanging(string value);
	    partial void OnDescTabelaChanged();

	    private string _DescTabela;

	    [DataMember(IsRequired = true, Name = "DescTabela", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[''];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="\"\"")]
	    public string DescTabela
	    {
	    	    get
	    	    {
	    	          if (_DescTabela != (GetDescTabela()))
	    	             _DescTabela =  GetDescTabela();
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
	    //Extensibility Partial Method Definitions For NomeTabela
	    partial void OnNomeTabelaChanging(string value);
	    partial void OnNomeTabelaChanged();

	    private string _NomeTabela;

	    [DataMember(IsRequired = true, Name = "NomeTabela", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[''];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="\"\"")]
	    public string NomeTabela
	    {
	    	    get
	    	    {
	    	          if (_NomeTabela != (GetNomeTabela()))
	    	             _NomeTabela =  GetNomeTabela();
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

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.DOC_MULTIMIDIA_TABELA").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.DOC_MULTIMIDIA_TABELA), QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA_TABELA" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.ID_CHAVE", Source = "IdChave", Target = "ID_CHAVE", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA_TABELA", RelationPropertyName = "DOC_MULTIMIDIA_TABELA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.UID_CHAVE", Source = "UidChave", Target = "UID_CHAVE", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA_TABELA", RelationPropertyName = "DOC_MULTIMIDIA_TABELA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.UID_TABELA", Source = "UidTabela", Target = "UID_TABELA", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA_TABELA", RelationPropertyName = "DOC_MULTIMIDIA_TABELA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.ORDEM_APRESENTACAO", Source = "OrdemApresentacao", Target = "ORDEM_APRESENTACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA_TABELA", RelationPropertyName = "DOC_MULTIMIDIA_TABELA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.UID_DOCUMENTO", Source = "UidDocumento", Target = "UID_DOCUMENTO", TargetKeyName = "UID_DOCUMENTO", NoUpdatable = false, IsKey = true, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.DOC_MULTIMIDIA").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.DOC_MULTIMIDIA), QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.OBS", Source = "Obs", Target = "OBS", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.URL", Source = "Url", Target = "URL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.CONTEUDO", Source = "Conteudo", Target = "CONTEUDO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.THUMBNAIL", Source = "Thumbnail", Target = "THUMBNAIL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.UID_DOCUMENTO", Source = "UidDocumento", Target = "UID_DOCUMENTO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.DESC_DOCUMENTO", Source = "DescDocumento", Target = "DESC_DOCUMENTO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.XML_MAPEAMENTO", Source = "XmlMapeamento", Target = "XML_MAPEAMENTO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.LX_TIPO_EXTENSAO", Source = "LxTipoExtensao", Target = "LX_TIPO_EXTENSAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.LX_TIPO_DOCUMENTO", Source = "LxTipoDocumento", Target = "LX_TIPO_DOCUMENTO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.DOC_CLASSIFICADOR.ID_DOC_CLASSIFICADOR", Source = "IdDocClassificador", Target = "ID_DOC_CLASSIFICADOR", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.DOC_CLASSIFICADOR", RelationPropertyName = "DOC_CLASSIFICADOR" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxTipoDocumentoValues()
	    {
	    	    return Linx.Framework.BV.Domains.TipoDocumento.GetValues();
	    }
	    private string _lxTipoDocumentoName;
	    [DataMember(IsRequired = false, Name = "LxTipoDocumentoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Lx Tipo Documento", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxTipoDocumentoName
	    {
	    	    get { if (this.LxTipoDocumento.IsNull()) { _lxTipoDocumentoName = String.Empty; } else { string key = this.LxTipoDocumento.ToString(); var dmValues = this.GetLxTipoDocumentoValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxTipoDocumentoName) _lxTipoDocumentoName = domainName; } return _lxTipoDocumentoName; } set { _lxTipoDocumentoName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="DOC_MULTIMIDIA.UID_DOCUMENTO", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[DocMultimidiaUid];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[UidDocumento];ReadOnly[false];Entities[DOC_MULTIMIDIA:UidDocumento];SubQueryInfo[];EdmEntityName[DOC_MULTIMIDIA];EntityRelations[DOC_CLASSIFICADOR(DOC_CLASSIFICADOR)#DOC_MULTIMIDIA1(DOC_MULTIMIDIA)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "DocMultimidiaUid")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Multimidia.DocMultimidiaUid")]
	public partial class DocMultimidiaUid : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For UidDocumento
	    partial void OnUidDocumentoChanging(System.Guid value);
	    partial void OnUidDocumentoChanged();

	    private System.Guid _UidDocumento;

	    [DataMember(IsRequired = true, Name = "UidDocumento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "UidDocumento", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA.UID_DOCUMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.UID_DOCUMENTO")]
	    public System.Guid UidDocumento
	    {
	    	    get
	    	    {
	    	          return _UidDocumento;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidDocumento != value)
	    	          {
	    	              this.ValidateProperty("UidDocumento", value);
	    	              this.OnUidDocumentoChanging(value);
	    	              this.RaiseDataMemberChanging("UidDocumento");
	    	              this._UidDocumento = value;
	    	              this.RaiseDataMemberChanged("UidDocumento");
	    	              this.OnUidDocumentoChanged();
	    	          }
	    	    }
	    }

	    private System.Guid _TemporaryUidDocumento;
	    [DataMember(Name = "TemporaryUidDocumento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "UidDocumento (Tmp)", Description="Temporary Key", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public System.Guid TemporaryUidDocumento
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryUidDocumento.IsNullOrEmpty())
	    	                this._TemporaryUidDocumento = this._UidDocumento;
	    	          return this._TemporaryUidDocumento;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryUidDocumento != value)
	    	              this._TemporaryUidDocumento = value;
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.DOC_MULTIMIDIA").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.DOC_MULTIMIDIA), QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA.UID_DOCUMENTO", Source = "UidDocumento", Target = "UID_DOCUMENTO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });

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

		

	[LinxPublicationView(PrimaryKeys="DocMultimidiaInfo.EntityUniqueKey", IsUpdatable=false, EdmName="")]
		
	[DataContract(IsReference = false, Name = "DocMultimidiaInfo")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Multimidia.DocMultimidiaInfo")]
	public partial class DocMultimidiaInfo 
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
	 


	    private Guid _UidDocumento;

	    [DataMember(Name = "UidDocumento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public Guid UidDocumento
	    {
	    	    get
	    	    {
	    	          return _UidDocumento;
	    	    }
	    	    set
	    	    {
	    	          this._UidDocumento = value;
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

	    private int _OrdemApresentacao;

	    [DataMember(Name = "OrdemApresentacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public int OrdemApresentacao
	    {
	    	    get
	    	    {
	    	          return _OrdemApresentacao;
	    	    }
	    	    set
	    	    {
	    	          this._OrdemApresentacao = value;
	    	    }
	    }

	    private byte _TipoDocumento;

	    [DataMember(Name = "TipoDocumento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public byte TipoDocumento
	    {
	    	    get
	    	    {
	    	          return _TipoDocumento;
	    	    }
	    	    set
	    	    {
	    	          this._TipoDocumento = value;
	    	    }
	    }

	    private string _DescricaoTipoDocumento;

	    [DataMember(Name = "DescricaoTipoDocumento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string DescricaoTipoDocumento
	    {
	    	    get
	    	    {
	    	          return _DescricaoTipoDocumento;
	    	    }
	    	    set
	    	    {
	    	          this._DescricaoTipoDocumento = value;
	    	    }
	    }

	    private byte _TipoMidia;

	    [DataMember(Name = "TipoMidia", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public byte TipoMidia
	    {
	    	    get
	    	    {
	    	          return _TipoMidia;
	    	    }
	    	    set
	    	    {
	    	          this._TipoMidia = value;
	    	    }
	    }

	    private string _DescricaoTipoMidia;

	    [DataMember(Name = "DescricaoTipoMidia", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string DescricaoTipoMidia
	    {
	    	    get
	    	    {
	    	          return _DescricaoTipoMidia;
	    	    }
	    	    set
	    	    {
	    	          this._DescricaoTipoMidia = value;
	    	    }
	    }

	    private string _NomeArquivo;

	    [DataMember(Name = "NomeArquivo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string NomeArquivo
	    {
	    	    get
	    	    {
	    	          return _NomeArquivo;
	    	    }
	    	    set
	    	    {
	    	          this._NomeArquivo = value;
	    	    }
	    }

	    private string _TipoConteudoHttp;

	    [DataMember(Name = "TipoConteudoHttp", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string TipoConteudoHttp
	    {
	    	    get
	    	    {
	    	          return _TipoConteudoHttp;
	    	    }
	    	    set
	    	    {
	    	          this._TipoConteudoHttp = value;
	    	    }
	    }

	    private int? _TamanhoMidia;

	    [DataMember(Name = "TamanhoMidia", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public int? TamanhoMidia
	    {
	    	    get
	    	    {
	    	          return _TamanhoMidia;
	    	    }
	    	    set
	    	    {
	    	          this._TamanhoMidia = value;
	    	    }
	    }

	    private string _UrlThumbnail;

	    [DataMember(Name = "UrlThumbnail", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string UrlThumbnail
	    {
	    	    get
	    	    {
	    	          return _UrlThumbnail;
	    	    }
	    	    set
	    	    {
	    	          this._UrlThumbnail = value;
	    	    }
	    }

	    private string _UrlServiceBus;

	    [DataMember(Name = "UrlServiceBus", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string UrlServiceBus
	    {
	    	    get
	    	    {
	    	          return _UrlServiceBus;
	    	    }
	    	    set
	    	    {
	    	          this._UrlServiceBus = value;
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

		

	[LinxPublicationView(PrimaryKeys="DOC_MULTIMIDIA.UID_DOCUMENTO", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[DocMultimidia,DocMultimidia.DocMultimidiaTabelaChild];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[UidDocumento];ReadOnly[false];Entities[DOC_MULTIMIDIA:UidDocumento];SubQueryInfo[];EdmEntityName[DOC_MULTIMIDIA];EntityRelations[DOC_CLASSIFICADOR(DOC_CLASSIFICADOR)#DOC_MULTIMIDIA1(DOC_MULTIMIDIA)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "DocMultimidia")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Multimidia.DocMultimidia")]
	public partial class DocMultimidia : Linx.Data.Entity
	{

	
		
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.DocMultimidiaTabelaChildList != null && this.DocMultimidiaTabelaChildList.Count() > 0)
	      {
	         foreach (var entity in this.DocMultimidiaTabelaChildList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.DocMultimidiaTabelaChildList != null)
	      {
	         foreach (var detail in this.DocMultimidiaTabelaChildList)
	         {
	            detail.ResetDetails();
	         }
	         this.DocMultimidiaTabelaChildList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(MultimidiaDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("DocMultimidiaTabelaChild"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("DocMultimidiaTabelaChild");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "UidDocumento"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.UidDocumento));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load DocMultimidiaTabelaChild and all sub-details
	         if (this.DocMultimidiaTabelaChildList == null || this.DocMultimidiaTabelaChildList.Count() == 0)
	         {
	             if (take > 0)
	                 this.DocMultimidiaTabelaChildList = context.GetPagedDocMultimidiaTabelaChild(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.DocMultimidiaTabelaChildList = (from r in context.GetDocMultimidiaTabelaChildByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _DocMultimidiaTabelaChildElements = changeSet.ChangeSetEntries.Where(e => e.Entity is DocMultimidiaTabelaChild && ((DocMultimidiaTabelaChild)e.Entity).DocMultimidia == null && e.Associations == null && e.OriginalAssociations == null && ((DocMultimidiaTabelaChild)e.Entity).UidDocumento == this.UidDocumento).ToList();
 	      if (_DocMultimidiaTabelaChildElements.Count > 0 && this.DocMultimidiaTabelaChildList.Count() == 0)
 	      {
 	          this.DocMultimidiaTabelaChildList = _DocMultimidiaTabelaChildElements.Select(e => (DocMultimidiaTabelaChild)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _DocMultimidiaTabelaChildElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((DocMultimidiaTabelaChild)detail.Entity).DocMultimidia = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("DocMultimidia", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("DocMultimidiaTabelaChildList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For Conteudo
	    partial void OnConteudoChanging(Byte[] value);
	    partial void OnConteudoChanged();

	    private Byte[] _Conteudo;

	    [DataMember(Name = "Conteudo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Conteudo", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA.CONTEUDO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.CONTEUDO")]
	    public Byte[] Conteudo
	    {
	    	    get
	    	    {
	    	          return _Conteudo;
	    	    }
	    	    set
	    	    {
	    	          if (this._Conteudo != value)
	    	          {
	    	              this.ValidateProperty("Conteudo", value);
	    	              this.OnConteudoChanging(value);
	    	              this.RaiseDataMemberChanging("Conteudo");
	    	              this._Conteudo = value;
	    	              this.RaiseDataMemberChanged("Conteudo");
	    	              this.OnConteudoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DataCriacao
	    partial void OnDataCriacaoChanging(System.DateTime value);
	    partial void OnDataCriacaoChanged();

	    private System.DateTime _DataCriacao;

	    [DataMember(IsRequired = true, Name = "DataCriacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Data Criacao", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA.DATA_CRIACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.DATA_CRIACAO")]
	    public System.DateTime DataCriacao
	    {
	    	    get
	    	    {
	    	          return _DataCriacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataCriacao != value)
	    	          {
	    	              this.ValidateProperty("DataCriacao", value);
	    	              this.OnDataCriacaoChanging(value);
	    	              this.RaiseDataMemberChanging("DataCriacao");
	    	              this._DataCriacao = value;
	    	              this.RaiseDataMemberChanged("DataCriacao");
	    	              this.OnDataCriacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescDocClassificador
	    partial void OnDescDocClassificadorChanging(System.String value);
	    partial void OnDescDocClassificadorChanged();

	    private System.String _DescDocClassificador;

	    [DataMember(IsRequired = true, Name = "DescDocClassificador", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Desc Doc Classificador", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocClassificador1];LookUpTitle[Seleção de (Desc Doc Classificador)];LookUpQuery[executeLookUpDocClassificador1];LookUpFinalize[finalizeLookUpDocClassificador1];LookUpDisplayColumns[{\"DescDocClassificador\" : \"Desc Doc Classificador\", \"IdDocClassificador\" : \"Id Doc Classificador\"}];LookUpColumns[{\"DescDocClassificador\" : true, \"IdDocClassificador\" : true}];FilterDataKey[DOC_MULTIMIDIA.DOC_CLASSIFICADOR.DESC_DOC_CLASSIFICADOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescDocClassificador#false##60:0##Desc Doc Classificador#0#true##::LookUpDocClassificador1##false#false#DOC_CLASSIFICADOR#DOC_CLASSIFICADOR#Linx.Framework.BV.Multimidia#IQueryable###true#false", EdmKey="DOC_MULTIMIDIA.DOC_CLASSIFICADOR.DESC_DOC_CLASSIFICADOR")]
	    public System.String DescDocClassificador
	    {
	    	    get
	    	    {
	    	          return _DescDocClassificador;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescDocClassificador != value)
	    	          {
	    	              this.ValidateProperty("DescDocClassificador", value);
	    	              this.OnDescDocClassificadorChanging(value);
	    	              this.RaiseDataMemberChanging("DescDocClassificador");
	    	              this._DescDocClassificador = value;
	    	              this.RaiseDataMemberChanged("DescDocClassificador");
	    	              this.OnDescDocClassificadorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescDocumento
	    partial void OnDescDocumentoChanging(System.String value);
	    partial void OnDescDocumentoChanged();

	    private System.String _DescDocumento;

	    [DataMember(IsRequired = true, Name = "DescDocumento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Desc Documento", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA.DESC_DOCUMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.DESC_DOCUMENTO")]
	    public System.String DescDocumento
	    {
	    	    get
	    	    {
	    	          return _DescDocumento;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescDocumento != value)
	    	          {
	    	              this.ValidateProperty("DescDocumento", value);
	    	              this.OnDescDocumentoChanging(value);
	    	              this.RaiseDataMemberChanging("DescDocumento");
	    	              this._DescDocumento = value;
	    	              this.RaiseDataMemberChanged("DescDocumento");
	    	              this.OnDescDocumentoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdDocClassificador
	    partial void OnIdDocClassificadorChanging(Int64 value);
	    partial void OnIdDocClassificadorChanged();

	    private Int64 _IdDocClassificador;

	    [DataMember(IsRequired = true, Name = "IdDocClassificador", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Doc Classificador", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocClassificador1];LookUpTitle[Seleção de (Id Doc Classificador)];LookUpQuery[executeLookUpDocClassificador1];LookUpFinalize[finalizeLookUpDocClassificador1];LookUpDisplayColumns[{\"DescDocClassificador\" : \"Desc Doc Classificador\", \"IdDocClassificador\" : \"Id Doc Classificador\"}];LookUpColumns[{\"DescDocClassificador\" : true, \"IdDocClassificador\" : true}];FilterDataKey[DOC_MULTIMIDIA.DOC_CLASSIFICADOR.ID_DOC_CLASSIFICADOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdDocClassificador#true##24:0##Id Doc Classificador#1#true##::LookUpDocClassificador1##false#false#DOC_CLASSIFICADOR#DOC_CLASSIFICADOR#Linx.Framework.BV.Multimidia#IQueryable###true#false", EdmKey="DOC_MULTIMIDIA.DOC_CLASSIFICADOR.ID_DOC_CLASSIFICADOR")]
	    public Int64 IdDocClassificador
	    {
	    	    get
	    	    {
	    	          return _IdDocClassificador;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdDocClassificador != value)
	    	          {
	    	              this.ValidateProperty("IdDocClassificador", value);
	    	              this.OnIdDocClassificadorChanging(value);
	    	              this.RaiseDataMemberChanging("IdDocClassificador");
	    	              this._IdDocClassificador = value;
	    	              this.RaiseDataMemberChanged("IdDocClassificador");
	    	              this.OnIdDocClassificadorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoDocumento
	    partial void OnLxTipoDocumentoChanging(Byte value);
	    partial void OnLxTipoDocumentoChanged();

	    private Byte _LxTipoDocumento;

	    [DataMember(IsRequired = true, Name = "LxTipoDocumento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Lx Tipo Documento", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA.LX_TIPO_DOCUMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.LX_TIPO_DOCUMENTO")]
	    public Byte LxTipoDocumento
	    {
	    	    get
	    	    {
	    	          return _LxTipoDocumento;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoDocumento != value)
	    	          {
	    	              this.ValidateProperty("LxTipoDocumento", value);
	    	              this.OnLxTipoDocumentoChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoDocumento");
	    	              this._LxTipoDocumento = value;
	    	              this.RaiseDataMemberChanged("LxTipoDocumento");
	    	              this.OnLxTipoDocumentoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoExtensao
	    partial void OnLxTipoExtensaoChanging(Byte value);
	    partial void OnLxTipoExtensaoChanged();

	    private Byte _LxTipoExtensao;

	    [DataMember(IsRequired = true, Name = "LxTipoExtensao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Lx Tipo Extensao", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA.LX_TIPO_EXTENSAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.LX_TIPO_EXTENSAO")]
	    public Byte LxTipoExtensao
	    {
	    	    get
	    	    {
	    	          return _LxTipoExtensao;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoExtensao != value)
	    	          {
	    	              this.ValidateProperty("LxTipoExtensao", value);
	    	              this.OnLxTipoExtensaoChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoExtensao");
	    	              this._LxTipoExtensao = value;
	    	              this.RaiseDataMemberChanged("LxTipoExtensao");
	    	              this.OnLxTipoExtensaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoMidia
	    partial void OnLxTipoMidiaChanging(Byte value);
	    partial void OnLxTipoMidiaChanged();

	    private Byte _LxTipoMidia;

	    [DataMember(IsRequired = true, Name = "LxTipoMidia", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Lx Tipo Midia", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA.LX_TIPO_MIDIA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.LX_TIPO_MIDIA")]
	    public Byte LxTipoMidia
	    {
	    	    get
	    	    {
	    	          return _LxTipoMidia;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoMidia != value)
	    	          {
	    	              this.ValidateProperty("LxTipoMidia", value);
	    	              this.OnLxTipoMidiaChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoMidia");
	    	              this._LxTipoMidia = value;
	    	              this.RaiseDataMemberChanged("LxTipoMidia");
	    	              this.OnLxTipoMidiaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeArquivo
	    partial void OnNomeArquivoChanging(System.String value);
	    partial void OnNomeArquivoChanged();

	    private System.String _NomeArquivo;

	    [DataMember(Name = "NomeArquivo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Arquivo", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(150)]
	    [FunctionalPoint("Precision[150:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA.NOME_ARQUIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.NOME_ARQUIVO")]
	    public System.String NomeArquivo
	    {
	    	    get
	    	    {
	    	          return _NomeArquivo;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeArquivo != value)
	    	          {
	    	              this.ValidateProperty("NomeArquivo", value);
	    	              this.OnNomeArquivoChanging(value);
	    	              this.RaiseDataMemberChanging("NomeArquivo");
	    	              this._NomeArquivo = value;
	    	              this.RaiseDataMemberChanged("NomeArquivo");
	    	              this.OnNomeArquivoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Obs
	    partial void OnObsChanging(System.String value);
	    partial void OnObsChanged();

	    private System.String _Obs;

	    [DataMember(Name = "Obs", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Obs", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA.OBS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.OBS")]
	    public System.String Obs
	    {
	    	    get
	    	    {
	    	          return _Obs;
	    	    }
	    	    set
	    	    {
	    	          if (this._Obs != value)
	    	          {
	    	              this.ValidateProperty("Obs", value);
	    	              this.OnObsChanging(value);
	    	              this.RaiseDataMemberChanging("Obs");
	    	              this._Obs = value;
	    	              this.RaiseDataMemberChanged("Obs");
	    	              this.OnObsChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For TamanhoMidia
	    partial void OnTamanhoMidiaChanging(System.Nullable<System.Int32> value);
	    partial void OnTamanhoMidiaChanged();

	    private System.Nullable<System.Int32> _TamanhoMidia;

	    [DataMember(Name = "TamanhoMidia", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tamanho Midia", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA.TAMANHO_MIDIA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.TAMANHO_MIDIA")]
	    public System.Nullable<System.Int32> TamanhoMidia
	    {
	    	    get
	    	    {
	    	          return _TamanhoMidia;
	    	    }
	    	    set
	    	    {
	    	          if (this._TamanhoMidia != value)
	    	          {
	    	              this.ValidateProperty("TamanhoMidia", value);
	    	              this.OnTamanhoMidiaChanging(value);
	    	              this.RaiseDataMemberChanging("TamanhoMidia");
	    	              this._TamanhoMidia = value;
	    	              this.RaiseDataMemberChanged("TamanhoMidia");
	    	              this.OnTamanhoMidiaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Thumbnail
	    partial void OnThumbnailChanging(Byte[] value);
	    partial void OnThumbnailChanged();

	    private Byte[] _Thumbnail;

	    [DataMember(Name = "Thumbnail", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Thumbnail", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA.THUMBNAIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.THUMBNAIL")]
	    public Byte[] Thumbnail
	    {
	    	    get
	    	    {
	    	          return _Thumbnail;
	    	    }
	    	    set
	    	    {
	    	          if (this._Thumbnail != value)
	    	          {
	    	              this.ValidateProperty("Thumbnail", value);
	    	              this.OnThumbnailChanging(value);
	    	              this.RaiseDataMemberChanging("Thumbnail");
	    	              this._Thumbnail = value;
	    	              this.RaiseDataMemberChanged("Thumbnail");
	    	              this.OnThumbnailChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For TipoConteudoHttp
	    partial void OnTipoConteudoHttpChanging(System.String value);
	    partial void OnTipoConteudoHttpChanged();

	    private System.String _TipoConteudoHttp;

	    [DataMember(Name = "TipoConteudoHttp", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo Conteudo Http", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(100)]
	    [FunctionalPoint("Precision[100:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA.TIPO_CONTEUDO_HTTP];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.TIPO_CONTEUDO_HTTP")]
	    public System.String TipoConteudoHttp
	    {
	    	    get
	    	    {
	    	          return _TipoConteudoHttp;
	    	    }
	    	    set
	    	    {
	    	          if (this._TipoConteudoHttp != value)
	    	          {
	    	              this.ValidateProperty("TipoConteudoHttp", value);
	    	              this.OnTipoConteudoHttpChanging(value);
	    	              this.RaiseDataMemberChanging("TipoConteudoHttp");
	    	              this._TipoConteudoHttp = value;
	    	              this.RaiseDataMemberChanged("TipoConteudoHttp");
	    	              this.OnTipoConteudoHttpChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidDocumento
	    partial void OnUidDocumentoChanging(System.Guid value);
	    partial void OnUidDocumentoChanged();

	    private System.Guid _UidDocumento;

	    [DataMember(IsRequired = true, Name = "UidDocumento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Documento", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA.UID_DOCUMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.UID_DOCUMENTO")]
	    public System.Guid UidDocumento
	    {
	    	    get
	    	    {
	    	          return _UidDocumento;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidDocumento != value)
	    	          {
	    	              this.ValidateProperty("UidDocumento", value);
	    	              this.OnUidDocumentoChanging(value);
	    	              this.RaiseDataMemberChanging("UidDocumento");
	    	              this._UidDocumento = value;
	    	              this.RaiseDataMemberChanged("UidDocumento");
	    	              this.OnUidDocumentoChanged();
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
	    [LinxStringLength(500)]
	    [FunctionalPoint("Precision[500:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA.URL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.URL")]
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
	    //Extensibility Partial Method Definitions For XmlMapeamento
	    partial void OnXmlMapeamentoChanging(System.String value);
	    partial void OnXmlMapeamentoChanged();

	    private System.String _XmlMapeamento;

	    [DataMember(Name = "XmlMapeamento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Xml Mapeamento", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA.XML_MAPEAMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.XML_MAPEAMENTO")]
	    public System.String XmlMapeamento
	    {
	    	    get
	    	    {
	    	          return _XmlMapeamento;
	    	    }
	    	    set
	    	    {
	    	          if (this._XmlMapeamento != value)
	    	          {
	    	              this.ValidateProperty("XmlMapeamento", value);
	    	              this.OnXmlMapeamentoChanging(value);
	    	              this.RaiseDataMemberChanging("XmlMapeamento");
	    	              this._XmlMapeamento = value;
	    	              this.RaiseDataMemberChanged("XmlMapeamento");
	    	              this.OnXmlMapeamentoChanged();
	    	          }
	    	    }
	    }

	    private System.Guid _TemporaryUidDocumento;
	    [DataMember(Name = "TemporaryUidDocumento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Documento (Tmp)", Description="Temporary Key", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public System.Guid TemporaryUidDocumento
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryUidDocumento.IsNullOrEmpty())
	    	                this._TemporaryUidDocumento = this._UidDocumento;
	    	          return this._TemporaryUidDocumento;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryUidDocumento != value)
	    	              this._TemporaryUidDocumento = value;
	    	    }
	    }	

	    #endregion Data Properties

	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<DocMultimidiaTabelaChild> _DocMultimidiaTabelaChildList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_DocMultimidia_DocMultimidiaTabelaChild", "UidDocumento", "UidDocumento", IsForeignKey=false)]
	    [DataMember(Name = "DocMultimidiaTabelaChildList", EmitDefaultValue = true)]
	    public IEnumerable<DocMultimidiaTabelaChild> DocMultimidiaTabelaChildList
	    {
	        get
	        {
	
	            if (this._DocMultimidiaTabelaChildList == null)
	            	this._DocMultimidiaTabelaChildList = new List<DocMultimidiaTabelaChild>();
	
	            return this._DocMultimidiaTabelaChildList;
	        }
	        set
	        {
	            if (this._DocMultimidiaTabelaChildList != value)
	            {
	                this._DocMultimidiaTabelaChildList = value;
	                this.RaisePropertyChanged("DocMultimidiaTabelaChildList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.DOC_MULTIMIDIA").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = true, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.DOC_MULTIMIDIA), QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA.OBS", Source = "Obs", Target = "OBS", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA.URL", Source = "Url", Target = "URL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA.CONTEUDO", Source = "Conteudo", Target = "CONTEUDO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA.THUMBNAIL", Source = "Thumbnail", Target = "THUMBNAIL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA.DATA_CRIACAO", Source = "DataCriacao", Target = "DATA_CRIACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA.NOME_ARQUIVO", Source = "NomeArquivo", Target = "NOME_ARQUIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA.LX_TIPO_MIDIA", Source = "LxTipoMidia", Target = "LX_TIPO_MIDIA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA.TAMANHO_MIDIA", Source = "TamanhoMidia", Target = "TAMANHO_MIDIA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA.UID_DOCUMENTO", Source = "UidDocumento", Target = "UID_DOCUMENTO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA.DESC_DOCUMENTO", Source = "DescDocumento", Target = "DESC_DOCUMENTO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA.XML_MAPEAMENTO", Source = "XmlMapeamento", Target = "XML_MAPEAMENTO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA.LX_TIPO_EXTENSAO", Source = "LxTipoExtensao", Target = "LX_TIPO_EXTENSAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA.LX_TIPO_DOCUMENTO", Source = "LxTipoDocumento", Target = "LX_TIPO_DOCUMENTO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA.TIPO_CONTEUDO_HTTP", Source = "TipoConteudoHttp", Target = "TIPO_CONTEUDO_HTTP", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA.DOC_CLASSIFICADOR.ID_DOC_CLASSIFICADOR", Source = "IdDocClassificador", Target = "ID_DOC_CLASSIFICADOR", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.DOC_CLASSIFICADOR", RelationPropertyName = "DOC_CLASSIFICADOR" });

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

		

	[LinxPublicationView(PrimaryKeys="DOC_MULTIMIDIA_TABELA.ID_CHAVE,DOC_MULTIMIDIA_TABELA.UID_CHAVE,DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.UID_DOCUMENTO,DOC_MULTIMIDIA_TABELA.UID_TABELA", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#.DOC_MULTIMIDIA_TABELA_LISTA as #Alias#];EdmEntityName[DOC_MULTIMIDIA_TABELA];EntityRelations[DOC_MULTIMIDIA(DOC_MULTIMIDIA)#DOC_CLASSIFICADOR(DOC_CLASSIFICADOR)#DOC_MULTIMIDIA1(DOC_MULTIMIDIA)];EdmParentEntityName[DOC_MULTIMIDIA];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "DocMultimidiaTabelaChild")]
	[Serializable()]
	public partial class DocMultimidiaTabelaChild : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(MultimidiaDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("DocMultimidia");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "UidDocumento"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.UidDocumento));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load DocMultimidia
	         this.DocMultimidia = (from r in context.GetDocMultimidiaByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For IdChave
	    partial void OnIdChaveChanging(Int64 value);
	    partial void OnIdChaveChanged();

	    private Int64 _IdChave;

	    [DataMember(IsRequired = true, Name = "IdChave", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Chave", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA.ID_CHAVE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.ID_CHAVE")]
	    public Int64 IdChave
	    {
	    	    get
	    	    {
	    	          return _IdChave;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdChave != value)
	    	          {
	    	              this.ValidateProperty("IdChave", value);
	    	              this.OnIdChaveChanging(value);
	    	              this.RaiseDataMemberChanging("IdChave");
	    	              this._IdChave = value;
	    	              this.RaiseDataMemberChanged("IdChave");
	    	              this.OnIdChaveChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For OrdemApresentacao
	    partial void OnOrdemApresentacaoChanging(Int16 value);
	    partial void OnOrdemApresentacaoChanged();

	    private Int16 _OrdemApresentacao;

	    [DataMember(IsRequired = true, Name = "OrdemApresentacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ordem Apresentacao", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA.ORDEM_APRESENTACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.ORDEM_APRESENTACAO")]
	    public Int16 OrdemApresentacao
	    {
	    	    get
	    	    {
	    	          return _OrdemApresentacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._OrdemApresentacao != value)
	    	          {
	    	              this.ValidateProperty("OrdemApresentacao", value);
	    	              this.OnOrdemApresentacaoChanging(value);
	    	              this.RaiseDataMemberChanging("OrdemApresentacao");
	    	              this._OrdemApresentacao = value;
	    	              this.RaiseDataMemberChanged("OrdemApresentacao");
	    	              this.OnOrdemApresentacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidChave
	    partial void OnUidChaveChanging(System.Guid value);
	    partial void OnUidChaveChanged();

	    private System.Guid _UidChave;

	    [DataMember(IsRequired = true, Name = "UidChave", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Chave", Description="", Order = 5, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA.UID_CHAVE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.UID_CHAVE")]
	    public System.Guid UidChave
	    {
	    	    get
	    	    {
	    	          return _UidChave;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidChave != value)
	    	          {
	    	              this.ValidateProperty("UidChave", value);
	    	              this.OnUidChaveChanging(value);
	    	              this.RaiseDataMemberChanging("UidChave");
	    	              this._UidChave = value;
	    	              this.RaiseDataMemberChanged("UidChave");
	    	              this.OnUidChaveChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidDocumento
	    partial void OnUidDocumentoChanging(System.Guid value);
	    partial void OnUidDocumentoChanged();

	    private System.Guid _UidDocumento;

	    [DataMember(IsRequired = true, Name = "UidDocumento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Documento", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.UID_DOCUMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.UID_DOCUMENTO")]
	    public System.Guid UidDocumento
	    {
	    	    get
	    	    {
	    	          return _UidDocumento;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidDocumento != value)
	    	          {
	    	              this.ValidateProperty("UidDocumento", value);
	    	              this.OnUidDocumentoChanging(value);
	    	              this.RaiseDataMemberChanging("UidDocumento");
	    	              this._UidDocumento = value;
	    	              this.RaiseDataMemberChanged("UidDocumento");
	    	              this.OnUidDocumentoChanged();
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
	    [Display(Name = "Uid Tabela", Description="", Order = 5, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA.UID_TABELA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.UID_TABELA")]
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

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private DocMultimidia _DocMultimidia;
	    [DataMember(Name = "DocMultimidia", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_DocMultimidia_DocMultimidiaTabelaChild", "UidDocumento", "UidDocumento", IsForeignKey=true)]
	    public DocMultimidia DocMultimidia
	    {
	        get
	        {
	            return this._DocMultimidia;
	        }
	        set
	        {
	            if (this._DocMultimidia != value)
	            {
	                this._DocMultimidia = value;
	                this.RaisePropertyChanged("DocMultimidiaList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.DOC_MULTIMIDIA_TABELA").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = true, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.DOC_MULTIMIDIA_TABELA), QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA_TABELA" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.ID_CHAVE", Source = "IdChave", Target = "ID_CHAVE", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA_TABELA", RelationPropertyName = "DOC_MULTIMIDIA_TABELA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.UID_CHAVE", Source = "UidChave", Target = "UID_CHAVE", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA_TABELA", RelationPropertyName = "DOC_MULTIMIDIA_TABELA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.UID_TABELA", Source = "UidTabela", Target = "UID_TABELA", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA_TABELA", RelationPropertyName = "DOC_MULTIMIDIA_TABELA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.ORDEM_APRESENTACAO", Source = "OrdemApresentacao", Target = "ORDEM_APRESENTACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA_TABELA", RelationPropertyName = "DOC_MULTIMIDIA_TABELA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.UID_DOCUMENTO", Source = "UidDocumento", Target = "UID_DOCUMENTO", TargetKeyName = "UID_DOCUMENTO", NoUpdatable = false, IsKey = true, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });

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

		

	[LinxPublicationView(PrimaryKeys="DOC_MULTIMIDIA_CONFIG.ID_TCS_APLICATIVO,DOC_MULTIMIDIA_CONFIG.LX_USO_MULTIMIDIA", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[DocMultimidiaConfig];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[];EdmEntityName[DOC_MULTIMIDIA_CONFIG];EntityRelations[];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "DocMultimidiaConfig")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Multimidia.DocMultimidiaConfig")]
	public partial class DocMultimidiaConfig : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For DocAltura
	    partial void OnDocAlturaChanging(System.Nullable<System.Int32> value);
	    partial void OnDocAlturaChanged();

	    private System.Nullable<System.Int32> _DocAltura;

	    [DataMember(Name = "DocAltura", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Altura", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_CONFIG.DOC_ALTURA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_CONFIG.DOC_ALTURA")]
	    public System.Nullable<System.Int32> DocAltura
	    {
	    	    get
	    	    {
	    	          return _DocAltura;
	    	    }
	    	    set
	    	    {
	    	          if (this._DocAltura != value)
	    	          {
	    	              this.ValidateProperty("DocAltura", value);
	    	              this.OnDocAlturaChanging(value);
	    	              this.RaiseDataMemberChanging("DocAltura");
	    	              this._DocAltura = value;
	    	              this.RaiseDataMemberChanged("DocAltura");
	    	              this.OnDocAlturaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DocDuracao
	    partial void OnDocDuracaoChanging(System.String value);
	    partial void OnDocDuracaoChanged();

	    private System.String _DocDuracao;

	    [DataMember(Name = "DocDuracao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Duração", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_CONFIG.DOC_DURACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_CONFIG.DOC_DURACAO")]
	    public System.String DocDuracao
	    {
	    	    get
	    	    {
	    	          return _DocDuracao;
	    	    }
	    	    set
	    	    {
	    	          if (this._DocDuracao != value)
	    	          {
	    	              this.ValidateProperty("DocDuracao", value);
	    	              this.OnDocDuracaoChanging(value);
	    	              this.RaiseDataMemberChanging("DocDuracao");
	    	              this._DocDuracao = value;
	    	              this.RaiseDataMemberChanged("DocDuracao");
	    	              this.OnDocDuracaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DocFormatoVisualizacao
	    partial void OnDocFormatoVisualizacaoChanging(System.String value);
	    partial void OnDocFormatoVisualizacaoChanged();

	    private System.String _DocFormatoVisualizacao;

	    [DataMember(Name = "DocFormatoVisualizacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Formato da Visualização", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_CONFIG.DOC_FORMATO_VISUALIZACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_CONFIG.DOC_FORMATO_VISUALIZACAO")]
	    public System.String DocFormatoVisualizacao
	    {
	    	    get
	    	    {
	    	          return _DocFormatoVisualizacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._DocFormatoVisualizacao != value)
	    	          {
	    	              this.ValidateProperty("DocFormatoVisualizacao", value);
	    	              this.OnDocFormatoVisualizacaoChanging(value);
	    	              this.RaiseDataMemberChanging("DocFormatoVisualizacao");
	    	              this._DocFormatoVisualizacao = value;
	    	              this.RaiseDataMemberChanged("DocFormatoVisualizacao");
	    	              this.OnDocFormatoVisualizacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DocLargura
	    partial void OnDocLarguraChanging(System.Nullable<System.Int32> value);
	    partial void OnDocLarguraChanged();

	    private System.Nullable<System.Int32> _DocLargura;

	    [DataMember(Name = "DocLargura", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Largura", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_CONFIG.DOC_LARGURA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_CONFIG.DOC_LARGURA")]
	    public System.Nullable<System.Int32> DocLargura
	    {
	    	    get
	    	    {
	    	          return _DocLargura;
	    	    }
	    	    set
	    	    {
	    	          if (this._DocLargura != value)
	    	          {
	    	              this.ValidateProperty("DocLargura", value);
	    	              this.OnDocLarguraChanging(value);
	    	              this.RaiseDataMemberChanging("DocLargura");
	    	              this._DocLargura = value;
	    	              this.RaiseDataMemberChanged("DocLargura");
	    	              this.OnDocLarguraChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DocTamanho
	    partial void OnDocTamanhoChanging(System.String value);
	    partial void OnDocTamanhoChanged();

	    private System.String _DocTamanho;

	    [DataMember(Name = "DocTamanho", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tamanho", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_CONFIG.DOC_TAMANHO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_CONFIG.DOC_TAMANHO")]
	    public System.String DocTamanho
	    {
	    	    get
	    	    {
	    	          return _DocTamanho;
	    	    }
	    	    set
	    	    {
	    	          if (this._DocTamanho != value)
	    	          {
	    	              this.ValidateProperty("DocTamanho", value);
	    	              this.OnDocTamanhoChanging(value);
	    	              this.RaiseDataMemberChanging("DocTamanho");
	    	              this._DocTamanho = value;
	    	              this.RaiseDataMemberChanged("DocTamanho");
	    	              this.OnDocTamanhoChanged();
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
	    [Display(Name = "Id Tcs Aplicativo", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicativo];LookUpTitle[Seleção de (Id Tcs Aplicativo)];LookUpQuery[executeLookUpTcsAplicativo];LookUpFinalize[finalizeLookUpTcsAplicativo];LookUpDisplayColumns[{\"IdTcsAplicativo\" : \"\", \"DescricaoAplicativo\" : \"Aplicativo\"}];LookUpColumns[{\"IdTcsAplicativo\" : false, \"DescricaoAplicativo\" : true}];FilterDataKey[DOC_MULTIMIDIA_CONFIG.ID_TCS_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="int#IdTcsAplicativo#false##12:0###0#false##::LookUpTcsAplicativo##false#false###Linx.Framework.BV.Multimidia#IQueryable###true#false", EdmKey="DOC_MULTIMIDIA_CONFIG.ID_TCS_APLICATIVO")]
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
	    //Extensibility Partial Method Definitions For LxUsoMultimidia
	    partial void OnLxUsoMultimidiaChanging(Byte value);
	    partial void OnLxUsoMultimidiaChanged();

	    private Byte _LxUsoMultimidia;

	    [DataMember(IsRequired = true, Name = "LxUsoMultimidia", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo de Uso", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[UsoMultimidia];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_CONFIG.LX_USO_MULTIMIDIA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_CONFIG.LX_USO_MULTIMIDIA")]
	    public Byte LxUsoMultimidia
	    {
	    	    get
	    	    {
	    	          return _LxUsoMultimidia;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxUsoMultimidia != value)
	    	          {
	    	              this.ValidateProperty("LxUsoMultimidia", value);
	    	              this.OnLxUsoMultimidiaChanging(value);
	    	              this.RaiseDataMemberChanging("LxUsoMultimidia");
	    	              this._LxUsoMultimidia = value;
	    	              this.RaiseDataMemberChanged("LxUsoMultimidia");
	    	              this.OnLxUsoMultimidiaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescricaoAplicativo
	    partial void OnDescricaoAplicativoChanging(string value);
	    partial void OnDescricaoAplicativoChanged();

	    private string _DescricaoAplicativo;

	    [DataMember(IsRequired = true, Name = "DescricaoAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicativo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#DescricaoAplicativo#false##250:0##Aplicativo#1#true##::LookUpTcsAplicativo##false#false###Linx.Framework.BV.Multimidia#IQueryable###true#false", EdmKey="")]
	    public string DescricaoAplicativo
	    {
	    	    get
	    	    {
	    	          if (_DescricaoAplicativo != (GetDescricaoAplicativo()))
	    	             _DescricaoAplicativo =  GetDescricaoAplicativo();
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

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.DOC_MULTIMIDIA_CONFIG").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.DOC_MULTIMIDIA_CONFIG), QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA_CONFIG" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_CONFIG.DOC_ALTURA", Source = "DocAltura", Target = "DOC_ALTURA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA_CONFIG", RelationPropertyName = "DOC_MULTIMIDIA_CONFIG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_CONFIG.DOC_DURACAO", Source = "DocDuracao", Target = "DOC_DURACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA_CONFIG", RelationPropertyName = "DOC_MULTIMIDIA_CONFIG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_CONFIG.DOC_LARGURA", Source = "DocLargura", Target = "DOC_LARGURA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA_CONFIG", RelationPropertyName = "DOC_MULTIMIDIA_CONFIG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_CONFIG.DOC_TAMANHO", Source = "DocTamanho", Target = "DOC_TAMANHO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA_CONFIG", RelationPropertyName = "DOC_MULTIMIDIA_CONFIG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_CONFIG.ID_TCS_APLICATIVO", Source = "IdTcsAplicativo", Target = "ID_TCS_APLICATIVO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA_CONFIG", RelationPropertyName = "DOC_MULTIMIDIA_CONFIG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_CONFIG.LX_USO_MULTIMIDIA", Source = "LxUsoMultimidia", Target = "LX_USO_MULTIMIDIA", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA_CONFIG", RelationPropertyName = "DOC_MULTIMIDIA_CONFIG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_CONFIG.DOC_FORMATO_VISUALIZACAO", Source = "DocFormatoVisualizacao", Target = "DOC_FORMATO_VISUALIZACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA_CONFIG", RelationPropertyName = "DOC_MULTIMIDIA_CONFIG" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxUsoMultimidiaValues()
	    {
	    	    return Linx.Framework.BV.Domains.UsoMultimidia.GetValues();
	    }
	    private string _lxUsoMultimidiaName;
	    [DataMember(IsRequired = false, Name = "LxUsoMultimidiaName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo de Uso", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxUsoMultimidiaName
	    {
	    	    get { if (this.LxUsoMultimidia.IsNull()) { _lxUsoMultimidiaName = String.Empty; } else { string key = this.LxUsoMultimidia.ToString(); var dmValues = this.GetLxUsoMultimidiaValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxUsoMultimidiaName) _lxUsoMultimidiaName = domainName; } return _lxUsoMultimidiaName; } set { _lxUsoMultimidiaName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

		
	[DataContract(IsReference = false, Name = "MediaElement")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Multimidia.MediaElement")]
	public partial class MediaElement 
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
	 


	    private Guid _Id;

	    [DataMember(Name = "Id", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    public Guid Id
	    {
	    	    get
	    	    {
	    	          return _Id;
	    	    }
	    	    set
	    	    {
	    	          this._Id = value;
	    	    }
	    }

	    private string _Url;

	    [DataMember(Name = "Url", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
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

	    private byte _ExtensionType;

	    [IgnoreDataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    public byte ExtensionType
	    {
	    	    get
	    	    {
	    	          return _ExtensionType;
	    	    }
	    	    set
	    	    {
	    	          this._ExtensionType = value;
	    	    }
	    }

	    private Byte _LxTipoDocumento;

	    [DataMember(Name = "LxTipoDocumento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Lx Tipo Documento", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    public Byte LxTipoDocumento
	    {
	    	    get
	    	    {
	    	          return _LxTipoDocumento;
	    	    }
	    	    set
	    	    {
	    	          this._LxTipoDocumento = value;
	    	    }
	    }

	    private bool _KeepContent;

	    [DataMember(Name = "KeepContent", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    public bool KeepContent
	    {
	    	    get
	    	    {
	    	          return _KeepContent;
	    	    }
	    	    set
	    	    {
	    	          this._KeepContent = value;
	    	    }
	    }

	    [DataMember(Name = "Extension", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    public string Extension
	    {
	    	    get
	    	    {
	    	          return this.GetExtension();
	    	    }
	    	    internal set { }
	    }

	    [DataMember(Name = "TipoDocumento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    public string TipoDocumento
	    {
	    	    get
	    	    {
	    	          return this.GetTipoDocumento();
	    	    }
	    	    internal set { }
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

		

		
	[DataContract(IsReference = false, Name = "MediaConfigLength")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Multimidia.MediaConfigLength")]
	public partial class MediaConfigLength 
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
	 


	    private int _IdApp;

	    [DataMember(Name = "IdApp", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    public int IdApp
	    {
	    	    get
	    	    {
	    	          return _IdApp;
	    	    }
	    	    set
	    	    {
	    	          this._IdApp = value;
	    	    }
	    }

	    private int _IdUse;

	    [DataMember(Name = "IdUse", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    public int IdUse
	    {
	    	    get
	    	    {
	    	          return _IdUse;
	    	    }
	    	    set
	    	    {
	    	          this._IdUse = value;
	    	    }
	    }

	    private int _Height;

	    [DataMember(Name = "Height", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    public int Height
	    {
	    	    get
	    	    {
	    	          return _Height;
	    	    }
	    	    set
	    	    {
	    	          this._Height = value;
	    	    }
	    }

	    private int _Width;

	    [DataMember(Name = "Width", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    public int Width
	    {
	    	    get
	    	    {
	    	          return _Width;
	    	    }
	    	    set
	    	    {
	    	          this._Width = value;
	    	    }
	    }

	    [DataMember(Name = "AppName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    public string AppName
	    {
	    	    get
	    	    {
	    	          return this.GetApp();
	    	    }
	    	    internal set { }
	    }

	    [DataMember(Name = "UseName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    public string UseName
	    {
	    	    get
	    	    {
	    	          return this.GetUse();
	    	    }
	    	    internal set { }
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

		

	[LinxPublicationView(PrimaryKeys="DocMultimidiaUpload.EntityUniqueKey", IsUpdatable=false, EdmName="")]
		
	[DataContract(IsReference = false, Name = "DocMultimidiaUpload")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Multimidia.DocMultimidiaUpload")]
	public partial class DocMultimidiaUpload 
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
	 


	    private byte _TipoDocumento;

	    [DataMember(Name = "TipoDocumento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public byte TipoDocumento
	    {
	    	    get
	    	    {
	    	          return _TipoDocumento;
	    	    }
	    	    set
	    	    {
	    	          this._TipoDocumento = value;
	    	    }
	    }

	    private string _Conteudo;

	    [DataMember(Name = "Conteudo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Conteudo
	    {
	    	    get
	    	    {
	    	          return _Conteudo;
	    	    }
	    	    set
	    	    {
	    	          this._Conteudo = value;
	    	    }
	    }

	    private string _NomeArquivo;

	    [DataMember(Name = "NomeArquivo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string NomeArquivo
	    {
	    	    get
	    	    {
	    	          return _NomeArquivo;
	    	    }
	    	    set
	    	    {
	    	          this._NomeArquivo = value;
	    	    }
	    }

	    private string _TipoConteudoHttp;

	    [DataMember(Name = "TipoConteudoHttp", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string TipoConteudoHttp
	    {
	    	    get
	    	    {
	    	          return _TipoConteudoHttp;
	    	    }
	    	    set
	    	    {
	    	          this._TipoConteudoHttp = value;
	    	    }
	    }

	    private int _Tamanho;

	    [DataMember(Name = "Tamanho", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public int Tamanho
	    {
	    	    get
	    	    {
	    	          return _Tamanho;
	    	    }
	    	    set
	    	    {
	    	          this._Tamanho = value;
	    	    }
	    }

	    private string _JExpression;

	    [DataMember(Name = "JExpression", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string JExpression
	    {
	    	    get
	    	    {
	    	          return _JExpression;
	    	    }
	    	    set
	    	    {
	    	          this._JExpression = value;
	    	    }
	    }

	    private string _NomeTabela;

	    [DataMember(Name = "NomeTabela", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string NomeTabela
	    {
	    	    get
	    	    {
	    	          return _NomeTabela;
	    	    }
	    	    set
	    	    {
	    	          this._NomeTabela = value;
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

		

	[LinxPublicationView(PrimaryKeys="DocTabelaSync.EntityUniqueKey", IsUpdatable=false, EdmName="")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[DocTabelaSync];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[];EdmEntityName[];EntityRelations[ ];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "DocTabelaSync")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Multimidia.DocTabelaSync")]
	public partial class DocTabelaSync : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For NomeTabela
	    partial void OnNomeTabelaChanging(string value);
	    partial void OnNomeTabelaChanged();

	    private string _NomeTabela;

	    [DataMember(IsRequired = true, Name = "NomeTabela", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string NomeTabela
	    {
	    	    get
	    	    {
	    	          if (_NomeTabela.IsNullOrEmpty())
	    	             _NomeTabela =  String.Empty;
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
	    //Extensibility Partial Method Definitions For IdChave
	    partial void OnIdChaveChanging(Int64? value);
	    partial void OnIdChaveChanged();

	    private Int64? _IdChave;

	    [DataMember(IsRequired = true, Name = "IdChave", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public Int64? IdChave
	    {
	    	    get
	    	    {
	    	          return _IdChave;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdChave != value)
	    	          {
	    	              this.ValidateProperty("IdChave", value);
	    	              this.OnIdChaveChanging(value);
	    	              this.RaiseDataMemberChanging("IdChave");
	    	              this._IdChave = value;
	    	              this.RaiseDataMemberChanged("IdChave");
	    	              this.OnIdChaveChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidChave
	    partial void OnUidChaveChanging(Guid? value);
	    partial void OnUidChaveChanged();

	    private Guid? _UidChave;

	    [DataMember(IsRequired = true, Name = "UidChave", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public Guid? UidChave
	    {
	    	    get
	    	    {
	    	          return _UidChave;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidChave != value)
	    	          {
	    	              this.ValidateProperty("UidChave", value);
	    	              this.OnUidChaveChanging(value);
	    	              this.RaiseDataMemberChanging("UidChave");
	    	              this._UidChave = value;
	    	              this.RaiseDataMemberChanged("UidChave");
	    	              this.OnUidChaveChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Midias
	    partial void OnMidiasChanging(List<Guid> value);
	    partial void OnMidiasChanged();

	    private List<Guid> _Midias;

	    [DataMember(IsRequired = true, Name = "Midias", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public List<Guid> Midias
	    {
	    	    get
	    	    {
	    	          return _Midias;
	    	    }
	    	    set
	    	    {
	    	          if (this._Midias != value)
	    	          {
	    	              this.ValidateProperty("Midias", value);
	    	              this.OnMidiasChanging(value);
	    	              this.RaiseDataMemberChanging("Midias");
	    	              this._Midias = value;
	    	              this.RaiseDataMemberChanged("Midias");
	    	              this.OnMidiasChanged();
	    	          }
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

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#.DOC_MULTIMIDIA_TABELA_LISTA as #Alias#];EdmEntityName[DOC_MULTIMIDIA_TABELA];EntityRelations[DOC_MULTIMIDIA(DOC_MULTIMIDIA)#DOC_CLASSIFICADOR(DOC_CLASSIFICADOR)#DOC_MULTIMIDIA1(DOC_MULTIMIDIA)];EdmParentEntityName[DOC_MULTIMIDIA];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "DocMultimidiaTabelaChild")]
	[Serializable()]
	public partial class DocMultimidiaTabelaChildParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For IdChave
	    partial void OnIdChaveChanging(Int64 value);
	    partial void OnIdChaveChanged();

	    private Int64 _IdChave;

	    [DataMember(IsRequired = true, Name = "IdChave", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Chave", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA.ID_CHAVE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.ID_CHAVE")]
	    public Int64 IdChave
	    {
	    	    get
	    	    {
	    	          return _IdChave;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdChave != value)
	    	          {
	    	              this.ValidateProperty("IdChave", value);
	    	              this.OnIdChaveChanging(value);
	    	              this.RaiseDataMemberChanging("IdChave");
	    	              this._IdChave = value;
	    	              this.RaiseDataMemberChanged("IdChave");
	    	              this.OnIdChaveChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For OrdemApresentacao
	    partial void OnOrdemApresentacaoChanging(Int16 value);
	    partial void OnOrdemApresentacaoChanged();

	    private Int16 _OrdemApresentacao;

	    [DataMember(IsRequired = true, Name = "OrdemApresentacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ordem Apresentacao", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA.ORDEM_APRESENTACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.ORDEM_APRESENTACAO")]
	    public Int16 OrdemApresentacao
	    {
	    	    get
	    	    {
	    	          return _OrdemApresentacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._OrdemApresentacao != value)
	    	          {
	    	              this.ValidateProperty("OrdemApresentacao", value);
	    	              this.OnOrdemApresentacaoChanging(value);
	    	              this.RaiseDataMemberChanging("OrdemApresentacao");
	    	              this._OrdemApresentacao = value;
	    	              this.RaiseDataMemberChanged("OrdemApresentacao");
	    	              this.OnOrdemApresentacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidChave
	    partial void OnUidChaveChanging(System.Guid value);
	    partial void OnUidChaveChanged();

	    private System.Guid _UidChave;

	    [DataMember(IsRequired = true, Name = "UidChave", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Chave", Description="", Order = 5, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA.UID_CHAVE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.UID_CHAVE")]
	    public System.Guid UidChave
	    {
	    	    get
	    	    {
	    	          return _UidChave;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidChave != value)
	    	          {
	    	              this.ValidateProperty("UidChave", value);
	    	              this.OnUidChaveChanging(value);
	    	              this.RaiseDataMemberChanging("UidChave");
	    	              this._UidChave = value;
	    	              this.RaiseDataMemberChanged("UidChave");
	    	              this.OnUidChaveChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidDocumento
	    partial void OnUidDocumentoChanging(System.Guid value);
	    partial void OnUidDocumentoChanged();

	    private System.Guid _UidDocumento;

	    [DataMember(IsRequired = true, Name = "UidDocumento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Documento", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.UID_DOCUMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.UID_DOCUMENTO")]
	    public System.Guid UidDocumento
	    {
	    	    get
	    	    {
	    	          return _UidDocumento;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidDocumento != value)
	    	          {
	    	              this.ValidateProperty("UidDocumento", value);
	    	              this.OnUidDocumentoChanging(value);
	    	              this.RaiseDataMemberChanging("UidDocumento");
	    	              this._UidDocumento = value;
	    	              this.RaiseDataMemberChanged("UidDocumento");
	    	              this.OnUidDocumentoChanged();
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
	    [Display(Name = "Uid Tabela", Description="", Order = 5, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA.UID_TABELA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.UID_TABELA")]
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
	    //Extensibility Partial Method Definitions For Conteudo
	    partial void OnConteudoChanging(Byte[] value);
	    partial void OnConteudoChanged();

	    private Byte[] _Conteudo;

	    [DataMember(Name = "Conteudo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Conteudo", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.CONTEUDO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.CONTEUDO")]
	    public Byte[] Conteudo
	    {
	    	    get
	    	    {
	    	          return _Conteudo;
	    	    }
	    	    set
	    	    {
	    	          if (this._Conteudo != value)
	    	          {
	    	              this.ValidateProperty("Conteudo", value);
	    	              this.OnConteudoChanging(value);
	    	              this.RaiseDataMemberChanging("Conteudo");
	    	              this._Conteudo = value;
	    	              this.RaiseDataMemberChanged("Conteudo");
	    	              this.OnConteudoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DataCriacao
	    partial void OnDataCriacaoChanging(System.DateTime value);
	    partial void OnDataCriacaoChanged();

	    private System.DateTime _DataCriacao;

	    [DataMember(IsRequired = true, Name = "DataCriacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Data Criacao", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.DATA_CRIACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.DATA_CRIACAO")]
	    public System.DateTime DataCriacao
	    {
	    	    get
	    	    {
	    	          return _DataCriacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataCriacao != value)
	    	          {
	    	              this.ValidateProperty("DataCriacao", value);
	    	              this.OnDataCriacaoChanging(value);
	    	              this.RaiseDataMemberChanging("DataCriacao");
	    	              this._DataCriacao = value;
	    	              this.RaiseDataMemberChanged("DataCriacao");
	    	              this.OnDataCriacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescDocClassificador
	    partial void OnDescDocClassificadorChanging(System.String value);
	    partial void OnDescDocClassificadorChanged();

	    private System.String _DescDocClassificador;

	    [DataMember(IsRequired = true, Name = "DescDocClassificador", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Desc Doc Classificador", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.DOC_CLASSIFICADOR.DESC_DOC_CLASSIFICADOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.DOC_CLASSIFICADOR.DESC_DOC_CLASSIFICADOR")]
	    public System.String DescDocClassificador
	    {
	    	    get
	    	    {
	    	          return _DescDocClassificador;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescDocClassificador != value)
	    	          {
	    	              this.ValidateProperty("DescDocClassificador", value);
	    	              this.OnDescDocClassificadorChanging(value);
	    	              this.RaiseDataMemberChanging("DescDocClassificador");
	    	              this._DescDocClassificador = value;
	    	              this.RaiseDataMemberChanged("DescDocClassificador");
	    	              this.OnDescDocClassificadorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescDocumento
	    partial void OnDescDocumentoChanging(System.String value);
	    partial void OnDescDocumentoChanged();

	    private System.String _DescDocumento;

	    [DataMember(IsRequired = true, Name = "DescDocumento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Desc Documento", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.DESC_DOCUMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.DESC_DOCUMENTO")]
	    public System.String DescDocumento
	    {
	    	    get
	    	    {
	    	          return _DescDocumento;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescDocumento != value)
	    	          {
	    	              this.ValidateProperty("DescDocumento", value);
	    	              this.OnDescDocumentoChanging(value);
	    	              this.RaiseDataMemberChanging("DescDocumento");
	    	              this._DescDocumento = value;
	    	              this.RaiseDataMemberChanged("DescDocumento");
	    	              this.OnDescDocumentoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdDocClassificador
	    partial void OnIdDocClassificadorChanging(Int64 value);
	    partial void OnIdDocClassificadorChanged();

	    private Int64 _IdDocClassificador;

	    [DataMember(IsRequired = true, Name = "IdDocClassificador", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Doc Classificador", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.DOC_CLASSIFICADOR.ID_DOC_CLASSIFICADOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.DOC_CLASSIFICADOR.ID_DOC_CLASSIFICADOR")]
	    public Int64 IdDocClassificador
	    {
	    	    get
	    	    {
	    	          return _IdDocClassificador;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdDocClassificador != value)
	    	          {
	    	              this.ValidateProperty("IdDocClassificador", value);
	    	              this.OnIdDocClassificadorChanging(value);
	    	              this.RaiseDataMemberChanging("IdDocClassificador");
	    	              this._IdDocClassificador = value;
	    	              this.RaiseDataMemberChanged("IdDocClassificador");
	    	              this.OnIdDocClassificadorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoDocumento
	    partial void OnLxTipoDocumentoChanging(Byte value);
	    partial void OnLxTipoDocumentoChanged();

	    private Byte _LxTipoDocumento;

	    [DataMember(IsRequired = true, Name = "LxTipoDocumento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Lx Tipo Documento", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.LX_TIPO_DOCUMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.LX_TIPO_DOCUMENTO")]
	    public Byte LxTipoDocumento
	    {
	    	    get
	    	    {
	    	          return _LxTipoDocumento;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoDocumento != value)
	    	          {
	    	              this.ValidateProperty("LxTipoDocumento", value);
	    	              this.OnLxTipoDocumentoChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoDocumento");
	    	              this._LxTipoDocumento = value;
	    	              this.RaiseDataMemberChanged("LxTipoDocumento");
	    	              this.OnLxTipoDocumentoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoExtensao
	    partial void OnLxTipoExtensaoChanging(Byte value);
	    partial void OnLxTipoExtensaoChanged();

	    private Byte _LxTipoExtensao;

	    [DataMember(IsRequired = true, Name = "LxTipoExtensao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Lx Tipo Extensao", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.LX_TIPO_EXTENSAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.LX_TIPO_EXTENSAO")]
	    public Byte LxTipoExtensao
	    {
	    	    get
	    	    {
	    	          return _LxTipoExtensao;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoExtensao != value)
	    	          {
	    	              this.ValidateProperty("LxTipoExtensao", value);
	    	              this.OnLxTipoExtensaoChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoExtensao");
	    	              this._LxTipoExtensao = value;
	    	              this.RaiseDataMemberChanged("LxTipoExtensao");
	    	              this.OnLxTipoExtensaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoMidia
	    partial void OnLxTipoMidiaChanging(Byte value);
	    partial void OnLxTipoMidiaChanged();

	    private Byte _LxTipoMidia;

	    [DataMember(IsRequired = true, Name = "LxTipoMidia", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Lx Tipo Midia", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.LX_TIPO_MIDIA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.LX_TIPO_MIDIA")]
	    public Byte LxTipoMidia
	    {
	    	    get
	    	    {
	    	          return _LxTipoMidia;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoMidia != value)
	    	          {
	    	              this.ValidateProperty("LxTipoMidia", value);
	    	              this.OnLxTipoMidiaChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoMidia");
	    	              this._LxTipoMidia = value;
	    	              this.RaiseDataMemberChanged("LxTipoMidia");
	    	              this.OnLxTipoMidiaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeArquivo
	    partial void OnNomeArquivoChanging(System.String value);
	    partial void OnNomeArquivoChanged();

	    private System.String _NomeArquivo;

	    [DataMember(Name = "NomeArquivo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Arquivo", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(150)]
	    [FunctionalPoint("Precision[150:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.NOME_ARQUIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.NOME_ARQUIVO")]
	    public System.String NomeArquivo
	    {
	    	    get
	    	    {
	    	          return _NomeArquivo;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeArquivo != value)
	    	          {
	    	              this.ValidateProperty("NomeArquivo", value);
	    	              this.OnNomeArquivoChanging(value);
	    	              this.RaiseDataMemberChanging("NomeArquivo");
	    	              this._NomeArquivo = value;
	    	              this.RaiseDataMemberChanged("NomeArquivo");
	    	              this.OnNomeArquivoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Obs
	    partial void OnObsChanging(System.String value);
	    partial void OnObsChanged();

	    private System.String _Obs;

	    [DataMember(Name = "Obs", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Obs", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.OBS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.OBS")]
	    public System.String Obs
	    {
	    	    get
	    	    {
	    	          return _Obs;
	    	    }
	    	    set
	    	    {
	    	          if (this._Obs != value)
	    	          {
	    	              this.ValidateProperty("Obs", value);
	    	              this.OnObsChanging(value);
	    	              this.RaiseDataMemberChanging("Obs");
	    	              this._Obs = value;
	    	              this.RaiseDataMemberChanged("Obs");
	    	              this.OnObsChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For TamanhoMidia
	    partial void OnTamanhoMidiaChanging(System.Nullable<System.Int32> value);
	    partial void OnTamanhoMidiaChanged();

	    private System.Nullable<System.Int32> _TamanhoMidia;

	    [DataMember(Name = "TamanhoMidia", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tamanho Midia", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.TAMANHO_MIDIA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.TAMANHO_MIDIA")]
	    public System.Nullable<System.Int32> TamanhoMidia
	    {
	    	    get
	    	    {
	    	          return _TamanhoMidia;
	    	    }
	    	    set
	    	    {
	    	          if (this._TamanhoMidia != value)
	    	          {
	    	              this.ValidateProperty("TamanhoMidia", value);
	    	              this.OnTamanhoMidiaChanging(value);
	    	              this.RaiseDataMemberChanging("TamanhoMidia");
	    	              this._TamanhoMidia = value;
	    	              this.RaiseDataMemberChanged("TamanhoMidia");
	    	              this.OnTamanhoMidiaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Thumbnail
	    partial void OnThumbnailChanging(Byte[] value);
	    partial void OnThumbnailChanged();

	    private Byte[] _Thumbnail;

	    [DataMember(Name = "Thumbnail", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Thumbnail", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.THUMBNAIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.THUMBNAIL")]
	    public Byte[] Thumbnail
	    {
	    	    get
	    	    {
	    	          return _Thumbnail;
	    	    }
	    	    set
	    	    {
	    	          if (this._Thumbnail != value)
	    	          {
	    	              this.ValidateProperty("Thumbnail", value);
	    	              this.OnThumbnailChanging(value);
	    	              this.RaiseDataMemberChanging("Thumbnail");
	    	              this._Thumbnail = value;
	    	              this.RaiseDataMemberChanged("Thumbnail");
	    	              this.OnThumbnailChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For TipoConteudoHttp
	    partial void OnTipoConteudoHttpChanging(System.String value);
	    partial void OnTipoConteudoHttpChanged();

	    private System.String _TipoConteudoHttp;

	    [DataMember(Name = "TipoConteudoHttp", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo Conteudo Http", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(100)]
	    [FunctionalPoint("Precision[100:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.TIPO_CONTEUDO_HTTP];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.TIPO_CONTEUDO_HTTP")]
	    public System.String TipoConteudoHttp
	    {
	    	    get
	    	    {
	    	          return _TipoConteudoHttp;
	    	    }
	    	    set
	    	    {
	    	          if (this._TipoConteudoHttp != value)
	    	          {
	    	              this.ValidateProperty("TipoConteudoHttp", value);
	    	              this.OnTipoConteudoHttpChanging(value);
	    	              this.RaiseDataMemberChanging("TipoConteudoHttp");
	    	              this._TipoConteudoHttp = value;
	    	              this.RaiseDataMemberChanged("TipoConteudoHttp");
	    	              this.OnTipoConteudoHttpChanged();
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
	    [LinxStringLength(500)]
	    [FunctionalPoint("Precision[500:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.URL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.URL")]
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
	    //Extensibility Partial Method Definitions For XmlMapeamento
	    partial void OnXmlMapeamentoChanging(System.String value);
	    partial void OnXmlMapeamentoChanged();

	    private System.String _XmlMapeamento;

	    [DataMember(Name = "XmlMapeamento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Xml Mapeamento", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.XML_MAPEAMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.XML_MAPEAMENTO")]
	    public System.String XmlMapeamento
	    {
	    	    get
	    	    {
	    	          return _XmlMapeamento;
	    	    }
	    	    set
	    	    {
	    	          if (this._XmlMapeamento != value)
	    	          {
	    	              this.ValidateProperty("XmlMapeamento", value);
	    	              this.OnXmlMapeamentoChanging(value);
	    	              this.RaiseDataMemberChanging("XmlMapeamento");
	    	              this._XmlMapeamento = value;
	    	              this.RaiseDataMemberChanged("XmlMapeamento");
	    	              this.OnXmlMapeamentoChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.DOC_MULTIMIDIA_TABELA").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = true, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.DOC_MULTIMIDIA_TABELA), QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA_TABELA" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.ID_CHAVE", Source = "IdChave", Target = "ID_CHAVE", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA_TABELA", RelationPropertyName = "DOC_MULTIMIDIA_TABELA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.UID_CHAVE", Source = "UidChave", Target = "UID_CHAVE", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA_TABELA", RelationPropertyName = "DOC_MULTIMIDIA_TABELA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.UID_TABELA", Source = "UidTabela", Target = "UID_TABELA", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA_TABELA", RelationPropertyName = "DOC_MULTIMIDIA_TABELA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.ORDEM_APRESENTACAO", Source = "OrdemApresentacao", Target = "ORDEM_APRESENTACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA_TABELA", RelationPropertyName = "DOC_MULTIMIDIA_TABELA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.UID_DOCUMENTO", Source = "UidDocumento", Target = "UID_DOCUMENTO", TargetKeyName = "UID_DOCUMENTO", NoUpdatable = false, IsKey = true, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.DOC_MULTIMIDIA", RelationPropertyName = "DOC_MULTIMIDIA" });

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
	[DomainIdentifier("ProcessorOverviewMultimidiaDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class MultimidiaDomainService : DomainService, IDataServiceContext 
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

		
	    public MultimidiaDomainService() : this("", null, null) { }
	    public MultimidiaDomainService(string connectionString) : this(connectionString, null, null) { }
	    public MultimidiaDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public MultimidiaDomainService(Linx.Framework.ControleSistema.BM.ControleSistemaContext dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public MultimidiaDomainService(string connectionString, Linx.Framework.ControleSistema.BM.ControleSistemaContext dataContext, Dictionary<string, string> headers) : base() 
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
	
	    
	        foreach (ChangeSetEntry entry in changeSet.ChangeSetEntries.Where(e => e.Entity is MultimidiaCompact2BO))
	        {
	            ((MultimidiaCompact2BO)entry.Entity).OnSavingChanges(this, changeSet.GetChangeOperation(entry.Entity));
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
	
	    
	        foreach (ChangeSetEntry entry in changeSet.ChangeSetEntries.Where(e => e.Entity is MultimidiaCompact2BO))
	        {
	            ((MultimidiaCompact2BO)entry.Entity).OnSavedChanges(this, changeSet.GetChangeOperation(entry.Entity));
	        }
    	
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
 	        var _DocMultimidiaElements = changeSet.ChangeSetEntries.Where(e => e.Entity is DocMultimidia && e.Entity.GetType().Name == "DocMultimidia" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _DocMultimidiaElements)
 	           if (((DocMultimidia)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 
 	        //Remove inconsistent details
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is DocMultimidiaTabelaChild && e.Entity.GetType().Name == "DocMultimidiaTabelaChild" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
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
	    //Get All LookUpDocMultimidia.
	    public IQueryable<LookUpDocMultimidia> GetAllLookUpDocMultimidia()
	    {
	        return this.GetLookUpDocMultimidia(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpDocMultimidia By EntitySearch.
	    public IQueryable<LookUpDocMultimidia> GetLookUpDocMultimidiaByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpDocMultimidia(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpDocMultimidia.
	    public IQueryable<LookUpDocMultimidia> GetLookUpDocMultimidia(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "DOC_MULTIMIDIA" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpDocMultimidia";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpDocMultimidia));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpDocMultimidia> query =  
	
	            (from entity in this.DbContext.DOC_MULTIMIDIA.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.DOC_CLASSIFICADOR
	            
	            select new LookUpDocMultimidia()		
	            {
	            
                Conteudo = entity.CONTEUDO
                , DescDocClassificador = entityAl1.DESC_DOC_CLASSIFICADOR
                , DescDocumento = entity.DESC_DOCUMENTO
                , IdDocClassificador = entityAl1.ID_DOC_CLASSIFICADOR
                , LxTipoDocumento = entity.LX_TIPO_DOCUMENTO
                , LxTipoExtensao = entity.LX_TIPO_EXTENSAO
                , Obs = entity.OBS
                , Thumbnail = entity.THUMBNAIL
                , UidDocumento = entity.UID_DOCUMENTO
                , Url = entity.URL
                , XmlMapeamento = entity.XML_MAPEAMENTO
	            });

	            
            //Inner Group Definition
            if (propertyName.InList("DescDocClassificador", "IdDocClassificador"))
            {
               query = (from r in query select new LookUpDocMultimidia() {
               Conteudo = default(Byte[])
               , DescDocClassificador = r.DescDocClassificador
               , DescDocumento = ""
               , IdDocClassificador = r.IdDocClassificador
               , LxTipoDocumento = default(Byte)
               , LxTipoExtensao = default(Byte)
               , Obs = ""
               , Thumbnail = default(Byte[])
               , UidDocumento = default(System.Guid)
               , Url = ""
               , XmlMapeamento = ""
                }).Distinct();
            }
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpDocMultimidiaCompact.
	    public IQueryable<LookUpDocMultimidiaCompact> GetAllLookUpDocMultimidiaCompact()
	    {
	        return this.GetLookUpDocMultimidiaCompact(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpDocMultimidiaCompact By EntitySearch.
	    public IQueryable<LookUpDocMultimidiaCompact> GetLookUpDocMultimidiaCompactByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpDocMultimidiaCompact(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpDocMultimidiaCompact.
	    public IQueryable<LookUpDocMultimidiaCompact> GetLookUpDocMultimidiaCompact(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "DOC_MULTIMIDIA" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpDocMultimidiaCompact";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpDocMultimidiaCompact));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpDocMultimidiaCompact> query =  
	
	            (from entity in this.DbContext.DOC_MULTIMIDIA.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpDocMultimidiaCompact()		
	            {
	            
                Conteudo = entity.CONTEUDO
                , DescDocumento = entity.DESC_DOCUMENTO
                , Thumbnail = entity.THUMBNAIL
                , UidDocumento = entity.UID_DOCUMENTO
                , Url = entity.URL
	            });

	            
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpDocMultimidiaCompact2.
	    public IQueryable<LookUpDocMultimidiaCompact2> GetAllLookUpDocMultimidiaCompact2()
	    {
	        return this.GetLookUpDocMultimidiaCompact2(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpDocMultimidiaCompact2 By EntitySearch.
	    public IQueryable<LookUpDocMultimidiaCompact2> GetLookUpDocMultimidiaCompact2ByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpDocMultimidiaCompact2(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpDocMultimidiaCompact2.
	    public IQueryable<LookUpDocMultimidiaCompact2> GetLookUpDocMultimidiaCompact2(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "DOC_MULTIMIDIA" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpDocMultimidiaCompact2";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpDocMultimidiaCompact2));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpDocMultimidiaCompact2> query =  
	
	            (from entity in this.DbContext.DOC_MULTIMIDIA.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.DOC_CLASSIFICADOR
	            
	            select new LookUpDocMultimidiaCompact2()		
	            {
	            
                Conteudo = entity.CONTEUDO
                , DescDocumento = entity.DESC_DOCUMENTO
                , IdDocClassificador = entityAl1.ID_DOC_CLASSIFICADOR
                , LxTipoDocumento = entity.LX_TIPO_DOCUMENTO
                , LxTipoExtensao = entity.LX_TIPO_EXTENSAO
                , Obs = entity.OBS
                , Thumbnail = entity.THUMBNAIL
                , UidDocumento = entity.UID_DOCUMENTO
                , Url = entity.URL
                , XmlMapeamento = entity.XML_MAPEAMENTO
	            });

	            
            //Inner Group Definition
            if (propertyName.InList("IdDocClassificador"))
            {
               query = (from r in query select new LookUpDocMultimidiaCompact2() {
               Conteudo = default(Byte[])
               , DescDocumento = ""
               , IdDocClassificador = r.IdDocClassificador
               , LxTipoDocumento = default(Byte)
               , LxTipoExtensao = default(Byte)
               , Obs = ""
               , Thumbnail = default(Byte[])
               , UidDocumento = default(System.Guid)
               , Url = ""
               , XmlMapeamento = ""
                }).Distinct();
            }
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpDocClassificador.
	    public IQueryable<LookUpDocClassificador> GetAllLookUpDocClassificador()
	    {
	        return this.GetLookUpDocClassificador(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpDocClassificador By EntitySearch.
	    public IQueryable<LookUpDocClassificador> GetLookUpDocClassificadorByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpDocClassificador(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpDocClassificador.
	    public IQueryable<LookUpDocClassificador> GetLookUpDocClassificador(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "DOC_CLASSIFICADOR" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpDocClassificador";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpDocClassificador));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpDocClassificador> query =  
	
	            (from entity in this.DbContext.DOC_CLASSIFICADOR.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpDocClassificador()		
	            {
	            
                IdDocClassificador = entity.ID_DOC_CLASSIFICADOR
	            });

	            
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpDocClassificador1.
	    public IQueryable<LookUpDocClassificador1> GetAllLookUpDocClassificador1()
	    {
	        return this.GetLookUpDocClassificador1(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpDocClassificador1 By EntitySearch.
	    public IQueryable<LookUpDocClassificador1> GetLookUpDocClassificador1ByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpDocClassificador1(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpDocClassificador1.
	    public IQueryable<LookUpDocClassificador1> GetLookUpDocClassificador1(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "DOC_CLASSIFICADOR" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpDocClassificador1";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpDocClassificador1));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpDocClassificador1> query =  
	
	            (from entity in this.DbContext.DOC_CLASSIFICADOR.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpDocClassificador1()		
	            {
	            
                DescDocClassificador = entity.DESC_DOC_CLASSIFICADOR
                , IdDocClassificador = entity.ID_DOC_CLASSIFICADOR
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
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
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

	        IQueryable<LookUpTcsAplicativo> query =  null;
		
			
		
	        DocMultimidiaConfig.OnLookingUpLookUpTcsAplicativo(ref query, propertyName, entitySearch);
	
	
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
	
		

	        if (entityName.InList("Linx.Framework.BV.Multimidia.DocMultimidiaTabela"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "DocMultimidiaTabela",
	        			NameSpace = "Linx.Framework.BV.Multimidia",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "DocMultimidiaTabela",
	        			ClearMethodName = "ClearDocMultimidiaTabela",
	        			QueryMethodName  = "GetPagedDocMultimidiaTabela",	
	        			CountingMethodName  = "GetDocMultimidiaTabela" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Multimidia.DocMultimidiaTabela"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Multimidia.DocMultimidiaTabela"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Multimidia.DocMultimidiaCompact"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "DocMultimidiaCompact",
	        			NameSpace = "Linx.Framework.BV.Multimidia",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "DocMultimidiaCompact",
	        			ClearMethodName = "ClearDocMultimidiaCompact",
	        			QueryMethodName  = "GetPagedDocMultimidiaCompact",	
	        			CountingMethodName  = "GetDocMultimidiaCompact" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Multimidia.DocMultimidiaCompact"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Multimidia.DocMultimidiaCompact"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Multimidia.MultimidiaCompact2BO"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "MultimidiaCompact2BO",
	        			NameSpace = "Linx.Framework.BV.Multimidia",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "MultimidiaCompact2BO",
	        			ClearMethodName = "ClearMultimidiaCompact2BO",
	        			QueryMethodName  = "GetPagedMultimidiaCompact2BO",	
	        			CountingMethodName  = "GetMultimidiaCompact2BO" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Multimidia.MultimidiaCompact2BO"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Multimidia.MultimidiaCompact2BO"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Multimidia.DocMultimidiaUid"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "DocMultimidiaUid",
	        			NameSpace = "Linx.Framework.BV.Multimidia",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "DocMultimidiaUid",
	        			ClearMethodName = "ClearDocMultimidiaUid",
	        			QueryMethodName  = "GetPagedDocMultimidiaUid",	
	        			CountingMethodName  = "GetDocMultimidiaUid" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Multimidia.DocMultimidiaUid"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Multimidia.DocMultimidiaUid"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Multimidia.DocMultimidiaInfo"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "DocMultimidiaInfo",
	        			NameSpace = "Linx.Framework.BV.Multimidia",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "DocMultimidiaInfo",
	        			ClearMethodName = "ClearDocMultimidiaInfo",
	        			QueryMethodName  = "GetPagedDocMultimidiaInfo",	
	        			CountingMethodName  = "GetDocMultimidiaInfo" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Multimidia.DocMultimidiaInfo"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Multimidia.DocMultimidiaInfo"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Multimidia.DocMultimidia"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "DocMultimidia",
	        			NameSpace = "Linx.Framework.BV.Multimidia",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "DocMultimidia",
	        			ClearMethodName = "ClearDocMultimidia",
	        			QueryMethodName  = "GetPagedDocMultimidia",	
	        			CountingMethodName  = "GetDocMultimidia" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Multimidia.DocMultimidia"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Multimidia.DocMultimidia"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Multimidia.DocMultimidia", "Linx.Framework.BV.Multimidia.DocMultimidiaTabelaChild"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "DocMultimidiaTabelaChild" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.Multimidia",
	        			HasQuickSearch = false,
	        			ParentClassName = "DocMultimidia",	
	        			DisplayName = "DocMultimidiaTabelaChild",
	        			ClearMethodName = "ClearDocMultimidiaTabelaChild" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedDocMultimidiaTabelaChild" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetDocMultimidiaTabelaChild" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Multimidia.DocMultimidiaTabelaChild"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Multimidia.DocMultimidiaTabelaChild" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Multimidia.DocMultimidiaConfig"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "DocMultimidiaConfig",
	        			NameSpace = "Linx.Framework.BV.Multimidia",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "DocMultimidiaConfig",
	        			ClearMethodName = "ClearDocMultimidiaConfig",
	        			QueryMethodName  = "GetPagedDocMultimidiaConfig",	
	        			CountingMethodName  = "GetDocMultimidiaConfig" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Multimidia.DocMultimidiaConfig"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Multimidia.DocMultimidiaConfig"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Multimidia.MediaElement"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "MediaElement",
	        			NameSpace = "Linx.Framework.BV.Multimidia",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "MediaElement",
	        			ClearMethodName = "ClearMediaElement",
	        			QueryMethodName  = "GetPagedMediaElement",	
	        			CountingMethodName  = "GetMediaElement" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Multimidia.MediaElement"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Multimidia.MediaElement"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Multimidia.MediaConfigLength"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "MediaConfigLength",
	        			NameSpace = "Linx.Framework.BV.Multimidia",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "MediaConfigLength",
	        			ClearMethodName = "ClearMediaConfigLength",
	        			QueryMethodName  = "GetPagedMediaConfigLength",	
	        			CountingMethodName  = "GetMediaConfigLength" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Multimidia.MediaConfigLength"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Multimidia.MediaConfigLength"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Multimidia.DocMultimidiaUpload"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "DocMultimidiaUpload",
	        			NameSpace = "Linx.Framework.BV.Multimidia",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "DocMultimidiaUpload",
	        			ClearMethodName = "ClearDocMultimidiaUpload",
	        			QueryMethodName  = "GetPagedDocMultimidiaUpload",	
	        			CountingMethodName  = "GetDocMultimidiaUpload" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Multimidia.DocMultimidiaUpload"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Multimidia.DocMultimidiaUpload"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Multimidia.DocTabelaSync"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "DocTabelaSync",
	        			NameSpace = "Linx.Framework.BV.Multimidia",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "DocTabelaSync",
	        			ClearMethodName = "ClearDocTabelaSync",
	        			QueryMethodName  = "GetPagedDocTabelaSync",	
	        			CountingMethodName  = "GetDocTabelaSync" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Multimidia.DocTabelaSync"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Multimidia.DocTabelaSync"), forceAll: forceAll)
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

         		    return new string[] { "Framework_MultimidiaClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.MultimidiaClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Framework_multimidiaService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.multimidiaService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear DocMultimidiaTabela.
	    public IEnumerable<DocMultimidiaTabela> ClearDocMultimidiaTabela()
	    {
	        List<DocMultimidiaTabela> result = new List<DocMultimidiaTabela>();
	        result.Add(new DocMultimidiaTabela());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear DocMultimidiaCompact.
	    public IEnumerable<DocMultimidiaCompact> ClearDocMultimidiaCompact()
	    {
	        List<DocMultimidiaCompact> result = new List<DocMultimidiaCompact>();
	        result.Add(new DocMultimidiaCompact());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear MultimidiaCompact2BO.
	    public IEnumerable<MultimidiaCompact2BO> ClearMultimidiaCompact2BO()
	    {
	        List<MultimidiaCompact2BO> result = new List<MultimidiaCompact2BO>();
	        result.Add(new MultimidiaCompact2BO());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear DocMultimidiaUid.
	    public IEnumerable<DocMultimidiaUid> ClearDocMultimidiaUid()
	    {
	        List<DocMultimidiaUid> result = new List<DocMultimidiaUid>();
	        result.Add(new DocMultimidiaUid());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear DocMultimidiaInfo.
	    public IEnumerable<DocMultimidiaInfo> ClearDocMultimidiaInfo()
	    {
	        List<DocMultimidiaInfo> result = new List<DocMultimidiaInfo>();
	        result.Add(new DocMultimidiaInfo());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear DocMultimidia.
	    public IEnumerable<DocMultimidia> ClearDocMultimidia()
	    {
	        List<DocMultimidia> result = new List<DocMultimidia>();
	        result.Add(new DocMultimidia());	
			
	        result[0].DocMultimidiaTabelaChildList = new List<DocMultimidiaTabelaChild>();
	        ((List<DocMultimidiaTabelaChild>)result[0].DocMultimidiaTabelaChildList).Add(new DocMultimidiaTabelaChild());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear DocMultimidiaTabelaChild.
	    public IEnumerable<DocMultimidiaTabelaChild> ClearDocMultimidiaTabelaChild()
	    {
	        List<DocMultimidiaTabelaChild> result = new List<DocMultimidiaTabelaChild>();
	        result.Add(new DocMultimidiaTabelaChild());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear DocMultimidiaConfig.
	    public IEnumerable<DocMultimidiaConfig> ClearDocMultimidiaConfig()
	    {
	        List<DocMultimidiaConfig> result = new List<DocMultimidiaConfig>();
	        result.Add(new DocMultimidiaConfig());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear MediaElement.
	    public IEnumerable<MediaElement> ClearMediaElement()
	    {
	        List<MediaElement> result = new List<MediaElement>();
	        result.Add(new MediaElement());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear MediaConfigLength.
	    public IEnumerable<MediaConfigLength> ClearMediaConfigLength()
	    {
	        List<MediaConfigLength> result = new List<MediaConfigLength>();
	        result.Add(new MediaConfigLength());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear DocMultimidiaUpload.
	    public IEnumerable<DocMultimidiaUpload> ClearDocMultimidiaUpload()
	    {
	        List<DocMultimidiaUpload> result = new List<DocMultimidiaUpload>();
	        result.Add(new DocMultimidiaUpload());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear DocTabelaSync.
	    public IEnumerable<DocTabelaSync> ClearDocTabelaSync()
	    {
	        List<DocTabelaSync> result = new List<DocTabelaSync>();
	        result.Add(new DocTabelaSync());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    
	    [Query(HasSideEffects = false)]
	    //Get DocMultimidiaTabela.
	    public IQueryable<DocMultimidiaTabela> GetDocMultimidiaTabela()
	    {




		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaTabela> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_TABELA
                  let entity0Al1 = entity0.DOC_MULTIMIDIA
                  let entity0Al2 = entity0.DOC_MULTIMIDIA.DOC_CLASSIFICADOR
	            
	            	
	            select new DocMultimidiaTabela()		
	            {
	            
                Conteudo = entity0Al1.CONTEUDO
                , DataCriacao = entity0Al1.DATA_CRIACAO
                , DescDocClassificador = entity0Al2.DESC_DOC_CLASSIFICADOR
                , DescDocumento = entity0Al1.DESC_DOCUMENTO
                , IdChave = entity0.ID_CHAVE
                , IdDocClassificador = entity0Al2.ID_DOC_CLASSIFICADOR
                , LxTipoDocumento = entity0Al1.LX_TIPO_DOCUMENTO
                , LxTipoExtensao = entity0Al1.LX_TIPO_EXTENSAO
                , LxTipoMidia = entity0Al1.LX_TIPO_MIDIA
                , NomeArquivo = entity0Al1.NOME_ARQUIVO
                , Obs = entity0Al1.OBS
                , OrdemApresentacao = entity0.ORDEM_APRESENTACAO
                , TamanhoMidia = entity0Al1.TAMANHO_MIDIA
                , Thumbnail = entity0Al1.THUMBNAIL
                , TipoConteudoHttp = entity0Al1.TIPO_CONTEUDO_HTTP
                , UidChave = entity0.UID_CHAVE
                , UidDocumento = entity0Al1.UID_DOCUMENTO
                , UidTabela = entity0.UID_TABELA
                , Url = entity0Al1.URL
                , XmlMapeamento = entity0Al1.XML_MAPEAMENTO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get DocMultimidiaTabelaNoAssociations.
	    public IQueryable<DocMultimidiaTabela> GetDocMultimidiaTabelaNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaTabela> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_TABELA
                  let entity0Al1 = entity0.DOC_MULTIMIDIA
                  let entity0Al2 = entity0.DOC_MULTIMIDIA.DOC_CLASSIFICADOR
	            
	            	
	            select new DocMultimidiaTabela()		
	            {
	            
                Conteudo = entity0Al1.CONTEUDO
                , DataCriacao = entity0Al1.DATA_CRIACAO
                , DescDocClassificador = entity0Al2.DESC_DOC_CLASSIFICADOR
                , DescDocumento = entity0Al1.DESC_DOCUMENTO
                , IdChave = entity0.ID_CHAVE
                , IdDocClassificador = entity0Al2.ID_DOC_CLASSIFICADOR
                , LxTipoDocumento = entity0Al1.LX_TIPO_DOCUMENTO
                , LxTipoExtensao = entity0Al1.LX_TIPO_EXTENSAO
                , LxTipoMidia = entity0Al1.LX_TIPO_MIDIA
                , NomeArquivo = entity0Al1.NOME_ARQUIVO
                , Obs = entity0Al1.OBS
                , OrdemApresentacao = entity0.ORDEM_APRESENTACAO
                , TamanhoMidia = entity0Al1.TAMANHO_MIDIA
                , Thumbnail = entity0Al1.THUMBNAIL
                , TipoConteudoHttp = entity0Al1.TIPO_CONTEUDO_HTTP
                , UidChave = entity0.UID_CHAVE
                , UidDocumento = entity0Al1.UID_DOCUMENTO
                , UidTabela = entity0.UID_TABELA
                , Url = entity0Al1.URL
                , XmlMapeamento = entity0Al1.XML_MAPEAMENTO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get DocMultimidiaCompact.
	    public IQueryable<DocMultimidiaCompact> GetDocMultimidiaCompact()
	    {




		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaCompact> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_TABELA
                  let entity0Al1 = entity0.DOC_MULTIMIDIA
	            
	            	
	            select new DocMultimidiaCompact()		
	            {
	            
                Conteudo = entity0Al1.CONTEUDO
                , DescDocumento = entity0Al1.DESC_DOCUMENTO
                , IdChave = entity0.ID_CHAVE
                , Thumbnail = entity0Al1.THUMBNAIL
                , UidChave = entity0.UID_CHAVE
                , UidDocumento = entity0Al1.UID_DOCUMENTO
                , UidTabela = entity0.UID_TABELA
                , Url = entity0Al1.URL
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get DocMultimidiaCompactNoAssociations.
	    public IQueryable<DocMultimidiaCompact> GetDocMultimidiaCompactNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaCompact> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_TABELA
                  let entity0Al1 = entity0.DOC_MULTIMIDIA
	            
	            	
	            select new DocMultimidiaCompact()		
	            {
	            
                Conteudo = entity0Al1.CONTEUDO
                , DescDocumento = entity0Al1.DESC_DOCUMENTO
                , IdChave = entity0.ID_CHAVE
                , Thumbnail = entity0Al1.THUMBNAIL
                , UidChave = entity0.UID_CHAVE
                , UidDocumento = entity0Al1.UID_DOCUMENTO
                , UidTabela = entity0.UID_TABELA
                , Url = entity0Al1.URL
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get MultimidiaCompact2BO.
	    public IQueryable<MultimidiaCompact2BO> GetMultimidiaCompact2BO()
	    {




		
	
	        
		
	        
	
	        IQueryable<MultimidiaCompact2BO> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_TABELA
                  let entity0Al1 = entity0.DOC_MULTIMIDIA
                  let entity0Al2 = entity0.DOC_MULTIMIDIA.DOC_CLASSIFICADOR
	            
	            	
	            select new MultimidiaCompact2BO()		
	            {
	            
                Conteudo = entity0Al1.CONTEUDO
                , DescDocumento = entity0Al1.DESC_DOCUMENTO
                , IdChave = entity0.ID_CHAVE
                , IdDocClassificador = entity0Al2.ID_DOC_CLASSIFICADOR
                , LxTipoDocumento = entity0Al1.LX_TIPO_DOCUMENTO
                , LxTipoDocumentoName = ((entity0Al1.LX_TIPO_DOCUMENTO) == 3 ? "Detalhe/Estampa" : ((entity0Al1.LX_TIPO_DOCUMENTO) == 4 ? "360°" : ((entity0Al1.LX_TIPO_DOCUMENTO) == 2 ? "Matriz Para Transformação" : ((entity0Al1.LX_TIPO_DOCUMENTO) == 1 ? "Normal" : ((entity0Al1.LX_TIPO_DOCUMENTO) == 5 ? "Vídeos" : "")))))
                , LxTipoExtensao = entity0Al1.LX_TIPO_EXTENSAO
                , Obs = entity0Al1.OBS
                , OrdemApresentacao = entity0.ORDEM_APRESENTACAO
                , Thumbnail = entity0Al1.THUMBNAIL
                , UidChave = entity0.UID_CHAVE
                , UidDocumento = entity0Al1.UID_DOCUMENTO
                , UidTabela = entity0.UID_TABELA
                , Url = entity0Al1.URL
                , XmlMapeamento = entity0Al1.XML_MAPEAMENTO
                , DescTabela = ""
                , NomeTabela = ""
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get MultimidiaCompact2BONoAssociations.
	    public IQueryable<MultimidiaCompact2BO> GetMultimidiaCompact2BONoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<MultimidiaCompact2BO> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_TABELA
                  let entity0Al1 = entity0.DOC_MULTIMIDIA
                  let entity0Al2 = entity0.DOC_MULTIMIDIA.DOC_CLASSIFICADOR
	            
	            	
	            select new MultimidiaCompact2BO()		
	            {
	            
                Conteudo = entity0Al1.CONTEUDO
                , DescDocumento = entity0Al1.DESC_DOCUMENTO
                , IdChave = entity0.ID_CHAVE
                , IdDocClassificador = entity0Al2.ID_DOC_CLASSIFICADOR
                , LxTipoDocumento = entity0Al1.LX_TIPO_DOCUMENTO
                , LxTipoDocumentoName = ((entity0Al1.LX_TIPO_DOCUMENTO) == 3 ? "Detalhe/Estampa" : ((entity0Al1.LX_TIPO_DOCUMENTO) == 4 ? "360°" : ((entity0Al1.LX_TIPO_DOCUMENTO) == 2 ? "Matriz Para Transformação" : ((entity0Al1.LX_TIPO_DOCUMENTO) == 1 ? "Normal" : ((entity0Al1.LX_TIPO_DOCUMENTO) == 5 ? "Vídeos" : "")))))
                , LxTipoExtensao = entity0Al1.LX_TIPO_EXTENSAO
                , Obs = entity0Al1.OBS
                , OrdemApresentacao = entity0.ORDEM_APRESENTACAO
                , Thumbnail = entity0Al1.THUMBNAIL
                , UidChave = entity0.UID_CHAVE
                , UidDocumento = entity0Al1.UID_DOCUMENTO
                , UidTabela = entity0.UID_TABELA
                , Url = entity0Al1.URL
                , XmlMapeamento = entity0Al1.XML_MAPEAMENTO
                , DescTabela = ""
                , NomeTabela = ""
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get DocMultimidiaUid.
	    public IQueryable<DocMultimidiaUid> GetDocMultimidiaUid()
	    {




		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaUid> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA
	            
	            	
	            select new DocMultimidiaUid()		
	            {
	            
                UidDocumento = entity0.UID_DOCUMENTO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get DocMultimidiaUidNoAssociations.
	    public IQueryable<DocMultimidiaUid> GetDocMultimidiaUidNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaUid> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA
	            
	            	
	            select new DocMultimidiaUid()		
	            {
	            
                UidDocumento = entity0.UID_DOCUMENTO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get DocMultimidiaInfo.
	    public IEnumerable<DocMultimidiaInfo> GetDocMultimidiaInfo()
	    {




	
	        IEnumerable<DocMultimidiaInfo> result = new List<DocMultimidiaInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get DocMultimidiaInfoNoAssociations.
	    public IEnumerable<DocMultimidiaInfo> GetDocMultimidiaInfoNoAssociations()
	    {




	
	        IEnumerable<DocMultimidiaInfo> result = new List<DocMultimidiaInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get DocMultimidia.
	    public IQueryable<DocMultimidia> GetDocMultimidia()
	    {




		
	
	        
		
	        
	
	        IQueryable<DocMultimidia> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA
                  let entity0Al1 = entity0.DOC_CLASSIFICADOR
	            
	            	
	            select new DocMultimidia()		
	            {
	            
                Conteudo = entity0.CONTEUDO
                , DataCriacao = entity0.DATA_CRIACAO
                , DescDocClassificador = entity0Al1.DESC_DOC_CLASSIFICADOR
                , DescDocumento = entity0.DESC_DOCUMENTO
                , IdDocClassificador = entity0Al1.ID_DOC_CLASSIFICADOR
                , LxTipoDocumento = entity0.LX_TIPO_DOCUMENTO
                , LxTipoExtensao = entity0.LX_TIPO_EXTENSAO
                , LxTipoMidia = entity0.LX_TIPO_MIDIA
                , NomeArquivo = entity0.NOME_ARQUIVO
                , Obs = entity0.OBS
                , TamanhoMidia = entity0.TAMANHO_MIDIA
                , Thumbnail = entity0.THUMBNAIL
                , TipoConteudoHttp = entity0.TIPO_CONTEUDO_HTTP
                , UidDocumento = entity0.UID_DOCUMENTO
                , Url = entity0.URL
                , XmlMapeamento = entity0.XML_MAPEAMENTO
			
                ,DocMultimidiaTabelaChildList = 
	                        (from entity1 in entity0.DOC_MULTIMIDIA_TABELA_LISTA
                                  let entity1Al1 = entity1.DOC_MULTIMIDIA
	                        
	                        	
	                        select new DocMultimidiaTabelaChild()
	                        {
	                        
                                IdChave = entity1.ID_CHAVE
                                , OrdemApresentacao = entity1.ORDEM_APRESENTACAO
                                , UidChave = entity1.UID_CHAVE
                                , UidDocumento = entity1Al1.UID_DOCUMENTO
                                , UidTabela = entity1.UID_TABELA
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get DocMultimidiaTabelaChild.
	    public IQueryable<DocMultimidiaTabelaChild> GetDocMultimidiaTabelaChild()
	    {




		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaTabelaChild> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_TABELA
                  let entity0Al1 = entity0.DOC_MULTIMIDIA
	            
	            	
	            select new DocMultimidiaTabelaChild()		
	            {
	            
                IdChave = entity0.ID_CHAVE
                , OrdemApresentacao = entity0.ORDEM_APRESENTACAO
                , UidChave = entity0.UID_CHAVE
                , UidDocumento = entity0Al1.UID_DOCUMENTO
                , UidTabela = entity0.UID_TABELA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get DocMultimidiaNoAssociations.
	    public IQueryable<DocMultimidia> GetDocMultimidiaNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<DocMultimidia> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA
                  let entity0Al1 = entity0.DOC_CLASSIFICADOR
	            
	            	
	            select new DocMultimidia()		
	            {
	            
                Conteudo = entity0.CONTEUDO
                , DataCriacao = entity0.DATA_CRIACAO
                , DescDocClassificador = entity0Al1.DESC_DOC_CLASSIFICADOR
                , DescDocumento = entity0.DESC_DOCUMENTO
                , IdDocClassificador = entity0Al1.ID_DOC_CLASSIFICADOR
                , LxTipoDocumento = entity0.LX_TIPO_DOCUMENTO
                , LxTipoExtensao = entity0.LX_TIPO_EXTENSAO
                , LxTipoMidia = entity0.LX_TIPO_MIDIA
                , NomeArquivo = entity0.NOME_ARQUIVO
                , Obs = entity0.OBS
                , TamanhoMidia = entity0.TAMANHO_MIDIA
                , Thumbnail = entity0.THUMBNAIL
                , TipoConteudoHttp = entity0.TIPO_CONTEUDO_HTTP
                , UidDocumento = entity0.UID_DOCUMENTO
                , Url = entity0.URL
                , XmlMapeamento = entity0.XML_MAPEAMENTO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get DocMultimidiaTabelaChildNoAssociations.
	    public IQueryable<DocMultimidiaTabelaChild> GetDocMultimidiaTabelaChildNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaTabelaChild> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_TABELA
                  let entity0Al1 = entity0.DOC_MULTIMIDIA
	            
	            	
	            select new DocMultimidiaTabelaChild()		
	            {
	            
                IdChave = entity0.ID_CHAVE
                , OrdemApresentacao = entity0.ORDEM_APRESENTACAO
                , UidChave = entity0.UID_CHAVE
                , UidDocumento = entity0Al1.UID_DOCUMENTO
                , UidTabela = entity0.UID_TABELA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get DocMultimidiaConfig.
	    public IQueryable<DocMultimidiaConfig> GetDocMultimidiaConfig()
	    {




		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaConfig> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_CONFIG
	            
	            	
	            select new DocMultimidiaConfig()		
	            {
	            
                DocAltura = entity0.DOC_ALTURA
                , DocDuracao = entity0.DOC_DURACAO
                , DocFormatoVisualizacao = entity0.DOC_FORMATO_VISUALIZACAO
                , DocLargura = entity0.DOC_LARGURA
                , DocTamanho = entity0.DOC_TAMANHO
                , IdTcsAplicativo = entity0.ID_TCS_APLICATIVO
                , LxUsoMultimidia = entity0.LX_USO_MULTIMIDIA
                , LxUsoMultimidiaName = ((entity0.LX_USO_MULTIMIDIA) == 1 ? "Catálogo" : ((entity0.LX_USO_MULTIMIDIA) == 2 ? "Detalhe" : ((entity0.LX_USO_MULTIMIDIA) == 9 ? "Look View" : ((entity0.LX_USO_MULTIMIDIA) == 8 ? "Matriz Mínima" : ((entity0.LX_USO_MULTIMIDIA) == 3 ? "Miniatura" : ((entity0.LX_USO_MULTIMIDIA) == 5 ? "Zoom Ampliado" : ((entity0.LX_USO_MULTIMIDIA) == 4 ? "Zoom de Lente" : "")))))))
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get DocMultimidiaConfigNoAssociations.
	    public IQueryable<DocMultimidiaConfig> GetDocMultimidiaConfigNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaConfig> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_CONFIG
	            
	            	
	            select new DocMultimidiaConfig()		
	            {
	            
                DocAltura = entity0.DOC_ALTURA
                , DocDuracao = entity0.DOC_DURACAO
                , DocFormatoVisualizacao = entity0.DOC_FORMATO_VISUALIZACAO
                , DocLargura = entity0.DOC_LARGURA
                , DocTamanho = entity0.DOC_TAMANHO
                , IdTcsAplicativo = entity0.ID_TCS_APLICATIVO
                , LxUsoMultimidia = entity0.LX_USO_MULTIMIDIA
                , LxUsoMultimidiaName = ((entity0.LX_USO_MULTIMIDIA) == 1 ? "Catálogo" : ((entity0.LX_USO_MULTIMIDIA) == 2 ? "Detalhe" : ((entity0.LX_USO_MULTIMIDIA) == 9 ? "Look View" : ((entity0.LX_USO_MULTIMIDIA) == 8 ? "Matriz Mínima" : ((entity0.LX_USO_MULTIMIDIA) == 3 ? "Miniatura" : ((entity0.LX_USO_MULTIMIDIA) == 5 ? "Zoom Ampliado" : ((entity0.LX_USO_MULTIMIDIA) == 4 ? "Zoom de Lente" : "")))))))
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get MediaElement.
	    public IEnumerable<MediaElement> GetMediaElement()
	    {




	
	        IEnumerable<MediaElement> result = new List<MediaElement>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get MediaElementNoAssociations.
	    public IEnumerable<MediaElement> GetMediaElementNoAssociations()
	    {




	
	        IEnumerable<MediaElement> result = new List<MediaElement>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get MediaConfigLength.
	    public IEnumerable<MediaConfigLength> GetMediaConfigLength()
	    {




	
	        IEnumerable<MediaConfigLength> result = new List<MediaConfigLength>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get MediaConfigLengthNoAssociations.
	    public IEnumerable<MediaConfigLength> GetMediaConfigLengthNoAssociations()
	    {




	
	        IEnumerable<MediaConfigLength> result = new List<MediaConfigLength>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get DocMultimidiaUpload.
	    public IEnumerable<DocMultimidiaUpload> GetDocMultimidiaUpload()
	    {




	
	        IEnumerable<DocMultimidiaUpload> result = new List<DocMultimidiaUpload>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get DocMultimidiaUploadNoAssociations.
	    public IEnumerable<DocMultimidiaUpload> GetDocMultimidiaUploadNoAssociations()
	    {




	
	        IEnumerable<DocMultimidiaUpload> result = new List<DocMultimidiaUpload>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get DocTabelaSync.
	    public IEnumerable<DocTabelaSync> GetDocTabelaSync()
	    {




	
	        IEnumerable<DocTabelaSync> result = new List<DocTabelaSync>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get DocTabelaSyncNoAssociations.
	    public IEnumerable<DocTabelaSync> GetDocTabelaSyncNoAssociations()
	    {




	
	        IEnumerable<DocTabelaSync> result = new List<DocTabelaSync>();
	  	
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	//Add filtering disabled property for DOC_MULTIMIDIA_TABELA
	    	string[] bmDisabledDocMultimidiaTabelaList = this.GetEDM().GetFilteringDisabledList("DOC_MULTIMIDIA_TABELA");
	    	if (bmDisabledDocMultimidiaTabelaList.Length > 0)
	    	{
	
	    		if (bmDisabledDocMultimidiaTabelaList.Contains("DOC_MULTIMIDIA_TABELA.ID_CHAVE"))
	    		{
	    			result.Add("DocMultimidiaTabela|IdChave");
	    			result.Add("DocMultimidiaTabela|DOC_MULTIMIDIA_TABELA.ID_CHAVE");
	    		}
	
	    		if (bmDisabledDocMultimidiaTabelaList.Contains("DOC_MULTIMIDIA_TABELA.ORDEM_APRESENTACAO"))
	    		{
	    			result.Add("DocMultimidiaTabela|OrdemApresentacao");
	    			result.Add("DocMultimidiaTabela|DOC_MULTIMIDIA_TABELA.ORDEM_APRESENTACAO");
	    		}
	
	    		if (bmDisabledDocMultimidiaTabelaList.Contains("DOC_MULTIMIDIA_TABELA.UID_CHAVE"))
	    		{
	    			result.Add("DocMultimidiaTabela|UidChave");
	    			result.Add("DocMultimidiaTabela|DOC_MULTIMIDIA_TABELA.UID_CHAVE");
	    		}
	
	    		if (bmDisabledDocMultimidiaTabelaList.Contains("DOC_MULTIMIDIA_TABELA.UID_TABELA"))
	    		{
	    			result.Add("DocMultimidiaTabela|UidTabela");
	    			result.Add("DocMultimidiaTabela|DOC_MULTIMIDIA_TABELA.UID_TABELA");
	    		}
	    	}
	    	//Add filtering disabled property for DOC_MULTIMIDIA_TABELA
	    	string[] bmDisabledDocMultimidiaCompactList = this.GetEDM().GetFilteringDisabledList("DOC_MULTIMIDIA_TABELA");
	    	if (bmDisabledDocMultimidiaCompactList.Length > 0)
	    	{
	
	    		if (bmDisabledDocMultimidiaCompactList.Contains("DOC_MULTIMIDIA_TABELA.ID_CHAVE"))
	    		{
	    			result.Add("DocMultimidiaCompact|IdChave");
	    			result.Add("DocMultimidiaCompact|DOC_MULTIMIDIA_TABELA.ID_CHAVE");
	    		}
	
	    		if (bmDisabledDocMultimidiaCompactList.Contains("DOC_MULTIMIDIA_TABELA.UID_CHAVE"))
	    		{
	    			result.Add("DocMultimidiaCompact|UidChave");
	    			result.Add("DocMultimidiaCompact|DOC_MULTIMIDIA_TABELA.UID_CHAVE");
	    		}
	
	    		if (bmDisabledDocMultimidiaCompactList.Contains("DOC_MULTIMIDIA_TABELA.UID_TABELA"))
	    		{
	    			result.Add("DocMultimidiaCompact|UidTabela");
	    			result.Add("DocMultimidiaCompact|DOC_MULTIMIDIA_TABELA.UID_TABELA");
	    		}
	    	}
	    	result.Add("MultimidiaCompact2BO|DescTabela");
	    	result.Add("MultimidiaCompact2BO|''");
	    	result.Add("MultimidiaCompact2BO|NomeTabela");
	    	result.Add("MultimidiaCompact2BO|''");
	    	//Add filtering disabled property for DOC_MULTIMIDIA_TABELA
	    	string[] bmDisabledMultimidiaCompact2BOList = this.GetEDM().GetFilteringDisabledList("DOC_MULTIMIDIA_TABELA");
	    	if (bmDisabledMultimidiaCompact2BOList.Length > 0)
	    	{
	
	    		if (bmDisabledMultimidiaCompact2BOList.Contains("DOC_MULTIMIDIA_TABELA.ID_CHAVE"))
	    		{
	    			result.Add("MultimidiaCompact2BO|IdChave");
	    			result.Add("MultimidiaCompact2BO|DOC_MULTIMIDIA_TABELA.ID_CHAVE");
	    		}
	
	    		if (bmDisabledMultimidiaCompact2BOList.Contains("DOC_MULTIMIDIA_TABELA.ORDEM_APRESENTACAO"))
	    		{
	    			result.Add("MultimidiaCompact2BO|OrdemApresentacao");
	    			result.Add("MultimidiaCompact2BO|DOC_MULTIMIDIA_TABELA.ORDEM_APRESENTACAO");
	    		}
	
	    		if (bmDisabledMultimidiaCompact2BOList.Contains("DOC_MULTIMIDIA_TABELA.UID_CHAVE"))
	    		{
	    			result.Add("MultimidiaCompact2BO|UidChave");
	    			result.Add("MultimidiaCompact2BO|DOC_MULTIMIDIA_TABELA.UID_CHAVE");
	    		}
	
	    		if (bmDisabledMultimidiaCompact2BOList.Contains("DOC_MULTIMIDIA_TABELA.UID_TABELA"))
	    		{
	    			result.Add("MultimidiaCompact2BO|UidTabela");
	    			result.Add("MultimidiaCompact2BO|DOC_MULTIMIDIA_TABELA.UID_TABELA");
	    		}
	    	}
	    	//Add filtering disabled property for DOC_MULTIMIDIA
	    	string[] bmDisabledDocMultimidiaUidList = this.GetEDM().GetFilteringDisabledList("DOC_MULTIMIDIA");
	    	if (bmDisabledDocMultimidiaUidList.Length > 0)
	    	{
	
	    		if (bmDisabledDocMultimidiaUidList.Contains("DOC_MULTIMIDIA.UID_DOCUMENTO"))
	    		{
	    			result.Add("DocMultimidiaUid|UidDocumento");
	    			result.Add("DocMultimidiaUid|DOC_MULTIMIDIA.UID_DOCUMENTO");
	    		}
	    	}
	    	//Add filtering disabled property for DOC_MULTIMIDIA
	    	string[] bmDisabledDocMultimidiaList = this.GetEDM().GetFilteringDisabledList("DOC_MULTIMIDIA");
	    	if (bmDisabledDocMultimidiaList.Length > 0)
	    	{
	
	    		if (bmDisabledDocMultimidiaList.Contains("DOC_MULTIMIDIA.CONTEUDO"))
	    		{
	    			result.Add("DocMultimidia|Conteudo");
	    			result.Add("DocMultimidia|DOC_MULTIMIDIA.CONTEUDO");
	    		}
	
	    		if (bmDisabledDocMultimidiaList.Contains("DOC_MULTIMIDIA.DATA_CRIACAO"))
	    		{
	    			result.Add("DocMultimidia|DataCriacao");
	    			result.Add("DocMultimidia|DOC_MULTIMIDIA.DATA_CRIACAO");
	    		}
	
	    		if (bmDisabledDocMultimidiaList.Contains("DOC_MULTIMIDIA.DESC_DOCUMENTO"))
	    		{
	    			result.Add("DocMultimidia|DescDocumento");
	    			result.Add("DocMultimidia|DOC_MULTIMIDIA.DESC_DOCUMENTO");
	    		}
	
	    		if (bmDisabledDocMultimidiaList.Contains("DOC_MULTIMIDIA.LX_TIPO_DOCUMENTO"))
	    		{
	    			result.Add("DocMultimidia|LxTipoDocumento");
	    			result.Add("DocMultimidia|DOC_MULTIMIDIA.LX_TIPO_DOCUMENTO");
	    		}
	
	    		if (bmDisabledDocMultimidiaList.Contains("DOC_MULTIMIDIA.LX_TIPO_EXTENSAO"))
	    		{
	    			result.Add("DocMultimidia|LxTipoExtensao");
	    			result.Add("DocMultimidia|DOC_MULTIMIDIA.LX_TIPO_EXTENSAO");
	    		}
	
	    		if (bmDisabledDocMultimidiaList.Contains("DOC_MULTIMIDIA.LX_TIPO_MIDIA"))
	    		{
	    			result.Add("DocMultimidia|LxTipoMidia");
	    			result.Add("DocMultimidia|DOC_MULTIMIDIA.LX_TIPO_MIDIA");
	    		}
	
	    		if (bmDisabledDocMultimidiaList.Contains("DOC_MULTIMIDIA.NOME_ARQUIVO"))
	    		{
	    			result.Add("DocMultimidia|NomeArquivo");
	    			result.Add("DocMultimidia|DOC_MULTIMIDIA.NOME_ARQUIVO");
	    		}
	
	    		if (bmDisabledDocMultimidiaList.Contains("DOC_MULTIMIDIA.OBS"))
	    		{
	    			result.Add("DocMultimidia|Obs");
	    			result.Add("DocMultimidia|DOC_MULTIMIDIA.OBS");
	    		}
	
	    		if (bmDisabledDocMultimidiaList.Contains("DOC_MULTIMIDIA.TAMANHO_MIDIA"))
	    		{
	    			result.Add("DocMultimidia|TamanhoMidia");
	    			result.Add("DocMultimidia|DOC_MULTIMIDIA.TAMANHO_MIDIA");
	    		}
	
	    		if (bmDisabledDocMultimidiaList.Contains("DOC_MULTIMIDIA.THUMBNAIL"))
	    		{
	    			result.Add("DocMultimidia|Thumbnail");
	    			result.Add("DocMultimidia|DOC_MULTIMIDIA.THUMBNAIL");
	    		}
	
	    		if (bmDisabledDocMultimidiaList.Contains("DOC_MULTIMIDIA.TIPO_CONTEUDO_HTTP"))
	    		{
	    			result.Add("DocMultimidia|TipoConteudoHttp");
	    			result.Add("DocMultimidia|DOC_MULTIMIDIA.TIPO_CONTEUDO_HTTP");
	    		}
	
	    		if (bmDisabledDocMultimidiaList.Contains("DOC_MULTIMIDIA.UID_DOCUMENTO"))
	    		{
	    			result.Add("DocMultimidia|UidDocumento");
	    			result.Add("DocMultimidia|DOC_MULTIMIDIA.UID_DOCUMENTO");
	    		}
	
	    		if (bmDisabledDocMultimidiaList.Contains("DOC_MULTIMIDIA.URL"))
	    		{
	    			result.Add("DocMultimidia|Url");
	    			result.Add("DocMultimidia|DOC_MULTIMIDIA.URL");
	    		}
	
	    		if (bmDisabledDocMultimidiaList.Contains("DOC_MULTIMIDIA.XML_MAPEAMENTO"))
	    		{
	    			result.Add("DocMultimidia|XmlMapeamento");
	    			result.Add("DocMultimidia|DOC_MULTIMIDIA.XML_MAPEAMENTO");
	    		}
	    	}
	    	//Add filtering disabled property for DOC_MULTIMIDIA_TABELA
	    	string[] bmDisabledDocMultimidiaTabelaChildList = this.GetEDM().GetFilteringDisabledList("DOC_MULTIMIDIA_TABELA");
	    	if (bmDisabledDocMultimidiaTabelaChildList.Length > 0)
	    	{
	
	    		if (bmDisabledDocMultimidiaTabelaChildList.Contains("DOC_MULTIMIDIA_TABELA.ID_CHAVE"))
	    		{
	    			result.Add("DocMultimidiaTabelaChild|IdChave");
	    			result.Add("DocMultimidiaTabelaChild|DOC_MULTIMIDIA_TABELA.ID_CHAVE");
	    		}
	
	    		if (bmDisabledDocMultimidiaTabelaChildList.Contains("DOC_MULTIMIDIA_TABELA.ORDEM_APRESENTACAO"))
	    		{
	    			result.Add("DocMultimidiaTabelaChild|OrdemApresentacao");
	    			result.Add("DocMultimidiaTabelaChild|DOC_MULTIMIDIA_TABELA.ORDEM_APRESENTACAO");
	    		}
	
	    		if (bmDisabledDocMultimidiaTabelaChildList.Contains("DOC_MULTIMIDIA_TABELA.UID_CHAVE"))
	    		{
	    			result.Add("DocMultimidiaTabelaChild|UidChave");
	    			result.Add("DocMultimidiaTabelaChild|DOC_MULTIMIDIA_TABELA.UID_CHAVE");
	    		}
	
	    		if (bmDisabledDocMultimidiaTabelaChildList.Contains("DOC_MULTIMIDIA_TABELA.UID_TABELA"))
	    		{
	    			result.Add("DocMultimidiaTabelaChild|UidTabela");
	    			result.Add("DocMultimidiaTabelaChild|DOC_MULTIMIDIA_TABELA.UID_TABELA");
	    		}
	    	}
	    	//Add filtering disabled property for DOC_MULTIMIDIA_CONFIG
	    	string[] bmDisabledDocMultimidiaConfigList = this.GetEDM().GetFilteringDisabledList("DOC_MULTIMIDIA_CONFIG");
	    	if (bmDisabledDocMultimidiaConfigList.Length > 0)
	    	{
	
	    		if (bmDisabledDocMultimidiaConfigList.Contains("DOC_MULTIMIDIA_CONFIG.DOC_ALTURA"))
	    		{
	    			result.Add("DocMultimidiaConfig|DocAltura");
	    			result.Add("DocMultimidiaConfig|DOC_MULTIMIDIA_CONFIG.DOC_ALTURA");
	    		}
	
	    		if (bmDisabledDocMultimidiaConfigList.Contains("DOC_MULTIMIDIA_CONFIG.DOC_DURACAO"))
	    		{
	    			result.Add("DocMultimidiaConfig|DocDuracao");
	    			result.Add("DocMultimidiaConfig|DOC_MULTIMIDIA_CONFIG.DOC_DURACAO");
	    		}
	
	    		if (bmDisabledDocMultimidiaConfigList.Contains("DOC_MULTIMIDIA_CONFIG.DOC_FORMATO_VISUALIZACAO"))
	    		{
	    			result.Add("DocMultimidiaConfig|DocFormatoVisualizacao");
	    			result.Add("DocMultimidiaConfig|DOC_MULTIMIDIA_CONFIG.DOC_FORMATO_VISUALIZACAO");
	    		}
	
	    		if (bmDisabledDocMultimidiaConfigList.Contains("DOC_MULTIMIDIA_CONFIG.DOC_LARGURA"))
	    		{
	    			result.Add("DocMultimidiaConfig|DocLargura");
	    			result.Add("DocMultimidiaConfig|DOC_MULTIMIDIA_CONFIG.DOC_LARGURA");
	    		}
	
	    		if (bmDisabledDocMultimidiaConfigList.Contains("DOC_MULTIMIDIA_CONFIG.DOC_TAMANHO"))
	    		{
	    			result.Add("DocMultimidiaConfig|DocTamanho");
	    			result.Add("DocMultimidiaConfig|DOC_MULTIMIDIA_CONFIG.DOC_TAMANHO");
	    		}
	
	    		if (bmDisabledDocMultimidiaConfigList.Contains("DOC_MULTIMIDIA_CONFIG.ID_TCS_APLICATIVO"))
	    		{
	    			result.Add("DocMultimidiaConfig|IdTcsAplicativo");
	    			result.Add("DocMultimidiaConfig|DOC_MULTIMIDIA_CONFIG.ID_TCS_APLICATIVO");
	    		}
	
	    		if (bmDisabledDocMultimidiaConfigList.Contains("DOC_MULTIMIDIA_CONFIG.LX_USO_MULTIMIDIA"))
	    		{
	    			result.Add("DocMultimidiaConfig|LxUsoMultimidia");
	    			result.Add("DocMultimidiaConfig|DOC_MULTIMIDIA_CONFIG.LX_USO_MULTIMIDIA");
	    		}
	    	}
	    	return result;
	    }

	    #endregion Excluded Filters

	    #region Get By EntitySearchId Methods Definitions.
	
				
	    [Ignore]
	    //Get DocMultimidiaTabela By EntitySearchId.
	    public IQueryable<DocMultimidiaTabela> GetDocMultimidiaTabelaByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetDocMultimidiaTabelaByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get DocMultimidiaTabela By EntitySearchId.
	    public IQueryable<DocMultimidiaTabela> GetDocMultimidiaTabelaByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetDocMultimidiaTabelaByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get DocMultimidiaCompact By EntitySearchId.
	    public IQueryable<DocMultimidiaCompact> GetDocMultimidiaCompactByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetDocMultimidiaCompactByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get DocMultimidiaCompact By EntitySearchId.
	    public IQueryable<DocMultimidiaCompact> GetDocMultimidiaCompactByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetDocMultimidiaCompactByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get MultimidiaCompact2BO By EntitySearchId.
	    public IQueryable<MultimidiaCompact2BO> GetMultimidiaCompact2BOByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetMultimidiaCompact2BOByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get MultimidiaCompact2BO By EntitySearchId.
	    public IQueryable<MultimidiaCompact2BO> GetMultimidiaCompact2BOByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetMultimidiaCompact2BOByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get DocMultimidiaUid By EntitySearchId.
	    public IQueryable<DocMultimidiaUid> GetDocMultimidiaUidByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetDocMultimidiaUidByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get DocMultimidiaUid By EntitySearchId.
	    public IQueryable<DocMultimidiaUid> GetDocMultimidiaUidByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetDocMultimidiaUidByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get DocMultimidiaInfo By EntitySearchId.
	    public IEnumerable<DocMultimidiaInfo> GetDocMultimidiaInfoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetDocMultimidiaInfoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get DocMultimidiaInfo By EntitySearchId.
	    public IEnumerable<DocMultimidiaInfo> GetDocMultimidiaInfoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetDocMultimidiaInfoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get DocMultimidia By EntitySearchId.
	    public IQueryable<DocMultimidia> GetDocMultimidiaByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetDocMultimidiaByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get DocMultimidiaTabelaChild By EntitySearchId.
	    public IQueryable<DocMultimidiaTabelaChild> GetDocMultimidiaTabelaChildByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetDocMultimidiaTabelaChildByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get DocMultimidia By EntitySearchId.
	    public IQueryable<DocMultimidia> GetDocMultimidiaByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetDocMultimidiaByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get DocMultimidiaTabelaChild By EntitySearchId.
	    public IQueryable<DocMultimidiaTabelaChild> GetDocMultimidiaTabelaChildByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetDocMultimidiaTabelaChildByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get DocMultimidiaConfig By EntitySearchId.
	    public IQueryable<DocMultimidiaConfig> GetDocMultimidiaConfigByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetDocMultimidiaConfigByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get DocMultimidiaConfig By EntitySearchId.
	    public IQueryable<DocMultimidiaConfig> GetDocMultimidiaConfigByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetDocMultimidiaConfigByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get MediaElement By EntitySearchId.
	    public IEnumerable<MediaElement> GetMediaElementByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetMediaElementByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get MediaElement By EntitySearchId.
	    public IEnumerable<MediaElement> GetMediaElementByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetMediaElementByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get MediaConfigLength By EntitySearchId.
	    public IEnumerable<MediaConfigLength> GetMediaConfigLengthByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetMediaConfigLengthByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get MediaConfigLength By EntitySearchId.
	    public IEnumerable<MediaConfigLength> GetMediaConfigLengthByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetMediaConfigLengthByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get DocMultimidiaUpload By EntitySearchId.
	    public IEnumerable<DocMultimidiaUpload> GetDocMultimidiaUploadByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetDocMultimidiaUploadByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get DocMultimidiaUpload By EntitySearchId.
	    public IEnumerable<DocMultimidiaUpload> GetDocMultimidiaUploadByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetDocMultimidiaUploadByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get DocTabelaSync By EntitySearchId.
	    public IEnumerable<DocTabelaSync> GetDocTabelaSyncByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetDocTabelaSyncByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get DocTabelaSync By EntitySearchId.
	    public IEnumerable<DocTabelaSync> GetDocTabelaSyncByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetDocTabelaSyncByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get DocMultimidiaTabela By Example.
	    [Ignore]
	    public IQueryable<DocMultimidiaTabela> GetDocMultimidiaTabelaByExample(DocMultimidiaTabela entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetDocMultimidiaTabelaByEntitySearch(queryAnalysis);
	    }
			
	    //Get DocMultimidiaTabela By Example.
	    [Ignore]
	    public IQueryable<DocMultimidiaTabela> GetDocMultimidiaTabelaByExampleNoAssociations(DocMultimidiaTabela entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetDocMultimidiaTabelaByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get DocMultimidiaCompact By Example.
	    [Ignore]
	    public IQueryable<DocMultimidiaCompact> GetDocMultimidiaCompactByExample(DocMultimidiaCompact entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetDocMultimidiaCompactByEntitySearch(queryAnalysis);
	    }
			
	    //Get DocMultimidiaCompact By Example.
	    [Ignore]
	    public IQueryable<DocMultimidiaCompact> GetDocMultimidiaCompactByExampleNoAssociations(DocMultimidiaCompact entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetDocMultimidiaCompactByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get MultimidiaCompact2BO By Example.
	    [Ignore]
	    public IQueryable<MultimidiaCompact2BO> GetMultimidiaCompact2BOByExample(MultimidiaCompact2BO entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetMultimidiaCompact2BOByEntitySearch(queryAnalysis);
	    }
			
	    //Get MultimidiaCompact2BO By Example.
	    [Ignore]
	    public IQueryable<MultimidiaCompact2BO> GetMultimidiaCompact2BOByExampleNoAssociations(MultimidiaCompact2BO entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetMultimidiaCompact2BOByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get DocMultimidiaUid By Example.
	    [Ignore]
	    public IQueryable<DocMultimidiaUid> GetDocMultimidiaUidByExample(DocMultimidiaUid entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetDocMultimidiaUidByEntitySearch(queryAnalysis);
	    }
			
	    //Get DocMultimidiaUid By Example.
	    [Ignore]
	    public IQueryable<DocMultimidiaUid> GetDocMultimidiaUidByExampleNoAssociations(DocMultimidiaUid entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetDocMultimidiaUidByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get DocMultimidiaInfo By Example.
	    [Ignore]
	    public IEnumerable<DocMultimidiaInfo> GetDocMultimidiaInfoByExample(DocMultimidiaInfo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetDocMultimidiaInfoByEntitySearch(queryAnalysis);
	    }
			
	    //Get DocMultimidiaInfo By Example.
	    [Ignore]
	    public IEnumerable<DocMultimidiaInfo> GetDocMultimidiaInfoByExampleNoAssociations(DocMultimidiaInfo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetDocMultimidiaInfoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get DocMultimidia By Example.
	    [Ignore]
	    public IQueryable<DocMultimidia> GetDocMultimidiaByExample(DocMultimidia entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetDocMultimidiaByEntitySearch(queryAnalysis);
	    }
			
	    //Get DocMultimidiaTabelaChild By Example.
	    [Ignore]
	    public IQueryable<DocMultimidiaTabelaChild> GetDocMultimidiaTabelaChildByExample(DocMultimidiaTabelaChild entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetDocMultimidiaTabelaChildByEntitySearch(queryAnalysis);
	    }
			
	    //Get DocMultimidia By Example.
	    [Ignore]
	    public IQueryable<DocMultimidia> GetDocMultimidiaByExampleNoAssociations(DocMultimidia entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetDocMultimidiaByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get DocMultimidiaTabelaChild By Example.
	    [Ignore]
	    public IQueryable<DocMultimidiaTabelaChild> GetDocMultimidiaTabelaChildByExampleNoAssociations(DocMultimidiaTabelaChild entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetDocMultimidiaTabelaChildByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get DocMultimidiaConfig By Example.
	    [Ignore]
	    public IQueryable<DocMultimidiaConfig> GetDocMultimidiaConfigByExample(DocMultimidiaConfig entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetDocMultimidiaConfigByEntitySearch(queryAnalysis);
	    }
			
	    //Get DocMultimidiaConfig By Example.
	    [Ignore]
	    public IQueryable<DocMultimidiaConfig> GetDocMultimidiaConfigByExampleNoAssociations(DocMultimidiaConfig entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetDocMultimidiaConfigByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get MediaElement By Example.
	    [Ignore]
	    public IEnumerable<MediaElement> GetMediaElementByExample(MediaElement entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetMediaElementByEntitySearch(queryAnalysis);
	    }
			
	    //Get MediaElement By Example.
	    [Ignore]
	    public IEnumerable<MediaElement> GetMediaElementByExampleNoAssociations(MediaElement entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetMediaElementByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get MediaConfigLength By Example.
	    [Ignore]
	    public IEnumerable<MediaConfigLength> GetMediaConfigLengthByExample(MediaConfigLength entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetMediaConfigLengthByEntitySearch(queryAnalysis);
	    }
			
	    //Get MediaConfigLength By Example.
	    [Ignore]
	    public IEnumerable<MediaConfigLength> GetMediaConfigLengthByExampleNoAssociations(MediaConfigLength entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetMediaConfigLengthByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get DocMultimidiaUpload By Example.
	    [Ignore]
	    public IEnumerable<DocMultimidiaUpload> GetDocMultimidiaUploadByExample(DocMultimidiaUpload entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetDocMultimidiaUploadByEntitySearch(queryAnalysis);
	    }
			
	    //Get DocMultimidiaUpload By Example.
	    [Ignore]
	    public IEnumerable<DocMultimidiaUpload> GetDocMultimidiaUploadByExampleNoAssociations(DocMultimidiaUpload entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetDocMultimidiaUploadByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get DocTabelaSync By Example.
	    [Ignore]
	    public IEnumerable<DocTabelaSync> GetDocTabelaSyncByExample(DocTabelaSync entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetDocTabelaSyncByEntitySearch(queryAnalysis);
	    }
			
	    //Get DocTabelaSync By Example.
	    [Ignore]
	    public IEnumerable<DocTabelaSync> GetDocTabelaSyncByExampleNoAssociations(DocTabelaSync entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetDocTabelaSyncByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public DocMultimidiaTabela GetDocMultimidiaTabelaByKey(Int64 idChave, System.Guid uidChave, System.Guid uidDocumento, System.Guid uidTabela)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("DocMultimidiaTabela");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdChave"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idChave));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "UidChave"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, uidChave));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "UidDocumento"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, uidDocumento));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "UidTabela"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, uidTabela));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetDocMultimidiaTabelaByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public DocMultimidiaCompact GetDocMultimidiaCompactByKey(Int64 idChave, System.Guid uidChave, System.Guid uidDocumento, System.Guid uidTabela)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("DocMultimidiaCompact");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdChave"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idChave));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "UidChave"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, uidChave));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "UidDocumento"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, uidDocumento));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "UidTabela"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, uidTabela));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetDocMultimidiaCompactByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public MultimidiaCompact2BO GetMultimidiaCompact2BOByKey(Int64 idChave, System.Guid uidChave, System.Guid uidDocumento, System.Guid uidTabela)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("MultimidiaCompact2BO");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdChave"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idChave));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "UidChave"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, uidChave));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "UidDocumento"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, uidDocumento));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "UidTabela"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, uidTabela));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetMultimidiaCompact2BOByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public DocMultimidiaUid GetDocMultimidiaUidByKey(System.Guid uidDocumento)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("DocMultimidiaUid");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "UidDocumento"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, uidDocumento));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetDocMultimidiaUidByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public DocMultimidiaInfo GetDocMultimidiaInfoByKey(Guid uidDocumento)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("DocMultimidiaInfo");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "UidDocumento"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, uidDocumento));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetDocMultimidiaInfoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public DocMultimidia GetDocMultimidiaByKey(System.Guid uidDocumento)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("DocMultimidia");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "UidDocumento"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, uidDocumento));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetDocMultimidiaByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public DocMultimidiaTabelaChild GetDocMultimidiaTabelaChildByKey(Int64 idChave, System.Guid uidChave, System.Guid uidDocumento, System.Guid uidTabela)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("DocMultimidiaTabelaChild");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdChave"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idChave));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "UidChave"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, uidChave));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "UidDocumento"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, uidDocumento));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "UidTabela"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, uidTabela));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetDocMultimidiaTabelaChildByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public DocMultimidiaConfig GetDocMultimidiaConfigByKey(Int32 idTcsAplicativo, Byte lxUsoMultimidia)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("DocMultimidiaConfig");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsAplicativo"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsAplicativo));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "LxUsoMultimidia"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, lxUsoMultimidia));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetDocMultimidiaConfigByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public MediaElement GetMediaElementByKey(Guid id)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("MediaElement");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "Id"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, id));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetMediaElementByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public MediaConfigLength GetMediaConfigLengthByKey(int idApp, int idUse)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("MediaConfigLength");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdApp"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idApp));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUse"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idUse));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetMediaConfigLengthByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public DocMultimidiaUpload GetDocMultimidiaUploadByKey(byte tipoDocumento)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("DocMultimidiaUpload");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "TipoDocumento"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, tipoDocumento));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetDocMultimidiaUploadByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public DocTabelaSync GetDocTabelaSyncByKey(string nomeTabela)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("DocTabelaSync");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "NomeTabela"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, nomeTabela));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetDocTabelaSyncByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get DocMultimidiaTabelaByEntitySearch.
	    public IQueryable<DocMultimidiaTabela> GetDocMultimidiaTabelaByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(DocMultimidiaTabela));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaTabela> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_TABELA.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.DOC_MULTIMIDIA
                  let entity0Al2 = entity0.DOC_MULTIMIDIA.DOC_CLASSIFICADOR
	            
	            	
	            select new DocMultimidiaTabela()		
	            {
	            
                Conteudo = entity0Al1.CONTEUDO
                , DataCriacao = entity0Al1.DATA_CRIACAO
                , DescDocClassificador = entity0Al2.DESC_DOC_CLASSIFICADOR
                , DescDocumento = entity0Al1.DESC_DOCUMENTO
                , IdChave = entity0.ID_CHAVE
                , IdDocClassificador = entity0Al2.ID_DOC_CLASSIFICADOR
                , LxTipoDocumento = entity0Al1.LX_TIPO_DOCUMENTO
                , LxTipoExtensao = entity0Al1.LX_TIPO_EXTENSAO
                , LxTipoMidia = entity0Al1.LX_TIPO_MIDIA
                , NomeArquivo = entity0Al1.NOME_ARQUIVO
                , Obs = entity0Al1.OBS
                , OrdemApresentacao = entity0.ORDEM_APRESENTACAO
                , TamanhoMidia = entity0Al1.TAMANHO_MIDIA
                , Thumbnail = entity0Al1.THUMBNAIL
                , TipoConteudoHttp = entity0Al1.TIPO_CONTEUDO_HTTP
                , UidChave = entity0.UID_CHAVE
                , UidDocumento = entity0Al1.UID_DOCUMENTO
                , UidTabela = entity0.UID_TABELA
                , Url = entity0Al1.URL
                , XmlMapeamento = entity0Al1.XML_MAPEAMENTO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get DocMultimidiaTabelaByEntitySearchNoAssociations.
	    public IQueryable<DocMultimidiaTabela> GetDocMultimidiaTabelaByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(DocMultimidiaTabela));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaTabela> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_TABELA.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.DOC_MULTIMIDIA
                  let entity0Al2 = entity0.DOC_MULTIMIDIA.DOC_CLASSIFICADOR
	            
	            	
	            select new DocMultimidiaTabela()		
	            {
	            
                Conteudo = entity0Al1.CONTEUDO
                , DataCriacao = entity0Al1.DATA_CRIACAO
                , DescDocClassificador = entity0Al2.DESC_DOC_CLASSIFICADOR
                , DescDocumento = entity0Al1.DESC_DOCUMENTO
                , IdChave = entity0.ID_CHAVE
                , IdDocClassificador = entity0Al2.ID_DOC_CLASSIFICADOR
                , LxTipoDocumento = entity0Al1.LX_TIPO_DOCUMENTO
                , LxTipoExtensao = entity0Al1.LX_TIPO_EXTENSAO
                , LxTipoMidia = entity0Al1.LX_TIPO_MIDIA
                , NomeArquivo = entity0Al1.NOME_ARQUIVO
                , Obs = entity0Al1.OBS
                , OrdemApresentacao = entity0.ORDEM_APRESENTACAO
                , TamanhoMidia = entity0Al1.TAMANHO_MIDIA
                , Thumbnail = entity0Al1.THUMBNAIL
                , TipoConteudoHttp = entity0Al1.TIPO_CONTEUDO_HTTP
                , UidChave = entity0.UID_CHAVE
                , UidDocumento = entity0Al1.UID_DOCUMENTO
                , UidTabela = entity0.UID_TABELA
                , Url = entity0Al1.URL
                , XmlMapeamento = entity0Al1.XML_MAPEAMENTO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get DocMultimidiaCompactByEntitySearch.
	    public IQueryable<DocMultimidiaCompact> GetDocMultimidiaCompactByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(DocMultimidiaCompact));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaCompact> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_TABELA.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.DOC_MULTIMIDIA
	            
	            	
	            select new DocMultimidiaCompact()		
	            {
	            
                Conteudo = entity0Al1.CONTEUDO
                , DescDocumento = entity0Al1.DESC_DOCUMENTO
                , IdChave = entity0.ID_CHAVE
                , Thumbnail = entity0Al1.THUMBNAIL
                , UidChave = entity0.UID_CHAVE
                , UidDocumento = entity0Al1.UID_DOCUMENTO
                , UidTabela = entity0.UID_TABELA
                , Url = entity0Al1.URL
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get DocMultimidiaCompactByEntitySearchNoAssociations.
	    public IQueryable<DocMultimidiaCompact> GetDocMultimidiaCompactByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(DocMultimidiaCompact));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaCompact> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_TABELA.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.DOC_MULTIMIDIA
	            
	            	
	            select new DocMultimidiaCompact()		
	            {
	            
                Conteudo = entity0Al1.CONTEUDO
                , DescDocumento = entity0Al1.DESC_DOCUMENTO
                , IdChave = entity0.ID_CHAVE
                , Thumbnail = entity0Al1.THUMBNAIL
                , UidChave = entity0.UID_CHAVE
                , UidDocumento = entity0Al1.UID_DOCUMENTO
                , UidTabela = entity0.UID_TABELA
                , Url = entity0Al1.URL
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get MultimidiaCompact2BOByEntitySearch.
	    public IQueryable<MultimidiaCompact2BO> GetMultimidiaCompact2BOByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(MultimidiaCompact2BO));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<MultimidiaCompact2BO> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_TABELA.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.DOC_MULTIMIDIA
                  let entity0Al2 = entity0.DOC_MULTIMIDIA.DOC_CLASSIFICADOR
	            
	            	
	            select new MultimidiaCompact2BO()		
	            {
	            
                Conteudo = entity0Al1.CONTEUDO
                , DescDocumento = entity0Al1.DESC_DOCUMENTO
                , IdChave = entity0.ID_CHAVE
                , IdDocClassificador = entity0Al2.ID_DOC_CLASSIFICADOR
                , LxTipoDocumento = entity0Al1.LX_TIPO_DOCUMENTO
                , LxTipoDocumentoName = ((entity0Al1.LX_TIPO_DOCUMENTO) == 3 ? "Detalhe/Estampa" : ((entity0Al1.LX_TIPO_DOCUMENTO) == 4 ? "360°" : ((entity0Al1.LX_TIPO_DOCUMENTO) == 2 ? "Matriz Para Transformação" : ((entity0Al1.LX_TIPO_DOCUMENTO) == 1 ? "Normal" : ((entity0Al1.LX_TIPO_DOCUMENTO) == 5 ? "Vídeos" : "")))))
                , LxTipoExtensao = entity0Al1.LX_TIPO_EXTENSAO
                , Obs = entity0Al1.OBS
                , OrdemApresentacao = entity0.ORDEM_APRESENTACAO
                , Thumbnail = entity0Al1.THUMBNAIL
                , UidChave = entity0.UID_CHAVE
                , UidDocumento = entity0Al1.UID_DOCUMENTO
                , UidTabela = entity0.UID_TABELA
                , Url = entity0Al1.URL
                , XmlMapeamento = entity0Al1.XML_MAPEAMENTO
                , DescTabela = ""
                , NomeTabela = ""
		
	            }
	            );
	
	        SetMultimidiaCompact2BOBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get MultimidiaCompact2BOByEntitySearchNoAssociations.
	    public IQueryable<MultimidiaCompact2BO> GetMultimidiaCompact2BOByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(MultimidiaCompact2BO));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<MultimidiaCompact2BO> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_TABELA.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.DOC_MULTIMIDIA
                  let entity0Al2 = entity0.DOC_MULTIMIDIA.DOC_CLASSIFICADOR
	            
	            	
	            select new MultimidiaCompact2BO()		
	            {
	            
                Conteudo = entity0Al1.CONTEUDO
                , DescDocumento = entity0Al1.DESC_DOCUMENTO
                , IdChave = entity0.ID_CHAVE
                , IdDocClassificador = entity0Al2.ID_DOC_CLASSIFICADOR
                , LxTipoDocumento = entity0Al1.LX_TIPO_DOCUMENTO
                , LxTipoDocumentoName = ((entity0Al1.LX_TIPO_DOCUMENTO) == 3 ? "Detalhe/Estampa" : ((entity0Al1.LX_TIPO_DOCUMENTO) == 4 ? "360°" : ((entity0Al1.LX_TIPO_DOCUMENTO) == 2 ? "Matriz Para Transformação" : ((entity0Al1.LX_TIPO_DOCUMENTO) == 1 ? "Normal" : ((entity0Al1.LX_TIPO_DOCUMENTO) == 5 ? "Vídeos" : "")))))
                , LxTipoExtensao = entity0Al1.LX_TIPO_EXTENSAO
                , Obs = entity0Al1.OBS
                , OrdemApresentacao = entity0.ORDEM_APRESENTACAO
                , Thumbnail = entity0Al1.THUMBNAIL
                , UidChave = entity0.UID_CHAVE
                , UidDocumento = entity0Al1.UID_DOCUMENTO
                , UidTabela = entity0.UID_TABELA
                , Url = entity0Al1.URL
                , XmlMapeamento = entity0Al1.XML_MAPEAMENTO
                , DescTabela = ""
                , NomeTabela = ""
		
	            }
	            );
	
	        SetMultimidiaCompact2BOBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
	
	    [Ignore()]
	    private void SetMultimidiaCompact2BOBusinessFilter(ref IQueryable<MultimidiaCompact2BO> query, List<EntitySearch> entitySearchList)
	    {
	    		int idxElement;
	    		string operatorValue;
	    		object value;
	    		//Get query by functions
	    		if (entitySearchList.Count > 0)
	    		{
	    			foreach (EntitySearch search in entitySearchList.Where(e => e.EntityName == "MultimidiaCompact2BO"))
	    			{

	
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "DescTabela" || e.Value.ToString() == "''")))
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
	    										string tmpDescTabela1 = (string)value;
	    										query = from r in query where r.DescTabela == tmpDescTabela1 select r;
	    										break;
	    									case "!=":
	    										string tmpDescTabela2 = (string)value;
	    										query = from r in query where r.DescTabela != tmpDescTabela2 select r;
	    										break;

	
	    									case "Contains":
	    										string tmpDescTabela7 = (string)value;
	    									    query = from r in query where r.DescTabela.Contains(tmpDescTabela7) select r;
	    									    break;
	    									case "StartsWith":
	    										string tmpDescTabela8 = (string)value;
	    									    query = from r in query where r.DescTabela.StartsWith(tmpDescTabela8) select r;
	    									    break;
	    									case "EndsWith":
	    										string tmpDescTabela9 = (string)value;
	    									    query = from r in query where r.DescTabela.EndsWith(tmpDescTabela9) select r;
	    									    break;

	
	    									default:
	    										break;
	    								}                                
	    							}
	    						}
        					} 

    
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "NomeTabela" || e.Value.ToString() == "''")))
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
	    										string tmpNomeTabela1 = (string)value;
	    										query = from r in query where r.NomeTabela == tmpNomeTabela1 select r;
	    										break;
	    									case "!=":
	    										string tmpNomeTabela2 = (string)value;
	    										query = from r in query where r.NomeTabela != tmpNomeTabela2 select r;
	    										break;

	
	    									case "Contains":
	    										string tmpNomeTabela7 = (string)value;
	    									    query = from r in query where r.NomeTabela.Contains(tmpNomeTabela7) select r;
	    									    break;
	    									case "StartsWith":
	    										string tmpNomeTabela8 = (string)value;
	    									    query = from r in query where r.NomeTabela.StartsWith(tmpNomeTabela8) select r;
	    									    break;
	    									case "EndsWith":
	    										string tmpNomeTabela9 = (string)value;
	    									    query = from r in query where r.NomeTabela.EndsWith(tmpNomeTabela9) select r;
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
	    //Get DocMultimidiaUidByEntitySearch.
	    public IQueryable<DocMultimidiaUid> GetDocMultimidiaUidByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(DocMultimidiaUid));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaUid> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new DocMultimidiaUid()		
	            {
	            
                UidDocumento = entity0.UID_DOCUMENTO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get DocMultimidiaUidByEntitySearchNoAssociations.
	    public IQueryable<DocMultimidiaUid> GetDocMultimidiaUidByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(DocMultimidiaUid));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaUid> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new DocMultimidiaUid()		
	            {
	            
                UidDocumento = entity0.UID_DOCUMENTO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get DocMultimidiaInfoByEntitySearch.
	    public IEnumerable<DocMultimidiaInfo> GetDocMultimidiaInfoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<DocMultimidiaInfo> result = new List<DocMultimidiaInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get DocMultimidiaInfoByEntitySearchNoAssociations.
	    public IEnumerable<DocMultimidiaInfo> GetDocMultimidiaInfoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<DocMultimidiaInfo> result = new List<DocMultimidiaInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get DocMultimidiaByEntitySearch.
	    public IQueryable<DocMultimidia> GetDocMultimidiaByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(DocMultimidia));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<DocMultimidia> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.DOC_CLASSIFICADOR
	            
	            	
	            select new DocMultimidia()		
	            {
	            
                Conteudo = entity0.CONTEUDO
                , DataCriacao = entity0.DATA_CRIACAO
                , DescDocClassificador = entity0Al1.DESC_DOC_CLASSIFICADOR
                , DescDocumento = entity0.DESC_DOCUMENTO
                , IdDocClassificador = entity0Al1.ID_DOC_CLASSIFICADOR
                , LxTipoDocumento = entity0.LX_TIPO_DOCUMENTO
                , LxTipoExtensao = entity0.LX_TIPO_EXTENSAO
                , LxTipoMidia = entity0.LX_TIPO_MIDIA
                , NomeArquivo = entity0.NOME_ARQUIVO
                , Obs = entity0.OBS
                , TamanhoMidia = entity0.TAMANHO_MIDIA
                , Thumbnail = entity0.THUMBNAIL
                , TipoConteudoHttp = entity0.TIPO_CONTEUDO_HTTP
                , UidDocumento = entity0.UID_DOCUMENTO
                , Url = entity0.URL
                , XmlMapeamento = entity0.XML_MAPEAMENTO
			
                ,DocMultimidiaTabelaChildList = 
	                        (from entity1 in entity0.DOC_MULTIMIDIA_TABELA_LISTA
                                  let entity1Al1 = entity1.DOC_MULTIMIDIA
	                        
	                        	
	                        select new DocMultimidiaTabelaChild()
	                        {
	                        
                                IdChave = entity1.ID_CHAVE
                                , OrdemApresentacao = entity1.ORDEM_APRESENTACAO
                                , UidChave = entity1.UID_CHAVE
                                , UidDocumento = entity1Al1.UID_DOCUMENTO
                                , UidTabela = entity1.UID_TABELA
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get DocMultimidiaTabelaChildByEntitySearch.
	    public IQueryable<DocMultimidiaTabelaChild> GetDocMultimidiaTabelaChildByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(DocMultimidiaTabelaChild));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaTabelaChild> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_TABELA.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.DOC_MULTIMIDIA
	            
	            	
	            select new DocMultimidiaTabelaChild()		
	            {
	            
                IdChave = entity0.ID_CHAVE
                , OrdemApresentacao = entity0.ORDEM_APRESENTACAO
                , UidChave = entity0.UID_CHAVE
                , UidDocumento = entity0Al1.UID_DOCUMENTO
                , UidTabela = entity0.UID_TABELA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get DocMultimidiaByEntitySearchNoAssociations.
	    public IQueryable<DocMultimidia> GetDocMultimidiaByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(DocMultimidia));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<DocMultimidia> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.DOC_CLASSIFICADOR
	            
	            	
	            select new DocMultimidia()		
	            {
	            
                Conteudo = entity0.CONTEUDO
                , DataCriacao = entity0.DATA_CRIACAO
                , DescDocClassificador = entity0Al1.DESC_DOC_CLASSIFICADOR
                , DescDocumento = entity0.DESC_DOCUMENTO
                , IdDocClassificador = entity0Al1.ID_DOC_CLASSIFICADOR
                , LxTipoDocumento = entity0.LX_TIPO_DOCUMENTO
                , LxTipoExtensao = entity0.LX_TIPO_EXTENSAO
                , LxTipoMidia = entity0.LX_TIPO_MIDIA
                , NomeArquivo = entity0.NOME_ARQUIVO
                , Obs = entity0.OBS
                , TamanhoMidia = entity0.TAMANHO_MIDIA
                , Thumbnail = entity0.THUMBNAIL
                , TipoConteudoHttp = entity0.TIPO_CONTEUDO_HTTP
                , UidDocumento = entity0.UID_DOCUMENTO
                , Url = entity0.URL
                , XmlMapeamento = entity0.XML_MAPEAMENTO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get DocMultimidiaTabelaChildByEntitySearchNoAssociations.
	    public IQueryable<DocMultimidiaTabelaChild> GetDocMultimidiaTabelaChildByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(DocMultimidiaTabelaChild));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaTabelaChild> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_TABELA.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.DOC_MULTIMIDIA
	            
	            	
	            select new DocMultimidiaTabelaChild()		
	            {
	            
                IdChave = entity0.ID_CHAVE
                , OrdemApresentacao = entity0.ORDEM_APRESENTACAO
                , UidChave = entity0.UID_CHAVE
                , UidDocumento = entity0Al1.UID_DOCUMENTO
                , UidTabela = entity0.UID_TABELA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get DocMultimidiaTabelaChildParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<DocMultimidiaTabelaChildParentComposition> GetDocMultimidiaTabelaChildParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "DOC_MULTIMIDIA", "DOC_MULTIMIDIA_TABELA", "DOC_MULTIMIDIA", typeof(DocMultimidiaTabelaChildParentComposition));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaTabelaChildParentComposition> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_TABELA.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.DOC_MULTIMIDIA
	            
	            	
	            select new DocMultimidiaTabelaChildParentComposition()		
	            {
	            
                IdChave = entity0.ID_CHAVE
                , OrdemApresentacao = entity0.ORDEM_APRESENTACAO
                , UidChave = entity0.UID_CHAVE
                , UidDocumento = entity0Al1.UID_DOCUMENTO
                , UidTabela = entity0.UID_TABELA
                //DocMultimidia Properties.
                , Conteudo = entity0.DOC_MULTIMIDIA.CONTEUDO
                , DataCriacao = entity0.DOC_MULTIMIDIA.DATA_CRIACAO
                , DescDocClassificador = entity0.DOC_MULTIMIDIA.DOC_CLASSIFICADOR.DESC_DOC_CLASSIFICADOR
                , DescDocumento = entity0.DOC_MULTIMIDIA.DESC_DOCUMENTO
                , IdDocClassificador = entity0.DOC_MULTIMIDIA.DOC_CLASSIFICADOR.ID_DOC_CLASSIFICADOR
                , LxTipoDocumento = entity0.DOC_MULTIMIDIA.LX_TIPO_DOCUMENTO
                , LxTipoExtensao = entity0.DOC_MULTIMIDIA.LX_TIPO_EXTENSAO
                , LxTipoMidia = entity0.DOC_MULTIMIDIA.LX_TIPO_MIDIA
                , NomeArquivo = entity0.DOC_MULTIMIDIA.NOME_ARQUIVO
                , Obs = entity0.DOC_MULTIMIDIA.OBS
                , TamanhoMidia = entity0.DOC_MULTIMIDIA.TAMANHO_MIDIA
                , Thumbnail = entity0.DOC_MULTIMIDIA.THUMBNAIL
                , TipoConteudoHttp = entity0.DOC_MULTIMIDIA.TIPO_CONTEUDO_HTTP
                , Url = entity0.DOC_MULTIMIDIA.URL
                , XmlMapeamento = entity0.DOC_MULTIMIDIA.XML_MAPEAMENTO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get DocMultimidiaConfigByEntitySearch.
	    public IQueryable<DocMultimidiaConfig> GetDocMultimidiaConfigByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(DocMultimidiaConfig));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaConfig> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_CONFIG.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new DocMultimidiaConfig()		
	            {
	            
                DocAltura = entity0.DOC_ALTURA
                , DocDuracao = entity0.DOC_DURACAO
                , DocFormatoVisualizacao = entity0.DOC_FORMATO_VISUALIZACAO
                , DocLargura = entity0.DOC_LARGURA
                , DocTamanho = entity0.DOC_TAMANHO
                , IdTcsAplicativo = entity0.ID_TCS_APLICATIVO
                , LxUsoMultimidia = entity0.LX_USO_MULTIMIDIA
                , LxUsoMultimidiaName = ((entity0.LX_USO_MULTIMIDIA) == 1 ? "Catálogo" : ((entity0.LX_USO_MULTIMIDIA) == 2 ? "Detalhe" : ((entity0.LX_USO_MULTIMIDIA) == 9 ? "Look View" : ((entity0.LX_USO_MULTIMIDIA) == 8 ? "Matriz Mínima" : ((entity0.LX_USO_MULTIMIDIA) == 3 ? "Miniatura" : ((entity0.LX_USO_MULTIMIDIA) == 5 ? "Zoom Ampliado" : ((entity0.LX_USO_MULTIMIDIA) == 4 ? "Zoom de Lente" : "")))))))
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get DocMultimidiaConfigByEntitySearchNoAssociations.
	    public IQueryable<DocMultimidiaConfig> GetDocMultimidiaConfigByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(DocMultimidiaConfig));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaConfig> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_CONFIG.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new DocMultimidiaConfig()		
	            {
	            
                DocAltura = entity0.DOC_ALTURA
                , DocDuracao = entity0.DOC_DURACAO
                , DocFormatoVisualizacao = entity0.DOC_FORMATO_VISUALIZACAO
                , DocLargura = entity0.DOC_LARGURA
                , DocTamanho = entity0.DOC_TAMANHO
                , IdTcsAplicativo = entity0.ID_TCS_APLICATIVO
                , LxUsoMultimidia = entity0.LX_USO_MULTIMIDIA
                , LxUsoMultimidiaName = ((entity0.LX_USO_MULTIMIDIA) == 1 ? "Catálogo" : ((entity0.LX_USO_MULTIMIDIA) == 2 ? "Detalhe" : ((entity0.LX_USO_MULTIMIDIA) == 9 ? "Look View" : ((entity0.LX_USO_MULTIMIDIA) == 8 ? "Matriz Mínima" : ((entity0.LX_USO_MULTIMIDIA) == 3 ? "Miniatura" : ((entity0.LX_USO_MULTIMIDIA) == 5 ? "Zoom Ampliado" : ((entity0.LX_USO_MULTIMIDIA) == 4 ? "Zoom de Lente" : "")))))))
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get MediaElementByEntitySearch.
	    public IEnumerable<MediaElement> GetMediaElementByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<MediaElement> result = new List<MediaElement>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get MediaElementByEntitySearchNoAssociations.
	    public IEnumerable<MediaElement> GetMediaElementByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<MediaElement> result = new List<MediaElement>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get MediaConfigLengthByEntitySearch.
	    public IEnumerable<MediaConfigLength> GetMediaConfigLengthByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<MediaConfigLength> result = new List<MediaConfigLength>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get MediaConfigLengthByEntitySearchNoAssociations.
	    public IEnumerable<MediaConfigLength> GetMediaConfigLengthByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<MediaConfigLength> result = new List<MediaConfigLength>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get DocMultimidiaUploadByEntitySearch.
	    public IEnumerable<DocMultimidiaUpload> GetDocMultimidiaUploadByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<DocMultimidiaUpload> result = new List<DocMultimidiaUpload>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get DocMultimidiaUploadByEntitySearchNoAssociations.
	    public IEnumerable<DocMultimidiaUpload> GetDocMultimidiaUploadByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<DocMultimidiaUpload> result = new List<DocMultimidiaUpload>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get DocTabelaSyncByEntitySearch.
	    public IEnumerable<DocTabelaSync> GetDocTabelaSyncByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<DocTabelaSync> result = new List<DocTabelaSync>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get DocTabelaSyncByEntitySearchNoAssociations.
	    public IEnumerable<DocTabelaSync> GetDocTabelaSyncByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<DocTabelaSync> result = new List<DocTabelaSync>();
	  	
	
	        	

	
	        return result;
	    }
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get PagedDocMultimidiaTabela.
	    public IQueryable<DocMultimidiaTabela> GetPagedDocMultimidiaTabela(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(DocMultimidiaTabela));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaTabela> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_TABELA.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.DOC_MULTIMIDIA
                  let entity0Al2 = entity0.DOC_MULTIMIDIA.DOC_CLASSIFICADOR
                orderby entity0.ID_CHAVE ascending, entity0.UID_CHAVE ascending, entity0.UID_TABELA ascending, entity0Al1.UID_DOCUMENTO ascending
	            
	            	
	            select new DocMultimidiaTabela()		
	            {
	            
                Conteudo = entity0Al1.CONTEUDO
                , DataCriacao = entity0Al1.DATA_CRIACAO
                , DescDocClassificador = entity0Al2.DESC_DOC_CLASSIFICADOR
                , DescDocumento = entity0Al1.DESC_DOCUMENTO
                , IdChave = entity0.ID_CHAVE
                , IdDocClassificador = entity0Al2.ID_DOC_CLASSIFICADOR
                , LxTipoDocumento = entity0Al1.LX_TIPO_DOCUMENTO
                , LxTipoExtensao = entity0Al1.LX_TIPO_EXTENSAO
                , LxTipoMidia = entity0Al1.LX_TIPO_MIDIA
                , NomeArquivo = entity0Al1.NOME_ARQUIVO
                , Obs = entity0Al1.OBS
                , OrdemApresentacao = entity0.ORDEM_APRESENTACAO
                , TamanhoMidia = entity0Al1.TAMANHO_MIDIA
                , Thumbnail = entity0Al1.THUMBNAIL
                , TipoConteudoHttp = entity0Al1.TIPO_CONTEUDO_HTTP
                , UidChave = entity0.UID_CHAVE
                , UidDocumento = entity0Al1.UID_DOCUMENTO
                , UidTabela = entity0.UID_TABELA
                , Url = entity0Al1.URL
                , XmlMapeamento = entity0Al1.XML_MAPEAMENTO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetDocMultimidiaTabelaCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(DocMultimidiaTabela));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.DOC_MULTIMIDIA_TABELA.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.DOC_MULTIMIDIA
                  let entityAl2 = entity.DOC_MULTIMIDIA.DOC_CLASSIFICADOR
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedDocMultimidiaCompact.
	    public IQueryable<DocMultimidiaCompact> GetPagedDocMultimidiaCompact(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(DocMultimidiaCompact));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaCompact> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_TABELA.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.DOC_MULTIMIDIA
                orderby entity0.ID_CHAVE ascending, entity0.UID_CHAVE ascending, entity0.UID_TABELA ascending, entity0Al1.UID_DOCUMENTO ascending
	            
	            	
	            select new DocMultimidiaCompact()		
	            {
	            
                Conteudo = entity0Al1.CONTEUDO
                , DescDocumento = entity0Al1.DESC_DOCUMENTO
                , IdChave = entity0.ID_CHAVE
                , Thumbnail = entity0Al1.THUMBNAIL
                , UidChave = entity0.UID_CHAVE
                , UidDocumento = entity0Al1.UID_DOCUMENTO
                , UidTabela = entity0.UID_TABELA
                , Url = entity0Al1.URL
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetDocMultimidiaCompactCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(DocMultimidiaCompact));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.DOC_MULTIMIDIA_TABELA.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.DOC_MULTIMIDIA
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedMultimidiaCompact2BO.
	    public IQueryable<MultimidiaCompact2BO> GetPagedMultimidiaCompact2BO(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(MultimidiaCompact2BO));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<MultimidiaCompact2BO> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_TABELA.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.DOC_MULTIMIDIA
                  let entity0Al2 = entity0.DOC_MULTIMIDIA.DOC_CLASSIFICADOR
                orderby entity0.ID_CHAVE ascending, entity0.UID_CHAVE ascending, entity0.UID_TABELA ascending, entity0Al1.UID_DOCUMENTO ascending
	            
	            	
	            select new MultimidiaCompact2BO()		
	            {
	            
                Conteudo = entity0Al1.CONTEUDO
                , DescDocumento = entity0Al1.DESC_DOCUMENTO
                , IdChave = entity0.ID_CHAVE
                , IdDocClassificador = entity0Al2.ID_DOC_CLASSIFICADOR
                , LxTipoDocumento = entity0Al1.LX_TIPO_DOCUMENTO
                , LxTipoDocumentoName = ((entity0Al1.LX_TIPO_DOCUMENTO) == 3 ? "Detalhe/Estampa" : ((entity0Al1.LX_TIPO_DOCUMENTO) == 4 ? "360°" : ((entity0Al1.LX_TIPO_DOCUMENTO) == 2 ? "Matriz Para Transformação" : ((entity0Al1.LX_TIPO_DOCUMENTO) == 1 ? "Normal" : ((entity0Al1.LX_TIPO_DOCUMENTO) == 5 ? "Vídeos" : "")))))
                , LxTipoExtensao = entity0Al1.LX_TIPO_EXTENSAO
                , Obs = entity0Al1.OBS
                , OrdemApresentacao = entity0.ORDEM_APRESENTACAO
                , Thumbnail = entity0Al1.THUMBNAIL
                , UidChave = entity0.UID_CHAVE
                , UidDocumento = entity0Al1.UID_DOCUMENTO
                , UidTabela = entity0.UID_TABELA
                , Url = entity0Al1.URL
                , XmlMapeamento = entity0Al1.XML_MAPEAMENTO
                , DescTabela = ""
                , NomeTabela = ""
		
	            }
	            ).Skip(skip).Take(take);
	
	        SetMultimidiaCompact2BOBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetMultimidiaCompact2BOCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(MultimidiaCompact2BO));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.DOC_MULTIMIDIA_TABELA.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.DOC_MULTIMIDIA
                  let entityAl2 = entity.DOC_MULTIMIDIA.DOC_CLASSIFICADOR
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedDocMultimidiaUid.
	    public IQueryable<DocMultimidiaUid> GetPagedDocMultimidiaUid(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(DocMultimidiaUid));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaUid> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA.Where(dynQuery, parameters.ToArray())
                orderby entity0.UID_DOCUMENTO ascending
	            
	            	
	            select new DocMultimidiaUid()		
	            {
	            
                UidDocumento = entity0.UID_DOCUMENTO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetDocMultimidiaUidCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(DocMultimidiaUid));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.DOC_MULTIMIDIA.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedDocMultimidiaInfo.
	    public IEnumerable<DocMultimidiaInfo> GetPagedDocMultimidiaInfo(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<DocMultimidiaInfo> result = new List<DocMultimidiaInfo>();
	  	
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetDocMultimidiaInfoCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
			
	
	    
	    [Ignore]
	    //Get PagedDocMultimidia.
	    public IQueryable<DocMultimidia> GetPagedDocMultimidia(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(DocMultimidia));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<DocMultimidia> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.DOC_CLASSIFICADOR
                orderby entity0.UID_DOCUMENTO ascending
	            
	            	
	            select new DocMultimidia()		
	            {
	            
                Conteudo = entity0.CONTEUDO
                , DataCriacao = entity0.DATA_CRIACAO
                , DescDocClassificador = entity0Al1.DESC_DOC_CLASSIFICADOR
                , DescDocumento = entity0.DESC_DOCUMENTO
                , IdDocClassificador = entity0Al1.ID_DOC_CLASSIFICADOR
                , LxTipoDocumento = entity0.LX_TIPO_DOCUMENTO
                , LxTipoExtensao = entity0.LX_TIPO_EXTENSAO
                , LxTipoMidia = entity0.LX_TIPO_MIDIA
                , NomeArquivo = entity0.NOME_ARQUIVO
                , Obs = entity0.OBS
                , TamanhoMidia = entity0.TAMANHO_MIDIA
                , Thumbnail = entity0.THUMBNAIL
                , TipoConteudoHttp = entity0.TIPO_CONTEUDO_HTTP
                , UidDocumento = entity0.UID_DOCUMENTO
                , Url = entity0.URL
                , XmlMapeamento = entity0.XML_MAPEAMENTO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedDocMultimidiaTabelaChild.
	    public IQueryable<DocMultimidiaTabelaChild> GetPagedDocMultimidiaTabelaChild(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(DocMultimidiaTabelaChild));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaTabelaChild> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_TABELA.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.DOC_MULTIMIDIA
                orderby entity0.ID_CHAVE ascending, entity0.UID_CHAVE ascending, entity0.UID_TABELA ascending, entity0Al1.UID_DOCUMENTO ascending
	            
	            	
	            select new DocMultimidiaTabelaChild()		
	            {
	            
                IdChave = entity0.ID_CHAVE
                , OrdemApresentacao = entity0.ORDEM_APRESENTACAO
                , UidChave = entity0.UID_CHAVE
                , UidDocumento = entity0Al1.UID_DOCUMENTO
                , UidTabela = entity0.UID_TABELA
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetDocMultimidiaCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(DocMultimidia));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.DOC_MULTIMIDIA.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.DOC_CLASSIFICADOR
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetDocMultimidiaTabelaChildCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(DocMultimidiaTabelaChild));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.DOC_MULTIMIDIA_TABELA.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.DOC_MULTIMIDIA
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedDocMultimidiaConfig.
	    public IQueryable<DocMultimidiaConfig> GetPagedDocMultimidiaConfig(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(DocMultimidiaConfig));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaConfig> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_CONFIG.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_TCS_APLICATIVO ascending, entity0.LX_USO_MULTIMIDIA ascending
	            
	            	
	            select new DocMultimidiaConfig()		
	            {
	            
                DocAltura = entity0.DOC_ALTURA
                , DocDuracao = entity0.DOC_DURACAO
                , DocFormatoVisualizacao = entity0.DOC_FORMATO_VISUALIZACAO
                , DocLargura = entity0.DOC_LARGURA
                , DocTamanho = entity0.DOC_TAMANHO
                , IdTcsAplicativo = entity0.ID_TCS_APLICATIVO
                , LxUsoMultimidia = entity0.LX_USO_MULTIMIDIA
                , LxUsoMultimidiaName = ((entity0.LX_USO_MULTIMIDIA) == 1 ? "Catálogo" : ((entity0.LX_USO_MULTIMIDIA) == 2 ? "Detalhe" : ((entity0.LX_USO_MULTIMIDIA) == 9 ? "Look View" : ((entity0.LX_USO_MULTIMIDIA) == 8 ? "Matriz Mínima" : ((entity0.LX_USO_MULTIMIDIA) == 3 ? "Miniatura" : ((entity0.LX_USO_MULTIMIDIA) == 5 ? "Zoom Ampliado" : ((entity0.LX_USO_MULTIMIDIA) == 4 ? "Zoom de Lente" : "")))))))
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetDocMultimidiaConfigCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(DocMultimidiaConfig));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.DOC_MULTIMIDIA_CONFIG.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedMediaElement.
	    public IEnumerable<MediaElement> GetPagedMediaElement(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<MediaElement> result = new List<MediaElement>();
	  	
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetMediaElementCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
			
	
	    
	    [Ignore]
	    //Get PagedMediaConfigLength.
	    public IEnumerable<MediaConfigLength> GetPagedMediaConfigLength(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<MediaConfigLength> result = new List<MediaConfigLength>();
	  	
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetMediaConfigLengthCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
			
	
	    
	    [Ignore]
	    //Get PagedDocMultimidiaUpload.
	    public IEnumerable<DocMultimidiaUpload> GetPagedDocMultimidiaUpload(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<DocMultimidiaUpload> result = new List<DocMultimidiaUpload>();
	  	
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetDocMultimidiaUploadCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
			
	
	    
	    [Ignore]
	    //Get PagedDocTabelaSync.
	    public IEnumerable<DocTabelaSync> GetPagedDocTabelaSync(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<DocTabelaSync> result = new List<DocTabelaSync>();
	  	
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetDocTabelaSyncCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    
	    [Update()]	
	    //Update DocMultimidiaTabela.
	    public void UpdateDocMultimidiaTabela(DocMultimidiaTabela entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert DocMultimidiaTabela.
	    public void InsertDocMultimidiaTabela(DocMultimidiaTabela entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete DocMultimidiaTabela.
	    public void DeleteDocMultimidiaTabela(DocMultimidiaTabela entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update DocMultimidiaCompact.
	    public void UpdateDocMultimidiaCompact(DocMultimidiaCompact entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert DocMultimidiaCompact.
	    public void InsertDocMultimidiaCompact(DocMultimidiaCompact entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete DocMultimidiaCompact.
	    public void DeleteDocMultimidiaCompact(DocMultimidiaCompact entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update MultimidiaCompact2BO.
	    public void UpdateMultimidiaCompact2BO(MultimidiaCompact2BO entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, this.ChangeSet.GetOriginal(entity), ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert MultimidiaCompact2BO.
	    public void InsertMultimidiaCompact2BO(MultimidiaCompact2BO entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete MultimidiaCompact2BO.
	    public void DeleteMultimidiaCompact2BO(MultimidiaCompact2BO entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update DocMultimidiaUid.
	    public void UpdateDocMultimidiaUid(DocMultimidiaUid entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert DocMultimidiaUid.
	    public void InsertDocMultimidiaUid(DocMultimidiaUid entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete DocMultimidiaUid.
	    public void DeleteDocMultimidiaUid(DocMultimidiaUid entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update DocMultimidiaInfo.
	    public void UpdateDocMultimidiaInfo(DocMultimidiaInfo entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert DocMultimidiaInfo.
	    public void InsertDocMultimidiaInfo(DocMultimidiaInfo entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete DocMultimidiaInfo.
	    public void DeleteDocMultimidiaInfo(DocMultimidiaInfo entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update DocMultimidia.
	    public void UpdateDocMultimidia(DocMultimidia entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert DocMultimidia.
	    public void InsertDocMultimidia(DocMultimidia entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete DocMultimidia.
	    public void DeleteDocMultimidia(DocMultimidia entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update DocMultimidiaTabelaChild.
	    public void UpdateDocMultimidiaTabelaChild(DocMultimidiaTabelaChild entity)
	    {



	
	        if (entity.DocMultimidia.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.DocMultimidia) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.DocMultimidia); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert DocMultimidiaTabelaChild.
	    public void InsertDocMultimidiaTabelaChild(DocMultimidiaTabelaChild entity)
	    {



	
	        if (entity.DocMultimidia.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.DocMultimidia) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.DocMultimidia);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete DocMultimidiaTabelaChild.
	    public void DeleteDocMultimidiaTabelaChild(DocMultimidiaTabelaChild entity)
	    {



	
	        if (entity.DocMultimidia.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.DocMultimidia) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.DocMultimidia);
	            

	
	        }

	
	    }
		
			
	    
	    [Update()]	
	    //Update DocMultimidiaConfig.
	    public void UpdateDocMultimidiaConfig(DocMultimidiaConfig entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert DocMultimidiaConfig.
	    public void InsertDocMultimidiaConfig(DocMultimidiaConfig entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete DocMultimidiaConfig.
	    public void DeleteDocMultimidiaConfig(DocMultimidiaConfig entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update MediaElement.
	    public void UpdateMediaElement(MediaElement entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert MediaElement.
	    public void InsertMediaElement(MediaElement entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete MediaElement.
	    public void DeleteMediaElement(MediaElement entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update MediaConfigLength.
	    public void UpdateMediaConfigLength(MediaConfigLength entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert MediaConfigLength.
	    public void InsertMediaConfigLength(MediaConfigLength entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete MediaConfigLength.
	    public void DeleteMediaConfigLength(MediaConfigLength entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update DocMultimidiaUpload.
	    public void UpdateDocMultimidiaUpload(DocMultimidiaUpload entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert DocMultimidiaUpload.
	    public void InsertDocMultimidiaUpload(DocMultimidiaUpload entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete DocMultimidiaUpload.
	    public void DeleteDocMultimidiaUpload(DocMultimidiaUpload entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update DocTabelaSync.
	    public void UpdateDocTabelaSync(DocTabelaSync entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert DocTabelaSync.
	    public void InsertDocTabelaSync(DocTabelaSync entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete DocTabelaSync.
	    public void DeleteDocTabelaSync(DocTabelaSync entity)
	    {



	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}