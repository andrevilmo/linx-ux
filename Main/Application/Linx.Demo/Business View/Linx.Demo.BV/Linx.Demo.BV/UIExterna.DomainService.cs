					
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

namespace Linx.Demo.BV.UIExterna
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="CLIENTE.ID_CLIENTE", IsUpdatable=false, EdmName="LINXDEMO.BM.BaseTeste")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[Cliente];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[];EdmEntityName[CLIENTE];EntityRelations[ESTADO(ESTADO)#PAIS(PAIS)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "Cliente")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Demo.BV.UIExterna.Cliente")]
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
	    [Display(Name = "Combobox Cliente", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    [Display(Name = "Datetime Cliente", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    [Display(Name = "Decimal Cliente", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[CLIENTE.DECIMAL_CLIENTE];IsMeasure[false]")]
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
	    [Display(Name = "Guid Cliente", Description="", Order = 6, AutoGenerateField = false, GroupName="", ResourceType= null)]
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
	    [Display(Name = "Id Cliente", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpEstado];LookUpTitle[Seleção de (Id Estado)];LookUpQuery[executeLookUpEstado];LookUpFinalize[finalizeLookUpEstado];LookUpDisplayColumns[{\"IdEstado\" : \"Id Estado\"}];LookUpColumns[{\"IdEstado\" : true}];FilterDataKey[CLIENTE.ESTADO.ID_ESTADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<int>#IdEstado#true##10:0##Id Estado#0#true##::LookUpEstado##false#false#ESTADO#ESTADO#Linx.Demo.BV.UIExterna#IQueryable###true#false", EdmKey="CLIENTE.ESTADO.ID_ESTADO")]
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
	    //Extensibility Partial Method Definitions For IntCliente
	    partial void OnIntClienteChanging(System.Nullable<int> value);
	    partial void OnIntClienteChanged();

	    private System.Nullable<int> _IntCliente;

	    [DataMember(Name = "IntCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int Cliente", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    [Display(Name = "String Cliente", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	//////////////////////// DomainService Class V1 ///////////////////////
	///////////////////////////////////////////////////////////////////////
	[EnableClientAccess()]	
	[DomainIdentifier("ProcessorOverviewUIExternaDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class UIExternaDomainService : DomainService, IDataServiceContext 
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

		
	    public UIExternaDomainService() : this("", null, null) { }
	    public UIExternaDomainService(string connectionString) : this(connectionString, null, null) { }
	    public UIExternaDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public UIExternaDomainService(LINXDEMO.BM.BaseTeste dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public UIExternaDomainService(string connectionString, LINXDEMO.BM.BaseTeste dataContext, Dictionary<string, string> headers) : base() 
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
	            
	            select new LookUpEstado()		
	            {
	            
                IdEstado = entity.ID_ESTADO
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
	
		

	        if (entityName.InList("Linx.Demo.BV.UIExterna.Cliente"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "Cliente",
	        			NameSpace = "Linx.Demo.BV.UIExterna",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "Cliente",
	        			ClearMethodName = "ClearCliente",
	        			QueryMethodName  = "GetPagedCliente",	
	        			CountingMethodName  = "GetCliente" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Demo.BV.UIExterna.Cliente"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Demo.BV.UIExterna.Cliente"), forceAll: forceAll)
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

         		    return new string[] { "Demo_UIExternaClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Demo.BV.ClientResources.UIExternaClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Demo_uIExternaService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Demo.BV.ClientResources.uIExternaService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
                , IntCliente = entity0.INT_CLIENTE
                , SmallIntCliente = entity0.SMALL_INT_CLIENTE
                , StringCliente = entity0.STRING_CLIENTE
		
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
                , IntCliente = entity0.INT_CLIENTE
                , SmallIntCliente = entity0.SMALL_INT_CLIENTE
                , StringCliente = entity0.STRING_CLIENTE
		
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
                , IntCliente = entity0.INT_CLIENTE
                , SmallIntCliente = entity0.SMALL_INT_CLIENTE
                , StringCliente = entity0.STRING_CLIENTE
		
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
                , IntCliente = entity0.INT_CLIENTE
                , SmallIntCliente = entity0.SMALL_INT_CLIENTE
                , StringCliente = entity0.STRING_CLIENTE
		
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
                , IntCliente = entity0.INT_CLIENTE
                , SmallIntCliente = entity0.SMALL_INT_CLIENTE
                , StringCliente = entity0.STRING_CLIENTE
		
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
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}