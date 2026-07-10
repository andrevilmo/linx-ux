					
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
using LinxTraining002.BM;

namespace LinxTraining001.BV.CadastroVendas
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="Vendas.ID_Vendas", IsUpdatable=false, EdmName="LinxTraining002.BM.ModeloVendaCliente")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[VendasView];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IDVendas];ReadOnly[false];Entities[Vendas:IDVendas|Clientes:IDClientes];SubQueryInfo[];EdmEntityName[Vendas];EntityRelations[Clientes(Clientes)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "VendasView")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "LinxTraining001.BV.CadastroVendas.VendasView")]
	public partial class VendasView : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For Data
	    partial void OnDataChanging(System.DateTime value);
	    partial void OnDataChanged();

	    private System.DateTime _Data;

	    [DataMember(IsRequired = true, Name = "Data", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Data", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[Vendas.Data];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Vendas.Data")]
	    public System.DateTime Data
	    {
	    	    get
	    	    {
	    	          return _Data;
	    	    }
	    	    set
	    	    {
	    	          if (this._Data != value)
	    	          {
	    	              this.ValidateProperty("Data", value);
	    	              this.OnDataChanging(value);
	    	              this.RaiseDataMemberChanging("Data");
	    	              this._Data = value;
	    	              this.RaiseDataMemberChanged("Data");
	    	              this.OnDataChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IDClientes
	    partial void OnIDClientesChanging(System.Guid value);
	    partial void OnIDClientesChanged();

	    private System.Guid _IDClientes;

	    [DataMember(IsRequired = true, Name = "IDClientes", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Clientes", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpClientes];LookUpTitle[Seleção de (ID Clientes)];LookUpQuery[executeLookUpClientes];LookUpFinalize[finalizeLookUpClientes];LookUpDisplayColumns[{\"IDClientes\" : \"ID Clientes\", \"Nome\" : \"Nome\"}];LookUpColumns[{\"IDClientes\" : true, \"Nome\" : true}];FilterDataKey[Vendas.Clientes.ID_Clientes];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Guid#IDClientes#true##36:0##ID Clientes#0#true##::LookUpClientes##false#false#Clientes#Clientes#LinxTraining001.BV.CadastroVendas#IQueryable###true#false", EdmKey="Vendas.Clientes.ID_Clientes")]
	    public System.Guid IDClientes
	    {
	    	    get
	    	    {
	    	          return _IDClientes;
	    	    }
	    	    set
	    	    {
	    	          if (this._IDClientes != value)
	    	          {
	    	              this.ValidateProperty("IDClientes", value);
	    	              this.OnIDClientesChanging(value);
	    	              this.RaiseDataMemberChanging("IDClientes");
	    	              this._IDClientes = value;
	    	              this.RaiseDataMemberChanged("IDClientes");
	    	              this.OnIDClientesChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IDVendas
	    partial void OnIDVendasChanging(Int32 value);
	    partial void OnIDVendasChanged();

	    private Int32 _IDVendas;

	    [DataMember(IsRequired = true, Name = "IDVendas", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Vendas", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[Vendas.ID_Vendas];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Vendas.ID_Vendas")]
	    public Int32 IDVendas
	    {
	    	    get
	    	    {
	    	          return _IDVendas;
	    	    }
	    	    set
	    	    {
	    	          if (this._IDVendas != value)
	    	          {
	    	              this.ValidateProperty("IDVendas", value);
	    	              this.OnIDVendasChanging(value);
	    	              this.RaiseDataMemberChanging("IDVendas");
	    	              this._IDVendas = value;
	    	              this.RaiseDataMemberChanged("IDVendas");
	    	              this.OnIDVendasChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Nome
	    partial void OnNomeChanging(System.String value);
	    partial void OnNomeChanged();

	    private System.String _Nome;

	    [DataMember(Name = "Nome", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpClientes];LookUpTitle[Seleção de (Nome)];LookUpQuery[executeLookUpClientes];LookUpFinalize[finalizeLookUpClientes];LookUpDisplayColumns[{\"IDClientes\" : \"ID Clientes\", \"Nome\" : \"Nome\"}];LookUpColumns[{\"IDClientes\" : true, \"Nome\" : true}];FilterDataKey[Vendas.Clientes.Nome];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#Nome#false##40:0##Nome#1#true##::LookUpClientes##false#false#Clientes#Clientes#LinxTraining001.BV.CadastroVendas#IQueryable###true#false", EdmKey="Vendas.Clientes.Nome")]
	    public System.String Nome
	    {
	    	    get
	    	    {
	    	          return _Nome;
	    	    }
	    	    set
	    	    {
	    	          if (this._Nome != value)
	    	          {
	    	              this.ValidateProperty("Nome", value);
	    	              this.OnNomeChanging(value);
	    	              this.RaiseDataMemberChanging("Nome");
	    	              this._Nome = value;
	    	              this.RaiseDataMemberChanged("Nome");
	    	              this.OnNomeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Origem
	    partial void OnOrigemChanging(System.Nullable<System.Int32> value);
	    partial void OnOrigemChanged();

	    private System.Nullable<System.Int32> _Origem;

	    [DataMember(Name = "Origem", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Origem", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[LXOrigem];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[Vendas.Origem];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Vendas.Origem")]
	    public System.Nullable<System.Int32> Origem
	    {
	    	    get
	    	    {
	    	          return _Origem;
	    	    }
	    	    set
	    	    {
	    	          if (this._Origem != value)
	    	          {
	    	              this.ValidateProperty("Origem", value);
	    	              this.OnOrigemChanging(value);
	    	              this.RaiseDataMemberChanging("Origem");
	    	              this._Origem = value;
	    	              this.RaiseDataMemberChanged("Origem");
	    	              this.OnOrigemChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ValorTotal
	    partial void OnValorTotalChanging(System.Nullable<System.Decimal> value);
	    partial void OnValorTotalChanged();

	    private System.Nullable<System.Decimal> _ValorTotal;

	    [DataMember(Name = "ValorTotal", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ValorTotal", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[Vendas.ValorTotal];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Vendas.ValorTotal")]
	    public System.Nullable<System.Decimal> ValorTotal
	    {
	    	    get
	    	    {
	    	          return _ValorTotal;
	    	    }
	    	    set
	    	    {
	    	          if (this._ValorTotal != value)
	    	          {
	    	              this.ValidateProperty("ValorTotal", value);
	    	              this.OnValorTotalChanging(value);
	    	              this.RaiseDataMemberChanging("ValorTotal");
	    	              this._ValorTotal = value;
	    	              this.RaiseDataMemberChanged("ValorTotal");
	    	              this.OnValorTotalChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For VendaVip
	    partial void OnVendaVipChanging(System.Nullable<System.Boolean> value);
	    partial void OnVendaVipChanged();

	    private System.Nullable<System.Boolean> _VendaVip;

	    [DataMember(Name = "VendaVip", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "VendaVip", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[Vendas.VendaVip];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Vendas.VendaVip")]
	    public System.Nullable<System.Boolean> VendaVip
	    {
	    	    get
	    	    {
	    	          return _VendaVip;
	    	    }
	    	    set
	    	    {
	    	          if (this._VendaVip != value)
	    	          {
	    	              this.ValidateProperty("VendaVip", value);
	    	              this.OnVendaVipChanging(value);
	    	              this.RaiseDataMemberChanging("VendaVip");
	    	              this._VendaVip = value;
	    	              this.RaiseDataMemberChanged("VendaVip");
	    	              this.OnVendaVipChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIDVendas;
	    [DataMember(Name = "TemporaryIDVendas", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Vendas (Tmp)", Description="Temporary Key", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIDVendas
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIDVendas.IsNullOrEmpty())
	    	                this._TemporaryIDVendas = this._IDVendas;
	    	          return this._TemporaryIDVendas;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIDVendas != value)
	    	              this._TemporaryIDVendas = value;
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ModeloVendaCliente.Vendas").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(LinxTraining002.BM.Vendas), QualifiedEntitySetName = "ModeloVendaCliente.Vendas" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Vendas.Data", Source = "Data", Target = "Data", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.Vendas", RelationPropertyName = "Vendas" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Vendas.Origem", Source = "Origem", Target = "Origem", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.Vendas", RelationPropertyName = "Vendas" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Vendas.VendaVip", Source = "VendaVip", Target = "VendaVip", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.Vendas", RelationPropertyName = "Vendas" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Vendas.ID_Vendas", Source = "IDVendas", Target = "ID_Vendas", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.Vendas", RelationPropertyName = "Vendas" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Vendas.ValorTotal", Source = "ValorTotal", Target = "ValorTotal", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.Vendas", RelationPropertyName = "Vendas" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="Vendas.Clientes.ID_Clientes", Source = "IDClientes", Target = "ID_Clientes", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ModeloVendaCliente.Clientes", RelationPropertyName = "Clientes" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetOrigemValues()
	    {
	    	    return LinxTraining001.BV.Domains.LXOrigem.GetValues();
	    }
	    private string _origemName;
	    [DataMember(IsRequired = false, Name = "OrigemName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Origem", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string OrigemName
	    {
	    	    get { if (this.Origem.IsNullOrEmpty()) { _origemName = String.Empty; } else { string key = this.Origem.ToString(); var dmValues = this.GetOrigemValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _origemName) _origemName = domainName; } return _origemName; } set { _origemName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TestePIVOT.ID_TestePIVOT", IsUpdatable=false, EdmName="LinxTraining002.BM.ModeloVendaCliente")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TestePIVOTView];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IDTestePIVOT];ReadOnly[false];Entities[TestePIVOT:IDTestePIVOT];SubQueryInfo[];EdmEntityName[TestePIVOT];EntityRelations[];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TestePIVOTView")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "LinxTraining001.BV.CadastroVendas.TestePIVOTView")]
	public partial class TestePIVOTView : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For Bolean
	    partial void OnBoleanChanging(System.Nullable<System.Boolean> value);
	    partial void OnBoleanChanged();

	    private System.Nullable<System.Boolean> _Bolean;

	    [DataMember(Name = "Bolean", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bolean", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TestePIVOT.Bolean];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TestePIVOT.Bolean")]
	    public System.Nullable<System.Boolean> Bolean
	    {
	    	    get
	    	    {
	    	          return _Bolean;
	    	    }
	    	    set
	    	    {
	    	          if (this._Bolean != value)
	    	          {
	    	              this.ValidateProperty("Bolean", value);
	    	              this.OnBoleanChanging(value);
	    	              this.RaiseDataMemberChanging("Bolean");
	    	              this._Bolean = value;
	    	              this.RaiseDataMemberChanged("Bolean");
	    	              this.OnBoleanChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Data
	    partial void OnDataChanging(System.Nullable<System.DateTime> value);
	    partial void OnDataChanged();

	    private System.Nullable<System.DateTime> _Data;

	    [DataMember(Name = "Data", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Data", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TestePIVOT.Data];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TestePIVOT.Data")]
	    public System.Nullable<System.DateTime> Data
	    {
	    	    get
	    	    {
	    	          return _Data;
	    	    }
	    	    set
	    	    {
	    	          if (this._Data != value)
	    	          {
	    	              this.ValidateProperty("Data", value);
	    	              this.OnDataChanging(value);
	    	              this.RaiseDataMemberChanging("Data");
	    	              this._Data = value;
	    	              this.RaiseDataMemberChanged("Data");
	    	              this.OnDataChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Decimal
	    partial void OnDecimalChanging(System.Decimal value);
	    partial void OnDecimalChanged();

	    private System.Decimal _Decimal;

	    [DataMember(IsRequired = true, Name = "Decimal", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Decimal", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[10:2];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[N2];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TestePIVOT.Decimal];IsMeasure[true]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TestePIVOT.Decimal")]
	    public System.Decimal Decimal
	    {
	    	    get
	    	    {
	    	          return _Decimal;
	    	    }
	    	    set
	    	    {
	    	          if (this._Decimal != value)
	    	          {
	    	              this.ValidateProperty("Decimal", value);
	    	              this.OnDecimalChanging(value);
	    	              this.RaiseDataMemberChanging("Decimal");
	    	              this._Decimal = value;
	    	              this.RaiseDataMemberChanged("Decimal");
	    	              this.OnDecimalChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IDTestePIVOT
	    partial void OnIDTestePIVOTChanging(Int32 value);
	    partial void OnIDTestePIVOTChanged();

	    private Int32 _IDTestePIVOT;

	    [DataMember(IsRequired = true, Name = "IDTestePIVOT", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID TestePIVOT", Description="", Order = 4, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TestePIVOT.ID_TestePIVOT];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TestePIVOT.ID_TestePIVOT")]
	    public Int32 IDTestePIVOT
	    {
	    	    get
	    	    {
	    	          return _IDTestePIVOT;
	    	    }
	    	    set
	    	    {
	    	          if (this._IDTestePIVOT != value)
	    	          {
	    	              this.ValidateProperty("IDTestePIVOT", value);
	    	              this.OnIDTestePIVOTChanging(value);
	    	              this.RaiseDataMemberChanging("IDTestePIVOT");
	    	              this._IDTestePIVOT = value;
	    	              this.RaiseDataMemberChanged("IDTestePIVOT");
	    	              this.OnIDTestePIVOTChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Inteiro
	    partial void OnInteiroChanging(Int32 value);
	    partial void OnInteiroChanged();

	    private Int32 _Inteiro;

	    [DataMember(IsRequired = true, Name = "Inteiro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inteiro", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TestePIVOT.Inteiro];IsMeasure[true]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TestePIVOT.Inteiro")]
	    public Int32 Inteiro
	    {
	    	    get
	    	    {
	    	          return _Inteiro;
	    	    }
	    	    set
	    	    {
	    	          if (this._Inteiro != value)
	    	          {
	    	              this.ValidateProperty("Inteiro", value);
	    	              this.OnInteiroChanging(value);
	    	              this.RaiseDataMemberChanging("Inteiro");
	    	              this._Inteiro = value;
	    	              this.RaiseDataMemberChanged("Inteiro");
	    	              this.OnInteiroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For String
	    partial void OnStringChanging(System.String value);
	    partial void OnStringChanged();

	    private System.String _String;

	    [DataMember(Name = "String", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TestePIVOT.String];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TestePIVOT.String")]
	    public System.String String
	    {
	    	    get
	    	    {
	    	          return _String;
	    	    }
	    	    set
	    	    {
	    	          if (this._String != value)
	    	          {
	    	              this.ValidateProperty("String", value);
	    	              this.OnStringChanging(value);
	    	              this.RaiseDataMemberChanging("String");
	    	              this._String = value;
	    	              this.RaiseDataMemberChanged("String");
	    	              this.OnStringChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIDTestePIVOT;
	    [DataMember(Name = "TemporaryIDTestePIVOT", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID TestePIVOT (Tmp)", Description="Temporary Key", Order = 4, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIDTestePIVOT
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIDTestePIVOT.IsNullOrEmpty())
	    	                this._TemporaryIDTestePIVOT = this._IDTestePIVOT;
	    	          return this._TemporaryIDTestePIVOT;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIDTestePIVOT != value)
	    	              this._TemporaryIDTestePIVOT = value;
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ModeloVendaCliente.TestePIVOT").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(LinxTraining002.BM.TestePIVOT), QualifiedEntitySetName = "ModeloVendaCliente.TestePIVOT" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TestePIVOT.Data", Source = "Data", Target = "Data", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TestePIVOT", RelationPropertyName = "TestePIVOT" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TestePIVOT.Bolean", Source = "Bolean", Target = "Bolean", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TestePIVOT", RelationPropertyName = "TestePIVOT" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TestePIVOT.String", Source = "String", Target = "String", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TestePIVOT", RelationPropertyName = "TestePIVOT" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TestePIVOT.Decimal", Source = "Decimal", Target = "Decimal", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TestePIVOT", RelationPropertyName = "TestePIVOT" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TestePIVOT.Inteiro", Source = "Inteiro", Target = "Inteiro", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TestePIVOT", RelationPropertyName = "TestePIVOT" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TestePIVOT.ID_TestePIVOT", Source = "IDTestePIVOT", Target = "ID_TestePIVOT", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ModeloVendaCliente.TestePIVOT", RelationPropertyName = "TestePIVOT" });

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
	[DomainIdentifier("ProcessorOverviewCadastroVendasDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class CadastroVendasDomainService : DomainService, IDataServiceContext 
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

	
	    private LinxTraining002.BM.ModeloVendaCliente _dbContext;
	    protected LinxTraining002.BM.ModeloVendaCliente DbContext 
	    { 
	    	get 
	    	{
	        	if (this._dbContext == null)
	        	{
	        		this._dbContext = new LinxTraining002.BM.ModeloVendaCliente(connectionString, this.Headers);
	        		((System.Data.Entity.Infrastructure.IObjectContextAdapter)this._dbContext).ObjectContext.CommandTimeout = 180;
	        	}
	        	return this._dbContext;
	    	}
	    }

	    public string GetModelAssemblyName()
	    {
	        return typeof(LinxTraining002.BM.ModeloVendaCliente).Assembly.FullName;
	    }

	    public System.Data.Entity.Database Database
	    {
	        get { return this.DbContext.Database; }
	    }

		
	    public CadastroVendasDomainService() : this("", null, null){ }
	    public CadastroVendasDomainService(string connectionString) : this(connectionString, null, null) { }
	    public CadastroVendasDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public CadastroVendasDomainService(LinxTraining002.BM.ModeloVendaCliente dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public CadastroVendasDomainService(string connectionString, LinxTraining002.BM.ModeloVendaCliente dataContext, Dictionary<string, string> headers) : base() 
	    { 
	    	this.connectionString = connectionString;
	    	this.Headers = headers;
	    	this._dbContext = dataContext; 


	    	this.OnCreate(); 
	    }

	    [Ignore]
	    public List<DataKeyMapping> SaveEntities(List<ChangeSetEntry> changeSetEntries)
	    {
	      if (changeSetEntries.Count == 0) return null;
	      
	      this.Initialize();
	      _keyMappings.Clear();
	      _controlKeyMapping = true;
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
	    public LinxTraining002.BM.ModeloVendaCliente GetEDM()
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
	    //Get All LookUpClientes.
	    public IQueryable<LookUpClientes> GetAllLookUpClientes()
	    {
	        return this.GetLookUpClientes(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpClientes By EntitySearch.
	    public IQueryable<LookUpClientes> GetLookUpClientesByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpClientes(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpClientes.
	    public IQueryable<LookUpClientes> GetLookUpClientes(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "Clientes" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpClientes";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpClientes));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpClientes> query =  
	
	            (from entity in this.DbContext.Clientes.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpClientes()		
	            {
	            
                IDClientes = entity.ID_Clientes
                , Nome = entity.Nome
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
	
		

	        if (entityName.InList("LinxTraining001.BV.CadastroVendas.VendasView"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "VendasView",
	        			NameSpace = "LinxTraining001.BV.CadastroVendas",
	        			ParentClassName = null,	
	        			DisplayName = "VendasView",
	        			ClearMethodName = "ClearVendasView",
	        			QueryMethodName  = "GetPagedVendasView",	
	        			CountingMethodName  = "GetVendasView" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("LinxTraining001.BV.CadastroVendas.VendasView"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("LinxTraining001.BV.CadastroVendas.VendasView"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("LinxTraining001.BV.CadastroVendas.TestePIVOTView"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TestePIVOTView",
	        			NameSpace = "LinxTraining001.BV.CadastroVendas",
	        			ParentClassName = null,	
	        			DisplayName = "TestePIVOTView",
	        			ClearMethodName = "ClearTestePIVOTView",
	        			QueryMethodName  = "GetPagedTestePIVOTView",	
	        			CountingMethodName  = "GetTestePIVOTView" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("LinxTraining001.BV.CadastroVendas.TestePIVOTView"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("LinxTraining001.BV.CadastroVendas.TestePIVOTView"), forceAll: forceAll)
	        		});
	        }
	
            return result;
        }
	
	    [Ignore]
	    public string[] GetClientDomains()
        {	


             return new string[] { "LinxTraining001_MobileDataDomains", Linx.Tools.AssemblyHelper.ReadResourceContent("LinxTraining001.BV.ClientResources.MobileDataDomains.res", System.Reflection.Assembly.GetExecutingAssembly()) };	

        }

	    [Ignore]
	    public string[] GetClientService()
        {	


             return new string[] { "LinxTraining001_cadastroVendasService", Linx.Tools.AssemblyHelper.ReadResourceContent("LinxTraining001.BV.ClientResources.cadastroVendasService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	

        }

	    [Ignore]
	    public string[] GetClientFactory(string entityName)
        {	


             return new string[] { };	

        }

	    [Ignore]
	    public string[] GetClientFactoryCustomEvents(string entityName)
        {	


             return new string[] { };	

        }
	
	    #endregion Get Meta Data.
	
	    #region Clear Methods Definitions.
	
		
	
	    [Ignore]
	    //Clear VendasView.
	    public IEnumerable<VendasView> ClearVendasView()
	    {
	        List<VendasView> result = new List<VendasView>();
	        result.Add(new VendasView());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TestePIVOTView.
	    public IEnumerable<TestePIVOTView> ClearTestePIVOTView()
	    {
	        List<TestePIVOTView> result = new List<TestePIVOTView>();
	        result.Add(new TestePIVOTView());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    [VendasViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendasView.
	    public IQueryable<VendasView> GetVendasView()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendasView")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendasViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<VendasView> result = 
	            (from entity0 in this.DbContext.Vendas
                  let entity0Al1 = entity0.Clientes
	            
	            	
	            select new VendasView()		
	            {
	            
                Data = entity0.Data
                , IDClientes = entity0Al1.ID_Clientes
                , IDVendas = entity0.ID_Vendas
                , Nome = entity0Al1.Nome
                , Origem = entity0.Origem
                , OrigemName = ((entity0.Origem) == 1 ? "Internet" : ((entity0.Origem) == 2 ? "Loja Física" : ""))
                , ValorTotal = entity0.ValorTotal
                , VendaVip = entity0.VendaVip
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [VendasViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendasViewNoAssociations.
	    public IQueryable<VendasView> GetVendasViewNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendasViewNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendasViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<VendasView> result = 
	            (from entity0 in this.DbContext.Vendas
                  let entity0Al1 = entity0.Clientes
	            
	            	
	            select new VendasView()		
	            {
	            
                Data = entity0.Data
                , IDClientes = entity0Al1.ID_Clientes
                , IDVendas = entity0.ID_Vendas
                , Nome = entity0Al1.Nome
                , Origem = entity0.Origem
                , OrigemName = ((entity0.Origem) == 1 ? "Internet" : ((entity0.Origem) == 2 ? "Loja Física" : ""))
                , ValorTotal = entity0.ValorTotal
                , VendaVip = entity0.VendaVip
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TestePIVOTViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TestePIVOTView.
	    public IQueryable<TestePIVOTView> GetTestePIVOTView()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTestePIVOTView")))
 	        {
 	             AuthorizationResult authorizationResult = (new TestePIVOTViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<TestePIVOTView> result = 
	            (from entity0 in this.DbContext.TestePIVOT
	            
	            	
	            select new TestePIVOTView()		
	            {
	            
                Bolean = entity0.Bolean
                , Data = entity0.Data
                , Decimal = entity0.Decimal
                , IDTestePIVOT = entity0.ID_TestePIVOT
                , Inteiro = entity0.Inteiro
                , String = entity0.String
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TestePIVOTViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TestePIVOTViewNoAssociations.
	    public IQueryable<TestePIVOTView> GetTestePIVOTViewNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTestePIVOTViewNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new TestePIVOTViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<TestePIVOTView> result = 
	            (from entity0 in this.DbContext.TestePIVOT
	            
	            	
	            select new TestePIVOTView()		
	            {
	            
                Bolean = entity0.Bolean
                , Data = entity0.Data
                , Decimal = entity0.Decimal
                , IDTestePIVOT = entity0.ID_TestePIVOT
                , Inteiro = entity0.Inteiro
                , String = entity0.String
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	result.Add("VendasView|ValorTotal");
	    	result.Add("VendasView|Vendas.ValorTotal");
	    	//Add filtering disabled property for Vendas
	    	string[] bmDisabledVendasViewList = this.GetEDM().GetFilteringDisabledList("Vendas");
	    	if (bmDisabledVendasViewList.Length > 0)
	    	{
	
	    		if (bmDisabledVendasViewList.Contains("Vendas.Data"))
	    		{
	    			result.Add("VendasView|Data");
	    			result.Add("VendasView|Vendas.Data");
	    		}
	
	    		if (bmDisabledVendasViewList.Contains("Vendas.ID_Vendas"))
	    		{
	    			result.Add("VendasView|IDVendas");
	    			result.Add("VendasView|Vendas.ID_Vendas");
	    		}
	
	    		if (bmDisabledVendasViewList.Contains("Vendas.Origem"))
	    		{
	    			result.Add("VendasView|Origem");
	    			result.Add("VendasView|Vendas.Origem");
	    		}
	
	    		if (bmDisabledVendasViewList.Contains("Vendas.VendaVip"))
	    		{
	    			result.Add("VendasView|VendaVip");
	    			result.Add("VendasView|Vendas.VendaVip");
	    		}
	    	}
	    	//Add filtering disabled property for TestePIVOT
	    	string[] bmDisabledTestePIVOTViewList = this.GetEDM().GetFilteringDisabledList("TestePIVOT");
	    	if (bmDisabledTestePIVOTViewList.Length > 0)
	    	{
	
	    		if (bmDisabledTestePIVOTViewList.Contains("TestePIVOT.Bolean"))
	    		{
	    			result.Add("TestePIVOTView|Bolean");
	    			result.Add("TestePIVOTView|TestePIVOT.Bolean");
	    		}
	
	    		if (bmDisabledTestePIVOTViewList.Contains("TestePIVOT.Data"))
	    		{
	    			result.Add("TestePIVOTView|Data");
	    			result.Add("TestePIVOTView|TestePIVOT.Data");
	    		}
	
	    		if (bmDisabledTestePIVOTViewList.Contains("TestePIVOT.Decimal"))
	    		{
	    			result.Add("TestePIVOTView|Decimal");
	    			result.Add("TestePIVOTView|TestePIVOT.Decimal");
	    		}
	
	    		if (bmDisabledTestePIVOTViewList.Contains("TestePIVOT.ID_TestePIVOT"))
	    		{
	    			result.Add("TestePIVOTView|IDTestePIVOT");
	    			result.Add("TestePIVOTView|TestePIVOT.ID_TestePIVOT");
	    		}
	
	    		if (bmDisabledTestePIVOTViewList.Contains("TestePIVOT.Inteiro"))
	    		{
	    			result.Add("TestePIVOTView|Inteiro");
	    			result.Add("TestePIVOTView|TestePIVOT.Inteiro");
	    		}
	
	    		if (bmDisabledTestePIVOTViewList.Contains("TestePIVOT.String"))
	    		{
	    			result.Add("TestePIVOTView|String");
	    			result.Add("TestePIVOTView|TestePIVOT.String");
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
	    //Get VendasView By EntitySearchId.
	    public IQueryable<VendasView> GetVendasViewByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetVendasViewByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get VendasView By EntitySearchId.
	    public IQueryable<VendasView> GetVendasViewByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetVendasViewByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get TestePIVOTView By EntitySearchId.
	    public IQueryable<TestePIVOTView> GetTestePIVOTViewByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTestePIVOTViewByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get TestePIVOTView By EntitySearchId.
	    public IQueryable<TestePIVOTView> GetTestePIVOTViewByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTestePIVOTViewByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get VendasView By Example.
	    [Ignore]
	    public IQueryable<VendasView> GetVendasViewByExample(VendasView entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetVendasViewByEntitySearch(queryAnalysis);
	    }
			
	    //Get VendasView By Example.
	    [Ignore]
	    public IQueryable<VendasView> GetVendasViewByExampleNoAssociations(VendasView entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetVendasViewByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TestePIVOTView By Example.
	    [Ignore]
	    public IQueryable<TestePIVOTView> GetTestePIVOTViewByExample(TestePIVOTView entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTestePIVOTViewByEntitySearch(queryAnalysis);
	    }
			
	    //Get TestePIVOTView By Example.
	    [Ignore]
	    public IQueryable<TestePIVOTView> GetTestePIVOTViewByExampleNoAssociations(TestePIVOTView entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTestePIVOTViewByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public VendasView GetVendasViewByKey(Int32 iDVendas)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("VendasView");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IDVendas"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, iDVendas));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetVendasViewByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TestePIVOTView GetTestePIVOTViewByKey(Int32 iDTestePIVOT)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TestePIVOTView");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IDTestePIVOT"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, iDTestePIVOT));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTestePIVOTViewByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    [VendasViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendasViewByEntitySearch.
	    public IQueryable<VendasView> GetVendasViewByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendasViewByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendasViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(VendasView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<VendasView> result = 
	            (from entity0 in this.DbContext.Vendas.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.Clientes
	            
	            	
	            select new VendasView()		
	            {
	            
                Data = entity0.Data
                , IDClientes = entity0Al1.ID_Clientes
                , IDVendas = entity0.ID_Vendas
                , Nome = entity0Al1.Nome
                , Origem = entity0.Origem
                , OrigemName = ((entity0.Origem) == 1 ? "Internet" : ((entity0.Origem) == 2 ? "Loja Física" : ""))
                , ValorTotal = entity0.ValorTotal
                , VendaVip = entity0.VendaVip
		
	            }
	            );
	
	        SetVendasViewBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    [VendasViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get VendasViewByEntitySearchNoAssociations.
	    public IQueryable<VendasView> GetVendasViewByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetVendasViewByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendasViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(VendasView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<VendasView> result = 
	            (from entity0 in this.DbContext.Vendas.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.Clientes
	            
	            	
	            select new VendasView()		
	            {
	            
                Data = entity0.Data
                , IDClientes = entity0Al1.ID_Clientes
                , IDVendas = entity0.ID_Vendas
                , Nome = entity0Al1.Nome
                , Origem = entity0.Origem
                , OrigemName = ((entity0.Origem) == 1 ? "Internet" : ((entity0.Origem) == 2 ? "Loja Física" : ""))
                , ValorTotal = entity0.ValorTotal
                , VendaVip = entity0.VendaVip
		
	            }
	            );
	
	        SetVendasViewBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
	
	    [Ignore()]
	    private void SetVendasViewBusinessFilter(ref IQueryable<VendasView> query, List<EntitySearch> entitySearchList)
	    {
	    		int idxElement;
	    		string operatorValue;
	    		object value;
	    		//Get query by functions
	    		if (entitySearchList.Count > 0)
	    		{
	    			foreach (EntitySearch search in entitySearchList.Where(e => e.EntityName == "VendasView"))
	    			{

	
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "ValorTotal" || e.Value.ToString() == "Vendas.ValorTotal")))
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
	    										System.Nullable<System.Decimal> tmpValorTotal1 = (System.Nullable<System.Decimal>)value;
	    										query = from r in query where r.ValorTotal == tmpValorTotal1 select r;
	    										break;
	    									case "!=":
	    										System.Nullable<System.Decimal> tmpValorTotal2 = (System.Nullable<System.Decimal>)value;
	    										query = from r in query where r.ValorTotal != tmpValorTotal2 select r;
	    										break;

	
	    									case "<":
	    										System.Nullable<System.Decimal> tmpValorTotal3 = (System.Nullable<System.Decimal>)value;
	    										query = from r in query where r.ValorTotal < tmpValorTotal3 select r;
	    										break;
	    									case "<=":
	    										System.Nullable<System.Decimal> tmpValorTotal4 = (System.Nullable<System.Decimal>)value;
	    										query = from r in query where r.ValorTotal <= tmpValorTotal4 select r;
	    										break;
	    									case ">":
	    										System.Nullable<System.Decimal> tmpValorTotal5 = (System.Nullable<System.Decimal>)value;
	    										query = from r in query where r.ValorTotal > tmpValorTotal5 select r;
	    										break;
	    									case ">=":
	    										System.Nullable<System.Decimal> tmpValorTotal6 = (System.Nullable<System.Decimal>)value;
	    										query = from r in query where r.ValorTotal >= tmpValorTotal6 select r;
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


		
	
	    [TestePIVOTViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TestePIVOTViewByEntitySearch.
	    public IQueryable<TestePIVOTView> GetTestePIVOTViewByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTestePIVOTViewByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new TestePIVOTViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TestePIVOTView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TestePIVOTView> result = 
	            (from entity0 in this.DbContext.TestePIVOT.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TestePIVOTView()		
	            {
	            
                Bolean = entity0.Bolean
                , Data = entity0.Data
                , Decimal = entity0.Decimal
                , IDTestePIVOT = entity0.ID_TestePIVOT
                , Inteiro = entity0.Inteiro
                , String = entity0.String
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TestePIVOTViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TestePIVOTViewByEntitySearchNoAssociations.
	    public IQueryable<TestePIVOTView> GetTestePIVOTViewByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTestePIVOTViewByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new TestePIVOTViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TestePIVOTView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TestePIVOTView> result = 
	            (from entity0 in this.DbContext.TestePIVOT.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TestePIVOTView()		
	            {
	            
                Bolean = entity0.Bolean
                , Data = entity0.Data
                , Decimal = entity0.Decimal
                , IDTestePIVOT = entity0.ID_TestePIVOT
                , Inteiro = entity0.Inteiro
                , String = entity0.String
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    [VendasViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedVendasView.
	    public IQueryable<VendasView> GetPagedVendasView(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedVendasView")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendasViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(VendasView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<VendasView> result = 
	            (from entity0 in this.DbContext.Vendas.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.Clientes
                orderby entity0.ID_Vendas ascending
	            
	            	
	            select new VendasView()		
	            {
	            
                Data = entity0.Data
                , IDClientes = entity0Al1.ID_Clientes
                , IDVendas = entity0.ID_Vendas
                , Nome = entity0Al1.Nome
                , Origem = entity0.Origem
                , OrigemName = ((entity0.Origem) == 1 ? "Internet" : ((entity0.Origem) == 2 ? "Loja Física" : ""))
                , ValorTotal = entity0.ValorTotal
                , VendaVip = entity0.VendaVip
		
	            }
	            ).Skip(skip).Take(take);
	
	        SetVendasViewBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetVendasViewCounting(string serializedEntitySearch)
	    {	
		 	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(VendasView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.Vendas.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.Clientes
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    [TestePIVOTViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedTestePIVOTView.
	    public IQueryable<TestePIVOTView> GetPagedTestePIVOTView(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedTestePIVOTView")))
 	        {
 	             AuthorizationResult authorizationResult = (new TestePIVOTViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TestePIVOTView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TestePIVOTView> result = 
	            (from entity0 in this.DbContext.TestePIVOT.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_TestePIVOT ascending
	            
	            	
	            select new TestePIVOTView()		
	            {
	            
                Bolean = entity0.Bolean
                , Data = entity0.Data
                , Decimal = entity0.Decimal
                , IDTestePIVOT = entity0.ID_TestePIVOT
                , Inteiro = entity0.Inteiro
                , String = entity0.String
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTestePIVOTViewCounting(string serializedEntitySearch)
	    {	
		 	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TestePIVOTView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TestePIVOT.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    [VendasViewUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update VendasView.
	    public void UpdateVendasView(VendasView entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateVendasView")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendasViewUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    [VendasViewInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert VendasView.
	    public void InsertVendasView(VendasView entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertVendasView")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendasViewInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    [VendasViewDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete VendasView.
	    public void DeleteVendasView(VendasView entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteVendasView")))
 	        {
 	             AuthorizationResult authorizationResult = (new VendasViewDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    [TestePIVOTViewUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update TestePIVOTView.
	    public void UpdateTestePIVOTView(TestePIVOTView entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateTestePIVOTView")))
 	        {
 	             AuthorizationResult authorizationResult = (new TestePIVOTViewUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    [TestePIVOTViewInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert TestePIVOTView.
	    public void InsertTestePIVOTView(TestePIVOTView entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertTestePIVOTView")))
 	        {
 	             AuthorizationResult authorizationResult = (new TestePIVOTViewInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    [TestePIVOTViewDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete TestePIVOTView.
	    public void DeleteTestePIVOTView(TestePIVOTView entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteTestePIVOTView")))
 	        {
 	             AuthorizationResult authorizationResult = (new TestePIVOTViewDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
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