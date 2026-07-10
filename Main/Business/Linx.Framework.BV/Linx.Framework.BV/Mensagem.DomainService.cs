					
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

namespace Linx.Framework.BV.Mensagem
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_MENSAGEM.ID_TCS_MENSAGEM", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsMensagem,TcsMensagem.TcsMensagemLogDetail];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsMensagem];ReadOnly[false];Entities[TCS_MENSAGEM:IdTcsMensagem|TCS_EMPRESA_AUTENTICACAO:IdLinx];SubQueryInfo[];EdmEntityName[TCS_MENSAGEM];EntityRelations[TCS_USUARIO_AUTENTICACAO(TCS_USUARIO_AUTENTICACAO)#TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsMensagem")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Mensagem.TcsMensagem")]
	public partial class TcsMensagem : Linx.Data.Entity
	{

	
		
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.TcsMensagemLogDetailList != null && this.TcsMensagemLogDetailList.Count() > 0)
	      {
	         foreach (var entity in this.TcsMensagemLogDetailList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.TcsMensagemLogDetailList != null)
	      {
	         foreach (var detail in this.TcsMensagemLogDetailList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsMensagemLogDetailList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(MensagemDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("TcsMensagemLogDetail"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsMensagemLogDetail");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsMensagem"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdTcsMensagem));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsMensagemLogDetail and all sub-details
	         if (this.TcsMensagemLogDetailList == null || this.TcsMensagemLogDetailList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsMensagemLogDetailList = context.GetPagedTcsMensagemLogDetail(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsMensagemLogDetailList = (from r in context.GetTcsMensagemLogDetailByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _TcsMensagemLogDetailElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsMensagemLogDetail && ((TcsMensagemLogDetail)e.Entity).TcsMensagem == null && e.Associations == null && e.OriginalAssociations == null && ((TcsMensagemLogDetail)e.Entity).IdTcsMensagem == this.IdTcsMensagem).ToList();
 	      if (_TcsMensagemLogDetailElements.Count > 0 && this.TcsMensagemLogDetailList.Count() == 0)
 	      {
 	          this.TcsMensagemLogDetailList = _TcsMensagemLogDetailElements.Select(e => (TcsMensagemLogDetail)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsMensagemLogDetailElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsMensagemLogDetail)detail.Entity).TcsMensagem = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsMensagem", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsMensagemLogDetailList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For Corpo
	    partial void OnCorpoChanging(System.String value);
	    partial void OnCorpoChanged();

	    private System.String _Corpo;

	    [DataMember(IsRequired = true, Name = "Corpo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Corpo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM.CORPO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM.CORPO")]
	    public System.String Corpo
	    {
	    	    get
	    	    {
	    	          return _Corpo;
	    	    }
	    	    set
	    	    {
	    	          if (this._Corpo != value)
	    	          {
	    	              this.ValidateProperty("Corpo", value);
	    	              this.OnCorpoChanging(value);
	    	              this.RaiseDataMemberChanging("Corpo");
	    	              this._Corpo = value;
	    	              this.RaiseDataMemberChanged("Corpo");
	    	              this.OnCorpoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Criacao
	    partial void OnCriacaoChanging(System.DateTime value);
	    partial void OnCriacaoChanged();

	    private System.DateTime _Criacao;

	    [DataMember(IsRequired = true, Name = "Criacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Criacao", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM.CRIACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM.CRIACAO")]
	    public System.DateTime Criacao
	    {
	    	    get
	    	    {
	    	          return _Criacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._Criacao != value)
	    	          {
	    	              this.ValidateProperty("Criacao", value);
	    	              this.OnCriacaoChanging(value);
	    	              this.RaiseDataMemberChanging("Criacao");
	    	              this._Criacao = value;
	    	              this.RaiseDataMemberChanged("Criacao");
	    	              this.OnCriacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Envio
	    partial void OnEnvioChanging(System.DateTime value);
	    partial void OnEnvioChanged();

	    private System.DateTime _Envio;

	    [DataMember(IsRequired = true, Name = "Envio", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Envio", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM.ENVIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM.ENVIO")]
	    public System.DateTime Envio
	    {
	    	    get
	    	    {
	    	          return _Envio;
	    	    }
	    	    set
	    	    {
	    	          if (this._Envio != value)
	    	          {
	    	              this.ValidateProperty("Envio", value);
	    	              this.OnEnvioChanging(value);
	    	              this.RaiseDataMemberChanging("Envio");
	    	              this._Envio = value;
	    	              this.RaiseDataMemberChanged("Envio");
	    	              this.OnEnvioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Filtro
	    partial void OnFiltroChanging(System.String value);
	    partial void OnFiltroChanged();

	    private System.String _Filtro;

	    [DataMember(IsRequired = true, Name = "Filtro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Filtro", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM.FILTRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM.FILTRO")]
	    public System.String Filtro
	    {
	    	    get
	    	    {
	    	          return _Filtro;
	    	    }
	    	    set
	    	    {
	    	          if (this._Filtro != value)
	    	          {
	    	              this.ValidateProperty("Filtro", value);
	    	              this.OnFiltroChanging(value);
	    	              this.RaiseDataMemberChanging("Filtro");
	    	              this._Filtro = value;
	    	              this.RaiseDataMemberChanged("Filtro");
	    	              this.OnFiltroChanged();
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
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
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
	    //Extensibility Partial Method Definitions For IdTcsMensagem
	    partial void OnIdTcsMensagemChanging(Int64 value);
	    partial void OnIdTcsMensagemChanged();

	    private Int64 _IdTcsMensagem;

	    [DataMember(IsRequired = true, Name = "IdTcsMensagem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Mensagem", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM.ID_TCS_MENSAGEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM.ID_TCS_MENSAGEM")]
	    public Int64 IdTcsMensagem
	    {
	    	    get
	    	    {
	    	          return _IdTcsMensagem;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsMensagem != value)
	    	          {
	    	              this.ValidateProperty("IdTcsMensagem", value);
	    	              this.OnIdTcsMensagemChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsMensagem");
	    	              this._IdTcsMensagem = value;
	    	              this.RaiseDataMemberChanged("IdTcsMensagem");
	    	              this.OnIdTcsMensagemChanged();
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
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM.TCS_USUARIO_AUTENTICACAO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM.TCS_USUARIO_AUTENTICACAO.ID_USUARIO")]
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
	    //Extensibility Partial Method Definitions For LxTipoMensagem
	    partial void OnLxTipoMensagemChanging(Byte value);
	    partial void OnLxTipoMensagemChanged();

	    private Byte _LxTipoMensagem;

	    [DataMember(IsRequired = true, Name = "LxTipoMensagem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Lx Tipo Mensagem", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoMensagem];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM.LX_TIPO_MENSAGEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM.LX_TIPO_MENSAGEM")]
	    public Byte LxTipoMensagem
	    {
	    	    get
	    	    {
	    	          return _LxTipoMensagem;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoMensagem != value)
	    	          {
	    	              this.ValidateProperty("LxTipoMensagem", value);
	    	              this.OnLxTipoMensagemChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoMensagem");
	    	              this._LxTipoMensagem = value;
	    	              this.RaiseDataMemberChanged("LxTipoMensagem");
	    	              this.OnLxTipoMensagemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Titulo
	    partial void OnTituloChanging(System.String value);
	    partial void OnTituloChanged();

	    private System.String _Titulo;

	    [DataMember(IsRequired = true, Name = "Titulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Titulo", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(100)]
	    [FunctionalPoint("Precision[100:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM.TITULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM.TITULO")]
	    public System.String Titulo
	    {
	    	    get
	    	    {
	    	          return _Titulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._Titulo != value)
	    	          {
	    	              this.ValidateProperty("Titulo", value);
	    	              this.OnTituloChanging(value);
	    	              this.RaiseDataMemberChanging("Titulo");
	    	              this._Titulo = value;
	    	              this.RaiseDataMemberChanged("Titulo");
	    	              this.OnTituloChanged();
	    	          }
	    	    }
	    }

	    private Int64 _TemporaryIdTcsMensagem;
	    [DataMember(Name = "TemporaryIdTcsMensagem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Mensagem (Tmp)", Description="Temporary Key", Order = 8, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdTcsMensagem
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTcsMensagem.IsNullOrEmpty())
	    	                this._TemporaryIdTcsMensagem = this._IdTcsMensagem;
	    	          return this._TemporaryIdTcsMensagem;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTcsMensagem != value)
	    	              this._TemporaryIdTcsMensagem = value;
	    	    }
	    }	

	    #endregion Data Properties

	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<TcsMensagemLogDetail> _TcsMensagemLogDetailList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsMensagem_TcsMensagemLogDetail", "IdTcsMensagem", "IdTcsMensagem", IsForeignKey=false)]
	    [DataMember(Name = "TcsMensagemLogDetailList", EmitDefaultValue = true)]
	    public IEnumerable<TcsMensagemLogDetail> TcsMensagemLogDetailList
	    {
	        get
	        {
	
	            if (this._TcsMensagemLogDetailList == null)
	            	this._TcsMensagemLogDetailList = new List<TcsMensagemLogDetail>();
	
	            return this._TcsMensagemLogDetailList;
	        }
	        set
	        {
	            if (this._TcsMensagemLogDetailList != value)
	            {
	                this._TcsMensagemLogDetailList = value;
	                this.RaisePropertyChanged("TcsMensagemLogDetailList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_MENSAGEM").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_MENSAGEM), QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM.CORPO", Source = "Corpo", Target = "CORPO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM", RelationPropertyName = "TCS_MENSAGEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM.ENVIO", Source = "Envio", Target = "ENVIO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM", RelationPropertyName = "TCS_MENSAGEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM.FILTRO", Source = "Filtro", Target = "FILTRO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM", RelationPropertyName = "TCS_MENSAGEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM.TITULO", Source = "Titulo", Target = "TITULO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM", RelationPropertyName = "TCS_MENSAGEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM.CRIACAO", Source = "Criacao", Target = "CRIACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM", RelationPropertyName = "TCS_MENSAGEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM.ID_TCS_MENSAGEM", Source = "IdTcsMensagem", Target = "ID_TCS_MENSAGEM", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM", RelationPropertyName = "TCS_MENSAGEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM.LX_TIPO_MENSAGEM", Source = "LxTipoMensagem", Target = "LX_TIPO_MENSAGEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM", RelationPropertyName = "TCS_MENSAGEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM.TCS_EMPRESA_AUTENTICACAO.ID_LINX", Source = "IdLinx", Target = "ID_LINX", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_AUTENTICACAO", RelationPropertyName = "TCS_EMPRESA_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM.TCS_USUARIO_AUTENTICACAO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxTipoMensagemValues()
	    {
	    	    return Linx.Framework.BV.Domains.TipoMensagem.GetValues();
	    }
	    private string _lxTipoMensagemName;
	    [DataMember(IsRequired = false, Name = "LxTipoMensagemName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Lx Tipo Mensagem", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxTipoMensagemName
	    {
	    	    get { if (this.LxTipoMensagem.IsNull()) { _lxTipoMensagemName = String.Empty; } else { string key = this.LxTipoMensagem.ToString(); var dmValues = this.GetLxTipoMensagemValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxTipoMensagemName) _lxTipoMensagemName = domainName; } return _lxTipoMensagemName; } set { _lxTipoMensagemName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_MENSAGEM_LOG.ID_TCS_MENSAGEM_LOG", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsMensagemLog];ReadOnly[false];Entities[TCS_MENSAGEM_LOG:IdTcsMensagemLog];SubQueryInfo[Select 1 From #ParentAlias#.TCS_MENSAGEM_LOG_LISTA as #Alias#];EdmEntityName[TCS_MENSAGEM_LOG];EntityRelations[TCS_USUARIO_AUTENTICACAO(TCS_USUARIO_AUTENTICACAO)#TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)#TCS_MENSAGEM(TCS_MENSAGEM)];EdmParentEntityName[TCS_MENSAGEM];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsMensagemLogDetail")]
	[Serializable()]
	public partial class TcsMensagemLogDetail : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(MensagemDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsMensagem");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsMensagem"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdTcsMensagem));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsMensagem
	         this.TcsMensagem = (from r in context.GetTcsMensagemByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For Dispensada
	    partial void OnDispensadaChanging(System.Nullable<System.DateTime> value);
	    partial void OnDispensadaChanged();

	    private System.Nullable<System.DateTime> _Dispensada;

	    [DataMember(Name = "Dispensada", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Dispensada", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM_LOG.DISPENSADA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM_LOG.DISPENSADA")]
	    public System.Nullable<System.DateTime> Dispensada
	    {
	    	    get
	    	    {
	    	          return _Dispensada;
	    	    }
	    	    set
	    	    {
	    	          if (this._Dispensada != value)
	    	          {
	    	              this.ValidateProperty("Dispensada", value);
	    	              this.OnDispensadaChanging(value);
	    	              this.RaiseDataMemberChanging("Dispensada");
	    	              this._Dispensada = value;
	    	              this.RaiseDataMemberChanged("Dispensada");
	    	              this.OnDispensadaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Entregue
	    partial void OnEntregueChanging(System.Nullable<System.DateTime> value);
	    partial void OnEntregueChanged();

	    private System.Nullable<System.DateTime> _Entregue;

	    [DataMember(Name = "Entregue", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Entregue", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM_LOG.ENTREGUE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM_LOG.ENTREGUE")]
	    public System.Nullable<System.DateTime> Entregue
	    {
	    	    get
	    	    {
	    	          return _Entregue;
	    	    }
	    	    set
	    	    {
	    	          if (this._Entregue != value)
	    	          {
	    	              this.ValidateProperty("Entregue", value);
	    	              this.OnEntregueChanging(value);
	    	              this.RaiseDataMemberChanging("Entregue");
	    	              this._Entregue = value;
	    	              this.RaiseDataMemberChanged("Entregue");
	    	              this.OnEntregueChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsMensagem
	    partial void OnIdTcsMensagemChanging(Int64 value);
	    partial void OnIdTcsMensagemChanged();

	    private Int64 _IdTcsMensagem;

	    [DataMember(IsRequired = true, Name = "IdTcsMensagem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Mensagem", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM_LOG.TCS_MENSAGEM.ID_TCS_MENSAGEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM_LOG.TCS_MENSAGEM.ID_TCS_MENSAGEM")]
	    public Int64 IdTcsMensagem
	    {
	    	    get
	    	    {
	    	          return _IdTcsMensagem;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsMensagem != value)
	    	          {
	    	              this.ValidateProperty("IdTcsMensagem", value);
	    	              this.OnIdTcsMensagemChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsMensagem");
	    	              this._IdTcsMensagem = value;
	    	              this.RaiseDataMemberChanged("IdTcsMensagem");
	    	              this.OnIdTcsMensagemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsMensagemLog
	    partial void OnIdTcsMensagemLogChanging(Int64 value);
	    partial void OnIdTcsMensagemLogChanged();

	    private Int64 _IdTcsMensagemLog;

	    [DataMember(IsRequired = true, Name = "IdTcsMensagemLog", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Mensagem Log", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM_LOG.ID_TCS_MENSAGEM_LOG];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM_LOG.ID_TCS_MENSAGEM_LOG")]
	    public Int64 IdTcsMensagemLog
	    {
	    	    get
	    	    {
	    	          return _IdTcsMensagemLog;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsMensagemLog != value)
	    	          {
	    	              this.ValidateProperty("IdTcsMensagemLog", value);
	    	              this.OnIdTcsMensagemLogChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsMensagemLog");
	    	              this._IdTcsMensagemLog = value;
	    	              this.RaiseDataMemberChanged("IdTcsMensagemLog");
	    	              this.OnIdTcsMensagemLogChanged();
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
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM_LOG.TCS_USUARIO_AUTENTICACAO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM_LOG.TCS_USUARIO_AUTENTICACAO.ID_USUARIO")]
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
	    //Extensibility Partial Method Definitions For Lida
	    partial void OnLidaChanging(System.Nullable<System.DateTime> value);
	    partial void OnLidaChanged();

	    private System.Nullable<System.DateTime> _Lida;

	    [DataMember(Name = "Lida", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Lida", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM_LOG.LIDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM_LOG.LIDA")]
	    public System.Nullable<System.DateTime> Lida
	    {
	    	    get
	    	    {
	    	          return _Lida;
	    	    }
	    	    set
	    	    {
	    	          if (this._Lida != value)
	    	          {
	    	              this.ValidateProperty("Lida", value);
	    	              this.OnLidaChanging(value);
	    	              this.RaiseDataMemberChanging("Lida");
	    	              this._Lida = value;
	    	              this.RaiseDataMemberChanged("Lida");
	    	              this.OnLidaChanged();
	    	          }
	    	    }
	    }

	    private Int64 _TemporaryIdTcsMensagemLog;
	    [DataMember(Name = "TemporaryIdTcsMensagemLog", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Mensagem Log (Tmp)", Description="Temporary Key", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdTcsMensagemLog
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTcsMensagemLog.IsNullOrEmpty())
	    	                this._TemporaryIdTcsMensagemLog = this._IdTcsMensagemLog;
	    	          return this._TemporaryIdTcsMensagemLog;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTcsMensagemLog != value)
	    	              this._TemporaryIdTcsMensagemLog = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsMensagem _TcsMensagem;
	    [DataMember(Name = "TcsMensagem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsMensagem_TcsMensagemLogDetail", "IdTcsMensagem", "IdTcsMensagem", IsForeignKey=true)]
	    public TcsMensagem TcsMensagem
	    {
	        get
	        {
	            return this._TcsMensagem;
	        }
	        set
	        {
	            if (this._TcsMensagem != value)
	            {
	                this._TcsMensagem = value;
	                this.RaisePropertyChanged("TcsMensagemList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_MENSAGEM_LOG").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_MENSAGEM_LOG), QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM_LOG" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM_LOG.LIDA", Source = "Lida", Target = "LIDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM_LOG", RelationPropertyName = "TCS_MENSAGEM_LOG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM_LOG.ENTREGUE", Source = "Entregue", Target = "ENTREGUE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM_LOG", RelationPropertyName = "TCS_MENSAGEM_LOG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM_LOG.DISPENSADA", Source = "Dispensada", Target = "DISPENSADA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM_LOG", RelationPropertyName = "TCS_MENSAGEM_LOG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM_LOG.ID_TCS_MENSAGEM_LOG", Source = "IdTcsMensagemLog", Target = "ID_TCS_MENSAGEM_LOG", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM_LOG", RelationPropertyName = "TCS_MENSAGEM_LOG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM_LOG.TCS_MENSAGEM.ID_TCS_MENSAGEM", Source = "IdTcsMensagem", Target = "ID_TCS_MENSAGEM", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM", RelationPropertyName = "TCS_MENSAGEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM_LOG.TCS_USUARIO_AUTENTICACAO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });

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

		

	[LinxPublicationView(PrimaryKeys="MensagemInfo.EntityUniqueKey", IsUpdatable=false, EdmName="")]
		
	[DataContract(IsReference = false, Name = "MensagemInfo")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Mensagem.MensagemInfo")]
	public partial class MensagemInfo 
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
	 


	    private Int64 _IdTcsMensagemLog;

	    [DataMember(Name = "IdTcsMensagemLog", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Mensagem Log", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public Int64 IdTcsMensagemLog
	    {
	    	    get
	    	    {
	    	          return _IdTcsMensagemLog;
	    	    }
	    	    set
	    	    {
	    	          this._IdTcsMensagemLog = value;
	    	    }
	    }

	    private System.String _Titulo;

	    [DataMember(Name = "Titulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Titulo", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public System.String Titulo
	    {
	    	    get
	    	    {
	    	          return _Titulo;
	    	    }
	    	    set
	    	    {
	    	          this._Titulo = value;
	    	    }
	    }

	    private System.String _Corpo;

	    [DataMember(Name = "Corpo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Corpo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public System.String Corpo
	    {
	    	    get
	    	    {
	    	          return _Corpo;
	    	    }
	    	    set
	    	    {
	    	          this._Corpo = value;
	    	    }
	    }

	    private bool _Lida;

	    [DataMember(Name = "Lida", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Lida", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public bool Lida
	    {
	    	    get
	    	    {
	    	          return _Lida;
	    	    }
	    	    set
	    	    {
	    	          this._Lida = value;
	    	    }
	    }

	    private System.DateTime? _Entregue;

	    [DataMember(Name = "Entregue", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Entregue", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public System.DateTime? Entregue
	    {
	    	    get
	    	    {
	    	          return _Entregue;
	    	    }
	    	    set
	    	    {
	    	          this._Entregue = value;
	    	    }
	    }

	    private System.String _TipoMensagem;

	    [DataMember(Name = "TipoMensagem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public System.String TipoMensagem
	    {
	    	    get
	    	    {
	    	          return _TipoMensagem;
	    	    }
	    	    set
	    	    {
	    	          this._TipoMensagem = value;
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

		

	[LinxPublicationView(PrimaryKeys="TCS_MENSAGEM_LOG.ID_TCS_MENSAGEM_LOG", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsMensagemUsuario];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsMensagemLog];ReadOnly[false];Entities[TCS_MENSAGEM_LOG:IdTcsMensagemLog];SubQueryInfo[];EdmEntityName[TCS_MENSAGEM_LOG];EntityRelations[TCS_USUARIO_AUTENTICACAO(TCS_USUARIO_AUTENTICACAO)#TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)#TCS_MENSAGEM(TCS_MENSAGEM)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsMensagemUsuario")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Mensagem.TcsMensagemUsuario")]
	public partial class TcsMensagemUsuario : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For Corpo
	    partial void OnCorpoChanging(System.String value);
	    partial void OnCorpoChanged();

	    private System.String _Corpo;

	    [DataMember(IsRequired = true, Name = "Corpo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Corpo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM_LOG.TCS_MENSAGEM.CORPO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM_LOG.TCS_MENSAGEM.CORPO")]
	    public System.String Corpo
	    {
	    	    get
	    	    {
	    	          return _Corpo;
	    	    }
	    	    set
	    	    {
	    	          if (this._Corpo != value)
	    	          {
	    	              this.ValidateProperty("Corpo", value);
	    	              this.OnCorpoChanging(value);
	    	              this.RaiseDataMemberChanging("Corpo");
	    	              this._Corpo = value;
	    	              this.RaiseDataMemberChanged("Corpo");
	    	              this.OnCorpoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Dispensada
	    partial void OnDispensadaChanging(System.Nullable<System.DateTime> value);
	    partial void OnDispensadaChanged();

	    private System.Nullable<System.DateTime> _Dispensada;

	    [DataMember(Name = "Dispensada", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Dispensada", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM_LOG.DISPENSADA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM_LOG.DISPENSADA")]
	    public System.Nullable<System.DateTime> Dispensada
	    {
	    	    get
	    	    {
	    	          return _Dispensada;
	    	    }
	    	    set
	    	    {
	    	          if (this._Dispensada != value)
	    	          {
	    	              this.ValidateProperty("Dispensada", value);
	    	              this.OnDispensadaChanging(value);
	    	              this.RaiseDataMemberChanging("Dispensada");
	    	              this._Dispensada = value;
	    	              this.RaiseDataMemberChanged("Dispensada");
	    	              this.OnDispensadaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Entregue
	    partial void OnEntregueChanging(System.Nullable<System.DateTime> value);
	    partial void OnEntregueChanged();

	    private System.Nullable<System.DateTime> _Entregue;

	    [DataMember(Name = "Entregue", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Entregue", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM_LOG.ENTREGUE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM_LOG.ENTREGUE")]
	    public System.Nullable<System.DateTime> Entregue
	    {
	    	    get
	    	    {
	    	          return _Entregue;
	    	    }
	    	    set
	    	    {
	    	          if (this._Entregue != value)
	    	          {
	    	              this.ValidateProperty("Entregue", value);
	    	              this.OnEntregueChanging(value);
	    	              this.RaiseDataMemberChanging("Entregue");
	    	              this._Entregue = value;
	    	              this.RaiseDataMemberChanged("Entregue");
	    	              this.OnEntregueChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Envio
	    partial void OnEnvioChanging(System.Nullable<System.DateTime> value);
	    partial void OnEnvioChanged();

	    private System.Nullable<System.DateTime> _Envio;

	    [DataMember(Name = "Envio", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Envio", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM_LOG.TCS_MENSAGEM.ENVIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM_LOG.TCS_MENSAGEM.ENVIO")]
	    public System.Nullable<System.DateTime> Envio
	    {
	    	    get
	    	    {
	    	          return _Envio;
	    	    }
	    	    set
	    	    {
	    	          if (this._Envio != value)
	    	          {
	    	              this.ValidateProperty("Envio", value);
	    	              this.OnEnvioChanging(value);
	    	              this.RaiseDataMemberChanging("Envio");
	    	              this._Envio = value;
	    	              this.RaiseDataMemberChanged("Envio");
	    	              this.OnEnvioChanged();
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
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM_LOG.TCS_MENSAGEM.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM_LOG.TCS_MENSAGEM.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
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
	    //Extensibility Partial Method Definitions For IdTcsMensagem
	    partial void OnIdTcsMensagemChanging(Int64 value);
	    partial void OnIdTcsMensagemChanged();

	    private Int64 _IdTcsMensagem;

	    [DataMember(IsRequired = true, Name = "IdTcsMensagem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Mensagem", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM_LOG.TCS_MENSAGEM.ID_TCS_MENSAGEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM_LOG.TCS_MENSAGEM.ID_TCS_MENSAGEM")]
	    public Int64 IdTcsMensagem
	    {
	    	    get
	    	    {
	    	          return _IdTcsMensagem;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsMensagem != value)
	    	          {
	    	              this.ValidateProperty("IdTcsMensagem", value);
	    	              this.OnIdTcsMensagemChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsMensagem");
	    	              this._IdTcsMensagem = value;
	    	              this.RaiseDataMemberChanged("IdTcsMensagem");
	    	              this.OnIdTcsMensagemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsMensagemLog
	    partial void OnIdTcsMensagemLogChanging(Int64 value);
	    partial void OnIdTcsMensagemLogChanged();

	    private Int64 _IdTcsMensagemLog;

	    [DataMember(IsRequired = true, Name = "IdTcsMensagemLog", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Mensagem Log", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM_LOG.ID_TCS_MENSAGEM_LOG];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM_LOG.ID_TCS_MENSAGEM_LOG")]
	    public Int64 IdTcsMensagemLog
	    {
	    	    get
	    	    {
	    	          return _IdTcsMensagemLog;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsMensagemLog != value)
	    	          {
	    	              this.ValidateProperty("IdTcsMensagemLog", value);
	    	              this.OnIdTcsMensagemLogChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsMensagemLog");
	    	              this._IdTcsMensagemLog = value;
	    	              this.RaiseDataMemberChanged("IdTcsMensagemLog");
	    	              this.OnIdTcsMensagemLogChanged();
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
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM_LOG.TCS_USUARIO_AUTENTICACAO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM_LOG.TCS_USUARIO_AUTENTICACAO.ID_USUARIO")]
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
	    //Extensibility Partial Method Definitions For Lida
	    partial void OnLidaChanging(System.Nullable<System.DateTime> value);
	    partial void OnLidaChanged();

	    private System.Nullable<System.DateTime> _Lida;

	    [DataMember(Name = "Lida", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Lida", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM_LOG.LIDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM_LOG.LIDA")]
	    public System.Nullable<System.DateTime> Lida
	    {
	    	    get
	    	    {
	    	          return _Lida;
	    	    }
	    	    set
	    	    {
	    	          if (this._Lida != value)
	    	          {
	    	              this.ValidateProperty("Lida", value);
	    	              this.OnLidaChanging(value);
	    	              this.RaiseDataMemberChanging("Lida");
	    	              this._Lida = value;
	    	              this.RaiseDataMemberChanged("Lida");
	    	              this.OnLidaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoMensagem
	    partial void OnLxTipoMensagemChanging(Byte value);
	    partial void OnLxTipoMensagemChanged();

	    private Byte _LxTipoMensagem;

	    [DataMember(IsRequired = true, Name = "LxTipoMensagem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Lx Tipo Mensagem", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoMensagem];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM_LOG.TCS_MENSAGEM.LX_TIPO_MENSAGEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM_LOG.TCS_MENSAGEM.LX_TIPO_MENSAGEM")]
	    public Byte LxTipoMensagem
	    {
	    	    get
	    	    {
	    	          return _LxTipoMensagem;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoMensagem != value)
	    	          {
	    	              this.ValidateProperty("LxTipoMensagem", value);
	    	              this.OnLxTipoMensagemChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoMensagem");
	    	              this._LxTipoMensagem = value;
	    	              this.RaiseDataMemberChanged("LxTipoMensagem");
	    	              this.OnLxTipoMensagemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Titulo
	    partial void OnTituloChanging(System.String value);
	    partial void OnTituloChanged();

	    private System.String _Titulo;

	    [DataMember(IsRequired = true, Name = "Titulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Titulo", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(100)]
	    [FunctionalPoint("Precision[100:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM_LOG.TCS_MENSAGEM.TITULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM_LOG.TCS_MENSAGEM.TITULO")]
	    public System.String Titulo
	    {
	    	    get
	    	    {
	    	          return _Titulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._Titulo != value)
	    	          {
	    	              this.ValidateProperty("Titulo", value);
	    	              this.OnTituloChanging(value);
	    	              this.RaiseDataMemberChanging("Titulo");
	    	              this._Titulo = value;
	    	              this.RaiseDataMemberChanged("Titulo");
	    	              this.OnTituloChanged();
	    	          }
	    	    }
	    }

	    private Int64 _TemporaryIdTcsMensagemLog;
	    [DataMember(Name = "TemporaryIdTcsMensagemLog", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Mensagem Log (Tmp)", Description="Temporary Key", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdTcsMensagemLog
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTcsMensagemLog.IsNullOrEmpty())
	    	                this._TemporaryIdTcsMensagemLog = this._IdTcsMensagemLog;
	    	          return this._TemporaryIdTcsMensagemLog;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTcsMensagemLog != value)
	    	              this._TemporaryIdTcsMensagemLog = value;
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_MENSAGEM_LOG").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_MENSAGEM_LOG), QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM_LOG" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM_LOG.LIDA", Source = "Lida", Target = "LIDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM_LOG", RelationPropertyName = "TCS_MENSAGEM_LOG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM_LOG.ENTREGUE", Source = "Entregue", Target = "ENTREGUE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM_LOG", RelationPropertyName = "TCS_MENSAGEM_LOG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM_LOG.DISPENSADA", Source = "Dispensada", Target = "DISPENSADA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM_LOG", RelationPropertyName = "TCS_MENSAGEM_LOG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM_LOG.ID_TCS_MENSAGEM_LOG", Source = "IdTcsMensagemLog", Target = "ID_TCS_MENSAGEM_LOG", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM_LOG", RelationPropertyName = "TCS_MENSAGEM_LOG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM_LOG.TCS_MENSAGEM.ID_TCS_MENSAGEM", Source = "IdTcsMensagem", Target = "ID_TCS_MENSAGEM", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM", RelationPropertyName = "TCS_MENSAGEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM_LOG.TCS_USUARIO_AUTENTICACAO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxTipoMensagemValues()
	    {
	    	    return Linx.Framework.BV.Domains.TipoMensagem.GetValues();
	    }
	    private string _lxTipoMensagemName;
	    [DataMember(IsRequired = false, Name = "LxTipoMensagemName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Lx Tipo Mensagem", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxTipoMensagemName
	    {
	    	    get { if (this.LxTipoMensagem.IsNull()) { _lxTipoMensagemName = String.Empty; } else { string key = this.LxTipoMensagem.ToString(); var dmValues = this.GetLxTipoMensagemValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxTipoMensagemName) _lxTipoMensagemName = domainName; } return _lxTipoMensagemName; } set { _lxTipoMensagemName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_MENSAGEM_LOG.ID_TCS_MENSAGEM_LOG", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsMensagemLog];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsMensagemLog];ReadOnly[false];Entities[TCS_MENSAGEM_LOG:IdTcsMensagemLog];SubQueryInfo[];EdmEntityName[TCS_MENSAGEM_LOG];EntityRelations[TCS_USUARIO_AUTENTICACAO(TCS_USUARIO_AUTENTICACAO)#TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)#TCS_MENSAGEM(TCS_MENSAGEM)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsMensagemLog")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Mensagem.TcsMensagemLog")]
	public partial class TcsMensagemLog : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For Dispensada
	    partial void OnDispensadaChanging(System.Nullable<System.DateTime> value);
	    partial void OnDispensadaChanged();

	    private System.Nullable<System.DateTime> _Dispensada;

	    [DataMember(Name = "Dispensada", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Dispensada", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM_LOG.DISPENSADA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM_LOG.DISPENSADA")]
	    public System.Nullable<System.DateTime> Dispensada
	    {
	    	    get
	    	    {
	    	          return _Dispensada;
	    	    }
	    	    set
	    	    {
	    	          if (this._Dispensada != value)
	    	          {
	    	              this.ValidateProperty("Dispensada", value);
	    	              this.OnDispensadaChanging(value);
	    	              this.RaiseDataMemberChanging("Dispensada");
	    	              this._Dispensada = value;
	    	              this.RaiseDataMemberChanged("Dispensada");
	    	              this.OnDispensadaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Entregue
	    partial void OnEntregueChanging(System.Nullable<System.DateTime> value);
	    partial void OnEntregueChanged();

	    private System.Nullable<System.DateTime> _Entregue;

	    [DataMember(Name = "Entregue", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Entregue", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM_LOG.ENTREGUE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM_LOG.ENTREGUE")]
	    public System.Nullable<System.DateTime> Entregue
	    {
	    	    get
	    	    {
	    	          return _Entregue;
	    	    }
	    	    set
	    	    {
	    	          if (this._Entregue != value)
	    	          {
	    	              this.ValidateProperty("Entregue", value);
	    	              this.OnEntregueChanging(value);
	    	              this.RaiseDataMemberChanging("Entregue");
	    	              this._Entregue = value;
	    	              this.RaiseDataMemberChanged("Entregue");
	    	              this.OnEntregueChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsMensagem
	    partial void OnIdTcsMensagemChanging(Int64 value);
	    partial void OnIdTcsMensagemChanged();

	    private Int64 _IdTcsMensagem;

	    [DataMember(IsRequired = true, Name = "IdTcsMensagem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Mensagem", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM_LOG.TCS_MENSAGEM.ID_TCS_MENSAGEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM_LOG.TCS_MENSAGEM.ID_TCS_MENSAGEM")]
	    public Int64 IdTcsMensagem
	    {
	    	    get
	    	    {
	    	          return _IdTcsMensagem;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsMensagem != value)
	    	          {
	    	              this.ValidateProperty("IdTcsMensagem", value);
	    	              this.OnIdTcsMensagemChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsMensagem");
	    	              this._IdTcsMensagem = value;
	    	              this.RaiseDataMemberChanged("IdTcsMensagem");
	    	              this.OnIdTcsMensagemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsMensagemLog
	    partial void OnIdTcsMensagemLogChanging(Int64 value);
	    partial void OnIdTcsMensagemLogChanged();

	    private Int64 _IdTcsMensagemLog;

	    [DataMember(IsRequired = true, Name = "IdTcsMensagemLog", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Mensagem Log", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM_LOG.ID_TCS_MENSAGEM_LOG];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM_LOG.ID_TCS_MENSAGEM_LOG")]
	    public Int64 IdTcsMensagemLog
	    {
	    	    get
	    	    {
	    	          return _IdTcsMensagemLog;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsMensagemLog != value)
	    	          {
	    	              this.ValidateProperty("IdTcsMensagemLog", value);
	    	              this.OnIdTcsMensagemLogChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsMensagemLog");
	    	              this._IdTcsMensagemLog = value;
	    	              this.RaiseDataMemberChanged("IdTcsMensagemLog");
	    	              this.OnIdTcsMensagemLogChanged();
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
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM_LOG.TCS_USUARIO_AUTENTICACAO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM_LOG.TCS_USUARIO_AUTENTICACAO.ID_USUARIO")]
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
	    //Extensibility Partial Method Definitions For Lida
	    partial void OnLidaChanging(System.Nullable<System.DateTime> value);
	    partial void OnLidaChanged();

	    private System.Nullable<System.DateTime> _Lida;

	    [DataMember(Name = "Lida", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Lida", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM_LOG.LIDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM_LOG.LIDA")]
	    public System.Nullable<System.DateTime> Lida
	    {
	    	    get
	    	    {
	    	          return _Lida;
	    	    }
	    	    set
	    	    {
	    	          if (this._Lida != value)
	    	          {
	    	              this.ValidateProperty("Lida", value);
	    	              this.OnLidaChanging(value);
	    	              this.RaiseDataMemberChanging("Lida");
	    	              this._Lida = value;
	    	              this.RaiseDataMemberChanged("Lida");
	    	              this.OnLidaChanged();
	    	          }
	    	    }
	    }

	    private Int64 _TemporaryIdTcsMensagemLog;
	    [DataMember(Name = "TemporaryIdTcsMensagemLog", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Mensagem Log (Tmp)", Description="Temporary Key", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdTcsMensagemLog
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTcsMensagemLog.IsNullOrEmpty())
	    	                this._TemporaryIdTcsMensagemLog = this._IdTcsMensagemLog;
	    	          return this._TemporaryIdTcsMensagemLog;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTcsMensagemLog != value)
	    	              this._TemporaryIdTcsMensagemLog = value;
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_MENSAGEM_LOG").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_MENSAGEM_LOG), QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM_LOG" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM_LOG.LIDA", Source = "Lida", Target = "LIDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM_LOG", RelationPropertyName = "TCS_MENSAGEM_LOG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM_LOG.ENTREGUE", Source = "Entregue", Target = "ENTREGUE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM_LOG", RelationPropertyName = "TCS_MENSAGEM_LOG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM_LOG.DISPENSADA", Source = "Dispensada", Target = "DISPENSADA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM_LOG", RelationPropertyName = "TCS_MENSAGEM_LOG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM_LOG.ID_TCS_MENSAGEM_LOG", Source = "IdTcsMensagemLog", Target = "ID_TCS_MENSAGEM_LOG", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM_LOG", RelationPropertyName = "TCS_MENSAGEM_LOG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM_LOG.TCS_MENSAGEM.ID_TCS_MENSAGEM", Source = "IdTcsMensagem", Target = "ID_TCS_MENSAGEM", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM", RelationPropertyName = "TCS_MENSAGEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM_LOG.TCS_USUARIO_AUTENTICACAO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });

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

		

	[LinxPublicationView(PrimaryKeys="TcsPerfil.EntityUniqueKey", IsUpdatable=false, EdmName="")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsPerfil];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsUsuarioPerfil];ReadOnly[false];Entities[TcsUsuarioPerfil:IdTcsUsuarioPerfil];SubQueryInfo[];EdmEntityName[TcsUsuarioPerfil];EntityRelations[];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsPerfil")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Mensagem.TcsPerfil")]
	public partial class TcsPerfil : Linx.Data.Entity
	{

	    #region Business View Query
	    public static IQueryable<TcsPerfil> GetBusinessView(MensagemDomainService context, string predicate, params System.Data.Entity.Core.Objects.ObjectParameter[] parameters)
	    {
	    		
	    		List<object> paramValues = new List<object>();
	    		predicate = predicate.ToDynamicLinqExpression(typeof(TcsPerfil), parameters, paramValues);
	
    			Linx.Framework.BV.UsuarioFranquia.UsuarioFranquiaDomainService usuarioFranquiaContext = new Linx.Framework.BV.UsuarioFranquia.UsuarioFranquiaDomainService();
    			Linx.Framework.BV.Perfil.PerfilDomainService perfilContext = new Linx.Framework.BV.Perfil.PerfilDomainService(usuarioFranquiaContext.GetEDM(), null);
    			
    			var query = (from entity0 in usuarioFranquiaContext.GetTcsUsuarioPerfilNoAssociations()
    			join entity1 in perfilContext.GetTcsPerfilNoAssociations() on entity0.IdPerfil equals entity1.IdPerfil
    			select new TcsPerfil()
    			{
    			    IdTcsUsuarioPerfil = entity0.IdTcsUsuarioPerfil,
    			    IdUsuario = entity0.IdUsuario,
    			    DescPerfil = entity1.DescPerfil,
    			    IdPerfil = entity1.IdPerfil
       })
       ;


	    		return query.AsQueryable().Where(predicate, paramValues.ToArray());
	    }
	    #endregion
	
		
	

	
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
	 

	    //Extensibility Partial Method Definitions For IdTcsUsuarioPerfil
	    partial void OnIdTcsUsuarioPerfilChanging(Int64 value);
	    partial void OnIdTcsUsuarioPerfilChanged();

	    private Int64 _IdTcsUsuarioPerfil;

	    [DataMember(IsRequired = true, Name = "IdTcsUsuarioPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Perfil", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TcsUsuarioPerfil.IdTcsUsuarioPerfil];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TcsUsuarioPerfil.IdTcsUsuarioPerfil")]
	    public Int64 IdTcsUsuarioPerfil
	    {
	    	    get
	    	    {
	    	          return _IdTcsUsuarioPerfil;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsUsuarioPerfil != value)
	    	          {
	    	              this.ValidateProperty("IdTcsUsuarioPerfil", value);
	    	              this.OnIdTcsUsuarioPerfilChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsUsuarioPerfil");
	    	              this._IdTcsUsuarioPerfil = value;
	    	              this.RaiseDataMemberChanged("IdTcsUsuarioPerfil");
	    	              this.OnIdTcsUsuarioPerfilChanged();
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
	    [Display(Name = "Id Usuario", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TcsUsuarioPerfil.IdUsuario];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TcsUsuarioPerfil.IdUsuario")]
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
	    //Extensibility Partial Method Definitions For DescPerfil
	    partial void OnDescPerfilChanging(String value);
	    partial void OnDescPerfilChanged();

	    private String _DescPerfil;

	    [DataMember(IsRequired = true, Name = "DescPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TcsUsuarioPerfil.DescPerfil];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TcsUsuarioPerfil.DescPerfil")]
	    public String DescPerfil
	    {
	    	    get
	    	    {
	    	          return _DescPerfil;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescPerfil != value)
	    	          {
	    	              this.ValidateProperty("DescPerfil", value);
	    	              this.OnDescPerfilChanging(value);
	    	              this.RaiseDataMemberChanging("DescPerfil");
	    	              this._DescPerfil = value;
	    	              this.RaiseDataMemberChanged("DescPerfil");
	    	              this.OnDescPerfilChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdPerfil
	    partial void OnIdPerfilChanging(Int64 value);
	    partial void OnIdPerfilChanged();

	    private Int64 _IdPerfil;

	    [DataMember(IsRequired = true, Name = "IdPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TcsUsuarioPerfil.IdPerfil];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TcsUsuarioPerfil.IdPerfil")]
	    public Int64 IdPerfil
	    {
	    	    get
	    	    {
	    	          return _IdPerfil;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdPerfil != value)
	    	          {
	    	              this.ValidateProperty("IdPerfil", value);
	    	              this.OnIdPerfilChanging(value);
	    	              this.RaiseDataMemberChanging("IdPerfil");
	    	              this._IdPerfil = value;
	    	              this.RaiseDataMemberChanged("IdPerfil");
	    	              this.OnIdPerfilChanged();
	    	          }
	    	    }
	    }

	    private Int64 _TemporaryIdTcsUsuarioPerfil;
	    [DataMember(Name = "TemporaryIdTcsUsuarioPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Perfil (Tmp)", Description="Temporary Key", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdTcsUsuarioPerfil
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTcsUsuarioPerfil.IsNullOrEmpty())
	    	                this._TemporaryIdTcsUsuarioPerfil = this._IdTcsUsuarioPerfil;
	    	          return this._TemporaryIdTcsUsuarioPerfil;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTcsUsuarioPerfil != value)
	    	              this._TemporaryIdTcsUsuarioPerfil = value;
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

		

	[LinxPublicationView(PrimaryKeys="TCS_USUARIO_AUTENTICACAO.ID_USUARIO", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsUsuario];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdUsuario];ReadOnly[false];Entities[TCS_USUARIO_AUTENTICACAO:IdUsuario|TCS_EMPRESA_AUTENTICACAO:IdLinx];SubQueryInfo[];EdmEntityName[TCS_USUARIO_AUTENTICACAO];EntityRelations[TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuario")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Mensagem.TcsUsuario")]
	public partial class TcsUsuario : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For AutenticacaoWindows
	    partial void OnAutenticacaoWindowsChanging(Boolean value);
	    partial void OnAutenticacaoWindowsChanged();

	    private Boolean _AutenticacaoWindows;

	    [DataMember(IsRequired = true, Name = "AutenticacaoWindows", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Autenticacao Windows", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.AUTENTICACAO_WINDOWS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.AUTENTICACAO_WINDOWS")]
	    public Boolean AutenticacaoWindows
	    {
	    	    get
	    	    {
	    	          return _AutenticacaoWindows;
	    	    }
	    	    set
	    	    {
	    	          if (this._AutenticacaoWindows != value)
	    	          {
	    	              this.ValidateProperty("AutenticacaoWindows", value);
	    	              this.OnAutenticacaoWindowsChanging(value);
	    	              this.RaiseDataMemberChanging("AutenticacaoWindows");
	    	              this._AutenticacaoWindows = value;
	    	              this.RaiseDataMemberChanged("AutenticacaoWindows");
	    	              this.OnAutenticacaoWindowsChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Bairro
	    partial void OnBairroChanging(System.String value);
	    partial void OnBairroChanged();

	    private System.String _Bairro;

	    [DataMember(Name = "Bairro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bairro", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.BAIRRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.BAIRRO")]
	    public System.String Bairro
	    {
	    	    get
	    	    {
	    	          return _Bairro;
	    	    }
	    	    set
	    	    {
	    	          if (this._Bairro != value)
	    	          {
	    	              this.ValidateProperty("Bairro", value);
	    	              this.OnBairroChanging(value);
	    	              this.RaiseDataMemberChanging("Bairro");
	    	              this._Bairro = value;
	    	              this.RaiseDataMemberChanged("Bairro");
	    	              this.OnBairroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Cep
	    partial void OnCepChanging(System.String value);
	    partial void OnCepChanged();

	    private System.String _Cep;

	    [DataMember(Name = "Cep", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cep", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.CEP];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.CEP")]
	    public System.String Cep
	    {
	    	    get
	    	    {
	    	          return _Cep;
	    	    }
	    	    set
	    	    {
	    	          if (this._Cep != value)
	    	          {
	    	              this.ValidateProperty("Cep", value);
	    	              this.OnCepChanging(value);
	    	              this.RaiseDataMemberChanging("Cep");
	    	              this._Cep = value;
	    	              this.RaiseDataMemberChanged("Cep");
	    	              this.OnCepChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For CnpjCpf
	    partial void OnCnpjCpfChanging(System.String value);
	    partial void OnCnpjCpfChanged();

	    private System.String _CnpjCpf;

	    [DataMember(Name = "CnpjCpf", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cnpj Cpf", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.CNPJ_CPF];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.CNPJ_CPF")]
	    public System.String CnpjCpf
	    {
	    	    get
	    	    {
	    	          return _CnpjCpf;
	    	    }
	    	    set
	    	    {
	    	          if (this._CnpjCpf != value)
	    	          {
	    	              this.ValidateProperty("CnpjCpf", value);
	    	              this.OnCnpjCpfChanging(value);
	    	              this.RaiseDataMemberChanging("CnpjCpf");
	    	              this._CnpjCpf = value;
	    	              this.RaiseDataMemberChanged("CnpjCpf");
	    	              this.OnCnpjCpfChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Complemento
	    partial void OnComplementoChanging(System.String value);
	    partial void OnComplementoChanged();

	    private System.String _Complemento;

	    [DataMember(Name = "Complemento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Complemento", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.COMPLEMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.COMPLEMENTO")]
	    public System.String Complemento
	    {
	    	    get
	    	    {
	    	          return _Complemento;
	    	    }
	    	    set
	    	    {
	    	          if (this._Complemento != value)
	    	          {
	    	              this.ValidateProperty("Complemento", value);
	    	              this.OnComplementoChanging(value);
	    	              this.RaiseDataMemberChanging("Complemento");
	    	              this._Complemento = value;
	    	              this.RaiseDataMemberChanged("Complemento");
	    	              this.OnComplementoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DataAlteracao
	    partial void OnDataAlteracaoChanging(System.Nullable<System.DateTime> value);
	    partial void OnDataAlteracaoChanged();

	    private System.Nullable<System.DateTime> _DataAlteracao;

	    [DataMember(Name = "DataAlteracao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Data Alteracao", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.DATA_ALTERACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.DATA_ALTERACAO")]
	    public System.Nullable<System.DateTime> DataAlteracao
	    {
	    	    get
	    	    {
	    	          return _DataAlteracao;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataAlteracao != value)
	    	          {
	    	              this.ValidateProperty("DataAlteracao", value);
	    	              this.OnDataAlteracaoChanging(value);
	    	              this.RaiseDataMemberChanging("DataAlteracao");
	    	              this._DataAlteracao = value;
	    	              this.RaiseDataMemberChanged("DataAlteracao");
	    	              this.OnDataAlteracaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DataCadastro
	    partial void OnDataCadastroChanging(System.Nullable<System.DateTime> value);
	    partial void OnDataCadastroChanged();

	    private System.Nullable<System.DateTime> _DataCadastro;

	    [DataMember(Name = "DataCadastro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Data Cadastro", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.DATA_CADASTRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.DATA_CADASTRO")]
	    public System.Nullable<System.DateTime> DataCadastro
	    {
	    	    get
	    	    {
	    	          return _DataCadastro;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataCadastro != value)
	    	          {
	    	              this.ValidateProperty("DataCadastro", value);
	    	              this.OnDataCadastroChanging(value);
	    	              this.RaiseDataMemberChanging("DataCadastro");
	    	              this._DataCadastro = value;
	    	              this.RaiseDataMemberChanged("DataCadastro");
	    	              this.OnDataCadastroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DataExpiracaoSenha
	    partial void OnDataExpiracaoSenhaChanging(System.DateTime value);
	    partial void OnDataExpiracaoSenhaChanged();

	    private System.DateTime _DataExpiracaoSenha;

	    [DataMember(IsRequired = true, Name = "DataExpiracaoSenha", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Data Expiracao Senha", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.DATA_EXPIRACAO_SENHA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.DATA_EXPIRACAO_SENHA")]
	    public System.DateTime DataExpiracaoSenha
	    {
	    	    get
	    	    {
	    	          return _DataExpiracaoSenha;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataExpiracaoSenha != value)
	    	          {
	    	              this.ValidateProperty("DataExpiracaoSenha", value);
	    	              this.OnDataExpiracaoSenhaChanging(value);
	    	              this.RaiseDataMemberChanging("DataExpiracaoSenha");
	    	              this._DataExpiracaoSenha = value;
	    	              this.RaiseDataMemberChanged("DataExpiracaoSenha");
	    	              this.OnDataExpiracaoSenhaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Email
	    partial void OnEmailChanging(System.String value);
	    partial void OnEmailChanged();

	    private System.String _Email;

	    [DataMember(IsRequired = true, Name = "Email", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Email", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.EMAIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.EMAIL")]
	    public System.String Email
	    {
	    	    get
	    	    {
	    	          return _Email;
	    	    }
	    	    set
	    	    {
	    	          if (this._Email != value)
	    	          {
	    	              this.ValidateProperty("Email", value);
	    	              this.OnEmailChanging(value);
	    	              this.RaiseDataMemberChanging("Email");
	    	              this._Email = value;
	    	              this.RaiseDataMemberChanged("Email");
	    	              this.OnEmailChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For FoneCelular
	    partial void OnFoneCelularChanging(System.String value);
	    partial void OnFoneCelularChanged();

	    private System.String _FoneCelular;

	    [DataMember(Name = "FoneCelular", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Fone Celular", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.FONE_CELULAR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.FONE_CELULAR")]
	    public System.String FoneCelular
	    {
	    	    get
	    	    {
	    	          return _FoneCelular;
	    	    }
	    	    set
	    	    {
	    	          if (this._FoneCelular != value)
	    	          {
	    	              this.ValidateProperty("FoneCelular", value);
	    	              this.OnFoneCelularChanging(value);
	    	              this.RaiseDataMemberChanging("FoneCelular");
	    	              this._FoneCelular = value;
	    	              this.RaiseDataMemberChanged("FoneCelular");
	    	              this.OnFoneCelularChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For FoneFixo
	    partial void OnFoneFixoChanging(System.String value);
	    partial void OnFoneFixoChanged();

	    private System.String _FoneFixo;

	    [DataMember(Name = "FoneFixo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Fone Fixo", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.FONE_FIXO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.FONE_FIXO")]
	    public System.String FoneFixo
	    {
	    	    get
	    	    {
	    	          return _FoneFixo;
	    	    }
	    	    set
	    	    {
	    	          if (this._FoneFixo != value)
	    	          {
	    	              this.ValidateProperty("FoneFixo", value);
	    	              this.OnFoneFixoChanging(value);
	    	              this.RaiseDataMemberChanging("FoneFixo");
	    	              this._FoneFixo = value;
	    	              this.RaiseDataMemberChanged("FoneFixo");
	    	              this.OnFoneFixoChanged();
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
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
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
	    //Extensibility Partial Method Definitions For IdUsuario
	    partial void OnIdUsuarioChanging(Int64 value);
	    partial void OnIdUsuarioChanged();

	    private Int64 _IdUsuario;

	    [DataMember(IsRequired = true, Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.ID_USUARIO")]
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
	    //Extensibility Partial Method Definitions For Inativo
	    partial void OnInativoChanging(Boolean value);
	    partial void OnInativoChanged();

	    private Boolean _Inativo;

	    [DataMember(IsRequired = true, Name = "Inativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inativo", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.INATIVO")]
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
	    //Extensibility Partial Method Definitions For IndicaAcessoSuporte
	    partial void OnIndicaAcessoSuporteChanging(Boolean value);
	    partial void OnIndicaAcessoSuporteChanged();

	    private Boolean _IndicaAcessoSuporte;

	    [DataMember(IsRequired = true, Name = "IndicaAcessoSuporte", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Indica Acesso Suporte", Description="", Order = 14, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.INDICA_ACESSO_SUPORTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.INDICA_ACESSO_SUPORTE")]
	    public Boolean IndicaAcessoSuporte
	    {
	    	    get
	    	    {
	    	          return _IndicaAcessoSuporte;
	    	    }
	    	    set
	    	    {
	    	          if (this._IndicaAcessoSuporte != value)
	    	          {
	    	              this.ValidateProperty("IndicaAcessoSuporte", value);
	    	              this.OnIndicaAcessoSuporteChanging(value);
	    	              this.RaiseDataMemberChanging("IndicaAcessoSuporte");
	    	              this._IndicaAcessoSuporte = value;
	    	              this.RaiseDataMemberChanged("IndicaAcessoSuporte");
	    	              this.OnIndicaAcessoSuporteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For InscrEstadualRg
	    partial void OnInscrEstadualRgChanging(System.String value);
	    partial void OnInscrEstadualRgChanged();

	    private System.String _InscrEstadualRg;

	    [DataMember(Name = "InscrEstadualRg", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inscr Estadual Rg", Description="", Order = 15, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.INSCR_ESTADUAL_RG];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.INSCR_ESTADUAL_RG")]
	    public System.String InscrEstadualRg
	    {
	    	    get
	    	    {
	    	          return _InscrEstadualRg;
	    	    }
	    	    set
	    	    {
	    	          if (this._InscrEstadualRg != value)
	    	          {
	    	              this.ValidateProperty("InscrEstadualRg", value);
	    	              this.OnInscrEstadualRgChanging(value);
	    	              this.RaiseDataMemberChanging("InscrEstadualRg");
	    	              this._InscrEstadualRg = value;
	    	              this.RaiseDataMemberChanged("InscrEstadualRg");
	    	              this.OnInscrEstadualRgChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Logradouro
	    partial void OnLogradouroChanging(System.String value);
	    partial void OnLogradouroChanged();

	    private System.String _Logradouro;

	    [DataMember(Name = "Logradouro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Logradouro", Description="", Order = 16, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.LOGRADOURO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.LOGRADOURO")]
	    public System.String Logradouro
	    {
	    	    get
	    	    {
	    	          return _Logradouro;
	    	    }
	    	    set
	    	    {
	    	          if (this._Logradouro != value)
	    	          {
	    	              this.ValidateProperty("Logradouro", value);
	    	              this.OnLogradouroChanging(value);
	    	              this.RaiseDataMemberChanging("Logradouro");
	    	              this._Logradouro = value;
	    	              this.RaiseDataMemberChanged("Logradouro");
	    	              this.OnLogradouroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxPfjFisicaJuridica
	    partial void OnLxPfjFisicaJuridicaChanging(System.Nullable<System.Byte> value);
	    partial void OnLxPfjFisicaJuridicaChanged();

	    private System.Nullable<System.Byte> _LxPfjFisicaJuridica;

	    [DataMember(Name = "LxPfjFisicaJuridica", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Lx Pfj Fisica Juridica", Description="", Order = 17, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[LX_PFJ_FISICA_JURIDICA];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.LX_PFJ_FISICA_JURIDICA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.LX_PFJ_FISICA_JURIDICA")]
	    public System.Nullable<System.Byte> LxPfjFisicaJuridica
	    {
	    	    get
	    	    {
	    	          return _LxPfjFisicaJuridica;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxPfjFisicaJuridica != value)
	    	          {
	    	              this.ValidateProperty("LxPfjFisicaJuridica", value);
	    	              this.OnLxPfjFisicaJuridicaChanging(value);
	    	              this.RaiseDataMemberChanging("LxPfjFisicaJuridica");
	    	              this._LxPfjFisicaJuridica = value;
	    	              this.RaiseDataMemberChanged("LxPfjFisicaJuridica");
	    	              this.OnLxPfjFisicaJuridicaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoLogradouro
	    partial void OnLxTipoLogradouroChanging(System.Nullable<System.Byte> value);
	    partial void OnLxTipoLogradouroChanged();

	    private System.Nullable<System.Byte> _LxTipoLogradouro;

	    [DataMember(Name = "LxTipoLogradouro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Lx Tipo Logradouro", Description="", Order = 18, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[LxTipoLogradouro];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO")]
	    public System.Nullable<System.Byte> LxTipoLogradouro
	    {
	    	    get
	    	    {
	    	          return _LxTipoLogradouro;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoLogradouro != value)
	    	          {
	    	              this.ValidateProperty("LxTipoLogradouro", value);
	    	              this.OnLxTipoLogradouroChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoLogradouro");
	    	              this._LxTipoLogradouro = value;
	    	              this.RaiseDataMemberChanged("LxTipoLogradouro");
	    	              this.OnLxTipoLogradouroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Municipio
	    partial void OnMunicipioChanging(System.String value);
	    partial void OnMunicipioChanged();

	    private System.String _Municipio;

	    [DataMember(Name = "Municipio", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Municipio", Description="", Order = 19, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.MUNICIPIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.MUNICIPIO")]
	    public System.String Municipio
	    {
	    	    get
	    	    {
	    	          return _Municipio;
	    	    }
	    	    set
	    	    {
	    	          if (this._Municipio != value)
	    	          {
	    	              this.ValidateProperty("Municipio", value);
	    	              this.OnMunicipioChanging(value);
	    	              this.RaiseDataMemberChanging("Municipio");
	    	              this._Municipio = value;
	    	              this.RaiseDataMemberChanged("Municipio");
	    	              this.OnMunicipioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeAutenticacao
	    partial void OnNomeAutenticacaoChanging(System.String value);
	    partial void OnNomeAutenticacaoChanged();

	    private System.String _NomeAutenticacao;

	    [DataMember(IsRequired = true, Name = "NomeAutenticacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Autenticacao", Description="", Order = 20, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO")]
	    public System.String NomeAutenticacao
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
	    //Extensibility Partial Method Definitions For NomeCurtoUsuario
	    partial void OnNomeCurtoUsuarioChanging(System.String value);
	    partial void OnNomeCurtoUsuarioChanged();

	    private System.String _NomeCurtoUsuario;

	    [DataMember(IsRequired = true, Name = "NomeCurtoUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Curto Usuario", Description="", Order = 21, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.NOME_CURTO_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.NOME_CURTO_USUARIO")]
	    public System.String NomeCurtoUsuario
	    {
	    	    get
	    	    {
	    	          return _NomeCurtoUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeCurtoUsuario != value)
	    	          {
	    	              this.ValidateProperty("NomeCurtoUsuario", value);
	    	              this.OnNomeCurtoUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("NomeCurtoUsuario");
	    	              this._NomeCurtoUsuario = value;
	    	              this.RaiseDataMemberChanged("NomeCurtoUsuario");
	    	              this.OnNomeCurtoUsuarioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeUsuario
	    partial void OnNomeUsuarioChanging(System.String value);
	    partial void OnNomeUsuarioChanged();

	    private System.String _NomeUsuario;

	    [DataMember(IsRequired = true, Name = "NomeUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Usuario", Description="", Order = 22, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.NOME_USUARIO")]
	    public System.String NomeUsuario
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
	    //Extensibility Partial Method Definitions For Numero
	    partial void OnNumeroChanging(System.String value);
	    partial void OnNumeroChanged();

	    private System.String _Numero;

	    [DataMember(Name = "Numero", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Numero", Description="", Order = 23, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.NUMERO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.NUMERO")]
	    public System.String Numero
	    {
	    	    get
	    	    {
	    	          return _Numero;
	    	    }
	    	    set
	    	    {
	    	          if (this._Numero != value)
	    	          {
	    	              this.ValidateProperty("Numero", value);
	    	              this.OnNumeroChanging(value);
	    	              this.RaiseDataMemberChanging("Numero");
	    	              this._Numero = value;
	    	              this.RaiseDataMemberChanged("Numero");
	    	              this.OnNumeroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ObsEndereco
	    partial void OnObsEnderecoChanging(System.String value);
	    partial void OnObsEnderecoChanged();

	    private System.String _ObsEndereco;

	    [DataMember(Name = "ObsEndereco", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Obs Endereco", Description="", Order = 24, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.OBS_ENDERECO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.OBS_ENDERECO")]
	    public System.String ObsEndereco
	    {
	    	    get
	    	    {
	    	          return _ObsEndereco;
	    	    }
	    	    set
	    	    {
	    	          if (this._ObsEndereco != value)
	    	          {
	    	              this.ValidateProperty("ObsEndereco", value);
	    	              this.OnObsEnderecoChanging(value);
	    	              this.RaiseDataMemberChanging("ObsEndereco");
	    	              this._ObsEndereco = value;
	    	              this.RaiseDataMemberChanged("ObsEndereco");
	    	              this.OnObsEnderecoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Ramal
	    partial void OnRamalChanging(System.String value);
	    partial void OnRamalChanged();

	    private System.String _Ramal;

	    [DataMember(Name = "Ramal", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ramal", Description="", Order = 25, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(6)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.RAMAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.RAMAL")]
	    public System.String Ramal
	    {
	    	    get
	    	    {
	    	          return _Ramal;
	    	    }
	    	    set
	    	    {
	    	          if (this._Ramal != value)
	    	          {
	    	              this.ValidateProperty("Ramal", value);
	    	              this.OnRamalChanging(value);
	    	              this.RaiseDataMemberChanging("Ramal");
	    	              this._Ramal = value;
	    	              this.RaiseDataMemberChanged("Ramal");
	    	              this.OnRamalChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Uf
	    partial void OnUfChanging(System.String value);
	    partial void OnUfChanged();

	    private System.String _Uf;

	    [DataMember(Name = "Uf", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uf", Description="", Order = 26, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(4)]
	    [FunctionalPoint("Precision[4:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.UF];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.UF")]
	    public System.String Uf
	    {
	    	    get
	    	    {
	    	          return _Uf;
	    	    }
	    	    set
	    	    {
	    	          if (this._Uf != value)
	    	          {
	    	              this.ValidateProperty("Uf", value);
	    	              this.OnUfChanging(value);
	    	              this.RaiseDataMemberChanging("Uf");
	    	              this._Uf = value;
	    	              this.RaiseDataMemberChanged("Uf");
	    	              this.OnUfChanged();
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
	    [Display(Name = "Uid Usuario", Description="", Order = 27, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.UID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.UID_USUARIO")]
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
	    //Extensibility Partial Method Definitions For VigenciaFinal
	    partial void OnVigenciaFinalChanging(System.DateTime value);
	    partial void OnVigenciaFinalChanged();

	    private System.DateTime _VigenciaFinal;

	    [DataMember(IsRequired = true, Name = "VigenciaFinal", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Vigencia Final", Description="", Order = 28, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.VIGENCIA_FINAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.VIGENCIA_FINAL")]
	    public System.DateTime VigenciaFinal
	    {
	    	    get
	    	    {
	    	          return _VigenciaFinal;
	    	    }
	    	    set
	    	    {
	    	          if (this._VigenciaFinal != value)
	    	          {
	    	              this.ValidateProperty("VigenciaFinal", value);
	    	              this.OnVigenciaFinalChanging(value);
	    	              this.RaiseDataMemberChanging("VigenciaFinal");
	    	              this._VigenciaFinal = value;
	    	              this.RaiseDataMemberChanged("VigenciaFinal");
	    	              this.OnVigenciaFinalChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For VigenciaInicial
	    partial void OnVigenciaInicialChanging(System.DateTime value);
	    partial void OnVigenciaInicialChanged();

	    private System.DateTime _VigenciaInicial;

	    [DataMember(IsRequired = true, Name = "VigenciaInicial", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Vigencia Inicial", Description="", Order = 29, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.VIGENCIA_INICIAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.VIGENCIA_INICIAL")]
	    public System.DateTime VigenciaInicial
	    {
	    	    get
	    	    {
	    	          return _VigenciaInicial;
	    	    }
	    	    set
	    	    {
	    	          if (this._VigenciaInicial != value)
	    	          {
	    	              this.ValidateProperty("VigenciaInicial", value);
	    	              this.OnVigenciaInicialChanging(value);
	    	              this.RaiseDataMemberChanging("VigenciaInicial");
	    	              this._VigenciaInicial = value;
	    	              this.RaiseDataMemberChanged("VigenciaInicial");
	    	              this.OnVigenciaInicialChanged();
	    	          }
	    	    }
	    }

	    private Int64 _TemporaryIdUsuario;
	    [DataMember(Name = "TemporaryIdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario (Tmp)", Description="Temporary Key", Order = 12, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdUsuario
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdUsuario.IsNullOrEmpty())
	    	                this._TemporaryIdUsuario = this._IdUsuario;
	    	          return this._TemporaryIdUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdUsuario != value)
	    	              this._TemporaryIdUsuario = value;
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_USUARIO_AUTENTICACAO), QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.UF", Source = "Uf", Target = "UF", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.CEP", Source = "Cep", Target = "CEP", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.EMAIL", Source = "Email", Target = "EMAIL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.RAMAL", Source = "Ramal", Target = "RAMAL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.BAIRRO", Source = "Bairro", Target = "BAIRRO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.NUMERO", Source = "Numero", Target = "NUMERO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.INATIVO", Source = "Inativo", Target = "INATIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.CNPJ_CPF", Source = "CnpjCpf", Target = "CNPJ_CPF", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.FONE_FIXO", Source = "FoneFixo", Target = "FONE_FIXO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.MUNICIPIO", Source = "Municipio", Target = "MUNICIPIO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.LOGRADOURO", Source = "Logradouro", Target = "LOGRADOURO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.COMPLEMENTO", Source = "Complemento", Target = "COMPLEMENTO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.UID_USUARIO", Source = "UidUsuario", Target = "UID_USUARIO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.FONE_CELULAR", Source = "FoneCelular", Target = "FONE_CELULAR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.NOME_USUARIO", Source = "NomeUsuario", Target = "NOME_USUARIO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.OBS_ENDERECO", Source = "ObsEndereco", Target = "OBS_ENDERECO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.DATA_CADASTRO", Source = "DataCadastro", Target = "DATA_CADASTRO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.DATA_ALTERACAO", Source = "DataAlteracao", Target = "DATA_ALTERACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.VIGENCIA_FINAL", Source = "VigenciaFinal", Target = "VIGENCIA_FINAL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.VIGENCIA_INICIAL", Source = "VigenciaInicial", Target = "VIGENCIA_INICIAL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.INSCR_ESTADUAL_RG", Source = "InscrEstadualRg", Target = "INSCR_ESTADUAL_RG", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO", Source = "NomeAutenticacao", Target = "NOME_AUTENTICACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO", Source = "LxTipoLogradouro", Target = "LX_TIPO_LOGRADOURO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.NOME_CURTO_USUARIO", Source = "NomeCurtoUsuario", Target = "NOME_CURTO_USUARIO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.AUTENTICACAO_WINDOWS", Source = "AutenticacaoWindows", Target = "AUTENTICACAO_WINDOWS", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.DATA_EXPIRACAO_SENHA", Source = "DataExpiracaoSenha", Target = "DATA_EXPIRACAO_SENHA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.INDICA_ACESSO_SUPORTE", Source = "IndicaAcessoSuporte", Target = "INDICA_ACESSO_SUPORTE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.LX_PFJ_FISICA_JURIDICA", Source = "LxPfjFisicaJuridica", Target = "LX_PFJ_FISICA_JURIDICA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.ID_LINX", Source = "IdLinx", Target = "ID_LINX", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_AUTENTICACAO", RelationPropertyName = "TCS_EMPRESA_AUTENTICACAO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxPfjFisicaJuridicaValues()
	    {
	    	    return Linx.Framework.BV.Domains.LX_PFJ_FISICA_JURIDICA.GetValues();
	    }
	    private string _lxPfjFisicaJuridicaName;
	    [DataMember(IsRequired = false, Name = "LxPfjFisicaJuridicaName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Lx Pfj Fisica Juridica", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxPfjFisicaJuridicaName
	    {
	    	    get { if (this.LxPfjFisicaJuridica.IsNull()) { _lxPfjFisicaJuridicaName = String.Empty; } else { string key = this.LxPfjFisicaJuridica.ToString(); var dmValues = this.GetLxPfjFisicaJuridicaValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxPfjFisicaJuridicaName) _lxPfjFisicaJuridicaName = domainName; } return _lxPfjFisicaJuridicaName; } set { _lxPfjFisicaJuridicaName = value;  }
	    }
	    public Dictionary<string, string> GetLxTipoLogradouroValues()
	    {
	    	    return Linx.Framework.BV.Domains.LxTipoLogradouro.GetValues();
	    }
	    private string _lxTipoLogradouroName;
	    [DataMember(IsRequired = false, Name = "LxTipoLogradouroName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Lx Tipo Logradouro", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxTipoLogradouroName
	    {
	    	    get { if (this.LxTipoLogradouro.IsNull()) { _lxTipoLogradouroName = String.Empty; } else { string key = this.LxTipoLogradouro.ToString(); var dmValues = this.GetLxTipoLogradouroValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxTipoLogradouroName) _lxTipoLogradouroName = domainName; } return _lxTipoLogradouroName; } set { _lxTipoLogradouroName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="NewMessageInfo.EntityUniqueKey", IsUpdatable=false, EdmName="")]
		
	[DataContract(IsReference = false, Name = "NewMessageInfo")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Mensagem.NewMessageInfo")]
	public partial class NewMessageInfo 
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
	 


	    private System.String _Titulo;

	    [DataMember(Name = "Titulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Titulo", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public System.String Titulo
	    {
	    	    get
	    	    {
	    	          return _Titulo;
	    	    }
	    	    set
	    	    {
	    	          this._Titulo = value;
	    	    }
	    }

	    private System.String _Corpo;

	    [DataMember(Name = "Corpo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Corpo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public System.String Corpo
	    {
	    	    get
	    	    {
	    	          return _Corpo;
	    	    }
	    	    set
	    	    {
	    	          this._Corpo = value;
	    	    }
	    }

	    private System.String _Filtro;

	    [DataMember(Name = "Filtro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Filtro", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public System.String Filtro
	    {
	    	    get
	    	    {
	    	          return _Filtro;
	    	    }
	    	    set
	    	    {
	    	          this._Filtro = value;
	    	    }
	    }

	    private System.Nullable<System.DateTime> _DataEnvio;

	    [DataMember(Name = "DataEnvio", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Envio", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public System.Nullable<System.DateTime> DataEnvio
	    {
	    	    get
	    	    {
	    	          return _DataEnvio;
	    	    }
	    	    set
	    	    {
	    	          this._DataEnvio = value;
	    	    }
	    }

	    private Int32 _IdLinx;

	    [DataMember(Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public Int32 IdLinx
	    {
	    	    get
	    	    {
	    	          return _IdLinx;
	    	    }
	    	    set
	    	    {
	    	          this._IdLinx = value;
	    	    }
	    }

	    private byte _LxTipoMensagem;

	    [DataMember(Name = "LxTipoMensagem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public byte LxTipoMensagem
	    {
	    	    get
	    	    {
	    	          return _LxTipoMensagem;
	    	    }
	    	    set
	    	    {
	    	          this._LxTipoMensagem = value;
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

		

	[LinxPublicationView(PrimaryKeys="TCS_MENSAGEM.ID_TCS_MENSAGEM", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsMensagemConsulta,TcsMensagemConsulta.TcsMensagemConsultaLog];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsMensagem];ReadOnly[true];Entities[TCS_MENSAGEM:IdTcsMensagem|TCS_EMPRESA_AUTENTICACAO:IdLinx];SubQueryInfo[];EdmEntityName[TCS_MENSAGEM];EntityRelations[TCS_USUARIO_AUTENTICACAO(TCS_USUARIO_AUTENTICACAO)#TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsMensagemConsulta")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Mensagem.TcsMensagemConsulta")]
	public partial class TcsMensagemConsulta : Linx.Data.Entity
	{

	
		
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.TcsMensagemConsultaLogList != null && this.TcsMensagemConsultaLogList.Count() > 0)
	      {
	         foreach (var entity in this.TcsMensagemConsultaLogList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.TcsMensagemConsultaLogList != null)
	      {
	         foreach (var detail in this.TcsMensagemConsultaLogList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsMensagemConsultaLogList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(MensagemDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("TcsMensagemConsultaLog"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsMensagemConsultaLog");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsMensagem"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdTcsMensagem));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsMensagemConsultaLog and all sub-details
	         if (this.TcsMensagemConsultaLogList == null || this.TcsMensagemConsultaLogList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsMensagemConsultaLogList = context.GetPagedTcsMensagemConsultaLog(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsMensagemConsultaLogList = (from r in context.GetTcsMensagemConsultaLogByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _TcsMensagemConsultaLogElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsMensagemConsultaLog && ((TcsMensagemConsultaLog)e.Entity).TcsMensagemConsulta == null && e.Associations == null && e.OriginalAssociations == null && ((TcsMensagemConsultaLog)e.Entity).IdTcsMensagem == this.IdTcsMensagem).ToList();
 	      if (_TcsMensagemConsultaLogElements.Count > 0 && this.TcsMensagemConsultaLogList.Count() == 0)
 	      {
 	          this.TcsMensagemConsultaLogList = _TcsMensagemConsultaLogElements.Select(e => (TcsMensagemConsultaLog)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsMensagemConsultaLogElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsMensagemConsultaLog)detail.Entity).TcsMensagemConsulta = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsMensagemConsulta", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsMensagemConsultaLogList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For Corpo
	    partial void OnCorpoChanging(System.String value);
	    partial void OnCorpoChanged();

	    private System.String _Corpo;

	    [DataMember(IsRequired = true, Name = "Corpo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Corpo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM.CORPO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM.CORPO")]
	    public System.String Corpo
	    {
	    	    get
	    	    {
	    	          return _Corpo;
	    	    }
	    	    set
	    	    {
	    	          if (this._Corpo != value)
	    	          {
	    	              this.ValidateProperty("Corpo", value);
	    	              this.OnCorpoChanging(value);
	    	              this.RaiseDataMemberChanging("Corpo");
	    	              this._Corpo = value;
	    	              this.RaiseDataMemberChanged("Corpo");
	    	              this.OnCorpoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Criacao
	    partial void OnCriacaoChanging(System.DateTime value);
	    partial void OnCriacaoChanged();

	    private System.DateTime _Criacao;

	    [DataMember(IsRequired = true, Name = "Criacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Criação", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[g];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM.CRIACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM.CRIACAO")]
	    public System.DateTime Criacao
	    {
	    	    get
	    	    {
	    	          return _Criacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._Criacao != value)
	    	          {
	    	              this.ValidateProperty("Criacao", value);
	    	              this.OnCriacaoChanging(value);
	    	              this.RaiseDataMemberChanging("Criacao");
	    	              this._Criacao = value;
	    	              this.RaiseDataMemberChanged("Criacao");
	    	              this.OnCriacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Envio
	    partial void OnEnvioChanging(System.DateTime value);
	    partial void OnEnvioChanged();

	    private System.DateTime _Envio;

	    [DataMember(IsRequired = true, Name = "Envio", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Envio", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[g];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM.ENVIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM.ENVIO")]
	    public System.DateTime Envio
	    {
	    	    get
	    	    {
	    	          return _Envio;
	    	    }
	    	    set
	    	    {
	    	          if (this._Envio != value)
	    	          {
	    	              this.ValidateProperty("Envio", value);
	    	              this.OnEnvioChanging(value);
	    	              this.RaiseDataMemberChanging("Envio");
	    	              this._Envio = value;
	    	              this.RaiseDataMemberChanged("Envio");
	    	              this.OnEnvioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Filtro
	    partial void OnFiltroChanging(System.String value);
	    partial void OnFiltroChanged();

	    private System.String _Filtro;

	    [DataMember(IsRequired = true, Name = "Filtro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Filtro", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM.FILTRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM.FILTRO")]
	    public System.String Filtro
	    {
	    	    get
	    	    {
	    	          return _Filtro;
	    	    }
	    	    set
	    	    {
	    	          if (this._Filtro != value)
	    	          {
	    	              this.ValidateProperty("Filtro", value);
	    	              this.OnFiltroChanging(value);
	    	              this.RaiseDataMemberChanging("Filtro");
	    	              this._Filtro = value;
	    	              this.RaiseDataMemberChanged("Filtro");
	    	              this.OnFiltroChanged();
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
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsEmpresaAutenticacaoC];LookUpTitle[Seleção de (Id Linx)];LookUpQuery[executeLookUpTcsEmpresaAutenticacaoC];LookUpFinalize[finalizeLookUpTcsEmpresaAutenticacaoC];LookUpDisplayColumns[{\"IdLinx\" : \"Id Linx\", \"NomeEmpresa\" : \"Empresa\"}];LookUpColumns[{\"IdLinx\" : true, \"NomeEmpresa\" : true}];FilterDataKey[TCS_MENSAGEM.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdLinx#true##12:0##Id Linx#0#true##::LookUpTcsEmpresaAutenticacaoC##false#false#TCS_EMPRESA_AUTENTICACAO#TCS_EMPRESA_AUTENTICACAO#Linx.Framework.BV.Mensagem#IQueryable###true#false", EdmKey="TCS_MENSAGEM.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
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
	    //Extensibility Partial Method Definitions For IdTcsMensagem
	    partial void OnIdTcsMensagemChanging(Int64 value);
	    partial void OnIdTcsMensagemChanged();

	    private Int64 _IdTcsMensagem;

	    [DataMember(IsRequired = true, Name = "IdTcsMensagem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Mensagem", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM.ID_TCS_MENSAGEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM.ID_TCS_MENSAGEM")]
	    public Int64 IdTcsMensagem
	    {
	    	    get
	    	    {
	    	          return _IdTcsMensagem;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsMensagem != value)
	    	          {
	    	              this.ValidateProperty("IdTcsMensagem", value);
	    	              this.OnIdTcsMensagemChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsMensagem");
	    	              this._IdTcsMensagem = value;
	    	              this.RaiseDataMemberChanged("IdTcsMensagem");
	    	              this.OnIdTcsMensagemChanged();
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
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacaoC];LookUpTitle[Seleção de (Id Usuario)];LookUpQuery[executeLookUpTcsUsuarioAutenticacaoC];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacaoC];LookUpDisplayColumns[{\"IdUsuario\" : \"Id Usuario\", \"NomeUsuario\" : \"Nome\", \"NomeAutenticacao\" : \"Usuário Autenticação\"}];LookUpColumns[{\"IdUsuario\" : false, \"NomeUsuario\" : true, \"NomeAutenticacao\" : true}];FilterDataKey[TCS_MENSAGEM.TCS_USUARIO_AUTENTICACAO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdUsuario#true##24:0##Id Usuario#0#false##::LookUpTcsUsuarioAutenticacaoC##false#false#TCS_USUARIO_AUTENTICACAO#TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.Mensagem#IQueryable###true#false", EdmKey="TCS_MENSAGEM.TCS_USUARIO_AUTENTICACAO.ID_USUARIO")]
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
	    //Extensibility Partial Method Definitions For LxTipoMensagem
	    partial void OnLxTipoMensagemChanging(Byte value);
	    partial void OnLxTipoMensagemChanged();

	    private Byte _LxTipoMensagem;

	    [DataMember(IsRequired = true, Name = "LxTipoMensagem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo Mensagem", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoMensagem];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM.LX_TIPO_MENSAGEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM.LX_TIPO_MENSAGEM")]
	    public Byte LxTipoMensagem
	    {
	    	    get
	    	    {
	    	          return _LxTipoMensagem;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoMensagem != value)
	    	          {
	    	              this.ValidateProperty("LxTipoMensagem", value);
	    	              this.OnLxTipoMensagemChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoMensagem");
	    	              this._LxTipoMensagem = value;
	    	              this.RaiseDataMemberChanged("LxTipoMensagem");
	    	              this.OnLxTipoMensagemChanged();
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
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsEmpresaAutenticacaoC];LookUpTitle[Seleção de (Empresa)];LookUpQuery[executeLookUpTcsEmpresaAutenticacaoC];LookUpFinalize[finalizeLookUpTcsEmpresaAutenticacaoC];LookUpDisplayColumns[{\"IdLinx\" : \"Id Linx\", \"NomeEmpresa\" : \"Empresa\"}];LookUpColumns[{\"IdLinx\" : true, \"NomeEmpresa\" : true}];FilterDataKey[TCS_MENSAGEM.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeEmpresa#false##250:0##Empresa#1#true##::LookUpTcsEmpresaAutenticacaoC##false#false#TCS_EMPRESA_AUTENTICACAO#TCS_EMPRESA_AUTENTICACAO#Linx.Framework.BV.Mensagem#IQueryable###true#false", EdmKey="TCS_MENSAGEM.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
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
	    //Extensibility Partial Method Definitions For Titulo
	    partial void OnTituloChanging(System.String value);
	    partial void OnTituloChanged();

	    private System.String _Titulo;

	    [DataMember(IsRequired = true, Name = "Titulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Título", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(100)]
	    [FunctionalPoint("Precision[100:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM.TITULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM.TITULO")]
	    public System.String Titulo
	    {
	    	    get
	    	    {
	    	          return _Titulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._Titulo != value)
	    	          {
	    	              this.ValidateProperty("Titulo", value);
	    	              this.OnTituloChanging(value);
	    	              this.RaiseDataMemberChanging("Titulo");
	    	              this._Titulo = value;
	    	              this.RaiseDataMemberChanged("Titulo");
	    	              this.OnTituloChanged();
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
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacaoC];LookUpTitle[Seleção de (Usuário Autenticação)];LookUpQuery[executeLookUpTcsUsuarioAutenticacaoC];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacaoC];LookUpDisplayColumns[{\"IdUsuario\" : \"Id Usuario\", \"NomeUsuario\" : \"Nome\", \"NomeAutenticacao\" : \"Usuário Autenticação\"}];LookUpColumns[{\"IdUsuario\" : false, \"NomeUsuario\" : true, \"NomeAutenticacao\" : true}];FilterDataKey[TCS_MENSAGEM.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="String#NomeAutenticacao#false##250:0##Usuário Autenticação#2#true##::LookUpTcsUsuarioAutenticacaoC##false#false#TCS_USUARIO_AUTENTICACAO#TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.Mensagem#IQueryable###true#false", EdmKey="TCS_MENSAGEM.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO")]
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
	    [Display(Name = "Emitente", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacaoC];LookUpTitle[Seleção de (Emitente)];LookUpQuery[executeLookUpTcsUsuarioAutenticacaoC];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacaoC];LookUpDisplayColumns[{\"IdUsuario\" : \"Id Usuario\", \"NomeUsuario\" : \"Nome\", \"NomeAutenticacao\" : \"Usuário Autenticação\"}];LookUpColumns[{\"IdUsuario\" : false, \"NomeUsuario\" : true, \"NomeAutenticacao\" : true}];FilterDataKey[TCS_MENSAGEM.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="String#NomeUsuario#false##250:0##Nome#1#true##::LookUpTcsUsuarioAutenticacaoC##false#false#TCS_USUARIO_AUTENTICACAO#TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.Mensagem#IQueryable###true#false", EdmKey="TCS_MENSAGEM.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO")]
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

	    private Int64 _TemporaryIdTcsMensagem;
	    [DataMember(Name = "TemporaryIdTcsMensagem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Mensagem (Tmp)", Description="Temporary Key", Order = 5, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdTcsMensagem
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTcsMensagem.IsNullOrEmpty())
	    	                this._TemporaryIdTcsMensagem = this._IdTcsMensagem;
	    	          return this._TemporaryIdTcsMensagem;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTcsMensagem != value)
	    	              this._TemporaryIdTcsMensagem = value;
	    	    }
	    }	

	    #endregion Data Properties

	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<TcsMensagemConsultaLog> _TcsMensagemConsultaLogList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsMensagemConsulta_TcsMensagemConsultaLog", "IdTcsMensagem", "IdTcsMensagem", IsForeignKey=false)]
	    [DataMember(Name = "TcsMensagemConsultaLogList", EmitDefaultValue = true)]
	    public IEnumerable<TcsMensagemConsultaLog> TcsMensagemConsultaLogList
	    {
	        get
	        {
	
	            if (this._TcsMensagemConsultaLogList == null)
	            	this._TcsMensagemConsultaLogList = new List<TcsMensagemConsultaLog>();
	
	            return this._TcsMensagemConsultaLogList;
	        }
	        set
	        {
	            if (this._TcsMensagemConsultaLogList != value)
	            {
	                this._TcsMensagemConsultaLogList = value;
	                this.RaisePropertyChanged("TcsMensagemConsultaLogList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_MENSAGEM").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_MENSAGEM), QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM.CORPO", Source = "Corpo", Target = "CORPO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM", RelationPropertyName = "TCS_MENSAGEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM.ENVIO", Source = "Envio", Target = "ENVIO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM", RelationPropertyName = "TCS_MENSAGEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM.FILTRO", Source = "Filtro", Target = "FILTRO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM", RelationPropertyName = "TCS_MENSAGEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM.TITULO", Source = "Titulo", Target = "TITULO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM", RelationPropertyName = "TCS_MENSAGEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM.CRIACAO", Source = "Criacao", Target = "CRIACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM", RelationPropertyName = "TCS_MENSAGEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM.ID_TCS_MENSAGEM", Source = "IdTcsMensagem", Target = "ID_TCS_MENSAGEM", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM", RelationPropertyName = "TCS_MENSAGEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM.LX_TIPO_MENSAGEM", Source = "LxTipoMensagem", Target = "LX_TIPO_MENSAGEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM", RelationPropertyName = "TCS_MENSAGEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM.TCS_EMPRESA_AUTENTICACAO.ID_LINX", Source = "IdLinx", Target = "ID_LINX", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_AUTENTICACAO", RelationPropertyName = "TCS_EMPRESA_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM.TCS_USUARIO_AUTENTICACAO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxTipoMensagemValues()
	    {
	    	    return Linx.Framework.BV.Domains.TipoMensagem.GetValues();
	    }
	    private string _lxTipoMensagemName;
	    [DataMember(IsRequired = false, Name = "LxTipoMensagemName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo Mensagem", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxTipoMensagemName
	    {
	    	    get { if (this.LxTipoMensagem.IsNull()) { _lxTipoMensagemName = String.Empty; } else { string key = this.LxTipoMensagem.ToString(); var dmValues = this.GetLxTipoMensagemValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxTipoMensagemName) _lxTipoMensagemName = domainName; } return _lxTipoMensagemName; } set { _lxTipoMensagemName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_MENSAGEM_LOG.ID_TCS_MENSAGEM_LOG", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[TcsMensagemConsultaLog];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsMensagemLog];ReadOnly[true];Entities[TCS_MENSAGEM_LOG:IdTcsMensagemLog];SubQueryInfo[Select 1 From #ParentAlias#.TCS_MENSAGEM_LOG_LISTA as #Alias#];EdmEntityName[TCS_MENSAGEM_LOG];EntityRelations[TCS_USUARIO_AUTENTICACAO(TCS_USUARIO_AUTENTICACAO)#TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)#TCS_MENSAGEM(TCS_MENSAGEM)];EdmParentEntityName[TCS_MENSAGEM];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsMensagemConsultaLog")]
	[Serializable()]
	public partial class TcsMensagemConsultaLog : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(MensagemDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsMensagemConsulta");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsMensagem"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdTcsMensagem));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsMensagemConsulta
	         this.TcsMensagemConsulta = (from r in context.GetTcsMensagemConsultaByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For Dispensada
	    partial void OnDispensadaChanging(System.Nullable<System.DateTime> value);
	    partial void OnDispensadaChanged();

	    private System.Nullable<System.DateTime> _Dispensada;

	    [DataMember(Name = "Dispensada", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Dispensada", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[g];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM_LOG.DISPENSADA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM_LOG.DISPENSADA")]
	    public System.Nullable<System.DateTime> Dispensada
	    {
	    	    get
	    	    {
	    	          return _Dispensada;
	    	    }
	    	    set
	    	    {
	    	          if (this._Dispensada != value)
	    	          {
	    	              this.ValidateProperty("Dispensada", value);
	    	              this.OnDispensadaChanging(value);
	    	              this.RaiseDataMemberChanging("Dispensada");
	    	              this._Dispensada = value;
	    	              this.RaiseDataMemberChanged("Dispensada");
	    	              this.OnDispensadaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Entregue
	    partial void OnEntregueChanging(System.Nullable<System.DateTime> value);
	    partial void OnEntregueChanged();

	    private System.Nullable<System.DateTime> _Entregue;

	    [DataMember(Name = "Entregue", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Entregue", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[g];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM_LOG.ENTREGUE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM_LOG.ENTREGUE")]
	    public System.Nullable<System.DateTime> Entregue
	    {
	    	    get
	    	    {
	    	          return _Entregue;
	    	    }
	    	    set
	    	    {
	    	          if (this._Entregue != value)
	    	          {
	    	              this.ValidateProperty("Entregue", value);
	    	              this.OnEntregueChanging(value);
	    	              this.RaiseDataMemberChanging("Entregue");
	    	              this._Entregue = value;
	    	              this.RaiseDataMemberChanged("Entregue");
	    	              this.OnEntregueChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsMensagem
	    partial void OnIdTcsMensagemChanging(Int64 value);
	    partial void OnIdTcsMensagemChanged();

	    private Int64 _IdTcsMensagem;

	    [DataMember(IsRequired = true, Name = "IdTcsMensagem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Mensagem", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM_LOG.TCS_MENSAGEM.ID_TCS_MENSAGEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM_LOG.TCS_MENSAGEM.ID_TCS_MENSAGEM")]
	    public Int64 IdTcsMensagem
	    {
	    	    get
	    	    {
	    	          return _IdTcsMensagem;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsMensagem != value)
	    	          {
	    	              this.ValidateProperty("IdTcsMensagem", value);
	    	              this.OnIdTcsMensagemChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsMensagem");
	    	              this._IdTcsMensagem = value;
	    	              this.RaiseDataMemberChanged("IdTcsMensagem");
	    	              this.OnIdTcsMensagemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsMensagemLog
	    partial void OnIdTcsMensagemLogChanging(Int64 value);
	    partial void OnIdTcsMensagemLogChanged();

	    private Int64 _IdTcsMensagemLog;

	    [DataMember(IsRequired = true, Name = "IdTcsMensagemLog", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Mensagem Log", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM_LOG.ID_TCS_MENSAGEM_LOG];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM_LOG.ID_TCS_MENSAGEM_LOG")]
	    public Int64 IdTcsMensagemLog
	    {
	    	    get
	    	    {
	    	          return _IdTcsMensagemLog;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsMensagemLog != value)
	    	          {
	    	              this.ValidateProperty("IdTcsMensagemLog", value);
	    	              this.OnIdTcsMensagemLogChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsMensagemLog");
	    	              this._IdTcsMensagemLog = value;
	    	              this.RaiseDataMemberChanged("IdTcsMensagemLog");
	    	              this.OnIdTcsMensagemLogChanged();
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
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacaoCL];LookUpTitle[Seleção de (Id Usuario)];LookUpQuery[executeLookUpTcsUsuarioAutenticacaoCL];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacaoCL];LookUpDisplayColumns[{\"IdUsuario\" : \"Id\", \"NomeUsuario\" : \"Nome\", \"NomeAutenticacao\" : \"Usuário Autenticação\"}];LookUpColumns[{\"IdUsuario\" : false, \"NomeUsuario\" : true, \"NomeAutenticacao\" : true}];FilterDataKey[TCS_MENSAGEM_LOG.TCS_USUARIO_AUTENTICACAO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdUsuario#true##24:0##Id#0#false##::LookUpTcsUsuarioAutenticacaoCL##false#false#TCS_USUARIO_AUTENTICACAO#TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.Mensagem#IQueryable###true#false", EdmKey="TCS_MENSAGEM_LOG.TCS_USUARIO_AUTENTICACAO.ID_USUARIO")]
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
	    //Extensibility Partial Method Definitions For Lida
	    partial void OnLidaChanging(System.Nullable<System.DateTime> value);
	    partial void OnLidaChanged();

	    private System.Nullable<System.DateTime> _Lida;

	    [DataMember(Name = "Lida", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Lida", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[g];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM_LOG.LIDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM_LOG.LIDA")]
	    public System.Nullable<System.DateTime> Lida
	    {
	    	    get
	    	    {
	    	          return _Lida;
	    	    }
	    	    set
	    	    {
	    	          if (this._Lida != value)
	    	          {
	    	              this.ValidateProperty("Lida", value);
	    	              this.OnLidaChanging(value);
	    	              this.RaiseDataMemberChanging("Lida");
	    	              this._Lida = value;
	    	              this.RaiseDataMemberChanged("Lida");
	    	              this.OnLidaChanged();
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
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacaoCL];LookUpTitle[Seleção de (Usuário Autenticação)];LookUpQuery[executeLookUpTcsUsuarioAutenticacaoCL];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacaoCL];LookUpDisplayColumns[{\"IdUsuario\" : \"Id\", \"NomeUsuario\" : \"Nome\", \"NomeAutenticacao\" : \"Usuário Autenticação\"}];LookUpColumns[{\"IdUsuario\" : false, \"NomeUsuario\" : true, \"NomeAutenticacao\" : true}];FilterDataKey[TCS_MENSAGEM_LOG.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="String#NomeAutenticacao#false##250:0##Usuário Autenticação#2#true##::LookUpTcsUsuarioAutenticacaoCL##false#false#TCS_USUARIO_AUTENTICACAO#TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.Mensagem#IQueryable###true#false", EdmKey="TCS_MENSAGEM_LOG.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO")]
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
	    [Display(Name = "Emitente", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacaoCL];LookUpTitle[Seleção de (Emitente)];LookUpQuery[executeLookUpTcsUsuarioAutenticacaoCL];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacaoCL];LookUpDisplayColumns[{\"IdUsuario\" : \"Id\", \"NomeUsuario\" : \"Nome\", \"NomeAutenticacao\" : \"Usuário Autenticação\"}];LookUpColumns[{\"IdUsuario\" : false, \"NomeUsuario\" : true, \"NomeAutenticacao\" : true}];FilterDataKey[TCS_MENSAGEM_LOG.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="String#NomeUsuario#false##250:0##Nome#1#true##::LookUpTcsUsuarioAutenticacaoCL##false#false#TCS_USUARIO_AUTENTICACAO#TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.Mensagem#IQueryable###true#false", EdmKey="TCS_MENSAGEM_LOG.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO")]
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

	    private Int64 _TemporaryIdTcsMensagemLog;
	    [DataMember(Name = "TemporaryIdTcsMensagemLog", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Mensagem Log (Tmp)", Description="Temporary Key", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdTcsMensagemLog
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTcsMensagemLog.IsNullOrEmpty())
	    	                this._TemporaryIdTcsMensagemLog = this._IdTcsMensagemLog;
	    	          return this._TemporaryIdTcsMensagemLog;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTcsMensagemLog != value)
	    	              this._TemporaryIdTcsMensagemLog = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsMensagemConsulta _TcsMensagemConsulta;
	    [DataMember(Name = "TcsMensagemConsulta", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsMensagemConsulta_TcsMensagemConsultaLog", "IdTcsMensagem", "IdTcsMensagem", IsForeignKey=true)]
	    public TcsMensagemConsulta TcsMensagemConsulta
	    {
	        get
	        {
	            return this._TcsMensagemConsulta;
	        }
	        set
	        {
	            if (this._TcsMensagemConsulta != value)
	            {
	                this._TcsMensagemConsulta = value;
	                this.RaisePropertyChanged("TcsMensagemConsultaList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_MENSAGEM_LOG").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_MENSAGEM_LOG), QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM_LOG" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM_LOG.LIDA", Source = "Lida", Target = "LIDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM_LOG", RelationPropertyName = "TCS_MENSAGEM_LOG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM_LOG.ENTREGUE", Source = "Entregue", Target = "ENTREGUE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM_LOG", RelationPropertyName = "TCS_MENSAGEM_LOG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM_LOG.DISPENSADA", Source = "Dispensada", Target = "DISPENSADA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM_LOG", RelationPropertyName = "TCS_MENSAGEM_LOG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM_LOG.ID_TCS_MENSAGEM_LOG", Source = "IdTcsMensagemLog", Target = "ID_TCS_MENSAGEM_LOG", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM_LOG", RelationPropertyName = "TCS_MENSAGEM_LOG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM_LOG.TCS_MENSAGEM.ID_TCS_MENSAGEM", Source = "IdTcsMensagem", Target = "ID_TCS_MENSAGEM", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM", RelationPropertyName = "TCS_MENSAGEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM_LOG.TCS_USUARIO_AUTENTICACAO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });

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

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsMensagemLog];ReadOnly[false];Entities[TCS_MENSAGEM_LOG:IdTcsMensagemLog];SubQueryInfo[Select 1 From #ParentAlias#.TCS_MENSAGEM_LOG_LISTA as #Alias#];EdmEntityName[TCS_MENSAGEM_LOG];EntityRelations[TCS_USUARIO_AUTENTICACAO(TCS_USUARIO_AUTENTICACAO)#TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)#TCS_MENSAGEM(TCS_MENSAGEM)];EdmParentEntityName[TCS_MENSAGEM];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsMensagemLogDetail")]
	[Serializable()]
	public partial class TcsMensagemLogDetailParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For Dispensada
	    partial void OnDispensadaChanging(System.Nullable<System.DateTime> value);
	    partial void OnDispensadaChanged();

	    private System.Nullable<System.DateTime> _Dispensada;

	    [DataMember(Name = "Dispensada", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Dispensada", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM_LOG.DISPENSADA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM_LOG.DISPENSADA")]
	    public System.Nullable<System.DateTime> Dispensada
	    {
	    	    get
	    	    {
	    	          return _Dispensada;
	    	    }
	    	    set
	    	    {
	    	          if (this._Dispensada != value)
	    	          {
	    	              this.ValidateProperty("Dispensada", value);
	    	              this.OnDispensadaChanging(value);
	    	              this.RaiseDataMemberChanging("Dispensada");
	    	              this._Dispensada = value;
	    	              this.RaiseDataMemberChanged("Dispensada");
	    	              this.OnDispensadaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Entregue
	    partial void OnEntregueChanging(System.Nullable<System.DateTime> value);
	    partial void OnEntregueChanged();

	    private System.Nullable<System.DateTime> _Entregue;

	    [DataMember(Name = "Entregue", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Entregue", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM_LOG.ENTREGUE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM_LOG.ENTREGUE")]
	    public System.Nullable<System.DateTime> Entregue
	    {
	    	    get
	    	    {
	    	          return _Entregue;
	    	    }
	    	    set
	    	    {
	    	          if (this._Entregue != value)
	    	          {
	    	              this.ValidateProperty("Entregue", value);
	    	              this.OnEntregueChanging(value);
	    	              this.RaiseDataMemberChanging("Entregue");
	    	              this._Entregue = value;
	    	              this.RaiseDataMemberChanged("Entregue");
	    	              this.OnEntregueChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsMensagem
	    partial void OnIdTcsMensagemChanging(Int64 value);
	    partial void OnIdTcsMensagemChanged();

	    private Int64 _IdTcsMensagem;

	    [DataMember(IsRequired = true, Name = "IdTcsMensagem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Mensagem", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM_LOG.TCS_MENSAGEM.ID_TCS_MENSAGEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM_LOG.TCS_MENSAGEM.ID_TCS_MENSAGEM")]
	    public Int64 IdTcsMensagem
	    {
	    	    get
	    	    {
	    	          return _IdTcsMensagem;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsMensagem != value)
	    	          {
	    	              this.ValidateProperty("IdTcsMensagem", value);
	    	              this.OnIdTcsMensagemChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsMensagem");
	    	              this._IdTcsMensagem = value;
	    	              this.RaiseDataMemberChanged("IdTcsMensagem");
	    	              this.OnIdTcsMensagemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsMensagemLog
	    partial void OnIdTcsMensagemLogChanging(Int64 value);
	    partial void OnIdTcsMensagemLogChanged();

	    private Int64 _IdTcsMensagemLog;

	    [DataMember(IsRequired = true, Name = "IdTcsMensagemLog", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Mensagem Log", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM_LOG.ID_TCS_MENSAGEM_LOG];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM_LOG.ID_TCS_MENSAGEM_LOG")]
	    public Int64 IdTcsMensagemLog
	    {
	    	    get
	    	    {
	    	          return _IdTcsMensagemLog;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsMensagemLog != value)
	    	          {
	    	              this.ValidateProperty("IdTcsMensagemLog", value);
	    	              this.OnIdTcsMensagemLogChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsMensagemLog");
	    	              this._IdTcsMensagemLog = value;
	    	              this.RaiseDataMemberChanged("IdTcsMensagemLog");
	    	              this.OnIdTcsMensagemLogChanged();
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
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM_LOG.TCS_USUARIO_AUTENTICACAO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM_LOG.TCS_USUARIO_AUTENTICACAO.ID_USUARIO")]
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
	    //Extensibility Partial Method Definitions For Lida
	    partial void OnLidaChanging(System.Nullable<System.DateTime> value);
	    partial void OnLidaChanged();

	    private System.Nullable<System.DateTime> _Lida;

	    [DataMember(Name = "Lida", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Lida", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM_LOG.LIDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM_LOG.LIDA")]
	    public System.Nullable<System.DateTime> Lida
	    {
	    	    get
	    	    {
	    	          return _Lida;
	    	    }
	    	    set
	    	    {
	    	          if (this._Lida != value)
	    	          {
	    	              this.ValidateProperty("Lida", value);
	    	              this.OnLidaChanging(value);
	    	              this.RaiseDataMemberChanging("Lida");
	    	              this._Lida = value;
	    	              this.RaiseDataMemberChanged("Lida");
	    	              this.OnLidaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Corpo
	    partial void OnCorpoChanging(System.String value);
	    partial void OnCorpoChanged();

	    private System.String _Corpo;

	    [DataMember(IsRequired = true, Name = "Corpo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Corpo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_MENSAGEM_LOG.TCS_MENSAGEM.CORPO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM.CORPO")]
	    public System.String Corpo
	    {
	    	    get
	    	    {
	    	          return _Corpo;
	    	    }
	    	    set
	    	    {
	    	          if (this._Corpo != value)
	    	          {
	    	              this.ValidateProperty("Corpo", value);
	    	              this.OnCorpoChanging(value);
	    	              this.RaiseDataMemberChanging("Corpo");
	    	              this._Corpo = value;
	    	              this.RaiseDataMemberChanged("Corpo");
	    	              this.OnCorpoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Criacao
	    partial void OnCriacaoChanging(System.DateTime value);
	    partial void OnCriacaoChanged();

	    private System.DateTime _Criacao;

	    [DataMember(IsRequired = true, Name = "Criacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Criacao", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_MENSAGEM_LOG.TCS_MENSAGEM.CRIACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM.CRIACAO")]
	    public System.DateTime Criacao
	    {
	    	    get
	    	    {
	    	          return _Criacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._Criacao != value)
	    	          {
	    	              this.ValidateProperty("Criacao", value);
	    	              this.OnCriacaoChanging(value);
	    	              this.RaiseDataMemberChanging("Criacao");
	    	              this._Criacao = value;
	    	              this.RaiseDataMemberChanged("Criacao");
	    	              this.OnCriacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Envio
	    partial void OnEnvioChanging(System.DateTime value);
	    partial void OnEnvioChanged();

	    private System.DateTime _Envio;

	    [DataMember(IsRequired = true, Name = "Envio", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Envio", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_MENSAGEM_LOG.TCS_MENSAGEM.ENVIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM.ENVIO")]
	    public System.DateTime Envio
	    {
	    	    get
	    	    {
	    	          return _Envio;
	    	    }
	    	    set
	    	    {
	    	          if (this._Envio != value)
	    	          {
	    	              this.ValidateProperty("Envio", value);
	    	              this.OnEnvioChanging(value);
	    	              this.RaiseDataMemberChanging("Envio");
	    	              this._Envio = value;
	    	              this.RaiseDataMemberChanged("Envio");
	    	              this.OnEnvioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Filtro
	    partial void OnFiltroChanging(System.String value);
	    partial void OnFiltroChanged();

	    private System.String _Filtro;

	    [DataMember(IsRequired = true, Name = "Filtro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Filtro", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_MENSAGEM_LOG.TCS_MENSAGEM.FILTRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM.FILTRO")]
	    public System.String Filtro
	    {
	    	    get
	    	    {
	    	          return _Filtro;
	    	    }
	    	    set
	    	    {
	    	          if (this._Filtro != value)
	    	          {
	    	              this.ValidateProperty("Filtro", value);
	    	              this.OnFiltroChanging(value);
	    	              this.RaiseDataMemberChanging("Filtro");
	    	              this._Filtro = value;
	    	              this.RaiseDataMemberChanged("Filtro");
	    	              this.OnFiltroChanged();
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
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_MENSAGEM_LOG.TCS_MENSAGEM.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
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
	    //Extensibility Partial Method Definitions For LxTipoMensagem
	    partial void OnLxTipoMensagemChanging(Byte value);
	    partial void OnLxTipoMensagemChanged();

	    private Byte _LxTipoMensagem;

	    [DataMember(IsRequired = true, Name = "LxTipoMensagem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Lx Tipo Mensagem", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoMensagem];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_MENSAGEM_LOG.TCS_MENSAGEM.LX_TIPO_MENSAGEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM.LX_TIPO_MENSAGEM")]
	    public Byte LxTipoMensagem
	    {
	    	    get
	    	    {
	    	          return _LxTipoMensagem;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoMensagem != value)
	    	          {
	    	              this.ValidateProperty("LxTipoMensagem", value);
	    	              this.OnLxTipoMensagemChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoMensagem");
	    	              this._LxTipoMensagem = value;
	    	              this.RaiseDataMemberChanged("LxTipoMensagem");
	    	              this.OnLxTipoMensagemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Titulo
	    partial void OnTituloChanging(System.String value);
	    partial void OnTituloChanged();

	    private System.String _Titulo;

	    [DataMember(IsRequired = true, Name = "Titulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Titulo", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(100)]
	    [FunctionalPoint("Precision[100:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_MENSAGEM_LOG.TCS_MENSAGEM.TITULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM.TITULO")]
	    public System.String Titulo
	    {
	    	    get
	    	    {
	    	          return _Titulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._Titulo != value)
	    	          {
	    	              this.ValidateProperty("Titulo", value);
	    	              this.OnTituloChanging(value);
	    	              this.RaiseDataMemberChanging("Titulo");
	    	              this._Titulo = value;
	    	              this.RaiseDataMemberChanged("Titulo");
	    	              this.OnTituloChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_MENSAGEM_LOG").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_MENSAGEM_LOG), QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM_LOG" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM_LOG.LIDA", Source = "Lida", Target = "LIDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM_LOG", RelationPropertyName = "TCS_MENSAGEM_LOG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM_LOG.ENTREGUE", Source = "Entregue", Target = "ENTREGUE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM_LOG", RelationPropertyName = "TCS_MENSAGEM_LOG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM_LOG.DISPENSADA", Source = "Dispensada", Target = "DISPENSADA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM_LOG", RelationPropertyName = "TCS_MENSAGEM_LOG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM_LOG.ID_TCS_MENSAGEM_LOG", Source = "IdTcsMensagemLog", Target = "ID_TCS_MENSAGEM_LOG", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM_LOG", RelationPropertyName = "TCS_MENSAGEM_LOG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM_LOG.TCS_MENSAGEM.ID_TCS_MENSAGEM", Source = "IdTcsMensagem", Target = "ID_TCS_MENSAGEM", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM", RelationPropertyName = "TCS_MENSAGEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM_LOG.TCS_USUARIO_AUTENTICACAO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxTipoMensagemValues()
	    {
	    	    return Linx.Framework.BV.Domains.TipoMensagem.GetValues();
	    }
	    private string _lxTipoMensagemName;
	    [DataMember(IsRequired = false, Name = "LxTipoMensagemName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Lx Tipo Mensagem", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxTipoMensagemName
	    {
	    	    get { if (this.LxTipoMensagem.IsNull()) { _lxTipoMensagemName = String.Empty; } else { string key = this.LxTipoMensagem.ToString(); var dmValues = this.GetLxTipoMensagemValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxTipoMensagemName) _lxTipoMensagemName = domainName; } return _lxTipoMensagemName; } set { _lxTipoMensagemName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[TcsMensagemConsultaLog];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsMensagemLog];ReadOnly[true];Entities[TCS_MENSAGEM_LOG:IdTcsMensagemLog];SubQueryInfo[Select 1 From #ParentAlias#.TCS_MENSAGEM_LOG_LISTA as #Alias#];EdmEntityName[TCS_MENSAGEM_LOG];EntityRelations[TCS_USUARIO_AUTENTICACAO(TCS_USUARIO_AUTENTICACAO)#TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)#TCS_MENSAGEM(TCS_MENSAGEM)];EdmParentEntityName[TCS_MENSAGEM];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsMensagemConsultaLog")]
	[Serializable()]
	public partial class TcsMensagemConsultaLogParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For Dispensada
	    partial void OnDispensadaChanging(System.Nullable<System.DateTime> value);
	    partial void OnDispensadaChanged();

	    private System.Nullable<System.DateTime> _Dispensada;

	    [DataMember(Name = "Dispensada", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Dispensada", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[g];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM_LOG.DISPENSADA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM_LOG.DISPENSADA")]
	    public System.Nullable<System.DateTime> Dispensada
	    {
	    	    get
	    	    {
	    	          return _Dispensada;
	    	    }
	    	    set
	    	    {
	    	          if (this._Dispensada != value)
	    	          {
	    	              this.ValidateProperty("Dispensada", value);
	    	              this.OnDispensadaChanging(value);
	    	              this.RaiseDataMemberChanging("Dispensada");
	    	              this._Dispensada = value;
	    	              this.RaiseDataMemberChanged("Dispensada");
	    	              this.OnDispensadaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Entregue
	    partial void OnEntregueChanging(System.Nullable<System.DateTime> value);
	    partial void OnEntregueChanged();

	    private System.Nullable<System.DateTime> _Entregue;

	    [DataMember(Name = "Entregue", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Entregue", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[g];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM_LOG.ENTREGUE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM_LOG.ENTREGUE")]
	    public System.Nullable<System.DateTime> Entregue
	    {
	    	    get
	    	    {
	    	          return _Entregue;
	    	    }
	    	    set
	    	    {
	    	          if (this._Entregue != value)
	    	          {
	    	              this.ValidateProperty("Entregue", value);
	    	              this.OnEntregueChanging(value);
	    	              this.RaiseDataMemberChanging("Entregue");
	    	              this._Entregue = value;
	    	              this.RaiseDataMemberChanged("Entregue");
	    	              this.OnEntregueChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsMensagem
	    partial void OnIdTcsMensagemChanging(Int64 value);
	    partial void OnIdTcsMensagemChanged();

	    private Int64 _IdTcsMensagem;

	    [DataMember(IsRequired = true, Name = "IdTcsMensagem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Mensagem", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM_LOG.TCS_MENSAGEM.ID_TCS_MENSAGEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM_LOG.TCS_MENSAGEM.ID_TCS_MENSAGEM")]
	    public Int64 IdTcsMensagem
	    {
	    	    get
	    	    {
	    	          return _IdTcsMensagem;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsMensagem != value)
	    	          {
	    	              this.ValidateProperty("IdTcsMensagem", value);
	    	              this.OnIdTcsMensagemChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsMensagem");
	    	              this._IdTcsMensagem = value;
	    	              this.RaiseDataMemberChanged("IdTcsMensagem");
	    	              this.OnIdTcsMensagemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsMensagemLog
	    partial void OnIdTcsMensagemLogChanging(Int64 value);
	    partial void OnIdTcsMensagemLogChanged();

	    private Int64 _IdTcsMensagemLog;

	    [DataMember(IsRequired = true, Name = "IdTcsMensagemLog", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Mensagem Log", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM_LOG.ID_TCS_MENSAGEM_LOG];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM_LOG.ID_TCS_MENSAGEM_LOG")]
	    public Int64 IdTcsMensagemLog
	    {
	    	    get
	    	    {
	    	          return _IdTcsMensagemLog;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsMensagemLog != value)
	    	          {
	    	              this.ValidateProperty("IdTcsMensagemLog", value);
	    	              this.OnIdTcsMensagemLogChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsMensagemLog");
	    	              this._IdTcsMensagemLog = value;
	    	              this.RaiseDataMemberChanged("IdTcsMensagemLog");
	    	              this.OnIdTcsMensagemLogChanged();
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
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacaoCL];LookUpTitle[Seleção de (Id Usuario)];LookUpQuery[executeLookUpTcsUsuarioAutenticacaoCL];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacaoCL];LookUpDisplayColumns[{\"IdUsuario\" : \"Id\", \"NomeUsuario\" : \"Nome\", \"NomeAutenticacao\" : \"Usuário Autenticação\"}];LookUpColumns[{\"IdUsuario\" : false, \"NomeUsuario\" : true, \"NomeAutenticacao\" : true}];FilterDataKey[TCS_MENSAGEM_LOG.TCS_USUARIO_AUTENTICACAO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdUsuario#true##24:0##Id#0#false##::LookUpTcsUsuarioAutenticacaoCL##false#false#TCS_USUARIO_AUTENTICACAO#TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.Mensagem#IQueryable###true#false", EdmKey="TCS_MENSAGEM_LOG.TCS_USUARIO_AUTENTICACAO.ID_USUARIO")]
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
	    //Extensibility Partial Method Definitions For Lida
	    partial void OnLidaChanging(System.Nullable<System.DateTime> value);
	    partial void OnLidaChanged();

	    private System.Nullable<System.DateTime> _Lida;

	    [DataMember(Name = "Lida", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Lida", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[g];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MENSAGEM_LOG.LIDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM_LOG.LIDA")]
	    public System.Nullable<System.DateTime> Lida
	    {
	    	    get
	    	    {
	    	          return _Lida;
	    	    }
	    	    set
	    	    {
	    	          if (this._Lida != value)
	    	          {
	    	              this.ValidateProperty("Lida", value);
	    	              this.OnLidaChanging(value);
	    	              this.RaiseDataMemberChanging("Lida");
	    	              this._Lida = value;
	    	              this.RaiseDataMemberChanged("Lida");
	    	              this.OnLidaChanged();
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
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacaoCL];LookUpTitle[Seleção de (Usuário Autenticação)];LookUpQuery[executeLookUpTcsUsuarioAutenticacaoCL];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacaoCL];LookUpDisplayColumns[{\"IdUsuario\" : \"Id\", \"NomeUsuario\" : \"Nome\", \"NomeAutenticacao\" : \"Usuário Autenticação\"}];LookUpColumns[{\"IdUsuario\" : false, \"NomeUsuario\" : true, \"NomeAutenticacao\" : true}];FilterDataKey[TCS_MENSAGEM_LOG.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="String#NomeAutenticacao#false##250:0##Usuário Autenticação#2#true##::LookUpTcsUsuarioAutenticacaoCL##false#false#TCS_USUARIO_AUTENTICACAO#TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.Mensagem#IQueryable###true#false", EdmKey="TCS_MENSAGEM_LOG.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO")]
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
	    [Display(Name = "Emitente", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacaoCL];LookUpTitle[Seleção de (Emitente)];LookUpQuery[executeLookUpTcsUsuarioAutenticacaoCL];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacaoCL];LookUpDisplayColumns[{\"IdUsuario\" : \"Id\", \"NomeUsuario\" : \"Nome\", \"NomeAutenticacao\" : \"Usuário Autenticação\"}];LookUpColumns[{\"IdUsuario\" : false, \"NomeUsuario\" : true, \"NomeAutenticacao\" : true}];FilterDataKey[TCS_MENSAGEM_LOG.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="String#NomeUsuario#false##250:0##Nome#1#true##::LookUpTcsUsuarioAutenticacaoCL##false#false#TCS_USUARIO_AUTENTICACAO#TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.Mensagem#IQueryable###true#false", EdmKey="TCS_MENSAGEM_LOG.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO")]
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
	    //Extensibility Partial Method Definitions For Corpo
	    partial void OnCorpoChanging(System.String value);
	    partial void OnCorpoChanged();

	    private System.String _Corpo;

	    [DataMember(IsRequired = true, Name = "Corpo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Corpo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_MENSAGEM_LOG.TCS_MENSAGEM.CORPO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM.CORPO")]
	    public System.String Corpo
	    {
	    	    get
	    	    {
	    	          return _Corpo;
	    	    }
	    	    set
	    	    {
	    	          if (this._Corpo != value)
	    	          {
	    	              this.ValidateProperty("Corpo", value);
	    	              this.OnCorpoChanging(value);
	    	              this.RaiseDataMemberChanging("Corpo");
	    	              this._Corpo = value;
	    	              this.RaiseDataMemberChanged("Corpo");
	    	              this.OnCorpoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Criacao
	    partial void OnCriacaoChanging(System.DateTime value);
	    partial void OnCriacaoChanged();

	    private System.DateTime _Criacao;

	    [DataMember(IsRequired = true, Name = "Criacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Criação", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[g];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_MENSAGEM_LOG.TCS_MENSAGEM.CRIACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM.CRIACAO")]
	    public System.DateTime Criacao
	    {
	    	    get
	    	    {
	    	          return _Criacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._Criacao != value)
	    	          {
	    	              this.ValidateProperty("Criacao", value);
	    	              this.OnCriacaoChanging(value);
	    	              this.RaiseDataMemberChanging("Criacao");
	    	              this._Criacao = value;
	    	              this.RaiseDataMemberChanged("Criacao");
	    	              this.OnCriacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Envio
	    partial void OnEnvioChanging(System.DateTime value);
	    partial void OnEnvioChanged();

	    private System.DateTime _Envio;

	    [DataMember(IsRequired = true, Name = "Envio", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Envio", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[g];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_MENSAGEM_LOG.TCS_MENSAGEM.ENVIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM.ENVIO")]
	    public System.DateTime Envio
	    {
	    	    get
	    	    {
	    	          return _Envio;
	    	    }
	    	    set
	    	    {
	    	          if (this._Envio != value)
	    	          {
	    	              this.ValidateProperty("Envio", value);
	    	              this.OnEnvioChanging(value);
	    	              this.RaiseDataMemberChanging("Envio");
	    	              this._Envio = value;
	    	              this.RaiseDataMemberChanged("Envio");
	    	              this.OnEnvioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Filtro
	    partial void OnFiltroChanging(System.String value);
	    partial void OnFiltroChanged();

	    private System.String _Filtro;

	    [DataMember(IsRequired = true, Name = "Filtro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Filtro", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_MENSAGEM_LOG.TCS_MENSAGEM.FILTRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM.FILTRO")]
	    public System.String Filtro
	    {
	    	    get
	    	    {
	    	          return _Filtro;
	    	    }
	    	    set
	    	    {
	    	          if (this._Filtro != value)
	    	          {
	    	              this.ValidateProperty("Filtro", value);
	    	              this.OnFiltroChanging(value);
	    	              this.RaiseDataMemberChanging("Filtro");
	    	              this._Filtro = value;
	    	              this.RaiseDataMemberChanged("Filtro");
	    	              this.OnFiltroChanged();
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
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_MENSAGEM_LOG.TCS_MENSAGEM.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
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
	    //Extensibility Partial Method Definitions For LxTipoMensagem
	    partial void OnLxTipoMensagemChanging(Byte value);
	    partial void OnLxTipoMensagemChanged();

	    private Byte _LxTipoMensagem;

	    [DataMember(IsRequired = true, Name = "LxTipoMensagem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo Mensagem", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoMensagem];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_MENSAGEM_LOG.TCS_MENSAGEM.LX_TIPO_MENSAGEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM.LX_TIPO_MENSAGEM")]
	    public Byte LxTipoMensagem
	    {
	    	    get
	    	    {
	    	          return _LxTipoMensagem;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoMensagem != value)
	    	          {
	    	              this.ValidateProperty("LxTipoMensagem", value);
	    	              this.OnLxTipoMensagemChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoMensagem");
	    	              this._LxTipoMensagem = value;
	    	              this.RaiseDataMemberChanged("LxTipoMensagem");
	    	              this.OnLxTipoMensagemChanged();
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
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_MENSAGEM_LOG.TCS_MENSAGEM.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
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
	    //Extensibility Partial Method Definitions For Titulo
	    partial void OnTituloChanging(System.String value);
	    partial void OnTituloChanged();

	    private System.String _Titulo;

	    [DataMember(IsRequired = true, Name = "Titulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Título", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(100)]
	    [FunctionalPoint("Precision[100:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_MENSAGEM_LOG.TCS_MENSAGEM.TITULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MENSAGEM.TITULO")]
	    public System.String Titulo
	    {
	    	    get
	    	    {
	    	          return _Titulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._Titulo != value)
	    	          {
	    	              this.ValidateProperty("Titulo", value);
	    	              this.OnTituloChanging(value);
	    	              this.RaiseDataMemberChanging("Titulo");
	    	              this._Titulo = value;
	    	              this.RaiseDataMemberChanged("Titulo");
	    	              this.OnTituloChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_MENSAGEM_LOG").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_MENSAGEM_LOG), QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM_LOG" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM_LOG.LIDA", Source = "Lida", Target = "LIDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM_LOG", RelationPropertyName = "TCS_MENSAGEM_LOG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM_LOG.ENTREGUE", Source = "Entregue", Target = "ENTREGUE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM_LOG", RelationPropertyName = "TCS_MENSAGEM_LOG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM_LOG.DISPENSADA", Source = "Dispensada", Target = "DISPENSADA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM_LOG", RelationPropertyName = "TCS_MENSAGEM_LOG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM_LOG.ID_TCS_MENSAGEM_LOG", Source = "IdTcsMensagemLog", Target = "ID_TCS_MENSAGEM_LOG", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM_LOG", RelationPropertyName = "TCS_MENSAGEM_LOG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM_LOG.TCS_MENSAGEM.ID_TCS_MENSAGEM", Source = "IdTcsMensagem", Target = "ID_TCS_MENSAGEM", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_MENSAGEM", RelationPropertyName = "TCS_MENSAGEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MENSAGEM_LOG.TCS_USUARIO_AUTENTICACAO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxTipoMensagemValues()
	    {
	    	    return Linx.Framework.BV.Domains.TipoMensagem.GetValues();
	    }
	    private string _lxTipoMensagemName;
	    [DataMember(IsRequired = false, Name = "LxTipoMensagemName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo Mensagem", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxTipoMensagemName
	    {
	    	    get { if (this.LxTipoMensagem.IsNull()) { _lxTipoMensagemName = String.Empty; } else { string key = this.LxTipoMensagem.ToString(); var dmValues = this.GetLxTipoMensagemValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxTipoMensagemName) _lxTipoMensagemName = domainName; } return _lxTipoMensagemName; } set { _lxTipoMensagemName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	//////////////////////// DomainService Class V1 ///////////////////////
	///////////////////////////////////////////////////////////////////////
	[EnableClientAccess()]	
	[DomainIdentifier("ProcessorOverviewMensagemDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class MensagemDomainService : DomainService, IDataServiceContext 
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

		
	    public MensagemDomainService() : this("", null, null) { }
	    public MensagemDomainService(string connectionString) : this(connectionString, null, null) { }
	    public MensagemDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public MensagemDomainService(Linx.Framework.Autorizacao.BM.AutorizacaoContext dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public MensagemDomainService(string connectionString, Linx.Framework.Autorizacao.BM.AutorizacaoContext dataContext, Dictionary<string, string> headers) : base() 
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
 	        var _TcsMensagemElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsMensagem && e.Entity.GetType().Name == "TcsMensagem" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _TcsMensagemElements)
 	           if (((TcsMensagem)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 	        var _TcsMensagemConsultaElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsMensagemConsulta && e.Entity.GetType().Name == "TcsMensagemConsulta" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _TcsMensagemConsultaElements)
 	           if (((TcsMensagemConsulta)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 
 	        //Remove inconsistent details
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsMensagemLogDetail && e.Entity.GetType().Name == "TcsMensagemLogDetail" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsMensagemConsultaLog && e.Entity.GetType().Name == "TcsMensagemConsultaLog" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
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
	    //Get All LookUpTcsEmpresaAutenticacaoC.
	    public IQueryable<LookUpTcsEmpresaAutenticacaoC> GetAllLookUpTcsEmpresaAutenticacaoC()
	    {
	        return this.GetLookUpTcsEmpresaAutenticacaoC(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsEmpresaAutenticacaoC By EntitySearch.
	    public IQueryable<LookUpTcsEmpresaAutenticacaoC> GetLookUpTcsEmpresaAutenticacaoCByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsEmpresaAutenticacaoC(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsEmpresaAutenticacaoC.
	    public IQueryable<LookUpTcsEmpresaAutenticacaoC> GetLookUpTcsEmpresaAutenticacaoC(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TCS_EMPRESA_AUTENTICACAO" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsEmpresaAutenticacaoC";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsEmpresaAutenticacaoC));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsEmpresaAutenticacaoC> query =  
	
	            (from entity in this.DbContext.TCS_EMPRESA_AUTENTICACAO.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpTcsEmpresaAutenticacaoC()		
	            {
	            
                IdLinx = entity.ID_LINX
                , NomeEmpresa = entity.NOME_EMPRESA
	            });

	            
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpTcsUsuarioAutenticacaoC.
	    public IQueryable<LookUpTcsUsuarioAutenticacaoC> GetAllLookUpTcsUsuarioAutenticacaoC()
	    {
	        return this.GetLookUpTcsUsuarioAutenticacaoC(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsUsuarioAutenticacaoC By EntitySearch.
	    public IQueryable<LookUpTcsUsuarioAutenticacaoC> GetLookUpTcsUsuarioAutenticacaoCByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsUsuarioAutenticacaoC(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsUsuarioAutenticacaoC.
	    public IQueryable<LookUpTcsUsuarioAutenticacaoC> GetLookUpTcsUsuarioAutenticacaoC(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TCS_USUARIO_AUTENTICACAO" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsUsuarioAutenticacaoC";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsUsuarioAutenticacaoC));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsUsuarioAutenticacaoC> query =  
	
	            (from entity in this.DbContext.TCS_USUARIO_AUTENTICACAO.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpTcsUsuarioAutenticacaoC()		
	            {
	            
                IdUsuario = entity.ID_USUARIO
                , NomeUsuario = entity.NOME_USUARIO
                , NomeAutenticacao = entity.NOME_AUTENTICACAO
	            });

	            
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpTcsUsuarioAutenticacaoCL.
	    public IQueryable<LookUpTcsUsuarioAutenticacaoCL> GetAllLookUpTcsUsuarioAutenticacaoCL()
	    {
	        return this.GetLookUpTcsUsuarioAutenticacaoCL(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsUsuarioAutenticacaoCL By EntitySearch.
	    public IQueryable<LookUpTcsUsuarioAutenticacaoCL> GetLookUpTcsUsuarioAutenticacaoCLByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsUsuarioAutenticacaoCL(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsUsuarioAutenticacaoCL.
	    public IQueryable<LookUpTcsUsuarioAutenticacaoCL> GetLookUpTcsUsuarioAutenticacaoCL(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TCS_USUARIO_AUTENTICACAO" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsUsuarioAutenticacaoCL";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsUsuarioAutenticacaoCL));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsUsuarioAutenticacaoCL> query =  
	
	            (from entity in this.DbContext.TCS_USUARIO_AUTENTICACAO.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpTcsUsuarioAutenticacaoCL()		
	            {
	            
                IdUsuario = entity.ID_USUARIO
                , NomeUsuario = entity.NOME_USUARIO
                , NomeAutenticacao = entity.NOME_AUTENTICACAO
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
	
		

	        if (entityName.InList("Linx.Framework.BV.Mensagem.TcsMensagem"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsMensagem",
	        			NameSpace = "Linx.Framework.BV.Mensagem",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsMensagem",
	        			ClearMethodName = "ClearTcsMensagem",
	        			QueryMethodName  = "GetPagedTcsMensagem",	
	        			CountingMethodName  = "GetTcsMensagem" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Mensagem.TcsMensagem"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Mensagem.TcsMensagem"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Mensagem.TcsMensagem", "Linx.Framework.BV.Mensagem.TcsMensagemLogDetail"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsMensagemLogDetail" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.Mensagem",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsMensagem",	
	        			DisplayName = "TcsMensagemLogDetail",
	        			ClearMethodName = "ClearTcsMensagemLogDetail" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsMensagemLogDetail" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsMensagemLogDetail" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Mensagem.TcsMensagemLogDetail"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Mensagem.TcsMensagemLogDetail" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Mensagem.MensagemInfo"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "MensagemInfo",
	        			NameSpace = "Linx.Framework.BV.Mensagem",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "MensagemInfo",
	        			ClearMethodName = "ClearMensagemInfo",
	        			QueryMethodName  = "GetPagedMensagemInfo",	
	        			CountingMethodName  = "GetMensagemInfo" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Mensagem.MensagemInfo"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Mensagem.MensagemInfo"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Mensagem.TcsMensagemUsuario"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsMensagemUsuario",
	        			NameSpace = "Linx.Framework.BV.Mensagem",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsMensagemUsuario",
	        			ClearMethodName = "ClearTcsMensagemUsuario",
	        			QueryMethodName  = "GetPagedTcsMensagemUsuario",	
	        			CountingMethodName  = "GetTcsMensagemUsuario" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Mensagem.TcsMensagemUsuario"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Mensagem.TcsMensagemUsuario"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Mensagem.TcsMensagemLog"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsMensagemLog",
	        			NameSpace = "Linx.Framework.BV.Mensagem",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsMensagemLog",
	        			ClearMethodName = "ClearTcsMensagemLog",
	        			QueryMethodName  = "GetPagedTcsMensagemLog",	
	        			CountingMethodName  = "GetTcsMensagemLog" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Mensagem.TcsMensagemLog"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Mensagem.TcsMensagemLog"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Mensagem.TcsPerfil"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsPerfil",
	        			NameSpace = "Linx.Framework.BV.Mensagem",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsPerfil",
	        			ClearMethodName = "ClearTcsPerfil",
	        			QueryMethodName  = "GetPagedTcsPerfil",	
	        			CountingMethodName  = "GetTcsPerfil" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Mensagem.TcsPerfil"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Mensagem.TcsPerfil"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Mensagem.TcsUsuario"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsUsuario",
	        			NameSpace = "Linx.Framework.BV.Mensagem",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsUsuario",
	        			ClearMethodName = "ClearTcsUsuario",
	        			QueryMethodName  = "GetPagedTcsUsuario",	
	        			CountingMethodName  = "GetTcsUsuario" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Mensagem.TcsUsuario"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Mensagem.TcsUsuario"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Mensagem.NewMessageInfo"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "NewMessageInfo",
	        			NameSpace = "Linx.Framework.BV.Mensagem",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "NewMessageInfo",
	        			ClearMethodName = "ClearNewMessageInfo",
	        			QueryMethodName  = "GetPagedNewMessageInfo",	
	        			CountingMethodName  = "GetNewMessageInfo" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Mensagem.NewMessageInfo"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Mensagem.NewMessageInfo"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Mensagem.TcsMensagemConsulta"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsMensagemConsulta",
	        			NameSpace = "Linx.Framework.BV.Mensagem",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsMensagemConsulta",
	        			ClearMethodName = "ClearTcsMensagemConsulta",
	        			QueryMethodName  = "GetPagedTcsMensagemConsulta",	
	        			CountingMethodName  = "GetTcsMensagemConsulta" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Mensagem.TcsMensagemConsulta"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Mensagem.TcsMensagemConsulta"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Mensagem.TcsMensagemConsulta", "Linx.Framework.BV.Mensagem.TcsMensagemConsultaLog"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsMensagemConsultaLog" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.Mensagem",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsMensagemConsulta",	
	        			DisplayName = "TcsMensagemConsultaLog",
	        			ClearMethodName = "ClearTcsMensagemConsultaLog" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsMensagemConsultaLog" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsMensagemConsultaLog" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Mensagem.TcsMensagemConsultaLog"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Mensagem.TcsMensagemConsultaLog" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
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

         		    return new string[] { "Framework_MensagemClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.MensagemClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Framework_mensagemService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.mensagemService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear TcsMensagem.
	    public IEnumerable<TcsMensagem> ClearTcsMensagem()
	    {
	        List<TcsMensagem> result = new List<TcsMensagem>();
	        result.Add(new TcsMensagem());	
			
	        result[0].TcsMensagemLogDetailList = new List<TcsMensagemLogDetail>();
	        ((List<TcsMensagemLogDetail>)result[0].TcsMensagemLogDetailList).Add(new TcsMensagemLogDetail());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsMensagemLogDetail.
	    public IEnumerable<TcsMensagemLogDetail> ClearTcsMensagemLogDetail()
	    {
	        List<TcsMensagemLogDetail> result = new List<TcsMensagemLogDetail>();
	        result.Add(new TcsMensagemLogDetail());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear MensagemInfo.
	    public IEnumerable<MensagemInfo> ClearMensagemInfo()
	    {
	        List<MensagemInfo> result = new List<MensagemInfo>();
	        result.Add(new MensagemInfo());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsMensagemUsuario.
	    public IEnumerable<TcsMensagemUsuario> ClearTcsMensagemUsuario()
	    {
	        List<TcsMensagemUsuario> result = new List<TcsMensagemUsuario>();
	        result.Add(new TcsMensagemUsuario());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsMensagemLog.
	    public IEnumerable<TcsMensagemLog> ClearTcsMensagemLog()
	    {
	        List<TcsMensagemLog> result = new List<TcsMensagemLog>();
	        result.Add(new TcsMensagemLog());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsPerfil.
	    public IEnumerable<TcsPerfil> ClearTcsPerfil()
	    {
	        List<TcsPerfil> result = new List<TcsPerfil>();
	        result.Add(new TcsPerfil());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsUsuario.
	    public IEnumerable<TcsUsuario> ClearTcsUsuario()
	    {
	        List<TcsUsuario> result = new List<TcsUsuario>();
	        result.Add(new TcsUsuario());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear NewMessageInfo.
	    public IEnumerable<NewMessageInfo> ClearNewMessageInfo()
	    {
	        List<NewMessageInfo> result = new List<NewMessageInfo>();
	        result.Add(new NewMessageInfo());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsMensagemConsulta.
	    public IEnumerable<TcsMensagemConsulta> ClearTcsMensagemConsulta()
	    {
	        List<TcsMensagemConsulta> result = new List<TcsMensagemConsulta>();
	        result.Add(new TcsMensagemConsulta());	
			
	        result[0].TcsMensagemConsultaLogList = new List<TcsMensagemConsultaLog>();
	        ((List<TcsMensagemConsultaLog>)result[0].TcsMensagemConsultaLogList).Add(new TcsMensagemConsultaLog());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsMensagemConsultaLog.
	    public IEnumerable<TcsMensagemConsultaLog> ClearTcsMensagemConsultaLog()
	    {
	        List<TcsMensagemConsultaLog> result = new List<TcsMensagemConsultaLog>();
	        result.Add(new TcsMensagemConsultaLog());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsMensagem.
	    public IQueryable<TcsMensagem> GetTcsMensagem()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsMensagem> result = 
	            (from entity0 in this.DbContext.TCS_MENSAGEM
                  let entity0Al1 = entity0.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_USUARIO_AUTENTICACAO
	            
	            	
	            select new TcsMensagem()		
	            {
	            
                Corpo = entity0.CORPO
                , Criacao = entity0.CRIACAO
                , Envio = entity0.ENVIO
                , Filtro = entity0.FILTRO
                , IdLinx = entity0Al1.ID_LINX
                , IdTcsMensagem = entity0.ID_TCS_MENSAGEM
                , IdUsuario = entity0Al2.ID_USUARIO
                , LxTipoMensagem = entity0.LX_TIPO_MENSAGEM
                , LxTipoMensagemName = ((entity0.LX_TIPO_MENSAGEM) == 3 ? "Erro" : ((entity0.LX_TIPO_MENSAGEM) == 1 ? "Informação" : ((entity0.LX_TIPO_MENSAGEM) == 4 ? "Sucesso" : ((entity0.LX_TIPO_MENSAGEM) == 2 ? "Alerta" : ""))))
                , Titulo = entity0.TITULO
			
                ,TcsMensagemLogDetailList = 
	                        (from entity1 in entity0.TCS_MENSAGEM_LOG_LISTA
                                  let entity1Al1 = entity1.TCS_MENSAGEM
                                  let entity1Al2 = entity1.TCS_USUARIO_AUTENTICACAO
	                        
	                        	
	                        select new TcsMensagemLogDetail()
	                        {
	                        
                                Dispensada = entity1.DISPENSADA
                                , Entregue = entity1.ENTREGUE
                                , IdTcsMensagem = entity1Al1.ID_TCS_MENSAGEM
                                , IdTcsMensagemLog = entity1.ID_TCS_MENSAGEM_LOG
                                , IdUsuario = entity1Al2.ID_USUARIO
                                , Lida = entity1.LIDA
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsMensagemLogDetail.
	    public IQueryable<TcsMensagemLogDetail> GetTcsMensagemLogDetail()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsMensagemLogDetail> result = 
	            (from entity0 in this.DbContext.TCS_MENSAGEM_LOG
                  let entity0Al1 = entity0.TCS_MENSAGEM
                  let entity0Al2 = entity0.TCS_USUARIO_AUTENTICACAO
	            
	            	
	            select new TcsMensagemLogDetail()		
	            {
	            
                Dispensada = entity0.DISPENSADA
                , Entregue = entity0.ENTREGUE
                , IdTcsMensagem = entity0Al1.ID_TCS_MENSAGEM
                , IdTcsMensagemLog = entity0.ID_TCS_MENSAGEM_LOG
                , IdUsuario = entity0Al2.ID_USUARIO
                , Lida = entity0.LIDA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsMensagemNoAssociations.
	    public IQueryable<TcsMensagem> GetTcsMensagemNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsMensagem> result = 
	            (from entity0 in this.DbContext.TCS_MENSAGEM
                  let entity0Al1 = entity0.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_USUARIO_AUTENTICACAO
	            
	            	
	            select new TcsMensagem()		
	            {
	            
                Corpo = entity0.CORPO
                , Criacao = entity0.CRIACAO
                , Envio = entity0.ENVIO
                , Filtro = entity0.FILTRO
                , IdLinx = entity0Al1.ID_LINX
                , IdTcsMensagem = entity0.ID_TCS_MENSAGEM
                , IdUsuario = entity0Al2.ID_USUARIO
                , LxTipoMensagem = entity0.LX_TIPO_MENSAGEM
                , LxTipoMensagemName = ((entity0.LX_TIPO_MENSAGEM) == 3 ? "Erro" : ((entity0.LX_TIPO_MENSAGEM) == 1 ? "Informação" : ((entity0.LX_TIPO_MENSAGEM) == 4 ? "Sucesso" : ((entity0.LX_TIPO_MENSAGEM) == 2 ? "Alerta" : ""))))
                , Titulo = entity0.TITULO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsMensagemLogDetailNoAssociations.
	    public IQueryable<TcsMensagemLogDetail> GetTcsMensagemLogDetailNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsMensagemLogDetail> result = 
	            (from entity0 in this.DbContext.TCS_MENSAGEM_LOG
                  let entity0Al1 = entity0.TCS_MENSAGEM
                  let entity0Al2 = entity0.TCS_USUARIO_AUTENTICACAO
	            
	            	
	            select new TcsMensagemLogDetail()		
	            {
	            
                Dispensada = entity0.DISPENSADA
                , Entregue = entity0.ENTREGUE
                , IdTcsMensagem = entity0Al1.ID_TCS_MENSAGEM
                , IdTcsMensagemLog = entity0.ID_TCS_MENSAGEM_LOG
                , IdUsuario = entity0Al2.ID_USUARIO
                , Lida = entity0.LIDA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get MensagemInfo.
	    public IEnumerable<MensagemInfo> GetMensagemInfo()
	    {




	
	        IEnumerable<MensagemInfo> result = new List<MensagemInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get MensagemInfoNoAssociations.
	    public IEnumerable<MensagemInfo> GetMensagemInfoNoAssociations()
	    {




	
	        IEnumerable<MensagemInfo> result = new List<MensagemInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsMensagemUsuario.
	    public IQueryable<TcsMensagemUsuario> GetTcsMensagemUsuario()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsMensagemUsuario> result = 
	            (from entity0 in this.DbContext.TCS_MENSAGEM_LOG
                  let entity0Al1 = entity0.TCS_MENSAGEM
                  let entity0Al3 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_MENSAGEM.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsMensagemUsuario()		
	            {
	            
                Corpo = entity0Al1.CORPO
                , Dispensada = entity0.DISPENSADA
                , Entregue = entity0.ENTREGUE
                , Envio = entity0Al1.ENVIO
                , IdLinx = entity0Al2.ID_LINX
                , IdTcsMensagem = entity0Al1.ID_TCS_MENSAGEM
                , IdTcsMensagemLog = entity0.ID_TCS_MENSAGEM_LOG
                , IdUsuario = entity0Al3.ID_USUARIO
                , Lida = entity0.LIDA
                , LxTipoMensagem = entity0Al1.LX_TIPO_MENSAGEM
                , LxTipoMensagemName = ((entity0Al1.LX_TIPO_MENSAGEM) == 3 ? "Erro" : ((entity0Al1.LX_TIPO_MENSAGEM) == 1 ? "Informação" : ((entity0Al1.LX_TIPO_MENSAGEM) == 4 ? "Sucesso" : ((entity0Al1.LX_TIPO_MENSAGEM) == 2 ? "Alerta" : ""))))
                , Titulo = entity0Al1.TITULO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsMensagemUsuarioNoAssociations.
	    public IQueryable<TcsMensagemUsuario> GetTcsMensagemUsuarioNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsMensagemUsuario> result = 
	            (from entity0 in this.DbContext.TCS_MENSAGEM_LOG
                  let entity0Al1 = entity0.TCS_MENSAGEM
                  let entity0Al3 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_MENSAGEM.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsMensagemUsuario()		
	            {
	            
                Corpo = entity0Al1.CORPO
                , Dispensada = entity0.DISPENSADA
                , Entregue = entity0.ENTREGUE
                , Envio = entity0Al1.ENVIO
                , IdLinx = entity0Al2.ID_LINX
                , IdTcsMensagem = entity0Al1.ID_TCS_MENSAGEM
                , IdTcsMensagemLog = entity0.ID_TCS_MENSAGEM_LOG
                , IdUsuario = entity0Al3.ID_USUARIO
                , Lida = entity0.LIDA
                , LxTipoMensagem = entity0Al1.LX_TIPO_MENSAGEM
                , LxTipoMensagemName = ((entity0Al1.LX_TIPO_MENSAGEM) == 3 ? "Erro" : ((entity0Al1.LX_TIPO_MENSAGEM) == 1 ? "Informação" : ((entity0Al1.LX_TIPO_MENSAGEM) == 4 ? "Sucesso" : ((entity0Al1.LX_TIPO_MENSAGEM) == 2 ? "Alerta" : ""))))
                , Titulo = entity0Al1.TITULO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsMensagemLog.
	    public IQueryable<TcsMensagemLog> GetTcsMensagemLog()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsMensagemLog> result = 
	            (from entity0 in this.DbContext.TCS_MENSAGEM_LOG
                  let entity0Al1 = entity0.TCS_MENSAGEM
                  let entity0Al2 = entity0.TCS_USUARIO_AUTENTICACAO
	            
	            	
	            select new TcsMensagemLog()		
	            {
	            
                Dispensada = entity0.DISPENSADA
                , Entregue = entity0.ENTREGUE
                , IdTcsMensagem = entity0Al1.ID_TCS_MENSAGEM
                , IdTcsMensagemLog = entity0.ID_TCS_MENSAGEM_LOG
                , IdUsuario = entity0Al2.ID_USUARIO
                , Lida = entity0.LIDA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsMensagemLogNoAssociations.
	    public IQueryable<TcsMensagemLog> GetTcsMensagemLogNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsMensagemLog> result = 
	            (from entity0 in this.DbContext.TCS_MENSAGEM_LOG
                  let entity0Al1 = entity0.TCS_MENSAGEM
                  let entity0Al2 = entity0.TCS_USUARIO_AUTENTICACAO
	            
	            	
	            select new TcsMensagemLog()		
	            {
	            
                Dispensada = entity0.DISPENSADA
                , Entregue = entity0.ENTREGUE
                , IdTcsMensagem = entity0Al1.ID_TCS_MENSAGEM
                , IdTcsMensagemLog = entity0.ID_TCS_MENSAGEM_LOG
                , IdUsuario = entity0Al2.ID_USUARIO
                , Lida = entity0.LIDA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsPerfil.
	    public IQueryable<TcsPerfil> GetTcsPerfil()
	    {




		

	        IQueryable<TcsPerfil> result = 
	            (from entity0 in TcsPerfil.GetBusinessView(this, "") select entity0);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsPerfilNoAssociations.
	    public IQueryable<TcsPerfil> GetTcsPerfilNoAssociations()
	    {




		

	        IQueryable<TcsPerfil> result = 
	            (from entity0 in TcsPerfil.GetBusinessView(this, "") select entity0);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsUsuario.
	    public IQueryable<TcsUsuario> GetTcsUsuario()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuario> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_AUTENTICACAO
                  let entity0Al1 = entity0.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsUsuario()		
	            {
	            
                AutenticacaoWindows = entity0.AUTENTICACAO_WINDOWS
                , Bairro = entity0.BAIRRO
                , Cep = entity0.CEP
                , CnpjCpf = entity0.CNPJ_CPF
                , Complemento = entity0.COMPLEMENTO
                , DataAlteracao = entity0.DATA_ALTERACAO
                , DataCadastro = entity0.DATA_CADASTRO
                , DataExpiracaoSenha = entity0.DATA_EXPIRACAO_SENHA
                , Email = entity0.EMAIL
                , FoneCelular = entity0.FONE_CELULAR
                , FoneFixo = entity0.FONE_FIXO
                , IdLinx = entity0Al1.ID_LINX
                , IdUsuario = entity0.ID_USUARIO
                , Inativo = entity0.INATIVO
                , IndicaAcessoSuporte = entity0.INDICA_ACESSO_SUPORTE
                , InscrEstadualRg = entity0.INSCR_ESTADUAL_RG
                , Logradouro = entity0.LOGRADOURO
                , LxPfjFisicaJuridica = entity0.LX_PFJ_FISICA_JURIDICA
                , LxPfjFisicaJuridicaName = ((entity0.LX_PFJ_FISICA_JURIDICA) == 1 ? "Pessoa Física" : ((entity0.LX_PFJ_FISICA_JURIDICA) == 2 ? "Pessoa Jurídica" : ""))
                , LxTipoLogradouro = entity0.LX_TIPO_LOGRADOURO
                , LxTipoLogradouroName = ((entity0.LX_TIPO_LOGRADOURO) == 1 ? "Aeroporto" : ((entity0.LX_TIPO_LOGRADOURO) == 2 ? "Alameda" : ((entity0.LX_TIPO_LOGRADOURO) == 3 ? "Apartamento" : ((entity0.LX_TIPO_LOGRADOURO) == 4 ? "Avenida" : ((entity0.LX_TIPO_LOGRADOURO) == 5 ? "Beco" : ((entity0.LX_TIPO_LOGRADOURO) == 6 ? "Bloco" : ((entity0.LX_TIPO_LOGRADOURO) == 7 ? "Caminho" : ((entity0.LX_TIPO_LOGRADOURO) == 8 ? "Escadinha" : ((entity0.LX_TIPO_LOGRADOURO) == 9 ? "Estação" : ((entity0.LX_TIPO_LOGRADOURO) == 10 ? "Estrada" : ((entity0.LX_TIPO_LOGRADOURO) == 11 ? "Fazenda" : ((entity0.LX_TIPO_LOGRADOURO) == 12 ? "Fortaleza" : ((entity0.LX_TIPO_LOGRADOURO) == 13 ? "Galeria" : ((entity0.LX_TIPO_LOGRADOURO) == 14 ? "Ladeira" : ((entity0.LX_TIPO_LOGRADOURO) == 15 ? "Largo" : ((entity0.LX_TIPO_LOGRADOURO) == 17 ? "Parque" : ((entity0.LX_TIPO_LOGRADOURO) == 16 ? "Praça" : ((entity0.LX_TIPO_LOGRADOURO) == 18 ? "Praia" : ((entity0.LX_TIPO_LOGRADOURO) == 19 ? "Quadra" : ((entity0.LX_TIPO_LOGRADOURO) == 20 ? "Quilômetro" : ((entity0.LX_TIPO_LOGRADOURO) == 21 ? "Quinta" : ((entity0.LX_TIPO_LOGRADOURO) == 22 ? "Rodovia" : ((entity0.LX_TIPO_LOGRADOURO) == 23 ? "Rua" : ((entity0.LX_TIPO_LOGRADOURO) == 24 ? "Super Quadra" : ((entity0.LX_TIPO_LOGRADOURO) == 25 ? "Travessa" : ((entity0.LX_TIPO_LOGRADOURO) == 26 ? "Viaduto" : ((entity0.LX_TIPO_LOGRADOURO) == 27 ? "Vila" : "")))))))))))))))))))))))))))
                , Municipio = entity0.MUNICIPIO
                , NomeAutenticacao = entity0.NOME_AUTENTICACAO
                , NomeCurtoUsuario = entity0.NOME_CURTO_USUARIO
                , NomeUsuario = entity0.NOME_USUARIO
                , Numero = entity0.NUMERO
                , ObsEndereco = entity0.OBS_ENDERECO
                , Ramal = entity0.RAMAL
                , Uf = entity0.UF
                , UidUsuario = entity0.UID_USUARIO
                , VigenciaFinal = entity0.VIGENCIA_FINAL
                , VigenciaInicial = entity0.VIGENCIA_INICIAL
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioNoAssociations.
	    public IQueryable<TcsUsuario> GetTcsUsuarioNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuario> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_AUTENTICACAO
                  let entity0Al1 = entity0.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsUsuario()		
	            {
	            
                AutenticacaoWindows = entity0.AUTENTICACAO_WINDOWS
                , Bairro = entity0.BAIRRO
                , Cep = entity0.CEP
                , CnpjCpf = entity0.CNPJ_CPF
                , Complemento = entity0.COMPLEMENTO
                , DataAlteracao = entity0.DATA_ALTERACAO
                , DataCadastro = entity0.DATA_CADASTRO
                , DataExpiracaoSenha = entity0.DATA_EXPIRACAO_SENHA
                , Email = entity0.EMAIL
                , FoneCelular = entity0.FONE_CELULAR
                , FoneFixo = entity0.FONE_FIXO
                , IdLinx = entity0Al1.ID_LINX
                , IdUsuario = entity0.ID_USUARIO
                , Inativo = entity0.INATIVO
                , IndicaAcessoSuporte = entity0.INDICA_ACESSO_SUPORTE
                , InscrEstadualRg = entity0.INSCR_ESTADUAL_RG
                , Logradouro = entity0.LOGRADOURO
                , LxPfjFisicaJuridica = entity0.LX_PFJ_FISICA_JURIDICA
                , LxPfjFisicaJuridicaName = ((entity0.LX_PFJ_FISICA_JURIDICA) == 1 ? "Pessoa Física" : ((entity0.LX_PFJ_FISICA_JURIDICA) == 2 ? "Pessoa Jurídica" : ""))
                , LxTipoLogradouro = entity0.LX_TIPO_LOGRADOURO
                , LxTipoLogradouroName = ((entity0.LX_TIPO_LOGRADOURO) == 1 ? "Aeroporto" : ((entity0.LX_TIPO_LOGRADOURO) == 2 ? "Alameda" : ((entity0.LX_TIPO_LOGRADOURO) == 3 ? "Apartamento" : ((entity0.LX_TIPO_LOGRADOURO) == 4 ? "Avenida" : ((entity0.LX_TIPO_LOGRADOURO) == 5 ? "Beco" : ((entity0.LX_TIPO_LOGRADOURO) == 6 ? "Bloco" : ((entity0.LX_TIPO_LOGRADOURO) == 7 ? "Caminho" : ((entity0.LX_TIPO_LOGRADOURO) == 8 ? "Escadinha" : ((entity0.LX_TIPO_LOGRADOURO) == 9 ? "Estação" : ((entity0.LX_TIPO_LOGRADOURO) == 10 ? "Estrada" : ((entity0.LX_TIPO_LOGRADOURO) == 11 ? "Fazenda" : ((entity0.LX_TIPO_LOGRADOURO) == 12 ? "Fortaleza" : ((entity0.LX_TIPO_LOGRADOURO) == 13 ? "Galeria" : ((entity0.LX_TIPO_LOGRADOURO) == 14 ? "Ladeira" : ((entity0.LX_TIPO_LOGRADOURO) == 15 ? "Largo" : ((entity0.LX_TIPO_LOGRADOURO) == 17 ? "Parque" : ((entity0.LX_TIPO_LOGRADOURO) == 16 ? "Praça" : ((entity0.LX_TIPO_LOGRADOURO) == 18 ? "Praia" : ((entity0.LX_TIPO_LOGRADOURO) == 19 ? "Quadra" : ((entity0.LX_TIPO_LOGRADOURO) == 20 ? "Quilômetro" : ((entity0.LX_TIPO_LOGRADOURO) == 21 ? "Quinta" : ((entity0.LX_TIPO_LOGRADOURO) == 22 ? "Rodovia" : ((entity0.LX_TIPO_LOGRADOURO) == 23 ? "Rua" : ((entity0.LX_TIPO_LOGRADOURO) == 24 ? "Super Quadra" : ((entity0.LX_TIPO_LOGRADOURO) == 25 ? "Travessa" : ((entity0.LX_TIPO_LOGRADOURO) == 26 ? "Viaduto" : ((entity0.LX_TIPO_LOGRADOURO) == 27 ? "Vila" : "")))))))))))))))))))))))))))
                , Municipio = entity0.MUNICIPIO
                , NomeAutenticacao = entity0.NOME_AUTENTICACAO
                , NomeCurtoUsuario = entity0.NOME_CURTO_USUARIO
                , NomeUsuario = entity0.NOME_USUARIO
                , Numero = entity0.NUMERO
                , ObsEndereco = entity0.OBS_ENDERECO
                , Ramal = entity0.RAMAL
                , Uf = entity0.UF
                , UidUsuario = entity0.UID_USUARIO
                , VigenciaFinal = entity0.VIGENCIA_FINAL
                , VigenciaInicial = entity0.VIGENCIA_INICIAL
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get NewMessageInfo.
	    public IEnumerable<NewMessageInfo> GetNewMessageInfo()
	    {




	
	        IEnumerable<NewMessageInfo> result = new List<NewMessageInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get NewMessageInfoNoAssociations.
	    public IEnumerable<NewMessageInfo> GetNewMessageInfoNoAssociations()
	    {




	
	        IEnumerable<NewMessageInfo> result = new List<NewMessageInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsMensagemConsulta.
	    public IQueryable<TcsMensagemConsulta> GetTcsMensagemConsulta()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsMensagemConsulta> result = 
	            (from entity0 in this.DbContext.TCS_MENSAGEM
                  let entity0Al1 = entity0.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_USUARIO_AUTENTICACAO
	            
	            	
	            select new TcsMensagemConsulta()		
	            {
	            
                Corpo = entity0.CORPO
                , Criacao = entity0.CRIACAO
                , Envio = entity0.ENVIO
                , Filtro = entity0.FILTRO
                , IdLinx = entity0Al1.ID_LINX
                , IdTcsMensagem = entity0.ID_TCS_MENSAGEM
                , IdUsuario = entity0Al2.ID_USUARIO
                , LxTipoMensagem = entity0.LX_TIPO_MENSAGEM
                , LxTipoMensagemName = ((entity0.LX_TIPO_MENSAGEM) == 3 ? "Erro" : ((entity0.LX_TIPO_MENSAGEM) == 1 ? "Informação" : ((entity0.LX_TIPO_MENSAGEM) == 4 ? "Sucesso" : ((entity0.LX_TIPO_MENSAGEM) == 2 ? "Alerta" : ""))))
                , NomeEmpresa = entity0Al1.NOME_EMPRESA
                , Titulo = entity0.TITULO
                , NomeAutenticacao = entity0Al2.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al2.NOME_USUARIO
			
                ,TcsMensagemConsultaLogList = 
	                        (from entity1 in entity0.TCS_MENSAGEM_LOG_LISTA
                                  let entity1Al1 = entity1.TCS_MENSAGEM
                                  let entity1Al2 = entity1.TCS_USUARIO_AUTENTICACAO
	                        
	                        	
	                        select new TcsMensagemConsultaLog()
	                        {
	                        
                                Dispensada = entity1.DISPENSADA
                                , Entregue = entity1.ENTREGUE
                                , IdTcsMensagem = entity1Al1.ID_TCS_MENSAGEM
                                , IdTcsMensagemLog = entity1.ID_TCS_MENSAGEM_LOG
                                , IdUsuario = entity1Al2.ID_USUARIO
                                , Lida = entity1.LIDA
                                , NomeAutenticacao = entity1Al2.NOME_AUTENTICACAO
                                , NomeUsuario = entity1Al2.NOME_USUARIO
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsMensagemConsultaLog.
	    public IQueryable<TcsMensagemConsultaLog> GetTcsMensagemConsultaLog()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsMensagemConsultaLog> result = 
	            (from entity0 in this.DbContext.TCS_MENSAGEM_LOG
                  let entity0Al1 = entity0.TCS_MENSAGEM
                  let entity0Al2 = entity0.TCS_USUARIO_AUTENTICACAO
	            
	            	
	            select new TcsMensagemConsultaLog()		
	            {
	            
                Dispensada = entity0.DISPENSADA
                , Entregue = entity0.ENTREGUE
                , IdTcsMensagem = entity0Al1.ID_TCS_MENSAGEM
                , IdTcsMensagemLog = entity0.ID_TCS_MENSAGEM_LOG
                , IdUsuario = entity0Al2.ID_USUARIO
                , Lida = entity0.LIDA
                , NomeAutenticacao = entity0Al2.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al2.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsMensagemConsultaNoAssociations.
	    public IQueryable<TcsMensagemConsulta> GetTcsMensagemConsultaNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsMensagemConsulta> result = 
	            (from entity0 in this.DbContext.TCS_MENSAGEM
                  let entity0Al1 = entity0.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_USUARIO_AUTENTICACAO
	            
	            	
	            select new TcsMensagemConsulta()		
	            {
	            
                Corpo = entity0.CORPO
                , Criacao = entity0.CRIACAO
                , Envio = entity0.ENVIO
                , Filtro = entity0.FILTRO
                , IdLinx = entity0Al1.ID_LINX
                , IdTcsMensagem = entity0.ID_TCS_MENSAGEM
                , IdUsuario = entity0Al2.ID_USUARIO
                , LxTipoMensagem = entity0.LX_TIPO_MENSAGEM
                , LxTipoMensagemName = ((entity0.LX_TIPO_MENSAGEM) == 3 ? "Erro" : ((entity0.LX_TIPO_MENSAGEM) == 1 ? "Informação" : ((entity0.LX_TIPO_MENSAGEM) == 4 ? "Sucesso" : ((entity0.LX_TIPO_MENSAGEM) == 2 ? "Alerta" : ""))))
                , NomeEmpresa = entity0Al1.NOME_EMPRESA
                , Titulo = entity0.TITULO
                , NomeAutenticacao = entity0Al2.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al2.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsMensagemConsultaLogNoAssociations.
	    public IQueryable<TcsMensagemConsultaLog> GetTcsMensagemConsultaLogNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsMensagemConsultaLog> result = 
	            (from entity0 in this.DbContext.TCS_MENSAGEM_LOG
                  let entity0Al1 = entity0.TCS_MENSAGEM
                  let entity0Al2 = entity0.TCS_USUARIO_AUTENTICACAO
	            
	            	
	            select new TcsMensagemConsultaLog()		
	            {
	            
                Dispensada = entity0.DISPENSADA
                , Entregue = entity0.ENTREGUE
                , IdTcsMensagem = entity0Al1.ID_TCS_MENSAGEM
                , IdTcsMensagemLog = entity0.ID_TCS_MENSAGEM_LOG
                , IdUsuario = entity0Al2.ID_USUARIO
                , Lida = entity0.LIDA
                , NomeAutenticacao = entity0Al2.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al2.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	//Add filtering disabled property for TCS_MENSAGEM
	    	string[] bmDisabledTcsMensagemList = this.GetEDM().GetFilteringDisabledList("TCS_MENSAGEM");
	    	if (bmDisabledTcsMensagemList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsMensagemList.Contains("TCS_MENSAGEM.CORPO"))
	    		{
	    			result.Add("TcsMensagem|Corpo");
	    			result.Add("TcsMensagem|TCS_MENSAGEM.CORPO");
	    		}
	
	    		if (bmDisabledTcsMensagemList.Contains("TCS_MENSAGEM.CRIACAO"))
	    		{
	    			result.Add("TcsMensagem|Criacao");
	    			result.Add("TcsMensagem|TCS_MENSAGEM.CRIACAO");
	    		}
	
	    		if (bmDisabledTcsMensagemList.Contains("TCS_MENSAGEM.ENVIO"))
	    		{
	    			result.Add("TcsMensagem|Envio");
	    			result.Add("TcsMensagem|TCS_MENSAGEM.ENVIO");
	    		}
	
	    		if (bmDisabledTcsMensagemList.Contains("TCS_MENSAGEM.FILTRO"))
	    		{
	    			result.Add("TcsMensagem|Filtro");
	    			result.Add("TcsMensagem|TCS_MENSAGEM.FILTRO");
	    		}
	
	    		if (bmDisabledTcsMensagemList.Contains("TCS_MENSAGEM.ID_TCS_MENSAGEM"))
	    		{
	    			result.Add("TcsMensagem|IdTcsMensagem");
	    			result.Add("TcsMensagem|TCS_MENSAGEM.ID_TCS_MENSAGEM");
	    		}
	
	    		if (bmDisabledTcsMensagemList.Contains("TCS_MENSAGEM.LX_TIPO_MENSAGEM"))
	    		{
	    			result.Add("TcsMensagem|LxTipoMensagem");
	    			result.Add("TcsMensagem|TCS_MENSAGEM.LX_TIPO_MENSAGEM");
	    		}
	
	    		if (bmDisabledTcsMensagemList.Contains("TCS_MENSAGEM.TITULO"))
	    		{
	    			result.Add("TcsMensagem|Titulo");
	    			result.Add("TcsMensagem|TCS_MENSAGEM.TITULO");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_MENSAGEM_LOG
	    	string[] bmDisabledTcsMensagemUsuarioList = this.GetEDM().GetFilteringDisabledList("TCS_MENSAGEM_LOG");
	    	if (bmDisabledTcsMensagemUsuarioList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsMensagemUsuarioList.Contains("TCS_MENSAGEM_LOG.DISPENSADA"))
	    		{
	    			result.Add("TcsMensagemUsuario|Dispensada");
	    			result.Add("TcsMensagemUsuario|TCS_MENSAGEM_LOG.DISPENSADA");
	    		}
	
	    		if (bmDisabledTcsMensagemUsuarioList.Contains("TCS_MENSAGEM_LOG.ENTREGUE"))
	    		{
	    			result.Add("TcsMensagemUsuario|Entregue");
	    			result.Add("TcsMensagemUsuario|TCS_MENSAGEM_LOG.ENTREGUE");
	    		}
	
	    		if (bmDisabledTcsMensagemUsuarioList.Contains("TCS_MENSAGEM_LOG.ID_TCS_MENSAGEM_LOG"))
	    		{
	    			result.Add("TcsMensagemUsuario|IdTcsMensagemLog");
	    			result.Add("TcsMensagemUsuario|TCS_MENSAGEM_LOG.ID_TCS_MENSAGEM_LOG");
	    		}
	
	    		if (bmDisabledTcsMensagemUsuarioList.Contains("TCS_MENSAGEM_LOG.LIDA"))
	    		{
	    			result.Add("TcsMensagemUsuario|Lida");
	    			result.Add("TcsMensagemUsuario|TCS_MENSAGEM_LOG.LIDA");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_MENSAGEM_LOG
	    	string[] bmDisabledTcsMensagemLogList = this.GetEDM().GetFilteringDisabledList("TCS_MENSAGEM_LOG");
	    	if (bmDisabledTcsMensagemLogList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsMensagemLogList.Contains("TCS_MENSAGEM_LOG.DISPENSADA"))
	    		{
	    			result.Add("TcsMensagemLog|Dispensada");
	    			result.Add("TcsMensagemLog|TCS_MENSAGEM_LOG.DISPENSADA");
	    		}
	
	    		if (bmDisabledTcsMensagemLogList.Contains("TCS_MENSAGEM_LOG.ENTREGUE"))
	    		{
	    			result.Add("TcsMensagemLog|Entregue");
	    			result.Add("TcsMensagemLog|TCS_MENSAGEM_LOG.ENTREGUE");
	    		}
	
	    		if (bmDisabledTcsMensagemLogList.Contains("TCS_MENSAGEM_LOG.ID_TCS_MENSAGEM_LOG"))
	    		{
	    			result.Add("TcsMensagemLog|IdTcsMensagemLog");
	    			result.Add("TcsMensagemLog|TCS_MENSAGEM_LOG.ID_TCS_MENSAGEM_LOG");
	    		}
	
	    		if (bmDisabledTcsMensagemLogList.Contains("TCS_MENSAGEM_LOG.LIDA"))
	    		{
	    			result.Add("TcsMensagemLog|Lida");
	    			result.Add("TcsMensagemLog|TCS_MENSAGEM_LOG.LIDA");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_USUARIO_AUTENTICACAO
	    	string[] bmDisabledTcsUsuarioList = this.GetEDM().GetFilteringDisabledList("TCS_USUARIO_AUTENTICACAO");
	    	if (bmDisabledTcsUsuarioList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO_AUTENTICACAO.AUTENTICACAO_WINDOWS"))
	    		{
	    			result.Add("TcsUsuario|AutenticacaoWindows");
	    			result.Add("TcsUsuario|TCS_USUARIO_AUTENTICACAO.AUTENTICACAO_WINDOWS");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO_AUTENTICACAO.BAIRRO"))
	    		{
	    			result.Add("TcsUsuario|Bairro");
	    			result.Add("TcsUsuario|TCS_USUARIO_AUTENTICACAO.BAIRRO");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO_AUTENTICACAO.CEP"))
	    		{
	    			result.Add("TcsUsuario|Cep");
	    			result.Add("TcsUsuario|TCS_USUARIO_AUTENTICACAO.CEP");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO_AUTENTICACAO.CNPJ_CPF"))
	    		{
	    			result.Add("TcsUsuario|CnpjCpf");
	    			result.Add("TcsUsuario|TCS_USUARIO_AUTENTICACAO.CNPJ_CPF");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO_AUTENTICACAO.COMPLEMENTO"))
	    		{
	    			result.Add("TcsUsuario|Complemento");
	    			result.Add("TcsUsuario|TCS_USUARIO_AUTENTICACAO.COMPLEMENTO");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO_AUTENTICACAO.DATA_ALTERACAO"))
	    		{
	    			result.Add("TcsUsuario|DataAlteracao");
	    			result.Add("TcsUsuario|TCS_USUARIO_AUTENTICACAO.DATA_ALTERACAO");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO_AUTENTICACAO.DATA_CADASTRO"))
	    		{
	    			result.Add("TcsUsuario|DataCadastro");
	    			result.Add("TcsUsuario|TCS_USUARIO_AUTENTICACAO.DATA_CADASTRO");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO_AUTENTICACAO.DATA_EXPIRACAO_SENHA"))
	    		{
	    			result.Add("TcsUsuario|DataExpiracaoSenha");
	    			result.Add("TcsUsuario|TCS_USUARIO_AUTENTICACAO.DATA_EXPIRACAO_SENHA");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO_AUTENTICACAO.EMAIL"))
	    		{
	    			result.Add("TcsUsuario|Email");
	    			result.Add("TcsUsuario|TCS_USUARIO_AUTENTICACAO.EMAIL");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO_AUTENTICACAO.FONE_CELULAR"))
	    		{
	    			result.Add("TcsUsuario|FoneCelular");
	    			result.Add("TcsUsuario|TCS_USUARIO_AUTENTICACAO.FONE_CELULAR");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO_AUTENTICACAO.FONE_FIXO"))
	    		{
	    			result.Add("TcsUsuario|FoneFixo");
	    			result.Add("TcsUsuario|TCS_USUARIO_AUTENTICACAO.FONE_FIXO");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO_AUTENTICACAO.ID_USUARIO"))
	    		{
	    			result.Add("TcsUsuario|IdUsuario");
	    			result.Add("TcsUsuario|TCS_USUARIO_AUTENTICACAO.ID_USUARIO");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO_AUTENTICACAO.INATIVO"))
	    		{
	    			result.Add("TcsUsuario|Inativo");
	    			result.Add("TcsUsuario|TCS_USUARIO_AUTENTICACAO.INATIVO");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO_AUTENTICACAO.INDICA_ACESSO_SUPORTE"))
	    		{
	    			result.Add("TcsUsuario|IndicaAcessoSuporte");
	    			result.Add("TcsUsuario|TCS_USUARIO_AUTENTICACAO.INDICA_ACESSO_SUPORTE");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO_AUTENTICACAO.INSCR_ESTADUAL_RG"))
	    		{
	    			result.Add("TcsUsuario|InscrEstadualRg");
	    			result.Add("TcsUsuario|TCS_USUARIO_AUTENTICACAO.INSCR_ESTADUAL_RG");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO_AUTENTICACAO.LOGRADOURO"))
	    		{
	    			result.Add("TcsUsuario|Logradouro");
	    			result.Add("TcsUsuario|TCS_USUARIO_AUTENTICACAO.LOGRADOURO");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO_AUTENTICACAO.LX_PFJ_FISICA_JURIDICA"))
	    		{
	    			result.Add("TcsUsuario|LxPfjFisicaJuridica");
	    			result.Add("TcsUsuario|TCS_USUARIO_AUTENTICACAO.LX_PFJ_FISICA_JURIDICA");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO"))
	    		{
	    			result.Add("TcsUsuario|LxTipoLogradouro");
	    			result.Add("TcsUsuario|TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO_AUTENTICACAO.MUNICIPIO"))
	    		{
	    			result.Add("TcsUsuario|Municipio");
	    			result.Add("TcsUsuario|TCS_USUARIO_AUTENTICACAO.MUNICIPIO");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO"))
	    		{
	    			result.Add("TcsUsuario|NomeAutenticacao");
	    			result.Add("TcsUsuario|TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO_AUTENTICACAO.NOME_CURTO_USUARIO"))
	    		{
	    			result.Add("TcsUsuario|NomeCurtoUsuario");
	    			result.Add("TcsUsuario|TCS_USUARIO_AUTENTICACAO.NOME_CURTO_USUARIO");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO_AUTENTICACAO.NOME_USUARIO"))
	    		{
	    			result.Add("TcsUsuario|NomeUsuario");
	    			result.Add("TcsUsuario|TCS_USUARIO_AUTENTICACAO.NOME_USUARIO");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO_AUTENTICACAO.NUMERO"))
	    		{
	    			result.Add("TcsUsuario|Numero");
	    			result.Add("TcsUsuario|TCS_USUARIO_AUTENTICACAO.NUMERO");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO_AUTENTICACAO.OBS_ENDERECO"))
	    		{
	    			result.Add("TcsUsuario|ObsEndereco");
	    			result.Add("TcsUsuario|TCS_USUARIO_AUTENTICACAO.OBS_ENDERECO");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO_AUTENTICACAO.RAMAL"))
	    		{
	    			result.Add("TcsUsuario|Ramal");
	    			result.Add("TcsUsuario|TCS_USUARIO_AUTENTICACAO.RAMAL");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO_AUTENTICACAO.UF"))
	    		{
	    			result.Add("TcsUsuario|Uf");
	    			result.Add("TcsUsuario|TCS_USUARIO_AUTENTICACAO.UF");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO_AUTENTICACAO.UID_USUARIO"))
	    		{
	    			result.Add("TcsUsuario|UidUsuario");
	    			result.Add("TcsUsuario|TCS_USUARIO_AUTENTICACAO.UID_USUARIO");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO_AUTENTICACAO.VIGENCIA_FINAL"))
	    		{
	    			result.Add("TcsUsuario|VigenciaFinal");
	    			result.Add("TcsUsuario|TCS_USUARIO_AUTENTICACAO.VIGENCIA_FINAL");
	    		}
	
	    		if (bmDisabledTcsUsuarioList.Contains("TCS_USUARIO_AUTENTICACAO.VIGENCIA_INICIAL"))
	    		{
	    			result.Add("TcsUsuario|VigenciaInicial");
	    			result.Add("TcsUsuario|TCS_USUARIO_AUTENTICACAO.VIGENCIA_INICIAL");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_MENSAGEM_LOG
	    	string[] bmDisabledTcsMensagemLogDetailList = this.GetEDM().GetFilteringDisabledList("TCS_MENSAGEM_LOG");
	    	if (bmDisabledTcsMensagemLogDetailList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsMensagemLogDetailList.Contains("TCS_MENSAGEM_LOG.DISPENSADA"))
	    		{
	    			result.Add("TcsMensagemLogDetail|Dispensada");
	    			result.Add("TcsMensagemLogDetail|TCS_MENSAGEM_LOG.DISPENSADA");
	    		}
	
	    		if (bmDisabledTcsMensagemLogDetailList.Contains("TCS_MENSAGEM_LOG.ENTREGUE"))
	    		{
	    			result.Add("TcsMensagemLogDetail|Entregue");
	    			result.Add("TcsMensagemLogDetail|TCS_MENSAGEM_LOG.ENTREGUE");
	    		}
	
	    		if (bmDisabledTcsMensagemLogDetailList.Contains("TCS_MENSAGEM_LOG.ID_TCS_MENSAGEM_LOG"))
	    		{
	    			result.Add("TcsMensagemLogDetail|IdTcsMensagemLog");
	    			result.Add("TcsMensagemLogDetail|TCS_MENSAGEM_LOG.ID_TCS_MENSAGEM_LOG");
	    		}
	
	    		if (bmDisabledTcsMensagemLogDetailList.Contains("TCS_MENSAGEM_LOG.LIDA"))
	    		{
	    			result.Add("TcsMensagemLogDetail|Lida");
	    			result.Add("TcsMensagemLogDetail|TCS_MENSAGEM_LOG.LIDA");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_MENSAGEM
	    	string[] bmDisabledTcsMensagemConsultaList = this.GetEDM().GetFilteringDisabledList("TCS_MENSAGEM");
	    	if (bmDisabledTcsMensagemConsultaList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsMensagemConsultaList.Contains("TCS_MENSAGEM.CORPO"))
	    		{
	    			result.Add("TcsMensagemConsulta|Corpo");
	    			result.Add("TcsMensagemConsulta|TCS_MENSAGEM.CORPO");
	    		}
	
	    		if (bmDisabledTcsMensagemConsultaList.Contains("TCS_MENSAGEM.CRIACAO"))
	    		{
	    			result.Add("TcsMensagemConsulta|Criacao");
	    			result.Add("TcsMensagemConsulta|TCS_MENSAGEM.CRIACAO");
	    		}
	
	    		if (bmDisabledTcsMensagemConsultaList.Contains("TCS_MENSAGEM.ENVIO"))
	    		{
	    			result.Add("TcsMensagemConsulta|Envio");
	    			result.Add("TcsMensagemConsulta|TCS_MENSAGEM.ENVIO");
	    		}
	
	    		if (bmDisabledTcsMensagemConsultaList.Contains("TCS_MENSAGEM.FILTRO"))
	    		{
	    			result.Add("TcsMensagemConsulta|Filtro");
	    			result.Add("TcsMensagemConsulta|TCS_MENSAGEM.FILTRO");
	    		}
	
	    		if (bmDisabledTcsMensagemConsultaList.Contains("TCS_MENSAGEM.ID_TCS_MENSAGEM"))
	    		{
	    			result.Add("TcsMensagemConsulta|IdTcsMensagem");
	    			result.Add("TcsMensagemConsulta|TCS_MENSAGEM.ID_TCS_MENSAGEM");
	    		}
	
	    		if (bmDisabledTcsMensagemConsultaList.Contains("TCS_MENSAGEM.LX_TIPO_MENSAGEM"))
	    		{
	    			result.Add("TcsMensagemConsulta|LxTipoMensagem");
	    			result.Add("TcsMensagemConsulta|TCS_MENSAGEM.LX_TIPO_MENSAGEM");
	    		}
	
	    		if (bmDisabledTcsMensagemConsultaList.Contains("TCS_MENSAGEM.TITULO"))
	    		{
	    			result.Add("TcsMensagemConsulta|Titulo");
	    			result.Add("TcsMensagemConsulta|TCS_MENSAGEM.TITULO");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_MENSAGEM_LOG
	    	string[] bmDisabledTcsMensagemConsultaLogList = this.GetEDM().GetFilteringDisabledList("TCS_MENSAGEM_LOG");
	    	if (bmDisabledTcsMensagemConsultaLogList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsMensagemConsultaLogList.Contains("TCS_MENSAGEM_LOG.DISPENSADA"))
	    		{
	    			result.Add("TcsMensagemConsultaLog|Dispensada");
	    			result.Add("TcsMensagemConsultaLog|TCS_MENSAGEM_LOG.DISPENSADA");
	    		}
	
	    		if (bmDisabledTcsMensagemConsultaLogList.Contains("TCS_MENSAGEM_LOG.ENTREGUE"))
	    		{
	    			result.Add("TcsMensagemConsultaLog|Entregue");
	    			result.Add("TcsMensagemConsultaLog|TCS_MENSAGEM_LOG.ENTREGUE");
	    		}
	
	    		if (bmDisabledTcsMensagemConsultaLogList.Contains("TCS_MENSAGEM_LOG.ID_TCS_MENSAGEM_LOG"))
	    		{
	    			result.Add("TcsMensagemConsultaLog|IdTcsMensagemLog");
	    			result.Add("TcsMensagemConsultaLog|TCS_MENSAGEM_LOG.ID_TCS_MENSAGEM_LOG");
	    		}
	
	    		if (bmDisabledTcsMensagemConsultaLogList.Contains("TCS_MENSAGEM_LOG.LIDA"))
	    		{
	    			result.Add("TcsMensagemConsultaLog|Lida");
	    			result.Add("TcsMensagemConsultaLog|TCS_MENSAGEM_LOG.LIDA");
	    		}
	    	}
	    	return result;
	    }

	    #endregion Excluded Filters

	    #region Get By EntitySearchId Methods Definitions.
	
				
	    [Ignore]
	    //Get TcsMensagem By EntitySearchId.
	    public IQueryable<TcsMensagem> GetTcsMensagemByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsMensagemByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsMensagemLogDetail By EntitySearchId.
	    public IQueryable<TcsMensagemLogDetail> GetTcsMensagemLogDetailByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsMensagemLogDetailByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsMensagem By EntitySearchId.
	    public IQueryable<TcsMensagem> GetTcsMensagemByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsMensagemByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsMensagemLogDetail By EntitySearchId.
	    public IQueryable<TcsMensagemLogDetail> GetTcsMensagemLogDetailByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsMensagemLogDetailByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get MensagemInfo By EntitySearchId.
	    public IEnumerable<MensagemInfo> GetMensagemInfoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetMensagemInfoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get MensagemInfo By EntitySearchId.
	    public IEnumerable<MensagemInfo> GetMensagemInfoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetMensagemInfoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsMensagemUsuario By EntitySearchId.
	    public IQueryable<TcsMensagemUsuario> GetTcsMensagemUsuarioByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsMensagemUsuarioByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsMensagemUsuario By EntitySearchId.
	    public IQueryable<TcsMensagemUsuario> GetTcsMensagemUsuarioByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsMensagemUsuarioByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsMensagemLog By EntitySearchId.
	    public IQueryable<TcsMensagemLog> GetTcsMensagemLogByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsMensagemLogByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsMensagemLog By EntitySearchId.
	    public IQueryable<TcsMensagemLog> GetTcsMensagemLogByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsMensagemLogByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsPerfil By EntitySearchId.
	    public IQueryable<TcsPerfil> GetTcsPerfilByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsPerfilByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsPerfil By EntitySearchId.
	    public IQueryable<TcsPerfil> GetTcsPerfilByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsPerfilByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuario By EntitySearchId.
	    public IQueryable<TcsUsuario> GetTcsUsuarioByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuario By EntitySearchId.
	    public IQueryable<TcsUsuario> GetTcsUsuarioByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get NewMessageInfo By EntitySearchId.
	    public IEnumerable<NewMessageInfo> GetNewMessageInfoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetNewMessageInfoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get NewMessageInfo By EntitySearchId.
	    public IEnumerable<NewMessageInfo> GetNewMessageInfoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetNewMessageInfoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsMensagemConsulta By EntitySearchId.
	    public IQueryable<TcsMensagemConsulta> GetTcsMensagemConsultaByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsMensagemConsultaByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsMensagemConsultaLog By EntitySearchId.
	    public IQueryable<TcsMensagemConsultaLog> GetTcsMensagemConsultaLogByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsMensagemConsultaLogByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsMensagemConsulta By EntitySearchId.
	    public IQueryable<TcsMensagemConsulta> GetTcsMensagemConsultaByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsMensagemConsultaByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsMensagemConsultaLog By EntitySearchId.
	    public IQueryable<TcsMensagemConsultaLog> GetTcsMensagemConsultaLogByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsMensagemConsultaLogByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get TcsMensagem By Example.
	    [Ignore]
	    public IQueryable<TcsMensagem> GetTcsMensagemByExample(TcsMensagem entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsMensagemByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsMensagemLogDetail By Example.
	    [Ignore]
	    public IQueryable<TcsMensagemLogDetail> GetTcsMensagemLogDetailByExample(TcsMensagemLogDetail entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsMensagemLogDetailByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsMensagem By Example.
	    [Ignore]
	    public IQueryable<TcsMensagem> GetTcsMensagemByExampleNoAssociations(TcsMensagem entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsMensagemByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsMensagemLogDetail By Example.
	    [Ignore]
	    public IQueryable<TcsMensagemLogDetail> GetTcsMensagemLogDetailByExampleNoAssociations(TcsMensagemLogDetail entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsMensagemLogDetailByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get MensagemInfo By Example.
	    [Ignore]
	    public IEnumerable<MensagemInfo> GetMensagemInfoByExample(MensagemInfo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetMensagemInfoByEntitySearch(queryAnalysis);
	    }
			
	    //Get MensagemInfo By Example.
	    [Ignore]
	    public IEnumerable<MensagemInfo> GetMensagemInfoByExampleNoAssociations(MensagemInfo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetMensagemInfoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsMensagemUsuario By Example.
	    [Ignore]
	    public IQueryable<TcsMensagemUsuario> GetTcsMensagemUsuarioByExample(TcsMensagemUsuario entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsMensagemUsuarioByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsMensagemUsuario By Example.
	    [Ignore]
	    public IQueryable<TcsMensagemUsuario> GetTcsMensagemUsuarioByExampleNoAssociations(TcsMensagemUsuario entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsMensagemUsuarioByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsMensagemLog By Example.
	    [Ignore]
	    public IQueryable<TcsMensagemLog> GetTcsMensagemLogByExample(TcsMensagemLog entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsMensagemLogByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsMensagemLog By Example.
	    [Ignore]
	    public IQueryable<TcsMensagemLog> GetTcsMensagemLogByExampleNoAssociations(TcsMensagemLog entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsMensagemLogByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsPerfil By Example.
	    [Ignore]
	    public IQueryable<TcsPerfil> GetTcsPerfilByExample(TcsPerfil entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsPerfilByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsPerfil By Example.
	    [Ignore]
	    public IQueryable<TcsPerfil> GetTcsPerfilByExampleNoAssociations(TcsPerfil entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsPerfilByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsUsuario By Example.
	    [Ignore]
	    public IQueryable<TcsUsuario> GetTcsUsuarioByExample(TcsUsuario entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsUsuario By Example.
	    [Ignore]
	    public IQueryable<TcsUsuario> GetTcsUsuarioByExampleNoAssociations(TcsUsuario entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get NewMessageInfo By Example.
	    [Ignore]
	    public IEnumerable<NewMessageInfo> GetNewMessageInfoByExample(NewMessageInfo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetNewMessageInfoByEntitySearch(queryAnalysis);
	    }
			
	    //Get NewMessageInfo By Example.
	    [Ignore]
	    public IEnumerable<NewMessageInfo> GetNewMessageInfoByExampleNoAssociations(NewMessageInfo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetNewMessageInfoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsMensagemConsulta By Example.
	    [Ignore]
	    public IQueryable<TcsMensagemConsulta> GetTcsMensagemConsultaByExample(TcsMensagemConsulta entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsMensagemConsultaByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsMensagemConsultaLog By Example.
	    [Ignore]
	    public IQueryable<TcsMensagemConsultaLog> GetTcsMensagemConsultaLogByExample(TcsMensagemConsultaLog entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsMensagemConsultaLogByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsMensagemConsulta By Example.
	    [Ignore]
	    public IQueryable<TcsMensagemConsulta> GetTcsMensagemConsultaByExampleNoAssociations(TcsMensagemConsulta entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsMensagemConsultaByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsMensagemConsultaLog By Example.
	    [Ignore]
	    public IQueryable<TcsMensagemConsultaLog> GetTcsMensagemConsultaLogByExampleNoAssociations(TcsMensagemConsultaLog entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsMensagemConsultaLogByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public TcsMensagem GetTcsMensagemByKey(Int64 idTcsMensagem)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsMensagem");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsMensagem"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsMensagem));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsMensagemByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public MensagemInfo GetMensagemInfoByKey(Int64 idTcsMensagemLog)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("MensagemInfo");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsMensagemLog"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsMensagemLog));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetMensagemInfoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsMensagemUsuario GetTcsMensagemUsuarioByKey(Int64 idTcsMensagemLog)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsMensagemUsuario");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsMensagemLog"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsMensagemLog));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsMensagemUsuarioByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsMensagemLog GetTcsMensagemLogByKey(Int64 idTcsMensagemLog)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsMensagemLog");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsMensagemLog"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsMensagemLog));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsMensagemLogByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsPerfil GetTcsPerfilByKey(Int64 idTcsUsuarioPerfil)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsPerfil");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsUsuarioPerfil"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsUsuarioPerfil));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsPerfilByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsUsuario GetTcsUsuarioByKey(Int64 idUsuario)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsUsuario");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuario"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idUsuario));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsUsuarioByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public NewMessageInfo GetNewMessageInfoByKey(Int32 idLinx)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("NewMessageInfo");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdLinx"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idLinx));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetNewMessageInfoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsMensagemLogDetail GetTcsMensagemLogDetailByKey(Int64 idTcsMensagemLog)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsMensagemLogDetail");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsMensagemLog"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsMensagemLog));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsMensagemLogDetailByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsMensagemConsulta GetTcsMensagemConsultaByKey(Int64 idTcsMensagem)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsMensagemConsulta");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsMensagem"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsMensagem));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsMensagemConsultaByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsMensagemConsultaLog GetTcsMensagemConsultaLogByKey(Int64 idTcsMensagemLog)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsMensagemConsultaLog");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsMensagemLog"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsMensagemLog));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsMensagemConsultaLogByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get TcsMensagemByEntitySearch.
	    public IQueryable<TcsMensagem> GetTcsMensagemByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsMensagem));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsMensagem> result = 
	            (from entity0 in this.DbContext.TCS_MENSAGEM.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_USUARIO_AUTENTICACAO
	            
	            	
	            select new TcsMensagem()		
	            {
	            
                Corpo = entity0.CORPO
                , Criacao = entity0.CRIACAO
                , Envio = entity0.ENVIO
                , Filtro = entity0.FILTRO
                , IdLinx = entity0Al1.ID_LINX
                , IdTcsMensagem = entity0.ID_TCS_MENSAGEM
                , IdUsuario = entity0Al2.ID_USUARIO
                , LxTipoMensagem = entity0.LX_TIPO_MENSAGEM
                , LxTipoMensagemName = ((entity0.LX_TIPO_MENSAGEM) == 3 ? "Erro" : ((entity0.LX_TIPO_MENSAGEM) == 1 ? "Informação" : ((entity0.LX_TIPO_MENSAGEM) == 4 ? "Sucesso" : ((entity0.LX_TIPO_MENSAGEM) == 2 ? "Alerta" : ""))))
                , Titulo = entity0.TITULO
			
                ,TcsMensagemLogDetailList = 
	                        (from entity1 in entity0.TCS_MENSAGEM_LOG_LISTA
                                  let entity1Al1 = entity1.TCS_MENSAGEM
                                  let entity1Al2 = entity1.TCS_USUARIO_AUTENTICACAO
	                        
	                        	
	                        select new TcsMensagemLogDetail()
	                        {
	                        
                                Dispensada = entity1.DISPENSADA
                                , Entregue = entity1.ENTREGUE
                                , IdTcsMensagem = entity1Al1.ID_TCS_MENSAGEM
                                , IdTcsMensagemLog = entity1.ID_TCS_MENSAGEM_LOG
                                , IdUsuario = entity1Al2.ID_USUARIO
                                , Lida = entity1.LIDA
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsMensagemLogDetailByEntitySearch.
	    public IQueryable<TcsMensagemLogDetail> GetTcsMensagemLogDetailByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsMensagemLogDetail));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsMensagemLogDetail> result = 
	            (from entity0 in this.DbContext.TCS_MENSAGEM_LOG.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_MENSAGEM
                  let entity0Al2 = entity0.TCS_USUARIO_AUTENTICACAO
	            
	            	
	            select new TcsMensagemLogDetail()		
	            {
	            
                Dispensada = entity0.DISPENSADA
                , Entregue = entity0.ENTREGUE
                , IdTcsMensagem = entity0Al1.ID_TCS_MENSAGEM
                , IdTcsMensagemLog = entity0.ID_TCS_MENSAGEM_LOG
                , IdUsuario = entity0Al2.ID_USUARIO
                , Lida = entity0.LIDA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsMensagemByEntitySearchNoAssociations.
	    public IQueryable<TcsMensagem> GetTcsMensagemByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsMensagem));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsMensagem> result = 
	            (from entity0 in this.DbContext.TCS_MENSAGEM.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_USUARIO_AUTENTICACAO
	            
	            	
	            select new TcsMensagem()		
	            {
	            
                Corpo = entity0.CORPO
                , Criacao = entity0.CRIACAO
                , Envio = entity0.ENVIO
                , Filtro = entity0.FILTRO
                , IdLinx = entity0Al1.ID_LINX
                , IdTcsMensagem = entity0.ID_TCS_MENSAGEM
                , IdUsuario = entity0Al2.ID_USUARIO
                , LxTipoMensagem = entity0.LX_TIPO_MENSAGEM
                , LxTipoMensagemName = ((entity0.LX_TIPO_MENSAGEM) == 3 ? "Erro" : ((entity0.LX_TIPO_MENSAGEM) == 1 ? "Informação" : ((entity0.LX_TIPO_MENSAGEM) == 4 ? "Sucesso" : ((entity0.LX_TIPO_MENSAGEM) == 2 ? "Alerta" : ""))))
                , Titulo = entity0.TITULO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsMensagemLogDetailByEntitySearchNoAssociations.
	    public IQueryable<TcsMensagemLogDetail> GetTcsMensagemLogDetailByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsMensagemLogDetail));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsMensagemLogDetail> result = 
	            (from entity0 in this.DbContext.TCS_MENSAGEM_LOG.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_MENSAGEM
                  let entity0Al2 = entity0.TCS_USUARIO_AUTENTICACAO
	            
	            	
	            select new TcsMensagemLogDetail()		
	            {
	            
                Dispensada = entity0.DISPENSADA
                , Entregue = entity0.ENTREGUE
                , IdTcsMensagem = entity0Al1.ID_TCS_MENSAGEM
                , IdTcsMensagemLog = entity0.ID_TCS_MENSAGEM_LOG
                , IdUsuario = entity0Al2.ID_USUARIO
                , Lida = entity0.LIDA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsMensagemLogDetailParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsMensagemLogDetailParentComposition> GetTcsMensagemLogDetailParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "TCS_MENSAGEM", "TCS_MENSAGEM_LOG", "TCS_MENSAGEM", typeof(TcsMensagemLogDetailParentComposition));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsMensagemLogDetailParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_MENSAGEM_LOG.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_MENSAGEM
                  let entity0Al2 = entity0.TCS_USUARIO_AUTENTICACAO
	            
	            	
	            select new TcsMensagemLogDetailParentComposition()		
	            {
	            
                Dispensada = entity0.DISPENSADA
                , Entregue = entity0.ENTREGUE
                , IdTcsMensagem = entity0Al1.ID_TCS_MENSAGEM
                , IdTcsMensagemLog = entity0.ID_TCS_MENSAGEM_LOG
                , IdUsuario = entity0Al2.ID_USUARIO
                , Lida = entity0.LIDA
                //TcsMensagem Properties.
                , Corpo = entity0.TCS_MENSAGEM.CORPO
                , Criacao = entity0.TCS_MENSAGEM.CRIACAO
                , Envio = entity0.TCS_MENSAGEM.ENVIO
                , Filtro = entity0.TCS_MENSAGEM.FILTRO
                , IdLinx = entity0.TCS_MENSAGEM.TCS_EMPRESA_AUTENTICACAO.ID_LINX
                , LxTipoMensagem = entity0.TCS_MENSAGEM.LX_TIPO_MENSAGEM
                , LxTipoMensagemName = ((entity0.TCS_MENSAGEM.LX_TIPO_MENSAGEM) == 3 ? "Erro" : ((entity0.TCS_MENSAGEM.LX_TIPO_MENSAGEM) == 1 ? "Informação" : ((entity0.TCS_MENSAGEM.LX_TIPO_MENSAGEM) == 4 ? "Sucesso" : ((entity0.TCS_MENSAGEM.LX_TIPO_MENSAGEM) == 2 ? "Alerta" : ""))))
                , Titulo = entity0.TCS_MENSAGEM.TITULO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get MensagemInfoByEntitySearch.
	    public IEnumerable<MensagemInfo> GetMensagemInfoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<MensagemInfo> result = new List<MensagemInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get MensagemInfoByEntitySearchNoAssociations.
	    public IEnumerable<MensagemInfo> GetMensagemInfoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<MensagemInfo> result = new List<MensagemInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsMensagemUsuarioByEntitySearch.
	    public IQueryable<TcsMensagemUsuario> GetTcsMensagemUsuarioByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsMensagemUsuario));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsMensagemUsuario> result = 
	            (from entity0 in this.DbContext.TCS_MENSAGEM_LOG.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_MENSAGEM
                  let entity0Al3 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_MENSAGEM.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsMensagemUsuario()		
	            {
	            
                Corpo = entity0Al1.CORPO
                , Dispensada = entity0.DISPENSADA
                , Entregue = entity0.ENTREGUE
                , Envio = entity0Al1.ENVIO
                , IdLinx = entity0Al2.ID_LINX
                , IdTcsMensagem = entity0Al1.ID_TCS_MENSAGEM
                , IdTcsMensagemLog = entity0.ID_TCS_MENSAGEM_LOG
                , IdUsuario = entity0Al3.ID_USUARIO
                , Lida = entity0.LIDA
                , LxTipoMensagem = entity0Al1.LX_TIPO_MENSAGEM
                , LxTipoMensagemName = ((entity0Al1.LX_TIPO_MENSAGEM) == 3 ? "Erro" : ((entity0Al1.LX_TIPO_MENSAGEM) == 1 ? "Informação" : ((entity0Al1.LX_TIPO_MENSAGEM) == 4 ? "Sucesso" : ((entity0Al1.LX_TIPO_MENSAGEM) == 2 ? "Alerta" : ""))))
                , Titulo = entity0Al1.TITULO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsMensagemUsuarioByEntitySearchNoAssociations.
	    public IQueryable<TcsMensagemUsuario> GetTcsMensagemUsuarioByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsMensagemUsuario));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsMensagemUsuario> result = 
	            (from entity0 in this.DbContext.TCS_MENSAGEM_LOG.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_MENSAGEM
                  let entity0Al3 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_MENSAGEM.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsMensagemUsuario()		
	            {
	            
                Corpo = entity0Al1.CORPO
                , Dispensada = entity0.DISPENSADA
                , Entregue = entity0.ENTREGUE
                , Envio = entity0Al1.ENVIO
                , IdLinx = entity0Al2.ID_LINX
                , IdTcsMensagem = entity0Al1.ID_TCS_MENSAGEM
                , IdTcsMensagemLog = entity0.ID_TCS_MENSAGEM_LOG
                , IdUsuario = entity0Al3.ID_USUARIO
                , Lida = entity0.LIDA
                , LxTipoMensagem = entity0Al1.LX_TIPO_MENSAGEM
                , LxTipoMensagemName = ((entity0Al1.LX_TIPO_MENSAGEM) == 3 ? "Erro" : ((entity0Al1.LX_TIPO_MENSAGEM) == 1 ? "Informação" : ((entity0Al1.LX_TIPO_MENSAGEM) == 4 ? "Sucesso" : ((entity0Al1.LX_TIPO_MENSAGEM) == 2 ? "Alerta" : ""))))
                , Titulo = entity0Al1.TITULO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsMensagemLogByEntitySearch.
	    public IQueryable<TcsMensagemLog> GetTcsMensagemLogByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsMensagemLog));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsMensagemLog> result = 
	            (from entity0 in this.DbContext.TCS_MENSAGEM_LOG.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_MENSAGEM
                  let entity0Al2 = entity0.TCS_USUARIO_AUTENTICACAO
	            
	            	
	            select new TcsMensagemLog()		
	            {
	            
                Dispensada = entity0.DISPENSADA
                , Entregue = entity0.ENTREGUE
                , IdTcsMensagem = entity0Al1.ID_TCS_MENSAGEM
                , IdTcsMensagemLog = entity0.ID_TCS_MENSAGEM_LOG
                , IdUsuario = entity0Al2.ID_USUARIO
                , Lida = entity0.LIDA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsMensagemLogByEntitySearchNoAssociations.
	    public IQueryable<TcsMensagemLog> GetTcsMensagemLogByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsMensagemLog));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsMensagemLog> result = 
	            (from entity0 in this.DbContext.TCS_MENSAGEM_LOG.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_MENSAGEM
                  let entity0Al2 = entity0.TCS_USUARIO_AUTENTICACAO
	            
	            	
	            select new TcsMensagemLog()		
	            {
	            
                Dispensada = entity0.DISPENSADA
                , Entregue = entity0.ENTREGUE
                , IdTcsMensagem = entity0Al1.ID_TCS_MENSAGEM
                , IdTcsMensagemLog = entity0.ID_TCS_MENSAGEM_LOG
                , IdUsuario = entity0Al2.ID_USUARIO
                , Lida = entity0.LIDA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsPerfilByEntitySearch.
	    public IQueryable<TcsPerfil> GetTcsPerfilByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsPerfil));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		

	        IQueryable<TcsPerfil> result = 
	            (from entity0 in TcsPerfil.GetBusinessView(this, dynQuery, parameters.ToArray()) select entity0);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsPerfilByEntitySearchNoAssociations.
	    public IQueryable<TcsPerfil> GetTcsPerfilByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsPerfil));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		

	        IQueryable<TcsPerfil> result = 
	            (from entity0 in TcsPerfil.GetBusinessView(this, dynQuery, parameters.ToArray()) select entity0);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioByEntitySearch.
	    public IQueryable<TcsUsuario> GetTcsUsuarioByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuario));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuario> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_AUTENTICACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsUsuario()		
	            {
	            
                AutenticacaoWindows = entity0.AUTENTICACAO_WINDOWS
                , Bairro = entity0.BAIRRO
                , Cep = entity0.CEP
                , CnpjCpf = entity0.CNPJ_CPF
                , Complemento = entity0.COMPLEMENTO
                , DataAlteracao = entity0.DATA_ALTERACAO
                , DataCadastro = entity0.DATA_CADASTRO
                , DataExpiracaoSenha = entity0.DATA_EXPIRACAO_SENHA
                , Email = entity0.EMAIL
                , FoneCelular = entity0.FONE_CELULAR
                , FoneFixo = entity0.FONE_FIXO
                , IdLinx = entity0Al1.ID_LINX
                , IdUsuario = entity0.ID_USUARIO
                , Inativo = entity0.INATIVO
                , IndicaAcessoSuporte = entity0.INDICA_ACESSO_SUPORTE
                , InscrEstadualRg = entity0.INSCR_ESTADUAL_RG
                , Logradouro = entity0.LOGRADOURO
                , LxPfjFisicaJuridica = entity0.LX_PFJ_FISICA_JURIDICA
                , LxPfjFisicaJuridicaName = ((entity0.LX_PFJ_FISICA_JURIDICA) == 1 ? "Pessoa Física" : ((entity0.LX_PFJ_FISICA_JURIDICA) == 2 ? "Pessoa Jurídica" : ""))
                , LxTipoLogradouro = entity0.LX_TIPO_LOGRADOURO
                , LxTipoLogradouroName = ((entity0.LX_TIPO_LOGRADOURO) == 1 ? "Aeroporto" : ((entity0.LX_TIPO_LOGRADOURO) == 2 ? "Alameda" : ((entity0.LX_TIPO_LOGRADOURO) == 3 ? "Apartamento" : ((entity0.LX_TIPO_LOGRADOURO) == 4 ? "Avenida" : ((entity0.LX_TIPO_LOGRADOURO) == 5 ? "Beco" : ((entity0.LX_TIPO_LOGRADOURO) == 6 ? "Bloco" : ((entity0.LX_TIPO_LOGRADOURO) == 7 ? "Caminho" : ((entity0.LX_TIPO_LOGRADOURO) == 8 ? "Escadinha" : ((entity0.LX_TIPO_LOGRADOURO) == 9 ? "Estação" : ((entity0.LX_TIPO_LOGRADOURO) == 10 ? "Estrada" : ((entity0.LX_TIPO_LOGRADOURO) == 11 ? "Fazenda" : ((entity0.LX_TIPO_LOGRADOURO) == 12 ? "Fortaleza" : ((entity0.LX_TIPO_LOGRADOURO) == 13 ? "Galeria" : ((entity0.LX_TIPO_LOGRADOURO) == 14 ? "Ladeira" : ((entity0.LX_TIPO_LOGRADOURO) == 15 ? "Largo" : ((entity0.LX_TIPO_LOGRADOURO) == 17 ? "Parque" : ((entity0.LX_TIPO_LOGRADOURO) == 16 ? "Praça" : ((entity0.LX_TIPO_LOGRADOURO) == 18 ? "Praia" : ((entity0.LX_TIPO_LOGRADOURO) == 19 ? "Quadra" : ((entity0.LX_TIPO_LOGRADOURO) == 20 ? "Quilômetro" : ((entity0.LX_TIPO_LOGRADOURO) == 21 ? "Quinta" : ((entity0.LX_TIPO_LOGRADOURO) == 22 ? "Rodovia" : ((entity0.LX_TIPO_LOGRADOURO) == 23 ? "Rua" : ((entity0.LX_TIPO_LOGRADOURO) == 24 ? "Super Quadra" : ((entity0.LX_TIPO_LOGRADOURO) == 25 ? "Travessa" : ((entity0.LX_TIPO_LOGRADOURO) == 26 ? "Viaduto" : ((entity0.LX_TIPO_LOGRADOURO) == 27 ? "Vila" : "")))))))))))))))))))))))))))
                , Municipio = entity0.MUNICIPIO
                , NomeAutenticacao = entity0.NOME_AUTENTICACAO
                , NomeCurtoUsuario = entity0.NOME_CURTO_USUARIO
                , NomeUsuario = entity0.NOME_USUARIO
                , Numero = entity0.NUMERO
                , ObsEndereco = entity0.OBS_ENDERECO
                , Ramal = entity0.RAMAL
                , Uf = entity0.UF
                , UidUsuario = entity0.UID_USUARIO
                , VigenciaFinal = entity0.VIGENCIA_FINAL
                , VigenciaInicial = entity0.VIGENCIA_INICIAL
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuario> GetTcsUsuarioByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuario));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuario> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_AUTENTICACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsUsuario()		
	            {
	            
                AutenticacaoWindows = entity0.AUTENTICACAO_WINDOWS
                , Bairro = entity0.BAIRRO
                , Cep = entity0.CEP
                , CnpjCpf = entity0.CNPJ_CPF
                , Complemento = entity0.COMPLEMENTO
                , DataAlteracao = entity0.DATA_ALTERACAO
                , DataCadastro = entity0.DATA_CADASTRO
                , DataExpiracaoSenha = entity0.DATA_EXPIRACAO_SENHA
                , Email = entity0.EMAIL
                , FoneCelular = entity0.FONE_CELULAR
                , FoneFixo = entity0.FONE_FIXO
                , IdLinx = entity0Al1.ID_LINX
                , IdUsuario = entity0.ID_USUARIO
                , Inativo = entity0.INATIVO
                , IndicaAcessoSuporte = entity0.INDICA_ACESSO_SUPORTE
                , InscrEstadualRg = entity0.INSCR_ESTADUAL_RG
                , Logradouro = entity0.LOGRADOURO
                , LxPfjFisicaJuridica = entity0.LX_PFJ_FISICA_JURIDICA
                , LxPfjFisicaJuridicaName = ((entity0.LX_PFJ_FISICA_JURIDICA) == 1 ? "Pessoa Física" : ((entity0.LX_PFJ_FISICA_JURIDICA) == 2 ? "Pessoa Jurídica" : ""))
                , LxTipoLogradouro = entity0.LX_TIPO_LOGRADOURO
                , LxTipoLogradouroName = ((entity0.LX_TIPO_LOGRADOURO) == 1 ? "Aeroporto" : ((entity0.LX_TIPO_LOGRADOURO) == 2 ? "Alameda" : ((entity0.LX_TIPO_LOGRADOURO) == 3 ? "Apartamento" : ((entity0.LX_TIPO_LOGRADOURO) == 4 ? "Avenida" : ((entity0.LX_TIPO_LOGRADOURO) == 5 ? "Beco" : ((entity0.LX_TIPO_LOGRADOURO) == 6 ? "Bloco" : ((entity0.LX_TIPO_LOGRADOURO) == 7 ? "Caminho" : ((entity0.LX_TIPO_LOGRADOURO) == 8 ? "Escadinha" : ((entity0.LX_TIPO_LOGRADOURO) == 9 ? "Estação" : ((entity0.LX_TIPO_LOGRADOURO) == 10 ? "Estrada" : ((entity0.LX_TIPO_LOGRADOURO) == 11 ? "Fazenda" : ((entity0.LX_TIPO_LOGRADOURO) == 12 ? "Fortaleza" : ((entity0.LX_TIPO_LOGRADOURO) == 13 ? "Galeria" : ((entity0.LX_TIPO_LOGRADOURO) == 14 ? "Ladeira" : ((entity0.LX_TIPO_LOGRADOURO) == 15 ? "Largo" : ((entity0.LX_TIPO_LOGRADOURO) == 17 ? "Parque" : ((entity0.LX_TIPO_LOGRADOURO) == 16 ? "Praça" : ((entity0.LX_TIPO_LOGRADOURO) == 18 ? "Praia" : ((entity0.LX_TIPO_LOGRADOURO) == 19 ? "Quadra" : ((entity0.LX_TIPO_LOGRADOURO) == 20 ? "Quilômetro" : ((entity0.LX_TIPO_LOGRADOURO) == 21 ? "Quinta" : ((entity0.LX_TIPO_LOGRADOURO) == 22 ? "Rodovia" : ((entity0.LX_TIPO_LOGRADOURO) == 23 ? "Rua" : ((entity0.LX_TIPO_LOGRADOURO) == 24 ? "Super Quadra" : ((entity0.LX_TIPO_LOGRADOURO) == 25 ? "Travessa" : ((entity0.LX_TIPO_LOGRADOURO) == 26 ? "Viaduto" : ((entity0.LX_TIPO_LOGRADOURO) == 27 ? "Vila" : "")))))))))))))))))))))))))))
                , Municipio = entity0.MUNICIPIO
                , NomeAutenticacao = entity0.NOME_AUTENTICACAO
                , NomeCurtoUsuario = entity0.NOME_CURTO_USUARIO
                , NomeUsuario = entity0.NOME_USUARIO
                , Numero = entity0.NUMERO
                , ObsEndereco = entity0.OBS_ENDERECO
                , Ramal = entity0.RAMAL
                , Uf = entity0.UF
                , UidUsuario = entity0.UID_USUARIO
                , VigenciaFinal = entity0.VIGENCIA_FINAL
                , VigenciaInicial = entity0.VIGENCIA_INICIAL
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get NewMessageInfoByEntitySearch.
	    public IEnumerable<NewMessageInfo> GetNewMessageInfoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<NewMessageInfo> result = new List<NewMessageInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get NewMessageInfoByEntitySearchNoAssociations.
	    public IEnumerable<NewMessageInfo> GetNewMessageInfoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<NewMessageInfo> result = new List<NewMessageInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsMensagemConsultaByEntitySearch.
	    public IQueryable<TcsMensagemConsulta> GetTcsMensagemConsultaByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsMensagemConsulta));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsMensagemConsulta> result = 
	            (from entity0 in this.DbContext.TCS_MENSAGEM.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_USUARIO_AUTENTICACAO
	            
	            	
	            select new TcsMensagemConsulta()		
	            {
	            
                Corpo = entity0.CORPO
                , Criacao = entity0.CRIACAO
                , Envio = entity0.ENVIO
                , Filtro = entity0.FILTRO
                , IdLinx = entity0Al1.ID_LINX
                , IdTcsMensagem = entity0.ID_TCS_MENSAGEM
                , IdUsuario = entity0Al2.ID_USUARIO
                , LxTipoMensagem = entity0.LX_TIPO_MENSAGEM
                , LxTipoMensagemName = ((entity0.LX_TIPO_MENSAGEM) == 3 ? "Erro" : ((entity0.LX_TIPO_MENSAGEM) == 1 ? "Informação" : ((entity0.LX_TIPO_MENSAGEM) == 4 ? "Sucesso" : ((entity0.LX_TIPO_MENSAGEM) == 2 ? "Alerta" : ""))))
                , NomeEmpresa = entity0Al1.NOME_EMPRESA
                , Titulo = entity0.TITULO
                , NomeAutenticacao = entity0Al2.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al2.NOME_USUARIO
			
                ,TcsMensagemConsultaLogList = 
	                        (from entity1 in entity0.TCS_MENSAGEM_LOG_LISTA
                                  let entity1Al1 = entity1.TCS_MENSAGEM
                                  let entity1Al2 = entity1.TCS_USUARIO_AUTENTICACAO
	                        
	                        	
	                        select new TcsMensagemConsultaLog()
	                        {
	                        
                                Dispensada = entity1.DISPENSADA
                                , Entregue = entity1.ENTREGUE
                                , IdTcsMensagem = entity1Al1.ID_TCS_MENSAGEM
                                , IdTcsMensagemLog = entity1.ID_TCS_MENSAGEM_LOG
                                , IdUsuario = entity1Al2.ID_USUARIO
                                , Lida = entity1.LIDA
                                , NomeAutenticacao = entity1Al2.NOME_AUTENTICACAO
                                , NomeUsuario = entity1Al2.NOME_USUARIO
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsMensagemConsultaLogByEntitySearch.
	    public IQueryable<TcsMensagemConsultaLog> GetTcsMensagemConsultaLogByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsMensagemConsultaLog));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsMensagemConsultaLog> result = 
	            (from entity0 in this.DbContext.TCS_MENSAGEM_LOG.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_MENSAGEM
                  let entity0Al2 = entity0.TCS_USUARIO_AUTENTICACAO
	            
	            	
	            select new TcsMensagemConsultaLog()		
	            {
	            
                Dispensada = entity0.DISPENSADA
                , Entregue = entity0.ENTREGUE
                , IdTcsMensagem = entity0Al1.ID_TCS_MENSAGEM
                , IdTcsMensagemLog = entity0.ID_TCS_MENSAGEM_LOG
                , IdUsuario = entity0Al2.ID_USUARIO
                , Lida = entity0.LIDA
                , NomeAutenticacao = entity0Al2.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al2.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsMensagemConsultaByEntitySearchNoAssociations.
	    public IQueryable<TcsMensagemConsulta> GetTcsMensagemConsultaByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsMensagemConsulta));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsMensagemConsulta> result = 
	            (from entity0 in this.DbContext.TCS_MENSAGEM.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_USUARIO_AUTENTICACAO
	            
	            	
	            select new TcsMensagemConsulta()		
	            {
	            
                Corpo = entity0.CORPO
                , Criacao = entity0.CRIACAO
                , Envio = entity0.ENVIO
                , Filtro = entity0.FILTRO
                , IdLinx = entity0Al1.ID_LINX
                , IdTcsMensagem = entity0.ID_TCS_MENSAGEM
                , IdUsuario = entity0Al2.ID_USUARIO
                , LxTipoMensagem = entity0.LX_TIPO_MENSAGEM
                , LxTipoMensagemName = ((entity0.LX_TIPO_MENSAGEM) == 3 ? "Erro" : ((entity0.LX_TIPO_MENSAGEM) == 1 ? "Informação" : ((entity0.LX_TIPO_MENSAGEM) == 4 ? "Sucesso" : ((entity0.LX_TIPO_MENSAGEM) == 2 ? "Alerta" : ""))))
                , NomeEmpresa = entity0Al1.NOME_EMPRESA
                , Titulo = entity0.TITULO
                , NomeAutenticacao = entity0Al2.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al2.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsMensagemConsultaLogByEntitySearchNoAssociations.
	    public IQueryable<TcsMensagemConsultaLog> GetTcsMensagemConsultaLogByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsMensagemConsultaLog));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsMensagemConsultaLog> result = 
	            (from entity0 in this.DbContext.TCS_MENSAGEM_LOG.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_MENSAGEM
                  let entity0Al2 = entity0.TCS_USUARIO_AUTENTICACAO
	            
	            	
	            select new TcsMensagemConsultaLog()		
	            {
	            
                Dispensada = entity0.DISPENSADA
                , Entregue = entity0.ENTREGUE
                , IdTcsMensagem = entity0Al1.ID_TCS_MENSAGEM
                , IdTcsMensagemLog = entity0.ID_TCS_MENSAGEM_LOG
                , IdUsuario = entity0Al2.ID_USUARIO
                , Lida = entity0.LIDA
                , NomeAutenticacao = entity0Al2.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al2.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsMensagemConsultaLogParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsMensagemConsultaLogParentComposition> GetTcsMensagemConsultaLogParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "TCS_MENSAGEM", "TCS_MENSAGEM_LOG", "TCS_MENSAGEM", typeof(TcsMensagemConsultaLogParentComposition));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsMensagemConsultaLogParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_MENSAGEM_LOG.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_MENSAGEM
                  let entity0Al2 = entity0.TCS_USUARIO_AUTENTICACAO
	            
	            	
	            select new TcsMensagemConsultaLogParentComposition()		
	            {
	            
                Dispensada = entity0.DISPENSADA
                , Entregue = entity0.ENTREGUE
                , IdTcsMensagem = entity0Al1.ID_TCS_MENSAGEM
                , IdTcsMensagemLog = entity0.ID_TCS_MENSAGEM_LOG
                , IdUsuario = entity0Al2.ID_USUARIO
                , Lida = entity0.LIDA
                , NomeAutenticacao = entity0Al2.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al2.NOME_USUARIO
                //TcsMensagemConsulta Properties.
                , Corpo = entity0.TCS_MENSAGEM.CORPO
                , Criacao = entity0.TCS_MENSAGEM.CRIACAO
                , Envio = entity0.TCS_MENSAGEM.ENVIO
                , Filtro = entity0.TCS_MENSAGEM.FILTRO
                , IdLinx = entity0.TCS_MENSAGEM.TCS_EMPRESA_AUTENTICACAO.ID_LINX
                , LxTipoMensagem = entity0.TCS_MENSAGEM.LX_TIPO_MENSAGEM
                , LxTipoMensagemName = ((entity0.TCS_MENSAGEM.LX_TIPO_MENSAGEM) == 3 ? "Erro" : ((entity0.TCS_MENSAGEM.LX_TIPO_MENSAGEM) == 1 ? "Informação" : ((entity0.TCS_MENSAGEM.LX_TIPO_MENSAGEM) == 4 ? "Sucesso" : ((entity0.TCS_MENSAGEM.LX_TIPO_MENSAGEM) == 2 ? "Alerta" : ""))))
                , NomeEmpresa = entity0.TCS_MENSAGEM.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA
                , Titulo = entity0.TCS_MENSAGEM.TITULO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get PagedTcsMensagem.
	    public IQueryable<TcsMensagem> GetPagedTcsMensagem(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsMensagem));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsMensagem> result = 
	            (from entity0 in this.DbContext.TCS_MENSAGEM.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_USUARIO_AUTENTICACAO
                orderby entity0.ID_TCS_MENSAGEM ascending
	            
	            	
	            select new TcsMensagem()		
	            {
	            
                Corpo = entity0.CORPO
                , Criacao = entity0.CRIACAO
                , Envio = entity0.ENVIO
                , Filtro = entity0.FILTRO
                , IdLinx = entity0Al1.ID_LINX
                , IdTcsMensagem = entity0.ID_TCS_MENSAGEM
                , IdUsuario = entity0Al2.ID_USUARIO
                , LxTipoMensagem = entity0.LX_TIPO_MENSAGEM
                , LxTipoMensagemName = ((entity0.LX_TIPO_MENSAGEM) == 3 ? "Erro" : ((entity0.LX_TIPO_MENSAGEM) == 1 ? "Informação" : ((entity0.LX_TIPO_MENSAGEM) == 4 ? "Sucesso" : ((entity0.LX_TIPO_MENSAGEM) == 2 ? "Alerta" : ""))))
                , Titulo = entity0.TITULO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsMensagemLogDetail.
	    public IQueryable<TcsMensagemLogDetail> GetPagedTcsMensagemLogDetail(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsMensagemLogDetail));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsMensagemLogDetail> result = 
	            (from entity0 in this.DbContext.TCS_MENSAGEM_LOG.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_MENSAGEM
                  let entity0Al2 = entity0.TCS_USUARIO_AUTENTICACAO
                orderby entity0.ID_TCS_MENSAGEM_LOG ascending
	            
	            	
	            select new TcsMensagemLogDetail()		
	            {
	            
                Dispensada = entity0.DISPENSADA
                , Entregue = entity0.ENTREGUE
                , IdTcsMensagem = entity0Al1.ID_TCS_MENSAGEM
                , IdTcsMensagemLog = entity0.ID_TCS_MENSAGEM_LOG
                , IdUsuario = entity0Al2.ID_USUARIO
                , Lida = entity0.LIDA
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsMensagemCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsMensagem));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_MENSAGEM.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_EMPRESA_AUTENTICACAO
                  let entityAl2 = entity.TCS_USUARIO_AUTENTICACAO
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsMensagemLogDetailCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsMensagemLogDetail));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_MENSAGEM_LOG.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_MENSAGEM
                  let entityAl2 = entity.TCS_USUARIO_AUTENTICACAO
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedMensagemInfo.
	    public IEnumerable<MensagemInfo> GetPagedMensagemInfo(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<MensagemInfo> result = new List<MensagemInfo>();
	  	
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetMensagemInfoCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsMensagemUsuario.
	    public IQueryable<TcsMensagemUsuario> GetPagedTcsMensagemUsuario(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsMensagemUsuario));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsMensagemUsuario> result = 
	            (from entity0 in this.DbContext.TCS_MENSAGEM_LOG.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_MENSAGEM
                  let entity0Al3 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_MENSAGEM.TCS_EMPRESA_AUTENTICACAO
                orderby entity0.ID_TCS_MENSAGEM_LOG ascending
	            
	            	
	            select new TcsMensagemUsuario()		
	            {
	            
                Corpo = entity0Al1.CORPO
                , Dispensada = entity0.DISPENSADA
                , Entregue = entity0.ENTREGUE
                , Envio = entity0Al1.ENVIO
                , IdLinx = entity0Al2.ID_LINX
                , IdTcsMensagem = entity0Al1.ID_TCS_MENSAGEM
                , IdTcsMensagemLog = entity0.ID_TCS_MENSAGEM_LOG
                , IdUsuario = entity0Al3.ID_USUARIO
                , Lida = entity0.LIDA
                , LxTipoMensagem = entity0Al1.LX_TIPO_MENSAGEM
                , LxTipoMensagemName = ((entity0Al1.LX_TIPO_MENSAGEM) == 3 ? "Erro" : ((entity0Al1.LX_TIPO_MENSAGEM) == 1 ? "Informação" : ((entity0Al1.LX_TIPO_MENSAGEM) == 4 ? "Sucesso" : ((entity0Al1.LX_TIPO_MENSAGEM) == 2 ? "Alerta" : ""))))
                , Titulo = entity0Al1.TITULO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsMensagemUsuarioCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsMensagemUsuario));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_MENSAGEM_LOG.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_MENSAGEM
                  let entityAl3 = entity.TCS_USUARIO_AUTENTICACAO
                  let entityAl2 = entity.TCS_MENSAGEM.TCS_EMPRESA_AUTENTICACAO
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsMensagemLog.
	    public IQueryable<TcsMensagemLog> GetPagedTcsMensagemLog(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsMensagemLog));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsMensagemLog> result = 
	            (from entity0 in this.DbContext.TCS_MENSAGEM_LOG.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_MENSAGEM
                  let entity0Al2 = entity0.TCS_USUARIO_AUTENTICACAO
                orderby entity0.ID_TCS_MENSAGEM_LOG ascending
	            
	            	
	            select new TcsMensagemLog()		
	            {
	            
                Dispensada = entity0.DISPENSADA
                , Entregue = entity0.ENTREGUE
                , IdTcsMensagem = entity0Al1.ID_TCS_MENSAGEM
                , IdTcsMensagemLog = entity0.ID_TCS_MENSAGEM_LOG
                , IdUsuario = entity0Al2.ID_USUARIO
                , Lida = entity0.LIDA
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsMensagemLogCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsMensagemLog));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_MENSAGEM_LOG.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_MENSAGEM
                  let entityAl2 = entity.TCS_USUARIO_AUTENTICACAO
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsPerfil.
	    public IQueryable<TcsPerfil> GetPagedTcsPerfil(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsPerfil));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		

	        IQueryable<TcsPerfil> result = 
	            (from entity0 in TcsPerfil.GetBusinessView(this, dynQuery, parameters.ToArray()).OrderBy(e => e.IdTcsUsuarioPerfil).Skip(skip).Take(take) select entity0);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsUsuario.
	    public IQueryable<TcsUsuario> GetPagedTcsUsuario(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuario));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuario> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_AUTENTICACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_EMPRESA_AUTENTICACAO
                orderby entity0.ID_USUARIO ascending
	            
	            	
	            select new TcsUsuario()		
	            {
	            
                AutenticacaoWindows = entity0.AUTENTICACAO_WINDOWS
                , Bairro = entity0.BAIRRO
                , Cep = entity0.CEP
                , CnpjCpf = entity0.CNPJ_CPF
                , Complemento = entity0.COMPLEMENTO
                , DataAlteracao = entity0.DATA_ALTERACAO
                , DataCadastro = entity0.DATA_CADASTRO
                , DataExpiracaoSenha = entity0.DATA_EXPIRACAO_SENHA
                , Email = entity0.EMAIL
                , FoneCelular = entity0.FONE_CELULAR
                , FoneFixo = entity0.FONE_FIXO
                , IdLinx = entity0Al1.ID_LINX
                , IdUsuario = entity0.ID_USUARIO
                , Inativo = entity0.INATIVO
                , IndicaAcessoSuporte = entity0.INDICA_ACESSO_SUPORTE
                , InscrEstadualRg = entity0.INSCR_ESTADUAL_RG
                , Logradouro = entity0.LOGRADOURO
                , LxPfjFisicaJuridica = entity0.LX_PFJ_FISICA_JURIDICA
                , LxPfjFisicaJuridicaName = ((entity0.LX_PFJ_FISICA_JURIDICA) == 1 ? "Pessoa Física" : ((entity0.LX_PFJ_FISICA_JURIDICA) == 2 ? "Pessoa Jurídica" : ""))
                , LxTipoLogradouro = entity0.LX_TIPO_LOGRADOURO
                , LxTipoLogradouroName = ((entity0.LX_TIPO_LOGRADOURO) == 1 ? "Aeroporto" : ((entity0.LX_TIPO_LOGRADOURO) == 2 ? "Alameda" : ((entity0.LX_TIPO_LOGRADOURO) == 3 ? "Apartamento" : ((entity0.LX_TIPO_LOGRADOURO) == 4 ? "Avenida" : ((entity0.LX_TIPO_LOGRADOURO) == 5 ? "Beco" : ((entity0.LX_TIPO_LOGRADOURO) == 6 ? "Bloco" : ((entity0.LX_TIPO_LOGRADOURO) == 7 ? "Caminho" : ((entity0.LX_TIPO_LOGRADOURO) == 8 ? "Escadinha" : ((entity0.LX_TIPO_LOGRADOURO) == 9 ? "Estação" : ((entity0.LX_TIPO_LOGRADOURO) == 10 ? "Estrada" : ((entity0.LX_TIPO_LOGRADOURO) == 11 ? "Fazenda" : ((entity0.LX_TIPO_LOGRADOURO) == 12 ? "Fortaleza" : ((entity0.LX_TIPO_LOGRADOURO) == 13 ? "Galeria" : ((entity0.LX_TIPO_LOGRADOURO) == 14 ? "Ladeira" : ((entity0.LX_TIPO_LOGRADOURO) == 15 ? "Largo" : ((entity0.LX_TIPO_LOGRADOURO) == 17 ? "Parque" : ((entity0.LX_TIPO_LOGRADOURO) == 16 ? "Praça" : ((entity0.LX_TIPO_LOGRADOURO) == 18 ? "Praia" : ((entity0.LX_TIPO_LOGRADOURO) == 19 ? "Quadra" : ((entity0.LX_TIPO_LOGRADOURO) == 20 ? "Quilômetro" : ((entity0.LX_TIPO_LOGRADOURO) == 21 ? "Quinta" : ((entity0.LX_TIPO_LOGRADOURO) == 22 ? "Rodovia" : ((entity0.LX_TIPO_LOGRADOURO) == 23 ? "Rua" : ((entity0.LX_TIPO_LOGRADOURO) == 24 ? "Super Quadra" : ((entity0.LX_TIPO_LOGRADOURO) == 25 ? "Travessa" : ((entity0.LX_TIPO_LOGRADOURO) == 26 ? "Viaduto" : ((entity0.LX_TIPO_LOGRADOURO) == 27 ? "Vila" : "")))))))))))))))))))))))))))
                , Municipio = entity0.MUNICIPIO
                , NomeAutenticacao = entity0.NOME_AUTENTICACAO
                , NomeCurtoUsuario = entity0.NOME_CURTO_USUARIO
                , NomeUsuario = entity0.NOME_USUARIO
                , Numero = entity0.NUMERO
                , ObsEndereco = entity0.OBS_ENDERECO
                , Ramal = entity0.RAMAL
                , Uf = entity0.UF
                , UidUsuario = entity0.UID_USUARIO
                , VigenciaFinal = entity0.VIGENCIA_FINAL
                , VigenciaInicial = entity0.VIGENCIA_INICIAL
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsUsuarioCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuario));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_USUARIO_AUTENTICACAO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_EMPRESA_AUTENTICACAO
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedNewMessageInfo.
	    public IEnumerable<NewMessageInfo> GetPagedNewMessageInfo(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<NewMessageInfo> result = new List<NewMessageInfo>();
	  	
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetNewMessageInfoCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsMensagemConsulta.
	    public IQueryable<TcsMensagemConsulta> GetPagedTcsMensagemConsulta(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsMensagemConsulta));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsMensagemConsulta> result = 
	            (from entity0 in this.DbContext.TCS_MENSAGEM.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_USUARIO_AUTENTICACAO
                orderby entity0.ID_TCS_MENSAGEM ascending
	            
	            	
	            select new TcsMensagemConsulta()		
	            {
	            
                Corpo = entity0.CORPO
                , Criacao = entity0.CRIACAO
                , Envio = entity0.ENVIO
                , Filtro = entity0.FILTRO
                , IdLinx = entity0Al1.ID_LINX
                , IdTcsMensagem = entity0.ID_TCS_MENSAGEM
                , IdUsuario = entity0Al2.ID_USUARIO
                , LxTipoMensagem = entity0.LX_TIPO_MENSAGEM
                , LxTipoMensagemName = ((entity0.LX_TIPO_MENSAGEM) == 3 ? "Erro" : ((entity0.LX_TIPO_MENSAGEM) == 1 ? "Informação" : ((entity0.LX_TIPO_MENSAGEM) == 4 ? "Sucesso" : ((entity0.LX_TIPO_MENSAGEM) == 2 ? "Alerta" : ""))))
                , NomeEmpresa = entity0Al1.NOME_EMPRESA
                , Titulo = entity0.TITULO
                , NomeAutenticacao = entity0Al2.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al2.NOME_USUARIO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsMensagemConsultaLog.
	    public IQueryable<TcsMensagemConsultaLog> GetPagedTcsMensagemConsultaLog(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsMensagemConsultaLog));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsMensagemConsultaLog> result = 
	            (from entity0 in this.DbContext.TCS_MENSAGEM_LOG.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_MENSAGEM
                  let entity0Al2 = entity0.TCS_USUARIO_AUTENTICACAO
                orderby entity0.ID_TCS_MENSAGEM_LOG ascending
	            
	            	
	            select new TcsMensagemConsultaLog()		
	            {
	            
                Dispensada = entity0.DISPENSADA
                , Entregue = entity0.ENTREGUE
                , IdTcsMensagem = entity0Al1.ID_TCS_MENSAGEM
                , IdTcsMensagemLog = entity0.ID_TCS_MENSAGEM_LOG
                , IdUsuario = entity0Al2.ID_USUARIO
                , Lida = entity0.LIDA
                , NomeAutenticacao = entity0Al2.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al2.NOME_USUARIO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsMensagemConsultaCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsMensagemConsulta));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_MENSAGEM.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_EMPRESA_AUTENTICACAO
                  let entityAl2 = entity.TCS_USUARIO_AUTENTICACAO
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsMensagemConsultaLogCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsMensagemConsultaLog));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_MENSAGEM_LOG.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_MENSAGEM
                  let entityAl2 = entity.TCS_USUARIO_AUTENTICACAO
	            
	            select 1
	            ).Count();	
		
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    
	    [Update()]	
	    //Update TcsMensagem.
	    public void UpdateTcsMensagem(TcsMensagem entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsMensagem.
	    public void InsertTcsMensagem(TcsMensagem entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsMensagem.
	    public void DeleteTcsMensagem(TcsMensagem entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsMensagemLogDetail.
	    public void UpdateTcsMensagemLogDetail(TcsMensagemLogDetail entity)
	    {



	
	        if (entity.TcsMensagem.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsMensagem) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsMensagem); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsMensagemLogDetail.
	    public void InsertTcsMensagemLogDetail(TcsMensagemLogDetail entity)
	    {



	
	        if (entity.TcsMensagem.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsMensagem) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsMensagem);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsMensagemLogDetail.
	    public void DeleteTcsMensagemLogDetail(TcsMensagemLogDetail entity)
	    {



	
	        if (entity.TcsMensagem.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsMensagem) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsMensagem);
	            

	
	        }

	
	    }
		
			
	    
	    [Update()]	
	    //Update MensagemInfo.
	    public void UpdateMensagemInfo(MensagemInfo entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert MensagemInfo.
	    public void InsertMensagemInfo(MensagemInfo entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete MensagemInfo.
	    public void DeleteMensagemInfo(MensagemInfo entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsMensagemUsuario.
	    public void UpdateTcsMensagemUsuario(TcsMensagemUsuario entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsMensagemUsuario.
	    public void InsertTcsMensagemUsuario(TcsMensagemUsuario entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsMensagemUsuario.
	    public void DeleteTcsMensagemUsuario(TcsMensagemUsuario entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsMensagemLog.
	    public void UpdateTcsMensagemLog(TcsMensagemLog entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsMensagemLog.
	    public void InsertTcsMensagemLog(TcsMensagemLog entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsMensagemLog.
	    public void DeleteTcsMensagemLog(TcsMensagemLog entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsPerfil.
	    public void UpdateTcsPerfil(TcsPerfil entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert TcsPerfil.
	    public void InsertTcsPerfil(TcsPerfil entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete TcsPerfil.
	    public void DeleteTcsPerfil(TcsPerfil entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsUsuario.
	    public void UpdateTcsUsuario(TcsUsuario entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsUsuario.
	    public void InsertTcsUsuario(TcsUsuario entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsUsuario.
	    public void DeleteTcsUsuario(TcsUsuario entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update NewMessageInfo.
	    public void UpdateNewMessageInfo(NewMessageInfo entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert NewMessageInfo.
	    public void InsertNewMessageInfo(NewMessageInfo entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete NewMessageInfo.
	    public void DeleteNewMessageInfo(NewMessageInfo entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsMensagemConsulta.
	    public void UpdateTcsMensagemConsulta(TcsMensagemConsulta entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert TcsMensagemConsulta.
	    public void InsertTcsMensagemConsulta(TcsMensagemConsulta entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete TcsMensagemConsulta.
	    public void DeleteTcsMensagemConsulta(TcsMensagemConsulta entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsMensagemConsultaLog.
	    public void UpdateTcsMensagemConsultaLog(TcsMensagemConsultaLog entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert TcsMensagemConsultaLog.
	    public void InsertTcsMensagemConsultaLog(TcsMensagemConsultaLog entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete TcsMensagemConsultaLog.
	    public void DeleteTcsMensagemConsultaLog(TcsMensagemConsultaLog entity)
	    {



	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}