					
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
using Linx.Demo.BM;

namespace Linx.Demo.BV.ModalExterna
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="LOJA.ID_LOJA", IsUpdatable=false, EdmName="Linx.Demo.BM.BMDTesteFrame")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[Loja];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[];EdmEntityName[LOJA];EntityRelations[CIDADE(CIDADE)#ESTADO(ESTADO)#PAIS(PAIS)];EdmParentEntityName[];IsIQueryable[true]")]
		
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
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_COMBOBOX_LOJA];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LOJA.COMBOBOX_LOJA];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For IdCidade
	    partial void OnIdCidadeChanging(int value);
	    partial void OnIdCidadeChanged();

	    private int _IdCidade;

	    [DataMember(IsRequired = true, Name = "IdCidade", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Cidade", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpCidade];LookUpTitle[Seleção de (Id Cidade)];LookUpQuery[executeLookUpCidade];LookUpFinalize[finalizeLookUpCidade];LookUpDisplayColumns[{\"IdCidade\" : \"Id Cidade\", \"IdEstado\" : \"Id Estado\", \"IdPais\" : \"Id Pais\"}];LookUpColumns[{\"IdCidade\" : true, \"IdEstado\" : true, \"IdPais\" : true}];FilterDataKey[LOJA.CIDADE.ID_CIDADE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="int#IdCidade#true##10:0##Id Cidade#0#true##::LookUpCidade##false#false#CIDADE#CIDADE#Linx.Demo.BV.ModalExterna#IQueryable#IdEstado[IdEstado,IdPais];IdPais[IdPais]#IdCidade[IdEstado=IdEstado,IdPais=IdPais];IdEstado[IdPais=IdPais]#true#false", EdmKey="LOJA.CIDADE.ID_CIDADE")]
	    public int IdCidade
	    {
	    	    get
	    	    {
	    	          return _IdCidade;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdCidade != value)
	    	          {
	    	              this.ValidateProperty("IdCidade", value);
	    	              this.OnIdCidadeChanging(value);
	    	              this.RaiseDataMemberChanging("IdCidade");
	    	              this._IdCidade = value;
	    	              this.RaiseDataMemberChanged("IdCidade");
	    	              this.OnIdCidadeChanged();
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
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpCidade];LookUpTitle[Seleção de (Id Estado)];LookUpQuery[executeLookUpCidade];LookUpFinalize[finalizeLookUpCidade];LookUpDisplayColumns[{\"IdCidade\" : \"Id Cidade\", \"IdEstado\" : \"Id Estado\", \"IdPais\" : \"Id Pais\"}];LookUpColumns[{\"IdCidade\" : true, \"IdEstado\" : true, \"IdPais\" : true}];FilterDataKey[LOJA.CIDADE.ESTADO.ID_ESTADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<int>#IdEstado#false##10:0##Id Estado#1#true##::LookUpCidade##false#false#CIDADE#CIDADE#Linx.Demo.BV.ModalExterna#IQueryable#IdEstado[IdEstado,IdPais];IdPais[IdPais]#IdCidade[IdEstado=IdEstado,IdPais=IdPais];IdEstado[IdPais=IdPais]#true#false", EdmKey="LOJA.CIDADE.ESTADO.ID_ESTADO")]
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
	    //Extensibility Partial Method Definitions For IdLoja
	    partial void OnIdLojaChanging(int value);
	    partial void OnIdLojaChanged();

	    private int _IdLoja;

	    [DataMember(IsRequired = true, Name = "IdLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Loja", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    //Extensibility Partial Method Definitions For IdPais
	    partial void OnIdPaisChanging(System.Nullable<int> value);
	    partial void OnIdPaisChanged();

	    private System.Nullable<int> _IdPais;

	    [DataMember(Name = "IdPais", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Pais", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpCidade];LookUpTitle[Seleção de (Id Pais)];LookUpQuery[executeLookUpCidade];LookUpFinalize[finalizeLookUpCidade];LookUpDisplayColumns[{\"IdCidade\" : \"Id Cidade\", \"IdEstado\" : \"Id Estado\", \"IdPais\" : \"Id Pais\"}];LookUpColumns[{\"IdCidade\" : true, \"IdEstado\" : true, \"IdPais\" : true}];FilterDataKey[LOJA.CIDADE.ESTADO.PAIS.ID_PAIS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<int>#IdPais#false##10:0##Id Pais#2#true##::LookUpCidade##false#false#CIDADE#CIDADE#Linx.Demo.BV.ModalExterna#IQueryable#IdEstado[IdEstado,IdPais];IdPais[IdPais]#IdCidade[IdEstado=IdEstado,IdPais=IdPais];IdEstado[IdPais=IdPais]#true#false", EdmKey="LOJA.CIDADE.ESTADO.PAIS.ID_PAIS")]
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
	    //Extensibility Partial Method Definitions For IntLoja
	    partial void OnIntLojaChanging(System.Nullable<int> value);
	    partial void OnIntLojaChanged();

	    private System.Nullable<int> _IntLoja;

	    [DataMember(Name = "IntLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Int Loja", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
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

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "BMDTesteFrame.LOJA").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Demo.BM.LOJA), QualifiedEntitySetName = "BMDTesteFrame.LOJA" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.ID_LOJA", Source = "IdLoja", Target = "ID_LOJA", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.BIT_LOJA", Source = "BitLoja", Target = "BIT_LOJA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.INT_LOJA", Source = "IntLoja", Target = "INT_LOJA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.BIG_INT_LOJA", Source = "BigIntLoja", Target = "BIG_INT_LOJA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.DECIMAL_LOJA", Source = "DecimalLoja", Target = "DECIMAL_LOJA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.COMBOBOX_LOJA", Source = "ComboboxLoja", Target = "COMBOBOX_LOJA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.DATETIME_LOJA", Source = "DatetimeLoja", Target = "DATETIME_LOJA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.SMALL_INT_LOJA", Source = "SmallIntLoja", Target = "SMALL_INT_LOJA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LOJA.CIDADE.ID_CIDADE", Source = "IdCidade", Target = "ID_CIDADE", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BMDTesteFrame.CIDADE", RelationPropertyName = "CIDADE" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetComboboxLojaValues()
	    {
	    	    return Linx.Demo.BV.Domains.LX_COMBOBOX_LOJA.GetValues();
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

		

	[LinxPublicationView(PrimaryKeys="VENDA.ID_VENDA", IsUpdatable=false, EdmName="Linx.Demo.BM.BMDTesteFrame")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[Venda];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[];EdmEntityName[VENDA];EntityRelations[LOJA(LOJA)#CIDADE(CIDADE)#ESTADO(ESTADO)#PAIS(PAIS)#VENDEDOR(VENDEDOR)];EdmParentEntityName[];IsIQueryable[true]")]
		
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
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	    }

	    #endregion Flat Entities

		
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
	    [Display(Name = "Combobox Venda", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    [Display(Name = "Datetime Venda", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    [Display(Name = "Decimal Venda", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[VENDA.DECIMAL_VENDA];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For IdLoja
	    partial void OnIdLojaChanging(System.Nullable<int> value);
	    partial void OnIdLojaChanged();

	    private System.Nullable<int> _IdLoja;

	    [DataMember(Name = "IdLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Loja", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpLoja];LookUpTitle[Seleção de (Id Loja)];LookUpQuery[executeLookUpLoja];LookUpFinalize[finalizeLookUpLoja];LookUpDisplayColumns[{\"IdLoja\" : \"Id Loja\"}];LookUpColumns[{\"IdLoja\" : true}];FilterDataKey[VENDA.LOJA.ID_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<int>#IdLoja#true##10:0##Id Loja#0#true##::LookUpLoja##false#false#LOJA#LOJA#Linx.Demo.BV.ModalExterna#IQueryable###true#false", EdmKey="VENDA.LOJA.ID_LOJA")]
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
	    [Display(Name = "Id Venda", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    //Extensibility Partial Method Definitions For IdVendedor
	    partial void OnIdVendedorChanging(System.Nullable<int> value);
	    partial void OnIdVendedorChanged();

	    private System.Nullable<int> _IdVendedor;

	    [DataMember(Name = "IdVendedor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Vendedor", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpVendedor];LookUpTitle[Seleção de (Id Vendedor)];LookUpQuery[executeLookUpVendedor];LookUpFinalize[finalizeLookUpVendedor];LookUpDisplayColumns[{\"IdVendedor\" : \"Id Vendedor\"}];LookUpColumns[{\"IdVendedor\" : true}];FilterDataKey[VENDA.VENDEDOR.ID_VENDEDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<int>#IdVendedor#true##10:0##Id Vendedor#0#true##::LookUpVendedor##false#false#VENDEDOR#VENDEDOR#Linx.Demo.BV.ModalExterna#IQueryable###true#false", EdmKey="VENDA.VENDEDOR.ID_VENDEDOR")]
	    public System.Nullable<int> IdVendedor
	    {
	    	    get
	    	    {
	    	          return _IdVendedor;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdVendedor != value)
	    	          {
	    	              this.ValidateProperty("IdVendedor", value);
	    	              this.OnIdVendedorChanging(value);
	    	              this.RaiseDataMemberChanging("IdVendedor");
	    	              this._IdVendedor = value;
	    	              this.RaiseDataMemberChanged("IdVendedor");
	    	              this.OnIdVendedorChanged();
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
	    [Display(Name = "Int Venda", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    [Display(Name = "Small Int Venda", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
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

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "BMDTesteFrame.VENDA").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Demo.BM.VENDA), QualifiedEntitySetName = "BMDTesteFrame.VENDA" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.ID_VENDA", Source = "IdVenda", Target = "ID_VENDA", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.BIT_VENDA", Source = "BitVenda", Target = "BIT_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.INT_VENDA", Source = "IntVenda", Target = "INT_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.LOJA.ID_LOJA", Source = "IdLoja", Target = "ID_LOJA", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BMDTesteFrame.LOJA", RelationPropertyName = "LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.BIG_INT_VENDA", Source = "BigIntVenda", Target = "BIG_INT_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.DECIMAL_VENDA", Source = "DecimalVenda", Target = "DECIMAL_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.COMBOBOX_VENDA", Source = "ComboboxVenda", Target = "COMBOBOX_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.DATETIME_VENDA", Source = "DatetimeVenda", Target = "DATETIME_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.SMALL_INT_VENDA", Source = "SmallIntVenda", Target = "SMALL_INT_VENDA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.VENDA", RelationPropertyName = "VENDA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="VENDA.VENDEDOR.ID_VENDEDOR", Source = "IdVendedor", Target = "ID_VENDEDOR", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BMDTesteFrame.VENDEDOR", RelationPropertyName = "VENDEDOR" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

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

		

	[LinxPublicationView(PrimaryKeys="PAIS.ID_PAIS", IsUpdatable=false, EdmName="Linx.Demo.BM.BMDTesteFrame")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[Pais];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[];EdmEntityName[PAIS];EntityRelations[];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "Pais")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Demo.BV.ModalExterna.Pais")]
	public partial class Pais : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For ComboboxPais
	    partial void OnComboboxPaisChanging(byte value);
	    partial void OnComboboxPaisChanged();

	    private byte _ComboboxPais;

	    [DataMember(IsRequired = true, Name = "ComboboxPais", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Pais", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_COMBOBOX_PAIS];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[PAIS.COMBOBOX_PAIS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="PAIS.COMBOBOX_PAIS")]
	    public byte ComboboxPais
	    {
	    	    get
	    	    {
	    	          return _ComboboxPais;
	    	    }
	    	    set
	    	    {
	    	          if (this._ComboboxPais != value)
	    	          {
	    	              this.ValidateProperty("ComboboxPais", value);
	    	              this.OnComboboxPaisChanging(value);
	    	              this.RaiseDataMemberChanging("ComboboxPais");
	    	              this._ComboboxPais = value;
	    	              this.RaiseDataMemberChanged("ComboboxPais");
	    	              this.OnComboboxPaisChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DecimalPais
	    partial void OnDecimalPaisChanging(System.Nullable<decimal> value);
	    partial void OnDecimalPaisChanged();

	    private System.Nullable<decimal> _DecimalPais;

	    [DataMember(Name = "DecimalPais", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal Pais", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[13:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[PAIS.DECIMAL_PAIS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="PAIS.DECIMAL_PAIS")]
	    public System.Nullable<decimal> DecimalPais
	    {
	    	    get
	    	    {
	    	          return _DecimalPais;
	    	    }
	    	    set
	    	    {
	    	          if (this._DecimalPais != value)
	    	          {
	    	              this.ValidateProperty("DecimalPais", value);
	    	              this.OnDecimalPaisChanging(value);
	    	              this.RaiseDataMemberChanging("DecimalPais");
	    	              this._DecimalPais = value;
	    	              this.RaiseDataMemberChanged("DecimalPais");
	    	              this.OnDecimalPaisChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdPais
	    partial void OnIdPaisChanging(int value);
	    partial void OnIdPaisChanged();

	    private int _IdPais;

	    [DataMember(IsRequired = true, Name = "IdPais", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Pais", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[PAIS.ID_PAIS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="PAIS.ID_PAIS")]
	    public int IdPais
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
	    //Extensibility Partial Method Definitions For StringPais
	    partial void OnStringPaisChanging(string value);
	    partial void OnStringPaisChanged();

	    private string _StringPais;

	    [DataMember(Name = "StringPais", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Pais", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[PAIS.STRING_PAIS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="PAIS.STRING_PAIS")]
	    public string StringPais
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
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "BMDTesteFrame.PAIS").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Demo.BM.PAIS), QualifiedEntitySetName = "BMDTesteFrame.PAIS" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="PAIS.ID_PAIS", Source = "IdPais", Target = "ID_PAIS", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.PAIS", RelationPropertyName = "PAIS" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="PAIS.STRING_PAIS", Source = "StringPais", Target = "STRING_PAIS", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.PAIS", RelationPropertyName = "PAIS" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="PAIS.DECIMAL_PAIS", Source = "DecimalPais", Target = "DECIMAL_PAIS", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.PAIS", RelationPropertyName = "PAIS" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="PAIS.COMBOBOX_PAIS", Source = "ComboboxPais", Target = "COMBOBOX_PAIS", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.PAIS", RelationPropertyName = "PAIS" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetComboboxPaisValues()
	    {
	    	    return Linx.Demo.BV.Domains.LX_COMBOBOX_PAIS.GetValues();
	    }
	    private string _comboboxPaisName;
	    [DataMember(IsRequired = false, Name = "ComboboxPaisName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Combobox Pais", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ComboboxPaisName
	    {
	    	    get { if (this.ComboboxPais.IsNull()) { _comboboxPaisName = String.Empty; } else { string key = this.ComboboxPais.ToString(); var dmValues = this.GetComboboxPaisValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _comboboxPaisName) _comboboxPaisName = domainName; } return _comboboxPaisName; } set { _comboboxPaisName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="ESTADO.ID_ESTADO", IsUpdatable=false, EdmName="Linx.Demo.BM.BMDTesteFrame")]
	
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

	    #endregion Flat Entities

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For ComboboxEstado
	    partial void OnComboboxEstadoChanging(byte value);
	    partial void OnComboboxEstadoChanged();

	    private byte _ComboboxEstado;

	    [DataMember(IsRequired = true, Name = "ComboboxEstado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Estado", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[LX_COMBOBOX_ESTADO];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ESTADO.COMBOBOX_ESTADO];IsMeasure[false]")]
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
	    [Display(Name = "Decimal Estado", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    //Extensibility Partial Method Definitions For IdEstado
	    partial void OnIdEstadoChanging(int value);
	    partial void OnIdEstadoChanged();

	    private int _IdEstado;

	    [DataMember(IsRequired = true, Name = "IdEstado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Estado", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
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
	    [Display(Name = "Id Pais", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ESTADO.PAIS.ID_PAIS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="ESTADO.PAIS.ID_PAIS")]
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
	    //Extensibility Partial Method Definitions For StringEstado
	    partial void OnStringEstadoChanging(string value);
	    partial void OnStringEstadoChanged();

	    private string _StringEstado;

	    [DataMember(Name = "StringEstado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Estado", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[ESTADO.STRING_ESTADO];IsMeasure[false]")]
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

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "BMDTesteFrame.ESTADO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Demo.BM.ESTADO), QualifiedEntitySetName = "BMDTesteFrame.ESTADO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ESTADO.ID_ESTADO", Source = "IdEstado", Target = "ID_ESTADO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.ESTADO", RelationPropertyName = "ESTADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ESTADO.PAIS.ID_PAIS", Source = "IdPais", Target = "ID_PAIS", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BMDTesteFrame.PAIS", RelationPropertyName = "PAIS" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ESTADO.STRING_ESTADO", Source = "StringEstado", Target = "STRING_ESTADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.ESTADO", RelationPropertyName = "ESTADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ESTADO.DECIMAL_ESTADO", Source = "DecimalEstado", Target = "DECIMAL_ESTADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.ESTADO", RelationPropertyName = "ESTADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ESTADO.COMBOBOX_ESTADO", Source = "ComboboxEstado", Target = "COMBOBOX_ESTADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.ESTADO", RelationPropertyName = "ESTADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="ESTADO.DATETIME_ESTADO", Source = "DatetimeEstado", Target = "DATETIME_ESTADO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.ESTADO", RelationPropertyName = "ESTADO" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetComboboxEstadoValues()
	    {
	    	    return Linx.Demo.BV.Domains.LX_COMBOBOX_ESTADO.GetValues();
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
	
	    private Linx.Demo.BM.BMDTesteFrame _dbContext;
	    protected Linx.Demo.BM.BMDTesteFrame DbContext 
	    { 
	    	get 
	    	{
	        	if (this._dbContext == null)
	        	{
	        		this._dbContext = new Linx.Demo.BM.BMDTesteFrame(connectionString, this.Headers);
	        		((System.Data.Entity.Infrastructure.IObjectContextAdapter)this._dbContext).ObjectContext.CommandTimeout = 180;
	        		this._hasGpeconControl = (!(this._dbContext.IsUserMultiGpecon && this._dbContext.IdGpecon == this._dbContext.IdLinx) && this._dbContext.IdGpecon > 0);		
	        	}
	        	return this._dbContext;
	    	}
	    }

	    public string GetModelAssemblyName()
	    {
	        return typeof(Linx.Demo.BM.BMDTesteFrame).Assembly.FullName;
	    }

	    public System.Data.Entity.Database Database
	    {
	        get { return this.DbContext.Database; }
	    }

		
	    public ModalExternaDomainService() : this("", null, null) { }
	    public ModalExternaDomainService(string connectionString) : this(connectionString, null, null) { }
	    public ModalExternaDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public ModalExternaDomainService(Linx.Demo.BM.BMDTesteFrame dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public ModalExternaDomainService(string connectionString, Linx.Demo.BM.BMDTesteFrame dataContext, Dictionary<string, string> headers) : base() 
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
	    public Linx.Demo.BM.BMDTesteFrame GetEDM()
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
	    //Get All LookUpCidade.
	    public IQueryable<LookUpCidade> GetAllLookUpCidade()
	    {
	        return this.GetLookUpCidade(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpCidade By EntitySearch.
	    public IQueryable<LookUpCidade> GetLookUpCidadeByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpCidade(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpCidade.
	    public IQueryable<LookUpCidade> GetLookUpCidade(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "CIDADE" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpCidade";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpCidade));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpCidade> query =  
	
	            (from entity in this.DbContext.CIDADE.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.ESTADO
                  let entityAl2 = entity.ESTADO.PAIS
	            
	            select new LookUpCidade()		
	            {
	            
                IdCidade = entity.ID_CIDADE
                , IdEstado = entityAl1.ID_ESTADO
                , IdPais = entityAl2.ID_PAIS
	            });

	            
            //Inner Group Definition
            if (propertyName.InList("IdEstado"))
            {
               query = (from r in query select new LookUpCidade() {
               IdCidade = default(int)
               , IdEstado = r.IdEstado
               , IdPais = r.IdPais
                }).Distinct();
            }
            else if (propertyName.InList("IdPais"))
            {
               query = (from r in query select new LookUpCidade() {
               IdCidade = default(int)
               , IdEstado = default(System.Nullable<int>)
               , IdPais = r.IdPais
                }).Distinct();
            }
	
		
	
	
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
	            });

	            
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpVendedor.
	    public IQueryable<LookUpVendedor> GetAllLookUpVendedor()
	    {
	        return this.GetLookUpVendedor(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpVendedor By EntitySearch.
	    public IQueryable<LookUpVendedor> GetLookUpVendedorByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpVendedor(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpVendedor.
	    public IQueryable<LookUpVendedor> GetLookUpVendedor(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "VENDEDOR" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpVendedor";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpVendedor));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpVendedor> query =  
	
	            (from entity in this.DbContext.VENDEDOR.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpVendedor()		
	            {
	            
                IdVendedor = entity.ID_VENDEDOR
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
		

	        if (entityName.InList("Linx.Demo.BV.ModalExterna.Pais"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "Pais",
	        			NameSpace = "Linx.Demo.BV.ModalExterna",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "Pais",
	        			ClearMethodName = "ClearPais",
	        			QueryMethodName  = "GetPagedPais",	
	        			CountingMethodName  = "GetPais" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Demo.BV.ModalExterna.Pais"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Demo.BV.ModalExterna.Pais"), forceAll: forceAll)
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
	    //Clear Loja.
	    public IEnumerable<Loja> ClearLoja()
	    {
	        List<Loja> result = new List<Loja>();
	        result.Add(new Loja());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear Venda.
	    public IEnumerable<Venda> ClearVenda()
	    {
	        List<Venda> result = new List<Venda>();
	        result.Add(new Venda());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear Pais.
	    public IEnumerable<Pais> ClearPais()
	    {
	        List<Pais> result = new List<Pais>();
	        result.Add(new Pais());	
		
	        

	
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
	
		
	
	    [LojaQueryCustomAuthorizationAuto()]
	    [Query(HasSideEffects = false)]
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
                  let entity0Al1 = entity0.CIDADE
                  let entity0Al2 = entity0.CIDADE.ESTADO
                  let entity0Al3 = entity0.CIDADE.ESTADO.PAIS
	            
	            	
	            select new Loja()		
	            {
	            
                BigIntLoja = entity0.BIG_INT_LOJA
                , BitLoja = entity0.BIT_LOJA
                , ComboboxLoja = entity0.COMBOBOX_LOJA
                , ComboboxLojaName = ((entity0.COMBOBOX_LOJA) == 1 ? "LOJA1" : ((entity0.COMBOBOX_LOJA) == 2 ? "LOJA2" : ((entity0.COMBOBOX_LOJA) == 3 ? "LOJA3" : ((entity0.COMBOBOX_LOJA) == 4 ? "LOJA4" : ""))))
                , DatetimeLoja = entity0.DATETIME_LOJA
                , DecimalLoja = entity0.DECIMAL_LOJA
                , IdCidade = entity0Al1.ID_CIDADE
                , IdEstado = entity0Al2.ID_ESTADO
                , IdLoja = entity0.ID_LOJA
                , IdPais = entity0Al3.ID_PAIS
                , IntLoja = entity0.INT_LOJA
                , SmallIntLoja = entity0.SMALL_INT_LOJA
		
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
                  let entity0Al1 = entity0.CIDADE
                  let entity0Al2 = entity0.CIDADE.ESTADO
                  let entity0Al3 = entity0.CIDADE.ESTADO.PAIS
	            
	            	
	            select new Loja()		
	            {
	            
                BigIntLoja = entity0.BIG_INT_LOJA
                , BitLoja = entity0.BIT_LOJA
                , ComboboxLoja = entity0.COMBOBOX_LOJA
                , ComboboxLojaName = ((entity0.COMBOBOX_LOJA) == 1 ? "LOJA1" : ((entity0.COMBOBOX_LOJA) == 2 ? "LOJA2" : ((entity0.COMBOBOX_LOJA) == 3 ? "LOJA3" : ((entity0.COMBOBOX_LOJA) == 4 ? "LOJA4" : ""))))
                , DatetimeLoja = entity0.DATETIME_LOJA
                , DecimalLoja = entity0.DECIMAL_LOJA
                , IdCidade = entity0Al1.ID_CIDADE
                , IdEstado = entity0Al2.ID_ESTADO
                , IdLoja = entity0.ID_LOJA
                , IdPais = entity0Al3.ID_PAIS
                , IntLoja = entity0.INT_LOJA
                , SmallIntLoja = entity0.SMALL_INT_LOJA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendaQueryCustomAuthorizationAuto()]
	    [Query(HasSideEffects = false)]
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
                  let entity0Al1 = entity0.LOJA
                  let entity0Al2 = entity0.VENDEDOR
	            
	            	
	            select new Venda()		
	            {
	            
                BigIntVenda = entity0.BIG_INT_VENDA
                , BitVenda = entity0.BIT_VENDA
                , ComboboxVenda = entity0.COMBOBOX_VENDA
                , ComboboxVendaName = ((entity0.COMBOBOX_VENDA) == 1 ? "VENDA1" : ((entity0.COMBOBOX_VENDA) == 2 ? "VENDA2" : ((entity0.COMBOBOX_VENDA) == 3 ? "VENDA3" : "")))
                , DatetimeVenda = entity0.DATETIME_VENDA
                , DecimalVenda = entity0.DECIMAL_VENDA
                , IdLoja = entity0Al1.ID_LOJA
                , IdVenda = entity0.ID_VENDA
                , IdVendedor = entity0Al2.ID_VENDEDOR
                , IntVenda = entity0.INT_VENDA
                , SmallIntVenda = entity0.SMALL_INT_VENDA
		
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
                  let entity0Al1 = entity0.LOJA
                  let entity0Al2 = entity0.VENDEDOR
	            
	            	
	            select new Venda()		
	            {
	            
                BigIntVenda = entity0.BIG_INT_VENDA
                , BitVenda = entity0.BIT_VENDA
                , ComboboxVenda = entity0.COMBOBOX_VENDA
                , ComboboxVendaName = ((entity0.COMBOBOX_VENDA) == 1 ? "VENDA1" : ((entity0.COMBOBOX_VENDA) == 2 ? "VENDA2" : ((entity0.COMBOBOX_VENDA) == 3 ? "VENDA3" : "")))
                , DatetimeVenda = entity0.DATETIME_VENDA
                , DecimalVenda = entity0.DECIMAL_VENDA
                , IdLoja = entity0Al1.ID_LOJA
                , IdVenda = entity0.ID_VENDA
                , IdVendedor = entity0Al2.ID_VENDEDOR
                , IntVenda = entity0.INT_VENDA
                , SmallIntVenda = entity0.SMALL_INT_VENDA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [PaisQueryCustomAuthorizationAuto()]
	    [Query(HasSideEffects = false)]
	    //Get Pais.
	    public IQueryable<Pais> GetPais()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPais")))
 	        {
 	             AuthorizationResult authorizationResult = (new PaisQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<Pais> result = 
	            (from entity0 in this.DbContext.PAIS
	            
	            	
	            select new Pais()		
	            {
	            
                ComboboxPais = entity0.COMBOBOX_PAIS
                , ComboboxPaisName = ((entity0.COMBOBOX_PAIS) == 1 ? "PAIS1" : ((entity0.COMBOBOX_PAIS) == 2 ? "PAIS2" : ((entity0.COMBOBOX_PAIS) == 3 ? "PAIS3" : "")))
                , DecimalPais = entity0.DECIMAL_PAIS
                , IdPais = entity0.ID_PAIS
                , StringPais = entity0.STRING_PAIS
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [PaisQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PaisNoAssociations.
	    public IQueryable<Pais> GetPaisNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPaisNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new PaisQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<Pais> result = 
	            (from entity0 in this.DbContext.PAIS
	            
	            	
	            select new Pais()		
	            {
	            
                ComboboxPais = entity0.COMBOBOX_PAIS
                , ComboboxPaisName = ((entity0.COMBOBOX_PAIS) == 1 ? "PAIS1" : ((entity0.COMBOBOX_PAIS) == 2 ? "PAIS2" : ((entity0.COMBOBOX_PAIS) == 3 ? "PAIS3" : "")))
                , DecimalPais = entity0.DECIMAL_PAIS
                , IdPais = entity0.ID_PAIS
                , StringPais = entity0.STRING_PAIS
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [EstadoQueryCustomAuthorizationAuto()]
	    [Query(HasSideEffects = false)]
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
	            
                ComboboxEstado = entity0.COMBOBOX_ESTADO
                , ComboboxEstadoName = ((entity0.COMBOBOX_ESTADO) == 1 ? "ESTADO1" : ((entity0.COMBOBOX_ESTADO) == 2 ? "ESTADO2" : ((entity0.COMBOBOX_ESTADO) == 3 ? "ESTADO3" : ((entity0.COMBOBOX_ESTADO) == 4 ? "ESTADO4" : ""))))
                , DatetimeEstado = entity0.DATETIME_ESTADO
                , DecimalEstado = entity0.DECIMAL_ESTADO
                , IdEstado = entity0.ID_ESTADO
                , IdPais = entity0Al1.ID_PAIS
                , StringEstado = entity0.STRING_ESTADO
		
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
	            
                ComboboxEstado = entity0.COMBOBOX_ESTADO
                , ComboboxEstadoName = ((entity0.COMBOBOX_ESTADO) == 1 ? "ESTADO1" : ((entity0.COMBOBOX_ESTADO) == 2 ? "ESTADO2" : ((entity0.COMBOBOX_ESTADO) == 3 ? "ESTADO3" : ((entity0.COMBOBOX_ESTADO) == 4 ? "ESTADO4" : ""))))
                , DatetimeEstado = entity0.DATETIME_ESTADO
                , DecimalEstado = entity0.DECIMAL_ESTADO
                , IdEstado = entity0.ID_ESTADO
                , IdPais = entity0Al1.ID_PAIS
                , StringEstado = entity0.STRING_ESTADO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
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
	    	}
	    	//Add filtering disabled property for PAIS
	    	string[] bmDisabledPaisList = this.GetEDM().GetFilteringDisabledList("PAIS");
	    	if (bmDisabledPaisList.Length > 0)
	    	{
	
	    		if (bmDisabledPaisList.Contains("PAIS.COMBOBOX_PAIS"))
	    		{
	    			result.Add("Pais|ComboboxPais");
	    			result.Add("Pais|PAIS.COMBOBOX_PAIS");
	    		}
	
	    		if (bmDisabledPaisList.Contains("PAIS.DECIMAL_PAIS"))
	    		{
	    			result.Add("Pais|DecimalPais");
	    			result.Add("Pais|PAIS.DECIMAL_PAIS");
	    		}
	
	    		if (bmDisabledPaisList.Contains("PAIS.ID_PAIS"))
	    		{
	    			result.Add("Pais|IdPais");
	    			result.Add("Pais|PAIS.ID_PAIS");
	    		}
	
	    		if (bmDisabledPaisList.Contains("PAIS.STRING_PAIS"))
	    		{
	    			result.Add("Pais|StringPais");
	    			result.Add("Pais|PAIS.STRING_PAIS");
	    		}
	    	}
	    	//Add filtering disabled property for ESTADO
	    	string[] bmDisabledEstadoList = this.GetEDM().GetFilteringDisabledList("ESTADO");
	    	if (bmDisabledEstadoList.Length > 0)
	    	{
	
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
	
	    		if (bmDisabledEstadoList.Contains("ESTADO.ID_ESTADO"))
	    		{
	    			result.Add("Estado|IdEstado");
	    			result.Add("Estado|ESTADO.ID_ESTADO");
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
				
	    [Ignore]
	    //Get Loja By EntitySearchId.
	    public IQueryable<Loja> GetLojaByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetLojaByEntitySearch(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get Loja By EntitySearchId.
	    public IQueryable<Loja> GetLojaByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetLojaByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get Venda By EntitySearchId.
	    public IQueryable<Venda> GetVendaByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetVendaByEntitySearch(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get Venda By EntitySearchId.
	    public IQueryable<Venda> GetVendaByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetVendaByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get Pais By EntitySearchId.
	    public IQueryable<Pais> GetPaisByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetPaisByEntitySearch(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get Pais By EntitySearchId.
	    public IQueryable<Pais> GetPaisByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetPaisByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get Estado By EntitySearchId.
	    public IQueryable<Estado> GetEstadoByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetEstadoByEntitySearch(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get Estado By EntitySearchId.
	    public IQueryable<Estado> GetEstadoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetEstadoByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
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
			
	    //Get Venda By Example.
	    [Ignore]
	    public IQueryable<Venda> GetVendaByExample(Venda entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetVendaByEntitySearch(queryAnalysis);
	    }
			
	    //Get Venda By Example.
	    [Ignore]
	    public IQueryable<Venda> GetVendaByExampleNoAssociations(Venda entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetVendaByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get Pais By Example.
	    [Ignore]
	    public IQueryable<Pais> GetPaisByExample(Pais entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetPaisByEntitySearch(queryAnalysis);
	    }
			
	    //Get Pais By Example.
	    [Ignore]
	    public IQueryable<Pais> GetPaisByExampleNoAssociations(Pais entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetPaisByEntitySearchNoAssociations(queryAnalysis);
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
	    public Pais GetPaisByKey(int idPais)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("Pais");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdPais"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idPais));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetPaisByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
                  let entity0Al1 = entity0.CIDADE
                  let entity0Al2 = entity0.CIDADE.ESTADO
                  let entity0Al3 = entity0.CIDADE.ESTADO.PAIS
	            
	            	
	            select new Loja()		
	            {
	            
                BigIntLoja = entity0.BIG_INT_LOJA
                , BitLoja = entity0.BIT_LOJA
                , ComboboxLoja = entity0.COMBOBOX_LOJA
                , ComboboxLojaName = ((entity0.COMBOBOX_LOJA) == 1 ? "LOJA1" : ((entity0.COMBOBOX_LOJA) == 2 ? "LOJA2" : ((entity0.COMBOBOX_LOJA) == 3 ? "LOJA3" : ((entity0.COMBOBOX_LOJA) == 4 ? "LOJA4" : ""))))
                , DatetimeLoja = entity0.DATETIME_LOJA
                , DecimalLoja = entity0.DECIMAL_LOJA
                , IdCidade = entity0Al1.ID_CIDADE
                , IdEstado = entity0Al2.ID_ESTADO
                , IdLoja = entity0.ID_LOJA
                , IdPais = entity0Al3.ID_PAIS
                , IntLoja = entity0.INT_LOJA
                , SmallIntLoja = entity0.SMALL_INT_LOJA
		
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
                  let entity0Al1 = entity0.CIDADE
                  let entity0Al2 = entity0.CIDADE.ESTADO
                  let entity0Al3 = entity0.CIDADE.ESTADO.PAIS
	            
	            	
	            select new Loja()		
	            {
	            
                BigIntLoja = entity0.BIG_INT_LOJA
                , BitLoja = entity0.BIT_LOJA
                , ComboboxLoja = entity0.COMBOBOX_LOJA
                , ComboboxLojaName = ((entity0.COMBOBOX_LOJA) == 1 ? "LOJA1" : ((entity0.COMBOBOX_LOJA) == 2 ? "LOJA2" : ((entity0.COMBOBOX_LOJA) == 3 ? "LOJA3" : ((entity0.COMBOBOX_LOJA) == 4 ? "LOJA4" : ""))))
                , DatetimeLoja = entity0.DATETIME_LOJA
                , DecimalLoja = entity0.DECIMAL_LOJA
                , IdCidade = entity0Al1.ID_CIDADE
                , IdEstado = entity0Al2.ID_ESTADO
                , IdLoja = entity0.ID_LOJA
                , IdPais = entity0Al3.ID_PAIS
                , IntLoja = entity0.INT_LOJA
                , SmallIntLoja = entity0.SMALL_INT_LOJA
		
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
                  let entity0Al1 = entity0.LOJA
                  let entity0Al2 = entity0.VENDEDOR
	            
	            	
	            select new Venda()		
	            {
	            
                BigIntVenda = entity0.BIG_INT_VENDA
                , BitVenda = entity0.BIT_VENDA
                , ComboboxVenda = entity0.COMBOBOX_VENDA
                , ComboboxVendaName = ((entity0.COMBOBOX_VENDA) == 1 ? "VENDA1" : ((entity0.COMBOBOX_VENDA) == 2 ? "VENDA2" : ((entity0.COMBOBOX_VENDA) == 3 ? "VENDA3" : "")))
                , DatetimeVenda = entity0.DATETIME_VENDA
                , DecimalVenda = entity0.DECIMAL_VENDA
                , IdLoja = entity0Al1.ID_LOJA
                , IdVenda = entity0.ID_VENDA
                , IdVendedor = entity0Al2.ID_VENDEDOR
                , IntVenda = entity0.INT_VENDA
                , SmallIntVenda = entity0.SMALL_INT_VENDA
		
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
                  let entity0Al1 = entity0.LOJA
                  let entity0Al2 = entity0.VENDEDOR
	            
	            	
	            select new Venda()		
	            {
	            
                BigIntVenda = entity0.BIG_INT_VENDA
                , BitVenda = entity0.BIT_VENDA
                , ComboboxVenda = entity0.COMBOBOX_VENDA
                , ComboboxVendaName = ((entity0.COMBOBOX_VENDA) == 1 ? "VENDA1" : ((entity0.COMBOBOX_VENDA) == 2 ? "VENDA2" : ((entity0.COMBOBOX_VENDA) == 3 ? "VENDA3" : "")))
                , DatetimeVenda = entity0.DATETIME_VENDA
                , DecimalVenda = entity0.DECIMAL_VENDA
                , IdLoja = entity0Al1.ID_LOJA
                , IdVenda = entity0.ID_VENDA
                , IdVendedor = entity0Al2.ID_VENDEDOR
                , IntVenda = entity0.INT_VENDA
                , SmallIntVenda = entity0.SMALL_INT_VENDA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [PaisQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PaisByEntitySearch.
	    public IQueryable<Pais> GetPaisByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPaisByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new PaisQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Pais));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<Pais> result = 
	            (from entity0 in this.DbContext.PAIS.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new Pais()		
	            {
	            
                ComboboxPais = entity0.COMBOBOX_PAIS
                , ComboboxPaisName = ((entity0.COMBOBOX_PAIS) == 1 ? "PAIS1" : ((entity0.COMBOBOX_PAIS) == 2 ? "PAIS2" : ((entity0.COMBOBOX_PAIS) == 3 ? "PAIS3" : "")))
                , DecimalPais = entity0.DECIMAL_PAIS
                , IdPais = entity0.ID_PAIS
                , StringPais = entity0.STRING_PAIS
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [PaisQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PaisByEntitySearchNoAssociations.
	    public IQueryable<Pais> GetPaisByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPaisByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new PaisQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Pais));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<Pais> result = 
	            (from entity0 in this.DbContext.PAIS.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new Pais()		
	            {
	            
                ComboboxPais = entity0.COMBOBOX_PAIS
                , ComboboxPaisName = ((entity0.COMBOBOX_PAIS) == 1 ? "PAIS1" : ((entity0.COMBOBOX_PAIS) == 2 ? "PAIS2" : ((entity0.COMBOBOX_PAIS) == 3 ? "PAIS3" : "")))
                , DecimalPais = entity0.DECIMAL_PAIS
                , IdPais = entity0.ID_PAIS
                , StringPais = entity0.STRING_PAIS
		
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
	            
                ComboboxEstado = entity0.COMBOBOX_ESTADO
                , ComboboxEstadoName = ((entity0.COMBOBOX_ESTADO) == 1 ? "ESTADO1" : ((entity0.COMBOBOX_ESTADO) == 2 ? "ESTADO2" : ((entity0.COMBOBOX_ESTADO) == 3 ? "ESTADO3" : ((entity0.COMBOBOX_ESTADO) == 4 ? "ESTADO4" : ""))))
                , DatetimeEstado = entity0.DATETIME_ESTADO
                , DecimalEstado = entity0.DECIMAL_ESTADO
                , IdEstado = entity0.ID_ESTADO
                , IdPais = entity0Al1.ID_PAIS
                , StringEstado = entity0.STRING_ESTADO
		
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
	            
                ComboboxEstado = entity0.COMBOBOX_ESTADO
                , ComboboxEstadoName = ((entity0.COMBOBOX_ESTADO) == 1 ? "ESTADO1" : ((entity0.COMBOBOX_ESTADO) == 2 ? "ESTADO2" : ((entity0.COMBOBOX_ESTADO) == 3 ? "ESTADO3" : ((entity0.COMBOBOX_ESTADO) == 4 ? "ESTADO4" : ""))))
                , DatetimeEstado = entity0.DATETIME_ESTADO
                , DecimalEstado = entity0.DECIMAL_ESTADO
                , IdEstado = entity0.ID_ESTADO
                , IdPais = entity0Al1.ID_PAIS
                , StringEstado = entity0.STRING_ESTADO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
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
                  let entity0Al1 = entity0.CIDADE
                  let entity0Al2 = entity0.CIDADE.ESTADO
                  let entity0Al3 = entity0.CIDADE.ESTADO.PAIS
                orderby entity0.ID_LOJA ascending
	            
	            	
	            select new Loja()		
	            {
	            
                BigIntLoja = entity0.BIG_INT_LOJA
                , BitLoja = entity0.BIT_LOJA
                , ComboboxLoja = entity0.COMBOBOX_LOJA
                , ComboboxLojaName = ((entity0.COMBOBOX_LOJA) == 1 ? "LOJA1" : ((entity0.COMBOBOX_LOJA) == 2 ? "LOJA2" : ((entity0.COMBOBOX_LOJA) == 3 ? "LOJA3" : ((entity0.COMBOBOX_LOJA) == 4 ? "LOJA4" : ""))))
                , DatetimeLoja = entity0.DATETIME_LOJA
                , DecimalLoja = entity0.DECIMAL_LOJA
                , IdCidade = entity0Al1.ID_CIDADE
                , IdEstado = entity0Al2.ID_ESTADO
                , IdLoja = entity0.ID_LOJA
                , IdPais = entity0Al3.ID_PAIS
                , IntLoja = entity0.INT_LOJA
                , SmallIntLoja = entity0.SMALL_INT_LOJA
		
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
                  let entityAl1 = entity.CIDADE
                  let entityAl2 = entity.CIDADE.ESTADO
                  let entityAl3 = entity.CIDADE.ESTADO.PAIS
	            
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
                  let entity0Al1 = entity0.LOJA
                  let entity0Al2 = entity0.VENDEDOR
                orderby entity0.ID_VENDA ascending
	            
	            	
	            select new Venda()		
	            {
	            
                BigIntVenda = entity0.BIG_INT_VENDA
                , BitVenda = entity0.BIT_VENDA
                , ComboboxVenda = entity0.COMBOBOX_VENDA
                , ComboboxVendaName = ((entity0.COMBOBOX_VENDA) == 1 ? "VENDA1" : ((entity0.COMBOBOX_VENDA) == 2 ? "VENDA2" : ((entity0.COMBOBOX_VENDA) == 3 ? "VENDA3" : "")))
                , DatetimeVenda = entity0.DATETIME_VENDA
                , DecimalVenda = entity0.DECIMAL_VENDA
                , IdLoja = entity0Al1.ID_LOJA
                , IdVenda = entity0.ID_VENDA
                , IdVendedor = entity0Al2.ID_VENDEDOR
                , IntVenda = entity0.INT_VENDA
                , SmallIntVenda = entity0.SMALL_INT_VENDA
		
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
                  let entityAl1 = entity.LOJA
                  let entityAl2 = entity.VENDEDOR
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    [PaisQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedPais.
	    public IQueryable<Pais> GetPagedPais(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedPais")))
 	        {
 	             AuthorizationResult authorizationResult = (new PaisQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Pais));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<Pais> result = 
	            (from entity0 in this.DbContext.PAIS.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_PAIS ascending
	            
	            	
	            select new Pais()		
	            {
	            
                ComboboxPais = entity0.COMBOBOX_PAIS
                , ComboboxPaisName = ((entity0.COMBOBOX_PAIS) == 1 ? "PAIS1" : ((entity0.COMBOBOX_PAIS) == 2 ? "PAIS2" : ((entity0.COMBOBOX_PAIS) == 3 ? "PAIS3" : "")))
                , DecimalPais = entity0.DECIMAL_PAIS
                , IdPais = entity0.ID_PAIS
                , StringPais = entity0.STRING_PAIS
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetPaisCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Pais));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.PAIS.Where(dynQuery, parameters.ToArray())
	            
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
	            
                ComboboxEstado = entity0.COMBOBOX_ESTADO
                , ComboboxEstadoName = ((entity0.COMBOBOX_ESTADO) == 1 ? "ESTADO1" : ((entity0.COMBOBOX_ESTADO) == 2 ? "ESTADO2" : ((entity0.COMBOBOX_ESTADO) == 3 ? "ESTADO3" : ((entity0.COMBOBOX_ESTADO) == 4 ? "ESTADO4" : ""))))
                , DatetimeEstado = entity0.DATETIME_ESTADO
                , DecimalEstado = entity0.DECIMAL_ESTADO
                , IdEstado = entity0.ID_ESTADO
                , IdPais = entity0Al1.ID_PAIS
                , StringEstado = entity0.STRING_ESTADO
		
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
		
			
	    [PaisUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update Pais.
	    public void UpdatePais(Pais entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdatePais")))
 	        {
 	             AuthorizationResult authorizationResult = (new PaisUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    [PaisInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert Pais.
	    public void InsertPais(Pais entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertPais")))
 	        {
 	             AuthorizationResult authorizationResult = (new PaisInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    [PaisDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete Pais.
	    public void DeletePais(Pais entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeletePais")))
 	        {
 	             AuthorizationResult authorizationResult = (new PaisDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
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