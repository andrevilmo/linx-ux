					
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

namespace Linx.Framework.BV.Transacao
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_TRANSACAO.ID_TRANSACAO", IsUpdatable=true, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Transações];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsTransacao,TcsTransacao.TcsTransacaoMenuChild,TcsTransacao.TcsTransacaoDependente];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[];EdmEntityName[TCS_TRANSACAO];EntityRelations[];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsTransacao")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Transacao.TcsTransacao")]
	public partial class TcsTransacao : Linx.Data.Entity
	{

	

	    public TcsTransacao() : this(true) { }

	    public TcsTransacao(bool setDefaults) 
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
	      if (this.TcsTransacaoMenuChildList != null && this.TcsTransacaoMenuChildList.Count() > 0)
	      {
	         foreach (var entity in this.TcsTransacaoMenuChildList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      if (this.TcsTransacaoDependenteList != null && this.TcsTransacaoDependenteList.Count() > 0)
	      {
	         foreach (var entity in this.TcsTransacaoDependenteList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.TcsTransacaoMenuChildList != null)
	      {
	         foreach (var detail in this.TcsTransacaoMenuChildList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsTransacaoMenuChildList = null;
	      }
	      if (this.TcsTransacaoDependenteList != null)
	      {
	         foreach (var detail in this.TcsTransacaoDependenteList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsTransacaoDependenteList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(TransacaoDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("TcsTransacaoMenuChild"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsTransacaoMenuChild");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTransacao"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdTransacao));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsTransacaoMenuChild and all sub-details
	         if (this.TcsTransacaoMenuChildList == null || this.TcsTransacaoMenuChildList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsTransacaoMenuChildList = context.GetPagedTcsTransacaoMenuChild(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsTransacaoMenuChildList = (from r in context.GetTcsTransacaoMenuChildByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	      if (viewNames == null || viewNames.Contains("TcsTransacaoDependente"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsTransacaoDependente");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTransacao"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdTransacao));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsTransacaoDependente and all sub-details
	         if (this.TcsTransacaoDependenteList == null || this.TcsTransacaoDependenteList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsTransacaoDependenteList = context.GetPagedTcsTransacaoDependente(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsTransacaoDependenteList = (from r in context.GetTcsTransacaoDependenteByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _TcsTransacaoMenuChildElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsTransacaoMenuChild && ((TcsTransacaoMenuChild)e.Entity).TcsTransacao == null && e.Associations == null && e.OriginalAssociations == null && ((TcsTransacaoMenuChild)e.Entity).IdTransacao == this.IdTransacao).ToList();
 	      if (_TcsTransacaoMenuChildElements.Count > 0 && this.TcsTransacaoMenuChildList.Count() == 0)
 	      {
 	          this.TcsTransacaoMenuChildList = _TcsTransacaoMenuChildElements.Select(e => (TcsTransacaoMenuChild)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsTransacaoMenuChildElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsTransacaoMenuChild)detail.Entity).TcsTransacao = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsTransacao", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsTransacaoMenuChildList", indexDetails.ToArray());
 	      }
 
 	      var _TcsTransacaoDependenteElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsTransacaoDependente && ((TcsTransacaoDependente)e.Entity).TcsTransacao == null && e.Associations == null && e.OriginalAssociations == null && ((TcsTransacaoDependente)e.Entity).IdTransacao == this.IdTransacao).ToList();
 	      if (_TcsTransacaoDependenteElements.Count > 0 && this.TcsTransacaoDependenteList.Count() == 0)
 	      {
 	          this.TcsTransacaoDependenteList = _TcsTransacaoDependenteElements.Select(e => (TcsTransacaoDependente)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsTransacaoDependenteElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsTransacaoDependente)detail.Entity).TcsTransacao = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsTransacao", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsTransacaoDependenteList", indexDetails.ToArray());
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
	    [FunctionalPoint("Precision[400:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO.CLASSE_NOME];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO.CLASSE_NOME")]
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
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO.COD_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO.COD_TRANSACAO")]
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
	    //Extensibility Partial Method Definitions For DescTransacao
	    partial void OnDescTransacaoChanging(string value);
	    partial void OnDescTransacaoChanged();

	    private string _DescTransacao;

	    [DataMember(IsRequired = true, Name = "DescTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição Detalhada", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO.DESC_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO.DESC_TRANSACAO")]
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
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO.ICONE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO.ICONE")]
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
	    [Display(Name = "Id Objeto", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsObjetoTransacao];LookUpTitle[Seleção de (Id Objeto)];LookUpQuery[executeLookUpTcsObjetoTransacao];LookUpFinalize[finalizeLookUpTcsObjetoTransacao];LookUpDisplayColumns[{\"IdObjeto\" : \"\", \"DescObjeto\" : \"Classe BO\", \"ClasseNome\" : \"\"}];LookUpColumns[{\"IdObjeto\" : false, \"DescObjeto\" : true, \"ClasseNome\" : false}];FilterDataKey[TCS_TRANSACAO.ID_OBJETO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdObjeto#true##12###0#false##::LookUpTcsObjetoTransacao##false#true###Linx.Framework.BV.Transacao#IQueryable###true#false", EdmKey="TCS_TRANSACAO.ID_OBJETO")]
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
	    [Display(Name = "Id Transacao", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO.ID_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO.ID_TRANSACAO")]
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
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO.INATIVO")]
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
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[CorFundo];KpiName[];KpiRelatedAttribute[];DefaultValue[7];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_TRANSACAO.LX_COR_FUNDO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO.LX_COR_FUNDO")]
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
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoTransacao];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO.LX_TIPO_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO.LX_TIPO_TRANSACAO")]
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
	    [Display(Name = "Descrição", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO.NOME_CURTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO.NOME_CURTO")]
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
	    //Extensibility Partial Method Definitions For Tag
	    partial void OnTagChanging(string value);
	    partial void OnTagChanged();

	    private string _Tag;

	    [DataMember(Name = "Tag", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tag", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(4000)]
	    [FunctionalPoint("Precision[4000:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO.TAG];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO.TAG")]
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
	    //Extensibility Partial Method Definitions For DescObjeto
	    partial void OnDescObjetoChanging(string value);
	    partial void OnDescObjetoChanged();

	    private string _DescObjeto;

	    [DataMember(IsRequired = true, Name = "DescObjeto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Classe BO", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(400)]
	    [FunctionalPoint("Precision[400:00];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[''];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#DescObjeto#false##60:0##Classe BO#1#true##::LookUpTcsObjetoTransacao##false#true###Linx.Framework.BV.Transacao#IQueryable###true#false", EdmKey="\"\"")]
	    public string DescObjeto
	    {
	    	    get
	    	    {
	    	          if (_DescObjeto != (GetDescObjeto()))
	    	             _DescObjeto =  GetDescObjeto();
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
	    //Extensibility Partial Method Definitions For ClasseNomeObjeto
	    partial void OnClasseNomeObjetoChanging(string value);
	    partial void OnClasseNomeObjetoChanged();

	    private string _ClasseNomeObjeto;

	    [DataMember(IsRequired = true, Name = "ClasseNomeObjeto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[''];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#ClasseNome#false##40:0###2#false##::LookUpTcsObjetoTransacao##false#true###Linx.Framework.BV.Transacao#IQueryable###true#false", EdmKey="\"\"")]
	    public string ClasseNomeObjeto
	    {
	    	    get
	    	    {
	    	          if (_ClasseNomeObjeto != (GetClasseNomeObjeto()))
	    	             _ClasseNomeObjeto =  GetClasseNomeObjeto();
	    	          return _ClasseNomeObjeto;
	    	    }
	    	    set
	    	    {
	    	          if (this._ClasseNomeObjeto != value)
	    	          {
	    	              this.ValidateProperty("ClasseNomeObjeto", value);
	    	              this.OnClasseNomeObjetoChanging(value);
	    	              this.RaiseDataMemberChanging("ClasseNomeObjeto");
	    	              this._ClasseNomeObjeto = value;
	    	              this.RaiseDataMemberChanged("ClasseNomeObjeto");
	    	              this.OnClasseNomeObjetoChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<TcsTransacaoDependente> _TcsTransacaoDependenteList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsTransacao_TcsTransacaoDependente", "IdTransacao", "IdTransacao", IsForeignKey=false)]
	    [DataMember(Name = "TcsTransacaoDependenteList", EmitDefaultValue = true)]
	    public IEnumerable<TcsTransacaoDependente> TcsTransacaoDependenteList
	    {
	        get
	        {
	
	            if (this._TcsTransacaoDependenteList == null)
	            	this._TcsTransacaoDependenteList = new List<TcsTransacaoDependente>();
	
	            return this._TcsTransacaoDependenteList;
	        }
	        set
	        {
	            if (this._TcsTransacaoDependenteList != value)
	            {
	                this._TcsTransacaoDependenteList = value;
	                this.RaisePropertyChanged("TcsTransacaoDependenteList");
	            }
	        }
	    }	 
		
	    private IEnumerable<TcsTransacaoMenuChild> _TcsTransacaoMenuChildList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsTransacao_TcsTransacaoMenuChild", "IdTransacao", "IdTransacao", IsForeignKey=false)]
	    [DataMember(Name = "TcsTransacaoMenuChildList", EmitDefaultValue = true)]
	    public IEnumerable<TcsTransacaoMenuChild> TcsTransacaoMenuChildList
	    {
	        get
	        {
	
	            if (this._TcsTransacaoMenuChildList == null)
	            	this._TcsTransacaoMenuChildList = new List<TcsTransacaoMenuChild>();
	
	            return this._TcsTransacaoMenuChildList;
	        }
	        set
	        {
	            if (this._TcsTransacaoMenuChildList != value)
	            {
	                this._TcsTransacaoMenuChildList = value;
	                this.RaisePropertyChanged("TcsTransacaoMenuChildList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_TRANSACAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_TRANSACAO), QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO.TAG", Source = "Tag", Target = "TAG", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO", RelationPropertyName = "TCS_TRANSACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO.ICONE", Source = "Icone", Target = "ICONE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO", RelationPropertyName = "TCS_TRANSACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO.INATIVO", Source = "Inativo", Target = "INATIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO", RelationPropertyName = "TCS_TRANSACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO.ID_OBJETO", Source = "IdObjeto", Target = "ID_OBJETO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO", RelationPropertyName = "TCS_TRANSACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO.NOME_CURTO", Source = "NomeCurto", Target = "NOME_CURTO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO", RelationPropertyName = "TCS_TRANSACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO.CLASSE_NOME", Source = "ClasseNome", Target = "CLASSE_NOME", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO", RelationPropertyName = "TCS_TRANSACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO.ID_TRANSACAO", Source = "IdTransacao", Target = "ID_TRANSACAO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO", RelationPropertyName = "TCS_TRANSACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO.LX_COR_FUNDO", Source = "LxCorFundo", Target = "LX_COR_FUNDO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO", RelationPropertyName = "TCS_TRANSACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO.COD_TRANSACAO", Source = "CodTransacao", Target = "COD_TRANSACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO", RelationPropertyName = "TCS_TRANSACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO.DESC_TRANSACAO", Source = "DescTransacao", Target = "DESC_TRANSACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO", RelationPropertyName = "TCS_TRANSACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO.LX_TIPO_TRANSACAO", Source = "LxTipoTransacao", Target = "LX_TIPO_TRANSACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO", RelationPropertyName = "TCS_TRANSACAO" });

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
	             Linx.Framework.BV.BusinessMediaHelper.SyncMedia("TCS_TRANSACAO", null, null, this.TableMedia.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(e => new Guid(e)).ToList());
	         }
	         else if (operation == DomainOperation.Delete) {
	             Linx.Framework.BV.BusinessMediaHelper.SyncMedia("TCS_TRANSACAO", null, null, new List<Guid>() { Guid.Empty });
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

		

	[LinxPublicationView(PrimaryKeys="TCS_TRANSACAO_MENU.ID_TCS_TRANSACAO_MENU", IsUpdatable=true, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Menu];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#. as #Alias#];EdmEntityName[TCS_TRANSACAO_MENU];EntityRelations[];EdmParentEntityName[TCS_TRANSACAO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsTransacaoMenuChild")]
	[Serializable()]
	public partial class TcsTransacaoMenuChild : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(TransacaoDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsTransacao");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTransacao"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdTransacao));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsTransacao
	         this.TcsTransacao = (from r in context.GetTcsTransacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For IdModuloMenu
	    partial void OnIdModuloMenuChanging(Int64 value);
	    partial void OnIdModuloMenuChanged();

	    private Int64 _IdModuloMenu;

	    [DataMember(IsRequired = true, Name = "IdModuloMenu", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo Menu", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsTransacaoMenuChildTcsModuloMenu];LookUpTitle[Seleção de (Id Modulo Menu)];LookUpQuery[executeLookUpTcsTransacaoMenuChildTcsModuloMenu];LookUpFinalize[finalizeLookUpTcsTransacaoMenuChildTcsModuloMenu];LookUpDisplayColumns[{\"DescModulo\" : \"Módulo\", \"DescAplicativo\" : \"Aplicativo\", \"DescModuloMenu\" : \"Menu\", \"IdModulo\" : \"\", \"IdModuloMenu\" : \"\"}];LookUpColumns[{\"DescModulo\" : true, \"DescAplicativo\" : true, \"DescModuloMenu\" : true, \"IdModulo\" : false, \"IdModuloMenu\" : false}];FilterDataKey[TCS_TRANSACAO_MENU.ID_MODULO_MENU];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdModuloMenu#true##12###4#false##::LookUpTcsTransacaoMenuChildTcsModuloMenu##true#true###Linx.Framework.BV.Transacao#IQueryable###true#false", EdmKey="TCS_TRANSACAO_MENU.ID_MODULO_MENU")]
	    public Int64 IdModuloMenu
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
	    //Extensibility Partial Method Definitions For IdTcsTransacaoMenu
	    partial void OnIdTcsTransacaoMenuChanging(Int32 value);
	    partial void OnIdTcsTransacaoMenuChanged();

	    private Int32 _IdTcsTransacaoMenu;

	    [DataMember(IsRequired = true, Name = "IdTcsTransacaoMenu", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Transacao Menu", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_MENU.ID_TCS_TRANSACAO_MENU];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_MENU.ID_TCS_TRANSACAO_MENU")]
	    public Int32 IdTcsTransacaoMenu
	    {
	    	    get
	    	    {
	    	          return _IdTcsTransacaoMenu;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsTransacaoMenu != value)
	    	          {
	    	              this.ValidateProperty("IdTcsTransacaoMenu", value);
	    	              this.OnIdTcsTransacaoMenuChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsTransacaoMenu");
	    	              this._IdTcsTransacaoMenu = value;
	    	              this.RaiseDataMemberChanged("IdTcsTransacaoMenu");
	    	              this.OnIdTcsTransacaoMenuChanged();
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
	    [Display(Name = "Id Transacao", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_MENU.ID_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_MENU.ID_TRANSACAO")]
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
	    //Extensibility Partial Method Definitions For Inativo
	    partial void OnInativoChanging(Boolean value);
	    partial void OnInativoChanged();

	    private Boolean _Inativo;

	    [DataMember(IsRequired = true, Name = "Inativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inativo", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_MENU.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_MENU.INATIVO")]
	    public Boolean Inativo
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
	    //Extensibility Partial Method Definitions For OrdemNavegacao
	    partial void OnOrdemNavegacaoChanging(Byte value);
	    partial void OnOrdemNavegacaoChanged();

	    private Byte _OrdemNavegacao;

	    [DataMember(IsRequired = true, Name = "OrdemNavegacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ordem", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_MENU.ORDEM_NAVEGACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_MENU.ORDEM_NAVEGACAO")]
	    public Byte OrdemNavegacao
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
	    //Extensibility Partial Method Definitions For SugestaoLinx
	    partial void OnSugestaoLinxChanging(Boolean value);
	    partial void OnSugestaoLinxChanged();

	    private Boolean _SugestaoLinx;

	    [DataMember(IsRequired = true, Name = "SugestaoLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Sugestao Linx", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_MENU.SUGESTAO_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_MENU.SUGESTAO_LINX")]
	    public Boolean SugestaoLinx
	    {
	    	    get
	    	    {
	    	          return _SugestaoLinx;
	    	    }
	    	    set
	    	    {
	    	          if (this._SugestaoLinx != value)
	    	          {
	    	              this.ValidateProperty("SugestaoLinx", value);
	    	              this.OnSugestaoLinxChanging(value);
	    	              this.RaiseDataMemberChanging("SugestaoLinx");
	    	              this._SugestaoLinx = value;
	    	              this.RaiseDataMemberChanged("SugestaoLinx");
	    	              this.OnSugestaoLinxChanged();
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
	    [Display(Name = "Menu", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[''];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#DescModuloMenu#false##60:0##Menu#2#true##::LookUpTcsTransacaoMenuChildTcsModuloMenu##true#true###Linx.Framework.BV.Transacao#IQueryable###true#false", EdmKey="\"\"")]
	    public string DescModuloMenu
	    {
	    	    get
	    	    {
	    	          if (_DescModuloMenu != (GetDescModuloMenu()))
	    	             _DescModuloMenu =  GetDescModuloMenu();
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
	    //Extensibility Partial Method Definitions For IdModulo
	    partial void OnIdModuloChanging(Int64 value);
	    partial void OnIdModuloChanged();

	    private Int64 _IdModulo;

	    [DataMember(IsRequired = true, Name = "IdModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "IdModulo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[0];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdModulo#false##12###3#false##::LookUpTcsTransacaoMenuChildTcsModuloMenu##true#true###Linx.Framework.BV.Transacao#IQueryable###true#false", EdmKey="0")]
	    public Int64 IdModulo
	    {
	    	    get
	    	    {
	    	          if (_IdModulo != (GetIdModulo()))
	    	             _IdModulo =  GetIdModulo();
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
	    //Extensibility Partial Method Definitions For DescModulo
	    partial void OnDescModuloChanging(string value);
	    partial void OnDescModuloChanged();

	    private string _DescModulo;

	    [DataMember(IsRequired = true, Name = "DescModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Módulo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[''];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#DescModulo#false##60:0##Módulo#0#true##::LookUpTcsTransacaoMenuChildTcsModuloMenu##true#true###Linx.Framework.BV.Transacao#IQueryable###true#false", EdmKey="\"\"")]
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
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#DescAplicativo#false##60:0##Aplicativo#1#true##::LookUpTcsTransacaoMenuChildTcsModuloMenu##true#true###Linx.Framework.BV.Transacao#IQueryable###true#false", EdmKey="")]
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

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsTransacao _TcsTransacao;
	    [DataMember(Name = "TcsTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsTransacao_TcsTransacaoMenuChild", "IdTransacao", "IdTransacao", IsForeignKey=true)]
	    public TcsTransacao TcsTransacao
	    {
	        get
	        {
	            return this._TcsTransacao;
	        }
	        set
	        {
	            if (this._TcsTransacao != value)
	            {
	                this._TcsTransacao = value;
	                this.RaisePropertyChanged("TcsTransacaoList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_TRANSACAO_MENU").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_TRANSACAO_MENU), QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_MENU" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_MENU.INATIVO", Source = "Inativo", Target = "INATIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_MENU", RelationPropertyName = "TCS_TRANSACAO_MENU" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_MENU.ID_TRANSACAO", Source = "IdTransacao", Target = "ID_TRANSACAO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_MENU", RelationPropertyName = "TCS_TRANSACAO_MENU" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_MENU.SUGESTAO_LINX", Source = "SugestaoLinx", Target = "SUGESTAO_LINX", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_MENU", RelationPropertyName = "TCS_TRANSACAO_MENU" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_MENU.ID_MODULO_MENU", Source = "IdModuloMenu", Target = "ID_MODULO_MENU", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_MENU", RelationPropertyName = "TCS_TRANSACAO_MENU" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_MENU.ORDEM_NAVEGACAO", Source = "OrdemNavegacao", Target = "ORDEM_NAVEGACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_MENU", RelationPropertyName = "TCS_TRANSACAO_MENU" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_MENU.ID_TCS_TRANSACAO_MENU", Source = "IdTcsTransacaoMenu", Target = "ID_TCS_TRANSACAO_MENU", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_MENU", RelationPropertyName = "TCS_TRANSACAO_MENU" });

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
	             Linx.Framework.BV.BusinessMediaHelper.SyncMedia("TCS_TRANSACAO_MENU", this.IdTcsTransacaoMenu, null, this.TableMedia.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(e => new Guid(e)).ToList());
	         }
	         else if (operation == DomainOperation.Delete) {
	             Linx.Framework.BV.BusinessMediaHelper.SyncMedia("TCS_TRANSACAO_MENU", this.IdTcsTransacaoMenu, null, new List<Guid>() { Guid.Empty });
	         }
	    }

	    #endregion Media Storage

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_TRANSACAO_DEPENDENTE.ID_TRANSACAO_DEPENDENTE", IsUpdatable=true, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Transações Dependentes];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTransacaoDependente];ReadOnly[false];Entities[TCS_TRANSACAO_DEPENDENTE:IdTransacaoDependente];SubQueryInfo[Select 1 From #ParentAlias#.TCS_TRANSACAO_DEPENDENTE_LISTA as #Alias#];EdmEntityName[TCS_TRANSACAO_DEPENDENTE];EntityRelations[TCS_TRANSACAO(TCS_TRANSACAO)];EdmParentEntityName[TCS_TRANSACAO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsTransacaoDependente")]
	[Serializable()]
	public partial class TcsTransacaoDependente : Linx.Data.Entity
	{

	

	    public TcsTransacaoDependente() : this(true) { }

	    public TcsTransacaoDependente(bool setDefaults) 
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
		

	    public void LoadParent(TransacaoDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsTransacao");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTransacao"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdTransacao));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsTransacao
	         this.TcsTransacao = (from r in context.GetTcsTransacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For CompartilhaBoPrincipal
	    partial void OnCompartilhaBoPrincipalChanging(Boolean value);
	    partial void OnCompartilhaBoPrincipalChanged();

	    private Boolean _CompartilhaBoPrincipal;

	    [DataMember(IsRequired = true, Name = "CompartilhaBoPrincipal", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Compartilha BO Principal", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE.COMPARTILHA_BO_PRINCIPAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE.COMPARTILHA_BO_PRINCIPAL")]
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
	    //Extensibility Partial Method Definitions For ExecutaPesquisa
	    partial void OnExecutaPesquisaChanging(Boolean value);
	    partial void OnExecutaPesquisaChanged();

	    private Boolean _ExecutaPesquisa;

	    [DataMember(IsRequired = true, Name = "ExecutaPesquisa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Sempre Executa Pesquisa", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE.EXECUTA_PESQUISA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE.EXECUTA_PESQUISA")]
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
	    [Display(Name = "Id Transacao", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE.TCS_TRANSACAO.ID_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE.TCS_TRANSACAO.ID_TRANSACAO")]
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
	    //Extensibility Partial Method Definitions For IdTransacaoDependente
	    partial void OnIdTransacaoDependenteChanging(Int64 value);
	    partial void OnIdTransacaoDependenteChanged();

	    private Int64 _IdTransacaoDependente;

	    [DataMember(IsRequired = true, Name = "IdTransacaoDependente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Transacao Dependente", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE.ID_TRANSACAO_DEPENDENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE.ID_TRANSACAO_DEPENDENTE")]
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
	    //Extensibility Partial Method Definitions For IdTransacaoRelacionada
	    partial void OnIdTransacaoRelacionadaChanging(Int64 value);
	    partial void OnIdTransacaoRelacionadaChanged();

	    private Int64 _IdTransacaoRelacionada;

	    [DataMember(IsRequired = true, Name = "IdTransacaoRelacionada", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Transacao Relacionada", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsTransacaoDependente];LookUpTitle[Seleção de (Id Transacao Relacionada)];LookUpQuery[executeLookUpTcsTransacaoDependente];LookUpFinalize[finalizeLookUpTcsTransacaoDependente];LookUpDisplayColumns[{\"IdTransacao\" : \"\", \"DescTransacao\" : \"Transação\", \"ClasseNome\" : \"Código Transação\"}];LookUpColumns[{\"IdTransacao\" : false, \"DescTransacao\" : true, \"ClasseNome\" : true}];FilterDataKey[TCS_TRANSACAO_DEPENDENTE.ID_TRANSACAO_RELACIONADA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdTransacao#true##12###0#false##::LookUpTcsTransacaoDependente##false#false###Linx.Framework.BV.Transacao#IQueryable###true#false", EdmKey="TCS_TRANSACAO_DEPENDENTE.ID_TRANSACAO_RELACIONADA")]
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
	    //Extensibility Partial Method Definitions For LxPosicaoDaTransacao
	    partial void OnLxPosicaoDaTransacaoChanging(Byte value);
	    partial void OnLxPosicaoDaTransacaoChanged();

	    private Byte _LxPosicaoDaTransacao;

	    [DataMember(IsRequired = true, Name = "LxPosicaoDaTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Posição", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[PosicaoDaTransacao];KpiName[];KpiRelatedAttribute[];DefaultValue[1];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE.LX_POSICAO_DA_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE.LX_POSICAO_DA_TRANSACAO")]
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
	    [Display(Name = "Tipo do Layout", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoLayoutDependente];KpiName[];KpiRelatedAttribute[];DefaultValue[7];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE.LX_TIPO_LAYOUT];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE.LX_TIPO_LAYOUT")]
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
	    [Display(Name = "Adição", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[true];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_ADICAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_ADICAO")]
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
	    [Display(Name = "Edição", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[true];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_EDICAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_EDICAO")]
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
	    [Display(Name = "Exclusão", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[true];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_EXCLUSAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_EXCLUSAO")]
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
	    [Display(Name = "Impressão", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[true];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_IMPRESSAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_IMPRESSAO")]
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
	    [Display(Name = "Layout", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[true];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_LAYOUT];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_LAYOUT")]
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
	    [Display(Name = "Limpa", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[true];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_LIMPA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_LIMPA")]
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
	    [Display(Name = "Navegação", Description="", Order = 14, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[true];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_NAVEGACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_NAVEGACAO")]
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
	    [Display(Name = "Pesquisa", Description="", Order = 15, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[true];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_PESQUISA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_PESQUISA")]
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
	    [Display(Name = "Pesquisa Especial", Description="", Order = 16, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[true];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_PESQUISA_ESP];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_PESQUISA_ESP")]
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
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE.POSSUI_TOOLBAR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE.POSSUI_TOOLBAR")]
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
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE.POSSUI_VISAO_TABULAR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE.POSSUI_VISAO_TABULAR")]
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
	    [Display(Name = "Propriedades do Detalhe", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE.PROPRIEDADES_DO_DETALHE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE.PROPRIEDADES_DO_DETALHE")]
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
	    [Display(Name = "Propriedades do Mestre", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE.PROPRIEDADES_DO_MESTRE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE.PROPRIEDADES_DO_MESTRE")]
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
	    [Display(Name = "Usa Filtros do BO Principal", Description="", Order = 22, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE.USA_FILTROS_DO_BO_PRINCIPAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE.USA_FILTROS_DO_BO_PRINCIPAL")]
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
	    [Display(Name = "Visível", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[true];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_DEPENDENTE.VISIVEL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_DEPENDENTE.VISIVEL")]
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
	    //Extensibility Partial Method Definitions For ClasseNome
	    partial void OnClasseNomeChanging(string value);
	    partial void OnClasseNomeChanged();

	    private string _ClasseNome;

	    [DataMember(IsRequired = true, Name = "ClasseNome", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Formulário / Url", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(400)]
	    [FunctionalPoint("Precision[400:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[''];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#ClasseNome#false##40:0##Código Transação#2#true##::LookUpTcsTransacaoDependente##false#false###Linx.Framework.BV.Transacao#IQueryable###true#false", EdmKey="\"\"")]
	    public string ClasseNome
	    {
	    	    get
	    	    {
	    	          if (_ClasseNome != (GetClasseNome()))
	    	             _ClasseNome =  GetClasseNome();
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
	    //Extensibility Partial Method Definitions For DescTransacao
	    partial void OnDescTransacaoChanging(string value);
	    partial void OnDescTransacaoChanged();

	    private string _DescTransacao;

	    [DataMember(IsRequired = true, Name = "DescTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição Detalhada", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[''];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#DescTransacao#false##60:0##Transação#1#true##::LookUpTcsTransacaoDependente##false#false###Linx.Framework.BV.Transacao#IQueryable###true#false", EdmKey="\"\"")]
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
	 
	    private TcsTransacao _TcsTransacao;
	    [DataMember(Name = "TcsTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsTransacao_TcsTransacaoDependente", "IdTransacao", "IdTransacao", IsForeignKey=true)]
	    public TcsTransacao TcsTransacao
	    {
	        get
	        {
	            return this._TcsTransacao;
	        }
	        set
	        {
	            if (this._TcsTransacao != value)
	            {
	                this._TcsTransacao = value;
	                this.RaisePropertyChanged("TcsTransacaoList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_TRANSACAO_DEPENDENTE").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_TRANSACAO_DEPENDENTE), QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_DEPENDENTE" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE.VISIVEL", Source = "Visivel", Target = "VISIVEL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_DEPENDENTE", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE.LX_TIPO_LAYOUT", Source = "LxTipoLayout", Target = "LX_TIPO_LAYOUT", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_DEPENDENTE", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE.POSSUI_TOOLBAR", Source = "PossuiToolbar", Target = "POSSUI_TOOLBAR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_DEPENDENTE", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE.EXECUTA_PESQUISA", Source = "ExecutaPesquisa", Target = "EXECUTA_PESQUISA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_DEPENDENTE", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_LIMPA", Source = "MostraBotaoLimpa", Target = "MOSTRA_BOTAO_LIMPA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_DEPENDENTE", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_ADICAO", Source = "MostraBotaoAdicao", Target = "MOSTRA_BOTAO_ADICAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_DEPENDENTE", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_EDICAO", Source = "MostraBotaoEdicao", Target = "MOSTRA_BOTAO_EDICAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_DEPENDENTE", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_LAYOUT", Source = "MostraBotaoLayout", Target = "MOSTRA_BOTAO_LAYOUT", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_DEPENDENTE", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE.POSSUI_VISAO_TABULAR", Source = "PossuiVisaoTabular", Target = "POSSUI_VISAO_TABULAR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_DEPENDENTE", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_EXCLUSAO", Source = "MostraBotaoExclusao", Target = "MOSTRA_BOTAO_EXCLUSAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_DEPENDENTE", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_PESQUISA", Source = "MostraBotaoPesquisa", Target = "MOSTRA_BOTAO_PESQUISA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_DEPENDENTE", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_IMPRESSAO", Source = "MostraBotaoImpressao", Target = "MOSTRA_BOTAO_IMPRESSAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_DEPENDENTE", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_NAVEGACAO", Source = "MostraBotaoNavegacao", Target = "MOSTRA_BOTAO_NAVEGACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_DEPENDENTE", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE.PROPRIEDADES_DO_MESTRE", Source = "PropriedadesDoMestre", Target = "PROPRIEDADES_DO_MESTRE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_DEPENDENTE", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE.ID_TRANSACAO_DEPENDENTE", Source = "IdTransacaoDependente", Target = "ID_TRANSACAO_DEPENDENTE", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_DEPENDENTE", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE.LX_POSICAO_DA_TRANSACAO", Source = "LxPosicaoDaTransacao", Target = "LX_POSICAO_DA_TRANSACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_DEPENDENTE", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE.PROPRIEDADES_DO_DETALHE", Source = "PropriedadesDoDetalhe", Target = "PROPRIEDADES_DO_DETALHE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_DEPENDENTE", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE.COMPARTILHA_BO_PRINCIPAL", Source = "CompartilhaBoPrincipal", Target = "COMPARTILHA_BO_PRINCIPAL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_DEPENDENTE", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE.ID_TRANSACAO_RELACIONADA", Source = "IdTransacaoRelacionada", Target = "ID_TRANSACAO_RELACIONADA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_DEPENDENTE", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_PESQUISA_ESP", Source = "MostraBotaoPesquisaEsp", Target = "MOSTRA_BOTAO_PESQUISA_ESP", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_DEPENDENTE", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE.TCS_TRANSACAO.ID_TRANSACAO", Source = "IdTransacao", Target = "ID_TRANSACAO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO", RelationPropertyName = "TCS_TRANSACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_DEPENDENTE.USA_FILTROS_DO_BO_PRINCIPAL", Source = "UsaFiltrosDoBoPrincipal", Target = "USA_FILTROS_DO_BO_PRINCIPAL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_DEPENDENTE", RelationPropertyName = "TCS_TRANSACAO_DEPENDENTE" });

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
	             Linx.Framework.BV.BusinessMediaHelper.SyncMedia("TCS_TRANSACAO_DEPENDENTE", this.IdTransacaoDependente, null, this.TableMedia.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(e => new Guid(e)).ToList());
	         }
	         else if (operation == DomainOperation.Delete) {
	             Linx.Framework.BV.BusinessMediaHelper.SyncMedia("TCS_TRANSACAO_DEPENDENTE", this.IdTransacaoDependente, null, new List<Guid>() { Guid.Empty });
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

		

	[LinxPublicationView(PrimaryKeys="TCS_TRANSACAO_MENU.ID_MODULO_MENU,TCS_TRANSACAO_MENU.ID_TRANSACAO", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsTransacaoMenu];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[];EdmEntityName[TCS_TRANSACAO_MENU];EntityRelations[];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsTransacaoMenu")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Transacao.TcsTransacaoMenu")]
	public partial class TcsTransacaoMenu : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For IdModuloMenu
	    partial void OnIdModuloMenuChanging(Int64 value);
	    partial void OnIdModuloMenuChanged();

	    private Int64 _IdModuloMenu;

	    [DataMember(IsRequired = true, Name = "IdModuloMenu", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo Menu", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_MENU.ID_MODULO_MENU];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_MENU.ID_MODULO_MENU")]
	    public Int64 IdModuloMenu
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
	    //Extensibility Partial Method Definitions For IdTransacao
	    partial void OnIdTransacaoChanging(Int64 value);
	    partial void OnIdTransacaoChanged();

	    private Int64 _IdTransacao;

	    [DataMember(IsRequired = true, Name = "IdTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Transacao", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_MENU.ID_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_MENU.ID_TRANSACAO")]
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
	    //Extensibility Partial Method Definitions For Inativo
	    partial void OnInativoChanging(Boolean value);
	    partial void OnInativoChanged();

	    private Boolean _Inativo;

	    [DataMember(IsRequired = true, Name = "Inativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inativo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_MENU.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_MENU.INATIVO")]
	    public Boolean Inativo
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
	    //Extensibility Partial Method Definitions For OrdemNavegacao
	    partial void OnOrdemNavegacaoChanging(Byte value);
	    partial void OnOrdemNavegacaoChanged();

	    private Byte _OrdemNavegacao;

	    [DataMember(IsRequired = true, Name = "OrdemNavegacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ordem Navegacao", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_MENU.ORDEM_NAVEGACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_MENU.ORDEM_NAVEGACAO")]
	    public Byte OrdemNavegacao
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
	    //Extensibility Partial Method Definitions For SugestaoLinx
	    partial void OnSugestaoLinxChanging(Boolean value);
	    partial void OnSugestaoLinxChanged();

	    private Boolean _SugestaoLinx;

	    [DataMember(IsRequired = true, Name = "SugestaoLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Sugestao Linx", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_MENU.SUGESTAO_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_MENU.SUGESTAO_LINX")]
	    public Boolean SugestaoLinx
	    {
	    	    get
	    	    {
	    	          return _SugestaoLinx;
	    	    }
	    	    set
	    	    {
	    	          if (this._SugestaoLinx != value)
	    	          {
	    	              this.ValidateProperty("SugestaoLinx", value);
	    	              this.OnSugestaoLinxChanging(value);
	    	              this.RaiseDataMemberChanging("SugestaoLinx");
	    	              this._SugestaoLinx = value;
	    	              this.RaiseDataMemberChanged("SugestaoLinx");
	    	              this.OnSugestaoLinxChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_TRANSACAO_MENU").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_TRANSACAO_MENU), QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_MENU" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_MENU.INATIVO", Source = "Inativo", Target = "INATIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_MENU", RelationPropertyName = "TCS_TRANSACAO_MENU" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_MENU.ID_TRANSACAO", Source = "IdTransacao", Target = "ID_TRANSACAO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_MENU", RelationPropertyName = "TCS_TRANSACAO_MENU" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_MENU.SUGESTAO_LINX", Source = "SugestaoLinx", Target = "SUGESTAO_LINX", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_MENU", RelationPropertyName = "TCS_TRANSACAO_MENU" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_MENU.ID_MODULO_MENU", Source = "IdModuloMenu", Target = "ID_MODULO_MENU", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_MENU", RelationPropertyName = "TCS_TRANSACAO_MENU" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_MENU.ORDEM_NAVEGACAO", Source = "OrdemNavegacao", Target = "ORDEM_NAVEGACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_MENU", RelationPropertyName = "TCS_TRANSACAO_MENU" });

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

		

	[LinxPublicationView(PrimaryKeys="TCS_MODULO_MENU.ID_MODULO_MENU", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsModuloMenuP];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdModuloMenu];ReadOnly[false];Entities[TCS_MODULO_MENU:IdModuloMenu];SubQueryInfo[];EdmEntityName[TCS_MODULO_MENU];EntityRelations[TCS_MODULO(TCS_MODULO)#MODULO_MENU_SUPERIOR(TCS_MODULO_MENU)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsModuloMenuP")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Transacao.TcsModuloMenuP")]
	public partial class TcsModuloMenuP : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For DescModulo
	    partial void OnDescModuloChanging(string value);
	    partial void OnDescModuloChanged();

	    private string _DescModulo;

	    [DataMember(IsRequired = true, Name = "DescModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[''];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="\"\"")]
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
	    partial void OnDescModuloMenuChanging(System.String value);
	    partial void OnDescModuloMenuChanged();

	    private System.String _DescModuloMenu;

	    [DataMember(IsRequired = true, Name = "DescModuloMenu", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Desc Modulo Menu", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO_MENU.DESC_MODULO_MENU];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_MENU.DESC_MODULO_MENU")]
	    public System.String DescModuloMenu
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
	    //Extensibility Partial Method Definitions For IdModulo
	    partial void OnIdModuloChanging(Int64 value);
	    partial void OnIdModuloChanged();

	    private Int64 _IdModulo;

	    [DataMember(IsRequired = true, Name = "IdModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO_MENU.TCS_MODULO.ID_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_MENU.TCS_MODULO.ID_MODULO")]
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
	    //Extensibility Partial Method Definitions For IdModuloMenu
	    partial void OnIdModuloMenuChanging(Int64 value);
	    partial void OnIdModuloMenuChanged();

	    private Int64 _IdModuloMenu;

	    [DataMember(IsRequired = true, Name = "IdModuloMenu", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo Menu", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO_MENU.ID_MODULO_MENU];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_MENU.ID_MODULO_MENU")]
	    public Int64 IdModuloMenu
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
	    //Extensibility Partial Method Definitions For IdTcsAplicativo
	    partial void OnIdTcsAplicativoChanging(Int32 value);
	    partial void OnIdTcsAplicativoChanged();

	    private Int32 _IdTcsAplicativo;

	    [DataMember(IsRequired = true, Name = "IdTcsAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Aplicativo", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[IdAplicativo];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO_MENU.TCS_MODULO.ID_TCS_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_MENU.TCS_MODULO.ID_TCS_APLICATIVO")]
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

	    private Int64 _TemporaryIdModuloMenu;
	    [DataMember(Name = "TemporaryIdModuloMenu", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo Menu (Tmp)", Description="Temporary Key", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdModuloMenu
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdModuloMenu.IsNullOrEmpty())
	    	                this._TemporaryIdModuloMenu = this._IdModuloMenu;
	    	          return this._TemporaryIdModuloMenu;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdModuloMenu != value)
	    	              this._TemporaryIdModuloMenu = value;
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_MODULO_MENU").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_MODULO_MENU), QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO_MENU" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_MENU.ID_MODULO_MENU", Source = "IdModuloMenu", Target = "ID_MODULO_MENU", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO_MENU", RelationPropertyName = "TCS_MODULO_MENU" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_MENU.DESC_MODULO_MENU", Source = "DescModuloMenu", Target = "DESC_MODULO_MENU", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO_MENU", RelationPropertyName = "TCS_MODULO_MENU" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_MENU.TCS_MODULO.ID_MODULO", Source = "IdModulo", Target = "ID_MODULO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO", RelationPropertyName = "TCS_MODULO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetIdTcsAplicativoValues()
	    {
	    	    return Linx.Framework.BV.Domains.IdAplicativo.GetValues();
	    }
	    private string _idTcsAplicativoName;
	    [DataMember(IsRequired = false, Name = "IdTcsAplicativoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Id Tcs Aplicativo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string IdTcsAplicativoName
	    {
	    	    get { if (this.IdTcsAplicativo.IsNull()) { _idTcsAplicativoName = String.Empty; } else { string key = this.IdTcsAplicativo.ToString(); var dmValues = this.GetIdTcsAplicativoValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _idTcsAplicativoName) _idTcsAplicativoName = domainName; } return _idTcsAplicativoName; } set { _idTcsAplicativoName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	//////////////////////// DomainService Class V1 ///////////////////////
	///////////////////////////////////////////////////////////////////////
	[EnableClientAccess()]	
	[DomainIdentifier("ProcessorOverviewTransacaoDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class TransacaoDomainService : DomainService, IDataServiceContext 
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

		
	    public TransacaoDomainService() : this("", null, null) { }
	    public TransacaoDomainService(string connectionString) : this(connectionString, null, null) { }
	    public TransacaoDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public TransacaoDomainService(Linx.Framework.ControleSistema.BM.ControleSistemaContext dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public TransacaoDomainService(string connectionString, Linx.Framework.ControleSistema.BM.ControleSistemaContext dataContext, Dictionary<string, string> headers) : base() 
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
	
		
	    }
	
	    private void SaveMedia(ChangeSet changeSet)
	    {
	    		foreach (ChangeSetEntry entry in changeSet.ChangeSetEntries)
	    		{
	    			if (entry.Entity is TcsTransacao) ((TcsTransacao)entry.Entity).SaveMedia(entry.Operation);
	    			if (entry.Entity is TcsTransacaoMenuChild) ((TcsTransacaoMenuChild)entry.Entity).SaveMedia(entry.Operation);
	    			if (entry.Entity is TcsTransacaoDependente) ((TcsTransacaoDependente)entry.Entity).SaveMedia(entry.Operation);
	    		}
	    }

	    private void OnSavedChanges(ChangeSet changeSet)
	    {
	
	
	        TcsTransacao.OnSavedContextChanges(this, changeSet.ChangeSetEntries.Where(e => e.Entity is TcsTransacao).ToArray());
    	
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
 	        var _TcsTransacaoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsTransacao && e.Entity.GetType().Name == "TcsTransacao" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _TcsTransacaoElements)
 	           if (((TcsTransacao)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 
 	        //Remove inconsistent details
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsTransacaoMenuChild && e.Entity.GetType().Name == "TcsTransacaoMenuChild" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsTransacaoDependente && e.Entity.GetType().Name == "TcsTransacaoDependente" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
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
	    //Get All LookUpTcsObjetoTransacao.
	    public IQueryable<LookUpTcsObjetoTransacao> GetAllLookUpTcsObjetoTransacao()
	    {
	        return this.GetLookUpTcsObjetoTransacao(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsObjetoTransacao By EntitySearch.
	    public IQueryable<LookUpTcsObjetoTransacao> GetLookUpTcsObjetoTransacaoByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsObjetoTransacao(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsObjetoTransacao.
	    public IQueryable<LookUpTcsObjetoTransacao> GetLookUpTcsObjetoTransacao(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsObjetoTransacao";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsObjetoTransacao));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsObjetoTransacao> query =  null;
		
			
		
	        TcsTransacao.OnLookUpingLookUpTcsObjetoTransacao(ref query, propertyName, entitySearch);
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpTcsTransacaoMenuChildTcsModuloMenu.
	    public IQueryable<LookUpTcsTransacaoMenuChildTcsModuloMenu> GetAllLookUpTcsTransacaoMenuChildTcsModuloMenu()
	    {
	        return this.GetLookUpTcsTransacaoMenuChildTcsModuloMenu(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsTransacaoMenuChildTcsModuloMenu By EntitySearch.
	    public IQueryable<LookUpTcsTransacaoMenuChildTcsModuloMenu> GetLookUpTcsTransacaoMenuChildTcsModuloMenuByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsTransacaoMenuChildTcsModuloMenu(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsTransacaoMenuChildTcsModuloMenu.
	    public IQueryable<LookUpTcsTransacaoMenuChildTcsModuloMenu> GetLookUpTcsTransacaoMenuChildTcsModuloMenu(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsTransacaoMenuChildTcsModuloMenu";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsTransacaoMenuChildTcsModuloMenu));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsTransacaoMenuChildTcsModuloMenu> query =  null;
		
			
		
	        TcsTransacaoMenuChild.OnLookUpingLookUpTcsTransacaoMenuChildTcsModuloMenu(ref query, propertyName, entitySearch);
	
	
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
		
			
		
	        TcsTransacaoDependente.OnLookUpingLookUpTcsTransacaoDependente(ref query, propertyName, entitySearch);
	
	
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
	
		

	        if (entityName.InList("Linx.Framework.BV.Transacao.TcsTransacao"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsTransacao",
	        			NameSpace = "Linx.Framework.BV.Transacao",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "Transações",
	        			ClearMethodName = "ClearTcsTransacao",
	        			QueryMethodName  = "GetPagedTcsTransacao",	
	        			CountingMethodName  = "GetTcsTransacao" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Transacao.TcsTransacao"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Transacao.TcsTransacao"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Transacao.TcsTransacao", "Linx.Framework.BV.Transacao.TcsTransacaoMenuChild"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsTransacaoMenuChild",
	        			NameSpace = "Linx.Framework.BV.Transacao",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsTransacao",	
	        			DisplayName = "Menu",
	        			ClearMethodName = "ClearTcsTransacaoMenuChild",
	        			QueryMethodName  = "GetPagedTcsTransacaoMenuChild",	
	        			CountingMethodName  = "GetTcsTransacaoMenuChild" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Transacao.TcsTransacaoMenuChild"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Transacao.TcsTransacaoMenuChild"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Transacao.TcsTransacao", "Linx.Framework.BV.Transacao.TcsTransacaoDependente"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsTransacaoDependente",
	        			NameSpace = "Linx.Framework.BV.Transacao",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsTransacao",	
	        			DisplayName = "Transações Dependentes",
	        			ClearMethodName = "ClearTcsTransacaoDependente",
	        			QueryMethodName  = "GetPagedTcsTransacaoDependente",	
	        			CountingMethodName  = "GetTcsTransacaoDependente" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Transacao.TcsTransacaoDependente"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Transacao.TcsTransacaoDependente"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Transacao.TcsTransacaoMenu"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsTransacaoMenu",
	        			NameSpace = "Linx.Framework.BV.Transacao",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsTransacaoMenu",
	        			ClearMethodName = "ClearTcsTransacaoMenu",
	        			QueryMethodName  = "GetPagedTcsTransacaoMenu",	
	        			CountingMethodName  = "GetTcsTransacaoMenu" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Transacao.TcsTransacaoMenu"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Transacao.TcsTransacaoMenu"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Transacao.TcsModuloMenuP"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsModuloMenuP",
	        			NameSpace = "Linx.Framework.BV.Transacao",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsModuloMenuP",
	        			ClearMethodName = "ClearTcsModuloMenuP",
	        			QueryMethodName  = "GetPagedTcsModuloMenuP",	
	        			CountingMethodName  = "GetTcsModuloMenuP" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Transacao.TcsModuloMenuP"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Transacao.TcsModuloMenuP"), forceAll: forceAll)
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

         		    return new string[] { "Framework_TransacaoClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.TransacaoClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Framework_transacaoService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.transacaoService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear TcsTransacao.
	    public IEnumerable<TcsTransacao> ClearTcsTransacao()
	    {
	        List<TcsTransacao> result = new List<TcsTransacao>();
	        result.Add(new TcsTransacao(false));	
			
	        result[0].TcsTransacaoMenuChildList = new List<TcsTransacaoMenuChild>();
	        ((List<TcsTransacaoMenuChild>)result[0].TcsTransacaoMenuChildList).Add(new TcsTransacaoMenuChild());
			
	        result[0].TcsTransacaoDependenteList = new List<TcsTransacaoDependente>();
	        ((List<TcsTransacaoDependente>)result[0].TcsTransacaoDependenteList).Add(new TcsTransacaoDependente(false));
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsTransacaoMenuChild.
	    public IEnumerable<TcsTransacaoMenuChild> ClearTcsTransacaoMenuChild()
	    {
	        List<TcsTransacaoMenuChild> result = new List<TcsTransacaoMenuChild>();
	        result.Add(new TcsTransacaoMenuChild());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsTransacaoDependente.
	    public IEnumerable<TcsTransacaoDependente> ClearTcsTransacaoDependente()
	    {
	        List<TcsTransacaoDependente> result = new List<TcsTransacaoDependente>();
	        result.Add(new TcsTransacaoDependente(false));	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsTransacaoMenu.
	    public IEnumerable<TcsTransacaoMenu> ClearTcsTransacaoMenu()
	    {
	        List<TcsTransacaoMenu> result = new List<TcsTransacaoMenu>();
	        result.Add(new TcsTransacaoMenu());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsModuloMenuP.
	    public IEnumerable<TcsModuloMenuP> ClearTcsModuloMenuP()
	    {
	        List<TcsModuloMenuP> result = new List<TcsModuloMenuP>();
	        result.Add(new TcsModuloMenuP());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsTransacao.
	    public IQueryable<TcsTransacao> GetTcsTransacao()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsTransacao> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO
	            
	            	
	            select new TcsTransacao()		
	            {
	            
                ClasseNome = entity0.CLASSE_NOME
                , CodTransacao = entity0.COD_TRANSACAO
                , DescTransacao = entity0.DESC_TRANSACAO
                , Icone = entity0.ICONE
                , IdObjeto = entity0.ID_OBJETO
                , IdTransacao = entity0.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , LxCorFundoName = ((entity0.LX_COR_FUNDO) == 8 ? "Fundo Laranja" : ((entity0.LX_COR_FUNDO) == 10 ? "Fundo Roxo" : ((entity0.LX_COR_FUNDO) == 7 ? "Laranja" : ((entity0.LX_COR_FUNDO) == 9 ? "Roxo" : ""))))
                , LxTipoTransacao = entity0.LX_TIPO_TRANSACAO
                , LxTipoTransacaoName = ((entity0.LX_TIPO_TRANSACAO) == 7 ? "Assistente" : ((entity0.LX_TIPO_TRANSACAO) == 8 ? "Dashboard" : ((entity0.LX_TIPO_TRANSACAO) == 2 ? "ERP" : ((entity0.LX_TIPO_TRANSACAO) == 6 ? "ERP App" : ((entity0.LX_TIPO_TRANSACAO) == 4 ? "Excel" : ((entity0.LX_TIPO_TRANSACAO) == 3 ? "Loja" : ((entity0.LX_TIPO_TRANSACAO) == 5 ? "Mobile" : ((entity0.LX_TIPO_TRANSACAO) == 1 ? "Todos" : ""))))))))
                , NomeCurto = entity0.NOME_CURTO
                , Tag = entity0.TAG
                , DescObjeto = ""
                , ClasseNomeObjeto = ""
			
                ,TcsTransacaoDependenteList = 
	                        (from entity1 in entity0.TCS_TRANSACAO_DEPENDENTE_LISTA
                                  let entity1Al1 = entity1.TCS_TRANSACAO
	                        
	                        	
	                        select new TcsTransacaoDependente()
	                        {
	                        
                                CompartilhaBoPrincipal = entity1.COMPARTILHA_BO_PRINCIPAL
                                , ExecutaPesquisa = entity1.EXECUTA_PESQUISA
                                , IdTransacao = entity1Al1.ID_TRANSACAO
                                , IdTransacaoDependente = entity1.ID_TRANSACAO_DEPENDENTE
                                , IdTransacaoRelacionada = entity1.ID_TRANSACAO_RELACIONADA
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
                                , ClasseNome = ""
                                , DescTransacao = ""
		
	                        }
	                        )
		
	            }
	            );
		
	
	        TcsTransacao.OnSearching(ref result, false, null);	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsTransacaoMenuChild.
	    public IQueryable<TcsTransacaoMenuChild> GetTcsTransacaoMenuChild()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoMenuChild> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_MENU
	            
	            	
	            select new TcsTransacaoMenuChild()		
	            {
	            
                IdModuloMenu = entity0.ID_MODULO_MENU
                , IdTcsTransacaoMenu = entity0.ID_TCS_TRANSACAO_MENU
                , IdTransacao = entity0.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
                , SugestaoLinx = entity0.SUGESTAO_LINX
                , DescModuloMenu = ""
                , IdModulo = 0
                , DescModulo = ""
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsTransacaoDependente.
	    public IQueryable<TcsTransacaoDependente> GetTcsTransacaoDependente()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoDependente> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_DEPENDENTE
                  let entity0Al1 = entity0.TCS_TRANSACAO
	            
	            	
	            select new TcsTransacaoDependente()		
	            {
	            
                CompartilhaBoPrincipal = entity0.COMPARTILHA_BO_PRINCIPAL
                , ExecutaPesquisa = entity0.EXECUTA_PESQUISA
                , IdTransacao = entity0Al1.ID_TRANSACAO
                , IdTransacaoDependente = entity0.ID_TRANSACAO_DEPENDENTE
                , IdTransacaoRelacionada = entity0.ID_TRANSACAO_RELACIONADA
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
                , ClasseNome = ""
                , DescTransacao = ""
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsTransacaoNoAssociations.
	    public IQueryable<TcsTransacao> GetTcsTransacaoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsTransacao> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO
	            
	            	
	            select new TcsTransacao()		
	            {
	            
                ClasseNome = entity0.CLASSE_NOME
                , CodTransacao = entity0.COD_TRANSACAO
                , DescTransacao = entity0.DESC_TRANSACAO
                , Icone = entity0.ICONE
                , IdObjeto = entity0.ID_OBJETO
                , IdTransacao = entity0.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , LxCorFundoName = ((entity0.LX_COR_FUNDO) == 8 ? "Fundo Laranja" : ((entity0.LX_COR_FUNDO) == 10 ? "Fundo Roxo" : ((entity0.LX_COR_FUNDO) == 7 ? "Laranja" : ((entity0.LX_COR_FUNDO) == 9 ? "Roxo" : ""))))
                , LxTipoTransacao = entity0.LX_TIPO_TRANSACAO
                , LxTipoTransacaoName = ((entity0.LX_TIPO_TRANSACAO) == 7 ? "Assistente" : ((entity0.LX_TIPO_TRANSACAO) == 8 ? "Dashboard" : ((entity0.LX_TIPO_TRANSACAO) == 2 ? "ERP" : ((entity0.LX_TIPO_TRANSACAO) == 6 ? "ERP App" : ((entity0.LX_TIPO_TRANSACAO) == 4 ? "Excel" : ((entity0.LX_TIPO_TRANSACAO) == 3 ? "Loja" : ((entity0.LX_TIPO_TRANSACAO) == 5 ? "Mobile" : ((entity0.LX_TIPO_TRANSACAO) == 1 ? "Todos" : ""))))))))
                , NomeCurto = entity0.NOME_CURTO
                , Tag = entity0.TAG
                , DescObjeto = ""
                , ClasseNomeObjeto = ""
		
	            }
	            );
		
	
	        TcsTransacao.OnSearching(ref result, true, null);	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsTransacaoMenuChildNoAssociations.
	    public IQueryable<TcsTransacaoMenuChild> GetTcsTransacaoMenuChildNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoMenuChild> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_MENU
	            
	            	
	            select new TcsTransacaoMenuChild()		
	            {
	            
                IdModuloMenu = entity0.ID_MODULO_MENU
                , IdTcsTransacaoMenu = entity0.ID_TCS_TRANSACAO_MENU
                , IdTransacao = entity0.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
                , SugestaoLinx = entity0.SUGESTAO_LINX
                , DescModuloMenu = ""
                , IdModulo = 0
                , DescModulo = ""
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsTransacaoDependenteNoAssociations.
	    public IQueryable<TcsTransacaoDependente> GetTcsTransacaoDependenteNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoDependente> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_DEPENDENTE
                  let entity0Al1 = entity0.TCS_TRANSACAO
	            
	            	
	            select new TcsTransacaoDependente()		
	            {
	            
                CompartilhaBoPrincipal = entity0.COMPARTILHA_BO_PRINCIPAL
                , ExecutaPesquisa = entity0.EXECUTA_PESQUISA
                , IdTransacao = entity0Al1.ID_TRANSACAO
                , IdTransacaoDependente = entity0.ID_TRANSACAO_DEPENDENTE
                , IdTransacaoRelacionada = entity0.ID_TRANSACAO_RELACIONADA
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
                , ClasseNome = ""
                , DescTransacao = ""
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsTransacaoMenu.
	    public IQueryable<TcsTransacaoMenu> GetTcsTransacaoMenu()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoMenu> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_MENU
	            
	            	
	            select new TcsTransacaoMenu()		
	            {
	            
                IdModuloMenu = entity0.ID_MODULO_MENU
                , IdTransacao = entity0.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
                , SugestaoLinx = entity0.SUGESTAO_LINX
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsTransacaoMenuNoAssociations.
	    public IQueryable<TcsTransacaoMenu> GetTcsTransacaoMenuNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoMenu> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_MENU
	            
	            	
	            select new TcsTransacaoMenu()		
	            {
	            
                IdModuloMenu = entity0.ID_MODULO_MENU
                , IdTransacao = entity0.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
                , SugestaoLinx = entity0.SUGESTAO_LINX
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsModuloMenuP.
	    public IQueryable<TcsModuloMenuP> GetTcsModuloMenuP()
	    {




		

	        IQueryable<TcsModuloMenuP> result = 
	            (from entity0 in TcsModuloMenuP.OnSearchingReplacement(this.DbContext, null, null, null) select entity0);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsModuloMenuPNoAssociations.
	    public IQueryable<TcsModuloMenuP> GetTcsModuloMenuPNoAssociations()
	    {




		

	        IQueryable<TcsModuloMenuP> result = 
	            (from entity0 in TcsModuloMenuP.OnSearchingReplacement(this.DbContext, null, null, null) select entity0);
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	result.Add("TcsTransacao|LxCorFundo");
	    	result.Add("TcsTransacao|TCS_TRANSACAO.LX_COR_FUNDO");
	    	result.Add("TcsTransacao|ClasseNomeObjeto");
	    	result.Add("TcsTransacao|''");
	    	//Add filtering disabled property for TCS_TRANSACAO
	    	string[] bmDisabledTcsTransacaoList = this.GetEDM().GetFilteringDisabledList("TCS_TRANSACAO");
	    	if (bmDisabledTcsTransacaoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsTransacaoList.Contains("TCS_TRANSACAO.CLASSE_NOME"))
	    		{
	    			result.Add("TcsTransacao|ClasseNome");
	    			result.Add("TcsTransacao|TCS_TRANSACAO.CLASSE_NOME");
	    		}
	
	    		if (bmDisabledTcsTransacaoList.Contains("TCS_TRANSACAO.COD_TRANSACAO"))
	    		{
	    			result.Add("TcsTransacao|CodTransacao");
	    			result.Add("TcsTransacao|TCS_TRANSACAO.COD_TRANSACAO");
	    		}
	
	    		if (bmDisabledTcsTransacaoList.Contains("TCS_TRANSACAO.DESC_TRANSACAO"))
	    		{
	    			result.Add("TcsTransacao|DescTransacao");
	    			result.Add("TcsTransacao|TCS_TRANSACAO.DESC_TRANSACAO");
	    		}
	
	    		if (bmDisabledTcsTransacaoList.Contains("TCS_TRANSACAO.ICONE"))
	    		{
	    			result.Add("TcsTransacao|Icone");
	    			result.Add("TcsTransacao|TCS_TRANSACAO.ICONE");
	    		}
	
	    		if (bmDisabledTcsTransacaoList.Contains("TCS_TRANSACAO.ID_OBJETO"))
	    		{
	    			result.Add("TcsTransacao|IdObjeto");
	    			result.Add("TcsTransacao|TCS_TRANSACAO.ID_OBJETO");
	    		}
	
	    		if (bmDisabledTcsTransacaoList.Contains("TCS_TRANSACAO.ID_TRANSACAO"))
	    		{
	    			result.Add("TcsTransacao|IdTransacao");
	    			result.Add("TcsTransacao|TCS_TRANSACAO.ID_TRANSACAO");
	    		}
	
	    		if (bmDisabledTcsTransacaoList.Contains("TCS_TRANSACAO.INATIVO"))
	    		{
	    			result.Add("TcsTransacao|Inativo");
	    			result.Add("TcsTransacao|TCS_TRANSACAO.INATIVO");
	    		}
	
	    		if (bmDisabledTcsTransacaoList.Contains("TCS_TRANSACAO.LX_TIPO_TRANSACAO"))
	    		{
	    			result.Add("TcsTransacao|LxTipoTransacao");
	    			result.Add("TcsTransacao|TCS_TRANSACAO.LX_TIPO_TRANSACAO");
	    		}
	
	    		if (bmDisabledTcsTransacaoList.Contains("TCS_TRANSACAO.NOME_CURTO"))
	    		{
	    			result.Add("TcsTransacao|NomeCurto");
	    			result.Add("TcsTransacao|TCS_TRANSACAO.NOME_CURTO");
	    		}
	
	    		if (bmDisabledTcsTransacaoList.Contains("TCS_TRANSACAO.TAG"))
	    		{
	    			result.Add("TcsTransacao|Tag");
	    			result.Add("TcsTransacao|TCS_TRANSACAO.TAG");
	    		}
	    	}
	    	result.Add("TcsTransacaoMenuChild|DescModuloMenu");
	    	result.Add("TcsTransacaoMenuChild|''");
	    	result.Add("TcsTransacaoMenuChild|IdModulo");
	    	result.Add("TcsTransacaoMenuChild|0");
	    	result.Add("TcsTransacaoMenuChild|DescModulo");
	    	result.Add("TcsTransacaoMenuChild|''");
	    	//Add filtering disabled property for TCS_TRANSACAO_MENU
	    	string[] bmDisabledTcsTransacaoMenuChildList = this.GetEDM().GetFilteringDisabledList("TCS_TRANSACAO_MENU");
	    	if (bmDisabledTcsTransacaoMenuChildList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsTransacaoMenuChildList.Contains("TCS_TRANSACAO_MENU.ID_MODULO_MENU"))
	    		{
	    			result.Add("TcsTransacaoMenuChild|IdModuloMenu");
	    			result.Add("TcsTransacaoMenuChild|TCS_TRANSACAO_MENU.ID_MODULO_MENU");
	    		}
	
	    		if (bmDisabledTcsTransacaoMenuChildList.Contains("TCS_TRANSACAO_MENU.ID_TCS_TRANSACAO_MENU"))
	    		{
	    			result.Add("TcsTransacaoMenuChild|IdTcsTransacaoMenu");
	    			result.Add("TcsTransacaoMenuChild|TCS_TRANSACAO_MENU.ID_TCS_TRANSACAO_MENU");
	    		}
	
	    		if (bmDisabledTcsTransacaoMenuChildList.Contains("TCS_TRANSACAO_MENU.ID_TRANSACAO"))
	    		{
	    			result.Add("TcsTransacaoMenuChild|IdTransacao");
	    			result.Add("TcsTransacaoMenuChild|TCS_TRANSACAO_MENU.ID_TRANSACAO");
	    		}
	
	    		if (bmDisabledTcsTransacaoMenuChildList.Contains("TCS_TRANSACAO_MENU.INATIVO"))
	    		{
	    			result.Add("TcsTransacaoMenuChild|Inativo");
	    			result.Add("TcsTransacaoMenuChild|TCS_TRANSACAO_MENU.INATIVO");
	    		}
	
	    		if (bmDisabledTcsTransacaoMenuChildList.Contains("TCS_TRANSACAO_MENU.ORDEM_NAVEGACAO"))
	    		{
	    			result.Add("TcsTransacaoMenuChild|OrdemNavegacao");
	    			result.Add("TcsTransacaoMenuChild|TCS_TRANSACAO_MENU.ORDEM_NAVEGACAO");
	    		}
	
	    		if (bmDisabledTcsTransacaoMenuChildList.Contains("TCS_TRANSACAO_MENU.SUGESTAO_LINX"))
	    		{
	    			result.Add("TcsTransacaoMenuChild|SugestaoLinx");
	    			result.Add("TcsTransacaoMenuChild|TCS_TRANSACAO_MENU.SUGESTAO_LINX");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_TRANSACAO_MENU
	    	string[] bmDisabledTcsTransacaoMenuList = this.GetEDM().GetFilteringDisabledList("TCS_TRANSACAO_MENU");
	    	if (bmDisabledTcsTransacaoMenuList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsTransacaoMenuList.Contains("TCS_TRANSACAO_MENU.ID_MODULO_MENU"))
	    		{
	    			result.Add("TcsTransacaoMenu|IdModuloMenu");
	    			result.Add("TcsTransacaoMenu|TCS_TRANSACAO_MENU.ID_MODULO_MENU");
	    		}
	
	    		if (bmDisabledTcsTransacaoMenuList.Contains("TCS_TRANSACAO_MENU.ID_TRANSACAO"))
	    		{
	    			result.Add("TcsTransacaoMenu|IdTransacao");
	    			result.Add("TcsTransacaoMenu|TCS_TRANSACAO_MENU.ID_TRANSACAO");
	    		}
	
	    		if (bmDisabledTcsTransacaoMenuList.Contains("TCS_TRANSACAO_MENU.INATIVO"))
	    		{
	    			result.Add("TcsTransacaoMenu|Inativo");
	    			result.Add("TcsTransacaoMenu|TCS_TRANSACAO_MENU.INATIVO");
	    		}
	
	    		if (bmDisabledTcsTransacaoMenuList.Contains("TCS_TRANSACAO_MENU.ORDEM_NAVEGACAO"))
	    		{
	    			result.Add("TcsTransacaoMenu|OrdemNavegacao");
	    			result.Add("TcsTransacaoMenu|TCS_TRANSACAO_MENU.ORDEM_NAVEGACAO");
	    		}
	
	    		if (bmDisabledTcsTransacaoMenuList.Contains("TCS_TRANSACAO_MENU.SUGESTAO_LINX"))
	    		{
	    			result.Add("TcsTransacaoMenu|SugestaoLinx");
	    			result.Add("TcsTransacaoMenu|TCS_TRANSACAO_MENU.SUGESTAO_LINX");
	    		}
	    	}
	    	result.Add("TcsTransacaoDependente|ClasseNome");
	    	result.Add("TcsTransacaoDependente|''");
	    	result.Add("TcsTransacaoDependente|DescTransacao");
	    	result.Add("TcsTransacaoDependente|''");
	    	//Add filtering disabled property for TCS_TRANSACAO_DEPENDENTE
	    	string[] bmDisabledTcsTransacaoDependenteList = this.GetEDM().GetFilteringDisabledList("TCS_TRANSACAO_DEPENDENTE");
	    	if (bmDisabledTcsTransacaoDependenteList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsTransacaoDependenteList.Contains("TCS_TRANSACAO_DEPENDENTE.COMPARTILHA_BO_PRINCIPAL"))
	    		{
	    			result.Add("TcsTransacaoDependente|CompartilhaBoPrincipal");
	    			result.Add("TcsTransacaoDependente|TCS_TRANSACAO_DEPENDENTE.COMPARTILHA_BO_PRINCIPAL");
	    		}
	
	    		if (bmDisabledTcsTransacaoDependenteList.Contains("TCS_TRANSACAO_DEPENDENTE.EXECUTA_PESQUISA"))
	    		{
	    			result.Add("TcsTransacaoDependente|ExecutaPesquisa");
	    			result.Add("TcsTransacaoDependente|TCS_TRANSACAO_DEPENDENTE.EXECUTA_PESQUISA");
	    		}
	
	    		if (bmDisabledTcsTransacaoDependenteList.Contains("TCS_TRANSACAO_DEPENDENTE.ID_TRANSACAO_DEPENDENTE"))
	    		{
	    			result.Add("TcsTransacaoDependente|IdTransacaoDependente");
	    			result.Add("TcsTransacaoDependente|TCS_TRANSACAO_DEPENDENTE.ID_TRANSACAO_DEPENDENTE");
	    		}
	
	    		if (bmDisabledTcsTransacaoDependenteList.Contains("TCS_TRANSACAO_DEPENDENTE.ID_TRANSACAO_RELACIONADA"))
	    		{
	    			result.Add("TcsTransacaoDependente|IdTransacaoRelacionada");
	    			result.Add("TcsTransacaoDependente|TCS_TRANSACAO_DEPENDENTE.ID_TRANSACAO_RELACIONADA");
	    		}
	
	    		if (bmDisabledTcsTransacaoDependenteList.Contains("TCS_TRANSACAO_DEPENDENTE.LX_POSICAO_DA_TRANSACAO"))
	    		{
	    			result.Add("TcsTransacaoDependente|LxPosicaoDaTransacao");
	    			result.Add("TcsTransacaoDependente|TCS_TRANSACAO_DEPENDENTE.LX_POSICAO_DA_TRANSACAO");
	    		}
	
	    		if (bmDisabledTcsTransacaoDependenteList.Contains("TCS_TRANSACAO_DEPENDENTE.LX_TIPO_LAYOUT"))
	    		{
	    			result.Add("TcsTransacaoDependente|LxTipoLayout");
	    			result.Add("TcsTransacaoDependente|TCS_TRANSACAO_DEPENDENTE.LX_TIPO_LAYOUT");
	    		}
	
	    		if (bmDisabledTcsTransacaoDependenteList.Contains("TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_ADICAO"))
	    		{
	    			result.Add("TcsTransacaoDependente|MostraBotaoAdicao");
	    			result.Add("TcsTransacaoDependente|TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_ADICAO");
	    		}
	
	    		if (bmDisabledTcsTransacaoDependenteList.Contains("TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_EDICAO"))
	    		{
	    			result.Add("TcsTransacaoDependente|MostraBotaoEdicao");
	    			result.Add("TcsTransacaoDependente|TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_EDICAO");
	    		}
	
	    		if (bmDisabledTcsTransacaoDependenteList.Contains("TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_EXCLUSAO"))
	    		{
	    			result.Add("TcsTransacaoDependente|MostraBotaoExclusao");
	    			result.Add("TcsTransacaoDependente|TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_EXCLUSAO");
	    		}
	
	    		if (bmDisabledTcsTransacaoDependenteList.Contains("TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_IMPRESSAO"))
	    		{
	    			result.Add("TcsTransacaoDependente|MostraBotaoImpressao");
	    			result.Add("TcsTransacaoDependente|TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_IMPRESSAO");
	    		}
	
	    		if (bmDisabledTcsTransacaoDependenteList.Contains("TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_LAYOUT"))
	    		{
	    			result.Add("TcsTransacaoDependente|MostraBotaoLayout");
	    			result.Add("TcsTransacaoDependente|TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_LAYOUT");
	    		}
	
	    		if (bmDisabledTcsTransacaoDependenteList.Contains("TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_LIMPA"))
	    		{
	    			result.Add("TcsTransacaoDependente|MostraBotaoLimpa");
	    			result.Add("TcsTransacaoDependente|TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_LIMPA");
	    		}
	
	    		if (bmDisabledTcsTransacaoDependenteList.Contains("TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_NAVEGACAO"))
	    		{
	    			result.Add("TcsTransacaoDependente|MostraBotaoNavegacao");
	    			result.Add("TcsTransacaoDependente|TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_NAVEGACAO");
	    		}
	
	    		if (bmDisabledTcsTransacaoDependenteList.Contains("TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_PESQUISA"))
	    		{
	    			result.Add("TcsTransacaoDependente|MostraBotaoPesquisa");
	    			result.Add("TcsTransacaoDependente|TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_PESQUISA");
	    		}
	
	    		if (bmDisabledTcsTransacaoDependenteList.Contains("TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_PESQUISA_ESP"))
	    		{
	    			result.Add("TcsTransacaoDependente|MostraBotaoPesquisaEsp");
	    			result.Add("TcsTransacaoDependente|TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_PESQUISA_ESP");
	    		}
	
	    		if (bmDisabledTcsTransacaoDependenteList.Contains("TCS_TRANSACAO_DEPENDENTE.POSSUI_TOOLBAR"))
	    		{
	    			result.Add("TcsTransacaoDependente|PossuiToolbar");
	    			result.Add("TcsTransacaoDependente|TCS_TRANSACAO_DEPENDENTE.POSSUI_TOOLBAR");
	    		}
	
	    		if (bmDisabledTcsTransacaoDependenteList.Contains("TCS_TRANSACAO_DEPENDENTE.POSSUI_VISAO_TABULAR"))
	    		{
	    			result.Add("TcsTransacaoDependente|PossuiVisaoTabular");
	    			result.Add("TcsTransacaoDependente|TCS_TRANSACAO_DEPENDENTE.POSSUI_VISAO_TABULAR");
	    		}
	
	    		if (bmDisabledTcsTransacaoDependenteList.Contains("TCS_TRANSACAO_DEPENDENTE.PROPRIEDADES_DO_DETALHE"))
	    		{
	    			result.Add("TcsTransacaoDependente|PropriedadesDoDetalhe");
	    			result.Add("TcsTransacaoDependente|TCS_TRANSACAO_DEPENDENTE.PROPRIEDADES_DO_DETALHE");
	    		}
	
	    		if (bmDisabledTcsTransacaoDependenteList.Contains("TCS_TRANSACAO_DEPENDENTE.PROPRIEDADES_DO_MESTRE"))
	    		{
	    			result.Add("TcsTransacaoDependente|PropriedadesDoMestre");
	    			result.Add("TcsTransacaoDependente|TCS_TRANSACAO_DEPENDENTE.PROPRIEDADES_DO_MESTRE");
	    		}
	
	    		if (bmDisabledTcsTransacaoDependenteList.Contains("TCS_TRANSACAO_DEPENDENTE.USA_FILTROS_DO_BO_PRINCIPAL"))
	    		{
	    			result.Add("TcsTransacaoDependente|UsaFiltrosDoBoPrincipal");
	    			result.Add("TcsTransacaoDependente|TCS_TRANSACAO_DEPENDENTE.USA_FILTROS_DO_BO_PRINCIPAL");
	    		}
	
	    		if (bmDisabledTcsTransacaoDependenteList.Contains("TCS_TRANSACAO_DEPENDENTE.VISIVEL"))
	    		{
	    			result.Add("TcsTransacaoDependente|Visivel");
	    			result.Add("TcsTransacaoDependente|TCS_TRANSACAO_DEPENDENTE.VISIVEL");
	    		}
	    	}
	    	result.Add("TcsModuloMenuP|DescModulo");
	    	result.Add("TcsModuloMenuP|''");
	    	//Add filtering disabled property for TCS_MODULO_MENU
	    	string[] bmDisabledTcsModuloMenuPList = this.GetEDM().GetFilteringDisabledList("TCS_MODULO_MENU");
	    	if (bmDisabledTcsModuloMenuPList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsModuloMenuPList.Contains("TCS_MODULO_MENU.DESC_MODULO_MENU"))
	    		{
	    			result.Add("TcsModuloMenuP|DescModuloMenu");
	    			result.Add("TcsModuloMenuP|TCS_MODULO_MENU.DESC_MODULO_MENU");
	    		}
	
	    		if (bmDisabledTcsModuloMenuPList.Contains("TCS_MODULO_MENU.ID_MODULO_MENU"))
	    		{
	    			result.Add("TcsModuloMenuP|IdModuloMenu");
	    			result.Add("TcsModuloMenuP|TCS_MODULO_MENU.ID_MODULO_MENU");
	    		}
	    	}
	    	return result;
	    }

	    #endregion Excluded Filters

	    #region Get By EntitySearchId Methods Definitions.
	
				
	    [Ignore]
	    //Get TcsTransacao By EntitySearchId.
	    public IQueryable<TcsTransacao> GetTcsTransacaoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsTransacaoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsTransacaoMenuChild By EntitySearchId.
	    public IQueryable<TcsTransacaoMenuChild> GetTcsTransacaoMenuChildByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsTransacaoMenuChildByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsTransacaoDependente By EntitySearchId.
	    public IQueryable<TcsTransacaoDependente> GetTcsTransacaoDependenteByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsTransacaoDependenteByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsTransacao By EntitySearchId.
	    public IQueryable<TcsTransacao> GetTcsTransacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsTransacaoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsTransacaoMenuChild By EntitySearchId.
	    public IQueryable<TcsTransacaoMenuChild> GetTcsTransacaoMenuChildByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsTransacaoMenuChildByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsTransacaoDependente By EntitySearchId.
	    public IQueryable<TcsTransacaoDependente> GetTcsTransacaoDependenteByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsTransacaoDependenteByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsTransacaoMenu By EntitySearchId.
	    public IQueryable<TcsTransacaoMenu> GetTcsTransacaoMenuByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsTransacaoMenuByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsTransacaoMenu By EntitySearchId.
	    public IQueryable<TcsTransacaoMenu> GetTcsTransacaoMenuByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsTransacaoMenuByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsModuloMenuP By EntitySearchId.
	    public IQueryable<TcsModuloMenuP> GetTcsModuloMenuPByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsModuloMenuPByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsModuloMenuP By EntitySearchId.
	    public IQueryable<TcsModuloMenuP> GetTcsModuloMenuPByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsModuloMenuPByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get TcsTransacao By Example.
	    [Ignore]
	    public IQueryable<TcsTransacao> GetTcsTransacaoByExample(TcsTransacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsTransacaoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsTransacaoMenuChild By Example.
	    [Ignore]
	    public IQueryable<TcsTransacaoMenuChild> GetTcsTransacaoMenuChildByExample(TcsTransacaoMenuChild entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsTransacaoMenuChildByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsTransacaoDependente By Example.
	    [Ignore]
	    public IQueryable<TcsTransacaoDependente> GetTcsTransacaoDependenteByExample(TcsTransacaoDependente entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsTransacaoDependenteByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsTransacao By Example.
	    [Ignore]
	    public IQueryable<TcsTransacao> GetTcsTransacaoByExampleNoAssociations(TcsTransacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsTransacaoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsTransacaoMenuChild By Example.
	    [Ignore]
	    public IQueryable<TcsTransacaoMenuChild> GetTcsTransacaoMenuChildByExampleNoAssociations(TcsTransacaoMenuChild entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsTransacaoMenuChildByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsTransacaoDependente By Example.
	    [Ignore]
	    public IQueryable<TcsTransacaoDependente> GetTcsTransacaoDependenteByExampleNoAssociations(TcsTransacaoDependente entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsTransacaoDependenteByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsTransacaoMenu By Example.
	    [Ignore]
	    public IQueryable<TcsTransacaoMenu> GetTcsTransacaoMenuByExample(TcsTransacaoMenu entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsTransacaoMenuByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsTransacaoMenu By Example.
	    [Ignore]
	    public IQueryable<TcsTransacaoMenu> GetTcsTransacaoMenuByExampleNoAssociations(TcsTransacaoMenu entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsTransacaoMenuByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsModuloMenuP By Example.
	    [Ignore]
	    public IQueryable<TcsModuloMenuP> GetTcsModuloMenuPByExample(TcsModuloMenuP entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsModuloMenuPByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsModuloMenuP By Example.
	    [Ignore]
	    public IQueryable<TcsModuloMenuP> GetTcsModuloMenuPByExampleNoAssociations(TcsModuloMenuP entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsModuloMenuPByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public TcsTransacao GetTcsTransacaoByKey(long idTransacao)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsTransacao");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTransacao"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTransacao));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsTransacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsTransacaoMenuChild GetTcsTransacaoMenuChildByKey(Int32 idTcsTransacaoMenu)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsTransacaoMenuChild");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsTransacaoMenu"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsTransacaoMenu));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsTransacaoMenuChildByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsTransacaoMenu GetTcsTransacaoMenuByKey(Int64 idModuloMenu, Int64 idTransacao)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsTransacaoMenu");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdModuloMenu"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idModuloMenu));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTransacao"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTransacao));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsTransacaoMenuByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsTransacaoDependente GetTcsTransacaoDependenteByKey(Int64 idTransacaoDependente)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsTransacaoDependente");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTransacaoDependente"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTransacaoDependente));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsTransacaoDependenteByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsModuloMenuP GetTcsModuloMenuPByKey(Int64 idModuloMenu)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsModuloMenuP");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdModuloMenu"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idModuloMenu));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsModuloMenuPByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get TcsTransacaoByEntitySearch.
	    public IQueryable<TcsTransacao> GetTcsTransacaoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsTransacao> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsTransacao()		
	            {
	            
                ClasseNome = entity0.CLASSE_NOME
                , CodTransacao = entity0.COD_TRANSACAO
                , DescTransacao = entity0.DESC_TRANSACAO
                , Icone = entity0.ICONE
                , IdObjeto = entity0.ID_OBJETO
                , IdTransacao = entity0.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , LxCorFundoName = ((entity0.LX_COR_FUNDO) == 8 ? "Fundo Laranja" : ((entity0.LX_COR_FUNDO) == 10 ? "Fundo Roxo" : ((entity0.LX_COR_FUNDO) == 7 ? "Laranja" : ((entity0.LX_COR_FUNDO) == 9 ? "Roxo" : ""))))
                , LxTipoTransacao = entity0.LX_TIPO_TRANSACAO
                , LxTipoTransacaoName = ((entity0.LX_TIPO_TRANSACAO) == 7 ? "Assistente" : ((entity0.LX_TIPO_TRANSACAO) == 8 ? "Dashboard" : ((entity0.LX_TIPO_TRANSACAO) == 2 ? "ERP" : ((entity0.LX_TIPO_TRANSACAO) == 6 ? "ERP App" : ((entity0.LX_TIPO_TRANSACAO) == 4 ? "Excel" : ((entity0.LX_TIPO_TRANSACAO) == 3 ? "Loja" : ((entity0.LX_TIPO_TRANSACAO) == 5 ? "Mobile" : ((entity0.LX_TIPO_TRANSACAO) == 1 ? "Todos" : ""))))))))
                , NomeCurto = entity0.NOME_CURTO
                , Tag = entity0.TAG
                , DescObjeto = ""
                , ClasseNomeObjeto = ""
			
                ,TcsTransacaoDependenteList = 
	                        (from entity1 in entity0.TCS_TRANSACAO_DEPENDENTE_LISTA
                                  let entity1Al1 = entity1.TCS_TRANSACAO
	                        
	                        	
	                        select new TcsTransacaoDependente()
	                        {
	                        
                                CompartilhaBoPrincipal = entity1.COMPARTILHA_BO_PRINCIPAL
                                , ExecutaPesquisa = entity1.EXECUTA_PESQUISA
                                , IdTransacao = entity1Al1.ID_TRANSACAO
                                , IdTransacaoDependente = entity1.ID_TRANSACAO_DEPENDENTE
                                , IdTransacaoRelacionada = entity1.ID_TRANSACAO_RELACIONADA
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
                                , ClasseNome = ""
                                , DescTransacao = ""
		
	                        }
	                        )
		
	            }
	            );
	
	        SetTcsTransacaoBusinessFilter(ref result, entitySearchList);

			
	
	        TcsTransacao.OnSearching(ref result, false, entitySearchList);	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsTransacaoMenuChildByEntitySearch.
	    public IQueryable<TcsTransacaoMenuChild> GetTcsTransacaoMenuChildByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacaoMenuChild));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoMenuChild> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_MENU.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsTransacaoMenuChild()		
	            {
	            
                IdModuloMenu = entity0.ID_MODULO_MENU
                , IdTcsTransacaoMenu = entity0.ID_TCS_TRANSACAO_MENU
                , IdTransacao = entity0.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
                , SugestaoLinx = entity0.SUGESTAO_LINX
                , DescModuloMenu = ""
                , IdModulo = 0
                , DescModulo = ""
		
	            }
	            );
	
	        SetTcsTransacaoMenuChildBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsTransacaoDependenteByEntitySearch.
	    public IQueryable<TcsTransacaoDependente> GetTcsTransacaoDependenteByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacaoDependente));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoDependente> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_DEPENDENTE.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_TRANSACAO
	            
	            	
	            select new TcsTransacaoDependente()		
	            {
	            
                CompartilhaBoPrincipal = entity0.COMPARTILHA_BO_PRINCIPAL
                , ExecutaPesquisa = entity0.EXECUTA_PESQUISA
                , IdTransacao = entity0Al1.ID_TRANSACAO
                , IdTransacaoDependente = entity0.ID_TRANSACAO_DEPENDENTE
                , IdTransacaoRelacionada = entity0.ID_TRANSACAO_RELACIONADA
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
                , ClasseNome = ""
                , DescTransacao = ""
		
	            }
	            );
	
	        SetTcsTransacaoDependenteBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsTransacaoByEntitySearchNoAssociations.
	    public IQueryable<TcsTransacao> GetTcsTransacaoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsTransacao> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsTransacao()		
	            {
	            
                ClasseNome = entity0.CLASSE_NOME
                , CodTransacao = entity0.COD_TRANSACAO
                , DescTransacao = entity0.DESC_TRANSACAO
                , Icone = entity0.ICONE
                , IdObjeto = entity0.ID_OBJETO
                , IdTransacao = entity0.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , LxCorFundoName = ((entity0.LX_COR_FUNDO) == 8 ? "Fundo Laranja" : ((entity0.LX_COR_FUNDO) == 10 ? "Fundo Roxo" : ((entity0.LX_COR_FUNDO) == 7 ? "Laranja" : ((entity0.LX_COR_FUNDO) == 9 ? "Roxo" : ""))))
                , LxTipoTransacao = entity0.LX_TIPO_TRANSACAO
                , LxTipoTransacaoName = ((entity0.LX_TIPO_TRANSACAO) == 7 ? "Assistente" : ((entity0.LX_TIPO_TRANSACAO) == 8 ? "Dashboard" : ((entity0.LX_TIPO_TRANSACAO) == 2 ? "ERP" : ((entity0.LX_TIPO_TRANSACAO) == 6 ? "ERP App" : ((entity0.LX_TIPO_TRANSACAO) == 4 ? "Excel" : ((entity0.LX_TIPO_TRANSACAO) == 3 ? "Loja" : ((entity0.LX_TIPO_TRANSACAO) == 5 ? "Mobile" : ((entity0.LX_TIPO_TRANSACAO) == 1 ? "Todos" : ""))))))))
                , NomeCurto = entity0.NOME_CURTO
                , Tag = entity0.TAG
                , DescObjeto = ""
                , ClasseNomeObjeto = ""
		
	            }
	            );
	
	        SetTcsTransacaoBusinessFilter(ref result, entitySearchList);

			
	
	        TcsTransacao.OnSearching(ref result, true, entitySearchList);	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsTransacaoMenuChildByEntitySearchNoAssociations.
	    public IQueryable<TcsTransacaoMenuChild> GetTcsTransacaoMenuChildByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacaoMenuChild));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoMenuChild> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_MENU.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsTransacaoMenuChild()		
	            {
	            
                IdModuloMenu = entity0.ID_MODULO_MENU
                , IdTcsTransacaoMenu = entity0.ID_TCS_TRANSACAO_MENU
                , IdTransacao = entity0.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
                , SugestaoLinx = entity0.SUGESTAO_LINX
                , DescModuloMenu = ""
                , IdModulo = 0
                , DescModulo = ""
		
	            }
	            );
	
	        SetTcsTransacaoMenuChildBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsTransacaoDependenteByEntitySearchNoAssociations.
	    public IQueryable<TcsTransacaoDependente> GetTcsTransacaoDependenteByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacaoDependente));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoDependente> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_DEPENDENTE.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_TRANSACAO
	            
	            	
	            select new TcsTransacaoDependente()		
	            {
	            
                CompartilhaBoPrincipal = entity0.COMPARTILHA_BO_PRINCIPAL
                , ExecutaPesquisa = entity0.EXECUTA_PESQUISA
                , IdTransacao = entity0Al1.ID_TRANSACAO
                , IdTransacaoDependente = entity0.ID_TRANSACAO_DEPENDENTE
                , IdTransacaoRelacionada = entity0.ID_TRANSACAO_RELACIONADA
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
                , ClasseNome = ""
                , DescTransacao = ""
		
	            }
	            );
	
	        SetTcsTransacaoDependenteBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
	
	    [Ignore()]
	    private void SetTcsTransacaoBusinessFilter(ref IQueryable<TcsTransacao> query, List<EntitySearch> entitySearchList)
	    {
	    		int idxElement;
	    		string operatorValue;
	    		object value;
	    		//Get query by functions
	    		if (entitySearchList.Count > 0)
	    		{
	    			foreach (EntitySearch search in entitySearchList.Where(e => e.EntityName == "TcsTransacao"))
	    			{

	
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "LxCorFundo" || e.Value.ToString() == "TCS_TRANSACAO.LX_COR_FUNDO")))
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

    
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "ClasseNomeObjeto" || e.Value.ToString() == "''")))
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
	    										string tmpClasseNomeObjeto1 = (string)value;
	    										query = from r in query where r.ClasseNomeObjeto == tmpClasseNomeObjeto1 select r;
	    										break;
	    									case "!=":
	    										string tmpClasseNomeObjeto2 = (string)value;
	    										query = from r in query where r.ClasseNomeObjeto != tmpClasseNomeObjeto2 select r;
	    										break;

	
	    									case "Contains":
	    										string tmpClasseNomeObjeto7 = (string)value;
	    									    query = from r in query where r.ClasseNomeObjeto.Contains(tmpClasseNomeObjeto7) select r;
	    									    break;
	    									case "StartsWith":
	    										string tmpClasseNomeObjeto8 = (string)value;
	    									    query = from r in query where r.ClasseNomeObjeto.StartsWith(tmpClasseNomeObjeto8) select r;
	    									    break;
	    									case "EndsWith":
	    										string tmpClasseNomeObjeto9 = (string)value;
	    									    query = from r in query where r.ClasseNomeObjeto.EndsWith(tmpClasseNomeObjeto9) select r;
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
	    private void SetTcsTransacaoMenuChildBusinessFilter(ref IQueryable<TcsTransacaoMenuChild> query, List<EntitySearch> entitySearchList)
	    {
	    		int idxElement;
	    		string operatorValue;
	    		object value;
	    		//Get query by functions
	    		if (entitySearchList.Count > 0)
	    		{
	    			foreach (EntitySearch search in entitySearchList.Where(e => e.EntityName == "TcsTransacaoMenuChild"))
	    			{

	
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "DescModuloMenu" || e.Value.ToString() == "''")))
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
	    										string tmpDescModuloMenu1 = (string)value;
	    										query = from r in query where r.DescModuloMenu == tmpDescModuloMenu1 select r;
	    										break;
	    									case "!=":
	    										string tmpDescModuloMenu2 = (string)value;
	    										query = from r in query where r.DescModuloMenu != tmpDescModuloMenu2 select r;
	    										break;

	
	    									case "Contains":
	    										string tmpDescModuloMenu7 = (string)value;
	    									    query = from r in query where r.DescModuloMenu.Contains(tmpDescModuloMenu7) select r;
	    									    break;
	    									case "StartsWith":
	    										string tmpDescModuloMenu8 = (string)value;
	    									    query = from r in query where r.DescModuloMenu.StartsWith(tmpDescModuloMenu8) select r;
	    									    break;
	    									case "EndsWith":
	    										string tmpDescModuloMenu9 = (string)value;
	    									    query = from r in query where r.DescModuloMenu.EndsWith(tmpDescModuloMenu9) select r;
	    									    break;

	
	    									default:
	    										break;
	    								}                                
	    							}
	    						}
        					} 

    
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "IdModulo" || e.Value.ToString() == "0")))
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
	    										Int64 tmpIdModulo1 = (Int64)value;
	    										query = from r in query where r.IdModulo == tmpIdModulo1 select r;
	    										break;
	    									case "!=":
	    										Int64 tmpIdModulo2 = (Int64)value;
	    										query = from r in query where r.IdModulo != tmpIdModulo2 select r;
	    										break;

	
	    									case "<":
	    										Int64 tmpIdModulo3 = (Int64)value;
	    										query = from r in query where r.IdModulo < tmpIdModulo3 select r;
	    										break;
	    									case "<=":
	    										Int64 tmpIdModulo4 = (Int64)value;
	    										query = from r in query where r.IdModulo <= tmpIdModulo4 select r;
	    										break;
	    									case ">":
	    										Int64 tmpIdModulo5 = (Int64)value;
	    										query = from r in query where r.IdModulo > tmpIdModulo5 select r;
	    										break;
	    									case ">=":
	    										Int64 tmpIdModulo6 = (Int64)value;
	    										query = from r in query where r.IdModulo >= tmpIdModulo6 select r;
	    										break;	

	
	    									default:
	    										break;
	    								}                                
	    							}
	    						}
        					} 

    
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "DescModulo" || e.Value.ToString() == "''")))
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
	    										string tmpDescModulo1 = (string)value;
	    										query = from r in query where r.DescModulo == tmpDescModulo1 select r;
	    										break;
	    									case "!=":
	    										string tmpDescModulo2 = (string)value;
	    										query = from r in query where r.DescModulo != tmpDescModulo2 select r;
	    										break;

	
	    									case "Contains":
	    										string tmpDescModulo7 = (string)value;
	    									    query = from r in query where r.DescModulo.Contains(tmpDescModulo7) select r;
	    									    break;
	    									case "StartsWith":
	    										string tmpDescModulo8 = (string)value;
	    									    query = from r in query where r.DescModulo.StartsWith(tmpDescModulo8) select r;
	    									    break;
	    									case "EndsWith":
	    										string tmpDescModulo9 = (string)value;
	    									    query = from r in query where r.DescModulo.EndsWith(tmpDescModulo9) select r;
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
	    private void SetTcsTransacaoDependenteBusinessFilter(ref IQueryable<TcsTransacaoDependente> query, List<EntitySearch> entitySearchList)
	    {
	    		int idxElement;
	    		string operatorValue;
	    		object value;
	    		//Get query by functions
	    		if (entitySearchList.Count > 0)
	    		{
	    			foreach (EntitySearch search in entitySearchList.Where(e => e.EntityName == "TcsTransacaoDependente"))
	    			{

	
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "ClasseNome" || e.Value.ToString() == "''")))
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
	    										string tmpClasseNome1 = (string)value;
	    										query = from r in query where r.ClasseNome == tmpClasseNome1 select r;
	    										break;
	    									case "!=":
	    										string tmpClasseNome2 = (string)value;
	    										query = from r in query where r.ClasseNome != tmpClasseNome2 select r;
	    										break;

	
	    									case "Contains":
	    										string tmpClasseNome7 = (string)value;
	    									    query = from r in query where r.ClasseNome.Contains(tmpClasseNome7) select r;
	    									    break;
	    									case "StartsWith":
	    										string tmpClasseNome8 = (string)value;
	    									    query = from r in query where r.ClasseNome.StartsWith(tmpClasseNome8) select r;
	    									    break;
	    									case "EndsWith":
	    										string tmpClasseNome9 = (string)value;
	    									    query = from r in query where r.ClasseNome.EndsWith(tmpClasseNome9) select r;
	    									    break;

	
	    									default:
	    										break;
	    								}                                
	    							}
	    						}
        					} 

    
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "DescTransacao" || e.Value.ToString() == "''")))
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
	    										string tmpDescTransacao1 = (string)value;
	    										query = from r in query where r.DescTransacao == tmpDescTransacao1 select r;
	    										break;
	    									case "!=":
	    										string tmpDescTransacao2 = (string)value;
	    										query = from r in query where r.DescTransacao != tmpDescTransacao2 select r;
	    										break;

	
	    									case "Contains":
	    										string tmpDescTransacao7 = (string)value;
	    									    query = from r in query where r.DescTransacao.Contains(tmpDescTransacao7) select r;
	    									    break;
	    									case "StartsWith":
	    										string tmpDescTransacao8 = (string)value;
	    									    query = from r in query where r.DescTransacao.StartsWith(tmpDescTransacao8) select r;
	    									    break;
	    									case "EndsWith":
	    										string tmpDescTransacao9 = (string)value;
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


		
	
	    
	    [Ignore]
	    //Get TcsTransacaoMenuByEntitySearch.
	    public IQueryable<TcsTransacaoMenu> GetTcsTransacaoMenuByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacaoMenu));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoMenu> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_MENU.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsTransacaoMenu()		
	            {
	            
                IdModuloMenu = entity0.ID_MODULO_MENU
                , IdTransacao = entity0.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
                , SugestaoLinx = entity0.SUGESTAO_LINX
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsTransacaoMenuByEntitySearchNoAssociations.
	    public IQueryable<TcsTransacaoMenu> GetTcsTransacaoMenuByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacaoMenu));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoMenu> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_MENU.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsTransacaoMenu()		
	            {
	            
                IdModuloMenu = entity0.ID_MODULO_MENU
                , IdTransacao = entity0.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
                , SugestaoLinx = entity0.SUGESTAO_LINX
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsModuloMenuPByEntitySearch.
	    public IQueryable<TcsModuloMenuP> GetTcsModuloMenuPByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsModuloMenuP));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		

	        IQueryable<TcsModuloMenuP> result = 
	            (from entity0 in TcsModuloMenuP.OnSearchingReplacement(this.DbContext, dynQuery, parameters, entitySearchList) select entity0);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsModuloMenuPByEntitySearchNoAssociations.
	    public IQueryable<TcsModuloMenuP> GetTcsModuloMenuPByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsModuloMenuP));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		

	        IQueryable<TcsModuloMenuP> result = 
	            (from entity0 in TcsModuloMenuP.OnSearchingReplacement(this.DbContext, dynQuery, parameters, entitySearchList) select entity0);
		
	
	        	

	
	        return result;
	    }
	
	    [Ignore()]
	    private void SetTcsModuloMenuPBusinessFilter(ref IQueryable<TcsModuloMenuP> query, List<EntitySearch> entitySearchList)
	    {
	    		int idxElement;
	    		string operatorValue;
	    		object value;
	    		//Get query by functions
	    		if (entitySearchList.Count > 0)
	    		{
	    			foreach (EntitySearch search in entitySearchList.Where(e => e.EntityName == "TcsModuloMenuP"))
	    			{

	
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "DescModulo" || e.Value.ToString() == "''")))
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
	    										string tmpDescModulo1 = (string)value;
	    										query = from r in query where r.DescModulo == tmpDescModulo1 select r;
	    										break;
	    									case "!=":
	    										string tmpDescModulo2 = (string)value;
	    										query = from r in query where r.DescModulo != tmpDescModulo2 select r;
	    										break;

	
	    									case "Contains":
	    										string tmpDescModulo7 = (string)value;
	    									    query = from r in query where r.DescModulo.Contains(tmpDescModulo7) select r;
	    									    break;
	    									case "StartsWith":
	    										string tmpDescModulo8 = (string)value;
	    									    query = from r in query where r.DescModulo.StartsWith(tmpDescModulo8) select r;
	    									    break;
	    									case "EndsWith":
	    										string tmpDescModulo9 = (string)value;
	    									    query = from r in query where r.DescModulo.EndsWith(tmpDescModulo9) select r;
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
	    //Get PagedTcsTransacao.
	    public IQueryable<TcsTransacao> GetPagedTcsTransacao(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsTransacao> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_TRANSACAO ascending
	            
	            	
	            select new TcsTransacao()		
	            {
	            
                ClasseNome = entity0.CLASSE_NOME
                , CodTransacao = entity0.COD_TRANSACAO
                , DescTransacao = entity0.DESC_TRANSACAO
                , Icone = entity0.ICONE
                , IdObjeto = entity0.ID_OBJETO
                , IdTransacao = entity0.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , LxCorFundoName = ((entity0.LX_COR_FUNDO) == 8 ? "Fundo Laranja" : ((entity0.LX_COR_FUNDO) == 10 ? "Fundo Roxo" : ((entity0.LX_COR_FUNDO) == 7 ? "Laranja" : ((entity0.LX_COR_FUNDO) == 9 ? "Roxo" : ""))))
                , LxTipoTransacao = entity0.LX_TIPO_TRANSACAO
                , LxTipoTransacaoName = ((entity0.LX_TIPO_TRANSACAO) == 7 ? "Assistente" : ((entity0.LX_TIPO_TRANSACAO) == 8 ? "Dashboard" : ((entity0.LX_TIPO_TRANSACAO) == 2 ? "ERP" : ((entity0.LX_TIPO_TRANSACAO) == 6 ? "ERP App" : ((entity0.LX_TIPO_TRANSACAO) == 4 ? "Excel" : ((entity0.LX_TIPO_TRANSACAO) == 3 ? "Loja" : ((entity0.LX_TIPO_TRANSACAO) == 5 ? "Mobile" : ((entity0.LX_TIPO_TRANSACAO) == 1 ? "Todos" : ""))))))))
                , NomeCurto = entity0.NOME_CURTO
                , Tag = entity0.TAG
                , DescObjeto = ""
                , ClasseNomeObjeto = ""
		
	            }
	            ).Skip(skip).Take(take);
	
	        SetTcsTransacaoBusinessFilter(ref result, entitySearchList);

			
	
	        TcsTransacao.OnSearching(ref result, true, entitySearchList);	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsTransacaoMenuChild.
	    public IQueryable<TcsTransacaoMenuChild> GetPagedTcsTransacaoMenuChild(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacaoMenuChild));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoMenuChild> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_MENU.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_TCS_TRANSACAO_MENU ascending
	            
	            	
	            select new TcsTransacaoMenuChild()		
	            {
	            
                IdModuloMenu = entity0.ID_MODULO_MENU
                , IdTcsTransacaoMenu = entity0.ID_TCS_TRANSACAO_MENU
                , IdTransacao = entity0.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
                , SugestaoLinx = entity0.SUGESTAO_LINX
                , DescModuloMenu = ""
                , IdModulo = 0
                , DescModulo = ""
		
	            }
	            ).Skip(skip).Take(take);
	
	        SetTcsTransacaoMenuChildBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsTransacaoDependente.
	    public IQueryable<TcsTransacaoDependente> GetPagedTcsTransacaoDependente(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacaoDependente));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoDependente> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_DEPENDENTE.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_TRANSACAO
                orderby entity0.ID_TRANSACAO_DEPENDENTE ascending
	            
	            	
	            select new TcsTransacaoDependente()		
	            {
	            
                CompartilhaBoPrincipal = entity0.COMPARTILHA_BO_PRINCIPAL
                , ExecutaPesquisa = entity0.EXECUTA_PESQUISA
                , IdTransacao = entity0Al1.ID_TRANSACAO
                , IdTransacaoDependente = entity0.ID_TRANSACAO_DEPENDENTE
                , IdTransacaoRelacionada = entity0.ID_TRANSACAO_RELACIONADA
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
                , ClasseNome = ""
                , DescTransacao = ""
		
	            }
	            ).Skip(skip).Take(take);
	
	        SetTcsTransacaoDependenteBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsTransacaoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_TRANSACAO.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsTransacaoMenuChildCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacaoMenuChild));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_TRANSACAO_MENU.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsTransacaoDependenteCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacaoDependente));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_TRANSACAO_DEPENDENTE.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_TRANSACAO
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsTransacaoMenu.
	    public IQueryable<TcsTransacaoMenu> GetPagedTcsTransacaoMenu(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacaoMenu));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoMenu> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_MENU.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_MODULO_MENU ascending, entity0.ID_TRANSACAO ascending
	            
	            	
	            select new TcsTransacaoMenu()		
	            {
	            
                IdModuloMenu = entity0.ID_MODULO_MENU
                , IdTransacao = entity0.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
                , SugestaoLinx = entity0.SUGESTAO_LINX
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsTransacaoMenuCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacaoMenu));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_TRANSACAO_MENU.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsModuloMenuP.
	    public IQueryable<TcsModuloMenuP> GetPagedTcsModuloMenuP(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsModuloMenuP));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		

	        IQueryable<TcsModuloMenuP> result = 
	            (from entity0 in TcsModuloMenuP.OnSearchingReplacement(this.DbContext, dynQuery, parameters, entitySearchList) select entity0);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsModuloMenuPCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsModuloMenuP));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_MODULO_MENU.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_MODULO
	            
	            select 1
	            ).Count();	
		
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    
	    [Update()]	
	    //Update TcsTransacao.
	    public void UpdateTcsTransacao(TcsTransacao entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsTransacao.
	    public void InsertTcsTransacao(TcsTransacao entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsTransacao.
	    public void DeleteTcsTransacao(TcsTransacao entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsTransacaoMenuChild.
	    public void UpdateTcsTransacaoMenuChild(TcsTransacaoMenuChild entity)
	    {



	
	        if (entity.TcsTransacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsTransacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsTransacao); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsTransacaoMenuChild.
	    public void InsertTcsTransacaoMenuChild(TcsTransacaoMenuChild entity)
	    {



	
	        if (entity.TcsTransacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsTransacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsTransacao);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsTransacaoMenuChild.
	    public void DeleteTcsTransacaoMenuChild(TcsTransacaoMenuChild entity)
	    {



	
	        if (entity.TcsTransacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsTransacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsTransacao);
	            

	
	        }

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsTransacaoDependente.
	    public void UpdateTcsTransacaoDependente(TcsTransacaoDependente entity)
	    {



	
	        if (entity.TcsTransacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsTransacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsTransacao); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsTransacaoDependente.
	    public void InsertTcsTransacaoDependente(TcsTransacaoDependente entity)
	    {



	
	        if (entity.TcsTransacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsTransacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsTransacao);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsTransacaoDependente.
	    public void DeleteTcsTransacaoDependente(TcsTransacaoDependente entity)
	    {



	
	        if (entity.TcsTransacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsTransacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsTransacao);
	            

	
	        }

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsTransacaoMenu.
	    public void UpdateTcsTransacaoMenu(TcsTransacaoMenu entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsTransacaoMenu.
	    public void InsertTcsTransacaoMenu(TcsTransacaoMenu entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsTransacaoMenu.
	    public void DeleteTcsTransacaoMenu(TcsTransacaoMenu entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsModuloMenuP.
	    public void UpdateTcsModuloMenuP(TcsModuloMenuP entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsModuloMenuP.
	    public void InsertTcsModuloMenuP(TcsModuloMenuP entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsModuloMenuP.
	    public void DeleteTcsModuloMenuP(TcsModuloMenuP entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}