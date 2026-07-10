					
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

namespace Linx.Framework.BV.Configuracao
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_USUARIO_AUTENTICACAO.ID_USUARIO", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsUsuarioConfiguracao,TcsUsuarioConfiguracao.TcsUsuarioConfiguracaoAcesso];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[true];SubQueryInfo[];EdmEntityName[TCS_USUARIO_AUTENTICACAO];EntityRelations[TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioConfiguracao")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Configuracao.TcsUsuarioConfiguracao")]
	public partial class TcsUsuarioConfiguracao : Linx.Data.Entity
	{

	
		
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.TcsUsuarioConfiguracaoAcessoList != null && this.TcsUsuarioConfiguracaoAcessoList.Count() > 0)
	      {
	         foreach (var entity in this.TcsUsuarioConfiguracaoAcessoList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.TcsUsuarioConfiguracaoAcessoList != null)
	      {
	         foreach (var detail in this.TcsUsuarioConfiguracaoAcessoList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsUsuarioConfiguracaoAcessoList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(ConfiguracaoDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("TcsUsuarioConfiguracaoAcesso"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsUsuarioConfiguracaoAcesso");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuario"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdUsuario));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsUsuarioConfiguracaoAcesso and all sub-details
	         if (this.TcsUsuarioConfiguracaoAcessoList == null || this.TcsUsuarioConfiguracaoAcessoList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsUsuarioConfiguracaoAcessoList = context.GetPagedTcsUsuarioConfiguracaoAcesso(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsUsuarioConfiguracaoAcessoList = (from r in context.GetTcsUsuarioConfiguracaoAcessoByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _TcsUsuarioConfiguracaoAcessoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioConfiguracaoAcesso && ((TcsUsuarioConfiguracaoAcesso)e.Entity).TcsUsuarioConfiguracao == null && e.Associations == null && e.OriginalAssociations == null && ((TcsUsuarioConfiguracaoAcesso)e.Entity).IdUsuario == this.IdUsuario).ToList();
 	      if (_TcsUsuarioConfiguracaoAcessoElements.Count > 0 && this.TcsUsuarioConfiguracaoAcessoList.Count() == 0)
 	      {
 	          this.TcsUsuarioConfiguracaoAcessoList = _TcsUsuarioConfiguracaoAcessoElements.Select(e => (TcsUsuarioConfiguracaoAcesso)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsUsuarioConfiguracaoAcessoElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsUsuarioConfiguracaoAcesso)detail.Entity).TcsUsuarioConfiguracao = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsUsuarioConfiguracao", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsUsuarioConfiguracaoAcessoList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For IdLinx
	    partial void OnIdLinxChanging(int value);
	    partial void OnIdLinxChanged();

	    private int _IdLinx;

	    [DataMember(IsRequired = true, Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsEmpresaAutenticacao];LookUpTitle[Seleção de (Id Linx)];LookUpQuery[executeLookUpTcsEmpresaAutenticacao];LookUpFinalize[finalizeLookUpTcsEmpresaAutenticacao];LookUpDisplayColumns[{\"NomeEmpresa\" : \"Empresa\", \"IdLinx\" : \"Id Linx\", \"UidEmpresa\" : \"Uid Empresa\"}];LookUpColumns[{\"NomeEmpresa\" : true, \"IdLinx\" : true, \"UidEmpresa\" : false}];FilterDataKey[TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="int#IdLinx#true##0:0##Id Linx#1#true##::LookUpTcsEmpresaAutenticacao##false#false#TCS_EMPRESA_AUTENTICACAO#TCS_EMPRESA_AUTENTICACAO#Linx.Framework.BV.Configuracao#IQueryable###true#false", EdmKey="TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
	    public int IdLinx
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
	    partial void OnIdUsuarioChanging(long value);
	    partial void OnIdUsuarioChanged();

	    private long _IdUsuario;

	    [DataMember(IsRequired = true, Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacao];LookUpTitle[Seleção de (Id Usuario)];LookUpQuery[executeLookUpTcsUsuarioAutenticacao];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacao];LookUpDisplayColumns[{\"IdUsuario\" : \"Id Usuario\", \"NomeUsuario\" : \"Usuário\", \"NomeAutenticacao\" : \"Usuário Autenticação\"}];LookUpColumns[{\"IdUsuario\" : false, \"NomeUsuario\" : true, \"NomeAutenticacao\" : true}];FilterDataKey[TCS_USUARIO_AUTENTICACAO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="long#IdUsuario#true##0##Id Usuario#1#false##::LookUpTcsUsuarioAutenticacao##false#false##TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.Configuracao#IQueryable#IdLinx[IdLinx]##true#false", EdmKey="TCS_USUARIO_AUTENTICACAO.ID_USUARIO")]
	    public long IdUsuario
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
	    //Extensibility Partial Method Definitions For NomeAutenticacao
	    partial void OnNomeAutenticacaoChanging(string value);
	    partial void OnNomeAutenticacaoChanged();

	    private string _NomeAutenticacao;

	    [DataMember(IsRequired = true, Name = "NomeAutenticacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário Autenticação", Description="", Order = 20, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacao];LookUpTitle[Seleção de (Usuário Autenticação)];LookUpQuery[executeLookUpTcsUsuarioAutenticacao];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacao];LookUpDisplayColumns[{\"IdUsuario\" : \"Id Usuario\", \"NomeUsuario\" : \"Usuário\", \"NomeAutenticacao\" : \"Usuário Autenticação\"}];LookUpColumns[{\"IdUsuario\" : false, \"NomeUsuario\" : true, \"NomeAutenticacao\" : true}];FilterDataKey[TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#NomeAutenticacao#false##2500##Usuário Autenticação#3#true##::LookUpTcsUsuarioAutenticacao##false#false##TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.Configuracao#IQueryable#IdLinx[IdLinx]##true#false", EdmKey="TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO")]
	    public string NomeAutenticacao
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
	    //Extensibility Partial Method Definitions For NomeEmpresa
	    partial void OnNomeEmpresaChanging(string value);
	    partial void OnNomeEmpresaChanged();

	    private string _NomeEmpresa;

	    [DataMember(IsRequired = true, Name = "NomeEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Empresa", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[IdLinx];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsEmpresaAutenticacao];LookUpTitle[Seleção de (Empresa)];LookUpQuery[executeLookUpTcsEmpresaAutenticacao];LookUpFinalize[finalizeLookUpTcsEmpresaAutenticacao];LookUpDisplayColumns[{\"NomeEmpresa\" : \"Empresa\", \"IdLinx\" : \"Id Linx\", \"UidEmpresa\" : \"Uid Empresa\"}];LookUpColumns[{\"NomeEmpresa\" : true, \"IdLinx\" : true, \"UidEmpresa\" : false}];FilterDataKey[TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#NomeEmpresa#false##250:0##Empresa#0#true##::LookUpTcsEmpresaAutenticacao##false#false#TCS_EMPRESA_AUTENTICACAO#TCS_EMPRESA_AUTENTICACAO#Linx.Framework.BV.Configuracao#IQueryable###true#false", EdmKey="TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
	    public string NomeEmpresa
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
	    //Extensibility Partial Method Definitions For NomeUsuario
	    partial void OnNomeUsuarioChanging(string value);
	    partial void OnNomeUsuarioChanged();

	    private string _NomeUsuario;

	    [DataMember(IsRequired = true, Name = "NomeUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário", Description="", Order = 22, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacao];LookUpTitle[Seleção de (Usuário)];LookUpQuery[executeLookUpTcsUsuarioAutenticacao];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacao];LookUpDisplayColumns[{\"IdUsuario\" : \"Id Usuario\", \"NomeUsuario\" : \"Usuário\", \"NomeAutenticacao\" : \"Usuário Autenticação\"}];LookUpColumns[{\"IdUsuario\" : false, \"NomeUsuario\" : true, \"NomeAutenticacao\" : true}];FilterDataKey[TCS_USUARIO_AUTENTICACAO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#NomeUsuario#false##2500##Usuário#2#true##::LookUpTcsUsuarioAutenticacao##false#false##TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.Configuracao#IQueryable#IdLinx[IdLinx]##true#false", EdmKey="TCS_USUARIO_AUTENTICACAO.NOME_USUARIO")]
	    public string NomeUsuario
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
	    //Extensibility Partial Method Definitions For UidEmpresa
	    partial void OnUidEmpresaChanging(Guid value);
	    partial void OnUidEmpresaChanged();

	    private Guid _UidEmpresa;

	    [DataMember(IsRequired = true, Name = "UidEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Empresa", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsEmpresaAutenticacao];LookUpTitle[Seleção de (Uid Empresa)];LookUpQuery[executeLookUpTcsEmpresaAutenticacao];LookUpFinalize[finalizeLookUpTcsEmpresaAutenticacao];LookUpDisplayColumns[{\"NomeEmpresa\" : \"Empresa\", \"IdLinx\" : \"Id Linx\", \"UidEmpresa\" : \"Uid Empresa\"}];LookUpColumns[{\"NomeEmpresa\" : true, \"IdLinx\" : true, \"UidEmpresa\" : false}];FilterDataKey[TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Guid#UidEmpresa#false##36:0##Uid Empresa#2#false##::LookUpTcsEmpresaAutenticacao##false#false#TCS_EMPRESA_AUTENTICACAO#TCS_EMPRESA_AUTENTICACAO#Linx.Framework.BV.Configuracao#IQueryable###true#false", EdmKey="TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA")]
	    public Guid UidEmpresa
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
	    //Extensibility Partial Method Definitions For UidUsuario
	    partial void OnUidUsuarioChanging(Guid value);
	    partial void OnUidUsuarioChanged();

	    private Guid _UidUsuario;

	    [DataMember(IsRequired = true, Name = "UidUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Usuario", Description="", Order = 27, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.UID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.UID_USUARIO")]
	    public Guid UidUsuario
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

	    #endregion Data Properties

	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<TcsUsuarioConfiguracaoAcesso> _TcsUsuarioConfiguracaoAcessoList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsUsuarioConfiguracao_TcsUsuarioConfiguracaoAcesso", "IdUsuario", "IdUsuario", IsForeignKey=false)]
	    [DataMember(Name = "TcsUsuarioConfiguracaoAcessoList", EmitDefaultValue = true)]
	    public IEnumerable<TcsUsuarioConfiguracaoAcesso> TcsUsuarioConfiguracaoAcessoList
	    {
	        get
	        {
	
	            if (this._TcsUsuarioConfiguracaoAcessoList == null)
	            	this._TcsUsuarioConfiguracaoAcessoList = new List<TcsUsuarioConfiguracaoAcesso>();
	
	            return this._TcsUsuarioConfiguracaoAcessoList;
	        }
	        set
	        {
	            if (this._TcsUsuarioConfiguracaoAcessoList != value)
	            {
	                this._TcsUsuarioConfiguracaoAcessoList = value;
	                this.RaisePropertyChanged("TcsUsuarioConfiguracaoAcessoList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
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

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.UID_USUARIO", Source = "UidUsuario", Target = "UID_USUARIO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.NOME_USUARIO", Source = "NomeUsuario", Target = "NOME_USUARIO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO", Source = "NomeAutenticacao", Target = "NOME_AUTENTICACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.ID_LINX", Source = "IdLinx", Target = "ID_LINX", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_AUTENTICACAO", RelationPropertyName = "TCS_EMPRESA_AUTENTICACAO" });

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
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Ambientes];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#.TCS_USUARIO_ACESSO_LISTA as #Alias#];EdmEntityName[TCS_USUARIO_ACESSO];EntityRelations[TCS_USUARIO_AUTENTICACAO(TCS_USUARIO_AUTENTICACAO)#TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)#TCS_AMBIENTE(TCS_AMBIENTE)#TCS_APLICACAO(TCS_APLICACAO)#TCS_APLICATIVO(TCS_APLICATIVO)#TCS_AMBIENTE1(TCS_AMBIENTE)];EdmParentEntityName[TCS_USUARIO_AUTENTICACAO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioConfiguracaoAcesso")]
	[Serializable()]
	public partial class TcsUsuarioConfiguracaoAcesso : Linx.Data.Entity
	{

	

	    public TcsUsuarioConfiguracaoAcesso() : this(true) { }

	    public TcsUsuarioConfiguracaoAcesso(bool setDefaults) 
	    {

	        if (setDefaults)
	        {
	        	        Selecionado = false;
	        }	

	    }

			
	

	
	    #region Load Data Parent
		

	    public void LoadParent(ConfiguracaoDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsUsuarioConfiguracao");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuario"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdUsuario));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsUsuarioConfiguracao
	         this.TcsUsuarioConfiguracao = (from r in context.GetTcsUsuarioConfiguracaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	    partial void OnDescricaoAmbienteChanging(string value);
	    partial void OnDescricaoAmbienteChanged();

	    private string _DescricaoAmbiente;

	    [DataMember(IsRequired = true, Name = "DescricaoAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ambiente", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.DESCRICAO_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.DESCRICAO_AMBIENTE")]
	    public string DescricaoAmbiente
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
	    partial void OnDescricaoAplicacaoChanging(string value);
	    partial void OnDescricaoAplicacaoChanged();

	    private string _DescricaoAplicacao;

	    [DataMember(IsRequired = true, Name = "DescricaoAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicação", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.DESCRICAO_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.DESCRICAO_APLICACAO")]
	    public string DescricaoAplicacao
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
	    partial void OnDescricaoAplicativoChanging(string value);
	    partial void OnDescricaoAplicativoChanged();

	    private string _DescricaoAplicativo;

	    [DataMember(Name = "DescricaoAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicativo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO")]
	    public string DescricaoAplicativo
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
	    partial void OnIdAplicacaoChanging(int value);
	    partial void OnIdAplicacaoChanged();

	    private int _IdAplicacao;

	    [DataMember(IsRequired = true, Name = "IdAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Aplicacao", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.ID_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.ID_APLICACAO")]
	    public int IdAplicacao
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
	    partial void OnIdLinxChanging(int value);
	    partial void OnIdLinxChanged();

	    private int _IdLinx;

	    [DataMember(IsRequired = true, Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
	    public int IdLinx
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
	    partial void OnIdTcsAmbienteChanging(int value);
	    partial void OnIdTcsAmbienteChanged();

	    private int _IdTcsAmbiente;

	    [DataMember(IsRequired = true, Name = "IdTcsAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.ID_TCS_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.ID_TCS_AMBIENTE")]
	    public int IdTcsAmbiente
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
	    partial void OnIdTcsAplicativoChanging(int value);
	    partial void OnIdTcsAplicativoChanged();

	    private int _IdTcsAplicativo;

	    [DataMember(IsRequired = true, Name = "IdTcsAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Aplicativo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO")]
	    public int IdTcsAplicativo
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
	    partial void OnIdTcsUsuarioAcessoChanging(int value);
	    partial void OnIdTcsUsuarioAcessoChanged();

	    private int _IdTcsUsuarioAcesso;

	    [DataMember(IsRequired = true, Name = "IdTcsUsuarioAcesso", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Acesso", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO")]
	    public int IdTcsUsuarioAcesso
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
	    partial void OnIdUsuarioChanging(long value);
	    partial void OnIdUsuarioChanged();

	    private long _IdUsuario;

	    [DataMember(IsRequired = true, Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.ID_USUARIO")]
	    public long IdUsuario
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
	    //Extensibility Partial Method Definitions For IndicaAcessoPadrao
	    partial void OnIndicaAcessoPadraoChanging(bool value);
	    partial void OnIndicaAcessoPadraoChanged();

	    private bool _IndicaAcessoPadrao;

	    [DataMember(IsRequired = true, Name = "IndicaAcessoPadrao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Acesso Padrão", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.INDICA_ACESSO_PADRAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.INDICA_ACESSO_PADRAO")]
	    public bool IndicaAcessoPadrao
	    {
	    	    get
	    	    {
	    	          return _IndicaAcessoPadrao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IndicaAcessoPadrao != value)
	    	          {
	    	              this.ValidateProperty("IndicaAcessoPadrao", value);
	    	              this.OnIndicaAcessoPadraoChanging(value);
	    	              this.RaiseDataMemberChanging("IndicaAcessoPadrao");
	    	              this._IndicaAcessoPadrao = value;
	    	              this.RaiseDataMemberChanged("IndicaAcessoPadrao");
	    	              this.OnIndicaAcessoPadraoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IndicaAdministrador
	    partial void OnIndicaAdministradorChanging(bool value);
	    partial void OnIndicaAdministradorChanged();

	    private bool _IndicaAdministrador;

	    [DataMember(IsRequired = true, Name = "IndicaAdministrador", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Administrador", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.INDICA_ADMINISTRADOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.INDICA_ADMINISTRADOR")]
	    public bool IndicaAdministrador
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
	    partial void OnIndicaMultiGpeconChanging(bool value);
	    partial void OnIndicaMultiGpeconChanged();

	    private bool _IndicaMultiGpecon;

	    [DataMember(IsRequired = true, Name = "IndicaMultiGpecon", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Multi Gpecon", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.INDICA_MULTI_GPECON];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.INDICA_MULTI_GPECON")]
	    public bool IndicaMultiGpecon
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
	    partial void OnNomeEmpresaChanging(string value);
	    partial void OnNomeEmpresaChanged();

	    private string _NomeEmpresa;

	    [DataMember(IsRequired = true, Name = "NomeEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Empresa", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[IdLinx];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
	    public string NomeEmpresa
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
	    //Extensibility Partial Method Definitions For Selecionado
	    partial void OnSelecionadoChanging(bool value);
	    partial void OnSelecionadoChanged();

	    private bool _Selecionado;

	    [DataMember(IsRequired = true, Name = "Selecionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Selecionado", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[false];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[false];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="false")]
	    public bool Selecionado
	    {
	    	    get
	    	    {
	    	          return _Selecionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._Selecionado != value)
	    	          {
	    	              this.ValidateProperty("Selecionado", value);
	    	              this.OnSelecionadoChanging(value);
	    	              this.RaiseDataMemberChanging("Selecionado");
	    	              this._Selecionado = value;
	    	              this.RaiseDataMemberChanged("Selecionado");
	    	              this.OnSelecionadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidAplicacao
	    partial void OnUidAplicacaoChanging(Guid value);
	    partial void OnUidAplicacaoChanged();

	    private Guid _UidAplicacao;

	    [DataMember(IsRequired = true, Name = "UidAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Aplicacao", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.UID_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.UID_APLICACAO")]
	    public Guid UidAplicacao
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
	    partial void OnUidEmpresaChanging(Guid value);
	    partial void OnUidEmpresaChanged();

	    private Guid _UidEmpresa;

	    [DataMember(IsRequired = true, Name = "UidEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Empresa", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA")]
	    public Guid UidEmpresa
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
	    //Extensibility Partial Method Definitions For NomeAutenticacao
	    partial void OnNomeAutenticacaoChanging(System.String value);
	    partial void OnNomeAutenticacaoChanged();

	    private System.String _NomeAutenticacao;

	    [DataMember(IsRequired = true, Name = "NomeAutenticacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário Autenticação", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO")]
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
	    //Extensibility Partial Method Definitions For NomeUsuario
	    partial void OnNomeUsuarioChanging(System.String value);
	    partial void OnNomeUsuarioChanged();

	    private System.String _NomeUsuario;

	    [DataMember(IsRequired = true, Name = "NomeUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO")]
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

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsUsuarioConfiguracao _TcsUsuarioConfiguracao;
	    [DataMember(Name = "TcsUsuarioConfiguracao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsUsuarioConfiguracao_TcsUsuarioConfiguracaoAcesso", "IdUsuario", "IdUsuario", IsForeignKey=true)]
	    public TcsUsuarioConfiguracao TcsUsuarioConfiguracao
	    {
	        get
	        {
	            return this._TcsUsuarioConfiguracao;
	        }
	        set
	        {
	            if (this._TcsUsuarioConfiguracao != value)
	            {
	                this._TcsUsuarioConfiguracao = value;
	                this.RaisePropertyChanged("TcsUsuarioConfiguracaoList");
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
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_ACESSO.INDICA_ACESSO_PADRAO", Source = "IndicaAcessoPadrao", Target = "INDICA_ACESSO_PADRAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_ACESSO", RelationPropertyName = "TCS_USUARIO_ACESSO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_ACESSO.INDICA_ADMINISTRADOR", Source = "IndicaAdministrador", Target = "INDICA_ADMINISTRADOR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_ACESSO", RelationPropertyName = "TCS_USUARIO_ACESSO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO", Source = "IdTcsUsuarioAcesso", Target = "ID_TCS_USUARIO_ACESSO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_ACESSO", RelationPropertyName = "TCS_USUARIO_ACESSO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_ACESSO.TCS_AMBIENTE.ID_TCS_AMBIENTE", Source = "IdTcsAmbiente", Target = "ID_TCS_AMBIENTE", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE", RelationPropertyName = "TCS_AMBIENTE" });
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

		

	[LinxPublicationView(PrimaryKeys="ConfiguracaoAcesso.EntityUniqueKey", IsUpdatable=false, EdmName="")]
		
	[DataContract(IsReference = false, Name = "ConfiguracaoAcesso")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Configuracao.ConfiguracaoAcesso")]
	public partial class ConfiguracaoAcesso 
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
	 


	    private int _IdLinx;

	    [DataMember(Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public int IdLinx
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

	    private int _IdTcsAmbiente;

	    [DataMember(Name = "IdTcsAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
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

	    private int _IdTcsAplicativo;

	    [DataMember(Name = "IdTcsAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Aplicativo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public int IdTcsAplicativo
	    {
	    	    get
	    	    {
	    	          return _IdTcsAplicativo;
	    	    }
	    	    set
	    	    {
	    	          this._IdTcsAplicativo = value;
	    	    }
	    }

	    private long _IdUsuario;

	    [DataMember(Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public long IdUsuario
	    {
	    	    get
	    	    {
	    	          return _IdUsuario;
	    	    }
	    	    set
	    	    {
	    	          this._IdUsuario = value;
	    	    }
	    }

	    private Guid _UidEmpresa;

	    [DataMember(Name = "UidEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Empresa", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA")]
	    public Guid UidEmpresa
	    {
	    	    get
	    	    {
	    	          return _UidEmpresa;
	    	    }
	    	    set
	    	    {
	    	          this._UidEmpresa = value;
	    	    }
	    }

	    private Guid _UidAplicacao;

	    [DataMember(Name = "UidAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Aplicacao", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public Guid UidAplicacao
	    {
	    	    get
	    	    {
	    	          return _UidAplicacao;
	    	    }
	    	    set
	    	    {
	    	          this._UidAplicacao = value;
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

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Ambientes];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#.TCS_USUARIO_ACESSO_LISTA as #Alias#];EdmEntityName[TCS_USUARIO_ACESSO];EntityRelations[TCS_USUARIO_AUTENTICACAO(TCS_USUARIO_AUTENTICACAO)#TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)#TCS_AMBIENTE(TCS_AMBIENTE)#TCS_APLICACAO(TCS_APLICACAO)#TCS_APLICATIVO(TCS_APLICATIVO)#TCS_AMBIENTE1(TCS_AMBIENTE)];EdmParentEntityName[TCS_USUARIO_AUTENTICACAO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioConfiguracaoAcesso")]
	[Serializable()]
	public partial class TcsUsuarioConfiguracaoAcessoParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For DescricaoAmbiente
	    partial void OnDescricaoAmbienteChanging(string value);
	    partial void OnDescricaoAmbienteChanged();

	    private string _DescricaoAmbiente;

	    [DataMember(IsRequired = true, Name = "DescricaoAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ambiente", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.DESCRICAO_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.DESCRICAO_AMBIENTE")]
	    public string DescricaoAmbiente
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
	    partial void OnDescricaoAplicacaoChanging(string value);
	    partial void OnDescricaoAplicacaoChanged();

	    private string _DescricaoAplicacao;

	    [DataMember(IsRequired = true, Name = "DescricaoAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicação", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.DESCRICAO_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.DESCRICAO_APLICACAO")]
	    public string DescricaoAplicacao
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
	    partial void OnDescricaoAplicativoChanging(string value);
	    partial void OnDescricaoAplicativoChanged();

	    private string _DescricaoAplicativo;

	    [DataMember(Name = "DescricaoAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicativo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO")]
	    public string DescricaoAplicativo
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
	    partial void OnIdAplicacaoChanging(int value);
	    partial void OnIdAplicacaoChanged();

	    private int _IdAplicacao;

	    [DataMember(IsRequired = true, Name = "IdAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Aplicacao", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.ID_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.ID_APLICACAO")]
	    public int IdAplicacao
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
	    partial void OnIdLinxChanging(int value);
	    partial void OnIdLinxChanged();

	    private int _IdLinx;

	    [DataMember(IsRequired = true, Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
	    public int IdLinx
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
	    partial void OnIdTcsAmbienteChanging(int value);
	    partial void OnIdTcsAmbienteChanged();

	    private int _IdTcsAmbiente;

	    [DataMember(IsRequired = true, Name = "IdTcsAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.ID_TCS_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.ID_TCS_AMBIENTE")]
	    public int IdTcsAmbiente
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
	    partial void OnIdTcsAplicativoChanging(int value);
	    partial void OnIdTcsAplicativoChanged();

	    private int _IdTcsAplicativo;

	    [DataMember(IsRequired = true, Name = "IdTcsAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Aplicativo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO")]
	    public int IdTcsAplicativo
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
	    partial void OnIdTcsUsuarioAcessoChanging(int value);
	    partial void OnIdTcsUsuarioAcessoChanged();

	    private int _IdTcsUsuarioAcesso;

	    [DataMember(IsRequired = true, Name = "IdTcsUsuarioAcesso", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Acesso", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO")]
	    public int IdTcsUsuarioAcesso
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
	    partial void OnIdUsuarioChanging(long value);
	    partial void OnIdUsuarioChanged();

	    private long _IdUsuario;

	    [DataMember(IsRequired = true, Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.ID_USUARIO")]
	    public long IdUsuario
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
	    //Extensibility Partial Method Definitions For IndicaAcessoPadrao
	    partial void OnIndicaAcessoPadraoChanging(bool value);
	    partial void OnIndicaAcessoPadraoChanged();

	    private bool _IndicaAcessoPadrao;

	    [DataMember(IsRequired = true, Name = "IndicaAcessoPadrao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Acesso Padrão", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.INDICA_ACESSO_PADRAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.INDICA_ACESSO_PADRAO")]
	    public bool IndicaAcessoPadrao
	    {
	    	    get
	    	    {
	    	          return _IndicaAcessoPadrao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IndicaAcessoPadrao != value)
	    	          {
	    	              this.ValidateProperty("IndicaAcessoPadrao", value);
	    	              this.OnIndicaAcessoPadraoChanging(value);
	    	              this.RaiseDataMemberChanging("IndicaAcessoPadrao");
	    	              this._IndicaAcessoPadrao = value;
	    	              this.RaiseDataMemberChanged("IndicaAcessoPadrao");
	    	              this.OnIndicaAcessoPadraoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IndicaAdministrador
	    partial void OnIndicaAdministradorChanging(bool value);
	    partial void OnIndicaAdministradorChanged();

	    private bool _IndicaAdministrador;

	    [DataMember(IsRequired = true, Name = "IndicaAdministrador", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Administrador", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.INDICA_ADMINISTRADOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.INDICA_ADMINISTRADOR")]
	    public bool IndicaAdministrador
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
	    partial void OnIndicaMultiGpeconChanging(bool value);
	    partial void OnIndicaMultiGpeconChanged();

	    private bool _IndicaMultiGpecon;

	    [DataMember(IsRequired = true, Name = "IndicaMultiGpecon", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Multi Gpecon", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.INDICA_MULTI_GPECON];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.INDICA_MULTI_GPECON")]
	    public bool IndicaMultiGpecon
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
	    partial void OnNomeEmpresaChanging(string value);
	    partial void OnNomeEmpresaChanged();

	    private string _NomeEmpresa;

	    [DataMember(IsRequired = true, Name = "NomeEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Empresa", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[IdLinx];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
	    public string NomeEmpresa
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
	    //Extensibility Partial Method Definitions For Selecionado
	    partial void OnSelecionadoChanging(bool value);
	    partial void OnSelecionadoChanged();

	    private bool _Selecionado;

	    [DataMember(IsRequired = true, Name = "Selecionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Selecionado", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[false];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[false];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="false")]
	    public bool Selecionado
	    {
	    	    get
	    	    {
	    	          return _Selecionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._Selecionado != value)
	    	          {
	    	              this.ValidateProperty("Selecionado", value);
	    	              this.OnSelecionadoChanging(value);
	    	              this.RaiseDataMemberChanging("Selecionado");
	    	              this._Selecionado = value;
	    	              this.RaiseDataMemberChanged("Selecionado");
	    	              this.OnSelecionadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidAplicacao
	    partial void OnUidAplicacaoChanging(Guid value);
	    partial void OnUidAplicacaoChanged();

	    private Guid _UidAplicacao;

	    [DataMember(IsRequired = true, Name = "UidAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Aplicacao", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.UID_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.UID_APLICACAO")]
	    public Guid UidAplicacao
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
	    partial void OnUidEmpresaChanging(Guid value);
	    partial void OnUidEmpresaChanged();

	    private Guid _UidEmpresa;

	    [DataMember(IsRequired = true, Name = "UidEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Empresa", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA")]
	    public Guid UidEmpresa
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
	    //Extensibility Partial Method Definitions For NomeAutenticacao
	    partial void OnNomeAutenticacaoChanging(System.String value);
	    partial void OnNomeAutenticacaoChanged();

	    private System.String _NomeAutenticacao;

	    [DataMember(IsRequired = true, Name = "NomeAutenticacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário Autenticação", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO")]
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
	    //Extensibility Partial Method Definitions For NomeUsuario
	    partial void OnNomeUsuarioChanging(System.String value);
	    partial void OnNomeUsuarioChanged();

	    private System.String _NomeUsuario;

	    [DataMember(IsRequired = true, Name = "NomeUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO")]
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
	    //Extensibility Partial Method Definitions For UidUsuario
	    partial void OnUidUsuarioChanging(Guid value);
	    partial void OnUidUsuarioChanged();

	    private Guid _UidUsuario;

	    [DataMember(IsRequired = true, Name = "UidUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Usuario", Description="", Order = 27, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.UID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.UID_USUARIO")]
	    public Guid UidUsuario
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
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_ACESSO.INDICA_ACESSO_PADRAO", Source = "IndicaAcessoPadrao", Target = "INDICA_ACESSO_PADRAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_ACESSO", RelationPropertyName = "TCS_USUARIO_ACESSO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_ACESSO.INDICA_ADMINISTRADOR", Source = "IndicaAdministrador", Target = "INDICA_ADMINISTRADOR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_ACESSO", RelationPropertyName = "TCS_USUARIO_ACESSO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO", Source = "IdTcsUsuarioAcesso", Target = "ID_TCS_USUARIO_ACESSO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_ACESSO", RelationPropertyName = "TCS_USUARIO_ACESSO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_ACESSO.TCS_AMBIENTE.ID_TCS_AMBIENTE", Source = "IdTcsAmbiente", Target = "ID_TCS_AMBIENTE", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE", RelationPropertyName = "TCS_AMBIENTE" });
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
	//////////////////////// DomainService Class V1 ///////////////////////
	///////////////////////////////////////////////////////////////////////
	[EnableClientAccess()]	
	[DomainIdentifier("ProcessorOverviewConfiguracaoDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class ConfiguracaoDomainService : DomainService, IDataServiceContext 
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

		
	    public ConfiguracaoDomainService() : this("", null, null) { }
	    public ConfiguracaoDomainService(string connectionString) : this(connectionString, null, null) { }
	    public ConfiguracaoDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public ConfiguracaoDomainService(Linx.Framework.Autorizacao.BM.AutorizacaoContext dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public ConfiguracaoDomainService(string connectionString, Linx.Framework.Autorizacao.BM.AutorizacaoContext dataContext, Dictionary<string, string> headers) : base() 
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
 	        var _TcsUsuarioConfiguracaoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioConfiguracao && e.Entity.GetType().Name == "TcsUsuarioConfiguracao" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _TcsUsuarioConfiguracaoElements)
 	           if (((TcsUsuarioConfiguracao)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 
 	        //Remove inconsistent details
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioConfiguracaoAcesso && e.Entity.GetType().Name == "TcsUsuarioConfiguracaoAcesso" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
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
	            
                NomeEmpresa = entity.NOME_EMPRESA
                , IdLinx = entity.ID_LINX
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
	            
	            select new LookUpTcsUsuarioAutenticacao()		
	            {
	            
                IdLinx = entity.TCS_EMPRESA_AUTENTICACAO.ID_LINX
                , IdUsuario = entity.ID_USUARIO
                , NomeUsuario = entity.NOME_USUARIO
                , NomeAutenticacao = entity.NOME_AUTENTICACAO
	            });

	            
            //Inner Group Definition
            if (propertyName.InList("IdLinx"))
            {
               query = (from r in query select new LookUpTcsUsuarioAutenticacao() {
               IdLinx = r.IdLinx
               , IdUsuario = default(long)
               , NomeUsuario = ""
               , NomeAutenticacao = ""
                }).Distinct();
            }
	
		
	
	
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
	
		

	        if (entityName.InList("Linx.Framework.BV.Configuracao.TcsUsuarioConfiguracao"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsUsuarioConfiguracao",
	        			NameSpace = "Linx.Framework.BV.Configuracao",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsUsuarioConfiguracao",
	        			ClearMethodName = "ClearTcsUsuarioConfiguracao",
	        			QueryMethodName  = "GetPagedTcsUsuarioConfiguracao",	
	        			CountingMethodName  = "GetTcsUsuarioConfiguracao" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Configuracao.TcsUsuarioConfiguracao"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Configuracao.TcsUsuarioConfiguracao"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Configuracao.TcsUsuarioConfiguracao", "Linx.Framework.BV.Configuracao.TcsUsuarioConfiguracaoAcesso"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsUsuarioConfiguracaoAcesso" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.Configuracao",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsUsuarioConfiguracao",	
	        			DisplayName = "Ambientes",
	        			ClearMethodName = "ClearTcsUsuarioConfiguracaoAcesso" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsUsuarioConfiguracaoAcesso" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsUsuarioConfiguracaoAcesso" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Configuracao.TcsUsuarioConfiguracaoAcesso"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Configuracao.TcsUsuarioConfiguracaoAcesso" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Configuracao.ConfiguracaoAcesso"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "ConfiguracaoAcesso",
	        			NameSpace = "Linx.Framework.BV.Configuracao",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "ConfiguracaoAcesso",
	        			ClearMethodName = "ClearConfiguracaoAcesso",
	        			QueryMethodName  = "GetPagedConfiguracaoAcesso",	
	        			CountingMethodName  = "GetConfiguracaoAcesso" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Configuracao.ConfiguracaoAcesso"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Configuracao.ConfiguracaoAcesso"), forceAll: forceAll)
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

         		    return new string[] { "Framework_ConfiguracaoClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.ConfiguracaoClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Framework_configuracaoService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.configuracaoService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear TcsUsuarioConfiguracao.
	    public IEnumerable<TcsUsuarioConfiguracao> ClearTcsUsuarioConfiguracao()
	    {
	        List<TcsUsuarioConfiguracao> result = new List<TcsUsuarioConfiguracao>();
	        result.Add(new TcsUsuarioConfiguracao());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear ConfiguracaoAcesso.
	    public IEnumerable<ConfiguracaoAcesso> ClearConfiguracaoAcesso()
	    {
	        List<ConfiguracaoAcesso> result = new List<ConfiguracaoAcesso>();
	        result.Add(new ConfiguracaoAcesso());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsUsuarioConfiguracao.
	    public IQueryable<TcsUsuarioConfiguracao> GetTcsUsuarioConfiguracao()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioConfiguracao> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_AUTENTICACAO
                  let entity0Al1 = entity0.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsUsuarioConfiguracao()		
	            {
	            
                IdLinx = entity0Al1.ID_LINX
                , IdUsuario = entity0.ID_USUARIO
                , NomeAutenticacao = entity0.NOME_AUTENTICACAO
                , NomeEmpresa = entity0Al1.NOME_EMPRESA
                , NomeUsuario = entity0.NOME_USUARIO
                , UidEmpresa = entity0Al1.UID_EMPRESA
                , UidUsuario = entity0.UID_USUARIO
			
                ,TcsUsuarioConfiguracaoAcessoList = 
	                        (from entity1 in entity0.TCS_USUARIO_ACESSO_LISTA
                                  let entity1Al1 = entity1.TCS_AMBIENTE
                                  let entity1Al5 = entity1.TCS_USUARIO_AUTENTICACAO
                                  let entity1Al2 = entity1.TCS_AMBIENTE.TCS_APLICACAO
                                  let entity1Al4 = entity1.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                                  let entity1Al3 = entity1.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
	                        
	                        	
	                        select new TcsUsuarioConfiguracaoAcesso()
	                        {
	                        
                                DescricaoAmbiente = entity1Al1.DESCRICAO_AMBIENTE
                                , DescricaoAplicacao = entity1Al2.DESCRICAO_APLICACAO
                                , DescricaoAplicativo = entity1Al3.DESCRICAO_APLICATIVO
                                , IdAplicacao = entity1Al2.ID_APLICACAO
                                , IdLinx = entity1Al4.ID_LINX
                                , IdTcsAmbiente = entity1Al1.ID_TCS_AMBIENTE
                                , IdTcsAplicativo = entity1Al3.ID_TCS_APLICATIVO
                                , IdTcsUsuarioAcesso = entity1.ID_TCS_USUARIO_ACESSO
                                , IdUsuario = entity1Al5.ID_USUARIO
                                , IndicaAcessoPadrao = entity1.INDICA_ACESSO_PADRAO
                                , IndicaAdministrador = entity1.INDICA_ADMINISTRADOR
                                , IndicaMultiGpecon = entity1.INDICA_MULTI_GPECON
                                , NomeEmpresa = entity1Al4.NOME_EMPRESA
                                , Selecionado = false
                                , UidAplicacao = entity1Al2.UID_APLICACAO
                                , UidEmpresa = entity1Al4.UID_EMPRESA
                                , NomeAutenticacao = entity1Al5.NOME_AUTENTICACAO
                                , NomeUsuario = entity1Al5.NOME_USUARIO
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsUsuarioConfiguracaoAcesso.
	    public IQueryable<TcsUsuarioConfiguracaoAcesso> GetTcsUsuarioConfiguracaoAcesso()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioConfiguracaoAcesso> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_ACESSO
                  let entity0Al1 = entity0.TCS_AMBIENTE
                  let entity0Al5 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_AMBIENTE.TCS_APLICACAO
                  let entity0Al4 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al3 = entity0.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
	            
	            	
	            select new TcsUsuarioConfiguracaoAcesso()		
	            {
	            
                DescricaoAmbiente = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al2.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0Al3.DESCRICAO_APLICATIVO
                , IdAplicacao = entity0Al2.ID_APLICACAO
                , IdLinx = entity0Al4.ID_LINX
                , IdTcsAmbiente = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsAplicativo = entity0Al3.ID_TCS_APLICATIVO
                , IdTcsUsuarioAcesso = entity0.ID_TCS_USUARIO_ACESSO
                , IdUsuario = entity0Al5.ID_USUARIO
                , IndicaAcessoPadrao = entity0.INDICA_ACESSO_PADRAO
                , IndicaAdministrador = entity0.INDICA_ADMINISTRADOR
                , IndicaMultiGpecon = entity0.INDICA_MULTI_GPECON
                , NomeEmpresa = entity0Al4.NOME_EMPRESA
                , Selecionado = false
                , UidAplicacao = entity0Al2.UID_APLICACAO
                , UidEmpresa = entity0Al4.UID_EMPRESA
                , NomeAutenticacao = entity0Al5.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al5.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioConfiguracaoNoAssociations.
	    public IQueryable<TcsUsuarioConfiguracao> GetTcsUsuarioConfiguracaoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioConfiguracao> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_AUTENTICACAO
                  let entity0Al1 = entity0.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsUsuarioConfiguracao()		
	            {
	            
                IdLinx = entity0Al1.ID_LINX
                , IdUsuario = entity0.ID_USUARIO
                , NomeAutenticacao = entity0.NOME_AUTENTICACAO
                , NomeEmpresa = entity0Al1.NOME_EMPRESA
                , NomeUsuario = entity0.NOME_USUARIO
                , UidEmpresa = entity0Al1.UID_EMPRESA
                , UidUsuario = entity0.UID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioConfiguracaoAcessoNoAssociations.
	    public IQueryable<TcsUsuarioConfiguracaoAcesso> GetTcsUsuarioConfiguracaoAcessoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioConfiguracaoAcesso> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_ACESSO
                  let entity0Al1 = entity0.TCS_AMBIENTE
                  let entity0Al5 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_AMBIENTE.TCS_APLICACAO
                  let entity0Al4 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al3 = entity0.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
	            
	            	
	            select new TcsUsuarioConfiguracaoAcesso()		
	            {
	            
                DescricaoAmbiente = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al2.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0Al3.DESCRICAO_APLICATIVO
                , IdAplicacao = entity0Al2.ID_APLICACAO
                , IdLinx = entity0Al4.ID_LINX
                , IdTcsAmbiente = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsAplicativo = entity0Al3.ID_TCS_APLICATIVO
                , IdTcsUsuarioAcesso = entity0.ID_TCS_USUARIO_ACESSO
                , IdUsuario = entity0Al5.ID_USUARIO
                , IndicaAcessoPadrao = entity0.INDICA_ACESSO_PADRAO
                , IndicaAdministrador = entity0.INDICA_ADMINISTRADOR
                , IndicaMultiGpecon = entity0.INDICA_MULTI_GPECON
                , NomeEmpresa = entity0Al4.NOME_EMPRESA
                , Selecionado = false
                , UidAplicacao = entity0Al2.UID_APLICACAO
                , UidEmpresa = entity0Al4.UID_EMPRESA
                , NomeAutenticacao = entity0Al5.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al5.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get ConfiguracaoAcesso.
	    public IEnumerable<ConfiguracaoAcesso> GetConfiguracaoAcesso()
	    {




	
	        IEnumerable<ConfiguracaoAcesso> result = new List<ConfiguracaoAcesso>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get ConfiguracaoAcessoNoAssociations.
	    public IEnumerable<ConfiguracaoAcesso> GetConfiguracaoAcessoNoAssociations()
	    {




	
	        IEnumerable<ConfiguracaoAcesso> result = new List<ConfiguracaoAcesso>();
	  	
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	//Add filtering disabled property for TCS_USUARIO_AUTENTICACAO
	    	string[] bmDisabledTcsUsuarioConfiguracaoList = this.GetEDM().GetFilteringDisabledList("TCS_USUARIO_AUTENTICACAO");
	    	if (bmDisabledTcsUsuarioConfiguracaoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsUsuarioConfiguracaoList.Contains("TCS_USUARIO_AUTENTICACAO.ID_USUARIO"))
	    		{
	    			result.Add("TcsUsuarioConfiguracao|IdUsuario");
	    			result.Add("TcsUsuarioConfiguracao|TCS_USUARIO_AUTENTICACAO.ID_USUARIO");
	    		}
	
	    		if (bmDisabledTcsUsuarioConfiguracaoList.Contains("TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO"))
	    		{
	    			result.Add("TcsUsuarioConfiguracao|NomeAutenticacao");
	    			result.Add("TcsUsuarioConfiguracao|TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO");
	    		}
	
	    		if (bmDisabledTcsUsuarioConfiguracaoList.Contains("TCS_USUARIO_AUTENTICACAO.NOME_USUARIO"))
	    		{
	    			result.Add("TcsUsuarioConfiguracao|NomeUsuario");
	    			result.Add("TcsUsuarioConfiguracao|TCS_USUARIO_AUTENTICACAO.NOME_USUARIO");
	    		}
	
	    		if (bmDisabledTcsUsuarioConfiguracaoList.Contains("TCS_USUARIO_AUTENTICACAO.UID_USUARIO"))
	    		{
	    			result.Add("TcsUsuarioConfiguracao|UidUsuario");
	    			result.Add("TcsUsuarioConfiguracao|TCS_USUARIO_AUTENTICACAO.UID_USUARIO");
	    		}
	    	}
	    	result.Add("TcsUsuarioConfiguracaoAcesso|Selecionado");
	    	result.Add("TcsUsuarioConfiguracaoAcesso|false");
	    	//Add filtering disabled property for TCS_USUARIO_ACESSO
	    	string[] bmDisabledTcsUsuarioConfiguracaoAcessoList = this.GetEDM().GetFilteringDisabledList("TCS_USUARIO_ACESSO");
	    	if (bmDisabledTcsUsuarioConfiguracaoAcessoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsUsuarioConfiguracaoAcessoList.Contains("TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO"))
	    		{
	    			result.Add("TcsUsuarioConfiguracaoAcesso|IdTcsUsuarioAcesso");
	    			result.Add("TcsUsuarioConfiguracaoAcesso|TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO");
	    		}
	
	    		if (bmDisabledTcsUsuarioConfiguracaoAcessoList.Contains("TCS_USUARIO_ACESSO.INDICA_ACESSO_PADRAO"))
	    		{
	    			result.Add("TcsUsuarioConfiguracaoAcesso|IndicaAcessoPadrao");
	    			result.Add("TcsUsuarioConfiguracaoAcesso|TCS_USUARIO_ACESSO.INDICA_ACESSO_PADRAO");
	    		}
	
	    		if (bmDisabledTcsUsuarioConfiguracaoAcessoList.Contains("TCS_USUARIO_ACESSO.INDICA_ADMINISTRADOR"))
	    		{
	    			result.Add("TcsUsuarioConfiguracaoAcesso|IndicaAdministrador");
	    			result.Add("TcsUsuarioConfiguracaoAcesso|TCS_USUARIO_ACESSO.INDICA_ADMINISTRADOR");
	    		}
	
	    		if (bmDisabledTcsUsuarioConfiguracaoAcessoList.Contains("TCS_USUARIO_ACESSO.INDICA_MULTI_GPECON"))
	    		{
	    			result.Add("TcsUsuarioConfiguracaoAcesso|IndicaMultiGpecon");
	    			result.Add("TcsUsuarioConfiguracaoAcesso|TCS_USUARIO_ACESSO.INDICA_MULTI_GPECON");
	    		}
	    	}
	    	result.Add("ConfiguracaoAcesso|UidEmpresa");
	    	result.Add("ConfiguracaoAcesso|TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA");
	    	return result;
	    }

	    #endregion Excluded Filters

	    #region Get By EntitySearchId Methods Definitions.
	
				
	    [Ignore]
	    //Get TcsUsuarioConfiguracao By EntitySearchId.
	    public IQueryable<TcsUsuarioConfiguracao> GetTcsUsuarioConfiguracaoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioConfiguracaoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioConfiguracaoAcesso By EntitySearchId.
	    public IQueryable<TcsUsuarioConfiguracaoAcesso> GetTcsUsuarioConfiguracaoAcessoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioConfiguracaoAcessoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioConfiguracao By EntitySearchId.
	    public IQueryable<TcsUsuarioConfiguracao> GetTcsUsuarioConfiguracaoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioConfiguracaoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioConfiguracaoAcesso By EntitySearchId.
	    public IQueryable<TcsUsuarioConfiguracaoAcesso> GetTcsUsuarioConfiguracaoAcessoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioConfiguracaoAcessoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get ConfiguracaoAcesso By EntitySearchId.
	    public IEnumerable<ConfiguracaoAcesso> GetConfiguracaoAcessoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetConfiguracaoAcessoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get ConfiguracaoAcesso By EntitySearchId.
	    public IEnumerable<ConfiguracaoAcesso> GetConfiguracaoAcessoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetConfiguracaoAcessoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get TcsUsuarioConfiguracao By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioConfiguracao> GetTcsUsuarioConfiguracaoByExample(TcsUsuarioConfiguracao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioConfiguracaoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsUsuarioConfiguracaoAcesso By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioConfiguracaoAcesso> GetTcsUsuarioConfiguracaoAcessoByExample(TcsUsuarioConfiguracaoAcesso entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioConfiguracaoAcessoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsUsuarioConfiguracao By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioConfiguracao> GetTcsUsuarioConfiguracaoByExampleNoAssociations(TcsUsuarioConfiguracao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioConfiguracaoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsUsuarioConfiguracaoAcesso By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioConfiguracaoAcesso> GetTcsUsuarioConfiguracaoAcessoByExampleNoAssociations(TcsUsuarioConfiguracaoAcesso entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioConfiguracaoAcessoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get ConfiguracaoAcesso By Example.
	    [Ignore]
	    public IEnumerable<ConfiguracaoAcesso> GetConfiguracaoAcessoByExample(ConfiguracaoAcesso entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetConfiguracaoAcessoByEntitySearch(queryAnalysis);
	    }
			
	    //Get ConfiguracaoAcesso By Example.
	    [Ignore]
	    public IEnumerable<ConfiguracaoAcesso> GetConfiguracaoAcessoByExampleNoAssociations(ConfiguracaoAcesso entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetConfiguracaoAcessoByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public TcsUsuarioConfiguracao GetTcsUsuarioConfiguracaoByKey(long idUsuario)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsUsuarioConfiguracao");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuario"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idUsuario));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsUsuarioConfiguracaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsUsuarioConfiguracaoAcesso GetTcsUsuarioConfiguracaoAcessoByKey(int idTcsUsuarioAcesso)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsUsuarioConfiguracaoAcesso");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsUsuarioAcesso"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsUsuarioAcesso));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsUsuarioConfiguracaoAcessoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public ConfiguracaoAcesso GetConfiguracaoAcessoByKey(int idLinx)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("ConfiguracaoAcesso");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdLinx"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idLinx));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetConfiguracaoAcessoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioConfiguracaoByEntitySearch.
	    public IQueryable<TcsUsuarioConfiguracao> GetTcsUsuarioConfiguracaoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioConfiguracao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioConfiguracao> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_AUTENTICACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsUsuarioConfiguracao()		
	            {
	            
                IdLinx = entity0Al1.ID_LINX
                , IdUsuario = entity0.ID_USUARIO
                , NomeAutenticacao = entity0.NOME_AUTENTICACAO
                , NomeEmpresa = entity0Al1.NOME_EMPRESA
                , NomeUsuario = entity0.NOME_USUARIO
                , UidEmpresa = entity0Al1.UID_EMPRESA
                , UidUsuario = entity0.UID_USUARIO
			
                ,TcsUsuarioConfiguracaoAcessoList = 
	                        (from entity1 in entity0.TCS_USUARIO_ACESSO_LISTA
                                  let entity1Al1 = entity1.TCS_AMBIENTE
                                  let entity1Al5 = entity1.TCS_USUARIO_AUTENTICACAO
                                  let entity1Al2 = entity1.TCS_AMBIENTE.TCS_APLICACAO
                                  let entity1Al4 = entity1.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                                  let entity1Al3 = entity1.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
	                        
	                        	
	                        select new TcsUsuarioConfiguracaoAcesso()
	                        {
	                        
                                DescricaoAmbiente = entity1Al1.DESCRICAO_AMBIENTE
                                , DescricaoAplicacao = entity1Al2.DESCRICAO_APLICACAO
                                , DescricaoAplicativo = entity1Al3.DESCRICAO_APLICATIVO
                                , IdAplicacao = entity1Al2.ID_APLICACAO
                                , IdLinx = entity1Al4.ID_LINX
                                , IdTcsAmbiente = entity1Al1.ID_TCS_AMBIENTE
                                , IdTcsAplicativo = entity1Al3.ID_TCS_APLICATIVO
                                , IdTcsUsuarioAcesso = entity1.ID_TCS_USUARIO_ACESSO
                                , IdUsuario = entity1Al5.ID_USUARIO
                                , IndicaAcessoPadrao = entity1.INDICA_ACESSO_PADRAO
                                , IndicaAdministrador = entity1.INDICA_ADMINISTRADOR
                                , IndicaMultiGpecon = entity1.INDICA_MULTI_GPECON
                                , NomeEmpresa = entity1Al4.NOME_EMPRESA
                                , Selecionado = false
                                , UidAplicacao = entity1Al2.UID_APLICACAO
                                , UidEmpresa = entity1Al4.UID_EMPRESA
                                , NomeAutenticacao = entity1Al5.NOME_AUTENTICACAO
                                , NomeUsuario = entity1Al5.NOME_USUARIO
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioConfiguracaoAcessoByEntitySearch.
	    public IQueryable<TcsUsuarioConfiguracaoAcesso> GetTcsUsuarioConfiguracaoAcessoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioConfiguracaoAcesso));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioConfiguracaoAcesso> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_ACESSO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_AMBIENTE
                  let entity0Al5 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_AMBIENTE.TCS_APLICACAO
                  let entity0Al4 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al3 = entity0.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
	            
	            	
	            select new TcsUsuarioConfiguracaoAcesso()		
	            {
	            
                DescricaoAmbiente = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al2.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0Al3.DESCRICAO_APLICATIVO
                , IdAplicacao = entity0Al2.ID_APLICACAO
                , IdLinx = entity0Al4.ID_LINX
                , IdTcsAmbiente = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsAplicativo = entity0Al3.ID_TCS_APLICATIVO
                , IdTcsUsuarioAcesso = entity0.ID_TCS_USUARIO_ACESSO
                , IdUsuario = entity0Al5.ID_USUARIO
                , IndicaAcessoPadrao = entity0.INDICA_ACESSO_PADRAO
                , IndicaAdministrador = entity0.INDICA_ADMINISTRADOR
                , IndicaMultiGpecon = entity0.INDICA_MULTI_GPECON
                , NomeEmpresa = entity0Al4.NOME_EMPRESA
                , Selecionado = false
                , UidAplicacao = entity0Al2.UID_APLICACAO
                , UidEmpresa = entity0Al4.UID_EMPRESA
                , NomeAutenticacao = entity0Al5.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al5.NOME_USUARIO
		
	            }
	            );
	
	        SetTcsUsuarioConfiguracaoAcessoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioConfiguracaoByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioConfiguracao> GetTcsUsuarioConfiguracaoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioConfiguracao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioConfiguracao> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_AUTENTICACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsUsuarioConfiguracao()		
	            {
	            
                IdLinx = entity0Al1.ID_LINX
                , IdUsuario = entity0.ID_USUARIO
                , NomeAutenticacao = entity0.NOME_AUTENTICACAO
                , NomeEmpresa = entity0Al1.NOME_EMPRESA
                , NomeUsuario = entity0.NOME_USUARIO
                , UidEmpresa = entity0Al1.UID_EMPRESA
                , UidUsuario = entity0.UID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioConfiguracaoAcessoByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioConfiguracaoAcesso> GetTcsUsuarioConfiguracaoAcessoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioConfiguracaoAcesso));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioConfiguracaoAcesso> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_ACESSO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_AMBIENTE
                  let entity0Al5 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_AMBIENTE.TCS_APLICACAO
                  let entity0Al4 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al3 = entity0.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
	            
	            	
	            select new TcsUsuarioConfiguracaoAcesso()		
	            {
	            
                DescricaoAmbiente = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al2.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0Al3.DESCRICAO_APLICATIVO
                , IdAplicacao = entity0Al2.ID_APLICACAO
                , IdLinx = entity0Al4.ID_LINX
                , IdTcsAmbiente = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsAplicativo = entity0Al3.ID_TCS_APLICATIVO
                , IdTcsUsuarioAcesso = entity0.ID_TCS_USUARIO_ACESSO
                , IdUsuario = entity0Al5.ID_USUARIO
                , IndicaAcessoPadrao = entity0.INDICA_ACESSO_PADRAO
                , IndicaAdministrador = entity0.INDICA_ADMINISTRADOR
                , IndicaMultiGpecon = entity0.INDICA_MULTI_GPECON
                , NomeEmpresa = entity0Al4.NOME_EMPRESA
                , Selecionado = false
                , UidAplicacao = entity0Al2.UID_APLICACAO
                , UidEmpresa = entity0Al4.UID_EMPRESA
                , NomeAutenticacao = entity0Al5.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al5.NOME_USUARIO
		
	            }
	            );
	
	        SetTcsUsuarioConfiguracaoAcessoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioConfiguracaoAcessoParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioConfiguracaoAcessoParentComposition> GetTcsUsuarioConfiguracaoAcessoParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "TCS_USUARIO_AUTENTICACAO", "TCS_USUARIO_ACESSO", "TCS_USUARIO_AUTENTICACAO", typeof(TcsUsuarioConfiguracaoAcessoParentComposition));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioConfiguracaoAcessoParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_ACESSO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_AMBIENTE
                  let entity0Al5 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_AMBIENTE.TCS_APLICACAO
                  let entity0Al4 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al3 = entity0.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
	            
	            	
	            select new TcsUsuarioConfiguracaoAcessoParentComposition()		
	            {
	            
                DescricaoAmbiente = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al2.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0Al3.DESCRICAO_APLICATIVO
                , IdAplicacao = entity0Al2.ID_APLICACAO
                , IdLinx = entity0Al4.ID_LINX
                , IdTcsAmbiente = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsAplicativo = entity0Al3.ID_TCS_APLICATIVO
                , IdTcsUsuarioAcesso = entity0.ID_TCS_USUARIO_ACESSO
                , IdUsuario = entity0Al5.ID_USUARIO
                , IndicaAcessoPadrao = entity0.INDICA_ACESSO_PADRAO
                , IndicaAdministrador = entity0.INDICA_ADMINISTRADOR
                , IndicaMultiGpecon = entity0.INDICA_MULTI_GPECON
                , NomeEmpresa = entity0Al4.NOME_EMPRESA
                , Selecionado = false
                , UidAplicacao = entity0Al2.UID_APLICACAO
                , UidEmpresa = entity0Al4.UID_EMPRESA
                , NomeAutenticacao = entity0Al5.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al5.NOME_USUARIO
                //TcsUsuarioConfiguracao Properties.
                , UidUsuario = entity0.TCS_USUARIO_AUTENTICACAO.UID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
	
	    [Ignore()]
	    private void SetTcsUsuarioConfiguracaoAcessoBusinessFilter(ref IQueryable<TcsUsuarioConfiguracaoAcesso> query, List<EntitySearch> entitySearchList)
	    {
	    		int idxElement;
	    		string operatorValue;
	    		object value;
	    		//Get query by functions
	    		if (entitySearchList.Count > 0)
	    		{
	    			foreach (EntitySearch search in entitySearchList.Where(e => e.EntityName == "TcsUsuarioConfiguracaoAcesso"))
	    			{

	
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "Selecionado" || e.Value.ToString() == "false")))
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
	    										bool tmpSelecionado1 = (bool)value;
	    										query = from r in query where r.Selecionado == tmpSelecionado1 select r;
	    										break;
	    									case "!=":
	    										bool tmpSelecionado2 = (bool)value;
	    										query = from r in query where r.Selecionado != tmpSelecionado2 select r;
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
	    //Get ConfiguracaoAcessoByEntitySearch.
	    public IEnumerable<ConfiguracaoAcesso> GetConfiguracaoAcessoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<ConfiguracaoAcesso> result = new List<ConfiguracaoAcesso>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get ConfiguracaoAcessoByEntitySearchNoAssociations.
	    public IEnumerable<ConfiguracaoAcesso> GetConfiguracaoAcessoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<ConfiguracaoAcesso> result = new List<ConfiguracaoAcesso>();
	  	
	
	        	

	
	        return result;
	    }
	
	    [Ignore()]
	    private void SetConfiguracaoAcessoBusinessFilter(ref IQueryable<ConfiguracaoAcesso> query, List<EntitySearch> entitySearchList)
	    {
	    		int idxElement;
	    		string operatorValue;
	    		object value;
	    		//Get query by functions
	    		if (entitySearchList.Count > 0)
	    		{
	    			foreach (EntitySearch search in entitySearchList.Where(e => e.EntityName == "ConfiguracaoAcesso"))
	    			{

	
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "UidEmpresa" || e.Value.ToString() == "TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA")))
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
	    										Guid tmpUidEmpresa1 = (Guid)value;
	    										query = from r in query where r.UidEmpresa == tmpUidEmpresa1 select r;
	    										break;
	    									case "!=":
	    										Guid tmpUidEmpresa2 = (Guid)value;
	    										query = from r in query where r.UidEmpresa != tmpUidEmpresa2 select r;
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
	    //Get PagedTcsUsuarioConfiguracao.
	    public IQueryable<TcsUsuarioConfiguracao> GetPagedTcsUsuarioConfiguracao(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioConfiguracao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioConfiguracao> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_AUTENTICACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_EMPRESA_AUTENTICACAO
                orderby entity0.ID_USUARIO ascending
	            
	            	
	            select new TcsUsuarioConfiguracao()		
	            {
	            
                IdLinx = entity0Al1.ID_LINX
                , IdUsuario = entity0.ID_USUARIO
                , NomeAutenticacao = entity0.NOME_AUTENTICACAO
                , NomeEmpresa = entity0Al1.NOME_EMPRESA
                , NomeUsuario = entity0.NOME_USUARIO
                , UidEmpresa = entity0Al1.UID_EMPRESA
                , UidUsuario = entity0.UID_USUARIO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsUsuarioConfiguracaoAcesso.
	    public IQueryable<TcsUsuarioConfiguracaoAcesso> GetPagedTcsUsuarioConfiguracaoAcesso(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioConfiguracaoAcesso));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioConfiguracaoAcesso> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_ACESSO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_AMBIENTE
                  let entity0Al5 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_AMBIENTE.TCS_APLICACAO
                  let entity0Al4 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al3 = entity0.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
                orderby entity0.ID_TCS_USUARIO_ACESSO ascending
	            
	            	
	            select new TcsUsuarioConfiguracaoAcesso()		
	            {
	            
                DescricaoAmbiente = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al2.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0Al3.DESCRICAO_APLICATIVO
                , IdAplicacao = entity0Al2.ID_APLICACAO
                , IdLinx = entity0Al4.ID_LINX
                , IdTcsAmbiente = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsAplicativo = entity0Al3.ID_TCS_APLICATIVO
                , IdTcsUsuarioAcesso = entity0.ID_TCS_USUARIO_ACESSO
                , IdUsuario = entity0Al5.ID_USUARIO
                , IndicaAcessoPadrao = entity0.INDICA_ACESSO_PADRAO
                , IndicaAdministrador = entity0.INDICA_ADMINISTRADOR
                , IndicaMultiGpecon = entity0.INDICA_MULTI_GPECON
                , NomeEmpresa = entity0Al4.NOME_EMPRESA
                , Selecionado = false
                , UidAplicacao = entity0Al2.UID_APLICACAO
                , UidEmpresa = entity0Al4.UID_EMPRESA
                , NomeAutenticacao = entity0Al5.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al5.NOME_USUARIO
		
	            }
	            ).Skip(skip).Take(take);
	
	        SetTcsUsuarioConfiguracaoAcessoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsUsuarioConfiguracaoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioConfiguracao));
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
	    public int GetTcsUsuarioConfiguracaoAcessoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioConfiguracaoAcesso));
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
	    //Get PagedConfiguracaoAcesso.
	    public IEnumerable<ConfiguracaoAcesso> GetPagedConfiguracaoAcesso(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<ConfiguracaoAcesso> result = new List<ConfiguracaoAcesso>();
	  	
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetConfiguracaoAcessoCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    
	    [Update()]	
	    //Update TcsUsuarioConfiguracao.
	    public void UpdateTcsUsuarioConfiguracao(TcsUsuarioConfiguracao entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert TcsUsuarioConfiguracao.
	    public void InsertTcsUsuarioConfiguracao(TcsUsuarioConfiguracao entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete TcsUsuarioConfiguracao.
	    public void DeleteTcsUsuarioConfiguracao(TcsUsuarioConfiguracao entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsUsuarioConfiguracaoAcesso.
	    public void UpdateTcsUsuarioConfiguracaoAcesso(TcsUsuarioConfiguracaoAcesso entity)
	    {



	
	        if (entity.TcsUsuarioConfiguracao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsUsuarioConfiguracao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsUsuarioConfiguracao); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsUsuarioConfiguracaoAcesso.
	    public void InsertTcsUsuarioConfiguracaoAcesso(TcsUsuarioConfiguracaoAcesso entity)
	    {



	
	        if (entity.TcsUsuarioConfiguracao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsUsuarioConfiguracao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsUsuarioConfiguracao);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsUsuarioConfiguracaoAcesso.
	    public void DeleteTcsUsuarioConfiguracaoAcesso(TcsUsuarioConfiguracaoAcesso entity)
	    {



	
	        if (entity.TcsUsuarioConfiguracao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsUsuarioConfiguracao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsUsuarioConfiguracao);
	            

	
	        }

	
	    }
		
			
	    
	    [Update()]	
	    //Update ConfiguracaoAcesso.
	    public void UpdateConfiguracaoAcesso(ConfiguracaoAcesso entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert ConfiguracaoAcesso.
	    public void InsertConfiguracaoAcesso(ConfiguracaoAcesso entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete ConfiguracaoAcesso.
	    public void DeleteConfiguracaoAcesso(ConfiguracaoAcesso entity)
	    {



	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}