					
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

namespace Linx.Framework.BV.UsuarioAutorizacao
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_USUARIO_AUTENTICACAO.ID_USUARIO", IsUpdatable=true, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsUsuarioAutenticacao,TcsUsuarioAutenticacao.TcsUsuarioAcesso,TcsUsuarioAutenticacao.TcsIdentidadeExterna,TcsUsuarioAutenticacao.TcsUsuarioGpecon];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdUsuario];ReadOnly[false];Entities[TCS_USUARIO_AUTENTICACAO:IdUsuario|TCS_EMPRESA_AUTENTICACAO:IdLinx];SubQueryInfo[];EdmEntityName[TCS_USUARIO_AUTENTICACAO];EntityRelations[TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioAutenticacao")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacao")]
	public partial class TcsUsuarioAutenticacao : Linx.Data.Entity
	{

	

	    public TcsUsuarioAutenticacao() : this(true) { }

	    public TcsUsuarioAutenticacao(bool setDefaults) 
	    {

	        if (setDefaults)
	        {
	        	        DataAlteracao = DateTime.Now;
	        	        DataCadastro = DateTime.Now;
	        	        DataExpiracaoSenha = DateTime.Now;
	        	        VigenciaFinal = new DateTime(2099, 12, 31);
	        	        VigenciaInicial = DateTime.Now;
	        }	

	    }

			
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.TcsUsuarioAcessoList != null && this.TcsUsuarioAcessoList.Count() > 0)
	      {
	         foreach (var entity in this.TcsUsuarioAcessoList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      if (this.TcsIdentidadeExternaList != null && this.TcsIdentidadeExternaList.Count() > 0)
	      {
	         foreach (var entity in this.TcsIdentidadeExternaList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      if (this.TcsUsuarioGpeconList != null && this.TcsUsuarioGpeconList.Count() > 0)
	      {
	         foreach (var entity in this.TcsUsuarioGpeconList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.TcsUsuarioAcessoList != null)
	      {
	         foreach (var detail in this.TcsUsuarioAcessoList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsUsuarioAcessoList = null;
	      }
	      if (this.TcsIdentidadeExternaList != null)
	      {
	         foreach (var detail in this.TcsIdentidadeExternaList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsIdentidadeExternaList = null;
	      }
	      if (this.TcsUsuarioGpeconList != null)
	      {
	         foreach (var detail in this.TcsUsuarioGpeconList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsUsuarioGpeconList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(UsuarioAutorizacaoDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("TcsUsuarioAcesso"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsUsuarioAcesso");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuario"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdUsuario));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsUsuarioAcesso and all sub-details
	         if (this.TcsUsuarioAcessoList == null || this.TcsUsuarioAcessoList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsUsuarioAcessoList = context.GetPagedTcsUsuarioAcesso(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsUsuarioAcessoList = (from r in context.GetTcsUsuarioAcessoByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	      if (viewNames == null || viewNames.Contains("TcsIdentidadeExterna"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsIdentidadeExterna");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuario"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdUsuario));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsIdentidadeExterna and all sub-details
	         if (this.TcsIdentidadeExternaList == null || this.TcsIdentidadeExternaList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsIdentidadeExternaList = context.GetPagedTcsIdentidadeExterna(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsIdentidadeExternaList = (from r in context.GetTcsIdentidadeExternaByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	      if (viewNames == null || viewNames.Contains("TcsUsuarioGpecon"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsUsuarioGpecon");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuario"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdUsuario));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsUsuarioGpecon and all sub-details
	         if (this.TcsUsuarioGpeconList == null || this.TcsUsuarioGpeconList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsUsuarioGpeconList = context.GetPagedTcsUsuarioGpecon(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsUsuarioGpeconList = (from r in context.GetTcsUsuarioGpeconByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _TcsUsuarioAcessoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioAcesso && ((TcsUsuarioAcesso)e.Entity).TcsUsuarioAutenticacao == null && e.Associations == null && e.OriginalAssociations == null && ((TcsUsuarioAcesso)e.Entity).IdUsuario == this.IdUsuario).ToList();
 	      if (_TcsUsuarioAcessoElements.Count > 0 && this.TcsUsuarioAcessoList.Count() == 0)
 	      {
 	          this.TcsUsuarioAcessoList = _TcsUsuarioAcessoElements.Select(e => (TcsUsuarioAcesso)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsUsuarioAcessoElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsUsuarioAcesso)detail.Entity).TcsUsuarioAutenticacao = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsUsuarioAutenticacao", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsUsuarioAcessoList", indexDetails.ToArray());
 	      }
 
 	      var _TcsIdentidadeExternaElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsIdentidadeExterna && ((TcsIdentidadeExterna)e.Entity).TcsUsuarioAutenticacao == null && e.Associations == null && e.OriginalAssociations == null && ((TcsIdentidadeExterna)e.Entity).IdUsuario == this.IdUsuario).ToList();
 	      if (_TcsIdentidadeExternaElements.Count > 0 && this.TcsIdentidadeExternaList.Count() == 0)
 	      {
 	          this.TcsIdentidadeExternaList = _TcsIdentidadeExternaElements.Select(e => (TcsIdentidadeExterna)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsIdentidadeExternaElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsIdentidadeExterna)detail.Entity).TcsUsuarioAutenticacao = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsUsuarioAutenticacao", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsIdentidadeExternaList", indexDetails.ToArray());
 	      }
 
 	      var _TcsUsuarioGpeconElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioGpecon && ((TcsUsuarioGpecon)e.Entity).TcsUsuarioAutenticacao == null && e.Associations == null && e.OriginalAssociations == null && ((TcsUsuarioGpecon)e.Entity).IdUsuario == this.IdUsuario).ToList();
 	      if (_TcsUsuarioGpeconElements.Count > 0 && this.TcsUsuarioGpeconList.Count() == 0)
 	      {
 	          this.TcsUsuarioGpeconList = _TcsUsuarioGpeconElements.Select(e => (TcsUsuarioGpecon)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsUsuarioGpeconElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsUsuarioGpecon)detail.Entity).TcsUsuarioAutenticacao = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsUsuarioAutenticacao", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsUsuarioGpeconList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For AutenticacaoWindows
	    partial void OnAutenticacaoWindowsChanging(Boolean value);
	    partial void OnAutenticacaoWindowsChanged();

	    private Boolean _AutenticacaoWindows;

	    [DataMember(IsRequired = true, Name = "AutenticacaoWindows", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Utiliza Autenticação Windows", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.AUTENTICACAO_WINDOWS];IsMeasure[false]")]
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
	    [Display(Name = "Bairro", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    [Display(Name = "CEP", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    [Display(Name = "CPF/CNPJ", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[###.###.###-##];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.CNPJ_CPF];IsMeasure[false]")]
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
	    [Display(Name = "Complemento", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    //Extensibility Partial Method Definitions For ConfirmacaoUsuario
	    partial void OnConfirmacaoUsuarioChanging(string value);
	    partial void OnConfirmacaoUsuarioChanged();

	    private string _ConfirmacaoUsuario;

	    [DataMember(Name = "ConfirmacaoUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Senha", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[''];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="\"\"")]
	    public string ConfirmacaoUsuario
	    {
	    	    get
	    	    {
	    	          return _ConfirmacaoUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._ConfirmacaoUsuario != value)
	    	          {
	    	              this.ValidateProperty("ConfirmacaoUsuario", value);
	    	              this.OnConfirmacaoUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("ConfirmacaoUsuario");
	    	              this._ConfirmacaoUsuario = value;
	    	              this.RaiseDataMemberChanged("ConfirmacaoUsuario");
	    	              this.OnConfirmacaoUsuarioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ConfirmacaoUsuario1
	    partial void OnConfirmacaoUsuario1Changing(string value);
	    partial void OnConfirmacaoUsuario1Changed();

	    private string _ConfirmacaoUsuario1;

	    [DataMember(Name = "ConfirmacaoUsuario1", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Confirmação", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[''];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="\"\"")]
	    public string ConfirmacaoUsuario1
	    {
	    	    get
	    	    {
	    	          return _ConfirmacaoUsuario1;
	    	    }
	    	    set
	    	    {
	    	          if (this._ConfirmacaoUsuario1 != value)
	    	          {
	    	              this.ValidateProperty("ConfirmacaoUsuario1", value);
	    	              this.OnConfirmacaoUsuario1Changing(value);
	    	              this.RaiseDataMemberChanging("ConfirmacaoUsuario1");
	    	              this._ConfirmacaoUsuario1 = value;
	    	              this.RaiseDataMemberChanged("ConfirmacaoUsuario1");
	    	              this.OnConfirmacaoUsuario1Changed();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For CriaUsuario
	    partial void OnCriaUsuarioChanging(bool value);
	    partial void OnCriaUsuarioChanged();

	    private bool _CriaUsuario;

	    [DataMember(IsRequired = true, Name = "CriaUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[false];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="false")]
	    public bool CriaUsuario
	    {
	    	    get
	    	    {
	    	          return _CriaUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._CriaUsuario != value)
	    	          {
	    	              this.ValidateProperty("CriaUsuario", value);
	    	              this.OnCriaUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("CriaUsuario");
	    	              this._CriaUsuario = value;
	    	              this.RaiseDataMemberChanged("CriaUsuario");
	    	              this.OnCriaUsuarioChanged();
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
	    [Display(Name = "Alteração", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[DateTime.Now];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.DATA_ALTERACAO];IsMeasure[false]")]
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
	    [Display(Name = "Cadastro", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[DateTime.Now];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.DATA_CADASTRO];IsMeasure[false]")]
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
	    [Display(Name = "Expiração Senha", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[DateTime.Now];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.DATA_EXPIRACAO_SENHA];IsMeasure[false]")]
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
	    [Display(Name = "Email", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    [Display(Name = "Móvel", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    [Display(Name = "Fixo / Ramal", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    //Extensibility Partial Method Definitions For GeraSenhaUsuario
	    partial void OnGeraSenhaUsuarioChanging(bool value);
	    partial void OnGeraSenhaUsuarioChanged();

	    private bool _GeraSenhaUsuario;

	    [DataMember(IsRequired = true, Name = "GeraSenhaUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[[GERA_SENHA_USUARIO]];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[false];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="false")]
	    public bool GeraSenhaUsuario
	    {
	    	    get
	    	    {
	    	          return _GeraSenhaUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._GeraSenhaUsuario != value)
	    	          {
	    	              this.ValidateProperty("GeraSenhaUsuario", value);
	    	              this.OnGeraSenhaUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("GeraSenhaUsuario");
	    	              this._GeraSenhaUsuario = value;
	    	              this.RaiseDataMemberChanged("GeraSenhaUsuario");
	    	              this.OnGeraSenhaUsuarioChanged();
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
	    [Display(Name = "Id Linx Empresa / Grupo Econômico", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioEmpresaAutenticacao];LookUpTitle[Seleção de (Id Linx Empresa / Grupo Econômico)];LookUpQuery[executeLookUpTcsUsuarioEmpresaAutenticacao];LookUpFinalize[finalizeLookUpTcsUsuarioEmpresaAutenticacao];LookUpDisplayColumns[{\"IdLinx\" : \"Grupo Econômico\", \"NomeEmpresa\" : \"Empresa / Grupo Econômico\", \"UidEmpresa\" : \"Uid Empresa\"}];LookUpColumns[{\"IdLinx\" : true, \"NomeEmpresa\" : true, \"UidEmpresa\" : true}];FilterDataKey[TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdLinx#true##12:0##Grupo Econômico#0#true##::LookUpTcsUsuarioEmpresaAutenticacao##false#false#TCS_EMPRESA_AUTENTICACAO#TCS_EMPRESA_AUTENTICACAO#Linx.Framework.BV.UsuarioAutorizacao#IQueryable###true#false", EdmKey="TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
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
	    [Display(Name = "Inativo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    [Display(Name = "Permite Acesso de Suporte", Description="", Order = 14, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    [Display(Name = "Inscr. Estadual / RG", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    [Display(Name = "Logradouro", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    [Display(Name = "Pessoa Física / Juridíca", Description="", Order = 14, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[LX_PFJ_FISICA_JURIDICA];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.LX_PFJ_FISICA_JURIDICA];IsMeasure[false]")]
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
	    [Display(Name = "Tipo Logradouro", Description="", Order = 15, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[LxTipoLogradouro];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO];IsMeasure[false]")]
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
	    [Display(Name = "Município", Description="", Order = 16, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    [Display(Name = "Usuário Autenticação", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=true, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO")]
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
	    [Display(Name = "Apelido", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    //Extensibility Partial Method Definitions For NomeEmpresa
	    partial void OnNomeEmpresaChanging(System.String value);
	    partial void OnNomeEmpresaChanged();

	    private System.String _NomeEmpresa;

	    [DataMember(IsRequired = true, Name = "NomeEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Empresa / Grupo Econômico", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioEmpresaAutenticacao];LookUpTitle[Seleção de (Empresa / Grupo Econômico)];LookUpQuery[executeLookUpTcsUsuarioEmpresaAutenticacao];LookUpFinalize[finalizeLookUpTcsUsuarioEmpresaAutenticacao];LookUpDisplayColumns[{\"IdLinx\" : \"Grupo Econômico\", \"NomeEmpresa\" : \"Empresa / Grupo Econômico\", \"UidEmpresa\" : \"Uid Empresa\"}];LookUpColumns[{\"IdLinx\" : true, \"NomeEmpresa\" : true, \"UidEmpresa\" : true}];FilterDataKey[TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeEmpresa#false##250:0##Empresa / Grupo Econômico#1#true##::LookUpTcsUsuarioEmpresaAutenticacao##false#false#TCS_EMPRESA_AUTENTICACAO#TCS_EMPRESA_AUTENTICACAO#Linx.Framework.BV.UsuarioAutorizacao#IQueryable###true#false", EdmKey="TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
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
	    //Extensibility Partial Method Definitions For NomeUsuario
	    partial void OnNomeUsuarioChanging(System.String value);
	    partial void OnNomeUsuarioChanged();

	    private System.String _NomeUsuario;

	    [DataMember(IsRequired = true, Name = "NomeUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=true, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.NOME_USUARIO")]
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
	    [Display(Name = "Número", Description="", Order = 20, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[Logradouro];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.NUMERO];IsMeasure[false]")]
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
	    [Display(Name = "Obs. Endereço", Description="", Order = 21, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    [Display(Name = "Ramal", Description="", Order = 22, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(6)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[FoneFixo];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.RAMAL];IsMeasure[false]")]
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
	    [Display(Name = "UF", Description="", Order = 23, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(4)]
	    [FunctionalPoint("Precision[4:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[Municipio];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.UF];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For UidEmpresa
	    partial void OnUidEmpresaChanging(System.Guid value);
	    partial void OnUidEmpresaChanged();

	    private System.Guid _UidEmpresa;

	    [DataMember(IsRequired = true, Name = "UidEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Empresa", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioEmpresaAutenticacao];LookUpTitle[Seleção de (Uid Empresa)];LookUpQuery[executeLookUpTcsUsuarioEmpresaAutenticacao];LookUpFinalize[finalizeLookUpTcsUsuarioEmpresaAutenticacao];LookUpDisplayColumns[{\"IdLinx\" : \"Grupo Econômico\", \"NomeEmpresa\" : \"Empresa / Grupo Econômico\", \"UidEmpresa\" : \"Uid Empresa\"}];LookUpColumns[{\"IdLinx\" : true, \"NomeEmpresa\" : true, \"UidEmpresa\" : true}];FilterDataKey[TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Guid#UidEmpresa#false##36:0##Uid Empresa#2#true##::LookUpTcsUsuarioEmpresaAutenticacao##false#false#TCS_EMPRESA_AUTENTICACAO#TCS_EMPRESA_AUTENTICACAO#Linx.Framework.BV.UsuarioAutorizacao#IQueryable###true#false", EdmKey="TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA")]
	    public System.Guid UidEmpresa
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
	    partial void OnUidUsuarioChanging(System.Guid value);
	    partial void OnUidUsuarioChanged();

	    private System.Guid _UidUsuario;

	    [DataMember(Name = "UidUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Usuario", Description="", Order = 26, AutoGenerateField = false, GroupName="", ResourceType= null)]
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
	    [Display(Name = "Vigência Final", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[new DateTime(2099, 12, 31)];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.VIGENCIA_FINAL];IsMeasure[false]")]
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
	    [Display(Name = "Vigência Inicial", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[DateTime.Now];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.VIGENCIA_INICIAL];IsMeasure[false]")]
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

	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<TcsIdentidadeExterna> _TcsIdentidadeExternaList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsUsuarioAutenticacao_TcsIdentidadeExterna", "IdUsuario", "IdUsuario", IsForeignKey=false)]
	    [DataMember(Name = "TcsIdentidadeExternaList", EmitDefaultValue = true)]
	    public IEnumerable<TcsIdentidadeExterna> TcsIdentidadeExternaList
	    {
	        get
	        {
	
	            if (this._TcsIdentidadeExternaList == null)
	            	this._TcsIdentidadeExternaList = new List<TcsIdentidadeExterna>();
	
	            return this._TcsIdentidadeExternaList;
	        }
	        set
	        {
	            if (this._TcsIdentidadeExternaList != value)
	            {
	                this._TcsIdentidadeExternaList = value;
	                this.RaisePropertyChanged("TcsIdentidadeExternaList");
	            }
	        }
	    }	 
		
	    private IEnumerable<TcsUsuarioAcesso> _TcsUsuarioAcessoList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsUsuarioAutenticacao_TcsUsuarioAcesso", "IdUsuario", "IdUsuario", IsForeignKey=false)]
	    [DataMember(Name = "TcsUsuarioAcessoList", EmitDefaultValue = true)]
	    public IEnumerable<TcsUsuarioAcesso> TcsUsuarioAcessoList
	    {
	        get
	        {
	
	            if (this._TcsUsuarioAcessoList == null)
	            	this._TcsUsuarioAcessoList = new List<TcsUsuarioAcesso>();
	
	            return this._TcsUsuarioAcessoList;
	        }
	        set
	        {
	            if (this._TcsUsuarioAcessoList != value)
	            {
	                this._TcsUsuarioAcessoList = value;
	                this.RaisePropertyChanged("TcsUsuarioAcessoList");
	            }
	        }
	    }	 
		
	    private IEnumerable<TcsUsuarioGpecon> _TcsUsuarioGpeconList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsUsuarioAutenticacao_TcsUsuarioGpecon", "IdUsuario", "IdUsuario", IsForeignKey=false)]
	    [DataMember(Name = "TcsUsuarioGpeconList", EmitDefaultValue = true)]
	    public IEnumerable<TcsUsuarioGpecon> TcsUsuarioGpeconList
	    {
	        get
	        {
	
	            if (this._TcsUsuarioGpeconList == null)
	            	this._TcsUsuarioGpeconList = new List<TcsUsuarioGpecon>();
	
	            return this._TcsUsuarioGpeconList;
	        }
	        set
	        {
	            if (this._TcsUsuarioGpeconList != value)
	            {
	                this._TcsUsuarioGpeconList = value;
	                this.RaisePropertyChanged("TcsUsuarioGpeconList");
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
	    [Display(Name = "Pessoa Física / Juridíca", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    [Display(Name = "Tipo Logradouro", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
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

		

	[LinxPublicationView(PrimaryKeys="TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO", IsUpdatable=true, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Acessos];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsUsuarioAcesso];ReadOnly[false];Entities[TCS_USUARIO_ACESSO:IdTcsUsuarioAcesso|TCS_AMBIENTE:IdTcsAmbiente|TCS_AMBIENTE:IdTcsAmbienteRelacionado];SubQueryInfo[Select 1 From #ParentAlias#.TCS_USUARIO_ACESSO_LISTA as #Alias#];EdmEntityName[TCS_USUARIO_ACESSO];EntityRelations[TCS_USUARIO_AUTENTICACAO(TCS_USUARIO_AUTENTICACAO)#TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)#TCS_AMBIENTE(TCS_AMBIENTE)#TCS_APLICACAO(TCS_APLICACAO)#TCS_APLICATIVO(TCS_APLICATIVO)#TCS_AMBIENTE1(TCS_AMBIENTE)];EdmParentEntityName[TCS_USUARIO_AUTENTICACAO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioAcesso")]
	[Serializable()]
	public partial class TcsUsuarioAcesso : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(UsuarioAutorizacaoDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsUsuarioAutenticacao");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuario"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdUsuario));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsUsuarioAutenticacao
	         this.TcsUsuarioAutenticacao = (from r in context.GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	    partial void OnDescricaoAmbienteChanging(System.String value);
	    partial void OnDescricaoAmbienteChanged();

	    private System.String _DescricaoAmbiente;

	    [DataMember(IsRequired = true, Name = "DescricaoAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ambiente", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente];LookUpTitle[Seleção de (Ambiente)];LookUpQuery[executeLookUpTcsAmbiente];LookUpFinalize[finalizeLookUpTcsAmbiente];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"DescricaoAplicacao\" : \"Aplicação\", \"EmDesenvolvimento\" : \"Em Desenvolvimento\", \"IdLinxEmpresa\" : \"Id Linx\", \"DescricaoAplicativo\" : \"Aplicativo\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\", \"NomeEmpresa\" : \"Grupo Econômico\", \"UidAplicacao\" : \"Uid Aplicacao\", \"UidEmpresa\" : \"Uid Empresa\", \"Url\" : \"Url\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"DescricaoAplicacao\" : true, \"EmDesenvolvimento\" : true, \"IdLinxEmpresa\" : false, \"DescricaoAplicativo\" : true, \"IdTcsAmbiente\" : false, \"NomeEmpresa\" : true, \"UidAplicacao\" : false, \"UidEmpresa\" : false, \"Url\" : false, \"IdTcsAplicativo\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.DESCRICAO_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoAmbiente#false##2500##Ambiente#0#true##::LookUpTcsAmbiente##true#false#TCS_AMBIENTE#TCS_AMBIENTE#Linx.Framework.BV.UsuarioAutorizacao#IQueryable#DescricaoAplicacao,EmDesenvolvimento,UidAplicacao,Url[DescricaoAplicacao,EmDesenvolvimento,DescricaoAplicativo,UidAplicacao,Url,IdTcsAplicativo];DescricaoAplicativo,IdTcsAplicativo[DescricaoAplicativo,IdTcsAplicativo]#DescricaoAmbiente[DescricaoAplicacao=DescricaoAplicacao,EmDesenvolvimento=EmDesenvolvimento,IdLinxEmpresa=IdLinxEmpresa,DescricaoAplicativo=DescricaoAplicativo,NomeEmpresa=NomeEmpresa,UidAplicacao=UidAplicacao,UidEmpresa=UidEmpresa,Url=Url,IdTcsAplicativo=IdTcsAplicativo];DescricaoAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];EmDesenvolvimento[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdLinxEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdTcsAmbiente[DescricaoAplicacao=DescricaoAplicacao,EmDesenvolvimento=EmDesenvolvimento,IdLinxEmpresa=IdLinxEmpresa,DescricaoAplicativo=DescricaoAplicativo,NomeEmpresa=NomeEmpresa,UidAplicacao=UidAplicacao,UidEmpresa=UidEmpresa,Url=Url,IdTcsAplicativo=IdTcsAplicativo];NomeEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UidAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UidEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];Url[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo]#true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.DESCRICAO_AMBIENTE")]
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
	    //Extensibility Partial Method Definitions For DescricaoAmbienteRelacionado
	    partial void OnDescricaoAmbienteRelacionadoChanging(System.String value);
	    partial void OnDescricaoAmbienteRelacionadoChanged();

	    private System.String _DescricaoAmbienteRelacionado;

	    [DataMember(Name = "DescricaoAmbienteRelacionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ambiente Relacionado", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente1];LookUpTitle[Seleção de (Ambiente Relacionado)];LookUpQuery[executeLookUpTcsAmbiente1];LookUpFinalize[finalizeLookUpTcsAmbiente1];LookUpDisplayColumns[{\"DescricaoAmbienteRelacionado\" : \"Ambiente\", \"NomeEmpresaAmbienteRelacionado\" : \"Empresa\", \"DescricaoAplicacaoAmbienteRelacionado\" : \"Aplicação\", \"IdLinxAmbienteRelacionado\" : \"ID Linx\", \"IdTcsAmbienteRelacionado\" : \"Id Tcs Ambiente1\", \"IdAplicacao\" : \"Id Aplicacao\"}];LookUpColumns[{\"DescricaoAmbienteRelacionado\" : true, \"NomeEmpresaAmbienteRelacionado\" : true, \"DescricaoAplicacaoAmbienteRelacionado\" : true, \"IdLinxAmbienteRelacionado\" : false, \"IdTcsAmbienteRelacionado\" : false, \"IdAplicacao\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE1.DESCRICAO_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoAmbienteRelacionado#false##250:0##Ambiente#0#true##::LookUpTcsAmbiente1##false#false#TCS_AMBIENTE1#TCS_AMBIENTE#Linx.Framework.BV.UsuarioAutorizacao#IQueryable#NomeEmpresaAmbienteRelacionado,IdLinxAmbienteRelacionado[NomeEmpresaAmbienteRelacionado,IdLinxAmbienteRelacionado]#DescricaoAmbienteRelacionado[NomeEmpresaAmbienteRelacionado=NomeEmpresaAmbienteRelacionado,DescricaoAplicacaoAmbienteRelacionado=DescricaoAplicacaoAmbienteRelacionado,IdLinxAmbienteRelacionado=IdLinxAmbienteRelacionado,IdAplicacao=IdAplicacao];IdTcsAmbienteRelacionado[NomeEmpresaAmbienteRelacionado=NomeEmpresaAmbienteRelacionado,DescricaoAplicacaoAmbienteRelacionado=DescricaoAplicacaoAmbienteRelacionado,IdLinxAmbienteRelacionado=IdLinxAmbienteRelacionado,IdAplicacao=IdAplicacao]#true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.DESCRICAO_AMBIENTE")]
	    public System.String DescricaoAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _DescricaoAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescricaoAmbienteRelacionado != value)
	    	          {
	    	              this.ValidateProperty("DescricaoAmbienteRelacionado", value);
	    	              this.OnDescricaoAmbienteRelacionadoChanging(value);
	    	              this.RaiseDataMemberChanging("DescricaoAmbienteRelacionado");
	    	              this._DescricaoAmbienteRelacionado = value;
	    	              this.RaiseDataMemberChanged("DescricaoAmbienteRelacionado");
	    	              this.OnDescricaoAmbienteRelacionadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescricaoAplicacao
	    partial void OnDescricaoAplicacaoChanging(System.String value);
	    partial void OnDescricaoAplicacaoChanged();

	    private System.String _DescricaoAplicacao;

	    [DataMember(IsRequired = true, Name = "DescricaoAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicação", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente];LookUpTitle[Seleção de (Aplicação)];LookUpQuery[executeLookUpTcsAmbiente];LookUpFinalize[finalizeLookUpTcsAmbiente];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"DescricaoAplicacao\" : \"Aplicação\", \"EmDesenvolvimento\" : \"Em Desenvolvimento\", \"IdLinxEmpresa\" : \"Id Linx\", \"DescricaoAplicativo\" : \"Aplicativo\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\", \"NomeEmpresa\" : \"Grupo Econômico\", \"UidAplicacao\" : \"Uid Aplicacao\", \"UidEmpresa\" : \"Uid Empresa\", \"Url\" : \"Url\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"DescricaoAplicacao\" : true, \"EmDesenvolvimento\" : true, \"IdLinxEmpresa\" : false, \"DescricaoAplicativo\" : true, \"IdTcsAmbiente\" : false, \"NomeEmpresa\" : true, \"UidAplicacao\" : false, \"UidEmpresa\" : false, \"Url\" : false, \"IdTcsAplicativo\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.DESCRICAO_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoAplicacao#false##600##Aplicação#1#true##::LookUpTcsAmbiente##true#false#TCS_AMBIENTE#TCS_AMBIENTE#Linx.Framework.BV.UsuarioAutorizacao#IQueryable#DescricaoAplicacao,EmDesenvolvimento,UidAplicacao,Url[DescricaoAplicacao,EmDesenvolvimento,DescricaoAplicativo,UidAplicacao,Url,IdTcsAplicativo];DescricaoAplicativo,IdTcsAplicativo[DescricaoAplicativo,IdTcsAplicativo]#DescricaoAmbiente[DescricaoAplicacao=DescricaoAplicacao,EmDesenvolvimento=EmDesenvolvimento,IdLinxEmpresa=IdLinxEmpresa,DescricaoAplicativo=DescricaoAplicativo,NomeEmpresa=NomeEmpresa,UidAplicacao=UidAplicacao,UidEmpresa=UidEmpresa,Url=Url,IdTcsAplicativo=IdTcsAplicativo];DescricaoAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];EmDesenvolvimento[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdLinxEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdTcsAmbiente[DescricaoAplicacao=DescricaoAplicacao,EmDesenvolvimento=EmDesenvolvimento,IdLinxEmpresa=IdLinxEmpresa,DescricaoAplicativo=DescricaoAplicativo,NomeEmpresa=NomeEmpresa,UidAplicacao=UidAplicacao,UidEmpresa=UidEmpresa,Url=Url,IdTcsAplicativo=IdTcsAplicativo];NomeEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UidAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UidEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];Url[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo]#true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.DESCRICAO_APLICACAO")]
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
	    //Extensibility Partial Method Definitions For DescricaoAplicacaoAmbienteRelacionado
	    partial void OnDescricaoAplicacaoAmbienteRelacionadoChanging(System.String value);
	    partial void OnDescricaoAplicacaoAmbienteRelacionadoChanged();

	    private System.String _DescricaoAplicacaoAmbienteRelacionado;

	    [DataMember(Name = "DescricaoAplicacaoAmbienteRelacionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicação Ambiente Relacionado", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente1];LookUpTitle[Seleção de (Aplicação Ambiente Relacionado)];LookUpQuery[executeLookUpTcsAmbiente1];LookUpFinalize[finalizeLookUpTcsAmbiente1];LookUpDisplayColumns[{\"DescricaoAmbienteRelacionado\" : \"Ambiente\", \"NomeEmpresaAmbienteRelacionado\" : \"Empresa\", \"DescricaoAplicacaoAmbienteRelacionado\" : \"Aplicação\", \"IdLinxAmbienteRelacionado\" : \"ID Linx\", \"IdTcsAmbienteRelacionado\" : \"Id Tcs Ambiente1\", \"IdAplicacao\" : \"Id Aplicacao\"}];LookUpColumns[{\"DescricaoAmbienteRelacionado\" : true, \"NomeEmpresaAmbienteRelacionado\" : true, \"DescricaoAplicacaoAmbienteRelacionado\" : true, \"IdLinxAmbienteRelacionado\" : false, \"IdTcsAmbienteRelacionado\" : false, \"IdAplicacao\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE1.TCS_APLICACAO.DESCRICAO_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoAplicacaoAmbienteRelacionado#false##60:0##Aplicação#2#true##::LookUpTcsAmbiente1##false#false#TCS_AMBIENTE1#TCS_AMBIENTE#Linx.Framework.BV.UsuarioAutorizacao#IQueryable#NomeEmpresaAmbienteRelacionado,IdLinxAmbienteRelacionado[NomeEmpresaAmbienteRelacionado,IdLinxAmbienteRelacionado]#DescricaoAmbienteRelacionado[NomeEmpresaAmbienteRelacionado=NomeEmpresaAmbienteRelacionado,DescricaoAplicacaoAmbienteRelacionado=DescricaoAplicacaoAmbienteRelacionado,IdLinxAmbienteRelacionado=IdLinxAmbienteRelacionado,IdAplicacao=IdAplicacao];IdTcsAmbienteRelacionado[NomeEmpresaAmbienteRelacionado=NomeEmpresaAmbienteRelacionado,DescricaoAplicacaoAmbienteRelacionado=DescricaoAplicacaoAmbienteRelacionado,IdLinxAmbienteRelacionado=IdLinxAmbienteRelacionado,IdAplicacao=IdAplicacao]#true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.TCS_APLICACAO.DESCRICAO_APLICACAO")]
	    public System.String DescricaoAplicacaoAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _DescricaoAplicacaoAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescricaoAplicacaoAmbienteRelacionado != value)
	    	          {
	    	              this.ValidateProperty("DescricaoAplicacaoAmbienteRelacionado", value);
	    	              this.OnDescricaoAplicacaoAmbienteRelacionadoChanging(value);
	    	              this.RaiseDataMemberChanging("DescricaoAplicacaoAmbienteRelacionado");
	    	              this._DescricaoAplicacaoAmbienteRelacionado = value;
	    	              this.RaiseDataMemberChanged("DescricaoAplicacaoAmbienteRelacionado");
	    	              this.OnDescricaoAplicacaoAmbienteRelacionadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For EmDesenvolvimento
	    partial void OnEmDesenvolvimentoChanging(Boolean value);
	    partial void OnEmDesenvolvimentoChanged();

	    private Boolean _EmDesenvolvimento;

	    [DataMember(IsRequired = true, Name = "EmDesenvolvimento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Em Desenvolvimento", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente];LookUpTitle[Seleção de (Em Desenvolvimento)];LookUpQuery[executeLookUpTcsAmbiente];LookUpFinalize[finalizeLookUpTcsAmbiente];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"DescricaoAplicacao\" : \"Aplicação\", \"EmDesenvolvimento\" : \"Em Desenvolvimento\", \"IdLinxEmpresa\" : \"Id Linx\", \"DescricaoAplicativo\" : \"Aplicativo\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\", \"NomeEmpresa\" : \"Grupo Econômico\", \"UidAplicacao\" : \"Uid Aplicacao\", \"UidEmpresa\" : \"Uid Empresa\", \"Url\" : \"Url\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"DescricaoAplicacao\" : true, \"EmDesenvolvimento\" : true, \"IdLinxEmpresa\" : false, \"DescricaoAplicativo\" : true, \"IdTcsAmbiente\" : false, \"NomeEmpresa\" : true, \"UidAplicacao\" : false, \"UidEmpresa\" : false, \"Url\" : false, \"IdTcsAplicativo\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.EM_DESENVOLVIMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Boolean#EmDesenvolvimento#false##0##Em Desenvolvimento#2#true##::LookUpTcsAmbiente##true#false#TCS_AMBIENTE#TCS_AMBIENTE#Linx.Framework.BV.UsuarioAutorizacao#IQueryable#DescricaoAplicacao,EmDesenvolvimento,UidAplicacao,Url[DescricaoAplicacao,EmDesenvolvimento,DescricaoAplicativo,UidAplicacao,Url,IdTcsAplicativo];DescricaoAplicativo,IdTcsAplicativo[DescricaoAplicativo,IdTcsAplicativo]#DescricaoAmbiente[DescricaoAplicacao=DescricaoAplicacao,EmDesenvolvimento=EmDesenvolvimento,IdLinxEmpresa=IdLinxEmpresa,DescricaoAplicativo=DescricaoAplicativo,NomeEmpresa=NomeEmpresa,UidAplicacao=UidAplicacao,UidEmpresa=UidEmpresa,Url=Url,IdTcsAplicativo=IdTcsAplicativo];DescricaoAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];EmDesenvolvimento[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdLinxEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdTcsAmbiente[DescricaoAplicacao=DescricaoAplicacao,EmDesenvolvimento=EmDesenvolvimento,IdLinxEmpresa=IdLinxEmpresa,DescricaoAplicativo=DescricaoAplicativo,NomeEmpresa=NomeEmpresa,UidAplicacao=UidAplicacao,UidEmpresa=UidEmpresa,Url=Url,IdTcsAplicativo=IdTcsAplicativo];NomeEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UidAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UidEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];Url[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo]#true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.EM_DESENVOLVIMENTO")]
	    public Boolean EmDesenvolvimento
	    {
	    	    get
	    	    {
	    	          return _EmDesenvolvimento;
	    	    }
	    	    set
	    	    {
	    	          if (this._EmDesenvolvimento != value)
	    	          {
	    	              this.ValidateProperty("EmDesenvolvimento", value);
	    	              this.OnEmDesenvolvimentoChanging(value);
	    	              this.RaiseDataMemberChanging("EmDesenvolvimento");
	    	              this._EmDesenvolvimento = value;
	    	              this.RaiseDataMemberChanged("EmDesenvolvimento");
	    	              this.OnEmDesenvolvimentoChanged();
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
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente1];LookUpTitle[Seleção de (Id Aplicacao)];LookUpQuery[executeLookUpTcsAmbiente1];LookUpFinalize[finalizeLookUpTcsAmbiente1];LookUpDisplayColumns[{\"DescricaoAmbienteRelacionado\" : \"Ambiente\", \"NomeEmpresaAmbienteRelacionado\" : \"Empresa\", \"DescricaoAplicacaoAmbienteRelacionado\" : \"Aplicação\", \"IdLinxAmbienteRelacionado\" : \"ID Linx\", \"IdTcsAmbienteRelacionado\" : \"Id Tcs Ambiente1\", \"IdAplicacao\" : \"Id Aplicacao\"}];LookUpColumns[{\"DescricaoAmbienteRelacionado\" : true, \"NomeEmpresaAmbienteRelacionado\" : true, \"DescricaoAplicacaoAmbienteRelacionado\" : true, \"IdLinxAmbienteRelacionado\" : false, \"IdTcsAmbienteRelacionado\" : false, \"IdAplicacao\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE1.TCS_APLICACAO.ID_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<Int32>#IdAplicacao#false##12:0##Id Aplicacao#5#false##::LookUpTcsAmbiente1##false#false#TCS_AMBIENTE1#TCS_AMBIENTE#Linx.Framework.BV.UsuarioAutorizacao#IQueryable#NomeEmpresaAmbienteRelacionado,IdLinxAmbienteRelacionado[NomeEmpresaAmbienteRelacionado,IdLinxAmbienteRelacionado]#DescricaoAmbienteRelacionado[NomeEmpresaAmbienteRelacionado=NomeEmpresaAmbienteRelacionado,DescricaoAplicacaoAmbienteRelacionado=DescricaoAplicacaoAmbienteRelacionado,IdLinxAmbienteRelacionado=IdLinxAmbienteRelacionado,IdAplicacao=IdAplicacao];IdTcsAmbienteRelacionado[NomeEmpresaAmbienteRelacionado=NomeEmpresaAmbienteRelacionado,DescricaoAplicacaoAmbienteRelacionado=DescricaoAplicacaoAmbienteRelacionado,IdLinxAmbienteRelacionado=IdLinxAmbienteRelacionado,IdAplicacao=IdAplicacao]#true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.TCS_APLICACAO.ID_APLICACAO")]
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
	    //Extensibility Partial Method Definitions For IdAplicacaoAmbiente
	    partial void OnIdAplicacaoAmbienteChanging(Int32 value);
	    partial void OnIdAplicacaoAmbienteChanged();

	    private Int32 _IdAplicacaoAmbiente;

	    [DataMember(IsRequired = true, Name = "IdAplicacaoAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Aplicacao", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.ID_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.ID_APLICACAO")]
	    public Int32 IdAplicacaoAmbiente
	    {
	    	    get
	    	    {
	    	          return _IdAplicacaoAmbiente;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdAplicacaoAmbiente != value)
	    	          {
	    	              this.ValidateProperty("IdAplicacaoAmbiente", value);
	    	              this.OnIdAplicacaoAmbienteChanging(value);
	    	              this.RaiseDataMemberChanging("IdAplicacaoAmbiente");
	    	              this._IdAplicacaoAmbiente = value;
	    	              this.RaiseDataMemberChanged("IdAplicacaoAmbiente");
	    	              this.OnIdAplicacaoAmbienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLinxAmbienteRelacionado
	    partial void OnIdLinxAmbienteRelacionadoChanging(System.Nullable<Int32> value);
	    partial void OnIdLinxAmbienteRelacionadoChanged();

	    private System.Nullable<Int32> _IdLinxAmbienteRelacionado;

	    [DataMember(Name = "IdLinxAmbienteRelacionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx1", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente1];LookUpTitle[Seleção de (Id Linx1)];LookUpQuery[executeLookUpTcsAmbiente1];LookUpFinalize[finalizeLookUpTcsAmbiente1];LookUpDisplayColumns[{\"DescricaoAmbienteRelacionado\" : \"Ambiente\", \"NomeEmpresaAmbienteRelacionado\" : \"Empresa\", \"DescricaoAplicacaoAmbienteRelacionado\" : \"Aplicação\", \"IdLinxAmbienteRelacionado\" : \"ID Linx\", \"IdTcsAmbienteRelacionado\" : \"Id Tcs Ambiente1\", \"IdAplicacao\" : \"Id Aplicacao\"}];LookUpColumns[{\"DescricaoAmbienteRelacionado\" : true, \"NomeEmpresaAmbienteRelacionado\" : true, \"DescricaoAplicacaoAmbienteRelacionado\" : true, \"IdLinxAmbienteRelacionado\" : false, \"IdTcsAmbienteRelacionado\" : false, \"IdAplicacao\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<Int32>#IdLinxAmbienteRelacionado#false##12:0##ID Linx#3#false##::LookUpTcsAmbiente1##false#false#TCS_AMBIENTE1#TCS_AMBIENTE#Linx.Framework.BV.UsuarioAutorizacao#IQueryable#NomeEmpresaAmbienteRelacionado,IdLinxAmbienteRelacionado[NomeEmpresaAmbienteRelacionado,IdLinxAmbienteRelacionado]#DescricaoAmbienteRelacionado[NomeEmpresaAmbienteRelacionado=NomeEmpresaAmbienteRelacionado,DescricaoAplicacaoAmbienteRelacionado=DescricaoAplicacaoAmbienteRelacionado,IdLinxAmbienteRelacionado=IdLinxAmbienteRelacionado,IdAplicacao=IdAplicacao];IdTcsAmbienteRelacionado[NomeEmpresaAmbienteRelacionado=NomeEmpresaAmbienteRelacionado,DescricaoAplicacaoAmbienteRelacionado=DescricaoAplicacaoAmbienteRelacionado,IdLinxAmbienteRelacionado=IdLinxAmbienteRelacionado,IdAplicacao=IdAplicacao]#true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
	    public System.Nullable<Int32> IdLinxAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _IdLinxAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLinxAmbienteRelacionado != value)
	    	          {
	    	              this.ValidateProperty("IdLinxAmbienteRelacionado", value);
	    	              this.OnIdLinxAmbienteRelacionadoChanging(value);
	    	              this.RaiseDataMemberChanging("IdLinxAmbienteRelacionado");
	    	              this._IdLinxAmbienteRelacionado = value;
	    	              this.RaiseDataMemberChanged("IdLinxAmbienteRelacionado");
	    	              this.OnIdLinxAmbienteRelacionadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLinxEmpresa
	    partial void OnIdLinxEmpresaChanging(Int32 value);
	    partial void OnIdLinxEmpresaChanged();

	    private Int32 _IdLinxEmpresa;

	    [DataMember(IsRequired = true, Name = "IdLinxEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente];LookUpTitle[Seleção de (Id Linx)];LookUpQuery[executeLookUpTcsAmbiente];LookUpFinalize[finalizeLookUpTcsAmbiente];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"DescricaoAplicacao\" : \"Aplicação\", \"EmDesenvolvimento\" : \"Em Desenvolvimento\", \"IdLinxEmpresa\" : \"Id Linx\", \"DescricaoAplicativo\" : \"Aplicativo\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\", \"NomeEmpresa\" : \"Grupo Econômico\", \"UidAplicacao\" : \"Uid Aplicacao\", \"UidEmpresa\" : \"Uid Empresa\", \"Url\" : \"Url\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"DescricaoAplicacao\" : true, \"EmDesenvolvimento\" : true, \"IdLinxEmpresa\" : false, \"DescricaoAplicativo\" : true, \"IdTcsAmbiente\" : false, \"NomeEmpresa\" : true, \"UidAplicacao\" : false, \"UidEmpresa\" : false, \"Url\" : false, \"IdTcsAplicativo\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdLinxEmpresa#true##12:0##Id Linx#3#false##::LookUpTcsAmbiente##true#false#TCS_AMBIENTE#TCS_AMBIENTE#Linx.Framework.BV.UsuarioAutorizacao#IQueryable#DescricaoAplicacao,EmDesenvolvimento,UidAplicacao,Url[DescricaoAplicacao,EmDesenvolvimento,DescricaoAplicativo,UidAplicacao,Url,IdTcsAplicativo];DescricaoAplicativo,IdTcsAplicativo[DescricaoAplicativo,IdTcsAplicativo]#DescricaoAmbiente[DescricaoAplicacao=DescricaoAplicacao,EmDesenvolvimento=EmDesenvolvimento,IdLinxEmpresa=IdLinxEmpresa,DescricaoAplicativo=DescricaoAplicativo,NomeEmpresa=NomeEmpresa,UidAplicacao=UidAplicacao,UidEmpresa=UidEmpresa,Url=Url,IdTcsAplicativo=IdTcsAplicativo];DescricaoAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];EmDesenvolvimento[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdLinxEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdTcsAmbiente[DescricaoAplicacao=DescricaoAplicacao,EmDesenvolvimento=EmDesenvolvimento,IdLinxEmpresa=IdLinxEmpresa,DescricaoAplicativo=DescricaoAplicativo,NomeEmpresa=NomeEmpresa,UidAplicacao=UidAplicacao,UidEmpresa=UidEmpresa,Url=Url,IdTcsAplicativo=IdTcsAplicativo];NomeEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UidAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UidEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];Url[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo]#true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
	    public Int32 IdLinxEmpresa
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
	    //Extensibility Partial Method Definitions For IdTcsAmbiente
	    partial void OnIdTcsAmbienteChanging(Int32 value);
	    partial void OnIdTcsAmbienteChanged();

	    private Int32 _IdTcsAmbiente;

	    [DataMember(IsRequired = true, Name = "IdTcsAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente];LookUpTitle[Seleção de (Id Tcs Ambiente)];LookUpQuery[executeLookUpTcsAmbiente];LookUpFinalize[finalizeLookUpTcsAmbiente];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"DescricaoAplicacao\" : \"Aplicação\", \"EmDesenvolvimento\" : \"Em Desenvolvimento\", \"IdLinxEmpresa\" : \"Id Linx\", \"DescricaoAplicativo\" : \"Aplicativo\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\", \"NomeEmpresa\" : \"Grupo Econômico\", \"UidAplicacao\" : \"Uid Aplicacao\", \"UidEmpresa\" : \"Uid Empresa\", \"Url\" : \"Url\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"DescricaoAplicacao\" : true, \"EmDesenvolvimento\" : true, \"IdLinxEmpresa\" : false, \"DescricaoAplicativo\" : true, \"IdTcsAmbiente\" : false, \"NomeEmpresa\" : true, \"UidAplicacao\" : false, \"UidEmpresa\" : false, \"Url\" : false, \"IdTcsAplicativo\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.ID_TCS_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdTcsAmbiente#true##12:0##Id Tcs Ambiente#5#false##::LookUpTcsAmbiente##true#false#TCS_AMBIENTE#TCS_AMBIENTE#Linx.Framework.BV.UsuarioAutorizacao#IQueryable#DescricaoAplicacao,EmDesenvolvimento,UidAplicacao,Url[DescricaoAplicacao,EmDesenvolvimento,DescricaoAplicativo,UidAplicacao,Url,IdTcsAplicativo];DescricaoAplicativo,IdTcsAplicativo[DescricaoAplicativo,IdTcsAplicativo]#DescricaoAmbiente[DescricaoAplicacao=DescricaoAplicacao,EmDesenvolvimento=EmDesenvolvimento,IdLinxEmpresa=IdLinxEmpresa,DescricaoAplicativo=DescricaoAplicativo,NomeEmpresa=NomeEmpresa,UidAplicacao=UidAplicacao,UidEmpresa=UidEmpresa,Url=Url,IdTcsAplicativo=IdTcsAplicativo];DescricaoAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];EmDesenvolvimento[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdLinxEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdTcsAmbiente[DescricaoAplicacao=DescricaoAplicacao,EmDesenvolvimento=EmDesenvolvimento,IdLinxEmpresa=IdLinxEmpresa,DescricaoAplicativo=DescricaoAplicativo,NomeEmpresa=NomeEmpresa,UidAplicacao=UidAplicacao,UidEmpresa=UidEmpresa,Url=Url,IdTcsAplicativo=IdTcsAplicativo];NomeEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UidAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UidEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];Url[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo]#true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.ID_TCS_AMBIENTE")]
	    public Int32 IdTcsAmbiente
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
	    //Extensibility Partial Method Definitions For IdTcsAmbienteRelacionado
	    partial void OnIdTcsAmbienteRelacionadoChanging(System.Nullable<Int32> value);
	    partial void OnIdTcsAmbienteRelacionadoChanged();

	    private System.Nullable<Int32> _IdTcsAmbienteRelacionado;

	    [DataMember(Name = "IdTcsAmbienteRelacionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente1", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente1];LookUpTitle[Seleção de (Id Tcs Ambiente1)];LookUpQuery[executeLookUpTcsAmbiente1];LookUpFinalize[finalizeLookUpTcsAmbiente1];LookUpDisplayColumns[{\"DescricaoAmbienteRelacionado\" : \"Ambiente\", \"NomeEmpresaAmbienteRelacionado\" : \"Empresa\", \"DescricaoAplicacaoAmbienteRelacionado\" : \"Aplicação\", \"IdLinxAmbienteRelacionado\" : \"ID Linx\", \"IdTcsAmbienteRelacionado\" : \"Id Tcs Ambiente1\", \"IdAplicacao\" : \"Id Aplicacao\"}];LookUpColumns[{\"DescricaoAmbienteRelacionado\" : true, \"NomeEmpresaAmbienteRelacionado\" : true, \"DescricaoAplicacaoAmbienteRelacionado\" : true, \"IdLinxAmbienteRelacionado\" : false, \"IdTcsAmbienteRelacionado\" : false, \"IdAplicacao\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE1.ID_TCS_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<Int32>#IdTcsAmbienteRelacionado#true##12:0##Id Tcs Ambiente1#4#false##::LookUpTcsAmbiente1##false#false#TCS_AMBIENTE1#TCS_AMBIENTE#Linx.Framework.BV.UsuarioAutorizacao#IQueryable#NomeEmpresaAmbienteRelacionado,IdLinxAmbienteRelacionado[NomeEmpresaAmbienteRelacionado,IdLinxAmbienteRelacionado]#DescricaoAmbienteRelacionado[NomeEmpresaAmbienteRelacionado=NomeEmpresaAmbienteRelacionado,DescricaoAplicacaoAmbienteRelacionado=DescricaoAplicacaoAmbienteRelacionado,IdLinxAmbienteRelacionado=IdLinxAmbienteRelacionado,IdAplicacao=IdAplicacao];IdTcsAmbienteRelacionado[NomeEmpresaAmbienteRelacionado=NomeEmpresaAmbienteRelacionado,DescricaoAplicacaoAmbienteRelacionado=DescricaoAplicacaoAmbienteRelacionado,IdLinxAmbienteRelacionado=IdLinxAmbienteRelacionado,IdAplicacao=IdAplicacao]#true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.ID_TCS_AMBIENTE")]
	    public System.Nullable<Int32> IdTcsAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _IdTcsAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAmbienteRelacionado != value)
	    	          {
	    	              this.ValidateProperty("IdTcsAmbienteRelacionado", value);
	    	              this.OnIdTcsAmbienteRelacionadoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsAmbienteRelacionado");
	    	              this._IdTcsAmbienteRelacionado = value;
	    	              this.RaiseDataMemberChanged("IdTcsAmbienteRelacionado");
	    	              this.OnIdTcsAmbienteRelacionadoChanged();
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
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente];LookUpTitle[Seleção de (Id Tcs Aplicativo)];LookUpQuery[executeLookUpTcsAmbiente];LookUpFinalize[finalizeLookUpTcsAmbiente];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"DescricaoAplicacao\" : \"Aplicação\", \"EmDesenvolvimento\" : \"Em Desenvolvimento\", \"IdLinxEmpresa\" : \"Id Linx\", \"DescricaoAplicativo\" : \"Aplicativo\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\", \"NomeEmpresa\" : \"Grupo Econômico\", \"UidAplicacao\" : \"Uid Aplicacao\", \"UidEmpresa\" : \"Uid Empresa\", \"Url\" : \"Url\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"DescricaoAplicacao\" : true, \"EmDesenvolvimento\" : true, \"IdLinxEmpresa\" : false, \"DescricaoAplicativo\" : true, \"IdTcsAmbiente\" : false, \"NomeEmpresa\" : true, \"UidAplicacao\" : false, \"UidEmpresa\" : false, \"Url\" : false, \"IdTcsAplicativo\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdTcsAplicativo#true##12:0##Id Tcs Aplicativo#10#false##::LookUpTcsAmbiente##true#false#TCS_AMBIENTE#TCS_AMBIENTE#Linx.Framework.BV.UsuarioAutorizacao#IQueryable#DescricaoAplicacao,EmDesenvolvimento,UidAplicacao,Url[DescricaoAplicacao,EmDesenvolvimento,DescricaoAplicativo,UidAplicacao,Url,IdTcsAplicativo];DescricaoAplicativo,IdTcsAplicativo[DescricaoAplicativo,IdTcsAplicativo]#DescricaoAmbiente[DescricaoAplicacao=DescricaoAplicacao,EmDesenvolvimento=EmDesenvolvimento,IdLinxEmpresa=IdLinxEmpresa,DescricaoAplicativo=DescricaoAplicativo,NomeEmpresa=NomeEmpresa,UidAplicacao=UidAplicacao,UidEmpresa=UidEmpresa,Url=Url,IdTcsAplicativo=IdTcsAplicativo];DescricaoAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];EmDesenvolvimento[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdLinxEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdTcsAmbiente[DescricaoAplicacao=DescricaoAplicacao,EmDesenvolvimento=EmDesenvolvimento,IdLinxEmpresa=IdLinxEmpresa,DescricaoAplicativo=DescricaoAplicativo,NomeEmpresa=NomeEmpresa,UidAplicacao=UidAplicacao,UidEmpresa=UidEmpresa,Url=Url,IdTcsAplicativo=IdTcsAplicativo];NomeEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UidAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UidEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];Url[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo]#true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO")]
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
	    //Extensibility Partial Method Definitions For IdTcsUsuarioAcesso
	    partial void OnIdTcsUsuarioAcessoChanging(Int32 value);
	    partial void OnIdTcsUsuarioAcessoChanged();

	    private Int32 _IdTcsUsuarioAcesso;

	    [DataMember(IsRequired = true, Name = "IdTcsUsuarioAcesso", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Acesso", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO")]
	    public Int32 IdTcsUsuarioAcesso
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
	    partial void OnIdUsuarioChanging(Int64 value);
	    partial void OnIdUsuarioChanged();

	    private Int64 _IdUsuario;

	    [DataMember(IsRequired = true, Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.ID_USUARIO")]
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
	    //Extensibility Partial Method Definitions For IndicaAcessoPadrao
	    partial void OnIndicaAcessoPadraoChanging(Boolean value);
	    partial void OnIndicaAcessoPadraoChanged();

	    private Boolean _IndicaAcessoPadrao;

	    [DataMember(IsRequired = true, Name = "IndicaAcessoPadrao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Acesso Padrão", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.INDICA_ACESSO_PADRAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.INDICA_ACESSO_PADRAO")]
	    public Boolean IndicaAcessoPadrao
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
	    partial void OnIndicaAdministradorChanging(Boolean value);
	    partial void OnIndicaAdministradorChanged();

	    private Boolean _IndicaAdministrador;

	    [DataMember(IsRequired = true, Name = "IndicaAdministrador", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Administrador", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.INDICA_ADMINISTRADOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.INDICA_ADMINISTRADOR")]
	    public Boolean IndicaAdministrador
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
	    partial void OnIndicaMultiGpeconChanging(Boolean value);
	    partial void OnIndicaMultiGpeconChanged();

	    private Boolean _IndicaMultiGpecon;

	    [DataMember(IsRequired = true, Name = "IndicaMultiGpecon", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Multi Grupo Econômico", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.INDICA_MULTI_GPECON];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.INDICA_MULTI_GPECON")]
	    public Boolean IndicaMultiGpecon
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
	    //Extensibility Partial Method Definitions For NomeEmpresaAmbienteRelacionado
	    partial void OnNomeEmpresaAmbienteRelacionadoChanging(System.String value);
	    partial void OnNomeEmpresaAmbienteRelacionadoChanged();

	    private System.String _NomeEmpresaAmbienteRelacionado;

	    [DataMember(Name = "NomeEmpresaAmbienteRelacionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Empresa Ambiente Relacionado", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente1];LookUpTitle[Seleção de (Empresa Ambiente Relacionado)];LookUpQuery[executeLookUpTcsAmbiente1];LookUpFinalize[finalizeLookUpTcsAmbiente1];LookUpDisplayColumns[{\"DescricaoAmbienteRelacionado\" : \"Ambiente\", \"NomeEmpresaAmbienteRelacionado\" : \"Empresa\", \"DescricaoAplicacaoAmbienteRelacionado\" : \"Aplicação\", \"IdLinxAmbienteRelacionado\" : \"ID Linx\", \"IdTcsAmbienteRelacionado\" : \"Id Tcs Ambiente1\", \"IdAplicacao\" : \"Id Aplicacao\"}];LookUpColumns[{\"DescricaoAmbienteRelacionado\" : true, \"NomeEmpresaAmbienteRelacionado\" : true, \"DescricaoAplicacaoAmbienteRelacionado\" : true, \"IdLinxAmbienteRelacionado\" : false, \"IdTcsAmbienteRelacionado\" : false, \"IdAplicacao\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeEmpresaAmbienteRelacionado#false##250:0##Empresa#1#true##::LookUpTcsAmbiente1##false#false#TCS_AMBIENTE1#TCS_AMBIENTE#Linx.Framework.BV.UsuarioAutorizacao#IQueryable#NomeEmpresaAmbienteRelacionado,IdLinxAmbienteRelacionado[NomeEmpresaAmbienteRelacionado,IdLinxAmbienteRelacionado]#DescricaoAmbienteRelacionado[NomeEmpresaAmbienteRelacionado=NomeEmpresaAmbienteRelacionado,DescricaoAplicacaoAmbienteRelacionado=DescricaoAplicacaoAmbienteRelacionado,IdLinxAmbienteRelacionado=IdLinxAmbienteRelacionado,IdAplicacao=IdAplicacao];IdTcsAmbienteRelacionado[NomeEmpresaAmbienteRelacionado=NomeEmpresaAmbienteRelacionado,DescricaoAplicacaoAmbienteRelacionado=DescricaoAplicacaoAmbienteRelacionado,IdLinxAmbienteRelacionado=IdLinxAmbienteRelacionado,IdAplicacao=IdAplicacao]#true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
	    public System.String NomeEmpresaAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _NomeEmpresaAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeEmpresaAmbienteRelacionado != value)
	    	          {
	    	              this.ValidateProperty("NomeEmpresaAmbienteRelacionado", value);
	    	              this.OnNomeEmpresaAmbienteRelacionadoChanging(value);
	    	              this.RaiseDataMemberChanging("NomeEmpresaAmbienteRelacionado");
	    	              this._NomeEmpresaAmbienteRelacionado = value;
	    	              this.RaiseDataMemberChanged("NomeEmpresaAmbienteRelacionado");
	    	              this.OnNomeEmpresaAmbienteRelacionadoChanged();
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
	    [Display(Name = "Usuário Autenticação", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO")]
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
	    [Display(Name = "Nome", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO")]
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

	    private Int32 _TemporaryIdTcsUsuarioAcesso;
	    [DataMember(Name = "TemporaryIdTcsUsuarioAcesso", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Acesso (Tmp)", Description="Temporary Key", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdTcsUsuarioAcesso
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTcsUsuarioAcesso.IsNullOrEmpty())
	    	                this._TemporaryIdTcsUsuarioAcesso = this._IdTcsUsuarioAcesso;
	    	          return this._TemporaryIdTcsUsuarioAcesso;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTcsUsuarioAcesso != value)
	    	              this._TemporaryIdTcsUsuarioAcesso = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsUsuarioAutenticacao _TcsUsuarioAutenticacao;
	    [DataMember(Name = "TcsUsuarioAutenticacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsUsuarioAutenticacao_TcsUsuarioAcesso", "IdUsuario", "IdUsuario", IsForeignKey=true)]
	    public TcsUsuarioAutenticacao TcsUsuarioAutenticacao
	    {
	        get
	        {
	            return this._TcsUsuarioAutenticacao;
	        }
	        set
	        {
	            if (this._TcsUsuarioAutenticacao != value)
	            {
	                this._TcsUsuarioAutenticacao = value;
	                this.RaisePropertyChanged("TcsUsuarioAutenticacaoList");
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
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.ID_TCS_AMBIENTE", Source = "IdTcsAmbienteRelacionado", Target = "ID_TCS_AMBIENTE", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE", RelationPropertyName = "TCS_AMBIENTE1" });
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

		

	[LinxPublicationView(PrimaryKeys="TCS_IDENTIDADE_EXTERNA.ID_IDENTIDADE_EXTERNA", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Identidade Externa];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdIdentidadeExterna];ReadOnly[false];Entities[TCS_IDENTIDADE_EXTERNA:IdIdentidadeExterna];SubQueryInfo[Select 1 From #ParentAlias#.TCS_IDENTIDADE_EXTERNA_LISTA as #Alias#];EdmEntityName[TCS_IDENTIDADE_EXTERNA];EntityRelations[TCS_USUARIO_AUTENTICACAO(TCS_USUARIO_AUTENTICACAO)#TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)];EdmParentEntityName[TCS_USUARIO_AUTENTICACAO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsIdentidadeExterna")]
	[Serializable()]
	public partial class TcsIdentidadeExterna : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(UsuarioAutorizacaoDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsUsuarioAutenticacao");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuario"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdUsuario));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsUsuarioAutenticacao
	         this.TcsUsuarioAutenticacao = (from r in context.GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For IdentidadeExterna
	    partial void OnIdentidadeExternaChanging(System.String value);
	    partial void OnIdentidadeExternaChanged();

	    private System.String _IdentidadeExterna;

	    [DataMember(IsRequired = true, Name = "IdentidadeExterna", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Identidade Externa", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_IDENTIDADE_EXTERNA.IDENTIDADE_EXTERNA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_IDENTIDADE_EXTERNA.IDENTIDADE_EXTERNA")]
	    public System.String IdentidadeExterna
	    {
	    	    get
	    	    {
	    	          return _IdentidadeExterna;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdentidadeExterna != value)
	    	          {
	    	              this.ValidateProperty("IdentidadeExterna", value);
	    	              this.OnIdentidadeExternaChanging(value);
	    	              this.RaiseDataMemberChanging("IdentidadeExterna");
	    	              this._IdentidadeExterna = value;
	    	              this.RaiseDataMemberChanged("IdentidadeExterna");
	    	              this.OnIdentidadeExternaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdIdentidadeExterna
	    partial void OnIdIdentidadeExternaChanging(Int64 value);
	    partial void OnIdIdentidadeExternaChanged();

	    private Int64 _IdIdentidadeExterna;

	    [DataMember(IsRequired = true, Name = "IdIdentidadeExterna", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Identidade Externa", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_IDENTIDADE_EXTERNA.ID_IDENTIDADE_EXTERNA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_IDENTIDADE_EXTERNA.ID_IDENTIDADE_EXTERNA")]
	    public Int64 IdIdentidadeExterna
	    {
	    	    get
	    	    {
	    	          return _IdIdentidadeExterna;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdIdentidadeExterna != value)
	    	          {
	    	              this.ValidateProperty("IdIdentidadeExterna", value);
	    	              this.OnIdIdentidadeExternaChanging(value);
	    	              this.RaiseDataMemberChanging("IdIdentidadeExterna");
	    	              this._IdIdentidadeExterna = value;
	    	              this.RaiseDataMemberChanged("IdIdentidadeExterna");
	    	              this.OnIdIdentidadeExternaChanged();
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
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_IDENTIDADE_EXTERNA.TCS_USUARIO_AUTENTICACAO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_IDENTIDADE_EXTERNA.TCS_USUARIO_AUTENTICACAO.ID_USUARIO")]
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

	    private Int64 _TemporaryIdIdentidadeExterna;
	    [DataMember(Name = "TemporaryIdIdentidadeExterna", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Identidade Externa (Tmp)", Description="Temporary Key", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdIdentidadeExterna
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdIdentidadeExterna.IsNullOrEmpty())
	    	                this._TemporaryIdIdentidadeExterna = this._IdIdentidadeExterna;
	    	          return this._TemporaryIdIdentidadeExterna;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdIdentidadeExterna != value)
	    	              this._TemporaryIdIdentidadeExterna = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsUsuarioAutenticacao _TcsUsuarioAutenticacao;
	    [DataMember(Name = "TcsUsuarioAutenticacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsUsuarioAutenticacao_TcsIdentidadeExterna", "IdUsuario", "IdUsuario", IsForeignKey=true)]
	    public TcsUsuarioAutenticacao TcsUsuarioAutenticacao
	    {
	        get
	        {
	            return this._TcsUsuarioAutenticacao;
	        }
	        set
	        {
	            if (this._TcsUsuarioAutenticacao != value)
	            {
	                this._TcsUsuarioAutenticacao = value;
	                this.RaisePropertyChanged("TcsUsuarioAutenticacaoList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_IDENTIDADE_EXTERNA").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_IDENTIDADE_EXTERNA), QualifiedEntitySetName = "AutorizacaoContext.TCS_IDENTIDADE_EXTERNA" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_IDENTIDADE_EXTERNA.IDENTIDADE_EXTERNA", Source = "IdentidadeExterna", Target = "IDENTIDADE_EXTERNA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_IDENTIDADE_EXTERNA", RelationPropertyName = "TCS_IDENTIDADE_EXTERNA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_IDENTIDADE_EXTERNA.ID_IDENTIDADE_EXTERNA", Source = "IdIdentidadeExterna", Target = "ID_IDENTIDADE_EXTERNA", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_IDENTIDADE_EXTERNA", RelationPropertyName = "TCS_IDENTIDADE_EXTERNA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_IDENTIDADE_EXTERNA.TCS_USUARIO_AUTENTICACAO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });

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

		

	[LinxPublicationView(PrimaryKeys="TCS_USUARIO_AUTENTICACAO_GPECON.ID_TCS_USUARIO_AUT_GPECON", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Grupo Econômico];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#. as #Alias#];EdmEntityName[TCS_USUARIO_AUTENTICACAO_GPECON];EntityRelations[TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)#TCS_USUARIO_AUTENTICACAO(TCS_USUARIO_AUTENTICACAO)];EdmParentEntityName[TCS_USUARIO_AUTENTICACAO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioGpecon")]
	[Serializable()]
	public partial class TcsUsuarioGpecon : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(UsuarioAutorizacaoDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsUsuarioAutenticacao");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuario"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdUsuario));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsUsuarioAutenticacao
	         this.TcsUsuarioAutenticacao = (from r in context.GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For IdLinx
	    partial void OnIdLinxChanging(int value);
	    partial void OnIdLinxChanged();

	    private int _IdLinx;

	    [DataMember(IsRequired = true, Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx Empresa / Grupo Econômico", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsEmpresaAutenticacao];LookUpTitle[Seleção de (Id Linx Empresa / Grupo Econômico)];LookUpQuery[executeLookUpTcsEmpresaAutenticacao];LookUpFinalize[finalizeLookUpTcsEmpresaAutenticacao];LookUpDisplayColumns[{\"IdLinx\" : \"Id Linx Empresa / Grupo Econômico\", \"NomeEmpresa\" : \"Empresa / Grupo Econômico\"}];LookUpColumns[{\"IdLinx\" : true, \"NomeEmpresa\" : true}];FilterDataKey[TCS_USUARIO_AUTENTICACAO_GPECON.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="int#IdLinx#true##0:0##Id Linx Empresa / Grupo Econômico#0#true##::LookUpTcsEmpresaAutenticacao##true#false#TCS_EMPRESA_AUTENTICACAO#TCS_EMPRESA_AUTENTICACAO#Linx.Framework.BV.UsuarioAutorizacao#IQueryable###true#false", EdmKey="TCS_USUARIO_AUTENTICACAO_GPECON.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
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
	    //Extensibility Partial Method Definitions For IdTcsUsuarioAutGpecon
	    partial void OnIdTcsUsuarioAutGpeconChanging(int value);
	    partial void OnIdTcsUsuarioAutGpeconChanged();

	    private int _IdTcsUsuarioAutGpecon;

	    [DataMember(IsRequired = true, Name = "IdTcsUsuarioAutGpecon", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Aut Gpecon", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO_GPECON.ID_TCS_USUARIO_AUT_GPECON];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO_GPECON.ID_TCS_USUARIO_AUT_GPECON")]
	    public int IdTcsUsuarioAutGpecon
	    {
	    	    get
	    	    {
	    	          return _IdTcsUsuarioAutGpecon;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsUsuarioAutGpecon != value)
	    	          {
	    	              this.ValidateProperty("IdTcsUsuarioAutGpecon", value);
	    	              this.OnIdTcsUsuarioAutGpeconChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsUsuarioAutGpecon");
	    	              this._IdTcsUsuarioAutGpecon = value;
	    	              this.RaiseDataMemberChanged("IdTcsUsuarioAutGpecon");
	    	              this.OnIdTcsUsuarioAutGpeconChanged();
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
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.ID_USUARIO")]
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
	    //Extensibility Partial Method Definitions For NomeEmpresa
	    partial void OnNomeEmpresaChanging(string value);
	    partial void OnNomeEmpresaChanged();

	    private string _NomeEmpresa;

	    [DataMember(IsRequired = true, Name = "NomeEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Empresa / Grupo Econômico", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsEmpresaAutenticacao];LookUpTitle[Seleção de (Empresa / Grupo Econômico)];LookUpQuery[executeLookUpTcsEmpresaAutenticacao];LookUpFinalize[finalizeLookUpTcsEmpresaAutenticacao];LookUpDisplayColumns[{\"IdLinx\" : \"Id Linx Empresa / Grupo Econômico\", \"NomeEmpresa\" : \"Empresa / Grupo Econômico\"}];LookUpColumns[{\"IdLinx\" : true, \"NomeEmpresa\" : true}];FilterDataKey[TCS_USUARIO_AUTENTICACAO_GPECON.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#NomeEmpresa#false##250:0##Empresa / Grupo Econômico#1#true##::LookUpTcsEmpresaAutenticacao##true#false#TCS_EMPRESA_AUTENTICACAO#TCS_EMPRESA_AUTENTICACAO#Linx.Framework.BV.UsuarioAutorizacao#IQueryable###true#false", EdmKey="TCS_USUARIO_AUTENTICACAO_GPECON.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
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
	    //Extensibility Partial Method Definitions For NomeAutenticacao
	    partial void OnNomeAutenticacaoChanging(System.String value);
	    partial void OnNomeAutenticacaoChanged();

	    private System.String _NomeAutenticacao;

	    [DataMember(Name = "NomeAutenticacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário Autenticação", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO")]
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

	    [DataMember(Name = "NomeUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO")]
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
	 
	    private TcsUsuarioAutenticacao _TcsUsuarioAutenticacao;
	    [DataMember(Name = "TcsUsuarioAutenticacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsUsuarioAutenticacao_TcsUsuarioGpecon", "IdUsuario", "IdUsuario", IsForeignKey=true)]
	    public TcsUsuarioAutenticacao TcsUsuarioAutenticacao
	    {
	        get
	        {
	            return this._TcsUsuarioAutenticacao;
	        }
	        set
	        {
	            if (this._TcsUsuarioAutenticacao != value)
	            {
	                this._TcsUsuarioAutenticacao = value;
	                this.RaisePropertyChanged("TcsUsuarioAutenticacaoList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO_GPECON").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_USUARIO_AUTENTICACAO_GPECON), QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO_GPECON" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO_GPECON.ID_TCS_USUARIO_AUT_GPECON", Source = "IdTcsUsuarioAutGpecon", Target = "ID_TCS_USUARIO_AUT_GPECON", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO_GPECON", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO_GPECON" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO_GPECON.TCS_EMPRESA_AUTENTICACAO.ID_LINX", Source = "IdLinx", Target = "ID_LINX", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_AUTENTICACAO", RelationPropertyName = "TCS_EMPRESA_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });

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

		

	[LinxPublicationView(PrimaryKeys="RequisicaoAcesso.EntityUniqueKey", IsUpdatable=false, EdmName="")]
		
	[DataContract(IsReference = false, Name = "RequisicaoAcesso")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.UsuarioAutorizacao.RequisicaoAcesso")]
	public partial class RequisicaoAcesso 
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
	 


	    private string _NomeAutenticacao;

	    [DataMember(Name = "NomeAutenticacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string NomeAutenticacao
	    {
	    	    get
	    	    {
	    	          if (_NomeAutenticacao.IsNullOrEmpty())
	    	             _NomeAutenticacao =  String.Empty;
	    	          return _NomeAutenticacao;
	    	    }
	    	    set
	    	    {
	    	          this._NomeAutenticacao = value;
	    	    }
	    }

	    private bool _AcessoLocal;

	    [DataMember(Name = "AcessoLocal", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public bool AcessoLocal
	    {
	    	    get
	    	    {
	    	          return _AcessoLocal;
	    	    }
	    	    set
	    	    {
	    	          this._AcessoLocal = value;
	    	    }
	    }

	    private string _Parametros;

	    [DataMember(Name = "Parametros", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Parametros
	    {
	    	    get
	    	    {
	    	          return _Parametros;
	    	    }
	    	    set
	    	    {
	    	          this._Parametros = value;
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

		

	[LinxPublicationView(PrimaryKeys="UsuarioAcesso.EntityUniqueKey", IsUpdatable=false, EdmName="")]
		
	[DataContract(IsReference = false, Name = "UsuarioAcesso")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.UsuarioAutorizacao.UsuarioAcesso")]
	public partial class UsuarioAcesso 
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

	    private string _DescricaoAmbiente;

	    [DataMember(Name = "DescricaoAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string DescricaoAmbiente
	    {
	    	    get
	    	    {
	    	          return _DescricaoAmbiente;
	    	    }
	    	    set
	    	    {
	    	          this._DescricaoAmbiente = value;
	    	    }
	    }

	    private Guid _UidAplicacao;

	    [DataMember(Name = "UidAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
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

	    private string _DescricaoAplicacao;

	    [DataMember(Name = "DescricaoAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string DescricaoAplicacao
	    {
	    	    get
	    	    {
	    	          return _DescricaoAplicacao;
	    	    }
	    	    set
	    	    {
	    	          this._DescricaoAplicacao = value;
	    	    }
	    }

	    private Guid _UidEmpresa;

	    [DataMember(Name = "UidEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
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

	    private string _NomeEmpresa;

	    [DataMember(Name = "NomeEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string NomeEmpresa
	    {
	    	    get
	    	    {
	    	          return _NomeEmpresa;
	    	    }
	    	    set
	    	    {
	    	          this._NomeEmpresa = value;
	    	    }
	    }

	    private Guid _UidGrupoEconomico;

	    [DataMember(Name = "UidGrupoEconomico", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public Guid UidGrupoEconomico
	    {
	    	    get
	    	    {
	    	          return _UidGrupoEconomico;
	    	    }
	    	    set
	    	    {
	    	          this._UidGrupoEconomico = value;
	    	    }
	    }

	    private string _GrupoEconomico;

	    [DataMember(Name = "GrupoEconomico", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string GrupoEconomico
	    {
	    	    get
	    	    {
	    	          return _GrupoEconomico;
	    	    }
	    	    set
	    	    {
	    	          this._GrupoEconomico = value;
	    	    }
	    }

	    private Guid _UidUsuario;

	    [DataMember(Name = "UidUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public Guid UidUsuario
	    {
	    	    get
	    	    {
	    	          return _UidUsuario;
	    	    }
	    	    set
	    	    {
	    	          this._UidUsuario = value;
	    	    }
	    }

	    private string _NomeUsuario;

	    [DataMember(Name = "NomeUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string NomeUsuario
	    {
	    	    get
	    	    {
	    	          return _NomeUsuario;
	    	    }
	    	    set
	    	    {
	    	          this._NomeUsuario = value;
	    	    }
	    }

	    private Guid _UidUsuarioSuporte;

	    [DataMember(Name = "UidUsuarioSuporte", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public Guid UidUsuarioSuporte
	    {
	    	    get
	    	    {
	    	          return _UidUsuarioSuporte;
	    	    }
	    	    set
	    	    {
	    	          this._UidUsuarioSuporte = value;
	    	    }
	    }

	    private string _UsuarioSuporte;

	    [DataMember(Name = "UsuarioSuporte", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string UsuarioSuporte
	    {
	    	    get
	    	    {
	    	          return _UsuarioSuporte;
	    	    }
	    	    set
	    	    {
	    	          this._UsuarioSuporte = value;
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

	    private string _DescricaoAplicativo;

	    [DataMember(Name = "DescricaoAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string DescricaoAplicativo
	    {
	    	    get
	    	    {
	    	          return _DescricaoAplicativo;
	    	    }
	    	    set
	    	    {
	    	          this._DescricaoAplicativo = value;
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

	    private int _IdLinxGpecon;

	    [DataMember(Name = "IdLinxGpecon", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public int IdLinxGpecon
	    {
	    	    get
	    	    {
	    	          return _IdLinxGpecon;
	    	    }
	    	    set
	    	    {
	    	          this._IdLinxGpecon = value;
	    	    }
	    }

	    private bool _IndicaAdministrador;

	    [DataMember(Name = "IndicaAdministrador", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public bool IndicaAdministrador
	    {
	    	    get
	    	    {
	    	          return _IndicaAdministrador;
	    	    }
	    	    set
	    	    {
	    	          this._IndicaAdministrador = value;
	    	    }
	    }

	    private string _NomeAutenticacao;

	    [DataMember(Name = "NomeAutenticacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string NomeAutenticacao
	    {
	    	    get
	    	    {
	    	          return _NomeAutenticacao;
	    	    }
	    	    set
	    	    {
	    	          this._NomeAutenticacao = value;
	    	    }
	    }

	    private Boolean _IndicaAcessoPadrao;

	    [DataMember(Name = "IndicaAcessoPadrao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public Boolean IndicaAcessoPadrao
	    {
	    	    get
	    	    {
	    	          return _IndicaAcessoPadrao;
	    	    }
	    	    set
	    	    {
	    	          this._IndicaAcessoPadrao = value;
	    	    }
	    }

	    private string _UrlWorkArea;

	    [DataMember(Name = "UrlWorkArea", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string UrlWorkArea
	    {
	    	    get
	    	    {
	    	          return _UrlWorkArea;
	    	    }
	    	    set
	    	    {
	    	          this._UrlWorkArea = value;
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

		

	[LinxPublicationView(PrimaryKeys="TCS_SUPORTE_ACESSO_LOG.ID_TCS_SUPORTE_ACESSO_LOG", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsSuporteAcessoLog];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsSuporteAcessoLog];ReadOnly[false];Entities[TCS_SUPORTE_ACESSO_LOG:IdTcsSuporteAcessoLog|TCS_USUARIO_ACESSO:IdTcsUsuarioAcesso];SubQueryInfo[];EdmEntityName[TCS_SUPORTE_ACESSO_LOG];EntityRelations[TCS_USUARIO_ACESSO(TCS_USUARIO_ACESSO)#TCS_USUARIO_AUTENTICACAO(TCS_USUARIO_AUTENTICACAO)#TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)#TCS_AMBIENTE(TCS_AMBIENTE)#TCS_APLICACAO(TCS_APLICACAO)#TCS_APLICATIVO(TCS_APLICATIVO)#TCS_AMBIENTE1(TCS_AMBIENTE)#USUARIO_SUPORTE(TCS_USUARIO_AUTENTICACAO)#USUARIO_ACESSO(TCS_USUARIO_AUTENTICACAO)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsSuporteAcessoLog")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.UsuarioAutorizacao.TcsSuporteAcessoLog")]
	public partial class TcsSuporteAcessoLog : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For AcessoExpirado
	    partial void OnAcessoExpiradoChanging(Boolean value);
	    partial void OnAcessoExpiradoChanged();

	    private Boolean _AcessoExpirado;

	    [DataMember(IsRequired = true, Name = "AcessoExpirado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Acesso Expirado", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_SUPORTE_ACESSO_LOG.ACESSO_EXPIRADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_SUPORTE_ACESSO_LOG.ACESSO_EXPIRADO")]
	    public Boolean AcessoExpirado
	    {
	    	    get
	    	    {
	    	          return _AcessoExpirado;
	    	    }
	    	    set
	    	    {
	    	          if (this._AcessoExpirado != value)
	    	          {
	    	              this.ValidateProperty("AcessoExpirado", value);
	    	              this.OnAcessoExpiradoChanging(value);
	    	              this.RaiseDataMemberChanging("AcessoExpirado");
	    	              this._AcessoExpirado = value;
	    	              this.RaiseDataMemberChanged("AcessoExpirado");
	    	              this.OnAcessoExpiradoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DataAcesso
	    partial void OnDataAcessoChanging(System.Nullable<System.DateTime> value);
	    partial void OnDataAcessoChanged();

	    private System.Nullable<System.DateTime> _DataAcesso;

	    [DataMember(Name = "DataAcesso", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Data Acesso", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_SUPORTE_ACESSO_LOG.DATA_ACESSO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_SUPORTE_ACESSO_LOG.DATA_ACESSO")]
	    public System.Nullable<System.DateTime> DataAcesso
	    {
	    	    get
	    	    {
	    	          return _DataAcesso;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataAcesso != value)
	    	          {
	    	              this.ValidateProperty("DataAcesso", value);
	    	              this.OnDataAcessoChanging(value);
	    	              this.RaiseDataMemberChanging("DataAcesso");
	    	              this._DataAcesso = value;
	    	              this.RaiseDataMemberChanged("DataAcesso");
	    	              this.OnDataAcessoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DataCadastro
	    partial void OnDataCadastroChanging(System.DateTime value);
	    partial void OnDataCadastroChanged();

	    private System.DateTime _DataCadastro;

	    [DataMember(IsRequired = true, Name = "DataCadastro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Data Cadastro", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_SUPORTE_ACESSO_LOG.DATA_CADASTRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_SUPORTE_ACESSO_LOG.DATA_CADASTRO")]
	    public System.DateTime DataCadastro
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
	    //Extensibility Partial Method Definitions For IdTcsSuporteAcessoLog
	    partial void OnIdTcsSuporteAcessoLogChanging(Int32 value);
	    partial void OnIdTcsSuporteAcessoLogChanged();

	    private Int32 _IdTcsSuporteAcessoLog;

	    [DataMember(IsRequired = true, Name = "IdTcsSuporteAcessoLog", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Suporte Acesso Log", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_SUPORTE_ACESSO_LOG.ID_TCS_SUPORTE_ACESSO_LOG];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_SUPORTE_ACESSO_LOG.ID_TCS_SUPORTE_ACESSO_LOG")]
	    public Int32 IdTcsSuporteAcessoLog
	    {
	    	    get
	    	    {
	    	          return _IdTcsSuporteAcessoLog;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsSuporteAcessoLog != value)
	    	          {
	    	              this.ValidateProperty("IdTcsSuporteAcessoLog", value);
	    	              this.OnIdTcsSuporteAcessoLogChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsSuporteAcessoLog");
	    	              this._IdTcsSuporteAcessoLog = value;
	    	              this.RaiseDataMemberChanged("IdTcsSuporteAcessoLog");
	    	              this.OnIdTcsSuporteAcessoLogChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsUsuarioAcesso
	    partial void OnIdTcsUsuarioAcessoChanging(Int32 value);
	    partial void OnIdTcsUsuarioAcessoChanged();

	    private Int32 _IdTcsUsuarioAcesso;

	    [DataMember(IsRequired = true, Name = "IdTcsUsuarioAcesso", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Acesso", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_SUPORTE_ACESSO_LOG.TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_SUPORTE_ACESSO_LOG.TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO")]
	    public Int32 IdTcsUsuarioAcesso
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
	    //Extensibility Partial Method Definitions For IdUsuarioAcesso
	    partial void OnIdUsuarioAcessoChanging(Int64 value);
	    partial void OnIdUsuarioAcessoChanged();

	    private Int64 _IdUsuarioAcesso;

	    [DataMember(IsRequired = true, Name = "IdUsuarioAcesso", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario1", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_SUPORTE_ACESSO_LOG.USUARIO_ACESSO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_SUPORTE_ACESSO_LOG.USUARIO_ACESSO.ID_USUARIO")]
	    public Int64 IdUsuarioAcesso
	    {
	    	    get
	    	    {
	    	          return _IdUsuarioAcesso;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdUsuarioAcesso != value)
	    	          {
	    	              this.ValidateProperty("IdUsuarioAcesso", value);
	    	              this.OnIdUsuarioAcessoChanging(value);
	    	              this.RaiseDataMemberChanging("IdUsuarioAcesso");
	    	              this._IdUsuarioAcesso = value;
	    	              this.RaiseDataMemberChanged("IdUsuarioAcesso");
	    	              this.OnIdUsuarioAcessoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuarioSuporte
	    partial void OnIdUsuarioSuporteChanging(System.Nullable<Int64> value);
	    partial void OnIdUsuarioSuporteChanged();

	    private System.Nullable<Int64> _IdUsuarioSuporte;

	    [DataMember(Name = "IdUsuarioSuporte", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_SUPORTE_ACESSO_LOG.USUARIO_SUPORTE.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_SUPORTE_ACESSO_LOG.USUARIO_SUPORTE.ID_USUARIO")]
	    public System.Nullable<Int64> IdUsuarioSuporte
	    {
	    	    get
	    	    {
	    	          return _IdUsuarioSuporte;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdUsuarioSuporte != value)
	    	          {
	    	              this.ValidateProperty("IdUsuarioSuporte", value);
	    	              this.OnIdUsuarioSuporteChanging(value);
	    	              this.RaiseDataMemberChanging("IdUsuarioSuporte");
	    	              this._IdUsuarioSuporte = value;
	    	              this.RaiseDataMemberChanged("IdUsuarioSuporte");
	    	              this.OnIdUsuarioSuporteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeAutenticacaoAcesso
	    partial void OnNomeAutenticacaoAcessoChanging(System.String value);
	    partial void OnNomeAutenticacaoAcessoChanged();

	    private System.String _NomeAutenticacaoAcesso;

	    [DataMember(IsRequired = true, Name = "NomeAutenticacaoAcesso", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Autenticacao", Description="", Order = 20, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_SUPORTE_ACESSO_LOG.USUARIO_ACESSO.NOME_AUTENTICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_SUPORTE_ACESSO_LOG.USUARIO_ACESSO.NOME_AUTENTICACAO")]
	    public System.String NomeAutenticacaoAcesso
	    {
	    	    get
	    	    {
	    	          return _NomeAutenticacaoAcesso;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeAutenticacaoAcesso != value)
	    	          {
	    	              this.ValidateProperty("NomeAutenticacaoAcesso", value);
	    	              this.OnNomeAutenticacaoAcessoChanging(value);
	    	              this.RaiseDataMemberChanging("NomeAutenticacaoAcesso");
	    	              this._NomeAutenticacaoAcesso = value;
	    	              this.RaiseDataMemberChanged("NomeAutenticacaoAcesso");
	    	              this.OnNomeAutenticacaoAcessoChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIdTcsSuporteAcessoLog;
	    [DataMember(Name = "TemporaryIdTcsSuporteAcessoLog", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Suporte Acesso Log (Tmp)", Description="Temporary Key", Order = 4, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdTcsSuporteAcessoLog
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTcsSuporteAcessoLog.IsNullOrEmpty())
	    	                this._TemporaryIdTcsSuporteAcessoLog = this._IdTcsSuporteAcessoLog;
	    	          return this._TemporaryIdTcsSuporteAcessoLog;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTcsSuporteAcessoLog != value)
	    	              this._TemporaryIdTcsSuporteAcessoLog = value;
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_SUPORTE_ACESSO_LOG").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = true, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_SUPORTE_ACESSO_LOG), QualifiedEntitySetName = "AutorizacaoContext.TCS_SUPORTE_ACESSO_LOG" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_SUPORTE_ACESSO_LOG.DATA_ACESSO", Source = "DataAcesso", Target = "DATA_ACESSO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_SUPORTE_ACESSO_LOG", RelationPropertyName = "TCS_SUPORTE_ACESSO_LOG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_SUPORTE_ACESSO_LOG.DATA_CADASTRO", Source = "DataCadastro", Target = "DATA_CADASTRO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_SUPORTE_ACESSO_LOG", RelationPropertyName = "TCS_SUPORTE_ACESSO_LOG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_SUPORTE_ACESSO_LOG.ACESSO_EXPIRADO", Source = "AcessoExpirado", Target = "ACESSO_EXPIRADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_SUPORTE_ACESSO_LOG", RelationPropertyName = "TCS_SUPORTE_ACESSO_LOG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_SUPORTE_ACESSO_LOG.ID_TCS_SUPORTE_ACESSO_LOG", Source = "IdTcsSuporteAcessoLog", Target = "ID_TCS_SUPORTE_ACESSO_LOG", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_SUPORTE_ACESSO_LOG", RelationPropertyName = "TCS_SUPORTE_ACESSO_LOG" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_SUPORTE_ACESSO_LOG.USUARIO_ACESSO.ID_USUARIO", Source = "IdUsuarioAcesso", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "USUARIO_ACESSO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_SUPORTE_ACESSO_LOG.USUARIO_SUPORTE.ID_USUARIO", Source = "IdUsuarioSuporte", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "USUARIO_SUPORTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_SUPORTE_ACESSO_LOG.TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO", Source = "IdTcsUsuarioAcesso", Target = "ID_TCS_USUARIO_ACESSO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_ACESSO", RelationPropertyName = "TCS_USUARIO_ACESSO" });

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

		

	[LinxPublicationView(PrimaryKeys="RequisicaoSuporte.EntityUniqueKey", IsUpdatable=false, EdmName="")]
		
	[DataContract(IsReference = false, Name = "RequisicaoSuporte")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.UsuarioAutorizacao.RequisicaoSuporte")]
	public partial class RequisicaoSuporte 
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
	 


	    private Guid? _UidUsuario;

	    [DataMember(Name = "UidUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public Guid? UidUsuario
	    {
	    	    get
	    	    {
	    	          return _UidUsuario;
	    	    }
	    	    set
	    	    {
	    	          this._UidUsuario = value;
	    	    }
	    }

	    private int? _IdTcsAmbiente;

	    [DataMember(Name = "IdTcsAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public int? IdTcsAmbiente
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

	    private string _UrlPortal;

	    [DataMember(Name = "UrlPortal", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string UrlPortal
	    {
	    	    get
	    	    {
	    	          if (_UrlPortal.IsNullOrEmpty())
	    	             _UrlPortal =  String.Empty;
	    	          return _UrlPortal;
	    	    }
	    	    set
	    	    {
	    	          this._UrlPortal = value;
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

		

	[LinxPublicationView(PrimaryKeys="TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsUsuarioAcessoAmbiente];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsUsuarioAcesso];ReadOnly[false];Entities[TCS_USUARIO_ACESSO:IdTcsUsuarioAcesso|TCS_AMBIENTE:IdTcsAmbiente|TCS_AMBIENTE:IdTcsAmbienteRelacionado];SubQueryInfo[];EdmEntityName[TCS_USUARIO_ACESSO];EntityRelations[TCS_USUARIO_AUTENTICACAO(TCS_USUARIO_AUTENTICACAO)#TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)#TCS_AMBIENTE(TCS_AMBIENTE)#TCS_APLICACAO(TCS_APLICACAO)#TCS_APLICATIVO(TCS_APLICATIVO)#TCS_AMBIENTE1(TCS_AMBIENTE)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioAcessoAmbiente")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAcessoAmbiente")]
	public partial class TcsUsuarioAcessoAmbiente : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For DescricaoAmbiente
	    partial void OnDescricaoAmbienteChanging(System.String value);
	    partial void OnDescricaoAmbienteChanged();

	    private System.String _DescricaoAmbiente;

	    [DataMember(IsRequired = true, Name = "DescricaoAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descricao Ambiente", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.DESCRICAO_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.DESCRICAO_AMBIENTE")]
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

	    [DataMember(IsRequired = true, Name = "DescricaoAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descricao Aplicacao", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.DESCRICAO_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.DESCRICAO_APLICACAO")]
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
	    //Extensibility Partial Method Definitions For DescricaoAplicativo
	    partial void OnDescricaoAplicativoChanging(System.String value);
	    partial void OnDescricaoAplicativoChanged();

	    private System.String _DescricaoAplicativo;

	    [DataMember(Name = "DescricaoAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descricao Aplicativo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO")]
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
	    //Extensibility Partial Method Definitions For EmDesenvolvimento
	    partial void OnEmDesenvolvimentoChanging(Boolean value);
	    partial void OnEmDesenvolvimentoChanged();

	    private Boolean _EmDesenvolvimento;

	    [DataMember(IsRequired = true, Name = "EmDesenvolvimento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Em Desenvolvimento", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.EM_DESENVOLVIMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.EM_DESENVOLVIMENTO")]
	    public Boolean EmDesenvolvimento
	    {
	    	    get
	    	    {
	    	          return _EmDesenvolvimento;
	    	    }
	    	    set
	    	    {
	    	          if (this._EmDesenvolvimento != value)
	    	          {
	    	              this.ValidateProperty("EmDesenvolvimento", value);
	    	              this.OnEmDesenvolvimentoChanging(value);
	    	              this.RaiseDataMemberChanging("EmDesenvolvimento");
	    	              this._EmDesenvolvimento = value;
	    	              this.RaiseDataMemberChanged("EmDesenvolvimento");
	    	              this.OnEmDesenvolvimentoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For GrupoEconomico
	    partial void OnGrupoEconomicoChanging(System.String value);
	    partial void OnGrupoEconomicoChanged();

	    private System.String _GrupoEconomico;

	    [DataMember(IsRequired = true, Name = "GrupoEconomico", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "GrupoEconomico", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
	    public System.String GrupoEconomico
	    {
	    	    get
	    	    {
	    	          return _GrupoEconomico;
	    	    }
	    	    set
	    	    {
	    	          if (this._GrupoEconomico != value)
	    	          {
	    	              this.ValidateProperty("GrupoEconomico", value);
	    	              this.OnGrupoEconomicoChanging(value);
	    	              this.RaiseDataMemberChanging("GrupoEconomico");
	    	              this._GrupoEconomico = value;
	    	              this.RaiseDataMemberChanged("GrupoEconomico");
	    	              this.OnGrupoEconomicoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLinxGpecon
	    partial void OnIdLinxGpeconChanging(Int32 value);
	    partial void OnIdLinxGpeconChanged();

	    private Int32 _IdLinxGpecon;

	    [DataMember(IsRequired = true, Name = "IdLinxGpecon", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "IdLinxGpecon", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
	    public Int32 IdLinxGpecon
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
	    partial void OnIdTcsAmbienteChanging(Int32 value);
	    partial void OnIdTcsAmbienteChanged();

	    private Int32 _IdTcsAmbiente;

	    [DataMember(IsRequired = true, Name = "IdTcsAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.ID_TCS_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.ID_TCS_AMBIENTE")]
	    public Int32 IdTcsAmbiente
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
	    //Extensibility Partial Method Definitions For IdTcsAmbienteRelacionado
	    partial void OnIdTcsAmbienteRelacionadoChanging(System.Nullable<Int32> value);
	    partial void OnIdTcsAmbienteRelacionadoChanged();

	    private System.Nullable<Int32> _IdTcsAmbienteRelacionado;

	    [DataMember(Name = "IdTcsAmbienteRelacionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente Relacionado", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE1.ID_TCS_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.ID_TCS_AMBIENTE")]
	    public System.Nullable<Int32> IdTcsAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _IdTcsAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAmbienteRelacionado != value)
	    	          {
	    	              this.ValidateProperty("IdTcsAmbienteRelacionado", value);
	    	              this.OnIdTcsAmbienteRelacionadoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsAmbienteRelacionado");
	    	              this._IdTcsAmbienteRelacionado = value;
	    	              this.RaiseDataMemberChanged("IdTcsAmbienteRelacionado");
	    	              this.OnIdTcsAmbienteRelacionadoChanged();
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
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO")]
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
	    //Extensibility Partial Method Definitions For IdTcsUsuarioAcesso
	    partial void OnIdTcsUsuarioAcessoChanging(Int32 value);
	    partial void OnIdTcsUsuarioAcessoChanged();

	    private Int32 _IdTcsUsuarioAcesso;

	    [DataMember(IsRequired = true, Name = "IdTcsUsuarioAcesso", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Acesso", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO")]
	    public Int32 IdTcsUsuarioAcesso
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
	    partial void OnIdUsuarioChanging(Int64 value);
	    partial void OnIdUsuarioChanged();

	    private Int64 _IdUsuario;

	    [DataMember(IsRequired = true, Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.ID_USUARIO")]
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
	    //Extensibility Partial Method Definitions For IndicaAcessoPadrao
	    partial void OnIndicaAcessoPadraoChanging(Boolean value);
	    partial void OnIndicaAcessoPadraoChanged();

	    private Boolean _IndicaAcessoPadrao;

	    [DataMember(IsRequired = true, Name = "IndicaAcessoPadrao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Indica Acesso Padrao", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.INDICA_ACESSO_PADRAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.INDICA_ACESSO_PADRAO")]
	    public Boolean IndicaAcessoPadrao
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
	    partial void OnIndicaAdministradorChanging(Boolean value);
	    partial void OnIndicaAdministradorChanged();

	    private Boolean _IndicaAdministrador;

	    [DataMember(IsRequired = true, Name = "IndicaAdministrador", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Indica Administrador", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.INDICA_ADMINISTRADOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.INDICA_ADMINISTRADOR")]
	    public Boolean IndicaAdministrador
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
	    //Extensibility Partial Method Definitions For NomeEmpresa
	    partial void OnNomeEmpresaChanging(System.String value);
	    partial void OnNomeEmpresaChanged();

	    private System.String _NomeEmpresa;

	    [DataMember(IsRequired = true, Name = "NomeEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Empresa", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
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
	    //Extensibility Partial Method Definitions For UidAplicacao
	    partial void OnUidAplicacaoChanging(System.Guid value);
	    partial void OnUidAplicacaoChanged();

	    private System.Guid _UidAplicacao;

	    [DataMember(IsRequired = true, Name = "UidAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Aplicacao", Description="", Order = 4, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.UID_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.UID_APLICACAO")]
	    public System.Guid UidAplicacao
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
	    partial void OnUidEmpresaChanging(System.Guid value);
	    partial void OnUidEmpresaChanged();

	    private System.Guid _UidEmpresa;

	    [DataMember(IsRequired = true, Name = "UidEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Empresa", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA")]
	    public System.Guid UidEmpresa
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
	    //Extensibility Partial Method Definitions For UidGrupoEconomico
	    partial void OnUidGrupoEconomicoChanging(System.Guid value);
	    partial void OnUidGrupoEconomicoChanged();

	    private System.Guid _UidGrupoEconomico;

	    [DataMember(IsRequired = true, Name = "UidGrupoEconomico", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Grupo Economico", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA")]
	    public System.Guid UidGrupoEconomico
	    {
	    	    get
	    	    {
	    	          return _UidGrupoEconomico;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidGrupoEconomico != value)
	    	          {
	    	              this.ValidateProperty("UidGrupoEconomico", value);
	    	              this.OnUidGrupoEconomicoChanging(value);
	    	              this.RaiseDataMemberChanging("UidGrupoEconomico");
	    	              this._UidGrupoEconomico = value;
	    	              this.RaiseDataMemberChanged("UidGrupoEconomico");
	    	              this.OnUidGrupoEconomicoChanged();
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
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.UID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.UID_USUARIO")]
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
	    //Extensibility Partial Method Definitions For Url
	    partial void OnUrlChanging(System.String value);
	    partial void OnUrlChanged();

	    private System.String _Url;

	    [DataMember(Name = "Url", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Url", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.URL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.URL")]
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
	    //Extensibility Partial Method Definitions For UrlWorkArea
	    partial void OnUrlWorkAreaChanging(System.String value);
	    partial void OnUrlWorkAreaChanged();

	    private System.String _UrlWorkArea;

	    [DataMember(Name = "UrlWorkArea", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Url Work Area", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.URL_WORK_AREA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.URL_WORK_AREA")]
	    public System.String UrlWorkArea
	    {
	    	    get
	    	    {
	    	          return _UrlWorkArea;
	    	    }
	    	    set
	    	    {
	    	          if (this._UrlWorkArea != value)
	    	          {
	    	              this.ValidateProperty("UrlWorkArea", value);
	    	              this.OnUrlWorkAreaChanging(value);
	    	              this.RaiseDataMemberChanging("UrlWorkArea");
	    	              this._UrlWorkArea = value;
	    	              this.RaiseDataMemberChanged("UrlWorkArea");
	    	              this.OnUrlWorkAreaChanged();
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
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO")]
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
	    [Display(Name = "Nome", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO")]
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

	    private Int32 _TemporaryIdTcsUsuarioAcesso;
	    [DataMember(Name = "TemporaryIdTcsUsuarioAcesso", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Acesso (Tmp)", Description="Temporary Key", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdTcsUsuarioAcesso
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTcsUsuarioAcesso.IsNullOrEmpty())
	    	                this._TemporaryIdTcsUsuarioAcesso = this._IdTcsUsuarioAcesso;
	    	          return this._TemporaryIdTcsUsuarioAcesso;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTcsUsuarioAcesso != value)
	    	              this._TemporaryIdTcsUsuarioAcesso = value;
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

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_ACESSO.INDICA_ACESSO_PADRAO", Source = "IndicaAcessoPadrao", Target = "INDICA_ACESSO_PADRAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_ACESSO", RelationPropertyName = "TCS_USUARIO_ACESSO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_ACESSO.INDICA_ADMINISTRADOR", Source = "IndicaAdministrador", Target = "INDICA_ADMINISTRADOR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_ACESSO", RelationPropertyName = "TCS_USUARIO_ACESSO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO", Source = "IdTcsUsuarioAcesso", Target = "ID_TCS_USUARIO_ACESSO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_ACESSO", RelationPropertyName = "TCS_USUARIO_ACESSO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_ACESSO.TCS_AMBIENTE.ID_TCS_AMBIENTE", Source = "IdTcsAmbiente", Target = "ID_TCS_AMBIENTE", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE", RelationPropertyName = "TCS_AMBIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.ID_TCS_AMBIENTE", Source = "IdTcsAmbienteRelacionado", Target = "ID_TCS_AMBIENTE", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE", RelationPropertyName = "TCS_AMBIENTE1" });
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

		

	[LinxPublicationView(PrimaryKeys="TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO", IsUpdatable=true, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Acessos];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsUsuarioAutenticacaoAcessoP];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsUsuarioAcesso];ReadOnly[false];Entities[TCS_USUARIO_ACESSO:IdTcsUsuarioAcesso|TCS_AMBIENTE:IdTcsAmbiente|TCS_AMBIENTE:IdTcsAmbienteRelacionado];SubQueryInfo[];EdmEntityName[TCS_USUARIO_ACESSO];EntityRelations[TCS_USUARIO_AUTENTICACAO(TCS_USUARIO_AUTENTICACAO)#TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)#TCS_AMBIENTE(TCS_AMBIENTE)#TCS_APLICACAO(TCS_APLICACAO)#TCS_APLICATIVO(TCS_APLICATIVO)#TCS_AMBIENTE1(TCS_AMBIENTE)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioAutenticacaoAcessoP")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacaoAcessoP")]
	public partial class TcsUsuarioAutenticacaoAcessoP : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For DescricaoAmbiente
	    partial void OnDescricaoAmbienteChanging(System.String value);
	    partial void OnDescricaoAmbienteChanged();

	    private System.String _DescricaoAmbiente;

	    [DataMember(IsRequired = true, Name = "DescricaoAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ambiente", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente2];LookUpTitle[Seleção de (Ambiente)];LookUpQuery[executeLookUpTcsAmbiente2];LookUpFinalize[finalizeLookUpTcsAmbiente2];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"DescricaoAplicativo\" : \"Aplicativo\", \"NomeEmpresa\" : \"Empresa\", \"DescricaoAplicacao\" : \"Aplicação\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\", \"IdAplicacao\" : \"Id Aplicacao\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\", \"IdLinx\" : \"Id Linx\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"DescricaoAplicativo\" : true, \"NomeEmpresa\" : true, \"DescricaoAplicacao\" : false, \"IdTcsAmbiente\" : false, \"IdAplicacao\" : false, \"IdTcsAplicativo\" : false, \"IdLinx\" : true}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.DESCRICAO_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoAmbiente#false##2500##Ambiente#0#true##::LookUpTcsAmbiente2##true#false###Linx.Framework.BV.UsuarioAutorizacao#IQueryable###true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.DESCRICAO_AMBIENTE")]
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
	    //Extensibility Partial Method Definitions For DescricaoAmbienteRelacionado
	    partial void OnDescricaoAmbienteRelacionadoChanging(System.String value);
	    partial void OnDescricaoAmbienteRelacionadoChanged();

	    private System.String _DescricaoAmbienteRelacionado;

	    [DataMember(Name = "DescricaoAmbienteRelacionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ambiente Relacionado", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente2Relacionado];LookUpTitle[Seleção de (Ambiente Relacionado)];LookUpQuery[executeLookUpTcsAmbiente2Relacionado];LookUpFinalize[finalizeLookUpTcsAmbiente2Relacionado];LookUpDisplayColumns[{\"DescricaoAmbienteRelacionado\" : \"Ambiente\", \"IdTcsAmbienteRelacionado\" : \"Id Tcs Ambiente\"}];LookUpColumns[{\"DescricaoAmbienteRelacionado\" : true, \"IdTcsAmbienteRelacionado\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE1.DESCRICAO_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoAmbienteRelacionado#false##2500##Ambiente#0#true##::LookUpTcsAmbiente2Relacionado##false#false###Linx.Framework.BV.UsuarioAutorizacao#IQueryable###true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.DESCRICAO_AMBIENTE")]
	    public System.String DescricaoAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _DescricaoAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescricaoAmbienteRelacionado != value)
	    	          {
	    	              this.ValidateProperty("DescricaoAmbienteRelacionado", value);
	    	              this.OnDescricaoAmbienteRelacionadoChanging(value);
	    	              this.RaiseDataMemberChanging("DescricaoAmbienteRelacionado");
	    	              this._DescricaoAmbienteRelacionado = value;
	    	              this.RaiseDataMemberChanged("DescricaoAmbienteRelacionado");
	    	              this.OnDescricaoAmbienteRelacionadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescricaoAplicacao
	    partial void OnDescricaoAplicacaoChanging(System.String value);
	    partial void OnDescricaoAplicacaoChanged();

	    private System.String _DescricaoAplicacao;

	    [DataMember(IsRequired = true, Name = "DescricaoAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descricao Aplicacao", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente2];LookUpTitle[Seleção de (Descricao Aplicacao)];LookUpQuery[executeLookUpTcsAmbiente2];LookUpFinalize[finalizeLookUpTcsAmbiente2];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"DescricaoAplicativo\" : \"Aplicativo\", \"NomeEmpresa\" : \"Empresa\", \"DescricaoAplicacao\" : \"Aplicação\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\", \"IdAplicacao\" : \"Id Aplicacao\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\", \"IdLinx\" : \"Id Linx\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"DescricaoAplicativo\" : true, \"NomeEmpresa\" : true, \"DescricaoAplicacao\" : false, \"IdTcsAmbiente\" : false, \"IdAplicacao\" : false, \"IdTcsAplicativo\" : false, \"IdLinx\" : true}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.DESCRICAO_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoAplicacao#false##600##Aplicação#3#false##::LookUpTcsAmbiente2##true#false###Linx.Framework.BV.UsuarioAutorizacao#IQueryable###true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.DESCRICAO_APLICACAO")]
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
	    //Extensibility Partial Method Definitions For DescricaoAplicativo
	    partial void OnDescricaoAplicativoChanging(System.String value);
	    partial void OnDescricaoAplicativoChanged();

	    private System.String _DescricaoAplicativo;

	    [DataMember(Name = "DescricaoAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicativo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente2];LookUpTitle[Seleção de (Aplicativo)];LookUpQuery[executeLookUpTcsAmbiente2];LookUpFinalize[finalizeLookUpTcsAmbiente2];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"DescricaoAplicativo\" : \"Aplicativo\", \"NomeEmpresa\" : \"Empresa\", \"DescricaoAplicacao\" : \"Aplicação\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\", \"IdAplicacao\" : \"Id Aplicacao\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\", \"IdLinx\" : \"Id Linx\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"DescricaoAplicativo\" : true, \"NomeEmpresa\" : true, \"DescricaoAplicacao\" : false, \"IdTcsAmbiente\" : false, \"IdAplicacao\" : false, \"IdTcsAplicativo\" : false, \"IdLinx\" : true}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoAplicativo#false##2500##Aplicativo#1#true##::LookUpTcsAmbiente2##true#false###Linx.Framework.BV.UsuarioAutorizacao#IQueryable###true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO")]
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
	    //Extensibility Partial Method Definitions For IdAplicacao
	    partial void OnIdAplicacaoChanging(Int32 value);
	    partial void OnIdAplicacaoChanged();

	    private Int32 _IdAplicacao;

	    [DataMember(IsRequired = true, Name = "IdAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Aplicacao", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente2];LookUpTitle[Seleção de (Id Aplicacao)];LookUpQuery[executeLookUpTcsAmbiente2];LookUpFinalize[finalizeLookUpTcsAmbiente2];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"DescricaoAplicativo\" : \"Aplicativo\", \"NomeEmpresa\" : \"Empresa\", \"DescricaoAplicacao\" : \"Aplicação\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\", \"IdAplicacao\" : \"Id Aplicacao\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\", \"IdLinx\" : \"Id Linx\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"DescricaoAplicativo\" : true, \"NomeEmpresa\" : true, \"DescricaoAplicacao\" : false, \"IdTcsAmbiente\" : false, \"IdAplicacao\" : false, \"IdTcsAplicativo\" : false, \"IdLinx\" : true}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.ID_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdAplicacao#false##12:0##Id Aplicacao#5#false##::LookUpTcsAmbiente2##true#false###Linx.Framework.BV.UsuarioAutorizacao#IQueryable###true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.ID_APLICACAO")]
	    public Int32 IdAplicacao
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
	    partial void OnIdLinxChanging(Int32 value);
	    partial void OnIdLinxChanged();

	    private Int32 _IdLinx;

	    [DataMember(IsRequired = true, Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente2];LookUpTitle[Seleção de (Id Linx)];LookUpQuery[executeLookUpTcsAmbiente2];LookUpFinalize[finalizeLookUpTcsAmbiente2];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"DescricaoAplicativo\" : \"Aplicativo\", \"NomeEmpresa\" : \"Empresa\", \"DescricaoAplicacao\" : \"Aplicação\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\", \"IdAplicacao\" : \"Id Aplicacao\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\", \"IdLinx\" : \"Id Linx\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"DescricaoAplicativo\" : true, \"NomeEmpresa\" : true, \"DescricaoAplicacao\" : false, \"IdTcsAmbiente\" : false, \"IdAplicacao\" : false, \"IdTcsAplicativo\" : false, \"IdLinx\" : true}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdLinx#false##12:0##Id Linx#7#true##::LookUpTcsAmbiente2##true#false###Linx.Framework.BV.UsuarioAutorizacao#IQueryable###true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
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
	    //Extensibility Partial Method Definitions For IdTcsAmbiente
	    partial void OnIdTcsAmbienteChanging(Int32 value);
	    partial void OnIdTcsAmbienteChanged();

	    private Int32 _IdTcsAmbiente;

	    [DataMember(IsRequired = true, Name = "IdTcsAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente2];LookUpTitle[Seleção de (Id Tcs Ambiente)];LookUpQuery[executeLookUpTcsAmbiente2];LookUpFinalize[finalizeLookUpTcsAmbiente2];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"DescricaoAplicativo\" : \"Aplicativo\", \"NomeEmpresa\" : \"Empresa\", \"DescricaoAplicacao\" : \"Aplicação\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\", \"IdAplicacao\" : \"Id Aplicacao\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\", \"IdLinx\" : \"Id Linx\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"DescricaoAplicativo\" : true, \"NomeEmpresa\" : true, \"DescricaoAplicacao\" : false, \"IdTcsAmbiente\" : false, \"IdAplicacao\" : false, \"IdTcsAplicativo\" : false, \"IdLinx\" : true}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.ID_TCS_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdTcsAmbiente#true##12:0##Id Tcs Ambiente#4#false##::LookUpTcsAmbiente2##true#false###Linx.Framework.BV.UsuarioAutorizacao#IQueryable###true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.ID_TCS_AMBIENTE")]
	    public Int32 IdTcsAmbiente
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
	    //Extensibility Partial Method Definitions For IdTcsAmbienteRelacionado
	    partial void OnIdTcsAmbienteRelacionadoChanging(System.Nullable<Int32> value);
	    partial void OnIdTcsAmbienteRelacionadoChanged();

	    private System.Nullable<Int32> _IdTcsAmbienteRelacionado;

	    [DataMember(Name = "IdTcsAmbienteRelacionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ambiente Relacionado", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente2Relacionado];LookUpTitle[Seleção de (Ambiente Relacionado)];LookUpQuery[executeLookUpTcsAmbiente2Relacionado];LookUpFinalize[finalizeLookUpTcsAmbiente2Relacionado];LookUpDisplayColumns[{\"DescricaoAmbienteRelacionado\" : \"Ambiente\", \"IdTcsAmbienteRelacionado\" : \"Id Tcs Ambiente\"}];LookUpColumns[{\"DescricaoAmbienteRelacionado\" : true, \"IdTcsAmbienteRelacionado\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE1.ID_TCS_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdTcsAmbienteRelacionado#true##12:0##Id Tcs Ambiente#1#false##::LookUpTcsAmbiente2Relacionado##false#false###Linx.Framework.BV.UsuarioAutorizacao#IQueryable###true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.ID_TCS_AMBIENTE")]
	    public System.Nullable<Int32> IdTcsAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _IdTcsAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAmbienteRelacionado != value)
	    	          {
	    	              this.ValidateProperty("IdTcsAmbienteRelacionado", value);
	    	              this.OnIdTcsAmbienteRelacionadoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsAmbienteRelacionado");
	    	              this._IdTcsAmbienteRelacionado = value;
	    	              this.RaiseDataMemberChanged("IdTcsAmbienteRelacionado");
	    	              this.OnIdTcsAmbienteRelacionadoChanged();
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
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente2];LookUpTitle[Seleção de (Id Tcs Aplicativo)];LookUpQuery[executeLookUpTcsAmbiente2];LookUpFinalize[finalizeLookUpTcsAmbiente2];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"DescricaoAplicativo\" : \"Aplicativo\", \"NomeEmpresa\" : \"Empresa\", \"DescricaoAplicacao\" : \"Aplicação\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\", \"IdAplicacao\" : \"Id Aplicacao\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\", \"IdLinx\" : \"Id Linx\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"DescricaoAplicativo\" : true, \"NomeEmpresa\" : true, \"DescricaoAplicacao\" : false, \"IdTcsAmbiente\" : false, \"IdAplicacao\" : false, \"IdTcsAplicativo\" : false, \"IdLinx\" : true}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdTcsAplicativo#false##12:0##Id Tcs Aplicativo#6#false##::LookUpTcsAmbiente2##true#false###Linx.Framework.BV.UsuarioAutorizacao#IQueryable###true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO")]
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
	    //Extensibility Partial Method Definitions For IdTcsUsuarioAcesso
	    partial void OnIdTcsUsuarioAcessoChanging(Int32 value);
	    partial void OnIdTcsUsuarioAcessoChanged();

	    private Int32 _IdTcsUsuarioAcesso;

	    [DataMember(IsRequired = true, Name = "IdTcsUsuarioAcesso", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Acesso", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO")]
	    public Int32 IdTcsUsuarioAcesso
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
	    partial void OnIdUsuarioChanging(Int64 value);
	    partial void OnIdUsuarioChanged();

	    private Int64 _IdUsuario;

	    [DataMember(IsRequired = true, Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.ID_USUARIO")]
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
	    //Extensibility Partial Method Definitions For IndicaAcessoPadrao
	    partial void OnIndicaAcessoPadraoChanging(Boolean value);
	    partial void OnIndicaAcessoPadraoChanged();

	    private Boolean _IndicaAcessoPadrao;

	    [DataMember(IsRequired = true, Name = "IndicaAcessoPadrao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Acesso Padrão", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.INDICA_ACESSO_PADRAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.INDICA_ACESSO_PADRAO")]
	    public Boolean IndicaAcessoPadrao
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
	    //Extensibility Partial Method Definitions For NomeEmpresa
	    partial void OnNomeEmpresaChanging(System.String value);
	    partial void OnNomeEmpresaChanged();

	    private System.String _NomeEmpresa;

	    [DataMember(IsRequired = true, Name = "NomeEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Empresa", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente2];LookUpTitle[Seleção de (Empresa)];LookUpQuery[executeLookUpTcsAmbiente2];LookUpFinalize[finalizeLookUpTcsAmbiente2];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"DescricaoAplicativo\" : \"Aplicativo\", \"NomeEmpresa\" : \"Empresa\", \"DescricaoAplicacao\" : \"Aplicação\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\", \"IdAplicacao\" : \"Id Aplicacao\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\", \"IdLinx\" : \"Id Linx\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"DescricaoAplicativo\" : true, \"NomeEmpresa\" : true, \"DescricaoAplicacao\" : false, \"IdTcsAmbiente\" : false, \"IdAplicacao\" : false, \"IdTcsAplicativo\" : false, \"IdLinx\" : true}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeEmpresa#false##2500##Empresa#2#true##::LookUpTcsAmbiente2##true#false###Linx.Framework.BV.UsuarioAutorizacao#IQueryable###true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
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
	    //Extensibility Partial Method Definitions For Perfil
	    partial void OnPerfilChanging(string value);
	    partial void OnPerfilChanged();

	    private string _Perfil;

	    [DataMember(IsRequired = true, Name = "Perfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[''];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="\"\"")]
	    public string Perfil
	    {
	    	    get
	    	    {
	    	          return _Perfil;
	    	    }
	    	    set
	    	    {
	    	          if (this._Perfil != value)
	    	          {
	    	              this.ValidateProperty("Perfil", value);
	    	              this.OnPerfilChanging(value);
	    	              this.RaiseDataMemberChanging("Perfil");
	    	              this._Perfil = value;
	    	              this.RaiseDataMemberChanged("Perfil");
	    	              this.OnPerfilChanged();
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
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO")]
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
	    [Display(Name = "Nome", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO")]
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

	    private Int32 _TemporaryIdTcsUsuarioAcesso;
	    [DataMember(Name = "TemporaryIdTcsUsuarioAcesso", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Acesso (Tmp)", Description="Temporary Key", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdTcsUsuarioAcesso
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTcsUsuarioAcesso.IsNullOrEmpty())
	    	                this._TemporaryIdTcsUsuarioAcesso = this._IdTcsUsuarioAcesso;
	    	          return this._TemporaryIdTcsUsuarioAcesso;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTcsUsuarioAcesso != value)
	    	              this._TemporaryIdTcsUsuarioAcesso = value;
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

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_ACESSO.INDICA_ACESSO_PADRAO", Source = "IndicaAcessoPadrao", Target = "INDICA_ACESSO_PADRAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_ACESSO", RelationPropertyName = "TCS_USUARIO_ACESSO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO", Source = "IdTcsUsuarioAcesso", Target = "ID_TCS_USUARIO_ACESSO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_ACESSO", RelationPropertyName = "TCS_USUARIO_ACESSO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_ACESSO.TCS_AMBIENTE.ID_TCS_AMBIENTE", Source = "IdTcsAmbiente", Target = "ID_TCS_AMBIENTE", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE", RelationPropertyName = "TCS_AMBIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.ID_TCS_AMBIENTE", Source = "IdTcsAmbienteRelacionado", Target = "ID_TCS_AMBIENTE", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE", RelationPropertyName = "TCS_AMBIENTE1" });
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

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Acessos];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsUsuarioAcesso];ReadOnly[false];Entities[TCS_USUARIO_ACESSO:IdTcsUsuarioAcesso|TCS_AMBIENTE:IdTcsAmbiente|TCS_AMBIENTE:IdTcsAmbienteRelacionado];SubQueryInfo[Select 1 From #ParentAlias#.TCS_USUARIO_ACESSO_LISTA as #Alias#];EdmEntityName[TCS_USUARIO_ACESSO];EntityRelations[TCS_USUARIO_AUTENTICACAO(TCS_USUARIO_AUTENTICACAO)#TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)#TCS_AMBIENTE(TCS_AMBIENTE)#TCS_APLICACAO(TCS_APLICACAO)#TCS_APLICATIVO(TCS_APLICATIVO)#TCS_AMBIENTE1(TCS_AMBIENTE)];EdmParentEntityName[TCS_USUARIO_AUTENTICACAO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioAcesso")]
	[Serializable()]
	public partial class TcsUsuarioAcessoParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For DescricaoAmbiente
	    partial void OnDescricaoAmbienteChanging(System.String value);
	    partial void OnDescricaoAmbienteChanged();

	    private System.String _DescricaoAmbiente;

	    [DataMember(IsRequired = true, Name = "DescricaoAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ambiente", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente];LookUpTitle[Seleção de (Ambiente)];LookUpQuery[executeLookUpTcsAmbiente];LookUpFinalize[finalizeLookUpTcsAmbiente];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"DescricaoAplicacao\" : \"Aplicação\", \"EmDesenvolvimento\" : \"Em Desenvolvimento\", \"IdLinxEmpresa\" : \"Id Linx\", \"DescricaoAplicativo\" : \"Aplicativo\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\", \"NomeEmpresa\" : \"Grupo Econômico\", \"UidAplicacao\" : \"Uid Aplicacao\", \"UidEmpresa\" : \"Uid Empresa\", \"Url\" : \"Url\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"DescricaoAplicacao\" : true, \"EmDesenvolvimento\" : true, \"IdLinxEmpresa\" : false, \"DescricaoAplicativo\" : true, \"IdTcsAmbiente\" : false, \"NomeEmpresa\" : true, \"UidAplicacao\" : false, \"UidEmpresa\" : false, \"Url\" : false, \"IdTcsAplicativo\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.DESCRICAO_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoAmbiente#false##2500##Ambiente#0#true##::LookUpTcsAmbiente##true#false#TCS_AMBIENTE#TCS_AMBIENTE#Linx.Framework.BV.UsuarioAutorizacao#IQueryable#DescricaoAplicacao,EmDesenvolvimento,UidAplicacao,Url[DescricaoAplicacao,EmDesenvolvimento,DescricaoAplicativo,UidAplicacao,Url,IdTcsAplicativo];DescricaoAplicativo,IdTcsAplicativo[DescricaoAplicativo,IdTcsAplicativo]#DescricaoAmbiente[DescricaoAplicacao=DescricaoAplicacao,EmDesenvolvimento=EmDesenvolvimento,IdLinxEmpresa=IdLinxEmpresa,DescricaoAplicativo=DescricaoAplicativo,NomeEmpresa=NomeEmpresa,UidAplicacao=UidAplicacao,UidEmpresa=UidEmpresa,Url=Url,IdTcsAplicativo=IdTcsAplicativo];DescricaoAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];EmDesenvolvimento[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdLinxEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdTcsAmbiente[DescricaoAplicacao=DescricaoAplicacao,EmDesenvolvimento=EmDesenvolvimento,IdLinxEmpresa=IdLinxEmpresa,DescricaoAplicativo=DescricaoAplicativo,NomeEmpresa=NomeEmpresa,UidAplicacao=UidAplicacao,UidEmpresa=UidEmpresa,Url=Url,IdTcsAplicativo=IdTcsAplicativo];NomeEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UidAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UidEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];Url[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo]#true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.DESCRICAO_AMBIENTE")]
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
	    //Extensibility Partial Method Definitions For DescricaoAmbienteRelacionado
	    partial void OnDescricaoAmbienteRelacionadoChanging(System.String value);
	    partial void OnDescricaoAmbienteRelacionadoChanged();

	    private System.String _DescricaoAmbienteRelacionado;

	    [DataMember(Name = "DescricaoAmbienteRelacionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ambiente Relacionado", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente1];LookUpTitle[Seleção de (Ambiente Relacionado)];LookUpQuery[executeLookUpTcsAmbiente1];LookUpFinalize[finalizeLookUpTcsAmbiente1];LookUpDisplayColumns[{\"DescricaoAmbienteRelacionado\" : \"Ambiente\", \"NomeEmpresaAmbienteRelacionado\" : \"Empresa\", \"DescricaoAplicacaoAmbienteRelacionado\" : \"Aplicação\", \"IdLinxAmbienteRelacionado\" : \"ID Linx\", \"IdTcsAmbienteRelacionado\" : \"Id Tcs Ambiente1\", \"IdAplicacao\" : \"Id Aplicacao\"}];LookUpColumns[{\"DescricaoAmbienteRelacionado\" : true, \"NomeEmpresaAmbienteRelacionado\" : true, \"DescricaoAplicacaoAmbienteRelacionado\" : true, \"IdLinxAmbienteRelacionado\" : false, \"IdTcsAmbienteRelacionado\" : false, \"IdAplicacao\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE1.DESCRICAO_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoAmbienteRelacionado#false##250:0##Ambiente#0#true##::LookUpTcsAmbiente1##false#false#TCS_AMBIENTE1#TCS_AMBIENTE#Linx.Framework.BV.UsuarioAutorizacao#IQueryable#NomeEmpresaAmbienteRelacionado,IdLinxAmbienteRelacionado[NomeEmpresaAmbienteRelacionado,IdLinxAmbienteRelacionado]#DescricaoAmbienteRelacionado[NomeEmpresaAmbienteRelacionado=NomeEmpresaAmbienteRelacionado,DescricaoAplicacaoAmbienteRelacionado=DescricaoAplicacaoAmbienteRelacionado,IdLinxAmbienteRelacionado=IdLinxAmbienteRelacionado,IdAplicacao=IdAplicacao];IdTcsAmbienteRelacionado[NomeEmpresaAmbienteRelacionado=NomeEmpresaAmbienteRelacionado,DescricaoAplicacaoAmbienteRelacionado=DescricaoAplicacaoAmbienteRelacionado,IdLinxAmbienteRelacionado=IdLinxAmbienteRelacionado,IdAplicacao=IdAplicacao]#true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.DESCRICAO_AMBIENTE")]
	    public System.String DescricaoAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _DescricaoAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescricaoAmbienteRelacionado != value)
	    	          {
	    	              this.ValidateProperty("DescricaoAmbienteRelacionado", value);
	    	              this.OnDescricaoAmbienteRelacionadoChanging(value);
	    	              this.RaiseDataMemberChanging("DescricaoAmbienteRelacionado");
	    	              this._DescricaoAmbienteRelacionado = value;
	    	              this.RaiseDataMemberChanged("DescricaoAmbienteRelacionado");
	    	              this.OnDescricaoAmbienteRelacionadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescricaoAplicacao
	    partial void OnDescricaoAplicacaoChanging(System.String value);
	    partial void OnDescricaoAplicacaoChanged();

	    private System.String _DescricaoAplicacao;

	    [DataMember(IsRequired = true, Name = "DescricaoAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicação", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente];LookUpTitle[Seleção de (Aplicação)];LookUpQuery[executeLookUpTcsAmbiente];LookUpFinalize[finalizeLookUpTcsAmbiente];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"DescricaoAplicacao\" : \"Aplicação\", \"EmDesenvolvimento\" : \"Em Desenvolvimento\", \"IdLinxEmpresa\" : \"Id Linx\", \"DescricaoAplicativo\" : \"Aplicativo\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\", \"NomeEmpresa\" : \"Grupo Econômico\", \"UidAplicacao\" : \"Uid Aplicacao\", \"UidEmpresa\" : \"Uid Empresa\", \"Url\" : \"Url\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"DescricaoAplicacao\" : true, \"EmDesenvolvimento\" : true, \"IdLinxEmpresa\" : false, \"DescricaoAplicativo\" : true, \"IdTcsAmbiente\" : false, \"NomeEmpresa\" : true, \"UidAplicacao\" : false, \"UidEmpresa\" : false, \"Url\" : false, \"IdTcsAplicativo\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.DESCRICAO_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoAplicacao#false##600##Aplicação#1#true##::LookUpTcsAmbiente##true#false#TCS_AMBIENTE#TCS_AMBIENTE#Linx.Framework.BV.UsuarioAutorizacao#IQueryable#DescricaoAplicacao,EmDesenvolvimento,UidAplicacao,Url[DescricaoAplicacao,EmDesenvolvimento,DescricaoAplicativo,UidAplicacao,Url,IdTcsAplicativo];DescricaoAplicativo,IdTcsAplicativo[DescricaoAplicativo,IdTcsAplicativo]#DescricaoAmbiente[DescricaoAplicacao=DescricaoAplicacao,EmDesenvolvimento=EmDesenvolvimento,IdLinxEmpresa=IdLinxEmpresa,DescricaoAplicativo=DescricaoAplicativo,NomeEmpresa=NomeEmpresa,UidAplicacao=UidAplicacao,UidEmpresa=UidEmpresa,Url=Url,IdTcsAplicativo=IdTcsAplicativo];DescricaoAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];EmDesenvolvimento[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdLinxEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdTcsAmbiente[DescricaoAplicacao=DescricaoAplicacao,EmDesenvolvimento=EmDesenvolvimento,IdLinxEmpresa=IdLinxEmpresa,DescricaoAplicativo=DescricaoAplicativo,NomeEmpresa=NomeEmpresa,UidAplicacao=UidAplicacao,UidEmpresa=UidEmpresa,Url=Url,IdTcsAplicativo=IdTcsAplicativo];NomeEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UidAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UidEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];Url[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo]#true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.DESCRICAO_APLICACAO")]
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
	    //Extensibility Partial Method Definitions For DescricaoAplicacaoAmbienteRelacionado
	    partial void OnDescricaoAplicacaoAmbienteRelacionadoChanging(System.String value);
	    partial void OnDescricaoAplicacaoAmbienteRelacionadoChanged();

	    private System.String _DescricaoAplicacaoAmbienteRelacionado;

	    [DataMember(Name = "DescricaoAplicacaoAmbienteRelacionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicação Ambiente Relacionado", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente1];LookUpTitle[Seleção de (Aplicação Ambiente Relacionado)];LookUpQuery[executeLookUpTcsAmbiente1];LookUpFinalize[finalizeLookUpTcsAmbiente1];LookUpDisplayColumns[{\"DescricaoAmbienteRelacionado\" : \"Ambiente\", \"NomeEmpresaAmbienteRelacionado\" : \"Empresa\", \"DescricaoAplicacaoAmbienteRelacionado\" : \"Aplicação\", \"IdLinxAmbienteRelacionado\" : \"ID Linx\", \"IdTcsAmbienteRelacionado\" : \"Id Tcs Ambiente1\", \"IdAplicacao\" : \"Id Aplicacao\"}];LookUpColumns[{\"DescricaoAmbienteRelacionado\" : true, \"NomeEmpresaAmbienteRelacionado\" : true, \"DescricaoAplicacaoAmbienteRelacionado\" : true, \"IdLinxAmbienteRelacionado\" : false, \"IdTcsAmbienteRelacionado\" : false, \"IdAplicacao\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE1.TCS_APLICACAO.DESCRICAO_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoAplicacaoAmbienteRelacionado#false##60:0##Aplicação#2#true##::LookUpTcsAmbiente1##false#false#TCS_AMBIENTE1#TCS_AMBIENTE#Linx.Framework.BV.UsuarioAutorizacao#IQueryable#NomeEmpresaAmbienteRelacionado,IdLinxAmbienteRelacionado[NomeEmpresaAmbienteRelacionado,IdLinxAmbienteRelacionado]#DescricaoAmbienteRelacionado[NomeEmpresaAmbienteRelacionado=NomeEmpresaAmbienteRelacionado,DescricaoAplicacaoAmbienteRelacionado=DescricaoAplicacaoAmbienteRelacionado,IdLinxAmbienteRelacionado=IdLinxAmbienteRelacionado,IdAplicacao=IdAplicacao];IdTcsAmbienteRelacionado[NomeEmpresaAmbienteRelacionado=NomeEmpresaAmbienteRelacionado,DescricaoAplicacaoAmbienteRelacionado=DescricaoAplicacaoAmbienteRelacionado,IdLinxAmbienteRelacionado=IdLinxAmbienteRelacionado,IdAplicacao=IdAplicacao]#true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.TCS_APLICACAO.DESCRICAO_APLICACAO")]
	    public System.String DescricaoAplicacaoAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _DescricaoAplicacaoAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescricaoAplicacaoAmbienteRelacionado != value)
	    	          {
	    	              this.ValidateProperty("DescricaoAplicacaoAmbienteRelacionado", value);
	    	              this.OnDescricaoAplicacaoAmbienteRelacionadoChanging(value);
	    	              this.RaiseDataMemberChanging("DescricaoAplicacaoAmbienteRelacionado");
	    	              this._DescricaoAplicacaoAmbienteRelacionado = value;
	    	              this.RaiseDataMemberChanged("DescricaoAplicacaoAmbienteRelacionado");
	    	              this.OnDescricaoAplicacaoAmbienteRelacionadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For EmDesenvolvimento
	    partial void OnEmDesenvolvimentoChanging(Boolean value);
	    partial void OnEmDesenvolvimentoChanged();

	    private Boolean _EmDesenvolvimento;

	    [DataMember(IsRequired = true, Name = "EmDesenvolvimento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Em Desenvolvimento", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente];LookUpTitle[Seleção de (Em Desenvolvimento)];LookUpQuery[executeLookUpTcsAmbiente];LookUpFinalize[finalizeLookUpTcsAmbiente];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"DescricaoAplicacao\" : \"Aplicação\", \"EmDesenvolvimento\" : \"Em Desenvolvimento\", \"IdLinxEmpresa\" : \"Id Linx\", \"DescricaoAplicativo\" : \"Aplicativo\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\", \"NomeEmpresa\" : \"Grupo Econômico\", \"UidAplicacao\" : \"Uid Aplicacao\", \"UidEmpresa\" : \"Uid Empresa\", \"Url\" : \"Url\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"DescricaoAplicacao\" : true, \"EmDesenvolvimento\" : true, \"IdLinxEmpresa\" : false, \"DescricaoAplicativo\" : true, \"IdTcsAmbiente\" : false, \"NomeEmpresa\" : true, \"UidAplicacao\" : false, \"UidEmpresa\" : false, \"Url\" : false, \"IdTcsAplicativo\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.EM_DESENVOLVIMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Boolean#EmDesenvolvimento#false##0##Em Desenvolvimento#2#true##::LookUpTcsAmbiente##true#false#TCS_AMBIENTE#TCS_AMBIENTE#Linx.Framework.BV.UsuarioAutorizacao#IQueryable#DescricaoAplicacao,EmDesenvolvimento,UidAplicacao,Url[DescricaoAplicacao,EmDesenvolvimento,DescricaoAplicativo,UidAplicacao,Url,IdTcsAplicativo];DescricaoAplicativo,IdTcsAplicativo[DescricaoAplicativo,IdTcsAplicativo]#DescricaoAmbiente[DescricaoAplicacao=DescricaoAplicacao,EmDesenvolvimento=EmDesenvolvimento,IdLinxEmpresa=IdLinxEmpresa,DescricaoAplicativo=DescricaoAplicativo,NomeEmpresa=NomeEmpresa,UidAplicacao=UidAplicacao,UidEmpresa=UidEmpresa,Url=Url,IdTcsAplicativo=IdTcsAplicativo];DescricaoAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];EmDesenvolvimento[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdLinxEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdTcsAmbiente[DescricaoAplicacao=DescricaoAplicacao,EmDesenvolvimento=EmDesenvolvimento,IdLinxEmpresa=IdLinxEmpresa,DescricaoAplicativo=DescricaoAplicativo,NomeEmpresa=NomeEmpresa,UidAplicacao=UidAplicacao,UidEmpresa=UidEmpresa,Url=Url,IdTcsAplicativo=IdTcsAplicativo];NomeEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UidAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UidEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];Url[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo]#true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.EM_DESENVOLVIMENTO")]
	    public Boolean EmDesenvolvimento
	    {
	    	    get
	    	    {
	    	          return _EmDesenvolvimento;
	    	    }
	    	    set
	    	    {
	    	          if (this._EmDesenvolvimento != value)
	    	          {
	    	              this.ValidateProperty("EmDesenvolvimento", value);
	    	              this.OnEmDesenvolvimentoChanging(value);
	    	              this.RaiseDataMemberChanging("EmDesenvolvimento");
	    	              this._EmDesenvolvimento = value;
	    	              this.RaiseDataMemberChanged("EmDesenvolvimento");
	    	              this.OnEmDesenvolvimentoChanged();
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
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente1];LookUpTitle[Seleção de (Id Aplicacao)];LookUpQuery[executeLookUpTcsAmbiente1];LookUpFinalize[finalizeLookUpTcsAmbiente1];LookUpDisplayColumns[{\"DescricaoAmbienteRelacionado\" : \"Ambiente\", \"NomeEmpresaAmbienteRelacionado\" : \"Empresa\", \"DescricaoAplicacaoAmbienteRelacionado\" : \"Aplicação\", \"IdLinxAmbienteRelacionado\" : \"ID Linx\", \"IdTcsAmbienteRelacionado\" : \"Id Tcs Ambiente1\", \"IdAplicacao\" : \"Id Aplicacao\"}];LookUpColumns[{\"DescricaoAmbienteRelacionado\" : true, \"NomeEmpresaAmbienteRelacionado\" : true, \"DescricaoAplicacaoAmbienteRelacionado\" : true, \"IdLinxAmbienteRelacionado\" : false, \"IdTcsAmbienteRelacionado\" : false, \"IdAplicacao\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE1.TCS_APLICACAO.ID_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<Int32>#IdAplicacao#false##12:0##Id Aplicacao#5#false##::LookUpTcsAmbiente1##false#false#TCS_AMBIENTE1#TCS_AMBIENTE#Linx.Framework.BV.UsuarioAutorizacao#IQueryable#NomeEmpresaAmbienteRelacionado,IdLinxAmbienteRelacionado[NomeEmpresaAmbienteRelacionado,IdLinxAmbienteRelacionado]#DescricaoAmbienteRelacionado[NomeEmpresaAmbienteRelacionado=NomeEmpresaAmbienteRelacionado,DescricaoAplicacaoAmbienteRelacionado=DescricaoAplicacaoAmbienteRelacionado,IdLinxAmbienteRelacionado=IdLinxAmbienteRelacionado,IdAplicacao=IdAplicacao];IdTcsAmbienteRelacionado[NomeEmpresaAmbienteRelacionado=NomeEmpresaAmbienteRelacionado,DescricaoAplicacaoAmbienteRelacionado=DescricaoAplicacaoAmbienteRelacionado,IdLinxAmbienteRelacionado=IdLinxAmbienteRelacionado,IdAplicacao=IdAplicacao]#true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.TCS_APLICACAO.ID_APLICACAO")]
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
	    //Extensibility Partial Method Definitions For IdAplicacaoAmbiente
	    partial void OnIdAplicacaoAmbienteChanging(Int32 value);
	    partial void OnIdAplicacaoAmbienteChanged();

	    private Int32 _IdAplicacaoAmbiente;

	    [DataMember(IsRequired = true, Name = "IdAplicacaoAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Aplicacao", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.ID_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.ID_APLICACAO")]
	    public Int32 IdAplicacaoAmbiente
	    {
	    	    get
	    	    {
	    	          return _IdAplicacaoAmbiente;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdAplicacaoAmbiente != value)
	    	          {
	    	              this.ValidateProperty("IdAplicacaoAmbiente", value);
	    	              this.OnIdAplicacaoAmbienteChanging(value);
	    	              this.RaiseDataMemberChanging("IdAplicacaoAmbiente");
	    	              this._IdAplicacaoAmbiente = value;
	    	              this.RaiseDataMemberChanged("IdAplicacaoAmbiente");
	    	              this.OnIdAplicacaoAmbienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLinxAmbienteRelacionado
	    partial void OnIdLinxAmbienteRelacionadoChanging(System.Nullable<Int32> value);
	    partial void OnIdLinxAmbienteRelacionadoChanged();

	    private System.Nullable<Int32> _IdLinxAmbienteRelacionado;

	    [DataMember(Name = "IdLinxAmbienteRelacionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx1", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente1];LookUpTitle[Seleção de (Id Linx1)];LookUpQuery[executeLookUpTcsAmbiente1];LookUpFinalize[finalizeLookUpTcsAmbiente1];LookUpDisplayColumns[{\"DescricaoAmbienteRelacionado\" : \"Ambiente\", \"NomeEmpresaAmbienteRelacionado\" : \"Empresa\", \"DescricaoAplicacaoAmbienteRelacionado\" : \"Aplicação\", \"IdLinxAmbienteRelacionado\" : \"ID Linx\", \"IdTcsAmbienteRelacionado\" : \"Id Tcs Ambiente1\", \"IdAplicacao\" : \"Id Aplicacao\"}];LookUpColumns[{\"DescricaoAmbienteRelacionado\" : true, \"NomeEmpresaAmbienteRelacionado\" : true, \"DescricaoAplicacaoAmbienteRelacionado\" : true, \"IdLinxAmbienteRelacionado\" : false, \"IdTcsAmbienteRelacionado\" : false, \"IdAplicacao\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<Int32>#IdLinxAmbienteRelacionado#false##12:0##ID Linx#3#false##::LookUpTcsAmbiente1##false#false#TCS_AMBIENTE1#TCS_AMBIENTE#Linx.Framework.BV.UsuarioAutorizacao#IQueryable#NomeEmpresaAmbienteRelacionado,IdLinxAmbienteRelacionado[NomeEmpresaAmbienteRelacionado,IdLinxAmbienteRelacionado]#DescricaoAmbienteRelacionado[NomeEmpresaAmbienteRelacionado=NomeEmpresaAmbienteRelacionado,DescricaoAplicacaoAmbienteRelacionado=DescricaoAplicacaoAmbienteRelacionado,IdLinxAmbienteRelacionado=IdLinxAmbienteRelacionado,IdAplicacao=IdAplicacao];IdTcsAmbienteRelacionado[NomeEmpresaAmbienteRelacionado=NomeEmpresaAmbienteRelacionado,DescricaoAplicacaoAmbienteRelacionado=DescricaoAplicacaoAmbienteRelacionado,IdLinxAmbienteRelacionado=IdLinxAmbienteRelacionado,IdAplicacao=IdAplicacao]#true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
	    public System.Nullable<Int32> IdLinxAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _IdLinxAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLinxAmbienteRelacionado != value)
	    	          {
	    	              this.ValidateProperty("IdLinxAmbienteRelacionado", value);
	    	              this.OnIdLinxAmbienteRelacionadoChanging(value);
	    	              this.RaiseDataMemberChanging("IdLinxAmbienteRelacionado");
	    	              this._IdLinxAmbienteRelacionado = value;
	    	              this.RaiseDataMemberChanged("IdLinxAmbienteRelacionado");
	    	              this.OnIdLinxAmbienteRelacionadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLinxEmpresa
	    partial void OnIdLinxEmpresaChanging(Int32 value);
	    partial void OnIdLinxEmpresaChanged();

	    private Int32 _IdLinxEmpresa;

	    [DataMember(IsRequired = true, Name = "IdLinxEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente];LookUpTitle[Seleção de (Id Linx)];LookUpQuery[executeLookUpTcsAmbiente];LookUpFinalize[finalizeLookUpTcsAmbiente];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"DescricaoAplicacao\" : \"Aplicação\", \"EmDesenvolvimento\" : \"Em Desenvolvimento\", \"IdLinxEmpresa\" : \"Id Linx\", \"DescricaoAplicativo\" : \"Aplicativo\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\", \"NomeEmpresa\" : \"Grupo Econômico\", \"UidAplicacao\" : \"Uid Aplicacao\", \"UidEmpresa\" : \"Uid Empresa\", \"Url\" : \"Url\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"DescricaoAplicacao\" : true, \"EmDesenvolvimento\" : true, \"IdLinxEmpresa\" : false, \"DescricaoAplicativo\" : true, \"IdTcsAmbiente\" : false, \"NomeEmpresa\" : true, \"UidAplicacao\" : false, \"UidEmpresa\" : false, \"Url\" : false, \"IdTcsAplicativo\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdLinxEmpresa#true##12:0##Id Linx#3#false##::LookUpTcsAmbiente##true#false#TCS_AMBIENTE#TCS_AMBIENTE#Linx.Framework.BV.UsuarioAutorizacao#IQueryable#DescricaoAplicacao,EmDesenvolvimento,UidAplicacao,Url[DescricaoAplicacao,EmDesenvolvimento,DescricaoAplicativo,UidAplicacao,Url,IdTcsAplicativo];DescricaoAplicativo,IdTcsAplicativo[DescricaoAplicativo,IdTcsAplicativo]#DescricaoAmbiente[DescricaoAplicacao=DescricaoAplicacao,EmDesenvolvimento=EmDesenvolvimento,IdLinxEmpresa=IdLinxEmpresa,DescricaoAplicativo=DescricaoAplicativo,NomeEmpresa=NomeEmpresa,UidAplicacao=UidAplicacao,UidEmpresa=UidEmpresa,Url=Url,IdTcsAplicativo=IdTcsAplicativo];DescricaoAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];EmDesenvolvimento[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdLinxEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdTcsAmbiente[DescricaoAplicacao=DescricaoAplicacao,EmDesenvolvimento=EmDesenvolvimento,IdLinxEmpresa=IdLinxEmpresa,DescricaoAplicativo=DescricaoAplicativo,NomeEmpresa=NomeEmpresa,UidAplicacao=UidAplicacao,UidEmpresa=UidEmpresa,Url=Url,IdTcsAplicativo=IdTcsAplicativo];NomeEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UidAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UidEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];Url[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo]#true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
	    public Int32 IdLinxEmpresa
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
	    //Extensibility Partial Method Definitions For IdTcsAmbiente
	    partial void OnIdTcsAmbienteChanging(Int32 value);
	    partial void OnIdTcsAmbienteChanged();

	    private Int32 _IdTcsAmbiente;

	    [DataMember(IsRequired = true, Name = "IdTcsAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente];LookUpTitle[Seleção de (Id Tcs Ambiente)];LookUpQuery[executeLookUpTcsAmbiente];LookUpFinalize[finalizeLookUpTcsAmbiente];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"DescricaoAplicacao\" : \"Aplicação\", \"EmDesenvolvimento\" : \"Em Desenvolvimento\", \"IdLinxEmpresa\" : \"Id Linx\", \"DescricaoAplicativo\" : \"Aplicativo\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\", \"NomeEmpresa\" : \"Grupo Econômico\", \"UidAplicacao\" : \"Uid Aplicacao\", \"UidEmpresa\" : \"Uid Empresa\", \"Url\" : \"Url\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"DescricaoAplicacao\" : true, \"EmDesenvolvimento\" : true, \"IdLinxEmpresa\" : false, \"DescricaoAplicativo\" : true, \"IdTcsAmbiente\" : false, \"NomeEmpresa\" : true, \"UidAplicacao\" : false, \"UidEmpresa\" : false, \"Url\" : false, \"IdTcsAplicativo\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.ID_TCS_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdTcsAmbiente#true##12:0##Id Tcs Ambiente#5#false##::LookUpTcsAmbiente##true#false#TCS_AMBIENTE#TCS_AMBIENTE#Linx.Framework.BV.UsuarioAutorizacao#IQueryable#DescricaoAplicacao,EmDesenvolvimento,UidAplicacao,Url[DescricaoAplicacao,EmDesenvolvimento,DescricaoAplicativo,UidAplicacao,Url,IdTcsAplicativo];DescricaoAplicativo,IdTcsAplicativo[DescricaoAplicativo,IdTcsAplicativo]#DescricaoAmbiente[DescricaoAplicacao=DescricaoAplicacao,EmDesenvolvimento=EmDesenvolvimento,IdLinxEmpresa=IdLinxEmpresa,DescricaoAplicativo=DescricaoAplicativo,NomeEmpresa=NomeEmpresa,UidAplicacao=UidAplicacao,UidEmpresa=UidEmpresa,Url=Url,IdTcsAplicativo=IdTcsAplicativo];DescricaoAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];EmDesenvolvimento[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdLinxEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdTcsAmbiente[DescricaoAplicacao=DescricaoAplicacao,EmDesenvolvimento=EmDesenvolvimento,IdLinxEmpresa=IdLinxEmpresa,DescricaoAplicativo=DescricaoAplicativo,NomeEmpresa=NomeEmpresa,UidAplicacao=UidAplicacao,UidEmpresa=UidEmpresa,Url=Url,IdTcsAplicativo=IdTcsAplicativo];NomeEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UidAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UidEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];Url[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo]#true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.ID_TCS_AMBIENTE")]
	    public Int32 IdTcsAmbiente
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
	    //Extensibility Partial Method Definitions For IdTcsAmbienteRelacionado
	    partial void OnIdTcsAmbienteRelacionadoChanging(System.Nullable<Int32> value);
	    partial void OnIdTcsAmbienteRelacionadoChanged();

	    private System.Nullable<Int32> _IdTcsAmbienteRelacionado;

	    [DataMember(Name = "IdTcsAmbienteRelacionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente1", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente1];LookUpTitle[Seleção de (Id Tcs Ambiente1)];LookUpQuery[executeLookUpTcsAmbiente1];LookUpFinalize[finalizeLookUpTcsAmbiente1];LookUpDisplayColumns[{\"DescricaoAmbienteRelacionado\" : \"Ambiente\", \"NomeEmpresaAmbienteRelacionado\" : \"Empresa\", \"DescricaoAplicacaoAmbienteRelacionado\" : \"Aplicação\", \"IdLinxAmbienteRelacionado\" : \"ID Linx\", \"IdTcsAmbienteRelacionado\" : \"Id Tcs Ambiente1\", \"IdAplicacao\" : \"Id Aplicacao\"}];LookUpColumns[{\"DescricaoAmbienteRelacionado\" : true, \"NomeEmpresaAmbienteRelacionado\" : true, \"DescricaoAplicacaoAmbienteRelacionado\" : true, \"IdLinxAmbienteRelacionado\" : false, \"IdTcsAmbienteRelacionado\" : false, \"IdAplicacao\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE1.ID_TCS_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<Int32>#IdTcsAmbienteRelacionado#true##12:0##Id Tcs Ambiente1#4#false##::LookUpTcsAmbiente1##false#false#TCS_AMBIENTE1#TCS_AMBIENTE#Linx.Framework.BV.UsuarioAutorizacao#IQueryable#NomeEmpresaAmbienteRelacionado,IdLinxAmbienteRelacionado[NomeEmpresaAmbienteRelacionado,IdLinxAmbienteRelacionado]#DescricaoAmbienteRelacionado[NomeEmpresaAmbienteRelacionado=NomeEmpresaAmbienteRelacionado,DescricaoAplicacaoAmbienteRelacionado=DescricaoAplicacaoAmbienteRelacionado,IdLinxAmbienteRelacionado=IdLinxAmbienteRelacionado,IdAplicacao=IdAplicacao];IdTcsAmbienteRelacionado[NomeEmpresaAmbienteRelacionado=NomeEmpresaAmbienteRelacionado,DescricaoAplicacaoAmbienteRelacionado=DescricaoAplicacaoAmbienteRelacionado,IdLinxAmbienteRelacionado=IdLinxAmbienteRelacionado,IdAplicacao=IdAplicacao]#true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.ID_TCS_AMBIENTE")]
	    public System.Nullable<Int32> IdTcsAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _IdTcsAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAmbienteRelacionado != value)
	    	          {
	    	              this.ValidateProperty("IdTcsAmbienteRelacionado", value);
	    	              this.OnIdTcsAmbienteRelacionadoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsAmbienteRelacionado");
	    	              this._IdTcsAmbienteRelacionado = value;
	    	              this.RaiseDataMemberChanged("IdTcsAmbienteRelacionado");
	    	              this.OnIdTcsAmbienteRelacionadoChanged();
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
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente];LookUpTitle[Seleção de (Id Tcs Aplicativo)];LookUpQuery[executeLookUpTcsAmbiente];LookUpFinalize[finalizeLookUpTcsAmbiente];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"DescricaoAplicacao\" : \"Aplicação\", \"EmDesenvolvimento\" : \"Em Desenvolvimento\", \"IdLinxEmpresa\" : \"Id Linx\", \"DescricaoAplicativo\" : \"Aplicativo\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\", \"NomeEmpresa\" : \"Grupo Econômico\", \"UidAplicacao\" : \"Uid Aplicacao\", \"UidEmpresa\" : \"Uid Empresa\", \"Url\" : \"Url\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"DescricaoAplicacao\" : true, \"EmDesenvolvimento\" : true, \"IdLinxEmpresa\" : false, \"DescricaoAplicativo\" : true, \"IdTcsAmbiente\" : false, \"NomeEmpresa\" : true, \"UidAplicacao\" : false, \"UidEmpresa\" : false, \"Url\" : false, \"IdTcsAplicativo\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdTcsAplicativo#true##12:0##Id Tcs Aplicativo#10#false##::LookUpTcsAmbiente##true#false#TCS_AMBIENTE#TCS_AMBIENTE#Linx.Framework.BV.UsuarioAutorizacao#IQueryable#DescricaoAplicacao,EmDesenvolvimento,UidAplicacao,Url[DescricaoAplicacao,EmDesenvolvimento,DescricaoAplicativo,UidAplicacao,Url,IdTcsAplicativo];DescricaoAplicativo,IdTcsAplicativo[DescricaoAplicativo,IdTcsAplicativo]#DescricaoAmbiente[DescricaoAplicacao=DescricaoAplicacao,EmDesenvolvimento=EmDesenvolvimento,IdLinxEmpresa=IdLinxEmpresa,DescricaoAplicativo=DescricaoAplicativo,NomeEmpresa=NomeEmpresa,UidAplicacao=UidAplicacao,UidEmpresa=UidEmpresa,Url=Url,IdTcsAplicativo=IdTcsAplicativo];DescricaoAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];EmDesenvolvimento[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdLinxEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdTcsAmbiente[DescricaoAplicacao=DescricaoAplicacao,EmDesenvolvimento=EmDesenvolvimento,IdLinxEmpresa=IdLinxEmpresa,DescricaoAplicativo=DescricaoAplicativo,NomeEmpresa=NomeEmpresa,UidAplicacao=UidAplicacao,UidEmpresa=UidEmpresa,Url=Url,IdTcsAplicativo=IdTcsAplicativo];NomeEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UidAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UidEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];Url[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo]#true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO")]
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
	    //Extensibility Partial Method Definitions For IdTcsUsuarioAcesso
	    partial void OnIdTcsUsuarioAcessoChanging(Int32 value);
	    partial void OnIdTcsUsuarioAcessoChanged();

	    private Int32 _IdTcsUsuarioAcesso;

	    [DataMember(IsRequired = true, Name = "IdTcsUsuarioAcesso", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Acesso", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO")]
	    public Int32 IdTcsUsuarioAcesso
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
	    partial void OnIdUsuarioChanging(Int64 value);
	    partial void OnIdUsuarioChanged();

	    private Int64 _IdUsuario;

	    [DataMember(IsRequired = true, Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.ID_USUARIO")]
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
	    //Extensibility Partial Method Definitions For IndicaAcessoPadrao
	    partial void OnIndicaAcessoPadraoChanging(Boolean value);
	    partial void OnIndicaAcessoPadraoChanged();

	    private Boolean _IndicaAcessoPadrao;

	    [DataMember(IsRequired = true, Name = "IndicaAcessoPadrao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Acesso Padrão", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.INDICA_ACESSO_PADRAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.INDICA_ACESSO_PADRAO")]
	    public Boolean IndicaAcessoPadrao
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
	    partial void OnIndicaAdministradorChanging(Boolean value);
	    partial void OnIndicaAdministradorChanged();

	    private Boolean _IndicaAdministrador;

	    [DataMember(IsRequired = true, Name = "IndicaAdministrador", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Administrador", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.INDICA_ADMINISTRADOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.INDICA_ADMINISTRADOR")]
	    public Boolean IndicaAdministrador
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
	    partial void OnIndicaMultiGpeconChanging(Boolean value);
	    partial void OnIndicaMultiGpeconChanged();

	    private Boolean _IndicaMultiGpecon;

	    [DataMember(IsRequired = true, Name = "IndicaMultiGpecon", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Multi Grupo Econômico", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.INDICA_MULTI_GPECON];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.INDICA_MULTI_GPECON")]
	    public Boolean IndicaMultiGpecon
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
	    //Extensibility Partial Method Definitions For NomeEmpresaAmbienteRelacionado
	    partial void OnNomeEmpresaAmbienteRelacionadoChanging(System.String value);
	    partial void OnNomeEmpresaAmbienteRelacionadoChanged();

	    private System.String _NomeEmpresaAmbienteRelacionado;

	    [DataMember(Name = "NomeEmpresaAmbienteRelacionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Empresa Ambiente Relacionado", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente1];LookUpTitle[Seleção de (Empresa Ambiente Relacionado)];LookUpQuery[executeLookUpTcsAmbiente1];LookUpFinalize[finalizeLookUpTcsAmbiente1];LookUpDisplayColumns[{\"DescricaoAmbienteRelacionado\" : \"Ambiente\", \"NomeEmpresaAmbienteRelacionado\" : \"Empresa\", \"DescricaoAplicacaoAmbienteRelacionado\" : \"Aplicação\", \"IdLinxAmbienteRelacionado\" : \"ID Linx\", \"IdTcsAmbienteRelacionado\" : \"Id Tcs Ambiente1\", \"IdAplicacao\" : \"Id Aplicacao\"}];LookUpColumns[{\"DescricaoAmbienteRelacionado\" : true, \"NomeEmpresaAmbienteRelacionado\" : true, \"DescricaoAplicacaoAmbienteRelacionado\" : true, \"IdLinxAmbienteRelacionado\" : false, \"IdTcsAmbienteRelacionado\" : false, \"IdAplicacao\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeEmpresaAmbienteRelacionado#false##250:0##Empresa#1#true##::LookUpTcsAmbiente1##false#false#TCS_AMBIENTE1#TCS_AMBIENTE#Linx.Framework.BV.UsuarioAutorizacao#IQueryable#NomeEmpresaAmbienteRelacionado,IdLinxAmbienteRelacionado[NomeEmpresaAmbienteRelacionado,IdLinxAmbienteRelacionado]#DescricaoAmbienteRelacionado[NomeEmpresaAmbienteRelacionado=NomeEmpresaAmbienteRelacionado,DescricaoAplicacaoAmbienteRelacionado=DescricaoAplicacaoAmbienteRelacionado,IdLinxAmbienteRelacionado=IdLinxAmbienteRelacionado,IdAplicacao=IdAplicacao];IdTcsAmbienteRelacionado[NomeEmpresaAmbienteRelacionado=NomeEmpresaAmbienteRelacionado,DescricaoAplicacaoAmbienteRelacionado=DescricaoAplicacaoAmbienteRelacionado,IdLinxAmbienteRelacionado=IdLinxAmbienteRelacionado,IdAplicacao=IdAplicacao]#true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
	    public System.String NomeEmpresaAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _NomeEmpresaAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeEmpresaAmbienteRelacionado != value)
	    	          {
	    	              this.ValidateProperty("NomeEmpresaAmbienteRelacionado", value);
	    	              this.OnNomeEmpresaAmbienteRelacionadoChanging(value);
	    	              this.RaiseDataMemberChanging("NomeEmpresaAmbienteRelacionado");
	    	              this._NomeEmpresaAmbienteRelacionado = value;
	    	              this.RaiseDataMemberChanged("NomeEmpresaAmbienteRelacionado");
	    	              this.OnNomeEmpresaAmbienteRelacionadoChanged();
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
	    [Display(Name = "Usuário Autenticação", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO")]
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
	    [Display(Name = "Nome", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO")]
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
	    //Extensibility Partial Method Definitions For AutenticacaoWindows
	    partial void OnAutenticacaoWindowsChanging(Boolean value);
	    partial void OnAutenticacaoWindowsChanged();

	    private Boolean _AutenticacaoWindows;

	    [DataMember(IsRequired = true, Name = "AutenticacaoWindows", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Utiliza Autenticação Windows", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.AUTENTICACAO_WINDOWS];IsMeasure[false]")]
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
	    [Display(Name = "Bairro", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.BAIRRO];IsMeasure[false]")]
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
	    [Display(Name = "CEP", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.CEP];IsMeasure[false]")]
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
	    [Display(Name = "CPF/CNPJ", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[###.###.###-##];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.CNPJ_CPF];IsMeasure[false]")]
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
	    [Display(Name = "Complemento", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.COMPLEMENTO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For ConfirmacaoUsuario
	    partial void OnConfirmacaoUsuarioChanging(string value);
	    partial void OnConfirmacaoUsuarioChanged();

	    private string _ConfirmacaoUsuario;

	    [DataMember(Name = "ConfirmacaoUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Senha", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[''];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="\"\"")]
	    public string ConfirmacaoUsuario
	    {
	    	    get
	    	    {
	    	          return _ConfirmacaoUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._ConfirmacaoUsuario != value)
	    	          {
	    	              this.ValidateProperty("ConfirmacaoUsuario", value);
	    	              this.OnConfirmacaoUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("ConfirmacaoUsuario");
	    	              this._ConfirmacaoUsuario = value;
	    	              this.RaiseDataMemberChanged("ConfirmacaoUsuario");
	    	              this.OnConfirmacaoUsuarioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ConfirmacaoUsuario1
	    partial void OnConfirmacaoUsuario1Changing(string value);
	    partial void OnConfirmacaoUsuario1Changed();

	    private string _ConfirmacaoUsuario1;

	    [DataMember(Name = "ConfirmacaoUsuario1", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Confirmação", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[''];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="\"\"")]
	    public string ConfirmacaoUsuario1
	    {
	    	    get
	    	    {
	    	          return _ConfirmacaoUsuario1;
	    	    }
	    	    set
	    	    {
	    	          if (this._ConfirmacaoUsuario1 != value)
	    	          {
	    	              this.ValidateProperty("ConfirmacaoUsuario1", value);
	    	              this.OnConfirmacaoUsuario1Changing(value);
	    	              this.RaiseDataMemberChanging("ConfirmacaoUsuario1");
	    	              this._ConfirmacaoUsuario1 = value;
	    	              this.RaiseDataMemberChanged("ConfirmacaoUsuario1");
	    	              this.OnConfirmacaoUsuario1Changed();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For CriaUsuario
	    partial void OnCriaUsuarioChanging(bool value);
	    partial void OnCriaUsuarioChanged();

	    private bool _CriaUsuario;

	    [DataMember(IsRequired = true, Name = "CriaUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[false];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="false")]
	    public bool CriaUsuario
	    {
	    	    get
	    	    {
	    	          return _CriaUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._CriaUsuario != value)
	    	          {
	    	              this.ValidateProperty("CriaUsuario", value);
	    	              this.OnCriaUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("CriaUsuario");
	    	              this._CriaUsuario = value;
	    	              this.RaiseDataMemberChanged("CriaUsuario");
	    	              this.OnCriaUsuarioChanged();
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
	    [Display(Name = "Alteração", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[DateTime.Now];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.DATA_ALTERACAO];IsMeasure[false]")]
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
	    [Display(Name = "Cadastro", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[DateTime.Now];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.DATA_CADASTRO];IsMeasure[false]")]
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
	    [Display(Name = "Expiração Senha", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[DateTime.Now];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.DATA_EXPIRACAO_SENHA];IsMeasure[false]")]
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
	    [Display(Name = "Email", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.EMAIL];IsMeasure[false]")]
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
	    [Display(Name = "Móvel", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.FONE_CELULAR];IsMeasure[false]")]
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
	    [Display(Name = "Fixo / Ramal", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.FONE_FIXO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For GeraSenhaUsuario
	    partial void OnGeraSenhaUsuarioChanging(bool value);
	    partial void OnGeraSenhaUsuarioChanged();

	    private bool _GeraSenhaUsuario;

	    [DataMember(IsRequired = true, Name = "GeraSenhaUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[[GERA_SENHA_USUARIO]];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[false];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="false")]
	    public bool GeraSenhaUsuario
	    {
	    	    get
	    	    {
	    	          return _GeraSenhaUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._GeraSenhaUsuario != value)
	    	          {
	    	              this.ValidateProperty("GeraSenhaUsuario", value);
	    	              this.OnGeraSenhaUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("GeraSenhaUsuario");
	    	              this._GeraSenhaUsuario = value;
	    	              this.RaiseDataMemberChanged("GeraSenhaUsuario");
	    	              this.OnGeraSenhaUsuarioChanged();
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
	    [Display(Name = "Id Linx Empresa / Grupo Econômico", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For Inativo
	    partial void OnInativoChanging(Boolean value);
	    partial void OnInativoChanged();

	    private Boolean _Inativo;

	    [DataMember(IsRequired = true, Name = "Inativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inativo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.INATIVO];IsMeasure[false]")]
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
	    [Display(Name = "Permite Acesso de Suporte", Description="", Order = 14, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.INDICA_ACESSO_SUPORTE];IsMeasure[false]")]
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
	    [Display(Name = "Inscr. Estadual / RG", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.INSCR_ESTADUAL_RG];IsMeasure[false]")]
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
	    [Display(Name = "Logradouro", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.LOGRADOURO];IsMeasure[false]")]
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
	    [Display(Name = "Pessoa Física / Juridíca", Description="", Order = 14, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[LX_PFJ_FISICA_JURIDICA];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.LX_PFJ_FISICA_JURIDICA];IsMeasure[false]")]
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
	    [Display(Name = "Tipo Logradouro", Description="", Order = 15, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[LxTipoLogradouro];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO];IsMeasure[false]")]
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
	    [Display(Name = "Município", Description="", Order = 16, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.MUNICIPIO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For NomeCurtoUsuario
	    partial void OnNomeCurtoUsuarioChanging(System.String value);
	    partial void OnNomeCurtoUsuarioChanged();

	    private System.String _NomeCurtoUsuario;

	    [DataMember(IsRequired = true, Name = "NomeCurtoUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Apelido", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_CURTO_USUARIO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For NomeEmpresa
	    partial void OnNomeEmpresaChanging(System.String value);
	    partial void OnNomeEmpresaChanged();

	    private System.String _NomeEmpresa;

	    [DataMember(IsRequired = true, Name = "NomeEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Empresa / Grupo Econômico", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeEmpresa#false##2500##Grupo Econômico#6#true##::LookUpTcsAmbiente##true#false#TCS_AMBIENTE#TCS_AMBIENTE#Linx.Framework.BV.UsuarioAutorizacao#IQueryable#DescricaoAplicacao,EmDesenvolvimento,UidAplicacao,Url[DescricaoAplicacao,EmDesenvolvimento,DescricaoAplicativo,UidAplicacao,Url,IdTcsAplicativo];DescricaoAplicativo,IdTcsAplicativo[DescricaoAplicativo,IdTcsAplicativo]#DescricaoAmbiente[DescricaoAplicacao=DescricaoAplicacao,EmDesenvolvimento=EmDesenvolvimento,IdLinxEmpresa=IdLinxEmpresa,DescricaoAplicativo=DescricaoAplicativo,NomeEmpresa=NomeEmpresa,UidAplicacao=UidAplicacao,UidEmpresa=UidEmpresa,Url=Url,IdTcsAplicativo=IdTcsAplicativo];DescricaoAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];EmDesenvolvimento[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdLinxEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdTcsAmbiente[DescricaoAplicacao=DescricaoAplicacao,EmDesenvolvimento=EmDesenvolvimento,IdLinxEmpresa=IdLinxEmpresa,DescricaoAplicativo=DescricaoAplicativo,NomeEmpresa=NomeEmpresa,UidAplicacao=UidAplicacao,UidEmpresa=UidEmpresa,Url=Url,IdTcsAplicativo=IdTcsAplicativo];NomeEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UidAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UidEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];Url[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo]#true#false", EdmKey="TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
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
	    //Extensibility Partial Method Definitions For Numero
	    partial void OnNumeroChanging(System.String value);
	    partial void OnNumeroChanged();

	    private System.String _Numero;

	    [DataMember(Name = "Numero", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Número", Description="", Order = 20, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[Logradouro];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NUMERO];IsMeasure[false]")]
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
	    [Display(Name = "Obs. Endereço", Description="", Order = 21, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.OBS_ENDERECO];IsMeasure[false]")]
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
	    [Display(Name = "Ramal", Description="", Order = 22, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(6)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[FoneFixo];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.RAMAL];IsMeasure[false]")]
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
	    [Display(Name = "UF", Description="", Order = 23, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(4)]
	    [FunctionalPoint("Precision[4:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[Municipio];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.UF];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For UidEmpresa
	    partial void OnUidEmpresaChanging(System.Guid value);
	    partial void OnUidEmpresaChanged();

	    private System.Guid _UidEmpresa;

	    [DataMember(IsRequired = true, Name = "UidEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Empresa", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Guid#UidEmpresa#false##36:0##Uid Empresa#8#false##::LookUpTcsAmbiente##true#false#TCS_AMBIENTE#TCS_AMBIENTE#Linx.Framework.BV.UsuarioAutorizacao#IQueryable#DescricaoAplicacao,EmDesenvolvimento,UidAplicacao,Url[DescricaoAplicacao,EmDesenvolvimento,DescricaoAplicativo,UidAplicacao,Url,IdTcsAplicativo];DescricaoAplicativo,IdTcsAplicativo[DescricaoAplicativo,IdTcsAplicativo]#DescricaoAmbiente[DescricaoAplicacao=DescricaoAplicacao,EmDesenvolvimento=EmDesenvolvimento,IdLinxEmpresa=IdLinxEmpresa,DescricaoAplicativo=DescricaoAplicativo,NomeEmpresa=NomeEmpresa,UidAplicacao=UidAplicacao,UidEmpresa=UidEmpresa,Url=Url,IdTcsAplicativo=IdTcsAplicativo];DescricaoAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];EmDesenvolvimento[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdLinxEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdTcsAmbiente[DescricaoAplicacao=DescricaoAplicacao,EmDesenvolvimento=EmDesenvolvimento,IdLinxEmpresa=IdLinxEmpresa,DescricaoAplicativo=DescricaoAplicativo,NomeEmpresa=NomeEmpresa,UidAplicacao=UidAplicacao,UidEmpresa=UidEmpresa,Url=Url,IdTcsAplicativo=IdTcsAplicativo];NomeEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UidAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UidEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];Url[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo]#true#false", EdmKey="TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA")]
	    public System.Guid UidEmpresa
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
	    partial void OnUidUsuarioChanging(System.Guid value);
	    partial void OnUidUsuarioChanged();

	    private System.Guid _UidUsuario;

	    [DataMember(Name = "UidUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Usuario", Description="", Order = 26, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.UID_USUARIO];IsMeasure[false]")]
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
	    [Display(Name = "Vigência Final", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[new DateTime(2099, 12, 31)];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.VIGENCIA_FINAL];IsMeasure[false]")]
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
	    [Display(Name = "Vigência Inicial", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[DateTime.Now];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.VIGENCIA_INICIAL];IsMeasure[false]")]
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
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.ID_TCS_AMBIENTE", Source = "IdTcsAmbienteRelacionado", Target = "ID_TCS_AMBIENTE", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE", RelationPropertyName = "TCS_AMBIENTE1" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });

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
	    [Display(Name = "Pessoa Física / Juridíca", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    [Display(Name = "Tipo Logradouro", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
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

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Identidade Externa];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdIdentidadeExterna];ReadOnly[false];Entities[TCS_IDENTIDADE_EXTERNA:IdIdentidadeExterna];SubQueryInfo[Select 1 From #ParentAlias#.TCS_IDENTIDADE_EXTERNA_LISTA as #Alias#];EdmEntityName[TCS_IDENTIDADE_EXTERNA];EntityRelations[TCS_USUARIO_AUTENTICACAO(TCS_USUARIO_AUTENTICACAO)#TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)];EdmParentEntityName[TCS_USUARIO_AUTENTICACAO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsIdentidadeExterna")]
	[Serializable()]
	public partial class TcsIdentidadeExternaParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For IdentidadeExterna
	    partial void OnIdentidadeExternaChanging(System.String value);
	    partial void OnIdentidadeExternaChanged();

	    private System.String _IdentidadeExterna;

	    [DataMember(IsRequired = true, Name = "IdentidadeExterna", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Identidade Externa", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_IDENTIDADE_EXTERNA.IDENTIDADE_EXTERNA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_IDENTIDADE_EXTERNA.IDENTIDADE_EXTERNA")]
	    public System.String IdentidadeExterna
	    {
	    	    get
	    	    {
	    	          return _IdentidadeExterna;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdentidadeExterna != value)
	    	          {
	    	              this.ValidateProperty("IdentidadeExterna", value);
	    	              this.OnIdentidadeExternaChanging(value);
	    	              this.RaiseDataMemberChanging("IdentidadeExterna");
	    	              this._IdentidadeExterna = value;
	    	              this.RaiseDataMemberChanged("IdentidadeExterna");
	    	              this.OnIdentidadeExternaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdIdentidadeExterna
	    partial void OnIdIdentidadeExternaChanging(Int64 value);
	    partial void OnIdIdentidadeExternaChanged();

	    private Int64 _IdIdentidadeExterna;

	    [DataMember(IsRequired = true, Name = "IdIdentidadeExterna", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Identidade Externa", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_IDENTIDADE_EXTERNA.ID_IDENTIDADE_EXTERNA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_IDENTIDADE_EXTERNA.ID_IDENTIDADE_EXTERNA")]
	    public Int64 IdIdentidadeExterna
	    {
	    	    get
	    	    {
	    	          return _IdIdentidadeExterna;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdIdentidadeExterna != value)
	    	          {
	    	              this.ValidateProperty("IdIdentidadeExterna", value);
	    	              this.OnIdIdentidadeExternaChanging(value);
	    	              this.RaiseDataMemberChanging("IdIdentidadeExterna");
	    	              this._IdIdentidadeExterna = value;
	    	              this.RaiseDataMemberChanged("IdIdentidadeExterna");
	    	              this.OnIdIdentidadeExternaChanged();
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
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_IDENTIDADE_EXTERNA.TCS_USUARIO_AUTENTICACAO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_IDENTIDADE_EXTERNA.TCS_USUARIO_AUTENTICACAO.ID_USUARIO")]
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
	    //Extensibility Partial Method Definitions For AutenticacaoWindows
	    partial void OnAutenticacaoWindowsChanging(Boolean value);
	    partial void OnAutenticacaoWindowsChanged();

	    private Boolean _AutenticacaoWindows;

	    [DataMember(IsRequired = true, Name = "AutenticacaoWindows", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Utiliza Autenticação Windows", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_IDENTIDADE_EXTERNA.TCS_USUARIO_AUTENTICACAO.AUTENTICACAO_WINDOWS];IsMeasure[false]")]
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
	    [Display(Name = "Bairro", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_IDENTIDADE_EXTERNA.TCS_USUARIO_AUTENTICACAO.BAIRRO];IsMeasure[false]")]
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
	    [Display(Name = "CEP", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_IDENTIDADE_EXTERNA.TCS_USUARIO_AUTENTICACAO.CEP];IsMeasure[false]")]
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
	    [Display(Name = "CPF/CNPJ", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[###.###.###-##];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_IDENTIDADE_EXTERNA.TCS_USUARIO_AUTENTICACAO.CNPJ_CPF];IsMeasure[false]")]
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
	    [Display(Name = "Complemento", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_IDENTIDADE_EXTERNA.TCS_USUARIO_AUTENTICACAO.COMPLEMENTO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For ConfirmacaoUsuario
	    partial void OnConfirmacaoUsuarioChanging(string value);
	    partial void OnConfirmacaoUsuarioChanged();

	    private string _ConfirmacaoUsuario;

	    [DataMember(Name = "ConfirmacaoUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Senha", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[''];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="\"\"")]
	    public string ConfirmacaoUsuario
	    {
	    	    get
	    	    {
	    	          return _ConfirmacaoUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._ConfirmacaoUsuario != value)
	    	          {
	    	              this.ValidateProperty("ConfirmacaoUsuario", value);
	    	              this.OnConfirmacaoUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("ConfirmacaoUsuario");
	    	              this._ConfirmacaoUsuario = value;
	    	              this.RaiseDataMemberChanged("ConfirmacaoUsuario");
	    	              this.OnConfirmacaoUsuarioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ConfirmacaoUsuario1
	    partial void OnConfirmacaoUsuario1Changing(string value);
	    partial void OnConfirmacaoUsuario1Changed();

	    private string _ConfirmacaoUsuario1;

	    [DataMember(Name = "ConfirmacaoUsuario1", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Confirmação", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[''];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="\"\"")]
	    public string ConfirmacaoUsuario1
	    {
	    	    get
	    	    {
	    	          return _ConfirmacaoUsuario1;
	    	    }
	    	    set
	    	    {
	    	          if (this._ConfirmacaoUsuario1 != value)
	    	          {
	    	              this.ValidateProperty("ConfirmacaoUsuario1", value);
	    	              this.OnConfirmacaoUsuario1Changing(value);
	    	              this.RaiseDataMemberChanging("ConfirmacaoUsuario1");
	    	              this._ConfirmacaoUsuario1 = value;
	    	              this.RaiseDataMemberChanged("ConfirmacaoUsuario1");
	    	              this.OnConfirmacaoUsuario1Changed();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For CriaUsuario
	    partial void OnCriaUsuarioChanging(bool value);
	    partial void OnCriaUsuarioChanged();

	    private bool _CriaUsuario;

	    [DataMember(IsRequired = true, Name = "CriaUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[false];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="false")]
	    public bool CriaUsuario
	    {
	    	    get
	    	    {
	    	          return _CriaUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._CriaUsuario != value)
	    	          {
	    	              this.ValidateProperty("CriaUsuario", value);
	    	              this.OnCriaUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("CriaUsuario");
	    	              this._CriaUsuario = value;
	    	              this.RaiseDataMemberChanged("CriaUsuario");
	    	              this.OnCriaUsuarioChanged();
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
	    [Display(Name = "Alteração", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[DateTime.Now];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_IDENTIDADE_EXTERNA.TCS_USUARIO_AUTENTICACAO.DATA_ALTERACAO];IsMeasure[false]")]
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
	    [Display(Name = "Cadastro", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[DateTime.Now];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_IDENTIDADE_EXTERNA.TCS_USUARIO_AUTENTICACAO.DATA_CADASTRO];IsMeasure[false]")]
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
	    [Display(Name = "Expiração Senha", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[DateTime.Now];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_IDENTIDADE_EXTERNA.TCS_USUARIO_AUTENTICACAO.DATA_EXPIRACAO_SENHA];IsMeasure[false]")]
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
	    [Display(Name = "Email", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_IDENTIDADE_EXTERNA.TCS_USUARIO_AUTENTICACAO.EMAIL];IsMeasure[false]")]
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
	    [Display(Name = "Móvel", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_IDENTIDADE_EXTERNA.TCS_USUARIO_AUTENTICACAO.FONE_CELULAR];IsMeasure[false]")]
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
	    [Display(Name = "Fixo / Ramal", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_IDENTIDADE_EXTERNA.TCS_USUARIO_AUTENTICACAO.FONE_FIXO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For GeraSenhaUsuario
	    partial void OnGeraSenhaUsuarioChanging(bool value);
	    partial void OnGeraSenhaUsuarioChanged();

	    private bool _GeraSenhaUsuario;

	    [DataMember(IsRequired = true, Name = "GeraSenhaUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[[GERA_SENHA_USUARIO]];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[false];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="false")]
	    public bool GeraSenhaUsuario
	    {
	    	    get
	    	    {
	    	          return _GeraSenhaUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._GeraSenhaUsuario != value)
	    	          {
	    	              this.ValidateProperty("GeraSenhaUsuario", value);
	    	              this.OnGeraSenhaUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("GeraSenhaUsuario");
	    	              this._GeraSenhaUsuario = value;
	    	              this.RaiseDataMemberChanged("GeraSenhaUsuario");
	    	              this.OnGeraSenhaUsuarioChanged();
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
	    [Display(Name = "Id Linx Empresa / Grupo Econômico", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_IDENTIDADE_EXTERNA.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For Inativo
	    partial void OnInativoChanging(Boolean value);
	    partial void OnInativoChanged();

	    private Boolean _Inativo;

	    [DataMember(IsRequired = true, Name = "Inativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inativo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_IDENTIDADE_EXTERNA.TCS_USUARIO_AUTENTICACAO.INATIVO];IsMeasure[false]")]
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
	    [Display(Name = "Permite Acesso de Suporte", Description="", Order = 14, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_IDENTIDADE_EXTERNA.TCS_USUARIO_AUTENTICACAO.INDICA_ACESSO_SUPORTE];IsMeasure[false]")]
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
	    [Display(Name = "Inscr. Estadual / RG", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_IDENTIDADE_EXTERNA.TCS_USUARIO_AUTENTICACAO.INSCR_ESTADUAL_RG];IsMeasure[false]")]
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
	    [Display(Name = "Logradouro", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_IDENTIDADE_EXTERNA.TCS_USUARIO_AUTENTICACAO.LOGRADOURO];IsMeasure[false]")]
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
	    [Display(Name = "Pessoa Física / Juridíca", Description="", Order = 14, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[LX_PFJ_FISICA_JURIDICA];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_IDENTIDADE_EXTERNA.TCS_USUARIO_AUTENTICACAO.LX_PFJ_FISICA_JURIDICA];IsMeasure[false]")]
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
	    [Display(Name = "Tipo Logradouro", Description="", Order = 15, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[LxTipoLogradouro];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_IDENTIDADE_EXTERNA.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO];IsMeasure[false]")]
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
	    [Display(Name = "Município", Description="", Order = 16, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_IDENTIDADE_EXTERNA.TCS_USUARIO_AUTENTICACAO.MUNICIPIO];IsMeasure[false]")]
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
	    [Display(Name = "Usuário Autenticação", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_IDENTIDADE_EXTERNA.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=true, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO")]
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
	    [Display(Name = "Apelido", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_IDENTIDADE_EXTERNA.TCS_USUARIO_AUTENTICACAO.NOME_CURTO_USUARIO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For NomeEmpresa
	    partial void OnNomeEmpresaChanging(System.String value);
	    partial void OnNomeEmpresaChanged();

	    private System.String _NomeEmpresa;

	    [DataMember(IsRequired = true, Name = "NomeEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Empresa / Grupo Econômico", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_IDENTIDADE_EXTERNA.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
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
	    //Extensibility Partial Method Definitions For NomeUsuario
	    partial void OnNomeUsuarioChanging(System.String value);
	    partial void OnNomeUsuarioChanged();

	    private System.String _NomeUsuario;

	    [DataMember(IsRequired = true, Name = "NomeUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_IDENTIDADE_EXTERNA.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=true, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.NOME_USUARIO")]
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
	    [Display(Name = "Número", Description="", Order = 20, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[Logradouro];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_IDENTIDADE_EXTERNA.TCS_USUARIO_AUTENTICACAO.NUMERO];IsMeasure[false]")]
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
	    [Display(Name = "Obs. Endereço", Description="", Order = 21, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_IDENTIDADE_EXTERNA.TCS_USUARIO_AUTENTICACAO.OBS_ENDERECO];IsMeasure[false]")]
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
	    [Display(Name = "Ramal", Description="", Order = 22, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(6)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[FoneFixo];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_IDENTIDADE_EXTERNA.TCS_USUARIO_AUTENTICACAO.RAMAL];IsMeasure[false]")]
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
	    [Display(Name = "UF", Description="", Order = 23, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(4)]
	    [FunctionalPoint("Precision[4:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[Municipio];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_IDENTIDADE_EXTERNA.TCS_USUARIO_AUTENTICACAO.UF];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For UidEmpresa
	    partial void OnUidEmpresaChanging(System.Guid value);
	    partial void OnUidEmpresaChanged();

	    private System.Guid _UidEmpresa;

	    [DataMember(IsRequired = true, Name = "UidEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Empresa", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_IDENTIDADE_EXTERNA.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA")]
	    public System.Guid UidEmpresa
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
	    partial void OnUidUsuarioChanging(System.Guid value);
	    partial void OnUidUsuarioChanged();

	    private System.Guid _UidUsuario;

	    [DataMember(Name = "UidUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Usuario", Description="", Order = 26, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_IDENTIDADE_EXTERNA.TCS_USUARIO_AUTENTICACAO.UID_USUARIO];IsMeasure[false]")]
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
	    [Display(Name = "Vigência Final", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[new DateTime(2099, 12, 31)];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_IDENTIDADE_EXTERNA.TCS_USUARIO_AUTENTICACAO.VIGENCIA_FINAL];IsMeasure[false]")]
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
	    [Display(Name = "Vigência Inicial", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[DateTime.Now];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_IDENTIDADE_EXTERNA.TCS_USUARIO_AUTENTICACAO.VIGENCIA_INICIAL];IsMeasure[false]")]
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

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_IDENTIDADE_EXTERNA").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_IDENTIDADE_EXTERNA), QualifiedEntitySetName = "AutorizacaoContext.TCS_IDENTIDADE_EXTERNA" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_IDENTIDADE_EXTERNA.IDENTIDADE_EXTERNA", Source = "IdentidadeExterna", Target = "IDENTIDADE_EXTERNA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_IDENTIDADE_EXTERNA", RelationPropertyName = "TCS_IDENTIDADE_EXTERNA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_IDENTIDADE_EXTERNA.ID_IDENTIDADE_EXTERNA", Source = "IdIdentidadeExterna", Target = "ID_IDENTIDADE_EXTERNA", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_IDENTIDADE_EXTERNA", RelationPropertyName = "TCS_IDENTIDADE_EXTERNA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_IDENTIDADE_EXTERNA.TCS_USUARIO_AUTENTICACAO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });

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
	    [Display(Name = "Pessoa Física / Juridíca", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    [Display(Name = "Tipo Logradouro", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
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

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Grupo Econômico];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#. as #Alias#];EdmEntityName[TCS_USUARIO_AUTENTICACAO_GPECON];EntityRelations[TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)#TCS_USUARIO_AUTENTICACAO(TCS_USUARIO_AUTENTICACAO)];EdmParentEntityName[TCS_USUARIO_AUTENTICACAO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioGpecon")]
	[Serializable()]
	public partial class TcsUsuarioGpeconParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For IdLinx
	    partial void OnIdLinxChanging(int value);
	    partial void OnIdLinxChanged();

	    private int _IdLinx;

	    [DataMember(IsRequired = true, Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx Empresa / Grupo Econômico", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsEmpresaAutenticacao];LookUpTitle[Seleção de (Id Linx Empresa / Grupo Econômico)];LookUpQuery[executeLookUpTcsEmpresaAutenticacao];LookUpFinalize[finalizeLookUpTcsEmpresaAutenticacao];LookUpDisplayColumns[{\"IdLinx\" : \"Id Linx Empresa / Grupo Econômico\", \"NomeEmpresa\" : \"Empresa / Grupo Econômico\"}];LookUpColumns[{\"IdLinx\" : true, \"NomeEmpresa\" : true}];FilterDataKey[TCS_USUARIO_AUTENTICACAO_GPECON.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="int#IdLinx#true##0:0##Id Linx Empresa / Grupo Econômico#0#true##::LookUpTcsEmpresaAutenticacao##true#false#TCS_EMPRESA_AUTENTICACAO#TCS_EMPRESA_AUTENTICACAO#Linx.Framework.BV.UsuarioAutorizacao#IQueryable###true#false", EdmKey="TCS_USUARIO_AUTENTICACAO_GPECON.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
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
	    //Extensibility Partial Method Definitions For IdTcsUsuarioAutGpecon
	    partial void OnIdTcsUsuarioAutGpeconChanging(int value);
	    partial void OnIdTcsUsuarioAutGpeconChanged();

	    private int _IdTcsUsuarioAutGpecon;

	    [DataMember(IsRequired = true, Name = "IdTcsUsuarioAutGpecon", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Aut Gpecon", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO_GPECON.ID_TCS_USUARIO_AUT_GPECON];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO_GPECON.ID_TCS_USUARIO_AUT_GPECON")]
	    public int IdTcsUsuarioAutGpecon
	    {
	    	    get
	    	    {
	    	          return _IdTcsUsuarioAutGpecon;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsUsuarioAutGpecon != value)
	    	          {
	    	              this.ValidateProperty("IdTcsUsuarioAutGpecon", value);
	    	              this.OnIdTcsUsuarioAutGpeconChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsUsuarioAutGpecon");
	    	              this._IdTcsUsuarioAutGpecon = value;
	    	              this.RaiseDataMemberChanged("IdTcsUsuarioAutGpecon");
	    	              this.OnIdTcsUsuarioAutGpeconChanged();
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
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.ID_USUARIO")]
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
	    //Extensibility Partial Method Definitions For NomeEmpresa
	    partial void OnNomeEmpresaChanging(string value);
	    partial void OnNomeEmpresaChanged();

	    private string _NomeEmpresa;

	    [DataMember(IsRequired = true, Name = "NomeEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Empresa / Grupo Econômico", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsEmpresaAutenticacao];LookUpTitle[Seleção de (Empresa / Grupo Econômico)];LookUpQuery[executeLookUpTcsEmpresaAutenticacao];LookUpFinalize[finalizeLookUpTcsEmpresaAutenticacao];LookUpDisplayColumns[{\"IdLinx\" : \"Id Linx Empresa / Grupo Econômico\", \"NomeEmpresa\" : \"Empresa / Grupo Econômico\"}];LookUpColumns[{\"IdLinx\" : true, \"NomeEmpresa\" : true}];FilterDataKey[TCS_USUARIO_AUTENTICACAO_GPECON.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#NomeEmpresa#false##250:0##Empresa / Grupo Econômico#1#true##::LookUpTcsEmpresaAutenticacao##true#false#TCS_EMPRESA_AUTENTICACAO#TCS_EMPRESA_AUTENTICACAO#Linx.Framework.BV.UsuarioAutorizacao#IQueryable###true#false", EdmKey="TCS_USUARIO_AUTENTICACAO_GPECON.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
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
	    //Extensibility Partial Method Definitions For NomeAutenticacao
	    partial void OnNomeAutenticacaoChanging(System.String value);
	    partial void OnNomeAutenticacaoChanged();

	    private System.String _NomeAutenticacao;

	    [DataMember(Name = "NomeAutenticacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário Autenticação", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO")]
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

	    [DataMember(Name = "NomeUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO")]
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
	    //Extensibility Partial Method Definitions For AutenticacaoWindows
	    partial void OnAutenticacaoWindowsChanging(Boolean value);
	    partial void OnAutenticacaoWindowsChanged();

	    private Boolean _AutenticacaoWindows;

	    [DataMember(IsRequired = true, Name = "AutenticacaoWindows", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Utiliza Autenticação Windows", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.AUTENTICACAO_WINDOWS];IsMeasure[false]")]
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
	    [Display(Name = "Bairro", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.BAIRRO];IsMeasure[false]")]
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
	    [Display(Name = "CEP", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.CEP];IsMeasure[false]")]
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
	    [Display(Name = "CPF/CNPJ", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[###.###.###-##];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.CNPJ_CPF];IsMeasure[false]")]
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
	    [Display(Name = "Complemento", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.COMPLEMENTO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For ConfirmacaoUsuario
	    partial void OnConfirmacaoUsuarioChanging(string value);
	    partial void OnConfirmacaoUsuarioChanged();

	    private string _ConfirmacaoUsuario;

	    [DataMember(Name = "ConfirmacaoUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Senha", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[''];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="\"\"")]
	    public string ConfirmacaoUsuario
	    {
	    	    get
	    	    {
	    	          return _ConfirmacaoUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._ConfirmacaoUsuario != value)
	    	          {
	    	              this.ValidateProperty("ConfirmacaoUsuario", value);
	    	              this.OnConfirmacaoUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("ConfirmacaoUsuario");
	    	              this._ConfirmacaoUsuario = value;
	    	              this.RaiseDataMemberChanged("ConfirmacaoUsuario");
	    	              this.OnConfirmacaoUsuarioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ConfirmacaoUsuario1
	    partial void OnConfirmacaoUsuario1Changing(string value);
	    partial void OnConfirmacaoUsuario1Changed();

	    private string _ConfirmacaoUsuario1;

	    [DataMember(Name = "ConfirmacaoUsuario1", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Confirmação", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[''];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="\"\"")]
	    public string ConfirmacaoUsuario1
	    {
	    	    get
	    	    {
	    	          return _ConfirmacaoUsuario1;
	    	    }
	    	    set
	    	    {
	    	          if (this._ConfirmacaoUsuario1 != value)
	    	          {
	    	              this.ValidateProperty("ConfirmacaoUsuario1", value);
	    	              this.OnConfirmacaoUsuario1Changing(value);
	    	              this.RaiseDataMemberChanging("ConfirmacaoUsuario1");
	    	              this._ConfirmacaoUsuario1 = value;
	    	              this.RaiseDataMemberChanged("ConfirmacaoUsuario1");
	    	              this.OnConfirmacaoUsuario1Changed();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For CriaUsuario
	    partial void OnCriaUsuarioChanging(bool value);
	    partial void OnCriaUsuarioChanged();

	    private bool _CriaUsuario;

	    [DataMember(IsRequired = true, Name = "CriaUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[false];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="false")]
	    public bool CriaUsuario
	    {
	    	    get
	    	    {
	    	          return _CriaUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._CriaUsuario != value)
	    	          {
	    	              this.ValidateProperty("CriaUsuario", value);
	    	              this.OnCriaUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("CriaUsuario");
	    	              this._CriaUsuario = value;
	    	              this.RaiseDataMemberChanged("CriaUsuario");
	    	              this.OnCriaUsuarioChanged();
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
	    [Display(Name = "Alteração", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[DateTime.Now];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.DATA_ALTERACAO];IsMeasure[false]")]
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
	    [Display(Name = "Cadastro", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[DateTime.Now];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.DATA_CADASTRO];IsMeasure[false]")]
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
	    [Display(Name = "Expiração Senha", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[DateTime.Now];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.DATA_EXPIRACAO_SENHA];IsMeasure[false]")]
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
	    [Display(Name = "Email", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.EMAIL];IsMeasure[false]")]
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
	    [Display(Name = "Móvel", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.FONE_CELULAR];IsMeasure[false]")]
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
	    [Display(Name = "Fixo / Ramal", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.FONE_FIXO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For GeraSenhaUsuario
	    partial void OnGeraSenhaUsuarioChanging(bool value);
	    partial void OnGeraSenhaUsuarioChanged();

	    private bool _GeraSenhaUsuario;

	    [DataMember(IsRequired = true, Name = "GeraSenhaUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[[GERA_SENHA_USUARIO]];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[false];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="false")]
	    public bool GeraSenhaUsuario
	    {
	    	    get
	    	    {
	    	          return _GeraSenhaUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._GeraSenhaUsuario != value)
	    	          {
	    	              this.ValidateProperty("GeraSenhaUsuario", value);
	    	              this.OnGeraSenhaUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("GeraSenhaUsuario");
	    	              this._GeraSenhaUsuario = value;
	    	              this.RaiseDataMemberChanged("GeraSenhaUsuario");
	    	              this.OnGeraSenhaUsuarioChanged();
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
	    [Display(Name = "Inativo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.INATIVO];IsMeasure[false]")]
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
	    [Display(Name = "Permite Acesso de Suporte", Description="", Order = 14, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.INDICA_ACESSO_SUPORTE];IsMeasure[false]")]
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
	    [Display(Name = "Inscr. Estadual / RG", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.INSCR_ESTADUAL_RG];IsMeasure[false]")]
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
	    [Display(Name = "Logradouro", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.LOGRADOURO];IsMeasure[false]")]
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
	    [Display(Name = "Pessoa Física / Juridíca", Description="", Order = 14, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[LX_PFJ_FISICA_JURIDICA];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.LX_PFJ_FISICA_JURIDICA];IsMeasure[false]")]
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
	    [Display(Name = "Tipo Logradouro", Description="", Order = 15, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[LxTipoLogradouro];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO];IsMeasure[false]")]
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
	    [Display(Name = "Município", Description="", Order = 16, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.MUNICIPIO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For NomeCurtoUsuario
	    partial void OnNomeCurtoUsuarioChanging(System.String value);
	    partial void OnNomeCurtoUsuarioChanged();

	    private System.String _NomeCurtoUsuario;

	    [DataMember(IsRequired = true, Name = "NomeCurtoUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Apelido", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.NOME_CURTO_USUARIO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For Numero
	    partial void OnNumeroChanging(System.String value);
	    partial void OnNumeroChanged();

	    private System.String _Numero;

	    [DataMember(Name = "Numero", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Número", Description="", Order = 20, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[Logradouro];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.NUMERO];IsMeasure[false]")]
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
	    [Display(Name = "Obs. Endereço", Description="", Order = 21, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.OBS_ENDERECO];IsMeasure[false]")]
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
	    [Display(Name = "Ramal", Description="", Order = 22, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(6)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[FoneFixo];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.RAMAL];IsMeasure[false]")]
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
	    [Display(Name = "UF", Description="", Order = 23, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(4)]
	    [FunctionalPoint("Precision[4:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[Municipio];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.UF];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For UidEmpresa
	    partial void OnUidEmpresaChanging(System.Guid value);
	    partial void OnUidEmpresaChanged();

	    private System.Guid _UidEmpresa;

	    [DataMember(IsRequired = true, Name = "UidEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Empresa", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA")]
	    public System.Guid UidEmpresa
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
	    partial void OnUidUsuarioChanging(System.Guid value);
	    partial void OnUidUsuarioChanged();

	    private System.Guid _UidUsuario;

	    [DataMember(Name = "UidUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Usuario", Description="", Order = 26, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.UID_USUARIO];IsMeasure[false]")]
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
	    [Display(Name = "Vigência Final", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[new DateTime(2099, 12, 31)];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.VIGENCIA_FINAL];IsMeasure[false]")]
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
	    [Display(Name = "Vigência Inicial", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[DateTime.Now];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.VIGENCIA_INICIAL];IsMeasure[false]")]
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

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO_GPECON").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_USUARIO_AUTENTICACAO_GPECON), QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO_GPECON" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO_GPECON.ID_TCS_USUARIO_AUT_GPECON", Source = "IdTcsUsuarioAutGpecon", Target = "ID_TCS_USUARIO_AUT_GPECON", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO_GPECON", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO_GPECON" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO_GPECON.TCS_EMPRESA_AUTENTICACAO.ID_LINX", Source = "IdLinx", Target = "ID_LINX", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_AUTENTICACAO", RelationPropertyName = "TCS_EMPRESA_AUTENTICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_AUTENTICACAO", RelationPropertyName = "TCS_USUARIO_AUTENTICACAO" });

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
	    [Display(Name = "Pessoa Física / Juridíca", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    [Display(Name = "Tipo Logradouro", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxTipoLogradouroName
	    {
	    	    get { if (this.LxTipoLogradouro.IsNull()) { _lxTipoLogradouroName = String.Empty; } else { string key = this.LxTipoLogradouro.ToString(); var dmValues = this.GetLxTipoLogradouroValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxTipoLogradouroName) _lxTipoLogradouroName = domainName; } return _lxTipoLogradouroName; } set { _lxTipoLogradouroName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	//////////////////////// DomainService Class V1 ///////////////////////
	///////////////////////////////////////////////////////////////////////
	[EnableClientAccess()]	
	[DomainIdentifier("ProcessorOverviewUsuarioAutorizacaoDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class UsuarioAutorizacaoDomainService : DomainService, IDataServiceContext 
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

		
	    public UsuarioAutorizacaoDomainService() : this("", null, null) { }
	    public UsuarioAutorizacaoDomainService(string connectionString) : this(connectionString, null, null) { }
	    public UsuarioAutorizacaoDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public UsuarioAutorizacaoDomainService(Linx.Framework.Autorizacao.BM.AutorizacaoContext dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public UsuarioAutorizacaoDomainService(string connectionString, Linx.Framework.Autorizacao.BM.AutorizacaoContext dataContext, Dictionary<string, string> headers) : base() 
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
	
	    
	        foreach (ChangeSetEntry entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioAutenticacao))
	        {
	            ((TcsUsuarioAutenticacao)entry.Entity).OnSavingChanges(this, changeSet.GetChangeOperation(entry.Entity));
	        }
    
	        TcsUsuarioAcesso.OnSavingContextChanges(this, changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioAcesso).ToArray());
    	
	    }
	
	    private void SaveMedia(ChangeSet changeSet)
	    {
	    		foreach (ChangeSetEntry entry in changeSet.ChangeSetEntries)
	    		{
	    		}
	    }

	    private void OnSavedChanges(ChangeSet changeSet)
	    {
	
	
	        TcsUsuarioAutenticacao.OnSavedContextChanges(this, changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioAutenticacao).ToArray());
    
	        TcsUsuarioAcesso.OnSavedContextChanges(this, changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioAcesso).ToArray());
    
	        TcsUsuarioGpecon.OnSavedContextChanges(this, changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioGpecon).ToArray());
    	
	    }
		
	    private void OnTransactingChanges(ChangeSet changeSet)
	    {
	
	    
	        foreach (ChangeSetEntry entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioAutenticacao))
	        {
	            ((TcsUsuarioAutenticacao)entry.Entity).OnTransactingChanges(this, changeSet.GetChangeOperation(entry.Entity));
	        }
    	
	    }
	
	    private void OnTransactedChanges(ChangeSet changeSet)
	    {
	
	    
	        foreach (ChangeSetEntry entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioAutenticacao))
	        {
	            ((TcsUsuarioAutenticacao)entry.Entity).OnTransactedChanges(this, changeSet.GetChangeOperation(entry.Entity));
	        }
    
	        TcsUsuarioAutenticacao.OnTransactedContextChanges(this, changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioAutenticacao).ToArray());
    	
	    }
		
	    #endregion Entity Event Call Definitions
	
	    #region Transaction Control.
	
	    TransactionScope transactionScope = null;	
	
	    //Adjust Hierarchy Composition
	    private ChangeSet AdjustHierarchyForSaving(ChangeSet changeSet)
	    {

		
 
 	        bool createNewChangeSet = false;
 
 	        //Adjust data hierarchy
 	        var _TcsUsuarioAutenticacaoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioAutenticacao && e.Entity.GetType().Name == "TcsUsuarioAutenticacao" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _TcsUsuarioAutenticacaoElements)
 	           if (((TcsUsuarioAutenticacao)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 
 	        //Remove inconsistent details
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioAcesso && e.Entity.GetType().Name == "TcsUsuarioAcesso" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsIdentidadeExterna && e.Entity.GetType().Name == "TcsIdentidadeExterna" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioGpecon && e.Entity.GetType().Name == "TcsUsuarioGpecon" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
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
	    //Get All LookUpTcsUsuarioEmpresaAutenticacao.
	    public IQueryable<LookUpTcsUsuarioEmpresaAutenticacao> GetAllLookUpTcsUsuarioEmpresaAutenticacao()
	    {
	        return this.GetLookUpTcsUsuarioEmpresaAutenticacao(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsUsuarioEmpresaAutenticacao By EntitySearch.
	    public IQueryable<LookUpTcsUsuarioEmpresaAutenticacao> GetLookUpTcsUsuarioEmpresaAutenticacaoByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsUsuarioEmpresaAutenticacao(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsUsuarioEmpresaAutenticacao.
	    public IQueryable<LookUpTcsUsuarioEmpresaAutenticacao> GetLookUpTcsUsuarioEmpresaAutenticacao(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TCS_EMPRESA_AUTENTICACAO" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsUsuarioEmpresaAutenticacao";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsUsuarioEmpresaAutenticacao));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsUsuarioEmpresaAutenticacao> query =  
	
	            (from entity in this.DbContext.TCS_EMPRESA_AUTENTICACAO.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpTcsUsuarioEmpresaAutenticacao()		
	            {
	            
                IdLinx = entity.ID_LINX
                , NomeEmpresa = entity.NOME_EMPRESA
                , UidEmpresa = entity.UID_EMPRESA
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
                  let entityAl1 = entity.TCS_APLICACAO
                  let entityAl2 = entity.TCS_EMPRESA_AUTENTICACAO
	            
	            select new LookUpTcsAmbiente()		
	            {
	            
                DescricaoAmbiente = entity.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entityAl1.DESCRICAO_APLICACAO
                , EmDesenvolvimento = entityAl1.EM_DESENVOLVIMENTO
                , IdLinxEmpresa = entityAl2.ID_LINX
                , DescricaoAplicativo = entityAl1.TCS_APLICATIVO.DESCRICAO_APLICATIVO
                , IdTcsAmbiente = entity.ID_TCS_AMBIENTE
                , NomeEmpresa = entityAl2.NOME_EMPRESA
                , UidAplicacao = entityAl1.UID_APLICACAO
                , UidEmpresa = entityAl2.UID_EMPRESA
                , Url = entityAl1.URL
                , IdTcsAplicativo = entityAl1.TCS_APLICATIVO.ID_TCS_APLICATIVO
	            });

	            
            //Inner Group Definition
            if (propertyName.InList("DescricaoAplicacao", "EmDesenvolvimento", "UidAplicacao", "Url"))
            {
               query = (from r in query select new LookUpTcsAmbiente() {
               DescricaoAmbiente = ""
               , DescricaoAplicacao = r.DescricaoAplicacao
               , EmDesenvolvimento = r.EmDesenvolvimento
               , IdLinxEmpresa = default(Int32)
               , DescricaoAplicativo = r.DescricaoAplicativo
               , IdTcsAmbiente = default(Int32)
               , NomeEmpresa = ""
               , UidAplicacao = r.UidAplicacao
               , UidEmpresa = default(System.Guid)
               , Url = r.Url
               , IdTcsAplicativo = r.IdTcsAplicativo
                }).Distinct();
            }
            else if (propertyName.InList("DescricaoAplicativo", "IdTcsAplicativo"))
            {
               query = (from r in query select new LookUpTcsAmbiente() {
               DescricaoAmbiente = ""
               , DescricaoAplicacao = ""
               , EmDesenvolvimento = default(Boolean)
               , IdLinxEmpresa = default(Int32)
               , DescricaoAplicativo = r.DescricaoAplicativo
               , IdTcsAmbiente = default(Int32)
               , NomeEmpresa = ""
               , UidAplicacao = default(System.Guid)
               , UidEmpresa = default(System.Guid)
               , Url = ""
               , IdTcsAplicativo = r.IdTcsAplicativo
                }).Distinct();
            }
	
		
			
		
	        TcsUsuarioAcesso.OnLookUpingLookUpTcsAmbiente(ref query, propertyName, entitySearch);
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpTcsAmbiente1.
	    public IQueryable<LookUpTcsAmbiente1> GetAllLookUpTcsAmbiente1()
	    {
	        return this.GetLookUpTcsAmbiente1(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsAmbiente1 By EntitySearch.
	    public IQueryable<LookUpTcsAmbiente1> GetLookUpTcsAmbiente1ByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsAmbiente1(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsAmbiente1.
	    public IQueryable<LookUpTcsAmbiente1> GetLookUpTcsAmbiente1(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TCS_AMBIENTE" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsAmbiente1";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsAmbiente1));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsAmbiente1> query =  
	
	            (from entity in this.DbContext.TCS_AMBIENTE.Where(dynQuery, parameters.ToArray())
                  let entityAl2 = entity.TCS_APLICACAO
                  let entityAl1 = entity.TCS_EMPRESA_AUTENTICACAO
	            
	            select new LookUpTcsAmbiente1()		
	            {
	            
                DescricaoAmbienteRelacionado = entity.DESCRICAO_AMBIENTE
                , NomeEmpresaAmbienteRelacionado = entityAl1.NOME_EMPRESA
                , DescricaoAplicacaoAmbienteRelacionado = entityAl2.DESCRICAO_APLICACAO
                , IdLinxAmbienteRelacionado = entityAl1.ID_LINX
                , IdTcsAmbienteRelacionado = entity.ID_TCS_AMBIENTE
                , IdAplicacao = entityAl2.ID_APLICACAO
	            });

	            
            //Inner Group Definition
            if (propertyName.InList("NomeEmpresaAmbienteRelacionado", "IdLinxAmbienteRelacionado"))
            {
               query = (from r in query select new LookUpTcsAmbiente1() {
               DescricaoAmbienteRelacionado = ""
               , NomeEmpresaAmbienteRelacionado = r.NomeEmpresaAmbienteRelacionado
               , DescricaoAplicacaoAmbienteRelacionado = ""
               , IdLinxAmbienteRelacionado = r.IdLinxAmbienteRelacionado
               , IdTcsAmbienteRelacionado = default(System.Nullable<Int32>)
               , IdAplicacao = default(System.Nullable<Int32>)
                }).Distinct();
            }
	
		
			
		
	        TcsUsuarioAcesso.OnLookingUpLookUpTcsAmbiente1(ref query, propertyName, entitySearch);
	
	
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
	            
                IdLinx = entity.ID_LINX
                , NomeEmpresa = entity.NOME_EMPRESA
	            });

	            
	
		
			
		
	        TcsUsuarioGpecon.OnLookingUpLookUpTcsEmpresaAutenticacao(ref query, propertyName, entitySearch);
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpTcsAmbiente2Relacionado.
	    public IQueryable<LookUpTcsAmbiente2Relacionado> GetAllLookUpTcsAmbiente2Relacionado()
	    {
	        return this.GetLookUpTcsAmbiente2Relacionado(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsAmbiente2Relacionado By EntitySearch.
	    public IQueryable<LookUpTcsAmbiente2Relacionado> GetLookUpTcsAmbiente2RelacionadoByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsAmbiente2Relacionado(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsAmbiente2Relacionado.
	    public IQueryable<LookUpTcsAmbiente2Relacionado> GetLookUpTcsAmbiente2Relacionado(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsAmbiente2Relacionado";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsAmbiente2Relacionado));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsAmbiente2Relacionado> query =  null;
		
			
		
	        TcsUsuarioAutenticacaoAcessoP.OnLookingUpLookUpTcsAmbiente2Relacionado(ref query, propertyName, entitySearch);
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpTcsAmbiente2.
	    public IQueryable<LookUpTcsAmbiente2> GetAllLookUpTcsAmbiente2()
	    {
	        return this.GetLookUpTcsAmbiente2(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsAmbiente2 By EntitySearch.
	    public IQueryable<LookUpTcsAmbiente2> GetLookUpTcsAmbiente2ByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsAmbiente2(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsAmbiente2.
	    public IQueryable<LookUpTcsAmbiente2> GetLookUpTcsAmbiente2(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsAmbiente2";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsAmbiente2));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsAmbiente2> query =  null;
		
			
		
	        TcsUsuarioAutenticacaoAcessoP.OnLookingUpLookUpTcsAmbiente2(ref query, propertyName, entitySearch);
	
	
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
	
		

	        if (entityName.InList("Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacao"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsUsuarioAutenticacao",
	        			NameSpace = "Linx.Framework.BV.UsuarioAutorizacao",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsUsuarioAutenticacao",
	        			ClearMethodName = "ClearTcsUsuarioAutenticacao",
	        			QueryMethodName  = "GetPagedTcsUsuarioAutenticacao",	
	        			CountingMethodName  = "GetTcsUsuarioAutenticacao" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacao"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacao"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacao", "Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAcesso"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsUsuarioAcesso" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.UsuarioAutorizacao",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsUsuarioAutenticacao",	
	        			DisplayName = "Acessos",
	        			ClearMethodName = "ClearTcsUsuarioAcesso" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsUsuarioAcesso" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsUsuarioAcesso" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAcesso"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAcesso" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacao", "Linx.Framework.BV.UsuarioAutorizacao.TcsIdentidadeExterna"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsIdentidadeExterna" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.UsuarioAutorizacao",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsUsuarioAutenticacao",	
	        			DisplayName = "Identidade Externa",
	        			ClearMethodName = "ClearTcsIdentidadeExterna" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsIdentidadeExterna" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsIdentidadeExterna" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.UsuarioAutorizacao.TcsIdentidadeExterna"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.UsuarioAutorizacao.TcsIdentidadeExterna" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacao", "Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioGpecon"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsUsuarioGpecon" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.UsuarioAutorizacao",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsUsuarioAutenticacao",	
	        			DisplayName = "Grupo Econômico",
	        			ClearMethodName = "ClearTcsUsuarioGpecon" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedTcsUsuarioGpecon" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetTcsUsuarioGpecon" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioGpecon"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioGpecon" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.UsuarioAutorizacao.RequisicaoAcesso"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "RequisicaoAcesso",
	        			NameSpace = "Linx.Framework.BV.UsuarioAutorizacao",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "RequisicaoAcesso",
	        			ClearMethodName = "ClearRequisicaoAcesso",
	        			QueryMethodName  = "GetPagedRequisicaoAcesso",	
	        			CountingMethodName  = "GetRequisicaoAcesso" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.UsuarioAutorizacao.RequisicaoAcesso"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.UsuarioAutorizacao.RequisicaoAcesso"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.UsuarioAutorizacao.UsuarioAcesso"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "UsuarioAcesso",
	        			NameSpace = "Linx.Framework.BV.UsuarioAutorizacao",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "UsuarioAcesso",
	        			ClearMethodName = "ClearUsuarioAcesso",
	        			QueryMethodName  = "GetPagedUsuarioAcesso",	
	        			CountingMethodName  = "GetUsuarioAcesso" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.UsuarioAutorizacao.UsuarioAcesso"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.UsuarioAutorizacao.UsuarioAcesso"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.UsuarioAutorizacao.TcsSuporteAcessoLog"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsSuporteAcessoLog",
	        			NameSpace = "Linx.Framework.BV.UsuarioAutorizacao",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsSuporteAcessoLog",
	        			ClearMethodName = "ClearTcsSuporteAcessoLog",
	        			QueryMethodName  = "GetPagedTcsSuporteAcessoLog",	
	        			CountingMethodName  = "GetTcsSuporteAcessoLog" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.UsuarioAutorizacao.TcsSuporteAcessoLog"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.UsuarioAutorizacao.TcsSuporteAcessoLog"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.UsuarioAutorizacao.RequisicaoSuporte"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "RequisicaoSuporte",
	        			NameSpace = "Linx.Framework.BV.UsuarioAutorizacao",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "RequisicaoSuporte",
	        			ClearMethodName = "ClearRequisicaoSuporte",
	        			QueryMethodName  = "GetPagedRequisicaoSuporte",	
	        			CountingMethodName  = "GetRequisicaoSuporte" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.UsuarioAutorizacao.RequisicaoSuporte"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.UsuarioAutorizacao.RequisicaoSuporte"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAcessoAmbiente"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsUsuarioAcessoAmbiente",
	        			NameSpace = "Linx.Framework.BV.UsuarioAutorizacao",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsUsuarioAcessoAmbiente",
	        			ClearMethodName = "ClearTcsUsuarioAcessoAmbiente",
	        			QueryMethodName  = "GetPagedTcsUsuarioAcessoAmbiente",	
	        			CountingMethodName  = "GetTcsUsuarioAcessoAmbiente" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAcessoAmbiente"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAcessoAmbiente"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacaoAcessoP"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsUsuarioAutenticacaoAcessoP",
	        			NameSpace = "Linx.Framework.BV.UsuarioAutorizacao",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "Acessos",
	        			ClearMethodName = "ClearTcsUsuarioAutenticacaoAcessoP",
	        			QueryMethodName  = "GetPagedTcsUsuarioAutenticacaoAcessoP",	
	        			CountingMethodName  = "GetTcsUsuarioAutenticacaoAcessoP" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacaoAcessoP"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacaoAcessoP"), forceAll: forceAll)
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

         		    return new string[] { "Framework_UsuarioAutorizacaoClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.UsuarioAutorizacaoClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Framework_usuarioAutorizacaoService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.usuarioAutorizacaoService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear TcsUsuarioAutenticacao.
	    public IEnumerable<TcsUsuarioAutenticacao> ClearTcsUsuarioAutenticacao()
	    {
	        List<TcsUsuarioAutenticacao> result = new List<TcsUsuarioAutenticacao>();
	        result.Add(new TcsUsuarioAutenticacao(false));	
			
	        result[0].TcsUsuarioAcessoList = new List<TcsUsuarioAcesso>();
	        ((List<TcsUsuarioAcesso>)result[0].TcsUsuarioAcessoList).Add(new TcsUsuarioAcesso());
			
	        result[0].TcsIdentidadeExternaList = new List<TcsIdentidadeExterna>();
	        ((List<TcsIdentidadeExterna>)result[0].TcsIdentidadeExternaList).Add(new TcsIdentidadeExterna());
			
	        result[0].TcsUsuarioGpeconList = new List<TcsUsuarioGpecon>();
	        ((List<TcsUsuarioGpecon>)result[0].TcsUsuarioGpeconList).Add(new TcsUsuarioGpecon());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsUsuarioAcesso.
	    public IEnumerable<TcsUsuarioAcesso> ClearTcsUsuarioAcesso()
	    {
	        List<TcsUsuarioAcesso> result = new List<TcsUsuarioAcesso>();
	        result.Add(new TcsUsuarioAcesso());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsIdentidadeExterna.
	    public IEnumerable<TcsIdentidadeExterna> ClearTcsIdentidadeExterna()
	    {
	        List<TcsIdentidadeExterna> result = new List<TcsIdentidadeExterna>();
	        result.Add(new TcsIdentidadeExterna());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsUsuarioGpecon.
	    public IEnumerable<TcsUsuarioGpecon> ClearTcsUsuarioGpecon()
	    {
	        List<TcsUsuarioGpecon> result = new List<TcsUsuarioGpecon>();
	        result.Add(new TcsUsuarioGpecon());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear RequisicaoAcesso.
	    public IEnumerable<RequisicaoAcesso> ClearRequisicaoAcesso()
	    {
	        List<RequisicaoAcesso> result = new List<RequisicaoAcesso>();
	        result.Add(new RequisicaoAcesso());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear UsuarioAcesso.
	    public IEnumerable<UsuarioAcesso> ClearUsuarioAcesso()
	    {
	        List<UsuarioAcesso> result = new List<UsuarioAcesso>();
	        result.Add(new UsuarioAcesso());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsSuporteAcessoLog.
	    public IEnumerable<TcsSuporteAcessoLog> ClearTcsSuporteAcessoLog()
	    {
	        List<TcsSuporteAcessoLog> result = new List<TcsSuporteAcessoLog>();
	        result.Add(new TcsSuporteAcessoLog());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear RequisicaoSuporte.
	    public IEnumerable<RequisicaoSuporte> ClearRequisicaoSuporte()
	    {
	        List<RequisicaoSuporte> result = new List<RequisicaoSuporte>();
	        result.Add(new RequisicaoSuporte());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsUsuarioAcessoAmbiente.
	    public IEnumerable<TcsUsuarioAcessoAmbiente> ClearTcsUsuarioAcessoAmbiente()
	    {
	        List<TcsUsuarioAcessoAmbiente> result = new List<TcsUsuarioAcessoAmbiente>();
	        result.Add(new TcsUsuarioAcessoAmbiente());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsUsuarioAutenticacaoAcessoP.
	    public IEnumerable<TcsUsuarioAutenticacaoAcessoP> ClearTcsUsuarioAutenticacaoAcessoP()
	    {
	        List<TcsUsuarioAutenticacaoAcessoP> result = new List<TcsUsuarioAutenticacaoAcessoP>();
	        result.Add(new TcsUsuarioAutenticacaoAcessoP());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsUsuarioAutenticacao.
	    public IQueryable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacao()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioAutenticacao> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_AUTENTICACAO
                  let entity0Al1 = entity0.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsUsuarioAutenticacao()		
	            {
	            
                AutenticacaoWindows = entity0.AUTENTICACAO_WINDOWS
                , Bairro = entity0.BAIRRO
                , Cep = entity0.CEP
                , CnpjCpf = entity0.CNPJ_CPF
                , Complemento = entity0.COMPLEMENTO
                , ConfirmacaoUsuario = ""
                , ConfirmacaoUsuario1 = ""
                , CriaUsuario = false
                , DataAlteracao = entity0.DATA_ALTERACAO
                , DataCadastro = entity0.DATA_CADASTRO
                , DataExpiracaoSenha = entity0.DATA_EXPIRACAO_SENHA
                , Email = entity0.EMAIL
                , FoneCelular = entity0.FONE_CELULAR
                , FoneFixo = entity0.FONE_FIXO
                , GeraSenhaUsuario = false
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
                , NomeEmpresa = entity0Al1.NOME_EMPRESA
                , NomeUsuario = entity0.NOME_USUARIO
                , Numero = entity0.NUMERO
                , ObsEndereco = entity0.OBS_ENDERECO
                , Ramal = entity0.RAMAL
                , Uf = entity0.UF
                , UidEmpresa = entity0Al1.UID_EMPRESA
                , UidUsuario = entity0.UID_USUARIO
                , VigenciaFinal = entity0.VIGENCIA_FINAL
                , VigenciaInicial = entity0.VIGENCIA_INICIAL
			
                ,TcsUsuarioAcessoList = 
	                        (from entity1 in entity0.TCS_USUARIO_ACESSO_LISTA
                                  let entity1Al1 = entity1.TCS_AMBIENTE
                                  let entity1Al2 = entity1.TCS_AMBIENTE1
                                  let entity1Al8 = entity1.TCS_USUARIO_AUTENTICACAO
                                  let entity1Al3 = entity1.TCS_AMBIENTE.TCS_APLICACAO
                                  let entity1Al4 = entity1.TCS_AMBIENTE1.TCS_APLICACAO
                                  let entity1Al6 = entity1.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                                  let entity1Al5 = entity1.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO
                                  let entity1Al7 = entity1.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
	                        
	                        	
	                        select new TcsUsuarioAcesso()
	                        {
	                        
                                DescricaoAmbiente = entity1Al1.DESCRICAO_AMBIENTE
                                , DescricaoAmbienteRelacionado = entity1Al2.DESCRICAO_AMBIENTE
                                , DescricaoAplicacao = entity1Al3.DESCRICAO_APLICACAO
                                , DescricaoAplicacaoAmbienteRelacionado = entity1Al4.DESCRICAO_APLICACAO
                                , EmDesenvolvimento = entity1Al3.EM_DESENVOLVIMENTO
                                , IdAplicacao = entity1Al4.ID_APLICACAO
                                , IdAplicacaoAmbiente = entity1Al3.ID_APLICACAO
                                , IdLinxAmbienteRelacionado = entity1Al5.ID_LINX
                                , IdLinxEmpresa = entity1Al6.ID_LINX
                                , IdTcsAmbiente = entity1Al1.ID_TCS_AMBIENTE
                                , IdTcsAmbienteRelacionado = entity1Al2.ID_TCS_AMBIENTE
                                , IdTcsAplicativo = entity1Al7.ID_TCS_APLICATIVO
                                , IdTcsUsuarioAcesso = entity1.ID_TCS_USUARIO_ACESSO
                                , IdUsuario = entity1Al8.ID_USUARIO
                                , IndicaAcessoPadrao = entity1.INDICA_ACESSO_PADRAO
                                , IndicaAdministrador = entity1.INDICA_ADMINISTRADOR
                                , IndicaMultiGpecon = entity1.INDICA_MULTI_GPECON
                                , NomeEmpresaAmbienteRelacionado = entity1Al5.NOME_EMPRESA
                                , NomeAutenticacao = entity1Al8.NOME_AUTENTICACAO
                                , NomeUsuario = entity1Al8.NOME_USUARIO
		
	                        }
	                        )
			
                ,TcsIdentidadeExternaList = 
	                        (from entity1 in entity0.TCS_IDENTIDADE_EXTERNA_LISTA
                                  let entity1Al1 = entity1.TCS_USUARIO_AUTENTICACAO
	                        
	                        	
	                        select new TcsIdentidadeExterna()
	                        {
	                        
                                IdentidadeExterna = entity1.IDENTIDADE_EXTERNA
                                , IdIdentidadeExterna = entity1.ID_IDENTIDADE_EXTERNA
                                , IdUsuario = entity1Al1.ID_USUARIO
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsUsuarioAcesso.
	    public IQueryable<TcsUsuarioAcesso> GetTcsUsuarioAcesso()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioAcesso> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_ACESSO
                  let entity0Al1 = entity0.TCS_AMBIENTE
                  let entity0Al2 = entity0.TCS_AMBIENTE1
                  let entity0Al8 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al3 = entity0.TCS_AMBIENTE.TCS_APLICACAO
                  let entity0Al4 = entity0.TCS_AMBIENTE1.TCS_APLICACAO
                  let entity0Al6 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al5 = entity0.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al7 = entity0.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
	            
	            	
	            select new TcsUsuarioAcesso()		
	            {
	            
                DescricaoAmbiente = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAmbienteRelacionado = entity0Al2.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al3.DESCRICAO_APLICACAO
                , DescricaoAplicacaoAmbienteRelacionado = entity0Al4.DESCRICAO_APLICACAO
                , EmDesenvolvimento = entity0Al3.EM_DESENVOLVIMENTO
                , IdAplicacao = entity0Al4.ID_APLICACAO
                , IdAplicacaoAmbiente = entity0Al3.ID_APLICACAO
                , IdLinxAmbienteRelacionado = entity0Al5.ID_LINX
                , IdLinxEmpresa = entity0Al6.ID_LINX
                , IdTcsAmbiente = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsAmbienteRelacionado = entity0Al2.ID_TCS_AMBIENTE
                , IdTcsAplicativo = entity0Al7.ID_TCS_APLICATIVO
                , IdTcsUsuarioAcesso = entity0.ID_TCS_USUARIO_ACESSO
                , IdUsuario = entity0Al8.ID_USUARIO
                , IndicaAcessoPadrao = entity0.INDICA_ACESSO_PADRAO
                , IndicaAdministrador = entity0.INDICA_ADMINISTRADOR
                , IndicaMultiGpecon = entity0.INDICA_MULTI_GPECON
                , NomeEmpresaAmbienteRelacionado = entity0Al5.NOME_EMPRESA
                , NomeAutenticacao = entity0Al8.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al8.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsIdentidadeExterna.
	    public IQueryable<TcsIdentidadeExterna> GetTcsIdentidadeExterna()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsIdentidadeExterna> result = 
	            (from entity0 in this.DbContext.TCS_IDENTIDADE_EXTERNA
                  let entity0Al1 = entity0.TCS_USUARIO_AUTENTICACAO
	            
	            	
	            select new TcsIdentidadeExterna()		
	            {
	            
                IdentidadeExterna = entity0.IDENTIDADE_EXTERNA
                , IdIdentidadeExterna = entity0.ID_IDENTIDADE_EXTERNA
                , IdUsuario = entity0Al1.ID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsUsuarioGpecon.
	    public IQueryable<TcsUsuarioGpecon> GetTcsUsuarioGpecon()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioGpecon> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_AUTENTICACAO_GPECON
                  let entity0Al1 = entity0.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_USUARIO_AUTENTICACAO
	            
	            	
	            select new TcsUsuarioGpecon()		
	            {
	            
                IdLinx = entity0Al1.ID_LINX
                , IdTcsUsuarioAutGpecon = entity0.ID_TCS_USUARIO_AUT_GPECON
                , IdUsuario = entity0Al2.ID_USUARIO
                , NomeEmpresa = entity0Al1.NOME_EMPRESA
                , NomeAutenticacao = entity0Al2.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al2.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioAutenticacaoNoAssociations.
	    public IQueryable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioAutenticacao> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_AUTENTICACAO
                  let entity0Al1 = entity0.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsUsuarioAutenticacao()		
	            {
	            
                AutenticacaoWindows = entity0.AUTENTICACAO_WINDOWS
                , Bairro = entity0.BAIRRO
                , Cep = entity0.CEP
                , CnpjCpf = entity0.CNPJ_CPF
                , Complemento = entity0.COMPLEMENTO
                , ConfirmacaoUsuario = ""
                , ConfirmacaoUsuario1 = ""
                , CriaUsuario = false
                , DataAlteracao = entity0.DATA_ALTERACAO
                , DataCadastro = entity0.DATA_CADASTRO
                , DataExpiracaoSenha = entity0.DATA_EXPIRACAO_SENHA
                , Email = entity0.EMAIL
                , FoneCelular = entity0.FONE_CELULAR
                , FoneFixo = entity0.FONE_FIXO
                , GeraSenhaUsuario = false
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
                , NomeEmpresa = entity0Al1.NOME_EMPRESA
                , NomeUsuario = entity0.NOME_USUARIO
                , Numero = entity0.NUMERO
                , ObsEndereco = entity0.OBS_ENDERECO
                , Ramal = entity0.RAMAL
                , Uf = entity0.UF
                , UidEmpresa = entity0Al1.UID_EMPRESA
                , UidUsuario = entity0.UID_USUARIO
                , VigenciaFinal = entity0.VIGENCIA_FINAL
                , VigenciaInicial = entity0.VIGENCIA_INICIAL
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioAcessoNoAssociations.
	    public IQueryable<TcsUsuarioAcesso> GetTcsUsuarioAcessoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioAcesso> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_ACESSO
                  let entity0Al1 = entity0.TCS_AMBIENTE
                  let entity0Al2 = entity0.TCS_AMBIENTE1
                  let entity0Al8 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al3 = entity0.TCS_AMBIENTE.TCS_APLICACAO
                  let entity0Al4 = entity0.TCS_AMBIENTE1.TCS_APLICACAO
                  let entity0Al6 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al5 = entity0.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al7 = entity0.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
	            
	            	
	            select new TcsUsuarioAcesso()		
	            {
	            
                DescricaoAmbiente = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAmbienteRelacionado = entity0Al2.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al3.DESCRICAO_APLICACAO
                , DescricaoAplicacaoAmbienteRelacionado = entity0Al4.DESCRICAO_APLICACAO
                , EmDesenvolvimento = entity0Al3.EM_DESENVOLVIMENTO
                , IdAplicacao = entity0Al4.ID_APLICACAO
                , IdAplicacaoAmbiente = entity0Al3.ID_APLICACAO
                , IdLinxAmbienteRelacionado = entity0Al5.ID_LINX
                , IdLinxEmpresa = entity0Al6.ID_LINX
                , IdTcsAmbiente = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsAmbienteRelacionado = entity0Al2.ID_TCS_AMBIENTE
                , IdTcsAplicativo = entity0Al7.ID_TCS_APLICATIVO
                , IdTcsUsuarioAcesso = entity0.ID_TCS_USUARIO_ACESSO
                , IdUsuario = entity0Al8.ID_USUARIO
                , IndicaAcessoPadrao = entity0.INDICA_ACESSO_PADRAO
                , IndicaAdministrador = entity0.INDICA_ADMINISTRADOR
                , IndicaMultiGpecon = entity0.INDICA_MULTI_GPECON
                , NomeEmpresaAmbienteRelacionado = entity0Al5.NOME_EMPRESA
                , NomeAutenticacao = entity0Al8.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al8.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsIdentidadeExternaNoAssociations.
	    public IQueryable<TcsIdentidadeExterna> GetTcsIdentidadeExternaNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsIdentidadeExterna> result = 
	            (from entity0 in this.DbContext.TCS_IDENTIDADE_EXTERNA
                  let entity0Al1 = entity0.TCS_USUARIO_AUTENTICACAO
	            
	            	
	            select new TcsIdentidadeExterna()		
	            {
	            
                IdentidadeExterna = entity0.IDENTIDADE_EXTERNA
                , IdIdentidadeExterna = entity0.ID_IDENTIDADE_EXTERNA
                , IdUsuario = entity0Al1.ID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioGpeconNoAssociations.
	    public IQueryable<TcsUsuarioGpecon> GetTcsUsuarioGpeconNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioGpecon> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_AUTENTICACAO_GPECON
                  let entity0Al1 = entity0.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_USUARIO_AUTENTICACAO
	            
	            	
	            select new TcsUsuarioGpecon()		
	            {
	            
                IdLinx = entity0Al1.ID_LINX
                , IdTcsUsuarioAutGpecon = entity0.ID_TCS_USUARIO_AUT_GPECON
                , IdUsuario = entity0Al2.ID_USUARIO
                , NomeEmpresa = entity0Al1.NOME_EMPRESA
                , NomeAutenticacao = entity0Al2.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al2.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get RequisicaoAcesso.
	    public IEnumerable<RequisicaoAcesso> GetRequisicaoAcesso()
	    {




	
	        IEnumerable<RequisicaoAcesso> result = new List<RequisicaoAcesso>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get RequisicaoAcessoNoAssociations.
	    public IEnumerable<RequisicaoAcesso> GetRequisicaoAcessoNoAssociations()
	    {




	
	        IEnumerable<RequisicaoAcesso> result = new List<RequisicaoAcesso>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get UsuarioAcesso.
	    public IEnumerable<UsuarioAcesso> GetUsuarioAcesso()
	    {




	
	        IEnumerable<UsuarioAcesso> result = new List<UsuarioAcesso>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get UsuarioAcessoNoAssociations.
	    public IEnumerable<UsuarioAcesso> GetUsuarioAcessoNoAssociations()
	    {




	
	        IEnumerable<UsuarioAcesso> result = new List<UsuarioAcesso>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsSuporteAcessoLog.
	    public IQueryable<TcsSuporteAcessoLog> GetTcsSuporteAcessoLog()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsSuporteAcessoLog> result = 
	            (from entity0 in this.DbContext.TCS_SUPORTE_ACESSO_LOG
                  let entity0Al2 = entity0.USUARIO_ACESSO
                  let entity0Al3 = entity0.USUARIO_SUPORTE
                  let entity0Al1 = entity0.TCS_USUARIO_ACESSO
	            
	            	
	            select new TcsSuporteAcessoLog()		
	            {
	            
                AcessoExpirado = entity0.ACESSO_EXPIRADO
                , DataAcesso = entity0.DATA_ACESSO
                , DataCadastro = entity0.DATA_CADASTRO
                , IdTcsSuporteAcessoLog = entity0.ID_TCS_SUPORTE_ACESSO_LOG
                , IdTcsUsuarioAcesso = entity0Al1.ID_TCS_USUARIO_ACESSO
                , IdUsuarioAcesso = entity0Al2.ID_USUARIO
                , IdUsuarioSuporte = entity0Al3.ID_USUARIO
                , NomeAutenticacaoAcesso = entity0Al2.NOME_AUTENTICACAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsSuporteAcessoLogNoAssociations.
	    public IQueryable<TcsSuporteAcessoLog> GetTcsSuporteAcessoLogNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsSuporteAcessoLog> result = 
	            (from entity0 in this.DbContext.TCS_SUPORTE_ACESSO_LOG
                  let entity0Al2 = entity0.USUARIO_ACESSO
                  let entity0Al3 = entity0.USUARIO_SUPORTE
                  let entity0Al1 = entity0.TCS_USUARIO_ACESSO
	            
	            	
	            select new TcsSuporteAcessoLog()		
	            {
	            
                AcessoExpirado = entity0.ACESSO_EXPIRADO
                , DataAcesso = entity0.DATA_ACESSO
                , DataCadastro = entity0.DATA_CADASTRO
                , IdTcsSuporteAcessoLog = entity0.ID_TCS_SUPORTE_ACESSO_LOG
                , IdTcsUsuarioAcesso = entity0Al1.ID_TCS_USUARIO_ACESSO
                , IdUsuarioAcesso = entity0Al2.ID_USUARIO
                , IdUsuarioSuporte = entity0Al3.ID_USUARIO
                , NomeAutenticacaoAcesso = entity0Al2.NOME_AUTENTICACAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get RequisicaoSuporte.
	    public IEnumerable<RequisicaoSuporte> GetRequisicaoSuporte()
	    {




	
	        IEnumerable<RequisicaoSuporte> result = new List<RequisicaoSuporte>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get RequisicaoSuporteNoAssociations.
	    public IEnumerable<RequisicaoSuporte> GetRequisicaoSuporteNoAssociations()
	    {




	
	        IEnumerable<RequisicaoSuporte> result = new List<RequisicaoSuporte>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsUsuarioAcessoAmbiente.
	    public IQueryable<TcsUsuarioAcessoAmbiente> GetTcsUsuarioAcessoAmbiente()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioAcessoAmbiente> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_ACESSO
                  let entity0Al1 = entity0.TCS_AMBIENTE
                  let entity0Al5 = entity0.TCS_AMBIENTE1
                  let entity0Al6 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_AMBIENTE.TCS_APLICACAO
                  let entity0Al7 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al3 = entity0.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
                  let entity0Al4 = entity0.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsUsuarioAcessoAmbiente()		
	            {
	            
                DescricaoAmbiente = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al2.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0Al3.DESCRICAO_APLICATIVO
                , EmDesenvolvimento = entity0Al2.EM_DESENVOLVIMENTO
                , GrupoEconomico = entity0Al4.NOME_EMPRESA
                , IdLinxGpecon = entity0Al4.ID_LINX
                , IdTcsAmbiente = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsAmbienteRelacionado = entity0Al5.ID_TCS_AMBIENTE
                , IdTcsAplicativo = entity0Al3.ID_TCS_APLICATIVO
                , IdTcsUsuarioAcesso = entity0.ID_TCS_USUARIO_ACESSO
                , IdUsuario = entity0Al6.ID_USUARIO
                , IndicaAcessoPadrao = entity0.INDICA_ACESSO_PADRAO
                , IndicaAdministrador = entity0.INDICA_ADMINISTRADOR
                , NomeEmpresa = entity0Al7.NOME_EMPRESA
                , UidAplicacao = entity0Al2.UID_APLICACAO
                , UidEmpresa = entity0Al7.UID_EMPRESA
                , UidGrupoEconomico = entity0Al4.UID_EMPRESA
                , UidUsuario = entity0Al6.UID_USUARIO
                , Url = entity0Al2.URL
                , UrlWorkArea = entity0Al2.URL_WORK_AREA
                , NomeAutenticacao = entity0Al6.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al6.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioAcessoAmbienteNoAssociations.
	    public IQueryable<TcsUsuarioAcessoAmbiente> GetTcsUsuarioAcessoAmbienteNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioAcessoAmbiente> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_ACESSO
                  let entity0Al1 = entity0.TCS_AMBIENTE
                  let entity0Al5 = entity0.TCS_AMBIENTE1
                  let entity0Al6 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_AMBIENTE.TCS_APLICACAO
                  let entity0Al7 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al3 = entity0.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
                  let entity0Al4 = entity0.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsUsuarioAcessoAmbiente()		
	            {
	            
                DescricaoAmbiente = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al2.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0Al3.DESCRICAO_APLICATIVO
                , EmDesenvolvimento = entity0Al2.EM_DESENVOLVIMENTO
                , GrupoEconomico = entity0Al4.NOME_EMPRESA
                , IdLinxGpecon = entity0Al4.ID_LINX
                , IdTcsAmbiente = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsAmbienteRelacionado = entity0Al5.ID_TCS_AMBIENTE
                , IdTcsAplicativo = entity0Al3.ID_TCS_APLICATIVO
                , IdTcsUsuarioAcesso = entity0.ID_TCS_USUARIO_ACESSO
                , IdUsuario = entity0Al6.ID_USUARIO
                , IndicaAcessoPadrao = entity0.INDICA_ACESSO_PADRAO
                , IndicaAdministrador = entity0.INDICA_ADMINISTRADOR
                , NomeEmpresa = entity0Al7.NOME_EMPRESA
                , UidAplicacao = entity0Al2.UID_APLICACAO
                , UidEmpresa = entity0Al7.UID_EMPRESA
                , UidGrupoEconomico = entity0Al4.UID_EMPRESA
                , UidUsuario = entity0Al6.UID_USUARIO
                , Url = entity0Al2.URL
                , UrlWorkArea = entity0Al2.URL_WORK_AREA
                , NomeAutenticacao = entity0Al6.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al6.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsUsuarioAutenticacaoAcessoP.
	    public IQueryable<TcsUsuarioAutenticacaoAcessoP> GetTcsUsuarioAutenticacaoAcessoP()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioAutenticacaoAcessoP> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_ACESSO
                  let entity0Al1 = entity0.TCS_AMBIENTE
                  let entity0Al2 = entity0.TCS_AMBIENTE1
                  let entity0Al6 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al3 = entity0.TCS_AMBIENTE.TCS_APLICACAO
                  let entity0Al5 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al4 = entity0.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
	            
	            	
	            select new TcsUsuarioAutenticacaoAcessoP()		
	            {
	            
                DescricaoAmbiente = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAmbienteRelacionado = entity0Al2.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al3.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0Al4.DESCRICAO_APLICATIVO
                , IdAplicacao = entity0Al3.ID_APLICACAO
                , IdLinx = entity0Al5.ID_LINX
                , IdTcsAmbiente = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsAmbienteRelacionado = entity0Al2.ID_TCS_AMBIENTE
                , IdTcsAplicativo = entity0Al4.ID_TCS_APLICATIVO
                , IdTcsUsuarioAcesso = entity0.ID_TCS_USUARIO_ACESSO
                , IdUsuario = entity0Al6.ID_USUARIO
                , IndicaAcessoPadrao = entity0.INDICA_ACESSO_PADRAO
                , NomeEmpresa = entity0Al5.NOME_EMPRESA
                , Perfil = ""
                , NomeAutenticacao = entity0Al6.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al6.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioAutenticacaoAcessoPNoAssociations.
	    public IQueryable<TcsUsuarioAutenticacaoAcessoP> GetTcsUsuarioAutenticacaoAcessoPNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioAutenticacaoAcessoP> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_ACESSO
                  let entity0Al1 = entity0.TCS_AMBIENTE
                  let entity0Al2 = entity0.TCS_AMBIENTE1
                  let entity0Al6 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al3 = entity0.TCS_AMBIENTE.TCS_APLICACAO
                  let entity0Al5 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al4 = entity0.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
	            
	            	
	            select new TcsUsuarioAutenticacaoAcessoP()		
	            {
	            
                DescricaoAmbiente = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAmbienteRelacionado = entity0Al2.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al3.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0Al4.DESCRICAO_APLICATIVO
                , IdAplicacao = entity0Al3.ID_APLICACAO
                , IdLinx = entity0Al5.ID_LINX
                , IdTcsAmbiente = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsAmbienteRelacionado = entity0Al2.ID_TCS_AMBIENTE
                , IdTcsAplicativo = entity0Al4.ID_TCS_APLICATIVO
                , IdTcsUsuarioAcesso = entity0.ID_TCS_USUARIO_ACESSO
                , IdUsuario = entity0Al6.ID_USUARIO
                , IndicaAcessoPadrao = entity0.INDICA_ACESSO_PADRAO
                , NomeEmpresa = entity0Al5.NOME_EMPRESA
                , Perfil = ""
                , NomeAutenticacao = entity0Al6.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al6.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	result.Add("TcsUsuarioAutenticacao|ConfirmacaoUsuario");
	    	result.Add("TcsUsuarioAutenticacao|''");
	    	result.Add("TcsUsuarioAutenticacao|ConfirmacaoUsuario1");
	    	result.Add("TcsUsuarioAutenticacao|''");
	    	result.Add("TcsUsuarioAutenticacao|CriaUsuario");
	    	result.Add("TcsUsuarioAutenticacao|false");
	    	result.Add("TcsUsuarioAutenticacao|GeraSenhaUsuario");
	    	result.Add("TcsUsuarioAutenticacao|false");
	    	//Add filtering disabled property for TCS_USUARIO_AUTENTICACAO
	    	string[] bmDisabledTcsUsuarioAutenticacaoList = this.GetEDM().GetFilteringDisabledList("TCS_USUARIO_AUTENTICACAO");
	    	if (bmDisabledTcsUsuarioAutenticacaoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsUsuarioAutenticacaoList.Contains("TCS_USUARIO_AUTENTICACAO.AUTENTICACAO_WINDOWS"))
	    		{
	    			result.Add("TcsUsuarioAutenticacao|AutenticacaoWindows");
	    			result.Add("TcsUsuarioAutenticacao|TCS_USUARIO_AUTENTICACAO.AUTENTICACAO_WINDOWS");
	    		}
	
	    		if (bmDisabledTcsUsuarioAutenticacaoList.Contains("TCS_USUARIO_AUTENTICACAO.BAIRRO"))
	    		{
	    			result.Add("TcsUsuarioAutenticacao|Bairro");
	    			result.Add("TcsUsuarioAutenticacao|TCS_USUARIO_AUTENTICACAO.BAIRRO");
	    		}
	
	    		if (bmDisabledTcsUsuarioAutenticacaoList.Contains("TCS_USUARIO_AUTENTICACAO.CEP"))
	    		{
	    			result.Add("TcsUsuarioAutenticacao|Cep");
	    			result.Add("TcsUsuarioAutenticacao|TCS_USUARIO_AUTENTICACAO.CEP");
	    		}
	
	    		if (bmDisabledTcsUsuarioAutenticacaoList.Contains("TCS_USUARIO_AUTENTICACAO.CNPJ_CPF"))
	    		{
	    			result.Add("TcsUsuarioAutenticacao|CnpjCpf");
	    			result.Add("TcsUsuarioAutenticacao|TCS_USUARIO_AUTENTICACAO.CNPJ_CPF");
	    		}
	
	    		if (bmDisabledTcsUsuarioAutenticacaoList.Contains("TCS_USUARIO_AUTENTICACAO.COMPLEMENTO"))
	    		{
	    			result.Add("TcsUsuarioAutenticacao|Complemento");
	    			result.Add("TcsUsuarioAutenticacao|TCS_USUARIO_AUTENTICACAO.COMPLEMENTO");
	    		}
	
	    		if (bmDisabledTcsUsuarioAutenticacaoList.Contains("TCS_USUARIO_AUTENTICACAO.DATA_ALTERACAO"))
	    		{
	    			result.Add("TcsUsuarioAutenticacao|DataAlteracao");
	    			result.Add("TcsUsuarioAutenticacao|TCS_USUARIO_AUTENTICACAO.DATA_ALTERACAO");
	    		}
	
	    		if (bmDisabledTcsUsuarioAutenticacaoList.Contains("TCS_USUARIO_AUTENTICACAO.DATA_CADASTRO"))
	    		{
	    			result.Add("TcsUsuarioAutenticacao|DataCadastro");
	    			result.Add("TcsUsuarioAutenticacao|TCS_USUARIO_AUTENTICACAO.DATA_CADASTRO");
	    		}
	
	    		if (bmDisabledTcsUsuarioAutenticacaoList.Contains("TCS_USUARIO_AUTENTICACAO.DATA_EXPIRACAO_SENHA"))
	    		{
	    			result.Add("TcsUsuarioAutenticacao|DataExpiracaoSenha");
	    			result.Add("TcsUsuarioAutenticacao|TCS_USUARIO_AUTENTICACAO.DATA_EXPIRACAO_SENHA");
	    		}
	
	    		if (bmDisabledTcsUsuarioAutenticacaoList.Contains("TCS_USUARIO_AUTENTICACAO.EMAIL"))
	    		{
	    			result.Add("TcsUsuarioAutenticacao|Email");
	    			result.Add("TcsUsuarioAutenticacao|TCS_USUARIO_AUTENTICACAO.EMAIL");
	    		}
	
	    		if (bmDisabledTcsUsuarioAutenticacaoList.Contains("TCS_USUARIO_AUTENTICACAO.FONE_CELULAR"))
	    		{
	    			result.Add("TcsUsuarioAutenticacao|FoneCelular");
	    			result.Add("TcsUsuarioAutenticacao|TCS_USUARIO_AUTENTICACAO.FONE_CELULAR");
	    		}
	
	    		if (bmDisabledTcsUsuarioAutenticacaoList.Contains("TCS_USUARIO_AUTENTICACAO.FONE_FIXO"))
	    		{
	    			result.Add("TcsUsuarioAutenticacao|FoneFixo");
	    			result.Add("TcsUsuarioAutenticacao|TCS_USUARIO_AUTENTICACAO.FONE_FIXO");
	    		}
	
	    		if (bmDisabledTcsUsuarioAutenticacaoList.Contains("TCS_USUARIO_AUTENTICACAO.ID_USUARIO"))
	    		{
	    			result.Add("TcsUsuarioAutenticacao|IdUsuario");
	    			result.Add("TcsUsuarioAutenticacao|TCS_USUARIO_AUTENTICACAO.ID_USUARIO");
	    		}
	
	    		if (bmDisabledTcsUsuarioAutenticacaoList.Contains("TCS_USUARIO_AUTENTICACAO.INATIVO"))
	    		{
	    			result.Add("TcsUsuarioAutenticacao|Inativo");
	    			result.Add("TcsUsuarioAutenticacao|TCS_USUARIO_AUTENTICACAO.INATIVO");
	    		}
	
	    		if (bmDisabledTcsUsuarioAutenticacaoList.Contains("TCS_USUARIO_AUTENTICACAO.INDICA_ACESSO_SUPORTE"))
	    		{
	    			result.Add("TcsUsuarioAutenticacao|IndicaAcessoSuporte");
	    			result.Add("TcsUsuarioAutenticacao|TCS_USUARIO_AUTENTICACAO.INDICA_ACESSO_SUPORTE");
	    		}
	
	    		if (bmDisabledTcsUsuarioAutenticacaoList.Contains("TCS_USUARIO_AUTENTICACAO.INSCR_ESTADUAL_RG"))
	    		{
	    			result.Add("TcsUsuarioAutenticacao|InscrEstadualRg");
	    			result.Add("TcsUsuarioAutenticacao|TCS_USUARIO_AUTENTICACAO.INSCR_ESTADUAL_RG");
	    		}
	
	    		if (bmDisabledTcsUsuarioAutenticacaoList.Contains("TCS_USUARIO_AUTENTICACAO.LOGRADOURO"))
	    		{
	    			result.Add("TcsUsuarioAutenticacao|Logradouro");
	    			result.Add("TcsUsuarioAutenticacao|TCS_USUARIO_AUTENTICACAO.LOGRADOURO");
	    		}
	
	    		if (bmDisabledTcsUsuarioAutenticacaoList.Contains("TCS_USUARIO_AUTENTICACAO.LX_PFJ_FISICA_JURIDICA"))
	    		{
	    			result.Add("TcsUsuarioAutenticacao|LxPfjFisicaJuridica");
	    			result.Add("TcsUsuarioAutenticacao|TCS_USUARIO_AUTENTICACAO.LX_PFJ_FISICA_JURIDICA");
	    		}
	
	    		if (bmDisabledTcsUsuarioAutenticacaoList.Contains("TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO"))
	    		{
	    			result.Add("TcsUsuarioAutenticacao|LxTipoLogradouro");
	    			result.Add("TcsUsuarioAutenticacao|TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO");
	    		}
	
	    		if (bmDisabledTcsUsuarioAutenticacaoList.Contains("TCS_USUARIO_AUTENTICACAO.MUNICIPIO"))
	    		{
	    			result.Add("TcsUsuarioAutenticacao|Municipio");
	    			result.Add("TcsUsuarioAutenticacao|TCS_USUARIO_AUTENTICACAO.MUNICIPIO");
	    		}
	
	    		if (bmDisabledTcsUsuarioAutenticacaoList.Contains("TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO"))
	    		{
	    			result.Add("TcsUsuarioAutenticacao|NomeAutenticacao");
	    			result.Add("TcsUsuarioAutenticacao|TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO");
	    		}
	
	    		if (bmDisabledTcsUsuarioAutenticacaoList.Contains("TCS_USUARIO_AUTENTICACAO.NOME_CURTO_USUARIO"))
	    		{
	    			result.Add("TcsUsuarioAutenticacao|NomeCurtoUsuario");
	    			result.Add("TcsUsuarioAutenticacao|TCS_USUARIO_AUTENTICACAO.NOME_CURTO_USUARIO");
	    		}
	
	    		if (bmDisabledTcsUsuarioAutenticacaoList.Contains("TCS_USUARIO_AUTENTICACAO.NOME_USUARIO"))
	    		{
	    			result.Add("TcsUsuarioAutenticacao|NomeUsuario");
	    			result.Add("TcsUsuarioAutenticacao|TCS_USUARIO_AUTENTICACAO.NOME_USUARIO");
	    		}
	
	    		if (bmDisabledTcsUsuarioAutenticacaoList.Contains("TCS_USUARIO_AUTENTICACAO.NUMERO"))
	    		{
	    			result.Add("TcsUsuarioAutenticacao|Numero");
	    			result.Add("TcsUsuarioAutenticacao|TCS_USUARIO_AUTENTICACAO.NUMERO");
	    		}
	
	    		if (bmDisabledTcsUsuarioAutenticacaoList.Contains("TCS_USUARIO_AUTENTICACAO.OBS_ENDERECO"))
	    		{
	    			result.Add("TcsUsuarioAutenticacao|ObsEndereco");
	    			result.Add("TcsUsuarioAutenticacao|TCS_USUARIO_AUTENTICACAO.OBS_ENDERECO");
	    		}
	
	    		if (bmDisabledTcsUsuarioAutenticacaoList.Contains("TCS_USUARIO_AUTENTICACAO.RAMAL"))
	    		{
	    			result.Add("TcsUsuarioAutenticacao|Ramal");
	    			result.Add("TcsUsuarioAutenticacao|TCS_USUARIO_AUTENTICACAO.RAMAL");
	    		}
	
	    		if (bmDisabledTcsUsuarioAutenticacaoList.Contains("TCS_USUARIO_AUTENTICACAO.UF"))
	    		{
	    			result.Add("TcsUsuarioAutenticacao|Uf");
	    			result.Add("TcsUsuarioAutenticacao|TCS_USUARIO_AUTENTICACAO.UF");
	    		}
	
	    		if (bmDisabledTcsUsuarioAutenticacaoList.Contains("TCS_USUARIO_AUTENTICACAO.UID_USUARIO"))
	    		{
	    			result.Add("TcsUsuarioAutenticacao|UidUsuario");
	    			result.Add("TcsUsuarioAutenticacao|TCS_USUARIO_AUTENTICACAO.UID_USUARIO");
	    		}
	
	    		if (bmDisabledTcsUsuarioAutenticacaoList.Contains("TCS_USUARIO_AUTENTICACAO.VIGENCIA_FINAL"))
	    		{
	    			result.Add("TcsUsuarioAutenticacao|VigenciaFinal");
	    			result.Add("TcsUsuarioAutenticacao|TCS_USUARIO_AUTENTICACAO.VIGENCIA_FINAL");
	    		}
	
	    		if (bmDisabledTcsUsuarioAutenticacaoList.Contains("TCS_USUARIO_AUTENTICACAO.VIGENCIA_INICIAL"))
	    		{
	    			result.Add("TcsUsuarioAutenticacao|VigenciaInicial");
	    			result.Add("TcsUsuarioAutenticacao|TCS_USUARIO_AUTENTICACAO.VIGENCIA_INICIAL");
	    		}
	    	}
	    	result.Add("TcsUsuarioAcesso|NomeAutenticacao");
	    	result.Add("TcsUsuarioAcesso|TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO");
	    	result.Add("TcsUsuarioAcesso|NomeUsuario");
	    	result.Add("TcsUsuarioAcesso|TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO");
	    	//Add filtering disabled property for TCS_USUARIO_ACESSO
	    	string[] bmDisabledTcsUsuarioAcessoList = this.GetEDM().GetFilteringDisabledList("TCS_USUARIO_ACESSO");
	    	if (bmDisabledTcsUsuarioAcessoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsUsuarioAcessoList.Contains("TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO"))
	    		{
	    			result.Add("TcsUsuarioAcesso|IdTcsUsuarioAcesso");
	    			result.Add("TcsUsuarioAcesso|TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO");
	    		}
	
	    		if (bmDisabledTcsUsuarioAcessoList.Contains("TCS_USUARIO_ACESSO.INDICA_ACESSO_PADRAO"))
	    		{
	    			result.Add("TcsUsuarioAcesso|IndicaAcessoPadrao");
	    			result.Add("TcsUsuarioAcesso|TCS_USUARIO_ACESSO.INDICA_ACESSO_PADRAO");
	    		}
	
	    		if (bmDisabledTcsUsuarioAcessoList.Contains("TCS_USUARIO_ACESSO.INDICA_ADMINISTRADOR"))
	    		{
	    			result.Add("TcsUsuarioAcesso|IndicaAdministrador");
	    			result.Add("TcsUsuarioAcesso|TCS_USUARIO_ACESSO.INDICA_ADMINISTRADOR");
	    		}
	
	    		if (bmDisabledTcsUsuarioAcessoList.Contains("TCS_USUARIO_ACESSO.INDICA_MULTI_GPECON"))
	    		{
	    			result.Add("TcsUsuarioAcesso|IndicaMultiGpecon");
	    			result.Add("TcsUsuarioAcesso|TCS_USUARIO_ACESSO.INDICA_MULTI_GPECON");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_IDENTIDADE_EXTERNA
	    	string[] bmDisabledTcsIdentidadeExternaList = this.GetEDM().GetFilteringDisabledList("TCS_IDENTIDADE_EXTERNA");
	    	if (bmDisabledTcsIdentidadeExternaList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsIdentidadeExternaList.Contains("TCS_IDENTIDADE_EXTERNA.IDENTIDADE_EXTERNA"))
	    		{
	    			result.Add("TcsIdentidadeExterna|IdentidadeExterna");
	    			result.Add("TcsIdentidadeExterna|TCS_IDENTIDADE_EXTERNA.IDENTIDADE_EXTERNA");
	    		}
	
	    		if (bmDisabledTcsIdentidadeExternaList.Contains("TCS_IDENTIDADE_EXTERNA.ID_IDENTIDADE_EXTERNA"))
	    		{
	    			result.Add("TcsIdentidadeExterna|IdIdentidadeExterna");
	    			result.Add("TcsIdentidadeExterna|TCS_IDENTIDADE_EXTERNA.ID_IDENTIDADE_EXTERNA");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_SUPORTE_ACESSO_LOG
	    	string[] bmDisabledTcsSuporteAcessoLogList = this.GetEDM().GetFilteringDisabledList("TCS_SUPORTE_ACESSO_LOG");
	    	if (bmDisabledTcsSuporteAcessoLogList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsSuporteAcessoLogList.Contains("TCS_SUPORTE_ACESSO_LOG.ACESSO_EXPIRADO"))
	    		{
	    			result.Add("TcsSuporteAcessoLog|AcessoExpirado");
	    			result.Add("TcsSuporteAcessoLog|TCS_SUPORTE_ACESSO_LOG.ACESSO_EXPIRADO");
	    		}
	
	    		if (bmDisabledTcsSuporteAcessoLogList.Contains("TCS_SUPORTE_ACESSO_LOG.DATA_ACESSO"))
	    		{
	    			result.Add("TcsSuporteAcessoLog|DataAcesso");
	    			result.Add("TcsSuporteAcessoLog|TCS_SUPORTE_ACESSO_LOG.DATA_ACESSO");
	    		}
	
	    		if (bmDisabledTcsSuporteAcessoLogList.Contains("TCS_SUPORTE_ACESSO_LOG.DATA_CADASTRO"))
	    		{
	    			result.Add("TcsSuporteAcessoLog|DataCadastro");
	    			result.Add("TcsSuporteAcessoLog|TCS_SUPORTE_ACESSO_LOG.DATA_CADASTRO");
	    		}
	
	    		if (bmDisabledTcsSuporteAcessoLogList.Contains("TCS_SUPORTE_ACESSO_LOG.ID_TCS_SUPORTE_ACESSO_LOG"))
	    		{
	    			result.Add("TcsSuporteAcessoLog|IdTcsSuporteAcessoLog");
	    			result.Add("TcsSuporteAcessoLog|TCS_SUPORTE_ACESSO_LOG.ID_TCS_SUPORTE_ACESSO_LOG");
	    		}
	    	}
	    	//Add filtering disabled property for TCS_USUARIO_ACESSO
	    	string[] bmDisabledTcsUsuarioAcessoAmbienteList = this.GetEDM().GetFilteringDisabledList("TCS_USUARIO_ACESSO");
	    	if (bmDisabledTcsUsuarioAcessoAmbienteList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsUsuarioAcessoAmbienteList.Contains("TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO"))
	    		{
	    			result.Add("TcsUsuarioAcessoAmbiente|IdTcsUsuarioAcesso");
	    			result.Add("TcsUsuarioAcessoAmbiente|TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO");
	    		}
	
	    		if (bmDisabledTcsUsuarioAcessoAmbienteList.Contains("TCS_USUARIO_ACESSO.INDICA_ACESSO_PADRAO"))
	    		{
	    			result.Add("TcsUsuarioAcessoAmbiente|IndicaAcessoPadrao");
	    			result.Add("TcsUsuarioAcessoAmbiente|TCS_USUARIO_ACESSO.INDICA_ACESSO_PADRAO");
	    		}
	
	    		if (bmDisabledTcsUsuarioAcessoAmbienteList.Contains("TCS_USUARIO_ACESSO.INDICA_ADMINISTRADOR"))
	    		{
	    			result.Add("TcsUsuarioAcessoAmbiente|IndicaAdministrador");
	    			result.Add("TcsUsuarioAcessoAmbiente|TCS_USUARIO_ACESSO.INDICA_ADMINISTRADOR");
	    		}
	    	}
	    	result.Add("TcsUsuarioAutenticacaoAcessoP|Perfil");
	    	result.Add("TcsUsuarioAutenticacaoAcessoP|''");
	    	//Add filtering disabled property for TCS_USUARIO_ACESSO
	    	string[] bmDisabledTcsUsuarioAutenticacaoAcessoPList = this.GetEDM().GetFilteringDisabledList("TCS_USUARIO_ACESSO");
	    	if (bmDisabledTcsUsuarioAutenticacaoAcessoPList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsUsuarioAutenticacaoAcessoPList.Contains("TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO"))
	    		{
	    			result.Add("TcsUsuarioAutenticacaoAcessoP|IdTcsUsuarioAcesso");
	    			result.Add("TcsUsuarioAutenticacaoAcessoP|TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO");
	    		}
	
	    		if (bmDisabledTcsUsuarioAutenticacaoAcessoPList.Contains("TCS_USUARIO_ACESSO.INDICA_ACESSO_PADRAO"))
	    		{
	    			result.Add("TcsUsuarioAutenticacaoAcessoP|IndicaAcessoPadrao");
	    			result.Add("TcsUsuarioAutenticacaoAcessoP|TCS_USUARIO_ACESSO.INDICA_ACESSO_PADRAO");
	    		}
	    	}
	    	result.Add("TcsUsuarioGpecon|NomeAutenticacao");
	    	result.Add("TcsUsuarioGpecon|TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO");
	    	//Add filtering disabled property for TCS_USUARIO_AUTENTICACAO_GPECON
	    	string[] bmDisabledTcsUsuarioGpeconList = this.GetEDM().GetFilteringDisabledList("TCS_USUARIO_AUTENTICACAO_GPECON");
	    	if (bmDisabledTcsUsuarioGpeconList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsUsuarioGpeconList.Contains("TCS_USUARIO_AUTENTICACAO_GPECON.ID_TCS_USUARIO_AUT_GPECON"))
	    		{
	    			result.Add("TcsUsuarioGpecon|IdTcsUsuarioAutGpecon");
	    			result.Add("TcsUsuarioGpecon|TCS_USUARIO_AUTENTICACAO_GPECON.ID_TCS_USUARIO_AUT_GPECON");
	    		}
	    	}
	    	return result;
	    }

	    #endregion Excluded Filters

	    #region Get By EntitySearchId Methods Definitions.
	
				
	    [Ignore]
	    //Get TcsUsuarioAutenticacao By EntitySearchId.
	    public IQueryable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioAutenticacaoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioAcesso By EntitySearchId.
	    public IQueryable<TcsUsuarioAcesso> GetTcsUsuarioAcessoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioAcessoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsIdentidadeExterna By EntitySearchId.
	    public IQueryable<TcsIdentidadeExterna> GetTcsIdentidadeExternaByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsIdentidadeExternaByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioGpecon By EntitySearchId.
	    public IQueryable<TcsUsuarioGpecon> GetTcsUsuarioGpeconByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioGpeconByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioAutenticacao By EntitySearchId.
	    public IQueryable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioAcesso By EntitySearchId.
	    public IQueryable<TcsUsuarioAcesso> GetTcsUsuarioAcessoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioAcessoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsIdentidadeExterna By EntitySearchId.
	    public IQueryable<TcsIdentidadeExterna> GetTcsIdentidadeExternaByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsIdentidadeExternaByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioGpecon By EntitySearchId.
	    public IQueryable<TcsUsuarioGpecon> GetTcsUsuarioGpeconByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioGpeconByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get RequisicaoAcesso By EntitySearchId.
	    public IEnumerable<RequisicaoAcesso> GetRequisicaoAcessoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetRequisicaoAcessoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get RequisicaoAcesso By EntitySearchId.
	    public IEnumerable<RequisicaoAcesso> GetRequisicaoAcessoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetRequisicaoAcessoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get UsuarioAcesso By EntitySearchId.
	    public IEnumerable<UsuarioAcesso> GetUsuarioAcessoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetUsuarioAcessoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get UsuarioAcesso By EntitySearchId.
	    public IEnumerable<UsuarioAcesso> GetUsuarioAcessoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetUsuarioAcessoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsSuporteAcessoLog By EntitySearchId.
	    public IQueryable<TcsSuporteAcessoLog> GetTcsSuporteAcessoLogByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsSuporteAcessoLogByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsSuporteAcessoLog By EntitySearchId.
	    public IQueryable<TcsSuporteAcessoLog> GetTcsSuporteAcessoLogByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsSuporteAcessoLogByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get RequisicaoSuporte By EntitySearchId.
	    public IEnumerable<RequisicaoSuporte> GetRequisicaoSuporteByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetRequisicaoSuporteByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get RequisicaoSuporte By EntitySearchId.
	    public IEnumerable<RequisicaoSuporte> GetRequisicaoSuporteByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetRequisicaoSuporteByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioAcessoAmbiente By EntitySearchId.
	    public IQueryable<TcsUsuarioAcessoAmbiente> GetTcsUsuarioAcessoAmbienteByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioAcessoAmbienteByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioAcessoAmbiente By EntitySearchId.
	    public IQueryable<TcsUsuarioAcessoAmbiente> GetTcsUsuarioAcessoAmbienteByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioAcessoAmbienteByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioAutenticacaoAcessoP By EntitySearchId.
	    public IQueryable<TcsUsuarioAutenticacaoAcessoP> GetTcsUsuarioAutenticacaoAcessoPByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioAutenticacaoAcessoPByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioAutenticacaoAcessoP By EntitySearchId.
	    public IQueryable<TcsUsuarioAutenticacaoAcessoP> GetTcsUsuarioAutenticacaoAcessoPByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioAutenticacaoAcessoPByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get TcsUsuarioAutenticacao By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoByExample(TcsUsuarioAutenticacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioAutenticacaoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsUsuarioAcesso By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioAcesso> GetTcsUsuarioAcessoByExample(TcsUsuarioAcesso entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioAcessoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsIdentidadeExterna By Example.
	    [Ignore]
	    public IQueryable<TcsIdentidadeExterna> GetTcsIdentidadeExternaByExample(TcsIdentidadeExterna entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsIdentidadeExternaByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsUsuarioGpecon By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioGpecon> GetTcsUsuarioGpeconByExample(TcsUsuarioGpecon entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioGpeconByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsUsuarioAutenticacao By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoByExampleNoAssociations(TcsUsuarioAutenticacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsUsuarioAcesso By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioAcesso> GetTcsUsuarioAcessoByExampleNoAssociations(TcsUsuarioAcesso entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioAcessoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsIdentidadeExterna By Example.
	    [Ignore]
	    public IQueryable<TcsIdentidadeExterna> GetTcsIdentidadeExternaByExampleNoAssociations(TcsIdentidadeExterna entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsIdentidadeExternaByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsUsuarioGpecon By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioGpecon> GetTcsUsuarioGpeconByExampleNoAssociations(TcsUsuarioGpecon entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioGpeconByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get RequisicaoAcesso By Example.
	    [Ignore]
	    public IEnumerable<RequisicaoAcesso> GetRequisicaoAcessoByExample(RequisicaoAcesso entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetRequisicaoAcessoByEntitySearch(queryAnalysis);
	    }
			
	    //Get RequisicaoAcesso By Example.
	    [Ignore]
	    public IEnumerable<RequisicaoAcesso> GetRequisicaoAcessoByExampleNoAssociations(RequisicaoAcesso entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetRequisicaoAcessoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get UsuarioAcesso By Example.
	    [Ignore]
	    public IEnumerable<UsuarioAcesso> GetUsuarioAcessoByExample(UsuarioAcesso entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetUsuarioAcessoByEntitySearch(queryAnalysis);
	    }
			
	    //Get UsuarioAcesso By Example.
	    [Ignore]
	    public IEnumerable<UsuarioAcesso> GetUsuarioAcessoByExampleNoAssociations(UsuarioAcesso entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetUsuarioAcessoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsSuporteAcessoLog By Example.
	    [Ignore]
	    public IQueryable<TcsSuporteAcessoLog> GetTcsSuporteAcessoLogByExample(TcsSuporteAcessoLog entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsSuporteAcessoLogByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsSuporteAcessoLog By Example.
	    [Ignore]
	    public IQueryable<TcsSuporteAcessoLog> GetTcsSuporteAcessoLogByExampleNoAssociations(TcsSuporteAcessoLog entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsSuporteAcessoLogByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get RequisicaoSuporte By Example.
	    [Ignore]
	    public IEnumerable<RequisicaoSuporte> GetRequisicaoSuporteByExample(RequisicaoSuporte entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetRequisicaoSuporteByEntitySearch(queryAnalysis);
	    }
			
	    //Get RequisicaoSuporte By Example.
	    [Ignore]
	    public IEnumerable<RequisicaoSuporte> GetRequisicaoSuporteByExampleNoAssociations(RequisicaoSuporte entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetRequisicaoSuporteByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsUsuarioAcessoAmbiente By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioAcessoAmbiente> GetTcsUsuarioAcessoAmbienteByExample(TcsUsuarioAcessoAmbiente entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioAcessoAmbienteByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsUsuarioAcessoAmbiente By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioAcessoAmbiente> GetTcsUsuarioAcessoAmbienteByExampleNoAssociations(TcsUsuarioAcessoAmbiente entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioAcessoAmbienteByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsUsuarioAutenticacaoAcessoP By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioAutenticacaoAcessoP> GetTcsUsuarioAutenticacaoAcessoPByExample(TcsUsuarioAutenticacaoAcessoP entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioAutenticacaoAcessoPByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsUsuarioAutenticacaoAcessoP By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioAutenticacaoAcessoP> GetTcsUsuarioAutenticacaoAcessoPByExampleNoAssociations(TcsUsuarioAutenticacaoAcessoP entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioAutenticacaoAcessoPByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public TcsUsuarioAutenticacao GetTcsUsuarioAutenticacaoByKey(Int64 idUsuario)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsUsuarioAutenticacao");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuario"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idUsuario));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsUsuarioAcesso GetTcsUsuarioAcessoByKey(Int32 idTcsUsuarioAcesso)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsUsuarioAcesso");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsUsuarioAcesso"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsUsuarioAcesso));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsUsuarioAcessoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsIdentidadeExterna GetTcsIdentidadeExternaByKey(Int64 idIdentidadeExterna)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsIdentidadeExterna");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdIdentidadeExterna"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idIdentidadeExterna));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsIdentidadeExternaByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public RequisicaoAcesso GetRequisicaoAcessoByKey(string nomeAutenticacao)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("RequisicaoAcesso");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "NomeAutenticacao"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, nomeAutenticacao));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetRequisicaoAcessoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public UsuarioAcesso GetUsuarioAcessoByKey(int idTcsAmbiente)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("UsuarioAcesso");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsAmbiente"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsAmbiente));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetUsuarioAcessoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsSuporteAcessoLog GetTcsSuporteAcessoLogByKey(Int32 idTcsSuporteAcessoLog)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsSuporteAcessoLog");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsSuporteAcessoLog"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsSuporteAcessoLog));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsSuporteAcessoLogByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public RequisicaoSuporte GetRequisicaoSuporteByKey(string urlPortal)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("RequisicaoSuporte");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "UrlPortal"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, urlPortal));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetRequisicaoSuporteByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsUsuarioAcessoAmbiente GetTcsUsuarioAcessoAmbienteByKey(Int32 idTcsUsuarioAcesso)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsUsuarioAcessoAmbiente");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsUsuarioAcesso"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsUsuarioAcesso));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsUsuarioAcessoAmbienteByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsUsuarioAutenticacaoAcessoP GetTcsUsuarioAutenticacaoAcessoPByKey(Int32 idTcsUsuarioAcesso)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsUsuarioAutenticacaoAcessoP");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsUsuarioAcesso"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsUsuarioAcesso));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsUsuarioAutenticacaoAcessoPByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsUsuarioGpecon GetTcsUsuarioGpeconByKey(int idTcsUsuarioAutGpecon)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsUsuarioGpecon");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsUsuarioAutGpecon"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsUsuarioAutGpecon));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsUsuarioGpeconByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioAutenticacaoByEntitySearch.
	    public IQueryable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioAutenticacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioAutenticacao> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_AUTENTICACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsUsuarioAutenticacao()		
	            {
	            
                AutenticacaoWindows = entity0.AUTENTICACAO_WINDOWS
                , Bairro = entity0.BAIRRO
                , Cep = entity0.CEP
                , CnpjCpf = entity0.CNPJ_CPF
                , Complemento = entity0.COMPLEMENTO
                , ConfirmacaoUsuario = ""
                , ConfirmacaoUsuario1 = ""
                , CriaUsuario = false
                , DataAlteracao = entity0.DATA_ALTERACAO
                , DataCadastro = entity0.DATA_CADASTRO
                , DataExpiracaoSenha = entity0.DATA_EXPIRACAO_SENHA
                , Email = entity0.EMAIL
                , FoneCelular = entity0.FONE_CELULAR
                , FoneFixo = entity0.FONE_FIXO
                , GeraSenhaUsuario = false
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
                , NomeEmpresa = entity0Al1.NOME_EMPRESA
                , NomeUsuario = entity0.NOME_USUARIO
                , Numero = entity0.NUMERO
                , ObsEndereco = entity0.OBS_ENDERECO
                , Ramal = entity0.RAMAL
                , Uf = entity0.UF
                , UidEmpresa = entity0Al1.UID_EMPRESA
                , UidUsuario = entity0.UID_USUARIO
                , VigenciaFinal = entity0.VIGENCIA_FINAL
                , VigenciaInicial = entity0.VIGENCIA_INICIAL
			
                ,TcsUsuarioAcessoList = 
	                        (from entity1 in entity0.TCS_USUARIO_ACESSO_LISTA
                                  let entity1Al1 = entity1.TCS_AMBIENTE
                                  let entity1Al2 = entity1.TCS_AMBIENTE1
                                  let entity1Al8 = entity1.TCS_USUARIO_AUTENTICACAO
                                  let entity1Al3 = entity1.TCS_AMBIENTE.TCS_APLICACAO
                                  let entity1Al4 = entity1.TCS_AMBIENTE1.TCS_APLICACAO
                                  let entity1Al6 = entity1.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                                  let entity1Al5 = entity1.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO
                                  let entity1Al7 = entity1.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
	                        
	                        	
	                        select new TcsUsuarioAcesso()
	                        {
	                        
                                DescricaoAmbiente = entity1Al1.DESCRICAO_AMBIENTE
                                , DescricaoAmbienteRelacionado = entity1Al2.DESCRICAO_AMBIENTE
                                , DescricaoAplicacao = entity1Al3.DESCRICAO_APLICACAO
                                , DescricaoAplicacaoAmbienteRelacionado = entity1Al4.DESCRICAO_APLICACAO
                                , EmDesenvolvimento = entity1Al3.EM_DESENVOLVIMENTO
                                , IdAplicacao = entity1Al4.ID_APLICACAO
                                , IdAplicacaoAmbiente = entity1Al3.ID_APLICACAO
                                , IdLinxAmbienteRelacionado = entity1Al5.ID_LINX
                                , IdLinxEmpresa = entity1Al6.ID_LINX
                                , IdTcsAmbiente = entity1Al1.ID_TCS_AMBIENTE
                                , IdTcsAmbienteRelacionado = entity1Al2.ID_TCS_AMBIENTE
                                , IdTcsAplicativo = entity1Al7.ID_TCS_APLICATIVO
                                , IdTcsUsuarioAcesso = entity1.ID_TCS_USUARIO_ACESSO
                                , IdUsuario = entity1Al8.ID_USUARIO
                                , IndicaAcessoPadrao = entity1.INDICA_ACESSO_PADRAO
                                , IndicaAdministrador = entity1.INDICA_ADMINISTRADOR
                                , IndicaMultiGpecon = entity1.INDICA_MULTI_GPECON
                                , NomeEmpresaAmbienteRelacionado = entity1Al5.NOME_EMPRESA
                                , NomeAutenticacao = entity1Al8.NOME_AUTENTICACAO
                                , NomeUsuario = entity1Al8.NOME_USUARIO
		
	                        }
	                        )
			
                ,TcsIdentidadeExternaList = 
	                        (from entity1 in entity0.TCS_IDENTIDADE_EXTERNA_LISTA
                                  let entity1Al1 = entity1.TCS_USUARIO_AUTENTICACAO
	                        
	                        	
	                        select new TcsIdentidadeExterna()
	                        {
	                        
                                IdentidadeExterna = entity1.IDENTIDADE_EXTERNA
                                , IdIdentidadeExterna = entity1.ID_IDENTIDADE_EXTERNA
                                , IdUsuario = entity1Al1.ID_USUARIO
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioAcessoByEntitySearch.
	    public IQueryable<TcsUsuarioAcesso> GetTcsUsuarioAcessoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioAcesso));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioAcesso> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_ACESSO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_AMBIENTE
                  let entity0Al2 = entity0.TCS_AMBIENTE1
                  let entity0Al8 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al3 = entity0.TCS_AMBIENTE.TCS_APLICACAO
                  let entity0Al4 = entity0.TCS_AMBIENTE1.TCS_APLICACAO
                  let entity0Al6 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al5 = entity0.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al7 = entity0.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
	            
	            	
	            select new TcsUsuarioAcesso()		
	            {
	            
                DescricaoAmbiente = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAmbienteRelacionado = entity0Al2.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al3.DESCRICAO_APLICACAO
                , DescricaoAplicacaoAmbienteRelacionado = entity0Al4.DESCRICAO_APLICACAO
                , EmDesenvolvimento = entity0Al3.EM_DESENVOLVIMENTO
                , IdAplicacao = entity0Al4.ID_APLICACAO
                , IdAplicacaoAmbiente = entity0Al3.ID_APLICACAO
                , IdLinxAmbienteRelacionado = entity0Al5.ID_LINX
                , IdLinxEmpresa = entity0Al6.ID_LINX
                , IdTcsAmbiente = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsAmbienteRelacionado = entity0Al2.ID_TCS_AMBIENTE
                , IdTcsAplicativo = entity0Al7.ID_TCS_APLICATIVO
                , IdTcsUsuarioAcesso = entity0.ID_TCS_USUARIO_ACESSO
                , IdUsuario = entity0Al8.ID_USUARIO
                , IndicaAcessoPadrao = entity0.INDICA_ACESSO_PADRAO
                , IndicaAdministrador = entity0.INDICA_ADMINISTRADOR
                , IndicaMultiGpecon = entity0.INDICA_MULTI_GPECON
                , NomeEmpresaAmbienteRelacionado = entity0Al5.NOME_EMPRESA
                , NomeAutenticacao = entity0Al8.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al8.NOME_USUARIO
		
	            }
	            );
	
	        SetTcsUsuarioAcessoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsIdentidadeExternaByEntitySearch.
	    public IQueryable<TcsIdentidadeExterna> GetTcsIdentidadeExternaByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsIdentidadeExterna));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsIdentidadeExterna> result = 
	            (from entity0 in this.DbContext.TCS_IDENTIDADE_EXTERNA.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_USUARIO_AUTENTICACAO
	            
	            	
	            select new TcsIdentidadeExterna()		
	            {
	            
                IdentidadeExterna = entity0.IDENTIDADE_EXTERNA
                , IdIdentidadeExterna = entity0.ID_IDENTIDADE_EXTERNA
                , IdUsuario = entity0Al1.ID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioGpeconByEntitySearch.
	    public IQueryable<TcsUsuarioGpecon> GetTcsUsuarioGpeconByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioGpecon));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioGpecon> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_AUTENTICACAO_GPECON.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_USUARIO_AUTENTICACAO
	            
	            	
	            select new TcsUsuarioGpecon()		
	            {
	            
                IdLinx = entity0Al1.ID_LINX
                , IdTcsUsuarioAutGpecon = entity0.ID_TCS_USUARIO_AUT_GPECON
                , IdUsuario = entity0Al2.ID_USUARIO
                , NomeEmpresa = entity0Al1.NOME_EMPRESA
                , NomeAutenticacao = entity0Al2.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al2.NOME_USUARIO
		
	            }
	            );
	
	        SetTcsUsuarioGpeconBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioAutenticacaoByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




            jEntitySearch = StripUiOnlyPasswordFilters(jEntitySearch);
	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioAutenticacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioAutenticacao> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_AUTENTICACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsUsuarioAutenticacao()		
	            {
	            
                AutenticacaoWindows = entity0.AUTENTICACAO_WINDOWS
                , Bairro = entity0.BAIRRO
                , Cep = entity0.CEP
                , CnpjCpf = entity0.CNPJ_CPF
                , Complemento = entity0.COMPLEMENTO
                , ConfirmacaoUsuario = ""
                , ConfirmacaoUsuario1 = ""
                , CriaUsuario = false
                , DataAlteracao = entity0.DATA_ALTERACAO
                , DataCadastro = entity0.DATA_CADASTRO
                , DataExpiracaoSenha = entity0.DATA_EXPIRACAO_SENHA
                , Email = entity0.EMAIL
                , FoneCelular = entity0.FONE_CELULAR
                , FoneFixo = entity0.FONE_FIXO
                , GeraSenhaUsuario = false
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
                , NomeEmpresa = entity0Al1.NOME_EMPRESA
                , NomeUsuario = entity0.NOME_USUARIO
                , Numero = entity0.NUMERO
                , ObsEndereco = entity0.OBS_ENDERECO
                , Ramal = entity0.RAMAL
                , Uf = entity0.UF
                , UidEmpresa = entity0Al1.UID_EMPRESA
                , UidUsuario = entity0.UID_USUARIO
                , VigenciaFinal = entity0.VIGENCIA_FINAL
                , VigenciaInicial = entity0.VIGENCIA_INICIAL
		
	            }
	            );
		
	
	        	
            ApplyCurrentGpeconFilter(ref result);

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioAcessoByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioAcesso> GetTcsUsuarioAcessoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioAcesso));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioAcesso> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_ACESSO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_AMBIENTE
                  let entity0Al2 = entity0.TCS_AMBIENTE1
                  let entity0Al8 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al3 = entity0.TCS_AMBIENTE.TCS_APLICACAO
                  let entity0Al4 = entity0.TCS_AMBIENTE1.TCS_APLICACAO
                  let entity0Al6 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al5 = entity0.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al7 = entity0.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
	            
	            	
	            select new TcsUsuarioAcesso()		
	            {
	            
                DescricaoAmbiente = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAmbienteRelacionado = entity0Al2.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al3.DESCRICAO_APLICACAO
                , DescricaoAplicacaoAmbienteRelacionado = entity0Al4.DESCRICAO_APLICACAO
                , EmDesenvolvimento = entity0Al3.EM_DESENVOLVIMENTO
                , IdAplicacao = entity0Al4.ID_APLICACAO
                , IdAplicacaoAmbiente = entity0Al3.ID_APLICACAO
                , IdLinxAmbienteRelacionado = entity0Al5.ID_LINX
                , IdLinxEmpresa = entity0Al6.ID_LINX
                , IdTcsAmbiente = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsAmbienteRelacionado = entity0Al2.ID_TCS_AMBIENTE
                , IdTcsAplicativo = entity0Al7.ID_TCS_APLICATIVO
                , IdTcsUsuarioAcesso = entity0.ID_TCS_USUARIO_ACESSO
                , IdUsuario = entity0Al8.ID_USUARIO
                , IndicaAcessoPadrao = entity0.INDICA_ACESSO_PADRAO
                , IndicaAdministrador = entity0.INDICA_ADMINISTRADOR
                , IndicaMultiGpecon = entity0.INDICA_MULTI_GPECON
                , NomeEmpresaAmbienteRelacionado = entity0Al5.NOME_EMPRESA
                , NomeAutenticacao = entity0Al8.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al8.NOME_USUARIO
		
	            }
	            );
	
	        SetTcsUsuarioAcessoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsIdentidadeExternaByEntitySearchNoAssociations.
	    public IQueryable<TcsIdentidadeExterna> GetTcsIdentidadeExternaByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsIdentidadeExterna));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsIdentidadeExterna> result = 
	            (from entity0 in this.DbContext.TCS_IDENTIDADE_EXTERNA.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_USUARIO_AUTENTICACAO
	            
	            	
	            select new TcsIdentidadeExterna()		
	            {
	            
                IdentidadeExterna = entity0.IDENTIDADE_EXTERNA
                , IdIdentidadeExterna = entity0.ID_IDENTIDADE_EXTERNA
                , IdUsuario = entity0Al1.ID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioGpeconByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioGpecon> GetTcsUsuarioGpeconByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioGpecon));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioGpecon> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_AUTENTICACAO_GPECON.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_USUARIO_AUTENTICACAO
	            
	            	
	            select new TcsUsuarioGpecon()		
	            {
	            
                IdLinx = entity0Al1.ID_LINX
                , IdTcsUsuarioAutGpecon = entity0.ID_TCS_USUARIO_AUT_GPECON
                , IdUsuario = entity0Al2.ID_USUARIO
                , NomeEmpresa = entity0Al1.NOME_EMPRESA
                , NomeAutenticacao = entity0Al2.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al2.NOME_USUARIO
		
	            }
	            );
	
	        SetTcsUsuarioGpeconBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioAcessoParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioAcessoParentComposition> GetTcsUsuarioAcessoParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




            jEntitySearch = StripUiOnlyPasswordFilters(jEntitySearch);
	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "TCS_USUARIO_AUTENTICACAO", "TCS_USUARIO_ACESSO", "TCS_USUARIO_AUTENTICACAO", typeof(TcsUsuarioAcessoParentComposition), typeof(TcsIdentidadeExterna), typeof(TcsUsuarioGpecon));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioAcessoParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_ACESSO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_AMBIENTE
                  let entity0Al2 = entity0.TCS_AMBIENTE1
                  let entity0Al8 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al3 = entity0.TCS_AMBIENTE.TCS_APLICACAO
                  let entity0Al4 = entity0.TCS_AMBIENTE1.TCS_APLICACAO
                  let entity0Al6 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al5 = entity0.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al7 = entity0.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
	            
	            	
	            select new TcsUsuarioAcessoParentComposition()		
	            {
	            
                DescricaoAmbiente = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAmbienteRelacionado = entity0Al2.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al3.DESCRICAO_APLICACAO
                , DescricaoAplicacaoAmbienteRelacionado = entity0Al4.DESCRICAO_APLICACAO
                , EmDesenvolvimento = entity0Al3.EM_DESENVOLVIMENTO
                , IdAplicacao = entity0Al4.ID_APLICACAO
                , IdAplicacaoAmbiente = entity0Al3.ID_APLICACAO
                , IdLinxAmbienteRelacionado = entity0Al5.ID_LINX
                , IdLinxEmpresa = entity0Al6.ID_LINX
                , IdTcsAmbiente = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsAmbienteRelacionado = entity0Al2.ID_TCS_AMBIENTE
                , IdTcsAplicativo = entity0Al7.ID_TCS_APLICATIVO
                , IdTcsUsuarioAcesso = entity0.ID_TCS_USUARIO_ACESSO
                , IdUsuario = entity0Al8.ID_USUARIO
                , IndicaAcessoPadrao = entity0.INDICA_ACESSO_PADRAO
                , IndicaAdministrador = entity0.INDICA_ADMINISTRADOR
                , IndicaMultiGpecon = entity0.INDICA_MULTI_GPECON
                , NomeEmpresaAmbienteRelacionado = entity0Al5.NOME_EMPRESA
                , NomeAutenticacao = entity0Al8.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al8.NOME_USUARIO
                //TcsUsuarioAutenticacao Properties.
                , AutenticacaoWindows = entity0.TCS_USUARIO_AUTENTICACAO.AUTENTICACAO_WINDOWS
                , Bairro = entity0.TCS_USUARIO_AUTENTICACAO.BAIRRO
                , Cep = entity0.TCS_USUARIO_AUTENTICACAO.CEP
                , CnpjCpf = entity0.TCS_USUARIO_AUTENTICACAO.CNPJ_CPF
                , Complemento = entity0.TCS_USUARIO_AUTENTICACAO.COMPLEMENTO
                , ConfirmacaoUsuario = ""
                , ConfirmacaoUsuario1 = ""
                , CriaUsuario = false
                , DataAlteracao = entity0.TCS_USUARIO_AUTENTICACAO.DATA_ALTERACAO
                , DataCadastro = entity0.TCS_USUARIO_AUTENTICACAO.DATA_CADASTRO
                , DataExpiracaoSenha = entity0.TCS_USUARIO_AUTENTICACAO.DATA_EXPIRACAO_SENHA
                , Email = entity0.TCS_USUARIO_AUTENTICACAO.EMAIL
                , FoneCelular = entity0.TCS_USUARIO_AUTENTICACAO.FONE_CELULAR
                , FoneFixo = entity0.TCS_USUARIO_AUTENTICACAO.FONE_FIXO
                , GeraSenhaUsuario = false
                , IdLinx = entity0.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.ID_LINX
                , Inativo = entity0.TCS_USUARIO_AUTENTICACAO.INATIVO
                , IndicaAcessoSuporte = entity0.TCS_USUARIO_AUTENTICACAO.INDICA_ACESSO_SUPORTE
                , InscrEstadualRg = entity0.TCS_USUARIO_AUTENTICACAO.INSCR_ESTADUAL_RG
                , Logradouro = entity0.TCS_USUARIO_AUTENTICACAO.LOGRADOURO
                , LxPfjFisicaJuridica = entity0.TCS_USUARIO_AUTENTICACAO.LX_PFJ_FISICA_JURIDICA
                , LxPfjFisicaJuridicaName = ((entity0.TCS_USUARIO_AUTENTICACAO.LX_PFJ_FISICA_JURIDICA) == 1 ? "Pessoa Física" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_PFJ_FISICA_JURIDICA) == 2 ? "Pessoa Jurídica" : ""))
                , LxTipoLogradouro = entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO
                , LxTipoLogradouroName = ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 1 ? "Aeroporto" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 2 ? "Alameda" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 3 ? "Apartamento" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 4 ? "Avenida" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 5 ? "Beco" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 6 ? "Bloco" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 7 ? "Caminho" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 8 ? "Escadinha" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 9 ? "Estação" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 10 ? "Estrada" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 11 ? "Fazenda" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 12 ? "Fortaleza" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 13 ? "Galeria" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 14 ? "Ladeira" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 15 ? "Largo" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 17 ? "Parque" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 16 ? "Praça" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 18 ? "Praia" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 19 ? "Quadra" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 20 ? "Quilômetro" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 21 ? "Quinta" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 22 ? "Rodovia" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 23 ? "Rua" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 24 ? "Super Quadra" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 25 ? "Travessa" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 26 ? "Viaduto" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 27 ? "Vila" : "")))))))))))))))))))))))))))
                , Municipio = entity0.TCS_USUARIO_AUTENTICACAO.MUNICIPIO
                , NomeCurtoUsuario = entity0.TCS_USUARIO_AUTENTICACAO.NOME_CURTO_USUARIO
                , NomeEmpresa = entity0.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA
                , Numero = entity0.TCS_USUARIO_AUTENTICACAO.NUMERO
                , ObsEndereco = entity0.TCS_USUARIO_AUTENTICACAO.OBS_ENDERECO
                , Ramal = entity0.TCS_USUARIO_AUTENTICACAO.RAMAL
                , Uf = entity0.TCS_USUARIO_AUTENTICACAO.UF
                , UidEmpresa = entity0.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA
                , UidUsuario = entity0.TCS_USUARIO_AUTENTICACAO.UID_USUARIO
                , VigenciaFinal = entity0.TCS_USUARIO_AUTENTICACAO.VIGENCIA_FINAL
                , VigenciaInicial = entity0.TCS_USUARIO_AUTENTICACAO.VIGENCIA_INICIAL
		
	            }
	            );
		
	
	        	
            ApplyCurrentGpeconFilter(ref result);

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsIdentidadeExternaParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsIdentidadeExternaParentComposition> GetTcsIdentidadeExternaParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




            jEntitySearch = StripUiOnlyPasswordFilters(jEntitySearch);
	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "TCS_USUARIO_AUTENTICACAO", "TCS_IDENTIDADE_EXTERNA", "TCS_USUARIO_AUTENTICACAO", typeof(TcsIdentidadeExternaParentComposition), typeof(TcsUsuarioAcesso), typeof(TcsUsuarioGpecon));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsIdentidadeExternaParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_IDENTIDADE_EXTERNA.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_USUARIO_AUTENTICACAO
	            
	            	
	            select new TcsIdentidadeExternaParentComposition()		
	            {
	            
                IdentidadeExterna = entity0.IDENTIDADE_EXTERNA
                , IdIdentidadeExterna = entity0.ID_IDENTIDADE_EXTERNA
                , IdUsuario = entity0Al1.ID_USUARIO
                //TcsUsuarioAutenticacao Properties.
                , AutenticacaoWindows = entity0.TCS_USUARIO_AUTENTICACAO.AUTENTICACAO_WINDOWS
                , Bairro = entity0.TCS_USUARIO_AUTENTICACAO.BAIRRO
                , Cep = entity0.TCS_USUARIO_AUTENTICACAO.CEP
                , CnpjCpf = entity0.TCS_USUARIO_AUTENTICACAO.CNPJ_CPF
                , Complemento = entity0.TCS_USUARIO_AUTENTICACAO.COMPLEMENTO
                , ConfirmacaoUsuario = ""
                , ConfirmacaoUsuario1 = ""
                , CriaUsuario = false
                , DataAlteracao = entity0.TCS_USUARIO_AUTENTICACAO.DATA_ALTERACAO
                , DataCadastro = entity0.TCS_USUARIO_AUTENTICACAO.DATA_CADASTRO
                , DataExpiracaoSenha = entity0.TCS_USUARIO_AUTENTICACAO.DATA_EXPIRACAO_SENHA
                , Email = entity0.TCS_USUARIO_AUTENTICACAO.EMAIL
                , FoneCelular = entity0.TCS_USUARIO_AUTENTICACAO.FONE_CELULAR
                , FoneFixo = entity0.TCS_USUARIO_AUTENTICACAO.FONE_FIXO
                , GeraSenhaUsuario = false
                , IdLinx = entity0.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.ID_LINX
                , Inativo = entity0.TCS_USUARIO_AUTENTICACAO.INATIVO
                , IndicaAcessoSuporte = entity0.TCS_USUARIO_AUTENTICACAO.INDICA_ACESSO_SUPORTE
                , InscrEstadualRg = entity0.TCS_USUARIO_AUTENTICACAO.INSCR_ESTADUAL_RG
                , Logradouro = entity0.TCS_USUARIO_AUTENTICACAO.LOGRADOURO
                , LxPfjFisicaJuridica = entity0.TCS_USUARIO_AUTENTICACAO.LX_PFJ_FISICA_JURIDICA
                , LxPfjFisicaJuridicaName = ((entity0.TCS_USUARIO_AUTENTICACAO.LX_PFJ_FISICA_JURIDICA) == 1 ? "Pessoa Física" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_PFJ_FISICA_JURIDICA) == 2 ? "Pessoa Jurídica" : ""))
                , LxTipoLogradouro = entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO
                , LxTipoLogradouroName = ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 1 ? "Aeroporto" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 2 ? "Alameda" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 3 ? "Apartamento" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 4 ? "Avenida" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 5 ? "Beco" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 6 ? "Bloco" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 7 ? "Caminho" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 8 ? "Escadinha" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 9 ? "Estação" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 10 ? "Estrada" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 11 ? "Fazenda" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 12 ? "Fortaleza" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 13 ? "Galeria" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 14 ? "Ladeira" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 15 ? "Largo" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 17 ? "Parque" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 16 ? "Praça" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 18 ? "Praia" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 19 ? "Quadra" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 20 ? "Quilômetro" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 21 ? "Quinta" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 22 ? "Rodovia" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 23 ? "Rua" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 24 ? "Super Quadra" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 25 ? "Travessa" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 26 ? "Viaduto" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 27 ? "Vila" : "")))))))))))))))))))))))))))
                , Municipio = entity0.TCS_USUARIO_AUTENTICACAO.MUNICIPIO
                , NomeAutenticacao = entity0.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO
                , NomeCurtoUsuario = entity0.TCS_USUARIO_AUTENTICACAO.NOME_CURTO_USUARIO
                , NomeEmpresa = entity0.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA
                , NomeUsuario = entity0.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO
                , Numero = entity0.TCS_USUARIO_AUTENTICACAO.NUMERO
                , ObsEndereco = entity0.TCS_USUARIO_AUTENTICACAO.OBS_ENDERECO
                , Ramal = entity0.TCS_USUARIO_AUTENTICACAO.RAMAL
                , Uf = entity0.TCS_USUARIO_AUTENTICACAO.UF
                , UidEmpresa = entity0.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA
                , UidUsuario = entity0.TCS_USUARIO_AUTENTICACAO.UID_USUARIO
                , VigenciaFinal = entity0.TCS_USUARIO_AUTENTICACAO.VIGENCIA_FINAL
                , VigenciaInicial = entity0.TCS_USUARIO_AUTENTICACAO.VIGENCIA_INICIAL
		
	            }
	            );
		
	
	        	
            ApplyCurrentGpeconFilter(ref result);

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioGpeconParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioGpeconParentComposition> GetTcsUsuarioGpeconParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




            jEntitySearch = StripUiOnlyPasswordFilters(jEntitySearch);
	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "TCS_USUARIO_AUTENTICACAO", "TCS_USUARIO_AUTENTICACAO_GPECON", "TCS_USUARIO_AUTENTICACAO", typeof(TcsUsuarioGpeconParentComposition), typeof(TcsUsuarioAcesso), typeof(TcsIdentidadeExterna));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioGpeconParentComposition> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_AUTENTICACAO_GPECON.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_USUARIO_AUTENTICACAO
	            
	            	
	            select new TcsUsuarioGpeconParentComposition()		
	            {
	            
                IdLinx = entity0Al1.ID_LINX
                , IdTcsUsuarioAutGpecon = entity0.ID_TCS_USUARIO_AUT_GPECON
                , IdUsuario = entity0Al2.ID_USUARIO
                , NomeEmpresa = entity0Al1.NOME_EMPRESA
                , NomeAutenticacao = entity0Al2.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al2.NOME_USUARIO
                //TcsUsuarioAutenticacao Properties.
                , AutenticacaoWindows = entity0.TCS_USUARIO_AUTENTICACAO.AUTENTICACAO_WINDOWS
                , Bairro = entity0.TCS_USUARIO_AUTENTICACAO.BAIRRO
                , Cep = entity0.TCS_USUARIO_AUTENTICACAO.CEP
                , CnpjCpf = entity0.TCS_USUARIO_AUTENTICACAO.CNPJ_CPF
                , Complemento = entity0.TCS_USUARIO_AUTENTICACAO.COMPLEMENTO
                , ConfirmacaoUsuario = ""
                , ConfirmacaoUsuario1 = ""
                , CriaUsuario = false
                , DataAlteracao = entity0.TCS_USUARIO_AUTENTICACAO.DATA_ALTERACAO
                , DataCadastro = entity0.TCS_USUARIO_AUTENTICACAO.DATA_CADASTRO
                , DataExpiracaoSenha = entity0.TCS_USUARIO_AUTENTICACAO.DATA_EXPIRACAO_SENHA
                , Email = entity0.TCS_USUARIO_AUTENTICACAO.EMAIL
                , FoneCelular = entity0.TCS_USUARIO_AUTENTICACAO.FONE_CELULAR
                , FoneFixo = entity0.TCS_USUARIO_AUTENTICACAO.FONE_FIXO
                , GeraSenhaUsuario = false
                , Inativo = entity0.TCS_USUARIO_AUTENTICACAO.INATIVO
                , IndicaAcessoSuporte = entity0.TCS_USUARIO_AUTENTICACAO.INDICA_ACESSO_SUPORTE
                , InscrEstadualRg = entity0.TCS_USUARIO_AUTENTICACAO.INSCR_ESTADUAL_RG
                , Logradouro = entity0.TCS_USUARIO_AUTENTICACAO.LOGRADOURO
                , LxPfjFisicaJuridica = entity0.TCS_USUARIO_AUTENTICACAO.LX_PFJ_FISICA_JURIDICA
                , LxPfjFisicaJuridicaName = ((entity0.TCS_USUARIO_AUTENTICACAO.LX_PFJ_FISICA_JURIDICA) == 1 ? "Pessoa Física" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_PFJ_FISICA_JURIDICA) == 2 ? "Pessoa Jurídica" : ""))
                , LxTipoLogradouro = entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO
                , LxTipoLogradouroName = ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 1 ? "Aeroporto" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 2 ? "Alameda" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 3 ? "Apartamento" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 4 ? "Avenida" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 5 ? "Beco" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 6 ? "Bloco" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 7 ? "Caminho" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 8 ? "Escadinha" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 9 ? "Estação" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 10 ? "Estrada" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 11 ? "Fazenda" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 12 ? "Fortaleza" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 13 ? "Galeria" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 14 ? "Ladeira" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 15 ? "Largo" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 17 ? "Parque" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 16 ? "Praça" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 18 ? "Praia" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 19 ? "Quadra" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 20 ? "Quilômetro" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 21 ? "Quinta" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 22 ? "Rodovia" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 23 ? "Rua" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 24 ? "Super Quadra" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 25 ? "Travessa" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 26 ? "Viaduto" : ((entity0.TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO) == 27 ? "Vila" : "")))))))))))))))))))))))))))
                , Municipio = entity0.TCS_USUARIO_AUTENTICACAO.MUNICIPIO
                , NomeCurtoUsuario = entity0.TCS_USUARIO_AUTENTICACAO.NOME_CURTO_USUARIO
                , Numero = entity0.TCS_USUARIO_AUTENTICACAO.NUMERO
                , ObsEndereco = entity0.TCS_USUARIO_AUTENTICACAO.OBS_ENDERECO
                , Ramal = entity0.TCS_USUARIO_AUTENTICACAO.RAMAL
                , Uf = entity0.TCS_USUARIO_AUTENTICACAO.UF
                , UidEmpresa = entity0.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA
                , UidUsuario = entity0.TCS_USUARIO_AUTENTICACAO.UID_USUARIO
                , VigenciaFinal = entity0.TCS_USUARIO_AUTENTICACAO.VIGENCIA_FINAL
                , VigenciaInicial = entity0.TCS_USUARIO_AUTENTICACAO.VIGENCIA_INICIAL
		
	            }
	            );
		
	
	        	
            ApplyCurrentGpeconFilter(ref result);

	
	        return result;
	    }
	
	    [Ignore()]
	    private void SetTcsUsuarioAcessoBusinessFilter(ref IQueryable<TcsUsuarioAcesso> query, List<EntitySearch> entitySearchList)
	    {
	    		int idxElement;
	    		string operatorValue;
	    		object value;
	    		//Get query by functions
	    		if (entitySearchList.Count > 0)
	    		{
	    			foreach (EntitySearch search in entitySearchList.Where(e => e.EntityName == "TcsUsuarioAcesso"))
	    			{

	
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "NomeAutenticacao" || e.Value.ToString() == "TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO")))
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
	    										String tmpNomeAutenticacao1 = (String)value;
	    										query = from r in query where r.NomeAutenticacao == tmpNomeAutenticacao1 select r;
	    										break;
	    									case "!=":
	    										String tmpNomeAutenticacao2 = (String)value;
	    										query = from r in query where r.NomeAutenticacao != tmpNomeAutenticacao2 select r;
	    										break;

	
	    									case "Contains":
	    										String tmpNomeAutenticacao7 = (String)value;
	    									    query = from r in query where r.NomeAutenticacao.Contains(tmpNomeAutenticacao7) select r;
	    									    break;
	    									case "StartsWith":
	    										String tmpNomeAutenticacao8 = (String)value;
	    									    query = from r in query where r.NomeAutenticacao.StartsWith(tmpNomeAutenticacao8) select r;
	    									    break;
	    									case "EndsWith":
	    										String tmpNomeAutenticacao9 = (String)value;
	    									    query = from r in query where r.NomeAutenticacao.EndsWith(tmpNomeAutenticacao9) select r;
	    									    break;

	
	    									default:
	    										break;
	    								}                                
	    							}
	    						}
        					} 

    
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "NomeUsuario" || e.Value.ToString() == "TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO")))
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
	    										String tmpNomeUsuario1 = (String)value;
	    										query = from r in query where r.NomeUsuario == tmpNomeUsuario1 select r;
	    										break;
	    									case "!=":
	    										String tmpNomeUsuario2 = (String)value;
	    										query = from r in query where r.NomeUsuario != tmpNomeUsuario2 select r;
	    										break;

	
	    									case "Contains":
	    										String tmpNomeUsuario7 = (String)value;
	    									    query = from r in query where r.NomeUsuario.Contains(tmpNomeUsuario7) select r;
	    									    break;
	    									case "StartsWith":
	    										String tmpNomeUsuario8 = (String)value;
	    									    query = from r in query where r.NomeUsuario.StartsWith(tmpNomeUsuario8) select r;
	    									    break;
	    									case "EndsWith":
	    										String tmpNomeUsuario9 = (String)value;
	    									    query = from r in query where r.NomeUsuario.EndsWith(tmpNomeUsuario9) select r;
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
	    private void SetTcsUsuarioGpeconBusinessFilter(ref IQueryable<TcsUsuarioGpecon> query, List<EntitySearch> entitySearchList)
	    {
	    		int idxElement;
	    		string operatorValue;
	    		object value;
	    		//Get query by functions
	    		if (entitySearchList.Count > 0)
	    		{
	    			foreach (EntitySearch search in entitySearchList.Where(e => e.EntityName == "TcsUsuarioGpecon"))
	    			{

	
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "NomeAutenticacao" || e.Value.ToString() == "TCS_USUARIO_AUTENTICACAO_GPECON.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO")))
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
	    										System.String tmpNomeAutenticacao1 = (System.String)value;
	    										query = from r in query where r.NomeAutenticacao == tmpNomeAutenticacao1 select r;
	    										break;
	    									case "!=":
	    										System.String tmpNomeAutenticacao2 = (System.String)value;
	    										query = from r in query where r.NomeAutenticacao != tmpNomeAutenticacao2 select r;
	    										break;

	
	    									case "Contains":
	    										System.String tmpNomeAutenticacao7 = (System.String)value;
	    									    query = from r in query where r.NomeAutenticacao.Contains(tmpNomeAutenticacao7) select r;
	    									    break;
	    									case "StartsWith":
	    										System.String tmpNomeAutenticacao8 = (System.String)value;
	    									    query = from r in query where r.NomeAutenticacao.StartsWith(tmpNomeAutenticacao8) select r;
	    									    break;
	    									case "EndsWith":
	    										System.String tmpNomeAutenticacao9 = (System.String)value;
	    									    query = from r in query where r.NomeAutenticacao.EndsWith(tmpNomeAutenticacao9) select r;
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
	    //Get RequisicaoAcessoByEntitySearch.
	    public IEnumerable<RequisicaoAcesso> GetRequisicaoAcessoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<RequisicaoAcesso> result = new List<RequisicaoAcesso>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get RequisicaoAcessoByEntitySearchNoAssociations.
	    public IEnumerable<RequisicaoAcesso> GetRequisicaoAcessoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<RequisicaoAcesso> result = new List<RequisicaoAcesso>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get UsuarioAcessoByEntitySearch.
	    public IEnumerable<UsuarioAcesso> GetUsuarioAcessoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<UsuarioAcesso> result = new List<UsuarioAcesso>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get UsuarioAcessoByEntitySearchNoAssociations.
	    public IEnumerable<UsuarioAcesso> GetUsuarioAcessoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<UsuarioAcesso> result = new List<UsuarioAcesso>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsSuporteAcessoLogByEntitySearch.
	    public IQueryable<TcsSuporteAcessoLog> GetTcsSuporteAcessoLogByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsSuporteAcessoLog));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsSuporteAcessoLog> result = 
	            (from entity0 in this.DbContext.TCS_SUPORTE_ACESSO_LOG.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.USUARIO_ACESSO
                  let entity0Al3 = entity0.USUARIO_SUPORTE
                  let entity0Al1 = entity0.TCS_USUARIO_ACESSO
	            
	            	
	            select new TcsSuporteAcessoLog()		
	            {
	            
                AcessoExpirado = entity0.ACESSO_EXPIRADO
                , DataAcesso = entity0.DATA_ACESSO
                , DataCadastro = entity0.DATA_CADASTRO
                , IdTcsSuporteAcessoLog = entity0.ID_TCS_SUPORTE_ACESSO_LOG
                , IdTcsUsuarioAcesso = entity0Al1.ID_TCS_USUARIO_ACESSO
                , IdUsuarioAcesso = entity0Al2.ID_USUARIO
                , IdUsuarioSuporte = entity0Al3.ID_USUARIO
                , NomeAutenticacaoAcesso = entity0Al2.NOME_AUTENTICACAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsSuporteAcessoLogByEntitySearchNoAssociations.
	    public IQueryable<TcsSuporteAcessoLog> GetTcsSuporteAcessoLogByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsSuporteAcessoLog));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsSuporteAcessoLog> result = 
	            (from entity0 in this.DbContext.TCS_SUPORTE_ACESSO_LOG.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.USUARIO_ACESSO
                  let entity0Al3 = entity0.USUARIO_SUPORTE
                  let entity0Al1 = entity0.TCS_USUARIO_ACESSO
	            
	            	
	            select new TcsSuporteAcessoLog()		
	            {
	            
                AcessoExpirado = entity0.ACESSO_EXPIRADO
                , DataAcesso = entity0.DATA_ACESSO
                , DataCadastro = entity0.DATA_CADASTRO
                , IdTcsSuporteAcessoLog = entity0.ID_TCS_SUPORTE_ACESSO_LOG
                , IdTcsUsuarioAcesso = entity0Al1.ID_TCS_USUARIO_ACESSO
                , IdUsuarioAcesso = entity0Al2.ID_USUARIO
                , IdUsuarioSuporte = entity0Al3.ID_USUARIO
                , NomeAutenticacaoAcesso = entity0Al2.NOME_AUTENTICACAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get RequisicaoSuporteByEntitySearch.
	    public IEnumerable<RequisicaoSuporte> GetRequisicaoSuporteByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<RequisicaoSuporte> result = new List<RequisicaoSuporte>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get RequisicaoSuporteByEntitySearchNoAssociations.
	    public IEnumerable<RequisicaoSuporte> GetRequisicaoSuporteByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<RequisicaoSuporte> result = new List<RequisicaoSuporte>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioAcessoAmbienteByEntitySearch.
	    public IQueryable<TcsUsuarioAcessoAmbiente> GetTcsUsuarioAcessoAmbienteByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioAcessoAmbiente));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioAcessoAmbiente> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_ACESSO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_AMBIENTE
                  let entity0Al5 = entity0.TCS_AMBIENTE1
                  let entity0Al6 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_AMBIENTE.TCS_APLICACAO
                  let entity0Al7 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al3 = entity0.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
                  let entity0Al4 = entity0.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsUsuarioAcessoAmbiente()		
	            {
	            
                DescricaoAmbiente = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al2.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0Al3.DESCRICAO_APLICATIVO
                , EmDesenvolvimento = entity0Al2.EM_DESENVOLVIMENTO
                , GrupoEconomico = entity0Al4.NOME_EMPRESA
                , IdLinxGpecon = entity0Al4.ID_LINX
                , IdTcsAmbiente = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsAmbienteRelacionado = entity0Al5.ID_TCS_AMBIENTE
                , IdTcsAplicativo = entity0Al3.ID_TCS_APLICATIVO
                , IdTcsUsuarioAcesso = entity0.ID_TCS_USUARIO_ACESSO
                , IdUsuario = entity0Al6.ID_USUARIO
                , IndicaAcessoPadrao = entity0.INDICA_ACESSO_PADRAO
                , IndicaAdministrador = entity0.INDICA_ADMINISTRADOR
                , NomeEmpresa = entity0Al7.NOME_EMPRESA
                , UidAplicacao = entity0Al2.UID_APLICACAO
                , UidEmpresa = entity0Al7.UID_EMPRESA
                , UidGrupoEconomico = entity0Al4.UID_EMPRESA
                , UidUsuario = entity0Al6.UID_USUARIO
                , Url = entity0Al2.URL
                , UrlWorkArea = entity0Al2.URL_WORK_AREA
                , NomeAutenticacao = entity0Al6.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al6.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioAcessoAmbienteByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioAcessoAmbiente> GetTcsUsuarioAcessoAmbienteByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioAcessoAmbiente));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioAcessoAmbiente> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_ACESSO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_AMBIENTE
                  let entity0Al5 = entity0.TCS_AMBIENTE1
                  let entity0Al6 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_AMBIENTE.TCS_APLICACAO
                  let entity0Al7 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al3 = entity0.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
                  let entity0Al4 = entity0.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsUsuarioAcessoAmbiente()		
	            {
	            
                DescricaoAmbiente = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al2.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0Al3.DESCRICAO_APLICATIVO
                , EmDesenvolvimento = entity0Al2.EM_DESENVOLVIMENTO
                , GrupoEconomico = entity0Al4.NOME_EMPRESA
                , IdLinxGpecon = entity0Al4.ID_LINX
                , IdTcsAmbiente = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsAmbienteRelacionado = entity0Al5.ID_TCS_AMBIENTE
                , IdTcsAplicativo = entity0Al3.ID_TCS_APLICATIVO
                , IdTcsUsuarioAcesso = entity0.ID_TCS_USUARIO_ACESSO
                , IdUsuario = entity0Al6.ID_USUARIO
                , IndicaAcessoPadrao = entity0.INDICA_ACESSO_PADRAO
                , IndicaAdministrador = entity0.INDICA_ADMINISTRADOR
                , NomeEmpresa = entity0Al7.NOME_EMPRESA
                , UidAplicacao = entity0Al2.UID_APLICACAO
                , UidEmpresa = entity0Al7.UID_EMPRESA
                , UidGrupoEconomico = entity0Al4.UID_EMPRESA
                , UidUsuario = entity0Al6.UID_USUARIO
                , Url = entity0Al2.URL
                , UrlWorkArea = entity0Al2.URL_WORK_AREA
                , NomeAutenticacao = entity0Al6.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al6.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioAutenticacaoAcessoPByEntitySearch.
	    public IQueryable<TcsUsuarioAutenticacaoAcessoP> GetTcsUsuarioAutenticacaoAcessoPByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioAutenticacaoAcessoP));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioAutenticacaoAcessoP> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_ACESSO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_AMBIENTE
                  let entity0Al2 = entity0.TCS_AMBIENTE1
                  let entity0Al6 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al3 = entity0.TCS_AMBIENTE.TCS_APLICACAO
                  let entity0Al5 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al4 = entity0.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
	            
	            	
	            select new TcsUsuarioAutenticacaoAcessoP()		
	            {
	            
                DescricaoAmbiente = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAmbienteRelacionado = entity0Al2.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al3.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0Al4.DESCRICAO_APLICATIVO
                , IdAplicacao = entity0Al3.ID_APLICACAO
                , IdLinx = entity0Al5.ID_LINX
                , IdTcsAmbiente = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsAmbienteRelacionado = entity0Al2.ID_TCS_AMBIENTE
                , IdTcsAplicativo = entity0Al4.ID_TCS_APLICATIVO
                , IdTcsUsuarioAcesso = entity0.ID_TCS_USUARIO_ACESSO
                , IdUsuario = entity0Al6.ID_USUARIO
                , IndicaAcessoPadrao = entity0.INDICA_ACESSO_PADRAO
                , NomeEmpresa = entity0Al5.NOME_EMPRESA
                , Perfil = ""
                , NomeAutenticacao = entity0Al6.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al6.NOME_USUARIO
		
	            }
	            );
	
	        SetTcsUsuarioAutenticacaoAcessoPBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioAutenticacaoAcessoPByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioAutenticacaoAcessoP> GetTcsUsuarioAutenticacaoAcessoPByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioAutenticacaoAcessoP));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioAutenticacaoAcessoP> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_ACESSO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_AMBIENTE
                  let entity0Al2 = entity0.TCS_AMBIENTE1
                  let entity0Al6 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al3 = entity0.TCS_AMBIENTE.TCS_APLICACAO
                  let entity0Al5 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al4 = entity0.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
	            
	            	
	            select new TcsUsuarioAutenticacaoAcessoP()		
	            {
	            
                DescricaoAmbiente = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAmbienteRelacionado = entity0Al2.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al3.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0Al4.DESCRICAO_APLICATIVO
                , IdAplicacao = entity0Al3.ID_APLICACAO
                , IdLinx = entity0Al5.ID_LINX
                , IdTcsAmbiente = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsAmbienteRelacionado = entity0Al2.ID_TCS_AMBIENTE
                , IdTcsAplicativo = entity0Al4.ID_TCS_APLICATIVO
                , IdTcsUsuarioAcesso = entity0.ID_TCS_USUARIO_ACESSO
                , IdUsuario = entity0Al6.ID_USUARIO
                , IndicaAcessoPadrao = entity0.INDICA_ACESSO_PADRAO
                , NomeEmpresa = entity0Al5.NOME_EMPRESA
                , Perfil = ""
                , NomeAutenticacao = entity0Al6.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al6.NOME_USUARIO
		
	            }
	            );
	
	        SetTcsUsuarioAutenticacaoAcessoPBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
	
	    [Ignore()]
	    private void SetTcsUsuarioAutenticacaoAcessoPBusinessFilter(ref IQueryable<TcsUsuarioAutenticacaoAcessoP> query, List<EntitySearch> entitySearchList)
	    {
	    		int idxElement;
	    		string operatorValue;
	    		object value;
	    		//Get query by functions
	    		if (entitySearchList.Count > 0)
	    		{
	    			foreach (EntitySearch search in entitySearchList.Where(e => e.EntityName == "TcsUsuarioAutenticacaoAcessoP"))
	    			{

	
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "Perfil" || e.Value.ToString() == "''")))
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
	    										string tmpPerfil1 = (string)value;
	    										query = from r in query where r.Perfil == tmpPerfil1 select r;
	    										break;
	    									case "!=":
	    										string tmpPerfil2 = (string)value;
	    										query = from r in query where r.Perfil != tmpPerfil2 select r;
	    										break;

	
	    									case "Contains":
	    										string tmpPerfil7 = (string)value;
	    									    query = from r in query where r.Perfil.Contains(tmpPerfil7) select r;
	    									    break;
	    									case "StartsWith":
	    										string tmpPerfil8 = (string)value;
	    									    query = from r in query where r.Perfil.StartsWith(tmpPerfil8) select r;
	    									    break;
	    									case "EndsWith":
	    										string tmpPerfil9 = (string)value;
	    									    query = from r in query where r.Perfil.EndsWith(tmpPerfil9) select r;
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
	    //Get PagedTcsUsuarioAutenticacao.
	    public IQueryable<TcsUsuarioAutenticacao> GetPagedTcsUsuarioAutenticacao(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioAutenticacao));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioAutenticacao> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_AUTENTICACAO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_EMPRESA_AUTENTICACAO
                orderby entity0.ID_USUARIO ascending
	            
	            	
	            select new TcsUsuarioAutenticacao()		
	            {
	            
                AutenticacaoWindows = entity0.AUTENTICACAO_WINDOWS
                , Bairro = entity0.BAIRRO
                , Cep = entity0.CEP
                , CnpjCpf = entity0.CNPJ_CPF
                , Complemento = entity0.COMPLEMENTO
                , ConfirmacaoUsuario = ""
                , ConfirmacaoUsuario1 = ""
                , CriaUsuario = false
                , DataAlteracao = entity0.DATA_ALTERACAO
                , DataCadastro = entity0.DATA_CADASTRO
                , DataExpiracaoSenha = entity0.DATA_EXPIRACAO_SENHA
                , Email = entity0.EMAIL
                , FoneCelular = entity0.FONE_CELULAR
                , FoneFixo = entity0.FONE_FIXO
                , GeraSenhaUsuario = false
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
                , NomeEmpresa = entity0Al1.NOME_EMPRESA
                , NomeUsuario = entity0.NOME_USUARIO
                , Numero = entity0.NUMERO
                , ObsEndereco = entity0.OBS_ENDERECO
                , Ramal = entity0.RAMAL
                , Uf = entity0.UF
                , UidEmpresa = entity0Al1.UID_EMPRESA
                , UidUsuario = entity0.UID_USUARIO
                , VigenciaFinal = entity0.VIGENCIA_FINAL
                , VigenciaInicial = entity0.VIGENCIA_INICIAL
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsUsuarioAcesso.
	    public IQueryable<TcsUsuarioAcesso> GetPagedTcsUsuarioAcesso(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioAcesso));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioAcesso> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_ACESSO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_AMBIENTE
                  let entity0Al2 = entity0.TCS_AMBIENTE1
                  let entity0Al8 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al3 = entity0.TCS_AMBIENTE.TCS_APLICACAO
                  let entity0Al4 = entity0.TCS_AMBIENTE1.TCS_APLICACAO
                  let entity0Al6 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al5 = entity0.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al7 = entity0.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
                orderby entity0.ID_TCS_USUARIO_ACESSO ascending
	            
	            	
	            select new TcsUsuarioAcesso()		
	            {
	            
                DescricaoAmbiente = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAmbienteRelacionado = entity0Al2.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al3.DESCRICAO_APLICACAO
                , DescricaoAplicacaoAmbienteRelacionado = entity0Al4.DESCRICAO_APLICACAO
                , EmDesenvolvimento = entity0Al3.EM_DESENVOLVIMENTO
                , IdAplicacao = entity0Al4.ID_APLICACAO
                , IdAplicacaoAmbiente = entity0Al3.ID_APLICACAO
                , IdLinxAmbienteRelacionado = entity0Al5.ID_LINX
                , IdLinxEmpresa = entity0Al6.ID_LINX
                , IdTcsAmbiente = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsAmbienteRelacionado = entity0Al2.ID_TCS_AMBIENTE
                , IdTcsAplicativo = entity0Al7.ID_TCS_APLICATIVO
                , IdTcsUsuarioAcesso = entity0.ID_TCS_USUARIO_ACESSO
                , IdUsuario = entity0Al8.ID_USUARIO
                , IndicaAcessoPadrao = entity0.INDICA_ACESSO_PADRAO
                , IndicaAdministrador = entity0.INDICA_ADMINISTRADOR
                , IndicaMultiGpecon = entity0.INDICA_MULTI_GPECON
                , NomeEmpresaAmbienteRelacionado = entity0Al5.NOME_EMPRESA
                , NomeAutenticacao = entity0Al8.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al8.NOME_USUARIO
		
	            }
	            ).Skip(skip).Take(take);
	
	        SetTcsUsuarioAcessoBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsIdentidadeExterna.
	    public IQueryable<TcsIdentidadeExterna> GetPagedTcsIdentidadeExterna(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsIdentidadeExterna));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsIdentidadeExterna> result = 
	            (from entity0 in this.DbContext.TCS_IDENTIDADE_EXTERNA.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_USUARIO_AUTENTICACAO
                orderby entity0.ID_IDENTIDADE_EXTERNA ascending
	            
	            	
	            select new TcsIdentidadeExterna()		
	            {
	            
                IdentidadeExterna = entity0.IDENTIDADE_EXTERNA
                , IdIdentidadeExterna = entity0.ID_IDENTIDADE_EXTERNA
                , IdUsuario = entity0Al1.ID_USUARIO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsUsuarioGpecon.
	    public IQueryable<TcsUsuarioGpecon> GetPagedTcsUsuarioGpecon(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioGpecon));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioGpecon> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_AUTENTICACAO_GPECON.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_USUARIO_AUTENTICACAO
                orderby entity0.ID_TCS_USUARIO_AUT_GPECON ascending
	            
	            	
	            select new TcsUsuarioGpecon()		
	            {
	            
                IdLinx = entity0Al1.ID_LINX
                , IdTcsUsuarioAutGpecon = entity0.ID_TCS_USUARIO_AUT_GPECON
                , IdUsuario = entity0Al2.ID_USUARIO
                , NomeEmpresa = entity0Al1.NOME_EMPRESA
                , NomeAutenticacao = entity0Al2.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al2.NOME_USUARIO
		
	            }
	            ).Skip(skip).Take(take);
	
	        SetTcsUsuarioGpeconBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsUsuarioAutenticacaoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioAutenticacao));
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
	    public int GetTcsUsuarioAcessoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioAcesso));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_USUARIO_ACESSO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_AMBIENTE
                  let entityAl2 = entity.TCS_AMBIENTE1
                  let entityAl8 = entity.TCS_USUARIO_AUTENTICACAO
                  let entityAl3 = entity.TCS_AMBIENTE.TCS_APLICACAO
                  let entityAl4 = entity.TCS_AMBIENTE1.TCS_APLICACAO
                  let entityAl6 = entity.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entityAl5 = entity.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO
                  let entityAl7 = entity.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsIdentidadeExternaCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsIdentidadeExterna));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_IDENTIDADE_EXTERNA.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_USUARIO_AUTENTICACAO
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsUsuarioGpeconCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioGpecon));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_USUARIO_AUTENTICACAO_GPECON.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_EMPRESA_AUTENTICACAO
                  let entityAl2 = entity.TCS_USUARIO_AUTENTICACAO
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedRequisicaoAcesso.
	    public IEnumerable<RequisicaoAcesso> GetPagedRequisicaoAcesso(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<RequisicaoAcesso> result = new List<RequisicaoAcesso>();
	  	
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetRequisicaoAcessoCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
			
	
	    
	    [Ignore]
	    //Get PagedUsuarioAcesso.
	    public IEnumerable<UsuarioAcesso> GetPagedUsuarioAcesso(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<UsuarioAcesso> result = new List<UsuarioAcesso>();
	  	
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetUsuarioAcessoCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsSuporteAcessoLog.
	    public IQueryable<TcsSuporteAcessoLog> GetPagedTcsSuporteAcessoLog(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsSuporteAcessoLog));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsSuporteAcessoLog> result = 
	            (from entity0 in this.DbContext.TCS_SUPORTE_ACESSO_LOG.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.USUARIO_ACESSO
                  let entity0Al3 = entity0.USUARIO_SUPORTE
                  let entity0Al1 = entity0.TCS_USUARIO_ACESSO
                orderby entity0.ID_TCS_SUPORTE_ACESSO_LOG ascending
	            
	            	
	            select new TcsSuporteAcessoLog()		
	            {
	            
                AcessoExpirado = entity0.ACESSO_EXPIRADO
                , DataAcesso = entity0.DATA_ACESSO
                , DataCadastro = entity0.DATA_CADASTRO
                , IdTcsSuporteAcessoLog = entity0.ID_TCS_SUPORTE_ACESSO_LOG
                , IdTcsUsuarioAcesso = entity0Al1.ID_TCS_USUARIO_ACESSO
                , IdUsuarioAcesso = entity0Al2.ID_USUARIO
                , IdUsuarioSuporte = entity0Al3.ID_USUARIO
                , NomeAutenticacaoAcesso = entity0Al2.NOME_AUTENTICACAO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsSuporteAcessoLogCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsSuporteAcessoLog));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_SUPORTE_ACESSO_LOG.Where(dynQuery, parameters.ToArray())
                  let entityAl2 = entity.USUARIO_ACESSO
                  let entityAl3 = entity.USUARIO_SUPORTE
                  let entityAl1 = entity.TCS_USUARIO_ACESSO
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedRequisicaoSuporte.
	    public IEnumerable<RequisicaoSuporte> GetPagedRequisicaoSuporte(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<RequisicaoSuporte> result = new List<RequisicaoSuporte>();
	  	
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetRequisicaoSuporteCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsUsuarioAcessoAmbiente.
	    public IQueryable<TcsUsuarioAcessoAmbiente> GetPagedTcsUsuarioAcessoAmbiente(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioAcessoAmbiente));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioAcessoAmbiente> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_ACESSO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_AMBIENTE
                  let entity0Al5 = entity0.TCS_AMBIENTE1
                  let entity0Al6 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al2 = entity0.TCS_AMBIENTE.TCS_APLICACAO
                  let entity0Al7 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al3 = entity0.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
                  let entity0Al4 = entity0.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO
                orderby entity0.ID_TCS_USUARIO_ACESSO ascending
	            
	            	
	            select new TcsUsuarioAcessoAmbiente()		
	            {
	            
                DescricaoAmbiente = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al2.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0Al3.DESCRICAO_APLICATIVO
                , EmDesenvolvimento = entity0Al2.EM_DESENVOLVIMENTO
                , GrupoEconomico = entity0Al4.NOME_EMPRESA
                , IdLinxGpecon = entity0Al4.ID_LINX
                , IdTcsAmbiente = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsAmbienteRelacionado = entity0Al5.ID_TCS_AMBIENTE
                , IdTcsAplicativo = entity0Al3.ID_TCS_APLICATIVO
                , IdTcsUsuarioAcesso = entity0.ID_TCS_USUARIO_ACESSO
                , IdUsuario = entity0Al6.ID_USUARIO
                , IndicaAcessoPadrao = entity0.INDICA_ACESSO_PADRAO
                , IndicaAdministrador = entity0.INDICA_ADMINISTRADOR
                , NomeEmpresa = entity0Al7.NOME_EMPRESA
                , UidAplicacao = entity0Al2.UID_APLICACAO
                , UidEmpresa = entity0Al7.UID_EMPRESA
                , UidGrupoEconomico = entity0Al4.UID_EMPRESA
                , UidUsuario = entity0Al6.UID_USUARIO
                , Url = entity0Al2.URL
                , UrlWorkArea = entity0Al2.URL_WORK_AREA
                , NomeAutenticacao = entity0Al6.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al6.NOME_USUARIO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsUsuarioAcessoAmbienteCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioAcessoAmbiente));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_USUARIO_ACESSO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_AMBIENTE
                  let entityAl5 = entity.TCS_AMBIENTE1
                  let entityAl6 = entity.TCS_USUARIO_AUTENTICACAO
                  let entityAl2 = entity.TCS_AMBIENTE.TCS_APLICACAO
                  let entityAl7 = entity.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entityAl3 = entity.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
                  let entityAl4 = entity.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsUsuarioAutenticacaoAcessoP.
	    public IQueryable<TcsUsuarioAutenticacaoAcessoP> GetPagedTcsUsuarioAutenticacaoAcessoP(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioAutenticacaoAcessoP));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioAutenticacaoAcessoP> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_ACESSO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_AMBIENTE
                  let entity0Al2 = entity0.TCS_AMBIENTE1
                  let entity0Al6 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al3 = entity0.TCS_AMBIENTE.TCS_APLICACAO
                  let entity0Al5 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al4 = entity0.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
                orderby entity0.ID_TCS_USUARIO_ACESSO ascending
	            
	            	
	            select new TcsUsuarioAutenticacaoAcessoP()		
	            {
	            
                DescricaoAmbiente = entity0Al1.DESCRICAO_AMBIENTE
                , DescricaoAmbienteRelacionado = entity0Al2.DESCRICAO_AMBIENTE
                , DescricaoAplicacao = entity0Al3.DESCRICAO_APLICACAO
                , DescricaoAplicativo = entity0Al4.DESCRICAO_APLICATIVO
                , IdAplicacao = entity0Al3.ID_APLICACAO
                , IdLinx = entity0Al5.ID_LINX
                , IdTcsAmbiente = entity0Al1.ID_TCS_AMBIENTE
                , IdTcsAmbienteRelacionado = entity0Al2.ID_TCS_AMBIENTE
                , IdTcsAplicativo = entity0Al4.ID_TCS_APLICATIVO
                , IdTcsUsuarioAcesso = entity0.ID_TCS_USUARIO_ACESSO
                , IdUsuario = entity0Al6.ID_USUARIO
                , IndicaAcessoPadrao = entity0.INDICA_ACESSO_PADRAO
                , NomeEmpresa = entity0Al5.NOME_EMPRESA
                , Perfil = ""
                , NomeAutenticacao = entity0Al6.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al6.NOME_USUARIO
		
	            }
	            ).Skip(skip).Take(take);
	
	        SetTcsUsuarioAutenticacaoAcessoPBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsUsuarioAutenticacaoAcessoPCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioAutenticacaoAcessoP));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_USUARIO_ACESSO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_AMBIENTE
                  let entityAl2 = entity.TCS_AMBIENTE1
                  let entityAl6 = entity.TCS_USUARIO_AUTENTICACAO
                  let entityAl3 = entity.TCS_AMBIENTE.TCS_APLICACAO
                  let entityAl5 = entity.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entityAl4 = entity.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
	            
	            select 1
	            ).Count();	
		
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    
	    [Update()]	
	    //Update TcsUsuarioAutenticacao.
	    public void UpdateTcsUsuarioAutenticacao(TcsUsuarioAutenticacao entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsUsuarioAutenticacao.
	    public void InsertTcsUsuarioAutenticacao(TcsUsuarioAutenticacao entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsUsuarioAutenticacao.
	    public void DeleteTcsUsuarioAutenticacao(TcsUsuarioAutenticacao entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsUsuarioAcesso.
	    public void UpdateTcsUsuarioAcesso(TcsUsuarioAcesso entity)
	    {



	
	        if (entity.TcsUsuarioAutenticacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsUsuarioAutenticacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsUsuarioAutenticacao); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsUsuarioAcesso.
	    public void InsertTcsUsuarioAcesso(TcsUsuarioAcesso entity)
	    {



	
	        if (entity.TcsUsuarioAutenticacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsUsuarioAutenticacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsUsuarioAutenticacao);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsUsuarioAcesso.
	    public void DeleteTcsUsuarioAcesso(TcsUsuarioAcesso entity)
	    {



	
	        if (entity.TcsUsuarioAutenticacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsUsuarioAutenticacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsUsuarioAutenticacao);
	            

	
	        }

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsIdentidadeExterna.
	    public void UpdateTcsIdentidadeExterna(TcsIdentidadeExterna entity)
	    {



	
	        if (entity.TcsUsuarioAutenticacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsUsuarioAutenticacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsUsuarioAutenticacao); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsIdentidadeExterna.
	    public void InsertTcsIdentidadeExterna(TcsIdentidadeExterna entity)
	    {



	
	        if (entity.TcsUsuarioAutenticacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsUsuarioAutenticacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsUsuarioAutenticacao);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsIdentidadeExterna.
	    public void DeleteTcsIdentidadeExterna(TcsIdentidadeExterna entity)
	    {



	
	        if (entity.TcsUsuarioAutenticacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsUsuarioAutenticacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsUsuarioAutenticacao);
	            

	
	        }

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsUsuarioGpecon.
	    public void UpdateTcsUsuarioGpecon(TcsUsuarioGpecon entity)
	    {



	
	        if (entity.TcsUsuarioAutenticacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsUsuarioAutenticacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.TcsUsuarioAutenticacao); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert TcsUsuarioGpecon.
	    public void InsertTcsUsuarioGpecon(TcsUsuarioGpecon entity)
	    {



	
	        if (entity.TcsUsuarioAutenticacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsUsuarioAutenticacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.TcsUsuarioAutenticacao);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete TcsUsuarioGpecon.
	    public void DeleteTcsUsuarioGpecon(TcsUsuarioGpecon entity)
	    {



	
	        if (entity.TcsUsuarioAutenticacao.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.TcsUsuarioAutenticacao) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.TcsUsuarioAutenticacao);
	            

	
	        }

	
	    }
		
			
	    
	    [Update()]	
	    //Update RequisicaoAcesso.
	    public void UpdateRequisicaoAcesso(RequisicaoAcesso entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert RequisicaoAcesso.
	    public void InsertRequisicaoAcesso(RequisicaoAcesso entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete RequisicaoAcesso.
	    public void DeleteRequisicaoAcesso(RequisicaoAcesso entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update UsuarioAcesso.
	    public void UpdateUsuarioAcesso(UsuarioAcesso entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert UsuarioAcesso.
	    public void InsertUsuarioAcesso(UsuarioAcesso entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete UsuarioAcesso.
	    public void DeleteUsuarioAcesso(UsuarioAcesso entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsSuporteAcessoLog.
	    public void UpdateTcsSuporteAcessoLog(TcsSuporteAcessoLog entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsSuporteAcessoLog.
	    public void InsertTcsSuporteAcessoLog(TcsSuporteAcessoLog entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsSuporteAcessoLog.
	    public void DeleteTcsSuporteAcessoLog(TcsSuporteAcessoLog entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update RequisicaoSuporte.
	    public void UpdateRequisicaoSuporte(RequisicaoSuporte entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert RequisicaoSuporte.
	    public void InsertRequisicaoSuporte(RequisicaoSuporte entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete RequisicaoSuporte.
	    public void DeleteRequisicaoSuporte(RequisicaoSuporte entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsUsuarioAcessoAmbiente.
	    public void UpdateTcsUsuarioAcessoAmbiente(TcsUsuarioAcessoAmbiente entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsUsuarioAcessoAmbiente.
	    public void InsertTcsUsuarioAcessoAmbiente(TcsUsuarioAcessoAmbiente entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsUsuarioAcessoAmbiente.
	    public void DeleteTcsUsuarioAcessoAmbiente(TcsUsuarioAcessoAmbiente entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsUsuarioAutenticacaoAcessoP.
	    public void UpdateTcsUsuarioAutenticacaoAcessoP(TcsUsuarioAutenticacaoAcessoP entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsUsuarioAutenticacaoAcessoP.
	    public void InsertTcsUsuarioAutenticacaoAcessoP(TcsUsuarioAutenticacaoAcessoP entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsUsuarioAutenticacaoAcessoP.
	    public void DeleteTcsUsuarioAutenticacaoAcessoP(TcsUsuarioAutenticacaoAcessoP entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}