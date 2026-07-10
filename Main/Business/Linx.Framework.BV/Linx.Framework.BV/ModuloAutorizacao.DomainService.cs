					
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

namespace Linx.Framework.BV.ModuloAutorizacao
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_MODULO_AUTORIZACAO.ID_MODULO", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsModuloAutorizacao,TcsModuloAutorizacao.TcsModuloMenuAutorizacao,TcsModuloMenuAutorizacao.TcsTransacaoMenuAutorizacaoModulo];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdModulo];ReadOnly[false];Entities[TCS_MODULO_AUTORIZACAO:IdModulo|TCS_APLICATIVO:IdTcsAplicativo];SubQueryInfo[];EdmEntityName[TCS_MODULO_AUTORIZACAO];EntityRelations[TCS_APLICATIVO(TCS_APLICATIVO)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsModuloAutorizacao")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.ModuloAutorizacao.TcsModuloAutorizacao")]
	public partial class TcsModuloAutorizacao : Linx.Data.Entity
	{

	

	    public TcsModuloAutorizacao() : this(true) { }

	    public TcsModuloAutorizacao(bool setDefaults) 
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
	      if (this.TcsModuloMenuAutorizacaoList != null && this.TcsModuloMenuAutorizacaoList.Count() > 0)
	      {
	         foreach (var entity in this.TcsModuloMenuAutorizacaoList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.TcsModuloMenuAutorizacaoList != null)
	      {
	         foreach (var detail in this.TcsModuloMenuAutorizacaoList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsModuloMenuAutorizacaoList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(ModuloAutorizacaoDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("TcsModuloMenuAutorizacao"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsModuloMenuAutorizacao");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdModulo"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdModulo));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsModuloMenuAutorizacao and all sub-details
	         if (this.TcsModuloMenuAutorizacaoList == null || this.TcsModuloMenuAutorizacaoList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsModuloMenuAutorizacaoList = context.GetPagedTcsModuloMenuAutorizacao(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsModuloMenuAutorizacaoList = (from r in context.GetTcsModuloMenuAutorizacaoByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	         foreach(TcsModuloMenuAutorizacao detail in this.TcsModuloMenuAutorizacaoList)
	         {
	             detail.FillDetails(context, serializedEntitySearch, jEntitySearch, viewNames, take);
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _TcsModuloMenuAutorizacaoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsModuloMenuAutorizacao && ((TcsModuloMenuAutorizacao)e.Entity).TcsModuloAutorizacao == null && e.Associations == null && e.OriginalAssociations == null && ((TcsModuloMenuAutorizacao)e.Entity).IdModulo == this.IdModulo).ToList();
 	      if (_TcsModuloMenuAutorizacaoElements.Count > 0 && this.TcsModuloMenuAutorizacaoList.Count() == 0)
 	      {
 	          this.TcsModuloMenuAutorizacaoList = _TcsModuloMenuAutorizacaoElements.Select(e => (TcsModuloMenuAutorizacao)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsModuloMenuAutorizacaoElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsModuloMenuAutorizacao)detail.Entity).TcsModuloAutorizacao = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsModuloAutorizacao", new int[] { masterIndex });
 	              ((TcsModuloMenuAutorizacao)detail.Entity).AdjustHierarchyForSaving(detail, changeSet);
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsModuloMenuAutorizacaoList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For DescModulo
	    partial void OnDescModuloChanging(System.String value);
	    partial void OnDescModuloChanged();

	    private System.String _DescModulo;

	    [DataMember(IsRequired = true, Name = "DescModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição Detalhada", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO_AUTORIZACAO.DESC_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_AUTORIZACAO.DESC_MODULO")]
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
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicativo];LookUpTitle[Seleção de (Aplicativo)];LookUpQuery[executeLookUpTcsAplicativo];LookUpFinalize[finalizeLookUpTcsAplicativo];LookUpDisplayColumns[{\"DescricaoAplicativo\" : \"Aplicativo\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\"}];LookUpColumns[{\"DescricaoAplicativo\" : true, \"IdTcsAplicativo\" : false}];FilterDataKey[TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoAplicativo#false##250:0##Aplicativo#0#true##::LookUpTcsAplicativo##false#false#TCS_APLICATIVO#TCS_APLICATIVO#Linx.Framework.BV.ModuloAutorizacao#IQueryable###true#false", EdmKey="TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO")]
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
	    //Extensibility Partial Method Definitions For Icone
	    partial void OnIconeChanging(System.String value);
	    partial void OnIconeChanged();

	    private System.String _Icone;

	    [DataMember(Name = "Icone", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ícone", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO_AUTORIZACAO.ICONE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_AUTORIZACAO.ICONE")]
	    public System.String Icone
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
	    partial void OnIdModuloChanging(Int64 value);
	    partial void OnIdModuloChanged();

	    private Int64 _IdModulo;

	    [DataMember(IsRequired = true, Name = "IdModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO_AUTORIZACAO.ID_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_AUTORIZACAO.ID_MODULO")]
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
	    [Display(Name = "Id Tcs Aplicativo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicativo];LookUpTitle[Seleção de (Id Tcs Aplicativo)];LookUpQuery[executeLookUpTcsAplicativo];LookUpFinalize[finalizeLookUpTcsAplicativo];LookUpDisplayColumns[{\"DescricaoAplicativo\" : \"Aplicativo\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\"}];LookUpColumns[{\"DescricaoAplicativo\" : true, \"IdTcsAplicativo\" : false}];FilterDataKey[TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdTcsAplicativo#true##12:0##Id Tcs Aplicativo#1#false##::LookUpTcsAplicativo##false#false#TCS_APLICATIVO#TCS_APLICATIVO#Linx.Framework.BV.ModuloAutorizacao#IQueryable###true#false", EdmKey="TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO")]
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
	    //Extensibility Partial Method Definitions For Inativo
	    partial void OnInativoChanging(Boolean value);
	    partial void OnInativoChanged();

	    private Boolean _Inativo;

	    [DataMember(IsRequired = true, Name = "Inativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inativo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO_AUTORIZACAO.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_AUTORIZACAO.INATIVO")]
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
	    //Extensibility Partial Method Definitions For LxCorFundo
	    partial void OnLxCorFundoChanging(System.Nullable<System.Int32> value);
	    partial void OnLxCorFundoChanged();

	    private System.Nullable<System.Int32> _LxCorFundo;

	    [DataMember(Name = "LxCorFundo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cor de Fundo", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[CorFundo];KpiName[];KpiRelatedAttribute[];DefaultValue[7];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_MODULO_AUTORIZACAO.LX_COR_FUNDO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_AUTORIZACAO.LX_COR_FUNDO")]
	    public System.Nullable<System.Int32> LxCorFundo
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
	    //Extensibility Partial Method Definitions For NomeCurto
	    partial void OnNomeCurtoChanging(System.String value);
	    partial void OnNomeCurtoChanged();

	    private System.String _NomeCurto;

	    [DataMember(IsRequired = true, Name = "NomeCurto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO_AUTORIZACAO.NOME_CURTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_AUTORIZACAO.NOME_CURTO")]
	    public System.String NomeCurto
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
	    //Extensibility Partial Method Definitions For OrdemNavegacao
	    partial void OnOrdemNavegacaoChanging(Byte value);
	    partial void OnOrdemNavegacaoChanged();

	    private Byte _OrdemNavegacao;

	    [DataMember(IsRequired = true, Name = "OrdemNavegacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ordem", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO_AUTORIZACAO.ORDEM_NAVEGACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_AUTORIZACAO.ORDEM_NAVEGACAO")]
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

	    private Int64 _TemporaryIdModulo;
	    [DataMember(Name = "TemporaryIdModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo (Tmp)", Description="Temporary Key", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdModulo
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdModulo.IsNullOrEmpty())
	    	                this._TemporaryIdModulo = this._IdModulo;
	    	          return this._TemporaryIdModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdModulo != value)
	    	              this._TemporaryIdModulo = value;
	    	    }
	    }	

	    #endregion Data Properties

	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<TcsModuloMenuAutorizacao> _TcsModuloMenuAutorizacaoList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsModuloAutorizacao_TcsModuloMenuAutorizacao", "IdModulo", "IdModulo", IsForeignKey=false)]
	    [DataMember(Name = "TcsModuloMenuAutorizacaoList", EmitDefaultValue = true)]
	    public IEnumerable<TcsModuloMenuAutorizacao> TcsModuloMenuAutorizacaoList
	    {
	        get
	        {
	
	            if (this._TcsModuloMenuAutorizacaoList == null)
	            	this._TcsModuloMenuAutorizacaoList = new List<TcsModuloMenuAutorizacao>();
	
	            return this._TcsModuloMenuAutorizacaoList;
	        }
	        set
	        {
	            if (this._TcsModuloMenuAutorizacaoList != value)
	            {
	                this._TcsModuloMenuAutorizacaoList = value;
	                this.RaisePropertyChanged("TcsModuloMenuAutorizacaoList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_MODULO_AUTORIZACAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_MODULO_AUTORIZACAO), QualifiedEntitySetName = "AutorizacaoContext.TCS_MODULO_AUTORIZACAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_AUTORIZACAO.ICONE", Source = "Icone", Target = "ICONE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MODULO_AUTORIZACAO", RelationPropertyName = "TCS_MODULO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_AUTORIZACAO.INATIVO", Source = "Inativo", Target = "INATIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MODULO_AUTORIZACAO", RelationPropertyName = "TCS_MODULO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_AUTORIZACAO.ID_MODULO", Source = "IdModulo", Target = "ID_MODULO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MODULO_AUTORIZACAO", RelationPropertyName = "TCS_MODULO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_AUTORIZACAO.NOME_CURTO", Source = "NomeCurto", Target = "NOME_CURTO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MODULO_AUTORIZACAO", RelationPropertyName = "TCS_MODULO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_AUTORIZACAO.DESC_MODULO", Source = "DescModulo", Target = "DESC_MODULO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MODULO_AUTORIZACAO", RelationPropertyName = "TCS_MODULO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_AUTORIZACAO.LX_COR_FUNDO", Source = "LxCorFundo", Target = "LX_COR_FUNDO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MODULO_AUTORIZACAO", RelationPropertyName = "TCS_MODULO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_AUTORIZACAO.ORDEM_NAVEGACAO", Source = "OrdemNavegacao", Target = "ORDEM_NAVEGACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MODULO_AUTORIZACAO", RelationPropertyName = "TCS_MODULO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO", Source = "IdTcsAplicativo", Target = "ID_TCS_APLICATIVO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICATIVO", RelationPropertyName = "TCS_APLICATIVO" });

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
	             Linx.Framework.BV.BusinessMediaHelper.SyncMedia("TCS_MODULO_AUTORIZACAO", this.IdModulo, null, this.TableMedia.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(e => new Guid(e)).ToList());
	         }
	         else if (operation == DomainOperation.Delete) {
	             Linx.Framework.BV.BusinessMediaHelper.SyncMedia("TCS_MODULO_AUTORIZACAO", this.IdModulo, null, new List<Guid>() { Guid.Empty });
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

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_MODULO_MENU_AUTORIZACAO.ID_MODULO_MENU", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Menu];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdModuloMenu];ReadOnly[false];Entities[TCS_MODULO_MENU_AUTORIZACAO:IdModuloMenu];SubQueryInfo[Select 1 From #ParentAlias#.TCS_MODULO_MENU_AUTORIZACAO_LISTA as #Alias#];EdmEntityName[TCS_MODULO_MENU_AUTORIZACAO];EntityRelations[TCS_MODULO_AUTORIZACAO(TCS_MODULO_AUTORIZACAO)#TCS_APLICATIVO(TCS_APLICATIVO)#MODULO_MENU_SUPERIOR(TCS_MODULO_MENU_AUTORIZACAO)];EdmParentEntityName[TCS_MODULO_AUTORIZACAO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsModuloMenuAutorizacao")]
	[Serializable()]
	public partial class TcsModuloMenuAutorizacao : Linx.Data.Entity
	{

	

	    public TcsModuloMenuAutorizacao() : this(true) { }

	    public TcsModuloMenuAutorizacao(bool setDefaults) 
	    {

	        if (setDefaults)
	        {
	        	        LxCorFundo = 7;
	        }	

	    }

			
	

	
	    #region Load Data Parent
		

	    public void LoadParent(ModuloAutorizacaoDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsModuloAutorizacao");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdModulo"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdModulo));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsModuloAutorizacao
	         this.TcsModuloAutorizacao = (from r in context.GetTcsModuloAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Load Data Parent

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.TcsTransacaoMenuAutorizacaoModuloList != null && this.TcsTransacaoMenuAutorizacaoModuloList.Count() > 0)
	      {
	         foreach (var entity in this.TcsTransacaoMenuAutorizacaoModuloList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.TcsTransacaoMenuAutorizacaoModuloList != null)
	      {
	         foreach (var detail in this.TcsTransacaoMenuAutorizacaoModuloList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsTransacaoMenuAutorizacaoModuloList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(ModuloAutorizacaoDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("TcsTransacaoMenuAutorizacaoModulo"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsTransacaoMenuAutorizacaoModulo");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdModuloMenu"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdModuloMenu));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsTransacaoMenuAutorizacaoModulo and all sub-details
	         if (this.TcsTransacaoMenuAutorizacaoModuloList == null || this.TcsTransacaoMenuAutorizacaoModuloList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsTransacaoMenuAutorizacaoModuloList = context.GetPagedTcsTransacaoMenuAutorizacaoModulo(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsTransacaoMenuAutorizacaoModuloList = (from r in context.GetTcsTransacaoMenuAutorizacaoModuloByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _TcsTransacaoMenuAutorizacaoModuloElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsTransacaoMenuAutorizacaoModulo && ((TcsTransacaoMenuAutorizacaoModulo)e.Entity).TcsModuloMenuAutorizacao == null && e.Associations == null && e.OriginalAssociations == null && ((TcsTransacaoMenuAutorizacaoModulo)e.Entity).IdModuloMenu == this.IdModuloMenu).ToList();
 	      if (_TcsTransacaoMenuAutorizacaoModuloElements.Count > 0 && this.TcsTransacaoMenuAutorizacaoModuloList.Count() == 0)
 	      {
 	          this.TcsTransacaoMenuAutorizacaoModuloList = _TcsTransacaoMenuAutorizacaoModuloElements.Select(e => (TcsTransacaoMenuAutorizacaoModulo)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsTransacaoMenuAutorizacaoModuloElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsTransacaoMenuAutorizacaoModulo)detail.Entity).TcsModuloMenuAutorizacao = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsModuloMenuAutorizacao", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsTransacaoMenuAutorizacaoModuloList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For DescModulo
	    partial void OnDescModuloChanging(System.String value);
	    partial void OnDescModuloChanged();

	    private System.String _DescModulo;

	    [DataMember(Name = "DescModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição Detalhada", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.DESC_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.DESC_MODULO")]
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
	    //Extensibility Partial Method Definitions For DescModuloMenu
	    partial void OnDescModuloMenuChanging(System.String value);
	    partial void OnDescModuloMenuChanged();

	    private System.String _DescModuloMenu;

	    [DataMember(Name = "DescModuloMenu", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição Detalhada", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_MODULO_MENU_AUTORIZACAO.DESC_MODULO_MENU];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_MENU_AUTORIZACAO.DESC_MODULO_MENU")]
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
	    //Extensibility Partial Method Definitions For DescModuloMenuSuperior
	    partial void OnDescModuloMenuSuperiorChanging(System.String value);
	    partial void OnDescModuloMenuSuperiorChanged();

	    private System.String _DescModuloMenuSuperior;

	    [DataMember(Name = "DescModuloMenuSuperior", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Menu Superior", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpModuloMenuSuperior];LookUpTitle[Seleção de (Menu Superior)];LookUpQuery[executeLookUpModuloMenuSuperior];LookUpFinalize[finalizeLookUpModuloMenuSuperior];LookUpDisplayColumns[{\"DescModuloMenuSuperior\" : \"Menu Superior\", \"IdModuloMenuSuperior\" : \"Id Modulo Menu Superior\"}];LookUpColumns[{\"DescModuloMenuSuperior\" : true, \"IdModuloMenuSuperior\" : false}];FilterDataKey[TCS_MODULO_MENU_AUTORIZACAO.MODULO_MENU_SUPERIOR.DESC_MODULO_MENU];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescModuloMenuSuperior#false##100:0##Menu Superior#2#true##::LookUpModuloMenuSuperior##false#false#MODULO_MENU_SUPERIOR#TCS_MODULO_MENU_AUTORIZACAO#Linx.Framework.BV.ModuloAutorizacao#IQueryable#DescModulo,IdModulo[DescModulo,IdModulo]##true#false", EdmKey="TCS_MODULO_MENU_AUTORIZACAO.MODULO_MENU_SUPERIOR.DESC_MODULO_MENU")]
	    public System.String DescModuloMenuSuperior
	    {
	    	    get
	    	    {
	    	          return _DescModuloMenuSuperior;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescModuloMenuSuperior != value)
	    	          {
	    	              this.ValidateProperty("DescModuloMenuSuperior", value);
	    	              this.OnDescModuloMenuSuperiorChanging(value);
	    	              this.RaiseDataMemberChanging("DescModuloMenuSuperior");
	    	              this._DescModuloMenuSuperior = value;
	    	              this.RaiseDataMemberChanged("DescModuloMenuSuperior");
	    	              this.OnDescModuloMenuSuperiorChanged();
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
	    [Display(Name = "Aplicativo", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO")]
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
	    //Extensibility Partial Method Definitions For Icone
	    partial void OnIconeChanging(System.String value);
	    partial void OnIconeChanged();

	    private System.String _Icone;

	    [DataMember(Name = "Icone", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ícone", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO_MENU_AUTORIZACAO.ICONE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_MENU_AUTORIZACAO.ICONE")]
	    public System.String Icone
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
	    partial void OnIdModuloChanging(Int64 value);
	    partial void OnIdModuloChanged();

	    private Int64 _IdModulo;

	    [DataMember(IsRequired = true, Name = "IdModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.ID_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.ID_MODULO")]
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
	    [FunctionalPoint("Precision[24:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO_MENU_AUTORIZACAO.ID_MODULO_MENU];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_MENU_AUTORIZACAO.ID_MODULO_MENU")]
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
	    //Extensibility Partial Method Definitions For IdModuloMenuSuperior
	    partial void OnIdModuloMenuSuperiorChanging(System.Nullable<Int64> value);
	    partial void OnIdModuloMenuSuperiorChanged();

	    private System.Nullable<Int64> _IdModuloMenuSuperior;

	    [DataMember(Name = "IdModuloMenuSuperior", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo Menu Superior", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpModuloMenuSuperior];LookUpTitle[Seleção de (Id Modulo Menu Superior)];LookUpQuery[executeLookUpModuloMenuSuperior];LookUpFinalize[finalizeLookUpModuloMenuSuperior];LookUpDisplayColumns[{\"DescModuloMenuSuperior\" : \"Menu Superior\", \"IdModuloMenuSuperior\" : \"Id Modulo Menu Superior\"}];LookUpColumns[{\"DescModuloMenuSuperior\" : true, \"IdModuloMenuSuperior\" : false}];FilterDataKey[TCS_MODULO_MENU_AUTORIZACAO.MODULO_MENU_SUPERIOR.ID_MODULO_MENU];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<Int64>#IdModuloMenuSuperior#true##24:0##Id Modulo Menu Superior#4#false##::LookUpModuloMenuSuperior##false#false#MODULO_MENU_SUPERIOR#TCS_MODULO_MENU_AUTORIZACAO#Linx.Framework.BV.ModuloAutorizacao#IQueryable#DescModulo,IdModulo[DescModulo,IdModulo]##true#false", EdmKey="TCS_MODULO_MENU_AUTORIZACAO.MODULO_MENU_SUPERIOR.ID_MODULO_MENU")]
	    public System.Nullable<Int64> IdModuloMenuSuperior
	    {
	    	    get
	    	    {
	    	          return _IdModuloMenuSuperior;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdModuloMenuSuperior != value)
	    	          {
	    	              this.ValidateProperty("IdModuloMenuSuperior", value);
	    	              this.OnIdModuloMenuSuperiorChanging(value);
	    	              this.RaiseDataMemberChanging("IdModuloMenuSuperior");
	    	              this._IdModuloMenuSuperior = value;
	    	              this.RaiseDataMemberChanged("IdModuloMenuSuperior");
	    	              this.OnIdModuloMenuSuperiorChanged();
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
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO")]
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
	    //Extensibility Partial Method Definitions For InativoModulo
	    partial void OnInativoModuloChanging(Boolean value);
	    partial void OnInativoModuloChanged();

	    private Boolean _InativoModulo;

	    [DataMember(IsRequired = true, Name = "InativoModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inativo", Description="", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.INATIVO")]
	    public Boolean InativoModulo
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
	    //Extensibility Partial Method Definitions For LxCorFundo
	    partial void OnLxCorFundoChanging(System.Nullable<System.Int32> value);
	    partial void OnLxCorFundoChanged();

	    private System.Nullable<System.Int32> _LxCorFundo;

	    [DataMember(Name = "LxCorFundo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cor de Fundo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[CorFundo];KpiName[];KpiRelatedAttribute[];DefaultValue[7];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_MODULO_MENU_AUTORIZACAO.LX_COR_FUNDO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_MENU_AUTORIZACAO.LX_COR_FUNDO")]
	    public System.Nullable<System.Int32> LxCorFundo
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
	    //Extensibility Partial Method Definitions For NomeCurto
	    partial void OnNomeCurtoChanging(System.String value);
	    partial void OnNomeCurtoChanged();

	    private System.String _NomeCurto;

	    [DataMember(IsRequired = true, Name = "NomeCurto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO_MENU_AUTORIZACAO.NOME_CURTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_MENU_AUTORIZACAO.NOME_CURTO")]
	    public System.String NomeCurto
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
	    //Extensibility Partial Method Definitions For OrdemNavegacao
	    partial void OnOrdemNavegacaoChanging(Byte value);
	    partial void OnOrdemNavegacaoChanged();

	    private Byte _OrdemNavegacao;

	    [DataMember(IsRequired = true, Name = "OrdemNavegacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ordem", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO_MENU_AUTORIZACAO.ORDEM_NAVEGACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_MENU_AUTORIZACAO.ORDEM_NAVEGACAO")]
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

		

	    #region Parent Association
	 
	    private TcsModuloAutorizacao _TcsModuloAutorizacao;
	    [DataMember(Name = "TcsModuloAutorizacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsModuloAutorizacao_TcsModuloMenuAutorizacao", "IdModulo", "IdModulo", IsForeignKey=true)]
	    public TcsModuloAutorizacao TcsModuloAutorizacao
	    {
	        get
	        {
	            return this._TcsModuloAutorizacao;
	        }
	        set
	        {
	            if (this._TcsModuloAutorizacao != value)
	            {
	                this._TcsModuloAutorizacao = value;
	                this.RaisePropertyChanged("TcsModuloAutorizacaoList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<TcsTransacaoMenuAutorizacaoModulo> _TcsTransacaoMenuAutorizacaoModuloList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsModuloMenuAutorizacao_TcsTransacaoMenuAutorizacaoModulo", "IdModuloMenu", "IdModuloMenu", IsForeignKey=false)]
	    [DataMember(Name = "TcsTransacaoMenuAutorizacaoModuloList", EmitDefaultValue = true)]
	    public IEnumerable<TcsTransacaoMenuAutorizacaoModulo> TcsTransacaoMenuAutorizacaoModuloList
	    {
	        get
	        {
	
	            if (this._TcsTransacaoMenuAutorizacaoModuloList == null)
	            	this._TcsTransacaoMenuAutorizacaoModuloList = new List<TcsTransacaoMenuAutorizacaoModulo>();
	
	            return this._TcsTransacaoMenuAutorizacaoModuloList;
	        }
	        set
	        {
	            if (this._TcsTransacaoMenuAutorizacaoModuloList != value)
	            {
	                this._TcsTransacaoMenuAutorizacaoModuloList = value;
	                this.RaisePropertyChanged("TcsTransacaoMenuAutorizacaoModuloList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_MODULO_MENU_AUTORIZACAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_MODULO_MENU_AUTORIZACAO), QualifiedEntitySetName = "AutorizacaoContext.TCS_MODULO_MENU_AUTORIZACAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_MENU_AUTORIZACAO.ICONE", Source = "Icone", Target = "ICONE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MODULO_MENU_AUTORIZACAO", RelationPropertyName = "TCS_MODULO_MENU_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_MENU_AUTORIZACAO.NOME_CURTO", Source = "NomeCurto", Target = "NOME_CURTO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MODULO_MENU_AUTORIZACAO", RelationPropertyName = "TCS_MODULO_MENU_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_MENU_AUTORIZACAO.LX_COR_FUNDO", Source = "LxCorFundo", Target = "LX_COR_FUNDO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MODULO_MENU_AUTORIZACAO", RelationPropertyName = "TCS_MODULO_MENU_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_MENU_AUTORIZACAO.ID_MODULO_MENU", Source = "IdModuloMenu", Target = "ID_MODULO_MENU", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MODULO_MENU_AUTORIZACAO", RelationPropertyName = "TCS_MODULO_MENU_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_MENU_AUTORIZACAO.ORDEM_NAVEGACAO", Source = "OrdemNavegacao", Target = "ORDEM_NAVEGACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MODULO_MENU_AUTORIZACAO", RelationPropertyName = "TCS_MODULO_MENU_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_MENU_AUTORIZACAO.DESC_MODULO_MENU", Source = "DescModuloMenu", Target = "DESC_MODULO_MENU", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MODULO_MENU_AUTORIZACAO", RelationPropertyName = "TCS_MODULO_MENU_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.ID_MODULO", Source = "IdModulo", Target = "ID_MODULO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_MODULO_AUTORIZACAO", RelationPropertyName = "TCS_MODULO_AUTORIZACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_MENU_AUTORIZACAO.MODULO_MENU_SUPERIOR.ID_MODULO_MENU", Source = "IdModuloMenuSuperior", Target = "ID_MODULO_MENU", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_MODULO_MENU_AUTORIZACAO", RelationPropertyName = "MODULO_MENU_SUPERIOR" });

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
	             Linx.Framework.BV.BusinessMediaHelper.SyncMedia("TCS_MODULO_MENU_AUTORIZACAO", this.IdModuloMenu, null, this.TableMedia.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(e => new Guid(e)).ToList());
	         }
	         else if (operation == DomainOperation.Delete) {
	             Linx.Framework.BV.BusinessMediaHelper.SyncMedia("TCS_MODULO_MENU_AUTORIZACAO", this.IdModuloMenu, null, new List<Guid>() { Guid.Empty });
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

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_TRANSACAO_MENU_AUTORIZACAO.ID_TCS_TRANSACAO_MENU_AUTORIZACAO", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Transação];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsTransacaoMenuAutorizacao];ReadOnly[false];Entities[TCS_TRANSACAO_MENU_AUTORIZACAO:IdTcsTransacaoMenuAutorizacao];SubQueryInfo[Select 1 From #ParentAlias#.TCS_TRANSACAO_MENU_AUTORIZACAO_LISTA as #Alias#];EdmEntityName[TCS_TRANSACAO_MENU_AUTORIZACAO];EntityRelations[TCS_MODULO_MENU_AUTORIZACAO(TCS_MODULO_MENU_AUTORIZACAO)#TCS_MODULO_AUTORIZACAO(TCS_MODULO_AUTORIZACAO)#TCS_APLICATIVO(TCS_APLICATIVO)#MODULO_MENU_SUPERIOR(TCS_MODULO_MENU_AUTORIZACAO)#TCS_TRANSACAO_AUTORIZACAO(TCS_TRANSACAO_AUTORIZACAO)#TCS_OBJETO_AUTORIZACAO(TCS_OBJETO_AUTORIZACAO)];EdmParentEntityName[TCS_MODULO_MENU_AUTORIZACAO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsTransacaoMenuAutorizacaoModulo")]
	[Serializable()]
	public partial class TcsTransacaoMenuAutorizacaoModulo : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(ModuloAutorizacaoDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsModuloMenuAutorizacao");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdModuloMenu"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdModuloMenu));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsModuloMenuAutorizacao
	         this.TcsModuloMenuAutorizacao = (from r in context.GetTcsModuloMenuAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For DescTransacao
	    partial void OnDescTransacaoChanging(System.String value);
	    partial void OnDescTransacaoChanged();

	    private System.String _DescTransacao;

	    [DataMember(IsRequired = true, Name = "DescTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Transação", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsTransacaoAutorizacao];LookUpTitle[Seleção de (Transação)];LookUpQuery[executeLookUpTcsTransacaoAutorizacao];LookUpFinalize[finalizeLookUpTcsTransacaoAutorizacao];LookUpDisplayColumns[{\"DescTransacao\" : \"Transação\", \"IdTransacao\" : \"Id Transacao\"}];LookUpColumns[{\"DescTransacao\" : true, \"IdTransacao\" : false}];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.DESC_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescTransacao#false##60:0##Transação#0#true##::LookUpTcsTransacaoAutorizacao##false#false#TCS_TRANSACAO_AUTORIZACAO#TCS_TRANSACAO_AUTORIZACAO#Linx.Framework.BV.ModuloAutorizacao#IQueryable###true#false", EdmKey="TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.DESC_TRANSACAO")]
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
	    //Extensibility Partial Method Definitions For IdModuloMenu
	    partial void OnIdModuloMenuChanging(Int64 value);
	    partial void OnIdModuloMenuChanged();

	    private Int64 _IdModuloMenu;

	    [DataMember(IsRequired = true, Name = "IdModuloMenu", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo Menu", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_MODULO_MENU_AUTORIZACAO.ID_MODULO_MENU];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_MODULO_MENU_AUTORIZACAO.ID_MODULO_MENU")]
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
	    //Extensibility Partial Method Definitions For IdTcsTransacaoMenuAutorizacao
	    partial void OnIdTcsTransacaoMenuAutorizacaoChanging(Int32 value);
	    partial void OnIdTcsTransacaoMenuAutorizacaoChanged();

	    private Int32 _IdTcsTransacaoMenuAutorizacao;

	    [DataMember(IsRequired = true, Name = "IdTcsTransacaoMenuAutorizacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Transacao Menu Autorizacao", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.ID_TCS_TRANSACAO_MENU_AUTORIZACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_MENU_AUTORIZACAO.ID_TCS_TRANSACAO_MENU_AUTORIZACAO")]
	    public Int32 IdTcsTransacaoMenuAutorizacao
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
	    partial void OnIdTransacaoChanging(Int64 value);
	    partial void OnIdTransacaoChanged();

	    private Int64 _IdTransacao;

	    [DataMember(IsRequired = true, Name = "IdTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Transacao", Description="", Order = 5, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsTransacaoAutorizacao];LookUpTitle[Seleção de (Id Transacao)];LookUpQuery[executeLookUpTcsTransacaoAutorizacao];LookUpFinalize[finalizeLookUpTcsTransacaoAutorizacao];LookUpDisplayColumns[{\"DescTransacao\" : \"Transação\", \"IdTransacao\" : \"Id Transacao\"}];LookUpColumns[{\"DescTransacao\" : true, \"IdTransacao\" : false}];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.ID_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdTransacao#true##24:0##Id Transacao#1#false##::LookUpTcsTransacaoAutorizacao##false#false#TCS_TRANSACAO_AUTORIZACAO#TCS_TRANSACAO_AUTORIZACAO#Linx.Framework.BV.ModuloAutorizacao#IQueryable###true#false", EdmKey="TCS_TRANSACAO_MENU_AUTORIZACAO.TCS_TRANSACAO_AUTORIZACAO.ID_TRANSACAO")]
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
	    [Display(Name = "Inativo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_MENU_AUTORIZACAO.INATIVO")]
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
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_MENU_AUTORIZACAO.ORDEM_NAVEGACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO_MENU_AUTORIZACAO.ORDEM_NAVEGACAO")]
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

	    private Int32 _TemporaryIdTcsTransacaoMenuAutorizacao;
	    [DataMember(Name = "TemporaryIdTcsTransacaoMenuAutorizacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Transacao Menu Autorizacao (Tmp)", Description="Temporary Key", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdTcsTransacaoMenuAutorizacao
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTcsTransacaoMenuAutorizacao.IsNullOrEmpty())
	    	                this._TemporaryIdTcsTransacaoMenuAutorizacao = this._IdTcsTransacaoMenuAutorizacao;
	    	          return this._TemporaryIdTcsTransacaoMenuAutorizacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTcsTransacaoMenuAutorizacao != value)
	    	              this._TemporaryIdTcsTransacaoMenuAutorizacao = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsModuloMenuAutorizacao _TcsModuloMenuAutorizacao;
	    [DataMember(Name = "TcsModuloMenuAutorizacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsModuloMenuAutorizacao_TcsTransacaoMenuAutorizacaoModulo", "IdModuloMenu", "IdModuloMenu", IsForeignKey=true)]
	    public TcsModuloMenuAutorizacao TcsModuloMenuAutorizacao
	    {
	        get
	        {
	            return this._TcsModuloMenuAutorizacao;
	        }
	        set
	        {
	            if (this._TcsModuloMenuAutorizacao != value)
	            {
	                this._TcsModuloMenuAutorizacao = value;
	                this.RaisePropertyChanged("TcsModuloMenuAutorizacaoList");
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
	//////////////////////// DomainService Class V1 ///////////////////////
	///////////////////////////////////////////////////////////////////////
	[EnableClientAccess()]	
	[DomainIdentifier("ProcessorOverviewModuloAutorizacaoDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class ModuloAutorizacaoDomainService : DomainService, IDataServiceContext 
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

		
	    public ModuloAutorizacaoDomainService() : this("", null, null) { }
	    public ModuloAutorizacaoDomainService(string connectionString) : this(connectionString, null, null) { }
	    public ModuloAutorizacaoDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public ModuloAutorizacaoDomainService(Linx.Framework.Autorizacao.BM.AutorizacaoContext dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public ModuloAutorizacaoDomainService(string connectionString, Linx.Framework.Autorizacao.BM.AutorizacaoContext dataContext, Dictionary<string, string> headers) : base() 
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
	    			if (entry.Entity is TcsModuloAutorizacao) ((TcsModuloAutorizacao)entry.Entity).SaveMedia(entry.Operation);
	    			if (entry.Entity is TcsModuloMenuAutorizacao) ((TcsModuloMenuAutorizacao)entry.Entity).SaveMedia(entry.Operation);
	    			if (entry.Entity is TcsTransacaoMenuAutorizacaoModulo) ((TcsTransacaoMenuAutorizacaoModulo)entry.Entity).SaveMedia(entry.Operation);
	    		}
	    }

	    private void OnSavedChanges(ChangeSet changeSet)
	    {
	
	
	        TcsModuloAutorizacao.OnSavedContextChanges(this, changeSet.ChangeSetEntries.Where(e => e.Entity is TcsModuloAutorizacao).ToArray());
    	
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
 	        var _TcsModuloAutorizacaoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsModuloAutorizacao && e.Entity.GetType().Name == "TcsModuloAutorizacao" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _TcsModuloAutorizacaoElements)
 	           if (((TcsModuloAutorizacao)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 
 	        //Remove inconsistent details
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsModuloMenuAutorizacao && e.Entity.GetType().Name == "TcsModuloMenuAutorizacao" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsTransacaoMenuAutorizacaoModulo && e.Entity.GetType().Name == "TcsTransacaoMenuAutorizacaoModulo" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
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
	    //Get All LookUpModuloMenuSuperior.
	    public IQueryable<LookUpModuloMenuSuperior> GetAllLookUpModuloMenuSuperior()
	    {
	        return this.GetLookUpModuloMenuSuperior(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpModuloMenuSuperior By EntitySearch.
	    public IQueryable<LookUpModuloMenuSuperior> GetLookUpModuloMenuSuperiorByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpModuloMenuSuperior(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpModuloMenuSuperior.
	    public IQueryable<LookUpModuloMenuSuperior> GetLookUpModuloMenuSuperior(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TCS_MODULO_MENU_AUTORIZACAO" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpModuloMenuSuperior";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpModuloMenuSuperior));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpModuloMenuSuperior> query =  
	
	            (from entity in this.DbContext.TCS_MODULO_MENU_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpModuloMenuSuperior()		
	            {
	            
                DescModulo = entity.TCS_MODULO_AUTORIZACAO.DESC_MODULO
                , DescModuloMenu = entity.MODULO_MENU_SUPERIOR.DESC_MODULO_MENU
                , DescModuloMenuSuperior = entity.DESC_MODULO_MENU
                , IdModulo = entity.TCS_MODULO_AUTORIZACAO.ID_MODULO
                , IdModuloMenuSuperior = entity.ID_MODULO_MENU
	            });

	            
            //Inner Group Definition
            if (propertyName.InList("DescModulo", "IdModulo"))
            {
               query = (from r in query select new LookUpModuloMenuSuperior() {
               DescModulo = r.DescModulo
               , DescModuloMenu = ""
               , DescModuloMenuSuperior = ""
               , IdModulo = r.IdModulo
               , IdModuloMenuSuperior = default(System.Nullable<Int64>)
                }).Distinct();
            }
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpTcsTransacaoAutorizacao.
	    public IQueryable<LookUpTcsTransacaoAutorizacao> GetAllLookUpTcsTransacaoAutorizacao()
	    {
	        return this.GetLookUpTcsTransacaoAutorizacao(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsTransacaoAutorizacao By EntitySearch.
	    public IQueryable<LookUpTcsTransacaoAutorizacao> GetLookUpTcsTransacaoAutorizacaoByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsTransacaoAutorizacao(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsTransacaoAutorizacao.
	    public IQueryable<LookUpTcsTransacaoAutorizacao> GetLookUpTcsTransacaoAutorizacao(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TCS_TRANSACAO_AUTORIZACAO" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsTransacaoAutorizacao";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsTransacaoAutorizacao));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsTransacaoAutorizacao> query =  
	
	            (from entity in this.DbContext.TCS_TRANSACAO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpTcsTransacaoAutorizacao()		
	            {
	            
                DescTransacao = entity.DESC_TRANSACAO
                , IdTransacao = entity.ID_TRANSACAO
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
	
		

	        if (entityName.InList("Linx.Framework.BV.ModuloAutorizacao.TcsModuloAutorizacao"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsModuloAutorizacao",
	        			NameSpace = "Linx.Framework.BV.ModuloAutorizacao",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsModuloAutorizacao",
	        			ClearMethodName = "ClearTcsModuloAutorizacao",
	        			QueryMethodName  = "GetPagedTcsModuloAutorizacao",	
	        			CountingMethodName  = "GetTcsModuloAutorizacao" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.ModuloAutorizacao.TcsModuloAutorizacao"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.ModuloAutorizacao.TcsModuloAutorizacao"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.ModuloAutorizacao.TcsModuloAutorizacao", "Linx.Framework.BV.ModuloAutorizacao.TcsModuloMenuAutorizacao"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsModuloMenuAutorizacao",
	        			NameSpace = "Linx.Framework.BV.ModuloAutorizacao",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsModuloAutorizacao",	
	        			DisplayName = "Menu",
	        			ClearMethodName = "ClearTcsModuloMenuAutorizacao",
	        			QueryMethodName  = "GetPagedTcsModuloMenuAutorizacao",	
	        			CountingMethodName  = "GetTcsModuloMenuAutorizacao" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.ModuloAutorizacao.TcsModuloMenuAutorizacao"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.ModuloAutorizacao.TcsModuloMenuAutorizacao"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.ModuloAutorizacao.TcsModuloAutorizacao", "Linx.Framework.BV.ModuloAutorizacao.TcsTransacaoMenuAutorizacaoModulo"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsTransacaoMenuAutorizacaoModulo",
	        			NameSpace = "Linx.Framework.BV.ModuloAutorizacao",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsModuloMenuAutorizacao",	
	        			DisplayName = "Transação",
	        			ClearMethodName = "ClearTcsTransacaoMenuAutorizacaoModulo",
	        			QueryMethodName  = "GetPagedTcsTransacaoMenuAutorizacaoModulo",	
	        			CountingMethodName  = "GetTcsTransacaoMenuAutorizacaoModulo" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.ModuloAutorizacao.TcsTransacaoMenuAutorizacaoModulo"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.ModuloAutorizacao.TcsTransacaoMenuAutorizacaoModulo"), forceAll: forceAll)
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

         		    return new string[] { "Framework_ModuloAutorizacaoClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.ModuloAutorizacaoClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Framework_moduloAutorizacaoService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.moduloAutorizacaoService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear TcsModuloAutorizacao.
	    public IEnumerable<TcsModuloAutorizacao> ClearTcsModuloAutorizacao()
	    {
	        List<TcsModuloAutorizacao> result = new List<TcsModuloAutorizacao>();
	        result.Add(new TcsModuloAutorizacao(false));	
			
	        result[0].TcsModuloMenuAutorizacaoList = new List<TcsModuloMenuAutorizacao>();
	        ((List<TcsModuloMenuAutorizacao>)result[0].TcsModuloMenuAutorizacaoList).Add(new TcsModuloMenuAutorizacao(false));
			
	        ((List<TcsModuloMenuAutorizacao>)result[0].TcsModuloMenuAutorizacaoList)[0].TcsTransacaoMenuAutorizacaoModuloList = new List<TcsTransacaoMenuAutorizacaoModulo>();
	        ((List<TcsTransacaoMenuAutorizacaoModulo>)((List<TcsModuloMenuAutorizacao>)result[0].TcsModuloMenuAutorizacaoList)[0].TcsTransacaoMenuAutorizacaoModuloList).Add(new TcsTransacaoMenuAutorizacaoModulo());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsModuloMenuAutorizacao.
	    public IEnumerable<TcsModuloMenuAutorizacao> ClearTcsModuloMenuAutorizacao()
	    {
	        List<TcsModuloMenuAutorizacao> result = new List<TcsModuloMenuAutorizacao>();
	        result.Add(new TcsModuloMenuAutorizacao(false));	
			
	        result[0].TcsTransacaoMenuAutorizacaoModuloList = new List<TcsTransacaoMenuAutorizacaoModulo>();
	        ((List<TcsTransacaoMenuAutorizacaoModulo>)result[0].TcsTransacaoMenuAutorizacaoModuloList).Add(new TcsTransacaoMenuAutorizacaoModulo());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsTransacaoMenuAutorizacaoModulo.
	    public IEnumerable<TcsTransacaoMenuAutorizacaoModulo> ClearTcsTransacaoMenuAutorizacaoModulo()
	    {
	        List<TcsTransacaoMenuAutorizacaoModulo> result = new List<TcsTransacaoMenuAutorizacaoModulo>();
	        result.Add(new TcsTransacaoMenuAutorizacaoModulo());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsModuloAutorizacao.
	    public IQueryable<TcsModuloAutorizacao> GetTcsModuloAutorizacao()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsModuloAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_MODULO_AUTORIZACAO
                  let entity0Al1 = entity0.TCS_APLICATIVO
	            
	            	
	            select new TcsModuloAutorizacao()		
	            {
	            
                DescModulo = entity0.DESC_MODULO
                , DescricaoAplicativo = entity0Al1.DESCRICAO_APLICATIVO
                , Icone = entity0.ICONE
                , IdModulo = entity0.ID_MODULO
                , IdTcsAplicativo = entity0Al1.ID_TCS_APLICATIVO
                , Inativo = entity0.INATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , LxCorFundoName = ((entity0.LX_COR_FUNDO) == 8 ? "Fundo Laranja" : ((entity0.LX_COR_FUNDO) == 10 ? "Fundo Roxo" : ((entity0.LX_COR_FUNDO) == 7 ? "Laranja" : ((entity0.LX_COR_FUNDO) == 9 ? "Roxo" : ""))))
                , NomeCurto = entity0.NOME_CURTO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
			
                ,TcsModuloMenuAutorizacaoList = 
	                        (from entity1 in entity0.TCS_MODULO_MENU_AUTORIZACAO_LISTA
                                  let entity1Al1 = entity1.MODULO_MENU_SUPERIOR
                                  let entity1Al3 = entity1.TCS_MODULO_AUTORIZACAO
                                  let entity1Al2 = entity1.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO
	                        
	                        	
	                        select new TcsModuloMenuAutorizacao()
	                        {
	                        
                                DescModulo = entity1Al3.DESC_MODULO
                                , DescModuloMenu = entity1.DESC_MODULO_MENU
                                , DescModuloMenuSuperior = entity1Al1.DESC_MODULO_MENU
                                , DescricaoAplicativo = entity1Al2.DESCRICAO_APLICATIVO
                                , Icone = entity1.ICONE
                                , IdModulo = entity1Al3.ID_MODULO
                                , IdModuloMenu = entity1.ID_MODULO_MENU
                                , IdModuloMenuSuperior = entity1Al1.ID_MODULO_MENU
                                , IdTcsAplicativo = entity1Al2.ID_TCS_APLICATIVO
                                , InativoModulo = entity1Al3.INATIVO
                                , LxCorFundo = entity1.LX_COR_FUNDO
                                , LxCorFundoName = ((entity1.LX_COR_FUNDO) == 8 ? "Fundo Laranja" : ((entity1.LX_COR_FUNDO) == 10 ? "Fundo Roxo" : ((entity1.LX_COR_FUNDO) == 7 ? "Laranja" : ((entity1.LX_COR_FUNDO) == 9 ? "Roxo" : ""))))
                                , NomeCurto = entity1.NOME_CURTO
                                , OrdemNavegacao = entity1.ORDEM_NAVEGACAO
			
                                ,TcsTransacaoMenuAutorizacaoModuloList = 
	                                                (from entity2 in entity1.TCS_TRANSACAO_MENU_AUTORIZACAO_LISTA
                                                                  let entity2Al1 = entity2.TCS_TRANSACAO_AUTORIZACAO
                                                                  let entity2Al2 = entity2.TCS_MODULO_MENU_AUTORIZACAO
	                                                
	                                                	
	                                                select new TcsTransacaoMenuAutorizacaoModulo()
	                                                {
	                                                
                                                                DescTransacao = entity2Al1.DESC_TRANSACAO
                                                                , IdModuloMenu = entity2Al2.ID_MODULO_MENU
                                                                , IdTcsTransacaoMenuAutorizacao = entity2.ID_TCS_TRANSACAO_MENU_AUTORIZACAO
                                                                , IdTransacao = entity2Al1.ID_TRANSACAO
                                                                , Inativo = entity2.INATIVO
                                                                , OrdemNavegacao = entity2.ORDEM_NAVEGACAO
		
	                                                }
	                                                )
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsModuloMenuAutorizacao.
	    public IQueryable<TcsModuloMenuAutorizacao> GetTcsModuloMenuAutorizacao()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsModuloMenuAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_MODULO_MENU_AUTORIZACAO
                  let entity0Al1 = entity0.MODULO_MENU_SUPERIOR
                  let entity0Al3 = entity0.TCS_MODULO_AUTORIZACAO
                  let entity0Al2 = entity0.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO
	            
	            	
	            select new TcsModuloMenuAutorizacao()		
	            {
	            
                DescModulo = entity0Al3.DESC_MODULO
                , DescModuloMenu = entity0.DESC_MODULO_MENU
                , DescModuloMenuSuperior = entity0Al1.DESC_MODULO_MENU
                , DescricaoAplicativo = entity0Al2.DESCRICAO_APLICATIVO
                , Icone = entity0.ICONE
                , IdModulo = entity0Al3.ID_MODULO
                , IdModuloMenu = entity0.ID_MODULO_MENU
                , IdModuloMenuSuperior = entity0Al1.ID_MODULO_MENU
                , IdTcsAplicativo = entity0Al2.ID_TCS_APLICATIVO
                , InativoModulo = entity0Al3.INATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , LxCorFundoName = ((entity0.LX_COR_FUNDO) == 8 ? "Fundo Laranja" : ((entity0.LX_COR_FUNDO) == 10 ? "Fundo Roxo" : ((entity0.LX_COR_FUNDO) == 7 ? "Laranja" : ((entity0.LX_COR_FUNDO) == 9 ? "Roxo" : ""))))
                , NomeCurto = entity0.NOME_CURTO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
			
                ,TcsTransacaoMenuAutorizacaoModuloList = 
	                        (from entity1 in entity0.TCS_TRANSACAO_MENU_AUTORIZACAO_LISTA
                                  let entity1Al1 = entity1.TCS_TRANSACAO_AUTORIZACAO
                                  let entity1Al2 = entity1.TCS_MODULO_MENU_AUTORIZACAO
	                        
	                        	
	                        select new TcsTransacaoMenuAutorizacaoModulo()
	                        {
	                        
                                DescTransacao = entity1Al1.DESC_TRANSACAO
                                , IdModuloMenu = entity1Al2.ID_MODULO_MENU
                                , IdTcsTransacaoMenuAutorizacao = entity1.ID_TCS_TRANSACAO_MENU_AUTORIZACAO
                                , IdTransacao = entity1Al1.ID_TRANSACAO
                                , Inativo = entity1.INATIVO
                                , OrdemNavegacao = entity1.ORDEM_NAVEGACAO
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsTransacaoMenuAutorizacaoModulo.
	    public IQueryable<TcsTransacaoMenuAutorizacaoModulo> GetTcsTransacaoMenuAutorizacaoModulo()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoMenuAutorizacaoModulo> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_MENU_AUTORIZACAO
                  let entity0Al1 = entity0.TCS_TRANSACAO_AUTORIZACAO
                  let entity0Al2 = entity0.TCS_MODULO_MENU_AUTORIZACAO
	            
	            	
	            select new TcsTransacaoMenuAutorizacaoModulo()		
	            {
	            
                DescTransacao = entity0Al1.DESC_TRANSACAO
                , IdModuloMenu = entity0Al2.ID_MODULO_MENU
                , IdTcsTransacaoMenuAutorizacao = entity0.ID_TCS_TRANSACAO_MENU_AUTORIZACAO
                , IdTransacao = entity0Al1.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsModuloAutorizacaoNoAssociations.
	    public IQueryable<TcsModuloAutorizacao> GetTcsModuloAutorizacaoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsModuloAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_MODULO_AUTORIZACAO
                  let entity0Al1 = entity0.TCS_APLICATIVO
	            
	            	
	            select new TcsModuloAutorizacao()		
	            {
	            
                DescModulo = entity0.DESC_MODULO
                , DescricaoAplicativo = entity0Al1.DESCRICAO_APLICATIVO
                , Icone = entity0.ICONE
                , IdModulo = entity0.ID_MODULO
                , IdTcsAplicativo = entity0Al1.ID_TCS_APLICATIVO
                , Inativo = entity0.INATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , LxCorFundoName = ((entity0.LX_COR_FUNDO) == 8 ? "Fundo Laranja" : ((entity0.LX_COR_FUNDO) == 10 ? "Fundo Roxo" : ((entity0.LX_COR_FUNDO) == 7 ? "Laranja" : ((entity0.LX_COR_FUNDO) == 9 ? "Roxo" : ""))))
                , NomeCurto = entity0.NOME_CURTO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsModuloMenuAutorizacaoNoAssociations.
	    public IQueryable<TcsModuloMenuAutorizacao> GetTcsModuloMenuAutorizacaoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsModuloMenuAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_MODULO_MENU_AUTORIZACAO
                  let entity0Al1 = entity0.MODULO_MENU_SUPERIOR
                  let entity0Al3 = entity0.TCS_MODULO_AUTORIZACAO
                  let entity0Al2 = entity0.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO
	            
	            	
	            select new TcsModuloMenuAutorizacao()		
	            {
	            
                DescModulo = entity0Al3.DESC_MODULO
                , DescModuloMenu = entity0.DESC_MODULO_MENU
                , DescModuloMenuSuperior = entity0Al1.DESC_MODULO_MENU
                , DescricaoAplicativo = entity0Al2.DESCRICAO_APLICATIVO
                , Icone = entity0.ICONE
                , IdModulo = entity0Al3.ID_MODULO
                , IdModuloMenu = entity0.ID_MODULO_MENU
                , IdModuloMenuSuperior = entity0Al1.ID_MODULO_MENU
                , IdTcsAplicativo = entity0Al2.ID_TCS_APLICATIVO
                , InativoModulo = entity0Al3.INATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , LxCorFundoName = ((entity0.LX_COR_FUNDO) == 8 ? "Fundo Laranja" : ((entity0.LX_COR_FUNDO) == 10 ? "Fundo Roxo" : ((entity0.LX_COR_FUNDO) == 7 ? "Laranja" : ((entity0.LX_COR_FUNDO) == 9 ? "Roxo" : ""))))
                , NomeCurto = entity0.NOME_CURTO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsTransacaoMenuAutorizacaoModuloNoAssociations.
	    public IQueryable<TcsTransacaoMenuAutorizacaoModulo> GetTcsTransacaoMenuAutorizacaoModuloNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoMenuAutorizacaoModulo> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_MENU_AUTORIZACAO
                  let entity0Al1 = entity0.TCS_TRANSACAO_AUTORIZACAO
                  let entity0Al2 = entity0.TCS_MODULO_MENU_AUTORIZACAO
	            
	            	
	            select new TcsTransacaoMenuAutorizacaoModulo()		
	            {
	            
                DescTransacao = entity0Al1.DESC_TRANSACAO
                , IdModuloMenu = entity0Al2.ID_MODULO_MENU
                , IdTcsTransacaoMenuAutorizacao = entity0.ID_TCS_TRANSACAO_MENU_AUTORIZACAO
                , IdTransacao = entity0Al1.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	result.Add("TcsModuloAutorizacao|LxCorFundo");
	    	result.Add("TcsModuloAutorizacao|TCS_MODULO_AUTORIZACAO.LX_COR_FUNDO");
	    	//Add filtering disabled property for TCS_MODULO_AUTORIZACAO
	    	string[] bmDisabledTcsModuloAutorizacaoList = this.GetEDM().GetFilteringDisabledList("TCS_MODULO_AUTORIZACAO");
	    	if (bmDisabledTcsModuloAutorizacaoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsModuloAutorizacaoList.Contains("TCS_MODULO_AUTORIZACAO.DESC_MODULO"))
	    		{
	    			result.Add("TcsModuloAutorizacao|DescModulo");
	    			result.Add("TcsModuloAutorizacao|TCS_MODULO_AUTORIZACAO.DESC_MODULO");
	    		}
	
	    		if (bmDisabledTcsModuloAutorizacaoList.Contains("TCS_MODULO_AUTORIZACAO.ICONE"))
	    		{
	    			result.Add("TcsModuloAutorizacao|Icone");
	    			result.Add("TcsModuloAutorizacao|TCS_MODULO_AUTORIZACAO.ICONE");
	    		}
	
	    		if (bmDisabledTcsModuloAutorizacaoList.Contains("TCS_MODULO_AUTORIZACAO.ID_MODULO"))
	    		{
	    			result.Add("TcsModuloAutorizacao|IdModulo");
	    			result.Add("TcsModuloAutorizacao|TCS_MODULO_AUTORIZACAO.ID_MODULO");
	    		}
	
	    		if (bmDisabledTcsModuloAutorizacaoList.Contains("TCS_MODULO_AUTORIZACAO.INATIVO"))
	    		{
	    			result.Add("TcsModuloAutorizacao|Inativo");
	    			result.Add("TcsModuloAutorizacao|TCS_MODULO_AUTORIZACAO.INATIVO");
	    		}
	
	    		if (bmDisabledTcsModuloAutorizacaoList.Contains("TCS_MODULO_AUTORIZACAO.NOME_CURTO"))
	    		{
	    			result.Add("TcsModuloAutorizacao|NomeCurto");
	    			result.Add("TcsModuloAutorizacao|TCS_MODULO_AUTORIZACAO.NOME_CURTO");
	    		}
	
	    		if (bmDisabledTcsModuloAutorizacaoList.Contains("TCS_MODULO_AUTORIZACAO.ORDEM_NAVEGACAO"))
	    		{
	    			result.Add("TcsModuloAutorizacao|OrdemNavegacao");
	    			result.Add("TcsModuloAutorizacao|TCS_MODULO_AUTORIZACAO.ORDEM_NAVEGACAO");
	    		}
	    	}
	    	result.Add("TcsModuloMenuAutorizacao|DescModulo");
	    	result.Add("TcsModuloMenuAutorizacao|TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.DESC_MODULO");
	    	result.Add("TcsModuloMenuAutorizacao|DescModuloMenu");
	    	result.Add("TcsModuloMenuAutorizacao|TCS_MODULO_MENU_AUTORIZACAO.DESC_MODULO_MENU");
	    	result.Add("TcsModuloMenuAutorizacao|LxCorFundo");
	    	result.Add("TcsModuloMenuAutorizacao|TCS_MODULO_MENU_AUTORIZACAO.LX_COR_FUNDO");
	    	//Add filtering disabled property for TCS_MODULO_MENU_AUTORIZACAO
	    	string[] bmDisabledTcsModuloMenuAutorizacaoList = this.GetEDM().GetFilteringDisabledList("TCS_MODULO_MENU_AUTORIZACAO");
	    	if (bmDisabledTcsModuloMenuAutorizacaoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsModuloMenuAutorizacaoList.Contains("TCS_MODULO_MENU_AUTORIZACAO.ICONE"))
	    		{
	    			result.Add("TcsModuloMenuAutorizacao|Icone");
	    			result.Add("TcsModuloMenuAutorizacao|TCS_MODULO_MENU_AUTORIZACAO.ICONE");
	    		}
	
	    		if (bmDisabledTcsModuloMenuAutorizacaoList.Contains("TCS_MODULO_MENU_AUTORIZACAO.ID_MODULO_MENU"))
	    		{
	    			result.Add("TcsModuloMenuAutorizacao|IdModuloMenu");
	    			result.Add("TcsModuloMenuAutorizacao|TCS_MODULO_MENU_AUTORIZACAO.ID_MODULO_MENU");
	    		}
	
	    		if (bmDisabledTcsModuloMenuAutorizacaoList.Contains("TCS_MODULO_MENU_AUTORIZACAO.NOME_CURTO"))
	    		{
	    			result.Add("TcsModuloMenuAutorizacao|NomeCurto");
	    			result.Add("TcsModuloMenuAutorizacao|TCS_MODULO_MENU_AUTORIZACAO.NOME_CURTO");
	    		}
	
	    		if (bmDisabledTcsModuloMenuAutorizacaoList.Contains("TCS_MODULO_MENU_AUTORIZACAO.ORDEM_NAVEGACAO"))
	    		{
	    			result.Add("TcsModuloMenuAutorizacao|OrdemNavegacao");
	    			result.Add("TcsModuloMenuAutorizacao|TCS_MODULO_MENU_AUTORIZACAO.ORDEM_NAVEGACAO");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_TRANSACAO_MENU_AUTORIZACAO
	    	string[] bmDisabledTcsTransacaoMenuAutorizacaoModuloList = this.GetEDM().GetFilteringDisabledList("TCS_TRANSACAO_MENU_AUTORIZACAO");
	    	if (bmDisabledTcsTransacaoMenuAutorizacaoModuloList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsTransacaoMenuAutorizacaoModuloList.Contains("TCS_TRANSACAO_MENU_AUTORIZACAO.ID_TCS_TRANSACAO_MENU_AUTORIZACAO"))
	    		{
	    			result.Add("TcsTransacaoMenuAutorizacaoModulo|IdTcsTransacaoMenuAutorizacao");
	    			result.Add("TcsTransacaoMenuAutorizacaoModulo|TCS_TRANSACAO_MENU_AUTORIZACAO.ID_TCS_TRANSACAO_MENU_AUTORIZACAO");
	    		}
	
	    		if (bmDisabledTcsTransacaoMenuAutorizacaoModuloList.Contains("TCS_TRANSACAO_MENU_AUTORIZACAO.INATIVO"))
	    		{
	    			result.Add("TcsTransacaoMenuAutorizacaoModulo|Inativo");
	    			result.Add("TcsTransacaoMenuAutorizacaoModulo|TCS_TRANSACAO_MENU_AUTORIZACAO.INATIVO");
	    		}
	
	    		if (bmDisabledTcsTransacaoMenuAutorizacaoModuloList.Contains("TCS_TRANSACAO_MENU_AUTORIZACAO.ORDEM_NAVEGACAO"))
	    		{
	    			result.Add("TcsTransacaoMenuAutorizacaoModulo|OrdemNavegacao");
	    			result.Add("TcsTransacaoMenuAutorizacaoModulo|TCS_TRANSACAO_MENU_AUTORIZACAO.ORDEM_NAVEGACAO");
	    		}
	    	}
	    	return result;
	    }

	    #endregion Excluded Filters

	    #region Get By EntitySearchId Methods Definitions.
	
				
	    [Ignore]
	    //Get TcsModuloAutorizacao By EntitySearchId.
	    public IQueryable<TcsModuloAutorizacao> GetTcsModuloAutorizacaoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsModuloAutorizacaoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsModuloMenuAutorizacao By EntitySearchId.
	    public IQueryable<TcsModuloMenuAutorizacao> GetTcsModuloMenuAutorizacaoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsModuloMenuAutorizacaoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsTransacaoMenuAutorizacaoModulo By EntitySearchId.
	    public IQueryable<TcsTransacaoMenuAutorizacaoModulo> GetTcsTransacaoMenuAutorizacaoModuloByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsTransacaoMenuAutorizacaoModuloByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsModuloAutorizacao By EntitySearchId.
	    public IQueryable<TcsModuloAutorizacao> GetTcsModuloAutorizacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsModuloAutorizacaoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsModuloMenuAutorizacao By EntitySearchId.
	    public IQueryable<TcsModuloMenuAutorizacao> GetTcsModuloMenuAutorizacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsModuloMenuAutorizacaoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsTransacaoMenuAutorizacaoModulo By EntitySearchId.
	    public IQueryable<TcsTransacaoMenuAutorizacaoModulo> GetTcsTransacaoMenuAutorizacaoModuloByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsTransacaoMenuAutorizacaoModuloByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get TcsModuloAutorizacao By Example.
	    [Ignore]
	    public IQueryable<TcsModuloAutorizacao> GetTcsModuloAutorizacaoByExample(TcsModuloAutorizacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsModuloAutorizacaoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsModuloMenuAutorizacao By Example.
	    [Ignore]
	    public IQueryable<TcsModuloMenuAutorizacao> GetTcsModuloMenuAutorizacaoByExample(TcsModuloMenuAutorizacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsModuloMenuAutorizacaoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsTransacaoMenuAutorizacaoModulo By Example.
	    [Ignore]
	    public IQueryable<TcsTransacaoMenuAutorizacaoModulo> GetTcsTransacaoMenuAutorizacaoModuloByExample(TcsTransacaoMenuAutorizacaoModulo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsTransacaoMenuAutorizacaoModuloByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsModuloAutorizacao By Example.
	    [Ignore]
	    public IQueryable<TcsModuloAutorizacao> GetTcsModuloAutorizacaoByExampleNoAssociations(TcsModuloAutorizacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsModuloAutorizacaoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsModuloMenuAutorizacao By Example.
	    [Ignore]
	    public IQueryable<TcsModuloMenuAutorizacao> GetTcsModuloMenuAutorizacaoByExampleNoAssociations(TcsModuloMenuAutorizacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsModuloMenuAutorizacaoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsTransacaoMenuAutorizacaoModulo By Example.
	    [Ignore]
	    public IQueryable<TcsTransacaoMenuAutorizacaoModulo> GetTcsTransacaoMenuAutorizacaoModuloByExampleNoAssociations(TcsTransacaoMenuAutorizacaoModulo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsTransacaoMenuAutorizacaoModuloByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public TcsModuloAutorizacao GetTcsModuloAutorizacaoByKey(Int64 idModulo)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsModuloAutorizacao");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdModulo"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idModulo));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsModuloAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsModuloMenuAutorizacao GetTcsModuloMenuAutorizacaoByKey(Int64 idModuloMenu)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsModuloMenuAutorizacao");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdModuloMenu"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idModuloMenu));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsModuloMenuAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsTransacaoMenuAutorizacaoModulo GetTcsTransacaoMenuAutorizacaoModuloByKey(Int32 idTcsTransacaoMenuAutorizacao)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsTransacaoMenuAutorizacaoModulo");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsTransacaoMenuAutorizacao"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsTransacaoMenuAutorizacao));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsTransacaoMenuAutorizacaoModuloByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get TcsModuloAutorizacaoByEntitySearch.
	    public IQueryable<TcsModuloAutorizacao> GetTcsModuloAutorizacaoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsModuloAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsModuloAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_MODULO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_APLICATIVO
	            
	            	
	            select new TcsModuloAutorizacao()		
	            {
	            
                DescModulo = entity0.DESC_MODULO
                , DescricaoAplicativo = entity0Al1.DESCRICAO_APLICATIVO
                , Icone = entity0.ICONE
                , IdModulo = entity0.ID_MODULO
                , IdTcsAplicativo = entity0Al1.ID_TCS_APLICATIVO
                , Inativo = entity0.INATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , LxCorFundoName = ((entity0.LX_COR_FUNDO) == 8 ? "Fundo Laranja" : ((entity0.LX_COR_FUNDO) == 10 ? "Fundo Roxo" : ((entity0.LX_COR_FUNDO) == 7 ? "Laranja" : ((entity0.LX_COR_FUNDO) == 9 ? "Roxo" : ""))))
                , NomeCurto = entity0.NOME_CURTO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
			
                ,TcsModuloMenuAutorizacaoList = 
	                        (from entity1 in entity0.TCS_MODULO_MENU_AUTORIZACAO_LISTA
                                  let entity1Al1 = entity1.MODULO_MENU_SUPERIOR
                                  let entity1Al3 = entity1.TCS_MODULO_AUTORIZACAO
                                  let entity1Al2 = entity1.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO
	                        
	                        	
	                        select new TcsModuloMenuAutorizacao()
	                        {
	                        
                                DescModulo = entity1Al3.DESC_MODULO
                                , DescModuloMenu = entity1.DESC_MODULO_MENU
                                , DescModuloMenuSuperior = entity1Al1.DESC_MODULO_MENU
                                , DescricaoAplicativo = entity1Al2.DESCRICAO_APLICATIVO
                                , Icone = entity1.ICONE
                                , IdModulo = entity1Al3.ID_MODULO
                                , IdModuloMenu = entity1.ID_MODULO_MENU
                                , IdModuloMenuSuperior = entity1Al1.ID_MODULO_MENU
                                , IdTcsAplicativo = entity1Al2.ID_TCS_APLICATIVO
                                , InativoModulo = entity1Al3.INATIVO
                                , LxCorFundo = entity1.LX_COR_FUNDO
                                , LxCorFundoName = ((entity1.LX_COR_FUNDO) == 8 ? "Fundo Laranja" : ((entity1.LX_COR_FUNDO) == 10 ? "Fundo Roxo" : ((entity1.LX_COR_FUNDO) == 7 ? "Laranja" : ((entity1.LX_COR_FUNDO) == 9 ? "Roxo" : ""))))
                                , NomeCurto = entity1.NOME_CURTO
                                , OrdemNavegacao = entity1.ORDEM_NAVEGACAO
			
                                ,TcsTransacaoMenuAutorizacaoModuloList = 
	                                                (from entity2 in entity1.TCS_TRANSACAO_MENU_AUTORIZACAO_LISTA
                                                                  let entity2Al1 = entity2.TCS_TRANSACAO_AUTORIZACAO
                                                                  let entity2Al2 = entity2.TCS_MODULO_MENU_AUTORIZACAO
	                                                
	                                                	
	                                                select new TcsTransacaoMenuAutorizacaoModulo()
	                                                {
	                                                
                                                                DescTransacao = entity2Al1.DESC_TRANSACAO
                                                                , IdModuloMenu = entity2Al2.ID_MODULO_MENU
                                                                , IdTcsTransacaoMenuAutorizacao = entity2.ID_TCS_TRANSACAO_MENU_AUTORIZACAO
                                                                , IdTransacao = entity2Al1.ID_TRANSACAO
                                                                , Inativo = entity2.INATIVO
                                                                , OrdemNavegacao = entity2.ORDEM_NAVEGACAO
		
	                                                }
	                                                )
		
	                        }
	                        )
		
	            }
	            );
	
	        SetTcsModuloAutorizacaoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsModuloMenuAutorizacaoByEntitySearch.
	    public IQueryable<TcsModuloMenuAutorizacao> GetTcsModuloMenuAutorizacaoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsModuloMenuAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsModuloMenuAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_MODULO_MENU_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.MODULO_MENU_SUPERIOR
                  let entity0Al3 = entity0.TCS_MODULO_AUTORIZACAO
                  let entity0Al2 = entity0.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO
	            
	            	
	            select new TcsModuloMenuAutorizacao()		
	            {
	            
                DescModulo = entity0Al3.DESC_MODULO
                , DescModuloMenu = entity0.DESC_MODULO_MENU
                , DescModuloMenuSuperior = entity0Al1.DESC_MODULO_MENU
                , DescricaoAplicativo = entity0Al2.DESCRICAO_APLICATIVO
                , Icone = entity0.ICONE
                , IdModulo = entity0Al3.ID_MODULO
                , IdModuloMenu = entity0.ID_MODULO_MENU
                , IdModuloMenuSuperior = entity0Al1.ID_MODULO_MENU
                , IdTcsAplicativo = entity0Al2.ID_TCS_APLICATIVO
                , InativoModulo = entity0Al3.INATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , LxCorFundoName = ((entity0.LX_COR_FUNDO) == 8 ? "Fundo Laranja" : ((entity0.LX_COR_FUNDO) == 10 ? "Fundo Roxo" : ((entity0.LX_COR_FUNDO) == 7 ? "Laranja" : ((entity0.LX_COR_FUNDO) == 9 ? "Roxo" : ""))))
                , NomeCurto = entity0.NOME_CURTO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
			
                ,TcsTransacaoMenuAutorizacaoModuloList = 
	                        (from entity1 in entity0.TCS_TRANSACAO_MENU_AUTORIZACAO_LISTA
                                  let entity1Al1 = entity1.TCS_TRANSACAO_AUTORIZACAO
                                  let entity1Al2 = entity1.TCS_MODULO_MENU_AUTORIZACAO
	                        
	                        	
	                        select new TcsTransacaoMenuAutorizacaoModulo()
	                        {
	                        
                                DescTransacao = entity1Al1.DESC_TRANSACAO
                                , IdModuloMenu = entity1Al2.ID_MODULO_MENU
                                , IdTcsTransacaoMenuAutorizacao = entity1.ID_TCS_TRANSACAO_MENU_AUTORIZACAO
                                , IdTransacao = entity1Al1.ID_TRANSACAO
                                , Inativo = entity1.INATIVO
                                , OrdemNavegacao = entity1.ORDEM_NAVEGACAO
		
	                        }
	                        )
		
	            }
	            );
	
	        SetTcsModuloMenuAutorizacaoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsTransacaoMenuAutorizacaoModuloByEntitySearch.
	    public IQueryable<TcsTransacaoMenuAutorizacaoModulo> GetTcsTransacaoMenuAutorizacaoModuloByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacaoMenuAutorizacaoModulo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoMenuAutorizacaoModulo> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_MENU_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_TRANSACAO_AUTORIZACAO
                  let entity0Al2 = entity0.TCS_MODULO_MENU_AUTORIZACAO
	            
	            	
	            select new TcsTransacaoMenuAutorizacaoModulo()		
	            {
	            
                DescTransacao = entity0Al1.DESC_TRANSACAO
                , IdModuloMenu = entity0Al2.ID_MODULO_MENU
                , IdTcsTransacaoMenuAutorizacao = entity0.ID_TCS_TRANSACAO_MENU_AUTORIZACAO
                , IdTransacao = entity0Al1.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsModuloAutorizacaoByEntitySearchNoAssociations.
	    public IQueryable<TcsModuloAutorizacao> GetTcsModuloAutorizacaoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsModuloAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsModuloAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_MODULO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_APLICATIVO
	            
	            	
	            select new TcsModuloAutorizacao()		
	            {
	            
                DescModulo = entity0.DESC_MODULO
                , DescricaoAplicativo = entity0Al1.DESCRICAO_APLICATIVO
                , Icone = entity0.ICONE
                , IdModulo = entity0.ID_MODULO
                , IdTcsAplicativo = entity0Al1.ID_TCS_APLICATIVO
                , Inativo = entity0.INATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , LxCorFundoName = ((entity0.LX_COR_FUNDO) == 8 ? "Fundo Laranja" : ((entity0.LX_COR_FUNDO) == 10 ? "Fundo Roxo" : ((entity0.LX_COR_FUNDO) == 7 ? "Laranja" : ((entity0.LX_COR_FUNDO) == 9 ? "Roxo" : ""))))
                , NomeCurto = entity0.NOME_CURTO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            );
	
	        SetTcsModuloAutorizacaoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsModuloMenuAutorizacaoByEntitySearchNoAssociations.
	    public IQueryable<TcsModuloMenuAutorizacao> GetTcsModuloMenuAutorizacaoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsModuloMenuAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsModuloMenuAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_MODULO_MENU_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.MODULO_MENU_SUPERIOR
                  let entity0Al3 = entity0.TCS_MODULO_AUTORIZACAO
                  let entity0Al2 = entity0.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO
	            
	            	
	            select new TcsModuloMenuAutorizacao()		
	            {
	            
                DescModulo = entity0Al3.DESC_MODULO
                , DescModuloMenu = entity0.DESC_MODULO_MENU
                , DescModuloMenuSuperior = entity0Al1.DESC_MODULO_MENU
                , DescricaoAplicativo = entity0Al2.DESCRICAO_APLICATIVO
                , Icone = entity0.ICONE
                , IdModulo = entity0Al3.ID_MODULO
                , IdModuloMenu = entity0.ID_MODULO_MENU
                , IdModuloMenuSuperior = entity0Al1.ID_MODULO_MENU
                , IdTcsAplicativo = entity0Al2.ID_TCS_APLICATIVO
                , InativoModulo = entity0Al3.INATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , LxCorFundoName = ((entity0.LX_COR_FUNDO) == 8 ? "Fundo Laranja" : ((entity0.LX_COR_FUNDO) == 10 ? "Fundo Roxo" : ((entity0.LX_COR_FUNDO) == 7 ? "Laranja" : ((entity0.LX_COR_FUNDO) == 9 ? "Roxo" : ""))))
                , NomeCurto = entity0.NOME_CURTO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            );
	
	        SetTcsModuloMenuAutorizacaoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsTransacaoMenuAutorizacaoModuloByEntitySearchNoAssociations.
	    public IQueryable<TcsTransacaoMenuAutorizacaoModulo> GetTcsTransacaoMenuAutorizacaoModuloByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacaoMenuAutorizacaoModulo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoMenuAutorizacaoModulo> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_MENU_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_TRANSACAO_AUTORIZACAO
                  let entity0Al2 = entity0.TCS_MODULO_MENU_AUTORIZACAO
	            
	            	
	            select new TcsTransacaoMenuAutorizacaoModulo()		
	            {
	            
                DescTransacao = entity0Al1.DESC_TRANSACAO
                , IdModuloMenu = entity0Al2.ID_MODULO_MENU
                , IdTcsTransacaoMenuAutorizacao = entity0.ID_TCS_TRANSACAO_MENU_AUTORIZACAO
                , IdTransacao = entity0Al1.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
	
	    [Ignore()]
	    private void SetTcsModuloAutorizacaoBusinessFilter(ref IQueryable<TcsModuloAutorizacao> query, List<EntitySearch> entitySearchList)
	    {
	    		int idxElement;
	    		string operatorValue;
	    		object value;
	    		//Get query by functions
	    		if (entitySearchList.Count > 0)
	    		{
	    			foreach (EntitySearch search in entitySearchList.Where(e => e.EntityName == "TcsModuloAutorizacao"))
	    			{

	
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "LxCorFundo" || e.Value.ToString() == "TCS_MODULO_AUTORIZACAO.LX_COR_FUNDO")))
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
	    										System.Nullable<System.Int32> tmpLxCorFundo1 = (System.Nullable<System.Int32>)value;
	    										query = from r in query where r.LxCorFundo == tmpLxCorFundo1 select r;
	    										break;
	    									case "!=":
	    										System.Nullable<System.Int32> tmpLxCorFundo2 = (System.Nullable<System.Int32>)value;
	    										query = from r in query where r.LxCorFundo != tmpLxCorFundo2 select r;
	    										break;

	
	    									case "<":
	    										System.Nullable<System.Int32> tmpLxCorFundo3 = (System.Nullable<System.Int32>)value;
	    										query = from r in query where r.LxCorFundo < tmpLxCorFundo3 select r;
	    										break;
	    									case "<=":
	    										System.Nullable<System.Int32> tmpLxCorFundo4 = (System.Nullable<System.Int32>)value;
	    										query = from r in query where r.LxCorFundo <= tmpLxCorFundo4 select r;
	    										break;
	    									case ">":
	    										System.Nullable<System.Int32> tmpLxCorFundo5 = (System.Nullable<System.Int32>)value;
	    										query = from r in query where r.LxCorFundo > tmpLxCorFundo5 select r;
	    										break;
	    									case ">=":
	    										System.Nullable<System.Int32> tmpLxCorFundo6 = (System.Nullable<System.Int32>)value;
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
	    private void SetTcsModuloMenuAutorizacaoBusinessFilter(ref IQueryable<TcsModuloMenuAutorizacao> query, List<EntitySearch> entitySearchList)
	    {
	    		int idxElement;
	    		string operatorValue;
	    		object value;
	    		//Get query by functions
	    		if (entitySearchList.Count > 0)
	    		{
	    			foreach (EntitySearch search in entitySearchList.Where(e => e.EntityName == "TcsModuloMenuAutorizacao"))
	    			{

	
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "DescModulo" || e.Value.ToString() == "TCS_MODULO_MENU_AUTORIZACAO.TCS_MODULO_AUTORIZACAO.DESC_MODULO")))
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
	    										System.String tmpDescModulo1 = (System.String)value;
	    										query = from r in query where r.DescModulo == tmpDescModulo1 select r;
	    										break;
	    									case "!=":
	    										System.String tmpDescModulo2 = (System.String)value;
	    										query = from r in query where r.DescModulo != tmpDescModulo2 select r;
	    										break;

	
	    									case "Contains":
	    										System.String tmpDescModulo7 = (System.String)value;
	    									    query = from r in query where r.DescModulo.Contains(tmpDescModulo7) select r;
	    									    break;
	    									case "StartsWith":
	    										System.String tmpDescModulo8 = (System.String)value;
	    									    query = from r in query where r.DescModulo.StartsWith(tmpDescModulo8) select r;
	    									    break;
	    									case "EndsWith":
	    										System.String tmpDescModulo9 = (System.String)value;
	    									    query = from r in query where r.DescModulo.EndsWith(tmpDescModulo9) select r;
	    									    break;

	
	    									default:
	    										break;
	    								}                                
	    							}
	    						}
        					} 

    
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "DescModuloMenu" || e.Value.ToString() == "TCS_MODULO_MENU_AUTORIZACAO.DESC_MODULO_MENU")))
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
	    										System.String tmpDescModuloMenu1 = (System.String)value;
	    										query = from r in query where r.DescModuloMenu == tmpDescModuloMenu1 select r;
	    										break;
	    									case "!=":
	    										System.String tmpDescModuloMenu2 = (System.String)value;
	    										query = from r in query where r.DescModuloMenu != tmpDescModuloMenu2 select r;
	    										break;

	
	    									case "Contains":
	    										System.String tmpDescModuloMenu7 = (System.String)value;
	    									    query = from r in query where r.DescModuloMenu.Contains(tmpDescModuloMenu7) select r;
	    									    break;
	    									case "StartsWith":
	    										System.String tmpDescModuloMenu8 = (System.String)value;
	    									    query = from r in query where r.DescModuloMenu.StartsWith(tmpDescModuloMenu8) select r;
	    									    break;
	    									case "EndsWith":
	    										System.String tmpDescModuloMenu9 = (System.String)value;
	    									    query = from r in query where r.DescModuloMenu.EndsWith(tmpDescModuloMenu9) select r;
	    									    break;

	
	    									default:
	    										break;
	    								}                                
	    							}
	    						}
        					} 

    
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "LxCorFundo" || e.Value.ToString() == "TCS_MODULO_MENU_AUTORIZACAO.LX_COR_FUNDO")))
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
	    										System.Nullable<System.Int32> tmpLxCorFundo1 = (System.Nullable<System.Int32>)value;
	    										query = from r in query where r.LxCorFundo == tmpLxCorFundo1 select r;
	    										break;
	    									case "!=":
	    										System.Nullable<System.Int32> tmpLxCorFundo2 = (System.Nullable<System.Int32>)value;
	    										query = from r in query where r.LxCorFundo != tmpLxCorFundo2 select r;
	    										break;

	
	    									case "<":
	    										System.Nullable<System.Int32> tmpLxCorFundo3 = (System.Nullable<System.Int32>)value;
	    										query = from r in query where r.LxCorFundo < tmpLxCorFundo3 select r;
	    										break;
	    									case "<=":
	    										System.Nullable<System.Int32> tmpLxCorFundo4 = (System.Nullable<System.Int32>)value;
	    										query = from r in query where r.LxCorFundo <= tmpLxCorFundo4 select r;
	    										break;
	    									case ">":
	    										System.Nullable<System.Int32> tmpLxCorFundo5 = (System.Nullable<System.Int32>)value;
	    										query = from r in query where r.LxCorFundo > tmpLxCorFundo5 select r;
	    										break;
	    									case ">=":
	    										System.Nullable<System.Int32> tmpLxCorFundo6 = (System.Nullable<System.Int32>)value;
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


	
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get PagedTcsModuloAutorizacao.
	    public IQueryable<TcsModuloAutorizacao> GetPagedTcsModuloAutorizacao(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsModuloAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsModuloAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_MODULO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_APLICATIVO
                orderby entity0.ID_MODULO ascending
	            
	            	
	            select new TcsModuloAutorizacao()		
	            {
	            
                DescModulo = entity0.DESC_MODULO
                , DescricaoAplicativo = entity0Al1.DESCRICAO_APLICATIVO
                , Icone = entity0.ICONE
                , IdModulo = entity0.ID_MODULO
                , IdTcsAplicativo = entity0Al1.ID_TCS_APLICATIVO
                , Inativo = entity0.INATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , LxCorFundoName = ((entity0.LX_COR_FUNDO) == 8 ? "Fundo Laranja" : ((entity0.LX_COR_FUNDO) == 10 ? "Fundo Roxo" : ((entity0.LX_COR_FUNDO) == 7 ? "Laranja" : ((entity0.LX_COR_FUNDO) == 9 ? "Roxo" : ""))))
                , NomeCurto = entity0.NOME_CURTO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            ).Skip(skip).Take(take);
	
	        SetTcsModuloAutorizacaoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsModuloMenuAutorizacao.
	    public IQueryable<TcsModuloMenuAutorizacao> GetPagedTcsModuloMenuAutorizacao(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsModuloMenuAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsModuloMenuAutorizacao> result = 
	            (from entity0 in this.DbContext.TCS_MODULO_MENU_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.MODULO_MENU_SUPERIOR
                  let entity0Al3 = entity0.TCS_MODULO_AUTORIZACAO
                  let entity0Al2 = entity0.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO
                orderby entity0.ID_MODULO_MENU ascending
	            
	            	
	            select new TcsModuloMenuAutorizacao()		
	            {
	            
                DescModulo = entity0Al3.DESC_MODULO
                , DescModuloMenu = entity0.DESC_MODULO_MENU
                , DescModuloMenuSuperior = entity0Al1.DESC_MODULO_MENU
                , DescricaoAplicativo = entity0Al2.DESCRICAO_APLICATIVO
                , Icone = entity0.ICONE
                , IdModulo = entity0Al3.ID_MODULO
                , IdModuloMenu = entity0.ID_MODULO_MENU
                , IdModuloMenuSuperior = entity0Al1.ID_MODULO_MENU
                , IdTcsAplicativo = entity0Al2.ID_TCS_APLICATIVO
                , InativoModulo = entity0Al3.INATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , LxCorFundoName = ((entity0.LX_COR_FUNDO) == 8 ? "Fundo Laranja" : ((entity0.LX_COR_FUNDO) == 10 ? "Fundo Roxo" : ((entity0.LX_COR_FUNDO) == 7 ? "Laranja" : ((entity0.LX_COR_FUNDO) == 9 ? "Roxo" : ""))))
                , NomeCurto = entity0.NOME_CURTO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            ).Skip(skip).Take(take);
	
	        SetTcsModuloMenuAutorizacaoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsTransacaoMenuAutorizacaoModulo.
	    public IQueryable<TcsTransacaoMenuAutorizacaoModulo> GetPagedTcsTransacaoMenuAutorizacaoModulo(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacaoMenuAutorizacaoModulo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsTransacaoMenuAutorizacaoModulo> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO_MENU_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_TRANSACAO_AUTORIZACAO
                  let entity0Al2 = entity0.TCS_MODULO_MENU_AUTORIZACAO
                orderby entity0.ID_TCS_TRANSACAO_MENU_AUTORIZACAO ascending
	            
	            	
	            select new TcsTransacaoMenuAutorizacaoModulo()		
	            {
	            
                DescTransacao = entity0Al1.DESC_TRANSACAO
                , IdModuloMenu = entity0Al2.ID_MODULO_MENU
                , IdTcsTransacaoMenuAutorizacao = entity0.ID_TCS_TRANSACAO_MENU_AUTORIZACAO
                , IdTransacao = entity0Al1.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsModuloAutorizacaoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsModuloAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_MODULO_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_APLICATIVO
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsModuloMenuAutorizacaoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsModuloMenuAutorizacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_MODULO_MENU_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.MODULO_MENU_SUPERIOR
                  let entityAl3 = entity.TCS_MODULO_AUTORIZACAO
                  let entityAl2 = entity.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsTransacaoMenuAutorizacaoModuloCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsTransacaoMenuAutorizacaoModulo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_TRANSACAO_MENU_AUTORIZACAO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_TRANSACAO_AUTORIZACAO
                  let entityAl2 = entity.TCS_MODULO_MENU_AUTORIZACAO
	            
	            select 1
	            ).Count();	
		
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    
	    [Update()]	
	    //Update TcsModuloAutorizacao.
	    public void UpdateTcsModuloAutorizacao(TcsModuloAutorizacao entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsModuloAutorizacao.
	    public void InsertTcsModuloAutorizacao(TcsModuloAutorizacao entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsModuloAutorizacao.
	    public void DeleteTcsModuloAutorizacao(TcsModuloAutorizacao entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsModuloMenuAutorizacao.
	    public void UpdateTcsModuloMenuAutorizacao(TcsModuloMenuAutorizacao entity)
	    {



	
	        if (entity.TcsModuloAutorizacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsModuloAutorizacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsModuloAutorizacao); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsModuloMenuAutorizacao.
	    public void InsertTcsModuloMenuAutorizacao(TcsModuloMenuAutorizacao entity)
	    {



	
	        if (entity.TcsModuloAutorizacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsModuloAutorizacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsModuloAutorizacao);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsModuloMenuAutorizacao.
	    public void DeleteTcsModuloMenuAutorizacao(TcsModuloMenuAutorizacao entity)
	    {



	
	        if (entity.TcsModuloAutorizacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsModuloAutorizacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsModuloAutorizacao);
	            

	
	        }

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsTransacaoMenuAutorizacaoModulo.
	    public void UpdateTcsTransacaoMenuAutorizacaoModulo(TcsTransacaoMenuAutorizacaoModulo entity)
	    {



	
	        if (entity.TcsModuloMenuAutorizacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsModuloMenuAutorizacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsModuloMenuAutorizacao); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsTransacaoMenuAutorizacaoModulo.
	    public void InsertTcsTransacaoMenuAutorizacaoModulo(TcsTransacaoMenuAutorizacaoModulo entity)
	    {



	
	        if (entity.TcsModuloMenuAutorizacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsModuloMenuAutorizacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsModuloMenuAutorizacao);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsTransacaoMenuAutorizacaoModulo.
	    public void DeleteTcsTransacaoMenuAutorizacaoModulo(TcsTransacaoMenuAutorizacaoModulo entity)
	    {



	
	        if (entity.TcsModuloMenuAutorizacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsModuloMenuAutorizacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsModuloMenuAutorizacao);
	            

	
	        }

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}