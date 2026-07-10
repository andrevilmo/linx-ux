					
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

namespace Linx.Framework.BV.MultimidiaAutorizacao
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="DOC_MULTIMIDIA_AUTORIZACAO.UID_DOCUMENTO", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[DocMultimidiaAutorizacao,DocMultimidiaAutorizacao.DocMultimidiaTabelaAutorizacaoChild];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[UidDocumento];ReadOnly[false];Entities[DOC_MULTIMIDIA_AUTORIZACAO:UidDocumento];SubQueryInfo[];EdmEntityName[DOC_MULTIMIDIA_AUTORIZACAO];EntityRelations[DOC_MULTIMIDIA_AUTORIZACAO1(DOC_MULTIMIDIA_AUTORIZACAO)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "DocMultimidiaAutorizacao")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.MultimidiaAutorizacao.DocMultimidiaAutorizacao")]
	public partial class DocMultimidiaAutorizacao : Linx.Data.Entity
	{

	
		
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.DocMultimidiaTabelaAutorizacaoChildList != null && this.DocMultimidiaTabelaAutorizacaoChildList.Count() > 0)
	      {
	         foreach (var entity in this.DocMultimidiaTabelaAutorizacaoChildList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.DocMultimidiaTabelaAutorizacaoChildList != null)
	      {
	         foreach (var detail in this.DocMultimidiaTabelaAutorizacaoChildList)
	         {
	            detail.ResetDetails();
	         }
	         this.DocMultimidiaTabelaAutorizacaoChildList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(MultimidiaAutorizacaoDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("DocMultimidiaTabelaAutorizacaoChild"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("DocMultimidiaTabelaAutorizacaoChild");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "UidDocumento"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.UidDocumento));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load DocMultimidiaTabelaAutorizacaoChild and all sub-details
	         if (this.DocMultimidiaTabelaAutorizacaoChildList == null || this.DocMultimidiaTabelaAutorizacaoChildList.Count() == 0)
	         {
	             if (take > 0)
	                 this.DocMultimidiaTabelaAutorizacaoChildList = context.GetPagedDocMultimidiaTabelaAutorizacaoChild(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.DocMultimidiaTabelaAutorizacaoChildList = (from r in context.GetDocMultimidiaTabelaAutorizacaoChildByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _DocMultimidiaTabelaAutorizacaoChildElements = changeSet.ChangeSetEntries.Where(e => e.Entity is DocMultimidiaTabelaAutorizacaoChild && ((DocMultimidiaTabelaAutorizacaoChild)e.Entity).DocMultimidiaAutorizacao == null && e.Associations == null && e.OriginalAssociations == null && ((DocMultimidiaTabelaAutorizacaoChild)e.Entity).UidDocumento == this.UidDocumento).ToList();
 	      if (_DocMultimidiaTabelaAutorizacaoChildElements.Count > 0 && this.DocMultimidiaTabelaAutorizacaoChildList.Count() == 0)
 	      {
 	          this.DocMultimidiaTabelaAutorizacaoChildList = _DocMultimidiaTabelaAutorizacaoChildElements.Select(e => (DocMultimidiaTabelaAutorizacaoChild)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _DocMultimidiaTabelaAutorizacaoChildElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((DocMultimidiaTabelaAutorizacaoChild)detail.Entity).DocMultimidiaAutorizacao = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("DocMultimidiaAutorizacao", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("DocMultimidiaTabelaAutorizacaoChildList", indexDetails.ToArray());
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
	    [Display(Name = "Conteudo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_AUTORIZACAO.CONTEUDO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_AUTORIZACAO.CONTEUDO")]
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
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_AUTORIZACAO.DATA_CRIACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_AUTORIZACAO.DATA_CRIACAO")]
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
	    //Extensibility Partial Method Definitions For DescDocumento
	    partial void OnDescDocumentoChanging(System.String value);
	    partial void OnDescDocumentoChanged();

	    private System.String _DescDocumento;

	    [DataMember(IsRequired = true, Name = "DescDocumento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Desc Documento", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_AUTORIZACAO.DESC_DOCUMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_AUTORIZACAO.DESC_DOCUMENTO")]
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
	    partial void OnIdDocClassificadorChanging(Int32 value);
	    partial void OnIdDocClassificadorChanged();

	    private Int32 _IdDocClassificador;

	    [DataMember(IsRequired = true, Name = "IdDocClassificador", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Doc Classificador Fk", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_AUTORIZACAO.ID_DOC_CLASSIFICADOR_FK];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_AUTORIZACAO.ID_DOC_CLASSIFICADOR_FK")]
	    public Int32 IdDocClassificador
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
	    [Display(Name = "Lx Tipo Documento", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_AUTORIZACAO.LX_TIPO_DOCUMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_AUTORIZACAO.LX_TIPO_DOCUMENTO")]
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
	    [Display(Name = "Lx Tipo Extensao", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_AUTORIZACAO.LX_TIPO_EXTENSAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_AUTORIZACAO.LX_TIPO_EXTENSAO")]
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
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_AUTORIZACAO.LX_TIPO_MIDIA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_AUTORIZACAO.LX_TIPO_MIDIA")]
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
	    [FunctionalPoint("Precision[150:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_AUTORIZACAO.NOME_ARQUIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_AUTORIZACAO.NOME_ARQUIVO")]
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
	    [Display(Name = "Obs", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_AUTORIZACAO.OBS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_AUTORIZACAO.OBS")]
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
	    [Display(Name = "Tamanho Midia", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_AUTORIZACAO.TAMANHO_MIDIA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_AUTORIZACAO.TAMANHO_MIDIA")]
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
	    [Display(Name = "Thumbnail", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_AUTORIZACAO.THUMBNAIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_AUTORIZACAO.THUMBNAIL")]
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
	    [Display(Name = "Tipo Conteudo Http", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(100)]
	    [FunctionalPoint("Precision[100:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_AUTORIZACAO.TIPO_CONTEUDO_HTTP];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_AUTORIZACAO.TIPO_CONTEUDO_HTTP")]
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
	    [Display(Name = "Uid Documento", Description="", Order = 14, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_AUTORIZACAO.UID_DOCUMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_AUTORIZACAO.UID_DOCUMENTO")]
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
	    [Display(Name = "Url", Description="", Order = 15, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(500)]
	    [FunctionalPoint("Precision[500:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_AUTORIZACAO.URL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_AUTORIZACAO.URL")]
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
	    [Display(Name = "Xml Mapeamento", Description="", Order = 16, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_AUTORIZACAO.XML_MAPEAMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_AUTORIZACAO.XML_MAPEAMENTO")]
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
	    [Display(Name = "Uid Documento (Tmp)", Description="Temporary Key", Order = 14, AutoGenerateField = false, GroupName="", ResourceType= null)]
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
	 
		
	    private IEnumerable<DocMultimidiaTabelaAutorizacaoChild> _DocMultimidiaTabelaAutorizacaoChildList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_DocMultimidiaAutorizacao_DocMultimidiaTabelaAutorizacaoChild", "UidDocumento", "UidDocumento", IsForeignKey=false)]
	    [DataMember(Name = "DocMultimidiaTabelaAutorizacaoChildList", EmitDefaultValue = true)]
	    public IEnumerable<DocMultimidiaTabelaAutorizacaoChild> DocMultimidiaTabelaAutorizacaoChildList
	    {
	        get
	        {
	
	            if (this._DocMultimidiaTabelaAutorizacaoChildList == null)
	            	this._DocMultimidiaTabelaAutorizacaoChildList = new List<DocMultimidiaTabelaAutorizacaoChild>();
	
	            return this._DocMultimidiaTabelaAutorizacaoChildList;
	        }
	        set
	        {
	            if (this._DocMultimidiaTabelaAutorizacaoChildList != value)
	            {
	                this._DocMultimidiaTabelaAutorizacaoChildList = value;
	                this.RaisePropertyChanged("DocMultimidiaTabelaAutorizacaoChildList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.DOC_MULTIMIDIA_AUTORIZACAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = true, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.DOC_MULTIMIDIA_AUTORIZACAO), QualifiedEntitySetName = "AutorizacaoContext.DOC_MULTIMIDIA_AUTORIZACAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_AUTORIZACAO.OBS", Source = "Obs", Target = "OBS", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.DOC_MULTIMIDIA_AUTORIZACAO", RelationPropertyName = "DOC_MULTIMIDIA_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_AUTORIZACAO.URL", Source = "Url", Target = "URL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.DOC_MULTIMIDIA_AUTORIZACAO", RelationPropertyName = "DOC_MULTIMIDIA_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_AUTORIZACAO.CONTEUDO", Source = "Conteudo", Target = "CONTEUDO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.DOC_MULTIMIDIA_AUTORIZACAO", RelationPropertyName = "DOC_MULTIMIDIA_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_AUTORIZACAO.THUMBNAIL", Source = "Thumbnail", Target = "THUMBNAIL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.DOC_MULTIMIDIA_AUTORIZACAO", RelationPropertyName = "DOC_MULTIMIDIA_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_AUTORIZACAO.DATA_CRIACAO", Source = "DataCriacao", Target = "DATA_CRIACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.DOC_MULTIMIDIA_AUTORIZACAO", RelationPropertyName = "DOC_MULTIMIDIA_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_AUTORIZACAO.NOME_ARQUIVO", Source = "NomeArquivo", Target = "NOME_ARQUIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.DOC_MULTIMIDIA_AUTORIZACAO", RelationPropertyName = "DOC_MULTIMIDIA_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_AUTORIZACAO.LX_TIPO_MIDIA", Source = "LxTipoMidia", Target = "LX_TIPO_MIDIA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.DOC_MULTIMIDIA_AUTORIZACAO", RelationPropertyName = "DOC_MULTIMIDIA_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_AUTORIZACAO.TAMANHO_MIDIA", Source = "TamanhoMidia", Target = "TAMANHO_MIDIA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.DOC_MULTIMIDIA_AUTORIZACAO", RelationPropertyName = "DOC_MULTIMIDIA_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_AUTORIZACAO.UID_DOCUMENTO", Source = "UidDocumento", Target = "UID_DOCUMENTO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.DOC_MULTIMIDIA_AUTORIZACAO", RelationPropertyName = "DOC_MULTIMIDIA_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_AUTORIZACAO.DESC_DOCUMENTO", Source = "DescDocumento", Target = "DESC_DOCUMENTO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.DOC_MULTIMIDIA_AUTORIZACAO", RelationPropertyName = "DOC_MULTIMIDIA_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_AUTORIZACAO.XML_MAPEAMENTO", Source = "XmlMapeamento", Target = "XML_MAPEAMENTO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.DOC_MULTIMIDIA_AUTORIZACAO", RelationPropertyName = "DOC_MULTIMIDIA_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_AUTORIZACAO.LX_TIPO_EXTENSAO", Source = "LxTipoExtensao", Target = "LX_TIPO_EXTENSAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.DOC_MULTIMIDIA_AUTORIZACAO", RelationPropertyName = "DOC_MULTIMIDIA_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_AUTORIZACAO.LX_TIPO_DOCUMENTO", Source = "LxTipoDocumento", Target = "LX_TIPO_DOCUMENTO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.DOC_MULTIMIDIA_AUTORIZACAO", RelationPropertyName = "DOC_MULTIMIDIA_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_AUTORIZACAO.TIPO_CONTEUDO_HTTP", Source = "TipoConteudoHttp", Target = "TIPO_CONTEUDO_HTTP", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.DOC_MULTIMIDIA_AUTORIZACAO", RelationPropertyName = "DOC_MULTIMIDIA_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_AUTORIZACAO.ID_DOC_CLASSIFICADOR_FK", Source = "IdDocClassificador", Target = "ID_DOC_CLASSIFICADOR_FK", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.DOC_MULTIMIDIA_AUTORIZACAO", RelationPropertyName = "DOC_MULTIMIDIA_AUTORIZACAO" });

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

		

	[LinxPublicationView(PrimaryKeys="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.ID_CHAVE,DOC_MULTIMIDIA_TABELA_AUTORIZACAO.UID_CHAVE,DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.UID_DOCUMENTO,DOC_MULTIMIDIA_TABELA_AUTORIZACAO.UID_TABELA", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#.DOC_MULTIMIDIA_TABELA_AUTORIZACAO_LISTA as #Alias#];EdmEntityName[DOC_MULTIMIDIA_TABELA_AUTORIZACAO];EntityRelations[DOC_MULTIMIDIA_AUTORIZACAO(DOC_MULTIMIDIA_AUTORIZACAO)#DOC_MULTIMIDIA_AUTORIZACAO1(DOC_MULTIMIDIA_AUTORIZACAO)];EdmParentEntityName[DOC_MULTIMIDIA_AUTORIZACAO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "DocMultimidiaTabelaAutorizacaoChild")]
	[Serializable()]
	public partial class DocMultimidiaTabelaAutorizacaoChild : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(MultimidiaAutorizacaoDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("DocMultimidiaAutorizacao");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "UidDocumento"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.UidDocumento));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load DocMultimidiaAutorizacao
	         this.DocMultimidiaAutorizacao = (from r in context.GetDocMultimidiaAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	    [Display(Name = "Id Chave", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.ID_CHAVE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.ID_CHAVE")]
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
	    [Display(Name = "Ordem Apresentacao", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.ORDEM_APRESENTACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.ORDEM_APRESENTACAO")]
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
	    [Display(Name = "Uid Chave", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.UID_CHAVE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.UID_CHAVE")]
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
	    [Display(Name = "Uid Documento", Description="", Order = 14, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.UID_DOCUMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.UID_DOCUMENTO")]
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
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.UID_TABELA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.UID_TABELA")]
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
	 
	    private DocMultimidiaAutorizacao _DocMultimidiaAutorizacao;
	    [DataMember(Name = "DocMultimidiaAutorizacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_DocMultimidiaAutorizacao_DocMultimidiaTabelaAutorizacaoChild", "UidDocumento", "UidDocumento", IsForeignKey=true)]
	    public DocMultimidiaAutorizacao DocMultimidiaAutorizacao
	    {
	        get
	        {
	            return this._DocMultimidiaAutorizacao;
	        }
	        set
	        {
	            if (this._DocMultimidiaAutorizacao != value)
	            {
	                this._DocMultimidiaAutorizacao = value;
	                this.RaisePropertyChanged("DocMultimidiaAutorizacaoList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.DOC_MULTIMIDIA_TABELA_AUTORIZACAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = true, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.DOC_MULTIMIDIA_TABELA_AUTORIZACAO), QualifiedEntitySetName = "AutorizacaoContext.DOC_MULTIMIDIA_TABELA_AUTORIZACAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.ID_CHAVE", Source = "IdChave", Target = "ID_CHAVE", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.DOC_MULTIMIDIA_TABELA_AUTORIZACAO", RelationPropertyName = "DOC_MULTIMIDIA_TABELA_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.UID_CHAVE", Source = "UidChave", Target = "UID_CHAVE", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.DOC_MULTIMIDIA_TABELA_AUTORIZACAO", RelationPropertyName = "DOC_MULTIMIDIA_TABELA_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.UID_TABELA", Source = "UidTabela", Target = "UID_TABELA", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.DOC_MULTIMIDIA_TABELA_AUTORIZACAO", RelationPropertyName = "DOC_MULTIMIDIA_TABELA_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.ORDEM_APRESENTACAO", Source = "OrdemApresentacao", Target = "ORDEM_APRESENTACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.DOC_MULTIMIDIA_TABELA_AUTORIZACAO", RelationPropertyName = "DOC_MULTIMIDIA_TABELA_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.UID_DOCUMENTO", Source = "UidDocumento", Target = "UID_DOCUMENTO", TargetKeyName = "UID_DOCUMENTO", NoUpdatable = false, IsKey = true, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.DOC_MULTIMIDIA_AUTORIZACAO", RelationPropertyName = "DOC_MULTIMIDIA_AUTORIZACAO" });

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

		

	[LinxPublicationView(PrimaryKeys="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.ID_CHAVE,DOC_MULTIMIDIA_TABELA_AUTORIZACAO.UID_CHAVE,DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.UID_DOCUMENTO,DOC_MULTIMIDIA_TABELA_AUTORIZACAO.UID_TABELA", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[DocMultimidiaTabelaAutorizacao];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];Entities[DOC_MULTIMIDIA_AUTORIZACAO:UidDocumento];SubQueryInfo[];EdmEntityName[DOC_MULTIMIDIA_TABELA_AUTORIZACAO];EntityRelations[DOC_MULTIMIDIA_AUTORIZACAO(DOC_MULTIMIDIA_AUTORIZACAO)#DOC_MULTIMIDIA_AUTORIZACAO1(DOC_MULTIMIDIA_AUTORIZACAO)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "DocMultimidiaTabelaAutorizacao")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.MultimidiaAutorizacao.DocMultimidiaTabelaAutorizacao")]
	public partial class DocMultimidiaTabelaAutorizacao : Linx.Data.Entity
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
	    [Display(Name = "Conteudo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidiaAutorizacao];LookUpTitle[Seleção de (Conteudo)];LookUpQuery[executeLookUpDocMultimidiaAutorizacao];LookUpFinalize[finalizeLookUpDocMultimidiaAutorizacao];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DataCriacao\" : \"Data Criacao\", \"DescDocumento\" : \"Desc Documento\", \"IdDocClassificadorFk\" : \"Id Doc Classificador Fk\", \"LxTipoDocumento\" : \"Lx Tipo Documento\", \"LxTipoExtensao\" : \"Lx Tipo Extensao\", \"LxTipoMidia\" : \"Lx Tipo Midia\", \"NomeArquivo\" : \"Nome Arquivo\", \"Obs\" : \"Obs\", \"TamanhoMidia\" : \"Tamanho Midia\", \"Thumbnail\" : \"Thumbnail\", \"TipoConteudoHttp\" : \"Tipo Conteudo Http\", \"UidDocumento\" : \"Uid Documento\", \"Url\" : \"Url\", \"XmlMapeamento\" : \"Xml Mapeamento\", \"IdDocClassificador\" : \"Id Doc Classificador Fk\"}];LookUpColumns[{\"Conteudo\" : true, \"DataCriacao\" : true, \"DescDocumento\" : true, \"IdDocClassificadorFk\" : true, \"LxTipoDocumento\" : true, \"LxTipoExtensao\" : true, \"LxTipoMidia\" : true, \"NomeArquivo\" : true, \"Obs\" : true, \"TamanhoMidia\" : true, \"Thumbnail\" : true, \"TipoConteudoHttp\" : true, \"UidDocumento\" : true, \"Url\" : true, \"XmlMapeamento\" : true, \"IdDocClassificador\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.CONTEUDO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Byte[]#Conteudo#false##3:0##Conteudo#0#true##::LookUpDocMultimidiaAutorizacao##false#false#DOC_MULTIMIDIA_AUTORIZACAO#DOC_MULTIMIDIA_AUTORIZACAO#Linx.Framework.BV.MultimidiaAutorizacao#IQueryable###true#false", EdmKey="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.CONTEUDO")]
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
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidiaAutorizacao];LookUpTitle[Seleção de (Data Criacao)];LookUpQuery[executeLookUpDocMultimidiaAutorizacao];LookUpFinalize[finalizeLookUpDocMultimidiaAutorizacao];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DataCriacao\" : \"Data Criacao\", \"DescDocumento\" : \"Desc Documento\", \"IdDocClassificadorFk\" : \"Id Doc Classificador Fk\", \"LxTipoDocumento\" : \"Lx Tipo Documento\", \"LxTipoExtensao\" : \"Lx Tipo Extensao\", \"LxTipoMidia\" : \"Lx Tipo Midia\", \"NomeArquivo\" : \"Nome Arquivo\", \"Obs\" : \"Obs\", \"TamanhoMidia\" : \"Tamanho Midia\", \"Thumbnail\" : \"Thumbnail\", \"TipoConteudoHttp\" : \"Tipo Conteudo Http\", \"UidDocumento\" : \"Uid Documento\", \"Url\" : \"Url\", \"XmlMapeamento\" : \"Xml Mapeamento\", \"IdDocClassificador\" : \"Id Doc Classificador Fk\"}];LookUpColumns[{\"Conteudo\" : true, \"DataCriacao\" : true, \"DescDocumento\" : true, \"IdDocClassificadorFk\" : true, \"LxTipoDocumento\" : true, \"LxTipoExtensao\" : true, \"LxTipoMidia\" : true, \"NomeArquivo\" : true, \"Obs\" : true, \"TamanhoMidia\" : true, \"Thumbnail\" : true, \"TipoConteudoHttp\" : true, \"UidDocumento\" : true, \"Url\" : true, \"XmlMapeamento\" : true, \"IdDocClassificador\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.DATA_CRIACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.DateTime#DataCriacao#false##10:0##Data Criacao#1#true##::LookUpDocMultimidiaAutorizacao##false#false#DOC_MULTIMIDIA_AUTORIZACAO#DOC_MULTIMIDIA_AUTORIZACAO#Linx.Framework.BV.MultimidiaAutorizacao#IQueryable###true#false", EdmKey="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.DATA_CRIACAO")]
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
	    //Extensibility Partial Method Definitions For DescDocumento
	    partial void OnDescDocumentoChanging(System.String value);
	    partial void OnDescDocumentoChanged();

	    private System.String _DescDocumento;

	    [DataMember(IsRequired = true, Name = "DescDocumento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Desc Documento", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidiaAutorizacao];LookUpTitle[Seleção de (Desc Documento)];LookUpQuery[executeLookUpDocMultimidiaAutorizacao];LookUpFinalize[finalizeLookUpDocMultimidiaAutorizacao];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DataCriacao\" : \"Data Criacao\", \"DescDocumento\" : \"Desc Documento\", \"IdDocClassificadorFk\" : \"Id Doc Classificador Fk\", \"LxTipoDocumento\" : \"Lx Tipo Documento\", \"LxTipoExtensao\" : \"Lx Tipo Extensao\", \"LxTipoMidia\" : \"Lx Tipo Midia\", \"NomeArquivo\" : \"Nome Arquivo\", \"Obs\" : \"Obs\", \"TamanhoMidia\" : \"Tamanho Midia\", \"Thumbnail\" : \"Thumbnail\", \"TipoConteudoHttp\" : \"Tipo Conteudo Http\", \"UidDocumento\" : \"Uid Documento\", \"Url\" : \"Url\", \"XmlMapeamento\" : \"Xml Mapeamento\", \"IdDocClassificador\" : \"Id Doc Classificador Fk\"}];LookUpColumns[{\"Conteudo\" : true, \"DataCriacao\" : true, \"DescDocumento\" : true, \"IdDocClassificadorFk\" : true, \"LxTipoDocumento\" : true, \"LxTipoExtensao\" : true, \"LxTipoMidia\" : true, \"NomeArquivo\" : true, \"Obs\" : true, \"TamanhoMidia\" : true, \"Thumbnail\" : true, \"TipoConteudoHttp\" : true, \"UidDocumento\" : true, \"Url\" : true, \"XmlMapeamento\" : true, \"IdDocClassificador\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.DESC_DOCUMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescDocumento#false##60:0##Desc Documento#2#true##::LookUpDocMultimidiaAutorizacao##false#false#DOC_MULTIMIDIA_AUTORIZACAO#DOC_MULTIMIDIA_AUTORIZACAO#Linx.Framework.BV.MultimidiaAutorizacao#IQueryable###true#false", EdmKey="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.DESC_DOCUMENTO")]
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
	    [Display(Name = "Id Chave", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.ID_CHAVE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.ID_CHAVE")]
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
	    partial void OnIdDocClassificadorChanging(Int32 value);
	    partial void OnIdDocClassificadorChanged();

	    private Int32 _IdDocClassificador;

	    [DataMember(IsRequired = true, Name = "IdDocClassificador", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Doc Classificador Fk", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidiaAutorizacao];LookUpTitle[Seleção de (Id Doc Classificador Fk)];LookUpQuery[executeLookUpDocMultimidiaAutorizacao];LookUpFinalize[finalizeLookUpDocMultimidiaAutorizacao];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DataCriacao\" : \"Data Criacao\", \"DescDocumento\" : \"Desc Documento\", \"IdDocClassificadorFk\" : \"Id Doc Classificador Fk\", \"LxTipoDocumento\" : \"Lx Tipo Documento\", \"LxTipoExtensao\" : \"Lx Tipo Extensao\", \"LxTipoMidia\" : \"Lx Tipo Midia\", \"NomeArquivo\" : \"Nome Arquivo\", \"Obs\" : \"Obs\", \"TamanhoMidia\" : \"Tamanho Midia\", \"Thumbnail\" : \"Thumbnail\", \"TipoConteudoHttp\" : \"Tipo Conteudo Http\", \"UidDocumento\" : \"Uid Documento\", \"Url\" : \"Url\", \"XmlMapeamento\" : \"Xml Mapeamento\", \"IdDocClassificador\" : \"Id Doc Classificador Fk\"}];LookUpColumns[{\"Conteudo\" : true, \"DataCriacao\" : true, \"DescDocumento\" : true, \"IdDocClassificadorFk\" : true, \"LxTipoDocumento\" : true, \"LxTipoExtensao\" : true, \"LxTipoMidia\" : true, \"NomeArquivo\" : true, \"Obs\" : true, \"TamanhoMidia\" : true, \"Thumbnail\" : true, \"TipoConteudoHttp\" : true, \"UidDocumento\" : true, \"Url\" : true, \"XmlMapeamento\" : true, \"IdDocClassificador\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.ID_DOC_CLASSIFICADOR_FK];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdDocClassificador#false##12:0##Id Doc Classificador Fk#15#true##::LookUpDocMultimidiaAutorizacao##false#false#DOC_MULTIMIDIA_AUTORIZACAO#DOC_MULTIMIDIA_AUTORIZACAO#Linx.Framework.BV.MultimidiaAutorizacao#IQueryable###true#false", EdmKey="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.ID_DOC_CLASSIFICADOR_FK")]
	    public Int32 IdDocClassificador
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
	    [Display(Name = "Lx Tipo Documento", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidiaAutorizacao];LookUpTitle[Seleção de (Lx Tipo Documento)];LookUpQuery[executeLookUpDocMultimidiaAutorizacao];LookUpFinalize[finalizeLookUpDocMultimidiaAutorizacao];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DataCriacao\" : \"Data Criacao\", \"DescDocumento\" : \"Desc Documento\", \"IdDocClassificadorFk\" : \"Id Doc Classificador Fk\", \"LxTipoDocumento\" : \"Lx Tipo Documento\", \"LxTipoExtensao\" : \"Lx Tipo Extensao\", \"LxTipoMidia\" : \"Lx Tipo Midia\", \"NomeArquivo\" : \"Nome Arquivo\", \"Obs\" : \"Obs\", \"TamanhoMidia\" : \"Tamanho Midia\", \"Thumbnail\" : \"Thumbnail\", \"TipoConteudoHttp\" : \"Tipo Conteudo Http\", \"UidDocumento\" : \"Uid Documento\", \"Url\" : \"Url\", \"XmlMapeamento\" : \"Xml Mapeamento\", \"IdDocClassificador\" : \"Id Doc Classificador Fk\"}];LookUpColumns[{\"Conteudo\" : true, \"DataCriacao\" : true, \"DescDocumento\" : true, \"IdDocClassificadorFk\" : true, \"LxTipoDocumento\" : true, \"LxTipoExtensao\" : true, \"LxTipoMidia\" : true, \"NomeArquivo\" : true, \"Obs\" : true, \"TamanhoMidia\" : true, \"Thumbnail\" : true, \"TipoConteudoHttp\" : true, \"UidDocumento\" : true, \"Url\" : true, \"XmlMapeamento\" : true, \"IdDocClassificador\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.LX_TIPO_DOCUMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Byte#LxTipoDocumento#false##3:0##Lx Tipo Documento#4#true##::LookUpDocMultimidiaAutorizacao##false#false#DOC_MULTIMIDIA_AUTORIZACAO#DOC_MULTIMIDIA_AUTORIZACAO#Linx.Framework.BV.MultimidiaAutorizacao#IQueryable###true#false", EdmKey="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.LX_TIPO_DOCUMENTO")]
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
	    [Display(Name = "Lx Tipo Extensao", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidiaAutorizacao];LookUpTitle[Seleção de (Lx Tipo Extensao)];LookUpQuery[executeLookUpDocMultimidiaAutorizacao];LookUpFinalize[finalizeLookUpDocMultimidiaAutorizacao];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DataCriacao\" : \"Data Criacao\", \"DescDocumento\" : \"Desc Documento\", \"IdDocClassificadorFk\" : \"Id Doc Classificador Fk\", \"LxTipoDocumento\" : \"Lx Tipo Documento\", \"LxTipoExtensao\" : \"Lx Tipo Extensao\", \"LxTipoMidia\" : \"Lx Tipo Midia\", \"NomeArquivo\" : \"Nome Arquivo\", \"Obs\" : \"Obs\", \"TamanhoMidia\" : \"Tamanho Midia\", \"Thumbnail\" : \"Thumbnail\", \"TipoConteudoHttp\" : \"Tipo Conteudo Http\", \"UidDocumento\" : \"Uid Documento\", \"Url\" : \"Url\", \"XmlMapeamento\" : \"Xml Mapeamento\", \"IdDocClassificador\" : \"Id Doc Classificador Fk\"}];LookUpColumns[{\"Conteudo\" : true, \"DataCriacao\" : true, \"DescDocumento\" : true, \"IdDocClassificadorFk\" : true, \"LxTipoDocumento\" : true, \"LxTipoExtensao\" : true, \"LxTipoMidia\" : true, \"NomeArquivo\" : true, \"Obs\" : true, \"TamanhoMidia\" : true, \"Thumbnail\" : true, \"TipoConteudoHttp\" : true, \"UidDocumento\" : true, \"Url\" : true, \"XmlMapeamento\" : true, \"IdDocClassificador\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.LX_TIPO_EXTENSAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Byte#LxTipoExtensao#false##3:0##Lx Tipo Extensao#5#true##::LookUpDocMultimidiaAutorizacao##false#false#DOC_MULTIMIDIA_AUTORIZACAO#DOC_MULTIMIDIA_AUTORIZACAO#Linx.Framework.BV.MultimidiaAutorizacao#IQueryable###true#false", EdmKey="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.LX_TIPO_EXTENSAO")]
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
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidiaAutorizacao];LookUpTitle[Seleção de (Lx Tipo Midia)];LookUpQuery[executeLookUpDocMultimidiaAutorizacao];LookUpFinalize[finalizeLookUpDocMultimidiaAutorizacao];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DataCriacao\" : \"Data Criacao\", \"DescDocumento\" : \"Desc Documento\", \"IdDocClassificadorFk\" : \"Id Doc Classificador Fk\", \"LxTipoDocumento\" : \"Lx Tipo Documento\", \"LxTipoExtensao\" : \"Lx Tipo Extensao\", \"LxTipoMidia\" : \"Lx Tipo Midia\", \"NomeArquivo\" : \"Nome Arquivo\", \"Obs\" : \"Obs\", \"TamanhoMidia\" : \"Tamanho Midia\", \"Thumbnail\" : \"Thumbnail\", \"TipoConteudoHttp\" : \"Tipo Conteudo Http\", \"UidDocumento\" : \"Uid Documento\", \"Url\" : \"Url\", \"XmlMapeamento\" : \"Xml Mapeamento\", \"IdDocClassificador\" : \"Id Doc Classificador Fk\"}];LookUpColumns[{\"Conteudo\" : true, \"DataCriacao\" : true, \"DescDocumento\" : true, \"IdDocClassificadorFk\" : true, \"LxTipoDocumento\" : true, \"LxTipoExtensao\" : true, \"LxTipoMidia\" : true, \"NomeArquivo\" : true, \"Obs\" : true, \"TamanhoMidia\" : true, \"Thumbnail\" : true, \"TipoConteudoHttp\" : true, \"UidDocumento\" : true, \"Url\" : true, \"XmlMapeamento\" : true, \"IdDocClassificador\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.LX_TIPO_MIDIA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Byte#LxTipoMidia#false##3:0##Lx Tipo Midia#6#true##::LookUpDocMultimidiaAutorizacao##false#false#DOC_MULTIMIDIA_AUTORIZACAO#DOC_MULTIMIDIA_AUTORIZACAO#Linx.Framework.BV.MultimidiaAutorizacao#IQueryable###true#false", EdmKey="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.LX_TIPO_MIDIA")]
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
	    [FunctionalPoint("Precision[150:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidiaAutorizacao];LookUpTitle[Seleção de (Nome Arquivo)];LookUpQuery[executeLookUpDocMultimidiaAutorizacao];LookUpFinalize[finalizeLookUpDocMultimidiaAutorizacao];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DataCriacao\" : \"Data Criacao\", \"DescDocumento\" : \"Desc Documento\", \"IdDocClassificadorFk\" : \"Id Doc Classificador Fk\", \"LxTipoDocumento\" : \"Lx Tipo Documento\", \"LxTipoExtensao\" : \"Lx Tipo Extensao\", \"LxTipoMidia\" : \"Lx Tipo Midia\", \"NomeArquivo\" : \"Nome Arquivo\", \"Obs\" : \"Obs\", \"TamanhoMidia\" : \"Tamanho Midia\", \"Thumbnail\" : \"Thumbnail\", \"TipoConteudoHttp\" : \"Tipo Conteudo Http\", \"UidDocumento\" : \"Uid Documento\", \"Url\" : \"Url\", \"XmlMapeamento\" : \"Xml Mapeamento\", \"IdDocClassificador\" : \"Id Doc Classificador Fk\"}];LookUpColumns[{\"Conteudo\" : true, \"DataCriacao\" : true, \"DescDocumento\" : true, \"IdDocClassificadorFk\" : true, \"LxTipoDocumento\" : true, \"LxTipoExtensao\" : true, \"LxTipoMidia\" : true, \"NomeArquivo\" : true, \"Obs\" : true, \"TamanhoMidia\" : true, \"Thumbnail\" : true, \"TipoConteudoHttp\" : true, \"UidDocumento\" : true, \"Url\" : true, \"XmlMapeamento\" : true, \"IdDocClassificador\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.NOME_ARQUIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeArquivo#false##150:0##Nome Arquivo#7#true##::LookUpDocMultimidiaAutorizacao##false#false#DOC_MULTIMIDIA_AUTORIZACAO#DOC_MULTIMIDIA_AUTORIZACAO#Linx.Framework.BV.MultimidiaAutorizacao#IQueryable###true#false", EdmKey="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.NOME_ARQUIVO")]
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
	    [Display(Name = "Obs", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidiaAutorizacao];LookUpTitle[Seleção de (Obs)];LookUpQuery[executeLookUpDocMultimidiaAutorizacao];LookUpFinalize[finalizeLookUpDocMultimidiaAutorizacao];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DataCriacao\" : \"Data Criacao\", \"DescDocumento\" : \"Desc Documento\", \"IdDocClassificadorFk\" : \"Id Doc Classificador Fk\", \"LxTipoDocumento\" : \"Lx Tipo Documento\", \"LxTipoExtensao\" : \"Lx Tipo Extensao\", \"LxTipoMidia\" : \"Lx Tipo Midia\", \"NomeArquivo\" : \"Nome Arquivo\", \"Obs\" : \"Obs\", \"TamanhoMidia\" : \"Tamanho Midia\", \"Thumbnail\" : \"Thumbnail\", \"TipoConteudoHttp\" : \"Tipo Conteudo Http\", \"UidDocumento\" : \"Uid Documento\", \"Url\" : \"Url\", \"XmlMapeamento\" : \"Xml Mapeamento\", \"IdDocClassificador\" : \"Id Doc Classificador Fk\"}];LookUpColumns[{\"Conteudo\" : true, \"DataCriacao\" : true, \"DescDocumento\" : true, \"IdDocClassificadorFk\" : true, \"LxTipoDocumento\" : true, \"LxTipoExtensao\" : true, \"LxTipoMidia\" : true, \"NomeArquivo\" : true, \"Obs\" : true, \"TamanhoMidia\" : true, \"Thumbnail\" : true, \"TipoConteudoHttp\" : true, \"UidDocumento\" : true, \"Url\" : true, \"XmlMapeamento\" : true, \"IdDocClassificador\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.OBS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#Obs#false##0:0##Obs#8#true##::LookUpDocMultimidiaAutorizacao##false#false#DOC_MULTIMIDIA_AUTORIZACAO#DOC_MULTIMIDIA_AUTORIZACAO#Linx.Framework.BV.MultimidiaAutorizacao#IQueryable###true#false", EdmKey="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.OBS")]
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
	    [Display(Name = "Ordem Apresentacao", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.ORDEM_APRESENTACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.ORDEM_APRESENTACAO")]
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
	    [Display(Name = "Tamanho Midia", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidiaAutorizacao];LookUpTitle[Seleção de (Tamanho Midia)];LookUpQuery[executeLookUpDocMultimidiaAutorizacao];LookUpFinalize[finalizeLookUpDocMultimidiaAutorizacao];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DataCriacao\" : \"Data Criacao\", \"DescDocumento\" : \"Desc Documento\", \"IdDocClassificadorFk\" : \"Id Doc Classificador Fk\", \"LxTipoDocumento\" : \"Lx Tipo Documento\", \"LxTipoExtensao\" : \"Lx Tipo Extensao\", \"LxTipoMidia\" : \"Lx Tipo Midia\", \"NomeArquivo\" : \"Nome Arquivo\", \"Obs\" : \"Obs\", \"TamanhoMidia\" : \"Tamanho Midia\", \"Thumbnail\" : \"Thumbnail\", \"TipoConteudoHttp\" : \"Tipo Conteudo Http\", \"UidDocumento\" : \"Uid Documento\", \"Url\" : \"Url\", \"XmlMapeamento\" : \"Xml Mapeamento\", \"IdDocClassificador\" : \"Id Doc Classificador Fk\"}];LookUpColumns[{\"Conteudo\" : true, \"DataCriacao\" : true, \"DescDocumento\" : true, \"IdDocClassificadorFk\" : true, \"LxTipoDocumento\" : true, \"LxTipoExtensao\" : true, \"LxTipoMidia\" : true, \"NomeArquivo\" : true, \"Obs\" : true, \"TamanhoMidia\" : true, \"Thumbnail\" : true, \"TipoConteudoHttp\" : true, \"UidDocumento\" : true, \"Url\" : true, \"XmlMapeamento\" : true, \"IdDocClassificador\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.TAMANHO_MIDIA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<System.Int32>#TamanhoMidia#false##12:0##Tamanho Midia#9#true##::LookUpDocMultimidiaAutorizacao##false#false#DOC_MULTIMIDIA_AUTORIZACAO#DOC_MULTIMIDIA_AUTORIZACAO#Linx.Framework.BV.MultimidiaAutorizacao#IQueryable###true#false", EdmKey="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.TAMANHO_MIDIA")]
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
	    [Display(Name = "Thumbnail", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidiaAutorizacao];LookUpTitle[Seleção de (Thumbnail)];LookUpQuery[executeLookUpDocMultimidiaAutorizacao];LookUpFinalize[finalizeLookUpDocMultimidiaAutorizacao];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DataCriacao\" : \"Data Criacao\", \"DescDocumento\" : \"Desc Documento\", \"IdDocClassificadorFk\" : \"Id Doc Classificador Fk\", \"LxTipoDocumento\" : \"Lx Tipo Documento\", \"LxTipoExtensao\" : \"Lx Tipo Extensao\", \"LxTipoMidia\" : \"Lx Tipo Midia\", \"NomeArquivo\" : \"Nome Arquivo\", \"Obs\" : \"Obs\", \"TamanhoMidia\" : \"Tamanho Midia\", \"Thumbnail\" : \"Thumbnail\", \"TipoConteudoHttp\" : \"Tipo Conteudo Http\", \"UidDocumento\" : \"Uid Documento\", \"Url\" : \"Url\", \"XmlMapeamento\" : \"Xml Mapeamento\", \"IdDocClassificador\" : \"Id Doc Classificador Fk\"}];LookUpColumns[{\"Conteudo\" : true, \"DataCriacao\" : true, \"DescDocumento\" : true, \"IdDocClassificadorFk\" : true, \"LxTipoDocumento\" : true, \"LxTipoExtensao\" : true, \"LxTipoMidia\" : true, \"NomeArquivo\" : true, \"Obs\" : true, \"TamanhoMidia\" : true, \"Thumbnail\" : true, \"TipoConteudoHttp\" : true, \"UidDocumento\" : true, \"Url\" : true, \"XmlMapeamento\" : true, \"IdDocClassificador\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.THUMBNAIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Byte[]#Thumbnail#false##3:0##Thumbnail#10#true##::LookUpDocMultimidiaAutorizacao##false#false#DOC_MULTIMIDIA_AUTORIZACAO#DOC_MULTIMIDIA_AUTORIZACAO#Linx.Framework.BV.MultimidiaAutorizacao#IQueryable###true#false", EdmKey="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.THUMBNAIL")]
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
	    [Display(Name = "Tipo Conteudo Http", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(100)]
	    [FunctionalPoint("Precision[100:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidiaAutorizacao];LookUpTitle[Seleção de (Tipo Conteudo Http)];LookUpQuery[executeLookUpDocMultimidiaAutorizacao];LookUpFinalize[finalizeLookUpDocMultimidiaAutorizacao];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DataCriacao\" : \"Data Criacao\", \"DescDocumento\" : \"Desc Documento\", \"IdDocClassificadorFk\" : \"Id Doc Classificador Fk\", \"LxTipoDocumento\" : \"Lx Tipo Documento\", \"LxTipoExtensao\" : \"Lx Tipo Extensao\", \"LxTipoMidia\" : \"Lx Tipo Midia\", \"NomeArquivo\" : \"Nome Arquivo\", \"Obs\" : \"Obs\", \"TamanhoMidia\" : \"Tamanho Midia\", \"Thumbnail\" : \"Thumbnail\", \"TipoConteudoHttp\" : \"Tipo Conteudo Http\", \"UidDocumento\" : \"Uid Documento\", \"Url\" : \"Url\", \"XmlMapeamento\" : \"Xml Mapeamento\", \"IdDocClassificador\" : \"Id Doc Classificador Fk\"}];LookUpColumns[{\"Conteudo\" : true, \"DataCriacao\" : true, \"DescDocumento\" : true, \"IdDocClassificadorFk\" : true, \"LxTipoDocumento\" : true, \"LxTipoExtensao\" : true, \"LxTipoMidia\" : true, \"NomeArquivo\" : true, \"Obs\" : true, \"TamanhoMidia\" : true, \"Thumbnail\" : true, \"TipoConteudoHttp\" : true, \"UidDocumento\" : true, \"Url\" : true, \"XmlMapeamento\" : true, \"IdDocClassificador\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.TIPO_CONTEUDO_HTTP];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#TipoConteudoHttp#false##100:0##Tipo Conteudo Http#11#true##::LookUpDocMultimidiaAutorizacao##false#false#DOC_MULTIMIDIA_AUTORIZACAO#DOC_MULTIMIDIA_AUTORIZACAO#Linx.Framework.BV.MultimidiaAutorizacao#IQueryable###true#false", EdmKey="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.TIPO_CONTEUDO_HTTP")]
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
	    [Display(Name = "Uid Chave", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.UID_CHAVE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.UID_CHAVE")]
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
	    [Display(Name = "Uid Documento", Description="", Order = 14, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidiaAutorizacao];LookUpTitle[Seleção de (Uid Documento)];LookUpQuery[executeLookUpDocMultimidiaAutorizacao];LookUpFinalize[finalizeLookUpDocMultimidiaAutorizacao];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DataCriacao\" : \"Data Criacao\", \"DescDocumento\" : \"Desc Documento\", \"IdDocClassificadorFk\" : \"Id Doc Classificador Fk\", \"LxTipoDocumento\" : \"Lx Tipo Documento\", \"LxTipoExtensao\" : \"Lx Tipo Extensao\", \"LxTipoMidia\" : \"Lx Tipo Midia\", \"NomeArquivo\" : \"Nome Arquivo\", \"Obs\" : \"Obs\", \"TamanhoMidia\" : \"Tamanho Midia\", \"Thumbnail\" : \"Thumbnail\", \"TipoConteudoHttp\" : \"Tipo Conteudo Http\", \"UidDocumento\" : \"Uid Documento\", \"Url\" : \"Url\", \"XmlMapeamento\" : \"Xml Mapeamento\", \"IdDocClassificador\" : \"Id Doc Classificador Fk\"}];LookUpColumns[{\"Conteudo\" : true, \"DataCriacao\" : true, \"DescDocumento\" : true, \"IdDocClassificadorFk\" : true, \"LxTipoDocumento\" : true, \"LxTipoExtensao\" : true, \"LxTipoMidia\" : true, \"NomeArquivo\" : true, \"Obs\" : true, \"TamanhoMidia\" : true, \"Thumbnail\" : true, \"TipoConteudoHttp\" : true, \"UidDocumento\" : true, \"Url\" : true, \"XmlMapeamento\" : true, \"IdDocClassificador\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.UID_DOCUMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Guid#UidDocumento#true##12:0##Uid Documento#12#true##::LookUpDocMultimidiaAutorizacao##false#false#DOC_MULTIMIDIA_AUTORIZACAO#DOC_MULTIMIDIA_AUTORIZACAO#Linx.Framework.BV.MultimidiaAutorizacao#IQueryable###true#false", EdmKey="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.UID_DOCUMENTO")]
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
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.UID_TABELA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.UID_TABELA")]
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
	    [Display(Name = "Url", Description="", Order = 15, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(500)]
	    [FunctionalPoint("Precision[500:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidiaAutorizacao];LookUpTitle[Seleção de (Url)];LookUpQuery[executeLookUpDocMultimidiaAutorizacao];LookUpFinalize[finalizeLookUpDocMultimidiaAutorizacao];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DataCriacao\" : \"Data Criacao\", \"DescDocumento\" : \"Desc Documento\", \"IdDocClassificadorFk\" : \"Id Doc Classificador Fk\", \"LxTipoDocumento\" : \"Lx Tipo Documento\", \"LxTipoExtensao\" : \"Lx Tipo Extensao\", \"LxTipoMidia\" : \"Lx Tipo Midia\", \"NomeArquivo\" : \"Nome Arquivo\", \"Obs\" : \"Obs\", \"TamanhoMidia\" : \"Tamanho Midia\", \"Thumbnail\" : \"Thumbnail\", \"TipoConteudoHttp\" : \"Tipo Conteudo Http\", \"UidDocumento\" : \"Uid Documento\", \"Url\" : \"Url\", \"XmlMapeamento\" : \"Xml Mapeamento\", \"IdDocClassificador\" : \"Id Doc Classificador Fk\"}];LookUpColumns[{\"Conteudo\" : true, \"DataCriacao\" : true, \"DescDocumento\" : true, \"IdDocClassificadorFk\" : true, \"LxTipoDocumento\" : true, \"LxTipoExtensao\" : true, \"LxTipoMidia\" : true, \"NomeArquivo\" : true, \"Obs\" : true, \"TamanhoMidia\" : true, \"Thumbnail\" : true, \"TipoConteudoHttp\" : true, \"UidDocumento\" : true, \"Url\" : true, \"XmlMapeamento\" : true, \"IdDocClassificador\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.URL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#Url#false##500:0##Url#13#true##::LookUpDocMultimidiaAutorizacao##false#false#DOC_MULTIMIDIA_AUTORIZACAO#DOC_MULTIMIDIA_AUTORIZACAO#Linx.Framework.BV.MultimidiaAutorizacao#IQueryable###true#false", EdmKey="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.URL")]
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
	    [Display(Name = "Xml Mapeamento", Description="", Order = 16, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpDocMultimidiaAutorizacao];LookUpTitle[Seleção de (Xml Mapeamento)];LookUpQuery[executeLookUpDocMultimidiaAutorizacao];LookUpFinalize[finalizeLookUpDocMultimidiaAutorizacao];LookUpDisplayColumns[{\"Conteudo\" : \"Conteudo\", \"DataCriacao\" : \"Data Criacao\", \"DescDocumento\" : \"Desc Documento\", \"IdDocClassificadorFk\" : \"Id Doc Classificador Fk\", \"LxTipoDocumento\" : \"Lx Tipo Documento\", \"LxTipoExtensao\" : \"Lx Tipo Extensao\", \"LxTipoMidia\" : \"Lx Tipo Midia\", \"NomeArquivo\" : \"Nome Arquivo\", \"Obs\" : \"Obs\", \"TamanhoMidia\" : \"Tamanho Midia\", \"Thumbnail\" : \"Thumbnail\", \"TipoConteudoHttp\" : \"Tipo Conteudo Http\", \"UidDocumento\" : \"Uid Documento\", \"Url\" : \"Url\", \"XmlMapeamento\" : \"Xml Mapeamento\", \"IdDocClassificador\" : \"Id Doc Classificador Fk\"}];LookUpColumns[{\"Conteudo\" : true, \"DataCriacao\" : true, \"DescDocumento\" : true, \"IdDocClassificadorFk\" : true, \"LxTipoDocumento\" : true, \"LxTipoExtensao\" : true, \"LxTipoMidia\" : true, \"NomeArquivo\" : true, \"Obs\" : true, \"TamanhoMidia\" : true, \"Thumbnail\" : true, \"TipoConteudoHttp\" : true, \"UidDocumento\" : true, \"Url\" : true, \"XmlMapeamento\" : true, \"IdDocClassificador\" : true}];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.XML_MAPEAMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#XmlMapeamento#false##0:0##Xml Mapeamento#14#true##::LookUpDocMultimidiaAutorizacao##false#false#DOC_MULTIMIDIA_AUTORIZACAO#DOC_MULTIMIDIA_AUTORIZACAO#Linx.Framework.BV.MultimidiaAutorizacao#IQueryable###true#false", EdmKey="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.XML_MAPEAMENTO")]
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
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.DOC_MULTIMIDIA_TABELA_AUTORIZACAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = true, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.DOC_MULTIMIDIA_TABELA_AUTORIZACAO), QualifiedEntitySetName = "AutorizacaoContext.DOC_MULTIMIDIA_TABELA_AUTORIZACAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.ID_CHAVE", Source = "IdChave", Target = "ID_CHAVE", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.DOC_MULTIMIDIA_TABELA_AUTORIZACAO", RelationPropertyName = "DOC_MULTIMIDIA_TABELA_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.UID_CHAVE", Source = "UidChave", Target = "UID_CHAVE", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.DOC_MULTIMIDIA_TABELA_AUTORIZACAO", RelationPropertyName = "DOC_MULTIMIDIA_TABELA_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.UID_TABELA", Source = "UidTabela", Target = "UID_TABELA", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.DOC_MULTIMIDIA_TABELA_AUTORIZACAO", RelationPropertyName = "DOC_MULTIMIDIA_TABELA_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.ORDEM_APRESENTACAO", Source = "OrdemApresentacao", Target = "ORDEM_APRESENTACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.DOC_MULTIMIDIA_TABELA_AUTORIZACAO", RelationPropertyName = "DOC_MULTIMIDIA_TABELA_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.UID_DOCUMENTO", Source = "UidDocumento", Target = "UID_DOCUMENTO", TargetKeyName = "UID_DOCUMENTO", NoUpdatable = false, IsKey = true, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.DOC_MULTIMIDIA_AUTORIZACAO", RelationPropertyName = "DOC_MULTIMIDIA_AUTORIZACAO" });

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

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#.DOC_MULTIMIDIA_TABELA_AUTORIZACAO_LISTA as #Alias#];EdmEntityName[DOC_MULTIMIDIA_TABELA_AUTORIZACAO];EntityRelations[DOC_MULTIMIDIA_AUTORIZACAO(DOC_MULTIMIDIA_AUTORIZACAO)#DOC_MULTIMIDIA_AUTORIZACAO1(DOC_MULTIMIDIA_AUTORIZACAO)];EdmParentEntityName[DOC_MULTIMIDIA_AUTORIZACAO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "DocMultimidiaTabelaAutorizacaoChild")]
	[Serializable()]
	public partial class DocMultimidiaTabelaAutorizacaoChildParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For IdChave
	    partial void OnIdChaveChanging(Int64 value);
	    partial void OnIdChaveChanged();

	    private Int64 _IdChave;

	    [DataMember(IsRequired = true, Name = "IdChave", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Chave", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.ID_CHAVE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.ID_CHAVE")]
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
	    [Display(Name = "Ordem Apresentacao", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.ORDEM_APRESENTACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.ORDEM_APRESENTACAO")]
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
	    [Display(Name = "Uid Chave", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.UID_CHAVE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.UID_CHAVE")]
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
	    [Display(Name = "Uid Documento", Description="", Order = 14, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.UID_DOCUMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.UID_DOCUMENTO")]
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
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.UID_TABELA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.UID_TABELA")]
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
	    [Display(Name = "Conteudo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.CONTEUDO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_AUTORIZACAO.CONTEUDO")]
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
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.DATA_CRIACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_AUTORIZACAO.DATA_CRIACAO")]
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
	    //Extensibility Partial Method Definitions For DescDocumento
	    partial void OnDescDocumentoChanging(System.String value);
	    partial void OnDescDocumentoChanged();

	    private System.String _DescDocumento;

	    [DataMember(IsRequired = true, Name = "DescDocumento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Desc Documento", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.DESC_DOCUMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_AUTORIZACAO.DESC_DOCUMENTO")]
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
	    partial void OnIdDocClassificadorChanging(Int32 value);
	    partial void OnIdDocClassificadorChanged();

	    private Int32 _IdDocClassificador;

	    [DataMember(IsRequired = true, Name = "IdDocClassificador", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Doc Classificador Fk", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.ID_DOC_CLASSIFICADOR_FK];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_AUTORIZACAO.ID_DOC_CLASSIFICADOR_FK")]
	    public Int32 IdDocClassificador
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
	    [Display(Name = "Lx Tipo Documento", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.LX_TIPO_DOCUMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_AUTORIZACAO.LX_TIPO_DOCUMENTO")]
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
	    [Display(Name = "Lx Tipo Extensao", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.LX_TIPO_EXTENSAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_AUTORIZACAO.LX_TIPO_EXTENSAO")]
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
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.LX_TIPO_MIDIA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_AUTORIZACAO.LX_TIPO_MIDIA")]
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
	    [FunctionalPoint("Precision[150:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.NOME_ARQUIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_AUTORIZACAO.NOME_ARQUIVO")]
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
	    [Display(Name = "Obs", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.OBS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_AUTORIZACAO.OBS")]
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
	    [Display(Name = "Tamanho Midia", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.TAMANHO_MIDIA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_AUTORIZACAO.TAMANHO_MIDIA")]
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
	    [Display(Name = "Thumbnail", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.THUMBNAIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_AUTORIZACAO.THUMBNAIL")]
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
	    [Display(Name = "Tipo Conteudo Http", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(100)]
	    [FunctionalPoint("Precision[100:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.TIPO_CONTEUDO_HTTP];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_AUTORIZACAO.TIPO_CONTEUDO_HTTP")]
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
	    [Display(Name = "Url", Description="", Order = 15, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(500)]
	    [FunctionalPoint("Precision[500:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.URL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_AUTORIZACAO.URL")]
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
	    [Display(Name = "Xml Mapeamento", Description="", Order = 16, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.XML_MAPEAMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_AUTORIZACAO.XML_MAPEAMENTO")]
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
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.DOC_MULTIMIDIA_TABELA_AUTORIZACAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = true, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.DOC_MULTIMIDIA_TABELA_AUTORIZACAO), QualifiedEntitySetName = "AutorizacaoContext.DOC_MULTIMIDIA_TABELA_AUTORIZACAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.ID_CHAVE", Source = "IdChave", Target = "ID_CHAVE", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.DOC_MULTIMIDIA_TABELA_AUTORIZACAO", RelationPropertyName = "DOC_MULTIMIDIA_TABELA_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.UID_CHAVE", Source = "UidChave", Target = "UID_CHAVE", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.DOC_MULTIMIDIA_TABELA_AUTORIZACAO", RelationPropertyName = "DOC_MULTIMIDIA_TABELA_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.UID_TABELA", Source = "UidTabela", Target = "UID_TABELA", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.DOC_MULTIMIDIA_TABELA_AUTORIZACAO", RelationPropertyName = "DOC_MULTIMIDIA_TABELA_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.ORDEM_APRESENTACAO", Source = "OrdemApresentacao", Target = "ORDEM_APRESENTACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.DOC_MULTIMIDIA_TABELA_AUTORIZACAO", RelationPropertyName = "DOC_MULTIMIDIA_TABELA_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="DOC_MULTIMIDIA_TABELA_AUTORIZACAO.DOC_MULTIMIDIA_AUTORIZACAO.UID_DOCUMENTO", Source = "UidDocumento", Target = "UID_DOCUMENTO", TargetKeyName = "UID_DOCUMENTO", NoUpdatable = false, IsKey = true, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.DOC_MULTIMIDIA_AUTORIZACAO", RelationPropertyName = "DOC_MULTIMIDIA_AUTORIZACAO" });

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
	[DomainIdentifier("ProcessorOverviewMultimidiaAutorizacaoDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class MultimidiaAutorizacaoDomainService : DomainService, IDataServiceContext 
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

		
	    public MultimidiaAutorizacaoDomainService() : this("", null, null) { }
	    public MultimidiaAutorizacaoDomainService(string connectionString) : this(connectionString, null, null) { }
	    public MultimidiaAutorizacaoDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public MultimidiaAutorizacaoDomainService(Linx.Framework.Autorizacao.BM.AutorizacaoContext dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public MultimidiaAutorizacaoDomainService(string connectionString, Linx.Framework.Autorizacao.BM.AutorizacaoContext dataContext, Dictionary<string, string> headers) : base() 
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
 	        var _DocMultimidiaAutorizacaoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is DocMultimidiaAutorizacao && e.Entity.GetType().Name == "DocMultimidiaAutorizacao" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _DocMultimidiaAutorizacaoElements)
 	           if (((DocMultimidiaAutorizacao)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 
 	        //Remove inconsistent details
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is DocMultimidiaTabelaAutorizacaoChild && e.Entity.GetType().Name == "DocMultimidiaTabelaAutorizacaoChild" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
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
	    //Get All LookUpDocMultimidiaAutorizacao.
	    public IQueryable<LookUpDocMultimidiaAutorizacao> GetAllLookUpDocMultimidiaAutorizacao()
	    {
	        return this.GetLookUpDocMultimidiaAutorizacao(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpDocMultimidiaAutorizacao By EntitySearch.
	    public IQueryable<LookUpDocMultimidiaAutorizacao> GetLookUpDocMultimidiaAutorizacaoByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpDocMultimidiaAutorizacao(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpDocMultimidiaAutorizacao.
	    public IQueryable<LookUpDocMultimidiaAutorizacao> GetLookUpDocMultimidiaAutorizacao(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "DOC_MULTIMIDIA_AUTORIZACAO" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpDocMultimidiaAutorizacao";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpDocMultimidiaAutorizacao));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpDocMultimidiaAutorizacao> query =  
	
	            (from entity in this.DbContext.DOC_MULTIMIDIA_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpDocMultimidiaAutorizacao()		
	            {
	            
                Conteudo = entity.CONTEUDO
                , DataCriacao = entity.DATA_CRIACAO
                , DescDocumento = entity.DESC_DOCUMENTO
                , IdDocClassificadorFk = entity.ID_DOC_CLASSIFICADOR_FK
                , LxTipoDocumento = entity.LX_TIPO_DOCUMENTO
                , LxTipoExtensao = entity.LX_TIPO_EXTENSAO
                , LxTipoMidia = entity.LX_TIPO_MIDIA
                , NomeArquivo = entity.NOME_ARQUIVO
                , Obs = entity.OBS
                , TamanhoMidia = entity.TAMANHO_MIDIA
                , Thumbnail = entity.THUMBNAIL
                , TipoConteudoHttp = entity.TIPO_CONTEUDO_HTTP
                , UidDocumento = entity.UID_DOCUMENTO
                , Url = entity.URL
                , XmlMapeamento = entity.XML_MAPEAMENTO
                , IdDocClassificador = entity.ID_DOC_CLASSIFICADOR_FK
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
	
		

	        if (entityName.InList("Linx.Framework.BV.MultimidiaAutorizacao.DocMultimidiaAutorizacao"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "DocMultimidiaAutorizacao",
	        			NameSpace = "Linx.Framework.BV.MultimidiaAutorizacao",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "DocMultimidiaAutorizacao",
	        			ClearMethodName = "ClearDocMultimidiaAutorizacao",
	        			QueryMethodName  = "GetPagedDocMultimidiaAutorizacao",	
	        			CountingMethodName  = "GetDocMultimidiaAutorizacao" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.MultimidiaAutorizacao.DocMultimidiaAutorizacao"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.MultimidiaAutorizacao.DocMultimidiaAutorizacao"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.MultimidiaAutorizacao.DocMultimidiaAutorizacao", "Linx.Framework.BV.MultimidiaAutorizacao.DocMultimidiaTabelaAutorizacaoChild"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "DocMultimidiaTabelaAutorizacaoChild" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.MultimidiaAutorizacao",
	        			HasQuickSearch = false,
	        			ParentClassName = "DocMultimidiaAutorizacao",	
	        			DisplayName = "DocMultimidiaTabelaAutorizacaoChild",
	        			ClearMethodName = "ClearDocMultimidiaTabelaAutorizacaoChild" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedDocMultimidiaTabelaAutorizacaoChild" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetDocMultimidiaTabelaAutorizacaoChild" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.MultimidiaAutorizacao.DocMultimidiaTabelaAutorizacaoChild"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.MultimidiaAutorizacao.DocMultimidiaTabelaAutorizacaoChild" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.MultimidiaAutorizacao.DocMultimidiaTabelaAutorizacao"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "DocMultimidiaTabelaAutorizacao",
	        			NameSpace = "Linx.Framework.BV.MultimidiaAutorizacao",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "DocMultimidiaTabelaAutorizacao",
	        			ClearMethodName = "ClearDocMultimidiaTabelaAutorizacao",
	        			QueryMethodName  = "GetPagedDocMultimidiaTabelaAutorizacao",	
	        			CountingMethodName  = "GetDocMultimidiaTabelaAutorizacao" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.MultimidiaAutorizacao.DocMultimidiaTabelaAutorizacao"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.MultimidiaAutorizacao.DocMultimidiaTabelaAutorizacao"), forceAll: forceAll)
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

         		    return new string[] { "Framework_MultimidiaAutorizacaoClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.MultimidiaAutorizacaoClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Framework_multimidiaAutorizacaoService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.multimidiaAutorizacaoService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear DocMultimidiaAutorizacao.
	    public IEnumerable<DocMultimidiaAutorizacao> ClearDocMultimidiaAutorizacao()
	    {
	        List<DocMultimidiaAutorizacao> result = new List<DocMultimidiaAutorizacao>();
	        result.Add(new DocMultimidiaAutorizacao());	
			
	        result[0].DocMultimidiaTabelaAutorizacaoChildList = new List<DocMultimidiaTabelaAutorizacaoChild>();
	        ((List<DocMultimidiaTabelaAutorizacaoChild>)result[0].DocMultimidiaTabelaAutorizacaoChildList).Add(new DocMultimidiaTabelaAutorizacaoChild());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear DocMultimidiaTabelaAutorizacaoChild.
	    public IEnumerable<DocMultimidiaTabelaAutorizacaoChild> ClearDocMultimidiaTabelaAutorizacaoChild()
	    {
	        List<DocMultimidiaTabelaAutorizacaoChild> result = new List<DocMultimidiaTabelaAutorizacaoChild>();
	        result.Add(new DocMultimidiaTabelaAutorizacaoChild());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear DocMultimidiaTabelaAutorizacao.
	    public IEnumerable<DocMultimidiaTabelaAutorizacao> ClearDocMultimidiaTabelaAutorizacao()
	    {
	        List<DocMultimidiaTabelaAutorizacao> result = new List<DocMultimidiaTabelaAutorizacao>();
	        result.Add(new DocMultimidiaTabelaAutorizacao());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    
	    [Query(HasSideEffects = false)]
	    //Get DocMultimidiaAutorizacao.
	    public IQueryable<DocMultimidiaAutorizacao> GetDocMultimidiaAutorizacao()
	    {




		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaAutorizacao> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_AUTORIZACAO
	            
	            	
	            select new DocMultimidiaAutorizacao()		
	            {
	            
                Conteudo = entity0.CONTEUDO
                , DataCriacao = entity0.DATA_CRIACAO
                , DescDocumento = entity0.DESC_DOCUMENTO
                , IdDocClassificador = entity0.ID_DOC_CLASSIFICADOR_FK
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
			
                ,DocMultimidiaTabelaAutorizacaoChildList = 
	                        (from entity1 in entity0.DOC_MULTIMIDIA_TABELA_AUTORIZACAO_LISTA
                                  let entity1Al1 = entity1.DOC_MULTIMIDIA_AUTORIZACAO
	                        
	                        	
	                        select new DocMultimidiaTabelaAutorizacaoChild()
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
	    //Get DocMultimidiaTabelaAutorizacaoChild.
	    public IQueryable<DocMultimidiaTabelaAutorizacaoChild> GetDocMultimidiaTabelaAutorizacaoChild()
	    {




		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaTabelaAutorizacaoChild> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_TABELA_AUTORIZACAO
                  let entity0Al1 = entity0.DOC_MULTIMIDIA_AUTORIZACAO
	            
	            	
	            select new DocMultimidiaTabelaAutorizacaoChild()		
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
	    //Get DocMultimidiaAutorizacaoNoAssociations.
	    public IQueryable<DocMultimidiaAutorizacao> GetDocMultimidiaAutorizacaoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaAutorizacao> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_AUTORIZACAO
	            
	            	
	            select new DocMultimidiaAutorizacao()		
	            {
	            
                Conteudo = entity0.CONTEUDO
                , DataCriacao = entity0.DATA_CRIACAO
                , DescDocumento = entity0.DESC_DOCUMENTO
                , IdDocClassificador = entity0.ID_DOC_CLASSIFICADOR_FK
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
	    //Get DocMultimidiaTabelaAutorizacaoChildNoAssociations.
	    public IQueryable<DocMultimidiaTabelaAutorizacaoChild> GetDocMultimidiaTabelaAutorizacaoChildNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaTabelaAutorizacaoChild> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_TABELA_AUTORIZACAO
                  let entity0Al1 = entity0.DOC_MULTIMIDIA_AUTORIZACAO
	            
	            	
	            select new DocMultimidiaTabelaAutorizacaoChild()		
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
	    //Get DocMultimidiaTabelaAutorizacao.
	    public IQueryable<DocMultimidiaTabelaAutorizacao> GetDocMultimidiaTabelaAutorizacao()
	    {




		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaTabelaAutorizacao> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_TABELA_AUTORIZACAO
                  let entity0Al1 = entity0.DOC_MULTIMIDIA_AUTORIZACAO
	            
	            	
	            select new DocMultimidiaTabelaAutorizacao()		
	            {
	            
                Conteudo = entity0Al1.CONTEUDO
                , DataCriacao = entity0Al1.DATA_CRIACAO
                , DescDocumento = entity0Al1.DESC_DOCUMENTO
                , IdChave = entity0.ID_CHAVE
                , IdDocClassificador = entity0Al1.ID_DOC_CLASSIFICADOR_FK
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
	    //Get DocMultimidiaTabelaAutorizacaoNoAssociations.
	    public IQueryable<DocMultimidiaTabelaAutorizacao> GetDocMultimidiaTabelaAutorizacaoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaTabelaAutorizacao> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_TABELA_AUTORIZACAO
                  let entity0Al1 = entity0.DOC_MULTIMIDIA_AUTORIZACAO
	            
	            	
	            select new DocMultimidiaTabelaAutorizacao()		
	            {
	            
                Conteudo = entity0Al1.CONTEUDO
                , DataCriacao = entity0Al1.DATA_CRIACAO
                , DescDocumento = entity0Al1.DESC_DOCUMENTO
                , IdChave = entity0.ID_CHAVE
                , IdDocClassificador = entity0Al1.ID_DOC_CLASSIFICADOR_FK
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
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	//Add filtering disabled property for DOC_MULTIMIDIA_AUTORIZACAO
	    	string[] bmDisabledDocMultimidiaAutorizacaoList = this.GetEDM().GetFilteringDisabledList("DOC_MULTIMIDIA_AUTORIZACAO");
	    	if (bmDisabledDocMultimidiaAutorizacaoList.Length > 0)
	    	{
	
	    		if (bmDisabledDocMultimidiaAutorizacaoList.Contains("DOC_MULTIMIDIA_AUTORIZACAO.CONTEUDO"))
	    		{
	    			result.Add("DocMultimidiaAutorizacao|Conteudo");
	    			result.Add("DocMultimidiaAutorizacao|DOC_MULTIMIDIA_AUTORIZACAO.CONTEUDO");
	    		}
	
	    		if (bmDisabledDocMultimidiaAutorizacaoList.Contains("DOC_MULTIMIDIA_AUTORIZACAO.DATA_CRIACAO"))
	    		{
	    			result.Add("DocMultimidiaAutorizacao|DataCriacao");
	    			result.Add("DocMultimidiaAutorizacao|DOC_MULTIMIDIA_AUTORIZACAO.DATA_CRIACAO");
	    		}
	
	    		if (bmDisabledDocMultimidiaAutorizacaoList.Contains("DOC_MULTIMIDIA_AUTORIZACAO.DESC_DOCUMENTO"))
	    		{
	    			result.Add("DocMultimidiaAutorizacao|DescDocumento");
	    			result.Add("DocMultimidiaAutorizacao|DOC_MULTIMIDIA_AUTORIZACAO.DESC_DOCUMENTO");
	    		}
	
	    		if (bmDisabledDocMultimidiaAutorizacaoList.Contains("DOC_MULTIMIDIA_AUTORIZACAO.ID_DOC_CLASSIFICADOR_FK"))
	    		{
	    			result.Add("DocMultimidiaAutorizacao|IdDocClassificador");
	    			result.Add("DocMultimidiaAutorizacao|DOC_MULTIMIDIA_AUTORIZACAO.ID_DOC_CLASSIFICADOR_FK");
	    		}
	
	    		if (bmDisabledDocMultimidiaAutorizacaoList.Contains("DOC_MULTIMIDIA_AUTORIZACAO.LX_TIPO_DOCUMENTO"))
	    		{
	    			result.Add("DocMultimidiaAutorizacao|LxTipoDocumento");
	    			result.Add("DocMultimidiaAutorizacao|DOC_MULTIMIDIA_AUTORIZACAO.LX_TIPO_DOCUMENTO");
	    		}
	
	    		if (bmDisabledDocMultimidiaAutorizacaoList.Contains("DOC_MULTIMIDIA_AUTORIZACAO.LX_TIPO_EXTENSAO"))
	    		{
	    			result.Add("DocMultimidiaAutorizacao|LxTipoExtensao");
	    			result.Add("DocMultimidiaAutorizacao|DOC_MULTIMIDIA_AUTORIZACAO.LX_TIPO_EXTENSAO");
	    		}
	
	    		if (bmDisabledDocMultimidiaAutorizacaoList.Contains("DOC_MULTIMIDIA_AUTORIZACAO.LX_TIPO_MIDIA"))
	    		{
	    			result.Add("DocMultimidiaAutorizacao|LxTipoMidia");
	    			result.Add("DocMultimidiaAutorizacao|DOC_MULTIMIDIA_AUTORIZACAO.LX_TIPO_MIDIA");
	    		}
	
	    		if (bmDisabledDocMultimidiaAutorizacaoList.Contains("DOC_MULTIMIDIA_AUTORIZACAO.NOME_ARQUIVO"))
	    		{
	    			result.Add("DocMultimidiaAutorizacao|NomeArquivo");
	    			result.Add("DocMultimidiaAutorizacao|DOC_MULTIMIDIA_AUTORIZACAO.NOME_ARQUIVO");
	    		}
	
	    		if (bmDisabledDocMultimidiaAutorizacaoList.Contains("DOC_MULTIMIDIA_AUTORIZACAO.OBS"))
	    		{
	    			result.Add("DocMultimidiaAutorizacao|Obs");
	    			result.Add("DocMultimidiaAutorizacao|DOC_MULTIMIDIA_AUTORIZACAO.OBS");
	    		}
	
	    		if (bmDisabledDocMultimidiaAutorizacaoList.Contains("DOC_MULTIMIDIA_AUTORIZACAO.TAMANHO_MIDIA"))
	    		{
	    			result.Add("DocMultimidiaAutorizacao|TamanhoMidia");
	    			result.Add("DocMultimidiaAutorizacao|DOC_MULTIMIDIA_AUTORIZACAO.TAMANHO_MIDIA");
	    		}
	
	    		if (bmDisabledDocMultimidiaAutorizacaoList.Contains("DOC_MULTIMIDIA_AUTORIZACAO.THUMBNAIL"))
	    		{
	    			result.Add("DocMultimidiaAutorizacao|Thumbnail");
	    			result.Add("DocMultimidiaAutorizacao|DOC_MULTIMIDIA_AUTORIZACAO.THUMBNAIL");
	    		}
	
	    		if (bmDisabledDocMultimidiaAutorizacaoList.Contains("DOC_MULTIMIDIA_AUTORIZACAO.TIPO_CONTEUDO_HTTP"))
	    		{
	    			result.Add("DocMultimidiaAutorizacao|TipoConteudoHttp");
	    			result.Add("DocMultimidiaAutorizacao|DOC_MULTIMIDIA_AUTORIZACAO.TIPO_CONTEUDO_HTTP");
	    		}
	
	    		if (bmDisabledDocMultimidiaAutorizacaoList.Contains("DOC_MULTIMIDIA_AUTORIZACAO.UID_DOCUMENTO"))
	    		{
	    			result.Add("DocMultimidiaAutorizacao|UidDocumento");
	    			result.Add("DocMultimidiaAutorizacao|DOC_MULTIMIDIA_AUTORIZACAO.UID_DOCUMENTO");
	    		}
	
	    		if (bmDisabledDocMultimidiaAutorizacaoList.Contains("DOC_MULTIMIDIA_AUTORIZACAO.URL"))
	    		{
	    			result.Add("DocMultimidiaAutorizacao|Url");
	    			result.Add("DocMultimidiaAutorizacao|DOC_MULTIMIDIA_AUTORIZACAO.URL");
	    		}
	
	    		if (bmDisabledDocMultimidiaAutorizacaoList.Contains("DOC_MULTIMIDIA_AUTORIZACAO.XML_MAPEAMENTO"))
	    		{
	    			result.Add("DocMultimidiaAutorizacao|XmlMapeamento");
	    			result.Add("DocMultimidiaAutorizacao|DOC_MULTIMIDIA_AUTORIZACAO.XML_MAPEAMENTO");
	    		}
	    	}
	    	//Add filtering disabled property for DOC_MULTIMIDIA_TABELA_AUTORIZACAO
	    	string[] bmDisabledDocMultimidiaTabelaAutorizacaoChildList = this.GetEDM().GetFilteringDisabledList("DOC_MULTIMIDIA_TABELA_AUTORIZACAO");
	    	if (bmDisabledDocMultimidiaTabelaAutorizacaoChildList.Length > 0)
	    	{
	
	    		if (bmDisabledDocMultimidiaTabelaAutorizacaoChildList.Contains("DOC_MULTIMIDIA_TABELA_AUTORIZACAO.ID_CHAVE"))
	    		{
	    			result.Add("DocMultimidiaTabelaAutorizacaoChild|IdChave");
	    			result.Add("DocMultimidiaTabelaAutorizacaoChild|DOC_MULTIMIDIA_TABELA_AUTORIZACAO.ID_CHAVE");
	    		}
	
	    		if (bmDisabledDocMultimidiaTabelaAutorizacaoChildList.Contains("DOC_MULTIMIDIA_TABELA_AUTORIZACAO.ORDEM_APRESENTACAO"))
	    		{
	    			result.Add("DocMultimidiaTabelaAutorizacaoChild|OrdemApresentacao");
	    			result.Add("DocMultimidiaTabelaAutorizacaoChild|DOC_MULTIMIDIA_TABELA_AUTORIZACAO.ORDEM_APRESENTACAO");
	    		}
	
	    		if (bmDisabledDocMultimidiaTabelaAutorizacaoChildList.Contains("DOC_MULTIMIDIA_TABELA_AUTORIZACAO.UID_CHAVE"))
	    		{
	    			result.Add("DocMultimidiaTabelaAutorizacaoChild|UidChave");
	    			result.Add("DocMultimidiaTabelaAutorizacaoChild|DOC_MULTIMIDIA_TABELA_AUTORIZACAO.UID_CHAVE");
	    		}
	
	    		if (bmDisabledDocMultimidiaTabelaAutorizacaoChildList.Contains("DOC_MULTIMIDIA_TABELA_AUTORIZACAO.UID_TABELA"))
	    		{
	    			result.Add("DocMultimidiaTabelaAutorizacaoChild|UidTabela");
	    			result.Add("DocMultimidiaTabelaAutorizacaoChild|DOC_MULTIMIDIA_TABELA_AUTORIZACAO.UID_TABELA");
	    		}
	    	}
	    	//Add filtering disabled property for DOC_MULTIMIDIA_TABELA_AUTORIZACAO
	    	string[] bmDisabledDocMultimidiaTabelaAutorizacaoList = this.GetEDM().GetFilteringDisabledList("DOC_MULTIMIDIA_TABELA_AUTORIZACAO");
	    	if (bmDisabledDocMultimidiaTabelaAutorizacaoList.Length > 0)
	    	{
	
	    		if (bmDisabledDocMultimidiaTabelaAutorizacaoList.Contains("DOC_MULTIMIDIA_TABELA_AUTORIZACAO.ID_CHAVE"))
	    		{
	    			result.Add("DocMultimidiaTabelaAutorizacao|IdChave");
	    			result.Add("DocMultimidiaTabelaAutorizacao|DOC_MULTIMIDIA_TABELA_AUTORIZACAO.ID_CHAVE");
	    		}
	
	    		if (bmDisabledDocMultimidiaTabelaAutorizacaoList.Contains("DOC_MULTIMIDIA_TABELA_AUTORIZACAO.ORDEM_APRESENTACAO"))
	    		{
	    			result.Add("DocMultimidiaTabelaAutorizacao|OrdemApresentacao");
	    			result.Add("DocMultimidiaTabelaAutorizacao|DOC_MULTIMIDIA_TABELA_AUTORIZACAO.ORDEM_APRESENTACAO");
	    		}
	
	    		if (bmDisabledDocMultimidiaTabelaAutorizacaoList.Contains("DOC_MULTIMIDIA_TABELA_AUTORIZACAO.UID_CHAVE"))
	    		{
	    			result.Add("DocMultimidiaTabelaAutorizacao|UidChave");
	    			result.Add("DocMultimidiaTabelaAutorizacao|DOC_MULTIMIDIA_TABELA_AUTORIZACAO.UID_CHAVE");
	    		}
	
	    		if (bmDisabledDocMultimidiaTabelaAutorizacaoList.Contains("DOC_MULTIMIDIA_TABELA_AUTORIZACAO.UID_TABELA"))
	    		{
	    			result.Add("DocMultimidiaTabelaAutorizacao|UidTabela");
	    			result.Add("DocMultimidiaTabelaAutorizacao|DOC_MULTIMIDIA_TABELA_AUTORIZACAO.UID_TABELA");
	    		}
	    	}
	    	return result;
	    }

	    #endregion Excluded Filters

	    #region Get By EntitySearchId Methods Definitions.
	
				
	    [Ignore]
	    //Get DocMultimidiaAutorizacao By EntitySearchId.
	    public IQueryable<DocMultimidiaAutorizacao> GetDocMultimidiaAutorizacaoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetDocMultimidiaAutorizacaoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get DocMultimidiaTabelaAutorizacaoChild By EntitySearchId.
	    public IQueryable<DocMultimidiaTabelaAutorizacaoChild> GetDocMultimidiaTabelaAutorizacaoChildByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetDocMultimidiaTabelaAutorizacaoChildByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get DocMultimidiaAutorizacao By EntitySearchId.
	    public IQueryable<DocMultimidiaAutorizacao> GetDocMultimidiaAutorizacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetDocMultimidiaAutorizacaoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get DocMultimidiaTabelaAutorizacaoChild By EntitySearchId.
	    public IQueryable<DocMultimidiaTabelaAutorizacaoChild> GetDocMultimidiaTabelaAutorizacaoChildByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetDocMultimidiaTabelaAutorizacaoChildByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get DocMultimidiaTabelaAutorizacao By EntitySearchId.
	    public IQueryable<DocMultimidiaTabelaAutorizacao> GetDocMultimidiaTabelaAutorizacaoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetDocMultimidiaTabelaAutorizacaoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get DocMultimidiaTabelaAutorizacao By EntitySearchId.
	    public IQueryable<DocMultimidiaTabelaAutorizacao> GetDocMultimidiaTabelaAutorizacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetDocMultimidiaTabelaAutorizacaoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get DocMultimidiaAutorizacao By Example.
	    [Ignore]
	    public IQueryable<DocMultimidiaAutorizacao> GetDocMultimidiaAutorizacaoByExample(DocMultimidiaAutorizacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetDocMultimidiaAutorizacaoByEntitySearch(queryAnalysis);
	    }
			
	    //Get DocMultimidiaTabelaAutorizacaoChild By Example.
	    [Ignore]
	    public IQueryable<DocMultimidiaTabelaAutorizacaoChild> GetDocMultimidiaTabelaAutorizacaoChildByExample(DocMultimidiaTabelaAutorizacaoChild entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetDocMultimidiaTabelaAutorizacaoChildByEntitySearch(queryAnalysis);
	    }
			
	    //Get DocMultimidiaAutorizacao By Example.
	    [Ignore]
	    public IQueryable<DocMultimidiaAutorizacao> GetDocMultimidiaAutorizacaoByExampleNoAssociations(DocMultimidiaAutorizacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetDocMultimidiaAutorizacaoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get DocMultimidiaTabelaAutorizacaoChild By Example.
	    [Ignore]
	    public IQueryable<DocMultimidiaTabelaAutorizacaoChild> GetDocMultimidiaTabelaAutorizacaoChildByExampleNoAssociations(DocMultimidiaTabelaAutorizacaoChild entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetDocMultimidiaTabelaAutorizacaoChildByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get DocMultimidiaTabelaAutorizacao By Example.
	    [Ignore]
	    public IQueryable<DocMultimidiaTabelaAutorizacao> GetDocMultimidiaTabelaAutorizacaoByExample(DocMultimidiaTabelaAutorizacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetDocMultimidiaTabelaAutorizacaoByEntitySearch(queryAnalysis);
	    }
			
	    //Get DocMultimidiaTabelaAutorizacao By Example.
	    [Ignore]
	    public IQueryable<DocMultimidiaTabelaAutorizacao> GetDocMultimidiaTabelaAutorizacaoByExampleNoAssociations(DocMultimidiaTabelaAutorizacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetDocMultimidiaTabelaAutorizacaoByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public DocMultimidiaAutorizacao GetDocMultimidiaAutorizacaoByKey(System.Guid uidDocumento)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("DocMultimidiaAutorizacao");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "UidDocumento"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, uidDocumento));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetDocMultimidiaAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public DocMultimidiaTabelaAutorizacaoChild GetDocMultimidiaTabelaAutorizacaoChildByKey(Int64 idChave, System.Guid uidChave, System.Guid uidDocumento, System.Guid uidTabela)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("DocMultimidiaTabelaAutorizacaoChild");
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
	         return (from r in this.GetDocMultimidiaTabelaAutorizacaoChildByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public DocMultimidiaTabelaAutorizacao GetDocMultimidiaTabelaAutorizacaoByKey(Int64 idChave, System.Guid uidChave, System.Guid uidDocumento, System.Guid uidTabela)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("DocMultimidiaTabelaAutorizacao");
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
	         return (from r in this.GetDocMultimidiaTabelaAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get DocMultimidiaAutorizacaoByEntitySearch.
	    public IQueryable<DocMultimidiaAutorizacao> GetDocMultimidiaAutorizacaoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(DocMultimidiaAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaAutorizacao> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new DocMultimidiaAutorizacao()		
	            {
	            
                Conteudo = entity0.CONTEUDO
                , DataCriacao = entity0.DATA_CRIACAO
                , DescDocumento = entity0.DESC_DOCUMENTO
                , IdDocClassificador = entity0.ID_DOC_CLASSIFICADOR_FK
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
			
                ,DocMultimidiaTabelaAutorizacaoChildList = 
	                        (from entity1 in entity0.DOC_MULTIMIDIA_TABELA_AUTORIZACAO_LISTA
                                  let entity1Al1 = entity1.DOC_MULTIMIDIA_AUTORIZACAO
	                        
	                        	
	                        select new DocMultimidiaTabelaAutorizacaoChild()
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
	    //Get DocMultimidiaTabelaAutorizacaoChildByEntitySearch.
	    public IQueryable<DocMultimidiaTabelaAutorizacaoChild> GetDocMultimidiaTabelaAutorizacaoChildByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(DocMultimidiaTabelaAutorizacaoChild));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaTabelaAutorizacaoChild> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_TABELA_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.DOC_MULTIMIDIA_AUTORIZACAO
	            
	            	
	            select new DocMultimidiaTabelaAutorizacaoChild()		
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
	    //Get DocMultimidiaAutorizacaoByEntitySearchNoAssociations.
	    public IQueryable<DocMultimidiaAutorizacao> GetDocMultimidiaAutorizacaoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(DocMultimidiaAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaAutorizacao> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new DocMultimidiaAutorizacao()		
	            {
	            
                Conteudo = entity0.CONTEUDO
                , DataCriacao = entity0.DATA_CRIACAO
                , DescDocumento = entity0.DESC_DOCUMENTO
                , IdDocClassificador = entity0.ID_DOC_CLASSIFICADOR_FK
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
	    //Get DocMultimidiaTabelaAutorizacaoChildByEntitySearchNoAssociations.
	    public IQueryable<DocMultimidiaTabelaAutorizacaoChild> GetDocMultimidiaTabelaAutorizacaoChildByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(DocMultimidiaTabelaAutorizacaoChild));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaTabelaAutorizacaoChild> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_TABELA_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.DOC_MULTIMIDIA_AUTORIZACAO
	            
	            	
	            select new DocMultimidiaTabelaAutorizacaoChild()		
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
	    //Get DocMultimidiaTabelaAutorizacaoChildParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<DocMultimidiaTabelaAutorizacaoChildParentComposition> GetDocMultimidiaTabelaAutorizacaoChildParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "DOC_MULTIMIDIA_AUTORIZACAO", "DOC_MULTIMIDIA_TABELA_AUTORIZACAO", "DOC_MULTIMIDIA_AUTORIZACAO", typeof(DocMultimidiaTabelaAutorizacaoChildParentComposition));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaTabelaAutorizacaoChildParentComposition> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_TABELA_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.DOC_MULTIMIDIA_AUTORIZACAO
	            
	            	
	            select new DocMultimidiaTabelaAutorizacaoChildParentComposition()		
	            {
	            
                IdChave = entity0.ID_CHAVE
                , OrdemApresentacao = entity0.ORDEM_APRESENTACAO
                , UidChave = entity0.UID_CHAVE
                , UidDocumento = entity0Al1.UID_DOCUMENTO
                , UidTabela = entity0.UID_TABELA
                //DocMultimidiaAutorizacao Properties.
                , Conteudo = entity0.DOC_MULTIMIDIA_AUTORIZACAO.CONTEUDO
                , DataCriacao = entity0.DOC_MULTIMIDIA_AUTORIZACAO.DATA_CRIACAO
                , DescDocumento = entity0.DOC_MULTIMIDIA_AUTORIZACAO.DESC_DOCUMENTO
                , IdDocClassificador = entity0.DOC_MULTIMIDIA_AUTORIZACAO.ID_DOC_CLASSIFICADOR_FK
                , LxTipoDocumento = entity0.DOC_MULTIMIDIA_AUTORIZACAO.LX_TIPO_DOCUMENTO
                , LxTipoExtensao = entity0.DOC_MULTIMIDIA_AUTORIZACAO.LX_TIPO_EXTENSAO
                , LxTipoMidia = entity0.DOC_MULTIMIDIA_AUTORIZACAO.LX_TIPO_MIDIA
                , NomeArquivo = entity0.DOC_MULTIMIDIA_AUTORIZACAO.NOME_ARQUIVO
                , Obs = entity0.DOC_MULTIMIDIA_AUTORIZACAO.OBS
                , TamanhoMidia = entity0.DOC_MULTIMIDIA_AUTORIZACAO.TAMANHO_MIDIA
                , Thumbnail = entity0.DOC_MULTIMIDIA_AUTORIZACAO.THUMBNAIL
                , TipoConteudoHttp = entity0.DOC_MULTIMIDIA_AUTORIZACAO.TIPO_CONTEUDO_HTTP
                , Url = entity0.DOC_MULTIMIDIA_AUTORIZACAO.URL
                , XmlMapeamento = entity0.DOC_MULTIMIDIA_AUTORIZACAO.XML_MAPEAMENTO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get DocMultimidiaTabelaAutorizacaoByEntitySearch.
	    public IQueryable<DocMultimidiaTabelaAutorizacao> GetDocMultimidiaTabelaAutorizacaoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(DocMultimidiaTabelaAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaTabelaAutorizacao> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_TABELA_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.DOC_MULTIMIDIA_AUTORIZACAO
	            
	            	
	            select new DocMultimidiaTabelaAutorizacao()		
	            {
	            
                Conteudo = entity0Al1.CONTEUDO
                , DataCriacao = entity0Al1.DATA_CRIACAO
                , DescDocumento = entity0Al1.DESC_DOCUMENTO
                , IdChave = entity0.ID_CHAVE
                , IdDocClassificador = entity0Al1.ID_DOC_CLASSIFICADOR_FK
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
	    //Get DocMultimidiaTabelaAutorizacaoByEntitySearchNoAssociations.
	    public IQueryable<DocMultimidiaTabelaAutorizacao> GetDocMultimidiaTabelaAutorizacaoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(DocMultimidiaTabelaAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaTabelaAutorizacao> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_TABELA_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.DOC_MULTIMIDIA_AUTORIZACAO
	            
	            	
	            select new DocMultimidiaTabelaAutorizacao()		
	            {
	            
                Conteudo = entity0Al1.CONTEUDO
                , DataCriacao = entity0Al1.DATA_CRIACAO
                , DescDocumento = entity0Al1.DESC_DOCUMENTO
                , IdChave = entity0.ID_CHAVE
                , IdDocClassificador = entity0Al1.ID_DOC_CLASSIFICADOR_FK
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
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get PagedDocMultimidiaAutorizacao.
	    public IQueryable<DocMultimidiaAutorizacao> GetPagedDocMultimidiaAutorizacao(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(DocMultimidiaAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaAutorizacao> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                orderby entity0.UID_DOCUMENTO ascending
	            
	            	
	            select new DocMultimidiaAutorizacao()		
	            {
	            
                Conteudo = entity0.CONTEUDO
                , DataCriacao = entity0.DATA_CRIACAO
                , DescDocumento = entity0.DESC_DOCUMENTO
                , IdDocClassificador = entity0.ID_DOC_CLASSIFICADOR_FK
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
	    //Get PagedDocMultimidiaTabelaAutorizacaoChild.
	    public IQueryable<DocMultimidiaTabelaAutorizacaoChild> GetPagedDocMultimidiaTabelaAutorizacaoChild(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(DocMultimidiaTabelaAutorizacaoChild));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaTabelaAutorizacaoChild> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_TABELA_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.DOC_MULTIMIDIA_AUTORIZACAO
                orderby entity0.ID_CHAVE ascending, entity0.UID_CHAVE ascending, entity0.UID_TABELA ascending, entity0Al1.UID_DOCUMENTO ascending
	            
	            	
	            select new DocMultimidiaTabelaAutorizacaoChild()		
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
	    public int GetDocMultimidiaAutorizacaoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(DocMultimidiaAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.DOC_MULTIMIDIA_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetDocMultimidiaTabelaAutorizacaoChildCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(DocMultimidiaTabelaAutorizacaoChild));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.DOC_MULTIMIDIA_TABELA_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.DOC_MULTIMIDIA_AUTORIZACAO
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedDocMultimidiaTabelaAutorizacao.
	    public IQueryable<DocMultimidiaTabelaAutorizacao> GetPagedDocMultimidiaTabelaAutorizacao(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(DocMultimidiaTabelaAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<DocMultimidiaTabelaAutorizacao> result = 
	            (from entity0 in this.DbContext.DOC_MULTIMIDIA_TABELA_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.DOC_MULTIMIDIA_AUTORIZACAO
                orderby entity0.ID_CHAVE ascending, entity0.UID_CHAVE ascending, entity0.UID_TABELA ascending, entity0Al1.UID_DOCUMENTO ascending
	            
	            	
	            select new DocMultimidiaTabelaAutorizacao()		
	            {
	            
                Conteudo = entity0Al1.CONTEUDO
                , DataCriacao = entity0Al1.DATA_CRIACAO
                , DescDocumento = entity0Al1.DESC_DOCUMENTO
                , IdChave = entity0.ID_CHAVE
                , IdDocClassificador = entity0Al1.ID_DOC_CLASSIFICADOR_FK
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
	    public int GetDocMultimidiaTabelaAutorizacaoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(DocMultimidiaTabelaAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.DOC_MULTIMIDIA_TABELA_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.DOC_MULTIMIDIA_AUTORIZACAO
	            
	            select 1
	            ).Count();	
		
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    
	    [Update()]	
	    //Update DocMultimidiaAutorizacao.
	    public void UpdateDocMultimidiaAutorizacao(DocMultimidiaAutorizacao entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert DocMultimidiaAutorizacao.
	    public void InsertDocMultimidiaAutorizacao(DocMultimidiaAutorizacao entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete DocMultimidiaAutorizacao.
	    public void DeleteDocMultimidiaAutorizacao(DocMultimidiaAutorizacao entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update DocMultimidiaTabelaAutorizacaoChild.
	    public void UpdateDocMultimidiaTabelaAutorizacaoChild(DocMultimidiaTabelaAutorizacaoChild entity)
	    {



	
	        if (entity.DocMultimidiaAutorizacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.DocMultimidiaAutorizacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.DocMultimidiaAutorizacao); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert DocMultimidiaTabelaAutorizacaoChild.
	    public void InsertDocMultimidiaTabelaAutorizacaoChild(DocMultimidiaTabelaAutorizacaoChild entity)
	    {



	
	        if (entity.DocMultimidiaAutorizacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.DocMultimidiaAutorizacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.DocMultimidiaAutorizacao);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete DocMultimidiaTabelaAutorizacaoChild.
	    public void DeleteDocMultimidiaTabelaAutorizacaoChild(DocMultimidiaTabelaAutorizacaoChild entity)
	    {



	
	        if (entity.DocMultimidiaAutorizacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.DocMultimidiaAutorizacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.DocMultimidiaAutorizacao);
	            

	
	        }

	
	    }
		
			
	    
	    [Update()]	
	    //Update DocMultimidiaTabelaAutorizacao.
	    public void UpdateDocMultimidiaTabelaAutorizacao(DocMultimidiaTabelaAutorizacao entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert DocMultimidiaTabelaAutorizacao.
	    public void InsertDocMultimidiaTabelaAutorizacao(DocMultimidiaTabelaAutorizacao entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete DocMultimidiaTabelaAutorizacao.
	    public void DeleteDocMultimidiaTabelaAutorizacao(DocMultimidiaTabelaAutorizacao entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}