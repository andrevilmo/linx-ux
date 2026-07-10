					
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

namespace Linx.Framework.BV.UsuarioExterno
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_USUARIO_EXTERNO.ID_TCS_USUARIO_EXTERNO", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsUsuarioExterno];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsUsuarioExterno];ReadOnly[false];Entities[TCS_USUARIO_EXTERNO:IdTcsUsuarioExterno];SubQueryInfo[];EdmEntityName[TCS_USUARIO_EXTERNO];EntityRelations[];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioExterno")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.UsuarioExterno.TcsUsuarioExterno")]
	public partial class TcsUsuarioExterno : Linx.Data.Entity
	{

	

	    public TcsUsuarioExterno() : this(true) { }

	    public TcsUsuarioExterno(bool setDefaults) 
	    {

	        if (setDefaults)
	        {
	        	        DataCadastro = DateTime.Now;
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
	 

	    //Extensibility Partial Method Definitions For CnpjCpf
	    partial void OnCnpjCpfChanging(System.String value);
	    partial void OnCnpjCpfChanged();

	    private System.String _CnpjCpf;

	    [DataMember(Name = "CnpjCpf", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "CPF / CNPJ", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_EXTERNO.CNPJ_CPF];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_EXTERNO.CNPJ_CPF")]
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
	    //Extensibility Partial Method Definitions For DataCadastro
	    partial void OnDataCadastroChanging(System.DateTime value);
	    partial void OnDataCadastroChanged();

	    private System.DateTime _DataCadastro;

	    [DataMember(IsRequired = true, Name = "DataCadastro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Data Cadastro", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[DateTime.Now];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_EXTERNO.DATA_CADASTRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_EXTERNO.DATA_CADASTRO")]
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
	    //Extensibility Partial Method Definitions For Email
	    partial void OnEmailChanging(System.String value);
	    partial void OnEmailChanged();

	    private System.String _Email;

	    [DataMember(IsRequired = true, Name = "Email", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Email", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_EXTERNO.EMAIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_EXTERNO.EMAIL")]
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
	    [Display(Name = "Celular", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_EXTERNO.FONE_CELULAR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_EXTERNO.FONE_CELULAR")]
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
	    //Extensibility Partial Method Definitions For IdDispositivo
	    partial void OnIdDispositivoChanging(System.String value);
	    partial void OnIdDispositivoChanged();

	    private System.String _IdDispositivo;

	    [DataMember(IsRequired = true, Name = "IdDispositivo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Dispositivo", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(256)]
	    [FunctionalPoint("Precision[256:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_EXTERNO.ID_DISPOSITIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_EXTERNO.ID_DISPOSITIVO")]
	    public System.String IdDispositivo
	    {
	    	    get
	    	    {
	    	          return _IdDispositivo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdDispositivo != value)
	    	          {
	    	              this.ValidateProperty("IdDispositivo", value);
	    	              this.OnIdDispositivoChanging(value);
	    	              this.RaiseDataMemberChanging("IdDispositivo");
	    	              this._IdDispositivo = value;
	    	              this.RaiseDataMemberChanged("IdDispositivo");
	    	              this.OnIdDispositivoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdentidadeExterna
	    partial void OnIdentidadeExternaChanging(System.String value);
	    partial void OnIdentidadeExternaChanged();

	    private System.String _IdentidadeExterna;

	    [DataMember(Name = "IdentidadeExterna", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Externo", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_EXTERNO.IDENTIDADE_EXTERNA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_EXTERNO.IDENTIDADE_EXTERNA")]
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
	    //Extensibility Partial Method Definitions For IdTcsUsuarioExterno
	    partial void OnIdTcsUsuarioExternoChanging(Int64 value);
	    partial void OnIdTcsUsuarioExternoChanged();

	    private Int64 _IdTcsUsuarioExterno;

	    [DataMember(IsRequired = true, Name = "IdTcsUsuarioExterno", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Externo", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_EXTERNO.ID_TCS_USUARIO_EXTERNO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_EXTERNO.ID_TCS_USUARIO_EXTERNO")]
	    public Int64 IdTcsUsuarioExterno
	    {
	    	    get
	    	    {
	    	          return _IdTcsUsuarioExterno;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsUsuarioExterno != value)
	    	          {
	    	              this.ValidateProperty("IdTcsUsuarioExterno", value);
	    	              this.OnIdTcsUsuarioExternoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsUsuarioExterno");
	    	              this._IdTcsUsuarioExterno = value;
	    	              this.RaiseDataMemberChanged("IdTcsUsuarioExterno");
	    	              this.OnIdTcsUsuarioExternoChanged();
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
	    [Display(Name = "Inativo", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_EXTERNO.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_EXTERNO.INATIVO")]
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
	    //Extensibility Partial Method Definitions For LxTipoAutenticador
	    partial void OnLxTipoAutenticadorChanging(System.Nullable<System.Byte> value);
	    partial void OnLxTipoAutenticadorChanged();

	    private System.Nullable<System.Byte> _LxTipoAutenticador;

	    [DataMember(Name = "LxTipoAutenticador", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Autenticador", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[TipoAutenticador];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_EXTERNO.LX_TIPO_AUTENTICADOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_EXTERNO.LX_TIPO_AUTENTICADOR")]
	    public System.Nullable<System.Byte> LxTipoAutenticador
	    {
	    	    get
	    	    {
	    	          return _LxTipoAutenticador;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoAutenticador != value)
	    	          {
	    	              this.ValidateProperty("LxTipoAutenticador", value);
	    	              this.OnLxTipoAutenticadorChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoAutenticador");
	    	              this._LxTipoAutenticador = value;
	    	              this.RaiseDataMemberChanged("LxTipoAutenticador");
	    	              this.OnLxTipoAutenticadorChanged();
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
	    [Display(Name = "Usuário", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_EXTERNO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_EXTERNO.NOME_USUARIO")]
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

	    private Int64 _TemporaryIdTcsUsuarioExterno;
	    [DataMember(Name = "TemporaryIdTcsUsuarioExterno", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Externo (Tmp)", Description="Temporary Key", Order = 6, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdTcsUsuarioExterno
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTcsUsuarioExterno.IsNullOrEmpty())
	    	                this._TemporaryIdTcsUsuarioExterno = this._IdTcsUsuarioExterno;
	    	          return this._TemporaryIdTcsUsuarioExterno;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTcsUsuarioExterno != value)
	    	              this._TemporaryIdTcsUsuarioExterno = value;
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_USUARIO_EXTERNO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = true, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_USUARIO_EXTERNO), QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_EXTERNO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_EXTERNO.EMAIL", Source = "Email", Target = "EMAIL", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_EXTERNO", RelationPropertyName = "TCS_USUARIO_EXTERNO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_EXTERNO.INATIVO", Source = "Inativo", Target = "INATIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_EXTERNO", RelationPropertyName = "TCS_USUARIO_EXTERNO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_EXTERNO.CNPJ_CPF", Source = "CnpjCpf", Target = "CNPJ_CPF", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_EXTERNO", RelationPropertyName = "TCS_USUARIO_EXTERNO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_EXTERNO.FONE_CELULAR", Source = "FoneCelular", Target = "FONE_CELULAR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_EXTERNO", RelationPropertyName = "TCS_USUARIO_EXTERNO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_EXTERNO.NOME_USUARIO", Source = "NomeUsuario", Target = "NOME_USUARIO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_EXTERNO", RelationPropertyName = "TCS_USUARIO_EXTERNO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_EXTERNO.DATA_CADASTRO", Source = "DataCadastro", Target = "DATA_CADASTRO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_EXTERNO", RelationPropertyName = "TCS_USUARIO_EXTERNO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_EXTERNO.ID_DISPOSITIVO", Source = "IdDispositivo", Target = "ID_DISPOSITIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_EXTERNO", RelationPropertyName = "TCS_USUARIO_EXTERNO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_EXTERNO.IDENTIDADE_EXTERNA", Source = "IdentidadeExterna", Target = "IDENTIDADE_EXTERNA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_EXTERNO", RelationPropertyName = "TCS_USUARIO_EXTERNO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_EXTERNO.LX_TIPO_AUTENTICADOR", Source = "LxTipoAutenticador", Target = "LX_TIPO_AUTENTICADOR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_EXTERNO", RelationPropertyName = "TCS_USUARIO_EXTERNO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_USUARIO_EXTERNO.ID_TCS_USUARIO_EXTERNO", Source = "IdTcsUsuarioExterno", Target = "ID_TCS_USUARIO_EXTERNO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO_EXTERNO", RelationPropertyName = "TCS_USUARIO_EXTERNO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxTipoAutenticadorValues()
	    {
	    	    return Linx.Framework.BV.Domains.TipoAutenticador.GetValues();
	    }
	    private string _lxTipoAutenticadorName;
	    [DataMember(IsRequired = false, Name = "LxTipoAutenticadorName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Autenticador", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxTipoAutenticadorName
	    {
	    	    get { if (this.LxTipoAutenticador.IsNull()) { _lxTipoAutenticadorName = String.Empty; } else { string key = this.LxTipoAutenticador.ToString(); var dmValues = this.GetLxTipoAutenticadorValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxTipoAutenticadorName) _lxTipoAutenticadorName = domainName; } return _lxTipoAutenticadorName; } set { _lxTipoAutenticadorName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	//////////////////////// DomainService Class V1 ///////////////////////
	///////////////////////////////////////////////////////////////////////
	[EnableClientAccess()]	
	[DomainIdentifier("ProcessorOverviewUsuarioExternoDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class UsuarioExternoDomainService : DomainService, IDataServiceContext 
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

		
	    public UsuarioExternoDomainService() : this("", null, null) { }
	    public UsuarioExternoDomainService(string connectionString) : this(connectionString, null, null) { }
	    public UsuarioExternoDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public UsuarioExternoDomainService(Linx.Framework.ControleSistema.BM.ControleSistemaContext dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public UsuarioExternoDomainService(string connectionString, Linx.Framework.ControleSistema.BM.ControleSistemaContext dataContext, Dictionary<string, string> headers) : base() 
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
	
		

	        if (entityName.InList("Linx.Framework.BV.UsuarioExterno.TcsUsuarioExterno"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsUsuarioExterno",
	        			NameSpace = "Linx.Framework.BV.UsuarioExterno",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsUsuarioExterno",
	        			ClearMethodName = "ClearTcsUsuarioExterno",
	        			QueryMethodName  = "GetPagedTcsUsuarioExterno",	
	        			CountingMethodName  = "GetTcsUsuarioExterno" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.UsuarioExterno.TcsUsuarioExterno"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.UsuarioExterno.TcsUsuarioExterno"), forceAll: forceAll)
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

         		    return new string[] { "Framework_UsuarioExternoClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.UsuarioExternoClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Framework_usuarioExternoService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.usuarioExternoService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear TcsUsuarioExterno.
	    public IEnumerable<TcsUsuarioExterno> ClearTcsUsuarioExterno()
	    {
	        List<TcsUsuarioExterno> result = new List<TcsUsuarioExterno>();
	        result.Add(new TcsUsuarioExterno(false));	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsUsuarioExterno.
	    public IQueryable<TcsUsuarioExterno> GetTcsUsuarioExterno()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioExterno> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_EXTERNO
	            
	            	
	            select new TcsUsuarioExterno()		
	            {
	            
                CnpjCpf = entity0.CNPJ_CPF
                , DataCadastro = entity0.DATA_CADASTRO
                , Email = entity0.EMAIL
                , FoneCelular = entity0.FONE_CELULAR
                , IdDispositivo = entity0.ID_DISPOSITIVO
                , IdentidadeExterna = entity0.IDENTIDADE_EXTERNA
                , IdTcsUsuarioExterno = entity0.ID_TCS_USUARIO_EXTERNO
                , Inativo = entity0.INATIVO
                , LxTipoAutenticador = entity0.LX_TIPO_AUTENTICADOR
                , LxTipoAutenticadorName = ((entity0.LX_TIPO_AUTENTICADOR) == 1 ? "Facebook" : ((entity0.LX_TIPO_AUTENTICADOR) == 2 ? "Google+" : ((entity0.LX_TIPO_AUTENTICADOR) == 4 ? "Linx" : ((entity0.LX_TIPO_AUTENTICADOR) == 3 ? "Microsoft Sign In" : ""))))
                , NomeUsuario = entity0.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioExternoNoAssociations.
	    public IQueryable<TcsUsuarioExterno> GetTcsUsuarioExternoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioExterno> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_EXTERNO
	            
	            	
	            select new TcsUsuarioExterno()		
	            {
	            
                CnpjCpf = entity0.CNPJ_CPF
                , DataCadastro = entity0.DATA_CADASTRO
                , Email = entity0.EMAIL
                , FoneCelular = entity0.FONE_CELULAR
                , IdDispositivo = entity0.ID_DISPOSITIVO
                , IdentidadeExterna = entity0.IDENTIDADE_EXTERNA
                , IdTcsUsuarioExterno = entity0.ID_TCS_USUARIO_EXTERNO
                , Inativo = entity0.INATIVO
                , LxTipoAutenticador = entity0.LX_TIPO_AUTENTICADOR
                , LxTipoAutenticadorName = ((entity0.LX_TIPO_AUTENTICADOR) == 1 ? "Facebook" : ((entity0.LX_TIPO_AUTENTICADOR) == 2 ? "Google+" : ((entity0.LX_TIPO_AUTENTICADOR) == 4 ? "Linx" : ((entity0.LX_TIPO_AUTENTICADOR) == 3 ? "Microsoft Sign In" : ""))))
                , NomeUsuario = entity0.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	//Add filtering disabled property for TCS_USUARIO_EXTERNO
	    	string[] bmDisabledTcsUsuarioExternoList = this.GetEDM().GetFilteringDisabledList("TCS_USUARIO_EXTERNO");
	    	if (bmDisabledTcsUsuarioExternoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsUsuarioExternoList.Contains("TCS_USUARIO_EXTERNO.CNPJ_CPF"))
	    		{
	    			result.Add("TcsUsuarioExterno|CnpjCpf");
	    			result.Add("TcsUsuarioExterno|TCS_USUARIO_EXTERNO.CNPJ_CPF");
	    		}
	
	    		if (bmDisabledTcsUsuarioExternoList.Contains("TCS_USUARIO_EXTERNO.DATA_CADASTRO"))
	    		{
	    			result.Add("TcsUsuarioExterno|DataCadastro");
	    			result.Add("TcsUsuarioExterno|TCS_USUARIO_EXTERNO.DATA_CADASTRO");
	    		}
	
	    		if (bmDisabledTcsUsuarioExternoList.Contains("TCS_USUARIO_EXTERNO.EMAIL"))
	    		{
	    			result.Add("TcsUsuarioExterno|Email");
	    			result.Add("TcsUsuarioExterno|TCS_USUARIO_EXTERNO.EMAIL");
	    		}
	
	    		if (bmDisabledTcsUsuarioExternoList.Contains("TCS_USUARIO_EXTERNO.FONE_CELULAR"))
	    		{
	    			result.Add("TcsUsuarioExterno|FoneCelular");
	    			result.Add("TcsUsuarioExterno|TCS_USUARIO_EXTERNO.FONE_CELULAR");
	    		}
	
	    		if (bmDisabledTcsUsuarioExternoList.Contains("TCS_USUARIO_EXTERNO.ID_DISPOSITIVO"))
	    		{
	    			result.Add("TcsUsuarioExterno|IdDispositivo");
	    			result.Add("TcsUsuarioExterno|TCS_USUARIO_EXTERNO.ID_DISPOSITIVO");
	    		}
	
	    		if (bmDisabledTcsUsuarioExternoList.Contains("TCS_USUARIO_EXTERNO.IDENTIDADE_EXTERNA"))
	    		{
	    			result.Add("TcsUsuarioExterno|IdentidadeExterna");
	    			result.Add("TcsUsuarioExterno|TCS_USUARIO_EXTERNO.IDENTIDADE_EXTERNA");
	    		}
	
	    		if (bmDisabledTcsUsuarioExternoList.Contains("TCS_USUARIO_EXTERNO.ID_TCS_USUARIO_EXTERNO"))
	    		{
	    			result.Add("TcsUsuarioExterno|IdTcsUsuarioExterno");
	    			result.Add("TcsUsuarioExterno|TCS_USUARIO_EXTERNO.ID_TCS_USUARIO_EXTERNO");
	    		}
	
	    		if (bmDisabledTcsUsuarioExternoList.Contains("TCS_USUARIO_EXTERNO.INATIVO"))
	    		{
	    			result.Add("TcsUsuarioExterno|Inativo");
	    			result.Add("TcsUsuarioExterno|TCS_USUARIO_EXTERNO.INATIVO");
	    		}
	
	    		if (bmDisabledTcsUsuarioExternoList.Contains("TCS_USUARIO_EXTERNO.LX_TIPO_AUTENTICADOR"))
	    		{
	    			result.Add("TcsUsuarioExterno|LxTipoAutenticador");
	    			result.Add("TcsUsuarioExterno|TCS_USUARIO_EXTERNO.LX_TIPO_AUTENTICADOR");
	    		}
	
	    		if (bmDisabledTcsUsuarioExternoList.Contains("TCS_USUARIO_EXTERNO.NOME_USUARIO"))
	    		{
	    			result.Add("TcsUsuarioExterno|NomeUsuario");
	    			result.Add("TcsUsuarioExterno|TCS_USUARIO_EXTERNO.NOME_USUARIO");
	    		}
	    	}
	    	return result;
	    }

	    #endregion Excluded Filters

	    #region Get By EntitySearchId Methods Definitions.
	
				
	    [Ignore]
	    //Get TcsUsuarioExterno By EntitySearchId.
	    public IQueryable<TcsUsuarioExterno> GetTcsUsuarioExternoByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioExternoByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioExterno By EntitySearchId.
	    public IQueryable<TcsUsuarioExterno> GetTcsUsuarioExternoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsUsuarioExternoByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get TcsUsuarioExterno By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioExterno> GetTcsUsuarioExternoByExample(TcsUsuarioExterno entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioExternoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsUsuarioExterno By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioExterno> GetTcsUsuarioExternoByExampleNoAssociations(TcsUsuarioExterno entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioExternoByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public TcsUsuarioExterno GetTcsUsuarioExternoByKey(Int64 idTcsUsuarioExterno)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsUsuarioExterno");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsUsuarioExterno"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsUsuarioExterno));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsUsuarioExternoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioExternoByEntitySearch.
	    public IQueryable<TcsUsuarioExterno> GetTcsUsuarioExternoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioExterno));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioExterno> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_EXTERNO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsUsuarioExterno()		
	            {
	            
                CnpjCpf = entity0.CNPJ_CPF
                , DataCadastro = entity0.DATA_CADASTRO
                , Email = entity0.EMAIL
                , FoneCelular = entity0.FONE_CELULAR
                , IdDispositivo = entity0.ID_DISPOSITIVO
                , IdentidadeExterna = entity0.IDENTIDADE_EXTERNA
                , IdTcsUsuarioExterno = entity0.ID_TCS_USUARIO_EXTERNO
                , Inativo = entity0.INATIVO
                , LxTipoAutenticador = entity0.LX_TIPO_AUTENTICADOR
                , LxTipoAutenticadorName = ((entity0.LX_TIPO_AUTENTICADOR) == 1 ? "Facebook" : ((entity0.LX_TIPO_AUTENTICADOR) == 2 ? "Google+" : ((entity0.LX_TIPO_AUTENTICADOR) == 4 ? "Linx" : ((entity0.LX_TIPO_AUTENTICADOR) == 3 ? "Microsoft Sign In" : ""))))
                , NomeUsuario = entity0.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioExternoByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioExterno> GetTcsUsuarioExternoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioExterno));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioExterno> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_EXTERNO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsUsuarioExterno()		
	            {
	            
                CnpjCpf = entity0.CNPJ_CPF
                , DataCadastro = entity0.DATA_CADASTRO
                , Email = entity0.EMAIL
                , FoneCelular = entity0.FONE_CELULAR
                , IdDispositivo = entity0.ID_DISPOSITIVO
                , IdentidadeExterna = entity0.IDENTIDADE_EXTERNA
                , IdTcsUsuarioExterno = entity0.ID_TCS_USUARIO_EXTERNO
                , Inativo = entity0.INATIVO
                , LxTipoAutenticador = entity0.LX_TIPO_AUTENTICADOR
                , LxTipoAutenticadorName = ((entity0.LX_TIPO_AUTENTICADOR) == 1 ? "Facebook" : ((entity0.LX_TIPO_AUTENTICADOR) == 2 ? "Google+" : ((entity0.LX_TIPO_AUTENTICADOR) == 4 ? "Linx" : ((entity0.LX_TIPO_AUTENTICADOR) == 3 ? "Microsoft Sign In" : ""))))
                , NomeUsuario = entity0.NOME_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get PagedTcsUsuarioExterno.
	    public IQueryable<TcsUsuarioExterno> GetPagedTcsUsuarioExterno(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioExterno));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsUsuarioExterno> result = 
	            (from entity0 in this.DbContext.TCS_USUARIO_EXTERNO.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_TCS_USUARIO_EXTERNO ascending
	            
	            	
	            select new TcsUsuarioExterno()		
	            {
	            
                CnpjCpf = entity0.CNPJ_CPF
                , DataCadastro = entity0.DATA_CADASTRO
                , Email = entity0.EMAIL
                , FoneCelular = entity0.FONE_CELULAR
                , IdDispositivo = entity0.ID_DISPOSITIVO
                , IdentidadeExterna = entity0.IDENTIDADE_EXTERNA
                , IdTcsUsuarioExterno = entity0.ID_TCS_USUARIO_EXTERNO
                , Inativo = entity0.INATIVO
                , LxTipoAutenticador = entity0.LX_TIPO_AUTENTICADOR
                , LxTipoAutenticadorName = ((entity0.LX_TIPO_AUTENTICADOR) == 1 ? "Facebook" : ((entity0.LX_TIPO_AUTENTICADOR) == 2 ? "Google+" : ((entity0.LX_TIPO_AUTENTICADOR) == 4 ? "Linx" : ((entity0.LX_TIPO_AUTENTICADOR) == 3 ? "Microsoft Sign In" : ""))))
                , NomeUsuario = entity0.NOME_USUARIO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsUsuarioExternoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsUsuarioExterno));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_USUARIO_EXTERNO.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    
	    [Update()]	
	    //Update TcsUsuarioExterno.
	    public void UpdateTcsUsuarioExterno(TcsUsuarioExterno entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsUsuarioExterno.
	    public void InsertTcsUsuarioExterno(TcsUsuarioExterno entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsUsuarioExterno.
	    public void DeleteTcsUsuarioExterno(TcsUsuarioExterno entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}