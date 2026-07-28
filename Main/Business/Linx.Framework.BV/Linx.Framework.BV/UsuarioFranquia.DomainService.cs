					
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

namespace Linx.Framework.BV.UsuarioFranquia
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_USUARIO_AUTENTICACAO.ID_USUARIO", IsUpdatable=false, EdmName="")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsUsuarioAutenticacao,TcsUsuarioAutenticacao.TcsUsuarioAutenticacaoAcesso];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdUsuario];ReadOnly[false];Entities[:IdUsuario];SubQueryInfo[];EdmEntityName[];EntityRelations[ ];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioAutenticacao")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.UsuarioFranquia.TcsUsuarioAutenticacao")]
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
	      if (this.TcsUsuarioAutenticacaoAcessoList != null && this.TcsUsuarioAutenticacaoAcessoList.Count() > 0)
	      {
	         foreach (var entity in this.TcsUsuarioAutenticacaoAcessoList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.TcsUsuarioAutenticacaoAcessoList != null)
	      {
	         foreach (var detail in this.TcsUsuarioAutenticacaoAcessoList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsUsuarioAutenticacaoAcessoList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(UsuarioFranquiaDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("TcsUsuarioAutenticacaoAcesso"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsUsuarioAutenticacaoAcesso");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuario"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdUsuario));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsUsuarioAutenticacaoAcesso and all sub-details
	         if (this.TcsUsuarioAutenticacaoAcessoList == null || this.TcsUsuarioAutenticacaoAcessoList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsUsuarioAutenticacaoAcessoList = context.GetPagedTcsUsuarioAutenticacaoAcesso(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsUsuarioAutenticacaoAcessoList = (from r in context.GetTcsUsuarioAutenticacaoAcessoByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _TcsUsuarioAutenticacaoAcessoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioAutenticacaoAcesso && ((TcsUsuarioAutenticacaoAcesso)e.Entity).TcsUsuarioAutenticacao == null && e.Associations == null && e.OriginalAssociations == null && ((TcsUsuarioAutenticacaoAcesso)e.Entity).IdUsuario == this.IdUsuario).ToList();
 	      if (_TcsUsuarioAutenticacaoAcessoElements.Count > 0 && this.TcsUsuarioAutenticacaoAcessoList.Count() == 0)
 	      {
 	          this.TcsUsuarioAutenticacaoAcessoList = _TcsUsuarioAutenticacaoAcessoElements.Select(e => (TcsUsuarioAutenticacaoAcesso)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsUsuarioAutenticacaoAcessoElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsUsuarioAutenticacaoAcesso)detail.Entity).TcsUsuarioAutenticacao = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsUsuarioAutenticacao", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsUsuarioAutenticacaoAcessoList", indexDetails.ToArray());
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
	    [Display(Name = "Utiliza Autenticação Windows", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    partial void OnBairroChanging(String value);
	    partial void OnBairroChanged();

	    private String _Bairro;

	    [DataMember(Name = "Bairro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bairro", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.BAIRRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.BAIRRO")]
	    public String Bairro
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
	    partial void OnCepChanging(String value);
	    partial void OnCepChanged();

	    private String _Cep;

	    [DataMember(Name = "Cep", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "CEP", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.CEP];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.CEP")]
	    public String Cep
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
	    partial void OnCnpjCpfChanging(String value);
	    partial void OnCnpjCpfChanged();

	    private String _CnpjCpf;

	    [DataMember(Name = "CnpjCpf", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "CPF/CNPJ", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[###.###.###-##];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.CNPJ_CPF];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.CNPJ_CPF")]
	    public String CnpjCpf
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
	    partial void OnComplementoChanging(String value);
	    partial void OnComplementoChanged();

	    private String _Complemento;

	    [DataMember(Name = "Complemento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Complemento", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.COMPLEMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.COMPLEMENTO")]
	    public String Complemento
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
	    partial void OnConfirmacaoUsuarioChanging(String value);
	    partial void OnConfirmacaoUsuarioChanged();

	    private String _ConfirmacaoUsuario;

	    [DataMember(Name = "ConfirmacaoUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Senha", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[''];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="\"\"")]
	    public String ConfirmacaoUsuario
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
	    partial void OnConfirmacaoUsuario1Changing(String value);
	    partial void OnConfirmacaoUsuario1Changed();

	    private String _ConfirmacaoUsuario1;

	    [DataMember(Name = "ConfirmacaoUsuario1", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Confirmação", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[''];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="\"\"")]
	    public String ConfirmacaoUsuario1
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
	    partial void OnCriaUsuarioChanging(Boolean value);
	    partial void OnCriaUsuarioChanged();

	    private Boolean _CriaUsuario;

	    [DataMember(IsRequired = true, Name = "CriaUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "CriaUsuario", Description="", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[false];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="false")]
	    public Boolean CriaUsuario
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
	    partial void OnDataAlteracaoChanging(System.Nullable<DateTime> value);
	    partial void OnDataAlteracaoChanged();

	    private System.Nullable<DateTime> _DataAlteracao;

	    [DataMember(Name = "DataAlteracao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Alteração", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[DateTime.Now];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.DATA_ALTERACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.DATA_ALTERACAO")]
	    public System.Nullable<DateTime> DataAlteracao
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
	    partial void OnDataCadastroChanging(System.Nullable<DateTime> value);
	    partial void OnDataCadastroChanged();

	    private System.Nullable<DateTime> _DataCadastro;

	    [DataMember(Name = "DataCadastro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cadastro", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[DateTime.Now];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.DATA_CADASTRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.DATA_CADASTRO")]
	    public System.Nullable<DateTime> DataCadastro
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
	    partial void OnDataExpiracaoSenhaChanging(DateTime value);
	    partial void OnDataExpiracaoSenhaChanged();

	    private DateTime _DataExpiracaoSenha;

	    [DataMember(IsRequired = true, Name = "DataExpiracaoSenha", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Expiração Senha", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[DateTime.Now];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.DATA_EXPIRACAO_SENHA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.DATA_EXPIRACAO_SENHA")]
	    public DateTime DataExpiracaoSenha
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
	    partial void OnEmailChanging(String value);
	    partial void OnEmailChanged();

	    private String _Email;

	    [DataMember(IsRequired = true, Name = "Email", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Email", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.EMAIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.EMAIL")]
	    public String Email
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
	    partial void OnFoneCelularChanging(String value);
	    partial void OnFoneCelularChanged();

	    private String _FoneCelular;

	    [DataMember(Name = "FoneCelular", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Móvel", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.FONE_CELULAR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.FONE_CELULAR")]
	    public String FoneCelular
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
	    partial void OnFoneFixoChanging(String value);
	    partial void OnFoneFixoChanged();

	    private String _FoneFixo;

	    [DataMember(Name = "FoneFixo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Fixo / Ramal", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.FONE_FIXO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.FONE_FIXO")]
	    public String FoneFixo
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
	    partial void OnGeraSenhaUsuarioChanging(Boolean value);
	    partial void OnGeraSenhaUsuarioChanged();

	    private Boolean _GeraSenhaUsuario;

	    [DataMember(IsRequired = true, Name = "GeraSenhaUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "GeraSenhaUsuario", Description="", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[[GERA_SENHA_USUARIO]];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[false];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="false")]
	    public Boolean GeraSenhaUsuario
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
	    [Display(Name = "Id Linx", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioEmpresaAutenticacao];LookUpTitle[Seleção de (Id Linx)];LookUpQuery[executeLookUpTcsUsuarioEmpresaAutenticacao];LookUpFinalize[finalizeLookUpTcsUsuarioEmpresaAutenticacao];LookUpDisplayColumns[{\"IdLinx\" : \"Grupo Econômico\"}];LookUpColumns[{\"IdLinx\" : true}];FilterDataKey[TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
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
	    [Display(Name = "Id Usuario", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.ID_USUARIO];IsMeasure[false]")]
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
	    [Display(Name = "Inativo", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    //Extensibility Partial Method Definitions For IndicaUsuarioServico
	    partial void OnIndicaUsuarioServicoChanging(Boolean value);
	    partial void OnIndicaUsuarioServicoChanged();

	    private Boolean _IndicaUsuarioServico;

	    [DataMember(IsRequired = true, Name = "IndicaUsuarioServico", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário de serviço", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.INDICA_USUARIO_SERVICO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.INDICA_USUARIO_SERVICO")]
	    public Boolean IndicaUsuarioServico
	    {
	    	    get
	    	    {
	    	          return _IndicaUsuarioServico;
	    	    }
	    	    set
	    	    {
	    	          if (this._IndicaUsuarioServico != value)
	    	          {
	    	              this.ValidateProperty("IndicaUsuarioServico", value);
	    	              this.OnIndicaUsuarioServicoChanging(value);
	    	              this.RaiseDataMemberChanging("IndicaUsuarioServico");
	    	              this._IndicaUsuarioServico = value;
	    	              this.RaiseDataMemberChanged("IndicaUsuarioServico");
	    	              this.OnIndicaUsuarioServicoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For InscrEstadualRg
	    partial void OnInscrEstadualRgChanging(String value);
	    partial void OnInscrEstadualRgChanged();

	    private String _InscrEstadualRg;

	    [DataMember(Name = "InscrEstadualRg", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inscr. Estadual / RG", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.INSCR_ESTADUAL_RG];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.INSCR_ESTADUAL_RG")]
	    public String InscrEstadualRg
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
	    partial void OnLogradouroChanging(String value);
	    partial void OnLogradouroChanged();

	    private String _Logradouro;

	    [DataMember(Name = "Logradouro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Logradouro", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.LOGRADOURO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.LOGRADOURO")]
	    public String Logradouro
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
	    partial void OnLxPfjFisicaJuridicaChanging(System.Nullable<Byte> value);
	    partial void OnLxPfjFisicaJuridicaChanged();

	    private System.Nullable<Byte> _LxPfjFisicaJuridica;

	    [DataMember(Name = "LxPfjFisicaJuridica", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Pessoa Física / Juridíca", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[LX_PFJ_FISICA_JURIDICA];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.LX_PFJ_FISICA_JURIDICA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.LX_PFJ_FISICA_JURIDICA")]
	    public System.Nullable<Byte> LxPfjFisicaJuridica
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
	    partial void OnLxTipoLogradouroChanging(System.Nullable<Byte> value);
	    partial void OnLxTipoLogradouroChanged();

	    private System.Nullable<Byte> _LxTipoLogradouro;

	    [DataMember(Name = "LxTipoLogradouro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo Logradouro", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[LxTipoLogradouro];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.LX_TIPO_LOGRADOURO")]
	    public System.Nullable<Byte> LxTipoLogradouro
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
	    partial void OnMunicipioChanging(String value);
	    partial void OnMunicipioChanged();

	    private String _Municipio;

	    [DataMember(Name = "Municipio", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Município", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.MUNICIPIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.MUNICIPIO")]
	    public String Municipio
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
	    partial void OnNomeAutenticacaoChanging(String value);
	    partial void OnNomeAutenticacaoChanged();

	    private String _NomeAutenticacao;

	    [DataMember(IsRequired = true, Name = "NomeAutenticacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário Autenticação", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO")]
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
	    //Extensibility Partial Method Definitions For NomeCurtoUsuario
	    partial void OnNomeCurtoUsuarioChanging(String value);
	    partial void OnNomeCurtoUsuarioChanged();

	    private String _NomeCurtoUsuario;

	    [DataMember(IsRequired = true, Name = "NomeCurtoUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Apelido", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.NOME_CURTO_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.NOME_CURTO_USUARIO")]
	    public String NomeCurtoUsuario
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
	    partial void OnNomeUsuarioChanging(String value);
	    partial void OnNomeUsuarioChanged();

	    private String _NomeUsuario;

	    [DataMember(IsRequired = true, Name = "NomeUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.NOME_USUARIO")]
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
	    //Extensibility Partial Method Definitions For Numero
	    partial void OnNumeroChanging(String value);
	    partial void OnNumeroChanged();

	    private String _Numero;

	    [DataMember(Name = "Numero", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Número", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[Logradouro];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.NUMERO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.NUMERO")]
	    public String Numero
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
	    partial void OnObsEnderecoChanging(String value);
	    partial void OnObsEnderecoChanged();

	    private String _ObsEndereco;

	    [DataMember(Name = "ObsEndereco", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Obs. Endereço", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.OBS_ENDERECO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.OBS_ENDERECO")]
	    public String ObsEndereco
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
	    partial void OnRamalChanging(String value);
	    partial void OnRamalChanged();

	    private String _Ramal;

	    [DataMember(Name = "Ramal", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ramal", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(6)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[FoneFixo];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.RAMAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.RAMAL")]
	    public String Ramal
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
	    partial void OnUfChanging(String value);
	    partial void OnUfChanged();

	    private String _Uf;

	    [DataMember(Name = "Uf", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "UF", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(4)]
	    [FunctionalPoint("Precision[4:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[Municipio];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.UF];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.UF")]
	    public String Uf
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
	    partial void OnUidUsuarioChanging(Guid value);
	    partial void OnUidUsuarioChanged();

	    private Guid _UidUsuario;

	    [DataMember(Name = "UidUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Usuario", Description="", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.UID_USUARIO];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For VigenciaFinal
	    partial void OnVigenciaFinalChanging(DateTime value);
	    partial void OnVigenciaFinalChanged();

	    private DateTime _VigenciaFinal;

	    [DataMember(IsRequired = true, Name = "VigenciaFinal", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Vigência Final", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[new DateTime(2099, 12, 31)];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.VIGENCIA_FINAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.VIGENCIA_FINAL")]
	    public DateTime VigenciaFinal
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
	    partial void OnVigenciaInicialChanging(DateTime value);
	    partial void OnVigenciaInicialChanged();

	    private DateTime _VigenciaInicial;

	    [DataMember(IsRequired = true, Name = "VigenciaInicial", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Vigência Inicial", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[DateTime.Now];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.VIGENCIA_INICIAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.VIGENCIA_INICIAL")]
	    public DateTime VigenciaInicial
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
	    [Display(Name = "Id Usuario (Tmp)", Description="Temporary Key", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
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
	 
		
	    private IEnumerable<TcsUsuarioAutenticacaoAcesso> _TcsUsuarioAutenticacaoAcessoList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsUsuarioAutenticacao_TcsUsuarioAutenticacaoAcesso", "IdUsuario", "IdUsuario", IsForeignKey=false)]
	    [DataMember(Name = "TcsUsuarioAutenticacaoAcessoList", EmitDefaultValue = true)]
	    public IEnumerable<TcsUsuarioAutenticacaoAcesso> TcsUsuarioAutenticacaoAcessoList
	    {
	        get
	        {
	
	            if (this._TcsUsuarioAutenticacaoAcessoList == null)
	            	this._TcsUsuarioAutenticacaoAcessoList = new List<TcsUsuarioAutenticacaoAcesso>();
	
	            return this._TcsUsuarioAutenticacaoAcessoList;
	        }
	        set
	        {
	            if (this._TcsUsuarioAutenticacaoAcessoList != value)
	            {
	                this._TcsUsuarioAutenticacaoAcessoList = value;
	                this.RaisePropertyChanged("TcsUsuarioAutenticacaoAcessoList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		
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

		

	[LinxPublicationView(PrimaryKeys="TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO", IsUpdatable=false, EdmName="")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Acessos];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsUsuarioAcesso];ReadOnly[false];Entities[:IdTcsUsuarioAcesso];SubQueryInfo[Select 1 From #ParentAlias#. as #Alias#];EdmEntityName[];EntityRelations[ ];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioAutenticacaoAcesso")]
	[Serializable()]
	public partial class TcsUsuarioAutenticacaoAcesso : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(UsuarioFranquiaDomainService context)
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
	    partial void OnDescricaoAmbienteChanging(String value);
	    partial void OnDescricaoAmbienteChanged();

	    private String _DescricaoAmbiente;

	    [DataMember(IsRequired = true, Name = "DescricaoAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ambiente", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente2];LookUpTitle[Seleção de (Ambiente)];LookUpQuery[executeLookUpTcsAmbiente2];LookUpFinalize[finalizeLookUpTcsAmbiente2];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"DescricaoAplicativo\" : \"Aplicativo\", \"NomeEmpresa\" : \"Empresa\", \"DescricaoAplicacao\" : \"Aplicação\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\", \"IdAplicacao\" : \"Id Aplicacao\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\", \"IdLinx\" : \"Id Linx\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"DescricaoAplicativo\" : true, \"NomeEmpresa\" : true, \"DescricaoAplicacao\" : false, \"IdTcsAmbiente\" : false, \"IdAplicacao\" : false, \"IdTcsAplicativo\" : false, \"IdLinx\" : true}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.DESCRICAO_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoAmbiente#false##2500##Ambiente#0#true##::LookUpTcsAmbiente2##true#false###Linx.Framework.BV.UsuarioAutorizacao#IQueryable###true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.DESCRICAO_AMBIENTE")]
	    public String DescricaoAmbiente
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
	    partial void OnDescricaoAmbienteRelacionadoChanging(String value);
	    partial void OnDescricaoAmbienteRelacionadoChanged();

	    private String _DescricaoAmbienteRelacionado;

	    [DataMember(Name = "DescricaoAmbienteRelacionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ambiente Relacionado", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente2Relacionado];LookUpTitle[Seleção de (Ambiente Relacionado)];LookUpQuery[executeLookUpTcsAmbiente2Relacionado];LookUpFinalize[finalizeLookUpTcsAmbiente2Relacionado];LookUpDisplayColumns[{\"DescricaoAmbienteRelacionado\" : \"Ambiente\", \"IdTcsAmbienteRelacionado\" : \"Id Tcs Ambiente\"}];LookUpColumns[{\"DescricaoAmbienteRelacionado\" : true, \"IdTcsAmbienteRelacionado\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE1.DESCRICAO_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoAmbienteRelacionado#false##2500##Ambiente#0#true##::LookUpTcsAmbiente2Relacionado##false#false###Linx.Framework.BV.UsuarioAutorizacao#IQueryable###true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.DESCRICAO_AMBIENTE")]
	    public String DescricaoAmbienteRelacionado
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
	    partial void OnDescricaoAplicacaoChanging(String value);
	    partial void OnDescricaoAplicacaoChanged();

	    private String _DescricaoAplicacao;

	    [DataMember(IsRequired = true, Name = "DescricaoAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descricao Aplicacao", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente2];LookUpTitle[Seleção de (Descricao Aplicacao)];LookUpQuery[executeLookUpTcsAmbiente2];LookUpFinalize[finalizeLookUpTcsAmbiente2];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"DescricaoAplicativo\" : \"Aplicativo\", \"NomeEmpresa\" : \"Empresa\", \"DescricaoAplicacao\" : \"Aplicação\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\", \"IdAplicacao\" : \"Id Aplicacao\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\", \"IdLinx\" : \"Id Linx\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"DescricaoAplicativo\" : true, \"NomeEmpresa\" : true, \"DescricaoAplicacao\" : false, \"IdTcsAmbiente\" : false, \"IdAplicacao\" : false, \"IdTcsAplicativo\" : false, \"IdLinx\" : true}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.DESCRICAO_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoAplicacao#false##600##Aplicação#3#false##::LookUpTcsAmbiente2##true#false###Linx.Framework.BV.UsuarioAutorizacao#IQueryable###true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.DESCRICAO_APLICACAO")]
	    public String DescricaoAplicacao
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
	    partial void OnDescricaoAplicativoChanging(String value);
	    partial void OnDescricaoAplicativoChanged();

	    private String _DescricaoAplicativo;

	    [DataMember(Name = "DescricaoAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicativo", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente2];LookUpTitle[Seleção de (Aplicativo)];LookUpQuery[executeLookUpTcsAmbiente2];LookUpFinalize[finalizeLookUpTcsAmbiente2];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"DescricaoAplicativo\" : \"Aplicativo\", \"NomeEmpresa\" : \"Empresa\", \"DescricaoAplicacao\" : \"Aplicação\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\", \"IdAplicacao\" : \"Id Aplicacao\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\", \"IdLinx\" : \"Id Linx\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"DescricaoAplicativo\" : true, \"NomeEmpresa\" : true, \"DescricaoAplicacao\" : false, \"IdTcsAmbiente\" : false, \"IdAplicacao\" : false, \"IdTcsAplicativo\" : false, \"IdLinx\" : true}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescricaoAplicativo#false##2500##Aplicativo#1#true##::LookUpTcsAmbiente2##true#false###Linx.Framework.BV.UsuarioAutorizacao#IQueryable###true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO")]
	    public String DescricaoAplicativo
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
	    [Display(Name = "Id Aplicacao", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    [Display(Name = "Id Linx", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente2];LookUpTitle[Seleção de (Id Linx)];LookUpQuery[executeLookUpTcsAmbiente2];LookUpFinalize[finalizeLookUpTcsAmbiente2];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"DescricaoAplicativo\" : \"Aplicativo\", \"NomeEmpresa\" : \"Empresa\", \"DescricaoAplicacao\" : \"Aplicação\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\", \"IdAplicacao\" : \"Id Aplicacao\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\", \"IdLinx\" : \"Id Linx\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"DescricaoAplicativo\" : true, \"NomeEmpresa\" : true, \"DescricaoAplicacao\" : false, \"IdTcsAmbiente\" : false, \"IdAplicacao\" : false, \"IdTcsAplicativo\" : false, \"IdLinx\" : true}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
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
	    [Display(Name = "Id Tcs Ambiente", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    [Display(Name = "Ambiente Relacionado", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    [Display(Name = "Id Tcs Aplicativo", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    [Display(Name = "Id Tcs Usuario Acesso", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    [Display(Name = "Id Usuario", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    [Display(Name = "Acesso Padrão", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    //Extensibility Partial Method Definitions For NomeAutenticacao
	    partial void OnNomeAutenticacaoChanging(String value);
	    partial void OnNomeAutenticacaoChanged();

	    private String _NomeAutenticacao;

	    [DataMember(IsRequired = true, Name = "NomeAutenticacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário Autenticação", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    //Extensibility Partial Method Definitions For NomeEmpresa
	    partial void OnNomeEmpresaChanging(String value);
	    partial void OnNomeEmpresaChanged();

	    private String _NomeEmpresa;

	    [DataMember(IsRequired = true, Name = "NomeEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Empresa", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente2];LookUpTitle[Seleção de (Empresa)];LookUpQuery[executeLookUpTcsAmbiente2];LookUpFinalize[finalizeLookUpTcsAmbiente2];LookUpDisplayColumns[{\"DescricaoAmbiente\" : \"Ambiente\", \"DescricaoAplicativo\" : \"Aplicativo\", \"NomeEmpresa\" : \"Empresa\", \"DescricaoAplicacao\" : \"Aplicação\", \"IdTcsAmbiente\" : \"Id Tcs Ambiente\", \"IdAplicacao\" : \"Id Aplicacao\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\", \"IdLinx\" : \"Id Linx\"}];LookUpColumns[{\"DescricaoAmbiente\" : true, \"DescricaoAplicativo\" : true, \"NomeEmpresa\" : true, \"DescricaoAplicacao\" : false, \"IdTcsAmbiente\" : false, \"IdAplicacao\" : false, \"IdTcsAplicativo\" : false, \"IdLinx\" : true}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeEmpresa#false##2500##Empresa#2#true##::LookUpTcsAmbiente2##true#false###Linx.Framework.BV.UsuarioAutorizacao#IQueryable###true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
	    public String NomeEmpresa
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
	    partial void OnNomeUsuarioChanging(String value);
	    partial void OnNomeUsuarioChanged();

	    private String _NomeUsuario;

	    [DataMember(IsRequired = true, Name = "NomeUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    //Extensibility Partial Method Definitions For Perfil
	    partial void OnPerfilChanging(String value);
	    partial void OnPerfilChanged();

	    private String _Perfil;

	    [DataMember(Name = "Perfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[''];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="\"\"")]
	    public String Perfil
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

	    private Int32 _TemporaryIdTcsUsuarioAcesso;
	    [DataMember(Name = "TemporaryIdTcsUsuarioAcesso", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Acesso (Tmp)", Description="Temporary Key", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
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
	    [Association("FK_TcsUsuarioAutenticacao_TcsUsuarioAutenticacaoAcesso", "IdUsuario", "IdUsuario", IsForeignKey=true)]
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

		

	[LinxPublicationView(PrimaryKeys="TCS_EMPRESA_AUTENTICACAO.ID_LINX,TCS_USUARIO_PERFIL.ID_TCS_USUARIO_PERFIL", IsUpdatable=false, EdmName="")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Perfil];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsUsuarioAutenticacaoPerfil];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsUsuarioPerfil];ReadOnly[false];Entities[:IdTcsUsuarioPerfil];SubQueryInfo[];EdmEntityName[];EntityRelations[ ];EdmParentEntityName[];IsIQueryable[false]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioAutenticacaoPerfil")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.UsuarioFranquia.TcsUsuarioAutenticacaoPerfil")]
	public partial class TcsUsuarioAutenticacaoPerfil : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For DescPerfil
	    partial void OnDescPerfilChanging(String value);
	    partial void OnDescPerfilChanged();

	    private String _DescPerfil;

	    [DataMember(IsRequired = true, Name = "DescPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Perfil", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsPerfil];LookUpTitle[Seleção de (Perfil)];LookUpQuery[executeLookUpTcsPerfil];LookUpFinalize[finalizeLookUpTcsPerfil];LookUpDisplayColumns[{\"DescPerfil\" : \"Perfil\", \"IdPerfil\" : \"Id Perfil\", \"IdLinxPerfil\" : \"\"}];LookUpColumns[{\"DescPerfil\" : true, \"IdPerfil\" : false, \"IdLinxPerfil\" : false}];FilterDataKey[TCS_USUARIO_PERFIL.TCS_PERFIL.DESC_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescPerfil#false##60:0##Perfil#0#true##::LookUpTcsPerfil##false#false#TCS_PERFIL#TCS_PERFIL#Linx.Framework.BV.UsuarioFranquia#IQueryable###true#false", EdmKey="TCS_USUARIO_PERFIL.TCS_PERFIL.DESC_PERFIL")]
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
	    //Extensibility Partial Method Definitions For IdLinx
	    partial void OnIdLinxChanging(Int32 value);
	    partial void OnIdLinxChanged();

	    private Int32 _IdLinx;

	    [DataMember(IsRequired = true, Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsEmpresaAutenticacaoP];LookUpTitle[Seleção de (Id Linx)];LookUpQuery[executeLookUpTcsEmpresaAutenticacaoP];LookUpFinalize[finalizeLookUpTcsEmpresaAutenticacaoP];LookUpDisplayColumns[{\"IdLinx\" : \"Id Linx\", \"NomeEmpresa\" : \"Empresa\", \"UidEmpresa\" : \"Uid Empresa\"}];LookUpColumns[{\"IdLinx\" : false, \"NomeEmpresa\" : true, \"UidEmpresa\" : false}];FilterDataKey[TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdLinx#true##12:0##Id Linx#0#false##::LookUpTcsEmpresaAutenticacaoP##true#false##TCS_EMPRESA_AUTENTICACAO#Linx.Framework.BV.Empresa#IQueryable###true#false", EdmKey="TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
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
	    //Extensibility Partial Method Definitions For IdLinxPerfil
	    partial void OnIdLinxPerfilChanging(Int32 value);
	    partial void OnIdLinxPerfilChanged();

	    private Int32 _IdLinxPerfil;

	    [DataMember(IsRequired = true, Name = "IdLinxPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "IdLinxPerfil", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsPerfil];LookUpTitle[Seleção de (IdLinxPerfil)];LookUpQuery[executeLookUpTcsPerfil];LookUpFinalize[finalizeLookUpTcsPerfil];LookUpDisplayColumns[{\"DescPerfil\" : \"Perfil\", \"IdPerfil\" : \"Id Perfil\", \"IdLinxPerfil\" : \"\"}];LookUpColumns[{\"DescPerfil\" : true, \"IdPerfil\" : false, \"IdLinxPerfil\" : false}];FilterDataKey[TCS_USUARIO_PERFIL.TCS_PERFIL.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="int#IdLinxPerfil#false##0###3#false##::LookUpTcsPerfil##false#false#TCS_PERFIL#TCS_PERFIL#Linx.Framework.BV.UsuarioFranquia#IQueryable###true#false", EdmKey="TCS_USUARIO_PERFIL.TCS_PERFIL.ID_LINX")]
	    public Int32 IdLinxPerfil
	    {
	    	    get
	    	    {
	    	          return _IdLinxPerfil;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLinxPerfil != value)
	    	          {
	    	              this.ValidateProperty("IdLinxPerfil", value);
	    	              this.OnIdLinxPerfilChanging(value);
	    	              this.RaiseDataMemberChanging("IdLinxPerfil");
	    	              this._IdLinxPerfil = value;
	    	              this.RaiseDataMemberChanged("IdLinxPerfil");
	    	              this.OnIdLinxPerfilChanged();
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
	    [Display(Name = "Id Perfil", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsPerfil];LookUpTitle[Seleção de (Id Perfil)];LookUpQuery[executeLookUpTcsPerfil];LookUpFinalize[finalizeLookUpTcsPerfil];LookUpDisplayColumns[{\"DescPerfil\" : \"Perfil\", \"IdPerfil\" : \"Id Perfil\", \"IdLinxPerfil\" : \"\"}];LookUpColumns[{\"DescPerfil\" : true, \"IdPerfil\" : false, \"IdLinxPerfil\" : false}];FilterDataKey[TCS_USUARIO_PERFIL.TCS_PERFIL.ID_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdPerfil#true##24:0##Id Perfil#1#false##::LookUpTcsPerfil##false#false#TCS_PERFIL#TCS_PERFIL#Linx.Framework.BV.UsuarioFranquia#IQueryable###true#false", EdmKey="TCS_USUARIO_PERFIL.TCS_PERFIL.ID_PERFIL")]
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
	    //Extensibility Partial Method Definitions For IdTcsUsuarioPerfil
	    partial void OnIdTcsUsuarioPerfilChanging(Int64 value);
	    partial void OnIdTcsUsuarioPerfilChanged();

	    private Int64 _IdTcsUsuarioPerfil;

	    [DataMember(IsRequired = true, Name = "IdTcsUsuarioPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Perfil", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_PERFIL.ID_TCS_USUARIO_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_PERFIL.ID_TCS_USUARIO_PERFIL")]
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
	    [Display(Name = "Id Usuario", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_PERFIL.TCS_USUARIO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_PERFIL.TCS_USUARIO.ID_USUARIO")]
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
	    //Extensibility Partial Method Definitions For NomeEmpresa
	    partial void OnNomeEmpresaChanging(String value);
	    partial void OnNomeEmpresaChanged();

	    private String _NomeEmpresa;

	    [DataMember(Name = "NomeEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Empresa", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsEmpresaAutenticacaoP];LookUpTitle[Seleção de (Empresa)];LookUpQuery[executeLookUpTcsEmpresaAutenticacaoP];LookUpFinalize[finalizeLookUpTcsEmpresaAutenticacaoP];LookUpDisplayColumns[{\"IdLinx\" : \"Id Linx\", \"NomeEmpresa\" : \"Empresa\", \"UidEmpresa\" : \"Uid Empresa\"}];LookUpColumns[{\"IdLinx\" : false, \"NomeEmpresa\" : true, \"UidEmpresa\" : false}];FilterDataKey[TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeEmpresa#false##2500##Empresa#1#true##::LookUpTcsEmpresaAutenticacaoP##true#false##TCS_EMPRESA_AUTENTICACAO#Linx.Framework.BV.Empresa#IQueryable###true#false", EdmKey="TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
	    public String NomeEmpresa
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
	    partial void OnNomeUsuarioChanging(String value);
	    partial void OnNomeUsuarioChanged();

	    private String _NomeUsuario;

	    [DataMember(Name = "NomeUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_PERFIL.TCS_USUARIO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_PERFIL.TCS_USUARIO.NOME_USUARIO")]
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
	    //Extensibility Partial Method Definitions For UidEmpresa
	    partial void OnUidEmpresaChanging(Guid value);
	    partial void OnUidEmpresaChanged();

	    private Guid _UidEmpresa;

	    [DataMember(Name = "UidEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Empresa", Description="", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsEmpresaAutenticacaoP];LookUpTitle[Seleção de (Uid Empresa)];LookUpQuery[executeLookUpTcsEmpresaAutenticacaoP];LookUpFinalize[finalizeLookUpTcsEmpresaAutenticacaoP];LookUpDisplayColumns[{\"IdLinx\" : \"Id Linx\", \"NomeEmpresa\" : \"Empresa\", \"UidEmpresa\" : \"Uid Empresa\"}];LookUpColumns[{\"IdLinx\" : false, \"NomeEmpresa\" : true, \"UidEmpresa\" : false}];FilterDataKey[TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Guid#UidEmpresa#false##36:0##Uid Empresa#2#false##::LookUpTcsEmpresaAutenticacaoP##true#false##TCS_EMPRESA_AUTENTICACAO#Linx.Framework.BV.Empresa#IQueryable###true#false", EdmKey="TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA")]
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

	    private Int64 _TemporaryIdTcsUsuarioPerfil;
	    [DataMember(Name = "TemporaryIdTcsUsuarioPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Perfil (Tmp)", Description="Temporary Key", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
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

		

	[LinxPublicationView(PrimaryKeys="TCS_USUARIO_PERFIL.ID_TCS_USUARIO_PERFIL", IsUpdatable=true, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsUsuarioPerfil];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsUsuarioPerfil];ReadOnly[false];Entities[TCS_USUARIO_PERFIL:IdTcsUsuarioPerfil];SubQueryInfo[];EdmEntityName[TCS_USUARIO_PERFIL];EntityRelations[TCS_PERFIL(TCS_PERFIL)#TCS_USUARIO(TCS_USUARIO)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioPerfil")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.UsuarioFranquia.TcsUsuarioPerfil")]
	public partial class TcsUsuarioPerfil : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For DescPerfil
	    partial void OnDescPerfilChanging(System.String value);
	    partial void OnDescPerfilChanged();

	    private System.String _DescPerfil;

	    [DataMember(IsRequired = true, Name = "DescPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Desc Perfil", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsPerfil];LookUpTitle[Seleção de (Desc Perfil)];LookUpQuery[executeLookUpTcsPerfil];LookUpFinalize[finalizeLookUpTcsPerfil];LookUpDisplayColumns[{\"DescPerfil\" : \"Perfil\", \"IdPerfil\" : \"Id Perfil\", \"Inativo\" : \"Inativo\", \"IdLinxPerfil\" : \"\"}];LookUpColumns[{\"DescPerfil\" : true, \"IdPerfil\" : false, \"Inativo\" : false, \"IdLinxPerfil\" : false}];FilterDataKey[TCS_USUARIO_PERFIL.TCS_PERFIL.DESC_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescPerfil#false##60:0##Perfil#0#true##::LookUpTcsPerfil##false#false#TCS_PERFIL#TCS_PERFIL#Linx.Framework.BV.UsuarioFranquia#IQueryable###true#false", EdmKey="TCS_USUARIO_PERFIL.TCS_PERFIL.DESC_PERFIL")]
	    public System.String DescPerfil
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
	    //Extensibility Partial Method Definitions For IdLinxPerfil
	    partial void OnIdLinxPerfilChanging(int value);
	    partial void OnIdLinxPerfilChanged();

	    private int _IdLinxPerfil;

	    [DataMember(IsRequired = true, Name = "IdLinxPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];LookUpName[LookUpTcsPerfil];LookUpTitle[Seleção de ()];LookUpQuery[executeLookUpTcsPerfil];LookUpFinalize[finalizeLookUpTcsPerfil];LookUpDisplayColumns[{\"DescPerfil\" : \"Perfil\", \"IdPerfil\" : \"Id Perfil\", \"Inativo\" : \"Inativo\", \"IdLinxPerfil\" : \"\"}];LookUpColumns[{\"DescPerfil\" : true, \"IdPerfil\" : false, \"Inativo\" : false, \"IdLinxPerfil\" : false}];FilterDataKey[TCS_USUARIO_PERFIL.TCS_PERFIL.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="int#IdLinxPerfil#false##0###3#false##::LookUpTcsPerfil##false#false#TCS_PERFIL#TCS_PERFIL#Linx.Framework.BV.UsuarioFranquia#IQueryable###true#false", EdmKey="TCS_USUARIO_PERFIL.TCS_PERFIL.ID_LINX")]
	    public int IdLinxPerfil
	    {
	    	    get
	    	    {
	    	          return _IdLinxPerfil;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLinxPerfil != value)
	    	          {
	    	              this.ValidateProperty("IdLinxPerfil", value);
	    	              this.OnIdLinxPerfilChanging(value);
	    	              this.RaiseDataMemberChanging("IdLinxPerfil");
	    	              this._IdLinxPerfil = value;
	    	              this.RaiseDataMemberChanged("IdLinxPerfil");
	    	              this.OnIdLinxPerfilChanged();
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
	    [Display(Name = "Id Perfil", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsPerfil];LookUpTitle[Seleção de (Id Perfil)];LookUpQuery[executeLookUpTcsPerfil];LookUpFinalize[finalizeLookUpTcsPerfil];LookUpDisplayColumns[{\"DescPerfil\" : \"Perfil\", \"IdPerfil\" : \"Id Perfil\", \"Inativo\" : \"Inativo\", \"IdLinxPerfil\" : \"\"}];LookUpColumns[{\"DescPerfil\" : true, \"IdPerfil\" : false, \"Inativo\" : false, \"IdLinxPerfil\" : false}];FilterDataKey[TCS_USUARIO_PERFIL.TCS_PERFIL.ID_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdPerfil#true##24:0##Id Perfil#1#false##::LookUpTcsPerfil##false#false#TCS_PERFIL#TCS_PERFIL#Linx.Framework.BV.UsuarioFranquia#IQueryable###true#false", EdmKey="TCS_USUARIO_PERFIL.TCS_PERFIL.ID_PERFIL")]
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
	    //Extensibility Partial Method Definitions For IdTcsUsuarioPerfil
	    partial void OnIdTcsUsuarioPerfilChanging(Int64 value);
	    partial void OnIdTcsUsuarioPerfilChanged();

	    private Int64 _IdTcsUsuarioPerfil;

	    [DataMember(IsRequired = true, Name = "IdTcsUsuarioPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Perfil", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_PERFIL.ID_TCS_USUARIO_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_PERFIL.ID_TCS_USUARIO_PERFIL")]
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
	    [Display(Name = "Id Usuario", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_PERFIL.TCS_USUARIO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_PERFIL.TCS_USUARIO.ID_USUARIO")]
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
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsPerfil];LookUpTitle[Seleção de (Inativo)];LookUpQuery[executeLookUpTcsPerfil];LookUpFinalize[finalizeLookUpTcsPerfil];LookUpDisplayColumns[{\"DescPerfil\" : \"Perfil\", \"IdPerfil\" : \"Id Perfil\", \"Inativo\" : \"Inativo\", \"IdLinxPerfil\" : \"\"}];LookUpColumns[{\"DescPerfil\" : true, \"IdPerfil\" : false, \"Inativo\" : false, \"IdLinxPerfil\" : false}];FilterDataKey[TCS_USUARIO_PERFIL.TCS_PERFIL.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Boolean#Inativo#false##0:0##Inativo#2#false##::LookUpTcsPerfil##false#false#TCS_PERFIL#TCS_PERFIL#Linx.Framework.BV.UsuarioFranquia#IQueryable###true#false", EdmKey="TCS_USUARIO_PERFIL.TCS_PERFIL.INATIVO")]
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
	    //Extensibility Partial Method Definitions For NomeUsuario
	    partial void OnNomeUsuarioChanging(System.String value);
	    partial void OnNomeUsuarioChanged();

	    private System.String _NomeUsuario;

	    [DataMember(IsRequired = true, Name = "NomeUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Usuario", Description="", Order = 17, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_PERFIL.TCS_USUARIO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_PERFIL.TCS_USUARIO.NOME_USUARIO")]
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

	    private Int64 _TemporaryIdTcsUsuarioPerfil;
	    [DataMember(Name = "TemporaryIdTcsUsuarioPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Perfil (Tmp)", Description="Temporary Key", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
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

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_USUARIO_PERFIL").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = true, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_USUARIO_PERFIL), QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_PERFIL" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_PERFIL.TCS_PERFIL.ID_PERFIL", Source = "IdPerfil", Target = "ID_PERFIL", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_PERFIL", RelationPropertyName = "TCS_PERFIL" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_PERFIL.ID_TCS_USUARIO_PERFIL", Source = "IdTcsUsuarioPerfil", Target = "ID_TCS_USUARIO_PERFIL", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_PERFIL", RelationPropertyName = "TCS_USUARIO_PERFIL" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_PERFIL.TCS_USUARIO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });

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

		

	[LinxPublicationView(PrimaryKeys="UsuarioPerfilInfo.EntityUniqueKey", IsUpdatable=false, EdmName="")]
		
	[DataContract(IsReference = false, Name = "UsuarioPerfilInfo")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.UsuarioFranquia.UsuarioPerfilInfo")]
	public partial class UsuarioPerfilInfo 
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
	 


	    private Int64 _IdUsuario;

	    [DataMember(Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public Int64 IdUsuario
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

	    private int _IdLinx;

	    [DataMember(Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
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

	    private List<TcsUsuarioPerfil> _PerfilList;

	    [DataMember(Name = "PerfilList", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public List<TcsUsuarioPerfil> PerfilList
	    {
	    	    get
	    	    {
	    	          return _PerfilList;
	    	    }
	    	    set
	    	    {
	    	          this._PerfilList = value;
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
	//////////////////////// DomainService Class V1 ///////////////////////
	///////////////////////////////////////////////////////////////////////
	[EnableClientAccess()]	
	[DomainIdentifier("ProcessorOverviewUsuarioFranquiaDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class UsuarioFranquiaDomainService : DomainService, IDataServiceContext 
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

		
	    public UsuarioFranquiaDomainService() : this("", null, null) { }
	    public UsuarioFranquiaDomainService(string connectionString) : this(connectionString, null, null) { }
	    public UsuarioFranquiaDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public UsuarioFranquiaDomainService(Linx.Framework.ControleSistema.BM.ControleSistemaContext dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public UsuarioFranquiaDomainService(string connectionString, Linx.Framework.ControleSistema.BM.ControleSistemaContext dataContext, Dictionary<string, string> headers) : base() 
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
	
 	        if (changedEntity is TcsUsuarioAutenticacao)
 	        {
 	          List<EntityChange> entityChanges = this.GetRepresentations((TcsUsuarioAutenticacao)changedEntity, originalEntity as TcsUsuarioAutenticacao, operation);
 	          Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService serviceContext1 = new Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService(this.Headers) { IsSecure = this.IsSecure };
 	          foreach (var entityChange in entityChanges.Where(e => e.Mark == "serviceContext1").ToList())
 	          {
 	                serviceContext1.AddCustomChanges(entityChange.Entity, entityChange.Original, operation);
 	                serviceContext1.SaveCustomChanges();
 	                if (operation == ChangeOperation.Insert) entityChange.RefreshKeys();
 	          }
 	        }
 	        else if (changedEntity is TcsUsuarioAutenticacaoAcesso)
 	        {
 	          List<EntityChange> entityChanges = this.GetRepresentations((TcsUsuarioAutenticacaoAcesso)changedEntity, originalEntity as TcsUsuarioAutenticacaoAcesso, operation);
 	          Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService serviceContext1 = new Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService(this.Headers) { IsSecure = this.IsSecure };
 	          foreach (var entityChange in entityChanges.Where(e => e.Mark == "serviceContext1").ToList())
 	          {
 	                serviceContext1.AddCustomChanges(entityChange.Entity, entityChange.Original, operation);
 	                serviceContext1.SaveCustomChanges();
 	                if (operation == ChangeOperation.Insert) entityChange.RefreshKeys();
 	          }
 	        }
 	        else if (changedEntity is TcsUsuarioAutenticacaoPerfil)
 	        {
 	          List<EntityChange> entityChanges = this.GetRepresentations((TcsUsuarioAutenticacaoPerfil)changedEntity, originalEntity as TcsUsuarioAutenticacaoPerfil, operation);
 	          Linx.Framework.BV.UsuarioFranquia.UsuarioFranquiaDomainService serviceContext2 = new Linx.Framework.BV.UsuarioFranquia.UsuarioFranquiaDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
 	          foreach (var entityChange in entityChanges.Where(e => e.Mark == "serviceContext2").ToList())
 	          {
 	                serviceContext2.AddCustomChanges(entityChange.Entity, entityChange.Original, operation);
 	                serviceContext2.SaveCustomChanges();
 	                if (operation == ChangeOperation.Insert) entityChange.RefreshKeys();
 	          }
 	        }
 	        else changedEntity.ApplyChanges(this.DbContext, originalEntity, operation, null);
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
 	        var _TcsUsuarioAutenticacaoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioAutenticacao && e.Entity.GetType().Name == "TcsUsuarioAutenticacao" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _TcsUsuarioAutenticacaoElements)
 	           if (((TcsUsuarioAutenticacao)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 
 	        //Remove inconsistent details
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioAutenticacaoAcesso && e.Entity.GetType().Name == "TcsUsuarioAutenticacaoAcesso" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
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
	                SaveAllRepresentations();

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
		

	    #region Save Representations.

	    //Replace detail keys
	    [Ignore]
	    private void ReplaceDetailsByParent(List<EntityChange> entityChanges, Entity parent)
	    {
		
 	        if (parent is TcsUsuarioAutenticacao)
 	        {
 	          foreach (TcsUsuarioAutenticacaoAcesso entity in ((TcsUsuarioAutenticacao)parent).TcsUsuarioAutenticacaoAcessoList)
 	          {
 	              entity.IdUsuario = ((TcsUsuarioAutenticacao)parent).IdUsuario;
 	              var entityEntry = entityChanges.FirstOrDefault(e => e.Representation == entity);
 	              if (entityEntry != null)
 	                  entityEntry.Entity.SetPropertyValue("IdUsuario", entity.IdUsuario);
 	          }
 	        }	
	    }

	    //Save all entity representations
	    [Ignore]
	    private void SaveAllRepresentations()
	    {
	        List<EntityChange> entityChanges = new List<EntityChange>();
				
	        SaveBufferRepresentationsOfTcsUsuarioAutenticacao(entityChanges);		
				
	        SaveBufferRepresentationsOfTcsUsuarioAutenticacaoAcesso(entityChanges);		
				
	        SaveBufferRepresentationsOfTcsUsuarioAutenticacaoPerfil(entityChanges);		
		
	        if (entityChanges.Count == 0) return;
		
 
 	        //Submitting all data changes
 	        Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService serviceContext1 = new Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService(this.Headers) { IsSecure = this.IsSecure };
 	        var serviceContext1Changes = entityChanges.Where(e => e.Mark == "serviceContext1").ToList();
 	        serviceContext1.SubmitData(this.ServiceContext, serviceContext1Changes);
 	        //Replace keys from source
 	        foreach (var entityChange in serviceContext1Changes) { entityChange.RefreshKeys(); this.ReplaceDetailsByParent(entityChanges, entityChange.Representation); }
 	        Linx.Framework.BV.UsuarioFranquia.UsuarioFranquiaDomainService serviceContext2 = new Linx.Framework.BV.UsuarioFranquia.UsuarioFranquiaDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
 	        var serviceContext2Changes = entityChanges.Where(e => e.Mark == "serviceContext2").ToList();
 	        serviceContext2.SubmitData(this.ServiceContext, serviceContext2Changes);
 	        //Replace keys from source
 	        foreach (var entityChange in serviceContext2Changes) { entityChange.RefreshKeys(); this.ReplaceDetailsByParent(entityChanges, entityChange.Representation); }	

	    }

			
	  
 	    //Save All Representations Of Entity TcsUsuarioAutenticacao
 	    [Ignore]
 	    private void SaveBufferRepresentationsOfTcsUsuarioAutenticacao(List<EntityChange> entityChanges)
 	    {
 	      foreach (ChangeSetEntry entry in this.ChangeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioAutenticacao && e.Entity.GetType().Name == "TcsUsuarioAutenticacao"))
 	      {
 	          TcsUsuarioAutenticacao entity = (TcsUsuarioAutenticacao)entry.Entity;
 	          entityChanges.AddRange(this.GetRepresentations(entity, (this.ChangeSet.GetChangeOperation(entity) == ChangeOperation.Update ? this.ChangeSet.GetOriginal(entity) : null), this.ChangeSet.GetChangeOperation(entity)));
 	      }
 	    }
 
 	    //Get Representation Of TcsUsuarioAutenticacao
 	    [Ignore]
 	    private List<EntityChange> GetRepresentations(TcsUsuarioAutenticacao entity, TcsUsuarioAutenticacao original, ChangeOperation operation)
 	    {
 	          List<EntityChange> result = new List<EntityChange>();
 	          switch (operation)
 	          {
 	              case ChangeOperation.None:
 	                  //None TcsUsuarioAutenticacao
 	                  Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacao noneTcsUsuarioAutenticacao = new Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacao() {
 	                  AutenticacaoWindows = entity.AutenticacaoWindows,
 	                  Bairro = entity.Bairro,
 	                  Cep = entity.Cep,
 	                  CnpjCpf = entity.CnpjCpf,
 	                  Complemento = entity.Complemento,
 	                  ConfirmacaoUsuario = entity.ConfirmacaoUsuario,
 	                  ConfirmacaoUsuario1 = entity.ConfirmacaoUsuario1,
 	                  CriaUsuario = entity.CriaUsuario,
 	                  DataAlteracao = entity.DataAlteracao,
 	                  DataCadastro = entity.DataCadastro,
 	                  DataExpiracaoSenha = entity.DataExpiracaoSenha,
 	                  Email = entity.Email,
 	                  FoneCelular = entity.FoneCelular,
 	                  FoneFixo = entity.FoneFixo,
 	                  GeraSenhaUsuario = entity.GeraSenhaUsuario,
 	                  IdLinx = entity.IdLinx,
 	                  IdUsuario = entity.IdUsuario,
 	                  Inativo = entity.Inativo,
 	                  IndicaUsuarioServico = entity.IndicaUsuarioServico,
 	                  InscrEstadualRg = entity.InscrEstadualRg,
 	                  Logradouro = entity.Logradouro,
 	                  LxPfjFisicaJuridica = entity.LxPfjFisicaJuridica,
 	                  LxTipoLogradouro = entity.LxTipoLogradouro,
 	                  Municipio = entity.Municipio,
 	                  NomeAutenticacao = entity.NomeAutenticacao,
 	                  NomeCurtoUsuario = entity.NomeCurtoUsuario,
 	                  NomeUsuario = entity.NomeUsuario,
 	                  Numero = entity.Numero,
 	                  ObsEndereco = entity.ObsEndereco,
 	                  Ramal = entity.Ramal,
 	                  Uf = entity.Uf,
 	                  UidUsuario = entity.UidUsuario,
 	                  VigenciaFinal = entity.VigenciaFinal,
 	                  VigenciaInicial = entity.VigenciaInicial
 	                  };
 	                  result.Add(new EntityChange() { Entity = noneTcsUsuarioAutenticacao, Original = noneTcsUsuarioAutenticacao, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext1" });
 	                  break;
 	              case ChangeOperation.Delete:
 	                  //Delete TcsUsuarioAutenticacao
 	                  Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacao deleteTcsUsuarioAutenticacao = new Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacao() {
 	                  AutenticacaoWindows = entity.AutenticacaoWindows,
 	                  Bairro = entity.Bairro,
 	                  Cep = entity.Cep,
 	                  CnpjCpf = entity.CnpjCpf,
 	                  Complemento = entity.Complemento,
 	                  ConfirmacaoUsuario = entity.ConfirmacaoUsuario,
 	                  ConfirmacaoUsuario1 = entity.ConfirmacaoUsuario1,
 	                  CriaUsuario = entity.CriaUsuario,
 	                  DataAlteracao = entity.DataAlteracao,
 	                  DataCadastro = entity.DataCadastro,
 	                  DataExpiracaoSenha = entity.DataExpiracaoSenha,
 	                  Email = entity.Email,
 	                  FoneCelular = entity.FoneCelular,
 	                  FoneFixo = entity.FoneFixo,
 	                  GeraSenhaUsuario = entity.GeraSenhaUsuario,
 	                  IdLinx = entity.IdLinx,
 	                  IdUsuario = entity.IdUsuario,
 	                  Inativo = entity.Inativo,
 	                  IndicaUsuarioServico = entity.IndicaUsuarioServico,
 	                  InscrEstadualRg = entity.InscrEstadualRg,
 	                  Logradouro = entity.Logradouro,
 	                  LxPfjFisicaJuridica = entity.LxPfjFisicaJuridica,
 	                  LxTipoLogradouro = entity.LxTipoLogradouro,
 	                  Municipio = entity.Municipio,
 	                  NomeAutenticacao = entity.NomeAutenticacao,
 	                  NomeCurtoUsuario = entity.NomeCurtoUsuario,
 	                  NomeUsuario = entity.NomeUsuario,
 	                  Numero = entity.Numero,
 	                  ObsEndereco = entity.ObsEndereco,
 	                  Ramal = entity.Ramal,
 	                  Uf = entity.Uf,
 	                  UidUsuario = entity.UidUsuario,
 	                  VigenciaFinal = entity.VigenciaFinal,
 	                  VigenciaInicial = entity.VigenciaInicial
 	                  };
 	                  result.Add(new EntityChange() { Entity = deleteTcsUsuarioAutenticacao, Original = null, Operation = ChangeOperation.Delete, Representation = null, Mark = "serviceContext1" });
 	                  break;
 	              case ChangeOperation.Insert:
 	                  //Insert TcsUsuarioAutenticacao
 	                  Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacao insertTcsUsuarioAutenticacao = new Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacao() {
 	                  AutenticacaoWindows = entity.AutenticacaoWindows,
 	                  Bairro = entity.Bairro,
 	                  Cep = entity.Cep,
 	                  CnpjCpf = entity.CnpjCpf,
 	                  Complemento = entity.Complemento,
 	                  ConfirmacaoUsuario = entity.ConfirmacaoUsuario,
 	                  ConfirmacaoUsuario1 = entity.ConfirmacaoUsuario1,
 	                  CriaUsuario = entity.CriaUsuario,
 	                  DataAlteracao = entity.DataAlteracao,
 	                  DataCadastro = entity.DataCadastro,
 	                  DataExpiracaoSenha = entity.DataExpiracaoSenha,
 	                  Email = entity.Email,
 	                  FoneCelular = entity.FoneCelular,
 	                  FoneFixo = entity.FoneFixo,
 	                  GeraSenhaUsuario = entity.GeraSenhaUsuario,
 	                  IdLinx = entity.IdLinx,
 	                  IdUsuario = entity.IdUsuario,
 	                  Inativo = entity.Inativo,
 	                  IndicaUsuarioServico = entity.IndicaUsuarioServico,
 	                  InscrEstadualRg = entity.InscrEstadualRg,
 	                  Logradouro = entity.Logradouro,
 	                  LxPfjFisicaJuridica = entity.LxPfjFisicaJuridica,
 	                  LxTipoLogradouro = entity.LxTipoLogradouro,
 	                  Municipio = entity.Municipio,
 	                  NomeAutenticacao = entity.NomeAutenticacao,
 	                  NomeCurtoUsuario = entity.NomeCurtoUsuario,
 	                  NomeUsuario = entity.NomeUsuario,
 	                  Numero = entity.Numero,
 	                  ObsEndereco = entity.ObsEndereco,
 	                  Ramal = entity.Ramal,
 	                  Uf = entity.Uf,
 	                  UidUsuario = entity.UidUsuario,
 	                  VigenciaFinal = entity.VigenciaFinal,
 	                  VigenciaInicial = entity.VigenciaInicial
 	                  };
 	                  result.Add(new EntityChange() { Entity = insertTcsUsuarioAutenticacao, Original = null, Operation = ChangeOperation.Insert, Representation = entity, Mark = "serviceContext1" });
 	                  foreach(var insertedEntity in result) insertedEntity.KeysForRefresh.Add("IdUsuario", "IdUsuario");
 	                  break;
 	              case ChangeOperation.Update:
 	                  //Update TcsUsuarioAutenticacao
 	                  Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacao updateTcsUsuarioAutenticacao = new Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacao() {
 	                  AutenticacaoWindows = entity.AutenticacaoWindows,
 	                  Bairro = entity.Bairro,
 	                  Cep = entity.Cep,
 	                  CnpjCpf = entity.CnpjCpf,
 	                  Complemento = entity.Complemento,
 	                  ConfirmacaoUsuario = entity.ConfirmacaoUsuario,
 	                  ConfirmacaoUsuario1 = entity.ConfirmacaoUsuario1,
 	                  CriaUsuario = entity.CriaUsuario,
 	                  DataAlteracao = entity.DataAlteracao,
 	                  DataCadastro = entity.DataCadastro,
 	                  DataExpiracaoSenha = entity.DataExpiracaoSenha,
 	                  Email = entity.Email,
 	                  FoneCelular = entity.FoneCelular,
 	                  FoneFixo = entity.FoneFixo,
 	                  GeraSenhaUsuario = entity.GeraSenhaUsuario,
 	                  IdLinx = entity.IdLinx,
 	                  IdUsuario = entity.IdUsuario,
 	                  Inativo = entity.Inativo,
 	                  IndicaUsuarioServico = entity.IndicaUsuarioServico,
 	                  InscrEstadualRg = entity.InscrEstadualRg,
 	                  Logradouro = entity.Logradouro,
 	                  LxPfjFisicaJuridica = entity.LxPfjFisicaJuridica,
 	                  LxTipoLogradouro = entity.LxTipoLogradouro,
 	                  Municipio = entity.Municipio,
 	                  NomeAutenticacao = entity.NomeAutenticacao,
 	                  NomeCurtoUsuario = entity.NomeCurtoUsuario,
 	                  NomeUsuario = entity.NomeUsuario,
 	                  Numero = entity.Numero,
 	                  ObsEndereco = entity.ObsEndereco,
 	                  Ramal = entity.Ramal,
 	                  Uf = entity.Uf,
 	                  UidUsuario = entity.UidUsuario,
 	                  VigenciaFinal = entity.VigenciaFinal,
 	                  VigenciaInicial = entity.VigenciaInicial
 	                  };
 	                  //Original Definition
 	                  Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacao updateTcsUsuarioAutenticacaoOriginal = (original == null ? null : new Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacao() {
 	                  AutenticacaoWindows = original.AutenticacaoWindows,
 	                  Bairro = original.Bairro,
 	                  Cep = original.Cep,
 	                  CnpjCpf = original.CnpjCpf,
 	                  Complemento = original.Complemento,
 	                  ConfirmacaoUsuario = original.ConfirmacaoUsuario,
 	                  ConfirmacaoUsuario1 = original.ConfirmacaoUsuario1,
 	                  CriaUsuario = original.CriaUsuario,
 	                  DataAlteracao = original.DataAlteracao,
 	                  DataCadastro = original.DataCadastro,
 	                  DataExpiracaoSenha = original.DataExpiracaoSenha,
 	                  Email = original.Email,
 	                  FoneCelular = original.FoneCelular,
 	                  FoneFixo = original.FoneFixo,
 	                  GeraSenhaUsuario = original.GeraSenhaUsuario,
 	                  IdLinx = original.IdLinx,
 	                  IdUsuario = original.IdUsuario,
 	                  Inativo = original.Inativo,
 	                  IndicaUsuarioServico = original.IndicaUsuarioServico,
 	                  InscrEstadualRg = original.InscrEstadualRg,
 	                  Logradouro = original.Logradouro,
 	                  LxPfjFisicaJuridica = original.LxPfjFisicaJuridica,
 	                  LxTipoLogradouro = original.LxTipoLogradouro,
 	                  Municipio = original.Municipio,
 	                  NomeAutenticacao = original.NomeAutenticacao,
 	                  NomeCurtoUsuario = original.NomeCurtoUsuario,
 	                  NomeUsuario = original.NomeUsuario,
 	                  Numero = original.Numero,
 	                  ObsEndereco = original.ObsEndereco,
 	                  Ramal = original.Ramal,
 	                  Uf = original.Uf,
 	                  UidUsuario = original.UidUsuario,
 	                  VigenciaFinal = original.VigenciaFinal,
 	                  VigenciaInicial = original.VigenciaInicial
 	                  });
 	                  result.Add(new EntityChange() { Entity = updateTcsUsuarioAutenticacao, Original = updateTcsUsuarioAutenticacaoOriginal, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext1" });
 	                  break;
 	              default:
 	                  break;
 	          }
 	          return result;
 	    }		
			
	  
 	    //Save All Representations Of Entity TcsUsuarioAutenticacaoAcesso
 	    [Ignore]
 	    private void SaveBufferRepresentationsOfTcsUsuarioAutenticacaoAcesso(List<EntityChange> entityChanges)
 	    {
 	      foreach (ChangeSetEntry entry in this.ChangeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioAutenticacaoAcesso && e.Entity.GetType().Name == "TcsUsuarioAutenticacaoAcesso"))
 	      {
 	          TcsUsuarioAutenticacaoAcesso entity = (TcsUsuarioAutenticacaoAcesso)entry.Entity;
 	          entityChanges.AddRange(this.GetRepresentations(entity, (this.ChangeSet.GetChangeOperation(entity) == ChangeOperation.Update ? this.ChangeSet.GetOriginal(entity) : null), this.ChangeSet.GetChangeOperation(entity)));
 	      }
 	    }
 
 	    //Get Representation Of TcsUsuarioAutenticacaoAcesso
 	    [Ignore]
 	    private List<EntityChange> GetRepresentations(TcsUsuarioAutenticacaoAcesso entity, TcsUsuarioAutenticacaoAcesso original, ChangeOperation operation)
 	    {
 	          List<EntityChange> result = new List<EntityChange>();
 	          switch (operation)
 	          {
 	              case ChangeOperation.None:
 	                  //None TcsUsuarioAutenticacaoAcessoP
 	                  Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacaoAcessoP noneTcsUsuarioAutenticacaoAcessoP = new Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacaoAcessoP() {
 	                  DescricaoAmbiente = entity.DescricaoAmbiente,
 	                  DescricaoAmbienteRelacionado = entity.DescricaoAmbienteRelacionado,
 	                  DescricaoAplicacao = entity.DescricaoAplicacao,
 	                  DescricaoAplicativo = entity.DescricaoAplicativo,
 	                  IdAplicacao = entity.IdAplicacao,
 	                  IdLinx = entity.IdLinx,
 	                  IdTcsAmbiente = entity.IdTcsAmbiente,
 	                  IdTcsAmbienteRelacionado = entity.IdTcsAmbienteRelacionado,
 	                  IdTcsAplicativo = entity.IdTcsAplicativo,
 	                  IdTcsUsuarioAcesso = entity.IdTcsUsuarioAcesso,
 	                  IdUsuario = entity.IdUsuario,
 	                  IndicaAcessoPadrao = entity.IndicaAcessoPadrao,
 	                  NomeAutenticacao = entity.NomeAutenticacao,
 	                  NomeEmpresa = entity.NomeEmpresa,
 	                  NomeUsuario = entity.NomeUsuario,
 	                  Perfil = entity.Perfil
 	                  };
 	                  result.Add(new EntityChange() { Entity = noneTcsUsuarioAutenticacaoAcessoP, Original = noneTcsUsuarioAutenticacaoAcessoP, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext1" });
 	                  break;
 	              case ChangeOperation.Delete:
 	                  //Delete TcsUsuarioAutenticacaoAcessoP
 	                  Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacaoAcessoP deleteTcsUsuarioAutenticacaoAcessoP = new Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacaoAcessoP() {
 	                  DescricaoAmbiente = entity.DescricaoAmbiente,
 	                  DescricaoAmbienteRelacionado = entity.DescricaoAmbienteRelacionado,
 	                  DescricaoAplicacao = entity.DescricaoAplicacao,
 	                  DescricaoAplicativo = entity.DescricaoAplicativo,
 	                  IdAplicacao = entity.IdAplicacao,
 	                  IdLinx = entity.IdLinx,
 	                  IdTcsAmbiente = entity.IdTcsAmbiente,
 	                  IdTcsAmbienteRelacionado = entity.IdTcsAmbienteRelacionado,
 	                  IdTcsAplicativo = entity.IdTcsAplicativo,
 	                  IdTcsUsuarioAcesso = entity.IdTcsUsuarioAcesso,
 	                  IdUsuario = entity.IdUsuario,
 	                  IndicaAcessoPadrao = entity.IndicaAcessoPadrao,
 	                  NomeAutenticacao = entity.NomeAutenticacao,
 	                  NomeEmpresa = entity.NomeEmpresa,
 	                  NomeUsuario = entity.NomeUsuario,
 	                  Perfil = entity.Perfil
 	                  };
 	                  result.Add(new EntityChange() { Entity = deleteTcsUsuarioAutenticacaoAcessoP, Original = null, Operation = ChangeOperation.Delete, Representation = null, Mark = "serviceContext1" });
 	                  break;
 	              case ChangeOperation.Insert:
 	                  //Insert TcsUsuarioAutenticacaoAcessoP
 	                  Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacaoAcessoP insertTcsUsuarioAutenticacaoAcessoP = new Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacaoAcessoP() {
 	                  DescricaoAmbiente = entity.DescricaoAmbiente,
 	                  DescricaoAmbienteRelacionado = entity.DescricaoAmbienteRelacionado,
 	                  DescricaoAplicacao = entity.DescricaoAplicacao,
 	                  DescricaoAplicativo = entity.DescricaoAplicativo,
 	                  IdAplicacao = entity.IdAplicacao,
 	                  IdLinx = entity.IdLinx,
 	                  IdTcsAmbiente = entity.IdTcsAmbiente,
 	                  IdTcsAmbienteRelacionado = entity.IdTcsAmbienteRelacionado,
 	                  IdTcsAplicativo = entity.IdTcsAplicativo,
 	                  IdTcsUsuarioAcesso = entity.IdTcsUsuarioAcesso,
 	                  IdUsuario = entity.IdUsuario,
 	                  IndicaAcessoPadrao = entity.IndicaAcessoPadrao,
 	                  NomeAutenticacao = entity.NomeAutenticacao,
 	                  NomeEmpresa = entity.NomeEmpresa,
 	                  NomeUsuario = entity.NomeUsuario,
 	                  Perfil = entity.Perfil
 	                  };
 	                  result.Add(new EntityChange() { Entity = insertTcsUsuarioAutenticacaoAcessoP, Original = null, Operation = ChangeOperation.Insert, Representation = entity, Mark = "serviceContext1" });
 	                  foreach(var insertedEntity in result) insertedEntity.KeysForRefresh.Add("IdTcsUsuarioAcesso", "IdTcsUsuarioAcesso");
 	                  break;
 	              case ChangeOperation.Update:
 	                  //Update TcsUsuarioAutenticacaoAcessoP
 	                  Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacaoAcessoP updateTcsUsuarioAutenticacaoAcessoP = new Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacaoAcessoP() {
 	                  DescricaoAmbiente = entity.DescricaoAmbiente,
 	                  DescricaoAmbienteRelacionado = entity.DescricaoAmbienteRelacionado,
 	                  DescricaoAplicacao = entity.DescricaoAplicacao,
 	                  DescricaoAplicativo = entity.DescricaoAplicativo,
 	                  IdAplicacao = entity.IdAplicacao,
 	                  IdLinx = entity.IdLinx,
 	                  IdTcsAmbiente = entity.IdTcsAmbiente,
 	                  IdTcsAmbienteRelacionado = entity.IdTcsAmbienteRelacionado,
 	                  IdTcsAplicativo = entity.IdTcsAplicativo,
 	                  IdTcsUsuarioAcesso = entity.IdTcsUsuarioAcesso,
 	                  IdUsuario = entity.IdUsuario,
 	                  IndicaAcessoPadrao = entity.IndicaAcessoPadrao,
 	                  NomeAutenticacao = entity.NomeAutenticacao,
 	                  NomeEmpresa = entity.NomeEmpresa,
 	                  NomeUsuario = entity.NomeUsuario,
 	                  Perfil = entity.Perfil
 	                  };
 	                  //Original Definition
 	                  Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacaoAcessoP updateTcsUsuarioAutenticacaoAcessoPOriginal = (original == null ? null : new Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacaoAcessoP() {
 	                  DescricaoAmbiente = original.DescricaoAmbiente,
 	                  DescricaoAmbienteRelacionado = original.DescricaoAmbienteRelacionado,
 	                  DescricaoAplicacao = original.DescricaoAplicacao,
 	                  DescricaoAplicativo = original.DescricaoAplicativo,
 	                  IdAplicacao = original.IdAplicacao,
 	                  IdLinx = original.IdLinx,
 	                  IdTcsAmbiente = original.IdTcsAmbiente,
 	                  IdTcsAmbienteRelacionado = original.IdTcsAmbienteRelacionado,
 	                  IdTcsAplicativo = original.IdTcsAplicativo,
 	                  IdTcsUsuarioAcesso = original.IdTcsUsuarioAcesso,
 	                  IdUsuario = original.IdUsuario,
 	                  IndicaAcessoPadrao = original.IndicaAcessoPadrao,
 	                  NomeAutenticacao = original.NomeAutenticacao,
 	                  NomeEmpresa = original.NomeEmpresa,
 	                  NomeUsuario = original.NomeUsuario,
 	                  Perfil = original.Perfil
 	                  });
 	                  result.Add(new EntityChange() { Entity = updateTcsUsuarioAutenticacaoAcessoP, Original = updateTcsUsuarioAutenticacaoAcessoPOriginal, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext1" });
 	                  break;
 	              default:
 	                  break;
 	          }
 	          return result;
 	    }		
			
	  
 	    //Save All Representations Of Entity TcsUsuarioAutenticacaoPerfil
 	    [Ignore]
 	    private void SaveBufferRepresentationsOfTcsUsuarioAutenticacaoPerfil(List<EntityChange> entityChanges)
 	    {
 	      foreach (ChangeSetEntry entry in this.ChangeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioAutenticacaoPerfil && e.Entity.GetType().Name == "TcsUsuarioAutenticacaoPerfil"))
 	      {
 	          TcsUsuarioAutenticacaoPerfil entity = (TcsUsuarioAutenticacaoPerfil)entry.Entity;
 	          entityChanges.AddRange(this.GetRepresentations(entity, (this.ChangeSet.GetChangeOperation(entity) == ChangeOperation.Update ? this.ChangeSet.GetOriginal(entity) : null), this.ChangeSet.GetChangeOperation(entity)));
 	      }
 	    }
 
 	    //Get Representation Of TcsUsuarioAutenticacaoPerfil
 	    [Ignore]
 	    private List<EntityChange> GetRepresentations(TcsUsuarioAutenticacaoPerfil entity, TcsUsuarioAutenticacaoPerfil original, ChangeOperation operation)
 	    {
 	          List<EntityChange> result = new List<EntityChange>();
 	          switch (operation)
 	          {
 	              case ChangeOperation.None:
 	                  //None TcsUsuarioPerfil
 	                  Linx.Framework.BV.UsuarioFranquia.TcsUsuarioPerfil noneTcsUsuarioPerfil = new Linx.Framework.BV.UsuarioFranquia.TcsUsuarioPerfil() {
 	                  DescPerfil = entity.DescPerfil,
 	                  IdLinxPerfil = entity.IdLinxPerfil,
 	                  IdPerfil = entity.IdPerfil,
 	                  IdTcsUsuarioPerfil = entity.IdTcsUsuarioPerfil,
 	                  IdUsuario = entity.IdUsuario,
 	                  NomeUsuario = entity.NomeUsuario
 	                  };
 	                  result.Add(new EntityChange() { Entity = noneTcsUsuarioPerfil, Original = noneTcsUsuarioPerfil, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext2" });
 	                  break;
 	              case ChangeOperation.Delete:
 	                  //Delete TcsUsuarioPerfil
 	                  Linx.Framework.BV.UsuarioFranquia.TcsUsuarioPerfil deleteTcsUsuarioPerfil = new Linx.Framework.BV.UsuarioFranquia.TcsUsuarioPerfil() {
 	                  DescPerfil = entity.DescPerfil,
 	                  IdLinxPerfil = entity.IdLinxPerfil,
 	                  IdPerfil = entity.IdPerfil,
 	                  IdTcsUsuarioPerfil = entity.IdTcsUsuarioPerfil,
 	                  IdUsuario = entity.IdUsuario,
 	                  NomeUsuario = entity.NomeUsuario
 	                  };
 	                  result.Add(new EntityChange() { Entity = deleteTcsUsuarioPerfil, Original = null, Operation = ChangeOperation.Delete, Representation = null, Mark = "serviceContext2" });
 	                  break;
 	              case ChangeOperation.Insert:
 	                  //Insert TcsUsuarioPerfil
 	                  Linx.Framework.BV.UsuarioFranquia.TcsUsuarioPerfil insertTcsUsuarioPerfil = new Linx.Framework.BV.UsuarioFranquia.TcsUsuarioPerfil() {
 	                  DescPerfil = entity.DescPerfil,
 	                  IdLinxPerfil = entity.IdLinxPerfil,
 	                  IdPerfil = entity.IdPerfil,
 	                  IdTcsUsuarioPerfil = entity.IdTcsUsuarioPerfil,
 	                  IdUsuario = entity.IdUsuario,
 	                  NomeUsuario = entity.NomeUsuario
 	                  };
 	                  result.Add(new EntityChange() { Entity = insertTcsUsuarioPerfil, Original = null, Operation = ChangeOperation.Insert, Representation = entity, Mark = "serviceContext2" });
 	                  foreach(var insertedEntity in result) insertedEntity.KeysForRefresh.Add("IdTcsUsuarioPerfil", "IdTcsUsuarioPerfil");
 	                  break;
 	              case ChangeOperation.Update:
 	                  //Update TcsUsuarioPerfil
 	                  Linx.Framework.BV.UsuarioFranquia.TcsUsuarioPerfil updateTcsUsuarioPerfil = new Linx.Framework.BV.UsuarioFranquia.TcsUsuarioPerfil() {
 	                  DescPerfil = entity.DescPerfil,
 	                  IdLinxPerfil = entity.IdLinxPerfil,
 	                  IdPerfil = entity.IdPerfil,
 	                  IdTcsUsuarioPerfil = entity.IdTcsUsuarioPerfil,
 	                  IdUsuario = entity.IdUsuario,
 	                  NomeUsuario = entity.NomeUsuario
 	                  };
 	                  //Original Definition
 	                  Linx.Framework.BV.UsuarioFranquia.TcsUsuarioPerfil updateTcsUsuarioPerfilOriginal = (original == null ? null : new Linx.Framework.BV.UsuarioFranquia.TcsUsuarioPerfil() {
 	                  DescPerfil = original.DescPerfil,
 	                  IdLinxPerfil = original.IdLinxPerfil,
 	                  IdPerfil = original.IdPerfil,
 	                  IdTcsUsuarioPerfil = original.IdTcsUsuarioPerfil,
 	                  IdUsuario = original.IdUsuario,
 	                  NomeUsuario = original.NomeUsuario
 	                  });
 	                  result.Add(new EntityChange() { Entity = updateTcsUsuarioPerfil, Original = updateTcsUsuarioPerfilOriginal, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext2" });
 	                  break;
 	              default:
 	                  break;
 	          }
 	          return result;
 	    }		
	
	    #endregion Save Representations.



	    #region Get OLAP Definitions.
	
			
	
	    #endregion Get OLAP Definitions.


	    #region Get LookUp Definitions.
	
		
			
        [Ignore]
	    //Get All LookUpTcsPerfil.
	    public IQueryable<LookUpTcsPerfil> GetAllLookUpTcsPerfil()
	    {
	        return this.GetLookUpTcsPerfil(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsPerfil By EntitySearch.
	    public IQueryable<LookUpTcsPerfil> GetLookUpTcsPerfilByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsPerfil(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsPerfil.
	    public IQueryable<LookUpTcsPerfil> GetLookUpTcsPerfil(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TCS_PERFIL" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsPerfil";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsPerfil));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsPerfil> query =  
	
	            (from entity in this.DbContext.TCS_PERFIL.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpTcsPerfil()		
	            {
	            
                DescPerfil = entity.DESC_PERFIL
                , IdPerfil = entity.ID_PERFIL
                , Inativo = entity.INATIVO
                , IdLinxPerfil = entity.ID_LINX
	            });

	            
	
		
			
		
	        TcsUsuarioPerfil.OnLookingUpLookUpTcsPerfil(ref query, propertyName, entitySearch);
	
	
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
	
		

	        if (entityName.InList("Linx.Framework.BV.UsuarioFranquia.TcsUsuarioAutenticacao"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsUsuarioAutenticacao",
	        			NameSpace = "Linx.Framework.BV.UsuarioFranquia",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsUsuarioAutenticacao",
	        			ClearMethodName = "ClearTcsUsuarioAutenticacao",
	        			QueryMethodName  = "GetPagedTcsUsuarioAutenticacao",	
	        			CountingMethodName  = "GetTcsUsuarioAutenticacao" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.UsuarioFranquia.TcsUsuarioAutenticacao"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.UsuarioFranquia.TcsUsuarioAutenticacao"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.UsuarioFranquia.TcsUsuarioAutenticacao", "Linx.Framework.BV.UsuarioFranquia.TcsUsuarioAutenticacaoAcesso"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsUsuarioAutenticacaoAcesso",
	        			NameSpace = "Linx.Framework.BV.UsuarioFranquia",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsUsuarioAutenticacao",	
	        			DisplayName = "Acessos",
	        			ClearMethodName = "ClearTcsUsuarioAutenticacaoAcesso",
	        			QueryMethodName  = "GetPagedTcsUsuarioAutenticacaoAcesso",	
	        			CountingMethodName  = "GetTcsUsuarioAutenticacaoAcesso" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.UsuarioFranquia.TcsUsuarioAutenticacaoAcesso"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.UsuarioFranquia.TcsUsuarioAutenticacaoAcesso"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.UsuarioFranquia.TcsUsuarioAutenticacaoPerfil"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsUsuarioAutenticacaoPerfil",
	        			NameSpace = "Linx.Framework.BV.UsuarioFranquia",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "Perfil",
	        			ClearMethodName = "ClearTcsUsuarioAutenticacaoPerfil",
	        			QueryMethodName  = "GetPagedTcsUsuarioAutenticacaoPerfil",	
	        			CountingMethodName  = "GetTcsUsuarioAutenticacaoPerfil" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.UsuarioFranquia.TcsUsuarioAutenticacaoPerfil"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.UsuarioFranquia.TcsUsuarioAutenticacaoPerfil"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.UsuarioFranquia.TcsUsuarioPerfil"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsUsuarioPerfil",
	        			NameSpace = "Linx.Framework.BV.UsuarioFranquia",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsUsuarioPerfil",
	        			ClearMethodName = "ClearTcsUsuarioPerfil",
	        			QueryMethodName  = "GetPagedTcsUsuarioPerfil",	
	        			CountingMethodName  = "GetTcsUsuarioPerfil" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.UsuarioFranquia.TcsUsuarioPerfil"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.UsuarioFranquia.TcsUsuarioPerfil"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.UsuarioFranquia.UsuarioPerfilInfo"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "UsuarioPerfilInfo",
	        			NameSpace = "Linx.Framework.BV.UsuarioFranquia",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "UsuarioPerfilInfo",
	        			ClearMethodName = "ClearUsuarioPerfilInfo",
	        			QueryMethodName  = "GetPagedUsuarioPerfilInfo",	
	        			CountingMethodName  = "GetUsuarioPerfilInfo" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.UsuarioFranquia.UsuarioPerfilInfo"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.UsuarioFranquia.UsuarioPerfilInfo"), forceAll: forceAll)
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

         		    return new string[] { "Framework_UsuarioFranquiaClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.UsuarioFranquiaClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Framework_usuarioFranquiaService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.usuarioFranquiaService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
			
	        result[0].TcsUsuarioAutenticacaoAcessoList = new List<TcsUsuarioAutenticacaoAcesso>();
	        ((List<TcsUsuarioAutenticacaoAcesso>)result[0].TcsUsuarioAutenticacaoAcessoList).Add(new TcsUsuarioAutenticacaoAcesso());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsUsuarioAutenticacaoAcesso.
	    public IEnumerable<TcsUsuarioAutenticacaoAcesso> ClearTcsUsuarioAutenticacaoAcesso()
	    {
	        List<TcsUsuarioAutenticacaoAcesso> result = new List<TcsUsuarioAutenticacaoAcesso>();
	        result.Add(new TcsUsuarioAutenticacaoAcesso());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsUsuarioAutenticacaoPerfil.
	    public IEnumerable<TcsUsuarioAutenticacaoPerfil> ClearTcsUsuarioAutenticacaoPerfil()
	    {
	        List<TcsUsuarioAutenticacaoPerfil> result = new List<TcsUsuarioAutenticacaoPerfil>();
	        result.Add(new TcsUsuarioAutenticacaoPerfil());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsUsuarioPerfil.
	    public IEnumerable<TcsUsuarioPerfil> ClearTcsUsuarioPerfil()
	    {
	        List<TcsUsuarioPerfil> result = new List<TcsUsuarioPerfil>();
	        result.Add(new TcsUsuarioPerfil());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear UsuarioPerfilInfo.
	    public IEnumerable<UsuarioPerfilInfo> ClearUsuarioPerfilInfo()
	    {
	        List<UsuarioPerfilInfo> result = new List<UsuarioPerfilInfo>();
	        result.Add(new UsuarioPerfilInfo());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsUsuarioAutenticacao.
	    public IQueryable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacao()
	    {




		
	
	        
		
	        
             Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService serviceContext1 = new Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsUsuarioAutenticacao> result = 
	            (
                 from TcsUsuarioAutenticacao_Rep1 in serviceContext1.GetTcsUsuarioAutenticacaoNoAssociations()
	            
	            	
	            select new TcsUsuarioAutenticacao()		
	            {
	            
                AutenticacaoWindows = TcsUsuarioAutenticacao_Rep1.AutenticacaoWindows
                , Bairro = TcsUsuarioAutenticacao_Rep1.Bairro
                , Cep = TcsUsuarioAutenticacao_Rep1.Cep
                , CnpjCpf = TcsUsuarioAutenticacao_Rep1.CnpjCpf
                , Complemento = TcsUsuarioAutenticacao_Rep1.Complemento
                , ConfirmacaoUsuario = TcsUsuarioAutenticacao_Rep1.ConfirmacaoUsuario
                , ConfirmacaoUsuario1 = TcsUsuarioAutenticacao_Rep1.ConfirmacaoUsuario1
                , CriaUsuario = TcsUsuarioAutenticacao_Rep1.CriaUsuario
                , DataAlteracao = TcsUsuarioAutenticacao_Rep1.DataAlteracao
                , DataCadastro = TcsUsuarioAutenticacao_Rep1.DataCadastro
                , DataExpiracaoSenha = TcsUsuarioAutenticacao_Rep1.DataExpiracaoSenha
                , Email = TcsUsuarioAutenticacao_Rep1.Email
                , FoneCelular = TcsUsuarioAutenticacao_Rep1.FoneCelular
                , FoneFixo = TcsUsuarioAutenticacao_Rep1.FoneFixo
                , GeraSenhaUsuario = TcsUsuarioAutenticacao_Rep1.GeraSenhaUsuario
                , IdLinx = TcsUsuarioAutenticacao_Rep1.IdLinx
                , IdUsuario = TcsUsuarioAutenticacao_Rep1.IdUsuario
                , Inativo = TcsUsuarioAutenticacao_Rep1.Inativo
                , IndicaUsuarioServico = TcsUsuarioAutenticacao_Rep1.IndicaUsuarioServico
                , InscrEstadualRg = TcsUsuarioAutenticacao_Rep1.InscrEstadualRg
                , Logradouro = TcsUsuarioAutenticacao_Rep1.Logradouro
                , LxPfjFisicaJuridica = TcsUsuarioAutenticacao_Rep1.LxPfjFisicaJuridica
                , LxPfjFisicaJuridicaName = ((TcsUsuarioAutenticacao_Rep1.LxPfjFisicaJuridica) == 1 ? "Pessoa Física" : ((TcsUsuarioAutenticacao_Rep1.LxPfjFisicaJuridica) == 2 ? "Pessoa Jurídica" : ""))
                , LxTipoLogradouro = TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro
                , LxTipoLogradouroName = ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 1 ? "Aeroporto" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 2 ? "Alameda" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 3 ? "Apartamento" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 4 ? "Avenida" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 5 ? "Beco" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 6 ? "Bloco" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 7 ? "Caminho" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 8 ? "Escadinha" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 9 ? "Estação" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 10 ? "Estrada" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 11 ? "Fazenda" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 12 ? "Fortaleza" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 13 ? "Galeria" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 14 ? "Ladeira" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 15 ? "Largo" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 17 ? "Parque" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 16 ? "Praça" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 18 ? "Praia" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 19 ? "Quadra" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 20 ? "Quilômetro" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 21 ? "Quinta" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 22 ? "Rodovia" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 23 ? "Rua" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 24 ? "Super Quadra" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 25 ? "Travessa" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 26 ? "Viaduto" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 27 ? "Vila" : "")))))))))))))))))))))))))))
                , Municipio = TcsUsuarioAutenticacao_Rep1.Municipio
                , NomeAutenticacao = TcsUsuarioAutenticacao_Rep1.NomeAutenticacao
                , NomeCurtoUsuario = TcsUsuarioAutenticacao_Rep1.NomeCurtoUsuario
                , NomeUsuario = TcsUsuarioAutenticacao_Rep1.NomeUsuario
                , Numero = TcsUsuarioAutenticacao_Rep1.Numero
                , ObsEndereco = TcsUsuarioAutenticacao_Rep1.ObsEndereco
                , Ramal = TcsUsuarioAutenticacao_Rep1.Ramal
                , Uf = TcsUsuarioAutenticacao_Rep1.Uf
                , UidUsuario = TcsUsuarioAutenticacao_Rep1.UidUsuario
                , VigenciaFinal = TcsUsuarioAutenticacao_Rep1.VigenciaFinal
                , VigenciaInicial = TcsUsuarioAutenticacao_Rep1.VigenciaInicial
		
	            }
	            );
		
	
	        TcsUsuarioAutenticacao.OnSearching(ref result, false, null);	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsUsuarioAutenticacaoAcesso.
	    public IQueryable<TcsUsuarioAutenticacaoAcesso> GetTcsUsuarioAutenticacaoAcesso()
	    {




		
	
	        
		
	        
             Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService serviceContext1 = new Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsUsuarioAutenticacaoAcesso> result = 
	            (
                 from TcsUsuarioAutenticacaoAcessoP_Rep1 in serviceContext1.GetTcsUsuarioAutenticacaoAcessoPNoAssociations()
	            
	            	
	            select new TcsUsuarioAutenticacaoAcesso()		
	            {
	            
                DescricaoAmbiente = TcsUsuarioAutenticacaoAcessoP_Rep1.DescricaoAmbiente
                , DescricaoAmbienteRelacionado = TcsUsuarioAutenticacaoAcessoP_Rep1.DescricaoAmbienteRelacionado
                , DescricaoAplicacao = TcsUsuarioAutenticacaoAcessoP_Rep1.DescricaoAplicacao
                , DescricaoAplicativo = TcsUsuarioAutenticacaoAcessoP_Rep1.DescricaoAplicativo
                , IdAplicacao = TcsUsuarioAutenticacaoAcessoP_Rep1.IdAplicacao
                , IdLinx = TcsUsuarioAutenticacaoAcessoP_Rep1.IdLinx
                , IdTcsAmbiente = TcsUsuarioAutenticacaoAcessoP_Rep1.IdTcsAmbiente
                , IdTcsAmbienteRelacionado = TcsUsuarioAutenticacaoAcessoP_Rep1.IdTcsAmbienteRelacionado
                , IdTcsAplicativo = TcsUsuarioAutenticacaoAcessoP_Rep1.IdTcsAplicativo
                , IdTcsUsuarioAcesso = TcsUsuarioAutenticacaoAcessoP_Rep1.IdTcsUsuarioAcesso
                , IdUsuario = TcsUsuarioAutenticacaoAcessoP_Rep1.IdUsuario
                , IndicaAcessoPadrao = TcsUsuarioAutenticacaoAcessoP_Rep1.IndicaAcessoPadrao
                , NomeAutenticacao = TcsUsuarioAutenticacaoAcessoP_Rep1.NomeAutenticacao
                , NomeEmpresa = TcsUsuarioAutenticacaoAcessoP_Rep1.NomeEmpresa
                , NomeUsuario = TcsUsuarioAutenticacaoAcessoP_Rep1.NomeUsuario
                , Perfil = TcsUsuarioAutenticacaoAcessoP_Rep1.Perfil
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioAutenticacaoNoAssociations.
	    public IQueryable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoNoAssociations()
	    {




		
	
	        
		
	        
             Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService serviceContext1 = new Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsUsuarioAutenticacao> result = 
	            (
                 from TcsUsuarioAutenticacao_Rep1 in serviceContext1.GetTcsUsuarioAutenticacaoNoAssociations()
	            
	            	
	            select new TcsUsuarioAutenticacao()		
	            {
	            
                AutenticacaoWindows = TcsUsuarioAutenticacao_Rep1.AutenticacaoWindows
                , Bairro = TcsUsuarioAutenticacao_Rep1.Bairro
                , Cep = TcsUsuarioAutenticacao_Rep1.Cep
                , CnpjCpf = TcsUsuarioAutenticacao_Rep1.CnpjCpf
                , Complemento = TcsUsuarioAutenticacao_Rep1.Complemento
                , ConfirmacaoUsuario = TcsUsuarioAutenticacao_Rep1.ConfirmacaoUsuario
                , ConfirmacaoUsuario1 = TcsUsuarioAutenticacao_Rep1.ConfirmacaoUsuario1
                , CriaUsuario = TcsUsuarioAutenticacao_Rep1.CriaUsuario
                , DataAlteracao = TcsUsuarioAutenticacao_Rep1.DataAlteracao
                , DataCadastro = TcsUsuarioAutenticacao_Rep1.DataCadastro
                , DataExpiracaoSenha = TcsUsuarioAutenticacao_Rep1.DataExpiracaoSenha
                , Email = TcsUsuarioAutenticacao_Rep1.Email
                , FoneCelular = TcsUsuarioAutenticacao_Rep1.FoneCelular
                , FoneFixo = TcsUsuarioAutenticacao_Rep1.FoneFixo
                , GeraSenhaUsuario = TcsUsuarioAutenticacao_Rep1.GeraSenhaUsuario
                , IdLinx = TcsUsuarioAutenticacao_Rep1.IdLinx
                , IdUsuario = TcsUsuarioAutenticacao_Rep1.IdUsuario
                , Inativo = TcsUsuarioAutenticacao_Rep1.Inativo
                , IndicaUsuarioServico = TcsUsuarioAutenticacao_Rep1.IndicaUsuarioServico
                , InscrEstadualRg = TcsUsuarioAutenticacao_Rep1.InscrEstadualRg
                , Logradouro = TcsUsuarioAutenticacao_Rep1.Logradouro
                , LxPfjFisicaJuridica = TcsUsuarioAutenticacao_Rep1.LxPfjFisicaJuridica
                , LxPfjFisicaJuridicaName = ((TcsUsuarioAutenticacao_Rep1.LxPfjFisicaJuridica) == 1 ? "Pessoa Física" : ((TcsUsuarioAutenticacao_Rep1.LxPfjFisicaJuridica) == 2 ? "Pessoa Jurídica" : ""))
                , LxTipoLogradouro = TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro
                , LxTipoLogradouroName = ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 1 ? "Aeroporto" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 2 ? "Alameda" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 3 ? "Apartamento" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 4 ? "Avenida" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 5 ? "Beco" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 6 ? "Bloco" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 7 ? "Caminho" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 8 ? "Escadinha" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 9 ? "Estação" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 10 ? "Estrada" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 11 ? "Fazenda" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 12 ? "Fortaleza" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 13 ? "Galeria" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 14 ? "Ladeira" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 15 ? "Largo" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 17 ? "Parque" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 16 ? "Praça" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 18 ? "Praia" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 19 ? "Quadra" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 20 ? "Quilômetro" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 21 ? "Quinta" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 22 ? "Rodovia" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 23 ? "Rua" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 24 ? "Super Quadra" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 25 ? "Travessa" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 26 ? "Viaduto" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 27 ? "Vila" : "")))))))))))))))))))))))))))
                , Municipio = TcsUsuarioAutenticacao_Rep1.Municipio
                , NomeAutenticacao = TcsUsuarioAutenticacao_Rep1.NomeAutenticacao
                , NomeCurtoUsuario = TcsUsuarioAutenticacao_Rep1.NomeCurtoUsuario
                , NomeUsuario = TcsUsuarioAutenticacao_Rep1.NomeUsuario
                , Numero = TcsUsuarioAutenticacao_Rep1.Numero
                , ObsEndereco = TcsUsuarioAutenticacao_Rep1.ObsEndereco
                , Ramal = TcsUsuarioAutenticacao_Rep1.Ramal
                , Uf = TcsUsuarioAutenticacao_Rep1.Uf
                , UidUsuario = TcsUsuarioAutenticacao_Rep1.UidUsuario
                , VigenciaFinal = TcsUsuarioAutenticacao_Rep1.VigenciaFinal
                , VigenciaInicial = TcsUsuarioAutenticacao_Rep1.VigenciaInicial
		
	            }
	            );
		
	
	        TcsUsuarioAutenticacao.OnSearching(ref result, true, null);	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioAutenticacaoAcessoNoAssociations.
	    public IQueryable<TcsUsuarioAutenticacaoAcesso> GetTcsUsuarioAutenticacaoAcessoNoAssociations()
	    {




		
	
	        
		
	        
             Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService serviceContext1 = new Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsUsuarioAutenticacaoAcesso> result = 
	            (
                 from TcsUsuarioAutenticacaoAcessoP_Rep1 in serviceContext1.GetTcsUsuarioAutenticacaoAcessoPNoAssociations()
	            
	            	
	            select new TcsUsuarioAutenticacaoAcesso()		
	            {
	            
                DescricaoAmbiente = TcsUsuarioAutenticacaoAcessoP_Rep1.DescricaoAmbiente
                , DescricaoAmbienteRelacionado = TcsUsuarioAutenticacaoAcessoP_Rep1.DescricaoAmbienteRelacionado
                , DescricaoAplicacao = TcsUsuarioAutenticacaoAcessoP_Rep1.DescricaoAplicacao
                , DescricaoAplicativo = TcsUsuarioAutenticacaoAcessoP_Rep1.DescricaoAplicativo
                , IdAplicacao = TcsUsuarioAutenticacaoAcessoP_Rep1.IdAplicacao
                , IdLinx = TcsUsuarioAutenticacaoAcessoP_Rep1.IdLinx
                , IdTcsAmbiente = TcsUsuarioAutenticacaoAcessoP_Rep1.IdTcsAmbiente
                , IdTcsAmbienteRelacionado = TcsUsuarioAutenticacaoAcessoP_Rep1.IdTcsAmbienteRelacionado
                , IdTcsAplicativo = TcsUsuarioAutenticacaoAcessoP_Rep1.IdTcsAplicativo
                , IdTcsUsuarioAcesso = TcsUsuarioAutenticacaoAcessoP_Rep1.IdTcsUsuarioAcesso
                , IdUsuario = TcsUsuarioAutenticacaoAcessoP_Rep1.IdUsuario
                , IndicaAcessoPadrao = TcsUsuarioAutenticacaoAcessoP_Rep1.IndicaAcessoPadrao
                , NomeAutenticacao = TcsUsuarioAutenticacaoAcessoP_Rep1.NomeAutenticacao
                , NomeEmpresa = TcsUsuarioAutenticacaoAcessoP_Rep1.NomeEmpresa
                , NomeUsuario = TcsUsuarioAutenticacaoAcessoP_Rep1.NomeUsuario
                , Perfil = TcsUsuarioAutenticacaoAcessoP_Rep1.Perfil
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsUsuarioAutenticacaoPerfil.
	    public IEnumerable<TcsUsuarioAutenticacaoPerfil> GetTcsUsuarioAutenticacaoPerfil()
	    {




		

	        IEnumerable<TcsUsuarioAutenticacaoPerfil> result = 
	            (from entity0 in TcsUsuarioAutenticacaoPerfil.OnSearchingReplacement(null) select entity0);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioAutenticacaoPerfilNoAssociations.
	    public IEnumerable<TcsUsuarioAutenticacaoPerfil> GetTcsUsuarioAutenticacaoPerfilNoAssociations()
	    {




		

	        IEnumerable<TcsUsuarioAutenticacaoPerfil> result = 
	            (from entity0 in TcsUsuarioAutenticacaoPerfil.OnSearchingReplacement(null) select entity0);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsUsuarioPerfil.
	    public IQueryable<TcsUsuarioPerfil> GetTcsUsuarioPerfil()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioPerfil> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_PERFIL
                  let entity0Al1 = entity0.TCS_PERFIL
                  let entity0Al2 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioPerfil()		
	            {
	            
                DescPerfil = entity0Al1.DESC_PERFIL
                , IdLinxPerfil = entity0Al1.ID_LINX
                , IdPerfil = entity0Al1.ID_PERFIL
                , IdTcsUsuarioPerfil = entity0.ID_TCS_USUARIO_PERFIL
                , IdUsuario = entity0Al2.ID_USUARIO
                , Inativo = entity0Al1.INATIVO
                , NomeUsuario = entity0Al2.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioPerfilNoAssociations.
	    public IQueryable<TcsUsuarioPerfil> GetTcsUsuarioPerfilNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioPerfil> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_PERFIL
                  let entity0Al1 = entity0.TCS_PERFIL
                  let entity0Al2 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioPerfil()		
	            {
	            
                DescPerfil = entity0Al1.DESC_PERFIL
                , IdLinxPerfil = entity0Al1.ID_LINX
                , IdPerfil = entity0Al1.ID_PERFIL
                , IdTcsUsuarioPerfil = entity0.ID_TCS_USUARIO_PERFIL
                , IdUsuario = entity0Al2.ID_USUARIO
                , Inativo = entity0Al1.INATIVO
                , NomeUsuario = entity0Al2.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get UsuarioPerfilInfo.
	    public IEnumerable<UsuarioPerfilInfo> GetUsuarioPerfilInfo()
	    {




	
	        IEnumerable<UsuarioPerfilInfo> result = new List<UsuarioPerfilInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get UsuarioPerfilInfoNoAssociations.
	    public IEnumerable<UsuarioPerfilInfo> GetUsuarioPerfilInfoNoAssociations()
	    {




	
	        IEnumerable<UsuarioPerfilInfo> result = new List<UsuarioPerfilInfo>();
	  	
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	result.Add("TcsUsuarioAutenticacaoAcesso|Perfil");
	    	result.Add("TcsUsuarioAutenticacaoAcesso|''");
	    	result.Add("TcsUsuarioPerfil|IdLinxPerfil");
	    	result.Add("TcsUsuarioPerfil|TCS_USUARIO_PERFIL.TCS_PERFIL.ID_LINX");
	    	//Add filtering disabled property for TCS_USUARIO_PERFIL
	    	string[] bmDisabledTcsUsuarioPerfilList = this.GetEDM().GetFilteringDisabledList("TCS_USUARIO_PERFIL");
	    	if (bmDisabledTcsUsuarioPerfilList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsUsuarioPerfilList.Contains("TCS_USUARIO_PERFIL.ID_TCS_USUARIO_PERFIL"))
	    		{
	    			result.Add("TcsUsuarioPerfil|IdTcsUsuarioPerfil");
	    			result.Add("TcsUsuarioPerfil|TCS_USUARIO_PERFIL.ID_TCS_USUARIO_PERFIL");
	    		}
	    	}
	    	return result;
	    }

	    #endregion Excluded Filters

	    #region Get By EntitySearchId Methods Definitions.
	
				
	    [Ignore]
	    //Get TcsUsuarioAutenticacao By EntitySearchId.
	    public IEnumerable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioAutenticacaoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioAutenticacaoAcesso By EntitySearchId.
	    public IEnumerable<TcsUsuarioAutenticacaoAcesso> GetTcsUsuarioAutenticacaoAcessoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioAutenticacaoAcessoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioAutenticacao By EntitySearchId.
	    public IEnumerable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioAutenticacaoAcesso By EntitySearchId.
	    public IEnumerable<TcsUsuarioAutenticacaoAcesso> GetTcsUsuarioAutenticacaoAcessoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioAutenticacaoAcessoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioAutenticacaoPerfil By EntitySearchId.
	    public IEnumerable<TcsUsuarioAutenticacaoPerfil> GetTcsUsuarioAutenticacaoPerfilByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioAutenticacaoPerfilByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioAutenticacaoPerfil By EntitySearchId.
	    public IEnumerable<TcsUsuarioAutenticacaoPerfil> GetTcsUsuarioAutenticacaoPerfilByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioAutenticacaoPerfilByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioPerfil By EntitySearchId.
	    public IQueryable<TcsUsuarioPerfil> GetTcsUsuarioPerfilByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioPerfilByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioPerfil By EntitySearchId.
	    public IQueryable<TcsUsuarioPerfil> GetTcsUsuarioPerfilByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioPerfilByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get UsuarioPerfilInfo By EntitySearchId.
	    public IEnumerable<UsuarioPerfilInfo> GetUsuarioPerfilInfoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetUsuarioPerfilInfoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get UsuarioPerfilInfo By EntitySearchId.
	    public IEnumerable<UsuarioPerfilInfo> GetUsuarioPerfilInfoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetUsuarioPerfilInfoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
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
			
	    //Get TcsUsuarioAutenticacaoAcesso By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioAutenticacaoAcesso> GetTcsUsuarioAutenticacaoAcessoByExample(TcsUsuarioAutenticacaoAcesso entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioAutenticacaoAcessoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsUsuarioAutenticacao By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoByExampleNoAssociations(TcsUsuarioAutenticacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsUsuarioAutenticacaoAcesso By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioAutenticacaoAcesso> GetTcsUsuarioAutenticacaoAcessoByExampleNoAssociations(TcsUsuarioAutenticacaoAcesso entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioAutenticacaoAcessoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsUsuarioAutenticacaoPerfil By Example.
	    [Ignore]
	    public IEnumerable<TcsUsuarioAutenticacaoPerfil> GetTcsUsuarioAutenticacaoPerfilByExample(TcsUsuarioAutenticacaoPerfil entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioAutenticacaoPerfilByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsUsuarioAutenticacaoPerfil By Example.
	    [Ignore]
	    public IEnumerable<TcsUsuarioAutenticacaoPerfil> GetTcsUsuarioAutenticacaoPerfilByExampleNoAssociations(TcsUsuarioAutenticacaoPerfil entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioAutenticacaoPerfilByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsUsuarioPerfil By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioPerfil> GetTcsUsuarioPerfilByExample(TcsUsuarioPerfil entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioPerfilByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsUsuarioPerfil By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioPerfil> GetTcsUsuarioPerfilByExampleNoAssociations(TcsUsuarioPerfil entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioPerfilByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get UsuarioPerfilInfo By Example.
	    [Ignore]
	    public IEnumerable<UsuarioPerfilInfo> GetUsuarioPerfilInfoByExample(UsuarioPerfilInfo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetUsuarioPerfilInfoByEntitySearch(queryAnalysis);
	    }
			
	    //Get UsuarioPerfilInfo By Example.
	    [Ignore]
	    public IEnumerable<UsuarioPerfilInfo> GetUsuarioPerfilInfoByExampleNoAssociations(UsuarioPerfilInfo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetUsuarioPerfilInfoByEntitySearchNoAssociations(queryAnalysis);
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
	    public TcsUsuarioAutenticacaoAcesso GetTcsUsuarioAutenticacaoAcessoByKey(Int32 idTcsUsuarioAcesso)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsUsuarioAutenticacaoAcesso");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsUsuarioAcesso"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsUsuarioAcesso));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsUsuarioAutenticacaoAcessoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsUsuarioAutenticacaoPerfil GetTcsUsuarioAutenticacaoPerfilByKey(Int32 idLinx, Int64 idTcsUsuarioPerfil)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsUsuarioAutenticacaoPerfil");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdLinx"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idLinx));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsUsuarioPerfil"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsUsuarioPerfil));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsUsuarioAutenticacaoPerfilByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsUsuarioPerfil GetTcsUsuarioPerfilByKey(Int64 idTcsUsuarioPerfil)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsUsuarioPerfil");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsUsuarioPerfil"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsUsuarioPerfil));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsUsuarioPerfilByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public UsuarioPerfilInfo GetUsuarioPerfilInfoByKey(Int64 idUsuario)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("UsuarioPerfilInfo");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuario"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idUsuario));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetUsuarioPerfilInfoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioAutenticacaoByEntitySearch.
	    public IQueryable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsUsuarioAutenticacao", "TcsUsuarioAutenticacao", 0, "AutenticacaoWindows#AutenticacaoWindows","Bairro#Bairro","Cep#Cep","CnpjCpf#CnpjCpf","Complemento#Complemento","ConfirmacaoUsuario#ConfirmacaoUsuario","ConfirmacaoUsuario1#ConfirmacaoUsuario1","CriaUsuario#CriaUsuario","DataAlteracao#DataAlteracao","DataCadastro#DataCadastro","DataExpiracaoSenha#DataExpiracaoSenha","Email#Email","FoneCelular#FoneCelular","FoneFixo#FoneFixo","GeraSenhaUsuario#GeraSenhaUsuario","IdLinx#IdLinx","IdUsuario#IdUsuario","Inativo#Inativo","IndicaUsuarioServico#IndicaUsuarioServico","InscrEstadualRg#InscrEstadualRg","Logradouro#Logradouro","LxPfjFisicaJuridica#LxPfjFisicaJuridica","LxTipoLogradouro#LxTipoLogradouro","Municipio#Municipio","NomeAutenticacao#NomeAutenticacao","NomeCurtoUsuario#NomeCurtoUsuario","NomeUsuario#NomeUsuario","Numero#Numero","ObsEndereco#ObsEndereco","Ramal#Ramal","Uf#Uf","UidUsuario#UidUsuario","VigenciaFinal#VigenciaFinal","VigenciaInicial#VigenciaInicial","IndicaAcessoSuporte#IndicaAcessoSuporte","NomeEmpresa#NomeEmpresa","UidEmpresa#UidEmpresa");
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsUsuarioAutenticacaoAcesso", "TcsUsuarioAutenticacaoAcessoP", 0, "DescricaoAmbiente#DescricaoAmbiente","DescricaoAmbienteRelacionado#DescricaoAmbienteRelacionado","DescricaoAplicacao#DescricaoAplicacao","DescricaoAplicativo#DescricaoAplicativo","IdAplicacao#IdAplicacao","IdLinx#IdLinx","IdTcsAmbiente#IdTcsAmbiente","IdTcsAmbienteRelacionado#IdTcsAmbienteRelacionado","IdTcsAplicativo#IdTcsAplicativo","IdTcsUsuarioAcesso#IdTcsUsuarioAcesso","IdUsuario#IdUsuario","IndicaAcessoPadrao#IndicaAcessoPadrao","NomeAutenticacao#NomeAutenticacao","NomeEmpresa#NomeEmpresa","NomeUsuario#NomeUsuario","Perfil#Perfil");
		
	        
             Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService serviceContext1 = new Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsUsuarioAutenticacao> result = 
	            (
                 from TcsUsuarioAutenticacao_Rep1 in serviceContext1.GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TcsUsuarioAutenticacao()		
	            {
	            
                AutenticacaoWindows = TcsUsuarioAutenticacao_Rep1.AutenticacaoWindows
                , Bairro = TcsUsuarioAutenticacao_Rep1.Bairro
                , Cep = TcsUsuarioAutenticacao_Rep1.Cep
                , CnpjCpf = TcsUsuarioAutenticacao_Rep1.CnpjCpf
                , Complemento = TcsUsuarioAutenticacao_Rep1.Complemento
                , ConfirmacaoUsuario = TcsUsuarioAutenticacao_Rep1.ConfirmacaoUsuario
                , ConfirmacaoUsuario1 = TcsUsuarioAutenticacao_Rep1.ConfirmacaoUsuario1
                , CriaUsuario = TcsUsuarioAutenticacao_Rep1.CriaUsuario
                , DataAlteracao = TcsUsuarioAutenticacao_Rep1.DataAlteracao
                , DataCadastro = TcsUsuarioAutenticacao_Rep1.DataCadastro
                , DataExpiracaoSenha = TcsUsuarioAutenticacao_Rep1.DataExpiracaoSenha
                , Email = TcsUsuarioAutenticacao_Rep1.Email
                , FoneCelular = TcsUsuarioAutenticacao_Rep1.FoneCelular
                , FoneFixo = TcsUsuarioAutenticacao_Rep1.FoneFixo
                , GeraSenhaUsuario = TcsUsuarioAutenticacao_Rep1.GeraSenhaUsuario
                , IdLinx = TcsUsuarioAutenticacao_Rep1.IdLinx
                , IdUsuario = TcsUsuarioAutenticacao_Rep1.IdUsuario
                , Inativo = TcsUsuarioAutenticacao_Rep1.Inativo
                , IndicaUsuarioServico = TcsUsuarioAutenticacao_Rep1.IndicaUsuarioServico
                , InscrEstadualRg = TcsUsuarioAutenticacao_Rep1.InscrEstadualRg
                , Logradouro = TcsUsuarioAutenticacao_Rep1.Logradouro
                , LxPfjFisicaJuridica = TcsUsuarioAutenticacao_Rep1.LxPfjFisicaJuridica
                , LxPfjFisicaJuridicaName = ((TcsUsuarioAutenticacao_Rep1.LxPfjFisicaJuridica) == 1 ? "Pessoa Física" : ((TcsUsuarioAutenticacao_Rep1.LxPfjFisicaJuridica) == 2 ? "Pessoa Jurídica" : ""))
                , LxTipoLogradouro = TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro
                , LxTipoLogradouroName = ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 1 ? "Aeroporto" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 2 ? "Alameda" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 3 ? "Apartamento" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 4 ? "Avenida" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 5 ? "Beco" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 6 ? "Bloco" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 7 ? "Caminho" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 8 ? "Escadinha" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 9 ? "Estação" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 10 ? "Estrada" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 11 ? "Fazenda" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 12 ? "Fortaleza" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 13 ? "Galeria" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 14 ? "Ladeira" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 15 ? "Largo" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 17 ? "Parque" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 16 ? "Praça" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 18 ? "Praia" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 19 ? "Quadra" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 20 ? "Quilômetro" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 21 ? "Quinta" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 22 ? "Rodovia" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 23 ? "Rua" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 24 ? "Super Quadra" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 25 ? "Travessa" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 26 ? "Viaduto" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 27 ? "Vila" : "")))))))))))))))))))))))))))
                , Municipio = TcsUsuarioAutenticacao_Rep1.Municipio
                , NomeAutenticacao = TcsUsuarioAutenticacao_Rep1.NomeAutenticacao
                , NomeCurtoUsuario = TcsUsuarioAutenticacao_Rep1.NomeCurtoUsuario
                , NomeUsuario = TcsUsuarioAutenticacao_Rep1.NomeUsuario
                , Numero = TcsUsuarioAutenticacao_Rep1.Numero
                , ObsEndereco = TcsUsuarioAutenticacao_Rep1.ObsEndereco
                , Ramal = TcsUsuarioAutenticacao_Rep1.Ramal
                , Uf = TcsUsuarioAutenticacao_Rep1.Uf
                , UidUsuario = TcsUsuarioAutenticacao_Rep1.UidUsuario
                , VigenciaFinal = TcsUsuarioAutenticacao_Rep1.VigenciaFinal
                , VigenciaInicial = TcsUsuarioAutenticacao_Rep1.VigenciaInicial
		
	            }
	            );
		
	
	        TcsUsuarioAutenticacao.OnSearching(ref result, false, entitySearchList);	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioAutenticacaoAcessoByEntitySearch.
	    public IQueryable<TcsUsuarioAutenticacaoAcesso> GetTcsUsuarioAutenticacaoAcessoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsUsuarioAutenticacaoAcesso", "TcsUsuarioAutenticacaoAcessoP", 0, "DescricaoAmbiente#DescricaoAmbiente","DescricaoAmbienteRelacionado#DescricaoAmbienteRelacionado","DescricaoAplicacao#DescricaoAplicacao","DescricaoAplicativo#DescricaoAplicativo","IdAplicacao#IdAplicacao","IdLinx#IdLinx","IdTcsAmbiente#IdTcsAmbiente","IdTcsAmbienteRelacionado#IdTcsAmbienteRelacionado","IdTcsAplicativo#IdTcsAplicativo","IdTcsUsuarioAcesso#IdTcsUsuarioAcesso","IdUsuario#IdUsuario","IndicaAcessoPadrao#IndicaAcessoPadrao","NomeAutenticacao#NomeAutenticacao","NomeEmpresa#NomeEmpresa","NomeUsuario#NomeUsuario","Perfil#Perfil");
		
	        
             Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService serviceContext1 = new Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsUsuarioAutenticacaoAcesso> result = 
	            (
                 from TcsUsuarioAutenticacaoAcessoP_Rep1 in serviceContext1.GetTcsUsuarioAutenticacaoAcessoPByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TcsUsuarioAutenticacaoAcesso()		
	            {
	            
                DescricaoAmbiente = TcsUsuarioAutenticacaoAcessoP_Rep1.DescricaoAmbiente
                , DescricaoAmbienteRelacionado = TcsUsuarioAutenticacaoAcessoP_Rep1.DescricaoAmbienteRelacionado
                , DescricaoAplicacao = TcsUsuarioAutenticacaoAcessoP_Rep1.DescricaoAplicacao
                , DescricaoAplicativo = TcsUsuarioAutenticacaoAcessoP_Rep1.DescricaoAplicativo
                , IdAplicacao = TcsUsuarioAutenticacaoAcessoP_Rep1.IdAplicacao
                , IdLinx = TcsUsuarioAutenticacaoAcessoP_Rep1.IdLinx
                , IdTcsAmbiente = TcsUsuarioAutenticacaoAcessoP_Rep1.IdTcsAmbiente
                , IdTcsAmbienteRelacionado = TcsUsuarioAutenticacaoAcessoP_Rep1.IdTcsAmbienteRelacionado
                , IdTcsAplicativo = TcsUsuarioAutenticacaoAcessoP_Rep1.IdTcsAplicativo
                , IdTcsUsuarioAcesso = TcsUsuarioAutenticacaoAcessoP_Rep1.IdTcsUsuarioAcesso
                , IdUsuario = TcsUsuarioAutenticacaoAcessoP_Rep1.IdUsuario
                , IndicaAcessoPadrao = TcsUsuarioAutenticacaoAcessoP_Rep1.IndicaAcessoPadrao
                , NomeAutenticacao = TcsUsuarioAutenticacaoAcessoP_Rep1.NomeAutenticacao
                , NomeEmpresa = TcsUsuarioAutenticacaoAcessoP_Rep1.NomeEmpresa
                , NomeUsuario = TcsUsuarioAutenticacaoAcessoP_Rep1.NomeUsuario
                , Perfil = TcsUsuarioAutenticacaoAcessoP_Rep1.Perfil
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioAutenticacaoByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsUsuarioAutenticacao", "TcsUsuarioAutenticacao", 0, "AutenticacaoWindows#AutenticacaoWindows","Bairro#Bairro","Cep#Cep","CnpjCpf#CnpjCpf","Complemento#Complemento","ConfirmacaoUsuario#ConfirmacaoUsuario","ConfirmacaoUsuario1#ConfirmacaoUsuario1","CriaUsuario#CriaUsuario","DataAlteracao#DataAlteracao","DataCadastro#DataCadastro","DataExpiracaoSenha#DataExpiracaoSenha","Email#Email","FoneCelular#FoneCelular","FoneFixo#FoneFixo","GeraSenhaUsuario#GeraSenhaUsuario","IdLinx#IdLinx","IdUsuario#IdUsuario","Inativo#Inativo","IndicaUsuarioServico#IndicaUsuarioServico","InscrEstadualRg#InscrEstadualRg","Logradouro#Logradouro","LxPfjFisicaJuridica#LxPfjFisicaJuridica","LxTipoLogradouro#LxTipoLogradouro","Municipio#Municipio","NomeAutenticacao#NomeAutenticacao","NomeCurtoUsuario#NomeCurtoUsuario","NomeUsuario#NomeUsuario","Numero#Numero","ObsEndereco#ObsEndereco","Ramal#Ramal","Uf#Uf","UidUsuario#UidUsuario","VigenciaFinal#VigenciaFinal","VigenciaInicial#VigenciaInicial","IndicaAcessoSuporte#IndicaAcessoSuporte","NomeEmpresa#NomeEmpresa","UidEmpresa#UidEmpresa");
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsUsuarioAutenticacaoAcesso", "TcsUsuarioAutenticacaoAcessoP", 0, "DescricaoAmbiente#DescricaoAmbiente","DescricaoAmbienteRelacionado#DescricaoAmbienteRelacionado","DescricaoAplicacao#DescricaoAplicacao","DescricaoAplicativo#DescricaoAplicativo","IdAplicacao#IdAplicacao","IdLinx#IdLinx","IdTcsAmbiente#IdTcsAmbiente","IdTcsAmbienteRelacionado#IdTcsAmbienteRelacionado","IdTcsAplicativo#IdTcsAplicativo","IdTcsUsuarioAcesso#IdTcsUsuarioAcesso","IdUsuario#IdUsuario","IndicaAcessoPadrao#IndicaAcessoPadrao","NomeAutenticacao#NomeAutenticacao","NomeEmpresa#NomeEmpresa","NomeUsuario#NomeUsuario","Perfil#Perfil");
		
	        
             Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService serviceContext1 = new Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsUsuarioAutenticacao> result = 
	            (
                 from TcsUsuarioAutenticacao_Rep1 in serviceContext1.GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TcsUsuarioAutenticacao()		
	            {
	            
                AutenticacaoWindows = TcsUsuarioAutenticacao_Rep1.AutenticacaoWindows
                , Bairro = TcsUsuarioAutenticacao_Rep1.Bairro
                , Cep = TcsUsuarioAutenticacao_Rep1.Cep
                , CnpjCpf = TcsUsuarioAutenticacao_Rep1.CnpjCpf
                , Complemento = TcsUsuarioAutenticacao_Rep1.Complemento
                , ConfirmacaoUsuario = TcsUsuarioAutenticacao_Rep1.ConfirmacaoUsuario
                , ConfirmacaoUsuario1 = TcsUsuarioAutenticacao_Rep1.ConfirmacaoUsuario1
                , CriaUsuario = TcsUsuarioAutenticacao_Rep1.CriaUsuario
                , DataAlteracao = TcsUsuarioAutenticacao_Rep1.DataAlteracao
                , DataCadastro = TcsUsuarioAutenticacao_Rep1.DataCadastro
                , DataExpiracaoSenha = TcsUsuarioAutenticacao_Rep1.DataExpiracaoSenha
                , Email = TcsUsuarioAutenticacao_Rep1.Email
                , FoneCelular = TcsUsuarioAutenticacao_Rep1.FoneCelular
                , FoneFixo = TcsUsuarioAutenticacao_Rep1.FoneFixo
                , GeraSenhaUsuario = TcsUsuarioAutenticacao_Rep1.GeraSenhaUsuario
                , IdLinx = TcsUsuarioAutenticacao_Rep1.IdLinx
                , IdUsuario = TcsUsuarioAutenticacao_Rep1.IdUsuario
                , Inativo = TcsUsuarioAutenticacao_Rep1.Inativo
                , IndicaUsuarioServico = TcsUsuarioAutenticacao_Rep1.IndicaUsuarioServico
                , InscrEstadualRg = TcsUsuarioAutenticacao_Rep1.InscrEstadualRg
                , Logradouro = TcsUsuarioAutenticacao_Rep1.Logradouro
                , LxPfjFisicaJuridica = TcsUsuarioAutenticacao_Rep1.LxPfjFisicaJuridica
                , LxPfjFisicaJuridicaName = ((TcsUsuarioAutenticacao_Rep1.LxPfjFisicaJuridica) == 1 ? "Pessoa Física" : ((TcsUsuarioAutenticacao_Rep1.LxPfjFisicaJuridica) == 2 ? "Pessoa Jurídica" : ""))
                , LxTipoLogradouro = TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro
                , LxTipoLogradouroName = ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 1 ? "Aeroporto" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 2 ? "Alameda" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 3 ? "Apartamento" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 4 ? "Avenida" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 5 ? "Beco" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 6 ? "Bloco" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 7 ? "Caminho" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 8 ? "Escadinha" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 9 ? "Estação" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 10 ? "Estrada" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 11 ? "Fazenda" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 12 ? "Fortaleza" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 13 ? "Galeria" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 14 ? "Ladeira" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 15 ? "Largo" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 17 ? "Parque" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 16 ? "Praça" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 18 ? "Praia" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 19 ? "Quadra" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 20 ? "Quilômetro" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 21 ? "Quinta" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 22 ? "Rodovia" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 23 ? "Rua" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 24 ? "Super Quadra" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 25 ? "Travessa" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 26 ? "Viaduto" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 27 ? "Vila" : "")))))))))))))))))))))))))))
                , Municipio = TcsUsuarioAutenticacao_Rep1.Municipio
                , NomeAutenticacao = TcsUsuarioAutenticacao_Rep1.NomeAutenticacao
                , NomeCurtoUsuario = TcsUsuarioAutenticacao_Rep1.NomeCurtoUsuario
                , NomeUsuario = TcsUsuarioAutenticacao_Rep1.NomeUsuario
                , Numero = TcsUsuarioAutenticacao_Rep1.Numero
                , ObsEndereco = TcsUsuarioAutenticacao_Rep1.ObsEndereco
                , Ramal = TcsUsuarioAutenticacao_Rep1.Ramal
                , Uf = TcsUsuarioAutenticacao_Rep1.Uf
                , UidUsuario = TcsUsuarioAutenticacao_Rep1.UidUsuario
                , VigenciaFinal = TcsUsuarioAutenticacao_Rep1.VigenciaFinal
                , VigenciaInicial = TcsUsuarioAutenticacao_Rep1.VigenciaInicial
		
	            }
	            );
		
	
	        TcsUsuarioAutenticacao.OnSearching(ref result, true, entitySearchList);	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioAutenticacaoAcessoByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioAutenticacaoAcesso> GetTcsUsuarioAutenticacaoAcessoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsUsuarioAutenticacaoAcesso", "TcsUsuarioAutenticacaoAcessoP", 0, "DescricaoAmbiente#DescricaoAmbiente","DescricaoAmbienteRelacionado#DescricaoAmbienteRelacionado","DescricaoAplicacao#DescricaoAplicacao","DescricaoAplicativo#DescricaoAplicativo","IdAplicacao#IdAplicacao","IdLinx#IdLinx","IdTcsAmbiente#IdTcsAmbiente","IdTcsAmbienteRelacionado#IdTcsAmbienteRelacionado","IdTcsAplicativo#IdTcsAplicativo","IdTcsUsuarioAcesso#IdTcsUsuarioAcesso","IdUsuario#IdUsuario","IndicaAcessoPadrao#IndicaAcessoPadrao","NomeAutenticacao#NomeAutenticacao","NomeEmpresa#NomeEmpresa","NomeUsuario#NomeUsuario","Perfil#Perfil");
		
	        
             Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService serviceContext1 = new Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsUsuarioAutenticacaoAcesso> result = 
	            (
                 from TcsUsuarioAutenticacaoAcessoP_Rep1 in serviceContext1.GetTcsUsuarioAutenticacaoAcessoPByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TcsUsuarioAutenticacaoAcesso()		
	            {
	            
                DescricaoAmbiente = TcsUsuarioAutenticacaoAcessoP_Rep1.DescricaoAmbiente
                , DescricaoAmbienteRelacionado = TcsUsuarioAutenticacaoAcessoP_Rep1.DescricaoAmbienteRelacionado
                , DescricaoAplicacao = TcsUsuarioAutenticacaoAcessoP_Rep1.DescricaoAplicacao
                , DescricaoAplicativo = TcsUsuarioAutenticacaoAcessoP_Rep1.DescricaoAplicativo
                , IdAplicacao = TcsUsuarioAutenticacaoAcessoP_Rep1.IdAplicacao
                , IdLinx = TcsUsuarioAutenticacaoAcessoP_Rep1.IdLinx
                , IdTcsAmbiente = TcsUsuarioAutenticacaoAcessoP_Rep1.IdTcsAmbiente
                , IdTcsAmbienteRelacionado = TcsUsuarioAutenticacaoAcessoP_Rep1.IdTcsAmbienteRelacionado
                , IdTcsAplicativo = TcsUsuarioAutenticacaoAcessoP_Rep1.IdTcsAplicativo
                , IdTcsUsuarioAcesso = TcsUsuarioAutenticacaoAcessoP_Rep1.IdTcsUsuarioAcesso
                , IdUsuario = TcsUsuarioAutenticacaoAcessoP_Rep1.IdUsuario
                , IndicaAcessoPadrao = TcsUsuarioAutenticacaoAcessoP_Rep1.IndicaAcessoPadrao
                , NomeAutenticacao = TcsUsuarioAutenticacaoAcessoP_Rep1.NomeAutenticacao
                , NomeEmpresa = TcsUsuarioAutenticacaoAcessoP_Rep1.NomeEmpresa
                , NomeUsuario = TcsUsuarioAutenticacaoAcessoP_Rep1.NomeUsuario
                , Perfil = TcsUsuarioAutenticacaoAcessoP_Rep1.Perfil
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioAutenticacaoPerfilByEntitySearch.
	    public IEnumerable<TcsUsuarioAutenticacaoPerfil> GetTcsUsuarioAutenticacaoPerfilByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		

	        IEnumerable<TcsUsuarioAutenticacaoPerfil> result = 
	            (from entity0 in TcsUsuarioAutenticacaoPerfil.OnSearchingReplacement(entitySearchList) select entity0);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioAutenticacaoPerfilByEntitySearchNoAssociations.
	    public IEnumerable<TcsUsuarioAutenticacaoPerfil> GetTcsUsuarioAutenticacaoPerfilByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		

	        IEnumerable<TcsUsuarioAutenticacaoPerfil> result = 
	            (from entity0 in TcsUsuarioAutenticacaoPerfil.OnSearchingReplacement(entitySearchList) select entity0);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioPerfilByEntitySearch.
	    public IQueryable<TcsUsuarioPerfil> GetTcsUsuarioPerfilByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioPerfil));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioPerfil> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_PERFIL.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_PERFIL
                  let entity0Al2 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioPerfil()		
	            {
	            
                DescPerfil = entity0Al1.DESC_PERFIL
                , IdLinxPerfil = entity0Al1.ID_LINX
                , IdPerfil = entity0Al1.ID_PERFIL
                , IdTcsUsuarioPerfil = entity0.ID_TCS_USUARIO_PERFIL
                , IdUsuario = entity0Al2.ID_USUARIO
                , Inativo = entity0Al1.INATIVO
                , NomeUsuario = entity0Al2.NOME_USUARIO
		
	            }
	            );
	
	        SetTcsUsuarioPerfilBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioPerfilByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioPerfil> GetTcsUsuarioPerfilByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioPerfil));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioPerfil> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_PERFIL.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_PERFIL
                  let entity0Al2 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsUsuarioPerfil()		
	            {
	            
                DescPerfil = entity0Al1.DESC_PERFIL
                , IdLinxPerfil = entity0Al1.ID_LINX
                , IdPerfil = entity0Al1.ID_PERFIL
                , IdTcsUsuarioPerfil = entity0.ID_TCS_USUARIO_PERFIL
                , IdUsuario = entity0Al2.ID_USUARIO
                , Inativo = entity0Al1.INATIVO
                , NomeUsuario = entity0Al2.NOME_USUARIO
		
	            }
	            );
	
	        SetTcsUsuarioPerfilBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
	
	    [Ignore()]
	    private void SetTcsUsuarioPerfilBusinessFilter(ref IQueryable<TcsUsuarioPerfil> query, List<EntitySearch> entitySearchList)
	    {
	    		int idxElement;
	    		string operatorValue;
	    		object value;
	    		//Get query by functions
	    		if (entitySearchList.Count > 0)
	    		{
	    			foreach (EntitySearch search in entitySearchList.Where(e => e.EntityName == "TcsUsuarioPerfil"))
	    			{

	
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "IdLinxPerfil" || e.Value.ToString() == "TCS_USUARIO_PERFIL.TCS_PERFIL.ID_LINX")))
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
	    										int tmpIdLinxPerfil1 = (int)value;
	    										query = from r in query where r.IdLinxPerfil == tmpIdLinxPerfil1 select r;
	    										break;
	    									case "!=":
	    										int tmpIdLinxPerfil2 = (int)value;
	    										query = from r in query where r.IdLinxPerfil != tmpIdLinxPerfil2 select r;
	    										break;

	
	    									case "<":
	    										int tmpIdLinxPerfil3 = (int)value;
	    										query = from r in query where r.IdLinxPerfil < tmpIdLinxPerfil3 select r;
	    										break;
	    									case "<=":
	    										int tmpIdLinxPerfil4 = (int)value;
	    										query = from r in query where r.IdLinxPerfil <= tmpIdLinxPerfil4 select r;
	    										break;
	    									case ">":
	    										int tmpIdLinxPerfil5 = (int)value;
	    										query = from r in query where r.IdLinxPerfil > tmpIdLinxPerfil5 select r;
	    										break;
	    									case ">=":
	    										int tmpIdLinxPerfil6 = (int)value;
	    										query = from r in query where r.IdLinxPerfil >= tmpIdLinxPerfil6 select r;
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
	    //Get UsuarioPerfilInfoByEntitySearch.
	    public IEnumerable<UsuarioPerfilInfo> GetUsuarioPerfilInfoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<UsuarioPerfilInfo> result = new List<UsuarioPerfilInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get UsuarioPerfilInfoByEntitySearchNoAssociations.
	    public IEnumerable<UsuarioPerfilInfo> GetUsuarioPerfilInfoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<UsuarioPerfilInfo> result = new List<UsuarioPerfilInfo>();
	  	
	
	        	

	
	        return result;
	    }
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get PagedTcsUsuarioAutenticacao.
	    public IQueryable<TcsUsuarioAutenticacao> GetPagedTcsUsuarioAutenticacao(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsUsuarioAutenticacao", "TcsUsuarioAutenticacao", 0, "AutenticacaoWindows#AutenticacaoWindows","Bairro#Bairro","Cep#Cep","CnpjCpf#CnpjCpf","Complemento#Complemento","ConfirmacaoUsuario#ConfirmacaoUsuario","ConfirmacaoUsuario1#ConfirmacaoUsuario1","CriaUsuario#CriaUsuario","DataAlteracao#DataAlteracao","DataCadastro#DataCadastro","DataExpiracaoSenha#DataExpiracaoSenha","Email#Email","FoneCelular#FoneCelular","FoneFixo#FoneFixo","GeraSenhaUsuario#GeraSenhaUsuario","IdLinx#IdLinx","IdUsuario#IdUsuario","Inativo#Inativo","IndicaUsuarioServico#IndicaUsuarioServico","InscrEstadualRg#InscrEstadualRg","Logradouro#Logradouro","LxPfjFisicaJuridica#LxPfjFisicaJuridica","LxTipoLogradouro#LxTipoLogradouro","Municipio#Municipio","NomeAutenticacao#NomeAutenticacao","NomeCurtoUsuario#NomeCurtoUsuario","NomeUsuario#NomeUsuario","Numero#Numero","ObsEndereco#ObsEndereco","Ramal#Ramal","Uf#Uf","UidUsuario#UidUsuario","VigenciaFinal#VigenciaFinal","VigenciaInicial#VigenciaInicial","IndicaAcessoSuporte#IndicaAcessoSuporte","NomeEmpresa#NomeEmpresa","UidEmpresa#UidEmpresa");
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsUsuarioAutenticacaoAcesso", "TcsUsuarioAutenticacaoAcessoP", 0, "DescricaoAmbiente#DescricaoAmbiente","DescricaoAmbienteRelacionado#DescricaoAmbienteRelacionado","DescricaoAplicacao#DescricaoAplicacao","DescricaoAplicativo#DescricaoAplicativo","IdAplicacao#IdAplicacao","IdLinx#IdLinx","IdTcsAmbiente#IdTcsAmbiente","IdTcsAmbienteRelacionado#IdTcsAmbienteRelacionado","IdTcsAplicativo#IdTcsAplicativo","IdTcsUsuarioAcesso#IdTcsUsuarioAcesso","IdUsuario#IdUsuario","IndicaAcessoPadrao#IndicaAcessoPadrao","NomeAutenticacao#NomeAutenticacao","NomeEmpresa#NomeEmpresa","NomeUsuario#NomeUsuario","Perfil#Perfil");
		
	        
             Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService serviceContext1 = new Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsUsuarioAutenticacao> result = 
	            (
                 from TcsUsuarioAutenticacao_Rep1 in serviceContext1.GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(repSerializedEntitySearch)
                orderby TcsUsuarioAutenticacao_Rep1.IdUsuario ascending
	            
	            	
	            select new TcsUsuarioAutenticacao()		
	            {
	            
                AutenticacaoWindows = TcsUsuarioAutenticacao_Rep1.AutenticacaoWindows
                , Bairro = TcsUsuarioAutenticacao_Rep1.Bairro
                , Cep = TcsUsuarioAutenticacao_Rep1.Cep
                , CnpjCpf = TcsUsuarioAutenticacao_Rep1.CnpjCpf
                , Complemento = TcsUsuarioAutenticacao_Rep1.Complemento
                , ConfirmacaoUsuario = TcsUsuarioAutenticacao_Rep1.ConfirmacaoUsuario
                , ConfirmacaoUsuario1 = TcsUsuarioAutenticacao_Rep1.ConfirmacaoUsuario1
                , CriaUsuario = TcsUsuarioAutenticacao_Rep1.CriaUsuario
                , DataAlteracao = TcsUsuarioAutenticacao_Rep1.DataAlteracao
                , DataCadastro = TcsUsuarioAutenticacao_Rep1.DataCadastro
                , DataExpiracaoSenha = TcsUsuarioAutenticacao_Rep1.DataExpiracaoSenha
                , Email = TcsUsuarioAutenticacao_Rep1.Email
                , FoneCelular = TcsUsuarioAutenticacao_Rep1.FoneCelular
                , FoneFixo = TcsUsuarioAutenticacao_Rep1.FoneFixo
                , GeraSenhaUsuario = TcsUsuarioAutenticacao_Rep1.GeraSenhaUsuario
                , IdLinx = TcsUsuarioAutenticacao_Rep1.IdLinx
                , IdUsuario = TcsUsuarioAutenticacao_Rep1.IdUsuario
                , Inativo = TcsUsuarioAutenticacao_Rep1.Inativo
                , IndicaUsuarioServico = TcsUsuarioAutenticacao_Rep1.IndicaUsuarioServico
                , InscrEstadualRg = TcsUsuarioAutenticacao_Rep1.InscrEstadualRg
                , Logradouro = TcsUsuarioAutenticacao_Rep1.Logradouro
                , LxPfjFisicaJuridica = TcsUsuarioAutenticacao_Rep1.LxPfjFisicaJuridica
                , LxPfjFisicaJuridicaName = ((TcsUsuarioAutenticacao_Rep1.LxPfjFisicaJuridica) == 1 ? "Pessoa Física" : ((TcsUsuarioAutenticacao_Rep1.LxPfjFisicaJuridica) == 2 ? "Pessoa Jurídica" : ""))
                , LxTipoLogradouro = TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro
                , LxTipoLogradouroName = ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 1 ? "Aeroporto" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 2 ? "Alameda" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 3 ? "Apartamento" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 4 ? "Avenida" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 5 ? "Beco" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 6 ? "Bloco" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 7 ? "Caminho" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 8 ? "Escadinha" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 9 ? "Estação" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 10 ? "Estrada" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 11 ? "Fazenda" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 12 ? "Fortaleza" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 13 ? "Galeria" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 14 ? "Ladeira" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 15 ? "Largo" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 17 ? "Parque" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 16 ? "Praça" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 18 ? "Praia" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 19 ? "Quadra" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 20 ? "Quilômetro" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 21 ? "Quinta" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 22 ? "Rodovia" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 23 ? "Rua" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 24 ? "Super Quadra" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 25 ? "Travessa" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 26 ? "Viaduto" : ((TcsUsuarioAutenticacao_Rep1.LxTipoLogradouro) == 27 ? "Vila" : "")))))))))))))))))))))))))))
                , Municipio = TcsUsuarioAutenticacao_Rep1.Municipio
                , NomeAutenticacao = TcsUsuarioAutenticacao_Rep1.NomeAutenticacao
                , NomeCurtoUsuario = TcsUsuarioAutenticacao_Rep1.NomeCurtoUsuario
                , NomeUsuario = TcsUsuarioAutenticacao_Rep1.NomeUsuario
                , Numero = TcsUsuarioAutenticacao_Rep1.Numero
                , ObsEndereco = TcsUsuarioAutenticacao_Rep1.ObsEndereco
                , Ramal = TcsUsuarioAutenticacao_Rep1.Ramal
                , Uf = TcsUsuarioAutenticacao_Rep1.Uf
                , UidUsuario = TcsUsuarioAutenticacao_Rep1.UidUsuario
                , VigenciaFinal = TcsUsuarioAutenticacao_Rep1.VigenciaFinal
                , VigenciaInicial = TcsUsuarioAutenticacao_Rep1.VigenciaInicial
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        TcsUsuarioAutenticacao.OnSearching(ref result, true, entitySearchList);	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsUsuarioAutenticacaoAcesso.
	    public IQueryable<TcsUsuarioAutenticacaoAcesso> GetPagedTcsUsuarioAutenticacaoAcesso(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsUsuarioAutenticacaoAcesso", "TcsUsuarioAutenticacaoAcessoP", 0, "DescricaoAmbiente#DescricaoAmbiente","DescricaoAmbienteRelacionado#DescricaoAmbienteRelacionado","DescricaoAplicacao#DescricaoAplicacao","DescricaoAplicativo#DescricaoAplicativo","IdAplicacao#IdAplicacao","IdLinx#IdLinx","IdTcsAmbiente#IdTcsAmbiente","IdTcsAmbienteRelacionado#IdTcsAmbienteRelacionado","IdTcsAplicativo#IdTcsAplicativo","IdTcsUsuarioAcesso#IdTcsUsuarioAcesso","IdUsuario#IdUsuario","IndicaAcessoPadrao#IndicaAcessoPadrao","NomeAutenticacao#NomeAutenticacao","NomeEmpresa#NomeEmpresa","NomeUsuario#NomeUsuario","Perfil#Perfil");
		
	        
             Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService serviceContext1 = new Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsUsuarioAutenticacaoAcesso> result = 
	            (
                 from TcsUsuarioAutenticacaoAcessoP_Rep1 in serviceContext1.GetTcsUsuarioAutenticacaoAcessoPByEntitySearchNoAssociations(repSerializedEntitySearch)
                orderby TcsUsuarioAutenticacaoAcessoP_Rep1.IdTcsUsuarioAcesso ascending
	            
	            	
	            select new TcsUsuarioAutenticacaoAcesso()		
	            {
	            
                DescricaoAmbiente = TcsUsuarioAutenticacaoAcessoP_Rep1.DescricaoAmbiente
                , DescricaoAmbienteRelacionado = TcsUsuarioAutenticacaoAcessoP_Rep1.DescricaoAmbienteRelacionado
                , DescricaoAplicacao = TcsUsuarioAutenticacaoAcessoP_Rep1.DescricaoAplicacao
                , DescricaoAplicativo = TcsUsuarioAutenticacaoAcessoP_Rep1.DescricaoAplicativo
                , IdAplicacao = TcsUsuarioAutenticacaoAcessoP_Rep1.IdAplicacao
                , IdLinx = TcsUsuarioAutenticacaoAcessoP_Rep1.IdLinx
                , IdTcsAmbiente = TcsUsuarioAutenticacaoAcessoP_Rep1.IdTcsAmbiente
                , IdTcsAmbienteRelacionado = TcsUsuarioAutenticacaoAcessoP_Rep1.IdTcsAmbienteRelacionado
                , IdTcsAplicativo = TcsUsuarioAutenticacaoAcessoP_Rep1.IdTcsAplicativo
                , IdTcsUsuarioAcesso = TcsUsuarioAutenticacaoAcessoP_Rep1.IdTcsUsuarioAcesso
                , IdUsuario = TcsUsuarioAutenticacaoAcessoP_Rep1.IdUsuario
                , IndicaAcessoPadrao = TcsUsuarioAutenticacaoAcessoP_Rep1.IndicaAcessoPadrao
                , NomeAutenticacao = TcsUsuarioAutenticacaoAcessoP_Rep1.NomeAutenticacao
                , NomeEmpresa = TcsUsuarioAutenticacaoAcessoP_Rep1.NomeEmpresa
                , NomeUsuario = TcsUsuarioAutenticacaoAcessoP_Rep1.NomeUsuario
                , Perfil = TcsUsuarioAutenticacaoAcessoP_Rep1.Perfil
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsUsuarioAutenticacaoCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
		
	    [Ignore]
	    public int GetTcsUsuarioAutenticacaoAcessoCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsUsuarioAutenticacaoPerfil.
	    public IEnumerable<TcsUsuarioAutenticacaoPerfil> GetPagedTcsUsuarioAutenticacaoPerfil(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		

	        IEnumerable<TcsUsuarioAutenticacaoPerfil> result = 
	            (from entity0 in TcsUsuarioAutenticacaoPerfil.OnSearchingReplacement(entitySearchList) select entity0);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsUsuarioAutenticacaoPerfilCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsUsuarioPerfil.
	    public IQueryable<TcsUsuarioPerfil> GetPagedTcsUsuarioPerfil(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioPerfil));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioPerfil> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_PERFIL.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_PERFIL
                  let entity0Al2 = entity0.TCS_USUARIO
                orderby entity0.ID_TCS_USUARIO_PERFIL ascending
	            
	            	
	            select new TcsUsuarioPerfil()		
	            {
	            
                DescPerfil = entity0Al1.DESC_PERFIL
                , IdLinxPerfil = entity0Al1.ID_LINX
                , IdPerfil = entity0Al1.ID_PERFIL
                , IdTcsUsuarioPerfil = entity0.ID_TCS_USUARIO_PERFIL
                , IdUsuario = entity0Al2.ID_USUARIO
                , Inativo = entity0Al1.INATIVO
                , NomeUsuario = entity0Al2.NOME_USUARIO
		
	            }
	            ).Skip(skip).Take(take);
	
	        SetTcsUsuarioPerfilBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsUsuarioPerfilCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioPerfil));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_USUARIO_PERFIL.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_PERFIL
                  let entityAl2 = entity.TCS_USUARIO
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedUsuarioPerfilInfo.
	    public IEnumerable<UsuarioPerfilInfo> GetPagedUsuarioPerfilInfo(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<UsuarioPerfilInfo> result = new List<UsuarioPerfilInfo>();
	  	
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetUsuarioPerfilInfoCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    
	    [Update()]	
	    //Update TcsUsuarioAutenticacao.
	    public void UpdateTcsUsuarioAutenticacao(TcsUsuarioAutenticacao entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert TcsUsuarioAutenticacao.
	    public void InsertTcsUsuarioAutenticacao(TcsUsuarioAutenticacao entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete TcsUsuarioAutenticacao.
	    public void DeleteTcsUsuarioAutenticacao(TcsUsuarioAutenticacao entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsUsuarioAutenticacaoAcesso.
	    public void UpdateTcsUsuarioAutenticacaoAcesso(TcsUsuarioAutenticacaoAcesso entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert TcsUsuarioAutenticacaoAcesso.
	    public void InsertTcsUsuarioAutenticacaoAcesso(TcsUsuarioAutenticacaoAcesso entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete TcsUsuarioAutenticacaoAcesso.
	    public void DeleteTcsUsuarioAutenticacaoAcesso(TcsUsuarioAutenticacaoAcesso entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsUsuarioAutenticacaoPerfil.
	    public void UpdateTcsUsuarioAutenticacaoPerfil(TcsUsuarioAutenticacaoPerfil entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert TcsUsuarioAutenticacaoPerfil.
	    public void InsertTcsUsuarioAutenticacaoPerfil(TcsUsuarioAutenticacaoPerfil entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete TcsUsuarioAutenticacaoPerfil.
	    public void DeleteTcsUsuarioAutenticacaoPerfil(TcsUsuarioAutenticacaoPerfil entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsUsuarioPerfil.
	    public void UpdateTcsUsuarioPerfil(TcsUsuarioPerfil entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsUsuarioPerfil.
	    public void InsertTcsUsuarioPerfil(TcsUsuarioPerfil entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsUsuarioPerfil.
	    public void DeleteTcsUsuarioPerfil(TcsUsuarioPerfil entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update UsuarioPerfilInfo.
	    public void UpdateUsuarioPerfilInfo(UsuarioPerfilInfo entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert UsuarioPerfilInfo.
	    public void InsertUsuarioPerfilInfo(UsuarioPerfilInfo entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete UsuarioPerfilInfo.
	    public void DeleteUsuarioPerfilInfo(UsuarioPerfilInfo entity)
	    {



	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}