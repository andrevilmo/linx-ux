					
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

namespace Linx.Framework.BV.Modulo
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_MODULO.ID_MODULO", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Módulos];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsModulo,TcsModulo.TcsModuloMenu,TcsModuloMenu.TcsTransacaoMenu,TcsModulo.TcsModuloDoGrupo];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdModulo];ReadOnly[false];Entities[TCS_MODULO:IdModulo];SubQueryInfo[];EdmEntityName[TCS_MODULO];EntityRelations[];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsModulo")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Modulo.TcsModulo")]
	public partial class TcsModulo : Linx.Data.Entity
	{

	

	    public TcsModulo() : this(true) { }

	    public TcsModulo(bool setDefaults) 
	    {

	        if (setDefaults)
	        {
	        	        LxCorFundo = 7;
	        	        NomeTabela = "";
	        }	

	    }

			
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.TcsModuloMenuList != null && this.TcsModuloMenuList.Count() > 0)
	      {
	         foreach (var entity in this.TcsModuloMenuList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      if (this.TcsModuloDoGrupoList != null && this.TcsModuloDoGrupoList.Count() > 0)
	      {
	         foreach (var entity in this.TcsModuloDoGrupoList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.TcsModuloMenuList != null)
	      {
	         foreach (var detail in this.TcsModuloMenuList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsModuloMenuList = null;
	      }
	      if (this.TcsModuloDoGrupoList != null)
	      {
	         foreach (var detail in this.TcsModuloDoGrupoList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsModuloDoGrupoList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(ModuloDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("TcsModuloMenu"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsModuloMenu");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdModulo"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdModulo));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsModuloMenu and all sub-details
	         if (this.TcsModuloMenuList == null || this.TcsModuloMenuList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsModuloMenuList = context.GetPagedTcsModuloMenu(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsModuloMenuList = (from r in context.GetTcsModuloMenuByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	         foreach(TcsModuloMenu detail in this.TcsModuloMenuList)
	         {
	             detail.FillDetails(context, serializedEntitySearch, jEntitySearch, viewNames, take);
	         }
	      }
	      if (viewNames == null || viewNames.Contains("TcsModuloDoGrupo"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsModuloDoGrupo");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdModulo"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdModulo));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsModuloDoGrupo and all sub-details
	         if (this.TcsModuloDoGrupoList == null || this.TcsModuloDoGrupoList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsModuloDoGrupoList = context.GetPagedTcsModuloDoGrupo(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsModuloDoGrupoList = (from r in context.GetTcsModuloDoGrupoByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _TcsModuloMenuElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsModuloMenu && ((TcsModuloMenu)e.Entity).TcsModulo == null && e.Associations == null && e.OriginalAssociations == null && ((TcsModuloMenu)e.Entity).IdModulo == this.IdModulo).ToList();
 	      if (_TcsModuloMenuElements.Count > 0 && this.TcsModuloMenuList.Count() == 0)
 	      {
 	          this.TcsModuloMenuList = _TcsModuloMenuElements.Select(e => (TcsModuloMenu)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsModuloMenuElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsModuloMenu)detail.Entity).TcsModulo = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsModulo", new int[] { masterIndex });
 	              ((TcsModuloMenu)detail.Entity).AdjustHierarchyForSaving(detail, changeSet);
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsModuloMenuList", indexDetails.ToArray());
 	      }
 
 	      var _TcsModuloDoGrupoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsModuloDoGrupo && ((TcsModuloDoGrupo)e.Entity).TcsModulo == null && e.Associations == null && e.OriginalAssociations == null && ((TcsModuloDoGrupo)e.Entity).IdModulo == this.IdModulo).ToList();
 	      if (_TcsModuloDoGrupoElements.Count > 0 && this.TcsModuloDoGrupoList.Count() == 0)
 	      {
 	          this.TcsModuloDoGrupoList = _TcsModuloDoGrupoElements.Select(e => (TcsModuloDoGrupo)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsModuloDoGrupoElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsModuloDoGrupo)detail.Entity).TcsModulo = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsModulo", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsModuloDoGrupoList", indexDetails.ToArray());
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
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO.DESC_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO.DESC_MODULO")]
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
	    //Extensibility Partial Method Definitions For Icone
	    partial void OnIconeChanging(System.String value);
	    partial void OnIconeChanged();

	    private System.String _Icone;

	    [DataMember(Name = "Icone", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ícone", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO.ICONE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO.ICONE")]
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
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO.ID_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO.ID_MODULO")]
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

	    [DataMember(Name = "IdTcsAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Aplicativo", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO.ID_TCS_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO.ID_TCS_APLICATIVO")]
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
	    [Display(Name = "Inativo", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO.INATIVO")]
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
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[CorFundo];KpiName[];KpiRelatedAttribute[];DefaultValue[7];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_MODULO.LX_COR_FUNDO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO.LX_COR_FUNDO")]
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
	    [Display(Name = "Descrição", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO.NOME_CURTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO.NOME_CURTO")]
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
	    //Extensibility Partial Method Definitions For NomeTabela
	    partial void OnNomeTabelaChanging(string value);
	    partial void OnNomeTabelaChanged();

	    private string _NomeTabela;

	    [DataMember(Name = "NomeTabela", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[\"\"];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[''];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="\"\"")]
	    public string NomeTabela
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
	    //Extensibility Partial Method Definitions For OrdemNavegacao
	    partial void OnOrdemNavegacaoChanging(Byte value);
	    partial void OnOrdemNavegacaoChanged();

	    private Byte _OrdemNavegacao;

	    [DataMember(IsRequired = true, Name = "OrdemNavegacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ordem", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO.ORDEM_NAVEGACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO.ORDEM_NAVEGACAO")]
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
	    //Extensibility Partial Method Definitions For DescricaoAplicativo
	    partial void OnDescricaoAplicativoChanging(string value);
	    partial void OnDescricaoAplicativoChanged();

	    private string _DescricaoAplicativo;

	    [DataMember(IsRequired = true, Name = "DescricaoAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicativo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
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
	 
		
	    private IEnumerable<TcsModuloDoGrupo> _TcsModuloDoGrupoList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsModulo_TcsModuloDoGrupo", "IdModulo", "IdModulo", IsForeignKey=false)]
	    [DataMember(Name = "TcsModuloDoGrupoList", EmitDefaultValue = true)]
	    public IEnumerable<TcsModuloDoGrupo> TcsModuloDoGrupoList
	    {
	        get
	        {
	
	            if (this._TcsModuloDoGrupoList == null)
	            	this._TcsModuloDoGrupoList = new List<TcsModuloDoGrupo>();
	
	            return this._TcsModuloDoGrupoList;
	        }
	        set
	        {
	            if (this._TcsModuloDoGrupoList != value)
	            {
	                this._TcsModuloDoGrupoList = value;
	                this.RaisePropertyChanged("TcsModuloDoGrupoList");
	            }
	        }
	    }	 
		
	    private IEnumerable<TcsModuloMenu> _TcsModuloMenuList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsModulo_TcsModuloMenu", "IdModulo", "IdModulo", IsForeignKey=false)]
	    [DataMember(Name = "TcsModuloMenuList", EmitDefaultValue = true)]
	    public IEnumerable<TcsModuloMenu> TcsModuloMenuList
	    {
	        get
	        {
	
	            if (this._TcsModuloMenuList == null)
	            	this._TcsModuloMenuList = new List<TcsModuloMenu>();
	
	            return this._TcsModuloMenuList;
	        }
	        set
	        {
	            if (this._TcsModuloMenuList != value)
	            {
	                this._TcsModuloMenuList = value;
	                this.RaisePropertyChanged("TcsModuloMenuList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_MODULO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_MODULO), QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO.ICONE", Source = "Icone", Target = "ICONE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO", RelationPropertyName = "TCS_MODULO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO.INATIVO", Source = "Inativo", Target = "INATIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO", RelationPropertyName = "TCS_MODULO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO.ID_MODULO", Source = "IdModulo", Target = "ID_MODULO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO", RelationPropertyName = "TCS_MODULO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO.NOME_CURTO", Source = "NomeCurto", Target = "NOME_CURTO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO", RelationPropertyName = "TCS_MODULO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO.DESC_MODULO", Source = "DescModulo", Target = "DESC_MODULO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO", RelationPropertyName = "TCS_MODULO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO.LX_COR_FUNDO", Source = "LxCorFundo", Target = "LX_COR_FUNDO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO", RelationPropertyName = "TCS_MODULO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO.ORDEM_NAVEGACAO", Source = "OrdemNavegacao", Target = "ORDEM_NAVEGACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO", RelationPropertyName = "TCS_MODULO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO.ID_TCS_APLICATIVO", Source = "IdTcsAplicativo", Target = "ID_TCS_APLICATIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO", RelationPropertyName = "TCS_MODULO" });

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
	             Linx.Framework.BV.BusinessMediaHelper.SyncMedia("TCS_MODULO", this.IdModulo, null, this.TableMedia.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(e => new Guid(e)).ToList());
	         }
	         else if (operation == DomainOperation.Delete) {
	             Linx.Framework.BV.BusinessMediaHelper.SyncMedia("TCS_MODULO", this.IdModulo, null, new List<Guid>() { Guid.Empty });
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

		

	[LinxPublicationView(PrimaryKeys="TCS_MODULO_MENU.ID_MODULO_MENU", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Menus];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdModuloMenu];ReadOnly[false];Entities[TCS_MODULO_MENU:IdModuloMenu];SubQueryInfo[Select 1 From #ParentAlias#.TCS_MODULO_MENU_LISTA as #Alias#];EdmEntityName[TCS_MODULO_MENU];EntityRelations[TCS_MODULO(TCS_MODULO)#MODULO_MENU_SUPERIOR(TCS_MODULO_MENU)];EdmParentEntityName[TCS_MODULO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsModuloMenu")]
	[Serializable()]
	public partial class TcsModuloMenu : Linx.Data.Entity
	{

	

	    public TcsModuloMenu() : this(true) { }

	    public TcsModuloMenu(bool setDefaults) 
	    {

	        if (setDefaults)
	        {
	        	        LxCorFundo = 7;
	        }	

	    }

			
	

	
	    #region Load Data Parent
		

	    public void LoadParent(ModuloDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsModulo");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdModulo"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdModulo));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsModulo
	         this.TcsModulo = (from r in context.GetTcsModuloByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Load Data Parent

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.TcsTransacaoMenuList != null && this.TcsTransacaoMenuList.Count() > 0)
	      {
	         foreach (var entity in this.TcsTransacaoMenuList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.TcsTransacaoMenuList != null)
	      {
	         foreach (var detail in this.TcsTransacaoMenuList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsTransacaoMenuList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(ModuloDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("TcsTransacaoMenu"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsTransacaoMenu");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdModuloMenu"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdModuloMenu));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsTransacaoMenu and all sub-details
	         if (this.TcsTransacaoMenuList == null || this.TcsTransacaoMenuList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsTransacaoMenuList = context.GetPagedTcsTransacaoMenu(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsTransacaoMenuList = (from r in context.GetTcsTransacaoMenuByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _TcsTransacaoMenuElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsTransacaoMenu && ((TcsTransacaoMenu)e.Entity).TcsModuloMenu == null && e.Associations == null && e.OriginalAssociations == null && ((TcsTransacaoMenu)e.Entity).IdModuloMenu == this.IdModuloMenu).ToList();
 	      if (_TcsTransacaoMenuElements.Count > 0 && this.TcsTransacaoMenuList.Count() == 0)
 	      {
 	          this.TcsTransacaoMenuList = _TcsTransacaoMenuElements.Select(e => (TcsTransacaoMenu)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsTransacaoMenuElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsTransacaoMenu)detail.Entity).TcsModuloMenu = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsModuloMenu", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsTransacaoMenuList", indexDetails.ToArray());
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
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_MODULO_MENU.TCS_MODULO.DESC_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_MENU.TCS_MODULO.DESC_MODULO")]
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

	    [DataMember(IsRequired = true, Name = "DescModuloMenu", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição Detalhada", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    //Extensibility Partial Method Definitions For DescModuloMenuSuperior
	    partial void OnDescModuloMenuSuperiorChanging(System.String value);
	    partial void OnDescModuloMenuSuperiorChanged();

	    private System.String _DescModuloMenuSuperior;

	    [DataMember(Name = "DescModuloMenuSuperior", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Menu Superior", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpModuloMenuSuperior];LookUpTitle[Seleção de (Menu Superior)];LookUpQuery[executeLookUpModuloMenuSuperior];LookUpFinalize[finalizeLookUpModuloMenuSuperior];LookUpDisplayColumns[{\"DescModuloMenuSuperior\" : \"Menu Superior\", \"IdModuloMenuSuperior\" : \"Id Modulo Menu\"}];LookUpColumns[{\"DescModuloMenuSuperior\" : true, \"IdModuloMenuSuperior\" : false}];FilterDataKey[TCS_MODULO_MENU.MODULO_MENU_SUPERIOR.DESC_MODULO_MENU];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescModuloMenuSuperior#false##100:0##Menu Superior#1#true##::LookUpModuloMenuSuperior##false#false#MODULO_MENU_SUPERIOR#TCS_MODULO_MENU#Linx.Framework.BV.Modulo#IQueryable#DescModuloMenu[DescModuloMenu]##true#false", EdmKey="TCS_MODULO_MENU.MODULO_MENU_SUPERIOR.DESC_MODULO_MENU")]
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
	    //Extensibility Partial Method Definitions For Icone
	    partial void OnIconeChanging(System.String value);
	    partial void OnIconeChanged();

	    private System.String _Icone;

	    [DataMember(Name = "Icone", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ícone", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO_MENU.ICONE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_MENU.ICONE")]
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
	    //Extensibility Partial Method Definitions For IdModuloMenuSuperior
	    partial void OnIdModuloMenuSuperiorChanging(System.Nullable<Int64> value);
	    partial void OnIdModuloMenuSuperiorChanged();

	    private System.Nullable<Int64> _IdModuloMenuSuperior;

	    [DataMember(Name = "IdModuloMenuSuperior", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo Menu", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpModuloMenuSuperior];LookUpTitle[Seleção de (Id Modulo Menu)];LookUpQuery[executeLookUpModuloMenuSuperior];LookUpFinalize[finalizeLookUpModuloMenuSuperior];LookUpDisplayColumns[{\"DescModuloMenuSuperior\" : \"Menu Superior\", \"IdModuloMenuSuperior\" : \"Id Modulo Menu\"}];LookUpColumns[{\"DescModuloMenuSuperior\" : true, \"IdModuloMenuSuperior\" : false}];FilterDataKey[TCS_MODULO_MENU.MODULO_MENU_SUPERIOR.ID_MODULO_MENU];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<Int64>#IdModuloMenuSuperior#true##24:0##Id Modulo Menu#3#false##::LookUpModuloMenuSuperior##false#false#MODULO_MENU_SUPERIOR#TCS_MODULO_MENU#Linx.Framework.BV.Modulo#IQueryable#DescModuloMenu[DescModuloMenu]##true#false", EdmKey="TCS_MODULO_MENU.MODULO_MENU_SUPERIOR.ID_MODULO_MENU")]
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

	    [DataMember(Name = "IdTcsAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Aplicativo", Description="", Order = 4, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO_MENU.TCS_MODULO.ID_TCS_APLICATIVO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For LxCorFundo
	    partial void OnLxCorFundoChanging(System.Nullable<System.Int32> value);
	    partial void OnLxCorFundoChanged();

	    private System.Nullable<System.Int32> _LxCorFundo;

	    [DataMember(Name = "LxCorFundo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cor de Fundo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[CorFundo];KpiName[];KpiRelatedAttribute[];DefaultValue[7];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_MODULO_MENU.LX_COR_FUNDO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_MENU.LX_COR_FUNDO")]
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
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO_MENU.NOME_CURTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_MENU.NOME_CURTO")]
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
	    //Extensibility Partial Method Definitions For NomeTabela
	    partial void OnNomeTabelaChanging(string value);
	    partial void OnNomeTabelaChanged();

	    private string _NomeTabela;

	    [DataMember(Name = "NomeTabela", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey['TCS_MODULO_MENU'];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="\"TCS_MODULO_MENU\"")]
	    public string NomeTabela
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
	    //Extensibility Partial Method Definitions For OrdemNavegacao
	    partial void OnOrdemNavegacaoChanging(Byte value);
	    partial void OnOrdemNavegacaoChanged();

	    private Byte _OrdemNavegacao;

	    [DataMember(IsRequired = true, Name = "OrdemNavegacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ordem", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO_MENU.ORDEM_NAVEGACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_MENU.ORDEM_NAVEGACAO")]
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
	 
	    private TcsModulo _TcsModulo;
	    [DataMember(Name = "TcsModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsModulo_TcsModuloMenu", "IdModulo", "IdModulo", IsForeignKey=true)]
	    public TcsModulo TcsModulo
	    {
	        get
	        {
	            return this._TcsModulo;
	        }
	        set
	        {
	            if (this._TcsModulo != value)
	            {
	                this._TcsModulo = value;
	                this.RaisePropertyChanged("TcsModuloList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<TcsTransacaoMenu> _TcsTransacaoMenuList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsModuloMenu_TcsTransacaoMenu", "IdModuloMenu", "IdModuloMenu", IsForeignKey=false)]
	    [DataMember(Name = "TcsTransacaoMenuList", EmitDefaultValue = true)]
	    public IEnumerable<TcsTransacaoMenu> TcsTransacaoMenuList
	    {
	        get
	        {
	
	            if (this._TcsTransacaoMenuList == null)
	            	this._TcsTransacaoMenuList = new List<TcsTransacaoMenu>();
	
	            return this._TcsTransacaoMenuList;
	        }
	        set
	        {
	            if (this._TcsTransacaoMenuList != value)
	            {
	                this._TcsTransacaoMenuList = value;
	                this.RaisePropertyChanged("TcsTransacaoMenuList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
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

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_MENU.ICONE", Source = "Icone", Target = "ICONE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO_MENU", RelationPropertyName = "TCS_MODULO_MENU" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_MENU.NOME_CURTO", Source = "NomeCurto", Target = "NOME_CURTO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO_MENU", RelationPropertyName = "TCS_MODULO_MENU" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_MENU.LX_COR_FUNDO", Source = "LxCorFundo", Target = "LX_COR_FUNDO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO_MENU", RelationPropertyName = "TCS_MODULO_MENU" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_MENU.ID_MODULO_MENU", Source = "IdModuloMenu", Target = "ID_MODULO_MENU", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO_MENU", RelationPropertyName = "TCS_MODULO_MENU" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_MENU.ORDEM_NAVEGACAO", Source = "OrdemNavegacao", Target = "ORDEM_NAVEGACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO_MENU", RelationPropertyName = "TCS_MODULO_MENU" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_MENU.DESC_MODULO_MENU", Source = "DescModuloMenu", Target = "DESC_MODULO_MENU", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO_MENU", RelationPropertyName = "TCS_MODULO_MENU" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_MENU.TCS_MODULO.ID_MODULO", Source = "IdModulo", Target = "ID_MODULO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO", RelationPropertyName = "TCS_MODULO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_MENU.MODULO_MENU_SUPERIOR.ID_MODULO_MENU", Source = "IdModuloMenuSuperior", Target = "ID_MODULO_MENU", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO_MENU", RelationPropertyName = "MODULO_MENU_SUPERIOR" });

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
	             Linx.Framework.BV.BusinessMediaHelper.SyncMedia("TCS_MODULO_MENU", this.IdModuloMenu, null, this.TableMedia.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(e => new Guid(e)).ToList());
	         }
	         else if (operation == DomainOperation.Delete) {
	             Linx.Framework.BV.BusinessMediaHelper.SyncMedia("TCS_MODULO_MENU", this.IdModuloMenu, null, new List<Guid>() { Guid.Empty });
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

		

	[LinxPublicationView(PrimaryKeys="TCS_TRANSACAO_MENU.ID_TCS_TRANSACAO_MENU", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Transação];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsTransacaoMenu];ReadOnly[false];Entities[TCS_TRANSACAO_MENU:IdTcsTransacaoMenu];SubQueryInfo[Select 1 From #ParentAlias#. as #Alias#];EdmEntityName[TCS_TRANSACAO_MENU];EntityRelations[];EdmParentEntityName[TCS_MODULO_MENU];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsTransacaoMenu")]
	[Serializable()]
	public partial class TcsTransacaoMenu : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(ModuloDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsModuloMenu");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdModuloMenu"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdModuloMenu));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsModuloMenu
	         this.TcsModuloMenu = (from r in context.GetTcsModuloMenuByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_TRANSACAO_MENU.ID_MODULO_MENU];IsMeasure[false]")]
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
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsTransacaoMenu];LookUpTitle[Seleção de (Id Transacao)];LookUpQuery[executeLookUpTcsTransacaoMenu];LookUpFinalize[finalizeLookUpTcsTransacaoMenu];LookUpDisplayColumns[{\"IdTransacao\" : \"\", \"DescTransacao\" : \"Transação\"}];LookUpColumns[{\"IdTransacao\" : false, \"DescTransacao\" : true}];FilterDataKey[TCS_TRANSACAO_MENU.ID_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdTransacao#true##12###0#false##::LookUpTcsTransacaoMenu##true#false###Linx.Framework.BV.Modulo#IQueryable###true#true", EdmKey="TCS_TRANSACAO_MENU.ID_TRANSACAO")]
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
	    [Display(Name = "Inativo", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    [Display(Name = "Ordem", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    //Extensibility Partial Method Definitions For DescTransacao
	    partial void OnDescTransacaoChanging(string value);
	    partial void OnDescTransacaoChanged();

	    private string _DescTransacao;

	    [DataMember(IsRequired = true, Name = "DescTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Transação", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[''];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#DescTransacao#false##60:0##Transação#1#true##::LookUpTcsTransacaoMenu##true#false###Linx.Framework.BV.Modulo#IQueryable###true#true", EdmKey="\"\"")]
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

	    private Int32 _TemporaryIdTcsTransacaoMenu;
	    [DataMember(Name = "TemporaryIdTcsTransacaoMenu", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Transacao Menu (Tmp)", Description="Temporary Key", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdTcsTransacaoMenu
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTcsTransacaoMenu.IsNullOrEmpty())
	    	                this._TemporaryIdTcsTransacaoMenu = this._IdTcsTransacaoMenu;
	    	          return this._TemporaryIdTcsTransacaoMenu;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTcsTransacaoMenu != value)
	    	              this._TemporaryIdTcsTransacaoMenu = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsModuloMenu _TcsModuloMenu;
	    [DataMember(Name = "TcsModuloMenu", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsModuloMenu_TcsTransacaoMenu", "IdModuloMenu", "IdModuloMenu", IsForeignKey=true)]
	    public TcsModuloMenu TcsModuloMenu
	    {
	        get
	        {
	            return this._TcsModuloMenu;
	        }
	        set
	        {
	            if (this._TcsModuloMenu != value)
	            {
	                this._TcsModuloMenu = value;
	                this.RaisePropertyChanged("TcsModuloMenuList");
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
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO_MENU.ID_TRANSACAO", Source = "IdTransacao", Target = "ID_TRANSACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO_MENU", RelationPropertyName = "TCS_TRANSACAO_MENU" });
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

		

	[LinxPublicationView(PrimaryKeys="TCS_MODULO_DO_GRUPO.TCS_MODULO_GRUPO.ID_GRUPO_MODULO,TCS_MODULO_DO_GRUPO.ID_MODULO", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Grupo];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdModulo];ReadOnly[false];Entities[TCS_MODULO_DO_GRUPO:IdModulo];SubQueryInfo[Select 1 From #ParentAlias#. as #Alias#];EdmEntityName[TCS_MODULO_DO_GRUPO];EntityRelations[TCS_MODULO_GRUPO(TCS_MODULO_GRUPO)];EdmParentEntityName[TCS_MODULO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsModuloDoGrupo")]
	[Serializable()]
	public partial class TcsModuloDoGrupo : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(ModuloDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsModulo");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdModulo"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdModulo));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsModulo
	         this.TcsModulo = (from r in context.GetTcsModuloByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For DescGrupoModulo
	    partial void OnDescGrupoModuloChanging(System.String value);
	    partial void OnDescGrupoModuloChanged();

	    private System.String _DescGrupoModulo;

	    [DataMember(IsRequired = true, Name = "DescGrupoModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsModuloGrupo];LookUpTitle[Seleção de (Descrição)];LookUpQuery[executeLookUpTcsModuloGrupo];LookUpFinalize[finalizeLookUpTcsModuloGrupo];LookUpDisplayColumns[{\"DescGrupoModulo\" : \"Descrição\", \"IdGrupoModulo\" : \"Grupo de Módulos\"}];LookUpColumns[{\"DescGrupoModulo\" : true, \"IdGrupoModulo\" : true}];FilterDataKey[TCS_MODULO_DO_GRUPO.TCS_MODULO_GRUPO.DESC_GRUPO_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescGrupoModulo#false##60:0##Descrição#0#true##::LookUpTcsModuloGrupo##false#false#TCS_MODULO_GRUPO#TCS_MODULO_GRUPO#Linx.Framework.BV.Modulo#IQueryable###true#false", EdmKey="TCS_MODULO_DO_GRUPO.TCS_MODULO_GRUPO.DESC_GRUPO_MODULO")]
	    public System.String DescGrupoModulo
	    {
	    	    get
	    	    {
	    	          return _DescGrupoModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescGrupoModulo != value)
	    	          {
	    	              this.ValidateProperty("DescGrupoModulo", value);
	    	              this.OnDescGrupoModuloChanging(value);
	    	              this.RaiseDataMemberChanging("DescGrupoModulo");
	    	              this._DescGrupoModulo = value;
	    	              this.RaiseDataMemberChanged("DescGrupoModulo");
	    	              this.OnDescGrupoModuloChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdGrupoModulo
	    partial void OnIdGrupoModuloChanging(Int64 value);
	    partial void OnIdGrupoModuloChanged();

	    private Int64 _IdGrupoModulo;

	    [DataMember(IsRequired = true, Name = "IdGrupoModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Grupo de Módulos", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsModuloGrupo];LookUpTitle[Seleção de (Grupo de Módulos)];LookUpQuery[executeLookUpTcsModuloGrupo];LookUpFinalize[finalizeLookUpTcsModuloGrupo];LookUpDisplayColumns[{\"DescGrupoModulo\" : \"Descrição\", \"IdGrupoModulo\" : \"Grupo de Módulos\"}];LookUpColumns[{\"DescGrupoModulo\" : true, \"IdGrupoModulo\" : true}];FilterDataKey[TCS_MODULO_DO_GRUPO.TCS_MODULO_GRUPO.ID_GRUPO_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdGrupoModulo#true##24:0##Grupo de Módulos#1#true##::LookUpTcsModuloGrupo##false#false#TCS_MODULO_GRUPO#TCS_MODULO_GRUPO#Linx.Framework.BV.Modulo#IQueryable###true#false", EdmKey="TCS_MODULO_DO_GRUPO.TCS_MODULO_GRUPO.ID_GRUPO_MODULO")]
	    public Int64 IdGrupoModulo
	    {
	    	    get
	    	    {
	    	          return _IdGrupoModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdGrupoModulo != value)
	    	          {
	    	              this.ValidateProperty("IdGrupoModulo", value);
	    	              this.OnIdGrupoModuloChanging(value);
	    	              this.RaiseDataMemberChanging("IdGrupoModulo");
	    	              this._IdGrupoModulo = value;
	    	              this.RaiseDataMemberChanged("IdGrupoModulo");
	    	              this.OnIdGrupoModuloChanged();
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
	    [Display(Name = "Id Modulo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO_DO_GRUPO.ID_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_DO_GRUPO.ID_MODULO")]
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

	    private Int64 _TemporaryIdModulo;
	    [DataMember(Name = "TemporaryIdModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo (Tmp)", Description="Temporary Key", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
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

		

	    #region Parent Association
	 
	    private TcsModulo _TcsModulo;
	    [DataMember(Name = "TcsModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsModulo_TcsModuloDoGrupo", "IdModulo", "IdModulo", IsForeignKey=true)]
	    public TcsModulo TcsModulo
	    {
	        get
	        {
	            return this._TcsModulo;
	        }
	        set
	        {
	            if (this._TcsModulo != value)
	            {
	                this._TcsModulo = value;
	                this.RaisePropertyChanged("TcsModuloList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_MODULO_DO_GRUPO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_MODULO_DO_GRUPO), QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO_DO_GRUPO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_DO_GRUPO.ID_MODULO", Source = "IdModulo", Target = "ID_MODULO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO_DO_GRUPO", RelationPropertyName = "TCS_MODULO_DO_GRUPO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_DO_GRUPO.TCS_MODULO_GRUPO.ID_GRUPO_MODULO", Source = "IdGrupoModulo", Target = "ID_GRUPO_MODULO", TargetKeyName = "ID_GRUPO_MODULO", NoUpdatable = false, IsKey = true, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO_GRUPO", RelationPropertyName = "TCS_MODULO_GRUPO" });

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

		

	[LinxPublicationView(PrimaryKeys="TCS_MODULO_GRUPO.ID_GRUPO_MODULO", IsUpdatable=true, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Grupo de Módulos];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsModuloGrupo,TcsModuloGrupo.TcsModuloDoGrupoDetalhe];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdGrupoModulo];ReadOnly[false];Entities[TCS_MODULO_GRUPO:IdGrupoModulo];SubQueryInfo[];EdmEntityName[TCS_MODULO_GRUPO];EntityRelations[];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsModuloGrupo")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Modulo.TcsModuloGrupo")]
	public partial class TcsModuloGrupo : Linx.Data.Entity
	{

	
		
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.TcsModuloDoGrupoDetalheList != null && this.TcsModuloDoGrupoDetalheList.Count() > 0)
	      {
	         foreach (var entity in this.TcsModuloDoGrupoDetalheList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.TcsModuloDoGrupoDetalheList != null)
	      {
	         foreach (var detail in this.TcsModuloDoGrupoDetalheList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsModuloDoGrupoDetalheList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(ModuloDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("TcsModuloDoGrupoDetalhe"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsModuloDoGrupoDetalhe");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdGrupoModulo"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdGrupoModulo));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsModuloDoGrupoDetalhe and all sub-details
	         if (this.TcsModuloDoGrupoDetalheList == null || this.TcsModuloDoGrupoDetalheList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsModuloDoGrupoDetalheList = context.GetPagedTcsModuloDoGrupoDetalhe(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsModuloDoGrupoDetalheList = (from r in context.GetTcsModuloDoGrupoDetalheByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _TcsModuloDoGrupoDetalheElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsModuloDoGrupoDetalhe && ((TcsModuloDoGrupoDetalhe)e.Entity).TcsModuloGrupo == null && e.Associations == null && e.OriginalAssociations == null && ((TcsModuloDoGrupoDetalhe)e.Entity).IdGrupoModulo == this.IdGrupoModulo).ToList();
 	      if (_TcsModuloDoGrupoDetalheElements.Count > 0 && this.TcsModuloDoGrupoDetalheList.Count() == 0)
 	      {
 	          this.TcsModuloDoGrupoDetalheList = _TcsModuloDoGrupoDetalheElements.Select(e => (TcsModuloDoGrupoDetalhe)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsModuloDoGrupoDetalheElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsModuloDoGrupoDetalhe)detail.Entity).TcsModuloGrupo = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsModuloGrupo", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsModuloDoGrupoDetalheList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For DescGrupoModulo
	    partial void OnDescGrupoModuloChanging(System.String value);
	    partial void OnDescGrupoModuloChanged();

	    private System.String _DescGrupoModulo;

	    [DataMember(IsRequired = true, Name = "DescGrupoModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO_GRUPO.DESC_GRUPO_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_GRUPO.DESC_GRUPO_MODULO")]
	    public System.String DescGrupoModulo
	    {
	    	    get
	    	    {
	    	          return _DescGrupoModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescGrupoModulo != value)
	    	          {
	    	              this.ValidateProperty("DescGrupoModulo", value);
	    	              this.OnDescGrupoModuloChanging(value);
	    	              this.RaiseDataMemberChanging("DescGrupoModulo");
	    	              this._DescGrupoModulo = value;
	    	              this.RaiseDataMemberChanged("DescGrupoModulo");
	    	              this.OnDescGrupoModuloChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdGrupoModulo
	    partial void OnIdGrupoModuloChanging(Int64 value);
	    partial void OnIdGrupoModuloChanged();

	    private Int64 _IdGrupoModulo;

	    [DataMember(IsRequired = true, Name = "IdGrupoModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO_GRUPO.ID_GRUPO_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_GRUPO.ID_GRUPO_MODULO")]
	    public Int64 IdGrupoModulo
	    {
	    	    get
	    	    {
	    	          return _IdGrupoModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdGrupoModulo != value)
	    	          {
	    	              this.ValidateProperty("IdGrupoModulo", value);
	    	              this.OnIdGrupoModuloChanging(value);
	    	              this.RaiseDataMemberChanging("IdGrupoModulo");
	    	              this._IdGrupoModulo = value;
	    	              this.RaiseDataMemberChanged("IdGrupoModulo");
	    	              this.OnIdGrupoModuloChanged();
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
	    [Display(Name = "ID Aplicativo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicativo];LookUpTitle[Seleção de (ID Aplicativo)];LookUpQuery[executeLookUpTcsAplicativo];LookUpFinalize[finalizeLookUpTcsAplicativo];LookUpDisplayColumns[{\"IdTcsAplicativo\" : \"\", \"DescricaoAplicativo\" : \"Aplicativo\"}];LookUpColumns[{\"IdTcsAplicativo\" : false, \"DescricaoAplicativo\" : true}];FilterDataKey[TCS_MODULO_GRUPO.ID_TCS_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="int#IdTcsAplicativo#false##12:0###0#false##::LookUpTcsAplicativo##false#false###Linx.Framework.BV.Modulo#IQueryable###true#false", EdmKey="TCS_MODULO_GRUPO.ID_TCS_APLICATIVO")]
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
	    //Extensibility Partial Method Definitions For DescricaoAplicativo
	    partial void OnDescricaoAplicativoChanging(string value);
	    partial void OnDescricaoAplicativoChanged();

	    private string _DescricaoAplicativo;

	    [DataMember(IsRequired = true, Name = "DescricaoAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicativo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#DescricaoAplicativo#false##250:0##Aplicativo#1#true##::LookUpTcsAplicativo##false#false###Linx.Framework.BV.Modulo#IQueryable###true#false", EdmKey="")]
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

	    private Int64 _TemporaryIdGrupoModulo;
	    [DataMember(Name = "TemporaryIdGrupoModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id (Tmp)", Description="Temporary Key", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdGrupoModulo
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdGrupoModulo.IsNullOrEmpty())
	    	                this._TemporaryIdGrupoModulo = this._IdGrupoModulo;
	    	          return this._TemporaryIdGrupoModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdGrupoModulo != value)
	    	              this._TemporaryIdGrupoModulo = value;
	    	    }
	    }	

	    #endregion Data Properties

	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<TcsModuloDoGrupoDetalhe> _TcsModuloDoGrupoDetalheList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsModuloGrupo_TcsModuloDoGrupoDetalhe", "IdGrupoModulo", "IdGrupoModulo", IsForeignKey=false)]
	    [DataMember(Name = "TcsModuloDoGrupoDetalheList", EmitDefaultValue = true)]
	    public IEnumerable<TcsModuloDoGrupoDetalhe> TcsModuloDoGrupoDetalheList
	    {
	        get
	        {
	
	            if (this._TcsModuloDoGrupoDetalheList == null)
	            	this._TcsModuloDoGrupoDetalheList = new List<TcsModuloDoGrupoDetalhe>();
	
	            return this._TcsModuloDoGrupoDetalheList;
	        }
	        set
	        {
	            if (this._TcsModuloDoGrupoDetalheList != value)
	            {
	                this._TcsModuloDoGrupoDetalheList = value;
	                this.RaisePropertyChanged("TcsModuloDoGrupoDetalheList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_MODULO_GRUPO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_MODULO_GRUPO), QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO_GRUPO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_GRUPO.ID_GRUPO_MODULO", Source = "IdGrupoModulo", Target = "ID_GRUPO_MODULO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO_GRUPO", RelationPropertyName = "TCS_MODULO_GRUPO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_GRUPO.DESC_GRUPO_MODULO", Source = "DescGrupoModulo", Target = "DESC_GRUPO_MODULO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO_GRUPO", RelationPropertyName = "TCS_MODULO_GRUPO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_GRUPO.ID_TCS_APLICATIVO", Source = "IdTcsAplicativo", Target = "ID_TCS_APLICATIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO_GRUPO", RelationPropertyName = "TCS_MODULO_GRUPO" });

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

		

	[LinxPublicationView(PrimaryKeys="TCS_MODULO_DO_GRUPO.ID_MODULO_DO_GRUPO", IsUpdatable=true, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Módulo];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdModuloDoGrupo];ReadOnly[false];Entities[TCS_MODULO_DO_GRUPO:IdModuloDoGrupo];SubQueryInfo[Select 1 From #ParentAlias#.TCS_MODULO_DO_GRUPO_LISTA as #Alias#];EdmEntityName[TCS_MODULO_DO_GRUPO];EntityRelations[TCS_MODULO_GRUPO(TCS_MODULO_GRUPO)];EdmParentEntityName[TCS_MODULO_GRUPO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsModuloDoGrupoDetalhe")]
	[Serializable()]
	public partial class TcsModuloDoGrupoDetalhe : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(ModuloDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsModuloGrupo");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdGrupoModulo"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdGrupoModulo));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsModuloGrupo
	         this.TcsModuloGrupo = (from r in context.GetTcsModuloGrupoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For IdGrupoModulo
	    partial void OnIdGrupoModuloChanging(Int64 value);
	    partial void OnIdGrupoModuloChanged();

	    private Int64 _IdGrupoModulo;

	    [DataMember(IsRequired = true, Name = "IdGrupoModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO_DO_GRUPO.TCS_MODULO_GRUPO.ID_GRUPO_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_DO_GRUPO.TCS_MODULO_GRUPO.ID_GRUPO_MODULO")]
	    public Int64 IdGrupoModulo
	    {
	    	    get
	    	    {
	    	          return _IdGrupoModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdGrupoModulo != value)
	    	          {
	    	              this.ValidateProperty("IdGrupoModulo", value);
	    	              this.OnIdGrupoModuloChanging(value);
	    	              this.RaiseDataMemberChanging("IdGrupoModulo");
	    	              this._IdGrupoModulo = value;
	    	              this.RaiseDataMemberChanged("IdGrupoModulo");
	    	              this.OnIdGrupoModuloChanged();
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
	    [Display(Name = "Id Modulo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsModuloDoGrupoDetalhe];LookUpTitle[Seleção de (Id Modulo)];LookUpQuery[executeLookUpTcsModuloDoGrupoDetalhe];LookUpFinalize[finalizeLookUpTcsModuloDoGrupoDetalhe];LookUpDisplayColumns[{\"IdModulo\" : \"\", \"DescModulo\" : \"Módulo\"}];LookUpColumns[{\"IdModulo\" : false, \"DescModulo\" : true}];FilterDataKey[TCS_MODULO_DO_GRUPO.ID_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdModulo#true##12###0#false##::LookUpTcsModuloDoGrupoDetalhe##true#false###Linx.Framework.BV.Modulo#IQueryable###true#false", EdmKey="TCS_MODULO_DO_GRUPO.ID_MODULO")]
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
	    //Extensibility Partial Method Definitions For IdModuloDoGrupo
	    partial void OnIdModuloDoGrupoChanging(Int64 value);
	    partial void OnIdModuloDoGrupoChanged();

	    private Int64 _IdModuloDoGrupo;

	    [DataMember(IsRequired = true, Name = "IdModuloDoGrupo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo Do Grupo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO_DO_GRUPO.ID_MODULO_DO_GRUPO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_DO_GRUPO.ID_MODULO_DO_GRUPO")]
	    public Int64 IdModuloDoGrupo
	    {
	    	    get
	    	    {
	    	          return _IdModuloDoGrupo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdModuloDoGrupo != value)
	    	          {
	    	              this.ValidateProperty("IdModuloDoGrupo", value);
	    	              this.OnIdModuloDoGrupoChanging(value);
	    	              this.RaiseDataMemberChanging("IdModuloDoGrupo");
	    	              this._IdModuloDoGrupo = value;
	    	              this.RaiseDataMemberChanged("IdModuloDoGrupo");
	    	              this.OnIdModuloDoGrupoChanged();
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
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#DescModulo#false##60:0##Módulo#1#true##::LookUpTcsModuloDoGrupoDetalhe##true#false###Linx.Framework.BV.Modulo#IQueryable###true#false", EdmKey="")]
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

	    private Int64 _TemporaryIdModuloDoGrupo;
	    [DataMember(Name = "TemporaryIdModuloDoGrupo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo Do Grupo (Tmp)", Description="Temporary Key", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdModuloDoGrupo
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdModuloDoGrupo.IsNullOrEmpty())
	    	                this._TemporaryIdModuloDoGrupo = this._IdModuloDoGrupo;
	    	          return this._TemporaryIdModuloDoGrupo;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdModuloDoGrupo != value)
	    	              this._TemporaryIdModuloDoGrupo = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsModuloGrupo _TcsModuloGrupo;
	    [DataMember(Name = "TcsModuloGrupo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsModuloGrupo_TcsModuloDoGrupoDetalhe", "IdGrupoModulo", "IdGrupoModulo", IsForeignKey=true)]
	    public TcsModuloGrupo TcsModuloGrupo
	    {
	        get
	        {
	            return this._TcsModuloGrupo;
	        }
	        set
	        {
	            if (this._TcsModuloGrupo != value)
	            {
	                this._TcsModuloGrupo = value;
	                this.RaisePropertyChanged("TcsModuloGrupoList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_MODULO_DO_GRUPO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_MODULO_DO_GRUPO), QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO_DO_GRUPO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_DO_GRUPO.ID_MODULO", Source = "IdModulo", Target = "ID_MODULO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO_DO_GRUPO", RelationPropertyName = "TCS_MODULO_DO_GRUPO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_DO_GRUPO.ID_MODULO_DO_GRUPO", Source = "IdModuloDoGrupo", Target = "ID_MODULO_DO_GRUPO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO_DO_GRUPO", RelationPropertyName = "TCS_MODULO_DO_GRUPO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_DO_GRUPO.TCS_MODULO_GRUPO.ID_GRUPO_MODULO", Source = "IdGrupoModulo", Target = "ID_GRUPO_MODULO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO_GRUPO", RelationPropertyName = "TCS_MODULO_GRUPO" });

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

		

	[LinxPublicationView(PrimaryKeys="AppModule.EntityUniqueKey", IsUpdatable=false, EdmName="")]
		
	[DataContract(IsReference = false, Name = "AppModule")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Modulo.AppModule")]
	public partial class AppModule 
	{

	

	    public AppModule() : this(true) { }

	    public AppModule(bool setDefaults) 
	    {

	        if (setDefaults)
	        {
	        	        IsFavorite = false;
	        }	

	    }

			
	

	
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
	 


	    private Int64 _Id;

	    [DataMember(Name = "Id", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public Int64 Id
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

	    private string _DisplayName;

	    [DataMember(Name = "DisplayName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string DisplayName
	    {
	    	    get
	    	    {
	    	          return _DisplayName;
	    	    }
	    	    set
	    	    {
	    	          this._DisplayName = value;
	    	    }
	    }

	    private string _Image;

	    [DataMember(Name = "Image", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Image
	    {
	    	    get
	    	    {
	    	          return _Image;
	    	    }
	    	    set
	    	    {
	    	          this._Image = value;
	    	    }
	    }

	    private string _Description;

	    [DataMember(Name = "Description", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Description
	    {
	    	    get
	    	    {
	    	          return _Description;
	    	    }
	    	    set
	    	    {
	    	          this._Description = value;
	    	    }
	    }

	    private List<AppMenu> _Menus;

	    [DataMember(Name = "Menus", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public List<AppMenu> Menus
	    {
	    	    get
	    	    {
	    	          return _Menus;
	    	    }
	    	    set
	    	    {
	    	          this._Menus = value;
	    	    }
	    }

	    private string _UrlRoute;

	    [DataMember(Name = "UrlRoute", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string UrlRoute
	    {
	    	    get
	    	    {
	    	          return _UrlRoute;
	    	    }
	    	    set
	    	    {
	    	          this._UrlRoute = value;
	    	    }
	    }

	    private string _ClassIcon;

	    [DataMember(Name = "ClassIcon", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ClassIcon
	    {
	    	    get
	    	    {
	    	          return _ClassIcon;
	    	    }
	    	    set
	    	    {
	    	          this._ClassIcon = value;
	    	    }
	    }

	    private string _ClassBackground;

	    [DataMember(Name = "ClassBackground", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ClassBackground
	    {
	    	    get
	    	    {
	    	          return _ClassBackground;
	    	    }
	    	    set
	    	    {
	    	          this._ClassBackground = value;
	    	    }
	    }

	    private string _ClassSize;

	    [DataMember(Name = "ClassSize", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ClassSize
	    {
	    	    get
	    	    {
	    	          return _ClassSize;
	    	    }
	    	    set
	    	    {
	    	          this._ClassSize = value;
	    	    }
	    }

	    private List<BreadCrumbItem> _BreadCrumb;

	    [DataMember(Name = "BreadCrumb", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public List<BreadCrumbItem> BreadCrumb
	    {
	    	    get
	    	    {
	    	          return _BreadCrumb;
	    	    }
	    	    set
	    	    {
	    	          this._BreadCrumb = value;
	    	    }
	    }

	    private bool _IsFavorite;

	    [DataMember(Name = "IsFavorite", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public bool IsFavorite
	    {
	    	    get
	    	    {
	    	          return _IsFavorite;
	    	    }
	    	    set
	    	    {
	    	          this._IsFavorite = value;
	    	    }
	    }

	    private string _FriendlyUrl;

	    [DataMember(Name = "FriendlyUrl", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string FriendlyUrl
	    {
	    	    get
	    	    {
	    	          return _FriendlyUrl;
	    	    }
	    	    set
	    	    {
	    	          this._FriendlyUrl = value;
	    	    }
	    }

	    private Linx.Framework.BV.Multimidia.DocMultimidiaInfo _Midia;

	    [DataMember(Name = "Midia", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public Linx.Framework.BV.Multimidia.DocMultimidiaInfo Midia
	    {
	    	    get
	    	    {
	    	          return _Midia;
	    	    }
	    	    set
	    	    {
	    	          this._Midia = value;
	    	    }
	    }

	    private int _IdTcsAplicativo;

	    [DataMember(Name = "IdTcsAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
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

	    private int _IdTcsAmbiente;

	    [DataMember(Name = "IdTcsAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
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

	    private string _ShortDisplayName;

	    [DataMember(Name = "ShortDisplayName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ShortDisplayName
	    {
	    	    get
	    	    {
	    	          return _ShortDisplayName;
	    	    }
	    	    set
	    	    {
	    	          this._ShortDisplayName = value;
	    	    }
	    }

	    private int _MenusCount;

	    [DataMember(Name = "MenusCount", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public int MenusCount
	    {
	    	    get
	    	    {
	    	          return _MenusCount;
	    	    }
	    	    set
	    	    {
	    	          this._MenusCount = value;
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

		

	[LinxPublicationView(PrimaryKeys="BreadCrumbItem.EntityUniqueKey", IsUpdatable=false, EdmName="")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[BreadCrumbItem];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[ModuleKey];ReadOnly[false];Entities[:ModuleKey];SubQueryInfo[];EdmEntityName[];EntityRelations[ ];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "BreadCrumbItem")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Modulo.BreadCrumbItem")]
	public partial class BreadCrumbItem : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For Order
	    partial void OnOrderChanging(int value);
	    partial void OnOrderChanged();

	    private int _Order;

	    [DataMember(IsRequired = true, Name = "Order", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public int Order
	    {
	    	    get
	    	    {
	    	          return _Order;
	    	    }
	    	    set
	    	    {
	    	          if (this._Order != value)
	    	          {
	    	              this.ValidateProperty("Order", value);
	    	              this.OnOrderChanging(value);
	    	              this.RaiseDataMemberChanging("Order");
	    	              this._Order = value;
	    	              this.RaiseDataMemberChanged("Order");
	    	              this.OnOrderChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DisplayName
	    partial void OnDisplayNameChanging(string value);
	    partial void OnDisplayNameChanged();

	    private string _DisplayName;

	    [DataMember(IsRequired = true, Name = "DisplayName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string DisplayName
	    {
	    	    get
	    	    {
	    	          return _DisplayName;
	    	    }
	    	    set
	    	    {
	    	          if (this._DisplayName != value)
	    	          {
	    	              this.ValidateProperty("DisplayName", value);
	    	              this.OnDisplayNameChanging(value);
	    	              this.RaiseDataMemberChanging("DisplayName");
	    	              this._DisplayName = value;
	    	              this.RaiseDataMemberChanged("DisplayName");
	    	              this.OnDisplayNameChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ModuleKey
	    partial void OnModuleKeyChanging(Guid value);
	    partial void OnModuleKeyChanged();

	    private Guid _ModuleKey;

	    [DataMember(IsRequired = true, Name = "ModuleKey", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public Guid ModuleKey
	    {
	    	    get
	    	    {
	    	          return _ModuleKey;
	    	    }
	    	    set
	    	    {
	    	          if (this._ModuleKey != value)
	    	          {
	    	              this.ValidateProperty("ModuleKey", value);
	    	              this.OnModuleKeyChanging(value);
	    	              this.RaiseDataMemberChanging("ModuleKey");
	    	              this._ModuleKey = value;
	    	              this.RaiseDataMemberChanged("ModuleKey");
	    	              this.OnModuleKeyChanged();
	    	          }
	    	    }
	    }

	    private Guid _TemporaryModuleKey;
	    [DataMember(Name = "TemporaryModuleKey", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = " (Tmp)", Description="Temporary Key", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Guid TemporaryModuleKey
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryModuleKey.IsNullOrEmpty())
	    	                this._TemporaryModuleKey = this._ModuleKey;
	    	          return this._TemporaryModuleKey;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryModuleKey != value)
	    	              this._TemporaryModuleKey = value;
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

		

	[LinxPublicationView(PrimaryKeys="AppMenu.EntityUniqueKey", IsUpdatable=false, EdmName="")]
		
	[DataContract(IsReference = false, Name = "AppMenu")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Modulo.AppMenu")]
	public partial class AppMenu 
	{

	

	    public AppMenu() : this(true) { }

	    public AppMenu(bool setDefaults) 
	    {

	        if (setDefaults)
	        {
	        	        IsFavorite = false;
	        }	

	    }

			
	

	
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
	 


	    private Int64 _Id;

	    [DataMember(Name = "Id", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public Int64 Id
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

	    private string _DisplayName;

	    [DataMember(Name = "DisplayName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string DisplayName
	    {
	    	    get
	    	    {
	    	          return _DisplayName;
	    	    }
	    	    set
	    	    {
	    	          this._DisplayName = value;
	    	    }
	    }

	    private string _Module;

	    [DataMember(Name = "Module", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Module
	    {
	    	    get
	    	    {
	    	          return _Module;
	    	    }
	    	    set
	    	    {
	    	          this._Module = value;
	    	    }
	    }

	    private bool _IsTransaction;

	    [DataMember(Name = "IsTransaction", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public bool IsTransaction
	    {
	    	    get
	    	    {
	    	          return _IsTransaction;
	    	    }
	    	    set
	    	    {
	    	          this._IsTransaction = value;
	    	    }
	    }

	    private string _UrlRoute;

	    [DataMember(Name = "UrlRoute", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string UrlRoute
	    {
	    	    get
	    	    {
	    	          return _UrlRoute;
	    	    }
	    	    set
	    	    {
	    	          this._UrlRoute = value;
	    	    }
	    }

	    private string _ClassIcon;

	    [DataMember(Name = "ClassIcon", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ClassIcon
	    {
	    	    get
	    	    {
	    	          return _ClassIcon;
	    	    }
	    	    set
	    	    {
	    	          this._ClassIcon = value;
	    	    }
	    }

	    private string _ClassBackground;

	    [DataMember(Name = "ClassBackground", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ClassBackground
	    {
	    	    get
	    	    {
	    	          return _ClassBackground;
	    	    }
	    	    set
	    	    {
	    	          this._ClassBackground = value;
	    	    }
	    }

	    private string _ClassSize;

	    [DataMember(Name = "ClassSize", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ClassSize
	    {
	    	    get
	    	    {
	    	          return _ClassSize;
	    	    }
	    	    set
	    	    {
	    	          this._ClassSize = value;
	    	    }
	    }

	    private List<BreadCrumbItem> _BreadCrumb;

	    [DataMember(Name = "BreadCrumb", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public List<BreadCrumbItem> BreadCrumb
	    {
	    	    get
	    	    {
	    	          return _BreadCrumb;
	    	    }
	    	    set
	    	    {
	    	          this._BreadCrumb = value;
	    	    }
	    }

	    private int _Order;

	    [DataMember(Name = "Order", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public int Order
	    {
	    	    get
	    	    {
	    	          return _Order;
	    	    }
	    	    set
	    	    {
	    	          this._Order = value;
	    	    }
	    }

	    private int _Type;

	    [DataMember(Name = "Type", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public int Type
	    {
	    	    get
	    	    {
	    	          return _Type;
	    	    }
	    	    set
	    	    {
	    	          this._Type = value;
	    	    }
	    }

	    private bool _IsFavorite;

	    [DataMember(Name = "IsFavorite", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public bool IsFavorite
	    {
	    	    get
	    	    {
	    	          return _IsFavorite;
	    	    }
	    	    set
	    	    {
	    	          this._IsFavorite = value;
	    	    }
	    }

	    private Int64 _IdModule;

	    [DataMember(Name = "IdModule", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public Int64 IdModule
	    {
	    	    get
	    	    {
	    	          return _IdModule;
	    	    }
	    	    set
	    	    {
	    	          this._IdModule = value;
	    	    }
	    }

	    private string _Image;

	    [DataMember(Name = "Image", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Image
	    {
	    	    get
	    	    {
	    	          return _Image;
	    	    }
	    	    set
	    	    {
	    	          this._Image = value;
	    	    }
	    }

	    private string _ModuleDescription;

	    [DataMember(Name = "ModuleDescription", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ModuleDescription
	    {
	    	    get
	    	    {
	    	          return _ModuleDescription;
	    	    }
	    	    set
	    	    {
	    	          this._ModuleDescription = value;
	    	    }
	    }

	    private string _TransactionCode;

	    [DataMember(Name = "TransactionCode", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string TransactionCode
	    {
	    	    get
	    	    {
	    	          return _TransactionCode;
	    	    }
	    	    set
	    	    {
	    	          this._TransactionCode = value;
	    	    }
	    }

	    private string _FriendlyUrl;

	    [DataMember(Name = "FriendlyUrl", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string FriendlyUrl
	    {
	    	    get
	    	    {
	    	          return _FriendlyUrl;
	    	    }
	    	    set
	    	    {
	    	          this._FriendlyUrl = value;
	    	    }
	    }

	    private Linx.Framework.BV.Multimidia.DocMultimidiaInfo _Midia;

	    [DataMember(Name = "Midia", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public Linx.Framework.BV.Multimidia.DocMultimidiaInfo Midia
	    {
	    	    get
	    	    {
	    	          return _Midia;
	    	    }
	    	    set
	    	    {
	    	          this._Midia = value;
	    	    }
	    }

	    private int _IdTcsAplicativo;

	    [DataMember(Name = "IdTcsAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
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

	    private string _ShortDisplayName;

	    [DataMember(Name = "ShortDisplayName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ShortDisplayName
	    {
	    	    get
	    	    {
	    	          return _ShortDisplayName;
	    	    }
	    	    set
	    	    {
	    	          this._ShortDisplayName = value;
	    	    }
	    }

	    private int _MenusCount;

	    [DataMember(Name = "MenusCount", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public int MenusCount
	    {
	    	    get
	    	    {
	    	          return _MenusCount;
	    	    }
	    	    set
	    	    {
	    	          this._MenusCount = value;
	    	    }
	    }

	    private string _Tags;

	    [DataMember(Name = "Tags", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Tags
	    {
	    	    get
	    	    {
	    	          return _Tags;
	    	    }
	    	    set
	    	    {
	    	          this._Tags = value;
	    	    }
	    }
 
 	    //Entity Collections
 
 	    private List<AppMenu> _Menus;
 	    [DataMember(Name = "Menus", EmitDefaultValue = true)]
 	    public List<AppMenu> Menus
 	    {
 	          get {
 	                  if (_Menus == null)
 	                      _Menus = new List<AppMenu>();
 	                  return _Menus;
 	              } 
 	          set { _Menus = value;}
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

		

	[LinxPublicationView(PrimaryKeys="UserModules.EntityUniqueKey", IsUpdatable=false, EdmName="")]
		
	[DataContract(IsReference = false, Name = "UserModules")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Modulo.UserModules")]
	public partial class UserModules 
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

	    private List<AppModule> _Modules;

	    [DataMember(Name = "Modules", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public List<AppModule> Modules
	    {
	    	    get
	    	    {
	    	          return _Modules;
	    	    }
	    	    set
	    	    {
	    	          this._Modules = value;
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

		

	[LinxPublicationView(PrimaryKeys="TCS_USUARIO_FAVORITO.ID_TCS_USUARIO_FAVORITO", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsUsuarioFavorito];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsUsuarioFavorito];ReadOnly[false];Entities[TCS_USUARIO_FAVORITO:IdTcsUsuarioFavorito];SubQueryInfo[];EdmEntityName[TCS_USUARIO_FAVORITO];EntityRelations[TCS_USUARIO(TCS_USUARIO)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioFavorito")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Modulo.TcsUsuarioFavorito")]
	public partial class TcsUsuarioFavorito : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For IdModulo
	    partial void OnIdModuloChanging(Int64 value);
	    partial void OnIdModuloChanged();

	    private Int64 _IdModulo;

	    [DataMember(IsRequired = true, Name = "IdModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_FAVORITO.ID_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_FAVORITO.ID_MODULO")]
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
	    partial void OnIdModuloMenuChanging(System.Nullable<System.Int64> value);
	    partial void OnIdModuloMenuChanged();

	    private System.Nullable<System.Int64> _IdModuloMenu;

	    [DataMember(Name = "IdModuloMenu", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo Menu", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_FAVORITO.ID_MODULO_MENU];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_FAVORITO.ID_MODULO_MENU")]
	    public System.Nullable<System.Int64> IdModuloMenu
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
	    //Extensibility Partial Method Definitions For IdTcsUsuarioFavorito
	    partial void OnIdTcsUsuarioFavoritoChanging(Int64 value);
	    partial void OnIdTcsUsuarioFavoritoChanged();

	    private Int64 _IdTcsUsuarioFavorito;

	    [DataMember(IsRequired = true, Name = "IdTcsUsuarioFavorito", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Favorito", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_FAVORITO.ID_TCS_USUARIO_FAVORITO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_FAVORITO.ID_TCS_USUARIO_FAVORITO")]
	    public Int64 IdTcsUsuarioFavorito
	    {
	    	    get
	    	    {
	    	          return _IdTcsUsuarioFavorito;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsUsuarioFavorito != value)
	    	          {
	    	              this.ValidateProperty("IdTcsUsuarioFavorito", value);
	    	              this.OnIdTcsUsuarioFavoritoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsUsuarioFavorito");
	    	              this._IdTcsUsuarioFavorito = value;
	    	              this.RaiseDataMemberChanged("IdTcsUsuarioFavorito");
	    	              this.OnIdTcsUsuarioFavoritoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTransacao
	    partial void OnIdTransacaoChanging(System.Nullable<System.Int64> value);
	    partial void OnIdTransacaoChanged();

	    private System.Nullable<System.Int64> _IdTransacao;

	    [DataMember(Name = "IdTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Transacao", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_FAVORITO.ID_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_FAVORITO.ID_TRANSACAO")]
	    public System.Nullable<System.Int64> IdTransacao
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
	    //Extensibility Partial Method Definitions For IdUsuario
	    partial void OnIdUsuarioChanging(Int64 value);
	    partial void OnIdUsuarioChanged();

	    private Int64 _IdUsuario;

	    [DataMember(IsRequired = true, Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_FAVORITO.TCS_USUARIO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_FAVORITO.TCS_USUARIO.ID_USUARIO")]
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
	    //Extensibility Partial Method Definitions For OrdemNavegacao
	    partial void OnOrdemNavegacaoChanging(Byte value);
	    partial void OnOrdemNavegacaoChanged();

	    private Byte _OrdemNavegacao;

	    [DataMember(IsRequired = true, Name = "OrdemNavegacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ordem Navegacao", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_FAVORITO.ORDEM_NAVEGACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_FAVORITO.ORDEM_NAVEGACAO")]
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

	    private Int64 _TemporaryIdTcsUsuarioFavorito;
	    [DataMember(Name = "TemporaryIdTcsUsuarioFavorito", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Favorito (Tmp)", Description="Temporary Key", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdTcsUsuarioFavorito
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTcsUsuarioFavorito.IsNullOrEmpty())
	    	                this._TemporaryIdTcsUsuarioFavorito = this._IdTcsUsuarioFavorito;
	    	          return this._TemporaryIdTcsUsuarioFavorito;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTcsUsuarioFavorito != value)
	    	              this._TemporaryIdTcsUsuarioFavorito = value;
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_USUARIO_FAVORITO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = true, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_USUARIO_FAVORITO), QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_FAVORITO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_FAVORITO.ID_MODULO", Source = "IdModulo", Target = "ID_MODULO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_FAVORITO", RelationPropertyName = "TCS_USUARIO_FAVORITO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_FAVORITO.ID_TRANSACAO", Source = "IdTransacao", Target = "ID_TRANSACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_FAVORITO", RelationPropertyName = "TCS_USUARIO_FAVORITO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_FAVORITO.ID_MODULO_MENU", Source = "IdModuloMenu", Target = "ID_MODULO_MENU", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_FAVORITO", RelationPropertyName = "TCS_USUARIO_FAVORITO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_FAVORITO.ORDEM_NAVEGACAO", Source = "OrdemNavegacao", Target = "ORDEM_NAVEGACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_FAVORITO", RelationPropertyName = "TCS_USUARIO_FAVORITO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_FAVORITO.TCS_USUARIO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_FAVORITO.ID_TCS_USUARIO_FAVORITO", Source = "IdTcsUsuarioFavorito", Target = "ID_TCS_USUARIO_FAVORITO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_FAVORITO", RelationPropertyName = "TCS_USUARIO_FAVORITO" });

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

		

	[LinxPublicationView(PrimaryKeys="EnvironmentInfo.EntityUniqueKey", IsUpdatable=false, EdmName="")]
		
	[DataContract(IsReference = false, Name = "EnvironmentInfo")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Modulo.EnvironmentInfo")]
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

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Módulo];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdModuloDoGrupo];ReadOnly[false];Entities[TCS_MODULO_DO_GRUPO:IdModuloDoGrupo];SubQueryInfo[Select 1 From #ParentAlias#.TCS_MODULO_DO_GRUPO_LISTA as #Alias#];EdmEntityName[TCS_MODULO_DO_GRUPO];EntityRelations[TCS_MODULO_GRUPO(TCS_MODULO_GRUPO)];EdmParentEntityName[TCS_MODULO_GRUPO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsModuloDoGrupoDetalhe")]
	[Serializable()]
	public partial class TcsModuloDoGrupoDetalheParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For IdGrupoModulo
	    partial void OnIdGrupoModuloChanging(Int64 value);
	    partial void OnIdGrupoModuloChanged();

	    private Int64 _IdGrupoModulo;

	    [DataMember(IsRequired = true, Name = "IdGrupoModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO_DO_GRUPO.TCS_MODULO_GRUPO.ID_GRUPO_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_DO_GRUPO.TCS_MODULO_GRUPO.ID_GRUPO_MODULO")]
	    public Int64 IdGrupoModulo
	    {
	    	    get
	    	    {
	    	          return _IdGrupoModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdGrupoModulo != value)
	    	          {
	    	              this.ValidateProperty("IdGrupoModulo", value);
	    	              this.OnIdGrupoModuloChanging(value);
	    	              this.RaiseDataMemberChanging("IdGrupoModulo");
	    	              this._IdGrupoModulo = value;
	    	              this.RaiseDataMemberChanged("IdGrupoModulo");
	    	              this.OnIdGrupoModuloChanged();
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
	    [Display(Name = "Id Modulo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsModuloDoGrupoDetalhe];LookUpTitle[Seleção de (Id Modulo)];LookUpQuery[executeLookUpTcsModuloDoGrupoDetalhe];LookUpFinalize[finalizeLookUpTcsModuloDoGrupoDetalhe];LookUpDisplayColumns[{\"IdModulo\" : \"\", \"DescModulo\" : \"Módulo\"}];LookUpColumns[{\"IdModulo\" : false, \"DescModulo\" : true}];FilterDataKey[TCS_MODULO_DO_GRUPO.ID_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdModulo#true##12###0#false##::LookUpTcsModuloDoGrupoDetalhe##true#false###Linx.Framework.BV.Modulo#IQueryable###true#false", EdmKey="TCS_MODULO_DO_GRUPO.ID_MODULO")]
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
	    //Extensibility Partial Method Definitions For IdModuloDoGrupo
	    partial void OnIdModuloDoGrupoChanging(Int64 value);
	    partial void OnIdModuloDoGrupoChanged();

	    private Int64 _IdModuloDoGrupo;

	    [DataMember(IsRequired = true, Name = "IdModuloDoGrupo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo Do Grupo", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO_DO_GRUPO.ID_MODULO_DO_GRUPO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_DO_GRUPO.ID_MODULO_DO_GRUPO")]
	    public Int64 IdModuloDoGrupo
	    {
	    	    get
	    	    {
	    	          return _IdModuloDoGrupo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdModuloDoGrupo != value)
	    	          {
	    	              this.ValidateProperty("IdModuloDoGrupo", value);
	    	              this.OnIdModuloDoGrupoChanging(value);
	    	              this.RaiseDataMemberChanging("IdModuloDoGrupo");
	    	              this._IdModuloDoGrupo = value;
	    	              this.RaiseDataMemberChanged("IdModuloDoGrupo");
	    	              this.OnIdModuloDoGrupoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescGrupoModulo
	    partial void OnDescGrupoModuloChanging(System.String value);
	    partial void OnDescGrupoModuloChanged();

	    private System.String _DescGrupoModulo;

	    [DataMember(IsRequired = true, Name = "DescGrupoModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_MODULO_DO_GRUPO.TCS_MODULO_GRUPO.DESC_GRUPO_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_GRUPO.DESC_GRUPO_MODULO")]
	    public System.String DescGrupoModulo
	    {
	    	    get
	    	    {
	    	          return _DescGrupoModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescGrupoModulo != value)
	    	          {
	    	              this.ValidateProperty("DescGrupoModulo", value);
	    	              this.OnDescGrupoModuloChanging(value);
	    	              this.RaiseDataMemberChanging("DescGrupoModulo");
	    	              this._DescGrupoModulo = value;
	    	              this.RaiseDataMemberChanged("DescGrupoModulo");
	    	              this.OnDescGrupoModuloChanged();
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
	    [Display(Name = "ID Aplicativo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_MODULO_DO_GRUPO.TCS_MODULO_GRUPO.ID_TCS_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_GRUPO.ID_TCS_APLICATIVO")]
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

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_MODULO_DO_GRUPO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_MODULO_DO_GRUPO), QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO_DO_GRUPO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_DO_GRUPO.ID_MODULO", Source = "IdModulo", Target = "ID_MODULO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO_DO_GRUPO", RelationPropertyName = "TCS_MODULO_DO_GRUPO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_DO_GRUPO.ID_MODULO_DO_GRUPO", Source = "IdModuloDoGrupo", Target = "ID_MODULO_DO_GRUPO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO_DO_GRUPO", RelationPropertyName = "TCS_MODULO_DO_GRUPO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_DO_GRUPO.TCS_MODULO_GRUPO.ID_GRUPO_MODULO", Source = "IdGrupoModulo", Target = "ID_GRUPO_MODULO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO_GRUPO", RelationPropertyName = "TCS_MODULO_GRUPO" });

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
	[DomainIdentifier("ProcessorOverviewModuloDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class ModuloDomainService : DomainService, IDataServiceContext 
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

		
	    public ModuloDomainService() : this("", null, null) { }
	    public ModuloDomainService(string connectionString) : this(connectionString, null, null) { }
	    public ModuloDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public ModuloDomainService(Linx.Framework.ControleSistema.BM.ControleSistemaContext dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public ModuloDomainService(string connectionString, Linx.Framework.ControleSistema.BM.ControleSistemaContext dataContext, Dictionary<string, string> headers) : base() 
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
	    			if (entry.Entity is TcsModulo) ((TcsModulo)entry.Entity).SaveMedia(entry.Operation);
	    			if (entry.Entity is TcsModuloMenu) ((TcsModuloMenu)entry.Entity).SaveMedia(entry.Operation);
	    			if (entry.Entity is TcsTransacaoMenu) ((TcsTransacaoMenu)entry.Entity).SaveMedia(entry.Operation);
	    		}
	    }

	    private void OnSavedChanges(ChangeSet changeSet)
	    {
	
	
	        TcsModulo.OnSavedContextChanges(this, changeSet.ChangeSetEntries.Where(e => e.Entity is TcsModulo).ToArray());
    
	        TcsModuloGrupo.OnSavedContextChanges(this, changeSet.ChangeSetEntries.Where(e => e.Entity is TcsModuloGrupo).ToArray());
    	
	    }
		
	    private void OnTransactingChanges(ChangeSet changeSet)
	    {
	
	    
	        foreach (ChangeSetEntry entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsModulo))
	        {
	            ((TcsModulo)entry.Entity).OnTransactingChanges(this, changeSet.GetChangeOperation(entry.Entity));
	        }
    	
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
 	        var _TcsModuloElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsModulo && e.Entity.GetType().Name == "TcsModulo" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _TcsModuloElements)
 	           if (((TcsModulo)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 	        var _TcsModuloGrupoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsModuloGrupo && e.Entity.GetType().Name == "TcsModuloGrupo" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _TcsModuloGrupoElements)
 	           if (((TcsModuloGrupo)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 
 	        //Remove inconsistent details
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsModuloMenu && e.Entity.GetType().Name == "TcsModuloMenu" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsModuloDoGrupo && e.Entity.GetType().Name == "TcsModuloDoGrupo" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsTransacaoMenu && e.Entity.GetType().Name == "TcsTransacaoMenu" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsModuloDoGrupoDetalhe && e.Entity.GetType().Name == "TcsModuloDoGrupoDetalhe" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
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
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TCS_MODULO_MENU" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
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
	
	            (from entity in this.DbContext.TCS_MODULO_MENU.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpModuloMenuSuperior()		
	            {
	            
                DescModuloMenu = entity.MODULO_MENU_SUPERIOR.DESC_MODULO_MENU
                , DescModuloMenuSuperior = entity.DESC_MODULO_MENU
                , IdModulo = entity.ID_MODULO
                , IdModuloMenuSuperior = entity.ID_MODULO_MENU
	            });

	            
            //Inner Group Definition
            if (propertyName.InList("DescModuloMenu"))
            {
               query = (from r in query select new LookUpModuloMenuSuperior() {
               DescModuloMenu = r.DescModuloMenu
               , DescModuloMenuSuperior = ""
               , IdModulo = default(Int64)
               , IdModuloMenuSuperior = default(System.Nullable<Int64>)
                }).Distinct();
            }
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpTcsTransacaoMenu.
	    public IQueryable<LookUpTcsTransacaoMenu> GetAllLookUpTcsTransacaoMenu()
	    {
	        return this.GetLookUpTcsTransacaoMenu(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsTransacaoMenu By EntitySearch.
	    public IQueryable<LookUpTcsTransacaoMenu> GetLookUpTcsTransacaoMenuByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsTransacaoMenu(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsTransacaoMenu.
	    public IQueryable<LookUpTcsTransacaoMenu> GetLookUpTcsTransacaoMenu(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsTransacaoMenu";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsTransacaoMenu));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsTransacaoMenu> query =  null;
		
			
		
	        TcsTransacaoMenu.OnLookUpingLookUpTcsTransacaoMenu(ref query, propertyName, entitySearch);
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpTcsModuloGrupo.
	    public IQueryable<LookUpTcsModuloGrupo> GetAllLookUpTcsModuloGrupo()
	    {
	        return this.GetLookUpTcsModuloGrupo(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsModuloGrupo By EntitySearch.
	    public IQueryable<LookUpTcsModuloGrupo> GetLookUpTcsModuloGrupoByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsModuloGrupo(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsModuloGrupo.
	    public IQueryable<LookUpTcsModuloGrupo> GetLookUpTcsModuloGrupo(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TCS_MODULO_GRUPO" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsModuloGrupo";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsModuloGrupo));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsModuloGrupo> query =  
	
	            (from entity in this.DbContext.TCS_MODULO_GRUPO.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpTcsModuloGrupo()		
	            {
	            
                DescGrupoModulo = entity.DESC_GRUPO_MODULO
                , IdGrupoModulo = entity.ID_GRUPO_MODULO
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
		
			
		
	        TcsModuloGrupo.OnLookingUpLookUpTcsAplicativo(ref query, propertyName, entitySearch);
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpTcsModuloDoGrupoDetalhe.
	    public IQueryable<LookUpTcsModuloDoGrupoDetalhe> GetAllLookUpTcsModuloDoGrupoDetalhe()
	    {
	        return this.GetLookUpTcsModuloDoGrupoDetalhe(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsModuloDoGrupoDetalhe By EntitySearch.
	    public IQueryable<LookUpTcsModuloDoGrupoDetalhe> GetLookUpTcsModuloDoGrupoDetalheByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsModuloDoGrupoDetalhe(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsModuloDoGrupoDetalhe.
	    public IQueryable<LookUpTcsModuloDoGrupoDetalhe> GetLookUpTcsModuloDoGrupoDetalhe(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsModuloDoGrupoDetalhe";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsModuloDoGrupoDetalhe));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsModuloDoGrupoDetalhe> query =  null;
		
			
		
	        TcsModuloDoGrupoDetalhe.OnLookUpingLookUpTcsModuloDoGrupoDetalhe(ref query, propertyName, entitySearch);
	
	
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
	
		

	        if (entityName.InList("Linx.Framework.BV.Modulo.TcsModulo"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsModulo",
	        			NameSpace = "Linx.Framework.BV.Modulo",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "Módulos",
	        			ClearMethodName = "ClearTcsModulo",
	        			QueryMethodName  = "GetPagedTcsModulo",	
	        			CountingMethodName  = "GetTcsModulo" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Modulo.TcsModulo"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Modulo.TcsModulo"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Modulo.TcsModulo", "Linx.Framework.BV.Modulo.TcsModuloMenu"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsModuloMenu",
	        			NameSpace = "Linx.Framework.BV.Modulo",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsModulo",	
	        			DisplayName = "Menus",
	        			ClearMethodName = "ClearTcsModuloMenu",
	        			QueryMethodName  = "GetPagedTcsModuloMenu",	
	        			CountingMethodName  = "GetTcsModuloMenu" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Modulo.TcsModuloMenu"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Modulo.TcsModuloMenu"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Modulo.TcsModulo", "Linx.Framework.BV.Modulo.TcsTransacaoMenu"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsTransacaoMenu",
	        			NameSpace = "Linx.Framework.BV.Modulo",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsModuloMenu",	
	        			DisplayName = "Transação",
	        			ClearMethodName = "ClearTcsTransacaoMenu",
	        			QueryMethodName  = "GetPagedTcsTransacaoMenu",	
	        			CountingMethodName  = "GetTcsTransacaoMenu" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Modulo.TcsTransacaoMenu"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Modulo.TcsTransacaoMenu"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Modulo.TcsModulo", "Linx.Framework.BV.Modulo.TcsModuloDoGrupo"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsModuloDoGrupo",
	        			NameSpace = "Linx.Framework.BV.Modulo",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsModulo",	
	        			DisplayName = "Grupo",
	        			ClearMethodName = "ClearTcsModuloDoGrupo",
	        			QueryMethodName  = "GetPagedTcsModuloDoGrupo",	
	        			CountingMethodName  = "GetTcsModuloDoGrupo" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Modulo.TcsModuloDoGrupo"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Modulo.TcsModuloDoGrupo"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Modulo.TcsModuloGrupo"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsModuloGrupo",
	        			NameSpace = "Linx.Framework.BV.Modulo",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "Grupo de Módulos",
	        			ClearMethodName = "ClearTcsModuloGrupo",
	        			QueryMethodName  = "GetPagedTcsModuloGrupo",	
	        			CountingMethodName  = "GetTcsModuloGrupo" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Modulo.TcsModuloGrupo"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Modulo.TcsModuloGrupo"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Modulo.TcsModuloGrupo", "Linx.Framework.BV.Modulo.TcsModuloDoGrupoDetalhe"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsModuloDoGrupoDetalhe" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.Modulo",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsModuloGrupo",	
	        			DisplayName = "Módulo",
	        			ClearMethodName = "ClearTcsModuloDoGrupoDetalhe" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsModuloDoGrupoDetalhe" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsModuloDoGrupoDetalhe" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Modulo.TcsModuloDoGrupoDetalhe"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Modulo.TcsModuloDoGrupoDetalhe" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Modulo.AppModule"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "AppModule",
	        			NameSpace = "Linx.Framework.BV.Modulo",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "AppModule",
	        			ClearMethodName = "ClearAppModule",
	        			QueryMethodName  = "GetPagedAppModule",	
	        			CountingMethodName  = "GetAppModule" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Modulo.AppModule"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Modulo.AppModule"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Modulo.BreadCrumbItem"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "BreadCrumbItem",
	        			NameSpace = "Linx.Framework.BV.Modulo",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "BreadCrumbItem",
	        			ClearMethodName = "ClearBreadCrumbItem",
	        			QueryMethodName  = "GetPagedBreadCrumbItem",	
	        			CountingMethodName  = "GetBreadCrumbItem" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Modulo.BreadCrumbItem"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Modulo.BreadCrumbItem"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Modulo.AppMenu"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "AppMenu",
	        			NameSpace = "Linx.Framework.BV.Modulo",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "AppMenu",
	        			ClearMethodName = "ClearAppMenu",
	        			QueryMethodName  = "GetPagedAppMenu",	
	        			CountingMethodName  = "GetAppMenu" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Modulo.AppMenu"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Modulo.AppMenu"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Modulo.UserModules"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "UserModules",
	        			NameSpace = "Linx.Framework.BV.Modulo",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "UserModules",
	        			ClearMethodName = "ClearUserModules",
	        			QueryMethodName  = "GetPagedUserModules",	
	        			CountingMethodName  = "GetUserModules" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Modulo.UserModules"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Modulo.UserModules"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Modulo.TcsUsuarioFavorito"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsUsuarioFavorito",
	        			NameSpace = "Linx.Framework.BV.Modulo",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsUsuarioFavorito",
	        			ClearMethodName = "ClearTcsUsuarioFavorito",
	        			QueryMethodName  = "GetPagedTcsUsuarioFavorito",	
	        			CountingMethodName  = "GetTcsUsuarioFavorito" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Modulo.TcsUsuarioFavorito"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Modulo.TcsUsuarioFavorito"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Modulo.EnvironmentInfo"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "EnvironmentInfo",
	        			NameSpace = "Linx.Framework.BV.Modulo",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "EnvironmentInfo",
	        			ClearMethodName = "ClearEnvironmentInfo",
	        			QueryMethodName  = "GetPagedEnvironmentInfo",	
	        			CountingMethodName  = "GetEnvironmentInfo" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Modulo.EnvironmentInfo"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Modulo.EnvironmentInfo"), forceAll: forceAll)
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

         		    return new string[] { "Framework_ModuloClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.ModuloClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Framework_moduloService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.moduloService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear TcsModulo.
	    public IEnumerable<TcsModulo> ClearTcsModulo()
	    {
	        List<TcsModulo> result = new List<TcsModulo>();
	        result.Add(new TcsModulo(false));	
			
	        result[0].TcsModuloMenuList = new List<TcsModuloMenu>();
	        ((List<TcsModuloMenu>)result[0].TcsModuloMenuList).Add(new TcsModuloMenu(false));
			
	        ((List<TcsModuloMenu>)result[0].TcsModuloMenuList)[0].TcsTransacaoMenuList = new List<TcsTransacaoMenu>();
	        ((List<TcsTransacaoMenu>)((List<TcsModuloMenu>)result[0].TcsModuloMenuList)[0].TcsTransacaoMenuList).Add(new TcsTransacaoMenu());
			
	        result[0].TcsModuloDoGrupoList = new List<TcsModuloDoGrupo>();
	        ((List<TcsModuloDoGrupo>)result[0].TcsModuloDoGrupoList).Add(new TcsModuloDoGrupo());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsModuloMenu.
	    public IEnumerable<TcsModuloMenu> ClearTcsModuloMenu()
	    {
	        List<TcsModuloMenu> result = new List<TcsModuloMenu>();
	        result.Add(new TcsModuloMenu(false));	
			
	        result[0].TcsTransacaoMenuList = new List<TcsTransacaoMenu>();
	        ((List<TcsTransacaoMenu>)result[0].TcsTransacaoMenuList).Add(new TcsTransacaoMenu());
		
	        

	
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
	    //Clear TcsModuloDoGrupo.
	    public IEnumerable<TcsModuloDoGrupo> ClearTcsModuloDoGrupo()
	    {
	        List<TcsModuloDoGrupo> result = new List<TcsModuloDoGrupo>();
	        result.Add(new TcsModuloDoGrupo());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsModuloGrupo.
	    public IEnumerable<TcsModuloGrupo> ClearTcsModuloGrupo()
	    {
	        List<TcsModuloGrupo> result = new List<TcsModuloGrupo>();
	        result.Add(new TcsModuloGrupo());	
			
	        result[0].TcsModuloDoGrupoDetalheList = new List<TcsModuloDoGrupoDetalhe>();
	        ((List<TcsModuloDoGrupoDetalhe>)result[0].TcsModuloDoGrupoDetalheList).Add(new TcsModuloDoGrupoDetalhe());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsModuloDoGrupoDetalhe.
	    public IEnumerable<TcsModuloDoGrupoDetalhe> ClearTcsModuloDoGrupoDetalhe()
	    {
	        List<TcsModuloDoGrupoDetalhe> result = new List<TcsModuloDoGrupoDetalhe>();
	        result.Add(new TcsModuloDoGrupoDetalhe());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear AppModule.
	    public IEnumerable<AppModule> ClearAppModule()
	    {
	        List<AppModule> result = new List<AppModule>();
	        result.Add(new AppModule(false));	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear BreadCrumbItem.
	    public IEnumerable<BreadCrumbItem> ClearBreadCrumbItem()
	    {
	        List<BreadCrumbItem> result = new List<BreadCrumbItem>();
	        result.Add(new BreadCrumbItem());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear AppMenu.
	    public IEnumerable<AppMenu> ClearAppMenu()
	    {
	        List<AppMenu> result = new List<AppMenu>();
	        result.Add(new AppMenu(false));	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear UserModules.
	    public IEnumerable<UserModules> ClearUserModules()
	    {
	        List<UserModules> result = new List<UserModules>();
	        result.Add(new UserModules());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsUsuarioFavorito.
	    public IEnumerable<TcsUsuarioFavorito> ClearTcsUsuarioFavorito()
	    {
	        List<TcsUsuarioFavorito> result = new List<TcsUsuarioFavorito>();
	        result.Add(new TcsUsuarioFavorito());	
		
	        

	
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
	    //Get TcsModulo.
	    public IQueryable<TcsModulo> GetTcsModulo()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsModulo> result = 
	            (from entity0 in this.DbContext.TCS_MODULO
                orderby entity0.DESC_MODULO ascending
	            
	            	
	            select new TcsModulo()		
	            {
	            
                DescModulo = entity0.DESC_MODULO
                , Icone = entity0.ICONE
                , IdModulo = entity0.ID_MODULO
                , IdTcsAplicativo = entity0.ID_TCS_APLICATIVO
                , Inativo = entity0.INATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , LxCorFundoName = ((entity0.LX_COR_FUNDO) == 8 ? "Fundo Laranja" : ((entity0.LX_COR_FUNDO) == 10 ? "Fundo Roxo" : ((entity0.LX_COR_FUNDO) == 7 ? "Laranja" : ((entity0.LX_COR_FUNDO) == 9 ? "Roxo" : ""))))
                , NomeCurto = entity0.NOME_CURTO
                , NomeTabela = ""
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
			
                ,TcsModuloMenuList = 
	                        (from entity1 in entity0.TCS_MODULO_MENU_LISTA
                                  let entity1Al2 = entity1.TCS_MODULO
                                  let entity1Al1 = entity1.MODULO_MENU_SUPERIOR
	                        
	                        	
	                        select new TcsModuloMenu()
	                        {
	                        
                                DescModulo = entity1Al2.DESC_MODULO
                                , DescModuloMenu = entity1.DESC_MODULO_MENU
                                , DescModuloMenuSuperior = entity1Al1.DESC_MODULO_MENU
                                , Icone = entity1.ICONE
                                , IdModulo = entity1Al2.ID_MODULO
                                , IdModuloMenu = entity1.ID_MODULO_MENU
                                , IdModuloMenuSuperior = entity1Al1.ID_MODULO_MENU
                                , IdTcsAplicativo = entity1Al2.ID_TCS_APLICATIVO
                                , LxCorFundo = entity1.LX_COR_FUNDO
                                , LxCorFundoName = ((entity1.LX_COR_FUNDO) == 8 ? "Fundo Laranja" : ((entity1.LX_COR_FUNDO) == 10 ? "Fundo Roxo" : ((entity1.LX_COR_FUNDO) == 7 ? "Laranja" : ((entity1.LX_COR_FUNDO) == 9 ? "Roxo" : ""))))
                                , NomeCurto = entity1.NOME_CURTO
                                , NomeTabela = "TCS_MODULO_MENU"
                                , OrdemNavegacao = entity1.ORDEM_NAVEGACAO
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsModuloMenu.
	    public IQueryable<TcsModuloMenu> GetTcsModuloMenu()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsModuloMenu> result = 
	            (from entity0 in this.DbContext.TCS_MODULO_MENU
                  let entity0Al2 = entity0.TCS_MODULO
                  let entity0Al1 = entity0.MODULO_MENU_SUPERIOR
	            
	            	
	            select new TcsModuloMenu()		
	            {
	            
                DescModulo = entity0Al2.DESC_MODULO
                , DescModuloMenu = entity0.DESC_MODULO_MENU
                , DescModuloMenuSuperior = entity0Al1.DESC_MODULO_MENU
                , Icone = entity0.ICONE
                , IdModulo = entity0Al2.ID_MODULO
                , IdModuloMenu = entity0.ID_MODULO_MENU
                , IdModuloMenuSuperior = entity0Al1.ID_MODULO_MENU
                , IdTcsAplicativo = entity0Al2.ID_TCS_APLICATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , LxCorFundoName = ((entity0.LX_COR_FUNDO) == 8 ? "Fundo Laranja" : ((entity0.LX_COR_FUNDO) == 10 ? "Fundo Roxo" : ((entity0.LX_COR_FUNDO) == 7 ? "Laranja" : ((entity0.LX_COR_FUNDO) == 9 ? "Roxo" : ""))))
                , NomeCurto = entity0.NOME_CURTO
                , NomeTabela = "TCS_MODULO_MENU"
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
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
                , IdTcsTransacaoMenu = entity0.ID_TCS_TRANSACAO_MENU
                , IdTransacao = entity0.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
                , DescTransacao = ""
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsModuloDoGrupo.
	    public IQueryable<TcsModuloDoGrupo> GetTcsModuloDoGrupo()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsModuloDoGrupo> result = 
	            (from entity0 in this.DbContext.TCS_MODULO_DO_GRUPO
                  let entity0Al1 = entity0.TCS_MODULO_GRUPO
	            
	            	
	            select new TcsModuloDoGrupo()		
	            {
	            
                DescGrupoModulo = entity0Al1.DESC_GRUPO_MODULO
                , IdGrupoModulo = entity0Al1.ID_GRUPO_MODULO
                , IdModulo = entity0.ID_MODULO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsModuloNoAssociations.
	    public IQueryable<TcsModulo> GetTcsModuloNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsModulo> result = 
	            (from entity0 in this.DbContext.TCS_MODULO
                orderby entity0.DESC_MODULO ascending
	            
	            	
	            select new TcsModulo()		
	            {
	            
                DescModulo = entity0.DESC_MODULO
                , Icone = entity0.ICONE
                , IdModulo = entity0.ID_MODULO
                , IdTcsAplicativo = entity0.ID_TCS_APLICATIVO
                , Inativo = entity0.INATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , LxCorFundoName = ((entity0.LX_COR_FUNDO) == 8 ? "Fundo Laranja" : ((entity0.LX_COR_FUNDO) == 10 ? "Fundo Roxo" : ((entity0.LX_COR_FUNDO) == 7 ? "Laranja" : ((entity0.LX_COR_FUNDO) == 9 ? "Roxo" : ""))))
                , NomeCurto = entity0.NOME_CURTO
                , NomeTabela = ""
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsModuloMenuNoAssociations.
	    public IQueryable<TcsModuloMenu> GetTcsModuloMenuNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsModuloMenu> result = 
	            (from entity0 in this.DbContext.TCS_MODULO_MENU
                  let entity0Al2 = entity0.TCS_MODULO
                  let entity0Al1 = entity0.MODULO_MENU_SUPERIOR
	            
	            	
	            select new TcsModuloMenu()		
	            {
	            
                DescModulo = entity0Al2.DESC_MODULO
                , DescModuloMenu = entity0.DESC_MODULO_MENU
                , DescModuloMenuSuperior = entity0Al1.DESC_MODULO_MENU
                , Icone = entity0.ICONE
                , IdModulo = entity0Al2.ID_MODULO
                , IdModuloMenu = entity0.ID_MODULO_MENU
                , IdModuloMenuSuperior = entity0Al1.ID_MODULO_MENU
                , IdTcsAplicativo = entity0Al2.ID_TCS_APLICATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , LxCorFundoName = ((entity0.LX_COR_FUNDO) == 8 ? "Fundo Laranja" : ((entity0.LX_COR_FUNDO) == 10 ? "Fundo Roxo" : ((entity0.LX_COR_FUNDO) == 7 ? "Laranja" : ((entity0.LX_COR_FUNDO) == 9 ? "Roxo" : ""))))
                , NomeCurto = entity0.NOME_CURTO
                , NomeTabela = "TCS_MODULO_MENU"
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
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
                , IdTcsTransacaoMenu = entity0.ID_TCS_TRANSACAO_MENU
                , IdTransacao = entity0.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
                , DescTransacao = ""
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsModuloDoGrupoNoAssociations.
	    public IQueryable<TcsModuloDoGrupo> GetTcsModuloDoGrupoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsModuloDoGrupo> result = 
	            (from entity0 in this.DbContext.TCS_MODULO_DO_GRUPO
                  let entity0Al1 = entity0.TCS_MODULO_GRUPO
	            
	            	
	            select new TcsModuloDoGrupo()		
	            {
	            
                DescGrupoModulo = entity0Al1.DESC_GRUPO_MODULO
                , IdGrupoModulo = entity0Al1.ID_GRUPO_MODULO
                , IdModulo = entity0.ID_MODULO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsModuloGrupo.
	    public IQueryable<TcsModuloGrupo> GetTcsModuloGrupo()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsModuloGrupo> result = 
	            (from entity0 in this.DbContext.TCS_MODULO_GRUPO
	            
	            	
	            select new TcsModuloGrupo()		
	            {
	            
                DescGrupoModulo = entity0.DESC_GRUPO_MODULO
                , IdGrupoModulo = entity0.ID_GRUPO_MODULO
                , IdTcsAplicativo = entity0.ID_TCS_APLICATIVO
			
                ,TcsModuloDoGrupoDetalheList = 
	                        (from entity1 in entity0.TCS_MODULO_DO_GRUPO_LISTA
                                  let entity1Al1 = entity1.TCS_MODULO_GRUPO
	                        
	                        	
	                        select new TcsModuloDoGrupoDetalhe()
	                        {
	                        
                                IdGrupoModulo = entity1Al1.ID_GRUPO_MODULO
                                , IdModulo = entity1.ID_MODULO
                                , IdModuloDoGrupo = entity1.ID_MODULO_DO_GRUPO
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsModuloDoGrupoDetalhe.
	    public IQueryable<TcsModuloDoGrupoDetalhe> GetTcsModuloDoGrupoDetalhe()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsModuloDoGrupoDetalhe> result = 
	            (from entity0 in this.DbContext.TCS_MODULO_DO_GRUPO
                  let entity0Al1 = entity0.TCS_MODULO_GRUPO
	            
	            	
	            select new TcsModuloDoGrupoDetalhe()		
	            {
	            
                IdGrupoModulo = entity0Al1.ID_GRUPO_MODULO
                , IdModulo = entity0.ID_MODULO
                , IdModuloDoGrupo = entity0.ID_MODULO_DO_GRUPO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsModuloGrupoNoAssociations.
	    public IQueryable<TcsModuloGrupo> GetTcsModuloGrupoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsModuloGrupo> result = 
	            (from entity0 in this.DbContext.TCS_MODULO_GRUPO
	            
	            	
	            select new TcsModuloGrupo()		
	            {
	            
                DescGrupoModulo = entity0.DESC_GRUPO_MODULO
                , IdGrupoModulo = entity0.ID_GRUPO_MODULO
                , IdTcsAplicativo = entity0.ID_TCS_APLICATIVO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsModuloDoGrupoDetalheNoAssociations.
	    public IQueryable<TcsModuloDoGrupoDetalhe> GetTcsModuloDoGrupoDetalheNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsModuloDoGrupoDetalhe> result = 
	            (from entity0 in this.DbContext.TCS_MODULO_DO_GRUPO
                  let entity0Al1 = entity0.TCS_MODULO_GRUPO
	            
	            	
	            select new TcsModuloDoGrupoDetalhe()		
	            {
	            
                IdGrupoModulo = entity0Al1.ID_GRUPO_MODULO
                , IdModulo = entity0.ID_MODULO
                , IdModuloDoGrupo = entity0.ID_MODULO_DO_GRUPO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get AppModule.
	    public IEnumerable<AppModule> GetAppModule()
	    {




	
	        IEnumerable<AppModule> result = new List<AppModule>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get AppModuleNoAssociations.
	    public IEnumerable<AppModule> GetAppModuleNoAssociations()
	    {




	
	        IEnumerable<AppModule> result = new List<AppModule>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get BreadCrumbItem.
	    public IEnumerable<BreadCrumbItem> GetBreadCrumbItem()
	    {




	
	        IEnumerable<BreadCrumbItem> result = new List<BreadCrumbItem>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get BreadCrumbItemNoAssociations.
	    public IEnumerable<BreadCrumbItem> GetBreadCrumbItemNoAssociations()
	    {




	
	        IEnumerable<BreadCrumbItem> result = new List<BreadCrumbItem>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get AppMenu.
	    public IEnumerable<AppMenu> GetAppMenu()
	    {




	
	        IEnumerable<AppMenu> result = new List<AppMenu>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get AppMenuNoAssociations.
	    public IEnumerable<AppMenu> GetAppMenuNoAssociations()
	    {




	
	        IEnumerable<AppMenu> result = new List<AppMenu>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get UserModules.
	    public IEnumerable<UserModules> GetUserModules()
	    {




	
	        IEnumerable<UserModules> result = new List<UserModules>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get UserModulesNoAssociations.
	    public IEnumerable<UserModules> GetUserModulesNoAssociations()
	    {




	
	        IEnumerable<UserModules> result = new List<UserModules>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsUsuarioFavorito.
	    public IQueryable<TcsUsuarioFavorito> GetTcsUsuarioFavorito()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioFavorito> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_FAVORITO
                  let entity0Al1 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioFavorito()		
	            {
	            
                IdModulo = entity0.ID_MODULO
                , IdModuloMenu = entity0.ID_MODULO_MENU
                , IdTcsUsuarioFavorito = entity0.ID_TCS_USUARIO_FAVORITO
                , IdTransacao = entity0.ID_TRANSACAO
                , IdUsuario = entity0Al1.ID_USUARIO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioFavoritoNoAssociations.
	    public IQueryable<TcsUsuarioFavorito> GetTcsUsuarioFavoritoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioFavorito> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_FAVORITO
                  let entity0Al1 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioFavorito()		
	            {
	            
                IdModulo = entity0.ID_MODULO
                , IdModuloMenu = entity0.ID_MODULO_MENU
                , IdTcsUsuarioFavorito = entity0.ID_TCS_USUARIO_FAVORITO
                , IdTransacao = entity0.ID_TRANSACAO
                , IdUsuario = entity0Al1.ID_USUARIO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            );
		
	
	        	

	
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
	    	result.Add("TcsModulo|LxCorFundo");
	    	result.Add("TcsModulo|TCS_MODULO.LX_COR_FUNDO");
	    	result.Add("TcsModulo|NomeTabela");
	    	result.Add("TcsModulo|''");
	    	//Add filtering disabled property for TCS_MODULO
	    	string[] bmDisabledTcsModuloList = this.GetEDM().GetFilteringDisabledList("TCS_MODULO");
	    	if (bmDisabledTcsModuloList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsModuloList.Contains("TCS_MODULO.DESC_MODULO"))
	    		{
	    			result.Add("TcsModulo|DescModulo");
	    			result.Add("TcsModulo|TCS_MODULO.DESC_MODULO");
	    		}
	
	    		if (bmDisabledTcsModuloList.Contains("TCS_MODULO.ICONE"))
	    		{
	    			result.Add("TcsModulo|Icone");
	    			result.Add("TcsModulo|TCS_MODULO.ICONE");
	    		}
	
	    		if (bmDisabledTcsModuloList.Contains("TCS_MODULO.ID_MODULO"))
	    		{
	    			result.Add("TcsModulo|IdModulo");
	    			result.Add("TcsModulo|TCS_MODULO.ID_MODULO");
	    		}
	
	    		if (bmDisabledTcsModuloList.Contains("TCS_MODULO.ID_TCS_APLICATIVO"))
	    		{
	    			result.Add("TcsModulo|IdTcsAplicativo");
	    			result.Add("TcsModulo|TCS_MODULO.ID_TCS_APLICATIVO");
	    		}
	
	    		if (bmDisabledTcsModuloList.Contains("TCS_MODULO.INATIVO"))
	    		{
	    			result.Add("TcsModulo|Inativo");
	    			result.Add("TcsModulo|TCS_MODULO.INATIVO");
	    		}
	
	    		if (bmDisabledTcsModuloList.Contains("TCS_MODULO.NOME_CURTO"))
	    		{
	    			result.Add("TcsModulo|NomeCurto");
	    			result.Add("TcsModulo|TCS_MODULO.NOME_CURTO");
	    		}
	
	    		if (bmDisabledTcsModuloList.Contains("TCS_MODULO.ORDEM_NAVEGACAO"))
	    		{
	    			result.Add("TcsModulo|OrdemNavegacao");
	    			result.Add("TcsModulo|TCS_MODULO.ORDEM_NAVEGACAO");
	    		}
	    	}
	    	result.Add("TcsModuloMenu|DescModulo");
	    	result.Add("TcsModuloMenu|TCS_MODULO_MENU.TCS_MODULO.DESC_MODULO");
	    	result.Add("TcsModuloMenu|LxCorFundo");
	    	result.Add("TcsModuloMenu|TCS_MODULO_MENU.LX_COR_FUNDO");
	    	result.Add("TcsModuloMenu|NomeTabela");
	    	result.Add("TcsModuloMenu|'TCS_MODULO_MENU'");
	    	//Add filtering disabled property for TCS_MODULO_MENU
	    	string[] bmDisabledTcsModuloMenuList = this.GetEDM().GetFilteringDisabledList("TCS_MODULO_MENU");
	    	if (bmDisabledTcsModuloMenuList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsModuloMenuList.Contains("TCS_MODULO_MENU.DESC_MODULO_MENU"))
	    		{
	    			result.Add("TcsModuloMenu|DescModuloMenu");
	    			result.Add("TcsModuloMenu|TCS_MODULO_MENU.DESC_MODULO_MENU");
	    		}
	
	    		if (bmDisabledTcsModuloMenuList.Contains("TCS_MODULO_MENU.ICONE"))
	    		{
	    			result.Add("TcsModuloMenu|Icone");
	    			result.Add("TcsModuloMenu|TCS_MODULO_MENU.ICONE");
	    		}
	
	    		if (bmDisabledTcsModuloMenuList.Contains("TCS_MODULO_MENU.ID_MODULO_MENU"))
	    		{
	    			result.Add("TcsModuloMenu|IdModuloMenu");
	    			result.Add("TcsModuloMenu|TCS_MODULO_MENU.ID_MODULO_MENU");
	    		}
	
	    		if (bmDisabledTcsModuloMenuList.Contains("TCS_MODULO_MENU.NOME_CURTO"))
	    		{
	    			result.Add("TcsModuloMenu|NomeCurto");
	    			result.Add("TcsModuloMenu|TCS_MODULO_MENU.NOME_CURTO");
	    		}
	
	    		if (bmDisabledTcsModuloMenuList.Contains("TCS_MODULO_MENU.ORDEM_NAVEGACAO"))
	    		{
	    			result.Add("TcsModuloMenu|OrdemNavegacao");
	    			result.Add("TcsModuloMenu|TCS_MODULO_MENU.ORDEM_NAVEGACAO");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_MODULO_DO_GRUPO
	    	string[] bmDisabledTcsModuloDoGrupoList = this.GetEDM().GetFilteringDisabledList("TCS_MODULO_DO_GRUPO");
	    	if (bmDisabledTcsModuloDoGrupoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsModuloDoGrupoList.Contains("TCS_MODULO_DO_GRUPO.ID_MODULO"))
	    		{
	    			result.Add("TcsModuloDoGrupo|IdModulo");
	    			result.Add("TcsModuloDoGrupo|TCS_MODULO_DO_GRUPO.ID_MODULO");
	    		}
	    	}
	    	result.Add("TcsTransacaoMenu|DescTransacao");
	    	result.Add("TcsTransacaoMenu|''");
	    	//Add filtering disabled property for TCS_TRANSACAO_MENU
	    	string[] bmDisabledTcsTransacaoMenuList = this.GetEDM().GetFilteringDisabledList("TCS_TRANSACAO_MENU");
	    	if (bmDisabledTcsTransacaoMenuList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsTransacaoMenuList.Contains("TCS_TRANSACAO_MENU.ID_MODULO_MENU"))
	    		{
	    			result.Add("TcsTransacaoMenu|IdModuloMenu");
	    			result.Add("TcsTransacaoMenu|TCS_TRANSACAO_MENU.ID_MODULO_MENU");
	    		}
	
	    		if (bmDisabledTcsTransacaoMenuList.Contains("TCS_TRANSACAO_MENU.ID_TCS_TRANSACAO_MENU"))
	    		{
	    			result.Add("TcsTransacaoMenu|IdTcsTransacaoMenu");
	    			result.Add("TcsTransacaoMenu|TCS_TRANSACAO_MENU.ID_TCS_TRANSACAO_MENU");
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
	    	}
	    	//Add filtering disabled property for TCS_MODULO_GRUPO
	    	string[] bmDisabledTcsModuloGrupoList = this.GetEDM().GetFilteringDisabledList("TCS_MODULO_GRUPO");
	    	if (bmDisabledTcsModuloGrupoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsModuloGrupoList.Contains("TCS_MODULO_GRUPO.DESC_GRUPO_MODULO"))
	    		{
	    			result.Add("TcsModuloGrupo|DescGrupoModulo");
	    			result.Add("TcsModuloGrupo|TCS_MODULO_GRUPO.DESC_GRUPO_MODULO");
	    		}
	
	    		if (bmDisabledTcsModuloGrupoList.Contains("TCS_MODULO_GRUPO.ID_GRUPO_MODULO"))
	    		{
	    			result.Add("TcsModuloGrupo|IdGrupoModulo");
	    			result.Add("TcsModuloGrupo|TCS_MODULO_GRUPO.ID_GRUPO_MODULO");
	    		}
	
	    		if (bmDisabledTcsModuloGrupoList.Contains("TCS_MODULO_GRUPO.ID_TCS_APLICATIVO"))
	    		{
	    			result.Add("TcsModuloGrupo|IdTcsAplicativo");
	    			result.Add("TcsModuloGrupo|TCS_MODULO_GRUPO.ID_TCS_APLICATIVO");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_MODULO_DO_GRUPO
	    	string[] bmDisabledTcsModuloDoGrupoDetalheList = this.GetEDM().GetFilteringDisabledList("TCS_MODULO_DO_GRUPO");
	    	if (bmDisabledTcsModuloDoGrupoDetalheList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsModuloDoGrupoDetalheList.Contains("TCS_MODULO_DO_GRUPO.ID_MODULO"))
	    		{
	    			result.Add("TcsModuloDoGrupoDetalhe|IdModulo");
	    			result.Add("TcsModuloDoGrupoDetalhe|TCS_MODULO_DO_GRUPO.ID_MODULO");
	    		}
	
	    		if (bmDisabledTcsModuloDoGrupoDetalheList.Contains("TCS_MODULO_DO_GRUPO.ID_MODULO_DO_GRUPO"))
	    		{
	    			result.Add("TcsModuloDoGrupoDetalhe|IdModuloDoGrupo");
	    			result.Add("TcsModuloDoGrupoDetalhe|TCS_MODULO_DO_GRUPO.ID_MODULO_DO_GRUPO");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_USUARIO_FAVORITO
	    	string[] bmDisabledTcsUsuarioFavoritoList = this.GetEDM().GetFilteringDisabledList("TCS_USUARIO_FAVORITO");
	    	if (bmDisabledTcsUsuarioFavoritoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsUsuarioFavoritoList.Contains("TCS_USUARIO_FAVORITO.ID_MODULO"))
	    		{
	    			result.Add("TcsUsuarioFavorito|IdModulo");
	    			result.Add("TcsUsuarioFavorito|TCS_USUARIO_FAVORITO.ID_MODULO");
	    		}
	
	    		if (bmDisabledTcsUsuarioFavoritoList.Contains("TCS_USUARIO_FAVORITO.ID_MODULO_MENU"))
	    		{
	    			result.Add("TcsUsuarioFavorito|IdModuloMenu");
	    			result.Add("TcsUsuarioFavorito|TCS_USUARIO_FAVORITO.ID_MODULO_MENU");
	    		}
	
	    		if (bmDisabledTcsUsuarioFavoritoList.Contains("TCS_USUARIO_FAVORITO.ID_TCS_USUARIO_FAVORITO"))
	    		{
	    			result.Add("TcsUsuarioFavorito|IdTcsUsuarioFavorito");
	    			result.Add("TcsUsuarioFavorito|TCS_USUARIO_FAVORITO.ID_TCS_USUARIO_FAVORITO");
	    		}
	
	    		if (bmDisabledTcsUsuarioFavoritoList.Contains("TCS_USUARIO_FAVORITO.ID_TRANSACAO"))
	    		{
	    			result.Add("TcsUsuarioFavorito|IdTransacao");
	    			result.Add("TcsUsuarioFavorito|TCS_USUARIO_FAVORITO.ID_TRANSACAO");
	    		}
	
	    		if (bmDisabledTcsUsuarioFavoritoList.Contains("TCS_USUARIO_FAVORITO.ORDEM_NAVEGACAO"))
	    		{
	    			result.Add("TcsUsuarioFavorito|OrdemNavegacao");
	    			result.Add("TcsUsuarioFavorito|TCS_USUARIO_FAVORITO.ORDEM_NAVEGACAO");
	    		}
	    	}
	    	return result;
	    }

	    #endregion Excluded Filters

	    #region Get By EntitySearchId Methods Definitions.
	
				
	    [Ignore]
	    //Get TcsModulo By EntitySearchId.
	    public IQueryable<TcsModulo> GetTcsModuloByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsModuloByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsModuloMenu By EntitySearchId.
	    public IQueryable<TcsModuloMenu> GetTcsModuloMenuByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsModuloMenuByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsTransacaoMenu By EntitySearchId.
	    public IQueryable<TcsTransacaoMenu> GetTcsTransacaoMenuByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsTransacaoMenuByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsModuloDoGrupo By EntitySearchId.
	    public IQueryable<TcsModuloDoGrupo> GetTcsModuloDoGrupoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsModuloDoGrupoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsModulo By EntitySearchId.
	    public IQueryable<TcsModulo> GetTcsModuloByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsModuloByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsModuloMenu By EntitySearchId.
	    public IQueryable<TcsModuloMenu> GetTcsModuloMenuByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsModuloMenuByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsTransacaoMenu By EntitySearchId.
	    public IQueryable<TcsTransacaoMenu> GetTcsTransacaoMenuByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsTransacaoMenuByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsModuloDoGrupo By EntitySearchId.
	    public IQueryable<TcsModuloDoGrupo> GetTcsModuloDoGrupoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsModuloDoGrupoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsModuloGrupo By EntitySearchId.
	    public IQueryable<TcsModuloGrupo> GetTcsModuloGrupoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsModuloGrupoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsModuloDoGrupoDetalhe By EntitySearchId.
	    public IQueryable<TcsModuloDoGrupoDetalhe> GetTcsModuloDoGrupoDetalheByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsModuloDoGrupoDetalheByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsModuloGrupo By EntitySearchId.
	    public IQueryable<TcsModuloGrupo> GetTcsModuloGrupoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsModuloGrupoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsModuloDoGrupoDetalhe By EntitySearchId.
	    public IQueryable<TcsModuloDoGrupoDetalhe> GetTcsModuloDoGrupoDetalheByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsModuloDoGrupoDetalheByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get AppModule By EntitySearchId.
	    public IEnumerable<AppModule> GetAppModuleByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetAppModuleByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get AppModule By EntitySearchId.
	    public IEnumerable<AppModule> GetAppModuleByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetAppModuleByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get BreadCrumbItem By EntitySearchId.
	    public IEnumerable<BreadCrumbItem> GetBreadCrumbItemByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetBreadCrumbItemByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get BreadCrumbItem By EntitySearchId.
	    public IEnumerable<BreadCrumbItem> GetBreadCrumbItemByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetBreadCrumbItemByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get AppMenu By EntitySearchId.
	    public IEnumerable<AppMenu> GetAppMenuByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetAppMenuByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get AppMenu By EntitySearchId.
	    public IEnumerable<AppMenu> GetAppMenuByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetAppMenuByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get UserModules By EntitySearchId.
	    public IEnumerable<UserModules> GetUserModulesByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetUserModulesByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get UserModules By EntitySearchId.
	    public IEnumerable<UserModules> GetUserModulesByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetUserModulesByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioFavorito By EntitySearchId.
	    public IQueryable<TcsUsuarioFavorito> GetTcsUsuarioFavoritoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioFavoritoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioFavorito By EntitySearchId.
	    public IQueryable<TcsUsuarioFavorito> GetTcsUsuarioFavoritoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioFavoritoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
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
	
			
	    //Get TcsModulo By Example.
	    [Ignore]
	    public IQueryable<TcsModulo> GetTcsModuloByExample(TcsModulo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsModuloByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsModuloMenu By Example.
	    [Ignore]
	    public IQueryable<TcsModuloMenu> GetTcsModuloMenuByExample(TcsModuloMenu entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsModuloMenuByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsTransacaoMenu By Example.
	    [Ignore]
	    public IQueryable<TcsTransacaoMenu> GetTcsTransacaoMenuByExample(TcsTransacaoMenu entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsTransacaoMenuByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsModuloDoGrupo By Example.
	    [Ignore]
	    public IQueryable<TcsModuloDoGrupo> GetTcsModuloDoGrupoByExample(TcsModuloDoGrupo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsModuloDoGrupoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsModulo By Example.
	    [Ignore]
	    public IQueryable<TcsModulo> GetTcsModuloByExampleNoAssociations(TcsModulo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsModuloByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsModuloMenu By Example.
	    [Ignore]
	    public IQueryable<TcsModuloMenu> GetTcsModuloMenuByExampleNoAssociations(TcsModuloMenu entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsModuloMenuByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsTransacaoMenu By Example.
	    [Ignore]
	    public IQueryable<TcsTransacaoMenu> GetTcsTransacaoMenuByExampleNoAssociations(TcsTransacaoMenu entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsTransacaoMenuByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsModuloDoGrupo By Example.
	    [Ignore]
	    public IQueryable<TcsModuloDoGrupo> GetTcsModuloDoGrupoByExampleNoAssociations(TcsModuloDoGrupo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsModuloDoGrupoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsModuloGrupo By Example.
	    [Ignore]
	    public IQueryable<TcsModuloGrupo> GetTcsModuloGrupoByExample(TcsModuloGrupo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsModuloGrupoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsModuloDoGrupoDetalhe By Example.
	    [Ignore]
	    public IQueryable<TcsModuloDoGrupoDetalhe> GetTcsModuloDoGrupoDetalheByExample(TcsModuloDoGrupoDetalhe entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsModuloDoGrupoDetalheByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsModuloGrupo By Example.
	    [Ignore]
	    public IQueryable<TcsModuloGrupo> GetTcsModuloGrupoByExampleNoAssociations(TcsModuloGrupo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsModuloGrupoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsModuloDoGrupoDetalhe By Example.
	    [Ignore]
	    public IQueryable<TcsModuloDoGrupoDetalhe> GetTcsModuloDoGrupoDetalheByExampleNoAssociations(TcsModuloDoGrupoDetalhe entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsModuloDoGrupoDetalheByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get AppModule By Example.
	    [Ignore]
	    public IEnumerable<AppModule> GetAppModuleByExample(AppModule entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetAppModuleByEntitySearch(queryAnalysis);
	    }
			
	    //Get AppModule By Example.
	    [Ignore]
	    public IEnumerable<AppModule> GetAppModuleByExampleNoAssociations(AppModule entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetAppModuleByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get BreadCrumbItem By Example.
	    [Ignore]
	    public IEnumerable<BreadCrumbItem> GetBreadCrumbItemByExample(BreadCrumbItem entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetBreadCrumbItemByEntitySearch(queryAnalysis);
	    }
			
	    //Get BreadCrumbItem By Example.
	    [Ignore]
	    public IEnumerable<BreadCrumbItem> GetBreadCrumbItemByExampleNoAssociations(BreadCrumbItem entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetBreadCrumbItemByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get AppMenu By Example.
	    [Ignore]
	    public IEnumerable<AppMenu> GetAppMenuByExample(AppMenu entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetAppMenuByEntitySearch(queryAnalysis);
	    }
			
	    //Get AppMenu By Example.
	    [Ignore]
	    public IEnumerable<AppMenu> GetAppMenuByExampleNoAssociations(AppMenu entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetAppMenuByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get UserModules By Example.
	    [Ignore]
	    public IEnumerable<UserModules> GetUserModulesByExample(UserModules entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetUserModulesByEntitySearch(queryAnalysis);
	    }
			
	    //Get UserModules By Example.
	    [Ignore]
	    public IEnumerable<UserModules> GetUserModulesByExampleNoAssociations(UserModules entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetUserModulesByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsUsuarioFavorito By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioFavorito> GetTcsUsuarioFavoritoByExample(TcsUsuarioFavorito entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioFavoritoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsUsuarioFavorito By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioFavorito> GetTcsUsuarioFavoritoByExampleNoAssociations(TcsUsuarioFavorito entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioFavoritoByEntitySearchNoAssociations(queryAnalysis);
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
	    public TcsModulo GetTcsModuloByKey(Int64 idModulo)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsModulo");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdModulo"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idModulo));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsModuloByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsModuloMenu GetTcsModuloMenuByKey(Int64 idModuloMenu)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsModuloMenu");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdModuloMenu"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idModuloMenu));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsModuloMenuByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsModuloDoGrupo GetTcsModuloDoGrupoByKey(Int64 idGrupoModulo, Int64 idModulo)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsModuloDoGrupo");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdGrupoModulo"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idGrupoModulo));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdModulo"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idModulo));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsModuloDoGrupoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsTransacaoMenu GetTcsTransacaoMenuByKey(Int32 idTcsTransacaoMenu)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsTransacaoMenu");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsTransacaoMenu"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsTransacaoMenu));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsTransacaoMenuByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsModuloGrupo GetTcsModuloGrupoByKey(Int64 idGrupoModulo)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsModuloGrupo");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdGrupoModulo"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idGrupoModulo));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsModuloGrupoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsModuloDoGrupoDetalhe GetTcsModuloDoGrupoDetalheByKey(Int64 idModuloDoGrupo)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsModuloDoGrupoDetalhe");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdModuloDoGrupo"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idModuloDoGrupo));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsModuloDoGrupoDetalheByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public AppModule GetAppModuleByKey(Int64 id)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("AppModule");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "Id"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, id));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetAppModuleByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public BreadCrumbItem GetBreadCrumbItemByKey(Guid moduleKey)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("BreadCrumbItem");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "ModuleKey"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, moduleKey));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetBreadCrumbItemByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public AppMenu GetAppMenuByKey(Int64 id, int idTcsAmbiente)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("AppMenu");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "Id"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, id));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsAmbiente"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsAmbiente));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetAppMenuByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public UserModules GetUserModulesByKey(string hash)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("UserModules");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "Hash"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, hash));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetUserModulesByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsUsuarioFavorito GetTcsUsuarioFavoritoByKey(Int64 idTcsUsuarioFavorito)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsUsuarioFavorito");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsUsuarioFavorito"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsUsuarioFavorito));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsUsuarioFavoritoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	    //Get TcsModuloByEntitySearch.
	    public IQueryable<TcsModulo> GetTcsModuloByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsModulo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsModulo> result = 
	            (from entity0 in this.DbContext.TCS_MODULO.Where(dynQuery, parameters.ToArray())
                orderby entity0.DESC_MODULO ascending
	            
	            	
	            select new TcsModulo()		
	            {
	            
                DescModulo = entity0.DESC_MODULO
                , Icone = entity0.ICONE
                , IdModulo = entity0.ID_MODULO
                , IdTcsAplicativo = entity0.ID_TCS_APLICATIVO
                , Inativo = entity0.INATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , LxCorFundoName = ((entity0.LX_COR_FUNDO) == 8 ? "Fundo Laranja" : ((entity0.LX_COR_FUNDO) == 10 ? "Fundo Roxo" : ((entity0.LX_COR_FUNDO) == 7 ? "Laranja" : ((entity0.LX_COR_FUNDO) == 9 ? "Roxo" : ""))))
                , NomeCurto = entity0.NOME_CURTO
                , NomeTabela = ""
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
			
                ,TcsModuloMenuList = 
	                        (from entity1 in entity0.TCS_MODULO_MENU_LISTA
                                  let entity1Al2 = entity1.TCS_MODULO
                                  let entity1Al1 = entity1.MODULO_MENU_SUPERIOR
	                        
	                        	
	                        select new TcsModuloMenu()
	                        {
	                        
                                DescModulo = entity1Al2.DESC_MODULO
                                , DescModuloMenu = entity1.DESC_MODULO_MENU
                                , DescModuloMenuSuperior = entity1Al1.DESC_MODULO_MENU
                                , Icone = entity1.ICONE
                                , IdModulo = entity1Al2.ID_MODULO
                                , IdModuloMenu = entity1.ID_MODULO_MENU
                                , IdModuloMenuSuperior = entity1Al1.ID_MODULO_MENU
                                , IdTcsAplicativo = entity1Al2.ID_TCS_APLICATIVO
                                , LxCorFundo = entity1.LX_COR_FUNDO
                                , LxCorFundoName = ((entity1.LX_COR_FUNDO) == 8 ? "Fundo Laranja" : ((entity1.LX_COR_FUNDO) == 10 ? "Fundo Roxo" : ((entity1.LX_COR_FUNDO) == 7 ? "Laranja" : ((entity1.LX_COR_FUNDO) == 9 ? "Roxo" : ""))))
                                , NomeCurto = entity1.NOME_CURTO
                                , NomeTabela = "TCS_MODULO_MENU"
                                , OrdemNavegacao = entity1.ORDEM_NAVEGACAO
		
	                        }
	                        )
		
	            }
	            );
	
	        SetTcsModuloBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsModuloMenuByEntitySearch.
	    public IQueryable<TcsModuloMenu> GetTcsModuloMenuByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsModuloMenu));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsModuloMenu> result = 
	            (from entity0 in this.DbContext.TCS_MODULO_MENU.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.TCS_MODULO
                  let entity0Al1 = entity0.MODULO_MENU_SUPERIOR
	            
	            	
	            select new TcsModuloMenu()		
	            {
	            
                DescModulo = entity0Al2.DESC_MODULO
                , DescModuloMenu = entity0.DESC_MODULO_MENU
                , DescModuloMenuSuperior = entity0Al1.DESC_MODULO_MENU
                , Icone = entity0.ICONE
                , IdModulo = entity0Al2.ID_MODULO
                , IdModuloMenu = entity0.ID_MODULO_MENU
                , IdModuloMenuSuperior = entity0Al1.ID_MODULO_MENU
                , IdTcsAplicativo = entity0Al2.ID_TCS_APLICATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , LxCorFundoName = ((entity0.LX_COR_FUNDO) == 8 ? "Fundo Laranja" : ((entity0.LX_COR_FUNDO) == 10 ? "Fundo Roxo" : ((entity0.LX_COR_FUNDO) == 7 ? "Laranja" : ((entity0.LX_COR_FUNDO) == 9 ? "Roxo" : ""))))
                , NomeCurto = entity0.NOME_CURTO
                , NomeTabela = "TCS_MODULO_MENU"
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            );
	
	        SetTcsModuloMenuBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
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
                , IdTcsTransacaoMenu = entity0.ID_TCS_TRANSACAO_MENU
                , IdTransacao = entity0.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
                , DescTransacao = ""
		
	            }
	            );
	
	        SetTcsTransacaoMenuBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsModuloDoGrupoByEntitySearch.
	    public IQueryable<TcsModuloDoGrupo> GetTcsModuloDoGrupoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsModuloDoGrupo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsModuloDoGrupo> result = 
	            (from entity0 in this.DbContext.TCS_MODULO_DO_GRUPO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_MODULO_GRUPO
	            
	            	
	            select new TcsModuloDoGrupo()		
	            {
	            
                DescGrupoModulo = entity0Al1.DESC_GRUPO_MODULO
                , IdGrupoModulo = entity0Al1.ID_GRUPO_MODULO
                , IdModulo = entity0.ID_MODULO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsModuloByEntitySearchNoAssociations.
	    public IQueryable<TcsModulo> GetTcsModuloByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsModulo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsModulo> result = 
	            (from entity0 in this.DbContext.TCS_MODULO.Where(dynQuery, parameters.ToArray())
                orderby entity0.DESC_MODULO ascending
	            
	            	
	            select new TcsModulo()		
	            {
	            
                DescModulo = entity0.DESC_MODULO
                , Icone = entity0.ICONE
                , IdModulo = entity0.ID_MODULO
                , IdTcsAplicativo = entity0.ID_TCS_APLICATIVO
                , Inativo = entity0.INATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , LxCorFundoName = ((entity0.LX_COR_FUNDO) == 8 ? "Fundo Laranja" : ((entity0.LX_COR_FUNDO) == 10 ? "Fundo Roxo" : ((entity0.LX_COR_FUNDO) == 7 ? "Laranja" : ((entity0.LX_COR_FUNDO) == 9 ? "Roxo" : ""))))
                , NomeCurto = entity0.NOME_CURTO
                , NomeTabela = ""
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            );
	
	        SetTcsModuloBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsModuloMenuByEntitySearchNoAssociations.
	    public IQueryable<TcsModuloMenu> GetTcsModuloMenuByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsModuloMenu));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsModuloMenu> result = 
	            (from entity0 in this.DbContext.TCS_MODULO_MENU.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.TCS_MODULO
                  let entity0Al1 = entity0.MODULO_MENU_SUPERIOR
	            
	            	
	            select new TcsModuloMenu()		
	            {
	            
                DescModulo = entity0Al2.DESC_MODULO
                , DescModuloMenu = entity0.DESC_MODULO_MENU
                , DescModuloMenuSuperior = entity0Al1.DESC_MODULO_MENU
                , Icone = entity0.ICONE
                , IdModulo = entity0Al2.ID_MODULO
                , IdModuloMenu = entity0.ID_MODULO_MENU
                , IdModuloMenuSuperior = entity0Al1.ID_MODULO_MENU
                , IdTcsAplicativo = entity0Al2.ID_TCS_APLICATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , LxCorFundoName = ((entity0.LX_COR_FUNDO) == 8 ? "Fundo Laranja" : ((entity0.LX_COR_FUNDO) == 10 ? "Fundo Roxo" : ((entity0.LX_COR_FUNDO) == 7 ? "Laranja" : ((entity0.LX_COR_FUNDO) == 9 ? "Roxo" : ""))))
                , NomeCurto = entity0.NOME_CURTO
                , NomeTabela = "TCS_MODULO_MENU"
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            );
	
	        SetTcsModuloMenuBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
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
                , IdTcsTransacaoMenu = entity0.ID_TCS_TRANSACAO_MENU
                , IdTransacao = entity0.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
                , DescTransacao = ""
		
	            }
	            );
	
	        SetTcsTransacaoMenuBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsModuloDoGrupoByEntitySearchNoAssociations.
	    public IQueryable<TcsModuloDoGrupo> GetTcsModuloDoGrupoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsModuloDoGrupo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsModuloDoGrupo> result = 
	            (from entity0 in this.DbContext.TCS_MODULO_DO_GRUPO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_MODULO_GRUPO
	            
	            	
	            select new TcsModuloDoGrupo()		
	            {
	            
                DescGrupoModulo = entity0Al1.DESC_GRUPO_MODULO
                , IdGrupoModulo = entity0Al1.ID_GRUPO_MODULO
                , IdModulo = entity0.ID_MODULO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
	
	    [Ignore()]
	    private void SetTcsModuloBusinessFilter(ref IQueryable<TcsModulo> query, List<EntitySearch> entitySearchList)
	    {
	    		int idxElement;
	    		string operatorValue;
	    		object value;
	    		//Get query by functions
	    		if (entitySearchList.Count > 0)
	    		{
	    			foreach (EntitySearch search in entitySearchList.Where(e => e.EntityName == "TcsModulo"))
	    			{

	
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "LxCorFundo" || e.Value.ToString() == "TCS_MODULO.LX_COR_FUNDO")))
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



	    [Ignore()]
	    private void SetTcsModuloMenuBusinessFilter(ref IQueryable<TcsModuloMenu> query, List<EntitySearch> entitySearchList)
	    {
	    		int idxElement;
	    		string operatorValue;
	    		object value;
	    		//Get query by functions
	    		if (entitySearchList.Count > 0)
	    		{
	    			foreach (EntitySearch search in entitySearchList.Where(e => e.EntityName == "TcsModuloMenu"))
	    			{

	
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "DescModulo" || e.Value.ToString() == "TCS_MODULO_MENU.TCS_MODULO.DESC_MODULO")))
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

    
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "LxCorFundo" || e.Value.ToString() == "TCS_MODULO_MENU.LX_COR_FUNDO")))
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

    
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "NomeTabela" || e.Value.ToString() == "'TCS_MODULO_MENU'")))
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



	    [Ignore()]
	    private void SetTcsTransacaoMenuBusinessFilter(ref IQueryable<TcsTransacaoMenu> query, List<EntitySearch> entitySearchList)
	    {
	    		int idxElement;
	    		string operatorValue;
	    		object value;
	    		//Get query by functions
	    		if (entitySearchList.Count > 0)
	    		{
	    			foreach (EntitySearch search in entitySearchList.Where(e => e.EntityName == "TcsTransacaoMenu"))
	    			{

	
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
	    //Get TcsModuloGrupoByEntitySearch.
	    public IQueryable<TcsModuloGrupo> GetTcsModuloGrupoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsModuloGrupo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsModuloGrupo> result = 
	            (from entity0 in this.DbContext.TCS_MODULO_GRUPO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsModuloGrupo()		
	            {
	            
                DescGrupoModulo = entity0.DESC_GRUPO_MODULO
                , IdGrupoModulo = entity0.ID_GRUPO_MODULO
                , IdTcsAplicativo = entity0.ID_TCS_APLICATIVO
			
                ,TcsModuloDoGrupoDetalheList = 
	                        (from entity1 in entity0.TCS_MODULO_DO_GRUPO_LISTA
                                  let entity1Al1 = entity1.TCS_MODULO_GRUPO
	                        
	                        	
	                        select new TcsModuloDoGrupoDetalhe()
	                        {
	                        
                                IdGrupoModulo = entity1Al1.ID_GRUPO_MODULO
                                , IdModulo = entity1.ID_MODULO
                                , IdModuloDoGrupo = entity1.ID_MODULO_DO_GRUPO
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsModuloDoGrupoDetalheByEntitySearch.
	    public IQueryable<TcsModuloDoGrupoDetalhe> GetTcsModuloDoGrupoDetalheByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsModuloDoGrupoDetalhe));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsModuloDoGrupoDetalhe> result = 
	            (from entity0 in this.DbContext.TCS_MODULO_DO_GRUPO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_MODULO_GRUPO
	            
	            	
	            select new TcsModuloDoGrupoDetalhe()		
	            {
	            
                IdGrupoModulo = entity0Al1.ID_GRUPO_MODULO
                , IdModulo = entity0.ID_MODULO
                , IdModuloDoGrupo = entity0.ID_MODULO_DO_GRUPO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsModuloGrupoByEntitySearchNoAssociations.
	    public IQueryable<TcsModuloGrupo> GetTcsModuloGrupoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsModuloGrupo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsModuloGrupo> result = 
	            (from entity0 in this.DbContext.TCS_MODULO_GRUPO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsModuloGrupo()		
	            {
	            
                DescGrupoModulo = entity0.DESC_GRUPO_MODULO
                , IdGrupoModulo = entity0.ID_GRUPO_MODULO
                , IdTcsAplicativo = entity0.ID_TCS_APLICATIVO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsModuloDoGrupoDetalheByEntitySearchNoAssociations.
	    public IQueryable<TcsModuloDoGrupoDetalhe> GetTcsModuloDoGrupoDetalheByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsModuloDoGrupoDetalhe));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsModuloDoGrupoDetalhe> result = 
	            (from entity0 in this.DbContext.TCS_MODULO_DO_GRUPO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_MODULO_GRUPO
	            
	            	
	            select new TcsModuloDoGrupoDetalhe()		
	            {
	            
                IdGrupoModulo = entity0Al1.ID_GRUPO_MODULO
                , IdModulo = entity0.ID_MODULO
                , IdModuloDoGrupo = entity0.ID_MODULO_DO_GRUPO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsModuloDoGrupoDetalheParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsModuloDoGrupoDetalheParentComposition> GetTcsModuloDoGrupoDetalheParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "TCS_MODULO_GRUPO", "TCS_MODULO_DO_GRUPO", "TCS_MODULO_GRUPO", typeof(TcsModuloDoGrupoDetalheParentComposition));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsModuloDoGrupoDetalheParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_MODULO_DO_GRUPO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_MODULO_GRUPO
	            
	            	
	            select new TcsModuloDoGrupoDetalheParentComposition()		
	            {
	            
                IdGrupoModulo = entity0Al1.ID_GRUPO_MODULO
                , IdModulo = entity0.ID_MODULO
                , IdModuloDoGrupo = entity0.ID_MODULO_DO_GRUPO
                //TcsModuloGrupo Properties.
                , DescGrupoModulo = entity0.TCS_MODULO_GRUPO.DESC_GRUPO_MODULO
                , IdTcsAplicativo = entity0.TCS_MODULO_GRUPO.ID_TCS_APLICATIVO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get AppModuleByEntitySearch.
	    public IEnumerable<AppModule> GetAppModuleByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<AppModule> result = new List<AppModule>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get AppModuleByEntitySearchNoAssociations.
	    public IEnumerable<AppModule> GetAppModuleByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<AppModule> result = new List<AppModule>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get BreadCrumbItemByEntitySearch.
	    public IEnumerable<BreadCrumbItem> GetBreadCrumbItemByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<BreadCrumbItem> result = new List<BreadCrumbItem>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get BreadCrumbItemByEntitySearchNoAssociations.
	    public IEnumerable<BreadCrumbItem> GetBreadCrumbItemByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<BreadCrumbItem> result = new List<BreadCrumbItem>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get AppMenuByEntitySearch.
	    public IEnumerable<AppMenu> GetAppMenuByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<AppMenu> result = new List<AppMenu>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get AppMenuByEntitySearchNoAssociations.
	    public IEnumerable<AppMenu> GetAppMenuByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<AppMenu> result = new List<AppMenu>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get UserModulesByEntitySearch.
	    public IEnumerable<UserModules> GetUserModulesByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<UserModules> result = new List<UserModules>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get UserModulesByEntitySearchNoAssociations.
	    public IEnumerable<UserModules> GetUserModulesByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<UserModules> result = new List<UserModules>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioFavoritoByEntitySearch.
	    public IQueryable<TcsUsuarioFavorito> GetTcsUsuarioFavoritoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioFavorito));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioFavorito> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_FAVORITO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioFavorito()		
	            {
	            
                IdModulo = entity0.ID_MODULO
                , IdModuloMenu = entity0.ID_MODULO_MENU
                , IdTcsUsuarioFavorito = entity0.ID_TCS_USUARIO_FAVORITO
                , IdTransacao = entity0.ID_TRANSACAO
                , IdUsuario = entity0Al1.ID_USUARIO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioFavoritoByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioFavorito> GetTcsUsuarioFavoritoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioFavorito));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioFavorito> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_FAVORITO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioFavorito()		
	            {
	            
                IdModulo = entity0.ID_MODULO
                , IdModuloMenu = entity0.ID_MODULO_MENU
                , IdTcsUsuarioFavorito = entity0.ID_TCS_USUARIO_FAVORITO
                , IdTransacao = entity0.ID_TRANSACAO
                , IdUsuario = entity0Al1.ID_USUARIO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            );
		
	
	        	

	
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
	    //Get PagedTcsModulo.
	    public IQueryable<TcsModulo> GetPagedTcsModulo(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsModulo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsModulo> result = 
	            (from entity0 in this.DbContext.TCS_MODULO.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_MODULO ascending
	            
	            	
	            select new TcsModulo()		
	            {
	            
                DescModulo = entity0.DESC_MODULO
                , Icone = entity0.ICONE
                , IdModulo = entity0.ID_MODULO
                , IdTcsAplicativo = entity0.ID_TCS_APLICATIVO
                , Inativo = entity0.INATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , LxCorFundoName = ((entity0.LX_COR_FUNDO) == 8 ? "Fundo Laranja" : ((entity0.LX_COR_FUNDO) == 10 ? "Fundo Roxo" : ((entity0.LX_COR_FUNDO) == 7 ? "Laranja" : ((entity0.LX_COR_FUNDO) == 9 ? "Roxo" : ""))))
                , NomeCurto = entity0.NOME_CURTO
                , NomeTabela = ""
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            ).Skip(skip).Take(take);
	
	        SetTcsModuloBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsModuloMenu.
	    public IQueryable<TcsModuloMenu> GetPagedTcsModuloMenu(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsModuloMenu));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsModuloMenu> result = 
	            (from entity0 in this.DbContext.TCS_MODULO_MENU.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.TCS_MODULO
                  let entity0Al1 = entity0.MODULO_MENU_SUPERIOR
                orderby entity0.ID_MODULO_MENU ascending
	            
	            	
	            select new TcsModuloMenu()		
	            {
	            
                DescModulo = entity0Al2.DESC_MODULO
                , DescModuloMenu = entity0.DESC_MODULO_MENU
                , DescModuloMenuSuperior = entity0Al1.DESC_MODULO_MENU
                , Icone = entity0.ICONE
                , IdModulo = entity0Al2.ID_MODULO
                , IdModuloMenu = entity0.ID_MODULO_MENU
                , IdModuloMenuSuperior = entity0Al1.ID_MODULO_MENU
                , IdTcsAplicativo = entity0Al2.ID_TCS_APLICATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , LxCorFundoName = ((entity0.LX_COR_FUNDO) == 8 ? "Fundo Laranja" : ((entity0.LX_COR_FUNDO) == 10 ? "Fundo Roxo" : ((entity0.LX_COR_FUNDO) == 7 ? "Laranja" : ((entity0.LX_COR_FUNDO) == 9 ? "Roxo" : ""))))
                , NomeCurto = entity0.NOME_CURTO
                , NomeTabela = "TCS_MODULO_MENU"
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            ).Skip(skip).Take(take);
	
	        SetTcsModuloMenuBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
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
                orderby entity0.ID_TCS_TRANSACAO_MENU ascending
	            
	            	
	            select new TcsTransacaoMenu()		
	            {
	            
                IdModuloMenu = entity0.ID_MODULO_MENU
                , IdTcsTransacaoMenu = entity0.ID_TCS_TRANSACAO_MENU
                , IdTransacao = entity0.ID_TRANSACAO
                , Inativo = entity0.INATIVO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
                , DescTransacao = ""
		
	            }
	            ).Skip(skip).Take(take);
	
	        SetTcsTransacaoMenuBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsModuloDoGrupo.
	    public IQueryable<TcsModuloDoGrupo> GetPagedTcsModuloDoGrupo(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsModuloDoGrupo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsModuloDoGrupo> result = 
	            (from entity0 in this.DbContext.TCS_MODULO_DO_GRUPO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_MODULO_GRUPO
                orderby entity0.ID_MODULO ascending, entity0Al1.ID_GRUPO_MODULO ascending
	            
	            	
	            select new TcsModuloDoGrupo()		
	            {
	            
                DescGrupoModulo = entity0Al1.DESC_GRUPO_MODULO
                , IdGrupoModulo = entity0Al1.ID_GRUPO_MODULO
                , IdModulo = entity0.ID_MODULO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsModuloCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsModulo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_MODULO.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsModuloMenuCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsModuloMenu));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_MODULO_MENU.Where(dynQuery, parameters.ToArray())
                  let entityAl2 = entity.TCS_MODULO
                  let entityAl1 = entity.MODULO_MENU_SUPERIOR
	            
	            select 1
	            ).Count();	
		
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
	    public int GetTcsModuloDoGrupoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsModuloDoGrupo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_MODULO_DO_GRUPO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_MODULO_GRUPO
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsModuloGrupo.
	    public IQueryable<TcsModuloGrupo> GetPagedTcsModuloGrupo(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsModuloGrupo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsModuloGrupo> result = 
	            (from entity0 in this.DbContext.TCS_MODULO_GRUPO.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_GRUPO_MODULO ascending
	            
	            	
	            select new TcsModuloGrupo()		
	            {
	            
                DescGrupoModulo = entity0.DESC_GRUPO_MODULO
                , IdGrupoModulo = entity0.ID_GRUPO_MODULO
                , IdTcsAplicativo = entity0.ID_TCS_APLICATIVO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsModuloDoGrupoDetalhe.
	    public IQueryable<TcsModuloDoGrupoDetalhe> GetPagedTcsModuloDoGrupoDetalhe(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsModuloDoGrupoDetalhe));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsModuloDoGrupoDetalhe> result = 
	            (from entity0 in this.DbContext.TCS_MODULO_DO_GRUPO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_MODULO_GRUPO
                orderby entity0.ID_MODULO_DO_GRUPO ascending
	            
	            	
	            select new TcsModuloDoGrupoDetalhe()		
	            {
	            
                IdGrupoModulo = entity0Al1.ID_GRUPO_MODULO
                , IdModulo = entity0.ID_MODULO
                , IdModuloDoGrupo = entity0.ID_MODULO_DO_GRUPO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsModuloGrupoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsModuloGrupo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_MODULO_GRUPO.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsModuloDoGrupoDetalheCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsModuloDoGrupoDetalhe));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_MODULO_DO_GRUPO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_MODULO_GRUPO
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedAppModule.
	    public IEnumerable<AppModule> GetPagedAppModule(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<AppModule> result = new List<AppModule>();
	  	
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetAppModuleCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
			
	
	    
	    [Ignore]
	    //Get PagedBreadCrumbItem.
	    public IEnumerable<BreadCrumbItem> GetPagedBreadCrumbItem(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<BreadCrumbItem> result = new List<BreadCrumbItem>();
	  	
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetBreadCrumbItemCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
			
	
	    
	    [Ignore]
	    //Get PagedAppMenu.
	    public IEnumerable<AppMenu> GetPagedAppMenu(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<AppMenu> result = new List<AppMenu>();
	  	
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetAppMenuCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
			
	
	    
	    [Ignore]
	    //Get PagedUserModules.
	    public IEnumerable<UserModules> GetPagedUserModules(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<UserModules> result = new List<UserModules>();
	  	
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetUserModulesCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsUsuarioFavorito.
	    public IQueryable<TcsUsuarioFavorito> GetPagedTcsUsuarioFavorito(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioFavorito));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioFavorito> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_FAVORITO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_USUARIO
                orderby entity0.ID_TCS_USUARIO_FAVORITO ascending
	            
	            	
	            select new TcsUsuarioFavorito()		
	            {
	            
                IdModulo = entity0.ID_MODULO
                , IdModuloMenu = entity0.ID_MODULO_MENU
                , IdTcsUsuarioFavorito = entity0.ID_TCS_USUARIO_FAVORITO
                , IdTransacao = entity0.ID_TRANSACAO
                , IdUsuario = entity0Al1.ID_USUARIO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsUsuarioFavoritoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioFavorito));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_USUARIO_FAVORITO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_USUARIO
	            
	            select 1
	            ).Count();	
		
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
	    //Update TcsModulo.
	    public void UpdateTcsModulo(TcsModulo entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsModulo.
	    public void InsertTcsModulo(TcsModulo entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsModulo.
	    public void DeleteTcsModulo(TcsModulo entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsModuloMenu.
	    public void UpdateTcsModuloMenu(TcsModuloMenu entity)
	    {



	
	        if (entity.TcsModulo.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsModulo) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsModulo); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsModuloMenu.
	    public void InsertTcsModuloMenu(TcsModuloMenu entity)
	    {



	
	        if (entity.TcsModulo.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsModulo) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsModulo);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsModuloMenu.
	    public void DeleteTcsModuloMenu(TcsModuloMenu entity)
	    {



	
	        if (entity.TcsModulo.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsModulo) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsModulo);
	            

	
	        }

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsTransacaoMenu.
	    public void UpdateTcsTransacaoMenu(TcsTransacaoMenu entity)
	    {



	
	        if (entity.TcsModuloMenu.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsModuloMenu) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsModuloMenu); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsTransacaoMenu.
	    public void InsertTcsTransacaoMenu(TcsTransacaoMenu entity)
	    {



	
	        if (entity.TcsModuloMenu.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsModuloMenu) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsModuloMenu);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsTransacaoMenu.
	    public void DeleteTcsTransacaoMenu(TcsTransacaoMenu entity)
	    {



	
	        if (entity.TcsModuloMenu.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsModuloMenu) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsModuloMenu);
	            

	
	        }

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsModuloDoGrupo.
	    public void UpdateTcsModuloDoGrupo(TcsModuloDoGrupo entity)
	    {



	
	        if (entity.TcsModulo.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsModulo) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsModulo); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsModuloDoGrupo.
	    public void InsertTcsModuloDoGrupo(TcsModuloDoGrupo entity)
	    {



	
	        if (entity.TcsModulo.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsModulo) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsModulo);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsModuloDoGrupo.
	    public void DeleteTcsModuloDoGrupo(TcsModuloDoGrupo entity)
	    {



	
	        if (entity.TcsModulo.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsModulo) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsModulo);
	            

	
	        }

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsModuloGrupo.
	    public void UpdateTcsModuloGrupo(TcsModuloGrupo entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsModuloGrupo.
	    public void InsertTcsModuloGrupo(TcsModuloGrupo entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsModuloGrupo.
	    public void DeleteTcsModuloGrupo(TcsModuloGrupo entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsModuloDoGrupoDetalhe.
	    public void UpdateTcsModuloDoGrupoDetalhe(TcsModuloDoGrupoDetalhe entity)
	    {



	
	        if (entity.TcsModuloGrupo.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsModuloGrupo) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsModuloGrupo); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsModuloDoGrupoDetalhe.
	    public void InsertTcsModuloDoGrupoDetalhe(TcsModuloDoGrupoDetalhe entity)
	    {



	
	        if (entity.TcsModuloGrupo.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsModuloGrupo) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsModuloGrupo);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsModuloDoGrupoDetalhe.
	    public void DeleteTcsModuloDoGrupoDetalhe(TcsModuloDoGrupoDetalhe entity)
	    {



	
	        if (entity.TcsModuloGrupo.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsModuloGrupo) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsModuloGrupo);
	            

	
	        }

	
	    }
		
			
	    
	    [Update()]	
	    //Update AppModule.
	    public void UpdateAppModule(AppModule entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert AppModule.
	    public void InsertAppModule(AppModule entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete AppModule.
	    public void DeleteAppModule(AppModule entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update BreadCrumbItem.
	    public void UpdateBreadCrumbItem(BreadCrumbItem entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert BreadCrumbItem.
	    public void InsertBreadCrumbItem(BreadCrumbItem entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete BreadCrumbItem.
	    public void DeleteBreadCrumbItem(BreadCrumbItem entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update AppMenu.
	    public void UpdateAppMenu(AppMenu entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert AppMenu.
	    public void InsertAppMenu(AppMenu entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete AppMenu.
	    public void DeleteAppMenu(AppMenu entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update UserModules.
	    public void UpdateUserModules(UserModules entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert UserModules.
	    public void InsertUserModules(UserModules entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete UserModules.
	    public void DeleteUserModules(UserModules entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsUsuarioFavorito.
	    public void UpdateTcsUsuarioFavorito(TcsUsuarioFavorito entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsUsuarioFavorito.
	    public void InsertTcsUsuarioFavorito(TcsUsuarioFavorito entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsUsuarioFavorito.
	    public void DeleteTcsUsuarioFavorito(TcsUsuarioFavorito entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
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