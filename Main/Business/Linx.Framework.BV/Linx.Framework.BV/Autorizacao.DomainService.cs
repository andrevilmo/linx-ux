					
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

namespace Linx.Framework.BV.Autorizacao
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="Acesso.EntityUniqueKey", IsUpdatable=false, EdmName="")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[Acesso];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[];EdmEntityName[];EntityRelations[ ];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "Acesso")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Autorizacao.Acesso")]
	public partial class Acesso : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For IdTcsAmbiente
	    partial void OnIdTcsAmbienteChanging(int value);
	    partial void OnIdTcsAmbienteChanged();

	    private int _IdTcsAmbiente;

	    [DataMember(IsRequired = true, Name = "IdTcsAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
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
	    //Extensibility Partial Method Definitions For Token
	    partial void OnTokenChanging(System.Guid value);
	    partial void OnTokenChanged();

	    private System.Guid _Token;

	    [DataMember(IsRequired = true, Name = "Token", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public System.Guid Token
	    {
	    	    get
	    	    {
	    	          return _Token;
	    	    }
	    	    set
	    	    {
	    	          if (this._Token != value)
	    	          {
	    	              this.ValidateProperty("Token", value);
	    	              this.OnTokenChanging(value);
	    	              this.RaiseDataMemberChanging("Token");
	    	              this._Token = value;
	    	              this.RaiseDataMemberChanged("Token");
	    	              this.OnTokenChanged();
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
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
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
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
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
	    //Extensibility Partial Method Definitions For IdAmbienteRelacionado
	    partial void OnIdAmbienteRelacionadoChanging(int? value);
	    partial void OnIdAmbienteRelacionadoChanged();

	    private int? _IdAmbienteRelacionado;

	    [DataMember(IsRequired = true, Name = "IdAmbienteRelacionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public int? IdAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _IdAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdAmbienteRelacionado != value)
	    	          {
	    	              this.ValidateProperty("IdAmbienteRelacionado", value);
	    	              this.OnIdAmbienteRelacionadoChanging(value);
	    	              this.RaiseDataMemberChanging("IdAmbienteRelacionado");
	    	              this._IdAmbienteRelacionado = value;
	    	              this.RaiseDataMemberChanged("IdAmbienteRelacionado");
	    	              this.OnIdAmbienteRelacionadoChanged();
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

		

	[LinxPublicationView(PrimaryKeys="UsuarioAcesso.EntityUniqueKey", IsUpdatable=false, EdmName="")]
		
	[DataContract(IsReference = false, Name = "UsuarioAcesso")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Autorizacao.UsuarioAcesso")]
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
	 


	    private Guid _UidUsuario;

	    [DataMember(Name = "UidUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
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

	    private int _IdAmbiente;

	    [DataMember(Name = "IdAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public int IdAmbiente
	    {
	    	    get
	    	    {
	    	          return _IdAmbiente;
	    	    }
	    	    set
	    	    {
	    	          this._IdAmbiente = value;
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
	    [Key()]
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
	    [Key()]
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
	    [Key()]
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

	    private string _DescricaoGrupoEconomico;

	    [DataMember(Name = "DescricaoGrupoEconomico", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string DescricaoGrupoEconomico
	    {
	    	    get
	    	    {
	    	          return _DescricaoGrupoEconomico;
	    	    }
	    	    set
	    	    {
	    	          this._DescricaoGrupoEconomico = value;
	    	    }
	    }

	    private Guid _UidGrupoAcesso;

	    [DataMember(Name = "UidGrupoAcesso", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public Guid UidGrupoAcesso
	    {
	    	    get
	    	    {
	    	          return _UidGrupoAcesso;
	    	    }
	    	    set
	    	    {
	    	          this._UidGrupoAcesso = value;
	    	    }
	    }

	    private string _DescricaoGrupoAcesso;

	    [DataMember(Name = "DescricaoGrupoAcesso", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string DescricaoGrupoAcesso
	    {
	    	    get
	    	    {
	    	          return _DescricaoGrupoAcesso;
	    	    }
	    	    set
	    	    {
	    	          this._DescricaoGrupoAcesso = value;
	    	    }
	    }

	    private string _UrlAplicacao;

	    [DataMember(Name = "UrlAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string UrlAplicacao
	    {
	    	    get
	    	    {
	    	          return _UrlAplicacao;
	    	    }
	    	    set
	    	    {
	    	          this._UrlAplicacao = value;
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

		

	[LinxPublicationView(PrimaryKeys="UserInfo.EntityUniqueKey", IsUpdatable=false, EdmName="")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[UserInfo];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[UidUsuario];ReadOnly[false];Entities[:UidUsuario];SubQueryInfo[];EdmEntityName[];EntityRelations[ ];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "UserInfo")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Autorizacao.UserInfo")]
	public partial class UserInfo : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For UidUsuario
	    partial void OnUidUsuarioChanging(Guid value);
	    partial void OnUidUsuarioChanged();

	    private Guid _UidUsuario;

	    [DataMember(IsRequired = true, Name = "UidUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
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
	    //Extensibility Partial Method Definitions For Inativo
	    partial void OnInativoChanging(bool value);
	    partial void OnInativoChanged();

	    private bool _Inativo;

	    [DataMember(IsRequired = true, Name = "Inativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
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
	    //Extensibility Partial Method Definitions For VigenciaInicial
	    partial void OnVigenciaInicialChanging(DateTime value);
	    partial void OnVigenciaInicialChanged();

	    private DateTime _VigenciaInicial;

	    [DataMember(IsRequired = true, Name = "VigenciaInicial", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
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
	    //Extensibility Partial Method Definitions For VigenciaFinal
	    partial void OnVigenciaFinalChanging(DateTime value);
	    partial void OnVigenciaFinalChanged();

	    private DateTime _VigenciaFinal;

	    [DataMember(IsRequired = true, Name = "VigenciaFinal", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
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
	    //Extensibility Partial Method Definitions For IdUsuario
	    partial void OnIdUsuarioChanging(Int64 value);
	    partial void OnIdUsuarioChanged();

	    private Int64 _IdUsuario;

	    [DataMember(IsRequired = true, Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
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
	    //Extensibility Partial Method Definitions For NomeUsuario
	    partial void OnNomeUsuarioChanging(string value);
	    partial void OnNomeUsuarioChanged();

	    private string _NomeUsuario;

	    [DataMember(IsRequired = true, Name = "NomeUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
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
	    //Extensibility Partial Method Definitions For NomeAutenticacao
	    partial void OnNomeAutenticacaoChanging(string value);
	    partial void OnNomeAutenticacaoChanged();

	    private string _NomeAutenticacao;

	    [DataMember(IsRequired = true, Name = "NomeAutenticacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
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

	    private Guid _TemporaryUidUsuario;
	    [DataMember(Name = "TemporaryUidUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = " (Tmp)", Description="Temporary Key", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Guid TemporaryUidUsuario
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryUidUsuario.IsNullOrEmpty())
	    	                this._TemporaryUidUsuario = this._UidUsuario;
	    	          return this._TemporaryUidUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryUidUsuario != value)
	    	              this._TemporaryUidUsuario = value;
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

		

	[LinxPublicationView(PrimaryKeys="LoginInfo.EntityUniqueKey", IsUpdatable=false, EdmName="")]
		
	[DataContract(IsReference = false, Name = "LoginInfo")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Autorizacao.LoginInfo")]
	public partial class LoginInfo 
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
	 


	    private Guid _UidUsuario;

	    [DataMember(Name = "UidUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
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

	    private Int64 _IdUsuario;

	    [DataMember(Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
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

	    private string _NomeCurtoUsuario;

	    [DataMember(Name = "NomeCurtoUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string NomeCurtoUsuario
	    {
	    	    get
	    	    {
	    	          return _NomeCurtoUsuario;
	    	    }
	    	    set
	    	    {
	    	          this._NomeCurtoUsuario = value;
	    	    }
	    }

	    private bool _AutenticacaoWindows;

	    [DataMember(Name = "AutenticacaoWindows", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public bool AutenticacaoWindows
	    {
	    	    get
	    	    {
	    	          return _AutenticacaoWindows;
	    	    }
	    	    set
	    	    {
	    	          this._AutenticacaoWindows = value;
	    	    }
	    }

	    private System.DateTime _DataExpiracaoSenha;

	    [DataMember(Name = "DataExpiracaoSenha", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public System.DateTime DataExpiracaoSenha
	    {
	    	    get
	    	    {
	    	          return _DataExpiracaoSenha;
	    	    }
	    	    set
	    	    {
	    	          this._DataExpiracaoSenha = value;
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

	    private string _DescricaoGrupoEconomico;

	    [DataMember(Name = "DescricaoGrupoEconomico", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string DescricaoGrupoEconomico
	    {
	    	    get
	    	    {
	    	          return _DescricaoGrupoEconomico;
	    	    }
	    	    set
	    	    {
	    	          this._DescricaoGrupoEconomico = value;
	    	    }
	    }

	    private int _IdLinxGrupoEconomico;

	    [DataMember(Name = "IdLinxGrupoEconomico", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public int IdLinxGrupoEconomico
	    {
	    	    get
	    	    {
	    	          return _IdLinxGrupoEconomico;
	    	    }
	    	    set
	    	    {
	    	          this._IdLinxGrupoEconomico = value;
	    	    }
	    }

	    private List<AmbienteInfo> _Ambientes;

	    [DataMember(Name = "Ambientes", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public List<AmbienteInfo> Ambientes
	    {
	    	    get
	    	    {
	    	          return _Ambientes;
	    	    }
	    	    set
	    	    {
	    	          this._Ambientes = value;
	    	    }
	    }

	    private List<GpeconInfo> _GruposEconomicos;

	    [DataMember(Name = "GruposEconomicos", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public List<GpeconInfo> GruposEconomicos
	    {
	    	    get
	    	    {
	    	          return _GruposEconomicos;
	    	    }
	    	    set
	    	    {
	    	          this._GruposEconomicos = value;
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

		

	[LinxPublicationView(PrimaryKeys="AmbienteInfo.EntityUniqueKey", IsUpdatable=false, EdmName="")]
		
	[DataContract(IsReference = false, Name = "AmbienteInfo")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Autorizacao.AmbienteInfo")]
	public partial class AmbienteInfo 
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

	    private Guid _Token;

	    [DataMember(Name = "Token", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public Guid Token
	    {
	    	    get
	    	    {
	    	          return _Token;
	    	    }
	    	    set
	    	    {
	    	          this._Token = value;
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

	    private string _DescricaoEmpresa;

	    [DataMember(Name = "DescricaoEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string DescricaoEmpresa
	    {
	    	    get
	    	    {
	    	          return _DescricaoEmpresa;
	    	    }
	    	    set
	    	    {
	    	          this._DescricaoEmpresa = value;
	    	    }
	    }

	    private string _UrlAplicativo;

	    [DataMember(Name = "UrlAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string UrlAplicativo
	    {
	    	    get
	    	    {
	    	          return _UrlAplicativo;
	    	    }
	    	    set
	    	    {
	    	          this._UrlAplicativo = value;
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

	    private bool _IndicaMultiGpecon;

	    [DataMember(Name = "IndicaMultiGpecon", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public bool IndicaMultiGpecon
	    {
	    	    get
	    	    {
	    	          return _IndicaMultiGpecon;
	    	    }
	    	    set
	    	    {
	    	          this._IndicaMultiGpecon = value;
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
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsUsuarioAcesso];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[];EdmEntityName[TCS_USUARIO_ACESSO];EntityRelations[TCS_USUARIO_AUTENTICACAO(TCS_USUARIO_AUTENTICACAO)#TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)#TCS_AMBIENTE(TCS_AMBIENTE)#TCS_APLICACAO(TCS_APLICACAO)#TCS_APLICATIVO(TCS_APLICATIVO)#TCS_AMBIENTE1(TCS_AMBIENTE)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioAcesso")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Autorizacao.TcsUsuarioAcesso")]
	public partial class TcsUsuarioAcesso : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For Administrador
	    partial void OnAdministradorChanging(bool value);
	    partial void OnAdministradorChanged();

	    private bool _Administrador;

	    [DataMember(IsRequired = true, Name = "Administrador", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Indica Administrador", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.INDICA_ADMINISTRADOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.INDICA_ADMINISTRADOR")]
	    public bool Administrador
	    {
	    	    get
	    	    {
	    	          return _Administrador;
	    	    }
	    	    set
	    	    {
	    	          if (this._Administrador != value)
	    	          {
	    	              this.ValidateProperty("Administrador", value);
	    	              this.OnAdministradorChanging(value);
	    	              this.RaiseDataMemberChanging("Administrador");
	    	              this._Administrador = value;
	    	              this.RaiseDataMemberChanged("Administrador");
	    	              this.OnAdministradorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For AutenticacaoWindows
	    partial void OnAutenticacaoWindowsChanging(bool value);
	    partial void OnAutenticacaoWindowsChanged();

	    private bool _AutenticacaoWindows;

	    [DataMember(IsRequired = true, Name = "AutenticacaoWindows", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Autenticacao Windows", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.AUTENTICACAO_WINDOWS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.AUTENTICACAO_WINDOWS")]
	    public bool AutenticacaoWindows
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
	    //Extensibility Partial Method Definitions For DataExpiracaoSenha
	    partial void OnDataExpiracaoSenhaChanging(DateTime value);
	    partial void OnDataExpiracaoSenhaChanged();

	    private DateTime _DataExpiracaoSenha;

	    [DataMember(IsRequired = true, Name = "DataExpiracaoSenha", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Data Expiracao Senha", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.DATA_EXPIRACAO_SENHA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.DATA_EXPIRACAO_SENHA")]
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
	    //Extensibility Partial Method Definitions For DescAmbiente
	    partial void OnDescAmbienteChanging(string value);
	    partial void OnDescAmbienteChanged();

	    private string _DescAmbiente;

	    [DataMember(IsRequired = true, Name = "DescAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descricao Ambiente", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.DESCRICAO_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.DESCRICAO_AMBIENTE")]
	    public string DescAmbiente
	    {
	    	    get
	    	    {
	    	          return _DescAmbiente;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescAmbiente != value)
	    	          {
	    	              this.ValidateProperty("DescAmbiente", value);
	    	              this.OnDescAmbienteChanging(value);
	    	              this.RaiseDataMemberChanging("DescAmbiente");
	    	              this._DescAmbiente = value;
	    	              this.RaiseDataMemberChanged("DescAmbiente");
	    	              this.OnDescAmbienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescAmbienteRelacionado
	    partial void OnDescAmbienteRelacionadoChanging(string value);
	    partial void OnDescAmbienteRelacionadoChanged();

	    private string _DescAmbienteRelacionado;

	    [DataMember(Name = "DescAmbienteRelacionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "DescAmbienteRelacionado ", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE1.DESCRICAO_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.DESCRICAO_AMBIENTE")]
	    public string DescAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _DescAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescAmbienteRelacionado != value)
	    	          {
	    	              this.ValidateProperty("DescAmbienteRelacionado", value);
	    	              this.OnDescAmbienteRelacionadoChanging(value);
	    	              this.RaiseDataMemberChanging("DescAmbienteRelacionado");
	    	              this._DescAmbienteRelacionado = value;
	    	              this.RaiseDataMemberChanged("DescAmbienteRelacionado");
	    	              this.OnDescAmbienteRelacionadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescAplicativo
	    partial void OnDescAplicativoChanging(string value);
	    partial void OnDescAplicativoChanged();

	    private string _DescAplicativo;

	    [DataMember(Name = "DescAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descricao Aplicativo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO")]
	    public string DescAplicativo
	    {
	    	    get
	    	    {
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
	    //Extensibility Partial Method Definitions For DescAplicativoAmbienteRelacionado
	    partial void OnDescAplicativoAmbienteRelacionadoChanging(string value);
	    partial void OnDescAplicativoAmbienteRelacionadoChanged();

	    private string _DescAplicativoAmbienteRelacionado;

	    [DataMember(Name = "DescAplicativoAmbienteRelacionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "DescAplicativoAmbienteRelacionado", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE1.TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.TCS_APLICACAO.TCS_APLICATIVO.DESCRICAO_APLICATIVO")]
	    public string DescAplicativoAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _DescAplicativoAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescAplicativoAmbienteRelacionado != value)
	    	          {
	    	              this.ValidateProperty("DescAplicativoAmbienteRelacionado", value);
	    	              this.OnDescAplicativoAmbienteRelacionadoChanging(value);
	    	              this.RaiseDataMemberChanging("DescAplicativoAmbienteRelacionado");
	    	              this._DescAplicativoAmbienteRelacionado = value;
	    	              this.RaiseDataMemberChanged("DescAplicativoAmbienteRelacionado");
	    	              this.OnDescAplicativoAmbienteRelacionadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescEmpresa
	    partial void OnDescEmpresaChanging(string value);
	    partial void OnDescEmpresaChanged();

	    private string _DescEmpresa;

	    [DataMember(IsRequired = true, Name = "DescEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Empresa1", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
	    public string DescEmpresa
	    {
	    	    get
	    	    {
	    	          return _DescEmpresa;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescEmpresa != value)
	    	          {
	    	              this.ValidateProperty("DescEmpresa", value);
	    	              this.OnDescEmpresaChanging(value);
	    	              this.RaiseDataMemberChanging("DescEmpresa");
	    	              this._DescEmpresa = value;
	    	              this.RaiseDataMemberChanged("DescEmpresa");
	    	              this.OnDescEmpresaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescEmpresaAmbienteRelacionado
	    partial void OnDescEmpresaAmbienteRelacionadoChanging(string value);
	    partial void OnDescEmpresaAmbienteRelacionadoChanged();

	    private string _DescEmpresaAmbienteRelacionado;

	    [DataMember(Name = "DescEmpresaAmbienteRelacionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Empresa1", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
	    public string DescEmpresaAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _DescEmpresaAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescEmpresaAmbienteRelacionado != value)
	    	          {
	    	              this.ValidateProperty("DescEmpresaAmbienteRelacionado", value);
	    	              this.OnDescEmpresaAmbienteRelacionadoChanging(value);
	    	              this.RaiseDataMemberChanging("DescEmpresaAmbienteRelacionado");
	    	              this._DescEmpresaAmbienteRelacionado = value;
	    	              this.RaiseDataMemberChanged("DescEmpresaAmbienteRelacionado");
	    	              this.OnDescEmpresaAmbienteRelacionadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescGrupoEconomico
	    partial void OnDescGrupoEconomicoChanging(string value);
	    partial void OnDescGrupoEconomicoChanged();

	    private string _DescGrupoEconomico;

	    [DataMember(IsRequired = true, Name = "DescGrupoEconomico", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Empresa", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
	    public string DescGrupoEconomico
	    {
	    	    get
	    	    {
	    	          return _DescGrupoEconomico;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescGrupoEconomico != value)
	    	          {
	    	              this.ValidateProperty("DescGrupoEconomico", value);
	    	              this.OnDescGrupoEconomicoChanging(value);
	    	              this.RaiseDataMemberChanging("DescGrupoEconomico");
	    	              this._DescGrupoEconomico = value;
	    	              this.RaiseDataMemberChanged("DescGrupoEconomico");
	    	              this.OnDescGrupoEconomicoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLinxAmbiente
	    partial void OnIdLinxAmbienteChanging(int value);
	    partial void OnIdLinxAmbienteChanged();

	    private int _IdLinxAmbiente;

	    [DataMember(IsRequired = true, Name = "IdLinxAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx1", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
	    public int IdLinxAmbiente
	    {
	    	    get
	    	    {
	    	          return _IdLinxAmbiente;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLinxAmbiente != value)
	    	          {
	    	              this.ValidateProperty("IdLinxAmbiente", value);
	    	              this.OnIdLinxAmbienteChanging(value);
	    	              this.RaiseDataMemberChanging("IdLinxAmbiente");
	    	              this._IdLinxAmbiente = value;
	    	              this.RaiseDataMemberChanged("IdLinxAmbiente");
	    	              this.OnIdLinxAmbienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLinxGrupoEconomico
	    partial void OnIdLinxGrupoEconomicoChanging(int value);
	    partial void OnIdLinxGrupoEconomicoChanged();

	    private int _IdLinxGrupoEconomico;

	    [DataMember(IsRequired = true, Name = "IdLinxGrupoEconomico", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
	    public int IdLinxGrupoEconomico
	    {
	    	    get
	    	    {
	    	          return _IdLinxGrupoEconomico;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLinxGrupoEconomico != value)
	    	          {
	    	              this.ValidateProperty("IdLinxGrupoEconomico", value);
	    	              this.OnIdLinxGrupoEconomicoChanging(value);
	    	              this.RaiseDataMemberChanging("IdLinxGrupoEconomico");
	    	              this._IdLinxGrupoEconomico = value;
	    	              this.RaiseDataMemberChanged("IdLinxGrupoEconomico");
	    	              this.OnIdLinxGrupoEconomicoChanged();
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
	    //Extensibility Partial Method Definitions For IdTcsAmbienteRelacionado
	    partial void OnIdTcsAmbienteRelacionadoChanging(System.Nullable<int> value);
	    partial void OnIdTcsAmbienteRelacionadoChanged();

	    private System.Nullable<int> _IdTcsAmbienteRelacionado;

	    [DataMember(Name = "IdTcsAmbienteRelacionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente1", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE1.ID_TCS_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.ID_TCS_AMBIENTE")]
	    public System.Nullable<int> IdTcsAmbienteRelacionado
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
	    //Extensibility Partial Method Definitions For IdTcsAplicativoAmbienteRelacionado
	    partial void OnIdTcsAplicativoAmbienteRelacionadoChanging(System.Nullable<int> value);
	    partial void OnIdTcsAplicativoAmbienteRelacionadoChanged();

	    private System.Nullable<int> _IdTcsAplicativoAmbienteRelacionado;

	    [DataMember(Name = "IdTcsAplicativoAmbienteRelacionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Aplicativo1", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE1.TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.TCS_APLICACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO")]
	    public System.Nullable<int> IdTcsAplicativoAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _IdTcsAplicativoAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAplicativoAmbienteRelacionado != value)
	    	          {
	    	              this.ValidateProperty("IdTcsAplicativoAmbienteRelacionado", value);
	    	              this.OnIdTcsAplicativoAmbienteRelacionadoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsAplicativoAmbienteRelacionado");
	    	              this._IdTcsAplicativoAmbienteRelacionado = value;
	    	              this.RaiseDataMemberChanged("IdTcsAplicativoAmbienteRelacionado");
	    	              this.OnIdTcsAplicativoAmbienteRelacionadoChanged();
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
	    //Extensibility Partial Method Definitions For MultiGpecon
	    partial void OnMultiGpeconChanging(bool value);
	    partial void OnMultiGpeconChanged();

	    private bool _MultiGpecon;

	    [DataMember(IsRequired = true, Name = "MultiGpecon", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Indica Multi Gpecon", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.INDICA_MULTI_GPECON];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.INDICA_MULTI_GPECON")]
	    public bool MultiGpecon
	    {
	    	    get
	    	    {
	    	          return _MultiGpecon;
	    	    }
	    	    set
	    	    {
	    	          if (this._MultiGpecon != value)
	    	          {
	    	              this.ValidateProperty("MultiGpecon", value);
	    	              this.OnMultiGpeconChanging(value);
	    	              this.RaiseDataMemberChanging("MultiGpecon");
	    	              this._MultiGpecon = value;
	    	              this.RaiseDataMemberChanged("MultiGpecon");
	    	              this.OnMultiGpeconChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeCurtoUsuario
	    partial void OnNomeCurtoUsuarioChanging(string value);
	    partial void OnNomeCurtoUsuarioChanged();

	    private string _NomeCurtoUsuario;

	    [DataMember(IsRequired = true, Name = "NomeCurtoUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Curto Usuario", Description="", Order = 20, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_CURTO_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_CURTO_USUARIO")]
	    public string NomeCurtoUsuario
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
	    //Extensibility Partial Method Definitions For UidAplicacao
	    partial void OnUidAplicacaoChanging(Guid value);
	    partial void OnUidAplicacaoChanged();

	    private Guid _UidAplicacao;

	    [DataMember(IsRequired = true, Name = "UidAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Aplicacao", Description="", Order = 4, AutoGenerateField = false, GroupName="", ResourceType= null)]
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
	    //Extensibility Partial Method Definitions For UidAplicacaoAmbienteRelacionado
	    partial void OnUidAplicacaoAmbienteRelacionadoChanging(System.Nullable<Guid> value);
	    partial void OnUidAplicacaoAmbienteRelacionadoChanged();

	    private System.Nullable<Guid> _UidAplicacaoAmbienteRelacionado;

	    [DataMember(Name = "UidAplicacaoAmbienteRelacionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Aplicacao", Description="", Order = 4, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE1.TCS_APLICACAO.UID_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.TCS_APLICACAO.UID_APLICACAO")]
	    public System.Nullable<Guid> UidAplicacaoAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _UidAplicacaoAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidAplicacaoAmbienteRelacionado != value)
	    	          {
	    	              this.ValidateProperty("UidAplicacaoAmbienteRelacionado", value);
	    	              this.OnUidAplicacaoAmbienteRelacionadoChanging(value);
	    	              this.RaiseDataMemberChanging("UidAplicacaoAmbienteRelacionado");
	    	              this._UidAplicacaoAmbienteRelacionado = value;
	    	              this.RaiseDataMemberChanged("UidAplicacaoAmbienteRelacionado");
	    	              this.OnUidAplicacaoAmbienteRelacionadoChanged();
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
	    [Display(Name = "Uid Empresa", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
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
	    //Extensibility Partial Method Definitions For UidEmpresaAmbienteRelacionado
	    partial void OnUidEmpresaAmbienteRelacionadoChanging(System.Nullable<Guid> value);
	    partial void OnUidEmpresaAmbienteRelacionadoChanged();

	    private System.Nullable<Guid> _UidEmpresaAmbienteRelacionado;

	    [DataMember(Name = "UidEmpresaAmbienteRelacionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Empresa1", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA")]
	    public System.Nullable<Guid> UidEmpresaAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _UidEmpresaAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidEmpresaAmbienteRelacionado != value)
	    	          {
	    	              this.ValidateProperty("UidEmpresaAmbienteRelacionado", value);
	    	              this.OnUidEmpresaAmbienteRelacionadoChanging(value);
	    	              this.RaiseDataMemberChanging("UidEmpresaAmbienteRelacionado");
	    	              this._UidEmpresaAmbienteRelacionado = value;
	    	              this.RaiseDataMemberChanged("UidEmpresaAmbienteRelacionado");
	    	              this.OnUidEmpresaAmbienteRelacionadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidGrupoEconomico
	    partial void OnUidGrupoEconomicoChanging(Guid value);
	    partial void OnUidGrupoEconomicoChanged();

	    private Guid _UidGrupoEconomico;

	    [DataMember(IsRequired = true, Name = "UidGrupoEconomico", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Empresa", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA")]
	    public Guid UidGrupoEconomico
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
	    partial void OnUidUsuarioChanging(Guid value);
	    partial void OnUidUsuarioChanged();

	    private Guid _UidUsuario;

	    [DataMember(IsRequired = true, Name = "UidUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Usuario", Description="", Order = 26, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.UID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.UID_USUARIO")]
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
	    //Extensibility Partial Method Definitions For NomeAutenticacao
	    partial void OnNomeAutenticacaoChanging(System.String value);
	    partial void OnNomeAutenticacaoChanged();

	    private System.String _NomeAutenticacao;

	    [DataMember(IsRequired = true, Name = "NomeAutenticacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = " Usuário Autenticação", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
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
	    [Display(Name = " Nome", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO];IsMeasure[false]")]
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

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_ACESSO.INDICA_MULTI_GPECON", Source = "MultiGpecon", Target = "INDICA_MULTI_GPECON", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_ACESSO", RelationPropertyName = "TCS_USUARIO_ACESSO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_ACESSO.INDICA_ADMINISTRADOR", Source = "Administrador", Target = "INDICA_ADMINISTRADOR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_USUARIO_ACESSO", RelationPropertyName = "TCS_USUARIO_ACESSO" });
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

		

	[LinxPublicationView(PrimaryKeys="AppInfo.EntityUniqueKey", IsUpdatable=false, EdmName="")]
		
	[DataContract(IsReference = false, Name = "AppInfo")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Autorizacao.AppInfo")]
	public partial class AppInfo 
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
	 


	    private string _AppName;

	    [DataMember(Name = "AppName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string AppName
	    {
	    	    get
	    	    {
	    	          if (_AppName.IsNullOrEmpty())
	    	             _AppName =  String.Empty;
	    	          return _AppName;
	    	    }
	    	    set
	    	    {
	    	          this._AppName = value;
	    	    }
	    }

	    private string _Name;

	    [DataMember(Name = "Name", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Name
	    {
	    	    get
	    	    {
	    	          return _Name;
	    	    }
	    	    set
	    	    {
	    	          this._Name = value;
	    	    }
	    }

	    private string _Location;

	    [DataMember(Name = "Location", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Location
	    {
	    	    get
	    	    {
	    	          return _Location;
	    	    }
	    	    set
	    	    {
	    	          this._Location = value;
	    	    }
	    }

	    private int _Length;

	    [DataMember(Name = "Length", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public int Length
	    {
	    	    get
	    	    {
	    	          return _Length;
	    	    }
	    	    set
	    	    {
	    	          this._Length = value;
	    	    }
	    }

	    private string _MD5;

	    [DataMember(Name = "MD5", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string MD5
	    {
	    	    get
	    	    {
	    	          return _MD5;
	    	    }
	    	    set
	    	    {
	    	          this._MD5 = value;
	    	    }
	    }

	    private string _AssemblyVersion;

	    [DataMember(Name = "AssemblyVersion", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string AssemblyVersion
	    {
	    	    get
	    	    {
	    	          return _AssemblyVersion;
	    	    }
	    	    set
	    	    {
	    	          this._AssemblyVersion = value;
	    	    }
	    }

	    private string _AssemblyFileVersion;

	    [DataMember(Name = "AssemblyFileVersion", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string AssemblyFileVersion
	    {
	    	    get
	    	    {
	    	          return _AssemblyFileVersion;
	    	    }
	    	    set
	    	    {
	    	          this._AssemblyFileVersion = value;
	    	    }
	    }

	    private string _AssemblyBuildDateTime;

	    [DataMember(Name = "AssemblyBuildDateTime", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string AssemblyBuildDateTime
	    {
	    	    get
	    	    {
	    	          return _AssemblyBuildDateTime;
	    	    }
	    	    set
	    	    {
	    	          this._AssemblyBuildDateTime = value;
	    	    }
	    }

	    private string _Download;

	    [DataMember(Name = "Download", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Download
	    {
	    	    get
	    	    {
	    	          return _Download;
	    	    }
	    	    set
	    	    {
	    	          this._Download = value;
	    	    }
	    }

	    private string _MinHostVersion;

	    [DataMember(Name = "MinHostVersion", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string MinHostVersion
	    {
	    	    get
	    	    {
	    	          return _MinHostVersion;
	    	    }
	    	    set
	    	    {
	    	          this._MinHostVersion = value;
	    	    }
	    }

	    private string _MaxHostVersion;

	    [DataMember(Name = "MaxHostVersion", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string MaxHostVersion
	    {
	    	    get
	    	    {
	    	          return _MaxHostVersion;
	    	    }
	    	    set
	    	    {
	    	          this._MaxHostVersion = value;
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

		

	[LinxPublicationView(PrimaryKeys="GpeconInfo.EntityUniqueKey", IsUpdatable=false, EdmName="")]
		
	[DataContract(IsReference = false, Name = "GpeconInfo")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Autorizacao.GpeconInfo")]
	public partial class GpeconInfo 
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
	 


	    private int _IdGpecon;

	    [DataMember(Name = "IdGpecon", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public int IdGpecon
	    {
	    	    get
	    	    {
	    	          return _IdGpecon;
	    	    }
	    	    set
	    	    {
	    	          this._IdGpecon = value;
	    	    }
	    }

	    private string _Descricao;

	    [DataMember(Name = "Descricao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Descricao
	    {
	    	    get
	    	    {
	    	          return _Descricao;
	    	    }
	    	    set
	    	    {
	    	          this._Descricao = value;
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
	/////////////////////////// Interface Definition //////////////////////
	///////////////////////////////////////////////////////////////////////
	public abstract class IAuthenticateUserExtension
	{

	
	    //ValidateUserExtension
	    public abstract bool ValidateUserExtension(string userName, string userPassword);
    	

	}
		
	
	///////////////////////////////////////////////////////////////////////
	//////////////////////// DomainService Class V1 ///////////////////////
	///////////////////////////////////////////////////////////////////////
	[EnableClientAccess()]	
	[DomainIdentifier("ProcessorOverviewAutorizacaoDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class AutorizacaoDomainService : DomainService, IDataServiceContext 
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

		
	    public AutorizacaoDomainService() : this("", null, null) { }
	    public AutorizacaoDomainService(string connectionString) : this(connectionString, null, null) { }
	    public AutorizacaoDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public AutorizacaoDomainService(Linx.Framework.Autorizacao.BM.AutorizacaoContext dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public AutorizacaoDomainService(string connectionString, Linx.Framework.Autorizacao.BM.AutorizacaoContext dataContext, Dictionary<string, string> headers) : base() 
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
	
		

	        if (entityName.InList("Linx.Framework.BV.Autorizacao.Acesso"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "Acesso",
	        			NameSpace = "Linx.Framework.BV.Autorizacao",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "Acesso",
	        			ClearMethodName = "ClearAcesso",
	        			QueryMethodName  = "GetPagedAcesso",	
	        			CountingMethodName  = "GetAcesso" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Autorizacao.Acesso"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Autorizacao.Acesso"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Autorizacao.UsuarioAcesso"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "UsuarioAcesso",
	        			NameSpace = "Linx.Framework.BV.Autorizacao",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "UsuarioAcesso",
	        			ClearMethodName = "ClearUsuarioAcesso",
	        			QueryMethodName  = "GetPagedUsuarioAcesso",	
	        			CountingMethodName  = "GetUsuarioAcesso" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Autorizacao.UsuarioAcesso"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Autorizacao.UsuarioAcesso"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Autorizacao.UserInfo"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "UserInfo",
	        			NameSpace = "Linx.Framework.BV.Autorizacao",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "UserInfo",
	        			ClearMethodName = "ClearUserInfo",
	        			QueryMethodName  = "GetPagedUserInfo",	
	        			CountingMethodName  = "GetUserInfo" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Autorizacao.UserInfo"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Autorizacao.UserInfo"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Autorizacao.LoginInfo"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "LoginInfo",
	        			NameSpace = "Linx.Framework.BV.Autorizacao",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "LoginInfo",
	        			ClearMethodName = "ClearLoginInfo",
	        			QueryMethodName  = "GetPagedLoginInfo",	
	        			CountingMethodName  = "GetLoginInfo" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Autorizacao.LoginInfo"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Autorizacao.LoginInfo"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Autorizacao.AmbienteInfo"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "AmbienteInfo",
	        			NameSpace = "Linx.Framework.BV.Autorizacao",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "AmbienteInfo",
	        			ClearMethodName = "ClearAmbienteInfo",
	        			QueryMethodName  = "GetPagedAmbienteInfo",	
	        			CountingMethodName  = "GetAmbienteInfo" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Autorizacao.AmbienteInfo"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Autorizacao.AmbienteInfo"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Autorizacao.TcsUsuarioAcesso"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsUsuarioAcesso",
	        			NameSpace = "Linx.Framework.BV.Autorizacao",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsUsuarioAcesso",
	        			ClearMethodName = "ClearTcsUsuarioAcesso",
	        			QueryMethodName  = "GetPagedTcsUsuarioAcesso",	
	        			CountingMethodName  = "GetTcsUsuarioAcesso" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Autorizacao.TcsUsuarioAcesso"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Autorizacao.TcsUsuarioAcesso"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Autorizacao.AppInfo"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "AppInfo",
	        			NameSpace = "Linx.Framework.BV.Autorizacao",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "AppInfo",
	        			ClearMethodName = "ClearAppInfo",
	        			QueryMethodName  = "GetPagedAppInfo",	
	        			CountingMethodName  = "GetAppInfo" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Autorizacao.AppInfo"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Autorizacao.AppInfo"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Autorizacao.GpeconInfo"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "GpeconInfo",
	        			NameSpace = "Linx.Framework.BV.Autorizacao",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "GpeconInfo",
	        			ClearMethodName = "ClearGpeconInfo",
	        			QueryMethodName  = "GetPagedGpeconInfo",	
	        			CountingMethodName  = "GetGpeconInfo" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Autorizacao.GpeconInfo"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Autorizacao.GpeconInfo"), forceAll: forceAll)
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

         		    return new string[] { "Framework_AutorizacaoClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.AutorizacaoClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Framework_autorizacaoService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.autorizacaoService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear Acesso.
	    public IEnumerable<Acesso> ClearAcesso()
	    {
	        List<Acesso> result = new List<Acesso>();
	        result.Add(new Acesso());	
		
	        

	
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
	    //Clear UserInfo.
	    public IEnumerable<UserInfo> ClearUserInfo()
	    {
	        List<UserInfo> result = new List<UserInfo>();
	        result.Add(new UserInfo());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear LoginInfo.
	    public IEnumerable<LoginInfo> ClearLoginInfo()
	    {
	        List<LoginInfo> result = new List<LoginInfo>();
	        result.Add(new LoginInfo());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear AmbienteInfo.
	    public IEnumerable<AmbienteInfo> ClearAmbienteInfo()
	    {
	        List<AmbienteInfo> result = new List<AmbienteInfo>();
	        result.Add(new AmbienteInfo());	
		
	        

	
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
	    //Clear AppInfo.
	    public IEnumerable<AppInfo> ClearAppInfo()
	    {
	        List<AppInfo> result = new List<AppInfo>();
	        result.Add(new AppInfo());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear GpeconInfo.
	    public IEnumerable<GpeconInfo> ClearGpeconInfo()
	    {
	        List<GpeconInfo> result = new List<GpeconInfo>();
	        result.Add(new GpeconInfo());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    
	    [Query(HasSideEffects = false)]
	    //Get Acesso.
	    public IEnumerable<Acesso> GetAcesso()
	    {




		

	        IEnumerable<Acesso> result = 
	            (from entity0 in Acesso.OnSearchingReplacement(null) select entity0);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get AcessoNoAssociations.
	    public IEnumerable<Acesso> GetAcessoNoAssociations()
	    {




		

	        IEnumerable<Acesso> result = 
	            (from entity0 in Acesso.OnSearchingReplacement(null) select entity0);
		
	
	        	

	
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
	    //Get UserInfo.
	    public IEnumerable<UserInfo> GetUserInfo()
	    {




	
	        IEnumerable<UserInfo> result = new List<UserInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get UserInfoNoAssociations.
	    public IEnumerable<UserInfo> GetUserInfoNoAssociations()
	    {




	
	        IEnumerable<UserInfo> result = new List<UserInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get LoginInfo.
	    public IEnumerable<LoginInfo> GetLoginInfo()
	    {




	
	        IEnumerable<LoginInfo> result = new List<LoginInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get LoginInfoNoAssociations.
	    public IEnumerable<LoginInfo> GetLoginInfoNoAssociations()
	    {




	
	        IEnumerable<LoginInfo> result = new List<LoginInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get AmbienteInfo.
	    public IEnumerable<AmbienteInfo> GetAmbienteInfo()
	    {




	
	        IEnumerable<AmbienteInfo> result = new List<AmbienteInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get AmbienteInfoNoAssociations.
	    public IEnumerable<AmbienteInfo> GetAmbienteInfoNoAssociations()
	    {




	
	        IEnumerable<AmbienteInfo> result = new List<AmbienteInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsUsuarioAcesso.
	    public IQueryable<TcsUsuarioAcesso> GetTcsUsuarioAcesso()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioAcesso> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_ACESSO
                  let entity0Al2 = entity0.TCS_AMBIENTE
                  let entity0Al3 = entity0.TCS_AMBIENTE1
                  let entity0Al1 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al9 = entity0.TCS_AMBIENTE.TCS_APLICACAO
                  let entity0Al10 = entity0.TCS_AMBIENTE1.TCS_APLICACAO
                  let entity0Al6 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al7 = entity0.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al4 = entity0.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
                  let entity0Al5 = entity0.TCS_AMBIENTE1.TCS_APLICACAO.TCS_APLICATIVO
                  let entity0Al8 = entity0.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsUsuarioAcesso()		
	            {
	            
                Administrador = entity0.INDICA_ADMINISTRADOR
                , AutenticacaoWindows = entity0Al1.AUTENTICACAO_WINDOWS
                , DataExpiracaoSenha = entity0Al1.DATA_EXPIRACAO_SENHA
                , DescAmbiente = entity0Al2.DESCRICAO_AMBIENTE
                , DescAmbienteRelacionado = entity0Al3.DESCRICAO_AMBIENTE
                , DescAplicativo = entity0Al4.DESCRICAO_APLICATIVO
                , DescAplicativoAmbienteRelacionado = entity0Al5.DESCRICAO_APLICATIVO
                , DescEmpresa = entity0Al6.NOME_EMPRESA
                , DescEmpresaAmbienteRelacionado = entity0Al7.NOME_EMPRESA
                , DescGrupoEconomico = entity0Al8.NOME_EMPRESA
                , IdLinxAmbiente = entity0Al6.ID_LINX
                , IdLinxGrupoEconomico = entity0Al8.ID_LINX
                , IdTcsAmbiente = entity0Al2.ID_TCS_AMBIENTE
                , IdTcsAmbienteRelacionado = entity0Al3.ID_TCS_AMBIENTE
                , IdTcsAplicativo = entity0Al4.ID_TCS_APLICATIVO
                , IdTcsAplicativoAmbienteRelacionado = entity0Al5.ID_TCS_APLICATIVO
                , IdTcsUsuarioAcesso = entity0.ID_TCS_USUARIO_ACESSO
                , IdUsuario = entity0Al1.ID_USUARIO
                , MultiGpecon = entity0.INDICA_MULTI_GPECON
                , NomeCurtoUsuario = entity0Al1.NOME_CURTO_USUARIO
                , UidAplicacao = entity0Al9.UID_APLICACAO
                , UidAplicacaoAmbienteRelacionado = entity0Al10.UID_APLICACAO
                , UidEmpresa = entity0Al6.UID_EMPRESA
                , UidEmpresaAmbienteRelacionado = entity0Al7.UID_EMPRESA
                , UidGrupoEconomico = entity0Al8.UID_EMPRESA
                , UidUsuario = entity0Al1.UID_USUARIO
                , NomeAutenticacao = entity0Al1.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al1.NOME_USUARIO
		
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
                  let entity0Al2 = entity0.TCS_AMBIENTE
                  let entity0Al3 = entity0.TCS_AMBIENTE1
                  let entity0Al1 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al9 = entity0.TCS_AMBIENTE.TCS_APLICACAO
                  let entity0Al10 = entity0.TCS_AMBIENTE1.TCS_APLICACAO
                  let entity0Al6 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al7 = entity0.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al4 = entity0.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
                  let entity0Al5 = entity0.TCS_AMBIENTE1.TCS_APLICACAO.TCS_APLICATIVO
                  let entity0Al8 = entity0.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsUsuarioAcesso()		
	            {
	            
                Administrador = entity0.INDICA_ADMINISTRADOR
                , AutenticacaoWindows = entity0Al1.AUTENTICACAO_WINDOWS
                , DataExpiracaoSenha = entity0Al1.DATA_EXPIRACAO_SENHA
                , DescAmbiente = entity0Al2.DESCRICAO_AMBIENTE
                , DescAmbienteRelacionado = entity0Al3.DESCRICAO_AMBIENTE
                , DescAplicativo = entity0Al4.DESCRICAO_APLICATIVO
                , DescAplicativoAmbienteRelacionado = entity0Al5.DESCRICAO_APLICATIVO
                , DescEmpresa = entity0Al6.NOME_EMPRESA
                , DescEmpresaAmbienteRelacionado = entity0Al7.NOME_EMPRESA
                , DescGrupoEconomico = entity0Al8.NOME_EMPRESA
                , IdLinxAmbiente = entity0Al6.ID_LINX
                , IdLinxGrupoEconomico = entity0Al8.ID_LINX
                , IdTcsAmbiente = entity0Al2.ID_TCS_AMBIENTE
                , IdTcsAmbienteRelacionado = entity0Al3.ID_TCS_AMBIENTE
                , IdTcsAplicativo = entity0Al4.ID_TCS_APLICATIVO
                , IdTcsAplicativoAmbienteRelacionado = entity0Al5.ID_TCS_APLICATIVO
                , IdTcsUsuarioAcesso = entity0.ID_TCS_USUARIO_ACESSO
                , IdUsuario = entity0Al1.ID_USUARIO
                , MultiGpecon = entity0.INDICA_MULTI_GPECON
                , NomeCurtoUsuario = entity0Al1.NOME_CURTO_USUARIO
                , UidAplicacao = entity0Al9.UID_APLICACAO
                , UidAplicacaoAmbienteRelacionado = entity0Al10.UID_APLICACAO
                , UidEmpresa = entity0Al6.UID_EMPRESA
                , UidEmpresaAmbienteRelacionado = entity0Al7.UID_EMPRESA
                , UidGrupoEconomico = entity0Al8.UID_EMPRESA
                , UidUsuario = entity0Al1.UID_USUARIO
                , NomeAutenticacao = entity0Al1.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al1.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get AppInfo.
	    public IEnumerable<AppInfo> GetAppInfo()
	    {




	
	        IEnumerable<AppInfo> result = new List<AppInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get AppInfoNoAssociations.
	    public IEnumerable<AppInfo> GetAppInfoNoAssociations()
	    {




	
	        IEnumerable<AppInfo> result = new List<AppInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get GpeconInfo.
	    public IEnumerable<GpeconInfo> GetGpeconInfo()
	    {




	
	        IEnumerable<GpeconInfo> result = new List<GpeconInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get GpeconInfoNoAssociations.
	    public IEnumerable<GpeconInfo> GetGpeconInfoNoAssociations()
	    {




	
	        IEnumerable<GpeconInfo> result = new List<GpeconInfo>();
	  	
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	//Add filtering disabled property for TCS_USUARIO_ACESSO
	    	string[] bmDisabledTcsUsuarioAcessoList = this.GetEDM().GetFilteringDisabledList("TCS_USUARIO_ACESSO");
	    	if (bmDisabledTcsUsuarioAcessoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsUsuarioAcessoList.Contains("TCS_USUARIO_ACESSO.INDICA_ADMINISTRADOR"))
	    		{
	    			result.Add("TcsUsuarioAcesso|Administrador");
	    			result.Add("TcsUsuarioAcesso|TCS_USUARIO_ACESSO.INDICA_ADMINISTRADOR");
	    		}
	
	    		if (bmDisabledTcsUsuarioAcessoList.Contains("TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO"))
	    		{
	    			result.Add("TcsUsuarioAcesso|IdTcsUsuarioAcesso");
	    			result.Add("TcsUsuarioAcesso|TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO");
	    		}
	
	    		if (bmDisabledTcsUsuarioAcessoList.Contains("TCS_USUARIO_ACESSO.INDICA_MULTI_GPECON"))
	    		{
	    			result.Add("TcsUsuarioAcesso|MultiGpecon");
	    			result.Add("TcsUsuarioAcesso|TCS_USUARIO_ACESSO.INDICA_MULTI_GPECON");
	    		}
	    	}
	    	return result;
	    }

	    #endregion Excluded Filters

	    #region Get By EntitySearchId Methods Definitions.
	
				
	    [Ignore]
	    //Get Acesso By EntitySearchId.
	    public IEnumerable<Acesso> GetAcessoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetAcessoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get Acesso By EntitySearchId.
	    public IEnumerable<Acesso> GetAcessoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetAcessoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
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
	    //Get UserInfo By EntitySearchId.
	    public IEnumerable<UserInfo> GetUserInfoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetUserInfoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get UserInfo By EntitySearchId.
	    public IEnumerable<UserInfo> GetUserInfoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetUserInfoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get LoginInfo By EntitySearchId.
	    public IEnumerable<LoginInfo> GetLoginInfoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetLoginInfoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get LoginInfo By EntitySearchId.
	    public IEnumerable<LoginInfo> GetLoginInfoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetLoginInfoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get AmbienteInfo By EntitySearchId.
	    public IEnumerable<AmbienteInfo> GetAmbienteInfoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetAmbienteInfoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get AmbienteInfo By EntitySearchId.
	    public IEnumerable<AmbienteInfo> GetAmbienteInfoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetAmbienteInfoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioAcesso By EntitySearchId.
	    public IQueryable<TcsUsuarioAcesso> GetTcsUsuarioAcessoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioAcessoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioAcesso By EntitySearchId.
	    public IQueryable<TcsUsuarioAcesso> GetTcsUsuarioAcessoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioAcessoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get AppInfo By EntitySearchId.
	    public IEnumerable<AppInfo> GetAppInfoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetAppInfoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get AppInfo By EntitySearchId.
	    public IEnumerable<AppInfo> GetAppInfoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetAppInfoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get GpeconInfo By EntitySearchId.
	    public IEnumerable<GpeconInfo> GetGpeconInfoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetGpeconInfoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get GpeconInfo By EntitySearchId.
	    public IEnumerable<GpeconInfo> GetGpeconInfoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetGpeconInfoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get Acesso By Example.
	    [Ignore]
	    public IEnumerable<Acesso> GetAcessoByExample(Acesso entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetAcessoByEntitySearch(queryAnalysis);
	    }
			
	    //Get Acesso By Example.
	    [Ignore]
	    public IEnumerable<Acesso> GetAcessoByExampleNoAssociations(Acesso entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetAcessoByEntitySearchNoAssociations(queryAnalysis);
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
			
	    //Get UserInfo By Example.
	    [Ignore]
	    public IEnumerable<UserInfo> GetUserInfoByExample(UserInfo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetUserInfoByEntitySearch(queryAnalysis);
	    }
			
	    //Get UserInfo By Example.
	    [Ignore]
	    public IEnumerable<UserInfo> GetUserInfoByExampleNoAssociations(UserInfo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetUserInfoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get LoginInfo By Example.
	    [Ignore]
	    public IEnumerable<LoginInfo> GetLoginInfoByExample(LoginInfo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetLoginInfoByEntitySearch(queryAnalysis);
	    }
			
	    //Get LoginInfo By Example.
	    [Ignore]
	    public IEnumerable<LoginInfo> GetLoginInfoByExampleNoAssociations(LoginInfo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetLoginInfoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get AmbienteInfo By Example.
	    [Ignore]
	    public IEnumerable<AmbienteInfo> GetAmbienteInfoByExample(AmbienteInfo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetAmbienteInfoByEntitySearch(queryAnalysis);
	    }
			
	    //Get AmbienteInfo By Example.
	    [Ignore]
	    public IEnumerable<AmbienteInfo> GetAmbienteInfoByExampleNoAssociations(AmbienteInfo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetAmbienteInfoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsUsuarioAcesso By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioAcesso> GetTcsUsuarioAcessoByExample(TcsUsuarioAcesso entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioAcessoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsUsuarioAcesso By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioAcesso> GetTcsUsuarioAcessoByExampleNoAssociations(TcsUsuarioAcesso entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioAcessoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get AppInfo By Example.
	    [Ignore]
	    public IEnumerable<AppInfo> GetAppInfoByExample(AppInfo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetAppInfoByEntitySearch(queryAnalysis);
	    }
			
	    //Get AppInfo By Example.
	    [Ignore]
	    public IEnumerable<AppInfo> GetAppInfoByExampleNoAssociations(AppInfo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetAppInfoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get GpeconInfo By Example.
	    [Ignore]
	    public IEnumerable<GpeconInfo> GetGpeconInfoByExample(GpeconInfo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetGpeconInfoByEntitySearch(queryAnalysis);
	    }
			
	    //Get GpeconInfo By Example.
	    [Ignore]
	    public IEnumerable<GpeconInfo> GetGpeconInfoByExampleNoAssociations(GpeconInfo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetGpeconInfoByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public Acesso GetAcessoByKey(int idTcsAmbiente)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("Acesso");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsAmbiente"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsAmbiente));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetAcessoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public UsuarioAcesso GetUsuarioAcessoByKey(Guid uidUsuario, int idAmbiente, Guid uidAplicacao, Guid uidEmpresa, Guid uidGrupoEconomico, Guid uidGrupoAcesso)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("UsuarioAcesso");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "UidUsuario"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, uidUsuario));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdAmbiente"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idAmbiente));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "UidAplicacao"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, uidAplicacao));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "UidEmpresa"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, uidEmpresa));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "UidGrupoEconomico"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, uidGrupoEconomico));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "UidGrupoAcesso"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, uidGrupoAcesso));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetUsuarioAcessoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public UserInfo GetUserInfoByKey(Guid uidUsuario)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("UserInfo");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "UidUsuario"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, uidUsuario));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetUserInfoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public LoginInfo GetLoginInfoByKey(Guid uidUsuario)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("LoginInfo");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "UidUsuario"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, uidUsuario));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetLoginInfoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public AmbienteInfo GetAmbienteInfoByKey(int idTcsAmbiente)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("AmbienteInfo");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsAmbiente"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsAmbiente));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetAmbienteInfoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsUsuarioAcesso GetTcsUsuarioAcessoByKey(int idTcsUsuarioAcesso)
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
	    public AppInfo GetAppInfoByKey(string appName)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("AppInfo");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "AppName"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, appName));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetAppInfoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public GpeconInfo GetGpeconInfoByKey(int idGpecon)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("GpeconInfo");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdGpecon"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idGpecon));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetGpeconInfoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get AcessoByEntitySearch.
	    public IEnumerable<Acesso> GetAcessoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		

	        IEnumerable<Acesso> result = 
	            (from entity0 in Acesso.OnSearchingReplacement(entitySearchList) select entity0);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get AcessoByEntitySearchNoAssociations.
	    public IEnumerable<Acesso> GetAcessoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		

	        IEnumerable<Acesso> result = 
	            (from entity0 in Acesso.OnSearchingReplacement(entitySearchList) select entity0);
		
	
	        	

	
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
	    //Get UserInfoByEntitySearch.
	    public IEnumerable<UserInfo> GetUserInfoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<UserInfo> result = new List<UserInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get UserInfoByEntitySearchNoAssociations.
	    public IEnumerable<UserInfo> GetUserInfoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<UserInfo> result = new List<UserInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get LoginInfoByEntitySearch.
	    public IEnumerable<LoginInfo> GetLoginInfoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<LoginInfo> result = new List<LoginInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get LoginInfoByEntitySearchNoAssociations.
	    public IEnumerable<LoginInfo> GetLoginInfoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<LoginInfo> result = new List<LoginInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get AmbienteInfoByEntitySearch.
	    public IEnumerable<AmbienteInfo> GetAmbienteInfoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<AmbienteInfo> result = new List<AmbienteInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get AmbienteInfoByEntitySearchNoAssociations.
	    public IEnumerable<AmbienteInfo> GetAmbienteInfoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<AmbienteInfo> result = new List<AmbienteInfo>();
	  	
	
	        	

	
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
                  let entity0Al2 = entity0.TCS_AMBIENTE
                  let entity0Al3 = entity0.TCS_AMBIENTE1
                  let entity0Al1 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al9 = entity0.TCS_AMBIENTE.TCS_APLICACAO
                  let entity0Al10 = entity0.TCS_AMBIENTE1.TCS_APLICACAO
                  let entity0Al6 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al7 = entity0.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al4 = entity0.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
                  let entity0Al5 = entity0.TCS_AMBIENTE1.TCS_APLICACAO.TCS_APLICATIVO
                  let entity0Al8 = entity0.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsUsuarioAcesso()		
	            {
	            
                Administrador = entity0.INDICA_ADMINISTRADOR
                , AutenticacaoWindows = entity0Al1.AUTENTICACAO_WINDOWS
                , DataExpiracaoSenha = entity0Al1.DATA_EXPIRACAO_SENHA
                , DescAmbiente = entity0Al2.DESCRICAO_AMBIENTE
                , DescAmbienteRelacionado = entity0Al3.DESCRICAO_AMBIENTE
                , DescAplicativo = entity0Al4.DESCRICAO_APLICATIVO
                , DescAplicativoAmbienteRelacionado = entity0Al5.DESCRICAO_APLICATIVO
                , DescEmpresa = entity0Al6.NOME_EMPRESA
                , DescEmpresaAmbienteRelacionado = entity0Al7.NOME_EMPRESA
                , DescGrupoEconomico = entity0Al8.NOME_EMPRESA
                , IdLinxAmbiente = entity0Al6.ID_LINX
                , IdLinxGrupoEconomico = entity0Al8.ID_LINX
                , IdTcsAmbiente = entity0Al2.ID_TCS_AMBIENTE
                , IdTcsAmbienteRelacionado = entity0Al3.ID_TCS_AMBIENTE
                , IdTcsAplicativo = entity0Al4.ID_TCS_APLICATIVO
                , IdTcsAplicativoAmbienteRelacionado = entity0Al5.ID_TCS_APLICATIVO
                , IdTcsUsuarioAcesso = entity0.ID_TCS_USUARIO_ACESSO
                , IdUsuario = entity0Al1.ID_USUARIO
                , MultiGpecon = entity0.INDICA_MULTI_GPECON
                , NomeCurtoUsuario = entity0Al1.NOME_CURTO_USUARIO
                , UidAplicacao = entity0Al9.UID_APLICACAO
                , UidAplicacaoAmbienteRelacionado = entity0Al10.UID_APLICACAO
                , UidEmpresa = entity0Al6.UID_EMPRESA
                , UidEmpresaAmbienteRelacionado = entity0Al7.UID_EMPRESA
                , UidGrupoEconomico = entity0Al8.UID_EMPRESA
                , UidUsuario = entity0Al1.UID_USUARIO
                , NomeAutenticacao = entity0Al1.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al1.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
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
                  let entity0Al2 = entity0.TCS_AMBIENTE
                  let entity0Al3 = entity0.TCS_AMBIENTE1
                  let entity0Al1 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al9 = entity0.TCS_AMBIENTE.TCS_APLICACAO
                  let entity0Al10 = entity0.TCS_AMBIENTE1.TCS_APLICACAO
                  let entity0Al6 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al7 = entity0.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al4 = entity0.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
                  let entity0Al5 = entity0.TCS_AMBIENTE1.TCS_APLICACAO.TCS_APLICATIVO
                  let entity0Al8 = entity0.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsUsuarioAcesso()		
	            {
	            
                Administrador = entity0.INDICA_ADMINISTRADOR
                , AutenticacaoWindows = entity0Al1.AUTENTICACAO_WINDOWS
                , DataExpiracaoSenha = entity0Al1.DATA_EXPIRACAO_SENHA
                , DescAmbiente = entity0Al2.DESCRICAO_AMBIENTE
                , DescAmbienteRelacionado = entity0Al3.DESCRICAO_AMBIENTE
                , DescAplicativo = entity0Al4.DESCRICAO_APLICATIVO
                , DescAplicativoAmbienteRelacionado = entity0Al5.DESCRICAO_APLICATIVO
                , DescEmpresa = entity0Al6.NOME_EMPRESA
                , DescEmpresaAmbienteRelacionado = entity0Al7.NOME_EMPRESA
                , DescGrupoEconomico = entity0Al8.NOME_EMPRESA
                , IdLinxAmbiente = entity0Al6.ID_LINX
                , IdLinxGrupoEconomico = entity0Al8.ID_LINX
                , IdTcsAmbiente = entity0Al2.ID_TCS_AMBIENTE
                , IdTcsAmbienteRelacionado = entity0Al3.ID_TCS_AMBIENTE
                , IdTcsAplicativo = entity0Al4.ID_TCS_APLICATIVO
                , IdTcsAplicativoAmbienteRelacionado = entity0Al5.ID_TCS_APLICATIVO
                , IdTcsUsuarioAcesso = entity0.ID_TCS_USUARIO_ACESSO
                , IdUsuario = entity0Al1.ID_USUARIO
                , MultiGpecon = entity0.INDICA_MULTI_GPECON
                , NomeCurtoUsuario = entity0Al1.NOME_CURTO_USUARIO
                , UidAplicacao = entity0Al9.UID_APLICACAO
                , UidAplicacaoAmbienteRelacionado = entity0Al10.UID_APLICACAO
                , UidEmpresa = entity0Al6.UID_EMPRESA
                , UidEmpresaAmbienteRelacionado = entity0Al7.UID_EMPRESA
                , UidGrupoEconomico = entity0Al8.UID_EMPRESA
                , UidUsuario = entity0Al1.UID_USUARIO
                , NomeAutenticacao = entity0Al1.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al1.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get AppInfoByEntitySearch.
	    public IEnumerable<AppInfo> GetAppInfoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<AppInfo> result = new List<AppInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get AppInfoByEntitySearchNoAssociations.
	    public IEnumerable<AppInfo> GetAppInfoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<AppInfo> result = new List<AppInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get GpeconInfoByEntitySearch.
	    public IEnumerable<GpeconInfo> GetGpeconInfoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<GpeconInfo> result = new List<GpeconInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get GpeconInfoByEntitySearchNoAssociations.
	    public IEnumerable<GpeconInfo> GetGpeconInfoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<GpeconInfo> result = new List<GpeconInfo>();
	  	
	
	        	

	
	        return result;
	    }
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get PagedAcesso.
	    public IEnumerable<Acesso> GetPagedAcesso(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		

	        IEnumerable<Acesso> result = 
	            (from entity0 in Acesso.OnSearchingReplacement(entitySearchList) select entity0);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetAcessoCounting(string serializedEntitySearch)
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
	    //Get PagedUserInfo.
	    public IEnumerable<UserInfo> GetPagedUserInfo(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<UserInfo> result = new List<UserInfo>();
	  	
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetUserInfoCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
			
	
	    
	    [Ignore]
	    //Get PagedLoginInfo.
	    public IEnumerable<LoginInfo> GetPagedLoginInfo(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<LoginInfo> result = new List<LoginInfo>();
	  	
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetLoginInfoCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
			
	
	    
	    [Ignore]
	    //Get PagedAmbienteInfo.
	    public IEnumerable<AmbienteInfo> GetPagedAmbienteInfo(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<AmbienteInfo> result = new List<AmbienteInfo>();
	  	
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetAmbienteInfoCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
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
                  let entity0Al2 = entity0.TCS_AMBIENTE
                  let entity0Al3 = entity0.TCS_AMBIENTE1
                  let entity0Al1 = entity0.TCS_USUARIO_AUTENTICACAO
                  let entity0Al9 = entity0.TCS_AMBIENTE.TCS_APLICACAO
                  let entity0Al10 = entity0.TCS_AMBIENTE1.TCS_APLICACAO
                  let entity0Al6 = entity0.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al7 = entity0.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO
                  let entity0Al4 = entity0.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
                  let entity0Al5 = entity0.TCS_AMBIENTE1.TCS_APLICACAO.TCS_APLICATIVO
                  let entity0Al8 = entity0.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO
                orderby entity0.ID_TCS_USUARIO_ACESSO ascending
	            
	            	
	            select new TcsUsuarioAcesso()		
	            {
	            
                Administrador = entity0.INDICA_ADMINISTRADOR
                , AutenticacaoWindows = entity0Al1.AUTENTICACAO_WINDOWS
                , DataExpiracaoSenha = entity0Al1.DATA_EXPIRACAO_SENHA
                , DescAmbiente = entity0Al2.DESCRICAO_AMBIENTE
                , DescAmbienteRelacionado = entity0Al3.DESCRICAO_AMBIENTE
                , DescAplicativo = entity0Al4.DESCRICAO_APLICATIVO
                , DescAplicativoAmbienteRelacionado = entity0Al5.DESCRICAO_APLICATIVO
                , DescEmpresa = entity0Al6.NOME_EMPRESA
                , DescEmpresaAmbienteRelacionado = entity0Al7.NOME_EMPRESA
                , DescGrupoEconomico = entity0Al8.NOME_EMPRESA
                , IdLinxAmbiente = entity0Al6.ID_LINX
                , IdLinxGrupoEconomico = entity0Al8.ID_LINX
                , IdTcsAmbiente = entity0Al2.ID_TCS_AMBIENTE
                , IdTcsAmbienteRelacionado = entity0Al3.ID_TCS_AMBIENTE
                , IdTcsAplicativo = entity0Al4.ID_TCS_APLICATIVO
                , IdTcsAplicativoAmbienteRelacionado = entity0Al5.ID_TCS_APLICATIVO
                , IdTcsUsuarioAcesso = entity0.ID_TCS_USUARIO_ACESSO
                , IdUsuario = entity0Al1.ID_USUARIO
                , MultiGpecon = entity0.INDICA_MULTI_GPECON
                , NomeCurtoUsuario = entity0Al1.NOME_CURTO_USUARIO
                , UidAplicacao = entity0Al9.UID_APLICACAO
                , UidAplicacaoAmbienteRelacionado = entity0Al10.UID_APLICACAO
                , UidEmpresa = entity0Al6.UID_EMPRESA
                , UidEmpresaAmbienteRelacionado = entity0Al7.UID_EMPRESA
                , UidGrupoEconomico = entity0Al8.UID_EMPRESA
                , UidUsuario = entity0Al1.UID_USUARIO
                , NomeAutenticacao = entity0Al1.NOME_AUTENTICACAO
                , NomeUsuario = entity0Al1.NOME_USUARIO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
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
                  let entityAl2 = entity.TCS_AMBIENTE
                  let entityAl3 = entity.TCS_AMBIENTE1
                  let entityAl1 = entity.TCS_USUARIO_AUTENTICACAO
                  let entityAl9 = entity.TCS_AMBIENTE.TCS_APLICACAO
                  let entityAl10 = entity.TCS_AMBIENTE1.TCS_APLICACAO
                  let entityAl6 = entity.TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO
                  let entityAl7 = entity.TCS_AMBIENTE1.TCS_EMPRESA_AUTENTICACAO
                  let entityAl4 = entity.TCS_AMBIENTE.TCS_APLICACAO.TCS_APLICATIVO
                  let entityAl5 = entity.TCS_AMBIENTE1.TCS_APLICACAO.TCS_APLICATIVO
                  let entityAl8 = entity.TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedAppInfo.
	    public IEnumerable<AppInfo> GetPagedAppInfo(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<AppInfo> result = new List<AppInfo>();
	  	
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetAppInfoCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
			
	
	    
	    [Ignore]
	    //Get PagedGpeconInfo.
	    public IEnumerable<GpeconInfo> GetPagedGpeconInfo(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<GpeconInfo> result = new List<GpeconInfo>();
	  	
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetGpeconInfoCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    
	    [Update()]	
	    //Update Acesso.
	    public void UpdateAcesso(Acesso entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert Acesso.
	    public void InsertAcesso(Acesso entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete Acesso.
	    public void DeleteAcesso(Acesso entity)
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
	    //Update UserInfo.
	    public void UpdateUserInfo(UserInfo entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert UserInfo.
	    public void InsertUserInfo(UserInfo entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete UserInfo.
	    public void DeleteUserInfo(UserInfo entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update LoginInfo.
	    public void UpdateLoginInfo(LoginInfo entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert LoginInfo.
	    public void InsertLoginInfo(LoginInfo entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete LoginInfo.
	    public void DeleteLoginInfo(LoginInfo entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update AmbienteInfo.
	    public void UpdateAmbienteInfo(AmbienteInfo entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert AmbienteInfo.
	    public void InsertAmbienteInfo(AmbienteInfo entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete AmbienteInfo.
	    public void DeleteAmbienteInfo(AmbienteInfo entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsUsuarioAcesso.
	    public void UpdateTcsUsuarioAcesso(TcsUsuarioAcesso entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsUsuarioAcesso.
	    public void InsertTcsUsuarioAcesso(TcsUsuarioAcesso entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsUsuarioAcesso.
	    public void DeleteTcsUsuarioAcesso(TcsUsuarioAcesso entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update AppInfo.
	    public void UpdateAppInfo(AppInfo entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert AppInfo.
	    public void InsertAppInfo(AppInfo entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete AppInfo.
	    public void DeleteAppInfo(AppInfo entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update GpeconInfo.
	    public void UpdateGpeconInfo(GpeconInfo entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert GpeconInfo.
	    public void InsertGpeconInfo(GpeconInfo entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete GpeconInfo.
	    public void DeleteGpeconInfo(GpeconInfo entity)
	    {



	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}