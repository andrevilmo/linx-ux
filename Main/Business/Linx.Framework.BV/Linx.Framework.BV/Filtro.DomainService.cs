					
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

namespace Linx.Framework.BV.Filtro
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_FILTRO.ID_FILTRO", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsFiltro];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdFiltro];ReadOnly[false];Entities[TCS_FILTRO:IdFiltro];SubQueryInfo[];EdmEntityName[TCS_FILTRO];EntityRelations[TCS_USUARIO(TCS_USUARIO)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsFiltro")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Filtro.TcsFiltro")]
	public partial class TcsFiltro : Linx.Data.Entity
	{

	

	    public TcsFiltro() : this(true) { }

	    public TcsFiltro(bool setDefaults) 
	    {

	        if (setDefaults)
	        {
	        	        LxTipoFiltro = 1;
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
	 

	    //Extensibility Partial Method Definitions For ComandoFiltro
	    partial void OnComandoFiltroChanging(System.String value);
	    partial void OnComandoFiltroChanged();

	    private System.String _ComandoFiltro;

	    [DataMember(IsRequired = true, Name = "ComandoFiltro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Comando Filtro", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_FILTRO.COMANDO_FILTRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_FILTRO.COMANDO_FILTRO")]
	    public System.String ComandoFiltro
	    {
	    	    get
	    	    {
	    	          return _ComandoFiltro;
	    	    }
	    	    set
	    	    {
	    	          if (this._ComandoFiltro != value)
	    	          {
	    	              this.ValidateProperty("ComandoFiltro", value);
	    	              this.OnComandoFiltroChanging(value);
	    	              this.RaiseDataMemberChanging("ComandoFiltro");
	    	              this._ComandoFiltro = value;
	    	              this.RaiseDataMemberChanged("ComandoFiltro");
	    	              this.OnComandoFiltroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ComandoSerializado
	    partial void OnComandoSerializadoChanging(System.String value);
	    partial void OnComandoSerializadoChanged();

	    private System.String _ComandoSerializado;

	    [DataMember(IsRequired = true, Name = "ComandoSerializado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Comando Serializado", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_FILTRO.COMANDO_SERIALIZADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_FILTRO.COMANDO_SERIALIZADO")]
	    public System.String ComandoSerializado
	    {
	    	    get
	    	    {
	    	          return _ComandoSerializado;
	    	    }
	    	    set
	    	    {
	    	          if (this._ComandoSerializado != value)
	    	          {
	    	              this.ValidateProperty("ComandoSerializado", value);
	    	              this.OnComandoSerializadoChanging(value);
	    	              this.RaiseDataMemberChanging("ComandoSerializado");
	    	              this._ComandoSerializado = value;
	    	              this.RaiseDataMemberChanged("ComandoSerializado");
	    	              this.OnComandoSerializadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescFiltro
	    partial void OnDescFiltroChanging(System.String value);
	    partial void OnDescFiltroChanged();

	    private System.String _DescFiltro;

	    [DataMember(IsRequired = true, Name = "DescFiltro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Desc Filtro", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_FILTRO.DESC_FILTRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_FILTRO.DESC_FILTRO")]
	    public System.String DescFiltro
	    {
	    	    get
	    	    {
	    	          return _DescFiltro;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescFiltro != value)
	    	          {
	    	              this.ValidateProperty("DescFiltro", value);
	    	              this.OnDescFiltroChanging(value);
	    	              this.RaiseDataMemberChanging("DescFiltro");
	    	              this._DescFiltro = value;
	    	              this.RaiseDataMemberChanged("DescFiltro");
	    	              this.OnDescFiltroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdFiltro
	    partial void OnIdFiltroChanging(Int64 value);
	    partial void OnIdFiltroChanged();

	    private Int64 _IdFiltro;

	    [DataMember(IsRequired = true, Name = "IdFiltro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Filtro", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_FILTRO.ID_FILTRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_FILTRO.ID_FILTRO")]
	    public Int64 IdFiltro
	    {
	    	    get
	    	    {
	    	          return _IdFiltro;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdFiltro != value)
	    	          {
	    	              this.ValidateProperty("IdFiltro", value);
	    	              this.OnIdFiltroChanging(value);
	    	              this.RaiseDataMemberChanging("IdFiltro");
	    	              this._IdFiltro = value;
	    	              this.RaiseDataMemberChanged("IdFiltro");
	    	              this.OnIdFiltroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuario
	    partial void OnIdUsuarioChanging(System.Nullable<Int64> value);
	    partial void OnIdUsuarioChanged();

	    private System.Nullable<Int64> _IdUsuario;

	    [DataMember(Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuario];LookUpTitle[Seleção de (Id Usuario)];LookUpQuery[executeLookUpTcsUsuario];LookUpFinalize[finalizeLookUpTcsUsuario];LookUpDisplayColumns[{\"NomeUsuario\" : \"Nome Usuario\", \"UidUsuario\" : \"Uid Usuario\", \"IdUsuario\" : \"Id Usuario\"}];LookUpColumns[{\"NomeUsuario\" : true, \"UidUsuario\" : true, \"IdUsuario\" : true}];FilterDataKey[TCS_FILTRO.TCS_USUARIO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<Int64>#IdUsuario#true##24:0##Id Usuario#2#true##::LookUpTcsUsuario##false#false#TCS_USUARIO#TCS_USUARIO#Linx.Framework.BV.Filtro#IQueryable###true#false", EdmKey="TCS_FILTRO.TCS_USUARIO.ID_USUARIO")]
	    public System.Nullable<Int64> IdUsuario
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
	    //Extensibility Partial Method Definitions For IndicaUsoLinx
	    partial void OnIndicaUsoLinxChanging(Boolean value);
	    partial void OnIndicaUsoLinxChanged();

	    private Boolean _IndicaUsoLinx;

	    [DataMember(IsRequired = true, Name = "IndicaUsoLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Indica Uso Linx", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_FILTRO.INDICA_USO_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_FILTRO.INDICA_USO_LINX")]
	    public Boolean IndicaUsoLinx
	    {
	    	    get
	    	    {
	    	          return _IndicaUsoLinx;
	    	    }
	    	    set
	    	    {
	    	          if (this._IndicaUsoLinx != value)
	    	          {
	    	              this.ValidateProperty("IndicaUsoLinx", value);
	    	              this.OnIndicaUsoLinxChanging(value);
	    	              this.RaiseDataMemberChanging("IndicaUsoLinx");
	    	              this._IndicaUsoLinx = value;
	    	              this.RaiseDataMemberChanged("IndicaUsoLinx");
	    	              this.OnIndicaUsoLinxChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoFiltro
	    partial void OnLxTipoFiltroChanging(Byte value);
	    partial void OnLxTipoFiltroChanged();

	    private Byte _LxTipoFiltro;

	    [DataMember(IsRequired = true, Name = "LxTipoFiltro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Lx Tipo Filtro", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoFiltro];KpiName[];KpiRelatedAttribute[];DefaultValue[1];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_FILTRO.LX_TIPO_FILTRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_FILTRO.LX_TIPO_FILTRO")]
	    public Byte LxTipoFiltro
	    {
	    	    get
	    	    {
	    	          return _LxTipoFiltro;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoFiltro != value)
	    	          {
	    	              this.ValidateProperty("LxTipoFiltro", value);
	    	              this.OnLxTipoFiltroChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoFiltro");
	    	              this._LxTipoFiltro = value;
	    	              this.RaiseDataMemberChanged("LxTipoFiltro");
	    	              this.OnLxTipoFiltroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeEntidadeBm
	    partial void OnNomeEntidadeBmChanging(System.String value);
	    partial void OnNomeEntidadeBmChanged();

	    private System.String _NomeEntidadeBm;

	    [DataMember(Name = "NomeEntidadeBm", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Entidade Bm", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_FILTRO.NOME_ENTIDADE_BM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_FILTRO.NOME_ENTIDADE_BM")]
	    public System.String NomeEntidadeBm
	    {
	    	    get
	    	    {
	    	          return _NomeEntidadeBm;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeEntidadeBm != value)
	    	          {
	    	              this.ValidateProperty("NomeEntidadeBm", value);
	    	              this.OnNomeEntidadeBmChanging(value);
	    	              this.RaiseDataMemberChanging("NomeEntidadeBm");
	    	              this._NomeEntidadeBm = value;
	    	              this.RaiseDataMemberChanged("NomeEntidadeBm");
	    	              this.OnNomeEntidadeBmChanged();
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
	    [Display(Name = "Nome Usuario", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuario];LookUpTitle[Seleção de (Nome Usuario)];LookUpQuery[executeLookUpTcsUsuario];LookUpFinalize[finalizeLookUpTcsUsuario];LookUpDisplayColumns[{\"NomeUsuario\" : \"Nome Usuario\", \"UidUsuario\" : \"Uid Usuario\", \"IdUsuario\" : \"Id Usuario\"}];LookUpColumns[{\"NomeUsuario\" : true, \"UidUsuario\" : true, \"IdUsuario\" : true}];FilterDataKey[TCS_FILTRO.TCS_USUARIO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeUsuario#false##250:0##Nome Usuario#0#true##::LookUpTcsUsuario##false#false#TCS_USUARIO#TCS_USUARIO#Linx.Framework.BV.Filtro#IQueryable###true#false", EdmKey="TCS_FILTRO.TCS_USUARIO.NOME_USUARIO")]
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
	    //Extensibility Partial Method Definitions For ObsFiltro
	    partial void OnObsFiltroChanging(System.String value);
	    partial void OnObsFiltroChanged();

	    private System.String _ObsFiltro;

	    [DataMember(Name = "ObsFiltro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Obs Filtro", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_FILTRO.OBS_FILTRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_FILTRO.OBS_FILTRO")]
	    public System.String ObsFiltro
	    {
	    	    get
	    	    {
	    	          return _ObsFiltro;
	    	    }
	    	    set
	    	    {
	    	          if (this._ObsFiltro != value)
	    	          {
	    	              this.ValidateProperty("ObsFiltro", value);
	    	              this.OnObsFiltroChanging(value);
	    	              this.RaiseDataMemberChanging("ObsFiltro");
	    	              this._ObsFiltro = value;
	    	              this.RaiseDataMemberChanged("ObsFiltro");
	    	              this.OnObsFiltroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Parametros
	    partial void OnParametrosChanging(System.String value);
	    partial void OnParametrosChanged();

	    private System.String _Parametros;

	    [DataMember(Name = "Parametros", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Parametros", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_FILTRO.PARAMETROS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_FILTRO.PARAMETROS")]
	    public System.String Parametros
	    {
	    	    get
	    	    {
	    	          return _Parametros;
	    	    }
	    	    set
	    	    {
	    	          if (this._Parametros != value)
	    	          {
	    	              this.ValidateProperty("Parametros", value);
	    	              this.OnParametrosChanging(value);
	    	              this.RaiseDataMemberChanging("Parametros");
	    	              this._Parametros = value;
	    	              this.RaiseDataMemberChanged("Parametros");
	    	              this.OnParametrosChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidUsuario
	    partial void OnUidUsuarioChanging(System.Nullable<System.Guid> value);
	    partial void OnUidUsuarioChanged();

	    private System.Nullable<System.Guid> _UidUsuario;

	    [DataMember(Name = "UidUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Usuario", Description="", Order = 5, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuario];LookUpTitle[Seleção de (Uid Usuario)];LookUpQuery[executeLookUpTcsUsuario];LookUpFinalize[finalizeLookUpTcsUsuario];LookUpDisplayColumns[{\"NomeUsuario\" : \"Nome Usuario\", \"UidUsuario\" : \"Uid Usuario\", \"IdUsuario\" : \"Id Usuario\"}];LookUpColumns[{\"NomeUsuario\" : true, \"UidUsuario\" : true, \"IdUsuario\" : true}];FilterDataKey[TCS_FILTRO.TCS_USUARIO.UID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<System.Guid>#UidUsuario#false##12:0##Uid Usuario#1#true##::LookUpTcsUsuario##false#false#TCS_USUARIO#TCS_USUARIO#Linx.Framework.BV.Filtro#IQueryable###true#false", EdmKey="TCS_FILTRO.TCS_USUARIO.UID_USUARIO")]
	    public System.Nullable<System.Guid> UidUsuario
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

	    private Int64 _TemporaryIdFiltro;
	    [DataMember(Name = "TemporaryIdFiltro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Filtro (Tmp)", Description="Temporary Key", Order = 4, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdFiltro
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdFiltro.IsNullOrEmpty())
	    	                this._TemporaryIdFiltro = this._IdFiltro;
	    	          return this._TemporaryIdFiltro;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdFiltro != value)
	    	              this._TemporaryIdFiltro = value;
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_FILTRO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = true, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_FILTRO), QualifiedEntitySetName = "ControleSistemaContext.TCS_FILTRO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_FILTRO.ID_FILTRO", Source = "IdFiltro", Target = "ID_FILTRO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_FILTRO", RelationPropertyName = "TCS_FILTRO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_FILTRO.OBS_FILTRO", Source = "ObsFiltro", Target = "OBS_FILTRO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_FILTRO", RelationPropertyName = "TCS_FILTRO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_FILTRO.PARAMETROS", Source = "Parametros", Target = "PARAMETROS", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_FILTRO", RelationPropertyName = "TCS_FILTRO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_FILTRO.DESC_FILTRO", Source = "DescFiltro", Target = "DESC_FILTRO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_FILTRO", RelationPropertyName = "TCS_FILTRO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_FILTRO.COMANDO_FILTRO", Source = "ComandoFiltro", Target = "COMANDO_FILTRO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_FILTRO", RelationPropertyName = "TCS_FILTRO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_FILTRO.LX_TIPO_FILTRO", Source = "LxTipoFiltro", Target = "LX_TIPO_FILTRO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_FILTRO", RelationPropertyName = "TCS_FILTRO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_FILTRO.INDICA_USO_LINX", Source = "IndicaUsoLinx", Target = "INDICA_USO_LINX", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_FILTRO", RelationPropertyName = "TCS_FILTRO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_FILTRO.NOME_ENTIDADE_BM", Source = "NomeEntidadeBm", Target = "NOME_ENTIDADE_BM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_FILTRO", RelationPropertyName = "TCS_FILTRO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_FILTRO.COMANDO_SERIALIZADO", Source = "ComandoSerializado", Target = "COMANDO_SERIALIZADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_FILTRO", RelationPropertyName = "TCS_FILTRO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_FILTRO.TCS_USUARIO.ID_USUARIO", Source = "IdUsuario", Target = "ID_USUARIO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_USUARIO", RelationPropertyName = "TCS_USUARIO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxTipoFiltroValues()
	    {
	    	    return Linx.Framework.BV.Domains.TipoFiltro.GetValues();
	    }
	    private string _lxTipoFiltroName;
	    [DataMember(IsRequired = false, Name = "LxTipoFiltroName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Lx Tipo Filtro", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxTipoFiltroName
	    {
	    	    get { if (this.LxTipoFiltro.IsNull()) { _lxTipoFiltroName = String.Empty; } else { string key = this.LxTipoFiltro.ToString(); var dmValues = this.GetLxTipoFiltroValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxTipoFiltroName) _lxTipoFiltroName = domainName; } return _lxTipoFiltroName; } set { _lxTipoFiltroName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="Parametro.EntityUniqueKey", IsUpdatable=true, EdmName="")]
		
	[DataContract(IsReference = false, Name = "Parametro")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Filtro.Parametro")]
	public partial class Parametro 
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
	 


	    private string _TituloParametro;

	    [DataMember(Name = "TituloParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string TituloParametro
	    {
	    	    get
	    	    {
	    	          if (_TituloParametro.IsNullOrEmpty())
	    	             _TituloParametro =  String.Empty;
	    	          return _TituloParametro;
	    	    }
	    	    set
	    	    {
	    	          this._TituloParametro = value;
	    	    }
	    }

	    private string _DataType;

	    [DataMember(Name = "DataType", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string DataType
	    {
	    	    get
	    	    {
	    	          return _DataType;
	    	    }
	    	    set
	    	    {
	    	          this._DataType = value;
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
	[DomainIdentifier("ProcessorOverviewFiltroDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class FiltroDomainService : DomainService, IDataServiceContext 
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

		
	    public FiltroDomainService() : this("", null, null) { }
	    public FiltroDomainService(string connectionString) : this(connectionString, null, null) { }
	    public FiltroDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public FiltroDomainService(Linx.Framework.ControleSistema.BM.ControleSistemaContext dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public FiltroDomainService(string connectionString, Linx.Framework.ControleSistema.BM.ControleSistemaContext dataContext, Dictionary<string, string> headers) : base() 
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
	
		
			
        [Ignore]
	    //Get All LookUpTcsUsuario.
	    public IQueryable<LookUpTcsUsuario> GetAllLookUpTcsUsuario()
	    {
	        return this.GetLookUpTcsUsuario(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTcsUsuario By EntitySearch.
	    public IQueryable<LookUpTcsUsuario> GetLookUpTcsUsuarioByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTcsUsuario(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTcsUsuario.
	    public IQueryable<LookUpTcsUsuario> GetLookUpTcsUsuario(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TCS_USUARIO" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTcsUsuario";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTcsUsuario));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTcsUsuario> query =  
	
	            (from entity in this.DbContext.TCS_USUARIO.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpTcsUsuario()		
	            {
	            
                NomeUsuario = entity.NOME_USUARIO
                , UidUsuario = entity.UID_USUARIO
                , IdUsuario = entity.ID_USUARIO
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
	
		

	        if (entityName.InList("Linx.Framework.BV.Filtro.TcsFiltro"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsFiltro",
	        			NameSpace = "Linx.Framework.BV.Filtro",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsFiltro",
	        			ClearMethodName = "ClearTcsFiltro",
	        			QueryMethodName  = "GetPagedTcsFiltro",	
	        			CountingMethodName  = "GetTcsFiltro" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Filtro.TcsFiltro"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Filtro.TcsFiltro"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.Filtro.Parametro"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "Parametro",
	        			NameSpace = "Linx.Framework.BV.Filtro",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "Parametro",
	        			ClearMethodName = "ClearParametro",
	        			QueryMethodName  = "GetPagedParametro",	
	        			CountingMethodName  = "GetParametro" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Filtro.Parametro"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Filtro.Parametro"), forceAll: forceAll)
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

         		    return new string[] { "Framework_FiltroClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.FiltroClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Framework_filtroService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.filtroService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear TcsFiltro.
	    public IEnumerable<TcsFiltro> ClearTcsFiltro()
	    {
	        List<TcsFiltro> result = new List<TcsFiltro>();
	        result.Add(new TcsFiltro(false));	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear Parametro.
	    public IEnumerable<Parametro> ClearParametro()
	    {
	        List<Parametro> result = new List<Parametro>();
	        result.Add(new Parametro());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsFiltro.
	    public IQueryable<TcsFiltro> GetTcsFiltro()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsFiltro> result = 
	            (from entity0 in this.DbContext.TCS_FILTRO
                  let entity0Al1 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsFiltro()		
	            {
	            
                ComandoFiltro = entity0.COMANDO_FILTRO
                , ComandoSerializado = entity0.COMANDO_SERIALIZADO
                , DescFiltro = entity0.DESC_FILTRO
                , IdFiltro = entity0.ID_FILTRO
                , IdUsuario = entity0Al1.ID_USUARIO
                , IndicaUsoLinx = entity0.INDICA_USO_LINX
                , LxTipoFiltro = entity0.LX_TIPO_FILTRO
                , LxTipoFiltroName = ((entity0.LX_TIPO_FILTRO) == 2 ? "Filtro BM" : ((entity0.LX_TIPO_FILTRO) == 1 ? "Filtro BV" : ((entity0.LX_TIPO_FILTRO) == 4 ? "Filtro Temporário" : ((entity0.LX_TIPO_FILTRO) == 3 ? "Filtro UI" : ""))))
                , NomeEntidadeBm = entity0.NOME_ENTIDADE_BM
                , NomeUsuario = entity0Al1.NOME_USUARIO
                , ObsFiltro = entity0.OBS_FILTRO
                , Parametros = entity0.PARAMETROS
                , UidUsuario = entity0Al1.UID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsFiltroNoAssociations.
	    public IQueryable<TcsFiltro> GetTcsFiltroNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsFiltro> result = 
	            (from entity0 in this.DbContext.TCS_FILTRO
                  let entity0Al1 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsFiltro()		
	            {
	            
                ComandoFiltro = entity0.COMANDO_FILTRO
                , ComandoSerializado = entity0.COMANDO_SERIALIZADO
                , DescFiltro = entity0.DESC_FILTRO
                , IdFiltro = entity0.ID_FILTRO
                , IdUsuario = entity0Al1.ID_USUARIO
                , IndicaUsoLinx = entity0.INDICA_USO_LINX
                , LxTipoFiltro = entity0.LX_TIPO_FILTRO
                , LxTipoFiltroName = ((entity0.LX_TIPO_FILTRO) == 2 ? "Filtro BM" : ((entity0.LX_TIPO_FILTRO) == 1 ? "Filtro BV" : ((entity0.LX_TIPO_FILTRO) == 4 ? "Filtro Temporário" : ((entity0.LX_TIPO_FILTRO) == 3 ? "Filtro UI" : ""))))
                , NomeEntidadeBm = entity0.NOME_ENTIDADE_BM
                , NomeUsuario = entity0Al1.NOME_USUARIO
                , ObsFiltro = entity0.OBS_FILTRO
                , Parametros = entity0.PARAMETROS
                , UidUsuario = entity0Al1.UID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get Parametro.
	    public IEnumerable<Parametro> GetParametro()
	    {




	
	        IEnumerable<Parametro> result = new List<Parametro>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get ParametroNoAssociations.
	    public IEnumerable<Parametro> GetParametroNoAssociations()
	    {




	
	        IEnumerable<Parametro> result = new List<Parametro>();
	  	
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	//Add filtering disabled property for TCS_FILTRO
	    	string[] bmDisabledTcsFiltroList = this.GetEDM().GetFilteringDisabledList("TCS_FILTRO");
	    	if (bmDisabledTcsFiltroList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsFiltroList.Contains("TCS_FILTRO.COMANDO_FILTRO"))
	    		{
	    			result.Add("TcsFiltro|ComandoFiltro");
	    			result.Add("TcsFiltro|TCS_FILTRO.COMANDO_FILTRO");
	    		}
	
	    		if (bmDisabledTcsFiltroList.Contains("TCS_FILTRO.COMANDO_SERIALIZADO"))
	    		{
	    			result.Add("TcsFiltro|ComandoSerializado");
	    			result.Add("TcsFiltro|TCS_FILTRO.COMANDO_SERIALIZADO");
	    		}
	
	    		if (bmDisabledTcsFiltroList.Contains("TCS_FILTRO.DESC_FILTRO"))
	    		{
	    			result.Add("TcsFiltro|DescFiltro");
	    			result.Add("TcsFiltro|TCS_FILTRO.DESC_FILTRO");
	    		}
	
	    		if (bmDisabledTcsFiltroList.Contains("TCS_FILTRO.ID_FILTRO"))
	    		{
	    			result.Add("TcsFiltro|IdFiltro");
	    			result.Add("TcsFiltro|TCS_FILTRO.ID_FILTRO");
	    		}
	
	    		if (bmDisabledTcsFiltroList.Contains("TCS_FILTRO.INDICA_USO_LINX"))
	    		{
	    			result.Add("TcsFiltro|IndicaUsoLinx");
	    			result.Add("TcsFiltro|TCS_FILTRO.INDICA_USO_LINX");
	    		}
	
	    		if (bmDisabledTcsFiltroList.Contains("TCS_FILTRO.LX_TIPO_FILTRO"))
	    		{
	    			result.Add("TcsFiltro|LxTipoFiltro");
	    			result.Add("TcsFiltro|TCS_FILTRO.LX_TIPO_FILTRO");
	    		}
	
	    		if (bmDisabledTcsFiltroList.Contains("TCS_FILTRO.NOME_ENTIDADE_BM"))
	    		{
	    			result.Add("TcsFiltro|NomeEntidadeBm");
	    			result.Add("TcsFiltro|TCS_FILTRO.NOME_ENTIDADE_BM");
	    		}
	
	    		if (bmDisabledTcsFiltroList.Contains("TCS_FILTRO.OBS_FILTRO"))
	    		{
	    			result.Add("TcsFiltro|ObsFiltro");
	    			result.Add("TcsFiltro|TCS_FILTRO.OBS_FILTRO");
	    		}
	
	    		if (bmDisabledTcsFiltroList.Contains("TCS_FILTRO.PARAMETROS"))
	    		{
	    			result.Add("TcsFiltro|Parametros");
	    			result.Add("TcsFiltro|TCS_FILTRO.PARAMETROS");
	    		}
	    	}
	    	return result;
	    }

	    #endregion Excluded Filters

	    #region Get By EntitySearchId Methods Definitions.
	
				
	    [Ignore]
	    //Get TcsFiltro By EntitySearchId.
	    public IQueryable<TcsFiltro> GetTcsFiltroByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsFiltroByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsFiltro By EntitySearchId.
	    public IQueryable<TcsFiltro> GetTcsFiltroByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsFiltroByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get Parametro By EntitySearchId.
	    public IEnumerable<Parametro> GetParametroByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetParametroByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get Parametro By EntitySearchId.
	    public IEnumerable<Parametro> GetParametroByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetParametroByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get TcsFiltro By Example.
	    [Ignore]
	    public IQueryable<TcsFiltro> GetTcsFiltroByExample(TcsFiltro entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsFiltroByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsFiltro By Example.
	    [Ignore]
	    public IQueryable<TcsFiltro> GetTcsFiltroByExampleNoAssociations(TcsFiltro entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsFiltroByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get Parametro By Example.
	    [Ignore]
	    public IEnumerable<Parametro> GetParametroByExample(Parametro entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetParametroByEntitySearch(queryAnalysis);
	    }
			
	    //Get Parametro By Example.
	    [Ignore]
	    public IEnumerable<Parametro> GetParametroByExampleNoAssociations(Parametro entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetParametroByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public TcsFiltro GetTcsFiltroByKey(Int64 idFiltro)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsFiltro");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdFiltro"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idFiltro));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsFiltroByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public Parametro GetParametroByKey(string tituloParametro)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("Parametro");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "TituloParametro"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, tituloParametro));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetParametroByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get TcsFiltroByEntitySearch.
	    public IQueryable<TcsFiltro> GetTcsFiltroByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsFiltro));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsFiltro> result = 
	            (from entity0 in this.DbContext.TCS_FILTRO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsFiltro()		
	            {
	            
                ComandoFiltro = entity0.COMANDO_FILTRO
                , ComandoSerializado = entity0.COMANDO_SERIALIZADO
                , DescFiltro = entity0.DESC_FILTRO
                , IdFiltro = entity0.ID_FILTRO
                , IdUsuario = entity0Al1.ID_USUARIO
                , IndicaUsoLinx = entity0.INDICA_USO_LINX
                , LxTipoFiltro = entity0.LX_TIPO_FILTRO
                , LxTipoFiltroName = ((entity0.LX_TIPO_FILTRO) == 2 ? "Filtro BM" : ((entity0.LX_TIPO_FILTRO) == 1 ? "Filtro BV" : ((entity0.LX_TIPO_FILTRO) == 4 ? "Filtro Temporário" : ((entity0.LX_TIPO_FILTRO) == 3 ? "Filtro UI" : ""))))
                , NomeEntidadeBm = entity0.NOME_ENTIDADE_BM
                , NomeUsuario = entity0Al1.NOME_USUARIO
                , ObsFiltro = entity0.OBS_FILTRO
                , Parametros = entity0.PARAMETROS
                , UidUsuario = entity0Al1.UID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsFiltroByEntitySearchNoAssociations.
	    public IQueryable<TcsFiltro> GetTcsFiltroByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsFiltro));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsFiltro> result = 
	            (from entity0 in this.DbContext.TCS_FILTRO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_USUARIO
	            
	            	
	            select new TcsFiltro()		
	            {
	            
                ComandoFiltro = entity0.COMANDO_FILTRO
                , ComandoSerializado = entity0.COMANDO_SERIALIZADO
                , DescFiltro = entity0.DESC_FILTRO
                , IdFiltro = entity0.ID_FILTRO
                , IdUsuario = entity0Al1.ID_USUARIO
                , IndicaUsoLinx = entity0.INDICA_USO_LINX
                , LxTipoFiltro = entity0.LX_TIPO_FILTRO
                , LxTipoFiltroName = ((entity0.LX_TIPO_FILTRO) == 2 ? "Filtro BM" : ((entity0.LX_TIPO_FILTRO) == 1 ? "Filtro BV" : ((entity0.LX_TIPO_FILTRO) == 4 ? "Filtro Temporário" : ((entity0.LX_TIPO_FILTRO) == 3 ? "Filtro UI" : ""))))
                , NomeEntidadeBm = entity0.NOME_ENTIDADE_BM
                , NomeUsuario = entity0Al1.NOME_USUARIO
                , ObsFiltro = entity0.OBS_FILTRO
                , Parametros = entity0.PARAMETROS
                , UidUsuario = entity0Al1.UID_USUARIO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get ParametroByEntitySearch.
	    public IEnumerable<Parametro> GetParametroByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<Parametro> result = new List<Parametro>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get ParametroByEntitySearchNoAssociations.
	    public IEnumerable<Parametro> GetParametroByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<Parametro> result = new List<Parametro>();
	  	
	
	        	

	
	        return result;
	    }
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get PagedTcsFiltro.
	    public IQueryable<TcsFiltro> GetPagedTcsFiltro(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsFiltro));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsFiltro> result = 
	            (from entity0 in this.DbContext.TCS_FILTRO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_USUARIO
                orderby entity0.ID_FILTRO ascending
	            
	            	
	            select new TcsFiltro()		
	            {
	            
                ComandoFiltro = entity0.COMANDO_FILTRO
                , ComandoSerializado = entity0.COMANDO_SERIALIZADO
                , DescFiltro = entity0.DESC_FILTRO
                , IdFiltro = entity0.ID_FILTRO
                , IdUsuario = entity0Al1.ID_USUARIO
                , IndicaUsoLinx = entity0.INDICA_USO_LINX
                , LxTipoFiltro = entity0.LX_TIPO_FILTRO
                , LxTipoFiltroName = ((entity0.LX_TIPO_FILTRO) == 2 ? "Filtro BM" : ((entity0.LX_TIPO_FILTRO) == 1 ? "Filtro BV" : ((entity0.LX_TIPO_FILTRO) == 4 ? "Filtro Temporário" : ((entity0.LX_TIPO_FILTRO) == 3 ? "Filtro UI" : ""))))
                , NomeEntidadeBm = entity0.NOME_ENTIDADE_BM
                , NomeUsuario = entity0Al1.NOME_USUARIO
                , ObsFiltro = entity0.OBS_FILTRO
                , Parametros = entity0.PARAMETROS
                , UidUsuario = entity0Al1.UID_USUARIO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsFiltroCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsFiltro));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_FILTRO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_USUARIO
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedParametro.
	    public IEnumerable<Parametro> GetPagedParametro(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<Parametro> result = new List<Parametro>();
	  	
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetParametroCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    
	    [Update()]	
	    //Update TcsFiltro.
	    public void UpdateTcsFiltro(TcsFiltro entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsFiltro.
	    public void InsertTcsFiltro(TcsFiltro entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsFiltro.
	    public void DeleteTcsFiltro(TcsFiltro entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update Parametro.
	    public void UpdateParametro(Parametro entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert Parametro.
	    public void InsertParametro(Parametro entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete Parametro.
	    public void DeleteParametro(Parametro entity)
	    {



	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}