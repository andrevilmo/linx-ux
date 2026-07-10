					
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
using LINXDEMO.BM;

namespace Linx.Demo.BV.ModalExterna
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="CLIENTE.ID_CLIENTE", IsUpdatable=false, EdmName="LINXDEMO.BM.BaseTeste")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[Cliente];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[];EdmEntityName[CLIENTE];EntityRelations[ESTADO(ESTADO)#PAIS(PAIS)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "Cliente")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Demo.BV.ModalExterna.Cliente")]
	public partial class Cliente : Linx.Data.Entity
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

	    public virtual void ResetChangeState()
	    {
	      this.ChangeState = "N";
	    }

	    #endregion Flat Entities

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For BigIntCliente
	    partial void OnBigIntClienteChanging(System.Nullable<long> value);
	    partial void OnBigIntClienteChanged();

	    private System.Nullable<long> _BigIntCliente;

	    [DataMember(Name = "BigIntCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Big Int Cliente", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[CLIENTE.BIG_INT_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.BIG_INT_CLIENTE")]
	    public System.Nullable<long> BigIntCliente
	    {
	    	    get
	    	    {
	    	          return _BigIntCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._BigIntCliente != value)
	    	          {
	    	              this.ValidateProperty("BigIntCliente", value);
	    	              this.OnBigIntClienteChanging(value);
	    	              this.RaiseDataMemberChanging("BigIntCliente");
	    	              this._BigIntCliente = value;
	    	              this.RaiseDataMemberChanged("BigIntCliente");
	    	              this.OnBigIntClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For BitCliente
	    partial void OnBitClienteChanging(System.Nullable<bool> value);
	    partial void OnBitClienteChanged();

	    private System.Nullable<bool> _BitCliente;

	    [DataMember(Name = "BitCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bit Cliente", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[CLIENTE.BIT_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.BIT_CLIENTE")]
	    public System.Nullable<bool> BitCliente
	    {
	    	    get
	    	    {
	    	          return _BitCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._BitCliente != value)
	    	          {
	    	              this.ValidateProperty("BitCliente", value);
	    	              this.OnBitClienteChanging(value);
	    	              this.RaiseDataMemberChanging("BitCliente");
	    	              this._BitCliente = value;
	    	              this.RaiseDataMemberChanged("BitCliente");
	    	              this.OnBitClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ComboboxCliente
	    partial void OnComboboxClienteChanging(byte value);
	    partial void OnComboboxClienteChanged();

	    private byte _ComboboxCliente;

	    [DataMember(IsRequired = true, Name = "ComboboxCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Cliente", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_CLIENTE];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[CLIENTE.COMBOBOX_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.COMBOBOX_CLIENTE")]
	    public byte ComboboxCliente
	    {
	    	    get
	    	    {
	    	          return _ComboboxCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._ComboboxCliente != value)
	    	          {
	    	              this.ValidateProperty("ComboboxCliente", value);
	    	              this.OnComboboxClienteChanging(value);
	    	              this.RaiseDataMemberChanging("ComboboxCliente");
	    	              this._ComboboxCliente = value;
	    	              this.RaiseDataMemberChanged("ComboboxCliente");
	    	              this.OnComboboxClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DatetimeCliente
	    partial void OnDatetimeClienteChanging(System.Nullable<DateTime> value);
	    partial void OnDatetimeClienteChanged();

	    private System.Nullable<DateTime> _DatetimeCliente;

	    [DataMember(Name = "DatetimeCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Datetime Cliente", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[CLIENTE.DATETIME_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.DATETIME_CLIENTE")]
	    public System.Nullable<DateTime> DatetimeCliente
	    {
	    	    get
	    	    {
	    	          return _DatetimeCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._DatetimeCliente != value)
	    	          {
	    	              this.ValidateProperty("DatetimeCliente", value);
	    	              this.OnDatetimeClienteChanging(value);
	    	              this.RaiseDataMemberChanging("DatetimeCliente");
	    	              this._DatetimeCliente = value;
	    	              this.RaiseDataMemberChanged("DatetimeCliente");
	    	              this.OnDatetimeClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DecimalCliente
	    partial void OnDecimalClienteChanging(System.Nullable<decimal> value);
	    partial void OnDecimalClienteChanged();

	    private System.Nullable<decimal> _DecimalCliente;

	    [DataMember(Name = "DecimalCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal Cliente", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[18:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N0];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[CLIENTE.DECIMAL_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.DECIMAL_CLIENTE")]
	    public System.Nullable<decimal> DecimalCliente
	    {
	    	    get
	    	    {
	    	          return _DecimalCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._DecimalCliente != value)
	    	          {
	    	              this.ValidateProperty("DecimalCliente", value);
	    	              this.OnDecimalClienteChanging(value);
	    	              this.RaiseDataMemberChanging("DecimalCliente");
	    	              this._DecimalCliente = value;
	    	              this.RaiseDataMemberChanged("DecimalCliente");
	    	              this.OnDecimalClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For GuidCliente
	    partial void OnGuidClienteChanging(System.Nullable<Guid> value);
	    partial void OnGuidClienteChanged();

	    private System.Nullable<Guid> _GuidCliente;

	    [DataMember(Name = "GuidCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Guid Cliente", Description="", Order = 8, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[CLIENTE.GUID_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.GUID_CLIENTE")]
	    public System.Nullable<Guid> GuidCliente
	    {
	    	    get
	    	    {
	    	          return _GuidCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._GuidCliente != value)
	    	          {
	    	              this.ValidateProperty("GuidCliente", value);
	    	              this.OnGuidClienteChanging(value);
	    	              this.RaiseDataMemberChanging("GuidCliente");
	    	              this._GuidCliente = value;
	    	              this.RaiseDataMemberChanged("GuidCliente");
	    	              this.OnGuidClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdCliente
	    partial void OnIdClienteChanging(int value);
	    partial void OnIdClienteChanged();

	    private int _IdCliente;

	    [DataMember(IsRequired = true, Name = "IdCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Cliente", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[CLIENTE.ID_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.ID_CLIENTE")]
	    public int IdCliente
	    {
	    	    get
	    	    {
	    	          return _IdCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdCliente != value)
	    	          {
	    	              this.ValidateProperty("IdCliente", value);
	    	              this.OnIdClienteChanging(value);
	    	              this.RaiseDataMemberChanging("IdCliente");
	    	              this._IdCliente = value;
	    	              this.RaiseDataMemberChanged("IdCliente");
	    	              this.OnIdClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdEstado
	    partial void OnIdEstadoChanging(System.Nullable<int> value);
	    partial void OnIdEstadoChanged();

	    private System.Nullable<int> _IdEstado;

	    [DataMember(Name = "IdEstado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Estado", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpEstado];LookUpTitle[Seleção de (Id Estado)];LookUpQuery[executeLookUpEstado];LookUpFinalize[finalizeLookUpEstado];LookUpDisplayColumns[{\"IdEstado\" : \"Id Estado\", \"IdPais\" : \"Id Pais\", \"StringPais\" : \"String Pais\", \"StringEstado\" : \"String Estado\"}];LookUpColumns[{\"IdEstado\" : true, \"IdPais\" : true, \"StringPais\" : true, \"StringEstado\" : true}];FilterDataKey[CLIENTE.ESTADO.ID_ESTADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<int>#IdEstado#true##10:0##Id Estado#0#true##::LookUpEstado#Linx.Demo.BV.SPA/LookUpExterna#false#false#ESTADO#ESTADO#Linx.Demo.BV.ModalExterna#IQueryable#IdPais,StringPais[IdPais,StringPais]#IdEstado[IdPais=IdPais,StringPais=StringPais];StringEstado[IdPais=IdPais,StringPais=StringPais]#true#false", EdmKey="CLIENTE.ESTADO.ID_ESTADO")]
	    public System.Nullable<int> IdEstado
	    {
	    	    get
	    	    {
	    	          return _IdEstado;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdEstado != value)
	    	          {
	    	              this.ValidateProperty("IdEstado", value);
	    	              this.OnIdEstadoChanging(value);
	    	              this.RaiseDataMemberChanging("IdEstado");
	    	              this._IdEstado = value;
	    	              this.RaiseDataMemberChanged("IdEstado");
	    	              this.OnIdEstadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdPais
	    partial void OnIdPaisChanging(System.Nullable<int> value);
	    partial void OnIdPaisChanged();

	    private System.Nullable<int> _IdPais;

	    [DataMember(Name = "IdPais", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Pais", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpEstado];LookUpTitle[Seleção de (Id Pais)];LookUpQuery[executeLookUpEstado];LookUpFinalize[finalizeLookUpEstado];LookUpDisplayColumns[{\"IdEstado\" : \"Id Estado\", \"IdPais\" : \"Id Pais\", \"StringPais\" : \"String Pais\", \"StringEstado\" : \"String Estado\"}];LookUpColumns[{\"IdEstado\" : true, \"IdPais\" : true, \"StringPais\" : true, \"StringEstado\" : true}];FilterDataKey[CLIENTE.ESTADO.PAIS.ID_PAIS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<int>#IdPais#false##10:0##Id Pais#1#true##::LookUpEstado#Linx.Demo.BV.SPA/LookUpExterna#false#false#ESTADO#ESTADO#Linx.Demo.BV.ModalExterna#IQueryable#IdPais,StringPais[IdPais,StringPais]#IdEstado[IdPais=IdPais,StringPais=StringPais];StringEstado[IdPais=IdPais,StringPais=StringPais]#true#false", EdmKey="CLIENTE.ESTADO.PAIS.ID_PAIS")]
	    public System.Nullable<int> IdPais
	    {
	    	    get
	    	    {
	    	          return _IdPais;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdPais != value)
	    	          {
	    	              this.ValidateProperty("IdPais", value);
	    	              this.OnIdPaisChanging(value);
	    	              this.RaiseDataMemberChanging("IdPais");
	    	              this._IdPais = value;
	    	              this.RaiseDataMemberChanged("IdPais");
	    	              this.OnIdPaisChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IntCliente
	    partial void OnIntClienteChanging(System.Nullable<int> value);
	    partial void OnIntClienteChanged();

	    private System.Nullable<int> _IntCliente;

	    [DataMember(Name = "IntCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int Cliente", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[CLIENTE.INT_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.INT_CLIENTE")]
	    public System.Nullable<int> IntCliente
	    {
	    	    get
	    	    {
	    	          return _IntCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._IntCliente != value)
	    	          {
	    	              this.ValidateProperty("IntCliente", value);
	    	              this.OnIntClienteChanging(value);
	    	              this.RaiseDataMemberChanging("IntCliente");
	    	              this._IntCliente = value;
	    	              this.RaiseDataMemberChanged("IntCliente");
	    	              this.OnIntClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For SmallIntCliente
	    partial void OnSmallIntClienteChanging(System.Nullable<short> value);
	    partial void OnSmallIntClienteChanged();

	    private System.Nullable<short> _SmallIntCliente;

	    [DataMember(Name = "SmallIntCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Small Int Cliente", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[CLIENTE.SMALL_INT_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.SMALL_INT_CLIENTE")]
	    public System.Nullable<short> SmallIntCliente
	    {
	    	    get
	    	    {
	    	          return _SmallIntCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._SmallIntCliente != value)
	    	          {
	    	              this.ValidateProperty("SmallIntCliente", value);
	    	              this.OnSmallIntClienteChanging(value);
	    	              this.RaiseDataMemberChanging("SmallIntCliente");
	    	              this._SmallIntCliente = value;
	    	              this.RaiseDataMemberChanged("SmallIntCliente");
	    	              this.OnSmallIntClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringCliente
	    partial void OnStringClienteChanging(string value);
	    partial void OnStringClienteChanged();

	    private string _StringCliente;

	    [DataMember(Name = "StringCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Cliente", Description="", Order = 14, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[CLIENTE.STRING_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="CLIENTE.STRING_CLIENTE")]
	    public string StringCliente
	    {
	    	    get
	    	    {
	    	          return _StringCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringCliente != value)
	    	          {
	    	              this.ValidateProperty("StringCliente", value);
	    	              this.OnStringClienteChanging(value);
	    	              this.RaiseDataMemberChanging("StringCliente");
	    	              this._StringCliente = value;
	    	              this.RaiseDataMemberChanged("StringCliente");
	    	              this.OnStringClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringEstado
	    partial void OnStringEstadoChanging(System.Nullable<string> value);
	    partial void OnStringEstadoChanged();

	    private System.Nullable<string> _StringEstado;

	    [DataMember(Name = "StringEstado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Estado", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpEstado];LookUpTitle[Seleção de (String Estado)];LookUpQuery[executeLookUpEstado];LookUpFinalize[finalizeLookUpEstado];LookUpDisplayColumns[{\"IdEstado\" : \"Id Estado\", \"IdPais\" : \"Id Pais\", \"StringPais\" : \"String Pais\", \"StringEstado\" : \"String Estado\"}];LookUpColumns[{\"IdEstado\" : true, \"IdPais\" : true, \"StringPais\" : true, \"StringEstado\" : true}];FilterDataKey[CLIENTE.ESTADO.STRING_ESTADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<string>#StringEstado#false##50:0##String Estado#3#true##::LookUpEstado#Linx.Demo.BV.SPA/LookUpExterna#false#false#ESTADO#ESTADO#Linx.Demo.BV.ModalExterna#IQueryable#IdPais,StringPais[IdPais,StringPais]#IdEstado[IdPais=IdPais,StringPais=StringPais];StringEstado[IdPais=IdPais,StringPais=StringPais]#true#false", EdmKey="CLIENTE.ESTADO.STRING_ESTADO")]
	    public System.Nullable<string> StringEstado
	    {
	    	    get
	    	    {
	    	          return _StringEstado;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringEstado != value)
	    	          {
	    	              this.ValidateProperty("StringEstado", value);
	    	              this.OnStringEstadoChanging(value);
	    	              this.RaiseDataMemberChanging("StringEstado");
	    	              this._StringEstado = value;
	    	              this.RaiseDataMemberChanged("StringEstado");
	    	              this.OnStringEstadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringPais
	    partial void OnStringPaisChanging(System.Nullable<string> value);
	    partial void OnStringPaisChanged();

	    private System.Nullable<string> _StringPais;

	    [DataMember(Name = "StringPais", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Pais", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpEstado];LookUpTitle[Seleção de (String Pais)];LookUpQuery[executeLookUpEstado];LookUpFinalize[finalizeLookUpEstado];LookUpDisplayColumns[{\"IdEstado\" : \"Id Estado\", \"IdPais\" : \"Id Pais\", \"StringPais\" : \"String Pais\", \"StringEstado\" : \"String Estado\"}];LookUpColumns[{\"IdEstado\" : true, \"IdPais\" : true, \"StringPais\" : true, \"StringEstado\" : true}];FilterDataKey[CLIENTE.ESTADO.PAIS.STRING_PAIS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<string>#StringPais#false##50:0##String Pais#2#true##::LookUpEstado#Linx.Demo.BV.SPA/LookUpExterna#false#false#ESTADO#ESTADO#Linx.Demo.BV.ModalExterna#IQueryable#IdPais,StringPais[IdPais,StringPais]#IdEstado[IdPais=IdPais,StringPais=StringPais];StringEstado[IdPais=IdPais,StringPais=StringPais]#true#false", EdmKey="CLIENTE.ESTADO.PAIS.STRING_PAIS")]
	    public System.Nullable<string> StringPais
	    {
	    	    get
	    	    {
	    	          return _StringPais;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringPais != value)
	    	          {
	    	              this.ValidateProperty("StringPais", value);
	    	              this.OnStringPaisChanging(value);
	    	              this.RaiseDataMemberChanging("StringPais");
	    	              this._StringPais = value;
	    	              this.RaiseDataMemberChanged("StringPais");
	    	              this.OnStringPaisChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "BaseTeste.CLIENTE").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(LINXDEMO.BM.CLIENTE), QualifiedEntitySetName = "BaseTeste.CLIENTE" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.ID_CLIENTE", Source = "IdCliente", Target = "ID_CLIENTE", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "BaseTeste.CLIENTE", RelationPropertyName = "CLIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.BIT_CLIENTE", Source = "BitCliente", Target = "BIT_CLIENTE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.CLIENTE", RelationPropertyName = "CLIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.INT_CLIENTE", Source = "IntCliente", Target = "INT_CLIENTE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.CLIENTE", RelationPropertyName = "CLIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.GUID_CLIENTE", Source = "GuidCliente", Target = "GUID_CLIENTE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.CLIENTE", RelationPropertyName = "CLIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.STRING_CLIENTE", Source = "StringCliente", Target = "STRING_CLIENTE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.CLIENTE", RelationPropertyName = "CLIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.BIG_INT_CLIENTE", Source = "BigIntCliente", Target = "BIG_INT_CLIENTE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.CLIENTE", RelationPropertyName = "CLIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.DECIMAL_CLIENTE", Source = "DecimalCliente", Target = "DECIMAL_CLIENTE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.CLIENTE", RelationPropertyName = "CLIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.COMBOBOX_CLIENTE", Source = "ComboboxCliente", Target = "COMBOBOX_CLIENTE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.CLIENTE", RelationPropertyName = "CLIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.DATETIME_CLIENTE", Source = "DatetimeCliente", Target = "DATETIME_CLIENTE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.CLIENTE", RelationPropertyName = "CLIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.ESTADO.ID_ESTADO", Source = "IdEstado", Target = "ID_ESTADO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BaseTeste.ESTADO", RelationPropertyName = "ESTADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.SMALL_INT_CLIENTE", Source = "SmallIntCliente", Target = "SMALL_INT_CLIENTE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.CLIENTE", RelationPropertyName = "CLIENTE" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 

	    private string _changeState = "N";
	    [DataMember()]
	    public string ChangeState { get { return _changeState; } set { _changeState = value; } }	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetComboboxClienteValues()
	    {
	    	    return Linx.Demo.BV.Domains.LX_CLIENTE.GetValues();
	    }
	    private string _comboboxClienteName;
	    [DataMember(IsRequired = false, Name = "ComboboxClienteName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Combobox Cliente", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ComboboxClienteName
	    {
	    	    get { if (this.ComboboxCliente.IsNull()) { _comboboxClienteName = String.Empty; } else { string key = this.ComboboxCliente.ToString(); var dmValues = this.GetComboboxClienteValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _comboboxClienteName) _comboboxClienteName = domainName; } return _comboboxClienteName; } set { _comboboxClienteName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="VENDA.ID_VENDA", IsUpdatable=false, EdmName="LINXDEMO.BM.BaseTeste")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[Venda,Venda.VendaItem];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[];EdmEntityName[VENDA];EntityRelations[CLIENTE(CLIENTE)#ESTADO(ESTADO)#PAIS(PAIS)#LOJA(LOJA)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "Venda")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Demo.BV.ModalExterna.Venda")]
	public partial class Venda : Linx.Data.Entity
	{

	
		
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.VendaItemList != null && this.VendaItemList.Count() > 0)
	      {
	         foreach (var entity in this.VendaItemList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.VendaItemList != null)
	      {
	         foreach (var detail in this.VendaItemList)
	         {
	            detail.ResetDetails();
	         }
	         this.VendaItemList = null;
	      }
	    }

	    public virtual void ResetChangeState()
	    {
	      this.ChangeState = "N";
	      if (this.VendaItemList != null)
	      {
	         foreach (var detail in this.VendaItemList.ToArray())
	         {
	            detail.ResetChangeState();
	         }
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(ModalExternaDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("VendaItem"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("VendaItem");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdVenda"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdVenda));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load VendaItem and all sub-details
	         if (this.VendaItemList == null || this.VendaItemList.Count() == 0)
	         {
	             if (take > 0)
	                 this.VendaItemList = context.GetPagedVendaItem(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.VendaItemList = (from r in context.GetVendaItemByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _VendaItemElements = changeSet.ChangeSetEntries.Where(e => e.Entity is VendaItem && ((VendaItem)e.Entity).Venda == null && e.Associations == null && e.OriginalAssociations == null && ((VendaItem)e.Entity).IdVenda == this.IdVenda).ToList();
 	      if (_VendaItemElements.Count > 0 && this.VendaItemList.Count() == 0)
 	      {
 	          this.VendaItemList = _VendaItemElements.Select(e => (VendaItem)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _VendaItemElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((VendaItem)detail.Entity).Venda = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("Venda", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("VendaItemList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For BigIntVenda
	    partial void OnBigIntVendaChanging(System.Nullable<long> value);
	    partial void OnBigIntVendaChanged();

	    private System.Nullable<long> _BigIntVenda;

	    [DataMember(Name = "BigIntVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Big Int Venda", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.BIG_INT_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.BIG_INT_VENDA")]
	    public System.Nullable<long> BigIntVenda
	    {
	    	    get
	    	    {
	    	          return _BigIntVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._BigIntVenda != value)
	    	          {
	    	              this.ValidateProperty("BigIntVenda", value);
	    	              this.OnBigIntVendaChanging(value);
	    	              this.RaiseDataMemberChanging("BigIntVenda");
	    	              this._BigIntVenda = value;
	    	              this.RaiseDataMemberChanged("BigIntVenda");
	    	              this.OnBigIntVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For BitVenda
	    partial void OnBitVendaChanging(System.Nullable<bool> value);
	    partial void OnBitVendaChanged();

	    private System.Nullable<bool> _BitVenda;

	    [DataMember(Name = "BitVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bit Venda", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.BIT_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.BIT_VENDA")]
	    public System.Nullable<bool> BitVenda
	    {
	    	    get
	    	    {
	    	          return _BitVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._BitVenda != value)
	    	          {
	    	              this.ValidateProperty("BitVenda", value);
	    	              this.OnBitVendaChanging(value);
	    	              this.RaiseDataMemberChanging("BitVenda");
	    	              this._BitVenda = value;
	    	              this.RaiseDataMemberChanged("BitVenda");
	    	              this.OnBitVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ComboboxVenda
	    partial void OnComboboxVendaChanging(byte value);
	    partial void OnComboboxVendaChanged();

	    private byte _ComboboxVenda;

	    [DataMember(IsRequired = true, Name = "ComboboxVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Venda", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_VENDA];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.COMBOBOX_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.COMBOBOX_VENDA")]
	    public byte ComboboxVenda
	    {
	    	    get
	    	    {
	    	          return _ComboboxVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._ComboboxVenda != value)
	    	          {
	    	              this.ValidateProperty("ComboboxVenda", value);
	    	              this.OnComboboxVendaChanging(value);
	    	              this.RaiseDataMemberChanging("ComboboxVenda");
	    	              this._ComboboxVenda = value;
	    	              this.RaiseDataMemberChanged("ComboboxVenda");
	    	              this.OnComboboxVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DatetimeVenda
	    partial void OnDatetimeVendaChanging(System.Nullable<DateTime> value);
	    partial void OnDatetimeVendaChanged();

	    private System.Nullable<DateTime> _DatetimeVenda;

	    [DataMember(Name = "DatetimeVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Datetime Venda", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.DATETIME_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.DATETIME_VENDA")]
	    public System.Nullable<DateTime> DatetimeVenda
	    {
	    	    get
	    	    {
	    	          return _DatetimeVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._DatetimeVenda != value)
	    	          {
	    	              this.ValidateProperty("DatetimeVenda", value);
	    	              this.OnDatetimeVendaChanging(value);
	    	              this.RaiseDataMemberChanging("DatetimeVenda");
	    	              this._DatetimeVenda = value;
	    	              this.RaiseDataMemberChanged("DatetimeVenda");
	    	              this.OnDatetimeVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DecimalVenda
	    partial void OnDecimalVendaChanging(System.Nullable<decimal> value);
	    partial void OnDecimalVendaChanged();

	    private System.Nullable<decimal> _DecimalVenda;

	    [DataMember(Name = "DecimalVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal Venda", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[18:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N0];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.DECIMAL_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.DECIMAL_VENDA")]
	    public System.Nullable<decimal> DecimalVenda
	    {
	    	    get
	    	    {
	    	          return _DecimalVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._DecimalVenda != value)
	    	          {
	    	              this.ValidateProperty("DecimalVenda", value);
	    	              this.OnDecimalVendaChanging(value);
	    	              this.RaiseDataMemberChanging("DecimalVenda");
	    	              this._DecimalVenda = value;
	    	              this.RaiseDataMemberChanged("DecimalVenda");
	    	              this.OnDecimalVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For GuidVenda
	    partial void OnGuidVendaChanging(System.Nullable<Guid> value);
	    partial void OnGuidVendaChanged();

	    private System.Nullable<Guid> _GuidVenda;

	    [DataMember(Name = "GuidVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Guid Venda", Description="", Order = 7, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.GUID_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.GUID_VENDA")]
	    public System.Nullable<Guid> GuidVenda
	    {
	    	    get
	    	    {
	    	          return _GuidVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._GuidVenda != value)
	    	          {
	    	              this.ValidateProperty("GuidVenda", value);
	    	              this.OnGuidVendaChanging(value);
	    	              this.RaiseDataMemberChanging("GuidVenda");
	    	              this._GuidVenda = value;
	    	              this.RaiseDataMemberChanged("GuidVenda");
	    	              this.OnGuidVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdCliente
	    partial void OnIdClienteChanging(System.Nullable<int> value);
	    partial void OnIdClienteChanged();

	    private System.Nullable<int> _IdCliente;

	    [DataMember(Name = "IdCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Cliente", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpCliente];LookUpTitle[Seleção de (Id Cliente)];LookUpQuery[executeLookUpCliente];LookUpFinalize[finalizeLookUpCliente];LookUpDisplayColumns[{\"IdCliente\" : \"Id Cliente\"}];LookUpColumns[{\"IdCliente\" : true}];FilterDataKey[VENDA.CLIENTE.ID_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<int>#IdCliente#true##10:0##Id Cliente#0#true##::LookUpCliente##false#false#CLIENTE#CLIENTE#Linx.Demo.BV.ModalExterna#IQueryable###true#false", EdmKey="VENDA.CLIENTE.ID_CLIENTE")]
	    public System.Nullable<int> IdCliente
	    {
	    	    get
	    	    {
	    	          return _IdCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdCliente != value)
	    	          {
	    	              this.ValidateProperty("IdCliente", value);
	    	              this.OnIdClienteChanging(value);
	    	              this.RaiseDataMemberChanging("IdCliente");
	    	              this._IdCliente = value;
	    	              this.RaiseDataMemberChanged("IdCliente");
	    	              this.OnIdClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLoja
	    partial void OnIdLojaChanging(System.Nullable<int> value);
	    partial void OnIdLojaChanged();

	    private System.Nullable<int> _IdLoja;

	    [DataMember(Name = "IdLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Loja", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpLoja];LookUpTitle[Seleção de (Id Loja)];LookUpQuery[executeLookUpLoja];LookUpFinalize[finalizeLookUpLoja];LookUpDisplayColumns[{\"IdLoja\" : \"Id Loja\", \"StringLoja\" : \"String Loja\"}];LookUpColumns[{\"IdLoja\" : true, \"StringLoja\" : true}];FilterDataKey[VENDA.LOJA.ID_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<int>#IdLoja#true##10:0##Id Loja#0#true##::LookUpLoja#Linx.Demo.BV.SPA/UILookUpDentroOutraUI#false#false#LOJA#LOJA#Linx.Demo.BV.ModalExterna#IQueryable###true#false", EdmKey="VENDA.LOJA.ID_LOJA")]
	    public System.Nullable<int> IdLoja
	    {
	    	    get
	    	    {
	    	          return _IdLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLoja != value)
	    	          {
	    	              this.ValidateProperty("IdLoja", value);
	    	              this.OnIdLojaChanging(value);
	    	              this.RaiseDataMemberChanging("IdLoja");
	    	              this._IdLoja = value;
	    	              this.RaiseDataMemberChanged("IdLoja");
	    	              this.OnIdLojaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdVenda
	    partial void OnIdVendaChanging(int value);
	    partial void OnIdVendaChanged();

	    private int _IdVenda;

	    [DataMember(IsRequired = true, Name = "IdVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Venda", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.ID_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.ID_VENDA")]
	    public int IdVenda
	    {
	    	    get
	    	    {
	    	          return _IdVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdVenda != value)
	    	          {
	    	              this.ValidateProperty("IdVenda", value);
	    	              this.OnIdVendaChanging(value);
	    	              this.RaiseDataMemberChanging("IdVenda");
	    	              this._IdVenda = value;
	    	              this.RaiseDataMemberChanged("IdVenda");
	    	              this.OnIdVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IntVenda
	    partial void OnIntVendaChanging(System.Nullable<int> value);
	    partial void OnIntVendaChanged();

	    private System.Nullable<int> _IntVenda;

	    [DataMember(Name = "IntVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int Venda", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.INT_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.INT_VENDA")]
	    public System.Nullable<int> IntVenda
	    {
	    	    get
	    	    {
	    	          return _IntVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._IntVenda != value)
	    	          {
	    	              this.ValidateProperty("IntVenda", value);
	    	              this.OnIntVendaChanging(value);
	    	              this.RaiseDataMemberChanging("IntVenda");
	    	              this._IntVenda = value;
	    	              this.RaiseDataMemberChanged("IntVenda");
	    	              this.OnIntVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For SmallIntVenda
	    partial void OnSmallIntVendaChanging(System.Nullable<short> value);
	    partial void OnSmallIntVendaChanged();

	    private System.Nullable<short> _SmallIntVenda;

	    [DataMember(Name = "SmallIntVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Small Int Venda", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.SMALL_INT_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.SMALL_INT_VENDA")]
	    public System.Nullable<short> SmallIntVenda
	    {
	    	    get
	    	    {
	    	          return _SmallIntVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._SmallIntVenda != value)
	    	          {
	    	              this.ValidateProperty("SmallIntVenda", value);
	    	              this.OnSmallIntVendaChanging(value);
	    	              this.RaiseDataMemberChanging("SmallIntVenda");
	    	              this._SmallIntVenda = value;
	    	              this.RaiseDataMemberChanged("SmallIntVenda");
	    	              this.OnSmallIntVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringLoja
	    partial void OnStringLojaChanging(System.Nullable<string> value);
	    partial void OnStringLojaChanged();

	    private System.Nullable<string> _StringLoja;

	    [DataMember(Name = "StringLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Loja", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpLoja];LookUpTitle[Seleção de (String Loja)];LookUpQuery[executeLookUpLoja];LookUpFinalize[finalizeLookUpLoja];LookUpDisplayColumns[{\"IdLoja\" : \"Id Loja\", \"StringLoja\" : \"String Loja\"}];LookUpColumns[{\"IdLoja\" : true, \"StringLoja\" : true}];FilterDataKey[VENDA.LOJA.STRING_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<string>#StringLoja#false##50:0##String Loja#1#true##::LookUpLoja#Linx.Demo.BV.SPA/UILookUpDentroOutraUI#false#false#LOJA#LOJA#Linx.Demo.BV.ModalExterna#IQueryable###true#false", EdmKey="VENDA.LOJA.STRING_LOJA")]
	    public System.Nullable<string> StringLoja
	    {
	    	    get
	    	    {
	    	          return _StringLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringLoja != value)
	    	          {
	    	              this.ValidateProperty("StringLoja", value);
	    	              this.OnStringLojaChanging(value);
	    	              this.RaiseDataMemberChanging("StringLoja");
	    	              this._StringLoja = value;
	    	              this.RaiseDataMemberChanged("StringLoja");
	    	              this.OnStringLojaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringVenda
	    partial void OnStringVendaChanging(string value);
	    partial void OnStringVendaChanged();

	    private string _StringVenda;

	    [DataMember(Name = "StringVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Venda", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.STRING_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.STRING_VENDA")]
	    public string StringVenda
	    {
	    	    get
	    	    {
	    	          return _StringVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringVenda != value)
	    	          {
	    	              this.ValidateProperty("StringVenda", value);
	    	              this.OnStringVendaChanging(value);
	    	              this.RaiseDataMemberChanging("StringVenda");
	    	              this._StringVenda = value;
	    	              this.RaiseDataMemberChanged("StringVenda");
	    	              this.OnStringVendaChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<VendaItem> _VendaItemList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_Venda_VendaItem", "IdVenda", "IdVenda", IsForeignKey=false)]
	    [DataMember(Name = "VendaItemList", EmitDefaultValue = true)]
	    public IEnumerable<VendaItem> VendaItemList
	    {
	        get
	        {
	
	            if (this._VendaItemList == null)
	            	this._VendaItemList = new List<VendaItem>();
	
	            return this._VendaItemList;
	        }
	        set
	        {
	            if (this._VendaItemList != value)
	            {
	                this._VendaItemList = value;
	                this.RaisePropertyChanged("VendaItemList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "BaseTeste.VENDA").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(LINXDEMO.BM.VENDA), QualifiedEntitySetName = "BaseTeste.VENDA" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.ID_VENDA", Source = "IdVenda", Target = "ID_VENDA", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.BIT_VENDA", Source = "BitVenda", Target = "BIT_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.INT_VENDA", Source = "IntVenda", Target = "INT_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.GUID_VENDA", Source = "GuidVenda", Target = "GUID_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.LOJA.ID_LOJA", Source = "IdLoja", Target = "ID_LOJA", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BaseTeste.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.STRING_VENDA", Source = "StringVenda", Target = "STRING_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.BIG_INT_VENDA", Source = "BigIntVenda", Target = "BIG_INT_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.DECIMAL_VENDA", Source = "DecimalVenda", Target = "DECIMAL_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.COMBOBOX_VENDA", Source = "ComboboxVenda", Target = "COMBOBOX_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.DATETIME_VENDA", Source = "DatetimeVenda", Target = "DATETIME_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.SMALL_INT_VENDA", Source = "SmallIntVenda", Target = "SMALL_INT_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.CLIENTE.ID_CLIENTE", Source = "IdCliente", Target = "ID_CLIENTE", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BaseTeste.CLIENTE", RelationPropertyName = "CLIENTE" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 

	    private string _changeState = "N";
	    [DataMember()]
	    public string ChangeState { get { return _changeState; } set { _changeState = value; } }	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetComboboxVendaValues()
	    {
	    	    return Linx.Demo.BV.Domains.LX_VENDA.GetValues();
	    }
	    private string _comboboxVendaName;
	    [DataMember(IsRequired = false, Name = "ComboboxVendaName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Combobox Venda", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ComboboxVendaName
	    {
	    	    get { if (this.ComboboxVenda.IsNull()) { _comboboxVendaName = String.Empty; } else { string key = this.ComboboxVenda.ToString(); var dmValues = this.GetComboboxVendaValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _comboboxVendaName) _comboboxVendaName = domainName; } return _comboboxVendaName; } set { _comboboxVendaName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="VENDA_ITEM.ID_VENDA_ITEM", IsUpdatable=false, EdmName="LINXDEMO.BM.BaseTeste")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[VendaItem];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#.VENDA_ITEM_LISTA as #Alias#];EdmEntityName[VENDA_ITEM];EntityRelations[VENDA(VENDA)#CLIENTE(CLIENTE)#ESTADO(ESTADO)#PAIS(PAIS)#LOJA(LOJA)];EdmParentEntityName[VENDA];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "VendaItem")]
	[Serializable()]
	public partial class VendaItem : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(ModalExternaDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("Venda");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdVenda"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdVenda));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load Venda
	         this.Venda = (from r in context.GetVendaByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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

	    public virtual void ResetChangeState()
	    {
	      this.ChangeState = "N";
	    }

	    #endregion Flat Entities

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For BigIntVendaItem
	    partial void OnBigIntVendaItemChanging(System.Nullable<long> value);
	    partial void OnBigIntVendaItemChanged();

	    private System.Nullable<long> _BigIntVendaItem;

	    [DataMember(Name = "BigIntVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Big Int Venda Item", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.BIG_INT_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.BIG_INT_VENDA_ITEM")]
	    public System.Nullable<long> BigIntVendaItem
	    {
	    	    get
	    	    {
	    	          return _BigIntVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._BigIntVendaItem != value)
	    	          {
	    	              this.ValidateProperty("BigIntVendaItem", value);
	    	              this.OnBigIntVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("BigIntVendaItem");
	    	              this._BigIntVendaItem = value;
	    	              this.RaiseDataMemberChanged("BigIntVendaItem");
	    	              this.OnBigIntVendaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For BitVendaItem
	    partial void OnBitVendaItemChanging(System.Nullable<bool> value);
	    partial void OnBitVendaItemChanged();

	    private System.Nullable<bool> _BitVendaItem;

	    [DataMember(Name = "BitVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bit Venda Item", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.BIT_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.BIT_VENDA_ITEM")]
	    public System.Nullable<bool> BitVendaItem
	    {
	    	    get
	    	    {
	    	          return _BitVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._BitVendaItem != value)
	    	          {
	    	              this.ValidateProperty("BitVendaItem", value);
	    	              this.OnBitVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("BitVendaItem");
	    	              this._BitVendaItem = value;
	    	              this.RaiseDataMemberChanged("BitVendaItem");
	    	              this.OnBitVendaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ComboboxVendaItem
	    partial void OnComboboxVendaItemChanging(byte value);
	    partial void OnComboboxVendaItemChanged();

	    private byte _ComboboxVendaItem;

	    [DataMember(IsRequired = true, Name = "ComboboxVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Venda Item", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_VENDA_ITEM];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.COMBOBOX_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.COMBOBOX_VENDA_ITEM")]
	    public byte ComboboxVendaItem
	    {
	    	    get
	    	    {
	    	          return _ComboboxVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._ComboboxVendaItem != value)
	    	          {
	    	              this.ValidateProperty("ComboboxVendaItem", value);
	    	              this.OnComboboxVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("ComboboxVendaItem");
	    	              this._ComboboxVendaItem = value;
	    	              this.RaiseDataMemberChanged("ComboboxVendaItem");
	    	              this.OnComboboxVendaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DatetimeVendaItem
	    partial void OnDatetimeVendaItemChanging(System.Nullable<DateTime> value);
	    partial void OnDatetimeVendaItemChanged();

	    private System.Nullable<DateTime> _DatetimeVendaItem;

	    [DataMember(Name = "DatetimeVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Datetime Venda Item", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.DATETIME_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.DATETIME_VENDA_ITEM")]
	    public System.Nullable<DateTime> DatetimeVendaItem
	    {
	    	    get
	    	    {
	    	          return _DatetimeVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._DatetimeVendaItem != value)
	    	          {
	    	              this.ValidateProperty("DatetimeVendaItem", value);
	    	              this.OnDatetimeVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("DatetimeVendaItem");
	    	              this._DatetimeVendaItem = value;
	    	              this.RaiseDataMemberChanged("DatetimeVendaItem");
	    	              this.OnDatetimeVendaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DecimalVendaItem
	    partial void OnDecimalVendaItemChanging(System.Nullable<decimal> value);
	    partial void OnDecimalVendaItemChanged();

	    private System.Nullable<decimal> _DecimalVendaItem;

	    [DataMember(Name = "DecimalVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal Venda Item", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.DECIMAL_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.DECIMAL_VENDA_ITEM")]
	    public System.Nullable<decimal> DecimalVendaItem
	    {
	    	    get
	    	    {
	    	          return _DecimalVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._DecimalVendaItem != value)
	    	          {
	    	              this.ValidateProperty("DecimalVendaItem", value);
	    	              this.OnDecimalVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("DecimalVendaItem");
	    	              this._DecimalVendaItem = value;
	    	              this.RaiseDataMemberChanged("DecimalVendaItem");
	    	              this.OnDecimalVendaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For GuidVendaItem
	    partial void OnGuidVendaItemChanging(System.Nullable<Guid> value);
	    partial void OnGuidVendaItemChanged();

	    private System.Nullable<Guid> _GuidVendaItem;

	    [DataMember(Name = "GuidVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Guid Venda Item", Description="", Order = 6, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.GUID_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.GUID_VENDA_ITEM")]
	    public System.Nullable<Guid> GuidVendaItem
	    {
	    	    get
	    	    {
	    	          return _GuidVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._GuidVendaItem != value)
	    	          {
	    	              this.ValidateProperty("GuidVendaItem", value);
	    	              this.OnGuidVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("GuidVendaItem");
	    	              this._GuidVendaItem = value;
	    	              this.RaiseDataMemberChanged("GuidVendaItem");
	    	              this.OnGuidVendaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdCliente
	    partial void OnIdClienteChanging(System.Nullable<int> value);
	    partial void OnIdClienteChanged();

	    private System.Nullable<int> _IdCliente;

	    [DataMember(Name = "IdCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Cliente", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.VENDA.CLIENTE.ID_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.VENDA.CLIENTE.ID_CLIENTE")]
	    public System.Nullable<int> IdCliente
	    {
	    	    get
	    	    {
	    	          return _IdCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdCliente != value)
	    	          {
	    	              this.ValidateProperty("IdCliente", value);
	    	              this.OnIdClienteChanging(value);
	    	              this.RaiseDataMemberChanging("IdCliente");
	    	              this._IdCliente = value;
	    	              this.RaiseDataMemberChanged("IdCliente");
	    	              this.OnIdClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdVenda
	    partial void OnIdVendaChanging(System.Nullable<int> value);
	    partial void OnIdVendaChanged();

	    private System.Nullable<int> _IdVenda;

	    [DataMember(Name = "IdVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Venda", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.VENDA.ID_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.VENDA.ID_VENDA")]
	    public System.Nullable<int> IdVenda
	    {
	    	    get
	    	    {
	    	          return _IdVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdVenda != value)
	    	          {
	    	              this.ValidateProperty("IdVenda", value);
	    	              this.OnIdVendaChanging(value);
	    	              this.RaiseDataMemberChanging("IdVenda");
	    	              this._IdVenda = value;
	    	              this.RaiseDataMemberChanged("IdVenda");
	    	              this.OnIdVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdVendaItem
	    partial void OnIdVendaItemChanging(int value);
	    partial void OnIdVendaItemChanged();

	    private int _IdVendaItem;

	    [DataMember(IsRequired = true, Name = "IdVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Venda Item", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.ID_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.ID_VENDA_ITEM")]
	    public int IdVendaItem
	    {
	    	    get
	    	    {
	    	          return _IdVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdVendaItem != value)
	    	          {
	    	              this.ValidateProperty("IdVendaItem", value);
	    	              this.OnIdVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("IdVendaItem");
	    	              this._IdVendaItem = value;
	    	              this.RaiseDataMemberChanged("IdVendaItem");
	    	              this.OnIdVendaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IntVenda
	    partial void OnIntVendaChanging(System.Nullable<int> value);
	    partial void OnIntVendaChanged();

	    private System.Nullable<int> _IntVenda;

	    [DataMember(Name = "IntVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int Venda", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.VENDA.INT_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.VENDA.INT_VENDA")]
	    public System.Nullable<int> IntVenda
	    {
	    	    get
	    	    {
	    	          return _IntVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._IntVenda != value)
	    	          {
	    	              this.ValidateProperty("IntVenda", value);
	    	              this.OnIntVendaChanging(value);
	    	              this.RaiseDataMemberChanging("IntVenda");
	    	              this._IntVenda = value;
	    	              this.RaiseDataMemberChanged("IntVenda");
	    	              this.OnIntVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IntVendaItem
	    partial void OnIntVendaItemChanging(System.Nullable<int> value);
	    partial void OnIntVendaItemChanged();

	    private System.Nullable<int> _IntVendaItem;

	    [DataMember(Name = "IntVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int Venda Item", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.INT_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.INT_VENDA_ITEM")]
	    public System.Nullable<int> IntVendaItem
	    {
	    	    get
	    	    {
	    	          return _IntVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._IntVendaItem != value)
	    	          {
	    	              this.ValidateProperty("IntVendaItem", value);
	    	              this.OnIntVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("IntVendaItem");
	    	              this._IntVendaItem = value;
	    	              this.RaiseDataMemberChanged("IntVendaItem");
	    	              this.OnIntVendaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For SmallIntVendaItem
	    partial void OnSmallIntVendaItemChanging(System.Nullable<short> value);
	    partial void OnSmallIntVendaItemChanged();

	    private System.Nullable<short> _SmallIntVendaItem;

	    [DataMember(Name = "SmallIntVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Small Int Venda Item", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.SMALL_INT_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.SMALL_INT_VENDA_ITEM")]
	    public System.Nullable<short> SmallIntVendaItem
	    {
	    	    get
	    	    {
	    	          return _SmallIntVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._SmallIntVendaItem != value)
	    	          {
	    	              this.ValidateProperty("SmallIntVendaItem", value);
	    	              this.OnSmallIntVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("SmallIntVendaItem");
	    	              this._SmallIntVendaItem = value;
	    	              this.RaiseDataMemberChanged("SmallIntVendaItem");
	    	              this.OnSmallIntVendaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringVendaItem
	    partial void OnStringVendaItemChanging(string value);
	    partial void OnStringVendaItemChanged();

	    private string _StringVendaItem;

	    [DataMember(Name = "StringVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Venda Item", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.STRING_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.STRING_VENDA_ITEM")]
	    public string StringVendaItem
	    {
	    	    get
	    	    {
	    	          return _StringVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringVendaItem != value)
	    	          {
	    	              this.ValidateProperty("StringVendaItem", value);
	    	              this.OnStringVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("StringVendaItem");
	    	              this._StringVendaItem = value;
	    	              this.RaiseDataMemberChanged("StringVendaItem");
	    	              this.OnStringVendaItemChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private Venda _Venda;
	    [DataMember(Name = "Venda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_Venda_VendaItem", "IdVenda", "IdVenda", IsForeignKey=true)]
	    public Venda Venda
	    {
	        get
	        {
	            return this._Venda;
	        }
	        set
	        {
	            if (this._Venda != value)
	            {
	                this._Venda = value;
	                this.RaisePropertyChanged("VendaList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "BaseTeste.VENDA_ITEM").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(LINXDEMO.BM.VENDA_ITEM), QualifiedEntitySetName = "BaseTeste.VENDA_ITEM" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.ID_VENDA_ITEM", Source = "IdVendaItem", Target = "ID_VENDA_ITEM", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.BIT_VENDA_ITEM", Source = "BitVendaItem", Target = "BIT_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.VENDA.ID_VENDA", Source = "IdVenda", Target = "ID_VENDA", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BaseTeste.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.INT_VENDA_ITEM", Source = "IntVendaItem", Target = "INT_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.GUID_VENDA_ITEM", Source = "GuidVendaItem", Target = "GUID_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.STRING_VENDA_ITEM", Source = "StringVendaItem", Target = "STRING_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.BIG_INT_VENDA_ITEM", Source = "BigIntVendaItem", Target = "BIG_INT_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.DECIMAL_VENDA_ITEM", Source = "DecimalVendaItem", Target = "DECIMAL_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.COMBOBOX_VENDA_ITEM", Source = "ComboboxVendaItem", Target = "COMBOBOX_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.DATETIME_VENDA_ITEM", Source = "DatetimeVendaItem", Target = "DATETIME_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.SMALL_INT_VENDA_ITEM", Source = "SmallIntVendaItem", Target = "SMALL_INT_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 

	    private string _changeState = "N";
	    [DataMember()]
	    public string ChangeState { get { return _changeState; } set { _changeState = value; } }	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetComboboxVendaItemValues()
	    {
	    	    return Linx.Demo.BV.Domains.LX_VENDA_ITEM.GetValues();
	    }
	    private string _comboboxVendaItemName;
	    [DataMember(IsRequired = false, Name = "ComboboxVendaItemName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Combobox Venda Item", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ComboboxVendaItemName
	    {
	    	    get { if (this.ComboboxVendaItem.IsNull()) { _comboboxVendaItemName = String.Empty; } else { string key = this.ComboboxVendaItem.ToString(); var dmValues = this.GetComboboxVendaItemValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _comboboxVendaItemName) _comboboxVendaItemName = domainName; } return _comboboxVendaItemName; } set { _comboboxVendaItemName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="FORMA_PAGAMENTO.ID_FORMA_PAGAMENTO", IsUpdatable=false, EdmName="LINXDEMO.BM.BaseTeste")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[FormaPagamento];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[];EdmEntityName[FORMA_PAGAMENTO];EntityRelations[VENDA(VENDA)#CLIENTE(CLIENTE)#ESTADO(ESTADO)#PAIS(PAIS)#LOJA(LOJA)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "FormaPagamento")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Demo.BV.ModalExterna.FormaPagamento")]
	public partial class FormaPagamento : Linx.Data.Entity
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

	    public virtual void ResetChangeState()
	    {
	      this.ChangeState = "N";
	    }

	    #endregion Flat Entities

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For BigIntFormaPagamento
	    partial void OnBigIntFormaPagamentoChanging(System.Nullable<long> value);
	    partial void OnBigIntFormaPagamentoChanged();

	    private System.Nullable<long> _BigIntFormaPagamento;

	    [DataMember(Name = "BigIntFormaPagamento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Big Int Forma Pagamento", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[FORMA_PAGAMENTO.BIG_INT_FORMA_PAGAMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="FORMA_PAGAMENTO.BIG_INT_FORMA_PAGAMENTO")]
	    public System.Nullable<long> BigIntFormaPagamento
	    {
	    	    get
	    	    {
	    	          return _BigIntFormaPagamento;
	    	    }
	    	    set
	    	    {
	    	          if (this._BigIntFormaPagamento != value)
	    	          {
	    	              this.ValidateProperty("BigIntFormaPagamento", value);
	    	              this.OnBigIntFormaPagamentoChanging(value);
	    	              this.RaiseDataMemberChanging("BigIntFormaPagamento");
	    	              this._BigIntFormaPagamento = value;
	    	              this.RaiseDataMemberChanged("BigIntFormaPagamento");
	    	              this.OnBigIntFormaPagamentoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For BitFormaPagamento
	    partial void OnBitFormaPagamentoChanging(System.Nullable<bool> value);
	    partial void OnBitFormaPagamentoChanged();

	    private System.Nullable<bool> _BitFormaPagamento;

	    [DataMember(Name = "BitFormaPagamento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bit Forma Pagamento", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[FORMA_PAGAMENTO.BIT_FORMA_PAGAMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="FORMA_PAGAMENTO.BIT_FORMA_PAGAMENTO")]
	    public System.Nullable<bool> BitFormaPagamento
	    {
	    	    get
	    	    {
	    	          return _BitFormaPagamento;
	    	    }
	    	    set
	    	    {
	    	          if (this._BitFormaPagamento != value)
	    	          {
	    	              this.ValidateProperty("BitFormaPagamento", value);
	    	              this.OnBitFormaPagamentoChanging(value);
	    	              this.RaiseDataMemberChanging("BitFormaPagamento");
	    	              this._BitFormaPagamento = value;
	    	              this.RaiseDataMemberChanged("BitFormaPagamento");
	    	              this.OnBitFormaPagamentoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ComboboxFormaPagamento
	    partial void OnComboboxFormaPagamentoChanging(byte value);
	    partial void OnComboboxFormaPagamentoChanged();

	    private byte _ComboboxFormaPagamento;

	    [DataMember(IsRequired = true, Name = "ComboboxFormaPagamento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Forma Pagamento", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_FORMA_PAGAMENTO];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[FORMA_PAGAMENTO.COMBOBOX_FORMA_PAGAMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="FORMA_PAGAMENTO.COMBOBOX_FORMA_PAGAMENTO")]
	    public byte ComboboxFormaPagamento
	    {
	    	    get
	    	    {
	    	          return _ComboboxFormaPagamento;
	    	    }
	    	    set
	    	    {
	    	          if (this._ComboboxFormaPagamento != value)
	    	          {
	    	              this.ValidateProperty("ComboboxFormaPagamento", value);
	    	              this.OnComboboxFormaPagamentoChanging(value);
	    	              this.RaiseDataMemberChanging("ComboboxFormaPagamento");
	    	              this._ComboboxFormaPagamento = value;
	    	              this.RaiseDataMemberChanged("ComboboxFormaPagamento");
	    	              this.OnComboboxFormaPagamentoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DatetimeFormaPagamento
	    partial void OnDatetimeFormaPagamentoChanging(System.Nullable<DateTime> value);
	    partial void OnDatetimeFormaPagamentoChanged();

	    private System.Nullable<DateTime> _DatetimeFormaPagamento;

	    [DataMember(Name = "DatetimeFormaPagamento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Datetime Forma Pagamento", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[FORMA_PAGAMENTO.DATETIME_FORMA_PAGAMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="FORMA_PAGAMENTO.DATETIME_FORMA_PAGAMENTO")]
	    public System.Nullable<DateTime> DatetimeFormaPagamento
	    {
	    	    get
	    	    {
	    	          return _DatetimeFormaPagamento;
	    	    }
	    	    set
	    	    {
	    	          if (this._DatetimeFormaPagamento != value)
	    	          {
	    	              this.ValidateProperty("DatetimeFormaPagamento", value);
	    	              this.OnDatetimeFormaPagamentoChanging(value);
	    	              this.RaiseDataMemberChanging("DatetimeFormaPagamento");
	    	              this._DatetimeFormaPagamento = value;
	    	              this.RaiseDataMemberChanged("DatetimeFormaPagamento");
	    	              this.OnDatetimeFormaPagamentoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DecimalFormaPagamento
	    partial void OnDecimalFormaPagamentoChanging(System.Nullable<decimal> value);
	    partial void OnDecimalFormaPagamentoChanged();

	    private System.Nullable<decimal> _DecimalFormaPagamento;

	    [DataMember(Name = "DecimalFormaPagamento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal Forma Pagamento", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[FORMA_PAGAMENTO.DECIMAL_FORMA_PAGAMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="FORMA_PAGAMENTO.DECIMAL_FORMA_PAGAMENTO")]
	    public System.Nullable<decimal> DecimalFormaPagamento
	    {
	    	    get
	    	    {
	    	          return _DecimalFormaPagamento;
	    	    }
	    	    set
	    	    {
	    	          if (this._DecimalFormaPagamento != value)
	    	          {
	    	              this.ValidateProperty("DecimalFormaPagamento", value);
	    	              this.OnDecimalFormaPagamentoChanging(value);
	    	              this.RaiseDataMemberChanging("DecimalFormaPagamento");
	    	              this._DecimalFormaPagamento = value;
	    	              this.RaiseDataMemberChanged("DecimalFormaPagamento");
	    	              this.OnDecimalFormaPagamentoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For GuidFormaPagamento
	    partial void OnGuidFormaPagamentoChanging(System.Nullable<Guid> value);
	    partial void OnGuidFormaPagamentoChanged();

	    private System.Nullable<Guid> _GuidFormaPagamento;

	    [DataMember(Name = "GuidFormaPagamento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Guid Forma Pagamento", Description="", Order = 6, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[FORMA_PAGAMENTO.GUID_FORMA_PAGAMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="FORMA_PAGAMENTO.GUID_FORMA_PAGAMENTO")]
	    public System.Nullable<Guid> GuidFormaPagamento
	    {
	    	    get
	    	    {
	    	          return _GuidFormaPagamento;
	    	    }
	    	    set
	    	    {
	    	          if (this._GuidFormaPagamento != value)
	    	          {
	    	              this.ValidateProperty("GuidFormaPagamento", value);
	    	              this.OnGuidFormaPagamentoChanging(value);
	    	              this.RaiseDataMemberChanging("GuidFormaPagamento");
	    	              this._GuidFormaPagamento = value;
	    	              this.RaiseDataMemberChanged("GuidFormaPagamento");
	    	              this.OnGuidFormaPagamentoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdFormaPagamento
	    partial void OnIdFormaPagamentoChanging(int value);
	    partial void OnIdFormaPagamentoChanged();

	    private int _IdFormaPagamento;

	    [DataMember(IsRequired = true, Name = "IdFormaPagamento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Forma Pagamento", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[FORMA_PAGAMENTO.ID_FORMA_PAGAMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="FORMA_PAGAMENTO.ID_FORMA_PAGAMENTO")]
	    public int IdFormaPagamento
	    {
	    	    get
	    	    {
	    	          return _IdFormaPagamento;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdFormaPagamento != value)
	    	          {
	    	              this.ValidateProperty("IdFormaPagamento", value);
	    	              this.OnIdFormaPagamentoChanging(value);
	    	              this.RaiseDataMemberChanging("IdFormaPagamento");
	    	              this._IdFormaPagamento = value;
	    	              this.RaiseDataMemberChanged("IdFormaPagamento");
	    	              this.OnIdFormaPagamentoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdVenda
	    partial void OnIdVendaChanging(System.Nullable<int> value);
	    partial void OnIdVendaChanged();

	    private System.Nullable<int> _IdVenda;

	    [DataMember(Name = "IdVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Venda", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpVenda];LookUpTitle[Seleção de (Id Venda)];LookUpQuery[executeLookUpVenda];LookUpFinalize[finalizeLookUpVenda];LookUpDisplayColumns[{\"IdVenda\" : \"Id Venda\"}];LookUpColumns[{\"IdVenda\" : true}];FilterDataKey[FORMA_PAGAMENTO.VENDA.ID_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<int>#IdVenda#true##10:0##Id Venda#0#true##::LookUpVenda##false#false#VENDA#VENDA#Linx.Demo.BV.ModalExterna#IQueryable###true#false", EdmKey="FORMA_PAGAMENTO.VENDA.ID_VENDA")]
	    public System.Nullable<int> IdVenda
	    {
	    	    get
	    	    {
	    	          return _IdVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdVenda != value)
	    	          {
	    	              this.ValidateProperty("IdVenda", value);
	    	              this.OnIdVendaChanging(value);
	    	              this.RaiseDataMemberChanging("IdVenda");
	    	              this._IdVenda = value;
	    	              this.RaiseDataMemberChanged("IdVenda");
	    	              this.OnIdVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IntFormaPagamento
	    partial void OnIntFormaPagamentoChanging(System.Nullable<int> value);
	    partial void OnIntFormaPagamentoChanged();

	    private System.Nullable<int> _IntFormaPagamento;

	    [DataMember(Name = "IntFormaPagamento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int Forma Pagamento", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[FORMA_PAGAMENTO.INT_FORMA_PAGAMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="FORMA_PAGAMENTO.INT_FORMA_PAGAMENTO")]
	    public System.Nullable<int> IntFormaPagamento
	    {
	    	    get
	    	    {
	    	          return _IntFormaPagamento;
	    	    }
	    	    set
	    	    {
	    	          if (this._IntFormaPagamento != value)
	    	          {
	    	              this.ValidateProperty("IntFormaPagamento", value);
	    	              this.OnIntFormaPagamentoChanging(value);
	    	              this.RaiseDataMemberChanging("IntFormaPagamento");
	    	              this._IntFormaPagamento = value;
	    	              this.RaiseDataMemberChanged("IntFormaPagamento");
	    	              this.OnIntFormaPagamentoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For SmallIntFormaPagamento
	    partial void OnSmallIntFormaPagamentoChanging(System.Nullable<short> value);
	    partial void OnSmallIntFormaPagamentoChanged();

	    private System.Nullable<short> _SmallIntFormaPagamento;

	    [DataMember(Name = "SmallIntFormaPagamento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Small Int Forma Pagamento", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[FORMA_PAGAMENTO.SMALL_INT_FORMA_PAGAMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="FORMA_PAGAMENTO.SMALL_INT_FORMA_PAGAMENTO")]
	    public System.Nullable<short> SmallIntFormaPagamento
	    {
	    	    get
	    	    {
	    	          return _SmallIntFormaPagamento;
	    	    }
	    	    set
	    	    {
	    	          if (this._SmallIntFormaPagamento != value)
	    	          {
	    	              this.ValidateProperty("SmallIntFormaPagamento", value);
	    	              this.OnSmallIntFormaPagamentoChanging(value);
	    	              this.RaiseDataMemberChanging("SmallIntFormaPagamento");
	    	              this._SmallIntFormaPagamento = value;
	    	              this.RaiseDataMemberChanged("SmallIntFormaPagamento");
	    	              this.OnSmallIntFormaPagamentoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringFormaPagamento
	    partial void OnStringFormaPagamentoChanging(string value);
	    partial void OnStringFormaPagamentoChanged();

	    private string _StringFormaPagamento;

	    [DataMember(Name = "StringFormaPagamento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Forma Pagamento", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[FORMA_PAGAMENTO.STRING_FORMA_PAGAMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="FORMA_PAGAMENTO.STRING_FORMA_PAGAMENTO")]
	    public string StringFormaPagamento
	    {
	    	    get
	    	    {
	    	          return _StringFormaPagamento;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringFormaPagamento != value)
	    	          {
	    	              this.ValidateProperty("StringFormaPagamento", value);
	    	              this.OnStringFormaPagamentoChanging(value);
	    	              this.RaiseDataMemberChanging("StringFormaPagamento");
	    	              this._StringFormaPagamento = value;
	    	              this.RaiseDataMemberChanged("StringFormaPagamento");
	    	              this.OnStringFormaPagamentoChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "BaseTeste.FORMA_PAGAMENTO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(LINXDEMO.BM.FORMA_PAGAMENTO), QualifiedEntitySetName = "BaseTeste.FORMA_PAGAMENTO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="FORMA_PAGAMENTO.VENDA.ID_VENDA", Source = "IdVenda", Target = "ID_VENDA", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BaseTeste.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="FORMA_PAGAMENTO.ID_FORMA_PAGAMENTO", Source = "IdFormaPagamento", Target = "ID_FORMA_PAGAMENTO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "BaseTeste.FORMA_PAGAMENTO", RelationPropertyName = "FORMA_PAGAMENTO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="FORMA_PAGAMENTO.BIT_FORMA_PAGAMENTO", Source = "BitFormaPagamento", Target = "BIT_FORMA_PAGAMENTO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.FORMA_PAGAMENTO", RelationPropertyName = "FORMA_PAGAMENTO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="FORMA_PAGAMENTO.INT_FORMA_PAGAMENTO", Source = "IntFormaPagamento", Target = "INT_FORMA_PAGAMENTO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.FORMA_PAGAMENTO", RelationPropertyName = "FORMA_PAGAMENTO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="FORMA_PAGAMENTO.GUID_FORMA_PAGAMENTO", Source = "GuidFormaPagamento", Target = "GUID_FORMA_PAGAMENTO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.FORMA_PAGAMENTO", RelationPropertyName = "FORMA_PAGAMENTO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="FORMA_PAGAMENTO.STRING_FORMA_PAGAMENTO", Source = "StringFormaPagamento", Target = "STRING_FORMA_PAGAMENTO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.FORMA_PAGAMENTO", RelationPropertyName = "FORMA_PAGAMENTO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="FORMA_PAGAMENTO.BIG_INT_FORMA_PAGAMENTO", Source = "BigIntFormaPagamento", Target = "BIG_INT_FORMA_PAGAMENTO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.FORMA_PAGAMENTO", RelationPropertyName = "FORMA_PAGAMENTO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="FORMA_PAGAMENTO.DECIMAL_FORMA_PAGAMENTO", Source = "DecimalFormaPagamento", Target = "DECIMAL_FORMA_PAGAMENTO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.FORMA_PAGAMENTO", RelationPropertyName = "FORMA_PAGAMENTO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="FORMA_PAGAMENTO.COMBOBOX_FORMA_PAGAMENTO", Source = "ComboboxFormaPagamento", Target = "COMBOBOX_FORMA_PAGAMENTO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.FORMA_PAGAMENTO", RelationPropertyName = "FORMA_PAGAMENTO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="FORMA_PAGAMENTO.DATETIME_FORMA_PAGAMENTO", Source = "DatetimeFormaPagamento", Target = "DATETIME_FORMA_PAGAMENTO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.FORMA_PAGAMENTO", RelationPropertyName = "FORMA_PAGAMENTO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="FORMA_PAGAMENTO.SMALL_INT_FORMA_PAGAMENTO", Source = "SmallIntFormaPagamento", Target = "SMALL_INT_FORMA_PAGAMENTO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.FORMA_PAGAMENTO", RelationPropertyName = "FORMA_PAGAMENTO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 

	    private string _changeState = "N";
	    [DataMember()]
	    public string ChangeState { get { return _changeState; } set { _changeState = value; } }	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetComboboxFormaPagamentoValues()
	    {
	    	    return Linx.Demo.BV.Domains.LX_FORMA_PAGAMENTO.GetValues();
	    }
	    private string _comboboxFormaPagamentoName;
	    [DataMember(IsRequired = false, Name = "ComboboxFormaPagamentoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Combobox Forma Pagamento", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ComboboxFormaPagamentoName
	    {
	    	    get { if (this.ComboboxFormaPagamento.IsNull()) { _comboboxFormaPagamentoName = String.Empty; } else { string key = this.ComboboxFormaPagamento.ToString(); var dmValues = this.GetComboboxFormaPagamentoValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _comboboxFormaPagamentoName) _comboboxFormaPagamentoName = domainName; } return _comboboxFormaPagamentoName; } set { _comboboxFormaPagamentoName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="LOJA.ID_LOJA", IsUpdatable=false, EdmName="LINXDEMO.BM.BaseTeste")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[Loja];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[];EdmEntityName[LOJA];EntityRelations[];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "Loja")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Demo.BV.ModalExterna.Loja")]
	public partial class Loja : Linx.Data.Entity
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

	    public virtual void ResetChangeState()
	    {
	      this.ChangeState = "N";
	    }

	    #endregion Flat Entities

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For BigIntLoja
	    partial void OnBigIntLojaChanging(System.Nullable<long> value);
	    partial void OnBigIntLojaChanged();

	    private System.Nullable<long> _BigIntLoja;

	    [DataMember(Name = "BigIntLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Big Int Loja", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LOJA.BIG_INT_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.BIG_INT_LOJA")]
	    public System.Nullable<long> BigIntLoja
	    {
	    	    get
	    	    {
	    	          return _BigIntLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._BigIntLoja != value)
	    	          {
	    	              this.ValidateProperty("BigIntLoja", value);
	    	              this.OnBigIntLojaChanging(value);
	    	              this.RaiseDataMemberChanging("BigIntLoja");
	    	              this._BigIntLoja = value;
	    	              this.RaiseDataMemberChanged("BigIntLoja");
	    	              this.OnBigIntLojaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For BitLoja
	    partial void OnBitLojaChanging(System.Nullable<bool> value);
	    partial void OnBitLojaChanged();

	    private System.Nullable<bool> _BitLoja;

	    [DataMember(Name = "BitLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bit Loja", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LOJA.BIT_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.BIT_LOJA")]
	    public System.Nullable<bool> BitLoja
	    {
	    	    get
	    	    {
	    	          return _BitLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._BitLoja != value)
	    	          {
	    	              this.ValidateProperty("BitLoja", value);
	    	              this.OnBitLojaChanging(value);
	    	              this.RaiseDataMemberChanging("BitLoja");
	    	              this._BitLoja = value;
	    	              this.RaiseDataMemberChanged("BitLoja");
	    	              this.OnBitLojaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ComboboxLoja
	    partial void OnComboboxLojaChanging(byte value);
	    partial void OnComboboxLojaChanged();

	    private byte _ComboboxLoja;

	    [DataMember(IsRequired = true, Name = "ComboboxLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Loja", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_LOJA];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LOJA.COMBOBOX_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.COMBOBOX_LOJA")]
	    public byte ComboboxLoja
	    {
	    	    get
	    	    {
	    	          return _ComboboxLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._ComboboxLoja != value)
	    	          {
	    	              this.ValidateProperty("ComboboxLoja", value);
	    	              this.OnComboboxLojaChanging(value);
	    	              this.RaiseDataMemberChanging("ComboboxLoja");
	    	              this._ComboboxLoja = value;
	    	              this.RaiseDataMemberChanged("ComboboxLoja");
	    	              this.OnComboboxLojaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DatetimeLoja
	    partial void OnDatetimeLojaChanging(System.Nullable<DateTime> value);
	    partial void OnDatetimeLojaChanged();

	    private System.Nullable<DateTime> _DatetimeLoja;

	    [DataMember(Name = "DatetimeLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Datetime Loja", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LOJA.DATETIME_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.DATETIME_LOJA")]
	    public System.Nullable<DateTime> DatetimeLoja
	    {
	    	    get
	    	    {
	    	          return _DatetimeLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._DatetimeLoja != value)
	    	          {
	    	              this.ValidateProperty("DatetimeLoja", value);
	    	              this.OnDatetimeLojaChanging(value);
	    	              this.RaiseDataMemberChanging("DatetimeLoja");
	    	              this._DatetimeLoja = value;
	    	              this.RaiseDataMemberChanged("DatetimeLoja");
	    	              this.OnDatetimeLojaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DecimalLoja
	    partial void OnDecimalLojaChanging(System.Nullable<decimal> value);
	    partial void OnDecimalLojaChanged();

	    private System.Nullable<decimal> _DecimalLoja;

	    [DataMember(Name = "DecimalLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal Loja", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LOJA.DECIMAL_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.DECIMAL_LOJA")]
	    public System.Nullable<decimal> DecimalLoja
	    {
	    	    get
	    	    {
	    	          return _DecimalLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._DecimalLoja != value)
	    	          {
	    	              this.ValidateProperty("DecimalLoja", value);
	    	              this.OnDecimalLojaChanging(value);
	    	              this.RaiseDataMemberChanging("DecimalLoja");
	    	              this._DecimalLoja = value;
	    	              this.RaiseDataMemberChanged("DecimalLoja");
	    	              this.OnDecimalLojaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For GuidLoja
	    partial void OnGuidLojaChanging(System.Nullable<Guid> value);
	    partial void OnGuidLojaChanged();

	    private System.Nullable<Guid> _GuidLoja;

	    [DataMember(Name = "GuidLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Guid Loja", Description="", Order = 6, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LOJA.GUID_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.GUID_LOJA")]
	    public System.Nullable<Guid> GuidLoja
	    {
	    	    get
	    	    {
	    	          return _GuidLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._GuidLoja != value)
	    	          {
	    	              this.ValidateProperty("GuidLoja", value);
	    	              this.OnGuidLojaChanging(value);
	    	              this.RaiseDataMemberChanging("GuidLoja");
	    	              this._GuidLoja = value;
	    	              this.RaiseDataMemberChanged("GuidLoja");
	    	              this.OnGuidLojaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLoja
	    partial void OnIdLojaChanging(int value);
	    partial void OnIdLojaChanged();

	    private int _IdLoja;

	    [DataMember(IsRequired = true, Name = "IdLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Loja", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LOJA.ID_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.ID_LOJA")]
	    public int IdLoja
	    {
	    	    get
	    	    {
	    	          return _IdLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLoja != value)
	    	          {
	    	              this.ValidateProperty("IdLoja", value);
	    	              this.OnIdLojaChanging(value);
	    	              this.RaiseDataMemberChanging("IdLoja");
	    	              this._IdLoja = value;
	    	              this.RaiseDataMemberChanged("IdLoja");
	    	              this.OnIdLojaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IntLoja
	    partial void OnIntLojaChanging(System.Nullable<int> value);
	    partial void OnIntLojaChanged();

	    private System.Nullable<int> _IntLoja;

	    [DataMember(Name = "IntLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int Loja", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LOJA.INT_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.INT_LOJA")]
	    public System.Nullable<int> IntLoja
	    {
	    	    get
	    	    {
	    	          return _IntLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._IntLoja != value)
	    	          {
	    	              this.ValidateProperty("IntLoja", value);
	    	              this.OnIntLojaChanging(value);
	    	              this.RaiseDataMemberChanging("IntLoja");
	    	              this._IntLoja = value;
	    	              this.RaiseDataMemberChanged("IntLoja");
	    	              this.OnIntLojaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For SmallIntLoja
	    partial void OnSmallIntLojaChanging(System.Nullable<short> value);
	    partial void OnSmallIntLojaChanged();

	    private System.Nullable<short> _SmallIntLoja;

	    [DataMember(Name = "SmallIntLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Small Int Loja", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LOJA.SMALL_INT_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.SMALL_INT_LOJA")]
	    public System.Nullable<short> SmallIntLoja
	    {
	    	    get
	    	    {
	    	          return _SmallIntLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._SmallIntLoja != value)
	    	          {
	    	              this.ValidateProperty("SmallIntLoja", value);
	    	              this.OnSmallIntLojaChanging(value);
	    	              this.RaiseDataMemberChanging("SmallIntLoja");
	    	              this._SmallIntLoja = value;
	    	              this.RaiseDataMemberChanged("SmallIntLoja");
	    	              this.OnSmallIntLojaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringLoja
	    partial void OnStringLojaChanging(string value);
	    partial void OnStringLojaChanged();

	    private string _StringLoja;

	    [DataMember(Name = "StringLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Loja", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LOJA.STRING_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LOJA.STRING_LOJA")]
	    public string StringLoja
	    {
	    	    get
	    	    {
	    	          return _StringLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringLoja != value)
	    	          {
	    	              this.ValidateProperty("StringLoja", value);
	    	              this.OnStringLojaChanging(value);
	    	              this.RaiseDataMemberChanging("StringLoja");
	    	              this._StringLoja = value;
	    	              this.RaiseDataMemberChanged("StringLoja");
	    	              this.OnStringLojaChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "BaseTeste.LOJA").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(LINXDEMO.BM.LOJA), QualifiedEntitySetName = "BaseTeste.LOJA" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.ID_LOJA", Source = "IdLoja", Target = "ID_LOJA", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "BaseTeste.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.BIT_LOJA", Source = "BitLoja", Target = "BIT_LOJA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.INT_LOJA", Source = "IntLoja", Target = "INT_LOJA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.GUID_LOJA", Source = "GuidLoja", Target = "GUID_LOJA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.STRING_LOJA", Source = "StringLoja", Target = "STRING_LOJA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.BIG_INT_LOJA", Source = "BigIntLoja", Target = "BIG_INT_LOJA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.DECIMAL_LOJA", Source = "DecimalLoja", Target = "DECIMAL_LOJA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.COMBOBOX_LOJA", Source = "ComboboxLoja", Target = "COMBOBOX_LOJA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.DATETIME_LOJA", Source = "DatetimeLoja", Target = "DATETIME_LOJA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.SMALL_INT_LOJA", Source = "SmallIntLoja", Target = "SMALL_INT_LOJA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.LOJA", RelationPropertyName = "LOJA" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 

	    private string _changeState = "N";
	    [DataMember()]
	    public string ChangeState { get { return _changeState; } set { _changeState = value; } }	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetComboboxLojaValues()
	    {
	    	    return Linx.Demo.BV.Domains.LX_LOJA.GetValues();
	    }
	    private string _comboboxLojaName;
	    [DataMember(IsRequired = false, Name = "ComboboxLojaName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Combobox Loja", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ComboboxLojaName
	    {
	    	    get { if (this.ComboboxLoja.IsNull()) { _comboboxLojaName = String.Empty; } else { string key = this.ComboboxLoja.ToString(); var dmValues = this.GetComboboxLojaValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _comboboxLojaName) _comboboxLojaName = domainName; } return _comboboxLojaName; } set { _comboboxLojaName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="ESTADO.ID_ESTADO", IsUpdatable=false, EdmName="LINXDEMO.BM.BaseTeste")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[Estado];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[];EdmEntityName[ESTADO];EntityRelations[PAIS(PAIS)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "Estado")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Demo.BV.ModalExterna.Estado")]
	public partial class Estado : Linx.Data.Entity
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

	    public virtual void ResetChangeState()
	    {
	      this.ChangeState = "N";
	    }

	    #endregion Flat Entities

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For BigIntEstado
	    partial void OnBigIntEstadoChanging(System.Nullable<long> value);
	    partial void OnBigIntEstadoChanged();

	    private System.Nullable<long> _BigIntEstado;

	    [DataMember(Name = "BigIntEstado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Big Int Estado", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ESTADO.BIG_INT_ESTADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ESTADO.BIG_INT_ESTADO")]
	    public System.Nullable<long> BigIntEstado
	    {
	    	    get
	    	    {
	    	          return _BigIntEstado;
	    	    }
	    	    set
	    	    {
	    	          if (this._BigIntEstado != value)
	    	          {
	    	              this.ValidateProperty("BigIntEstado", value);
	    	              this.OnBigIntEstadoChanging(value);
	    	              this.RaiseDataMemberChanging("BigIntEstado");
	    	              this._BigIntEstado = value;
	    	              this.RaiseDataMemberChanged("BigIntEstado");
	    	              this.OnBigIntEstadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For BitEstado
	    partial void OnBitEstadoChanging(System.Nullable<bool> value);
	    partial void OnBitEstadoChanged();

	    private System.Nullable<bool> _BitEstado;

	    [DataMember(Name = "BitEstado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bit Estado", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ESTADO.BIT_ESTADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ESTADO.BIT_ESTADO")]
	    public System.Nullable<bool> BitEstado
	    {
	    	    get
	    	    {
	    	          return _BitEstado;
	    	    }
	    	    set
	    	    {
	    	          if (this._BitEstado != value)
	    	          {
	    	              this.ValidateProperty("BitEstado", value);
	    	              this.OnBitEstadoChanging(value);
	    	              this.RaiseDataMemberChanging("BitEstado");
	    	              this._BitEstado = value;
	    	              this.RaiseDataMemberChanged("BitEstado");
	    	              this.OnBitEstadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ComboboxEstado
	    partial void OnComboboxEstadoChanging(byte value);
	    partial void OnComboboxEstadoChanged();

	    private byte _ComboboxEstado;

	    [DataMember(IsRequired = true, Name = "ComboboxEstado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Estado", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_ESTADO];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ESTADO.COMBOBOX_ESTADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ESTADO.COMBOBOX_ESTADO")]
	    public byte ComboboxEstado
	    {
	    	    get
	    	    {
	    	          return _ComboboxEstado;
	    	    }
	    	    set
	    	    {
	    	          if (this._ComboboxEstado != value)
	    	          {
	    	              this.ValidateProperty("ComboboxEstado", value);
	    	              this.OnComboboxEstadoChanging(value);
	    	              this.RaiseDataMemberChanging("ComboboxEstado");
	    	              this._ComboboxEstado = value;
	    	              this.RaiseDataMemberChanged("ComboboxEstado");
	    	              this.OnComboboxEstadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DatetimeEstado
	    partial void OnDatetimeEstadoChanging(System.Nullable<DateTime> value);
	    partial void OnDatetimeEstadoChanged();

	    private System.Nullable<DateTime> _DatetimeEstado;

	    [DataMember(Name = "DatetimeEstado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Datetime Estado", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ESTADO.DATETIME_ESTADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ESTADO.DATETIME_ESTADO")]
	    public System.Nullable<DateTime> DatetimeEstado
	    {
	    	    get
	    	    {
	    	          return _DatetimeEstado;
	    	    }
	    	    set
	    	    {
	    	          if (this._DatetimeEstado != value)
	    	          {
	    	              this.ValidateProperty("DatetimeEstado", value);
	    	              this.OnDatetimeEstadoChanging(value);
	    	              this.RaiseDataMemberChanging("DatetimeEstado");
	    	              this._DatetimeEstado = value;
	    	              this.RaiseDataMemberChanged("DatetimeEstado");
	    	              this.OnDatetimeEstadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DecimalEstado
	    partial void OnDecimalEstadoChanging(System.Nullable<decimal> value);
	    partial void OnDecimalEstadoChanged();

	    private System.Nullable<decimal> _DecimalEstado;

	    [DataMember(Name = "DecimalEstado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal Estado", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ESTADO.DECIMAL_ESTADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ESTADO.DECIMAL_ESTADO")]
	    public System.Nullable<decimal> DecimalEstado
	    {
	    	    get
	    	    {
	    	          return _DecimalEstado;
	    	    }
	    	    set
	    	    {
	    	          if (this._DecimalEstado != value)
	    	          {
	    	              this.ValidateProperty("DecimalEstado", value);
	    	              this.OnDecimalEstadoChanging(value);
	    	              this.RaiseDataMemberChanging("DecimalEstado");
	    	              this._DecimalEstado = value;
	    	              this.RaiseDataMemberChanged("DecimalEstado");
	    	              this.OnDecimalEstadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For GuidEstado
	    partial void OnGuidEstadoChanging(System.Nullable<Guid> value);
	    partial void OnGuidEstadoChanged();

	    private System.Nullable<Guid> _GuidEstado;

	    [DataMember(Name = "GuidEstado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Guid Estado", Description="", Order = 6, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ESTADO.GUID_ESTADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ESTADO.GUID_ESTADO")]
	    public System.Nullable<Guid> GuidEstado
	    {
	    	    get
	    	    {
	    	          return _GuidEstado;
	    	    }
	    	    set
	    	    {
	    	          if (this._GuidEstado != value)
	    	          {
	    	              this.ValidateProperty("GuidEstado", value);
	    	              this.OnGuidEstadoChanging(value);
	    	              this.RaiseDataMemberChanging("GuidEstado");
	    	              this._GuidEstado = value;
	    	              this.RaiseDataMemberChanged("GuidEstado");
	    	              this.OnGuidEstadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdEstado
	    partial void OnIdEstadoChanging(int value);
	    partial void OnIdEstadoChanged();

	    private int _IdEstado;

	    [DataMember(IsRequired = true, Name = "IdEstado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Estado", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ESTADO.ID_ESTADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ESTADO.ID_ESTADO")]
	    public int IdEstado
	    {
	    	    get
	    	    {
	    	          return _IdEstado;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdEstado != value)
	    	          {
	    	              this.ValidateProperty("IdEstado", value);
	    	              this.OnIdEstadoChanging(value);
	    	              this.RaiseDataMemberChanging("IdEstado");
	    	              this._IdEstado = value;
	    	              this.RaiseDataMemberChanged("IdEstado");
	    	              this.OnIdEstadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdPais
	    partial void OnIdPaisChanging(System.Nullable<int> value);
	    partial void OnIdPaisChanged();

	    private System.Nullable<int> _IdPais;

	    [DataMember(Name = "IdPais", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Pais", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpPais];LookUpTitle[Seleção de (Id Pais)];LookUpQuery[executeLookUpPais];LookUpFinalize[finalizeLookUpPais];LookUpDisplayColumns[{\"IdPais\" : \"Id Pais\", \"StringPais\" : \"String Pais\"}];LookUpColumns[{\"IdPais\" : true, \"StringPais\" : true}];FilterDataKey[ESTADO.PAIS.ID_PAIS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<int>#IdPais#true##10:0##Id Pais#0#true##::LookUpPais##false#false#PAIS#PAIS#Linx.Demo.BV.ModalExterna#IQueryable###true#false", EdmKey="ESTADO.PAIS.ID_PAIS")]
	    public System.Nullable<int> IdPais
	    {
	    	    get
	    	    {
	    	          return _IdPais;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdPais != value)
	    	          {
	    	              this.ValidateProperty("IdPais", value);
	    	              this.OnIdPaisChanging(value);
	    	              this.RaiseDataMemberChanging("IdPais");
	    	              this._IdPais = value;
	    	              this.RaiseDataMemberChanged("IdPais");
	    	              this.OnIdPaisChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IntEstado
	    partial void OnIntEstadoChanging(System.Nullable<int> value);
	    partial void OnIntEstadoChanged();

	    private System.Nullable<int> _IntEstado;

	    [DataMember(Name = "IntEstado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int Estado", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ESTADO.INT_ESTADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ESTADO.INT_ESTADO")]
	    public System.Nullable<int> IntEstado
	    {
	    	    get
	    	    {
	    	          return _IntEstado;
	    	    }
	    	    set
	    	    {
	    	          if (this._IntEstado != value)
	    	          {
	    	              this.ValidateProperty("IntEstado", value);
	    	              this.OnIntEstadoChanging(value);
	    	              this.RaiseDataMemberChanging("IntEstado");
	    	              this._IntEstado = value;
	    	              this.RaiseDataMemberChanged("IntEstado");
	    	              this.OnIntEstadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For SmallIntEstado
	    partial void OnSmallIntEstadoChanging(System.Nullable<short> value);
	    partial void OnSmallIntEstadoChanged();

	    private System.Nullable<short> _SmallIntEstado;

	    [DataMember(Name = "SmallIntEstado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Small Int Estado", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ESTADO.SMALL_INT_ESTADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ESTADO.SMALL_INT_ESTADO")]
	    public System.Nullable<short> SmallIntEstado
	    {
	    	    get
	    	    {
	    	          return _SmallIntEstado;
	    	    }
	    	    set
	    	    {
	    	          if (this._SmallIntEstado != value)
	    	          {
	    	              this.ValidateProperty("SmallIntEstado", value);
	    	              this.OnSmallIntEstadoChanging(value);
	    	              this.RaiseDataMemberChanging("SmallIntEstado");
	    	              this._SmallIntEstado = value;
	    	              this.RaiseDataMemberChanged("SmallIntEstado");
	    	              this.OnSmallIntEstadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringEstado
	    partial void OnStringEstadoChanging(string value);
	    partial void OnStringEstadoChanged();

	    private string _StringEstado;

	    [DataMember(Name = "StringEstado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Estado", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ESTADO.STRING_ESTADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ESTADO.STRING_ESTADO")]
	    public string StringEstado
	    {
	    	    get
	    	    {
	    	          return _StringEstado;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringEstado != value)
	    	          {
	    	              this.ValidateProperty("StringEstado", value);
	    	              this.OnStringEstadoChanging(value);
	    	              this.RaiseDataMemberChanging("StringEstado");
	    	              this._StringEstado = value;
	    	              this.RaiseDataMemberChanged("StringEstado");
	    	              this.OnStringEstadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringPais
	    partial void OnStringPaisChanging(System.Nullable<string> value);
	    partial void OnStringPaisChanged();

	    private System.Nullable<string> _StringPais;

	    [DataMember(Name = "StringPais", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Pais", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpPais];LookUpTitle[Seleção de (String Pais)];LookUpQuery[executeLookUpPais];LookUpFinalize[finalizeLookUpPais];LookUpDisplayColumns[{\"IdPais\" : \"Id Pais\", \"StringPais\" : \"String Pais\"}];LookUpColumns[{\"IdPais\" : true, \"StringPais\" : true}];FilterDataKey[ESTADO.PAIS.STRING_PAIS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<string>#StringPais#false##50:0##String Pais#1#true##::LookUpPais##false#false#PAIS#PAIS#Linx.Demo.BV.ModalExterna#IQueryable###true#false", EdmKey="ESTADO.PAIS.STRING_PAIS")]
	    public System.Nullable<string> StringPais
	    {
	    	    get
	    	    {
	    	          return _StringPais;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringPais != value)
	    	          {
	    	              this.ValidateProperty("StringPais", value);
	    	              this.OnStringPaisChanging(value);
	    	              this.RaiseDataMemberChanging("StringPais");
	    	              this._StringPais = value;
	    	              this.RaiseDataMemberChanged("StringPais");
	    	              this.OnStringPaisChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "BaseTeste.ESTADO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(LINXDEMO.BM.ESTADO), QualifiedEntitySetName = "BaseTeste.ESTADO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ESTADO.ID_ESTADO", Source = "IdEstado", Target = "ID_ESTADO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "BaseTeste.ESTADO", RelationPropertyName = "ESTADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ESTADO.BIT_ESTADO", Source = "BitEstado", Target = "BIT_ESTADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.ESTADO", RelationPropertyName = "ESTADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ESTADO.INT_ESTADO", Source = "IntEstado", Target = "INT_ESTADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.ESTADO", RelationPropertyName = "ESTADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ESTADO.GUID_ESTADO", Source = "GuidEstado", Target = "GUID_ESTADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.ESTADO", RelationPropertyName = "ESTADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ESTADO.PAIS.ID_PAIS", Source = "IdPais", Target = "ID_PAIS", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BaseTeste.PAIS", RelationPropertyName = "PAIS" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ESTADO.STRING_ESTADO", Source = "StringEstado", Target = "STRING_ESTADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.ESTADO", RelationPropertyName = "ESTADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ESTADO.BIG_INT_ESTADO", Source = "BigIntEstado", Target = "BIG_INT_ESTADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.ESTADO", RelationPropertyName = "ESTADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ESTADO.DECIMAL_ESTADO", Source = "DecimalEstado", Target = "DECIMAL_ESTADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.ESTADO", RelationPropertyName = "ESTADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ESTADO.COMBOBOX_ESTADO", Source = "ComboboxEstado", Target = "COMBOBOX_ESTADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.ESTADO", RelationPropertyName = "ESTADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ESTADO.DATETIME_ESTADO", Source = "DatetimeEstado", Target = "DATETIME_ESTADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.ESTADO", RelationPropertyName = "ESTADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ESTADO.SMALL_INT_ESTADO", Source = "SmallIntEstado", Target = "SMALL_INT_ESTADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.ESTADO", RelationPropertyName = "ESTADO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 

	    private string _changeState = "N";
	    [DataMember()]
	    public string ChangeState { get { return _changeState; } set { _changeState = value; } }	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetComboboxEstadoValues()
	    {
	    	    return Linx.Demo.BV.Domains.LX_ESTADO.GetValues();
	    }
	    private string _comboboxEstadoName;
	    [DataMember(IsRequired = false, Name = "ComboboxEstadoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Combobox Estado", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ComboboxEstadoName
	    {
	    	    get { if (this.ComboboxEstado.IsNull()) { _comboboxEstadoName = String.Empty; } else { string key = this.ComboboxEstado.ToString(); var dmValues = this.GetComboboxEstadoValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _comboboxEstadoName) _comboboxEstadoName = domainName; } return _comboboxEstadoName; } set { _comboboxEstadoName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[VendaItem];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[Select 1 From #ParentAlias#.VENDA_ITEM_LISTA as #Alias#];EdmEntityName[VENDA_ITEM];EntityRelations[VENDA(VENDA)#CLIENTE(CLIENTE)#ESTADO(ESTADO)#PAIS(PAIS)#LOJA(LOJA)];EdmParentEntityName[VENDA];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "VendaItem")]
	[Serializable()]
	public partial class VendaItemParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For BigIntVendaItem
	    partial void OnBigIntVendaItemChanging(System.Nullable<long> value);
	    partial void OnBigIntVendaItemChanged();

	    private System.Nullable<long> _BigIntVendaItem;

	    [DataMember(Name = "BigIntVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Big Int Venda Item", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.BIG_INT_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.BIG_INT_VENDA_ITEM")]
	    public System.Nullable<long> BigIntVendaItem
	    {
	    	    get
	    	    {
	    	          return _BigIntVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._BigIntVendaItem != value)
	    	          {
	    	              this.ValidateProperty("BigIntVendaItem", value);
	    	              this.OnBigIntVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("BigIntVendaItem");
	    	              this._BigIntVendaItem = value;
	    	              this.RaiseDataMemberChanged("BigIntVendaItem");
	    	              this.OnBigIntVendaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For BitVendaItem
	    partial void OnBitVendaItemChanging(System.Nullable<bool> value);
	    partial void OnBitVendaItemChanged();

	    private System.Nullable<bool> _BitVendaItem;

	    [DataMember(Name = "BitVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bit Venda Item", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.BIT_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.BIT_VENDA_ITEM")]
	    public System.Nullable<bool> BitVendaItem
	    {
	    	    get
	    	    {
	    	          return _BitVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._BitVendaItem != value)
	    	          {
	    	              this.ValidateProperty("BitVendaItem", value);
	    	              this.OnBitVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("BitVendaItem");
	    	              this._BitVendaItem = value;
	    	              this.RaiseDataMemberChanged("BitVendaItem");
	    	              this.OnBitVendaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ComboboxVendaItem
	    partial void OnComboboxVendaItemChanging(byte value);
	    partial void OnComboboxVendaItemChanged();

	    private byte _ComboboxVendaItem;

	    [DataMember(IsRequired = true, Name = "ComboboxVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Venda Item", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_VENDA_ITEM];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.COMBOBOX_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.COMBOBOX_VENDA_ITEM")]
	    public byte ComboboxVendaItem
	    {
	    	    get
	    	    {
	    	          return _ComboboxVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._ComboboxVendaItem != value)
	    	          {
	    	              this.ValidateProperty("ComboboxVendaItem", value);
	    	              this.OnComboboxVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("ComboboxVendaItem");
	    	              this._ComboboxVendaItem = value;
	    	              this.RaiseDataMemberChanged("ComboboxVendaItem");
	    	              this.OnComboboxVendaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DatetimeVendaItem
	    partial void OnDatetimeVendaItemChanging(System.Nullable<DateTime> value);
	    partial void OnDatetimeVendaItemChanged();

	    private System.Nullable<DateTime> _DatetimeVendaItem;

	    [DataMember(Name = "DatetimeVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Datetime Venda Item", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.DATETIME_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.DATETIME_VENDA_ITEM")]
	    public System.Nullable<DateTime> DatetimeVendaItem
	    {
	    	    get
	    	    {
	    	          return _DatetimeVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._DatetimeVendaItem != value)
	    	          {
	    	              this.ValidateProperty("DatetimeVendaItem", value);
	    	              this.OnDatetimeVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("DatetimeVendaItem");
	    	              this._DatetimeVendaItem = value;
	    	              this.RaiseDataMemberChanged("DatetimeVendaItem");
	    	              this.OnDatetimeVendaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DecimalVendaItem
	    partial void OnDecimalVendaItemChanging(System.Nullable<decimal> value);
	    partial void OnDecimalVendaItemChanged();

	    private System.Nullable<decimal> _DecimalVendaItem;

	    [DataMember(Name = "DecimalVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal Venda Item", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.DECIMAL_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.DECIMAL_VENDA_ITEM")]
	    public System.Nullable<decimal> DecimalVendaItem
	    {
	    	    get
	    	    {
	    	          return _DecimalVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._DecimalVendaItem != value)
	    	          {
	    	              this.ValidateProperty("DecimalVendaItem", value);
	    	              this.OnDecimalVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("DecimalVendaItem");
	    	              this._DecimalVendaItem = value;
	    	              this.RaiseDataMemberChanged("DecimalVendaItem");
	    	              this.OnDecimalVendaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For GuidVendaItem
	    partial void OnGuidVendaItemChanging(System.Nullable<Guid> value);
	    partial void OnGuidVendaItemChanged();

	    private System.Nullable<Guid> _GuidVendaItem;

	    [DataMember(Name = "GuidVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Guid Venda Item", Description="", Order = 6, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.GUID_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.GUID_VENDA_ITEM")]
	    public System.Nullable<Guid> GuidVendaItem
	    {
	    	    get
	    	    {
	    	          return _GuidVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._GuidVendaItem != value)
	    	          {
	    	              this.ValidateProperty("GuidVendaItem", value);
	    	              this.OnGuidVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("GuidVendaItem");
	    	              this._GuidVendaItem = value;
	    	              this.RaiseDataMemberChanged("GuidVendaItem");
	    	              this.OnGuidVendaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdCliente
	    partial void OnIdClienteChanging(System.Nullable<int> value);
	    partial void OnIdClienteChanged();

	    private System.Nullable<int> _IdCliente;

	    [DataMember(Name = "IdCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Cliente", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.VENDA.CLIENTE.ID_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.VENDA.CLIENTE.ID_CLIENTE")]
	    public System.Nullable<int> IdCliente
	    {
	    	    get
	    	    {
	    	          return _IdCliente;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdCliente != value)
	    	          {
	    	              this.ValidateProperty("IdCliente", value);
	    	              this.OnIdClienteChanging(value);
	    	              this.RaiseDataMemberChanging("IdCliente");
	    	              this._IdCliente = value;
	    	              this.RaiseDataMemberChanged("IdCliente");
	    	              this.OnIdClienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdVenda
	    partial void OnIdVendaChanging(System.Nullable<int> value);
	    partial void OnIdVendaChanged();

	    private System.Nullable<int> _IdVenda;

	    [DataMember(Name = "IdVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Venda", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.VENDA.ID_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.VENDA.ID_VENDA")]
	    public System.Nullable<int> IdVenda
	    {
	    	    get
	    	    {
	    	          return _IdVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdVenda != value)
	    	          {
	    	              this.ValidateProperty("IdVenda", value);
	    	              this.OnIdVendaChanging(value);
	    	              this.RaiseDataMemberChanging("IdVenda");
	    	              this._IdVenda = value;
	    	              this.RaiseDataMemberChanged("IdVenda");
	    	              this.OnIdVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdVendaItem
	    partial void OnIdVendaItemChanging(int value);
	    partial void OnIdVendaItemChanged();

	    private int _IdVendaItem;

	    [DataMember(IsRequired = true, Name = "IdVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Venda Item", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.ID_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.ID_VENDA_ITEM")]
	    public int IdVendaItem
	    {
	    	    get
	    	    {
	    	          return _IdVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdVendaItem != value)
	    	          {
	    	              this.ValidateProperty("IdVendaItem", value);
	    	              this.OnIdVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("IdVendaItem");
	    	              this._IdVendaItem = value;
	    	              this.RaiseDataMemberChanged("IdVendaItem");
	    	              this.OnIdVendaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IntVenda
	    partial void OnIntVendaChanging(System.Nullable<int> value);
	    partial void OnIntVendaChanged();

	    private System.Nullable<int> _IntVenda;

	    [DataMember(Name = "IntVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int Venda", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.VENDA.INT_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.VENDA.INT_VENDA")]
	    public System.Nullable<int> IntVenda
	    {
	    	    get
	    	    {
	    	          return _IntVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._IntVenda != value)
	    	          {
	    	              this.ValidateProperty("IntVenda", value);
	    	              this.OnIntVendaChanging(value);
	    	              this.RaiseDataMemberChanging("IntVenda");
	    	              this._IntVenda = value;
	    	              this.RaiseDataMemberChanged("IntVenda");
	    	              this.OnIntVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IntVendaItem
	    partial void OnIntVendaItemChanging(System.Nullable<int> value);
	    partial void OnIntVendaItemChanged();

	    private System.Nullable<int> _IntVendaItem;

	    [DataMember(Name = "IntVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int Venda Item", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.INT_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.INT_VENDA_ITEM")]
	    public System.Nullable<int> IntVendaItem
	    {
	    	    get
	    	    {
	    	          return _IntVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._IntVendaItem != value)
	    	          {
	    	              this.ValidateProperty("IntVendaItem", value);
	    	              this.OnIntVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("IntVendaItem");
	    	              this._IntVendaItem = value;
	    	              this.RaiseDataMemberChanged("IntVendaItem");
	    	              this.OnIntVendaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For SmallIntVendaItem
	    partial void OnSmallIntVendaItemChanging(System.Nullable<short> value);
	    partial void OnSmallIntVendaItemChanged();

	    private System.Nullable<short> _SmallIntVendaItem;

	    [DataMember(Name = "SmallIntVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Small Int Venda Item", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.SMALL_INT_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.SMALL_INT_VENDA_ITEM")]
	    public System.Nullable<short> SmallIntVendaItem
	    {
	    	    get
	    	    {
	    	          return _SmallIntVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._SmallIntVendaItem != value)
	    	          {
	    	              this.ValidateProperty("SmallIntVendaItem", value);
	    	              this.OnSmallIntVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("SmallIntVendaItem");
	    	              this._SmallIntVendaItem = value;
	    	              this.RaiseDataMemberChanged("SmallIntVendaItem");
	    	              this.OnSmallIntVendaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringVendaItem
	    partial void OnStringVendaItemChanging(string value);
	    partial void OnStringVendaItemChanged();

	    private string _StringVendaItem;

	    [DataMember(Name = "StringVendaItem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Venda Item", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA_ITEM.STRING_VENDA_ITEM];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA_ITEM.STRING_VENDA_ITEM")]
	    public string StringVendaItem
	    {
	    	    get
	    	    {
	    	          return _StringVendaItem;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringVendaItem != value)
	    	          {
	    	              this.ValidateProperty("StringVendaItem", value);
	    	              this.OnStringVendaItemChanging(value);
	    	              this.RaiseDataMemberChanging("StringVendaItem");
	    	              this._StringVendaItem = value;
	    	              this.RaiseDataMemberChanged("StringVendaItem");
	    	              this.OnStringVendaItemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For BigIntVenda
	    partial void OnBigIntVendaChanging(System.Nullable<long> value);
	    partial void OnBigIntVendaChanged();

	    private System.Nullable<long> _BigIntVenda;

	    [DataMember(Name = "BigIntVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Big Int Venda", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.BIG_INT_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.BIG_INT_VENDA")]
	    public System.Nullable<long> BigIntVenda
	    {
	    	    get
	    	    {
	    	          return _BigIntVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._BigIntVenda != value)
	    	          {
	    	              this.ValidateProperty("BigIntVenda", value);
	    	              this.OnBigIntVendaChanging(value);
	    	              this.RaiseDataMemberChanging("BigIntVenda");
	    	              this._BigIntVenda = value;
	    	              this.RaiseDataMemberChanged("BigIntVenda");
	    	              this.OnBigIntVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For BitVenda
	    partial void OnBitVendaChanging(System.Nullable<bool> value);
	    partial void OnBitVendaChanged();

	    private System.Nullable<bool> _BitVenda;

	    [DataMember(Name = "BitVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bit Venda", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.BIT_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.BIT_VENDA")]
	    public System.Nullable<bool> BitVenda
	    {
	    	    get
	    	    {
	    	          return _BitVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._BitVenda != value)
	    	          {
	    	              this.ValidateProperty("BitVenda", value);
	    	              this.OnBitVendaChanging(value);
	    	              this.RaiseDataMemberChanging("BitVenda");
	    	              this._BitVenda = value;
	    	              this.RaiseDataMemberChanged("BitVenda");
	    	              this.OnBitVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ComboboxVenda
	    partial void OnComboboxVendaChanging(byte value);
	    partial void OnComboboxVendaChanged();

	    private byte _ComboboxVenda;

	    [DataMember(IsRequired = true, Name = "ComboboxVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Venda", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_VENDA];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.COMBOBOX_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.COMBOBOX_VENDA")]
	    public byte ComboboxVenda
	    {
	    	    get
	    	    {
	    	          return _ComboboxVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._ComboboxVenda != value)
	    	          {
	    	              this.ValidateProperty("ComboboxVenda", value);
	    	              this.OnComboboxVendaChanging(value);
	    	              this.RaiseDataMemberChanging("ComboboxVenda");
	    	              this._ComboboxVenda = value;
	    	              this.RaiseDataMemberChanged("ComboboxVenda");
	    	              this.OnComboboxVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DatetimeVenda
	    partial void OnDatetimeVendaChanging(System.Nullable<DateTime> value);
	    partial void OnDatetimeVendaChanged();

	    private System.Nullable<DateTime> _DatetimeVenda;

	    [DataMember(Name = "DatetimeVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Datetime Venda", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.DATETIME_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.DATETIME_VENDA")]
	    public System.Nullable<DateTime> DatetimeVenda
	    {
	    	    get
	    	    {
	    	          return _DatetimeVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._DatetimeVenda != value)
	    	          {
	    	              this.ValidateProperty("DatetimeVenda", value);
	    	              this.OnDatetimeVendaChanging(value);
	    	              this.RaiseDataMemberChanging("DatetimeVenda");
	    	              this._DatetimeVenda = value;
	    	              this.RaiseDataMemberChanged("DatetimeVenda");
	    	              this.OnDatetimeVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DecimalVenda
	    partial void OnDecimalVendaChanging(System.Nullable<decimal> value);
	    partial void OnDecimalVendaChanged();

	    private System.Nullable<decimal> _DecimalVenda;

	    [DataMember(Name = "DecimalVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal Venda", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[18:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N0];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.DECIMAL_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.DECIMAL_VENDA")]
	    public System.Nullable<decimal> DecimalVenda
	    {
	    	    get
	    	    {
	    	          return _DecimalVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._DecimalVenda != value)
	    	          {
	    	              this.ValidateProperty("DecimalVenda", value);
	    	              this.OnDecimalVendaChanging(value);
	    	              this.RaiseDataMemberChanging("DecimalVenda");
	    	              this._DecimalVenda = value;
	    	              this.RaiseDataMemberChanged("DecimalVenda");
	    	              this.OnDecimalVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For GuidVenda
	    partial void OnGuidVendaChanging(System.Nullable<Guid> value);
	    partial void OnGuidVendaChanged();

	    private System.Nullable<Guid> _GuidVenda;

	    [DataMember(Name = "GuidVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Guid Venda", Description="", Order = 7, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.GUID_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.GUID_VENDA")]
	    public System.Nullable<Guid> GuidVenda
	    {
	    	    get
	    	    {
	    	          return _GuidVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._GuidVenda != value)
	    	          {
	    	              this.ValidateProperty("GuidVenda", value);
	    	              this.OnGuidVendaChanging(value);
	    	              this.RaiseDataMemberChanging("GuidVenda");
	    	              this._GuidVenda = value;
	    	              this.RaiseDataMemberChanged("GuidVenda");
	    	              this.OnGuidVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLoja
	    partial void OnIdLojaChanging(System.Nullable<int> value);
	    partial void OnIdLojaChanged();

	    private System.Nullable<int> _IdLoja;

	    [DataMember(Name = "IdLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Loja", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.LOJA.ID_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.LOJA.ID_LOJA")]
	    public System.Nullable<int> IdLoja
	    {
	    	    get
	    	    {
	    	          return _IdLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLoja != value)
	    	          {
	    	              this.ValidateProperty("IdLoja", value);
	    	              this.OnIdLojaChanging(value);
	    	              this.RaiseDataMemberChanging("IdLoja");
	    	              this._IdLoja = value;
	    	              this.RaiseDataMemberChanged("IdLoja");
	    	              this.OnIdLojaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For SmallIntVenda
	    partial void OnSmallIntVendaChanging(System.Nullable<short> value);
	    partial void OnSmallIntVendaChanged();

	    private System.Nullable<short> _SmallIntVenda;

	    [DataMember(Name = "SmallIntVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Small Int Venda", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.SMALL_INT_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.SMALL_INT_VENDA")]
	    public System.Nullable<short> SmallIntVenda
	    {
	    	    get
	    	    {
	    	          return _SmallIntVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._SmallIntVenda != value)
	    	          {
	    	              this.ValidateProperty("SmallIntVenda", value);
	    	              this.OnSmallIntVendaChanging(value);
	    	              this.RaiseDataMemberChanging("SmallIntVenda");
	    	              this._SmallIntVenda = value;
	    	              this.RaiseDataMemberChanged("SmallIntVenda");
	    	              this.OnSmallIntVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringLoja
	    partial void OnStringLojaChanging(System.Nullable<string> value);
	    partial void OnStringLojaChanged();

	    private System.Nullable<string> _StringLoja;

	    [DataMember(Name = "StringLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Loja", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.LOJA.STRING_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.LOJA.STRING_LOJA")]
	    public System.Nullable<string> StringLoja
	    {
	    	    get
	    	    {
	    	          return _StringLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringLoja != value)
	    	          {
	    	              this.ValidateProperty("StringLoja", value);
	    	              this.OnStringLojaChanging(value);
	    	              this.RaiseDataMemberChanging("StringLoja");
	    	              this._StringLoja = value;
	    	              this.RaiseDataMemberChanged("StringLoja");
	    	              this.OnStringLojaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For StringVenda
	    partial void OnStringVendaChanging(string value);
	    partial void OnStringVendaChanged();

	    private string _StringVenda;

	    [DataMember(Name = "StringVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Venda", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[VENDA_ITEM.VENDA.STRING_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="VENDA.STRING_VENDA")]
	    public string StringVenda
	    {
	    	    get
	    	    {
	    	          return _StringVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._StringVenda != value)
	    	          {
	    	              this.ValidateProperty("StringVenda", value);
	    	              this.OnStringVendaChanging(value);
	    	              this.RaiseDataMemberChanging("StringVenda");
	    	              this._StringVenda = value;
	    	              this.RaiseDataMemberChanged("StringVenda");
	    	              this.OnStringVendaChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "BaseTeste.VENDA_ITEM").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(LINXDEMO.BM.VENDA_ITEM), QualifiedEntitySetName = "BaseTeste.VENDA_ITEM" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.ID_VENDA_ITEM", Source = "IdVendaItem", Target = "ID_VENDA_ITEM", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.BIT_VENDA_ITEM", Source = "BitVendaItem", Target = "BIT_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.VENDA.ID_VENDA", Source = "IdVenda", Target = "ID_VENDA", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BaseTeste.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.INT_VENDA_ITEM", Source = "IntVendaItem", Target = "INT_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.GUID_VENDA_ITEM", Source = "GuidVendaItem", Target = "GUID_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.STRING_VENDA_ITEM", Source = "StringVendaItem", Target = "STRING_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.BIG_INT_VENDA_ITEM", Source = "BigIntVendaItem", Target = "BIG_INT_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.DECIMAL_VENDA_ITEM", Source = "DecimalVendaItem", Target = "DECIMAL_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.COMBOBOX_VENDA_ITEM", Source = "ComboboxVendaItem", Target = "COMBOBOX_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.DATETIME_VENDA_ITEM", Source = "DatetimeVendaItem", Target = "DATETIME_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA_ITEM.SMALL_INT_VENDA_ITEM", Source = "SmallIntVendaItem", Target = "SMALL_INT_VENDA_ITEM", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BaseTeste.VENDA_ITEM", RelationPropertyName = "VENDA_ITEM" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 

	    private string _changeState = "N";
	    [DataMember()]
	    public string ChangeState { get { return _changeState; } set { _changeState = value; } }	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetComboboxVendaItemValues()
	    {
	    	    return Linx.Demo.BV.Domains.LX_VENDA_ITEM.GetValues();
	    }
	    private string _comboboxVendaItemName;
	    [DataMember(IsRequired = false, Name = "ComboboxVendaItemName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Combobox Venda Item", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ComboboxVendaItemName
	    {
	    	    get { if (this.ComboboxVendaItem.IsNull()) { _comboboxVendaItemName = String.Empty; } else { string key = this.ComboboxVendaItem.ToString(); var dmValues = this.GetComboboxVendaItemValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _comboboxVendaItemName) _comboboxVendaItemName = domainName; } return _comboboxVendaItemName; } set { _comboboxVendaItemName = value;  }
	    }
	    public Dictionary<string, string> GetComboboxVendaValues()
	    {
	    	    return Linx.Demo.BV.Domains.LX_VENDA.GetValues();
	    }
	    private string _comboboxVendaName;
	    [DataMember(IsRequired = false, Name = "ComboboxVendaName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Combobox Venda", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ComboboxVendaName
	    {
	    	    get { if (this.ComboboxVenda.IsNull()) { _comboboxVendaName = String.Empty; } else { string key = this.ComboboxVenda.ToString(); var dmValues = this.GetComboboxVendaValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _comboboxVendaName) _comboboxVendaName = domainName; } return _comboboxVendaName; } set { _comboboxVendaName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	//////////////////////// DomainService Class V1 ///////////////////////
	///////////////////////////////////////////////////////////////////////
	[EnableClientAccess()]	
	[DomainIdentifier("ProcessorOverviewModalExternaDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class ModalExternaDomainService : DomainService, IDataServiceContext 
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
	
	    private LINXDEMO.BM.BaseTeste _dbContext;
	    protected LINXDEMO.BM.BaseTeste DbContext 
	    { 
	    	get 
	    	{
	        	if (this._dbContext == null)
	        	{
	        		this._dbContext = new LINXDEMO.BM.BaseTeste(connectionString, this.Headers);
	        		((System.Data.Entity.Infrastructure.IObjectContextAdapter)this._dbContext).ObjectContext.CommandTimeout = 180;
	        		this._hasGpeconControl = (!(this._dbContext.IsUserMultiGpecon && this._dbContext.IdGpecon == this._dbContext.IdLinx) && this._dbContext.IdGpecon > 0);		
	        	}
	        	return this._dbContext;
	    	}
	    }

	    public string GetModelAssemblyName()
	    {
	        return typeof(LINXDEMO.BM.BaseTeste).Assembly.FullName;
	    }

	    public System.Data.Entity.Database Database
	    {
	        get { return this.DbContext.Database; }
	    }

		
	    public ModalExternaDomainService() : this("", null, null) { }
	    public ModalExternaDomainService(string connectionString) : this(connectionString, null, null) { }
	    public ModalExternaDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public ModalExternaDomainService(LINXDEMO.BM.BaseTeste dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public ModalExternaDomainService(string connectionString, LINXDEMO.BM.BaseTeste dataContext, Dictionary<string, string> headers) : base() 
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
	    public LINXDEMO.BM.BaseTeste GetEDM()
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
 	        var _VendaElements = changeSet.ChangeSetEntries.Where(e => e.Entity is Venda && e.Entity.GetType().Name == "Venda" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _VendaElements)
 	           if (((Venda)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 
 	        //Remove inconsistent details
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is VendaItem && e.Entity.GetType().Name == "VendaItem" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
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
	    //Get All LookUpEstado.
	    public IQueryable<LookUpEstado> GetAllLookUpEstado()
	    {
	        return this.GetLookUpEstado(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpEstado By EntitySearch.
	    public IQueryable<LookUpEstado> GetLookUpEstadoByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpEstado(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpEstado.
	    public IQueryable<LookUpEstado> GetLookUpEstado(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "ESTADO" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpEstado";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpEstado));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpEstado> query =  
	
	            (from entity in this.DbContext.ESTADO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.PAIS
	            
	            select new LookUpEstado()		
	            {
	            
                IdEstado = entity.ID_ESTADO
                , IdPais = entityAl1.ID_PAIS
                , StringPais = entityAl1.STRING_PAIS
                , StringEstado = entity.STRING_ESTADO
	            });

	            
            //Inner Group Definition
            if (propertyName.InList("IdPais", "StringPais"))
            {
               query = (from r in query select new LookUpEstado() {
               IdEstado = default(System.Nullable<int>)
               , IdPais = r.IdPais
               , StringPais = r.StringPais
               , StringEstado = default(System.Nullable<string>)
                }).Distinct();
            }
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpCliente.
	    public IQueryable<LookUpCliente> GetAllLookUpCliente()
	    {
	        return this.GetLookUpCliente(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpCliente By EntitySearch.
	    public IQueryable<LookUpCliente> GetLookUpClienteByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpCliente(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpCliente.
	    public IQueryable<LookUpCliente> GetLookUpCliente(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "CLIENTE" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpCliente";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpCliente));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpCliente> query =  
	
	            (from entity in this.DbContext.CLIENTE.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpCliente()		
	            {
	            
                IdCliente = entity.ID_CLIENTE
	            });

	            
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpLoja.
	    public IQueryable<LookUpLoja> GetAllLookUpLoja()
	    {
	        return this.GetLookUpLoja(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpLoja By EntitySearch.
	    public IQueryable<LookUpLoja> GetLookUpLojaByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpLoja(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpLoja.
	    public IQueryable<LookUpLoja> GetLookUpLoja(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "LOJA" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpLoja";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpLoja));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpLoja> query =  
	
	            (from entity in this.DbContext.LOJA.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpLoja()		
	            {
	            
                IdLoja = entity.ID_LOJA
                , StringLoja = entity.STRING_LOJA
	            });

	            
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpVenda.
	    public IQueryable<LookUpVenda> GetAllLookUpVenda()
	    {
	        return this.GetLookUpVenda(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpVenda By EntitySearch.
	    public IQueryable<LookUpVenda> GetLookUpVendaByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpVenda(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpVenda.
	    public IQueryable<LookUpVenda> GetLookUpVenda(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "VENDA" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpVenda";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpVenda));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpVenda> query =  
	
	            (from entity in this.DbContext.VENDA.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpVenda()		
	            {
	            
                IdVenda = entity.ID_VENDA
	            });

	            
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpPais.
	    public IQueryable<LookUpPais> GetAllLookUpPais()
	    {
	        return this.GetLookUpPais(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpPais By EntitySearch.
	    public IQueryable<LookUpPais> GetLookUpPaisByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpPais(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpPais.
	    public IQueryable<LookUpPais> GetLookUpPais(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "PAIS" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpPais";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpPais));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpPais> query =  
	
	            (from entity in this.DbContext.PAIS.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpPais()		
	            {
	            
                IdPais = entity.ID_PAIS
                , StringPais = entity.STRING_PAIS
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
	
		

	        if (entityName.InList("Linx.Demo.BV.ModalExterna.Cliente"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "Cliente",
	        			NameSpace = "Linx.Demo.BV.ModalExterna",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "Cliente",
	        			ClearMethodName = "ClearCliente",
	        			QueryMethodName  = "GetPagedCliente",	
	        			CountingMethodName  = "GetCliente" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Demo.BV.ModalExterna.Cliente"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Demo.BV.ModalExterna.Cliente"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Demo.BV.ModalExterna.Venda"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "Venda",
	        			NameSpace = "Linx.Demo.BV.ModalExterna",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "Venda",
	        			ClearMethodName = "ClearVenda",
	        			QueryMethodName  = "GetPagedVenda",	
	        			CountingMethodName  = "GetVenda" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Demo.BV.ModalExterna.Venda"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Demo.BV.ModalExterna.Venda"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Demo.BV.ModalExterna.Venda", "Linx.Demo.BV.ModalExterna.VendaItem"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "VendaItem" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Demo.BV.ModalExterna",
	        			HasQuickSearch = false,
	        			ParentClassName = "Venda",	
	        			DisplayName = "VendaItem",
	        			ClearMethodName = "ClearVendaItem" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedVendaItem" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetVendaItem" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Demo.BV.ModalExterna.VendaItem"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Demo.BV.ModalExterna.VendaItem" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Demo.BV.ModalExterna.FormaPagamento"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "FormaPagamento",
	        			NameSpace = "Linx.Demo.BV.ModalExterna",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "FormaPagamento",
	        			ClearMethodName = "ClearFormaPagamento",
	        			QueryMethodName  = "GetPagedFormaPagamento",	
	        			CountingMethodName  = "GetFormaPagamento" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Demo.BV.ModalExterna.FormaPagamento"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Demo.BV.ModalExterna.FormaPagamento"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Demo.BV.ModalExterna.Loja"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "Loja",
	        			NameSpace = "Linx.Demo.BV.ModalExterna",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "Loja",
	        			ClearMethodName = "ClearLoja",
	        			QueryMethodName  = "GetPagedLoja",	
	        			CountingMethodName  = "GetLoja" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Demo.BV.ModalExterna.Loja"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Demo.BV.ModalExterna.Loja"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Demo.BV.ModalExterna.Estado"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "Estado",
	        			NameSpace = "Linx.Demo.BV.ModalExterna",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "Estado",
	        			ClearMethodName = "ClearEstado",
	        			QueryMethodName  = "GetPagedEstado",	
	        			CountingMethodName  = "GetEstado" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Demo.BV.ModalExterna.Estado"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Demo.BV.ModalExterna.Estado"), forceAll: forceAll)
	        		});
	        }
	
            return result;
        }
	
	    [Ignore]
	    public string[] GetClientDomains(bool erp)
        {	
	    		if (erp)
	    		{

         		    return new string[] { "Demo_ClientErpDataDomainsFactory", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Demo.BV.ClientResources.ClientErpDataDomainsFactory.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
	    		}
	    		else 
	    		{

         		    return new string[] { "Demo_MobileDataDomains", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Demo.BV.ClientResources.MobileDataDomains.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
	    		}

        }

	    [Ignore]
	    public string[] GetClientService(bool erp)
        {	

	    		if (erp)
	    		{

         		    return new string[] { "Demo_ModalExternaClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Demo.BV.ClientResources.ModalExternaClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Demo_modalExternaService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Demo.BV.ClientResources.modalExternaService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear Cliente.
	    public IEnumerable<Cliente> ClearCliente()
	    {
	        List<Cliente> result = new List<Cliente>();
	        result.Add(new Cliente());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear Venda.
	    public IEnumerable<Venda> ClearVenda()
	    {
	        List<Venda> result = new List<Venda>();
	        result.Add(new Venda());	
			
	        result[0].VendaItemList = new List<VendaItem>();
	        ((List<VendaItem>)result[0].VendaItemList).Add(new VendaItem());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear VendaItem.
	    public IEnumerable<VendaItem> ClearVendaItem()
	    {
	        List<VendaItem> result = new List<VendaItem>();
	        result.Add(new VendaItem());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear FormaPagamento.
	    public IEnumerable<FormaPagamento> ClearFormaPagamento()
	    {
	        List<FormaPagamento> result = new List<FormaPagamento>();
	        result.Add(new FormaPagamento());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear Loja.
	    public IEnumerable<Loja> ClearLoja()
	    {
	        List<Loja> result = new List<Loja>();
	        result.Add(new Loja());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear Estado.
	    public IEnumerable<Estado> ClearEstado()
	    {
	        List<Estado> result = new List<Estado>();
	        result.Add(new Estado());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    [ClienteQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get Cliente.
	    public IQueryable<Cliente> GetCliente()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetCliente")))
 	        {
 	             AuthorizationResult authorizationResult = (new ClienteQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<Cliente> result = 
	            (from entity0 in this.DbContext.CLIENTE
                  let entity0Al1 = entity0.ESTADO
                  let entity0Al2 = entity0.ESTADO.PAIS
	            
	            	
	            select new Cliente()		
	            {
	            
                BigIntCliente = entity0.BIG_INT_CLIENTE
                , BitCliente = entity0.BIT_CLIENTE
                , ComboboxCliente = entity0.COMBOBOX_CLIENTE
                , ComboboxClienteName = ((entity0.COMBOBOX_CLIENTE) == 1 ? "CLIENTE 1" : ((entity0.COMBOBOX_CLIENTE) == 2 ? "CLIENTE 2" : ((entity0.COMBOBOX_CLIENTE) == 3 ? "CLIENTE 3" : "")))
                , DatetimeCliente = entity0.DATETIME_CLIENTE
                , DecimalCliente = entity0.DECIMAL_CLIENTE
                , GuidCliente = entity0.GUID_CLIENTE
                , IdCliente = entity0.ID_CLIENTE
                , IdEstado = entity0Al1.ID_ESTADO
                , IdPais = entity0Al2.ID_PAIS
                , IntCliente = entity0.INT_CLIENTE
                , SmallIntCliente = entity0.SMALL_INT_CLIENTE
                , StringCliente = entity0.STRING_CLIENTE
                , StringEstado = entity0Al1.STRING_ESTADO
                , StringPais = entity0Al2.STRING_PAIS
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [ClienteQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get ClienteNoAssociations.
	    public IQueryable<Cliente> GetClienteNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetClienteNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new ClienteQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<Cliente> result = 
	            (from entity0 in this.DbContext.CLIENTE
                  let entity0Al1 = entity0.ESTADO
                  let entity0Al2 = entity0.ESTADO.PAIS
	            
	            	
	            select new Cliente()		
	            {
	            
                BigIntCliente = entity0.BIG_INT_CLIENTE
                , BitCliente = entity0.BIT_CLIENTE
                , ComboboxCliente = entity0.COMBOBOX_CLIENTE
                , ComboboxClienteName = ((entity0.COMBOBOX_CLIENTE) == 1 ? "CLIENTE 1" : ((entity0.COMBOBOX_CLIENTE) == 2 ? "CLIENTE 2" : ((entity0.COMBOBOX_CLIENTE) == 3 ? "CLIENTE 3" : "")))
                , DatetimeCliente = entity0.DATETIME_CLIENTE
                , DecimalCliente = entity0.DECIMAL_CLIENTE
                , GuidCliente = entity0.GUID_CLIENTE
                , IdCliente = entity0.ID_CLIENTE
                , IdEstado = entity0Al1.ID_ESTADO
                , IdPais = entity0Al2.ID_PAIS
                , IntCliente = entity0.INT_CLIENTE
                , SmallIntCliente = entity0.SMALL_INT_CLIENTE
                , StringCliente = entity0.STRING_CLIENTE
                , StringEstado = entity0Al1.STRING_ESTADO
                , StringPais = entity0Al2.STRING_PAIS
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendaQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get Venda.
	    public IQueryable<Venda> GetVenda()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVenda")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<Venda> result = 
	            (from entity0 in this.DbContext.VENDA
                  let entity0Al2 = entity0.LOJA
                  let entity0Al1 = entity0.CLIENTE
	            
	            	
	            select new Venda()		
	            {
	            
                BigIntVenda = entity0.BIG_INT_VENDA
                , BitVenda = entity0.BIT_VENDA
                , ComboboxVenda = entity0.COMBOBOX_VENDA
                , ComboboxVendaName = ((entity0.COMBOBOX_VENDA) == 1 ? "VENDA 1" : ((entity0.COMBOBOX_VENDA) == 2 ? "VENDA 2" : ((entity0.COMBOBOX_VENDA) == 3 ? "VENDA 3" : "")))
                , DatetimeVenda = entity0.DATETIME_VENDA
                , DecimalVenda = entity0.DECIMAL_VENDA
                , GuidVenda = entity0.GUID_VENDA
                , IdCliente = entity0Al1.ID_CLIENTE
                , IdLoja = entity0Al2.ID_LOJA
                , IdVenda = entity0.ID_VENDA
                , IntVenda = entity0.INT_VENDA
                , SmallIntVenda = entity0.SMALL_INT_VENDA
                , StringLoja = entity0Al2.STRING_LOJA
                , StringVenda = entity0.STRING_VENDA
			
                ,VendaItemList = 
	                        (from entity1 in entity0.VENDA_ITEM_LISTA
                                  let entity1Al2 = entity1.VENDA
                                  let entity1Al1 = entity1.VENDA.CLIENTE
	                        
	                        	
	                        select new VendaItem()
	                        {
	                        
                                BigIntVendaItem = entity1.BIG_INT_VENDA_ITEM
                                , BitVendaItem = entity1.BIT_VENDA_ITEM
                                , ComboboxVendaItem = entity1.COMBOBOX_VENDA_ITEM
                                , ComboboxVendaItemName = ((entity1.COMBOBOX_VENDA_ITEM) == 1 ? "VENDA ITEM 1" : ((entity1.COMBOBOX_VENDA_ITEM) == 2 ? "VENDA ITEM 2" : ((entity1.COMBOBOX_VENDA_ITEM) == 3 ? "VENDA ITEM 3" : "")))
                                , DatetimeVendaItem = entity1.DATETIME_VENDA_ITEM
                                , DecimalVendaItem = entity1.DECIMAL_VENDA_ITEM
                                , GuidVendaItem = entity1.GUID_VENDA_ITEM
                                , IdCliente = entity1Al1.ID_CLIENTE
                                , IdVenda = entity1Al2.ID_VENDA
                                , IdVendaItem = entity1.ID_VENDA_ITEM
                                , IntVenda = entity1Al2.INT_VENDA
                                , IntVendaItem = entity1.INT_VENDA_ITEM
                                , SmallIntVendaItem = entity1.SMALL_INT_VENDA_ITEM
                                , StringVendaItem = entity1.STRING_VENDA_ITEM
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendaItemQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendaItem.
	    public IQueryable<VendaItem> GetVendaItem()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendaItem")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaItemQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<VendaItem> result = 
	            (from entity0 in this.DbContext.VENDA_ITEM
                  let entity0Al2 = entity0.VENDA
                  let entity0Al1 = entity0.VENDA.CLIENTE
	            
	            	
	            select new VendaItem()		
	            {
	            
                BigIntVendaItem = entity0.BIG_INT_VENDA_ITEM
                , BitVendaItem = entity0.BIT_VENDA_ITEM
                , ComboboxVendaItem = entity0.COMBOBOX_VENDA_ITEM
                , ComboboxVendaItemName = ((entity0.COMBOBOX_VENDA_ITEM) == 1 ? "VENDA ITEM 1" : ((entity0.COMBOBOX_VENDA_ITEM) == 2 ? "VENDA ITEM 2" : ((entity0.COMBOBOX_VENDA_ITEM) == 3 ? "VENDA ITEM 3" : "")))
                , DatetimeVendaItem = entity0.DATETIME_VENDA_ITEM
                , DecimalVendaItem = entity0.DECIMAL_VENDA_ITEM
                , GuidVendaItem = entity0.GUID_VENDA_ITEM
                , IdCliente = entity0Al1.ID_CLIENTE
                , IdVenda = entity0Al2.ID_VENDA
                , IdVendaItem = entity0.ID_VENDA_ITEM
                , IntVenda = entity0Al2.INT_VENDA
                , IntVendaItem = entity0.INT_VENDA_ITEM
                , SmallIntVendaItem = entity0.SMALL_INT_VENDA_ITEM
                , StringVendaItem = entity0.STRING_VENDA_ITEM
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendaQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendaNoAssociations.
	    public IQueryable<Venda> GetVendaNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendaNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<Venda> result = 
	            (from entity0 in this.DbContext.VENDA
                  let entity0Al2 = entity0.LOJA
                  let entity0Al1 = entity0.CLIENTE
	            
	            	
	            select new Venda()		
	            {
	            
                BigIntVenda = entity0.BIG_INT_VENDA
                , BitVenda = entity0.BIT_VENDA
                , ComboboxVenda = entity0.COMBOBOX_VENDA
                , ComboboxVendaName = ((entity0.COMBOBOX_VENDA) == 1 ? "VENDA 1" : ((entity0.COMBOBOX_VENDA) == 2 ? "VENDA 2" : ((entity0.COMBOBOX_VENDA) == 3 ? "VENDA 3" : "")))
                , DatetimeVenda = entity0.DATETIME_VENDA
                , DecimalVenda = entity0.DECIMAL_VENDA
                , GuidVenda = entity0.GUID_VENDA
                , IdCliente = entity0Al1.ID_CLIENTE
                , IdLoja = entity0Al2.ID_LOJA
                , IdVenda = entity0.ID_VENDA
                , IntVenda = entity0.INT_VENDA
                , SmallIntVenda = entity0.SMALL_INT_VENDA
                , StringLoja = entity0Al2.STRING_LOJA
                , StringVenda = entity0.STRING_VENDA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendaItemQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendaItemNoAssociations.
	    public IQueryable<VendaItem> GetVendaItemNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendaItemNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaItemQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<VendaItem> result = 
	            (from entity0 in this.DbContext.VENDA_ITEM
                  let entity0Al2 = entity0.VENDA
                  let entity0Al1 = entity0.VENDA.CLIENTE
	            
	            	
	            select new VendaItem()		
	            {
	            
                BigIntVendaItem = entity0.BIG_INT_VENDA_ITEM
                , BitVendaItem = entity0.BIT_VENDA_ITEM
                , ComboboxVendaItem = entity0.COMBOBOX_VENDA_ITEM
                , ComboboxVendaItemName = ((entity0.COMBOBOX_VENDA_ITEM) == 1 ? "VENDA ITEM 1" : ((entity0.COMBOBOX_VENDA_ITEM) == 2 ? "VENDA ITEM 2" : ((entity0.COMBOBOX_VENDA_ITEM) == 3 ? "VENDA ITEM 3" : "")))
                , DatetimeVendaItem = entity0.DATETIME_VENDA_ITEM
                , DecimalVendaItem = entity0.DECIMAL_VENDA_ITEM
                , GuidVendaItem = entity0.GUID_VENDA_ITEM
                , IdCliente = entity0Al1.ID_CLIENTE
                , IdVenda = entity0Al2.ID_VENDA
                , IdVendaItem = entity0.ID_VENDA_ITEM
                , IntVenda = entity0Al2.INT_VENDA
                , IntVendaItem = entity0.INT_VENDA_ITEM
                , SmallIntVendaItem = entity0.SMALL_INT_VENDA_ITEM
                , StringVendaItem = entity0.STRING_VENDA_ITEM
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [FormaPagamentoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get FormaPagamento.
	    public IQueryable<FormaPagamento> GetFormaPagamento()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetFormaPagamento")))
 	        {
 	             AuthorizationResult authorizationResult = (new FormaPagamentoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<FormaPagamento> result = 
	            (from entity0 in this.DbContext.FORMA_PAGAMENTO
                  let entity0Al1 = entity0.VENDA
	            
	            	
	            select new FormaPagamento()		
	            {
	            
                BigIntFormaPagamento = entity0.BIG_INT_FORMA_PAGAMENTO
                , BitFormaPagamento = entity0.BIT_FORMA_PAGAMENTO
                , ComboboxFormaPagamento = entity0.COMBOBOX_FORMA_PAGAMENTO
                , ComboboxFormaPagamentoName = ((entity0.COMBOBOX_FORMA_PAGAMENTO) == 1 ? "FORMA PAGAMENTO 1" : ((entity0.COMBOBOX_FORMA_PAGAMENTO) == 2 ? "FORMA PAGAMENTO 2" : ((entity0.COMBOBOX_FORMA_PAGAMENTO) == 3 ? "FORMA PAGAMENTO 3" : "")))
                , DatetimeFormaPagamento = entity0.DATETIME_FORMA_PAGAMENTO
                , DecimalFormaPagamento = entity0.DECIMAL_FORMA_PAGAMENTO
                , GuidFormaPagamento = entity0.GUID_FORMA_PAGAMENTO
                , IdFormaPagamento = entity0.ID_FORMA_PAGAMENTO
                , IdVenda = entity0Al1.ID_VENDA
                , IntFormaPagamento = entity0.INT_FORMA_PAGAMENTO
                , SmallIntFormaPagamento = entity0.SMALL_INT_FORMA_PAGAMENTO
                , StringFormaPagamento = entity0.STRING_FORMA_PAGAMENTO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [FormaPagamentoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get FormaPagamentoNoAssociations.
	    public IQueryable<FormaPagamento> GetFormaPagamentoNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetFormaPagamentoNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new FormaPagamentoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<FormaPagamento> result = 
	            (from entity0 in this.DbContext.FORMA_PAGAMENTO
                  let entity0Al1 = entity0.VENDA
	            
	            	
	            select new FormaPagamento()		
	            {
	            
                BigIntFormaPagamento = entity0.BIG_INT_FORMA_PAGAMENTO
                , BitFormaPagamento = entity0.BIT_FORMA_PAGAMENTO
                , ComboboxFormaPagamento = entity0.COMBOBOX_FORMA_PAGAMENTO
                , ComboboxFormaPagamentoName = ((entity0.COMBOBOX_FORMA_PAGAMENTO) == 1 ? "FORMA PAGAMENTO 1" : ((entity0.COMBOBOX_FORMA_PAGAMENTO) == 2 ? "FORMA PAGAMENTO 2" : ((entity0.COMBOBOX_FORMA_PAGAMENTO) == 3 ? "FORMA PAGAMENTO 3" : "")))
                , DatetimeFormaPagamento = entity0.DATETIME_FORMA_PAGAMENTO
                , DecimalFormaPagamento = entity0.DECIMAL_FORMA_PAGAMENTO
                , GuidFormaPagamento = entity0.GUID_FORMA_PAGAMENTO
                , IdFormaPagamento = entity0.ID_FORMA_PAGAMENTO
                , IdVenda = entity0Al1.ID_VENDA
                , IntFormaPagamento = entity0.INT_FORMA_PAGAMENTO
                , SmallIntFormaPagamento = entity0.SMALL_INT_FORMA_PAGAMENTO
                , StringFormaPagamento = entity0.STRING_FORMA_PAGAMENTO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [LojaQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get Loja.
	    public IQueryable<Loja> GetLoja()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetLoja")))
 	        {
 	             AuthorizationResult authorizationResult = (new LojaQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<Loja> result = 
	            (from entity0 in this.DbContext.LOJA
	            
	            	
	            select new Loja()		
	            {
	            
                BigIntLoja = entity0.BIG_INT_LOJA
                , BitLoja = entity0.BIT_LOJA
                , ComboboxLoja = entity0.COMBOBOX_LOJA
                , ComboboxLojaName = ((entity0.COMBOBOX_LOJA) == 1 ? "LOJA 1" : ((entity0.COMBOBOX_LOJA) == 2 ? "LOJA 2" : ((entity0.COMBOBOX_LOJA) == 3 ? "LOJA 3" : "")))
                , DatetimeLoja = entity0.DATETIME_LOJA
                , DecimalLoja = entity0.DECIMAL_LOJA
                , GuidLoja = entity0.GUID_LOJA
                , IdLoja = entity0.ID_LOJA
                , IntLoja = entity0.INT_LOJA
                , SmallIntLoja = entity0.SMALL_INT_LOJA
                , StringLoja = entity0.STRING_LOJA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [LojaQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get LojaNoAssociations.
	    public IQueryable<Loja> GetLojaNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetLojaNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new LojaQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<Loja> result = 
	            (from entity0 in this.DbContext.LOJA
	            
	            	
	            select new Loja()		
	            {
	            
                BigIntLoja = entity0.BIG_INT_LOJA
                , BitLoja = entity0.BIT_LOJA
                , ComboboxLoja = entity0.COMBOBOX_LOJA
                , ComboboxLojaName = ((entity0.COMBOBOX_LOJA) == 1 ? "LOJA 1" : ((entity0.COMBOBOX_LOJA) == 2 ? "LOJA 2" : ((entity0.COMBOBOX_LOJA) == 3 ? "LOJA 3" : "")))
                , DatetimeLoja = entity0.DATETIME_LOJA
                , DecimalLoja = entity0.DECIMAL_LOJA
                , GuidLoja = entity0.GUID_LOJA
                , IdLoja = entity0.ID_LOJA
                , IntLoja = entity0.INT_LOJA
                , SmallIntLoja = entity0.SMALL_INT_LOJA
                , StringLoja = entity0.STRING_LOJA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [EstadoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get Estado.
	    public IQueryable<Estado> GetEstado()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetEstado")))
 	        {
 	             AuthorizationResult authorizationResult = (new EstadoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<Estado> result = 
	            (from entity0 in this.DbContext.ESTADO
                  let entity0Al1 = entity0.PAIS
	            
	            	
	            select new Estado()		
	            {
	            
                BigIntEstado = entity0.BIG_INT_ESTADO
                , BitEstado = entity0.BIT_ESTADO
                , ComboboxEstado = entity0.COMBOBOX_ESTADO
                , ComboboxEstadoName = ((entity0.COMBOBOX_ESTADO) == 1 ? "ESTADO 1" : ((entity0.COMBOBOX_ESTADO) == 2 ? "ESTADO 2" : ((entity0.COMBOBOX_ESTADO) == 3 ? "ESTADO 3" : ((entity0.COMBOBOX_ESTADO) == 4 ? "ESTADO 4" : ""))))
                , DatetimeEstado = entity0.DATETIME_ESTADO
                , DecimalEstado = entity0.DECIMAL_ESTADO
                , GuidEstado = entity0.GUID_ESTADO
                , IdEstado = entity0.ID_ESTADO
                , IdPais = entity0Al1.ID_PAIS
                , IntEstado = entity0.INT_ESTADO
                , SmallIntEstado = entity0.SMALL_INT_ESTADO
                , StringEstado = entity0.STRING_ESTADO
                , StringPais = entity0Al1.STRING_PAIS
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [EstadoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get EstadoNoAssociations.
	    public IQueryable<Estado> GetEstadoNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetEstadoNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new EstadoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<Estado> result = 
	            (from entity0 in this.DbContext.ESTADO
                  let entity0Al1 = entity0.PAIS
	            
	            	
	            select new Estado()		
	            {
	            
                BigIntEstado = entity0.BIG_INT_ESTADO
                , BitEstado = entity0.BIT_ESTADO
                , ComboboxEstado = entity0.COMBOBOX_ESTADO
                , ComboboxEstadoName = ((entity0.COMBOBOX_ESTADO) == 1 ? "ESTADO 1" : ((entity0.COMBOBOX_ESTADO) == 2 ? "ESTADO 2" : ((entity0.COMBOBOX_ESTADO) == 3 ? "ESTADO 3" : ((entity0.COMBOBOX_ESTADO) == 4 ? "ESTADO 4" : ""))))
                , DatetimeEstado = entity0.DATETIME_ESTADO
                , DecimalEstado = entity0.DECIMAL_ESTADO
                , GuidEstado = entity0.GUID_ESTADO
                , IdEstado = entity0.ID_ESTADO
                , IdPais = entity0Al1.ID_PAIS
                , IntEstado = entity0.INT_ESTADO
                , SmallIntEstado = entity0.SMALL_INT_ESTADO
                , StringEstado = entity0.STRING_ESTADO
                , StringPais = entity0Al1.STRING_PAIS
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	//Add filtering disabled property for CLIENTE
	    	string[] bmDisabledClienteList = this.GetEDM().GetFilteringDisabledList("CLIENTE");
	    	if (bmDisabledClienteList.Length > 0)
	    	{
	
	    		if (bmDisabledClienteList.Contains("CLIENTE.BIG_INT_CLIENTE"))
	    		{
	    			result.Add("Cliente|BigIntCliente");
	    			result.Add("Cliente|CLIENTE.BIG_INT_CLIENTE");
	    		}
	
	    		if (bmDisabledClienteList.Contains("CLIENTE.BIT_CLIENTE"))
	    		{
	    			result.Add("Cliente|BitCliente");
	    			result.Add("Cliente|CLIENTE.BIT_CLIENTE");
	    		}
	
	    		if (bmDisabledClienteList.Contains("CLIENTE.COMBOBOX_CLIENTE"))
	    		{
	    			result.Add("Cliente|ComboboxCliente");
	    			result.Add("Cliente|CLIENTE.COMBOBOX_CLIENTE");
	    		}
	
	    		if (bmDisabledClienteList.Contains("CLIENTE.DATETIME_CLIENTE"))
	    		{
	    			result.Add("Cliente|DatetimeCliente");
	    			result.Add("Cliente|CLIENTE.DATETIME_CLIENTE");
	    		}
	
	    		if (bmDisabledClienteList.Contains("CLIENTE.DECIMAL_CLIENTE"))
	    		{
	    			result.Add("Cliente|DecimalCliente");
	    			result.Add("Cliente|CLIENTE.DECIMAL_CLIENTE");
	    		}
	
	    		if (bmDisabledClienteList.Contains("CLIENTE.GUID_CLIENTE"))
	    		{
	    			result.Add("Cliente|GuidCliente");
	    			result.Add("Cliente|CLIENTE.GUID_CLIENTE");
	    		}
	
	    		if (bmDisabledClienteList.Contains("CLIENTE.ID_CLIENTE"))
	    		{
	    			result.Add("Cliente|IdCliente");
	    			result.Add("Cliente|CLIENTE.ID_CLIENTE");
	    		}
	
	    		if (bmDisabledClienteList.Contains("CLIENTE.INT_CLIENTE"))
	    		{
	    			result.Add("Cliente|IntCliente");
	    			result.Add("Cliente|CLIENTE.INT_CLIENTE");
	    		}
	
	    		if (bmDisabledClienteList.Contains("CLIENTE.SMALL_INT_CLIENTE"))
	    		{
	    			result.Add("Cliente|SmallIntCliente");
	    			result.Add("Cliente|CLIENTE.SMALL_INT_CLIENTE");
	    		}
	
	    		if (bmDisabledClienteList.Contains("CLIENTE.STRING_CLIENTE"))
	    		{
	    			result.Add("Cliente|StringCliente");
	    			result.Add("Cliente|CLIENTE.STRING_CLIENTE");
	    		}
	    	}
	    	//Add filtering disabled property for VENDA
	    	string[] bmDisabledVendaList = this.GetEDM().GetFilteringDisabledList("VENDA");
	    	if (bmDisabledVendaList.Length > 0)
	    	{
	
	    		if (bmDisabledVendaList.Contains("VENDA.BIG_INT_VENDA"))
	    		{
	    			result.Add("Venda|BigIntVenda");
	    			result.Add("Venda|VENDA.BIG_INT_VENDA");
	    		}
	
	    		if (bmDisabledVendaList.Contains("VENDA.BIT_VENDA"))
	    		{
	    			result.Add("Venda|BitVenda");
	    			result.Add("Venda|VENDA.BIT_VENDA");
	    		}
	
	    		if (bmDisabledVendaList.Contains("VENDA.COMBOBOX_VENDA"))
	    		{
	    			result.Add("Venda|ComboboxVenda");
	    			result.Add("Venda|VENDA.COMBOBOX_VENDA");
	    		}
	
	    		if (bmDisabledVendaList.Contains("VENDA.DATETIME_VENDA"))
	    		{
	    			result.Add("Venda|DatetimeVenda");
	    			result.Add("Venda|VENDA.DATETIME_VENDA");
	    		}
	
	    		if (bmDisabledVendaList.Contains("VENDA.DECIMAL_VENDA"))
	    		{
	    			result.Add("Venda|DecimalVenda");
	    			result.Add("Venda|VENDA.DECIMAL_VENDA");
	    		}
	
	    		if (bmDisabledVendaList.Contains("VENDA.GUID_VENDA"))
	    		{
	    			result.Add("Venda|GuidVenda");
	    			result.Add("Venda|VENDA.GUID_VENDA");
	    		}
	
	    		if (bmDisabledVendaList.Contains("VENDA.ID_VENDA"))
	    		{
	    			result.Add("Venda|IdVenda");
	    			result.Add("Venda|VENDA.ID_VENDA");
	    		}
	
	    		if (bmDisabledVendaList.Contains("VENDA.INT_VENDA"))
	    		{
	    			result.Add("Venda|IntVenda");
	    			result.Add("Venda|VENDA.INT_VENDA");
	    		}
	
	    		if (bmDisabledVendaList.Contains("VENDA.SMALL_INT_VENDA"))
	    		{
	    			result.Add("Venda|SmallIntVenda");
	    			result.Add("Venda|VENDA.SMALL_INT_VENDA");
	    		}
	
	    		if (bmDisabledVendaList.Contains("VENDA.STRING_VENDA"))
	    		{
	    			result.Add("Venda|StringVenda");
	    			result.Add("Venda|VENDA.STRING_VENDA");
	    		}
	    	}
	    	//Add filtering disabled property for VENDA_ITEM
	    	string[] bmDisabledVendaItemList = this.GetEDM().GetFilteringDisabledList("VENDA_ITEM");
	    	if (bmDisabledVendaItemList.Length > 0)
	    	{
	
	    		if (bmDisabledVendaItemList.Contains("VENDA_ITEM.BIG_INT_VENDA_ITEM"))
	    		{
	    			result.Add("VendaItem|BigIntVendaItem");
	    			result.Add("VendaItem|VENDA_ITEM.BIG_INT_VENDA_ITEM");
	    		}
	
	    		if (bmDisabledVendaItemList.Contains("VENDA_ITEM.BIT_VENDA_ITEM"))
	    		{
	    			result.Add("VendaItem|BitVendaItem");
	    			result.Add("VendaItem|VENDA_ITEM.BIT_VENDA_ITEM");
	    		}
	
	    		if (bmDisabledVendaItemList.Contains("VENDA_ITEM.COMBOBOX_VENDA_ITEM"))
	    		{
	    			result.Add("VendaItem|ComboboxVendaItem");
	    			result.Add("VendaItem|VENDA_ITEM.COMBOBOX_VENDA_ITEM");
	    		}
	
	    		if (bmDisabledVendaItemList.Contains("VENDA_ITEM.DATETIME_VENDA_ITEM"))
	    		{
	    			result.Add("VendaItem|DatetimeVendaItem");
	    			result.Add("VendaItem|VENDA_ITEM.DATETIME_VENDA_ITEM");
	    		}
	
	    		if (bmDisabledVendaItemList.Contains("VENDA_ITEM.DECIMAL_VENDA_ITEM"))
	    		{
	    			result.Add("VendaItem|DecimalVendaItem");
	    			result.Add("VendaItem|VENDA_ITEM.DECIMAL_VENDA_ITEM");
	    		}
	
	    		if (bmDisabledVendaItemList.Contains("VENDA_ITEM.GUID_VENDA_ITEM"))
	    		{
	    			result.Add("VendaItem|GuidVendaItem");
	    			result.Add("VendaItem|VENDA_ITEM.GUID_VENDA_ITEM");
	    		}
	
	    		if (bmDisabledVendaItemList.Contains("VENDA_ITEM.ID_VENDA_ITEM"))
	    		{
	    			result.Add("VendaItem|IdVendaItem");
	    			result.Add("VendaItem|VENDA_ITEM.ID_VENDA_ITEM");
	    		}
	
	    		if (bmDisabledVendaItemList.Contains("VENDA_ITEM.INT_VENDA_ITEM"))
	    		{
	    			result.Add("VendaItem|IntVendaItem");
	    			result.Add("VendaItem|VENDA_ITEM.INT_VENDA_ITEM");
	    		}
	
	    		if (bmDisabledVendaItemList.Contains("VENDA_ITEM.SMALL_INT_VENDA_ITEM"))
	    		{
	    			result.Add("VendaItem|SmallIntVendaItem");
	    			result.Add("VendaItem|VENDA_ITEM.SMALL_INT_VENDA_ITEM");
	    		}
	
	    		if (bmDisabledVendaItemList.Contains("VENDA_ITEM.STRING_VENDA_ITEM"))
	    		{
	    			result.Add("VendaItem|StringVendaItem");
	    			result.Add("VendaItem|VENDA_ITEM.STRING_VENDA_ITEM");
	    		}
	    	}
	    	//Add filtering disabled property for FORMA_PAGAMENTO
	    	string[] bmDisabledFormaPagamentoList = this.GetEDM().GetFilteringDisabledList("FORMA_PAGAMENTO");
	    	if (bmDisabledFormaPagamentoList.Length > 0)
	    	{
	
	    		if (bmDisabledFormaPagamentoList.Contains("FORMA_PAGAMENTO.BIG_INT_FORMA_PAGAMENTO"))
	    		{
	    			result.Add("FormaPagamento|BigIntFormaPagamento");
	    			result.Add("FormaPagamento|FORMA_PAGAMENTO.BIG_INT_FORMA_PAGAMENTO");
	    		}
	
	    		if (bmDisabledFormaPagamentoList.Contains("FORMA_PAGAMENTO.BIT_FORMA_PAGAMENTO"))
	    		{
	    			result.Add("FormaPagamento|BitFormaPagamento");
	    			result.Add("FormaPagamento|FORMA_PAGAMENTO.BIT_FORMA_PAGAMENTO");
	    		}
	
	    		if (bmDisabledFormaPagamentoList.Contains("FORMA_PAGAMENTO.COMBOBOX_FORMA_PAGAMENTO"))
	    		{
	    			result.Add("FormaPagamento|ComboboxFormaPagamento");
	    			result.Add("FormaPagamento|FORMA_PAGAMENTO.COMBOBOX_FORMA_PAGAMENTO");
	    		}
	
	    		if (bmDisabledFormaPagamentoList.Contains("FORMA_PAGAMENTO.DATETIME_FORMA_PAGAMENTO"))
	    		{
	    			result.Add("FormaPagamento|DatetimeFormaPagamento");
	    			result.Add("FormaPagamento|FORMA_PAGAMENTO.DATETIME_FORMA_PAGAMENTO");
	    		}
	
	    		if (bmDisabledFormaPagamentoList.Contains("FORMA_PAGAMENTO.DECIMAL_FORMA_PAGAMENTO"))
	    		{
	    			result.Add("FormaPagamento|DecimalFormaPagamento");
	    			result.Add("FormaPagamento|FORMA_PAGAMENTO.DECIMAL_FORMA_PAGAMENTO");
	    		}
	
	    		if (bmDisabledFormaPagamentoList.Contains("FORMA_PAGAMENTO.GUID_FORMA_PAGAMENTO"))
	    		{
	    			result.Add("FormaPagamento|GuidFormaPagamento");
	    			result.Add("FormaPagamento|FORMA_PAGAMENTO.GUID_FORMA_PAGAMENTO");
	    		}
	
	    		if (bmDisabledFormaPagamentoList.Contains("FORMA_PAGAMENTO.ID_FORMA_PAGAMENTO"))
	    		{
	    			result.Add("FormaPagamento|IdFormaPagamento");
	    			result.Add("FormaPagamento|FORMA_PAGAMENTO.ID_FORMA_PAGAMENTO");
	    		}
	
	    		if (bmDisabledFormaPagamentoList.Contains("FORMA_PAGAMENTO.INT_FORMA_PAGAMENTO"))
	    		{
	    			result.Add("FormaPagamento|IntFormaPagamento");
	    			result.Add("FormaPagamento|FORMA_PAGAMENTO.INT_FORMA_PAGAMENTO");
	    		}
	
	    		if (bmDisabledFormaPagamentoList.Contains("FORMA_PAGAMENTO.SMALL_INT_FORMA_PAGAMENTO"))
	    		{
	    			result.Add("FormaPagamento|SmallIntFormaPagamento");
	    			result.Add("FormaPagamento|FORMA_PAGAMENTO.SMALL_INT_FORMA_PAGAMENTO");
	    		}
	
	    		if (bmDisabledFormaPagamentoList.Contains("FORMA_PAGAMENTO.STRING_FORMA_PAGAMENTO"))
	    		{
	    			result.Add("FormaPagamento|StringFormaPagamento");
	    			result.Add("FormaPagamento|FORMA_PAGAMENTO.STRING_FORMA_PAGAMENTO");
	    		}
	    	}
	    	//Add filtering disabled property for LOJA
	    	string[] bmDisabledLojaList = this.GetEDM().GetFilteringDisabledList("LOJA");
	    	if (bmDisabledLojaList.Length > 0)
	    	{
	
	    		if (bmDisabledLojaList.Contains("LOJA.BIG_INT_LOJA"))
	    		{
	    			result.Add("Loja|BigIntLoja");
	    			result.Add("Loja|LOJA.BIG_INT_LOJA");
	    		}
	
	    		if (bmDisabledLojaList.Contains("LOJA.BIT_LOJA"))
	    		{
	    			result.Add("Loja|BitLoja");
	    			result.Add("Loja|LOJA.BIT_LOJA");
	    		}
	
	    		if (bmDisabledLojaList.Contains("LOJA.COMBOBOX_LOJA"))
	    		{
	    			result.Add("Loja|ComboboxLoja");
	    			result.Add("Loja|LOJA.COMBOBOX_LOJA");
	    		}
	
	    		if (bmDisabledLojaList.Contains("LOJA.DATETIME_LOJA"))
	    		{
	    			result.Add("Loja|DatetimeLoja");
	    			result.Add("Loja|LOJA.DATETIME_LOJA");
	    		}
	
	    		if (bmDisabledLojaList.Contains("LOJA.DECIMAL_LOJA"))
	    		{
	    			result.Add("Loja|DecimalLoja");
	    			result.Add("Loja|LOJA.DECIMAL_LOJA");
	    		}
	
	    		if (bmDisabledLojaList.Contains("LOJA.GUID_LOJA"))
	    		{
	    			result.Add("Loja|GuidLoja");
	    			result.Add("Loja|LOJA.GUID_LOJA");
	    		}
	
	    		if (bmDisabledLojaList.Contains("LOJA.ID_LOJA"))
	    		{
	    			result.Add("Loja|IdLoja");
	    			result.Add("Loja|LOJA.ID_LOJA");
	    		}
	
	    		if (bmDisabledLojaList.Contains("LOJA.INT_LOJA"))
	    		{
	    			result.Add("Loja|IntLoja");
	    			result.Add("Loja|LOJA.INT_LOJA");
	    		}
	
	    		if (bmDisabledLojaList.Contains("LOJA.SMALL_INT_LOJA"))
	    		{
	    			result.Add("Loja|SmallIntLoja");
	    			result.Add("Loja|LOJA.SMALL_INT_LOJA");
	    		}
	
	    		if (bmDisabledLojaList.Contains("LOJA.STRING_LOJA"))
	    		{
	    			result.Add("Loja|StringLoja");
	    			result.Add("Loja|LOJA.STRING_LOJA");
	    		}
	    	}
	    	//Add filtering disabled property for ESTADO
	    	string[] bmDisabledEstadoList = this.GetEDM().GetFilteringDisabledList("ESTADO");
	    	if (bmDisabledEstadoList.Length > 0)
	    	{
	
	    		if (bmDisabledEstadoList.Contains("ESTADO.BIG_INT_ESTADO"))
	    		{
	    			result.Add("Estado|BigIntEstado");
	    			result.Add("Estado|ESTADO.BIG_INT_ESTADO");
	    		}
	
	    		if (bmDisabledEstadoList.Contains("ESTADO.BIT_ESTADO"))
	    		{
	    			result.Add("Estado|BitEstado");
	    			result.Add("Estado|ESTADO.BIT_ESTADO");
	    		}
	
	    		if (bmDisabledEstadoList.Contains("ESTADO.COMBOBOX_ESTADO"))
	    		{
	    			result.Add("Estado|ComboboxEstado");
	    			result.Add("Estado|ESTADO.COMBOBOX_ESTADO");
	    		}
	
	    		if (bmDisabledEstadoList.Contains("ESTADO.DATETIME_ESTADO"))
	    		{
	    			result.Add("Estado|DatetimeEstado");
	    			result.Add("Estado|ESTADO.DATETIME_ESTADO");
	    		}
	
	    		if (bmDisabledEstadoList.Contains("ESTADO.DECIMAL_ESTADO"))
	    		{
	    			result.Add("Estado|DecimalEstado");
	    			result.Add("Estado|ESTADO.DECIMAL_ESTADO");
	    		}
	
	    		if (bmDisabledEstadoList.Contains("ESTADO.GUID_ESTADO"))
	    		{
	    			result.Add("Estado|GuidEstado");
	    			result.Add("Estado|ESTADO.GUID_ESTADO");
	    		}
	
	    		if (bmDisabledEstadoList.Contains("ESTADO.ID_ESTADO"))
	    		{
	    			result.Add("Estado|IdEstado");
	    			result.Add("Estado|ESTADO.ID_ESTADO");
	    		}
	
	    		if (bmDisabledEstadoList.Contains("ESTADO.INT_ESTADO"))
	    		{
	    			result.Add("Estado|IntEstado");
	    			result.Add("Estado|ESTADO.INT_ESTADO");
	    		}
	
	    		if (bmDisabledEstadoList.Contains("ESTADO.SMALL_INT_ESTADO"))
	    		{
	    			result.Add("Estado|SmallIntEstado");
	    			result.Add("Estado|ESTADO.SMALL_INT_ESTADO");
	    		}
	
	    		if (bmDisabledEstadoList.Contains("ESTADO.STRING_ESTADO"))
	    		{
	    			result.Add("Estado|StringEstado");
	    			result.Add("Estado|ESTADO.STRING_ESTADO");
	    		}
	    	}
	    	return result;
	    }

	    #endregion Excluded Filters

	    #region Get By EntitySearchId Methods Definitions.
	
			
	    [Ignore]
	    //Add EntitySearch Id.
	    public void AddEntitySearchId(Guid entitySearchId, string searchDefinition)
	    {	
	            Linx.Tools.WebCacheHelper.AddWebCache(entitySearchId.ToString(), searchDefinition);
	    }
	    
	    [Ignore]
	    //Remove EntitySearch Id.
	    public void RemoveEntitySearchId(Guid entitySearchId)
	    {	
	            Linx.Tools.WebCacheHelper.RemoveWebCache(entitySearchId.ToString());
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get Cliente By EntitySearchId.
	    public IQueryable<Cliente> GetClienteByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetClienteByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get Cliente By EntitySearchId.
	    public IQueryable<Cliente> GetClienteByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetClienteByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get Venda By EntitySearchId.
	    public IQueryable<Venda> GetVendaByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetVendaByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get VendaItem By EntitySearchId.
	    public IQueryable<VendaItem> GetVendaItemByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetVendaItemByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get Venda By EntitySearchId.
	    public IQueryable<Venda> GetVendaByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetVendaByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get VendaItem By EntitySearchId.
	    public IQueryable<VendaItem> GetVendaItemByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetVendaItemByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get FormaPagamento By EntitySearchId.
	    public IQueryable<FormaPagamento> GetFormaPagamentoByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetFormaPagamentoByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get FormaPagamento By EntitySearchId.
	    public IQueryable<FormaPagamento> GetFormaPagamentoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetFormaPagamentoByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get Loja By EntitySearchId.
	    public IQueryable<Loja> GetLojaByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetLojaByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get Loja By EntitySearchId.
	    public IQueryable<Loja> GetLojaByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetLojaByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get Estado By EntitySearchId.
	    public IQueryable<Estado> GetEstadoByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetEstadoByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get Estado By EntitySearchId.
	    public IQueryable<Estado> GetEstadoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetEstadoByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get Cliente By Example.
	    [Ignore]
	    public IQueryable<Cliente> GetClienteByExample(Cliente entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetClienteByEntitySearch(queryAnalysis);
	    }
			
	    //Get Cliente By Example.
	    [Ignore]
	    public IQueryable<Cliente> GetClienteByExampleNoAssociations(Cliente entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetClienteByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get Venda By Example.
	    [Ignore]
	    public IQueryable<Venda> GetVendaByExample(Venda entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetVendaByEntitySearch(queryAnalysis);
	    }
			
	    //Get VendaItem By Example.
	    [Ignore]
	    public IQueryable<VendaItem> GetVendaItemByExample(VendaItem entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetVendaItemByEntitySearch(queryAnalysis);
	    }
			
	    //Get Venda By Example.
	    [Ignore]
	    public IQueryable<Venda> GetVendaByExampleNoAssociations(Venda entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetVendaByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get VendaItem By Example.
	    [Ignore]
	    public IQueryable<VendaItem> GetVendaItemByExampleNoAssociations(VendaItem entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetVendaItemByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get FormaPagamento By Example.
	    [Ignore]
	    public IQueryable<FormaPagamento> GetFormaPagamentoByExample(FormaPagamento entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetFormaPagamentoByEntitySearch(queryAnalysis);
	    }
			
	    //Get FormaPagamento By Example.
	    [Ignore]
	    public IQueryable<FormaPagamento> GetFormaPagamentoByExampleNoAssociations(FormaPagamento entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetFormaPagamentoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get Loja By Example.
	    [Ignore]
	    public IQueryable<Loja> GetLojaByExample(Loja entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetLojaByEntitySearch(queryAnalysis);
	    }
			
	    //Get Loja By Example.
	    [Ignore]
	    public IQueryable<Loja> GetLojaByExampleNoAssociations(Loja entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetLojaByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get Estado By Example.
	    [Ignore]
	    public IQueryable<Estado> GetEstadoByExample(Estado entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetEstadoByEntitySearch(queryAnalysis);
	    }
			
	    //Get Estado By Example.
	    [Ignore]
	    public IQueryable<Estado> GetEstadoByExampleNoAssociations(Estado entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetEstadoByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public Cliente GetClienteByKey(int idCliente)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("Cliente");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdCliente"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idCliente));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetClienteByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public Venda GetVendaByKey(int idVenda)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("Venda");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdVenda"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idVenda));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetVendaByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public VendaItem GetVendaItemByKey(int idVendaItem)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("VendaItem");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdVendaItem"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idVendaItem));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetVendaItemByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public FormaPagamento GetFormaPagamentoByKey(int idFormaPagamento)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("FormaPagamento");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdFormaPagamento"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idFormaPagamento));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetFormaPagamentoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public Loja GetLojaByKey(int idLoja)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("Loja");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdLoja"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idLoja));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetLojaByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public Estado GetEstadoByKey(int idEstado)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("Estado");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdEstado"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idEstado));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetEstadoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    [ClienteQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get ClienteByEntitySearch.
	    public IQueryable<Cliente> GetClienteByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetClienteByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new ClienteQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Cliente));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<Cliente> result = 
	            (from entity0 in this.DbContext.CLIENTE.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.ESTADO
                  let entity0Al2 = entity0.ESTADO.PAIS
	            
	            	
	            select new Cliente()		
	            {
	            
                BigIntCliente = entity0.BIG_INT_CLIENTE
                , BitCliente = entity0.BIT_CLIENTE
                , ComboboxCliente = entity0.COMBOBOX_CLIENTE
                , ComboboxClienteName = ((entity0.COMBOBOX_CLIENTE) == 1 ? "CLIENTE 1" : ((entity0.COMBOBOX_CLIENTE) == 2 ? "CLIENTE 2" : ((entity0.COMBOBOX_CLIENTE) == 3 ? "CLIENTE 3" : "")))
                , DatetimeCliente = entity0.DATETIME_CLIENTE
                , DecimalCliente = entity0.DECIMAL_CLIENTE
                , GuidCliente = entity0.GUID_CLIENTE
                , IdCliente = entity0.ID_CLIENTE
                , IdEstado = entity0Al1.ID_ESTADO
                , IdPais = entity0Al2.ID_PAIS
                , IntCliente = entity0.INT_CLIENTE
                , SmallIntCliente = entity0.SMALL_INT_CLIENTE
                , StringCliente = entity0.STRING_CLIENTE
                , StringEstado = entity0Al1.STRING_ESTADO
                , StringPais = entity0Al2.STRING_PAIS
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [ClienteQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get ClienteByEntitySearchNoAssociations.
	    public IQueryable<Cliente> GetClienteByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetClienteByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new ClienteQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Cliente));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<Cliente> result = 
	            (from entity0 in this.DbContext.CLIENTE.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.ESTADO
                  let entity0Al2 = entity0.ESTADO.PAIS
	            
	            	
	            select new Cliente()		
	            {
	            
                BigIntCliente = entity0.BIG_INT_CLIENTE
                , BitCliente = entity0.BIT_CLIENTE
                , ComboboxCliente = entity0.COMBOBOX_CLIENTE
                , ComboboxClienteName = ((entity0.COMBOBOX_CLIENTE) == 1 ? "CLIENTE 1" : ((entity0.COMBOBOX_CLIENTE) == 2 ? "CLIENTE 2" : ((entity0.COMBOBOX_CLIENTE) == 3 ? "CLIENTE 3" : "")))
                , DatetimeCliente = entity0.DATETIME_CLIENTE
                , DecimalCliente = entity0.DECIMAL_CLIENTE
                , GuidCliente = entity0.GUID_CLIENTE
                , IdCliente = entity0.ID_CLIENTE
                , IdEstado = entity0Al1.ID_ESTADO
                , IdPais = entity0Al2.ID_PAIS
                , IntCliente = entity0.INT_CLIENTE
                , SmallIntCliente = entity0.SMALL_INT_CLIENTE
                , StringCliente = entity0.STRING_CLIENTE
                , StringEstado = entity0Al1.STRING_ESTADO
                , StringPais = entity0Al2.STRING_PAIS
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendaQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendaByEntitySearch.
	    public IQueryable<Venda> GetVendaByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendaByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Venda));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<Venda> result = 
	            (from entity0 in this.DbContext.VENDA.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.LOJA
                  let entity0Al1 = entity0.CLIENTE
	            
	            	
	            select new Venda()		
	            {
	            
                BigIntVenda = entity0.BIG_INT_VENDA
                , BitVenda = entity0.BIT_VENDA
                , ComboboxVenda = entity0.COMBOBOX_VENDA
                , ComboboxVendaName = ((entity0.COMBOBOX_VENDA) == 1 ? "VENDA 1" : ((entity0.COMBOBOX_VENDA) == 2 ? "VENDA 2" : ((entity0.COMBOBOX_VENDA) == 3 ? "VENDA 3" : "")))
                , DatetimeVenda = entity0.DATETIME_VENDA
                , DecimalVenda = entity0.DECIMAL_VENDA
                , GuidVenda = entity0.GUID_VENDA
                , IdCliente = entity0Al1.ID_CLIENTE
                , IdLoja = entity0Al2.ID_LOJA
                , IdVenda = entity0.ID_VENDA
                , IntVenda = entity0.INT_VENDA
                , SmallIntVenda = entity0.SMALL_INT_VENDA
                , StringLoja = entity0Al2.STRING_LOJA
                , StringVenda = entity0.STRING_VENDA
			
                ,VendaItemList = 
	                        (from entity1 in entity0.VENDA_ITEM_LISTA
                                  let entity1Al2 = entity1.VENDA
                                  let entity1Al1 = entity1.VENDA.CLIENTE
	                        
	                        	
	                        select new VendaItem()
	                        {
	                        
                                BigIntVendaItem = entity1.BIG_INT_VENDA_ITEM
                                , BitVendaItem = entity1.BIT_VENDA_ITEM
                                , ComboboxVendaItem = entity1.COMBOBOX_VENDA_ITEM
                                , ComboboxVendaItemName = ((entity1.COMBOBOX_VENDA_ITEM) == 1 ? "VENDA ITEM 1" : ((entity1.COMBOBOX_VENDA_ITEM) == 2 ? "VENDA ITEM 2" : ((entity1.COMBOBOX_VENDA_ITEM) == 3 ? "VENDA ITEM 3" : "")))
                                , DatetimeVendaItem = entity1.DATETIME_VENDA_ITEM
                                , DecimalVendaItem = entity1.DECIMAL_VENDA_ITEM
                                , GuidVendaItem = entity1.GUID_VENDA_ITEM
                                , IdCliente = entity1Al1.ID_CLIENTE
                                , IdVenda = entity1Al2.ID_VENDA
                                , IdVendaItem = entity1.ID_VENDA_ITEM
                                , IntVenda = entity1Al2.INT_VENDA
                                , IntVendaItem = entity1.INT_VENDA_ITEM
                                , SmallIntVendaItem = entity1.SMALL_INT_VENDA_ITEM
                                , StringVendaItem = entity1.STRING_VENDA_ITEM
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendaItemQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendaItemByEntitySearch.
	    public IQueryable<VendaItem> GetVendaItemByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendaItemByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaItemQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(VendaItem));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<VendaItem> result = 
	            (from entity0 in this.DbContext.VENDA_ITEM.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.VENDA
                  let entity0Al1 = entity0.VENDA.CLIENTE
	            
	            	
	            select new VendaItem()		
	            {
	            
                BigIntVendaItem = entity0.BIG_INT_VENDA_ITEM
                , BitVendaItem = entity0.BIT_VENDA_ITEM
                , ComboboxVendaItem = entity0.COMBOBOX_VENDA_ITEM
                , ComboboxVendaItemName = ((entity0.COMBOBOX_VENDA_ITEM) == 1 ? "VENDA ITEM 1" : ((entity0.COMBOBOX_VENDA_ITEM) == 2 ? "VENDA ITEM 2" : ((entity0.COMBOBOX_VENDA_ITEM) == 3 ? "VENDA ITEM 3" : "")))
                , DatetimeVendaItem = entity0.DATETIME_VENDA_ITEM
                , DecimalVendaItem = entity0.DECIMAL_VENDA_ITEM
                , GuidVendaItem = entity0.GUID_VENDA_ITEM
                , IdCliente = entity0Al1.ID_CLIENTE
                , IdVenda = entity0Al2.ID_VENDA
                , IdVendaItem = entity0.ID_VENDA_ITEM
                , IntVenda = entity0Al2.INT_VENDA
                , IntVendaItem = entity0.INT_VENDA_ITEM
                , SmallIntVendaItem = entity0.SMALL_INT_VENDA_ITEM
                , StringVendaItem = entity0.STRING_VENDA_ITEM
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendaQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendaByEntitySearchNoAssociations.
	    public IQueryable<Venda> GetVendaByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendaByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Venda));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<Venda> result = 
	            (from entity0 in this.DbContext.VENDA.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.LOJA
                  let entity0Al1 = entity0.CLIENTE
	            
	            	
	            select new Venda()		
	            {
	            
                BigIntVenda = entity0.BIG_INT_VENDA
                , BitVenda = entity0.BIT_VENDA
                , ComboboxVenda = entity0.COMBOBOX_VENDA
                , ComboboxVendaName = ((entity0.COMBOBOX_VENDA) == 1 ? "VENDA 1" : ((entity0.COMBOBOX_VENDA) == 2 ? "VENDA 2" : ((entity0.COMBOBOX_VENDA) == 3 ? "VENDA 3" : "")))
                , DatetimeVenda = entity0.DATETIME_VENDA
                , DecimalVenda = entity0.DECIMAL_VENDA
                , GuidVenda = entity0.GUID_VENDA
                , IdCliente = entity0Al1.ID_CLIENTE
                , IdLoja = entity0Al2.ID_LOJA
                , IdVenda = entity0.ID_VENDA
                , IntVenda = entity0.INT_VENDA
                , SmallIntVenda = entity0.SMALL_INT_VENDA
                , StringLoja = entity0Al2.STRING_LOJA
                , StringVenda = entity0.STRING_VENDA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendaItemQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendaItemByEntitySearchNoAssociations.
	    public IQueryable<VendaItem> GetVendaItemByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendaItemByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaItemQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(VendaItem));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<VendaItem> result = 
	            (from entity0 in this.DbContext.VENDA_ITEM.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.VENDA
                  let entity0Al1 = entity0.VENDA.CLIENTE
	            
	            	
	            select new VendaItem()		
	            {
	            
                BigIntVendaItem = entity0.BIG_INT_VENDA_ITEM
                , BitVendaItem = entity0.BIT_VENDA_ITEM
                , ComboboxVendaItem = entity0.COMBOBOX_VENDA_ITEM
                , ComboboxVendaItemName = ((entity0.COMBOBOX_VENDA_ITEM) == 1 ? "VENDA ITEM 1" : ((entity0.COMBOBOX_VENDA_ITEM) == 2 ? "VENDA ITEM 2" : ((entity0.COMBOBOX_VENDA_ITEM) == 3 ? "VENDA ITEM 3" : "")))
                , DatetimeVendaItem = entity0.DATETIME_VENDA_ITEM
                , DecimalVendaItem = entity0.DECIMAL_VENDA_ITEM
                , GuidVendaItem = entity0.GUID_VENDA_ITEM
                , IdCliente = entity0Al1.ID_CLIENTE
                , IdVenda = entity0Al2.ID_VENDA
                , IdVendaItem = entity0.ID_VENDA_ITEM
                , IntVenda = entity0Al2.INT_VENDA
                , IntVendaItem = entity0.INT_VENDA_ITEM
                , SmallIntVendaItem = entity0.SMALL_INT_VENDA_ITEM
                , StringVendaItem = entity0.STRING_VENDA_ITEM
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendaItemQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendaItemParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<VendaItemParentComposition> GetVendaItemParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendaItemParentCompositionByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaItemQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "VENDA", "VENDA_ITEM", "VENDA", typeof(VendaItemParentComposition));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<VendaItemParentComposition> result = 
	            (from entity0 in this.DbContext.VENDA_ITEM.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.VENDA
                  let entity0Al1 = entity0.VENDA.CLIENTE
	            
	            	
	            select new VendaItemParentComposition()		
	            {
	            
                BigIntVendaItem = entity0.BIG_INT_VENDA_ITEM
                , BitVendaItem = entity0.BIT_VENDA_ITEM
                , ComboboxVendaItem = entity0.COMBOBOX_VENDA_ITEM
                , ComboboxVendaItemName = ((entity0.COMBOBOX_VENDA_ITEM) == 1 ? "VENDA ITEM 1" : ((entity0.COMBOBOX_VENDA_ITEM) == 2 ? "VENDA ITEM 2" : ((entity0.COMBOBOX_VENDA_ITEM) == 3 ? "VENDA ITEM 3" : "")))
                , DatetimeVendaItem = entity0.DATETIME_VENDA_ITEM
                , DecimalVendaItem = entity0.DECIMAL_VENDA_ITEM
                , GuidVendaItem = entity0.GUID_VENDA_ITEM
                , IdCliente = entity0Al1.ID_CLIENTE
                , IdVenda = entity0Al2.ID_VENDA
                , IdVendaItem = entity0.ID_VENDA_ITEM
                , IntVenda = entity0Al2.INT_VENDA
                , IntVendaItem = entity0.INT_VENDA_ITEM
                , SmallIntVendaItem = entity0.SMALL_INT_VENDA_ITEM
                , StringVendaItem = entity0.STRING_VENDA_ITEM
                //Venda Properties.
                , BigIntVenda = entity0.VENDA.BIG_INT_VENDA
                , BitVenda = entity0.VENDA.BIT_VENDA
                , ComboboxVenda = entity0.VENDA.COMBOBOX_VENDA
                , ComboboxVendaName = ((entity0.VENDA.COMBOBOX_VENDA) == 1 ? "VENDA 1" : ((entity0.VENDA.COMBOBOX_VENDA) == 2 ? "VENDA 2" : ((entity0.VENDA.COMBOBOX_VENDA) == 3 ? "VENDA 3" : "")))
                , DatetimeVenda = entity0.VENDA.DATETIME_VENDA
                , DecimalVenda = entity0.VENDA.DECIMAL_VENDA
                , GuidVenda = entity0.VENDA.GUID_VENDA
                , IdLoja = entity0.VENDA.LOJA.ID_LOJA
                , SmallIntVenda = entity0.VENDA.SMALL_INT_VENDA
                , StringLoja = entity0.VENDA.LOJA.STRING_LOJA
                , StringVenda = entity0.VENDA.STRING_VENDA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [FormaPagamentoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get FormaPagamentoByEntitySearch.
	    public IQueryable<FormaPagamento> GetFormaPagamentoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetFormaPagamentoByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new FormaPagamentoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(FormaPagamento));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<FormaPagamento> result = 
	            (from entity0 in this.DbContext.FORMA_PAGAMENTO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.VENDA
	            
	            	
	            select new FormaPagamento()		
	            {
	            
                BigIntFormaPagamento = entity0.BIG_INT_FORMA_PAGAMENTO
                , BitFormaPagamento = entity0.BIT_FORMA_PAGAMENTO
                , ComboboxFormaPagamento = entity0.COMBOBOX_FORMA_PAGAMENTO
                , ComboboxFormaPagamentoName = ((entity0.COMBOBOX_FORMA_PAGAMENTO) == 1 ? "FORMA PAGAMENTO 1" : ((entity0.COMBOBOX_FORMA_PAGAMENTO) == 2 ? "FORMA PAGAMENTO 2" : ((entity0.COMBOBOX_FORMA_PAGAMENTO) == 3 ? "FORMA PAGAMENTO 3" : "")))
                , DatetimeFormaPagamento = entity0.DATETIME_FORMA_PAGAMENTO
                , DecimalFormaPagamento = entity0.DECIMAL_FORMA_PAGAMENTO
                , GuidFormaPagamento = entity0.GUID_FORMA_PAGAMENTO
                , IdFormaPagamento = entity0.ID_FORMA_PAGAMENTO
                , IdVenda = entity0Al1.ID_VENDA
                , IntFormaPagamento = entity0.INT_FORMA_PAGAMENTO
                , SmallIntFormaPagamento = entity0.SMALL_INT_FORMA_PAGAMENTO
                , StringFormaPagamento = entity0.STRING_FORMA_PAGAMENTO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [FormaPagamentoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get FormaPagamentoByEntitySearchNoAssociations.
	    public IQueryable<FormaPagamento> GetFormaPagamentoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetFormaPagamentoByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new FormaPagamentoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(FormaPagamento));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<FormaPagamento> result = 
	            (from entity0 in this.DbContext.FORMA_PAGAMENTO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.VENDA
	            
	            	
	            select new FormaPagamento()		
	            {
	            
                BigIntFormaPagamento = entity0.BIG_INT_FORMA_PAGAMENTO
                , BitFormaPagamento = entity0.BIT_FORMA_PAGAMENTO
                , ComboboxFormaPagamento = entity0.COMBOBOX_FORMA_PAGAMENTO
                , ComboboxFormaPagamentoName = ((entity0.COMBOBOX_FORMA_PAGAMENTO) == 1 ? "FORMA PAGAMENTO 1" : ((entity0.COMBOBOX_FORMA_PAGAMENTO) == 2 ? "FORMA PAGAMENTO 2" : ((entity0.COMBOBOX_FORMA_PAGAMENTO) == 3 ? "FORMA PAGAMENTO 3" : "")))
                , DatetimeFormaPagamento = entity0.DATETIME_FORMA_PAGAMENTO
                , DecimalFormaPagamento = entity0.DECIMAL_FORMA_PAGAMENTO
                , GuidFormaPagamento = entity0.GUID_FORMA_PAGAMENTO
                , IdFormaPagamento = entity0.ID_FORMA_PAGAMENTO
                , IdVenda = entity0Al1.ID_VENDA
                , IntFormaPagamento = entity0.INT_FORMA_PAGAMENTO
                , SmallIntFormaPagamento = entity0.SMALL_INT_FORMA_PAGAMENTO
                , StringFormaPagamento = entity0.STRING_FORMA_PAGAMENTO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [LojaQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get LojaByEntitySearch.
	    public IQueryable<Loja> GetLojaByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetLojaByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new LojaQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Loja));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<Loja> result = 
	            (from entity0 in this.DbContext.LOJA.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new Loja()		
	            {
	            
                BigIntLoja = entity0.BIG_INT_LOJA
                , BitLoja = entity0.BIT_LOJA
                , ComboboxLoja = entity0.COMBOBOX_LOJA
                , ComboboxLojaName = ((entity0.COMBOBOX_LOJA) == 1 ? "LOJA 1" : ((entity0.COMBOBOX_LOJA) == 2 ? "LOJA 2" : ((entity0.COMBOBOX_LOJA) == 3 ? "LOJA 3" : "")))
                , DatetimeLoja = entity0.DATETIME_LOJA
                , DecimalLoja = entity0.DECIMAL_LOJA
                , GuidLoja = entity0.GUID_LOJA
                , IdLoja = entity0.ID_LOJA
                , IntLoja = entity0.INT_LOJA
                , SmallIntLoja = entity0.SMALL_INT_LOJA
                , StringLoja = entity0.STRING_LOJA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [LojaQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get LojaByEntitySearchNoAssociations.
	    public IQueryable<Loja> GetLojaByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetLojaByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new LojaQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Loja));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<Loja> result = 
	            (from entity0 in this.DbContext.LOJA.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new Loja()		
	            {
	            
                BigIntLoja = entity0.BIG_INT_LOJA
                , BitLoja = entity0.BIT_LOJA
                , ComboboxLoja = entity0.COMBOBOX_LOJA
                , ComboboxLojaName = ((entity0.COMBOBOX_LOJA) == 1 ? "LOJA 1" : ((entity0.COMBOBOX_LOJA) == 2 ? "LOJA 2" : ((entity0.COMBOBOX_LOJA) == 3 ? "LOJA 3" : "")))
                , DatetimeLoja = entity0.DATETIME_LOJA
                , DecimalLoja = entity0.DECIMAL_LOJA
                , GuidLoja = entity0.GUID_LOJA
                , IdLoja = entity0.ID_LOJA
                , IntLoja = entity0.INT_LOJA
                , SmallIntLoja = entity0.SMALL_INT_LOJA
                , StringLoja = entity0.STRING_LOJA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [EstadoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get EstadoByEntitySearch.
	    public IQueryable<Estado> GetEstadoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetEstadoByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new EstadoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Estado));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<Estado> result = 
	            (from entity0 in this.DbContext.ESTADO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.PAIS
	            
	            	
	            select new Estado()		
	            {
	            
                BigIntEstado = entity0.BIG_INT_ESTADO
                , BitEstado = entity0.BIT_ESTADO
                , ComboboxEstado = entity0.COMBOBOX_ESTADO
                , ComboboxEstadoName = ((entity0.COMBOBOX_ESTADO) == 1 ? "ESTADO 1" : ((entity0.COMBOBOX_ESTADO) == 2 ? "ESTADO 2" : ((entity0.COMBOBOX_ESTADO) == 3 ? "ESTADO 3" : ((entity0.COMBOBOX_ESTADO) == 4 ? "ESTADO 4" : ""))))
                , DatetimeEstado = entity0.DATETIME_ESTADO
                , DecimalEstado = entity0.DECIMAL_ESTADO
                , GuidEstado = entity0.GUID_ESTADO
                , IdEstado = entity0.ID_ESTADO
                , IdPais = entity0Al1.ID_PAIS
                , IntEstado = entity0.INT_ESTADO
                , SmallIntEstado = entity0.SMALL_INT_ESTADO
                , StringEstado = entity0.STRING_ESTADO
                , StringPais = entity0Al1.STRING_PAIS
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [EstadoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get EstadoByEntitySearchNoAssociations.
	    public IQueryable<Estado> GetEstadoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetEstadoByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new EstadoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Estado));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<Estado> result = 
	            (from entity0 in this.DbContext.ESTADO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.PAIS
	            
	            	
	            select new Estado()		
	            {
	            
                BigIntEstado = entity0.BIG_INT_ESTADO
                , BitEstado = entity0.BIT_ESTADO
                , ComboboxEstado = entity0.COMBOBOX_ESTADO
                , ComboboxEstadoName = ((entity0.COMBOBOX_ESTADO) == 1 ? "ESTADO 1" : ((entity0.COMBOBOX_ESTADO) == 2 ? "ESTADO 2" : ((entity0.COMBOBOX_ESTADO) == 3 ? "ESTADO 3" : ((entity0.COMBOBOX_ESTADO) == 4 ? "ESTADO 4" : ""))))
                , DatetimeEstado = entity0.DATETIME_ESTADO
                , DecimalEstado = entity0.DECIMAL_ESTADO
                , GuidEstado = entity0.GUID_ESTADO
                , IdEstado = entity0.ID_ESTADO
                , IdPais = entity0Al1.ID_PAIS
                , IntEstado = entity0.INT_ESTADO
                , SmallIntEstado = entity0.SMALL_INT_ESTADO
                , StringEstado = entity0.STRING_ESTADO
                , StringPais = entity0Al1.STRING_PAIS
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    [ClienteQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedCliente.
	    public IQueryable<Cliente> GetPagedCliente(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedCliente")))
 	        {
 	             AuthorizationResult authorizationResult = (new ClienteQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Cliente));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<Cliente> result = 
	            (from entity0 in this.DbContext.CLIENTE.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.ESTADO
                  let entity0Al2 = entity0.ESTADO.PAIS
                orderby entity0.ID_CLIENTE ascending
	            
	            	
	            select new Cliente()		
	            {
	            
                BigIntCliente = entity0.BIG_INT_CLIENTE
                , BitCliente = entity0.BIT_CLIENTE
                , ComboboxCliente = entity0.COMBOBOX_CLIENTE
                , ComboboxClienteName = ((entity0.COMBOBOX_CLIENTE) == 1 ? "CLIENTE 1" : ((entity0.COMBOBOX_CLIENTE) == 2 ? "CLIENTE 2" : ((entity0.COMBOBOX_CLIENTE) == 3 ? "CLIENTE 3" : "")))
                , DatetimeCliente = entity0.DATETIME_CLIENTE
                , DecimalCliente = entity0.DECIMAL_CLIENTE
                , GuidCliente = entity0.GUID_CLIENTE
                , IdCliente = entity0.ID_CLIENTE
                , IdEstado = entity0Al1.ID_ESTADO
                , IdPais = entity0Al2.ID_PAIS
                , IntCliente = entity0.INT_CLIENTE
                , SmallIntCliente = entity0.SMALL_INT_CLIENTE
                , StringCliente = entity0.STRING_CLIENTE
                , StringEstado = entity0Al1.STRING_ESTADO
                , StringPais = entity0Al2.STRING_PAIS
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetClienteCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Cliente));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.CLIENTE.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.ESTADO
                  let entityAl2 = entity.ESTADO.PAIS
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    [VendaQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedVenda.
	    public IQueryable<Venda> GetPagedVenda(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedVenda")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Venda));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<Venda> result = 
	            (from entity0 in this.DbContext.VENDA.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.LOJA
                  let entity0Al1 = entity0.CLIENTE
                orderby entity0.ID_VENDA ascending
	            
	            	
	            select new Venda()		
	            {
	            
                BigIntVenda = entity0.BIG_INT_VENDA
                , BitVenda = entity0.BIT_VENDA
                , ComboboxVenda = entity0.COMBOBOX_VENDA
                , ComboboxVendaName = ((entity0.COMBOBOX_VENDA) == 1 ? "VENDA 1" : ((entity0.COMBOBOX_VENDA) == 2 ? "VENDA 2" : ((entity0.COMBOBOX_VENDA) == 3 ? "VENDA 3" : "")))
                , DatetimeVenda = entity0.DATETIME_VENDA
                , DecimalVenda = entity0.DECIMAL_VENDA
                , GuidVenda = entity0.GUID_VENDA
                , IdCliente = entity0Al1.ID_CLIENTE
                , IdLoja = entity0Al2.ID_LOJA
                , IdVenda = entity0.ID_VENDA
                , IntVenda = entity0.INT_VENDA
                , SmallIntVenda = entity0.SMALL_INT_VENDA
                , StringLoja = entity0Al2.STRING_LOJA
                , StringVenda = entity0.STRING_VENDA
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendaItemQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedVendaItem.
	    public IQueryable<VendaItem> GetPagedVendaItem(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedVendaItem")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaItemQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(VendaItem));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<VendaItem> result = 
	            (from entity0 in this.DbContext.VENDA_ITEM.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.VENDA
                  let entity0Al1 = entity0.VENDA.CLIENTE
                orderby entity0.ID_VENDA_ITEM ascending
	            
	            	
	            select new VendaItem()		
	            {
	            
                BigIntVendaItem = entity0.BIG_INT_VENDA_ITEM
                , BitVendaItem = entity0.BIT_VENDA_ITEM
                , ComboboxVendaItem = entity0.COMBOBOX_VENDA_ITEM
                , ComboboxVendaItemName = ((entity0.COMBOBOX_VENDA_ITEM) == 1 ? "VENDA ITEM 1" : ((entity0.COMBOBOX_VENDA_ITEM) == 2 ? "VENDA ITEM 2" : ((entity0.COMBOBOX_VENDA_ITEM) == 3 ? "VENDA ITEM 3" : "")))
                , DatetimeVendaItem = entity0.DATETIME_VENDA_ITEM
                , DecimalVendaItem = entity0.DECIMAL_VENDA_ITEM
                , GuidVendaItem = entity0.GUID_VENDA_ITEM
                , IdCliente = entity0Al1.ID_CLIENTE
                , IdVenda = entity0Al2.ID_VENDA
                , IdVendaItem = entity0.ID_VENDA_ITEM
                , IntVenda = entity0Al2.INT_VENDA
                , IntVendaItem = entity0.INT_VENDA_ITEM
                , SmallIntVendaItem = entity0.SMALL_INT_VENDA_ITEM
                , StringVendaItem = entity0.STRING_VENDA_ITEM
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetVendaCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Venda));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.VENDA.Where(dynQuery, parameters.ToArray())
                  let entityAl2 = entity.LOJA
                  let entityAl1 = entity.CLIENTE
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetVendaItemCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(VendaItem));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.VENDA_ITEM.Where(dynQuery, parameters.ToArray())
                  let entityAl2 = entity.VENDA
                  let entityAl1 = entity.VENDA.CLIENTE
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    [FormaPagamentoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedFormaPagamento.
	    public IQueryable<FormaPagamento> GetPagedFormaPagamento(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedFormaPagamento")))
 	        {
 	             AuthorizationResult authorizationResult = (new FormaPagamentoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(FormaPagamento));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<FormaPagamento> result = 
	            (from entity0 in this.DbContext.FORMA_PAGAMENTO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.VENDA
                orderby entity0.ID_FORMA_PAGAMENTO ascending
	            
	            	
	            select new FormaPagamento()		
	            {
	            
                BigIntFormaPagamento = entity0.BIG_INT_FORMA_PAGAMENTO
                , BitFormaPagamento = entity0.BIT_FORMA_PAGAMENTO
                , ComboboxFormaPagamento = entity0.COMBOBOX_FORMA_PAGAMENTO
                , ComboboxFormaPagamentoName = ((entity0.COMBOBOX_FORMA_PAGAMENTO) == 1 ? "FORMA PAGAMENTO 1" : ((entity0.COMBOBOX_FORMA_PAGAMENTO) == 2 ? "FORMA PAGAMENTO 2" : ((entity0.COMBOBOX_FORMA_PAGAMENTO) == 3 ? "FORMA PAGAMENTO 3" : "")))
                , DatetimeFormaPagamento = entity0.DATETIME_FORMA_PAGAMENTO
                , DecimalFormaPagamento = entity0.DECIMAL_FORMA_PAGAMENTO
                , GuidFormaPagamento = entity0.GUID_FORMA_PAGAMENTO
                , IdFormaPagamento = entity0.ID_FORMA_PAGAMENTO
                , IdVenda = entity0Al1.ID_VENDA
                , IntFormaPagamento = entity0.INT_FORMA_PAGAMENTO
                , SmallIntFormaPagamento = entity0.SMALL_INT_FORMA_PAGAMENTO
                , StringFormaPagamento = entity0.STRING_FORMA_PAGAMENTO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetFormaPagamentoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(FormaPagamento));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.FORMA_PAGAMENTO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.VENDA
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    [LojaQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedLoja.
	    public IQueryable<Loja> GetPagedLoja(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedLoja")))
 	        {
 	             AuthorizationResult authorizationResult = (new LojaQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Loja));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<Loja> result = 
	            (from entity0 in this.DbContext.LOJA.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_LOJA ascending
	            
	            	
	            select new Loja()		
	            {
	            
                BigIntLoja = entity0.BIG_INT_LOJA
                , BitLoja = entity0.BIT_LOJA
                , ComboboxLoja = entity0.COMBOBOX_LOJA
                , ComboboxLojaName = ((entity0.COMBOBOX_LOJA) == 1 ? "LOJA 1" : ((entity0.COMBOBOX_LOJA) == 2 ? "LOJA 2" : ((entity0.COMBOBOX_LOJA) == 3 ? "LOJA 3" : "")))
                , DatetimeLoja = entity0.DATETIME_LOJA
                , DecimalLoja = entity0.DECIMAL_LOJA
                , GuidLoja = entity0.GUID_LOJA
                , IdLoja = entity0.ID_LOJA
                , IntLoja = entity0.INT_LOJA
                , SmallIntLoja = entity0.SMALL_INT_LOJA
                , StringLoja = entity0.STRING_LOJA
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetLojaCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Loja));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.LOJA.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    [EstadoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedEstado.
	    public IQueryable<Estado> GetPagedEstado(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedEstado")))
 	        {
 	             AuthorizationResult authorizationResult = (new EstadoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Estado));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<Estado> result = 
	            (from entity0 in this.DbContext.ESTADO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.PAIS
                orderby entity0.ID_ESTADO ascending
	            
	            	
	            select new Estado()		
	            {
	            
                BigIntEstado = entity0.BIG_INT_ESTADO
                , BitEstado = entity0.BIT_ESTADO
                , ComboboxEstado = entity0.COMBOBOX_ESTADO
                , ComboboxEstadoName = ((entity0.COMBOBOX_ESTADO) == 1 ? "ESTADO 1" : ((entity0.COMBOBOX_ESTADO) == 2 ? "ESTADO 2" : ((entity0.COMBOBOX_ESTADO) == 3 ? "ESTADO 3" : ((entity0.COMBOBOX_ESTADO) == 4 ? "ESTADO 4" : ""))))
                , DatetimeEstado = entity0.DATETIME_ESTADO
                , DecimalEstado = entity0.DECIMAL_ESTADO
                , GuidEstado = entity0.GUID_ESTADO
                , IdEstado = entity0.ID_ESTADO
                , IdPais = entity0Al1.ID_PAIS
                , IntEstado = entity0.INT_ESTADO
                , SmallIntEstado = entity0.SMALL_INT_ESTADO
                , StringEstado = entity0.STRING_ESTADO
                , StringPais = entity0Al1.STRING_PAIS
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetEstadoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Estado));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.ESTADO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.PAIS
	            
	            select 1
	            ).Count();	
		
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    [ClienteUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update Cliente.
	    public void UpdateCliente(Cliente entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateCliente")))
 	        {
 	             AuthorizationResult authorizationResult = (new ClienteUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    [ClienteInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert Cliente.
	    public void InsertCliente(Cliente entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertCliente")))
 	        {
 	             AuthorizationResult authorizationResult = (new ClienteInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    [ClienteDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete Cliente.
	    public void DeleteCliente(Cliente entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteCliente")))
 	        {
 	             AuthorizationResult authorizationResult = (new ClienteDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    [VendaUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update Venda.
	    public void UpdateVenda(Venda entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateVenda")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    [VendaInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert Venda.
	    public void InsertVenda(Venda entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertVenda")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    [VendaDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete Venda.
	    public void DeleteVenda(Venda entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteVenda")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    [VendaItemUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update VendaItem.
	    public void UpdateVendaItem(VendaItem entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateVendaItem")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaItemUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.Venda.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.Venda) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.Venda); 	
	            

	
	        }
	
	    }

	    [VendaItemInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert VendaItem.
	    public void InsertVendaItem(VendaItem entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertVendaItem")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaItemInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.Venda.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.Venda) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.Venda);
	            

	
	        }
	
	    }

	    [VendaItemDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete VendaItem.
	    public void DeleteVendaItem(VendaItem entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteVendaItem")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendaItemDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	        if (entity.Venda.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.Venda) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.Venda);
	            

	
	        }

	
	    }
		
			
	    [FormaPagamentoUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update FormaPagamento.
	    public void UpdateFormaPagamento(FormaPagamento entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateFormaPagamento")))
 	        {
 	             AuthorizationResult authorizationResult = (new FormaPagamentoUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    [FormaPagamentoInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert FormaPagamento.
	    public void InsertFormaPagamento(FormaPagamento entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertFormaPagamento")))
 	        {
 	             AuthorizationResult authorizationResult = (new FormaPagamentoInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    [FormaPagamentoDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete FormaPagamento.
	    public void DeleteFormaPagamento(FormaPagamento entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteFormaPagamento")))
 	        {
 	             AuthorizationResult authorizationResult = (new FormaPagamentoDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    [LojaUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update Loja.
	    public void UpdateLoja(Loja entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateLoja")))
 	        {
 	             AuthorizationResult authorizationResult = (new LojaUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    [LojaInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert Loja.
	    public void InsertLoja(Loja entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertLoja")))
 	        {
 	             AuthorizationResult authorizationResult = (new LojaInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    [LojaDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete Loja.
	    public void DeleteLoja(Loja entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteLoja")))
 	        {
 	             AuthorizationResult authorizationResult = (new LojaDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    [EstadoUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update Estado.
	    public void UpdateEstado(Estado entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateEstado")))
 	        {
 	             AuthorizationResult authorizationResult = (new EstadoUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    [EstadoInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert Estado.
	    public void InsertEstado(Estado entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertEstado")))
 	        {
 	             AuthorizationResult authorizationResult = (new EstadoInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    [EstadoDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete Estado.
	    public void DeleteEstado(Estado entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteEstado")))
 	        {
 	             AuthorizationResult authorizationResult = (new EstadoDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}